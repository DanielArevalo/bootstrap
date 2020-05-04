using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Contabilidad.Data;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.Contabilidad.Business
{
    /// <summary>
    /// Objeto de negocio para CentroCosto
    /// </summary>
    public class CentroGestionBusiness : GlobalBusiness
    {
        private CentroGestionData DACentro;

        /// <summary>
        /// Constructor del objeto de negocio para CentroCosto
        /// </summary>
        public CentroGestionBusiness()
        {
            DACentro = new CentroGestionData();
        }


        public CentroGestion Crear_Mod_CentroGestion(CentroGestion pCentro, Usuario vUsuario, int opcion)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pCentro = DACentro.Crear_Mod_CentroGestion(pCentro, vUsuario, opcion);
                    ts.Complete();
                }

                return pCentro;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CentroGestionBusiness", "CrearCentroCosto", ex);
                return null;
            }
        }


        public List<CentroGestion> ListarCentroGestion(CentroGestion pCentro,String filtro, Usuario vUsuario)
        {
            try
            {
                return DACentro.ListarCentroGestion(pCentro, filtro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CentroGestionBusiness", "ListarCentroGestion", ex);
                return null;
            }
        }


        public void EliminarCentroGestion(Int64 pId, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DACentro.EliminarCentroGestion(pId, vUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CentroGestionBusiness", "EliminarCentroGestion", ex);
            }
        }


        public CentroGestion ConsultarCentroGestion(Int64 pId, Usuario vUsuario)
        {
            try
            {
                CentroGestion CentroGest = new CentroGestion();

                CentroGest = DACentro.ConsultarCentroGestion(pId, vUsuario);

                return CentroGest;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CentroGestionBusiness", "ConsultarCentroGestion", ex);
                return null;
            }
        }

        
        
    }
}

