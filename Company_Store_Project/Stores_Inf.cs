using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Company_Store_Project
{
    public partial class Stores_Inf : Form
    {
        Company_storeEntities1 model=new Company_storeEntities1();
        public Stores_Inf()
        {
            InitializeComponent();
            showstores();
        }
        public void showstores()
        {
            comboBox1.Items.Clear();
            var stores = from store in model.Stores select store;
            foreach ( var store in stores )
            {
                comboBox1.Items.Add( store.Name );
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var store = (from s in model.Stores where s.Name == comboBox1.Text select s).FirstOrDefault() ;
            textBox1.Text=store.id.ToString();
            textBox2.Text=store.Name;
            textBox3.Text = store.Manger_Name;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int store_id = int.Parse(textBox1.Text);

            var store = (from s in model.Stores where s.id == store_id select s).FirstOrDefault();

            if (store == null)
            {
                Store storee = new Store();
                storee.id = store_id;
                storee.Name = textBox2.Text;
                storee.Manger_Name =textBox3.Text;
                model.Stores.Add( storee );
                MessageBox.Show("Added Successfuly");
                textBox1.Text = textBox2.Text = textBox3.Text =string.Empty;
				var products = model.Products;
				foreach (var product in products)
				{
					store_product st_prod = new store_product { product_id = product.id, store_id = store_id };
					model.store_product.Add(st_prod);

				}
				model.SaveChanges();
                showstores();
            }
            else
            {
                MessageBox.Show("Id Is Already Exist");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int store_id = int.Parse(textBox1.Text);

            var st = (from s in model.Stores where s.id == store_id select s).FirstOrDefault();

            if (st != null)
            {

                st.id = store_id;
                st.Name = textBox2.Text;
                st.Manger_Name =textBox3.Text;
                MessageBox.Show("Updated Successfuly");
                textBox1.Text = textBox2.Text = textBox3.Text =  string.Empty;
                model.SaveChanges();
                showstores();
            }
            else
            {
                MessageBox.Show("Not Exist");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int store_id = int.Parse(textBox1.Text);

            var st = (from s in model.Stores where s.id == store_id select s).FirstOrDefault();

            if (st != null)
            {

                model.Stores.Remove(st);

                MessageBox.Show("Removed Successfuly");
                textBox1.Text = textBox2.Text = textBox3.Text =  string.Empty;
                model.SaveChanges();
                showstores();
            }
            else
            {
                MessageBox.Show("Not Exist");
            }
        }
    }
}
