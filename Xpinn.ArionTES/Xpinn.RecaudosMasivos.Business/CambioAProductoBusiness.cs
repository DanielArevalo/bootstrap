using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Tesoreria.Data;
using Xpinn.Tesoreria.Entities;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Aportes.Data;
using Xpinn.Aportes.Entities;
using Xpinn.Ahorros.Data;
using Xpinn.Ahorros.Entities;
using Xpinn.Programado.Entities;
using Xpinn.Programado.Data;

namespace Xpinn.Tesoreria.Business
{
    /// <summary>
    /// Objeto de negocio para TipoListaRecaudo
    /// </summary>
    public class CambioAProductoBusiness : GlobalBusiness
    {
        private CambioAProductoData DACambio;
        private TipoListaRecaudoDetalleData DACambioDetalle;

        /// <summary>
        /// Constructor del objeto de negocio para TipoListaRecaudo
        /// </summary>
        public CambioAProductoBusiness()
        {
            DACambio = new CambioAProductoData();
            DACambioDetalle = new TipoListaRecaudoDetalleData();
        }

        public CambioAProducto ModificarProducto(CambioAProducto pEntidad, string tabla, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {                     

                    if (tabla == "APORTE")
                    {
                        AporteData objAporteData = new AporteData();
                        Aporte objAporte = new Aporte();
                        objAporte.numero_aporte = pEntidad.numero_producto;
                        objAporte.fecha_empieza_cambio = pEntidad.fecha_empieza_cambio;
                        objAporte.nuevo_valor_cuota = pEntidad.valor_cuota.Value;
                        objAporte.cuota = pEntidad.valor_cuota_anterior.Value;
                        objAporte.estado_modificacion = "1";
                        objAporte.cod_persona = pEntidad.cod_persona;
                        objAporteData.CrearNovedadCambio(objAporte, vUsuario);
                    }
                    else if (tabla == "AHORRO_VISTA")
                    {
                        AhorroVistaData objAhorroVistaData = new AhorroVistaData();
                        AhorroVista objAhorroVista = new AhorroVista();
                        objAhorroVista.numero_cuenta = pEntidad.numero_producto.ToString();
                        objAhorroVista.fecha = pEntidad.fecha_empieza_cambio.Value;
                        objAhorroVista.valor_cuota = pEntidad.valor_cuota;
                        objAhorroVista.valor_cuota_anterior = pEntidad.valor_cuota_anterior;
                        objAhorroVistaData.ModificarCuota(objAhorroVista, vUsuario);
                    }
                    else if (tabla == "AHORRO_PROGRAMADO")
                    {
                        CuentasProgramadoData objAhorroProgramadoData = new CuentasProgramadoData();
                        CuentasProgramado objAhorroProgramado = new CuentasProgramado();
                        objAhorroProgramado.numero_cuenta = pEntidad.numero_producto.ToString();
                        objAhorroProgramado.estado = 1;
                        objAhorroProgramado.fecha_empieza_cambio = pEntidad.fecha_empieza_cambio.Value;
                        objAhorroProgramado.valor_cuota = pEntidad.valor_cuota.Value;
                        objAhorroProgramado.valor_cuota_anterior = pEntidad.valor_cuota_anterior.Value;
                        objAhorroProgramadoData.ModificarCuota(objAhorroProgramado, vUsuario);
                    }

                    pEntidad = DACambio.ModificarProducto(pEntidad, tabla, vUsuario);
                    

                    // Si es credito es que tengo detalles y modifico y creo segun sea necesario
                    if (pEntidad.lstDetalle != null && pEntidad.lstDetalle.Count > 0)
                    {
                        FabricaCreditos.Data.CreditoData CambioData = new Xpinn.FabricaCreditos.Data.CreditoData();
                        foreach (Xpinn.FabricaCreditos.Entities.CreditoEmpresaRecaudo nCambio in pEntidad.lstDetalle)
                        {
                            nCambio.numero_radicacion = pEntidad.numero_producto;
                            Xpinn.FabricaCreditos.Entities.CreditoEmpresaRecaudo nCambioProduct = new Xpinn.FabricaCreditos.Entities.CreditoEmpresaRecaudo();
                            if (nCambio.idcrerecaudo != 0)
                                nCambioProduct = CambioData.CrearModEmpresa_Recaudo(nCambio, vUsuario, 2); //MODIFICAR
                            else
                                nCambioProduct = CambioData.CrearModEmpresa_Recaudo(nCambio, vUsuario, 1); //CREAR
                        }
                    }

                    ts.Complete();
                }

                return pEntidad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CambioAProductoBusiness", "ModificarProducto", ex);
                return null;
            }
        }


        public List<PersonaEmpresaRecaudo> ListarPersonaEmpresaRecaudo(PersonaEmpresaRecaudo pPersonaEmpresaRecaudo, Usuario vUsuario)
        {
            try
            {
                return DACambio.ListarPersonaEmpresaRecaudo(pPersonaEmpresaRecaudo, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CambioAProductoBusiness", "ListarPersonaEmpresaRecaudo", ex);
                return null;
            }
        }

        public List<CambioAProducto> ListarCreditoEmpresa_Recaudo(Int64 pNumRadicacion, Usuario vUsuario)
        {
            try
            {
                return DACambio.ListarCreditoEmpresa_Recaudo(pNumRadicacion, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CambioAProductoBusiness", "ListarCreditoEmpresa_Recaudo", ex);
                return null;
            }
        }

        public CambioAProducto ConsultarFormaDePagoProducto(String pId, String tabla, Usuario vUsuario)
        {
            try
            {
                return DACambio.ConsultarFormaDePagoProducto(pId, tabla, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CambioAProductoBusiness", "ConsultarFormaDePagoProducto", ex);
                return null;
            }
        }

    }
}