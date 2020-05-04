using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Data
{
    public class MotivoGeneracionExtractoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para Programa
        /// </summary>
        public MotivoGeneracionExtractoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Obtiene la lista de Lineas de credito
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Lineas de credito obtenidos</returns>
        public List<MotivoGeneracionExtracto> ListarMotivoGeneracionExtractos(MotivoGeneracionExtracto entidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<MotivoGeneracionExtracto> lstMotivoGeneracionExtractos = new List<MotivoGeneracionExtracto>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    //try
                    //{
                        //connection.Open();
                        //cmdTransaccionFactory.Connection = connection;
                        //cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        //cmdTransaccionFactory.CommandText = "XPF_AS_MOTIVOEXT";

                        //DbParameter p_DATA = new OracleParameter("p_data", OracleType.Cursor);
                        //p_DATA.Direction = ParameterDirection.Output;
                        //cmdTransaccionFactory.Parameters.Add(p_DATA);
                        ////cmdTransaccionFactory.Parameters.Add(new OracleParameter("p_data", OracleType.Cursor)).Direction = ParameterDirection.Output;
                        //resultado = cmdTransaccionFactory.ExecuteReader();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText =
                            @"SELECT * FROM motivo_extracto ";
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            entidad = new MotivoGeneracionExtracto();
                            //Asociar todos los valores a la entidad
                            if (resultado["COD_MOTIVO_EXT"] != DBNull.Value) entidad.Codigo = Convert.ToInt32(resultado["COD_MOTIVO_EXT"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.Nombre = Convert.ToString(resultado["DESCRIPCION"]);
                            lstMotivoGeneracionExtractos.Add(entidad);
                        }

                        return lstMotivoGeneracionExtractos;
                    //}
                    //catch (Exception ex)
                    //{
                    //    BOExcepcion.Throw("MotivoGeneracionExtractoData", "ListarMotivoGeneracionExtractos", ex);
                    //    return null;
                    //}
                }
            }
        }

    }
}
