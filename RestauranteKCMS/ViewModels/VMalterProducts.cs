using Android.Content;
using MvvmHelpers;
using MvvmHelpers.Commands;
using RestauranteKCMS.Models;
using RestauranteKCMS.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace RestauranteKCMS.ViewModels
{
    internal class VMalterProducts: BaseViewModel
    {
        private DBcontext DBcontext;

        private List<Product> products;
        public List<Product> Products 
        {
            get => products;
            set => SetProperty(ref products, value);
        }

        public List<Category> categories;
        public List<Category > Categories 
        { 
            get => categories; 
            set => SetProperty(ref categories, value); 
        }
        public static List<string> NameCategories { get; set; }
        public static string nameCategory { get; set; }

        private bool refreshing;
        public bool Refreshing
        {
            get => refreshing;
            set => SetProperty(ref refreshing, value);
        }

        public ICommand Refresh => new Command(refresh);
        public VMalterProducts() 
        {
            DBcontext = new DBcontext();
            Products = DBcontext.ListProducts();
            Categories = DBcontext.ListCategory();

            var category = Categories;
            NameCategories = new List<string>();
            if (category != null)
            {
                categories = category;
                foreach (var categorie in categories)
                {
                    NameCategories.Add(categorie.Name);
                }
            }
        }

        private void refresh()
        {
            Products = DBcontext.ListProducts();
            Categories = DBcontext.ListCategory();

            var category = Categories;
            NameCategories = new List<string>();
            if (category != null)
            {
                categories = category;
                foreach (var categorie in categories)
                {
                    NameCategories.Add(categorie.Name);
                }
            }
            Refreshing = false;
        }
    }
}
