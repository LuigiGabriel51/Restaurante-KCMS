using RestauranteKCMS.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace RestauranteKCMS.Services
{
    public class DBcontext
    {
        private readonly SQLiteConnection _database;

        public DBcontext()
        {
            // O construtor cria a conexão com o banco de dados SQLite local do aplicativo.
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DBKCMS.db");
            _database = new SQLiteConnection(dbPath);

            // Também cria as tabelas "Category" e "Product" se elas ainda não existirem.
            _database.CreateTable<Category>();
            _database.CreateTable<Product>();
        }

        // Método para obter todas as categorias.
        public List<Category> ListCategory()
        {
            return _database.Table<Category>().ToList();
        }

        // Método para obter todos os produtos.
        public List<Product> ListProducts()
        {
            return _database.Table<Product>().ToList();
        }

        // Método para criar uma nova categoria.
        public int CreateCategory(Category entity)
        {
            return _database.Insert(entity);
        }

        // Método para criar um novo produto.
        public int CreateProducts(Product entity)
        {
            return _database.Insert(entity);
        }

        // Método para atualizar uma categoria existente.
        public int UpdateCategory(Category entity)
        {
            return _database.Update(entity);
        }

        // Método para atualizar um produto existente.
        public int UpdateProducts(Product entity)
        {
            return _database.Update(entity);
        }

        // Método para excluir uma categoria.
        public int DeleteCategory(Category entity)
        {
            return _database.Delete(entity);
        }

        // Método para excluir um produto.
        public int DeleteProducts(Product entity)
        {
            return _database.Delete(entity);
        }
    }
}
