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
using Xpinn.Comun.Entities;

namespace Xpinn.Cartera.Business
{
    public class RepEdadesBusiness : GlobalData
    {

        private RepEdadesData DARepEdades;

        /// <summary>
        /// Constructor del objeto de negocio para cierre histórico
        /// </summary>
        public RepEdadesBusiness()
        {
            DARepEdades = new RepEdadesData();
        }

        public List<Cierea> ListarFechaCierre(Usuario pUsuario)
        {
            try
            {
                return DARepEdades.ListarFechaCierre(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RepEdadesBusiness", "ListarCredito", ex);
                return null;
            }
        }

        public List<EdadMora> ListarRangos(Usuario pUsuario)
        {
            try
            {
                return DARepEdades.ListarRangos(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RepEdadesBusiness", "ListarRangos", ex);
                return null;
            }
        }

        /// <summary>
        /// Método para listar los créditos a refinanciar
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public List<RepEdades> ListarCredito(DateTime pFecha, Usuario pUsuario, String filtro)
        {
            try
            {                
                return DARepEdades.ListarCredito(pFecha, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RepEdadesBusiness", "ListarCredito", ex);
                return null;
            }
        }

        

    }
}
