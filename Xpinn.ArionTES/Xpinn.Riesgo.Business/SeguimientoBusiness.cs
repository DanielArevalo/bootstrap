using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using Xpinn.Riesgo.Data;
using Xpinn.Riesgo.Entities;
using System.Transactions;

namespace Xpinn.Riesgo.Entities
{
    public class SeguimientoBusiness : GlobalBusiness
    {
        private SeguimientoData DASeguimiento;

        /// <summary>
        /// Constructor para el acceso a la capa Data
        /// </summary>
        public SeguimientoBusiness()
        {
            DASeguimiento = new SeguimientoData();
        }

        /// <summary>
        /// Crear un registro de una forma de control
        /// </summary>
        /// <param name="pControl">Objeto con los datos de la forma de control</param>
        /// <param name="vUsuario"></param>
        /// <returns>Objeto con el código asignado</returns>
        public Seguimiento CrearFormaControl(Seguimiento pControl, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pControl = DASeguimiento.CrearFormaControl(pControl, vUsuario);
                    ts.Complete();
                    return pControl;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SeguimientoBusiness", "CrearFormaControl", ex);
                return null;
            }
        }

        ///<summary>
        /// Modificar un registro de una forma de control
        /// </summary>
        /// <param name="pControl">Objeto con los datos de la forma de control</param>
        /// <param name="vUsuario"></param>
        /// <returns>Objeto con el código asignado</returns>
        public Seguimiento ModificarFormaControl(Seguimiento pControl, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pControl = DASeguimiento.ModificarFormaControl(pControl, vUsuario);
                    ts.Complete();
                    return pControl;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SeguimientoBusiness", "ModificarFormaControl", ex);
                return null;
            }
        }

        ///<summary>
        /// Eliminar un registro de una forma de control
        /// </summary>
        /// <param name="pControl">Objeto con el código de la forma de control</param>
        /// <param name="vUsuario"></param>
        public void EliminarFormaControl(Seguimiento pControl, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DASeguimiento.EliminarFormaControl(pControl, vUsuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SeguimientoBusiness", "EliminarFormaControl", ex);
            }
        }

        ///<summary>
        /// Crear un registro de un tipo de monitoreo
        /// </summary>
        /// <param name="pControl">Objeto con los datos del tipo de monitoreo</param>
        /// <param name="vUsuario"></param>
        /// <returns>Objeto con el código asignado</returns>
        public Seguimiento CrearTipoMonitoreo(Seguimiento pMonitoreo, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pMonitoreo = DASeguimiento.CrearTipoMonitoreo(pMonitoreo, vUsuario);
                    ts.Complete();
                    return pMonitoreo;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SeguimientoBusiness", "CrearTipoMonitoreo", ex);
                return null;
            }
        }

        ///<summary>
        /// Modificar un registro de un tipo de monitoreo
        /// </summary>
        /// <param name="pControl">Objeto con los datos del tipo de monitoreo</param>
        /// <param name="vUsuario"></param>
        /// <returns>Objeto con los datos modificados</returns>
        public Seguimiento ModificarTipoMonitoreo(Seguimiento pMonitoreo, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pMonitoreo = DASeguimiento.ModificarTipoMonitoreo(pMonitoreo, vUsuario);
                    ts.Complete();
                    return pMonitoreo;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SeguimientoBusiness", "ModificarTipoMonitoreo", ex);
                return null;
            }
        }

        ///<summary>
        /// Eliminar un registro de un tipo de monitoreo
        /// </summary>
        /// <param name="pControl">Objeto con el código del tipo de monitoreo</param>
        /// <param name="vUsuario"></param>
        public void EliminarTipoMonitoreo(Seguimiento pMonitoreo, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DASeguimiento.EliminarTipoMonitoreo(pMonitoreo, vUsuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SeguimientoBusiness", "EliminarTipoMonitoreo", ex);
            }
        }

        /// <summary>
        /// Consultar los datos de una forma de control especifica
        /// </summary>
        /// <param name="pParametro">Objetos con los datos para el filtro</param>
        /// <param name="vUsuario"></param>
        /// <returns>Objeto con los datos obtenidos</returns>
        public Seguimiento ConsultarFormaControl(Seguimiento pControl, Usuario vUsuario)
        {
            try
            {
                pControl = DASeguimiento.ConsultarFormaControl(pControl, vUsuario);
                return pControl;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SeguimientoBusiness", "ConsultarFormaControl", ex);
                return null;
            }
        }

        /// <summary>
        /// Lista de formas de control bajo un filtro especifico
        /// </summary>
        /// <param name="pParametro">Objeto con datos para el filtro</param>
        /// <param name="vUsuario"></param>
        /// <returns>Lista con los objetos correspondientes al filtro</returns>
        public List<Seguimiento> ListarFormasControl(Seguimiento pControl, Usuario vUsuario)
        {
            try
            {
                List<Seguimiento> lstFormasControl = new List<Seguimiento>();
                lstFormasControl = DASeguimiento.ListarFormasControl(pControl, vUsuario);
                return lstFormasControl;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SeguimientoBusiness", "ListarFormasControl", ex);
                return null;
            }
        }

        /// <summary>
        /// Consultar tipo de monitoreo especifico
        /// </summary>
        /// <param name="pParametro">Objeto con los datos para el filtro</param>
        /// <param name="vUsuario"></param>
        /// <returns>Objeto con los datos obtenidos</returns>
        public Seguimiento ConsultarTipoMonitoreo(Seguimiento pMonitoreo, Usuario vUsuario)
        {
            try
            {
                pMonitoreo = DASeguimiento.ConsultarTipoMonitoreo(pMonitoreo, vUsuario);
                return pMonitoreo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SeguimientoBusiness", "ConsultarTipoMonitoreo", ex);
                return null;
            }
        }

        /// <summary>
        /// Lista de tipos de monitoreo bajo un filtro especifico
        /// </summary>
        /// <param name="pParametro">Objeto con datos para el filtro</param>
        /// <param name="vUsuario"></param>
        /// <returns>Lista con los objetos correspondientes al filtro</returns>
        public List<Seguimiento> ListarTiposMonitoreo(Seguimiento pMonitoreo, Usuario vUsuario)
        {
            try
            {
                List<Seguimiento> lstTiposMonitoreo = new List<Seguimiento>();
                lstTiposMonitoreo = DASeguimiento.ListarTiposMonitoreo(pMonitoreo, vUsuario);
                return lstTiposMonitoreo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SeguimientoBusiness", "ListarTiposMonitoreo", ex);
                return null;
            }
        }
    }
}
