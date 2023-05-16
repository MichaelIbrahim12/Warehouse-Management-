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
    public partial class Reports : Form
    {
        Company_storeEntities1 model = new Company_storeEntities1();
        public Reports()
        {
            InitializeComponent();
            showStores();
            showProducts();

        }
        public void showStores()
        {
            comboBox1.Items.Clear();
            var Stores = model.Stores;
            foreach ( var item in Stores )
            {
                comboBox1.Items.Add( item.Name );
                comboBox3.Items.Add( item.Name );
                comboBox4.Items.Add( item.Name );
            }

        }        
        public void showProducts()
        {
            comboBox2.Items.Clear();
            var Stores = model.Products;
            foreach ( var item in Stores )
            {
                comboBox2.Items.Add( item.Name );
            }

        }
        

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            listBox4.Items.Clear();
            listBox5.Items.Clear(); 



            var Products = from p in model.store_product where p.Store.Name==comboBox1.Text  select p;

         foreach (var item in Products)
                {
                label4.Text = item.Store.Name;
                label5.Text = item.Store.Manger_Name;
                if (item.quantity > 0)
                {
                    listBox1.Items.Add(item.Product.Name);
                    listBox2.Items.Add(item.quantity);
                    listBox3.Items.Add(item.unit);
                    listBox4.Items.Add(item.production_date);
                    listBox5.Items.Add(item.expire_date);
                }

                }
               
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox6.Items.Clear();
            listBox7.Items.Clear();
            listBox8.Items.Clear();
            listBox9.Items.Clear();
            listBox10.Items.Clear();
            comboBox3.Text = string.Empty;
            var products=from p in model.store_product where p.Product.Name==comboBox2.Text select p;

            foreach (var item in products)
            {
                listBox10.Items.Add(item.Store.Name);
                if (item.quantity > 0) {
                   
                    listBox9.Items.Add(item.quantity);
                    listBox8.Items.Add(item.unit);
                    listBox7.Items.Add(item.expire_date);
                    listBox6.Items.Add(item.production_date);
                }else if ( item.quantity ==0 || item.quantity is null)
                {
                    listBox9.Items.Add("Empty");
                    listBox8.Items.Add("Empty");
                    listBox7.Items.Add("Empty");
                    listBox6.Items.Add("Empty");

                }
            }

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox2.Text!=string.Empty)
            {
                listBox6.Items.Clear();
                listBox7.Items.Clear();
                listBox8.Items.Clear();
                listBox9.Items.Clear();
                listBox10.Items.Clear();
                string store = comboBox3.Text;
                string product = comboBox2.Text;

                var products = (from p in model.store_product where p.Store.Name == store && p.Product.Name == product select p).FirstOrDefault();

                listBox10.Items.Add(store);

                if (products.quantity > 0)
                {
                    listBox9.Items.Add(products.quantity);
                    listBox8.Items.Add(products.unit);
                    listBox7.Items.Add(products.production_date);
                    listBox6.Items.Add(products.expire_date);
                }
                else
                {
                    listBox9.Items.Add("Empty");
                    listBox8.Items.Add("Empty");
                    listBox7.Items.Add("Empty");
                    listBox6.Items.Add("Empty");

                }

            }else
            {
                MessageBox.Show("Choose Prodcut First");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime From_date = DateTime.Parse(textBox1.Text);
                DateTime To_date = DateTime.Parse(textBox2.Text);
                listBox15.Items.Clear();
                listBox14.Items.Clear();
                listBox13.Items.Clear();
                listBox12.Items.Clear();
                listBox11.Items.Clear();

                var Transfers = from p in model.Transfer_Product select p;

                foreach (var item in Transfers)
                {
                    int comp1 = DateTime.Compare((DateTime)item.Transfer_date, From_date);
                    int comp2 = DateTime.Compare((DateTime)item.Transfer_date, To_date);
                    if (comp1 >= 0 && comp2 <= 0)
                    {

                        listBox15.Items.Add(item.Product.Name);
                        listBox14.Items.Add(item.Store.Name);
                        listBox13.Items.Add(item.Store1.Name);
                        listBox12.Items.Add(item.Quantity);
                        listBox11.Items.Add(item.unit);

                    }
                }

            }catch (Exception ex)
            {
                MessageBox.Show("Enter Year/Month/Day");
            }
        }

		private void button1_Click(object sender, EventArgs e)
        {
            listBox20.Items.Clear();
            listBox19.Items.Clear();
            listBox18.Items.Clear();
            listBox17.Items.Clear();
            listBox16.Items.Clear();
            listBox21.Items.Clear();

            DateTime now= DateTime.Today;
            int period=0;
            TimeSpan p=new TimeSpan();

			if (textBox3.Text != string.Empty)
            {
                period= Convert.ToInt32(textBox3.Text)*12*30;
				p = TimeSpan.Parse(period.ToString());
			}
			else if(textBox4.Text!=string.Empty){

                period= Convert.ToInt32(textBox4.Text)*30;
                 p= TimeSpan.Parse(period.ToString());
            }else if(textBox4.Text != string.Empty && textBox3.Text!=string.Empty) {

				period = Convert.ToInt32(textBox4.Text) * 30 + Convert.ToInt32(textBox3.Text) * 12 * 30;
				p = TimeSpan.Parse(period.ToString());

            }

            var products = from pp in model.store_product  select pp;

            foreach (var product in products) {
                if ((product.expire_date - now) < p)
                {
                    listBox20.Items.Add(product.Product.Name);
                    listBox19.Items.Add(product.quantity.ToString());
                    listBox18.Items.Add(product.unit.ToString());
                    listBox16.Items.Add(product.expire_date.ToString());
                    listBox21.Items.Add(product.Store.Name);
					var x = (product.expire_date - now);
                    
                    
					listBox17.Items.Add( x + " " + "Days");
                    
                   
                }

            }
            



        }

		private void button2_Click(object sender, EventArgs e)
		{
			listBox22.Items.Clear();
			listBox23.Items.Clear();
			listBox24.Items.Clear();
			listBox26.Items.Clear();
			listBox27.Items.Clear();

			if (comboBox5.Items.Count > 0 && comboBox4.Text!=string.Empty)
            {

                DateTime now = DateTime.Today;
                int period = 0;
                TimeSpan p = new TimeSpan();

                if (textBox6.Text != string.Empty)
                {
                    period = Convert.ToInt32(textBox6.Text) * 12 * 30;
                    p = TimeSpan.Parse(period.ToString());
                }
                else if (textBox5.Text != string.Empty)
                {

                    period = Convert.ToInt32(textBox5.Text) * 30;
                    p = TimeSpan.Parse(period.ToString());
                }
                else if (textBox6.Text != string.Empty && textBox5.Text != string.Empty)
                {

                    period = Convert.ToInt32(textBox5.Text) * 30 + Convert.ToInt32(textBox6.Text) * 12 * 30;
                    p = TimeSpan.Parse(period.ToString());

                }

                var products = from pp in model.Import_product where  pp.Import_perm.Store.Name == comboBox4.Text select pp;

                foreach (var product in products)
                {
                    if ((now - product.Import_perm.perm_date) < p)
                    {
                        listBox27.Items.Add(product.Product.Name);
                        listBox26.Items.Add(product.Import_perm.Store.Name);
                        listBox23.Items.Add(product.production_date);
                        listBox24.Items.Add(product.expire_date);
                        var x = (now - product.Import_perm.perm_date);


                        listBox22.Items.Add(x + " " + "Days");


                    }

                }
            }
            else if( comboBox4.Text == string.Empty)
            {

				MessageBox.Show("Choose Store First");
			}
            else if (comboBox5.Items.Count==0) { 

                MessageBox.Show("No Products In This Store Now");
            }
		}

		private void label53_Click(object sender, EventArgs e)
		{

		}

		private void label54_Click(object sender, EventArgs e)
		{

		}

		private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
		{
            comboBox5.Items.Clear();
            var products=from p in model.store_product where p.Store.Name==comboBox4.Text&& p.quantity>0 select p;

            foreach (var product in products)
            {
                comboBox5.Items.Add(product.Product.Name);
            }
		}


	}
}
