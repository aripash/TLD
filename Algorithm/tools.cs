using System;
using System.Collections.Generic;
using System.Text;

namespace TLD
{
    class tools
    {
        public static string DeepToString(int[] arr)
        {
            string res = "";
            for (int i = 0; i < arr.Length; i++)
            {
                res+=" " + arr[i];
            }
            return res+"\n";
        }
    }
}
