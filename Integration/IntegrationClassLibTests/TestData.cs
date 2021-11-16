using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integration.Model;

namespace IntegrationClassLibTests
{
    public class TestData
    {
        public static List<Receipt> GetReceiptLogs()
        {
            List<Medicine> medicines = GetMedicines();
            Receipt receiptLog1 = new Receipt
            {
                ReceiptDate = new DateTime(2021, 9, 30),
                Medicine = medicines[2],
                AmountSpent = 4
            };
            Receipt receiptLog2 = new Receipt
            {
                ReceiptDate = new DateTime(2021, 5, 19),
                Medicine = medicines[1],
                AmountSpent = 2
            };
            Receipt receiptLog3 = new Receipt
            {
                ReceiptDate = new DateTime(2021, 9, 19),
                Medicine = medicines[1],
                AmountSpent = 4
            };
            Receipt receiptLog4 = new Receipt
            {
                ReceiptDate = new DateTime(2021, 10, 19),
                Medicine = medicines[2],
                AmountSpent = 1
            };
            Receipt receiptLog5 = new Receipt
            {
                ReceiptDate = new DateTime(2021, 9, 5),
                Medicine = medicines[0],
                AmountSpent = 2
            };
            List<Receipt> allLogs = new List<Receipt>();
            allLogs.Add(receiptLog1);
            allLogs.Add(receiptLog2);
            allLogs.Add(receiptLog3);
            allLogs.Add(receiptLog4);
            allLogs.Add(receiptLog5);
            return allLogs;
        }

        public static List<Medicine> GetMedicines()
        {
            Medicine aspirin = new Medicine { Name = "Aspirin" };
            Medicine probiotik = new Medicine { Name = "Probiotik" };
            Medicine brufen = new Medicine { Name = "Brufen" };
            List<Medicine> medicines = new List<Medicine>();
            medicines.Add(aspirin);
            medicines.Add(probiotik);
            medicines.Add(brufen);
            return medicines;
        }

        public static List<TimeRange> getTimeRanges()
        {
            TimeRange september = new TimeRange
            {
                startDate = new DateTime(2021, 9, 1),
                endDate = new DateTime(2021, 10, 1)
            };
            TimeRange october = new TimeRange
            {
                startDate = new DateTime(2021, 10, 1),
                endDate = new DateTime(2021, 11, 1)
            };
            TimeRange year = new TimeRange
            {
                startDate = new DateTime(2020, 10, 1),
                endDate = new DateTime(2021, 10, 1)
            };
            List<TimeRange> retVal = new List<TimeRange>();
            retVal.Add(september);
            retVal.Add(october);
            retVal.Add(year);
            return retVal;
        }
    }
}
