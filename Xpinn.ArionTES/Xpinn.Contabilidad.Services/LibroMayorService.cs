using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Contabilidad.Business;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.Contabilidad.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class LibroMayorService
    {
        private LibroMayorBusiness BOLibroMayor;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para el libro mayor
        /// </summary>
        public LibroMayorService()
        {
            BOLibroMayor = new LibroMayorBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "30205"; } }
        public string CodigoProgramaNiif { get { return "210116"; } }
        public List<LibroMayor> ListarLibroMayor(LibroMayor pEntidad, ref Double TotDeb, ref Double TotCre, Usuario vUsuario, bool isNiif)
        {
            return BOLibroMayor.ListarLibroMayor(pEntidad, ref TotDeb, ref TotCre, vUsuario, isNiif);
        }


        /// <summary>
        /// Servicio para obtener lista de Fecha de Corte a partir de unos filtros
        /// </summary>
        /// <param name="pMovimientoCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Fecha de Corte obtenidos</returns>
        public List<LibroMayor> ListarFechaCorte(Usuario pUsuario, bool isNiif = false)
        {
            try
            {
                List<LibroMayor> lstMayor = new List<LibroMayor>();
                lstMayor = BOLibroMayor.ListarFechaCierre(pUsuario, isNiif);
                return lstMayor;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LibroMayorServices", "ListarFechaCorte", ex);
                return null;
            }
        }

        /// <summary>
        /// Método para consultar datos de la empresa
        /// </summary>
        /// <param name="empresa"></param>
        /// <param name="nit"></param>
        public void DatosEmpresa(ref string empresa, ref string nit, Usuario pUsuario)
        {
            BOLibroMayor.DatosEmpresa(ref empresa, ref nit, pUsuario);
        }

    }
}