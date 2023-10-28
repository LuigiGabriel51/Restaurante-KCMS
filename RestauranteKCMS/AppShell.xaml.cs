using Java.Net;
using RestauranteKCMS.Views;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
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

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Launcher.OpenAsync("https://www.instagram.com/kcmsbrasil/");
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://github.com/kcmsbrasil"));
        }

        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            Launcher.OpenAsync("https://www.kcms.com.br/");
        }
    }
}
