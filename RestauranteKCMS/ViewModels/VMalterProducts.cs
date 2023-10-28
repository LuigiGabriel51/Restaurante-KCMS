using Android.Content;
using MvvmHelpers;
using RestauranteKCMS.Models;
using RestauranteKCMS.Services;
using System;
using System.Collections.Generic;
using System.Text;

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

    }
}
