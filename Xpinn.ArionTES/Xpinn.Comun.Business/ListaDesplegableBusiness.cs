using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Comun.Data;
using Xpinn.Comun.Entities;
using System.Web;

namespace Xpinn.Comun.Business
{
    public class ListaDeplegableBusiness : GlobalData
    {

        private Xpinn.Comun.Data.ListaDesplegableData DAListaDeplegable;

        /// <summary>
        /// Constructor del objeto de negocio para cierre histórico
        /// </summary>
        public ListaDeplegableBusiness()
        {
            DAListaDeplegable = new Xpinn.Comun.Data.ListaDesplegableData();
        }

        /// <summary>
        /// Listar cierres realizados
        /// </summary>
        /// <param name="pTipo"></param>
        /// <returns></returns>
        public List<Xpinn.Comun.Entities.ListaDesplegable> ListarListaDesplegable(Xpinn.Comun.Entities.ListaDesplegable pLista, string pTabla, Usuario pUsuario)
        {
            return ListarListaDesplegable(pLista, pTabla, "", "", "", pUsuario);
        }

        public List<ListaDesplegable> ListarListaDesplegable(ListaDesplegable pListaDesplegable, string pTabla, string pColumnas, string pCondicion, string pOrden, Usuario pUsuario)
        {
            try
            {
                return DAListaDeplegable.ListarListaDesplegable(pListaDesplegable, pTabla, pColumnas, pCondicion, pOrden, pUsuario);
            }
            catch //(Exception ex)
            {
                throw;
            }
            
        }

        public List<ListaDesplegable> ListarListaDesplegable2(ListaDesplegable pListaDesplegable, string IdLinea, Usuario pUsuario)
        {
            try
            {
                return DAListaDeplegable.ListarListaDesplegable2(pListaDesplegable, IdLinea, pUsuario);
            }
            catch //(Exception ex)
            {
                throw;
            }

        }

        public List<ListaDesplegable> ListarListaDesplegable3(ListaDesplegable pListaDesplegable, string IdLinea, Usuario pUsuario)
        {
            try
            {
                return DAListaDeplegable.ListarListaDesplegable3(pListaDesplegable, IdLinea, pUsuario);
            }
            catch //(Exception ex)
            {
                throw;
            }

        }

        public List<ListaDesplegable> ListarListaDesplegableEmpresaaportes(ListaDesplegable pListaDesplegable, string pTabla, string pColumnas, string pCondicion, string pOrden, Usuario pUsuario)
        {
            try
            {
                return DAListaDeplegable.ListarListaDesplegableEmpresaaportes(pListaDesplegable, pTabla, pColumnas, pCondicion, pOrden, pUsuario);
            }
            catch //(Exception ex)
            {
                throw;
            }

        }

        public List<ListaDesplegable> ListarPeriodicidad(string pCodPeriodicidad, Usuario pUsuario)
        {
            try
            {
                return DAListaDeplegable.ListarPeriodicidad(pCodPeriodicidad, pUsuario);
            }
            catch //(Exception ex)
            {
                throw;
            }

        }



    }
}
