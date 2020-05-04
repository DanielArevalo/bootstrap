using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.FabricaCreditos.Business;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Util;
using System.Data;

namespace Xpinn.FabricaCreditos.Services
{
    /// <summary>
    /// Servicio para Aprobador
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class OficinaService
    {
        private OficinaBusiness BOOficina;
        private ExcepcionBusiness BOExcepcion;
        public Int32 codusuario;
        /// <summary>
        /// Constructor del servicio para Caja
        /// </summary>
        public OficinaService()
        {
            BOOficina = new OficinaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "10101"; } }

        public List<Oficina> ListarOficinas(Oficina oficina, Usuario pUsuario)
        {
            try
            {
                return BOOficina.ListarOficinas(oficina, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("OficinaService", "ListarOficinas", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de Oficinas de asesores para Recuperaciones a partir de unos filtros
        /// </summary>
        /// <param name="pCiudad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Ciudad obtenidos</returns>
        public List<Oficina> ListarOficinasAsesores(Oficina oficina, Usuario pUsuario)
        {
            try
            {
                return BOOficina.ListarOficinasAsesores(oficina, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("OficinaService", "ListarOficinasAsesores", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de Oficinas de los usuarios para Recuperaciones a partir de unos filtros
        /// </summary>
        /// <param name="pCiudad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Ciudad obtenidos</returns>
        public List<Oficina> ListarOficinasUsuarios(Oficina oficina, Usuario pUsuario)
        {
            try
            {
                return BOOficina.ListarOficinasUsuarios(oficina, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("OficinaService", "ListarOficinasUsuarios", ex);
                return null;
            }
        }


        public Int32 UsuarioPuedeConsultarCreditosOficinas(int cod, Usuario pUsuario)
        {
            return BOOficina.UsuarioPuedeConsultarCreditosOficinas(cod, pUsuario);
        }

        public Int32 UsuarioPuedecambiartasas(int cod, Usuario pUsuario)
        {
            return BOOficina.UsuarioPuedecambiartasas(cod, pUsuario);
        }

        public void ValidarComisionAporte(string pCod_Linea, ref bool comision, ref bool aporte, ref bool seguro, Usuario pUsuario)
        {
            try
            {
                BOOficina.ValidarComisionAporte(pCod_Linea, ref comision, ref aporte, ref seguro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("OficinaService", "ValidarComisionAporte", ex);
            }
        }


    }
}
