using AIAsistant.NLPModel;
using CSCore;
using CSCore.Codecs.WAV;
using CSCore.SoundIn;
using Microsoft.CognitiveServices.Speech;
using Newtonsoft.Json;
using NumSharp;
using NumSharp.Generic;
using TensorFlow;

namespace AIAsistant
{
    public partial class Form1 : Form
    {
        private WasapiCapture _soundIn;
        private WaveWriter _waveWriter;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnStartRecording_Click(object sender, EventArgs e)
        {
            string outputFilePath = Path.Combine("SoundRecords", $"recording_{DateTime.Now:yyyyMMddHHmmss}.wav");
            Directory.CreateDirectory("SoundRecords");

            _soundIn = new WasapiCapture();
            _soundIn.Initialize();
            string label = textBox1.Text; // txtLabel, kullanıcının girdiği etiketi içeren bir TextBox öğesidir

            WaveFormat waveFormat = new WaveFormat(16000, 16, 1,AudioEncoding.Pcm); // Örnek sıklığı: 16 kHz, bit derinliği: 16 bit, kanal sayısı: 1 (mono)
        
      
            _waveWriter = new WaveWriter(outputFilePath, waveFormat);

            _soundIn.DataAvailable += (s, a) =>
            {
                _waveWriter.Write(a.Data, a.Offset, a.ByteCount);
            };

            _soundIn.Start();

            // Etiket bilgisini ses dosyasının adıyla eşleştirerek bir JSON dosyası oluşturun
            string jsonFilePath = Path.Combine("SoundRecords", "labels.json");
            if (!File.Exists(jsonFilePath))
            {
                var labels = new Dictionary<string, string>();
                labels.Add(label, Path.GetFileName(outputFilePath));

                string json = JsonConvert.SerializeObject(labels);
                File.WriteAllText(jsonFilePath, json);
            }
            else
            {
                var labels = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(jsonFilePath));
                labels.Add(label, Path.GetFileName(outputFilePath));

                string json = JsonConvert.SerializeObject(labels);
                File.WriteAllText(jsonFilePath, json);
            }
        }

        private void btnStopRecording_Click(object sender, EventArgs e)
        {
            _soundIn.Stop();
            _waveWriter.Dispose();
            _soundIn.Dispose();
        }

        private void btnPredict_Click(object sender, EventArgs e)
        {

            string modelPath = "path_to_your_model/model.pb"; // Model dosya yolunu belirtin

            // TensorFlow oturumu ve grafiği oluştur
            var graph = new TensorFlow.TFGraph();
            var session = new TensorFlow.TFSession(graph);

            // Modeli yükle
            var model = File.ReadAllBytes(modelPath);
            graph.Import(model);

            // Ses dosyasını oku
            string audioFilePath = textBox1.Text;
            var audioBytes = File.ReadAllBytes(audioFilePath);

            // Tensor oluşturma (örnek tensör, ses verinizi uygun şekilde işleyin)
            var audioTensorV = NumSharp.np.array(audioBytes).reshape(1, audioBytes.Length);
            TFTensor audioTensor = new TFTensor(audioTensorV.ToArray<byte>());
            // Modelin giriş ve çıkış tensörlerini alın
            // var inputOperation = graph.GetOperationByName("input");
            // var outputOperation = graph.GetOperationByName("output");

            // Tahmin yapma
            var runner = session.GetRunner();
            runner.AddInput("input", audioTensor);
            runner.Fetch("output");

            var result = runner.Run();

            var resultArray = result[0].GetValue() as float[,];
            var resultNdArray = np.array(resultArray);

            // En büyük öğenin indeksini bulma
            var predictedLabel = NumSharp.np.argmax(resultNdArray);

            MessageBox.Show($"Predicted label: {predictedLabel}");
        }
    }
}
