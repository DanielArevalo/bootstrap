using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Tesoreria.Data;
using Xpinn.Tesoreria.Entities;

namespace Xpinn.Tesoreria.Business
{
    public class ParametrosFlujoCajaBusiness : GlobalBusiness
    {
        private ParametrosFlujoCajaData DAParametrosFlujoCaja;

        /// <summary>
        /// Constructor del objeto de negocio para FlujoCaja
        /// </summary>
        public ParametrosFlujoCajaBusiness()
        {
            DAParametrosFlujoCaja = new ParametrosFlujoCajaData();
        }
        
        public ParametrosFlujoCaja CrearConceptoCuenta(ParametrosFlujoCaja pFlujoCajaConcepto, List<ParametrosFlujoCaja> lstConceptoCuenta, Usuario pUsuario)
        {            
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pFlujoCajaConcepto = DAParametrosFlujoCaja.CrearConcepto(pFlujoCajaConcepto, pUsuario);
                    if (lstConceptoCuenta != null && lstConceptoCuenta.Count > 0)
                    {
                        foreach (ParametrosFlujoCaja ConceptoCuenta in lstConceptoCuenta)
                        {
                            ParametrosFlujoCaja parametro = new ParametrosFlujoCaja();
                            ConceptoCuenta.cod_concepto = pFlujoCajaConcepto.cod_concepto;
                            parametro = DAParametrosFlujoCaja.CrearConceptoCuenta(ConceptoCuenta, pUsuario);
                        }
                    }
                    ts.Complete();
                }
                return pFlujoCajaConcepto;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametrosFlujoCajaBusiness", "CrearConceptoCuenta", ex);
                return null;
            }
        }
        
        public ParametrosFlujoCaja ModificarConceptoCuenta(ParametrosFlujoCaja pFlujoCajaConcepto, List<ParametrosFlujoCaja> lstConceptoCuenta, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pFlujoCajaConcepto = DAParametrosFlujoCaja.ModificarConcepto(pFlujoCajaConcepto, pUsuario);
                    if (lstConceptoCuenta != null && lstConceptoCuenta.Count > 0)
                    {
                        foreach (ParametrosFlujoCaja ConceptoCuenta in lstConceptoCuenta)
                        {
                            ParametrosFlujoCaja parametro = new ParametrosFlujoCaja();
                            ConceptoCuenta.cod_concepto = pFlujoCajaConcepto.cod_concepto;
                            if(ConceptoCuenta.cod_cuenta_con == null)
                                parametro = DAParametrosFlujoCaja.CrearConceptoCuenta(ConceptoCuenta, pUsuario);
                        }
                    }
                    ts.Complete();
                }
                return pFlujoCajaConcepto;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametrosFlujoCajaBusiness", "ModificarConceptoCuenta", ex);
                return null;
            }
        }

        public ParametrosFlujoCaja ConsultarConceptoCuenta(Int64 cod_concepto, Usuario pUsuario)
        {
            try
            {
                return DAParametrosFlujoCaja.ConsultarConceptoCuenta(cod_concepto, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametrosFlujoCajaBusiness", "ConsultarConceptoCuenta", ex);
                return null;
            }
        }

        public List<ParametrosFlujoCaja> ListarConceptos(ParametrosFlujoCaja pConcepto, Usuario pUsuario)
        {
            try
            {
                return DAParametrosFlujoCaja.ListarConceptos(pConcepto, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametrosFlujoCajaBusiness", "ListarConceptos", ex);
                return null;
            }
        }

        public List<ParametrosFlujoCaja> ListarCuentas(Int64 cod_concepto, Usuario pUsuario)
        {
            try
            {
                return DAParametrosFlujoCaja.ListarCuentas(cod_concepto, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametrosFlujoCajaBusiness", "ListarCuentas", ex);
                return null;
            }
        }

        public void EliminarConcepto(Int64 cod_concepto, Usuario pUsuario)
        {
            try
            {                
                List<ParametrosFlujoCaja> listaCuentas = new List<ParametrosFlujoCaja>();
                listaCuentas = DAParametrosFlujoCaja.ListarCuentas(cod_concepto, pUsuario);
                if(listaCuentas.Count > 0 && listaCuentas != null)
                {
                    foreach(ParametrosFlujoCaja cuenta in listaCuentas)
                    {
                        DAParametrosFlujoCaja.EliminarConceptoCuenta(Convert.ToInt64(cuenta.cod_cuenta_con), pUsuario);
                    }
                }
                DAParametrosFlujoCaja.EliminarConcepto(cod_concepto, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametrosFlujoCajaBusiness", "EliminarConcepto", ex);
            }
        }

        public void EliminarConceptoCuenta(Int64 cod_cuenta_con, Usuario pUsuario)
        {
            try
            {
                DAParametrosFlujoCaja.EliminarConceptoCuenta(cod_cuenta_con, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametrosFlujoCajaBusiness", "EliminarConceptoCuenta", ex);
            }
        }

        public List<ParametrosFlujoCaja> ListarConceptosReporte(DateTime fecha_inicial, DateTime fecha_final, Usuario pUsuario)
        {
            try
            {
                return DAParametrosFlujoCaja.ListarConceptosReporte(fecha_inicial, fecha_final, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametrosFlujoCajaBusiness", "ListarConceptosReporte", ex);
                return null;
            }
        }

        public List<string> ListarTitulos(DateTime fecha_inicial, DateTime fecha_final, Usuario pUsuario)
        {
            try
            {
                return DAParametrosFlujoCaja.ListarTitulos(fecha_inicial, fecha_final, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametrosFlujoCajaBusiness", "ListarTitulos", ex);
                return null;
            }
        }
    }
}
