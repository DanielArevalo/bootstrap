using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla Vehiculos
    /// </summary>
    public class CreditoGerencialData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla Vehiculos
        /// </summary>
        public CreditoGerencialData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public List<Credito> ListarCreditoGerencial(Usuario pUsuario, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Xpinn.FabricaCreditos.Entities.Credito> lstCredito = new List<Xpinn.FabricaCreditos.Entities.Credito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select distinct C.Cod_Deudor, P.Identificacion, P.Tipo_Identificacion,T.Descripcion, P.Primer_Nombre, "
                                    +"p.Segundo_Nombre, p.Primer_Apellido, p.Segundo_Apellido From Credito C Inner Join Persona P "
                                    +"on C.Cod_Deudor = P.Cod_Persona left join Tipoidentificacion t on T.Codtipoidentificacion = P.Tipo_Identificacion " + filtro;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Xpinn.FabricaCreditos.Entities.Credito entidad = new Xpinn.FabricaCreditos.Entities.Credito();

                            if (resultado["COD_DEUDOR"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_DEUDOR"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["PRIMER_NOMBRE"] != DBNull.Value) entidad.primer_nombre = Convert.ToString(resultado["PRIMER_NOMBRE"]);
                            if (resultado["SEGUNDO_NOMBRE"] != DBNull.Value) entidad.segundo_nombre = Convert.ToString(resultado["SEGUNDO_NOMBRE"]);
                            if (resultado["PRIMER_APELLIDO"] != DBNull.Value) entidad.primer_apellido = Convert.ToString(resultado["PRIMER_APELLIDO"]);
                            if (resultado["SEGUNDO_APELLIDO"] != DBNull.Value) entidad.segundo_apellido = Convert.ToString(resultado["SEGUNDO_APELLIDO"]);

                            lstCredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoGerencialData", "ListarCreditoGerencial", ex);
                        return null;
                    }
                }
            }
        }




        public List<Atributos> ListarAtributosXlinea(string pLinea, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<Atributos> lstAtributos = new List<Atributos>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select A.Cod_Atr, T.Nombre, A.calculo_atr, A.tasa, A.tipo_tasa, A.desviacion, A.tipo_historico, A.cobra_mora From Atributoslinea A Inner Join Atributos T On A.Cod_Atr = T.Cod_Atr "
                                    + "where A.Cod_Linea_Credito = '" + pLinea.ToString() + "' And A.Cod_Atr = AtrCorriente()";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Atributos entidad = new Atributos();
                            if (resultado["Cod_Atr"] != DBNull.Value) entidad.cod_atr = Convert.ToInt64(resultado["Cod_Atr"]);
                            if (resultado["Nombre"] != DBNull.Value) entidad.nom_atr = Convert.ToString(resultado["Nombre"]);
                            if (resultado["calculo_atr"] != DBNull.Value) entidad.calculo_atr = Convert.ToInt64(resultado["calculo_atr"]);
                            if (resultado["tasa"] != DBNull.Value) entidad.tasa = Convert.ToDouble(resultado["tasa"]);
                            if (resultado["tipo_tasa"] != DBNull.Value) entidad.tipo_tasa = Convert.ToInt64(resultado["tipo_tasa"]);
                            if (resultado["desviacion"] != DBNull.Value) entidad.desviacion = Convert.ToDouble(resultado["desviacion"]);
                            if (resultado["tipo_historico"] != DBNull.Value) entidad.tipo_historico = Convert.ToInt64(resultado["tipo_historico"]);
                            if (resultado["cobra_mora"] != DBNull.Value) entidad.cobra_mora = Convert.ToInt64(resultado["cobra_mora"]);
                            lstAtributos.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAtributos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoGerencialData", "ListarAtributosXlinea", ex);
                        return null;
                    }
                }
            }        
        }


        public LineasCredito ConsultarDatosXatributos(LineasCredito pLineas, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            //List<Atributos> lstsAtributos = new List<Atributos>();
            LineasCredito entidad = new LineasCredito();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * From Atributoslinea Where Cod_Linea_Credito = '" + pLineas.cod_linea_credito+ "' and Cod_Atr = "+pLineas.cod_atr;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["COD_RANGO_ATR"] != DBNull.Value) entidad.cod_rango_atr = Convert.ToInt32(resultado["COD_RANGO_ATR"]);
                            if (resultado["COD_ATR"] != DBNull.Value) entidad.cod_atr = Convert.ToInt32(resultado["COD_ATR"]);
                            if (resultado["CALCULO_ATR"] != DBNull.Value) entidad.calculo_atr = Convert.ToString(resultado["CALCULO_ATR"]);
                            if (resultado["TIPO_HISTORICO"] != DBNull.Value) entidad.tipo_historico = Convert.ToInt32(resultado["TIPO_HISTORICO"]);
                            if (resultado["DESVIACION"] != DBNull.Value) entidad.desviacion = Convert.ToDecimal(resultado["DESVIACION"]);
                            if (resultado["TIPO_TASA"] != DBNull.Value) entidad.tipotasa = Convert.ToInt32(resultado["TIPO_TASA"]);
                            if (resultado["TASA"] != DBNull.Value) entidad.tasa = Convert.ToDecimal(resultado["TASA"]);
                            if (resultado["COBRA_MORA"] != DBNull.Value) entidad.cobra_mora = Convert.ToInt32(resultado["COBRA_MORA"]);                         
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoGerencialData", "ConsultarDatosXatributos", ex);
                        return null;
                    }
                }
            }
        }

    }
}