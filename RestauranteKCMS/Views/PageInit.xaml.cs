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
            // Redireciona o usuário para a página inicial (HomePage) usando o Shell
            Shell.Current.GoToAsync("///HomePage");
        }
    }

}