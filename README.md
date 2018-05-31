
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GsmDemo
{
    public class Call
    {
        public const double MinDuration = 0;
        public const double MaxDuration = 120;

        private static decimal _pricePerMinute;
        public static decimal PricePerMinute
        {
            get { return _pricePerMinute; }
            set
            {
                if (value >= 0)
                {
                    _pricePerMinute = value;
                }
            }
        }

        public Gsm Caller { get; private set; }
        public Gsm Receiver { get; private set; }

        private double _duration;
        public double Duration
        {
            get { return this._duration; }
            set
            {
                if (IsValidDuration(value))
                {
                    this._duration = value;
                }
            }
        }

        public Call(Gsm caller, Gsm receiver, double duration)
        {
            this.Caller = caller;

            this.Receiver = receiver;

            this.Duration = duration;
        }

        public void PrintInfo()
        {
            Console.WriteLine("Duration: " + Duration);
            Console.WriteLine("Caller number:" + Caller.SimMobileNumber);
            Console.WriteLine("Receiver number:" + Receiver.SimMobileNumber);
        }

        public static bool IsValidDuration(double duration)
        {
            if (duration >= MinDuration && duration <= MaxDuration)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

namespace GsmDemo
{
    public class Gsm
    {
        public string Model { get; private set; }

        public bool HasSimCard { get; private set; }

        public string SimMobileNumber { get; private set; }

        public double OutgoingCallsDuration { get; private set; }

        public Call LastIncomingCall { get; private set; }

        public Call LastOutgoingCall { get; private set; }

        public Gsm(string model)
        {
            this.Model = model;
        }

        public decimal SumForCalls
        {
            get { return Call.PricePerMinute * (decimal)OutgoingCallsDuration; }
        }

        public void RemoveSimCard()
        {
            this.HasSimCard = false;
            this.SimMobileNumber = null;
        }

        public void InsertSimCard(string simMobileNumber)
        {
            if (this.IsValidNumber(simMobileNumber))
            {
                this.SimMobileNumber = simMobileNumber;
                this.HasSimCard = true;
            }
            else
            {
                Console.WriteLine("Invalid SIM number: " + simMobileNumber);
            }
        }

        private bool IsValidNumber(string simMobileNumber)
        {
            if (simMobileNumber.Length != 10 || !simMobileNumber.StartsWith("08"))
            {
                return false;
            }

            for (int i = 0; i < simMobileNumber.Length; i++)
            {
                if(!Char.IsDigit(simMobileNumber[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public void MakeCall(Gsm receiver, double duration)
        {
            if (Call.IsValidDuration(duration)
                && this.HasSimCard && receiver.HasSimCard
                && this.SimMobileNumber != receiver.SimMobileNumber)
            {
                Call currentCall = new Call(this, receiver, duration);
                this.LastOutgoingCall = currentCall;

                receiver.LastIncomingCall = currentCall;
                this.OutgoingCallsDuration += duration;
            }
            else
            {
                Console.WriteLine("Cannot make call!");
            }
        }

        public void PrintInfoForTheLastOutgoingCall()
        {
            if (this.LastOutgoingCall != null)
            {
                Console.WriteLine("Last outgoing call info:");
                this.LastOutgoingCall.PrintInfo();
            }
            else
            {
                Console.WriteLine("No outgoing calls yet.");
            }
        }

        public void PrintInfoForTheLastIncomingCall()
        {
            if (this.LastIncomingCall != null)
            {
                Console.WriteLine("Last incoming call info:");
                this.LastIncomingCall.PrintInfo();
            }
            else
            {
                Console.WriteLine("No incoming calls yet!");
            }
        }
    }
}


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
