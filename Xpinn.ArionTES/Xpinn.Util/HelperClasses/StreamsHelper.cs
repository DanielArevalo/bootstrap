using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Xpinn.Util
{
    public class StreamsHelper
    {
        public byte[] LeerTodosLosBytesDeUnStream(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }


        /// <summary>
        /// Delimita el archivo por default con '|'
        /// </summary>
        public IEnumerable<string[]> LeerLineaArchivoDelimitadoYDevolverSinLeerTodo(Stream stream)
        {
            return LeerLineaArchivoDelimitadoYDevolverSinLeerTodo(stream, '|');
        }

        public IEnumerable<string[]> LeerLineaArchivoDelimitadoYDevolverSinLeerTodo(Stream stream, char separador)
        {
            string linea = string.Empty;

            using (StreamReader strReader = new StreamReader(stream))
            {
                while ((linea = strReader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(linea)) continue;

                    // Retorna el string[] por cada vuelta, no espera a que el while termine
                    // Después de retornar, vuelve al while y retorna el siguiente string[]
                    // Sale del while al no haber mas lineas que leer (trReader.ReadLine()) == null)
                    // Ese es el comportamiento del yield return

                    yield return linea.Split(separador);
                }
            }
        }

        public IEnumerable<string> LeerLineaArchivoYDevolverSinLeerTodo(Stream stream)
        {
            using (StreamReader strReader = new StreamReader(stream))
            {
                string linea = string.Empty;

                while ((linea = strReader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(linea)) continue;

                    // Retorna el string[] por cada vuelta, no espera a que el while termine
                    // Después de retornar, vuelve al while y retorna el siguiente string[]
                    // Sale del while al no haber mas lineas que leer (trReader.ReadLine()) == null)
                    // Ese es el comportamiento del yield return
                    yield return linea;
                }
            }
        }

        /// <summary>
        /// Delimita el archivo por default con '|'
        /// </summary>
        public IEnumerable<string[]> LeerLineasArchivoDelimitado(Stream stream)
        {
            return LeerLineasArchivoDelimitado(stream, '|');
        }

        public IEnumerable<string[]> LeerLineasArchivoDelimitado(Stream stream, char separador)
        {
            List<string[]> lstLineas = new List<string[]>();
            string linea = string.Empty;

            using (StreamReader strReader = new StreamReader(stream))
            {
                while ((linea = strReader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(linea)) continue;
                    lstLineas.Add(linea.Split(separador));
                }
            }

            return lstLineas;
        }


        public IEnumerable<string> LeerLineasArchivo(Stream stream)
        {
            List<string> lstLineas = new List<string>();

            using (StreamReader strReader = new StreamReader(stream))
            {
                string linea = string.Empty;
                while ((linea = strReader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(linea)) continue;
                    lstLineas.Add(strReader.ReadLine());
                }
            }

            return lstLineas;
        }
    }
}
