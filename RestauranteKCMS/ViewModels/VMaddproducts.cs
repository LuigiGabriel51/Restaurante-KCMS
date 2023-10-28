using Android.Content;
using Android.Webkit;
using Android.Widget;
using MvvmHelpers;
using RestauranteKCMS.Models;
using RestauranteKCMS.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using static System.Net.Mime.MediaTypeNames;
using static Xamarin.Essentials.AppleSignInAuthenticator;
using Image = Xamarin.Forms.Image;

namespace RestauranteKCMS.ViewModels
{
    internal class VMaddproducts: BaseViewModel
    {
        private DBcontext dbcontext;
        public List<Category> categories {  get; set; }
        public static string nameCategory { get; set; }

        private List<string> namecategories;
        public List<string> NameCategories 
        {
            get => namecategories; 
            set => SetProperty(ref namecategories, value); 
        }

        private string _nameProduct;
        public string NameProduct
        {
            get { return _nameProduct; }
            set
            {
                _nameProduct = value;
            }
        }

        private string _descriptionproduct;
        public string DescriptionProduct
        {
            get { return _descriptionproduct; }
            set
            {
                _descriptionproduct = value;
            }
        }

        private float _priceproduct;
        public float PriceProduct
        {
            get { return _priceproduct; }
            set
            {
                _priceproduct = value;
            }
        }
        private byte[] imageByte { get; set; }

        private ImageSource imagesource;
        public ImageSource ImageSource
        {
            get => imagesource;
            set => SetProperty(ref imagesource, value);

        }

        public ICommand RegisterCommand => new Command(Register);
        public ICommand PickImage => new Command(SelectImage);
        public ICommand SaveProduct => new Command(saveproduct);

        public VMaddproducts()
        {
            dbcontext = new DBcontext();
            var category = dbcontext.ListCategory();
            NameCategories = new List<string>();
            if (category != null)
            {
                categories = category;
                foreach(var categorie in categories)
                {
                    NameCategories.Add(categorie.Name);
                }
            }
        }

        public void Register()
        {
            if (!String.IsNullOrEmpty(NameProduct))
            {
                Category newCategory = new Category()
                {
                    Name = NameProduct
                };
                dbcontext.CreateCategory(newCategory);
            }
        }
        public async void SelectImage() 
        {   
            await PickAndShow();
        }
        async Task PickAndShow()
        {
            try
            {
                var photo = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Escolha uma foto"
                });
                if (photo == null) { return; }
                Stream sourceStream = await photo.OpenReadAsync();
                MemoryStream memoryStream = new MemoryStream();
                await sourceStream.CopyToAsync(memoryStream);
                byte[] byteArray = memoryStream.ToArray();
                imageByte = byteArray;
                if (imageByte != null && imageByte.Length > 0)
                {
                    ImageSource = ImageSource.FromStream(() => new MemoryStream(imageByte));
                }
            }
            catch (PermissionException)
            {
                var status = CheckAndRequestCameraPermission();
            }
        }

        public async Task<PermissionStatus> CheckAndRequestCameraPermission()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();

            if (status == PermissionStatus.Granted)
                return status;

            if (status == PermissionStatus.Denied && DeviceInfo.Platform == DevicePlatform.iOS)
            {
                return status;
            }

            status = await Permissions.RequestAsync<Permissions.StorageRead>();

            return status;
        }

        private void saveproduct()
        {
            if(string.IsNullOrEmpty(NameProduct) || string.IsNullOrEmpty(DescriptionProduct) || PriceProduct == 0) { return; }
            Product product = new Product()
            {
                name = NameProduct,
                description = DescriptionProduct,
                price = PriceProduct,
                image = imageByte,
                Idcategory = nameCategory 
            };
            dbcontext.CreateProducts(product);

            Context context = Android.App.Application.Context;
            string text = "Novo produto adicionado";
            ToastLength duration = ToastLength.Short;

            var toast = Toast.MakeText(context, text, duration);
            toast.Show();
        }
    }
}
