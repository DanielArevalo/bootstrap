using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Ahorros.Data;
using Xpinn.Ahorros.Entities;
using Xpinn.Comun.Data;
using Xpinn.Comun.Entities;

namespace Xpinn.Ahorros.Business
{
    /// <summary>
    /// Objeto de negocio para NumeracionAhorros
    /// </summary>
    public class NumeracionAhorrosBusiness : GlobalBusiness
    {
        private NumeracionAhorrosData DANumeracionAhorros;
       private CuentaHabienteData DACtaHabiente;
       private GeneralData DAGeneral;
        /// <summary>
        /// Constructor del objeto de negocio para NumeracionAhorros
        /// </summary>
        public NumeracionAhorrosBusiness()
        {
            DANumeracionAhorros = new NumeracionAhorrosData();
            
        }

        /// <summary>
        /// Crea un NumeracionAhorros
        /// </summary>
        /// <param name="pNumeracionAhorros">Entidad NumeracionAhorros</param>
        /// <returns>Entidad NumeracionAhorros creada</returns>



        public NumeracionAhorros CrearNumeracionAhorros(NumeracionAhorros pNumeracionAhorros, Usuario pUsuario)
        {
             try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (pNumeracionAhorros.lstNumeracion != null && pNumeracionAhorros.lstNumeracion.Count > 0)
                    {
                        foreach (NumeracionAhorros nNumeracion in pNumeracionAhorros.lstNumeracion)
                        {
                            nNumeracion.tipo_producto = pNumeracionAhorros.tipo_producto;
                            NumeracionAhorros pEntidad = new NumeracionAhorros();
                            pEntidad = DANumeracionAhorros.CrearNumeracionAhorros(nNumeracion, pUsuario);
                        }
                    }                    
                    ts.Complete();
                }

                return pNumeracionAhorros;
            }
             catch (Exception ex)
             {
                 BOExcepcion.Throw("EmpresaRecaudosBusiness", "CrearEmpresaRecaudo", ex);
                 return null;
             }
        }



        /// <summary>
        /// Modifica un NumeracionAhorros
        /// </summary>
        /// <param name="pNumeracionAhorros">Entidad NumeracionAhorros</param>
        /// <returns>Entidad NumeracionAhorros modificada</returns>
        public NumeracionAhorros ModificarNumeracionAhorros(NumeracionAhorros pNumeracionAhorros, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (pNumeracionAhorros.lstNumeracion != null && pNumeracionAhorros.lstNumeracion.Count > 0)
                    {
                        foreach (NumeracionAhorros nNumeracion in pNumeracionAhorros.lstNumeracion)
                        {
                            nNumeracion.tipo_producto = pNumeracionAhorros.tipo_producto;
                            NumeracionAhorros pEntidad = new NumeracionAhorros();
                            if (nNumeracion.idconsecutivo > 0)
                                pEntidad = DANumeracionAhorros.ModificarNumeracionAhorros(nNumeracion, pUsuario);
                            else
                                pEntidad = DANumeracionAhorros.CrearNumeracionAhorros(nNumeracion, pUsuario);
                        }
                    }
                    ts.Complete();
                }

                return pNumeracionAhorros;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("NumeracionAhorrosBusiness", "ModificarNumeracionAhorros", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un NumeracionAhorros
        /// </summary>
        /// <param name="pId">Identificador de NumeracionAhorros</param>
        public void EliminarNumeracionAhorros(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DANumeracionAhorros.EliminarNumeracionAhorros(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("NumeracionAhorrosBusiness", "EliminarNumeracionAhorros", ex);
            }
        }

        /// <summary>
        /// Obtiene un NumeracionAhorros
        /// </summary>
        /// <param name="pId">Identificador de NumeracionAhorros</param>
        /// <returns>Entidad NumeracionAhorros</returns>
        public NumeracionAhorros ConsultarNumeracionAhorros(Int64 pId, Usuario vUsuario)
        {
            try
            {
                NumeracionAhorros NumeracionAhorros = new NumeracionAhorros();
                NumeracionAhorros = DANumeracionAhorros.ConsultarNumeracionAhorros(pId, vUsuario);                
                return NumeracionAhorros;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("NumeracionAhorrosBusiness", "ConsultarNumeracionAhorros", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pNumeracionAhorros">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de NumeracionAhorros obtenidos</returns>
        public List<NumeracionAhorros> ListarNumeracionAhorros(NumeracionAhorros pNumeracionAhorros, Usuario pUsuario)
        {
            try
            {
                return DANumeracionAhorros.ListarNumeracionAhorros(pNumeracionAhorros, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("NumeracionAhorrosBusiness", "ListarNumeracionAhorros", ex);
                return null;
            }
        }

        public Boolean GeneraNumeroCuenta(Usuario pUsuario)
        {
            try
            {
                General eGeneral = new General();
                eGeneral = DAGeneral.ConsultarGeneral(580, pUsuario);
                if (eGeneral.codigo != null && eGeneral.valor == "1")
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
    
        
    }
}