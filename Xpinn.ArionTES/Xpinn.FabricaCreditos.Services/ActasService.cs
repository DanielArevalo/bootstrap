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
    public class ActasService
    {
        private ActaBusiness BoReporte;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Credito
        /// </summary>
        public ActasService()
        {
            BoReporte = new ActaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        // Còdigo de programa para generaciòn de documentos
        public string CodigoPrograma { get { return "100153"; } }


        /// <summary>
        /// Servicio para obtener lista de Actas a partir de unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<Credito> ListarActas(Credito pCredito, Usuario pUsuario, String filtro)
        {
            try
            {
                return BoReporte.ListarActas(pCredito, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActasService", "ListarActas", ex);
                return null;
            }
        }
       
       
        /// <summary>
        /// Servicio para obtener lista de Creditos a partir de unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<Credito> ListarCreditosActas(Credito pCredito, Usuario pUsuario, String filtro)
        {
            try
            {
                return BoReporte.ListarCreditosActas(pCredito, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActasService", "ListarCreditosActas", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de Creditos a partir de unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<Credito> ListarCreditosReporte(Credito pCredito, Usuario pUsuario, string Acta)
        {
            try
            {
                return BoReporte.ListarCreditosReporte(pCredito, pUsuario, Acta);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActasService", "ListarCreditosReporte", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de Creditos a partir de unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<Credito> ListarCreditos(Credito pCredito, Usuario pUsuario, String filtro)
        {
            try
            {
                return BoReporte.ListarCreditos(pCredito, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActasService", "ListarCreditos", ex);
                return null;
            }
        }
        /// <summary>
        /// Servicio para obtener lista de Creditos a partir de unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<Credito> ListarCreditosUsuarios(Credito pCredito, Usuario pUsuario, String filtro, Int64 oficina)
        {
            try
            {
                return BoReporte.ListarCreditosUsuarios(pCredito, pUsuario, filtro,oficina);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActasService", "ListarCreditosUsuarios", ex);
                return null;
            }
        }
        /// <summary>
        /// Servicio para obtener lista de Creditos a partir de unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<Credito> ListarCreditosRestructurados(Credito pCredito, Usuario pUsuario, String filtro)
        {
            try
            {
                return BoReporte.ListarCreditosRestructurados(pCredito, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActasService", "ListarCreditosRestructurados", ex);
                return null;
            }
        }
        /// <summary>
        /// Crea una acta 
        /// </summary>
        /// <param name="pEntity">Entidad Acta</param>
        /// <returns>Entidad creada</returns>
        public Credito CrearRadicadosActas(Credito pActa, DateTime fechaaprobacion, Int64 codusuario, Usuario pUsuario)
        {
            try
            {
                return BoReporte.CrearRadicadosActas(pActa, fechaaprobacion, codusuario, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActasService", "CrearRadicadosActas", ex);
                return null;
            }
        }
        /// <summary>
        /// Servicio para obtener el parametro para  credito restructurado
        /// </summary>
        /// <param name="pId">identificador
        /// <returns>Entidad</returns>
        public Credito ConsultarParametroReestructurado(Usuario pUsuario)
        {
            try
            {
                return BoReporte.ConsultarParametroReestructurado(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActasService", "ConsultarParametroReestrurado", ex);
                return null;
            }
        }
        /// <summary>
        /// Servicio para obtener el parametro para Cargo Gerente
        /// </summary>
        /// <param name="pId">identificador
        /// <returns>Entidad</returns>
        public Credito ConsultarParametrocargoGerente(Usuario pUsuario)
        {
            try
            {
                return BoReporte.ConsultarParametrocargoGerente(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActasService", "ConsultarParametrocargoGerente", ex);
                return null;
            }
        }
        /// <summary>
        /// Servicio para obtener el parametro para Cargo Comite de Credito n1
        /// </summary>
        /// <param name="pId">identificador
        /// <returns>Entidad</returns>
        public Credito ConsultarParametrocargoComitedecreditoniv1(Usuario pUsuario)
        {
            try
            {
                return BoReporte.ConsultarParametrocargoComitedecreditoniv1(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActasService", "ConsultarParametrocargoComitedecreditoniv1", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener el parametro para Cargo Comite de Credito n1
        /// </summary>
        /// <param name="pId">identificador
        /// <returns>Entidad</returns>
        public Credito ConsultarParametrocargoComitedecreditoniv4(Usuario pUsuario)
        {
            try
            {
                return BoReporte.ConsultarParametrocargoComitedecreditoniv4(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActasService", "ConsultarParametrocargoComitedecreditoniv4", ex);
                return null;
            }
        }
        /// <summary>
        /// Obtiene una Acta
        /// </summary>
        /// <param name="pId">identificador de Acta</param>
        /// <returns>Acta consultada</returns>
        public Credito ConsultarActa(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BoReporte.ConsultarActa(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActasService", "ConsultarActa", ex);
                return null;
            }
        }
        /// <summary>
        /// Obtiene Datos de aprobador acta
        /// </summary>
        /// <param name="pId">identificador de Acta</param>
        /// <returns>Acta consultada</returns>
        public Credito ConsultarAprobadorActa(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BoReporte.ConsultarAprobadorActa(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActasService", "ConsultarAprobadorActa", ex);
                return null;
            }
        }

        public Int64? CrearActaNumero(string idacta, DateTime fechaaprobacion, Int64? codoficina, Int64 codusuario, Usuario pUsuario)
        {
            try
            {
                return BoReporte.CrearActaNumero(idacta, fechaaprobacion, codoficina, codusuario, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActasService", "CrearActaNumero", ex);
                return null;
            }
        }


    }
}