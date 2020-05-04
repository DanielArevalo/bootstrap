using System;
using System.Collections.Generic;
using System.Linq;
//using System.Net.Http;
//using System.Net.Http.Headers;
using System.Text;

namespace Xpinn.Util
{
    /// <summary>
    /// Objeto para definicion de caracteristicas globales para la capa de servicios
    /// </summary>
    public abstract class GlobalService
    {
        protected ExcepcionBusiness BOExcepcion;
       // protected HttpClient _client;
        string _urlWebAPI;

        /// <summary>
        /// Constructor del objeto global de capa de servicios
        /// </summary>
        public GlobalService()
        {
            BOExcepcion = new ExcepcionBusiness();

            // Ip publica DataCenter
            //_urlWebAPI = @"http://192.175.101.116/ServiceWebApi/api/";

            // Ip privada DataCenter
            _urlWebAPI = @"http://172.18.40.6/ServiceWebApi/api/"; 

            //Ip de pruebas en localhost
            //_urlWebAPI = @"http://localhost:18147/api/";

           // _client = new HttpClient();

            //_client.BaseAddress = new Uri(_urlWebAPI);
            //_client.DefaultRequestHeaders.Accept.Clear();
            //_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
