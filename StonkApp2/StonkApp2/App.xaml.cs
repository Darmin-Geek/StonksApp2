using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using StonksApp2.Views;
using StonksApp2.ViewModels;



namespace StonkApp2
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Home(new HomeViewModel()));
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
