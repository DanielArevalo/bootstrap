using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Tesoreria.Data;
using Xpinn.Tesoreria.Entities;

namespace Xpinn.Tesoreria.Business
{

    public class ChequeraBusiness : GlobalBusiness
    {
        private ChequeraData DAChequera;

        
        public ChequeraBusiness()
        {
            DAChequera = new ChequeraData();
        }


        public Chequera CrearChequera(Chequera pChequera, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pChequera = DAChequera.CrearChequera(pChequera, vUsuario);

                    ts.Complete();
                }
                return pChequera;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ChequeraBusiness", "CrearChequera", ex);
                return null;
            }
        }


        public Chequera ModificarChequera(Chequera pChequera, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pChequera = DAChequera.ModificarChequera(pChequera, vUsuario);

                    ts.Complete();
                }
                return pChequera;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ChequeraBusiness", "ModificarChequera", ex);
                return null;
            }
        }

       
       
        public void EliminarChequera(Int32 pId, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAChequera.EliminarChequera(pId, vUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ChequeraBusiness", "EliminarChequera", ex);
            }
        }


        public List<Chequera> ListarChequera(Chequera pChequera, Usuario vUsuario,String filtro)
        {
            try
            {
                return DAChequera.ListarChequera(pChequera, vUsuario,filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ChequeraBusiness", "ListarChequera", ex);
                return null;
            }
        }


        public Chequera ConsultarChequera(Chequera pChequera, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pChequera = DAChequera.ConsultarChequera(pChequera, vUsuario);

                    ts.Complete();
                }

                return pChequera;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ChequeraBusiness", "ConsultarChequera", ex);
                return null;
            }
        }

        public Chequera ConsultarBanco(Chequera pChequera, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pChequera = DAChequera.ConsultarBanco(pChequera, vUsuario);

                    ts.Complete();
                }

                return pChequera;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ChequeraBusiness", "ConsultarBanco", ex);
                return null;
            }
        }


        public List<Chequera> ListarCuentasBancarias(Chequera pChequera, Usuario vUsuario)
        {

            try
            {
                return DAChequera.ListarCuentasBancarias(pChequera, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ChequeraBusiness", "ListarCuentasBancarias", ex);
                return null;
            }
        }

        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            try
            {
                return DAChequera.ObtenerSiguienteCodigo(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ChequeraBusiness", "ObtenerSiguienteCodigo", ex);
                return 0;
            }
        }
        public List<Chequera> ConsultarChequeraReporte(Chequera pChequera, Usuario vUsuario)
        {
            try
            {
                return DAChequera.ConsultarChequeraReporte(pChequera, vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ChequeraBusiness", "ConsultarBanco", ex);
                return null;
            }
        }

    }
}