using NAudio.Wave;
using System.Threading;

namespace Extensions
{
    static class AudioManager
    {
        public const string Sent
            = @"C:\Users\MacWin\Source\Repos\AzureMessaging\Media\Sound\Sent.mp3";

        public static void Play(string file)
        {
            try
            {
                using (var audioFile = new AudioFileReader(file))
                using (var outputDevice = new WaveOutEvent())
                {
                    outputDevice.Init(audioFile);
                    outputDevice.Play();
                    while (outputDevice.PlaybackState == PlaybackState.Playing)
                    {
                        Thread.Sleep(10);
                    }
                }
            }
            catch { }
        }
    }
}
