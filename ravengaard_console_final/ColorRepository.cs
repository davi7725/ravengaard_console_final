using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ravengaard_console_final
{
    public class ColorRepository
    {
        Dictionary<int, Color> dictionaryOfColor = new Dictionary<int, Color>();

        public Dictionary<int, string> idAndNameOfProducts()
        {
            Dictionary<int, string> idAndNameOfColors = new Dictionary<int, string>();

            foreach (Color color in dictionaryOfColor.Values)
            {

                idAndNameOfColors.Add(color.ColorId, color.Name);
            }

            return idAndNameOfColors;
        }

        public void Clear()
        {
            dictionaryOfColor.Clear();
        }

        public Color Create(int id, string name)
        {
            Color color = new Color(id, name);

            dictionaryOfColor.Add(id, color);

            return color;
        }

        internal string Load(int color)
        {
            return dictionaryOfColor[color].Name;
        }
    }
}
