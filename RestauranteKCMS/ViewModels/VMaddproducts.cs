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
    internal class VMaddproducts : BaseViewModel
    {
        private DBcontext dbcontext; // Acesso ao banco de dados

        public List<Category> categories { get; set; } // Lista de categorias

        public static string nameCategory { get; set; } // Nome da categoria selecionada

        private List<string> namecategories; // Lista de nomes de categorias
        public List<string> NameCategories
        {
            get => namecategories;
            set => SetProperty(ref namecategories, value);
        }

        private string _nameProduct; // Nome do produto
        public string NameProduct
        {
            get { return _nameProduct; }
            set
            {
                _nameProduct = value;
            }
        }

        private string _descriptionproduct; // Descrição do produto
        public string DescriptionProduct
        {
            get { return _descriptionproduct; }
            set
            {
                _descriptionproduct = value;
            }
        }

        private float _priceproduct; // Preço do produto
        public float PriceProduct
        {
            get { return _priceproduct; }
            set
            {
                _priceproduct = value;
            }
        }

        private byte[] imageByte { get; set; } // Array de bytes da imagem do produto

        private ImageSource imagesource; // Fonte da imagem do produto
        public ImageSource ImageSource
        {
            get => imagesource;
            set => SetProperty(ref imagesource, value);
        }

        public ICommand RegisterCommand => new Command(Register); // Comando para registrar uma nova categoria
        public ICommand PickImage => new Command(SelectImage); // Comando para escolher uma imagem
        public ICommand SaveProduct => new Command(saveproduct); // Comando para salvar o produto

        public VMaddproducts()
        {
            dbcontext = new DBcontext(); // Inicializa o contexto do banco de dados

            var category = dbcontext.ListCategory(); // Obtém a lista de categorias do banco de dados
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

        public void Register()
        {
            if (!String.IsNullOrEmpty(NameProduct))
            {
                // Cria uma nova categoria
                Category newCategory = new Category()
                {
                    Name = NameProduct
                };
                dbcontext.CreateCategory(newCategory);
            }
        }

        public async void SelectImage()
        {
            await PickAndShow(); // Abre a galeria para escolher uma imagem
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
            // Verifica e solicita permissão para acessar o armazenamento
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
            if (string.IsNullOrEmpty(NameProduct) || string.IsNullOrEmpty(DescriptionProduct) || PriceProduct == 0) { return; }

            // Cria um novo produto com os detalhes fornecidos
            Product product = new Product()
            {
                name = NameProduct,
                description = DescriptionProduct,
                price = PriceProduct,
                image = imageByte,
                Idcategory = nameCategory
            };

            dbcontext.CreateProducts(product); // Salva o produto no banco de dados

            // Exibe uma mensagem de "toast" informando que o novo produto foi adicionado
            Context context = Android.App.Application.Context;
            string text = "Novo produto adicionado";
            ToastLength duration = ToastLength.Short;
            var toast = Toast.MakeText(context, text, duration);
            toast.Show();
        }
    }

}
