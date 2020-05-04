using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Data;
using Xpinn.FabricaCreditos.Entities;
using System.Transactions;

namespace Xpinn.FabricaCreditos.Business
{
    public class ActividadesBusiness : GlobalData
    {

        private ActividadesData DAActividad;


        public ActividadesBusiness()
        {
            DAActividad = new ActividadesData();
        }


        public Actividades CrearActividadesPersona(Actividades pActividad, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pActividad = DAActividad.CrearActividadesPersona(pActividad, pUsuario);

                    ts.Complete();
                }

                return pActividad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActividadPersonaBusiness", "CrearActividadesPersona", ex);
                return null;
            }
        }



        public Actividades ModificarActividadesPersona(Actividades pActividad, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pActividad = DAActividad.ModificarActividadesPersona(pActividad, vUsuario);

                    ts.Complete();
                }

                return pActividad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActividadPersonaBusiness", "ModificarActividadesPersona", ex);
                return null;
            }
        }


        public List<Actividades> ListarActividadesPersona(Actividades pActividad, Usuario pUsuario)
        {
            try
            {
                return DAActividad.ListarActividadesPersona(pActividad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActividadPersonaBusiness", "ListarActividadesPersona", ex);
                return null;
            }
        }



        public List<Actividades> ConsultarActividad(Int64 pId, Usuario vUsuario)
        {
            try
            {

                return DAActividad.ConsultarActividad(pId, vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActividadPersonaBusiness", "ConsultarActividad", ex);
                return null;
            }
        }




        public void EliminarActividadPersona(Int64 pIdActividad, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAActividad.EliminarActividadPersona(pIdActividad, vUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActividadPersonaBusiness", "EliminarActividadPersona", ex);
            }
        }

        public List<Actividades> ConsultarActividadesEconomicasSecundarias(Int64 pCodPersona, Usuario vUsuario)
        {
            List<Actividades> lstActividadCIIU = null;
            try
            {
                lstActividadCIIU = DAActividad.ConsultarActividadesEconomicasSecundarias(pCodPersona, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActividadPersonaBusiness", "ConsultarActividadesEconomicasSecundarias", ex);
                return null;
            }
            return lstActividadCIIU;
        }

        #region PERSONA INFORMACION FINANCIERA

        public List<CuentasBancarias> ConsultarCuentasBancarias(Int64 pId, string filtro, Usuario vUsuario)
        {
            try
            {

                return DAActividad.ConsultarCuentasBancarias(pId, filtro, vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasPersonaBusiness", "ConsultarCuentasBancarias", ex);
                return null;
            }
        }

        public void EliminarCuentasBancarias(Int64 pIdCuentabancaria, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAActividad.EliminarCuentasBancarias(pIdCuentabancaria, vUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasPersonaBusiness", "EliminarCuentasBancarias", ex);
            }
        }


        #endregion

        #region ACTIVIDAD ECONOMICA

        public List<Actividades> ConsultarActividadEconomica(Actividades Actividades, Usuario vUsuario)
        {
            try
            {

                return DAActividad.ConsultarActividadEconomica(Actividades, vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActividadesBusiness", "ConsultarActividadEconomica", ex);
                return null;
            }
        }

        public Actividades ConsultarActividadEconomicaId(Int64 pId, Usuario vUsuario)
        {
            try
            {
                Actividades Actividad = new Actividades();
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    Actividad = DAActividad.ConsultarActividadEconomicaId(pId, vUsuario);
                    ts.Complete();
                }
                return Actividad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActividadesBusiness", "ConsultarActividadEconomicaId", ex);
                return null;
            }
        }

        public Actividades CrearActividadEconomica(Actividades pActividad, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pActividad = DAActividad.CrearActividadEconomica(pActividad, pUsuario);

                    ts.Complete();
                }

                return pActividad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActividadesBusiness", "CrearActividadEconomica", ex);
                return null;
            }
        }

        public Actividades ModificarActividadEconomica(String pCodactividad, Actividades pActividad, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pActividad = DAActividad.ModificarActividadEconomica(pCodactividad, pActividad, vUsuario);

                    ts.Complete();
                }

                return pActividad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActividadesBusiness", "ModificarActividadEconomica", ex);
                return null;
            }
        }

        public List<Actividades> listarTemasInteres(Usuario pUsuario)
        {
            try
            {
                return DAActividad.listarTemasInteres(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActividadesBusiness", "listarTemasInteres", ex);
                return null;
            }
        }

        public void EliminarActividadEconomica(String pCodactividad, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAActividad.EliminarActividadEconomica(pCodactividad, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActividadesBusiness", "EliminarActividadEconomica", ex);
            }
        }

        #endregion
        
    }
}
