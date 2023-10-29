using Android.Content;
using Android.Widget;
using RestauranteKCMS.Models;
using RestauranteKCMS.Services;
using RestauranteKCMS.ViewModels;
using RestauranteKCMS.Views.GenericView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RestauranteKCMS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SetCategoriaProducts : ContentPage
    {
        Product ProductSelected { get; set; }

        public SetCategoriaProducts()
        {
            InitializeComponent();
        }

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                // Define a categoria selecionada para o produto
                VMalterProducts.nameCategory = picker.Items[selectedIndex];
            }
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Product current = e.SelectedItem as Product;
            var listCategoria = VMalterProducts.NameCategories;

            // Define o contexto de ligação (BindingContext) para o produto selecionado
            card.BindingContext = current;
            ProductSelected = current;
            picker.ItemsSource = listCategoria;
            listP.IsVisible = false;
            card.IsVisible = true;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (card.IsVisible && listP.IsVisible == false)
            {
                // Limpa a seleção e altera a visibilidade dos elementos
                listP.SelectedItem = null;
                card.IsVisible = false;
                listP.IsVisible = true;
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var DBcontext = new DBcontext();

            // Define a categoria do produto selecionado
            ProductSelected.Idcategory = VMalterProducts.nameCategory;

            // Atualiza o produto no banco de dados
            var response = DBcontext.UpdateProducts(ProductSelected);

            // Exibe uma mensagem de produto atualizado
            Context context = Android.App.Application.Context;
            string text = "Produto atualizado!";
            ToastLength duration = ToastLength.Short;
            var toast = Toast.MakeText(context, text, duration);
            toast.Show();
        }

        private async void deleteProduct(object sender, EventArgs e)
        {
            bool decision = await DisplayAlert("Alerta", "Você tem certeza que deseja excluir esse produto?", "Sim", "Não");
            if (decision)
            {
                var DBcontext = new DBcontext();

                // Exclui o produto do banco de dados
                var response = DBcontext.DeleteProducts(ProductSelected);

                // Exibe uma mensagem de item excluído
                Context context = Android.App.Application.Context;
                string text = "Item excluído!";
                ToastLength duration = ToastLength.Short;
                var toast = Toast.MakeText(context, text, duration);
                toast.Show();
            }
        }
    }

}