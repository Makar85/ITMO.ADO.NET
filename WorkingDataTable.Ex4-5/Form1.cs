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

namespace WorkingDataTable.Ex4_5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Добавьте к классу формы метод, возвращающий выбранный в сетке CustomersRow:
        private NorthwindDataSet.CustomersRow GetSelectedRow()
        {
            String SelectedCustomerID = CustomersDataGridView.CurrentRow.Cells["CustomerID"].Value.ToString();
            NorthwindDataSet.CustomersRow SelectedRow =
            northwindDataSet1.Customers.FindByCustomerID(SelectedCustomerID);
            return SelectedRow;
        }

        //Добавьте к классу формы метод, обновляющий текстовые поля версиями и состояниями строки:
        private void UpdateRowVersionDisplay()
        {
            try
            {
                CurrentDRVTextBox.Text = GetSelectedRow()[CustomersDataGridView.CurrentCell.OwningColumn.Name, DataRowVersion.Current].ToString();
            }
            catch (Exception ex)
            {
                CurrentDRVTextBox.Text = ex.Message;
            }
            try
            {
                OriginalDRVTextBox.Text = GetSelectedRow()[CustomersDataGridView.CurrentCell.OwningColumn.Name, DataRowVersion.Original].ToString();
            }
            catch (Exception ex)
            {
                OriginalDRVTextBox.Text = ex.Message;
            }
            //В этом же методе отобразите текущий RowState выбранной строки:
            RowStateTextBox.Text = GetSelectedRow().RowState.ToString();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CustomersDataGridView.DataSource = northwindDataSet1.Customers;
            CustomersDataGridView.MultiSelect = false;
            CustomersDataGridView.SelectionMode = DataGridViewSelectionMode.CellSelect;
            CustomersDataGridView.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        //--------------------FillTableButton----------------------------
        private void FillTableButton_Click(object sender, EventArgs e)
        {
            sqlDataAdapter1.Fill(northwindDataSet1.Customers);
        }


        //--------------------AddRowButton-------------------------------
        private void AddRowButton_Click(object sender, EventArgs e)
        {
            //Создайте новый экземпляр строки Customers:
            NorthwindDataSet.CustomersRow NewRow = (NorthwindDataSet.CustomersRow)northwindDataSet1.Customers.NewRow();
            //Присвойте значения каждому столбцу строки (или можно сделать как в упражнении 3):
            NewRow.CustomerID = "WINGT";
            NewRow.CompanyName = "Wingtip Toys";
            NewRow.ContactName = "Steve Lasker";
            NewRow.ContactTitle = "CEO";
            NewRow.Address = "1234 Main Street";
            NewRow.City = "Buffalo";
            NewRow.Region = "NY";
            NewRow.PostalCode = "98052";
            NewRow.Country = "USA";
            NewRow.Phone = "206-555-0111";
            NewRow.Fax = "206-555-0112";
            //Реализуйте добавление строки к коллекции Rows таблицы Customers:
            try
            {
                northwindDataSet1.Customers.Rows.Add(NewRow);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add Row Failed");
            }

        }
        //--------------------DeleteRowButton------------------------------
        private void DeleteRowButton_Click(object sender, EventArgs e)
        {
            GetSelectedRow().Delete();
        }

        //--------------------UpdateValueButton------------------------------
        private void UpdateValueButton_Click(object sender, EventArgs e)
        {
            GetSelectedRow()[CustomersDataGridView.CurrentCell.OwningColumn.Name] = CellValueTextBox.Text;
            UpdateRowVersionDisplay();
        }

        //-------------------обработчик события CustomersDataGridView Click-----------
        private void CustomersDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Заполните CellValueTextBox значением выбранной ячейки
            CellValueTextBox.Text = CustomersDataGridView.CurrentCell.Value.ToString();
            //Обновите другие текстовые поля:
            UpdateRowVersionDisplay();
        }

        //--------------------AcceptChangesButton------------------------------
        private void AcceptChangesButton_Click(object sender, EventArgs e)
        {
            GetSelectedRow().AcceptChanges();
            UpdateRowVersionDisplay();
        }

        //--------------------RejectChangesButton------------------------------
        private void RejectChangesButton_Click(object sender, EventArgs e)
        {
            GetSelectedRow().RejectChanges();
            UpdateRowVersionDisplay();
        }
    }
}

