using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Riesgo.Entities;
using System.Globalization;

namespace Xpinn.Riesgo.Data
{
  public  class HistoricoSegPerfilData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor para el acceso a base de datos
        /// </summary>
        public HistoricoSegPerfilData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public List<HistoricoSegPersona> ListarPersonaHistorico(HistoricoSegPersona pHistoricoSegPersona, string nombre, string apellido, string iden, string perfil, string segR, Usuario vUsuario)
        {
            DbDataReader resultado;
           List<HistoricoSegPersona> lsHistoricopersonas = new List<HistoricoSegPersona>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        sql = @"SELECT persona.COD_PERSONA,persona.PRIMER_NOMBRE,persona.SEGUNDO_NOMBRE,persona.PRIMER_APELLIDO,persona.IDENTIFICACION,
                                HISTORICO_SEGMENTACION.FECHACIERRE, SP.PERFIL AS PERFIL_RIESGO, S.NOMBRE AS PERFIL_SEGMENTO, HISTORICO_SEGMENTACION.CALIFICACION,
                                SP.NOMBRE AS SEGMENTO_PRO, SC.NOMBRE AS SEGMENTO_CAN, SJ.NOMBRE AS SEGMENTO_JUR
                                FROM HISTORICO_SEGMENTACION 
                                INNER JOIN persona ON persona.COD_PERSONA = HISTORICO_SEGMENTACION.CODIGOPERSONA
                                INNER JOIN GR_SEGMENTACION_PERFIL SP ON persona.COD_PERSONA = SP.COD_PERSONA
                                LEFT JOIN SEGMENTOS S ON S.CODSEGMENTO = HISTORICO_SEGMENTACION.SEGMENTOASO
                                LEFT JOIN SEGMENTOS sp on sp.codsegmento = HISTORICO_SEGMENTACION.segmentopro
                                LEFT JOIN SEGMENTOS sc on sc.codsegmento = HISTORICO_SEGMENTACION.segmentocan
                                LEFT JOIN SEGMENTOS sj on sj.codsegmento = HISTORICO_SEGMENTACION.segmentojur  " + ObtenerFiltro(pHistoricoSegPersona);
                        if (nombre!="") { sql += "AND persona.PRIMER_NOMBRE LIKE '%"+nombre+"%'"; }
                        if (apellido!="") { sql += "AND persona.PRIMER_APELLIDO LIKE '%" + apellido + "%'"; }
                        if (iden!= "") { sql += "AND persona.IDENTIFICACION LIKE '%" + iden + "%'"; }
                        if (perfil != "") { sql += "AND SP.PERFIL LIKE '%" + perfil + "%'"; }
                        if (segR != "") { sql += "AND S.CODSEGMENTO LIKE '%" + segR + "%'"; }
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            HistoricoSegPersona entidad = new HistoricoSegPersona();

                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["PRIMER_NOMBRE"] != DBNull.Value) entidad.primer_nombre = Convert.ToString(resultado["PRIMER_NOMBRE"]);
                            if (resultado["SEGUNDO_NOMBRE"] != DBNull.Value) entidad.segundo_nombre = Convert.ToString(resultado["SEGUNDO_NOMBRE"]);
                            if (resultado["PRIMER_APELLIDO"] != DBNull.Value) entidad.primer_apellido = Convert.ToString(resultado["PRIMER_APELLIDO"]);
                            if (resultado["FECHACIERRE"] != DBNull.Value) entidad.FECHACIERRE = Convert.ToDateTime(resultado["FECHACIERRE"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["PERFIL_SEGMENTO"] != DBNull.Value) entidad.segmentoActual = Convert.ToString(resultado["PERFIL_SEGMENTO"]);
                            if (resultado["SEGMENTO_PRO"] != DBNull.Value) entidad.segmento_pro = Convert.ToString(resultado["SEGMENTO_PRO"]);
                            if (resultado["SEGMENTO_CAN"] != DBNull.Value) entidad.segmento_can = Convert.ToString(resultado["SEGMENTO_JUR"]);
                            if (resultado["SEGMENTO_JUR"] != DBNull.Value) entidad.segmento_jur = Convert.ToString(resultado["PERFIL_SEGMENTO"]);
                            if (resultado["PERFIL_RIESGO"] !=DBNull.Value) entidad.Perfil_riesgo = Convert.ToString(resultado["PERFIL_RIESGO"]);
                            if (resultado["CALIFICACION"] != DBNull.Value) entidad.calificacion_segmento = Convert.ToInt32(resultado["CALIFICACION"]);
                            switch (Convert.ToInt32(entidad.calificacion_segmento))
                            {
                                case 1: entidad.calificacion = "BAJO"; break;
                                case 2: entidad.calificacion = "MODERADO"; break;
                                case 3: entidad.calificacion = "ALTO"; break;
                                case 4: entidad.calificacion = "EXTREMO"; break;
                            }
                            lsHistoricopersonas.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lsHistoricopersonas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("HistoricoSegPerfilData", "ListarPersonaHistorico", ex);
                        return null;
                    }
                }
            }
        }
        public HistoricoSegPersona Captaciones(Int64 cod_persona, string Fecha_cierre, Usuario vUsuario)
        {
            DbDataReader resultado;
            HistoricoSegPersona vHistoricoSegPersona = new HistoricoSegPersona();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            Fecha_cierre = "TO_DATE('" + Convert.ToDateTime(Fecha_cierre).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                        else
                            Fecha_cierre = "'" + Fecha_cierre + "'";
                        string sql = @"
                                      WITH captacion AS (
                                        SELECT T.COD_PERSONA, R.TIPO_MOV, COUNT(Distinct T.COD_OPE) AS NUMERO, SUM(T.VALOR) AS VALOR
                                        FROM TRAN_APORTE T JOIN OPERACION O ON O.COD_OPE = T.COD_OPE JOIN TIPO_TRAN R ON T.TIPO_TRAN = R.TIPO_TRAN
                                        WHERE TRUNC(O.FECHA_OPER) > DATEPRIMERDIADELMES(" + Fecha_cierre + ") AND TRUNC(O.FECHA_OPER) <= " + Fecha_cierre + @"  
                                        AND T.COD_PERSONA = '" + cod_persona + @"' GROUP BY T.COD_PERSONA, R.TIPO_MOV
                                        UNION ALL
                                        SELECT T.COD_CLIENTE, R.TIPO_MOV, COUNT(Distinct T.COD_OPE) AS NUMERO, SUM(T.VALOR) AS VALOR
                                        FROM TRAN_AHORRO T JOIN OPERACION O ON O.COD_OPE = T.COD_OPE JOIN TIPO_TRAN R ON T.TIPO_TRAN = R.TIPO_TRAN
                                        WHERE TRUNC(O.FECHA_OPER) > DATEPRIMERDIADELMES(" + Fecha_cierre + ") AND TRUNC(O.FECHA_OPER) <= " + Fecha_cierre + @"  
                                        AND T.COD_CLIENTE = '" + cod_persona + @"' GROUP BY T.COD_CLIENTE, R.TIPO_MOV  
                                        UNION ALL
                                        SELECT T.COD_CLIENTE, R.TIPO_MOV, COUNT(Distinct T.COD_OPE) AS NUMERO, SUM(T.VALOR) AS VALOR 
                                        FROM TRAN_PROGRAMADO T JOIN OPERACION O ON O.COD_OPE = T.COD_OPE JOIN TIPO_TRAN R ON T.TIPO_TRAN = R.TIPO_TRAN
                                        WHERE TRUNC(O.FECHA_OPER) > DATEPRIMERDIADELMES(" + Fecha_cierre + ") AND TRUNC(O.FECHA_OPER) <= " + Fecha_cierre + @"  
                                        AND T.COD_CLIENTE = '" + cod_persona + @"' GROUP BY T.COD_CLIENTE, R.TIPO_MOV 
                                        UNION ALL
                                        SELECT T.COD_CLIENTE, R.TIPO_MOV, COUNT(Distinct T.COD_OPE) AS NUMERO, SUM(T.VALOR) AS VALOR
                                        FROM TRAN_CDAT T JOIN OPERACION O ON O.COD_OPE = T.COD_OPE JOIN TIPO_TRAN R ON T.TIPO_TRAN = R.TIPO_TRAN
                                        WHERE TRUNC(O.FECHA_OPER) > DATEPRIMERDIADELMES(" + Fecha_cierre + ") AND TRUNC(O.FECHA_OPER) <= " + Fecha_cierre + @"  
                                        AND T.COD_CLIENTE = '" + cod_persona + @"' GROUP BY T.COD_CLIENTE, R.TIPO_MOV 
                                    )
                                    SELECT TIPO_MOV, NUMERO, SUM(VALOR) FROM captacion GROUP BY TIPO_MOV, NUMERO";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        vHistoricoSegPersona.MontoCPD = 0;
                        vHistoricoSegPersona.NumCPD = 0;
                        vHistoricoSegPersona.MontoCPC = 0;
                        vHistoricoSegPersona.NumCPC = 0;
                        while (resultado.Read())
                        {
                            if (resultado["TIPO_MOV"] != DBNull.Value && Convert.ToUInt32(resultado["TIPO_MOV"]) == 1)
                            {
                                vHistoricoSegPersona.MontoCPD = vHistoricoSegPersona.MontoCPD + Convert.ToInt32(resultado["SUM(VALOR)"]);
                                vHistoricoSegPersona.NumCPD = vHistoricoSegPersona.NumCPD + Convert.ToInt32(resultado["NUMERO"]);
                            }
                            else if (resultado["TIPO_MOV"] != DBNull.Value && Convert.ToUInt32(resultado["TIPO_MOV"]) == 2)
                            {
                                vHistoricoSegPersona.MontoCPC = vHistoricoSegPersona.MontoCPC + Convert.ToInt32(resultado["SUM(VALOR)"]);
                                vHistoricoSegPersona.NumCPC = vHistoricoSegPersona.NumCPC + Convert.ToInt32(resultado["NUMERO"]);
                            }
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return vHistoricoSegPersona;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("HistoricoSegPerfilData", "Captaciones", ex);
                        return null;
                    }
                }
            }
        }
        public HistoricoSegPersona Colocaciones(Int64 cod_persona, string Fecha_cierre, Usuario vUsuario)
        {
            DbDataReader resultado;
            HistoricoSegPersona vHistoricoSegPersona = new HistoricoSegPersona();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            Fecha_cierre = "TO_DATE('" + Convert.ToDateTime(Fecha_cierre).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                        else
                            Fecha_cierre = "'" + Fecha_cierre + "'";
                        string sql = @"
                                    WITH colocacion AS
                                    (
                                        SELECT T.COD_CLIENTE, R.TIPO_MOV, COUNT(Distinct T.COD_OPE) AS NUMERO, SUM(T.VALOR) AS VALOR
                                        FROM TRAN_CRED T JOIN OPERACION O ON O.COD_OPE = T.COD_OPE JOIN TIPO_TRAN R ON T.TIPO_TRAN = R.TIPO_TRAN
                                        WHERE TRUNC(O.FECHA_OPER) > DATEPRIMERDIADELMES("+ Fecha_cierre + ") AND TRUNC(O.FECHA_OPER) <= " + Fecha_cierre + @"
                                        AND T.COD_CLIENTE = '" + cod_persona +  @"' GROUP BY T.COD_CLIENTE, R.TIPO_MOV 
                                        UNION ALL
                                        SELECT T.COD_CLIENTE, R.TIPO_MOV, COUNT(Distinct T.COD_OPE), SUM(T.VALOR) FROM TRAN_SERVICIOS T JOIN OPERACION O ON O.COD_OPE = T.COD_OPE  JOIN TIPO_TRAN R ON T.TIPO_TRAN = R.TIPO_TRAN 
                                        WHERE TRUNC(O.FECHA_OPER) > DATEPRIMERDIADELMES(" + Fecha_cierre + ") AND TRUNC(O.FECHA_OPER) <= " + Fecha_cierre + "  AND T.COD_CLIENTE = " + cod_persona + @" GROUP BY T.COD_CLIENTE, R.TIPO_MOV
                                    )
                                    SELECT TIPO_MOV, NUMERO, SUM(VALOR) FROM colocacion GROUP BY TIPO_MOV, NUMERO";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        vHistoricoSegPersona.MontoCLD = 0;
                        vHistoricoSegPersona.NumCLD = 0;
                        vHistoricoSegPersona.MontoCLC = 0;
                        vHistoricoSegPersona.NumCLC = 0;
                        while (resultado.Read())
                        {
                            if (resultado["TIPO_MOV"] != DBNull.Value && Convert.ToUInt32(resultado["TIPO_MOV"]) == 1) { 
                                vHistoricoSegPersona.MontoCLD = vHistoricoSegPersona.MontoCLD + Convert.ToInt32(resultado["SUM(VALOR)"]);
                                vHistoricoSegPersona.NumCLD = vHistoricoSegPersona.NumCLD + Convert.ToInt32(resultado["NUMERO"]);
                            }else if (resultado["TIPO_MOV"] != DBNull.Value && Convert.ToUInt32(resultado["TIPO_MOV"]) == 2) { 
                                vHistoricoSegPersona.MontoCLC = vHistoricoSegPersona.MontoCLC + Convert.ToInt32(resultado["SUM(VALOR)"]);
                                vHistoricoSegPersona.NumCLC = vHistoricoSegPersona.NumCLC + Convert.ToInt32(resultado["NUMERO"]);}
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return vHistoricoSegPersona;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("HistoricoSegPerfilData", "Colocaciones", ex);
                        return null;
                    }
                }
            }
        }

        public HistoricoSegPersona ConsultarPersonaHistorico(Int64 cod_persona, string Fecha_cierre, Usuario vUsuario)
        {
            DbDataReader resultado;
            HistoricoSegPersona vHistoricoSegPersona = new HistoricoSegPersona();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        String.Format("{0:M/d/yyyy}", Fecha_cierre);

                        string sql = @"SELECT DISTINCT persona.COD_PERSONA, persona.PRIMER_NOMBRE, persona.SEGUNDO_NOMBRE, persona.PRIMER_APELLIDO, persona.IDENTIFICACION, persona.DIRECCION, persona.TELEFONO, persona.EMAIL,
                                        HISTORICO_SEGMENTACION.EDAD, HISTORICO_SEGMENTACION.INGRESOSMENSUALES, HISTORICO_SEGMENTACION.ESTRATO, OFICINA.NOMBRE as NOM_OFI, OFICINA.DIRECCION as DIR_OFI, OFICINA.TELEFONO as TEL_OFI,
                                        GR_SEGMENTACION_PERFIL.PERFIL as PERFIL_RIESGO, HISTORICO_SEGMENTACION.VALOROPERACIONESMES, HISTORICO_SEGMENTACION.ANALISISOFICIALCUMPLIMIENTO, HISTORICO_SEGMENTACION.OPERACIONESPRODUCTOSALMES, SEGMENTOS.NOMBRE AS NOMSEGMENTO,
                                        act.DESCRIPCION AS ACTIVIDAD, persona.CELULAR, HISTORICO_SEGMENTACION.CALIFICACION
                                        FROM PERSONA 
                                        INNER JOIN HISTORICO_SEGMENTACION ON persona.COD_PERSONA = HISTORICO_SEGMENTACION.CODIGOPERSONA 
                                        LEFT JOIN GR_SEGMENTACION_PERFIL ON GR_SEGMENTACION_PERFIL.COD_PERSONA = HISTORICO_SEGMENTACION.CODIGOPERSONA  
                                        LEFT JOIN OFICINA ON persona.COD_OFICINA = OFICINA.COD_OFICINA  
                                        LEFT JOIN SEGMENTOS ON SEGMENTOS.CODSEGMENTO = HISTORICO_SEGMENTACION.SEGMENTOACTUAL
                                        LEFT JOIN ACTIVIDAD act ON act.CODACTIVIDAD = persona.CODACTIVIDAD
                                        WHERE PERSONA.COD_PERSONA = " + cod_persona + " AND HISTORICO_SEGMENTACION.CODIGOPERSONA = " + cod_persona + " AND HISTORICO_SEGMENTACION.FECHACIERRE = TO_DATE('"+ String.Format("{0:M/d/yyyy}", Fecha_cierre) + "','DD/MM/YYYY')";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_PERSONA"] != DBNull.Value) vHistoricoSegPersona.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) vHistoricoSegPersona.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["PRIMER_NOMBRE"] != DBNull.Value) vHistoricoSegPersona.primer_nombre = Convert.ToString(resultado["PRIMER_NOMBRE"]);
                            if (resultado["SEGUNDO_NOMBRE"] != DBNull.Value) vHistoricoSegPersona.segundo_nombre = Convert.ToString(resultado["SEGUNDO_NOMBRE"]);
                            if (resultado["PRIMER_APELLIDO"] != DBNull.Value) vHistoricoSegPersona.primer_apellido = Convert.ToString(resultado["PRIMER_APELLIDO"]);
                            if (resultado["DIRECCION"] != DBNull.Value) vHistoricoSegPersona.Direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) vHistoricoSegPersona.Telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["CELULAR"] != DBNull.Value) vHistoricoSegPersona.Celular = Convert.ToString(resultado["CELULAR"]);
                            if (resultado["EMAIL"] != DBNull.Value) vHistoricoSegPersona.Email = Convert.ToString(resultado["EMAIL"]);
                            if (resultado["EDAD"] != DBNull.Value) vHistoricoSegPersona.Edad = Convert.ToInt64(resultado["EDAD"]);
                            if (resultado["INGRESOSMENSUALES"] != DBNull.Value) vHistoricoSegPersona.Ingresosmensuales = Convert.ToInt64(resultado["INGRESOSMENSUALES"]);
                            if (resultado["ESTRATO"] != DBNull.Value) vHistoricoSegPersona.Estrato = Convert.ToInt64(resultado["ESTRATO"]);
                            if (resultado["NOM_OFI"] != DBNull.Value) vHistoricoSegPersona.Nom_ofi = Convert.ToString(resultado["NOM_OFI"]);
                            if (resultado["DIR_OFI"] != DBNull.Value) vHistoricoSegPersona.Dir_ofi = Convert.ToString(resultado["DIR_OFI"]);
                            if (resultado["TEL_OFI"] != DBNull.Value) vHistoricoSegPersona.Tel_ofi = Convert.ToString(resultado["TEL_OFI"]);
                            if (resultado["PERFIL_RIESGO"] != DBNull.Value) vHistoricoSegPersona.Perfil_riesgo = Convert.ToString(resultado["PERFIL_RIESGO"]);
                            if (resultado["VALOROPERACIONESMES"] != DBNull.Value) vHistoricoSegPersona.Valor_operacionMes = Convert.ToInt64(resultado["VALOROPERACIONESMES"]);
                            if (resultado["OPERACIONESPRODUCTOSALMES"] != DBNull.Value) vHistoricoSegPersona.Numero_operacionMes = Convert.ToInt64(resultado["OPERACIONESPRODUCTOSALMES"]);
                            if (resultado["ACTIVIDAD"] != DBNull.Value) vHistoricoSegPersona.Actividad_Eco = Convert.ToString(resultado["ACTIVIDAD"]);
                            if (resultado["NOMSEGMENTO"] != DBNull.Value) vHistoricoSegPersona.segmentoActual = Convert.ToString(resultado["NOMSEGMENTO"]);
                            if (resultado["ANALISISOFICIALCUMPLIMIENTO"] != DBNull.Value) vHistoricoSegPersona.analisisCumplimiento = Convert.ToString(resultado["ANALISISOFICIALCUMPLIMIENTO"]);
                            if (resultado["CALIFICACION"] != DBNull.Value) vHistoricoSegPersona.calificacion = Convert.ToString(resultado["CALIFICACION"]);
                            switch (Convert.ToInt32(vHistoricoSegPersona.calificacion))
                            {
                                case 1: vHistoricoSegPersona.calificacion = "BAJO"; break;
                                case 2: vHistoricoSegPersona.calificacion = "MODERADO"; break;
                                case 3: vHistoricoSegPersona.calificacion = "ALTO"; break;
                                case 4: vHistoricoSegPersona.calificacion = "EXTREMO"; break;
                            }
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return vHistoricoSegPersona;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("HistoricoSegPerfilData", "ConsultarPersonaHistorico", ex);
                        return null;
                    }
                }
            }
        }


        public List<HistoricoSegPersona> ListarPersonaAnalisis(Int64 pCodPersona, string fecha_cierre, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<HistoricoSegPersona> lsHistoricopersonas = new List<HistoricoSegPersona>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "";
                        string sfecha = "TO_DATE('" + fecha_cierre + "', '" + conf.ObtenerFormatoFecha() + "')";
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            sql = @"Select v.fecha_historico, 
                                    Sum(Case CodigoProducto(v.tipo_producto) When 2 Then 0 When 4 Then 0 Else v.saldo End) AS captaciones,
                                    Sum(Case CodigoProducto(v.tipo_producto) When 2 Then v.saldo When 4 Then v.saldo Else 0 End) AS colocaciones
                                    From vsarlaft_producto v
                                    Where v.cod_persona = " + pCodPersona + @" And v.fecha_historico >= " + sfecha + @" - 365 And v.fecha_historico <= " + sfecha + @"
                                    Group by v.fecha_historico 
                                    Order by 1";
                        else
                            sql = @"Select v.fecha_historico, 
                                    Sum(Case v.tipo_producto When 'Créditos' Then 0 When 'Servicios' Then 0 Else v.saldo End) AS captaciones,
                                    Sum(Case v.tipo_producto When 'Créditos' Then v.saldo When 'Servicios' Then v.saldo Else 0 End) AS colocaciones
                                    From vsarlaft_producto v
                                    Where v.cod_persona = " + pCodPersona + @" 
                                    Group by v.fecha_historico 
                                    Order by 1";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            HistoricoSegPersona entidad = new HistoricoSegPersona();

                            if (resultado["FECHA_HISTORICO"] != DBNull.Value) entidad.fecha_historico = Convert.ToDateTime(resultado["FECHA_HISTORICO"]);
                            if (resultado["CAPTACIONES"] != DBNull.Value) entidad.captaciones = Convert.ToInt64(resultado["CAPTACIONES"]);
                            if (resultado["COLOCACIONES"] != DBNull.Value) entidad.colocaciones = Convert.ToInt64(resultado["COLOCACIONES"]);

                            lsHistoricopersonas.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lsHistoricopersonas;
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }
            }
        }

        public List<HistoricoSegPersona> ListarHistorialSegementacion(Int64 cod_persona, Usuario vUsuario)
        {
            return ListarHistorialSegementacion(cod_persona, "", vUsuario);
        }

        public List<HistoricoSegPersona> ListarHistorialSegementacion(Int64 cod_persona, string fecha_cierre, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<HistoricoSegPersona> lsHistorialSegementacion = new List<HistoricoSegPersona>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"SELECT H.FECHACIERRE, H.CALIFICACION, S.NOMBRE FROM HISTORICO_SEGMENTACION H
                                       LEFT JOIN SEGMENTOS S ON S.CODSEGMENTO = H.SEGMENTOACTUAL
                                       WHERE CODIGOPERSONA = " + cod_persona + (fecha_cierre.Trim() == "" ? "" : " AND H.FECHACIERRE <= TO_DATE('" + fecha_cierre.Trim() + "', '" + conf.ObtenerFormatoFecha() + "')") + " ORDER BY FECHACIERRE DESC";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            HistoricoSegPersona entidad = new HistoricoSegPersona();

                            if (resultado["FECHACIERRE"] != DBNull.Value) entidad.fecha_segemento = Convert.ToDateTime(resultado["FECHACIERRE"]);
                            if (resultado["CALIFICACION"] != DBNull.Value) entidad.calificacion_segmento = Convert.ToInt32(resultado["CALIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.segmento_perfil = Convert.ToString(resultado["NOMBRE"]);
                            switch (Convert.ToInt32(entidad.calificacion_segmento))
                            {
                                case 1: entidad.calificacion = "BAJO"; break;
                                case 2: entidad.calificacion = "MODERADO"; break;
                                case 3: entidad.calificacion = "ALTO"; break;
                                case 4: entidad.calificacion = "EXTREMO"; break;
                            }

                            lsHistorialSegementacion.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lsHistorialSegementacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("HistoricoSegPerfilData", "ListarHistorialSegementacion", ex);
                        return null;
                    }
                }
            }
        }

    }
}
