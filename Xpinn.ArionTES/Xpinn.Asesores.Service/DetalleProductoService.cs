using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Asesores.Business;
using Xpinn.Asesores.Entities;


namespace Xpinn.Asesores.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class DetalleProductoService
    {

        private DetalleProductoBusiness BODetalleProducto;
        private ExcepcionBusiness BOExcepcion;

        public DetalleProductoService()
        {
            BODetalleProducto = new DetalleProductoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "110116"; } }

        public List<DetalleProducto> ListarDetalleProductos(Producto pEntityProducto, Usuario pUsuario,int detalle, int calcularTotal = 0)
        {
            try
            {
                return BODetalleProducto.ListarDetalleProductos(pEntityProducto, pUsuario,detalle, calcularTotal);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DetalleProductoService", "ListarDetalleProductos", ex);
                return null;
            }
        }

        public List<MovimientoProducto> ListarMovCreditos(Int64 pNumeroRadicacion, Usuario pUsuario, int detalle)
        {
            try
            {
                return BODetalleProducto.ListarMovCreditos(pNumeroRadicacion, pUsuario, detalle);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DetalleProductoService", "ListarMovCreditos", ex);
                return null;
            }
        }

        public List<ConsultaAvance> ListarAvances(Int64 pNumeroRadicacion, Usuario pUsuario)
        {
            try
            {
                return BODetalleProducto.ListarAvances(pNumeroRadicacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DetalleProductoService", "ListarMovCreditos", ex);
                return null;
            }
        }

        public List<ConsultaAvance> ListarAvances(Int64 pNumeroRadicacion, string pFiltro, Usuario pUsuario)
        {
            try
            {
                return BODetalleProducto.ListarAvances(pNumeroRadicacion, pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DetalleProductoService", "ListarMovCreditos", ex);
                return null;
            }
        }

        public ConsultaAvance ModificarAvances(ConsultaAvance avance, Usuario pUsuario)
        {
            try
            {
                return BODetalleProducto.ModificarAvances(avance,pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DetalleProductoService", "ListarMovCreditos", ex);
                return null;
            }
        }


        public List<DetalleProducto> ListarValoresAdeudados(Int64 pRadicado, Usuario pUsuario)
        {
            try
            {
                return BODetalleProducto.ListarValoresAdeudados(pRadicado, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DetalleProductoService", "ListarDetalleProductos", ex);
                return null;
            }
        }


        public List<DetalleProducto> ListarValoresAdeudados(Int64 pRadicado,DateTime Fecha_Pago, Usuario pUsuario)
        {
            try
            {
                return BODetalleProducto.ListarValoresAdeudados(pRadicado, Fecha_Pago, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DetalleProductoService", "ListarDetalleProductos", ex);
                return null;
            }
        }


    }
}
