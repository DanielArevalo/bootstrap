using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Tesoreria.Entities;

namespace Xpinn.Tesoreria.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla SoporteCaj
    /// </summary>
    public class SoporteCajData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla SoporteCaj
        /// </summary>
        public SoporteCajData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla SoporteCaj de la base de datos
        /// </summary>
        /// <param name="pSoporteCaj">Entidad SoporteCaj</param>
        /// <returns>Entidad SoporteCaj creada</returns>
        public SoporteCaj CrearSoporteCaj(SoporteCaj pSoporteCaj, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidsoporte = cmdTransaccionFactory.CreateParameter();
                        pidsoporte.ParameterName = "p_idsoporte";
                        pidsoporte.Value = pSoporteCaj.idsoporte;
                        pidsoporte.Direction = ParameterDirection.Input;
                        pidsoporte.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidsoporte);

                        DbParameter pcod_per = cmdTransaccionFactory.CreateParameter();
                        pcod_per.ParameterName = "p_cod_per";
                        pcod_per.Value = pSoporteCaj.cod_per;
                        pcod_per.Direction = ParameterDirection.Input;
                        pcod_per.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_per);

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "p_fecha";
                        pfecha.Value = pSoporteCaj.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        pvalor.Value = pSoporteCaj.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pSoporteCaj.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pidtiposop = cmdTransaccionFactory.CreateParameter();
                        pidtiposop.ParameterName = "p_idtiposop";
                        pidtiposop.Value = pSoporteCaj.idtiposop;
                        pidtiposop.Direction = ParameterDirection.Input;
                        pidtiposop.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidtiposop);

                        DbParameter pnum_comp = cmdTransaccionFactory.CreateParameter();
                        pnum_comp.ParameterName = "p_num_comp";
                        pnum_comp.Value = pSoporteCaj.num_comp;
                        pnum_comp.Direction = ParameterDirection.Input;
                        pnum_comp.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnum_comp);

                        DbParameter ptipo_comp = cmdTransaccionFactory.CreateParameter();
                        ptipo_comp.ParameterName = "p_tipo_comp";                        
                        ptipo_comp.Value = pSoporteCaj.tipo_comp;
                        ptipo_comp.Direction = ParameterDirection.Input;
                        ptipo_comp.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_comp);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = pSoporteCaj.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter pcod_oficina = cmdTransaccionFactory.CreateParameter();
                        pcod_oficina.ParameterName = "p_cod_oficina";
                        pcod_oficina.Value = pSoporteCaj.cod_oficina;
                        pcod_oficina.Direction = ParameterDirection.Input;
                        pcod_oficina.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_oficina);

                        DbParameter pcod_usuario = cmdTransaccionFactory.CreateParameter();
                        pcod_usuario.ParameterName = "p_cod_usuario";
                        pcod_usuario.Value = pSoporteCaj.cod_usuario;
                        pcod_usuario.Direction = ParameterDirection.Input;
                        pcod_usuario.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_usuario);

                        DbParameter pidarea = cmdTransaccionFactory.CreateParameter();
                        pidarea.ParameterName = "p_idarea";
                        pidarea.Value = pSoporteCaj.idarea;
                        pidarea.Direction = ParameterDirection.Input;
                        pidarea.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidarea);
                        //Agregado para vales provisionales

                        DbParameter pvale = cmdTransaccionFactory.CreateParameter();
                        pvale.ParameterName = "p_vale";
                        pvale.Value = pSoporteCaj.vale_prov;
                        pvale.Direction = ParameterDirection.Input;
                        pvale.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pvale);

                        DbParameter parqueo = cmdTransaccionFactory.CreateParameter();
                        parqueo.ParameterName = "p_arqueo";
                        if(pSoporteCaj.id_arqueo == null)
                        {
                            parqueo.Value = DBNull.Value;
                        }
                        else
                        {
                            parqueo.Value = pSoporteCaj.id_arqueo;
                        }  
                        parqueo.Direction = ParameterDirection.Input;
                        parqueo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(parqueo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_SOPORTECAJ_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pSoporteCaj.idsoporte = Convert.ToInt64(pidsoporte.Value);

                        return pSoporteCaj;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SoporteCajData", "CrearSoporteCaj", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla SoporteCaj de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad SoporteCaj modificada</returns>
        public SoporteCaj ModificarSoporteCaj(SoporteCaj pSoporteCaj, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidsoporte = cmdTransaccionFactory.CreateParameter();
                        pidsoporte.ParameterName = "p_idsoporte";
                        pidsoporte.Value = pSoporteCaj.idsoporte;
                        pidsoporte.Direction = ParameterDirection.Input;
                        pidsoporte.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidsoporte);

                        DbParameter pcod_per = cmdTransaccionFactory.CreateParameter();
                        pcod_per.ParameterName = "p_cod_per";
                        pcod_per.Value = pSoporteCaj.cod_per;
                        pcod_per.Direction = ParameterDirection.Input;
                        pcod_per.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_per);

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "p_fecha";
                        pfecha.Value = pSoporteCaj.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        pvalor.Value = pSoporteCaj.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pSoporteCaj.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pidtiposop = cmdTransaccionFactory.CreateParameter();
                        pidtiposop.ParameterName = "p_idtiposop";
                        pidtiposop.Value = pSoporteCaj.idtiposop;
                        pidtiposop.Direction = ParameterDirection.Input;
                        pidtiposop.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidtiposop);

                        DbParameter pnum_comp = cmdTransaccionFactory.CreateParameter();
                        pnum_comp.ParameterName = "p_num_comp";
                        pnum_comp.Value = pSoporteCaj.num_comp;
                        pnum_comp.Direction = ParameterDirection.Input;
                        pnum_comp.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnum_comp);

                        DbParameter ptipo_comp = cmdTransaccionFactory.CreateParameter();
                        ptipo_comp.ParameterName = "p_tipo_comp";
                        ptipo_comp.Value = pSoporteCaj.tipo_comp;
                        ptipo_comp.Direction = ParameterDirection.Input;
                        ptipo_comp.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_comp);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = pSoporteCaj.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter pcod_oficina = cmdTransaccionFactory.CreateParameter();
                        pcod_oficina.ParameterName = "p_cod_oficina";
                        pcod_oficina.Value = pSoporteCaj.cod_oficina;
                        pcod_oficina.Direction = ParameterDirection.Input;
                        pcod_oficina.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_oficina);

                        DbParameter pcod_usuario = cmdTransaccionFactory.CreateParameter();
                        pcod_usuario.ParameterName = "p_cod_usuario";
                        pcod_usuario.Value = pSoporteCaj.cod_usuario;
                        pcod_usuario.Direction = ParameterDirection.Input;
                        pcod_usuario.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_usuario);

                        DbParameter pidarea = cmdTransaccionFactory.CreateParameter();
                        pidarea.ParameterName = "p_idarea";
                        pidarea.Value = pSoporteCaj.idarea;
                        pidarea.Direction = ParameterDirection.Input;
                        pidarea.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidarea);

                        DbParameter pvale = cmdTransaccionFactory.CreateParameter();
                        pvale.ParameterName = "p_vale";
                        pvale.Value = pSoporteCaj.vale_prov;
                        pvale.Direction = ParameterDirection.Input;
                        pvale.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pvale);

                        DbParameter parqueo = cmdTransaccionFactory.CreateParameter();
                        parqueo.ParameterName = "p_arqueo";
                        if (pSoporteCaj.id_arqueo == null)
                        {
                            parqueo.Value = DBNull.Value;
                        }
                        else
                        {
                            parqueo.Value = pSoporteCaj.id_arqueo;
                        }
                        parqueo.Direction = ParameterDirection.Input;
                        parqueo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(parqueo);

                        DbParameter pCodOpe = cmdTransaccionFactory.CreateParameter();
                        pCodOpe.ParameterName = "P_COD_OPE";
                        pCodOpe.Value = pSoporteCaj.cod_ope;
                        pCodOpe.Direction = ParameterDirection.Input;
                        pCodOpe.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pCodOpe);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_SOPORTECAJ_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pSoporteCaj;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SoporteCajData", "ModificarSoporteCaj", ex);
                        return null;
                    }
                }
            }
        }

        public SoporteCaj ModificarEstadoSoporteCaj(SoporteCaj pSoporteCaj, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidsoporte = cmdTransaccionFactory.CreateParameter();
                        pidsoporte.ParameterName = "p_idsoporte";
                        pidsoporte.Value = pSoporteCaj.idsoporte;
                        pidsoporte.Direction = ParameterDirection.Input;
                        pidsoporte.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidsoporte);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pSoporteCaj.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_SOPORTECAJ_MEST";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pSoporteCaj;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SoporteCajData", "ModificarSoporteCaj", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla SoporteCaj de la base de datos
        /// </summary>
        /// <param name="pId">identificador de SoporteCaj</param>
        public void EliminarSoporteCaj(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        SoporteCaj pSoporteCaj = new SoporteCaj();
                        pSoporteCaj = ConsultarSoporteCaj(pId, vUsuario);

                        DbParameter pidsoporte = cmdTransaccionFactory.CreateParameter();
                        pidsoporte.ParameterName = "p_idsoporte";
                        pidsoporte.Value = pSoporteCaj.idsoporte;
                        pidsoporte.Direction = ParameterDirection.Input;
                        pidsoporte.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidsoporte);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_SOPORTECAJ_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SoporteCajData", "EliminarSoporteCaj", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla SoporteCaj de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla SoporteCajS</param>
        /// <returns>Entidad SoporteCaj consultado</returns>
        public SoporteCaj ConsultarSoporteCaj(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            SoporteCaj entidad = new SoporteCaj();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT Soporte_Caj.*, 
                        Case Soporte_Caj.estado When '1' Then 'Elaborado' When '2' Then 'Contabilizado' When '3' Then 'Anulado' Else Soporte_Caj.estado End As nomestado,
                        Case Soporte_Caj.VALE_PROV When '0' Then 'Legalizado' When '1' Then 'No legalizado' When '2' Then 'No aplica' Else NULL End As nomvale,                
                        p.nombre, t.descripcion AS nomtiposop, tc.descripcion As nomtipo_comp, o.nombre As nomoficina, u.nombre As nomusuario, a.nombre As nomarea  
                        FROM Soporte_Caj LEFT JOIN v_persona p ON Soporte_Caj.cod_per = p.cod_persona 
                        LEFT JOIN tip_sop_caj t ON Soporte_Caj.idtiposop = t.idtiposop  
                        LEFT JOIN tipo_comp tc ON Soporte_Caj.tipo_comp = tc.tipo_comp
                        LEFT JOIN oficina o ON Soporte_Caj.cod_oficina = o.cod_oficina
                        LEFT JOIN usuarios u ON Soporte_Caj.cod_usuario = u.codusuario
                        LEFT JOIN areas_caj a ON Soporte_Caj.idarea = a.idarea
                        WHERE Soporte_Caj.idsoporte = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDSOPORTE"] != DBNull.Value) entidad.idsoporte = Convert.ToInt64(resultado["IDSOPORTE"]);
                            if (resultado["COD_PER"] != DBNull.Value) entidad.cod_per = Convert.ToInt64(resultado["COD_PER"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["NOMESTADO"] != DBNull.Value) entidad.nomestado = Convert.ToString(resultado["NOMESTADO"]);
                            if (resultado["IDTIPOSOP"] != DBNull.Value) entidad.idtiposop = Convert.ToInt32(resultado["IDTIPOSOP"]);
                            if (resultado["NOMTIPOSOP"] != DBNull.Value) entidad.nomtiposop = Convert.ToString(resultado["NOMTIPOSOP"]);
                            if (resultado["NUM_COMP"] != DBNull.Value) entidad.num_comp = Convert.ToInt32(resultado["NUM_COMP"]);
                            if (resultado["TIPO_COMP"] != DBNull.Value) entidad.tipo_comp = Convert.ToInt32(resultado["TIPO_COMP"]);
                            if (resultado["NOMTIPO_COMP"] != DBNull.Value) entidad.nomtipo_comp = Convert.ToString(resultado["NOMTIPO_COMP"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["COD_OFICINA"]);
                            if (resultado["NOMOFICINA"] != DBNull.Value) entidad.nomoficina = Convert.ToString(resultado["NOMOFICINA"]);
                            if (resultado["COD_USUARIO"] != DBNull.Value) entidad.cod_usuario = Convert.ToInt32(resultado["COD_USUARIO"]);
                            if (resultado["NOMUSUARIO"] != DBNull.Value) entidad.nomusuario = Convert.ToString(resultado["NOMUSUARIO"]);
                            if (resultado["IDAREA"] != DBNull.Value) entidad.idarea = Convert.ToInt32(resultado["IDAREA"]);
                            if (resultado["NOMAREA"] != DBNull.Value) entidad.nomarea = Convert.ToString(resultado["NOMAREA"]);
                            if (resultado["VALE_PROV"] != DBNull.Value) entidad.vale_prov = Convert.ToString(resultado["VALE_PROV"]);
                            if (resultado["NOMVALE"] != DBNull.Value) entidad.nomvale = Convert.ToString(resultado["NOMVALE"]);
                            if (resultado["ID_ARQUEO"] != DBNull.Value) entidad.id_arqueo = Convert.ToInt64(resultado["ID_ARQUEO"]);
                            if (resultado["COD_OPE"] != DBNull.Value) entidad.cod_ope = Convert.ToInt64(resultado["COD_OPE"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SoporteCajData", "ConsultarSoporteCaj", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla SoporteCaj dados unos filtros
        /// </summary>
        /// <param name="pSoporteCajS">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de SoporteCaj obtenidos</returns>
        public List<SoporteCaj> ListarSoporteCaj(SoporteCaj pSoporteCaj, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<SoporteCaj> lstSoporteCaj = new List<SoporteCaj>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT Soporte_Caj.*, Case Soporte_Caj.estado When '1' Then 'Elaborado' When '2' Then 'Contabilizado' When '3' Then 'Anulado' Else Soporte_Caj.estado End As nomestado,
                                        Case Soporte_Caj.VALE_PROV When '0' Then 'Legalizado' When '1' Then 'No legalizado' When '2' Then 'No aplica' Else null End As nomvale,                                        
                                        p.nombre, t.descripcion AS nomtiposop, tc.descripcion As nomtipo_comp, o.nombre As nomoficina, u.nombre As nomusuario, a.nombre As nomarea, t.cod_cuenta  
                                        FROM Soporte_Caj LEFT JOIN v_persona p ON Soporte_Caj.cod_per = p.cod_persona 
                                        LEFT JOIN tip_sop_caj t ON Soporte_Caj.idtiposop = t.idtiposop  
                                        LEFT JOIN tipo_comp tc ON Soporte_Caj.tipo_comp = tc.tipo_comp
                                        LEFT JOIN oficina o ON Soporte_Caj.cod_oficina = o.cod_oficina
                                        LEFT JOIN usuarios u ON Soporte_Caj.cod_usuario = u.codusuario
                                        LEFT JOIN areas_caj a ON Soporte_Caj.idarea = a.idarea
                                        " + ObtenerFiltro(pSoporteCaj, "Soporte_Caj.") + " ORDER BY Soporte_Caj.IDSOPORTE ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            SoporteCaj entidad = new SoporteCaj();
                            if (resultado["IDSOPORTE"] != DBNull.Value) entidad.idsoporte = Convert.ToInt64(resultado["IDSOPORTE"]);
                            if (resultado["COD_PER"] != DBNull.Value) entidad.cod_per = Convert.ToInt64(resultado["COD_PER"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["NOMESTADO"] != DBNull.Value) entidad.nomestado = Convert.ToString(resultado["NOMESTADO"]);
                            if (resultado["IDTIPOSOP"] != DBNull.Value) entidad.idtiposop = Convert.ToInt32(resultado["IDTIPOSOP"]);
                            if (resultado["NOMTIPOSOP"] != DBNull.Value) entidad.nomtiposop = Convert.ToString(resultado["NOMTIPOSOP"]);
                            if (resultado["NUM_COMP"] != DBNull.Value) entidad.num_comp = Convert.ToInt32(resultado["NUM_COMP"]);
                            if (resultado["TIPO_COMP"] != DBNull.Value) entidad.tipo_comp = Convert.ToInt32(resultado["TIPO_COMP"]);
                            if (resultado["NOMTIPO_COMP"] != DBNull.Value) entidad.nomtipo_comp = Convert.ToString(resultado["NOMTIPO_COMP"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["COD_OFICINA"]);
                            if (resultado["NOMOFICINA"] != DBNull.Value) entidad.nomoficina = Convert.ToString(resultado["NOMOFICINA"]);
                            if (resultado["COD_USUARIO"] != DBNull.Value) entidad.cod_usuario = Convert.ToInt32(resultado["COD_USUARIO"]);
                            if (resultado["NOMUSUARIO"] != DBNull.Value) entidad.nomusuario = Convert.ToString(resultado["NOMUSUARIO"]);
                            if (resultado["IDAREA"] != DBNull.Value) entidad.idarea = Convert.ToInt32(resultado["IDAREA"]);
                            if (resultado["NOMAREA"] != DBNull.Value) entidad.nomarea = Convert.ToString(resultado["NOMAREA"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["VALE_PROV"] != DBNull.Value) entidad.vale_prov = Convert.ToString(resultado["VALE_PROV"]);
                            if (resultado["NOMVALE"] != DBNull.Value) entidad.nomvale = Convert.ToString(resultado["NOMVALE"]);
                            if (resultado["COD_OPE"] != DBNull.Value) entidad.cod_ope = Convert.ToInt64(resultado["COD_OPE"]);
                            lstSoporteCaj.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstSoporteCaj;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SoporteCajData", "ListarSoporteCaj", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene el siguiente codigo disponible de la tabla
        /// </summary>
        /// <returns>codigo disponible</returns>
        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            Int64 resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT MAX(idsoporte) + 1 FROM Soporte_Caj ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = Convert.ToInt64(cmdTransaccionFactory.ExecuteScalar());

                        return resultado;
                    }
                    catch
                    {
                        return 0;
                    }
                }
            }
        }


        public List<SoporteCaj> ConsultarSoporteArqueo(Int64 idarea, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<SoporteCaj> lstsoporte = new List<SoporteCaj>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select IDSOPORTE from soporte_caj where id_arqueo is null and idarea = " + idarea;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            SoporteCaj entidad = new SoporteCaj();
                            if (resultado["IDSOPORTE"] != DBNull.Value) entidad.idsoporte = Convert.ToInt64(resultado["IDSOPORTE"]);
                            lstsoporte.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstsoporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SoporteCajData", "ConsultarSoporteArqueo", ex);
                        return null;
                    }
                }
            }
        }
        public SoporteCaj ActualizarSoporteArqueo(SoporteCaj vSoporteCaj, Int64? id_arqueo, Usuario vUsuario)
        {
           using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        SoporteCaj pSoporteCaj = new SoporteCaj();
                        DbParameter pidsoporte = cmdTransaccionFactory.CreateParameter();
                        pidsoporte.ParameterName = "p_idsoporte";
                        pidsoporte.Value = vSoporteCaj.idsoporte;
                        pidsoporte.Direction = ParameterDirection.Input;
                        pidsoporte.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidsoporte);

                        DbParameter pidarqueo = cmdTransaccionFactory.CreateParameter();
                        pidarqueo.ParameterName = "p_id_arqueo";
                        pidarqueo.Value = id_arqueo;
                        pidarqueo.Direction = ParameterDirection.Input;
                        pidarqueo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidarqueo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_SOPORTECAJ_ARQ";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pSoporteCaj;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SoporteCajData", "ActualizarSoporteArqueo", ex);
                        return null;
                    }
                }
            }
        }
    }
}