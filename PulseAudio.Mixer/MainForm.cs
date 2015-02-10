using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using PulseAudio.Client;

namespace PulseAudio.Mixer
{
    public partial class MainForm : Form
    {
        private IContext _context;
        private readonly string _server;

        public MainForm(string server)
        {
            InitializeComponent();
            _server = server;
        }

        public async Task Reload()
        {
            var firstSink = (await _context.Sinks.GetAll()).First();
            var inputs =
                (await _context.SinkInputs.GetAll()).Where(si => si.SinkIndex == firstSink.Index).ToArray();

            tableSliders.Controls.Clear();
            tableSliders.ColumnCount = 1 + inputs.Length;
            var sinkSlider = new VolumeSlider(firstSink) {Dock = DockStyle.Left};
            tableSliders.Controls.Add(sinkSlider);

            foreach (var input in inputs)
            {
                tableSliders.Controls.Add(new VolumeSlider(input) {Dock = DockStyle.Left});
            }
        }

        protected async override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await Connect(_server);
        }

        private async Task Connect(string server)
        {
            _context = await Pulse.CreateContext("pamixer", server);
            Enabled = true;
            Text = @"PulseAudio Mixer";
            await Reload();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            e.Cancel = true;
            Hide();
        }

        private void NotifyIconOnClick(object sender, EventArgs e)
        {
            var me = e as MouseEventArgs;
            if (me == null || me.Button == MouseButtons.Left)
            {
                RepositionToNotificationIcons();
                Show();
                Activate();
            }
        }

        private void RepositionToNotificationIcons()
        {
            var workArea = Screen.PrimaryScreen.WorkingArea;
            Left = workArea.Right - Width - 10;
            Top = workArea.Bottom - Height - 10;
        }

        private void NotifyButtonQuitOnClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ButtonQuitOnClick(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
