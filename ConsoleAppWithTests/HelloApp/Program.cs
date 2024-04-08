namespace HelloApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            var console = new StdConsole();
            Run(console);
        }

        public static void Run(IConsole console)
        {
            console.WriteLine("Enter your name:");   
            var name = console.ReadLine();
            console.WriteLine($"Hello, {name}!");
        }
    }
}