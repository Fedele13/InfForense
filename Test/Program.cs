using CsvHelper;
using GoogleMeetLogsNavigator.BO;
using GoogleMeetLogsNavigator.GoogleParser.Interface;
using GoogleMeetLogsNavigator.GoogleParser.nteface;
using GoogleMeetLogsNavigator.GoogleParser.Parser;
using GoogleMeetLogsNavigator.Model;
using GoogleMeetLogsNavigator.TransferObject;
using GoogleMeetLogsNavigator.TransferObject.Interface;
using GoogleMeetLogsNavigator.Utility;
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

            IList<IGoogleMeetLogTO> logs = new List<IGoogleMeetLogTO>();
            foreach(var pair in dataCalculator.MeetingLogsDictionary)
            {
                foreach (GoogleMeetLogModel log in pair.Value)
                {
                    logs.Add(log.MapObjectModelInTransferObjectITA());
                }
                Console.WriteLine(pair.Key + ": " + pair.Value.Count);

            }



            ICSVWriter<IGoogleMeetLogTO> writer = new GoogleMeetCSVWriter();
            string s = writer.ToGoogleMeetCsv(logs.ToList());
            Console.WriteLine();
            using (FileStream fs = File.Create(@"C:\Users\Fedele Simone De Feo\Desktop\GMAnonimo2.csv"))
            {
                byte[] info = new UTF8Encoding(true).GetBytes(s);
                fs.Write(info, 0, info.Length);
            }


            Console.ReadKey();

        }
    }
}
