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
    /// <summary>
    /// Objeto de acceso a datos para la tabla Programa
    /// </summary>
    public class FacturaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para Programa
        /// </summary>
        public FacturaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

  
        /// <summary>
        /// Obtiene un registro del número de facturaData
        /// </summary>
        /// <returns>Central consultada</returns>
        public Factura ObtenerNumeroFactura(Usuario pUsuario)
        {
            Factura entidad = new Factura();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pENTIDAD = cmdTransaccionFactory.CreateParameter();
                        pENTIDAD.ParameterName = "pENTIDAD";
                        pENTIDAD.Value = "                               ";
                        pENTIDAD.Direction = ParameterDirection.Output;

                        DbParameter pNUMEROFACTURA = cmdTransaccionFactory.CreateParameter();
                        pNUMEROFACTURA.ParameterName = "pNUMEROFACTURA";
                        pNUMEROFACTURA.Value = "                               ";
                        pNUMEROFACTURA.Direction = ParameterDirection.Output;

                        DbParameter pRESOLUCION = cmdTransaccionFactory.CreateParameter();
                        pRESOLUCION.ParameterName = "pRESOLUCION";
                        pRESOLUCION.Value = "                               ";
                        pRESOLUCION.Direction = ParameterDirection.Output;

                        cmdTransaccionFactory.Parameters.Add(pENTIDAD);
                        cmdTransaccionFactory.Parameters.Add(pNUMEROFACTURA);
                        cmdTransaccionFactory.Parameters.Add(pRESOLUCION);

                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_NUMEROFACTURA"; 
                        connection.Open();
                        cmdTransaccionFactory.ExecuteNonQuery();

                        entidad.entidad = Convert.ToString(pENTIDAD.Value);
                        entidad.numerofactura = Convert.ToString(pNUMEROFACTURA.Value);
                        entidad.resolucion = Convert.ToString(pRESOLUCION.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("FacturaData", "ObtenerNumeroFactura", ex);
                        return null;
                    }
                }
            }
        }


    }
}