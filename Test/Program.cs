using GoogleMeetLogsNavigator.GoogleReader;
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
            GoogleCSVReader reader = new GoogleCSVReader(new System.IO.StreamReader(@"C:\Users\Fedele Simone De Feo\Downloads\meet_logs_1608390927280.csv"));
        }
    }
}
