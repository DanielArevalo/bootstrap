using System;
using System.Configuration;
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
    /// Objeto de acceso a datos para la tabla Credito
    /// </summary>
    public class Credito_GiroData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla Credito
        /// </summary>
        public Credito_GiroData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public Credito_Giro CrearCredito_giro(Credito_Giro pCredito_giro, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidgiro = cmdTransaccionFactory.CreateParameter();
                        pidgiro.ParameterName = "p_idgiro";
                        pidgiro.Value = pCredito_giro.idgiro;
                        pidgiro.Direction = ParameterDirection.Output;
                        pidgiro.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidgiro);

                        DbParameter pnumero_radicacion = cmdTransaccionFactory.CreateParameter();
                        pnumero_radicacion.ParameterName = "p_numero_radicacion";
                        pnumero_radicacion.Value = pCredito_giro.numero_radicacion;
                        pnumero_radicacion.Direction = ParameterDirection.Input;
                        pnumero_radicacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_radicacion);                        

                        DbParameter pidentificacion = cmdTransaccionFactory.CreateParameter();
                        pidentificacion.ParameterName = "p_identificacion";
                        if (pCredito_giro.identificacion == null)
                            pidentificacion.Value = DBNull.Value;
                        else
                            pidentificacion.Value = pCredito_giro.identificacion;
                        pidentificacion.Direction = ParameterDirection.Input;
                        pidentificacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidentificacion);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "p_nombre";
                        if (pCredito_giro.nombre == null)
                            pnombre.Value = DBNull.Value;
                        else
                            pnombre.Value = pCredito_giro.nombre;
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        pvalor.Value = pCredito_giro.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);                       

                        DbParameter ptipo = cmdTransaccionFactory.CreateParameter();
                        ptipo.ParameterName = "p_tipo";
                        ptipo.Value = pCredito_giro.tipo;
                        ptipo.Direction = ParameterDirection.Input;
                        ptipo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        if (pCredito_giro.cod_persona == null)
                            pcod_persona.Value = DBNull.Value;
                        else
                            pcod_persona.Value = pCredito_giro.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_CREDITGIRO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pCredito_giro.idgiro = Convert.ToInt64(pidgiro.Value); 
                        return pCredito_giro;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Credito_GiroData", "CrearCredito_giro", ex);
                        return null;
                    }
                }
            }
        }

        public Credito_Giro CrearCredito_giroACC(Credito_Giro pCredito_giro, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidgiro = cmdTransaccionFactory.CreateParameter();
                        pidgiro.ParameterName = "p_idgiro";
                        pidgiro.Value = pCredito_giro.idgiro;
                        pidgiro.Direction = ParameterDirection.Output;
                        pidgiro.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidgiro);

                        DbParameter pnumero_radicacion = cmdTransaccionFactory.CreateParameter();
                        pnumero_radicacion.ParameterName = "p_numero_radicacion";
                        pnumero_radicacion.Value = pCredito_giro.numero_radicacion;
                        pnumero_radicacion.Direction = ParameterDirection.Input;
                        pnumero_radicacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_radicacion);

                        DbParameter pidentificacion = cmdTransaccionFactory.CreateParameter();
                        pidentificacion.ParameterName = "p_identificacion";
                        if (pCredito_giro.identificacion == null)
                            pidentificacion.Value = DBNull.Value;
                        else
                            pidentificacion.Value = pCredito_giro.identificacion;
                        pidentificacion.Direction = ParameterDirection.Input;
                        pidentificacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidentificacion);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "p_nombre";
                        if (pCredito_giro.nombre == null)
                            pnombre.Value = DBNull.Value;
                        else
                            pnombre.Value = pCredito_giro.nombre;
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        pvalor.Value = pCredito_giro.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter ptipo = cmdTransaccionFactory.CreateParameter();
                        ptipo.ParameterName = "p_tipo";
                        ptipo.Value = pCredito_giro.tipo;
                        ptipo.Direction = ParameterDirection.Input;
                        ptipo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo);

                        DbParameter p_forma_desembolso = cmdTransaccionFactory.CreateParameter();
                        p_forma_desembolso.ParameterName = "p_forma_desembolso";
                        p_forma_desembolso.Value = pCredito_giro.id_tipo_desembolso;
                        p_forma_desembolso.Direction = ParameterDirection.Input;
                        p_forma_desembolso.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_forma_desembolso);

                        DbParameter ptipo_cuenta = cmdTransaccionFactory.CreateParameter();
                        ptipo_cuenta.ParameterName = "ptipo_cuenta";
                        if (pCredito_giro.cuenta.tipo_cuenta == 0)
                            ptipo_cuenta.Value = DBNull.Value;
                        else
                            ptipo_cuenta.Value = pCredito_giro.cuenta.tipo_cuenta;
                        ptipo_cuenta.Direction = ParameterDirection.Input;
                        ptipo_cuenta.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_cuenta);

                        DbParameter pnum_cuenta = cmdTransaccionFactory.CreateParameter();
                        pnum_cuenta.ParameterName = "pnum_cuenta";
                        if (pCredito_giro.cuenta.numero_cuenta == null)
                            pnum_cuenta.Value = DBNull.Value;
                        else
                            pnum_cuenta.Value = pCredito_giro.cuenta.numero_cuenta;
                        pnum_cuenta.Direction = ParameterDirection.Input;
                        pnum_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnum_cuenta);

                        DbParameter pcod_banco = cmdTransaccionFactory.CreateParameter();
                        pcod_banco.ParameterName = "pcod_banco";
                        if (pCredito_giro.cuenta.cod_banco == 0)
                            pcod_banco.Value = DBNull.Value;
                        else
                            pcod_banco.Value = pCredito_giro.cuenta.numero_cuenta;
                        pcod_banco.Direction = ParameterDirection.Input;
                        pcod_banco.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_banco);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        if (pCredito_giro.cod_persona == null)
                            pcod_persona.Value = DBNull.Value;
                        else
                            pcod_persona.Value = pCredito_giro.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_CREGIROACC_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pCredito_giro.idgiro = Convert.ToInt64(pidgiro.Value);
                        return pCredito_giro;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Credito_GiroData", "CrearCredito_giro", ex);
                        return null;
                    }
                }
            }
        }

        public List<Credito_Giro> ListarGiros(string radicado, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Credito_Giro> lstCredGIro = new List<Credito_Giro>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT CREDITO_GIRO.*,CASE CREDITO_GIRO.TIPO WHEN 0 THEN 'Asociado' WHEN 1 THEN 'Tercero' END AS NOM_TIPO FROM CREDITO_GIRO where NUMERO_RADICACION = " +radicado;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Credito_Giro entidad = new Credito_Giro();
                            if (resultado["IDGIRO"] != DBNull.Value) entidad.idgiro = Convert.ToInt64(resultado["IDGIRO"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToInt32(resultado["TIPO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt32(resultado["COD_PERSONA"]);
                            if (resultado["NOM_TIPO"] != DBNull.Value) entidad.nom_tipo = Convert.ToString(resultado["NOM_TIPO"]);
                            lstCredGIro.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCredGIro;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Credito_GiroData", "ConsultarCredito_Giro", ex);
                        return null;
                    }
                }
            }
        }

        public List<Credito_Giro> ConsultarCredito_Giro(Credito_Giro pCredito_Giro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Credito_Giro> lstCredGIro = new List<Credito_Giro>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT CREDITO_GIRO.*,CASE CREDITO_GIRO.TIPO WHEN 0 THEN 'Asociado' WHEN 1 THEN 'Tercero' END AS NOM_TIPO FROM CREDITO_GIRO " + ObtenerFiltro(pCredito_Giro);
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Credito_Giro entidad = new Credito_Giro();
                            if (resultado["IDGIRO"] != DBNull.Value) entidad.idgiro = Convert.ToInt64(resultado["IDGIRO"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToInt32(resultado["TIPO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt32(resultado["COD_PERSONA"]);
                            if (resultado["NOM_TIPO"] != DBNull.Value) entidad.nom_tipo = Convert.ToString(resultado["NOM_TIPO"]);
                            lstCredGIro.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCredGIro;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Credito_GiroData", "ConsultarCredito_Giro", ex);
                        return null;
                    }
                }
            }
        }


    }
}