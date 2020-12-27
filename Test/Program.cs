using CsvHelper;
using GoogleMeetLogsNavigator.BO;
using GoogleMeetLogsNavigator.GoogleParser.Interface;
using GoogleMeetLogsNavigator.GoogleParser.Parser;
using GoogleMeetLogsNavigator.TransferObject;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            bool fileExcel = true;
            string delimiter = ",";
            if (fileExcel)
            {
                delimiter = ";";
            }
            ICSVReader<GoogleMeetingTO> reader = new GoogleMeetCSVReader(new System.IO.StreamReader(@"C:\Users\Fedele Simone De Feo\Desktop\GMAnonimo.csv"), delimiter);
            GoogleMeetMissingDataCalculator dataCalculator = new GoogleMeetMissingDataCalculator(reader);


            foreach (var pair in reader.MeetingDictionary)
            {
                Console.WriteLine(pair.Value);
            }

           
            Console.ReadKey();
        }
    }
}
