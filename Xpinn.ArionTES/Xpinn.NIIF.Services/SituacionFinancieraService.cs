﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.NIIF.Business;
using Xpinn.NIIF.Entities;

namespace Xpinn.NIIF.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class SituacionFinancieraService
    {
        private SituacionFinancieraBusiness BOSituacionFinanciera;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Balance General Comparativo
        /// </summary>
        public SituacionFinancieraService()
        {
            BOSituacionFinanciera = new SituacionFinancieraBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }
    public string CodigoProgramaNIIF { get { return "210309"; } }


        /// <summary>
        /// Servicio para obtener lista de  balance comparativo a partir de unos filtros
        /// </summary>
        /// <param name="pMovimientoCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de balance comparativo obtenidos</returns>
        public List<SituacionFinanciera> ListarSituacionFinanciera(SituacionFinanciera pEntidad, Usuario vUsuario, int pOpcion)
        {
            try
            {
                return BOSituacionFinanciera.ListarSituacionFinanciera(pEntidad, vUsuario, pOpcion);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SituacionFinancieraService", "ListarSituacionFinanciera", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de  balance comparativo a partir de unos filtros
        /// </summary>
        /// <param name="pMovimientoCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de balance comparativo obtenidos</returns>
        public List<SituacionFinanciera> ListarFecha(Usuario pUsuario)
        {
            try
            {
                return BOSituacionFinanciera.ListarFecha(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SituacionFinancieraService", "ListarFecha", ex);
                return null;
            }
        }       

    }
}