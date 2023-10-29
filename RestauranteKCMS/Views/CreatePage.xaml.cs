using RestauranteKCMS.Views.GenericView;
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
    public partial class CreatePage : ContentPage
    {
        public CreatePage()
        {
            InitializeComponent();
        }

        private void pushAddCategory(object sender, EventArgs e)
        {
            // Cria uma nova página para adicionar uma categoria e a empilha na pilha de navegação
            var page = new AddCategoria();
            Navigation.PushAsync(page);
        }

        private void pushAddProduct(object sender, EventArgs e)
        {
            // Cria uma nova página para adicionar um produto e a empilha na pilha de navegação
            var page = new AddProduct();
            Navigation.PushAsync(page);
        }
    }

}