using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Network.Packet.Analyzer.Core.Domain.Utils
{
    public class HashCalculator
    {
        public HashCalculator()
        {
        }

        public static int CalculateHash(string str, int port, int val,long param,int param1,int param2)
        {
            if (str == null || str == String.Empty)
                return -1;

            int ret = 0;
            int a = 127;

            char[] ch = str.ToCharArray();
            for (int i = 0; i < ch.Length; i++)
            {
                ret = ((a * ret + port + param1 + (int)ch[i]) % val);
            }

            return (ret + (int)((param +param2) & val));
        }
    }
}
