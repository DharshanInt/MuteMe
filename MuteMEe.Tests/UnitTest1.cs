using Microsoft.VisualStudio.TestTools.UnitTesting;
using MuteMEe;
using NAudio.CoreAudioApi;
namespace MuteMEe.Tests
{
    [TestClass]
    public class Form1Tests
    {
        [TestMethod]
        public void Form1_InitializesWithoutExceptions()
        {
            // Arrange & Act
            var form = new Form1();

            // Assert
            Assert.IsNotNull(form);
        }
    }

  
}