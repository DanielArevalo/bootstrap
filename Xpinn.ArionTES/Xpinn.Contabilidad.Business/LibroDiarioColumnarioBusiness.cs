using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Contabilidad.Data;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.Contabilidad.Business
{
    /// <summary>
    /// Objeto de negocio para concepto
    /// </summary>
    public class LibroDiarioColumnarioBusiness : GlobalBusiness
    {
        private LibroDiarioColumnarioData DALibroDiarioColumnario;

        /// <summary>
        /// Constructor del objeto de negocio para concepto
        /// </summary>
        public LibroDiarioColumnarioBusiness()
        {
            DALibroDiarioColumnario= new LibroDiarioColumnarioData();
        } 

        public List<LibroDiarioColumnario> ListarLibroDiarioNiff(LibroDiarioColumnario pDatos, Usuario pUsuario)
        {
            return DALibroDiarioColumnario.ListarLibroDiarioNiff(pDatos, pUsuario);
        }
        public List<LibroDiarioColumnario> ListarLibroDiario(LibroDiarioColumnario pDatos, Usuario pUsuario)
        {
            return DALibroDiarioColumnario.ListarLibroDiario(pDatos, pUsuario);
        }

        public List<LibroDiarioColumnario> ListarFechaCorte(Usuario vUsuario)
        {
            return DALibroDiarioColumnario.ListarFechaCorte(vUsuario);
        }
    }
}

