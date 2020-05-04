using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.ServiceModel;
using Xpinn.Interfaces.Entities;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
using System.Collections.Generic;

namespace Xpinn.Interfaces.Services
{
    /// <summary>
    /// Interfaz para efectuar consultas a las personas buscadas en US http://developer.trade.gov/
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class TradeUSServices
    {
        /// <summary>
        /// Consultar persona prohibidas en US por nombre e identificacion
        /// </summary>
        /// <param name="nombre">Pasar primer nombre nada mas o razon social, evitar nombre completos</param>
        /// <param name="identificacion"></param>
        /// <returns></returns>
        public TradeUSSearchResults ConsultarPersonaBuscadaPorIdentificacion(string nombre, string identificacion)
        {
            // Validar los argumentos que sean validos
            if (string.IsNullOrWhiteSpace(nombre)) throw new ArgumentException("Nombre a buscar invalido!.");
            if (string.IsNullOrWhiteSpace(identificacion)) throw new ArgumentException("Nombre a buscar invalido!.");

            using (HttpClient client = new HttpClient())
            {
                string urlToQuery = string.Format(@"https://api.trade.gov/consolidated_screening_list/search?api_key=2cHNuvHZNGFujMeUKaGeCzX9&name={0}", nombre.Trim());
                HttpResponseMessage response = client.GetAsync(urlToQuery).Result;

                string responseString = response.Content.ReadAsStringAsync().Result;
                TradeUSSearchResults tradeEntity = JsonConvert.DeserializeObject<TradeUSSearchResults>(responseString);

                if (tradeEntity != null && tradeEntity.results.Count > 0)
                {
                    // Descartamos las personas que no tengan una entidad valida para la identificacion
                    tradeEntity.results = tradeEntity.results.Where(x => x.ids != null && x.ids.Count > 0).ToList();

                    // Descartamos las personas que no tengan una identificacion valida o que no sean iguales
                    tradeEntity.results = tradeEntity.results.Where(x => x.ids.Where(z =>
                    {
                        if (!string.IsNullOrWhiteSpace(z.number))
                        {
                            return z.number.Replace(",", "").Replace(".", "").Replace("-", "").Trim() == identificacion.Replace(",", "").Replace(".", "").Replace("-", "").Trim();
                        }
                        else
                        {
                            return false;
                        }
                    }).Any()).ToList();
                }

                return tradeEntity;
            }
        }

        /// <summary>
        /// Consultar persona prohibidas en US por nombre
        /// </summary>
        /// <param name="nombre">Pasar primer nombre nada mas o razon social, evitar nombre completos</param>
        /// <returns></returns>
        public TradeUSSearchResults ConsultarPersonaBuscadaPorNombre(string nombre)
        {
            // Validar los argumentos que sean validos
            if (string.IsNullOrWhiteSpace(nombre)) throw new ArgumentException("Nombre a buscar invalido!.");

            using (HttpClient client = new HttpClient())
            {
                string urlToQuery = string.Format(@"https://api.trade.gov/consolidated_screening_list/search?api_key=2cHNuvHZNGFujMeUKaGeCzX9&q={0}", nombre.Trim());
                HttpResponseMessage response = client.GetAsync(urlToQuery).Result;

                string responseString = response.Content.ReadAsStringAsync().Result;
                TradeUSSearchResults tradeEntity = JsonConvert.DeserializeObject<TradeUSSearchResults>(responseString);

                return tradeEntity;
            }
        }

        /// <summary>
        /// Método para consultar personas o entidades registradas en lista CS/ONU
        /// </summary>
        /// <param name="pIdentificacion">No Identificacion</param>
        /// <param name="pNombre">Nombre completo persona</param>
        /// <param name="tipo_persona">Persona Natural o Juridica</param>
        /// <param name="PersonaIndividual">Entidad con datos de la persona natural registrada en la lista</param>
        /// <param name="PersonaEntidad">Entidad con datos de la persona juridica registrada en la lista</param>
        public void ListaConsolidadaCSONU(string nombre, string identificacion, string tipo_persona, ref Individual PersonaIndividual, ref Entity PersonaEntidad)
        {
            using (HttpClient client = new HttpClient())
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Individual));
                Individual personaConsulta = new Individual();

