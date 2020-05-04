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
    public class TransicionSegmentoNIFService
    {

        private TransicionSegmentoNIFBusiness DABalance;
        private ExcepcionBusiness BOExcepcion;

        public TransicionSegmentoNIFService()
        {
            DABalance = new TransicionSegmentoNIFBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }


        public string CodigoProgramaoriginal { get { return "210204"; } }





        public TransicionSegmentoNIF CrearTransicionSegmento(TransicionSegmentoNIF pTransi, Usuario vUsuario)
        {
            try
            {
                return DABalance.CrearTransicionSegmento(pTransi, vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransicionSegmentoNIFService", "CrearTransicionSegmento", ex);
                return null;
            }
        }



        public TransicionSegmentoNIF ModificarTransicionSegmento(TransicionSegmentoNIF pTransi, Usuario vUsuario)
        {
            try
            {
                return DABalance.ModificarTransicionSegmento(pTransi, vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransicionSegmentoNIFService", "ModificarTransicionSegmento", ex);
                return null;
            }
        }


        public void EliminarTransicionSegmentoNIF(Int32 pId, Usuario vUsuario)
        {
            try
            {
                DABalance.EliminarTransicionSegmentoNIF(pId, vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransicionSegmentoNIFService", "EliminarTransicionSegmentoNIF", ex);
            }
        }


        public TransicionSegmentoNIF ConsultarTransicionSegmentoNIF(TransicionSegmentoNIF pEntidad, Usuario vUsuario)
        {
            try
            {
                return DABalance.ConsultarTransicionSegmentoNIF(pEntidad, vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransicionSegmentoNIFService", "ConsultarTransicionSegmentoNIF", ex);
                return null;
            }
        }



        public List<TransicionSegmentoNIF> ListarTransicionSegmento(TransicionSegmentoNIF pTransi, Usuario vUsuario)
        {
            try
            {
                return DABalance.ListarTransicionSegmento(pTransi, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransicionSegmentoNIFService", "ListarTransicionSegmento", ex);
                return null;
            }
        }



        public void EliminarDetalleTransicionNIF(Int32 pId, Usuario vUsuario)
        {
            try
            {
                DABalance.EliminarDetalleTransicionNIF(pId, vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransicionSegmentoNIFService", "EliminarDetalleTransicionNIF", ex);
            }
        }



        public List<TransicionDetalle> ListarDetalleTransicion(Int32 pId, Usuario vUsuario)
        {
            try
            {
                return DABalance.ListarDetalleTransicion(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransicionSegmentoNIFService", "ListarDetalleTransicion", ex);
                return null;
            }
        }

        public List<TransicionDetalle> ListarDetalleSegmento(int codigoSegmento, Usuario usuario)
        {
            try
            {
                return DABalance.ListarDetalleSegmento(codigoSegmento, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransicionSegmentoNIFService", "ListarDetalleSegmento", ex);
                return null;
            }
        }
    }     

}
