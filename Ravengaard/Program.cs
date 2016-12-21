using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ravengaard_console_final;

namespace Ravengaard
{
    class Program
    {
        Controller ctrl = new Controller();
        static void Main(string[] args)
        {
            Program myProgram = new Program();
            myProgram.Run();

        }

        private void Run()
        {
            bool programIsRunning = true;

            ctrl.InitializeAllRepositories();
            programIsRunning = ctrl.LoginProcedure();

            while (programIsRunning == true)
            {
                programIsRunning = ctrl.MainMenuProcedure();
            }
        }
    }
}
