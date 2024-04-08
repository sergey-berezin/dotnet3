namespace HelloApp
{
    public class StdConsole : IConsole
    {
        public string ReadLine() => Console.ReadLine();

        public void WriteLine(string text) => Console.WriteLine(text);
    }
}