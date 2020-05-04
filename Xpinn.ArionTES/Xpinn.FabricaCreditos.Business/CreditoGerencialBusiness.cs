using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Data;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Business
{
    /// <summary>
    /// Objeto de negocio para Vehiculos
    /// </summary>
    public class CreditoGerencialBusiness : GlobalData
    {
        private CreditoGerencialData DAVehiculos;

        /// <summary>
        /// Constructor del objeto de negocio para Vehiculos
        /// </summary>
        public CreditoGerencialBusiness()
        {
            DAVehiculos = new CreditoGerencialData();
        }


        public List<Credito> ListarCreditoGerencial(Usuario pUsuario, String filtro)
        {
            try
            {
                return DAVehiculos.ListarCreditoGerencial(pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoGerencialBusiness", "ListarCreditoGerencial", ex);
                return null;
            }
        }


        public List<Atributos> ListarAtributosXlinea(string pLinea, Usuario pUsuario)
        {
            try
            {
                return DAVehiculos.ListarAtributosXlinea(pLinea, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoGerencialBusiness", "ListarAtributosXlinea", ex);
                return null;
            }
        }

        public LineasCredito ConsultarDatosXatributos(LineasCredito pLineas, Usuario pUsuario)
        {
            try
            {
                return DAVehiculos.ConsultarDatosXatributos(pLineas, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoGerencialBusiness", "ConsultarDatosXatributos", ex);
                return null;
            }
        }

    }
}