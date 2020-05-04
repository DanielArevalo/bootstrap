using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Aportes.Data;
using Xpinn.Aportes.Entities;
using System.IO;

namespace Xpinn.Aportes.Business
{
    /// <summary>
    /// Objeto de negocio para LineaAporte
    /// </summary>
    public class GrupoLineaAporteBusiness : GlobalBusiness
    {
        private GrupoLineaAporteData DALineaAporte;

        /// <summary>
        /// Constructor del objeto de negocio para LineaAporte
        /// </summary>
        public GrupoLineaAporteBusiness()
        {
            DALineaAporte = new GrupoLineaAporteData();
        }

        /// <summary>
        /// Crea un LineaAporte
        /// </summary>
        /// <param name="pLineaAporte">Entidad LineaAporte</param>
        /// <returns>Entidad LineaAporte creada</returns>
        public GrupoLineaAporte CrearLineaAporte(GrupoLineaAporte pLineaAporte, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pLineaAporte = DALineaAporte.CrearLineaAporte(pLineaAporte, pUsuario);

                    ts.Complete();
                }

                return pLineaAporte;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaAporteBusiness", "CrearLineaAporte", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un LineaAporte
        /// </summary>
        /// <param name="pLineaAporte">Entidad LineaAporte</param>
        /// <returns>Entidad LineaAporte modificada</returns>
        public GrupoLineaAporte ModificarLineaAporte(GrupoLineaAporte pLineaAporte, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pLineaAporte = DALineaAporte.ModificarLineaAporte(pLineaAporte, pUsuario);

                    ts.Complete();
                }

                return pLineaAporte;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaAporteBusiness", "ModificarLineaAporte", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un LineaAporte
        /// </summary>
        /// <param name="pId">Identificador de LineaAporte</param>
        public void EliminarLineaAporte(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DALineaAporte.EliminarLineaAporte(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaAporteBusiness", "EliminarLineaAporte", ex);
            }
        }

