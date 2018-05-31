using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
