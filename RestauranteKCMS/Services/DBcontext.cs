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
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DBKCMS.db");
            _database = new SQLiteConnection(dbPath);
            _database.CreateTable<Category>();
            _database.CreateTable<Product>();
        }

        //get all category
        public List<Category> ListCategory()
        {
            return _database.Table<Category>().ToList();
        }

        //get all products
        public List<Product> ListProducts()
        {
            return _database.Table<Product>().ToList();
        }

        //create new category

        public int CreateCategory(Category entity)
        {
            return _database.Insert(entity);
        }
        //create new product
        public int CreateProducts(Product entity)
        {
            return _database.Insert(entity);
        }
        //update category
        public int UpdateCategory(Category entity)
        {
            return _database.Update(entity);
        }
        //update product
        public int UpdateProducts(Product entity)
        {
            return _database.Update(entity);
        }

        //delete category
        public int DeleteCategory(Category entity)
        {
            return _database.Delete(entity);
        }
        //delete product
        public int DeleteProducts(Product entity)
        {
            return _database.Delete(entity);
        }
    }
}
