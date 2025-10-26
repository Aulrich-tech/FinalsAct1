using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Protobuf.Reflection;
using MySql.Data.MySqlClient;

namespace FinalsAct1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        string[] EventArray = 
        { "Basketball Live", "News and Others", "Entertainment", "Car Shows","Seminar and Workshop", "Training"};
        string[] TicketClassArray = 
        { "Upper Box - 200", "Lower Box - 400", "Gen Ad - 100" };
        
        private void Form1_Load(object sender, EventArgs e)
        {
            tbtrans.Text = TransactionGenerator.Generate();
            cbevent.Items.AddRange(EventArray);
            cbtix.Items.AddRange(TicketClassArray);
        }

        private void btnrecords_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
            this.Hide();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            bool valid = NoText.Validation(tbcs, tbaddress, tbtix, tbcontact, tbemail, cbevent, cbtix);
            if (!valid)
            {
                return;
            }

            // Total Calculation
            int total = Convert.ToInt32(TotalAmount.GetTotal(cbtix.SelectedItem.ToString(), int.Parse(tbtix.Text)));
            TxtTotal.Text = Convert.ToString(total);




            // Database Connection
            string dbconnect = "SERVER=localhost; database=dbactivity; uid=root";
            MySqlConnection sqlconnection = new MySqlConnection(dbconnect);
            MySqlCommand sqlcmd = new MySqlCommand();

            sqlconnection.Open();

            sqlcmd.CommandText = $"INSERT INTO tblcustomer (TRN, Customer, Address, Event, Ticket, Class, Total, Contact, Email)" +
                                 $"VALUES ('{tbtrans.Text}', '{tbcs.Text}', '{tbaddress.Text}', '{cbevent.SelectedItem}', '{tbtix.Text}', '{cbtix.SelectedItem}', '{total}', '{tbcontact.Text}' , '{tbemail.Text}')";

            sqlcmd.CommandType = CommandType.Text;
            sqlcmd.Connection = sqlconnection;

            sqlcmd.ExecuteNonQuery();

            sqlconnection.Close();

            tbtrans.Text = TransactionGenerator.Generate();
            tbcs.Clear();
            tbaddress.Clear();
            tbtix.Clear();
            tbcontact.Clear();
            tbemail.Clear();
            cbevent.SelectedIndex = 0;
            cbtix.SelectedIndex = 0;
            TxtTotal.Text = string.Empty;

            MessageBox.Show("Record Saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);




        }

        private void cbevent_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedEvent = cbevent.SelectedItem.ToString();

            switch (selectedEvent)
            {
                case "Basketball Live":
                    label1.ForeColor = Color.Black;
                    label2.ForeColor = Color.Black;
                    btnsave.BackColor = Color.Black;
                    panel1.BackgroundImage = Properties.Resources.basketball;
                    pictureBox2.Image = Properties.Resources.basketball;
                    break;

                case "News and Others":
                    label1.ForeColor = Color.Orange;
                    label2.ForeColor = Color.Orange;
                    btnsave.BackColor = Color.Orange;
                    panel1.BackgroundImage = Properties.Resources.news;
                    pictureBox2.Image = Properties.Resources.news;
                    break;

                case "Entertainment":
                    label1.ForeColor = Color.Salmon;
                    label2.ForeColor = Color.Salmon;
                    btnsave.BackColor = Color.Salmon;
                    panel1.BackgroundImage = Properties.Resources.entertainment;
                    pictureBox2.Image = Properties.Resources.entertainment;
                    break;

                case "Car Shows":
                    label1.ForeColor = Color.Black;
                    label2.ForeColor = Color.Black;
                    btnsave.BackColor = Color.Black;
                    panel1.BackgroundImage = Properties.Resources.car;
                    pictureBox2.Image = Properties.Resources.car;
                    break;

                case "Seminar and Workshop":
                    label1.ForeColor = Color.SandyBrown;
                    label2.ForeColor = Color.SandyBrown;
                    btnsave.BackColor = Color.SandyBrown;
                    panel1.BackgroundImage = Properties.Resources.seminar;
                    pictureBox2.Image = Properties.Resources.seminar;
                    break;

                case "Training":
                    label1.ForeColor = Color.Black;
                    label2.ForeColor = Color.Black;
                    btnsave.BackColor = Color.Black;
                    panel1.BackgroundImage = Properties.Resources.training;
                    pictureBox2.Image = Properties.Resources.training;
                    break;

                default:
                    panel1.BackgroundImage = null;
                    break;
            }

            panel1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;


        }

        private void cbtix_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbtix.SelectedItem != null && tbtix.Text != "")
            {
                Double total = Convert.ToDouble(TotalAmount.GetTotal(cbtix.SelectedItem.ToString(), Convert.ToDouble(tbtix.Text)));
                TxtTotal.Text = "P" + Convert.ToString(total);
            }
            
        }

        private void tbtix_TextChanged(object sender, EventArgs e)
        {
            foreach (char c in tbtix.Text)
            {
                if (!char.IsDigit(c))
                {
                    MessageBox.Show("Ticket number should contain only digits.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tbtix.Focus();
                    tbtix.Text = "";
                }
            }
            if (cbtix.SelectedItem != null && tbtix.Text != "")
            {
                Double total = Convert.ToDouble(TotalAmount.GetTotal(cbtix.SelectedItem.ToString(), Convert.ToDouble(tbtix.Text)));
                TxtTotal.Text = "P" + Convert.ToString(total);
            }
        }
    }
}
