using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using CSCore;
using CSCore.SoundIn;
using CSCore.Codecs.WAV;


namespace AIAsistant.NLPModel
{

    class SoundRecorder
    {
        private WasapiCapture capture;
        private WaveWriter writer;

        public SoundRecorder()
        {
            capture = new WasapiCapture();
            capture.Initialize();

            writer = new WaveWriter("temp.wav", capture.WaveFormat);
            capture.DataAvailable += (s, e) =>
            {
                writer.Write(e.Data, e.Offset, e.ByteCount);
            };
        }

        // Ses kaydını başlatan yöntem
        public void StartRecording(string label)
        {
            // Ses dosyasını oluştur
            string filePath = $"{label}.wav";
            writer = new WaveWriter(filePath, capture.WaveFormat);

            capture.Start();
            Console.WriteLine($"Kayıt başladı. Lütfen {label} ifadesini söyleyin...");
        }

        // Ses kaydını durduran yöntem
        public void StopRecording()
        {
            capture.Stop();
            writer.Dispose();
            Console.WriteLine("Kayıt tamamlandı.");
        }
    }
}
