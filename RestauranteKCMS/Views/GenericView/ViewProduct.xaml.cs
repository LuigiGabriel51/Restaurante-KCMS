using Android.Content;
using Android.Widget;
using RestauranteKCMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RestauranteKCMS.Views.GenericView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewProduct : ContentPage
    {
        Product Product;
        public ViewProduct(Product product)
        {
            InitializeComponent();
            this.Product = product;
            BindingContext = product;
        }

        private void add(object sender, EventArgs e)
        {
            int unidadeI = Convert.ToInt32(unidade.Text);
            unidade.Text = Convert.ToString(unidadeI + 1);
            price.Text = $"Total:    R$ {Product.price * (unidadeI + 1)}";
        }

        private void sub(object sender, EventArgs e)
        {
            int unidadeI = Convert.ToInt32(unidade.Text);
            if (unidadeI > 0)
            {
                unidade.Text = Convert.ToString(unidadeI - 1);
                price.Text = $"Total:    R$ {Product.price * (unidadeI - 1)}";
            }
        }

        private void confirm(object sender, EventArgs e)
        {
            int unidadeI = Convert.ToInt32(unidade.Text);
            if (unidadeI > 0)
            {
                Context context = Android.App.Application.Context;
                string text = "Compra realizada!";
                ToastLength duration = ToastLength.Short;

                var toast = Toast.MakeText(context, text, duration);
                toast.Show();
            }
        }
    }
}