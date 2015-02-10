using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using PulseAudio.Client;

namespace PulseAudio.Mixer
{
    public partial class VolumeSlider : UserControl
    {
        private readonly IVolumeControllable _volumeControllable;
        private int _maxVolumePercent;

        public VolumeSlider(IVolumeControllable volumeControllable)
        {
            _volumeControllable = volumeControllable;
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            UpdateFromControllable();
            trackBar.Scroll += TrackBarOnScroll;
            numericUpDown.ValueChanged += NumericUpDownOnValueChanged;
            checkBoxMute.CheckedChanged += CheckboxMuteOnCheckChange;
        }

        private void TrackBarOnScroll(object sender, EventArgs e)
        {
            numericUpDown.Value = trackBar.Value;
        }

        private async void NumericUpDownOnValueChanged(object sender, EventArgs e)
        {
            trackBar.Value = (int) numericUpDown.Value;
            await UpdateVolume();
        }

        private async void CheckboxMuteOnCheckChange(object sender, EventArgs e)
        {
            await UpdateMuted();
        }

        private void UpdateFromControllable()
        {
            _maxVolumePercent = _volumeControllable.Volumes.IsSoftware ? 150 : 100;
            numericUpDown.Maximum = trackBar.Maximum = _maxVolumePercent;

            Enabled = !_volumeControllable.Volumes.IsReadOnly;
            labelName.Text = _volumeControllable.Name;
            toolTip.SetToolTip(labelName, _volumeControllable.Name);
            var volumeInPercent = VolumeToPercent(_volumeControllable.Volumes.Average);
            numericUpDown.Value = volumeInPercent;
            trackBar.Value = volumeInPercent;
            checkBoxMute.Checked = _volumeControllable.Muted;
        }

        private async Task UpdateMuted()
        {
            if(_volumeControllable.Volumes.IsReadOnly) return;
            await _volumeControllable.SetMuted(checkBoxMute.Checked);
        }

        private async Task UpdateVolume()
        {
            if (_volumeControllable.Volumes.IsReadOnly) return;
            _volumeControllable.Volumes.SetAllChannels(PercentToVolume((int) numericUpDown.Value));
            await _volumeControllable.Volumes.Commit();
        }

        private int VolumeToPercent(Volume volume)
        {
            var percent = (int) (volume.ToLinear()*100);
            if (percent < 0) percent = 0;
            if (percent > _maxVolumePercent) percent = _maxVolumePercent;
            return percent;
        }

        private static Volume PercentToVolume(int percent)
        {
            return Volume.FromLinear(percent/100.0);
        }
    }
}
