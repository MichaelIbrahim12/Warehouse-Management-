using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Company_Store_Project
{
    public partial class Clients_Inf : Form
    {
        Company_storeEntities1 Model=new Company_storeEntities1();
        public Clients_Inf()
        {
            InitializeComponent();
            showClientName();
        }
        public void showClientName()
        {
            comboBox1.Items.Clear();
            var clients = Model.Clients;
            foreach (var client in clients)
            {
                comboBox1.Items.Add(client.First_Name);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var clientt = from client in Model.Clients where client.First_Name == comboBox1.Text select client;
            foreach (var cl in clientt)
            {
                textBox1.Text = cl.id.ToString();
                textBox2.Text = cl.Telephone;
                textBox3.Text = cl.mobile;
                textBox4.Text = cl.email;
                textBox5.Text = cl.First_Name;
                textBox6.Text = cl.Last_Name;
                textBox7.Text = cl.fax;
                textBox8.Text = cl.website;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int client_id = int.Parse(textBox1.Text);

            var client = (from c in Model.Clients where c.id == client_id select c).FirstOrDefault();

            if (client == null)
            {
                Client cl = new Client();
                cl.id = client_id;
                cl.Telephone = textBox2.Text;
                cl.mobile = textBox3.Text;
                cl.email = textBox4.Text;
                cl.First_Name = textBox5.Text;
                cl.Last_Name = textBox6.Text;
                cl.fax = textBox7.Text;
                cl.website = textBox8.Text;
                Model.Clients.Add(cl);
                MessageBox.Show("Added Successfuly");
                textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = textBox6.Text = textBox7.Text = textBox8.Text = string.Empty;
                Model.SaveChanges();
                showClientName();


            }
            else
            {
                MessageBox.Show("Id Is Already Exist");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int clientId = int.Parse(textBox1.Text);

            var client = (from c in Model.Clients where c.id == clientId select c).FirstOrDefault();

            if (client != null)
            {

                client.id = clientId;
                client.Telephone = textBox2.Text;
                client.mobile = textBox3.Text;
                client.email = textBox4.Text;
                client.First_Name = textBox5.Text;
                client.Last_Name = textBox6.Text;
                client.fax = textBox7.Text;
                client.website = textBox8.Text;
                MessageBox.Show("Updated Successfuly");
                textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = textBox6.Text = textBox7.Text = textBox8.Text = string.Empty;
                Model.SaveChanges();
                showClientName();
            }
            else
            {
                MessageBox.Show("Not Exist");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int clientId = int.Parse(textBox1.Text);

            var client = (from c in Model.Clients where c.id == clientId select c).FirstOrDefault();

            if (client != null)
            {
                Model.Clients.Remove(client);
                MessageBox.Show("Removed Successfuly");
                textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = textBox6.Text = textBox7.Text = textBox8.Text = string.Empty;
                Model.SaveChanges();
                showClientName();
            }
            else
            {
                MessageBox.Show("Not Exist");
            }
        }
    }
}
