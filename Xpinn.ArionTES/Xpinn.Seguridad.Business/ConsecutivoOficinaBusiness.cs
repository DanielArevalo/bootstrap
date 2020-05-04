using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Seguridad.Data;
using Xpinn.Seguridad.Entities;
 
namespace Xpinn.Seguridad.Business
{
 
        public class ConsecutivoOficinasBusiness : GlobalBusiness
        {
 
            private ConsecutivoOficinasData DAOficina;

            public ConsecutivoOficinasBusiness()
            {
                DAOficina = new ConsecutivoOficinasData();
            }
 
            public ConsecutivoOficinas CrearConsecutivoOficinas(ConsecutivoOficinas pOficina, Usuario pusuario)
            {
                try
                {
                    using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                    {
                        pOficina = DAOficina.CrearConsecutivoOficinas(pOficina, pusuario);
 
                        ts.Complete();
 
                    }
 
                    return pOficina;
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("ConsecutivoOficinasBusiness", "CrearConsecutivoOficinas", ex);
                    return null;
                }
            }


            public ConsecutivoOficinas ModificarConsecutivoOficinas(ConsecutivoOficinas pOficina, Usuario pusuario)
            {
                try
                {
                    using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                    {
                        pOficina = DAOficina.ModificarConsecutivoOficinas(pOficina, pusuario);
 
                        ts.Complete();
 
                    }
 
                    return pOficina;
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("ConsecutivoOficinasBusiness", "ModificarConsecutivoOficinas", ex);
                    return null;
                }
            }


            public void EliminarConsecutivoOficinas(Int64 pId, Usuario pusuario)
            {
                try
                {
                    using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                    {
                        DAOficina.EliminarConsecutivoOficinas(pId, pusuario);
 
                        ts.Complete();
 
                    }
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("ConsecutivoOficinasBusiness", "EliminarConsecutivoOficinas", ex);
                }
            }


            public ConsecutivoOficinas ConsultarConsecutivoOficinas(Int64 pId, Usuario pusuario)
            {
                try
                {
                    ConsecutivoOficinas ConsecutivoOficinas = new ConsecutivoOficinas();
                    ConsecutivoOficinas = DAOficina.ConsultarConsecutivoOficinas(pId, pusuario);
                    return ConsecutivoOficinas;
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("ConsecutivoOficinasBusiness", "ConsultarConsecutivoOficinas", ex);
                    return null;
                }
            }
            public ConsecutivoOficinas ConsultarConsOfiXOfyTabla(String pIdTabla, Int64 pIdOficina, Int64 prangoin, Int64 prangfin, Usuario vUsuario)
            {
                try
                {
                    ConsecutivoOficinas ConsecutivoOficinas = new ConsecutivoOficinas();
                    ConsecutivoOficinas = DAOficina.ConsultarConsOfiXOfyTabla(pIdTabla, pIdOficina, prangoin, prangfin, vUsuario);
                    return ConsecutivoOficinas;
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("ConsecutivoOficinasBusiness", "ConsultarConsOfiXOfyTabla", ex);
                    return null;
                }
            }


            public List<ConsecutivoOficinas> ListarConsecutivoOficinas(String filtro, Usuario pusuario)
            {
                try
                {
                    return DAOficina.ListarConsecutivoOficinas(filtro, pusuario);
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("ConsecutivoOficinasBusiness", "ListarConsecutivoOficinas", ex);
                    return null;
                }
            }
 
 
        }
    }
