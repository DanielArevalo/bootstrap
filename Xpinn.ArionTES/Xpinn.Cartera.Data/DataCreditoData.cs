using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Cartera.Entities;


namespace Xpinn.Cartera.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla cierre histórico
    /// </summary>
    public class DataCreditoData : GlobalData
    {      
         
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla  cierre histórico
        /// </summary>
        public DataCreditoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }



        public void ArchivoPlano(DateTime fecha, string tipo, string oficina, string ciudad, string codigo, string tipoEntrega, int archivo, int creditosEmpleados, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pfecha_corte = cmdTransaccionFactory.CreateParameter();
                        pfecha_corte.ParameterName = "pfecha_corte";
                        pfecha_corte.Value = fecha;
                        pfecha_corte.DbType = DbType.Date;

                        DbParameter pSuscriptor = cmdTransaccionFactory.CreateParameter();
                        pSuscriptor.ParameterName = "pSuscriptor";
                        pSuscriptor.Value = codigo;
                        pSuscriptor.DbType = DbType.Int32;

                        DbParameter pTipoCuenta = cmdTransaccionFactory.CreateParameter();
                        pTipoCuenta.ParameterName = "pTipoCuenta";
                        pTipoCuenta.Value = tipo;
                        pTipoCuenta.DbType = DbType.Int32;

                        DbParameter pOficina = cmdTransaccionFactory.CreateParameter();
                        pOficina.ParameterName = "pOficina";
                        pOficina.Value = oficina;
                        pOficina.DbType = DbType.String;
                            
                        DbParameter pCiudad = cmdTransaccionFactory.CreateParameter();
                        pCiudad.ParameterName = "pCiudad";
                        pCiudad.Value = ciudad;
                        pCiudad.DbType = DbType.String;

                        DbParameter pTipoArchivo = cmdTransaccionFactory.CreateParameter();
                        pTipoArchivo.ParameterName = "pTipoArchivo";
                        pTipoArchivo.Value = archivo;
                        pTipoArchivo.DbType = DbType.Int32;

                        DbParameter ptipoEntrega = cmdTransaccionFactory.CreateParameter();
                        ptipoEntrega.ParameterName = "ptipoEntrega";
                        ptipoEntrega.Value = tipoEntrega;
                        ptipoEntrega.DbType = DbType.String;

                        DbParameter pcreditosEmpleados = cmdTransaccionFactory.CreateParameter();
                        pcreditosEmpleados.ParameterName = "pcreditosEmpleados";
                        pcreditosEmpleados.Value = creditosEmpleados;
                        pcreditosEmpleados.DbType = DbType.Int32;

                        cmdTransaccionFactory.Parameters.Add(pfecha_corte);
                        cmdTransaccionFactory.Parameters.Add(pSuscriptor);
                        cmdTransaccionFactory.Parameters.Add(pTipoCuenta);
                        cmdTransaccionFactory.Parameters.Add(pOficina);
                        cmdTransaccionFactory.Parameters.Add(pCiudad);
                        cmdTransaccionFactory.Parameters.Add(pTipoArchivo);
                        cmdTransaccionFactory.Parameters.Add(ptipoEntrega);
                        cmdTransaccionFactory.Parameters.Add(pcreditosEmpleados);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_DATACREDITO";
                        cmdTransaccionFactory.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DataCreditoData", "ModificarControlTiempos", ex);
                    
                    }
                }
            }

        }


        public void ArchivoPlanoCIFIN(DateTime fecha, int pCod_paquete, int pTipoEntidad, string pCodigoEntidad, string pProbabilidad, string tipoEntrega, int archivo, int creditosempleados, Boolean IsAhorro, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pfecha_corte = cmdTransaccionFactory.CreateParameter();
                        pfecha_corte.ParameterName = "pfecha_corte";
                        pfecha_corte.Value = fecha;
                        pfecha_corte.DbType = DbType.Date;

                        DbParameter p_Cod_paquete = cmdTransaccionFactory.CreateParameter();
                        p_Cod_paquete.ParameterName = "pCod_paquete";
                        p_Cod_paquete.Value = pCod_paquete;
                        p_Cod_paquete.DbType = DbType.Int32;

                        DbParameter p_TipoEntidad = cmdTransaccionFactory.CreateParameter();
                        p_TipoEntidad.ParameterName = "pTipoEntidad";
                        p_TipoEntidad.Value = pTipoEntidad;
                        p_TipoEntidad.DbType = DbType.Int32;

                        DbParameter p_CodigoEntidad = cmdTransaccionFactory.CreateParameter();
                        p_CodigoEntidad.ParameterName = "pCodigoEntidad";
                        p_CodigoEntidad.Value = pCodigoEntidad;
                        p_CodigoEntidad.DbType = DbType.String;

                        DbParameter p_Probabilidad = cmdTransaccionFactory.CreateParameter();
                        p_Probabilidad.ParameterName = "pProbabilidad";
                        p_Probabilidad.Value = pProbabilidad;
                        p_Probabilidad.DbType = DbType.String;

                        DbParameter pTipoArchivo = cmdTransaccionFactory.CreateParameter();
                        pTipoArchivo.ParameterName = "pTipoArchivo";
                        pTipoArchivo.Value = archivo;
                        pTipoArchivo.DbType = DbType.Int32;

                        DbParameter ptipoEntrega = cmdTransaccionFactory.CreateParameter();
                        ptipoEntrega.ParameterName = "ptipoEntrega";
                        ptipoEntrega.Value = tipoEntrega;
                        ptipoEntrega.DbType = DbType.String;

                        DbParameter pcreditosEmpleados = cmdTransaccionFactory.CreateParameter();
                        pcreditosEmpleados.ParameterName = "pcreditosEmpleados";
                        pcreditosEmpleados.Value = creditosempleados;
                        pcreditosEmpleados.DbType = DbType.Int32;

                        cmdTransaccionFactory.Parameters.Add(pfecha_corte);
                        cmdTransaccionFactory.Parameters.Add(p_Cod_paquete);
                        cmdTransaccionFactory.Parameters.Add(p_TipoEntidad);
                        cmdTransaccionFactory.Parameters.Add(p_CodigoEntidad);
                        cmdTransaccionFactory.Parameters.Add(p_Probabilidad);
                        cmdTransaccionFactory.Parameters.Add(pTipoArchivo);
                        cmdTransaccionFactory.Parameters.Add(ptipoEntrega);
                        cmdTransaccionFactory.Parameters.Add(pcreditosEmpleados);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        if (!IsAhorro)
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_CIFIN";
                        else
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_CIFIN_AHORRO";
                        cmdTransaccionFactory.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DataCreditoData", "ArchivoPlanoCIFIN", ex);

                    }
                }
            }

        }


        public List<DataCredito> listarArchivoPlano(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<DataCredito> listarchivo = new List<DataCredito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select * from temp_datacredito order by idlinea";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            DataCredito entidad = new DataCredito(); 
                            if (resultado["LINEA"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["LINEA"]);
                            listarchivo.Add(entidad);
                        }

                        return listarchivo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DataCreditoData", "ListaTGarantiasReales", ex);
                        return null;
                    }
                }
            }
        }

        public Int64 ValidarInformacionCentrales(DateTime pfecha,string pFiltro, Usuario pUsuario)
        {
            Int64 numero = 0;
            DbDataReader resultado = default(DbDataReader);

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion config = new Configuracion();
                        string sql = "";
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                        {
                            sql = @"Select Count(*) As numero From informacion_centrales_riesgo a 
                                        Where a.fecha_corte = to_date('" + pfecha.ToString(config.ObtenerFormatoFecha()) + "','" + config.ObtenerFormatoFecha() + "') " + pFiltro;
                        }
                        else
                        {
                            sql = @"Select Count(*) As numero From informacion_centrales_riesgo a 
                                   Where a.fecha_corte = '" + pfecha.ToString(config.ObtenerFormatoFecha()) + "' " + pFiltro;
                        }
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["NUMERO"] != DBNull.Value) numero = Convert.ToInt64(resultado["NUMERO"]);
                        }

                        return numero;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DataCreditoData", "ValidarInformacionCentrales", ex);
                        return 0;
                    }
                }
            }
        }


        public void InformacionCentralesRiesgo(DateTime pfecha, int pNuevo, int pCodeudores, int pTipo_producto, Usuario pUsuario, ref string serror)
        {
            serror = "";

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter PFECHA_CORTE = cmdTransaccionFactory.CreateParameter();
                        PFECHA_CORTE.ParameterName = "PFECHA_CORTE";
                        PFECHA_CORTE.Value = pfecha;
                        PFECHA_CORTE.DbType = DbType.Date;

                        DbParameter PNUEVO = cmdTransaccionFactory.CreateParameter();
                        PNUEVO.ParameterName = "PNUEVO";
                        PNUEVO.Value = pNuevo;
                        PNUEVO.DbType = DbType.Int32;

                        DbParameter PCODEUDORES = cmdTransaccionFactory.CreateParameter();
                        PCODEUDORES.ParameterName = "PCODEUDORES";
                        PCODEUDORES.Value = pCodeudores;
                        PCODEUDORES.DbType = DbType.Int32;

                        DbParameter PTIPOPRODUCTO = cmdTransaccionFactory.CreateParameter();
                        PTIPOPRODUCTO.ParameterName = "PTIPOPRODUCTO";
                        PTIPOPRODUCTO.Value = pTipo_producto;
                        PTIPOPRODUCTO.DbType = DbType.Int32;
                        
                        cmdTransaccionFactory.Parameters.Add(PFECHA_CORTE);
                        cmdTransaccionFactory.Parameters.Add(PNUEVO);
                        cmdTransaccionFactory.Parameters.Add(PCODEUDORES);
                        cmdTransaccionFactory.Parameters.Add(PTIPOPRODUCTO);
                         
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_CENTRALRIESGO";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        connection.Close();
                        serror = ex.Message;
                        return;
                    }
                }
            }
        }

  
    }
}