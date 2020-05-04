using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Contabilidad.Data;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.Contabilidad.Business
{
    /// <summary>
    /// Objeto de negocio para concepto
    /// </summary>
    public class ConceptoBusiness : GlobalBusiness
    {
        private ConceptoData DAconcepto;

        /// <summary>
        /// Constructor del objeto de negocio para concepto
        /// </summary>
        public ConceptoBusiness()
        {
            DAconcepto = new ConceptoData();
        }

        /// <summary>
        /// Crea un concepto
        /// </summary>
        /// <param name="pconcepto">Entidad concepto</param>
        /// <returns>Entidad concepto creada</returns>
        public Concepto CrearConcepto(Concepto pconcepto, Usuario vusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pconcepto = DAconcepto.CrearConcepto(pconcepto, vusuario);

                    ts.Complete();
                }

                return pconcepto;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("conceptoBusiness", "Crearconcepto", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un concepto
        /// </summary>
        /// <param name="pconcepto">Entidad concepto</param>
        /// <returns>Entidad concepto modificada</returns>
        public Concepto ModificarConcepto(Concepto pconcepto, Usuario vusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pconcepto = DAconcepto.ModificarConcepto(pconcepto, vusuario);

                    ts.Complete();
                }

                return pconcepto;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("conceptoBusiness", "Modificarconcepto", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un concepto
        /// </summary>
        /// <param name="pId">Identificador de concepto</param>
        public void EliminarConcepto(Int64 pId, Usuario vusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAconcepto.EliminarConcepto(pId, vusuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("conceptoBusiness", "Eliminarconcepto", ex);
            }
        }

        /// <summary>
        /// Obtiene un concepto
        /// </summary>
        /// <param name="pId">Identificador de concepto</param>
        /// <returns>Entidad concepto</returns>
        public Concepto ConsultarConcepto(Int64 pId, Usuario vusuario)
        {
            try
            {
                Concepto concepto = new Concepto();

                concepto = DAconcepto.ConsultarConcepto(pId, vusuario);

                return concepto;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("conceptoBusiness", "Consultarconcepto", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pconcepto">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de concepto obtenidos</returns>
        public List<Concepto> ListarConcepto(Concepto pconcepto, Usuario vUsuario)
        {
            try
            {
                return DAconcepto.ListarConcepto(pconcepto, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("conceptoBusiness", "Listarconcepto", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene un concepto
        /// </summary>
        /// <param name="pId">Identificador de concepto</param>
        /// <returns>Entidad concepto</returns>
        public Concepto ValidarConcepto(Int64 pconcepto, Usuario pUsuario)
        {
            try
            {
                Concepto concepto = new Concepto();

                try
                {
                    concepto = DAconcepto.ValidarConcepto(pconcepto, pUsuario);
                }
                catch (ExceptionBusiness ex)
                {
                    if (ex.Message.Contains("El registro no existe. Verifique por favor."))
                        throw new ExceptionBusiness("Tipo de Comprobante no encontrado.");
                    else
                        throw new ExceptionBusiness(ex.Message);
                }

                return concepto;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("conceptoBusiness", "Validarconcepto", ex);
                return null;
            }
        }


    }
}