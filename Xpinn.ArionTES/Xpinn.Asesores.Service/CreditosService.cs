using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Asesores.Business;
using Xpinn.Asesores.Entities;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.Asesores.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class CreditosService
    {
        private CreditosBusiness BOCredito;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Credito
        /// </summary>
        public CreditosService()
        {
            BOCredito = new CreditosBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "100138"; } }
        public string CodigoProgramaoriginal { get { return "100140"; } }




        /// <summary>
        /// Servicio para obtener Credito
        /// </summary>
        /// <param name="pId">identificador de Credito</param>
        /// <returns>Entidad Credito</returns>
        public Creditos ConsultarCreditoAsesor(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOCredito.ConsultarCreditoAsesor(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "ConsultarCreditoAsesor", ex);
                return null;
            }
        }

        public List<Garantia> ListarCreditosGVPorFiltro(string filtroDefinido, string filtroGrilla, Usuario pUsuario)
        {
            try
            {
                return BOCredito.ListarCreditosGVPorFiltro(filtroDefinido, filtroGrilla, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "ListarCreditosGVPorFiltro", ex);
                return null;
            }
        }


        public Cliente ConsultarCodeudor(Int64 pnumeroradicacion, Usuario pUsuario)
        {
            try
            {
                return BOCredito.ConsultarCodeudor(pnumeroradicacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "ConsultarCodeudor", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene el parametro para campaña
        /// </summary>
        /// <param name="pId">identificador de Habeas Data
        /// <returns>Entidad ParametroHabeas Data</returns>
        public Creditos ConsultarParametroCampaña(Usuario pUsuario)
        {
            try
            {
                return BOCredito.ConsultarParametroCampaña(pUsuario);
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
        /// <returns>Entidad ParametroHabeas Data</returns>
        public Creditos ConsultarParametroVisitaAbogado(Usuario pUsuario)
        {
            try
            {
                return BOCredito.ConsultarParametroVisitaAbogado(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "ConsultarParametroVisitaAbogado", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener el parametro para habeas Data
        /// </summary>
        /// <param name="pId">identificador de Habeas Data</param>
        /// <returns>Entidad Parametro Habeas Data</returns>
        public Creditos ConsultarParametroHabeas(Usuario pUsuario)
        {
            try
            {
                return BOCredito.ConsultarParametroHabeas(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "ConsultarParametroHabeas", ex);
                return null;
            }
        }
        /// <summary>
        /// Servicio para obtener el parametro para CobroJuridico
        /// </summary>
        /// <param name="pId">identificador de CobroJuridico
        /// <returns>Entidad ParametroCobroJuridico</returns>
        public Creditos ConsultarParametroCobroPreJuridico2(Usuario pUsuario)
        {
            try
            {
                return BOCredito.ConsultarParametroCobroPreJuridico2(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "ConsultarParametroCobroPreJuridico2", ex);
                return null;
            }
        }
        /// <summary>
        /// Servicio para obtener el parametro para CobroPreJuridico
        /// </summary>
        /// <param name="pId">identificador de CobroPreJuridico
        /// <returns>Entidad ParametroCobroJuridico</returns>
        public Creditos ConsultarParametroCobroPreJuridico(Usuario pUsuario)
        {
            try
            {
                return BOCredito.ConsultarParametroCobroPreJuridico(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "ConsultarParametroCobroPreJuridico", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener el parametro para Permisos
        /// </summary>
        /// <param name="pId">identificador de Permisos 
        /// <returns>Entidad Usuarios</returns>
        public Int32 UsuarioEsEjecutivo(int cod, Usuario pUsuario)
        {
            return BOCredito.UsuarioEsEjecutivo(cod, pUsuario);
        }

        /// <summary>
        /// Servicio para obtener el parametro para PermisosDirectores
        /// </summary>
        /// <param name="pId">identificador de Permisos 
        /// <returns>Entidad Usuarios</returns>
        public Int32 usuariopermisos(int cod, Usuario pUsuario)
        {
            return BOCredito.usuariopermisos(cod, pUsuario);
        }




        /// <summary>
        /// Servicio para obtener parametro general usuario director
        /// </summary>
        /// <param name="pId">identificador de Parametro</param>
        /// <returns>Entidad Diligencia</returns>
        public Diligencia ConsultarparametroUsuarioAsesor(Usuario pUsuario)
        {
            try
            {
                return BOCredito.ConsultarparametroUsuarioAsesor(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "ConsultarparametroUsuarioAsesor", ex);
                return null;
            }
        }



        /// <summary>
        /// Servicio para obtener lista de Creditos a partir de unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<Creditos> ListarCreditoAsesor(Creditos pCredito, Usuario pUsuario, String filtro2, String orden)
        {
            try
            {
                return BOCredito.ListarCreditoAsesor(pCredito, pUsuario, filtro2, orden);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "ListarCreditoAsesor", ex);
                return null;
            }
        }


        /// <summary>
        /// Servicio para obtener lista de Creditos x asesor a partir de unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<Creditos> ListarCreditoXAsesor(Creditos pCredito, Usuario pUsuario, String filtro, String orden)
        {
            try
            {
                return BOCredito.ListarCreditoXAsesor(pCredito, pUsuario, filtro, orden);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "ListarCreditoXAsesor", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de Creditos x oficina a partir de unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<Creditos> ListarCreditoXOficina(Int64 pcodoficina, Usuario pUsuario, String filtro, String orden, bool todasLasOficinas = false)
        {
            try
            {
                return BOCredito.ListarCreditoXOficina(pcodoficina, pUsuario, filtro, orden, todasLasOficinas);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "ListarCreditoXOficina", ex);
                return null;
            }
        }

        public List<PersonaMora> ListarPersonasMora(string filtro, Usuario pUsuario)
        {
            try
            {
                return BOCredito.ListarPersonasMora(filtro, pUsuario);
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
                return BOCredito.ConsultarTotalValorMoraPersona(pCod_Persona, identificacion, pFechaCorte, pUsuario);
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
                return BOCredito.ConsultarDetalleMoraPersona(pCod_Persona, identificacion, pFechaCorte, filtro_productos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "ConsultarDetalleMoraPersona", ex);
                return null;
            }
        }


    }
}