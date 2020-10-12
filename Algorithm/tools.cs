using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

class tools
{
    public static string DeepToString<T>(ref T[] arr)
    {
        string res = "";
        for (int i = 0; i < arr.Length; i++)
        {
            res += " " + arr[i];
        }
        return res + "\n";
    }
    public static string DeepToString<T>(ref List<T> lst)
    {
        string res = "";
        for (int i = 0; i < lst.Count; i++)
        {
            res += " " + lst[i];
        }
        return res + "\n";
    }
    public static string DeepToString<T>(ref T[,] arr)
    {
        string res = "";
        for (int i = 0; i < arr.GetLength(0); i++)
        {
            for (int j = 0; j < arr.GetLength(1); j++)
                res += " " + arr[i, j];
            res += "\n";
        }
        return res;
    }
}

