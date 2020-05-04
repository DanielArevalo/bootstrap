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
    public class ParametrosAfiliacionBusiness : GlobalBusiness
    {
        private ParametrosAfiliacionData DAParametro;


        public ParametrosAfiliacionBusiness()
        {
            DAParametro = new ParametrosAfiliacionData();
        }


        public ParametrosAfiliacion CrearParametrosAfiliacion(ParametrosAfiliacion pParam, Usuario vUsuario, int opcion)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pParam = DAParametro.CrearParametrosAfiliacion(pParam, vUsuario, opcion);
                    ts.Complete();
                }
                return pParam;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametrosAfiliacionBusiness", "CrearParametrosAfiliacion", ex);
                return null;
            }
        }

        public ParametrosAfiliacion ConsultarParametrosAfiliacion(Int32 pId, Usuario vUsuario)
        {
            try
            {
                return DAParametro.ConsultarParametrosAfiliacion(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametrosAfiliacionBusiness", "ConsultarParametrosAfiliacion", ex);
                return null;
            }
        }


        public List<ParametrosAfiliacion> ListarParametrosAfiliacion(string filtro, Usuario vUsuario)
        {
            try
            {
                return DAParametro.ListarParametrosAfiliacion(filtro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametrosAfiliacionBusiness", "ListarCiudad", ex);
                return null;
            }
        }

        public void EliminarParametrosAfiliacion(Int32 pId, Usuario vUsuario)
        {
            try
            {
                DAParametro.EliminarParametrosAfiliacion(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametrosAfiliacionBusiness", "EliminarParametrosAfiliacion", ex);               
            }
        }


        public PersonaActualizacion CrearPersona_Actualizacion(PersonaActualizacion pPersona, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pPersona = DAParametro.CrearPersona_Actualizacion(pPersona, vUsuario);
                    ts.Complete();
                }
                return pPersona;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametrosAfiliacionBusiness", "CrearPersona_Actualizacion", ex);
                return null;
            }
        }


        public PersonaActualizacion ConsultarPersona_actualizacion(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return DAParametro.ConsultarPersona_actualizacion(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametrosAfiliacionBusiness", "ConsultarPersona_actualizacion", ex);
                return null;
            }
        }


        public void ModificarPersona_Actualizacion(ref string pError, List<PersonaActualizacion> lstPersona, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (lstPersona.Count != null && lstPersona.Count > 0)
                    {
                        foreach (PersonaActualizacion nActualizar in lstPersona)
                        {
                            PersonaActualizacion pEntidad = new PersonaActualizacion();
                            pEntidad = DAParametro.ModificarPersona_Actualizacion(nActualizar, vUsuario);
                        }
                    }
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                pError = ex.Message;
            }
        }


        public void EliminarPersona_Actualizacion(decimal pId, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAParametro.EliminarPersona_Actualizacion(pId, vUsuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametrosAfiliacionBusiness", "EliminarPersona_Actualizacion", ex);
            }
        }

    }
}