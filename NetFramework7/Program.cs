using System;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NetFramework7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var name = "Mark";
            var date = DateTime.Now;
            Console.WriteLine("Hello, World!");
            // Composite formatting:
            Console.WriteLine("Hello, {0}! Today is {1}, it's {2:HH:mm} now.", name, date.DayOfWeek, date);
            // String interpolation:
            Console.WriteLine($"Hello, {name}! Today is {date.DayOfWeek}, it's {date:HH:mm} now.");
            Console.WriteLine($"Hello, {name}! Todays Date is {date:MM/dd/yyyy}.");
            // Both calls produce the same output that is similar to:
            // Hello, Mark! Today is Wednesday, it's 19:40 now.

            Console.WriteLine($"|{"Left",-7}|{"Right",7}|");

            const int FieldWidthRightAligned = 20;
            Console.WriteLine($"{Math.PI} - default formatting of the pi number");
            Console.WriteLine($"{Math.PI,FieldWidthRightAligned} - default formatting of the pi number");
            Console.WriteLine($"{Math.PI,FieldWidthRightAligned:F3} - display only three decimal digits of the pi number");
            Console.WriteLine($"{Math.PI,FieldWidthRightAligned:F4} - display only three decimal digits of the pi number");

            // Output is:
            // |Left   |  Right|
            //     3.14159265358979 - default formatting of the pi number
            //                3.142 - display only three decimal digits of the pi number
        }
    }
}
