using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ex8_1.LINQsql_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //В контрукторе класса формы создайте объект класса
            //DataContext, который задает входную точку в базу данных,
            //и в качестве параметра конструктора укажите строку подключения к базе Northwind:
            db = new DataContext(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=Northwind;Integrated Security=True");
        }
        //В поле класса формы объявите ссылку на контекст данных:
        DataContext db;

        private void button1_Click(object sender, EventArgs e)
        {
            //Составьте запрос для вывода данных из объекта db тех заказчиков,
            //которые живут в Лондоне (для объекта класса DataContext используйте
            //метод GetTable<>(), который возвращает коллекцию определенного типа, в данном случае типа Customer;
            //Результат запроса верните с список.
            var results = from c in db.GetTable<Customer>()
                          where c.City == "London"
                          select c;
            foreach (var c in results)
                listBox1.Items.Add(c.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Создайте нового клиента с требуемыми свойствами, например:
            Customer cust = new Customer();
            cust.CustomerID = "WINGT";
            cust.City = "London";
            cust.CompanyName = "Steve Lasker";
            //Добавьте созданный объект таблицу Customers с помощью метода InsertOnSubmit()
            //и вызовите метод SubmitChanges() для сохранения изменений в объекте db:
            db.GetTable<Customer>().InsertOnSubmit(cust);
            db.SubmitChanges();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Создайте запрос на извлечение требуемого клиента CustomerID
            //которого равен WINGT, например:
            var deleteIndivCust =
                 from cust in db.GetTable<Customer>()
                 where cust.CustomerID == "WINGT"
                 select cust;
            //После подтверждения извлечения строки клиента вызывите метод DeleteOnSubmit()
            //для удаления объекта из коллекции и после этого вызовите метод SubmitChanges()
            //для сохранения изменений в контексте данных:
            if (deleteIndivCust.Count() > 0)
            {
                db.GetTable<Customer>().DeleteOnSubmit(deleteIndivCust.First());
                db.SubmitChanges();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //укажите запрос на получение связанных данных и реализуйте
            //вывод результата в таблицу компонента listView:
            var custQuery =
                from cust in db.GetTable<Customer>()
                where cust.Orders.Any()
                select cust;
            foreach (var custObj in custQuery)
            {
                ListViewItem item =
                listView1.Items.Add(custObj.CustomerID.ToString());
                item.SubItems.Add(custObj.City.ToString());
                item.SubItems.Add(custObj.Orders.Count.ToString());
            }
        }
    }
}