        /// <summary>
        /// Obtiene un LineaAporte
        /// </summary>
        /// <param name="pId">Identificador de LineaAporte</param>
        /// <returns>Entidad LineaAporte</returns>
        public GrupoLineaAporte ConsultarLineaAporte(Int64 pId, Usuario vUsuario)
        {
            try
            {
                GrupoLineaAporte LineaAporte = new GrupoLineaAporte();

                LineaAporte = DALineaAporte.ConsultarLineaAporte(pId, vUsuario);

                return LineaAporte;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaAporteBusiness", "ConsultarLineaAporte", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pLineaAporte">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de LineaAporte obtenidos</returns>
        public List<GrupoLineaAporte> ListarLineaAporte(GrupoLineaAporte pLineaAporte, Usuario pUsuario)
        {
            try
            {
                return DALineaAporte.ListarLineaAporte(pLineaAporte, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaAporteBusiness", "ListarLineaAporte", ex);
                return null;
            }
        }


        /// <summary>
        /// Crea un GrupoAporte
        /// </summary>
        /// <param name="pGrupoAporte">Entidad GrupoAporte</param>
        /// <returns>Entidad GrupoAporte creada</returns>
        public void CrearGrupoAporte(List<GrupoLineaAporte> lstGrupos, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    foreach (GrupoLineaAporte pGrupoAporte in lstGrupos)
                    {
                        GrupoLineaAporte pEntidad = new GrupoLineaAporte();
                        pEntidad = DALineaAporte.CrearGrupoAporte(pGrupoAporte, pUsuario);
                    }
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GrupoAporteBusiness", "CrearGrupoAporte", ex);
            }
        }

        /// <summary>
        /// Modifica un GrupoAporte
        /// </summary>
        /// <param name="pGrupoAporte">Entidad GrupoAporte</param>
        /// <returns>Entidad GrupoAporte modificada</returns>
        public void ModificarGrupoAporte(List<GrupoLineaAporte> lstGrupos, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    foreach (GrupoLineaAporte pGrupoAporte in lstGrupos)
                    {
                        GrupoLineaAporte pEntidad = new GrupoLineaAporte();

                        //GENERAR CONSULTA
                        pEntidad.idgrupo = pGrupoAporte.idgrupo;
                        pEntidad.cod_linea_aporte = pGrupoAporte.cod_linea_aporte;
                        pEntidad = DALineaAporte.ConsultarGrupoAporteDetalle(pEntidad, pUsuario);
                        bool rpta = false;
                        rpta = pEntidad.rpta;
                        if (rpta == true)
                            pEntidad = DALineaAporte.ModificarGrupoAporte(pGrupoAporte, pUsuario);
                        else
                            pEntidad = DALineaAporte.CrearGrupoAporte(pGrupoAporte, pUsuario);
                    }
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GrupoAporteBusiness", "ModificarGrupoAporte", ex);
            }
        }

        /// <summary>
        /// Elimina un GrupoAporte
        /// </summary>
        /// <param name="pId">Identificador de GrupoAporte</param>
        public void EliminarGrupoAporte(long pId, long pCod_linea, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DALineaAporte.EliminarGrupoAporte(pId, pCod_linea, vUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                //BOExcepcion.Throw("GrupoAporteBusiness", "EliminarGrupoAporte", ex);
            }
        }

        /// <summary>
        /// Obtiene un GrupoAporte
        /// </summary>
        /// <param name="pId">Identificador de GrupoAporte</param>
        /// <returns>Entidad GrupoAporte</returns>
        public GrupoLineaAporte ConsultarGrupoAporte(Int64 pId, Usuario vUsuario)
        {
            try
            {
                GrupoLineaAporte GrupoAporte = new GrupoLineaAporte();

                GrupoAporte = DALineaAporte.ConsultarGrupoAporte(pId, vUsuario);

                return GrupoAporte;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GrupoAporteBusiness", "ConsultarGrupoAporte", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pGrupoAporte">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de GrupoAporte obtenidos</returns>
        public List<GrupoLineaAporte> ListarGrupoAporte(GrupoLineaAporte pGrupoAporte, Usuario pUsuario)
        {
            try
            {
                return DALineaAporte.ListarGrupoAporte(pGrupoAporte, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GrupoAporteBusiness", "ListarGrupoAporte", ex);
                return null;
            }
        }
        
        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pGrupoAporte">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de GrupoAporte obtenidos</returns>
        public List<GrupoLineaAporte> ListarGrupoAporteDetalle(GrupoLineaAporte pGrupoAporte, Usuario pUsuario)
        {
            try
            {
                return DALineaAporte.ListarGrupoAporteDetalle(pGrupoAporte, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GrupoAporteBusiness", "ListarGrupoAporteEdit", ex);
                return null;
            }
        }
        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pGrupoAporte">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de GrupoAporte obtenidos</returns>
        public List<GrupoLineaAporte> ListarGrupoAporteEdit(GrupoLineaAporte pGrupoAporte, Usuario pUsuario)
        {
            try
            {
                return DALineaAporte.ListarGrupoAporteEdit(pGrupoAporte, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GrupoAporteBusiness", "ListarGrupoAporteEdit", ex);
                return null;
            }
        }
        
        /// <summary>
        /// Obtiene un Aporte
        /// </summary>
        /// <param name="pId">Identificador de Aporte</param>
        /// <returns>Entidad Aporte</returns>
        public GrupoLineaAporte ConsultarMaxGrupoAporte(Usuario vUsuario)
        {
            try
            {
                GrupoLineaAporte Aporte = new GrupoLineaAporte();

                Aporte = DALineaAporte.ConsultarMaxGrupoAporte(vUsuario);

                return Aporte;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "ConsultarMaxGrupoAporte", ex);
                return null;
            }
        }

        public List<GrupoLineaAporte> ListaGrupoLineaAporte(string pFiltro, Usuario vUsuario)
        {
            List<GrupoLineaAporte> lstLineas = null;
            try
            {
                lstLineas = DALineaAporte.ListaGrupoLineaAporte(pFiltro, vUsuario);
                return lstLineas;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "ListaGrupoLineaAporte", ex);
                return null;
            }
        }

        // Anderson Acuña--Cargue Masivo        
        private StreamReader strReader;
        public void CargaAportes(ref string pError, string sformato_fecha, Stream pstream, ref List<GrupoLineaAporte> lstAportes, ref List<ErroresCargaAportes> plstErrores, Usuario pUsuario)
        {
            Configuracion conf = new Configuracion();
            string sSeparadorDecimal = conf.ObtenerSeparadorDecimalConfig();

            string readLine;

            // Inicializar control de errores
            RegistrarError(-1, "", "", "", ref plstErrores);

            try
            {
                using (strReader = new StreamReader(pstream))
                {
                    //recorriendo las filas del archivo
                    while (strReader.Peek() >= 0)
                    {
                        //BAJANDO LA FILA A UNA VARIABLE
                        readLine = strReader.ReadLine();
                        string Separador = "|";

                        //Separando la data a un array
                        string[] arrayline = readLine.Split(Convert.ToChar(Separador));
                        int contadorreg = 0;

                        GrupoLineaAporte pEntidad = new GrupoLineaAporte();
                        for (int i = 0; i <= 2; i++)
                        {
                            if (i == 0) { try { pEntidad.identificacion = Convert.ToString(arrayline[i].ToString().Trim()); } catch (Exception ex) { RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                            if (i == 1) { try { pEntidad.numero_aporte = Convert.ToInt64(arrayline[i].ToString().Trim()); } catch (Exception ex) { RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                            if (i == 2) { try { pEntidad.cuota = Convert.ToDecimal(arrayline[i].ToString().Trim()); } catch (Exception ex) { RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                            pEntidad.fechaAct = DateTime.Now;


                            //pEntidad.cod_usuario = Convert.ToInt32(pUsuario.codusuario);
                            //pEntidad.cod_empresa = pUsuario.idEmpresa;

                            contadorreg++;
                        }
                        lstAportes.Add(pEntidad);
                    }
                }
            }
            catch (IOException ex)
            {
                pError = ex.Message;
            }
        }
        // Anderson Acuña--Guardar Masivo 
          
        public void CrearImportacion(DateTime pFechaCarga, ref string pError, List<GrupoLineaAporte> lstAporte, Usuario pUsuario, string cod_linea, ref List<Int64> lst_Num_Apor)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (lstAporte != null && lstAporte.Count > 0)
                    {
                        foreach (GrupoLineaAporte nAporte in lstAporte)
                        {
                            //nAporte.numero_aporte = 0;
                            nAporte.cod_linea_aporte = Convert.ToInt64(cod_linea);
                            //nAporte.fechaAct = pFechaCarga;                            
                            GrupoLineaAporte pEntidad = new GrupoLineaAporte();
                            pEntidad = DALineaAporte.CrearAportes(nAporte, pUsuario);
                            if (pEntidad.numero_aporteR != 0)
                            {
                                lst_Num_Apor.Add(pEntidad.numero_aporteR);
                            }   
                                                   
                        }
                        
                    }
                    
                    ts.Complete();
                }
               
            }
            catch (Exception ex)
            {
                pError = ex.Message;
            }
        }

        
        public void RegistrarError(int pNumeroLinea, string pRegistro, string pError, string pDato, ref List<ErroresCargaAportes> plstErrores)
        {
            if (pNumeroLinea == -1)
            {
                plstErrores.Clear();
                return;
            }
            ErroresCargaAportes registro = new ErroresCargaAportes();
            registro.numero_registro = pNumeroLinea.ToString();
            registro.datos = pDato;
            registro.error = " Campo No.:" + pRegistro + " Error:" + pError;
            plstErrores.Add(registro);
        }


    }
}