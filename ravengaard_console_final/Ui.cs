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
            Console.WriteLine("Q. Exit");
        }

        static public void ShowMainMenu()
        {
            Clear();
            Console.WriteLine("Welcome To Ravengaard Ring Design system!");
            Console.WriteLine("What do you want to design to day?");
            Console.WriteLine("1. Ring");
            Console.WriteLine("2. Necklace");
            Console.WriteLine("8. Go To Checkout");
            Console.WriteLine("Q. Exit");
            Console.WriteLine("Select what kind of product you want to design:");
        }

        static public void ShowRingDesignerMenu()
        {
            Clear();
            Console.WriteLine("What do you want to design first?");
            Console.WriteLine("1. Ring Type");
            Console.WriteLine("2. Rock Type");
            Console.WriteLine("3. Ring Color");
            Console.WriteLine("Q. Exit");
            Console.WriteLine("Please choose an option:");
        }

        static public void ShowNecklaceDesignerMenu()
        {
            Clear();
            Console.WriteLine("What do you want to design first?");
            Console.WriteLine("1. Necklace Chain");
            Console.WriteLine("2. Necklace Pendant");
            Console.WriteLine("3. Necklace Color");
            Console.WriteLine("Q. Exit");
            Console.WriteLine("Please choose an option:");
        }

        static public void ShowInvalidOptionError(string option)
        {
            if(option != "exit")
            {
                Console.WriteLine("Invalid option, please choose again!");
                Wait();
            }
        }

        static public void ShowListOfOptions(Dictionary<int, string> dictionaryOfProducts)
        {
            Clear();
            foreach(KeyValuePair<int, string> product in dictionaryOfProducts)
            {
                WriteL(product.Key + " - " + product.Value);
            }
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
            Console.WriteLine("What do you want to design first?");
            Console.WriteLine("1. Show cart products");
            Console.WriteLine("2. Order the products");
            Console.WriteLine("Q. Exit");
            Console.WriteLine("Please choose an option:");
        }
    }
}
