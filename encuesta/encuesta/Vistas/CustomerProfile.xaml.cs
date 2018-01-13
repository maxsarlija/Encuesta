
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace encuesta.Vistas
{
    public partial class CustomerProfile : ContentPage
    {

        public Customer SelectedCustomer { get; set; }

        public CustomerProfile(Customer _customer)
        {
            InitializeComponent();

            SelectedCustomer = _customer;

            Title = SelectedCustomer.Name;
            CustomerName.Text = SelectedCustomer.Name;
            CustomerAddress.Text = SelectedCustomer.Address;
        }


        async void BtnViewSurveys_OnClick(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new Vistas.CustomerSurveys(SelectedCustomer));
        }

        async void BtnStartSurvey_OnClick(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new Vistas.CustomerNewSurvey(SelectedCustomer));
        }


    }
}