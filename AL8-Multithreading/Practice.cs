using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Advanced_Lesson_6_Multithreading
{
    class Practice
    {
        /// <summary>
        /// LA8.P1/X. Написать консольные часы, которые можно останавливать и запускать с 
        /// консоли без перезапуска приложения.
        /// </summary>
        public static void LA8_P1_5()
        {
            var thread = new Thread(() =>
            {
                while (true)
                {
                    Console.WriteLine(DateTime.Now);
                    System.Threading.Thread.Sleep(1000);
                    Console.Clear();
                }
            });
            thread.Start();
            while (true)
            {
                switch (Console.ReadLine())
                {
                    case "s":
                        thread.Resume();
                        break;
                    case "b":
                        thread.Suspend();
                        break;
                }
            }

        }

        /// <summary>
        /// LA8.P2/X. Написать консольное приложение, которое “делает массовую рассылку”. 
        /// </summary>
        public static void LA8_P2_5()
        {
            string path = @"d:\Рабочая папка\CS\ДЗ\";
            Random rand = new Random();
            for (int i = 0; i < 50; i++)
            {
                ThreadPool.QueueUserWorkItem((object state) =>
                {
                    string text = $"hello {rand.Next(1, 200)}";
                    using (StreamWriter sw = new StreamWriter(path + $"{rand.Next(1, 200)}.txt"))
                    {
                        sw.Write(text);
                    }
                    LA8_P5_5();
                });

            }
        }

        /// <summary>
        /// Написать код, который в цикле (10 итераций) эмулирует посещение 
        /// сайта увеличивая на единицу количество посещений для каждой из страниц.  
        /// </summary>
        //public static void LA8_P3_5()
        //{            
        //}

        /// <summary>
        /// LA8.P4/X. Отредактировать приложение по “рассылке” “писем”. 
        /// Сохранять все “тела” “писем” в один файл. Использовать блокировку потоков, чтобы избежать проблем синхронизации.  
        /// </summary>
        public static void LA8_P4_5()
        {
            object locker = new object();
            string path = @"d:\Рабочая папка\CS\ДЗ\";
            Random rand = new Random();
            int x = 0;
            using (StreamWriter sw = new StreamWriter(path + "email.txt"))
            {
                for (int i = 0; i < 50; i++)
                {
                    ThreadPool.QueueUserWorkItem((object state) =>
                    {
                        lock (locker)
                        {
                            string text = $"hello {x++}";
                            sw.WriteLine(text);

                        }
                    });
                }
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// LA8.P5/5. Асинхронная “отсылка” “письма” с блокировкой вызывающего потока 
        /// и информировании об окончании рассылки (вывод на консоль информации 
        /// удачно ли завершилась отсылка). 
        /// </summary>
        public async static void LA8_P5_5()
        {
            object locker = new object();
            Random rand = new Random();
            bool rezult = false;

            rezult = await SmptServer.SendEmail($"email {rand.Next(1, 10)}");
            if (rezult)
            {
                Console.WriteLine("success");
            }
            else
            {
                Console.WriteLine("fail");
            }
        }
    }
}
