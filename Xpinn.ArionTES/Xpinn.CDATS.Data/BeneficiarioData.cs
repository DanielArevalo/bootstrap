using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.CDATS.Entities;

namespace Xpinn.CDATS.Data
{
    /// <summary>
    /// Objeto de acceso a datos para centros de costo
    /// </summary>    
    public class BeneficiarioData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para centros de costo
        /// </summary>
        public BeneficiarioData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public List<Beneficiario> ListarParentesco(Beneficiario pBeneficiario, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Beneficiario> lstBeneficiario = new List<Beneficiario>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select * from parentescos order by codparentesco";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Beneficiario entidad = new Beneficiario();
                            if (resultado["CODPARENTESCO"] != DBNull.Value) entidad.codparentesco = Convert.ToInt32(resultado["CODPARENTESCO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                           
                            lstBeneficiario.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstBeneficiario;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BeneficiarioData", "ListarParentesco", ex);
                        return null;
                    }
                }
            }
        }

        #region BENEFICIARIO DE cdats

        public List<Beneficiario> ConsultarBeneficiarioCdat(String pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Beneficiario> lstBeneficiario = new List<Beneficiario>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM BENEFICIARIO_CDAT WHERE NUMERO_CDAT = '" + pId +"'";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Beneficiario entidad = new Beneficiario();
                            if (resultado["IDBENEFICIARIO"] != DBNull.Value) entidad.idbeneficiario = Convert.ToInt32(resultado["IDBENEFICIARIO"]);
                            if (resultado["NUMERO_CDAT"] != DBNull.Value) entidad.numero_cdat = Convert.ToString(resultado["NUMERO_CDAT"]);
                            if (resultado["IDENTIFICACION_BEN"] != DBNull.Value) entidad.identificacion_ben = Convert.ToString(resultado["IDENTIFICACION_BEN"]);
                            if (resultado["TIPO_IDENTIFICACION_BEN"] != DBNull.Value) entidad.tipo_identificacion_ben = Convert.ToInt32(resultado["TIPO_IDENTIFICACION_BEN"]);
                            if (resultado["NOMBRE_BEN"] != DBNull.Value) entidad.nombre_ben = Convert.ToString(resultado["NOMBRE_BEN"]);
                            if (resultado["FECHA_NACIMIENTO_BEN"] != DBNull.Value)
                            {
                                entidad.fecha_nacimiento_ben = Convert.ToDateTime(resultado["FECHA_NACIMIENTO_BEN"]);

                                try
                                {
                                    DateTimeHelper dateHelper = new DateTimeHelper();
                                    entidad.edad = Convert.ToInt32(dateHelper.DiferenciaEntreDosFechasAños(DateTime.Today, entidad.fecha_nacimiento_ben.Value));
                                }
                                catch (Exception) // Si no logro calcular la edad en base a su fecha de nacimiento, uso el dato que este en la tabla
                                {                 // Se ha dado el caso que la edad es inconsistente a la fecha de nacimiento  
                                    if (resultado["EDAD_BEN"] != DBNull.Value) entidad.edad = Convert.ToInt32(resultado["EDAD_BEN"]);
                                }
                            }
                            if (resultado["PARENTESCO"] != DBNull.Value) entidad.parentesco = Convert.ToInt32(resultado["PARENTESCO"]);
                            if (resultado["PORCENTAJE_BEN"] != DBNull.Value) entidad.porcentaje_ben = Convert.ToDecimal(resultado["PORCENTAJE_BEN"]);
                            if (resultado["SEXO_BEN"] != DBNull.Value) entidad.sexo = Convert.ToString(resultado["SEXO_BEN"]);
                            lstBeneficiario.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstBeneficiario;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BeneficiarioData", "ConsultarBeneficiarioCdat", ex);
                        return null;
                    }
                }
            }
        }

        public void EliminarBeneficiarioCdat(String pFiltro, Int64 pNumero_Cuenta, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidbeneficiarios = cmdTransaccionFactory.CreateParameter();
                        pidbeneficiarios.ParameterName = "p_idbeneficiarios";
                        pidbeneficiarios.Value = pFiltro;
                        pidbeneficiarios.Direction = ParameterDirection.Input;
                        pidbeneficiarios.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidbeneficiarios);

                        DbParameter pNumeroCuenta = cmdTransaccionFactory.CreateParameter();
                        pNumeroCuenta.ParameterName = "p_numero_cuenta";
                        pNumeroCuenta.Value = pNumero_Cuenta;
                        pNumeroCuenta.Direction = ParameterDirection.Input;
                        pNumeroCuenta.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pNumeroCuenta);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CDA_BENEFICIAR_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BeneficiarioData", "EliminarBeneficiarioCdat", ex);
                    }
                }
            }
        }

        public Beneficiario ModificarBeneficiarioCdat(Beneficiario pBeneficiario, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidbeneficiario = cmdTransaccionFactory.CreateParameter();
                        pidbeneficiario.ParameterName = "p_idbeneficiario";
                        pidbeneficiario.Value = pBeneficiario.idbeneficiario;
                        pidbeneficiario.Direction = ParameterDirection.Input;
                        pidbeneficiario.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidbeneficiario);

                        DbParameter pnumero_cuenta = cmdTransaccionFactory.CreateParameter();
                        pnumero_cuenta.ParameterName = "p_numero_cuenta";
                        pnumero_cuenta.Value = pBeneficiario.numero_cdat;
                        pnumero_cuenta.Direction = ParameterDirection.Input;
                        pnumero_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumero_cuenta);

                        DbParameter pidentificacion_ben = cmdTransaccionFactory.CreateParameter();
                        pidentificacion_ben.ParameterName = "p_identificacion_ben";
                        if (pBeneficiario.identificacion_ben == null)
                            pidentificacion_ben.Value = DBNull.Value;
                        else
                            pidentificacion_ben.Value = pBeneficiario.identificacion_ben;
                        pidentificacion_ben.Direction = ParameterDirection.Input;
                        pidentificacion_ben.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidentificacion_ben);

                        DbParameter ptipo_identificacion_ben = cmdTransaccionFactory.CreateParameter();
                        ptipo_identificacion_ben.ParameterName = "p_tipo_identificacion_ben";
                        if (pBeneficiario.tipo_identificacion_ben == null)
                            ptipo_identificacion_ben.Value = DBNull.Value;
                        else
                            ptipo_identificacion_ben.Value = pBeneficiario.tipo_identificacion_ben;
                        ptipo_identificacion_ben.Direction = ParameterDirection.Input;
                        ptipo_identificacion_ben.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_identificacion_ben);

                        DbParameter pnombre_ben = cmdTransaccionFactory.CreateParameter();
                        pnombre_ben.ParameterName = "p_nombre_ben";
                        if (pBeneficiario.nombre_ben == null)
                            pnombre_ben.Value = DBNull.Value;
                        else
                            pnombre_ben.Value = pBeneficiario.nombre_ben;
                        pnombre_ben.Direction = ParameterDirection.Input;
                        pnombre_ben.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre_ben);

                        DbParameter pfecha_nacimiento_ben = cmdTransaccionFactory.CreateParameter();
                        pfecha_nacimiento_ben.ParameterName = "p_fecha_nacimiento_ben";
                        if (pBeneficiario.fecha_nacimiento_ben == null)
                            pfecha_nacimiento_ben.Value = DBNull.Value;
                        else
                            pfecha_nacimiento_ben.Value = pBeneficiario.fecha_nacimiento_ben;
                        pfecha_nacimiento_ben.Direction = ParameterDirection.Input;
                        pfecha_nacimiento_ben.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_nacimiento_ben);

                        DbParameter pparentesco = cmdTransaccionFactory.CreateParameter();
                        pparentesco.ParameterName = "p_parentesco";
                        if (pBeneficiario.parentesco == null)
                            pparentesco.Value = DBNull.Value;
                        else
                            pparentesco.Value = pBeneficiario.parentesco;
                        pparentesco.Direction = ParameterDirection.Input;
                        pparentesco.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pparentesco);

                        DbParameter pporcentaje_ben = cmdTransaccionFactory.CreateParameter();
                        pporcentaje_ben.ParameterName = "p_porcentaje_ben";
                        if (pBeneficiario.porcentaje_ben == null)
                            pporcentaje_ben.Value = DBNull.Value;
                        else
                            pporcentaje_ben.Value = pBeneficiario.porcentaje_ben;
                        pporcentaje_ben.Direction = ParameterDirection.Input;
                        pporcentaje_ben.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pporcentaje_ben);

                        DbParameter p_edad_ben = cmdTransaccionFactory.CreateParameter();
                        p_edad_ben.ParameterName = "p_edad_ben";
                        if (pBeneficiario.edad == null)
                            p_edad_ben.Value = DBNull.Value;
                        else
                            p_edad_ben.Value = pBeneficiario.edad;
                        p_edad_ben.Direction = ParameterDirection.Input;
                        p_edad_ben.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_edad_ben);

                        DbParameter p_sexo_ben = cmdTransaccionFactory.CreateParameter();
                        p_sexo_ben.ParameterName = "p_sexo_ben";
                        p_sexo_ben.Value = pBeneficiario.sexo;
                        p_sexo_ben.Direction = ParameterDirection.Input;
                        p_sexo_ben.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_sexo_ben);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CDA_BENEFICIAR_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        if (vUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pBeneficiario, "BENEFICIARIO", vUsuario, Accion.Crear.ToString());

                        return pBeneficiario;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BeneficiarioData", "ModificarBeneficiarioCdat", ex);
                        return null;
                    }
                }
            }
        }

        public Beneficiario CrearBeneficiarioCdat(Beneficiario pBeneficiario, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidbeneficiario = cmdTransaccionFactory.CreateParameter();
                        pidbeneficiario.ParameterName = "p_idbeneficiario";
                        pidbeneficiario.Value = pBeneficiario.idbeneficiario;
                        pidbeneficiario.Direction = ParameterDirection.Output;
                        pidbeneficiario.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidbeneficiario);

                        DbParameter pnumero_cuenta = cmdTransaccionFactory.CreateParameter();
                        pnumero_cuenta.ParameterName = "p_numero_cuenta";
                        pnumero_cuenta.Value = pBeneficiario.numero_cdat;
                        pnumero_cuenta.Direction = ParameterDirection.Input;
                        pnumero_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumero_cuenta);

                        DbParameter pidentificacion_ben = cmdTransaccionFactory.CreateParameter();
                        pidentificacion_ben.ParameterName = "p_identificacion_ben";
                        if (pBeneficiario.identificacion_ben == null)
                            pidentificacion_ben.Value = DBNull.Value;
                        else
                            pidentificacion_ben.Value = pBeneficiario.identificacion_ben;
                        pidentificacion_ben.Direction = ParameterDirection.Input;
                        pidentificacion_ben.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidentificacion_ben);

                        DbParameter ptipo_identificacion_ben = cmdTransaccionFactory.CreateParameter();
                        ptipo_identificacion_ben.ParameterName = "p_tipo_identificacion_ben";
                        if (pBeneficiario.tipo_identificacion_ben == null)
                            ptipo_identificacion_ben.Value = DBNull.Value;
                        else
                            ptipo_identificacion_ben.Value = pBeneficiario.tipo_identificacion_ben;
                        ptipo_identificacion_ben.Direction = ParameterDirection.Input;
                        ptipo_identificacion_ben.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_identificacion_ben);

                        DbParameter pnombre_ben = cmdTransaccionFactory.CreateParameter();
                        pnombre_ben.ParameterName = "p_nombre_ben";
                        if (pBeneficiario.nombre_ben == null)
                            pnombre_ben.Value = DBNull.Value;
                        else
                            pnombre_ben.Value = pBeneficiario.nombre_ben;
                        pnombre_ben.Direction = ParameterDirection.Input;
                        pnombre_ben.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre_ben);

                        DbParameter pfecha_nacimiento_ben = cmdTransaccionFactory.CreateParameter();
                        pfecha_nacimiento_ben.ParameterName = "p_fecha_nacimiento_ben";
                        if (pBeneficiario.fecha_nacimiento_ben == null)
                            pfecha_nacimiento_ben.Value = DBNull.Value;
                        else
                            pfecha_nacimiento_ben.Value = pBeneficiario.fecha_nacimiento_ben;
                        pfecha_nacimiento_ben.Direction = ParameterDirection.Input;
                        pfecha_nacimiento_ben.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_nacimiento_ben);

                        DbParameter pparentesco = cmdTransaccionFactory.CreateParameter();
                        pparentesco.ParameterName = "p_parentesco";
                        if (pBeneficiario.parentesco == null)
                            pparentesco.Value = DBNull.Value;
                        else
                            pparentesco.Value = pBeneficiario.parentesco;
                        pparentesco.Direction = ParameterDirection.Input;
                        pparentesco.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pparentesco);

                        DbParameter pporcentaje_ben = cmdTransaccionFactory.CreateParameter();
                        pporcentaje_ben.ParameterName = "p_porcentaje_ben";
                        if (pBeneficiario.porcentaje_ben == null)
                            pporcentaje_ben.Value = DBNull.Value;
                        else
                            pporcentaje_ben.Value = pBeneficiario.porcentaje_ben;
                        pporcentaje_ben.Direction = ParameterDirection.Input;
                        pporcentaje_ben.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pporcentaje_ben);

                        DbParameter p_edad_ben = cmdTransaccionFactory.CreateParameter();
                        p_edad_ben.ParameterName = "p_edad_ben";
                        if (pBeneficiario.edad == null)
                            p_edad_ben.Value = DBNull.Value;
                        else
                            p_edad_ben.Value = pBeneficiario.edad;
                        p_edad_ben.Direction = ParameterDirection.Input;
                        p_edad_ben.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_edad_ben);

                        DbParameter p_sexo_ben = cmdTransaccionFactory.CreateParameter();
                        p_sexo_ben.ParameterName = "p_sexo_ben";
                        p_sexo_ben.Value = pBeneficiario.sexo;
                        p_sexo_ben.Direction = ParameterDirection.Input;
                        p_sexo_ben.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_sexo_ben);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CDA_BENEFICIAR_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        if (vUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pBeneficiario, "BENEFICIARIO", vUsuario, Accion.Crear.ToString());

                        pBeneficiario.idbeneficiario = Convert.ToInt32(pidbeneficiario.Value);
                        return pBeneficiario;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BeneficiarioData", "CrearBeneficiarioCdat", ex);
                        return null;
                    }
                }
            }
        }

        #endregion

    }
}
