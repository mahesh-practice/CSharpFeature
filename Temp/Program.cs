namespace Temp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            C c = new C();
            c.M("sdgfdfsd ");
        }

        partial class C
        {
            // The declaration of C.M
            public partial void M(string message);
        }

        partial class C
        {
            // The definition of C.M
            public partial void M(string message) => Console.WriteLine(message);
        }
    }
}
