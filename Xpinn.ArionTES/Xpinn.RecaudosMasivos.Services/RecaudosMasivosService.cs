using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Tesoreria.Business;
using Xpinn.Tesoreria.Entities;
using System.Web;
using System.IO;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.Tesoreria.Services
{
    public class RecaudosMasivosService
    {
        private RecaudosMasivosBusiness BORecaudosMasivos;
        private ExcepcionBusiness BOExcepcion;

        public RecaudosMasivosService()
        {
            BORecaudosMasivos = new RecaudosMasivosBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "180101"; } }
        public string CodigoProgramaAplicacion { get { return "180102"; } }
        public string CodigoProgramaConsulta { get { return "180103"; } }
        public string CodigoProgramaEnMora { get { return "180107"; } }
        public string CodigoProgramaAportesPend { get { return "180111"; } }

        /// <summary>
        ///  Método para leer el archivo 
        /// </summary>
        /// <param name="pEntityRecaudos"></param>
        /// <param name="perror"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public List<RecaudosMasivos> CargarArchivo(Int64 pentidad, Int64 pestructura, DateTime pfecha, Stream pstream, Int64? pNumeroNovedad, ref string perror, ref List<Xpinn.Tesoreria.Entities.ErroresCarga> plstErrores, Usuario pUsuario)
        {
            return BORecaudosMasivos.CargarArchivo(pentidad, pestructura, pfecha, pstream, pNumeroNovedad, ref perror, ref plstErrores, pUsuario);
        }


        public Boolean AplicarPago(Int64 numero_reclamacion, DateTime fecha_reclamacion, List<RecaudosMasivos> lstReclamaciones, Usuario pUsuario, ref string Error, ref Int64 pCodOpe)
        {
            try
            {
                return BORecaudosMasivos.AplicarPago(numero_reclamacion, fecha_reclamacion, lstReclamaciones, pUsuario, ref Error, ref pCodOpe);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RecuadosMasivosService", "AplicarPago", ex);
                return false;
            }
        }

        public Boolean ProcesoAplicarPago(Int64 numero_aplicacion, DateTime fecha_reclamacion, List<RecaudosMasivos> lstRecaudos, Boolean pAporteXaplicar, Usuario pUsuario, ref string Error, ref Int64 pCodOpe, Int64 ptipoOpe = 119)
        {
            try
            {
                return BORecaudosMasivos.ProcesoAplicarPago(numero_aplicacion, fecha_reclamacion, lstRecaudos, pAporteXaplicar, pUsuario, ref Error, ref pCodOpe , ptipoOpe);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RecuadosMasivosService", "ProcesoAplicarPago", ex);
                return false;
            }

        }


        public Boolean Validar(DateTime fecha_reclamacion, List<RecaudosMasivos> lstReclamaciones, Usuario pUsuario, ref string Error)
        {
            try
            {
                return BORecaudosMasivos.Validar(fecha_reclamacion, lstReclamaciones, pUsuario, ref Error);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RecuadosMasivosService", "Validar", ex);
                return false;
            }

        }


        public List<RecaudosMasivos> ListadoProductosEnMora(DateTime pFecha, RecaudosMasivos ProductosEnMora, string filtros, Usuario pUsuario)
        {
            try
            {
                return BORecaudosMasivos.ListadoProductosEnMora(pFecha, ProductosEnMora, filtros, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RecuadosMasivosService", "ListadoProductosEnMora", ex);
                return null;
            }
        }



        public List<EmpresaRecaudo> ListarEmpresaRecaudo(EmpresaRecaudo pEmpresaRecaudo, Usuario pUsuario)
        {
            try
            {
                return BORecaudosMasivos.ListarEmpresaRecaudo(pEmpresaRecaudo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RecuadosMasivosService", "ListarEmpresaRecaudo", ex);
                return null;
            }
        }

        public String GuardarRecaudos(RecaudosMasivos pentidad, DateTime pfecha_aplicacion, List<RecaudosMasivos> lstRecaudos, Usuario pUsuario, ref string Error)
        {
            try
            {
                return BORecaudosMasivos.GuardarRecaudos(pentidad, pfecha_aplicacion, lstRecaudos, pUsuario, ref Error);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RecuadosMasivosService", "GuardarRecaudo", ex);
                return null;
            }

        }


        public List<RecaudosMasivos> ListarRecaudo(RecaudosMasivos pRecaudosMasivos, Usuario pUsuario)
        {
            try
            {
                return BORecaudosMasivos.ListarRecaudo(pRecaudosMasivos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RecuadosMasivosService", "ListarRecaudo", ex);
                return null;
            }
        }

        public List<RecaudosMasivos> ListarAportesPorAplicar(string pFiltro, Usuario pUsuario)
        {
            try
            {
                return BORecaudosMasivos.ListarAportesPorAplicar(pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RecuadosMasivosService", "ListarAportesPorAplicar", ex);
                return null;
            }
        }


        public RecaudosMasivos ConsultarRecaudo(string pNumeroRecaudo, Usuario pUsuario)
        {
            try
            {
                return BORecaudosMasivos.ConsultarRecaudo(pNumeroRecaudo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RecuadosMasivosService", "ConsultarRecaudo", ex);
                return null;
            }
        }

        public RecaudosMasivos ConsultarRecaudo(RecaudosMasivos pRecaudo, Usuario pUsuario)
        {
            try
            {
                return BORecaudosMasivos.ConsultarRecaudo(pRecaudo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RecuadosMasivosService", "ConsultarRecaudo", ex);
                return null;
            }
        }


        public List<RecaudosMasivos> ListarDetalleRecaudo(int pNumeroRecaudo, Usuario pUsuario)
        {
            try
            {
                return BORecaudosMasivos.ListarDetalleRecaudo(pNumeroRecaudo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RecuadosMasivosService", "ListarDetalleRecaudo", ex);
                return null;
            }
        }

        public List<RecaudosMasivos> ListarDetalleAportePendientes(string pFiltro, Usuario pUsuario)
        {
            try
            {
                return BORecaudosMasivos.ListarDetalleAportePendientes(pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RecuadosMasivosService", "ListarDetalleAportePendientes", ex);
                return null;
            }
        }


        public List<RecaudosMasivos> ListarDetalleRecaudoConsulta(int pNumeroRecaudo, bool bDetallado, Usuario pUsuario)
        {
            try
            {
                return BORecaudosMasivos.ListarDetalleRecaudoConsulta(pNumeroRecaudo, bDetallado, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RecuadosMasivosService", "ListarDetalleRecaudoConsulta", ex);
                return null;
            }
        }

        public void EliminarRecaudosMasivos(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BORecaudosMasivos.EliminarRecaudosMasivos(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarRecaudosMasivos", ex);
            }
        }


        public Persona1 ConsultarPersona(string recaudosmasivos, Usuario pUsuario)
        {
            try
            {
                return BORecaudosMasivos.ConsultarPersona(recaudosmasivos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RecuadosMasivosService", "ConsultarPersona", ex);
                return null;
            }
        }


        public List<RecaudosMasivos> ListarDetalleReporte(int pNumeroRecaudo, int pNumeroNovedad, Usuario pUsuario)
        {
            try
            {
                return BORecaudosMasivos.ListarDetalleReporte(pNumeroRecaudo, pNumeroNovedad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RecuadosMasivosService", "ListarDetalleReporte", ex);
                return null;
            }
        }

        public List<RecaudosMasivos> ListarTEMP_Consolidado(RecaudosMasivos pRecaudos, ref string pError, Usuario vUsuario)
        {
            try
            {
                return BORecaudosMasivos.ListarTEMP_Consolidado(pRecaudos, ref pError, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RecuadosMasivosService", "ListarTEMP_Consolidado", ex);
                return null;
            }
        }


        public void AplicarRecaudo(Int64 numero_recaudo, DateTime fechaaplicacion, Boolean pAportePend, ref Int64 pcod_ope, ref string Error, Usuario pUsuario)
        {
            try
            {
                BORecaudosMasivos.AplicarRecaudo(numero_recaudo, fechaaplicacion, pAportePend, ref pcod_ope, ref Error, pUsuario);
            }
            catch (Exception ex)
            {
                Error += ex.Message;
            }
        }

        public int RegistrosAplicados(Int64 pNumeroRecaudo, Usuario pUsuario)
        {
            return BORecaudosMasivos.RegistrosAplicados(pNumeroRecaudo, pUsuario);
        }

        public int ConsultarProgresoRecaudos(long numero_reclamacion, Usuario usuario)
        {
            try
            {
                return BORecaudosMasivos.ConsultarProgresoRecaudos(numero_reclamacion, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RecuadosMasivosService", "ConsultarProgresoRecaudos", ex);
                return 0;
            }
        }


        //EXTRACTOS

        public List<RecaudosMasivos> ListarRecaudoExtracto(RecaudosMasivos pRecaudosMasivos, Usuario pUsuario)
        {
            try
            {
                return BORecaudosMasivos.ListarRecaudoExtracto(pRecaudosMasivos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RecuadosMasivosService", "ListarRecaudo", ex);
                return null;
            }
        }

        public List<RecaudosMasivos> ListarDetalleRecaudoConsultaExtracto(int pNumeroRecaudo, string estadoNom, bool bDetallado, Usuario pUsuario)
        {
            try
            {
                return BORecaudosMasivos.ListarDetalleRecaudoConsultaExtracto(pNumeroRecaudo, estadoNom, bDetallado, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RecuadosMasivosService", "ListarDetalleRecaudoConsulta", ex);
                return null;
            }
        }
        public List<RecaudosMasivos> ListarDetalleRecaudoConsultaExtractoxPersona(RecaudosMasivos pRecaudosMasivos, Usuario pUsuario)
        {
            try
            {
                return BORecaudosMasivos.ListarDetalleRecaudoConsultaExtractoxPersona( pRecaudosMasivos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RecuadosMasivosService", "ListarDetalleRecaudoConsulta", ex);
                return null;
            }
        }

        public List<RecaudosMasivos> ListarDeduccionesxPersona(RecaudosMasivos pRecaudos, ref string pError, Usuario vUsuario)
        {
            try
            {
                return BORecaudosMasivos.ListarDeduccionesxPersona(pRecaudos, ref pError, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RecuadosMasivosService", "ListarTEMP_Consolidado", ex);
                return null;
            }
        }

        public List<RecaudosMasivos> ListarDetalleRecaudoDeUnPeriodoYEmpresa(string codigoEmpresaRecaudadora, string fechaPeriodoNovedad, Usuario usuario)
        {
            try
            {
                return BORecaudosMasivos.ListarDetalleRecaudoDeUnPeriodoYEmpresa(codigoEmpresaRecaudadora, fechaPeriodoNovedad, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RecuadosMasivosService", "ListarDetalleRecaudoDeUnPeriodoYEmpresa", ex);
                return null;
            }
        }
    }
}
