using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Tesoreria.Entities;

namespace Xpinn.Tesoreria.Data
{
    public class CobroCodeudorData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public CobroCodeudorData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
        public CobroCodeudor CrearCobroCodeudor(CobroCodeudor pCobroCodeudor, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidcobrocodeud = cmdTransaccionFactory.CreateParameter();
                        pidcobrocodeud.ParameterName = "p_idcobrocodeud";
                        pidcobrocodeud.Value = pCobroCodeudor.idcobrocodeud;
                        pidcobrocodeud.Direction = ParameterDirection.Output;
                        pidcobrocodeud.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidcobrocodeud);

                        DbParameter pnumero_radicacion = cmdTransaccionFactory.CreateParameter();
                        pnumero_radicacion.ParameterName = "p_numero_radicacion";
                        pnumero_radicacion.Value = pCobroCodeudor.numero_radicacion;
                        pnumero_radicacion.Direction = ParameterDirection.Input;
                        pnumero_radicacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_radicacion);

                        DbParameter pcod_deudor = cmdTransaccionFactory.CreateParameter();
                        pcod_deudor.ParameterName = "p_cod_deudor";
                        pcod_deudor.Value = pCobroCodeudor.cod_deudor;
                        pcod_deudor.Direction = ParameterDirection.Input;
                        pcod_deudor.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_deudor);

                        DbParameter pfecha_cobro = cmdTransaccionFactory.CreateParameter();
                        pfecha_cobro.ParameterName = "p_fecha_cobro";
                        pfecha_cobro.Value = pCobroCodeudor.fecha_cobro;
                        pfecha_cobro.Direction = ParameterDirection.Input;
                        pfecha_cobro.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_cobro);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pCobroCodeudor.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pporcentaje = cmdTransaccionFactory.CreateParameter();
                        pporcentaje.ParameterName = "p_porcentaje";
                        if (pCobroCodeudor.porcentaje == null)
                            pporcentaje.Value = DBNull.Value;
                        else
                            pporcentaje.Value = pCobroCodeudor.porcentaje;
                        pporcentaje.Direction = ParameterDirection.Input;
                        pporcentaje.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pporcentaje);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        pvalor.Value = pCobroCodeudor.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter pcod_empresa = cmdTransaccionFactory.CreateParameter();
                        pcod_empresa.ParameterName = "p_cod_empresa";
                        if (pCobroCodeudor.cod_empresa == null)
                            pcod_empresa.Value = DBNull.Value;
                        else
                            pcod_empresa.Value = pCobroCodeudor.cod_empresa;
                        pcod_empresa.Direction = ParameterDirection.Input;
                        pcod_empresa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_empresa);

                        DbParameter pfechacrea = cmdTransaccionFactory.CreateParameter();
                        pfechacrea.ParameterName = "p_fechacrea";
                        if (pCobroCodeudor.fechacrea == null)
                            pfechacrea.Value = DBNull.Value;
                        else
                            pfechacrea.Value = pCobroCodeudor.fechacrea;
                        pfechacrea.Direction = ParameterDirection.Input;
                        pfechacrea.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechacrea);

                        DbParameter pcod_usuario = cmdTransaccionFactory.CreateParameter();
                        pcod_usuario.ParameterName = "p_cod_usuario";
                        if (pCobroCodeudor.cod_usuario == null)
                            pcod_usuario.Value = DBNull.Value;
                        else
                            pcod_usuario.Value = pCobroCodeudor.cod_usuario;
                        pcod_usuario.Direction = ParameterDirection.Input;
                        pcod_usuario.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pcod_usuario);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pCobroCodeudor.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REC_COBRO_CODE_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pCobroCodeudor;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CobroCodeudorData", "CrearCobroCodeudor", ex);
                        return null;
                    }
                }
            }
        }

        public List<CobroCodeudor> ConsultarCodeudoresDeUnCredito(long numeroRadicacion, Usuario pusuario)
        {
            DbDataReader resultado;
            List<CobroCodeudor> lstEntidad = new List<CobroCodeudor>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select cod.codpersona, per.identificacion, per.nombreyapellido, cobro.porcentaje, cobro.valor, cobro.idcobrocodeud
                                        from credito cre
                                        join codeudores cod on cre.numero_radicacion = cod.numero_radicacion
                                        join V_persona per on cod.codpersona = per.cod_persona
                                        left join Cobro_codeudor cobro on cre.numero_radicacion = cobro.numero_radicacion
                                        where cre.numero_radicacion = " + numeroRadicacion.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            CobroCodeudor entidad = new CobroCodeudor();
                            if (resultado["idcobrocodeud"] != DBNull.Value) entidad.idcobrocodeud = Convert.ToInt32(resultado["idcobrocodeud"]);
                            if (resultado["codpersona"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["codpersona"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion_codeudor = Convert.ToString(resultado["identificacion"]);
                            if (resultado["nombreyapellido"] != DBNull.Value) entidad.nombreYApellidoCodeudor = Convert.ToString(resultado["nombreyapellido"]);
                            if (resultado["porcentaje"] != DBNull.Value) entidad.porcentaje = Convert.ToDecimal(resultado["porcentaje"]);
                            if (resultado["valor"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["valor"]);
                            lstEntidad.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CobroCodeudorData", "ConsultarCodeudoresDeUnCredito", ex);
                        return null;
                    }
                }
            }
        }


        public bool ConsultarSiCreditoTieneCodeudor(long numeroRadicacion, Usuario pusuario)
        {
            DbDataReader resultado;
            bool tieneCodeudor = false;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select * from codeudores where numero_radicacion = " + numeroRadicacion.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            tieneCodeudor = true;
                        }

                        dbConnectionFactory.CerrarConexion(connection);

                        return tieneCodeudor;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CobroCodeudorData", "ConsultarSiCreditoTieneCodeudor", ex);
                        return false;
                    }
                }
            }
        }


        public List<EmpresaRecaudo> ListarEmpresaRecaudo(long cod_persona, Usuario pusuario)
        {
            DbDataReader resultado;
            List<EmpresaRecaudo> lstEntidad = new List<EmpresaRecaudo>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select emprec.cod_empresa, emprec.nom_empresa from PERSONA_EMPRESA_RECAUDO perReca
                                        JOIN EMPRESA_RECAUDO empRec on perreca.cod_empresa = emprec.cod_empresa
                                        where perreca.cod_persona = " + cod_persona.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            EmpresaRecaudo entidad = new EmpresaRecaudo();
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt64(resultado["COD_EMPRESA"]);
                            if (resultado["NOM_EMPRESA"] != DBNull.Value) entidad.nom_empresa = Convert.ToString(resultado["NOM_EMPRESA"]);
                            lstEntidad.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CobroCodeudorData", "ListarEmpresaRecaudo", ex);
                        return null;
                    }
                }
            }
        }

        public CobroCodeudor ModificarCobroCodeudor(CobroCodeudor pCobroCodeudor, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidcobrocodeud = cmdTransaccionFactory.CreateParameter();
                        pidcobrocodeud.ParameterName = "p_idcobrocodeud";
                        pidcobrocodeud.Value = pCobroCodeudor.idcobrocodeud;
                        pidcobrocodeud.Direction = ParameterDirection.Input;
                        pidcobrocodeud.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidcobrocodeud);

                        DbParameter pnumero_radicacion = cmdTransaccionFactory.CreateParameter();
                        pnumero_radicacion.ParameterName = "p_numero_radicacion";
                        pnumero_radicacion.Value = pCobroCodeudor.numero_radicacion;
                        pnumero_radicacion.Direction = ParameterDirection.Input;
                        pnumero_radicacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_radicacion);

                        DbParameter pcod_deudor = cmdTransaccionFactory.CreateParameter();
                        pcod_deudor.ParameterName = "p_cod_deudor";
                        pcod_deudor.Value = pCobroCodeudor.cod_deudor;
                        pcod_deudor.Direction = ParameterDirection.Input;
                        pcod_deudor.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_deudor);

                        DbParameter pfecha_cobro = cmdTransaccionFactory.CreateParameter();
                        pfecha_cobro.ParameterName = "p_fecha_cobro";
                        pfecha_cobro.Value = pCobroCodeudor.fecha_cobro;
                        pfecha_cobro.Direction = ParameterDirection.Input;
                        pfecha_cobro.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_cobro);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pCobroCodeudor.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pporcentaje = cmdTransaccionFactory.CreateParameter();
                        pporcentaje.ParameterName = "p_porcentaje";
                        if (pCobroCodeudor.porcentaje == null)
                            pporcentaje.Value = DBNull.Value;
                        else
                            pporcentaje.Value = pCobroCodeudor.porcentaje;
                        pporcentaje.Direction = ParameterDirection.Input;
                        pporcentaje.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pporcentaje);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        pvalor.Value = pCobroCodeudor.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter pcod_empresa = cmdTransaccionFactory.CreateParameter();
                        pcod_empresa.ParameterName = "p_cod_empresa";
                        if (pCobroCodeudor.cod_empresa == null)
                            pcod_empresa.Value = DBNull.Value;
                        else
                            pcod_empresa.Value = pCobroCodeudor.cod_empresa;
                        pcod_empresa.Direction = ParameterDirection.Input;
                        pcod_empresa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_empresa);

                        DbParameter pfechacrea = cmdTransaccionFactory.CreateParameter();
                        pfechacrea.ParameterName = "p_fechacrea";
                        if (pCobroCodeudor.fechacrea == null)
                            pfechacrea.Value = DBNull.Value;
                        else
                            pfechacrea.Value = pCobroCodeudor.fechacrea;
                        pfechacrea.Direction = ParameterDirection.Input;
                        pfechacrea.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechacrea);

                        DbParameter pcod_usuario = cmdTransaccionFactory.CreateParameter();
                        pcod_usuario.ParameterName = "p_cod_usuario";
                        if (pCobroCodeudor.cod_usuario == null)
                            pcod_usuario.Value = DBNull.Value;
                        else
                            pcod_usuario.Value = pCobroCodeudor.cod_usuario;
                        pcod_usuario.Direction = ParameterDirection.Input;
                        pcod_usuario.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pcod_usuario);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pCobroCodeudor.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REC_COBRO_CODE_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pCobroCodeudor;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CobroCodeudorData", "ModificarCobroCodeudor", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarCobroCodeudor(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        CobroCodeudor pCobroCodeudor = new CobroCodeudor();
                        pCobroCodeudor = ConsultarCobroCodeudor(pId, vUsuario);

                        DbParameter pidcobrocodeud = cmdTransaccionFactory.CreateParameter();
                        pidcobrocodeud.ParameterName = "p_idcobrocodeud";
                        pidcobrocodeud.Value = pCobroCodeudor.idcobrocodeud;
                        pidcobrocodeud.Direction = ParameterDirection.Input;
                        pidcobrocodeud.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidcobrocodeud);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REC_COBRO_CODE_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CobroCodeudorData", "EliminarCobroCodeudor", ex);
                    }
                }
            }
        }


        public CobroCodeudor ConsultarCobroCodeudor(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            CobroCodeudor entidad = new CobroCodeudor();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM COBRO_CODEUDOR WHERE IDCOBROCODEUD = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDCOBROCODEUD"] != DBNull.Value) entidad.idcobrocodeud = Convert.ToInt32(resultado["IDCOBROCODEUD"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["COD_DEUDOR"] != DBNull.Value) entidad.cod_deudor = Convert.ToInt64(resultado["COD_DEUDOR"]);
                            if (resultado["FECHA_COBRO"] != DBNull.Value) entidad.fecha_cobro = Convert.ToDateTime(resultado["FECHA_COBRO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["PORCENTAJE"] != DBNull.Value) entidad.porcentaje = Convert.ToDecimal(resultado["PORCENTAJE"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt32(resultado["COD_EMPRESA"]);
                            if (resultado["FECHACREA"] != DBNull.Value) entidad.fechacrea = Convert.ToDateTime(resultado["FECHACREA"]);
                            if (resultado["COD_USUARIO"] != DBNull.Value) entidad.cod_usuario = Convert.ToDecimal(resultado["COD_USUARIO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
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
                        BOExcepcion.Throw("CobroCodeudorData", "ConsultarCobroCodeudor", ex);
                        return null;
                    }
                }
            }
        }


        public List<CobroCodeudor> ListarCobroCodeudor(string filtro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<CobroCodeudor> lstCobroCodeudor = new List<CobroCodeudor>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT CO.*, v1.nombre as NombreDeudor, v1.identificacion as IdentDeudor, v.nombre as NombreCodeudor, v.identificacion as IdentCodeudor
                                        FROM COBRO_CODEUDOR CO      
                                        INNER JOIN credito CRE on CRE.numero_radicacion = CO.numero_radicacion
                                        --INNER JOIN CODEUDORES C ON C.NUMERO_RADICACION = CO.NUMERO_RADICACION
                                        INNER JOIN V_PERSONA V ON V.COD_PERSONA = CO.COD_PERSONA 
                                        INNER JOIN V_PERSONA V1 ON V1.COD_PERSONA = CO.COD_DEUDOR 
                                        WHERE CRE.estado = 'C' " + filtro + " ORDER BY CO.IDCOBROCODEUD ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            CobroCodeudor entidad = new CobroCodeudor();
                            if (resultado["IDCOBROCODEUD"] != DBNull.Value) entidad.idcobrocodeud = Convert.ToInt32(resultado["IDCOBROCODEUD"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["COD_DEUDOR"] != DBNull.Value) entidad.cod_deudor = Convert.ToInt64(resultado["COD_DEUDOR"]);
                            if (resultado["FECHA_COBRO"] != DBNull.Value) entidad.fecha_cobro = Convert.ToDateTime(resultado["FECHA_COBRO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["PORCENTAJE"] != DBNull.Value) entidad.porcentaje = Convert.ToDecimal(resultado["PORCENTAJE"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt32(resultado["COD_EMPRESA"]);
                            if (resultado["FECHACREA"] != DBNull.Value) entidad.fechacrea = Convert.ToDateTime(resultado["FECHACREA"]);
                            if (resultado["COD_USUARIO"] != DBNull.Value) entidad.cod_usuario = Convert.ToDecimal(resultado["COD_USUARIO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["NombreDeudor"] != DBNull.Value) entidad.nombre_deudor = Convert.ToString(resultado["NombreDeudor"]);
                            if (resultado["IdentDeudor"] != DBNull.Value) entidad.identificacion_deudor = Convert.ToString(resultado["IdentDeudor"]);
                            if (resultado["NombreCodeudor"] != DBNull.Value) entidad.nombre_codeudor = Convert.ToString(resultado["NombreCodeudor"]);
                            if (resultado["IdentCodeudor"] != DBNull.Value) entidad.identificacion_codeudor = Convert.ToString(resultado["IdentCodeudor"]);

                            lstCobroCodeudor.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCobroCodeudor;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CobroCodeudorData", "ListarCobroCodeudor", ex);
                        return null;
                    }
                }
            }
        }


    }
}
