using System;
using System.Collections.Generic;
using System.Text;

namespace TLD
{
    class SimulatedAnnealing
    {
        /* Constructor */
        public SimulatedAnnealing(Junction start, float t, float end_t, int limit)
        {
            Junction jen = start;
            Random rand = new Random();
            int eval = start.Eval();
            while (t > end_t)
            {
                for(int i = 0; i < limit; i++)
                {
                    Junction jen_new = jen.Choose_Random_Successor();
                    int eval_new = jen_new.Eval();
                    int delta_jen = eval_new - eval;
                    if (delta_jen > 0 || rand.NextDouble() < Acceptor(delta_jen, t))
                    {
                        jen = jen_new;
                        eval = eval_new;
                    }
                }
                t = Schedule(t);
            }
        }

        /// <summary>
        /// Function for decision making: should we try worse junction?
        /// </summary>
        /// <param name="delta"> diffrence between new evaluation and old one </param>
        /// <param name="temprature"> SA temprates at given moment </param>
        /// <returns> Acceptor evaluation </returns>
        private float Acceptor(int delta, float temprature)
        {
            return (float)Math.Pow(Math.E, (float)delta / temprature);
        }
        
        /// <summary>
        /// Tempreture decrease function - a * t : 0.5 < a < 1
        /// </summary>
        /// <param name="t"> temprature </param>
        /// <returns></returns>
        private float Schedule(float t)
        {
            float a = 0.9f;
            return t * a;
        }

    }
}
