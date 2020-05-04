using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Caja.Entities;

namespace Xpinn.Caja.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla ProcesoOficina
    /// </summary>
    public class ProcesoOficinaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para ProcesoOficina
        /// </summary>
        public ProcesoOficinaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

         /// <summary>
        /// Crea una entidad procesoOficina en la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad ProcesoOficina</param>
        /// <returns>Entidad creada</returns>
        public ProcesoOficina InsertarProcesoOficina(ProcesoOficina pEntidad, ref string pError, Usuario pUsuario)
        {
            pError = "";
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                            
                        DbParameter pcod_oficina = cmdTransaccionFactory.CreateParameter();
                        pcod_oficina.ParameterName = "pcodigooficina";
                        pcod_oficina.Value = pEntidad.cod_oficina;
                        pcod_oficina.DbType = DbType.Int16;
                        pcod_oficina.Size = 8;
                        pcod_oficina.Direction = ParameterDirection.Input;
                        
                        DbParameter pcod_encargado = cmdTransaccionFactory.CreateParameter();
                        pcod_encargado.ParameterName = "pcodigoencargado";
                        pcod_encargado.Value = pEntidad.cod_usuario;
                        pcod_encargado.Size = 8;
                        pcod_encargado.DbType = DbType.Int16;
                        pcod_encargado.Direction = ParameterDirection.Input;

                        DbParameter pfecha_proceso = cmdTransaccionFactory.CreateParameter();
                        pfecha_proceso.ParameterName = "pfechaproceso";
                        pfecha_proceso.Value = pEntidad.fecha_proceso;
                        pfecha_proceso.DbType = DbType.DateTime;
                        pfecha_proceso.Direction = ParameterDirection.Input;
                        pfecha_proceso.Size = 7;

                        DbParameter ptipo_proceso = cmdTransaccionFactory.CreateParameter();
                        ptipo_proceso.ParameterName = "ptipoproceso";
                        ptipo_proceso.Value = pEntidad.tipo_proceso;
                        ptipo_proceso.DbType = DbType.Int16;
                        ptipo_proceso.Direction = ParameterDirection.Input;
                        ptipo_proceso.Size = 8;

                        DbParameter ptipo_horario = cmdTransaccionFactory.CreateParameter();
                        ptipo_horario.ParameterName = "ptipohorario";
                        ptipo_horario.Value = pEntidad.tipo_horario;
                        ptipo_horario.DbType = DbType.Int16;
                        ptipo_horario.Direction = ParameterDirection.Input;
                        ptipo_horario.Size = 8;

                        cmdTransaccionFactory.Parameters.Add(pcod_oficina);
                        cmdTransaccionFactory.Parameters.Add(pcod_encargado);
                        cmdTransaccionFactory.Parameters.Add(pfecha_proceso);
                        cmdTransaccionFactory.Parameters.Add(ptipo_proceso);
                        cmdTransaccionFactory.Parameters.Add(ptipo_horario);
                        
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_PROCOFIINSERTAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //se actualiza el estado de la oficina dependiendo de la apertura o cierre de la oficina
                        int estadoOp = 0;
                        if (pEntidad.tipo_proceso == 1)//Apertura
                            estadoOp = 1;
                        else //Cierre de Oficina
                            estadoOp = 0;

                        cmdTransaccionFactory.Parameters.Clear();                        

                        DbParameter pcode_oficina = cmdTransaccionFactory.CreateParameter();
                        pcode_oficina.ParameterName = "pcodigooficina";
                        pcode_oficina.Value = pEntidad.cod_oficina;
                        pcode_oficina.DbType = DbType.Int16;
                        pcode_oficina.Size = 8;
                        pcode_oficina.Direction = ParameterDirection.Input;

                        DbParameter pcode_estado = cmdTransaccionFactory.CreateParameter();
                        pcode_estado.ParameterName = "pestado";
                        pcode_estado.Value = estadoOp;
                        pcode_estado.DbType = DbType.Int16;
                        pcode_estado.Size = 8;
                        pcode_estado.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(pcode_oficina);
                        cmdTransaccionFactory.Parameters.Add(pcode_estado);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPN_CAJAFIN_PROCOFI_U";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        List<Xpinn.Caja.Entities.Caja> lstCaja = new List<Xpinn.Caja.Entities.Caja>();
                        Xpinn.Caja.Entities.Caja caja = new Xpinn.Caja.Entities.Caja();
                    
                        caja.cod_oficina = pEntidad.cod_oficina;
                        lstCaja = ListarDatosCaja(caja,pUsuario);

                        foreach (Caja.Entities.Caja fil in lstCaja)
                        {
                            cmdTransaccionFactory.Parameters.Clear();
                            DbParameter pcod_caja = cmdTransaccionFactory.CreateParameter();
                            pcod_caja.ParameterName = "pcodigocaja";
                            pcod_caja.Value = fil.cod_caja;

                            DbParameter pes_principal = cmdTransaccionFactory.CreateParameter();
                            pes_principal.ParameterName = "pesprincipal";
                            pes_principal.Value = fil.esprincipal;

                            DbParameter pfec_proceso = cmdTransaccionFactory.CreateParameter();
                            pfec_proceso.ParameterName = "pfechaproceso";
                            pfec_proceso.Value = pEntidad.fecha_proceso;

                            cmdTransaccionFactory.Parameters.Add(pcod_caja);
                            cmdTransaccionFactory.Parameters.Add(pes_principal);
                            cmdTransaccionFactory.Parameters.Add(pfec_proceso);

                            cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                            cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_ASIG_SALCAJ";
                            cmdTransaccionFactory.ExecuteNonQuery();
    
                        }

                        return pEntidad;
                    }
                    catch (Exception ex)
                    {                        
                        pError = ex.Message;
                        return null;
                    }
                
                }
            }
        }

        /// <summary>
        /// Obtiene un registro de la tabla Oficina de la base de datos relacionado
        /// ciudad, responsable
        /// </summary>
        /// <param name="pId">identificador del registro</param>
        /// <returns>Oficina consultada</returns>
        public ProcesoOficina ConsultarXProcesoOficina(ProcesoOficina pProcesoOficina, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            ProcesoOficina entidad = new ProcesoOficina();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * from procesooficina where fecha_proceso in(select max(fecha_proceso) from procesooficina where cod_oficina=" + pProcesoOficina.cod_oficina.ToString() + " and cod_usuario=" + pProcesoOficina.cod_usuario.ToString() + " and to_char(fecha_proceso,'dd/mm/yyyy')='" + pProcesoOficina.fecha_proceso.ToShortDateString() + "' ) ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            //Asociar todos los valores a la entidad
                            if (resultado["tipo_horario"] != DBNull.Value) entidad.tipo_horario = Convert.ToInt64(resultado["tipo_horario"]);
                            if (resultado["cod_oficina"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["cod_oficina"]);
                            if (resultado["cod_usuario"] != DBNull.Value) entidad.cod_usuario = Convert.ToInt64(resultado["cod_usuario"]);
                            if (resultado["fecha_proceso"] != DBNull.Value) entidad.fecha_proceso = Convert.ToDateTime(resultado["fecha_proceso"]);
                        }
                        else
                        {
                            entidad.fecha_proceso = DateTime.Now;
                            entidad.tipo_horario = 1;
                            entidad.cod_oficina = pProcesoOficina.cod_oficina;
                            entidad.cod_usuario = pProcesoOficina.cod_usuario;
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProcesoOficinaData", "ConsultarXProcesoOficina", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro de la tabla Oficina de la base de datos relacionado
        /// ciudad, responsable
        /// </summary>
        /// <param name="pId">identificador del registro</param>
        /// <returns>Oficina consultada</returns>
        public ProcesoOficina ConsultarUsuarioAperturo(ProcesoOficina pProcesoOficina, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            ProcesoOficina entidad = new ProcesoOficina();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                      //  string sql = "select *  from procesooficina a  inner join usuarios u  on u.codusuario=a.cod_usuario where a.fecha_proceso in(select max(c.fecha_proceso) from procesooficina  c where  c.tipo_proceso=1  and c.cod_oficina=" + pProcesoOficina.cod_oficina.ToString() + " and to_char(c.fecha_proceso,'dd/mm/yyyy')='" + pProcesoOficina.fecha_proceso.ToShortDateString() + "' ) ";
                        string sql = "select *  from procesooficina a inner join cajero c on c.cod_cajero=a.cod_usuario inner join usuarios u  on u.codusuario=c.cod_persona where a.fecha_proceso in(select max(c.fecha_proceso) from procesooficina  c where  c.tipo_proceso=1  and c.cod_oficina=" + pProcesoOficina.cod_oficina.ToString() + " and to_char(c.fecha_proceso,'dd/mm/yyyy')='" + pProcesoOficina.fecha_proceso.ToShortDateString() + "' ) ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            //Asociar todos los valores a la entidad
                            if (resultado["tipo_horario"] != DBNull.Value) entidad.tipo_horario = Convert.ToInt64(resultado["tipo_horario"]);
                            if (resultado["cod_oficina"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["cod_oficina"]);
                            if (resultado["cod_usuario"] != DBNull.Value) entidad.cod_usuario = Convert.ToInt64(resultado["cod_usuario"]);
                            if (resultado["fecha_proceso"] != DBNull.Value) entidad.fecha_proceso = Convert.ToDateTime(resultado["fecha_proceso"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.usuario_aperturo = Convert.ToString(resultado["nombre"]);
                        }
                        //else
                        //{
                        //    entidad.fecha_proceso = DateTime.Now;
                        //    entidad.tipo_horario = 1;
                        //    entidad.cod_oficina = pProcesoOficina.cod_oficina;
                        //    entidad.cod_usuario = pProcesoOficina.cod_usuario;
                        //}

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProcesoOficinaData", "ConsultarUsuarioAperturo", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades 
        /// </summary>
        /// <param name="">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto obtenidos</returns>
        public List<Caja.Entities.Caja> ListarDatosCaja(Caja.Entities.Caja pCaja, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Caja.Entities.Caja> lstCaja = new List<Caja.Entities.Caja>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = " select c.cod_caja codcaja,c.esprincipal esprincip " +
                                     " from Caja c " +
                                     " where c.cod_oficina=" + pCaja.cod_oficina;


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Caja.Entities.Caja entidad = new Caja.Entities.Caja();

                            if (resultado["codcaja"] != DBNull.Value) entidad.cod_caja = Convert.ToString(resultado["codcaja"]);
                            if (resultado["esprincip"] != DBNull.Value) entidad.esprincipal = Convert.ToInt64(resultado["esprincip"]);

                            lstCaja.Add(entidad);
                        }

                        return lstCaja;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProcesoOficinaData", "ListarDatosCaja", ex);
                        return null;
                    }
                }
            }
        }
    }
}
