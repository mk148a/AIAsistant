using AIAsistant.NLPModel;
using CSCore.Codecs.WAV;
using CSCore.SoundIn;
using Microsoft.CognitiveServices.Speech;
using Tensorflow.NumPy;
using Tensorflow;
using NumSharp;

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

            _waveWriter = new WaveWriter(outputFilePath, _soundIn.WaveFormat);

            _soundIn.DataAvailable += (s, a) =>
            {
                _waveWriter.Write(a.Data, a.Offset, a.ByteCount);
            };

            _soundIn.Start();
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
            var graph = new Graph().as_default();
            var session = new Session(graph);

            // Modeli yükle
            var model = File.ReadAllBytes(modelPath);
            graph.Import(model);

            // Ses dosyasını oku
            string audioFilePath = textBox1.Text;
            var audioBytes = File.ReadAllBytes(audioFilePath);

            // Tensor oluşturma (örnek tensör, ses verinizi uygun şekilde işleyin)
            var audioTensor = NumSharp.np.array(audioBytes).reshape(1, audioBytes.Length);

            // Modelin giriş ve çıkış tensörlerini alın
            var inputOperation = graph.OperationByName("input");
            var outputOperation = graph.OperationByName("output");

            // Tahmin yapma
            var runner = session.GetRunner();
            runner.AddInput(inputOperation, audioTensor);
            runner.Fetch(outputOperation);

            var result = runner.Run();
            var predictedLabel = NumSharp.np.argmax(result[0]);

            MessageBox.Show($"Predicted label: {predictedLabel}");
        }
    }
}
