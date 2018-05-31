using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GsmDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Call.PricePerMinute = 10;

            Gsm mishoGsm = new Gsm("Samsung");
            mishoGsm.InsertSimCard("0893456789");
            Gsm svetlioGsm = new Gsm("NOKIA");
            svetlioGsm.InsertSimCard("0893456780");

            mishoGsm.MakeCall(svetlioGsm, 10);

            Console.WriteLine(mishoGsm.SumForCalls);
            Console.WriteLine(svetlioGsm.SumForCalls);

            mishoGsm.PrintInfoForTheLastIncomingCall();
            mishoGsm.PrintInfoForTheLastOutgoingCall();
            svetlioGsm.PrintInfoForTheLastIncomingCall();
            svetlioGsm.PrintInfoForTheLastOutgoingCall();

            Console.ReadKey();
        }
    }
}
