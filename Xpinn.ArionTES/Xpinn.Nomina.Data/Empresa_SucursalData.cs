using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Nomina.Entities;

namespace Xpinn.Nomina.Data
{
    public class Empresa_SucursalData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public Empresa_SucursalData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
        public Empresa_Sucursal CrearEmpresa_Sucursal(Empresa_Sucursal pEmpresa_Sucursal, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pEmpresa_Sucursal.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        if (pEmpresa_Sucursal.descripcion == null)
                            pdescripcion.Value = DBNull.Value;
                        else
                            pdescripcion.Value = pEmpresa_Sucursal.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter pciudad = cmdTransaccionFactory.CreateParameter();
                        pciudad.ParameterName = "p_ciudad";
                        if (pEmpresa_Sucursal.ciudad == null)
                            pciudad.Value = DBNull.Value;
                        else
                            pciudad.Value = pEmpresa_Sucursal.ciudad;
                        pciudad.Direction = ParameterDirection.Input;
                        pciudad.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pciudad);

                        DbParameter pdireccion = cmdTransaccionFactory.CreateParameter();
                        pdireccion.ParameterName = "p_direccion";
                        if (pEmpresa_Sucursal.direccion == null)
                            pdireccion.Value = DBNull.Value;
                        else
                            pdireccion.Value = pEmpresa_Sucursal.direccion;
                        pdireccion.Direction = ParameterDirection.Input;
                        pdireccion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdireccion);

                        DbParameter ptelefono = cmdTransaccionFactory.CreateParameter();
                        ptelefono.ParameterName = "p_telefono";
                        if (pEmpresa_Sucursal.telefono == null)
                            ptelefono.Value = DBNull.Value;
                        else
                            ptelefono.Value = pEmpresa_Sucursal.telefono;
                        ptelefono.Direction = ParameterDirection.Input;
                        ptelefono.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptelefono);

                        DbParameter pemail = cmdTransaccionFactory.CreateParameter();
                        pemail.ParameterName = "p_email";
                        if (pEmpresa_Sucursal.Email == null)
                            pemail.Value = DBNull.Value;
                        else
                            pemail.Value = pEmpresa_Sucursal.Email;
                        pemail.Direction = ParameterDirection.Input;
                        pemail.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pemail);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_EMPRESA_SU_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pEmpresa_Sucursal;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Empresa_SucursalData", "CrearEmpresa_Sucursal", ex);
                        return null;
                    }
                }
            }
        }


        public Empresa_Sucursal ModificarEmpresa_Sucursal(Empresa_Sucursal pEmpresa_Sucursal, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pEmpresa_Sucursal.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        if (pEmpresa_Sucursal.descripcion == null)
                            pdescripcion.Value = DBNull.Value;
                        else
                            pdescripcion.Value = pEmpresa_Sucursal.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter pciudad = cmdTransaccionFactory.CreateParameter();
                        pciudad.ParameterName = "p_ciudad";
                        if (pEmpresa_Sucursal.ciudad == null)
                            pciudad.Value = DBNull.Value;
                        else
                            pciudad.Value = pEmpresa_Sucursal.ciudad;
                        pciudad.Direction = ParameterDirection.Input;
                        pciudad.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pciudad);

                        DbParameter pdireccion = cmdTransaccionFactory.CreateParameter();
                        pdireccion.ParameterName = "p_direccion";
                        if (pEmpresa_Sucursal.direccion == null)
                            pdireccion.Value = DBNull.Value;
                        else
                            pdireccion.Value = pEmpresa_Sucursal.direccion;
                        pdireccion.Direction = ParameterDirection.Input;
                        pdireccion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdireccion);

                        DbParameter ptelefono = cmdTransaccionFactory.CreateParameter();
                        ptelefono.ParameterName = "p_telefono";
                        if (pEmpresa_Sucursal.telefono == null)
                            ptelefono.Value = DBNull.Value;
                        else
                            ptelefono.Value = pEmpresa_Sucursal.telefono;
                        ptelefono.Direction = ParameterDirection.Input;
                        ptelefono.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptelefono);

                        DbParameter pemail = cmdTransaccionFactory.CreateParameter();
                        pemail.ParameterName = "p_email";
                        if (pEmpresa_Sucursal.Email == null)
                            pemail.Value = DBNull.Value;
                        else
                            pemail.Value = pEmpresa_Sucursal.Email;
                        pemail.Direction = ParameterDirection.Input;
                        pemail.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pemail);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_EMPRESA_SU_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pEmpresa_Sucursal;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Empresa_SucursalData", "ModificarEmpresa_Sucursal", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarEmpresa_Sucursal(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Empresa_Sucursal pEmpresa_Sucursal = new Empresa_Sucursal();
                        pEmpresa_Sucursal = ConsultarEmpresa_Sucursal(pId, vUsuario);

                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pEmpresa_Sucursal.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_EMPRESA_SU_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Empresa_SucursalData", "EliminarEmpresa_Sucursal", ex);
                    }
                }
            }
        }

        public Empresa_Sucursal ConsultarDartosEmpresa_Sucursal(string pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Empresa_Sucursal entidad = new Empresa_Sucursal();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Empresa_Sucursal WHERE CONSECUTIVO = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["CIUDAD"] != DBNull.Value) entidad.ciudad = Convert.ToInt64(resultado["CIUDAD"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["EMAIL"] != DBNull.Value) entidad.Email = Convert.ToString(resultado["EMAIL"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Empresa_SucursalData", "ConsultarEmpresa_Sucursal", ex);
                        return null;
                    }
                }
            }
        }

        public Empresa_Sucursal ConsultarEmpresa_Sucursal(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Empresa_Sucursal entidad = new Empresa_Sucursal();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Empresa_Sucursal WHERE CONSECUTIVO = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["CIUDAD"] != DBNull.Value) entidad.ciudad = Convert.ToInt64(resultado["CIUDAD"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["EMAIL"] != DBNull.Value) entidad.Email = Convert.ToString(resultado["EMAIL"]);
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
                        BOExcepcion.Throw("Empresa_SucursalData", "ConsultarEmpresa_Sucursal", ex);
                        return null;
                    }
                }
            }
        }


        public List<Empresa_Sucursal> ListarEmpresa_Sucursal(string filtro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Empresa_Sucursal> lstEmpresa_Sucursal = new List<Empresa_Sucursal>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT Empresa_Sucursal.consecutivo,
                        Empresa_Sucursal.descripcion,
                        Empresa_Sucursal.ciudad,
                        Empresa_Sucursal.direccion,
                        Empresa_Sucursal.telefono,
                        Empresa_Sucursal.email
                        FROM Empresa_Sucursal " + filtro.ToString() + " ORDER BY CONSECUTIVO ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Empresa_Sucursal entidad = new Empresa_Sucursal();
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["CIUDAD"] != DBNull.Value) entidad.ciudad = Convert.ToInt64(resultado["CIUDAD"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["EMAIL"] != DBNull.Value) entidad.Email = Convert.ToString(resultado["EMAIL"]);
                            lstEmpresa_Sucursal.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstEmpresa_Sucursal;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Empresa_SucursalData", "ListarEmpresa_Sucursal", ex);
                        return null;
                    }
                }
            }
        }


    }
}
