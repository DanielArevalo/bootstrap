using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Xpinn.Util;
using Xpinn.Icetex.Entities;
using System.Configuration;

namespace Xpinn.Icetex.Data
{
    public class AprobacionData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;
        public AprobacionData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public DataTable ListarCreditosIcetex(string pFiltro, ref string pError, Usuario vUsuario)
        {
            DbDataReader resultado;
            DataTable dtReporte = new DataTable();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        /*
                        string sql = @"SELECT C.COD_PERSONA , CASE C.TIPO_BENEFICIARIO WHEN '0' THEN 'Asociado' WHEN '1' THEN 'Hijo del Asociado' WHEN '2' THEN 'Nieto del Asociado'
                                    WHEN '3' THEN 'Empleado' ELSE 'Asociado' END AS NOM_TIPO_BENEFICIARIO, C.IDENTIFICACION, T.DESCRIPCION AS TIPO_IDENTIFICACION,
                                    C.PRIMER_NOMBRE, C.SEGUNDO_NOMBRE, C.PRIMER_APELLIDO, C.SEGUNDO_APELLIDO, C.DIRECCION, C.TELEFONO, C.EMAIL, C.ESTRATO, 
                                    U.DESCRIPCION AS NOM_UNIVERSIDAD, P.DESCRIPCION AS NOM_PROGRAMA, 
                                    CASE C.TIPO_PROGRAMA WHEN 1 THEN 'Especialización (1 año)' WHEN 2 THEN 'Maestria (2 años)' else 'SIN DATOS' END AS NOM_TIPO_PROGRAMA,
                                    C.VALOR ,C.PERIODOS
                                    from Creditoicetex c inner join Tipoidentificacion t on C.Codtipoidentificacion = T.Codtipoidentificacion
                                    Inner Join Universidad U On U.Cod_Universidad = C.Cod_Universidad 
                                    INNER JOIN Programa P ON P.Cod_Programa = C.Cod_Programa and P.COD_UNIVERSIDAD = C.Cod_Universidad  " + pFiltro.ToString() + " ORDER BY C.NUMERO_CREDITO ";
                                    */

                        string sql = @"SELECT C.COD_PERSONA, c.fecha_solicitud AS B_FECHA_SOLICITUD, tipo_beneficiario AS C_TIPO_BENEFICIARIO, IDENTIFICACION ,
                                    CODTIPOIDENTIFICACION AS C_TIPO_IDENTIFICACION, c.primer_nombre, c.segundo_nombre, c.primer_apellido, c.segundo_apellido, 
                                    c.direccion, c.telefono, c.email, C.ESTRATO, U.DESCRIPCION AS NOM_UNIVERSIDAD, P.DESCRIPCION AS PROGRAMA, c.tipo_programa AS C_TIPO_PROGRAMA,
                                    c.valor, c.periodos as C_PERIODOS
                                    FROM CREDITOICETEX C
                                    Inner Join Universidad U On U.Cod_Universidad = C.Cod_Universidad 
                                    INNER JOIN Programa P ON P.Cod_Programa = C.Cod_Programa and P.COD_UNIVERSIDAD = C.Cod_Universidad " + pFiltro;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        
                        dtReporte.Clear();
                        Boolean bRes = TraerResultados(resultado, ref dtReporte, ref pError);
                        dbConnectionFactory.CerrarConexion(connection);
                        return dtReporte;
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                        return null;
                    }
                }
            }
        }


        public List<ListadosIcetex> ListarDocumentosIcetex(string pFiltro, ref string pError, Usuario vUsuario)
        {
            DbDataReader resultado;
            DataTable dtReporte = new DataTable();
            List<ListadosIcetex> lstIcetex = new List<ListadosIcetex>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT C.COD_CREDOC,T.DESCRIPCION,C.ARCHIVO, C.OBSERVACION
                                        FROM CREDITOICETEXDOC C INNER JOIN TIPO_DOCICETEX T ON T.COD_TIPO_DOC = C.COD_TIPO_DOC " + pFiltro.ToString() + " ORDER BY C.NUMERO_CREDITO ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        ListadosIcetex entidad;
                        while (resultado.Read())
                        {
                            entidad = new ListadosIcetex();
                            if (resultado["COD_CREDOC"] != DBNull.Value) entidad.codigo = Convert.ToInt64(resultado["COD_CREDOC"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["ARCHIVO"] != DBNull.Value) entidad.archivo = (byte[])resultado["ARCHIVO"];
                            if (resultado["OBSERVACION"] != DBNull.Value) entidad.observacion = Convert.ToString(resultado["OBSERVACION"]); 
                            lstIcetex.Add(entidad);
                        }
                        return lstIcetex;
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                        return null;
                    }
                }
            }
        }



        public int Estructura(DbDataReader resultado, ref string[] aColumnas, ref System.Type[] aTipos, ref string Error)
        {
            Error = "";
            DataTable schemaTable;
            schemaTable = resultado.GetSchemaTable();
            int numerocolumna = 0;
            foreach (DataRow myField in schemaTable.Rows)
            {
                string NombreColumna = "";
                string TipoColumna = "";
                System.Type Tipo = null;
                foreach (DataColumn myProperty in schemaTable.Columns)
                {
                    if (myProperty.ColumnName.Trim() == "ColumnName")
                        NombreColumna = myField[myProperty].ToString();
                    if (myProperty.ColumnName.Trim() == "DataType")
                    {
                        if (NombreColumna.ToUpper() == "COD_PERSONA")
                            TipoColumna = "System.Int64";
                        else
                            TipoColumna = myField[myProperty].ToString();
                        Tipo = System.Type.GetType(TipoColumna);
                    }
                }
                try
                {
                    Array.Resize(ref aColumnas, numerocolumna + 1);
                    aColumnas.SetValue(NombreColumna, numerocolumna);
                    Array.Resize(ref aTipos, numerocolumna + 1);
                    aTipos.SetValue(Tipo, numerocolumna);
                    numerocolumna = numerocolumna + 1;
                }
                catch (Exception ex)
                {
                    Error = ex.Message;
                    return 0;
                }
            }
            return numerocolumna;
        }


        public Boolean TraerResultados(DbDataReader resultado, ref DataTable dtReporte, ref string Error)
        {
            try
            {
                string[] aColumnas = new string[] { };
                System.Type[] aTipos = new System.Type[] { };

                int numerocolumnas = 0;
                numerocolumnas = Estructura(resultado, ref aColumnas, ref aTipos, ref Error);

                dtReporte.Columns.Add("Tipo_Dato");
                dtReporte.Columns.Add("Valor");
                while (resultado.Read())
                {
                    for (int i = 0; i < numerocolumnas; i++)
                    {
                        DataRow drFila;
                        drFila = dtReporte.NewRow();
                        if (resultado[aColumnas[i]] != DBNull.Value)
                        {
                            //drFila[i] = Convert.ChangeType(resultado[aColumnas[i]], aTipos[i]);
                            drFila["Tipo_Dato"] = aColumnas[i];
                            drFila["Valor"] = Convert.ChangeType(resultado[aColumnas[i]], aTipos[i]);
                            dtReporte.Rows.Add(drFila);
                        }                        
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return false;
            }
            return true;
        }


        public CreditoIcetexCheckList CrearCreditoCheckList(CreditoIcetexCheckList pCredito, Usuario vUsuario, int pOpcion)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidcheck = cmdTransaccionFactory.CreateParameter();
                        pidcheck.ParameterName = "p_idcheck";
                        pidcheck.Value = pCredito.idcheck;
                        pidcheck.Direction = pOpcion == 1 ? ParameterDirection.Output : ParameterDirection.Input;
                        pidcheck.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidcheck);

                        DbParameter pidaprobacion = cmdTransaccionFactory.CreateParameter();
                        pidaprobacion.ParameterName = "p_idaprobacion";
                        pidaprobacion.Value = pCredito.idaprobacion;
                        pidaprobacion.Direction = ParameterDirection.Input;
                        pidaprobacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidaprobacion);

                        DbParameter pnumero_credito = cmdTransaccionFactory.CreateParameter();
                        pnumero_credito.ParameterName = "p_numero_credito";
                        pnumero_credito.Value = pCredito.numero_credito;
                        pnumero_credito.Direction = ParameterDirection.Input;
                        pnumero_credito.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_credito);

                        DbParameter ptipo = cmdTransaccionFactory.CreateParameter();
                        ptipo.ParameterName = "p_tipo";
                        if (pCredito.tipo == null)
                            ptipo.Value = DBNull.Value;
                        else
                            ptipo.Value = pCredito.tipo;
                        ptipo.Direction = ParameterDirection.Input;
                        ptipo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        if (pCredito.descripcion == null)
                            pdescripcion.Value = DBNull.Value;
                        else
                            pdescripcion.Value = pCredito.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        if (pCredito.valor == null)
                            pvalor.Value = DBNull.Value;
                        else
                            pvalor.Value = pCredito.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter presultado = cmdTransaccionFactory.CreateParameter();
                        presultado.ParameterName = "p_resultado";
                        if (pCredito.resultado == null)
                            presultado.Value = DBNull.Value;
                        else
                            presultado.Value = pCredito.resultado;
                        presultado.Direction = ParameterDirection.Input;
                        presultado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(presultado);

                        DbParameter pobservacion = cmdTransaccionFactory.CreateParameter();
                        pobservacion.ParameterName = "p_observacion";
                        if (pCredito.observacion == null)
                            pobservacion.Value = DBNull.Value;
                        else
                            pobservacion.Value = pCredito.observacion;
                        pobservacion.Direction = ParameterDirection.Input;
                        pobservacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pobservacion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = pOpcion == 1 ? "USP_XPINN_ICT_CHECKLIST_CREAR" : "";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        if (pOpcion == 1)
                            pCredito.idcheck = Convert.ToInt32(pidcheck.Value);
                        return pCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AprobacionData", "CrearCreditoCheckList", ex);
                        return null;
                    }
                }
            }
        }


        public List<CreditoIcetexCheckList> ListarCreditoIcetexCheckList(string pFiltro, ref string pError, Usuario vUsuario)
        {
            DbDataReader resultado;
            DataTable dtReporte = new DataTable();
            List<CreditoIcetexCheckList> lstIcetex = new List<CreditoIcetexCheckList>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT c.* FROM creditoicetexchecklist c " + pFiltro.ToString() + " order by 1";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        CreditoIcetexCheckList entidad;
                        while (resultado.Read())
                        {
                            entidad = new CreditoIcetexCheckList();
                            if (resultado["IDCHECK"] != DBNull.Value) entidad.idcheck = Convert.ToInt32(resultado["IDCHECK"]);
                            if (resultado["IDAPROBACION"] != DBNull.Value) entidad.idaprobacion = Convert.ToInt32(resultado["IDAPROBACION"]);
                            if (resultado["NUMERO_CREDITO"] != DBNull.Value) entidad.numero_credito = Convert.ToInt64(resultado["NUMERO_CREDITO"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToInt32(resultado["TIPO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToString(resultado["VALOR"]);
                            if (entidad.descripcion == null)
                            {
                                if (entidad.valor != null)
                                    entidad.descripcion = "DOCUMENTO CARGADO";
                            }
                            if (resultado["RESULTADO"] != DBNull.Value) entidad.resultado = Convert.ToInt32(resultado["RESULTADO"]);
                            if (entidad.resultado != null)
                            {
                                if (entidad.resultado == 1)
                                    entidad.obs_resultado = "Aprobado";
                                else
                                    entidad.obs_resultado = entidad.resultado == 2 ? "Aplazado" : "Negado";
                            }
                            if (resultado["OBSERVACION"] != DBNull.Value) entidad.observacion = Convert.ToString(resultado["OBSERVACION"]);
                            lstIcetex.Add(entidad);
                        }
                        return lstIcetex;
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                        return null;
                    }
                }
            }
        }

        public List<CreditoIcetexAprobacion> ListarCreditosAprobacion(string pFiltro, ref string pError, Usuario vUsuario)
        {
            DbDataReader resultado;
            DataTable dtReporte = new DataTable();
            List<CreditoIcetexAprobacion> lstAprobaciones = new List<CreditoIcetexAprobacion>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT c.* FROM creditoicetexaprobacion c " + pFiltro.ToString() + " order by 1";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        CreditoIcetexAprobacion entidad;
                        while (resultado.Read())
                        {
                            entidad = new CreditoIcetexAprobacion();
                            if (resultado["IDAPROBACION"] != DBNull.Value) entidad.idaprobacion = Convert.ToInt32(resultado["IDAPROBACION"]);
                            if (resultado["NUMERO_CREDITO"] != DBNull.Value) entidad.numero_credito = Convert.ToInt64(resultado["NUMERO_CREDITO"]);
                            if (resultado["FECHA_APROBACION"] != DBNull.Value) entidad.fecha_aprobacion = Convert.ToDateTime(resultado["FECHA_APROBACION"]);
                            if (resultado["COD_USUARIO"] != DBNull.Value) entidad.cod_usuario = Convert.ToInt32(resultado["COD_USUARIO"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);
                            if (resultado["DOCUMENTO_SOPORTE"] != DBNull.Value) entidad.documento_soporte = (byte[])resultado["DOCUMENTO_SOPORTE"];
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["TIPO_APROBACION"] != DBNull.Value) entidad.tipo_aprobacion = Convert.ToInt32(resultado["TIPO_APROBACION"]);
                            if (entidad.tipo_aprobacion != null)
                            {
                                if (entidad.tipo_aprobacion == 1)
                                {
                                    switch (entidad.estado)
                                    {
                                        case "A":
                                            entidad.descripcion = "Pre-Inscripción Aprobada";
                                            break;
                                        case "N":
                                            entidad.descripcion = "Pre-Inscripción Negada";
                                            break;
                                        case "Z":
                                            entidad.descripcion = "Pre-Inscripción Aplazada";
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (entidad.estado)
                                    {
                                        case "I":
                                            entidad.descripcion = "Inscripción Aprobada";
                                            break;
                                        case "N":
                                            entidad.descripcion = "Inscripción Negada";
                                            break;
                                        case "Z":
                                            entidad.descripcion = "Inscripción Aplazada";
                                            break;
                                    }
                                }
                            }
                            lstAprobaciones.Add(entidad);
                        }
                        return lstAprobaciones;
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                        return null;
                    }
                }
            }
        }
            
    }
}
