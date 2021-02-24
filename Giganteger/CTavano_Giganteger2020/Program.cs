//////////////////////////////////////////////////////////////////////
//Christofer Tavano - Giganteger
//
//Created to bypass the number value constraints of a data member
//////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CTavano_Giganteger2020{
    class Program{

        static readonly Random _rnd = new Random();

        static void Main(string[] args){
            //// construction tests
            //{
            //    GInt a = new GInt(12345);
            //    Console.WriteLine($"Should be 12345 : {a}");
            //    GInt b = new GInt("12345");
            //    Console.WriteLine($"Should be 12345 : {b}");
            //    Console.WriteLine(a.Equals(b));
            //    GInt c = new GInt(1235);
            //    Console.WriteLine($"is {a} = {c}");
            //    Console.WriteLine(a.Equals(c));
            //    GInt c = new GInt(b);
            //    Console.WriteLine($"Should be 12345 : {c}");
            //    GInt d = new GInt(UInt64.MaxValue);
            //    Console.WriteLine($"Should be 2^64 - 1 : {d}");
            //}

            // equals / CompareTo tests
            //{
            //    Console.WriteLine("500 sorted values (0-49) : ");
            //    List<GInt> stuff = new List<GInt>();
            //    for (int i = 0; i < 500; ++i)
            //        stuff.Add(new GInt((uint)_rnd.Next(0, 50)));
            //    stuff.Sort();
            //    for (int i = 0; i < 500; ++i)
            //        Console.Write($"{stuff[i],2}, ");
            //    Console.WriteLine();
            //}

            // basic adding tests
            {
                GInt a = new GInt(56);
                GInt b = new GInt(5);
                GInt c = a.Add(b);
                Console.WriteLine($"56 + 5 == {56 + 5} : {c}");

                GInt d = new GInt("");
                GInt e = new GInt();
                GInt f = d.Add(e);
                Console.WriteLine($"0 + 0 == {0} : {f}");

                GInt g = new GInt("99");
                GInt h = new GInt(1);
                GInt i = g.Add(h);
                Console.WriteLine($"99 + 1 == {100} : {i}");

                Console.WriteLine($"Slow count to 1M start...");
                while (f.ToString() != "1000000")
                    f = f.Add(new GInt(1));
                Console.WriteLine(f);
                Console.WriteLine($"Slow count to 1M done!");

                // static form
                Console.WriteLine($"{67} + {45} is {67 + 45} : {GInt.Add(new GInt(67), new GInt(45))}");
            }

                //// basic subtraction tests
            {
                GInt a = new GInt(56);
                GInt b = new GInt(5);
                GInt c = a.Sub(b);
                Console.WriteLine($"{56} - {5} == {56 - 5} : {c}");

                GInt d = new GInt(10000000);
                GInt e = new GInt(9999999);
                GInt f = d.Sub(e);
                Console.WriteLine($"{10000000} - {9999999} == {10000000 - 9999999} : {f}");

                GInt g = new GInt(1);
                GInt h = new GInt(1);
                GInt i = g.Sub(h);
                Console.WriteLine($"{1} - {1} == {1 - 1} : {i}");

                ////Uncomment for throwing an exception of subtracting number which would equate to a negative number
                //GInt j = new GInt(1);
                //GInt k = new GInt(10);
                //GInt l = j.Sub(k);
                //Console.WriteLine($"{1} - {10} == {"error"} : {f}");
            }

            // basic multiplication tests
            {
                //random test(low hammer)
                Console.WriteLine($"low value range hammer test for SMult");
                for (int i = 0; i < 500; ++i)
                {
                    int a = _rnd.Next(0, 5);
                    int b = _rnd.Next(0, 5);
                    GInt ga = new GInt((uint)a);
                    GInt gb = new GInt((uint)b);
                    GInt gc = ga.SMult(gb);
                    if (gc.ToString() != (a * b).ToString())
                        Console.WriteLine($"Error {a} x {b} == {a * b}, not {gc}!");
                    else
                        Console.Write(".");
                }
                Console.WriteLine();

                Console.WriteLine($"high value range hammer test for SMult");
                // random test (high hammer)
                for (int i = 0; i < 500; ++i)
                {
                    //max would be 250M, so well within int range
                    int a = _rnd.Next(0, 50000);
                    int b = _rnd.Next(0, 5000);
                    GInt ga = new GInt((uint)a);
                    GInt gb = new GInt((uint)b);
                    GInt gc = ga.SMult(gb);
                    if (gc.ToString() != (a * b).ToString())
                        Console.WriteLine($"Error {a} x {b} == {a * b}, not {gc}!");
                    else
                        Console.Write(".");
                }
                Console.WriteLine();
            }

            //IDiv tests
            {
                GInt a = new GInt(50);
                GInt b = new GInt(10);
                GInt c = a.IDiv(b);

                Console.WriteLine($"{55} IDIV {10} == {55 / 10} : {c}");

                ////Cant divide by zero exception error
                //GInt d = new GInt(50);
                //GInt e = new GInt(0);
                //GInt f = d.IDiv(e);
                //Console.WriteLine($"{55} IDIV {0} == {"error"} : {e}");

                GInt g = new GInt(0);
                GInt h = new GInt(10);
                GInt i = g.IDiv(h);

                Console.WriteLine($"{0} IDIV {10} == {0 / 10} : {i}");
            }

            //// FMult tests
            {
                Console.WriteLine("Really big FMult test : ");
                GInt a = new GInt("57434234232342342345756745856723452345456567786783456345876545332");
                GInt b = new GInt("235189237490128374291837412837461482384761289374450235098273450982345723459823465982347569823756923874569832475692387456983475");
                //Answer
                //Console.WriteLine("13507913754934024072085868037590354551475571522224417209261368019051376903642391971644827039466973732961330920451358784947836532869173094727587651764722567496889789805298633742253090812388700");
                GInt c = a.FMult(b);
                Console.WriteLine(c);
            }

            Console.ReadKey();

        }
    }
}