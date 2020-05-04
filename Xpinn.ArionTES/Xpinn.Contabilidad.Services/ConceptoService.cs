using System;
using System.Collections.Generic;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Contabilidad.Business;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.Contabilidad.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ConceptoService
    {
        private ConceptoBusiness BOConcepto;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Concepto
        /// </summary>
        public ConceptoService()
        {
            BOConcepto = new ConceptoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "30704"; } }

        /// <summary>
        /// Servicio para crear Concepto
        /// </summary>
        /// <param name="pEntity">Entidad Concepto</param>
        /// <returns>Entidad Concepto creada</returns>
        public Concepto CrearConcepto(Concepto vConcepto, Usuario pUsuario)
        {
            try
            {
                return BOConcepto.CrearConcepto(vConcepto, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConceptoService", "CrearConcepto", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar Concepto
        /// </summary>
        /// <param name="pConcepto">Entidad Concepto</param>
        /// <returns>Entidad Concepto modificada</returns>
        public Concepto ModificarConcepto(Concepto vConcepto, Usuario pUsuario)
        {
            try
            {
                return BOConcepto.ModificarConcepto(vConcepto, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConceptoService", "ModificarConcepto", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar Concepto
        /// </summary>
        /// <param name="pId">identificador de Concepto</param>
        public void EliminarConcepto(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOConcepto.EliminarConcepto(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarConcepto", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener Concepto
        /// </summary>
        /// <param name="pId">identificador de Concepto</param>
        /// <returns>Entidad Concepto</returns>
        public Concepto ConsultarConcepto(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOConcepto.ConsultarConcepto(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConceptoService", "ConsultarConcepto", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de Conceptos a partir de unos filtros
        /// </summary>
        /// <param name="pConcepto">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Concepto obtenidos</returns>
        public List<Concepto> ListarConcepto(Concepto vConcepto, Usuario pUsuario)
        {
            try
            {
                return BOConcepto.ListarConcepto(vConcepto, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConceptoService", "ListarConcepto", ex);
                return null;
            }
        }


        /// <summary>
        /// Valida la existencia y acceso del Concepto en el sistema
        /// </summary>
        /// <param name="pConcepto">nombre de Concepto</param>
        /// <param name="pPassword">clave de acceso</param>
        /// <returns>Entidad Concepto</returns>
        public Concepto ValidarConcepto(Int64 pConcepto, Usuario pUsuario)
        {
            try
            {
                return BOConcepto.ValidarConcepto(pConcepto, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConceptoService", "ValidarConcepto", ex);
                return null;
            }
        }

    }
}