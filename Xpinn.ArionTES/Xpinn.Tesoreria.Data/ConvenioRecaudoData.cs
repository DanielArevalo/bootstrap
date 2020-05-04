using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Tesoreria.Entities;
using System.Web;

namespace Xpinn.Tesoreria.Data
{
    public class ConvenioRecaudoData:GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public ConvenioRecaudoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public List<ConvenioRecaudo> ListarConvenios(string filtro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ConvenioRecaudo> lstConvenios = new List<ConvenioRecaudo>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select * from CONVENIO_RECAUDO";

                        if (filtro != "")
                        {
                            sql = sql + filtro;
                        }

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ConvenioRecaudo entidad = new ConvenioRecaudo();
                            if (resultado["COD_CONVENIO"] != DBNull.Value) entidad.cod_convenio = Convert.ToInt64(resultado["COD_CONVENIO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre_convenio = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["FECHA_CONVENIO"] != DBNull.Value) entidad.fecha_convenio = Convert.ToDateTime(resultado["FECHA_CONVENIO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["TIPO_PRODUCTO"] != DBNull.Value) entidad.tipo_producto = Convert.ToInt64(resultado["TIPO_PRODUCTO"]);
                            if (resultado["NUMERO_PRODUCTO"] != DBNull.Value) entidad.num_producto = Convert.ToString(resultado["NUMERO_PRODUCTO"]);
                            if (resultado["TIPO_TRAN"] != DBNull.Value) entidad.tipo_tran = Convert.ToInt64(resultado["TIPO_TRAN"]);
                            if (resultado["EAN"] != DBNull.Value) entidad.EAN = Convert.ToString(resultado["EAN"]);
                            lstConvenios.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);

                        return lstConvenios;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConvenioRecaudoData", "ListarConvenios", ex);
                        return null;
                    }

                }
            }



        }

        public ConvenioRecaudo ConsultarConvenio(string id, Usuario vUsuario)
        {
            DbDataReader resultado;
            ConvenioRecaudo entidad = new ConvenioRecaudo();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select * from CONVENIO_RECAUDO where COD_CONVENIO =" + id;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COD_CONVENIO"] != DBNull.Value) entidad.cod_convenio = Convert.ToInt64(resultado["COD_CONVENIO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre_convenio = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["FECHA_CONVENIO"] != DBNull.Value) entidad.fecha_convenio = Convert.ToDateTime(resultado["FECHA_CONVENIO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["TIPO_PRODUCTO"] != DBNull.Value) entidad.tipo_producto = Convert.ToInt64(resultado["TIPO_PRODUCTO"]);
                            if (resultado["NUMERO_PRODUCTO"] != DBNull.Value) entidad.num_producto = Convert.ToString(resultado["NUMERO_PRODUCTO"]);
                            if (resultado["TIPO_TRAN"] != DBNull.Value) entidad.tipo_tran = Convert.ToInt64(resultado["TIPO_TRAN"]);
                            if (resultado["CUENTA_PROPIA"] != DBNull.Value) entidad.cuenta_propia = Convert.ToInt32(resultado["CUENTA_PROPIA"]);
                            if (resultado["CONTRATO_FIRMADO"] != DBNull.Value) entidad.contrato_firmado = Convert.ToInt32(resultado["CONTRATO_FIRMADO"]);
                            if (resultado["NATURALEZA_CONVENIO"] != DBNull.Value) entidad.naturaleza_convenio = Convert.ToInt32(resultado["NATURALEZA_CONVENIO"]);
                            if (resultado["EAN"] != DBNull.Value) entidad.EAN  = Convert.ToString (resultado["EAN"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConvenioRecaudoData", "ConsultarConvenio", ex);
                        return null;
                    }


                }
            }
        }

        public ConvenioRecaudo Cre_Mod_Convenio(ConvenioRecaudo ConvenioReca, int opcion, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter P_COD_CONVENIO = cmdTransaccionFactory.CreateParameter();
                        P_COD_CONVENIO.ParameterName = "P_COD_CONVENIO";
                        P_COD_CONVENIO.Value = ConvenioReca.cod_convenio;
                        if (opcion == 1) //CREAR
                            P_COD_CONVENIO.Direction = ParameterDirection.Output;
                        else if (opcion == 2)//MODIFICAR
                            P_COD_CONVENIO.Direction = ParameterDirection.Input;
                        P_COD_CONVENIO.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(P_COD_CONVENIO);

                        DbParameter P_NOMBRE = cmdTransaccionFactory.CreateParameter();
                        P_NOMBRE.ParameterName = "P_NOMBRE";
                        if (ConvenioReca.nombre_convenio != "") P_NOMBRE.Value = ConvenioReca.nombre_convenio; else P_NOMBRE.Value = DBNull.Value;
                        P_NOMBRE.Direction = ParameterDirection.Input;
                        P_NOMBRE.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(P_NOMBRE);

                        DbParameter P_FECHA_CONVENIO = cmdTransaccionFactory.CreateParameter();
                        P_FECHA_CONVENIO.ParameterName = "P_FECHA_CONVENIO";
                        if (ConvenioReca.fecha_convenio != null) P_FECHA_CONVENIO.Value = ConvenioReca.fecha_convenio; else P_FECHA_CONVENIO.Value = DBNull.Value;
                        P_FECHA_CONVENIO.Value = ConvenioReca.fecha_convenio;
                        P_FECHA_CONVENIO.Direction = ParameterDirection.Input;
                        P_FECHA_CONVENIO.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(P_FECHA_CONVENIO);

                        DbParameter P_COD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        P_COD_PERSONA.ParameterName = "P_COD_PERSONA";
                        P_COD_PERSONA.Value = ConvenioReca.cod_persona;
                        P_COD_PERSONA.Direction = ParameterDirection.Input;
                        P_COD_PERSONA.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(P_COD_PERSONA);

                        DbParameter P_TIPO_PRODUCTO = cmdTransaccionFactory.CreateParameter();
                        P_TIPO_PRODUCTO.ParameterName = "P_TIPO_PRODUCTO";
                        P_TIPO_PRODUCTO.Value = ConvenioReca.tipo_producto;
                        P_TIPO_PRODUCTO.Direction = ParameterDirection.Input;
                        P_TIPO_PRODUCTO.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(P_TIPO_PRODUCTO);

                        DbParameter P_NUMERO_PRODUCTO = cmdTransaccionFactory.CreateParameter();
                        P_NUMERO_PRODUCTO.ParameterName = "P_NUMERO_PRODUCTO";
                        if (ConvenioReca.num_producto != "") P_NUMERO_PRODUCTO.Value = ConvenioReca.num_producto; else P_NUMERO_PRODUCTO.Value = DBNull.Value;
                        P_NUMERO_PRODUCTO.Direction = ParameterDirection.Input;
                        P_NUMERO_PRODUCTO.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(P_NUMERO_PRODUCTO);

                        DbParameter P_TIPO_TRAN = cmdTransaccionFactory.CreateParameter();
                        P_TIPO_TRAN.ParameterName = "P_TIPO_TRAN";
                        if (ConvenioReca.tipo_tran != 0) P_TIPO_TRAN.Value = ConvenioReca.tipo_tran; else P_TIPO_TRAN.Value = DBNull.Value; ;
                        P_TIPO_TRAN.Direction = ParameterDirection.Input;
                        P_TIPO_TRAN.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(P_TIPO_TRAN);

                        DbParameter P_CUENTA_PROPIA = cmdTransaccionFactory.CreateParameter();
                        P_CUENTA_PROPIA.ParameterName = "P_CUENTA_PROPIA";
                        P_CUENTA_PROPIA.Value = ConvenioReca.cuenta_propia;
                        P_CUENTA_PROPIA.Direction = ParameterDirection.Input;
                        P_CUENTA_PROPIA.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_CUENTA_PROPIA);

                        DbParameter P_CONTRATO_FIRMADO = cmdTransaccionFactory.CreateParameter();
                        P_CONTRATO_FIRMADO.ParameterName = "P_CONTRATO_FIRMADO";
                        P_CONTRATO_FIRMADO.Value = ConvenioReca.contrato_firmado;
                        P_CONTRATO_FIRMADO.Direction = ParameterDirection.Input;
                        P_CONTRATO_FIRMADO.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_CONTRATO_FIRMADO);

                        DbParameter P_NATURALEZA_CONVENIO = cmdTransaccionFactory.CreateParameter();
                        P_NATURALEZA_CONVENIO.ParameterName = "P_NATURALEZA_CONVENIO";
                        P_NATURALEZA_CONVENIO.Value = ConvenioReca.naturaleza_convenio;
                        P_NATURALEZA_CONVENIO.Direction = ParameterDirection.Input;
                        P_NATURALEZA_CONVENIO.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_NATURALEZA_CONVENIO);


                        DbParameter P_EAN = cmdTransaccionFactory.CreateParameter();
                        P_EAN.ParameterName = "P_EAN";
                        P_EAN.Value = ConvenioReca.EAN;
                        P_EAN.Direction = ParameterDirection.Input;
                        P_EAN.DbType = DbType.String ;
                        cmdTransaccionFactory.Parameters.Add(P_EAN);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        if (opcion == 1)//CREAR
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CONVENIO_RECAUDO_CRE";
                        else if (opcion == 2) //MODIFICAR
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CONVENIO_RECAUDO_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        if (opcion == 1) // CREAR
                            ConvenioReca.cod_convenio = Convert.ToInt32(P_COD_CONVENIO.Value);
                        return ConvenioReca;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConvenioRecaudoData", "Cre_Mod_Convenio", ex);
                        return null;
                    }
                }
            }
        }

        public Int64 EliminarConvenio(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter P_COD_CONVENIO = cmdTransaccionFactory.CreateParameter();
                        P_COD_CONVENIO.ParameterName = "P_COD_CONVENIO";
                        P_COD_CONVENIO.Value = pId;
                        P_COD_CONVENIO.Direction = ParameterDirection.Input;
                        P_COD_CONVENIO.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(P_COD_CONVENIO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CONVENIO_RECAUDO_ELI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pId;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConvenioRecaudoData", "EliminarConvenio", ex);
                        return 0;
                    }
                }
            }
        }
        public List<ConvenioRecaudo> ConsultarNumeroProducto(Int64 codpersona, Int64 tipoproducto, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ConvenioRecaudo> lstConsulta = new List<ConvenioRecaudo>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select 0 as Numero_cuenta from aporte where cod_persona=" + codpersona;
                        if (tipoproducto == 1)
                        {
                            sql = "select Numero_aporte as Numero_cuenta from aporte where cod_persona=" + codpersona;
                        }
                        if (tipoproducto == 2)
                        {
                            sql = "SELECT numero_radicacion AS Numero_cuenta FROM credito WHERE cod_deudor=" + codpersona;
                        }
                        if (tipoproducto == 3)
                        {
                            sql = "select numero_cuenta from ahorro_vista where cod_persona=" + codpersona;
                        }
                        if (tipoproducto == 4)
                        {
                            sql = "SELECT numero_servicio AS Numero_cuenta FROM servicios WHERE cod_persona=" + codpersona;
                        }
                        if (tipoproducto == 5)
                        {
                            sql = "select numero_cdat as Numero_cuenta  from cdat c join cdat_titular t on c.codigo_cdat=t.codigo_cdat where t.cod_persona=" + codpersona;
                        }
                        if (tipoproducto == 8)
                        {
                            sql = "select Num_devolucion as Numero_cuenta from devolucion where cod_persona=" + codpersona;
                        }
                        if (tipoproducto == 9)
                        {
                            sql = "select Numero_programado as Numero_cuenta from ahorro_programado where cod_persona=" + codpersona;
                        }



                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ConvenioRecaudo entidad = new ConvenioRecaudo();


                            if (resultado["Numero_cuenta"] != DBNull.Value) entidad.num_producto = Convert.ToString(resultado["Numero_cuenta"]);
                            lstConsulta.Add(entidad);

                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstConsulta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConvenioRecaudoData", "ConsultarNumeroProducto", ex);
                        return null;
                    }


                }
            }
            }
        }
}
