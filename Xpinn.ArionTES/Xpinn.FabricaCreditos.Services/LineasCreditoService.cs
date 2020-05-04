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
    public class LineasCreditoService
    {
        private LineasCreditoBusiness BOLineasCredito;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para LineasCredito
        /// </summary>
        public LineasCreditoService()
        {
            BOLineasCredito = new LineasCreditoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "100207"; } }

        /// <summary>
        /// Servicio para crear LineasCredito
        /// </summary>
        /// <param name="pEntity">Entidad LineasCredito</param>
        /// <returns>Entidad LineasCredito creada</returns>
        public LineasCredito CrearLineasCredito(LineasCredito pLineasCredito, Usuario pUsuario)
        {
            try
            {
                return BOLineasCredito.CrearLineasCredito(pLineasCredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "CrearLineasCredito", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar LineasCredito
        /// </summary>
        /// <param name="pLineasCredito">Entidad LineasCredito</param>
        /// <returns>Entidad LineasCredito modificada</returns>
        public LineasCredito ModificarLineasCredito(LineasCredito pLineasCredito, Usuario pUsuario)
        {
            try
            {
                return BOLineasCredito.ModificarLineasCredito(pLineasCredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "ModificarLineasCredito", ex);
                return null;
            }
        }

        public bool ConsultarLineasCreditosActivasPorClasificacion(string cod_clasificacion, Usuario usuario)
        {
            try
            {
                return BOLineasCredito.ConsultarLineasCreditosActivasPorClasificacion(cod_clasificacion, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "ConsultarLineasCreditosActivasPorClasificacion", ex);
                return false;
            }
        }

        /// <summary>
        /// Servicio para Eliminar LineasCredito
        /// </summary>
        /// <param name="pId">identificador de LineasCredito</param>
        public void EliminarLineasCredito(string pId, Usuario pUsuario)
        {
            try
            {
                BOLineasCredito.EliminarLineasCredito(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarLineasCredito", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener LineasCredito
        /// </summary>
        /// <param name="pId">identificador de LineasCredito</param>
        /// <returns>Entidad LineasCredito</returns>
        public LineasCredito ConsultarLineasCredito(string pId, Usuario pUsuario)
        {
            try
            {
                return BOLineasCredito.ConsultarLineasCredito(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "ConsultarLineasCredito", ex);
                return null;
            }
        }

        public long? ConsultarNumeroCodeudoresXLinea(string pId, Usuario pUsuario)
        {
            try
            {
                return BOLineasCredito.ConsultarNumeroCodeudoresXLinea(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "ConsultarNumeroCodeudoresXLinea", ex);
                return null;
            }
        }

        public LineasCredito ConsultarTasaInteresLineaCredito(string pId, Usuario pUsuario)
        {
            try
            {
                return BOLineasCredito.ConsultarTasaInteresLineaCredito(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "ConsultarTasaInteresLineaCredito", ex);
                return null;
            }
        }

        public LineasCredito ConsultarTasaInteresLineaCredito(string pCodLinea, Int64 pCodPersona, decimal pMonto, decimal pPlazo, Usuario pUsuario)
        { 
            try
            {
                return BOLineasCredito.ConsultarTasaInteresLineaCredito(pCodLinea, pCodPersona, pMonto, pPlazo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "ConsultarTasaInteresLineaCredito", ex);
                return null;
            }
        }

        public List<LineasCredito> ConsultarGarantiasPorCredito(int pCreditoId, Usuario pUsuario)
        {
            try
            {
                return BOLineasCredito.ConsultarGarantiasPorCredito(pCreditoId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "ConsultarGarantiasPorCredito", ex);
                return null;
            }
        }

        public List<LineasCredito> ListarLineasCreditoTasaInteres(string pFiltro, Usuario pUsuario)
        {
            try
            {
                return BOLineasCredito.ListarLineasCreditoTasaInteres(pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "ListarLineasCreditoTasaInteres", ex);
                return null;
            }
        }

        /// <summary>
        /// Envía la consulta para obtener la tasa de interes para una simulación especifica
        /// </summary>
        /// <param name="cod_linea_credito"></param>
        /// <param name="plazo"></param>
        /// <param name="monto"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public decimal obtenerTasaInteresEspecifica(string cod_linea_credito, int plazo, Usuario pUsuario)
        {
            try
            {
                return BOLineasCredito.obtenerTasaInteresEspecifica(cod_linea_credito, plazo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "ListarLineasCreditoTasaInteres", ex);
                return 0;
            }
        }


        /// <summary>
        /// Servicio para obtener LineasCredito
        /// </summary>
        /// <param name="pId">identificador de LineasCredito</param>
        /// <returns>Entidad LineasCredito</returns>
        public LineasCredito ConsultarLineasCreditoRotativo(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOLineasCredito.ConsultarLineasCreditoRotativo(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "ConsultarLineasCreditoRotativo", ex);
                return null;
            }
        }



        public List<LineasCredito> ddlliquidacion(Usuario pUsuario)
        {
            try
            {
                return BOLineasCredito.ddlliquidacion(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "ConsultarLineasCredito", ex);
                return null;
            }
        }

        public List<LineasCredito> ddlatributo(Usuario pUsuario)
        {
            try
            {
                return BOLineasCredito.ddlatributo(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "ConsultarLineasCredito", ex);
                return null;
            }
        }

        public List<LineasCredito> ddlimpuestos(Usuario pUsuario)
        {
            try
            {
                return BOLineasCredito.ddlimpuestos(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "ConsultarLineasCredito", ex);
                return null;
            }
        }


        public List<LineasCredito> ConsultarLineas_Creditoatributo(string codigo, Usuario pUsuario)
        {
            try
            {
                return BOLineasCredito.ConsultarLineasCreditoatributos(codigo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "ConsultarLineasCredito", ex);
                return null;
            }
        }

        public LineasCredito ConsultarAtributoGeneral(string codigo, Usuario pUsuario)
        {
            try
            {
                return BOLineasCredito.ConsultarAtributoGeneral(codigo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "ConsultarAtributoGeneral", ex);
                return null;
            }
        }
        public LineasCredito ConsultarAtributos(string codigo, Usuario pUsuario)
        {
            try
            {
                return BOLineasCredito.ConsultarAtributos(codigo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "ConsultarAtributos", ex);
                return null;
            }
        }

        public List<LineasCredito> ListarDeducciones(int codigo, int atributo, Usuario pUsuario)
        {
            try
            {
                return BOLineasCredito.ListarDeducciones(codigo, atributo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "ListarDeducciones", ex);
                return null;
            }
        }

        public LineasCredito ConsultarDeducciones(string cod_linea, int atributo, Usuario pUsuario)
        {
            try
            {
                return BOLineasCredito.ConsultarDeducciones(cod_linea, atributo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "ConsultarDeducciones", ex);
                return null;
            }
        }

        public List<LineasCredito> ConsultarLineasCrediatributo(string codigo, int rango, Usuario pUsuario)
        {
            try
            {
                return BOLineasCredito.ConsultarLineasCrediatributo(codigo, rango, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "ConsultarLineasCredito", ex);
                return null;
            }
        }
        public List<LineasCredito> ConsultarLineasCrediatributo2(string codigo, Usuario pUsuario,string numradic)
        {
            try
            {
                return BOLineasCredito.ConsultarLineasCrediatributo2(codigo, pUsuario,numradic);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "ConsultarLineasCredito", ex);
                return null;
            }
        }
        public List<RangosTopes> ConsultarLineasCreditopes(string codigo, string atr, Usuario pUsuario)
        {
            try
            {
                return BOLineasCredito.ConsultarLineasCreditopes(codigo, atr, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "ConsultarLineasCredito", ex);
                return null;
            }
        }
        public RangosTopes ConsultarCreditoTopes(int codigo, int tope, Usuario pUsuario)
        {
            try
            {
                return BOLineasCredito.ConsultarCreditoTopes(codigo, tope, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "ConsultarCreditoTopes", ex);
                return null;
            }
        }

        public RangosTopes ConsultarTopestodos(string codigo, Usuario pUsuario)
        {
            try
            {
                return BOLineasCredito.ConsultarTopestodos(codigo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "ConsultarTopestodos", ex);
                return null;
            }
        }

        public void Creardeducciones(LineasCredito entidad, Usuario pUsuario)
        {
            try
            {
                BOLineasCredito.Creardeducciones(entidad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "ConsultarLineasCredito", ex);

            }
        }

        public void modificardeducciones(LineasCredito entidad, Usuario pUsuario)
        {

            try
            {
                BOLineasCredito.modificardeducciones(entidad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "ConsultarLineasCredito", ex);

            }
        }


        public LineasCredito CrearAtributosEXPINN(LineasCredito pAtributos, Usuario pUsuario)
        {
            try
            {
                return BOLineasCredito.CrearAtributosEXPINN(pAtributos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "CrearAtributosEXPINN", ex);
                return null;
            }
        }


        public LineasCredito ModificarAtributosEXPINN(LineasCredito pAtributos, Usuario pUsuario)
        {
            try
            {
                return BOLineasCredito.ModificarAtributosEXPINN(pAtributos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "ModificarAtributosEXPINN", ex);
                return null;
            }
        }


        public void EliminarAtributos(LineasCredito pAtributos, Usuario vUsuario)
        {
            try
            {
                BOLineasCredito.EliminarAtributos(pAtributos, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "EliminarAtributos", ex);
            }
        }

        public void EliminarTopes(RangosTopes pTopes, Usuario vUsuario)
        {
            try
            {
                BOLineasCredito.EliminarTopes(pTopes, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "EliminarTopes", ex);
            }
        }


        public List<LineasCredito> ConsultarLineasCreditodeducciones(string codigo, Usuario pUsuario)
        {
            try
            {
                return BOLineasCredito.ConsultarLineasCreditodeducciones(codigo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "ConsultarLineasCredito", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de LineasCreditos a partir de unos filtros
        /// </summary>
        /// <param name="pLineasCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de LineasCredito obtenidos</returns>
        public List<LineasCredito> ListarLineasCredito(LineasCredito pLineasCredito, Usuario pUsuario)
        {
            try
            {
                return BOLineasCredito.ListarLineasCredito(pLineasCredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "ListarLineasCredito", ex);
                return null;
            }
        }

        /// <summary>
        /// Listar todas las lineas de crédito
        /// </summary>
        /// <param name="pLineasCredito"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public List<LineasCredito> ListarLineasCredito(Usuario pUsuario)
        {
            try
            {
                return BOLineasCredito.ListarLineasCredito(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "ListarLineasCredito", ex);
                return null;
            }
        }

        public List<LineasCredito> ListarLineasCreditoSinAuxilio(LineasCredito pLineasCredito, Usuario pUsuario)
        {
            try
            {
                return BOLineasCredito.ListarLineasCreditoSinAuxilio(pLineasCredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "ListarLineasCreditoSinAuxilio", ex);
                return null;
            }
        }

        public List<LineasCredito> ListarParentesco(LineasCredito pLineasCredito, Usuario pUsuario)
        {
            try
            {
                return BOLineasCredito.ListarParentesco(pLineasCredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "ListarParentesco", ex);
                return null;
            }
        }

        public List<LineasCredito> MotivoCredito(LineasCredito pLineasCredito, Usuario pUsuario)
        {
            try
            {
                return BOLineasCredito.MotivoCredito(pLineasCredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoBusiness", "MotivoCredito", ex);
                return null;
            }
        }

        public List<LineasCredito> ddlLineasCreditoServices(Usuario pUsuario)
        {
            try
            {
                return BOLineasCredito.ddlListarLineaBusines(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "ListarLineasCredito", ex);
                return null;
            }
        }

        public LineasCredito getPorcentajeMatriculaServices(string CodLineaAuxilio, Usuario pUsuario)
        {
            try
            {
                return BOLineasCredito.getPorcentajeMatriculaBusines(CodLineaAuxilio, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "ListarLineasCredito", ex);
                return null;
            }
        }



        /// <summary>
        /// Obtiene  listas desplegables
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de elementos obtenidos</returns>
        public List<Persona1> ListasDesplegables(String ListaSolicitada, Usuario pUsuario)
        {
            try
            {
                return BOLineasCredito.ListasDesplegables(ListaSolicitada, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosClienteService", "ListarListasMenu", ex);
                return null;
            }
        }


        public List<Atributos> ListarAtributos(Atributos pentidad, Usuario pUsuario)
        {
            try
            {
                return BOLineasCredito.ListarAtributos(pentidad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaCreditoService", "ListarAtributos", ex);
                return null;
            }
        }

        public List<RangosTopes> ListarTopes(RangosTopes pentidad, Usuario pUsuario)
        {
            try
            {
                return BOLineasCredito.ListarTopes(pentidad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaCreditoService", "ListarTopes", ex);
                return null;
            }
        }


        public LineasCredito ConsultaLineaCredito(String cod_linea_credito, Usuario pUsuario)
        {
            try
            {
                return BOLineasCredito.ConsultaLineaCredito(cod_linea_credito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConsultaLineaCreditoService", "ConsultaLineaCredito", ex);
                return null;
            }
        }


        public LineasCredito Calcular_Cupo(String pcod_linea_credito, Int64 pcod_persona, DateTime pfecha, Usuario pUsuario)
        {
            try
            {
                return BOLineasCredito.Calcular_Cupo(pcod_linea_credito, pcod_persona, pfecha, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConsultaLineaCreditoService", "ConsultaLineaCredito", ex);
                return null;
            }
        }


        /// <summary>
        /// Método para listas líneas de crédito re-estructuradas
        /// </summary>
        /// <param name="linea"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public List<LineasCredito> ListarLineasCreditoRes(LineasCredito linea, Usuario pUsuario)
        {
            try
            {
                return BOLineasCredito.ListarLineasCreditoRes(linea, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaCreditoService", "ListarLineasCreditoRes", ex);
                return null;
            }
        }


        public List<Atributos> ListasAtributosLinea(LineasCredito pLinea, DateTime pfecha_solicitud, Int64 pnumero_cuotas, Double pmonto_solicitado, Usuario pUsuario)
        {
            try
            {
                return BOLineasCredito.ListasAtributosLinea(pLinea, pfecha_solicitud, pnumero_cuotas, pmonto_solicitado, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaCreditoService", "ListasAtributosLinea", ex);
                return null;
            }
        }


        public Boolean LineaEsFondoGarantiasComunitarias(String pId, Usuario pUsuario)
        {
            try
            {
                return BOLineasCredito.LineaEsFondoGarantiasComunitarias(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaCreditoService", "LineaEsFondoGarantiasComunitarias", ex);
                return false;
            }
        }

        public List<LineasCredito> LineasCastigo(Usuario pUsuario)
        {
            try
            {
                return BOLineasCredito.LineasCastigo(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "LineasCastigo", ex);
                return null;
            }
        }

        public decimal ConsultarParametrosLinea(string cod_linea, string cod_parametro, Usuario pUsuario)
        {
            try
            {
                return BOLineasCredito.ConsultarParametrosLinea(cod_linea, cod_parametro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "ConsultarParametrosLinea", ex);
                return 0;
            }
        }


        public void EliminarDeducciones(LineasCredito pDeducciones, Usuario vUsuario)
        {
            try
            {
                BOLineasCredito.EliminarDeducciones(pDeducciones, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "ElimiEliminarDeduccionesnarAtributos", ex);
            }
        }


        public void EliminarTodoElAtributo(LineasCredito pAtri, Usuario vUsuario)
        {
            try
            {
                BOLineasCredito.EliminarTodoElAtributo(pAtri, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "EliminarTodoElAtributo", ex);
            }
        }

        public List<RangosTopes> CrearRanValAtributo(List<RangosTopes> lstRangos, Usuario vUsuario)
        {
            try
            {
                return BOLineasCredito.CrearRanValAtributo(lstRangos, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "CrearRanValAtributo", ex);
                return null;
            }
        }

        public void EliminarRanValAtributo(Int64 cod_atributo, Usuario vUsuario)
        {
            try
            {
                BOLineasCredito.EliminarRanValAtributo(cod_atributo, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "EliminarRanValAtributo", ex);
            }
        }

        public List<RangosTopes> ListarRangosAtributos(Int64 cod_atr, Usuario vUsuario)
        {
            try
            {
                return BOLineasCredito.ListarRangosAtributos(cod_atr, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "ListarRangosAtributos", ex);
                return null;
            }
        }

        #region Parametros de Componentes (Documentos)

        public List<LineasCredito> ListarDocumentos(LineasCredito pLinea, string filtro, Usuario pUsuario)
        {
            try
            {
                return BOLineasCredito.ListarDocumentos(pLinea, filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "ListarDocumentos", ex);
                return null;
            }
        }

        public List<LineasCredito> ListarDocumentosXLinea(string pCod_linea_credito, Usuario pUsuario)
        {
            try
            {
                return BOLineasCredito.ListarDocumentosXLinea(pCod_linea_credito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "ListarDocumentosXLinea", ex);
                return null;
            }
        }


        public List<LineasCredito> ListarComboTipoDocumentos(LineasCredito pLinea, Usuario pUsuario)
        {
            try
            {
                return BOLineasCredito.ListarComboTipoDocumentos(pLinea, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "ListarComboTipoDocumentos", ex);
                return null;
            }
        }

        public List<LineasCredito> ConsultarGarantiaDocumento(string pId, Usuario vUsuario)
        {
            try
            {
                return BOLineasCredito.ConsultarGarantiaDocumento(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "ConsultarGarantiaDocumento", ex);
                return null;
            }
        }

        public List<LineasCredito> ConsultarGarantiacompleta(Usuario vUsuario)
        {
            try
            {
                return BOLineasCredito.ConsultarGarantiacompleta(vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoBusinness", "ConsultarGarantiaDocumento", ex);
                return null;
            }
        }

        public void EliminarDocumentosGarantia(string pId, Usuario vUsuario)
        {
            try
            {
                BOLineasCredito.EliminarDocumentosGarantia(pId, vUsuario);
                return;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoBusinness", "ConsultarGarantiaDocumento", ex);
                return;
            }
        }



        public void Eliminardocumentosdegarantia(string pId, string linea, Usuario vUsuario)
        {
            try
            {
                BOLineasCredito.Eliminardocumentosdegarantia(pId, linea, vUsuario);
                return;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoBusinness", "ConsultarGarantiaDocumento", ex);
                return;
            }
        }

        #endregion

        #region Procesos

        public List<ProcesoLineaCredito> ListarProcesos(ProcesoLineaCredito pProceso, string filtro, Usuario vUsuario)
        {
            try
            {
                return BOLineasCredito.ListarProcesos(pProceso, filtro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "ListarProcesos", ex);
                return null;
            }
        }

        #endregion

        #region PRIORIDADES

        public List<LineasCredito> ConsultarPrioridad_Linea(string pId, Usuario vUsuario)
        {
            try
            {
                return BOLineasCredito.ConsultarPrioridad_Linea(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "ConsultarPrioridad_Linea", ex);
                return null;
            }
        }

        #endregion

        #region destinacion
        public List<LineasCredito> ConsultarDestinacion_Linea(string pId, Usuario vUsuario)
        {
            try
            {
                return BOLineasCredito.ConsultarDestinacion_Linea(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaExcluyenteServices", "Listardestinacion", ex);
                return null;
            }
        }

        public List<LineaCred_Destinacion> ListaDestinacionCredito(string pId, Usuario vUsuario)
        {
            try
            {
                return BOLineasCredito.ListaDestinacionCredito(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaExcluyenteServices", "ListaDestinacionCredito", ex);
                return null;
            }
        }


        #endregion

        public List<ProcesoLineaCredito> ListarParametrosLinea(string codLienaCredito, Usuario vUsuario)
        {
            try
            {
                return BOLineasCredito.ListarParametrosLinea(codLienaCredito, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "ListarProcesos", ex);
                return null;
            }
        }

    } 
}