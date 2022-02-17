using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SavingDataSetXml.Ex6_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //----------------------FillDataSetButton---------------------------
        private void FillDataSetButton_Click(object sender, EventArgs e)
        {
            //Заполните таблицы Cusromers и Orders:
            CustomersAdapter.Fill(northwindDataSet1.Customers); 
            OrdersAdapter.Fill(northwindDataSet1.Orders);
            //Свяжите сетку с таблицей Cusromers:
            CustomersGrid.DataSource = northwindDataSet1.Customers;

        }

        //---------------------SaveXmlDataButton---------------------------
        private void SaveXmlDataButton_Click(object sender, EventArgs e)
        {
            //сохраните данные northwindDataSet1 в файл XML:
            try 
            { 
                northwindDataSet1.WriteXml("Northwind.xml"); 
                MessageBox.Show("Data save as XML"); 
            } 
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); 
            }
        }
        //----------------------SaveXmlSchemaButton-------------------------
        private void SaveXmlSchemaButton_Click(object sender, EventArgs e)
        {
            //сохраните данные схемы объекта northwindDataSet1 в файл XML:
            try 
            { 
                northwindDataSet1.WriteXmlSchema("Northwind.xsd");
                MessageBox.Show("Schema save as XML"); 
            } 
            catch (Exception ex)
            { 
                MessageBox.Show(ex.Message);
            }
        }
    }
}
