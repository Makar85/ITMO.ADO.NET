using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBindingComplex.Ex5_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //--------------------------BindGridButton--------------------------------
        private void BindGridButton_Click(object sender, EventArgs e)
        {
            //Создайте новый компонент BindingSource для таблицы Products:
            //Замечание. Компонент BindingSource предоставляет уровень абстракции при
            //привязке элементов управления к данным. Элементы управления формы могут быть
            //привязаны компоненту к BindingSource вместо непосредственно к источнику данных.
            
                 BindingSource productsBindingSource = new BindingSource(northwindDataSet1, "Products");
            //Свяжите сетку с компонентом BindingSource:
            ProductsGrid.DataSource = productsBindingSource;
            //Свяжите навигатор с компонентом BindingSource:
            bindingNavigator1.BindingSource = productsBindingSource;
            //Заполните таблицу Products данными из базы данных:
            productsTableAdapter1.Fill(northwindDataSet1.Products);
        }
    }
}
