using GoogleMeetLogsNavigator.BO;
using GoogleMeetLogsNavigator.GoogleParser.Interface;
using GoogleMeetLogsNavigator.GoogleParser.nteface;
using GoogleMeetLogsNavigator.GoogleParser.Parser;
using GoogleMeetLogsNavigator.Utility;
using GoogleMeetLogsNavigator.TransferObject;
using GoogleMeetLogsNavigator.TransferObject.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            bool prodottoDaExcel = true;
            string delimiter = ",";
            if (prodottoDaExcel)
            {
                delimiter = ";";
            }
            ICSVReader<GoogleMeetingTO> reader = new GoogleMeetCSVReader(new StreamReader(@"C:\Users\Fedele Simone De Feo\Desktop\GMAnonimoMenoColonne.csv"), delimiter);
            GoogleMeetMissingDataCalculator dataCalculator = new GoogleMeetMissingDataCalculator(reader);

            List<IGoogleMeetLogTO> logs = new List<IGoogleMeetLogTO>();
            foreach(var pair in dataCalculator.MeetingLogsDictionary)
            {
                
                logs.AddRange(pair.Value.Select(i => i.MapObjectModelInTransferObjectITA()).ToList().Cast<IGoogleMeetLogTO>());
                
                Console.WriteLine(pair.Key + ": " + pair.Value.Count);

            }


            ICSVWriter<IGoogleMeetLogTO> writer = new GoogleMeetCSVWriter(reader.CSVTextEncoding);
            string s = writer.ToGoogleMeetCsv(logs);

            
            Console.WriteLine();
            using (FileStream fs = File.Create(@"C:\Users\Fedele Simone De Feo\Desktop\GMAnonimo3.csv"))
            {
                byte[] info = reader.CSVTextEncoding.GetBytes(s);
                fs.Write(info, 0, info.Length);
            }


            Console.ReadKey();

        }
    }
}
