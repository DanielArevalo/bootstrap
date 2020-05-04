using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.NIIF.Entities;

namespace Xpinn.NIIF.Data
{
   
    public class TransicionSegmentoNIFData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

       
        public TransicionSegmentoNIFData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public TransicionSegmentoNIF CrearTransicionSegmento(TransicionSegmentoNIF pTransi, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodsegmento = cmdTransaccionFactory.CreateParameter();
                        pcodsegmento.ParameterName = "p_codsegmento";
                        pcodsegmento.Value = pTransi.codsegmento;
                        pcodsegmento.Direction = ParameterDirection.Output;
                        pcodsegmento.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodsegmento);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "p_nombre";
                        pnombre.Value = pTransi.nombre;
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NIF_TRANSEGMEN_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pTransi.codsegmento = Convert.ToInt32(pcodsegmento.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pTransi;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TransicionSegmentoNIFData", "CrearTransicionSegmento", ex);
                        return null;
                    }
                }
            }
        }



        public TransicionSegmentoNIF ModificarTransicionSegmento(TransicionSegmentoNIF pTransi, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodsegmento = cmdTransaccionFactory.CreateParameter();
                        pcodsegmento.ParameterName = "p_codsegmento";
                        pcodsegmento.Value = pTransi.codsegmento;
                        pcodsegmento.Direction = ParameterDirection.Input;
                        pcodsegmento.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodsegmento);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "p_nombre";
                        pnombre.Value = pTransi.nombre;
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NIF_TRANSEGMEN_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                        return pTransi;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TransicionSegmentoNIFData", "ModificarTransicionSegmento", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarTransicionSegmentoNIF(Int32 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodsegmento = cmdTransaccionFactory.CreateParameter();
                        pcodsegmento.ParameterName = "p_codsegmento";
                        pcodsegmento.Value = pId;
                        pcodsegmento.Direction = ParameterDirection.Input;
                        pcodsegmento.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodsegmento);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NIF_TRANSEGMEN_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TransicionSegmentoNIFData", "EliminarTransicionSegmentoNIF", ex);
                    }
                }
            }
        }

        public List<TransicionDetalle> ListarDetalleSegmento(int codigoSegmento, Usuario usuario)
        {
            DbDataReader resultado;
            List<TransicionDetalle> listaEntidad = new List<TransicionDetalle>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT tDet.*, sc.NOMBRE as NombreCriterio
                                        FROM TRANSICION_SEGMENTO t
                                        JOIN Transicion_Segmento_cond tDet on tDet.CODSEGMENTO = t.CODSEGMENTO
                                        JOIN SCVARIABLE sc on sc.VARIABLE1 = tDet.Variable
                                        WHERE t.CODSEGMENTO = " + codigoSegmento + " ORDER BY VARIABLE ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TransicionDetalle entidad = new TransicionDetalle();

                            if (resultado["CODSEGMENTO"] != DBNull.Value) entidad.codsegmento = Convert.ToInt32(resultado["CODSEGMENTO"]);
                            if (resultado["NombreCriterio"] != DBNull.Value) entidad.nombre_criterio = Convert.ToString(resultado["NombreCriterio"]);
                            if (resultado["IDCONDICIONTRAN"] != DBNull.Value) entidad.idcondiciontran = Convert.ToInt32(resultado["IDCONDICIONTRAN"]);
                            if (resultado["VARIABLE"] != DBNull.Value) entidad.variable = Convert.ToString(resultado["VARIABLE"]);
                            if (resultado["OPERADOR"] != DBNull.Value) entidad.operador = Convert.ToString(resultado["OPERADOR"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToString(resultado["VALOR"]);
                            if (resultado["SEGUNDOVALOR"] != DBNull.Value) entidad.segundo_valor = Convert.ToString(resultado["SEGUNDOVALOR"]);

                            listaEntidad.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return listaEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TransicionSegmentoNIFData", "ListarDetalleSegmento", ex);
                        return null;
                    }
                }
            }
        }

        public TransicionSegmentoNIF ConsultarTransicionSegmentoNIF(TransicionSegmentoNIF pEntidad, Usuario vUsuario)
        {
            DbDataReader resultado;
            TransicionSegmentoNIF entidad = new TransicionSegmentoNIF();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select * from Transicion_Segmento " + ObtenerFiltro(pEntidad);
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["CODSEGMENTO"] != DBNull.Value) entidad.codsegmento = Convert.ToInt32(resultado["CODSEGMENTO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                        }
                       
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TransicionSegmentoNIFData", "ConsultarTransicionSegmentoNIF", ex);
                        return null;
                    }
                }
            }
        }



        public List<TransicionSegmentoNIF> ListarTransicionSegmento(TransicionSegmentoNIF pTransi, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<TransicionSegmentoNIF> lstNIF = new List<TransicionSegmentoNIF>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Transicion_Segmento " + ObtenerFiltro(pTransi) + " ORDER BY CODSEGMENTO ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            TransicionSegmentoNIF entidad = new TransicionSegmentoNIF();
                            if (resultado["CODSEGMENTO"] != DBNull.Value) entidad.codsegmento = Convert.ToInt32(resultado["CODSEGMENTO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            lstNIF.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstNIF;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TransicionSegmentoNIFData", "ListarTransicionSegmento", ex);
                        return null;
                    }
                }
            }
        }



        public TransicionDetalle Crear_MOD_DetalleTransicion(TransicionDetalle pTransi, Usuario vUsuario ,int opcion )
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidcondiciontran = cmdTransaccionFactory.CreateParameter();
                        pidcondiciontran.ParameterName = "p_idcondiciontran";
                        pidcondiciontran.Value = pTransi.idcondiciontran;
                        if(opcion == 1)
                            pidcondiciontran.Direction = ParameterDirection.Output;
                        else
                            pidcondiciontran.Direction = ParameterDirection.Input;
                        pidcondiciontran.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidcondiciontran);

                        DbParameter pcodsegmento = cmdTransaccionFactory.CreateParameter();
                        pcodsegmento.ParameterName = "p_codsegmento";
                        pcodsegmento.Value = pTransi.codsegmento;
                        pcodsegmento.Direction = ParameterDirection.Input;
                        pcodsegmento.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodsegmento);

                        DbParameter pvariable = cmdTransaccionFactory.CreateParameter();
                        pvariable.ParameterName = "p_variable";
                        pvariable.Value = pTransi.variable;
                        pvariable.Direction = ParameterDirection.Input;
                        pvariable.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pvariable);

                        DbParameter poperador = cmdTransaccionFactory.CreateParameter();
                        poperador.ParameterName = "p_operador";
                        poperador.Value = pTransi.operador;
                        poperador.Direction = ParameterDirection.Input;
                        poperador.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(poperador);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        pvalor.Value = pTransi.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter p_valor_segundo = cmdTransaccionFactory.CreateParameter();
                        p_valor_segundo.ParameterName = "p_valor_segundo";
                        if (!string.IsNullOrWhiteSpace(pTransi.segundo_valor))
                        {
                            p_valor_segundo.Value = pTransi.segundo_valor;
                        }
                        else
                        {
                            p_valor_segundo.Value = DBNull.Value;
                        }
                        
                        p_valor_segundo.Direction = ParameterDirection.Input;
                        p_valor_segundo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_valor_segundo);   

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        if(opcion == 1) //CREAR
                            cmdTransaccionFactory.CommandText = "USP_XPINN_NIF_DETSEGMENT_CREAR";
                        else
                            cmdTransaccionFactory.CommandText = "USP_XPINN_NIF_DETSEGMENT_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if(opcion == 1)
                            pTransi.idcondiciontran = Convert.ToInt32(pidcondiciontran.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pTransi;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TransicionSegmentoNIFData", "Crear_MOD_DetalleTransicion", ex);
                        return null;
                    }
                }
            }
        }



        public void EliminarDetalleTransicionNIF(Int32 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidcondiciontran = cmdTransaccionFactory.CreateParameter();
                        pidcondiciontran.ParameterName = "p_idcondiciontran";
                        pidcondiciontran.Value = pId;
                        pidcondiciontran.Direction = ParameterDirection.Input;
                        pidcondiciontran.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidcondiciontran);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NIF_DETSEGMENT_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TransicionSegmentoNIFData", "EliminarDetalleTransicionNIF", ex);
                    }
                }
            }
        }



        public List<TransicionDetalle> ListarDetalleTransicion(Int32 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<TransicionDetalle> lstNIF = new List<TransicionDetalle>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Transicion_Segmento_Cond where CODSEGMENTO = " + pId.ToString() + " ORDER BY IDCONDICIONTRAN ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            TransicionDetalle entidad = new TransicionDetalle();
                            if (resultado["IDCONDICIONTRAN"] != DBNull.Value) entidad.idcondiciontran = Convert.ToInt32(resultado["IDCONDICIONTRAN"]);
                            if (resultado["CODSEGMENTO"] != DBNull.Value) entidad.codsegmento = Convert.ToInt32(resultado["CODSEGMENTO"]);
                            if (resultado["VARIABLE"] != DBNull.Value) entidad.variable = Convert.ToString(resultado["VARIABLE"]);
                            if (resultado["OPERADOR"] != DBNull.Value) entidad.operador = Convert.ToString(resultado["OPERADOR"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToString(resultado["VALOR"]);
                            if (resultado["SEGUNDOVALOR"] != DBNull.Value) entidad.segundo_valor = Convert.ToString(resultado["SEGUNDOVALOR"]);

                            lstNIF.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstNIF;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TransicionSegmentoNIFData", "ListarDetalleTransicion", ex);
                        return null;
                    }
                }
            }
        }

    }
}