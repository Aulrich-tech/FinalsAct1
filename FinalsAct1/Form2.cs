using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace FinalsAct1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        string[] EventArray =
        { "Basketball Live", "News and Others", "Entertainment", "Car Shows","Seminar and Workshop", "Training"};
        string[] TicketClassArray =
        { "Upper Box - 200", "Lower Box - 400", "Gen Ad - 100" };

        private void btnnew_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Hide();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            cbevent.Items.AddRange(EventArray);
            cbtix.Items.AddRange(TicketClassArray);

            LoadData();

        }

        public void LoadData()
        {
            string dbconnect = "SERVER=localhost; database=dbactivity; uid=root";
            MySqlConnection sqlconnection = new MySqlConnection(dbconnect);
            MySqlCommand sqlcmd = new MySqlCommand();
            MySqlDataAdapter sqlDA = new MySqlDataAdapter();
            DataSet sqlds = new DataSet();

            sqlconnection.Open();

            sqlcmd.CommandText = $"SELECT * FROM tblcustomer";

            sqlcmd.CommandType = CommandType.Text;
            sqlcmd.Connection = sqlconnection;
            sqlDA.SelectCommand = sqlcmd;
            sqlDA.Fill(sqlds, "recordsfetch");
            dataGridView1.DataSource = sqlds;
            dataGridView1.DataMember = "recordsfetch";

            sqlconnection.Close();
        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            bool valid = NoText.Validation(tbcs, tbaddress, tbtix, tbcontact, tbemail, cbevent, cbtix);
            if (!valid)
            {
                return;
            }


            if (string.IsNullOrWhiteSpace(tbtn.Text))
            {
                MessageBox.Show("Please enter the Transaction Number.", "Validation Error",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbtn.Focus();
                
            }
            else
            {
                string transNum = tbtn.Text, Name=tbcs.Text, Address=tbaddress.Text,
                    Event=cbevent.SelectedItem.ToString(), TicketNo=tbtix.Text,
                    TicketClass=cbtix.SelectedItem.ToString(), Contact=tbcontact.Text,
                    Email=tbemail.Text;
                Double total = Convert.ToDouble(TotalAmount.GetTotal(cbtix.SelectedItem.ToString(), Convert.ToDouble(tbtix.Text)));


                string dbconnect = "SERVER=localhost; database=dbactivity; uid=root";
                MySqlConnection sqlconnection = new MySqlConnection(dbconnect);
                MySqlCommand sqlcmd = new MySqlCommand();




                sqlconnection.Open();

                sqlcmd.CommandText = $"UPDATE tblcustomer SET Customer='{Name}', Address='{Address}', Event='{Event}', Ticket='{TicketNo}', Class='{TicketClass}', Total='{total}', Contact='{Contact}', Email='{Email}' WHERE TRN LIKE '%{transNum}%'";
                sqlcmd.CommandType = CommandType.Text;
                sqlcmd.Connection = sqlconnection;

                sqlcmd.ExecuteNonQuery();

                sqlconnection.Close();

                LoadData();
            }
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
             

            if (string.IsNullOrWhiteSpace(tbtn.Text))
            {
                MessageBox.Show("Please enter the Transaction Number.", "Validation Error",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbtn.Focus();
            }
            else
            {
                string transNum = tbtn.Text;

                string dbconnect = "SERVER=localhost; database=dbactivity; uid=root";
                MySqlConnection sqlconnection = new MySqlConnection(dbconnect);
                MySqlCommand sqlcmd = new MySqlCommand();

                
                

                sqlconnection.Open();

                sqlcmd.CommandText = $"DELETE FROM tblcustomer WHERE TRN LIKE '%{transNum}%'";
                sqlcmd.CommandType = CommandType.Text;
                sqlcmd.Connection = sqlconnection;

                sqlcmd.ExecuteNonQuery();

                sqlconnection.Close();



                LoadData();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string transNum = tbtn.Text;

            string dbconnect = "SERVER=localhost; database=dbactivity; uid=root";
            MySqlConnection sqlconnection = new MySqlConnection(dbconnect);
            MySqlCommand sqlcmd = new MySqlCommand();
            MySqlDataReader sqlreader;




            sqlconnection.Open();

            sqlcmd.CommandText = $"SELECT * FROM tblcustomer WHERE TRN = '{transNum}'";
            sqlcmd.CommandType = CommandType.Text;
            sqlcmd.Connection = sqlconnection;

            sqlreader = sqlcmd.ExecuteReader();

            if (sqlreader.HasRows)
                {
                while (sqlreader.Read())
                {
                    tbcs.Text = sqlreader[1].ToString();
                    tbaddress.Text = sqlreader[2].ToString();
                    cbevent.SelectedItem = sqlreader[3].ToString();
                    tbtix.Text = sqlreader[4].ToString();
                    cbtix.SelectedItem = sqlreader[5].ToString();
                    tbcontact.Text = sqlreader[7].ToString();
                    tbemail.Text = sqlreader[8].ToString();
                }
            }
            else
            {
                MessageBox.Show("Transaction Number not found.", "Search Result",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            } 

            sqlconnection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
