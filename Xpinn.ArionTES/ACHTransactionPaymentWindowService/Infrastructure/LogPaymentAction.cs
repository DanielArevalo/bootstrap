using System.IO;
using System.Reflection;

namespace ACHTransactionPaymentWindowService.Infrastructure
{
    public static class LogPaymentAction
    {
        public static void Grabar<T>(T obj, string archivo)
        {
            PropertyInfo[] propiedades = obj.GetType().GetProperties();
            using (StreamWriter sw = new StreamWriter(archivo, true))
            {
                object valor;
                foreach (PropertyInfo nPropiedad in propiedades)
                {
                    valor = nPropiedad.GetValue(obj, null);
                    sw.WriteLine("{0}={1}", nPropiedad.Name, valor != null ? valor : "");
                }
                sw.WriteLine(new string('_', 50));
            }
        }
    }
}
