using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataViewExample.Ex4_6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //В поле класса формы объявите ссылки на классы DataView для таблиц Customers и Orders:
        DataView customersDataView; 
        DataView ordersDataView;

        //-------------обработчик события загрузки формы--------------------------
        private void Form1_Load(object sender, EventArgs e)
        {
            //Загрузите данные в таблицы Customers и Orders:
            customersTableAdapter1.Fill(northwindDataSet1.Customers);
            ordersTableAdapter1.Fill(northwindDataSet1.Orders);

            //Настройте объекты DataView для отображения таблиц Customers и Orders:
            customersDataView = new DataView(northwindDataSet1.Customers); 
            ordersDataView = new DataView(northwindDataSet1.Orders);

            //Присвойте исходный порядок сортировки в DataView:
            customersDataView.Sort = "CustomerID";
            //Настройте CustomerGrid для отображения содержимого CustomerDataView:
            CustomersGrid.DataSource = customersDataView;



        }

        //---------------------SetDataViewPropertiesButton-----------------------------
        private void SetDataViewPropertiesButton_Click(object sender, EventArgs e)
        {
            //В обработчике события Click кнопки Set DataView Properties реализуйте
            //сортировку и фильтрацию данных в зависимости от значений, введенных
            //в элементы SortTextBox и FilterTextBox соответственно:
            customersDataView.Sort = SortTextBox.Text;
            customersDataView.RowFilter = FilterTextBox.Text;

        }

        //---------------------AddRowButton-----------------------------
        private void AddRowButton_Click(object sender, EventArgs e)
        {
            //Создайте новую строку:
            DataRowView newCustomRow = customersDataView.AddNew();
            //Присвойте значения столбцам CustomerID и CompanyName значения
            //(для примера этого хватит, но в реальном случае потребуется
            //организаовать создание полной строки):
            newCustomRow["CustomerID"] = "WINGT";
            newCustomRow["CompanyName"] = "Wing Tip Toys";
            //Укажите явное окончание редактирования:
            newCustomRow.EndEdit();



        }

        //---------------------GetOrdersButton-----------------------------
        private void GetOrdersButton_Click(object sender, EventArgs e)
        {
            //Получите CustomerID для строки, выбранной в CustomersGrid:
            string selectedCustomerID = (string)CustomersGrid.SelectedCells[0].OwningRow.Cells["CustomerID"].Value;
            //Создайте DataRowView и присвойте ему выбранную строку:
            DataRowView selectedRow = customersDataView[customersDataView.Find(selectedCustomerID)];
            //Вызовите метод CreateChildView() для перемещения по записям и
            //создания нового DataView, основанного на связанных записях:
            ordersDataView = selectedRow.CreateChildView(northwindDataSet1.Relations
            ["FK_Orders_Customers"]);
            //Настройте OrderGrid для отображения связанного DataView:
            OrdersGrid.DataSource = ordersDataView;
        }
    }
}
