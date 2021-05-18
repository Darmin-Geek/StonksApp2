using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using SQLite;
namespace StonksApp2.Models
{
    public class Bank
    {
        [PrimaryKey]
        public string UserID { get; set; }
        public double Money { get; set; }
    }
}