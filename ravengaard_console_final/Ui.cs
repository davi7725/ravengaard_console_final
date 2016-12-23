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
            WriteL("New to Ravengaard or regular customer?");
            WriteL("1. Login");
            WriteL("2. Create Account");
            WriteL("Q. Exit");
            WriteL("Please choose an option:");
        }

        static public void ShowMainMenu()
        {
            Clear();
            WriteL("Welcome To Ravengaard Ring Design system!");
            WriteL("What do you want to design to day?");
            WriteL("1. Ring");
            WriteL("2. Necklace");
            WriteL("8. Go To Checkout");
            WriteL("Q. Exit");
            WriteL("Select what kind of product you want to design:");
        }

        static public void ShowRingDesignerMenu()
        {
            Clear();
            WriteL("After you selected the 3 options exit so the product is saved!");
            WriteL("What do you want to design first?");
            WriteL("1. Ring Type");
            WriteL("2. Rock Type");
            WriteL("3. Ring Color");
            WriteL("Q. Exit");
            WriteL("Please choose an option:");
        }

        static public void ShowNecklaceDesignerMenu()
        {
            Clear();
            WriteL("After you selected the 3 options exit so the product is saved!");
            WriteL("What do you want to design first?");
            WriteL("1. Necklace Chain");
            WriteL("2. Necklace Pendant");
            WriteL("3. Necklace Color");
            WriteL("Q. Exit");
            WriteL("Please choose an option:");
        }

        static public void ShowInvalidOptionError(string option)
        {
            if(option != "exit")
            {
                WriteL("Invalid option, please choose again!");
                Wait();
            }
        }

        static public void ShowListOfOptions(Dictionary<int, string> dictionaryOfProducts, string headerString)
        {
            Clear();
            WriteL(headerString+ "\n");
            foreach(KeyValuePair<int, string> product in dictionaryOfProducts)
            {
                WriteL(product.Key + " - " + product.Value);
            }
            WriteL("Q. Exit");
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

        internal static void ShowAllInsertedProducts(Dictionary<int, Product> dictionary, RingTypeRepository ringTypeRepo, RockRepository rockRepo, ColorRepository colorRepo, PendantRepository pendantRepo, ChainRepository chainRepo)
        {
            Clear();
            foreach(KeyValuePair<int, Product> kvp in dictionary)
            {
                if(kvp.Value.ProductType == 1)
                {
                    WriteL("*-------------------------*");
                    WriteL(ringTypeRepo.Load(kvp.Value.RingType) + " | " + rockRepo.Load(kvp.Value.Rock) + " | " + colorRepo.Load(kvp.Value.Color));
                }
                else if (kvp.Value.ProductType == 2)
                {
                    WriteL("*-------------------------*");
                    WriteL(chainRepo.Load(kvp.Value.Chain) + " | " + pendantRepo.Load(kvp.Value.Pendant) + " | " + colorRepo.Load(kvp.Value.Color));
                }
            }

            WriteL("\nPress any key to exit");
            Wait();
        }

        internal static void ShowCheckoutMenu()
        {
            Clear();
            WriteL("What do you want to design first?");
            WriteL("1. Show cart products");
            WriteL("2. Order the products");
            WriteL("Q. Exit");
            WriteL("Please choose an option:");
        }
    }
}
