using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ravengaard_console_final
{
    public class Chain
    {
        public int ChainId { get; set; }
        public string Name { get; set; }
        public float Lenght { get; set; }
        public float Thickness { get; set; }

        public Chain(int id, string name, float lenght, float thickness)
        {
            ChainId = id;
            Name = name;
            Lenght = lenght;
            Thickness = thickness;
        }
    }
}
