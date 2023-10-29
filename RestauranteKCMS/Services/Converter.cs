using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace RestauranteKCMS.Services
{
    public class ByteArrayToImageSourceConverter : IValueConverter
    {
        // O método Convert é usado para converter de byte[] para ImageSource.
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ImageSource retSource = null;

            // Verifica se o valor de entrada não é nulo.
            if (value != null)
            {
                // Converte o valor para um array de bytes.
                byte[] imageAsBytes = (byte[])value;

                // Cria um novo ImageSource a partir dos bytes usando ImageSource.FromStream.
                retSource = ImageSource.FromStream(() => new MemoryStream(imageAsBytes));
            }

            // Retorna a fonte de imagem resultante (pode ser nula se o valor de entrada for nulo).
            return retSource;
        }

        // O método ConvertBack não é implementado (lança uma exceção NotImplementedException) porque
        // a conversão de ImageSource para byte[] pode ser complexa e dependente de contexto.
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
