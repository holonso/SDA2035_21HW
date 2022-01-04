using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace p21ex01
{
    class Program
    {
        //Инициализируем сад
        static int n1;
        static int n2;
        static int[,] garden;

        static void Main(string[] args)
        {
            Console.WriteLine("Введите длину сада");
            n1 = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите ширину сада");
            n2 = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("");


            garden = new int[n1, n2];

            //Первый поток (delegate -> thread -> start)
            ThreadStart gardenerDelegate1 = new ThreadStart(Gardener1);
            Thread gardener1 = new Thread(gardenerDelegate1);
            gardener1.Start();
            //Второй поток (из под основной программы)
            Gardener2();

            // Вывод итогового массива
            Console.WriteLine("В итоге сад был выполнен соответственно:");
            for (int i = 0; i < n1; i++)
            {
                for (int j = 0; j < n2; j++)
                {
                    Console.Write("{0,3} ", garden[i, j]);
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }

        static void Gardener1()
        {
            for (int i = 0; i < n1; i++)
            {
                for (int j = 0; j < n2; j++)
                {
                    if (garden[j, i] == 0)
                    {
                        garden[j, i] = -1;
                        Console.WriteLine("ПЕРВЫЙ обработал {0},{1}", j, i);
                    }
                    else
                    {
                        Console.WriteLine("ПЕРВЫЙ пропустил {0},{1}, уже обработан ВТОРЫМ", j, i);
                    }
                    Thread.Sleep(1);
                }
            }
        }

        static void Gardener2()
        {
            for (int i = n2 - 1; i >= 0; i--)
            {
                for (int j = n1 - 1; j >= 0; j--)
                {
                    if (garden[i, j] == 0)
                    {
                        garden[i, j] = -2;
                        Console.WriteLine("ВТОРОЙ обработал {0},{1}", i, j);
                    }
                    else
                    {
                        Console.WriteLine("ВТОРОЙ пропустил {0},{1}, уже обработан ПЕРВЫМ", i, j);
                    }

                    Thread.Sleep(1);
                }
            }
        }
    }
}
