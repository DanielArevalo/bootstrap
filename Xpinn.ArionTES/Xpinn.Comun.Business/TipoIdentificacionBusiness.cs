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
    public class TipoIdentificacionBusiness : GlobalData
    {

        private TipoIdentificacionData DATipoIdentificacion;

        /// <summary>
        /// Constructor del objeto de negocio para cierre histórico
        /// </summary>
        public TipoIdentificacionBusiness()
        {
            DATipoIdentificacion = new TipoIdentificacionData();
        }

        public List<TipoIdentificacion> ListarTipoIdentificacion(TipoIdentificacion pTipo, Usuario pUsuario)
        {
            return DATipoIdentificacion.ListarTipoIdentificacion(pTipo, pUsuario);
        }

        public TipoIdentificacion ConsultarTipoIdentificacion(Int64 pTipo, Usuario pUsuario)
        {
            return DATipoIdentificacion.ConsultarTipoIdentificacion(pTipo, pUsuario);
        }

        public TipoIdentificacion ConsultarTipoIdentificacion(String pTipo, Usuario pUsuario)
        {
            return DATipoIdentificacion.ConsultarTipoIdentificacion(pTipo, pUsuario);
        }


    }
}
