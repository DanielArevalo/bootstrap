using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using Xpinn.Reporteador.Entities;
using Xpinn.Util;

namespace Xpinn.Reporteador.Data
{
    public class AFIANCOLData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory = new ConnectionDataBase();
        public List<FechaCorte> ListarFecha(Usuario vUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<FechaCorte> lstAfiancolReportes = new List<FechaCorte>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql =
                            @"select distinct fecha from cierea where estado = 'D' And Tipo in ('A','C')";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            FechaCorte entidad = new FechaCorte();

                            if (resultado["fecha"] != DBNull.Value) entidad.Fecha = Convert.ToDateTime(resultado["fecha"]);

                            lstAfiancolReportes.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAfiancolReportes;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AFIANCOLData", "ListarFechas", ex);
                        return null;
                    }
                }
            }
        }

        public bool LlenarTablaAfiancol(Usuario vUsuario, DateTime FechaCorte)
        {

            //DbDataReader resultado = default(DbDataReader);

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        //Ejecuta Proceso para llenar la tabla de Casuacion De Afiancol 
                        DbParameter PFECHAHISTORICO = cmdTransaccionFactory.CreateParameter();
                        PFECHAHISTORICO.ParameterName = "PFECHAHISTORICO";
                        PFECHAHISTORICO.Value = FechaCorte.ToString("yyyy-MM-dd");
                        PFECHAHISTORICO.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(PFECHAHISTORICO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REP_AFIANCOL";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AFIANCOLData", "LlenarTablaAfiancol", ex);
                        return false;
                    }
                }
            }
        }

        public List<AFIANCOL_Reporte> ListarReporte(Usuario vUsuario, DateTime FechaCorte)
        {

            DbDataReader resultado = default(DbDataReader);
            List<AFIANCOL_Reporte> lstAfiancolReportes = new List<AFIANCOL_Reporte>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        string sql =
                             @"select * from CAUSACION_AFIANCOL where FECHA_HISTORICO = date'" + FechaCorte.ToString("yyyy-MM-dd") + "'";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            AFIANCOL_Reporte entidad = new AFIANCOL_Reporte();

                            if (resultado["FECHA_HISTORICO"] != DBNull.Value) entidad.FechaHistorico = Convert.ToDateTime(resultado["FECHA_HISTORICO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.Identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRES_APELLIDOS"] != DBNull.Value) entidad.NombreApellidos = Convert.ToString(resultado["NOMBRES_APELLIDOS"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.NumeroRadicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["FECHA_DESEMBOLSO"] != DBNull.Value) entidad.FechaDesembolso = Convert.ToDateTime(resultado["FECHA_DESEMBOLSO"]);
                            if (resultado["PLAZO"] != DBNull.Value) entidad.Plazo = Convert.ToInt32(resultado["PLAZO"]);
                            if (resultado["VALOR_DESEMBOLSADO"] != DBNull.Value) entidad.ValorDesembolsado = Convert.ToInt32(resultado["VALOR_DESEMBOLSADO"]);
                            if (resultado["SALDO_CREDITO"] != DBNull.Value) entidad.SaldoCredito = Convert.ToInt32(resultado["SALDO_CREDITO"]);
                            if (resultado["VALOR_APORTES"] != DBNull.Value) entidad.ValorAportes = Convert.ToInt32(resultado["VALOR_APORTES"]);
                            if (resultado["TASA_AFIANCOL"] != DBNull.Value) entidad.TasaAfiancol = Convert.ToDecimal(resultado["TASA_AFIANCOL"]);
                            if (resultado["SALDO_INSOLUTO"] != DBNull.Value) entidad.SaldoInsoluto = Convert.ToInt32(resultado["SALDO_INSOLUTO"]);
                            if (resultado["REMUNERACION"] != DBNull.Value) entidad.Remuneracion = Convert.ToInt32(resultado["REMUNERACION"]);
                            if (resultado["IVA"] != DBNull.Value) entidad.Iva = Convert.ToDecimal(resultado["IVA"]);
                            if (resultado["TOTAL"] != DBNull.Value) entidad.Total = Convert.ToInt32(resultado["TOTAL"]);

                            lstAfiancolReportes.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAfiancolReportes;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AFIANCOLData", "ListarReporte", ex);
                        return null;
                    }
                }
            }
        }


        //Consulta la tabla de causacion de afiancol 

        public bool CausarAfiancol(Usuario vUsuario, DateTime FechaCorte, Xpinn.Tesoreria.Entities.Operacion vOpe, ref Int64 pnum_comp, ref int ptipo_comp, ref string perror)
        {
            pnum_comp = 0;
            ptipo_comp = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter PFECHA_CORTE = cmdTransaccionFactory.CreateParameter();
                        PFECHA_CORTE.ParameterName = "PFECHA_CORTE";
                        PFECHA_CORTE.Value = Convert.ToDateTime(FechaCorte);
                        PFECHA_CORTE.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(PFECHA_CORTE);

                        DbParameter Ptipo_Ope = cmdTransaccionFactory.CreateParameter();
                        Ptipo_Ope.ParameterName = "Ptipo_Ope";
                        Ptipo_Ope.Value = vOpe.tipo_ope;
                        Ptipo_Ope.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(Ptipo_Ope);

                        DbParameter pCod_ope = cmdTransaccionFactory.CreateParameter();
                        pCod_ope.ParameterName = "pCod_ope";
                        pCod_ope.Value = vOpe.cod_ope;
                        pCod_ope.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pCod_ope);

                        DbParameter PCOD_PROCESO = cmdTransaccionFactory.CreateParameter();
                        PCOD_PROCESO.ParameterName = "PCOD_PROCESO";
                        PCOD_PROCESO.Value = Convert.ToInt64(vOpe.cod_proceso);
                        PCOD_PROCESO.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(PCOD_PROCESO);

                        DbParameter PCOD_USU = cmdTransaccionFactory.CreateParameter();
                        PCOD_USU.ParameterName = "PCOD_USU";
                        PCOD_USU.Value = Convert.ToInt32(vOpe.cod_usu);
                        PCOD_USU.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(PCOD_USU);

                        DbParameter PNUM_COMP = cmdTransaccionFactory.CreateParameter();
                        PNUM_COMP.ParameterName = "PNUM_COMP";
                        PNUM_COMP.Value = Convert.ToInt32(vOpe.num_comp);
                        PNUM_COMP.DbType = DbType.Int32;
                        PNUM_COMP.Direction = ParameterDirection.Output;
                        cmdTransaccionFactory.Parameters.Add(PNUM_COMP);

                        DbParameter PTIPO_COMP = cmdTransaccionFactory.CreateParameter();
                        PTIPO_COMP.ParameterName = "PTIPO_COMP";
                        PTIPO_COMP.Value = Convert.ToInt32(vOpe.tipo_comp);
                        PTIPO_COMP.DbType = DbType.Int32;
                        PTIPO_COMP.Direction = ParameterDirection.Output;
                        cmdTransaccionFactory.Parameters.Add(PTIPO_COMP);

                        DbParameter PERROR = cmdTransaccionFactory.CreateParameter();
                        PERROR.ParameterName = "PERROR";
                        PERROR.Value = " ";
                        PERROR.DbType = DbType.String;
                        PERROR.Size = 300;
                        PERROR.Direction = ParameterDirection.Output;
                        cmdTransaccionFactory.Parameters.Add(PERROR);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_CAUSAFIANCOL";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (PNUM_COMP.Value != null)
                            try { pnum_comp = Convert.ToInt64(PNUM_COMP.Value); } catch { }
                        if (PTIPO_COMP.Value != null)
                            try { ptipo_comp = Convert.ToInt32(PTIPO_COMP.Value); } catch { }
                        if (PERROR.Value != null)
                            perror = Convert.ToString(PERROR.Value);

                        dbConnectionFactory.CerrarConexion(connection);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        perror = ex.Message;
                        return false;
                    }
                }
            }
        }


        public AFIANCOL_Reporte ValidarList(Usuario vUsuario, DateTime FechaCorte)
        {
            DbDataReader resultado = default(DbDataReader);
            AFIANCOL_Reporte entidad = new AFIANCOL_Reporte();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select Count(*) Validar From operacion Where tipo_ope = 146 And Fecha_Oper = trunc(to_date('" + FechaCorte.ToString("dd/MM/yyyy") + "', 'DD/MM/YYYY')) And num_comp != -2 And tipo_comp != -2";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            if (resultado["Validar"] != DBNull.Value) entidad.Validar = Convert.ToInt32(resultado["Validar"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AFIANCOLData", "ListarReporte", ex);
                        return null;
                    }
                }
            }
        }
    }
}
