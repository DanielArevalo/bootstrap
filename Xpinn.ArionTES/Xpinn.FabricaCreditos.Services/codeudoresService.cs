using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Business;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class codeudoresService
    {
        private codeudoresBusiness BOcodeudores;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para codeudores
        /// </summary>
        public codeudoresService()
        {
            BOcodeudores = new codeudoresBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "codeud01"; } }

        /// <summary>
        /// Servicio para crear codeudores
        /// </summary>
        /// <param name="pEntity">Entidad codeudores</param>
        /// <returns>Entidad codeudores creada</returns>
        public void Crearcodeudores(List<codeudores> lstCodeudores, Usuario pUsuario)
        {
            try
            {
                BOcodeudores.Crearcodeudores(lstCodeudores, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("codeudoresService", "Crearcodeudores", ex);
            }
        }

        public void CrearCodeudoresDesdeFuncionImportacion(codeudores Codeudores, Usuario pUsuario)
        {
            try
            {
                BOcodeudores.CrearCodeudoresDesdeFuncionImportacion(Codeudores, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("codeudoresService", "CrearCodeudoresDesdeFuncionImportacion", ex);
            }
        }

        /// <summary>
        /// Servicio para modificar codeudores
        /// </summary>
        /// <param name="pcodeudores">Entidad codeudores</param>
        /// <returns>Entidad codeudores modificada</returns>
        public codeudores Modificarcodeudores(codeudores pcodeudores, Usuario pUsuario)
        {
            try
            {
                return BOcodeudores.Modificarcodeudores(pcodeudores, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("codeudoresService", "Modificarcodeudores", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar codeudores
        /// </summary>
        /// <param name="pId">identificador de codeudores</param>
        public void EliminarcodeudoresCred(Int64 pId, Int64 radicado, Usuario pUsuario)
        {
            try
            {
                BOcodeudores.EliminarcodeudoresCred(pId, radicado, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarcodeudoresCred", ex);
            }
        }

        /// <summary>
        /// Servicio para Eliminar codeudores
        /// </summary>
        /// <param name="pId">identificador de codeudores</param>
        public void EliminarcodeudoresSol(Int64 pId, Int64 solicitud, Usuario pUsuario)
        {
            try
            {
                BOcodeudores.EliminarcodeudoresSol(pId, solicitud, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarcodeudoresSol", ex);
            }
        }





        /// <summary>
        /// Servicio para obtener codeudores
        /// </summary>
        /// <param name="pId">identificador de codeudores</param>
        /// <returns>Entidad codeudores</returns>
    
         public codeudores ConsultarDatosCodeudorRepo(string pId, Usuario pUsuario)
        {
            try
            {
                return BOcodeudores.ConsultarDatosCodeudorRepo(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("codeudoresService", "ConsultarDatosCodeudorRepo", ex);
                return null;
            }
        }

         public codeudores Consultarcodeudores(string pId, Usuario pUsuario)
         {
             try
             {
                 return BOcodeudores.ConsultarDatosCodeudorRepo(pId, pUsuario);
             }
             catch (Exception ex)
             {
                 BOExcepcion.Throw("codeudoresService", "Consultarcodeudores", ex);
                 return null;
             }
         }


        /// <summary>
        /// Servicio para obtener lista de codeudoress a partir de unos filtros
        /// </summary>
        /// <param name="pcodeudores">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de codeudores obtenidos</returns>
        public List<codeudores> Listarcodeudores(codeudores pcodeudores, Usuario pUsuario)
        {
            try
            {
                return BOcodeudores.Listarcodeudores(pcodeudores, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("codeudoresService", "Listarcodeudores", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de codeudoress a partir de unos filtros
        /// </summary>
        /// <param name="pcodeudores">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de codeudores obtenidos</returns>
        public List<codeudores> ListarcodeudoresCredito(String radicado, Usuario pUsuario)
        {
            try
            {
                return BOcodeudores.ListarcodeudoresCredito(radicado, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("codeudoresService", "ListarcodeudoresCredito", ex);
                return null;
            }
        }
        public codeudores ConsultarDatosCodeudor(string pId, Usuario pUsuario)
        {
            try
            {
                return BOcodeudores.ConsultarDatosCodeudor(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("codeudoresService", "ConsultarDatosCodeudor", ex);
                return null;
            }
        }


        public codeudores ValidarCodeudor(codeudores pCodeudor, Usuario pUsuario, ref string sError)
        {
            try
            {
                return BOcodeudores.ValidarCodeudor(pCodeudor, pUsuario, ref sError);
            }
            catch
            {
                return pCodeudor;
            }
        }

        public codeudores ConsultarCantidadCodeudores(string pId, Usuario pUsuario)
        {
            try
            {
                return BOcodeudores.ConsultarCantidadCodeudores(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("codeudoresService", "ConsultarCantidadCodeudores", ex);
                return null;
            }
        }


    }
}