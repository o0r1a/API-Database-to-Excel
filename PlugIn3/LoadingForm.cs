using System;
using System.Windows.Forms;
using System.Drawing;

namespace PlugIn3
{
    public class LoadingForm : Form
    {
        private Label lbl;
        private ProgressBar bar;

        public LoadingForm()
        {
            this.Text = "Please wait...";
            this.Size = new Size(300, 100);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;

            lbl = new Label()
            {
                Text = "Collecting EB metadata...",
                Dock = DockStyle.Top,
                Height = 30,
                TextAlign = ContentAlignment.MiddleCenter
            };

            bar = new ProgressBar()
            {
                Style = ProgressBarStyle.Marquee,
                Dock = DockStyle.Bottom,
                Height = 20,
                MarqueeAnimationSpeed = 30
            };

            this.Controls.Add(lbl);
            this.Controls.Add(bar);
        }
    }
}