                //Obtener XML con las lista consolidada
                string urlTOQuery = string.Format(@"https://scsanctions.un.org/resources/xml/en/consolidated.xml");
                HttpResponseMessage response = client.GetAsync(urlTOQuery).Result;
                string responseString = response.Content.ReadAsStringAsync().Result;

                //Convertir string XML
                XmlDocument xm = new XmlDocument();
                xm.LoadXml(responseString);

                //Deserializar XML para obtener lista de personas y entidades
                Individuals result = (Individuals)DeserializarXml(xm, typeof(Individuals));

                if (result != null)
                {
                    if (tipo_persona == "N")
                    {
                        personaConsulta = result.individual.FirstOrDefault();
                    }

                    PersonaIndividual = (from item in result.individual
                                    let nombres = item.first_name.Trim() + " " + item.second_name.Trim() + " " + item.third_name.Trim()
                                    where (nombres.Trim() == nombre.Trim()) && item != null
                                    select item).FirstOrDefault();
                }
                else if (tipo_persona == "J")
                {
                    PersonaEntidad = result.entity.Where(x => (x.first_name.Contains(nombre))).FirstOrDefault();
                }
                else
                {
                    PersonaIndividual = null;
                    PersonaEntidad = null;
                }
            }
        }

        /// <summary>
        /// Método para deserializar un documento XML
        /// </summary>
        /// <param name="xml">Documento XML</param>x
        /// <param name="type">Tipo de dato en el cual se va a deserializar el documento XML</param>
        /// <returns>Objeto deserializado</returns>
        public static object DeserializarXml(XmlDocument xml, Type type)
        {
            XmlSerializer s = new XmlSerializer(type);
            string xmlString = xml.OuterXml.ToString();
            byte[] buffer = ASCIIEncoding.UTF8.GetBytes(xmlString);
            MemoryStream ms = new MemoryStream(buffer);
            XmlReader reader = new XmlTextReader(ms);
            object o = null;
            try
            {
                o = s.Deserialize(reader);
            }
            finally
            {
                reader.Close();
            }
            return o;
        }


        public List<TradeUSSearchEntity> ListaConsolidadOFAQ()
        {
            List<TradeUSSearchEntity> lista = new List<TradeUSSearchEntity>();
            using (HttpClient client = new HttpClient())
            {
                //Obtener XML con las lista consolidada
                string urlTOQuery = string.Format(@"https://www.treasury.gov/ofac/downloads/sdn.csv");
                HttpResponseMessage response = client.GetAsync(urlTOQuery).Result;
                Stream respuesta = response.Content.ReadAsStreamAsync().Result;
                using (StreamReader strReader = new StreamReader(respuesta))
                {
                    string readLine;
                    while (strReader.Peek() >= 0)
                    {
                        string[] arrayline;
                        readLine = strReader.ReadLine();
                        arrayline = readLine.Split(Convert.ToChar(","));
                        if (arrayline.Count() > 0)
                        {
                            try
                            { 
                                TradeUSSearchEntity entidad = new TradeUSSearchEntity();
                                entidad.id = arrayline[0];
                                entidad.name = arrayline[1];
                                lista.Add(entidad);
                            }
                            catch { }
                        }
                    }
                }
            }
            return lista;
        }

        public Individuals ListaConsolidadaCSONU()
        {
            using (HttpClient client = new HttpClient())
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Individual));
                Individual personaConsulta = new Individual();

                //Obtener XML con las lista consolidada
                string urlTOQuery = string.Format(@"https://scsanctions.un.org/resources/xml/en/consolidated.xml");
                HttpResponseMessage response = client.GetAsync(urlTOQuery).Result;
                string responseString = response.Content.ReadAsStringAsync().Result;

                //Convertir string XML
                XmlDocument xm = new XmlDocument();
                xm.LoadXml(responseString);

                //Deserializar XML para obtener lista de personas y entidades
                Individuals result = (Individuals)DeserializarXml(xm, typeof(Individuals));
                return result;
            }
        }



    }
}
