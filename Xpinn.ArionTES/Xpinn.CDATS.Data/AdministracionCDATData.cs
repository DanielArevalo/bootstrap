using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.CDATS.Entities;

namespace Xpinn.CDATS.Data
{
    public class AdministracionCDATData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public AdministracionCDATData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }




        public List<AdministracionCDAT> ListarCdat(string filtro, DateTime FechaApe, Usuario vUsuario, DateTime FechaVencimiento)
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
                        string sql = @"Select C.Codigo_Cdat,c.Numero_Cdat,C.Numero_Fisico, C.Fecha_Apertura, "
                                        + "Case C.Modalidad When 'IND' Then 'INDIVIDUAL' When 'CON' Then 'CONJUNTO' When 'ALT' Then 'ALTEERNA' End As Modalidad, "
                                        + "F.Descripcion As Nomcapta, C.Valor,M.Descripcion As Nommoneda,C.Plazo, "
                                        + "Case C.Tipo_Calendario When 1 Then 'COMERCIAL' When 2 Then 'CALENDARIO' End As Nomtipocalendario, "
                                        + "D.Descripcion As Nomdestinacion, O.Nombre As Nomoficina, "
                                        + "Case C.Tipo_Interes When '0' Then 'Ninguno' When '1' Then 'Tasa Fija' When '2' Then 'Histórico Fijo' When '3' Then 'Histórico Variable' End As Nomtipointeres, "
                                        + "C.Tasa_Interes, (select T.Nombre from Tipotasa T where T.COD_TIPO_TASA = C.COD_TIPO_TASA ) As Nomtipotasa, "
                                        + "(Select H.Descripcion from Tipotasahist H where H.Tipo_Historico = C.Tipo_Historico ) As Nomtipohistorico,C.Desviacion, "
                                        + "Case C.Modalidad_Int when 1 then 'VENCIDO' when 2 then 'ANTICIPADO' end as NomModalidadInt, "
                                        + "P.Descripcion As Nomperiodicidad, U.Nombre as NomUsuario,"
                                        + "Case C.Capitalizar_Int When 0 Then 'NO' When 1 Then 'SI' End As Nomcapitaliza, "
                                        + "Case C.Cobra_Retencion When 0 Then 'NO' When 1 Then 'SI' End As Nomretencion, "
                                        + "Case C.Desmaterializado when 0 then 'NO' When 1 then 'SI' end As nomDesmate,C.Estado,c.fecha_vencimiento "
                                        + "From Cdat C Inner Join Oficina O On C.Cod_Oficina = O.Cod_Oficina "
                                        + "Left Join Formacaptacion_Cdat F On F.Codforma_Captacion = C.Codforma_Captacion "
                                        + "Left Join Tipomoneda M On C.Cod_Moneda = M.Cod_Moneda "
                                        + "left Join Destinacion_Cdat D On C.Cod_Destinacion = D.Cod_Destinacion "
                                        + "left join Usuarios U on U.Codusuario = C.Cod_Asesor_Com "
                                        + "left join Periodicidad P on P.Cod_Periodicidad = C.Cod_Periodicidad_Int where 1 = 1 " + filtro;

                        if (FechaApe != null && FechaApe != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " And ";
                            else
                                sql += " Where ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " C.Fecha_Apertura = To_Date('" + Convert.ToDateTime(FechaApe).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " C.Fecha_Apertura = '" + Convert.ToDateTime(FechaApe).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }


                        if (FechaVencimiento != null && FechaVencimiento != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " And ";
                            else
                                sql += " Where ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " C.Fecha_Vencimiento = To_Date('" + Convert.ToDateTime(FechaVencimiento).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " C.Fecha_Vencimiento = '" + Convert.ToDateTime(FechaVencimiento).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }

                        sql += " ORDER BY c.fecha_vencimiento asc ";

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
                            if (resultado["NUMERO_FISICO"] != DBNull.Value) entidad.numero_fisico = Convert.ToString(resultado["NUMERO_FISICO"]);
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                            if (resultado["MODALIDAD"] != DBNull.Value) entidad.modalidad = Convert.ToString(resultado["MODALIDAD"]);
                            if (resultado["NOMCAPTA"] != DBNull.Value) entidad.nomcapta = Convert.ToString(resultado["NOMCAPTA"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["NOMMONEDA"] != DBNull.Value) entidad.nommoneda = Convert.ToString(resultado["NOMMONEDA"]);
                            if (resultado["PLAZO"] != DBNull.Value) entidad.plazo = Convert.ToInt32(resultado["PLAZO"]);
                            if (resultado["NOMTIPOCALENDARIO"] != DBNull.Value) entidad.nomtipocalendario = Convert.ToString(resultado["NOMTIPOCALENDARIO"]);
                            if (resultado["NOMDESTINACION"] != DBNull.Value) entidad.nomdestinacion = Convert.ToString(resultado["NOMDESTINACION"]);
                            if (resultado["NOMOFICINA"] != DBNull.Value) entidad.nomoficina = Convert.ToString(resultado["NOMOFICINA"]);
                            if (resultado["NOMTIPOINTERES"] != DBNull.Value) entidad.nomtipointeres = Convert.ToString(resultado["NOMTIPOINTERES"]);
                            if (resultado["TASA_INTERES"] != DBNull.Value) entidad.tasa_interes = Convert.ToDecimal(resultado["TASA_INTERES"]);
                            if (resultado["NOMTIPOTASA"] != DBNull.Value) entidad.nomtipotasa = Convert.ToString(resultado["NOMTIPOTASA"]);
                            if (resultado["NOMTIPOHISTORICO"] != DBNull.Value) entidad.nomtipohistorico = Convert.ToString(resultado["NOMTIPOHISTORICO"]);
                            if (resultado["DESVIACION"] != DBNull.Value) entidad.desviacion = Convert.ToDecimal(resultado["DESVIACION"]);
                            if (resultado["NOMMODALIDADINT"] != DBNull.Value) entidad.nommodalidadint = Convert.ToString(resultado["NOMMODALIDADINT"]);
                            if (resultado["NOMPERIODICIDAD"] != DBNull.Value) entidad.nomperiodicidad = Convert.ToString(resultado["NOMPERIODICIDAD"]);
                            if (resultado["NOMCAPITALIZA"] != DBNull.Value) entidad.nomcapitaliza = Convert.ToString(resultado["NOMCAPITALIZA"]);
                            if (resultado["NOMRETENCION"] != DBNull.Value) entidad.nomretencion = Convert.ToString(resultado["NOMRETENCION"]);
                            if (resultado["NOMDESMATE"] != DBNull.Value) entidad.nomdesmate = Convert.ToString(resultado["NOMDESMATE"]);
                            if (resultado["NOMUSUARIO"] != DBNull.Value) entidad.nomusuario = Convert.ToString(resultado["NOMUSUARIO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["FECHA_VENCIMIENTO"] != DBNull.Value) entidad.fecha_vencimiento = Convert.ToDateTime(resultado["FECHA_VENCIMIENTO"]);

                            lstCdat.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCdat;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AdministracionCDATData", "ListarCdat", ex);
                        return null;
                    }
                }
            }
        }
        public List<AdministracionCDAT> ListarCdatProroga(string filtro, DateTime FechaApe, Usuario vUsuario, DateTime FechaVencimiento)
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
                        string sql = @"Select C.Codigo_Cdat,c.Numero_Cdat,C.Numero_Fisico, C.Fecha_Apertura, "
                                        + "Case C.Modalidad When 'IND' Then 'INDIVIDUAL' When 'CON' Then 'CONJUNTO' When 'ALT' Then 'ALTEERNA' End As Modalidad, "
                                        + "F.Descripcion As Nomcapta, C.Valor,M.Descripcion As Nommoneda,C.Plazo, "
                                        + "Case C.Tipo_Calendario When 1 Then 'COMERCIAL' When 2 Then 'CALENDARIO' End As Nomtipocalendario, "
                                        + "D.Descripcion As Nomdestinacion, O.Nombre As Nomoficina, "
                                        + "Case C.Tipo_Interes When '0' Then 'Ninguno' When '1' Then 'Tasa Fija' When '2' Then 'Histórico Fijo' When '3' Then 'Histórico Variable' End As Nomtipointeres, "
                                        + "C.Tasa_Interes, (select T.Nombre from Tipotasa T where T.COD_TIPO_TASA = C.COD_TIPO_TASA ) As Nomtipotasa, "
                                        + "(Select H.Descripcion from Tipotasahist H where H.Tipo_Historico = C.Tipo_Historico ) As Nomtipohistorico,C.Desviacion, "
                                        + "Case C.Modalidad_Int when 1 then 'VENCIDO' when 2 then 'ANTICIPADO' end as NomModalidadInt, "
                                        + "P.Descripcion As Nomperiodicidad, U.Nombre as NomUsuario,"
                                        + "Case C.Capitalizar_Int When 0 Then 'NO' When 1 Then 'SI' End As Nomcapitaliza, "
                                        + "Case C.Cobra_Retencion When 0 Then 'NO' When 1 Then 'SI' End As Nomretencion, "
                                        + "Case C.Desmaterializado when 0 then 'NO' When 1 then 'SI' end As nomDesmate,C.Estado,c.fecha_vencimiento "
                                        + "From Cdat C Inner Join Oficina O On C.Cod_Oficina = O.Cod_Oficina "
                                        + "Left Join Formacaptacion_Cdat F On F.Codforma_Captacion = C.Codforma_Captacion "
                                        + "Left Join Tipomoneda M On C.Cod_Moneda = M.Cod_Moneda "
                                        + "left Join Destinacion_Cdat D On C.Cod_Destinacion = D.Cod_Destinacion "
                                        + "left join Usuarios U on U.Codusuario = C.Cod_Asesor_Com "
                                        + "left join Periodicidad P on P.Cod_Periodicidad = C.Cod_Periodicidad_Int where 1 = 1 " + filtro;

                        if (FechaApe != null && FechaApe != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " And ";
                            else
                                sql += " Where ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " C.Fecha_Apertura = To_Date('" + Convert.ToDateTime(FechaApe).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " C.Fecha_Apertura = '" + Convert.ToDateTime(FechaApe).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }


                        if (FechaVencimiento != null && FechaVencimiento != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " And ";
                            else
                                sql += " Where ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " C.Fecha_Vencimiento  <= To_Date('" + Convert.ToDateTime(FechaVencimiento).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " C.Fecha_Vencimiento <= '" + Convert.ToDateTime(FechaVencimiento).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }

                        sql += " ORDER BY  c.fecha_vencimiento asc ";

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
                            if (resultado["NUMERO_FISICO"] != DBNull.Value) entidad.numero_fisico = Convert.ToString(resultado["NUMERO_FISICO"]);
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                            if (resultado["MODALIDAD"] != DBNull.Value) entidad.modalidad = Convert.ToString(resultado["MODALIDAD"]);
                            if (resultado["NOMCAPTA"] != DBNull.Value) entidad.nomcapta = Convert.ToString(resultado["NOMCAPTA"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["NOMMONEDA"] != DBNull.Value) entidad.nommoneda = Convert.ToString(resultado["NOMMONEDA"]);
                            if (resultado["PLAZO"] != DBNull.Value) entidad.plazo = Convert.ToInt32(resultado["PLAZO"]);
                            if (resultado["NOMTIPOCALENDARIO"] != DBNull.Value) entidad.nomtipocalendario = Convert.ToString(resultado["NOMTIPOCALENDARIO"]);
                            if (resultado["NOMDESTINACION"] != DBNull.Value) entidad.nomdestinacion = Convert.ToString(resultado["NOMDESTINACION"]);
                            if (resultado["NOMOFICINA"] != DBNull.Value) entidad.nomoficina = Convert.ToString(resultado["NOMOFICINA"]);
                            if (resultado["NOMTIPOINTERES"] != DBNull.Value) entidad.nomtipointeres = Convert.ToString(resultado["NOMTIPOINTERES"]);
                            if (resultado["TASA_INTERES"] != DBNull.Value) entidad.tasa_interes = Convert.ToDecimal(resultado["TASA_INTERES"]);
                            if (resultado["NOMTIPOTASA"] != DBNull.Value) entidad.nomtipotasa = Convert.ToString(resultado["NOMTIPOTASA"]);
                            if (resultado["NOMTIPOHISTORICO"] != DBNull.Value) entidad.nomtipohistorico = Convert.ToString(resultado["NOMTIPOHISTORICO"]);
                            if (resultado["DESVIACION"] != DBNull.Value) entidad.desviacion = Convert.ToDecimal(resultado["DESVIACION"]);
                            if (resultado["NOMMODALIDADINT"] != DBNull.Value) entidad.nommodalidadint = Convert.ToString(resultado["NOMMODALIDADINT"]);
                            if (resultado["NOMPERIODICIDAD"] != DBNull.Value) entidad.nomperiodicidad = Convert.ToString(resultado["NOMPERIODICIDAD"]);
                            if (resultado["NOMCAPITALIZA"] != DBNull.Value) entidad.nomcapitaliza = Convert.ToString(resultado["NOMCAPITALIZA"]);
                            if (resultado["NOMRETENCION"] != DBNull.Value) entidad.nomretencion = Convert.ToString(resultado["NOMRETENCION"]);
                            if (resultado["NOMDESMATE"] != DBNull.Value) entidad.nomdesmate = Convert.ToString(resultado["NOMDESMATE"]);
                            if (resultado["NOMUSUARIO"] != DBNull.Value) entidad.nomusuario = Convert.ToString(resultado["NOMUSUARIO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["FECHA_VENCIMIENTO"] != DBNull.Value) entidad.fecha_vencimiento = Convert.ToDateTime(resultado["FECHA_VENCIMIENTO"]);

                            lstCdat.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCdat;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AdministracionCDATData", "ListarCdatProroga", ex);
                        return null;
                    }
                }
            }
        }
        public List<AdministracionCDAT> ListarCdatCancelacion(string filtro, DateTime FechaApe, Usuario vUsuario, DateTime FechaVencimiento, DateTime FechaVencimientoFinal)
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
                        string sql = @"Select C.Codigo_Cdat,c.Numero_Cdat,C.Numero_Fisico, C.Fecha_Apertura, "
                                        + "Case C.Modalidad When 'IND' Then 'INDIVIDUAL' When 'CON' Then 'CONJUNTO' When 'ALT' Then 'ALTEERNA' End As Modalidad, "
                                        + "F.Descripcion As Nomcapta, C.Valor,M.Descripcion As Nommoneda,C.Plazo, "
                                        + "Case C.Tipo_Calendario When 1 Then 'COMERCIAL' When 2 Then 'CALENDARIO' End As Nomtipocalendario, "
                                        + "D.Descripcion As Nomdestinacion, O.Nombre As Nomoficina, "
                                        + "Case C.Tipo_Interes When '0' Then 'Ninguno' When '1' Then 'Tasa Fija' When '2' Then 'Histórico Fijo' When '3' Then 'Histórico Variable' End As Nomtipointeres, "
                                        + "C.Tasa_Interes, (select T.Nombre from Tipotasa T where T.COD_TIPO_TASA = C.COD_TIPO_TASA ) As Nomtipotasa, "
                                        + "(Select H.Descripcion from Tipotasahist H where H.Tipo_Historico = C.Tipo_Historico ) As Nomtipohistorico,C.Desviacion, "
                                        + "Case C.Modalidad_Int when 1 then 'VENCIDO' when 2 then 'ANTICIPADO' end as NomModalidadInt, "
                                        + "P.Descripcion As Nomperiodicidad, U.Nombre as NomUsuario,"
                                        + "Case C.Capitalizar_Int When 0 Then 'NO' When 1 Then 'SI' End As Nomcapitaliza, "
                                        + "Case C.Cobra_Retencion When 0 Then 'NO' When 1 Then 'SI' End As Nomretencion, "
                                        + "Case C.Desmaterializado when 0 then 'NO' When 1 then 'SI' end As nomDesmate,C.Estado,c.fecha_vencimiento "
                                        + "From Cdat C Inner Join Oficina O On C.Cod_Oficina = O.Cod_Oficina "
                                        + "Left Join Formacaptacion_Cdat F On F.Codforma_Captacion = C.Codforma_Captacion "
                                        + "Left Join Tipomoneda M On C.Cod_Moneda = M.Cod_Moneda "
                                        + "left Join Destinacion_Cdat D On C.Cod_Destinacion = D.Cod_Destinacion "
                                        + "left join Usuarios U on U.Codusuario = C.Cod_Asesor_Com "
                                        + "left join Periodicidad P on P.Cod_Periodicidad = C.Cod_Periodicidad_Int where 1 = 1 " + filtro;

                        if (FechaApe != null && FechaApe != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " And ";
                            else
                                sql += " Where ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " C.Fecha_Apertura  To_Date('" + Convert.ToDateTime(FechaApe).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " C.Fecha_Apertura = '" + Convert.ToDateTime(FechaApe).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }


                        if (FechaVencimiento != null && FechaVencimiento != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " And ";
                            else
                                sql += " Where ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " C.Fecha_Vencimiento between To_Date('" + Convert.ToDateTime(FechaVencimiento).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')" + "and " + "+ To_Date('" + Convert.ToDateTime(FechaVencimientoFinal).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " C.Fecha_Vencimiento between '" + Convert.ToDateTime(FechaVencimiento).ToString(conf.ObtenerFormatoFecha()) + "' " + "and " + "+ To_Date('" + Convert.ToDateTime(FechaVencimientoFinal).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                        }

                        sql += " ORDER BY c.fecha_vencimiento asc ";

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
                            if (resultado["NUMERO_FISICO"] != DBNull.Value) entidad.numero_fisico = Convert.ToString(resultado["NUMERO_FISICO"]);
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                            if (resultado["MODALIDAD"] != DBNull.Value) entidad.modalidad = Convert.ToString(resultado["MODALIDAD"]);
                            if (resultado["NOMCAPTA"] != DBNull.Value) entidad.nomcapta = Convert.ToString(resultado["NOMCAPTA"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["NOMMONEDA"] != DBNull.Value) entidad.nommoneda = Convert.ToString(resultado["NOMMONEDA"]);
                            if (resultado["PLAZO"] != DBNull.Value) entidad.plazo = Convert.ToInt32(resultado["PLAZO"]);
                            if (resultado["NOMTIPOCALENDARIO"] != DBNull.Value) entidad.nomtipocalendario = Convert.ToString(resultado["NOMTIPOCALENDARIO"]);
                            if (resultado["NOMDESTINACION"] != DBNull.Value) entidad.nomdestinacion = Convert.ToString(resultado["NOMDESTINACION"]);
                            if (resultado["NOMOFICINA"] != DBNull.Value) entidad.nomoficina = Convert.ToString(resultado["NOMOFICINA"]);
                            if (resultado["NOMTIPOINTERES"] != DBNull.Value) entidad.nomtipointeres = Convert.ToString(resultado["NOMTIPOINTERES"]);
                            if (resultado["TASA_INTERES"] != DBNull.Value) entidad.tasa_interes = Convert.ToDecimal(resultado["TASA_INTERES"]);
                            if (resultado["NOMTIPOTASA"] != DBNull.Value) entidad.nomtipotasa = Convert.ToString(resultado["NOMTIPOTASA"]);
                            if (resultado["NOMTIPOHISTORICO"] != DBNull.Value) entidad.nomtipohistorico = Convert.ToString(resultado["NOMTIPOHISTORICO"]);
                            if (resultado["DESVIACION"] != DBNull.Value) entidad.desviacion = Convert.ToDecimal(resultado["DESVIACION"]);
                            if (resultado["NOMMODALIDADINT"] != DBNull.Value) entidad.nommodalidadint = Convert.ToString(resultado["NOMMODALIDADINT"]);
                            if (resultado["NOMPERIODICIDAD"] != DBNull.Value) entidad.nomperiodicidad = Convert.ToString(resultado["NOMPERIODICIDAD"]);
                            if (resultado["NOMCAPITALIZA"] != DBNull.Value) entidad.nomcapitaliza = Convert.ToString(resultado["NOMCAPITALIZA"]);
                            if (resultado["NOMRETENCION"] != DBNull.Value) entidad.nomretencion = Convert.ToString(resultado["NOMRETENCION"]);
                            if (resultado["NOMDESMATE"] != DBNull.Value) entidad.nomdesmate = Convert.ToString(resultado["NOMDESMATE"]);
                            if (resultado["NOMUSUARIO"] != DBNull.Value) entidad.nomusuario = Convert.ToString(resultado["NOMUSUARIO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["FECHA_VENCIMIENTO"] != DBNull.Value) entidad.fecha_vencimiento = Convert.ToDateTime(resultado["FECHA_VENCIMIENTO"]);

                            lstCdat.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCdat;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AdministracionCDATData", "ListarCdat", ex);
                        return null;
                    }
                }
            }
        }

        public AdministracionCDAT ModificarSoloNUmFISICO_CDAT(AdministracionCDAT pCdat, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodigo_cdat = cmdTransaccionFactory.CreateParameter();
                        pcodigo_cdat.ParameterName = "p_codigo_cdat";
                        pcodigo_cdat.Value = pCdat.codigo_cdat;
                        pcodigo_cdat.Direction = ParameterDirection.Input;
                        pcodigo_cdat.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigo_cdat);

                        DbParameter pnumero_fisico = cmdTransaccionFactory.CreateParameter();
                        pnumero_fisico.ParameterName = "p_numero_fisico";
                        if (pCdat.numero_fisico != null) pnumero_fisico.Value = pCdat.numero_fisico; else pnumero_fisico.Value = DBNull.Value;
                        pnumero_fisico.Direction = ParameterDirection.Input;
                        pnumero_fisico.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumero_fisico);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CDA_SOLONUMFISIC_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                        return pCdat;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AdministracionCDATData", "ModificarSoloNUmFISICO_CDAT", ex);
                        return null;
                    }
                }
            }
        }


        public List<AdministracionCDAT> getListarCdatBloquear(string filtro, DateTime FechaApe, Usuario vUsuario)
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
                        string sql = @"Select C.Codigo_Cdat, c.Numero_Cdat,C.Numero_Fisico, C.Fecha_Apertura, "
                                        + " Case C.Modalidad When 'IND' Then 'INDIVIDUAL' When 'CON' Then 'CONJUNTO' When 'ALT' Then 'ALTEERNA' End As Modalidad, "
                                        + " F.Descripcion As Nomcapta, C.Valor,M.Descripcion As Nommoneda,C.Plazo, "
                                        + " Case C.Tipo_Calendario When 1 Then 'COMERCIAL' When 2 Then 'CALENDARIO' End As Nomtipocalendario, "
                                        + " D.Descripcion As Nomdestinacion, O.Nombre As Nomoficina, "
                                        + " Case C.Tipo_Interes When 'Fijo' Then 'FIJO' When 'Vari' Then 'VARIABLE' End As Nomtipointeres,"
                                        + " C.Tasa_Interes, (select T.Nombre from Tipotasa T where T.COD_TIPO_TASA = C.COD_TIPO_TASA ) As Nomtipotasa,"
                                        + " (Select H.Descripcion from Tipotasahist H where H.Tipo_Historico = C.Tipo_Historico ) As Nomtipohistorico,C.Desviacion,"
                                        + " Case C.Modalidad_Int when 1 then 'VENCIDO' when 2 then 'ANTICIPADO' end as NomModalidadInt, "
                                        + " P.Descripcion As Nomperiodicidad, U.Nombre as NomUsuario,"
                                        + " Case C.Capitalizar_Int When 0 Then 'NO' When 1 Then 'SI' End As Nomcapitaliza, "
                                        + " Case C.Cobra_Retencion When 0 Then 'NO' When 1 Then 'SI' End As Nomretencion, "
                                        + " Case C.Desmaterializado when 0 then 'NO' When 1 then 'SI' end As nomDesmate,C.Estado "
                                        + " From Cdat C Left Join Oficina O On C.Cod_Oficina = O.Cod_Oficina "
                                        + " Left Join Formacaptacion_Cdat F On F.Codforma_Captacion = c.Codforma_Captacion "
                                        + " Left Join Tipomoneda M On C.Cod_Moneda = M.Cod_Moneda "
                                        + " Left Join Destinacion_Cdat D On C.Cod_Destinacion = D.Cod_Destinacion "
                                        + " Left join Usuarios U on U.Codusuario = C.Cod_Asesor_Com "
                                        + " Left join Periodicidad P on P.Cod_Periodicidad = C.Cod_Periodicidad_Int where 1 = 1 and (c.estado=2 or c.estado=5) " + filtro;

                        if (FechaApe != null && FechaApe != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " And ";
                            else
                                sql += " Where ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " C.Fecha_Apertura = To_Date('" + Convert.ToDateTime(FechaApe).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " C.Fecha_Apertura = '" + Convert.ToDateTime(FechaApe).ToString(conf.ObtenerFormatoFecha()) + "' ";
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
                            if (resultado["NUMERO_FISICO"] != DBNull.Value) entidad.numero_fisico = Convert.ToString(resultado["NUMERO_FISICO"]);
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                            if (resultado["MODALIDAD"] != DBNull.Value) entidad.modalidad = Convert.ToString(resultado["MODALIDAD"]);
                            if (resultado["NOMCAPTA"] != DBNull.Value) entidad.nomcapta = Convert.ToString(resultado["NOMCAPTA"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["NOMMONEDA"] != DBNull.Value) entidad.nommoneda = Convert.ToString(resultado["NOMMONEDA"]);
                            if (resultado["PLAZO"] != DBNull.Value) entidad.plazo = Convert.ToInt32(resultado["PLAZO"]);
                            if (resultado["NOMTIPOCALENDARIO"] != DBNull.Value) entidad.nomtipocalendario = Convert.ToString(resultado["NOMTIPOCALENDARIO"]);
                            if (resultado["NOMDESTINACION"] != DBNull.Value) entidad.nomdestinacion = Convert.ToString(resultado["NOMDESTINACION"]);
                            if (resultado["NOMOFICINA"] != DBNull.Value) entidad.nomoficina = Convert.ToString(resultado["NOMOFICINA"]);
                            if (resultado["NOMTIPOINTERES"] != DBNull.Value) entidad.nomtipointeres = Convert.ToString(resultado["NOMTIPOINTERES"]);
                            if (resultado["TASA_INTERES"] != DBNull.Value) entidad.tasa_interes = Convert.ToDecimal(resultado["TASA_INTERES"]);
                            if (resultado["NOMTIPOTASA"] != DBNull.Value) entidad.nomtipotasa = Convert.ToString(resultado["NOMTIPOTASA"]);
                            if (resultado["NOMTIPOHISTORICO"] != DBNull.Value) entidad.nomtipohistorico = Convert.ToString(resultado["NOMTIPOHISTORICO"]);
                            if (resultado["DESVIACION"] != DBNull.Value) entidad.desviacion = Convert.ToDecimal(resultado["DESVIACION"]);
                            if (resultado["NOMMODALIDADINT"] != DBNull.Value) entidad.nommodalidadint = Convert.ToString(resultado["NOMMODALIDADINT"]);
                            if (resultado["NOMPERIODICIDAD"] != DBNull.Value) entidad.nomperiodicidad = Convert.ToString(resultado["NOMPERIODICIDAD"]);
                            if (resultado["NOMCAPITALIZA"] != DBNull.Value) entidad.nomcapitaliza = Convert.ToString(resultado["NOMCAPITALIZA"]);
                            if (resultado["NOMRETENCION"] != DBNull.Value) entidad.nomretencion = Convert.ToString(resultado["NOMRETENCION"]);
                            if (resultado["NOMDESMATE"] != DBNull.Value) entidad.nomdesmate = Convert.ToString(resultado["NOMDESMATE"]);
                            if (resultado["NOMUSUARIO"] != DBNull.Value) entidad.nomusuario = Convert.ToString(resultado["NOMUSUARIO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            lstCdat.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCdat;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AdministracionCDATData", "getListarCdatBloquear", ex);
                        return null;
                    }
                }
            }
        }


        public List<AdministracionCDAT> guardargrilla(AdministracionCDAT vprovision, List<AdministracionCDAT> lstLista, Usuario vUsuario)
        {
            List<AdministracionCDAT> lstguardar = new List<AdministracionCDAT>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Xpinn.Tesoreria.Entities.Operacion vOpe = new Xpinn.Tesoreria.Entities.Operacion();
                        vOpe.cod_ope = 0;

                        DbParameter pidprovision = cmdTransaccionFactory.CreateParameter();
                        pidprovision.ParameterName = "p_idprovision";
                        pidprovision.Value = vprovision.cod_persona;
                        pidprovision.Direction = ParameterDirection.Input;
                        pidprovision.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidprovision);

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "p_fecha";
                        if (vprovision.fecha_apertura == null)
                            pfecha.Value = DBNull.Value;
                        else
                            pfecha.Value = vprovision.fecha_apertura;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        DbParameter pcod_ope = cmdTransaccionFactory.CreateParameter();
                        pcod_ope.ParameterName = "p_cod_ope";
                        pcod_ope.Value = vOpe.cod_ope;
                        pcod_ope.Direction = ParameterDirection.Input;
                        pcod_ope.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_ope);

                        DbParameter pcodigo_cdat = cmdTransaccionFactory.CreateParameter();
                        pcodigo_cdat.ParameterName = "p_codigo_cdat";
                        pcodigo_cdat.Value = vprovision.codigo_cdat;
                        pcodigo_cdat.Direction = ParameterDirection.Input;
                        pcodigo_cdat.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigo_cdat);

                        DbParameter ptasa_interes = cmdTransaccionFactory.CreateParameter();
                        ptasa_interes.ParameterName = "p_tasa_interes";
                        if (vprovision.tasa_interes == null)
                            ptasa_interes.Value = DBNull.Value;
                        else
                            ptasa_interes.Value = vprovision.tasa_interes;
                        ptasa_interes.Direction = ParameterDirection.Input;
                        ptasa_interes.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ptasa_interes);

                        DbParameter pfecha_interes = cmdTransaccionFactory.CreateParameter();
                        pfecha_interes.ParameterName = "p_fecha_interes";
                        if (vprovision.fecha_intereses == null)
                            pfecha_interes.Value = DBNull.Value;
                        else
                            pfecha_interes.Value = vprovision.fecha_intereses;
                        pfecha_interes.Direction = ParameterDirection.Input;
                        pfecha_interes.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_interes);

                        DbParameter pdias = cmdTransaccionFactory.CreateParameter();
                        pdias.ParameterName = "p_dias";
                        if (vprovision.dias == null)
                            pdias.Value = DBNull.Value;
                        else
                            pdias.Value = vprovision.dias;
                        pdias.Direction = ParameterDirection.Input;
                        pdias.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pdias);

                        DbParameter pinteres_causado = cmdTransaccionFactory.CreateParameter();
                        pinteres_causado.ParameterName = "p_interes_causado";
                        if (vprovision.intereses_cap == null)
                            pinteres_causado.Value = DBNull.Value;
                        else
                            pinteres_causado.Value = vprovision.intereses_cap;
                        pinteres_causado.Direction = ParameterDirection.Input;
                        pinteres_causado.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pinteres_causado);

                        DbParameter pretencion = cmdTransaccionFactory.CreateParameter();
                        pretencion.ParameterName = "p_retencion";
                        if (vprovision.retencion == null)
                            pretencion.Value = DBNull.Value;
                        else
                            pretencion.Value = vprovision.retencion;
                        pretencion.Direction = ParameterDirection.Input;
                        pretencion.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pretencion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CDA_PROVISION__CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstguardar;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AdministracionCDATData", "CrearAdministracionCDAT", ex);
                        return null;
                    }
                }
            }
        }

        //Anderson Acuña -- Reporte CDAT       
        public List<Cdat> ListarCDAT(Cdat pCdta, Usuario pUsuario, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Cdat> lstCdat = new List<Cdat>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT DISTINCT T4.NOMBRE, T1.numero_cdat, T3.identificacion,
                                T3.primer_apellido || T3.segundo_apellido || T3.primer_nombre || T3.segundo_nombre as nombres,
                                T1.fecha_apertura, T1.fecha_vencimiento, T1.tasa_interes, T1.plazo, T1.valor, nvl(T6.INTERESES, 0) as INTERESES,
                                T1.retencion_cap, T5.INT_CAUSADOS, T1.intereses_cap, T5.VALOR_ACUMULADO, T1.FECHA_INTERESES
                                FROM CDAT T1
                                INNER JOIN cdat_titular T2 ON T1.codigo_cdat = T2.codigo_cdat
                                LEFT JOIN persona T3 ON T2.cod_persona = T3.cod_persona 
                                LEFT JOIN OFICINA T4 ON T1.COD_OFICINA = t4.COD_OFICINA 
                                LEFT JOIN CAUSACION_CDAT T5 ON T1.CODIGO_CDAT = T5.CODIGO_CDAT 
                                LEFT JOIN LIQUIDACION_CDAT T6 ON T1.CODIGO_CDAT = T6.CODIGO_CDAT 
                                 " + filtro+ " And (T5.FECHA_CAUSACION In (select max(FECHA_CAUSACION) from CAUSACION_CDAT sT5 where T1.numero_cdat = sT5.numero_cdat) Or T5.FECHA_CAUSACION Is Null) ORDER BY T1.fecha_apertura DESC";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Cdat entidad = new Cdat();

                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["numero_cdat"] != DBNull.Value) entidad.numero_cdat = Convert.ToString(resultado["numero_cdat"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["nombres"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["nombres"]);
                            if (resultado["fecha_apertura"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["fecha_apertura"]);
                            if (resultado["fecha_vencimiento"] != DBNull.Value) entidad.fecha_vencimiento = Convert.ToDateTime(resultado["fecha_vencimiento"]);
                            if (resultado["tasa_interes"] != DBNull.Value) entidad.tasa_interes = Convert.ToDecimal(resultado["tasa_interes"]);
                            if (resultado["plazo"] != DBNull.Value) entidad.plazo = Convert.ToInt32(resultado["plazo"]);
                            if (resultado["valor"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["valor"]);
                            if (resultado["INTERESES"] != DBNull.Value) entidad.intereses = Convert.ToDecimal(resultado["INTERESES"]);
                            if (resultado["retencion_cap"] != DBNull.Value) entidad.retencion = Convert.ToString(resultado["retencion_cap"]);
                            if (resultado["INT_CAUSADOS"] != DBNull.Value) entidad.intereses_cau = Convert.ToDecimal(resultado["INT_CAUSADOS"]);
                            if (resultado["VALOR_ACUMULADO"] != DBNull.Value) entidad.valor_acumulado = Convert.ToDecimal(resultado["VALOR_ACUMULADO"]);
                            if (resultado["FECHA_INTERESES"] != DBNull.Value) entidad.fecha_intereses = Convert.ToDateTime(resultado["FECHA_INTERESES"]);


                            lstCdat.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCdat;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ListarCredito", ex);
                        return null;
                    }
                }
            }
        }

        public AdministracionCDAT getcdatById(Int64 Codigo, Usuario pUsuario)
        {
            DbDataReader resultado;
            AdministracionCDAT entidad = new AdministracionCDAT();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Cdat WHERE CODIGO_CDAT = " + Codigo;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["CODIGO_CDAT"] != DBNull.Value) entidad.codigo_cdat = Convert.ToInt64(resultado["CODIGO_CDAT"]);
                            if (resultado["NUMERO_CDAT"] != DBNull.Value) entidad.numero_cdat = Convert.ToString(resultado["NUMERO_CDAT"]);
                            if (resultado["NUMERO_FISICO"] != DBNull.Value) entidad.numero_fisico = Convert.ToString(resultado["NUMERO_FISICO"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["COD_OFICINA"]);
                            if (resultado["COD_LINEACDAT"] != DBNull.Value) entidad.cod_lineacdat = Convert.ToString(resultado["COD_LINEACDAT"]);
                            if (resultado["COD_DESTINACION"] != DBNull.Value) entidad.cod_destinacion = Convert.ToInt64(resultado["COD_DESTINACION"]);
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                            if (resultado["MODALIDAD"] != DBNull.Value) entidad.modalidad = Convert.ToString(resultado["MODALIDAD"]);
                            if (resultado["CODFORMA_CAPTACION"] != DBNull.Value) entidad.codforma_captacion = Convert.ToInt32(resultado["CODFORMA_CAPTACION"]);
                            if (resultado["PLAZO"] != DBNull.Value) entidad.plazo = Convert.ToInt32(resultado["PLAZO"]);
                            if (resultado["TIPO_CALENDARIO"] != DBNull.Value) entidad.tipo_calendario = Convert.ToInt32(resultado["TIPO_CALENDARIO"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["COD_MONEDA"] != DBNull.Value) entidad.cod_moneda = Convert.ToInt32(resultado["COD_MONEDA"]);
                            if (resultado["FECHA_INICIO"] != DBNull.Value) entidad.fecha_inicio = Convert.ToDateTime(resultado["FECHA_INICIO"]);
                            if (resultado["FECHA_VENCIMIENTO"] != DBNull.Value) entidad.fecha_vencimiento = Convert.ToDateTime(resultado["FECHA_VENCIMIENTO"]);
                            if (resultado["COD_ASESOR_COM"] != DBNull.Value) entidad.cod_asesor_com = Convert.ToInt32(resultado["COD_ASESOR_COM"]);
                            if (resultado["TIPO_INTERES"] != DBNull.Value) entidad.tipo_interes = Convert.ToString(resultado["TIPO_INTERES"]);
                            if (resultado["TASA_INTERES"] != DBNull.Value) entidad.tasa_interes = Convert.ToDecimal(resultado["TASA_INTERES"]);
                            if (resultado["COD_TIPO_TASA"] != DBNull.Value) entidad.cod_tipo_tasa = Convert.ToInt32(resultado["COD_TIPO_TASA"]);
                            if (resultado["TIPO_HISTORICO"] != DBNull.Value) entidad.tipo_historico = Convert.ToInt32(resultado["TIPO_HISTORICO"]);
                            if (resultado["DESVIACION"] != DBNull.Value) entidad.desviacion = Convert.ToDecimal(resultado["DESVIACION"]);
                            if (resultado["COD_PERIODICIDAD_INT"] != DBNull.Value) entidad.cod_periodicidad_int = Convert.ToInt32(resultado["COD_PERIODICIDAD_INT"]);
                            if (resultado["MODALIDAD_INT"] != DBNull.Value) entidad.modalidad_int = Convert.ToInt32(resultado["MODALIDAD_INT"]);
                            if (resultado["CAPITALIZAR_INT"] != DBNull.Value) entidad.capitalizar_int = Convert.ToInt32(resultado["CAPITALIZAR_INT"]);
                            if (resultado["COBRA_RETENCION"] != DBNull.Value) entidad.cobra_retencion = Convert.ToInt32(resultado["COBRA_RETENCION"]);
                            if (resultado["TASA_NOMINAL"] != DBNull.Value) entidad.tasa_nominal = Convert.ToDecimal(resultado["TASA_NOMINAL"]);
                            if (resultado["TASA_EFECTIVA"] != DBNull.Value) entidad.tasa_efectiva = Convert.ToDecimal(resultado["TASA_EFECTIVA"]);
                            if (resultado["INTERESES_CAP"] != DBNull.Value) entidad.intereses_cap = Convert.ToDecimal(resultado["INTERESES_CAP"]);
                            if (resultado["RETENCION_CAP"] != DBNull.Value) entidad.retencion_cap = Convert.ToDecimal(resultado["RETENCION_CAP"]);
                            if (resultado["FECHA_INTERESES"] != DBNull.Value) entidad.fecha_intereses = Convert.ToDateTime(resultado["FECHA_INTERESES"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["DESMATERIALIZADO"] != DBNull.Value) entidad.desmaterializado = Convert.ToInt32(resultado["DESMATERIALIZADO"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ApertuData", "ConsultarApertu", ex);
                        return null;
                    }
                }
            }
        }

        public int updateCDAT(Int32 pcodigo, Usuario pUsaurio)
        {
            Int32 resultado = 0;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsaurio))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodigo_cdat = cmdTransaccionFactory.CreateParameter();
                        pcodigo_cdat.ParameterName = "pcodigo_cdat";
                        pcodigo_cdat.Value = pcodigo;
                        pcodigo_cdat.Direction = ParameterDirection.Input;
                        //   pcodigo_cdat.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigo_cdat);

                        DbParameter pRespuesta = cmdTransaccionFactory.CreateParameter();
                        pRespuesta.ParameterName = "pRespuesta";
                        pRespuesta.Value = 0;
                        pRespuesta.Direction = ParameterDirection.Output;
                        // pRespuesta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pRespuesta);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_ADM_MODIFI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pRespuesta.Value != null)
                            resultado = Convert.ToInt32(pRespuesta.Value);

                        dbConnectionFactory.CerrarConexion(connection);
                        return resultado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AdministracionCDATData", "updateCDAT", ex);
                        return resultado;
                    }
                }
            }
        }


        public void insertNovedadCDAT(NovedadCDAT entidad, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_codigo_cdat = cmdTransaccionFactory.CreateParameter();
                        p_codigo_cdat.ParameterName = "p_codigo_cdat";
                        p_codigo_cdat.Value = entidad.CODIGO_CDAT;
                        p_codigo_cdat.Direction = ParameterDirection.Input;
                        p_codigo_cdat.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_codigo_cdat);

                        DbParameter p_tipo_novedad = cmdTransaccionFactory.CreateParameter();
                        p_tipo_novedad.ParameterName = "p_tipo_novedad";
                        p_tipo_novedad.Value = entidad.TIPO_NOVEDAD;
                        p_tipo_novedad.Direction = ParameterDirection.Input;
                        p_tipo_novedad.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_tipo_novedad);

                        DbParameter p_fecha_novedad = cmdTransaccionFactory.CreateParameter();
                        p_fecha_novedad.ParameterName = "p_fecha_novedad";
                        p_fecha_novedad.Value = entidad.FECHA_NOVEDAD;
                        p_fecha_novedad.Direction = ParameterDirection.Input;
                        p_fecha_novedad.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(p_fecha_novedad);

                        DbParameter p_observaciones = cmdTransaccionFactory.CreateParameter();
                        p_observaciones.ParameterName = "p_observaciones";
                        p_observaciones.Value = entidad.OBSERVACIONES;
                        p_observaciones.Direction = ParameterDirection.Input;
                        p_observaciones.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_observaciones);


                        DbParameter p_cod_usuario = cmdTransaccionFactory.CreateParameter();
                        p_cod_usuario.ParameterName = "p_cod_usuario";
                        p_cod_usuario.Value = entidad.COD_USUARIO;
                        p_cod_usuario.Direction = ParameterDirection.Input;
                        p_cod_usuario.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_usuario);

                        DbParameter p_fechacrea = cmdTransaccionFactory.CreateParameter();
                        p_fechacrea.ParameterName = "p_fechacrea";
                        p_fechacrea.Value = entidad.FECHACREA;
                        p_fechacrea.Direction = ParameterDirection.Input;
                        p_fechacrea.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(p_fechacrea);


                        DbParameter pestadoactual = cmdTransaccionFactory.CreateParameter();
                        pestadoactual.ParameterName = "P_ESTADOACTUAL";
                        pestadoactual.Value = entidad.EstadoActual;
                        pestadoactual.Direction = ParameterDirection.Input;
                        pestadoactual.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pestadoactual);


                        DbParameter pEstadoSiguiente = cmdTransaccionFactory.CreateParameter();
                        pEstadoSiguiente.ParameterName = "P_ESTADOSIGUIENTE";
                        pEstadoSiguiente.Value = entidad.EstadoSiguiente;
                        pEstadoSiguiente.Direction = ParameterDirection.Input;
                        pEstadoSiguiente.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pEstadoSiguiente);



                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CDA_NOVEDAD_CD_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AdministracionCDATData", "insertNovedadCDAT", ex);
                    }
                }
            }
        }

        public List<AdministracionCDAT> ListarIntereses(DateTime FechaApe, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<AdministracionCDAT> lstCDAT = new List<AdministracionCDAT>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter fecha_provision = cmdTransaccionFactory.CreateParameter();
                        fecha_provision.ParameterName = "P_FECHA_PROVISION";
                        fecha_provision.Value = FechaApe;
                        fecha_provision.Direction = ParameterDirection.Input;
                        fecha_provision.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(fecha_provision);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CDA_PROVISION";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        cmdTransaccionFactory.Dispose();
                        cmdTransaccionFactory.Parameters.Clear();
                        string sql = "Select * From TEMP_LIQUIDACION_CDAT";
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            AdministracionCDAT entidad = new AdministracionCDAT();
                            if (resultado["FECHA_LIQUIDACION"] != DBNull.Value) entidad.fecha_provision = Convert.ToDateTime(resultado["FECHA_LIQUIDACION"]);
                            if (resultado["CODIGO_CDAT"] != DBNull.Value) entidad.codigo_cdat = Convert.ToInt64(resultado["CODIGO_CDAT"]);
                            if (resultado["NUMERO_CDAT"] != DBNull.Value) entidad.numero_cdat = Convert.ToString(resultado["NUMERO_CDAT"]);
                            if (resultado["FECHA_INICIAL"] != DBNull.Value) entidad.fecha_inicio = Convert.ToDateTime(resultado["FECHA_INICIAL"]);
                            if (resultado["FECHA_FINAL"] != DBNull.Value) entidad.fecha_vencimiento = Convert.ToDateTime(resultado["FECHA_FINAL"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.nomperiodicidad = Convert.ToString(resultado["PERIODICIDAD"]);
                            if (resultado["TASA"] != DBNull.Value) entidad.tasa_efectiva = Convert.ToDecimal(resultado["TASA"]);
                            if (resultado["FECHA_INT"] != DBNull.Value) entidad.fecha_intereses = Convert.ToDateTime(resultado["FECHA_INT"]);
                            if (resultado["INTERES"] != DBNull.Value) entidad.intereses_cap = Convert.ToDecimal(resultado["INTERES"]);
                            if (resultado["RETENCION"] != DBNull.Value) entidad.retencion = Convert.ToDecimal(resultado["RETENCION"]);
                            if (resultado["DIAS_LIQUIDA"] != DBNull.Value) entidad.dias = Convert.ToInt32(resultado["DIAS_LIQUIDA"]);
                            if (resultado["INTERES_CAUSADO"] != DBNull.Value) entidad.capitalizar_int = Convert.ToInt32(resultado["INTERES_CAUSADO"]);
                            if (resultado["RETENCION_CAUSADO"] != DBNull.Value) entidad.retencion_cap = Convert.ToDecimal(resultado["RETENCION_CAUSADO"]);

                            lstCDAT.Add(entidad);
                        }


                        return lstCDAT;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionCDATData", "ListarIntereses", ex);
                        return null;
                    }
                }
            }
        }

        public List<AdministracionCDAT> guardarCausacion(AdministracionCDAT vprovision, List<AdministracionCDAT> lstLista, Usuario vUsuario)
        {
            List<AdministracionCDAT> lstguardar = new List<AdministracionCDAT>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Xpinn.Tesoreria.Entities.Operacion vOpe = new Xpinn.Tesoreria.Entities.Operacion();
                        vOpe.cod_ope = 0;

                        //DbParameter pidprovision = cmdTransaccionFactory.CreateParameter();
                        //pidprovision.ParameterName = "p_idcausacion";
                        //pidprovision.Value = vprovision.cod_persona;
                        //pidprovision.Direction = ParameterDirection.Input;
                        //pidprovision.DbType = DbType.Int64;
                        //cmdTransaccionFactory.Parameters.Add(pidprovision);

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "p_fecha_causacion";
                        if (vprovision.fecha_intereses == null)
                            pfecha.Value = DBNull.Value;
                        else
                            pfecha.Value = vprovision.fecha_intereses;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        DbParameter pcod_ope = cmdTransaccionFactory.CreateParameter();
                        pcod_ope.ParameterName = "p_cod_ope";
                        pcod_ope.Value = vprovision.cod_ope;
                        pcod_ope.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcod_ope);

                        DbParameter pcodigo_cdat = cmdTransaccionFactory.CreateParameter();
                        pcodigo_cdat.ParameterName = "p_codigo_cdat";
                        pcodigo_cdat.Value = vprovision.codigo_cdat;
                        pcodigo_cdat.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcodigo_cdat);

                        DbParameter p_numero_cdat = cmdTransaccionFactory.CreateParameter();
                        p_numero_cdat.ParameterName = "p_numero_cdat";
                        p_numero_cdat.Value = vprovision.numero_cdat;
                        p_numero_cdat.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_numero_cdat);

                        DbParameter p_valor_cdat = cmdTransaccionFactory.CreateParameter();
                        p_valor_cdat.ParameterName = "p_valor_cdat";
                        p_valor_cdat.Value = vprovision.valor;
                        p_valor_cdat.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_valor_cdat);

                        DbParameter ptasa_interes = cmdTransaccionFactory.CreateParameter();
                        ptasa_interes.ParameterName = "p_tasa";
                        if (vprovision.tasa_interes == decimal.MinValue)
                            ptasa_interes.Value = DBNull.Value;
                        else
                            ptasa_interes.Value = vprovision.tasa_interes;
                        ptasa_interes.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(ptasa_interes);

                        DbParameter pdias = cmdTransaccionFactory.CreateParameter();
                        pdias.ParameterName = "p_dias_causados";
                        if (vprovision.dias == int.MinValue)
                            pdias.Value = DBNull.Value;
                        else
                            pdias.Value = vprovision.dias;
                        pdias.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pdias);

                        DbParameter pinteres_causado = cmdTransaccionFactory.CreateParameter();
                        pinteres_causado.ParameterName = "p_int_causados";
                        if (vprovision.intereses_cap == decimal.MinValue)
                            pinteres_causado.Value = DBNull.Value;
                        else
                            pinteres_causado.Value = vprovision.intereses_cap;
                        pinteres_causado.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pinteres_causado);

                        DbParameter pretencion = cmdTransaccionFactory.CreateParameter();
                        pretencion.ParameterName = "p_retencion";
                        if (vprovision.retencion == decimal.MinValue)
                            pretencion.Value = DBNull.Value;
                        else
                            pretencion.Value = vprovision.retencion;
                        pretencion.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pretencion);

                        DbParameter p_valor_acumulado = cmdTransaccionFactory.CreateParameter();
                        p_valor_acumulado.ParameterName = "p_valor_acumulado";
                        if (vprovision.capitalizar_int == int.MinValue)
                            p_valor_acumulado.Value = DBNull.Value;
                        else
                            p_valor_acumulado.Value = vprovision.capitalizar_int;
                        p_valor_acumulado.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_valor_acumulado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CDA_CAUSCDAT__CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstguardar;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AdministracionCDATData", "CrearAdministracionCDAT", ex);
                        return null;
                    }
                }
            }
        }

        //Anderson Acuña -- Reporte 1020       
        public List<Cdat> Reporte_1020(Cdat pCdta, Usuario pUsuario, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Cdat> lstCdat = new List<Cdat>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select 
                                    TI.DESCRIPCION as tipo_identificacion,
                                    P.IDENTIFICACION,
                                    case when P.TIPO_IDENTIFICACION = 2 then P.DIGITO_VERIFICACION  else P.DIGITO_VERIFICACION end as DV,
                                    P.PRIMER_APELLIDO,
                                    P.SEGUNDO_APELLIDO,
                                    P.PRIMER_NOMBRE,
                                    P.SEGUNDO_NOMBRE,
                                    case when P.TIPO_PERSONA = 'J' then P.RAZON_SOCIAL else '' end as RAZONSOCIAL,
                                    P.DIRECCION,
                                    CD.DEPENDE_DE as codDep,
                                    P.CODCIUDADRESIDENCIA as codMcp,
                                    'Colombia' as Pais,
                                    C.NUMERO_CDAT,
                                    3 as TipoTitulo, --1 Certificado Depósitode Mercancias, 2 Bono de Prenda, 3 Certificado de Depósito de Ahorro a Término(CDAT), 
                                                        --4 Certificado de Depósito a Término(CDT), 5 Bono Ordinario, 6 Bono subordinario, 7 Otros 
                                    4 as TipoMovimiento, --1 Emisión, 2 Renovación, 3 Cancelación, 4 Vigente
                                    C.VALOR as saldoininial,
                                    C.VALOR as ValorInversionEfec,
                                    sum(CCD.INT_CAUSADOS) as IntCausados,
                                    sum(CCD.VALOR_ACUMULADO) as IntPagados,         --revisar
                                    C.VALOR as SaldoFinal                           -- revisar
                                    from CDAT C
                                    inner join CDAT_TITULAR CDT ON C.CODIGO_CDAT = CDT.CODIGO_CDAT
                                    inner join persona P on CDT.COD_PERSONA = P.COD_PERSONA
                                    inner join TIPOIDENTIFICACION TI on P.TIPO_IDENTIFICACION = TI.CODTIPOIDENTIFICACION
                                    left join Ciudades CD on p.CODCIUDADRESIDENCIA = CD.CODCIUDAD 
                                    left JOIN CAUSACION_CDAT CCD ON C.CODIGO_CDAT = CCD.CODIGO_CDAT 
                                    where CDT.PRINCIPAL = 1                                    
                                    and FECHA_INICIO BETWEEN " + filtro +
                                    @"group by 
                                    P.DIGITO_VERIFICACION,
                                    TI.DESCRIPCION,
                                    P.IDENTIFICACION,
                                    P.TIPO_IDENTIFICACION,
                                    P.DIGITO_VERIFICACION,
                                    P.PRIMER_APELLIDO,
                                    P.SEGUNDO_APELLIDO,
                                    P.PRIMER_NOMBRE,
                                    P.SEGUNDO_NOMBRE,
                                    P.TIPO_PERSONA,
                                    P.RAZON_SOCIAL,
                                    P.DIRECCION,
                                    CD.DEPENDE_DE,
                                    P.CODCIUDADRESIDENCIA,
                                    C.NUMERO_CDAT,
                                    C.VALOR
                                    order by C.NUMERO_CDAT";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Cdat entidad = new Cdat();
                            

                            if (resultado["tipo_identificacion"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["tipo_identificacion"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["DV"] != DBNull.Value) entidad.Digito_Verificacion = Convert.ToInt32(resultado["DV"]);
                            if (resultado["PRIMER_APELLIDO"] != DBNull.Value) entidad.Primer_Apellido = Convert.ToString(resultado["PRIMER_APELLIDO"]);
                            if (resultado["SEGUNDO_APELLIDO"] != DBNull.Value) entidad.Segundo_Apellido = Convert.ToString(resultado["SEGUNDO_APELLIDO"]);
                            if (resultado["PRIMER_NOMBRE"] != DBNull.Value) entidad.Primer_Nombre = Convert.ToString(resultado["PRIMER_NOMBRE"]);
                            if (resultado["SEGUNDO_NOMBRE"] != DBNull.Value) entidad.Segundo_Nombre = Convert.ToString(resultado["SEGUNDO_NOMBRE"]);
                            if (resultado["RAZONSOCIAL"] != DBNull.Value) entidad.Razon_Social = Convert.ToString(resultado["RAZONSOCIAL"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.Direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["codDep"] != DBNull.Value) entidad.Cod_Dep = Convert.ToInt32(resultado["codDep"]);
                            if (resultado["codMcp"] != DBNull.Value) entidad.Cod_Mun_Ciud = Convert.ToInt32(resultado["codMcp"]);
                            if (resultado["Pais"] != DBNull.Value) entidad.Pais = Convert.ToString(resultado["Pais"]);
                            if (resultado["NUMERO_CDAT"] != DBNull.Value) entidad.numero_cdat = Convert.ToString(resultado["NUMERO_CDAT"]);
                            if (resultado["TipoTitulo"] != DBNull.Value) entidad.TipoTitulo = Convert.ToInt32(resultado["TipoTitulo"]);
                            if (resultado["TipoMovimiento"] != DBNull.Value) entidad.TipoMovimiento = Convert.ToInt32(resultado["TipoMovimiento"]);
                            if (resultado["saldoininial"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["saldoininial"]);
                            if (resultado["ValorInversionEfec"] != DBNull.Value) entidad.valorInversion = Convert.ToDecimal(resultado["ValorInversionEfec"]);
                            if (resultado["IntCausados"] != DBNull.Value) entidad.intereses_cau = Convert.ToDecimal(resultado["IntCausados"]);
                            if (resultado["IntPagados"] != DBNull.Value) entidad.intereses = Convert.ToDecimal(resultado["IntPagados"]);
                            if (resultado["SaldoFinal"] != DBNull.Value) entidad.valor_acumulado = Convert.ToDecimal(resultado["SaldoFinal"]);
                            
                            lstCdat.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCdat;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ListarCredito", ex);
                        return null;
                    }
                }
            }
        }



    }
}
