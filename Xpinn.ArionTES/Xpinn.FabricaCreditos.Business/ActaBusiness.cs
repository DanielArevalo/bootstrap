using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Data;

namespace Xpinn.FabricaCreditos.Business
{
    /// <summary>
    /// Objeto de negocio para Credito
    /// </summary>
    public class ActaBusiness : GlobalData
    {
        private ActaData DAReporte;

        /// <summary>
        /// Constructor del objeto de negocio para Credito
        /// </summary>
        public ActaBusiness()
        {
            DAReporte = new ActaData();
        }

        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<Credito> ListarActas(Credito pCredito, Usuario pUsuario, String filtro)
        {
            try
            {
                return DAReporte.ListarActas(pCredito, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActaBusiness", "ListarActas", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<Credito> ListarCreditosActas(Credito pCredito, Usuario pUsuario, String filtro)
        {
            try
            {
                return DAReporte.ListarCreditosActas(pCredito, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActaBusiness", "ListarCreditosActas", ex);
                return null;
            }
        }

         /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<Credito> ListarCreditosReporte(Credito pCredito, Usuario pUsuario, string Acta)
        {
            try
            {
                return DAReporte.ListarCreditosReporte(pCredito, pUsuario, Acta);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActaBusiness", "ListarCreditosReporte", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<Credito> ListarCreditos(Credito pCredito, Usuario pUsuario, String filtro)
        {
            try
            {
                return DAReporte.ListarCreditos(pCredito, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActaBusiness", "ListarCreditos", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<Credito> ListarCreditosUsuarios(Credito pCredito, Usuario pUsuario, String filtro,Int64 oficina)
        {
            try
            {
                return DAReporte.ListarCreditosUsuarios(pCredito, pUsuario, filtro, oficina);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActaBusiness", "ListarCreditosUsuarios", ex);
                return null;
            }
        }
        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<Credito> ListarCreditosRestructurados(Credito pCredito, Usuario pUsuario, String filtro)
        {
            try
            {
                return DAReporte.ListarCreditosRestructurados(pCredito, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActaBusiness", "ListarCreditosRestructurados", ex);
                return null;
            }
        }

        /// <summary>
        /// Crea los radicados del acta 
        /// </summary>
        /// <param name="pEntity">Entidad Beneficiarios</param>
        /// <returns>Entidad creada</returns>
        public Credito CrearRadicadosActas(Credito pActa, DateTime fechaaprobacion, Int64 codusuario, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pActa = DAReporte.CrearActa(pActa, fechaaprobacion, codusuario, pUsuario);

                    ts.Complete();
                }

                return pActa;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActaBusiness", "CrearRadicadosActas", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene el parametro para cargo gerente 
        /// </summary>
        /// <param name="pId">identificador 
        /// <returns>Entidad</returns>
        public Credito ConsultarParametrocargoGerente(Usuario pUsuario)
        {
            try
            {
                return DAReporte.ConsultarParametrocargoGerente(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActaBusiness", "ConsultarParametrocargoGerente", ex);
                return null;
            }
        }
        /// <summary>
        /// Obtiene el parametro para credito restructurado
        /// </summary>
        /// <param name="pId">identificador 
        /// <returns>Entidad</returns>
        public Credito ConsultarParametroReestructurado(Usuario pUsuario)
        {
            try
            {
                return DAReporte.ConsultarParametroReestructurado(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActaBusiness", "ConsultarParametroReestructurado", ex);
                return null;
            }
        }
        /// <summary>
        /// Obtiene el parametropara Cargo Comite de Credito n1
      
        /// </summary>
        /// <param name="pId">identificador 
        /// <returns>Entidad</returns>
        public Credito ConsultarParametrocargoComitedecreditoniv1(Usuario pUsuario)
        {
            try
            {
                return DAReporte.ConsultarParametrocargoComitedecreditoniv1(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActaBusiness", "ConsultarParametrocargoComitedecreditoniv1", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene el parametropara Cargo Comite de Credito n1

        /// </summary>
        /// <param name="pId">identificador 
        /// <returns>Entidad</returns>
        public Credito ConsultarParametrocargoComitedecreditoniv4(Usuario pUsuario)
        {
            try
            {
                return DAReporte.ConsultarParametrocargoComitedecreditoniv4(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActaBusiness", "ConsultarParametrocargoComitedecreditoniv4", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene una Acta
        /// </summary>
        /// <param name="pId">identificador del Acta</param>
        /// <returns>Acta consultada</returns>
        public Credito ConsultarActa(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAReporte.ConsultarActa(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActaBusiness", "ConsultarActa", ex);
                return null;
            }
        }
        /// <summary>
        /// Obtiene Datos de aprobador acta
        /// </summary>
        /// <param name="pId">identificador del Acta</param>
        /// <returns>Acta consultada</returns>
        public Credito ConsultarAprobadorActa(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAReporte.ConsultarAprobadorActa(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActaBusiness", "ConsultarAprobadorActa", ex);
                return null;
            }
        }

        public Int64? CrearActaNumero(string idacta, DateTime fechaaprobacion, Int64? codoficina, Int64 codusuario, Usuario pUsuario)
        {
            Int64? pcod_acta = null;
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pcod_acta = DAReporte.CrearActaNumero(idacta, fechaaprobacion, codoficina, codusuario, pUsuario);

                    ts.Complete();
                }

                return pcod_acta;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActaBusiness", "CrearRadicadosActas", ex);
                return null;
            }
        }


    }
}