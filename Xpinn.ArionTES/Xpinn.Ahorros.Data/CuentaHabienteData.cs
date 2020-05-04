using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Ahorros.Entities;

namespace Xpinn.Ahorros.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla Ahorro_Vista
    /// </summary>
    public class CuentaHabienteData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla Ahorro_Vista
        /// </summary>
        public CuentaHabienteData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public CuentaHabientes CrearCuentaHabientes(CuentaHabientes pCuentaHabientes, Usuario vUsuario, int opcion)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter pidcuenta_habiente = cmdTransaccionFactory.CreateParameter();
                        pidcuenta_habiente.ParameterName = "p_idcuenta_habiente";
                        pidcuenta_habiente.Value = pCuentaHabientes.idcuenta_habiente;

                        if (opcion == 1)//Crear
                            pidcuenta_habiente.Direction = ParameterDirection.Output;
                        else //Modificar
                            pidcuenta_habiente.Direction = ParameterDirection.Input;

                        pidcuenta_habiente.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidcuenta_habiente);

                      

                        DbParameter pnumero_cuenta = cmdTransaccionFactory.CreateParameter();
                        pnumero_cuenta.ParameterName = "p_numero_cuenta";
                        pnumero_cuenta.Value = pCuentaHabientes.numero_cuenta;
                        pnumero_cuenta.Direction = ParameterDirection.Input;
                        pnumero_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumero_cuenta);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pCuentaHabientes.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter ptipo_firma = cmdTransaccionFactory.CreateParameter();
                        ptipo_firma.ParameterName = "p_tipo_firma";
                        if (pCuentaHabientes.tipo_firma == null)
                            ptipo_firma.Value = DBNull.Value;
                        else
                            ptipo_firma.Value = pCuentaHabientes.tipo_firma;
                        ptipo_firma.Direction = ParameterDirection.Input;
                        ptipo_firma.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(ptipo_firma);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;



                        if (opcion == 1)//Crear
                            cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_CUENTAHAB_CREAR";
                        else //Modificar
                            cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_CUENTAHAB_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (opcion == 1)//Crear
                            pCuentaHabientes.idcuenta_habiente = Convert.ToInt64(pidcuenta_habiente.Value);

                        dbConnectionFactory.CerrarConexion(connection);

                        return pCuentaHabientes;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentaHabientesData", "CrearCuentaHabientes", ex);
                        return null;
                    }
                }
            }
        }


        public CuentaHabientes ModificarCuentaHabientes(CuentaHabientes pCuentaHabientes, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidcuenta_habiente = cmdTransaccionFactory.CreateParameter();
                        pidcuenta_habiente.ParameterName = "p_idcuenta_habiente";
                        pidcuenta_habiente.Value = pCuentaHabientes.idcuenta_habiente;
                        pidcuenta_habiente.Direction = ParameterDirection.InputOutput;
                        pidcuenta_habiente.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidcuenta_habiente);

                        DbParameter pnumero_cuenta = cmdTransaccionFactory.CreateParameter();
                        pnumero_cuenta.ParameterName = "p_numero_cuenta";
                        pnumero_cuenta.Value = pCuentaHabientes.numero_cuenta;
                        pnumero_cuenta.Direction = ParameterDirection.Input;
                        pnumero_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumero_cuenta);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pCuentaHabientes.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter ptipo_firma = cmdTransaccionFactory.CreateParameter();
                        ptipo_firma.ParameterName = "p_tipo_firma";
                        ptipo_firma.Value = pCuentaHabientes.tipo_firma;
                        ptipo_firma.Direction = ParameterDirection.Input;
                        ptipo_firma.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_firma);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_CUENTAHAB_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pCuentaHabientes;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentaHabientesData", "ModificarCuentaHabientes", ex);
                        return null;
                    }
                }
            }
        }

        public void EliminarCuentaHabientes(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        CuentaHabientes pCuentaHabientes = new CuentaHabientes();
                        pCuentaHabientes = ConsultarCuentaHabientes(pId, vUsuario);

                        DbParameter pidcuenta_habiente = cmdTransaccionFactory.CreateParameter();
                        pidcuenta_habiente.ParameterName = "p_idcuenta_habiente";
                        pidcuenta_habiente.Value = pCuentaHabientes.idcuenta_habiente;
                        pidcuenta_habiente.Direction = ParameterDirection.Input;
                        pidcuenta_habiente.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidcuenta_habiente);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_CUENTAHABIENTES_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentaHabientesData", "EliminarCuentaHabientes", ex);
                    }
                }
            }
        }

        public CuentaHabientes ConsultarCuentaHabientes(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            CuentaHabientes entidad = new CuentaHabientes();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Cuenta_Habientes WHERE IDCUENTA_HABIENTE = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDCUENTA_HABIENTE"] != DBNull.Value) entidad.idcuenta_habiente = Convert.ToInt32(resultado["IDCUENTA_HABIENTE"]);
                            if (resultado["NUMERO_CUENTA"] != DBNull.Value) entidad.numero_cuenta = Convert.ToString(resultado["NUMERO_CUENTA"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt32(resultado["COD_PERSONA"]);
                            if (resultado["TIPO_FIRMA"] != DBNull.Value) entidad.tipo_firma = Convert.ToInt32(resultado["TIPO_FIRMA"]);
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
                        BOExcepcion.Throw("CuentaHabientesData", "ConsultarCuentaHabientes", ex);
                        return null;
                    }
                }
            }
        }

        public List<CuentaHabientes> ListarCuentaHabientes(CuentaHabientes pCuentaHabientes, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<CuentaHabientes> lstCuentaHabientes = new List<CuentaHabientes>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Cuenta_Habientes " + ObtenerFiltro(pCuentaHabientes) + " ORDER BY IDCUENTA_HABIENTE ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            CuentaHabientes entidad = new CuentaHabientes();
                            if (resultado["IDCUENTA_HABIENTE"] != DBNull.Value) entidad.idcuenta_habiente = Convert.ToInt32(resultado["IDCUENTA_HABIENTE"]);
                            if (resultado["NUMERO_CUENTA"] != DBNull.Value) entidad.numero_cuenta = Convert.ToString(resultado["NUMERO_CUENTA"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt32(resultado["COD_PERSONA"]);
                            if (resultado["TIPO_FIRMA"] != DBNull.Value) entidad.tipo_firma = Convert.ToInt32(resultado["TIPO_FIRMA"]);
                            lstCuentaHabientes.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCuentaHabientes;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentaHabientesData", "ListarCuentaHabientes", ex);
                        return null;
                    }
                }
            }
        }

    }
}
