using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Data;
using Xpinn.FabricaCreditos.Entities;
 
namespace Xpinn.FabricaCreditos.Business
{
 
        public class PagoOrdenesBusiness : GlobalBusiness
        {
 
            private PagoOrdenesFabricaCreditosData DAPagoOrdenes;
 
            public PagoOrdenesBusiness()
            {
                DAPagoOrdenes = new PagoOrdenesFabricaCreditosData();
            }

            public bool CrearPagoOrdenes(List<PagoOrdenes> plstOrdenes, Xpinn.FabricaCreditos.Entities.Giro pGiro, ref string pError, Usuario pusuario)
            {
                try
                {
                    using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                    {

                        //ACTUALIZAR LAS ORDENES DE PAGO
                        foreach (PagoOrdenes rFila in plstOrdenes)
                        {
                            PagoOrdenes entidadRetorna = new PagoOrdenes();
                            entidadRetorna = DAPagoOrdenes.ModificarPagoOrdenes(rFila, ref pError, pusuario);
                        }
                       
                        //GRABAR NUEVO GIRO AL PROVEEDOR
                        Xpinn.FabricaCreditos.Data.AvanceData DAGiro = new Xpinn.FabricaCreditos.Data.AvanceData();                       
                        pGiro.cod_ope = 0;
                        pGiro = DAGiro.CrearGiro(pGiro, pusuario, 1);

                        ts.Complete();                         
                    }
                    return true;
                }
                catch 
                {                    
                    return false;
                }
            }
 
 
            public PagoOrdenes ModificarPagoOrdenes(PagoOrdenes pPagoOrdenes, Usuario pusuario)
            {
                String Error = "";
                try
                {
                    using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                    {
                        pPagoOrdenes = DAPagoOrdenes.ModificarPagoOrdenes(pPagoOrdenes, ref Error, pusuario);
 
                        ts.Complete();
 
                    }
 
                    return pPagoOrdenes;
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("PagoOrdenesBusiness", "ModificarPagoOrdenes", ex);
                    return null;
                }
            }
 
 
            public List<PagoOrdenes> ConsultarPagoOrdenes( string pFiltro,Usuario pusuario)
            {
                try
                {
                    return DAPagoOrdenes.ConsultarPagoOrdenes(pFiltro, pusuario);
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("PagoOrdenesBusiness", "ListarPagoOrdenes", ex);
                    return null;
                }
            }

            public string TipoConexion(Usuario vUsuario)
            {
                return DAPagoOrdenes.TipoConexion(vUsuario);
            }

 
        }
    }
