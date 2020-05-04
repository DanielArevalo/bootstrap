using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.CDATS.Entities;

namespace Xpinn.CDATS.Data
{
    public class LibroOficialCDATData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public LibroOficialCDATData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }



        public List<AdministracionCDAT> ListarCdat(string filtro, DateTime FechaIni,DateTime FechaFin, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<AdministracionCDAT> lstCdat = new List<AdministracionCDAT>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"Select C.Codigo_Cdat,C.Numero_Cdat, C.Fecha_Apertura,C.Fecha_Vencimiento,C.Valor,C.Tasa_Efectiva,C.Tasa_Nominal, "
                                        + "Case C.Modalidad_Int When 1 Then 'VENCIDO' When 2 Then 'ANTICIPADO' End As Nommodalidadint, "
                                        + "P.Descripcion As Nomperiodicidad,C.Plazo, "
                                        + "T.Cod_Persona, V.Identificacion,V.Nombres,V.Apellidos,V.Direccion,V.Telefono, "
                                        + "Case C.Modalidad When 'IND' Then 'INDIVIDUAL' When 'CON' Then 'CONJUNTO' When 'ALT' Then 'ALTERNA' End As Modalidad, "
                                        + "Case C.Estado When 1 Then 'APERTURA' When 2 then 'ACTIVO' When 3 Then 'TERMINADO' "
                                        + "when 4 then 'ANULADO' when 5 then 'EMBARGADO' end as nomestado,O.Nombre as nomOficina "
                                        + "From Cdat C Left Join Periodicidad P "
                                        + "On P.Cod_Periodicidad = C.Cod_Periodicidad_Int "
                                        + "Inner Join Cdat_Titular T On T.Codigo_Cdat = C.Codigo_Cdat "
                                        + "inner join Oficina O on O.Cod_Oficina = C.Cod_Oficina "
                                        + "inner join V_Persona v on V.Cod_Persona = T.Cod_Persona  where 1 = 1 " + filtro;
                        if (FechaIni != null && FechaIni != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " And ";
                            else
                                sql += " Where ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " C.Fecha_Apertura >= To_Date('" + Convert.ToDateTime(FechaIni).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " C.Fecha_Apertura >= '" + Convert.ToDateTime(FechaIni).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }

                        if (FechaFin != null && FechaFin != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " And ";
                            else
                                sql += " Where ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " C.Fecha_Apertura <= To_Date('" + Convert.ToDateTime(FechaFin).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " C.Fecha_Apertura <= '" + Convert.ToDateTime(FechaFin).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }

                        sql += " ORDER BY C.CODIGO_CDAT ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            AdministracionCDAT entidad = new AdministracionCDAT();
                            if (resultado["CODIGO_CDAT"] != DBNull.Value) entidad.codigo_cdat = Convert.ToInt64(resultado["CODIGO_CDAT"]);
                            if (resultado["NUMERO_CDAT"] != DBNull.Value) entidad.numero_cdat = Convert.ToString(resultado["NUMERO_CDAT"]);
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                            if (resultado["FECHA_VENCIMIENTO"] != DBNull.Value) entidad.fecha_vencimiento = Convert.ToDateTime(resultado["FECHA_VENCIMIENTO"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["TASA_EFECTIVA"] != DBNull.Value) entidad.tasa_efectiva = Convert.ToDecimal(resultado["TASA_EFECTIVA"]);
                            if (resultado["TASA_NOMINAL"] != DBNull.Value) entidad.tasa_nominal = Convert.ToDecimal(resultado["TASA_NOMINAL"]);
                            if (resultado["NOMMODALIDADINT"] != DBNull.Value) entidad.nommodalidadint = Convert.ToString(resultado["NOMMODALIDADINT"]);
                            if (resultado["NOMPERIODICIDAD"] != DBNull.Value) entidad.nomperiodicidad = Convert.ToString(resultado["NOMPERIODICIDAD"]);

                            if (entidad.nomperiodicidad == "" || entidad.nomperiodicidad == null)
                            {
                                entidad.nomperiodicidad = "AL VENCIMIENTO";
                            }
                            if (resultado["PLAZO"] != DBNull.Value) entidad.plazo = Convert.ToInt32(resultado["PLAZO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["APELLIDOS"] != DBNull.Value) entidad.apellidos = Convert.ToString(resultado["APELLIDOS"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["MODALIDAD"] != DBNull.Value) entidad.modalidad = Convert.ToString(resultado["MODALIDAD"]);
                            if (resultado["NOMESTADO"] != DBNull.Value) entidad.nomestado = Convert.ToString(resultado["NOMESTADO"]);
                            if (resultado["NOMOFICINA"] != DBNull.Value) entidad.nomoficina = Convert.ToString(resultado["NOMOFICINA"]);
                            lstCdat.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCdat;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LibroOficialCDATData", "ListarCdat", ex);
                        return null;
                    }
                }
            }
        }



        
    }
}
