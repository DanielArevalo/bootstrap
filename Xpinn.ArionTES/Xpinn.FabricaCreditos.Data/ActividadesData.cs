using System;
using System.Collections.Generic;
using System.Text;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Util;
using System.Data;
using System.Data.Common;

namespace Xpinn.FabricaCreditos.Data
{
    public class ActividadesData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public ActividadesData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


       
        public Actividades CrearActividadesPersona(Actividades pActividad, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidactividad = cmdTransaccionFactory.CreateParameter();
                        pidactividad.ParameterName = "p_idactividad";
                        pidactividad.Value = pActividad.idactividad;
                        pidactividad.Direction = ParameterDirection.Output;
                        pidactividad.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidactividad);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pActividad.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pfecha_realizacion = cmdTransaccionFactory.CreateParameter();
                        pfecha_realizacion.ParameterName = "p_fecha_realizacion";
                        pfecha_realizacion.Value = pActividad.fecha_realizacion;
                        pfecha_realizacion.Direction = ParameterDirection.Input;
                        pfecha_realizacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_realizacion);

                        DbParameter ptipo_actividad = cmdTransaccionFactory.CreateParameter();
                        ptipo_actividad.ParameterName = "p_tipo_actividad";
                        ptipo_actividad.Value = pActividad.tipo_actividad;
                        ptipo_actividad.Direction = ParameterDirection.Input;
                        ptipo_actividad.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ptipo_actividad);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = pActividad.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter pparticipante = cmdTransaccionFactory.CreateParameter();
                        pparticipante.ParameterName = "p_participante";
                        pparticipante.Value = pActividad.participante;
                        pparticipante.Direction = ParameterDirection.Input;
                        pparticipante.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pparticipante);

                        DbParameter pcalificacion = cmdTransaccionFactory.CreateParameter();
                        pcalificacion.ParameterName = "p_calificacion";
                        pcalificacion.Value = pActividad.calificacion;
                        pcalificacion.Direction = ParameterDirection.Input;
                        pcalificacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcalificacion);

                        DbParameter pduracion = cmdTransaccionFactory.CreateParameter();
                        pduracion.ParameterName = "p_duracion";
                        pduracion.Value = pActividad.duracion;
                        pduracion.Direction = ParameterDirection.Input;
                        pduracion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pduracion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_PERSONAACT_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        
                        dbConnectionFactory.CerrarConexion(connection);

                        pActividad.idactividad = Convert.ToInt64(pidactividad.Value);

                        return pActividad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActividadPersonaData", "CrearActividadesPersona", ex);
                        return null;
                    }
                }
            }
        }


        public Actividades ModificarActividadesPersona(Actividades pActividad, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidactividad = cmdTransaccionFactory.CreateParameter();
                        pidactividad.ParameterName = "p_idactividad";
                        pidactividad.Value = pActividad.idactividad;
                        pidactividad.Direction = ParameterDirection.Input;
                        pidactividad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidactividad);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pActividad.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pfecha_realizacion = cmdTransaccionFactory.CreateParameter();
                        pfecha_realizacion.ParameterName = "p_fecha_realizacion";
                        pfecha_realizacion.Value = pActividad.fecha_realizacion;
                        pfecha_realizacion.Direction = ParameterDirection.Input;
                        pfecha_realizacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_realizacion);

                        DbParameter ptipo_actividad = cmdTransaccionFactory.CreateParameter();
                        ptipo_actividad.ParameterName = "p_tipo_actividad";
                        ptipo_actividad.Value = pActividad.tipo_actividad;
                        ptipo_actividad.Direction = ParameterDirection.Input;
                        ptipo_actividad.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ptipo_actividad);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = pActividad.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter pparticipante = cmdTransaccionFactory.CreateParameter();
                        pparticipante.ParameterName = "p_participante";
                        pparticipante.Value = pActividad.participante;
                        pparticipante.Direction = ParameterDirection.Input;
                        pparticipante.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pparticipante);

                        DbParameter pcalificacion = cmdTransaccionFactory.CreateParameter();
                        pcalificacion.ParameterName = "p_calificacion";
                        pcalificacion.Value = pActividad.calificacion;
                        pcalificacion.Direction = ParameterDirection.Input;
                        pcalificacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcalificacion);

                        DbParameter pduracion = cmdTransaccionFactory.CreateParameter();
                        pduracion.ParameterName = "p_duracion";
                        pduracion.Value = pActividad.duracion;
                        pduracion.Direction = ParameterDirection.Input;
                        pduracion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pduracion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_PERACTIVIDAD_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pActividad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActividadPersonaData", "ModificarActividadesPersona", ex);
                        return null;
                    }
                }
            }
        }

        public List<Actividades> listarTemasInteres(Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Actividades> lstTemas = new List<Actividades>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select * from temas_interes where estado = 1 order by 1 desc";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Actividades entidad = new Actividades();
                            if (resultado["ID_TEMA"] != DBNull.Value) entidad.idactividad = Convert.ToInt64(resultado["ID_TEMA"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["NOMBRE"]);                            
                            lstTemas.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTemas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActividadPersonaData", "ListarActividadesPersona", ex);
                        return null;
                    }
                }
            }
        }

        public List<Actividades> ListarActividadesPersona(Actividades pActividad, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Actividades> lstActividad = new List<Actividades>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM PERSONA_ACTIVIDAD " + ObtenerFiltro(pActividad) + " ORDER BY IDACTIVIDAD ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Actividades entidad = new Actividades();
                            if (resultado["IDACTIVIDAD"] != DBNull.Value) entidad.idactividad = Convert.ToInt64(resultado["IDACTIVIDAD"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["FECHA_REALIZACION"] != DBNull.Value) entidad.fecha_realizacion = Convert.ToDateTime(resultado["FECHA_REALIZACION"]);
                            if (resultado["TIPO_ACTIVIDAD"] != DBNull.Value) entidad.tipo_actividad = Convert.ToDecimal(resultado["TIPO_ACTIVIDAD"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["PARTICIPANTE"] != DBNull.Value) entidad.participante = Convert.ToInt32(resultado["PARTICIPANTE"]);
                            if (resultado["CALIFICACION"] != DBNull.Value) entidad.calificacion = Convert.ToString(resultado["CALIFICACION"]);
                            if (resultado["DURACION"] != DBNull.Value) entidad.duracion = Convert.ToString(resultado["DURACION"]);
                            lstActividad.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstActividad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActividadPersonaData", "ListarActividadesPersona", ex);
                        return null;
                    }
                }
            }
        }


        public List<Actividades> ConsultarActividad(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            
            List<Actividades> lstActividad = new List<Actividades>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM PERSONA_ACTIVIDAD WHERE COD_PERSONA = " + pId;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Actividades entidad = new Actividades();
                            if (resultado["IDACTIVIDAD"] != DBNull.Value) entidad.idactividad = Convert.ToInt64(resultado["IDACTIVIDAD"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["FECHA_REALIZACION"] != DBNull.Value) entidad.fecha_realizacion = Convert.ToDateTime(resultado["FECHA_REALIZACION"]);
                            if (resultado["TIPO_ACTIVIDAD"] != DBNull.Value) entidad.tipo_actividad = Convert.ToDecimal(resultado["TIPO_ACTIVIDAD"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["PARTICIPANTE"] != DBNull.Value) entidad.participante = Convert.ToInt32(resultado["PARTICIPANTE"]);
                            if (resultado["CALIFICACION"] != DBNull.Value) entidad.calificacion = Convert.ToString(resultado["CALIFICACION"]);
                            if (resultado["DURACION"] != DBNull.Value) entidad.duracion = Convert.ToString(resultado["DURACION"]);
                            lstActividad.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstActividad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActividadPersonaData", "ConsultarActividad", ex);
                        return null;
                    }
                }
            }
        }



        public void EliminarActividadPersona(Int64 pIdActividad, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {       
                        DbParameter pidactividad = cmdTransaccionFactory.CreateParameter();
                        pidactividad.ParameterName = "p_idactividad";
                        pidactividad.Value = pIdActividad;
                        pidactividad.Direction = ParameterDirection.Input;
                        pidactividad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidactividad);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_PERSONAACT_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActividadPersonaData", "EliminarActividadPersona", ex);
                    }
                }
            }
        }



        #region PERSONA INFORMACION FINANCIERA

        public CuentasBancarias CrearPer_CuentaFinac(CuentasBancarias pCuentasBanc, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidcuentabancaria = cmdTransaccionFactory.CreateParameter();
                        pidcuentabancaria.ParameterName = "p_idcuentabancaria";
                        pidcuentabancaria.Value = pCuentasBanc.idcuentabancaria;
                        pidcuentabancaria.Direction = ParameterDirection.Output;
                        pidcuentabancaria.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidcuentabancaria);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pCuentasBanc.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter ptipo_cuenta = cmdTransaccionFactory.CreateParameter();
                        ptipo_cuenta.ParameterName = "p_tipo_cuenta";
                        ptipo_cuenta.Value = pCuentasBanc.tipo_cuenta;
                        ptipo_cuenta.Direction = ParameterDirection.Input;
                        ptipo_cuenta.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_cuenta);

                        DbParameter pnumero_cuenta = cmdTransaccionFactory.CreateParameter();
                        pnumero_cuenta.ParameterName = "p_numero_cuenta";
                        pnumero_cuenta.Value = pCuentasBanc.numero_cuenta;
                        pnumero_cuenta.Direction = ParameterDirection.Input;
                        pnumero_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumero_cuenta);

                        DbParameter pcod_banco = cmdTransaccionFactory.CreateParameter();
                        pcod_banco.ParameterName = "p_cod_banco";
                        pcod_banco.Value = pCuentasBanc.cod_banco;
                        pcod_banco.Direction = ParameterDirection.Input;
                        pcod_banco.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_banco);

                        DbParameter psucursal = cmdTransaccionFactory.CreateParameter();
                        psucursal.ParameterName = "p_sucursal";
                        if (pCuentasBanc.sucursal != null) psucursal.Value = pCuentasBanc.sucursal; else psucursal.Value = DBNull.Value;
                        psucursal.Direction = ParameterDirection.Input;
                        psucursal.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(psucursal);

                        DbParameter pfecha_apertura = cmdTransaccionFactory.CreateParameter();
                        pfecha_apertura.ParameterName = "p_fecha_apertura";
                        if (pCuentasBanc.fecha_apertura != null) pfecha_apertura.Value = pCuentasBanc.fecha_apertura; else pfecha_apertura.Value = DBNull.Value;
                        pfecha_apertura.Direction = ParameterDirection.Input;
                        pfecha_apertura.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_apertura);

                        DbParameter pprincipal = cmdTransaccionFactory.CreateParameter();
                        pprincipal.ParameterName = "p_principal";
                        pprincipal.Value = pCuentasBanc.principal;
                        pprincipal.Direction = ParameterDirection.Input;
                        pprincipal.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pprincipal);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_PER_CUENTA_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pCuentasBanc.idcuentabancaria = Convert.ToInt64(pidcuentabancaria.Value);

                        return pCuentasBanc;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentaBancariaPersonaData", "CrearPer_CuentaFinac", ex);
                        return null;
                    }
                }
            }
        }


        public CuentasBancarias ModificarPer_CuentaFinac(CuentasBancarias pCuentasBanc, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidcuentabancaria = cmdTransaccionFactory.CreateParameter();
                        pidcuentabancaria.ParameterName = "p_idcuentabancaria";
                        pidcuentabancaria.Value = pCuentasBanc.idcuentabancaria;
                        pidcuentabancaria.Direction = ParameterDirection.Input;
                        pidcuentabancaria.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidcuentabancaria);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pCuentasBanc.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter ptipo_cuenta = cmdTransaccionFactory.CreateParameter();
                        ptipo_cuenta.ParameterName = "p_tipo_cuenta";
                        ptipo_cuenta.Value = pCuentasBanc.tipo_cuenta;
                        ptipo_cuenta.Direction = ParameterDirection.Input;
                        ptipo_cuenta.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_cuenta);

                        DbParameter pnumero_cuenta = cmdTransaccionFactory.CreateParameter();
                        pnumero_cuenta.ParameterName = "p_numero_cuenta";
                        pnumero_cuenta.Value = pCuentasBanc.numero_cuenta;
                        pnumero_cuenta.Direction = ParameterDirection.Input;
                        pnumero_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumero_cuenta);

                        DbParameter pcod_banco = cmdTransaccionFactory.CreateParameter();
                        pcod_banco.ParameterName = "p_cod_banco";
                        pcod_banco.Value = pCuentasBanc.cod_banco;
                        pcod_banco.Direction = ParameterDirection.Input;
                        pcod_banco.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_banco);

                        DbParameter psucursal = cmdTransaccionFactory.CreateParameter();
                        psucursal.ParameterName = "p_sucursal";
                        if (pCuentasBanc.sucursal != null) psucursal.Value = pCuentasBanc.sucursal; else psucursal.Value = DBNull.Value;
                        psucursal.Direction = ParameterDirection.Input;
                        psucursal.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(psucursal);

                        DbParameter pfecha_apertura = cmdTransaccionFactory.CreateParameter();
                        pfecha_apertura.ParameterName = "p_fecha_apertura";
                        if (pCuentasBanc.fecha_apertura != null) pfecha_apertura.Value = pCuentasBanc.fecha_apertura; else pfecha_apertura.Value = DBNull.Value;
                        pfecha_apertura.Direction = ParameterDirection.Input;
                        pfecha_apertura.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_apertura);

                        DbParameter pprincipal = cmdTransaccionFactory.CreateParameter();
                        pprincipal.ParameterName = "p_principal";
                        pprincipal.Value = pCuentasBanc.principal;
                        pprincipal.Direction = ParameterDirection.Input;
                        pprincipal.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pprincipal);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_PER_CUENTA_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pCuentasBanc;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentaBancariaPersonaData", "ModificarPer_CuentaFinac", ex);
                        return null;
                    }
                }
            }
        }



        public List<CuentasBancarias> ConsultarCuentasBancarias(Int64 pId,string filtro, Usuario vUsuario)
        {
            DbDataReader resultado;

            List<CuentasBancarias> lstCuentasBanc = new List<CuentasBancarias>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT perc.*, b.NOMBREBANCO FROM PERSONA_CUENTASBANCARIAS perc
                                        JOIN BANCOS b on perc.COD_BANCO = b.COD_BANCO
                                        WHERE perc.COD_PERSONA = " + pId;

                        if (filtro != "")
                            sql += filtro;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            CuentasBancarias entidad = new CuentasBancarias();
                            if (resultado["IDCUENTABANCARIA"] != DBNull.Value) entidad.idcuentabancaria = Convert.ToInt64(resultado["IDCUENTABANCARIA"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["TIPO_CUENTA"] != DBNull.Value) entidad.tipo_cuenta = Convert.ToInt32(resultado["TIPO_CUENTA"]);
                            if (resultado["NUMERO_CUENTA"] != DBNull.Value) entidad.numero_cuenta = Convert.ToString(resultado["NUMERO_CUENTA"]);
                            if (resultado["COD_BANCO"] != DBNull.Value) entidad.cod_banco = Convert.ToInt32(resultado["COD_BANCO"]);
                            if (resultado["SUCURSAL"] != DBNull.Value) entidad.sucursal = Convert.ToString(resultado["SUCURSAL"]);
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                            if (resultado["PRINCIPAL"] != DBNull.Value) entidad.principal = Convert.ToInt32(resultado["PRINCIPAL"]);
                            if (resultado["NOMBREBANCO"] != DBNull.Value) entidad.nombre_banco = Convert.ToString(resultado["NOMBREBANCO"]);

                            lstCuentasBanc.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCuentasBanc;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasPersonaData", "ConsultarCuentasBancarias", ex);
                        return null;
                    }
                }
            }
        }



        public void EliminarCuentasBancarias(Int64 pIdCuentabancaria, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidcuentabancaria = cmdTransaccionFactory.CreateParameter();
                        pidcuentabancaria.ParameterName = "p_idcuentabancaria";
                        pidcuentabancaria.Value = pIdCuentabancaria;
                        pidcuentabancaria.Direction = ParameterDirection.Input;
                        pidcuentabancaria.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidcuentabancaria);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_PER_CUENTA_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasPersonaData", "EliminarCuentasBancarias", ex);
                    }
                }
            }
        }



        #endregion

        #region ACTIVIDAD ECONOMICA

        public List<Actividades> ConsultarActividadEconomica(Actividades pActividad, Usuario vUsuario)
        {
            DbDataReader resultado;

            List<Actividades> lstActividad = new List<Actividades>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM ACTIVIDAD " + ObtenerFiltro(pActividad) + "";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Actividades entidad = new Actividades();
                            if (resultado["CODACTIVIDAD"] != DBNull.Value) entidad.codactividad = Convert.ToString(resultado["CODACTIVIDAD"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            lstActividad.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstActividad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActividadData", "ConsultarActividadEconomica", ex);
                        return null;
                    }
                }
            }
        }

        public Actividades ConsultarActividadEconomicaId(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;

            List<Actividades> lstActividad = new List<Actividades>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM ACTIVIDAD WHERE CODACTIVIDAD ='"+ pId + "'";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        Actividades entidad = new Actividades();
                        if (resultado.Read())
                        {
                            
                            if (resultado["CODACTIVIDAD"] != DBNull.Value) entidad.codactividad = Convert.ToString(resultado["CODACTIVIDAD"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActividadData", "ConsultarActividadEconomicaId", ex);
                        return null;
                    }
                }
            }
        }

        public Actividades CrearActividadEconomica(Actividades pActividad, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodactividad = cmdTransaccionFactory.CreateParameter();
                        pcodactividad.ParameterName = "p_codactividad";
                        pcodactividad.Value = pActividad.codactividad;
                        pcodactividad.Direction = ParameterDirection.Input;
                        pcodactividad.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcodactividad);
                       

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = pActividad.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_ACTIVIDAD_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);

                        return pActividad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActividadData", "CrearActividadEconomica", ex);
                        return null;
                    }
                }
            }
        }

        public Actividades ModificarActividadEconomica(String pCodactividad, Actividades pActividad, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_codactividad_antiguo = cmdTransaccionFactory.CreateParameter();
                        p_codactividad_antiguo.ParameterName = "p_codactividad_antiguo";
                        p_codactividad_antiguo.Value = pCodactividad;
                        p_codactividad_antiguo.Direction = ParameterDirection.Input;
                        p_codactividad_antiguo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_codactividad_antiguo);

                        DbParameter p_codactividad_nuevo = cmdTransaccionFactory.CreateParameter();
                        p_codactividad_nuevo.ParameterName = "p_codactividad_nuevo";
                        p_codactividad_nuevo.Value = pActividad.codactividad;
                        p_codactividad_nuevo.Direction = ParameterDirection.Input;
                        p_codactividad_nuevo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_codactividad_nuevo);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = pActividad.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_ACTIVIDAD_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);

                        return pActividad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActividadData", "ModificarActividadEconomica", ex);
                        return null;
                    }
                }
            }
        }

        public void EliminarActividadEconomica(String pCodactividad, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_codactividad = cmdTransaccionFactory.CreateParameter();
                        p_codactividad.ParameterName = "p_codactividad";
                        p_codactividad.Value = pCodactividad;
                        p_codactividad.Direction = ParameterDirection.Input;
                        p_codactividad.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_codactividad);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_ACTIVIDAD_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActividadData", "EliminarActividadEconomica", ex);
                    }
                }
            }
        }

        public List<Actividades> ConsultarActividadesEconomicasSecundarias(Int64 pCodPersona, Usuario vUsuario)
        {
            DbDataReader resultado;

            List<Actividades> lstActividad = new List<Actividades>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT COD_PERSONA , COD_ACTIVIDAD , A.DESCRIPCION FROM PERSONA_ACTIVIDAD_SECUNDARIA P
                                       INNER JOIN ACTIVIDAD A ON A.CODACTIVIDAD = P.COD_ACTIVIDAD WHERE COD_PERSONA = " + pCodPersona;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Actividades entidad = new Actividades();
                            entidad.codactividad = Convert.ToString(resultado["COD_ACTIVIDAD"]);
                            entidad.cod_persona = pCodPersona;
                            entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            lstActividad.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstActividad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActividadesData", "ConsultarActividadesEconomicasSecundarias", ex);
                        return null;
                    }
                }
            }
        }

        public void CrearActividadEconomicaSecundaria(Int64 pCodPersona , String pCodActividad , Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_Persona = cmdTransaccionFactory.CreateParameter();
                        p_cod_Persona.ParameterName = "p_cod_persona";
                        p_cod_Persona.Value = pCodPersona;
                        p_cod_Persona.Direction = ParameterDirection.Input;
                        p_cod_Persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_Persona);

                        DbParameter p_cod_actividad = cmdTransaccionFactory.CreateParameter();
                        p_cod_actividad.ParameterName = "p_cod_actividad";
                        p_cod_actividad.Value = pCodActividad;
                        p_cod_actividad.Direction = ParameterDirection.Input;
                        p_cod_actividad.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_cod_actividad);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_PERACTISEC_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActividadesData", "CrearActividadEconomicaSecundaria", ex);
                    }
                }
            }
        }

        public void EliminarActividadesEconomicasSecundarias(Int64 pCodPersona, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_Persona = cmdTransaccionFactory.CreateParameter();
                        p_cod_Persona.ParameterName = "p_cod_persona";
                        p_cod_Persona.Value = pCodPersona;
                        p_cod_Persona.Direction = ParameterDirection.Input;
                        p_cod_Persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_Persona);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_PERACTISEC_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActividadesData", "CrearActividadEconomicaSecundaria", ex);
                    }
                }
            }
        }

        #endregion

    }
}
