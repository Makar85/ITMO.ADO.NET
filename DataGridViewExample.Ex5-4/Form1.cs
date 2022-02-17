using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataGridViewExample.Ex5_4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void customersBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.customersBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.northwindDataSet);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'northwindDataSet.Customers' table. You can move, or remove it, as needed.
            this.customersTableAdapter.Fill(this.northwindDataSet.Customers);
            //Создайте новый столбец в наборе данных, который должен содержать данные о городе и стране (вычисляемый столбец), для
            //этого добавьте в обработчик события загрузки формы следующий код:
            DataColumn Location = new DataColumn("Location"); 
            Location.Expression = "City + ',' + Country"; 
            northwindDataSet.Customers.Columns.Add(Location);

        }
        //--------------------AddColumnButton-----------------------------
        private void AddColumnButton_Click(object sender, EventArgs e)
        {
            //Создайте объект столбца:
            DataGridViewTextBoxColumn LocationColumn = new DataGridViewTextBoxColumn();
            //Укажите свойства создаваемого столбца (имя объекта, надпись в сетке, имя
            //соответсвующего столбца в наборе данных с конкретной информацией):
            LocationColumn.Name = "LocationColumn";
            LocationColumn.HeaderText = "Location"; 
            LocationColumn.DataPropertyName = "Location";
            //Добавьте созданный объект в коллекцию столбцов элемента DataGridView:
            customersDataGridView.Columns.Add(LocationColumn);
        }
        //--------------------DeleteColumnButton---------------------------
        private void DeleteColumnButton_Click(object sender, EventArgs e)
        {
            //Реализуйте в обработчике события Click кнопки Delete Column удаление столбца в DataGridView:
            try
            {
                customersDataGridView.Columns.Remove("LocationColumn");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //--------------------GetClickedCellButton---------------------------
        private void GetClickedCellButton_Click(object sender, EventArgs e)
        {
            //Объявите переменную для хранения информации о выбранной ячейке:
            string CurrentCellInfo;
            //Присвойте объявленной переменной содержимое ячейки:
            CurrentCellInfo = customersDataGridView.CurrentCell.Value.ToString() + Environment.NewLine;
            //Добавьте к переменной информацию об имени столбца, а также индексах столбца и строки:
            CurrentCellInfo += "Column: " +
                customersDataGridView.CurrentCell.OwningColumn.DataPropertyName + Environment.NewLine;
            CurrentCellInfo += "Column Index: " +
                customersDataGridView.CurrentCell.ColumnIndex.ToString() + Environment.NewLine;
            CurrentCellInfo += "Row Index: " +
                customersDataGridView.CurrentCell.RowIndex.ToString() + Environment.NewLine;
            //Результирующую строку выведете в поле надписи формы:
            label1.Text = CurrentCellInfo;
        }
        //----------------------обработчик события customersDataGridView_CellValidating-----------------------------
        private void customersDataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (customersDataGridView.Columns[e.ColumnIndex].DataPropertyName == "ContactName")
            { 
                if (e.FormattedValue.ToString() == "") 
                { 
                    customersDataGridView.Rows[e.RowIndex].ErrorText =
                        "ContactName is a required field";
                    e.Cancel = true;
                } 
                else 
                    customersDataGridView.Rows[e.RowIndex].ErrorText = "";
            }
        }
        //-----------------обработчик события ApplyStyle(CheckBox)_Click
        private void ApplyStyleButton_Click(object sender, EventArgs e)
        {
            //Измените формат отображения в DataGridView, например, чередующиеся строки со светло-серым фоном,
            //для этого укажите в обработчике события Click флажка Apply Style:
            if (ApplyStyleButton.Checked == true) 
                customersDataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray; 
            else 
                customersDataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.White;

        }
    }
}
