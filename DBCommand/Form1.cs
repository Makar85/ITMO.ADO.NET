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

namespace DBCommand
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //----------when button "Запрос данных" clicked------------------
        
        private void button1_Click(object sender, EventArgs e)
        {
            // Создаем объект StringBuilder для хранения результата запроса:

            StringBuilder results = new StringBuilder();

            // в блоке using указываем созданный с помощью компонента
            // объект соединения – объект класса SqlConnection и в блоке try открываем соединение:

            using (sqlConnection1)
            {
                try
                {
                    sqlConnection1.Open();
                    // Вызоваем метод, выполняющий чтение набора данных,
                    // и везвращаем результат в объект SqlDataReader:
                    SqlDataReader reader = sqlCommand1.ExecuteReader();
                    //цикле while реализуйте чтение объекта SqlDataReader,
                    //причем для разделения полей (столбцов) примените цикл for,
                    //формирующий отдельную строку результирующего набора и после
                    //каждой строки добавьте перенос на новую строку (Environment.NewLine):

                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            results.Append(reader[i].ToString() + "\t");
                        }
                        results.Append(Environment.NewLine);
                    }
                    // Выведете результаты в текстовое поле:

                    ResultsTextBox.Text = results.ToString();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка! ",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }                  

        }

        //----------when button "Пакетный запрос" clicked------------------

        private void button2_Click(object sender, EventArgs e)
        {
            // за основу берем код обработчика button1

            StringBuilder results = new StringBuilder();
                        
            using (sqlConnection1)
            {
                sqlCommand1.CommandText = "SELECT CustomerID, CompanyName FROM Customers;" + 
                    "SELECT ProductName, UnitPrice, QuantityPerUnit FROM Products;";
                try
                {
                    sqlConnection1.Open();
                    SqlDataReader reader = sqlCommand1.ExecuteReader();
                    //Чтение данный будет проходит последовательно до тех пор,
                    //пока некоторая булевая переменная будет true, добавьте ее
                    //объявление и поместите имеющийся цикл while в цикл do-while
                    //пока булевая переменная, получающая результат метода NextResult
                    //(значение true, если имеются и другие наборы результатов;
                    //в противном случае — значение false).
                    bool MoreResults = false;

                    do
                    {
                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                results.Append(reader[i].ToString() + "\t");
                            }
                            results.Append(Environment.NewLine);
                        }
                        MoreResults = reader.NextResult();
                    }
                    while (MoreResults);    

                    ResultsTextBox.Text = results.ToString();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка! ",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //----------when button "Вызов процедуры" clicked------------------
        private void button3_Click(object sender, EventArgs e)
        {
            StringBuilder results = new StringBuilder();                      
            using (sqlConnection1)
            {
                try
                {
                    sqlConnection1.Open();                
                    SqlDataReader reader = sqlCommand2.ExecuteReader();                 

                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            results.Append(reader[i].ToString() + "\t");
                        }
                        results.Append(Environment.NewLine);
                    }                  
                    ResultsTextBox.Text = results.ToString();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка! ",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        //----------when button "Создание таблицы" clicked------------------

        //В этой части упражнения рассматривается возможность выполнения
        //команды для создания новой таблицы в базе данных.
        //Для выполнения такой операции необходимо применить
        //метод ExecuteNonQuery() объекта Command.

        //Для новой кнопки добавьте обработчик события события Click
        //и в нем реализуйте создание таблиы с помощью SQL-запроса,
        //учитывая, что для выполнения команды, запускающей выражения
        //SQL вызывается метод ExecuteNonQuery() и нет необходимости
        //использовать SqlDataReader, поскольку команда не возвращает
        //никаких данных.
        private void button4_Click(object sender, EventArgs e)
        {
            using (sqlConnection1) 
            { 
                sqlCommand3.CommandText = 
                    "CREATE TABLE SalesPersons (" +
                    "[SalesPersonID] [int] IDENTITY(1,1) NOT NULL, " + 
                    "[FirstName] [nvarchar](50) NULL, " + 
                    "[LastName] [nvarchar](50) NULL)"; 
                try 
                { 
                    sqlConnection1.Open();
                    sqlCommand3.ExecuteNonQuery(); 
                    MessageBox.Show("Таблица SalesPersons создана"); 
                } 
                catch (SqlException ex) 
                {
                    MessageBox.Show(ex.Message, "Ошибка!", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                } 
            }
        }

        //----------when button "Запрос с параметром" clicked------------------
        private void button5_Click(object sender, EventArgs e)
        {
            StringBuilder results = new StringBuilder(); 
            try
            {
                sqlCommand4.Parameters["@City"].Value = CityTextBox.Text; 
                sqlConnection1.Open(); 
                SqlDataReader reader = sqlCommand4.ExecuteReader();
                while (reader.Read()) 
                { 
                    for (int i = 0; i < reader.FieldCount; i++)
                    { 
                        results.Append(reader[i].ToString() + "\t");
                    } 
                    results.Append(Environment.NewLine);
                }
                ResultsTextBox.Text = results.ToString();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            { 
                sqlConnection1.Close(); 
            }
        }
        //----------when button "Процедура с параметром" clicked------------------
        private void button6_Click(object sender, EventArgs e)
        {
            StringBuilder results = new StringBuilder();
            try
            {
                sqlCommand5.Parameters["@CategoryName"].Value = CategoryNameTextBox.Text;
                sqlCommand5.Parameters["@OrdYear"].Value = OrdYearTextBox.Text;
                sqlConnection1.Open();
                SqlDataReader reader = sqlCommand5.ExecuteReader();
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        results.Append(reader[i].ToString() + "\t");
                    }
                    results.Append(Environment.NewLine);
                }
                ResultsTextBox.Text = results.ToString();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sqlConnection1.Close();
            }
        }
    }
}
