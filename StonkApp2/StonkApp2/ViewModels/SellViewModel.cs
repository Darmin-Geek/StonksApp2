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
using System.Collections.ObjectModel;

namespace StonksApp2.ViewModels
{
    public class  SellViewModel : ViewModel
    {
        public string StockText { get; set; }
        public string NumberOfShares { get; set; }
        public double UserBalance { get; set; }

        public Xamarin.Forms.ListView ShownStocksView = new Xamarin.Forms.ListView();
        public ObservableCollection<string> ShownStocks { get; set; }

        public SellViewModel()
        {
            ShownStocks = new ObservableCollection<string>();
            GetBalance();
            OwnedStocks();
        }

        public async Task GetBalance()
        {
            var info = await GetUserInfo();
            UserBalance = info.Money;
        }

        public ICommand Sell => new Command(async () =>
        {
            await SellStock(StockText, Int32.Parse(NumberOfShares));
            await OwnedStocks();
        });

        public async Task OwnedStocks()
        {
            var UserInfo = await GetUserInfo();
            var OwnedStocks = UserInfo.OwnedStocks;
            ShownStocks.Clear();
           
            foreach (TotalStock stock in OwnedStocks)
            {
                
                string symbol = stock.Symbol;
                double previousprice = stock.PreviousPrice;
                int numberofshares = stock.TotalShares;
                string toAdd = $"Symbol {symbol} Number of Shares {numberofshares} Previous Price {previousprice}";
                System.Console.WriteLine(toAdd);
            ShownStocks.Add(toAdd);
            }

            //ShownStocks = new ObservableCollection<TotalStock>(OwnedStocks);
            RaisePropertyChanged(nameof(ShownStocks));
            ShownStocksView.ItemsSource = ShownStocks;

        }
    }
}