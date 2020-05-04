using System;
using System.Collections.Generic;
using System.Text;
using Xpinn.Util;
using System.ServiceModel;
using Xpinn.Nomina.Entities;
using Xpinn.Nomina.Business;

namespace Xpinn.Nomina.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class DotacionService
    {

        private DotacionBusiness BODotacion;
        private ExcepcionBusiness BOExcepcion;

        public DotacionService()
        {
            BODotacion = new DotacionBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "250204"; } }

        public Dotacion CrearDotacion(Dotacion pDotacion, Usuario pusuario)
        {
            try
            {
                pDotacion = BODotacion.CrearDotacion(pDotacion, pusuario);
                return pDotacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DotacionService", "CrearDotacion", ex);
                return null;
            }
            }


        public Dotacion ModificarDotacion(Dotacion pDotacion, Usuario pusuario)
        {
            try
            {
                pDotacion = BODotacion.ModificarDotacion(pDotacion, pusuario);
                return pDotacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DotacionService", "ModificarDotacion", ex);
                return null;
            }
        }


        public void EliminarDotacion(Int64 pId, Usuario pusuario)
        {
            try
            {
                BODotacion.EliminarDotacion(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DotacionService", "EliminarDotacion", ex);
            }
        }
        public Dotacion ConsultarDatosDotacion(Int64 pId, Usuario pusuario)
        {
            try
            {
                Dotacion Dotacion = new Dotacion();
                Dotacion = BODotacion.ConsultarDatosDotacion(pId, pusuario);
                return Dotacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DotacionService", "ConsultarDotacion", ex);
                return null;
            }
        }

        public Dotacion ConsultarDotacion(Int64 pId, Usuario pusuario)
        {
            try
            {
                Dotacion Dotacion = new Dotacion();
                Dotacion = BODotacion.ConsultarDotacion(pId, pusuario);
                return Dotacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DotacionService", "ConsultarDotacion", ex);
                return null;
            }
        }


        public List<Dotacion> ListarDotacion(string filtro, Usuario pusuario)
        {
            try
            {
                return BODotacion.ListarDotacion(filtro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DotacionService", "ListarDotacion", ex);
                return null;
            }
        }


    }
}
