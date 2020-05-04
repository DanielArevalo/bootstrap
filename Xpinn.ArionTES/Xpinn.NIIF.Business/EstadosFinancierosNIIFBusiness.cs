using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.NIIF.Data;
using Xpinn.NIIF.Entities;

namespace Xpinn.NIIF.Business
{
    /// <summary>
    /// Objeto de negocio para EstadosFinancierosNIIF
    /// </summary>
    public class EstadosFinancierosNIIFBusiness : GlobalData
    {
        private EstadosFinancierosNIIFData DAEstadosFinancierosNIIF;

        /// <summary>
        /// Constructor del objeto de negocio para EstadosFinancierosNIIF
        /// </summary>
        public EstadosFinancierosNIIFBusiness()
        {
            DAEstadosFinancierosNIIF = new EstadosFinancierosNIIFData();
        }
        public bool CrearConceptosNIF(List<EstadosFinancierosNIIF> lstEstadosFinancierosNIIF, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    foreach (EstadosFinancierosNIIF pEstadosFinancierosNIIF in lstEstadosFinancierosNIIF)
                    {
                        if (pEstadosFinancierosNIIF.cod_concepto == 0)
                            DAEstadosFinancierosNIIF.CrearConceptosNIF(pEstadosFinancierosNIIF, vUsuario);
                        else
                            DAEstadosFinancierosNIIF.ModificarConceptosNIF(pEstadosFinancierosNIIF, vUsuario);
                    }
                    ts.Complete();
                    return true;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadosFinancierosNIFBusiness", "CrearConceptosNIF", ex);
                return false;
            }
        }
        public EstadosFinancierosNIIF CrearConceptosNIIF(EstadosFinancierosNIIF pEstadosFinancierosNIF, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pEstadosFinancierosNIF = DAEstadosFinancierosNIIF.CrearConceptosNIF(pEstadosFinancierosNIF, pUsuario);
                    Int64 cod;
                    cod = Convert.ToInt32(pEstadosFinancierosNIF.cod_concepto);

                    if (pEstadosFinancierosNIF.lstDetalle != null)
                    {
                        int num = 0;
                        foreach (EstadosFinancierosNIIF eEstruc in pEstadosFinancierosNIF.lstDetalle)
                        {
                            EstadosFinancierosNIIF nEstructura = new EstadosFinancierosNIIF();
                            eEstruc.cod_concepto = cod;
                            nEstructura = DAEstadosFinancierosNIIF.CrearCuentasConceptosNIF(eEstruc, pUsuario);
                            num += 1;
                        }
                    }

                    ts.Complete();
                }

                return pEstadosFinancierosNIF;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadosFinancierosNIFBusiness", "CrearConceptosNIIF", ex);
                return null;
            }
        }
        public EstadosFinancierosNIIF ModificarConceptosNIF(EstadosFinancierosNIIF pEstadosFinancierosNIF, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pEstadosFinancierosNIF = DAEstadosFinancierosNIIF.ModificarConceptosNIF(pEstadosFinancierosNIF, pUsuario);

                    Int64 cod;
                    cod = Convert.ToInt32(pEstadosFinancierosNIF.cod_concepto);


                    if (pEstadosFinancierosNIF.lstDetalle != null)
                    {
                        int num = 0;
                        Int64 codigo = 0;
                        foreach (EstadosFinancierosNIIF eEstruc in pEstadosFinancierosNIF.lstDetalle)
                        {
                            eEstruc.cod_concepto = cod;
                            codigo = eEstruc.codigo;
                            EstadosFinancierosNIIF nEstructura = new EstadosFinancierosNIIF();
                            if (eEstruc.codigo <= 0 || eEstruc.codigo == null)                            
                                nEstructura = DAEstadosFinancierosNIIF.CrearCuentasConceptosNIF(eEstruc, pUsuario);
                            else
                                nEstructura = DAEstadosFinancierosNIIF.ModificarEstructuraDetalle(eEstruc, pUsuario);                           
                           
                            
                            num += 1;



                            if (eEstruc.codigo > 0 && eEstruc.cod_cuenta_niif == "0")
                                DAEstadosFinancierosNIIF.EliminarCuentasConceptosNIF(codigo, pUsuario);

                        }
                    }


                    ts.Complete();
                }

                return pEstadosFinancierosNIF;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadosFinancierosNIFBusiness", "ModificarConceptosNIF", ex);
                return null;
            }
        }
        public void EliminarConceptosNIF(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAEstadosFinancierosNIIF.EliminarConceptosNIF(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadosFinancierosNIFBusiness", "EliminarConceptosNIF", ex);
            }
        }
        public void EliminarCuentasConceptosNIF(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAEstadosFinancierosNIIF.EliminarCuentasConceptosNIF(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadosFinancierosNIFBusiness", "EliminarCuentasConceptosNIF", ex);
            }
        }


        public EstadosFinancierosNIIF ConsultarConceptosNIF(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAEstadosFinancierosNIIF.ConsultarConceptosNIF(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadosFinancierosNIFBusiness", "ConsultarConceptosNIF", ex);
                return null;
            }
        }
        public List<EstadosFinancierosNIIF> ConsultarCuentasNIIF(Int32 nivel, Usuario pUsuario)
        {
            try
            {
                return DAEstadosFinancierosNIIF.ConsultarCuentasNIIF(nivel,pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadosFinancierosNIFBusiness", "ConsultarCuentasNIIF", ex);
                return null;
            }
        }

        public List<EstadosFinancierosNIIF> ConsultarDependeDe(Int32 tipo, Usuario pUsuario)
        {
            try
            {
                return DAEstadosFinancierosNIIF.ConsultarDependeDe(tipo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadosFinancierosNIFBusiness", "ConsultarDependeDe", ex);
                return null;
            }
        }
        public List<EstadosFinancierosNIIF> ConsultarCuentasLocalNIIF(Int32 nivel,Usuario pUsuario)
        {
            try
            {
                return DAEstadosFinancierosNIIF.ConsultarCuentasLocalNIIF(nivel,pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadosFinancierosNIFBusiness", "ConsultarCuentasLocalNIIF", ex);
                return null;
            }
        }
        public List<EstadosFinancierosNIIF> ListarCuentasNIIF(Int64 pcodigo, Usuario pUsuario)
        {
            try
            {
                return DAEstadosFinancierosNIIF.ListarCuentasNIIF(pcodigo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadosFinancierosNIFBusiness", "ListarCuentasNIIF", ex);
                return null;
            }
        }
        public List<EstadosFinancierosNIIF> ListarCuentasLocalNIIF(Int64 pcodigo, Usuario pUsuario)
        {
            try
            {
                return DAEstadosFinancierosNIIF.ListarCuentasLocalNIIF(pcodigo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadosFinancierosNIFBusiness", "ListarCuentasLocalNIIF", ex);
                return null;
            }
        }
        public List<EstadosFinancierosNIIF> ListarConceptosNIF(Int64 pestadofinanciero, Usuario pUsuario)
        {
            try
            {
                return DAEstadosFinancierosNIIF.ListarConceptosNIF(pestadofinanciero, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadosFinancierosNIFBusiness", "ListarConceptosNIF", ex);
                return null;
            }
        }
        public List<EstadosFinancierosNIIF> ListarTipoEstadoFinancieroNIIF(string filtro, Usuario pUsuario)
        {
            try
            {
                return DAEstadosFinancierosNIIF.ListarTipoEstadoFinancieroNIIF(filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadosFinancierosNIFBusiness", "ListarTipoEstadoFinancieroNIF", ex);
                return null;
            }
        }
    }

}