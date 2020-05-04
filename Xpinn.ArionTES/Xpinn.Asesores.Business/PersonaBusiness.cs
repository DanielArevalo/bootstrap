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
    public class PersonaBusiness : GlobalData
    {
        private PersonaData dataPersona;

        public PersonaBusiness()
        {
            dataPersona = new PersonaData();
        }

        public Persona Consultar(Int64 pIdEntityPersona, Usuario pUsuario)
        {
            try
            {
                return dataPersona.Consultar(pIdEntityPersona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaData", "Consultar", ex);
                return null;
            }

        }

        public List<Persona> Listar(Persona pEntityPersona, Usuario pUsuario)
        {
            try
            {
                return dataPersona.Listar(pEntityPersona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaData", "Listar", ex);
                return null;
            }

        }


        public List<DetalleProducto> ListarAcodeudados(Persona pPersona, Usuario pUsuario)
        {
            return null;
            /* try
             {
                 var list = dataDetalleProducto.Listar(pEntityProducto, pUsuario);

                 foreach (var nodeDetalleProducto in list)
                 {
                     nodeDetalleProducto.Producto.Codeudores = dataCodeudor.Listar(nodeDetalleProducto, pUsuario);
                     nodeDetalleProducto.Documentos = dataDocCred.Listar(nodeDetalleProducto.NumeroRadicacion, pUsuario);
                     nodeDetalleProducto.Garantias = dataGtia.Listar(nodeDetalleProducto.NumeroRadicacion, pUsuario);
                     nodeDetalleProducto.Producto.Persona = dataPersona.Consultar(nodeDetalleProducto.Producto.Persona.IdPersona, pUsuario);
                     nodeDetalleProducto.MovimientosProducto = dataMovProd.Listar(nodeDetalleProducto.NumeroRadicacion, pUsuario);
                     nodeDetalleProducto.DetallePagos = dataDetPago.Listar(nodeDetalleProducto.NumeroRadicacion, pUsuario);

                     foreach (var nodeCodeudor in nodeDetalleProducto.Producto.Codeudores)
                     {
                         nodeCodeudor.Persona = dataPersona.Consultar(nodeCodeudor.Persona.IdPersona, pUsuario);
                     }
                 }
                 return list;
             }
             catch (Exception ex)
             {
                 BOExcepcion.Throw("ProductosBusiness", "ListarProductos", ex);
                 return null;
             }*/
        }
        public Persona ConsultaryaExistente(Int64 pIdEntityPersona, Usuario pUsuario)
        {
            try
            {
                return dataPersona.ConsultaryaExistente(pIdEntityPersona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaData", "Consultar", ex);
                return null;
            }

        }
    }
}
