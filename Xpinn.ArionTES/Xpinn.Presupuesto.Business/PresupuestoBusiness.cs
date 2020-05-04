using System;
using System.Collections.Generic;
using System.Data;
using System.Transactions;
using Xpinn.FabricaCreditos.Data;
using Xpinn.Presupuesto.Data;
using Xpinn.Util;

namespace Xpinn.Presupuesto.Business
{

    /// <summary>
    /// Objeto de negocio para Presupuesto
    /// </summary>
    public class PresupuestoBusiness : GlobalBusiness
    {
        private PresupuestoData DAPresupuesto;
        const int numero_columnas = 7;

        /// <summary>
        /// Constructor del objeto de negocio para Presupuesto
        /// </summary>
        public PresupuestoBusiness()
        {
            DAPresupuesto = new PresupuestoData();
        }

        public int GetNumeroColumnas()
        {
            return numero_columnas;
        }

        /// <summary>
        /// Crea un Presupuesto
        /// </summary>
        /// <param name="pPresupuesto">Entidad Presupuesto</param>
        /// <returns>Entidad Presupuesto creada</returns>
        public Xpinn.Presupuesto.Entities.Presupuesto CrearPresupuesto(Xpinn.Presupuesto.Entities.Presupuesto pPresupuesto, Usuario vusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pPresupuesto = DAPresupuesto.CrearPresupuesto(pPresupuesto, vusuario);

                    foreach (DataRow drColocacion in pPresupuesto.dtColocacion.Rows)
                    {
                        if (drColocacion[0].ToString().Length >= 2)
                        {
                            if (drColocacion[0].ToString().Substring(0, 2) == "4.")
                            {
                                string sConcepto = drColocacion[0].ToString();
                                pPresupuesto.iddetallecolocacion = 0;
                                pPresupuesto.oficina = Convert.ToInt64(sConcepto.Substring(2, sConcepto.Length - 2));
                                pPresupuesto.numero_ejecutivos = Convert.ToInt64(drColocacion[2].ToString());
                                pPresupuesto = DAPresupuesto.CrearNumeroEjecutivos(pPresupuesto, vusuario);
                            }
                        }
                    }

                    int numeroPeriodo = 0;
                    foreach (DataRow drFecha in pPresupuesto.dtFechas.Rows)
                    {
                        pPresupuesto.iddetalle = 0;
                        pPresupuesto.numero_periodo = Convert.ToInt64(drFecha["numero"].ToString());
                        pPresupuesto.fecha_inicial = Convert.ToDateTime(drFecha["fecha_inicial"].ToString());
                        pPresupuesto.fecha_final = Convert.ToDateTime(drFecha["fecha_final"].ToString());
                        foreach (DataRow drFila in pPresupuesto.dtPresupuesto.Rows)
                        {
                            if (drFila["cod_cuenta"].ToString() != "")
                                pPresupuesto.cod_cuenta = Convert.ToString(drFila["cod_cuenta"].ToString());
                            else
                                pPresupuesto.cod_cuenta = " ";
                            pPresupuesto.dcentro_costo = pPresupuesto.centro_costo;
                            if (drFila[numeroPeriodo + numero_columnas].ToString() != "")
                                pPresupuesto.valor = Convert.ToDouble(drFila[numeroPeriodo + numero_columnas].ToString());
                            else
                                pPresupuesto.valor = 0;
                            if (drFila[6].ToString() != "")
                            {
                                pPresupuesto.incremento = Convert.ToDouble(drFila[6].ToString());
                            }
                            pPresupuesto = DAPresupuesto.CrearDetallePresupuesto(pPresupuesto, vusuario);
                        }
                        foreach (DataRow drFila in pPresupuesto.dtFlujo.Rows)
                        {
                            if (drFila["cod_cuenta"].ToString() != "")
                                pPresupuesto.cod_cuenta = Convert.ToString(drFila["cod_cuenta"].ToString());
                            else
                                pPresupuesto.cod_cuenta = " ";
                            pPresupuesto.dcentro_costo = pPresupuesto.centro_costo;
                            if (drFila[numeroPeriodo + numero_columnas].ToString() != "")
                                pPresupuesto.valor = Convert.ToDouble(drFila[numeroPeriodo + numero_columnas].ToString());
                            else
                                pPresupuesto.valor = 0;
                            pPresupuesto.incremento = 0;
                            if (drFila[6].ToString() != "")
                            {
                                pPresupuesto.incremento = Convert.ToDouble(drFila[6].ToString());
                            }
                            pPresupuesto = DAPresupuesto.CrearDetalleFlujo(pPresupuesto, vusuario);
                        }
                        foreach (DataRow drColocacion in pPresupuesto.dtColocacion.Rows)
                        {
                            if (drColocacion[0].ToString() == "1" || drColocacion[0].ToString() == "2" || drColocacion[0].ToString() == "3"
                                || drColocacion[0].ToString() == "9-2" || drColocacion[0].ToString() == "9-3" || drColocacion[0].ToString() == "9-4" || drColocacion[0].ToString() == "9-5")

                            {
                                if (drColocacion[numeroPeriodo + 3].ToString() != "")
                                {
                                    pPresupuesto.iddetallecolocacion = 0;
                                    pPresupuesto.item = drColocacion[0].ToString();
                                    pPresupuesto.colocacion = Convert.ToDouble(drColocacion[numeroPeriodo + 3].ToString());
                                    pPresupuesto = DAPresupuesto.CrearDetalleColocacion(pPresupuesto, vusuario);
                                }
                            }
                        }
                        foreach (DataRow drFila in pPresupuesto.dtObligacionesNuevas.Rows)
                        {
                            if (drFila["codigo"].ToString() != "")
                                pPresupuesto.codigo_obl = Convert.ToInt64(drFila["codigo"].ToString());
                            else
                                pPresupuesto.codigo_obl = 0;
                            if (drFila["descripcion"].ToString() != "")
                                pPresupuesto.descripcion_obl = Convert.ToString(drFila["descripcion"].ToString());
                            else
                                pPresupuesto.descripcion_obl = " ";
                            if (drFila["cupo"].ToString() != "")
                                pPresupuesto.cupo_obl = Convert.ToDouble(drFila["cupo"].ToString());
                            else
                                pPresupuesto.cupo_obl = 0;
                            if (drFila["tasa"].ToString() != "")
                                pPresupuesto.tasa_obl = Convert.ToDouble(drFila["tasa"].ToString());
                            else
                                pPresupuesto.tasa_obl = 0;
                            pPresupuesto.oficina_obl = 0;
                            if (drFila[numeroPeriodo + 4].ToString() != "")
                                pPresupuesto.valor = Convert.ToDouble(drFila[numeroPeriodo + 4].ToString());
                            else
                                pPresupuesto.valor = 0;
                            pPresupuesto = DAPresupuesto.CrearDetalleObligacion(pPresupuesto, vusuario);
                        }
                        numeroPeriodo += 1;
                    }                    

                    ts.Complete();
                }

                return pPresupuesto;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "CrearPresupuesto", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un Presupuesto
        /// </summary>
        /// <param name="pPresupuesto">Entidad Presupuesto</param>
        /// <returns>Entidad Presupuesto modificada</returns>
        public Xpinn.Presupuesto.Entities.Presupuesto ModificarPresupuesto(Xpinn.Presupuesto.Entities.Presupuesto pPresupuesto, Usuario vusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pPresupuesto = DAPresupuesto.ModificarPresupuesto(pPresupuesto, vusuario);

                    if (pPresupuesto.dtColocacion != null)
                    { 
                        foreach (DataRow drColocacion in pPresupuesto.dtColocacion.Rows)
                        {
                            if (drColocacion[0].ToString().Length >= 2)
                            {
                                if (drColocacion[0].ToString().Substring(0, 2) == "4.")
                                {
                                    string sConcepto = drColocacion[0].ToString();
                                    pPresupuesto.iddetallecolocacion = 0;
                                    pPresupuesto.oficina = Convert.ToInt64(sConcepto.Substring(2, sConcepto.Length-2));
                                    pPresupuesto.numero_ejecutivos = Convert.ToInt64(drColocacion[2].ToString());
                                    pPresupuesto = DAPresupuesto.CrearNumeroEjecutivos(pPresupuesto, vusuario);
                                }
                            }
                        }
                    }

                    int numeroPeriodo = 0;
                    foreach (DataRow drFecha in pPresupuesto.dtFechas.Rows)
                    {
                        pPresupuesto.iddetalle = 0;
                        pPresupuesto.numero_periodo = Convert.ToInt64(drFecha["numero"].ToString());
                        pPresupuesto.fecha_inicial = Convert.ToDateTime(drFecha["fecha_inicial"].ToString());
                        pPresupuesto.fecha_final = Convert.ToDateTime(drFecha["fecha_final"].ToString());
                        foreach (DataRow drFila in pPresupuesto.dtPresupuesto.Rows)
                        {
                            if (drFila["cod_cuenta"].ToString() != "")
                                pPresupuesto.cod_cuenta = Convert.ToString(drFila["cod_cuenta"].ToString());
                            else
                                pPresupuesto.cod_cuenta = " ";
                            pPresupuesto.dcentro_costo = pPresupuesto.centro_costo;
                            if (drFila[numeroPeriodo + numero_columnas].ToString() != "")
                                pPresupuesto.valor = Convert.ToDouble(drFila[numeroPeriodo + numero_columnas].ToString());
                            else
                                pPresupuesto.valor = 0;
                            pPresupuesto.incremento = 0;
                            if (drFila[6].ToString() != "")
                            {
                                pPresupuesto.incremento = Convert.ToDouble(drFila[6].ToString());
                            }
                            pPresupuesto = DAPresupuesto.CrearDetallePresupuesto(pPresupuesto, vusuario);
                        }
                        foreach (DataRow drFila in pPresupuesto.dtFlujo.Rows)
                        {
                            if (drFila["cod_cuenta"].ToString() != "")
                                pPresupuesto.cod_cuenta = Convert.ToString(drFila["cod_cuenta"].ToString());
                            else
                                pPresupuesto.cod_cuenta = " ";
                            pPresupuesto.dcentro_costo = pPresupuesto.centro_costo;
                            if (drFila[numeroPeriodo + numero_columnas].ToString() != "")
                                pPresupuesto.valor = Convert.ToDouble(drFila[numeroPeriodo + numero_columnas].ToString());
                            else
                                pPresupuesto.valor = 0;
                            pPresupuesto.incremento = 0;
                            if (drFila[6].ToString() != "")
                            {
                                pPresupuesto.incremento = Convert.ToDouble(drFila[6].ToString());
                            }
                            pPresupuesto = DAPresupuesto.CrearDetalleFlujo(pPresupuesto, vusuario);
                        }

                        if (pPresupuesto.dtColocacion != null)
                        {
                            foreach (DataRow drColocacion in pPresupuesto.dtColocacion.Rows)
                            {
                                if (drColocacion[numeroPeriodo + 3].ToString() != "")
                                {
                                    pPresupuesto.iddetallecolocacion = 0;
                                    pPresupuesto.item = drColocacion[0].ToString();
                                    pPresupuesto.colocacion = Convert.ToDouble(drColocacion[numeroPeriodo + 3].ToString());
                                    pPresupuesto = DAPresupuesto.CrearDetalleColocacion(pPresupuesto, vusuario);
                                }
                            }
                        }
                        if (pPresupuesto.dtObligacionesNuevas != null)
                        {
                            foreach (DataRow drFila in pPresupuesto.dtObligacionesNuevas.Rows)
                            {
                                if (drFila["codigo"].ToString() != "")
                                    pPresupuesto.codigo_obl = Convert.ToInt64(drFila["codigo"].ToString());
                                else
                                    pPresupuesto.codigo_obl = 0;
                                if (drFila["descripcion"].ToString() != "")
                                    pPresupuesto.descripcion_obl = Convert.ToString(drFila["descripcion"].ToString());
                                else
                                    pPresupuesto.descripcion_obl = " ";
                                if (drFila["cupo"].ToString() != "")
                                    pPresupuesto.cupo_obl = Convert.ToDouble(drFila["cupo"].ToString());
                                else
                                    pPresupuesto.cupo_obl = 0;
                                if (drFila["tasa"].ToString() != "")
                                    pPresupuesto.tasa_obl = Convert.ToDouble(drFila["tasa"].ToString());
                                else
                                    pPresupuesto.tasa_obl = 0;
                                if (drFila["plazo"].ToString() != "")
                                    pPresupuesto.plazo_obl = Convert.ToDouble(drFila["plazo"].ToString());
                                else
                                    pPresupuesto.plazo_obl = 0;
                                if (drFila["gracia"].ToString() != "")
                                    pPresupuesto.gracia_obl = Convert.ToDouble(drFila["gracia"].ToString());
                                else
                                    pPresupuesto.gracia_obl = 0;
                                if (drFila["cod_periodicidad"].ToString() != "")
                                    pPresupuesto.cod_periodicidad_obl = Convert.ToDouble(drFila["cod_periodicidad"].ToString());
                                else
                                    pPresupuesto.cod_periodicidad_obl = 0;
                                pPresupuesto.oficina_obl = 0;
                                if (drFila[numeroPeriodo + 8].ToString() != "")
                                    pPresupuesto.valor = Convert.ToDouble(drFila[numeroPeriodo + 8].ToString());
                                else
                                    pPresupuesto.valor = 0;
                                pPresupuesto = DAPresupuesto.CrearDetalleObligacion(pPresupuesto, vusuario);
                            }
                        }
                        numeroPeriodo += 1;
                    }

                    ts.Complete();
                }

                return pPresupuesto;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "ModificarPresupuesto", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un Presupuesto
        /// </summary>
        /// <param name="pId">Identificador de Presupuesto</param>
        public void EliminarPresupuesto(Int64 pId, Usuario vusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAPresupuesto.EliminarPresupuesto(pId, vusuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "EliminarPresupuesto", ex);
            }
        }

