using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Ahorros.Data;
using Xpinn.Ahorros.Entities;
 
namespace Xpinn.Ahorros.Business
{
 
        public class ParametroGMFBusiness : GlobalBusiness
        {
 
            private ParametroGMFData DAParametroGMF;
 
            public ParametroGMFBusiness()
            {
                DAParametroGMF = new ParametroGMFData();
            }
 
            public ParametroGMF CrearParametroGMF(ParametroGMF pParametroGMF, Usuario pusuario)
            {
                try
                {
                    using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                    {
                        pParametroGMF = DAParametroGMF.CrearGMF(pParametroGMF, pusuario);
 
                        ts.Complete();
 
                    }
 
                    return pParametroGMF;
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("ParametroGMFBusiness", "CrearParametroGMF", ex);
                    return null;
                }
            }
 
 
            public ParametroGMF ModificarParametroGMF(Int64 idobjeto,ParametroGMF pParametroGMF, Usuario pusuario)
            {
                try
                {
                    using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                    {
                        pParametroGMF = DAParametroGMF.ModificarGMF(idobjeto,pParametroGMF, pusuario);
 
                        ts.Complete();
 
                    }
 
                    return pParametroGMF;
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("ParametroGMFBusiness", "ModificarParametroGMF", ex);
                    return null;
                }
            }
 
 
            public void EliminarParametroGMF(Int64 pId, Usuario pusuario)
            {
                try
                {
                    using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                    {
                        DAParametroGMF.EliminarGMF(pId, pusuario);
 
                        ts.Complete();
 
                    }
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("ParametroGMFBusiness", "EliminarParametroGMF", ex);
                }
            }
 
 
            public ParametroGMF ConsultarParametroGMF(Int64 pId, Usuario pusuario)
            {
                try
                {
                    ParametroGMF ParametroGMF = new ParametroGMF();
                    ParametroGMF = DAParametroGMF.ConsultarGMF(pId, pusuario);
                    return ParametroGMF;
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("ParametroGMFBusiness", "ConsultarParametroGMF", ex);
                    return null;
                }
            }
 
 
            public List<ParametroGMF> ListarParametroGMF(String filtro,ParametroGMF pParametroGMF, Usuario pusuario)
            {
                try
                {
                    return DAParametroGMF.ListarGMF(filtro,pParametroGMF, pusuario);
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("ParametroGMFBusiness", "ListarParametroGMF", ex);
                    return null;
                }
            }

            public List<ParametroGMF> combooperacion(Usuario pusuario)
            {
                try
                {
                    return DAParametroGMF.combooperacion(pusuario);
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("ParametroGMFBusiness", "ListarParametroGMF", ex);
                    return null;
                }
            }


            public int ModificarEstadoTranGmf(ParametroGMF Entidad, Usuario pUsuario, DateTime fecha, DateTime fechafinal)
            {
                int respuesta = 0;
                try
                {
                    using (TransactionScope tr = new TransactionScope())
                    {
                        respuesta = DAParametroGMF.ModificarEstadoTranGmf(Entidad, pUsuario, fecha,fechafinal);
                        tr.Complete();
                    }
                    return respuesta;
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("ParametroGMFBusiness", "ModificarEstadoTranGmf", ex);
                    return respuesta;
                }
            }

        }
    }
