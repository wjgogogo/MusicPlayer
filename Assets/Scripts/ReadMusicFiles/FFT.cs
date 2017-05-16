namespace NaudioSource
{
    public enum FFTSize
    {
        FFT64 = 64,
        FFT128 = 128,
        FFT256 = 256,
        FFT512 = 512,
        FFT1024 = 1024,
        FFT2048 = 2048,
        FFT4096 = 4096,
    }

    public enum FFTWindow
    {
        BlackmannHarrisWindow,
        HammingWindow,
        HannWindow
    }
}