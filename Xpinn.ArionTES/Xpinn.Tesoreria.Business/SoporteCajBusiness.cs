using System;
using System.Collections.Generic;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Tesoreria.Data;
using Xpinn.Tesoreria.Entities;

namespace Xpinn.Tesoreria.Business
{
    /// <summary>
    /// Objeto de negocio para SoporteCaj
    /// </summary>
    public class SoporteCajBusiness : GlobalBusiness
    {
        private SoporteCajData DASoporteCaj;

        /// <summary>
        /// Constructor del objeto de negocio para SoporteCaj
        /// </summary>
        public SoporteCajBusiness()
        {
            DASoporteCaj = new SoporteCajData();
        }

        /// <summary>
        /// Crea un SoporteCaj
        /// </summary>
        /// <param name="pSoporteCaj">Entidad SoporteCaj</param>
        /// <returns>Entidad SoporteCaj creada</returns>
        public SoporteCaj CrearSoporteCaj(SoporteCaj pSoporteCaj, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pSoporteCaj = DASoporteCaj.CrearSoporteCaj(pSoporteCaj, pUsuario);

                    ts.Complete();
                }

                return pSoporteCaj;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SoporteCajBusiness", "CrearSoporteCaj", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un SoporteCaj
        /// </summary>
        /// <param name="pSoporteCaj">Entidad SoporteCaj</param>
        /// <returns>Entidad SoporteCaj modificada</returns>
        public SoporteCaj ModificarSoporteCaj(SoporteCaj pSoporteCaj, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pSoporteCaj = DASoporteCaj.ModificarSoporteCaj(pSoporteCaj, pUsuario);

                    ts.Complete();
                }

                return pSoporteCaj;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SoporteCajBusiness", "ModificarSoporteCaj", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un SoporteCaj
        /// </summary>
        /// <param name="pId">Identificador de SoporteCaj</param>
        public void EliminarSoporteCaj(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DASoporteCaj.EliminarSoporteCaj(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SoporteCajBusiness", "EliminarSoporteCaj", ex);
            }
        }

        /// <summary>
        /// Obtiene un SoporteCaj
        /// </summary>
        /// <param name="pId">Identificador de SoporteCaj</param>
        /// <returns>Entidad SoporteCaj</returns>
        public SoporteCaj ConsultarSoporteCaj(Int64 pId, Usuario vUsuario)
        {
            try
            {
                SoporteCaj SoporteCaj = new SoporteCaj();

                SoporteCaj = DASoporteCaj.ConsultarSoporteCaj(pId, vUsuario);

                return SoporteCaj;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SoporteCajBusiness", "ConsultarSoporteCaj", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pSoporteCaj">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de SoporteCaj obtenidos</returns>
        public List<SoporteCaj> ListarSoporteCaj(SoporteCaj pSoporteCaj, Usuario pUsuario)
        {
            try
            {
                return DASoporteCaj.ListarSoporteCaj(pSoporteCaj, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SoporteCajBusiness", "ListarSoporteCaj", ex);
                return null;
            }
        }

        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            try
            {
                return DASoporteCaj.ObtenerSiguienteCodigo(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SoporteCajBusiness", "ObtenerSiguienteCodigo", ex);
                return 0;
            }
        }

        public List<SoporteCaj> ComprobanteSoporteCaj(List<SoporteCaj> pLstSoporteCaj, ref Giro pGiro, Operacion pOperacion, ref string pError, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (pLstSoporteCaj != null)
                    {

                        // Agregado para crear la operación
                        OperacionData DAOperacion = new OperacionData();
                        pOperacion = DAOperacion.GrabarOperacion(pOperacion, pUsuario);
                        if (pOperacion == null)
                        {
                            ts.Dispose();
                            pError = "No se generó el código de operación.";
                            return null;
                        }
                        if (pOperacion.cod_ope == 0)
                        {
                            ts.Dispose();
                            pError = "No se generó el código de operación de manera correcta.";
                            return null;
                        }
                        
                        // ACTUALIZANDO LOS SOPORTES CON CODIGO DE OPERACION
                        foreach (SoporteCaj nSoporte in pLstSoporteCaj)
                        {
                            nSoporte.cod_ope = pOperacion.cod_ope;
                            nSoporte.tipo_ope = pOperacion.tipo_ope;
                            nSoporte.estado = "2";
                            DASoporteCaj.ModificarSoporteCaj(nSoporte, pUsuario);
                        }
                        
                        // REGISTRO DE GIRO
                        if (pGiro != null)
                        {
                            GiroData AvancData = new GiroData();
                            pGiro.cod_ope = pOperacion.cod_ope;
                            pGiro = AvancData.CrearGiro(pGiro, pUsuario, 1);
                        }
                    }
                    ts.Complete();
                }
                return pLstSoporteCaj;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SoporteCajBusiness", "ComprobanteSoporteCaj", ex);
                return null;
            }
        }

        public SoporteCaj ActualizarSoporteArqueo (Int64 id_areas, Int64? id_arqueo, Usuario pUsuario)
        {
            try
            {
                SoporteCaj pSoporteA = new SoporteCaj();
                List<SoporteCaj> lstSoporte = new List<SoporteCaj>();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.Required))
                {
                    lstSoporte = DASoporteCaj.ConsultarSoporteArqueo(id_areas, pUsuario);
                    t.Complete();
                }
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    foreach (SoporteCaj soporteC in lstSoporte)
                    {
                        pSoporteA = DASoporteCaj.ActualizarSoporteArqueo(soporteC, id_arqueo, pUsuario);
                    }                    
                    ts.Complete();
                }
                return pSoporteA;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SoporteCajBusiness", "ActualizarSoporteArqueo", ex);
                return null;
            }
        }

    }
}