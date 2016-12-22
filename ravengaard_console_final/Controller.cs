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
        private RingTypeRepository ringTypeRepo = new RingTypeRepository();
        private RockRepository rockRepo = new RockRepository();
        private ColorRepository colorRepo = new ColorRepository();
        private PendantRepository pendantRepo = new PendantRepository();
        private ChainRepository chainRepo = new ChainRepository();


        public void InitializeAllRepositories()
        {
            GetClientRepositoryFromDb();
            GetRingTypeRepositoryFromDb();
            GetRockRepositoryFromDb();
            GetColorRepositoryFromDb();
            GetPendantRepositoryFromDb();
            GetChainRepositoryFromDb();
        }

        public void GetClientRepositoryFromDb()
        {
            cliRepo.Clear();
        }

        public void GetRingTypeRepositoryFromDb()
        {
            ringTypeRepo.Clear();
            ringTypeRepo.Create("Teste");
        }

        public void GetRockRepositoryFromDb()
        {
            rockRepo.Clear();
            rockRepo.Create("Teste");
        }

        public void GetColorRepositoryFromDb()
        {
            colorRepo.Clear();
            colorRepo.Create("Yello");
        }

        public void GetPendantRepositoryFromDb()
        {
            pendantRepo.Clear();
            pendantRepo.Create("pendanti", 1.2f, 3.4f);
        }

        public void GetChainRepositoryFromDb()
        {
            chainRepo.Clear();
            chainRepo.Create("chainz", 1.2f, 3.4f);
        }

        public bool LoginProcedure()
        {
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
                    default:
                        if(isExitOption(option))
                        {
                            Ui.Clear();
                            Ui.WriteL("Thanks for using our system!");
                            Ui.Wait();
                        }
                        else
                        {
                            Ui.ShowInvalidOptionError(option);
                        }
                        break;
                }
            } while (isLoggedIn == false && isExitOption(option) == false);

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
                if(username != "exit")
                {
                    Ui.WriteL("Password: ");
                    password = Console.ReadLine();

                    if (Db.isUsernamePasswordCorrect(username, password))
                    {
                        isLoggedIn = true;
                    }
                    else
                    {
                        Ui.WriteL("Email/Password combination is wrong, please try again!");
                        Ui.Wait();
                    }
                }
            } while (isLoggedIn == false && isExitOption(username) == false);

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

            Client newClient = cliRepo.CreateClient(firstName, lastName, phone, address, email, password);
            bool inserted = Db.InsertClientIntoDb(newClient);

            GetClientRepositoryFromDb();

            if (inserted == false)
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
                Ui.ShowMainMenu();
                option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        RingDesigner();
                        break;
                    case "2":
                        NecklaceDesigner();
                        break;
                    case "8":
                        //Checkout();
                        break;
                    default:
                        if (isExitOption(option))
                        {
                            Ui.Clear();
                            Ui.WriteL("Thanks for using our system!");
                            isMenuRunning = false;
                            Ui.Wait();
                        }
                        else
                        {
                            Ui.ShowInvalidOptionError(option);
                        }
                        break;
                }

            } while (isMenuRunning == false && isExitOption(option) == false);

            return isMenuRunning;
        }

        private void RingDesigner()
        {
            bool isDesigning = true;

            int ringType = 0;
            int rock = 0;
            int color = 0;

            string option = "";
            do
            {
                Ui.ShowRingDesignerMenu();
                option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        ringType = RingType();
                        break;
                    case "2":
                        rock = Rock();
                        break;
                    case "3":
                        color = Color();
                        break;
                    case "9":
                        isDesigning = false;
                        break;
                    default:
                        Ui.ShowInvalidOptionError(option);
                        break;
                }
            } while (isDesigning == true && isExitOption(option) == false);

        }

        private void NecklaceDesigner()
        {
            bool isDesigning = true;

            int pendant = 0;
            int chain = 0;
            int color = 0;

            string option = "";
            do
            {
                Ui.ShowNecklaceDesignerMenu();
                option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        pendant = Pendant();
                        break;
                    case "2":
                        chain = Chain();
                        break;
                    case "3":
                        color = Color();
                        break;
                    case "9":
                        isDesigning = false;
                        break;
                    default:
                        Ui.ShowInvalidOptionError(option);
                        break;
                }
            } while (isDesigning == true && isExitOption(option) == false);

        }

        private int Pendant()
        {
            Dictionary<int, string> chainProducts = chainRepo.idAndNameOfProducts();

            string option = "";
            bool isChoosingProduct = true;
            int optionInt = 0;

            do
            {
                Ui.ShowListOfOptions(chainProducts);

                option = Console.ReadLine();

                switch (option)
                {
                    case "9":
                        isChoosingProduct = false;
                        break;
                    default:
                        try
                        {
                            optionInt = chosenOptionExists(option, chainProducts);
                            isChoosingProduct = false;
                        }
                        catch
                        {
                            Ui.ShowInvalidOptionError(option);
                            Ui.Clear();
                        }
                        break;
                }

            } while (isChoosingProduct == true && isExitOption(option) == false);

            return optionInt;
        }

        private int Chain()
        {
            Dictionary<int, string> pendantProducts = pendantRepo.idAndNameOfProducts();

            string option = "";
            bool isChoosingProduct = true;
            int optionInt = 0;

            do
            {
                Ui.ShowListOfOptions(pendantProducts);

                option = Console.ReadLine();

                switch (option)
                {
                    case "9":
                        isChoosingProduct = false;
                        break;
                    default:
                        try
                        {
                            optionInt = chosenOptionExists(option, pendantProducts);
                            isChoosingProduct = false;
                        }
                        catch
                        {
                            Ui.ShowInvalidOptionError(option);
                            Ui.Clear();
                        }
                        break;
                }

            } while (isChoosingProduct == true && isExitOption(option) == false);

            return optionInt;
        }

        private int RingType()
        {
            Dictionary<int, string> ringTypeProducts = ringTypeRepo.idAndNameOfProducts();
            
            string option = "";
            bool isChoosingProduct = true;
            int optionInt = 0;

            do
            {
                Ui.ShowListOfOptions(ringTypeProducts);

                option = Console.ReadLine();

                switch (option)
                {
                    case "9":
                        isChoosingProduct = false;
                        break;
                    default:
                        try
                        {
                            optionInt = chosenOptionExists(option, ringTypeProducts);
                            isChoosingProduct = false;
                        }
                        catch
                        {
                            Ui.ShowInvalidOptionError(option);
                            Ui.Clear();
                        }
                        break;
                }

            } while (isChoosingProduct == true && isExitOption(option) == false);

            return optionInt;
        }

        private int Rock()
        {
            Dictionary<int, string> rockProducts = rockRepo.idAndNameOfProducts();

            string option = "";
            bool isChoosingProduct = true;
            int optionInt = 0;

            do
            {
                Ui.ShowListOfOptions(rockProducts);

                option = Console.ReadLine();

                switch (option)
                {
                    case "9":
                        isChoosingProduct = false;
                        break;
                    default:
                        try
                        {
                            optionInt = chosenOptionExists(option, rockProducts);
                            isChoosingProduct = false;
                        }
                        catch
                        {
                            Ui.ShowInvalidOptionError(option);
                            Ui.Clear();
                        }
                        break;
                }

            } while (isChoosingProduct == true && isExitOption(option) == false);

            return optionInt;
        }

        private int Color()
        {
            Dictionary<int, string> colorProducts = colorRepo.idAndNameOfProducts();

            string option = "";
            bool isChoosingProduct = true;
            int optionInt = 0;

            do
            {
                Ui.ShowListOfOptions(colorProducts);

                option = Console.ReadLine();

                switch (option)
                {
                    case "9":
                        isChoosingProduct = false;
                        break;
                    default:
                        try
                        {
                            optionInt = chosenOptionExists(option, colorProducts);
                            isChoosingProduct = false;
                        }
                        catch
                        {
                            Ui.ShowInvalidOptionError(option);
                            Ui.Clear();
                        }
                        break;
                }

            } while (isChoosingProduct == true && isExitOption(option) == false);

            return optionInt;
        }


        private int chosenOptionExists(string option, Dictionary<int, string> dictionaryOfProducts)
        {
            int optionInt = 0;
            try
            {
                 optionInt = Convert.ToInt32(option);

                if (dictionaryOfProducts.ContainsKey(optionInt) == false)
                {
                    throw new Exception("Product does not exist");
                }
            }
            catch
            {
                throw new Exception("Not a valid option");
            }

            return optionInt;
        }
        private bool isExitOption(string option)
        {
            bool isExitOp = false;

            if (option == "9" || option == "exit")
            {
                isExitOp = true;
            }

            return isExitOp;
        }
    }
}
