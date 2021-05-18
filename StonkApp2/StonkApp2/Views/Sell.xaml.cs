using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using StonksApp2.ViewModels;

namespace StonkApp2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Sell : ContentPage
    {
        public Sell(SellViewModel viewModel)
        {
            InitializeComponent();
            viewModel.Navigation = Navigation;
            BindingContext = viewModel;
        }
    }
}