using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBindingSimple.Ex5_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //В поле класса формы объявите экземпляр класса BindingSource:
        private BindingSource productsBindingSource;

        //обработчик события загрузки формы
        private void Form1_Load(object sender, EventArgs e)
        {
            //Загрузите данные в таблицу Products:
            productsTableAdapter1.Fill(northwindDataSet1.Products);
            //Создайте BindingSource для таблицы Products:
            productsBindingSource = new BindingSource(northwindDataSet1, "Products");
            //Настройте связывание для TextBox:
            ProductIDTextBox.DataBindings.Add("Text", productsBindingSource, "ProductID");
            ProductNameTextBox.DataBindings.Add("Text", productsBindingSource, "ProductName");
        }
        //------------------PreviousButton-----------------------------
        private void PreviousButton_Click(object sender, EventArgs e)
        {
            //Реализуйте в обработчике события Click кнопки Previous перемещение
            //к предыдущей записи в источнике данных:
            productsBindingSource.MovePrevious();
        }

        //-----------------NextButton----------------------------------
        private void NextButton_Click(object sender, EventArgs e)
        {
            //Реализуйте в обработчике события Click кнопки Next перемещение к
            //следующей записи в источнике данных:
            productsBindingSource.MoveNext();
        }
    }
}