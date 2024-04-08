using HelloApp;
using Moq;
using System.Reflection.Metadata.Ecma335;

namespace Tests
{
    public class MainScenarioTests
    {
        [Fact]
        public void CustomConsole_Test()
        {
            Program.Run(new TestConsole());
        }

        [Fact]
        public void Moq_Test()
        {
            var mock = new Mock<IConsole>();
            mock.Setup(c => c.ReadLine()).Returns("World");

            Program.Run(mock.Object);

            mock.Verify(c => c.WriteLine("Enter your name:"));
            mock.Verify(c => c.ReadLine());
            mock.Verify(c => c.WriteLine("Hello, World!"));
            mock.VerifyNoOtherCalls();
        }
    }

    public class TestConsole : IConsole
    {
        public string ReadLine() => "World";

        public void WriteLine(string text) => Assert.Equal("Hello, World!", text);
    }
}