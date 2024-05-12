using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Reflection.PortableExecutable;
using System.Threading.Channels;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CSharpFeatures
{
    public class CSharp9
    {
        public const int MIN_AGE = 18;

        public static void NewFeatures()
        {
            // C# 9
            // Records
            Person psd = new Person();
            Person p = new Person("Mahesh", "Bhosale");
            Person pCopy = new Person("FirstName", "LastName");
            Person pDiff = new Person("sds", "LastName");

            // Immutability and Init only setters
            Person2 p2 = new Person2("Mahesh", "Bhosale");
            p2.Profession = "Developer";
            //p2.FirstName = "test";
            //p2.Id = 121;
            p2 = new Person2("Mahesh", "Bhosale") { Id = 1 };


            // Struct
            Point pt = new Point(10, 20, 30);
            Console.WriteLine(pt);

            // Structural Value equality
            Console.WriteLine(p == pCopy);
            Console.WriteLine(p == pDiff);

            // Build in formatting for display
            Console.WriteLine(p2);


            // Inheritance same as class

            // with expressions in derived records or
            var ss = p2 with { FirstName = "Lead" };
            var fn = p2.FirstName;
            var ln = p2.LastName;
            // Nominal Records  Deconstructor 
            //(string sdfg, string dfg) = p2;
            //Console.WriteLine(firstName);
            //Console.WriteLine(lastName);

            //var p = new PRecord("", "", "", "");
            //(string s1, string s4) = p;
            // Top - level statements
            //      programs without Main methods

            // New features for partial methods

            // Target-typed new expressions
            Person pr1 = new();
            Console.WriteLine(pr1);
            Person pr = new("First", "Last");
            Dictionary<string, List<int>> field = new() { { "item1", new() { 1, 2, 3 } } };

            // static anonymous functions
            // Lambda discard parameters discard
            (var firstName, _, _, var email) = DiscordFun();
            DiscordFun2();

            // Attributes on local functions
            BeforCsharp8();
            AfterCSharp9();

            // C# 10
            // C# 11
            // C# 12
        }

        public record PRecord(string s1, string s2, string s3, string s4);

        private static void AfterCSharp9()
        {
            [Conditional("DEBUG")]
            static void DoAction()
            {
                // Perform action

                Console.WriteLine("Performing action");
            }

            DoAction();
        }

        private static void BeforCsharp8()
        {
            static void DoAction()
            {
                // Perform action

                Console.WriteLine("Performing action");
            }

#if DEBUG
            DoAction();
#endif
        }

        public static (string, string, int, string) DiscordFun()
        {
            return ("FirstName", "LastName", 15, "email");
        }

        public static void DiscordFun2()
        {
            //bool.TryParse("True", out bool va) ? Op1() : Op2();
            _ = bool.TryParse("True", out _) ? Op1() : Op2();
        }

        public static string Op1() { return ""; }
        public static string Op2() { return ""; }

        public record Person(string FirstName = "", string LastName = "");
        public record Person2(string FirstName, string LastName)
        {
            public int Id { get; init; }
            public string Profession { get; set; } = string.Empty;
        }
        public record Person3(string FirstName, string LastName, string Company) : Person(FirstName, LastName);

        public readonly record struct Point(double X, double Y, double Z);

        //public class Person
        //{
        //    public string FirstName { get; set; }
        //    public string LastName { get; set; }

        //    public Person(string FirstName, string LastName)
        //    {
        //        this.FirstName = FirstName;
        //        this.LastName = LastName;
        //    }
        //}
    }
}