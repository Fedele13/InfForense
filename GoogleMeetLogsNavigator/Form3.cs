using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using GoogleMeetLogsNavigator.GoogleParser.Parser;
using GoogleMeetLogsNavigator.BO;
using GoogleMeetLogsNavigator.Model;
using GoogleMeetLogsNavigator.Utility;
using GoogleMeetLogsNavigator.TransferObject.Interface;
using GoogleMeetLogsNavigator.GoogleParser.GoogleEnum;
using GoogleMeetLogsNavigator.GoogleParser.nteface;

namespace GoogleMeetLogsNavigator
{

    public partial class Form3 : Form
    {
        DataTable dt = new DataTable();
        LogItem logItem;
        String selectComboItem;
        private IList<string> columns;
        private IList<GoogleMeetLogModel> listLog;
        public Form3()
        {
            InitializeComponent();
            text_search.Visible = false;
            dataGridView1.Visible = false;
            listBox1.DisplayMember = "Title";
            listBox1.ValueMember = "Path";
            listBox2.DisplayMember = "codiceRiunione";
            listBox2.ValueMember = "logListModel";
            listBox2.Visible = false;
            logItem = new LogItem();
            comboBox1.Visible = false;
            checkedListBox1.Visible = false;
            button_filter.Visible = false;
            label2.Visible = false;

            columns = typeof(GoogleMeetLogModel).GetProperties().Select(item => item.Name).ToList();
            foreach (var value in columns)
            {
                dt.Columns.Add(value, typeof(string));
            }

        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            
            if (FBD.ShowDialog() == DialogResult.OK)
            {

                listBox1.Items.Clear();
                string[] files = Directory.GetFiles(FBD.SelectedPath);
                
                foreach (string file in files)
                {
                    if (Path.GetExtension(file).Equals(".csv"))
                    {
                        var fileItem = new FileItem { Title = Path.GetFileName(file), Path = Path.GetFullPath(file) };
                        listBox1.Items.Add(fileItem);
                    }
                    
                   
                }
                
            }
        }

        private void fILEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
          
            var selectedItems = listBox1.SelectedItems.Cast<FileItem>();
            var all = string.Join(Environment.NewLine, selectedItems.Select(x => x.Path));
            string csvDelimiter = ";";
            StreamReader streamCsv = new StreamReader(all);
            GoogleMeetCSVReader gcsvr = new GoogleMeetCSVReader(streamCsv, csvDelimiter);
            var gmdc = new GoogleMeetMissingDataCalculator(gcsvr);
            IDictionary<string, IList<GoogleMeetLogModel>> dicLog = gmdc.MeetingLogsDictionary;

            foreach (KeyValuePair<string, IList<GoogleMeetLogModel>> kvp in dicLog)
            {
                var logitem = new LogItem { codiceRiunione = kvp.Key, logListModel = kvp.Value};
                listBox2.Items.Add(logitem);
            }
            listBox2.Visible = true;

        }

        private void aBOUTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new About_form();
            frm.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DataView dv = dt.DefaultView;
            /*foreach (var value in Enum.GetValues(typeof(comboColumn)))
            {
                //String columnName = ((comboColumn)value).ToString();
               
            }*/
            String columnName=",";
            if (!selectComboItem.Equals("All"))
            {
                dv.RowFilter = selectComboItem + " LIKE '%" + text_search.Text + "%'";
            }
            else
            {
                foreach (var value in columns)
                {
                    String comboItem = value;
                    columnName = comboItem + columnName;
                }
                dv.RowFilter = columnName + " LIKE '%" + text_search.Text + "%'";
            }
           
            dataGridView1.DataSource = dv;
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            checkedListBox1.Visible = true;
            dataGridView1.Visible = true;
            comboBox1.Visible = true;
            text_search.Visible = true;
            button_filter.Visible = true;
            label2.Visible = true;
            logItem = (LogItem)listBox2.SelectedItem;

            listLog = (IList<GoogleMeetLogModel>)logItem.logListModel;
            //dataGridView1.DataSource = list;
            dt.Rows.Clear();

