﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TLD
{
    class Constraints
    {
        //Neighbours Matrix for Constraints
        static bool[,] cons_mat;
        private static int LENGTH;

        public Constraints(int size)
        {
            if (cons_mat == null)
            {
                cons_mat = new bool[size, size];
                LENGTH = size;
            }
        }

        public Constraints()
        {
            if (cons_mat == null)
                throw new System.InvalidOperationException("Constraints was not initialized");
        }

        /// <summary>
        /// add constraint for lanes : (a,b)
        /// </summary>
        /// <param name="a"> lane </param>
        /// <param name="b"> lane </param>
        public void Add_cons(int a, int b)
        {
            cons_mat[a, b] = true;
            cons_mat[b, a] = true;
        }

        /// <summary>
        /// returns if there is constraint between lanes : (a,b)
        /// </summary>
        /// <param name="a"> lane </param>
        /// <param name="b"> lane </param>
        /// <returns> true if there is constraint, false otherwise </returns>
        public bool Check_cons(int a, int b)
        {
            return cons_mat[a, b];
        }
        /// <summary>
        /// ToString
        /// </summary>
        /// <returns> {[?][?]
        ///            [?][?]}</returns>
        public override string ToString()
        {
            String res = "{\n";
            for(int i = 0; i < LENGTH; i++)
            {
                for(int j = 0; j < LENGTH; j++)
                {
                    res += "[" + cons_mat[i, j] + "] ";
                }
                res += "\n";
            }
            return res+"}";
        }
    }
}