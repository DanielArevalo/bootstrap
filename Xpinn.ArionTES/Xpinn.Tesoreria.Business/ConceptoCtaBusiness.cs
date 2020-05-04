using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Tesoreria.Data;
using Xpinn.Tesoreria.Entities;
using Xpinn.Contabilidad.Entities;
namespace Xpinn.Tesoreria.Business
{ 
    public class ConceptoCtaBusiness : GlobalBusiness
    {
 
        private ConceptoCtaData DAConceptoCta;
 
        public ConceptoCtaBusiness()
        {
            DAConceptoCta = new ConceptoCtaData();
        }

        public ConceptoCta CrearConceptoCta(ConceptoCta pConceptoCta, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pConceptoCta = DAConceptoCta.CrearConceptoCta(pConceptoCta, pusuario);
                    if (pConceptoCta.lstImpuesto != null && pConceptoCta.lstImpuesto.Count > 0)
                    {
                        foreach (Concepto_CuentasXpagarImp pImpuesto in pConceptoCta.lstImpuesto)
                        {
                            pImpuesto.cod_concepto_fac = pConceptoCta.cod_concepto_fac; 
                            Concepto_CuentasXpagarImp Impuesto = new Concepto_CuentasXpagarImp();
                            Impuesto = DAConceptoCta.CrearConcepto_CtaXpagarImpuesto(pImpuesto, pusuario);
                        }
                    }
                    ts.Complete();
 
                }
 
                return pConceptoCta;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConceptoCtaBusiness", "CrearConceptoCta", ex);
                return null;
            }
        }
 
 
        public ConceptoCta ModificarConceptoCta(ConceptoCta pConceptoCta, Usuario pusuario)
        {
          
         try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pConceptoCta = DAConceptoCta.ModificarConceptoCta(pConceptoCta, pusuario);
                    if (pConceptoCta.lstImpuesto != null && pConceptoCta.lstImpuesto.Count > 0)
                    {
                        foreach (Concepto_CuentasXpagarImp pImpuesto in pConceptoCta.lstImpuesto)
                        {
                            pImpuesto.cod_concepto_fac = pConceptoCta.cod_concepto_fac; 
                            Concepto_CuentasXpagarImp Impuesto = new Concepto_CuentasXpagarImp();
                            if (pImpuesto.idconceptoimp != null && pImpuesto.idconceptoimp > 0)
                                Impuesto = DAConceptoCta.ModificarConcepto_CtaXpagarImpuesto(pImpuesto, pusuario);
                            else
                                Impuesto = DAConceptoCta.CrearConcepto_CtaXpagarImpuesto(pImpuesto, pusuario);
                        }
                    }

                    if (pConceptoCta.lstImpuesto != null && pConceptoCta.lstImpuesto.Count > 0)
                    {
                        foreach (Concepto_CuentasXpagarImp rImpu in pConceptoCta.lstImpuesto)
                        {
                            Concepto_CuentasXpagarImp nImpuesto = new Concepto_CuentasXpagarImp();
                            rImpu.cod_cuenta = pConceptoCta.cod_cuenta;
                            if (rImpu.idimpuesto > 0 && rImpu.idimpuesto != null)
                            {
                                nImpuesto = DAConceptoCta.ModificarPlanCuentasImpuesto(rImpu, pusuario);
                            }
                            else
                            {
                            //    nImpuesto = DAConceptoCta.CrearPlanCuentasImpuesto(rImpu, pusuario);
                            }
                        }
                    }
                    else
                    {
                       // DAImpuestos.EliminarPlanCuentaImpuestoXCuenta(pPlanCuentas.cod_cuenta, vUsuario);
                    }


                    ts.Complete();
 
                }
 
                return pConceptoCta;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConceptoCtaBusiness", "ModificarConceptoCta", ex);
                return null;
            }
        }
 
 
        public void EliminarConceptoCta(Int64 pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAConceptoCta.EliminarConceptoCta(pId, pusuario);
 
                    ts.Complete();
 
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConceptoCtaBusiness", "EliminarConceptoCta", ex);
            }
        }


        public void EliminarConceptoImpuesto(Int32 pId, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAConceptoCta.EliminarConceptoImpuesto(pId, vUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConceptoCtaBusiness", "EliminarConceptoImpuesto", ex);
            }
        }
 
        public ConceptoCta ConsultarConceptoCta(Int64 pId, Usuario pusuario)
        {
            try
            {
                ConceptoCta ConceptoCta = new ConceptoCta();
                ConceptoCta = DAConceptoCta.ConsultarConceptoCta(pId, pusuario);
                return ConceptoCta;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConceptoCtaBusiness", "ConsultarConceptoCta", ex);
                return null;
            }
        }
 
 
        public List<ConceptoCta> ListarConceptoCta(ConceptoCta pConceptoCta, Usuario pusuario)
        {
            try
            {
                return DAConceptoCta.ListarConceptoCta(pConceptoCta, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConceptoCtaBusiness", "ListarConceptoCta", ex);
                return null;
            }
        }


        public List<Concepto_CuentasXpagarImp> ListarConceptoImpuesto(Concepto_CuentasXpagarImp pImpuesto, Usuario vUsuario)
        {
            try
            {
                return DAConceptoCta.ListarConceptoImpuesto(pImpuesto, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConceptoCtaBusiness", "ListarConceptoImpuesto", ex);
                return null;
            }
        }

        public List<Concepto_CuentasXpagarImp> ListarConceptoImpuestoDetalle(Concepto_CuentasXpagarImp pImpuesto, Usuario vUsuario)
        {
            try
            {
                return DAConceptoCta.ListarConceptoImpuestoDetalle(pImpuesto, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConceptoCtaBusiness", "ListarConceptoImpuestoDetalle", ex);
                return null;
            }
        }

        public List<Concepto_CuentasXpagarImp> ListarConceptoImpuestoDetalleCxp(Concepto_CuentasXpagarImp pImpuesto, Usuario vUsuario)
        {
            try
            {
                return DAConceptoCta.ListarConceptoImpuestoDetalleCxp(pImpuesto, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConceptoCtaBusiness", "ListarConceptoImpuestoDetalleCxp", ex);
                return null;
            }
        }

        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            try
            {
                return DAConceptoCta.ObtenerSiguienteCodigo(pUsuario);
            }
            catch (Exception ex)
            {
                return 0;
            }
        } 
       
    }
}
