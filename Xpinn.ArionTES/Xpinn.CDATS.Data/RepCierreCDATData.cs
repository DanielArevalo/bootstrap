using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using Xpinn.CDATS.Entities;
using System.Data.Common;
using System.Data;

namespace Xpinn.CDATS.Data
{
    public class RepCierreCDATData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public RepCierreCDATData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public List<AdministracionCDAT> ListarCDATReporteCierre(DateTime pFecha, Usuario vUsuario)
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
                        string sql = @"SELECT h.cod_oficina,p.cod_persona,p.Identificacion,p.apellidos,p.nombres,h.numero_cdat,h.codigo_cdat,a.numero_fisico,l.descripcion as nom_linea_cdat,"
                                    + "a.fecha_apertura,h.fecha_inicio,h.plazo,a.valor,h.valor as saldo_total,h.fecha_vencimiento,d.descripcion periodicidad,a.tasa_interes,a.fecha_intereses,"
                                    + "h.interes_causado,h.estado "
                                    + "from historico_cdat h inner join cdat a on h.numero_cdat=a.numero_cdat "
                                    + "Left join cdat_titular t on h.codigo_cdat=t.codigo_cdat "
                                    + "Left join v_persona p on t.cod_persona = p.cod_persona "
                                    + "Left join lineacdat l on h.cod_lineacdat=l.cod_lineacdat "
                                    + "Left join periodicidad d on h.cod_periodicidad_int = d.cod_periodicidad where t.principal=1 and h.estado=2";
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            sql = sql + " and h.fecha_historico = to_date('" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "','" + conf.ObtenerFormatoFecha() + "')";
                        else
                            sql = sql + " and h.fecha_historico = '" + pFecha + "'";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            AdministracionCDAT entidad = new AdministracionCDAT();
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["COD_OFICINA"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["APELLIDOS"] != DBNull.Value) entidad.apellidos = Convert.ToString(resultado["APELLIDOS"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["NUMERO_CDAT"] != DBNull.Value) entidad.numero_cdat = Convert.ToString(resultado["NUMERO_CDAT"]);
                            if (resultado["CODIGO_CDAT"] != DBNull.Value) entidad.codigo_cdat = Convert.ToInt64(resultado["CODIGO_CDAT"]);
                            if (resultado["NUMERO_FISICO"] != DBNull.Value) entidad.numero_fisico = Convert.ToString(resultado["NUMERO_FISICO"]);
                            if (resultado["NOM_LINEA_CDAT"] != DBNull.Value) entidad.nom_linea_cdat = Convert.ToString(resultado["NOM_LINEA_CDAT"]);
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                            if (resultado["FECHA_INICIO"] != DBNull.Value) entidad.fecha_inicio = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                            if (resultado["PLAZO"] != DBNull.Value) entidad.plazo = Convert.ToInt32(resultado["PLAZO"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["SALDO_TOTAL"] != DBNull.Value) entidad.saldo = Convert.ToDecimal(resultado["SALDO_TOTAL"]);
                            if (resultado["FECHA_VENCIMIENTO"] != DBNull.Value) entidad.fecha_vencimiento = Convert.ToDateTime(resultado["FECHA_VENCIMIENTO"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.nom_periodicidad = Convert.ToString(resultado["PERIODICIDAD"]);
                            if (resultado["TASA_INTERES"] != DBNull.Value) entidad.tasa_interes = Convert.ToDecimal(resultado["TASA_INTERES"]);
                            if (resultado["FECHA_INTERESES"] != DBNull.Value) entidad.fecha_intereses = Convert.ToDateTime(resultado["FECHA_INTERESES"]);
                            if (resultado["INTERES_CAUSADO"] != DBNull.Value) entidad.intereses = Convert.ToDecimal(resultado["INTERES_CAUSADO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            lstCdat.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCdat;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RepCierreCDATData", "ListarCDATReporteCierre", ex);
                        return null;
                    }
                }
            }
        }
    }
}
