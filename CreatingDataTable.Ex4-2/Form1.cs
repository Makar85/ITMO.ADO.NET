using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CreatingDataTable.Ex4_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            MakeDataTables();
        }
        private DataSet dataSet; 
        private BindingSource bindingSource1; 
        private BindingSource bindingSource2;

        //метод который создаёт таблицу и столбцы и строки
        private void MakeParentTable()
        {
            DataTable table = new DataTable("ParentTable");

            //объявите две ссылки: на столбец DataColumn и на строку DataRow:
            DataColumn column; 
            DataRow row;

            // создаем первый столбец
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "id"; 
            column.ReadOnly = true; 
            column.Unique = true;
            
            table.Columns.Add(column);
            // создаем второй столбец
            column = new DataColumn(); 
            column.DataType = System.Type.GetType("System.String"); 
            column.ColumnName = "ParentItem"; 
            column.AutoIncrement = false;
            column.Caption = "ParentItem"; 
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);

            // создаем третий и четвертый столбцы другим методом

            table.Columns.Add("Total", typeof(Double)); 
            table.Columns.Add("SalesTax", typeof(Double), "Total * 0.13");

            //Укажите первый столбец в качестве первичного ключа таблицы.
            //Для этого сначала создайте массив из одного или в общем случае
            //нескольких объектов DataColumn, потом сформируйте ключ(можно из нескольких столбцов)
            //добавив столбец id в массив, а затем с помощью свойства таблицы PrimaryKey назначьте первичный ключ:

            DataColumn[] PrimaryKeyColumns = new DataColumn[1];
            PrimaryKeyColumns[0] = table.Columns["id"]; 
            table.PrimaryKey = PrimaryKeyColumns;

            //Создайте набор данных (DataSet) и добавьте в него созданную таблицу:
            dataSet = new DataSet(); 
            dataSet.Tables.Add(table);

            //Чтобы добавить новую строку сначала вызовите для таблицы метод NewRow()
            //и возвращаемый результат присвойте ранее объявленной ссылке на DataRow,
            //затем, используя имя столбца или индекс заполните ячейки произвольными
            //данными и. наконец, добавьте созданный объект строки в коллекцию строк
            //таблицы(для удобства примера используется цикл):
            
            for (int i = 0; i <= 2; i++)
            { 
                row = table.NewRow(); 
                row["id"] = i; 
                row["ParentItem"] = "ParentItem " + i;
                row["Total"] = 2 * i + 0.5; 
                table.Rows.Add(row); 
            }

        }

        //метод который создаёт вторую таблицу и столбцы и строки аналогично первой

        private void MakeChildTable()
        {
            DataTable table = new DataTable("childTable");
            DataColumn column;
            DataRow row;

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "ChildID"; column.AutoIncrement = true;
            column.Caption = "ID";
            column.ReadOnly = true;
            column.Unique = true;
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ChildItem";
            column.AutoIncrement = false;
            column.Caption = "ChildItem";
            column.ReadOnly = false;
            column.Unique = false;
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "ParentID";
            column.AutoIncrement = false;
            column.Caption = "ParentID";
            column.ReadOnly = false;
            column.Unique = false;
            table.Columns.Add(column);

            dataSet.Tables.Add(table);

            for (int i = 0; i <= 4; i++)
            {
                row = table.NewRow();
                row["childID"] = i;
                row["ChildItem"] = "Item " + i; row["ParentID"] = 0; table.Rows.Add(row);
            }

            for (int i = 0; i <= 4; i++)
            {
                row = table.NewRow();
                row["childID"] = i + 5;
                row["ChildItem"] = "Item " + i;
                row["ParentID"] = 1; table.Rows.Add(row);
            }
            for (int i = 0; i <= 4; i++)
            {
                row = table.NewRow();
                row["childID"] = i + 10;
                row["ChildItem"] = "Item " + i; row["ParentID"] = 2;
                table.Rows.Add(row);
            }
        }

        //Определите метод MakeDataRelation(). В нем сначала создайте две ссылки DataColumn на столбцы,
        //которые служат в качестве родительских и дочерних столбцов связи, а затем объект DataRelation,
        //определяющий необходимое отношение между таблицами:


        private void MakeDataRelation()
        
        {
            // DataRelation requires two DataColumn 
            // (parent and child) and a name.
            DataColumn parentColumn = dataSet.Tables["ParentTable"].Columns["id"];
            DataColumn childColumn = dataSet.Tables["ChildTable"].Columns["ParentID"];
            DataRelation relation = new DataRelation("parent2Child", parentColumn, childColumn); 
            dataSet.Tables["ChildTable"].ParentRelations.Add(relation);

        }


        //Определите метод BindToDataGrid(), в котором создайте два объекта BindingSource,
        //которые будут использоваться для привязки данных к элементу формы, данными будут
        //являться созданные таблицы:

        private void BindToDataGrid()
        {
            bindingSource1 = new BindingSource();           
            bindingSource2 = new BindingSource();
            bindingSource1.DataSource = dataSet.Tables["ParentTable"];
            bindingSource2.DataSource = dataSet.Tables["childTable"];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = bindingSource1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView2.DataSource = bindingSource2;
        }
        //Определите метод MakeDataTables(), в котором последовательно вызовите
        //ранее созданные методы согласно алгоритму создания набора данных:
        private void MakeDataTables()
        {
            MakeParentTable();
            MakeChildTable();
            MakeDataRelation();
            BindToDataGrid();
        }
    }
}
