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
    public class InformacionFinancieraData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public InformacionFinancieraData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public InformacionFinanciera ListarInformacionFinanciera(int codigo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {

                    string sql = "SELECT * FROM VAsesoresinffinanciera WHERE IDENTIFICACION = '" + Convert.ToString(codigo) + "'";

                    connection.Open();
                    cmdTransaccionFactory.Connection = connection;
                    cmdTransaccionFactory.CommandType = CommandType.Text;
                    cmdTransaccionFactory.CommandText = sql;
                    resultado = cmdTransaccionFactory.ExecuteReader();

                    InformacionFinanciera entidad = new InformacionFinanciera();

                    while (resultado.Read())
                    {
                        if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt32(resultado["COD_PERSONA"]);
                        if (resultado["TOTINGRESOS"] != DBNull.Value) entidad.totingresos = Convert.ToInt32(resultado["TOTINGRESOS"]);
                       if (resultado["TOTEGRESOS"] != DBNull.Value) entidad.totegresos = Convert.ToInt32(resultado["TOTEGRESOS"]);
                        if (resultado["TOTACTIVOS"] != DBNull.Value) entidad.totactivos = Convert.ToInt32(resultado["TOTACTIVOS"]);
                        if (resultado["TOTPASIVOS"] != DBNull.Value) entidad.totpasivo = Convert.ToInt32(resultado["TOTPASIVOS"]);
                        if (resultado["TOTDISPONIBLE"] != DBNull.Value) entidad.totdisponible = Convert.ToInt32(resultado["TOTDISPONIBLE"]);
                        if (resultado["TOTPATRIMONIO"] != DBNull.Value) entidad.totpatrimonio = Convert.ToInt32(resultado["TOTPATRIMONIO"]);


                    }

                    return entidad;

                }
            }
        }
    }
}

