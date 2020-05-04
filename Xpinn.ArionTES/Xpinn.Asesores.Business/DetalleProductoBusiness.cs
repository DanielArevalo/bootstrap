using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Asesores.Entities;
using Xpinn.Asesores.Entities.Common;
using Xpinn.Asesores.Data;
using Xpinn.Util;
using System.IO;

namespace Xpinn.Asesores.Business
{
    public class DetalleProductoBusiness : GlobalData
    {
        private DetalleProductoData dataDetalleProducto;
        private CodeudorData dataCodeudor;
        private PersonaData dataPersona;
        private DocumentoCreditoData dataDocCred;
        private GarantiaData dataGtia;
        private MovimientoProductoData dataMovProd;
        private DetallePagoData dataDetPago;
        private ConsultarAvanceData dataConsAvance;

        public DetalleProductoBusiness()
        {
            dataDetalleProducto = new DetalleProductoData();
            dataCodeudor = new CodeudorData();
            dataPersona = new PersonaData();
            dataDocCred = new DocumentoCreditoData();
            dataGtia = new GarantiaData();
            dataMovProd = new MovimientoProductoData();
            dataDetPago = new DetallePagoData();
            dataConsAvance = new ConsultarAvanceData();
        }

        public List<DetalleProducto> ListarDetalleProductos(Producto pEntityProducto, Usuario pUsuario, int detalle, int calcularTotal = 0)
        {
            try
            {
                var list = dataDetalleProducto.Listar(pEntityProducto, pUsuario);
               
                foreach (var nodeDetalleProducto in list)
                {
                    if (pEntityProducto.noconsultaTodo == 0)
                        nodeDetalleProducto.Producto.Codeudores = dataCodeudor.Listar(nodeDetalleProducto, pUsuario);
                    if (pEntityProducto.noconsultaTodo == 0)
                        nodeDetalleProducto.Documentos = dataDocCred.Listar(nodeDetalleProducto.NumeroRadicacion, pUsuario);
                    if (pEntityProducto.noconsultaTodo == 0)
                        nodeDetalleProducto.Garantias = dataGtia.Listar(nodeDetalleProducto.NumeroRadicacion, pUsuario);
                    nodeDetalleProducto.Producto.Persona = dataPersona.Consultar(Convert.ToInt64(nodeDetalleProducto.Producto.Persona.IdPersona), pUsuario);
                    nodeDetalleProducto.MovimientosProducto = dataMovProd.Listar(nodeDetalleProducto.NumeroRadicacion, pUsuario, detalle);
  
                    if (pEntityProducto.noconsultaTodo == 0)
                    { 
                        nodeDetalleProducto.DetallePagos = dataDetPago.Listar(pEntityProducto.FechaPago, nodeDetalleProducto.NumeroRadicacion, pUsuario, calcularTotal);
                        // Se puso para COOPCHIPAQUE para que si no genera pagos vuelva a ejecutar el proceso. FerOrt. 12-Oct-2017.
                        if (nodeDetalleProducto.DetallePagos == null || nodeDetalleProducto.DetallePagos.Count <= 0)
                            nodeDetalleProducto.DetallePagos = dataDetPago.Listar(pEntityProducto.FechaPago, nodeDetalleProducto.NumeroRadicacion, pUsuario, calcularTotal);
                    }

                    if (pEntityProducto.noconsultaTodo == 0)
                        foreach (var nodeCodeudor in nodeDetalleProducto.Producto.Codeudores)
                        {
                            nodeCodeudor.Persona = dataPersona.Consultar(Convert.ToInt64(nodeCodeudor.Persona.IdPersona), pUsuario);
                        }

                    string a = nodeDetalleProducto.EstadoCredito;
                }
              
                return list;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProductosBusiness", "ListarProductos", ex);
                return null;
            }
        }

        public List<ConsultaAvance> ListarAvances(Int64 pNumeroRadicacion, Usuario pUsuario)
        {
            try
            {
                return dataConsAvance.Listar(pNumeroRadicacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProductosBusiness", "ListarMovCreditos", ex);
                return null;
            }
        }

        public List<ConsultaAvance> ListarAvances(Int64 pNumeroRadicacion, string pFiltro, Usuario pUsuario)
        {
            try
            {
                return dataConsAvance.Listar(pNumeroRadicacion, pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProductosBusiness", "ListarMovCreditos", ex);
                return null;
            }
        }

        public ConsultaAvance ModificarAvances(ConsultaAvance avance, Usuario pUsuario)
        {
            try
            {
                return dataConsAvance.ModificarAvances(avance, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProductosBusiness", "ListarMovCreditos", ex);
                return null;
            }
        }

        public List<MovimientoProducto> ListarMovCreditos(Int64 pNumeroRadicacion, Usuario pUsuario, int detalle)
        {
            try
            {
                return dataMovProd.Listar(pNumeroRadicacion, pUsuario, detalle);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProductosBusiness", "ListarMovCreditos", ex);
                return null;
            }
        }

          public List<DetalleProducto> ListarValoresAdeudados(Int64 pRadicado, Usuario pUsuario)
        {
            try
            {
                var list = dataDetalleProducto.ListarValoresAdeudados(pRadicado, pUsuario);
               
                foreach (var nodeDetalleProducto in list)
                {                
                    nodeDetalleProducto.DetallePagos = dataDetPago.ListarValoresAdeudados(nodeDetalleProducto.NumeroRadicacion,pUsuario);                 
                }
              
                return list;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProductosBusiness", "ListarValoresAdeudados", ex);
                return null;
            }
        }

        public List<DetalleProducto> ListarValoresAdeudados(Int64 pRadicado,DateTime FechaPago, Usuario pUsuario)
        {
            try
            {
                var list = dataDetalleProducto.ListarValoresAdeudados(pRadicado,pUsuario);

                foreach (var nodeDetalleProducto in list)
                {
                    nodeDetalleProducto.DetallePagos = dataDetPago.Listar(FechaPago, nodeDetalleProducto.NumeroRadicacion, pUsuario, 1);
                }

                return list;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProductosBusiness", "ListarValoresAdeudados", ex);
                return null;
            }
        }



    }
          
}
