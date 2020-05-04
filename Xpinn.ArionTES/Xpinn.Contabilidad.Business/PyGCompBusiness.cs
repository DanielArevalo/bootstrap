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
    public class PyGCompBusiness : GlobalBusiness
    {
        private PyGCompData DAPyGComp;

        /// <summary>
        /// Constructor del objeto de negocio para concepto
        /// </summary>
        public PyGCompBusiness()
        {
            DAPyGComp = new PyGCompData();
        }

        public List<PyGComp> ListarPyGComparativo(PyGComp pEntidad, Usuario vUsuario, int pOpcion)
        {
            return DAPyGComp.ListarPyGComparativo(pEntidad, vUsuario, pOpcion);
        }

        public List<PyGComp> ListarFecha(Usuario pUsuario)
        {
            return DAPyGComp.ListarFecha(pUsuario);
        }


    }
}

