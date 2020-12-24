using GoogleMeetLogsNavigator.GoogleReader;
using GoogleMeetLogsNavigator.Interface;
using GoogleMeetLogsNavigator.TO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //ICSVReader<GoogleMeetingTO> reader = new GoogleMeetCSVReader(new System.IO.StreamReader(@"C:\Users\Fedele Simone De Feo\Downloads\meet_logs_1608390927280.csv"));

            TimeSpan span = new TimeSpan(0, 0, 100);

            Console.WriteLine(span);

            Console.ReadKey();
         
        }
    }
}
