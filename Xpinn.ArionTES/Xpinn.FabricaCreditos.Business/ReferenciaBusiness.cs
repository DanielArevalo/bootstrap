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
    /// Objeto de negocio para Referencia
    /// </summary>
    public class ReferenciaBusiness : GlobalData
    {
        private ReferenciaData DAReferencia;

        /// <summary>
        /// Constructor del objeto de negocio para Referencia
        /// </summary>
        public ReferenciaBusiness()
        {
            DAReferencia = new ReferenciaData();
        }

        /// <summary>
        /// Crea un Referencia
        /// </summary>
        /// <param name="pReferencia">Entidad Referencia</param>
        /// <returns>Entidad Referencia creada</returns>
        public Referencia CrearReferencia(Referencia pReferencia, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pReferencia = DAReferencia.CrearReferencia(pReferencia, pUsuario);

                    ts.Complete();
                }

                return pReferencia;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReferenciaBusiness", "CrearReferencia", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un Referencia
        /// </summary>
        /// <param name="pReferencia">Entidad Referencia</param>
        /// <returns>Entidad Referencia modificada</returns>
        public Referencia ModificarReferencia(Referencia pReferencia, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pReferencia = DAReferencia.ModificarReferencia(pReferencia, pUsuario);

                    ts.Complete();
                }

                return pReferencia;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReferenciaBusiness", "ModificarReferencia", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un Referencia
        /// </summary>
        /// <param name="pId">Identificador de Referencia</param>
        public void EliminarReferencia(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAReferencia.EliminarReferencia(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReferenciaBusiness", "EliminarReferencia", ex);
            }
        }

        /// <summary>
        /// Obtiene un Referencia
        /// </summary>
        /// <param name="pId">Identificador de Referencia</param>
        /// <returns>Entidad Referencia</returns>
        public Referencia ConsultarReferencia(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAReferencia.ConsultarReferencia(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReferenciaBusiness", "ConsultarReferencia", ex);
                return null;
            }
        }


        public Referencia ConsultarReferenciacion(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAReferencia.ConsultarReferenciacion(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReferenciaBusiness", "ConsultarReferencia", ex);
                return null;
            }
        }

        public Referencia ConsultarDatosReferenciacion(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAReferencia.ConsultardatosReferenciacion(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReferenciaBusiness", "ConsultarReferencia", ex);
                return null;
            }
        }
        public Referencia ConsultarCorreo(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAReferencia.ConsultarCorreo(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReferenciaBusiness", "ConsultarReferencia", ex);
                return null;
            }
        }

        public List<Referencia> Listarrefereciados(string identificacion, Usuario pUsuario)
        {

            return DAReferencia.Listarrefereciados(identificacion, pUsuario);

        }

        public List<Referencia> Listardatosrefereciacion(string identificacion, Usuario pUsuario, int referencia)
        {

            return DAReferencia.Listardatosrefereciacion (identificacion, pUsuario, referencia);

        }

        public void guardar(long CODREFERENCIA, int TIPO_REFERENCIACION, int NUM_PREGUNTA, int RESPPUESTA, long REFERENCIADOR, string NUMERO_RADICACION, long COD_PERSONA, string OBSERVACIONES, Usuario pUsuario)
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {
                DAReferencia.guardar(CODREFERENCIA, TIPO_REFERENCIACION, NUM_PREGUNTA, RESPPUESTA, REFERENCIADOR, NUMERO_RADICACION, COD_PERSONA, OBSERVACIONES, pUsuario);
                ts.Complete();
            }
        
        }

        public void guardarpregunta(long CODREFERENCIA, int TIPO_REFERENCIACION, int NUM_PREGUNTA, int RESPPUESTA, long REFERENCIADOR, string NUMERO_RADICACION, long COD_PERSONA, string OBSERVACIONES, Usuario pUsuario)
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {
                DAReferencia.guardarpregunta(CODREFERENCIA, TIPO_REFERENCIACION, NUM_PREGUNTA, RESPPUESTA, REFERENCIADOR, NUMERO_RADICACION, COD_PERSONA, OBSERVACIONES, pUsuario);
                ts.Complete();
            }
          
        }

        public void modifiar(int NUM_PREGUNTA, int RESPPUESTA, string OBSERVACIONES, string NUMERO_RADICACION, int TIPO_REFERENCIACION, Usuario pUsuario)
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {
                DAReferencia.modificar( NUM_PREGUNTA, RESPPUESTA,  OBSERVACIONES, NUMERO_RADICACION, TIPO_REFERENCIACION, pUsuario);
                ts.Complete();
            }
        }

        public Referencia ConsultarReferenciacionPersonas(string pId, Usuario pUsuario)
        {
            try
            {
                return DAReferencia.ConsultarReferenciacionPersonas(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReferenciaBusiness", "ConsultarReferencia", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pReferencia">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Referencia obtenidos</returns>
        public List<Referencia> ListarReferencia(Referencia pReferencia, Usuario pUsuario, String filtro)
        {
            try
            {
                return DAReferencia.ListarReferencia(pReferencia, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReferenciaBusiness", "ListarReferencia", ex);
                return null;
            }
        }


        public List<Referencia> ListarReferenciacion(Referencia pReferencia, Usuario pUsuario, String filtro)
        {
            try
            {
                return DAReferencia.ListarReferenciacion(pReferencia, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReferenciaBusiness", "ListarReferencia", ex);
                return null;
            }
        }

        public List<Referencia> ObservacionReferenciacion(Referencia vReferencia, Usuario pUsuario)
        {
            try
            {
                return DAReferencia.ObservacionReferenciacion(vReferencia, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReferenciaService", "ObservacionReferenciacion", ex);
                return null;
            }
        }

        /// <summary>
        /// Método para ver las referencias de un crédito
        /// </summary>
        /// <param name="pReferencia"></param>
        /// <param name="pUsuario"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public List<Referencia> ConsultarReferenciacionCredito(Int64 pnumero_radicacion, Usuario pUsuario)
        {
            try
            {
                return DAReferencia.ConsultarReferenciacionCredito(pnumero_radicacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReferenciaBusiness", "ConsultarReferenciacionCredito", ex);
                return null;
            }
        }

    }
}