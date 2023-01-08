using System;
using System.Data.OleDb;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using System.Data.Common;

namespace Lab11_csv
{
    public partial class Form1 : Form
    {

        public DataTable dt = new DataTable();

        public Form1()
        {
            InitializeComponent();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                dt = GetDataTableFromCsv(openFileDialog.FileName, false);
            }
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
        }




        static DataTable GetDataTableFromCsv(string path, bool isFirstRowHeader)
        {
            string header = isFirstRowHeader ? "Yes" : "No";
            

            string pathOnly = Path.GetDirectoryName(path);
            string fileName = Path.GetFileName(path);

            string sql = @"SELECT * FROM [" + fileName + "]";


            using (OleDbConnection connection = new OleDbConnection(
                @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathOnly +
                ";Extended Properties=\"Text;HDR=" + header + "\""))
            using (OleDbCommand command = new OleDbCommand(sql,connection))
            using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
            {
                DataTable dataTable = new DataTable();
                dataTable.Locale = CultureInfo.CurrentCulture;
                adapter.Fill(dataTable);
                return dataTable;
            }                
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            int index = dataGridView1.CurrentCell.RowIndex;
            string info = "";
            //string info = dt.Rows[index][4].ToString();

            if (checkBox1.Checked)
            {
                info = info + "\n" + dt.Rows[index][0];
            }
            if (checkBox2.Checked)
            {
                info = info + "\n" + dt.Rows[index][1];
            }
            if (checkBox3.Checked)
            {
                info = info + "\n" + dt.Rows[index][2];
            }
            if (checkBox4.Checked)
            {
                info = info + "\n" + dt.Rows[index][3];
            }
            if (checkBox5.Checked)
            {
                info = info + "\n" + dt.Rows[index][5];
            }
            if (checkBox6.Checked)
            {
                info = info + "\n" + dt.Rows[index][6];
            }
            Form2 fr2 = new Form2(info);
            fr2.ShowDialog();

        }
    }
}
