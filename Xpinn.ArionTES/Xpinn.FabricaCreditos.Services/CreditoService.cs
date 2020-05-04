using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Business;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Auxilios.Entities;
using Xpinn.Cartera.Entities;
using Xpinn.Asesores.Entities;
using System.IO;

namespace Xpinn.FabricaCreditos.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class CreditoService
    {
        private CreditoBusiness BOCredito;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Credito
        /// </summary>
        public CreditoService()
        {
            BOCredito = new CreditoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        // Còdigo de programa para generaciòn de documentos
        public string CodigoPrograma { get { return "100138"; } }

        // Còdigo de programa para desembolso de crèditos
        public string CodigoProgramaoriginal { get { return "100140"; } }
        public string CodigoProgramaModifi { get { return "100157"; } }
        public string CodigoProgramaCancelacion { get { return "100161"; } }
        public string CodigoProgramaModificacion { get { return "100150"; } }

        // Còdigo de programa para reporte de créditos educativos
        public string CodigoProgramaRepEdu { get { return "100164"; } }

        // Modificación datos del cliente
        public string CodigoProgramaMod { get { return "100152"; } }

        // Codigo de programa para analisis de credito
        public string CodigoProgramaAnalisisCredito { get { return "100165"; } }



        // Còdigo de programa para  modificacion de cupos rotativo
        public string CodigoProgramaRotativo { get { return "100164"; } }


        /// <summary>
        /// Servicio para crear Credito
        /// </summary>
        /// <param name="pEntity">Entidad Credito</param>
        /// <returns>Entidad Credito creada</returns>
        public Credito CrearCredito(Credito pCredito, Usuario pUsuario)
        {
            try
            {
                return BOCredito.CrearCredito(pCredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "CrearCredito", ex);
                return null;
            }
        }


        public Credito CrearCreditoDesdeFuncionImportacion(Credito pCredito, Usuario pUsuario)
        {
            try
            {
                return BOCredito.CrearCreditoDesdeFuncionImportacion(pCredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "CrearCreditoDesdeFuncionImportacion", ex);
                return null;
            }
        }


        public AtributosCredito CrearAtributoCredito(AtributosCredito pAtributo, Usuario pUsuario)
        {
            try
            {
                return BOCredito.CrearAtributoCredito(pAtributo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "CrearAtributoCredito", ex);
                return null;
            }
        }



        public Analisis_Credito CrearAnalisisCredito(Analisis_Credito pAnalisis, Usuario pUsuario)
        {
            try
            {
                return BOCredito.CrearAnalisisCredito(pAnalisis, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "CrearAnalisisCredito", ex);
                return null;
            }
        }

        public Analisis_Capacidad_Pago CrearAnalisisCapacidadPago(Analisis_Capacidad_Pago pAnalisis, Usuario pUsuario)
        {
            try
            {
                return BOCredito.CrearAnalisisCapacidadPago(pAnalisis, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "CrearAnalisisCapacidadPago", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar Credito
        /// </summary>
        /// <param name="pCredito">Entidad Credito</param>
        /// <returns>Entidad Credito modificada</returns>
        public void GuardarGiro(Int64 numero_radicacion, Int64 cod_ope, long formadesembolso, DateTime fecha_desembolso, double monto,
            int idCtaBancaria, int cod_banco, string numerocuenta, int tipo_cuenta, Int64 codperson, string usuario, Usuario pUsuario)
        {
            BOCredito.GuardarGiro(numero_radicacion, cod_ope, formadesembolso, fecha_desembolso, monto, idCtaBancaria, cod_banco, numerocuenta, tipo_cuenta, codperson, pUsuario.nombre, pUsuario);
        }

        public void GuardarCuentaBancariaCliente(Int64 codpersona, string numerocuenta, Int64 tipocuenta, Int64 cod_banco, Usuario pUsuario)
        {
            BOCredito.GuardarCuentaBancariaCliente(codpersona, numerocuenta, tipocuenta, cod_banco, pUsuario);
        }

        public Credito ModificarCredito(Credito pCredito, Usuario pUsuario)
        {
            try
            {
                return BOCredito.ModificarCredito(pCredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "ModificarCredito", ex);
                return null;
            }
        }

        public void ModificarFechaDesembolsoCredito(DateTime fechadesembolso, Credito pCredito, Usuario pUsuario)
        {
            try
            {
                BOCredito.ModificarFechaDesembolsoCredito(fechadesembolso, pCredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "ModificarFechaDesembolsoCredito", ex);

            }
        }


        public Credito ModificarCreditoUlt(Credito pCredito, Usuario pUsuario)
        {
            try
            {
                return BOCredito.ModificarCreditoUlt(pCredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "ModificarCredito", ex);
                return null;
            }
        }


        public Credito ModificarCupoRotativo(Credito pCredito, Usuario pUsuario)
        {
            try
            {
                return BOCredito.ModificarCupoRotativo(pCredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "ModificarCupoRotativo", ex);
                return null;
            }
        }

        public AtributosCredito ModificarAtributosFinanciados(AtributosCredito pCredito, Usuario pUsuario)
        {
            try
            {
                return BOCredito.ModificarAtributosFinanciados(pCredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "ModificarAtributosFinanciados", ex);
                return null;
            }
        }

        public Avance ModificarDescuentos(Avance pCredito, Usuario pUsuario)
        {
            try
            {
                return BOCredito.ModificarDescuentos(pCredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "ModificarDescuentos", ex);
                return null;
            }
        }


        public void cambiotasa(string radicacion, string calculo_atr, string tasa, string tipotasa, string desviacion, string tipoHisto, string codart, Usuario pUsuario, string op)
        {
            try
            {
                BOCredito.cambiotasa(radicacion, calculo_atr, tasa, tipotasa, desviacion, tipoHisto, pUsuario, codart, op);
            }
            catch
            { }
        }

        public void cambiolinea(Int64 radicacion, string cod_linea, Usuario pUsuario)
        {
            try
            {
                BOCredito.cambiolinea(radicacion, cod_linea, pUsuario);

            }
            catch
            { }
        }


        public void cambiotasa_fecha(string tasa, string tipotasa, string radicacion, DateTime fechaIni, Usuario pUsuario)
        {
            try
            {
                BOCredito.cambiotasa_fecha(tasa, tipotasa, radicacion, fechaIni, pUsuario);

            }
            catch
            { }
        }
        /// <summary>
        /// Realizar el desembolso del crèdito
        /// </summary>
        /// <param name="pCredito"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public Credito DesembolsarCredito(Credito pCredito, bool opcion, long formadesembolso, int idCtaBancaria, int cod_banco, string numerocuenta, int tipo_cuenta, ref string Error, Usuario pUsuario)
        {
            Error = "";
            try
            {
                pCredito = BOCredito.DesembolsarCredito(pCredito, opcion, formadesembolso, idCtaBancaria, cod_banco, numerocuenta, tipo_cuenta, ref Error, pUsuario);
                return pCredito;
            }
            catch (Exception ex)
            {
                Error += ex.Message;
                return null;
            }
        }


        /// <summary>
        /// Realizar el desembolso del crèdito
        /// </summary>
        /// <param name="pCredito"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public Credito DesembolsarCreditoMasivo(Credito pCredito, bool opcion, long formadesembolso, int idCtaBancaria, int cod_banco, string numerocuenta, int tipo_cuenta, ref string Error, Usuario pUsuario)
        {
            Error = "";
            try
            {
                pCredito = BOCredito.DesembolsarCreditoMasivo(pCredito, opcion, formadesembolso, idCtaBancaria, cod_banco, numerocuenta, tipo_cuenta, ref Error, pUsuario);
                return pCredito;
            }
            catch (Exception ex)
            {
                Error += ex.Message;
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar Credito
        /// </summary>
        /// <param name="pId">identificador de Credito</param>
        /// <param name="idDeudor">identificador del deudor</param>
        public void EliminarCredito(Int64 pId, long idDeudor, Usuario pUsuario)
        {
            try
            {
                BOCredito.EliminarCredito(pId, idDeudor, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarCredito", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener Credito
        /// </summary>
        /// <param name="pId">identificador de Credito</param>
        /// <returns>Entidad Credito</returns>
        public Credito ConsultarCredito(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOCredito.ConsultarCredito(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "ConsultarCredito", ex);
                return null;
            }
        }


        //  Servicio para consultar el analisis promedio (Modulo Analisis Credito)
        public List<AnalisisPromedio> ConsultarAnalisisPromedio(string cod_linea, Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOCredito.ConsultarAnalisisPromedio(cod_linea, pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "ConsultarAnalisisPromedio", ex);
                return null;
            }
        }

        public List<AnalisisPromedio> ConsultarCalificacionHistorial(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOCredito.ConsultarCalificacionHistorial(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "ConsultarAnalisisPromedio", ex);
                return null;
            }
        }


        //Servicio para crear informacion de analisis de credito 
        public bool CrearAnalisis_Info(AnalisisInfo analisisInfo, Usuario pUsuario)
        {
            try
            {
                return BOCredito.CrearAnalisis_Info(analisisInfo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "CrearAnalisis_Info", ex);
                return false;
            }
        }
        //Consultar Informacion Analisis de credito
        public AnalisisInfo ConsultarAnalisis_Info(long numeroRadicacion, int tipoPersona, Usuario pUsuario)
        {
            try
            {
                return BOCredito.ConsultarAnalisis_Info(numeroRadicacion, tipoPersona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "ConsultarAnalisis_Info", ex);
                return null;
            }
        }

        public List<Credito> ListarCreditosPorFiltro(string filtroDefinido, string filtroGrilla, Usuario pUsuario)
        {
            try
            {
                return BOCredito.ListarCreditosPorFiltro(filtroDefinido, filtroGrilla, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "ListarCreditosPorFiltro", ex);
                return null;
            }
        }



        public Credito ConsultarCreditoModSolicitud(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOCredito.ConsultarCreditoModSolicitud(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "ConsultarCreditoModSolicitud", ex);
                return null;
            }
        }


        public Credito ConsultarCreditoModCupoRotativo(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOCredito.ConsultarCreditoModCupoRotativo(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "ConsultarCreditoModCupoRotativo", ex);
                return null;
            }
        }
        public List<Cliente> ListarCodeudores(Int64 pnumeroradicacion, Usuario pUsuario)
        {
            try
            {
                return BOCredito.ListarCodeudores(pnumeroradicacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "ListarCodeudores", ex);
                return null;
            }
        }

        public CreditoOrdenServicio ConsultarCREDITO_OrdenServ(String pFiltro, Usuario pUsuario)
        {
            try
            {
                return BOCredito.ConsultarCREDITO_OrdenServ(pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "ConsultarCREDITO_OrdenServ", ex);
                return null;
            }
        }

        public List<Credito> ConsultarCuotas(long radicacion, Usuario pUsuario)
        {
            try
            {
                return BOCredito.ConsultarCuotas(radicacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "ConsultarCredito", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de Creditos a partir de unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<Credito> ListarCredito(Credito pCredito, Usuario pUsuario, String filtro)
        {
            try
            {
                return BOCredito.ListarCredito(pCredito, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "ListarCredito", ex);
                return null;
            }
        }

        public List<Credito> ListarCreditoActivos(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOCredito.ListarCreditoActivos(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "ListarCredito", ex);
                return null;
            }
        }

        public List<Credito> ListarCarteraActiva(long codPersona, Usuario usuario)
        {
            try
            {
                return BOCredito.ListarCarteraActiva(codPersona, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "ListarCarteraActiva", ex);
                return null;
            }
        }



        public List<Credito> ConsultarCreditoTerminado(Int64 pCredito, Usuario pUsuario)
        {
            try
            {
                return BOCredito.ConsultarCreditoTerminado(pCredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "ListarCredito", ex);
                return null;
            }
        }

        public List<Credito> ListarCreditoAsociados(Int64 pCodPersona, DateTime pFecha, Usuario pUsuario)
        {
            try
            {
                return BOCredito.ListarCreditoAsociados(pCodPersona, pFecha, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "ListarCreditoAsociados", ex);
                return null;
            }
        }

        //Anderson acuña---Reporte Credito Desembolsados
        public List<Credito> ListarCreditosDesembolsados(Credito pCredito, Usuario pUsuario, String filtro)
        {
            try
            {
                return BOCredito.ListarCreditosDesembolsados(pCredito, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "ListarCredito", ex);
                return null;
            }
        }


        //Anderson acuña--- Reporte Cartera
        public List<Credito> ListarCartera(Credito pCredito, Usuario pUsuario, String filtro, String fechaAct)
        {
            try
            {
                return BOCredito.ListarCartera(pCredito, pUsuario, filtro, fechaAct);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "ListarCredito", ex);
                return null;
            }
        }


        public Credito MODIFICARcredito(Credito pcredito, Usuario pusuario)
        {
            try
            {
                pcredito = BOCredito.MODIFICARcredito(pcredito, pusuario);
                return pcredito;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("creditoService", "Modificarcredito", ex);
                return null;
            }
        }
        /// <summary>
        /// Servicio para obtener lista de Creditos a partir de unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<Credito> ListarCreditoDocumRequeridos(Credito pCredito, Usuario pUsuario, String filtro)
        {
            try
            {
                return BOCredito.ListarCreditoDocumRequeridos(pCredito, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "ListarCreditoDocumRequeridos", ex);
                return null;
            }
        }

        public List<LineasCredito> ListarAtributosFinanciados(long pNumRadicacion, Usuario pUsuario)
        {
            try
            {
                return BOCredito.ListarAtributosFinanciados(pNumRadicacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "ListarAtributosFinanciados", ex);
                return null;
            }
        }

        public List<Credito> ListarCreditoMasivo(Credito pCredito, DateTime pFechaIni, DateTime pFechaFin, Usuario pUsuario, String filtro)
        {
            try
            {
                return BOCredito.ListarCreditoMasivo(pCredito, pFechaIni, pFechaFin, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "ListarCredito", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener Credito
        /// </summary>
        /// <param name="pId">identificador de Credito</param>
        /// <returns>Entidad Credito</returns>
        public Credito ConsultarCreditoAsesor(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOCredito.ConsultarCreditoAsesor(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "ConsultarCreditoAsesor", ex);
                return null;
            }
        }
        /// <summary>
        /// Servicio para obtener el parametro para habeas Data
        /// </summary>
        /// <param name="pId">identificador de Habeas Data</param>
        /// <returns>Entidad Parametro Habeas Data</returns>
        public Credito ConsultarParametroHabeas(Usuario pUsuario)
        {
            try
            {
                return BOCredito.ConsultarParametroHabeas(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "ConsultarParametroHabeas", ex);
                return null;
            }
        }
        /// <summary>
        /// Servicio para obtener el parametro para CobroJuridico
        /// </summary>
        /// <param name="pId">identificador de CobroJuridico
        /// <returns>Entidad ParametroCobroJuridico</returns>
        public Credito ConsultarParametroCobroJuridico(Usuario pUsuario)
        {
            try
            {
                return BOCredito.ConsultarParametroCobroJuridico(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "ConsultarParametroCobroJuridico", ex);
                return null;
            }
        }
        /// <summary>
        /// Servicio para obtener el parametro para CobroPreJuridico
        /// </summary>
        /// <param name="pId">identificador de CobroPreJuridico
        /// <returns>Entidad ParametroCobroJuridico</returns>
        public Credito ConsultarParametroCobroPreJuridico(Usuario pUsuario)
        {
            try
            {
                return BOCredito.ConsultarParametroCobroPreJuridico(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "ConsultarParametroCobroPreJuridico", ex);
                return null;
            }
        }
        /// <summary>
        /// Servicio para obtener lista de Creditos a partir de unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<Credito> ListarCreditoAsesor(Credito pCredito, Usuario pUsuario, String filtro, String orden)
        {
            try
            {
                return BOCredito.ListarCreditoAsesor(pCredito, pUsuario, filtro, orden);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "ListarCreditoAsesor", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener Credito
        /// </summary>
        /// <param name="pId">identificador de Credito</param>
        /// <returns>Entidad Credito</returns>
        public Credito ConsultarCreditoPorObligacion(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOCredito.ConsultarCreditoPorObligacion(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "ConsultarCreditoPorObligacion", ex);
                return null;
            }
        }


        public Credito ConsultarCreditoSolicitud(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOCredito.ConsultarCreditoSolicitud(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "ConsultarCreditoSolicitud", ex);
                return null;
            }
        }

        public Analisis_Credito ListarAnalisisCredito(Analisis_Credito analisisCredito, Usuario _usuario)
        {
            try
            {
                return BOCredito.ListarAnalisisCredito(analisisCredito, _usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "ListarAnalisis", ex);
                return null;
            }
        }

        public Credito consultarinterescredito(Int64 pnumero_radicacion, DateTime pfecha_pago, Usuario pUsuario, long numeroRadicacion = 0)
        {
            try
            {
                return BOCredito.consultarinterescredito(pnumero_radicacion, pfecha_pago, pUsuario, numeroRadicacion);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "consultarinterescredito", ex);
                return null;
            }
        }


        public Decimal AmortizarCredito(Int64 pnumero_radicacion, Int64 ptipo_pago, DateTime pfecha_pago, Usuario pUsuario)
        {
            try
            {
                return BOCredito.AmortizarCredito(pnumero_radicacion, ptipo_pago, pfecha_pago, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "AmortizarCredito", ex);
                return 0;
            }
        }

        public Decimal AmortizarCreditoNumCuotas(Int64 pnumero_radicacion, DateTime pfecha_pago, int pnum_cuotas, Usuario pUsuario)
        {
            try
            {
                return BOCredito.AmortizarCreditoNumCuotas(pnumero_radicacion, pfecha_pago, pnum_cuotas, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "AmortizarCreditoNumCuotas", ex);
                return 0;
            }
        }

        public List<Atributos> AmortizarCreditoDetalle(Int64 pnumero_radicacion, DateTime pfecha_pago, Double pvalor_pago, Int64 ptipo_pago, ref Int64 pError, Usuario pUsuario)
        {
            try
            {
                return BOCredito.AmortizarCreditoDetalle(pnumero_radicacion, pfecha_pago, pvalor_pago, ptipo_pago, ref pError, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "AmortizarCreditoDetalle", ex);
                return null;
            }
        }


        public void guardardescuento(DescuentosDesembolso variable, Usuario pUsuario)
        {
            try
            {
                BOCredito.guardardescuento(variable, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "ConsultarCreditoSolicitud", ex);
            }
        }

        public List<Credito> Consultardescuentos(Usuario pUsuario)
        {
            try
            {
                return BOCredito.Consultardescuentos(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "ConsultarCredito", ex);
                return null;
            }
        }

        public string ValidarCredito(Credito pEntidad, Usuario pUsuario)
        {
            try
            {
                return BOCredito.ValidarCredito(pEntidad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "ValidarCrédito", ex);
                return ex.Message;
            }
        }


        public CreditoEmpresaRecaudo CrearModEmpresa_Recaudo(CreditoEmpresaRecaudo pEmpresa, Usuario vUsuario, int opcion)
        {
            try
            {
                return BOCredito.CrearModEmpresa_Recaudo(pEmpresa, vUsuario, opcion);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "CrearModEmpresa_Recaudo", ex);
                return null;
            }
        }

        public Int64? AutorizarCredito(Int64 pnumero_radicacion, Int64 pcod_deudor, DateTime pfecha_desembolso, ref string pError, Usuario pUsuario)
        {
            try
            {
                return BOCredito.AutorizarCredito(pnumero_radicacion, pcod_deudor, pfecha_desembolso, ref pError, pUsuario);
            }
            catch
            {
                return null;
            }
        }


        public Int32 VerificarAutorizacion(int pTipoProducto, string pNumeroProducto, DateTime pFecha, String pIp, String pAutorizacion, Usuario pUsuario)
        {
            try
            {
                return BOCredito.VerificarAutorizacion(pTipoProducto, pNumeroProducto, pFecha, pIp, pAutorizacion, pUsuario);
            }
            catch
            {
                return 0;
            }
        }

        public Int32 VerificarAutorizacion(String pAutorizacion, Usuario pUsuario)
        {
            try
            {
                return BOCredito.VerificarAutorizacion(pAutorizacion, pUsuario);
            }
            catch
            {
                return 0;
            }
        }

        public DateTime? FechaInicioDESEMBOLSO(Int64 pNumero_radicacion, Usuario vUsuario)
        {
            try
            {
                return BOCredito.FechaInicioDESEMBOLSO(pNumero_radicacion, vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "FechaInicioDESEMBOLSO", ex);
                return null;
            }
        }

        public DateTime? FechaInicioCredito(DateTime pFecha_desembolso, Int32 pCodperiodicidad, Int32 pFormapago, Int32? pCodEmpresa, string pCodLinea, ref string pError, ref Boolean pRpta, Usuario vUsuario)
        {
            try
            {
                return BOCredito.FechaInicioCredito(pFecha_desembolso, pCodperiodicidad, pFormapago, pCodEmpresa, pCodLinea, ref pError, ref pRpta, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "FechaInicioCredito", ex);
                return null;
            }
        }

        public void CrearSolicitudCreditoServices(Usuario pUsuario, CreditoOrdenServicio pCredOrden, Auxilio_Orden_Servicio pAuxOrden, CreditoEducativoEntit pLineaCredito, Reestructuracion vReestructuracion, ref Int64 numero_radicacion, ref string error, SolicitudAuxilio pServicio, List<DetalleSolicitudAuxilio> lstDetalle)
        {
            try
            {
                BOCredito.CrearSolicitudCredito(pUsuario, pCredOrden, pAuxOrden, pLineaCredito, vReestructuracion, ref numero_radicacion, ref error, pServicio, lstDetalle);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "CrearSolicitudCreditoServices", ex);
            }
        }

        public List<Credito> RealizarPreAnalisis(bool preanalisis, DateTime pfecha, Int64 pCodPersona, decimal pDisponible, Int64 pNumeroCuotas, decimal pMontoSolicitado, Int32 pCodPeriodicidad, bool pEducativo, Usuario pUsuario)
        {
            try
            {
                return BOCredito.RealizarPreAnalisis(preanalisis, pfecha, pCodPersona, pDisponible, pNumeroCuotas, pMontoSolicitado, pCodPeriodicidad, pEducativo, pUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "RealizarPreAnalisis", ex);
                return null;
            }
        }


        public Int64 ObtenerNumeroPreImpreso(Usuario pUsuario)
        {
            try
            {
                return BOCredito.ObtenerNumeroPreImpreso(pUsuario);
            }
            catch
            {
                return 1;
            }
        }

        ///AGREGADO
        ///
        public Credito CREARCREDITOANALISIS(Credito pCredito, Usuario pusuario)
        {
            try
            {
                pCredito = BOCredito.CREARCREDITOANALISIS(pCredito, pusuario);
                return pCredito;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "CrearCredito", ex);
                return null;
            }
        }

        public decimal ObtenerSaldoTotalXpersona(Int64 pCodPersona, Usuario pUsuario)
        {
            try
            {
                return BOCredito.ObtenerSaldoTotalXpersona(pCodPersona, pUsuario);
            }
            catch
            {
                return 1;
            }
        }
        /// <summary>
        /// Realizar el desembolso del crèdito
        /// </summary>
        /// <param name="pCredito"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public Credito DesembolsarAvances(Credito pCredito, bool opcion, long formadesembolso, int idCtaBancaria, int cod_banco, string numerocuenta, int tipo_cuenta, ref string Error, Usuario pUsuario)
        {
            Error = "";
            try
            {
                pCredito = BOCredito.DesembolsarAvances(pCredito, opcion, formadesembolso, idCtaBancaria, cod_banco, numerocuenta, tipo_cuenta, ref Error, pUsuario);
                return pCredito;
            }
            catch (Exception ex)
            {
                Error += ex.Message;
                return null;
            }
        }

        public List<Credito> ListarCreditosEducativos(DateTime pFecha, string pFiltro, Usuario pUsuario)
        {
            try
            {
                return BOCredito.ListarCreditosEducativos(pFecha, pFiltro, pUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "ListarCreditosEducativos", ex);
                return null;
            }
        }


        //Anderson acuña---Reporte Credito Desembolsados
        public List<Credito> Reporte1026(Credito pCredito, Usuario pUsuario, String filtro)
        {
            try
            {
                return BOCredito.Reporte1026(pCredito, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "Reporte1026", ex);
                return null;
            }
        }


        //Anderson Cargue Masivo Creditos
        public void CargarCreditos(ref string pError, Stream pstream, ref List<Credito> lstCreditos, ref List<Entities.ErroresCarga> plstErrores, Usuario pUsuario)
        {
            try
            {
                BOCredito.CargarCreditos(ref pError, pstream, ref lstCreditos, ref plstErrores, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "CargaCreditos", ex);
            }
        }


        //Anderson Guardar Creditos Masivo
        public void CrearImportacionCred(List<Credito> lstCreditos, ref string pError, ref List<Credito> lst_Num_cred, Usuario pUsuario)
        {
            try
            {
                BOCredito.CrearImportacionCred(lstCreditos, ref pError, ref lst_Num_cred, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "CrearAporteImportacion", ex);
            }
        }

        public Credito ConsultarCierreCartera(Usuario vUsuario)
        {
            try
            {
                return BOCredito.ConsultarCierreCartera(vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "ConsultarCierreCartera", ex);
                return null;
            }
        }

        public bool ModificarDescuentos(List<DescuentosCredito> plstDescuentosCredito, Usuario pUsuario)
        {
            try
            {
                return BOCredito.ModificarDescuentos(plstDescuentosCredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "ModificarDescuentos", ex);
                return false;
            }
        }


    }
}