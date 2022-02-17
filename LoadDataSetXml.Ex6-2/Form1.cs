using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoadDataSetXml.Ex6_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //В поле клсса фломы создайте нетипизированный объект DataSet:
        DataSet NorthwindDataSet = new DataSet();

        //----------------------LoadSchemaButton------------------------------
        private void LoadSchemaButton_Click(object sender, EventArgs e)
        {
            //Загрузите сведения схемы из файла .xsd:
            NorthwindDataSet.ReadXmlSchema("Northwind.xsd");
            //Свяжите CustomersGrid и OrdersGrid для отображения данных:
            CustomersGrid.DataSource = NorthwindDataSet.Tables["Customers"];
            OrdersGrid.DataSource = NorthwindDataSet.Tables["Orders"];

        }
        //-----------------------LoadDataButton-------------------------------
        private void LoadDataButton_Click(object sender, EventArgs e)
        {
            //загрузите данные в набор данных:
            NorthwindDataSet.ReadXml("Northwind.xml");
        }
    }
}
