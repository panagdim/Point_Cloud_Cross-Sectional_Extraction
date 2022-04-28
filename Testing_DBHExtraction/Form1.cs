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
using System.Globalization;
using Microsoft.Win32;
using Tulpep.NotificationWindow;

namespace Testing_DBHExtraction
{
    public partial class Form1 : Form
    {
        private List<Point> points;

        private List<Point> selectedPoints;
        public Form1()
        {
            InitializeComponent();
            points = new List<Point>();
            selectedPoints = new List<Point>();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.VirtualMode = true;
        }

        OpenFileDialog of = new OpenFileDialog();

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Do sth
        }

        private void resultButton_Click(object sender, EventArgs e)
        {
            points = new List<Point>();
            try
            {
                of.Filter = "TXT|*.txt|XYZ|*.xyz|CSV|*.csv";
                of.Title = "Please select one of the following formats to proceed";
                if (of.ShowDialog() == DialogResult.OK)
                {
                    using (StreamReader sr = File.OpenText(of.FileName))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            string qwe = line;
                            string[] ew = qwe.Split(' '); // Use whatever is your delimeter in your .txt file

                            double x = double.Parse(ew[0], CultureInfo.InvariantCulture);
                            double y = double.Parse(ew[1], CultureInfo.InvariantCulture);
                            double z = double.Parse(ew[2], CultureInfo.InvariantCulture);

                            Point p = new Point(x, y, z);
                            points.Add(p);                          
                        }
                    }
                }
                var bindingList = new BindingList<Point>(points);
                var source = new BindingSource(bindingList, null);
                dataGridView1.DataSource = source;
                MessageBox.Show("Click on Step 2", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex);  
            }
        }

        private void calculateButton_Click(object sender, EventArgs e)
        {           
            foreach (var p in points)
            {
                p.Comp = Math.Round(p.Z, 3);                           
            }

            var bindingList = new BindingList<Point>(points);
            var source = new BindingSource(bindingList, null);
            dataGridView1.DataSource = source;
            MessageBox.Show("Click on Step 3", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
        }

        private void dataGridView1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void dataGridView1_DragDrop(object sender, DragEventArgs e)
        {
            //do sth
        }

        private void copyButton_Click(object sender, EventArgs e)
        {
            var s = dataGridView1.SelectedRows;
            selectedPoints = new List<Point>();
            foreach (DataGridViewRow row in s)
            {
                var o = row.DataBoundItem as Point;  
                selectedPoints.Add(o);
                dataGridView1.DefaultCellStyle.BackColor = Color.Red;               
            }

            var bindingList = new BindingList<Point>(selectedPoints);
            var source = new BindingSource(bindingList, null);
            filteredDataGridView.DataSource = source;
            filteredDataGridView.DefaultCellStyle.BackColor = Color.Coral;
            var rowscount = filteredDataGridView.Rows;
            foreach (DataGridViewRow rowsfiltered in rowscount)
            {
            label2.Text = "Total Number of Selected Lines:" + filteredDataGridView.RowCount.ToString();            
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "TXT|*.txt|CSV|*.csv";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (Stream s = File.Open(saveFileDialog1.FileName, FileMode.CreateNew))
                using (StreamWriter sw = new StreamWriter(s))

                    foreach (DataGridViewRow row in filteredDataGridView.Rows)
                    {
                        var o = row.DataBoundItem as Point;
                        sw.WriteLine(o.X + " " + o.Y + " " + o.Comp);
                    }
            }
            MessageBox.Show("Data Exported Succesfully", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            selectedPoints = new List<Point>();
            var bindingList = new BindingList<Point>(selectedPoints);
            var source = new BindingSource(bindingList, null);
            filteredDataGridView.DataSource = source;
        }

        private void selectButton_Click(object sender, EventArgs e)
        {
            var rows = dataGridView1.Rows;
           
            foreach (DataGridViewRow row in rows)
            {

                var p = row.DataBoundItem as Point;
                if (isCondition(p))
                {
                    row.Selected = true;
                    label1.Text = "Total Number of lines:" + dataGridView1.RowCount.ToString();
                }    
            }
            MessageBox.Show("Selection Completed, please click on step 4. To export your result click on 'Save' button", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
        }

        private bool isCondition(Point p)
        {
            if (p.Comp >= 1.27 && p.Comp <= 1.33)     
            {
                    return true;
                }
            else
                {
                    return false;
                }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                // Show Icon = false;
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(1000); 
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {            
            notifyIcon1.Visible = false;
            ShowInTaskbar = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PopupNotifier pop = new PopupNotifier();
            pop.Image = Properties.Resources.info;
            pop.TitleText = "Developer: Dimitrios Panagiotidis" + Environment.NewLine + "2022";
            pop.ContentText = Environment.NewLine + "I hope you find my app useful";
            pop.ContentColor = Color.DarkGray;
            pop.BorderColor = Color.DimGray;
            pop.Popup(); 
            notifyIcon1.BalloonTipText = "Application is minimized";
        }
    }
}
   