using System;

namespace Xpinn.Util
{
    public class ByteHelper
    {
        public string ConvertByteArrToBase64String(byte[] byteArr)
        {
            string base64Image = string.Empty;

            if (byteArr != null)
            {
                base64Image = @"data:image/jpeg;base64," + Convert.ToBase64String(byteArr);
            }

            return base64Image;
        }

        // Sirve para mostrar el pdf en un iframe, el iframe no carga controles para imprimir, etc.
        public string ConvertByteArrToBase64StringForPDFView(byte[] byteArr)
        {
            string base64Image = string.Empty;

            if (byteArr != null)
            {
                base64Image = @"data:application/pdf;base64," + Convert.ToBase64String(byteArr);
            }

            return base64Image;
        }
        
    }
}
