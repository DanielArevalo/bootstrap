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
    public class ActaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla Credito
        /// </summary>
        public ActaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Credito dados unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<Credito> ListarCreditosActas(Credito pCredito, Usuario pUsuario, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Credito> lstCredito = new List<Credito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"With v_codeudores As (Select c.numero_radicacion, p.identificacion As identificacioncodeudor, p.primer_nombre||' '||p.segundo_nombre||' '|| p.primer_apellido||' '|| p.segundo_apellido as nombrecodeudor
                                                                From codeudores c, persona p where c.codpersona = p.cod_persona and c.idcodeud in (select min(x.idcodeud) from codeudores x where x.numero_radicacion = c.numero_radicacion))
                                       Select b.numero_radicacion, p.identificacion, T.Descripcion As tipo_identificacion, p.nombre As nombres, c.cod_linea_credito As linea, 
                                       o.nombre As oficina, c.monto_aprobado, c.numero_cuotas As plazo, Pe.Descripcion As Periodicidad, c.valor_cuota, 
                                       C.Estado, ua.nombre As asesor, cd.identificacioncodeudor, cd.nombrecodeudor,
                                       a.codacta, to_char(a.fecha,'MM/dd/yyyy')as fecha, u.nombre as usuario, ec.descripcion As nom_estado 
                                       From  actas_numero a Inner Join acta b  on a.codacta = b.codacta 
                                       Left Join credito c On c.numero_radicacion = b.numero_radicacion 
                                       Left Join Periodicidad Pe On C.Cod_Periodicidad = Pe.Cod_Periodicidad
                                       Left Join Oficina O  On O.Cod_Oficina = C.Cod_Oficina
                                       Left Join Usuarios Ua On c.cod_asesor_com = ua.codusuario     
                                       Left Join v_codeudores cd On c.numero_radicacion = cd.numero_radicacion
                                       Left Join v_persona p On c.cod_deudor = p.cod_persona
                                       Left Join Tipoidentificacion T On T.Codtipoidentificacion = P.Tipo_Identificacion
                                       Left Join usuarios u on u.codusuario = b.cod_usuario
                                       Left Join estado_credito ec On c.estado = ec.estado
                                       Where 1 = 1 " + filtro;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Credito entidad = new Credito();

                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea_credito = Convert.ToString(resultado["LINEA"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.monto = Convert.ToInt64(resultado["MONTO_APROBADO"]);
                            if (resultado["PLAZO"] != DBNull.Value) entidad.plazo = Convert.ToInt64(resultado["PLAZO"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.periodicidad = Convert.ToString(resultado["PERIODICIDAD"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fechaacta = Convert.ToString(resultado["FECHA"]);
                            if (resultado["CODACTA"] != DBNull.Value) entidad.acta = Convert.ToInt64(resultado["CODACTA"]);
                            if (resultado["ASESOR"] != DBNull.Value) entidad.NombreAsesor = Convert.ToString(resultado["ASESOR"]);
                            if (resultado["IDENTIFICACIONCODEUDOR"] != DBNull.Value) entidad.Codeudor = Convert.ToString(resultado["IDENTIFICACIONCODEUDOR"]);
                            if (resultado["NOMBRECODEUDOR"] != DBNull.Value) entidad.NombreCodeudor = Convert.ToString(resultado["NOMBRECODEUDOR"]);
                            if (resultado["NOM_ESTADO"] != DBNull.Value) entidad.nomestado = Convert.ToString(resultado["NOM_ESTADO"]);

                            lstCredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActaData", "ListarCreditosActas", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Credito dados unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<Credito> ListarCreditosReporte(Credito pCredito, Usuario pUsuario, string Acta)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Credito> lstCredito = new List<Credito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        //string sql = "Select a.*, c.codacta, To_Char(c.fecha,'" + conf.ObtenerFormatoFecha() + "') as fecha from  V_ACTA_APROBACIONCRED a Inner Join acta b on a.numero_radicacion = b.numero_radicacion Inner Join actas_numero c on c.codacta = b.codacta Where c.codacta In (" + Acta  + ") ";
                        string sql = @"With v_codeudores As (Select c.numero_radicacion, p.identificacion As identificacioncodeudor, p.primer_nombre||' '||p.segundo_nombre||' '|| p.primer_apellido||' '|| p.segundo_apellido as nombrecodeudor
                                                                From codeudores c, persona p where c.codpersona = p.cod_persona and c.idcodeud in (select min(x.idcodeud) from codeudores x where x.numero_radicacion = c.numero_radicacion))
                                       Select b.numero_radicacion, p.identificacion, T.Descripcion As tipo_identificacion, p.nombre As nombres, c.cod_linea_credito As linea, 
                                       o.nombre As oficina, c.monto_aprobado, c.numero_cuotas As plazo, Pe.Descripcion As Periodicidad, c.valor_cuota, 
                                       C.Estado, ua.nombre As asesor, cd.identificacioncodeudor, cd.nombrecodeudor, ec.descripcion As nom_estado,
                                       a.codacta, to_char(a.fecha,'MM/dd/yyyy')as fecha, u.nombre as usuario, TasaIntCteCredito(c.numero_radicacion) As Tasa, 'Nominal Anual M.V.' As tipo_tasa
                                       From  actas_numero a Inner Join acta b  on a.codacta = b.codacta 
                                       Left Join credito c On c.numero_radicacion = b.numero_radicacion 
                                       Left Join Periodicidad Pe On C.Cod_Periodicidad = Pe.Cod_Periodicidad
                                       Left Join Oficina O  On O.Cod_Oficina = C.Cod_Oficina
                                       Left Join Usuarios Ua On c.cod_asesor_com = ua.codusuario     
                                       Left Join v_codeudores cd On c.numero_radicacion = cd.numero_radicacion
                                       Left Join estado_credito ec On c.estado = ec.estado
                                       Left Join v_persona p On c.cod_deudor = p.cod_persona        
                                       Left Join Tipoidentificacion T On T.Codtipoidentificacion = P.Tipo_Identificacion
                                       Left Join usuarios u on u.codusuario = b.cod_usuario
                                       Where a.codacta In (" + Acta + ") ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Credito entidad = new Credito();

                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea_credito = Convert.ToString(resultado["LINEA"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.monto = Convert.ToInt64(resultado["MONTO_APROBADO"]);
                            if (resultado["PLAZO"] != DBNull.Value) entidad.plazo = Convert.ToInt64(resultado["PLAZO"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.periodicidad = Convert.ToString(resultado["PERIODICIDAD"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                            if (resultado["NOM_ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["NOM_ESTADO"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fechaacta = Convert.ToString(resultado["FECHA"]);
                            if (resultado["CODACTA"] != DBNull.Value) entidad.acta = Convert.ToInt64(resultado["CODACTA"]);
                            if (resultado["ASESOR"] != DBNull.Value) entidad.NombreAsesor = Convert.ToString(resultado["ASESOR"]);
                            if (resultado["TASA"] != DBNull.Value) entidad.tasa = Convert.ToInt64(resultado["TASA"]);
                            if (resultado["TIPO_TASA"] != DBNull.Value) entidad.desc_tasa = Convert.ToString(resultado["TIPO_TASA"]);
                            if (resultado["IDENTIFICACIONCODEUDOR"] != DBNull.Value) entidad.Codeudor = Convert.ToString(resultado["IDENTIFICACIONCODEUDOR"]);
                            if (resultado["NOMBRECODEUDOR"] != DBNull.Value) entidad.NombreCodeudor = Convert.ToString(resultado["NOMBRECODEUDOR"]);
                            
                            lstCredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActaData", "ListarCreditosReporte", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Credito dados unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<Credito> ListarCreditos(Credito pCredito, Usuario pUsuario, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Credito> lstCredito = new List<Credito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql1 = "Select * from  V_ACTA_APROBACIONCRED a where a.cod_linea_credito Not In (Select cod_linea_credito From parametros_linea Where cod_parametro = 320 And valor = '1') and  a.numero_radicacion Not In (select NUMERO_RADICACION from acta) " + filtro;
                        string sql2 = " order by a.COD_OFICINA asc";

                        string sql = sql1 + sql2;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Credito entidad = new Credito();

                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea_credito = Convert.ToString(resultado["LINEA"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.monto = Convert.ToInt64(resultado["MONTO_APROBADO"]);
                            if (resultado["PLAZO"] != DBNull.Value) entidad.plazo = Convert.ToInt64(resultado["PLAZO"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.periodicidad = Convert.ToString(resultado["PERIODICIDAD"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estadocierre = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["ASESOR"] != DBNull.Value) entidad.NombreAsesor = Convert.ToString(resultado["ASESOR"]);
                            if (resultado["IDENTIFICACIONCODEUDOR"] != DBNull.Value) entidad.Codeudor = Convert.ToString(resultado["IDENTIFICACIONCODEUDOR"]);
                            if (resultado["NOMBRECODEUDOR"] != DBNull.Value) entidad.NombreCodeudor= Convert.ToString(resultado["NOMBRECODEUDOR"]);
                            if (resultado["NOM_ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["NOM_ESTADO"]);
                            if (resultado["TASA"] != DBNull.Value) entidad.tasa = Convert.ToDecimal(resultado["TASA"]);
                            if (resultado["TIPO_TASA"] != DBNull.Value) entidad.desc_tasa = Convert.ToString(resultado["TIPO_TASA"]);
                            lstCredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActaData", "ListarCreditos", ex);
                        return null;
                    }
                }
            }
        }
        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Credito dados unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<Credito> ListarCreditosUsuarios(Credito pCredito, Usuario pUsuario, String filtro,Int64 oficina)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Credito> lstCredito = new List<Credito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * from  V_ACTA_APROBACIONCRED a where  a.numero_radicacion not in( select NUMERO_RADICACION from acta) " + filtro + "and COD_OFICINA = " + oficina;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Credito entidad = new Credito();

                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea_credito = Convert.ToString(resultado["LINEA"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.monto = Convert.ToInt64(resultado["MONTO_APROBADO"]);
                            if (resultado["PLAZO"] != DBNull.Value) entidad.plazo = Convert.ToInt64(resultado["PLAZO"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.periodicidad = Convert.ToString(resultado["PERIODICIDAD"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["ASESOR"] != DBNull.Value) entidad.NombreAsesor = Convert.ToString(resultado["ASESOR"]);
                            lstCredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActaData", "ListarCreditosUsuario", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Credito dados unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<Credito> ListarCreditosRestructurados(Credito pCredito, Usuario pUsuario, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Credito> lstCredito = new List<Credito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql1 = "Select * from  V_ACTA_APROBACIONCRED a where a.cod_linea_credito=310  and a.numero_radicacion  Not In ( select  NUMERO_RADICACION from acta) " + filtro;
                        string sql2 = " order by a.COD_OFICINA asc";

                        string sql = sql1 + sql2;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Credito entidad = new Credito();

                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea_credito = Convert.ToString(resultado["LINEA"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.monto = Convert.ToInt64(resultado["MONTO_APROBADO"]);
                            if (resultado["PLAZO"] != DBNull.Value) entidad.plazo = Convert.ToInt64(resultado["PLAZO"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.periodicidad = Convert.ToString(resultado["PERIODICIDAD"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["ASESOR"] != DBNull.Value) entidad.NombreAsesor = Convert.ToString(resultado["ASESOR"]);
                            if (resultado["IDENTIFICACIONCODEUDOR"] != DBNull.Value) entidad.Codeudor = Convert.ToString(resultado["IDENTIFICACIONCODEUDOR"]);
                            if (resultado["NOMBRECODEUDOR"] != DBNull.Value) entidad.NombreCodeudor = Convert.ToString(resultado["NOMBRECODEUDOR"]);
                            lstCredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActaData", "ListarCreditos", ex);
                        return null;
                    }
                }
            }
        }
        // <summary>
        /// Obtiene una lista de Entidades de la tabla Credito dados unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<Credito> ListarActas(Credito pCredito, Usuario pUsuario, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Credito> lstCredito = new List<Credito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql1 = "Select distinct a.codacta, to_char(b.fecha,'MM/dd/yyyy') as fecha From actas_numero b Inner Join acta a On b.codacta = a.codacta " + filtro ;

                        string sql2 = " order by a.codacta desc";

                        string sql = sql1 + sql2;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Credito entidad = new Credito();

                            if (resultado["CODACTA"] != DBNull.Value) entidad.acta = Convert.ToInt64(resultado["CODACTA"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fechaacta = Convert.ToString(resultado["FECHA"]);

                            lstCredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActaData", "ListarActas", ex);
                        return null;
                    }
                }
            }
        }



        /// <summary>
        /// Crea un registro en la tabla Diligencia de la base de datos
        /// </summary>
        /// <param name="pDiligencia">Entidad Diligencia</param>
        /// <returns>Entidad Diligencia creada</returns>
        public Credito CrearActa(Credito pActa, DateTime fechaaprobacion, Int64 codusuario, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter P_CONSECUTIVO = cmdTransaccionFactory.CreateParameter();
                        P_CONSECUTIVO.ParameterName = "P_CONSECUTIVO";
                        P_CONSECUTIVO.Value = 0;
                        P_CONSECUTIVO.DbType = DbType.Int64;
                        P_CONSECUTIVO.Direction = ParameterDirection.Output;   

                        DbParameter p_NUMERO_RADICACION = cmdTransaccionFactory.CreateParameter();
                        p_NUMERO_RADICACION.ParameterName = "p_NUMERO_RADICACION";
                        p_NUMERO_RADICACION.Value = pActa.numero_radicacion;
                        p_NUMERO_RADICACION.Direction = ParameterDirection.Input;

                        DbParameter P_COD_USUARIO = cmdTransaccionFactory.CreateParameter();
                        P_COD_USUARIO.ParameterName = "P_COD_USUARIO";
                        P_COD_USUARIO.Direction = ParameterDirection.Input;
                        P_COD_USUARIO.Value = codusuario;

                        DbParameter P_CODACTA = cmdTransaccionFactory.CreateParameter();
                        P_CODACTA.ParameterName = "P_CODACTA";
                        P_CODACTA.Direction = ParameterDirection.Input;
                        P_CODACTA.Value = pActa.acta;

                        cmdTransaccionFactory.Parameters.Add(P_CONSECUTIVO);
                        cmdTransaccionFactory.Parameters.Add(p_NUMERO_RADICACION);
                        cmdTransaccionFactory.Parameters.Add(P_COD_USUARIO);
                        cmdTransaccionFactory.Parameters.Add(P_CODACTA);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_ACTAS_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                                  
                        return pActa;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActaData", "CrearActa", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un dato de la tabla general para cargo gerente 
        /// </summary>
        /// <param name="pId">identificador de General</param>
        /// <returns>Parametro consultada</returns>
        public Credito ConsultarParametrocargoGerente(Usuario pUsuario)
        {
            DbDataReader resultado;
            Credito entidad = new Credito();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select valor FROM GENERAL WHERE CODIGO=402";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["valor"] != DBNull.Value) entidad.paramcargo= Convert.ToInt64(resultado["valor"]);
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActaData", "ConsultarParametrocargoGerente", ex);
                        return null;
                    }
                }
            }
        }
        /// <summary>
        /// Obtiene un dato de la tabla general para Cargo Comite de Credito n1
        /// </summary>
        /// <param name="pId">identificador de General</param>
        /// <returns>Parametro consultada</returns>
        public Credito ConsultarParametrocargoComitedecreditoniv1(Usuario pUsuario)
        {
            DbDataReader resultado;
            Credito entidad = new Credito();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select valor FROM GENERAL WHERE CODIGO=403";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["valor"] != DBNull.Value) entidad.paramcargo = Convert.ToInt64(resultado["valor"]);
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActaData", "ConsultarParametrocargoComitedecreditoniv1", ex);
                        return null;
                    }
                }
            }
        }
        /// <summary>
        /// Obtiene un dato de la tabla general para Cargo Comite de Credito n1
        /// </summary>
        /// <param name="pId">identificador de General</param>
        /// <returns>Parametro consultada</returns>
        public Credito ConsultarParametroReestructurado(Usuario pUsuario)
        {
            DbDataReader resultado;
            Credito entidad = new Credito();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select valor FROM GENERAL WHERE CODIGO=430";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["valor"] != DBNull.Value) entidad.paramrestruct = Convert.ToString(resultado["valor"]);
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActaData", "ConsultarParametroReestructurado", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un dato de la tabla general para Cargo Comite de Credito n1
        /// </summary>
        /// <param name="pId">identificador de General</param>
        /// <returns>Parametro consultada</returns>
        public Credito ConsultarParametrocargoComitedecreditoniv4(Usuario pUsuario)
        {
            DbDataReader resultado;
            Credito entidad = new Credito();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select valor FROM GENERAL WHERE CODIGO=404";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["valor"] != DBNull.Value) entidad.paramcargo = Convert.ToInt64(resultado["valor"]);
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActaData", "ConsultarParametrocargoComitedecreditoniv4", ex);
                        return null;
                    }
                }
            }
        }
        /// <summary>
        /// Obtiene un registro de la tabla acta de la base de datos
        /// </summary>
        /// <param name="pId">identificador del registro</param>
        /// <returns>Acta consultada</returns>
        public Credito ConsultarActa(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Credito entidad = new Credito();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select a.*,c.codacta,to_char(c.fecha,'MM/dd/yyyy')as fecha from  V_ACTA_APROBACIONCRED a inner join  XPINNADM.acta b  on a.NUMERO_RADICACION=b.DESCRIPCION inner join  XPINNADM.actas_numero c on c.CODACTA=b.CODACTA where b.codacta =" + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea_credito = Convert.ToString(resultado["LINEA"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.monto = Convert.ToInt64(resultado["MONTO_APROBADO"]);
                            if (resultado["PLAZO"] != DBNull.Value) entidad.plazo = Convert.ToInt64(resultado["PLAZO"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.periodicidad = Convert.ToString(resultado["PERIODICIDAD"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["IDENTIFICACIONCODEUDOR"] != DBNull.Value) entidad.Codeudor = Convert.ToString(resultado["IDENTIFICACIONCODEUDOR"]);
                            if (resultado["NOMBRECODEUDOR"] != DBNull.Value) entidad.NombreCodeudor = Convert.ToString(resultado["NOMBRECODEUDOR"]);
                           
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
                        BOExcepcion.Throw("ActaData", "ConsultarActa", ex);
                        return null;
                    }

                }
            }
        }

        /// <summary>
        /// Obtiene un registro de la tabla acta de la base de datos
        /// </summary>
        /// <param name="pId">identificador del registro</param>
        /// <returns>Acta consultada</returns>
        public Credito ConsultarAprobadorActa(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Credito entidad = new Credito();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"With fuck As (Select C.Numero_Radicacion, P.Identificacion, T.Descripcion As tipo_identificacion, P.Nombre As nombres, L.Cod_Linea_Credito, 
                                                        l.nombre As linea, c.cod_oficina, o.nombre As oficina, c.monto_solicitado, c.monto_aprobado, c.numero_cuotas As plazo, c.valor_cuota, 
                                                        C.Estado , Pe.Descripcion As Periodicidad, trunc(c.fecha_aprobacion), TasaIntCteCredito(c.numero_radicacion) tasa_anual, 
                                                        'Nominal Anual M.V.', u.nombre, con.cod_persona, ec.descripcion As nom_estado
                                                        From V_Persona P, Credito C
                                                        Left Join Tipomoneda Tm On C.Cod_Moneda = Tm.Cod_Moneda
                                                        Left Join Atributoscredito a On c.numero_radicacion = a.numero_radicacion And a.cod_atr = 2
                                                        Left Join TipoTasa tp On tp.cod_tipo_tasa = a.tipo_tasa
                                                        Left Join Usuarios U On c.cod_asesor_com = u.codusuario
                                                        Left Join controlcreditos con On con.numero_radicacion = c.numero_radicacion
                                                        Left Join estado_credito ec On c.estado = ec.estado, 
                                                        Lineascredito L, Periodicidad Pe, Tipoidentificacion T, Oficina O
                                                        where p.cod_persona = c.cod_deudor
                                                        and c.cod_linea_credito = l.cod_linea_credito
                                                        And C.Cod_Periodicidad = Pe.Cod_Periodicidad
                                                        And T.Codtipoidentificacion = P.Tipo_Identificacion
                                                        And O.Cod_Oficina = C.Cod_Oficina
                                                        And con.codtipoproceso In(Select codtipoproceso From tipoprocesos Where descripcion Like 'Aprobado%' Or descripcion Like 'Negado%' Or descripcion Like 'Aplazado%'))
                                     Select a.*, c.codacta, to_char(c.fecha,'MM/dd/yyyy')as fecha, u.nombre as usuario 
                                     From  fuck a 
                                     Inner Join acta b on a.numero_radicacion = b.numero_radicacion 
                                     Inner Join actas_numero c on c.codacta = b.codacta Inner Join usuarios u  on u.codusuario = b.cod_usuario 
                                     Where c.codacta = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea_credito = Convert.ToString(resultado["LINEA"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.monto = Convert.ToInt64(resultado["MONTO_APROBADO"]);
                            if (resultado["PLAZO"] != DBNull.Value) entidad.plazo = Convert.ToInt64(resultado["PLAZO"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.periodicidad = Convert.ToString(resultado["PERIODICIDAD"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
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
                        BOExcepcion.Throw("ActaData", "ConsultarAprobadorActa", ex);
                        return null;
                    }

                }
            }
        }

        public Int64? CrearActaNumero(String idacta, DateTime fechaaprobacion, Int64? codoficina, Int64 codusuario, Usuario pUsuario)
        {
            Int64? pActa = null;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter P_CODACTA = cmdTransaccionFactory.CreateParameter();
                        P_CODACTA.ParameterName = "P_CODACTA";
                        P_CODACTA.Value = pActa;
                        P_CODACTA.DbType = DbType.Int64;
                        P_CODACTA.Direction = ParameterDirection.Output;

                        DbParameter p_FECHA = cmdTransaccionFactory.CreateParameter();
                        p_FECHA.ParameterName = "p_FECHA";
                        p_FECHA.Direction = ParameterDirection.Input;
                        p_FECHA.DbType = DbType.Date;
                        p_FECHA.Value = fechaaprobacion;

                        DbParameter P_IDACTA = cmdTransaccionFactory.CreateParameter();
                        P_IDACTA.ParameterName = "P_IDACTA";
                        P_IDACTA.Direction = ParameterDirection.Input;
                        P_IDACTA.DbType = DbType.AnsiStringFixedLength;
                        P_IDACTA.Size = 20;
                        if (idacta != null)
                            P_IDACTA.Value = idacta;
                        else
                            P_IDACTA.Value = DBNull.Value;

                        DbParameter P_COD_OFICINA = cmdTransaccionFactory.CreateParameter();
                        P_COD_OFICINA.ParameterName = "P_COD_OFICINA";
                        P_COD_OFICINA.Direction = ParameterDirection.Input;
                        P_COD_OFICINA.DbType = DbType.Int64;
                        if (codoficina != null)
                            P_COD_OFICINA.Value = codoficina;
                        else
                            P_COD_OFICINA.Value = DBNull.Value;

                        DbParameter P_COD_USUARIO = cmdTransaccionFactory.CreateParameter();
                        P_COD_USUARIO.ParameterName = "P_COD_USUARIO";
                        P_COD_USUARIO.Direction = ParameterDirection.Input;
                        P_COD_USUARIO.Value = codusuario;

                        cmdTransaccionFactory.Parameters.Add(P_CODACTA);
                        cmdTransaccionFactory.Parameters.Add(p_FECHA);
                        cmdTransaccionFactory.Parameters.Add(P_IDACTA);
                        cmdTransaccionFactory.Parameters.Add(P_COD_OFICINA);
                        cmdTransaccionFactory.Parameters.Add(P_COD_USUARIO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_ACTASNUMER_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pActa = Convert.ToInt64(P_CODACTA.Value);

                        return pActa;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActaData", "CrearActaNumero", ex);
                        return null;
                    }
                }
            }
        }





    }   
}