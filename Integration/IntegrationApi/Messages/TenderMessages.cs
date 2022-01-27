using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;

namespace IntegrationApi.Messages
{
    public static class TenderMessages
    {
        public static readonly string InvalidTenderName = "Invalid tender name.";
        public static readonly string InvalidDateRange = "Start date must be before end date.";
        public static readonly string InvalidMedicineList = "No medicine requests in list.";
        public static readonly string InvalidMedicineName = "Medicine name required.";

        public static string InvalidQuantity(string medicineName)
        {
            return "Invalid quantity for " + medicineName + ".";
        }

        public static readonly string FailedToSendTender = "Error while sending tender via rabbitmq!";
        public static readonly string Created = "Tender created and sent";
        public static readonly string FailedToSendClosedTender = "Error while sending closed tender via rabbitmq!";
        public static readonly string Winner = "Winner chosen, tender completed";
        public static readonly string NotFound = "Tender does not exist";
        public static readonly string AlreadyClosed = "Tender already closed!";
        public static readonly string Closed = "Tender closed.";
    }
}