            foreach (GoogleMeetLogModel model in listLog)
            {
               
                    dt.Rows.Add(model.Date.ConvertGoogleDateTimeInString(model.CommonEuropeanTimeType), model.EventName,model.EventDescription,model.MeetingCode,model.PartecipantIdentifier, model.ExternalPartecipantIdentifier, model.ClientType
                        , model.MeetingOwnerEmail, model.ProductType, model.Duration, model.CallEvaluationOn5, model.PartecipantName,model.IPAddress,
                        model.City,model.Nation,model.ActionCause,model.ActionDescription,model.VisualizedDestinationName,model.DestinationEmailsAddresses,
                        model.DestinationPhoneNumber,model.CalendarEventIdentifier,model.ConferenceID,model.NETRoundTrip,model.TransportProtocol,
                        model.PredictedBandWidthLoading,model.PredictedBandWidthUploading,model.MaxReceptionAudioPacketsLost,model.AverageReceptionAudioPacketsLost,
                        model.AudioReceptionDuration,model.BitRatioAudioSending,model.MaxSendingAudioPacketsLost,model.AverageSendingAudioPacketsLost,model.AudioSendingDuration,model.AverageReceptionFlickering,
                        model.MaxReceptionFilckering,model.AverageSendingFlickering,model.BitRatioScreencastReception,model.AverageScreecastReception,model.LongSideMedianScreencastReception,
                        model.MaxReceptionScreencastPacketsLost,model.AverageReceptionScreencastPacketsLost,model.ScreencastReceptionDuration,model.ShortSideMedianScreencastReception,
                        model.BitRatioScreencastSending,model.AverageScreecastSending,model.LongSideMedianScreencastSending,model.MaxSendingScreencastPacketsLost,
                        model.AverageSendingScreencastPacketsLost,model.ScreencastSendingDuration,model.ShortSideMedianScreencastSending,model.AverageVideoReception,model.LongSideMedianVideoReception,
                        model.MaxVideoReceptionPacketsLost,model.AverageVideoReceptionPacketsLost,model.ReceptionVideoDuration,model.ShortSideMedianVideoReception,
                        model.BitRatioVideoSending,model.AverageVideoSending,model.LongSideMedianVideoSending,model.MaxSendingVideoPacketsLost,model.AverageSendingVideoPacketsLost,
                        model.VideoSendingDuration,model.ShortSideMedianVideoSending,model.NetworkCongestion,model.MeetingStartDate,model.MeetingEndDate,model.MeetingEnteringDate,
                        model.TotalMeetingUserPartecipation,model.CommonEuropeanTimeType);
               
            }
            dataGridView1.DataSource = dt;
           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectComboItem = comboBox1.SelectedItem.ToString();
           /* foreach (var value in Enum.GetValues(typeof(comboColumn)))
            {
                String comboItem = ((comboColumn)value).ToString();
                if (!selectedItem.Equals("All"))
                {
                    this.dataGridView1.Columns[comboItem].Visible = false;
                    this.dataGridView1.Columns[selectedItem].Visible = true;
                }
                else
                {
                    if (selectedItem.Equals("All"))
                    {
                        this.dataGridView1.Columns[comboItem].Visible = true;
                    }
                    
                }
                
            }*/
            
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button_filter_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.CheckedItems.Count > 0)
            {
                foreach (var value in columns)
                {
                    String comboItem = value;
                    this.dataGridView1.Columns[comboItem].Visible = false;
                    foreach (String s in checkedListBox1.CheckedItems)
                    {
                        this.dataGridView1.Columns[s].Visible = true;
                    }
                }
            }
            else
            {
                
                foreach (var value in columns)
                {

                    String comboItem = value;
                    this.dataGridView1.Columns[comboItem].Visible = true;

                }
                
            }
            
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IDictionary<CSVHeaderEnum, bool> exportConfiguration = new Dictionary<CSVHeaderEnum, bool>();
            if(checkedListBox1.CheckedItems.Count > 0)
            {
                foreach (string columnSelected in checkedListBox1.CheckedItems)
                {
                    foreach (CSVHeaderEnum item in Enum.GetValues(typeof(CSVHeaderEnum)))
                    {
                        if (columnSelected == item.ToString())
                        {
                            exportConfiguration.Add(item, true);
                            break;
                        }
                        
                    }
                }
            }
            else
            {
                exportConfiguration=GoogleMeetCSVWriter.GetDefaultConfiguration();
            }
            
            
            IList<IGoogleMeetLogTO> logs = listLog.Select(i => i.MapObjectModelInTransferObjectITA()).Cast<IGoogleMeetLogTO>().ToList(); 
            ICSVWriter<IGoogleMeetLogTO> writer = new GoogleMeetCSVWriter(exportConfiguration);
            string s = writer.ToGoogleMeetCsv(logs);
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "CSV files (*.csv)|*.csv";
            saveFileDialog1.Title = "Save file";
            String directory = saveFileDialog1.InitialDirectory;
            DialogResult result = saveFileDialog1.ShowDialog();
            
            if (result == DialogResult.OK)
            {
                using (Stream stream = File.Open(saveFileDialog1.FileName, FileMode.Create))
                using(StreamWriter sw = new StreamWriter(stream))
                {
                    sw.Write(s);
                }
                
            }
        }
    }
    public class FileItem
    {
        public string Title { get; set; }
        public string Path { get; set; }
    }

    public class LogItem
    {
        public string codiceRiunione { get; set; }
        public IList<GoogleMeetLogModel> logListModel { get; set; }
    }
}
