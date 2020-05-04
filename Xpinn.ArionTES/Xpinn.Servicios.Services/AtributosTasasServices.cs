using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Servicios.Entities;
using Xpinn.Servicios.Business;
using Xpinn.Util;

namespace Xpinn.Servicios.Services
{
    public class AtributosTasasServices
    {
        public string CodigoPrograma { get { return "80102"; } }
        
        AtributosTasasBusiness BORangoTasa;
        ExcepcionBusiness BOExcepcion;

        public AtributosTasasServices()
        {
            BORangoTasa = new AtributosTasasBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }


        public Boolean CrearRangoTasas(RangoTasas pRangoTasas, AtributosLineaServicio pAtributos, ref string pError, Usuario vUsuario)
        {
            try
            {
                return BORangoTasa.CrearRangoTasas(pRangoTasas, pAtributos, ref pError, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AtributosTasasServices", "CrearRangoTasas", ex);
                return false;
            }
        }

        public Boolean ModificarRangoTasas(RangoTasas pRangoTasas, AtributosLineaServicio pAtributos, ref string pError, Usuario vUsuario)
        {
            try
            {
                return BORangoTasa.ModificarRangoTasas(pRangoTasas, pAtributos, ref pError, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AtributosTasasServices", "ModificarRangoTasas", ex);
                return false;
            }
        }


        public void EliminarRangoTopes(Int64 pId, Usuario vUsuario)
        {
            try
            {
                BORangoTasa.EliminarRangoTopes(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AtributosTasasServices", "EliminarRangoTopes", ex);
            }
        }

        public List<RangoTasas> ListarRangoTasas(RangoTasas pRangoTasas, string pFiltro, Usuario vUsuario)
        {
            try
            {
                return BORangoTasa.ListarRangoTasas(pRangoTasas, pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AperturaCDATService", "ListarRangoTasas", ex);
                return null;
            }
        }



        public Boolean ConsultarRangoTasasGeneral(ref RangoTasas eResult, ref AtributosLineaServicio pAtributos, ref string pError, Usuario vUsuario)
        {
            try
            {
                return BORangoTasa.ConsultarRangoTasasGeneral(ref eResult, ref pAtributos, ref pError, vUsuario);
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                return false;
            }
        }

        public Int64 ObtenerValorTopeMaximo(RangoTasasTope pRangoTasasTope, Usuario pUsuario)
        {
            try
            {
                return BORangoTasa.ObtenerValorTopeMaximo(pRangoTasasTope, pUsuario);
            }
            catch
            {
                return 1;
            }
        }

        public void EliminarSoloRangoTasasTope(long conseID, Usuario usuario)
        {
            try
            {
                BORangoTasa.EliminarSoloRangoTasasTope(conseID, usuario);
            }
            catch (Exception ex)
            { 
                BOExcepcion.Throw("AtributosTasasServices", "EliminarSoloRangoTasasTope", ex);
            }
        }

        public Int64 ValorPagoServicio(string tipoPago, string numeroservicios, string fecha, Usuario vUsuario)
        {
            try
            {
                return BORangoTasa.ValorPagoServicio(tipoPago, numeroservicios, fecha, vUsuario);
            }
            catch
            {
                return 0;
            }
        }


    }
}
