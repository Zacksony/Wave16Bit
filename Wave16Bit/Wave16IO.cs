using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Wave16Bit;

public unsafe static class Wave16IO
{
  private static readonly byte[] Bytes_RIFF = Encoding.ASCII.GetBytes("RIFF");
  private static readonly byte[] Bytes_WAVE = Encoding.ASCII.GetBytes("WAVE");
  private static readonly byte[] Bytes_fmt = Encoding.ASCII.GetBytes("fmt ");
  private static readonly byte[] Bytes_data = Encoding.ASCII.GetBytes("data");

  public static Wave16 Load(string filePath)
  {
    using FileStream rawDataStream = File.OpenRead(filePath);
    using BinaryReader reader = new(rawDataStream);

    rawDataStream.Position += 16;
    int fmtChunkSize = reader.ReadInt32();
    rawDataStream.Position += 4;
    int sampleRate = reader.ReadInt32();
    rawDataStream.Position += Math.Max(0, fmtChunkSize - 8);

    nint data = 0;
    int dataSize = 0;    

    while (rawDataStream.Position < rawDataStream.Length)
    {
      string chunkId = Encoding.ASCII.GetString(reader.ReadBytes(4));
      int chunkSize = reader.ReadInt32();
      if (chunkId != "data")
      {
        rawDataStream.Position += chunkSize;
        continue;
      }

      nint chunkData = Marshal.AllocCoTaskMem(chunkSize);

      try
      {
        Span<byte> chunkDataSpan = new(chunkData.ToPointer(), chunkSize);
        reader.Read(chunkDataSpan);
      }
      catch
      {
        Marshal.FreeCoTaskMem(chunkData);
        throw;
      }

      data = chunkData;
      dataSize = chunkSize;      
      break;
    }

    return new(data, dataSize, sampleRate);
  }

  public static void Save(Wave16 wave, string destFilePath, bool overwrite = false)
  {
    FileStream destStream;

    if (overwrite)
    {
      destStream = File.Create(destFilePath);
    }
    else
    {
      if (File.Exists(destFilePath))
      {
        string tempPath = Path.Combine(Path.GetDirectoryName(destFilePath)!, "conflicts-" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempPath);
        destStream = File.Create(Path.Combine(tempPath, Path.GetFileName(destFilePath)));
      }
      else
      {
        destStream = File.Create(destFilePath);
      }
    }

    using BinaryWriter writer = new(destStream);

#pragma warning disable IDE0004

    writer.Write(Bytes_RIFF);
    writer.Write((int)wave.TotalChunkSize);
    writer.Write(Bytes_WAVE);
    writer.Write(Bytes_fmt);
    writer.Write((int)Wave16.SubChunk1Size);
    writer.Write((short)Wave16.AudioFormat);
    writer.Write((short)Wave16.ChannelCount);
    writer.Write((int)wave.SampleRate);
    writer.Write((int)wave.ByteRate);
    writer.Write((short)Wave16.BlockAlign);
    writer.Write((short)Wave16.BitsPerSample);
    writer.Write(Bytes_data);
    writer.Write((int)wave.DataSize);

#pragma warning restore IDE0004

    writer.Write(new Span<byte>(wave.Data.ToPointer(), wave.DataSize));
  }
}