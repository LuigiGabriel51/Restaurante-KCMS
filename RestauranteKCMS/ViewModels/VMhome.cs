using Android.Media;
using MvvmHelpers;
using RestauranteKCMS.Models;
using RestauranteKCMS.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace RestauranteKCMS.ViewModels
{
    internal class VMhome: BaseViewModel
    {
        private DBcontext dbcontext;
        private bool _refresing;
        public bool refreshing 
        {
            get => _refresing;
            set => SetProperty(ref _refresing, value);
        }
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }

        private List<ProtuctsInCategory> protuctsInCategories;
        public List<ProtuctsInCategory> ProtuctsInCategories
        {
            get => protuctsInCategories;
            set => SetProperty(ref protuctsInCategories, value);
        }



        private Product _product;
        public Product RecomendedProd
        {
            get => _product;
            set => SetProperty(ref _product, value);
        }

        private ImageSource imagesource;
        public ImageSource ImageRecomended 
        { 
            get => imagesource; 
            set => SetProperty(ref imagesource, value); 
        } 

        public string Saudacoes { get; set; }
        public VMhome()
        {
            dbcontext = new DBcontext();
            Categories = dbcontext.ListCategory();
            Products = dbcontext.ListProducts();
            Organize();
            GetDay();
        }
        private void Organize()
        {
            ProtuctsInCategories = new List<ProtuctsInCategory>();
            foreach (var category in Categories)
            {
                var products = Products.Where(x => x.Idcategory == category.Name).ToList();
                var protuctsIncategories = new ProtuctsInCategory()
                {
                    Category = category,
                    Products = products
                };
                ProtuctsInCategories.Add(protuctsIncategories);
                recommended();
            }
        }

        private void recommended()
        {
            Random rnd = new Random();
            RecomendedProd = new Product();
            List<Product> prod = dbcontext.ListProducts();
            if(prod.Count > 0)
            {
                int maxIndex = prod.Count - 1;
                int randomIndex = rnd.Next(0, maxIndex);
                RecomendedProd = prod[randomIndex];
            }
            ImageRecomended = ImageSource.FromStream(() => new MemoryStream(RecomendedProd.image));
        }
        private void GetDay()
        {
            var Hday = DateTime.Now.Hour;
            if(Hday >= 5 && Hday < 12) Saudacoes = "Bom dia, (usuário)";
            else if(Hday >= 12 && Hday < 18) Saudacoes = "Boa tarde, (usuário)";
            else Saudacoes = "Boa noite, (usuário)";
        }
    }
    public class ProtuctsInCategory
    {
        public Category Category { get; set; }
        public List<Product> Products { get; set; }
    }

}
