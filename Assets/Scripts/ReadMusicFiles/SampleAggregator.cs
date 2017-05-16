using System;
using NAudio.Dsp;
using NaudioSource;

public class SampleAggregator
{
    private Complex[] channelDataLeft;
    private Complex[] channelDataRight;
    private int bufferSize;
    private int binaryExponentitation;
    private int channelDataPosition;

    public SampleAggregator(int Size)
    {
        bufferSize = Size * 2;
        binaryExponentitation = (int)Math.Log(bufferSize, 2);
        channelDataLeft = new Complex[bufferSize];
        channelDataRight = new Complex[bufferSize];
    }

    /// <summary>
    /// Add a sample value to the aggregator.
    /// </summary>
    /// <param name="value">The value of the sample.</param>
    public void Add(float leftValue, float rightValue)
    {
        channelDataLeft[channelDataPosition].X = leftValue;
        channelDataLeft[channelDataPosition].Y = 0;

        channelDataRight[channelDataPosition].X = rightValue;
        channelDataRight[channelDataPosition].Y = 0;
        channelDataPosition++;
        if (channelDataPosition >= channelDataLeft.Length)
        {
            channelDataPosition = 0;
        }
    }

    /// <summary>
    /// Performs an FFT calculation on the channel data upon request.
    /// </summary>
    /// <param name="fftBuffer">A buffer where the FFT data will be stored.</param>
    public void GetFFTResults(float[] fftBuffer, int channel, FFTWindow window)
    {
        Complex[] channelDataClone = new Complex[bufferSize];
        if (channel == 0)
            channelDataLeft.CopyTo(channelDataClone, 0);
        else
            channelDataRight.CopyTo(channelDataClone, 0);

        FFTWindowChange(channelDataClone, window);

        for (int i = 0; i < channelDataClone.Length / 2; i++)
        {
            fftBuffer[i] = (float)Math.Sqrt(channelDataClone[i].X * channelDataClone[i].X + channelDataClone[i].Y * channelDataClone[i].Y) / 100f;
            fftBuffer[i] = fftBuffer[i] > 1.0f ? 1.0f : fftBuffer[i];
        }
    }

    private void FFTWindowChange(Complex[] channelData, FFTWindow window)
    {
        switch (window)
        {
            case FFTWindow.HannWindow:
                for (int i = 0; i < channelData.Length; i++)
                {
                    channelData[i].X = (float)(channelData[i].X * FastFourierTransform.HannWindow(i, bufferSize));
                }
                break;

            case FFTWindow.HammingWindow:
                for (int i = 0; i < channelData.Length; i++)
                {
                    channelData[i].X = (float)(channelData[i].X * FastFourierTransform.HammingWindow(i, bufferSize));
                }
                break;

            case FFTWindow.BlackmannHarrisWindow:
                for (int i = 0; i < channelData.Length; i++)
                {
                    channelData[i].X = (float)(channelData[i].X * FastFourierTransform.BlackmannHarrisWindow(i, bufferSize));
                }
                break;
        }
        FastFourierTransform.FFT(false, binaryExponentitation, channelData);
    }
}