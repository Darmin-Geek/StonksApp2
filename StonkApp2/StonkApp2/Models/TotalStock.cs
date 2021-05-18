using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;

namespace StonksApp2.Models
{
    public class TotalStock
    {
        [PrimaryKey]
        public long TradeID { get; set; }
        public string UserID { get; set; }

        public string Symbol { get; set; }

        public int TotalShares { get; set; }

        public double PreviousPrice { get; set; }
    }
}