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
    public partial class Import_Export : Form
    {
        Company_storeEntities1 model = new Company_storeEntities1();

        public Import_Export()
        {
            InitializeComponent();
            foreach (var item in model.Products)
            {
                Product_comboBox.Items.Add(item.Name);
                comboBox8.Items.Add(item.Name);
            }
            foreach (var item in model.Suppliers)
            {
                Supplier_ComboBox.Items.Add(item.First_Name);
                comboBox7.Items.Add(item.First_Name);
            }
            foreach (var item in model.Stores)
            {
                Store_comboBox.Items.Add(item.Name);
                comboBox6.Items.Add(item.Name);
            }
            showPermissionsImport();
            showPermissionsExport();

        }
        public void showPermissionsImport()
        {
            comboBox1.Items.Clear();
            var perm = from id in model.Import_perm select id;
            foreach (var item in perm)
            {
                comboBox1.Items.Add(item.per_id);
            }            

        }
        public void showPermissionsExport()
        {
            comboBox5.Items.Clear();
            var perm = from id in model.Export_perm select id;
            foreach (var item in perm)
            {
                comboBox5.Items.Add(item.perm_id);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int perm_id = int.Parse(comboBox1.Text);

            var perm = (from p in model.Import_perm where p.per_id == perm_id select p).FirstOrDefault();
            var products = (from pr in model.Import_product where pr.perm_id == perm_id select pr);

            Pem_date_textBox.Text = perm.perm_date.ToString();
            Supplier_ComboBox.Text = perm.Supplier.First_Name;
            Store_comboBox.Text = perm.Store.Name;
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            listBox4.Items.Clear();
            listBox5.Items.Clear();
            foreach (var item in products)
            {
                listBox1.Items.Add(item.Product.Name);
                listBox2.Items.Add(item.quantity);
                listBox3.Items.Add(item.unit);
                listBox4.Items.Add(item.production_date);
                listBox5.Items.Add(item.expire_date);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Quantity_textBox.Text != string.Empty && textBox4.Text != string.Empty && textBox3.Text != string.Empty && textBox5.Text != string.Empty)
            {
                string prod_name = Product_comboBox.Text;
                int prod_quant = int.Parse(Quantity_textBox.Text);
                string prod_unit = textBox3.Text;
                string prod_production_date = textBox4.Text;
                string prod_expire_date = textBox5.Text;
                listBox1.Items.Add(prod_name);
                listBox2.Items.Add(prod_quant);
                listBox3.Items.Add(prod_unit);
                listBox4.Items.Add(prod_production_date);
                listBox5.Items.Add(prod_expire_date);
                Quantity_textBox.Text = textBox3.Text = textBox4.Text = textBox5.Text = string.Empty;
            }
            else
            {
                MessageBox.Show("Please Enter all the data");
            }
        }



        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count != 0)
            {
                listBox1.Items.RemoveAt(listBox1.Items.Count - 1);
                listBox2.Items.RemoveAt(listBox2.Items.Count - 1);
                listBox3.Items.RemoveAt(listBox3.Items.Count - 1);
                listBox4.Items.RemoveAt(listBox4.Items.Count - 1);
                listBox5.Items.RemoveAt(listBox5.Items.Count - 1);
            }


        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {

                int perm_id = int.Parse(comboBox1.Text);

                var perm = (from p in model.Import_perm where p.per_id == perm_id select p).FirstOrDefault();
                if (perm == null)
                {

                    DateTime perm_date = DateTime.Parse(Pem_date_textBox.Text);

                    var supplier = (from sup in model.Suppliers where sup.First_Name == Supplier_ComboBox.Text select sup).FirstOrDefault();
                    int supplier_id = supplier.id;

                    var store = (from st in model.Stores where st.Name == Store_comboBox.Text select st).FirstOrDefault();
                    int store_id = store.id;

                    Import_perm import_perm = new Import_perm();

                    import_perm.per_id = perm_id;
                    import_perm.perm_date = perm_date;
                    import_perm.store_id = store_id;
                    import_perm.supplier_id = supplier_id;

                    int[] productsId = new int[listBox1.Items.Count];

                    int i = 0;
                    foreach (var item in listBox1.Items)
                    {
                        var prod = (from it in model.Products where it.Name == item.ToString() select it).FirstOrDefault();
                        productsId[i] = prod.id;
                        i++;
                    }

                    for (int j = 0; j < listBox1.Items.Count; j++)
                    {

                        Import_product prod = new Import_product();
                        prod.perm_id = perm_id;
                        prod.product_id = productsId[j];
                        prod.quantity = int.Parse(listBox2.Items[j].ToString());
                        prod.unit = listBox3.Items[j].ToString();
                        prod.production_date = DateTime.Parse(listBox4.Items[j].ToString());
                        prod.expire_date = DateTime.Parse(listBox5.Items[j].ToString());

                        import_perm.Import_product.Add(prod);
                        var st_prod = (from s in model.store_product where s.product_id == prod.product_id && s.store_id == store_id select s).FirstOrDefault();
                        if (st_prod != null)
                        {
                            if (st_prod.quantity is null)
                            {
                                st_prod.quantity = int.Parse(listBox2.Items[j].ToString());
                            }
                            else
                            {
                                st_prod.quantity += int.Parse(listBox2.Items[j].ToString());

                            }
                            st_prod.unit = listBox3.Items[j].ToString();
                            st_prod.production_date = DateTime.Parse(listBox4.Items[j].ToString());
                            st_prod.expire_date = DateTime.Parse(listBox5.Items[j].ToString());
                        }
                    }

                    model.Import_perm.Add(import_perm);

                    model.SaveChanges();
                    showPermissionsImport();


                    MessageBox.Show("added succeccfuly");
                }
                else
                {
                    MessageBox.Show("Permision Is Already Exist");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Enter Year/Month/Day");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //modify import_permession table
            int perm_id = int.Parse(comboBox1.Text);
            var perm = (from p in model.Import_perm where p.per_id == perm_id select p).FirstOrDefault();
            if (perm != null)
            {

                DateTime perm_date = DateTime.Parse(Pem_date_textBox.Text);

                var supplier = (from sup in model.Suppliers where sup.First_Name == Supplier_ComboBox.Text select sup).FirstOrDefault();
                int supplier_id = supplier.id;

                var store = (from st in model.Stores where st.Name == Store_comboBox.Text select st).FirstOrDefault();
                int store_id = store.id;

                int[] productsId = new int[listBox1.Items.Count];

                int i = 0;
               
                foreach (var item in listBox1.Items)
                {
                    var prod = (from it in model.Products where it.Name == item.ToString() select it).FirstOrDefault();
                    productsId[i] = prod.id;
                    i++;
                }
               
                //Remove each in Import_product &&  modify each in store_product
                var prod_imp=from p in model.Import_product where p.perm_id==perm_id select p;
                
                foreach(var prod in prod_imp)
                {
                    var st = (from s in model.store_product where s.store_id == prod.Import_perm.store_id && s.product_id == prod.product_id select s).FirstOrDefault();
                    st.quantity -= prod.quantity;
                    st.expire_date = null;
                    st.production_date = null;
                    st.unit = null;
                    model.Import_product.Remove(prod);
                  


                }
                model.SaveChanges();

                //add in import_prod && in store_product again
                for (int j = 0 ; j < listBox1.Items.Count; j++)
                {

                    Import_product prod = new Import_product();
                    prod.perm_id = perm_id;
                    prod.product_id = productsId[j];
                    prod.quantity = int.Parse(listBox2.Items[j].ToString());
                    prod.unit = listBox3.Items[j].ToString();
                    prod.production_date = DateTime.Parse(listBox4.Items[j].ToString());
                    prod.expire_date = DateTime.Parse(listBox5.Items[j].ToString());

                    perm.Import_product.Add(prod);

                    var st_prod = (from s in model.store_product where s.product_id == prod.product_id && s.store_id == store_id select s).FirstOrDefault();
                    if (st_prod != null)
                    {
                        if (st_prod.quantity is null)
                        {
                            st_prod.quantity = int.Parse(listBox2.Items[j].ToString());
                        }
                        else
                        {
                            st_prod.quantity += int.Parse(listBox2.Items[j].ToString());

                        }
                        st_prod.unit = listBox3.Items[j].ToString();
                        st_prod.production_date = DateTime.Parse(listBox4.Items[j].ToString());
                        st_prod.expire_date = DateTime.Parse(listBox5.Items[j].ToString());
                    }

                }
                perm.per_id = perm_id;
                perm.perm_date = perm_date;
                perm.store_id = store_id;
                perm.supplier_id = supplier_id;

                model.SaveChanges();
                showPermissionsImport();


                MessageBox.Show("Updated succeccfuly");
            }
            else
            {
                MessageBox.Show("Permision Not Found");
            }
        }


        private void button7_Click(object sender, EventArgs e)
        {
            if (comboBox8.Text != string.Empty && textBox10.Text != string.Empty)
            {
                string prod_name = comboBox8.Text;
                int prod_quantrReq = int.Parse(textBox10.Text);

                    listBox10.Items.Add(prod_name);
                    listBox9.Items.Add(prod_quantrReq);
                    textBox10.Text = string.Empty;
            }
            else
            {
                MessageBox.Show("Please Enter all the data");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox10.Items.Count != 0)
            {
                listBox10.Items.RemoveAt(listBox10.Items.Count - 1);
                listBox9.Items.RemoveAt(listBox9.Items.Count - 1);

            }
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
        
            try
            {
                int perm_id = int.Parse(comboBox5.Text);


                var perm = (from p in model.Export_perm where p.perm_id == perm_id select p).FirstOrDefault();
                if (perm == null)
                {
                    DateTime perm_date = DateTime.Parse(textBox6.Text);
                    var supplier = (from sup in model.Suppliers where sup.First_Name == comboBox7.Text select sup).FirstOrDefault();
                    int supplier_id = supplier.id;

                    var store = (from st in model.Stores where st.Name == comboBox6.Text select st).FirstOrDefault();
                    int store_id = store.id;

                    Export_perm export_perm = new Export_perm();

                    export_perm.perm_id = perm_id;
                    export_perm.perm_date = perm_date;
                    export_perm.store_id = store_id;
                    export_perm.supplier_id = supplier_id;

                    int[] productsId = new int[listBox10.Items.Count];

                    int i = 0;
                    foreach (var item in listBox10.Items)
                    {
                        var prod = (from it in model.Products where it.Name == item.ToString() select it).FirstOrDefault();
                        productsId[i] = prod.id;
                        i++;
                    }

                    for (int j = 0; j < listBox10.Items.Count; j++)
                    {

                        Export_product prod = new Export_product();
                        prod.Perm_id = perm_id;
                        prod.product_id = productsId[j];
                        prod.quantity = int.Parse(listBox9.Items[j].ToString());


                        export_perm.Export_product.Add(prod);

                        var st_prod = (from s in model.store_product where s.product_id == prod.product_id && s.store_id == store_id select s).FirstOrDefault();
                        if (st_prod != null)
                        {
                            if (st_prod.quantity< int.Parse(listBox9.Items[j].ToString()))
                            {
                                MessageBox.Show( st_prod.Product.Name+" "+ " is More Than Available in Store");
                            }
                            else {

                                st_prod.quantity -= int.Parse(listBox9.Items[j].ToString());

                                if (st_prod.quantity == 0)
                                {
                                    st_prod.unit = null;
                                    st_prod.production_date = null;
                                    st_prod.expire_date = null;
                                }

								model.Export_perm.Add(export_perm);



								model.SaveChanges();
								showPermissionsExport();


								MessageBox.Show("added succeccfuly");
							}
                        }

                    }

                }
                else
                {
                    MessageBox.Show("Permision Is Already Exist");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Enter Year/Month/Day");
            }
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            //modify export_permession table
            int perm_id = int.Parse(comboBox5.Text);
            var perm = (from p in model.Export_perm where p.perm_id == perm_id select p).FirstOrDefault();
            if (perm != null)
            {

                DateTime perm_date = DateTime.Parse(textBox6.Text);

                var supplier = (from sup in model.Suppliers where sup.First_Name == comboBox7.Text select sup).FirstOrDefault();
                int supplier_id = supplier.id;

                var store = (from st in model.Stores where st.Name == comboBox6.Text select st).FirstOrDefault();
                int store_id = store.id;


                int[] productsId = new int[listBox10.Items.Count];

                int i = 0;

                foreach (var item in listBox10.Items)
                {
                    var prod = (from it in model.Products where it.Name == item.ToString() select it).FirstOrDefault();
                    productsId[i] = prod.id;
                    i++;
                }

                //Remove each in Export_product &&  modify each in store_product
                var prod_imp = from p in model.Export_product where p.Perm_id == perm_id select p;

                foreach (var prod in prod_imp)
                {
                    var st = (from s in model.store_product where s.store_id == prod.Export_perm.store_id && s.product_id == prod.product_id select s).FirstOrDefault();
                    st.quantity += prod.quantity;

                    model.Export_product.Remove(prod);



                }
                model.SaveChanges();

                //add in import_prod && in store_product again
                for (int j = 0; j < listBox10.Items.Count; j++)
                {

                    Export_product prod = new Export_product();
                    prod.Perm_id = perm_id;
                    prod.product_id = productsId[j];
                    prod.quantity = int.Parse(listBox9.Items[j].ToString());
           

                    perm.Export_product.Add(prod);

                    var st_prod = (from s in model.store_product where s.product_id == prod.product_id && s.store_id == store_id select s).FirstOrDefault();
                    if (st_prod != null) { 
                        
                        st_prod.quantity -= int.Parse(listBox9.Items[j].ToString());
                        perm.perm_id = perm_id;
                        perm.perm_date = perm_date;
                        perm.store_id = store_id;
                        perm.supplier_id = supplier_id;

                        if (st_prod.quantity > 0)
                        {

                            model.SaveChanges();
                            showPermissionsExport();
                            MessageBox.Show("Updated succeccfuly");
                        }

                        else if (st_prod.quantity == 0)
                        {
                            st_prod.unit = null;
                            st_prod.production_date = null;
                            st_prod.expire_date = null;


                            model.SaveChanges();
                            showPermissionsExport();
                            MessageBox.Show("Updated succeccfuly");
                        }
                        else if (st_prod.quantity < 0)
                        {
                            MessageBox.Show("NO Enough Products in this Store");

                        }
                    }

                }

            }
            else
            {
                MessageBox.Show("Permision Not Found");
            }
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            int perm_id = int.Parse(comboBox5.Text);

            var perm = (from p in model.Export_perm where p.perm_id == perm_id select p).FirstOrDefault();
            var products = (from pr in model.Export_product where pr.Perm_id == perm_id select pr);

            textBox6.Text = perm.perm_date.ToString();
            comboBox7.Text = perm.Supplier.First_Name;
            comboBox6.Text = perm.Store.Name;
            listBox10.Items.Clear();
            listBox9.Items.Clear();

            foreach (var item in products)
            {
                listBox10.Items.Add(item.Product.Name);
                listBox9.Items.Add(item.quantity);

            }
        }

		private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
		{

		}
	}
}
