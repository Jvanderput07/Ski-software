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

namespace Skibeheer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                LoadData("database.txt");
                LoadSkiData("Skiamount.txt");
                LoadNotepadData("notepaddata.txt");
               
            }
            catch(Exception ex)
            {
                
            }
            
        }
        private bool AreAnyTextBoxesEmpty()
        {
            return Controls.OfType<TextBox>().Any(textBox => string.IsNullOrWhiteSpace(textBox.Text));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (AreAnyTextBoxesEmpty())
            {
                MessageBox.Show("Niet overal is informatie ingevoerd");
            }
            else
            {

                int lengte = int.Parse(textBox3.Text);
                skilengte(lengte);
                int uitkomst = selecteerski(gecalculeerdelengte);
                dataGridView1.Rows.Add(textBox1.Text, uitkomst.ToString(), textBox2.Text);
            }
        }
        public int gecalculeerdelengte;
        int skilengte(int lengte)
        {
            int gemideldhoofdmaatM = 50;
            int gemideldhoofdmaatV = 50;
            int gemideldvoorhoofdM = 19;
            int gemideldvoorhoofdV = 16;
            if (radioButton1.Checked)
            {
                int berekeningM = gemideldhoofdmaatM - gemideldvoorhoofdM;
                gecalculeerdelengte = lengte - berekeningM;  // Corrected calculation
                return gecalculeerdelengte;
            }
            else if (radioButton2.Checked)
            {
                int berekeningV = gemideldhoofdmaatV - gemideldvoorhoofdV;
                gecalculeerdelengte = lengte - berekeningV;  // Corrected calculation
                return gecalculeerdelengte;
            }

            else
            {
                MessageBox.Show("Kies aub een geslacht");
            }
            return gecalculeerdelengte;
        }
        public int geselecteerdeski;
        int selecteerski(int nummertocheck)
        {
            List<(int Lower, int Upper)> bounds = new List<(int, int)>
        {
            (80, 89),
            (100, 109),
            (110, 119),
            (120, 129),
            (130, 139),
            (140, 149),
            (150, 155),
            (156, 159),
            (160, 164)
        };
            var matchingRange = bounds.FirstOrDefault(range => nummertocheck >= range.Lower && nummertocheck <= range.Upper);
            geselecteerdeski = matchingRange.Lower;
            return geselecteerdeski;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SlaInfoOp("database.txt");
        }
        public void SlaInfoOp(string fileName)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            string rowData = $"{row.Cells[0].Value},{row.Cells[1].Value},{row.Cells[2].Value}";
                            writer.WriteLine(rowData);
                        }
                    }
                    MessageBox.Show("Alles correct opgeslagen");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Kon bestand niet opslaan");
            }
            
            } 
        public void LoadData(string Fileplace)
        {
            try
            {
                dataGridView1.Rows.Clear();

                using (StreamReader reader = new StreamReader(Fileplace))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] values = line.Split(',');
                        dataGridView1.Rows.Add(values);
                    }
                }

                MessageBox.Show("database is aanwezig");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }
        }
        public void LoadSkiData(string Fileplace)
        {
            try
            {
                dataGridView2.Rows.Clear();

                using (StreamReader reader = new StreamReader(Fileplace))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] values = line.Split(',');
                        dataGridView2.Rows.Add(values);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }
        }
        public void LoadNotepadData(string Fileplace)
        {
            try
            {
                string fileContent = File.ReadAllText(Fileplace);
                richTextBox1.Text = fileContent;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            File.WriteAllText("notepaddata.txt", richTextBox1.Text);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "Default text";
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {

        }
    } 
}

