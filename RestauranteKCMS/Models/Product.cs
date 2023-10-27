using SQLite;
using System;
using System.IO;
using Xamarin.Forms;

namespace RestauranteKCMS.Models
{
    public class Category
    {
        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
    }
    public class Product
    {
        [AutoIncrement, PrimaryKey]
        public int Id {  get; set; }
        public string name {  get; set; }
        public string Idcategory {  get; set; }
        public byte[] image {  get; set; }
        public string description {  get; set; }
        public float price { get; set; }
    }
}