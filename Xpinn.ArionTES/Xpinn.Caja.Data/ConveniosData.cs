using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Caja.Entities;

namespace Xpinn.Caja.Data
{

    /// <summary>
    /// Objeto de acceso a datos para la tabla TipoMoneda
    /// </summary>
    public class ConveniosData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para Programa
        /// </summary>
        public ConveniosData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public ConveniosRecaudo ConsultarConvenioRecaudo(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            ConveniosRecaudo entidad = new ConveniosRecaudo();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT C.*,P.TIPO_IDENTIFICACION,P.IDENTIFICACION,P.NOMBRE AS NOMCLIENTE,TC.DESCRIPCION AS NOMBRE_PRODUC,TT.DESCRIPCION AS NOMBRE_TRAN
                                        FROM CONVENIO_RECAUDO C INNER JOIN V_PERSONA P ON P.COD_PERSONA = C.COD_PERSONA
                                        INNER JOIN TIPOPRODUCTOCAJA TC ON TC.CODTIPOPRODUCTOCAJA = C.TIPO_PRODUCTO
                                        LEFT JOIN TIPO_TRAN TT ON TT.TIPO_TRAN = C.TIPO_TRAN
                                        WHERE C.COD_CONVENIO = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COD_CONVENIO"] != DBNull.Value) entidad.cod_convenio = Convert.ToInt64(resultado["COD_CONVENIO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["FECHA_CONVENIO"] != DBNull.Value) entidad.fecha_convenio = Convert.ToDateTime(resultado["FECHA_CONVENIO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["TIPO_PRODUCTO"] != DBNull.Value) entidad.tipo_producto = Convert.ToInt32(resultado["TIPO_PRODUCTO"]);
                            if (resultado["NUMERO_PRODUCTO"] != DBNull.Value) entidad.numero_producto = Convert.ToString(resultado["NUMERO_PRODUCTO"]);
                            if (resultado["TIPO_TRAN"] != DBNull.Value) entidad.tipo_tran = Convert.ToInt32(resultado["TIPO_TRAN"]);

                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToInt32(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMCLIENTE"] != DBNull.Value) entidad.nombre_persona = Convert.ToString(resultado["NOMCLIENTE"]);
                            if (resultado["NOMBRE_PRODUC"] != DBNull.Value) entidad.nombre_produc = Convert.ToString(resultado["NOMBRE_PRODUC"]);
                            if (resultado["NOMBRE_TRAN"] != DBNull.Value) entidad.nombre_tran = Convert.ToString(resultado["NOMBRE_TRAN"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConveniosData", "ConsultarConvenioRecaudo", ex);
                        return null;
                    }
                }
            }
        }
        

    }
}
