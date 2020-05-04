using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Auxilios.Entities;
using Xpinn.Auxilios.Business;
using Xpinn.Util;

namespace Xpinn.Auxilios.Services
{
    public class LineaAuxilioServices
    {
        public string CodigoPrograma { get { return "70201"; } }
        public string CodigoProgramaParametroAuxilio { get { return "31008"; } }

        private LineaAuxilioBusiness BOAuxilio;
        private ExcepcionBusiness BOExcepcion;

        public LineaAuxilioServices()
        {
            BOAuxilio = new LineaAuxilioBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }


        

        public LineaAuxilio CrearLineaAuxilio(LineaAuxilio pLineaAuxilio, Usuario pusuario)
        {
            try
            {
                pLineaAuxilio = BOAuxilio.CrearLineaAuxilio(pLineaAuxilio, pusuario);
                return pLineaAuxilio;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaAuxilioService", "CrearLineaAuxilio", ex);
                return null;
            }
        }


        public LineaAuxilio ModificarLineaAuxilio(LineaAuxilio pLineaAuxilio, Usuario pusuario)
        {
            try
            {
                pLineaAuxilio = BOAuxilio.ModificarLineaAuxilio(pLineaAuxilio, pusuario);
                return pLineaAuxilio;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaAuxilioService", "ModificarLineaAuxilio", ex);
                return null;
            }
        }


        public void EliminarLineaAuxilios(Int64 pId, Usuario pusuario)
        {
            try
            {
                BOAuxilio.EliminarLineaAuxilios(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaAuxilioService", "EliminarLineaAuxilio", ex);
            }
        }

        public LineaAuxilio Crear_MOD_LineaAuxilio(LineaAuxilio pAuxilio, Usuario vUsuario, int Opcion)
        {
            try
            {
                if(Opcion==1)
                    return BOAuxilio.CrearLineaAuxilio(pAuxilio, vUsuario,Opcion);
                else
                    return BOAuxilio.ModificarLineaAuxilio(pAuxilio, vUsuario, Opcion);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaAuxilioServices", "Crear_MOD_LineaAuxilio", ex);
                return null;
            }
        }


        public LineaAuxilio ConsultarLineaAuxilio(Int64 pId, Usuario pusuario)
        {
            try
            {
                LineaAuxilio LineaAuxilio = new LineaAuxilio();
                LineaAuxilio = BOAuxilio.ConsultarAuxilio(pId, pusuario);
                return LineaAuxilio;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaAuxilioService", "ConsultarLineaAuxilio", ex);
                return null;
            }
        }
        //public SolicitudAuxilio ModificarSolicitudAuxilio(SolicitudAuxilio pServicio, Usuario vUsuario)
        //{
        //    try
        //    {
        //        return BOAuxilio.ModificarSolicitudAuxilio(pServicio, vUsuario);
        //    }
        //    catch (Exception ex)
        //    {
        //        BOExcepcion.Throw("LineaAuxilioServices", "ModificarSolicitudAuxilio", ex);
        //        return null;
        //    }
        //}


        public List<LineaAuxilio> ListarLineaAuxilio(LineaAuxilio pAuxilio, Usuario vUsuario, string filtro)
        {
            try
            {
                return BOAuxilio.ListarLineaAuxilio(pAuxilio, vUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaAuxilioServices", "ListarLineaAuxilio", ex);
                return null;
            }
        }


        public void EliminarLineaAuxilio(Int64 pId, Usuario vUsuario)
        {
            try
            {
                BOAuxilio.EliminarLineaAuxilio(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaAuxilioServices", "EliminarLineaAuxilio", ex);
            }
        }

        public LineaAuxilio ConsultarLineaAUXILIO(string pId, Usuario vUsuario)
        {
            try
            {
                return BOAuxilio.ConsultarLineaAUXILIO(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaAuxilioServices", "ConsultarLineaAUXILIO", ex);
                return null;
            }
        }



        //Detalle

        public void EliminarDETALLELineaAux(Int64 pId, Usuario vUsuario)
        {
            try
            {
                BOAuxilio.EliminarDETALLELineaAux(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaAuxilioServices", "EliminarDETALLELineaAux", ex);
            }
        }


        public List<RequisitosLineaAuxilio> ConsultarDETALLELineaAux(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return BOAuxilio.ConsultarDETALLELineaAux(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaAuxilioServices", "ConsultarDETALLELineaAux", ex);
                return null;
            }
        }


        
    }
}
