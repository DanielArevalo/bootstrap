using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using System.Data;
using System.Reflection;
using System.Data.Common;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Data
{
    public class EjecutivoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public EjecutivoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public Ejecutivo Crear(Ejecutivo pAseEntiEjecutivo, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pPrimerNombre = cmdTransaccionFactory.CreateParameter();
                        pPrimerNombre.Direction = ParameterDirection.Input;
                        pPrimerNombre.ParameterName = "P_SNOMBRE1";
                        pPrimerNombre.Value = pAseEntiEjecutivo.PrimerNombre;

                        DbParameter pSegundoNombre = cmdTransaccionFactory.CreateParameter();
                        pSegundoNombre.Direction = ParameterDirection.Input;
                        pSegundoNombre.ParameterName = "P_SNOMBRE2";
                        pSegundoNombre.Value = pAseEntiEjecutivo.SegundoNombre;

                        DbParameter pPrimerApellido = cmdTransaccionFactory.CreateParameter();
                        pPrimerApellido.Direction = ParameterDirection.Input;
                        pPrimerApellido.ParameterName = "P_SAPELLIDO1";
                        pPrimerApellido.Value = pAseEntiEjecutivo.PrimerApellido;

                        DbParameter pSegundoApellido = cmdTransaccionFactory.CreateParameter();
                        pSegundoApellido.Direction = ParameterDirection.Input;
                        pSegundoApellido.ParameterName = "P_SAPELLIDO2";
                        pSegundoApellido.Value = pAseEntiEjecutivo.SegundoApellido;

                        DbParameter pTipoDocumento = cmdTransaccionFactory.CreateParameter();
                        pTipoDocumento.Direction = ParameterDirection.Input;
                        pTipoDocumento.ParameterName = "P_ITIPOIDEN";
                        pTipoDocumento.DbType = DbType.String;
                        pTipoDocumento.Value = pAseEntiEjecutivo.TipoDocumento.ToString();

                        DbParameter pNumeroDocumento = cmdTransaccionFactory.CreateParameter();
                        pNumeroDocumento.Direction = ParameterDirection.Input;
                        pNumeroDocumento.ParameterName = "P_SIDENTIFICACION";
                        pNumeroDocumento.DbType = DbType.String;
                        pNumeroDocumento.Value = pAseEntiEjecutivo.NumeroDocumento.ToString();

                        DbParameter pDireccion = cmdTransaccionFactory.CreateParameter();
                        pDireccion.Direction = ParameterDirection.Input;
                        pDireccion.ParameterName = "P_SDIRECCION";
                        pDireccion.Value = pAseEntiEjecutivo.Direccion;

                        DbParameter pBarrio = cmdTransaccionFactory.CreateParameter();
                        pBarrio.Direction = ParameterDirection.Input;
                        pBarrio.ParameterName = "P_SBARRIO";
                        pBarrio.Value = pAseEntiEjecutivo.Barrio;

                        DbParameter pTelefono = cmdTransaccionFactory.CreateParameter();
                        pTelefono.Direction = ParameterDirection.Input;
                        pTelefono.DbType = DbType.String;
                        pTelefono.ParameterName = "P_STELEFONO";
                        pTelefono.Value = pAseEntiEjecutivo.Telefono.ToString();

                        DbParameter pTelefonoCel = cmdTransaccionFactory.CreateParameter();
                        pTelefonoCel.Direction = ParameterDirection.Input;
                        pTelefonoCel.DbType = DbType.String;
                        pTelefonoCel.ParameterName = "P_STELEFONOCEL";
                        pTelefonoCel.Value = pAseEntiEjecutivo.TelefonoCel.ToString();

                        DbParameter pEmail = cmdTransaccionFactory.CreateParameter();
                        pEmail.Direction = ParameterDirection.Input;
                        pEmail.DbType = DbType.String;
                        pEmail.ParameterName = "P_SMAIL";
                        pEmail.Value = pAseEntiEjecutivo.Email;

                        DbParameter pFechaIngreso = cmdTransaccionFactory.CreateParameter();
                        pFechaIngreso.ParameterName = "P_FREGISTRO";
                        pFechaIngreso.Direction = ParameterDirection.Input;
                        pFechaIngreso.Value = pAseEntiEjecutivo.FechaIngreso;

                        DbParameter pidOficina = cmdTransaccionFactory.CreateParameter();
                        pidOficina.Direction = ParameterDirection.Input;
                        pidOficina.ParameterName = "P_IOFICINA";
                        pidOficina.DbType = DbType.String;
                        pidOficina.Value = pAseEntiEjecutivo.IdOficina.ToString();

                        DbParameter pidEstado = cmdTransaccionFactory.CreateParameter();
                        pidEstado.Direction = ParameterDirection.Input;
                        pidEstado.ParameterName = "P_IESTADO";
                        pidEstado.DbType = DbType.String;
                        pidEstado.Value = pAseEntiEjecutivo.IdEstado.ToString();

                        DbParameter pidZona = cmdTransaccionFactory.CreateParameter();
                        pidZona.Direction = ParameterDirection.Input;
                        pidZona.ParameterName = "P_ICODZONA";
                        pidZona.DbType = DbType.String;
                        pidZona.Value = pAseEntiEjecutivo.IdZona.ToString();

                        DbParameter pIdEjecutivo = cmdTransaccionFactory.CreateParameter();
                        pIdEjecutivo.Direction = ParameterDirection.Output;
                        pIdEjecutivo.ParameterName = "P_IDCODIGO";
                        pIdEjecutivo.DbType = DbType.Int64;
                        pIdEjecutivo.Value = pAseEntiEjecutivo.IdEjecutivo.ToString();

                        cmdTransaccionFactory.Parameters.Add(pPrimerNombre);
                        cmdTransaccionFactory.Parameters.Add(pSegundoNombre);
                        cmdTransaccionFactory.Parameters.Add(pPrimerApellido);
                        cmdTransaccionFactory.Parameters.Add(pSegundoApellido);
                        cmdTransaccionFactory.Parameters.Add(pTipoDocumento);
                        cmdTransaccionFactory.Parameters.Add(pNumeroDocumento);
                        cmdTransaccionFactory.Parameters.Add(pDireccion);
                        cmdTransaccionFactory.Parameters.Add(pBarrio);
                        cmdTransaccionFactory.Parameters.Add(pTelefono);
                        cmdTransaccionFactory.Parameters.Add(pTelefonoCel);
                        cmdTransaccionFactory.Parameters.Add(pEmail);
                        cmdTransaccionFactory.Parameters.Add(pFechaIngreso);
                        cmdTransaccionFactory.Parameters.Add(pidOficina);
                        cmdTransaccionFactory.Parameters.Add(pidEstado);
                        cmdTransaccionFactory.Parameters.Add(pidZona);
                        cmdTransaccionFactory.Parameters.Add(pIdEjecutivo);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_AS_EJECUTIVO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pAseEntiEjecutivo, "ASEJECUTIVOS", pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pAseEntiEjecutivo.IdEjecutivo = Convert.ToInt64(pIdEjecutivo.Value);

                        return pAseEntiEjecutivo;

                    }
                    catch (DbException ex)
                    {
                        BOExcepcion.Throw("AsesoresData", "CrearCliente", ex);
                        return null;
                    }
                }
            }
        }//end crear

        public void Eliminar(Int64 pIdEjecutivo, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Ejecutivo aseEntEjecutivo = new Ejecutivo();

                        //if (pUsuario.programaGeneraLog)
                        // aseEntEjecutivo = ConsultarEjecutivo(pIdEjecutivo, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pIdPrograma = cmdTransaccionFactory.CreateParameter();
                        pIdPrograma.Direction = ParameterDirection.Input;
                        pIdPrograma.ParameterName = "P_IDCODIGO";
                        pIdPrograma.Value = pIdEjecutivo;

                        cmdTransaccionFactory.Parameters.Add(pIdPrograma);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_AS_EJECUTIVO_ELIMINAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //DAauditoria.InsertarLog(aseEntEjecutivo, pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AsesoresData", "EliminarEjecutivo", ex);
                    }
                }
            }
        }
        public string DetalleBarriosEjecutivo(Int64 pIdAseEntiEjecutivo, Usuario pUsuario)
        {
            DbDataReader reader = default(DbDataReader);

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string barrios = "";
                        string sql = "SELECT B.NOMCIUDAD AS DESCRIPCION FROM EJECUTIVO_BARRIOS E INNER JOIN CIUDADES B ON E.COD_BARRIO = B.CODCIUDAD WHERE E.COD_EJECTIVO = " + pIdAseEntiEjecutivo;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        reader = cmdTransaccionFactory.ExecuteReader();

                        while (reader.Read())
                        {
                            if (reader["DESCRIPCION"] != DBNull.Value) barrios += Convert.ToString(reader["DESCRIPCION"]) + ", ";
                        }
                        return barrios;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProgramaData", "DetalleBarriosEjecutivo", ex);
                        return "";
                    }
                }
            }

        }
        public string DetalleZonasEjecutivo(Int64 pIdAseEntiEjecutivo, Usuario pUsuario)
        {
            DbDataReader reader = default(DbDataReader);

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string zonas = "";
                        string sql = "SELECT Z.DESCRIPCION FROM EJECUTIVO_ZONAS E INNER JOIN ZONAS Z ON E.COD_ZONA = Z.COD_ZONA WHERE E.COD_EJECUTIVO = " + pIdAseEntiEjecutivo;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        reader = cmdTransaccionFactory.ExecuteReader();

                        while (reader.Read())
                        {
                            if (reader["DESCRIPCION"] != DBNull.Value) zonas += Convert.ToString(reader["DESCRIPCION"])+", ";
                        }
                        return zonas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProgramaData", "ConsultarPrograma", ex);
                        return "";
                    }
                }
            }

        }
        public Ejecutivo Consultar(Int64 pIdAseEntiEjecutivo, Usuario pUsuario)
        {





            DbDataReader reader = default(DbDataReader);
            Ejecutivo aseEntEjecutivo = new Ejecutivo();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        //DbParameter pIdEjecutivo = cmdTransaccionFactory.CreateParameter();
                        //pIdEjecutivo.Direction = ParameterDirection.Input;
                        //pIdEjecutivo.ParameterName = "P_IDCODIGO";
                        //pIdEjecutivo.Value = pIdAseEntiEjecutivo;

                        //OracleParameter pData = (OracleParameter)cmdTransaccionFactory.CreateParameter();
                        //pData.ParameterName = "P_DATA";
                        //pData.Direction = ParameterDirection.Output;
                        //pData.OracleType = OracleType.Cursor;

                        //cmdTransaccionFactory.Parameters.Add(pIdEjecutivo);
                        //cmdTransaccionFactory.Parameters.Add(pData);

                        //connection.Open();
                        //cmdTransaccionFactory.Connection = connection;
                        //cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        //cmdTransaccionFactory.CommandText = "XPF_AS_EJECUTIVO_CONSULTAR";
                        //reader = cmdTransaccionFactory.ExecuteReader();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText =
                            @"Select Ej.Icodigo, ej.iusuario, ej.snombre1, ej.snombre2, ej.sapellido1, ej.sapellido2, Ej.Itipoiden,
                              ej.sidentificacion, T.Descripcion Nombidentificacion, Ej.Sdireccion,  Ej.Sbarrio,  Ej.Stelefono, 
                              ej.stelefonoCelular, ej.semail,  Ej.Fingreso,  ej.ioficina, ofi.nombre nombOficina, Ej.Iestado, 
                              es.descripcion nombEstado, ej.icodzona, z.NOMCIUDAD
                              FROM    asejecutivos ej INNER JOIN tipoidentificacion t ON ej.itipoiden = t.codtipoidentificacion
		                              INNER JOIN oficina ofi ON ej.ioficina = ofi.cod_oficina
		                              INNER JOIN asestado es ON es.iestado = ej.iestado
		                              LEFT JOIN Ciudades Z On Ej.Icodzona = Z.Codciudad
                              WHERE    Ej.Icodigo=" + pIdAseEntiEjecutivo;
                        reader = cmdTransaccionFactory.ExecuteReader();

                        if (reader.Read())
                        {
                            if (reader["icodigo"] != DBNull.Value)      aseEntEjecutivo.IdEjecutivo     = Convert.ToInt64(reader["icodigo"].ToString());
                            if (reader["ICODIGO"] != DBNull.Value)      aseEntEjecutivo.Codigo          = reader["ICODIGO"].ToString();
                            if (reader["iusuario"] != DBNull.Value)     aseEntEjecutivo.IdUsuario       = Convert.ToInt64(reader["iusuario"].ToString());
                            if (reader["SNOMBRE1"] != DBNull.Value)     aseEntEjecutivo.PrimerNombre    = reader["SNOMBRE1"].ToString();
                            if (reader["SNOMBRE2"] != DBNull.Value)     aseEntEjecutivo.SegundoNombre   = reader["SNOMBRE2"].ToString();
                            if (reader["SAPELLIDO1"] != DBNull.Value)   aseEntEjecutivo.PrimerApellido  = reader["SAPELLIDO1"].ToString();
                            if (reader["SAPELLIDO2"] != DBNull.Value)   aseEntEjecutivo.SegundoApellido = reader["SAPELLIDO2"].ToString();
                            if (reader["ITIPOIDEN"] != DBNull.Value)    aseEntEjecutivo.TipoDocumento   = Convert.ToInt64(reader["ITIPOIDEN"].ToString());
                            if (reader["SDIRECCION"] != DBNull.Value)   aseEntEjecutivo.Direccion       = reader["SDIRECCION"].ToString();
                            if (reader["SBARRIO"] != DBNull.Value)      aseEntEjecutivo.Barrio          = reader["SBARRIO"].ToString();
                            if (reader["STELEFONO"] != DBNull.Value)    aseEntEjecutivo.Telefono        = Convert.ToInt64(reader["STELEFONO"].ToString());
                            if (reader["stelefonoCelular"] != DBNull.Value) aseEntEjecutivo.TelefonoCel = Convert.ToInt64(reader["stelefonoCelular"].ToString());
                            if (reader["SEMAIL"] != DBNull.Value)       aseEntEjecutivo.Email           = reader["SEMAIL"].ToString();
                            if (reader["FINGRESO"] != DBNull.Value)     aseEntEjecutivo.FechaIngreso    = Convert.ToDateTime(reader["FINGRESO"].ToString()); //Convert.ToDateTime(txtFechaIngreso.Text).ToLongDateString();
                            if (reader["ioficina"] != DBNull.Value)     aseEntEjecutivo.IdOficina       = Convert.ToInt64(reader["ioficina"].ToString());
                            if (reader["nombOficina"] != DBNull.Value)  aseEntEjecutivo.NombreOficina   = reader["nombOficina"].ToString();
                            if (reader["iestado"] != DBNull.Value)      aseEntEjecutivo.IdEstado        = Convert.ToInt64(reader["iestado"].ToString());
                            if (reader["nombEstado"] != DBNull.Value)   aseEntEjecutivo.NombreEstado    = reader["nombEstado"].ToString();
                            if (reader["SIDENTIFICACION"] != DBNull.Value) aseEntEjecutivo.NumeroDocumento = reader["SIDENTIFICACION"].ToString();
                            if (reader["nombIdentificacion"] != DBNull.Value) aseEntEjecutivo.NombreTipoDocumento = reader["nombIdentificacion"].ToString();
                            if (reader["icodzona"] != DBNull.Value)     aseEntEjecutivo.IdZona          = Convert.ToInt64(reader["icodzona"].ToString());
                            if (reader["NOMCIUDAD"] != DBNull.Value) aseEntEjecutivo.NombreZona = reader["NOMCIUDAD"].ToString();
                        }
                      //  else
                        //{
                          //  throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        //}
                        return aseEntEjecutivo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProgramaData", "ConsultarPrograma", ex);
                        return null;
                    }
                }
            }

        }
        public Ejecutivo ConsultarDatosEjecutivo(Int64 pId, Usuario pUsuario)
        {
                     


            DbDataReader reader = default(DbDataReader);
            Ejecutivo aseEntEjecutivo = new Ejecutivo();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        string sql = "SELECT * FROM V_ASEJECUTIVOS_ACTIVOS WHERE icodigo = " + pId.ToString();
                                                                    
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        reader = cmdTransaccionFactory.ExecuteReader();

                        if (reader.Read())
                        {
                            if (reader["icodigo"] != DBNull.Value) aseEntEjecutivo.IdEjecutivo = Convert.ToInt64(reader["icodigo"].ToString());
                            if (reader["NOMBRECOMPLETO"] != DBNull.Value) aseEntEjecutivo.PrimerNombre = reader["NOMBRECOMPLETO"].ToString();
                            if (reader["SEMAIL"] != DBNull.Value) aseEntEjecutivo.Email = reader["SEMAIL"].ToString();
                            if (reader["ioficina"] != DBNull.Value) aseEntEjecutivo.IdOficina = Convert.ToInt64(reader["ioficina"].ToString());
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        return aseEntEjecutivo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EjecutivoData", "ConsultarDatosEjecutivo", ex);
                        return null;
                    }
                }
            }

        }


        public Ejecutivo ConsultarDatosDirector(Int64 pId, Usuario pUsuario)
        {

            DbDataReader reader = default(DbDataReader);
            Ejecutivo aseEntEjecutivo = new Ejecutivo();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        string sql = "SELECT u.codusuario, p.email FROM usuarios u INNER JOIN persona p ON u.identificacion = p.identificacion WHERE u.codperfil = 9 AND u.cod_oficina = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        reader = cmdTransaccionFactory.ExecuteReader();

                        if (reader.Read())
                        {
                            if (reader["CODUSUARIO"] != DBNull.Value) aseEntEjecutivo.IdDirector = Convert.ToInt64(reader["CODUSUARIO"].ToString());
                            if (reader["EMAIL"] != DBNull.Value) aseEntEjecutivo.Email = Convert.ToString(reader["EMAIL"].ToString());
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        return aseEntEjecutivo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EjecutivoData", "ConsultarDatosDirector", ex);
                        return null;
                    }
                }
            }

        }
        public Int64 eliminarZonasEjecutivo(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter p_cod_ejecutivo = cmdTransaccionFactory.CreateParameter();
                        p_cod_ejecutivo.Direction = ParameterDirection.Input;
                        p_cod_ejecutivo.ParameterName = "P_COD_EJECUTIVO";
                        p_cod_ejecutivo.Value = pId;
                        cmdTransaccionFactory.Parameters.Add(p_cod_ejecutivo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_EJE_ZONA_ELIM";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        return pId;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EjecutivoData", "eliminarZonasEjecutivo", ex);
                        return pId;
                    }
                }
            }

        }
        public Ejecutivo guardarZonasEjecutivo(Ejecutivo zonaEjecutivo, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_ejecutivo = cmdTransaccionFactory.CreateParameter();
                        p_cod_ejecutivo.Direction = ParameterDirection.Input;
                        p_cod_ejecutivo.ParameterName = "P_COD_EJECUTIVO";
                        p_cod_ejecutivo.Value = zonaEjecutivo.IdEjecutivo;
                        cmdTransaccionFactory.Parameters.Add(p_cod_ejecutivo);

                        DbParameter p_zona = cmdTransaccionFactory.CreateParameter();
                        p_zona.Direction = ParameterDirection.Input;
                        p_zona.ParameterName = "P_COD_ZONA";
                        p_zona.Value = zonaEjecutivo.icodciudad;
                        cmdTransaccionFactory.Parameters.Add(p_zona);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_EJECUTIVO_ZONA";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        return zonaEjecutivo;

                    }
                    catch (DbException ex)
                    {
                        BOExcepcion.Throw("EjecutivoData", "guardarZonasEjecutivo", ex);
                        return null;
                    }
                }
            }

        }//end crear
        public Ejecutivo guardarBarriosEjecutivo (Ejecutivo barrioEjecutivo, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_asignacion = cmdTransaccionFactory.CreateParameter();
                        p_asignacion.Direction = ParameterDirection.Input;
                        p_asignacion.ParameterName = "P_ID_ASIGNACION";
                        p_asignacion.Value = 0;
                        cmdTransaccionFactory.Parameters.Add(p_asignacion);

                        DbParameter p_cod_ejecutivo = cmdTransaccionFactory.CreateParameter();
                        p_cod_ejecutivo.Direction = ParameterDirection.Input;
                        p_cod_ejecutivo.ParameterName = "P_COD_EJECTIVO";
                        p_cod_ejecutivo.Value = barrioEjecutivo.IdEjecutivo;
                        cmdTransaccionFactory.Parameters.Add(p_cod_ejecutivo);

                        DbParameter p_barrio = cmdTransaccionFactory.CreateParameter();
                        p_barrio.Direction = ParameterDirection.Input;
                        p_barrio.ParameterName = "P_COD_BARRIO";
                        p_barrio.Value = barrioEjecutivo.icodciudad;
                        cmdTransaccionFactory.Parameters.Add(p_barrio);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_EJECUTIVO_BARRIO";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        return barrioEjecutivo;

                    }
                    catch (DbException ex)
                    {
                        BOExcepcion.Throw("EjecutivoData", "guardarBarriosEjecutivo", ex);
                        return null;
                    }
                }
            }

        }//end crear
        public List<Ejecutivo> ListarZonasEjecutivo(Int64 idEjecutivo, Usuario pUsuario)
        {
            DbDataReader reader = default(DbDataReader);
            List<Ejecutivo> lstBarrios = new List<Ejecutivo>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {

                    try
                    {
                        string sql = "SELECT COD_ZONA FROM EJECUTIVO_ZONAS ORDER BY COD_ZONA";
                        if(idEjecutivo != 0)
                        {
                            sql = "SELECT COD_ZONA FROM EJECUTIVO_ZONAS where Cod_Ejecutivo = " + idEjecutivo + " ORDER BY COD_ZONA";
                        }                        

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        reader = cmdTransaccionFactory.ExecuteReader();

                        while (reader.Read())
                        {
                            Ejecutivo aseEntEjecutivo = new Ejecutivo();

                            if (reader["COD_ZONA"] != DBNull.Value) aseEntEjecutivo.icodciudad = Convert.ToInt64(reader["COD_ZONA"].ToString());
                            lstBarrios.Add(aseEntEjecutivo);
                        }

                        return lstBarrios;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AsesoresData", "ListarZonasEjecutivo", ex);
                        return null;
                    }
                }
            }
        }


        public List<Ejecutivo> ListarZonasDeEjecutivo(Int64 idEjecutivo, Usuario pUsuario)
        {
            DbDataReader reader = default(DbDataReader);
            List<Ejecutivo> lstBarrios = new List<Ejecutivo>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {

                    try
                    {
                        string sql = "SELECT * FROM Zonas ORDER BY COD_ZONA";
                        if (idEjecutivo != 0)
                        {
                            sql = "SELECT z.COD_ZONA, Z.DESCRIPCION FROM EJECUTIVO_ZONAS e INNER JOIN Zonas z ON E.Cod_Zona = z.Cod_Zona " +
                                    " WHERE e.Cod_Ejecutivo =" + idEjecutivo + " ORDER BY 2" ;
                        }

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        reader = cmdTransaccionFactory.ExecuteReader();

                        while (reader.Read())
                        {
                            Ejecutivo aseEntEjecutivo = new Ejecutivo();

                            if (reader["COD_ZONA"] != DBNull.Value) aseEntEjecutivo.icodciudad = Convert.ToInt64(reader["COD_ZONA"].ToString());
                            if (reader["DESCRIPCION"] != DBNull.Value) aseEntEjecutivo.nomciudad = Convert.ToString(reader["DESCRIPCION"].ToString());
                            lstBarrios.Add(aseEntEjecutivo);
                        }

                        return lstBarrios;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AsesoresData", "ListarZonasEjecutivo", ex);
                        return null;
                    }
                }
            }
        }


        public List<Ejecutivo> ListarBarriosEjecutivo(Int64 idEjecutivo, Usuario pUsuario)
        {
            DbDataReader reader = default(DbDataReader);
            List<Ejecutivo> lstBarrios = new List<Ejecutivo>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {

                    try
                    {
                        string sql = "SELECT COD_BARRIO FROM EJECUTIVO_BARRIOS ORDER BY COD_BARRIO";
                        if (idEjecutivo != 0)
                        {
                            sql = "SELECT COD_BARRIO FROM EJECUTIVO_BARRIOS where Cod_Ejectivo = " + idEjecutivo + " ORDER BY COD_BARRIO";
                        }

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        reader = cmdTransaccionFactory.ExecuteReader();

                        while (reader.Read())
                        {
                            Ejecutivo aseEntEjecutivo = new Ejecutivo();

                            if (reader["COD_BARRIO"] != DBNull.Value) aseEntEjecutivo.icodciudad = Convert.ToInt64(reader["COD_BARRIO"].ToString());
                            lstBarrios.Add(aseEntEjecutivo);
                        }

                        return lstBarrios;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AsesoresData", "ListarBarriosEjecutivo", ex);
                        return null;
                    }
                }
            }
        }

        public List<Ejecutivo> Listar(Ejecutivo pAseEntiEjecutivo, Usuario pUsuario)
        {
            DbDataReader reader = default(DbDataReader);
            List<Ejecutivo> lstPrograma = new List<Ejecutivo>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {

                    try
                    {
                        string sql = "SELECT * FROM VAsesoresEjecutivos " + Filtrar(pAseEntiEjecutivo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        reader = cmdTransaccionFactory.ExecuteReader();

                        while (reader.Read())
                        {
                            Ejecutivo aseEntEjecutivo = new Ejecutivo();

                            if (reader["ICODIGO"] != DBNull.Value)              aseEntEjecutivo.IdEjecutivo         = Convert.ToInt64(reader["ICODIGO"].ToString());
                            if (reader["ICODIGO"] != DBNull.Value)              aseEntEjecutivo.Codigo              = reader["ICODIGO"].ToString();
                            if (reader["IUSUARIO"] != DBNull.Value)             aseEntEjecutivo.IdUsuario           = Convert.ToInt64(reader["IUSUARIO"].ToString());
                            if (reader["SNOMBRE1"] != DBNull.Value)             aseEntEjecutivo.PrimerNombre        = reader["SNOMBRE1"].ToString();
                            if (reader["SNOMBRE2"] != DBNull.Value)             aseEntEjecutivo.SegundoNombre       = reader["SNOMBRE2"].ToString();
                            if (reader["SAPELLIDO1"] != DBNull.Value)           aseEntEjecutivo.PrimerApellido      = reader["SAPELLIDO1"].ToString();
                            if (reader["SAPELLIDO2"] != DBNull.Value)           aseEntEjecutivo.SegundoApellido     = reader["SAPELLIDO2"].ToString();
                            if (reader["ITIPOIDEN"] != DBNull.Value)            aseEntEjecutivo.TipoDocumento       = Convert.ToInt64(reader["ITIPOIDEN"].ToString());
                            if (reader["nombIdentificacion"] != DBNull.Value)   aseEntEjecutivo.NombreTipoDocumento = reader["nombIdentificacion"].ToString();
                            if (reader["SIDENTIFICACION"] != DBNull.Value)      aseEntEjecutivo.NumeroDocumento     = reader["SIDENTIFICACION"].ToString();
                            if (reader["SDIRECCION"] != DBNull.Value)           aseEntEjecutivo.Direccion           = reader["SDIRECCION"].ToString();
                            if (reader["SBARRIO"] != DBNull.Value)              aseEntEjecutivo.Barrio              = reader["SBARRIO"].ToString();
                            if (reader["STELEFONO"] != DBNull.Value)            aseEntEjecutivo.Telefono            = Convert.ToInt64(reader["STELEFONO"].ToString());
                            if (reader["SEMAIL"] != DBNull.Value)               aseEntEjecutivo.Email               = reader["SEMAIL"].ToString();
                            if (reader["FINGRESO"] != DBNull.Value)             aseEntEjecutivo.FechaIngreso        = Convert.ToDateTime(reader["FINGRESO"].ToString());
                            if (reader["IOFICINA"] != DBNull.Value)             aseEntEjecutivo.IdOficina           = Convert.ToInt64(reader["IOFICINA"].ToString());
                            if (reader["nombOficina"] != DBNull.Value)          aseEntEjecutivo.NombreOficina       = reader["nombOficina"].ToString();
                            if (reader["nombEstado"] != DBNull.Value)           aseEntEjecutivo.NombreEstado        = reader["nombEstado"].ToString();

                            lstPrograma.Add(aseEntEjecutivo);
                        }

                        return lstPrograma;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AsesoresData", "ListarEjecutivo", ex);
                        return null;
                    }
                }
            }
        }


        public List<Ejecutivo> Listar(Usuario pUsuario)
        {
            DbDataReader reader = default(DbDataReader);
            List<Ejecutivo> lstPrograma = new List<Ejecutivo>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {

                    try
                    {
                        string sql = "select icodigo, snombre1||' '||Sapellido1||' '||Sapellido2 as NOMBRE from Asejecutivos where Iestado = 1";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        reader = cmdTransaccionFactory.ExecuteReader();

                        while (reader.Read())
                        {
                            Ejecutivo aseEntEjecutivo = new Ejecutivo();

                            if (reader["ICODIGO"] != DBNull.Value) aseEntEjecutivo.Codigo = reader["ICODIGO"].ToString();
                            if (reader["NOMBRE"] != DBNull.Value) aseEntEjecutivo.PrimerNombre = reader["NOMBRE"].ToString();                            
                            lstPrograma.Add(aseEntEjecutivo);
                        }

                        return lstPrograma;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AsesoresData", "ListarEjecutivo", ex);
                        return null;
                    }
                }
            }
        }        

        public Ejecutivo Actualizar(Ejecutivo pAseEntiEjecutivo, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pPrimerNombre = cmdTransaccionFactory.CreateParameter();
                        pPrimerNombre.Direction = ParameterDirection.Input;
                        pPrimerNombre.ParameterName = "P_SNOMBRE1";
                        pPrimerNombre.Value = pAseEntiEjecutivo.PrimerNombre;

                        DbParameter pSegundoNombre = cmdTransaccionFactory.CreateParameter();
                        pSegundoNombre.Direction = ParameterDirection.Input;
                        pSegundoNombre.ParameterName = "P_SNOMBRE2";
                        pSegundoNombre.Value = pAseEntiEjecutivo.SegundoNombre;

                        DbParameter pPrimerApellido = cmdTransaccionFactory.CreateParameter();
                        pPrimerApellido.Direction = ParameterDirection.Input;
                        pPrimerApellido.ParameterName = "P_SAPELLIDO1";
                        pPrimerApellido.Value = pAseEntiEjecutivo.PrimerApellido;

                        DbParameter pSegundoApellido = cmdTransaccionFactory.CreateParameter();
                        pSegundoApellido.Direction = ParameterDirection.Input;
                        pSegundoApellido.ParameterName = "P_SAPELLIDO2";
                        pSegundoApellido.Value = pAseEntiEjecutivo.SegundoApellido;

                        DbParameter pTipoDocumento = cmdTransaccionFactory.CreateParameter();
                        pTipoDocumento.Direction = ParameterDirection.Input;
                        pTipoDocumento.ParameterName = "P_ITIPOIDEN";
                        pTipoDocumento.DbType = DbType.String;
                        pTipoDocumento.Value = pAseEntiEjecutivo.TipoDocumento.ToString();

                        DbParameter pNumeroDocumento = cmdTransaccionFactory.CreateParameter();
                        pNumeroDocumento.Direction = ParameterDirection.Input;
                        pNumeroDocumento.ParameterName = "P_SIDENTIFICACION";
                        pNumeroDocumento.DbType = DbType.String;
                        pNumeroDocumento.Value = pAseEntiEjecutivo.NumeroDocumento.ToString();

                        DbParameter pDireccion = cmdTransaccionFactory.CreateParameter();
                        pDireccion.Direction = ParameterDirection.Input;
                        pDireccion.ParameterName = "P_SDIRECCION";
                        pDireccion.Value = pAseEntiEjecutivo.Direccion;

                        DbParameter pBarrio = cmdTransaccionFactory.CreateParameter();
                        pBarrio.Direction = ParameterDirection.Input;
                        pBarrio.ParameterName = "P_SBARRIO";
                        pBarrio.Value = pAseEntiEjecutivo.Barrio;

                        DbParameter pTelefono = cmdTransaccionFactory.CreateParameter();
                        pTelefono.Direction = ParameterDirection.Input;
                        pTelefono.DbType = DbType.String;
                        pTelefono.ParameterName = "P_STELEFONO";
                        pTelefono.Value = pAseEntiEjecutivo.Telefono.ToString();

                        DbParameter pTelefonoCel = cmdTransaccionFactory.CreateParameter();
                        pTelefonoCel.Direction = ParameterDirection.Input;
                        pTelefonoCel.DbType = DbType.String;
                        pTelefonoCel.ParameterName = "P_STELEFONOCEL";
                        pTelefonoCel.Value = pAseEntiEjecutivo.TelefonoCel.ToString();

                        DbParameter pEmail = cmdTransaccionFactory.CreateParameter();
                        pEmail.Direction = ParameterDirection.Input;
                        pEmail.DbType = DbType.String;
                        pEmail.ParameterName = "P_SMAIL";
                        pEmail.Value = pAseEntiEjecutivo.Email;

                        DbParameter pFechaIngreso = cmdTransaccionFactory.CreateParameter();
                        pFechaIngreso.ParameterName = "P_FREGISTRO";
                        pFechaIngreso.Direction = ParameterDirection.Input;
                        pFechaIngreso.Value = pAseEntiEjecutivo.FechaIngreso;

                        DbParameter pidOficina = cmdTransaccionFactory.CreateParameter();
                        pidOficina.Direction = ParameterDirection.Input;
                        pidOficina.ParameterName = "P_IOFICINA";
                        pidOficina.DbType = DbType.String;
                        pidOficina.Value = pAseEntiEjecutivo.IdOficina.ToString();

                        DbParameter pidEstado = cmdTransaccionFactory.CreateParameter();
                        pidEstado.Direction = ParameterDirection.Input;
                        pidEstado.ParameterName = "P_IESTADO";
                        pidEstado.DbType = DbType.String;
                        pidEstado.Value = pAseEntiEjecutivo.IdEstado.ToString();

                        DbParameter pidZona = cmdTransaccionFactory.CreateParameter();
                        pidZona.Direction = ParameterDirection.Input;
                        pidZona.ParameterName = "P_ICODZONA";
                        pidZona.DbType = DbType.String;
                        pidZona.Value = pAseEntiEjecutivo.IdZona.ToString();

                        DbParameter pIdEjecutivo = cmdTransaccionFactory.CreateParameter();
                        pIdEjecutivo.ParameterName = "P_IDCODIGO";
                        pIdEjecutivo.DbType = DbType.String;
                        pIdEjecutivo.Size = 50;
                        pIdEjecutivo.Value = pAseEntiEjecutivo.IdEjecutivo.ToString();

                        cmdTransaccionFactory.Parameters.Add(pPrimerNombre);
                        cmdTransaccionFactory.Parameters.Add(pSegundoNombre);
                        cmdTransaccionFactory.Parameters.Add(pPrimerApellido);
                        cmdTransaccionFactory.Parameters.Add(pSegundoApellido);
                        cmdTransaccionFactory.Parameters.Add(pTipoDocumento);
                        cmdTransaccionFactory.Parameters.Add(pNumeroDocumento);
                        cmdTransaccionFactory.Parameters.Add(pDireccion);
                        cmdTransaccionFactory.Parameters.Add(pBarrio);
                        cmdTransaccionFactory.Parameters.Add(pTelefono);
                        cmdTransaccionFactory.Parameters.Add(pTelefonoCel);
                        cmdTransaccionFactory.Parameters.Add(pEmail);
                        cmdTransaccionFactory.Parameters.Add(pFechaIngreso);
                        cmdTransaccionFactory.Parameters.Add(pidOficina);
                        cmdTransaccionFactory.Parameters.Add(pidEstado);
                        cmdTransaccionFactory.Parameters.Add(pidZona);
                        cmdTransaccionFactory.Parameters.Add(pIdEjecutivo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_AS_EJECUTIVO_ACTUALIZAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pAseEntiEjecutivo, "ASEJECUTIVOS", pUsuario, Accion.Crear.ToString());

                        pAseEntiEjecutivo.IdEjecutivo = Convert.ToInt64(pIdEjecutivo.Value);

                        return pAseEntiEjecutivo;

                    }
                    catch (DbException ex)
                    {
                        BOExcepcion.Throw("EjecutivoData", "Actualizar", ex);
                        return null;
                    }
                }
            }

        }//end crear

        public string Filtrar(Ejecutivo pEntidad)
        {

            string iniSql = " WHERE ";
            string str = "";
            bool flag = false;
            PropertyInfo[] propertyInfos;

            if (pEntidad != null)
            {
                propertyInfos = pEntidad.GetType().GetProperties();


                foreach (PropertyInfo property in propertyInfos)
                {
                    if (property.Name.Equals("PrimerNombre") && property.GetValue(pEntidad, null) != null)
                    {
                        if (flag) str += " AND ";
                        str += " SNOMBRE1 like '%" + property.GetValue(pEntidad, null) + "%' ";
                        flag = true;
                    }

                    if (property.Name.Equals("PrimerApellido") && property.GetValue(pEntidad, null) != null)
                    {
                        if (flag) str += " AND ";
                        str += " SAPELLIDO1 like '%" + property.GetValue(pEntidad, null) + "%' ";
                        flag = true;
                    }

                    if (property.Name.Equals("NumeroDocumento") && property.GetValue(pEntidad, null) != null && property.GetValue(pEntidad, null).ToString().Trim() != "0")
                    {
                        if (flag) str += " AND ";
                        str += " SIDENTIFICACION = '" + property.GetValue(pEntidad, null) + "' ";
                        flag = true;
                    }

                    if (property.Name.Equals("IdOficina") && property.GetValue(pEntidad, null) != null && property.GetValue(pEntidad,null).ToString().Trim() != "0")
                    {
                        if (flag) str += " AND ";
                        str += " IOFICINA = " + property.GetValue(pEntidad, null);
                        flag = true;
                    }

                    if (property.Name.Equals("IdEstado") && property.GetValue(pEntidad, null) != null && property.GetValue(pEntidad, null).ToString().Trim() != "0")
                    {
                        if (flag) str += " AND ";
                        str += " IESTADO = " + property.GetValue(pEntidad, null);
                        flag = true;
                    }

                }
            }
            if (str != "")
                return iniSql + str;
            else
                return "";
        }

        public List<Ejecutivo> ListarAsesores(Ejecutivo ejecutivo, Usuario pUsuario)
        {
            DbDataReader reader = default(DbDataReader);
            List<Ejecutivo> lstPrograma = new List<Ejecutivo>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {

                    try
                    {
                        string sql = "SELECT * FROM v_Asejecutivos_ACTIVOS" + ObtenerFiltro(ejecutivo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        reader = cmdTransaccionFactory.ExecuteReader();

                        while (reader.Read())
                        {
                            Ejecutivo aseEntEjecutivo = new Ejecutivo();

                            if (reader["ICODIGO"] != DBNull.Value) aseEntEjecutivo.IdEjecutivo = Convert.ToInt64(reader["ICODIGO"].ToString());
                            if (reader["NOMBRECOMPLETO"] != DBNull.Value) aseEntEjecutivo.NombreCompleto = reader["NOMBRECOMPLETO"].ToString();
                            if (reader["ICODCIUDAD"] != DBNull.Value) aseEntEjecutivo.icodciudad = Convert.ToInt64(reader["ICODCIUDAD"].ToString());
                            if (reader["NOMCIUDAD"] != DBNull.Value) aseEntEjecutivo.nomciudad = reader["NOMCIUDAD"].ToString();
                            if (reader["SDIRECCION"] != DBNull.Value) aseEntEjecutivo.Direccion = reader["SDIRECCION"].ToString();
                            if (reader["STELEFONO"] != DBNull.Value) aseEntEjecutivo.Telefono = Convert.ToInt64(reader["STELEFONO"].ToString());
                            if (reader["SEMAIL"] != DBNull.Value) aseEntEjecutivo.Email = reader["SEMAIL"].ToString();
                            if (reader["LATITUD"] != DBNull.Value) aseEntEjecutivo.latitud = reader["LATITUD"].ToString();
                            if (reader["LONGITUD"] != DBNull.Value) aseEntEjecutivo.longitud = reader["LONGITUD"].ToString();
                            lstPrograma.Add(aseEntEjecutivo);
                        }

                        return lstPrograma;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EjecutivoData", "ListarEjecutivo", ex);
                        return null;
                    }
                }
            }
        }
        public List<Ejecutivo> ListartodosAsesores(Usuario pUsuario)
        {
            DbDataReader reader = default(DbDataReader);
            List<Ejecutivo> lstPrograma = new List<Ejecutivo>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {

                    try
                    {
                        string sql = "SELECT * FROM v_Asejecutivos_ACTIVOS";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        reader = cmdTransaccionFactory.ExecuteReader();

                        while (reader.Read())
                        {
                            Ejecutivo aseEntEjecutivo = new Ejecutivo();

                            if (reader["ICODIGO"] != DBNull.Value) aseEntEjecutivo.IdEjecutivo = Convert.ToInt64(reader["ICODIGO"].ToString());
                            if (reader["NOMBRECOMPLETO"] != DBNull.Value) aseEntEjecutivo.NombreCompleto = reader["NOMBRECOMPLETO"].ToString();
                            if (reader["ICODCIUDAD"] != DBNull.Value) aseEntEjecutivo.icodciudad = Convert.ToInt64(reader["ICODCIUDAD"].ToString());
                            if (reader["NOMCIUDAD"] != DBNull.Value) aseEntEjecutivo.nomciudad = reader["NOMCIUDAD"].ToString();
                            if (reader["SDIRECCION"] != DBNull.Value) aseEntEjecutivo.Direccion = reader["SDIRECCION"].ToString();
                            if (reader["STELEFONO"] != DBNull.Value) aseEntEjecutivo.Telefono = Convert.ToInt64(reader["STELEFONO"].ToString());

                            lstPrograma.Add(aseEntEjecutivo);
                        }

                        return lstPrograma;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EjecutivoData", "ListarEjecutivo", ex);
                        return null;
                    }
                }
            }
        }

        public List<Ejecutivo> ListartodosAsesoresRuta(Usuario pUsuario)
        {
            DbDataReader reader = default(DbDataReader);
            List<Ejecutivo> lstPrograma = new List<Ejecutivo>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {

                    try
                    {
                        string sql = "SELECT * FROM v_Asejecutivos";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        reader = cmdTransaccionFactory.ExecuteReader();

                        while (reader.Read())
                        {
                            Ejecutivo aseEntEjecutivo = new Ejecutivo();

                            if (reader["ICODIGO"] != DBNull.Value) aseEntEjecutivo.IdEjecutivo = Convert.ToInt64(reader["ICODIGO"].ToString());
                            if (reader["NOMBRECOMPLETO"] != DBNull.Value) aseEntEjecutivo.NombreCompleto = reader["NOMBRECOMPLETO"].ToString();
                            if (reader["ICODCIUDAD"] != DBNull.Value) aseEntEjecutivo.icodciudad = Convert.ToInt64(reader["ICODCIUDAD"].ToString());
                            if (reader["NOMCIUDAD"] != DBNull.Value) aseEntEjecutivo.nomciudad = reader["NOMCIUDAD"].ToString();
                            if (reader["SDIRECCION"] != DBNull.Value) aseEntEjecutivo.Direccion = reader["SDIRECCION"].ToString();
                            if (reader["STELEFONO"] != DBNull.Value) aseEntEjecutivo.Telefono = Convert.ToInt64(reader["STELEFONO"].ToString());

                            lstPrograma.Add(aseEntEjecutivo);
                        }

                        return lstPrograma;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EjecutivoData", "ListarEjecutivo", ex);
                        return null;
                    }
                }
            }
        }


        public List<Ejecutivo> ListarAsesoresgeoreferencia(Ejecutivo ejecutivo, Usuario pUsuario,string filtro)
        {
            DbDataReader reader = default(DbDataReader);
            List<Ejecutivo> lstPrograma = new List<Ejecutivo>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {

                    try
                    {
                        string sql = "SELECT * FROM v_ubicaciongeograficaejecutivo " + ObtenerFiltro(ejecutivo);
                        if (filtro != "")
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " and "+filtro;
                            else
                                sql += " where " + filtro;
                        }

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        reader = cmdTransaccionFactory.ExecuteReader();

                        while (reader.Read())
                        {
                            Ejecutivo aseEntEjecutivo = new Ejecutivo();

                            if (reader["ICODIGO"] != DBNull.Value) aseEntEjecutivo.IdEjecutivo = Convert.ToInt64(reader["ICODIGO"].ToString());
                            if (reader["NOMBRECOMPLETO"] != DBNull.Value) aseEntEjecutivo.NombreCompleto = reader["NOMBRECOMPLETO"].ToString();
                            if (reader["ICODCIUDAD"] != DBNull.Value) aseEntEjecutivo.icodciudad = Convert.ToInt64(reader["ICODCIUDAD"].ToString());
                            if (reader["NOMCIUDAD"] != DBNull.Value) aseEntEjecutivo.nomciudad = reader["NOMCIUDAD"].ToString();
                            if (reader["SDIRECCION"] != DBNull.Value) aseEntEjecutivo.Direccion = reader["SDIRECCION"].ToString();
                            if (reader["STELEFONO"] != DBNull.Value) aseEntEjecutivo.Telefono = Convert.ToInt64(reader["STELEFONO"].ToString());
                            if (reader["latitud"] != DBNull.Value) aseEntEjecutivo.latitud = reader["latitud"].ToString();
                            if (reader["longitud"] != DBNull.Value) aseEntEjecutivo.longitud = reader["longitud"].ToString();
                            lstPrograma.Add(aseEntEjecutivo);
                        }

                        return lstPrograma;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EjecutivoData", "ListarEjecutivo", ex);
                        return null;
                    }
                }
            }
        }


        public List<Ejecutivo> ListartodosUsuarios(Usuario pUsuario)
        {
            DbDataReader reader = default(DbDataReader);
            List<Ejecutivo> lstPrograma = new List<Ejecutivo>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {

                    try
                    {
                        string sql = "SELECT * FROM USUARIOS WHERE ESTADO=1 order by NOMBRE asc ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        reader = cmdTransaccionFactory.ExecuteReader();

                        while (reader.Read())
                        {
                            Ejecutivo aseEntEjecutivo = new Ejecutivo();

                            if (reader["CODUSUARIO"] != DBNull.Value) aseEntEjecutivo.IdEjecutivo = Convert.ToInt64(reader["CODUSUARIO"].ToString());
                            if (reader["NOMBRE"] != DBNull.Value) aseEntEjecutivo.NombreCompleto = reader["NOMBRE"].ToString();
                         
                            lstPrograma.Add(aseEntEjecutivo);
                        }

                        return lstPrograma;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EjecutivoData", "ListartodosUsuarios", ex);
                        return null;
                    }
                }
            }
        }
    }
}