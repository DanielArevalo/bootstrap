using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Data;

namespace Xpinn.FabricaCreditos.Business
{
    /// <summary>
    /// Objeto de negocio para Credito
    /// </summary>
    public class Credito_GiroBusiness : GlobalData
    {
        private Credito_GiroData DACredito;

        /// <summary>
        /// Constructor del objeto de negocio para Credito
        /// </summary>
        public Credito_GiroBusiness()
        {
            DACredito = new Credito_GiroData();
        }

        public List<Credito_Giro> ConsultarCredito_Giro(Credito_Giro pCredito_Giro, Usuario vUsuario)
        {
            try
            {
                return DACredito.ConsultarCredito_Giro(pCredito_Giro, vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Credito_GiroBusiness", "ConsultarCredito_Giro", ex);
                return null;
            }
        }

        public bool CrearGiros(List<Credito_Giro> lstGiros, Usuario vUsuario)
        {
            try
            {
                if (lstGiros.Count > 0)
                {
                    Credito_GiroData DACreditoGiro = new Credito_GiroData();
                    foreach (Credito_Giro item in lstGiros)
                    {
                        Credito_Giro pEntidad = new Credito_Giro();
                        pEntidad = DACreditoGiro.CrearCredito_giroACC(item, vUsuario);
                    }
                }                
                return true;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Credito_GiroBusiness", "ConsultarCredito_Giro", ex);
                return false;
            }
        }

        public List<Credito_Giro> ListarGiros(string radicado, Usuario vUsuario)
        {
            try
            {
                return DACredito.ListarGiros(radicado, vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Credito_GiroBusiness", "ListarGiros", ex);
                return null;
            }
        }
    }
}