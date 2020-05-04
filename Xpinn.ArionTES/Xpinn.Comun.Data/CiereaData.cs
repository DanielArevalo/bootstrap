using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Comun.Entities;

namespace Xpinn.Comun.Data
{
    public class CiereaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla ConceptoS
        /// </summary>
        public CiereaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public Cierea CrearCierea(Cierea pCierea, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "p_fecha";
                        pfecha.Value = pCierea.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        DbParameter ptipo = cmdTransaccionFactory.CreateParameter();
                        ptipo.ParameterName = "p_tipo";
                        ptipo.Value = pCierea.tipo;
                        ptipo.Direction = ParameterDirection.Input;
                        ptipo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipo);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pCierea.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pcampo1 = cmdTransaccionFactory.CreateParameter();
                        pcampo1.ParameterName = "p_campo1";
                        pcampo1.Value = pCierea.campo1;
                        pcampo1.Direction = ParameterDirection.Input;
                        pcampo1.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcampo1);

                        DbParameter pcampo2 = cmdTransaccionFactory.CreateParameter();
                        pcampo2.ParameterName = "p_campo2";
                        if (pCierea.campo2 == null)
                            pcampo2.Value = DBNull.Value;
                        else
                            pcampo2.Value = pCierea.campo2;
                        pcampo2.Direction = ParameterDirection.Input;
                        pcampo2.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcampo2);

                        DbParameter pfecrea = cmdTransaccionFactory.CreateParameter();
                        pfecrea.ParameterName = "p_fecrea";
                        if (pCierea.fecrea == null)
                            pfecrea.Value = DBNull.Value;
                        else
                            pfecrea.Value = pCierea.fecrea;
                        pfecrea.Direction = ParameterDirection.Input;
                        pfecrea.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecrea);

                        DbParameter pcodusuario = cmdTransaccionFactory.CreateParameter();
                        pcodusuario.ParameterName = "p_codusuario";
                        if (pCierea.codusuario < 0)
                            pcodusuario.Value = DBNull.Value;
                        else
                            pcodusuario.Value = pCierea.codusuario;
                        pcodusuario.Direction = ParameterDirection.Input;
                        pcodusuario.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodusuario);

                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_CIEREA_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pCierea;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CiereaData", "CrearCierea", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene una lista de cierres
        /// </summary>
        /// <param name="pConceptoS">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de cierres obtenidos</returns>
        public List<Cierea> ListarCierea(Cierea pTipo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Cierea> lstCierea = new List<Cierea>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  cierea " + ObtenerFiltro(pTipo);

                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Cierea entidad = new Cierea();

                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["TIPO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["CAMPO1"] != DBNull.Value) entidad.campo1 = Convert.ToString(resultado["CAMPO1"]);
                            if (resultado["CAMPO2"] != DBNull.Value) entidad.campo2 = Convert.ToString(resultado["CAMPO2"]);
                            if (resultado["FECREA"] != DBNull.Value) entidad.fecrea = Convert.ToDateTime(resultado["FECREA"]);
                            if (resultado["CODUSUARIO"] != DBNull.Value) entidad.codusuario = Convert.ToInt64(resultado["CODUSUARIO"]);

                            lstCierea.Add(entidad);
                        }

                        return lstCierea;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CiereaData", "ListarCierea", ex);
                        return null;
                    }
                }
            }
        }


        public Boolean ExisteCierre(DateTime pFecha, String pTipo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        Configuracion config = new Configuracion();
                        if (string.Equals(dbConnectionFactory.TipoConexion(), "ORACLE"))
                            sql = "SELECT * FROM  cierea WHERE fecha = To_Date('" + pFecha.ToString(config.ObtenerFormatoFecha()) + "', '" + config.ObtenerFormatoFecha() + "') And tipo = '" + pTipo + "' ";
                        else
                            sql = "SELECT * FROM  cierea WHERE fecha = '" + pFecha.ToString(config.ObtenerFormatoFecha()) + "' And tipo = '" + pTipo + "' ";

                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Cierea entidad = new Cierea();

                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["TIPO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["CAMPO1"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["CAMPO1"]);
                            if (resultado["CAMPO2"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["CAMPO2"]);
                            if (resultado["FECREA"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["FECREA"]);
                            if (resultado["CODUSUARIO"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["CODUSUARIO"]);

                            return true;
                        }

                        return false;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CiereaData", "ExisteCierre", ex);
                        return false;
                    }
                }
            }
        }

        public DateTime FechaUltimoCierre(String pTipo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        sql = "SELECT MAX(FECHA) AS FECHA FROM  cierea WHERE estado = 'D' And tipo = '" + pTipo + "' ";

                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            Cierea entidad = new Cierea();

                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            return entidad.fecha;
                        }

                        return System.DateTime.MinValue;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CiereaData", "FechaUltimoCierre", ex);
                        return System.DateTime.MinValue;
                    }
                }
            }
        }


        public List<Cierea> ConsultaGeneralCierea(String pFiltro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Cierea> lstCierea = new List<Cierea>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        sql = "SELECT distinct FECHA,CAMPO1 FROM cierea " + pFiltro;

                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Cierea entidad = new Cierea();

                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["CAMPO1"] != DBNull.Value) entidad.campo1 = Convert.ToString(resultado["CAMPO1"]);
                            lstCierea.Add(entidad);
                        }

                        return lstCierea;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CiereaData", "ConsultaGeneralCierea", ex);
                        return null;
                    }
                }
            }
        }

        public List<Cierea> ListarCiereaFecha(String pTipo, String pEstado, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Cierea> lstCierea = new List<Cierea>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select distinct Fecha From cierea Where tipo = '" + pTipo + "' And estado = '" + pEstado + "' Order by fecha";

                        connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Cierea entidad = new Cierea();
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            lstCierea.Add(entidad);
                        }

                        return lstCierea;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CiereaData", "ListarCierea", ex);
                        return null;
                    }
                }
            }
        }

        public void PeriodicidadCierre(ref int dias_cierre, ref int tipo_calendario, Usuario pUsuario, int pCodigo = 1911)
        {
            dias_cierre = 30;
            tipo_calendario = 1;
            int periodicidad = 0;
            DbDataReader resultado = default(DbDataReader);

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string valor = "";
                        string sql = "Select valor From general Where codigo = " + pCodigo;
                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["VALOR"] != DBNull.Value) valor = Convert.ToString(resultado["VALOR"].ToString().Trim());
                        }
                        try
                        {
                            periodicidad = Convert.ToInt16(valor);
                        }
                        catch
                        {
                            return;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CierreHistoricoData", "PeriodicidadCierre", ex);
                        return;
                    }
                }
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "Select numero_dias, tipo_calendario From periodicidad Where cod_periodicidad = " + periodicidad;
                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["NUMERO_DIAS"] != DBNull.Value) dias_cierre = Convert.ToInt16(resultado["NUMERO_DIAS"].ToString());
                            if (resultado["TIPO_CALENDARIO"] != DBNull.Value) tipo_calendario = Convert.ToInt16(resultado["TIPO_CALENDARIO"].ToString());
                        }
                        return;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CierreHistoricoData", "PeriodicidadCierre", ex);
                        return;
                    }
                }

            }
        }

        //Agregado para seguimiento de cierres
        public Cierea ConsultarSigCierre(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Cierea entidad = new Cierea();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        sql = @"SELECT cr.COD_PROCESO,cr.DESCRIPCION,
                                CASE cr.ESTADO_PROCESO WHEN 'P' THEN 'Prueba' WHEN 'D' THEN 'Definitivo' ELSE null END AS ESTADO_PROCESO, 
                                u.NOMBRE AS Usuario, 
                                cr.FECHA_PROCESO, 
                                cr.FECHA_REALIZACION,
                                CASE cr.ESTADO_PROCESO WHEN 'P' THEN cr.DESCRIPCION WHEN 'D' THEN sg.NOM_SIG ELSE null END AS NOM_SIG,                                
                                CASE cr.ESTADO_PROCESO WHEN 'P' THEN sg.RUTA_PROCESO WHEN 'D' THEN sg.Ruta_Sig ELSE null END AS RUTA_PROCESO
                                FROM 
                                (SELECT cc.*, tp.DESCRIPCION, tp.TIPO_PROCESO FROM CONTROL_CIERRE cc LEFT JOIN PROCESO_CIERRE tp ON cc.COD_PROCESO = tp.COD_PROCESO WHERE cc.COD_CONTROL IN (Select Max(x.COD_CONTROL) FROM CONTROL_CIERRE x WHERE  x.COD_PROCESO = cc.COD_PROCESO)) cr 
                                LEFT JOIN USUARIOS u On cr.COD_USUARIO = u.CODUSUARIO
                                LEFT JOIN (SELECT p.COD_PROCESO, x.COD_PROCESO AS COD_PROCESO_SIG, x.DESCRIPCION as NOM_SIG, x.RUTA_PROCESO AS RUTA_SIG, p.RUTA_PROCESO FROM PROCESO_CIERRE p LEFT JOIN PROCESO_CIERRE x ON x.PROCESO_ANTERIOR = p.COD_PROCESO) sg ON sg.COD_PROCESO = cr.COD_PROCESO
                                WHERE cr.COD_CONTROL = (SELECT MAX(COD_CONTROL) FROM CONTROL_CIERRE) ";

                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_PROCESO"] != DBNull.Value) entidad.cod_proceso = Convert.ToInt64(resultado["COD_PROCESO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.nom_cierre_ant = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["ESTADO_PROCESO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO_PROCESO"]);
                            if (resultado["USUARIO"] != DBNull.Value) entidad.nom_usuario = Convert.ToString(resultado["USUARIO"]);
                            if (resultado["FECHA_PROCESO"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA_PROCESO"]);
                            if (resultado["FECHA_REALIZACION"] != DBNull.Value) entidad.fecrea = Convert.ToDateTime(resultado["FECHA_REALIZACION"]);
                            if (resultado["NOM_SIG"] != DBNull.Value) entidad.nom_cierre_sig = Convert.ToString(resultado["NOM_SIG"]);
                            if (resultado["RUTA_PROCESO"] != DBNull.Value) entidad.ruta_proceso = Convert.ToString(resultado["RUTA_PROCESO"]);
                        }
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CiereaData", "ConsultarSigCierre", ex);
                        return null;
                    }
                }
            }
        }

        public List<Cierea> ListarControlCierres(string filtro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Cierea> lstCierres = new List<Cierea>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        sql = @"SELECT c.DESCRIPCION as Nom_Anterior, u.NOMBRE, CASE t.ESTADO_PROCESO WHEN 'P' THEN 'Prueba' WHEN 'D' THEN 'Definitivo' ELSE null END as ESTADO_PROCESO, t.FECHA_PROCESO, t.FECHA_REALIZACION
                                FROM PROCESO_CIERRE c 
                                INNER JOIN CONTROL_CIERRE t ON c.COD_PROCESO = t.COD_PROCESO
                                INNER JOIN USUARIOS u ON t.COD_USUARIO = u.CODUSUARIO ";
                        if (filtro != "")
                            sql += filtro;

                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Cierea entidad = new Cierea();
                            if (resultado["NOM_ANTERIOR"] != DBNull.Value) entidad.nom_cierre_ant = Convert.ToString(resultado["NOM_ANTERIOR"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nom_usuario = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["ESTADO_PROCESO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO_PROCESO"]);
                            if (resultado["FECHA_PROCESO"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA_PROCESO"]);
                            if (resultado["FECHA_REALIZACION"] != DBNull.Value) entidad.fecrea = Convert.ToDateTime(resultado["FECHA_REALIZACION"]);
                            lstCierres.Add(entidad);
                        }
                        return lstCierres;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CiereaData", "ListarControlCierres", ex);
                        return null;
                    }
                }
            }
        }

        public List<string> ListarPeriodosCierres(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<string> lstPeriodos = new List<string>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        sql = @"SELECT DISTINCT FECHA FROM CIEREA ORDER BY FECHA DESC";

                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            DateTime fecha = new DateTime();
                            if (resultado["FECHA"] != DBNull.Value) fecha = Convert.ToDateTime(resultado["FECHA"]);
                            lstPeriodos.Add(fecha.ToString("dd/MM/yyyy"));
                        }
                        return lstPeriodos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CiereaData", "ListarPeriodosCierres", ex);
                        return null;
                    }
                }
            }
        }

        public List<Cierea> ListarCiereaFechaComp(String pTipo, String pEstado, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Cierea> lstCierea = new List<Cierea>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select distinct Fecha From cierea Where tipo = '" + pTipo + "' and estado = '" + pEstado + "' And (campo1 Is Null Or Trim(campo1) Is Null Or Trim(Campo1) = '') And fecha Not In (Select Min(x.Fecha) From cierea x Where x.tipo = '" + pTipo + "') Order by fecha";

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Cierea entidad = new Cierea();
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            lstCierea.Add(entidad);
                        }

                        return lstCierea;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CiereaData", "ListarCierea", ex);
                        return null;
                    }
                }
            }
        }



    }
}
