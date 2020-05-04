using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Asesores.Entities;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.Asesores.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla Credito
    /// </summary>
    public class CreditosData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla Credito
        /// </summary>
        public CreditosData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        /// <summary>
        /// Obtiene un registro en la tabla Credito de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla Credito</param>
        /// <returns>Entidad Credito consultado</returns>
        public Creditos ConsultarCreditoAsesor(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            Creditos entidad = new Creditos();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * from v_CreditoAsesor where numero_radicacion=" + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["numero_radicacion"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["numero_radicacion"]);
                            if (resultado["cod_deudor"] != DBNull.Value) entidad.codigo_cliente = Convert.ToInt64(resultado["cod_deudor"]);
                            if (resultado["linea"] != DBNull.Value) entidad.linea_credito = Convert.ToString(resultado["linea"]);
                            if (resultado["fecha_solicitud"] != DBNull.Value) entidad.fecha_solicitud = Convert.ToDateTime(resultado["fecha_solicitud"]);
                            if (resultado["monto_aprobado"] != DBNull.Value) entidad.monto_aprobado = Convert.ToInt64(resultado["monto_aprobado"]);
                            if (resultado["numero_cuotas"] != DBNull.Value) entidad.numero_cuotas = Convert.ToInt64(resultado["numero_cuotas"]);
                            if (resultado["cod_asesor_com"] != DBNull.Value) entidad.CodigoAsesor = Convert.ToInt64(resultado["cod_asesor_com"]);
                            if (resultado["asesor"] != DBNull.Value) entidad.NombreAsesor = Convert.ToString(resultado["asesor"]);
                            if (resultado["saldo_capital"] != DBNull.Value) entidad.saldo_capital = Convert.ToInt64(resultado["saldo_capital"]);
                            if (resultado["fecha_vencimiento"] != DBNull.Value) entidad.fecha_vencimiento = Convert.ToDateTime(resultado["fecha_vencimiento"]);
                            if (resultado["calificacion_promedio"] != DBNull.Value) entidad.calificacion_promedio = Convert.ToInt64(resultado["calificacion_promedio"]);
                            if (resultado["valor_cuota"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["valor_cuota"]);
                            if (resultado["cuotas_pagadas"] != DBNull.Value) entidad.cuotas_pagadas = Convert.ToInt64(resultado["cuotas_pagadas"]);
                            if (resultado["fecha_ultimo_pago"] != DBNull.Value) entidad.fecha_ultimo_pago = Convert.ToDateTime(resultado["fecha_ultimo_pago"]);
                            if (resultado["ult_valor_pagado"] != DBNull.Value) entidad.ult_valor_pagado = Convert.ToInt64(resultado["ult_valor_pagado"]);
                            if (resultado["fecha_proximo_pago"] != DBNull.Value) entidad.fecha_prox_pago = Convert.ToDateTime(resultado["fecha_proximo_pago"]);
                            if (resultado["valor_a_pagar"] != DBNull.Value) entidad.valor_a_pagar = Convert.ToDecimal(resultado["valor_a_pagar"]);
                            if (resultado["saldo_mora"] != DBNull.Value) entidad.saldo_mora = Convert.ToDecimal(resultado["saldo_mora"]);
                            if (resultado["saldo_atributos_mora"] != DBNull.Value) entidad.saldo_atributos_mora = Convert.ToDecimal(resultado["saldo_atributos_mora"]);
                            if (resultado["total_a_pagar"] != DBNull.Value) entidad.total_a_pagar = Convert.ToDecimal(resultado["total_a_pagar"]);
                            if (resultado["dias_mora"] != DBNull.Value) entidad.dias_mora = Convert.ToInt64(resultado["dias_mora"]);
                            if (resultado["pagare"] != DBNull.Value) entidad.numero_obligacion = Convert.ToString(resultado["pagare"]);
                            // Calcular el valor a pagar si no lo tiene
                            if (entidad.fecha_corte_string == "" || entidad.fecha_corte_string == null)
                            {
                                DetallePagoData DADetPago = new DetallePagoData();
                                decimal capital = 0, otros = 0;
                                if (DADetPago.ListarValoresAPagar(DateTime.Now, entidad.numero_radicacion, ref capital, ref otros, pUsuario))
                                {
                                    entidad.saldo_mora = capital;
                                    entidad.saldo_atributos_mora = otros;
                                }
                            }
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ConsultarCreditoAsesor", ex);
                        return null;
                    }
                }
            }
        }

        public List<Garantia> ListarCreditosGVPorFiltro(string filtroDefinido, string filtroGrilla, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Garantia> lstCreditos = new List<Garantia>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        if (string.IsNullOrWhiteSpace(filtroDefinido) && !string.IsNullOrWhiteSpace(filtroGrilla))
                        {
                            if (filtroGrilla.StartsWith(" and "))
                            {
                                filtroGrilla = filtroGrilla.Remove(0, 4).Insert(0, " WHERE ");
                            }
                        }

                        string sql = @"Select p.cod_persona, c.valor_cuota, c.numero_cuotas, c.monto_solicitado, c.numero_radicacion, p.identificacion, p.nombre as nombres, p.primer_apellido, p.segundo_apellido, c.cod_linea_credito, p.cod_oficina, 
                                        (select a.SNOMBRE1||' '||a.SNOMBRE2||' '||a.SAPELLIDO1||' '||a.SAPELLIDO2 from asejecutivos a where a.ICODIGO=c.cod_asesor_com) AS Nombre_Asesor, 
                                        (select ofi.nombre from oficina ofi where p.cod_oficina=ofi.cod_oficina) as oficina  
                                        From v_persona p
                                        JOIN credito c on c.COD_DEUDOR = p.COD_PERSONA " + filtroDefinido + " " + filtroGrilla;
                         
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Garantia entidad = new Garantia();
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.NumeroRadicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nom_persona = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["Nombre_Asesor"] != DBNull.Value) entidad.nombre_asesor = Convert.ToString(resultado["Nombre_Asesor"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["MONTO_SOLICITADO"] != DBNull.Value) entidad.monto = Convert.ToInt64(resultado["MONTO_SOLICITADO"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.plazo = Convert.ToInt64(resultado["NUMERO_CUOTAS"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);

                            lstCreditos.Add(entidad);
                        }

                        return lstCreditos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditosData", "ListarCreditosGVPorFiltro", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene un dato de la tabla general para Habeas Data
        /// </summary>
        /// <param name="pId">identificador de General</param>
        /// <returns>Parametro consultada</returns>
        public Creditos ConsultarParametroHabeas(Usuario pUsuario)
        {
            DbDataReader resultado;
            Creditos entidad = new Creditos();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select valor FROM GENERAL WHERE CODIGO=1050";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["valor"] != DBNull.Value) entidad.dias_mora_param = Convert.ToInt64(resultado["valor"]);
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ConsultarParametroHabeas", ex);
                        return null;
                    }
                }
            }
        }
        /// <summary>
        /// Obtiene un dato de la tabla general para Cobro Juridico
        /// </summary>
        /// <param name="pId">identificador de General</param>
        /// <returns>Parametro consultada</returns>
        public Creditos ConsultarParametroCobroPreJuridico2(Usuario pUsuario)
        {
            DbDataReader resultado;
            Creditos entidad = new Creditos();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select valor FROM GENERAL WHERE CODIGO=1052";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["valor"] != DBNull.Value) entidad.dias_mora_param = Convert.ToInt64(resultado["valor"]);
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ConsultarParametroCobroJuridico", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un dato de la tabla general para Cobro PreJuridico
        /// </summary>
        /// <param name="pId">identificador de General</param>
        /// <returns>Parametro consultada</returns>
        public Creditos ConsultarParametroCobroPreJuridico(Usuario pUsuario)
        {
            DbDataReader resultado;
            Creditos entidad = new Creditos();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select valor FROM GENERAL WHERE CODIGO=1051";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["valor"] != DBNull.Value) entidad.dias_mora_param = Convert.ToInt64(resultado["valor"]);
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ConsultarParametroCobroPreJuridico", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Método paa saber si un usuario es ejecutivo o no
        /// </summary>
        /// <param name="cod"></param>
        /// <returns></returns>
        public Int32 UsuarioEsEjecutivo(int cod, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Int32 resul = new Int32();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {

                    string sql = "Select count(*) as NUMERO From usuarios Where usuarios.identificacion Not In (Select a.sidentificacion From asejecutivos a Where a.iestado = 1) And usuarios.estado = 1 and usuarios.codusuario = " + cod;

                    connection.Open();
                    cmdTransaccionFactory.Connection = connection;
                    cmdTransaccionFactory.CommandType = CommandType.Text;
                    cmdTransaccionFactory.CommandText = sql;
                    resultado = cmdTransaccionFactory.ExecuteReader();
                    if (resultado.Read())
                        resul = Convert.ToInt32(resultado["NUMERO"]);
                    return resul;

                }
            }
        }


        public Int32 usuariopermisos(int cod, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Int32 resul = new Int32();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {

                    string sql = "select count(*) as NUMERO from usuarios where identificacion not in (select sidentificacion from asejecutivos where estado =1) and usuarios.estado=1 and codperfil =" + cod;

                    connection.Open();
                    cmdTransaccionFactory.Connection = connection;
                    cmdTransaccionFactory.CommandType = CommandType.Text;
                    cmdTransaccionFactory.CommandText = sql;
                    resultado = cmdTransaccionFactory.ExecuteReader();
                    if (resultado.Read())
                        resul = Convert.ToInt32(resultado["NUMERO"]);
                    dbConnectionFactory.CerrarConexion(connection);
                    return resul;

                }
            }
        }

        

        /// <summary>
        /// Obtiene un registro de la tabla General de la base de datos
        /// </summary>
        /// <param name="pId">identificador del registro</param>
        /// <returns>Parametro consultado</returns>
        public Diligencia ConsultarparametroUsuarioAsesor(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Diligencia entidad = new Diligencia();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT  valor FROM GENERAL WHERE CODIGO=6098";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["valor"] != DBNull.Value) entidad.codigo_parametro = Convert.ToInt64(resultado["valor"]);


                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditosData", "ConsultarparametroUsuarioDirector", ex);
                        return null;
                    }

                }
            }
        }
     
        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Credito dados unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<Creditos> ListarCreditoAsesor(Creditos pCredito, Usuario pUsuario, String filtro2, String orden)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Creditos> lstCredito = new List<Creditos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "select * from v_informe_creditos " + filtro2 + orden;
                      
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Creditos entidad = new Creditos();
                            if (resultado["idinforme"] != DBNull.Value) entidad.idinforme = Convert.ToInt64(resultado["idinforme"]);
                            if (resultado["cod_persona"] != DBNull.Value) entidad.codigo_cliente = Convert.ToInt64(resultado["cod_persona"]);
                            if (resultado["identificacion_persona"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion_persona"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nombre"]);
                            if (resultado["cod_nomina"] != DBNull.Value) entidad.cod_nomina = Convert.ToString(resultado["cod_nomina"]);
                            if (resultado["numero_radicacion"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["numero_radicacion"]);
                            if (resultado["cod_linea_credito"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["cod_linea_credito"]);
                            if (resultado["nombre_linea"] != DBNull.Value) entidad.linea_credito = Convert.ToString(resultado["nombre_linea"]);
                            if (resultado["fecha_solicitud"] != DBNull.Value) entidad.fecha_solicitud_string = Convert.ToDateTime(resultado["fecha_solicitud"]).ToString(conf.ObtenerFormatoFecha());
                            if (resultado["monto_aprobado"] != DBNull.Value) entidad.monto_aprobado = Convert.ToInt64(resultado["monto_aprobado"]);
                            if (resultado["saldo_capital"] != DBNull.Value) entidad.saldo_capital = Convert.ToInt64(resultado["saldo_capital"]);
                            if (resultado["valor_cuota"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["valor_cuota"]);
                            if (resultado["otros_saldos"] != DBNull.Value) entidad.otros_saldos = Convert.ToInt64(resultado["otros_saldos"]);
                            if (resultado["plazo"] != DBNull.Value) entidad.plazo = Convert.ToInt64(resultado["plazo"]);
                            if (resultado["cuotas_pagadas"] != DBNull.Value) entidad.cuotas_pagadas = Convert.ToInt64(resultado["cuotas_pagadas"]);
                            if (resultado["fecha_proximo_pago"] != DBNull.Value) entidad.fecha_prox_pago_string = Convert.ToDateTime(resultado["fecha_proximo_pago"]).ToString(conf.ObtenerFormatoFecha());
                            if (resultado["cod_oficina"] != DBNull.Value) entidad.codigo_oficina = Convert.ToInt64(resultado["cod_oficina"]);
                            if (resultado["oficina"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["oficina"]);
                            if (resultado["calificacion_promedio"] != DBNull.Value) entidad.calificacion_promedio = Convert.ToInt64(resultado["calificacion_promedio"]);
                            if (resultado["calificacion_cliente"] != DBNull.Value) entidad.calificacion_cliente = Convert.ToInt64(resultado["calificacion_cliente"]);
                            if (resultado["porc_renovacion_cuotas"] != DBNull.Value) entidad.porc_renovacion_cuotas = Convert.ToInt64(resultado["porc_renovacion_cuotas"]);
                            if (resultado["porc_renovacion_montos"] != DBNull.Value) entidad.porc_renovacion_montos = Convert.ToInt64(resultado["porc_renovacion_montos"]);
                            if (resultado["dias_mora"] != DBNull.Value) entidad.dias_mora = Convert.ToInt64(resultado["dias_mora"]);
                            if (resultado["saldo_mora"] != DBNull.Value) entidad.saldo_mora = Convert.ToInt64(resultado["saldo_mora"]);
                            if (resultado["saldo_atributos_mora"] != DBNull.Value) entidad.saldo_atributos_mora = Convert.ToInt64(resultado["saldo_atributos_mora"]);
                            if (resultado["estado"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["estado"]);
                            if (resultado["estado_juridico"] != DBNull.Value) entidad.estado_juridico = Convert.ToString(resultado["estado_juridico"]);
                            if (resultado["fecha_corte"] != DBNull.Value) entidad.fecha_corte_string = Convert.ToDateTime(resultado["fecha_corte"]).ToString(conf.ObtenerFormatoFecha());
                            if (resultado["cod_zona"] != DBNull.Value) entidad.zona = Convert.ToInt64(resultado["cod_zona"]);
                            // Calcular el valor a pagar si no lo tiene
                            if (entidad.fecha_corte_string == "" || entidad.fecha_corte_string == null)
                            {
                                DetallePagoData DADetPago = new DetallePagoData();
                                decimal capital = 0, otros = 0;
                                if (DADetPago.ListarValoresAPagar(DateTime.Now, entidad.numero_radicacion, ref capital, ref otros, pUsuario))
                                {
                                    entidad.saldo_mora = capital;
                                    entidad.saldo_atributos_mora = otros;
                                }
                            }

                            lstCredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ListarCreditoAsesor", ex);
                        return null;
                    }
                }
            }
        }
        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Credito dados unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<Creditos> ListarCreditoXAsesor(Creditos pCredito, Usuario pUsuario, String filtro, String orden)
        {
            DbDataReader resultado = default(DbDataReader);
            Creditos entidad = null;
            List<Creditos> lstCredito = new List<Creditos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try 
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "Select * from v_informe_creditos ";
                        sql = sql + "Where  cod_asesor = " + pUsuario.codusuario.ToString() + filtro.ToUpper().Replace("WHERE", " And ") + orden; 
                        
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            entidad = new Creditos();
                            if (resultado["idinforme"] != DBNull.Value) entidad.idinforme = Convert.ToInt64(resultado["idinforme"]);
                            if (resultado["cod_persona"] != DBNull.Value) entidad.codigo_cliente = Convert.ToInt64(resultado["cod_persona"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nombre"]);
                            if (resultado["identificacion_persona"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion_persona"]);
                            if (resultado["cod_nomina"] != DBNull.Value) entidad.cod_nomina = Convert.ToString(resultado["cod_nomina"]);
                            if (resultado["numero_radicacion"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["numero_radicacion"]);
                            if (resultado["cod_linea_credito"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["cod_linea_credito"]);
                            if (resultado["nombre_linea"] != DBNull.Value) entidad.linea_credito = Convert.ToString(resultado["nombre_linea"]);
                            if (resultado["fecha_solicitud"] != DBNull.Value) entidad.fecha_solicitud_string = Convert.ToDateTime(resultado["fecha_solicitud"]).ToString(conf.ObtenerFormatoFecha());
                            if (resultado["monto_aprobado"] != DBNull.Value) entidad.monto_aprobado = Convert.ToInt64(resultado["monto_aprobado"]);
                            if (resultado["saldo_capital"] != DBNull.Value) entidad.saldo_capital = Convert.ToInt64(resultado["saldo_capital"]);
                            if (resultado["valor_cuota"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["valor_cuota"]);
                            if (resultado["otros_saldos"] != DBNull.Value) entidad.otros_saldos = Convert.ToInt64(resultado["otros_saldos"]);
                            if (resultado["plazo"] != DBNull.Value) entidad.plazo = Convert.ToInt64(resultado["plazo"]);
                            if (resultado["cuotas_pagadas"] != DBNull.Value) entidad.cuotas_pagadas = Convert.ToInt64(resultado["cuotas_pagadas"]);
                            if (resultado["fecha_proximo_pago"] != DBNull.Value) entidad.fecha_prox_pago_string = Convert.ToDateTime(resultado["fecha_proximo_pago"]).ToString(conf.ObtenerFormatoFecha());
                            if (resultado["cod_oficina"] != DBNull.Value) entidad.codigo_oficina = Convert.ToInt64(resultado["cod_oficina"]);
                            if (resultado["oficina"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["oficina"]);
                            if (resultado["calificacion_promedio"] != DBNull.Value) entidad.calificacion_promedio = Convert.ToInt64(resultado["calificacion_promedio"]);
                            if (resultado["calificacion_cliente"] != DBNull.Value) entidad.calificacion_cliente = Convert.ToInt64(resultado["calificacion_cliente"]);
                            if (resultado["porc_renovacion_cuotas"] != DBNull.Value) entidad.porc_renovacion_cuotas = Convert.ToInt64(resultado["porc_renovacion_cuotas"]);
                            if (resultado["porc_renovacion_montos"] != DBNull.Value) entidad.porc_renovacion_montos = Convert.ToInt64(resultado["porc_renovacion_montos"]);
                            if (resultado["dias_mora"] != DBNull.Value) entidad.dias_mora = Convert.ToInt64(resultado["dias_mora"]);
                            if (resultado["saldo_mora"] != DBNull.Value) entidad.saldo_mora = Convert.ToInt64(resultado["saldo_mora"]);
                            if (resultado["saldo_atributos_mora"] != DBNull.Value) entidad.saldo_atributos_mora = Convert.ToInt64(resultado["saldo_atributos_mora"]);
                            if (resultado["estado"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["estado"]);
                            if (resultado["estado_juridico"] != DBNull.Value) entidad.estado_juridico = Convert.ToString(resultado["estado_juridico"]);
                            if (resultado["fecha_corte"] != DBNull.Value) entidad.fecha_corte_string = Convert.ToDateTime(resultado["fecha_corte"]).ToString("dd/MM/yyyy");
                            if (resultado["cod_zona"] != DBNull.Value) entidad.zona = Convert.ToInt64(resultado["cod_zona"]);
                            if (resultado["cod_asesor"] != DBNull.Value) entidad.CodigoAsesor = Convert.ToInt64(resultado["cod_asesor"]);
                            // Calcular el valor a pagar si no lo tiene
                            if (entidad.fecha_corte_string == "" || entidad.fecha_corte_string == null)
                            {
                                DetallePagoData DADetPago = new DetallePagoData();
                                decimal capital = 0, otros = 0;
                                if (DADetPago.ListarValoresAPagar(DateTime.Now, entidad.numero_radicacion, ref capital, ref otros, pUsuario))
                                {
                                    entidad.saldo_mora = capital;
                                    entidad.saldo_atributos_mora = otros;
                                }
                            }
                            lstCredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCredito;

                    }
                    catch (Exception ex)
                    {
                   
                        BOExcepcion.Throw("CreditoData", "ListarCreditoXAsesor", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Credito dados unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<Creditos> ListarCreditoXOficina(Int64 pcodoficina, Usuario pUsuario, String filtro, String orden, bool todasLasOficinas = false)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Creditos> lstCredito = new List<Creditos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "Select * from v_informe_creditos ";

                        if (!todasLasOficinas)
                        {
                            sql += "Where cod_oficina = " + pcodoficina + filtro.ToUpper().Replace("WHERE", " And ") + orden;    
                        }
                        else
                        {
                            sql += filtro.ToUpper() + orden;
                        }

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Creditos entidad = new Creditos();
                            if (resultado["idinforme"] != DBNull.Value) entidad.idinforme = Convert.ToInt64(resultado["idinforme"]);
                            if (resultado["cod_persona"] != DBNull.Value) entidad.codigo_cliente = Convert.ToInt64(resultado["cod_persona"]);
                            if (resultado["identificacion_persona"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion_persona"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nombre"]);
                            if (resultado["cod_nomina"] != DBNull.Value) entidad.cod_nomina = Convert.ToString(resultado["cod_nomina"]);
                            if (resultado["numero_radicacion"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["numero_radicacion"]);
                            if (resultado["cod_linea_credito"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["cod_linea_credito"]);
                            if (resultado["nombre_linea"] != DBNull.Value) entidad.linea_credito = Convert.ToString(resultado["nombre_linea"]);
                            if (resultado["fecha_solicitud"] != DBNull.Value) entidad.fecha_solicitud_string = Convert.ToDateTime(resultado["fecha_solicitud"]).ToString(conf.ObtenerFormatoFecha());
                            if (resultado["monto_aprobado"] != DBNull.Value) entidad.monto_aprobado = Convert.ToInt64(resultado["monto_aprobado"]);
                            if (resultado["saldo_capital"] != DBNull.Value) entidad.saldo_capital = Convert.ToInt64(resultado["saldo_capital"]);
                            if (resultado["valor_cuota"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["valor_cuota"]);
                            if (resultado["otros_saldos"] != DBNull.Value) entidad.otros_saldos = Convert.ToInt64(resultado["otros_saldos"]);
                            if (resultado["plazo"] != DBNull.Value) entidad.plazo = Convert.ToInt64(resultado["plazo"]);
                            if (resultado["cuotas_pagadas"] != DBNull.Value) entidad.cuotas_pagadas = Convert.ToInt64(resultado["cuotas_pagadas"]);
                            if (resultado["fecha_proximo_pago"] != DBNull.Value) entidad.fecha_prox_pago_string = Convert.ToDateTime(resultado["fecha_proximo_pago"]).ToString(conf.ObtenerFormatoFecha());
                            if (resultado["cod_oficina"] != DBNull.Value) entidad.codigo_oficina = Convert.ToInt64(resultado["cod_oficina"]);
                            if (resultado["oficina"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["oficina"]);
                            if (resultado["calificacion_promedio"] != DBNull.Value) entidad.calificacion_promedio = Convert.ToInt64(resultado["calificacion_promedio"]);
                            if (resultado["calificacion_cliente"] != DBNull.Value) entidad.calificacion_cliente = Convert.ToInt64(resultado["calificacion_cliente"]);
                            if (resultado["porc_renovacion_cuotas"] != DBNull.Value) entidad.porc_renovacion_cuotas = Convert.ToInt64(resultado["porc_renovacion_cuotas"]);
                            if (resultado["porc_renovacion_montos"] != DBNull.Value) entidad.porc_renovacion_montos = Convert.ToInt64(resultado["porc_renovacion_montos"]);
                            if (resultado["dias_mora"] != DBNull.Value) entidad.dias_mora = Convert.ToInt64(resultado["dias_mora"]);
                            if (resultado["saldo_mora"] != DBNull.Value) entidad.saldo_mora = Convert.ToInt64(resultado["saldo_mora"]);
                            if (resultado["saldo_atributos_mora"] != DBNull.Value) entidad.saldo_atributos_mora = Convert.ToInt64(resultado["saldo_atributos_mora"]);
                            if (resultado["estado"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["estado"]);
                            if (resultado["estado_juridico"] != DBNull.Value) entidad.estado_juridico = Convert.ToString(resultado["estado_juridico"]);
                            if (resultado["fecha_corte"] != DBNull.Value) entidad.fecha_corte_string = Convert.ToDateTime(resultado["fecha_corte"]).ToString(conf.ObtenerFormatoFecha());
                            if (resultado["cod_zona"] != DBNull.Value) entidad.zona = Convert.ToInt64(resultado["cod_zona"]);
                            if (resultado["cod_asesor"] != DBNull.Value) entidad.CodigoAsesor = Convert.ToInt64(resultado["cod_asesor"]);
                            // Calcular el valor a pagar si no lo tiene
                            if (entidad.fecha_corte_string == "" || entidad.fecha_corte_string == null)
                            {
                                DetallePagoData DADetPago = new DetallePagoData();
                                decimal capital = 0, otros = 0;
                                if (DADetPago.ListarValoresAPagar(DateTime.Now, entidad.numero_radicacion, ref capital, ref otros, pUsuario))
                                {
                                    entidad.saldo_mora = capital;
                                    entidad.saldo_atributos_mora = otros;
                                }
                            }
                            lstCredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ListarCreditoXAsesor", ex);
                        return null;
                    }
                }
            }
        }

        public Cliente ConsultarCodeudor(Int64 numero_radicacion, Usuario pUsuario)
        {
            DbDataReader resultado;
            Cliente entidad = new Cliente();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * from v_Codeudores where numero_radicacion = " + numero_radicacion;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["cod_persona"] != DBNull.Value) entidad.IdCliente = Convert.ToInt64(resultado["cod_persona"]);
                            if (resultado["tipo_identificacion"] != DBNull.Value) entidad.TipoDocumento = Convert.ToString(resultado["tipo_identificacion"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.NumeroDocumento = Convert.ToString(resultado["identificacion"]);
                            if (resultado["primer_nombre"] != DBNull.Value) entidad.PrimerNombre = Convert.ToString(resultado["primer_nombre"]);
                            if (resultado["segundo_nombre"] != DBNull.Value) entidad.SegundoNombre = Convert.ToString(resultado["segundo_nombre"]);
                            if (resultado["primer_apellido"] != DBNull.Value) entidad.PrimerApellido = Convert.ToString(resultado["primer_apellido"]);
                            if (resultado["segundo_apellido"] != DBNull.Value) entidad.SegundoApellido = Convert.ToString(resultado["segundo_apellido"]);
                            if (resultado["direccion"] != DBNull.Value) entidad.Direccion = Convert.ToString(resultado["direccion"]);
                            if (resultado["telefono"] != DBNull.Value) entidad.Telefono = Convert.ToString(resultado["telefono"]);
                            if (resultado["barrio"] != DBNull.Value) entidad.Barrio = Convert.ToString(resultado["barrio"]);
                            if (resultado["email"] != DBNull.Value) entidad.Email = Convert.ToString(resultado["email"].ToString());

                        }
                        //else
                        //{
                        //  throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        // }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ConsultarCodeudor", ex);
                        return null;
                    }
                }

            }
        }

        public List<Cliente> ListarCodeudores(Int64 numero_radicacion, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<Cliente> lstCodeudores = new List<Cliente>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * from v_Codeudores where numero_radicacion = " + numero_radicacion;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            Cliente entidad = new Cliente();
                            if (resultado["cod_persona"] != DBNull.Value) entidad.IdCliente = Convert.ToInt64(resultado["cod_persona"]);
                            if (resultado["tipo_identificacion"] != DBNull.Value) entidad.TipoDocumento = Convert.ToString(resultado["tipo_identificacion"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.NumeroDocumento = Convert.ToString(resultado["identificacion"]);
                            if (resultado["primer_nombre"] != DBNull.Value) entidad.PrimerNombre = Convert.ToString(resultado["primer_nombre"]);
                            if (resultado["segundo_nombre"] != DBNull.Value) entidad.SegundoNombre = Convert.ToString(resultado["segundo_nombre"]);
                            if (resultado["primer_apellido"] != DBNull.Value) entidad.PrimerApellido = Convert.ToString(resultado["primer_apellido"]);
                            if (resultado["segundo_apellido"] != DBNull.Value) entidad.SegundoApellido = Convert.ToString(resultado["segundo_apellido"]);
                            if (resultado["direccion"] != DBNull.Value) entidad.Direccion = Convert.ToString(resultado["direccion"]);
                            if (resultado["telefono"] != DBNull.Value) entidad.Telefono = Convert.ToString(resultado["telefono"]);
                            if (resultado["barrio"] != DBNull.Value) entidad.Barrio = Convert.ToString(resultado["barrio"]);
                            if (resultado["email"] != DBNull.Value) entidad.Email = Convert.ToString(resultado["email"].ToString());
                            lstCodeudores.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCodeudores;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ListarCodeudores", ex);
                        return null;
                    }
                }
            }
        }

        public Creditos ConsultarParametroCampaña (Usuario pUsuario)
        {
            DbDataReader resultado;
            Creditos entidad = new Creditos();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select valor FROM GENERAL WHERE CODIGO=1053";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["valor"] != DBNull.Value) entidad.dias_mora_param = Convert.ToInt64(resultado["valor"]);
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ConsultarParametroCampaña", ex);
                        return null;
                    }
                }
            }
        }

        public Creditos ConsultarParametroVisitaAbogado(Usuario pUsuario)
        {
            DbDataReader resultado;
            Creditos entidad = new Creditos();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select valor FROM GENERAL WHERE CODIGO=1054";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["valor"] != DBNull.Value) entidad.dias_mora_param = Convert.ToInt64(resultado["valor"]);                            
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ConsultarParametroVisitaAbogado", ex);
                        return null;
                    }
                }
            }
        }

        public List<PersonaMora> ListarPersonasMora(string filtro, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<PersonaMora> lstPersonasMora = new List<PersonaMora>();


            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        //string sql = @"with Resultado as( 
                        //            select P.identificacion, P.COD_PERSONA , P.COD_NOMINA, P.COD_OFICINA, P.COD_ZONA, P.PRIMER_NOMBRE || ' ' || P.SEGUNDO_NOMBRE || ' ' || P.PRIMER_APELLIDO || ' ' || P.SEGUNDO_APELLIDO AS NOMBRE,
                        //            (select count(c.NUMERO_RADICACION) from credito c where c.COD_DEUDOR = P.COD_PERSONA and c.ESTADO in('C') and c.SALDO_CAPITAL != 0
                        //            and(select Nvl((CASE Sign(x.fecha_proximo_pago - SYSDATE) WHEN - 1 THEN FecDifDia(x.fecha_proximo_pago, SYSDATE, 1) ELSE 0 END), 0)
                        //            from credito x where c.NUMERO_RADICACION = x.NUMERO_RADICACION) > 0) as creditos,
                        //            (select count(a.NUMERO_APORTE) from APORTE a where a.COD_PERSONA = P.COD_PERSONA and a.ESTADO in(1) and a.NUMERO_APORTE != 0
                        //            and(select Nvl((CASE Sign(y.fecha_proximo_pago - SYSDATE) WHEN - 1 THEN FecDifDia(y.fecha_proximo_pago, SYSDATE, 1) ELSE 0 END), 0)
                        //            from aporte y where a.NUMERO_APORTE = y.NUMERO_APORTE) > 0 ) as aportes,
                        //            (select count(s.NUMERO_SERVICIO) from servicios s where s.COD_PERSONA = P.COD_PERSONA  and s.ESTADO in('C') and s.SALDO != 0
                        //            and(select Nvl((CASE Sign(z.fecha_proximo_pago - SYSDATE) WHEN - 1 THEN FecDifDia(z.fecha_proximo_pago, SYSDATE, 1) ELSE 0 END), 0)
                        //            from SERVICIOS z where s.NUMERO_SERVICIO = z.NUMERO_SERVICIO) > 0) as servicios,
                        //            (select COUNT(pa.IDAFILIACION) from PERSONA_AFILIACION pa 
                        //            where pa.SALDO > 0
                        //            and (select Nvl((CASE Sign(y.fecha_proximo_pago - SYSDATE) WHEN - 1 THEN FecDifDia(y.fecha_proximo_pago, SYSDATE, 1) ELSE 0 END), 0)
                        //            from PERSONA_AFILIACION y where pa.COD_PERSONA = P.COD_PERSONA and pa.IDAFILIACION = y.IDAFILIACION) > 0) as afiliacion, 
                        //            --Suma los productos
                        //            sum((select count(c.NUMERO_RADICACION) from credito c where c.COD_DEUDOR = P.COD_PERSONA and c.ESTADO in('C') and c.SALDO_CAPITAL != 0
                        //            and(select Nvl((CASE Sign(x.fecha_proximo_pago - SYSDATE) WHEN - 1 THEN FecDifDia(x.fecha_proximo_pago, SYSDATE, 1) ELSE 0 END), 0)
                        //            from credito x where c.NUMERO_RADICACION = x.NUMERO_RADICACION) > 0) +
                        //            (select count(a.NUMERO_APORTE) from APORTE a where a.COD_PERSONA = P.COD_PERSONA and a.ESTADO in(1) and a.NUMERO_APORTE != 0
                        //            and(select Nvl((CASE Sign(y.fecha_proximo_pago - SYSDATE) WHEN - 1 THEN FecDifDia(y.fecha_proximo_pago, SYSDATE, 1) ELSE 0 END), 0)
                        //            from aporte y where a.NUMERO_APORTE = y.NUMERO_APORTE) > 0 ) +
                        //            (select count(s.NUMERO_SERVICIO) from servicios s where s.COD_PERSONA = P.COD_PERSONA  and s.ESTADO in('C') and s.SALDO != 0
                        //            and(select Nvl((CASE Sign(z.fecha_proximo_pago - SYSDATE) WHEN - 1 THEN FecDifDia(z.fecha_proximo_pago, SYSDATE, 1) ELSE 0 END), 0)
                        //            from SERVICIOS z where s.NUMERO_SERVICIO = z.NUMERO_SERVICIO) > 0)+
                        //            (select COUNT(pa.IDAFILIACION) from PERSONA_AFILIACION pa 
                        //            where pa.COD_PERSONA = P.COD_PERSONA and pa.SALDO > 0
                        //            and (select Nvl((CASE Sign(y.fecha_proximo_pago - SYSDATE) WHEN - 1 THEN FecDifDia(y.fecha_proximo_pago, SYSDATE, 1) ELSE 0 END), 0)
                        //            from PERSONA_AFILIACION y where pa.IDAFILIACION = y.IDAFILIACION) > 0)) as productos
                        //            from persona P
                        //            group by P.identificacion, P.COD_PERSONA, P.PRIMER_NOMBRE, P.SEGUNDO_NOMBRE, P.PRIMER_APELLIDO, 
                        //            P.SEGUNDO_APELLIDO, P.COD_NOMINA, P.COD_OFICINA, P.COD_ZONA)
                        //            select r.*
                        //            from resultado r
                        //            where r.productos > 0";

                        string sql = @"select P.identificacion, r.COD_PERSONA , P.COD_NOMINA, P.COD_OFICINA,p.NOMBRES AS NOMBRE,
                                        creditos ,ROUND(NVL(r.DIAS_MORA_C/creditos,0),0)  DIAS_MORA_C ,Aportes,ROUND(NVL(DIAS_MORA_A/aportes,0),0) DIAS_MORA_A
                                        ,SERVICIOS,ROUND(NVL(DIAS_MORA_S/SERVICIOS,0),0) DIAS_MORA_S ,r.AFILIACION,ROUND(NVL(r.DIAS_MORA_PA/AFILIACION,0),0)
                                        DIAS_MORA_PA,
                                        (r.CREDITOs + r.APORTES + r.SERVICIOS + r.AFILIACION) Productos from V_DIAS_MORA_PERSONA r 
                                        inner join v_persona p on r.cod_persona = p.cod_persona
                                        where (r.CREDITOs + r.APORTES + r.SERVICIOS + r.AFILIACION)  != 0 ";


                        if (filtro != "")
                        {
                            sql = sql + filtro;
                        }


                        //sql += "order by CREDITOs,APORTES,SERVICIOS,AFILIACION"; 
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            PersonaMora entidad = new PersonaMora();
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.NumeroDocumento = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.IdPersona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["COD_NOMINA"] != DBNull.Value) entidad.CodigoNomina = Convert.ToString(resultado["COD_NOMINA"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.Oficina = resultado["COD_OFICINA"].ToString();                           
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.Nombres = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["PRODUCTOS"] != DBNull.Value) entidad.Productos = Convert.ToInt16(resultado["PRODUCTOS"]);

                            if (resultado["CREDITOS"] != DBNull.Value) entidad.CantCreditos = Convert.ToInt16(resultado["CREDITOS"]);
                            if (resultado["APORTES"] != DBNull.Value) entidad.CantAportes = Convert.ToInt16(resultado["APORTES"].ToString());
                            if (resultado["SERVICIOS"] != DBNull.Value) entidad.CantServicios = Convert.ToInt16(resultado["SERVICIOS"]);
                            if (resultado["AFILIACION"] != DBNull.Value) entidad.CantAfiliacion = Convert.ToInt16(resultado["AFILIACION"]);

                            if (resultado["DIAS_MORA_C"] != DBNull.Value) entidad.MoraCreditos = Convert.ToDecimal(resultado["DIAS_MORA_C"]);
                            if (resultado["DIAS_MORA_A"] != DBNull.Value) entidad.MoraAportes = Convert.ToDecimal(resultado["DIAS_MORA_A"]);
                            if (resultado["DIAS_MORA_S"] != DBNull.Value) entidad.MoraServicios = Convert.ToDecimal(resultado["DIAS_MORA_S"]);
                            if (resultado["DIAS_MORA_PA"] != DBNull.Value) entidad.MoraAfiliacion = Convert.ToDecimal(resultado["DIAS_MORA_PA"]);

                            lstPersonasMora.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPersonasMora;




                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ListarPersonasMora", ex);
                        return null;
                    }
                }
            }
        }
        public decimal ConsultarTotalValorMoraPersona(string pCod_Persona, string pidentificacion, DateTime pFechaCorte, Usuario pUsuario)
        {
            DbDataReader resultado;
            decimal moratotal = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;

                        DbParameter p_identificacion = cmdTransaccionFactory.CreateParameter();
                        p_identificacion.ParameterName = "p_identificacion";
                        p_identificacion.Value = pidentificacion;
                        p_identificacion.Direction = ParameterDirection.Input;
                        p_identificacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_identificacion);

                        DbParameter p_cod_persona = cmdTransaccionFactory.CreateParameter();
                        p_cod_persona.ParameterName = "p_cod_persona";
                        p_cod_persona.Value = Convert.ToInt64(pCod_Persona);
                        p_cod_persona.Direction = ParameterDirection.Input;
                        p_cod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_persona);

                        DbParameter p_fecha_corte = cmdTransaccionFactory.CreateParameter();
                        p_fecha_corte.ParameterName = "p_fecha_corte";
                        p_fecha_corte.Value = pFechaCorte;
                        p_fecha_corte.Direction = ParameterDirection.Input;
                        p_fecha_corte.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(p_fecha_corte);


                        cmdTransaccionFactory.CommandText = "XPF_AS_CALCULAR_MORAS";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ConsultarTotalValorMoraPersona", ex);
                        return 0;
                    }
                };
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {

                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        string sql = @"select NVL(sum(SALDO_TOTAL),0) AS VALOR from temp_moras where COD_PERSONA = " + pCod_Persona + " and TIPO_PRODUCTO not in(22)";


                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["VALOR"] != DBNull.Value) moratotal = Convert.ToDecimal(resultado["VALOR"]);
                        }


                        dbConnectionFactory.CerrarConexion(connection);
                        return moratotal;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ConsultarTotalValorMoraPersona", ex);
                        return 0;
                    }
                }
            }
        }
        public List<ProductosMora> ConsultarDetalleMoraPersona(string pCod_Persona, string pidentificacion, DateTime pFechaCorte, string filtro_productos, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<ProductosMora> lstProductMora = new List<ProductosMora>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;

                        DbParameter p_identificacion = cmdTransaccionFactory.CreateParameter();
                        p_identificacion.ParameterName = "p_identificacion";
                        if (pidentificacion != "")
                        {
                            p_identificacion.Value = pidentificacion;
                        }
                        else
                        {
                            p_identificacion.Value = DBNull.Value;
                        }
                        p_identificacion.Direction = ParameterDirection.Input;
                        p_identificacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_identificacion);

                        DbParameter p_cod_persona = cmdTransaccionFactory.CreateParameter();
                        p_cod_persona.ParameterName = "p_cod_persona";
                        if (pCod_Persona != "")
                        {
                            p_cod_persona.Value = Convert.ToInt64(pCod_Persona);
                        }
                        else
                        {
                            p_cod_persona.Value = DBNull.Value;
                        }
                        p_cod_persona.Direction = ParameterDirection.Input;
                        p_cod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_persona);

                        DbParameter p_fecha_corte = cmdTransaccionFactory.CreateParameter();
                        p_fecha_corte.ParameterName = "p_fecha_corte";
                        p_fecha_corte.Value = pFechaCorte;
                        p_fecha_corte.Direction = ParameterDirection.Input;
                        p_fecha_corte.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(p_fecha_corte);


                        cmdTransaccionFactory.CommandText = "XPF_AS_CALCULAR_MORAS";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ConsultarDetalleMoraPersona", ex);
                        return null;
                    }
                };
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {

                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        string sql = @"select * from temp_moras where COD_PERSONA = " + pCod_Persona;

                        if (filtro_productos != "")
                        {
                            string filtro = " and TIPO_PRODUCTO in (" + filtro_productos + ")";
                            sql = sql + filtro;
                        }

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ProductosMora entidad = new ProductosMora();

                            if (resultado["PERIODO"] != DBNull.Value) entidad.periodo = Convert.ToString(resultado["PERIODO"]);
                            if (resultado["NUMERO_PRODUCTO"] != DBNull.Value) entidad.numero_producto = Convert.ToString(resultado["NUMERO_PRODUCTO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["FECHA_VENCIMENTO"] != DBNull.Value) entidad.fecha_vencimento = Convert.ToDateTime(resultado["FECHA_VENCIMENTO"]);
                            if (resultado["DIAS"] != DBNull.Value) entidad.dias = Convert.ToInt32(resultado["DIAS"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToString(resultado["FORMA_PAGO"]);
                            if (resultado["CAPITAL"] != DBNull.Value) entidad.capital = Convert.ToDecimal(resultado["CAPITAL"]);
                            if (resultado["EXTRAS"] != DBNull.Value) entidad.extras = Convert.ToDecimal(resultado["EXTRAS"]);
                            if (resultado["INTERES"] != DBNull.Value) entidad.interes = Convert.ToDecimal(resultado["INTERES"]);
                            if (resultado["MORA"] != DBNull.Value) entidad.mora = Convert.ToDecimal(resultado["MORA"]);
                            if (resultado["OTROS"] != DBNull.Value) entidad.otros = Convert.ToDecimal(resultado["OTROS"]);
                            if (resultado["SALDO_TOTAL"] != DBNull.Value) entidad.saldo_total = Convert.ToDecimal(resultado["SALDO_TOTAL"]);
                            if (resultado["TIPO_PRODUCTO"] != DBNull.Value) entidad.tipo_producto = Convert.ToInt32(resultado["TIPO_PRODUCTO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);

                            if (entidad.forma_pago != "")
                            {
                                if (entidad.forma_pago == "1" || entidad.forma_pago == "C")
                                {
                                    entidad.forma_pago = "C";
                                }
                                else
                                {
                                    entidad.forma_pago = "N";
                                }
                            }

                            lstProductMora.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstProductMora;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ConsultarDetalleMoraPersona", ex);
                        return null;
                    }
                }
            }
        }




    }
}