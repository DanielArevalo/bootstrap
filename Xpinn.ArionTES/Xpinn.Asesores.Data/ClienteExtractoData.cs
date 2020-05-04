using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Data
{
    public class ClienteExtractoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para Programa
        /// </summary>
        public ClienteExtractoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Obtiene la lista de Lineas de credito
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Lineas de credito obtenidos</returns>
        public List<ClienteExtracto> ListarClienteExtractos(ClienteExtracto entidad, string Filtro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ClienteExtracto> lstClienteExtractos = new List<ClienteExtracto>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;

                        DbParameter pFILTRO = cmdTransaccionFactory.CreateParameter();
                        pFILTRO.ParameterName = "p_filtro";
                        if (Filtro.Length > 0) Filtro = " and " + Filtro;
                        pFILTRO.Value = Filtro;
                        cmdTransaccionFactory.Parameters.Add(pFILTRO);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_AS_CLIENTESEXT";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        cmdTransaccionFactory.Parameters.Clear();
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = "SELECT * FROM TEMP_EXTRACTO";
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            entidad = new ClienteExtracto();
                            //Asociar todos los valores a la entidad
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.NumeroRadicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.CodPersona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.Identificacion = Convert.ToInt64(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.Nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.CodigoLineaDeCredito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.Linea = Convert.ToString(resultado["LINEA"]);
                            if (resultado["SALDO_CAPITAL"] != DBNull.Value) entidad.SaldoCapital = Convert.ToDouble(resultado["SALDO_CAPITAL"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.Oficina = Convert.ToString(resultado["OFICINA"]);
                            lstClienteExtractos.Add(entidad);
                        }

                        return lstClienteExtractos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ClienteExtractoData", "ListarClienteExtractos", ex);
                        return null;
                    }
                }
            }
        }



        //MODIFICADO 
        public List<ClienteExtracto> ListarExtractoCliente(string filtro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ClienteExtracto> lstExtra = new List<ClienteExtracto>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM TEMP_EXTRACTO where 1 = 1 ORDER BY  NUMERO_RADICACION";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ClienteExtracto entidad = new ClienteExtracto();
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.NumeroRadicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.CodPersona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.Identificacion = Convert.ToInt64(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.Nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.CodigoLineaDeCredito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.Linea = Convert.ToString(resultado["LINEA"]);
                            if (resultado["SALDO_CAPITAL"] != DBNull.Value) entidad.SaldoCapital = Convert.ToInt64(resultado["SALDO_CAPITAL"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.Oficina = Convert.ToString(resultado["OFICINA"]);
                            lstExtra.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstExtra;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ClienteExtractoData", "ListarExtractoCliente", ex);
                        return null;
                    }
                }
            }
        }



        public void GenerarExtractoClientes(string Filtro, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pfiltro = cmdTransaccionFactory.CreateParameter();
                        pfiltro.ParameterName = "p_filtro";
                        if (Filtro != "") pfiltro.Value = Filtro; else pfiltro.Value = DBNull.Value;
                        pfiltro.Direction = ParameterDirection.Input;
                        pfiltro.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pfiltro);


                        DbParameter popcion = cmdTransaccionFactory.CreateParameter();
                        popcion.ParameterName = "p_opcion";
                        if (Filtro.Length > 0) popcion.Value = 1; else popcion.Value = 0;
                        cmdTransaccionFactory.Parameters.Add(popcion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_AS_CLIEN_EXTRACTO";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ClienteExtractoData", "GenerarExtractoClientes", ex);
                    }
                }
            }
        }

    }
}

