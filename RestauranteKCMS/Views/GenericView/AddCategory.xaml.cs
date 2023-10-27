﻿using RestauranteKCMS.ViewModels;
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
    public partial class AddCategoria : ContentPage
    {
        public AddCategoria()
        {
            InitializeComponent();
        }

        private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.CurrentSelection != null && e.CurrentSelection.Count > 0) 
            {
                string current = e.CurrentSelection[0] as string;
                VMaddcategory.itemSelected = current;
            }
        }
    }
}