        /// <summary>
        /// Obtiene un Presupuesto
        /// </summary>
        /// <param name="pId">Identificador de Presupuesto</param>
        /// <returns>Entidad Presupuesto</returns>
        public Xpinn.Presupuesto.Entities.Presupuesto ConsultarPresupuesto(Int64 pId, Usuario vusuario)
        {
            try
            {
                Xpinn.Presupuesto.Entities.Presupuesto Presupuesto = new Xpinn.Presupuesto.Entities.Presupuesto();

                Presupuesto = DAPresupuesto.ConsultarPresupuesto(pId, vusuario);

                return Presupuesto;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "ConsultarPresupuesto", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pPresupuesto">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Presupuesto obtenidos</returns>
        public List<Xpinn.Presupuesto.Entities.Presupuesto> ListarPresupuesto(Xpinn.Presupuesto.Entities.Presupuesto pPresupuesto, Usuario vUsuario)
        {
            try
            {
                return DAPresupuesto.ListarPresupuesto(pPresupuesto, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "ListarPresupuesto", ex);
                return null;
            }
        }

        public List<Xpinn.Presupuesto.Entities.Presupuesto> ListarPeriodosPresupuesto(Xpinn.Presupuesto.Entities.Presupuesto pPresupuesto, Usuario vUsuario)
        {
            try
            {
                return DAPresupuesto.ListarPeriodosPresupuesto(pPresupuesto, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "ListarPeriodosPresupuesto", ex);
                return null;
            }
        }

        public DataTable ListarCuentas(string filtro, Usuario pUsuario)
        {
            try
            {
                return DAPresupuesto.ListarCuentas(filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "ListarCuentas", ex);
                return null;
            }
        }

        public DataTable ListarEjecucionPresupuesto(Int64 pIdPresupuesto, Int64 pNumeroPeriodo, Usuario pUsuario,Int32 nivel)
        {
            

            try
            {
                DataTable dtEjecucion = new DataTable();
                dtEjecucion = DAPresupuesto.ListarEjecucionPresupuesto(pIdPresupuesto, pNumeroPeriodo, pUsuario);
                DataColumn[] dtLlavePresupuesto = new DataColumn[1];
                dtLlavePresupuesto[0] = dtEjecucion.Columns[0];
                dtEjecucion.PrimaryKey = dtLlavePresupuesto;
                foreach (DataRow drFila in dtEjecucion.Rows)
                {
                    string codCuenta = "";
                    DateTime fecha_inicial = DateTime.MaxValue;
                    DateTime fecha_final = DateTime.MaxValue;
                    Int64 centro_costo = 0;
                    decimal valor_presupuestado = 0;
                    decimal valor_presupuestado_acumulado = 0;
                    decimal valor_ejecutado = 0;
                    decimal valor_ejecutado_acumulado = 0;
                    if (drFila["cod_cuenta"].ToString().Length>nivel)
                    {
                        codCuenta = drFila["cod_cuenta"].ToString();
                        fecha_inicial = Convert.ToDateTime(drFila["fecha_inicial"].ToString());
                        fecha_final = Convert.ToDateTime(drFila["fecha_final"].ToString());
                        centro_costo = Convert.ToInt64(drFila["centro_costo"].ToString());
                        valor_presupuestado = Convert.ToDecimal(drFila["valor_presupuestado"].ToString());
                        valor_ejecutado = 0;
                        if (DAPresupuesto.ConsultarValorEjecutado(pIdPresupuesto, pNumeroPeriodo, fecha_inicial, fecha_final, codCuenta, centro_costo, ref valor_presupuestado, ref valor_ejecutado, ref valor_presupuestado_acumulado, ref valor_ejecutado_acumulado, pUsuario))
                        {
                            drFila["valor_ejecutado"] = valor_ejecutado;
                            drFila["acumulado_presupuestado"] = valor_presupuestado_acumulado;
                            drFila["acumulado_ejecutado"] = valor_ejecutado_acumulado;
                        }
                        else
                        {
                            drFila["valor_ejecutado"] = 0;
                            drFila["acumulado_presupuestado"] = 0;
                            drFila["acumulado_ejecutado"] = 0;
                        }
                    }
                }

                // Mayorizar las cuentas contables
                foreach (DataRow drPres in dtEjecucion.Rows)
                {
                    // Si la cuenta es auxiliar entonces mayorizar
                    if (drPres[0].ToString().Length > nivel && drPres["valor_ejecutado"].ToString() != "" && drPres["valor_ejecutado"].ToString() != "0")
                    {
                        decimal valorCuenta = 0;
                        valorCuenta = Convert.ToDecimal(drPres["valor_ejecutado"].ToString());
                        string depende_de = "";
                        depende_de = (string)drPres["depende_de"];
                        DataRow drCUENTA = dtEjecucion.Rows.Find(depende_de);
                        while (drCUENTA != null && depende_de != "0")
                        {
                            decimal saldoCuenta = 0;
                            if (drCUENTA["valor_ejecutado"].ToString() != "")
                                saldoCuenta = (decimal)drCUENTA["valor_ejecutado"];
                            drCUENTA["valor_ejecutado"] = saldoCuenta + valorCuenta;
                            if (drCUENTA["depende_de"].ToString() != "")
                                depende_de = (string)drCUENTA["depende_de"];
                            drCUENTA = dtEjecucion.Rows.Find(depende_de);
                        }
                    }
                    // Si la cuenta es auxiliar entonces mayorizar
                    if (drPres[0].ToString().Length > nivel && drPres["acumulado_presupuestado"].ToString() != "" && drPres["acumulado_presupuestado"].ToString() != "0")
                    {
                        decimal valorCuentaAcumulado = 0;
                        valorCuentaAcumulado = Convert.ToDecimal(drPres["acumulado_presupuestado"].ToString());
                        string depende_de = "";
                        depende_de = (string)drPres["depende_de"];
                        DataRow drCUENTA = dtEjecucion.Rows.Find(depende_de);
                        while (drCUENTA != null && depende_de != "0")
                        {
                            decimal saldoCuenta = 0;
                            if (drCUENTA["acumulado_presupuestado"].ToString() != "")
                                saldoCuenta = (decimal)drCUENTA["acumulado_presupuestado"];
                            drCUENTA["acumulado_presupuestado"] = saldoCuenta + valorCuentaAcumulado;
                            if (drCUENTA["depende_de"].ToString() != "")
                                depende_de = (string)drCUENTA["depende_de"];
                            drCUENTA = dtEjecucion.Rows.Find(depende_de);
                        }
                    }
                    // Si la cuenta es auxiliar entonces mayorizar
                    if (drPres[0].ToString().Length > nivel && drPres["acumulado_ejecutado"].ToString() != "" && drPres["acumulado_ejecutado"].ToString() != "0")
                    {
                        decimal valorCuentaAcumulado = 0;
                        valorCuentaAcumulado = Convert.ToDecimal(drPres["acumulado_ejecutado"].ToString());
                        string depende_de = "";
                        depende_de = (string)drPres["depende_de"];
                        DataRow drCUENTA = dtEjecucion.Rows.Find(depende_de);
                        while (drCUENTA != null && depende_de != "0")
                        {
                            decimal saldoCuenta = 0;
                            if (drCUENTA["acumulado_ejecutado"].ToString() != "")
                                saldoCuenta = (decimal)drCUENTA["acumulado_ejecutado"];
                            drCUENTA["acumulado_ejecutado"] = saldoCuenta + valorCuentaAcumulado;
                            if (drCUENTA["depende_de"].ToString() != "")
                                depende_de = (string)drCUENTA["depende_de"];
                            drCUENTA = dtEjecucion.Rows.Find(depende_de);
                        }
                    }
                }

                // Calculando la utilidad del ejercicio
                decimal ingresos = 0;
                decimal gastos = 0;
                decimal costos = 0;
                DataRow drINGRESOS = dtEjecucion.Rows.Find("4");
                if (drINGRESOS["valor_ejecutado"].ToString() != "")
                    ingresos = (decimal)drINGRESOS["valor_ejecutado"];
                DataRow drGASTOS = dtEjecucion.Rows.Find("5");
                if (drGASTOS["valor_ejecutado"].ToString() != "")
                    gastos = (decimal)drGASTOS["valor_ejecutado"];
                DataRow drCOSTOS = dtEjecucion.Rows.Find("6");
                if (drCOSTOS["valor_ejecutado"].ToString() != "")
                    costos = (decimal)drCOSTOS["valor_ejecutado"];
                DataRow drUTILIDAD = dtEjecucion.Rows.Find("3505");
                if (drUTILIDAD != null)
                {
                    drUTILIDAD["nombre"] = "UTILIDAD O PERDIDA DEL EJERCICIO";
                    drUTILIDAD["valor_ejecutado"] = ingresos - gastos - costos;
                }

                // Calculando la utilidad del ejercicio
                decimal ingresosAcumulado = 0;
                decimal gastosAcumulado = 0;
                decimal costosAcumulado = 0;
                DataRow drINGRESOSACUM = dtEjecucion.Rows.Find("4");
                if (drINGRESOSACUM["acumulado_ejecutado"].ToString() != "")
                    ingresosAcumulado = (decimal)drINGRESOSACUM["acumulado_ejecutado"];
                DataRow drGASTOSACUM = dtEjecucion.Rows.Find("5");
                if (drGASTOSACUM["acumulado_ejecutado"].ToString() != "")
                    gastosAcumulado = (decimal)drGASTOSACUM["acumulado_ejecutado"];
                DataRow drCOSTOSACUM = dtEjecucion.Rows.Find("6");
                if (drCOSTOSACUM["acumulado_ejecutado"].ToString() != "")
                    costosAcumulado = (decimal)drCOSTOSACUM["acumulado_ejecutado"];
                DataRow drUTILIDADACUM = dtEjecucion.Rows.Find("3505");
                if (drUTILIDADACUM != null)
                {
                    drUTILIDADACUM["nombre"] = "UTILIDAD O PERDIDA DEL EJERCICIO";
                    drUTILIDADACUM["acumulado_ejecutado"] = ingresosAcumulado - gastosAcumulado - costosAcumulado;
                }

                // Calculando la utilidad del ejercicio
                decimal ingresosPreAcu = 0;
                decimal gastosPreAcu = 0;
                decimal costosPreAcu = 0;
                DataRow drINGRESOSPREACUM = dtEjecucion.Rows.Find("4");
                if (drINGRESOSPREACUM["acumulado_presupuestado"].ToString() != "")
                    ingresosPreAcu = (decimal)drINGRESOSPREACUM["acumulado_presupuestado"];
                DataRow drGASTOSPREACUM = dtEjecucion.Rows.Find("5");
                if (drGASTOSPREACUM["acumulado_presupuestado"].ToString() != "")
                    gastosPreAcu = (decimal)drGASTOSPREACUM["acumulado_presupuestado"];
                DataRow drCOSTOSPREACUM = dtEjecucion.Rows.Find("6");
                if (drCOSTOSPREACUM["acumulado_presupuestado"].ToString() != "")
                    costosPreAcu = (decimal)drCOSTOSPREACUM["acumulado_presupuestado"];
                DataRow drUTILIDADPREACUM = dtEjecucion.Rows.Find("3505");
                if (drUTILIDADPREACUM != null)
                {
                    drUTILIDADPREACUM["nombre"] = "UTILIDAD O PERDIDA DEL EJERCICIO";
                    drUTILIDADPREACUM["acumulado_presupuestado"] = ingresosPreAcu - gastosPreAcu - costosPreAcu;
                }

                // Calculando la diferencia y porcentaje
                foreach (DataRow drFila in dtEjecucion.Rows)
                {
                    decimal valor_presupuestado = 0;
                    decimal valor_ejecutado = 0;
                    valor_presupuestado = Convert.ToDecimal(drFila["valor_presupuestado"].ToString());
                    valor_ejecutado = Convert.ToDecimal(drFila["valor_ejecutado"].ToString());
                    drFila["diferencia"] = (valor_ejecutado - valor_presupuestado);
                    if (valor_presupuestado != 0)
                        drFila["porcentaje"] = ((valor_ejecutado - valor_presupuestado) / valor_presupuestado) * 100;
                    else
                        drFila["porcentaje"] = 0;

                    decimal valor_presupuestadoAcumulado = 0;
                    decimal valor_ejecutadoAcumulado = 0;
                    valor_presupuestadoAcumulado = Convert.ToDecimal(drFila["acumulado_presupuestado"].ToString());
                    valor_ejecutadoAcumulado = Convert.ToDecimal(drFila["acumulado_ejecutado"].ToString());
                    drFila["acumulado_diferencia"] = (valor_ejecutadoAcumulado - valor_presupuestadoAcumulado);
                    if (valor_presupuestadoAcumulado != 0)
                        drFila["acumulado_porcentaje"] = ((valor_ejecutadoAcumulado - valor_presupuestadoAcumulado) / valor_presupuestadoAcumulado) * 100;
                    else
                        drFila["acumulado_porcentaje"] = 0;
                }

                return dtEjecucion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "ListarEjecucionPresupuesto", ex);
                return null;
            }
        }

        public decimal SaldoPromedioCuenta(string pcod_cuenta, DateTime pfecha_inicial, DateTime pfecha_final, string pfiltro, Usuario pUsuario)
        {
            try
            {
                return DAPresupuesto.SaldoPromedioCuenta(pcod_cuenta, pfecha_inicial, pfecha_final, pfiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "SaldoPromedioCuenta", ex);
                return 0;
            }
        }

        public decimal SaldoFinalCuenta(string pcod_cuenta, DateTime pfecha_final, string pfiltro, Usuario pUsuario)
        {
            try
            {
                return DAPresupuesto.SaldoFinalCuenta(pcod_cuenta, pfecha_final, pfiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "SaldoFinalCuenta", ex);
                return 0;
            }
        }

        public decimal SaldoPeriodoCuenta(string pcod_cuenta, DateTime pfecha_final, string pfiltro, Usuario pUsuario)
        {
            try
            {
                return DAPresupuesto.SaldoPeriodoCuenta(pcod_cuenta, pfecha_final, pfiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "SaldoPeriodoCuenta", ex);
                return 0;
            }
        }

        public void MayorizarPresupuesto(ref DataTable dtPresupuesto, DataTable dtFechas, Usuario pUsuario, Int16 nivel)
        {
            try
            {
                double UtilidadAnterior = 0;

                // Inicializando variable de número de columnas
                int numeroPeriodo = 0;

                foreach (DataRow drFecha in dtFechas.Rows)
                {

                    // Inicializar las cuentas contables
                    foreach (DataRow drPres in dtPresupuesto.Rows)
                    {
                        // Si la cuenta no es auxiliar entonces inicializar
                        if (drPres[0].ToString().Length <= nivel && drPres[numeroPeriodo + numero_columnas].ToString() != "" && drPres[0].ToString() != "12")
                            drPres[numeroPeriodo + numero_columnas] = 0;
                    }

                    // Mayorizar las cuentas contables
                    foreach (DataRow drPres in dtPresupuesto.Rows)
                    {
                        // Si la cuenta es auxiliar entonces mayorizar
                        if (drPres[0].ToString().Length > nivel && drPres[numeroPeriodo + numero_columnas].ToString() != "" && drPres[numeroPeriodo + numero_columnas].ToString() != "0")
                        {
                            double valorCuenta = 0;
                            valorCuenta = (double)drPres[numeroPeriodo + numero_columnas];
                            string depende_de ="";
                            string CUENTA = Convert.ToString(drPres[0]);
                            depende_de = DAPresupuesto.CuentaDependeDe(CUENTA, pUsuario);
                            DataRow drCUENTA = dtPresupuesto.Rows.Find(depende_de);
                            while (drCUENTA != null && depende_de != "0")
                            {
                                double saldoCuenta = 0;
                                if (drCUENTA[numeroPeriodo + numero_columnas].ToString() != "")
                                    saldoCuenta = (double)drCUENTA[numeroPeriodo + numero_columnas];
                                drCUENTA[numeroPeriodo + numero_columnas] = saldoCuenta + valorCuenta;
                                if (drCUENTA[2].ToString() != "")
                                    depende_de = (string)drCUENTA[2];
                                drCUENTA = dtPresupuesto.Rows.Find(depende_de);
                            }
                        }
                    }

                    // Calculando la utilidad del ejercicio
                    double ingresos = 0;
                    double gastos = 0;
                    double costos = 0;
                    DataRow drINGRESOS = dtPresupuesto.Rows.Find("4");
                    if (drINGRESOS[numeroPeriodo + numero_columnas].ToString() != "")
                        ingresos = (double)drINGRESOS[numeroPeriodo + numero_columnas];
                    DataRow drGASTOS = dtPresupuesto.Rows.Find("5");
                    if (drGASTOS[numeroPeriodo + numero_columnas].ToString() != "")
                        gastos = (double)drGASTOS[numeroPeriodo + numero_columnas];
                    DataRow drCOSTOS = dtPresupuesto.Rows.Find("6");
                    if (drCOSTOS[numeroPeriodo + numero_columnas].ToString() != "")
                        costos = (double)drCOSTOS[numeroPeriodo + numero_columnas];
                    DataRow drUTILIDAD = dtPresupuesto.Rows.Find("3505");
                    if (drUTILIDAD != null)
                        drUTILIDAD[numeroPeriodo + numero_columnas] = ingresos - gastos - costos;
                    DataRow drUTILIDADACUMULADA = dtPresupuesto.Rows.Find("35");
                    if (drUTILIDADACUMULADA != null)
                    {
                        drUTILIDADACUMULADA[numeroPeriodo + numero_columnas] = UtilidadAnterior + (ingresos - gastos - costos);
                        UtilidadAnterior = Convert.ToDouble(drUTILIDADACUMULADA[numeroPeriodo + numero_columnas]);
                    }

                    // Ir al siguiente período
                    numeroPeriodo += 1;
                };

                TotalizarPresupuesto(ref dtPresupuesto, dtFechas);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "MayorizarPresupuesto", ex);
                return;
            }
        }

