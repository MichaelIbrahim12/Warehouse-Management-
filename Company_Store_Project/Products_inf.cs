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
    public partial class Products_inf : Form
    {
        Company_storeEntities1 model=new Company_storeEntities1();
        public Products_inf()
        {
            InitializeComponent();
            showProducts();
        }
        public void showProducts()
        {
            comboBox1.Items.Clear();
            var products= model.Products;

            foreach (var product in products)
            {
                comboBox1.Items.Add(product.Name);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int product_id = int.Parse(textBox1.Text);
            
                var product = (from p in model.Products where p.id == product_id select p).FirstOrDefault();
            
            if (product == null)
            {
                Product prod = new Product();
                prod.id = product_id;
                prod.Name = textBox2.Text;
                prod.Product_unit.Add(new Product_unit { id = product_id, unit = textBox3.Text }) ;
                model.Products.Add(prod);
                MessageBox.Show("Added Successfuly");
                textBox1.Text = textBox2.Text = string.Empty;
                var stores= model.Stores;
                foreach (var store in stores)
                {
                    store_product st_prod=new store_product {product_id= product_id,store_id=store.id};
                    model.store_product.Add(st_prod);
                    
                }

                model.SaveChanges();
                showProducts();
            }
            else
            {
                MessageBox.Show("Id Is Already Exist");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int prod_id = int.Parse(textBox1.Text);

            var prod = (from p in model.Products where p.id == prod_id select p).FirstOrDefault();
           


            if (prod != null )
            {

                prod.id = prod_id;
                prod.Name = textBox2.Text;
                MessageBox.Show("Updated Successfuly");
                textBox1.Text = textBox2.Text=textBox3.Text = string.Empty;
                model.SaveChanges();
                showProducts() ;
            }
            else
            {
                MessageBox.Show("Not Exist");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int prod_id = int.Parse(textBox1.Text);

            var prod = (from p in model.Products where p.id == prod_id select p).FirstOrDefault();

            if (prod != null)
            {

                model.Products.Remove(prod);

                MessageBox.Show("Removed Successfuly");
                textBox1.Text = textBox2.Text=textBox3.Text =string.Empty;
                model.SaveChanges();
                showProducts();
            }
            else
            {
                MessageBox.Show("Not Exist");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { 

            var product = (from p in model.Products where p.Name == comboBox1.Text select p).FirstOrDefault();
            var product_unit = (from p in model.Product_unit where p.Product.Name == comboBox1.Text select p).FirstOrDefault();
            textBox1.Text=product.id.ToString();
            textBox2.Text=product.Name;
            textBox3.Text = product_unit.unit.ToString();
            

        }
    }
}
