using AForge.Video.DirectShow;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Camera_Config
{
    public partial class Form1 : Form
    {
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoDevice;
        private VideoCapabilities[] videoCapabilities;
        private VideoCapabilities[] snapshotCapabilities;

        public Form1()
        {
            InitializeComponent();

            this.videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (this.videoDevices.Count != 0)
            {
                foreach (FilterInfo videoDevice1 in (CollectionBase)this.videoDevices)
                    this.comboBox1.Items.Add((object)videoDevice1.Name);
            }
            else
                this.comboBox1.Items.Add((object)"No DirectShow devices found");
        }

        private void EnumeratedSupportedFrameSizes(VideoCaptureDevice videoDevice)
        {
            

            this.Cursor = Cursors.WaitCursor;
            this.videoResolutionsCombo.Items.Clear();
            this.snapshotResolutionsCombo.Items.Clear();
            try
            {
                this.videoCapabilities = videoDevice.VideoCapabilities;
                this.snapshotCapabilities = videoDevice.SnapshotCapabilities;
                foreach (VideoCapabilities videoCapability in this.videoCapabilities)
                    this.videoResolutionsCombo.Items.Add((object)string.Format("{0} x {1}", (object)videoCapability.FrameSize.Width, (object)videoCapability.FrameSize.Height));
                foreach (VideoCapabilities snapshotCapability in this.snapshotCapabilities)
                    this.snapshotResolutionsCombo.Items.Add((object)string.Format("{0} x {1}", (object)snapshotCapability.FrameSize.Width, (object)snapshotCapability.FrameSize.Height));
                if (this.videoCapabilities.Length == 0)
                    this.videoResolutionsCombo.Items.Add((object)"Not supported");
                if (this.snapshotCapabilities.Length == 0)
                    this.snapshotResolutionsCombo.Items.Add((object)"Not supported");
                this.videoResolutionsCombo.SelectedIndex = -1;
                this.snapshotResolutionsCombo.SelectedIndex = -1;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.videoDevices.Count == 0)
                return;
            this.videoDevice = new VideoCaptureDevice(this.videoDevices[this.comboBox1.SelectedIndex].MonikerString);
            this.EnumeratedSupportedFrameSizes(this.videoDevice);

            label1.Text = comboBox1.SelectedIndex.ToString();
        }

        private void videoResolutionsCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            label2.Text = videoResolutionsCombo.SelectedIndex.ToString();
        }

        private void snapshotResolutionsCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            label3.Text = snapshotResolutionsCombo.SelectedIndex.ToString();
        }
    }
}
