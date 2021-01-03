using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using GoogleMeetLogsNavigator.GoogleParser.Parser;
using GoogleMeetLogsNavigator.BO;
using GoogleMeetLogsNavigator.Model;
using GoogleMeetLogsNavigator.Utility;
using GoogleMeetLogsNavigator.TransferObject.Interface;
using GoogleMeetLogsNavigator.GoogleParser.GoogleEnum;
using GoogleMeetLogsNavigator.GoogleParser.nteface;
using System.Text;
using static System.Windows.Forms.CheckedListBox;
using System.Drawing;
using System.Configuration;
using System.Threading;
using CsvHelper.Configuration.Attributes;

namespace GoogleMeetLogsNavigator
{

    public partial class Form3 : Form
    {
        DataTable dt = new DataTable();
        LogItem logItem;
        String selectComboItem;
        string csvDelimiter = ",";
        private IList<string> columns;
        private IList<string> mandatoryColumns = new List<string> { CSVHeaderEnum.Date.ToString(), CSVHeaderEnum.EventName.ToString(),
            CSVHeaderEnum.EventDescription.ToString(), CSVHeaderEnum.MeetingCode.ToString(),CSVHeaderEnum.PartecipantIdentifier.ToString(),
            CSVHeaderEnum.ExternalPartecipantIdentifier.ToString(), CSVHeaderEnum.Duration.ToString(),CSVHeaderEnum.PartecipantName.ToString()
            ,CSVHeaderEnum.ClientType.ToString()
        };
        private IList<GoogleMeetLogModel> listLog;
        private string _supportedLanguage = "it";
        private Encoding csvEncoding = Encoding.UTF8;
        public Form3()
        {
            InitializeComponent();
            text_search.Visible = false;
            dataGridView1.Visible = false;
            listBox2.Visible = false;
            comboBox1.Visible = false;
            checkedListBox1.Visible = false;
            button_filter.Visible = false;
            label2.Visible = false;
            exportToolStripMenuItem.Enabled = false;
            listBox1.DisplayMember = "Title";
            listBox1.ValueMember = "Path";
            listBox2.DisplayMember = "codiceRiunione";
            listBox2.ValueMember = "logListModel";
            comboBox1.SelectedItem = "All";
            comboBox1.SelectedText = "All";
            logItem = new LogItem();
           
            columns = typeof(GoogleMeetLogModel).GetProperties().Select(item => item.Name).ToList();
         
            foreach (var value in columns)
            {
                comboBox1.Items.Add(value);
                dt.Columns.Add(value, typeof(string));
                checkedListBox1.Items.Add(value);
                if (mandatoryColumns.Contains(value))
                {
                    checkedListBox1.SetItemChecked(checkedListBox1.Items.IndexOf(value), true);
                }
            }
            var appSettings = ConfigurationManager.AppSettings;
            if (appSettings.Count > 0)
            {
                _supportedLanguage = string.IsNullOrEmpty(appSettings["Langauge"]) ? _supportedLanguage : appSettings["Langauge"];
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
            try
            {
                text_search.Visible = false;
                dataGridView1.Visible = false;
                listBox2.Visible = false;
                comboBox1.Visible = false;
                checkedListBox1.Visible = false;
                button_filter.Visible = false;
                label2.Visible = false;
                exportToolStripMenuItem.Enabled = false;
                label1.Text = "Files";
                listBox2.Items.Clear();
                listBox1.Items.Clear();
                FolderBrowserDialog FBD = new FolderBrowserDialog();
                if (FBD.ShowDialog() == DialogResult.OK)
                {
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void fILEToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                label1.Text = "Meeting codes";
                listBox2.Visible = true;

                var selectedItems = listBox1.SelectedItems.Cast<FileItem>();
                var all = string.Join(Environment.NewLine, selectedItems.Select(x => x.Path));
                using (Form1 form1 = new Form1())
                {
                    form1.ShowDialog();
                    csvDelimiter = form1.Delimitator;
                }
                StreamReader streamCsv = new StreamReader(all);
                GoogleMeetCSVReader gcsvr = new GoogleMeetCSVReader(streamCsv, csvDelimiter, _supportedLanguage);
                csvEncoding = gcsvr.CSVTextEncoding;
                var gmdc = new GoogleMeetMissingDataCalculator(gcsvr);
                IDictionary<string, IList<GoogleMeetLogModel>> dicLog = gmdc.MeetingLogsDictionary;
                IList<GoogleMeetLogModel> listAllMeeting = new List<GoogleMeetLogModel>();
                foreach (KeyValuePair<string, IList<GoogleMeetLogModel>> kvp in dicLog)
                {
                    var logitem = new LogItem { codiceRiunione = kvp.Key, logListModel = kvp.Value };
                    foreach (var val in kvp.Value)
                    {
                        listAllMeeting.Add(val);
                    }
                    listBox2.Items.Add(logitem);
                }
                var logAllItem = new LogItem { codiceRiunione = "AllMeeting", logListModel = listAllMeeting };
                listBox2.Items.Add(logAllItem);
               
            }
            catch (Exception ex)
            {
                label1.Text = "Files";
                MessageBox.Show(ex.Message);
            }
        }

        private void aBOUTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new About_form();
            frm.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataView dv = dt.DefaultView;
                String like = " LIKE '%";
                String or = "%' OR ";
                String columnName = "";
                if (!comboBox1.SelectedItem.Equals("All"))
                {
                    dv.RowFilter = selectComboItem + " LIKE '%" + text_search.Text + "%'";
                }
                else
                {
                    foreach (var value in columns)
                    {
                        String comboItem = value;
                        String lastElement = columns.Last();
                        if (value.Equals(lastElement))
                        {
                            columnName = columnName + comboItem + like + text_search.Text + "%' ";
                        }
                        else
                        {
                            columnName = comboItem + like + text_search.Text + or + columnName;
                        }

                    }
                    dv.RowFilter = columnName;
                }

                dataGridView1.DataSource = dv;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                checkedListBox1.Visible = true;
                dataGridView1.Visible = true;
                comboBox1.Visible = true;
                text_search.Visible = true;
                button_filter.Visible = true;
                label2.Visible = true;
                exportToolStripMenuItem.Enabled = true;
                comboBox1.SelectedItem = "All";
                logItem = (LogItem)listBox2.SelectedItem;
                listLog = (IList<GoogleMeetLogModel>)logItem.logListModel;
                dt.Rows.Clear();
                DataRow dr;
                foreach (GoogleMeetLogModel model in listLog)
                {
                    dr = dt.NewRow();
                    foreach (var value in columns)
                    {
                        dr[value] = model.GetType().GetProperty(value).GetValue(model);
                    }
                    dt.Rows.Add(dr);

                    /*
                    dt.Rows.Add(model.Date.ConvertGoogleDateTimeInString(model.CommonEuropeanTimeType), model.EventName, model.EventDescription, model.MeetingCode, model.PartecipantIdentifier, model.ExternalPartecipantIdentifier, model.ClientType
                        , model.MeetingOwnerEmail, model.ProductType, model.Duration, model.CallEvaluationOn5, model.PartecipantName, model.IPAddress,
                        model.City, model.Nation, model.ActionCause, model.ActionDescription, model.VisualizedDestinationName, model.DestinationEmailsAddresses,
                        model.DestinationPhoneNumber, model.CalendarEventIdentifier, model.ConferenceID, model.NETRoundTrip, model.TransportProtocol,
                        model.PredictedBandWidthLoading, model.PredictedBandWidthUploading, model.MaxReceptionAudioPacketsLost, model.AverageReceptionAudioPacketsLost,
                        model.AudioReceptionDuration, model.BitRatioAudioSending, model.MaxSendingAudioPacketsLost, model.AverageSendingAudioPacketsLost, model.AudioSendingDuration, model.AverageReceptionFlickering,
                        model.MaxReceptionFilckering, model.AverageSendingFlickering, model.BitRatioScreencastReception, model.AverageScreecastReception, model.LongSideMedianScreencastReception,
                        model.MaxReceptionScreencastPacketsLost, model.AverageReceptionScreencastPacketsLost, model.ScreencastReceptionDuration, model.ShortSideMedianScreencastReception,
                        model.BitRatioScreencastSending, model.AverageScreecastSending, model.LongSideMedianScreencastSending, model.MaxSendingScreencastPacketsLost,
                        model.AverageSendingScreencastPacketsLost, model.ScreencastSendingDuration, model.ShortSideMedianScreencastSending, model.AverageVideoReception, model.LongSideMedianVideoReception,
                        model.MaxVideoReceptionPacketsLost, model.AverageVideoReceptionPacketsLost, model.ReceptionVideoDuration, model.ShortSideMedianVideoReception,
                        model.BitRatioVideoSending, model.AverageVideoSending, model.LongSideMedianVideoSending, model.MaxSendingVideoPacketsLost, model.AverageSendingVideoPacketsLost,
                        model.VideoSendingDuration, model.ShortSideMedianVideoSending, model.NetworkCongestion, model.MeetingStartDate, model.MeetingEndDate, model.MeetingEnteringDate,
                        model.TotalMeetingUserPartecipation, model.CommonEuropeanTimeType);
                    */

                }
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectComboItem = comboBox1.SelectedItem.ToString();
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button_filter_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                IDictionary<CSVHeaderEnum, bool> exportConfiguration = new Dictionary<CSVHeaderEnum, bool>();
                if (checkedListBox1.CheckedItems.Count > 0)
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
                    exportConfiguration = GoogleMeetCSVWriter.GetDefaultConfiguration();
                }
                using (Form1 form1 = new Form1())
                {
                    form1.ShowDialog();
                    csvDelimiter = form1.Delimitator;
                }
                IList<IGoogleMeetLogTO> logs = listLog.Select(i => i.MapObjectModelInTransferObjectITA()).Cast<IGoogleMeetLogTO>().ToList();
                ICSVWriter<IGoogleMeetLogTO> writer = new GoogleMeetCSVWriter(exportConfiguration, csvEncoding, csvDelimiter, _supportedLanguage);
                string s = writer.ToGoogleMeetCsv(logs);
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "CSV files (*.csv)|*.csv";
                saveFileDialog1.Title = "Save file";
                String directory = saveFileDialog1.InitialDirectory;
                DialogResult result = saveFileDialog1.ShowDialog();

                if (result == DialogResult.OK)
                {
                    bool written = false;
                    Thread cursorThread = new Thread(() =>
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        while (!written) ;
                        Cursor.Current = Cursors.Default;
                    });
                    cursorThread.Start();
                    using (Stream stream = File.Open(saveFileDialog1.FileName, FileMode.Create))
                    using (StreamWriter sw = new StreamWriter(stream))
                    {
                        sw.Write(s);
                        written = true;
                    }
                    cursorThread.Abort();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        void listBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            ListBox list = (ListBox)sender;
            if (e.Index > -1)
            {
                object item = list.Items[e.Index];
                e.DrawBackground();
                e.DrawFocusRectangle();
                Brush brush = new SolidBrush(e.ForeColor);
                SizeF size = e.Graphics.MeasureString(item.ToString(), e.Font);
                e.Graphics.DrawString(item.ToString(), e.Font, brush, e.Bounds.Left + (e.Bounds.Width / 2 - size.Width / 2), e.Bounds.Top + (e.Bounds.Height / 2 - size.Height / 2));
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
