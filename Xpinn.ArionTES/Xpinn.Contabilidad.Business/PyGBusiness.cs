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
    public class PyGBusiness : GlobalBusiness
    {
        private PyGData DAPyG;

        /// <summary>
        /// Constructor del objeto de negocio para concepto
        /// </summary>
        public PyGBusiness()
        {
            DAPyG = new PyGData();
        }

        public List<PyG> ListarPyG(PyG pEntidad, Usuario vUsuario, int pOpcion)
        {
            return DAPyG.ListarPyG(pEntidad, vUsuario, pOpcion);
        }

        public List<PyG> ListarFechaCierre(Usuario vUsuario)
        {
            return DAPyG.ListarFechaCierre(vUsuario);
        }
    }
}

