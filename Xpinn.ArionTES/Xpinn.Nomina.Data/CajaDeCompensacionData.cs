
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Nomina.Entities;
 
namespace Xpinn.Nomina.Data
{
    public class CajaDeCompensacionaData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public CajaDeCompensacionaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
        public CajaDeCompensacion CrearCajaDeCompensacion(CajaDeCompensacion pCajaDeCompensacion, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pCajaDeCompensacion.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Output;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pnit = cmdTransaccionFactory.CreateParameter();
                        pnit.ParameterName = "p_nit";
                        pnit.Value = pCajaDeCompensacion.nit;
                        pnit.Direction = ParameterDirection.Input;
                        pnit.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnit);

                        DbParameter pdireccion = cmdTransaccionFactory.CreateParameter();
                        pdireccion.ParameterName = "p_direccion";
                        if (pCajaDeCompensacion.direccion == null)
                            pdireccion.Value = DBNull.Value;
                        else
                            pdireccion.Value = pCajaDeCompensacion.direccion;
                        pdireccion.Direction = ParameterDirection.Input;
                        pdireccion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdireccion);

                        DbParameter ptelefono = cmdTransaccionFactory.CreateParameter();
                        ptelefono.ParameterName = "p_telefono";
                        if (pCajaDeCompensacion.telefono == null)
                            ptelefono.Value = DBNull.Value;
                        else
                            ptelefono.Value = pCajaDeCompensacion.telefono;
                        ptelefono.Direction = ParameterDirection.Input;
                        ptelefono.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptelefono);

                        DbParameter pcodigopila = cmdTransaccionFactory.CreateParameter();
                        pcodigopila.ParameterName = "p_codigopila";
                        if (pCajaDeCompensacion.codigopila == null)
                            pcodigopila.Value = DBNull.Value;
                        else
                            pcodigopila.Value = pCajaDeCompensacion.codigopila;
                        pcodigopila.Direction = ParameterDirection.Input;
                        pcodigopila.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcodigopila);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "p_nombre";
                        if (pCajaDeCompensacion.nombre == null)
                            pnombre.Value = DBNull.Value;
                        else
                            pnombre.Value = pCajaDeCompensacion.nombre;
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre);


                        DbParameter pcod_cuenta = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta.ParameterName = "p_cod_cuenta";
                        if (pCajaDeCompensacion.cod_cuenta == null)
                            pcod_cuenta.Value = DBNull.Value;
                        else
                            pcod_cuenta.Value = pCajaDeCompensacion.cod_cuenta;
                        pcod_cuenta.Direction = ParameterDirection.Input;
                        pcod_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta);


                        DbParameter pcod_cuenta_contra = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta_contra.ParameterName = "p_cod_cuenta_contra";
                        if (pCajaDeCompensacion.cod_cuenta_contra == null)
                            pcod_cuenta_contra.Value = DBNull.Value;
                        else
                            pcod_cuenta_contra.Value = pCajaDeCompensacion.cod_cuenta_contra;
                        pcod_cuenta_contra.Direction = ParameterDirection.Input;
                        pcod_cuenta_contra.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta_contra);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_CAJACO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        if (pconsecutivo.Value != null)
                            pCajaDeCompensacion.consecutivo = Convert.ToInt64(pconsecutivo.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pCajaDeCompensacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CajaDeCompensacionData", "CrearCajaDeCompensacion", ex);
                        return null;
                    }
                }
            }
        }


        public CajaDeCompensacion ModificarCajaDeCompensacion(CajaDeCompensacion pCajaDeCompensacion, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pCajaDeCompensacion.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pnit = cmdTransaccionFactory.CreateParameter();
                        pnit.ParameterName = "p_nit";
                        pnit.Value = pCajaDeCompensacion.nit;
                        pnit.Direction = ParameterDirection.Input;
                        pnit.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnit);

                        DbParameter pdireccion = cmdTransaccionFactory.CreateParameter();
                        pdireccion.ParameterName = "p_direccion";
                        if (pCajaDeCompensacion.direccion == null)
                            pdireccion.Value = DBNull.Value;
                        else
                            pdireccion.Value = pCajaDeCompensacion.direccion;
                        pdireccion.Direction = ParameterDirection.Input;
                        pdireccion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdireccion);

                        DbParameter ptelefono = cmdTransaccionFactory.CreateParameter();
                        ptelefono.ParameterName = "p_telefono";
                        if (pCajaDeCompensacion.telefono == null)
                            ptelefono.Value = DBNull.Value;
                        else
                            ptelefono.Value = pCajaDeCompensacion.telefono;
                        ptelefono.Direction = ParameterDirection.Input;
                        ptelefono.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptelefono);

                        DbParameter pcodigopila = cmdTransaccionFactory.CreateParameter();
                        pcodigopila.ParameterName = "p_codigopila";
                        if (pCajaDeCompensacion.codigopila == null)
                            pcodigopila.Value = DBNull.Value;
                        else
                            pcodigopila.Value = pCajaDeCompensacion.codigopila;
                        pcodigopila.Direction = ParameterDirection.Input;
                        pcodigopila.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcodigopila);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "p_nombre";
                        if (pCajaDeCompensacion.nombre == null)
                            pnombre.Value = DBNull.Value;
                        else
                            pnombre.Value = pCajaDeCompensacion.nombre;
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        
                        DbParameter pcod_cuenta = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta.ParameterName = "p_cod_cuenta";
                        if (pCajaDeCompensacion.cod_cuenta == null)
                            pcod_cuenta.Value = DBNull.Value;
                        else
                            pcod_cuenta.Value = pCajaDeCompensacion.cod_cuenta;
                        pcod_cuenta.Direction = ParameterDirection.Input;
                        pcod_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta);

                        DbParameter pcod_cuenta_contra = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta_contra.ParameterName = "p_cod_cuenta_contra";
                        if (pCajaDeCompensacion.cod_cuenta_contra == null)
                            pcod_cuenta_contra.Value = DBNull.Value;
                        else
                            pcod_cuenta_contra.Value = pCajaDeCompensacion.cod_cuenta_contra;
                        pcod_cuenta_contra.Direction = ParameterDirection.Input;
                        pcod_cuenta_contra.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta_contra);



                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_CAJAC_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        if (pconsecutivo.Value != null)
                            pCajaDeCompensacion.consecutivo = Convert.ToInt64(pconsecutivo.Value);
                        dbConnectionFactory.CerrarConexion(connection);

                        return pCajaDeCompensacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CajaDeCompensacionData", "ModificarCajaDeCompensacion", ex);
                        return null;
                    }
                }
            }
        }

        public void EliminarCajaDeCompensacion(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        CajaDeCompensacion pCajaDeCompensacion = new CajaDeCompensacion();
                        pCajaDeCompensacion = ConsultarCajaDeCompensacion(pId, vUsuario);

                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pCajaDeCompensacion.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_CAJACOMPEN_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CajaDeCompensacionData", "EliminarCajaDeCompensacion", ex);
                    }
                }
            }
        }

        public CajaDeCompensacion ConsultarDatosCajaDeCompensacion(string pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            CajaDeCompensacion entidad = new CajaDeCompensacion();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM CAJACOMPENSACION WHERE CONSECUTIVO = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["NIT"] != DBNull.Value) entidad.nit = Convert.ToString(resultado["NIT"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["CODIGOPILA"] != DBNull.Value) entidad.codigopila = Convert.ToString(resultado["CODIGOPILA"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["COD_CUENTA_CONTRA"] != DBNull.Value) entidad.cod_cuenta_contra = Convert.ToString(resultado["COD_CUENTA_CONTRA"]);


                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CajaDeCompensacionData", "ConsultarCajaDeCompensacion", ex);
                        return null;
                    }
                }
            }
        }


        public CajaDeCompensacion ConsultarCajaDeCompensacion(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            CajaDeCompensacion entidad = new CajaDeCompensacion();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM CAJACOMPENSACION WHERE CONSECUTIVO = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["NIT"] != DBNull.Value) entidad.nit = Convert.ToString(resultado["NIT"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["CODIGOPILA"] != DBNull.Value) entidad.codigopila = Convert.ToString(resultado["CODIGOPILA"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["COD_CUENTA_CONTRA"] != DBNull.Value) entidad.cod_cuenta_contra = Convert.ToString(resultado["COD_CUENTA_CONTRA"]);

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
                        BOExcepcion.Throw("CajaDeCompensacionData", "ConsultarCajaDeCompensacion", ex);
                        return null;
                    }
                }
            }
        }


        public List<CajaDeCompensacion> ListarCajaDeCompensacion(string filtro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<CajaDeCompensacion> lstCajaDeCompensacion = new List<CajaDeCompensacion>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select CAJACOMPENSACION.CONSECUTIVO,
                        CAJACOMPENSACION.NIT,
                        CAJACOMPENSACION.DIRECCION,
                        CAJACOMPENSACION.TELEFONO,
                        CAJACOMPENSACION.CODIGOPILA,   
                        CAJACOMPENSACION.COD_CUENTA,  
                        CAJACOMPENSACION.COD_CUENTA_contra, 
                        CAJACOMPENSACION.NOMBRE from CAJACOMPENSACION" + filtro.ToString();
  
                        connection.Open();

                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            CajaDeCompensacion entidad = new CajaDeCompensacion();

                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["NIT"] != DBNull.Value) entidad.nit = Convert.ToString(resultado["NIT"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["CODIGOPILA"] != DBNull.Value) entidad.codigopila = Convert.ToString(resultado["CODIGOPILA"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["COD_CUENTA_CONTRA"] != DBNull.Value) entidad.cod_cuenta_contra = Convert.ToString(resultado["COD_CUENTA_CONTRA"]);

                            lstCajaDeCompensacion.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCajaDeCompensacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CajaDeCompensacionData", "ListarCajaDeCompensacion", ex);
                        return null;
                    }
                }
            }
        }


    }
}