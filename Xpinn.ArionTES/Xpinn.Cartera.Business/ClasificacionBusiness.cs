using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Cartera.Data;
using Xpinn.Cartera.Entities;
 
namespace Xpinn.Cartera.Business
{
 
    public class ClasificacionBusiness : GlobalBusiness
    {
 
        private ClasificacionData DAClasificacion;
 
        public ClasificacionBusiness()
        {
            DAClasificacion = new ClasificacionData();
        }
 
        public Clasificacion CrearClasificacion(Clasificacion pClasificacion, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pClasificacion = DAClasificacion.CrearClasificacion(pClasificacion, pusuario);
 
                    ts.Complete();
 
                }
 
                return pClasificacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ClasificacionBusiness", "CrearClasificacion", ex);
                return null;
            }
        }
 
 
        public Clasificacion ModificarClasificacion(Clasificacion pClasificacion, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pClasificacion = DAClasificacion.ModificarClasificacion(pClasificacion, pusuario);
 
                    ts.Complete();
 
                }
 
                return pClasificacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ClasificacionBusiness", "ModificarClasificacion", ex);
                return null;
            }
        }
 
 
        public void EliminarClasificacion(Int64 pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAClasificacion.EliminarClasificacion(pId, pusuario);
 
                    ts.Complete();
 
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ClasificacionBusiness", "EliminarClasificacion", ex);
            }
        }
 
 
        public Clasificacion ConsultarClasificacion(Int64 pId, Usuario pusuario)
        {
            try
            {
                Clasificacion Clasificacion = new Clasificacion();
                Clasificacion = DAClasificacion.ConsultarClasificacion(pId, pusuario);
                return Clasificacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ClasificacionBusiness", "ConsultarClasificacion", ex);
                return null;
            }
        }
 
 
        public List<Clasificacion> ListarClasificacion(Clasificacion pClasificacion, Usuario pusuario)
        {
            try
            {
                return DAClasificacion.ListarClasificacion(pClasificacion, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ClasificacionBusiness", "ListarClasificacion", ex);
                return null;
            }
        }
        public List<ClasificacionCartera> Listarpersona(Usuario pUsuario, string fechainicio, string fechafin, string oficina)
        {
            try
            {
                return DAClasificacion.listarpersonas(pUsuario, fechainicio, fechafin, oficina);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ClasificacionBusiness", "Listarpersona", ex);
                return null;
            }
        }
        public List<ClasificacionCartera> listarfechas(Usuario pUsuario, string fechainicio, string fechafin)
        {
            try
            {
                return DAClasificacion.listarfechas(pUsuario, fechainicio, fechafin);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ClasificacionBusiness", "listarfechas", ex);
                return null;
            }
        }
        public ClasificacionCartera ConsultarClasificacionHist(string numero_radicacion, string cod_clasifica, string fecha, Usuario pUsuario)
        {
            return DAClasificacion.ConsultarClasificacionHist(numero_radicacion, cod_clasifica, fecha, pUsuario);

        }
    }
}
