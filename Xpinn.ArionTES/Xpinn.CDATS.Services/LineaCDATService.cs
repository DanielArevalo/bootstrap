using System;
using System.Collections.Generic;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.CDATS.Business;
using Xpinn.CDATS.Entities;

namespace Xpinn.CDATS.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class LineaCDATService
    {

        private LineaCDATBusiness BOLineaCDAT;
        private ExcepcionBusiness BOExcepcion;

        public LineaCDATService()
        {
            BOLineaCDAT = new LineaCDATBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "220104"; } }

        public LineaCDAT CrearLineaCDAT(LineaCDAT pLineaCDAT, Usuario pusuario)
        {
            try
            {
                pLineaCDAT = BOLineaCDAT.CrearLineaCDAT(pLineaCDAT, pusuario);
                return pLineaCDAT;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaCDATService", "CrearLineaCDAT", ex);
                return null;
            }
        }


        public LineaCDAT ModificarLineaCDAT(LineaCDAT pLineaCDAT, Usuario pusuario)
        {
            try
            {
                pLineaCDAT = BOLineaCDAT.ModificarLineaCDAT(pLineaCDAT, pusuario);
                return pLineaCDAT;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaCDATService", "ModificarLineaCDAT", ex);
                return null;
            }
        }


        public void EliminarLineaCDAT(string pId, Usuario pusuario)
        {
            try
            {
                BOLineaCDAT.EliminarLineaCDAT(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaCDATService", "EliminarLineaCDAT", ex);
            }
        }


        public LineaCDAT ConsultarLineaCDAT(string pId, Usuario pusuario)
        {
            try
            {
                LineaCDAT LineaCDAT = new LineaCDAT();
                LineaCDAT = BOLineaCDAT.ConsultarLineaCDAT(pId, pusuario);
                return LineaCDAT;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaCDATService", "ConsultarLineaCDAT", ex);
                return null;
            }
        }


        public List<LineaCDAT> ListarLineaCDAT(LineaCDAT pLinea, Usuario pusuario)
        {
            try
            {
                return BOLineaCDAT.ListarLineaCDAT(pLinea, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaCDATService", "ListarLineaCDAT", ex);
                return null;
            }
        }

        public void EliminarRangoCDAT(Int64 pId, Usuario pusuario)
        {
            try
            {
                BOLineaCDAT.EliminarRangoCDAT(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaCDATService", "EliminarRangoCDAT", ex);
            }
        }

        public RangoCDAT ConsultarRangoCDATPorLineaYTipoTope(RangoCDAT rango, Usuario usuario)
        {
            try
            {
                return BOLineaCDAT.ConsultarRangoCDATPorLineaYTipoTope(rango, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaCDATBusiness", "ConsultarRangoCDATPorLineaYTipoTope", ex);
                return null;
            }
        }
    }
}