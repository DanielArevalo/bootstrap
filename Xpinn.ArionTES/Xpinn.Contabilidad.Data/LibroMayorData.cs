using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.Contabilidad.Data
{
    /// <summary>
    /// Objeto de acceso a datos para PlanCuentas
    /// </summary>    
    public class LibroMayorData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para PlanCuentas
        /// </summary>
        public LibroMayorData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
        /// <summary>
        /// Método para consultar el libro auxiliar
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public List<LibroMayor> ListarLibroMayor(LibroMayor pEntidad, ref Double TotDeb, ref Double TotCre, Usuario vUsuario, bool isNiif)
        {
            DbDataReader resultado = default(DbDataReader);
            List<LibroMayor> lstMayor = new List<LibroMayor>();
            TotDeb = 0;
            TotCre = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;

                        DbParameter pFECHA = cmdTransaccionFactory.CreateParameter();
                        pFECHA.ParameterName = "PFECHA";
                        pFECHA.Value = pEntidad.fecha_corte;
                        pFECHA.Direction = ParameterDirection.Input;
                        pFECHA.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pFECHA);

                        DbParameter PCENINI = cmdTransaccionFactory.CreateParameter();
                        PCENINI.ParameterName = "PCENINI";
                        PCENINI.Value = pEntidad.cenini;
                        PCENINI.Direction = ParameterDirection.Input;
                        PCENINI.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PCENINI);

                        DbParameter PCENFIN = cmdTransaccionFactory.CreateParameter();
                        PCENFIN.ParameterName = "PCENFIN";
                        PCENFIN.Value = pEntidad.cenfin;
                        PCENFIN.Direction = ParameterDirection.Input;
                        PCENFIN.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PCENFIN);

                        DbParameter PNIVEL = cmdTransaccionFactory.CreateParameter();
                        PNIVEL.ParameterName = "PNIVEL";
                        PNIVEL.Value = pEntidad.nivel;
                        PNIVEL.Direction = ParameterDirection.Input;
                        PNIVEL.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PNIVEL);

                        DbParameter PCUENTASENCERO = cmdTransaccionFactory.CreateParameter();
                        PCUENTASENCERO.ParameterName = "PCUENTASENCERO";
                        PCUENTASENCERO.Value = pEntidad.mostrarceros;
                        PCUENTASENCERO.Direction = ParameterDirection.Input;
                        PCUENTASENCERO.DbType = DbType.Int16;
                        cmdTransaccionFactory.Parameters.Add(PCUENTASENCERO);

                        DbParameter PPORTERCEROS = cmdTransaccionFactory.CreateParameter();
                        PPORTERCEROS.ParameterName = "PPORTERCEROS";
                        PPORTERCEROS.Value = pEntidad.generarterceros;
                        PPORTERCEROS.Direction = ParameterDirection.Input;
                        PPORTERCEROS.DbType = DbType.Int16;
                        cmdTransaccionFactory.Parameters.Add(PPORTERCEROS);

                        DbParameter PNIVELSELECCIONADO = cmdTransaccionFactory.CreateParameter();
                        PNIVELSELECCIONADO.ParameterName = "PNIVELSELECCIONADO";
                        PNIVELSELECCIONADO.Value = pEntidad.solonivel;
                        PNIVELSELECCIONADO.Direction = ParameterDirection.Input;
                        PNIVELSELECCIONADO.DbType = DbType.Int16;
                        cmdTransaccionFactory.Parameters.Add(PNIVELSELECCIONADO);

                        DbParameter PEXCEDENTES = cmdTransaccionFactory.CreateParameter();
                        PEXCEDENTES.ParameterName = "PEXCEDENTES";
                        PEXCEDENTES.Value = pEntidad.excedentes;
                        PEXCEDENTES.Direction = ParameterDirection.Input;
                        PEXCEDENTES.DbType = DbType.Int16;
                        cmdTransaccionFactory.Parameters.Add(PEXCEDENTES);

                        DbParameter PMOSTRARMOVPER13 = cmdTransaccionFactory.CreateParameter();
                        PMOSTRARMOVPER13.ParameterName = "PMOSTRARMOVPER13";
                        if (pEntidad.mostrarmovper13 == null)
                            PMOSTRARMOVPER13.Value = DBNull.Value;
                        else
                            PMOSTRARMOVPER13.Value = pEntidad.mostrarmovper13;
                        PMOSTRARMOVPER13.Direction = ParameterDirection.InputOutput;
                        PMOSTRARMOVPER13.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PMOSTRARMOVPER13);

                        DbParameter PMONEDA = cmdTransaccionFactory.CreateParameter();
                        PMONEDA.ParameterName = "pMoneda";
                        PMONEDA.Value = pEntidad.cod_moneda;
                        PMONEDA.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(PMONEDA);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = isNiif == true ? "USP_XPINN_CON_LIBMAYNIIF" : "USP_XPINN_CON_LIBMAY";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LibroMayorData", "USP_XPINN_CON_LIBMAY", ex);
                        return null;
                    }
                };


                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        Configuracion conf = new Configuracion();

                        string sql = "Select * from TEMP_LIBROMAYOR Where fecha = To_Date('" + pEntidad.fecha_corte.ToShortDateString() + "','" + conf.ObtenerFormatoFecha() + "') Order by cod_cuenta";

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            LibroMayor entidad = new LibroMayor();

                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["NOMBRE_CUENTA"] != DBNull.Value) entidad.nombre_cuenta = Convert.ToString(resultado["NOMBRE_CUENTA"]);
                            if (resultado["COD_TERCERO"] != DBNull.Value) entidad.cod_tercero = Convert.ToInt64(resultado["COD_TERCERO"]);
                            if (resultado["IDEN_TERCERO"] != DBNull.Value) entidad.iden_tercero = Convert.ToString(resultado["IDEN_TERCERO"]);
                            if (resultado["NOM_TERCERO"] != DBNull.Value) entidad.nom_tercero = Convert.ToString(resultado["NOM_TERCERO"]);
                            if (resultado["SALDO_INICIAL_DEBITO"] != DBNull.Value) entidad.saldo_inicial_debito = Convert.ToDouble(resultado["SALDO_INICIAL_DEBITO"]);
                            if (resultado["SALDO_INICIAL_CREDITO"] != DBNull.Value) entidad.saldo_inicial_credito = Convert.ToDouble(resultado["SALDO_INICIAL_CREDITO"]);
                            if (resultado["DEBITO"] != DBNull.Value) entidad.debito = Convert.ToDouble(resultado["DEBITO"]);
                            if (resultado["CREDITO"] != DBNull.Value) entidad.credito = Convert.ToDouble(resultado["CREDITO"]);
                            if (resultado["SALDO_FINAL_DEBITO"] != DBNull.Value) entidad.saldo_final_debito = Convert.ToDouble(resultado["SALDO_FINAL_DEBITO"]);
                            if (resultado["SALDO_FINAL_CREDITO"] != DBNull.Value) entidad.saldo_final_credito = Convert.ToDouble(resultado["SALDO_FINAL_CREDITO"]);                            
                            lstMayor.Add(entidad);
                        }

                        
                        string sqltot = "";
                        if (pEntidad.solonivel == 1)
                            sqltot = "Select Sum(debito) As debito, Sum(credito) As credito from TEMP_LIBROMAYOR Where fecha = To_Date('" + pEntidad.fecha_corte.ToShortDateString() + "','" + conf.ObtenerFormatoFecha() + "') ";
                        else
                            sqltot = "Select Sum(debito) As debito, Sum(credito) As credito from TEMP_LIBROMAYOR Where fecha = To_Date('" + pEntidad.fecha_corte.ToShortDateString() + "','" + conf.ObtenerFormatoFecha() + "') And nivel = 1";

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sqltot;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {                           
                            if (resultado["DEBITO"] != DBNull.Value) TotDeb = Convert.ToDouble(resultado["DEBITO"]);
                            if (resultado["CREDITO"] != DBNull.Value) TotCre = Convert.ToDouble(resultado["CREDITO"]);                         
                        }

                        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        /// Calcular la utilidad crédito que fue sumada para que cuadren los valores
                        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        double utilidad = 0;
                        DateTime _fecha_inicial, _fecha_final;
                        _fecha_final = Convert.ToDateTime(pEntidad.fecha_corte);
                        _fecha_inicial = new DateTime(_fecha_final.Year, _fecha_final.Month, 1).AddDays(-1);
                        if (_fecha_final.Day == 31 && _fecha_final.Month == 12 && pEntidad.mostrarmovper13 == 1)
                            _fecha_inicial = _fecha_final;
                        string s_fecha_inicial, s_fecha_final;
                        s_fecha_inicial = " To_Date('" + _fecha_inicial.ToShortDateString() + "', '" + conf.ObtenerFormatoFecha() + "') ";
                        s_fecha_final = " To_Date('" + _fecha_final.ToShortDateString() + "', '" + conf.ObtenerFormatoFecha() + "') ";

                        if (pEntidad.mostrarmovper13 == 1 && Convert.ToDateTime(pEntidad.fecha_corte).Day == 31 && Convert.ToDateTime(pEntidad.fecha_corte).Month == 12)
                            sqltot = @"Select -(Case When (Select Count(*) From cierea Where tipo = 'N' And fecha = " + s_fecha_final + @" And estado = 'D') >= 1 
                                                    Then Nvl(Calcular_Utilidad_Anual(" + s_fecha_final + @", " + pEntidad.cenini.ToString() + @", " + pEntidad.cenfin.ToString() + @"), 0)
                                                    Else Nvl(Calcular_Utilidad(" + s_fecha_final + @", " + pEntidad.cenini.ToString() + @", " + pEntidad.cenfin.ToString() + @"), 0)
                                                End) As utilidad 
                                        From TEMP_LIBROMAYOR t Where t.cod_cuenta = '3' And t.fecha = To_Date('" + Convert.ToDateTime(pEntidad.fecha_corte).ToShortDateString() + "','" + conf.ObtenerFormatoFecha() + "') Group by t.fecha";
                        else
                            sqltot = @"Select (Case When (Select Count(*) From cierea Where tipo = 'N' And fecha = " + s_fecha_final + @" And estado = 'D') >= 1 
                                                    Then Nvl(Calcular_Utilidad_Anual(" + s_fecha_final + @", " + pEntidad.cenini.ToString() + @", " + pEntidad.cenfin.ToString() + @"), 0)
                                                    Else Nvl(Calcular_Utilidad(" + s_fecha_final + @", " + pEntidad.cenini.ToString() + @", " + pEntidad.cenfin.ToString() + @"), 0)
                                                End) -  
                                                (Case When (Select Count(*) From cierea Where tipo = 'N' And fecha = " + s_fecha_inicial + @" And estado = 'D') >= 1
                                                    Then Nvl(Calcular_Utilidad_Anual(" + s_fecha_inicial + @", " + pEntidad.cenini.ToString() + @", " + pEntidad.cenfin.ToString() + @"), 0)
                                                    Else Nvl(Calcular_Utilidad(" + s_fecha_inicial + @", " + pEntidad.cenini.ToString() + @", " + pEntidad.cenfin.ToString() + @"), 0) 
                                                End) As utilidad 
                                        From TEMP_LIBROMAYOR t Where t.cod_cuenta = '3' And t.fecha = To_Date('" + Convert.ToDateTime(pEntidad.fecha_corte).ToShortDateString() + "','" + conf.ObtenerFormatoFecha() + "') Group by t.fecha";
                        if ((pEntidad.excedentes == 1) && !(pEntidad.excedentes == 1 && pEntidad.mostrarmovper13 == 1))
                        { 
                            cmdTransaccionFactory.CommandType = CommandType.Text;
                            cmdTransaccionFactory.CommandText = sqltot;
                            resultado = cmdTransaccionFactory.ExecuteReader();

                            if (resultado.Read())
                            {
                                if (resultado["UTILIDAD"] != DBNull.Value) utilidad = Convert.ToDouble(resultado["UTILIDAD"]);
                                TotCre = TotCre - utilidad;
                            }
                        }

                        dbConnectionFactory.CerrarConexion(connection);

                        return lstMayor;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LibroMayorData", "ListarLibroMayor", ex);
                        return null;
                    }
                }
            }
        }
        


        /// <summary>
        /// Método para consultar fechas de cierre
        /// </summary>
        /// <param name="pPlanCuentas"></param>
        /// <param name="pUsuario"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public List<LibroMayor> ListarFechaCierre(Usuario pUsuario, string pTipo = "C")
        {
            DbDataReader resultado = default(DbDataReader);
            DateTime fecultcie = DateTime.MinValue;
            List<LibroMayor> lstFechaCierre = new List<LibroMayor>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = string.Empty;
                        if (pTipo != null)
                        {
                            sql = "Select Distinct fecha From cierea Where tipo = '" + pTipo + "' And estado = 'D' Order by fecha desc";
                        }

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            LibroMayor entidad = new LibroMayor();

                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"].ToString());
                            if (resultado["FECHA"] != DBNull.Value) fecultcie = Convert.ToDateTime(resultado["FECHA"].ToString());
                            lstFechaCierre.Add(entidad);
                        }

                        return lstFechaCierre;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LibroMayorData", "ListarFechaCierre", ex);
                        return null;
                    }
                }
            }
        }



        public void DatosEmpresa(ref string empresa, ref string nit, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            DateTime fecultcie = DateTime.MinValue;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "Select nombre, nit From empresa";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            if (resultado["NOMBRE"] != DBNull.Value) empresa = Convert.ToString(resultado["NOMBRE"].ToString());
                            if (resultado["NIT"] != DBNull.Value) nit = Convert.ToString(resultado["NIT"].ToString());
                        }

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LibroMayorData", "DatosEmpresa", ex);
                        return;
                    }
                }
            }
        }

    }
}
