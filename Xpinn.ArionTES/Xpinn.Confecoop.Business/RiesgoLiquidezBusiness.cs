using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Confecoop.Entities;
using Xpinn.Confecoop.Data;
using Xpinn.Util;

namespace Xpinn.Confecoop.Business
{
    public class RiesgoLiquidezBusiness : GlobalData
    {
        RiesgoLiquidezData DARiesgo;
        public RiesgoLiquidezBusiness()
        {
            DARiesgo = new RiesgoLiquidezData();
        }

        public Boolean CrearRiesgoLiquidez(List<RiesgoLiquidez> lstRiesgoLiquidez, ref string pError, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (lstRiesgoLiquidez.Count > 0)
                    {
                        foreach (RiesgoLiquidez nRiesgo in lstRiesgoLiquidez)
                        {
                            RiesgoLiquidez pEntidad = new RiesgoLiquidez();
                            pEntidad = DARiesgo.CrearRiesgoLiquidez(nRiesgo, vUsuario);
                        }
                    }
                    ts.Complete();
                }
                return true;
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                return false;
            }
        }

        public List<RiesgoLiquidez> ListarProyeccionRiesgoLiquidez(RiesgoLiquidez riesgo, TipoProyeccionRiesgoLiquidez tipoProyeccion, Usuario usuario)
        {
            try
            {
                List<RiesgoLiquidez> lstProyeccion = null;
                long cuentaRestar = 0;

                switch (tipoProyeccion)
                {
                    case TipoProyeccionRiesgoLiquidez.Disponible:
                        cuentaRestar = 1120;
                        lstProyeccion = DARiesgo.ListarProyeccionDisponible(riesgo, usuario);
                        break;
                    case TipoProyeccionRiesgoLiquidez.AhorroPermanente:
                        cuentaRestar = 2105;
                        lstProyeccion = DARiesgo.ListarProyeccionAhorro(riesgo, usuario);
                        break;
                    case TipoProyeccionRiesgoLiquidez.Cartera:
                        lstProyeccion = DARiesgo.ListarProyeccionCartera(riesgo, usuario);
                        break;
                    case TipoProyeccionRiesgoLiquidez.Aporte:
                        lstProyeccion = DARiesgo.ListarProyeccionAporte(riesgo, usuario);
                        break;
                    default:
                        throw new NotSupportedException("Tipo de reporte no soportado!.");
                }

                if (tipoProyeccion == TipoProyeccionRiesgoLiquidez.Aporte)
                {
                    lstProyeccion.ForEach(x => x.vr_brecha7 = x.saldo_actual + x.vr_brecha1 + x.vr_brecha2 + x.vr_brecha3 + x.vr_brecha4 + x.vr_brecha5 + x.vr_brecha6);
                }
                else if (tipoProyeccion != TipoProyeccionRiesgoLiquidez.Cartera)
                {
                    lstProyeccion = AgruparYOrdenarListaProyeccionPorDias(lstProyeccion);
                    lstProyeccion = PrepararBrechasListaProyeccion(lstProyeccion, riesgo, cuentaRestar);
                }

                return lstProyeccion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RiesgoLiquidezBusiness", "ListarProyeccionRiesgoLiquidez", ex);
                return null;
            }
        }

        List<RiesgoLiquidez> AgruparYOrdenarListaProyeccionPorDias(List<RiesgoLiquidez> lstProyeccion)
        {
            lstProyeccion = (from riesgoSaldoSumar in lstProyeccion
                             orderby riesgoSaldoSumar.fecha
                             group riesgoSaldoSumar by new { day = riesgoSaldoSumar.fecha.Day } into fullLista
                             select new RiesgoLiquidez()
                             {
                                 dia = fullLista.Key.day,
                                 grupo = fullLista.ToList()
                             }).ToList();

            return lstProyeccion;
        }

        List<RiesgoLiquidez> PrepararBrechasListaProyeccion(List<RiesgoLiquidez> lstProyeccion, RiesgoLiquidez liquidez, long cuentaRestar = 0, bool agruparEnTrimestre = false)
        {
            DateTimeHelper dateHelper = new DateTimeHelper();
            DateTime fechaCorte = dateHelper.UltimoDiaDelMes(liquidez.fecha_corte.Value);
            DateTime fechaInicial = dateHelper.PrimerDiaDelMes(fechaCorte.AddYears(-1).AddMonths(1));

            for (int i = 0; i < lstProyeccion.Count; i++)
            {
                RiesgoLiquidez riesgoConGrupo = lstProyeccion[i];

                for (int x = 0; x < riesgoConGrupo.grupo.Count; x++)
                {
                    RiesgoLiquidez riesgo = riesgoConGrupo.grupo[x];

                    if (cuentaRestar != 0 && riesgo.cod_cuenta == cuentaRestar)
                    {
                        riesgo.saldo_madurar *= -1;
                    }

                    if (riesgo.fecha <= dateHelper.UltimoDiaDelMes(fechaInicial))
                    {
                        if (!riesgoConGrupo.vr_brecha1.HasValue) riesgoConGrupo.vr_brecha1 = 0;
                        riesgoConGrupo.vr_brecha1 += riesgo.saldo_madurar;
                    }
                    else if (fechaInicial.AddMonths(1) <= riesgo.fecha && riesgo.fecha <= dateHelper.UltimoDiaDelMes(fechaInicial.AddMonths(1)))
                    {
                        if (!riesgoConGrupo.vr_brecha2.HasValue) riesgoConGrupo.vr_brecha2 = 0;
                        riesgoConGrupo.vr_brecha2 += riesgo.saldo_madurar;
                    }
                    else if (fechaInicial.AddMonths(2) <= riesgo.fecha && riesgo.fecha <= dateHelper.UltimoDiaDelMes(fechaInicial.AddMonths(2)))
                    {
                        if (!riesgoConGrupo.vr_brecha3.HasValue) riesgoConGrupo.vr_brecha3 = 0;
                        riesgoConGrupo.vr_brecha3 += riesgo.saldo_madurar;
                    }

                    if (agruparEnTrimestre)
                    {
                        CalcularUltimosTresTrimestres(dateHelper, fechaCorte, fechaInicial, riesgoConGrupo, riesgo);
                    }
                    else
                    {
                        CalcularUltimosNueveMeses(dateHelper, fechaCorte, fechaInicial, riesgoConGrupo, riesgo);
                    }
                }
            }

            return lstProyeccion;
        }

        void CalcularUltimosTresTrimestres(DateTimeHelper dateHelper, DateTime fechaCorte, DateTime fechaInicial, RiesgoLiquidez riesgoConGrupo, RiesgoLiquidez riesgo)
        {
            if (fechaInicial.AddMonths(3) <= riesgo.fecha && riesgo.fecha <= dateHelper.UltimoDiaDelMes(fechaInicial.AddMonths(5)))
            {
                if (!riesgoConGrupo.vr_brecha4.HasValue) riesgoConGrupo.vr_brecha4 = 0;
                riesgoConGrupo.vr_brecha4 += riesgo.saldo_madurar;
            }
            else if (fechaInicial.AddMonths(6) <= riesgo.fecha && riesgo.fecha <= dateHelper.UltimoDiaDelMes(fechaInicial.AddMonths(8)))
            {
                if (!riesgoConGrupo.vr_brecha5.HasValue) riesgoConGrupo.vr_brecha5 = 0;
                riesgoConGrupo.vr_brecha5 += riesgo.saldo_madurar;
            }
            else if (fechaInicial.AddMonths(9) <= riesgo.fecha && riesgo.fecha <= fechaCorte)
            {
                if (!riesgoConGrupo.vr_brecha6.HasValue) riesgoConGrupo.vr_brecha6 = 0;
                riesgoConGrupo.vr_brecha6 += riesgo.saldo_madurar;
            }
        }

        void CalcularUltimosNueveMeses(DateTimeHelper dateHelper, DateTime fechaCorte, DateTime fechaInicial, RiesgoLiquidez riesgoConGrupo, RiesgoLiquidez riesgo)
        {
            if (fechaInicial.AddMonths(3) <= riesgo.fecha && riesgo.fecha <= dateHelper.UltimoDiaDelMes(fechaInicial.AddMonths(3)))
            {
                if (!riesgoConGrupo.vr_brecha4.HasValue) riesgoConGrupo.vr_brecha4 = 0;
                riesgoConGrupo.vr_brecha4 += riesgo.saldo_madurar;
            }
            else if (fechaInicial.AddMonths(4) <= riesgo.fecha && riesgo.fecha <= dateHelper.UltimoDiaDelMes(fechaInicial.AddMonths(4)))
            {
                if (!riesgoConGrupo.vr_brecha5.HasValue) riesgoConGrupo.vr_brecha5 = 0;
                riesgoConGrupo.vr_brecha5 += riesgo.saldo_madurar;
            }
            else if (fechaInicial.AddMonths(5) <= riesgo.fecha && riesgo.fecha <= dateHelper.UltimoDiaDelMes(fechaInicial.AddMonths(5)))
            {
                if (!riesgoConGrupo.vr_brecha6.HasValue) riesgoConGrupo.vr_brecha6 = 0;
                riesgoConGrupo.vr_brecha6 += riesgo.saldo_madurar;
            }
            else if (fechaInicial.AddMonths(6) <= riesgo.fecha && riesgo.fecha <= dateHelper.UltimoDiaDelMes(fechaInicial.AddMonths(6)))
            {
                if (!riesgoConGrupo.vr_brecha7.HasValue) riesgoConGrupo.vr_brecha7 = 0;
                riesgoConGrupo.vr_brecha7 += riesgo.saldo_madurar;
            }
            else if (fechaInicial.AddMonths(7) <= riesgo.fecha && riesgo.fecha <= dateHelper.UltimoDiaDelMes(fechaInicial.AddMonths(7)))
            {
                if (!riesgoConGrupo.vr_brecha8.HasValue) riesgoConGrupo.vr_brecha8 = 0;
                riesgoConGrupo.vr_brecha8 += riesgo.saldo_madurar;
            }
            else if (fechaInicial.AddMonths(8) <= riesgo.fecha && riesgo.fecha <= dateHelper.UltimoDiaDelMes(fechaInicial.AddMonths(8)))
            {
                if (!riesgoConGrupo.vr_brecha9.HasValue) riesgoConGrupo.vr_brecha9 = 0;
                riesgoConGrupo.vr_brecha9 += riesgo.saldo_madurar;
            }
            else if (fechaInicial.AddMonths(9) <= riesgo.fecha && riesgo.fecha <= dateHelper.UltimoDiaDelMes(fechaInicial.AddMonths(9)))
            {
                if (!riesgoConGrupo.vr_brecha10.HasValue) riesgoConGrupo.vr_brecha10 = 0;
                riesgoConGrupo.vr_brecha10 += riesgo.saldo_madurar;
            }
            else if (fechaInicial.AddMonths(10) <= riesgo.fecha && riesgo.fecha <= dateHelper.UltimoDiaDelMes(fechaInicial.AddMonths(10)))
            {
                if (!riesgoConGrupo.vr_brecha11.HasValue) riesgoConGrupo.vr_brecha11 = 0;
                riesgoConGrupo.vr_brecha11 += riesgo.saldo_madurar;
            }
            else if (fechaInicial.AddMonths(11) <= riesgo.fecha && riesgo.fecha <= fechaCorte)
            {
                if (!riesgoConGrupo.vr_brecha12.HasValue) riesgoConGrupo.vr_brecha12 = 0;
                riesgoConGrupo.vr_brecha12 += riesgo.saldo_madurar;
            }
        }

        #region Calculos de riesgo Liquidez

        public decimal CalcularSaldoContableRango(string pCuentaIni, string pCuentaFin, int pOperador, DateTime pFecha, bool pNiif, Usuario vUsuario)
        {
            decimal pSaldo_actual = 0;
            // pOperador  0 = SUMAR , 1 = RESTAR
            if (pOperador == 0)
                pSaldo_actual = DARiesgo.CalculoSaldoContable(pFecha, pCuentaIni, pNiif, vUsuario) + DARiesgo.CalculoSaldoContable(pFecha, pCuentaFin, pNiif, vUsuario);
            else
                pSaldo_actual = DARiesgo.CalculoSaldoContable(pFecha, pCuentaIni, pNiif, vUsuario) - DARiesgo.CalculoSaldoContable(pFecha, pCuentaFin, pNiif, vUsuario);
            return pSaldo_actual;
        }

        public decimal CalcularSaldoContable(string pCuenta, DateTime pFecha, bool pNiif, Usuario vUsuario)
        {
            decimal pSaldo_actual = 0;
            pSaldo_actual = DARiesgo.CalculoSaldoContable(pFecha, pCuenta, pNiif, vUsuario);
            return pSaldo_actual;
        }

        /// <summary>
        /// Maduración del disponible.
        /// </summary>
        /// <param name="pFecha"></param>
        /// <param name="pNiif"></param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public RiesgoLiquidez CalcularDisponibleRiesgoLiquidez(DateTime pFecha, bool pNiif, Usuario vUsuario)
        {
            try
            {
                List<RiesgoLiquidez> lstFecFinMes = new List<RiesgoLiquidez>();
                decimal pSaldoTotal = 0, pSaldo_actual = 0, pSaldoPromedio = 0, pSaldoDia = 0, pSaldoCopia = 0;

                //DateTime pFecha_Ini = new DateTime((pFecha.AddYears(-1)).Year, pFecha.Month, 1);
                DateTime pFecha_Ini = pFecha.AddDays(1).AddYears(-1);

                //Calculando el saldo a la fecha de corte
                pSaldo_actual = DARiesgo.CalculoSaldoContable(pFecha, "11", pNiif, vUsuario) - DARiesgo.CalculoSaldoContable(pFecha, "1120", pNiif, vUsuario);
                pSaldoDia = DARiesgo.CalculoSaldoContable(pFecha_Ini, "11", pNiif, vUsuario) - DARiesgo.CalculoSaldoContable(pFecha_Ini, "1120", pNiif, vUsuario);
                pSaldoCopia = pSaldoDia;

                //Recorriendo los 365 dias
                decimal totCambio = 0;
                decimal variacion = 0;
                int contador = 0;
                while (pFecha_Ini <= pFecha)
                {
                    //Ubicando la fecha del ultimo mes
                    DateTime pFechaUltMes = DateTime.ParseExact("01/" + pFecha_Ini.AddMonths(1).ToString("MM/yyyy"), "dd/MM/yyyy", null);
                    pFechaUltMes = pFechaUltMes.AddDays(-1);

                    if (!DARiesgo.SaldoDiario(pFecha_Ini, "11", ref pSaldoDia, pNiif, vUsuario))
                    {
                        pSaldoDia += DARiesgo.CalculoMovimientoContable(pFecha_Ini, "11%", pNiif, vUsuario) - DARiesgo.CalculoMovimientoContable(pFecha_Ini, "1120%", pNiif, vUsuario);
                    }
                    pSaldoTotal += pSaldoDia;

                    //Si es el último día del mes entonces guardar el saldo para tener saldo mensual
                    if (pFecha_Ini == pFechaUltMes)
                    {
                        RiesgoLiquidez pCierre = new RiesgoLiquidez();
                        pCierre.fecha_corte = pFecha_Ini;
                        pCierre.saldo_actual = pSaldoDia;
                        lstFecFinMes.Add(pCierre);
                        if (contador == 0)
                            variacion = Convert.ToDecimal(lstFecFinMes[contador].saldo_actual - pSaldoCopia);
                        else
                            variacion = Convert.ToDecimal(lstFecFinMes[contador].saldo_actual - lstFecFinMes[contador - 1].saldo_actual);
                        contador += 1;
                        // Si hay disminución madura en la banda correspondiente
                        if (variacion < 0)
                        {
                            totCambio += -variacion;
                        }
                    }

                    pFecha_Ini = pFecha_Ini.AddDays(1);
                }


                //Cargando Datos
                RiesgoLiquidez pEntidad = new RiesgoLiquidez();
                pEntidad.unidad_captura = 1;
                pEntidad.renglon = 1;
                pEntidad.saldo_actual = pSaldo_actual;
                pSaldoPromedio = pSaldoTotal / 365;
                //Maduracion por bandas
                if (pSaldo_actual > pSaldoPromedio)
                {
                    if (lstFecFinMes.Count > 0)
                    {
                        decimal valorX = 0, banda1 = 0, banda2 = 0, banda3 = 0, banda4 = 0, banda5 = 0, banda6 = 0;
                        decimal controlCambio = 0;
                        DateTime pFecha_x;

                        for (int i = 0; i < lstFecFinMes.Count; i++)
                        {
                            lstFecFinMes[i].fecha_corte = lstFecFinMes[i].fecha_corte != null ? lstFecFinMes[i].fecha_corte : DateTime.MinValue;
                            lstFecFinMes[i].saldo_actual = lstFecFinMes[i].saldo_actual != null ? lstFecFinMes[i].saldo_actual : 0;
                            pFecha_x = Convert.ToDateTime(lstFecFinMes[i].fecha_corte);
                            // Determinar si hay disminución o aumento
                            decimal cambio = 0;
                            if (i == 0)
                                cambio = Convert.ToDecimal(lstFecFinMes[i].saldo_actual - pSaldoCopia);
                            else
                                cambio = Convert.ToDecimal(lstFecFinMes[i].saldo_actual - lstFecFinMes[i - 1].saldo_actual);
                            // Si hay disminución madura en la banda correspondiente
                            if (cambio < 0)
                            {
                                valorX = Math.Round(pSaldo_actual * (-cambio / totCambio));
                                if (i >= 0 && i < 1)
                                    banda1 += valorX;
                                if (i > 1 && i <= 2)
                                    banda2 += valorX;
                                if (i > 2 && i <= 3)
                                    banda3 += valorX;
                                if (i > 3 && i <= 6)
                                    banda4 += valorX;
                                if (i > 6 && i <= 9)
                                    banda5 += valorX;
                                if (i > 9 && i <= 12)
                                    banda6 += pSaldo_actual - controlCambio;
                                controlCambio += valorX;
                            }
                        }
                        //Cargando bandas
                        pEntidad.vr_brecha1 = banda1;
                        pEntidad.vr_brecha2 = banda2;
                        pEntidad.vr_brecha3 = banda3;
                        pEntidad.vr_brecha4 = banda4;
                        pEntidad.vr_brecha5 = banda5;
                        pEntidad.vr_brecha6 = banda6;
                    }
                }
                else
                {
                    // Si el saldo actual es menor que el saldo promedio anual diario entonces madura en la última banda
                    pEntidad.vr_brecha7 = pSaldo_actual;
                }

                return pEntidad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RiesgoLiquidezBusiness", "CalcularDatosRiesgoLiquidez", ex);
                return null;
            }
        }

        public RiesgoLiquidez CalcularMaduracionCartera(DateTime pFecha, int pCod_Clasificacion, Usuario vUsuario)
        {            

            decimal pSaldo_actual = 0;

            if (pCod_Clasificacion != 0)
                pSaldo_actual = DARiesgo.CarteraSaldoActual(pFecha, pCod_Clasificacion, vUsuario);
            else
                pSaldo_actual = DARiesgo.CarteraSaldoActualEmpleados(pFecha, vUsuario);

            RiesgoLiquidez pEntidad = new RiesgoLiquidez();
            pEntidad.saldo_actual = pSaldo_actual;

            string pFiltro = string.Empty;
            if (manejaLineasCreditoEmpleados(vUsuario))
                if (pCod_Clasificacion != 0)
                    pFiltro = " AND C.COD_LINEA_CREDITO NOT IN (SELECT X.COD_LINEA_CREDITO FROM LINEASCREDITO X WHERE X.APLICA_EMPLEADO = 1) AND C.COD_CLASIFICA = " + pCod_Clasificacion;
                else
                    pFiltro = " AND C.COD_LINEA_CREDITO IN (SELECT X.COD_LINEA_CREDITO FROM LINEASCREDITO X WHERE X.APLICA_EMPLEADO = 1)";
            else
                if (pCod_Clasificacion != 0)
                    pFiltro = " C.COD_CLASIFICA = " + pCod_Clasificacion;
                else
                    pFiltro = " ";
            //Maduracion por Bandas
            DateTime pFec1 = new DateTime(pFecha.Year, pFecha.Month, 1);
            DateTime pFec2 = pFecha;
            pFec1 = pFecha.AddDays(1);
            pFec2 = pFecha.AddMonths(1);
            pEntidad.vr_brecha1 = DARiesgo.CarteraMaduracionBandas(pFecha, pFec1, pFec2, pFiltro, vUsuario);
            pFec1 = pFecha.AddDays(1).AddMonths(1);
            pFec2 = pFecha.AddMonths(2);
            pEntidad.vr_brecha2 = DARiesgo.CarteraMaduracionBandas(pFecha, pFec1, pFec2, pFiltro, vUsuario);
            pFec1 = pFecha.AddDays(1).AddMonths(2);
            pFec2 = pFecha.AddMonths(3);
            pEntidad.vr_brecha3 = DARiesgo.CarteraMaduracionBandas(pFecha, pFec1, pFec2, pFiltro, vUsuario);
            pFec1 = pFecha.AddDays(1).AddMonths(3);
            pFec2 = pFecha.AddMonths(6);
            pEntidad.vr_brecha4 = DARiesgo.CarteraMaduracionBandas(pFecha, pFec1, pFec2, pFiltro, vUsuario);
            pFec1 = pFecha.AddDays(1).AddMonths(6);
            pFec2 = pFecha.AddMonths(9);
            pEntidad.vr_brecha5 = DARiesgo.CarteraMaduracionBandas(pFecha, pFec1, pFec2, pFiltro, vUsuario);
            pFec1 = pFecha.AddDays(1).AddMonths(9);
            pFec2 = pFecha.AddMonths(12);
            pEntidad.vr_brecha6 = DARiesgo.CarteraMaduracionBandas(pFecha, pFec1, pFec2, pFiltro, vUsuario);
            return pEntidad;
        }

        public RiesgoLiquidez CalcularMaduracionCausacion(DateTime pFecha, int pCod_Clasificacion, Usuario vUsuario)
        {
            decimal pSaldo_actual = 0;

            if (pCod_Clasificacion != 0)
                pSaldo_actual = DARiesgo.CausacionSaldoActual(pFecha, pCod_Clasificacion, vUsuario);
            else
                pSaldo_actual = DARiesgo.CausacionSaldoActualEmpleados(pFecha, vUsuario);

            RiesgoLiquidez pEntidad = new RiesgoLiquidez();
            pEntidad.saldo_actual = pSaldo_actual;

            string pFiltro = string.Empty;
            if (manejaLineasCreditoEmpleados(vUsuario))
                if (pCod_Clasificacion != 0)
                    pFiltro = " AND H.COD_LINEA_CREDITO NOT IN (SELECT X.COD_LINEA_CREDITO FROM LINEASCREDITO X WHERE X.APLICA_EMPLEADO = 1) AND H.COD_CLASIFICA = " + pCod_Clasificacion;
                else
                    pFiltro = " AND H.COD_LINEA_CREDITO IN (SELECT X.COD_LINEA_CREDITO FROM LINEASCREDITO X WHERE X.APLICA_EMPLEADO = 1)";
            else
                if (pCod_Clasificacion != 0)
                pFiltro = " H.COD_CLASIFICA = " + pCod_Clasificacion;
            else
                pFiltro = " ";
            //Maduracion por Bandas
            DateTime pFec1 = new DateTime(pFecha.Year, pFecha.Month, 1);
            DateTime pFec2 = pFecha;
            pFec1 = pFecha.AddDays(1);
            pFec2 = pFecha.AddMonths(1);
            pEntidad.vr_brecha1 = DARiesgo.CausacionMaduracion(pFecha, pFec1, pFec2, pFiltro, vUsuario);
            pFec1 = pFecha.AddDays(1).AddMonths(1);
            pFec2 = pFecha.AddMonths(2);
            pEntidad.vr_brecha2 = DARiesgo.CausacionMaduracion(pFecha, pFec1, pFec2, pFiltro, vUsuario);
            pFec1 = pFecha.AddDays(1).AddMonths(2);
            pFec2 = pFecha.AddMonths(3);
            pEntidad.vr_brecha3 = DARiesgo.CausacionMaduracion(pFecha, pFec1, pFec2, pFiltro, vUsuario);
            pFec1 = pFecha.AddDays(1).AddMonths(3);
            pFec2 = pFecha.AddMonths(6);
            pEntidad.vr_brecha4 = DARiesgo.CausacionMaduracion(pFecha, pFec1, pFec2, pFiltro, vUsuario);
            pFec1 = pFecha.AddDays(1).AddMonths(6);
            pFec2 = pFecha.AddMonths(9);
            pEntidad.vr_brecha5 = DARiesgo.CausacionMaduracion(pFecha, pFec1, pFec2, pFiltro, vUsuario);
            pFec1 = pFecha.AddDays(1).AddMonths(9);
            pFec2 = pFecha.AddMonths(12);
            pEntidad.vr_brecha6 = DARiesgo.CausacionMaduracion(pFecha, pFec1, pFec2, pFiltro, vUsuario);
            return pEntidad;
        }


        public List<RiesgoLiquidez> ListarRiesgoLiquidez(DateTime pFechaCorte, bool pNiif, Usuario vUsuario)
        {
            List<RiesgoLiquidez> lstDatos = new List<RiesgoLiquidez>();
            lstDatos = ListarRiesgoLiquidezDatos();

            foreach (RiesgoLiquidez nLiquidez in lstDatos)
            {
                RiesgoLiquidez pEntidad;
                if (nLiquidez.unidad_captura == 1 && nLiquidez.renglon == 1)
                {
                    /* 
                     EFECTIVO Y EQUIVALENTE AL EFECTIVO. a.	Disponible - Cuentas 110000 – 112000: Se madura el saldo a la fecha 
                    (cuenta 110000 – 112000). Para la determinación de la porción permanente del disponible se calcula el monto 
                     promedio día/año a la fecha de corte (sumatoria de los saldos diarios del disponible ocurridos en el último 
                     año y se divide entre 365). Este valor se ubica en la última banda. */

                    pEntidad = new RiesgoLiquidez();
                    pEntidad = CalcularDisponibleRiesgoLiquidez(pFechaCorte, pNiif, vUsuario);
                    nLiquidez.saldo_actual = pEntidad.saldo_actual;
                    nLiquidez.vr_brecha1 = pEntidad.vr_brecha1;
                    nLiquidez.vr_brecha2 = pEntidad.vr_brecha2;
                    nLiquidez.vr_brecha3 = pEntidad.vr_brecha3;
                    nLiquidez.vr_brecha4 = pEntidad.vr_brecha4;
                    nLiquidez.vr_brecha5 = pEntidad.vr_brecha5;
                    nLiquidez.vr_brecha6 = pEntidad.vr_brecha6;
                    nLiquidez.vr_brecha7 = pEntidad.vr_brecha7;
                }
                if (nLiquidez.unidad_captura == 1 && nLiquidez.renglon == 3)
                {
                    /* FONDO DE LIQUIDEZ. Se madura el saldo a la fecha (cuenta 120300 + 112000)  más los intereses a recibir a 
                       partir de la fecha de corte. Debe madurarse el saldo a la fecha en la última banda de tiempo, si 
                       históricamente no se ha utilizado en el último año. */

                    pEntidad = new RiesgoLiquidez();
                    pEntidad.saldo_actual = CalcularSaldoContableRango("1203", "1120", 0, pFechaCorte, pNiif, vUsuario);
                    nLiquidez.saldo_actual = pEntidad.saldo_actual;
                    nLiquidez.vr_brecha7 = pEntidad.saldo_actual;
                }
                if (nLiquidez.unidad_captura == 1 && nLiquidez.renglon == 18)
                {
                    /* OTRAS INVERSIONES. Se madura el saldo a la fecha más los intereses a recibir a partir de la fecha de corte, 
                       en consideración a la clasificación */
                    nLiquidez.saldo_actual = CalcularSaldoContable("1226", pFechaCorte, pNiif, vUsuario);
                    nLiquidez.vr_brecha7 = nLiquidez.saldo_actual;
                }
                if (nLiquidez.unidad_captura == 1 && nLiquidez.renglon == 34)
                {   //VIVIENDA
                    pEntidad = new RiesgoLiquidez();
                    pEntidad = CalcularMaduracionCartera(pFechaCorte, 3, vUsuario);
                    nLiquidez.saldo_actual = pEntidad.saldo_actual;
                    nLiquidez.vr_brecha1 = pEntidad.vr_brecha1;
                    nLiquidez.vr_brecha2 = pEntidad.vr_brecha2;
                    nLiquidez.vr_brecha3 = pEntidad.vr_brecha3;
                    nLiquidez.vr_brecha4 = pEntidad.vr_brecha4;
                    nLiquidez.vr_brecha5 = pEntidad.vr_brecha5;
                    nLiquidez.vr_brecha6 = pEntidad.vr_brecha6;
                    nLiquidez.vr_brecha7 = nLiquidez.saldo_actual - nLiquidez.vr_brecha6 - nLiquidez.vr_brecha5 - nLiquidez.vr_brecha4 - nLiquidez.vr_brecha3 - nLiquidez.vr_brecha2 - nLiquidez.vr_brecha1;
                }
                if (nLiquidez.unidad_captura == 1 && nLiquidez.renglon == 40)
                {   //CONSUMO
                    pEntidad = new RiesgoLiquidez();
                    pEntidad = CalcularMaduracionCartera(pFechaCorte, 1, vUsuario);
                    nLiquidez.saldo_actual = pEntidad.saldo_actual;
                    nLiquidez.vr_brecha1 = pEntidad.vr_brecha1;
                    nLiquidez.vr_brecha2 = pEntidad.vr_brecha2;
                    nLiquidez.vr_brecha3 = pEntidad.vr_brecha3;
                    nLiquidez.vr_brecha4 = pEntidad.vr_brecha4;
                    nLiquidez.vr_brecha5 = pEntidad.vr_brecha5;
                    nLiquidez.vr_brecha6 = pEntidad.vr_brecha6;
                    nLiquidez.vr_brecha7 = nLiquidez.saldo_actual - nLiquidez.vr_brecha6 - nLiquidez.vr_brecha5 - nLiquidez.vr_brecha4 - nLiquidez.vr_brecha3 - nLiquidez.vr_brecha2 - nLiquidez.vr_brecha1;
                }
                if (nLiquidez.unidad_captura == 1 && nLiquidez.renglon == 46)
                {   //MICROCREDITO
                    pEntidad = new RiesgoLiquidez();
                    pEntidad = CalcularMaduracionCartera(pFechaCorte, 4, vUsuario);
                    nLiquidez.saldo_actual = pEntidad.saldo_actual;
                    nLiquidez.vr_brecha1 = pEntidad.vr_brecha1;
                    nLiquidez.vr_brecha2 = pEntidad.vr_brecha2;
                    nLiquidez.vr_brecha3 = pEntidad.vr_brecha3;
                    nLiquidez.vr_brecha4 = pEntidad.vr_brecha4;
                    nLiquidez.vr_brecha5 = pEntidad.vr_brecha5;
                    nLiquidez.vr_brecha6 = pEntidad.vr_brecha6;
                    nLiquidez.vr_brecha7 = nLiquidez.saldo_actual - nLiquidez.vr_brecha6 - nLiquidez.vr_brecha5 - nLiquidez.vr_brecha4 - nLiquidez.vr_brecha3 - nLiquidez.vr_brecha2 - nLiquidez.vr_brecha1;
                }
                if (nLiquidez.unidad_captura == 1 && nLiquidez.renglon == 58)
                {   //COMERCIAL
                    pEntidad = new RiesgoLiquidez();
                    pEntidad = CalcularMaduracionCartera(pFechaCorte, 2, vUsuario);
                    nLiquidez.saldo_actual = pEntidad.saldo_actual;
                    nLiquidez.vr_brecha1 = pEntidad.vr_brecha1;
                    nLiquidez.vr_brecha2 = pEntidad.vr_brecha2;
                    nLiquidez.vr_brecha3 = pEntidad.vr_brecha3;
                    nLiquidez.vr_brecha4 = pEntidad.vr_brecha4;
                    nLiquidez.vr_brecha5 = pEntidad.vr_brecha5;
                    nLiquidez.vr_brecha6 = pEntidad.vr_brecha6;
                    nLiquidez.vr_brecha7 = nLiquidez.saldo_actual - nLiquidez.vr_brecha6 - nLiquidez.vr_brecha5 - nLiquidez.vr_brecha4 - nLiquidez.vr_brecha3 - nLiquidez.vr_brecha2 - nLiquidez.vr_brecha1;
                }
                if (nLiquidez.unidad_captura == 1 && nLiquidez.renglon == 64)
                {   //EMPLEADOS
                    pEntidad = new RiesgoLiquidez();
                    pEntidad = CalcularMaduracionCartera(pFechaCorte, 0, vUsuario);
                    nLiquidez.saldo_actual = pEntidad.saldo_actual;
                    nLiquidez.vr_brecha1 = pEntidad.vr_brecha1;
                    nLiquidez.vr_brecha2 = pEntidad.vr_brecha2;
                    nLiquidez.vr_brecha3 = pEntidad.vr_brecha3;
                    nLiquidez.vr_brecha4 = pEntidad.vr_brecha4;
                    nLiquidez.vr_brecha5 = pEntidad.vr_brecha5;
                    nLiquidez.vr_brecha6 = pEntidad.vr_brecha6;
                    nLiquidez.vr_brecha7 = nLiquidez.saldo_actual - nLiquidez.vr_brecha6 - nLiquidez.vr_brecha5 - nLiquidez.vr_brecha4 - nLiquidez.vr_brecha3 - nLiquidez.vr_brecha2 - nLiquidez.vr_brecha1;
                }
                if (nLiquidez.unidad_captura == 1 && nLiquidez.renglon == 68)
                {   //CONVENIOS POR COBRAR
                    nLiquidez.saldo_actual = CalcularSaldoContable("1473", pFechaCorte, pNiif, vUsuario);
                    nLiquidez.vr_brecha7 = nLiquidez.saldo_actual;
                }
                if (nLiquidez.unidad_captura == 1 && nLiquidez.renglon == 74)
                {   //CUENTAS POR COBRAR OTROS
                    nLiquidez.saldo_actual = CalcularSaldoContable("16", pFechaCorte, pNiif, vUsuario);
                    nLiquidez.vr_brecha7 = nLiquidez.saldo_actual;
                }
                if (nLiquidez.unidad_captura == 1 && nLiquidez.renglon == 76)
                {   //PROPIEDAD, PLANTA Y EQUIPO
                    nLiquidez.saldo_actual = CalcularSaldoContable("1705", pFechaCorte, pNiif, vUsuario);
                    nLiquidez.vr_brecha7 = nLiquidez.saldo_actual;
                }
                if (nLiquidez.unidad_captura == 1 && nLiquidez.renglon == 36)
                {   //INTERESES CREDITOS DE VIVIENDA
                    pEntidad = new RiesgoLiquidez();
                    pEntidad = CalcularMaduracionCausacion(pFechaCorte, 3, vUsuario);
                    nLiquidez.saldo_actual = pEntidad.saldo_actual;
                    nLiquidez.vr_brecha1 = pEntidad.vr_brecha1;
                    nLiquidez.vr_brecha2 = pEntidad.vr_brecha2;
                    nLiquidez.vr_brecha3 = pEntidad.vr_brecha3;
                    nLiquidez.vr_brecha4 = pEntidad.vr_brecha4;
                    nLiquidez.vr_brecha5 = pEntidad.vr_brecha5;
                    nLiquidez.vr_brecha6 = pEntidad.vr_brecha6;
                }
                if (nLiquidez.unidad_captura == 1 && nLiquidez.renglon == 42)
                {   //INTERESES CREDITOS DE CONSUMO
                    pEntidad = new RiesgoLiquidez();
                    pEntidad = CalcularMaduracionCausacion(pFechaCorte, 1, vUsuario);
                    nLiquidez.saldo_actual = pEntidad.saldo_actual;
                    nLiquidez.vr_brecha1 = pEntidad.vr_brecha1;
                    nLiquidez.vr_brecha2 = pEntidad.vr_brecha2;
                    nLiquidez.vr_brecha3 = pEntidad.vr_brecha3;
                    nLiquidez.vr_brecha4 = pEntidad.vr_brecha4;
                    nLiquidez.vr_brecha5 = pEntidad.vr_brecha5;
                    nLiquidez.vr_brecha6 = pEntidad.vr_brecha6;
                }
                if (nLiquidez.unidad_captura == 1 && nLiquidez.renglon == 48)
                {   //INTERESES MICROCREDITO INMOBILIARIO
                    pEntidad = new RiesgoLiquidez();
                    pEntidad = CalcularMaduracionCausacion(pFechaCorte, 4, vUsuario);
                    nLiquidez.saldo_actual = pEntidad.saldo_actual;
                    nLiquidez.vr_brecha1 = pEntidad.vr_brecha1;
                    nLiquidez.vr_brecha2 = pEntidad.vr_brecha2;
                    nLiquidez.vr_brecha3 = pEntidad.vr_brecha3;
                    nLiquidez.vr_brecha4 = pEntidad.vr_brecha4;
                    nLiquidez.vr_brecha5 = pEntidad.vr_brecha5;
                    nLiquidez.vr_brecha6 = pEntidad.vr_brecha6;
                }
                if (nLiquidez.unidad_captura == 1 && nLiquidez.renglon == 60)
                {   //INTERESES CREDITOS COMERCIALES
                    pEntidad = new RiesgoLiquidez();
                    pEntidad = CalcularMaduracionCausacion(pFechaCorte, 2, vUsuario);
                    nLiquidez.saldo_actual = pEntidad.saldo_actual;
                    nLiquidez.vr_brecha1 = pEntidad.vr_brecha1;
                    nLiquidez.vr_brecha2 = pEntidad.vr_brecha2;
                    nLiquidez.vr_brecha3 = pEntidad.vr_brecha3;
                    nLiquidez.vr_brecha4 = pEntidad.vr_brecha4;
                    nLiquidez.vr_brecha5 = pEntidad.vr_brecha5;
                    nLiquidez.vr_brecha6 = pEntidad.vr_brecha6;
                }
                if (nLiquidez.unidad_captura == 1 && nLiquidez.renglon == 66)
                {   //INTERESES CREDITOS EMPLEADOS
                    pEntidad = new RiesgoLiquidez();
                    pEntidad = CalcularMaduracionCausacion(pFechaCorte, 0, vUsuario);
                    nLiquidez.saldo_actual = pEntidad.saldo_actual;
                    nLiquidez.vr_brecha1 = pEntidad.vr_brecha1;
                    nLiquidez.vr_brecha2 = pEntidad.vr_brecha2;
                    nLiquidez.vr_brecha3 = pEntidad.vr_brecha3;
                    nLiquidez.vr_brecha4 = pEntidad.vr_brecha4;
                    nLiquidez.vr_brecha5 = pEntidad.vr_brecha5;
                    nLiquidez.vr_brecha6 = pEntidad.vr_brecha6;
                }
                if (nLiquidez.unidad_captura == 1 && nLiquidez.renglon == 84)
                {
                    // OTROS ACTIVOS
                    nLiquidez.saldo_actual = CalcularSaldoContable("19", pFechaCorte, pNiif, vUsuario);
                    nLiquidez.vr_brecha7 = nLiquidez.saldo_actual;
                }
                // ======================================================================================================
                if (nLiquidez.unidad_captura == 2 && nLiquidez.renglon == 1)
                {
                    //DEPOSITOS DE AHORRO
                    pEntidad = new RiesgoLiquidez();
                    pEntidad = CalcularAhorrosRiesgoLiquidez(pFechaCorte, pNiif, vUsuario);
                    nLiquidez.saldo_actual = Math.Round(Convert.ToDecimal(pEntidad.saldo_actual));
                    nLiquidez.vr_brecha1 = pEntidad.vr_brecha1;
                    nLiquidez.vr_brecha2 = pEntidad.vr_brecha2;
                    nLiquidez.vr_brecha3 = pEntidad.vr_brecha3;
                    nLiquidez.vr_brecha4 = pEntidad.vr_brecha4;
                    nLiquidez.vr_brecha5 = pEntidad.vr_brecha5;
                    nLiquidez.vr_brecha6 = pEntidad.vr_brecha6;
                    nLiquidez.vr_brecha7 = pEntidad.vr_brecha7;
                }
                if (nLiquidez.unidad_captura == 2 && nLiquidez.renglon == 3)
                {
                    // CERTIFICADOS DE DEPOSITOS DE AHORRO A TERMINO
                    pEntidad = new RiesgoLiquidez();
                    pEntidad = CalcularCDATSRiesgoLiquidez(pFechaCorte, pNiif, vUsuario);
                    nLiquidez.saldo_actual = CalcularSaldoContable("2110", pFechaCorte, pNiif, vUsuario);
                    nLiquidez.vr_brecha7 = nLiquidez.saldo_actual;
                }
                if (nLiquidez.unidad_captura == 2 && nLiquidez.renglon == 5)
                {
                    // DEPOSITOS DE AHORRO CONTRACTUAL
                    nLiquidez.saldo_actual = CalcularSaldoContable("2125", pFechaCorte, pNiif, vUsuario);
                    nLiquidez.vr_brecha7 = nLiquidez.saldo_actual;
                }
                if (nLiquidez.unidad_captura == 2 && nLiquidez.renglon == 7)
                {
                    // DEPOSITOS DE AHORRO PERMANENTE
                    nLiquidez.saldo_actual = CalcularSaldoContable("2130", pFechaCorte, pNiif, vUsuario);
                    nLiquidez.vr_brecha7 = nLiquidez.saldo_actual;
                }
                if (nLiquidez.unidad_captura == 2 && nLiquidez.renglon == 10)
                {
                    // OBLIGACIONES FINANCIERAS
                    nLiquidez.saldo_actual = CalcularSaldoContable("23", pFechaCorte, pNiif, vUsuario);
                    nLiquidez.vr_brecha7 = nLiquidez.saldo_actual;
                }
                if (nLiquidez.unidad_captura == 2 && nLiquidez.renglon == 13)
                {
                    // CUENTAS POR PAGAR Y OTRAS
                    nLiquidez.saldo_actual = CalcularSaldoContable("24", pFechaCorte, pNiif, vUsuario);
                    nLiquidez.vr_brecha7 = nLiquidez.saldo_actual;
                }
                if (nLiquidez.unidad_captura == 2 && nLiquidez.renglon == 17)
                {
                    // FONDOS SOCIALES Y MUTUALES
                    nLiquidez.saldo_actual = CalcularSaldoContable("26", pFechaCorte, pNiif, vUsuario);
                    nLiquidez.vr_brecha7 = nLiquidez.saldo_actual;
                }
                if (nLiquidez.unidad_captura == 2 && nLiquidez.renglon == 19)
                {
                    // OTROS PASIVOS
                    nLiquidez.saldo_actual = CalcularSaldoContable("27", pFechaCorte, pNiif, vUsuario);
                    nLiquidez.vr_brecha7 = nLiquidez.saldo_actual;
                }
                // ======================================================================================================
                if (nLiquidez.unidad_captura == 3 && nLiquidez.renglon == 1)
                {
                    // APORTES SOCIALES TEMPORALMENTE RESTRINGIDOS
                    nLiquidez.saldo_actual = CalcularSaldoContable("3105", pFechaCorte, pNiif, vUsuario);
                    nLiquidez.vr_brecha7 = nLiquidez.saldo_actual;
                }
                if (nLiquidez.unidad_captura == 3 && nLiquidez.renglon == 3)
                {
                    // APORTES SOCIALES MINIMOS NO REDUCIBLES
                    nLiquidez.saldo_actual = CalcularSaldoContable("3110", pFechaCorte, pNiif, vUsuario);
                    nLiquidez.vr_brecha7 = nLiquidez.saldo_actual;
                }
                if (nLiquidez.unidad_captura == 3 && nLiquidez.renglon == 5)
                {
                    // RESERVAS
                    nLiquidez.saldo_actual = CalcularSaldoContable("32", pFechaCorte, pNiif, vUsuario);
                    nLiquidez.vr_brecha7 = nLiquidez.saldo_actual;
                }
                if (nLiquidez.unidad_captura == 3 && nLiquidez.renglon == 7)
                {
                    // FONDOS DE DESTINACION ESPECIFICA
                    nLiquidez.saldo_actual = CalcularSaldoContable("33", pFechaCorte, pNiif, vUsuario);
                    nLiquidez.vr_brecha7 = nLiquidez.saldo_actual;
                }
                if (nLiquidez.unidad_captura == 3 && nLiquidez.renglon == 9)
                {
                    // SUPERAVIT
                    nLiquidez.saldo_actual = CalcularSaldoContable("34", pFechaCorte, pNiif, vUsuario);
                    nLiquidez.vr_brecha7 = nLiquidez.saldo_actual;
                }
                if (nLiquidez.unidad_captura == 3 && nLiquidez.renglon == 11)
                {
                    // EXCEDENTES Y/O PERDIDAS DEL EJERCICIO
                    nLiquidez.saldo_actual = CalcularSaldoContable("35", pFechaCorte, pNiif, vUsuario);
                    nLiquidez.vr_brecha7 = nLiquidez.saldo_actual;
                }
                if (nLiquidez.unidad_captura == 3 && nLiquidez.renglon == 13)
                {
                    // RESULTADOS ACUMULADOS POR ADOPCIÓN POR
                    nLiquidez.saldo_actual = CalcularSaldoContable("36", pFechaCorte, pNiif, vUsuario);
                    nLiquidez.vr_brecha7 = nLiquidez.saldo_actual;
                }
            }
            // Calcular Totales
            Totalizar(ref lstDatos);
            // Calcular Brecha de Liquidez
            return lstDatos;
        }


        public void Totalizar(ref List<RiesgoLiquidez> lstDatos)
        {
            //TOTAL POSICIONES ACTIVAS
            TotalUnidad(1, ref lstDatos);
            //TOTAL POSICIONES PASIVAS
            TotalUnidad(2, ref lstDatos);
            //TOTAL POSICIONES DEL PATRIMONIO
            TotalUnidad(3, ref lstDatos);
            //BRECHA DE LIQUIDEZ
            RiesgoLiquidez pActivos = new RiesgoLiquidez();
            pActivos = BuscarUnidadRenglon(1, 999, lstDatos);
            RiesgoLiquidez pPasivos = new RiesgoLiquidez();
            pPasivos = BuscarUnidadRenglon(2, 999, lstDatos);
            if (pActivos != null && pPasivos != null)
            {
                foreach (RiesgoLiquidez nLiquidez in lstDatos)
                {
                    if (nLiquidez.unidad_captura == 4 && nLiquidez.renglon == 1)
                    {
                        nLiquidez.saldo_actual = 0;
                        nLiquidez.vr_brecha1 = (pActivos.vr_brecha1 == null ? 0 : pActivos.vr_brecha1) - (pPasivos.vr_brecha1 == null ? 0 : pPasivos.vr_brecha1);
                        nLiquidez.vr_brecha2 = (pActivos.vr_brecha2 == null ? 0 : pActivos.vr_brecha2) - (pPasivos.vr_brecha2 == null ? 0 : pPasivos.vr_brecha2);
                        nLiquidez.vr_brecha3 = (pActivos.vr_brecha3 == null ? 0 : pActivos.vr_brecha3) - (pPasivos.vr_brecha3 == null ? 0 : pPasivos.vr_brecha3);
                        nLiquidez.vr_brecha4 = (pActivos.vr_brecha4 == null ? 0 : pActivos.vr_brecha4) - (pPasivos.vr_brecha4 == null ? 0 : pPasivos.vr_brecha4);
                        nLiquidez.vr_brecha5 = (pActivos.vr_brecha5 == null ? 0 : pActivos.vr_brecha5) - (pPasivos.vr_brecha5 == null ? 0 : pPasivos.vr_brecha5);
                        nLiquidez.vr_brecha6 = (pActivos.vr_brecha6 == null ? 0 : pActivos.vr_brecha6) - (pPasivos.vr_brecha6 == null ? 0 : pPasivos.vr_brecha6);
                        nLiquidez.vr_brecha7 = (pActivos.vr_brecha7 == null ? 0 : pActivos.vr_brecha7) - (pPasivos.vr_brecha7 == null ? 0 : pPasivos.vr_brecha7);
                    }
                }
            }
            //BRECHA ACUMULADA DE LIQUIDEZ
            RiesgoLiquidez pBrecha = new RiesgoLiquidez();
            pBrecha = BuscarUnidadRenglon(4, 1, lstDatos);
            if (pBrecha != null)
            {
                decimal? saldoACU = 0, saldoACUL = 0;
                foreach (RiesgoLiquidez nLiquidez in lstDatos)
                {
                    if (nLiquidez.unidad_captura == 4 && nLiquidez.renglon == 3)
                    {
                        nLiquidez.saldo_actual = 0;
                        saldoACU += (pBrecha.vr_brecha1 == null ? 0 : pBrecha.vr_brecha1);
                        nLiquidez.vr_brecha1 = saldoACU;
                        saldoACU += (pBrecha.vr_brecha2 == null ? 0 : pBrecha.vr_brecha2);
                        nLiquidez.vr_brecha2 = saldoACU;
                        saldoACU += (pBrecha.vr_brecha3 == null ? 0 : pBrecha.vr_brecha3);
                        nLiquidez.vr_brecha3 = saldoACU;
                        saldoACU += (pBrecha.vr_brecha4 == null ? 0 : pBrecha.vr_brecha4);
                        nLiquidez.vr_brecha4 = saldoACU;
                        saldoACU += (pBrecha.vr_brecha5 == null ? 0 : pBrecha.vr_brecha5);
                        nLiquidez.vr_brecha5 = saldoACU;
                        saldoACU += (pBrecha.vr_brecha6 == null ? 0 : pBrecha.vr_brecha6);
                        nLiquidez.vr_brecha6 = saldoACU;
                        saldoACU += (pBrecha.vr_brecha7 == null ? 0 : pBrecha.vr_brecha7);
                        nLiquidez.vr_brecha7 = saldoACU;
                    }
                    if (nLiquidez.unidad_captura == 4 && nLiquidez.renglon == 9)
                    {
                        nLiquidez.saldo_actual = 0;
                        saldoACUL += (pBrecha.vr_brecha1 == null ? 0 : pBrecha.vr_brecha1);
                        nLiquidez.vr_brecha1 = saldoACUL;
                        saldoACUL += (pBrecha.vr_brecha2 == null ? 0 : pBrecha.vr_brecha2);
                        nLiquidez.vr_brecha2 = saldoACUL;
                        saldoACUL += (pBrecha.vr_brecha3 == null ? 0 : pBrecha.vr_brecha3);
                        nLiquidez.vr_brecha3 = saldoACUL;
                    }

                    if (nLiquidez.unidad_captura == 4 && nLiquidez.renglon == 11)
                    {
                        nLiquidez.saldo_actual = 0;
                        saldoACUL += (pBrecha.vr_brecha1 == null ? 0 : pBrecha.vr_brecha1);
                        saldoACUL += (pBrecha.vr_brecha2 == null ? 0 : pBrecha.vr_brecha2);
                        nLiquidez.vr_brecha2 = saldoACUL;
                        saldoACUL += (pBrecha.vr_brecha3 == null ? 0 : pBrecha.vr_brecha3);
                        nLiquidez.vr_brecha3 = saldoACUL;
                    }
                }
            }
            //TOTAL ACTIVOS LIQUIDOS NETOS
            TotalUnidad(5, ref lstDatos);
        }

        public void TotalUnidad(int pUnidad, ref List<RiesgoLiquidez> lstDatos)
        {
            // Calcular Totales
            decimal? saldo_fec = 0, sal = 0, val1 = 0, val2 = 0, val3 = 0, val4 = 0, val5 = 0, val6 = 0, val7 = 0;
            foreach (RiesgoLiquidez nLiquidez in lstDatos)
            {
                if (nLiquidez.unidad_captura == pUnidad)
                {
                    saldo_fec += (nLiquidez.saldo_actual == null ? 0 : nLiquidez.saldo_actual);
                    sal = sal + (nLiquidez.saldo_actual == null ? 0 : nLiquidez.saldo_actual);
                    val1 = val1 + (nLiquidez.vr_brecha1 == null ? 0 : nLiquidez.vr_brecha1);
                    val2 = val2 + (nLiquidez.vr_brecha2 == null ? 0 : nLiquidez.vr_brecha2);
                    val3 = val3 + (nLiquidez.vr_brecha3 == null ? 0 : nLiquidez.vr_brecha3);
                    val4 = val4 + (nLiquidez.vr_brecha4 == null ? 0 : nLiquidez.vr_brecha4);
                    val5 = val5 + (nLiquidez.vr_brecha5 == null ? 0 : nLiquidez.vr_brecha5);
                    val6 = val6 + (nLiquidez.vr_brecha6 == null ? 0 : nLiquidez.vr_brecha6);
                    val7 = val7 + (nLiquidez.vr_brecha7 == null ? 0 : nLiquidez.vr_brecha7);
                }
            }
            foreach (RiesgoLiquidez nLiquidez in lstDatos)
            {
                if (nLiquidez.unidad_captura == pUnidad && nLiquidez.renglon == 999)
                {
                    nLiquidez.saldo_actual = saldo_fec;
                    nLiquidez.vr_brecha1 = val1;
                    nLiquidez.vr_brecha2 = val2;
                    nLiquidez.vr_brecha3 = val3;
                    nLiquidez.vr_brecha4 = val4;
                    nLiquidez.vr_brecha5 = val5;
                    nLiquidez.vr_brecha6 = val6;
                    nLiquidez.vr_brecha7 = val7;
                }
            }
        }

        public RiesgoLiquidez BuscarUnidadRenglon(int pUnidad, int pRenglon, List<RiesgoLiquidez> lstDatos)
        {
            // Calcular Totales
            foreach (RiesgoLiquidez nLiquidez in lstDatos)
            {
                if (nLiquidez.unidad_captura == pUnidad && nLiquidez.renglon == pRenglon)
                {
                    return nLiquidez;
                }
            }
            return null;
        }

        #endregion


        protected List<RiesgoLiquidez> ListarRiesgoLiquidezDatos()
        {
            List<RiesgoLiquidez> lista = new List<RiesgoLiquidez>()
            {
                new RiesgoLiquidez(){ unidad_captura = 1, renglon = 1, descripcion = "EFECTIVO Y EQUIVALENTE AL EFECTIVO (sin fondo de liquidez)"},
                new RiesgoLiquidez(){ unidad_captura = 1, renglon = 3, descripcion = "FONDO DE LIQUIDEZ"},
                new RiesgoLiquidez(){ unidad_captura = 1, renglon = 5, descripcion = "INVERSIONES NEGOCIABLES"},
                new RiesgoLiquidez(){ unidad_captura = 1, renglon = 7, descripcion = "INVERSIONES PARA MANTENER HASTA EL VENCIMIENTO"},
                new RiesgoLiquidez(){ unidad_captura = 1, renglon = 9, descripcion = "INVERSIONES DISPONIBLES PARA LA VENTA"},
                new RiesgoLiquidez(){ unidad_captura = 1, renglon = 11, descripcion = "INVERSIONES EN ENTIDADES SUBSIDIARIAS"},
                new RiesgoLiquidez(){ unidad_captura = 1, renglon = 13, descripcion = "INVERSIONES EN ENTIDADES ASOCIADAS"},
                new RiesgoLiquidez(){ unidad_captura = 1, renglon = 15, descripcion = "INVERSIONES EN OPERACIONES CONJUNTAS"},
                new RiesgoLiquidez(){ unidad_captura = 1, renglon = 16, descripcion = "INVERSIONES CONTABILIZADAS A COSTO AMORTIZADO"},
                new RiesgoLiquidez(){ unidad_captura = 1, renglon = 17, descripcion = "INVERSIONES EN NEGOCIOS CONJUNTOS"},
                new RiesgoLiquidez(){ unidad_captura = 1, renglon = 18, descripcion = "OTRAS INVERSIONES EN INSTRUMENTOS DE PATRIMONIO"},
                new RiesgoLiquidez(){ unidad_captura = 1, renglon = 20, descripcion = "INVERSIONES CONTABILIZADAS A VALOR RAZONABLE CON CAMBIOS EN EL RESULTADO"},
                new RiesgoLiquidez(){ unidad_captura = 1, renglon = 22, descripcion = "INVERSIONES CONTABILIZADAS A VALOR RAZONABLE CON CAMBIOS EN EL ORI"},
                new RiesgoLiquidez(){ unidad_captura = 1, renglon = 24, descripcion = "INVERSIONES A VALOR DE MERCADO CON CAMBIOS EN EL PATRIMONIO"},
                new RiesgoLiquidez(){ unidad_captura = 1, renglon = 26, descripcion = "INSTRUMENTOS DERIVADOS CON FINES DE ESPECULACIÓN MEDIDOS A VALOR RAZONABLE"},
                new RiesgoLiquidez(){ unidad_captura = 1, renglon = 28, descripcion = "INSTRUMENTOS DERIVADOS CON FINES DE COBERTURA DE VALOR DE MERCADO (VALOR RAZONABLE) CON CAMBIOS EN EL ORI"},
                new RiesgoLiquidez(){ unidad_captura = 1, renglon = 30, descripcion = "INSTRUMENTOS DERIVADOS CON FINES DE COBERTURA DE FLUJOS DE EFECTIVO MEDIDOS A COSTO AMORTIZADO"},
                new RiesgoLiquidez(){ unidad_captura = 1, renglon = 32, descripcion = "INVENTARIOS"},
                new RiesgoLiquidez(){ unidad_captura = 1, renglon = 34, descripcion = "CRÉDITOS DE VIVIENDA"},
                new RiesgoLiquidez(){ unidad_captura = 1, renglon = 36, descripcion = "INTERESES CREDITOS DE VIVIENDA"},
                new RiesgoLiquidez(){ unidad_captura = 1, renglon = 38, descripcion = "PAGOS POR CUENTA DE ASOCIADOS - CRÉDITOS VIVIENDA"},
                new RiesgoLiquidez(){ unidad_captura = 1, renglon = 40, descripcion = "CRÉDITOS DE CONSUMO"},
                new RiesgoLiquidez(){ unidad_captura = 1, renglon = 42, descripcion = "INTERESES CREDITOS DE CONSUMO"},
                new RiesgoLiquidez(){ unidad_captura = 1, renglon = 44, descripcion = "PAGOS POR CUENTA DE ASOCIADOS - CRÉDITOS CONSUMO"},
                new RiesgoLiquidez(){ unidad_captura = 1, renglon = 46, descripcion = "MICROCREDITO INMOBILIARIO"},
                new RiesgoLiquidez(){ unidad_captura = 1, renglon = 48, descripcion = "INTERESES MICROCREDITO INMOBILIARIO"},
                new RiesgoLiquidez(){ unidad_captura = 1, renglon = 50, descripcion = "PAGOS POR CUENTA DE ASOCIADOS - MICROCREDITO INMOBILIARIO"},
                new RiesgoLiquidez(){ unidad_captura = 1, renglon = 52, descripcion = "MICROCREDITO EMPRESARIAL"},
                new RiesgoLiquidez(){ unidad_captura = 1, renglon = 54, descripcion = "INTERESES MICROCREDITO EMPRESARIAL"},
                new RiesgoLiquidez(){ unidad_captura = 1, renglon = 56, descripcion = "PAGOS POR CUENTA DE ASOCIADOS - MICROCREDITO EMPRESARIAL"},
                new RiesgoLiquidez(){ unidad_captura = 1, renglon = 58, descripcion = "CRÉDITOS COMERCIALES"},
                new RiesgoLiquidez(){ unidad_captura = 1, renglon = 60, descripcion = "INTERESES CREDITOS COMERCIALES"},
                new RiesgoLiquidez(){ unidad_captura = 1, renglon = 62, descripcion = "PAGOS POR CUENTA DE ASOCIADOS - COMERCIALES"},
                new RiesgoLiquidez(){ unidad_captura = 1, renglon = 64, descripcion = "CRÉDITOS A EMPLEADOS"},
                new RiesgoLiquidez(){ unidad_captura = 1, renglon = 66, descripcion = "INTERESES CRÉDITOS A EMPLEADOS"},
                new RiesgoLiquidez(){ unidad_captura = 1, renglon = 68, descripcion = "CONVENIOS POR COBRAR"},
                new RiesgoLiquidez(){ unidad_captura = 1, renglon = 70, descripcion = "PAGOS POR CUENTA DE CREDITOS A EMPLEADOS"},
                new RiesgoLiquidez(){ unidad_captura = 1, renglon = 72, descripcion = "ACTIVOS BIOLÓGICOS"},
                new RiesgoLiquidez(){ unidad_captura = 1, renglon = 74, descripcion = "CUENTAS POR COBRAR Y OTRAS"},
                new RiesgoLiquidez(){ unidad_captura = 1, renglon = 76, descripcion = "PROPIEDAD, PLANTA Y EQUIPO"},
                new RiesgoLiquidez(){ unidad_captura = 1, renglon = 78, descripcion = "PROPIEDADES DE INVERSIÓN MEDIDAS AL COSTO"},
                new RiesgoLiquidez(){ unidad_captura = 1, renglon = 80, descripcion = "PROPIEDADES DE INVERSIÓN MEDIDAS A VALOR RAZONABLE"},
                new RiesgoLiquidez(){ unidad_captura = 1, renglon = 82, descripcion = "ACTIVOS CORRIENTES MANTENIDOS PARA LA VENTA"},
                new RiesgoLiquidez(){ unidad_captura = 1, renglon = 84, descripcion = "OTROS ACTIVOS"},
                new RiesgoLiquidez(){ unidad_captura = 1, renglon = 86, descripcion = "DEUDORAS CONTINGENTES"},
                new RiesgoLiquidez(){ unidad_captura = 1, renglon = 999, descripcion = "TOTAL POSICIONES ACTIVAS"},

                new RiesgoLiquidez(){ unidad_captura = 2, renglon = 1, descripcion = "DEPOSITOS DE AHORRO"},
                new RiesgoLiquidez(){ unidad_captura = 2, renglon = 3, descripcion = "CERTIFICADOS DEPOSITOS DE AHORRO A TÉRMINO"},
                new RiesgoLiquidez(){ unidad_captura = 2, renglon = 5, descripcion = "DEPOSITOS DE AHORRO CONTRACTUAL"},
                new RiesgoLiquidez(){ unidad_captura = 2, renglon = 7, descripcion = "DEPOSITOS DE AHORRO PERMANENTE"},
                new RiesgoLiquidez(){ unidad_captura = 2, renglon = 9, descripcion = "TITULOS DE INVERSION EN CIRCULACIÓN"},
                new RiesgoLiquidez(){ unidad_captura = 2, renglon = 11, descripcion = "CRÉDITOS DE BANCOS Y OTRAS OBLIGACIONES FINANCIERAS"},
                new RiesgoLiquidez(){ unidad_captura = 2, renglon = 12, descripcion = "IMPUESTO DIFERIDO PASIVO"},
                new RiesgoLiquidez(){ unidad_captura = 2, renglon = 13, descripcion = "CUENTAS POR PAGAR Y OTRAS"},
                new RiesgoLiquidez(){ unidad_captura = 2, renglon = 15, descripcion = "IMPUESTOS DIFERIOS"},
                new RiesgoLiquidez(){ unidad_captura = 2, renglon = 17, descripcion = "FONDOS SOCIALES Y MUTUALES"},
                new RiesgoLiquidez(){ unidad_captura = 2, renglon = 19, descripcion = "OTROS PASIVOS"},
                new RiesgoLiquidez(){ unidad_captura = 2, renglon = 21, descripcion = "PROVISIONES"},
                new RiesgoLiquidez(){ unidad_captura = 2, renglon = 23, descripcion = "PASIVOS INCLUIDOS EN GRUPOS DE ACTIVOS PARA SU DISPOSICIÓN CLASIFICADOS COMO MANTENIDOS PARA LA VENTA"},
                new RiesgoLiquidez(){ unidad_captura = 2, renglon = 25, descripcion = "ACREEDORAS CONTINGENTES"},
                new RiesgoLiquidez(){ unidad_captura = 2, renglon = 999, descripcion = "TOTAL POSICIONES PASIVAS"},

                new RiesgoLiquidez(){ unidad_captura = 3, renglon = 1, descripcion = "APORTES SOCIALES TEMPORALMENTE RESTRINGIDOS"},
                new RiesgoLiquidez(){ unidad_captura = 3, renglon = 3, descripcion = "APORTES SOCIALES MINIMOS NO REDUCIBLES"},
                new RiesgoLiquidez(){ unidad_captura = 3, renglon = 4, descripcion = "FONDO SOCIAL MUTUAL"},
                new RiesgoLiquidez(){ unidad_captura = 3, renglon = 5, descripcion = "RESERVAS"},
                new RiesgoLiquidez(){ unidad_captura = 3, renglon = 7, descripcion = "FONDOS DE DESTINACIÓN ESPECÍFICA"},
                new RiesgoLiquidez(){ unidad_captura = 3, renglon = 9, descripcion = "SUPERÁVIT"},
                new RiesgoLiquidez(){ unidad_captura = 3, renglon = 11, descripcion = "EXCEDENTES Y/O PÉRDIDAS DEL EJERCICIO"},
                new RiesgoLiquidez(){ unidad_captura = 3, renglon = 13, descripcion = "RESULTADOS ACUMULADOS POR ADOPCIÓN POR PRIMERA VEZ"},
                new RiesgoLiquidez(){ unidad_captura = 3, renglon = 14, descripcion = "OTRO RESULTADO INTEGRAL"},
                new RiesgoLiquidez(){ unidad_captura = 3, renglon = 15, descripcion = "EXCEDENTES O PÉRDIDAS NO REALIZADAS (ORI)"},
                new RiesgoLiquidez(){ unidad_captura = 3, renglon = 19, descripcion = "RESULTADOS DE EJERCICIOS ANTERIORES"},
                new RiesgoLiquidez(){ unidad_captura = 3, renglon = 999, descripcion = "TOTAL POSICIONES DEL PATRIMONIO"},

                new RiesgoLiquidez(){ unidad_captura = 4, renglon = 1, descripcion = "BRECHA DE LIQUIDEZ"},
                new RiesgoLiquidez(){ unidad_captura = 4, renglon = 3, descripcion = "BRECHA ACUMULADA DE LIQUIDEZ"},
                new RiesgoLiquidez(){ unidad_captura = 4, renglon = 5, descripcion = "VALOR EN RIESGO DE LIQUIDEZ"},
                new RiesgoLiquidez(){ unidad_captura = 4, renglon = 7, descripcion = "ACTIVOS LIQUIDOS NETOS"},
                new RiesgoLiquidez(){ unidad_captura = 4, renglon = 9, descripcion = "RESULTADO DE LA EVALUACIÓN PERIODO ACTUAL"},
                new RiesgoLiquidez(){ unidad_captura = 4, renglon = 11, descripcion = "RESULTADO DE LA EVALUACIÓN PERIODO ANTERIOR"},
                new RiesgoLiquidez(){ unidad_captura = 4, renglon = 13, descripcion = "EXPOSICIÓN SIGNIFICATIVA AL RIESGO DE LIQUIDEZ"},

                new RiesgoLiquidez(){ unidad_captura = 5, renglon = 1, descripcion = "EFECTIVO Y EQUIVALENTE AL EFECTIVO (Excepto efectivo restringido)"},
                new RiesgoLiquidez(){ unidad_captura = 5, renglon = 3, descripcion = "FONDO DE LIQUIDEZ"},
                new RiesgoLiquidez(){ unidad_captura = 5, renglon = 5, descripcion = "INVERSIONES NEGOCIABLES"},
                new RiesgoLiquidez(){ unidad_captura = 5, renglon = 999, descripcion = "TOTAL ACTIVOS LIQUIDOS NETOS"}
            };

            return lista;
        }


        public RiesgoLiquidez CalcularAhorrosRiesgoLiquidez(DateTime pFecha, bool pNiif, Usuario vUsuario)
        {
            try
            {
                List<RiesgoLiquidez> lstFecFinMes = new List<RiesgoLiquidez>();
                decimal pSaldoTotal = 0, pSaldo_actual = 0, pSaldoPromedio = 0, pSaldoDia = 0, pSaldoCopia = 0;

                //DateTime pFecha_Ini = new DateTime((pFecha.AddYears(-1)).Year, pFecha.Month, 1);
                DateTime pFecha_Ini = pFecha.AddDays(1).AddYears(-1);

                //Calculando el saldo a la fecha de corte
                pSaldo_actual = DARiesgo.CalculoSaldoContable(pFecha, "2105", pNiif, vUsuario);
                pSaldoDia = DARiesgo.CalculoSaldoContable(pFecha_Ini, "2105", pNiif, vUsuario);
                pSaldoCopia = pSaldoDia;

                //Recorriendo los 365 dias
                decimal totCambio = 0;
                decimal variacion = 0;
                int contador = 0;
                while (pFecha_Ini <= pFecha)
                {
                    //Ubicando la fecha del ultimo mes
                    DateTime pFechaUltMes = DateTime.ParseExact("01/" + pFecha_Ini.AddMonths(1).ToString("MM/yyyy"), "dd/MM/yyyy", null);
                    pFechaUltMes = pFechaUltMes.AddDays(-1);

                    if (!DARiesgo.SaldoDiario(pFecha_Ini, "2105", ref pSaldoDia, pNiif, vUsuario))
                    {
                        pSaldoDia += DARiesgo.CalculoMovimientoContable(pFecha_Ini, "2105%", pNiif, vUsuario);
                    }
                    pSaldoTotal += pSaldoDia;

                    //Si es el último día del mes entonces guardar el saldo para tener saldo mensual
                    if (pFecha_Ini == pFechaUltMes)
                    {
                        RiesgoLiquidez pCierre = new RiesgoLiquidez();
                        pCierre.fecha_corte = pFecha_Ini;
                        pCierre.saldo_actual = pSaldoDia;
                        lstFecFinMes.Add(pCierre);
                        if (contador == 0)
                            variacion = Convert.ToDecimal(lstFecFinMes[contador].saldo_actual - pSaldoCopia);
                        else
                            variacion = Convert.ToDecimal(lstFecFinMes[contador].saldo_actual - lstFecFinMes[contador - 1].saldo_actual);
                        contador += 1;
                        // Si hay disminución madura en la banda correspondiente
                        if (variacion < 0)
                        {
                            totCambio += -variacion;
                        }
                    }

                    pFecha_Ini = pFecha_Ini.AddDays(1);
                }


                //Cargando Datos
                RiesgoLiquidez pEntidad = new RiesgoLiquidez();
                pEntidad.unidad_captura = 1;
                pEntidad.renglon = 1;
                pEntidad.saldo_actual = pSaldo_actual;
                pSaldoPromedio = pSaldoTotal / 365;
                //Maduracion por bandas
                if (pSaldo_actual > pSaldoPromedio)
                {
                    if (lstFecFinMes.Count > 0)
                    {
                        decimal valorX = 0, banda1 = 0, banda2 = 0, banda3 = 0, banda4 = 0, banda5 = 0, banda6 = 0;
                        decimal controlCambio = 0;
                        DateTime pFecha_x;

                        for (int i = 0; i < lstFecFinMes.Count; i++)
                        {
                            lstFecFinMes[i].fecha_corte = lstFecFinMes[i].fecha_corte != null ? lstFecFinMes[i].fecha_corte : DateTime.MinValue;
                            lstFecFinMes[i].saldo_actual = lstFecFinMes[i].saldo_actual != null ? lstFecFinMes[i].saldo_actual : 0;
                            pFecha_x = Convert.ToDateTime(lstFecFinMes[i].fecha_corte);
                            // Determinar si hay disminución o aumento
                            decimal cambio = 0;
                            if (i == 0)
                                cambio = Convert.ToDecimal(lstFecFinMes[i].saldo_actual - pSaldoCopia);
                            else
                                cambio = Convert.ToDecimal(lstFecFinMes[i].saldo_actual - lstFecFinMes[i - 1].saldo_actual);
                            // Si hay disminución madura en la banda correspondiente
                            if (cambio < 0)
                            {
                                valorX = Math.Round(pSaldo_actual * (-cambio / totCambio));
                                if (i >= 0 && i < 1)
                                    banda1 += valorX;
                                if (i > 1 && i <= 2)
                                    banda2 += valorX;
                                if (i > 2 && i <= 3)
                                    banda3 += valorX;
                                if (i > 3 && i <= 6)
                                    banda4 += valorX;
                                if (i > 6 && i <= 9)
                                    banda5 += valorX;
                                if (i > 9 && i <= 12)
                                    banda6 += pSaldo_actual - controlCambio;
                                controlCambio += valorX;
                            }
                        }
                        //Cargando bandas
                        pEntidad.vr_brecha1 = banda1;
                        pEntidad.vr_brecha2 = banda2;
                        pEntidad.vr_brecha3 = banda3;
                        pEntidad.vr_brecha4 = banda4;
                        pEntidad.vr_brecha5 = banda5;
                        pEntidad.vr_brecha6 = banda6;
                    }
                }
                else
                {
                    // Si el saldo actual es menor que el saldo promedio anual diario entonces madura en la última banda
                    pEntidad.vr_brecha7 = pSaldo_actual;
                }

                return pEntidad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RiesgoLiquidezBusiness", "CalcularDatosRiesgoLiquidez", ex);
                return null;
            }
        }


        public RiesgoLiquidez CalcularCDATSRiesgoLiquidez(DateTime pFecha, bool pNiif, Usuario vUsuario)
        {
            try
            {
                List<RiesgoLiquidez> lstFecFinMes = new List<RiesgoLiquidez>();
                decimal pSaldo_actual = 0, pSaldoDia = 0, pSaldoCopia = 0;

                //DateTime pFecha_Ini = new DateTime((pFecha.AddYears(-1)).Year, pFecha.Month, 1);
                DateTime pFecha_Ini = pFecha.AddDays(1).AddYears(-1);

                //Calculando el saldo a la fecha de corte
                pSaldo_actual = DARiesgo.CalculoSaldoContable(pFecha, "2110", pNiif, vUsuario);
                pSaldoDia = DARiesgo.CalculoSaldoContable(pFecha_Ini, "2110", pNiif, vUsuario);
                pSaldoCopia = pSaldoDia;

                //Cargar arreglo con los periodos de las bandas
                DateTime pFecIniBanda1 = pFecha.AddDays(1);
                DateTime pFecFinBanda1 = pFecIniBanda1.AddMonths(1).AddDays(-1);

                DateTime pFecIniBanda2 = pFecIniBanda1.AddMonths(1);
                DateTime pFecFinBanda2 = pFecIniBanda2.AddMonths(1).AddDays(-1);

                DateTime pFecIniBanda3 = pFecIniBanda2.AddMonths(1);
                DateTime pFecFinBanda3 = pFecIniBanda3.AddMonths(1).AddDays(-1);

                DateTime pFecIniBanda4 = pFecIniBanda3.AddMonths(1);
                DateTime pFecFinBanda4 = pFecIniBanda4.AddMonths(3).AddDays(-1);

                DateTime pFecIniBanda5 = pFecIniBanda4.AddMonths(1);
                DateTime pFecFinBanda5 = pFecIniBanda5.AddMonths(3).AddDays(-1);

                DateTime pFecIniBanda6 = pFecIniBanda5.AddMonths(1);
                DateTime pFecFinBanda6 = pFecIniBanda6.AddMonths(3).AddDays(-1);

                DateTime pFecIniBanda7 = pFecIniBanda6.AddMonths(1);

                //Cargando Datos
                RiesgoLiquidez pEntidad = new RiesgoLiquidez();
                pEntidad.unidad_captura = 1;
                pEntidad.renglon = 1;
                pEntidad.saldo_actual = pSaldo_actual;

                return pEntidad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RiesgoLiquidezBusiness", "CalcularCDATSRiesgoLiquidez", ex);
                return null;
            }
        }

        public bool manejaLineasCreditoEmpleados(Usuario pUsuario)
        {
            return false;
        }


    }
}
