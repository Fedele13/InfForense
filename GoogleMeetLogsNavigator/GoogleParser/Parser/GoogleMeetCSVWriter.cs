using CsvHelper;
using GoogleMeetLogsNavigator.GoogleParser.Enum;
using GoogleMeetLogsNavigator.GoogleParser.nteface;
using GoogleMeetLogsNavigator.Model;
using GoogleMeetLogsNavigator.Utility;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GoogleMeetLogsNavigator.GoogleParser.Parser
{
    /// <summary>
    /// 
    /// </summary>
    public class GoogleMeetCSVWriter : ICSVWriter<GoogleMeetLogModel>
    {
      
        /// <summary>
        /// 
        /// </summary>
        private IDictionary<CSVHeaderEnum, bool> _configurationDictionary = null;

        public GoogleMeetCSVWriter(IDictionary<CSVHeaderEnum, bool> configurationDictionary)
        {
            this._configurationDictionary = configurationDictionary;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configurationDictionary"></param>
        public void SetConfiguration(IDictionary<CSVHeaderEnum, bool> configurationDictionary)
        {
            this._configurationDictionary = configurationDictionary;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logs"></param>
        public string ToGoogleMeetCsv(IList<GoogleMeetLogModel> logs)
        {
            using (var mem = new MemoryStream())
            using (var writer = new StreamWriter(mem))
            using (var csvWriter = new CsvWriter(writer, System.Globalization.CultureInfo.InvariantCulture))
            {
                csvWriter.Configuration.SanitizeForInjection = true;
                csvWriter.Configuration.Delimiter = ",";
                
                if (this._configurationDictionary[CSVHeaderEnum.ExternalPartecipantIdentifier])
                    csvWriter.WriteField(Constants.CSVHeader.ExternalPartecipantIdentifier);

                if (this._configurationDictionary[CSVHeaderEnum.ExternalPartecipantIdentifier])
                    csvWriter.WriteField(Constants.CSVHeader.ExternalPartecipantIdentifier);





                //qua posso scegliere i field da considerare
                /*
                 * csvWriter.WriteField("Customer");
				
				csvWriter.WriteField("Deadline");
				csvWriter.NextRecord();

				foreach (var project in data)
				{
					csvWriter.WriteField(project.CustomerName);
					csvWriter.WriteField(project.Title);
					csvWriter.WriteField(project.Deadline);
					csvWriter.NextRecord();
				}
                 */

                return Encoding.UTF8.GetString(mem.ToArray());
            }
        }

        private IDictionary<CSVHeaderEnum, bool> getDefaultConfiguration()
        {
            return new Dictionary<CSVHeaderEnum, bool>();
        }
    }
}
