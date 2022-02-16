using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DataAdapterProgram.Ex4_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        static SqlConnection NorthwindConnection =
        new SqlConnection(@"Data Source=(localDB)\MSSQLLocalDB;Initial Catalog=Northwind;Integrated Security=True");
        static string query = "SELECT * FROM Customers";
        
        //создайте объект адаптера (SqlDataAdapter) с передачей в конструктор переменной запроса и объекта подключения:
        static SqlDataAdapter SqlDataAdapter1 = new SqlDataAdapter(query, NorthwindConnection);
        
        //Создайте объект DataSet (можно его создать с заданным именем набора данных – Northwind):
        DataSet NorthwindDataset = new DataSet("Northwind");

        //Для применения класса SqlCommandBuilder, который позволяет автоматически сгенерировать выражения вставки,
        //обновления и удаления, вызовите его конструктор, в который передайте используемый адаптер:
        SqlCommandBuilder commands = new SqlCommandBuilder(SqlDataAdapter1);

        //Создайте обработчик события Load формы и в нем сначала вызовите метод Fill для заполнения набора данных,
        //а затем свяжите заполненный набор с элементом формы для отображения:
        private void Form1_Load(object sender, EventArgs e)
        {
            SqlDataAdapter1.Fill(NorthwindDataset, "Customers");
            dataGridView1.DataSource = NorthwindDataset.Tables["Customers"];
        }

        //--------------------"Обновить"-----------------------------------

        //В обработчике события Click первой кнопки вызовите метод адаптера данных Update для сохранения изменений в
        //базе данных, а перед этим вызовите метод EndInit() (использование методов BeginInit и EndInit предотвращает
        //использование элемента управления до полной инициализации):
        private void button1_Click(object sender, EventArgs e)
        {
            NorthwindDataset.EndInit();
            SqlDataAdapter1.Update(NorthwindDataset.Tables["Customers"]);
        }
        //------------------"Добавить строку"----------------------------
        //В обработчике события Click второй кнопки создайте новую строку методом NewRow(), заполните ее ячейки данными
        //с помощью массива (в общем случае данные могут быть введены в другие элементы формы):
        private void button2_Click(object sender, EventArgs e)
        {
            DataRow CustRow = NorthwindDataset.Tables["Customers"].NewRow();
            Object[] CustRecord = { "AAAAA", "Alfreds Futterkiste", "Maria Anders", "Sales Representative", 
                "Obere Str. 57", "Berlin", null, "12209", "Germany", "030-0074321", "030-0076545" }; 
            CustRow.ItemArray = CustRecord;
            NorthwindDataset.Tables["Customers"].Rows.Add(CustRow);

            SqlDataAdapter1.Update(NorthwindDataset.Tables["Customers"]);

        }

        //----------------------"Удалить строку"--------------------------
        //В обработчике события Click третьей кнопки получите сначала индекс строки с активной ячейкой,
        //а затем вызовите метод Delete()для удаления этой строки:
        private void button3_Click(object sender, EventArgs e)
        {
            NorthwindDataset.EndInit(); 
            var index = dataGridView1.CurrentRow.Index; 
            NorthwindDataset.Tables["Customers"].Rows[index].Delete();

            SqlDataAdapter1.Update(NorthwindDataset.Tables["Customers"]);
        }
    }
}
