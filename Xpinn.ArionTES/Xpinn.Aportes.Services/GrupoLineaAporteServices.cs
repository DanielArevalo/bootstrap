using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Aportes.Business;
using Xpinn.Aportes.Entities;
using System.IO;

namespace Xpinn.Aportes.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class GrupoLineaAporteServices
    {
        private GrupoLineaAporteBusiness BOLineaAporte;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para LineaAporte
        /// </summary>
        public GrupoLineaAporteServices()
        {
            BOLineaAporte = new GrupoLineaAporteBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }
        public int CodigoGrupoAporte;
        public string CodigoPrograma { get { return "170203"; } }
        public string CodigoProgramaLineas { get { return "170202"; } }

        /// <summary>
        /// Servicio para crear LineaAporte
        /// </summary>
        /// <param name="pEntity">Entidad LineaAporte</param>
        /// <returns>Entidad LineaAporte creada</returns>
        public GrupoLineaAporte CrearLineaAporte(GrupoLineaAporte vLineaAporte, Usuario pUsuario)
        {
            try
            {
                return BOLineaAporte.CrearLineaAporte(vLineaAporte, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaAporteService", "CrearLineaAporte", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar LineaAporte
        /// </summary>
        /// <param name="pLineaAporte">Entidad LineaAporte</param>
        /// <returns>Entidad LineaAporte modificada</returns>
        public GrupoLineaAporte ModificarLineaAporte(GrupoLineaAporte vLineaAporte, Usuario pUsuario)
        {
            try
            {
                return BOLineaAporte.ModificarLineaAporte(vLineaAporte, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaAporteService", "ModificarLineaAporte", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar LineaAporte
        /// </summary>
        /// <param name="pId">identificador de LineaAporte</param>
        public void EliminarLineaAporte(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOLineaAporte.EliminarLineaAporte(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarLineaAporte", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener LineaAporte
        /// </summary>
        /// <param name="pId">identificador de LineaAporte</param>
        /// <returns>Entidad LineaAporte</returns>
        public GrupoLineaAporte ConsultarLineaAporte(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOLineaAporte.ConsultarLineaAporte(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaAporteService", "ConsultarLineaAporte", ex);
                return null;
            }
        }

        public List<GrupoLineaAporte> ListarLineaAporte(GrupoLineaAporte pLineaAporte, Usuario pUsuario)
        {
            try
            {
                return BOLineaAporte.ListarLineaAporte(pLineaAporte, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaAporteService", "ListarLineaAporte", ex);
                return null;
            }
        }


        /// <summary>
        /// Servicio para crear GrupoAporte
        /// </summary>
        /// <param name="pEntity">Entidad GrupoAporte</param>
        /// <returns>Entidad GrupoAporte creada</returns>
        public void CrearGrupoAporte(List<GrupoLineaAporte> lstGrupos, Usuario pUsuario)
        {
            try
            {
                BOLineaAporte.CrearGrupoAporte(lstGrupos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GrupoAporteService", "CrearGrupooAporte", ex);
            }
        }

        /// <summary>
        /// Servicio para modificar GrupoAporte
        /// </summary>
        /// <param name="pGrupoAporte">Entidad GrupoAporte</param>
        /// <returns>Entidad GrupoAporte modificada</returns>
        public void ModificarGrupoAporte(List<GrupoLineaAporte> lstGrupos, Usuario pUsuario)
        {
            try
            {
                BOLineaAporte.ModificarGrupoAporte(lstGrupos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GrupoAporteService", "ModificarGrupoAporte", ex);
            }
        }

        /// <summary>
        /// Servicio para Eliminar GrupoAporte
        /// </summary>
        /// <param name="pId">identificador de GrupoAporte</param>
        public void EliminarGrupoAporte(long pId, long pCod_linea, Usuario vUsuario)
        {
            try
            {
                BOLineaAporte.EliminarGrupoAporte(pId, pCod_linea, vUsuario);
            }
            catch (Exception ex)
            {
                //BOExcepcion.Throw("$Programa$Service", "EliminarGrupoAporte", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener GrupoAporte
        /// </summary>
        /// <param name="pId">identificador de GrupoAporte</param>
        /// <returns>Entidad GrupoAporte</returns>
        public GrupoLineaAporte ConsultarGrupoAporte(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOLineaAporte.ConsultarGrupoAporte(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GrupoAporteService", "ConsultarGrupoAporte", ex);
                return null;
            }
        }

        public List<GrupoLineaAporte> ListarGrupoAporte(GrupoLineaAporte pGrupoAporte, Usuario pUsuario)
        {
            try
            {
                return BOLineaAporte.ListarGrupoAporte(pGrupoAporte, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GrupoAporteService", "ListarTipoComp", ex);
                return null;
            }
        }
        public List<GrupoLineaAporte> ListarGrupoAporteDetalle(GrupoLineaAporte pGrupoAporte, Usuario pUsuario)
        {
            try
            {
                return BOLineaAporte.ListarGrupoAporteDetalle(pGrupoAporte, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GrupoAporteService", "ListarGrupoAporteDetalle", ex);
                return null;
            }
        }
        public List<GrupoLineaAporte> ListarGrupoAporteEdit(GrupoLineaAporte pGrupoAporte, Usuario pUsuario)
        {
            try
            {
                return BOLineaAporte.ListarGrupoAporteEdit(pGrupoAporte, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GrupoAporteService", "ListarGrupoAporteEdit", ex);
                return null;
            }
        }
        /// Servicio para obtener MaxAporte
        /// </summary>
        /// <param name="pId">identificador de Aporte</param>
        /// <returns>Entidad Aporte</returns>
        /// 

        public GrupoLineaAporte ConsultarMaxGrupoAporte(Usuario pUsuario)
        {
            try
            {
                return BOLineaAporte.ConsultarMaxGrupoAporte(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteService", "ConsultarMaxGrupoAporte", ex);
                return null;
            }
        }

        public List<GrupoLineaAporte> ListaGrupoLineaAporte(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return BOLineaAporte.ListaGrupoLineaAporte(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteService", "ListaGrupoLineaAporte", ex);
                return null;
            }
        }

        //Anderson Cargue Masivo
        public void CargaAportes(ref string pError, string sformato_fecha, Stream pstream, ref List<GrupoLineaAporte> lstAportes, ref List<ErroresCargaAportes> plstErrores, Usuario pUsuario)
        {
            try
            {
                BOLineaAporte.CargaAportes(ref pError, sformato_fecha, pstream, ref lstAportes, ref plstErrores, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteService", "CargaAportes", ex);
            }
        }


        //Anderson Guardar Aporte Masivo
        public void CrearImportacion(DateTime pFechaCarga, ref string pError, List<GrupoLineaAporte> lstAporte, Usuario pUsuario, string cod_linea, ref List<Int64> lst_Num_Apor)
        {
            try
            {
                BOLineaAporte.CrearImportacion(pFechaCarga, ref pError, lstAporte, pUsuario, cod_linea, ref lst_Num_Apor);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteService", "CrearAporteImportacion", ex);
            }
        }




    }
}