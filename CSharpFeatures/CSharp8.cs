using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Reflection.PortableExecutable;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CSharpFeatures
{
    internal class CSharp8
    {
        public static async Task NewFeatures()
        {
            // C# 8 
            // 1. Default interface methods
            IWriteLine writeLine = new WL();
            writeLine.WriteLine();

            // 2.Nullable reference type
            string? nullableString = null;
            if (nullableString != null)
            {
                Console.WriteLine(nullableString.Length); // WARNING: may be null! Take care!
            }

            Program? program = null;
            program?.Equals(nullableString);

            // 3. Advanced Pattern Matching
            // Declaration and type patterns
            Ranges();
            DeclarationAndTypePattern1();
            DeclarationAndTypePattern2();
            DeclarationAndTypePattern3();
            ConstantPattern();
            RelationalPatterns(); // C# 9
            LogicalPatterns(); // C# 9
            PrecedenceAndOrderOfChecking(); // C# 10
            PropertyPattern();
            PropertyPattern2();
            PropertyPattern3();

            // Positional pattern
            PositionalPattern();
            PositionalPattern2();

            // Parenthesized pattern
            ParenthesizedPattern(10);
            // List patterns
            ListPatterns();
            ListPatterns2();

            // 4. Using declarations
            UsingsOld();
            UsingNew();

            // 5. Static local function
            // C# 7 using a static modifier with the local function is impossible
            StaticLocalFunc();

            // 6. Asynchronous streams
            // 7. Indices and ranges
            IndicesAndRanges();
            IndicesAndRanges2();

            // Asynchronous Streams in C# 8
            await AsynchronousStreams();

            // 8. Null - coalescing assignment
            NullCoalescingAssignment2();
            // 9. Unmanaged constructed types
            // where (generic type constraint) (C# Reference)
            // 10. Stackalloc in nested expressions
            // 11. Enhancement of interpolated verbatim strings
            // Format {<interpolationExpression>[,<alignment>][:<formatString>]}

            InterpolatedStr();
            InterpolatedStr2();
            InterpolatedRawStr2();
        }

        private static async Task AsynchronousStreams()
        {
            await foreach (var number in GenerateSequence())
            {
                Console.WriteLine(number);
            }

        }
        public static async System.Collections.Generic.IAsyncEnumerable<int> GenerateSequence()
        {
            for (int i = 0; i < 20; i++)
            {
                await Task.Delay(100);
                yield return i;
            }
        }

        private static void InterpolatedRawStr2()
        {
            int X = 2;
            int Y = 3;

            var pointMessage = $"""The point "{X}, {Y}" is {Math.Sqrt(X * X + Y * Y):F3} from the origin""";

            Console.WriteLine(pointMessage);
            // Output is:
            // The point "2, 3" is 3.606 from the origin

            pointMessage = $$"""{The point {{{X}}, {{Y}}} is {{Math.Sqrt(X * X + Y * Y):F3}} from the origin}""";
            Console.WriteLine(pointMessage);
            // Output is:
            // {The point {2, 3} is 3.606 from the origin}
        }

        private static void InterpolatedStr2()
        {
            Console.WriteLine($"|{"Left",-7}|{"Right",7}|");

            const int FieldWidthRightAligned = 20;
            Console.WriteLine($"{Math.PI,FieldWidthRightAligned} - default formatting of the pi number");
            Console.WriteLine($"{Math.PI,FieldWidthRightAligned:F3} - display only three decimal digits of the pi number");
            Console.WriteLine($"{Math.PI,FieldWidthRightAligned:F4} - display only 4 decimal digits of the pi number");

            // Output is:
            // |Left   |  Right|
            //     3.14159265358979 - default formatting of the pi number
            //                3.142 - display only three decimal digits of the pi number
        }

        private static void InterpolatedStr()
        {
            var name = "Mark";
            var date = DateTime.Now;

            date.ToString("HH:mm");

            var ss = $"{date:HH:mm}";

            // Composite formatting:
            Console.WriteLine("Hello, {0}! Today is {1}, it's {2:HH:mm} now.", name, date.DayOfWeek, date);
            // String interpolation:
            Console.WriteLine($"Hello, {name}! Today is {date.DayOfWeek}, it's {date:HH:mm} now.");
            Console.WriteLine($"Hello, {name}! Today is {date.Date}, it's {date:MM/dd/yyyy} now.");
            // Both calls produce the same output that is similar to:
            // Hello, Mark! Today is Wednesday, it's 19:40 now.
        }

        private static void IndicesAndRanges2()
        {
            int[] numbers = [0, 10, 20, 30, 40, 50];
            int start = 1;
            int amountToTake = 3;
            int[] subset = numbers[start..(start + amountToTake)];
            Display(subset);  // output: 10 20 30

            int margin = 1;
            int[] inner = numbers[margin..^margin];
            Display(inner);  // output: 10 20 30 40

            string line = "one two three";
            int amountToTakeFromEnd = 5;
            Range endIndices = ^amountToTakeFromEnd..^0;
            string end = line[endIndices];
            Console.WriteLine(end);  // output: three

            void Display<T>(IEnumerable<T> xs) => Console.WriteLine(string.Join(" ", xs));
        }

        private static void IndicesAndRanges()
        {
            int[] numbers = [0, 10, 20, 30, 40, 50];
            int amountToDrop = numbers.Length / 2;

            int[] rightHalf = numbers[amountToDrop..];
            Display(rightHalf);  // output: 30 40 50

            int[] leftHalf = numbers[..amountToDrop];
            Display(leftHalf);  // output: 0 10 20

            int[] all = numbers[..];
            Display(all);  // output: 0 10 20 30 40 50

            void Display<T>(IEnumerable<T> xs) => Console.WriteLine(string.Join(" ", xs));
        }

        private static void NullCoalescingAssignment2()
        {
            string variable = null;

            if (variable == null)
            {
                variable = "C#7"; // C# 1..7  
            }
            variable = null;
            variable ??= "C#8"; // C# 8
        }

        private static int StaticLocalFunc()
        {
            int y;
            LocalFunction();
            return y;

            void LocalFunction() => y = 0;
        }

        private static void UsingNew()
        {

            using StreamReader reader = File.OpenText("numbers.txt");

            var numbers = new List<int>();
            string line;
            while ((line = reader.ReadLine()) is not null)
            {
                if (int.TryParse(line, out int number))
                {
                    numbers.Add(number);
                }
            }

            // Reader will dispose here
        }

        private static void UsingsOld()
        {
            var numbers = new List<int>();
            using (StreamReader reader = File.OpenText("numbers.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) is not null)
                {
                    if (int.TryParse(line, out int number))
                    {
                        numbers.Add(number);
                    }
                }
            }
            // Reader will dispose here
        }

        private static void ListPatterns2()
        {
            List<int> numbers = new() { 1, 2, 3 };

            if (numbers is [var first, _, _])
            {
                Console.WriteLine($"The first element of a three-item list is {first}.");
            }

            if (numbers is [var f, .., var l])
            {
                Console.WriteLine($"The first element of a three-item list is {f}. last is {l}");
            }
            // Output:
            // The first element of a three-item list is 1.
        }

        private static void ListPatterns()
        {
            int[] numbers = { 1, 2, 3 };

            Console.WriteLine(numbers is [1, 2, 3]);  // True
            Console.WriteLine(numbers is [1, 2, 4]);  // False
            Console.WriteLine(numbers is [1, 2, 3, 4]);  // False
            Console.WriteLine(numbers is [0 or 1, <= 2, >= 3]);  // True

            Console.WriteLine(new[] { 1, 2, 3, 4, 5 } is [> 0, > 0, ..]);  // True
            Console.WriteLine(new[] { 1, 1 } is [_, _, ..]);  // True
            Console.WriteLine(new[] { 0, 1, 2, 3, 4 } is [> 0, > 0, ..]);  // False
            Console.WriteLine(new[] { 1 } is [1, 2, ..]);  // False

            Console.WriteLine(new[] { 1, 2, 3, 4 } is [.., > 0, > 0]);  // True
            Console.WriteLine(new[] { 2, 4 } is [.., > 0, 2, 4]);  // False
            Console.WriteLine(new[] { 2, 4 } is [.., 2, 4]);  // True

            Console.WriteLine(new[] { 1, 2, 3, 4 } is [>= 0, .., 2 or 4]);  // True
            Console.WriteLine(new[] { 1, 0, 0, 1 } is [1, 0, .., 0, 1]);  // True
            Console.WriteLine(new[] { 1, 0, 1 } is [1, 0, .., 0, 1]);  // False

            void MatchMessage(string message)
            {
                var result = message is ['a' or 'A', .. var s, 'a' or 'A']
                    ? $"Message {message} matches; inner part is {s}."
                    : $"Message {message} doesn't match.";
                Console.WriteLine(result);
            }

            MatchMessage("aBBA");  // output: Message aBBA matches; inner part is BB.
            MatchMessage("apron");  // output: Message apron doesn't match.

            void Validate(int[] numbers)
            {
                var result = numbers is [< 0, .. { Length: 2 or 4 }, > 0] ? "valid" : "not valid";
                Console.WriteLine(result);
            }

            Validate(new[] { -1, 0, 1 });  // output: not valid
            Validate(new[] { -1, 0, 0, 1, 2, 1, 5 });  // output: valid
        }

        private static void ParenthesizedPattern(object input)
        {
            if (input is (float or double))
            {
                Console.WriteLine("Float or Double");
            }
        }

        private static void PositionalPattern2()
        {
            var numbers = new List<int> { 1, 2, 3 };
            if (SumAndCount(numbers) is (Sum: var sum, Count: > 0))
            {
                Console.WriteLine($"Sum of [{string.Join(" ", numbers)}] is {sum}");  // output: Sum of [1 2 3] is 6
            }

            static (double Sum, int Count) SumAndCount(IEnumerable<int> numbers)
            {
                int sum = 0;
                int count = 0;
                foreach (int number in numbers)
                {
                    sum += number;
                    count++;
                }
                return (sum, count);
            }
        }

        private static void PropertyPattern3()
        {
            Segment segment = new Segment(new Point(5, 4), new Point(10, 20));
            if ((segment.Start.X == 2 && segment.Start.Y == 0) || segment.End.Y == 0)
            {

            }

            if (segment is { Start: { X: 2 } and { Y: 0 } } or { End: { Y: 0 } })
            { }

            IsAnyEndOnXAxis(segment);

            static bool IsAnyEndOnXAxis(Segment segment) =>
                        segment is { Start: { X: 2 } and { Y: 0 } } or { End: { Y: 0 } };
        }

        public record Point(int X, int Y);
        public record Segment(Point Start, Point End);

        private static void PropertyPattern2()
        {
            Console.WriteLine(TakeFive("Hello, world!"));  // output: Hello
            Console.WriteLine(TakeFive("Hi!"));  // output: Hi!
            Console.WriteLine(TakeFive(new[] { '1', '2', '3', '4', '5', '6', '7' }));  // output: 12345
            Console.WriteLine(TakeFive(new[] { 'a', 'b', 'c' }));  // output: abc



            static string TakeFive(dynamic input) => input switch
            {
                string { Length: >= 5 } s => s[0..5],
                string s => s,

                ICollection<char> { Count: >= 5 } symbols => new string(symbols.Take(5).ToArray()),
                ICollection<char> symbols => new string(symbols.ToArray()),

                null => throw new ArgumentNullException(nameof(input)),
                _ => throw new ArgumentException("Not supported input type."),
            };
        }

        private static void PropertyPattern()
        {
            DateTime date = new DateTime(2023, 10, 21);
            if (date.Year == 2023 && date.Month == 5 && (date.Month == 19 || date.Month == 20 || date.Month == 21))
                Console.WriteLine("There is Conference");

            if (date is { Year: 2020, Month: 5, Day: 19 or 20 or 21 })
                Console.WriteLine("There is Conference");

            if (IsConferenceDay(date))
                Console.WriteLine("There is Conference");

            static bool IsConferenceDay(DateTime date) => date is { Year: 2020, Month: 5, Day: 19 or 20 or 21 };
        }

        private static void PrecedenceAndOrderOfChecking()
        {
            char c = 'd';
            if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z'))
                Console.WriteLine($"{c} Is letter");
            if (c is (>= 'a' and <= 'z') or (>= 'A' and <= 'Z'))
                Console.WriteLine($"{c} Is letter");

            if (c is (>= 'a' and <= 'z') or (>= 'A' and <= 'Z'))
            {
                Console.WriteLine($"{c} Is letter");
            }

            if (IsLetter(c))
                Console.WriteLine($"{c} Is letter");

            static bool IsLetter(char c) => c is (>= 'a' and <= 'z') or (>= 'A' and <= 'Z');
        }

        private static void LogicalPatterns()
        {
            Console.WriteLine(GetCalendarSeason(new DateTime(2021, 3, 14)));  // output: spring
            Console.WriteLine(GetCalendarSeason(new DateTime(2021, 7, 19)));  // output: summer
            Console.WriteLine(GetCalendarSeason(new DateTime(2021, 2, 17)));  // output: winter

            static string GetCalendarSeason(DateTime date) => date.Month switch
            {
                >= 3 and < 6 => "spring",
                >= 6 and < 9 => "summer",
                >= 9 and < 12 => "autumn",
                12 or (>= 1 and < 3) => "winter",
                _ => throw new ArgumentOutOfRangeException(nameof(date), $"Date with unexpected month: {date.Month}."),
            };
        }

        private static void RelationalPatterns()
        {
            // C# 9
            Console.WriteLine(Classify(13));  // output: Too high
            Console.WriteLine(Classify(double.NaN));  // output: Unknown
            Console.WriteLine(Classify(2.4));  // output: Acceptable

            var m = 13;
            //Old way
            static string Cl(double measurement)
            {
                if (measurement < -4.0)
                {
                    return "Too low";
                }
                else if (measurement > 10)
                {
                    return "Too high";
                }
                else
                {
                    return "Acceptable";
                }
            }

            // New way
            static string Classify(object measurement) => measurement switch
            {
                < -4.0 => "Too low",
                > 10.0 => "Too high",
                double.NaN => "Unknown",
                _ => "Acceptable",
            };
        }

        private static void ConstantPattern()
        {
            int switch_on = 10;
            double dt = 0;
            switch (switch_on)
            {
                case 10:
                    dt = 10.99;
                    break;
                case 20:
                    dt = 20.99;
                    break;
                default:
                    dt = 30.99;
                    break;
            }

            ///
            Classify(10);

            static double Classify(int measurement) => measurement switch
            {
                10 => 10.99,
                20 => 20.99,
                _ => 30.99,
            };
        }

        private static void Ranges()
        {
            Index i1 = 3; // number 4 from beginning  
            Index i2 = ^4; // number 4 from end
            int[] a = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            // Older way
            Console.WriteLine($"{a[i1]}, {a[a.Length - 4]}"); // "3, 6"

            // New way
            Console.WriteLine($"{a[i1]}, {a[^4]}"); // "3, 6"

            //Older way

            // New way
            var slice = a[3..^4]; // { 3, 4, 5 }
            Console.WriteLine(slice);
        }

        private static void DeclarationAndTypePattern3()
        {
            int? xNullable = null;
            int y = 23;
            object yBoxed = y;

            // Older way

            if (xNullable.HasValue && typeof(int) == yBoxed.GetType())
            {
                int p = xNullable.Value;
                int q = (int)yBoxed;
                Console.WriteLine(p + q);  // output: 30
            }

            // New way
            if (xNullable is int a && yBoxed is int b)
            {
                Console.WriteLine(a + b);  // output: 30
            }
        }

        private static void DeclarationAndTypePattern2()
        {
            var numbers = new int[] { 10, 20, 30 };
            // Older way
            Console.WriteLine(GetSourceLabel1(numbers));  // output: 1

            // New Way
            Console.WriteLine(GetSourceLabel(numbers));  // output: 1

            var letters = new List<char> { 'a', 'b', 'c', 'd' };
            Console.WriteLine(GetSourceLabel1(letters));  // output: 2
            Console.WriteLine(GetSourceLabel(letters));  // output: 2

            static int GetSourceLabel1<T>(IEnumerable<T> source)
            {
                if (typeof(System.Int32[]) == source.GetType())
                {
                    return 1;
                }
                else if (typeof(List<char>) == source.GetType())
                {
                    return 2;
                }
                else
                {
                    return 3;
                }
            };

            static int GetSourceLabel<T>(IEnumerable<T> source) => source switch
            {
                Array array => 1,
                ICollection<T> collection => 2,
                _ => 3,
            };
        }

        private static void DeclarationAndTypePattern1()
        {
            object greeting = "Hello, World!";

            if (greeting.GetType() == typeof(string))
            {
                string ss = (string)greeting;

            }


            //string ss = greeting;
            if (greeting is string message)
            {
                Console.WriteLine(message.ToLower());  // output: hello, world!
            }
        }

        #region Positional pattern
        //public readonly struct PT
        //{
        //    public int X { get; }
        //    public int Y { get; }

        //    public PT(int x, int y) => (X, Y) = (x, y);

        //    public void Deconstruct(out int x, out int y) => (x, y) = (X, Y);
        //}

        static string Classify(Point point) => point switch
        {
            (0, 0) => "Origin",
            (1, 0) => "positive X basis end",
            (0, 1) => "positive Y basis end",
            _ => "Just a point",
        };

        static decimal GetGroupTicketPriceDiscount(int groupSize, DateTime visitDate)
    => (groupSize, visitDate.DayOfWeek) switch
    {
        ( <= 0, _) => throw new ArgumentException("Group size must be positive."),
        (_, DayOfWeek.Saturday or DayOfWeek.Sunday) => 0.0m,
        ( >= 5 and < 10, DayOfWeek.Monday) => 20.0m,
        ( >= 10, DayOfWeek.Monday) => 30.0m,
        ( >= 5 and < 10, _) => 12.0m,
        ( >= 10, _) => 15.0m,
        _ => 0.0m,
    };

        public record Point2D(int X, int Y);
        public record Point3D(int X, int Y, int Z);

        static string PrintIfAllCoordinatesArePositive(object point) => point switch
        {
            Point2D(> 0, > 0) p => p.ToString(),
            Point3D(> 0, > 0, > 0) p => p.ToString(),
            _ => string.Empty,
        };

        private static void PositionalPattern()
        {
            Point p = new Point(0, 0);
            Console.WriteLine(Classify(p));
            Console.WriteLine(Classify(new Point(1, 0)));

            GetGroupTicketPriceDiscount(6, new DateTime(2023, 10, 12));

            PrintIfAllCoordinatesArePositive(new Point2D(10, 20));
            PrintIfAllCoordinatesArePositive(new Point3D(10, 20, 30));
            PrintIfAllCoordinatesArePositive(new Point3D(0, 0, 0));
            PrintIfAllCoordinatesArePositive(new Point3D(0, 20, 30));
        }
        #endregion


        public static decimal GetGroupTicketPrice(int visitorCount) => visitorCount switch
        {
            1 => 12.0m,
            2 => 20.0m,
            3 => 27.0m,
            4 => 32.0m,
            0 => 0.0m,
            _ => throw new ArgumentException($"Not supported number of visitors: {visitorCount}", nameof(visitorCount)),
        };

        #region where (generic type constraint)
        public class UsingEnum<T> where T : System.Enum { }

        public class UsingDelegate<T> where T : System.Delegate { }

        public class Multicaster<T> where T : System.MulticastDelegate { }
        class MyClass<T, U> where T : class where U : struct { }
        public abstract class B
        {
            public void M<T>(T? item) where T : struct { }
            public abstract void M<T>(T? item);

        }
        #endregion
    }

    public abstract class Vehicle { public int MyProperty { get; set; } }
    public class Car : Vehicle { }
    public class Truck : Vehicle { }

    public static class TollCalculator
    {
        public static decimal CalculateToll(this Vehicle vehicle) => vehicle switch
        {
            Car _ => 2.00m,
            Truck _ => 7.50m,
            null => throw new ArgumentNullException(nameof(vehicle)),
            _ => throw new ArgumentException("Unknown type of a vehicle", nameof(vehicle)),
        };
    }
}
