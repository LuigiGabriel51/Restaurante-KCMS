using RestauranteKCMS.Models;
using RestauranteKCMS.Services;
using RestauranteKCMS.ViewModels;
using RestauranteKCMS.Views.GenericView;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RestauranteKCMS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        DBcontext dbcontext;

        public HomePage()
        {
            InitializeComponent();
            dbcontext = new DBcontext();
        }

        private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null && e.CurrentSelection.Count > 0)
            {
                // Obtém a categoria selecionada a partir da coleção de seleção
                ProtuctsInCategory current = e.CurrentSelection[0] as ProtuctsInCategory;

                // Cria uma nova página para listar os produtos da categoria selecionada
                var page = new ListProducts(current);
                Navigation.PushAsync(page);
            }
        }

        private void RefreshView_Refreshing(object sender, EventArgs e)
        {
            // Atualiza a lista de categorias, produtos organizados e o produto recomendado
            var Categories = dbcontext.ListCategory();
            var Products = dbcontext.ListProducts();
            var productsOganized = Organize(Categories, Products);
            recommended();

            // Atualiza a fonte de dados do CollectionView
            collectionIcons.ItemsSource = productsOganized;

            // Conclui a ação de atualização
            refreshview.IsRefreshing = false;
        }

        private List<ProtuctsInCategory> Organize(List<Category> Categories, List<Product> Products)
        {
            var ProtuctsInCategories = new List<ProtuctsInCategory>();

            foreach (var category in Categories)
            {
                // Filtra os produtos pertencentes a esta categoria
                var products = Products.Where(x => x.Idcategory == category.Name).ToList();

                var protuctsIncategories = new ProtuctsInCategory()
                {
                    Category = category,
                    Products = products
                };
                ProtuctsInCategories.Add(protuctsIncategories);
            }

            return ProtuctsInCategories;
        }

        private void recommended()
        {
            Random rnd = new Random();
            var RecomendedProd = new Product();
            List<Product> prod = dbcontext.ListProducts();

            if (prod.Count > 0)
            {
                int maxIndex = prod.Count - 1;
                int randomIndex = rnd.Next(0, maxIndex);
                RecomendedProd = prod[randomIndex];
            }

            // Define o contexto de ligação (BindingContext) para o produto recomendado
            grid.BindingContext = RecomendedProd;
            frame.BindingContext = prod;
            if (prod.Count > 0)
            {
                frame.IsVisible = true;
            }
            else frame.IsVisible = false;
        }

        private void enterRecomend(object sender, EventArgs e)
        {
            Button button = sender as Button;
            Product current = button.BindingContext as Product;

            // Cria uma nova página para visualizar o produto recomendado
            var page = new ViewProduct(current);
            Navigation.PushAsync(page);
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Grid gr = sender as Grid;
            Product current = gr.BindingContext as Product;

            // Cria uma nova página para visualizar o produto recomendado
            var page = new ViewProduct(current);
            Navigation.PushAsync(page);
        }
    }

}