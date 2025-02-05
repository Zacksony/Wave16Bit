using System.Runtime.InteropServices;

namespace Wave16Bit;

public class Wave16(nint data, int dataSize, int sampleRate) :  IDisposable
{
  public const short BitsPerSample = 16;
  public const int SubChunk1Size = 16;
  public const short AudioFormat = 1;
  public const short ChannelCount = 2;
  public const short BlockAlign = 4;

  private bool isDisposed = false;

  public nint Data { get; private set; } = data;

  public int DataSize { get; } = dataSize;

  public int SampleRate { get; set; } = sampleRate;

  public int ByteRate => SampleRate * ChannelCount * BitsPerSample / 8;

  public int TotalChunkSize => 36 + DataSize;

  public void Resize(int value)
  {

  }

  public void Dispose()
  {
    Dispose(disposing: true);
    GC.SuppressFinalize(this);
  }

  private void Dispose(bool disposing)
  {
    if (!isDisposed)
    {
      if (disposing)
      {

      }

      Marshal.FreeCoTaskMem(Data);
      Data = 0;
      isDisposed = true;
    }
  }

  ~Wave16()
  {
    Dispose(disposing: false);
  }  
}
