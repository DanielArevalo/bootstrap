using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Asesores.Entities;
using Xpinn.Asesores.Data;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.Asesores.Business
{
    /// <summary>
    /// Objeto de negocio para Credito
    /// </summary>
    public class CreditosBusiness : GlobalData
    {
        private CreditosData DACredito;

        /// <summary>
        /// Constructor del objeto de negocio para Credito
        /// </summary>
        public CreditosBusiness()
        {
            DACredito = new CreditosData();
        }

       

        /// <summary>
        /// Obtiene un Credito
        /// </summary>
        /// <param name="pId">Identificador de Credito</param>
        /// <returns>Entidad Credito</returns>
        public Creditos ConsultarCreditoAsesor(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DACredito.ConsultarCreditoAsesor(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "ConsultarCreditoAsesor", ex);
                return null;
            }
        }

        public List<Garantia> ListarCreditosGVPorFiltro(string filtroDefinido, string filtroGrilla, Usuario pUsuario)
        {
            try
            {
                return DACredito.ListarCreditosGVPorFiltro(filtroDefinido, filtroGrilla, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "ListarCreditosGVPorFiltro", ex);
                return null;
            }
        }


        public List<Cliente> ListarCodeudores(Int64 pnumeroradicacion, Usuario pUsuario)
        {
            try
            {
                return DACredito.ListarCodeudores(pnumeroradicacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "ListarCodeudores", ex);
                return null;
            }

        }

        public Cliente ConsultarCodeudor(Int64 pnumeroradicacion, Usuario pUsuario)
        {
            try
            {
                return DACredito.ConsultarCodeudor(pnumeroradicacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "ConsultarCodeudor", ex);
                return null;
            }

        }


        /// <summary>
        /// Obtiene el parametro para Habeas Data
        /// </summary>
        /// <param name="pId">identificador de Habeas Data
        /// <returns>Entidad ParametroHabeas Data</returns>
        public Creditos ConsultarParametroHabeas(Usuario pUsuario)
        {
            try
            {
                return DACredito.ConsultarParametroHabeas(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "ConsultarParametroHabeas", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene el parametro para campaña
        /// </summary>
        /// <param name="pId">identificador de Habeas Data
        /// <returns>Entidad Paramaetro campaña Data</returns>
        public Creditos ConsultarParametroCampaña(Usuario pUsuario)
        {
            try
            {
                return DACredito.ConsultarParametroCampaña(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "ConsultarParametroCampaña", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene el parametro para campaña
        /// </summary>
        /// <param name="pId">identificador de Habeas Data
        /// <returns>Entidad Paramaetro campaña Data</returns>
        public Creditos ConsultarParametroVisitaAbogado(Usuario pUsuario)
        {
            try
            {
                return DACredito.ConsultarParametroVisitaAbogado(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "ConsultarParametroVisitaAbogado", ex);
                return null;
            }
        }
        /// <summary>
        /// Obtiene el parametro para CobroJuridico
        /// </summary>
        /// <param name="pId">identificador de CobroJuridico
        /// <returns>Entidad ParametroCobroJuridico</returns>
        public Creditos ConsultarParametroCobroPreJuridico2(Usuario pUsuario)
        {
            try
            {
                return DACredito.ConsultarParametroCobroPreJuridico2(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "ConsultarParametroCobroPreJuridico2", ex);
                return null;
            }
        }
        /// <summary>
        /// Obtiene el parametro para Cobro PreJuridico
        /// </summary>
        /// <param name="pId">identificador de Cobro PreJuridico
        /// <returns>Entidad ParametroCobroPreJuridico</returns>
        public Creditos ConsultarParametroCobroPreJuridico(Usuario pUsuario)
        {
            try
            {
                return DACredito.ConsultarParametroCobroPreJuridico(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "ConsultarParametroCobroPreJuridico", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene el parametro para Permisos
        /// </summary>
        /// <param name="pId">identificador de Permisos 
        /// <returns>Entidad Usuarios</returns>
        public Int32 usuariopermisos(int cod, Usuario pUsuario)
        {
            return DACredito.usuariopermisos(cod, pUsuario);
        }

      

        /// <summary>
        /// Obtiene parametro general  usuario Asesor
        /// </summary>
        /// <param name="pId">identificador de Parametro</param>
        /// <returns>Entidad Diligencia</returns>
        public Diligencia ConsultarparametroUsuarioAsesor(Usuario pUsuario)
        {
            try
            {
                return DACredito.ConsultarparametroUsuarioAsesor(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "ConsultarparametroUsuarioAsesor", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene el parametro para Permisos
        /// </summary>
        /// <param name="pId">identificador de Permisos 
        /// <returns>Entidad Usuarios</returns>
        public Int32 UsuarioEsEjecutivo(int cod, Usuario pUsuario)
        {
            return DACredito.UsuarioEsEjecutivo(cod, pUsuario);
        }


        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<Creditos> ListarCreditoAsesor(Creditos pCredito, Usuario pUsuario, String filtro2, String orden)
        {
            try
            {
                return DACredito.ListarCreditoAsesor(pCredito, pUsuario, filtro2, orden);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "ListarCreditoAsesor", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<Creditos> ListarCreditoXAsesor(Creditos pCredito, Usuario pUsuario, String filtro, String orden)
        {
            try
            {
                return DACredito.ListarCreditoXAsesor(pCredito, pUsuario, filtro, orden);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "ListarCreditoXAsesor", ex);
                return null;
            }
        }
        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<Creditos> ListarCreditoXOficina(Int64 pcodoficina, Usuario pUsuario, String filtro, String orden, bool todasLasOficinas = false)
        {
            try
            {
                return DACredito.ListarCreditoXOficina(pcodoficina, pUsuario, filtro, orden, todasLasOficinas);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "ListarCreditoXOficina", ex);
                return null;
            }
        }

        public List<PersonaMora> ListarPersonasMora(string filtro, Usuario pUsuario)
        {
            try
            {
                return DACredito.ListarPersonasMora(filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "ListarPersonasMora", ex);
                return null;
            }
        }

        public decimal ConsultarTotalValorMoraPersona(string pCod_Persona, string identificacion, DateTime pFechaCorte, Usuario pUsuario)
        {
            try
            {
                return DACredito.ConsultarTotalValorMoraPersona(pCod_Persona, identificacion, pFechaCorte, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "ConsultarTotalValorMoraPersona", ex);
                return 0;
            }
        }


        public List<ProductosMora> ConsultarDetalleMoraPersona(string pCod_Persona, string identificacion, DateTime pFechaCorte, string filtro_productos, Usuario pUsuario)
        {
            try
            {
                return DACredito.ConsultarDetalleMoraPersona(pCod_Persona, identificacion, pFechaCorte, filtro_productos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "ConsultarDetalleMoraPersona", ex);
                return null;
            }
        }


    }
}