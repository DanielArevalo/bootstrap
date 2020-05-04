using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Integracion.Entities;
using Xpinn.Integracion.Data;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace Xpinn.Integracion.Business
{
    public class ImoneyBusiness : GlobalData
    {
        private ImoneyData BOImoneyData;
        private IntegracionData BOIntegracionData;
        private Xpinn.Integracion.Entities.Auth token;

        public ImoneyBusiness()
        {
            BOImoneyData = new ImoneyData();
            BOIntegracionData = new IntegracionData();
        }

        /// <summary>
        /// Obtiene token de seguridad
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Auth getAuthorization(Usuario pUsuario)
        {
            Auth token = new Auth();
            try
            {
                Entities.Integracion entidad = BOIntegracionData.ObtenerIntegracion(1,pUsuario);

                if(entidad != null && !string.IsNullOrEmpty(entidad.password))
                {
                    Util.CifradoBusiness cb = new CifradoBusiness();
                    string pass = cb.Desencriptar(entidad.password); 
                    string href = "http://test.pagos.iimoney.co:4000/api/auth";

                    HttpWebRequest httpWebRequest = WebRequest.Create(href) as HttpWebRequest;
                    httpWebRequest.Method = "POST";
                    httpWebRequest.ContentType = "application/json";
                    httpWebRequest.Accept = "application/json";

                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        string json = "{\"username\":\""+entidad.usuario+"\"," +
                                      "\"password\":\""+pass+"\"," +
                                      "\"grant_type\":\"password\"}";
                        streamWriter.Write(json);
                    }

                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();                        
                        token = JsonConvert.DeserializeObject<Auth>(result);
                    }
                    return token;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AuthBusiness", "getAuthorization", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene lista operadores disponibles de fullmovil : {jwt:token};
        /// </summary>
        /// <param name="jwt"></param>
        /// <param name="pUsuario"></param>
        public List<Operators> getOperators(Usuario pUsuario)
        {
            List<Operators> lstOperatorss = new List<Operators>();
            try
            {
                token = getAuthorization(pUsuario);
                if (token != null)
                {
                    string href = "http://test.pagos.iimoney.co:4000/admin/fullmovil/operators";

                    HttpWebRequest httpWebRequest = WebRequest.Create(href) as HttpWebRequest;
                    httpWebRequest.Method = "GET";
                    httpWebRequest.ContentType = "application/json";
                    httpWebRequest.Headers.Add("Authorization", "Bearer " + token.jwt);
                    httpWebRequest.Accept = "application/json";

                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        lstOperatorss = JsonConvert.DeserializeObject<List<Operators>>(result);
                    }
                    return lstOperatorss;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AuthBusiness", "getOperators", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene paquetes filtrados por el operador dado
        /// </summary>
        /// <param name="jwt"></param>
        /// <param name="ope"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public List<Package> getPackages(string ope, Usuario pUsuario)
        {
            List<Package> lstPackages = new List<Package>();
            try
            {
                token = getAuthorization(pUsuario);
                if (token != null)
                {
                    string href = "http://test.pagos.iimoney.co:4000/admin/fullmovil/packages";

                    HttpWebRequest httpWebRequest = WebRequest.Create(href) as HttpWebRequest;
                    httpWebRequest.Method = "GET";
                    httpWebRequest.ContentType = "application/json";
                    httpWebRequest.Headers.Add("Authorization", "Bearer " + token.jwt);
                    httpWebRequest.Headers.Add("operator", ope);
                    httpWebRequest.Accept = "application/json";

                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        lstPackages = JsonConvert.DeserializeObject<List<Package>>(result);
                    }
                    return lstPackages;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AuthBusiness", "getOperators", ex);
                return null;
            }
        }

        /// <summary>
        /// Crea una nueva recarga y carga por fullmovil con la información dada
        /// </summary>
        /// <param name="transact"></param>
        /// <param name="jwt"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public Fullmovil createFullMovilTransaction(Fullmovil transact, Usuario pUsuario)
        {
            Fullmovil fullmovil = new Fullmovil();
            try
            {
                token = getAuthorization(pUsuario);
                if (token != null)
                {
                    transact.id = 0;
                    transact.subtotal = 0;
                    transact.status = "TRY";
                    transact = BOImoneyData.guardarTransaccion(transact, pUsuario);

                    if(transact.id_transaccion > 0)
                    {
                        string href = "http://test.pagos.iimoney.co:4000/api/auth";

                        HttpWebRequest httpWebRequest = WebRequest.Create(href) as HttpWebRequest;
                        httpWebRequest.Method = "POST";
                        httpWebRequest.ContentType = "application/json";
                        httpWebRequest.Headers.Add("Authorization", "Bearer " + token.jwt);
                        httpWebRequest.Accept = "application/json";

                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            string json = "{\"phone\":\"" + transact.phone + "\"," +
                                          "\"operator\":\"" + transact.operador + "\"," +
                                          "\"total\":\"" + transact.total + "\"," +
                                          "\"description\":\"" + transact.description + "\"," +
                                          "\"package_id\":\"" + transact.package_id + "\"}";

                            streamWriter.Write(json);
                        }

                        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                        {
                            var result = streamReader.ReadToEnd();
                            fullmovil = JsonConvert.DeserializeObject<Fullmovil>(result);
                        }
                        if (fullmovil != null)
                        {
                            fullmovil.id_transaccion = transact.id_transaccion;
                            fullmovil.cod_persona = transact.cod_persona;
                            fullmovil.descripcion_plan = !string.IsNullOrEmpty(transact.descripcion_plan) ? transact.descripcion_plan : "";
                            BOImoneyData.actualizarTransaccion(fullmovil, pUsuario);
                            return fullmovil;
                        }
                    }                   
                }
                return null;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AuthBusiness", "getAuthorization", ex);
                return null;
            }
        }

        /// <summary>
        /// Lista las transacciones de la persona actualizando estados
        /// </summary>
        /// <param name="cod_persona"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public List<Fullmovil> getFulltransactionList(string cod_persona, Usuario pUsuario)
        {
            List<Fullmovil> lstTransacciones = new List<Fullmovil>();
            try
            {
                List<Fullmovil> lista = BOImoneyData.listarTransaccionesPersona(cod_persona, pUsuario);
                if(lista != null)
                {
                    //Si hay transacciones pendientes las consulta
                    if (lista.Where(x => x.status == "PENDING").Count() > 0)
                    {
                        //obtiene token
                        token = getAuthorization(pUsuario);
                        if(token != null)
                        {
                            foreach (Fullmovil item in lista)
                            {
                                //verifica si no está en un estado final
                                if(item.status.ToUpper() == "PENDING")
                                {
                                    //Obtiene datos de la transacción pendiente
                                    Fullmovil full = getFullMovilTransaction(item.ref_id.ToString(), pUsuario, token.jwt);
                                    //evalua si el estado ha cambiado
                                    if(full != null && item.status.ToUpper() != full.status.ToUpper())
                                    {
                                        //actualiza el estado
                                        item.status = full.status;
                                        BOImoneyData.actualizarEstadoTransaccion(item, pUsuario);
                                    }
                                }
                                lstTransacciones.Add(item);
                            }
                            return lstTransacciones;
                        }
                    }
                    return lista;
                }
                return null;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AuthBusiness", "getAuthorization", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene el estado de una transacción por id
        /// </summary>
        /// <param name="jwt"></param>
        /// <param name="top_up_id"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public Fullmovil getFullMovilTransaction(string top_up_id, Usuario pUsuario, string jwt = "")
        {
            Fullmovil top_up = new Fullmovil();
            try
            {
                if (string.IsNullOrEmpty(jwt))
                {
                    token = getAuthorization(pUsuario);
                    if (token != null)
                        jwt = token.jwt;
                }

                if (!string.IsNullOrEmpty(jwt))
                {
                    string href = "http://test.pagos.iimoney.co:4000/admin/fullmovil/top_ups";

                    HttpWebRequest httpWebRequest = WebRequest.Create(href) as HttpWebRequest;
                    httpWebRequest.Method = "GET";
                    httpWebRequest.ContentType = "application/json";
                    httpWebRequest.Headers.Add("Authorization", "Bearer " + jwt);
                    httpWebRequest.Headers.Add("top_up_id", top_up_id);
                    httpWebRequest.Accept = "application/json";

                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        top_up = JsonConvert.DeserializeObject<Fullmovil>(result);
                    }
                    return top_up;
                }
                return null;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AuthBusiness", "getOperators", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene datos de Tenant : {jwt:token, tenantID= "admin_expinn"};
        /// </summary>
        /// <param name="jwt"></param>
        /// <param name="tenantID"></param>
        /// <param name="pUsuario"></param>
        public TenantResult getTenant(string jwt, string tenantID, Usuario pUsuario)
        {
            
            TenantResult lstTenant = new TenantResult();
            try
            {
                string href = "http://test.pagos.iimoney.co:4000/admin/tenants";

                HttpWebRequest httpWebRequest = WebRequest.Create(href) as HttpWebRequest;
                httpWebRequest.Method = "GET";
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Headers.Add("Authorization", "Bearer " + jwt);
                httpWebRequest.Headers.Add("id", tenantID);
                httpWebRequest.Accept = "application/json";

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    lstTenant = JsonConvert.DeserializeObject<TenantResult>(result);
                }
                return lstTenant;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AuthBusiness", "getTenant", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene lista de Agreements o comercio : {jwt:token, AgreementsID= "admin_expinn"};
        /// </summary>
        /// <param name="jwt"></param>
        /// <param name="AgreementsID"></param>
        /// <param name="pUsuario"></param>
        public AgreementsResult getAgreements(string jwt, string AgreementsID, Usuario pUsuario)
        {

            AgreementsResult lstAgreements = new AgreementsResult();
            try
            {
                string href = "http://test.pagos.iimoney.co:4000/admin/agreements";

                HttpWebRequest httpWebRequest = WebRequest.Create(href) as HttpWebRequest;
                httpWebRequest.Method = "GET";
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Headers.Add("Authorization", "Bearer " + jwt);
                httpWebRequest.Headers.Add("id", AgreementsID);
                httpWebRequest.Accept = "application/json";

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    lstAgreements = JsonConvert.DeserializeObject<AgreementsResult>(result);
                }
                return lstAgreements;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AuthBusiness", "getTenant", ex);
                return null;
            }
        }        
                
    }
}


