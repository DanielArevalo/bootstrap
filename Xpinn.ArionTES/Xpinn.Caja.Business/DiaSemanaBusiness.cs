using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Caja.Data;
using Xpinn.Caja.Entities;


namespace Xpinn.Caja.Business
{
    /// <summary>
    /// Objeto de negocio para Dia Semana
    /// </summary>
    public class DiaSemanaBusiness:GlobalData
    {
        private DiaSemanaData DADiaSemana;

        /// <summary>
        /// Constructor del objeto de negocio para DiaSemana
        /// </summary>
        public DiaSemanaBusiness()
        {
            DADiaSemana = new DiaSemanaData();
        }

          /// <summary>
        /// Obtiene la lista de Personaes dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Personaes obtenidos</returns>
        public List<DiaSemana> ListarDiaSemana(DiaSemana pDiaSemana, Usuario pUsuario)
        {
            try
            {
                return DADiaSemana.ListarDiaSemana(pDiaSemana, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DiaSemanaBusiness", "ListarDiaSemana", ex);
                return null;
            }
        }

    }
}
