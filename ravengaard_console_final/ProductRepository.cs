using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ravengaard_console_final
{
    public class ProductRepository
    {
        private Dictionary<int, Product> dictionaryProduct = new Dictionary<int, Product>();

        public Product CreateRing(int id, int rock, int ringType, int color, bool createdNow)
        {
            Product ring = new Product(id, 1, rock, ringType,0,0, color, createdNow);
            dictionaryProduct.Add(id, ring);

            return ring;
        }
        

        public Product CreateNecklace(int id, int chain, int pendant, int color, bool createdNow)
        {
            Product necklace = new Product(id, 2,0,0, chain, pendant, color,createdNow);
            dictionaryProduct.Add(id, necklace);

            return necklace;
        } 

        public void Clear()
        {
            dictionaryProduct.Clear();
        }

        public int NextId()
        {
            int greatestId = 0;
            foreach(int id in dictionaryProduct.Keys)
            {
                if(id>greatestId)
                {
                    greatestId = id;
                }
            }
            return greatestId+1;
        }

        public Dictionary<int,Product> getAllProductsCreatedThisSession()
        {
            Dictionary<int, Product> createdNow = new Dictionary<int, Product>();
            foreach (KeyValuePair<int,Product> prod in dictionaryProduct)
            {
                if(prod.Value.InsertedInThisRun == true)
                {
                    createdNow.Add(prod.Key, prod.Value);
                }
            }
            return createdNow;
        }

    }
}
