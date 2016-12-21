using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ravengaard_console_final
{
    public class Pendant
    {
        public int PendantId { get; set; }
        public string Name { get; set; }
        public float Height { get; set; }
        public float Width { get; set; }

        public Pendant(string name, float height, float width)
        {
            Name = name;
            Height = height;
            Width = width;
        }
    }
}
