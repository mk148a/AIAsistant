namespace AIAsistant
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnPredict = new Button();
            textBox1 = new TextBox();
            btnStartRecording = new Button();
            btnStopRecording = new Button();
            SuspendLayout();
            // 
            // btnPredict
            // 
            btnPredict.Location = new Point(476, 197);
            btnPredict.Name = "btnPredict";
            btnPredict.Size = new Size(75, 23);
            btnPredict.TabIndex = 0;
            btnPredict.Text = "btnPredict";
            btnPredict.UseVisualStyleBackColor = true;
            btnPredict.Click += btnPredict_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(228, 198);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(144, 23);
            textBox1.TabIndex = 1;
            // 
            // btnStartRecording
            // 
            btnStartRecording.Location = new Point(228, 236);
            btnStartRecording.Name = "btnStartRecording";
            btnStartRecording.Size = new Size(144, 23);
            btnStartRecording.TabIndex = 2;
            btnStartRecording.Text = "Start Recording";
            btnStartRecording.UseVisualStyleBackColor = true;
            btnStartRecording.Click += btnStartRecording_Click;
            // 
            // btnStopRecording
            // 
            btnStopRecording.Location = new Point(228, 265);
            btnStopRecording.Name = "btnStopRecording";
            btnStopRecording.Size = new Size(144, 23);
            btnStopRecording.TabIndex = 2;
            btnStopRecording.Text = "Stop Recording";
            btnStopRecording.UseVisualStyleBackColor = true;
            btnStopRecording.Click += btnStopRecording_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnStopRecording);
            Controls.Add(btnStartRecording);
            Controls.Add(textBox1);
            Controls.Add(btnPredict);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnPredict;
        private TextBox textBox1;
        private Button btnStartRecording;
        private Button btnStopRecording;
    }
}
