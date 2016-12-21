using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ravengaard_console_final
{
    public class RingTypeRepository
    {
        Dictionary<int, RingType> dictionaryOfRingType = new Dictionary<int, RingType>();
        
        public Dictionary<int, string> idAndNameOfProducts()
        {
            Dictionary<int, string> idAndNameOfRingTypes = new Dictionary<int, string>();

            foreach (RingType ringType in dictionaryOfRingType.Values)
            {
                
                idAndNameOfRingTypes.Add(ringType.RingTypeId, ringType.Name);
            }

            return idAndNameOfRingTypes;
        }

        public void Clear()
        {
            dictionaryOfRingType.Clear();
        }

        public RingType Create(string name)
        {
            RingType ringType = new RingType(name);
            ringType.RingTypeId = 1;

            dictionaryOfRingType.Add(1,ringType);

            return ringType;
        }
    }
}
