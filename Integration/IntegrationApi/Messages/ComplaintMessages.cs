using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationApi.Messages
{
    public static class ComplaintMessages
    {
        public static readonly string WrongId = "Complaint with that ID not found";
        public static readonly string DidNotReceive = "Pharmacy failed to receive complaint! Try again";
        public static readonly string Received = "Complaint saved and sent to pharmacy!";
        public static readonly string NotFound = "Complaint not found";
        public static readonly string ResponseReceived = "Complaint response received!";
    }
}
