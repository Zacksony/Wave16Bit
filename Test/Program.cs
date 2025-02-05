using Wave16Bit;

namespace Test;

internal class Program
{
  static void Main(string[] args)
  {
    using Wave16 wave = Wave16IO.Load(@"D:\DESKTOP\i01_A#4.wav");

    Wave16IO.Save(wave, @"D:\DESKTOP\i01_A#4.wav");
  }
}