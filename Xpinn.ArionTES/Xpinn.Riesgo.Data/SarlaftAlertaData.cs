using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Riesgo.Entities;

namespace Xpinn.Riesgo.Data
{
    public class SarlaftAlertaData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public SarlaftAlertaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public SarlaftAlerta CrearSarlaftAlerta(SarlaftAlerta pSarlaftAlerta, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidalerta = cmdTransaccionFactory.CreateParameter();
                        pidalerta.ParameterName = "p_idalerta";
                        pidalerta.Value = pSarlaftAlerta.idalerta;
                        pidalerta.Direction = ParameterDirection.Input;
                        pidalerta.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidalerta);

                        DbParameter pfecha_alerta = cmdTransaccionFactory.CreateParameter();
                        pfecha_alerta.ParameterName = "p_fecha_alerta";
                        pfecha_alerta.Value = pSarlaftAlerta.fecha_alerta;
                        pfecha_alerta.Direction = ParameterDirection.Input;
                        pfecha_alerta.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_alerta);

                        DbParameter pcod_usuario = cmdTransaccionFactory.CreateParameter();
                        pcod_usuario.ParameterName = "p_cod_usuario";
                        pcod_usuario.Value = pSarlaftAlerta.cod_usuario;
                        pcod_usuario.Direction = ParameterDirection.Input;
                        pcod_usuario.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_usuario);

                        DbParameter ptipo_alerta = cmdTransaccionFactory.CreateParameter();
                        ptipo_alerta.ParameterName = "p_tipo_alerta";
                        ptipo_alerta.Value = pSarlaftAlerta.tipo_alerta;
                        ptipo_alerta.Direction = ParameterDirection.Input;
                        ptipo_alerta.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_alerta);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        if (pSarlaftAlerta.cod_persona == null)
                            pcod_persona.Value = DBNull.Value;
                        else
                            pcod_persona.Value = pSarlaftAlerta.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter ptipo_producto = cmdTransaccionFactory.CreateParameter();
                        ptipo_producto.ParameterName = "p_tipo_producto";
                        if (pSarlaftAlerta.tipo_producto == null)
                            ptipo_producto.Value = DBNull.Value;
                        else
                            ptipo_producto.Value = pSarlaftAlerta.tipo_producto;
                        ptipo_producto.Direction = ParameterDirection.Input;
                        ptipo_producto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_producto);

                        DbParameter pnumero_producto = cmdTransaccionFactory.CreateParameter();
                        pnumero_producto.ParameterName = "p_numero_producto";
                        if (pSarlaftAlerta.numero_producto == null)
                            pnumero_producto.Value = DBNull.Value;
                        else
                            pnumero_producto.Value = pSarlaftAlerta.numero_producto;
                        pnumero_producto.Direction = ParameterDirection.Input;
                        pnumero_producto.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumero_producto);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        if (pSarlaftAlerta.descripcion == null)
                            pdescripcion.Value = DBNull.Value;
                        else
                            pdescripcion.Value = pSarlaftAlerta.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter pconsulta = cmdTransaccionFactory.CreateParameter();
                        pconsulta.ParameterName = "p_consulta";
                        if (pSarlaftAlerta.consulta == null)
                            pconsulta.Value = DBNull.Value;
                        else
                            pconsulta.Value = pSarlaftAlerta.consulta;
                        pconsulta.Direction = ParameterDirection.Input;
                        pconsulta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pconsulta);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        if (pSarlaftAlerta.estado == null)
                            pestado.Value = DBNull.Value;
                        else
                            pestado.Value = pSarlaftAlerta.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pfechacrea = cmdTransaccionFactory.CreateParameter();
                        pfechacrea.ParameterName = "p_fechacrea";
                        if (pSarlaftAlerta.fechacrea == null)
                            pfechacrea.Value = DBNull.Value;
                        else
                            pfechacrea.Value = pSarlaftAlerta.fechacrea;
                        pfechacrea.Direction = ParameterDirection.Input;
                        pfechacrea.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechacrea);

                        DbParameter pfechaultmod = cmdTransaccionFactory.CreateParameter();
                        pfechaultmod.ParameterName = "p_fechaultmod";
                        if (pSarlaftAlerta.fechaultmod == null)
                            pfechaultmod.Value = DBNull.Value;
                        else
                            pfechaultmod.Value = pSarlaftAlerta.fechaultmod;
                        pfechaultmod.Direction = ParameterDirection.Input;
                        pfechaultmod.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechaultmod);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_SARLAFT_AL_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pSarlaftAlerta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SarlaftAlertaData", "CrearSarlaftAlerta", ex);
                        return null;
                    }
                }
            }
        }


        public SarlaftAlerta ModificarSarlaftAlerta(SarlaftAlerta pSarlaftAlerta, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidalerta = cmdTransaccionFactory.CreateParameter();
                        pidalerta.ParameterName = "p_idalerta";
                        pidalerta.Value = pSarlaftAlerta.idalerta;
                        pidalerta.Direction = ParameterDirection.Input;
                        pidalerta.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidalerta);

                        DbParameter pfecha_alerta = cmdTransaccionFactory.CreateParameter();
                        pfecha_alerta.ParameterName = "p_fecha_alerta";
                        pfecha_alerta.Value = pSarlaftAlerta.fecha_alerta;
                        pfecha_alerta.Direction = ParameterDirection.Input;
                        pfecha_alerta.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_alerta);

                        DbParameter pcod_usuario = cmdTransaccionFactory.CreateParameter();
                        pcod_usuario.ParameterName = "p_cod_usuario";
                        pcod_usuario.Value = pSarlaftAlerta.cod_usuario;
                        pcod_usuario.Direction = ParameterDirection.Input;
                        pcod_usuario.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_usuario);

                        DbParameter ptipo_alerta = cmdTransaccionFactory.CreateParameter();
                        ptipo_alerta.ParameterName = "p_tipo_alerta";
                        ptipo_alerta.Value = pSarlaftAlerta.tipo_alerta;
                        ptipo_alerta.Direction = ParameterDirection.Input;
                        ptipo_alerta.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_alerta);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        if (pSarlaftAlerta.cod_persona == null)
                            pcod_persona.Value = DBNull.Value;
                        else
                            pcod_persona.Value = pSarlaftAlerta.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter ptipo_producto = cmdTransaccionFactory.CreateParameter();
                        ptipo_producto.ParameterName = "p_tipo_producto";
                        if (pSarlaftAlerta.tipo_producto == null)
                            ptipo_producto.Value = DBNull.Value;
                        else
                            ptipo_producto.Value = pSarlaftAlerta.tipo_producto;
                        ptipo_producto.Direction = ParameterDirection.Input;
                        ptipo_producto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_producto);

                        DbParameter pnumero_producto = cmdTransaccionFactory.CreateParameter();
                        pnumero_producto.ParameterName = "p_numero_producto";
                        if (pSarlaftAlerta.numero_producto == null)
                            pnumero_producto.Value = DBNull.Value;
                        else
                            pnumero_producto.Value = pSarlaftAlerta.numero_producto;
                        pnumero_producto.Direction = ParameterDirection.Input;
                        pnumero_producto.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumero_producto);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        if (pSarlaftAlerta.descripcion == null)
                            pdescripcion.Value = DBNull.Value;
                        else
                            pdescripcion.Value = pSarlaftAlerta.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter pconsulta = cmdTransaccionFactory.CreateParameter();
                        pconsulta.ParameterName = "p_consulta";
                        if (pSarlaftAlerta.consulta == null)
                            pconsulta.Value = DBNull.Value;
                        else
                            pconsulta.Value = pSarlaftAlerta.consulta;
                        pconsulta.Direction = ParameterDirection.Input;
                        pconsulta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pconsulta);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        if (pSarlaftAlerta.estado == null)
                            pestado.Value = DBNull.Value;
                        else
                            pestado.Value = pSarlaftAlerta.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pfechacrea = cmdTransaccionFactory.CreateParameter();
                        pfechacrea.ParameterName = "p_fechacrea";
                        if (pSarlaftAlerta.fechacrea == null)
                            pfechacrea.Value = DBNull.Value;
                        else
                            pfechacrea.Value = pSarlaftAlerta.fechacrea;
                        pfechacrea.Direction = ParameterDirection.Input;
                        pfechacrea.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechacrea);

                        DbParameter pfechaultmod = cmdTransaccionFactory.CreateParameter();
                        pfechaultmod.ParameterName = "p_fechaultmod";
                        if (pSarlaftAlerta.fechaultmod == null)
                            pfechaultmod.Value = DBNull.Value;
                        else
                            pfechaultmod.Value = pSarlaftAlerta.fechaultmod;
                        pfechaultmod.Direction = ParameterDirection.Input;
                        pfechaultmod.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechaultmod);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_SARLAFT_AL_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pSarlaftAlerta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SarlaftAlertaData", "ModificarSarlaftAlerta", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarSarlaftAlerta(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        SarlaftAlerta pSarlaftAlerta = new SarlaftAlerta();
                        pSarlaftAlerta = ConsultarSarlaftAlerta(pId, vUsuario);

                        DbParameter pidalerta = cmdTransaccionFactory.CreateParameter();
                        pidalerta.ParameterName = "p_idalerta";
                        pidalerta.Value = pSarlaftAlerta.idalerta;
                        pidalerta.Direction = ParameterDirection.Input;
                        pidalerta.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidalerta);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_SARLAFT_AL_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SarlaftAlertaData", "EliminarSarlaftAlerta", ex);
                    }
                }
            }
        }


        public SarlaftAlerta ConsultarSarlaftAlerta(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            SarlaftAlerta entidad = new SarlaftAlerta();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM SARLAFT_ALERTA WHERE IDALERTA = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDALERTA"] != DBNull.Value) entidad.idalerta = Convert.ToInt32(resultado["IDALERTA"]);
                            if (resultado["FECHA_ALERTA"] != DBNull.Value) entidad.fecha_alerta = Convert.ToDateTime(resultado["FECHA_ALERTA"]);
                            if (resultado["COD_USUARIO"] != DBNull.Value) entidad.cod_usuario = Convert.ToInt32(resultado["COD_USUARIO"]);
                            if (resultado["TIPO_ALERTA"] != DBNull.Value) entidad.tipo_alerta = Convert.ToInt32(resultado["TIPO_ALERTA"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt32(resultado["COD_PERSONA"]);
                            if (resultado["TIPO_PRODUCTO"] != DBNull.Value) entidad.tipo_producto = Convert.ToInt32(resultado["TIPO_PRODUCTO"]);
                            if (resultado["NUMERO_PRODUCTO"] != DBNull.Value) entidad.numero_producto = Convert.ToString(resultado["NUMERO_PRODUCTO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["CONSULTA"] != DBNull.Value) entidad.consulta = Convert.ToString(resultado["CONSULTA"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["FECHACREA"] != DBNull.Value) entidad.fechacrea = Convert.ToDateTime(resultado["FECHACREA"]);
                            if (resultado["FECHAULTMOD"] != DBNull.Value) entidad.fechaultmod = Convert.ToDateTime(resultado["FECHAULTMOD"]);
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
                        BOExcepcion.Throw("SarlaftAlertaData", "ConsultarSarlaftAlerta", ex);
                        return null;
                    }
                }
            }
        }


        public List<SarlaftAlerta> ListarSarlaftAlerta(SarlaftAlerta pSarlaftAlerta, DateTime? pFecIni, DateTime? pFecFin, int pOrden, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<SarlaftAlerta> lstSarlaftAlerta = new List<SarlaftAlerta>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"SELECT SARLAFT_ALERTA.*, P.IDENTIFICACION, P.NOMBRE, CASE SARLAFT_ALERTA.ESTADO WHEN 'P' THEN 'PENDIENTE' WHEN 'G' THEN 'GESTIONADA' ELSE NULL END AS NOM_ESTADO FROM SARLAFT_ALERTA LEFT JOIN V_PERSONA P ON SARLAFT_ALERTA.COD_PERSONA = P.COD_PERSONA " + ObtenerFiltro(pSarlaftAlerta);
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                        {
                            sql += pFecIni == null ? " " : (!sql.ToUpper().Contains("WHERE") ? " WHERE " : " ") + " TRUNC(SARLAFT_ALERTA.FECHA_ALERTA) >= TO_DATE('" + Convert.ToDateTime(pFecIni).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') ";
                            sql += pFecFin == null ? " " : (!sql.ToUpper().Contains("WHERE") ? " WHERE " : " AND") + " TRUNC(SARLAFT_ALERTA.FECHA_ALERTA) <= TO_DATE('" + Convert.ToDateTime(pFecFin).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') ";
                        }
                        else
                        {
                            sql += pFecIni == null ? " " : (!sql.ToUpper().Contains("WHERE") ? " WHERE " : " ") + " SARLAFT_ALERTA.FECHA_ALERTA >= '" + Convert.ToDateTime(pFecIni).ToString(conf.ObtenerFormatoFecha()) + "' ";
                            sql += pFecFin == null ? " " : (!sql.ToUpper().Contains("WHERE") ? " WHERE " : " AND") + " SARLAFT_ALERTA.FECHA_ALERTA <= '" + Convert.ToDateTime(pFecFin).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }
                        if (pOrden == 0)
                            sql += " ORDER BY P.IDENTIFICACION ";
                        else if (pOrden == 1)
                            sql += " ORDER BY P.NOMBRE ";
                        else if (pOrden == 2)
                            sql += " ORDER BY SARLAFT_ALERTA.FECHA_ALERTA ";
                        else
                            sql += " ORDER BY SARLAFT_ALERTA.IDALERTA ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            SarlaftAlerta entidad = new SarlaftAlerta();
                            if (resultado["IDALERTA"] != DBNull.Value) entidad.idalerta = Convert.ToInt32(resultado["IDALERTA"]);
                            if (resultado["FECHA_ALERTA"] != DBNull.Value) entidad.fecha_alerta = Convert.ToDateTime(resultado["FECHA_ALERTA"]);
                            if (resultado["COD_USUARIO"] != DBNull.Value) entidad.cod_usuario = Convert.ToInt32(resultado["COD_USUARIO"]);
                            if (resultado["TIPO_ALERTA"] != DBNull.Value) entidad.tipo_alerta = Convert.ToInt32(resultado["TIPO_ALERTA"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt32(resultado["COD_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["TIPO_PRODUCTO"] != DBNull.Value) entidad.tipo_producto = Convert.ToInt32(resultado["TIPO_PRODUCTO"]);
                            if (resultado["NUMERO_PRODUCTO"] != DBNull.Value) entidad.numero_producto = Convert.ToString(resultado["NUMERO_PRODUCTO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["CONSULTA"] != DBNull.Value) entidad.consulta = Convert.ToString(resultado["CONSULTA"]);
                            if (resultado["NOM_ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["NOM_ESTADO"]);
                            if (resultado["FECHACREA"] != DBNull.Value) entidad.fechacrea = Convert.ToDateTime(resultado["FECHACREA"]);
                            if (resultado["FECHAULTMOD"] != DBNull.Value) entidad.fechaultmod = Convert.ToDateTime(resultado["FECHAULTMOD"]);
                            lstSarlaftAlerta.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstSarlaftAlerta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SarlaftAlertaData", "ListarSarlaftAlerta", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Creación de registro de las consultas a listas restrictivas por persona
        /// </summary>
        /// <param name="pConsulta">Datos de la consulta realizada</param>
        /// <param name="pusuario"></param>
        public Consulta CrearRegistroConsultaLista(Consulta pConsulta, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_consulta = cmdTransaccionFactory.CreateParameter();
                        p_cod_consulta.ParameterName = "p_cod_consulta";
                        p_cod_consulta.Value = pConsulta.cod_consulta;
                        p_cod_consulta.Direction = ParameterDirection.Output;
                        p_cod_consulta.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_consulta);

                        DbParameter p_cod_persona = cmdTransaccionFactory.CreateParameter();
                        p_cod_persona.ParameterName = "p_cod_persona";
                        p_cod_persona.Value = pConsulta.cod_persona;
                        p_cod_persona.Direction = ParameterDirection.Input;
                        p_cod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_persona);

                        DbParameter p_tipo_consulta = cmdTransaccionFactory.CreateParameter();
                        p_tipo_consulta.ParameterName = "p_tipo_consulta";
                        p_tipo_consulta.Value = Convert.ToInt32(pConsulta.tipo_consulta);
                        p_tipo_consulta.Direction = ParameterDirection.Input;
                        p_tipo_consulta.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_tipo_consulta);

                        DbParameter p_contenido = cmdTransaccionFactory.CreateParameter();
                        p_contenido.ParameterName = "p_contenido";
                        if (pConsulta.contenido == null)
                            p_contenido.Value = " ";
                        else
                            p_contenido.Value = pConsulta.contenido;
                        p_contenido.Direction = ParameterDirection.Input;
                        p_contenido.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_contenido);

                        DbParameter p_coincidencia = cmdTransaccionFactory.CreateParameter();
                        p_coincidencia.ParameterName = "p_coincidencia";
                        p_coincidencia.Value = pConsulta.coincidencia;
                        p_coincidencia.Direction = ParameterDirection.Input;
                        p_coincidencia.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_coincidencia);

                        DbParameter p_fecha_consulta = cmdTransaccionFactory.CreateParameter();
                        p_fecha_consulta.ParameterName = "p_fecha_consulta";
                        p_fecha_consulta.Value = pConsulta.fecha_consulta;
                        p_fecha_consulta.Direction = ParameterDirection.Input;
                        p_fecha_consulta.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(p_fecha_consulta);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_GES_CONSULTA_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pConsulta.cod_consulta = Convert.ToInt64(p_cod_consulta.Value);
                        
                        dbConnectionFactory.CerrarConexion(connection);

                        return pConsulta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SarlaftAlertaData", "CrearRegistroConsultaLista", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Consultar si la persona se encuentra reportada
        /// </summary>
        /// <param name="pId">Código de la persona</param>
        /// <param name="pusuario"></param>
        /// <returns>Valor booleano</returns>
        public bool ConsultarReportePersona(Int64 cod_persona, Usuario vUsuario)
        {
            DbDataReader resultado;
            bool coincidencia = false;
            int cantidad = 0;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"SELECT COUNT(COINCIDENCIA) AS COINCIDENCIA FROM GR_CONSULTA_LISTA WHERE COINCIDENCIA = 1 AND COD_PERSONA = " + cod_persona +
                                      " AND FECHA_CONSULTA = (SELECT MAX(FECHA_CONSULTA) FROM GR_CONSULTA_LISTA WHERE COD_PERSONA = " + cod_persona + " AND COINCIDENCIA = 1 )";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COINCIDENCIA"] != DBNull.Value) cantidad = Convert.ToInt32(resultado["COINCIDENCIA"]);                            
                        }
                        if (cantidad > 0)
                            coincidencia = true;

                        dbConnectionFactory.CerrarConexion(connection);
                        return coincidencia;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SarlaftAlertaData", "ConsultarReportePersona", ex);
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// Listar personas que ya fueron consultadas en listas restrictivas
        /// </summary>
        /// <param name="filtro">Filtro para listado</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public List<SarlaftAlerta> ListarPersonasConsultadas(string filtro, bool pUltimo, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<SarlaftAlerta> lstSarlaft = new List<SarlaftAlerta>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();

                        string sql = @"WITH CONSULTALISTAS AS (
                                        SELECT C.COD_PERSONA, C.FECHA_CONSULTA
                                        FROM GR_CONSULTA_LISTA C
                                        WHERE C.TIPO_CONSULTA IN (1, 2) 
                                        GROUP BY C.COD_PERSONA, C.FECHA_CONSULTA)
                                       SELECT C.*, P.IDENTIFICACION, P.TIPO_PERSONA, P.DIGITO_VERIFICACION, P.NOMBRE, P.ESTADO,
                                        (SELECT L.COINCIDENCIA FROM GR_CONSULTA_LISTA L WHERE L.COD_PERSONA = C.COD_PERSONA AND L.FECHA_CONSULTA = C.FECHA_CONSULTA AND L.TIPO_CONSULTA = 1) AS COINCIDENCIA1,
                                        (SELECT L.COINCIDENCIA FROM GR_CONSULTA_LISTA L WHERE L.COD_PERSONA = C.COD_PERSONA AND L.FECHA_CONSULTA = C.FECHA_CONSULTA AND L.TIPO_CONSULTA = 2) AS COINCIDENCIA2
                                        FROM CONSULTALISTAS C LEFT JOIN V_PERSONA P ON C.COD_PERSONA = P.COD_PERSONA 
                                        WHERE C.COD_PERSONA > 0 ";
                        sql += !string.IsNullOrEmpty(filtro)? filtro : "";
                        sql += " ORDER BY c.COD_PERSONA, c.FECHA_CONSULTA";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        string auxIden = "";
                        bool coinc = false;
                        while (resultado.Read())
                        {
                            SarlaftAlerta entidad = new SarlaftAlerta();
                            if (pUltimo)
                            { 
                                if(Convert.ToString(resultado["IDENTIFICACION"]) == auxIden)
                                {
                                    lstSarlaft.RemoveAll(x => Convert.ToString(x.identificacion) == auxIden);
                                } else { coinc = false; }
                            }                            
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["TIPO_PERSONA"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["TIPO_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["DIGITO_VERIFICACION"] != DBNull.Value)
                            {
                                if (Convert.ToInt32(resultado["DIGITO_VERIFICACION"]) > 0)
                                    entidad.identificacion += "-" + Convert.ToInt32(resultado["DIGITO_VERIFICACION"]);
                            }
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["FECHA_CONSULTA"] != DBNull.Value) entidad.fecha_consulta = Convert.ToDateTime(resultado["FECHA_CONSULTA"]);
                            if (resultado["COINCIDENCIA1"] != DBNull.Value)
                            {
                                entidad.coincidencia = Convert.ToInt64(resultado["COINCIDENCIA1"]) > 0 ? true : false;
                                entidad.coincidencia2 = coinc;
                                coinc = entidad.coincidencia;
                            }
                            if (resultado["COINCIDENCIA2"] != DBNull.Value)
                            {
                                entidad.coincidencia2 = Convert.ToInt64(resultado["COINCIDENCIA2"]) > 0 ? true : false;
                                entidad.coincidencia = coinc;
                                coinc = entidad.coincidencia;
                            }
                            lstSarlaft.Add(entidad);
                            auxIden = "";
                            if (resultado["IDENTIFICACION"] != DBNull.Value)  auxIden = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["DIGITO_VERIFICACION"] != DBNull.Value)
                            {
                                if (Convert.ToInt64(resultado["DIGITO_VERIFICACION"]) > 0)
                                { 
                                    var z = resultado["IDENTIFICACION"].ToString();
                                    var y = resultado["DIGITO_VERIFICACION"].ToString();
                                    auxIden = Convert.ToString(resultado["IDENTIFICACION"]) + "-" + Convert.ToInt64(resultado["DIGITO_VERIFICACION"]);
                                }
                            }
                        }
                        
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstSarlaft;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SarlaftAlertaData", "ListarPersonasConsultadas", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica el estado de la persona si se encuentra reportada en listas restrictivas
        /// </summary>
        /// <param name="cod_persona">Código del asociado</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public void ModificarEstadoPersona(Int64 cod_persona, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_persona = cmdTransaccionFactory.CreateParameter();
                        p_cod_persona.ParameterName = "p_cod_persona";
                        p_cod_persona.Value = cod_persona;
                        p_cod_persona.Direction = ParameterDirection.Input;
                        p_cod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_persona);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_GES_ESTADOPER_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SarlaftAlertaData", "ModificarEstadoPersona", ex);
                    }
                }
            }
        }
   

        public List<SarlaftAlerta> ListarPersonasParaConsultar(string filtro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<SarlaftAlerta> lstSarlaft = new List<SarlaftAlerta>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"SELECT P.COD_PERSONA, CASE P.TIPO_PERSONA WHEN 'N' THEN 'NATURAL' WHEN 'J' THEN 'JURIDICA' ELSE NULL END AS TIPO_PERSONA, 
                                        P.IDENTIFICACION, P.DIGITO_VERIFICACION, P.NOMBRES ||' '|| P.APELLIDOS AS NOMBRE
                                        FROM V_PERSONA P 
                                        WHERE P.COD_PERSONA > 0 ";
                        //linea de prueba para limitar consulta
                        //sql += "AND P.COD_PERSONA IN (1,2,3,4,5,6,7,8,9,10) ";
                        sql += filtro != null && filtro != "" ? filtro : "";
                        sql += " ORDER BY P.COD_PERSONA";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            SarlaftAlerta entidad = new SarlaftAlerta();
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt32(resultado["COD_PERSONA"]);
                            if (resultado["TIPO_PERSONA"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["TIPO_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["DIGITO_VERIFICACION"] != DBNull.Value)
                            {
                                if (Convert.ToInt32(resultado["DIGITO_VERIFICACION"]) > 0)
                                    entidad.identificacion += "-" + Convert.ToInt32(resultado["DIGITO_VERIFICACION"]);
                            }
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            lstSarlaft.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstSarlaft;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SarlaftAlertaData", "ListarPersonasParaConsultar", ex);
                        return null;
                    }
                }
            }
        }




    }
}