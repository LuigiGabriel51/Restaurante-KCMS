using Android.App;
using Android.Content;
using Android.Service.Voice;
using Android.Widget;
using RestauranteKCMS.Models;
using RestauranteKCMS.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace RestauranteKCMS.ViewModels
{
    internal class VMaddcategory
    {
        private DBcontext dbcontext;
        private string _namecategory;
        public string NameCategory
        {
            get { return _namecategory; }
            set
            {
                _namecategory = value;
            }
        }

        public static string itemSelected { get; set; }

        public List<string> Icons {  get; set; }

        public ICommand RegisterCommand => new Command(Register);
        public VMaddcategory() 
        {
            dbcontext = new DBcontext();
            Icons = new List<string>()
        {
            IconFont.PizzaSlice, IconFont.MugHot, IconFont.FishFins,
            IconFont.Seedling, IconFont.MartiniGlassEmpty, IconFont.AppleWhole,
            IconFont.Hotdog, IconFont.IceCream, IconFont.Cookie,
            IconFont.Cheese, IconFont.BowlRice
        };
        }
        public void Register()
        {
            if (!String.IsNullOrEmpty(NameCategory))
            {
                Category newCategory = new Category()
                {
                    Name = NameCategory,
                    Icon = itemSelected.ToString(),
                };
                dbcontext.CreateCategory(newCategory);
                Context context = Android.App.Application.Context;
                string text = "Nova categoria adicionada";
                ToastLength duration = ToastLength.Short;

                var toast = Toast.MakeText(context, text, duration);
                toast.Show();
            }
        }
    }
}
