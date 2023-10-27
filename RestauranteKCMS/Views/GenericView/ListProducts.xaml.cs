using RestauranteKCMS.Models;
using RestauranteKCMS.ViewModels;
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
	public partial class ListProducts : ContentPage
	{
		public ListProducts (ProtuctsInCategory category)
		{
			InitializeComponent ();
			layout.BindingContext = category;
		}
	}
}