using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Nomina.Entities;
 
namespace Xpinn.Nomina.Data
{
    public class ContratacionNominaData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public ContratacionNominaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
        public TipoContrato CrearContratacion(TipoContrato pContratacion, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_contratacion = cmdTransaccionFactory.CreateParameter();
                        pcod_contratacion.ParameterName = "p_cod_contratacion";
                        pcod_contratacion.Value = pContratacion.cod_contratacion;
                        pcod_contratacion.Direction = ParameterDirection.Output;
                        pcod_contratacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_contratacion);

                        DbParameter pcod_contrato = cmdTransaccionFactory.CreateParameter();
                        pcod_contrato.ParameterName = "p_cod_contrato";
                        if (pContratacion.cod_contrato == null)
                            pcod_contrato.Value = DBNull.Value;
                        else
                            pcod_contrato.Value = pContratacion.cod_contrato;
                        pcod_contrato.Direction = ParameterDirection.Input;
                        pcod_contrato.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_contrato);

                        DbParameter ptipo_contrato = cmdTransaccionFactory.CreateParameter();
                        ptipo_contrato.ParameterName = "p_tipo_contrato";
                        if (pContratacion.tipo_contrato == null)
                            ptipo_contrato.Value = DBNull.Value;
                        else
                            ptipo_contrato.Value = pContratacion.tipo_contrato;
                        ptipo_contrato.Direction = ParameterDirection.Input;
                        ptipo_contrato.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipo_contrato);

                        DbParameter pdia_habil = cmdTransaccionFactory.CreateParameter();
                        pdia_habil.ParameterName = "p_dia_habil";
                        if (pContratacion.dia_habil == null)
                            pdia_habil.Value = DBNull.Value;
                        else
                            pdia_habil.Value = pContratacion.dia_habil;
                        pdia_habil.Direction = ParameterDirection.Input;
                        pdia_habil.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdia_habil);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_CONTRATACI_CREAR";

                        cmdTransaccionFactory.ExecuteNonQuery();
                        if (pcod_contratacion.Value != null)
                            pContratacion.cod_contratacion = Convert.ToInt64(pcod_contratacion.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pContratacion;
                    }

                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ContratacionData", "CrearContratacion", ex);
                        return null;
                    }
                }
            }
        }
        public TipoContrato CrearTipoRetirocontrato(TipoContrato pContratacion, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                    
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pContratacion.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Output;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);                      

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        if (pContratacion.descripciontiporetiro == null)
                            pdescripcion.Value = DBNull.Value;
                        else
                            pdescripcion.Value = pContratacion.descripciontiporetiro;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_TIPORETCON_CREAR";

                        cmdTransaccionFactory.ExecuteNonQuery();
                        if (pconsecutivo.Value != null)
                            pContratacion.consecutivo = Convert.ToInt64(pconsecutivo.Value);

                        dbConnectionFactory.CerrarConexion(connection);
                        return pContratacion;
                    }

                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ContratacionData", "CrearTipoRetirocontrato", ex);
                        return null;
                    }
                }
            }
        }

        public TipoContrato ModificarTipoRetirocontrato(TipoContrato pContratacion, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {


                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pContratacion.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        if (pContratacion.descripciontiporetiro == null)
                            pdescripcion.Value = DBNull.Value;
                        else
                            pdescripcion.Value = pContratacion.descripciontiporetiro;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_TIPORETCON_MOD";

                     
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pContratacion;
                    }

                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ContratacionData", "ModificarTipoRetirocontrato", ex);
                        return null;
                    }
                }
            }
        }


        public List<TipoContrato> ListarTipoContratos(Usuario usuario)
        {
            DbDataReader resultado;
            List<TipoContrato> listaEntidades = new List<TipoContrato>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM TipoContrato ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TipoContrato entidad = new TipoContrato();

                            if (resultado["CODTIPOCONTRATO"] != DBNull.Value) entidad.cod_tipo_contrato = Convert.ToInt64(resultado["CODTIPOCONTRATO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["COD_GARANTIA"] != DBNull.Value) entidad.cod_garantia = Convert.ToInt64(resultado["COD_GARANTIA"]);

                            listaEntidades.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return listaEntidades;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ContratacionData", "ListarTipoContratos", ex);
                        return null;
                    }
                }
            }
        }

        public TipoContrato ModificarContratacion(TipoContrato pContratacion, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_contratacion = cmdTransaccionFactory.CreateParameter();
                        pcod_contratacion.ParameterName = "p_cod_contratacion";
                        pcod_contratacion.Value = pContratacion.cod_contratacion;
                        pcod_contratacion.Direction = ParameterDirection.Input;
                        pcod_contratacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_contratacion);

                        DbParameter pcod_contrato = cmdTransaccionFactory.CreateParameter();
                        pcod_contrato.ParameterName = "p_cod_contrato";
                        if (pContratacion.cod_contrato == null)
                            pcod_contrato.Value = DBNull.Value;
                        else
                            pcod_contrato.Value = pContratacion.cod_contrato;
                        pcod_contrato.Direction = ParameterDirection.Input;
                        pcod_contrato.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_contrato);

                        DbParameter ptipo_contrato = cmdTransaccionFactory.CreateParameter();
                        ptipo_contrato.ParameterName = "p_tipo_contrato";
                        if (pContratacion.tipo_contrato == null)
                            ptipo_contrato.Value = DBNull.Value;
                        else
                            ptipo_contrato.Value = pContratacion.tipo_contrato;
                        ptipo_contrato.Direction = ParameterDirection.Input;
                        ptipo_contrato.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipo_contrato);

                        DbParameter pdia_habil = cmdTransaccionFactory.CreateParameter();
                        pdia_habil.ParameterName = "p_dia_habil";
                        if (pContratacion.dia_habil == null)
                            pdia_habil.Value = DBNull.Value;
                        else
                            pdia_habil.Value = pContratacion.dia_habil;
                        pdia_habil.Direction = ParameterDirection.Input;
                        pdia_habil.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdia_habil);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_CONTRATACI_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pContratacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ContratacionData", "ModificarContratacion", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarContratacion(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        TipoContrato pContratacion = new TipoContrato();
                        pContratacion = ConsultarContratacion(pId, vUsuario);

                        DbParameter pcod_contratacion = cmdTransaccionFactory.CreateParameter();
                        pcod_contratacion.ParameterName = "p_cod_contratacion";
                        pcod_contratacion.Value = pContratacion.cod_contratacion;
                        pcod_contratacion.Direction = ParameterDirection.Input;
                        pcod_contratacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_contratacion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_CONTRATACI_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ContratacionData", "EliminarContratacion", ex);
                    }
                }
            }
        }


        public TipoContrato ConsultarContratacion(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            TipoContrato entidad = new TipoContrato();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM CONTRATACION WHERE COD_CONTRATACION = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COD_CONTRATACION"] != DBNull.Value) entidad.cod_contratacion = Convert.ToInt64(resultado["COD_CONTRATACION"]);
                            if (resultado["COD_CONTRATO"] != DBNull.Value) entidad.cod_contrato = Convert.ToInt64(resultado["COD_CONTRATO"]);
                            if (resultado["TIPO_CONTRATO"] != DBNull.Value) entidad.tipo_contrato = Convert.ToString(resultado["TIPO_CONTRATO"]);
                            if (resultado["DIA_HABIL"] != DBNull.Value) entidad.dia_habil = Convert.ToString(resultado["DIA_HABIL"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ContratacionData", "ConsultarContratacion", ex);
                        return null;
                    }
                }
            }
        }

        public TipoContrato ConsultarTipoRetiroContrato(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            TipoContrato entidad = new TipoContrato();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM TIPORETIROCONTRATO WHERE CONSECUTIVO = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripciontiporetiro = Convert.ToString(resultado["DESCRIPCION"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ContratacionData", "ConsultarTipoRetiroContrato", ex);
                        return null;
                    }
                }
            }
        }

        public List<TipoContrato> ListarContratacion(string pid, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<TipoContrato> lstContratacion = new List<TipoContrato>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM CONTRATACION " + pid.ToString() + " ORDER BY COD_CONTRATACION ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            TipoContrato entidad = new TipoContrato();
                            if (resultado["COD_CONTRATACION"] != DBNull.Value) entidad.cod_contratacion = Convert.ToInt64(resultado["COD_CONTRATACION"]);
                            if (resultado["COD_CONTRATO"] != DBNull.Value) entidad.cod_contrato = Convert.ToInt64(resultado["COD_CONTRATO"]);
                            if (resultado["TIPO_CONTRATO"] != DBNull.Value) entidad.tipo_contrato = Convert.ToString(resultado["TIPO_CONTRATO"]);
                            if (resultado["DIA_HABIL"] != DBNull.Value) entidad.dia_habil = Convert.ToString(resultado["DIA_HABIL"]);
                            lstContratacion.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstContratacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ContratacionData", "ListarContratacion", ex);
                        return null;
                    }
                }
            }
        }
        public List<TipoContrato> ListarTipoRetiroContrato(string pid, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<TipoContrato> lstContratacion = new List<TipoContrato>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM TIPORETIROCONTRATO " + pid.ToString() + " ORDER BY CONSECUTIVO ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            TipoContrato entidad = new TipoContrato();
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripciontiporetiro = Convert.ToString(resultado["DESCRIPCION"]);
                               lstContratacion.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstContratacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ContratacionData", "ListarTipoRetiroContrato", ex);
                        return null;
                    }
                }
            }
        }


    }
}