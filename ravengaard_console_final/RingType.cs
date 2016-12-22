using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ravengaard_console_final
{
    public class RingType
    {
        public int RingTypeId { get; set; }
        public string Name { get; set; }

        public RingType(int id, string name)
        {
            RingTypeId = id;
            Name = name;
        }
    }
}
