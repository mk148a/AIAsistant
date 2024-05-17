using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;


namespace AIAsistant.NLPModel
{
    class SoundRecorder
    {
        private string filePath;

        public SoundRecorder(string filePath)
        {
            this.filePath = filePath;
        }

        // Ses kaydını başlatan yöntem
        public void StartRecording()
        {
            Console.WriteLine("Kayıt başlatıldı. İsminizi söyleyin.");
            // Burada kayıt işleminin gerçekleştirilmesi gerekir, bu örnek için bir ses dosyası oluşturulacak
        }

        // Ses kaydını durduran yöntem
        public void StopRecording()
        {
            Console.WriteLine("Kayıt tamamlandı.");
            // Burada kayıt işleminin durdurulması gerekir, bu örnek için bir ses dosyası oluşturulacak
            // Bu kısımda filePath değişkenindeki yere kaydedilen sesin dosyasının oluşturulması işlemi yapılacak
        }

        // Ses dosyasını oynatmak için bir yöntem (isteğe bağlı)
        public void PlaySound(string filePath)
        {
            using (SoundPlayer player = new SoundPlayer(filePath))
            {
                player.PlaySync();
            }
        }

        // Ses kaydını etiketleyen yöntem
        public void LabelRecording(string label)
        {
            // Ses kaydına etiket eklemek için gerekli işlemleri gerçekleştirin
            // Örneğin, kaydedilen ses dosyasının adını değiştirin ve etiketi ekleyin
            string labeledFilePath =
                Path.Combine(Path.GetDirectoryName(filePath), $"{label}_{Path.GetFileName(filePath)}");
            File.Move(filePath, labeledFilePath); // Ses dosyasının adını değiştirme işlemi
            Console.WriteLine($"Kaydedilen ses dosyasına \"{label}\" etiketi eklendi.");
        }
    }
}
