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
    /// Objeto de negocio para PreAnalisis
    /// </summary>
    public class PreAnalisisBusiness : GlobalData
    {
        private PreAnalisisData DAprograma;

        /// <summary>
        /// Constructor del objeto de negocio para Programa
        /// </summary>
        public PreAnalisisBusiness()
        {
            DAprograma = new PreAnalisisData();
        }

        /// <summary>
        /// Crea un Parametro
        /// </summary>
        /// <param name="pEntity">Entidad Parametro</param>
        /// <returns>Entidad creada</returns>
        public Parametrizar CrearPrograma(Parametrizar pPrograma, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pPrograma = DAprograma.CrearPrograma(pPrograma, pUsuario);

                    ts.Complete();
                }

                return pPrograma;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProgramaBusiness", "CrearPrograma", ex);
                return null;
            }
        }


        /// <summary>
        /// Crea una Central
        /// </summary>
        /// <param name="pEntity">Entidad Central</param>
        /// <returns>Entidad creada</returns>
        public Parametrizar CrearCentral(Parametrizar pCentral, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    //Calculo IVA
                    //if (pCentral.cobra == "S")
                    //    pCentral.valoriva = (pCentral.valor * pCentral.porcentaje) / 100;
                    pCentral.valoriva = pCentral.cobra == "S" ? (pCentral.valor * pCentral.porcentaje) / 100 : 0;
                    pCentral = DAprograma.CrearCentral(pCentral, pUsuario);
                    ts.Complete();
                }

                return pCentral;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PreAnalisisBusiness", "CrearCentral", ex);
                return null;
            }
        }






        /// <summary>
        /// Modifica un Parametro
        /// </summary>
        /// <param name="pEntity">Entidad Parametro</param>
        /// <returns>Entidad modificada</returns>
        public Parametrizar ModificarPrograma(Parametrizar pPrograma, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pPrograma = DAprograma.ModificarPrograma(pPrograma, pUsuario);

                    ts.Complete();
                }

                return pPrograma;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PreAnalisisBusiness", "ModificarParametro", ex);
                return null;
            }

        }


        /// <summary>
        /// Modifica una Central
        /// </summary>
        /// <param name="pEntity">Entidad Central</param>
        /// <returns>Entidad modificada</returns>
        public Parametrizar ModificarCentral(Parametrizar pCentral, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    //if (pCentral.cobra == "S")
                    //    pCentral.valoriva = (pCentral.valor * pCentral.porcentaje) / 100;

                    pCentral.valoriva = pCentral.cobra == "S" ? (pCentral.valor * pCentral.porcentaje) / 100 : 0;
                    pCentral = DAprograma.ModificarCentral(pCentral, pUsuario);
                    ts.Complete();
                }

                return pCentral;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PreAnalisisBusiness", "ModificarCentral", ex);
                return null;
            }

        }



        /// <summary>
        /// Elimina un Parametro
        /// </summary>
        /// <param name="pId">Identificador del Parametro</param>
        public void EliminarPrograma(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {

                    DAprograma.EliminarPrograma(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProgramaBusiness", "EliminarParametro", ex);
            }
        }


        /// <summary>
        /// Elimina una Central
        /// </summary>
        /// <param name="pId">Identificador de la Central</param>
        public void EliminarCentral(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {

                    DAprograma.EliminarCentral(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PreAnalisisBusiness", "EliminarCentral", ex);
            }
        }



        /// <summary>
        /// Obtiene un Parametro
        /// </summary>
        /// <param name="pId">Identificador del Parametro</param>
        /// <returns>Programa consultado</returns>
        public Parametrizar ConsultarPrograma(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAprograma.ConsultarPrograma(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PreAnalisisBusiness", "ConsultarParametro", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene una Central
        /// </summary>
        /// <param name="pId">Identificador de la Central</param>
        /// <returns>Programa consultado</returns>
        public Parametrizar ConsultarCentral(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAprograma.ConsultarCentral(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PreAnalisisBusiness", "ConsultarCentral", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene la lista de Parametros dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Parametros obtenidos</returns>
        public List<Parametrizar> ListarPrograma(Parametrizar pPrograma, Usuario pUsuario)
        {
            try
            {
                return DAprograma.ListarPrograma(pPrograma, pUsuario); 
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PreAnalisisBusiness", "ListarParametros", ex);
                return null;
            }
        }



        /// <summary>
        /// Obtiene la lista de Centrales dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Centrales obtenidas</returns>
        public List<Parametrizar> ListarCentrales(Parametrizar pPrograma, Usuario pUsuario)
        {
            try
            {
                return DAprograma.ListarCentrales(pPrograma, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PreAnalisisBusiness", "ListarCentrales", ex);
                return null;
            }
        }

        public Credito ConsultarPreAnalisis_credito(Credito pEntidad, Usuario vUsuario)
        {
            try
            {
                return DAprograma.ConsultarPreAnalisis_credito(pEntidad, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PreAnalisisBusiness", "ConsultarPreAnalisis_credito", ex);
                return null;
            }
        }


    }
}
