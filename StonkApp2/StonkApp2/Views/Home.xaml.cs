using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using StonksApp2.ViewModels;

namespace StonksApp2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        public Home(HomeViewModel viewModel)
        {
            InitializeComponent();
            viewModel.Navigation = Navigation;
            BindingContext = viewModel;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((HomeViewModel) BindingContext).GetBalance();
        }
    }
}