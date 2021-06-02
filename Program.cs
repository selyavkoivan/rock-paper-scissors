using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace rock_paper_scissors
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
          
            if (args.Length == 0)
            {
                Console.WriteLine("ERROR\nTHERE IS NO arguments\nNO\tdotnet run\nYES\tdotnet run rock paper scissors");
            }
            else if (args.Length < 3)
            {
                Console.WriteLine($"ERROR\nINCORRECT number of arguments\nthe number of arguments MUST BE MORE THAN THREE\nNO\tdotnet run {string.Join(" ", args)}\nYES\tdotnet run rock paper scissors");
            }
            else if (args.Length % 2 == 0)
            {
                Console.WriteLine($"ERROR\nINCORRECT number of arguments\nthe number of arguments must be odd\nNO\tdotnet run {string.Join(" ", args)}\nYES\tdotnet run rock paper scissors");
            }
            else if (args.Distinct().Count() != args.Length)
            {
                Console.WriteLine($"ERROR\nINCORRECT arguments\nthe number of arguments must be odd\nNO\tdotnet run {string.Join(" ", args)}\nYES\tdotnet run {string.Join(" ", args.Distinct())} uniqueArgs");

            }
            else StartPlaying(args);
          
           
        }

        private static void StartPlaying(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Make a move");
                for (var i = 0; i < args.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. - {args[i]}");
                }

                Console.WriteLine("0. - exit");
                Console.Write("ENTER your move : ");
                try
                {
                    var choice = int.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case > 0 when choice <= args.Length:
                            var randomMove = MoveMaker(args.Length);
                            var key = GetKey();
                            Console.WriteLine($"ur move : {args[choice - 1]}");
                            Console.WriteLine($"random move : {args[randomMove]}");
                            Result(choice - 1, randomMove, args.Length);
                            Hash(key, randomMove);
                            break;
                        case 0:
                            Console.WriteLine("nice to play with u\nhave a nice day");
                            return;
                        default:
                            throw new Exception();
                    }
                    Console.ReadKey();

                }
                catch (Exception)
                {
                    Console.WriteLine("wrong input");
                    Console.WriteLine("PRESS any button to continue"); 
                    Console.ReadKey();
                }
            }
        }

        private static byte[] GetKey()
        {
            using var random = RandomNumberGenerator.Create();
            var key = new byte[16];
            random.GetBytes(key);
            return key;
        }
        private static int MoveMaker(int length)
        {
            return new Random().Next(0, length);
        }
        private static void Result(int urMove, int randMove, int length)
        {
            if (randMove == urMove) Console.WriteLine("result : nobody won");
            else if (randMove > urMove)
            {
                Console.WriteLine(randMove - urMove > length / 2 ? "result : u lose" : "result : u win");
            }
            else
            {
                Console.WriteLine(urMove - randMove > length / 2 ? "result : u win" : "result : u lose");
            }
            
        }

        private static void Hash(byte[] key, int move)
        {
            var hmac = new HMACSHA256(key);
           
            var d = move.ToString();
            var f = Encoding.Unicode.GetBytes(d);
            var g = hmac.ComputeHash(f);
            var hashedMove = Convert.ToBase64String(g);
            Console.WriteLine($"HMAC : {hashedMove}");
        }
    }
}
