using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Data
{
    public class CodeudorData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public CodeudorData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public List<Codeudor> Listar(DetalleProducto pEntityDetalleProducto, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Codeudor> lstCodeudor = new List<Codeudor>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  CODEUDORES WHERE NUMERO_RADICACION = " + pEntityDetalleProducto.NumeroRadicacion;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Codeudor entidad = new Codeudor();

                            if (resultado["IDCODEUD"] != DBNull.Value) entidad.IdCodeudor = Convert.ToInt64(resultado["IDCODEUD"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.NumeroRadicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["CODPERSONA"] != DBNull.Value) entidad.Persona.IdPersona = Convert.ToInt64(resultado["CODPERSONA"]);
                            if (resultado["TIPO_CODEUDOR"] != DBNull.Value) entidad.TipoCodeudor = resultado["TIPO_CODEUDOR"].ToString();
                            if (resultado["PARENTESCO"] != DBNull.Value) entidad.parentesco = Convert.ToInt32(resultado["PARENTESCO"]);
                            if (resultado["OPINION"] != DBNull.Value) entidad.opinion = resultado["OPINION"].ToString();
                            if (resultado["RESPONSABILIDAD"] != DBNull.Value) entidad.responsabilidad = resultado["RESPONSABILIDAD"].ToString();
                            if (resultado["ORDEN"] != DBNull.Value) entidad.orden = Convert.ToInt32(resultado["ORDEN"]);
                            lstCodeudor.Add(entidad);
                        }

                        return lstCodeudor;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CodeudorData", "Listar", ex);
                        return null;
                    }
                }
            }
        }
    }
}