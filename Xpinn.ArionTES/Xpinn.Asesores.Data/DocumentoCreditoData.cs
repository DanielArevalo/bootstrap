using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using System.Data;
using System.Data.Common;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Data
{
    public class DocumentoCreditoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public DocumentoCreditoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public List<DocumentoProducto> Listar(Int64 pNumeroRadicacion, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<DocumentoProducto> lstDocProd = new List<DocumentoProducto>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        //DbParameter pNumRadicacion = cmdTransaccionFactory.CreateParameter();
                        //pNumRadicacion.ParameterName = "P_NUMRADICACION";
                        //pNumRadicacion.Direction = ParameterDirection.Input;
                        //pNumRadicacion.Value = pNumeroRadicacion;

                        //OracleParameter pData = (OracleParameter)cmdTransaccionFactory.CreateParameter();
                        //pData.ParameterName = "P_DATA";
                        //pData.Direction = ParameterDirection.Output;
                        //pData.OracleType = OracleType.Cursor;

                        //cmdTransaccionFactory.Parameters.Add(pNumRadicacion);
                        //cmdTransaccionFactory.Parameters.Add(pData);

                        //connection.Open();
                        //cmdTransaccionFactory.Connection = connection;
                        //cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        //cmdTransaccionFactory.CommandText = "XPF_AS_DOCPRODUCTO_CONSULTAR";
                        //resultado = cmdTransaccionFactory.ExecuteReader();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText =
                            @"SELECT	*
	                            FROM    VAsesoresDocumentosCreditos
	                            WHERE	numero_radicacion = " + pNumeroRadicacion;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            DocumentoProducto entidad = new DocumentoProducto();

                            if (resultado["numero_radicacion"] != DBNull.Value) entidad.NumeroRadicacion    = Convert.ToInt64(resultado["numero_radicacion"].ToString());
                            if (resultado["referencia"] != DBNull.Value)        entidad.Referencia          = resultado["referencia"].ToString();
                            if (resultado["descripcion"] != DBNull.Value)       entidad.Descripcion         = resultado["descripcion"].ToString();

                            lstDocProd.Add(entidad);
                        }

                        return lstDocProd;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DocumentoCreditoData", "Consultar", ex);
                        return null;
                    }
                }
            }
        }
    }
}