using CsvHelper;
using GoogleMeetLogsNavigator.GoogleParser.GoogleEnum;
using GoogleMeetLogsNavigator.GoogleParser.nteface;
using GoogleMeetLogsNavigator.Model;
using GoogleMeetLogsNavigator.TransferObject.Interface;
using GoogleMeetLogsNavigator.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GoogleMeetLogsNavigator.GoogleParser.Parser
{
    /// <summary>
    /// 
    /// </summary>
    public class GoogleMeetCSVWriter : ICSVWriter<IGoogleMeetLogTO>
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
        public string ToGoogleMeetCsv(IList<IGoogleMeetLogTO> logs)
        {
            using (var mem = new MemoryStream())
            using (var writer = new StreamWriter(mem))
            using (var csvWriter = new CsvWriter(writer, System.Globalization.CultureInfo.InvariantCulture))
            {
                csvWriter.Configuration.SanitizeForInjection = true;
                csvWriter.Configuration.Delimiter = ",";
                

                csvWriter.WriteField(Constants.CSVHeader.Date);
                csvWriter.WriteField(Constants.CSVHeader.EventName);
                csvWriter.WriteField(Constants.CSVHeader.EventDescription);
                csvWriter.WriteField(Constants.CSVHeader.MeetingCode);
                csvWriter.WriteField(Constants.CSVHeader.PartecipantIdentifier);
                csvWriter.WriteField(Constants.CSVHeader.ExternalPartecipantIdentifier);
                //csvWriter.WriteField(Constants.CSVHeader.)


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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IDictionary<CSVHeaderEnum, bool> getDefaultConfiguration()
        {
            IDictionary<CSVHeaderEnum, bool> defaultConfigurationDictionary = new Dictionary<CSVHeaderEnum, bool>();

            for (int headerEnum = 7; headerEnum < Enum.GetValues(typeof(CSVHeaderEnum)).Length; ++headerEnum)
            {
                defaultConfigurationDictionary.Add((CSVHeaderEnum)headerEnum, true);
            }
            return defaultConfigurationDictionary;
        }
    }
}
