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
    public class AcodeudadosData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public AcodeudadosData(){
            dbConnectionFactory = new ConnectionDataBase();
        }

        public List<Acodeudados> ListarAcodeudados(Cliente pCliente, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Acodeudados> lstPrograma = new List<Acodeudados>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select * from v_acodeudados where CODPERSONA = " + pCliente.IdCliente + "and saldo>0 ORDER BY ESTADO,SALDO";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        
                        while (resultado.Read())
                        {
                            Acodeudados entidad = new Acodeudados();
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NUMERORADICACION"] != DBNull.Value)  entidad.NumRadicacion   = Convert.ToInt64(resultado["NUMERORADICACION"]);
                            if (resultado["CODPERSONA"] != DBNull.Value)        entidad.CodPersona      = Convert.ToInt64(resultado["CODPERSONA"]);
                            if (resultado["ESTADO"] != DBNull.Value)            entidad.Estado          = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["LINEA"] != DBNull.Value)             entidad.Linea           = Convert.ToString(resultado["LINEA"]);
                            if (resultado["NOMBRES"] != DBNull.Value)           entidad.Nombres         = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["MONTO"] != DBNull.Value)             entidad.Monto           = Convert.ToInt64(resultado["MONTO"]);
                            if (resultado["SALDO"] != DBNull.Value)             entidad.Saldo           = Convert.ToInt64(resultado["SALDO"]);
                            if (resultado["CUOTA"] != DBNull.Value)             entidad.Cuota           = Convert.ToInt64(resultado["CUOTA"]);
                            if (resultado["FECHA_PROX_PAGO"] != DBNull.Value)   entidad.FechaProxPago   = Convert.ToDateTime(resultado["FECHA_PROX_PAGO"]);

                            lstPrograma.Add(entidad);
                        }
                        return lstPrograma;
                    }
                    catch (Exception ex){
                        BOExcepcion.Throw("AcodeudadosData", "ListarAcodeudados", ex);
                        return null;
                    }
                }
            }
        }





        public List<Acodeudados> ListarAcodeudadoss(Cliente pCliente, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Acodeudados> lstPrograma = new List<Acodeudados>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select v_acodeudados.*,VAsesoresDatosCliente.nomciudad as ciudad, Calcular_VrAPagar(v_acodeudados.numeroradicacion, SYSDATE) As Valor_apagar
                                        From v_acodeudados inner join VAsesoresDatosCliente on v_acodeudados.codpersona = VAsesoresDatosCliente.Cod_persona 
                                        Where codpersona = " + pCliente.IdCliente + " and saldo > 0 ORDER BY SALDO DESC";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Acodeudados entidad = new Acodeudados();
                            if (resultado["CIUDAD"] != DBNull.Value) entidad.ciudad = Convert.ToString(resultado["CIUDAD"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NUMERORADICACION"] != DBNull.Value) entidad.NumRadicacion = Convert.ToInt64(resultado["NUMERORADICACION"]);
                            if (resultado["CODPERSONA"] != DBNull.Value) entidad.CodPersona = Convert.ToInt64(resultado["CODPERSONA"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.Estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.Linea = Convert.ToString(resultado["LINEA"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.Nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["MONTO"] != DBNull.Value) entidad.Monto = Convert.ToInt64(resultado["MONTO"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.Saldo = Convert.ToInt64(resultado["SALDO"]);
                            if (resultado["CUOTA"] != DBNull.Value) entidad.Cuota = Convert.ToInt64(resultado["CUOTA"]);
                            if (resultado["FECHA_PROX_PAGO"] != DBNull.Value) entidad.FechaProxPago = Convert.ToDateTime(resultado["FECHA_PROX_PAGO"]);
                            if (resultado["Valor_apagar"] != DBNull.Value) entidad.Valor_apagar = Convert.ToDecimal(resultado["Valor_apagar"]);
                            if (resultado["Estado_Codeudor"] != DBNull.Value) entidad.Estado_Codeudor = Convert.ToString(resultado["Estado_Codeudor"]);

                            lstPrograma.Add(entidad);
                        }
                        return lstPrograma;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AcodeudadosData", "ListarAcodeudados", ex);
                        return null;
                    }
                }
            }
        }
    }
}
