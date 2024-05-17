using AIAsistant.NLPModel;
using Microsoft.CognitiveServices.Speech;

namespace AIAsistant
{
    public partial class Form1 : Form
    {
        private SoundRecorder recorder;
        private static Dictionary<string, Func<int, int, int>> operations = new Dictionary<string, Func<int, int, int>>()
        {
            { "Topla", (a, b) => a + b },
            { "Çıkar", (a, b) => a - b },
            { "Çarp", (a, b) => a * b },
            { "Böl", (a, b) => a / b }
        };
        public Form1()
        {
            InitializeComponent();
            recorder = new SoundRecorder();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var config = SpeechConfig.FromSubscription("6a293a68d2e747ecbc162e3f8123e1df", "westeurope");
            config.SpeechRecognitionLanguage = "tr-TR";
            config.SpeechSynthesisLanguage = "tr-TR";
            var recognizer = new SpeechRecognizer(config);

            var recognitionResult = await recognizer.RecognizeOnceAsync();

            if (recognitionResult.Reason == ResultReason.RecognizedSpeech)
            {
                string command = recognitionResult.Text.Replace(".", "");
                Console.WriteLine($"Algılanan komut: {command}");

                string response = ProcessCommand(command);

                var synthesizer = new SpeechSynthesizer(config);
                await synthesizer.SpeakTextAsync(response);
            }
            else if (recognitionResult.Reason == ResultReason.NoMatch)
            {
                MessageBox.Show("No speech could be recognized.");
            }
            else if (recognitionResult.Reason == ResultReason.Canceled)
            {
                var cancellation = CancellationDetails.FromResult(recognitionResult);
                MessageBox.Show($"CANCELED: Reason={cancellation.Reason}");

                if (cancellation.Reason == CancellationReason.Error)
                {
                    MessageBox.Show($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                    MessageBox.Show($"CANCELED: ErrorDetails={cancellation.ErrorDetails}");
                    MessageBox.Show($"CANCELED: Did you update the subscription info?");
                }
            }
        }
        private static string ProcessCommand(string command)
        {
            foreach (var operation in operations)
            {
                if (command.Contains(operation.Key))
                {
                    string[] words = command.Split(' ');
                    int firstNumber = int.Parse(words[1]);
                    int secondNumber = int.Parse(words[3]);
                    int result = operation.Value(firstNumber, secondNumber);
                    return $"{firstNumber} {operation.Key} {secondNumber} = {result}";
                }
            }
            return "Geçersiz komut!";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Kayıt başlatma butonuna tıklandığında ses kaydını başlat
            string label = textBox1.Text; // TextBox'tan etiket al
            recorder.StartRecording(label);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Kayıt durdurma butonuna tıklandığında ses kaydını durdur
            recorder.StopRecording();
        }
    }
}
