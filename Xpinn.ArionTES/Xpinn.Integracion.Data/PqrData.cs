using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Integracion.Entities;
 
namespace Xpinn.Integracion.Data
{
    public class PqrData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public PqrData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public Int32 crearPQR(PQR peticion, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {                                                
                        //P_ID_MONEDERO OUT INT_MONEDERO.ID_MONEDERO % TYPE                        
                        DbParameter P_ID = cmdTransaccionFactory.CreateParameter();
                        P_ID.ParameterName = "P_ID";
                        P_ID.Value = 0;
                        P_ID.Size = 15;
                        P_ID.DbType = DbType.Int32;
                        P_ID.Direction = ParameterDirection.Output;


                        //P_COD_PERSONA INT_MONEDERO.COD_PERSONA % TYPE
                        DbParameter P_COD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        P_COD_PERSONA.ParameterName = "P_COD_PERSONA";
                        P_COD_PERSONA.Value = peticion.cod_persona;
                        P_COD_PERSONA.Direction = ParameterDirection.Input;


                        //P_TIPO_PQR PQR.TIPO_PQR % TYPE,
                        DbParameter P_TIPO_PQR = cmdTransaccionFactory.CreateParameter();
                        P_TIPO_PQR.ParameterName = "P_TIPO_PQR";
                        P_TIPO_PQR.Value = peticion.tipo_pqr;
                        P_TIPO_PQR.Direction = ParameterDirection.Input;

                        //P_DESCRIPCION PQR.DESCRIPCION % TYPE)  
                        DbParameter P_DESCRIPCION = cmdTransaccionFactory.CreateParameter();
                        P_DESCRIPCION.ParameterName = "P_DESCRIPCION";
                        P_DESCRIPCION.Value = peticion.descripcion;
                        P_DESCRIPCION.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(P_ID);
                        cmdTransaccionFactory.Parameters.Add(P_COD_PERSONA);
                        cmdTransaccionFactory.Parameters.Add(P_TIPO_PQR);
                        cmdTransaccionFactory.Parameters.Add(P_DESCRIPCION);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APP_PQR_CRE";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        if (P_ID.Value != null)
                        {
                            return Convert.ToInt32(P_ID.Value);
                        }
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PqrData", "crearPQR", ex);
                        return 0;
                    }
                }
            }
            return 0;
        }

        public Int32 actualizarPQR(PQR peticion, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        //P_ID OUT PQR_SEGUIMIENTO.ID % TYPE,    
                        DbParameter P_ID = cmdTransaccionFactory.CreateParameter();
                        P_ID.ParameterName = "P_ID";
                        P_ID.Value = 0;
                        P_ID.Size = 15;
                        P_ID.DbType = DbType.Int32;
                        P_ID.Direction = ParameterDirection.Output;


                        //P_ID_PQR PQR_SEGUIMIENTO.ID_PQR % TYPE,                        
                        DbParameter P_ID_PQR = cmdTransaccionFactory.CreateParameter();
                        P_ID_PQR.ParameterName = "P_ID_PQR";
                        P_ID_PQR.Value = peticion.id;
                        P_ID_PQR.Direction = ParameterDirection.Input;


                        //P_ESTADO PQR.ESTADO % TYPE,   
                        DbParameter P_ESTADO = cmdTransaccionFactory.CreateParameter();
                        P_ESTADO.ParameterName = "P_ESTADO";
                        P_ESTADO.Value = peticion.estado;
                        P_ESTADO.Direction = ParameterDirection.Input;
                            
                        //P_OBSERVACION PQR_SEGUIMIENTO.OBSERVACION % TYPE,
                        DbParameter P_OBSERVACION = cmdTransaccionFactory.CreateParameter();
                        P_OBSERVACION.ParameterName = "P_OBSERVACION";
                        P_OBSERVACION.Value = peticion.descripcion;
                        P_OBSERVACION.Direction = ParameterDirection.Input;

                        //P_NOM_PERSONA PQR_SEGUIMIENTO.NOM_PERSONA % TYPE
                        DbParameter P_NOM_PERSONA = cmdTransaccionFactory.CreateParameter();
                        P_NOM_PERSONA.ParameterName = "P_NOM_PERSONA";
                        P_NOM_PERSONA.Value = peticion.nombre;
                        P_NOM_PERSONA.Direction = ParameterDirection.Input;


                        cmdTransaccionFactory.Parameters.Add(P_ID);
                        cmdTransaccionFactory.Parameters.Add(P_ID_PQR);
                        cmdTransaccionFactory.Parameters.Add(P_ESTADO);
                        cmdTransaccionFactory.Parameters.Add(P_OBSERVACION);
                        cmdTransaccionFactory.Parameters.Add(P_NOM_PERSONA);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APP_PQR_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        if (P_ID.Value != null)
                        {
                            return Convert.ToInt32(P_ID.Value);
                        }
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PqrData", "crearPQR", ex);
                        return 0;
                    }
                }
            }
            return 0;
        }

        public List<PQR> listarPQR(string filtro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<PQR> lista = new List<PQR>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select pqr.*, P.Nombreyapellido, a.snombre1 ||' '|| a.snombre2 ||' '|| a.sapellido1 ||' '|| a.sapellido2 as asesor,
                                        case pqr.estado when 0 then 'Solicitado' when 1 then 'Respondido' when 2 then 'Rechazado' when 3 then 'Aceptado' end as NOM_ESTADO,
                                        case pqr.tipo_pqr when 1 then 'Petición' when 2 then 'Queja' when 3 then 'Reclamo' when 4 then 'Sugerencia' end as NOM_TIPO
                                       from PQR 
                                       inner join v_persona p on Pqr.Cod_Persona = P.Cod_Persona
                                       left join asejecutivos a on p.cod_asesor=a.iusuario
                                       where pqr.id > 0 " + filtro+ " order by FECHA desc";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            PQR entidad = new PQR();
                            if (resultado["ID"] != DBNull.Value) entidad.id = Convert.ToInt32(resultado["ID"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["TIPO_PQR"] != DBNull.Value) entidad.tipo_pqr = Convert.ToInt32(resultado["TIPO_PQR"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["Nombreyapellido"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["Nombreyapellido"]);
                            if (resultado["asesor"] != DBNull.Value) entidad.asesor = Convert.ToString(resultado["asesor"]);
                            if (resultado["NOM_ESTADO"] != DBNull.Value) entidad.nom_estado = Convert.ToString(resultado["NOM_ESTADO"]);
                            if (resultado["NOM_TIPO"] != DBNull.Value) entidad.nom_tipo = Convert.ToString(resultado["NOM_TIPO"]);
                            
                            

                            lista.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lista;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PqrData", "listarPQR", ex);
                        return null;
                    }
                }
            }
        }

        public PQR obtenerPQR(int id_pqr, Usuario vUsuario)
        {
            DbDataReader resultado;
            PQR entidad = new PQR();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select PQR.*, 
                                       case pqr.estado when 0 then 'Solicitado' when 1 then 'Respondido' when 2 then 'Rechazado' when 3 then 'Aceptado' end as NOM_ESTADO,
                                       case pqr.tipo_pqr when 1 then 'Petición' when 2 then 'Queja' when 3 then 'Reclamo' when 4 then 'Sugerencia' end as NOM_TIPO,
                                       P.Nombre
                                       from PQR
                                       inner join v_persona p on p.cod_persona = pqr.cod_persona
                                       where id = " + id_pqr;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {                            
                            if (resultado["ID"] != DBNull.Value) entidad.id = Convert.ToInt32(resultado["ID"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["TIPO_PQR"] != DBNull.Value) entidad.tipo_pqr = Convert.ToInt32(resultado["TIPO_PQR"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["ADJUNTO"] != DBNull.Value) entidad.adjunto = (byte[])(resultado["ADJUNTO"]);
                            if (resultado["NOM_ESTADO"] != DBNull.Value) entidad.nom_estado = Convert.ToString(resultado["NOM_ESTADO"]);
                            if (resultado["NOM_TIPO"] != DBNull.Value) entidad.nom_tipo = Convert.ToString(resultado["NOM_TIPO"]);
                            if (resultado["Nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["Nombre"]);                            
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PqrData", "obtenerPQR", ex);
                        return null;
                    }
                }
            }
        }

        public List<PQR_Respuesta> listarSeguimientoPQR(int id_pqr, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<PQR_Respuesta> lista = new List<PQR_Respuesta>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select * from PQR_SEGUIMIENTO where ID_PQR = " + id_pqr + " order by FECHA_RESPUESTA desc";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            PQR_Respuesta entidad = new PQR_Respuesta();
                            if (resultado["ID"] != DBNull.Value) entidad.id = Convert.ToInt32(resultado["ID"]);
                            if (resultado["ID_PQR"] != DBNull.Value) entidad.id_pqr = Convert.ToInt32(resultado["ID_PQR"]);
                            if (resultado["OBSERVACION"] != DBNull.Value) entidad.observacion = Convert.ToString(resultado["OBSERVACION"]);
                            if (resultado["NOM_PERSONA"] != DBNull.Value) entidad.nom_persona = Convert.ToString(resultado["NOM_PERSONA"]);
                            if (resultado["FECHA_RESPUESTA"] != DBNull.Value) entidad.fecha_respuesta = Convert.ToDateTime(resultado["FECHA_RESPUESTA"]);
                            lista.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lista;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PqrData", "listarSeguimientoPQR", ex);
                        return null;
                    }
                }
            }
        }

    }
}
