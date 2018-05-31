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
