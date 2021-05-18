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

using StonkApp2.Views;

namespace StonksApp2.ViewModels
{
    public class HomeViewModel : ViewModel
    {
        public double UserBalance { get; set; }

        public HomeViewModel()
        {
            UserBalance = 27;
            GetBalance();
            

        }

        public async Task GetBalance()
        {
            var info = await GetUserInfo();
            Console.WriteLine("The Balance Has Been Obtained!!555555");
            UserBalance = info.Money;
            Console.WriteLine("The Balance Has Been Obtained!!22222");
        }

        public ICommand GoToBuy => new Command(async () =>
            {
                await Navigation.PushAsync(new Buy(new BuyViewModel()));
            }
        );
        public ICommand GoToSell => new Command(async () =>
        {
           await Navigation.PushAsync(new Sell(new SellViewModel()));
        }
        );

    }
}