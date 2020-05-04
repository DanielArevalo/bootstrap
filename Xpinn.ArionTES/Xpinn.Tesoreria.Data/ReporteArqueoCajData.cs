using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using System.Data;
using System.Data.Common;
using Xpinn.Tesoreria.Entities;

namespace Xpinn.Tesoreria.Data
{
    public class ReporteArqueoCajData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public ReporteArqueoCajData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }       
        //Lista para cargar el efectivo
        public List<ArqueoCajaMenor> lstDetalleEfectivo(Int64 pid_arqueo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ArqueoCajaMenor> lstEfectivo = new List<ArqueoCajaMenor>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string sql = @"select CASE a.TIPO_EFECTIVO WHEN 'B' THEN 'Billete' WHEN 'M' THEN 'Moneda' ELSE a.TIPO_EFECTIVO END AS Tipo,
                                 a.DENOMINACION, a.CANTIDAD, a.TOTAL
                                 from ARQUEO_CAJA_MENOR_DET a
                                 join ARQUEO_CAJA_MENOR ar on a.ID_ARQUEO = ar.ID_ARQUEO
                                 join AREAS_CAJ ac on ac.IDAREA = ar.IDAREA
                                 where ac.COD_USUARIO = " + pUsuario.codusuario + " and ar.ID_ARQUEO = " + pid_arqueo;
                    connection.Open();
                    cmdTransaccionFactory.Connection = connection;
                    cmdTransaccionFactory.CommandType = CommandType.Text;
                    cmdTransaccionFactory.CommandText = sql;
                    resultado = cmdTransaccionFactory.ExecuteReader();

                    while (resultado.Read())
                    {
                        ArqueoCajaMenor entidad = new ArqueoCajaMenor();
                        if (resultado["TIPO"] != DBNull.Value) entidad.tipo_efectivo = (resultado["TIPO"]).ToString();
                        if (resultado["DENOMINACION"] != DBNull.Value) entidad.denominacion = Convert.ToInt64(resultado["DENOMINACION"]);
                        if (resultado["CANTIDAD"] != DBNull.Value) entidad.cantidad = Convert.ToInt64(resultado["CANTIDAD"]);
                        if (resultado["TOTAL"] != DBNull.Value) entidad.valor = Convert.ToInt64(resultado["TOTAL"]);
                        lstEfectivo.Add(entidad);
                    }
                    return lstEfectivo;

                }
            }
        }

       
        public List<ArqueoCajaMenor> DocLegalizados(Int64 id_arqueo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ArqueoCajaMenor> lstLegalizados  = new List<ArqueoCajaMenor>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string sql = @"select s.IDSOPORTE, s.FECHA, s.DESCRIPCION, p.PRIMER_NOMBRE || ' ' || p.SEGUNDO_NOMBRE || ' ' || p.PRIMER_APELLIDO || ' ' || p.SEGUNDO_APELLIDO AS Beneficiario, s.VALOR
                                from SOPORTE_CAJ s
                                join ARQUEO_CAJA_MENOR ar on ar.ID_ARQUEO = s.ID_ARQUEO
                                join PERSONA p on p.COD_PERSONA = s.COD_PER
                                where s.VALE_PROV IN (0,2) and ar.ID_ARQUEO = " + id_arqueo; 
                    connection.Open();
                    cmdTransaccionFactory.Connection = connection;
                    cmdTransaccionFactory.CommandType = CommandType.Text;
                    cmdTransaccionFactory.CommandText = sql;
                    resultado = cmdTransaccionFactory.ExecuteReader();

                    while (resultado.Read())
                    {
                        ArqueoCajaMenor entidad = new ArqueoCajaMenor();
                        if (resultado["IDSOPORTE"] != DBNull.Value) entidad.id_soporte = Convert.ToInt64(resultado["IDSOPORTE"]);
                        if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                        if (resultado["BENEFICIARIO"] != DBNull.Value) entidad.persona = (resultado["BENEFICIARIO"]).ToString();
                        if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToInt64(resultado["VALOR"]);
                        lstLegalizados.Add(entidad);                        
                    }
                    return lstLegalizados;
                }
            }
        }
        //Documentos no legalizados
        public List<ArqueoCajaMenor> DocNoLegalizados(Int64 id_arqueo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ArqueoCajaMenor> lstNoLegalizados = new List<ArqueoCajaMenor>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string sql = @"select s.IDSOPORTE, s.FECHA, s.DESCRIPCION, p.PRIMER_NOMBRE || ' ' || p.SEGUNDO_NOMBRE || ' ' || p.PRIMER_APELLIDO || ' ' || p.SEGUNDO_APELLIDO AS Beneficiario, s.VALOR
                                from SOPORTE_CAJ s
                                join ARQUEO_CAJA_MENOR ar on ar.ID_ARQUEO = s.ID_ARQUEO
                                join PERSONA p on p.COD_PERSONA = s.COD_PER
                                where s.VALE_PROV IN (1) and ar.ID_ARQUEO = " + id_arqueo;
                    connection.Open();
                    cmdTransaccionFactory.Connection = connection;
                    cmdTransaccionFactory.CommandType = CommandType.Text;
                    cmdTransaccionFactory.CommandText = sql;
                    resultado = cmdTransaccionFactory.ExecuteReader();

                    while (resultado.Read())
                    {
                        ArqueoCajaMenor entidad = new ArqueoCajaMenor();
                        if (resultado["IDSOPORTE"] != DBNull.Value) entidad.id_soporte = Convert.ToInt64(resultado["IDSOPORTE"]);
                        if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                        if (resultado["BENEFICIARIO"] != DBNull.Value) entidad.persona = (resultado["BENEFICIARIO"]).ToString();
                        if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToInt64(resultado["VALOR"]);
                        lstNoLegalizados.Add(entidad);
                    }
                    return lstNoLegalizados;
                }
            }
        }
        public ArqueoCajaMenor ResumenArqueo(Int64 id_arqueo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            ArqueoCajaMenor resumen = new ArqueoCajaMenor();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string sql = @"with resumen as
                                (
                                select ESNULO((select sum(s.VALOR) from SOPORTE_CAJ s  join ARQUEO_CAJA_MENOR ar on ar.ID_ARQUEO = s.ID_ARQUEO WHERE s.VALE_PROV IN (0,2) and s.ID_ARQUEO = arq.ID_ARQUEO),0) as TotalLegalizados,
                                ESNULO((select sum(s.VALOR) from SOPORTE_CAJ s join ARQUEO_CAJA_MENOR ar on ar.ID_ARQUEO = s.ID_ARQUEO WHERE s.VALE_PROV IN (1) and s.ID_ARQUEO = arq.ID_ARQUEO),0) as TotalNoLegalizados,
                                ESNULO((select sum(a.TOTAL) from arqueo_caja_menor_det a where arq.ID_ARQUEO = a.ID_ARQUEO and ac.IDAREA = a.IDAREA),0) as TotalEfectivo,
                                ESNULO((select ac.BASE_VALOR from AREAS_CAJ a where a.COD_USUARIO = ac.COD_USUARIO),0) as Base
                                from AREAS_CAJ ac
                                inner join ARQUEO_CAJA_MENOR a on ac.IDAREA = a.IDAREA
                                inner join ARQUEO_CAJA_MENOR_DET arq on a.ID_ARQUEO = arq.ID_ARQUEO
                                where arq.ID_ARQUEO = " + id_arqueo +
                                ") select distinct TotalLegalizados, TotalNoLegalizados, TotalLegalizados + TotalNoLegalizados as TotalGastos, TotalEfectivo, Base, Base - (TotalLegalizados+TotalNoLegalizados+TotalEfectivo) as Diferencia from resumen ";
                    
                    connection.Open();
                    cmdTransaccionFactory.Connection = connection;
                    cmdTransaccionFactory.CommandType = CommandType.Text;
                    cmdTransaccionFactory.CommandText = sql;
                    resultado = cmdTransaccionFactory.ExecuteReader();

                    if (resultado.Read())
                    {
                        ArqueoCajaMenor entidad = new ArqueoCajaMenor();
                        if (resultado["TotalLegalizados"] != DBNull.Value) entidad.total_legalizados = Convert.ToInt64(resultado["TotalLegalizados"]);
                        if (resultado["TotalNoLegalizados"] != DBNull.Value) entidad.total_no_legalizados = Convert.ToInt64(resultado["TotalNoLegalizados"]);
                        if (resultado["TotalGastos"] != DBNull.Value) entidad.total_gastos = Convert.ToInt64(resultado["TotalGastos"]);
                        if (resultado["TotalEfectivo"] != DBNull.Value) entidad.total_efectivo = Convert.ToInt64((resultado["TotalEfectivo"]));
                        if (resultado["Base"] != DBNull.Value) entidad.valor = Convert.ToInt64(resultado["Base"]); 
                        if (resultado["Diferencia"] != DBNull.Value) entidad.diferencia = Convert.ToInt64((resultado["Diferencia"]));
                        resumen = entidad;                    
                    }
                    return resumen;
                }
            }
        }





    }
}
