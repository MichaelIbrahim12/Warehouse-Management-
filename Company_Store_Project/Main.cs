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
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Stores_Inf stores= new Stores_Inf();
            stores.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Clients_Inf clients = new Clients_Inf();
            clients.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Suppliers_Inf suppliers = new Suppliers_Inf();
            suppliers.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Products_inf products = new Products_inf();
            products.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Import_Export import_Export= new Import_Export();
            import_Export.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Product_Transfer transfer= new Product_Transfer();
            transfer.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Reports Reports=new Reports();
            Reports.ShowDialog();
        }
    }
}
