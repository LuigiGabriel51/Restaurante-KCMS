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
        private DBcontext dbcontext; // Acesso ao banco de dados

        private string _namecategory; // Nome da categoria
        public string NameCategory
        {
            get { return _namecategory; }
            set
            {
                _namecategory = value;
            }
        }

        public static string itemSelected { get; set; } // Uma propriedade estática para o item selecionado

        public List<string> Icons { get; set; } // Lista de ícones para a categoria

        public ICommand RegisterCommand => new Command(Register); // Comando para registrar uma nova categoria

        public VMaddcategory()
        {
            dbcontext = new DBcontext(); // Inicializa o contexto do banco de dados

            Icons = new List<string>()
        {
            // Inicializa a lista de ícones com alguns valores
            IconFont.PizzaSlice, IconFont.MugHot, IconFont.FishFins,
            IconFont.Seedling, IconFont.MartiniGlassEmpty, IconFont.AppleWhole,
            IconFont.Hotdog, IconFont.IceCream, IconFont.Cookie,
            IconFont.Cheese, IconFont.BowlRice
        };
        }

        // Método chamado quando o comando de registro é executado
        public void Register()
        {
            if (!String.IsNullOrEmpty(NameCategory))
            {
                // Cria uma nova instância de Categoria com os valores fornecidos
                Category newCategory = new Category()
                {
                    Name = NameCategory,
                    Icon = itemSelected.ToString(),
                };

                // Chama o método CreateCategory do contexto do banco de dados para adicionar a nova categoria.
                dbcontext.CreateCategory(newCategory);

                // Exibe uma mensagem de toast para informar que a nova categoria foi adicionada
                Context context = Android.App.Application.Context;
                string text = "Nova categoria adicionada";
                ToastLength duration = ToastLength.Short;
                var toast = Toast.MakeText(context, text, duration);
                toast.Show();
            }
        }
    }
}
