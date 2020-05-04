using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Reporteador.Entities;

namespace Xpinn.Reporteador.Data
{
    
    public class UIAFData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public UIAFData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public UIAF CrearUiafREporte(UIAF pUiaf, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidreporte = cmdTransaccionFactory.CreateParameter();
                        pidreporte.ParameterName = "p_idreporte";
                        pidreporte.Value = pUiaf.idreporte;                        
                        pidreporte.Direction = ParameterDirection.Output;
                        pidreporte.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidreporte);

                        DbParameter pidformato = cmdTransaccionFactory.CreateParameter();
                        pidformato.ParameterName = "p_idformato";
                        if (pUiaf.idformato != null) pidformato.Value = pUiaf.idformato; else pidformato.Value = DBNull.Value;
                        pidformato.Direction = ParameterDirection.Input;
                        pidformato.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidformato);

                        DbParameter pfecha_generacion = cmdTransaccionFactory.CreateParameter();
                        pfecha_generacion.ParameterName = "p_fecha_generacion";
                        pfecha_generacion.Value = pUiaf.fecha_generacion;
                        pfecha_generacion.Direction = ParameterDirection.Input;
                        pfecha_generacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_generacion);

                        DbParameter pnumero_registros = cmdTransaccionFactory.CreateParameter();
                        pnumero_registros.ParameterName = "p_numero_registros";
                        pnumero_registros.Value = pUiaf.numero_registros;
                        pnumero_registros.Direction = ParameterDirection.Input;
                        pnumero_registros.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_registros);

                        DbParameter pcodusuario = cmdTransaccionFactory.CreateParameter();
                        pcodusuario.ParameterName = "p_codusuario";
                        pcodusuario.Value = vUsuario.codusuario;
                        pcodusuario.Direction = ParameterDirection.Input;
                        pcodusuario.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodusuario);

                        DbParameter pfecha_inicial = cmdTransaccionFactory.CreateParameter();
                        pfecha_inicial.ParameterName = "p_fecha_inicial";
                        pfecha_inicial.Value = pUiaf.fecha_inicial;
                        pfecha_inicial.Direction = ParameterDirection.Input;
                        pfecha_inicial.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_inicial);

                        DbParameter pfecha_final = cmdTransaccionFactory.CreateParameter();
                        pfecha_final.ParameterName = "p_fecha_final";
                        pfecha_final.Value = pUiaf.fecha_final;
                        pfecha_final.Direction = ParameterDirection.Input;
                        pfecha_final.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_final);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REP_UIAFREPORT_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pUiaf.idreporte = Convert.ToInt32(pidreporte.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pUiaf;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UIAFData", "CrearUiafREporte", ex);
                        return null;
                    }
                }
            }
        }


        public UIAF ModificarUiafREporte(UIAF pUiaf, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidreporte = cmdTransaccionFactory.CreateParameter();
                        pidreporte.ParameterName = "p_idreporte";
                        pidreporte.Value = pUiaf.idreporte;
                        pidreporte.Direction = ParameterDirection.Input;
                        pidreporte.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidreporte);

                        DbParameter pidformato = cmdTransaccionFactory.CreateParameter();
                        pidformato.ParameterName = "p_idformato";
                        if (pUiaf.idformato != null) pidformato.Value = pUiaf.idformato; else pidformato.Value = DBNull.Value;
                        pidformato.Direction = ParameterDirection.Input;
                        pidformato.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidformato);

                        DbParameter pfecha_generacion = cmdTransaccionFactory.CreateParameter();
                        pfecha_generacion.ParameterName = "p_fecha_generacion";
                        pfecha_generacion.Value = pUiaf.fecha_generacion;
                        pfecha_generacion.Direction = ParameterDirection.Input;
                        pfecha_generacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_generacion);

                        DbParameter pnumero_registros = cmdTransaccionFactory.CreateParameter();
                        pnumero_registros.ParameterName = "p_numero_registros";
                        pnumero_registros.Value = pUiaf.numero_registros;
                        pnumero_registros.Direction = ParameterDirection.Input;
                        pnumero_registros.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_registros);

                        DbParameter pcodusuario = cmdTransaccionFactory.CreateParameter();
                        pcodusuario.ParameterName = "p_codusuario";
                        pcodusuario.Value = vUsuario.codusuario;
                        pcodusuario.Direction = ParameterDirection.Input;
                        pcodusuario.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodusuario);

                        DbParameter pfecha_inicial = cmdTransaccionFactory.CreateParameter();
                        pfecha_inicial.ParameterName = "p_fecha_inicial";
                        pfecha_inicial.Value = pUiaf.fecha_inicial;
                        pfecha_inicial.Direction = ParameterDirection.Input;
                        pfecha_inicial.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_inicial);

                        DbParameter pfecha_final = cmdTransaccionFactory.CreateParameter();
                        pfecha_final.ParameterName = "p_fecha_final";
                        pfecha_final.Value = pUiaf.fecha_final;
                        pfecha_final.Direction = ParameterDirection.Input;
                        pfecha_final.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_final);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REP_UIAFREPORT_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                        return pUiaf;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UIAFData", "ModificarUiafREporte", ex);
                        return null;
                    }
                }
            }
        }


        public List<UIAF> ListarReporteUIAF(String filtro, DateTime pFechaIni, DateTime pFechaFin, Usuario vUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<UIAF> lstReporte = new List<UIAF>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();

                        string sql = @"Select Uiaf_Reporte.* , Usuarios.Nombre as NomUsuario From Uiaf_Reporte Inner Join Usuarios "
                                       +"on Usuarios.Codusuario = Uiaf_Reporte.Codusuario where 1=1 " + filtro ;

                        if (pFechaIni != null && pFechaIni != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " And ";
                            else
                                sql += " Where ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " FECHA_GENERACION >= To_Date('" + Convert.ToDateTime(pFechaIni).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " FECHA_GENERACION >= '" + Convert.ToDateTime(pFechaIni).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }

                        if (pFechaFin != null && pFechaFin != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " And ";
                            else
                                sql += " Where ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " FECHA_GENERACION <= To_Date('" + Convert.ToDateTime(pFechaFin).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " FECHA_GENERACION <= '" + Convert.ToDateTime(pFechaFin).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }

                        sql +=  " ORDER BY IDREPORTE";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        int cont = 0;
                        while (resultado.Read())
                        {
                            cont++;
                            UIAF entidad = new UIAF();
                            entidad.consecutivo = cont;
                            if (resultado["IDREPORTE"] != DBNull.Value) entidad.idreporte = Convert.ToInt32(resultado["IDREPORTE"]);
                            if (resultado["IDFORMATO"] != DBNull.Value) entidad.idformato = Convert.ToString(resultado["IDFORMATO"]);
                            if (resultado["FECHA_GENERACION"] != DBNull.Value) entidad.fecha_generacion = Convert.ToDateTime(resultado["FECHA_GENERACION"]);
                            if (resultado["NUMERO_REGISTROS"] != DBNull.Value) entidad.numero_registros = Convert.ToInt32(resultado["NUMERO_REGISTROS"]);
                            if (resultado["CODUSUARIO"] != DBNull.Value) entidad.codusuario = Convert.ToInt32(resultado["CODUSUARIO"]);
                            if (resultado["NOMUSUARIO"] != DBNull.Value) entidad.NomUsuario = Convert.ToString(resultado["NOMUSUARIO"]);
                            if (resultado["FECHA_INICIAL"] != DBNull.Value) entidad.fecha_inicial = Convert.ToDateTime(resultado["FECHA_INICIAL"]);
                            if (resultado["FECHA_FINAL"] != DBNull.Value) entidad.fecha_final = Convert.ToDateTime(resultado["FECHA_FINAL"]);
                            lstReporte.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstReporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UIAFData", "ListarReporteUIAF", ex);
                        return null;
                    }
                }
            }
        }



        public UIAFDetalle Crear_Mod_UIAFProductos(UIAFDetalle pUIAF, Usuario vUsuario, int opcion)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidreporteproductos = cmdTransaccionFactory.CreateParameter();
                        pidreporteproductos.ParameterName = "p_idreporteproductos";
                        pidreporteproductos.Value = pUIAF.idreporteproductos;
                        if (opcion == 1)//Crear
                            pidreporteproductos.Direction = ParameterDirection.Output;
                        else if (opcion == 2)//Modificar
                            pidreporteproductos.Direction = ParameterDirection.Input;
                        pidreporteproductos.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidreporteproductos);

                        DbParameter pidreporte = cmdTransaccionFactory.CreateParameter();
                        pidreporte.ParameterName = "p_idreporte";
                        pidreporte.Value = pUIAF.idreporte;
                        pidreporte.Direction = ParameterDirection.Input;
                        pidreporte.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidreporte);

                        DbParameter pnumero_producto = cmdTransaccionFactory.CreateParameter();
                        pnumero_producto.ParameterName = "p_numero_producto";
                        pnumero_producto.Value = pUIAF.numero_producto;
                        pnumero_producto.Direction = ParameterDirection.Input;
                        pnumero_producto.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumero_producto);

                        DbParameter pfecha_apertura = cmdTransaccionFactory.CreateParameter();
                        pfecha_apertura.ParameterName = "p_fecha_apertura";
                        pfecha_apertura.Value = pUIAF.fecha_apertura;
                        pfecha_apertura.Direction = ParameterDirection.Input;
                        pfecha_apertura.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_apertura);

                        DbParameter ptipo_producto = cmdTransaccionFactory.CreateParameter();
                        ptipo_producto.ParameterName = "p_tipo_producto";
                        ptipo_producto.Value = pUIAF.tipo_producto;
                        ptipo_producto.Direction = ParameterDirection.Input;
                        ptipo_producto.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipo_producto);

                        DbParameter pdepartamento = cmdTransaccionFactory.CreateParameter();
                        pdepartamento.ParameterName = "p_departamento";
                        if (pUIAF.departamento != null && pUIAF.departamento != "&nbsp;") pdepartamento.Value = pUIAF.departamento; else pdepartamento.Value = DBNull.Value;
                        pdepartamento.Direction = ParameterDirection.Input;
                        pdepartamento.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdepartamento);

                        DbParameter ptipo_identificacion1 = cmdTransaccionFactory.CreateParameter();
                        ptipo_identificacion1.ParameterName = "p_tipo_identificacion1";
                        if (pUIAF.tipo_identificacion1 != null) ptipo_identificacion1.Value = pUIAF.tipo_identificacion1; else ptipo_identificacion1.Value = DBNull.Value;
                        ptipo_identificacion1.Direction = ParameterDirection.Input;
                        ptipo_identificacion1.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipo_identificacion1);

                        DbParameter pidentificacion1 = cmdTransaccionFactory.CreateParameter();
                        pidentificacion1.ParameterName = "p_identificacion1";
                        if (pUIAF.identificacion1 != null && pUIAF.identificacion1 != "&nbsp;") pidentificacion1.Value = pUIAF.identificacion1;
                        pidentificacion1.Direction = ParameterDirection.Input;
                        pidentificacion1.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidentificacion1);

                        DbParameter pprimer_apellido1 = cmdTransaccionFactory.CreateParameter();
                        pprimer_apellido1.ParameterName = "p_primer_apellido1";
                        pprimer_apellido1.Value = pUIAF.primer_apellido1;
                        pprimer_apellido1.Direction = ParameterDirection.Input;
                        pprimer_apellido1.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pprimer_apellido1);

                        DbParameter psegundo_apellido1 = cmdTransaccionFactory.CreateParameter();
                        psegundo_apellido1.ParameterName = "p_segundo_apellido1";
                        psegundo_apellido1.Value = pUIAF.segundo_apellido1;
                        psegundo_apellido1.Direction = ParameterDirection.Input;
                        psegundo_apellido1.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(psegundo_apellido1);

                        DbParameter pprimer_nombre1 = cmdTransaccionFactory.CreateParameter();
                        pprimer_nombre1.ParameterName = "p_primer_nombre1";
                        pprimer_nombre1.Value = pUIAF.primer_nombre1;
                        pprimer_nombre1.Direction = ParameterDirection.Input;
                        pprimer_nombre1.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pprimer_nombre1);

                        DbParameter psegundo_nombre1 = cmdTransaccionFactory.CreateParameter();
                        psegundo_nombre1.ParameterName = "p_segundo_nombre1";
                        if (pUIAF.segundo_nombre1 != null) psegundo_nombre1.Value = pUIAF.segundo_nombre1; else psegundo_nombre1.Value = DBNull.Value;
                        psegundo_nombre1.Direction = ParameterDirection.Input;
                        psegundo_nombre1.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(psegundo_nombre1);

                        DbParameter prazon_social1 = cmdTransaccionFactory.CreateParameter();
                        prazon_social1.ParameterName = "p_razon_social1";
                        if (pUIAF.razon_social1 != null) prazon_social1.Value = pUIAF.razon_social1; else prazon_social1.Value = DBNull.Value;
                        prazon_social1.Direction = ParameterDirection.Input;
                        prazon_social1.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(prazon_social1);

                        DbParameter ptipo_identificacion2 = cmdTransaccionFactory.CreateParameter();
                        ptipo_identificacion2.ParameterName = "p_tipo_identificacion2";
                        if (pUIAF.tipo_identificacion2 != null && pUIAF.tipo_identificacion2 != "0") ptipo_identificacion2.Value = pUIAF.tipo_identificacion2; else ptipo_identificacion2.Value = DBNull.Value; 
                        ptipo_identificacion2.Direction = ParameterDirection.Input;
                        ptipo_identificacion2.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipo_identificacion2);

                        DbParameter pidentificacion2 = cmdTransaccionFactory.CreateParameter();
                        pidentificacion2.ParameterName = "p_identificacion2";
                        if (pUIAF.identificacion2 != null && pUIAF.identificacion2 != "&nbsp;") pidentificacion2.Value = pUIAF.identificacion2; else pidentificacion2.Value = DBNull.Value;
                        pidentificacion2.Direction = ParameterDirection.Input;
                        pidentificacion2.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidentificacion2);

                        DbParameter pprimer_apellido2 = cmdTransaccionFactory.CreateParameter();
                        pprimer_apellido2.ParameterName = "p_primer_apellido2";
                        if (pUIAF.primer_apellido2 != null && pUIAF.primer_apellido2 != "&nbsp;") pprimer_apellido2.Value = pUIAF.primer_apellido2; else pprimer_apellido2.Value = DBNull.Value;
                        pprimer_apellido2.Direction = ParameterDirection.Input;
                        pprimer_apellido2.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pprimer_apellido2);

                        DbParameter psegundo_apellido2 = cmdTransaccionFactory.CreateParameter();
                        psegundo_apellido2.ParameterName = "p_segundo_apellido2";
                        if (pUIAF.segundo_apellido2 != null && pUIAF.segundo_apellido2 != "&nbsp;") psegundo_apellido2.Value = pUIAF.segundo_apellido2; else psegundo_apellido2.Value = DBNull.Value;
                        psegundo_apellido2.Direction = ParameterDirection.Input;
                        psegundo_apellido2.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(psegundo_apellido2);

                        DbParameter pprimer_nombre2 = cmdTransaccionFactory.CreateParameter();
                        pprimer_nombre2.ParameterName = "p_primer_nombre2";
                        if (pUIAF.primer_nombre2 != null && pUIAF.primer_nombre2 != "&nbsp;") pprimer_nombre2.Value = pUIAF.primer_nombre2; else pprimer_nombre2.Value = DBNull.Value;
                        pprimer_nombre2.Direction = ParameterDirection.Input;
                        pprimer_nombre2.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pprimer_nombre2);

                        DbParameter psegundo_nombre2 = cmdTransaccionFactory.CreateParameter();
                        psegundo_nombre2.ParameterName = "p_segundo_nombre2";
                        if (pUIAF.segundo_nombre2 != null && pUIAF.segundo_nombre2 != "&nbsp;") psegundo_nombre2.Value = pUIAF.segundo_nombre2; else psegundo_nombre2.Value = DBNull.Value;
                        psegundo_nombre2.Direction = ParameterDirection.Input;
                        psegundo_nombre2.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(psegundo_nombre2);

                        DbParameter prazon_social2 = cmdTransaccionFactory.CreateParameter();
                        prazon_social2.ParameterName = "p_razon_social2";
                        if (pUIAF.razon_social2 != null && pUIAF.razon_social2 != "&nbsp;") prazon_social2.Value = pUIAF.razon_social2; else prazon_social2.Value = DBNull.Value;
                        prazon_social2.Direction = ParameterDirection.Input;
                        prazon_social2.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(prazon_social2);

                        DbParameter plinea = cmdTransaccionFactory.CreateParameter();
                        plinea.ParameterName = "p_linea";
                        if (pUIAF.linea != null) plinea.Value = pUIAF.linea; else plinea.Value = DBNull.Value;
                        plinea.Direction = ParameterDirection.Input;
                        plinea.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(plinea);

                        DbParameter p_tipo_tran = cmdTransaccionFactory.CreateParameter();
                        p_tipo_tran.ParameterName = "p_tipo_tran";
                        p_tipo_tran.Value = "2";
                        p_tipo_tran.Direction = ParameterDirection.Input;
                        p_tipo_tran.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_tipo_tran);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        if (opcion == 1)//crear
                            cmdTransaccionFactory.CommandText = "USP_XPINN_REP_UIAFPRODUC_CREAR";
                        else
                            cmdTransaccionFactory.CommandText = "USP_XPINN_REP_UIAFPRODUC_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();


                        if(opcion == 1)//crear
                            pUIAF.idreporteproductos = Convert.ToInt32(pidreporteproductos.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pUIAF;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UIAFData", "Crear_Mod_UIAFProductos", ex);
                        return null;
                    }
                }
            }
        }



        public void EliminarReporteUIAF(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_idreporte = cmdTransaccionFactory.CreateParameter();
                        p_idreporte.ParameterName = "p_idreporte";
                        p_idreporte.Value = pId;
                        p_idreporte.Direction = ParameterDirection.Input;
                        p_idreporte.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_idreporte);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REP_UIAFREPORT_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UIAFData", "EliminarReporteUIAF", ex);
                    }
                }
            }
        }


        public void EliminarUIAFProducto(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidreporteproductos = cmdTransaccionFactory.CreateParameter();
                        pidreporteproductos.ParameterName = "p_idreporteproductos";
                        pidreporteproductos.Value = pId;
                        pidreporteproductos.Direction = ParameterDirection.Input;
                        pidreporteproductos.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidreporteproductos);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REP_UIAFPRODUC_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UIAFData", "EliminarUIAFProducto", ex);
                    }
                }
            }
        }

        


        public UIAF ConsultarReporteUIAF(Int32 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            UIAF entidad = new UIAF();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * From Uiaf_Reporte where  IDREPORTE = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["IDREPORTE"] != DBNull.Value) entidad.idreporte = Convert.ToInt32(resultado["IDREPORTE"]);
                            if (resultado["IDFORMATO"] != DBNull.Value) entidad.idformato = Convert.ToString(resultado["IDFORMATO"]);
                            if (resultado["FECHA_GENERACION"] != DBNull.Value) entidad.fecha_generacion = Convert.ToDateTime(resultado["FECHA_GENERACION"]);
                            if (resultado["NUMERO_REGISTROS"] != DBNull.Value) entidad.numero_registros = Convert.ToInt32(resultado["NUMERO_REGISTROS"]);
                            if (resultado["CODUSUARIO"] != DBNull.Value) entidad.codusuario = Convert.ToInt32(resultado["CODUSUARIO"]);
                            if (resultado["FECHA_INICIAL"] != DBNull.Value) entidad.fecha_inicial = Convert.ToDateTime(resultado["FECHA_INICIAL"]);
                            if (resultado["FECHA_FINAL"] != DBNull.Value) entidad.fecha_final = Convert.ToDateTime(resultado["FECHA_FINAL"]);
                        }                        
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UIAFData", "ConsultarReporteUIAF", ex);
                        return null;
                    }
                }
            }
        }


        public List<UIAFDetalle> ListarVistaProductos(String filtro, DateTime pFechaIni, DateTime pFechaFin, Usuario vUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<UIAFDetalle> lstReporte = new List<UIAFDetalle>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();

                        string sql = "select * from V_Reporte_Productos where 1=1 " + filtro;

                        if (pFechaIni != null && pFechaIni != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " And ";
                            else
                                sql += " Where ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " FECHA >= To_Date('" + Convert.ToDateTime(pFechaIni).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " FECHA >= '" + Convert.ToDateTime(pFechaIni).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }

                        if (pFechaFin != null && pFechaFin != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " And ";
                            else
                                sql += " Where ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " FECHA <= To_Date('" + Convert.ToDateTime(pFechaFin).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " FECHA <= '" + Convert.ToDateTime(pFechaFin).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }

                        sql += " ORDER BY NUM_PRODUCTO";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        int cont = 0;

                        while (resultado.Read())
                        {
                            cont++;
                            UIAFDetalle entidad = new UIAFDetalle();
                            entidad.consecutivo = cont;                           
                            if (resultado["NUM_PRODUCTO"] != DBNull.Value) entidad.numero_producto = Convert.ToString(resultado["NUM_PRODUCTO"]);
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                            if (resultado["TIPO_PRODUCTO"] != DBNull.Value) entidad.tipo_producto_vista = Convert.ToInt32(resultado["TIPO_PRODUCTO"]);
                            if (resultado["DEPARTAMENTO"] != DBNull.Value) entidad.departamento = Convert.ToString(resultado["DEPARTAMENTO"]);
                            if (resultado["DEPARTAMENTO2"] != DBNull.Value) entidad.departamento2 = Convert.ToString(resultado["DEPARTAMENTO2"]);
                            if (resultado["TIPO_IDENTIFICACION1"] != DBNull.Value) entidad.tipo_identificacion1_vista = Convert.ToInt32(resultado["TIPO_IDENTIFICACION1"]);
                            if (resultado["IDENTIFICACION1"] != DBNull.Value) entidad.identificacion1 = Convert.ToString(resultado["IDENTIFICACION1"]);
                            if (resultado["PRIMER_APELLIDO1"] != DBNull.Value) entidad.primer_apellido1 = Convert.ToString(resultado["PRIMER_APELLIDO1"]);
                            if (resultado["SEGUN_APELLIDO1"] != DBNull.Value) entidad.segundo_apellido1 = Convert.ToString(resultado["SEGUN_APELLIDO1"]);
                            if (resultado["PRIMER_NOMBRE1"] != DBNull.Value) entidad.primer_nombre1 = Convert.ToString(resultado["PRIMER_NOMBRE1"]);
                            if (resultado["SEGUN_NOMBRE1"] != DBNull.Value) entidad.segundo_nombre1 = Convert.ToString(resultado["SEGUN_NOMBRE1"]);
                            if (resultado["RAZON_SOCIAL1"] != DBNull.Value) entidad.razon_social1 = Convert.ToString(resultado["RAZON_SOCIAL1"]);
                            if (resultado["TIPO_IDENTIFICACION2"] != DBNull.Value) entidad.tipo_identificacion2_vista = Convert.ToInt32(resultado["TIPO_IDENTIFICACION2"]);
                            if (resultado["IDENTIFICACION2"] != DBNull.Value) entidad.identificacion2 = Convert.ToString(resultado["IDENTIFICACION2"]);
                            if (resultado["PRIMER_APELLIDO2"] != DBNull.Value) entidad.primer_apellido2 = Convert.ToString(resultado["PRIMER_APELLIDO2"]);
                            if (resultado["SEGUN_APELLIDO2"] != DBNull.Value) entidad.segundo_apellido2 = Convert.ToString(resultado["SEGUN_APELLIDO2"]);
                            if (resultado["PRIMER_NOMBRE2"] != DBNull.Value) entidad.primer_nombre2 = Convert.ToString(resultado["PRIMER_NOMBRE2"]);
                            if (resultado["SEGUN_NOMBRE2"] != DBNull.Value) entidad.segundo_nombre2 = Convert.ToString(resultado["SEGUN_NOMBRE2"]);
                            if (resultado["RAZON_SOCIAL2"] != DBNull.Value) entidad.razon_social2 = Convert.ToString(resultado["RAZON_SOCIAL2"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea_vista = Convert.ToInt32(resultado["LINEA"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha_vista = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["TIPO_TRAN"] != DBNull.Value) entidad.tipo_tran = Convert.ToString(resultado["TIPO_TRAN"]);

                            lstReporte.Add(entidad);    
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstReporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UIAFData", "ListarVistaProductos", ex);
                        return null;
                    }
                }
            }
        }


        public List<UIAFDetalle> ListarVistaProductosAll(String filtro, Usuario vUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<UIAFDetalle> lstReporte = new List<UIAFDetalle>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        int mes = DateTime.Now.Month;
                        int x = 3;
                        string sql = "select * from V_Reporte_Productos where 1=1 ";
                        while (x <= 12)
                        {
                            if (mes <= x)
                            {
                                if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                    sql = sql + " And  FECHA < To_date('01/" + (x - 2 ).ToString("00") + "/" + DateTime.Now.Year + "', 'DD/MM/YYYY')";
                                else
                                    sql = sql + " And  FECHA < '01/" + (x - 2).ToString("00") + "/" + DateTime.Now.Year + "', 'DD/MM/YYYY'";
                                break;
                            }
                            x += 3;
                        }
                        sql += " ORDER BY NUM_PRODUCTO";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        int cont = 0;

                        while (resultado.Read())
                        {
                            cont++;
                            UIAFDetalle entidad = new UIAFDetalle();
                            entidad.consecutivo = cont;
                            if (resultado["NUM_PRODUCTO"] != DBNull.Value) entidad.numero_producto = Convert.ToString(resultado["NUM_PRODUCTO"]);
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                            if (resultado["TIPO_PRODUCTO"] != DBNull.Value) entidad.tipo_producto_vista = Convert.ToInt32(resultado["TIPO_PRODUCTO"]);
                            if (resultado["DEPARTAMENTO"] != DBNull.Value) entidad.departamento = Convert.ToString(resultado["DEPARTAMENTO"]);
                            if (resultado["DEPARTAMENTO2"] != DBNull.Value) entidad.departamento2 = Convert.ToString(resultado["DEPARTAMENTO"]);
                            if (resultado["TIPO_IDENTIFICACION1"] != DBNull.Value) entidad.tipo_identificacion1_vista = Convert.ToInt32(resultado["TIPO_IDENTIFICACION1"]);
                            if (resultado["IDENTIFICACION1"] != DBNull.Value) entidad.identificacion1 = Convert.ToString(resultado["IDENTIFICACION1"]);
                            if (resultado["PRIMER_APELLIDO1"] != DBNull.Value) entidad.primer_apellido1 = Convert.ToString(resultado["PRIMER_APELLIDO1"]);
                            if (resultado["SEGUN_APELLIDO1"] != DBNull.Value) entidad.segundo_apellido1 = Convert.ToString(resultado["SEGUN_APELLIDO1"]);
                            if (resultado["PRIMER_NOMBRE1"] != DBNull.Value) entidad.primer_nombre1 = Convert.ToString(resultado["PRIMER_NOMBRE1"]);
                            if (resultado["SEGUN_NOMBRE1"] != DBNull.Value) entidad.segundo_nombre1 = Convert.ToString(resultado["SEGUN_NOMBRE1"]);
                            if (resultado["RAZON_SOCIAL1"] != DBNull.Value) entidad.razon_social1 = Convert.ToString(resultado["RAZON_SOCIAL1"]);
                            if (resultado["TIPO_IDENTIFICACION2"] != DBNull.Value) entidad.tipo_identificacion2_vista = Convert.ToInt32(resultado["TIPO_IDENTIFICACION2"]);
                            if (resultado["IDENTIFICACION2"] != DBNull.Value) entidad.identificacion2 = Convert.ToString(resultado["IDENTIFICACION2"]);
                            if (resultado["PRIMER_APELLIDO2"] != DBNull.Value) entidad.primer_apellido2 = Convert.ToString(resultado["PRIMER_APELLIDO2"]);
                            if (resultado["SEGUN_APELLIDO2"] != DBNull.Value) entidad.segundo_apellido2 = Convert.ToString(resultado["SEGUN_APELLIDO2"]);
                            if (resultado["PRIMER_NOMBRE2"] != DBNull.Value) entidad.primer_nombre2 = Convert.ToString(resultado["PRIMER_NOMBRE2"]);
                            if (resultado["SEGUN_NOMBRE2"] != DBNull.Value) entidad.segundo_nombre2 = Convert.ToString(resultado["SEGUN_NOMBRE2"]);
                            if (resultado["RAZON_SOCIAL2"] != DBNull.Value) entidad.razon_social2 = Convert.ToString(resultado["RAZON_SOCIAL2"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea_vista = Convert.ToInt32(resultado["LINEA"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha_vista = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["TIPO_TRAN"] != DBNull.Value) entidad.tipo_tran = Convert.ToString(resultado["TIPO_TRAN"]);

                            lstReporte.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstReporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UIAFData", "ListarVistaProductos", ex);
                        return null;
                    }
                }
            }
        }



        public List<UIAFDetalle> ListarUIAFProductos(String filtro, Usuario vUsuario) // DateTime pFechaIni, DateTime pFechaFin,
        {
            DbDataReader resultado = default(DbDataReader);
            List<UIAFDetalle> lstReporte = new List<UIAFDetalle>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();

                        string sql = "SELECT * FROM uiaf_Reporte_Producto where 1=1 " + filtro;

                        //if (pFechaIni != null && pFechaIni != DateTime.MinValue)
                        //{
                        //    if (sql.ToUpper().Contains("WHERE"))
                        //        sql += " And ";
                        //    else
                        //        sql += " Where ";
                        //    if (dbConnectionFactory.TipoConexion() == "ORACLE")
                        //        sql += " FECHA >= To_Date('" + Convert.ToDateTime(pFechaIni).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                        //    else
                        //        sql += " FECHA >= '" + Convert.ToDateTime(pFechaIni).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        //}

                        //if (pFechaFin != null && pFechaFin != DateTime.MinValue)
                        //{
                        //    if (sql.ToUpper().Contains("WHERE"))
                        //        sql += " And ";
                        //    else
                        //        sql += " Where ";
                        //    if (dbConnectionFactory.TipoConexion() == "ORACLE")
                        //        sql += " FECHA <= To_Date('" + Convert.ToDateTime(pFechaFin).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                        //    else
                        //        sql += " FECHA <= '" + Convert.ToDateTime(pFechaFin).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        //}

                        sql += " ORDER BY IDREPORTEPRODUCTOS";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        int cont = 0;

                        while (resultado.Read())
                        {
                            cont++;
                            UIAFDetalle entidad = new UIAFDetalle();
                            entidad.consecutivo = cont;
                            if (resultado["IDREPORTEPRODUCTOS"] != DBNull.Value) entidad.idreporteproductos = Convert.ToInt32(resultado["IDREPORTEPRODUCTOS"]);
                            if (resultado["IDREPORTE"] != DBNull.Value) entidad.idreporte = Convert.ToInt32(resultado["IDREPORTE"]);
                            if (resultado["NUMERO_PRODUCTO"] != DBNull.Value) entidad.numero_producto = Convert.ToString(resultado["NUMERO_PRODUCTO"]);
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                            if (resultado["TIPO_PRODUCTO"] != DBNull.Value) entidad.tipo_producto = Convert.ToString(resultado["TIPO_PRODUCTO"]);
                            if (resultado["DEPARTAMENTO"] != DBNull.Value) entidad.departamento = Convert.ToString(resultado["DEPARTAMENTO"]);
                            if (resultado["TIPO_IDENTIFICACION1"] != DBNull.Value) entidad.tipo_identificacion1 = Convert.ToString(resultado["TIPO_IDENTIFICACION1"]);
                            if (resultado["IDENTIFICACION1"] != DBNull.Value) entidad.identificacion1 = Convert.ToString(resultado["IDENTIFICACION1"]);
                            if (resultado["PRIMER_APELLIDO1"] != DBNull.Value) entidad.primer_apellido1 = Convert.ToString(resultado["PRIMER_APELLIDO1"]);
                            if (resultado["SEGUNDO_APELLIDO1"] != DBNull.Value) entidad.segundo_apellido1 = Convert.ToString(resultado["SEGUNDO_APELLIDO1"]);
                            if (resultado["PRIMER_NOMBRE1"] != DBNull.Value) entidad.primer_nombre1 = Convert.ToString(resultado["PRIMER_NOMBRE1"]);
                            if (resultado["SEGUNDO_NOMBRE1"] != DBNull.Value) entidad.segundo_nombre1 = Convert.ToString(resultado["SEGUNDO_NOMBRE1"]);
                            if (resultado["RAZON_SOCIAL1"] != DBNull.Value) entidad.razon_social1 = Convert.ToString(resultado["RAZON_SOCIAL1"]);
                            if (resultado["TIPO_IDENTIFICACION2"] != DBNull.Value) entidad.tipo_identificacion2 = Convert.ToString(resultado["TIPO_IDENTIFICACION2"]);
                            if (resultado["IDENTIFICACION2"] != DBNull.Value) entidad.identificacion2 = Convert.ToString(resultado["IDENTIFICACION2"]);
                            if (resultado["PRIMER_APELLIDO2"] != DBNull.Value) entidad.primer_apellido2 = Convert.ToString(resultado["PRIMER_APELLIDO2"]);
                            if (resultado["SEGUNDO_APELLIDO2"] != DBNull.Value) entidad.segundo_apellido2 = Convert.ToString(resultado["SEGUNDO_APELLIDO2"]);
                            if (resultado["PRIMER_NOMBRE2"] != DBNull.Value) entidad.primer_nombre2 = Convert.ToString(resultado["PRIMER_NOMBRE2"]);
                            if (resultado["SEGUNDO_NOMBRE2"] != DBNull.Value) entidad.segundo_nombre2 = Convert.ToString(resultado["SEGUNDO_NOMBRE2"]);
                            if (resultado["RAZON_SOCIAL2"] != DBNull.Value) entidad.razon_social2 = Convert.ToString(resultado["RAZON_SOCIAL2"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["LINEA"]);
                            if (resultado["TIPO_TRAN"] != DBNull.Value) entidad.tipo_tran = Convert.ToString(resultado["TIPO_TRAN"]);

                            lstReporte.Add(entidad);    
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstReporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UIAFData", "ListarUIAFProductos", ex);
                        return null;
                    }
                }
            }
        }


        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            Int64 resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select MAX(ID) + 1 from TABLA";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = Convert.ToInt64(cmdTransaccionFactory.ExecuteScalar());
                        dbConnectionFactory.CerrarConexion(connection);
                        return resultado;
                    }
                    catch
                    {
                        return 1;
                    }
                }
            }
        }


        public List<UIAFTarjetas> ListarTransaccionesTarjeta(String filtro, DateTime pFechaIni, DateTime pFechaFin, Usuario vUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<UIAFTarjetas> lstReporte = new List<UIAFTarjetas>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "Select * from V_Reporte_Tarjeta Where tipotransaccion In ('2', '3', '5', '6', '7', '9') And ";
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            sql = sql + " fecha >= To_date('" + pFechaIni.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') And fecha <= To_date('" + pFechaFin.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') ";
                        else
                            sql = sql + " fecha >= '" + pFechaIni + "' And fecha <= '" + pFechaFin + "' ";
                        sql += " Order By fecha, cod_ope ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        int cont = 0;
                        while (resultado.Read())
                        {
                            cont++;
                            UIAFTarjetas entidad = new UIAFTarjetas();
                            entidad.consecutivo = cont;
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha_tran = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["MONTO"] != DBNull.Value) entidad.valor_tran = Convert.ToDecimal(resultado["MONTO"]);
                            if (resultado["RED"] != DBNull.Value) entidad.red = Convert.ToString(resultado["RED"]);
                            if (resultado["LUGAR"] != DBNull.Value) entidad.lugar = Convert.ToString(resultado["LUGAR"]);
                            if (resultado["TIPOTRANSACCION"] != DBNull.Value) entidad.tipo_tran = HomologaTipoTran(Convert.ToString(resultado["TIPOTRANSACCION"]), entidad.red, entidad.lugar);
                            if (resultado["PAIS"] != DBNull.Value) entidad.pais = Convert.ToString(resultado["PAIS"]);
                            if (resultado["MUNICIPIO"] != DBNull.Value) entidad.departamento = Convert.ToString(resultado["MUNICIPIO"]);
                            if (resultado["MUNICIPIO2"] != DBNull.Value) entidad.departamento2 = Convert.ToString(resultado["MUNICIPIO2"]);
                            if (resultado["TIPO_TARJETA"] != DBNull.Value) entidad.tipo_tarjeta = Convert.ToString(resultado["TIPO_TARJETA"]);
                            if (resultado["TARJETA"] != DBNull.Value) entidad.numero_tarjeta = Convert.ToString(resultado["TARJETA"]);  // Se colocá comilla para poder exportar datos a EXCEL.
                            if (resultado["NUMERO_CUENTA"] != DBNull.Value) entidad.numero_cuenta = Convert.ToString(resultado["NUMERO_CUENTA"]);
                            if (resultado["VALOR_CUPO"] != DBNull.Value) entidad.valor_cupo = Convert.ToDecimal(resultado["VALOR_CUPO"]);
                            if (resultado["FRANQUICIA"] != DBNull.Value) entidad.franquicia = Convert.ToString(resultado["FRANQUICIA"]);
                            //if (resultado["SALDO_TOTAL"] != DBNull.Value) entidad.saldo_tarjeta = Convert.ToDecimal(resultado["SALDO_TOTAL"]);
                            if (entidad.numero_cuenta != "")
                            {
                                if (entidad.fecha_tran != null)
                                { 
                                    DateTime? pFechaCierre = ProximoCierreAhorros(Convert.ToDateTime(entidad.fecha_tran), vUsuario);
                                    if (pFechaCierre != null)
                                        entidad.saldo_tarjeta = SaldoCuentaAhorros(entidad.numero_cuenta, Convert.ToDateTime(pFechaCierre), vUsuario);
                                }
                            }
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = HomologaIdentificacion(Convert.ToString(resultado["TIPO_IDENTIFICACION"]));
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["DIGITO_VERIFICACION"] != DBNull.Value) entidad.digito_verificacion = Convert.ToString(resultado["DIGITO_VERIFICACION"]);
                            if (resultado["PRIMER_APELLIDO"] != DBNull.Value) entidad.primer_apellido = Convert.ToString(resultado["PRIMER_APELLIDO"]);
                            if (resultado["SEGUNDO_APELLIDO"] != DBNull.Value) entidad.segundo_apellido = Convert.ToString(resultado["SEGUNDO_APELLIDO"]);
                            if (resultado["PRIMER_NOMBRE"] != DBNull.Value) entidad.primer_nombre = Convert.ToString(resultado["PRIMER_NOMBRE"]);
                            if (resultado["SEGUNDO_NOMBRE"] != DBNull.Value) entidad.segundo_nombre = Convert.ToString(resultado["SEGUNDO_NOMBRE"]);
                            if (resultado["RAZON_SOCIAL"] != DBNull.Value) entidad.razon_social = Convert.ToString(resultado["RAZON_SOCIAL"]);                            

                            lstReporte.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstReporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UIAFData", "ListarTransaccionesTarjeta", ex);
                        return null;
                    }
                }
            }
        }

        public decimal SaldoCuentaAhorros(string pNumeroCuenta, DateTime pfecha, Usuario pUsuario)
        {
            decimal resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "";
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            sql += "Select saldo_total From historico_ahorro Where fecha_historico = To_Date('" + pfecha.ToString(conf.ObtenerFormatoFecha()) + "', '" +  conf.ObtenerFormatoFecha() + "') And numero_cuenta = '" + pNumeroCuenta + "' ";
                        else
                            sql += "Select saldo_total From historico_ahorro Where fecha_historico = '" + pfecha.ToString() + "' And numero_cuenta = '" + pNumeroCuenta + "' ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = Convert.ToDecimal(cmdTransaccionFactory.ExecuteScalar());
                        dbConnectionFactory.CerrarConexion(connection);
                        return resultado;
                    }
                    catch
                    {
                        return 0;
                    }
                }
            }
        }

        public DateTime? ProximoCierreAhorros(DateTime pfecha, Usuario pUsuario)
        {
            DateTime resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "";
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            sql += "Select Min(fecha) From cierea Where tipo = 'H' And fecha >= To_Date('" + pfecha.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') ";
                        else
                            sql += "Select Min(fecha) From cierea Where tipo = 'H' And fecha >= '" + pfecha.ToString() + "' ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = Convert.ToDateTime(cmdTransaccionFactory.ExecuteScalar());
                        dbConnectionFactory.CerrarConexion(connection);
                        return resultado;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }

        public string HomologaTipoTran(string pTipoTransaccion, string pRed, string pLugar)
        {            
            // 01 Red de Cajeros
            if (pLugar.StartsWith("ATH")) return "01";
            // 02 Servicios Públicos y Datafonos
            if (pRed.Trim() == "5") return "02";
            if (pTipoTransaccion == "9")  return "02";
            // 06 Compras o Pagos            
            if (pTipoTransaccion == "3")  return "06";             

            return "01";
        }

        public string HomologaIdentificacion(string pTipoIdentificacion)
        {
            if (pTipoIdentificacion == "1") return "13";
            if (pTipoIdentificacion == "2") return "31";
            if (pTipoIdentificacion == "3") return "22";
            if (pTipoIdentificacion == "4") return "11";
            if (pTipoIdentificacion == "5") return "41";
            return "13";
        }

    }
}
