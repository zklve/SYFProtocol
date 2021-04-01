using SYFProtocol;
using System;

namespace UnitTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Sample.ModbusTcpMasterReadInputs();

            Console.ReadLine();
        }
    }
}
