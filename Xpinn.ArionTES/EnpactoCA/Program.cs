using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;
using System.IO;


namespace EnpactoCA
{
    static class Program
    {

        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            int _tipo_convenio = 1;

            if (args.Length == 0)
{
                System.Console.WriteLine("No ingreso ningùn paràmetro");
                return;
            }
            string sec = "jR0c0OCG/6YAIaHq/7+TDpPoM0XJbdOSg7X8dXA+og8=";    // Pruebas
            //string sec = "jR0c0OCG/6YAIaHq/7+TDg6T/V2ekF5mBjoSQVCPYhU=";  // Producción 
            if (args[0].ToUpper() == "SALDOS")
            {
                System.Console.WriteLine("Generando archivo de saldos");
                // Generar el archivo
                WSTarjeta.WSTarjetaSoapClient _wstarjeta = new WSTarjeta.WSTarjetaSoapClient();                
                //string datos = _wstarjeta.DatosClientes(sec, DateTime.Now.ToString("ddMMyyyyHHmmss"));
                string datos = _wstarjeta.DatosClientes(_tipo_convenio, sec, DateTime.Now.ToString("ddMMyyyyHHmmss"));
                // Descargar el archivo
                string archivo = DateTime.Now.ToString("ddMMyyyy") + ".CLS";
                using (StreamWriter outputFile = new StreamWriter(archivo))
                {
                    outputFile.WriteLine(datos);
                    outputFile.Close();
                }
                System.Console.WriteLine("Se genero el archivo: " + archivo);
                return;
            }
            if (args[0].ToUpper() == "VINCTAR")
            {
                return;
            }
            if (args[0].ToUpper().Length >= 8)
            {
                string[] data;
                int linea = 0;
                int _estadocarga = 0;
                string archivo = args[0].ToUpper() + ".SIC";
                Stream stream;
                try { stream = File.OpenRead(archivo); _estadocarga = 1;  } catch (Exception ex) { System.Console.WriteLine("No se pudo encontrar el archivo " + archivo + " " + ex.Message);  return; }
                using (StreamReader strReader = new StreamReader(stream))
                {
                    data = new string[stream.Length];
                    while (strReader.Peek() >= 0)
                    {
                        string readLine = strReader.ReadLine();
                        data[linea] = readLine;
                        System.Console.WriteLine(readLine);
                        _estadocarga = 2;                        
                    }
                }
                if (_estadocarga == 0)
                    System.Console.WriteLine("No se pudo encontrar el archivo " + archivo);
                if (_estadocarga == 1)
                    System.Console.WriteLine("No se pudo leer el archivo " + archivo);
                string _error = "";
                WSTarjeta.WSTarjetaSoapClient _wstarjeta = new WSTarjeta.WSTarjetaSoapClient();
                Int64 _cod_ope = _wstarjeta.CargarMovimientos(_tipo_convenio, sec, data, DateTime.Now, ref _error);
                if (_cod_ope != 0)
                    System.Console.WriteLine("Se cargaron los movimientos. Operacion No." + _cod_ope);
                else
                    System.Console.WriteLine("No se pudieron cargar los movimientos." + _error);
                return;
            }
        }
    }
}