        public void TotalizarPresupuesto(ref DataTable dtPresupuesto, DataTable dtFechas)
        {
            try
            {
                int numeroPeriodo = 0;
                // Calcular valor de totales
                foreach (DataRow drPres in dtPresupuesto.Rows)
                {
                    double valorTotal = 0;
                    numeroPeriodo = 0;
                    foreach (DataRow drFecha in dtFechas.Rows)
                    {
                        if (drPres[0].ToString() != "35")
                        {
                            if (drPres[numeroPeriodo + numero_columnas] != null && drPres[numeroPeriodo + numero_columnas].ToString() != "")
                                valorTotal = valorTotal + Convert.ToDouble(drPres[numeroPeriodo + numero_columnas].ToString());
                        }
                        numeroPeriodo += 1;
                    }
                    drPres[numeroPeriodo + numero_columnas] = valorTotal;
                }

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "TotalizarPresupuesto", ex);
                return;
            }
        }

        public DataTable GenerarFlujoCaja(DataTable dtPresupuesto, DataTable dtFechas, DataTable dtColocacion, DataTable dtObligaciones, DataTable dtObligacionesPagos, DataTable dtObligacionesNuevas, DataTable dtTecnologia, double pvalorFlujoInicial, Usuario pUsuario,Int16 nivel)
        {
            DataRow drPorMor1;
            drPorMor1 = dtColocacion.Rows.Find("9-2");
            DataRow drPorMor2;
            drPorMor2 = dtColocacion.Rows.Find("9-3");
            DataRow drPorMor3;
            drPorMor3 = dtColocacion.Rows.Find("9-4");
            DataRow drPorMor4;
            drPorMor4 = dtColocacion.Rows.Find("9-5");
            DataRow drIngresos;
            drIngresos = dtColocacion.Rows.Find("11");
            string cod_cuenta_ingresos = DAPresupuesto.ConsultarParametroCuenta(1, pUsuario);

            // Ajustar pago de cesantias e intereses de cesantias
            foreach (DataRow drFila in dtPresupuesto.Rows)
            {
                if (drFila[0].ToString() == "510530")
                {
                    int numeroPer = 0;
                    double totalCesantias = 0;
                    foreach (DataRow drFecha in dtFechas.Rows)
                    {
                        if (drFila[numeroPer + numero_columnas].ToString() != "")
                        {
                            totalCesantias = totalCesantias + Convert.ToDouble(drFila[numeroPer + numero_columnas].ToString());
                            drFila[numeroPer + numero_columnas] = 0;
                            drFila.AcceptChanges();
                        }
                        numeroPer = numeroPer + 1;
                    }
                    numeroPer = 0;
                    foreach (DataRow drFecha in dtFechas.Rows)
                    {
                        DateTime fecha_final = DateTime.MinValue;
                        fecha_final = Convert.ToDateTime(drFecha[2]);
                        if (fecha_final.Month == 2)
                        {
                            drFila[numeroPer + numero_columnas] = totalCesantias;
                            drFila.AcceptChanges();
                        }
                        numeroPer = numeroPer + 1;
                    }
                }
                if (drFila[0].ToString() == "510533")
                {
                    int numeroPer = 0;
                    double totalIntCesantias = 0;
                    foreach (DataRow drFecha in dtFechas.Rows)
                    {
                        if (drFila[numeroPer + numero_columnas].ToString() != "")
                        {
                            totalIntCesantias = totalIntCesantias + Convert.ToDouble(drFila[numeroPer + numero_columnas].ToString());
                            drFila[numeroPer + numero_columnas] = 0;
                            drFila.AcceptChanges();
                        }
                        numeroPer = numeroPer + 1;
                    }
                    numeroPer = 0;
                    foreach (DataRow drFecha in dtFechas.Rows)
                    {
                        DateTime fecha_final = DateTime.MinValue;
                        fecha_final = Convert.ToDateTime(drFecha[2]);
                        if (fecha_final.Month == 1)
                        {
                            drFila[numeroPer + numero_columnas] = totalIntCesantias;
                            drFila.AcceptChanges();
                        }
                        numeroPer = numeroPer + 1;
                    }
                }
                if (drFila[0].ToString() == "510536")
                {
                    int numeroPer = 0;
                    double totalPrimas = 0;
                    foreach (DataRow drFecha in dtFechas.Rows)
                    {
                        if (drFila[numeroPer + numero_columnas].ToString() != "")
                        {
                            totalPrimas = totalPrimas + Convert.ToDouble(drFila[numeroPer + numero_columnas].ToString());
                            drFila[numeroPer + numero_columnas] = 0;
                            drFila.AcceptChanges();
                        }
                        numeroPer = numeroPer + 1;
                    }
                    numeroPer = 0;
                    foreach (DataRow drFecha in dtFechas.Rows)
                    {
                        DateTime fecha_final = DateTime.MinValue;
                        fecha_final = Convert.ToDateTime(drFecha[2]);
                        if (fecha_final.Month == 6 || fecha_final.Month == 12)
                        {
                            drFila[numeroPer + numero_columnas] = Math.Round(totalPrimas/2, 2);
                            drFila.AcceptChanges();
                        }
                        numeroPer = numeroPer + 1;
                    }
                }
                if (drFila[0].ToString().Length >= 6)
                {
                    if (drFila[0].ToString().Substring(0, 4) == "5115" || drFila[0].ToString().Substring(0, 4) == "5120" || drFila[0].ToString().Substring(0, 4) == "5125")
                    {
                        int numeroPer = 0;
                        foreach (DataRow drFecha in dtFechas.Rows)
                        {
                            if (drFila[numeroPer + numero_columnas].ToString() != "")
                            {                                
                                drFila[numeroPer + numero_columnas] = 0;
                                drFila.AcceptChanges();
                            }
                            numeroPer = numeroPer + 1;
                        }
                    }
                }
                // Calculando ingresos
                if (drFila[0].ToString() == cod_cuenta_ingresos)
                {
                    int numeroPer = 0;
                    foreach (DataRow drFecha in dtFechas.Rows)
                    {
                        if (drFila[numeroPer + numero_columnas].ToString() != "")
                        {
                            double valorIngresos = 0;
                            if (drIngresos != null)
                                if (drIngresos[numeroPer + 3].ToString() != "")
                                    valorIngresos = (double)drIngresos[numeroPer + 3];
                            double porMora1 = 0;
                            if (drPorMor1 != null)
                                if (drPorMor1[numeroPer + 3].ToString() != "")
                                    porMora1 = (double)drPorMor1[numeroPer + 3];
                            double porMora2 = 0;
                            if (drPorMor2 != null)
                                if (drPorMor2[numeroPer + 3].ToString() != "")
                                    porMora2 = (double)drPorMor2[numeroPer + 3];
                            double porMora3 = 0;
                            if (drPorMor3 != null)
                                if (drPorMor3[numeroPer + 3].ToString() != "")
                                    porMora3 = (double)drPorMor3[numeroPer + 3];
                            double porMora4 = 0;
                            if (drPorMor4 != null)
                                if (drPorMor4[numeroPer + 3].ToString() != "")
                                    porMora4 = (double)drPorMor4[numeroPer + 3];
                            DataRow drINGRESOSMICROCREDITO;
                            drINGRESOSMICROCREDITO = dtPresupuesto.Rows.Find(cod_cuenta_ingresos);
                            valorIngresos = valorIngresos - (valorIngresos * (porMora1 + porMora2 + porMora3 + porMora4) / 100);
                            if (drINGRESOSMICROCREDITO != null)
                                drINGRESOSMICROCREDITO[numeroPer + numero_columnas] = valorIngresos;
                        }
                        numeroPer = numeroPer + 1;
                    }
                }
            }

            DataRow drSaldoInicial;
            if (dtPresupuesto.Rows.Find("0") == null)
            {
                drSaldoInicial = dtPresupuesto.NewRow();
                drSaldoInicial[0] = "0";
                drSaldoInicial[1] = "SALDO INICIAL";
                drSaldoInicial[2] = 0;
                dtPresupuesto.Rows.InsertAt(drSaldoInicial, 0);
            }
            else
            {
                drSaldoInicial = dtPresupuesto.Rows.Find("0");
            }

            DataRow drSubtotal;
            if (dtPresupuesto.Rows.Find("10") == null)
            {
                drSubtotal = dtPresupuesto.NewRow();
                drSubtotal[0] = "10";
                drSubtotal[1] = "SUBTOTAL FLUJO DE CAJA";
                drSubtotal[2] = 0;
                dtPresupuesto.Rows.Add(drSubtotal);
            }
            else
            {
                drSubtotal = dtPresupuesto.Rows.Find("10");
            }

            DataRow drColocacion;
            if (dtPresupuesto.Rows.Find("11") == null)
            {
                drColocacion = dtPresupuesto.NewRow();
                drColocacion[0] = "11";
                drColocacion[1] = "COLOCACIONES DE CARTERA";
                drColocacion[2] = 0;
                dtPresupuesto.Rows.Add(drColocacion);
            }
            else
            {
                drColocacion = dtPresupuesto.Rows.Find("11");
            }

            DataRow drPagosObl;
            if (dtPresupuesto.Rows.Find("12") == null)
            {
                drPagosObl = dtPresupuesto.NewRow();
                drPagosObl[0] = "12";
                drPagosObl[1] = "PAGO CAPITAL OBLIGACIONES FINANCIERAS";
                drPagosObl[2] = 0;
                dtPresupuesto.Rows.Add(drPagosObl);
            }
            else
            {
                drPagosObl = dtPresupuesto.Rows.Find("12");
            }

            DataRow drRecCap;
            if (dtPresupuesto.Rows.Find("13") == null)
            {
                drRecCap = dtPresupuesto.NewRow();
                drRecCap[0] = "13";
                drRecCap[1] = "RECUPERACIONES DE CAPITAL";
                drRecCap[2] = 0;
                dtPresupuesto.Rows.Add(drRecCap);
            }
            else
            {
                drRecCap = dtPresupuesto.Rows.Find("13");
            }

            DataRow drFondos;
            if (dtPresupuesto.Rows.Find("14") == null)
            {
                drFondos = dtPresupuesto.NewRow();
                drFondos[0] = "14";
                drFondos[1] = "FONDO DE DESTINACION ESPECIFICA (metodolgoco, riesgos e investigacion sectorial)";
                drFondos[2] = 0;
                dtPresupuesto.Rows.Add(drFondos);
            }
            else
            {
                drFondos = dtPresupuesto.Rows.Find("14");
            }

            DataRow drAdqTec;
            if (dtPresupuesto.Rows.Find("15") == null)
            {
                drAdqTec = dtPresupuesto.NewRow();
                drAdqTec[0] = "15";
                drAdqTec[1] = "ADQUISICIONES DE TECNOLOGIA";
                drAdqTec[2] = 0;
                dtPresupuesto.Rows.Add(drAdqTec);
            }
            else
            {
                drAdqTec = dtPresupuesto.Rows.Find("15");
            }

            DataRow drFondReq;
            if (dtPresupuesto.Rows.Find("16") == null)
            {
                drFondReq = dtPresupuesto.NewRow();
                drFondReq[0] = "16";
                drFondReq[1] = "FONDEO REQUERIDO";
                drFondReq[2] = 0;
                dtPresupuesto.Rows.Add(drFondReq);
            }
            else
            {
                drFondReq = dtPresupuesto.Rows.Find("16");
            }

            DataRow drFluCaj;
            if (dtPresupuesto.Rows.Find("17") == null)
            {
                drFluCaj = dtPresupuesto.NewRow();
                drFluCaj[0] = "17";
                drFluCaj[1] = "FLUJO DE CAJA FINAL";
                drFluCaj[2] = 0;
                dtPresupuesto.Rows.Add(drFluCaj);
            }
            else
            {
                drFluCaj = dtPresupuesto.Rows.Find("17");
            }

            // Carga al flujo los ingresos por obligaciones diferente a que en el PYG se cargan son provisiones            
            CargarPresupuestoObligacion(ref dtPresupuesto, dtObligaciones, dtObligacionesNuevas, dtObligacionesPagos, dtFechas, false, true, pUsuario,nivel);
            drPagosObl = dtPresupuesto.Rows.Find("12");

            // Calculando la utilidad del ejercicio
            int numeroPeriodo = 0;
            double UtilidadAnterior = 0;
            foreach (DataRow drFila in dtFechas.Rows)
            {
                double ingresos = 0;
                double gastos = 0;
                double costos = 0;
                DataRow drINGRESOS = dtPresupuesto.Rows.Find("4");
                if (drINGRESOS[numeroPeriodo + numero_columnas].ToString() != "")
                    ingresos = (double)drINGRESOS[numeroPeriodo + numero_columnas];
                DataRow drGASTOS = dtPresupuesto.Rows.Find("5");
                if (drGASTOS[numeroPeriodo + numero_columnas].ToString() != "")
                    gastos = (double)drGASTOS[numeroPeriodo + numero_columnas];
                DataRow drCOSTOS = dtPresupuesto.Rows.Find("6");
                if (drCOSTOS[numeroPeriodo + numero_columnas].ToString() != "")
                    costos = (double)drCOSTOS[numeroPeriodo + numero_columnas];
                DataRow drUTILIDAD = dtPresupuesto.Rows.Find("3505");
                if (drUTILIDAD != null)
                    drUTILIDAD[numeroPeriodo + numero_columnas] = ingresos - gastos - costos;
                DataRow drUTILIDADACUMULADA = dtPresupuesto.Rows.Find("35");
                if (drUTILIDADACUMULADA != null)
                {
                    drUTILIDADACUMULADA[numeroPeriodo + numero_columnas] = UtilidadAnterior + (ingresos - gastos - costos);
                    UtilidadAnterior = Convert.ToDouble(drUTILIDADACUMULADA[numeroPeriodo + numero_columnas]);
                }
            }

            DataRow drUtilidad;
            drUtilidad = dtPresupuesto.Rows.Find("3505");
            if (drUtilidad != null)
                dtPresupuesto.Rows.Remove(drUtilidad);
            DataRow drUtilidadAcu;
            drUtilidadAcu = dtPresupuesto.Rows.Find("35");
            if (drUtilidadAcu != null)
                dtPresupuesto.Rows.Remove(drUtilidadAcu);

            DataRow drTotalColocacion;
            drTotalColocacion = dtColocacion.Rows.Find("5");

            DataRow drPagCapColocaciones;
            drPagCapColocaciones = dtColocacion.Rows.Find("8");

            numeroPeriodo = 0;
            double saldoInicial = pvalorFlujoInicial;
            try
            {
                foreach (DataRow drFecha in dtFechas.Rows)
                {
                    // Calculando el subtotal del flujo
                    double subTotal = 0;
                    double ingresos = 0;
                    double gastos = 0;
                    double costos = 0;
                    DataRow drINGRESOS = dtPresupuesto.Rows.Find("4");
                    if (drINGRESOS[numeroPeriodo + numero_columnas].ToString() != "")
                        ingresos = (double)drINGRESOS[numeroPeriodo + numero_columnas];
                    DataRow drGASTOS = dtPresupuesto.Rows.Find("5");
                    if (drGASTOS[numeroPeriodo + numero_columnas].ToString() != "")
                        gastos = (double)drGASTOS[numeroPeriodo + numero_columnas];
                    DataRow drCOSTOS = dtPresupuesto.Rows.Find("6");
                    if (drCOSTOS[numeroPeriodo + numero_columnas].ToString() != "")
                        costos = (double)drCOSTOS[numeroPeriodo + numero_columnas];
                    subTotal = ingresos - gastos - costos;

                    if (drSaldoInicial != null)
                    {
                        drSaldoInicial[numeroPeriodo + numero_columnas] = saldoInicial;
                    }
                    if (drSubtotal != null)
                    {
                        saldoInicial = saldoInicial + subTotal;
                        drSubtotal[numeroPeriodo + numero_columnas] = saldoInicial;
                    }
                    double totCol = 0;
                    if (drTotalColocacion[numeroPeriodo + 3].ToString() != "")
                    {
                        totCol = Convert.ToDouble(drTotalColocacion[numeroPeriodo + 3]);
                    }
                    if (drColocacion != null)
                    {
                        drColocacion[numeroPeriodo + numero_columnas] = totCol;
                    }
                    double pagosCap = 0;
                    if (drPagosObl != null)
                    {
                        pagosCap = Convert.ToDouble(drPagosObl[numeroPeriodo + numero_columnas]);
                    }
                    double recCap = 0;
                    if (drPagCapColocaciones[numeroPeriodo + 3].ToString() != "")
                    {
                        double porMora1 = 0;
                        if (drPorMor1 != null)
                            if (drPorMor1[numeroPeriodo + 3].ToString() != "")
                                porMora1 = (double)drPorMor1[numeroPeriodo + 3];
                        double porMora2 = 0;
                        if (drPorMor2 != null)
                            if (drPorMor2[numeroPeriodo + 3].ToString() != "")
                                porMora2 = (double)drPorMor2[numeroPeriodo + 3];
                        double porMora3 = 0;
                        if (drPorMor3 != null)
                            if (drPorMor3[numeroPeriodo + 3].ToString() != "")
                                porMora3 = (double)drPorMor3[numeroPeriodo + 3];
                        double porMora4 = 0;
                        if (drPorMor4 != null)
                            if (drPorMor4[numeroPeriodo + 3].ToString() != "")
                                porMora4 = (double)drPorMor4[numeroPeriodo + 3];
                        recCap = Convert.ToDouble(drPagCapColocaciones[numeroPeriodo + 3]) * (1 - (porMora1 / 100) - (porMora2 / 100) - (porMora3 / 100) - (porMora4 / 100));
                    }
                    if (drRecCap != null)
                    {
                        drRecCap[numeroPeriodo + numero_columnas] = recCap;
                    }
                    if (drFondos != null)
                    {
                        drFondos[numeroPeriodo + numero_columnas] = 0;
                    }
                    double adqTec = 0;
                    if (drAdqTec != null)
                    {
                        if (numeroPeriodo == 0)
                            CargarPresupuestoTecnologia(ref dtPresupuesto, dtTecnologia, dtFechas, false, pUsuario,nivel);                      
                        drAdqTec = dtPresupuesto.Rows.Find("15");
                        if (drAdqTec[numeroPeriodo + 3].ToString() != "")
                            adqTec = Convert.ToDouble(drAdqTec[numeroPeriodo + 3]);
                    }
                    double fonReq = 0;
                    if (drFondReq != null)
                    {
                        foreach (DataRow drFila in dtObligacionesNuevas.Rows)
                        {
                            if (drFila[numeroPeriodo + 8].ToString() != "")
                            {
                                double monto = Convert.ToDouble(drFila[numeroPeriodo + 8]);
                                fonReq = fonReq + monto;
                            }
                        }
                        drFondReq[numeroPeriodo + numero_columnas] = fonReq;
                    }
                    if (drFluCaj != null)
                    {
                        drFluCaj[numeroPeriodo + numero_columnas] = saldoInicial - totCol - pagosCap + recCap + fonReq - adqTec;
                        saldoInicial = saldoInicial - totCol - pagosCap + recCap + fonReq - adqTec;
                    }
                    numeroPeriodo += 1;
                }
            }
            catch
            {
            }            

            return dtPresupuesto;
        }

        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            try
            {
                return DAPresupuesto.ObtenerSiguienteCodigo(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "ObtenerSiguienteCodigo", ex);
                return 0;
            }
        }

        public Decimal ConsultarValorPresupuesto(Int64 idPresupuesto, Int64 pnumeroPeriodo, string pcod_cuenta_pre, Int64 pcentro_costo, Usuario pUsuario)
        {
            try
            {
                return DAPresupuesto.ConsultarValorPresupuesto(idPresupuesto, pnumeroPeriodo, pcod_cuenta_pre, pcentro_costo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "ConsultarValorPresupuesto", ex);
                return 0;
            }
        }

        public Decimal ConsultarIncrementoPresupuesto(Int64 idPresupuesto, Int64 pnumeroPeriodo, string pcod_cuenta_pre, Int64 pcentro_costo, Usuario pUsuario)
        {
            try
            {
                return DAPresupuesto.ConsultarIncrementoPresupuesto(idPresupuesto, pnumeroPeriodo, pcod_cuenta_pre, pcentro_costo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "ConsultarIncrementoPresupuesto", ex);
                return 0;
            }
        }

        public Decimal ConsultarValorPresupuestoColocacion(Int64 idPresupuesto, Int64 pnumeroPeriodo, string pitem, Int64 pcentro_costo, Usuario pUsuario)
        {
            try
            {
                return DAPresupuesto.ConsultarValorPresupuestoColocacion(idPresupuesto, pnumeroPeriodo, pitem, pcentro_costo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "ConsultarValorPresupuestoColocacion", ex);
                return 0;
            }
        }

        public Decimal ConsultarValorPresupuestoObligacion(Int64 idPresupuesto, Int64 pnumeroPeriodo, string pitem, Int64 pcentro_costo, Usuario pUsuario)
        {
            try
            {
                return DAPresupuesto.ConsultarValorPresupuestoObligacion(idPresupuesto, pnumeroPeriodo, pitem, pcentro_costo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "ConsultarValorPresupuestoObligacion", ex);
                return 0;
            }
        }

        public Int64 ConsultarNumeroEjecutivos(Int64 idPresupuesto, Int64 pcod_oficina, Usuario pUsuario)
        {
            try
            {
                return DAPresupuesto.ConsultarNumeroEjecutivos(idPresupuesto, pcod_oficina, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "ConsultarNumeroEjecutivos", ex);
                return 0;
            }
        }

        public string ConsultarParametroCuenta(Int64 pcodigo, Usuario pUsuario)
        {
            try
            {
                return DAPresupuesto.ConsultarParametroCuenta(pcodigo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "ConsultarParametroCuenta", ex);
                return "";
            }
        }

        public Boolean EsParametroCuenta(string pcod_cuenta, Usuario pUsuario)
        {
            try
            {
                return DAPresupuesto.EsParametroCuenta(pcod_cuenta, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "EsParametroCuenta", ex);
                return false;
            }
        }

        #region cartera

        public DataTable ListarClasificacion(DateTime pfechahistorico, Usuario pUsuario)
        {
            try
            {
                return DAPresupuesto.ListarClasificacion(pfechahistorico, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "ListarClasificacion", ex);
                return null;
            }
        }

        public DataTable ListarClasificacionOficinas(DateTime pfechahistorico, string filtro, Usuario pUsuario)
        {
            try
            {
                return DAPresupuesto.ListarClasificacionOficinas(pfechahistorico, filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "ListarClasificacionOficinas", ex);
                return null;
            }
        }

        public Decimal ConsultarValorCartera(DateTime fechahistorico, int cod_clasifica, DateTime fecha_inicial, DateTime fecha_final, string filtro, Usuario pUsuario)
        {
            try
            {
                return DAPresupuesto.ConsultarValorCartera(fechahistorico, cod_clasifica, fecha_inicial, fecha_final, filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "ConsultarValorCartera", ex);
                return 0;
            }
        }

        public Decimal ConsultarValorRecuperacion(DateTime fechahistorico, int cod_clasifica, int cod_atr, DateTime fecha_inicial, DateTime fecha_final, string filtro, Usuario pUsuario)
        {
            try
            {
                return DAPresupuesto.ConsultarValorRecuperacion(fechahistorico, cod_clasifica, cod_atr, fecha_inicial, fecha_final, filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "ConsultarValorRecuperacion", ex);
                return 0;
            }
        }

        public Decimal ConsultarValorRecuperacionCausado(DateTime fechahistorico, int cod_clasifica, int cod_atr, DateTime fecha_inicial, DateTime fecha_final, string filtro, Usuario pUsuario)
        {
            try
            {
                return DAPresupuesto.ConsultarValorRecuperacionCausado(fechahistorico, cod_clasifica, cod_atr, fecha_inicial, fecha_final, filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "ConsultarValorRecuperacionCausado", ex);
                return 0;
            }
        }

        public double TasaPromedioColocacion(Usuario pUsuario)
        {
            try
            {
                return DAPresupuesto.TasaPromedioColocacion(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "TasaPromedioColocacion", ex);
                return 0;
            }
        }

        public double PlazoPromedioColocacion(Usuario pUsuario)
        {
            try
            {
                return DAPresupuesto.PlazoPromedioColocacion(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "PlazoPromedioColocacion", ex);
                return 0;
            }
        }

        public int NumeroEjecutivosOficina(int cod_oficina, Usuario pUsuario)
        {
            try
            {
                return DAPresupuesto.NumeroEjecutivosOficina(cod_oficina, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "NumeroEjecutivosOficina", ex);
                return 0;
            }
        }

        public DateTime FechaUltimoHistorico(DateTime pFechaInicial, Usuario vusuario)
        {
            try
            {
                return DAPresupuesto.FechaUltimoHistorico(pFechaInicial, vusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "FechaUltimoHistorico", ex);
                return DateTime.MinValue;
            }
        }

        public void CalcularPresupuestoCartera(ref DataTable dtColocacion, DataTable dtFechas, double VrPorCredito, double saldoActualCartera, double porPolizasVencidas, double valorUnitPoliza, double comisionPoliza, double porLeyMiPyme, double porProvision, double porProvisionGen)
        {
            // Validando que el datatable de fechas tenga datos
            if (dtFechas == null)
                return;

            // Determinando configuracion
            Configuracion conf = new Configuracion();
            string separadorMiles = conf.ObtenerSeparadorMilesConfig();

            // Cargando la fila que contiene las tasas promedio de colocación
            DataRow drTasas;
            drTasas = dtColocacion.Rows.Find("1");
            if (drTasas == null)
                return;

            // Cargando la fila que contiene los plazos promedio de colocación
            DataRow drPlazo;
            drPlazo = dtColocacion.Rows.Find("2");
            if (drPlazo == null)
                return;

            // Cargando la fila de colocación por ejecutivo
            DataRow drColXEje;
            drColXEje = dtColocacion.Rows.Find("3");
            if (dtFechas == null)
                return;

            // Cargando la fila de total colocación
            DataRow drTotCol;
            drTotCol = dtColocacion.Rows.Find("5");
            if (drTotCol == null)
                return;

            // Determinar el número total de ejecutivos
            int NumTotEje = 0;
            if (drTotCol[2].ToString() != "")
                NumTotEje = Convert.ToInt32(drTotCol[2].ToString());

            // Cargando la fila de número de créditos colocados por ejectuvio
            DataRow drNumCre;
            drNumCre = dtColocacion.Rows.Find("6");
            if (drNumCre == null)
                return;

            // Cargando la fila de total créditos colocados
            DataRow drNumTotCre;
            drNumTotCre = dtColocacion.Rows.Find("7");
            if (drNumTotCre == null)
                return;

            // Cargando la fila de recuperaciones o pagos de capital de créditos antiguos
            DataRow drPagosCap;
            drPagosCap = dtColocacion.Rows.Find("8-1");
            if (drPagosCap == null)
                return;

            // Cargando la fila de recuperaciones o pagos de capital de los créditos nuevos
            DataRow drRecuperaciones;
            drRecuperaciones = dtColocacion.Rows.Find("8-2");
            if (drRecuperaciones == null)
                return;

            // Cargando la fila de recuperaciones o pagos de capital de los créditos nuevos
            DataRow drRecuperaTotal;
            drRecuperaTotal = dtColocacion.Rows.Find("8");
            if (drRecuperaTotal == null)
                return;

            // Cargar fila de saldos de cartera
            DataRow drSaldos;
            drSaldos = dtColocacion.Rows.Find("9");
            if (drSaldos == null)
                return;

            // Cargar fila de porcentaje variación de saldos de cartera
            DataRow drVariacionSaldos;
            drVariacionSaldos = dtColocacion.Rows.Find("9-1");
            if (drVariacionSaldos == null)
                return;

            // Cargar porcentajes de cartera en mora
            DataRow drporMora1;
            drporMora1 = dtColocacion.Rows.Find("9-2");
            if (drporMora1 == null)
                return;
            DataRow drporMora2;
            drporMora2 = dtColocacion.Rows.Find("9-3");
            if (drporMora2 == null)
                return;
            DataRow drporMora3;
            drporMora3 = dtColocacion.Rows.Find("9-4");
            if (drporMora3 == null)
                return;
            DataRow drporMora4;
            drporMora4 = dtColocacion.Rows.Find("9-5");
            if (drporMora4 == null)
                return;

            // Cargar fila de ingresos
            DataRow drPagosInt;
            drPagosInt = dtColocacion.Rows.Find("10-1");
            if (drPagosInt == null)
                return;

            // Cargar fila de ingresos
            DataRow drIngresosInt;
            drIngresosInt = dtColocacion.Rows.Find("10-2");
            if (drIngresosInt == null)
                return;

            // Cargar fila de ingresos
            DataRow drIngresos;
            drIngresos = dtColocacion.Rows.Find("11");
            if (drIngresos == null)
                return;

            // Cargar fila de número polizas vencidas
            DataRow drPolizas;
            drPolizas = dtColocacion.Rows.Find("12");
            if (drPolizas == null)
                return;

            // Cargar fila de valor de polizas vencidas
            DataRow drValorPolizas;
            drValorPolizas = dtColocacion.Rows.Find("13");
            if (drValorPolizas == null)
                return;

            // Cargar fila de valor de comisiones
            DataRow drComision;
            drComision = dtColocacion.Rows.Find("14");
            if (drComision == null)
                return;

            // Cargar fila de valor de ley MiPYme créditos viejos
            DataRow drLeyMiPymeV;
            drLeyMiPymeV = dtColocacion.Rows.Find("15-1");
            if (drLeyMiPymeV == null)
                return;

            // Cargar fila de valor de ley MiPYme créditos viejos
            DataRow drLeyMiPymeN;
            drLeyMiPymeN = dtColocacion.Rows.Find("15-2");
            if (drLeyMiPymeN == null)
                return;

            // Cargar fila de valor de comisiones
            DataRow drLeyMiPyme;
            drLeyMiPyme = dtColocacion.Rows.Find("15");
            if (drLeyMiPyme == null)
                return;

            // Cargar fila de valor de provision
            DataRow drProvision;
            drProvision = dtColocacion.Rows.Find("16");
            if (drProvision == null)
                return;

            // Cargar fila de valor de provision general
            DataRow drProvisionGen;
            drProvisionGen = dtColocacion.Rows.Find("17");
            if (drProvisionGen == null)
                return;

            // Inicializando variable de número de columnas
            int numeroPeriodo = 0;

            // Determinar el saldo inicial de cartera
            double saldoAnterior = 0;
            saldoAnterior = saldoActualCartera;

            // Calculando los valores por cada período de fechas
            foreach (DataRow drFecha in dtFechas.Rows)
            {
                double totalColocacion = 0;
                double valorColocacion = 0;
                double TasaIntCte = 0;
                int numeroejecutivos = 0;
                
                // Verificando si se ingreso valor de colocación por ejecutivo en la columna dada
                if (drColXEje[numeroPeriodo + 3].ToString().Trim() != "")
                {
                    // Cargar el valor de colocación por ejecutivo
                    valorColocacion = Convert.ToDouble(drColXEje[numeroPeriodo + 3].ToString().Replace(separadorMiles, ""));
                    // Calcula por cada oficina el valor de colocación según el número de ejecutivos que tenga la oficina
                    foreach (DataRow drFila in dtColocacion.Rows)
                    {
                        if (drFila[0].ToString().Contains(".") && drFila[1].ToString().Contains("-"))
                        {
                            if (drFila[2].ToString() != null)
                            {
                                try
                                {
                                    numeroejecutivos = Convert.ToInt32(drFila[2]);
                                    drFila[numeroPeriodo + 3] = valorColocacion * numeroejecutivos;
                                    totalColocacion = totalColocacion + (valorColocacion * numeroejecutivos);
                                }
                                catch
                                {
                                    numeroejecutivos = 0;
                                }
                            }
                        }
                    }
                }
                
                // Calcular total de colocación para el período dado
                drTotCol[numeroPeriodo + 3] = totalColocacion;
                
                // Calcular el número de créditos colocados por ejecutivo y el número total de créditos
                if (VrPorCredito != 0)
                {
                    drNumCre[numeroPeriodo + 3] = Math.Round(valorColocacion / VrPorCredito);
                    drNumTotCre[numeroPeriodo + 3] = Math.Round(valorColocacion / VrPorCredito) * NumTotEje;
                }          
                
                // Calcular el valor de las recuperaciones o pagos de capital
                double totalPagos = 0;                    
                double saldoColocacion = 0;
                
                // Por cada período anterior ya proyectado calcular el valor que se planea recibir de interes corrientes
                double ultperiodoPago = 0;
                int periodoi = 0;
                foreach (DataRow drFechai in dtFechas.Rows)
                {
                    double valorColocacionPer = 0;
                    double periodoPago = 0;
                    int tiempo = 0;
                    if (drTotCol[periodoi + 3].ToString() != "")
                    {
                        valorColocacionPer = Convert.ToDouble(drTotCol[periodoi + 3].ToString().Replace(separadorMiles, ""));
                        tiempo = Convert.ToInt32(Math.Round(Convert.ToDouble(drPlazo[periodoi + 3])).ToString().Replace(separadorMiles, ""));
                    }
                    if (periodoi < numeroPeriodo && tiempo != 0)
                    {
                        periodoPago = Math.Round(valorColocacionPer / tiempo);
                        totalPagos = totalPagos + Math.Round(valorColocacionPer / tiempo);                        
                        ultperiodoPago = periodoPago;
                    }
                    else
                    {
                        valorColocacionPer = 0;
                    }                    
                    saldoColocacion = saldoColocacion + valorColocacionPer - periodoPago;
                    periodoi += 1;
                }
                drRecuperaciones[numeroPeriodo + 3] = Convert.ToDouble(totalPagos);

                // Calculando el saldo de cartera
                double pagosCapital = 0;
                if (drPagosCap[numeroPeriodo + 3] != null)
                    pagosCapital = Convert.ToDouble(drPagosCap[numeroPeriodo + 3].ToString());
                if (drRecuperaTotal != null)
                    drRecuperaTotal[numeroPeriodo + 3] = totalPagos + pagosCapital;
                double colocacion = 0;
                if (drTotCol[numeroPeriodo + 3] != null && drTotCol[numeroPeriodo + 3].ToString() != "")
                    colocacion = Convert.ToDouble(drTotCol[numeroPeriodo + 3].ToString());
                drSaldos[numeroPeriodo + 3] = saldoAnterior - (pagosCapital + totalPagos) + colocacion;
                if (saldoAnterior != 0)
                    drVariacionSaldos[numeroPeriodo + 3] =  Math.Round((Convert.ToDouble(drSaldos[numeroPeriodo + 3]) - saldoAnterior) / saldoAnterior * 100, 2);
                else
                    drVariacionSaldos[numeroPeriodo + 3] = 0;
                if (drSaldos[numeroPeriodo + 3] != null)
                    saldoAnterior = Convert.ToDouble(drSaldos[numeroPeriodo + 3].ToString());                
                
                // Calcular ingresos de cartera
                if (drTasas[numeroPeriodo + 3] != null)
                    TasaIntCte = Convert.ToDouble(drTasas[numeroPeriodo + 3].ToString());
                drIngresosInt[numeroPeriodo + 3] = Math.Round((saldoColocacion + ultperiodoPago)* ((TasaIntCte / 100) / 12));
                drIngresos[numeroPeriodo + 3] = (double)drPagosInt[numeroPeriodo + 3] + (double)drIngresosInt[numeroPeriodo + 3];                
                
                // Calculando número de polizas vencidas y valor de polizas
                Int64 numTotCreditos = 0;
                if (drNumTotCre[numeroPeriodo + 3].ToString() != "")                
                    numTotCreditos = Convert.ToInt64(drNumTotCre[numeroPeriodo + 3].ToString());
                drPolizas[numeroPeriodo + 3] = Math.Round(porPolizasVencidas * numTotCreditos);
                drValorPolizas[numeroPeriodo + 3] = Math.Round((double)drPolizas[numeroPeriodo + 3] * valorUnitPoliza);
                drComision[numeroPeriodo + 3] = Math.Round((double)drValorPolizas[numeroPeriodo + 3] * comisionPoliza);

                // Calculando valor de Ley MiPyme
                double totalLeyMiPYme = 0;
                periodoi = 0;
                foreach (DataRow drFechai in dtFechas.Rows)
                {
                    double valorColocacionPer = 0;
                    double ValorLeyMiPYme = 0;
                    if (drTotCol[periodoi + 3].ToString() != "")
                    {
                        if (periodoi < numeroPeriodo)
                        {
                            if (numeroPeriodo - periodoi <= 3)
                            {
                                valorColocacionPer = Convert.ToDouble(drTotCol[periodoi + 3].ToString().Replace(separadorMiles, ""));
                                ValorLeyMiPYme = Math.Round(valorColocacionPer * porLeyMiPyme / 3);
                                totalLeyMiPYme = totalLeyMiPYme + ValorLeyMiPYme;
                            }
                        }
                    }
                    periodoi += 1;
                }
                double LeyMiPymeViejos = 0;
                if (drLeyMiPymeV[numeroPeriodo + 3].ToString() != "")
                    LeyMiPymeViejos = (double)drLeyMiPymeV[numeroPeriodo + 3];
                drLeyMiPymeN[numeroPeriodo + 3] = totalLeyMiPYme;
                drLeyMiPyme[numeroPeriodo + 3] = totalLeyMiPYme + LeyMiPymeViejos;

                // Calcular la provisión de cartera
                double porcentajeMora = 0;
                if (drporMora1[numeroPeriodo + 3].ToString() != "")
                    porcentajeMora = porcentajeMora + (double)drporMora1[numeroPeriodo + 3];
                if (drporMora2[numeroPeriodo + 3].ToString() != "")
                    porcentajeMora = porcentajeMora + (double)drporMora2[numeroPeriodo + 3];
                if (drporMora3[numeroPeriodo + 3].ToString() != "")
                    porcentajeMora = porcentajeMora + (double)drporMora3[numeroPeriodo + 3];
                if (drporMora4[numeroPeriodo + 3].ToString() != "")
                    porcentajeMora = porcentajeMora + (double)drporMora4[numeroPeriodo + 3];
                drProvision[numeroPeriodo + 3] = Math.Round((double)drSaldos[numeroPeriodo + 3] * porProvision * (porcentajeMora/100));
                if (drSaldos[numeroPeriodo + 3].ToString() != "")
                    drProvisionGen[numeroPeriodo + 3] = Math.Round((double)drSaldos[numeroPeriodo + 3] * porProvisionGen);

                // Ir al siguiente periodo
                numeroPeriodo = numeroPeriodo + 1;
            }

            // Calculando variación total de cartera
            if (saldoAnterior != 0)
                drVariacionSaldos[2] = Math.Round((saldoAnterior - saldoActualCartera) / saldoActualCartera * 100, 2);
            else
                drVariacionSaldos[2] = 0;
        }

        public void CargarPresupuestoCartera(ref DataTable dtPresupuesto, DataTable dtColocacion, DataTable dtFechas, Boolean bMayorizar, Usuario pUsuario, Int16 nivel)
        {
            try
            {
                // Cargar porcentaje de cartera
                DataRow drPorMora;
                drPorMora = dtColocacion.Rows.Find("9-5");
                if (drPorMora == null)
                    return;

                // Cargar fila de ingresos de microcrédito de la tabla de colocacion
                DataRow drIngresos;
                drIngresos = dtColocacion.Rows.Find("11");
                if (drIngresos == null)
                    return;
                DataRow drIngresosNuevos;
                drIngresosNuevos = dtColocacion.Rows.Find("10-2");
                DataRow drCausado;
                drCausado = dtColocacion.Rows.Find("10-3");

                // Cargar fila de comisiones de la tabla de colocacion
                DataRow drComisiones;
                drComisiones = dtColocacion.Rows.Find("14");
                if (drComisiones == null)
                    return;

                // Cargar fila de Ley MiPYme de la tabla de colocacion
                DataRow drLeyMiPyme;
                drLeyMiPyme = dtColocacion.Rows.Find("15");
                if (drLeyMiPyme == null)
                    return;

                // Cargar fila de Provisión de la tabla de colocacion
                DataRow drProvision;
                drProvision = dtColocacion.Rows.Find("16");
                if (drProvision == null)
                    return;

                // Cargar fila de Provisión General de la tabla de colocacion
                DataRow drProvisionGen;
                drProvisionGen = dtColocacion.Rows.Find("17");
                if (drProvisionGen == null)
                    return;

                // Inicializando variable de número de columnas
                int numeroPeriodo = 0;                                                

                foreach (DataRow drFecha in dtFechas.Rows)
                {

                    // Cargar fila de ingresos de microcrédito del PRESUPUESTO
                    double valorIngresos = 0;
                    double valorIngresosNuevos = 0;
                    double valorCausado = 0;
                    if (drIngresosNuevos[numeroPeriodo + 3].ToString() != "")
                        valorIngresosNuevos = (double)drIngresosNuevos[numeroPeriodo + 3];
                    if (drCausado[numeroPeriodo + 3].ToString() != "")
                        valorCausado = (double)drCausado[numeroPeriodo + 3];
                    valorIngresos = valorIngresosNuevos + valorCausado;
                    double porMora = 0;              
                    if (drPorMora[numeroPeriodo + 3].ToString() != "")
                        porMora = (double)drPorMora[numeroPeriodo + 3];
                    string cod_cuenta_ingreso = DAPresupuesto.ConsultarParametroCuenta(1, pUsuario);
                    DataRow drINGRESOSMICROCREDITO;
                    drINGRESOSMICROCREDITO = dtPresupuesto.Rows.Find(cod_cuenta_ingreso);
                    valorIngresos = valorIngresos - (valorIngresos * porMora/100);
                    if (drINGRESOSMICROCREDITO != null)
                        drINGRESOSMICROCREDITO[numeroPeriodo + numero_columnas] = valorIngresos;
                    if (drINGRESOSMICROCREDITO != null && numeroPeriodo == 0)
                    {
                        double saldoPromedio = 0;
                        if (drINGRESOSMICROCREDITO[4] != null)
                            saldoPromedio = Convert.ToDouble(drINGRESOSMICROCREDITO[4]);
                        if (saldoPromedio != 0)
                            drINGRESOSMICROCREDITO[6] = Math.Round(((valorIngresos - saldoPromedio) / saldoPromedio) * 100, 2);
                    }

                    // Cargar fila de ingresos de microcrédito del PRESUPUESTO
                    double valorComisiones = 0;
                    if (drComisiones[numeroPeriodo + 3].ToString() != "")
                        valorComisiones = (double)drComisiones[numeroPeriodo + 3];
                    string cod_cuenta_comision = DAPresupuesto.ConsultarParametroCuenta(2, pUsuario); // "418010";
                    if (cod_cuenta_comision != "")
                    {
                        DataRow drCOMISIONES;
                        drCOMISIONES = dtPresupuesto.Rows.Find(cod_cuenta_comision);
                        if (drCOMISIONES != null)
                            drCOMISIONES[numeroPeriodo + numero_columnas] = valorComisiones;
                        if (drCOMISIONES != null && numeroPeriodo == 0)
                        {
                            double saldoPromedio = 0;
                            if (drCOMISIONES[4] != null)
                                saldoPromedio = Convert.ToDouble(drCOMISIONES[4]);
                            if (saldoPromedio != 0)
                                drCOMISIONES[6] = Math.Round(((valorComisiones - saldoPromedio) / saldoPromedio) * 100, 2);
                        }
                    }

                    // Cargar fila de ingresos de microcrédito del PRESUPUESTO
                    double valorLeyMiPyme = 0;
                    if (drLeyMiPyme[numeroPeriodo + 3].ToString() != "")
                        valorLeyMiPyme = (double)drLeyMiPyme[numeroPeriodo + 3];
                    string cod_cuenta_leymipyme = DAPresupuesto.ConsultarParametroCuenta(3, pUsuario); // "418560";
                    if (cod_cuenta_leymipyme != "")
                    {
                        DataRow drLEYMIPYME;
                        drLEYMIPYME = dtPresupuesto.Rows.Find(cod_cuenta_leymipyme);
                        if (drLEYMIPYME != null)
                            drLEYMIPYME[numeroPeriodo + numero_columnas] = valorLeyMiPyme;
                        if (drLEYMIPYME != null && numeroPeriodo == 0)
                        {
                            double saldoPromedio = 0;
                            if (drLEYMIPYME[4] != null)
                                saldoPromedio = Convert.ToDouble(drLEYMIPYME[4]);
                            if (saldoPromedio != 0)
                                drLEYMIPYME[6] = Math.Round(((valorLeyMiPyme - saldoPromedio) / saldoPromedio) * 100, 2);
                        }
                    }

                    // Cargar fila de gastos de provisión del PRESUPUESTO
                    double valorProvision = 0;
                    if (drProvision[numeroPeriodo + 3].ToString() != "")
                        valorProvision = (double)drProvision[numeroPeriodo + 3];
                    string cod_cuenta_provision = DAPresupuesto.ConsultarParametroCuenta(4, pUsuario); // "511523";
                    if (cod_cuenta_provision != "")
                    {
                        DataRow drPROVISION;
                        drPROVISION = dtPresupuesto.Rows.Find(cod_cuenta_provision);
                        if (drPROVISION != null)
                            drPROVISION[numeroPeriodo + numero_columnas] = valorProvision;
                        if (drPROVISION != null && numeroPeriodo == 0)
                        {
                            double saldoPromedio = 0;
                            if (drPROVISION[4] != null)
                                saldoPromedio = Convert.ToDouble(drPROVISION[4]);
                            if (saldoPromedio != 0)
                                drPROVISION[6] = Math.Round(((valorProvision - saldoPromedio) / saldoPromedio) * 100, 2);
                        }
                    }

                    // Cargar fila de gastos de provisión general del PRESUPUESTO
                    double valorProvisionGen = 0;
                    if (drProvisionGen[numeroPeriodo + 3].ToString() != "")
                        valorProvisionGen = (double)drProvisionGen[numeroPeriodo + 3];
                    string cod_cuenta_provisiongen = DAPresupuesto.ConsultarParametroCuenta(5, pUsuario); // "511524";
                    if (cod_cuenta_provision != "")
                    {
                        DataRow drPROVISIONGEN;
                        drPROVISIONGEN = dtPresupuesto.Rows.Find(cod_cuenta_provisiongen);
                        if (drPROVISIONGEN != null)
                            drPROVISIONGEN[numeroPeriodo + numero_columnas] = valorProvisionGen;
                        if (drPROVISIONGEN != null && numeroPeriodo == 0)
                        {
                            double saldoPromedio = 0;
                            if (drPROVISIONGEN[4] != null)
                                saldoPromedio = Convert.ToDouble(drPROVISIONGEN[4]);
                            if (saldoPromedio != 0)
                                drPROVISIONGEN[6] = Math.Round(((valorProvisionGen - saldoPromedio) / saldoPromedio) * 100, 2);
                        }
                    }
                    
                    // Ir al siguiente período
                    numeroPeriodo = numeroPeriodo + 1;

                };

                if (bMayorizar == true)
                    MayorizarPresupuesto(ref dtPresupuesto, dtFechas, pUsuario,  nivel);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "CargarPresupuestoCartera", ex);
                return;
            }
        }

        #endregion cartera

        #region nomina

        public Xpinn.Presupuesto.Entities.Nomina CrearEmpleado(Xpinn.Presupuesto.Entities.Nomina pNomina, Usuario vusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pNomina = DAPresupuesto.CrearEmpleado(pNomina, vusuario);

                    ts.Complete();
                }

                return pNomina;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "CrearEmpleado", ex);
                return null;
            }
        }

        public Xpinn.Presupuesto.Entities.Nomina ModificarEmpleado(Xpinn.Presupuesto.Entities.Nomina pNomina, Usuario vusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pNomina = DAPresupuesto.ModificarEmpleado(pNomina, vusuario);

                    ts.Complete();
                }

                return pNomina;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "ModificarEmpleado", ex);
                return null;
            }
        }

        public Xpinn.Presupuesto.Entities.Nomina ActualizarEmpleado(Xpinn.Presupuesto.Entities.Nomina pNomina, Usuario vusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pNomina = DAPresupuesto.ActualizarEmpleado(pNomina, vusuario);

                    ts.Complete();
                }

                return pNomina;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "ActualizarEmpleado", ex);
                return null;
            }
        }

        public void EliminarEmpleado(Xpinn.Presupuesto.Entities.Nomina pNomina, Usuario vusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAPresupuesto.EliminarEmpleado(pNomina, vusuario);

                    ts.Complete();
                }

                return;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "EliminarEmpleado", ex);
                return;
            }
        }

        public DataTable ListarNomina(DateTime pfechahistorico, ref string Error, Usuario pUsuario, string filtro)
        {
            try
            {
                return DAPresupuesto.ListarNomina(pfechahistorico, ref Error, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "ListarNomina", ex);
                return null;
            }
        }

        public DataTable ListarTotalesNomina(DateTime pfechahistorico, ref string Error, Usuario pUsuario, string filtro)
        {
            try
            {
                return DAPresupuesto.ListarTotalesNomina(pfechahistorico, ref Error, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "ListarTotalesNomina", ex);
                return null;
            }
        }

        public void CargarPresupuestoNomina(ref DataTable dtPresupuesto, DataTable dtNomina, DataTable dtFechas, Boolean bMayorizar, Usuario pUsuario,Int16 nivel)
        {
            try
            {

                   // Inicializando variable de número de columnas
                int numeroPeriodo = 0;

                foreach (DataRow drFecha in dtFechas.Rows)
                {
                    string cod_cuenta = "";
                    DateTime fecha_inicial = Convert.ToDateTime(drFecha[1].ToString());
                    DateTime fecha_final = Convert.ToDateTime(drFecha[2].ToString());

                    // Calculando el valor del salario              
                    double valorSalario = 0;
                    double valorSalarioIntegral = 0;
                    double valorAuxTrans = 0;
                    double valorComisiones = 0;
                    double valoraux_tel = 0;
                    double valoraux_gas = 0;
                    double valorcesantias = 0;
                    double valorint_ces = 0;
                    double valorprima = 0;
                    double valorvacaciones = 0;
                    double valordotacion = 0;
                    double valorsalud = 0;
                    double valorpension = 0;
                    double valorarp = 0;
                    double valorcaja_comp = 0;

                    foreach (DataRow drFila in dtNomina.Rows)
                    {
                        int tipo_salario = 0;
                        try
                        {
                            tipo_salario = Convert.ToInt32(drFila["TIPO_SALARIO"].ToString());
                        }
                        catch
                        {
                            tipo_salario = 1;
                        }
                        DateTime fecha_ingreso = DateTime.MinValue;
                        if (drFila["FECHA_INGRESO"].ToString() != "")
                            fecha_ingreso = Convert.ToDateTime(drFila["FECHA_INGRESO"].ToString());
                        else
                            fecha_ingreso = fecha_inicial;
                        if (fecha_ingreso <= fecha_final)
                        {
                            if (drFila["SALARIO_NUEVO"].ToString() != "")
                            {
                                double valor = Convert.ToDouble(drFila["SALARIO_NUEVO"]);
                                if (tipo_salario == 2)
                                    valorSalarioIntegral = valorSalarioIntegral + valor;
                                else
                                    valorSalario = valorSalario + valor;
                            }
                            // Determinar el valor del auxilio de transporte del presupuesto de NOMINA
                            var sumaAuxT = drFila["aux_trans"];
                            if (sumaAuxT != System.DBNull.Value)
                                valorAuxTrans = valorAuxTrans + Convert.ToDouble(sumaAuxT.ToString().Trim());

                            // Determinar el valor del Comisiones del presupuesto de NOMINA                            
                            var sumaComisiones = drFila["Comisiones"];
                            if (sumaComisiones != System.DBNull.Value)
                                valorComisiones = valorComisiones + Convert.ToDouble(sumaComisiones.ToString().Trim());

                            // Determinar el valor del aux_tel del presupuesto de NOMINA                            
                            var sumaaux_tel = drFila["aux_tel"];
                            if (sumaaux_tel != System.DBNull.Value)
                                valoraux_tel = valoraux_tel + Convert.ToDouble(sumaaux_tel.ToString().Trim());

                            // Determinar el valor del aux_gas del presupuesto de NOMINA                            
                            var sumaaux_gas = drFila["aux_gas"];
                            if (sumaaux_gas != System.DBNull.Value)
                                valoraux_gas = valoraux_gas + Convert.ToDouble(sumaaux_gas.ToString().Trim());

                            // Determinar el valor del cesantias del presupuesto de NOMINA                            
                            var sumacesantias = drFila["cesantias"];
                            if (sumacesantias != System.DBNull.Value)
                                valorcesantias = valorcesantias + Convert.ToDouble(sumacesantias.ToString().Trim());

                            // Determinar el valor del int_ces del presupuesto de NOMINA                            
                            var sumaint_ces = drFila["int_ces"];
                            if (sumaint_ces != System.DBNull.Value)
                                valorint_ces = valorint_ces + Convert.ToDouble(sumaint_ces.ToString().Trim());

                            // Determinar el valor del prima del presupuesto de NOMINA                            
                            var sumaprima = drFila["prima"];
                            if (sumaprima != System.DBNull.Value)
                                valorprima = valorprima + Convert.ToDouble(sumaprima.ToString().Trim());

                            // Determinar el valor del vacaciones del presupuesto de NOMINA                            
                            var sumavacaciones = drFila["vacaciones"];
                            if (sumavacaciones != System.DBNull.Value)
                                valorvacaciones = valorvacaciones + Convert.ToDouble(sumavacaciones.ToString().Trim());

                            // Determinar el valor del dotacion del presupuesto de NOMINA                            
                            var sumadotacion = drFila["dotacion"];
                            if (sumadotacion != System.DBNull.Value)
                                valordotacion = valordotacion + Convert.ToDouble(sumadotacion.ToString().Trim());

                            // Determinar el valor del salud del presupuesto de NOMINA                            
                            var sumasalud = drFila["salud"];
                            if (sumasalud != System.DBNull.Value)
                                valorsalud = valorsalud + Convert.ToDouble(sumasalud.ToString().Trim());

                            // Determinar el valor del pension del presupuesto de NOMINA                            
                            var sumapension = drFila["pension"];
                            if (sumapension != System.DBNull.Value)
                                valorpension = valorpension + Convert.ToDouble(sumapension.ToString().Trim());

                            // Determinar el valor del arp del presupuesto de NOMINA                            
                            var sumaarp = drFila["arp"];
                            if (sumaarp != System.DBNull.Value)
                                valorarp = valorarp + Convert.ToDouble(sumaarp.ToString().Trim());

                            // Determinar el valor del caja_comp del presupuesto de NOMINA                            
                            var sumacaja_comp = drFila["caja_comp"];
                            if (sumacaja_comp != System.DBNull.Value)
                                valorcaja_comp = valorcaja_comp + Convert.ToDouble(sumacaja_comp.ToString().Trim());

                        }
                    }

                    // Cargar fila de salarios del PRESUPUESTO 
                    cod_cuenta = DAPresupuesto.ConsultarParametroCuenta(6, pUsuario); // "510503";
                    if (cod_cuenta != "")
                    {
                        DataRow drSALARIOINTEGRAL;
                        drSALARIOINTEGRAL = dtPresupuesto.Rows.Find(cod_cuenta);
                        if (drSALARIOINTEGRAL != null)
                            drSALARIOINTEGRAL[numeroPeriodo + numero_columnas] = valorSalarioIntegral;
                        if (drSALARIOINTEGRAL != null && numeroPeriodo == 0)
                        {
                            double saldoPromedio = 0;
                            if (drSALARIOINTEGRAL[4] != null)
                                saldoPromedio = Convert.ToDouble(drSALARIOINTEGRAL[4]);
                            if (saldoPromedio != 0)
                                drSALARIOINTEGRAL[6] = Math.Round(((valorSalarioIntegral - saldoPromedio) / saldoPromedio) * 100, 2);
                        }
                    }

                    // Cargar fila de salarios del PRESUPUESTO
                    cod_cuenta = "510506";
                    // cod_cuenta = DAPresupuesto.ConsultarParametroCuenta(29, pUsuario); // "510506";
                    if (cod_cuenta != "")
                    {
                        DataRow drSALARIO;
                        drSALARIO = dtPresupuesto.Rows.Find(cod_cuenta);
                        if (drSALARIO != null)
                            drSALARIO[numeroPeriodo + numero_columnas] = valorSalario;
                        if (drSALARIO != null && numeroPeriodo == 0)
                        {
                            double saldoPromedio = 0;
                            if (drSALARIO[4] != null)
                                saldoPromedio = Convert.ToDouble(drSALARIO[4]);
                            if (saldoPromedio != 0)
                                drSALARIO[6] = Math.Round(((valorSalario - saldoPromedio) / saldoPromedio) * 100, 2);
                        }
                    }

                    // Cargar fila de comisiones del PRESUPUESTO
                    cod_cuenta = DAPresupuesto.ConsultarParametroCuenta(7, pUsuario); // "510518";
                    if (cod_cuenta != "")
                    {
                        DataRow drCOMISIONES;
                        drCOMISIONES = dtPresupuesto.Rows.Find(cod_cuenta);
                        if (drCOMISIONES != null)
                            drCOMISIONES[numeroPeriodo + numero_columnas] = valorComisiones;
                        if (drCOMISIONES != null && numeroPeriodo == 0)
                        {
                            double saldoPromedio = 0;
                            if (drCOMISIONES[4] != null)
                                saldoPromedio = Convert.ToDouble(drCOMISIONES[4]);
                            if (saldoPromedio != 0)
                                drCOMISIONES[6] = Math.Round(((valorComisiones - saldoPromedio) / saldoPromedio) * 100, 2);
                        }
                    }

                    // Cargar fila de auxilio transporte del PRESUPUESTO
                    cod_cuenta = DAPresupuesto.ConsultarParametroCuenta(8, pUsuario); // "510527";
                    if (cod_cuenta != "")
                    {
                        DataRow drAUXTRANS;
                        drAUXTRANS = dtPresupuesto.Rows.Find(cod_cuenta);
                        if (drAUXTRANS != null)
                            drAUXTRANS[numeroPeriodo + numero_columnas] = valorAuxTrans;
                        if (drAUXTRANS != null && numeroPeriodo == 0)
                        {
                            double saldoPromedio = 0;
                            if (drAUXTRANS[4] != null)
                                saldoPromedio = Convert.ToDouble(drAUXTRANS[4]);
                            if (saldoPromedio != 0)
                                drAUXTRANS[6] = Math.Round(((valorAuxTrans - saldoPromedio) / saldoPromedio) * 100, 2);
                        }
                    }

                    // Cargar fila de auxilio telefono y gasolina del PRESUPUESTO
                    cod_cuenta = DAPresupuesto.ConsultarParametroCuenta(9, pUsuario); // "510528";
                    if (cod_cuenta != "")
                    {
                        DataRow drAUXTEL;
                        drAUXTEL = dtPresupuesto.Rows.Find(cod_cuenta);
                        if (drAUXTEL != null)
                            drAUXTEL[numeroPeriodo + numero_columnas] = valoraux_tel + valoraux_gas;
                        if (drAUXTEL != null && numeroPeriodo == 0)
                        {
                            double saldoPromedio = 0;
                            if (drAUXTEL[4] != null)
                                saldoPromedio = Convert.ToDouble(drAUXTEL[4]);
                            if (saldoPromedio != 0)
                                drAUXTEL[6] = Math.Round((((valoraux_tel + valoraux_gas) - saldoPromedio) / saldoPromedio) * 100, 2);
                        }
                    }

                    // Cargar fila de cesantias del PRESUPUESTO
                    cod_cuenta = DAPresupuesto.ConsultarParametroCuenta(10, pUsuario); // "510530";
                    if (cod_cuenta != "")
                    {
                        DataRow drCESANTIAS;
                        drCESANTIAS = dtPresupuesto.Rows.Find(cod_cuenta);
                        if (drCESANTIAS != null)
                            drCESANTIAS[numeroPeriodo + numero_columnas] = valorcesantias;
                        if (drCESANTIAS != null && numeroPeriodo == 0)
                        {
                            double saldoPromedio = 0;
                            if (drCESANTIAS[4] != null)
                                saldoPromedio = Convert.ToDouble(drCESANTIAS[4]);
                            if (saldoPromedio != 0)
                                drCESANTIAS[6] = Math.Round(((valorcesantias - saldoPromedio) / saldoPromedio) * 100, 2);
                        }
                    }

                    // Cargar fila de intereses cesantias del PRESUPUESTO
                    cod_cuenta = DAPresupuesto.ConsultarParametroCuenta(11, pUsuario); // "510533";
                    if (cod_cuenta != "")
                    {
                        DataRow drINTCESANTIAS;
                        drINTCESANTIAS = dtPresupuesto.Rows.Find(cod_cuenta);
                        if (drINTCESANTIAS != null)
                            drINTCESANTIAS[numeroPeriodo + numero_columnas] = valorint_ces;
                        if (drINTCESANTIAS != null && numeroPeriodo == 0)
                        {
                            double saldoPromedio = 0;
                            if (drINTCESANTIAS[4] != null)
                                saldoPromedio = Convert.ToDouble(drINTCESANTIAS[4]);
                            if (saldoPromedio != 0)
                                drINTCESANTIAS[6] = Math.Round(((valorint_ces - saldoPromedio) / saldoPromedio) * 100, 2);
                        }
                    }

                    // Cargar fila de prima del PRESUPUESTO
                    cod_cuenta = DAPresupuesto.ConsultarParametroCuenta(12, pUsuario); // "510536";
                    if (cod_cuenta != "")
                    {
                        DataRow drPRIMA;
                        drPRIMA = dtPresupuesto.Rows.Find(cod_cuenta);
                        if (drPRIMA != null)
                            drPRIMA[numeroPeriodo + numero_columnas] = valorprima;
                        if (drPRIMA != null && numeroPeriodo == 0)
                        {
                            double saldoPromedio = 0;
                            if (drPRIMA[4] != null)
                                saldoPromedio = Convert.ToDouble(drPRIMA[4]);
                            if (saldoPromedio != 0)
                                drPRIMA[6] = Math.Round(((valorprima - saldoPromedio) / saldoPromedio) * 100, 2);
                        }
                    }

                    // Cargar fila de vacaciones del PRESUPUESTO
                    cod_cuenta = DAPresupuesto.ConsultarParametroCuenta(13, pUsuario); // "510539";
                    if (cod_cuenta != "")
                    {
                        DataRow drVACACIONES;
                        drVACACIONES = dtPresupuesto.Rows.Find(cod_cuenta);
                        if (drVACACIONES != null)
                            drVACACIONES[numeroPeriodo + numero_columnas] = valorvacaciones;
                        if (drVACACIONES != null && numeroPeriodo == 0)
                        {
                            double saldoPromedio = 0;
                            if (drVACACIONES[4] != null)
                                saldoPromedio = Convert.ToDouble(drVACACIONES[4]);
                            if (saldoPromedio != 0)
                                drVACACIONES[6] = Math.Round(((valorvacaciones - saldoPromedio) / saldoPromedio) * 100, 2);
                        }
                    }

                    // Cargar fila de dotación del PRESUPUESTO
                    cod_cuenta = DAPresupuesto.ConsultarParametroCuenta(14, pUsuario); // "510551";
                    if (cod_cuenta != "")
                    {
                        DataRow drDOTACION;
                        drDOTACION = dtPresupuesto.Rows.Find(cod_cuenta);
                        if (drDOTACION != null)
                            drDOTACION[numeroPeriodo + numero_columnas] = valordotacion;
                        if (drDOTACION != null && numeroPeriodo == 0)
                        {
                            double saldoPromedio = 0;
                            if (drDOTACION[4] != null)
                                saldoPromedio = Convert.ToDouble(drDOTACION[4]);
                            if (saldoPromedio != 0)
                                drDOTACION[6] = Math.Round(((valordotacion - saldoPromedio) / saldoPromedio) * 100, 2);
                        }
                    }

                    // Cargar fila de salud del PRESUPUESTO
                    cod_cuenta = DAPresupuesto.ConsultarParametroCuenta(15, pUsuario); // "510569";
                    if (cod_cuenta != "")
                    {
                        DataRow drSALUD;
                        drSALUD = dtPresupuesto.Rows.Find(cod_cuenta);
                        if (drSALUD != null)
                            drSALUD[numeroPeriodo + numero_columnas] = valorsalud;
                        if (drSALUD != null && numeroPeriodo == 0)
                        {
                            double saldoPromedio = 0;
                            if (drSALUD[4] != null)
                                saldoPromedio = Convert.ToDouble(drSALUD[4]);
                            if (saldoPromedio != 0)
                                drSALUD[6] = Math.Round(((valorsalud - saldoPromedio) / saldoPromedio) * 100, 2);
                        }
                    }

                    // Cargar fila de pension del PRESUPUESTO
                    cod_cuenta = DAPresupuesto.ConsultarParametroCuenta(16, pUsuario); // "510570";
                    if (cod_cuenta != "")
                    {
                        DataRow drPENSION;
                        drPENSION = dtPresupuesto.Rows.Find(cod_cuenta);
                        if (drPENSION != null)
                            drPENSION[numeroPeriodo + numero_columnas] = valorpension;
                        if (drPENSION != null && numeroPeriodo == 0)
                        {
                            double saldoPromedio = 0;
                            if (drPENSION[4] != null)
                                saldoPromedio = Convert.ToDouble(drPENSION[4]);
                            if (saldoPromedio != 0)
                                drPENSION[6] = Math.Round(((valorpension - saldoPromedio) / saldoPromedio) * 100, 2);
                        }
                    }

                    // Cargar fila de arp del PRESUPUESTO
                    cod_cuenta = DAPresupuesto.ConsultarParametroCuenta(17, pUsuario); // "510571";
                    if (cod_cuenta != "")
                    {
                        DataRow drARP;
                        drARP = dtPresupuesto.Rows.Find(cod_cuenta);
                        if (drARP != null)
                            drARP[numeroPeriodo + numero_columnas] = valorarp;
                        if (drARP != null && numeroPeriodo == 0)
                        {
                            double saldoPromedio = 0;
                            if (drARP[4] != null)
                                saldoPromedio = Convert.ToDouble(drARP[4]);
                            if (saldoPromedio != 0)
                                drARP[6] = Math.Round(((valorarp - saldoPromedio) / saldoPromedio) * 100, 2);
                        }
                    }

                    // Cargar fila de caja de compensación del PRESUPUESTO
                    cod_cuenta = DAPresupuesto.ConsultarParametroCuenta(18, pUsuario); // "510572";
                    if (cod_cuenta != "")
                    {
                        DataRow drCAJACOMP;
                        drCAJACOMP = dtPresupuesto.Rows.Find(cod_cuenta);
                        if (drCAJACOMP != null)
                            drCAJACOMP[numeroPeriodo + numero_columnas] = valorcaja_comp;
                        if (drCAJACOMP != null && numeroPeriodo == 0)
                        {
                            double saldoPromedio = 0;
                            if (drCAJACOMP[4] != null)
                                saldoPromedio = Convert.ToDouble(drCAJACOMP[4]);
                            if (saldoPromedio != 0)
                                drCAJACOMP[6] = Math.Round(((valorcaja_comp - saldoPromedio) / saldoPromedio) * 100, 2);
                        }
                    }

                    // Ir al siguiente período
                    numeroPeriodo = numeroPeriodo + 1;

                };

                if (bMayorizar == true)
                    MayorizarPresupuesto(ref dtPresupuesto, dtFechas, pUsuario,nivel);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "CargarPresupuestoCartera", ex);
                return;
            }
        }

        public DataTable ListarCargos(Usuario pUsuario)
        {
            try
            {
                return DAPresupuesto.ListarCargos(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "ListarCargos", ex);
                return null;
            }
        }

        public Xpinn.Presupuesto.Entities.Cargos CrearCargo(Xpinn.Presupuesto.Entities.Cargos pCargos, Usuario vusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pCargos = DAPresupuesto.CrearCargo(pCargos, vusuario);

                    ts.Complete();
                }

                return pCargos;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "CrearCargo", ex);
                return null;
            }
        }

        public Xpinn.Presupuesto.Entities.Cargos ModificarCargo(Xpinn.Presupuesto.Entities.Cargos pCargos, Usuario vusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pCargos = DAPresupuesto.ModificarCargo(pCargos, vusuario);

                    ts.Complete();
                }

                return pCargos;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "ModificarCargo", ex);
                return null;
            }
        }

        public Xpinn.Presupuesto.Entities.Cargos ConsultarCargo(Int64 pId, Usuario vusuario)
        {
            try
            {
                Xpinn.Presupuesto.Entities.Cargos lCargos = new Xpinn.Presupuesto.Entities.Cargos();

                lCargos = DAPresupuesto.ConsultarCargo(pId, vusuario);

                return lCargos;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "ConsultarCargo", ex);
                return null;
            }
        }

        #endregion nomina

        #region activosfijos

        public DataTable ListarActivosFijos(DateTime pfechahistorico, ref string Error, Usuario pUsuario)
        {
            try
            {
                return DAPresupuesto.ListarActivosFijos(pfechahistorico, ref Error, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "ListarActivosFijos", ex);
                return null;
            }
        }

        public void CargarPresupuestoActivosFijos(ref DataTable dtPresupuesto, DataTable dtActivosFij, DataTable dtFechas, Boolean bMayorizar, Usuario pUsuario,Int16 nivel)
        {
            try
            {
                // Inicializando variable de número de columnas
                int numeroPeriodo = 0;

                foreach (DataRow drFecha in dtFechas.Rows)
                {
                    // Calcular depreciación de activos fijos si cumple fecha de compra
                    double valorVrCompra = 0;
                    double valorDepre = 0;
                    double valorDepreMuebles = 0;
                    double valorDepreEquipos = 0;
                    double valorDepreVehiculos = 0;
                    DateTime fecha_inicial = Convert.ToDateTime(drFecha[1].ToString());
                    DateTime fecha_final = Convert.ToDateTime(drFecha[2].ToString());
                    foreach (DataRow drActFij in dtActivosFij.Rows)
                    {
                        int tipo_activo = 0;
                        if (drActFij["tipo_activo"].ToString() != "")
                            tipo_activo = Convert.ToInt32(drActFij["tipo_activo"].ToString());
                        else
                            tipo_activo = 1;
                        DateTime fecha_compra = DateTime.MinValue;
                        if (drActFij["fecha_compra"].ToString() != "")
                            fecha_compra = Convert.ToDateTime(drActFij["fecha_compra"].ToString());
                        else
                            fecha_compra = fecha_inicial;
                        if (fecha_compra <= fecha_final)
                        {
                            if (drActFij["vrcompra"].ToString() != "")
                                valorVrCompra = Convert.ToDouble(drActFij["vrcompra"]);
                            else
                                valorVrCompra = 0;
                            if (tipo_activo == 1)
                                valorDepre = valorDepre + 0; //  Math.Round(valorVrCompra / 36);
                            else if (tipo_activo == 2)
                                valorDepreMuebles = valorDepreMuebles + Math.Round(valorVrCompra / 36);
                            else if (tipo_activo == 3)
                                valorDepreEquipos = valorDepreEquipos + Math.Round(valorVrCompra / 36);
                            else if (tipo_activo == 4)
                                valorDepreVehiculos = valorDepreVehiculos + Math.Round(valorVrCompra / 36);
                        }
                    }

                    // Cargar fila de valor de activos fijos del PRESUPUESTO
                    string cod_cuenta = "";
                    cod_cuenta = DAPresupuesto.ConsultarParametroCuenta(19, pUsuario); // "512515";
                    if (cod_cuenta != "")
                    {
                        DataRow drVRDEPRECIACION;
                        drVRDEPRECIACION = dtPresupuesto.Rows.Find(cod_cuenta);
                        if (drVRDEPRECIACION != null)
                            drVRDEPRECIACION[numeroPeriodo + numero_columnas] = valorDepre;
                        if (drVRDEPRECIACION != null && numeroPeriodo == 0)
                        {
                            double saldoPromedio = 0;
                            if (drVRDEPRECIACION[4] != null)
                                saldoPromedio = Convert.ToDouble(drVRDEPRECIACION[4]);
                            if (saldoPromedio != 0)
                                drVRDEPRECIACION[6] = Math.Round(((valorDepre - saldoPromedio) / saldoPromedio) * 100, 2);
                        }
                    }

                    // Cargar fila de valor de activos fijos del PRESUPUESTO
                    cod_cuenta = DAPresupuesto.ConsultarParametroCuenta(20, pUsuario); // "512520";
                    if (cod_cuenta != "")
                    {
                        DataRow drVRDEPRECIACIONMUEBLES;
                        drVRDEPRECIACIONMUEBLES = dtPresupuesto.Rows.Find(cod_cuenta);
                        if (drVRDEPRECIACIONMUEBLES != null)
                            drVRDEPRECIACIONMUEBLES[numeroPeriodo + numero_columnas] = valorDepreMuebles;
                        if (drVRDEPRECIACIONMUEBLES != null && numeroPeriodo == 0)
                        {
                            double saldoPromedio = 0;
                            if (drVRDEPRECIACIONMUEBLES[4] != null)
                                saldoPromedio = Convert.ToDouble(drVRDEPRECIACIONMUEBLES[4]);
                            if (saldoPromedio != 0)
                                drVRDEPRECIACIONMUEBLES[6] = Math.Round(((valorDepreMuebles - saldoPromedio) / saldoPromedio) * 100, 2);
                        }
                    }

                    // Cargar fila de valor de activos fijos del PRESUPUESTO                    
                    cod_cuenta = DAPresupuesto.ConsultarParametroCuenta(21, pUsuario); //  "512525";
                    if (cod_cuenta != "")
                    {
                        DataRow drVRDEPRECIACIONEQUIPOS;
                        drVRDEPRECIACIONEQUIPOS = dtPresupuesto.Rows.Find(cod_cuenta);
                        if (drVRDEPRECIACIONEQUIPOS != null)
                            drVRDEPRECIACIONEQUIPOS[numeroPeriodo + numero_columnas] = valorDepreEquipos;
                        if (drVRDEPRECIACIONEQUIPOS != null && numeroPeriodo == 0)
                        {
                            double saldoPromedio = 0;
                            if (drVRDEPRECIACIONEQUIPOS[4] != null)
                                saldoPromedio = Convert.ToDouble(drVRDEPRECIACIONEQUIPOS[4]);
                            if (saldoPromedio != 0)
                                drVRDEPRECIACIONEQUIPOS[6] = Math.Round(((valorDepreEquipos - saldoPromedio) / saldoPromedio) * 100, 2);
                        }
                    }

                    // Cargar fila de valor de activos fijos del PRESUPUESTO                    
                    cod_cuenta = DAPresupuesto.ConsultarParametroCuenta(22, pUsuario); //  "512530";
                    if (cod_cuenta != "")
                    {
                        DataRow drVRDEPRECIACIONVEHICULOS;
                        drVRDEPRECIACIONVEHICULOS = dtPresupuesto.Rows.Find(cod_cuenta);
                        if (drVRDEPRECIACIONVEHICULOS != null)
                            drVRDEPRECIACIONVEHICULOS[numeroPeriodo + numero_columnas] = valorDepreVehiculos;
                        if (drVRDEPRECIACIONVEHICULOS != null && numeroPeriodo == 0)
                        {
                            double saldoPromedio = 0;
                            if (drVRDEPRECIACIONVEHICULOS[4] != null)
                                saldoPromedio = Convert.ToDouble(drVRDEPRECIACIONVEHICULOS[4]);
                            if (saldoPromedio != 0)
                                drVRDEPRECIACIONVEHICULOS[6] = Math.Round(((valorDepreVehiculos - saldoPromedio) / saldoPromedio) * 100, 2);
                        }
                    }

                    // Ir al siguiente período
                    numeroPeriodo = numeroPeriodo + 1;

                };

                if (bMayorizar == true)
                    MayorizarPresupuesto(ref dtPresupuesto, dtFechas, pUsuario, nivel);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "CargarPresupuestoCartera", ex);
                return;
            }
        }

        public Xpinn.Presupuesto.Entities.ActivosFijos CrearActivoFijo(Xpinn.Presupuesto.Entities.ActivosFijos pActivosFijos, Usuario vusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pActivosFijos = DAPresupuesto.CrearActivoFijo(pActivosFijos, vusuario);

                    ts.Complete();
                }

                return pActivosFijos;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "CrearActivoFijo", ex);
                return null;
            }
        }

        public void EliminarActivoFijo(Xpinn.Presupuesto.Entities.ActivosFijos pActivosFijos, Usuario vusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAPresupuesto.EliminarActivoFijo(pActivosFijos, vusuario);

                    ts.Complete();
                }

                return;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "EliminarActivoFijo", ex);
                return;
            }
        }

        public Xpinn.Presupuesto.Entities.ActivosFijos ModificarActivoFijo(Xpinn.Presupuesto.Entities.ActivosFijos pActivosFijos, Usuario vusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pActivosFijos = DAPresupuesto.ModificarActivoFijo(pActivosFijos, vusuario);

                    ts.Complete();
                }

                return pActivosFijos;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "ModificarActivoFijo", ex);
                return null;
            }
        }

        #endregion activosfijos

        #region diferidos

        public Xpinn.Presupuesto.Entities.Diferidos CrearDiferido(Xpinn.Presupuesto.Entities.Diferidos pDiferidos, Usuario vusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pDiferidos = DAPresupuesto.CrearDiferido(pDiferidos, vusuario);

                    ts.Complete();
                }

                return pDiferidos;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "CrearDiferido", ex);
                return null;
            }
        }

        public void EliminarDiferido(Xpinn.Presupuesto.Entities.Diferidos pDiferidos, Usuario vusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAPresupuesto.EliminarDiferido(pDiferidos, vusuario);

                    ts.Complete();
                }

                return;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "EliminarDiferido", ex);
                return;
            }
        }

        public Xpinn.Presupuesto.Entities.Diferidos ModificarDiferido(Xpinn.Presupuesto.Entities.Diferidos pDiferidos, Usuario vusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pDiferidos = DAPresupuesto.ModificarDiferido(pDiferidos, vusuario);

                    ts.Complete();
                }

                return pDiferidos;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "ModificarDiferido", ex);
                return null;
            }
        }

        public void CargarPresupuestoDiferidos(ref DataTable dtPresupuesto, DataTable dtDiferidos, DataTable dtFechas, Boolean bMayorizar, Usuario pUsuario,Int16 nivel)
        {
            try
            {            
                Xpinn.Comun.Business.FechasBusiness fechasBusiness = new Xpinn.Comun.Business.FechasBusiness();    
                // Inicializando variable de número de columnas
                int numeroPeriodo = 0;

                foreach (DataRow drFecha in dtFechas.Rows)
                {
                    string cod_cuenta = "";
                    double valorValorAmortizar = 0;
                    DateTime fecha_inicial = Convert.ToDateTime(drFecha[1].ToString());
                    DateTime fecha_final = Convert.ToDateTime(drFecha[2].ToString());
                    foreach (DataRow drFila in dtDiferidos.Rows)
                    {
                        double valor = 0;
                        double plazo = 0;
                        DateTime fecha_diferido = DateTime.MinValue;
                        DateTime fecha_final_diferido = DateTime.MinValue;
                        if (drFila["fecha_diferido"].ToString() != "")
                            fecha_diferido = Convert.ToDateTime(drFila["fecha_diferido"].ToString());
                        else
                            fecha_diferido = fecha_inicial;
                        if (drFila["plazo"].ToString() != "")
                            plazo = Convert.ToDouble(drFila["plazo"].ToString());
                        fecha_final_diferido = fechasBusiness.FecSumDia(fecha_diferido, Convert.ToInt32(Math.Round(plazo * 30)), 2);
                        if (fecha_diferido <= fecha_final && fecha_final_diferido >= fecha_inicial)
                        {
                            if (drFila["valor"].ToString() != "")
                                valor = Convert.ToDouble(drFila["valor"]);
                            else
                                valor = 0;
                            if (drFila["plazo"].ToString() != "")
                                plazo = Convert.ToDouble(drFila["plazo"]);
                            else
                                plazo = 0;
                            if (valor != 0 && plazo != 0)
                                valorValorAmortizar = valorValorAmortizar + Math.Round((valor / plazo));
                        }
                    }
                    
                    // Cargar fila de valor de diferidos del PRESUPUESTO
                    cod_cuenta = "512010";
                    DataRow drVALORDIFERIDOS;
                    drVALORDIFERIDOS = dtPresupuesto.Rows.Find(cod_cuenta);
                    if (drVALORDIFERIDOS != null)
                        drVALORDIFERIDOS[numeroPeriodo + numero_columnas] = valorValorAmortizar;
                    if (drVALORDIFERIDOS != null && numeroPeriodo == 0)
                    {
                        double saldoPromedio = 0;
                        if (drVALORDIFERIDOS[4] != null)
                            saldoPromedio = Convert.ToDouble(drVALORDIFERIDOS[4]);
                        if (saldoPromedio != 0)
                            drVALORDIFERIDOS[6] = Math.Round(((valorValorAmortizar - saldoPromedio) / saldoPromedio) * 100, 2);
                    }

                    // Ir al siguiente período
                    numeroPeriodo = numeroPeriodo + 1;

                };

                if (bMayorizar == true)
                    MayorizarPresupuesto(ref dtPresupuesto, dtFechas, pUsuario,nivel);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "CargarPresupuestoDiferidos", ex);
                return;
            }
        }

        public DataTable ListarDiferidos(DateTime pfechahistorico, ref string Error, Usuario pUsuario)
        {
            try
            {
                return DAPresupuesto.ListarDiferidos(pfechahistorico, ref Error, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "ListarDiferidos", ex);
                return null;
            }
        }

        #endregion diferidos

        #region otros

        public Xpinn.Presupuesto.Entities.Otros CrearOtro(Xpinn.Presupuesto.Entities.Otros pOtros, Usuario vusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pOtros = DAPresupuesto.CrearOtro(pOtros, vusuario);

                    ts.Complete();
                }

                return pOtros;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "CrearOtro", ex);
                return null;
            }
        }

        public Xpinn.Presupuesto.Entities.Otros ModificarOtro(Xpinn.Presupuesto.Entities.Otros pOtros, Usuario vusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pOtros = DAPresupuesto.ModificarOtro(pOtros, vusuario);

                    ts.Complete();
                }

                return pOtros;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "ModificarOtro", ex);
                return null;
            }
        }

        public void CargarPresupuestoOtros(ref DataTable dtPresupuesto, DataTable dtOtros, DataTable dtFechas, Boolean bMayorizar, Usuario pUsuario, Int16 nivel)
        {
            try
            {
                // Determinar el valor de los otros
                double valorArriendo = 0;
                double valorServicios = 0;
                double valorVigilancia = 0;
                foreach (DataRow drFila in dtOtros.Rows)
                {
                    try
                    {
                        if (drFila["ARRIENDO"].ToString() != "")
                        {
                            double valor = Convert.ToDouble(drFila["ARRIENDO"].ToString());
                            valorArriendo = valorArriendo + valor;
                        }
                        if (drFila["SERVICIOS"].ToString() != "")
                        {
                            double valor1 = Convert.ToDouble(drFila["SERVICIOS"].ToString());
                            valorServicios = valorServicios + valor1;
                        }
                        if (drFila["VIGILANCIA"].ToString() != "")
                        {
                            double valor2 = Convert.ToDouble(drFila["VIGILANCIA"].ToString());
                            valorVigilancia = valorVigilancia + valor2;
                        }
                    }
                    catch
                    {
                    }
                }

                // Inicializando variable de número de columnas
                int numeroPeriodo = 0;

                foreach (DataRow drFecha in dtFechas.Rows)
                {
                    string cod_cuenta = "";
                    // Cargar fila de valor del PRESUPUESTO
                    cod_cuenta = "511004";
                    DataRow drVALOR;
                    drVALOR = dtPresupuesto.Rows.Find(cod_cuenta);
                    if (drVALOR != null)
                        drVALOR[numeroPeriodo + numero_columnas] = valorArriendo;
                    if (drVALOR != null && numeroPeriodo == 0)
                    {
                        double saldoPromedio = 0;
                        if (drVALOR[4] != null)
                            saldoPromedio = Convert.ToDouble(drVALOR[4]);
                        if (saldoPromedio != 0)
                            drVALOR[6] = Math.Round(((valorArriendo - saldoPromedio) / saldoPromedio) * 100, 2);
                    }

                    // Cargar fila de valor del PRESUPUESTO
                    cod_cuenta = "511022";
                    DataRow drSERVICIOS;
                    drSERVICIOS = dtPresupuesto.Rows.Find(cod_cuenta);
                    if (drSERVICIOS != null)
                        drSERVICIOS[numeroPeriodo + numero_columnas] = valorServicios;
                    if (drSERVICIOS != null && numeroPeriodo == 0)
                    {
                        double saldoPromedio = 0;
                        if (drSERVICIOS[4] != null)
                            saldoPromedio = Convert.ToDouble(drSERVICIOS[4]);
                        if (saldoPromedio != 0)
                            drSERVICIOS[6] = Math.Round(((valorServicios - saldoPromedio) / saldoPromedio) * 100, 2);
                    }

                    // Cargar fila de valor del PRESUPUESTO
                    cod_cuenta = "511056";
                    DataRow drVIGILANCIA;
                    drVIGILANCIA = dtPresupuesto.Rows.Find(cod_cuenta);
                    if (drVIGILANCIA != null)
                        drVIGILANCIA[numeroPeriodo + numero_columnas] = valorVigilancia;
                    if (drVIGILANCIA != null && numeroPeriodo == 0)
                    {
                        double saldoPromedio = 0;
                        if (drVIGILANCIA[4] != null)
                            saldoPromedio = Convert.ToDouble(drVIGILANCIA[4]);
                        if (saldoPromedio != 0)
                            drVIGILANCIA[6] = Math.Round(((valorVigilancia - saldoPromedio) / saldoPromedio) * 100, 2);
                    }

                    // Ir al siguiente período
                    numeroPeriodo = numeroPeriodo + 1;

                };

                if (bMayorizar == true)
                    MayorizarPresupuesto(ref dtPresupuesto, dtFechas, pUsuario, nivel);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "CargarPresupuestoOtros", ex);
                return;
            }
        }

        public DataTable ListarOtros(DateTime pfechahistorico, ref string Error, Usuario pUsuario)
        {
            try
            {
                return DAPresupuesto.ListarOtros(pfechahistorico, ref Error, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "ListarOtros", ex);
                return null;
            }
        }

        #endregion otros

        #region Honorarios

        public Xpinn.Presupuesto.Entities.Honorarios CrearHonorario(Xpinn.Presupuesto.Entities.Honorarios pHonorarios, Usuario vusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pHonorarios = DAPresupuesto.CrearHonorario(pHonorarios, vusuario);

                    ts.Complete();
                }

                return pHonorarios;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "CrearHonorario", ex);
                return null;
            }
        }

        public void EliminarHonorario(Xpinn.Presupuesto.Entities.Honorarios pHonorarios, Usuario vusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAPresupuesto.EliminarHonorario(pHonorarios, vusuario);

                    ts.Complete();
                }

                return;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "EliminarHonorario", ex);
                return;
            }
        }

        public Xpinn.Presupuesto.Entities.Honorarios ModificarHonorario(Xpinn.Presupuesto.Entities.Honorarios pHonorarios, Usuario vusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pHonorarios = DAPresupuesto.ModificarHonorario(pHonorarios, vusuario);

                    ts.Complete();
                }

                return pHonorarios;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "ModificarHonorario", ex);
                return null;
            }
        }

        public DataTable ListarHonorarios(DateTime pfechahistorico, ref string Error, Usuario pUsuario)
        {
            try
            {
                return DAPresupuesto.ListarHonorarios(pfechahistorico, ref Error, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "ListarHonorarios", ex);
                return null;
            }
        }

        public void CargarPresupuestoHonorarios(ref DataTable dtPresupuesto, DataTable dtHonorarios, DataTable dtFechas, Boolean bMayorizar, Usuario pUsuario,Int16 nivel)
        {
            try
            {
                // Determinar el valor de los otros
                double valor = 0;
                var sumaSal = dtHonorarios.Compute("SUM(Valor)", string.Empty);
                if (sumaSal != System.DBNull.Value)
                    valor = Convert.ToDouble(sumaSal.ToString().Trim());

                // Inicializando variable de número de columnas
                int numeroPeriodo = 0;

                foreach (DataRow drFecha in dtFechas.Rows)
                {
                    string cod_cuenta = "";
                    // Cargar fila de valor del PRESUPUESTO
                    cod_cuenta = "511001";
                    DataRow drVALOR;
                    drVALOR = dtPresupuesto.Rows.Find(cod_cuenta);
                    if (drVALOR != null)
                        drVALOR[numeroPeriodo + numero_columnas] = valor;
                    if (drVALOR != null && numeroPeriodo == 0)
                    {
                        double saldoPromedio = 0;
                        if (drVALOR[4] != null)
                            saldoPromedio = Convert.ToDouble(drVALOR[4]);
                        if (saldoPromedio != 0)
                            drVALOR[6] = Math.Round(((valor - saldoPromedio) / saldoPromedio) * 100, 2);
                    }

                    // Ir al siguiente período
                    numeroPeriodo = numeroPeriodo + 1;

                };

                if (bMayorizar == true)
                    MayorizarPresupuesto(ref dtPresupuesto, dtFechas, pUsuario,nivel);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "CargarPresupuestoHonorarios", ex);
                return;
            }
        }

        #endregion Honorarios

        #region Obligaciones

        public Xpinn.Presupuesto.Entities.Obligacion CrearObligacion(Xpinn.Presupuesto.Entities.Obligacion pObligacion, Usuario vusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pObligacion = DAPresupuesto.CrearObligacion(pObligacion, vusuario);

                    ts.Complete();
                }

                return pObligacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "CrearObligacion", ex);
                return null;
            }
        }

        public void EliminarObligacion(Xpinn.Presupuesto.Entities.Obligacion pObligacion, Usuario vusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAPresupuesto.EliminarObligacion(pObligacion, vusuario);

                    ts.Complete();
                }

                return;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "EliminarObligacion", ex);
                return;
            }
        }

        public Xpinn.Presupuesto.Entities.Obligacion ModificarObligacion(Xpinn.Presupuesto.Entities.Obligacion pObligacion, Usuario vusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pObligacion = DAPresupuesto.ModificarObligacion(pObligacion, vusuario);

                    //int numeroPeriodo = 0;
                    //foreach (DataRow drFecha in pObligacion.dtFechas.Rows)
                    //{
                    //    pObligacion.iddetalle = 0;
                    //    pObligacion.numero_periodo = Convert.ToInt64(drFecha["numero"].ToString());
                    //    pObligacion.fecha_inicial = Convert.ToDateTime(drFecha["fecha_inicial"].ToString());
                    //    pObligacion.fecha_final = Convert.ToDateTime(drFecha["fecha_final"].ToString());
                    //    foreach (DataRow drFila in pObligacion.dtObligacionesNuevas.Rows)
                    //    {
                    //        pObligacion.centro_costo = pObligacion.centro_costo;
                    //        if (drFila[numeroPeriodo + 7].ToString() != "")
                    //            pObligacion.monto = Convert.ToDouble(drFila[numeroPeriodo + 7].ToString());
                    //        else
                    //            pObligacion.monto = 0;
                    //        pObligacion = DAPresupuesto.CrearDesembolsoObligacion(pObligacion, vusuario);
                    //    }
                    //}

                    ts.Complete();
                }

                return pObligacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "ModificarObligacion", ex);
                return null;
            }
        }

        public void CargarPresupuestoObligacion(ref DataTable dtPresupuesto, DataTable dtObligacion, DataTable dtObligacionNuevas, DataTable dtObligacionPagos, DataTable dtFechas, Boolean pCausado, Boolean bMayorizar, Usuario pUsuario,Int16 nivel)
        {
            try
            {
                // Inicializando variable de número de columnas
                int numeroPeriodo = 0;

                foreach (DataRow drFecha in dtFechas.Rows)
                {
                    // Calcular el valor del pago de interes, si es el flujo es interés y capital
                    decimal totalInteres = 0;
                    decimal totalCapital = 0;
                    foreach (DataRow drFila in dtObligacionPagos.Rows)
                    {                        
                        if (drFila[0].ToString() != "")
                        {
                            string codcomponente = "";
                            if (pCausado == true)
                            {
                                codcomponente = "-2";
                            }
                            else
                            {
                                if (drFila[2].ToString().Contains("1-"))
                                    codcomponente = "1";
                                else
                                    codcomponente = "2";
                            }
                            if (drFila[2].ToString().Substring(0, codcomponente.Length) == codcomponente)
                            {
                                decimal valPago = 0;
                                valPago = Convert.ToDecimal(drFila[numeroPeriodo + 3]);                                
                                if (codcomponente == "1")
                                    totalCapital = totalCapital + valPago;
                                else
                                    totalInteres = totalInteres + valPago;
                            }                            
                        }
                    }
                    // Calcular el valor de los intereses de las nuevas obligaciones
                    Xpinn.Comun.Business.FechasBusiness fechasBusiness = new Comun.Business.FechasBusiness();
                    PeriodicidadData DAPeriodicidad = new PeriodicidadData();
                    FabricaCreditos.Entities.Periodicidad ePeriodicidad = new FabricaCreditos.Entities.Periodicidad();
                    DateTime fecha_inicial_periodo = Convert.ToDateTime(drFecha[1].ToString());
                    DateTime fecha_final_periodo = Convert.ToDateTime(drFecha[2].ToString());
                    decimal totalInteresNuevos = 0; 
                    decimal totalCapitalNuevos = 0;
                    foreach (DataRow drFila in dtObligacionNuevas.Rows)
                    {
                        if (drFila[0].ToString() != "0" && drFila[numeroPeriodo + 8].ToString() != "")
                        {                            
                            decimal tasa = 0;
                            tasa = Convert.ToDecimal(drFila["tasa"]);
                            Int64 cod_periodicidad = 0;
                            cod_periodicidad = Convert.ToInt64(drFila["cod_periodicidad"]);     
                            ePeriodicidad = DAPeriodicidad.ConsultarPeriodicidad(cod_periodicidad, pUsuario);
                            decimal plazo = 0;
                            plazo = Convert.ToDecimal(drFila["plazo"]);

                            int periodoObligacion = 0;
                            foreach (DataRow drFec in dtFechas.Rows)
                            {
                                DateTime fecha_inicial = Convert.ToDateTime(drFec[1].ToString());
                                DateTime fecha_final = Convert.ToDateTime(drFec[2].ToString());
                                if (periodoObligacion < numeroPeriodo && plazo != 0 && drFila[periodoObligacion + 8].ToString() != "")
                                {                                    
                                    decimal monto = Convert.ToDecimal(drFila[periodoObligacion + 8]);;                                    
                                    decimal saldo = monto;
                                    decimal cuota = 0;
                                    if (ePeriodicidad.numero_meses != 0)
                                        // cuota = Math.Round(monto / (plazo / Convert.ToDecimal(ePeriodicidad.numero_meses)));
                                        cuota = Math.Round(monto / plazo);
                                    DateTime fecha_proximo_pago = fecha_final;
                                    for (int i=periodoObligacion;i<=numeroPeriodo;i++)
                                    {
                                        if (fecha_proximo_pago >= fecha_inicial_periodo && fecha_proximo_pago <= fecha_final_periodo)
                                        {
                                            totalCapitalNuevos = totalCapitalNuevos + cuota;
                                            if (pCausado == false)
                                                if (saldo != 0 && tasa != 0)
                                                    totalInteresNuevos = totalInteresNuevos + Math.Round((saldo * (tasa / 100) / Convert.ToDecimal(ePeriodicidad.periodos_anuales)));
                                        }
                                        if (fecha_proximo_pago >= fecha_inicial && fecha_proximo_pago <= fecha_final)
                                        {
                                            if (saldo >= cuota)
                                                saldo = saldo - cuota;
                                            else
                                                saldo = 0;
                                            fecha_proximo_pago = fechasBusiness.FecSumDia(fecha_proximo_pago, Convert.ToInt32(ePeriodicidad.numero_dias), Convert.ToInt32(ePeriodicidad.tipo_calendario));
                                        }                                        
                                    }
                                    if (pCausado == true)
                                        if (saldo != 0 && tasa != 0)
                                            totalInteresNuevos = totalInteresNuevos + Math.Round((saldo * (tasa / 100) / Convert.ToDecimal(ePeriodicidad.periodos_anuales))*30/Convert.ToDecimal(ePeriodicidad.numero_dias));
                                }
                                periodoObligacion += 1;
                            }
                            
                        }
                    }
                    // Cargar fila de valor de obligaciones del PRESUPUESTO
                    DataRow drVALOROBLIGACION;
                    drVALOROBLIGACION = dtPresupuesto.Rows.Find("615060");
                    if (drVALOROBLIGACION != null)
                    {
                        drVALOROBLIGACION[numeroPeriodo + numero_columnas] = totalInteres + totalInteresNuevos;
                        drVALOROBLIGACION.AcceptChanges();
                    }
                    if (drVALOROBLIGACION != null && numeroPeriodo == 0)
                    {
                        decimal saldoPromedio = 0;
                        if (drVALOROBLIGACION[4] != null)
                            saldoPromedio = Convert.ToDecimal(drVALOROBLIGACION[4]);
                        if (saldoPromedio != 0)
                            drVALOROBLIGACION[6] = Math.Round((((totalInteres + totalInteresNuevos) - saldoPromedio) / saldoPromedio) * 100, 2);
                    }

                    // Cargar fila de valor de capital obligaciones al FLUJO
                    if (pCausado == false)
                    {
                        DataRow drPagosObl;
                        drPagosObl = dtPresupuesto.Rows.Find("12");
                        if (drPagosObl != null)
                        {
                            drPagosObl[numeroPeriodo + numero_columnas] = totalCapital + totalCapitalNuevos;
                            drPagosObl.AcceptChanges();
                        }
                    }

                    // Ir al siguiente período
                    numeroPeriodo = numeroPeriodo + 1;

                };

                if (bMayorizar == true)
                    MayorizarPresupuesto(ref dtPresupuesto, dtFechas, pUsuario, nivel);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "CargarPresupuestoObligacion", ex);
                return;
            }
        }

        public void TotalizarObligaciones(ref DataTable dtTotales, ref DataTable dtTotalesPagos, DataTable dtObligacion, DataTable dtObligacionNuevas, DataTable dtObligacionPagos, DataTable dtFechas, Usuario pUsuario)
        {
            try
            {              
                // Inicializando variable de número de columnas
                int numeroPeriodo = 0;

                foreach (DataRow drFecha in dtFechas.Rows)
                {
                    // Inicializar columna de totales
                    foreach (DataRow drFila in dtTotales.Rows)
                    {
                        drFila[numeroPeriodo + 2] = 0;
                        drFila.AcceptChanges();
                    }
                    // Calcular el total de obligaciones por entidad
                    foreach (DataRow drFila in dtObligacion.Rows)
                    {
                        if (drFila[1].ToString() != "" && drFila[numeroPeriodo + 3].ToString() != "")
                        {
                            double valor = Convert.ToDouble(drFila[numeroPeriodo + 3].ToString());
                            DataRow drTot = dtTotales.Rows.Find(drFila[1].ToString());
                            if (drTot != null)
                            {
                                double total = 0;
                                if (drTot[numeroPeriodo + 2].ToString() != "")
                                    total = Convert.ToDouble(drTot[numeroPeriodo + 2].ToString());
                                total = total + valor;
                                drTot[numeroPeriodo + 2] = total;
                            }
                        }
                    }

                    // Calcular el total de obligaciones nuevas por entidad
                    Xpinn.Comun.Business.FechasBusiness fechasBusiness = new Comun.Business.FechasBusiness();
                    PeriodicidadData DAPeriodicidad = new PeriodicidadData();
                    FabricaCreditos.Entities.Periodicidad ePeriodicidad = new FabricaCreditos.Entities.Periodicidad();
                    DateTime fecha_inicial_periodo = Convert.ToDateTime(drFecha[1].ToString());
                    DateTime fecha_final_periodo = Convert.ToDateTime(drFecha[2].ToString());
                    foreach (DataRow drFila in dtObligacionNuevas.Rows)
                    {
                        if (drFila[1].ToString() != "" && drFila[numeroPeriodo + 8].ToString() != "" && drFila[0].ToString() != "0")
                        {
                            //////////////////////////////////////////////////////////////////////////////////////////////////////////
                            decimal tasa = 0;
                            tasa = Convert.ToDecimal(drFila["tasa"]);
                            Int64 cod_periodicidad = 0;
                            cod_periodicidad = Convert.ToInt64(drFila["cod_periodicidad"]);
                            ePeriodicidad = DAPeriodicidad.ConsultarPeriodicidad(cod_periodicidad, pUsuario);
                            decimal plazo = 0;
                            plazo = Convert.ToDecimal(drFila["plazo"]);
                            decimal saldoTotal = 0;
                            int periodoObligacion = 0;
                            foreach (DataRow drFec in dtFechas.Rows)
                            {
                                DateTime fecha_inicial = Convert.ToDateTime(drFec[1].ToString());
                                DateTime fecha_final = Convert.ToDateTime(drFec[2].ToString());        
                                if (periodoObligacion <= numeroPeriodo && plazo != 0 && drFila[periodoObligacion + 8].ToString() != "")
                                {
                                    decimal monto = Convert.ToDecimal(drFila[periodoObligacion + 8]); ;
                                    decimal saldo = monto;  
                                    decimal cuota = 0;
                                    if (ePeriodicidad.numero_meses != 0)
                                        // cuota = Math.Round(monto / (plazo/Convert.ToDecimal(ePeriodicidad.numero_meses)));
                                        cuota = Math.Round(monto / plazo);
                                    DateTime fecha_proximo_pago = fecha_final;
                                    for (int i = periodoObligacion; i < numeroPeriodo; i++)
                                    {
                                        if (fecha_proximo_pago >= fecha_inicial && fecha_proximo_pago <= fecha_final)
                                        {
                                            if (saldo >= cuota)
                                                saldo = saldo - cuota;
                                            else
                                                saldo = 0;
                                            fecha_proximo_pago = fechasBusiness.FecSumDia(fecha_proximo_pago, Convert.ToInt32(ePeriodicidad.numero_dias), Convert.ToInt32(ePeriodicidad.tipo_calendario));
                                        }
                                    }
                                    saldoTotal = saldoTotal + saldo;
                                }
                                periodoObligacion += 1;
                            }
                            /////////////////////////////////////////////////////////////////////////////////////////////////////////
                            //double valor = Convert.ToDouble(drFila[numeroPeriodo + 8].ToString());
                            double valor = Convert.ToDouble(saldoTotal);
                            DataRow drTot = dtTotales.Rows.Find(drFila[1].ToString());
                            if (drTot != null)
                            {
                                double total = 0;
                                if (drTot[numeroPeriodo + 2].ToString() != "")
                                    total = Convert.ToDouble(drTot[numeroPeriodo + 2].ToString());
                                total = total + valor;
                                drTot[numeroPeriodo + 2] = total;
                            }
                        }
                    }

                    // Calcular el total de obligaciones nevas por entidad
                    foreach (DataRow drFila in dtObligacionPagos.Rows)
                    {
                        if (drFila[1].ToString() != "" && drFila[numeroPeriodo + 3].ToString() != "")
                        {
                            double valor = Convert.ToDouble(drFila[numeroPeriodo + 3].ToString());
                            //DataRow drTot = dtTotalesPagos.Rows.Find(drFila[1].ToString() + "," + drFila[2].ToString());
                            DataRow[] drTot = dtTotalesPagos.Select("descripcion = '" + drFila[1].ToString() + "' And componente = '" + drFila[2].ToString() + "'");
                            if (drTot[0] != null)
                            {
                                double total = 0;
                                if (drTot[0][numeroPeriodo + 2].ToString() != "")
                                    total = Convert.ToDouble(drTot[0][numeroPeriodo + 2].ToString());
                                total = total + valor;
                                drTot[0][numeroPeriodo + 2] = total;
                            }
                        }
                    }

                    // Ir al siguiente período
                    numeroPeriodo = numeroPeriodo + 1;

                };
                
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "TotalizarObligaciones", ex);
                return;
            }
        }

        public DataTable ListarObligaciones(DateTime pfechahistorico, ref string Error, Usuario pUsuario, string filtro)
        {
            try
            {
                return DAPresupuesto.ListarObligaciones(pfechahistorico, ref Error, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "ListarObligaciones", ex);
                return null;
            }
        }

        public DataTable ListarObligacionesTotal(DateTime pfechahistorico, ref string Error, Usuario pUsuario, string filtro)
        {
            try
            {
                return DAPresupuesto.ListarObligacionesTotal(pfechahistorico, ref Error, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "ListarObligacionesTotal", ex);
                return null;
            }
        }

        public DataTable ListarObligacionesTotalPagos(DateTime pfechahistorico, ref string Error, Usuario pUsuario, string filtro)
        {
            try
            {
                return DAPresupuesto.ListarObligacionesTotalPagos(pfechahistorico, ref Error, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "ListarObligacionesTotalPagos", ex);
                return null;
            }
        }

        public DataTable ListarObligacionesNuevas(DateTime pfechahistorico, ref string Error, Usuario pUsuario, string filtro)
        {
            try
            {
                return DAPresupuesto.ListarObligacionesNuevas(pfechahistorico, ref Error, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "ListarObligacionesNuevas", ex);
                return null;
            }
        }

        public DataTable ListarObligacionesPagos(DateTime pfechahistorico, ref string Error, Usuario pUsuario, string filtro)
        {
            try
            {
                return DAPresupuesto.ListarObligacionesPagos(pfechahistorico, ref Error, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "ListarObligacionesPagos", ex);
                return null;
            }
        }

        public decimal ConsultarValorAPagarObligacion(DateTime fechahistorico, int cod_obligacion, DateTime fecha_inicial, DateTime fecha_final, int codcomponente, Usuario pUsuario)
        {
            try
            {
                return DAPresupuesto.ConsultarValorAPagarObligacion(fechahistorico, cod_obligacion, fecha_inicial, fecha_final, codcomponente, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "ConsultarValorAPagarObligacion", ex);
                return 0;
            }
        }

        public decimal ConsultarValorProvisionObligacion(DateTime fechahistorico, int cod_obligacion, DateTime fecha_inicial, DateTime fecha_final, Usuario pUsuario)
        {
            try
            {
                return DAPresupuesto.ConsultarValorProvisionObligacion(fechahistorico, cod_obligacion, fecha_inicial, fecha_final, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "ConsultarValorProvisionObligacion", ex);
                return 0;
            }
        }

        #endregion Obligaciones

        #region Tecnologia

        public DataTable ListarTecnologia(DateTime pfechahistorico, ref string Error, Usuario pUsuario)
        {
            try
            {
                return DAPresupuesto.ListarTecnologia(pfechahistorico, ref Error, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "ListarTecnologia", ex);
                return null;
            }
        }

        public void CargarPresupuestoTecnologia(ref DataTable dtPresupuesto, DataTable dtTecnologia, DataTable dtFechas, Boolean bMayorizar, Usuario pUsuario,Int16 nivel)
        {
            try
            {
                // Inicializando variable de número de columnas
                int numeroPeriodo = 0;

                foreach (DataRow drFecha in dtFechas.Rows)
                {
                    // Calcular depreciación de activos fijos si cumple fecha de compra
                    double valorTotal = 0;
                    double valorVrCompra = 0;
                    DateTime fecha_inicial = Convert.ToDateTime(drFecha[1].ToString());
                    DateTime fecha_final = Convert.ToDateTime(drFecha[2].ToString());
                    foreach (DataRow drTecno in dtTecnologia.Rows)
                    {
                        int tipo_concepto = 0;
                        if (drTecno["tipo_concepto"].ToString() != "")
                            tipo_concepto = Convert.ToInt32(drTecno["tipo_concepto"].ToString());
                        else
                            tipo_concepto = 1;
                        DateTime fecha_compra = DateTime.MinValue;
                        if (drTecno["fecha_compra"].ToString() != "")
                            fecha_compra = Convert.ToDateTime(drTecno["fecha_compra"].ToString());
                        else
                            fecha_compra = fecha_inicial;
                        if (fecha_compra >= fecha_inicial && fecha_compra <= fecha_final)
                        {
                            if (drTecno["valor"].ToString() != "")
                                valorVrCompra = Convert.ToDouble(drTecno["valor"]);
                            else
                                valorVrCompra = 0;
                            valorTotal = valorTotal + valorVrCompra;
                        }
                    }

                    // Cargar fila de valor de activos fijos del PRESUPUESTO
                    string cod_cuenta = "";
                    cod_cuenta = "15";
                    DataRow drTECNOLOGIA;
                    drTECNOLOGIA = dtPresupuesto.Rows.Find(cod_cuenta);
                    if (drTECNOLOGIA != null)
                        drTECNOLOGIA[numeroPeriodo + numero_columnas] = valorTotal;

                    // Ir al siguiente período
                    numeroPeriodo = numeroPeriodo + 1;

                };

                if (bMayorizar == true)
                    MayorizarPresupuesto(ref dtPresupuesto, dtFechas, pUsuario, nivel);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "CargarPresupuestoTecnologia", ex);
                return;
            }
        }

        public Xpinn.Presupuesto.Entities.Tecnologia CrearTecnologia(Xpinn.Presupuesto.Entities.Tecnologia pTecnologia, Usuario vusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTecnologia = DAPresupuesto.CrearTecnologia(pTecnologia, vusuario);

                    ts.Complete();
                }

                return pTecnologia;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "CrearTecnologia", ex);
                return null;
            }
        }

        public void EliminarTecnologia(Xpinn.Presupuesto.Entities.Tecnologia pTecnologia, Usuario vusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAPresupuesto.EliminarTecnologia(pTecnologia, vusuario);

                    ts.Complete();
                }

                return;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "EliminarTecnologia", ex);
                return;
            }
        }

        public Xpinn.Presupuesto.Entities.Tecnologia ModificarTecnologia(Xpinn.Presupuesto.Entities.Tecnologia pTecnologia, Usuario vusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTecnologia = DAPresupuesto.ModificarTecnologia(pTecnologia, vusuario);

                    ts.Complete();
                }

                return pTecnologia;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoBusiness", "ModificarTecnologia", ex);
                return null;
            }
        }

        #endregion Tecnologia

    }
}
