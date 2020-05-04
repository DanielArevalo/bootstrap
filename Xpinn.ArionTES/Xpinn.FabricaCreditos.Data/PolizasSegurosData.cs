using System;
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
    /// Objeto de acceso a datos para la tabla POLIZASSEGUROS
    /// </summary>
    public class PolizasSegurosData: GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para Polizas
        /// </summary>
        public PolizasSegurosData()
        {
           dbConnectionFactory = new ConnectionDataBase();
        }
         /// <summary>
        /// Obtiene la lista de PolizasSeguros dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de PolizasSeguros obtenidos</returns>
        public List<PolizasSeguros> ListarPolizasSeguros(PolizasSeguros pPoliza, Usuario pUsuario, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            PolizasSeguros entidad = null;
            List<PolizasSeguros> lstPolizasSeguros = new List<PolizasSeguros>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * from v_consultapolizas " + filtro;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();


                        while (resultado.Read())
                        {
                            entidad = new PolizasSeguros();
                            //Asociar todos los valores a la entidad
                            if (resultado["cod_poliza"] != DBNull.Value) entidad.cod_poliza = Convert.ToInt64(resultado["cod_poliza"]);
                            if (resultado["numero_radicacion"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["numero_radicacion"]);
                            if (resultado["nombre_deudor"] != DBNull.Value) entidad.nombre_deudor = Convert.ToString(resultado["nombre_deudor"]);
                            if (resultado["monto_desembolsado"] != DBNull.Value) entidad.monto_desembolsado = Convert.ToInt64(resultado["monto_desembolsado"]);
                            if (resultado["valor_prima_mensual"] != DBNull.Value) entidad.valor_prima_mensual = Convert.ToInt64(resultado["valor_prima_mensual"]);
                            if (resultado["valor_prima_total"] != DBNull.Value) entidad.valor_prima_total = Convert.ToInt64(resultado["valor_prima_total"]);
                            lstPolizasSeguros.Add(entidad);
                        }
                        if (lstPolizasSeguros.Count == 0)
                        {
                            throw new ExceptionBusiness("No existe información con dichos criterios de búsqueda.");
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPolizasSeguros;
                        
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PolizasSegurosData", "ListarPolizasSeguros", ex);
                        return null;
                    }

                }
            }
        }


        public List<PolizasSeguros> ListarPolizassinSeguros(PolizasSeguros pPoliza, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            PolizasSeguros entidad = null;
            List<PolizasSeguros> lstPolizasSeguros = new List<PolizasSeguros>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select   p.cod_persona, c.valor_cuota,   c.numero_cuotas,  c.monto_solicitado,  c.numero_radicacion,  p.identificacion,  p.nombre as nombres,  p.primer_apellido,  p.segundo_apellido,  c.cod_linea_credito, p.cod_oficina, (select a.SNOMBRE1||' '||a.SNOMBRE2||' '||a.SAPELLIDO1||' '||a.SAPELLIDO2 from asejecutivos a where a.ICODIGO=c.cod_asesor_com)as nombre_asesor,(select ofi.nombre from oficina ofi where p.cod_oficina=ofi.cod_oficina) as oficina  From v_persona p , credito c where p.cod_persona=c.cod_deudor and estado not in('D','P','T','N') ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();


                        while (resultado.Read())
                        {
                            entidad = new PolizasSeguros();
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToInt64(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.Nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["nombre_asesor"] != DBNull.Value) entidad.nombre_asesor = Convert.ToString(resultado["nombre_asesor"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["MONTO_SOLICITADO"] != DBNull.Value) entidad.monto = Convert.ToInt64(resultado["MONTO_SOLICITADO"]); else entidad.monto = 0;
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.plazo = Convert.ToInt64(resultado["NUMERO_CUOTAS"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);

                            lstPolizasSeguros.Add(entidad);
                        }
                        if (lstPolizasSeguros.Count == 0)
                        {
                            throw new ExceptionBusiness("No existe información con dichos criterios de búsqueda.");
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPolizasSeguros;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PolizasSegurosData", "ListarPolizasSeguros", ex);
                        return null;
                    }

                }
            }
        }
     

        /// <summary>
        /// Obtiene la lista de PolizasSeguros dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de PolizasSeguros obtenidos</returns>
        public List<PolizasSegurosVida> ListarPolizasSegurosvida(PolizasSegurosVida pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<PolizasSegurosVida> lstPolizasSegurosVida = new List<PolizasSegurosVida>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT COD_POLIZA, TIPO_PLAN,TIPO,INDIVIDUAL,VALOR_PRIMA from polizasegurosvida " + ObtenerFiltro(pEntidad);
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        //     cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        //   cmdTransaccionFactory.CommandText = "XPF_AS_POLIZASEGUROS_CONSULTAR";
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            PolizasSegurosVida entidad = new PolizasSegurosVida();
                            //Asociar todos los valores a la entidad
                            if (resultado["cod_poliza"] != DBNull.Value) entidad.cod_poliza = Convert.ToInt64(resultado["cod_poliza"]);
                            if (resultado["tipo_plan"] != DBNull.Value) entidad.tipo_plan = Convert.ToInt64(resultado["tipo_plan"]);
                            if (resultado["tipo"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["tipo"]);
                            if (resultado["individual"] != DBNull.Value) entidad.individual = Convert.ToString(resultado["individual"]);
                            if (resultado["valor_prima"] != DBNull.Value) entidad.valor_prima = Convert.ToInt64(resultado["valor_prima"]);
                            lstPolizasSegurosVida.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPolizasSegurosVida;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PolizasSegurosData", "ListarPolizasSegurosVida", ex);
                        return null;
                    }

                }
            }
        }
        
        /// <summary>
        /// Modifica una entidad PolizasSegurosVida en la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad PolizasSegurosVida</param>
        /// <returns>Entidad modificada</returns>
        public PolizasSegurosVida ModificarPolizasSegurosVida(PolizasSegurosVida pEntidad, Usuario pUsuario)
        {

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_poliza = cmdTransaccionFactory.CreateParameter();
                        p_cod_poliza.ParameterName = "p_cod_poliza";
                        p_cod_poliza.Value = pEntidad.cod_poliza;
                        p_cod_poliza.DbType = DbType.Int64;
                        p_cod_poliza.Size = 8;
                        p_cod_poliza.Direction = ParameterDirection.Input;

                        DbParameter p_tipo_plan = cmdTransaccionFactory.CreateParameter();
                        p_tipo_plan.ParameterName = "p_tipo_plan";
                        p_tipo_plan.Value = pEntidad.tipo_plan;
                        p_tipo_plan.DbType = DbType.Int64;
                        p_tipo_plan.Size = 8;
                        p_tipo_plan.Direction = ParameterDirection.Input;

                        DbParameter p_tipo = cmdTransaccionFactory.CreateParameter();
                        p_tipo.ParameterName = "p_tipo";
                        p_tipo.Value = pEntidad.tipo;
                        p_tipo.DbType = DbType.AnsiString;
                        p_tipo.Size = 50;
                        p_tipo.Direction = ParameterDirection.Input;

                        DbParameter p_valor_prima = cmdTransaccionFactory.CreateParameter();
                        p_valor_prima.ParameterName = "p_valor_prima";
                        p_valor_prima.Value = pEntidad.valor_prima;
                        p_valor_prima.DbType = DbType.Int64;
                        p_valor_prima.Size = 8;
                        p_valor_prima.Direction = ParameterDirection.Input;

                        DbParameter p_valor_prima_individual = cmdTransaccionFactory.CreateParameter();
                        p_valor_prima_individual.ParameterName = "p_valor_prima_individual";
                        if (pEntidad.individual == null)
                            p_valor_prima_individual.Value = DBNull.Value;
                        else
                            p_valor_prima_individual.Value = pEntidad.individual;
                        p_valor_prima_individual.DbType = DbType.Int64;
                        p_valor_prima_individual.Size = 8;
                        p_valor_prima_individual.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(p_cod_poliza);
                        cmdTransaccionFactory.Parameters.Add(p_tipo_plan);
                        cmdTransaccionFactory.Parameters.Add(p_tipo);
                        cmdTransaccionFactory.Parameters.Add(p_valor_prima);
                        cmdTransaccionFactory.Parameters.Add(p_valor_prima_individual);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_POLSEGVIDA_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        // if (pUsuario.programaGeneraLog)
                        //      DAauditoria.InsertarLog(pEntidad, pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA
                        //pEntidad.cod_poliza = Convert.ToString(p_cod_poliza.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PolizasSegurosVidaData", "ModificarPolizasSegurosVida", ex);
                        return null;
                    }
            
                }
            }
        }

         /// <summary>
        /// Crea una entidad PolizasSegurosVida en la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad PolizasSegurosVida</param>
        /// <returns>Entidad creada</returns>
        public PolizasSegurosVida InsertarPolizasSegurosVida(PolizasSegurosVida pEntidad, Usuario pUsuario)
        {

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_poliza = cmdTransaccionFactory.CreateParameter();
                        p_cod_poliza.ParameterName = "p_cod_poliza";
                        p_cod_poliza.Value = pEntidad.cod_poliza;
                        p_cod_poliza.DbType = DbType.Int64;
                        p_cod_poliza.Size = 8;
                        p_cod_poliza.Direction = ParameterDirection.Input;

                        DbParameter p_tipo_plan = cmdTransaccionFactory.CreateParameter();
                        p_tipo_plan.ParameterName = "p_tipo_plan";
                        p_tipo_plan.Value = pEntidad.tipo_plan;
                        p_tipo_plan.DbType = DbType.Int64;
                        p_tipo_plan.Size = 8;                        
                        p_tipo_plan.Direction = ParameterDirection.Input;

                        DbParameter p_tipo = cmdTransaccionFactory.CreateParameter();
                        p_tipo.ParameterName = "p_tipo";
                        p_tipo.Value = pEntidad.tipo;
                        p_tipo.DbType = DbType.AnsiString;
                        p_tipo.Size = 50;
                        p_tipo.Direction = ParameterDirection.Input;

                        DbParameter p_valor_prima = cmdTransaccionFactory.CreateParameter();
                        p_valor_prima.ParameterName = "p_valor_prima";
                        p_valor_prima.Value = pEntidad.valor_prima;
                        p_valor_prima.DbType = DbType.Int64;
                        p_valor_prima.Size = 8;
                        p_valor_prima.Direction = ParameterDirection.Input;

                        DbParameter p_individual = cmdTransaccionFactory.CreateParameter();
                        p_individual.ParameterName = "p_individual";
                        p_individual.Value = pEntidad.individual;
                        p_individual.DbType = DbType.Int64;
                        p_individual.Size = 8;
                        p_individual.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(p_cod_poliza);
                        cmdTransaccionFactory.Parameters.Add(p_tipo_plan);
                        cmdTransaccionFactory.Parameters.Add(p_tipo);
                        cmdTransaccionFactory.Parameters.Add(p_valor_prima);
                        cmdTransaccionFactory.Parameters.Add(p_individual);
                      
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_SEGVIDA_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        // if (pUsuario.programaGeneraLog)
                           // DAauditoria.InsertarLog(pEntidad, pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA
                        //pEntidad.cod_poliza = Convert.ToString(p_cod_poliza.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PolizasSegurosData", "InsertarPolizasSeguros", ex);
                        return null;
                    }

                }


            }
        }
        /// <summary>
        /// Crea una entidad PolizasSeguros en la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad PolizasSeguros</param>
        /// <returns>Entidad creada</returns>
        public PolizasSeguros InsertarPolizasSeguros(PolizasSeguros pEntidad, Usuario pUsuario)
        {

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {               

                        DbParameter p_numero_radicacion = cmdTransaccionFactory.CreateParameter();
                        p_numero_radicacion.ParameterName = "p_numero_radicacion";
                        p_numero_radicacion.Value = pEntidad.numero_radicacion;
                        p_numero_radicacion.DbType = DbType.Int64;
                       // p_numero_radicacion.Size = 50;
                        p_numero_radicacion.Direction = ParameterDirection.Input;

                        DbParameter p_num_poliza= cmdTransaccionFactory.CreateParameter();
                        p_num_poliza.ParameterName = "p_num_poliza";
                        p_num_poliza.Value = pEntidad.num_poliza;
                        p_num_poliza.DbType = DbType.Int64;
                       // p_num_poliza.Size = 50;
                        p_num_poliza.Direction = ParameterDirection.Input;

                        DbParameter p_cod_asegurado = cmdTransaccionFactory.CreateParameter();
                        p_cod_asegurado.ParameterName = "p_cod_asegurado";
                        p_cod_asegurado.Value = pEntidad.cod_asegurado;
                        p_cod_asegurado.DbType = DbType.Int64;
                        //p_cod_asegurado.Size = 50;
                        p_cod_asegurado.Direction = ParameterDirection.Input;

                        DbParameter p_cod_asesor = cmdTransaccionFactory.CreateParameter();
                        p_cod_asesor.ParameterName = "p_cod_asesor";
                        p_cod_asesor.Value = pEntidad.icodigo;
                        p_cod_asesor.DbType = DbType.Int64;
                        p_cod_asesor.Size =50;
                        p_cod_asesor.Direction = ParameterDirection.Input;

                        DbParameter p_fec_ini_vig = cmdTransaccionFactory.CreateParameter();
                        p_fec_ini_vig.ParameterName = "p_fec_ini_vig";
                        p_fec_ini_vig.Value = pEntidad.fec_ini_vig;
                        p_fec_ini_vig.DbType = DbType.Date;
                        p_fec_ini_vig.Direction = ParameterDirection.Input;

                        DbParameter p_fec_fin_vig = cmdTransaccionFactory.CreateParameter();
                        p_fec_fin_vig.ParameterName = "p_fec_fin_vig";
                        p_fec_fin_vig.Value = pEntidad.fec_fin_vig;
                        p_fec_fin_vig.DbType = DbType.Date;
                        p_fec_fin_vig.Direction = ParameterDirection.Input;

                        DbParameter p_tipo_plan= cmdTransaccionFactory.CreateParameter();
                        p_tipo_plan.ParameterName = "p_tipo_plan";
                        p_tipo_plan.Value = pEntidad.tipo_plan;
                        p_tipo_plan.DbType = DbType.Int64;
                        //p_tipo_plan.Size = 8;
                        p_tipo_plan.Direction = ParameterDirection.Input;

                        DbParameter p_vida_individual= cmdTransaccionFactory.CreateParameter();
                        p_vida_individual.ParameterName = "p_vida_individual";
                        p_vida_individual.Value = pEntidad.individual;
                        p_vida_individual.DbType = DbType.Int64;
                       // p_vida_individual.Size = 20;
                        p_vida_individual.Direction = ParameterDirection.Input;

                        DbParameter p_accidentes_individual = cmdTransaccionFactory.CreateParameter();
                        p_accidentes_individual.ParameterName = "p_accidentes_individual";
                        p_accidentes_individual.Value = pEntidad.accidentes;
                        p_accidentes_individual.DbType = DbType.Int64;
                        //p_accidentes_individual.Size = 8;
                        p_accidentes_individual.Direction = ParameterDirection.Input;

                        DbParameter p_valor_prima_mensual = cmdTransaccionFactory.CreateParameter();
                        p_valor_prima_mensual.ParameterName = "p_valor_prima_mensual";
                        p_valor_prima_mensual.Value = pEntidad.valor_prima_mensual;
                        p_valor_prima_mensual.DbType = DbType.Int64;
                        //p_valor_prima_mensual.Size = 8;
                        p_valor_prima_mensual.Direction = ParameterDirection.Input;

                        DbParameter p_valor_prima_total = cmdTransaccionFactory.CreateParameter();
                        p_valor_prima_total.ParameterName = "p_valor_prima_total";
                        p_valor_prima_total.Value = pEntidad.valor_prima_total;
                        p_valor_prima_total.DbType = DbType.Int64;
                        p_valor_prima_total.Direction = ParameterDirection.Input;

                        DbParameter p_cod_poliza = cmdTransaccionFactory.CreateParameter();
                        p_cod_poliza.ParameterName = "p_cod_poliza";
                        p_cod_poliza.DbType = DbType.Int64;
                       // p_cod_poliza.Size = 8;
                        p_cod_poliza.Direction = ParameterDirection.Output;
                        
                        cmdTransaccionFactory.Parameters.Add(p_numero_radicacion);
                        cmdTransaccionFactory.Parameters.Add(p_num_poliza);
                        cmdTransaccionFactory.Parameters.Add(p_cod_asegurado);
                        cmdTransaccionFactory.Parameters.Add(p_cod_asesor);
                        cmdTransaccionFactory.Parameters.Add(p_fec_ini_vig);
                        cmdTransaccionFactory.Parameters.Add(p_fec_fin_vig);
                        cmdTransaccionFactory.Parameters.Add(p_tipo_plan);
                        cmdTransaccionFactory.Parameters.Add(p_vida_individual);
                        cmdTransaccionFactory.Parameters.Add(p_accidentes_individual);
                        cmdTransaccionFactory.Parameters.Add(p_valor_prima_mensual);
                        cmdTransaccionFactory.Parameters.Add(p_valor_prima_total);
                        cmdTransaccionFactory.Parameters.Add(p_cod_poliza);
                        
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_POLIZASSEG_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pEntidad.cod_poliza = Convert.ToInt64(p_cod_poliza.Value);
                        // if (pUsuario.programaGeneraLog)
                        // DAauditoria.InsertarLog(pEntidad, pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA
                        //pEntidad.cod_poliza = Convert.ToString(p_cod_poliza.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PolizasSegurosData", "InsertarPolizasSeguros", ex);
                        return null;
                    }

                }


            }
        }
        /// <summary>
        /// Modificada una entidad PolizasSeguros en la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad PolizasSeguros</param>
        /// <returns>Entidad modificada</returns>
        public PolizasSeguros ModificarPolizasSeguros(PolizasSeguros pEntidad, Usuario pUsuario)
        {

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_poliza = cmdTransaccionFactory.CreateParameter();
                        p_cod_poliza.ParameterName = "p_cod_poliza";
                        p_cod_poliza.Value = pEntidad.cod_poliza;
                        p_cod_poliza.DbType = DbType.Int64;
                        p_cod_poliza.Size = 8;
                        p_cod_poliza.Direction = ParameterDirection.Input;

                        DbParameter p_numero_radicacion = cmdTransaccionFactory.CreateParameter();
                        p_numero_radicacion.ParameterName = "p_numero_radicacion";
                        p_numero_radicacion.Value = pEntidad.numero_radicacion;
                        p_numero_radicacion.DbType = DbType.Int64;
                        p_numero_radicacion.Size = 50;
                        p_numero_radicacion.Direction = ParameterDirection.Input;

                        DbParameter p_num_poliza = cmdTransaccionFactory.CreateParameter();
                        p_num_poliza.ParameterName = "p_num_poliza";
                        p_num_poliza.Value = pEntidad.num_poliza;
                        p_num_poliza.DbType = DbType.Int64;
                        //p_num_poliza.Size = 8;
                        p_num_poliza.Direction = ParameterDirection.Input;

                        DbParameter p_cod_asegurado = cmdTransaccionFactory.CreateParameter();
                        p_cod_asegurado.ParameterName = "p_cod_asegurado";
                        p_cod_asegurado.Value = pEntidad.cod_asegurado;
                        p_cod_asegurado.DbType = DbType.Int64;
                        p_cod_asegurado.Size = 8;
                        p_cod_asegurado.Direction = ParameterDirection.Input;

                        DbParameter p_cod_asesor = cmdTransaccionFactory.CreateParameter();
                        p_cod_asesor.ParameterName = "p_cod_asesor";
                        p_cod_asesor.Value = pEntidad.icodigo;
                        p_cod_asesor.DbType = DbType.Int64;
                        p_cod_asesor.Size = 8;
                        p_cod_asesor.Direction = ParameterDirection.Input;

                        DbParameter p_fec_ini_vig = cmdTransaccionFactory.CreateParameter();
                        p_fec_ini_vig.ParameterName = "p_fec_ini_vig";
                        p_fec_ini_vig.Value = pEntidad.fec_ini_vig;
                        p_fec_ini_vig.DbType = DbType.Date;
                        p_fec_ini_vig.Direction = ParameterDirection.Input;

                        DbParameter p_fec_fin_vig = cmdTransaccionFactory.CreateParameter();
                        p_fec_fin_vig.ParameterName = "p_fec_fin_vig";
                        p_fec_fin_vig.DbType = DbType.DateTime;
                        p_fec_fin_vig.Direction = ParameterDirection.Input;
                        p_fec_fin_vig.Value = pEntidad.fec_fin_vig;
                     
                        DbParameter p_tipo_plan = cmdTransaccionFactory.CreateParameter();
                        p_tipo_plan.ParameterName = "p_tipo_plan";
                        p_tipo_plan.Value = pEntidad.tipo_plan;
                        p_tipo_plan.DbType = DbType.Int64;
                        p_tipo_plan.Size = 8;
                        p_tipo_plan.Direction = ParameterDirection.Input;

                        DbParameter p_vida_individual = cmdTransaccionFactory.CreateParameter();
                        p_vida_individual.ParameterName = "p_vida_individual";
                        p_vida_individual.Value = pEntidad.individual;
                        p_vida_individual.DbType = DbType.Int64;
                        p_vida_individual.Size = 8;
                        p_vida_individual.Direction = ParameterDirection.Input;

                        DbParameter p_accidentes_individual = cmdTransaccionFactory.CreateParameter();
                        p_accidentes_individual.ParameterName = "p_accidentes_individual";
                        p_accidentes_individual.Value = pEntidad.accidentes;
                        p_accidentes_individual.DbType = DbType.Int64;
                        p_accidentes_individual.Size = 8;
                        p_accidentes_individual.Direction = ParameterDirection.Input;

                        DbParameter p_valor_prima_mensual = cmdTransaccionFactory.CreateParameter();
                        p_valor_prima_mensual.ParameterName = "p_valor_prima_mensual";
                        p_valor_prima_mensual.Value = pEntidad.valor_prima_mensual;
                        p_valor_prima_mensual.DbType = DbType.Int64;
                        p_valor_prima_mensual.Size = 8;
                        p_valor_prima_mensual.Direction = ParameterDirection.Input;

                        DbParameter p_valor_prima_total = cmdTransaccionFactory.CreateParameter();
                        p_valor_prima_total.ParameterName = "p_valor_prima_total";
                        p_valor_prima_total.Value = pEntidad.valor_prima_total;
                        p_valor_prima_total.DbType = DbType.Int64;
                        p_valor_prima_total.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(p_cod_poliza);
                        cmdTransaccionFactory.Parameters.Add(p_numero_radicacion);
                        cmdTransaccionFactory.Parameters.Add(p_num_poliza);
                        cmdTransaccionFactory.Parameters.Add(p_cod_asegurado);
                        cmdTransaccionFactory.Parameters.Add(p_cod_asesor);
                        cmdTransaccionFactory.Parameters.Add(p_fec_ini_vig);
                        cmdTransaccionFactory.Parameters.Add(p_fec_fin_vig);
                        cmdTransaccionFactory.Parameters.Add(p_tipo_plan);
                        cmdTransaccionFactory.Parameters.Add(p_vida_individual);
                        cmdTransaccionFactory.Parameters.Add(p_accidentes_individual);
                        cmdTransaccionFactory.Parameters.Add(p_valor_prima_mensual);
                        cmdTransaccionFactory.Parameters.Add(p_valor_prima_total);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_POLIZASSEG_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        // if (pUsuario.programaGeneraLog)
                        // DAauditoria.InsertarLog(pEntidad, pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA
                        //pEntidad.cod_poliza = Convert.ToString(p_cod_poliza.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PolizasSegurosData", "ModificarPolizasSeguros", ex);
                        return null;
                    }
                }
            }
        }

    
        /// <summary>
        /// Elimina una PolizasSeguros en la base de datos
        /// </summary>
        /// <param name="pId">identificador de la PolizasSeguros</param>
        public void EliminarPolizasSeguros(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        PolizasSeguros pEntidad = new PolizasSeguros();

                        // if (pUsuario.programaGeneraLog)
                          // pEntidad = ConsultarPolizasSeguros(pId, pUsuario); //REGISTRO DE AUDITORIA
                                                
                        DbParameter p_cod_poliza = cmdTransaccionFactory.CreateParameter();
                        p_cod_poliza.ParameterName = "p_cod_poliza";
                        p_cod_poliza.Value = pId;
                        p_cod_poliza.DbType = DbType.Int64;
                        p_cod_poliza.Size = 8;
                        p_cod_poliza.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(p_cod_poliza);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_POLIZASSEG_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                 //     if (pUsuario.programaGeneraLog)
                          //  DAauditoria.InsertarLog(pEntidad, pUsuario, Convert.ToInt64(pEntidad.cod_poliza), "POLIZA",Accion.Eliminar.ToString(),connection,cmdTransaccionFactory); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    
                    {
                        BOExcepcion.Throw("PolizasSegurosData", "EliminarPolizasSeguros", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro de la tabla PolizasSeguros de la base de datos
        /// </summary>
        /// <param name="pId">identificador del registro</param>
        /// <returns>PolizasSeguros consultada</returns>
        public PolizasSeguros ConsultarPolizasSeguros(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            PolizasSeguros entidad = new PolizasSeguros();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT A.cod_poliza,A.numero_radicacion,a.tipo_plan as tipo_plan_s,a.vida_individual,a.valor_prima_mensual,a.valor_prima_total,B.identificacion,k.descripcion as tipo_iden,b.cod_persona, B.primer_nombre || CHR(160) || B.segundo_nombre || CHR(160) || B.primer_apellido || CHR(160) || B.segundo_apellido as nombre_deudor,A.num_poliza,e.sidentificacion,e.icodigo,e.snombre1 || CHR(160) || e.snombre2 || CHR(160) || e.sapellido1 || CHR(160) || e.sapellido2  as nombre_asesor,a.fec_ini_vig,a.fec_fin_vig,j.nombre as oficina,c.cod_oficina,B.fechanacimiento,h.descripcion as estado_civil,b.sexo,G.descripcion as actividad,b.direccion,b.telefono,i.nomciudad as ciudad_residencia,b.email,b.celular " + 
                                     "FROM TIPOIDENTIFICACION K, POLIZASSEGUROS A LEFT JOIN ASEJECUTIVOS E ON e.icodigo = a.cod_asesor, PERSONA B LEFT JOIN CIUDADES I ON i.codciudad = b.codciudadresidencia LEFT JOIN ACTIVIDAD G ON to_char(b.codactividad) = G.codactividad LEFT JOIN ESTADOCIVIL H ON h. codestadocivil=b.codestadocivil, CREDITO C, OFICINA J " + 
                                     "WHERE A.cod_asegurado = B.cod_persona AND C.numero_radicacion = A.numero_radicacion AND c.cod_oficina = j.cod_oficina AND b.tipo_identificacion = b.tipo_identificacion AND k.codtipoidentificacion = 1  " +
                                     "AND a.cod_poliza =" + pId.ToString();
                       
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["cod_poliza"] != DBNull.Value) entidad.cod_poliza = Convert.ToInt64(resultado["cod_poliza"]);
                            if (resultado["cod_persona"] != DBNull.Value) entidad.cod_asegurado = Convert.ToInt64(resultado["cod_persona"]);
                            if (resultado["numero_radicacion"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["numero_radicacion"]);
                            if (resultado["nombre_deudor"] != DBNull.Value) entidad.nombre_deudor = Convert.ToString(resultado["nombre_deudor"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToInt64(resultado["identificacion"]);
                            if (resultado["tipo_iden"] != DBNull.Value) entidad.tipo_iden = Convert.ToString(resultado["tipo_iden"]);
                            if (resultado["num_poliza"] != DBNull.Value) entidad.num_poliza = Convert.ToInt64(resultado["num_poliza"]);
                            if (resultado["oficina"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["oficina"]);
                            if (resultado["cod_oficina"] != DBNull.Value) entidad.codoficina = Convert.ToString(resultado["cod_oficina"]);                          
                            if (resultado["sidentificacion"] != DBNull.Value) entidad.ident_asesor = Convert.ToInt64(resultado["sidentificacion"]);
                            if (resultado["nombre_asesor"] != DBNull.Value) entidad.nombre_asesor = Convert.ToString(resultado["nombre_asesor"]);
                            if (resultado["icodigo"] != DBNull.Value) entidad.icodigo = Convert.ToInt64(resultado["icodigo"]);
                            if (resultado["fec_ini_vig"] != DBNull.Value) entidad.fec_ini_vig = Convert.ToDateTime(resultado["fec_ini_vig"].ToString());
                            if (resultado["fec_fin_vig"] != DBNull.Value) entidad.fec_fin_vig = Convert.ToDateTime(resultado["fec_fin_vig"]);
                            if (resultado["fechanacimiento"] != DBNull.Value) entidad.fechanacimiento = Convert.ToDateTime(resultado["fechanacimiento"]);
                            if (resultado["estado_civil"] != DBNull.Value) entidad.estado_civil = Convert.ToString(resultado["estado_civil"]);
                            if (resultado["actividad"] != DBNull.Value) entidad.actividad = Convert.ToString(resultado["actividad"]);
                            if (resultado["sexo"] != DBNull.Value) entidad.sexo = Convert.ToString(resultado["sexo"]);
                            if (resultado["direccion"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["direccion"]);
                            if (resultado["email"] != DBNull.Value) entidad.email = Convert.ToString(resultado["email"]);
                            if (resultado["ciudad_residencia"] != DBNull.Value) entidad.ciudad_residencia = Convert.ToString(resultado["ciudad_residencia"]);
                            if (resultado["celular"] != DBNull.Value) entidad.celular = Convert.ToString(resultado["celular"]);
                            if (resultado["telefono"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["telefono"]);
                            if (resultado["tipo_plan_s"] != DBNull.Value) entidad.tipo_plan_s = Convert.ToInt64(resultado["tipo_plan_s"]);
                            if (resultado["vida_individual"] != DBNull.Value) entidad.individual = Convert.ToInt64(resultado["vida_individual"]);
                            if (resultado["valor_prima_mensual"] != DBNull.Value) entidad.valor_prima_mensual = Convert.ToInt64(resultado["valor_prima_mensual"]);
                            if (resultado["valor_prima_total"] != DBNull.Value) entidad.valor_prima_total = Convert.ToInt64(resultado["valor_prima_total"]);                                                      
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
                        BOExcepcion.Throw("PolizasSegurosData", "ConsultarPolizasSeguros", ex);
                        return null;
                    }

                }       
            }
        }
        /// <summary>
        /// Obtiene un registro de la tabla PolizasSeguros de la base de datos
        /// </summary>
        /// <param name="pId">identificador del registro</param>
        /// <returns>PolizasSeguros consultada</returns>
        public PolizasSeguros ConsultarPolizasSegurosValidacion(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            PolizasSeguros entidad = new PolizasSeguros();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT cod_poliza,numero_radicacion FROM POLIZASSEGUROS where  numero_radicacion= " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["cod_poliza"] != DBNull.Value) entidad.cod_poliza = Convert.ToInt64(resultado["cod_poliza"]);
                            if (resultado["numero_radicacion"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["numero_radicacion"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PolizasSegurosData", "ConsultarPolizasSegurosValidacion", ex);
                        return null;
                    }

                }
            }
        }
        // <summary>
        /// Obtiene la lista de PolizasSeguros dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de PolizasSeguros obtenidos</returns>
        public PolizasSegurosVida ConsultarPolizasSegurosVida(Int64 pId, String tipo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
           
            PolizasSegurosVida polizasegurosvida = null;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT COD_POLIZA, TIPO_PLAN,TIPO,INDIVIDUAL,VALOR_PRIMA from polizasegurosvida where cod_poliza  =  " + pId.ToString() + " and tipo = '" + tipo + "'";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            polizasegurosvida = new PolizasSegurosVida();
                            //Asociar todos los valores a la entidad
                            if (resultado["cod_poliza"] != DBNull.Value) polizasegurosvida.cod_poliza = Convert.ToInt64(resultado["cod_poliza"]);
                            if (resultado["tipo_plan"] != DBNull.Value) polizasegurosvida.tipo_plan = Convert.ToInt64(resultado["tipo_plan"]);
                            if (resultado["tipo"] != DBNull.Value) polizasegurosvida.tipo = Convert.ToString(resultado["tipo"]);
                            if (resultado["individual"] != DBNull.Value) polizasegurosvida.individual = Convert.ToString(resultado["individual"]);
                            if (resultado["valor_prima"] != DBNull.Value) polizasegurosvida.valor_prima = Convert.ToInt64(resultado["valor_prima"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return polizasegurosvida;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PolizasSegurosData", "ConsultarPolizasSegurosVida", ex);
                        return null;
                    }

                }
            }
        }

        /// <summary>
        /// Obtiene un registro de la tabla desembolso de la base de datos
        /// </summary>
        /// <param name="pId">identificador del registro</param>
        /// <returns>Credito consultado</returns>
        public PolizasSeguros ConsultarCredito(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            PolizasSeguros entidad = new PolizasSeguros();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT a.numero_radicacion,B.identificacion,b.cod_persona,k.descripcion as tipo_iden,B.primer_nombre || CHR(160) || B.segundo_nombre || B.primer_apellido || B.segundo_apellido as nombre_deudor,e.icodigo,e.sidentificacion,e.snombre1 || CHR(160) || e.snombre2 || e.sapellido1 || e.sapellido2  as nombre_asesor,a.fecha_aprobacion,a.fecha_vencimiento,j.nombre as oficina,B.fechanacimiento,h.descripcion as estado_civil,b.sexo,G.descripcion as actividad,b.direccion,b.telefono,i.nomciudad as ciudad_residencia,b.email,b.celular " +
                                     "FROM CREDITO A LEFT JOIN ASEJECUTIVOS E ON e.icodigo=a.cod_asesor_com, PERSONA B Left Join ACTIVIDAD G ON to_char(b.codactividad) = G.codactividad LEFT JOIN CIUDADES I ON i.codciudad = b.codciudadresidencia, ESTADOCIVIL H, OFICINA J, TIPOIDENTIFICACION K " +
                                     "WHERE a.cod_deudor = b.cod_persona and h.codestadocivil = b.codestadocivil and a.cod_oficina=j.cod_oficina and b.tipo_identificacion=b.tipo_identificacion and k.codtipoidentificacion = 1  and a.estado in ('S','V','A','G') and a.numero_radicacion =" + pId.ToString();  
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {                          
                            if (resultado["numero_radicacion"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["numero_radicacion"]);
                            if (resultado["cod_persona"] != DBNull.Value) entidad.cod_asegurado = Convert.ToInt64(resultado["cod_persona"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToInt64(resultado["identificacion"]);
                            if (resultado["tipo_iden"] != DBNull.Value) entidad.tipo_iden = Convert.ToString(resultado["tipo_iden"]);
                            if (resultado["nombre_deudor"] != DBNull.Value) entidad.nombre_deudor = Convert.ToString(resultado["nombre_deudor"]);
                            if (resultado["icodigo"] != DBNull.Value) entidad.icodigo = Convert.ToInt64(resultado["icodigo"]);
                            if (resultado["sidentificacion"] != DBNull.Value) entidad.ident_asesor = Convert.ToInt64(resultado["sidentificacion"]);
                            if (resultado["nombre_asesor"] != DBNull.Value) entidad.nombre_asesor = Convert.ToString(resultado["nombre_asesor"]);
                            if (resultado["fecha_aprobacion"] != DBNull.Value) entidad.fec_ini_vig = Convert.ToDateTime(resultado["fecha_aprobacion"]);
                            if (resultado["fecha_vencimiento"] != DBNull.Value) entidad.fec_fin_vig = Convert.ToDateTime(resultado["fecha_vencimiento"]);
                            if (resultado["oficina"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["oficina"]);
                            if (resultado["fechanacimiento"] != DBNull.Value) entidad.fechanacimiento = Convert.ToDateTime(resultado["fechanacimiento"]);
                            if (resultado["estado_civil"] != DBNull.Value) entidad.estado_civil = Convert.ToString(resultado["estado_civil"]);
                            if (resultado["actividad"] != DBNull.Value) entidad.actividad = Convert.ToString(resultado["actividad"]);
                            if (resultado["sexo"] != DBNull.Value) entidad.sexo = Convert.ToString(resultado["sexo"]);
                            if (resultado["direccion"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["direccion"]);
                            if (resultado["email"] != DBNull.Value) entidad.email = Convert.ToString(resultado["email"]);
                            if (resultado["ciudad_residencia"] != DBNull.Value) entidad.ciudad_residencia = Convert.ToString(resultado["ciudad_residencia"]);
                            if (resultado["celular"] != DBNull.Value) entidad.celular = Convert.ToString(resultado["celular"]);
                            if (resultado["telefono"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["telefono"]);                          
                        }
                        else
                        {
                            throw new ExceptionBusiness("El  credito no se encuentra en estado para generar póliza. Verifique por favor.");
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PolizasSegurosData", "ConsultarCredito", ex);
                        return null;
                    }

                }
            }
        }
        /// <summary>
        /// Obtiene un registro de la tabla General de la base de datos
        /// </summary>
        /// <param name="pId">identificador del registro</param>
        /// <returns>Parametro consultado</returns>
        public PolizasSeguros ConsultarParametroEdad(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            PolizasSeguros entidad = new PolizasSeguros();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT  valor FROM GENERAL WHERE CODIGO=653";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["valor"] != DBNull.Value) entidad.edad_maxima = Convert.ToInt64(resultado["valor"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PolizasSegurosData", "ConsultarParametroEdad", ex);
                        return null;
                    }

                }
            }
        }
        /// <summary>
        /// Obtiene un registro de la tabla General de la base de datos
        /// </summary>
        /// <param name="pId">identificador del registro</param>
        /// <returns>Parametro consultado</returns>
        public PolizasSeguros ConsultarParametroEdadBeneficiarios(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            PolizasSeguros entidad = new PolizasSeguros();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT  valor FROM GENERAL WHERE CODIGO=654";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["valor"] != DBNull.Value) entidad.edad_maxima = Convert.ToInt64(resultado["valor"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PolizasSegurosData", "ConsultarParametroEdad", ex);
                        return null;
                    }

                }
            }
        }


        /// <summary>
        /// Obtiene un registro de la tabla desembolso de la base de datos
        /// </summary>
        /// <param name="pId">identificador del registro</param>
        /// <returns>Credito consultado</returns>
        public List<PolizasSeguros> FiltrarCredito(PolizasSeguros pEntidad, Usuario pUsuario, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            PolizasSeguros entidad = null;
            List<PolizasSeguros> polizasseguros = new List<PolizasSeguros>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {                        
                        string sql = "SELECT * from V_CONSULTACREDITOPOLIZA  " + filtro;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            entidad = new PolizasSeguros();
                            if (resultado["numero_radicacion"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["numero_radicacion"]);
                            if (resultado["cod_persona"] != DBNull.Value) entidad.cod_asegurado = Convert.ToInt64(resultado["cod_persona"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToInt64(resultado["identificacion"]);                           
                            if (resultado["nombre_deudor"] != DBNull.Value) entidad.nombre_deudor = Convert.ToString(resultado["nombre_deudor"]);                            
                            if (resultado["nombre_asesor"] != DBNull.Value) entidad.nombre_asesor = Convert.ToString(resultado["nombre_asesor"]);                           
                            if (resultado["oficina"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["oficina"]);                           
                            polizasseguros.Add(entidad);
                        }
                        if (polizasseguros.Count ==0)
                        {
                            throw new ExceptionBusiness("No existe información con dichos criterios de búsqueda.");
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return polizasseguros;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PolizasSegurosData", "FiltrarCredito", ex);
                        return null;
                    }
                }
            }
        }

        private String QueryData(PolizasSeguros pEntidad)
        {
            String result = "";
            String sql = "";

            sql = " and ";
            result = "";
            if (pEntidad.numero_radicacion > 0)
            {
                result = " numero_radicacion = " + pEntidad.numero_radicacion.ToString().Trim();
            }
            if (pEntidad.identificacion > 0)
            {
                if (result.Trim().Length > 0)
                {
                    result = result + " and ";
                }
                result = " identificacion = " + pEntidad.identificacion.ToString().Trim();
            }

            if (pEntidad.primer_nombre.Trim().Length>0)
            {
                if (result.Trim().Length > 0)
                {
                    result = result + " and ";
                }
                result = " RTRIM(LTRIM(primer_nombre)) = '" + pEntidad.primer_nombre.ToString().Trim() + "'";
            }

            if (pEntidad.segundo_nombre.Trim().Length>0)
            {
                if (result.Trim().Length > 0)
                {
                    result = result + " and ";
                }
                result = " RTRIM(LTRIM(segundo_nombre))  = '" + pEntidad.segundo_nombre.ToString().Trim() + "'";
            }

            if (pEntidad.primer_apellido.Trim().Length>0)
            {
                if (result.Trim().Length > 0)
                {
                    result = result + " and ";
                }
                result = " RTRIM(LTRIM(primer_apellido)) = '" + pEntidad.primer_apellido.ToString().Trim() + "'";
            }

            if (pEntidad.segundo_apellido.Trim().Length>0)
            {
                if (result.Trim().Length > 0)
                {
                    result = result + " and ";
                }
                result = " RTRIM(LTRIM(segundo_apellido))  = '" + pEntidad.segundo_apellido.ToString().Trim() + "'";
            }

            if (result.Trim().Length > 0)
            {
                result = sql + result;
            }
            
            return result;
            }


        private String QueryData2(PolizasSeguros pEntidad)
        {
            String result = "";
            String sql = "";

            sql = " and ";
            result = "";
            if (pEntidad.numero_radicacion > 0)
            {
                result = " a.numero_radicacion = " + pEntidad.numero_radicacion.ToString().Trim();
            }
            if (pEntidad.cod_asegurado > 0)
            {
                if (result.Trim().Length > 0)
                {
                    result = result + " and ";
                }
                result = " a.cod_persona = " + pEntidad.cod_asegurado.ToString().Trim();
            }
            if (pEntidad.cod_poliza> 0)
            {
                if (result.Trim().Length > 0)
                {
                    result = result + " and ";
                }
                result = " a.cod_poliza = " + pEntidad.cod_poliza.ToString().Trim();
            }


            if (pEntidad.primer_nombre.Trim().Length > 0)
            {
                if (result.Trim().Length > 0)
                {
                    result = result + " and ";
                }
                result = "nom_peronsa= '" + pEntidad.primer_nombre.ToString().Trim() + "'";
            }

            if (result.Trim().Length > 0)
            {
                result = sql + result;
            }

            return result;
        }


        /// <summary>
        /// Obtiene una lista de Entidades de la tabla parentescos dados unos filtros
        /// </summary>
        /// <param name="pparentescos">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Parentesco obtenidos</returns>
        public List<ParentescoPolizas> ListarParentesco(ParentescoPolizas pParentesco, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ParentescoPolizas> lstParentesco = new List<ParentescoPolizas>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  PARENTESCOS" + ObtenerFiltro(pParentesco);

                        connection.Open();
                        
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ParentescoPolizas entidad = new ParentescoPolizas();

                            if (resultado["CODPARENTESCO"] != DBNull.Value) entidad.codparentesco = Convert.ToInt64(resultado["CODPARENTESCO"]);                           
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.parentesco = Convert.ToString(resultado["DESCRIPCION"]);

                            lstParentesco.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstParentesco;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ParentescoData", "ListarParentesco", ex);
                        return null;
                    }
                }
            }
        }
        /// <summary>
        /// Obtiene una lista de Entidades de la tabla parentescos dados unos filtros
        /// </summary>
        /// <param name="pparentescos">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Parentesco obtenidos</returns>
        public List<ParentescoPolizas> ListarParentescofamiliares(ParentescoPolizas pParentesco, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ParentescoPolizas> lstParentesco = new List<ParentescoPolizas>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  PARENTESCOS where descripcion in('Conyuge','Hijo(a)')";

                        connection.Open();

                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ParentescoPolizas entidad = new ParentescoPolizas();

                            if (resultado["CODPARENTESCO"] != DBNull.Value) entidad.codparentesco = Convert.ToInt64(resultado["CODPARENTESCO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.parentesco = Convert.ToString(resultado["DESCRIPCION"]);

                            lstParentesco.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstParentesco;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ParentescoData", "ListarParentesco", ex);
                        return null;
                    }
                }
            }
        }
        
    }
}
