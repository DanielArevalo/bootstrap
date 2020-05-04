using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Aportes.Data;
using Xpinn.Aportes.Entities;
 
namespace Xpinn.Aportes.Business
{
 
        public class MotivoExcepcionBusiness : GlobalBusiness
        {
 
            private MotivoExcepcionData DAMotivoExcepcion;
 
            public MotivoExcepcionBusiness()
            {
                DAMotivoExcepcion = new MotivoExcepcionData();
            }
 
            public MotivoExcepcion CrearMotivoExcepcion(MotivoExcepcion pMotivoExcepcion, Usuario pusuario)
            {
                try
                {
                    using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                    {
                        pMotivoExcepcion = DAMotivoExcepcion.CrearMotivoExcepcion(pMotivoExcepcion, pusuario);
 
                        ts.Complete();
 
                    }
 
                    return pMotivoExcepcion;
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("MotivoExcepcionBusiness", "CrearMotivoExcepcion", ex);
                    return null;
                }
            }
 
 
            public MotivoExcepcion ModificarMotivoExcepcion(MotivoExcepcion pMotivoExcepcion, Usuario pusuario)
            {
                try
                {
                    using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                    {
                        pMotivoExcepcion = DAMotivoExcepcion.ModificarMotivoExcepcion(pMotivoExcepcion, pusuario);
 
                        ts.Complete();
 
                    }
 
                    return pMotivoExcepcion;
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("MotivoExcepcionBusiness", "ModificarMotivoExcepcion", ex);
                    return null;
                }
            }
 
 
            public void EliminarMotivoExcepcion(Int64 pId, Usuario pusuario)
            {
                try
                {
                    using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                    {
                        DAMotivoExcepcion.EliminarMotivoExcepcion(pId, pusuario);
 
                        ts.Complete();
 
                    }
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("MotivoExcepcionBusiness", "EliminarMotivoExcepcion", ex);
                }
            }
 
 
            public MotivoExcepcion ConsultarMotivoExcepcion(Int64 pId, Usuario pusuario)
            {
                try
                {
                    MotivoExcepcion MotivoExcepcion = new MotivoExcepcion();
                    MotivoExcepcion = DAMotivoExcepcion.ConsultarMotivoExcepcion(pId, pusuario);
                    return MotivoExcepcion;
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("MotivoExcepcionBusiness", "ConsultarMotivoExcepcion", ex);
                    return null;
                }
            }
 
 
            public List<MotivoExcepcion> ListarMotivoExcepcion(MotivoExcepcion pMotivoExcepcion, Usuario pusuario)
            {
                try
                {
                   return DAMotivoExcepcion.ListarMotivoExcepcion(pMotivoExcepcion, pusuario);
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("MotivoExcepcionBusiness", "ListarMotivoExcepcion", ex);
                    return null;
                }
            }
 
 
        }
    }

