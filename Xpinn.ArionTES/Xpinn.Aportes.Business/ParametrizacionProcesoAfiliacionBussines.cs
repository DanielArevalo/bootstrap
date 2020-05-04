using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Aportes.Business;
using Xpinn.Aportes.Entities;
using Xpinn.Aportes.Data;

namespace Xpinn.Aportes.Business
{
    public class ParametrizacionProcesoAfiliacionBussines : GlobalBusiness
    {
        private ParametrizacionProcesoAfiliacionData DAparametrizacionProcesoAfiliacionData;

        /// <summary>
        /// Constructor del objeto de negocio para Perfil
        /// </summary>
        public ParametrizacionProcesoAfiliacionBussines()
        {
            DAparametrizacionProcesoAfiliacionData = new ParametrizacionProcesoAfiliacionData();
        }
        public List<ParametrizacionProcesoAfiliacion> ListarParametrosProcesoAfiliacion(Usuario pUsuario)
        {
            try
            {
                return DAparametrizacionProcesoAfiliacionData.ListarParametrosProcesoAfiliacion(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametrizacionProcesoAfiliacionBussines", "ListarParametrosProcesoAfiliacion", ex);
                return null;
            }
        }
        public List<ParametrizacionProcesoAfiliacion> ListarDetalleRuta(string iden, Usuario pUsuario)
        {
            try
            {
                return DAparametrizacionProcesoAfiliacionData.ListarDetalleRuta(iden, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametrizacionProcesoAfiliacionBussines", "ListarDetalleRuta", ex);
                return null;
            }
        }
        public List<ParametrizacionProcesoAfiliacion> ListarHistorialRuta(string iden, Usuario pUsuario)
        {
            try
            {
                return DAparametrizacionProcesoAfiliacionData.ListarHistorialRuta(iden, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametrizacionProcesoAfiliacionBussines", "ListarHistorialRuta", ex);
                return null;
            }
        }
        public ParametrizacionProcesoAfiliacion validarProcesoAnterior(string cod_per, Int32 proceso, Usuario pUsuario)
        {
            try
            {
                return DAparametrizacionProcesoAfiliacionData.validarProcesoAnterior(cod_per, proceso, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametrizacionProcesoAfilicacionService", "ListarParametrosProcesoAfiliacion", ex);
                return null;
            }
        }
        public bool controlRegistrado(Int32 cod_proceso, Int64 cod_per, Usuario pUsuario)
        {
            try
            {
                return DAparametrizacionProcesoAfiliacionData.controlRegistrado(cod_proceso, cod_per, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametrizacionProcesoAfilicacionService", "controlRegistrado", ex);
                return false;
            }
        }
        public void cambiarEstadoAsociado(string estado, Int64 cod_per, Usuario pUsuario)
        {
            try
            {
                DAparametrizacionProcesoAfiliacionData.cambiarEstadoAsociado(estado, cod_per, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametrizacionProcesoAfilicacionService", "cambiarEstadoAsociado", ex);
            }
        }
        public ParametrizacionProcesoAfiliacion ModificarParametros(ParametrizacionProcesoAfiliacion lstParam, Usuario usuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    lstParam = DAparametrizacionProcesoAfiliacionData.ModificarParametros(lstParam, usuario);
                    ts.Complete();
                }

                return lstParam;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametrizacionProcesoAfiliacionBussines", "ModificarParametros", ex);
                return null;
            }
        }
    }
}
