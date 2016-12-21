using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ravengaard_console_final
{
    public class Ui
    {
        static public void ShowLoginMenu()
        {
            Clear();
            Console.WriteLine("New to Ravengaard or regular customer?");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Create Account");
            Console.WriteLine("9. Exit");
        }

        static public void ShowInvalidOptionError()
        {
            Console.WriteLine("Invalid option, please choose again!");
            Wait();
        }

        static public void WriteL(string text)
        {
            Console.WriteLine(text);
        }

        static public void Clear()
        {
            Console.Clear();
        }

        static public void Wait()
        {
            Console.ReadKey();
        }
    }
}
