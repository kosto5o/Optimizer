using System;
using System.Collections.Generic;

namespace Optimizer.Utils
{
    public static class ShuffleUtil
    {


        /// <summary>
        /// Fisher-Yates shuffle.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stack"></param>
        /// <returns></returns>
        public static Stack<T> Shuffle<T>(this Stack<T> stack )
        {
            Random _random = new Random();

            var array = stack.ToArray();

            int n = array.Length;
            for (int i = 0; i < n; i++)
            {
                // NextDouble returns a random number between 0 and 1.
                // ... It is equivalent to Math.random() in Java.
                int r = i + (int)(_random.NextDouble() * (n - i));
                T t = array[r];
                array[r] = array[i];
                array[i] = t;
            }

            return new Stack<T>(array);
        }
    }
}
