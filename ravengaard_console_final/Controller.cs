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
        private Client loggedInClient = new Client();

        private ClientRepository cliRepo = new ClientRepository();
        private RingTypeRepository ringTypeRepo = new RingTypeRepository();
        private RockRepository rockRepo = new RockRepository();
        private ColorRepository colorRepo = new ColorRepository();
        private PendantRepository pendantRepo = new PendantRepository();
        private ChainRepository chainRepo = new ChainRepository();
        private ProductRepository productRepo = new ProductRepository();


        public bool InitializeAllRepositories()
        {
            bool couldInititalize = true;
            try
            {
                GetClientRepositoryFromDb();
                GetRingTypeRepositoryFromDb();
                GetRockRepositoryFromDb();
                GetColorRepositoryFromDb();
                GetPendantRepositoryFromDb();
                GetChainRepositoryFromDb();
                GetProductRepositoryFromDb();
            }
            catch
            {
                couldInititalize = false;
            }

            return couldInititalize;
        }

        public void GetClientRepositoryFromDb()
        {
            cliRepo.Clear();
            Db.GetClient(cliRepo);
        }

        public void GetRingTypeRepositoryFromDb()
        {
            ringTypeRepo.Clear();
            Db.GetRingType(ringTypeRepo);
        }

        public void GetRockRepositoryFromDb()
        {
            rockRepo.Clear();
            Db.GetRock(rockRepo);
        }

        public void GetColorRepositoryFromDb()
        {
            colorRepo.Clear();
            Db.GetColor(colorRepo);
        }

        public void GetPendantRepositoryFromDb()
        {
            pendantRepo.Clear();
            Db.GetPendant(pendantRepo);
        }

        public void GetChainRepositoryFromDb()
        {
            chainRepo.Clear();
            Db.GetChain(chainRepo);
        }
        public void GetProductRepositoryFromDb()
        {
            productRepo.Clear();
            Db.GetProduct(productRepo);
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
                username = username.ToLower();

                if(isExitOption(username) == false)
                {
                    Ui.WriteL("Password: ");
                    password = Console.ReadLine();

                    if (Db.isUsernamePasswordCorrect(username, password))
                    {
                        isLoggedIn = true;
                        loggedInClient = cliRepo.Load(username);
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

            Client newClient = cliRepo.Create(cliRepo.NextId(),firstName, lastName, phone, address, email, password);
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
                        Checkout();
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
        private void Checkout()
        {
            bool isCheckingOut = true;

            string option = "";
            do
            {
                Ui.ShowCheckoutMenu();
                option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        Dictionary<int, Product> productsFromThisSession = productRepo.getAllProductsCreatedThisSession();
                        if (productsFromThisSession.Count > 0)
                        {
                            Ui.ShowAllInsertedProducts(productRepo.getAllProductsCreatedThisSession(), ringTypeRepo, rockRepo, colorRepo, pendantRepo, chainRepo);
                        }
                        else
                        {
                            Ui.Clear();
                            Ui.WriteL("Your cart is empty!");
                            Ui.Wait();
                        }
                            break;
                    case "2":
                        if (productRepo.getAllProductsCreatedThisSession().Count > 0)
                        {
                            CheckoutItems();
                            isCheckingOut = false;
                            productRepo.Clear();
                        }
                        else
                        {
                            Ui.Clear();
                            Ui.WriteL("You don't have any products in your cart!");
                            Ui.Wait();
                        }
                        break;
                    default:
                        if (isExitOption(option)== false)
                        { 
                            Ui.ShowInvalidOptionError(option);
                        }
                        break;
                }
            } while (isCheckingOut == true && isExitOption(option) == false);

        }

        private void CheckoutItems()
        {
            Db.AddProductsToTheDb(productRepo.getAllProductsCreatedThisSession(), loggedInClient);
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
                    default:
                        if (isExitOption(option))
                        {
                            isDesigning = checkRingIsCompleted(ringType, rock, color);
                            option = "";

                            if (allFieldsAreFullfiled(ringType,rock,color) && isDesigning == false)
                            {
                                Product ring = productRepo.CreateRing(productRepo.NextId(),rock, ringType, color,true);
                            }
                        }
                        else
                        {
                            Ui.ShowInvalidOptionError(option);
                        }
                        break;
                }
            } while (isDesigning == true && isExitOption(option) == false);

        }

        private bool allFieldsAreFullfiled(int product, int product1, int product2)
        {
            bool arefullfiled = true;
            if (product == 0 || product1 == 0 || product2 == 0)
            {
                arefullfiled = false;
            }

            return arefullfiled;
        }

        private bool checkRingIsCompleted(int ringType, int rock, int color)
        {
            bool yesOrNoOption = false;
            if(allFieldsAreFullfiled(ringType,rock,color) == false)
            {
                Ui.Clear();
                Ui.WriteL("Looks like you forgot to select one of the options (Ring Type, Rock or Color), are you sure you want to exit? (Y/N)");
                Ui.WriteL("If you do, all your changes will be discarded!");
                string option = Console.ReadLine().ToLower();

                switch(option)
                {
                    case "y":
                        yesOrNoOption = false;
                        break;
                    case "n":
                        yesOrNoOption = true;
                        break;
                }
            }
            else
            {
                yesOrNoOption = false;
            }

            return yesOrNoOption;
        }

        private bool checkNecklaceIsCompleted(int chain, int pendant, int color)
        {
            bool yesOrNoOption = false;
            if (allFieldsAreFullfiled(chain, pendant, color) == false)
            {
                Ui.Clear();
                Ui.WriteL("Looks like you forgot to select one of the options (Necklace Chain, Pendant or Color), are you sure you want to exit? (Y/N)");
                Ui.WriteL("If you do, all your changes will be discarded!");
                string option = Console.ReadLine().ToLower();

                switch (option)
                {
                    case "y":
                        yesOrNoOption = false;
                        break;
                    case "n":
                        yesOrNoOption = true;
                        break;
                }
            }
            else
            {
                yesOrNoOption = false;
            }

            return yesOrNoOption;
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
                        chain = Chain();
                        break;
                    case "2":
                        pendant = Pendant();
                        break;
                    case "3":
                        color = Color();
                        break;
                    default:
                        if (isExitOption(option))
                        {
                            isDesigning = checkNecklaceIsCompleted(pendant, chain, color);
                            option = "";

                            if(isDesigning == false && allFieldsAreFullfiled(pendant,chain,color))
                            {
                                Product necklace = productRepo.CreateNecklace(productRepo.NextId(), chain,pendant,color,true);
                            }
                        }
                        else
                        {
                            Ui.ShowInvalidOptionError(option);
                        }
                        break;
                }
            } while (isDesigning == true && isExitOption(option) == false);

        }

        private int Pendant()
        {
            Dictionary<int, string> pendantProducts = pendantRepo.idAndNameOfProducts();

            string option = "";
            bool isChoosingProduct = true;
            int optionInt = 0;

            do
            {
                Ui.ShowListOfOptions(pendantProducts, "Id - Name | Height | Width");

                option = Console.ReadLine();

                if (isExitOption(option))
                {
                    isChoosingProduct = false;
                }
                else
                {
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
                }

            } while (isChoosingProduct == true && isExitOption(option) == false);

            return optionInt;
        }

        private int Chain()
        {
            Dictionary<int, string> chainProducts = chainRepo.idAndNameOfProducts();

            string option = "";
            bool isChoosingProduct = true;
            int optionInt = 0;

            do
            {
                Ui.ShowListOfOptions(chainProducts, "Id - Name | Length | Thickness");

                option = Console.ReadLine();

                if (isExitOption(option))
                        {
                            isChoosingProduct = false;
                        }
                        else
                        {
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
                Ui.ShowListOfOptions(ringTypeProducts, "Id - Name");

                option = Console.ReadLine();

                if (isExitOption(option))
                {
                    isChoosingProduct = false;
                }
                else
                {
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
                Ui.ShowListOfOptions(rockProducts, "Id - Name");

                option = Console.ReadLine();

                if (isExitOption(option))
                {
                    isChoosingProduct = false;
                }
                else
                {
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
                Ui.ShowListOfOptions(colorProducts,"Id - Name");

                option = Console.ReadLine();

                if (isExitOption(option))
                {
                    isChoosingProduct = false;
                }
                else
                {
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
            option = option.ToLower();

            if (option == "q" || option == "exit")
            {
                isExitOp = true;
            }

            return isExitOp;
        }
    }
}
