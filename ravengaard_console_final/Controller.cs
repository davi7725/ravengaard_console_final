using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ravengaard_console_final
{
    public class Controller
    {

        public bool isLoggedIn { get; set; }

        private ClientRepository cliRepo = new ClientRepository();


        public void InitializeAllRepositories()
        {
            GetClientRepositoryFromDb();
        }

        public void GetClientRepositoryFromDb()
        {
            cliRepo.Clear();
        }
        public bool LoginProcedure()
        {
            string option = "";
            do
            {
                Ui.ShowLoginMenu();
                option = Console.ReadLine();
                switch(option)
                {
                    case "1":
                        DoLogin();
                        break;
                    case "2":
                        DoRegister();
                        break;
                    case "9":
                        Ui.Clear();
                        Ui.WriteL("Thanks for using our system!");
                        Ui.Wait();
                        break;
                    default:
                        Ui.ShowInvalidOptionError();
                        break;
                }

            } while (isLoggedIn == false && isExitOption(option) == false) ;

            return isLoggedIn;
        }

        public void DoLogin()
        {
            string username = "";
            string password = "";

            do
            {
                Ui.Clear();
                Ui.WriteL("If you want to cancel write \"exit\" as username!");
                Ui.WriteL("Email: ");
                username = Console.ReadLine();
                Ui.WriteL("Password: ");
                password = Console.ReadLine();

                if(username == "a" && password == "a")
                {
                    isLoggedIn = true;
                }
                else
                {
                    Ui.WriteL("Email/Password combination is wrong, please try again!");
                    Ui.Wait();
                }
            } while (isLoggedIn == false || isExitOption(username));

        }

        public void DoRegister()
        {
            string firstName = "";
            string lastName = "";
            string phone = "";
            string address = "";
            string password = "";
            string email = "";

            Ui.Clear();

            Ui.WriteL("First name: ");
            firstName = Console.ReadLine();
            Ui.WriteL("Last name: ");
            lastName = Console.ReadLine();
            Ui.WriteL("Phone: ");
            phone = Console.ReadLine();
            Ui.WriteL("Address: ");
            address = Console.ReadLine();
            Ui.WriteL("Password: ");
            password = Console.ReadLine();
            Ui.WriteL("Email: ");
            email = Console.ReadLine();

            Client newClient = cliRepo.CreateClient(firstName, lastName, phone, address, email);
            bool inserted = Db.InsertClientIntoDb(newClient);

            GetClientRepositoryFromDb();

            if(inserted == false)
            {
                Ui.WriteL("Could not register user to DB, please try again later");
                Ui.Wait();
            }
        }

        public bool MainMenuProcedure()
        {
            bool isMenuRunning = true;

            string option = "";
            do
            {
                Ui.ShowLoginMenu();
                option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        DoLogin();
                        break;
                    case "2":
                        DoRegister();
                        break;
                    case "9":
                        Ui.Clear();
                        Ui.WriteL("Thanks for using our system!");
                        Ui.Wait();
                        break;
                    default:
                        Ui.ShowInvalidOptionError();
                        break;
                }

            } while (isMenuRunning == false && isExitOption(option) == false);

            return isMenuRunning;
        }

        private bool isExitOption(string option)
        {
            bool isExitOp = false;

            if(option == "9" || option == "exit")
            {
                isExitOp = true;
            }

            return isExitOp;
        }
    }
}
