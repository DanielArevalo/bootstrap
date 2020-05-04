using System;
using System.Collections.Generic;
using Xpinn.Util;
using System.Data;
using System.Data.Common;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Data
{
    public class EstadoCuentaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public EstadoCuentaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public EstadoCuenta enviarAlertas(EstadoCuenta vAlerta, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter numero_cuenta = cmdTransaccionFactory.CreateParameter();
                        numero_cuenta.ParameterName = "P_COD_PERSONA";
                        numero_cuenta.Value = vAlerta.Codigo;
                        numero_cuenta.Direction = ParameterDirection.Input;
                        numero_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(numero_cuenta);

                        DbParameter cod_persona = cmdTransaccionFactory.CreateParameter();
                        cod_persona.ParameterName = "P_FECHA";
                        cod_persona.Value = vAlerta.fecha_inicio;
                        cod_persona.Direction = ParameterDirection.Input;
                        cod_persona.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(cod_persona);

                        DbParameter P_ALERTA = cmdTransaccionFactory.CreateParameter();
                        P_ALERTA.ParameterName = "P_ALERTA";
                        P_ALERTA.Value = RepetirString(" ", 200);
                        P_ALERTA.Direction = ParameterDirection.Output;
                        P_ALERTA.DbType = DbType.AnsiString;
                        P_ALERTA.Size = 200;
                        cmdTransaccionFactory.Parameters.Add(P_ALERTA);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_AS_ALERTAS";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        vAlerta.linea = Convert.ToString(P_ALERTA.Value);
                        return vAlerta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EstadoCuentaData", "enviarAlertas", ex);
                        return null;
                    }
                }
            }
        }

        public EstadoCuenta enviarAlertas(EstadoCuenta vAlerta, Usuario vUsuario, int pCod=0)
        {
            vAlerta.linea = "";
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        int laux = 0;
                        string fecha = dbConnectionFactory.TipoConexion() == "ORACLE" ? " To_Date('" + vAlerta.fecha_inicio.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') " : " '" + vAlerta.fecha_inicio.ToString(conf.ObtenerFormatoFecha()) + "' ";                         
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        // Validando aportes en mora
                        laux = 0;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = @"SELECT COUNT(*) AS NUMERO FROM APORTE A INNER JOIN LINEAAPORTE LA ON LA.COD_LINEA_APORTE = A.COD_LINEA_APORTE
                                                              WHERE (LA.ALERTA = 0 or LA.ALERTA IS NULL) AND a.cod_persona = " + vAlerta.Codigo + " AND a.estado = 1 AND a.fecha_proximo_pago < " + fecha + "AND LA.TIPO_APORTE NOT IN (4)";
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {                            
                            if (resultado["NUMERO"] != DBNull.Value) laux = Convert.ToInt32(resultado["NUMERO"]);
                            if (laux > 0)
                                vAlerta.linea += "La persona tiene mora en aportes-";
                        }
                        // Validando codeudor de créditos en mora
                        laux = 0;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = @"SELECT COUNT(*) AS NUMERO FROM credito c WHERE c.cod_deudor = " + vAlerta.Codigo + " AND c.saldo_capital != 0 AND c.estado = 'C' AND c.fecha_proximo_pago < " + fecha;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["NUMERO"] != DBNull.Value) laux = Convert.ToInt32(resultado["NUMERO"]);
                            if (laux > 0)
                                vAlerta.linea += "Tiene créditos en mora-";
                        }
                        // Validando codeudor de créditos en mora
                        laux = 0;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = @"SELECT COUNT(*) AS NUMERO FROM credito c INNER JOIN codeudores d ON c.numero_radicacion = d.numero_radicacion WHERE d.codpersona = " + vAlerta.Codigo + " AND c.saldo_capital != 0 AND c.fecha_proximo_pago < " + fecha;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["NUMERO"] != DBNull.Value) laux = Convert.ToInt32(resultado["NUMERO"]);
                            if (laux > 0)
                                vAlerta.linea += "Es codeudora de créditos en mora-";
                        }
                        // Validando Ahorro Programado
                        laux = 0;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = @"SELECT COUNT(*) AS NUMERO FROM ahorro_programado c WHERE c.cod_persona = " + vAlerta.Codigo + " AND c.saldo != 0 AND c.fecha_proximo_pago < " + fecha + " AND c.plazo < c.cuotas_pagadas";
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["NUMERO"] != DBNull.Value) laux = Convert.ToInt32(resultado["NUMERO"]);
                            if (laux > 0)
                                vAlerta.linea += "Tiene Ahorro Programado en mora-";
                        }


                        // Validando Servicios
                        laux = 0;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = @"select COUNT(*) NUMERO from SERVICIOS  s WHERE s.SALDO != 0 and s.cod_persona = " + vAlerta.Codigo + " AND  s.fecha_proximo_pago < " + fecha + " AND CUOTAS_PENDIENTES > 0";
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["NUMERO"] != DBNull.Value) laux = Convert.ToInt32(resultado["NUMERO"]);
                            if (laux > 0)
                                vAlerta.linea += "Tiene Servicios en mora-";
                        }



                        laux = 0;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = @"select  count(*) as Numero from credito c  inner join CUOTASEXTRAS ce on 
                                                            c.numero_radicacion = ce.numero_radicacion
                                                            where c.cod_deudor = " + vAlerta.Codigo + " and  ce.saldo_capital > 0  and c.saldo_capital > 0 and ce.FECHA_PAGO  <= " + fecha + " ";
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["NUMERO"] != DBNull.Value) laux = Convert.ToInt32(resultado["NUMERO"]);
                            if (laux > 0)
                                vAlerta.linea += "Tiene Cuotas Extras en Mora-";
                        }

                        //Alerta estado Tarjeta 0=Pendiente, 1=Activa, 2=Inactiva, 3=Bloqueda
                        laux = 0;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = @"select T.ESTADO as NUMERO from TARJETA T where T.COD_PERSONA = " + vAlerta.Codigo + "and T.TIPO_CUENTA = 2";
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["NUMERO"] != DBNull.Value) laux = Convert.ToInt32(resultado["NUMERO"]);
                            if (laux == 3)
                                vAlerta.linea += "Tarjeta Bloqueada-";
                        }

                        //Alerta estado cupo Crédito 0 = activo, 1 = bloqueado
                        laux = 0;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = @"select ESTADO_SALDO as NUMERO from TARJETA T inner join CREDITO C ON T.NUMERO_CUENTA = TO_CHAR(C.NUMERO_RADICACION) where T.COD_PERSONA = " + vAlerta.Codigo + " AND C.ESTADO = 'C'";
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["NUMERO"] != DBNull.Value) laux = Convert.ToInt32(resultado["NUMERO"]);
                            if (laux > 0)
                                vAlerta.linea += "Cupo Bloqueado (Crédito)-";
                        }




                        connection.Close();
                        return vAlerta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EstadoCuentaData", "enviarAlertas", ex);
                        return null;
                    }
                }
            }
        }

        public string RepetirString(string pdato, int pveces)
        {
            if (pdato == null) pdato = "";
            string linea = pdato;
            for (int i=1;i<=pveces; i++)
            {
                pdato = linea + pdato;
            }
            return linea;
        }


        public List<EstadoCuenta> consultarServicios(EstadoCuenta pCodPersona, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<EstadoCuenta> lstextract = new List<EstadoCuenta>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT servicios.*, lineasservicios.nombre, planservicios.nombre AS nombre_plan, 
                                        CASE SERVICIOS.ESTADO WHEN 'S' THEN 'Solicitado' WHEN 'A' THEN 'Aprobado' WHEN 'C' THEN  'Activado' WHEN 'T' THEN 'Terminado'   WHEN 'N' THEN 'Negado-Borrado' end as nom_estado,
                                        CASE SERVICIOS.FORMA_PAGO WHEN '1' THEN 'Caja' WHEN '2' THEN 'Nomina' END as FORMA_PAGO_DESC,
                                        CALCULAR_DIASMORA_SERVICIO(NUMERO_SERVICIO) as Dias_Mora,
                                        (Select sum(saldo) From pendiente_servicio p Where p.numero_servicio = servicios.numero_servicio) As valor_pendiente
                                        FROM servicios INNER JOIN lineasservicios ON lineasservicios.cod_linea_servicio=servicios.cod_linea_servicio
                                        LEFT JOIN planservicios ON servicios.cod_linea_servicio = planservicios.cod_linea_servicio and servicios.COD_PLAN_SERVICIO = planservicios.COD_PLAN_SERVICIO
                                        WHERE nvl(lineasservicios.ocultar_informacion , 0) not in (1) and servicios.cod_persona =" + pCodPersona.Codigo.ToString();

                        if (!pCodPersona.cerrado)
                        {
                            sql += " and servicios.estado != 'T'   and servicios.estado != 'N' ";
                        }

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            EstadoCuenta entidad = new EstadoCuenta();
                            if (resultado["NUMERO_SERVICIO"] != DBNull.Value) entidad.num_servicio = Convert.ToInt32(resultado["NUMERO_SERVICIO"]);
                            if (resultado["FECHA_SOLICITUD"] != DBNull.Value) entidad.fecha_solicitud = Convert.ToDateTime(resultado["FECHA_SOLICITUD"]);
                            if (resultado["FECHA_INICIO_VIGENCIA"] != DBNull.Value) entidad.fecha_inicio_vigencia = Convert.ToDateTime(resultado["FECHA_INICIO_VIGENCIA"]);
                            if (resultado["FECHA_FINAL_VIGENCIA"] != DBNull.Value) entidad.fecha_final_vigencia = Convert.ToDateTime(resultado["FECHA_FINAL_VIGENCIA"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["FORMA_PAGO_DESC"] != DBNull.Value) entidad.forma_pago = Convert.ToString(resultado["FORMA_PAGO_DESC"]);
                            if (resultado["VALOR_TOTAL"] != DBNull.Value) entidad.valor = Convert.ToInt32(resultado["VALOR_TOTAL"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.saldo = Convert.ToInt32(resultado["SALDO"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.cuota = Convert.ToInt32(resultado["VALOR_CUOTA"]);
                            if (resultado["nombre_plan"] != DBNull.Value) entidad.plan = Convert.ToString(resultado["nombre_plan"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["NOM_ESTADO"] != DBNull.Value) entidad.nom_estado = Convert.ToString(resultado["NOM_ESTADO"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.cuotas = Convert.ToDecimal(resultado["NUMERO_CUOTAS"]);
                            if (resultado["FECHA_APROBACION"] != DBNull.Value) entidad.fecha_aprobacion = Convert.ToDateTime(resultado["FECHA_APROBACION"]);
                            if (resultado["Dias_Mora"] != DBNull.Value) entidad.Dias_Mora = Convert.ToInt32(resultado["Dias_Mora"]);
                            if (resultado["valor_pendiente"] != DBNull.Value) entidad.valor_pendiente = Convert.ToInt64(resultado["valor_pendiente"]);
                            if (resultado["FECHA_ACTIVACION"] != DBNull.Value)
                            {
                                DbDataReader resultado1;
                                cmdTransaccionFactory.CommandType = CommandType.Text;
                                cmdTransaccionFactory.CommandText = "Select cod_servicio_fijo, cod_servicio_adicionales from lineas_telefonicas where cod_servicio_fijo in (" + entidad.num_servicio + ") or cod_servicio_adicionales in (" + entidad.num_servicio + ")";
                                resultado1 = cmdTransaccionFactory.ExecuteReader();
                                if (resultado1.Read())
                                {
                                    if (resultado1["COD_SERVICIO_FIJO"] != DBNull.Value) entidad.fecha_desembolso = Convert.ToDateTime(entidad.fecha_inicio_vigencia);
                                    if (resultado1["COD_SERVICIO_ADICIONALES"] != DBNull.Value) entidad.fecha_desembolso = Convert.ToDateTime(entidad.fecha_inicio_vigencia);
                                }
                                else
                                {
                                    if (resultado["FECHA_ACTIVACION"] != DBNull.Value) entidad.fecha_desembolso = Convert.ToDateTime(resultado["FECHA_ACTIVACION"]);
                                }

                            }        
                            lstextract.Add(entidad);                            
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstextract;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EstadoCuentaData", "consultarServicios", ex);
                        return null;
                    }
                }
            }
        }
        

        public List<EstadoCuenta> consultaracodeudado(Usuario pUsuario)
        {
            DbDataReader resultado;
            EstadoCuenta entidad = new EstadoCuenta();
            List<EstadoCuenta> lstextract = new List<EstadoCuenta>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT credito.*,servicios.* FROM CREDITO inner join servicios on servicios.estado=credito.estado";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt32(resultado["NUMERO_RADICACION"]);
                            if (resultado["NOMBRE_TITULAR"] != DBNull.Value) entidad.nombre_titular = Convert.ToString(resultado["NOMBRE_TITULAR"]);
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.monto = Convert.ToInt32(resultado["MONTO_APROBADO"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.saldo = Convert.ToInt32(resultado["SALDO"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.cuota = Convert.ToInt32(resultado["VALOR_CUOTA"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["VALOR_TOTAL"] != DBNull.Value) entidad.valor_mora = Convert.ToInt32(resultado["VALOR_TOTAL"]);

                            lstextract.Add(entidad);
                        }
                     
                        return lstextract;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EstadoCuentaData", "consultaracodeudado", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un dato de la tabla general para Creditos
        /// </summary>
        /// <param name="pId">identificador de General</param>
        /// <returns>Parametro consultada</returns>
        public string ConsultarParametroCredito(Usuario pUsuario)
        {
            DbDataReader resultado;
            string valor = "0";

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select valor FROM GENERAL WHERE CODIGO = 1";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["valor"] != DBNull.Value) valor = Convert.ToString(resultado["valor"]);
                        }

                        return valor;
                    }
                    catch 
                    {
                        return "0";
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene un dato de la tabla general para Aportes
        /// </summary>
        /// <param name="pId">identificador de General</param>
        /// <returns>Parametro consultada</returns>
        public string ConsultarParametroAportes(Usuario pUsuario)
        {
            DbDataReader resultado;
            string valor = "0";

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select valor FROM GENERAL WHERE CODIGO = 2";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["valor"] != DBNull.Value) valor = Convert.ToString(resultado["valor"]);
                        }

                        return valor;
                    }
                    catch 
                    {
                        return "0";
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un dato de la tabla general para Aportes
        /// </summary>
        /// <param name="pId">identificador de General</param>
        /// <returns>Parametro consultada</returns>
        public string ConsultarParametroAhorros(Usuario pUsuario)
        {
            DbDataReader resultado;
            string valor = "0";

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select valor FROM GENERAL WHERE CODIGO =3";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["valor"] != DBNull.Value) valor = Convert.ToString(resultado["valor"]);
                        }

                        return valor;
                    }
                    catch 
                    {
                        return "";
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un dato de la tabla general para Aportes
        /// </summary>
        /// <param name="pId">identificador de General</param>
        /// <returns>Parametro consultada</returns>
        public string ConsultarParametroServicios(Usuario pUsuario)
        {
            DbDataReader resultado;
            string valor="0";

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT valor FROM GENERAL WHERE codigo = 4";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["valor"] != DBNull.Value) valor = Convert.ToString(resultado["valor"]);
                        }

                        return valor;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EstadoCuentaData", "ConsultarParametroServicios", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene un dato de la tabla general para Ocultar COlumnas
        /// </summary>
        /// <param name="pId">identificador de General</param>
        /// <returns>Parametro consultada</returns>
        public int ConsultarParametroColumnas(Usuario pUsuario)
        {
            DbDataReader resultado;
            string valor = "0";

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select valor FROM GENERAL WHERE CODIGO =7";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["valor"] != DBNull.Value)valor= Convert.ToString(resultado["valor"]);
                            
                        }
                        try
                        {
                            return Convert.ToInt32(valor);
                        }
                        catch 
                        {
                            return 0;
                        }
                       
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EstadoCuentaData", "ConsultarParametroServicios", ex);
                        return 0;
                    }
                }
            }
        }

        public List<EstadoCuenta> ListarClientes(string filtro, DateTime pFechaCreacion, DateTime pFechaCorte, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<EstadoCuenta> lstClientes = new List<EstadoCuenta>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT p.cod_persona, p.identificacion, p.primer_nombre ||' '||p.segundo_nombre||' '||p.primer_apellido||' '||p.segundo_apellido as NombreCliente, p.email,
                          Max(trunc(H.Fecha_Consulta)) Fecha_Consulta, Max(trunc(h.fecha_envio)) fecha_envio
                          FROM persona p
                          LEFT JOIN Historial_Notificacion h on P.Cod_Persona = H.Cod_Persona
                          WHERE (p.cod_persona IN (SELECT ap.cod_persona FROM  aporte ap) OR
                          p.cod_persona IN(SELECT cr.cod_deudor FROM credito cr) OR
                          p.cod_persona IN(SELECT av.cod_persona FROM ahorro_vista av) OR
                          p.cod_persona IN(SELECT ap.cod_persona FROM ahorro_programado ap) OR
                          p.cod_persona IN(SELECT c.cod_persona FROM  cdat_titular c INNER JOIN cdat d ON c.codigo_cdat = d.codigo_cdat) OR
                          p.cod_persona IN(SELECT s.cod_persona FROM servicios s)) and p.estado = 'A' 
                          And p.fechacreacion <= TO_DATE('" + pFechaCreacion.ToShortDateString() + @"','DD/MM/YYYY')
                          And P.Cod_Persona Not In (select distinct cod_persona from Historial_Notificacion where Fecha_Consulta >= TO_DATE('" + pFechaCorte.ToShortDateString() + @"','DD/MM/YYYY')) " + filtro + @"
                          GROUP BY p.cod_persona, p.identificacion, p.primer_nombre ||' '||p.segundo_nombre||' '||p.primer_apellido||' '||p.segundo_apellido , p.email
                          ORDER BY p.cod_persona";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            EstadoCuenta estado = new EstadoCuenta();
                            if (resultado["COD_PERSONA"] != DBNull.Value) estado.Codigo = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["NOMBRECLIENTE"] != DBNull.Value) estado.nombre_titular = Convert.ToString(resultado["NOMBRECLIENTE"]);
                            if (resultado["EMAIL"] != DBNull.Value) estado.email = Convert.ToString(resultado["EMAIL"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) estado.identificacion = Convert.ToInt64(resultado["IDENTIFICACION"]);
                            if (resultado["Fecha_Consulta"] != DBNull.Value) estado.fecha_final = Convert.ToDateTime(resultado["Fecha_Consulta"]);
                            if (resultado["fecha_envio"] != DBNull.Value) estado.fecha_final_vigencia = Convert.ToDateTime(resultado["fecha_envio"]);

                            //if(estado.email != null && estado.email != "")
                            lstClientes.Add(estado);
                        }
                        return lstClientes;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EstadoCuentaData", "ListarClientes", ex);
                        return null;
                    }
                }
            }
        }
    }
}