using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Aportes.Entities;

namespace Xpinn.Aportes.Data
{
    public class Persona_infcertificadoData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public Persona_infcertificadoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }



        public List<Int32> ListarAniosPersonaCertificado(Int64 pCodAsociado, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Int32> lstAnios = new List<Int32>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT DISTINCT TO_CHAR(FECHA_CORTE,'yyyy') AS ANIOS FROM PERSONA_INFCERTIFICADO where COD_PERSONA = " + pCodAsociado.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Int32 entidad = new Int32();

                            if (resultado["ANIOS"] != DBNull.Value) entidad = Convert.ToInt32(resultado["ANIOS"]);
                            lstAnios.Add(entidad);
                        }
                        return lstAnios;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Persona_infcertificadoData", "ListarAniosPersonaCertificado", ex);
                        return null;
                    }
                }
            }
        }



        public List<Persona_infcertificado> ListarInformacionCertificado(Persona_infcertificado pInfor,string pFiltro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Persona_infcertificado> lstInformacion = new List<Persona_infcertificado>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT SUM(VALOR_APORTES) VALOR_APORTES,SUM(VALOR_CARTERA) VALOR_CARTERA, SUM(VALOR_INTERESES) VALOR_INTERESES, 
                                        SUM(OTROS_INGRESOS) OTROS_INGRESOS,SUM(RETEFUENTE) RETEFUENTE FROM PERSONA_INFCERTIFICADO " + ObtenerFiltro(pInfor) + pFiltro + " ORDER BY IDCONSECUTIVO ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Persona_infcertificado entidad = new Persona_infcertificado();                            
                            if (resultado["VALOR_APORTES"] != DBNull.Value) entidad.valor_aportes = Convert.ToDecimal(resultado["VALOR_APORTES"]);
                            if (resultado["VALOR_CARTERA"] != DBNull.Value) entidad.valor_cartera = Convert.ToDecimal(resultado["VALOR_CARTERA"]);
                            if (resultado["VALOR_INTERESES"] != DBNull.Value) entidad.valor_intereses = Convert.ToDecimal(resultado["VALOR_INTERESES"]);
                            if (resultado["OTROS_INGRESOS"] != DBNull.Value) entidad.otros_ingresos = Convert.ToDecimal(resultado["OTROS_INGRESOS"]);
                            if (resultado["RETEFUENTE"] != DBNull.Value) entidad.retefuente = Convert.ToDecimal(resultado["RETEFUENTE"]);
                            lstInformacion.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstInformacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Persona_infcertificadoData", "ListarInformacionCertificado", ex);
                        return null;
                    }
                }
            }
        }




    }
}