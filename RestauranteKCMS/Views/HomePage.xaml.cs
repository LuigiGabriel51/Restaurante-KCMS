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
                ProtuctsInCategory current = e.CurrentSelection[0] as ProtuctsInCategory;
                var page = new ListProducts(current);
                Navigation.PushAsync(page);
            }
        }

        private void RefreshView_Refreshing(object sender, EventArgs e)
        {
            var Categories = dbcontext.ListCategory();
            var Products = dbcontext.ListProducts();
            var productsOganized = Organize(Categories, Products);
            recommended();
            collectionIcons.ItemsSource = productsOganized;
            refreshview.IsRefreshing = false;
        }

        private List<ProtuctsInCategory> Organize(List<Category> Categories, List<Product> Products)
        {
            var ProtuctsInCategories = new List<ProtuctsInCategory>();
            foreach (var category in Categories)
            {
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
            grid.BindingContext = RecomendedProd;
        }


        private void enterRecomend(object sender, EventArgs e)
        {
            Button button = sender as Button;
            Product current = button.BindingContext as Product;
            var page = new ViewProduct(current);
            Navigation.PushAsync(page);
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Grid gr = sender as Grid;
            Product current = gr.BindingContext as Product;
            var page = new ViewProduct(current);
            Navigation.PushAsync(page);
        }
    }
}