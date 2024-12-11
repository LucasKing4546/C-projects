using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Settings
    {
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static string direction { get; set; }
        public Settings()
        {
            Width = 15;
            Height = 15;
            direction = "down";
        }
    }
}
