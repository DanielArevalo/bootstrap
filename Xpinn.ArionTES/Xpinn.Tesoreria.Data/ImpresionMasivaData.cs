using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Tesoreria.Entities;
using System.Web;
using System.Web.UI.WebControls;


namespace Xpinn.Tesoreria.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla Giro
    /// </summary>
    public class ImpresionMasivaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla Giro
        /// </summary>
        public ImpresionMasivaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public List<Xpinn.Contabilidad.Entities.Comprobante> ListarComprobante(Xpinn.Contabilidad.Entities.Comprobante pComprobante, Usuario pUsuario, String filtro, String orden)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Xpinn.Contabilidad.Entities.Comprobante> lstComprobante = new List<Xpinn.Contabilidad.Entities.Comprobante>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string condicion = ObtenerFiltro(pComprobante, "v_comprobante.");
                        string sql = @"Select v_comprobante.*, usuarios.nombre As elaboro, ua.nombre as Aprobo, " 
                                        +"Decode(v_comprobante.tipo_comp, 5, (Select Sum(Decode(d.tipo, 'C', d.valor, -d.valor)) From d_comprobante d Where d.num_comp = v_comprobante.num_comp And d.tipo_comp = v_comprobante.tipo_comp And d.cod_cuenta Like '11%'), 0) AS desembolso "
                                        +"from v_comprobante Left Join usuarios On v_comprobante.cod_elaboro = usuarios.codusuario "
                                        +"Left Join usuarios ua On v_comprobante.cod_aprobo = ua.codusuario ";
                        if (condicion.Trim() != "")
                        {
                            sql = sql + condicion;
                        }
                        if (filtro.Trim() != "")
                        {
                            if (condicion.Trim() == "")
                                sql = sql + " Where 1=1 ";
                            sql = sql + filtro;
                        }
                        if (orden != "")
                            sql += " Order by " + orden;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Xpinn.Contabilidad.Entities.Comprobante entidad = new Xpinn.Contabilidad.Entities.Comprobante();

                            if (resultado["NUM_COMP"] != DBNull.Value) entidad.num_comp = Convert.ToInt64(resultado["NUM_COMP"]);
                            if (resultado["TIPO_COMP"] != DBNull.Value) entidad.tipo_comp = Convert.ToInt64(resultado["TIPO_COMP"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["DESCRIPCION_CONCEPTO"] != DBNull.Value) entidad.descripcion_concepto = Convert.ToString(resultado["DESCRIPCION_CONCEPTO"]);
                            if (resultado["N_DOCUMENTO"] != DBNull.Value) entidad.n_documento = Convert.ToString(resultado["N_DOCUMENTO"]);
                            if (resultado["NUM_CONSIG"] != DBNull.Value) entidad.num_consig = Convert.ToString(resultado["NUM_CONSIG"]);
                            if (entidad.tipo_comp == 5)
                            {
                                if (resultado["N_DOCUMENTO"] != DBNull.Value) entidad.soporte = Convert.ToString(resultado["N_DOCUMENTO"]);
                            }
                            else
                            {
                                if (resultado["NUM_CONSIG"] != DBNull.Value) entidad.soporte = Convert.ToString(resultado["NUM_CONSIG"]);
                            }
                            if (resultado["CIUDAD"] != DBNull.Value) entidad.ciudad = Convert.ToInt64(resultado["CIUDAD"]);
                            if (resultado["IDEN_BENEF"] != DBNull.Value) entidad.iden_benef = Convert.ToString(resultado["IDEN_BENEF"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["APELLIDOS"] != DBNull.Value) entidad.apellidos = Convert.ToString(resultado["APELLIDOS"]);
                            if (resultado["RAZON_SOCIAL"] != DBNull.Value) entidad.razon_social = Convert.ToString(resultado["RAZON_SOCIAL"]);
                            if (resultado["COD_ELABORO"] != DBNull.Value) entidad.cod_elaboro = Convert.ToInt64(resultado["COD_ELABORO"]);
                            if (resultado["ELABORO"] != DBNull.Value) entidad.elaboro = Convert.ToString(resultado["ELABORO"]);
                            if (resultado["COD_APROBO"] != DBNull.Value) entidad.cod_aprobo = Convert.ToInt64(resultado["COD_APROBO"]);
                            if (resultado["APROBO"] != DBNull.Value) entidad.aprobo = Convert.ToString(resultado["APROBO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["TOTALCOM"] != DBNull.Value) entidad.totalcom = Convert.ToDecimal(resultado["TOTALCOM"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);
                            if (resultado["DESEMBOLSO"] != DBNull.Value) entidad.desembolso = Convert.ToDecimal(resultado["DESEMBOLSO"]);
                            lstComprobante.Add(entidad);
                        }

                        return lstComprobante;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ImpresionMasivaData", "ListarComprobante", ex);
                        return null;
                    }
                }
            }
        }


    }
}
