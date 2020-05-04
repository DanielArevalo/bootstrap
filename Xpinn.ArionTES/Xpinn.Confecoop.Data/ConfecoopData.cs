using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Confecoop.Entities;
using System;


namespace Xpinn.Confecoop.Data
{
    
    public class ConfecoopData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;


        public ConfecoopData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public List<PUC> ListarFechaCierreGLOBAL(string tipo, string estado, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<PUC> lstFechaCierre = new List<PUC>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "SELECT DISTINCT FECHA FROM CIEREA WHERE TIPO = '"+ tipo+"' AND ESTADO = '"+estado+"' ORDER BY FECHA DESC";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            PUC entidad = new PUC();

                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"].ToString());
                            lstFechaCierre.Add(entidad);
                        }

                        return lstFechaCierre;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConfecoopData", "ListarFechaCierre", ex);
                        return null;
                    }
                }
            }
        }



        public List<PUC> ListarTEMP_SUPERSOLIDARIA(PUC pPuc, ref string pError, Usuario vUsuario, bool estado, Int32 pTipoNorma)
        {
            pError = "";
            DbDataReader resultado = default(DbDataReader);
            List<PUC> lstSuperPuc = new List<PUC>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
              {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;                       
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                       
                        DbParameter pCuentasCero = cmdTransaccionFactory.CreateParameter();
                        pCuentasCero.ParameterName = "pCuentasCero";
                        if (estado) pCuentasCero.Value = 1; else pCuentasCero.Value = 0;
                        pCuentasCero.Direction = ParameterDirection.Input;
                        pCuentasCero.DbType = DbType.Int16;
                        cmdTransaccionFactory.Parameters.Add(pCuentasCero);

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "PFECHA";
                        pfecha.Value = pPuc.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pfecha);
                        
                        DbParameter pseparador = cmdTransaccionFactory.CreateParameter();
                        pseparador.ParameterName = "PSEPARADOR";
                        pseparador.Value = pPuc.separador;
                        pseparador.Direction = ParameterDirection.Input;
                        pseparador.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pseparador);
                        
                        DbParameter pcuentasNiif = cmdTransaccionFactory.CreateParameter();
                        pcuentasNiif.ParameterName = "PTIPONORMA";
                        pcuentasNiif.Value = pTipoNorma;
                        pcuentasNiif.Direction = ParameterDirection.Input;
                        pcuentasNiif.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcuentasNiif);

                        DbParameter pcorte = cmdTransaccionFactory.CreateParameter();
                        pcorte.ParameterName = "PCORTE";
                        pcorte.Value = pPuc.a_fecha_corte;
                        pcorte.Direction = ParameterDirection.Input;
                        pcorte.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcorte);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SUPER_PUC";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                        //BOExcepcion.Throw("ConfecoopData", "USP_XPINN_SUPER_PUC", ex);
                        return null;
                    }
                };


                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        Configuracion conf = new Configuracion();

                        string sql = "Select * from TEMP_SUPERSOLIDARIA order by IDLINEA";  //Where fecha = To_Date('" + Convert.ToDateTime(pEntidad.fecha).ToShortDateString() + "','" + conf.ObtenerFormatoFecha() + "') Order by cod_cuenta"                       
                        
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            PUC entidad = new PUC();

                            if (resultado["IDLINEA"] != DBNull.Value) entidad.idlinea = Convert.ToInt32(resultado["IDLINEA"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["LINEA"]);

                            lstSuperPuc.Add(entidad);
                        }

                        return lstSuperPuc;
                    }
                    catch (Exception ex)
                    {   
                        BOExcepcion.Throw("ConfecoopData", "ListarTEMP_SUPERSOLIDARIA", ex);
                        return null;
                    }
                }
            }
        }

        public List<PUC> ListarTEMP_POSICIONNETADIRECTIVOS(PUC pPuc, ref string pError, Usuario usuario)
        {
            pError = "";
            DbDataReader resultado = default(DbDataReader);
            List<PUC> lstSuperPuc = new List<PUC>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "PFECHA";
                        pfecha.Value = pPuc.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        DbParameter pseparador = cmdTransaccionFactory.CreateParameter();
                        pseparador.ParameterName = "PSEPARADOR";
                        pseparador.Value = pPuc.separador;
                        pseparador.Direction = ParameterDirection.Input;
                        pseparador.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pseparador);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REP_NETADIRECTIVO";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                        return null;
                    }
                };


                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        Configuracion conf = new Configuracion();

                        string sql = "Select * from TEMP_REPORTES_ARCHIVOS order by IDLINEA";

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            PUC entidad = new PUC();

                            if (resultado["IDLINEA"] != DBNull.Value) entidad.idlinea = Convert.ToInt32(resultado["IDLINEA"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["LINEA"]);

                            lstSuperPuc.Add(entidad);
                        }

                        return lstSuperPuc;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConfecoopData", "ListarTEMP_POSICIONNETANEGATIVA", ex);
                        return null;
                    }
                }
            }
        }

        public List<PUC> ListarTEMP_POSICIONNETANEGATIVA(PUC pPuc, ref string pError, Usuario usuario)
        {
            pError = "";
            DbDataReader resultado = default(DbDataReader);
            List<PUC> lstSuperPuc = new List<PUC>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "PFECHA";
                        pfecha.Value = pPuc.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        DbParameter pseparador = cmdTransaccionFactory.CreateParameter();
                        pseparador.ParameterName = "PSEPARADOR";
                        pseparador.Value = pPuc.separador;
                        pseparador.Direction = ParameterDirection.Input;
                        pseparador.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pseparador);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REP_NETANEGATIVA";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                        return null;
                    }
                };


                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        Configuracion conf = new Configuracion();

                        string sql = "Select * from TEMP_REPORTES_ARCHIVOS order by IDLINEA";

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            PUC entidad = new PUC();

                            if (resultado["IDLINEA"] != DBNull.Value) entidad.idlinea = Convert.ToInt32(resultado["IDLINEA"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["LINEA"]);

                            lstSuperPuc.Add(entidad);
                        }

                        return lstSuperPuc;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConfecoopData", "ListarTEMP_POSICIONNETANEGATIVA", ex);
                        return null;
                    }
                }
            }
        }

        public List<PUC> ListarTEMP_POSICIONNETAPOSITIVA(PUC pPuc, ref string pError, Usuario usuario)
        {
            pError = "";
            DbDataReader resultado = default(DbDataReader);
            List<PUC> lstSuperPuc = new List<PUC>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "PFECHA";
                        pfecha.Value = pPuc.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        DbParameter pseparador = cmdTransaccionFactory.CreateParameter();
                        pseparador.ParameterName = "PSEPARADOR";
                        pseparador.Value = pPuc.separador;
                        pseparador.Direction = ParameterDirection.Input;
                        pseparador.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pseparador);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REP_NETAPOSITIVA";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                        return null;
                    }
                };


                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        Configuracion conf = new Configuracion();

                        string sql = "Select * from TEMP_REPORTES_ARCHIVOS order by IDLINEA";

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            PUC entidad = new PUC();

                            if (resultado["IDLINEA"] != DBNull.Value) entidad.idlinea = Convert.ToInt32(resultado["IDLINEA"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["LINEA"]);

                            lstSuperPuc.Add(entidad);
                        }

                        return lstSuperPuc;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConfecoopData", "ListarTEMP_POSICIONNETA", ex);
                        return null;
                    }
                }
            }
        }

        public List<PUC> ListarTEMP_SUPER_DIRECTIVOS(PUC pPuc, ref string pError, Usuario usuario)
        {
            pError = "";
            DbDataReader resultado = default(DbDataReader);
            List<PUC> lstSuperPuc = new List<PUC>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "PFECHA";
                        pfecha.Value = pPuc.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        DbParameter pseparador = cmdTransaccionFactory.CreateParameter();
                        pseparador.ParameterName = "PSEPARADOR";
                        pseparador.Value = pPuc.separador;
                        pseparador.Direction = ParameterDirection.Input;
                        pseparador.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pseparador);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SUPER_DIRECTIVOS";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                        return null;
                    }
                };


                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        Configuracion conf = new Configuracion();

                        string sql = "Select * from TEMP_SUPERSOLIDARIA order by IDLINEA"; 

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            PUC entidad = new PUC();

                            if (resultado["IDLINEA"] != DBNull.Value) entidad.idlinea = Convert.ToInt32(resultado["IDLINEA"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["LINEA"]);

                            lstSuperPuc.Add(entidad);
                        }

                        return lstSuperPuc;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConfecoopData", "ListarTEMP_SUPER_DIRECTIVOS", ex);
                        return null;
                    }
                }
            }
        }

        public List<PUC> ListarTEMP_SUPERSOLI_APORTES(PUC pPuc, ref string pError, Usuario vUsuario)
        {
            pError = "";
            DbDataReader resultado = default(DbDataReader);
            List<PUC> lstSuperPuc = new List<PUC>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "PFECHA";
                        pfecha.Value = pPuc.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pfecha);



                        DbParameter pseparador = cmdTransaccionFactory.CreateParameter();
                        pseparador.ParameterName = "PSEPARADOR";
                        pseparador.Value = pPuc.separador;
                        pseparador.Direction = ParameterDirection.Input;
                        pseparador.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pseparador);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SUPER_APORTES";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                        //BOExcepcion.Throw("ConfecoopData", "USP_XPINN_SUPER_PUC", ex);
                        return null;
                    }
                };


                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        Configuracion conf = new Configuracion();

                        string sql = "Select * from TEMP_SUPERSOLIDARIA order by IDLINEA";  //Where fecha = To_Date('" + Convert.ToDateTime(pEntidad.fecha).ToShortDateString() + "','" + conf.ObtenerFormatoFecha() + "') Order by cod_cuenta"                       

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            PUC entidad = new PUC();

                            if (resultado["IDLINEA"] != DBNull.Value) entidad.idlinea = Convert.ToInt32(resultado["IDLINEA"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["LINEA"]);

                            lstSuperPuc.Add(entidad);
                        }

                        return lstSuperPuc;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConfecoopData", "ListarTEMP_SUPERSOLI_APORTES", ex);
                        return null;
                    }
                }
            }
        }


        public List<PUC> ListarTEMP_SUPERSOLI_CARTERA(PUC pPuc, ref string pError, Usuario vUsuario)
        {
            pError = "";
            DbDataReader resultado = default(DbDataReader);
            List<PUC> lstSuperPuc = new List<PUC>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "PFECHA";
                        pfecha.Value = pPuc.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pfecha);
                        
                        DbParameter pseparador = cmdTransaccionFactory.CreateParameter();
                        pseparador.ParameterName = "PSEPARADOR";
                        pseparador.Value = pPuc.separador;
                        pseparador.Direction = ParameterDirection.Input;
                        pseparador.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pseparador);

                        DbParameter pcuentaniif = cmdTransaccionFactory.CreateParameter();
                        pcuentaniif.ParameterName = "PCUENTANIIF";
                        pcuentaniif.Value = pPuc.maneja_niif;
                        pcuentaniif.Direction = ParameterDirection.Input;
                        pcuentaniif.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcuentaniif);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SUPER_CARTERA";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                        //BOExcepcion.Throw("ConfecoopData", "USP_XPINN_SUPER_PUC", ex);
                        return null;
                    }
                };


                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;
                        Configuracion conf = new Configuracion();

                        string sql = "Select * from TEMP_SUPERSOLICART order by IDLINEA";  //Where fecha = To_Date('" + Convert.ToDateTime(pEntidad.fecha).ToShortDateString() + "','" + conf.ObtenerFormatoFecha() + "') Order by cod_cuenta"                       

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            PUC entidad = new PUC();

                            if (resultado["IDLINEA"] != DBNull.Value) entidad.idlinea = Convert.ToInt32(resultado["IDLINEA"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["LINEA"]);

                            lstSuperPuc.Add(entidad);
                        }

                        return lstSuperPuc;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConfecoopData", "ListarTEMP_SUPERSOLI_CARTERA", ex);
                        return null;
                    }
                }
            }
        }

        public List<PUC> ListarTEMP_SUPERSOLI_ACTIVOSFIJOS(PUC pPuc, ref string pError, Usuario vUsuario)
        {
            pError = "";
            DbDataReader resultado = default(DbDataReader);
            List<PUC> lstSuperPuc = new List<PUC>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "PFECHA";
                        pfecha.Value = pPuc.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pfecha);
                        
                        DbParameter pseparador = cmdTransaccionFactory.CreateParameter();
                        pseparador.ParameterName = "PSEPARADOR";
                        pseparador.Value = pPuc.separador;
                        pseparador.Direction = ParameterDirection.Input;
                        pseparador.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pseparador);

                        DbParameter pcuentaniif = cmdTransaccionFactory.CreateParameter();
                        pcuentaniif.ParameterName = "PCUENTANIIF";
                        pcuentaniif.Value = pPuc.maneja_niif;
                        pcuentaniif.Direction = ParameterDirection.Input;
                        pcuentaniif.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcuentaniif);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SUPER_PROPLANTEQUIPO";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                        //BOExcepcion.Throw("ConfecoopData", "USP_XPINN_SUPER_PUC", ex);
                        return null;
                    }
                };


                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;
                        Configuracion conf = new Configuracion();

                        string sql = "Select * from TEMP_SUPERSOLIDARIA order by IDLINEA";  //Where fecha = To_Date('" + Convert.ToDateTime(pEntidad.fecha).ToShortDateString() + "','" + conf.ObtenerFormatoFecha() + "') Order by cod_cuenta"                       

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            PUC entidad = new PUC();

                            if (resultado["IDLINEA"] != DBNull.Value) entidad.idlinea = Convert.ToInt32(resultado["IDLINEA"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["LINEA"]);

                            lstSuperPuc.Add(entidad);
                        }

                        return lstSuperPuc;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConfecoopData", "ListarTEMP_SUPERSOLI_ACTIVOSFIJOS", ex);
                        return null;
                    }
                }
            }
        }



        public List<PUC> ListarTEMP_SUPER_ASOCIADOS(PUC pPuc, ref string pError, Usuario vUsuario)
        {
            pError = "";
            DbDataReader resultado = default(DbDataReader);
            List<PUC> lstSuperPuc = new List<PUC>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "PFECHA";
                        pfecha.Value = pPuc.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pfecha);
                        

                        DbParameter pseparador = cmdTransaccionFactory.CreateParameter();
                        pseparador.ParameterName = "PSEPARADOR";
                        pseparador.Value = pPuc.separador;
                        pseparador.Direction = ParameterDirection.Input;
                        pseparador.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pseparador);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SUPER_ASOCIADOS";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                        //BOExcepcion.Throw("ConfecoopData", "USP_XPINN_SUPER_PUC", ex);
                        return null;
                    }
                };



                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        Configuracion conf = new Configuracion();

                        string sql = "Select * from TEMP_SUPERSOLIAFIL order by IDLINEA";  //Where fecha = To_Date('" + Convert.ToDateTime(pEntidad.fecha).ToShortDateString() + "','" + conf.ObtenerFormatoFecha() + "') Order by cod_cuenta"                       

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            PUC entidad = new PUC();

                            if (resultado["IDLINEA"] != DBNull.Value) entidad.idlinea = Convert.ToInt32(resultado["IDLINEA"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["LINEA"]);

                            lstSuperPuc.Add(entidad);
                        }

                        return lstSuperPuc;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConfecoopData", "ListarTEMP_SUPER_ASOCIADOS", ex);
                        return null;
                    }
                }
            }
        }

        public List<PUC> ListarTEMP_SUPERCAPTACIONESS(PUC pPuc, ref string pError, Usuario vUsuario)
        {
            pError = "";
            DbDataReader resultado = default(DbDataReader);
            List<PUC> lstSuperPuc = new List<PUC>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "PFECHA";
                        pfecha.Value = pPuc.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pfecha);


                        DbParameter pseparador = cmdTransaccionFactory.CreateParameter();
                        pseparador.ParameterName = "PSEPARADOR";
                        pseparador.Value = pPuc.separador;
                        pseparador.Direction = ParameterDirection.Input;
                        pseparador.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pseparador);

                        DbParameter pcuentaniif = cmdTransaccionFactory.CreateParameter();
                        pcuentaniif.ParameterName = "PCUENTANIIF";
                        pcuentaniif.Value = pPuc.maneja_niif;
                        pcuentaniif.Direction = ParameterDirection.Input;
                        pcuentaniif.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcuentaniif);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SUPER_CAPTACIONES";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                        //BOExcepcion.Throw("ConfecoopData", "USP_XPINN_SUPER_CAPTACIONES", ex);
                        return null;
                    }
                };



                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        Configuracion conf = new Configuracion();

                        string sql = "Select * from TEMP_SUPERSOLICAPT order by IDLINEA";  //Where fecha = To_Date('" + Convert.ToDateTime(pEntidad.fecha).ToShortDateString() + "','" + conf.ObtenerFormatoFecha() + "') Order by cod_cuenta"                       

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            PUC entidad = new PUC();

                            if (resultado["IDLINEA"] != DBNull.Value) entidad.idlinea = Convert.ToInt32(resultado["IDLINEA"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["LINEA"]);

                            lstSuperPuc.Add(entidad);
                        }

                        return lstSuperPuc;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConfecoopData", "ListarTEMP_SUPERCAPTACIONESS", ex);
                        return null;
                    }
                }
            }
        }
        

        public List<PUC> ListarTEMP_SUPERiesgoLiquidez(PUC pPuc, ref string pError, Usuario vUsuario)
        {
            pError = "";
            DbDataReader resultado = default(DbDataReader);
            List<PUC> lstSuperPuc = new List<PUC>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "PFECHA";
                        pfecha.Value = pPuc.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pfecha);


                        DbParameter pseparador = cmdTransaccionFactory.CreateParameter();
                        pseparador.ParameterName = "PSEPARADOR";
                        pseparador.Value = pPuc.separador;
                        pseparador.Direction = ParameterDirection.Input;
                        pseparador.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pseparador);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SUPER_RIESGOLIQUIDEZ";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                        //BOExcepcion.Throw("ConfecoopData", "USP_XPINN_SUPER_PUC", ex);
                        return null;
                    }
                };


                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        Configuracion conf = new Configuracion();

                        string sql = "Select * from TEMP_SUPERSOLIDARIA order by IDLINEA";  //Where fecha = To_Date('" + Convert.ToDateTime(pEntidad.fecha).ToShortDateString() + "','" + conf.ObtenerFormatoFecha() + "') Order by cod_cuenta"                       

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            PUC entidad = new PUC();

                            if (resultado["IDLINEA"] != DBNull.Value) entidad.idlinea = Convert.ToInt32(resultado["IDLINEA"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["LINEA"]);

                            lstSuperPuc.Add(entidad);
                        }

                        return lstSuperPuc;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConfecoopData", "ListarTEMP_SUPERiesgoLiquidez", ex);
                        return null;
                    }
                }
            }
        }


        public List<PUC> ListarTEMP_SUPERObligacionFinanciera(PUC pPuc, ref string pError, Usuario vUsuario)
        {
            pError = "";
            DbDataReader resultado = default(DbDataReader);
            List<PUC> lstSuperPuc = new List<PUC>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "PFECHA";
                        pfecha.Value = pPuc.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pfecha);


                        DbParameter pseparador = cmdTransaccionFactory.CreateParameter();
                        pseparador.ParameterName = "PSEPARADOR";
                        pseparador.Value = pPuc.separador;
                        pseparador.Direction = ParameterDirection.Input;
                        pseparador.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pseparador);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SUPER_OBLIGACIONFINA";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                        //BOExcepcion.Throw("ConfecoopData", "USP_XPINN_SUPER_PUC", ex);
                        return null;
                    }
                };


                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        Configuracion conf = new Configuracion();

                        string sql = "Select * from TEMP_SUPERSOLIDARIA order by IDLINEA";  //Where fecha = To_Date('" + Convert.ToDateTime(pEntidad.fecha).ToShortDateString() + "','" + conf.ObtenerFormatoFecha() + "') Order by cod_cuenta"                       

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            PUC entidad = new PUC();

                            if (resultado["IDLINEA"] != DBNull.Value) entidad.idlinea = Convert.ToInt32(resultado["IDLINEA"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["LINEA"]);

                            lstSuperPuc.Add(entidad);
                        }

                        return lstSuperPuc;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConfecoopData", "ListarTEMP_SUPERObligacionFinanciera", ex);
                        return null;
                    }
                }
            }
        }


        public List<PUC> ListarTEMP_SUPERedOficinas(PUC pPuc, ref string pError, Usuario vUsuario)
        {
            pError = "";
            DbDataReader resultado = default(DbDataReader);
            List<PUC> lstSuperPuc = new List<PUC>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "PFECHA";
                        pfecha.Value = pPuc.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pfecha);


                        DbParameter ptipo_norma = cmdTransaccionFactory.CreateParameter();
                        ptipo_norma.ParameterName = "PTIPONORMA";
                        ptipo_norma.Value = pPuc.maneja_niif;
                        ptipo_norma.Direction = ParameterDirection.Input;
                        ptipo_norma.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_norma);


                        DbParameter pseparador = cmdTransaccionFactory.CreateParameter();
                        pseparador.ParameterName = "PSEPARADOR";
                        pseparador.Value = pPuc.separador;
                        pseparador.Direction = ParameterDirection.Input;
                        pseparador.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pseparador);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SUPER_REDOFICINAS";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                        //BOExcepcion.Throw("ConfecoopData", "USP_XPINN_SUPER_PUC", ex);
                        return null;
                    }
                };


                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        Configuracion conf = new Configuracion();

                        string sql = "Select * from TEMP_SUPERSOLIDARIA order by IDLINEA";  //Where fecha = To_Date('" + Convert.ToDateTime(pEntidad.fecha).ToShortDateString() + "','" + conf.ObtenerFormatoFecha() + "') Order by cod_cuenta"                       

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            PUC entidad = new PUC();

                            if (resultado["IDLINEA"] != DBNull.Value) entidad.idlinea = Convert.ToInt32(resultado["IDLINEA"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["LINEA"]);

                            lstSuperPuc.Add(entidad);
                        }

                        return lstSuperPuc;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConfecoopData", "ListarTEMP_SUPERedOficinas", ex);
                        return null;
                    }
                }
            }
        }

        public List<PUC> ListarTEMP_SUPERSOLI_ESTADISTICAS(PUC pPuc, ref string pError, Usuario vUsuario)
        {
            pError = "";
            DbDataReader resultado = default(DbDataReader);
            List<PUC> lstSuperPuc = new List<PUC>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "PFECHA";
                        pfecha.Value = pPuc.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pfecha);


                        DbParameter pseparador = cmdTransaccionFactory.CreateParameter();
                        pseparador.ParameterName = "PSEPARADOR";
                        pseparador.Value = pPuc.separador;
                        pseparador.Direction = ParameterDirection.Input;
                        pseparador.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pseparador);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SUPER_ESTADISTICAS";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                        return null;
                    }
                };


                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        Configuracion conf = new Configuracion();

                        string sql = "Select * from TEMP_SUPERSOLIDARIA order by IDLINEA";  //Where fecha = To_Date('" + Convert.ToDateTime(pEntidad.fecha).ToShortDateString() + "','" + conf.ObtenerFormatoFecha() + "') Order by cod_cuenta"                       

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            PUC entidad = new PUC();

                            if (resultado["IDLINEA"] != DBNull.Value) entidad.idlinea = Convert.ToInt32(resultado["IDLINEA"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["LINEA"]);

                            lstSuperPuc.Add(entidad);
                        }

                        return lstSuperPuc;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConfecoopData", "ListarTEMP_SUPERSOLI_ESTADISTICAS", ex);
                        return null;
                    }
                }
            }
        }

        public DateTime FechaUltimoCierre(Usuario vUsuario)
        {
            DateTime fecha = DateTime.MinValue;
            DbDataReader resultado = default(DbDataReader);

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "Select Max(fecha) As fecha From cierea Where tipo = 'G' And estado = 'D' ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["FECHA"] != DBNull.Value) fecha = Convert.ToDateTime(resultado["FECHA"].ToString());
                        }
                        return fecha;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConfecoopData", "FechaUltimoCierre", ex);
                        return fecha;
                    }
                }
            }
        }

        public void PeriodicidadCierre(ref int dias_cierre, ref int tipo_calendario, Usuario pUsuario)
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
                        string sql = "Select valor From general Where codigo = 4100 ";
                        connection.Open();
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
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CierremensualData", "PeriodicidadCierre", ex);
                        return;
                    }
                }
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "Select numero_dias, tipo_calendario From periodicidad Where cod_periodicidad = " + periodicidad;
                        connection.Open();
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
                        BOExcepcion.Throw("ConfecoopData", "PeriodicidadCierre", ex);
                        return;
                    }
                }

            }
        }

        public List<PUC> ListarTEMP_SUPERSOLI_OPERACIONES(PUC pPuc, ref string pError, Usuario vUsuario)
        {
            pError = "";
            DbDataReader resultado = default(DbDataReader);
            List<PUC> lstSuperPuc = new List<PUC>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "PFECHA";
                        pfecha.Value = pPuc.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pfecha);


                        DbParameter pseparador = cmdTransaccionFactory.CreateParameter();
                        pseparador.ParameterName = "PSEPARADOR";
                        pseparador.Value = pPuc.separador;
                        pseparador.Direction = ParameterDirection.Input;
                        pseparador.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pseparador);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SUPER_OPERACIONES";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                        return null;
                    }
                };


                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        Configuracion conf = new Configuracion();

                        string sql = "Select * from TEMP_SUPERSOLIDARIA order by IDLINEA";  //Where fecha = To_Date('" + Convert.ToDateTime(pEntidad.fecha).ToShortDateString() + "','" + conf.ObtenerFormatoFecha() + "') Order by cod_cuenta"                       

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            PUC entidad = new PUC();

                            if (resultado["IDLINEA"] != DBNull.Value) entidad.idlinea = Convert.ToInt32(resultado["IDLINEA"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["LINEA"]);

                            lstSuperPuc.Add(entidad);
                        }

                        return lstSuperPuc;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConfecoopData", "ListarTEMP_SUPERSOLI_OPERACIONES", ex);
                        return null;
                    }
                }
            }
        }


        public List<PUC> ListarTEMP_SUPER_FDO_LIQUIDEZ(PUC pPuc, ref string pError, Usuario usuario)
        {
            pError = "";
            DbDataReader resultado = default(DbDataReader);
            List<PUC> lstSuperPuc = new List<PUC>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "PFECHA";
                        pfecha.Value = pPuc.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        DbParameter pseparador = cmdTransaccionFactory.CreateParameter();
                        pseparador.ParameterName = "PSEPARADOR";
                        pseparador.Value = pPuc.separador;
                        pseparador.Direction = ParameterDirection.Input;
                        pseparador.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pseparador);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SUPER_FDOLIQUIDEZ";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                        return null;
                    }
                };


                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        Configuracion conf = new Configuracion();

                        string sql = "Select * from TEMP_SUPERSOLIDARIA order by IDLINEA";

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            PUC entidad = new PUC();

                            if (resultado["IDLINEA"] != DBNull.Value) entidad.idlinea = Convert.ToInt32(resultado["IDLINEA"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["LINEA"]);

                            lstSuperPuc.Add(entidad);
                        }

                        return lstSuperPuc;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConfecoopData", "ListarTEMP_SUPER_FDO_LIQUIDEZ", ex);
                        return null;
                    }
                }
            }
        }


        public List<PUC> ListarTEMP_SUPERCotitulares(PUC pPuc, ref string pError, Usuario vUsuario)
        {
            pError = "";
            DbDataReader resultado = default(DbDataReader);
            List<PUC> lstSuperPuc = new List<PUC>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "PFECHA";
                        pfecha.Value = pPuc.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pfecha);


                        DbParameter pseparador = cmdTransaccionFactory.CreateParameter();
                        pseparador.ParameterName = "PSEPARADOR";
                        pseparador.Value = pPuc.separador;
                        pseparador.Direction = ParameterDirection.Input;
                        pseparador.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pseparador);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SUPER_COTITULARES";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                        //BOExcepcion.Throw("ConfecoopData", "USP_XPINN_SUPER_CAPTACIONES", ex);
                        return null;
                    }
                };


                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        Configuracion conf = new Configuracion();

                        string sql = "Select * from TEMP_SUPERSOLIDARIA order by IDLINEA";

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            PUC entidad = new PUC();

                            if (resultado["IDLINEA"] != DBNull.Value) entidad.idlinea = Convert.ToInt32(resultado["IDLINEA"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["LINEA"]);

                            lstSuperPuc.Add(entidad);
                        }

                        return lstSuperPuc;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConfecoopData", "ListarTEMP_SUPERCotitulares", ex);
                        return null;
                    }
                }
            }
        }


        public List<PUC> ListarTEMP_SUPERSOLI_CUENTAXPAGAR(PUC pPuc, ref string pError, Usuario vUsuario)
        {
            pError = "";
            DbDataReader resultado = default(DbDataReader);
            List<PUC> lstSuperPuc = new List<PUC>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "PFECHA";
                        pfecha.Value = pPuc.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        DbParameter pseparador = cmdTransaccionFactory.CreateParameter();
                        pseparador.ParameterName = "PSEPARADOR";
                        pseparador.Value = pPuc.separador;
                        pseparador.Direction = ParameterDirection.Input;
                        pseparador.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pseparador);

                        DbParameter pcuentaniif = cmdTransaccionFactory.CreateParameter();
                        pcuentaniif.ParameterName = "PCUENTANIIF";
                        pcuentaniif.Value = pPuc.maneja_niif;
                        pcuentaniif.Direction = ParameterDirection.Input;
                        pcuentaniif.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcuentaniif);


                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SUPER_CUENTASXPAGAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                        //BOExcepcion.Throw("ConfecoopData", "USP_XPINN_SUPER_CAPTACIONES", ex);
                        return null;
                    }
                };


                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        Configuracion conf = new Configuracion();

                        string sql = "Select * from TEMP_SUPERSOLIDARIA order by IDLINEA";

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            PUC entidad = new PUC();

                            if (resultado["IDLINEA"] != DBNull.Value) entidad.idlinea = Convert.ToInt32(resultado["IDLINEA"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["LINEA"]);

                            lstSuperPuc.Add(entidad);
                        }

                        return lstSuperPuc;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConfecoopData", "ListarTEMP_SUPERSOLI_CUENTAXPAGAR", ex);
                        return null;
                    }
                }
            }
        }


        public List<PUC> ListarTEMP_SUPERSOLI_CONVENIO(PUC pPuc, ref string pError, Usuario vUsuario)
        {
            pError = "";
            DbDataReader resultado = default(DbDataReader);
            List<PUC> lstSuperPuc = new List<PUC>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "PFECHA";
                        pfecha.Value = pPuc.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        DbParameter pseparador = cmdTransaccionFactory.CreateParameter();
                        pseparador.ParameterName = "PSEPARADOR";
                        pseparador.Value = pPuc.separador;
                        pseparador.Direction = ParameterDirection.Input;
                        pseparador.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pseparador);

                        DbParameter pcuentaniif = cmdTransaccionFactory.CreateParameter();
                        pcuentaniif.ParameterName = "PCUENTANIIF";
                        pcuentaniif.Value = pPuc.maneja_niif;
                        pcuentaniif.Direction = ParameterDirection.Input;
                        pcuentaniif.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcuentaniif);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SUPER_CONVENIOS";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                        //BOExcepcion.Throw("ConfecoopData", "USP_XPINN_SUPER_PUC", ex);
                        return null;
                    }
                };


                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;
                        Configuracion conf = new Configuracion();

                        string sql = "Select * from TEMP_SUPERSOLIDARIA order by IDLINEA";  //Where fecha = To_Date('" + Convert.ToDateTime(pEntidad.fecha).ToShortDateString() + "','" + conf.ObtenerFormatoFecha() + "') Order by cod_cuenta"                       

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            PUC entidad = new PUC();

                            if (resultado["IDLINEA"] != DBNull.Value) entidad.idlinea = Convert.ToInt32(resultado["IDLINEA"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["LINEA"]);

                            lstSuperPuc.Add(entidad);
                        }

                        return lstSuperPuc;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConfecoopData", "ListarTEMP_SUPERSOLI_CONVENIO", ex);
                        return null;
                    }
                }
            }
        }

        public List<PUC> ListarTEMP_SUPERSOLI_PATRIMONIO(PUC pPuc, ref string pError, Usuario vUsuario, Int32 pTipoNorma)
        {
            pError = "";
            DbDataReader resultado = default(DbDataReader);
            List<PUC> lstSuperPuc = new List<PUC>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "P_FECHA";
                        pfecha.Value = pPuc.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        DbParameter pseparador = cmdTransaccionFactory.CreateParameter();
                        pseparador.ParameterName = "PSEPARADOR";
                        pseparador.Value = pPuc.separador;
                        pseparador.Direction = ParameterDirection.Input;
                        pseparador.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pseparador);

                        DbParameter pcuentasNiif = cmdTransaccionFactory.CreateParameter();
                        pcuentasNiif.ParameterName = "PTIPONORMA";
                        pcuentasNiif.Value = pTipoNorma;
                        pcuentasNiif.Direction = ParameterDirection.Input;
                        pcuentasNiif.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcuentasNiif);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SUPER_PATRIMONIO";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                        return null;
                    }
                };


                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;
                        Configuracion conf = new Configuracion();

                        string sql = "Select * from TEMP_SUPERSOLIDARIA order by IDLINEA";  //Where fecha = To_Date('" + Convert.ToDateTime(pEntidad.fecha).ToShortDateString() + "','" + conf.ObtenerFormatoFecha() + "') Order by cod_cuenta"                       

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            PUC entidad = new PUC();

                            if (resultado["IDLINEA"] != DBNull.Value) entidad.idlinea = Convert.ToInt32(resultado["IDLINEA"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["LINEA"]);

                            lstSuperPuc.Add(entidad);
                        }

                        return lstSuperPuc;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConfecoopData", "ListarTEMP_SUPERSOLI_PATRIMONIO", ex);
                        return null;
                    }
                }
            }
        }

        public List<PUC> ListarTEMP_SUPERSOLI_TERCEROS(PUC pPuc, ref string pError, Usuario vUsuario)
        {
            pError = "";
            DbDataReader resultado = default(DbDataReader);
            List<PUC> lstSuperPuc = new List<PUC>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "PFECHA";
                        pfecha.Value = pPuc.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        DbParameter pseparador = cmdTransaccionFactory.CreateParameter();
                        pseparador.ParameterName = "PSEPARADOR";
                        pseparador.Value = pPuc.separador;
                        pseparador.Direction = ParameterDirection.Input;
                        pseparador.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pseparador);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SUPER_TERCEROS";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                        return null;
                    }
                };


                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;
                        Configuracion conf = new Configuracion();

                        string sql = "Select * from TEMP_SUPERSOLIDARIA order by IDLINEA";  //Where fecha = To_Date('" + Convert.ToDateTime(pEntidad.fecha).ToShortDateString() + "','" + conf.ObtenerFormatoFecha() + "') Order by cod_cuenta"                       

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            PUC entidad = new PUC();

                            if (resultado["IDLINEA"] != DBNull.Value) entidad.idlinea = Convert.ToInt32(resultado["IDLINEA"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["LINEA"]);

                            lstSuperPuc.Add(entidad);
                        }

                        return lstSuperPuc;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConfecoopData", "ListarTEMP_SUPERSOLI_PATRIMONIO", ex);
                        return null;
                    }
                }
            }
        }

        public List<PUC> ListarTEMP_SUPERSOLI_PATRONALES(PUC pPuc, ref string pError, Usuario vUsuario)
        {
            pError = "";
            DbDataReader resultado = default(DbDataReader);
            List<PUC> lstSuperPuc = new List<PUC>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "PFECHA";
                        pfecha.Value = pPuc.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        DbParameter pseparador = cmdTransaccionFactory.CreateParameter();
                        pseparador.ParameterName = "PSEPARADOR";
                        pseparador.Value = pPuc.separador;
                        pseparador.Direction = ParameterDirection.Input;
                        pseparador.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pseparador);

                        DbParameter pcuentaniif = cmdTransaccionFactory.CreateParameter();
                        pcuentaniif.ParameterName = "PCUENTANIIF";
                        pcuentaniif.Value = pPuc.maneja_niif;
                        pcuentaniif.Direction = ParameterDirection.Input;
                        pcuentaniif.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcuentaniif);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SUPER_PATRONALES";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                        //BOExcepcion.Throw("ConfecoopData", "USP_XPINN_SUPER_PUC", ex);
                        return null;
                    }
                };


                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;
                        Configuracion conf = new Configuracion();

                        string sql = "Select * from TEMP_SUPERSOLIDARIA order by IDLINEA"; 

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            PUC entidad = new PUC();

                            if (resultado["IDLINEA"] != DBNull.Value) entidad.idlinea = Convert.ToInt32(resultado["IDLINEA"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["LINEA"]);

                            lstSuperPuc.Add(entidad);
                        }

                        return lstSuperPuc;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConfecoopData", "ListarTEMP_SUPERSOLI_PATRONALES", ex);
                        return null;
                    }
                }
            }
        }

        public List<PUC> ListarTEMP_SUPERSOLIDARIA_SALDOS(PUC pPuc, ref string pError, Usuario vUsuario, bool estado, Int32 pTipoNorma)
        {
            pError = "";
            DbDataReader resultado = default(DbDataReader);
            List<PUC> lstSuperPuc = new List<PUC>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();

                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;

                        DbParameter pfecha_ini = cmdTransaccionFactory.CreateParameter();
                        pfecha_ini.ParameterName = "PFECHA_INI";
                        pfecha_ini.Value = pPuc.fecha_ini.ToString(conf.ObtenerFormatoFecha());
                        pfecha_ini.Direction = ParameterDirection.Input;
                        pfecha_ini.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pfecha_ini);

                        DbParameter pfecha_fin = cmdTransaccionFactory.CreateParameter();
                        pfecha_fin.ParameterName = "PFECHA_FIN";
                        pfecha_fin.Value = pPuc.fecha.ToString(conf.ObtenerFormatoFecha());
                        pfecha_fin.Direction = ParameterDirection.Input;
                        pfecha_fin.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pfecha_fin);

                        DbParameter pseparador = cmdTransaccionFactory.CreateParameter();
                        pseparador.ParameterName = "PSEPARADOR";
                        pseparador.Value = pPuc.separador;
                        pseparador.Direction = ParameterDirection.Input;
                        pseparador.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pseparador);

                        DbParameter pcuentasNiif = cmdTransaccionFactory.CreateParameter();
                        pcuentasNiif.ParameterName = "PTIPONORMA";
                        pcuentasNiif.Value = pTipoNorma;
                        pcuentasNiif.Direction = ParameterDirection.Input;
                        pcuentasNiif.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcuentasNiif);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SUPER_SALDOSDIARIOS";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                        //BOExcepcion.Throw("ConfecoopData", "USP_XPINN_SUPER_PUC", ex);
                        return null;
                    }
                };


                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        Configuracion conf = new Configuracion();

                        string sql = "Select * from TEMP_SUPERSOLIDARIA order by IDLINEA";  //Where fecha = To_Date('" + Convert.ToDateTime(pEntidad.fecha).ToShortDateString() + "','" + conf.ObtenerFormatoFecha() + "') Order by cod_cuenta"                       

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            PUC entidad = new PUC();

                            if (resultado["IDLINEA"] != DBNull.Value) entidad.idlinea = Convert.ToInt32(resultado["IDLINEA"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["LINEA"]);

                            lstSuperPuc.Add(entidad);
                        }

                        return lstSuperPuc;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConfecoopData", "ListarTEMP_SUPERSOLIDARIA", ex);
                        return null;
                    }
                }
            }
        }


        public List<PUC> ListarTEMP_SUPERSOLI_CASTIGOS(PUC pPuc, ref string pError, Usuario vUsuario)
        {
            pError = "";
            DbDataReader resultado = default(DbDataReader);
            List<PUC> lstSuperPuc = new List<PUC>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "PFECHA";
                        pfecha.Value = pPuc.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        DbParameter pseparador = cmdTransaccionFactory.CreateParameter();
                        pseparador.ParameterName = "PSEPARADOR";
                        pseparador.Value = pPuc.separador;
                        pseparador.Direction = ParameterDirection.Input;
                        pseparador.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pseparador);

                        DbParameter pcuentaniif = cmdTransaccionFactory.CreateParameter();
                        pcuentaniif.ParameterName = "PCUENTANIIF";
                        pcuentaniif.Value = pPuc.maneja_niif;
                        pcuentaniif.Direction = ParameterDirection.Input;
                        pcuentaniif.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcuentaniif);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SUPER_CASTIGOS";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                        //BOExcepcion.Throw("ConfecoopData", "USP_XPINN_SUPER_PUC", ex);
                        return null;
                    }
                };


                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;
                        Configuracion conf = new Configuracion();

                        string sql = "Select * from TEMP_SUPERSOLICART order by IDLINEA";  //Where fecha = To_Date('" + Convert.ToDateTime(pEntidad.fecha).ToShortDateString() + "','" + conf.ObtenerFormatoFecha() + "') Order by cod_cuenta"                       

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            PUC entidad = new PUC();

                            if (resultado["IDLINEA"] != DBNull.Value) entidad.idlinea = Convert.ToInt32(resultado["IDLINEA"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["LINEA"]);

                            lstSuperPuc.Add(entidad);
                        }

                        return lstSuperPuc;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConfecoopData", "ListarTEMP_SUPERSOLI_CASTIGOS", ex);
                        return null;
                    }
                }
            }
        }



    }
}
