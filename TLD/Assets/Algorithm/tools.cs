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
    /// <summary>
    /// swap 2 columns in matrix [x,i] <=> [x,j]
    /// </summary>
    /// <typeparam name="T"> Type </typeparam>
    /// <param name="arr"> Matrix </param>
    /// <param name="a"> First column index </param>
    /// <param name="b"> Second column index </param>
    public static void SwapColumns<T>(ref T[,] arr, int a, int b)
    {
        T temp;
        for(int i = 0; i < arr.GetLength(0); i++)
        {
            temp = arr[i, a];
            arr[i, a] = arr[i, b];
            arr[i, b] = temp;
        }
    }
    /// <summary>
    /// swap in array arr[i] <=> arr[j]
    /// </summary>
    /// <typeparam name="T"> Type </typeparam>
    /// <param name="arr"> Array </param>
    /// <param name="a"> First index </param>
    /// <param name="b"> Second index </param>
    public static void SwapArr<T>(ref T[] arr, int a, int b)
    {
        T temp;
        temp = arr[a];
        arr[a] = arr[b];
        arr[b] = temp;
    }
}

