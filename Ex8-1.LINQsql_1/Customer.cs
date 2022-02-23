using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Ex8_1.LINQsql_1
{   [Table(Name = "Customers")]
    
    class Customer
    {   //Добавьте в класс Customer ссылку связь с классом Order и
        //в конструкторе проинициализируйте коллекцию заказов для конкретного клиента:
        private EntitySet<Order> _Orders;
        public Customer()
        {
            this._Orders = new EntitySet<Order>();
        }
        [Association(Storage = "_Orders", OtherKey = "CustomerID")]
        public EntitySet<Order> Orders
        {
            get { return this._Orders; }
            set { this._Orders.Assign(value); }
        }



        //В классе назначьте свойства CustomerID, City и CompanyName для
        //представления столбцов базы данных, для этого укажите атрибут Column:
        private string _CustomerID;
        [Column(IsPrimaryKey = true, Storage = "_CustomerID")]
        public string CustomerID
        {
            get
            {
                return this._CustomerID;
            }
            set
            {
                this._CustomerID = value;
            }
        }
        private string _City;
        [Column(Storage = "_City")]
        public string City
        {
            get
            {
                return this._City;
            }
            set
            {
                this._City = value;
            }
        }
        private string _CompanyName;
        [Column(Storage = "_CompanyName")]
        public string CompanyName
        {
            get
            {
                return this._CompanyName;
            }
            set
            {
                this._CompanyName = value;
            }
        }
        //Переопределите метод ToString() :
        public override string ToString()
        {
            return CustomerID + "\t" + City + "\t" + CompanyName;
        }
        
    }
}

