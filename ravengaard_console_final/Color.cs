using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ravengaard_console_final
{
    public class Color
    {
        public int ColorId { get; set; }
        public string Name { get; set; }

        public Color(string name)
        {
            Name = name;
        }
    }
}
