using RestauranteKCMS.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace RestauranteKCMS
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(PageInit), typeof(PageInit));

        }
    }
}
