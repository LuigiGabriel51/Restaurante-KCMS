using Android.Content;
using MvvmHelpers;
using MvvmHelpers.Commands;
using RestauranteKCMS.Models;
using RestauranteKCMS.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace RestauranteKCMS.ViewModels
{
    internal class VMalterProducts : BaseViewModel
    {
        private DBcontext DBcontext; // Acesso ao banco de dados

        private List<Product> products; // Lista de produtos
        public List<Product> Products
        {
            get => products;
            set => SetProperty(ref products, value);
        }

        public List<Category> categories; // Lista de categorias
        public List<Category> Categories
        {
            get => categories;
            set => SetProperty(ref categories, value);
        }

        public static List<string> NameCategories { get; set; } // Lista de nomes de categorias
        public static string nameCategory { get; set; } // Nome da categoria selecionada

        private bool refreshing; // Sinalizador para indicar se a lista está sendo atualizada
        public bool Refreshing
        {
            get => refreshing;
            set => SetProperty(ref refreshing, value);
        }

        public ICommand Refresh => new Command(refresh); // Comando para atualizar a lista

        public VMalterProducts()
        {
            DBcontext = new DBcontext(); // Inicializa o contexto do banco de dados

            Products = DBcontext.ListProducts(); // Obtém a lista de produtos do banco de dados
            Categories = DBcontext.ListCategory(); // Obtém a lista de categorias do banco de dados

            var category = Categories;
            NameCategories = new List<string>();
            if (category != null)
            {
                categories = category;
                foreach (var categorie in categories)
                {
                    NameCategories.Add(categorie.Name);
                }
            }
        }

        private void refresh()
        {
            Products = DBcontext.ListProducts(); // Atualiza a lista de produtos do banco de dados
            Categories = DBcontext.ListCategory(); // Atualiza a lista de categorias do banco de dados

            var category = Categories;
            NameCategories = new List<string>();
            if (category != null)
            {
                categories = category;
                foreach (var categorie in categories)
                {
                    NameCategories.Add(categorie.Name);
                }
            }
            Refreshing = false; // Define o sinalizador de atualização como falso após a atualização da lista
        }
    }
}
