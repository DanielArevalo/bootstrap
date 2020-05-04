using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Data;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Business
{
    public class DatosAprobadorBusiness : GlobalData
    {
        private DatosAprobadorData DAAprobador;

        /// <summary>
        /// Constructor del objeto de negocio para Caja
        /// </summary>
        public DatosAprobadorBusiness()
        {
            DAAprobador = new DatosAprobadorData();
        }
        
        /// <summary>
        /// Obtiene la lista de Aprobadores
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Conjunto de Aprobadores obtenidos</returns>
        public List<DatosAprobador> ListarDatosAprobador(DatosAprobador pDatos, Usuario pUsuario)
        {
            try
            {
                return DAAprobador.ListarDatosAprobador(pDatos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosAprobadorBusiness", "ListarDatosAprobador", ex);
                return null;
            }
        }
    }
}
