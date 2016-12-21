using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ravengaard_console_final
{
    public class PendantRepository
    {
        Dictionary<int, Pendant> dictionaryOfPendant = new Dictionary<int, Pendant>();

        public Dictionary<int, string> idAndNameOfProducts()
        {
            Dictionary<int, string> idAndNameOfPendants = new Dictionary<int, string>();

            foreach (Pendant pendant in dictionaryOfPendant.Values)
            {

                idAndNameOfPendants.Add(pendant.PendantId, pendant.Name + " | " + pendant.Height + " | " + pendant.Width);
            }

            return idAndNameOfPendants;
        }

        public void Clear()
        {
            dictionaryOfPendant.Clear();
        }

        public Pendant Create(string name, float height, float width)
        {
            Pendant pendant = new Pendant(name,height,width);
            pendant.PendantId = 1;

            dictionaryOfPendant.Add(1, pendant);

            return pendant;
        }
    }
}
