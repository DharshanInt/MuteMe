using NAudio.CoreAudioApi;


namespace MuteMEe
{
    public partial class Form1 : Form
    {
        MMDeviceEnumerator controller;
        MMDeviceCollection devices;
        MMDevice selectedDevice = null;
        bool muteMeActive = false;

        public Form1()
        {
            InitializeComponent();
            controller = new MMDeviceEnumerator();
            devices = controller.EnumerateAudioEndPoints(DataFlow.All, DeviceState.Active);

            foreach (var device in devices)
            {
                comboBox1.Items.Add(device.FriendlyName);
            }


            // optional: start monitoring immediately
            Thread thread = new Thread(MonitorDeviceConnection);
            thread.Start();
        }
        /// <summary>
        /// Monitors the connection status of the selected Bluetooth device and mutes the system volume if the device is disconnected.
        /// </summary>
        private void MonitorDeviceConnection()
        {
            while (true)
            {
                if (muteMeActive)
                {
                    if (selectedDevice != null && selectedDevice.State != DeviceState.Active)
                    {
                        var defaultDevice = controller.GetDefaultAudioEndpoint(DataFlow.Render, Role.Console);
                        defaultDevice.AudioEndpointVolume.Mute = true;

                        Invoke(new Action(() => label1.Text = $"The selected Bluetooth device {selectedDevice.FriendlyName} is disconnected. System volume is muted."));

                        button1.BackColor = Color.Red;
                        button1.Text = "Device Disconnected";
                    }


                    // check again after 10 seconds
                    Thread.Sleep(10 * 1000);
                }

            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedDevice = devices.First(d => d.FriendlyName == comboBox1.SelectedItem.ToString());

        }
        /// <summary>
        /// Event handler for the button1 click event.
        /// </summary>
        /// <param name="sender">The object that triggered the event.</param>
        /// <param name="e">The event arguments.</param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (Authenticate("mySecret"))
            {
                // Access the database or perform some action
            }
            else
            {
                // Authentication failed
            }
            if (muteMeActive == false && selectedDevice != null && selectedDevice.State.Equals(DeviceState.Active))
                muteMeActive = true;
            else
                muteMeActive = false;


            if (muteMeActive)
            {
                button1.BackColor = Color.Green;
                button1.Text = "Monitoring";
                Invoke(new Action(() => label1.Text = $"The selected Bluetooth device {selectedDevice?.FriendlyName} is Moniterd."));
            }
            else
            {
                button1.BackColor = Color.Yellow;
                button1.Text = "Paused";
                Invoke(new Action(() => label1.Text = $"Monitor Mode Stopped!"));
            }


        }

























        private bool Authenticate(string secret)
        {
            // This is a placeholder for actual authentication logic.
            // In a real-world application, NEVER hard-code secrets or passwords.
            // Always retrieve them from secure storage or environment variables.
            string hardcodedSecret = Environment.GetEnvironmentVariable("MY_SECRET") ?? "";


            if (secret == hardcodedSecret)
            {
                return true;
            }

            return false;
        }

    }
}