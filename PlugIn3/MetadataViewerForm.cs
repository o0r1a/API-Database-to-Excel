using System;
using System.Drawing;
using System.Windows.Forms;

namespace PlugIn3
{
    public class MetadataViewerForm : Form
    {
        private RichTextBox richTextBox1;
        private Label label1;
        private ProgressBar progressBar;

        public MetadataViewerForm(string content, string filePath)
        {
            this.Text = "EB Metadata Viewer";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            //RichTextBox
            richTextBox1 = new RichTextBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Consolas", 10),
                ReadOnly = true,
                WordWrap = false,
                Text = content
            };

            //Label
            label1 = new Label
            {
                Text = $"Saved at: {filePath}",
                Dock = DockStyle.Bottom,
                Height = 30,
                Font = new Font("Segoe UI", 9, FontStyle.Italic),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(5)
            };

            //ProgressBar 
            progressBar = new ProgressBar
            {
                Style = ProgressBarStyle.Marquee,
                Dock = DockStyle.Top,
                Height = 20,
                MarqueeAnimationSpeed = 30
            };

            // Controls
            Controls.Add(richTextBox1);
            Controls.Add(label1);
            Controls.Add(progressBar);

           
            this.Load += (s, e) => progressBar.Visible = false;
        }
    }
}
