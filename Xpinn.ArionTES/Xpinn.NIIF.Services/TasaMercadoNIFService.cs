using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.NIIF.Business;
using Xpinn.NIIF.Entities;
using Xpinn.Util;
using System.Data;
using System.ServiceModel;

namespace Xpinn.NIIF.Services
{
    public class TasaMercadoNIFService
    {

        private TasaMercadoNIFBusiness BOTasaMercado;
        private ExcepcionBusiness BOExcepcion;

        public TasaMercadoNIFService()
        {
            BOTasaMercado = new TasaMercadoNIFBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoProgramaoriginal { get { return "210201"; } }



        public void EliminarTasaCondicionNIIF(TasaMercadoCondicionNIF pTasaCondicionNIIF, Usuario vUsuario)
        {
            try
            {
                BOTasaMercado.EliminarTasaCondicionNIIF(pTasaCondicionNIIF, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TasaMercadoNIIFService", "EliminarTasaCondicionNIIF", ex);
            }
        }


        public List<TasaMercadoCondicionNIF> FiltrarDatosTasaCondicion(int codigo, Usuario vUsuario)
        {
            try
            {
                return BOTasaMercado.FiltrarDatosTasaCondicion(codigo, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TasaMercadoNIIFService", "FiltrarDatosTasaCondicion", ex);
                return null;
            }
        }


        public int ObtenerCodigo(TasaMercadoNIF pTasaMercado, Usuario pUsuario)
        {
            try
            {
                return BOTasaMercado.ObtenerCodigo(pTasaMercado, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TasaMercadoNIIFService", "ObtenerCodigo", ex);
                return 0;
            }
        }


        public void EliminarTasaMercadoNIIF(TasaMercadoNIF pCarteraNIIF, Usuario vUsuario)
        {
            try
            {
                BOTasaMercado.EliminarTasaMercadoNIIF(pCarteraNIIF, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TasaMercadoNIIFService", "EliminarTasaMercadoNIIF", ex);
            }
        }



        public TasaMercadoNIF ModificarTasaMercadoNIIF(TasaMercadoNIF pTasaMercado, Usuario vUsuario)
        {
            try
            {
                return BOTasaMercado.ModificarTasaMercadoNIIF(pTasaMercado, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TasaMercadoNIIFService", "ModificarTasaMercadoNIIF", ex);
                return null;
            }
        }


        public TasaMercadoNIF CrearTasaMercadoNIIF(TasaMercadoNIF pTasaMercado, Usuario vUsuario)
        {
            try
            {
                return BOTasaMercado.CrearTasaMercadoNIIF(pTasaMercado, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TasaMercadoNIIFService", "CrearTasaMercadoNIIF", ex);
                return null;
            }
        }



        public List<TasaMercadoNIF> ListarTasaMercadoNIIF(TasaMercadoNIF pTasaMercado, Usuario vUsuario)
        {
            try
            {
                return BOTasaMercado.ListarTasaMercadoNIIF(pTasaMercado, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TasaMercadoNIIFService", "ListarTasaMercadoNIIF", ex);
                return null;
            }
        }



        public List<TasaMercadoNIF> DatosCondicionNIIF(TasaMercadoNIF pTasaMercadoNIIF, Usuario vUsuario)
        {
            try
            {
                return BOTasaMercado.DatosCondicionNIIF(pTasaMercadoNIIF, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TasaMercadoNIIFService", "DatosCondicionNIIF", ex);
                return null;
            }
        }

    }
}