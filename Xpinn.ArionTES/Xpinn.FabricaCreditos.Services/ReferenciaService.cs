using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Business;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ReferenciaService
    {
        private ReferenciaBusiness BOReferencia;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Referencia
        /// </summary>
        public ReferenciaService()
        {
            BOReferencia = new ReferenciaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "100139"; } }

        /// <summary>
        /// Servicio para crear Referencia
        /// </summary>
        /// <param name="pEntity">Entidad Referencia</param>
        /// <returns>Entidad Referencia creada</returns>
        public Referencia CrearReferencia(Referencia pReferencia, Usuario pUsuario)
        {
            try
            {
                return BOReferencia.CrearReferencia(pReferencia, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReferenciaService", "CrearReferencia", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar Referencia
        /// </summary>
        /// <param name="pReferencia">Entidad Referencia</param>
        /// <returns>Entidad Referencia modificada</returns>
        public Referencia ModificarReferencia(Referencia pReferencia, Usuario pUsuario)
        {
            try
            {
                return BOReferencia.ModificarReferencia(pReferencia, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReferenciaService", "ModificarReferencia", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar Referencia
        /// </summary>
        /// <param name="pId">identificador de Referencia</param>
        public void EliminarReferencia(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOReferencia.EliminarReferencia(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarReferencia", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener Referencia
        /// </summary>
        /// <param name="pId">identificador de Referencia</param>
        /// <returns>Entidad Referencia</returns>
        public Referencia ConsultarReferencia(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOReferencia.ConsultarReferencia(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReferenciaService", "ConsultarReferencia", ex);
                return null;
            }
        }

        public Referencia ConsultarReferenciacion(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOReferencia.ConsultarReferenciacion(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReferenciaService", "ConsultarReferencia", ex);
                return null;
            }
        }

        public Referencia ConsultarDatosReferenciacion(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOReferencia.ConsultarDatosReferenciacion(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReferenciaService", "ConsultarDatosReferenciacion", ex);
                return null;
            }
        }
        public Referencia ConsultarCorreo(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOReferencia.ConsultarCorreo(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReferenciaService", "ConsultarDatosReferenciacion", ex);
                return null;
            }
        }

        public List<Referencia> Listarrefereciados(string identificacion, Usuario pUsuario)
        {

            return BOReferencia.Listarrefereciados(identificacion, pUsuario);
           
        }
        public List<Referencia> Listardatosrefereciacion(string identificacion, Usuario pUsuario, int referencia)
        {

            return BOReferencia.Listardatosrefereciacion (identificacion, pUsuario, referencia);

        }
        public void guardar(long CODREFERENCIA, int TIPO_REFERENCIACION, int NUM_PREGUNTA, int RESPPUESTA, long REFERENCIADOR, string NUMERO_RADICACION, long COD_PERSONA, string OBSERVACIONES, Usuario pUsuario)
        {

            BOReferencia.guardar(CODREFERENCIA,  TIPO_REFERENCIACION,  NUM_PREGUNTA,  RESPPUESTA,  REFERENCIADOR,  NUMERO_RADICACION,  COD_PERSONA,  OBSERVACIONES, pUsuario);
        
        }
        public void guardarpregunta(long CODREFERENCIA, int TIPO_REFERENCIACION, int NUM_PREGUNTA, int RESPPUESTA, long REFERENCIADOR, string NUMERO_RADICACION, long COD_PERSONA, string OBSERVACIONES, Usuario pUsuario)
        {

            BOReferencia.guardarpregunta(CODREFERENCIA, TIPO_REFERENCIACION, NUM_PREGUNTA, RESPPUESTA, REFERENCIADOR, NUMERO_RADICACION, COD_PERSONA, OBSERVACIONES, pUsuario);

        }

        public void modificar(int NUM_PREGUNTA, int RESPPUESTA, string OBSERVACIONES, string NUMERO_RADICACION, int TIPO_REFERENCIACION, Usuario pUsuario)
        {

            BOReferencia.modifiar( NUM_PREGUNTA, RESPPUESTA,  OBSERVACIONES, NUMERO_RADICACION, TIPO_REFERENCIACION, pUsuario);

        }

        public Referencia ConsultarReferenciacionPersonas(string pId, Usuario pUsuario)
        {
            try
            {
                return BOReferencia.ConsultarReferenciacionPersonas(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReferenciaService", "ConsultarReferencia", ex);
                return null;
            }
        }





        /// <summary>
        /// Servicio para obtener lista de Referencias a partir de unos filtros
        /// </summary>
        /// <param name="pReferencia">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Referencia obtenidos</returns>
        public List<Referencia> ListarReferencia(Referencia pReferencia, Usuario pUsuario, String filtro)
        {
            try
            {
                return BOReferencia.ListarReferencia(pReferencia, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReferenciaService", "ListarReferencia", ex);
                return null;
            }
        }



        public List<Referencia> ListarReferenciacion(Referencia pReferencia, Usuario pUsuario, String filtro)
        {
            try
            {
                return BOReferencia.ListarReferenciacion(pReferencia, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReferenciaService", "ListarReferencia", ex);
                return null;
            }
        }


        public List<Referencia> ObservacionReferenciacion(Referencia vReferencia, Usuario pUsuario)
        {
            try
            {
                return BOReferencia.ObservacionReferenciacion(vReferencia, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReferenciaService", "ObservacionReferenciacion", ex);
                return null;
            }
        }


        public List<Referencia> ConsultarReferenciacionCredito(Int64 pnumero_radicacion, Usuario pusuario)
        {
            try
            {
                return BOReferencia.ConsultarReferenciacionCredito(pnumero_radicacion, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReferenciaService", "ConsultarReferenciacionCredito", ex);
                return null;
            }
        }



    }
}