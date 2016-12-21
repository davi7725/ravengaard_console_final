using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ravengaard_console_final
{
    public class RockRepository
    {
        Dictionary<int, Rock> dictionaryOfRock = new Dictionary<int, Rock>();

        public Dictionary<int, string> idAndNameOfProducts()
        {
            Dictionary<int, string> idAndNameOfRocks = new Dictionary<int, string>();

            foreach (Rock rock in dictionaryOfRock.Values)
            {

                idAndNameOfRocks.Add(rock.RockId, rock.Name);
            }

            return idAndNameOfRocks;
        }

        public void Clear()
        {
            dictionaryOfRock.Clear();
        }

        public Rock Create(string name)
        {
            Rock rock = new Rock(name);
            rock.RockId = 1;

            dictionaryOfRock.Add(1, rock);

            return rock;
        }
    }
}
