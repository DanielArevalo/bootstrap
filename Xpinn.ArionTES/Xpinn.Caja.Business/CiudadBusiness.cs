using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Caja.Data;
using Xpinn.Caja.Entities;

namespace Xpinn.Caja.Business
{
    /// <summary>
    /// Objeto de negocio para Ciudad
    /// </summary>
    public class CiudadBusiness:GlobalData
    {
        private CiudadData DACiudad;

        /// <summary>
        /// Constructor del objeto de negocio para Ciudad
        /// </summary>
        public CiudadBusiness()
        {
            DACiudad = new CiudadData();
        }

        /// <summary>
        /// Obtiene la lista de Ciudades dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Ciudades obtenidos</returns>
        public List<Ciudad> ListarCiudad(Ciudad pCiudad, Usuario pUsuario)
        {
            try
            {
                return DACiudad.ListarCiudad(pCiudad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CiudadBusiness", "ListarCiudad", ex);
                return null;
            }
        }

        public Ciudad ConsultarCiudadXNombre(Ciudad pCiudad, Usuario pUsuario)
        {
            try
            {
                return DACiudad.ConsultarCiudadXNombre(pCiudad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CiudadBusiness", "ConsultarCiudadXNombre", ex);
                return null;
            }
        }

        public List<Ciudad> ListadoCiudad(Ciudad pCiudad, Usuario pUsuario)
        {
            try
            {
                return DACiudad.ListadoCiudad(pCiudad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CiudadBusiness", "ListadoCiudad", ex);
                return null;
            }
        }

        public Ciudad CiudadTran(Usuario pUsuario)
        {
            try
            {
                return DACiudad.CiudadTran(pUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Ciudad", "Ciudad", ex);
                return null;
            }
        }
    }
}
