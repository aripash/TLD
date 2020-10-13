using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
/// Class for debugging tools and storage
/// </summary>
class tools
{
    public static int numOfRoads;
    public static List<string> cons;
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
            res += i + ": ";
            for (int j = 0; j < arr.GetLength(1); j++)
                res += " " + arr[i, j];
            res += "\n";
        }
        return res;
    }
}

