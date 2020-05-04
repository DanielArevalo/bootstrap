using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Tesoreria.Data;
using Xpinn.Tesoreria.Entities;

namespace Xpinn.Tesoreria.Business
{
    /// <summary>
    /// Objeto de negocio para AreasCaj
    /// </summary>
    public class AprobacionGirosBusiness : GlobalBusiness
    {
        private AprobacionGirosData DAAprobacion;

        /// <summary>
        /// Constructor del objeto de negocio para AreasCaj
        /// </summary>
        public AprobacionGirosBusiness()
        {
            DAAprobacion = new AprobacionGirosData();
        }


        public List<Giro> ListarGiro(Giro pGiro, String Orden, DateTime pFechaGiro, Usuario vUsuario)
        {
            try
            {
                return DAAprobacion.ListarGiro(pGiro, Orden, pFechaGiro,vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionGirosBusiness", "ListarGiro", ex);
                return null;
            }
        }

        public Giro AprobarGiro(Giro pGiroTot, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (pGiroTot.lstGiro != null && pGiroTot.lstGiro.Count > 0)
                    {
                        foreach (Giro nGiro in pGiroTot.lstGiro)
                        {
                            Giro pEntidad = new Giro();
                            nGiro.estado = 1;
                            nGiro.usu_apro = vUsuario.nombre;
                            if (nGiro.fec_apro == DateTime.MinValue)
                            {
                                nGiro.fec_apro = DateTime.Now;
                            }
                            if (nGiro.nom_forma_pago != "Efectivo")
                            {
                                nGiro.idctabancaria = nGiro.idctabancaria;
                                if (nGiro.idctabancaria == 0)
                                {
                                    if (pGiroTot.idctabancaria != 0)
                                        nGiro.idctabancaria = pGiroTot.idctabancaria;
                                }
                            }
                            else
                                nGiro.idctabancaria = 0;
                            pEntidad = DAAprobacion.AprobarGiro(nGiro, vUsuario);
                        }
                    }
                    ts.Complete();
                }
                return pGiroTot;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionGirosBusiness", "AprobarGiro", ex);
                return null;
            }
        }

        public CuentasBancarias ConsultarCuentasBancariasXNumCuenta(String pNumCuenta, Usuario vUsuario)
        {
            try
            {
                return DAAprobacion.ConsultarCuentasBancariasXNumCuenta(pNumCuenta,vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionGirosBusiness", "ConsultarCuentasBancariasXNumCuenta", ex);
                return null;
            }
        }

    }
}