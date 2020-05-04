using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Comun.Entities;
using Xpinn.Comun.Business;
using System.Web;

namespace Xpinn.Comun.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ListaDeplegableService
    {
        private ListaDeplegableBusiness BOListaDeplegable;
        private ListaDeplegableBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para CierreHistorio
        /// </summary>
        public ListaDeplegableService()
        {
            BOListaDeplegable = new ListaDeplegableBusiness();
            BOExcepcion = new ListaDeplegableBusiness();
        }

        public List<Xpinn.Comun.Entities.ListaDesplegable> ListarListaDesplegable(Xpinn.Comun.Entities.ListaDesplegable pLista, string pTabla, Usuario pUsuario)
        {
            return ListarListaDesplegable(pLista, pTabla, pUsuario);
        }

        public List<ListaDesplegable> ListarListaDesplegable(ListaDesplegable pListaDesplegable, string pTabla, string pColumnas, string pCondicion, string pOrden, Usuario pUsuario)
        {
            try
            {
                return BOListaDeplegable.ListarListaDesplegable(pListaDesplegable, pTabla, pColumnas, pCondicion, pOrden, pUsuario);
            }
            catch (Exception)
            {
                throw;
            }
            
        }


        public List<ListaDesplegable> ListarListaDesplegable2(ListaDesplegable pListaDesplegable, string IdLinea, Usuario pUsuario)
        {
            try
            {
                return BOListaDeplegable.ListarListaDesplegable2(pListaDesplegable, IdLinea, pUsuario);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public List<ListaDesplegable> ListarListaDesplegable3(ListaDesplegable pListaDesplegable, string IdLinea, Usuario pUsuario)
        {
            try
            {
                return BOListaDeplegable.ListarListaDesplegable3(pListaDesplegable, IdLinea, pUsuario);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public List<ListaDesplegable> ListarListaDesplegableEmpresaaportes(ListaDesplegable pListaDesplegable, string pTabla, string pColumnas, string pCondicion, string pOrden, Usuario pUsuario)
        {
            try
            {
                return BOListaDeplegable.ListarListaDesplegableEmpresaaportes(pListaDesplegable, pTabla, pColumnas, pCondicion, pOrden, pUsuario);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public List<ListaDesplegable> ListarPeriodicidad(string pCodPeriodicidad, Usuario pUsuario)
        {
            try
            {
                return BOListaDeplegable.ListarPeriodicidad(pCodPeriodicidad, pUsuario);
            }
            catch (Exception ex)
            {
                throw;
            }

        }


    }
}
