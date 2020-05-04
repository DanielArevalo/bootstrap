using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Data
{
    public class PlanCuentasData : GlobalData 
    {  
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para Programa
        /// </summary>
        public PlanCuentasData()
        {
           dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Obtiene la lista de PlanCuentas
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de PlanCuentas obtenidos</returns>
        public List<PlanCuentas> ListarPlanCuentas(PlanCuentas entidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<PlanCuentas> lstPlanCuentas = new List<PlanCuentas>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select cod_cuenta as codigo, cod_cuenta || ' - ' || nombre as Nombre from plan_cuentas order by codigo";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            entidad = new PlanCuentas();
                            //Asociar todos los valores a la entidad
                            if (resultado["codigo"] != DBNull.Value) entidad.Codigo = Convert.ToInt64(resultado["Codigo"]);
                            if (resultado["Nombre"] != DBNull.Value) entidad.Nombre = Convert.ToString(resultado["nombre"]);
                            if (entidad.Nombre.Trim().Length >30)
                            {
                               entidad.Nombre = entidad.Nombre.Substring(0, 30);
                            }
                          //  entidad.Nombre = entidad.Codigo + "-" + entidad.Nombre;

                            lstPlanCuentas.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPlanCuentas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PlanCuentasData", "ListarPlanCuentas", ex);
                        return null;
                    }
                }
            }
        }
    }
}