using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class Form1Tests
{
    [TestMethod]
    public void MonitorDeviceConnection_SelectedDeviceDisconnected_VolumeMuted()
    {
        // Arrange
        var form = new Form1();
        form.muteMeActive = true;
        form.selectedDevice = new BluetoothDevice { State = DeviceState.Disconnected };
        var defaultDevice = new AudioEndpointDevice();
        var audioEndpointVolume = new AudioEndpointVolume();
        defaultDevice.AudioEndpointVolume = audioEndpointVolume;
        form.controller = new AudioController();
        form.controller.GetDefaultAudioEndpoint(DataFlow.Render, Role.Console).Returns(defaultDevice);

        // Act
        form.MonitorDeviceConnection();

        // Assert
        Assert.IsTrue(audioEndpointVolume.Mute);
        Assert.AreEqual($"The selected Bluetooth device {form.selectedDevice.FriendlyName} is disconnected. System volume is muted.", form.label1.Text);
        Assert.AreEqual(Color.Red, form.button1.BackColor);
        Assert.AreEqual("Device Disconnected", form.button1.Text);
    }
}