using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using static StonksApp2.Library.Library;
using StonksApp2.Models;
using StonksApp2.Repositories;
using System.Windows.Input;
using StonksApp2.Library;
using System.Threading.Tasks;

namespace StonksApp2.ViewModels
{
    public class BuyViewModel : ViewModel
    {
        public string StockText { get; set; }
        public string NumberOfShares { get; set; }
        public double SharePrice { get; set; }
        public double UserBalance { get; set; }

        public BuyViewModel()
        {
            Task.Run(async() => await GetBalance());
        }
        public ICommand GetPrice => new Command(async () =>
        {
            StockInfo priceInfo = await GetStockInfo(StockText);
            SharePrice = priceInfo.price;

        });

        public ICommand Buy => new Command(async () =>
        {
            await BuyStock(StockText, Int32.Parse(NumberOfShares));
        });

        public async Task GetBalance()
        {
            var info = await GetUserInfo();
            UserBalance = info.Money;
        }
    }
}