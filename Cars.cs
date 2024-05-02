using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarCrowd
{
    public partial class Cars : Form
    {
        public Cars()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-2DFCC0R\\SQLEXPRESS;Initial Catalog=CarCrowdSP_DB;Integrated Security=True;Encrypt=False");

        private void button1_Click(object sender, EventArgs e)
        {
            int carid = int.Parse(textBox1.Text);
            string model = textBox2.Text, brand = comboBox1.Text, available = comboBox2.Text, colour = textBox4.Text;
            double price = double.Parse(textBox3.Text);
            con.Open();
            SqlCommand c = new SqlCommand("exec InsertCars_SP '" + carid + "','" + brand + "','" + model + "','" + colour + "','" + price + "','" + available + "'", con);
            c.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Successfully Inserted");
            GetCarList();

        }
        void GetCarList()
        {
            SqlCommand c = new SqlCommand("exec ListCar_SP", con);
            SqlDataAdapter sd = new SqlDataAdapter(c);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void Cars_Load(object sender, EventArgs e)
        {
            GetCarList();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Update
            int carid = int.Parse(textBox1.Text);
            string model = textBox2.Text, brand = comboBox1.Text, available = comboBox2.Text, colour = textBox4.Text;
            double price = double.Parse(textBox3.Text);
            con.Open();
            SqlCommand c = new SqlCommand("exec UpdateCars_SP '" + carid + "','" + brand + "','" + model + "','" + colour + "','" + price + "','" + available + "'", con);
            c.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Successfully Updated...");
            GetCarList();


        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Delete
            if (MessageBox.Show("Are you sure to delete?", "Delete Document", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int carid = int.Parse(textBox1.Text);

                con.Open();
                SqlCommand c = new SqlCommand("exec DeleteCars_SP '" + carid + "'", con);
                c.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Successfully Deleted...");
                GetCarList();
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Search
            int carid = int.Parse(textBox1.Text);
            SqlCommand c = new SqlCommand("exec SearchCars_SP '" + carid + "'", con);
            SqlDataAdapter sd = new SqlDataAdapter(c);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
