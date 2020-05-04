using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Nomina.Data;
using Xpinn.Nomina.Entities;
 
namespace Xpinn.Nomina.Business
{

    public class ContratacionBusiness : GlobalBusiness
    {

        private ContratacionNominaData DAContratacion;

        public ContratacionBusiness()
        {
            DAContratacion = new ContratacionNominaData();
        }

        public TipoContrato CrearContratacion(TipoContrato pContratacion, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pContratacion = DAContratacion.CrearContratacion(pContratacion, pusuario);

                    ts.Complete();

                }

                return pContratacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ContratacionBusiness", "CrearContratacion", ex);
                return null;
            }
        }


        public TipoContrato CrearTipoRetirocontrato(TipoContrato pContratacion, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pContratacion = DAContratacion.CrearTipoRetirocontrato(pContratacion, pusuario);

                    ts.Complete();

                }

                return pContratacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ContratacionBusiness", "CrearTipoRetirocontrato", ex);
                return null;
            }
        }

        public TipoContrato ModificarTipoRetirocontrato(TipoContrato pContratacion, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pContratacion = DAContratacion.ModificarTipoRetirocontrato(pContratacion, pusuario);

                    ts.Complete();

                }

                return pContratacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ContratacionBusiness", "ModificarTipoRetirocontrato", ex);
                return null;
            }
        }

        public TipoContrato ModificarContratacion(TipoContrato pContratacion, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pContratacion = DAContratacion.ModificarContratacion(pContratacion, pusuario);

                    ts.Complete();

                }

                return pContratacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ContratacionBusiness", "ModificarContratacion", ex);
                return null;
            }
        }


        public void EliminarContratacion(Int64 pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAContratacion.EliminarContratacion(pId, pusuario);

                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ContratacionBusiness", "EliminarContratacion", ex);
            }
        }


        public TipoContrato ConsultarContratacion(Int64 pId, Usuario pusuario)
        {
            try
            {
                TipoContrato Contratacion = new TipoContrato();
                Contratacion = DAContratacion.ConsultarContratacion(pId, pusuario);
                return Contratacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ContratacionBusiness", "ConsultarContratacion", ex);
                return null;
            }
        }

        public TipoContrato ConsultarTipoRetiroContrato(Int64 pId, Usuario pusuario)
        {
            try
            {
                TipoContrato Contratacion = new TipoContrato();
                Contratacion = DAContratacion.ConsultarTipoRetiroContrato(pId, pusuario);
                return Contratacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ContratacionBusiness", "ConsultarTipoRetiroContrato", ex);
                return null;
            }
        }

        public List<TipoContrato> ListarTipoContratos(Usuario usuario)
        {
            try
            {
                return DAContratacion.ListarTipoContratos(usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ContratacionBusiness", "ListarTipoContratos", ex);
                return null;
            }
        }

        public List<TipoContrato> ListarContratacion(string pid, Usuario pusuario)
        {
            try
            {
                return DAContratacion.ListarContratacion(pid, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ContratacionBusiness", "ListarContratacion", ex);
                return null;
            }
        }

        public List<TipoContrato> ListarTipoRetiroContrato(string pid, Usuario pusuario)
        {
            try
            {
                return DAContratacion.ListarTipoRetiroContrato(pid, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ContratacionBusiness", "ListarTipoRetiroContrato", ex);
                return null;
            }
        }


    }
}
