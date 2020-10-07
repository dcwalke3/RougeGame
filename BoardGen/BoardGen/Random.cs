using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BrandonPlayerGen
{
    class Random
    {
        static public String Choice(String[] list)
        {
            int RandomNum = StaticRandom.Instance.Next(0, list.Length - 1);
            return list[RandomNum];
        }

        static public int randInt(int min, int max)
        {
            return StaticRandom.Instance.Next(min, max);
        }
    }
}
