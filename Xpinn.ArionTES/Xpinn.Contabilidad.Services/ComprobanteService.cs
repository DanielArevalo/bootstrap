using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Contabilidad.Business;
using Xpinn.Contabilidad.Entities;
using System.IO;
using Xpinn.Tesoreria.Entities;


namespace Xpinn.Contabilidad.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ComprobanteService
    {
        private ComprobanteBusiness BOComprobante;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para comprobante
        /// </summary>
        public ComprobanteService()
        {
            BOComprobante = new ComprobanteBusiness();
            BOExcepcion = new ExcepcionBusiness();
            CodigoPrograma = "30108";
        }

        public int numero_comp;
        public int tipo_comp;
        public string CodigoPrograma;
        public string CodigoProgramaContable { get { return "30101"; } }
        public string CodigoProgramaIngreso { get { return "30102"; } }
        public string CodigoProgramaEgreso { get { return "30103"; } }
        public string CodigoProgramaCarga { get { return "30107"; } }
        public string CodigoProgramaCopia { get { return "30106"; } }
        public string CodigoProgramaModif { get { return "30104"; } }
        public string CodigoProgramaAnulacion { get { return "30109"; } }

        public List<DetalleComprobante> ConsultarCargaComprobanteDetalle(Int64 operacion, Usuario pUsuario)
        {
            try
            {
                return BOComprobante.ConsultarCargaComprobanteDetalle(operacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiasService", "ConsultarCargaComprobanteDetalle", ex);
                return null;
            }
        }

        public List<DetalleComprobante> ConsultarCargaComprobanteNiifDetalle(Int64 operacion, Usuario pUsuario)
        {
            try
            {
                return BOComprobante.ConsultarCargaComprobanteNiifDetalle(operacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiasService", "ConsultarCargaComprobanteNiifDetalle", ex);
                return null;
            }
        }

        public Boolean CargarArchivo(Stream stream, string pTipoNorma, bool pgrabar, ref List<DetalleComprobante> lstcargacomprobante, ref string error, Usuario pUsuario)
        {
            return BOComprobante.CargarArchivo(stream, pTipoNorma, pgrabar, ref lstcargacomprobante, ref error, pUsuario);
        }

        public Boolean CargarTexto(string datos, string pTipoNorma, ref string error, Usuario pUsuario)
        {
            return BOComprobante.CargarTexto(datos, pTipoNorma, ref error, pUsuario);
        }

        /// <summary>
        ///  Valida datos para insertar en un detalle de comprobante
        /// </summary>
        /// <param name="lstCargaComprobante"></param>
        /// <param name="pUsuario"></param>
        /// <param name="Error"></param>
        /// <returns></returns>
        public Boolean Validar(List<DetalleComprobante> lstCargaComprobante, Usuario pUsuario, ref string Error)
        {
            try
            {
                return BOComprobante.Validar(lstCargaComprobante, pUsuario, ref Error);
            }
            catch (Exception ex)
            {
                Error += ex.Message;
                return false;
            }

        }

        public Comprobante CrearComprobante(List<DetalleComprobante> vDetalleComprobante,Comprobante vComprobante, Usuario pUsuario)
        {
            try
            {
                return BOComprobante.CrearComprobante(vDetalleComprobante,vComprobante, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteService", "CrearComprobante", ex);
                return null;
            }
        }

        public Comprobante ModificarComprobante(List<DetalleComprobante> vDetalleComprobante,Comprobante vComprobante, Usuario pUsuario)
        {
            try
            {
                return BOComprobante.ModificarComprobante(vDetalleComprobante,vComprobante, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteService", "ModificarComprobante", ex);
                return null;
            }
        }

        public Boolean ConsultarComprobante(Int64 pnum_comp, Int64 ptipo_comp, ref Comprobante pComprobante, ref List<DetalleComprobante> pDetalleComprobante, Usuario pUsuario)
        {
            try
            {
                return BOComprobante.ConsultarComprobante(pnum_comp, ptipo_comp, ref pComprobante, ref pDetalleComprobante, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteService", "ConsultarComprobante", ex);
                return false;
            }
        }
        public Boolean ConsultarComprobante_Anulacion(Int64 pnum_comp, Int64 ptipo_comp, ref Comprobante pComprobante, ref List<DetalleComprobante> pDetalleComprobante, Usuario pUsuario)
        {
            try
            {
                return BOComprobante.ConsultarComprobante_Anulacion(pnum_comp, ptipo_comp, ref pComprobante, ref pDetalleComprobante, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteService", "ConsultarComprobante_Anulacion", ex);
                return false;
            }
        }
        public Boolean ConsultarGiroGeneral(Int64 pnum_comp, Int64 ptipo_comp, ref Giro pGiro, Usuario pUsuario)
        {
            try
            {
                return BOComprobante.ConsultarGiroGeneral(pnum_comp, ptipo_comp,ref pGiro, pUsuario);                
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteService", "ConsultarGiroGeneral", ex);
                return false;
            }
        }

        public string ConsultarCuenta(Int64 pCodBanco, string pNumCuenta, Usuario pUsuario)
        {
            try
            {
                return BOComprobante.ConsultarCuenta(pCodBanco, pNumCuenta, pUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteService", "ConsultarComprobante", ex);
                return "";
            }
        }


        public Int64 ConsultarOperacion(Int64 pnumComp,Int64 ptipo_comp, Usuario pUsuario)
        {
            try
            {
                return BOComprobante.ConsultarOperacion(pnumComp,ptipo_comp, pUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteService", "ConsultarOperacion", ex);
                return -1;
            }
        }
  
        /// <summary>
        /// Servicio para obtener lista de comprobantes a partir de unos filtros
        /// </summary>
        /// <param name="pMovimientoCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de comprobantes obtenidos</returns>        
        public List<Comprobante> ListarComprobantes(Comprobante pComprobante, Usuario pUsuario, String pfiltro, String orden)
        {
            try
            {
                return BOComprobante.ListarComprobante(pComprobante, pUsuario, pfiltro, orden);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteServices", "ListarComprobantes", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de comprobantes a partir de unos filtros
        /// </summary>
        /// <param name="pMovimientoCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de comprobantes obtenidos</returns>
        public List<DetalleComprobante> ListarComprobantesreporte(DetalleComprobante pComprobante, Usuario pUsuario)
        {
            try
            {
                return BOComprobante.ListarComprobantesreporte(pComprobante, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteServices", "ListarComprobantesreporte", ex);
                return null;
            }
        }


        public string Consultafecha(Usuario pUsuario, string tipoComprobante = null)
        {
            try
            {
                return BOComprobante.Consultafecha(pUsuario, tipoComprobante);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteServices", "ConsultaUsuario", ex);
                return null;
            }
        }

        public string ConsultaUsuario(long cod, Usuario pUsuario)
        {
            try
            {
                return BOComprobante.ConsultaUsuario(cod, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteServices", "ConsultaUsuario", ex);
                return null;
            }
        }

        public Int64 ConsultaCodUsuario(string cod, Usuario pUsuario)
        {
            try
            {
                return BOComprobante.ConsultaCodUsuario(cod, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteServices", "ConsultaCodUsuario", ex);
                return -1;
            }
        }

        public string CuentaEsAuxiliar(string cod, Usuario pUsuario)
        {
            try
            {
                return BOComprobante.CuentaEsAuxiliar(cod, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteServices", "CuentaEsAuxiliar", ex);
                return "";
            }
        }

        public string CuentaNIFEsAuxiliar(string cod, Usuario pUsuario)
        {
            try
            {
                return BOComprobante.CuentaNIFEsAuxiliar(cod, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteServices", "CuentaNIFEsAuxiliar", ex);
                return "";
            }
        }

        public Boolean CuentaEsGiro(string cod, Usuario pUsuario)
        {
            try
            {
                return BOComprobante.CuentaEsGiro(cod, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteServices", "CuentaEsGiro", ex);
                return false;
            }
        }

        public List<ProcesoContable> ConsultaProceso(Int64 pcod_ope, Int64 ptip_ope, DateTime pfecha, Usuario pUsuario)
        {
            try
            {
                return BOComprobante.ConsultaProceso(pcod_ope, ptip_ope, pfecha, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteServices", "ConsultaProceso", ex);
                return null;
            }
        }

        public List<ProcesoContable> ConsultaProcesoUlt(Int64 pcod_ope, Int64 ptip_ope, DateTime pfecha, Usuario pUsuario)
        {
            try
            {
                return BOComprobante.ConsultaProcesoUlt(pcod_ope, ptip_ope, pfecha, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteServices", "ConsultaProcesoUlt", ex);
                return null;
            }
        }

        public Boolean GenerarComprobante(Int64 pcod_ope, Int64 ptip_ope, DateTime pfecha, Int64 pcod_ofi, Int64 pcod_persona, Int64 pcod_proceso, ref Int64 pnum_comp, ref Int64 ptipo_comp, ref string pError, Usuario pUsuario)
        {
            try
            {
                if (ptip_ope == 36 || ptip_ope == 37 || ptip_ope == 38)
                    return BOComprobante.GenerarComprobanteSinCommit(pcod_ope, ptip_ope, pfecha, pcod_ofi, pcod_persona, pcod_proceso, ref pnum_comp, ref ptipo_comp, ref pError, pUsuario);
                else
                    return BOComprobante.GenerarComprobante(pcod_ope, ptip_ope, pfecha, pcod_ofi, pcod_persona, pcod_proceso, ref pnum_comp, ref ptipo_comp, ref pError, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteBusiness", "GenerarComprobanTe", ex);
                return false;
            }
        }                

        public Int64 ObtenerSiguienteCodigo(Int64 pTipoComp, DateTime pFecha, Int64 pOficina, Usuario pUsuario)
        {
            try
            {
                return BOComprobante.ObtenerSiguienteCodigo(pTipoComp, pFecha, pOficina, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteServices", "ObtenerSiguienteCodigo", ex);
                return 0;
            }
        }

        public void Anularcomprobante(Comprobante pCOMPROBANTE, Usuario pusuario)
        {
            try
            {
                    BOComprobante.Anularcomprobante(pCOMPROBANTE, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("COMPROBANTEBusiness", "Anularcomprobante", ex);
            }
        }

        public Boolean ConsultarAnulaciondetalle(Int64 pnum_comp, Int64 ptipo_comp, ref Comprobante pComprobante, ref List<DetalleComprobante> pDetalleComprobante, Usuario pUsuario)
        {
            try
            {
                return BOComprobante.ConsultarAnulaciondetalle(pnum_comp, ptipo_comp, ref pComprobante, ref pDetalleComprobante, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteService", "ConsultarComprobante", ex);
                return false;
            }
        }


        public Comprobante crearanulacioncomprobante(List<DetalleComprobante> vDetalleComprobante, Comprobante pComprobante, Usuario vUsuario)
        {
            try
            {
                pComprobante = BOComprobante.crearanulacioncomprobante(vDetalleComprobante, pComprobante, vUsuario);
                    return pComprobante;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteBusiness", "CrearComprobante", ex);
                return null;
            }
        }

        public Int64 consultacod_persona(string cod, Usuario pUsuario)
        {
            try
            {
                return BOComprobante.consultacod_persona(cod, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteServices", "ConsultaCodUsuario", ex);
                return -1;
            }
        }


        public Comprobante ConsultarObservacionesAnulacion(Int64 pnum_comp, Int64 ptipo_comp, Usuario vUsuario)
        {
            try
            {
                Comprobante Comprobante = new Comprobante();
                Comprobante = BOComprobante.ConsultarObservacionesAnulacion(pnum_comp, ptipo_comp, vUsuario);
                return Comprobante;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteBusiness", "ConsultarObservacionesAnulacion", ex);
                return null;
            }
        }
        public Boolean GenerarComprobanteCaja(Int64 pcod_ope, Int64 ptip_ope, DateTime pfecha, Int64 pcod_ofi, Int64 pcod_caja, Int64 pcod_persona, Int64 pcod_proceso, ref Int64 pnum_comp, ref Int64 ptipo_comp, ref string pError, Usuario pUsuario)
        {
            try
            {
               return BOComprobante.GenerarComprobanteCaja(pcod_ope, ptip_ope, pfecha, pcod_ofi, pcod_caja, pcod_persona, pcod_proceso, ref pnum_comp, ref ptipo_comp, ref pError, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteBusiness", "GenerarComprobanteCaja", ex);
                return false;
            }
        }

        public List<DetalleComprobante> ConsultarDetalleComprobante(Int64 pnum_comp, Int64 ptipo_comp, Usuario pUsuario)
        {
            List<DetalleComprobante> pLstDetalle = new List<DetalleComprobante>();
            try
            {
                pLstDetalle = BOComprobante.ConsultarDetalleComprobante(pnum_comp, ptipo_comp, pUsuario);
            }
            catch
            {
                return pLstDetalle;
            }
            return pLstDetalle;
        }
        //Agregado para verificar cuentas contables que se manejan en productos
        public Int32 ValidarCuentaContable(Int64 nuevo,string cod_cuenta, Usuario pUsuario)
        {
            try
            {
                Int32 validar = BOComprobante.ValidarCuentaContable(nuevo, cod_cuenta, pUsuario);
                return validar;
            }
            catch(Exception ex)
            {
                BOExcepcion.Throw("ComprobanteBusiness", "ValidarCuentaContable", ex);
                return 0;
            }
        }

        public bool GenerarComprobanteTraslado(DateTime fecha_contabilizacion, Int64 cod_traslado, ref Int64 num_comp, ref Int64 tipo_comp, ref string error, Usuario pUsuario)
        {
            try
            {
                return BOComprobante.GenerarComprobanteTraslado(fecha_contabilizacion, cod_traslado, ref num_comp, ref tipo_comp, ref error, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteBusiness", "GenerarComprobanteTraslado", ex);
                return false;
            }
        }

        public List<Comprobante> ContabilizarOperacionSinComp(DateTime pFechaIni, DateTime pFechaFin, Int64 pTipoProducto, Usuario vUsuario)
        {
            try
            {
                return BOComprobante.ContabilizarOperacionSinComp(pFechaIni, pFechaFin, pTipoProducto, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteBusiness", "ContabilizarOperacionSinComp", ex);
                return null;
            }
        }



        public Comprobante ConsultarDatosElaboro(Int64 pelaboro, Usuario pUsuario)
        {
            try
            {
                return BOComprobante.ConsultarDatosElaboro(pelaboro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobanteService", "ConsultarDatosElaboro", ex);
                return null;
            }
        }
    }
}