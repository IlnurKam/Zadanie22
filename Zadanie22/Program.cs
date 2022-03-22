using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Homework22
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Укажите размерность массива n=");
            int n = Convert.ToInt32(Console.ReadLine());

            Func<object, int[]> func1 = new Func<object, int[]>(Step1);
            Task<int[]> task1 = new Task<int[]>(func1, n);

            Func<Task<int[]>, int[]> func2 = new Func<Task<int[]>, int[]>(Step2);
            Task<int[]> task2 = task1.ContinueWith<int[]>(func2);

            Action<Task<int[]>> action = new Action<Task<int[]>>(Step3);
            Task task3 = task2.ContinueWith(action);

            task1.Start();
            Console.ReadKey();
        }

        static int[] Step1(object a)
        {
            int n = (int)a;
            Console.WriteLine("Сформирован массив случайных чисел:");
            int[] array = new int[n];
            Random random = new Random();
            for (int i = 0; i < n; i++)

            {
                array[i] = random.Next(0, 100);
            }

            int max = 0, min = 0;

            for (int i = 0; i < n; i++)
            {
                if (max < array[i])
                {
                    max = array[i];
                }
            }
            Console.WriteLine("Максимальный элемент массива = {0}", max);
            min = array[0];
            for (int i = 0; i < n; i++)
            {
                if (min > array[i])
                {
                    min = array[i];
                }
            }
            int result = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] % 2 == 0)
                {
                    result += array[i];
                }
            }
            Console.WriteLine("Сумма элементов массива = {0}", result);
            return array;
            Console.ReadKey();
        }
        static int[] Step2(Task<int[]> task)
        {

            int[] array = task.Result;
            for (int i = 0; i < array.Count() - 1; i++)
            {
                for (int n = i + 1; n < array.Count(); n++)
                {
                    if (array[i] > array[n])
                    {
                        int t = array[i];
                        array[i] = array[n];
                        array[n] = t;
                    }
                }
            }
            return array;
        }
        static void Step3(Task<int[]> task)
        {
            int[] array = task.Result;
            for (int i = 0; i < array.Count(); i++)
            {
                Console.Write($"{array[i]} ");
            }
        }

    }
}
