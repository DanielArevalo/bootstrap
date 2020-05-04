using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Cartera.Data;
using Xpinn.Cartera.Entities;
using System.Web;


namespace Xpinn.Cartera.Business
{
    public class RefinanciacionBusiness : GlobalBusiness
    {
        private RefinanciacionData DARefinanciacion;

        /// <summary>
        /// Constructor del objeto de negocio para cierre histórico
        /// </summary>
        public RefinanciacionBusiness()
        {
            DARefinanciacion = new RefinanciacionData();
        }

        /// <summary>
        /// Método para listar los créditos a refinanciar
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public List<Xpinn.FabricaCreditos.Entities.Credito> ListarCredito(DateTime? pFecha, String filtro, Usuario pUsuario)
        {
            try
            {
                return DARefinanciacion.ListarCredito(pFecha, filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RefinanciacionBusiness", "ListarCredito", ex);
                return null;
            }
        }

        public decimal TotalaPagarCredito(Int64 pNumeroRadicacion, DateTime pFecha, Usuario pusuario)
        {
            try
            {
                return DARefinanciacion.TotalaPagarCredito(pNumeroRadicacion, pFecha, pusuario);
            }
            catch (Exception ex)
            { return pNumeroRadicacion; }

        }
        public Refinanciacion CrearRefinanciacion(Refinanciacion pRefinanciacion, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    foreach (AtributosRefinanciar eFila in pRefinanciacion.lstAtributosRefinanciar)
                    {
                        DARefinanciacion.CrearAtributosRefinanciar(eFila, pUsuario);
                    }
                    pRefinanciacion = DARefinanciacion.CrearRefinanciacion(pRefinanciacion, pUsuario);
                    ts.Complete();
                    return pRefinanciacion;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RefinanciacionBusiness", "CrearRefinanciacion", ex);
                return null;
            }
        }
    }
}
