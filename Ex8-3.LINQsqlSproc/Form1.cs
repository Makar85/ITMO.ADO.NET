using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Linq;
using System.Data.Linq.Mapping;


namespace Ex8_3.LINQsqlSproc
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //В поле класса формы создайте подключения к базе данных:
        Northwnd db = new Northwnd(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=Northwind;Integrated Security=True");

        //--------------------button "Подробности заказа"-----------------------------------
        private void button1_Click(object sender, EventArgs e)
        {
            //Объявите переменную для хранения содержимого textBox1 в качестве аргумента для хранимой процедуры:
            string param = textBox1.Text;
            //Объявите переменную для хранения результатов, возвращаемых хранимой процедурой CustOrdersDetail:
            var custquery = db.CustOrdersDetail(Convert.ToInt32(param));
            //Выполните хранимую процедуру и отобразите результаты (для упрощения примера используется стандартное окно):
            string msg = "";
            foreach (CustOrdersDetailResult custOrdersDetail in custquery)
            {
                msg = msg + custOrdersDetail.ProductName + "\n";
            }
            if (msg == "")
                msg = "No results.";
            MessageBox.Show(msg);
            //Очистите переменные для дальнейшего использования:
            param = "";
            textBox1.Text = "";
        }

        //------------------button "История заказа"-----------------------------------
        private void button2_Click(object sender, EventArgs e)
        {
            //реализуйте функциональность подобную для кнопки Подробности заказа для хранимой процедуры CustOrderHist:
            string param = textBox2.Text;
            var custquery = db.CustOrderHist(param);
            string msg = "";
            foreach (CustOrderHistResult custOrdHist in custquery)
            {
                msg = msg + custOrdHist.ProductName + "\n";
            }
            MessageBox.Show(msg);
            param = "";
            textBox2.Text = "";
        }
    }
}
