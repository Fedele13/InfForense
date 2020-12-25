using CsvHelper;
using GoogleMeetLogsNavigator.BO;
using GoogleMeetLogsNavigator.GoogleParser;
using GoogleMeetLogsNavigator.Interface;
using GoogleMeetLogsNavigator.TO;
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
            ICSVReader<GoogleMeetingTO> reader = new GoogleMeetCSVReader(new System.IO.StreamReader(@"C:\Users\Fedele Simone De Feo\Desktop\meet_logs_1608390927280.csv"));
            GoogleMeetingDataCalculator dataCalculator = new GoogleMeetingDataCalculator(reader);



            foreach (var pair in reader.MeetingDictionary)
            {
                Console.WriteLine(pair.Value);
            }

           
            Console.ReadKey();
        }
    }
}
