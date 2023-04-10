using System;
using System.IO;

namespace DIP
{
    class Program
    {
        static void Main(string[] args)
        {
            Copy("a.txt", "b.txt");

            using var src = File.OpenRead("a.txt");
            using var dst = File.OpenRead("b.txt");
            Copy(src, dst, p => Console.WriteLine($"{p}% completed"));

            using var src2 = File.OpenRead("a.txt");
            using var dst2 = File.OpenRead("b.txt");
            Copy(new StreamInput(src2), new StreamOutput(dst2), new ConsoleReporter());
        }

        static void Copy(string source, string target)
        {
            using Stream src = File.OpenRead(source);
            using Stream dst = File.OpenWrite(target);

            const int BufferSize = 1024;
            byte[] buffer = new byte[BufferSize];
            
            int count = src.Read(buffer);
            long bytesRead = 0;
            long totalLength = src.Length;
            int percentReported = -1;

            while(count > 0)
            {
                dst.Write(buffer);
                bytesRead += count;
                int p = (int)(100 * bytesRead / totalLength);
                if(p != percentReported)
                {
                    Console.WriteLine($"Percents copied {percentReported}");
                    percentReported = p;
                }
            }
        }

        static void Copy(Stream src, Stream dst, Action<int> reporter)
        {
            const int BufferSize = 1024;
            byte[] buffer = new byte[BufferSize];

            int count = src.Read(buffer);
            long bytesRead = 0;
            long totalLength = src.Length;
            int percentReported = -1;

            while (count > 0)
            {
                dst.Write(buffer);
                bytesRead += count;
                int p = (int)(100 * bytesRead / totalLength);
                if (p != percentReported)
                {
                    reporter(p);
                    percentReported = p;
                }
            }
        }

        public interface IInput
        {
            int Read(byte[] buffer);
            long Length { get; }
        }

        public interface IOutput
        {
            void Write(byte[] buffer);
        }

        public interface IReporter
        {
            void ReportProgress(int percent);
        }

        static void Copy(IInput src, IOutput dst, IReporter reporter)
        {
            const int BufferSize = 1024;
            byte[] buffer = new byte[BufferSize];

            int count = src.Read(buffer);
            long bytesRead = 0;
            long totalLength = src.Length;
            int percentReported = -1;

            while (count > 0)
            {
                dst.Write(buffer);
                bytesRead += count;
                int p = (int)(100 * bytesRead / totalLength);
                if (p != percentReported)
                {
                    reporter.ReportProgress(p);
                    percentReported = p;
                }
            }
        }

        public class StreamInput : IInput
        {
            private readonly Stream s;

            public StreamInput(Stream s)
            {
                this.s = s;
            }
            public long Length => s.Length;

            public int Read(byte[] buffer) => s.Read(buffer);
        }

        public class StreamOutput : IOutput
        {
            private readonly Stream s;

            public StreamOutput(Stream s)
            {
                this.s = s;
            }

            public void Write(byte[] buffer) => s.Write(buffer);
        }

        public class ConsoleReporter : IReporter
        {
            public void ReportProgress(int percent) => Console.WriteLine($"{percent}% completed");
        }
    }
}
