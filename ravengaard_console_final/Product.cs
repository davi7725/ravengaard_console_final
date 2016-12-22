using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ravengaard_console_final
{
    public class Product
    {
        public int ProductId { get; set; }
        public int ProductType { get; set; }
        public int Rock { get; set; }
        public int Chain { get; set; }
        public int Pendant { get; set; }
        public int RingType { get; set; }
        public int Color { get; set; }
        public bool InsertedInThisRun { get; set; }

        public Product(int id, int productType, int rock, int ringType, int chain, int pendant, int color, bool insertedInThisRun)
        {
            if(productType == 1)
            {
                ProductId = id;
                ProductType = productType;
                RingType = ringType;
                Rock = rock;
                Color = color;
                InsertedInThisRun = insertedInThisRun;
            }
            else if(productType == 2)
            {
                ProductId = id;
                ProductType = productType;
                Chain = chain;
                Pendant = pendant;
                Color = color;
                InsertedInThisRun = insertedInThisRun;
            }
        }
    }
}
