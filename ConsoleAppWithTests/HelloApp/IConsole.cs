using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloApp
{
    public interface IConsole
    {
        string ReadLine();
        void WriteLine(string text);
    }
}
