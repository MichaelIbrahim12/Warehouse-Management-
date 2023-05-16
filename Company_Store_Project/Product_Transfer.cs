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
    public partial class Product_Transfer : Form
    {
        Company_storeEntities1 model = new Company_storeEntities1();
        public Product_Transfer()
        {
            InitializeComponent();
            ShowTransfer();
            
            foreach(var supplier in model.Suppliers)
            {
                comboBox3.Items.Add(supplier.First_Name);
            }
            foreach(var item in model.Stores)
            {
                comboBox2.Items.Add(item.Name);
                comboBox6.Items.Add(item.Name);
            }

            textBox1.Enabled = false;
            textBox2.Enabled = false;
        }
        public void ShowTransfer()
        {
            comboBox1.Items.Clear();
            var transfer = model.Transfer_Product;
            foreach( var item in transfer ) {
                comboBox1.Items.Add( item.Transfer_id );
            }
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox8.Items.Clear();
            var store_name=comboBox6.Text;
            var  store_id=(from st in model.Stores where st.Name==store_name select st.id).FirstOrDefault();
            var products=from p in model.store_product where p.store_id == store_id &&p.quantity>0 select p;
            foreach( var item in products )
            {
                comboBox8.Items.Add(item.Product.Name);
            }
            if(products.Count()==0)
            {
                label7.Text = (0).ToString();
                textBox1.Text = string.Empty;
                textBox2.Text = string.Empty;
                textBox10.Text = string.Empty;
                comboBox8.Text = string.Empty;

            }
        }

        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            var store_id=(from s in model.store_product where s.Store.Name==comboBox6.Text select s.store_id).FirstOrDefault();
            var product_id= (from ss in model.store_product where ss.Product.Name==comboBox8.Text select ss.product_id).FirstOrDefault();
            var prod_store= (from p in model.store_product where p.store_id==store_id && p.product_id==product_id select p).FirstOrDefault();
            
            label7.Text=prod_store.quantity.ToString();
            textBox1.Text=prod_store.production_date.ToString();
            textBox2.Text=prod_store.expire_date.ToString();
            
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                int transfer_id = int.Parse(comboBox1.Text);

                var transfer = (from p in model.Transfer_Product where p.Transfer_id == transfer_id select p).FirstOrDefault();

                if (transfer == null)
                {

                    var product_id = (from p in model.Products where p.Name == comboBox8.Text select p.id).FirstOrDefault();
                    var From_id = (from p in model.Stores where p.Name == comboBox6.Text select p.id).FirstOrDefault();
                    var To_id = (from p in model.Stores where p.Name == comboBox2.Text select p.id).FirstOrDefault();
                    var supplier_id = (from p in model.Suppliers where p.First_Name == comboBox3.Text select p.id).FirstOrDefault();
                    int quantity = int.Parse(textBox10.Text);
                    DateTime prod_date = DateTime.Parse(textBox1.Text);
                    DateTime expire_date = DateTime.Parse(textBox2.Text);
                    DateTime transfer_date = DateTime.Parse(textBox3.Text);
                    var store_prod = (from p in model.store_product where p.store_id == From_id && p.product_id == product_id select p).FirstOrDefault();
                    var unit = store_prod.unit;
                    var availablequantity = store_prod.quantity;

                    if (availablequantity >= quantity)
                    {
                        Transfer_Product prod_trans = new Transfer_Product
                        {
                            Transfer_id = transfer_id,
                            Transfer_date = transfer_date,
                            Product_id = product_id,
                            From_store_id = From_id,
                            To_store_id = To_id,
                            unit = unit,
                            Supplier_id = supplier_id,
                            Quantity = quantity,
                            Production_date = prod_date,
                            Expire_date = expire_date,
                        };
                        model.Transfer_Product.Add(prod_trans);

                        var from_store_prod = (from p in model.store_product where p.store_id == From_id && p.product_id == product_id select p).FirstOrDefault();
                        from_store_prod.quantity -= quantity;
                        var to_store_prod = (from p in model.store_product where p.store_id == To_id && p.product_id == product_id select p).FirstOrDefault();
                        if (to_store_prod.quantity is null)
                        {
                            to_store_prod.quantity = quantity;
                        }
                        else
                        {

                            to_store_prod.quantity += quantity;

                        }
                        to_store_prod.unit = unit;
                        to_store_prod.production_date = prod_date;
                        to_store_prod.expire_date = expire_date;
                        if (from_store_prod.quantity == 0)
                        {
                            to_store_prod.unit = null;
                            to_store_prod.production_date = null;
                            to_store_prod.expire_date = null;
                        }
                        model.SaveChanges();
						MessageBox.Show("Transfered Successfuly");
                        ShowTransfer();

					}
                    else
                    {
                        MessageBox.Show("Quantity is More Than available");
                    }


                }
                else
                {
                    MessageBox.Show("Id Is Already Available");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Enter Year/Month/Day");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = int.Parse(comboBox1.Text);
            var transfer = (from p in model.Transfer_Product where p.Transfer_id == id select p).FirstOrDefault();
            if (transfer != null)
            {
                comboBox6.Text = transfer.Store.Name;
                comboBox2.Text = transfer.Store1.Name;
                comboBox8.Text=transfer.Product.Name;
                textBox10.Text = transfer.Quantity.ToString();
                comboBox3.Text = transfer.Supplier.First_Name;
                textBox1.Text = transfer.Production_date.ToString();
                textBox2.Text = transfer.Expire_date.ToString();
                textBox3.Text = transfer.Transfer_date.ToString();

            }
        }

        
    }
}
