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
    /// Objeto de negocio para codeudores
    /// </summary>
    public class codeudoresBusiness : GlobalBusiness
    {
        private codeudoresData DAcodeudores;

        /// <summary>
        /// Constructor del objeto de negocio para codeudores
        /// </summary>
        public codeudoresBusiness()
        {
            DAcodeudores = new codeudoresData();
        }

        /// <summary>
        /// Crea un codeudores
        /// </summary>
        /// <param name="pcodeudores">Entidad codeudores</param>
        /// <returns>Entidad codeudores creada</returns>
        public void Crearcodeudores(List<codeudores> lstCodeudores, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (lstCodeudores != null && lstCodeudores.Count > 0)
                        foreach (codeudores pCodeudores in lstCodeudores)
                        {
                            codeudores entidad = new codeudores();
                            codeudores entidad2 = new codeudores();
                            Int64 num_radicacion = pCodeudores.numero_radicacion;
                            pCodeudores.numero_radicacion = pCodeudores.numero_solicitud;
                            entidad = DAcodeudores.Crearcodeudores(pCodeudores, pUsuario); //TABLA SOLICRED_CODEUDORES
                            if (num_radicacion != 0)
                            {
                                pCodeudores.idcodeud = 0;
                                pCodeudores.numero_radicacion = num_radicacion;
                                entidad2 = DAcodeudores.CrearCodeudoresCredito(pCodeudores, pUsuario); //TABLA CODEUDORES
                            }
                        }
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("codeudoresBusiness", "Crearcodeudores", ex);
            }
        }


        public void CrearCodeudoresDesdeFuncionImportacion(codeudores Codeudores, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (Codeudores.numero_radicacion != 0 && Codeudores.codpersona != 0)
                    {
                        Codeudores = DAcodeudores.CrearCodeudoresCredito(Codeudores, pUsuario); //TABLA CODEUDORES
                    }
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("codeudoresBusiness", "CrearCodeudoresDesdeFuncionImportacion", ex);
            }
        }


        /// <summary>
        /// Modifica un codeudores
        /// </summary>
        /// <param name="pcodeudores">Entidad codeudores</param>
        /// <returns>Entidad codeudores modificada</returns>
        public codeudores Modificarcodeudores(codeudores pcodeudores, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pcodeudores = DAcodeudores.Modificarcodeudores(pcodeudores, pUsuario);

                    ts.Complete();
                }

                return pcodeudores;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("codeudoresBusiness", "Modificarcodeudores", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un codeudores
        /// </summary>
        /// <param name="pId">Identificador de codeudores</param>
        public void EliminarcodeudoresSol(Int64 pId, Int64 solicitud, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAcodeudores.EliminarcodeudoresSol(pId, solicitud, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("codeudoresBusiness", "Eliminarcodeudores", ex);
            }
        }

        // <summary>
        /// Elimina un codeudores
        /// </summary>
        /// <param name="pId">Identificador de codeudores</param>
        public void EliminarcodeudoresCred(Int64 pId, Int64 radicado, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAcodeudores.EliminarcodeudoresCred(pId, radicado, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("codeudoresBusiness", "EliminarcodeudoresCred", ex);
            }
        }



        /// <summary>
        /// Obtiene un codeudores
        /// </summary>
        /// <param name="pId">Identificador de codeudores</param>
        /// <returns>Entidad codeudores</returns>
        public codeudores Consultarcodeudores(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAcodeudores.Consultarcodeudores(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("codeudoresBusiness", "Consultarcodeudores", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene un codeudores
        /// </summary>
        /// <param name="pId">Identificador de codeudores</param>
        /// <returns>Entidad codeudores</returns>
        public codeudores ConsultarDatosCodeudorRepo(string pId, Usuario pUsuario)
        {
            try
            {
                return DAcodeudores.ConsultarDatosCodeudorRepo(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("codeudoresBusiness", "ConsultarDatosCodeudorRepo", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pcodeudores">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de codeudores obtenidos</returns>
        public List<codeudores> Listarcodeudores(codeudores pcodeudores, Usuario pUsuario)
        {
            try
            {
                return DAcodeudores.Listarcodeudores(pcodeudores, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("codeudoresBusiness", "Listarcodeudores", ex);
                return null;
            }
        }
        public List<codeudores> ListarcodeudoresCredito(String radicado, Usuario pUsuario)
        {
            try
            {
                return DAcodeudores.ListarCodeudoresCredito(radicado, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("codeudoresBusiness", "ListarcodeudoresCredito", ex);
                return null;
            }
        }



        public codeudores ConsultarDatosCodeudor(string pId, Usuario pUsuario)
        {
            try
            {
                return DAcodeudores.ConsultarDatosCodeudor(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("codeudoresBusiness", "ConsultarDatosCodeudor", ex);
                return null;
            }
        }

        public codeudores ValidarCodeudor(codeudores pCodeudor, Usuario pUsuario, ref string sError)
        {
            try
            {
                return DAcodeudores.ValidarCodeudor(pCodeudor, pUsuario, ref sError);
            }
            catch
            {
                return pCodeudor;
            }
        }


        public codeudores ConsultarCantidadCodeudores(string pId, Usuario pUsuario)
        {
            try
            {
                return DAcodeudores.ConsultarCantidadCodeudores(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("codeudoresBusiness", "ConsultarCantidadCodeudores", ex);
                return null;
            }
        }

    }
}