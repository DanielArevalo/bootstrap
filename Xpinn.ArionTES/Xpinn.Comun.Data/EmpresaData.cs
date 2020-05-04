using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Comun.Entities;

namespace Xpinn.Comun.Data
{
    public class EmpresaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla ConceptoS
        /// </summary>
        public EmpresaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Obtiene datos de la empresa
        /// </summary>
        /// <param name="pConceptoS">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de cierres obtenidos</returns>
        public Empresa ConsultarEmpresa(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Empresa entidad = new Empresa();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Empresa WHERE COD_EMPRESA = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt32(resultado["COD_EMPRESA"]);
                            if (resultado["NIT"] != DBNull.Value) entidad.nit = Convert.ToString(resultado["NIT"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["SIGLA"] != DBNull.Value) entidad.sigla = Convert.ToString(resultado["SIGLA"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["FECHA_CONSTITUCIÓN"] != DBNull.Value) entidad.fecha_constitución = Convert.ToDateTime(resultado["FECHA_CONSTITUCIÓN"]);
                            if (resultado["CIUDAD"] != DBNull.Value) entidad.ciudad = Convert.ToInt32(resultado["CIUDAD"]);
                            if (resultado["E_MAIL"] != DBNull.Value) entidad.e_mail = Convert.ToString(resultado["E_MAIL"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpresaData", "ConsultarEmpresa", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene datos de la empresa con recaudos por persona
        /// </summary>
    }
}
