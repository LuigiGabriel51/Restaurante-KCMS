using Android.App.AppSearch;
using RestauranteKCMS.Models;
using RestauranteKCMS.ViewModels;
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
    public partial class ListProducts : ContentPage
    {
        ProtuctsInCategory category;

        public ListProducts(ProtuctsInCategory category)
        {
            InitializeComponent();
            this.category = category;
            layout.BindingContext = this.category;
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchBar searchBar = (SearchBar)sender;

            // Filtra a lista de produtos com base no texto da barra de pesquisa
            Listproducts.ItemsSource = this.category.Products
                .Where(x => x.name.ToLowerInvariant().Contains(searchBar.Text))
                .ToList();
        }

        private void Listproducts_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Product current = e.SelectedItem as Product;

            // Cria uma nova página para exibir detalhes do produto selecionado
            var page = new ViewProduct(current);
            Navigation.PushAsync(page);
        }
    }

}