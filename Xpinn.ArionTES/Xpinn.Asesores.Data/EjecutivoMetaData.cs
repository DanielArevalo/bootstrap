using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using System.Data;
using System.Data.Common;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Data
{
    public class EjecutivoMetaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public EjecutivoMetaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public EjecutivoMeta CrearEjecutivoMeta(EjecutivoMeta pEntityEjecMeta, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pIdEjecutivo = cmdTransaccionFactory.CreateParameter();
                        pIdEjecutivo.Direction = ParameterDirection.Input;
                        pIdEjecutivo.ParameterName = "p_idEjecutivo";
                        pIdEjecutivo.Value = pEntityEjecMeta.IdEjecutivo;

                        DbParameter pIdMeta = cmdTransaccionFactory.CreateParameter();
                        pIdMeta.Direction = ParameterDirection.Input;
                        pIdMeta.ParameterName = "p_idMeta";
                        pIdMeta.Value = pEntityEjecMeta.IdMeta;

                        DbParameter pVlrMeta = cmdTransaccionFactory.CreateParameter();
                        pVlrMeta.Direction = ParameterDirection.Input;
                        pVlrMeta.ParameterName = "p_vlrMeta";
                        pVlrMeta.Value = pEntityEjecMeta.VlrMeta;

                        DbParameter pMes = cmdTransaccionFactory.CreateParameter();
                        pMes.ParameterName = "p_mes";
                        pMes.Direction = ParameterDirection.Input;
                        pMes.Value = pEntityEjecMeta.Mes;

                        DbParameter pyear = cmdTransaccionFactory.CreateParameter();
                        pyear.ParameterName = "p_year";
                        pyear.Direction = ParameterDirection.Input;
                        pyear.Value = pEntityEjecMeta.Year;

                        DbParameter pVigencia = cmdTransaccionFactory.CreateParameter();
                        pVigencia.ParameterName = "p_vigencia";
                        pVigencia.Direction = ParameterDirection.Input;
                        pVigencia.Value = pEntityEjecMeta.Vigencia;

                        DbParameter pIdEjecutivoMeta = cmdTransaccionFactory.CreateParameter();
                        pIdEjecutivoMeta.Direction = ParameterDirection.Output;
                        pIdEjecutivoMeta.ParameterName = "p_iasejecmeta";
                        pIdEjecutivoMeta.DbType = DbType.String;
                        pIdEjecutivoMeta.Size = 50;
                        pIdEjecutivoMeta.Value = pEntityEjecMeta.IdEjecutivoMeta.ToString();

                        cmdTransaccionFactory.Parameters.Add(pIdEjecutivo);
                        cmdTransaccionFactory.Parameters.Add(pIdMeta);
                        cmdTransaccionFactory.Parameters.Add(pVlrMeta);
                        cmdTransaccionFactory.Parameters.Add(pMes);
                        cmdTransaccionFactory.Parameters.Add(pyear);
                        cmdTransaccionFactory.Parameters.Add(pVigencia);
                        cmdTransaccionFactory.Parameters.Add(pIdEjecutivoMeta);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_AS_EJECMETA_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //DAauditoria.InsertarLog(pEntityEjecMeta, pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pEntityEjecMeta.IdEjecutivoMeta = Convert.ToInt64(pIdEjecutivoMeta.Value);

                        return pEntityEjecMeta;

                    }
                    catch (DbException ex)
                    {
                        BOExcepcion.Throw("AsesoresData", "CrearCliente", ex);
                        return null;
                    }
                }
            }

        }//end crear

        public EjecutivoMeta CrearMeta(EjecutivoMeta pEntityEjecMeta, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                       
                       
                        DbParameter p_snombremeta = cmdTransaccionFactory.CreateParameter();
                        p_snombremeta.Direction = ParameterDirection.Input;
                        p_snombremeta.ParameterName = "p_snombremeta";
                        p_snombremeta.Value = pEntityEjecMeta.NombreMeta;


                        DbParameter p_sformatometa = cmdTransaccionFactory.CreateParameter();
                        p_sformatometa.Direction = ParameterDirection.Input;
                        p_sformatometa.ParameterName = "p_sformatometa";
                        p_sformatometa.Value = pEntityEjecMeta.formatoMeta;

                        DbParameter pIdMeta = cmdTransaccionFactory.CreateParameter();
                        pIdMeta.Direction = ParameterDirection.Output;
                        pIdMeta.ParameterName = "p_icodmeta";
                        pIdMeta.DbType = DbType.String;
                        pIdMeta.Size = 50;
                        pIdMeta.Value = pEntityEjecMeta.IdMeta.ToString();


                        cmdTransaccionFactory.Parameters.Add(pIdMeta);
                        cmdTransaccionFactory.Parameters.Add(p_snombremeta);
                        cmdTransaccionFactory.Parameters.Add(p_sformatometa);
                        
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_AS_META_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //DAauditoria.InsertarLog(pEntityEjecMeta, pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pEntityEjecMeta.IdMeta = Convert.ToInt64(pIdMeta.Value);

                        return pEntityEjecMeta;

                    }
                    catch (DbException ex)
                    {
                        BOExcepcion.Throw("AsesoresData", "CrearMeta", ex);
                        return null;
                    }
                }
            }

        }//end crear



        public EjecutivoMeta ModificarMeta(EjecutivoMeta pEntityEjecMeta, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {


                        DbParameter p_snombremeta = cmdTransaccionFactory.CreateParameter();
                        p_snombremeta.Direction = ParameterDirection.Input;
                        p_snombremeta.ParameterName = "p_snombremeta";
                        p_snombremeta.Value = pEntityEjecMeta.NombreMeta;


                        DbParameter p_sformatometa = cmdTransaccionFactory.CreateParameter();
                        p_sformatometa.Direction = ParameterDirection.Input;
                        p_sformatometa.ParameterName = "p_sformatometa";
                        p_sformatometa.Value = pEntityEjecMeta.formatoMeta;

                        DbParameter pIdMeta = cmdTransaccionFactory.CreateParameter();
                        pIdMeta.Direction = ParameterDirection.Input;
                        pIdMeta.ParameterName = "p_icodmeta";
                        pIdMeta.DbType = DbType.String;
                        pIdMeta.Size = 50;
                        pIdMeta.Value = pEntityEjecMeta.IdMeta.ToString();


                        cmdTransaccionFactory.Parameters.Add(pIdMeta);
                        cmdTransaccionFactory.Parameters.Add(p_snombremeta);
                        cmdTransaccionFactory.Parameters.Add(p_sformatometa);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_AS_META_MODIFICAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //DAauditoria.InsertarLog(pEntityEjecMeta, pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pEntityEjecMeta.IdMeta = Convert.ToInt64(pIdMeta.Value);

                        return pEntityEjecMeta;

                    }
                    catch (DbException ex)
                    {
                        BOExcepcion.Throw("AsesoresData", "ModificarMeta", ex);
                        return null;
                    }
                }
            }

        }
         public List<EjecutivoMeta> ListarEjecutivos(EjecutivoMeta pEntityEjecMeta, Usuario pUsuario)
        {
            DbDataReader reader = default(DbDataReader);
            List<EjecutivoMeta> lstPrograma = new List<EjecutivoMeta>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                  try
                    {
                        string sql = "SELECT ej.IUSUARIO,ej.snombre1 ||' '||ej.sapellido1 ||' '||ej.sapellido2 as NOMBRE, ofi.NOMBRE NombreOficina,ofi.COD_OFICINA frOM asejecutivos ej INNER JOIN OFICINA ofi ON ofi.COD_OFICINA = ej.ioficina  WHERE  iestado=1 ORDER BY ofi.COD_OFICINA ASC";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        reader = cmdTransaccionFactory.ExecuteReader();

                       
                        while (reader.Read())
                        {
                            EjecutivoMeta entityEjecutivoMeta = new EjecutivoMeta();

                            if (reader["IUSUARIO"] != DBNull.Value) entityEjecutivoMeta.IdEjecutivo = Convert.ToInt64(reader["IUSUARIO"].ToString());
                            if (reader["NOMBRE"] != DBNull.Value) entityEjecutivoMeta.Nombres = reader["NOMBRE"].ToString();
                            if (reader["NombreOficina"] != DBNull.Value) entityEjecutivoMeta.NombreOficina = Convert.ToString(reader["NombreOficina"].ToString());
                            if (reader["COD_OFICINA"] != DBNull.Value) entityEjecutivoMeta.Codficina = Convert.ToInt64(reader["COD_OFICINA"].ToString());                           

                            lstPrograma.Add(entityEjecutivoMeta);
                        }

                        return lstPrograma;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DataEjecutivoMeta", "ListarEjecutivo", ex);
                        return null;
                    }
                }            
         }
    }

         public EjecutivoMeta ConsultarMeta(Usuario pUsuario, String filtro)
         {
             DbDataReader reader = default(DbDataReader);
             List<EjecutivoMeta> lstPrograma = new List<EjecutivoMeta>();
             EjecutivoMeta entityEjecutivoMeta = new EjecutivoMeta();

             using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
             {
                 using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                 {
                     try
                     {
                         string sql = "SELECT * from asmeta" + filtro;

                         connection.Open();
                         cmdTransaccionFactory.Connection = connection;
                         cmdTransaccionFactory.CommandType = CommandType.Text;
                         cmdTransaccionFactory.CommandText = sql;
                         reader = cmdTransaccionFactory.ExecuteReader();


                         if (reader.Read())
                         {
                            
                             if (reader["ICODMETA"] != DBNull.Value) entityEjecutivoMeta.IdMeta = Convert.ToInt64(reader["ICODMETA"].ToString());
                             if (reader["SNOMBREMETA"] != DBNull.Value) entityEjecutivoMeta.NombreMeta = reader["SNOMBREMETA"].ToString();
                             if (reader["SFORMATOMETA"] != DBNull.Value) entityEjecutivoMeta.formatoMeta = Convert.ToString(reader["SFORMATOMETA"].ToString());                           

                            
                         }

                         return entityEjecutivoMeta;
                     }
                     catch (Exception ex)
                     {
                         BOExcepcion.Throw("DataEjecutivoMeta", "ConsultarMeta", ex);
                         return null;
                     }
                 }
             }
         }

         public EjecutivoMeta ConsultarMetas(Usuario pUsuario, String idobjeto)
         {
             DbDataReader reader = default(DbDataReader);
             List<EjecutivoMeta> lstPrograma = new List<EjecutivoMeta>();
             EjecutivoMeta entityEjecutivoMeta = new EjecutivoMeta();

             using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
             {
                 using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                 {
                     try
                     {
                         string sql = "SELECT * from asmeta where ICODMETA =" + idobjeto;

                         connection.Open();
                         cmdTransaccionFactory.Connection = connection;
                         cmdTransaccionFactory.CommandType = CommandType.Text;
                         cmdTransaccionFactory.CommandText = sql;
                         reader = cmdTransaccionFactory.ExecuteReader();


                         if (reader.Read())
                         {

                             if (reader["ICODMETA"] != DBNull.Value) entityEjecutivoMeta.IdMeta = Convert.ToInt64(reader["ICODMETA"].ToString());
                             if (reader["SNOMBREMETA"] != DBNull.Value) entityEjecutivoMeta.NombreMeta = reader["SNOMBREMETA"].ToString();
                             if (reader["SFORMATOMETA"] != DBNull.Value) entityEjecutivoMeta.formatoMeta = Convert.ToString(reader["SFORMATOMETA"].ToString());


                         }

                         return entityEjecutivoMeta;
                     }
                     catch (Exception ex)
                     {
                         BOExcepcion.Throw("DataEjecutivoMeta", "ConsultarMetas", ex);
                         return null;
                     }
                 }
             }
         }
        public List<EjecutivoMeta> ListarEjecutivoMeta(EjecutivoMeta pEntityEjecMeta, Usuario pUsuario)
        {
            DbDataReader reader = default(DbDataReader);
            List<EjecutivoMeta> lstPrograma = new List<EjecutivoMeta>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText =
                            @"SELECT em.iasejecmeta, em.icodigo,ej.snombre1,ej.snombre2,ej.sapellido1, em.icodmeta, 
	                            m.snombremeta,em.iexpr, em.mes,em.years,em.vigencia, ofi.NOMBRE nombOficina, m.snombremeta, em.iexpr vlrmeta
	                            FROM   asejecmeta em INNER JOIN asejecutivos ej ON ej.icodigo = em.icodigo
	                            INNER JOIN asmeta m ON m.icodmeta = em.icodmeta
	                            INNER JOIN OFICINA ofi ON ofi.COD_OFICINA = ej.ioficina
                                 WHERE em.mes = " + Convert.ToInt32(pEntityEjecMeta.Mes) + " and em.years = " + pEntityEjecMeta.Year;
                                
                        reader = cmdTransaccionFactory.ExecuteReader();

                        while (reader.Read())
                        {
                            EjecutivoMeta entityEjecutivoMeta = new EjecutivoMeta();

                            if (reader["iasejecmeta"] != DBNull.Value) entityEjecutivoMeta.IdEjecutivoMeta = Convert.ToInt64(reader["iasejecmeta"].ToString());
                            if (reader["icodigo"] != DBNull.Value) entityEjecutivoMeta.IdEjecutivo = Convert.ToInt64(reader["icodigo"].ToString());
                            if (reader["snombre1"] != DBNull.Value) entityEjecutivoMeta.PrimerNombre = reader["snombre1"].ToString();
                            if (reader["snombre2"] != DBNull.Value) entityEjecutivoMeta.SegundoNombre = reader["snombre2"].ToString();
                            if (reader["sapellido1"] != DBNull.Value) entityEjecutivoMeta.PrimerApellido = reader["sapellido1"].ToString();
                            if (reader["icodmeta"] != DBNull.Value) entityEjecutivoMeta.IdMeta = Convert.ToInt64(reader["icodmeta"].ToString());
                            if (reader["snombremeta"] != DBNull.Value) entityEjecutivoMeta.NombreMeta = reader["snombremeta"].ToString();
                            if (reader["iexpr"] != DBNull.Value) entityEjecutivoMeta.VlrMeta = Convert.ToInt64(reader["iexpr"].ToString());
                            if (reader["mes"] != DBNull.Value) entityEjecutivoMeta.Mes = reader["mes"].ToString();
                            if (reader["vigencia"] != DBNull.Value) entityEjecutivoMeta.Vigencia = reader["vigencia"].ToString();
                            if (reader["nombOficina"] != DBNull.Value) entityEjecutivoMeta.NombreOficina = reader["nombOficina"].ToString();
                            if (reader["vlrmeta"] != DBNull.Value) entityEjecutivoMeta.VlrMeta = Convert.ToInt64(reader["vlrmeta"].ToString());
                            if (reader["years"] != DBNull.Value) entityEjecutivoMeta.Year = reader["years"].ToString();
                            lstPrograma.Add(entityEjecutivoMeta);
                        }

                        return lstPrograma;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DataEjecutivoMeta", "ListarEjecutivoMeta", ex);
                        return null;
                    }
                }
            }

        }
        public List<EjecutivoMeta> ListarMeta( Usuario pUsuario)
        {
            DbDataReader reader = default(DbDataReader);
            List<EjecutivoMeta> lstPrograma = new List<EjecutivoMeta>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText =
                            @"SELECT icodmeta,snombremeta from asmeta";

                        reader = cmdTransaccionFactory.ExecuteReader();

                        while (reader.Read())
                        {
                            EjecutivoMeta entityEjecutivoMeta = new EjecutivoMeta();

                            if (reader["icodmeta"] != DBNull.Value) entityEjecutivoMeta.IdMeta = Convert.ToInt64(reader["icodmeta"].ToString());
                            if (reader["snombremeta"] != DBNull.Value) entityEjecutivoMeta.NombreMeta = reader["snombremeta"].ToString();
                            
                            lstPrograma.Add(entityEjecutivoMeta);
                        }

                        return lstPrograma;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DataEjecutivoMeta", "ListarMeta", ex);
                        return null;
                    }
                }
            }

        }
        public List<EjecutivoMeta> ListarMetas(EjecutivoMeta pEntityMeta, Usuario pUsuario)
        {
            DbDataReader reader = default(DbDataReader);
            List<EjecutivoMeta> lstPrograma = new List<EjecutivoMeta>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText =
                            @"SELECT icodmeta,snombremeta,sformatometa from asmeta";

                        reader = cmdTransaccionFactory.ExecuteReader();

                        while (reader.Read())
                        {
                            EjecutivoMeta entityEjecutivoMeta = new EjecutivoMeta();

                            if (reader["icodmeta"] != DBNull.Value) entityEjecutivoMeta.IdMeta = Convert.ToInt64(reader["icodmeta"].ToString());
                            if (reader["snombremeta"] != DBNull.Value) entityEjecutivoMeta.NombreMeta = reader["snombremeta"].ToString();
                            if (reader["sformatometa"] != DBNull.Value) entityEjecutivoMeta.formatoMeta = reader["sformatometa"].ToString();

                            lstPrograma.Add(entityEjecutivoMeta);
                        }

                        return lstPrograma;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DataEjecutivoMeta", "ListarMetas", ex);
                        return null;
                    }
                }
            }

        }
        public List<EjecutivoMeta> ListarMetasFiltro(String filtro , Usuario pUsuario)
        {
            DbDataReader reader = default(DbDataReader);
            List<EjecutivoMeta> lstPrograma = new List<EjecutivoMeta>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText =
                            @"SELECT icodmeta,snombremeta,sformatometa from asmeta  " + filtro;
                        reader = cmdTransaccionFactory.ExecuteReader();

                        while (reader.Read())
                        {
                            EjecutivoMeta entityEjecutivoMeta = new EjecutivoMeta();

                            if (reader["icodmeta"] != DBNull.Value) entityEjecutivoMeta.IdMeta = Convert.ToInt64(reader["icodmeta"].ToString());
                            if (reader["snombremeta"] != DBNull.Value) entityEjecutivoMeta.NombreMeta = reader["snombremeta"].ToString();
                            if (reader["sformatometa"] != DBNull.Value) entityEjecutivoMeta.formatoMeta = reader["sformatometa"].ToString();

                            lstPrograma.Add(entityEjecutivoMeta);
                        }

                        return lstPrograma;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DataEjecutivoMeta", "ListarMetas", ex);
                        return null;
                    }
                }
            }

        }
       
        public List<EjecutivoMeta> ListarPeriodicidad(Usuario pUsuario)
        {
            DbDataReader reader = default(DbDataReader);
            List<EjecutivoMeta> lstPrograma = new List<EjecutivoMeta>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText =
                            @"SELECT cod_periodicidad,descripcion from periodicidad";

                        reader = cmdTransaccionFactory.ExecuteReader();

                        while (reader.Read())
                        {
                            EjecutivoMeta entityEjecutivoMeta = new EjecutivoMeta();

                            if (reader["cod_periodicidad"] != DBNull.Value) entityEjecutivoMeta.IdPeriodicidad = Convert.ToInt64(reader["cod_periodicidad"].ToString());
                            if (reader["descripcion"] != DBNull.Value) entityEjecutivoMeta.DescripcionPeriodicidad = reader["descripcion"].ToString();

                            lstPrograma.Add(entityEjecutivoMeta);
                        }

                        return lstPrograma;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DataEjecutivoMeta", "Listarperiodicidad", ex);
                        return null;
                    }
                }
            }

        }
        public void Eliminar(Int64 pIdEjecutivoMeta, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        EjecutivoMeta entityEjecutivoMeta = new EjecutivoMeta();

                        //if (pUsuario.programaGeneraLog)
                        // aseEntEjecutivo = ConsultarEjecutivo(pIdEjecutivo, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pIdPrograma = cmdTransaccionFactory.CreateParameter();
                        pIdPrograma.Direction = ParameterDirection.Input;
                        pIdPrograma.ParameterName = "P_IDCODIGO";
                        pIdPrograma.Value = pIdEjecutivoMeta;

                        cmdTransaccionFactory.Parameters.Add(pIdPrograma);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_AS_EJEMETA_ELIMINAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //DAauditoria.InsertarLog(aseEntEjecutivo, pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EjecutivoMetaData", "Eliminar", ex);
                    }
                }
            }
        }
        public void EliminarMeta(Int64 pIdEjecutivoMeta, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        EjecutivoMeta entityEjecutivoMeta = new EjecutivoMeta();

                        //if (pUsuario.programaGeneraLog)
                        // aseEntEjecutivo = ConsultarEjecutivo(pIdEjecutivo, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pIdPrograma = cmdTransaccionFactory.CreateParameter();
                        pIdPrograma.Direction = ParameterDirection.Input;
                        pIdPrograma.ParameterName = "P_IDCODIGO";
                        pIdPrograma.Value = pIdEjecutivoMeta;

                        cmdTransaccionFactory.Parameters.Add(pIdPrograma);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_AS_META_ELIMINAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //DAauditoria.InsertarLog(aseEntEjecutivo, pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EjecutivoMetaData", "Eliminar", ex);
                    }
                }
            }
        }

      
        public EjecutivoMeta Actualizar(EjecutivoMeta pEntityEjecMeta, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter p_iasejecmeta = cmdTransaccionFactory.CreateParameter();
                        p_iasejecmeta.Direction = ParameterDirection.Input;
                        p_iasejecmeta.ParameterName = "p_iasejecmeta";
                        p_iasejecmeta.Value = pEntityEjecMeta.IdEjecutivoMeta;

                        DbParameter p_iexpr = cmdTransaccionFactory.CreateParameter();
                        p_iexpr.Direction = ParameterDirection.Input;
                        p_iexpr.ParameterName = "p_iexpr";
                        p_iexpr.Value = pEntityEjecMeta.VlrMeta;

                        DbParameter p_icodmeta = cmdTransaccionFactory.CreateParameter();
                        p_icodmeta.Direction = ParameterDirection.Input;
                        p_icodmeta.ParameterName = "p_icodmeta";
                        p_icodmeta.Value = pEntityEjecMeta.IdMeta;

                        DbParameter p_mes = cmdTransaccionFactory.CreateParameter();
                        p_mes.Direction = ParameterDirection.Input;
                        p_mes.ParameterName = "p_mes";
                        p_mes.Value = pEntityEjecMeta.Mes;

                      
                        DbParameter pIdEjecutivo = cmdTransaccionFactory.CreateParameter();
                        pIdEjecutivo.Direction = ParameterDirection.Input;
                        pIdEjecutivo.ParameterName = "pIdEjecutivo";
                        pIdEjecutivo.Value = pEntityEjecMeta.IdEjecutivo;

                        DbParameter p_year = cmdTransaccionFactory.CreateParameter();
                        p_year.ParameterName = "p_year";
                        p_year.Direction = ParameterDirection.Input;
                        p_year.Value = pEntityEjecMeta.Year;

                        DbParameter p_vigencia = cmdTransaccionFactory.CreateParameter();
                        p_vigencia.ParameterName = "p_vigencia";
                        p_vigencia.Direction = ParameterDirection.Input;
                        p_vigencia.Value = pEntityEjecMeta.Vigencia;

                         DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "pfecha";
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.Value = pEntityEjecMeta.Fecha;



                        cmdTransaccionFactory.Parameters.Add(p_iasejecmeta);
                        cmdTransaccionFactory.Parameters.Add(pIdEjecutivo);
                        cmdTransaccionFactory.Parameters.Add(p_icodmeta);
                        cmdTransaccionFactory.Parameters.Add(p_iexpr);                        
                        cmdTransaccionFactory.Parameters.Add(p_mes);
                        cmdTransaccionFactory.Parameters.Add(p_year);
                        cmdTransaccionFactory.Parameters.Add(pfecha);
                        cmdTransaccionFactory.Parameters.Add(p_vigencia);

                       
                        pIdEjecutivo.Value = pEntityEjecMeta.IdEjecutivo;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_AS_EJECMETA_ACTUALIZAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                           // DAauditoria.InsertarLog(pEntityEjecMeta, "ASEJECUTIVOS", pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pEntityEjecMeta.IdEjecutivo = Convert.ToInt64(pIdEjecutivo.Value);

                        return pEntityEjecMeta;

                    }
                    catch (DbException ex)
                    {

                        BOExcepcion.Throw("EjecutivoMetaData", "Actualizar", ex);
                        return null;
                    }//end catch
                }
            }
        }//end crear
    }
}