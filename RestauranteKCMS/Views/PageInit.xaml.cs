using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RestauranteKCMS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageInit : ContentPage
    {
        public PageInit()
        {
            InitializeComponent();
        }

        private void EnterButton(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("///HomePage");
        }
    }
}