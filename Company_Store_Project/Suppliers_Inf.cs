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

    public partial class Suppliers_Inf : Form
    {
        Company_storeEntities1 Model= new Company_storeEntities1();
        public Suppliers_Inf()
        {
            InitializeComponent();
            showSuppName();
            
        }
        public void showSuppName()
        {
            comboBox1.Items.Clear();
            var supplier = Model.Suppliers;
            foreach (var supp in supplier)
            {
                comboBox1.Items.Add(supp.First_Name);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var supplier = from supp in Model.Suppliers where supp.First_Name==comboBox1.Text select supp;
            foreach (var supp in supplier)
            {
                textBox1.Text = supp.id.ToString();
                textBox2.Text = supp.Telephone;
                textBox3.Text = supp.mobile;
                textBox4.Text = supp.email;
                textBox5.Text = supp.First_Name;
                textBox6.Text = supp.Last_Name;
                textBox7.Text = supp.fax;
                textBox8.Text = supp.website;
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int supp_id = int.Parse(textBox1.Text);

            var supp= (from s in Model.Suppliers where s.id==supp_id select s).FirstOrDefault();

            if (supp == null)
            {
                Supplier supplier = new Supplier();
                supplier.id= supp_id;
                supplier.Telephone= textBox2.Text;
                supplier.mobile= textBox3.Text;
                supplier.email= textBox4.Text;
                supplier.First_Name=textBox5.Text;
                supplier.Last_Name=textBox6.Text;
                supplier.fax= textBox7.Text;
                supplier.website= textBox8.Text;
                Model.Suppliers.Add(supplier);
                MessageBox.Show("Added Successfuly");
                textBox1.Text=textBox2.Text=textBox3.Text=textBox4.Text=textBox5.Text=textBox6.Text=textBox7.Text=textBox8.Text=string.Empty;
                Model.SaveChanges();
                showSuppName();


            }
            else
            {
                MessageBox.Show("Id Is Already Exist");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int supp_id = int.Parse(textBox1.Text);

            var supp = (from s in Model.Suppliers where s.id == supp_id select s).FirstOrDefault();

            if (supp != null)
            {

                supp.id = supp_id;
                supp.Telephone = textBox2.Text;
                supp.mobile = textBox3.Text;
                supp.email = textBox4.Text;
                supp.First_Name = textBox5.Text;
                supp.Last_Name = textBox6.Text;
                supp.fax = textBox7.Text;
                supp.website = textBox8.Text;
                MessageBox.Show("Updated Successfuly");
                textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = textBox6.Text = textBox7.Text = textBox8.Text = string.Empty;
                Model.SaveChanges();
                showSuppName();
            }
            else
            {
                MessageBox.Show("Not Exist");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int supp_id = int.Parse(textBox1.Text);

            var supp = (from s in Model.Suppliers where s.id == supp_id select s).FirstOrDefault();

            if (supp != null)
            {

                Model.Suppliers.Remove(supp);
        
                MessageBox.Show("Removed Successfuly");
                textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = textBox6.Text = textBox7.Text = textBox8.Text = string.Empty;
                Model.SaveChanges();
                showSuppName();
            }
            else
            {
                MessageBox.Show("Not Exist");
            }
        }
    }
}
