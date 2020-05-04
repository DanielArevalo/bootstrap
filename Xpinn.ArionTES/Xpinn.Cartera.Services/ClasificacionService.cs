using System;
using System.Collections.Generic;
using System.Text;
using Xpinn.Util;
using System.ServiceModel;
using Xpinn.Cartera.Entities;
using Xpinn.Cartera.Business;

namespace Xpinn.Cartera.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ClasificacionService
    {

        private ClasificacionBusiness BOClasificacion;
        private ExcepcionBusiness BOExcepcion;

        public ClasificacionService()
        {
            BOClasificacion = new ClasificacionBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "60402"; } }

        public Clasificacion CrearClasificacion(Clasificacion pClasificacion, Usuario pusuario)
        {
            try
            {
                pClasificacion = BOClasificacion.CrearClasificacion(pClasificacion, pusuario);
                return pClasificacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ClasificacionService", "CrearClasificacion", ex);
                return null;
            }
        }


        public Clasificacion ModificarClasificacion(Clasificacion pClasificacion, Usuario pusuario)
        {
            try
            {
                pClasificacion = BOClasificacion.ModificarClasificacion(pClasificacion, pusuario);
                return pClasificacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ClasificacionService", "ModificarClasificacion", ex);
                return null;
            }
        }


        public void EliminarClasificacion(Int64 pId, Usuario pusuario)
        {
            try
            {
                BOClasificacion.EliminarClasificacion(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ClasificacionService", "EliminarClasificacion", ex);
            }
        }


        public Clasificacion ConsultarClasificacion(Int64 pId, Usuario pusuario)
        {
            try
            {
                Clasificacion Clasificacion = new Clasificacion();
                Clasificacion = BOClasificacion.ConsultarClasificacion(pId, pusuario);
                return Clasificacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ClasificacionService", "ConsultarClasificacion", ex);
                return null;
            }
        }


        public List<Clasificacion> ListarClasificacion(Clasificacion pClasificacion, Usuario pusuario)
        {
            try
            {
                return BOClasificacion.ListarClasificacion(pClasificacion, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ClasificacionService", "ListarClasificacion", ex);
                return null;
            }
        }
        public List<ClasificacionCartera> Listarpersonas(Usuario pUsuario, string fechainicio, string fechafin, string oficina)
        {
            try
            {
                return BOClasificacion.Listarpersona(pUsuario, fechainicio, fechafin, oficina);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ClasificacionService", "Listarpersona", ex);
                return null;
            }
        }
        public List<ClasificacionCartera> listarfechas(Usuario pUsuario, string fechainicio, string fechafin)
        {
            try
            {
                return BOClasificacion.listarfechas(pUsuario, fechainicio, fechafin);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ClasificacionService", "listarfechas", ex);
                return null;
            }
        }
        public ClasificacionCartera ConsultarClasificacionHis(string numero_radicacion, string cod_clasifica, string fecha, Usuario pUsuario)
        {
            return BOClasificacion.ConsultarClasificacionHist(numero_radicacion, cod_clasifica, fecha, pUsuario);

        }
    }
}
