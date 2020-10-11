using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using UnityEngine;

    class SimulatedAnnealing
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"> starting junction - can reduce proccess </param>
        /// <param name="t"> starting temprature </param>
        /// <param name="end_t"> ending temprature </param>
        /// <param name="limit"> epochs for each temprature </param>
        public static Junction Compute(Junction start, float t, float end_t, int limit)
        {
            Junction jen = start;
            Junction best_jen = start;
            System.Random rand = new System.Random();
            int eval = start.Eval();
            while (t > end_t)
            {
                for (int i = 0; i < limit; i++)
                {
                    Junction jen_new = jen.Choose_Random_Successor();
                    int eval_new = jen_new.Eval();
                    int delta_jen = eval_new - eval;
                    if (delta_jen > 0 || (rand.NextDouble() < Acceptor(delta_jen, t) && eval_new > 0))
                    {
                        jen = jen_new;
                        eval = eval_new;
                        if(eval >= best_jen.Eval())
                        {
                            best_jen = jen;
                        }
                    }
                }
                t = Schedule(t);
            }
            return best_jen;
        }

        /// <summary>
        /// Function for decision making: should we try worse junction?
        /// </summary>
        /// <param name="delta"> diffrence between new evaluation and old one </param>
        /// <param name="temprature"> SA temprates at given moment </param>
        /// <returns> Acceptor evaluation </returns>
        private static float Acceptor(int delta, float temprature)
        {
            return (float)Math.Pow(Math.E, ((float)delta / temprature));
        }

        /// <summary>
        /// Tempreture decrease function - a * t : 0.5 < a < 1
        /// </summary>
        /// <param name="t"> temprature </param>
        /// <returns></returns>
        private static float Schedule(float t)
        {
            float a = 0.9f;
            return t * a;
        }

    }

