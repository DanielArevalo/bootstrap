using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Integracion.Entities;
using Xpinn.Integracion.Data;
using Xpinn.Imagenes.Data;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace Xpinn.Integracion.Business
{
    public class PagoInternoBusiness : GlobalData
    {
        private PagoInternoData BOPagoInternoData;
        private NotificacionBusiness BONoti;

        public PagoInternoBusiness()
        {
            BOPagoInternoData = new PagoInternoData();
            BONoti = new NotificacionBusiness(); 
        }
        
        public List<ProductoPorPagar> listarProductosPorPagar(long cod_persona, string filtro, Usuario vUsuario)
        {
            try
            {
                return BOPagoInternoData.listarProductosPorPagar(cod_persona, filtro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BOPagoInternoBusines", "listarProductosPorPagar", ex);
                return null;
            }
        }

        public List<ProductoOrigenPago> listarProductoOrigenPago(long cod_persona, string filtro, Usuario vUsuario)
        {
            try
            {
                return BOPagoInternoData.listarProductosOrigenPago(cod_persona, filtro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BOPagoInternoBusines", "listarProductosOrigenPago", ex);
                return null;
            }
        }

        public Int32 procesarPagoInterno(PagoInterno pago, Usuario vUsuario)
        {
            int id_pago = 0;
            try
            {
                
                id_pago = BOPagoInternoData.procesarPagoInterno(pago, vUsuario);
                if(id_pago > 0)
                {
                    //Enviar notificacion de confirmacion
                    Notificacion not = new Notificacion()
                    {
                        cod_email = 22,
                        cod_mensaje = 23,
                        cod_persona = pago.cod_persona,
                        mensaje_parametros = "nombreProducto;" + pago.nom_linea_destino,
                        correo_parametros = "nombreProducto;" + pago.nom_linea_destino + "|NombreCompletoPersona;" + pago.nombres + "|NumeroRadicacion;" + pago.destino_id_producto,
                        enviar_mensaje = true,
                        enviar_email = true
                    };
                    BONoti.enviarNotificaciones(not, vUsuario);
                    return id_pago;
                }

                return id_pago;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BOPagoInternoBusines", "procesarPagoInterno", ex);
                return id_pago;
            }
        }

        public List<PagoInterno> listarPagosInternos(string filtro, Usuario pUsuario)
        {
            try
            {
                return BOPagoInternoData.listarPagosInternos(filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BOPagoInternoBusines", "listarPagosInternos", ex);
                return null;
            }
        }
    }
}


