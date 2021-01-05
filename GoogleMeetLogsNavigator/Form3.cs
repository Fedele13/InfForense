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
using GoogleMeetLogsNavigator.TransferObject;

namespace GoogleMeetLogsNavigator
{

    public partial class Form3 : Form
    {
        DataTable dt = new DataTable();
        
        LogItem logItem;
        String selectComboItem;
        string csvDelimiter = ",";
        private DateTime dataPickerValueIni;
        private DateTime dataPickerValueFin;
        private IList<string> columns;
        private IList<string> mandatoryColumns = new List<string> { CSVHeaderEnum.Date.ToString(), CSVHeaderEnum.EventName.ToString(),
            CSVHeaderEnum.EventDescription.ToString(), CSVHeaderEnum.MeetingCode.ToString(),CSVHeaderEnum.PartecipantIdentifier.ToString(),
            CSVHeaderEnum.ExternalPartecipantIdentifier.ToString(), CSVHeaderEnum.Duration.ToString(),CSVHeaderEnum.PartecipantName.ToString()
            ,CSVHeaderEnum.ClientType.ToString()
        };
        private IList<GoogleMeetLogModel> actualListLog;
        private string _supportedLanguage = "it";
        private Encoding csvEncoding = Encoding.UTF8;
        private bool AllChecked = false;
        IList<string> supportedLanguages = new List<string>();
        IList<string> supportedDelimitators = new List<string>(); 
        public Form3()
        {

            InitializeComponent();
            dateTimePicker_inizio_previsto.Format = DateTimePickerFormat.Time;
            dateTimePicker_fine_previsto.Format = DateTimePickerFormat.Time;
            text_search.Visible = false;
            dataGridView1.Visible = false;
            listBox2.Visible = false;
            comboBox1.Visible = false;
            checkedListBox1.Visible = false;
            button_filter.Visible = false;
            label2.Visible = false;
            exportToolStripMenuItem.Enabled = false;
            dateTimePicker_inizio_previsto.Visible = false;
            dateTimePicker_fine_previsto.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            listBox1.DisplayMember = "Title";
            listBox1.ValueMember = "Path";
            listBox2.DisplayMember = "codiceRiunione";
            listBox2.ValueMember = "logListModel";
            comboBox1.SelectedItem = "All";
            comboBox1.SelectedText = "All";
            logItem = new LogItem();
           
            columns = typeof(GoogleMeetLogModel).GetProperties().Select(item => item.Name).ToList();
            checkedListBox1.Items.Add("All");
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

            supportedLanguages = ConfigurationManager.AppSettings["Language"]?.Split(',').ToList();
            supportedDelimitators = ConfigurationManager.AppSettings["Delimitator"]?.Split('\\').ToList();

            if (supportedDelimitators == null)
            {
                supportedDelimitators = new List<string>();
            }

            if (supportedLanguages == null)
            {
                supportedLanguages = new List<string>();
            }

            comboLanguage.Items.AddRange(supportedLanguages.ToArray());
            comboLanguage.SelectedItem = supportedLanguages.FirstOrDefault();
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
                dateTimePicker_inizio_previsto.Visible = false;
                dateTimePicker_fine_previsto.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
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
                MessageBox.Show(ex.Message,"ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void fILEToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var selectedItems = listBox1.SelectedItems.Cast<FileItem>();
                if (selectedItems == null || selectedItems.Count()==0)
                {
                    return;
                }
                label1.Text = "Meeting codes";
                listBox2.Visible = true;
                label5.Visible = false;
                //comboLanguage.Visible = false;
                var all = string.Join(Environment.NewLine, selectedItems.Select(x => x.Path));
                using (Form1 form1 = new Form1(this.supportedDelimitators))
                {
                    form1.ShowDialog();
                    csvDelimiter = form1.Delimitator;
                }
                StreamReader streamCsv = new StreamReader(all);
                GoogleMeetCSVReader gcsvr = new GoogleMeetCSVReader(streamCsv, csvDelimiter, _supportedLanguage);
                csvEncoding = gcsvr.CSVTextEncoding;
                var gmdc = new GoogleMeetMissingDataCalculator(gcsvr.MeetingDictionary, this._supportedLanguage);
                IDictionary<string, IList<GoogleMeetLogModel>> dicLog = gmdc.MeetingLogsDictionary;
                IList<GoogleMeetLogModel> listAllMeeting = new List<GoogleMeetLogModel>();
                var logAllItem = new LogItem { codiceRiunione = "AllMeeting", logListModel = listAllMeeting };
                listBox2.Items.Add(logAllItem);
                foreach (KeyValuePair<string, IList<GoogleMeetLogModel>> kvp in dicLog)
                {
                    var logitem = new LogItem { codiceRiunione = kvp.Key, logListModel = kvp.Value };
                    foreach (var val in kvp.Value)
                    {
                        listAllMeeting.Add(val);
                    }
                    listBox2.Items.Add(logitem);
                }
            }
            catch (Exception ex)
            {
                label1.Text = "Files";
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (listBox2.SelectedItem==null)
                {
                    return;
                }

                checkedListBox1.Visible = true;
                dataGridView1.Visible = true;
                comboBox1.Visible = true;
                text_search.Visible = true;
                button_filter.Visible = true;
                label2.Visible = true;
                exportToolStripMenuItem.Enabled = true;
                dateTimePicker_inizio_previsto.Visible = true;
                dateTimePicker_fine_previsto.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                comboBox1.SelectedItem = "All";
                var logItem = (LogItem)listBox2.SelectedItem;
                actualListLog = (IList<GoogleMeetLogModel>)logItem.logListModel;
                
                dt.Rows.Clear();
                DataRow dr;
               
                //if (defaultDateInizialized)
                //{
                //    dataPickerValueIni = logItem.logListModel.FirstOrDefault().MeetingStartDate;
                //    dataPickerValueFin = logItem.logListModel.FirstOrDefault().MeetingEndDate;
                //    defaultDateInizialized = false;
                //}

                dateTimePicker_inizio_previsto.Value = logItem.logListModel.FirstOrDefault().EffectiveMeetingStartDate;
                dateTimePicker_fine_previsto.Value = logItem.logListModel.FirstOrDefault().EffectiveMeetingEndDate;
                dataPickerValueIni = logItem.logListModel.FirstOrDefault().EffectiveMeetingStartDate;
                dataPickerValueFin = logItem.logListModel.FirstOrDefault().EffectiveMeetingEndDate;
                foreach (GoogleMeetLogModel model in actualListLog)
                {

                    dr = dt.NewRow();
                    foreach (var value in columns)
                    {
                        dr[value] = model.GetType().GetProperty(value).GetValue(model);
                    }

                    dt.Rows.Add(dr);

                }
                dataGridView1.DataSource = dt;

                if (checkedListBox1.CheckedItems.Count > 0)
                {
                    foreach (var value in columns)
                    {

                        this.dataGridView1.Columns[value].Visible = false;
                        foreach (string s in checkedListBox1.CheckedItems)
                        {
                            if (s == "All")
                            {
                                continue;
                            }
                            this.dataGridView1.Columns[s].Visible = true;
                        }
                    }
                }
                else
                {
                    foreach (var value in mandatoryColumns)
                    {
                        checkedListBox1.SetItemChecked(checkedListBox1.Items.IndexOf(value), true);
                        this.dataGridView1.Columns[value].Visible = true;

                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectComboItem = comboBox1.SelectedItem.ToString();
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool actuallyChecked = checkedListBox1.GetItemChecked(checkedListBox1.Items.IndexOf("All"));

            if (actuallyChecked == false && AllChecked == true)
            {
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    checkedListBox1.SetItemChecked(i, false);
                }
            }
            if(AllChecked == false && actuallyChecked == true) {
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    checkedListBox1.SetItemChecked(i, true);
                }
            }
            if(actuallyChecked == true)
            {
                bool allItemsAreChecked = true;
                
                for(int i = 0; i< checkedListBox1.Items.Count; i++)
                {
                    if (checkedListBox1.GetItemChecked(i) == false)
                    {
                        allItemsAreChecked = false;
                        break;
                    }
                }
                if(allItemsAreChecked == false)
                {
                    actuallyChecked= false;
                    checkedListBox1.SetItemChecked(checkedListBox1.Items.IndexOf("All"), false);
                }
            }
            AllChecked = actuallyChecked;
        } 

     
          
        private void button_filter_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkedListBox1.CheckedItems.Count > 0)
                {
                    foreach (var value in columns)
                    {
                        
                        this.dataGridView1.Columns[value].Visible = false;
                        foreach (string s in checkedListBox1.CheckedItems)
                        {
                            if (s == "All")
                            {
                                continue;
                            }
                            this.dataGridView1.Columns[s].Visible = true;
                        }
                    }
                }
                else
                {
                    foreach (var value in columns)
                    {
                       
                        this.dataGridView1.Columns[value].Visible = false;

                    }

                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                //using (Form1 form1 = new Form1())
                //{
                //    form1.ShowDialog();
                //    csvDelimiter = form1.Delimitator;
                //}
                IList<IGoogleMeetLogTO> logs = actualListLog.Select(i => i.MapObjectModelInTransferObject(_supportedLanguage)).Cast<IGoogleMeetLogTO>().ToList();
                ICSVWriter<IGoogleMeetLogTO> writer = new GoogleMeetCSVWriter(exportConfiguration, csvEncoding, ",", _supportedLanguage);
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
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void dateTimePicker_inizio_previsto_ValueChanged(object sender, EventArgs e)
        {
            var logItem = (LogItem)listBox2.SelectedItem;
            
            if (logItem.logListModel.Where(item => item.Date == logItem.logListModel.Select(log => log.Date).Min()).FirstOrDefault().EffectiveMeetingStartDate == dateTimePicker_inizio_previsto.Value 
                || dateTimePicker_inizio_previsto.Value == dataPickerValueIni)
            {
                return;
            }

            if(dateTimePicker_inizio_previsto.Value < logItem.logListModel.Where(item => item.Date == logItem.logListModel.Select(log => log.Date).Min()).FirstOrDefault().Date.AddHours(-12))
            {
                MessageBox.Show("Non è possibile iniziare una riunione più di 12 ore prima della data dell'evento meno recende della riunione in esame","WARNING",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                dateTimePicker_inizio_previsto.Value = dataPickerValueIni;
                return;
            }

            if (dateTimePicker_fine_previsto.Value < dateTimePicker_inizio_previsto.Value)
            {
                MessageBox.Show("Non è possibile impostare una data di inizio più recente della data di fine", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dateTimePicker_inizio_previsto.Value = dataPickerValueIni;
                return;
            }

            IList<GoogleMeetLogModel> newModel = new List<GoogleMeetLogModel>();
            foreach(GoogleMeetLogModel mod in actualListLog)
            {

                mod.EffectiveMeetingStartDate = dateTimePicker_inizio_previsto.Value;
                GoogleMeetLogModel actualModel = TimesHelper.changeEffectiveDateTime(mod);
                newModel.Add(actualModel);
            }


            dt.Rows.Clear();
            DataRow dr;
            foreach (GoogleMeetLogModel mod in newModel)
            {
                dr = dt.NewRow();
                foreach (var value in columns)
                {
                  
                        dr[value] = mod.GetType().GetProperty(value).GetValue(mod);
   
                }


                dt.Rows.Add(dr);

            }
            dataGridView1.DataSource = dt;
            dataPickerValueIni = dateTimePicker_inizio_previsto.Value;
        }

        private void dateTimePicker_fine_previsto_ValueChanged(object sender, EventArgs e)
        {
            var logItem = (LogItem)listBox2.SelectedItem;
            if (logItem.logListModel.Where(item => item.Date == logItem.logListModel.Select(log => log.Date).Max()).FirstOrDefault().EffectiveMeetingEndDate == dateTimePicker_fine_previsto.Value
                || dateTimePicker_fine_previsto.Value == dataPickerValueFin)
            {
                return;
            }

            if (dateTimePicker_fine_previsto.Value > logItem.logListModel.Where(item => item.Date == logItem.logListModel.Select(log => log.Date).Max()).FirstOrDefault().Date.AddHours(12))
            {
                MessageBox.Show("Non è possibile iniziare una riunione più di 12 ore prima della data dell'evento più recende della riunione in esame","WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dateTimePicker_fine_previsto.Value = dataPickerValueFin;
                return;
            }

            if (dateTimePicker_fine_previsto.Value < dateTimePicker_inizio_previsto.Value)
            {
                MessageBox.Show("Non è possibile impostare una data di fine meno recente della data di inizio", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dateTimePicker_fine_previsto.Value = dataPickerValueFin;
                return;
            }

            IList<GoogleMeetLogModel> newModel = new List<GoogleMeetLogModel>();
            foreach (GoogleMeetLogModel mod in actualListLog)
            {

                mod.EffectiveMeetingEndDate = dateTimePicker_fine_previsto.Value;
                GoogleMeetLogModel actualModel = TimesHelper.changeEffectiveDateTime(mod);
                newModel.Add(actualModel);
            }


            dt.Rows.Clear();
            DataRow dr;
            foreach (GoogleMeetLogModel mod in newModel)
            {
                dr = dt.NewRow();
                foreach (var value in columns)
                {

                    dr[value] = mod.GetType().GetProperty(value).GetValue(mod);

                }


                dt.Rows.Add(dr);

            }
            dataGridView1.DataSource = dt;
        }

        private void comboLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            _supportedLanguage = comboLanguage.SelectedItem.ToString();
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
