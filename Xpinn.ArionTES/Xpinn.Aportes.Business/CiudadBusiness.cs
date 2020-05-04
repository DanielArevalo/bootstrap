using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Aportes.Data;
using Xpinn.Aportes.Entities;

namespace Xpinn.Aportes.Business
{
    /// <summary>
    /// Objeto de negocio para Beneficiario
    /// </summary>
    public class CiudadBusiness : GlobalBusiness
    {
        private CiudadData DACiudad;


        public CiudadBusiness()
        {
            DACiudad = new CiudadData();
        }


        public Ciudad Crear_Mod_Ciudad(Ciudad pCiudad, Usuario vUsuario, int opcion)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pCiudad = DACiudad.Crear_Mod_Ciudad(pCiudad, vUsuario,opcion);
                    ts.Complete();
                }
                return pCiudad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CiudadBusiness", "Crear_Mod_Ciudad", ex);
                return null;
            }
        }

        public Ciudad ConsultarCiudad(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return DACiudad.ConsultarCiudad(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CiudadBusiness", "ConsultarCiudad", ex);
                return null;
            }
        }


        public List<Ciudad> ListarCiudad(string filtro, Usuario vUsuario)
        {
            try
            {
                return DACiudad.ListarCiudad(filtro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CiudadBusiness", "ListarCiudad", ex);
                return null;
            }
        }

        public void EliminarCiudad(Int64 pId, Usuario vUsuario)
        {
            try
            {
                DACiudad.EliminarCiudad(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CiudadBusiness", "EliminarCiudad", ex);               
            }
        }

        
    }
}