using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Tesoreria.Data;
using Xpinn.Tesoreria.Entities;
 
namespace Xpinn.Tesoreria.Business
{
 
        public class TrasladoPagaduriasBusiness : GlobalBusiness
        {
 
            private TrasladoPagaduriasData DATrasladoPagadurias;
 
            public TrasladoPagaduriasBusiness()
            {
                DATrasladoPagadurias = new TrasladoPagaduriasData();
            }
 

            public TrasladoPagadurias ModificarTrasladoPagadurias(TrasladoPagadurias pTrasladoPagadurias, Usuario pusuario)
            {
                try
                {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))

                {
                    foreach (Productos_Persona producto in pTrasladoPagadurias.Lista_Producto)
                    {
                        Productos_Persona _producto = new Productos_Persona();
                        _producto = DATrasladoPagadurias.ModificarTrasladoPagadurias(producto, pTrasladoPagadurias.cod_persona, pusuario);
                    }
                   

                    ts.Complete();

                }
 
                    return pTrasladoPagadurias;
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("TrasladoPagaduriasBusiness", "ModificarTrasladoPagadurias", ex);
                    return null;
                }
            }
 
  
            public TrasladoPagadurias ConsultarTrasladoPagadurias(Int64 pId, Usuario pusuario)
            {
                try
                {
                    TrasladoPagadurias TrasladoPagadurias = new TrasladoPagadurias();
                    TrasladoPagadurias = DATrasladoPagadurias.ConsultarTrasladoPagadurias(pId, pusuario);
                    return TrasladoPagadurias;
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("TrasladoPagaduriasBusiness", "ConsultarTrasladoPagadurias", ex);
                    return null;
                }
            }
 
        }
    }
