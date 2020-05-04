using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Caja.Entities;
using System.Web;
using System.Web.UI.WebControls;

namespace Xpinn.Caja.Data
{

    /// <summary>
    /// Objeto de acceso a datos para la tabla Cajero
    /// </summary>
    public class CajeroData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para Programa
        /// </summary>
        public CajeroData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea una entidad Cajero en la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad Cajero</param>
        /// <returns>Entidad creada</returns>
        public Cajero InsertarCajero(Cajero pEntidad, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_cajero = cmdTransaccionFactory.CreateParameter();
                        pcod_cajero.ParameterName = "pcodigocajero";
                        pcod_cajero.Value = 0;
                        pcod_cajero.DbType = DbType.Int64;
                        pcod_cajero.Size = 8;
                        pcod_cajero.Direction = ParameterDirection.InputOutput;

                        DbParameter pcod_caja = cmdTransaccionFactory.CreateParameter();
                        pcod_caja.ParameterName = "pcodigocaja";
                        pcod_caja.Value = pEntidad.cod_caja;
                        pcod_caja.DbType = DbType.Int64;
                        pcod_caja.Size = 8;
                        pcod_caja.Direction = ParameterDirection.Input;

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "pcodigopersona";
                        pcod_persona.Value = pEntidad.cod_persona;
                        pcod_persona.DbType = DbType.Int64;
                        pcod_persona.Size = 8;
                        pcod_persona.Direction = ParameterDirection.Input;

                        DbParameter pfecha_ingreso = cmdTransaccionFactory.CreateParameter();
                        pfecha_ingreso.ParameterName = "pfechaingreso";
                        pfecha_ingreso.Value = pEntidad.fecha_ingreso;
                        pfecha_ingreso.DbType = DbType.DateTime;
                        pfecha_ingreso.Direction = ParameterDirection.Input;
                        pfecha_ingreso.Size = 7;

                        DbParameter pfecha_retiro = cmdTransaccionFactory.CreateParameter();
                        pfecha_retiro.ParameterName = "pfecharetiro";
                        pfecha_retiro.Value = pEntidad.fecha_retiro;
                        pfecha_retiro.DbType = DbType.DateTime;
                        pfecha_retiro.Direction = ParameterDirection.Input;
                        pfecha_retiro.Size = 7;

                        DbParameter p_estado = cmdTransaccionFactory.CreateParameter();
                        p_estado.ParameterName = "pestado";
                        p_estado.Value = pEntidad.estado;
                        p_estado.DbType = DbType.Int64;
                        p_estado.Size = 8;
                        p_estado.Direction = ParameterDirection.Input;


                        DbParameter PIDENTIFICACION = cmdTransaccionFactory.CreateParameter();
                        PIDENTIFICACION.ParameterName = "P_IDENTIFICACION";
                        PIDENTIFICACION.Value = pEntidad.identificacion;
                        PIDENTIFICACION.Direction = ParameterDirection.Input;
                        PIDENTIFICACION.DbType = DbType.String;
                      


                        cmdTransaccionFactory.Parameters.Add(pcod_cajero);
                        cmdTransaccionFactory.Parameters.Add(pcod_caja);
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);
                        cmdTransaccionFactory.Parameters.Add(pfecha_ingreso);
                        cmdTransaccionFactory.Parameters.Add(pfecha_retiro);
                        cmdTransaccionFactory.Parameters.Add(p_estado);
                        cmdTransaccionFactory.Parameters.Add(PIDENTIFICACION);




                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_CAJEROINSERTAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pEntidad, "CAJERO", pUsuario, Accion.Crear.ToString(), TipoAuditoria.CajaFinanciera, "Registrar Cajero");
                            //DAauditoria.InsertarLog(pEntidad, pUsuario, long.Parse(pEntidad.cod_cajero), "CAJERO", Accion.Crear.ToString(), connection, cmdTransaccionFactory); //REGISTRO DE AUDITORIA

                        pEntidad.cod_cajero = pcod_cajero.Value.ToString();

                        return pEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CajeroData", "InsertarCajero", ex);
                        return null;
                    }

                }

            }
        }

        /// <summary>
        /// Modifica una entidad Cajero en la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad Cajero</param>
        /// <returns>Entidad modificada</returns>
        public Cajero ModificarCajero(Cajero pEntidad, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_cajero = cmdTransaccionFactory.CreateParameter();
                        pcod_cajero.ParameterName = "pcodigocajero";
                        pcod_cajero.Value = pEntidad.cod_cajero;
                        pcod_cajero.DbType = DbType.Int64;
                        pcod_cajero.Size = 8;
                        pcod_cajero.Direction = ParameterDirection.Input;

                        DbParameter pcod_caja = cmdTransaccionFactory.CreateParameter();
                        pcod_caja.ParameterName = "pcodigocaja";
                        pcod_caja.Value = pEntidad.cod_caja;
                        pcod_caja.DbType = DbType.Int64;
                        pcod_caja.Size = 8;
                        pcod_caja.Direction = ParameterDirection.Input;

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "pcodigopersona";
                        pcod_persona.Value = pEntidad.cod_persona;
                        pcod_persona.DbType = DbType.Int64;
                        pcod_persona.Size = 8;
                        pcod_persona.Direction = ParameterDirection.Input;

                        DbParameter pfecha_ingreso = cmdTransaccionFactory.CreateParameter();
                        pfecha_ingreso.ParameterName = "pfechaingreso";
                        pfecha_ingreso.Value = pEntidad.fecha_ingreso;
                        pfecha_ingreso.DbType = DbType.DateTime;
                        pfecha_ingreso.Direction = ParameterDirection.Input;
                        pfecha_ingreso.Size = 7;

                        DbParameter pfecha_retiro = cmdTransaccionFactory.CreateParameter();
                        pfecha_retiro.ParameterName = "pfecharetiro";
                        pfecha_retiro.Value = pEntidad.fecha_retiro;
                        pfecha_retiro.DbType = DbType.DateTime;
                        pfecha_retiro.Direction = ParameterDirection.Input;
                        pfecha_retiro.Size = 7;

                        DbParameter p_estado = cmdTransaccionFactory.CreateParameter();
                        p_estado.ParameterName = "pestado";
                        p_estado.Value = pEntidad.estado;
                        p_estado.DbType = DbType.Int64;
                        p_estado.Size = 8;
                        p_estado.Direction = ParameterDirection.Input;



                        DbParameter PIDENTIFICACION = cmdTransaccionFactory.CreateParameter();
                        PIDENTIFICACION.ParameterName = "P_IDENTIFICACION";
                        PIDENTIFICACION.Value = pEntidad.identificacion;
                        PIDENTIFICACION.Direction = ParameterDirection.Input;
                        PIDENTIFICACION.DbType = DbType.String;




                        cmdTransaccionFactory.Parameters.Add(pcod_cajero);
                        cmdTransaccionFactory.Parameters.Add(pcod_caja);
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);
                        cmdTransaccionFactory.Parameters.Add(pfecha_ingreso);
                        cmdTransaccionFactory.Parameters.Add(pfecha_retiro);
                        cmdTransaccionFactory.Parameters.Add(p_estado);
                        cmdTransaccionFactory.Parameters.Add(PIDENTIFICACION);



                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPN_CAJAFIN_CAJERO_U";
                        cmdTransaccionFactory.ExecuteNonQuery();


                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pEntidad, "CAJERO", pUsuario, Accion.Modificar.ToString(), TipoAuditoria.CajaFinanciera, "Actualizar Cajero");
                            //DAauditoria.InsertarLog(pEntidad, pUsuario, long.Parse(pEntidad.cod_cajero), "CAJERO", Accion.Modificar.ToString(), connection, cmdTransaccionFactory); //REGISTRO DE AUDITORIA

                        return pEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CajeroData", "ModificarCajera", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un Cajero en la base de datos
        /// </summary>
        /// <param name="pId">identificador del Cajero</param>
        public void EliminarCajero(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Xpinn.Caja.Entities.Cajero pEntidad = new Xpinn.Caja.Entities.Cajero();

                        if (pUsuario.programaGeneraLog)
                            pEntidad = ConsultarCajero(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pcod_cajero = cmdTransaccionFactory.CreateParameter();
                        pcod_cajero.ParameterName = "pcodigocajero";
                        pcod_cajero.Value = pId;
                        pcod_cajero.DbType = DbType.Int64;
                        pcod_cajero.Size = 8;
                        pcod_cajero.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(pcod_cajero);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPN_CAJAFIN_CAJERO_D";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pEntidad, "CAJERO", pUsuario, Accion.Eliminar.ToString(), TipoAuditoria.CajaFinanciera, "Eliminar Cajero");
                            //DAauditoria.InsertarLog(pEntidad, pUsuario, long.Parse(pEntidad.cod_cajero), "CAJERO", Accion.Eliminar.ToString(), connection, cmdTransaccionFactory); //REGISTRO DE AUDITORIA

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CajeroData", "EliminarCajero", ex);
                    }
                }

            }
        }

        /// <summary>
        /// Obtiene un registro de la tabla Oficina de la base de datos
        /// </summary>
        /// <param name="pId">identificador del registro</param>
        /// <returns>Oficina consultada</returns>
        public Cajero ConsultarCajero(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Cajero entidad = new Cajero();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM CAJERO where cod_cajero=" + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["cod_cajero"] != DBNull.Value) entidad.cod_cajero = Convert.ToString(resultado["cod_cajero"]);
                            if (resultado["cod_persona"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["cod_persona"]);
                            if (resultado["cod_caja"] != DBNull.Value) entidad.cod_caja = Convert.ToInt64(resultado["cod_caja"]);
                            if (resultado["fecha_creacion"] != DBNull.Value) entidad.fecha_ingreso = Convert.ToDateTime(resultado["fecha_creacion"]);
                            if (resultado["fecha_retiro"] != DBNull.Value) entidad.fecha_retiro = Convert.ToDateTime(resultado["fecha_retiro"]);
                            if (resultado["estado"] != DBNull.Value) entidad.estado = Convert.ToInt64(resultado["estado"].ToString());
                            if (resultado["cod_caja_des"] != DBNull.Value) entidad.cod_caja_des = Convert.ToInt64(resultado["cod_caja_des"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);

                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CajeroData", "ConsultarCajero", ex);
                        return null;
                    }

                }

            }
        }


        /// <summary>
        /// Obtiene la lista de cajeros dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Cajas obtenidas</returns>
        public List<Cajero> ListarCajero(Cajero pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Cajero> lstCajero = new List<Cajero>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = " SELECT a.cod_cajero codcajero, codusuario,(Select b.cod_caja from caja b where b.cod_caja=a.cod_caja and cod_oficina="+ pEntidad.cod_oficina.ToString()+" ) codcaja,a.fecha_creacion fechacreacion, decode(to_char(a.fecha_retiro,'dd/mm/yyyy'),'01/01/0001',null,to_char(a.fecha_retiro,'dd/mm/yyyy')) fecharetiro, decode(a.estado,1,'Activo','Inactivo') cajeroestado, (Select b.nombre from caja b where b.cod_caja=a.cod_caja) nomcaja, c.nombre nomusuario,a.identificacion FROM cajero a, usuarios c  where a.cod_persona=c.codusuario and c.cod_oficina=" + pEntidad.cod_oficina.ToString() + " Order By cod_cajero asc";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Cajero entidad = new Cajero();
                            //Asociar todos los valores a la entidad
                            if (resultado["codcajero"] != DBNull.Value) entidad.cod_cajero = Convert.ToString(resultado["codcajero"]);
                            if (resultado["codusuario"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["codusuario"]);
                            if (resultado["codcaja"] != DBNull.Value) entidad.cod_caja = Convert.ToInt64(resultado["codcaja"]);
                            if (resultado["fechacreacion"] != DBNull.Value) entidad.fecha_ingreso = Convert.ToDateTime(resultado["fechacreacion"]);

                            if (resultado["fecharetiro"] != DBNull.Value) entidad.fecharetiro = resultado["fecharetiro"].ToString();
                            else
                                entidad.fecharetiro = "";

                            if (resultado["cajeroestado"] != DBNull.Value) entidad.nomestado = resultado["cajeroestado"].ToString();
                            if (resultado["nomcaja"] != DBNull.Value) entidad.nom_caja = Convert.ToString(resultado["nomcaja"]);
                            if (resultado["nomusuario"] != DBNull.Value) entidad.nom_cajero = Convert.ToString(resultado["nomusuario"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);


                            lstCajero.Add(entidad);
                        }

                        return lstCajero;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CajeroData", "ListarCajero", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene la lista de cajeros T dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Cajeros obtenidas</returns>
        public List<Cajero> ListarTCajero(Cajero pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Cajero> lstCajero = new List<Cajero>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = " select a.cod_cajero, b.nombre from cajero a, usuarios b where a.cod_persona=codusuario and b.cod_oficina=" + pEntidad.cod_oficina + " order by a.cod_cajero asc";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Cajero entidad = new Cajero();
                            //Asociar todos los valores a la entidad

                            if (resultado["cod_cajero"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["cod_cajero"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nom_cajero = Convert.ToString(resultado["nombre"]);
                            lstCajero.Add(entidad);
                        }

                        return lstCajero;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CajeroData", "ListarTCajero", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro de la tabla CajeroXCaja de la base de datos
        /// </summary>
        /// <param name="pId">identificador del registro</param>
        /// <returns>CajeroXCaja consultada</returns>
        public Cajero ConsultarCajeroXCaja(Int64 pId, Int64 pCaja, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Cajero entidad = new Cajero();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT cod_caja_des, count(*) as conteo FROM CAJERO where cod_cajero = " + pId.ToString() + " and cod_caja = " + pCaja + " group by cod_caja_des";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            //Asociar todos los valores a la entidad
                            if (resultado["conteo"] != DBNull.Value) entidad.conteo = long.Parse(resultado["conteo"].ToString());
                            if (resultado["COD_CAJA_DES"] != DBNull.Value) entidad.cod_caja_des = long.Parse(resultado["COD_CAJA_DES"].ToString());
                        }
                        else
                        {
                            entidad.conteo = 0;
                            //throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CajeroData", "ConsultarCajeroXCaja", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene un registro de la tabla Cajero de la base de datos
        /// </summary>
        /// <param name="pId">identificador del registro</param>
        /// <returns>Cajero consultada</returns>
        public Caja.Entities.Cajero ConsultarCajeroRelCaja(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Caja.Entities.Cajero entidad = new Caja.Entities.Cajero();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT count(*) conteo FROM CAJERO where cod_persona=" + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["conteo"] != DBNull.Value) entidad.conteo = Convert.ToInt64(resultado["conteo"]);
                        }
                        else
                            entidad.conteo = 0;

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CajeroData", "ConsultarCajeroRelCaja", ex);
                        return null;
                    }

                }
            }
        }


        /// <summary>
        /// Obtiene un registro de la tabla Cajero de la base de datos
        /// </summary>
        /// <param name="pId">identificador del registro</param>
        /// <returns>Cajero Principal Consultado</returns>
        public Caja.Entities.Cajero ConsultarCajeroPrincipal(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Caja.Entities.Cajero entidad = new Caja.Entities.Cajero();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        //string sql = "SELECT  FROM CAJERO where cod_persona=" + pId.ToString();
                        string sql = " select cod_cajero,cod_caja, nombre, codusuario from cajero y , usuarios x where y.cod_persona=x.codusuario and cod_caja in(select cod_caja from caja where cod_oficina=" + pId.ToString() + " and esprincipal=1) ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["cod_caja"] != DBNull.Value) entidad.cod_caja_ppal = Convert.ToInt64(resultado["cod_caja"]);
                            if (resultado["cod_cajero"] != DBNull.Value) entidad.cod_cajero_ppal = Convert.ToInt64(resultado["cod_cajero"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nom_cajero_ppal = Convert.ToString(resultado["nombre"]);
                            if (resultado["codusuario"] != DBNull.Value) entidad.cod_usuario = Convert.ToString(resultado["codusuario"]);
                        }
                        else
                            entidad.conteo = 0;

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CajeroData", "ConsultarCajeroPrincipal", ex);
                        return null;
                    }

                }
            }
        }



        public Caja.Entities.Cajero ConsultarCajeroPrincipalAsignadoAlCajero(Int64 codigo_cajero, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Caja.Entities.Cajero entidad = new Caja.Entities.Cajero();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        //string sql = "SELECT  FROM CAJERO where cod_persona=" + pId.ToString();
                        string sql = @"select * from cajero where cod_caja = (Select cod_caja_des from cajero where cod_cajero = " + codigo_cajero + ")";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["cod_caja"] != DBNull.Value) entidad.cod_caja_ppal = Convert.ToInt64(resultado["cod_caja"]);
                            if (resultado["cod_cajero"] != DBNull.Value) entidad.cod_cajero_ppal = Convert.ToInt64(resultado["cod_cajero"]);
                            //if (resultado["nombre"] != DBNull.Value) entidad.nom_cajero_ppal = Convert.ToString(resultado["nombre"]);
                            //if (resultado["codusuario"] != DBNull.Value) entidad.cod_usuario = Convert.ToString(resultado["codusuario"]);
                        }
                        else
                            entidad.conteo = 0;

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CajeroData", "ConsultarCajeroPrincipalAsignadoAlCajero", ex);
                        return null;
                    }

                }
            }
        }


        /// <summary>
        /// Crea una Caja Cajeros MAsivos para una oficina y caja especifica en la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad Cajero</param>
        /// <returns>Entidad creada</returns>
        public Xpinn.Caja.Entities.Cajero InsertarCajeroMass(Xpinn.Caja.Entities.Cajero pEntidad, GridView gvCajeros, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        //se inserta las opciones de la grilla en TipoOperacion
                        CheckBox chkOperacionPermitida;

                        int persona = 0;//captura el valor del codigo de Tipo de Operacion
                        //se limpia los parametros de entrada

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;

                        foreach (GridViewRow fila in gvCajeros.Rows)
                        {
                            //se captura la opcion chequeda en el grid
                            persona = int.Parse(fila.Cells[0].Text);
                            chkOperacionPermitida = (CheckBox)fila.FindControl("chkPermiso");
                            DropDownList ddlCajaDestino = fila.FindControl("ddlCajaPrincipal") as DropDownList;

                            cmdTransaccionFactory.Parameters.Clear();
                            DbParameter pcod_caja = cmdTransaccionFactory.CreateParameter();
                            pcod_caja.ParameterName = "pcodigocaja";
                            pcod_caja.Value = pEntidad.cod_caja;
                            pcod_caja.Direction = ParameterDirection.Input;

                            DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                            pcod_persona.ParameterName = "pcodigopersona";
                            pcod_persona.Value = persona;
                            pcod_persona.Direction = ParameterDirection.Input;

                            DbParameter p_seleccion = cmdTransaccionFactory.CreateParameter();
                            p_seleccion.ParameterName = "pseleccion";
                            
                            if (chkOperacionPermitida.Checked == false)
                                p_seleccion.Value = 0;
                            else
                                p_seleccion.Value = 1;
                            p_seleccion.Direction = ParameterDirection.Input;

                            DbParameter pcajerodestino = cmdTransaccionFactory.CreateParameter();
                            pcajerodestino.ParameterName = "pcajadestino";

                            if (ddlCajaDestino != null && ddlCajaDestino.SelectedValue != "0")
                            {
                                pcajerodestino.Value = ddlCajaDestino.SelectedValue;
                            }
                            else
                            {
                                pcajerodestino.Value = DBNull.Value;
                            }
                            pcajerodestino.Direction = ParameterDirection.Input;

                            cmdTransaccionFactory.Parameters.Add(pcod_caja);
                            cmdTransaccionFactory.Parameters.Add(pcod_persona);
                            cmdTransaccionFactory.Parameters.Add(p_seleccion);
                            cmdTransaccionFactory.Parameters.Add(pcajerodestino);

                            cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;                            
                            cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_ASIGNAR_CAJERO_U";
                            cmdTransaccionFactory.ExecuteNonQuery();
                         
                        }

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pEntidad, "CAJERO", pUsuario, Accion.Modificar.ToString(), TipoAuditoria.CajaFinanciera, "Asignar Cajero");
                            //DAauditoria.InsertarLog(pEntidad, pUsuario, long.Parse(pEntidad.cod_cajero), "ASIGNARCAJERO", Accion.Modificar.ToString(), connection, cmdTransaccionFactory); //REGISTRO DE AUDITORIA

                        return pEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CajaData", "InsertarCajeroMass", ex);
                        return null;
                    }
                }
            }
        }



        /// <summary>
        /// Obtiene un registro de la tabla Cajero de la base de datos, siempre y cuando sea un usuario auxiliar
        /// </summary>
        /// <param name="pId">identificador del registro</param>
        /// <returns>Cajero Principal Consultado</returns>
        public Caja.Entities.Cajero ConsultarIfUserIsntCajeroPrincipal(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Caja.Entities.Cajero entidad = new Caja.Entities.Cajero();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @" Select cod_cajero, cod_caja, nombre, cod_oficina,
                                         (select count(*) from caja z where z.esprincipal = 1 and z.cod_caja = y.cod_caja) esprincipal, y.estado estcajero, 
                                         (select Max(tipo_proceso) from procesooficina z where z.cod_oficina = x.cod_oficina and fecha_proceso = (select decode(max(z.fecha_proceso), null, sysdate, max(z.fecha_proceso)) from procesooficina z where z.cod_oficina = x.cod_oficina)) EstOficina, 
                                         (select f.estado from caja f where x.cod_oficina = f.cod_oficina and y.cod_caja = f.cod_caja) estadoCaja " +
                                     "  From cajero y Inner Join usuarios x On y.cod_persona = x.codusuario Where x.codusuario = " + pUsuario.codusuario.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["cod_oficina"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["cod_oficina"]);
                            if (resultado["cod_caja"] != DBNull.Value) entidad.cod_caja = Convert.ToInt64(resultado["cod_caja"]);
                            if (resultado["cod_cajero"] != DBNull.Value) entidad.cod_cajero = Convert.ToString(resultado["cod_cajero"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nom_cajero = Convert.ToString(resultado["nombre"]);
                            if (resultado["esprincipal"] != DBNull.Value) entidad.conteo = Convert.ToInt64(resultado["esprincipal"]);
                            if (resultado["estcajero"] != DBNull.Value) entidad.estado = Convert.ToInt64(resultado["estcajero"]);
                            if (resultado["estadoCaja"] != DBNull.Value) entidad.estado_caja  = Convert.ToInt64(resultado["estadoCaja"]);
                            if (resultado["EstOficina"] != DBNull.Value) entidad.estado_ofi = Convert.ToInt64(resultado["EstOficina"]);// aqui se verifica que la oficina esta abierta o cerrada
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CajeroData", "ConsultarIfUserIsntCajeroPrincipal", ex);
                        return null;
                    }

                }
            }
        }


        /// <summary>
        /// Obtiene la lista de Cajeros de una Caja especifcia
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Cajeros obtenidas</returns>
        public List<Cajero> ListarCajeroXCaja(Cajero pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Cajero> lstCajero = new List<Cajero>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = " select cod_cajero,nombre from cajero a , usuarios b where b.codusuario=a.cod_persona and cod_caja=" + pEntidad.cod_caja + " order by cod_cajero asc";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Cajero entidad = new Cajero();
                            //Asociar todos los valores a la entidad

                            if (resultado["cod_cajero"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["cod_cajero"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nom_cajero = Convert.ToString(resultado["nombre"]);
                            lstCajero.Add(entidad);
                        }

                        return lstCajero;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CajeroData", "ListarCajeroXCaja", ex);
                        return null;
                    }
                }
            }
        }


        public Reintegro ConsultarFecha(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Reintegro entidad = new Reintegro();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT u.codusuario as codcajero, u.nombre nomcajero, u.cod_oficina codoficina, o.nombre nomoficina, (select decode(max(x.fecha_proceso), null, sysdate, max(x.fecha_proceso)) as fechaproceso from procesooficina x where x.cod_oficina = u.cod_oficina) fechaproceso FROM usuarios u, oficina o WHERE u.cod_oficina = o.cod_oficina And u.codusuario = " + pUsuario.codusuario.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["nomcajero"] != DBNull.Value) entidad.nomcajero = Convert.ToString(resultado["nomcajero"]);
                            if (resultado["codcajero"] != DBNull.Value) entidad.cod_cajero = Convert.ToInt64(resultado["codcajero"]);
                            if (resultado["codoficina"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["codoficina"]);
                            if (resultado["nomoficina"] != DBNull.Value) entidad.nomoficina = Convert.ToString(resultado["nomoficina"]);
                            if (resultado["fechaproceso"] != DBNull.Value) entidad.fechareintegro = Convert.ToDateTime(resultado["fechaproceso"]);
                            entidad.esprincipal = 1;
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReintegroData", "ConsultarCajero", ex);
                        return null;
                    }

                }

            }
        }



        public List<Cajero> ListarCajeroPorOficina(Cajero pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Cajero> lstCajero = new List<Cajero>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = " SELECT a.identificacion,a.cod_cajero codcajero, codusuario,(Select b.cod_caja from caja b where b.cod_caja=a.cod_caja and cod_oficina=" + pEntidad.cod_oficina.ToString() + " ) codcaja,a.fecha_creacion fechacreacion, decode(to_char(a.fecha_retiro,'dd/mm/yyyy'),'01/01/0001',null,to_char(a.fecha_retiro,'dd/mm/yyyy')) fecharetiro, decode(a.estado,1,'Activo','Inactivo') cajeroestado, (Select b.nombre from caja b where b.cod_caja=a.cod_caja) nomcaja, c.nombre nomusuario  FROM cajero a, usuarios c  where a.cod_persona=c.codusuario and c.cod_oficina=" + pEntidad.cod_oficina.ToString() + " Order By cod_cajero asc";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Cajero entidad = new Cajero();
                            //Asociar todos los valores a la entidad
                            if (resultado["codcajero"] != DBNull.Value) entidad.cod_cajero = Convert.ToString(resultado["codcajero"]);
                            if (resultado["codusuario"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["codusuario"]);
                            if (resultado["codcaja"] != DBNull.Value) entidad.cod_caja = Convert.ToInt64(resultado["codcaja"]);
                            if (resultado["fechacreacion"] != DBNull.Value) entidad.fecha_ingreso = Convert.ToDateTime(resultado["fechacreacion"]);

                            if (resultado["fecharetiro"] != DBNull.Value) entidad.fecharetiro = resultado["fecharetiro"].ToString();
                            else
                                entidad.fecharetiro = "";

                            if (resultado["cajeroestado"] != DBNull.Value) entidad.nomestado = resultado["cajeroestado"].ToString();
                            if (resultado["nomcaja"] != DBNull.Value) entidad.nom_caja = Convert.ToString(resultado["nomcaja"]);
                            if (resultado["nomusuario"] != DBNull.Value) entidad.nom_cajero = Convert.ToString(resultado["nomusuario"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);

                            lstCajero.Add(entidad);
                        }

                        return lstCajero;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CajeroData", "ListarCajero", ex);
                        return null;
                    }
                }
            }
        }


    }
}
