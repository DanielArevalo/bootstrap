using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Aportes.Entities;

namespace Xpinn.Aportes.Data
{
    public class PlanesTelefonicosData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public PlanesTelefonicosData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
        //Region planes Moviles
        #region planes
        // Crear Plan Telefonico
        public PlanTelefonico CrearPlanTelefonico(PlanTelefonico pPlanTel, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter p_nombre = cmdTransaccionFactory.CreateParameter();
                        p_nombre.ParameterName = "p_nombre";
                        if (pPlanTel.nombre == null)
                            p_nombre.Value = DBNull.Value;
                        else
                            p_nombre.Value = pPlanTel.nombre;
                        p_nombre.Direction = ParameterDirection.Input;
                        p_nombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_nombre);

                        DbParameter p_valor = cmdTransaccionFactory.CreateParameter();
                        p_valor.ParameterName = "p_valor";
                        p_valor.Value = pPlanTel.valor;
                        p_valor.Direction = ParameterDirection.Input;
                        p_valor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_valor);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_PLAN_TEL_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pPlanTel;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PlanesTelefonicosData", "CrearPlanTelefonico", ex);
                        return null;
                    }
                }
            }
        }

        //Modificar Plan Telefonico
        public PlanTelefonico ModificarPlanTelefonico(PlanTelefonico pPlanTel, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_plan = cmdTransaccionFactory.CreateParameter();
                        p_cod_plan.ParameterName = "p_cod_plan";
                        p_cod_plan.Value = pPlanTel.cod_plan;
                        p_cod_plan.Direction = ParameterDirection.Input;
                        p_cod_plan.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_cod_plan);

                        DbParameter p_nombre = cmdTransaccionFactory.CreateParameter();
                        p_nombre.ParameterName = "p_nombre";
                        if (pPlanTel.nombre == null)
                            p_nombre.Value = DBNull.Value;
                        else
                            p_nombre.Value = pPlanTel.nombre;
                        p_nombre.Direction = ParameterDirection.Input;
                        p_nombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_nombre);

                        DbParameter p_valor = cmdTransaccionFactory.CreateParameter();
                        p_valor.ParameterName = "p_valor";
                        p_valor.Value = pPlanTel.valor;
                        p_valor.Direction = ParameterDirection.Input;
                        p_valor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_valor);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_PLAN_TEL_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pPlanTel;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PlanesTelefonicosData", "ModificarPlanTelefonico", ex);
                        return null;
                    }
                }
            }
        }

        //Eliminar Plan Telefonico
        public void EliminarPlanTelefonico(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        PlanTelefonico pPlanTelefonico = new PlanTelefonico();
                        pPlanTelefonico = ConsultarPlanTelefonico(pId, vUsuario);

                        DbParameter p_cod_plan = cmdTransaccionFactory.CreateParameter();
                        p_cod_plan.ParameterName = "p_cod_plan";
                        p_cod_plan.Value = pPlanTelefonico.cod_plan;
                        p_cod_plan.Direction = ParameterDirection.Input;
                        p_cod_plan.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_cod_plan);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_PLAN_TEL_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PlanesTelefonicosData", "EliminarDestinacion", ex);
                    }
                }
            }
        }

        //Consultar Plan Telefonico
        public PlanTelefonico ConsultarPlanTelefonico(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            PlanTelefonico entidad = new PlanTelefonico();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM PLANES_TELEFONICOS WHERE COD_PLAN = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COD_PLAN"] != DBNull.Value) entidad.cod_plan = Convert.ToInt32(resultado["COD_PLAN"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
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
                        BOExcepcion.Throw("PlanesTelefonicosData", "ConsultarPlanTelefonico", ex);
                        return null;
                    }
                }
            }
        }

        //Consultar existencia de líneas teleonicas
        public bool ConsultarLineasExistentes(Usuario vUsuario)
        {
            DbDataReader resultado;
            bool existe = false;
            Int64 cont = 0;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT COUNT(*) FROM LINEAS_TELEFONICAS";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COUNT(*)"] != DBNull.Value) cont = Convert.ToInt32(resultado["COUNT(*)"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        if (cont > 0)
                            existe = true;
                          
                        return existe;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PlanesTelefonicosData", "ConsultarPlanTelefonico", ex);
                        return false;
                    }
                }
            }
        }

        //Consultar todos los planes Telefonicos
        public List<PlanTelefonico> ListarPlanTelefonico(PlanTelefonico pDestinacion, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<PlanTelefonico> lstPlanesTel = new List<PlanTelefonico>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT PL.* FROM PLANES_TELEFONICOS PL" + ObtenerFiltro(pDestinacion) + " ORDER BY PL.COD_PLAN";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            PlanTelefonico entidad = new PlanTelefonico();
                            if (resultado["COD_PLAN"] != DBNull.Value) entidad.cod_plan = Convert.ToInt32(resultado["COD_PLAN"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            lstPlanesTel.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPlanesTel;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PlanesTelefonicosData", "ListarPlanTelefonico", ex);
                        return null;
                    }
                }
            }
        }

        //Concultar todos los planes Telefonicos
        public List<PlanTelefonico> ListarProveedores(Usuario vUsuario)
        {
            DbDataReader resultado;
            List<PlanTelefonico> lstPlanesTel = new List<PlanTelefonico>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM PROVEEDORES ORDER BY COD_PROVEEDOR";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            PlanTelefonico entidad = new PlanTelefonico();
                            if (resultado["COD_PROVEEDOR"] != DBNull.Value) entidad.cod_proveedor = Convert.ToInt32(resultado["COD_PROVEEDOR"]);
                            if (resultado["NOMBRE_PROVEEDOR"] != DBNull.Value) entidad.nombre_proveedor = Convert.ToString(resultado["NOMBRE_PROVEEDOR"]);
                            lstPlanesTel.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPlanesTel;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PlanesTelefonicosData", "ListarProveedores", ex);
                        return null;
                    }
                }
            }
        }

        #endregion

        //Region lineas
        #region Proceso_Linea
        //Crear Linea Telefonica y sus repectivos Servicicos
        public PlanTelefonico CrearLineaTelefonica(Int64 vCod_Ope, PlanTelefonico pLinTel, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter p_Num_Linea_Telefonica = cmdTransaccionFactory.CreateParameter();
                        p_Num_Linea_Telefonica.ParameterName = "p_Num_Linea_Telefonica";
                        p_Num_Linea_Telefonica.Value = pLinTel.num_linea_telefonica;
                        p_Num_Linea_Telefonica.Direction = ParameterDirection.InputOutput;
                        p_Num_Linea_Telefonica.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_Num_Linea_Telefonica);

                        //DbParameter p_Cod_Titular = cmdTransaccionFactory.CreateParameter();
                        //p_Cod_Titular.ParameterName = "p_Cod_Titular";
                        //p_Cod_Titular.Value = pLinTel.cod_titular;
                        //p_Cod_Titular.Direction = ParameterDirection.Input;
                        //p_Cod_Titular.DbType = DbType.Int32;
                        //cmdTransaccionFactory.Parameters.Add(p_Cod_Titular);

                        DbParameter p_Identificacion_titular = cmdTransaccionFactory.CreateParameter();
                        p_Identificacion_titular.ParameterName = "p_Identificacion_titular";
                        p_Identificacion_titular.Value = pLinTel.identificacion_titular;
                        p_Identificacion_titular.Direction = ParameterDirection.InputOutput;
                        p_Identificacion_titular.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_Identificacion_titular);


                        DbParameter p_Fecha_Activacion = cmdTransaccionFactory.CreateParameter();
                        p_Fecha_Activacion.ParameterName = "p_Fecha_Activacion";
                        p_Fecha_Activacion.Value = pLinTel.fecha_activacion;
                        p_Fecha_Activacion.Direction = ParameterDirection.Input;
                        p_Fecha_Activacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(p_Fecha_Activacion);

                        DbParameter p_Fecha_vencimiento = cmdTransaccionFactory.CreateParameter();
                        p_Fecha_vencimiento.ParameterName = "p_Fecha_vencimiento";
                        if (pLinTel.fecha_vencimiento == null)
                            p_Fecha_vencimiento.Value = DBNull.Value;
                        else
                            p_Fecha_vencimiento.Value = pLinTel.fecha_vencimiento;
                        p_Fecha_vencimiento.Direction = ParameterDirection.Input;
                        p_Fecha_vencimiento.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(p_Fecha_vencimiento);

                        DbParameter p_Cod_plan = cmdTransaccionFactory.CreateParameter();
                        p_Cod_plan.ParameterName = "p_Cod_plan";
                        p_Cod_plan.Value = pLinTel.cod_plan;
                        p_Cod_plan.Direction = ParameterDirection.Input;
                        p_Cod_plan.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_Cod_plan);

                        DbParameter p_cod_linea_servicio = cmdTransaccionFactory.CreateParameter();
                        p_cod_linea_servicio.ParameterName = "p_cod_linea_servicio";
                        p_cod_linea_servicio.Value = pLinTel.cod_linea_servicio;
                        p_cod_linea_servicio.Direction = ParameterDirection.Input;
                        p_cod_linea_servicio.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_linea_servicio);

                        DbParameter p_fecha_primera_cuota = cmdTransaccionFactory.CreateParameter();
                        p_fecha_primera_cuota.ParameterName = "p_fecha_primera_cuota";
                        p_fecha_primera_cuota.Value = pLinTel.fecha_primera_cuota;
                        p_fecha_primera_cuota.Direction = ParameterDirection.Input;
                        p_fecha_primera_cuota.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(p_fecha_primera_cuota);

                        DbParameter P_VALOR_CUOTA = cmdTransaccionFactory.CreateParameter();
                        P_VALOR_CUOTA.ParameterName = "P_VALOR_CUOTA";
                        P_VALOR_CUOTA.Value = pLinTel.valor_cuota;
                        P_VALOR_CUOTA.Direction = ParameterDirection.Input;
                        P_VALOR_CUOTA.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(P_VALOR_CUOTA);

                        DbParameter P_COD_PERIODICIDAD = cmdTransaccionFactory.CreateParameter();
                        P_COD_PERIODICIDAD.ParameterName = "P_COD_PERIODICIDAD";
                        P_COD_PERIODICIDAD.Value = pLinTel.cod_periodicidad;
                        P_COD_PERIODICIDAD.Direction = ParameterDirection.Input;
                        P_COD_PERIODICIDAD.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(P_COD_PERIODICIDAD);

                        DbParameter P_FORMA_PAGO = cmdTransaccionFactory.CreateParameter();
                        P_FORMA_PAGO.ParameterName = "P_FORMA_PAGO";
                        P_FORMA_PAGO.Value = pLinTel.forma_pago;
                        P_FORMA_PAGO.Direction = ParameterDirection.Input;
                        P_FORMA_PAGO.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(P_FORMA_PAGO);

                        DbParameter p_cod_empresa = cmdTransaccionFactory.CreateParameter();
                        p_cod_empresa.ParameterName = "p_cod_empresa";
                        if (pLinTel.cod_empresa == null)
                            p_cod_empresa.Value = DBNull.Value;
                        else
                            p_cod_empresa.Value = pLinTel.cod_empresa;
                        p_cod_empresa.Direction = ParameterDirection.Input;
                        p_cod_empresa.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_empresa);

                        DbParameter p_Valor_Compra = cmdTransaccionFactory.CreateParameter();
                        p_Valor_Compra.ParameterName = "p_Valor_Compra";
                        if (pLinTel.valor_compra == null)
                            p_Valor_Compra.Value = 0;
                        else
                            p_Valor_Compra.Value = pLinTel.valor_compra;
                        p_Valor_Compra.Direction = ParameterDirection.Input;
                        p_Valor_Compra.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_Valor_Compra);

                        DbParameter p_Beneficio = cmdTransaccionFactory.CreateParameter();
                        p_Beneficio.ParameterName = "p_Beneficio";
                        if (pLinTel.beneficio == null)
                            p_Beneficio.Value = 0;
                        else
                            p_Beneficio.Value = pLinTel.beneficio;
                        p_Beneficio.Direction = ParameterDirection.Input;
                        p_Beneficio.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_Beneficio);

                        DbParameter p_Valor_Mercado = cmdTransaccionFactory.CreateParameter();
                        p_Valor_Mercado.ParameterName = "p_Valor_Mercado";
                        if (pLinTel.valor_mercado == null)
                            p_Valor_Mercado.Value = 0;
                        else
                            p_Valor_Mercado.Value = pLinTel.valor_mercado;
                        p_Valor_Mercado.Direction = ParameterDirection.Input;
                        p_Valor_Mercado.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_Valor_Mercado);

                        DbParameter p_COD_OPE = cmdTransaccionFactory.CreateParameter();
                        p_COD_OPE.ParameterName = "p_cod_ope";
                        p_COD_OPE.Value = vCod_Ope;
                        p_COD_OPE.Direction = ParameterDirection.Input;
                        p_COD_OPE.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_COD_OPE);

                        DbParameter p_COD_SERVICIO_FIJO = cmdTransaccionFactory.CreateParameter();
                        p_COD_SERVICIO_FIJO.ParameterName = "p_COD_SERVICIO_FIJO";
                        p_COD_SERVICIO_FIJO.Value = pLinTel.cod_serv_fijo;
                        p_COD_SERVICIO_FIJO.Direction = ParameterDirection.Output;
                        p_COD_SERVICIO_FIJO.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_COD_SERVICIO_FIJO);

                        DbParameter p_COD_SERVICIO_ADICIONALES = cmdTransaccionFactory.CreateParameter();
                        p_COD_SERVICIO_ADICIONALES.ParameterName = "p_COD_SERVICIO_ADICIONALES";
                        p_COD_SERVICIO_ADICIONALES.Value = pLinTel.cod_serv_adicional;
                        p_COD_SERVICIO_ADICIONALES.Direction = ParameterDirection.Output;
                        p_COD_SERVICIO_ADICIONALES.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_COD_SERVICIO_ADICIONALES);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SER_LIN_TELE_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pLinTel;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PlanesTelefonicosData", "CrearLineaTelefonica", ex);
                        return null;
                    }
                }
            }
        }

        //Modificar Linea Telefonica y sus repectivos Servicicos
        public PlanTelefonico ModificarLineaTelefonica(PlanTelefonico pLinTel, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter p_Num_Linea_Telefonica = cmdTransaccionFactory.CreateParameter();
                        p_Num_Linea_Telefonica.ParameterName = "p_Num_Linea_Telefonica";
                        p_Num_Linea_Telefonica.Value = pLinTel.num_linea_telefonica;
                        p_Num_Linea_Telefonica.Direction = ParameterDirection.InputOutput;
                        p_Num_Linea_Telefonica.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_Num_Linea_Telefonica);

                        DbParameter p_Cod_Titular = cmdTransaccionFactory.CreateParameter();
                        p_Cod_Titular.ParameterName = "p_Cod_Titular";
                        p_Cod_Titular.Value = pLinTel.cod_titular;
                        p_Cod_Titular.Direction = ParameterDirection.Input;
                        p_Cod_Titular.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_Cod_Titular);

                        DbParameter p_Fecha_Activacion = cmdTransaccionFactory.CreateParameter();
                        p_Fecha_Activacion.ParameterName = "p_Fecha_Activacion";
                        p_Fecha_Activacion.Value = pLinTel.fecha_activacion;
                        p_Fecha_Activacion.Direction = ParameterDirection.Input;
                        p_Fecha_Activacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(p_Fecha_Activacion);

                        DbParameter p_Fecha_vencimiento = cmdTransaccionFactory.CreateParameter();
                        p_Fecha_vencimiento.ParameterName = "p_Fecha_vencimiento";
                        if (pLinTel.fecha_vencimiento == null)
                            p_Fecha_vencimiento.Value = DBNull.Value;
                        else
                            p_Fecha_vencimiento.Value = pLinTel.fecha_vencimiento;
                        p_Fecha_vencimiento.Direction = ParameterDirection.Input;
                        p_Fecha_vencimiento.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(p_Fecha_vencimiento);

                        DbParameter p_Cod_plan = cmdTransaccionFactory.CreateParameter();
                        p_Cod_plan.ParameterName = "p_Cod_plan";
                        p_Cod_plan.Value = pLinTel.cod_plan;
                        p_Cod_plan.Direction = ParameterDirection.Input;
                        p_Cod_plan.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_Cod_plan);
                        
                        DbParameter p_cod_linea_servicio = cmdTransaccionFactory.CreateParameter();
                        p_cod_linea_servicio.ParameterName = "p_cod_linea_servicio";
                        p_cod_linea_servicio.Value = pLinTel.cod_linea_servicio;
                        p_cod_linea_servicio.Direction = ParameterDirection.Input;
                        p_cod_linea_servicio.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_linea_servicio);

                        DbParameter p_fecha_primera_cuota = cmdTransaccionFactory.CreateParameter();
                        p_fecha_primera_cuota.ParameterName = "p_fecha_primera_cuota";
                        p_fecha_primera_cuota.Value = pLinTel.fecha_primera_cuota;
                        p_fecha_primera_cuota.Direction = ParameterDirection.Input;
                        p_fecha_primera_cuota.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(p_fecha_primera_cuota);

                        DbParameter P_VALOR_CUOTA = cmdTransaccionFactory.CreateParameter();
                        P_VALOR_CUOTA.ParameterName = "P_VALOR_CUOTA";
                        P_VALOR_CUOTA.Value = pLinTel.valor_cuota;
                        P_VALOR_CUOTA.Direction = ParameterDirection.Input;
                        P_VALOR_CUOTA.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(P_VALOR_CUOTA);
                        
                        DbParameter P_COD_PERIODICIDAD = cmdTransaccionFactory.CreateParameter();
                        P_COD_PERIODICIDAD.ParameterName = "P_COD_PERIODICIDAD";
                        P_COD_PERIODICIDAD.Value = pLinTel.cod_periodicidad;
                        P_COD_PERIODICIDAD.Direction = ParameterDirection.Input;
                        P_COD_PERIODICIDAD.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(P_COD_PERIODICIDAD);

                        DbParameter P_FORMA_PAGO = cmdTransaccionFactory.CreateParameter();
                        P_FORMA_PAGO.ParameterName = "P_FORMA_PAGO";
                        P_FORMA_PAGO.Value = pLinTel.forma_pago;
                        P_FORMA_PAGO.Direction = ParameterDirection.Input;
                        P_FORMA_PAGO.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(P_FORMA_PAGO);

                        DbParameter p_cod_empresa = cmdTransaccionFactory.CreateParameter();
                        p_cod_empresa.ParameterName = "p_cod_empresa";
                        if (pLinTel.cod_empresa == null)
                            p_cod_empresa.Value = DBNull.Value;
                        else
                            p_cod_empresa.Value = pLinTel.cod_empresa;
                        p_cod_empresa.Direction = ParameterDirection.Input;
                        p_cod_empresa.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_empresa);

                        DbParameter p_Valor_Compra = cmdTransaccionFactory.CreateParameter();
                        p_Valor_Compra.ParameterName = "p_Valor_Compra";
                        if (pLinTel.valor_compra == null)
                            p_Valor_Compra.Value = 0;
                        else
                            p_Valor_Compra.Value = pLinTel.valor_compra;
                        p_Valor_Compra.Direction = ParameterDirection.Input;
                        p_Valor_Compra.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_Valor_Compra);

                        DbParameter p_Beneficio = cmdTransaccionFactory.CreateParameter();
                        p_Beneficio.ParameterName = "p_Beneficio";
                        if (pLinTel.beneficio == null)
                            p_Beneficio.Value = 0;
                        else
                            p_Beneficio.Value = pLinTel.beneficio;
                        p_Beneficio.Direction = ParameterDirection.Input;
                        p_Beneficio.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_Beneficio);

                        DbParameter p_Valor_Mercado = cmdTransaccionFactory.CreateParameter();
                        p_Valor_Mercado.ParameterName = "p_Valor_Mercado";
                        if (pLinTel.valor_mercado == null)
                            p_Valor_Mercado.Value = 0;
                        else
                            p_Valor_Mercado.Value = pLinTel.valor_mercado;
                        p_Valor_Mercado.Direction = ParameterDirection.Input;
                        p_Valor_Mercado.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_Valor_Mercado);

                        DbParameter p_COD_SERVICIO_FIJO = cmdTransaccionFactory.CreateParameter();
                        p_COD_SERVICIO_FIJO.ParameterName = "p_COD_SERVICIO_FIJO";
                        p_COD_SERVICIO_FIJO.Value = pLinTel.cod_serv_fijo;
                        p_COD_SERVICIO_FIJO.Direction = ParameterDirection.Output;
                        p_COD_SERVICIO_FIJO.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_COD_SERVICIO_FIJO);

                        DbParameter p_COD_SERVICIO_ADICIONALES = cmdTransaccionFactory.CreateParameter();
                        p_COD_SERVICIO_ADICIONALES.ParameterName = "p_COD_SERVICIO_ADICIONALES";
                        p_COD_SERVICIO_ADICIONALES.Value = pLinTel.cod_serv_adicional;
                        p_COD_SERVICIO_ADICIONALES.Direction = ParameterDirection.Output;
                        p_COD_SERVICIO_ADICIONALES.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_COD_SERVICIO_ADICIONALES);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SER_LIN_TELE_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pLinTel;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PlanesTelefonicosData", "ModificarLineaTelefonica", ex);
                        return null;
                    }
                }
            }
        }

        //Concultar todos los planes Telefonicos
        public List<PlanTelefonico> ListarLineasTelefonicas(PlanTelefonico plineafiltro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<PlanTelefonico> lstPlanesTel = new List<PlanTelefonico>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string compl = "";

                        string sql = @"SELECT LT.*, P.NOMBRE, P.IDENTIFICACION, S.VALOR_CUOTA, LS.NOMBRE AS NOM_LINEA, PT.NOMBRE AS NOM_PLAN
                                    FROM LINEAS_TELEFONICAS LT
                                    INNER JOIN V_PERSONA P ON LT.COD_TITULAR = P.COD_PERSONA
                                    INNER JOIN SERVICIOS S ON LT.COD_SERVICIO_FIJO = S.NUMERO_SERVICIO
                                    INNER JOIN LINEASSERVICIOS LS ON S.COD_LINEA_SERVICIO = LS.COD_LINEA_SERVICIO AND S.DESTINACION = 1
                                    INNER JOIN PLANES_TELEFONICOS PT ON LT.COD_PLAN = PT.COD_PLAN ";

                        if (!string.IsNullOrEmpty(plineafiltro.estado))
                            compl = compl + " AND LT.ESTADO = '" + plineafiltro.estado + "'";
                        else
                            compl = compl + " AND LT.ESTADO = 'A'";

                        if (plineafiltro.num_linea_telefonica != null)
                            compl = " AND LT.NUM_LINEA_TELEFONICA ='" + plineafiltro.num_linea_telefonica.ToString() + "'";

                        if (plineafiltro.identificacion_titular != null)
                            compl = compl + " AND P.IDENTIFICACION = '" + plineafiltro.identificacion_titular + "'";

                        if (plineafiltro.cod_plan != 0)
                            compl = compl + " AND LT.COD_PLAN =" + plineafiltro.cod_plan;

                        if (!string.IsNullOrEmpty(compl))
                        {
                            compl = compl.Substring(4);
                            compl = " WHERE " + compl;
                        }
                        sql = sql + compl;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            PlanTelefonico entidad = new PlanTelefonico();
                            if (resultado["NUM_LINEA_TELEFONICA"] != DBNull.Value) entidad.num_linea_telefonica = Convert.ToString(resultado["NUM_LINEA_TELEFONICA"]);
                            if (resultado["COD_SERVICIO_FIJO"] != DBNull.Value) entidad.cod_serv_fijo = Convert.ToInt64(resultado["COD_SERVICIO_FIJO"]);
                            if (resultado["COD_SERVICIO_ADICIONALES"] != DBNull.Value) entidad.cod_serv_adicional = Convert.ToInt64(resultado["COD_SERVICIO_ADICIONALES"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion_titular = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["COD_TITULAR"] != DBNull.Value) entidad.cod_titular = Convert.ToInt64(resultado["COD_TITULAR"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre_titular = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["FECHA_ACTIVACION"] != DBNull.Value) entidad.fecha_activacion = Convert.ToDateTime(resultado["FECHA_ACTIVACION"]);
                            if (resultado["FECHA_ULTIMA_REPOSICION"] != DBNull.Value) entidad.fecha_ult_reposicion = Convert.ToDateTime(resultado["FECHA_ULTIMA_REPOSICION"]);
                            if (resultado["FECHA_VENCIMIENTO"] != DBNull.Value) entidad.fecha_vencimiento = Convert.ToDateTime(resultado["FECHA_VENCIMIENTO"]);
                            if (resultado["COD_PLAN"] != DBNull.Value) entidad.cod_plan = Convert.ToInt32(resultado["COD_PLAN"]);
                            if (resultado["FECHA_INACTIVACION"] != DBNull.Value) entidad.fecha_incativacion = Convert.ToDateTime(resultado["FECHA_INACTIVACION"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToDecimal(resultado["VALOR_CUOTA"]);
                            if (resultado["NOM_PLAN"] != DBNull.Value) entidad.nom_plan = Convert.ToString(resultado["NOM_PLAN"]);
                            if (resultado["NOM_LINEA"] != DBNull.Value) entidad.nom_linea = Convert.ToString(resultado["NOM_LINEA"]);

                            lstPlanesTel.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPlanesTel;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PlanesTelefonicosData", "ListarProveedores", ex);
                        return null;
                    }
                }
            }
        }

        public PlanTelefonico ConsultarLineaTelefonica(string numlin, Usuario vUsuario)
        {
            DbDataReader resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT LT.*, P.NOMBRE, P.IDENTIFICACION, S.VALOR_CUOTA, LS.NOMBRE AS NOM_LINEA, PT.NOMBRE AS NOM_PLAN,
                                    S.FECHA_SOLICITUD, S.COD_LINEA_SERVICIO, S.FECHA_PRIMERA_CUOTA, S.COD_PERIODICIDAD, S.FORMA_PAGO , S.COD_EMPRESA,
                                    TS.VALOR_COMPRA, TS.VALOR_MERCADO, TS.BENEFICIO,
                                    (select SALDO from servicios where NUMERO_SERVICIO = LT.COD_SERVICIO_FIJO ) as saldo_ser_fij,
                                    (select SALDO from servicios where NUMERO_SERVICIO = LT.COD_SERVICIO_ADICIONALES) as saldo_ser_adi
                                    FROM LINEAS_TELEFONICAS LT
                                    INNER JOIN V_PERSONA P ON LT.COD_TITULAR = P.COD_PERSONA
                                    INNER JOIN SERVICIOS S ON LT.COD_SERVICIO_FIJO = S.NUMERO_SERVICIO
                                    INNER JOIN LINEASSERVICIOS LS ON S.COD_LINEA_SERVICIO = LS.COD_LINEA_SERVICIO AND S.DESTINACION = 1
                                    INNER JOIN PLANES_TELEFONICOS PT ON LT.COD_PLAN = PT.COD_PLAN 
                                    LEFT OUTER JOIN TRANSFERENCIA_SOLIDARIA TS ON LT.COD_SERVICIO_FIJO = TS.NUM_PRODUCTO
                                    WHERE LT.NUM_LINEA_TELEFONICA = '" + numlin + "'";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        PlanTelefonico entidad = new PlanTelefonico();

                        if (resultado.Read())
                        {

                            if (resultado["NUM_LINEA_TELEFONICA"] != DBNull.Value) entidad.num_linea_telefonica = Convert.ToString(resultado["NUM_LINEA_TELEFONICA"]);
                            if (resultado["COD_SERVICIO_FIJO"] != DBNull.Value) entidad.cod_serv_fijo = Convert.ToInt64(resultado["COD_SERVICIO_FIJO"]);
                            if (resultado["COD_SERVICIO_ADICIONALES"] != DBNull.Value) entidad.cod_serv_adicional = Convert.ToInt64(resultado["COD_SERVICIO_ADICIONALES"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion_titular = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["COD_TITULAR"] != DBNull.Value) entidad.cod_titular = Convert.ToInt64(resultado["COD_TITULAR"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre_titular = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["FECHA_ACTIVACION"] != DBNull.Value) entidad.fecha_activacion = Convert.ToDateTime(resultado["FECHA_ACTIVACION"]);
                            if (resultado["FECHA_ULTIMA_REPOSICION"] != DBNull.Value) entidad.fecha_ult_reposicion = Convert.ToDateTime(resultado["FECHA_ULTIMA_REPOSICION"]);
                            if (resultado["FECHA_VENCIMIENTO"] != DBNull.Value) entidad.fecha_vencimiento = Convert.ToDateTime(resultado["FECHA_VENCIMIENTO"]);
                            if (resultado["COD_PLAN"] != DBNull.Value) entidad.cod_plan = Convert.ToInt32(resultado["COD_PLAN"]);
                            if (resultado["FECHA_INACTIVACION"] != DBNull.Value) entidad.fecha_incativacion = Convert.ToDateTime(resultado["FECHA_INACTIVACION"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToDecimal(resultado["VALOR_CUOTA"]);
                            if (resultado["NOM_PLAN"] != DBNull.Value) entidad.nom_plan = Convert.ToString(resultado["NOM_PLAN"]);
                            if (resultado["NOM_LINEA"] != DBNull.Value) entidad.nom_linea = Convert.ToString(resultado["NOM_LINEA"]);
                            //AGREGADOS
                            if (resultado["FECHA_SOLICITUD"] != DBNull.Value) entidad.fecha_solicitud = Convert.ToDateTime(resultado["FECHA_SOLICITUD"]);
                            if (resultado["COD_LINEA_SERVICIO"] != DBNull.Value) entidad.cod_linea_servicio = Convert.ToInt64(resultado["COD_LINEA_SERVICIO"]);
                            if (resultado["FECHA_PRIMERA_CUOTA"] != DBNull.Value) entidad.fecha_primera_cuota = Convert.ToDateTime(resultado["FECHA_PRIMERA_CUOTA"]);
                            if (resultado["COD_PERIODICIDAD"] != DBNull.Value) entidad.cod_periodicidad = Convert.ToInt64(resultado["COD_PERIODICIDAD"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToString(resultado["FORMA_PAGO"]);
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt64(resultado["COD_EMPRESA"]);
                            if (resultado["VALOR_COMPRA"] != DBNull.Value) entidad.valor_compra = Convert.ToDecimal(resultado["VALOR_COMPRA"]);
                            if (resultado["VALOR_MERCADO"] != DBNull.Value) entidad.valor_mercado = Convert.ToDecimal(resultado["VALOR_MERCADO"]);
                            if (resultado["BENEFICIO"] != DBNull.Value) entidad.beneficio = Convert.ToDecimal(resultado["BENEFICIO"]);
                            //Saldos Servicios
                            if (resultado["saldo_ser_fij"] != DBNull.Value) entidad.Saldo_ser_fijo = Convert.ToDecimal(resultado["saldo_ser_fij"]);
                            if (resultado["saldo_ser_adi"] != DBNull.Value) entidad.saldo_ser_adicional = Convert.ToDecimal(resultado["saldo_ser_adi"]);
                        }
                        else
                        {
                            //throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PlanesTelefonicosData", "ConsultarLineaTelefonica", ex);
                        return null;
                    }
                }
            }
        }

        #endregion


        /// <summary>
        /// Modifica el valor de cuota a la tabla aportes y deja un registro en la tabla novedades_aporte
        /// </summary>
        /// <param name="pLineaTel">Entidad PLAN_TEL</param>S
        /// /// <param name="lst_Num_Apor">Entidad GAporte</param>S
        /// <returns>Entidad Aporte creada</returns>
        public PlanTelefonico CrearAdicional(PlanTelefonico pLineaTel, Int64 vCod_Ope, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {

                    try
                    {
                        DbParameter p_identificacion = cmdTransaccionFactory.CreateParameter();
                        p_identificacion.ParameterName = "p_identificacion";
                        p_identificacion.Value = pLineaTel.identificacion_titular;
                        p_identificacion.Direction = ParameterDirection.Input;
                        p_identificacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_identificacion);

                        DbParameter p_Num_Linea_Telefonica = cmdTransaccionFactory.CreateParameter();
                        p_Num_Linea_Telefonica.ParameterName = "p_Num_Linea_Telefonica";
                        p_Num_Linea_Telefonica.Value = pLineaTel.num_linea_telefonica;
                        p_Num_Linea_Telefonica.Direction = ParameterDirection.InputOutput;
                        p_Num_Linea_Telefonica.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_Num_Linea_Telefonica);

                        DbParameter p_valor_cuota = cmdTransaccionFactory.CreateParameter();
                        p_valor_cuota.ParameterName = "p_valor_cuota";
                        p_valor_cuota.Value = pLineaTel.valor_cuota;
                        p_valor_cuota.Direction = ParameterDirection.Input;
                        p_valor_cuota.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_valor_cuota);

                        DbParameter p_Num_Linea_TelefonicaR = cmdTransaccionFactory.CreateParameter();
                        p_Num_Linea_TelefonicaR.ParameterName = "p_Num_Linea_TelefonicaR";
                        p_Num_Linea_TelefonicaR.Value = pLineaTel.num_linea_telefonica;
                        p_Num_Linea_TelefonicaR.Direction = ParameterDirection.InputOutput;
                        p_Num_Linea_TelefonicaR.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_Num_Linea_TelefonicaR);

                        DbParameter p_cod_ope = cmdTransaccionFactory.CreateParameter();
                        p_cod_ope.ParameterName = "p_cod_ope";
                        if (vCod_Ope > 0)
                            p_cod_ope.Value = vCod_Ope;
                        else p_cod_ope.Value = DBNull.Value;
                        p_cod_ope.Direction = ParameterDirection.InputOutput;
                        p_cod_ope.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_ope);

                        DbParameter pfechaProxPago = cmdTransaccionFactory.CreateParameter();
                        pfechaProxPago.ParameterName = "P_FECHA_PROX_PAGO";
                        if (pLineaTel.fecha_activacion != DateTime.MinValue)
                            pfechaProxPago.Value = pLineaTel.fecha_activacion;
                        else
                            pfechaProxPago.Value = DBNull.Value;
                        pfechaProxPago.Direction = ParameterDirection.Input;
                        pfechaProxPago.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechaProxPago);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SER_ADICIONALES";
                        cmdTransaccionFactory.ExecuteNonQuery();


                        if (p_Num_Linea_TelefonicaR.Value != null)
                        {

                            if (p_Num_Linea_TelefonicaR.Value != DBNull.Value)
                            {
                                pLineaTel.num_linea_telefonicaR = Convert.ToString(p_Num_Linea_TelefonicaR.Value);
                            }

                            pLineaTel.num_linea_telefonicaR = Convert.ToString(p_Num_Linea_TelefonicaR.Value);

                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return pLineaTel;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PlanesTelefonicosData", "CrearAdicional", ex);
                        return null;
                    }
                }
            }
        }
        public bool CuotasActAdici(Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter p_usuario = cmdTransaccionFactory.CreateParameter();
                        p_usuario.ParameterName = "p_usuario";
                        p_usuario.Value = vUsuario.cod_oficina;
                        p_usuario.Direction = ParameterDirection.Input;
                        p_usuario.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_usuario);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SER_ADICIONALES_ACT";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PlanesTelefonicosData", "CrearAdicional", ex);
                        return false;
                    }
                }
            }
        }
        //Estado de cuenta
        public List<PlanTelefonico> ListarLineas(PlanTelefonico pLinea, DateTime pFecha, Usuario vUsuario, string filtro)
        {
            DbDataReader resultado;
            List<PlanTelefonico> lstLineas = new List<PlanTelefonico>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"SELECT LT.NUM_LINEA_TELEFONICA,S.NUMERO_SERVICIO, S2.NUMERO_SERVICIO AS NUM_ADICIONAL, LT.FECHA_ACTIVACION, LT.FECHA_ULTIMA_REPOSICION, S.VALOR_CUOTA AS CARGO_FIJO,  S2.VALOR_CUOTA AS CARGO_ADICIONAL,
                                    S.VALOR_CUOTA + S2.VALOR_CUOTA AS COSTO_FACTURA, LT.FECHA_VENCIMIENTO, PT.NOMBRE AS TIPO_PLAN, LT.FECHA_INACTIVACION
                                    FROM LINEAS_TELEFONICAS LT
                                    INNER JOIN SERVICIOS S ON LT.COD_SERVICIO_FIJO = S.NUMERO_SERVICIO
                                    INNER JOIN SERVICIOS S2 ON LT.COD_SERVICIO_ADICIONALES = S2.NUMERO_SERVICIO
                                    LEFT OUTER JOIN PLANES_TELEFONICOS PT ON LT.COD_PLAN = PT.COD_PLAN
                                    INNER JOIN PERSONA P ON LT.COD_TITULAR = P.COD_PERSONA";
                        if (filtro.Trim() != "")
                        {
                            sql = sql + filtro;
                        }

                        sql += " ORDER BY LT.NUM_LINEA_TELEFONICA";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            PlanTelefonico entidad = new PlanTelefonico();
                            if (resultado["NUM_LINEA_TELEFONICA"] != DBNull.Value) entidad.num_linea_telefonica = Convert.ToString(resultado["NUM_LINEA_TELEFONICA"]);
                            if (resultado["NUMERO_SERVICIO"] != DBNull.Value) entidad.cod_serv_fijo = Convert.ToInt64(resultado["NUMERO_SERVICIO"]);
                            if (resultado["NUM_ADICIONAL"] != DBNull.Value) entidad.cod_serv_adicional = Convert.ToInt64(resultado["NUM_ADICIONAL"]);
                            if (resultado["FECHA_ACTIVACION"] != DBNull.Value) entidad.fecha_activacion = Convert.ToDateTime(resultado["FECHA_ACTIVACION"]);
                            if (resultado["FECHA_ULTIMA_REPOSICION"] != DBNull.Value) entidad.fecha_ult_reposicion = Convert.ToDateTime(resultado["FECHA_ULTIMA_REPOSICION"]);
                            if (resultado["CARGO_FIJO"] != DBNull.Value) entidad.valor_fijo = Convert.ToDecimal(resultado["CARGO_FIJO"]);
                            if (resultado["CARGO_ADICIONAL"] != DBNull.Value) entidad.valor_adicional = Convert.ToDecimal(resultado["CARGO_ADICIONAL"]);
                            if (resultado["COSTO_FACTURA"] != DBNull.Value) entidad.valor_total = Convert.ToDecimal(resultado["COSTO_FACTURA"]);
                            if (resultado["FECHA_VENCIMIENTO"] != DBNull.Value) entidad.fecha_vencimiento = Convert.ToDateTime(resultado["FECHA_VENCIMIENTO"]);
                            if (resultado["TIPO_PLAN"] != DBNull.Value) entidad.nom_plan = Convert.ToString(resultado["TIPO_PLAN"]);
                            if (resultado["FECHA_INACTIVACION"] != DBNull.Value) entidad.fecha_incativacion = Convert.ToDateTime(resultado["FECHA_INACTIVACION"]);

                            lstLineas.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstLineas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DevolucionData", "ListarDevolucion", ex);
                        return null;
                    }
                }
            }
        }
        public List<PlanTelefonico> ListarLineasAtencionWeb(Usuario vUsuario, string filtro)
        {
            DbDataReader resultado;
            List<PlanTelefonico> lstLineas = new List<PlanTelefonico>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"SELECT LT.NUM_LINEA_TELEFONICA,S.NUMERO_SERVICIO, S2.NUMERO_SERVICIO AS NUM_ADICIONAL, LT.FECHA_ACTIVACION, LT.FECHA_ULTIMA_REPOSICION, S.VALOR_CUOTA AS CARGO_FIJO,  S2.VALOR_CUOTA AS CARGO_ADICIONAL,
                                    S.VALOR_CUOTA + S2.VALOR_CUOTA AS COSTO_FACTURA, LT.FECHA_VENCIMIENTO, PT.NOMBRE AS TIPO_PLAN, LT.FECHA_INACTIVACION
                                    FROM LINEAS_TELEFONICAS LT
                                    INNER JOIN SERVICIOS S ON LT.COD_SERVICIO_FIJO = S.NUMERO_SERVICIO
                                    INNER JOIN SERVICIOS S2 ON LT.COD_SERVICIO_ADICIONALES = S2.NUMERO_SERVICIO
                                    LEFT OUTER JOIN PLANES_TELEFONICOS PT ON LT.COD_PLAN = PT.COD_PLAN
                                    INNER JOIN PERSONA P ON LT.COD_TITULAR = P.COD_PERSONA WHERE P.IDENTIFICACION = '"+filtro+"'";
                       

                       

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            PlanTelefonico entidad = new PlanTelefonico();
                            if (resultado["NUM_LINEA_TELEFONICA"] != DBNull.Value) entidad.num_linea_telefonica = Convert.ToString(resultado["NUM_LINEA_TELEFONICA"]);
                            if (resultado["NUMERO_SERVICIO"] != DBNull.Value) entidad.cod_serv_fijo = Convert.ToInt64(resultado["NUMERO_SERVICIO"]);
                            if (resultado["NUM_ADICIONAL"] != DBNull.Value) entidad.cod_serv_adicional = Convert.ToInt64(resultado["NUM_ADICIONAL"]);
                            if (resultado["FECHA_ACTIVACION"] != DBNull.Value) entidad.fecha_activacion = Convert.ToDateTime(resultado["FECHA_ACTIVACION"]);
                            if (resultado["FECHA_ULTIMA_REPOSICION"] != DBNull.Value) entidad.fecha_ult_reposicion = Convert.ToDateTime(resultado["FECHA_ULTIMA_REPOSICION"]);
                            if (resultado["CARGO_FIJO"] != DBNull.Value) entidad.valor_fijo = Convert.ToDecimal(resultado["CARGO_FIJO"]);
                            if (resultado["CARGO_ADICIONAL"] != DBNull.Value) entidad.valor_adicional = Convert.ToDecimal(resultado["CARGO_ADICIONAL"]);
                            if (resultado["COSTO_FACTURA"] != DBNull.Value) entidad.valor_total = Convert.ToDecimal(resultado["COSTO_FACTURA"]);
                            if (resultado["FECHA_VENCIMIENTO"] != DBNull.Value) entidad.fecha_vencimiento = Convert.ToDateTime(resultado["FECHA_VENCIMIENTO"]);
                            if (resultado["TIPO_PLAN"] != DBNull.Value) entidad.nom_plan = Convert.ToString(resultado["TIPO_PLAN"]);
                            if (resultado["FECHA_INACTIVACION"] != DBNull.Value) entidad.fecha_incativacion = Convert.ToDateTime(resultado["FECHA_INACTIVACION"]);

                            lstLineas.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstLineas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DevolucionData", "ListarLineasAtencionWeb", ex);
                        return null;
                    }
                }
            }
        }
        //Traspaso de la Linea Telefonica y sus repectivos Servicicos
        public PlanTelefonico Traspaso(Int64 vCod_Ope, PlanTelefonico pLinTel, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter p_Num_Linea_Telefonica = cmdTransaccionFactory.CreateParameter();
                        p_Num_Linea_Telefonica.ParameterName = "p_Num_Linea_Telefonica";
                        p_Num_Linea_Telefonica.Value = pLinTel.num_linea_telefonica;
                        p_Num_Linea_Telefonica.Direction = ParameterDirection.InputOutput;
                        p_Num_Linea_Telefonica.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_Num_Linea_Telefonica);


                        DbParameter p_Fecha_Activacion = cmdTransaccionFactory.CreateParameter();
                        p_Fecha_Activacion.ParameterName = "p_Fecha_Activacion";
                        p_Fecha_Activacion.Value = DateTime.Now;
                        p_Fecha_Activacion.Direction = ParameterDirection.Input;
                        p_Fecha_Activacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(p_Fecha_Activacion);

                        DbParameter p_Fecha_vencimiento = cmdTransaccionFactory.CreateParameter();
                        p_Fecha_vencimiento.ParameterName = "p_Fecha_vencimiento";
                        if (pLinTel.fecha_vencimiento == null)
                            p_Fecha_vencimiento.Value = DBNull.Value;
                        else
                            p_Fecha_vencimiento.Value = pLinTel.fecha_vencimiento;
                        p_Fecha_vencimiento.Direction = ParameterDirection.Input;
                        p_Fecha_vencimiento.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(p_Fecha_vencimiento);

                        DbParameter p_Cod_plan = cmdTransaccionFactory.CreateParameter();
                        p_Cod_plan.ParameterName = "p_Cod_plan";
                        p_Cod_plan.Value = pLinTel.cod_plan;
                        p_Cod_plan.Direction = ParameterDirection.Input;
                        p_Cod_plan.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_Cod_plan);

                        DbParameter p_cod_linea_servicio = cmdTransaccionFactory.CreateParameter();
                        p_cod_linea_servicio.ParameterName = "p_cod_linea_servicio";
                        p_cod_linea_servicio.Value = pLinTel.cod_linea_servicio;
                        p_cod_linea_servicio.Direction = ParameterDirection.Input;
                        p_cod_linea_servicio.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_linea_servicio);

                        DbParameter p_fecha_primera_cuota = cmdTransaccionFactory.CreateParameter();
                        p_fecha_primera_cuota.ParameterName = "p_fecha_primera_cuota";
                        p_fecha_primera_cuota.Value = pLinTel.fecha_primera_cuota;
                        p_fecha_primera_cuota.Direction = ParameterDirection.Input;
                        p_fecha_primera_cuota.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(p_fecha_primera_cuota);

                        DbParameter P_VALOR_CUOTA = cmdTransaccionFactory.CreateParameter();
                        P_VALOR_CUOTA.ParameterName = "P_VALOR_CUOTA";
                        P_VALOR_CUOTA.Value = pLinTel.valor_cuota;
                        P_VALOR_CUOTA.Direction = ParameterDirection.Input;
                        P_VALOR_CUOTA.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(P_VALOR_CUOTA);

                        DbParameter P_COD_PERIODICIDAD = cmdTransaccionFactory.CreateParameter();
                        P_COD_PERIODICIDAD.ParameterName = "P_COD_PERIODICIDAD";
                        P_COD_PERIODICIDAD.Value = pLinTel.cod_periodicidad;
                        P_COD_PERIODICIDAD.Direction = ParameterDirection.Input;
                        P_COD_PERIODICIDAD.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(P_COD_PERIODICIDAD);

                        //DbParameter p_Valor_Compra = cmdTransaccionFactory.CreateParameter();
                        //p_Valor_Compra.ParameterName = "p_Valor_Compra";
                        //p_Valor_Compra.Value = pLinTel.valor_compra;
                        //p_Valor_Compra.Direction = ParameterDirection.Input;
                        //p_Valor_Compra.DbType = DbType.Decimal;
                        //cmdTransaccionFactory.Parameters.Add(p_Valor_Compra);

                        //DbParameter p_Beneficio = cmdTransaccionFactory.CreateParameter();
                        //p_Beneficio.ParameterName = "p_Beneficio";
                        //p_Beneficio.Value = pLinTel.beneficio;
                        //p_Beneficio.Direction = ParameterDirection.Input;
                        //p_Beneficio.DbType = DbType.Decimal;
                        //cmdTransaccionFactory.Parameters.Add(p_Beneficio);

                        //DbParameter p_Valor_Mercado = cmdTransaccionFactory.CreateParameter();
                        //p_Valor_Mercado.ParameterName = "p_Valor_Mercado";
                        //p_Valor_Mercado.Value = pLinTel.valor_mercado;
                        //p_Valor_Mercado.Direction = ParameterDirection.Input;
                        //p_Valor_Mercado.DbType = DbType.Decimal;
                        //cmdTransaccionFactory.Parameters.Add(p_Valor_Mercado);

                        //Nuevo titular

                        DbParameter p_Identificacion_nuevo_titular = cmdTransaccionFactory.CreateParameter();
                        p_Identificacion_nuevo_titular.ParameterName = "p_identificacion_nv_titular";
                        p_Identificacion_nuevo_titular.Value = pLinTel.identificacion_nuevo_titular;
                        p_Identificacion_nuevo_titular.Direction = ParameterDirection.InputOutput;
                        p_Identificacion_nuevo_titular.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_Identificacion_nuevo_titular);


                        DbParameter P_forma_pago_nv_titular = cmdTransaccionFactory.CreateParameter();
                        P_forma_pago_nv_titular.ParameterName = "p_forma_pago_nv_titular";
                        P_forma_pago_nv_titular.Value = pLinTel.forma_pago_nv_titular;
                        P_forma_pago_nv_titular.Direction = ParameterDirection.Input;
                        P_forma_pago_nv_titular.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(P_forma_pago_nv_titular);

                        DbParameter p_cod_empresa_nv_titular = cmdTransaccionFactory.CreateParameter();
                        p_cod_empresa_nv_titular.ParameterName = "p_cod_empresa_nv_titular";
                        if (pLinTel.cod_empresa == null)
                            p_cod_empresa_nv_titular.Value = DBNull.Value;
                        else
                            p_cod_empresa_nv_titular.Value = pLinTel.cod_empresa_nv_titular;
                        p_cod_empresa_nv_titular.Direction = ParameterDirection.Input;
                        p_cod_empresa_nv_titular.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_empresa_nv_titular);

                        DbParameter p_cod_ope = cmdTransaccionFactory.CreateParameter();
                        p_cod_ope.ParameterName = "p_cod_ope";
                        p_cod_ope.Value = vCod_Ope;
                        p_cod_ope.Direction = ParameterDirection.Input;
                        p_cod_ope.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_cod_ope);

                        DbParameter p_COD_SERVICIO_FIJO = cmdTransaccionFactory.CreateParameter();
                        p_COD_SERVICIO_FIJO.ParameterName = "p_COD_SERVICIO_FIJO";
                        p_COD_SERVICIO_FIJO.Value = pLinTel.cod_serv_fijo;
                        p_COD_SERVICIO_FIJO.Direction = ParameterDirection.Output;
                        p_COD_SERVICIO_FIJO.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_COD_SERVICIO_FIJO);

                        DbParameter p_COD_SERVICIO_ADICIONALES = cmdTransaccionFactory.CreateParameter();
                        p_COD_SERVICIO_ADICIONALES.ParameterName = "p_COD_SERVICIO_ADICIONALES";
                        p_COD_SERVICIO_ADICIONALES.Value = pLinTel.cod_serv_adicional;
                        p_COD_SERVICIO_ADICIONALES.Direction = ParameterDirection.Output;
                        p_COD_SERVICIO_ADICIONALES.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_COD_SERVICIO_ADICIONALES);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SER_TRASPASO_LIN";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pLinTel;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PlanesTelefonicosData", "Traspaso", ex);
                        return null;
                    }
                }
            }
        }
        //Reposición
        public PlanTelefonico Reposicion(PlanTelefonico pLinTel, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter p_Num_Linea_Telefonica = cmdTransaccionFactory.CreateParameter();
                        p_Num_Linea_Telefonica.ParameterName = "p_Num_Linea_Telefonica";
                        p_Num_Linea_Telefonica.Value = pLinTel.num_linea_telefonica;
                        p_Num_Linea_Telefonica.Direction = ParameterDirection.InputOutput;
                        p_Num_Linea_Telefonica.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_Num_Linea_Telefonica);

                        DbParameter p_Fecha_Reposicion = cmdTransaccionFactory.CreateParameter();
                        p_Fecha_Reposicion.ParameterName = "p_Fecha_Reposicion";
                        p_Fecha_Reposicion.Value = pLinTel.fecha_ult_reposicion;
                        p_Fecha_Reposicion.Direction = ParameterDirection.Input;
                        p_Fecha_Reposicion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(p_Fecha_Reposicion);

                        DbParameter p_Valor_Reposicion = cmdTransaccionFactory.CreateParameter();
                        p_Valor_Reposicion.ParameterName = "p_Valor_Reposicion";
                        p_Valor_Reposicion.Value = pLinTel.valor_reposicion;
                        p_Valor_Reposicion.Direction = ParameterDirection.Input;
                        p_Valor_Reposicion.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_Valor_Reposicion);

                        DbParameter p_Observaciones = cmdTransaccionFactory.CreateParameter();
                        p_Observaciones.ParameterName = "p_Observaciones";
                        p_Observaciones.Value = pLinTel.observaciones;
                        p_Observaciones.Direction = ParameterDirection.Input;
                        p_Observaciones.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_Observaciones);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SER_REPOSICION";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pLinTel;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PlanesTelefonicosData", "Reposicion", ex);
                        return null;
                    }
                }
            }

        }
        //Cancelación
        public PlanTelefonico Cancelacion(PlanTelefonico pLinTel, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter p_Num_Linea_Telefonica = cmdTransaccionFactory.CreateParameter();
                        p_Num_Linea_Telefonica.ParameterName = "p_Num_Linea_Telefonica";
                        p_Num_Linea_Telefonica.Value = pLinTel.num_linea_telefonica;
                        p_Num_Linea_Telefonica.Direction = ParameterDirection.InputOutput;
                        p_Num_Linea_Telefonica.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_Num_Linea_Telefonica);

                        DbParameter p_Fecha_Cancelacion = cmdTransaccionFactory.CreateParameter();
                        p_Fecha_Cancelacion.ParameterName = "p_Fecha_Cancelacion";
                        p_Fecha_Cancelacion.Value = pLinTel.fecha_cancelacion;
                        p_Fecha_Cancelacion.Direction = ParameterDirection.Input;
                        p_Fecha_Cancelacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(p_Fecha_Cancelacion);

                        DbParameter p_Observaciones = cmdTransaccionFactory.CreateParameter();
                        p_Observaciones.ParameterName = "p_Observaciones";
                        if (pLinTel.observaciones == null || pLinTel.observaciones == "")
                            p_Observaciones.Value = DBNull.Value;
                        else
                            p_Observaciones.Value = pLinTel.observaciones;                           
                        p_Observaciones.Direction = ParameterDirection.Input;
                        p_Observaciones.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_Observaciones);

                        DbParameter p_usuario = cmdTransaccionFactory.CreateParameter();
                        p_usuario.ParameterName = "p_usuario";
                        p_usuario.Value = vUsuario.nombre;
                        p_usuario.Direction = ParameterDirection.Input;
                        p_usuario.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_usuario);

                        DbParameter p_cod_cancelacion = cmdTransaccionFactory.CreateParameter();
                        p_cod_cancelacion.ParameterName = "p_cod_cancelacion";
                        p_cod_cancelacion.Value = pLinTel.cod_cacelacion;
                        p_cod_cancelacion.Direction = ParameterDirection.Output;
                        p_cod_cancelacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_cancelacion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SER_CANCELAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pLinTel;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PlanesTelefonicosData", "Cancelacion", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Método de Activación de líneas telefónicas
        /// </summary>
        /// <param name="vCod_Ope"></param>
        /// <param name="pLinTel"></param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public PlanTelefonico ActivacionDeLineasTelefonica(ref string pError, Int64 vCod_Ope, PlanTelefonico pLinTel, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_Num_Linea_Telefonica = cmdTransaccionFactory.CreateParameter();
                        p_Num_Linea_Telefonica.ParameterName = "p_Num_Linea_Telefonica";
                        p_Num_Linea_Telefonica.Value = pLinTel.num_linea_telefonica;
                        p_Num_Linea_Telefonica.Direction = ParameterDirection.Input;
                        p_Num_Linea_Telefonica.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_Num_Linea_Telefonica);
                        
                        DbParameter p_Identificacion_titular = cmdTransaccionFactory.CreateParameter();
                        p_Identificacion_titular.ParameterName = "p_Identificacion_titular";
                        p_Identificacion_titular.Value = pLinTel.identificacion_titular;
                        p_Identificacion_titular.Direction = ParameterDirection.Input;
                        p_Identificacion_titular.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_Identificacion_titular);
                        
                        DbParameter p_Fecha_Activacion = cmdTransaccionFactory.CreateParameter();
                        p_Fecha_Activacion.ParameterName = "p_Fecha_Activacion";
                        p_Fecha_Activacion.Value = pLinTel.fecha_activacion;
                        p_Fecha_Activacion.Direction = ParameterDirection.Input;
                        p_Fecha_Activacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(p_Fecha_Activacion);

                        DbParameter p_Fecha_vencimiento = cmdTransaccionFactory.CreateParameter();
                        p_Fecha_vencimiento.ParameterName = "p_Fecha_vencimiento";
                        if (pLinTel.fecha_vencimiento == null)
                            p_Fecha_vencimiento.Value = DBNull.Value;
                        else
                            p_Fecha_vencimiento.Value = pLinTel.fecha_vencimiento;
                        p_Fecha_vencimiento.Direction = ParameterDirection.Input;
                        p_Fecha_vencimiento.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(p_Fecha_vencimiento);

                        DbParameter p_Cod_plan = cmdTransaccionFactory.CreateParameter();
                        p_Cod_plan.ParameterName = "p_Cod_plan";
                        p_Cod_plan.Value = pLinTel.cod_plan;
                        p_Cod_plan.Direction = ParameterDirection.Input;
                        p_Cod_plan.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_Cod_plan);

                        DbParameter p_cod_linea_servicio = cmdTransaccionFactory.CreateParameter();
                        p_cod_linea_servicio.ParameterName = "p_cod_linea_servicio";
                        p_cod_linea_servicio.Value = pLinTel.cod_linea_servicio;
                        p_cod_linea_servicio.Direction = ParameterDirection.Input;
                        p_cod_linea_servicio.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_linea_servicio);

                        DbParameter p_fecha_primera_cuota = cmdTransaccionFactory.CreateParameter();
                        p_fecha_primera_cuota.ParameterName = "p_fecha_primera_cuota";
                        p_fecha_primera_cuota.Value = pLinTel.fecha_primera_cuota;
                        p_fecha_primera_cuota.Direction = ParameterDirection.Input;
                        p_fecha_primera_cuota.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(p_fecha_primera_cuota);

                        DbParameter P_VALOR_CUOTA = cmdTransaccionFactory.CreateParameter();
                        P_VALOR_CUOTA.ParameterName = "P_VALOR_CUOTA";
                        P_VALOR_CUOTA.Value = pLinTel.valor_cuota;
                        P_VALOR_CUOTA.Direction = ParameterDirection.Input;
                        P_VALOR_CUOTA.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(P_VALOR_CUOTA);

                        DbParameter P_COD_PERIODICIDAD = cmdTransaccionFactory.CreateParameter();
                        P_COD_PERIODICIDAD.ParameterName = "P_COD_PERIODICIDAD";
                        P_COD_PERIODICIDAD.Value = pLinTel.cod_periodicidad;
                        P_COD_PERIODICIDAD.Direction = ParameterDirection.Input;
                        P_COD_PERIODICIDAD.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(P_COD_PERIODICIDAD);

                        DbParameter P_FORMA_PAGO = cmdTransaccionFactory.CreateParameter();
                        P_FORMA_PAGO.ParameterName = "P_FORMA_PAGO";
                        P_FORMA_PAGO.Value = pLinTel.forma_pago;
                        P_FORMA_PAGO.Direction = ParameterDirection.Input;
                        P_FORMA_PAGO.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(P_FORMA_PAGO);

                        DbParameter p_cod_empresa = cmdTransaccionFactory.CreateParameter();
                        p_cod_empresa.ParameterName = "p_cod_empresa";
                        if (pLinTel.cod_empresa == null)
                            p_cod_empresa.Value = DBNull.Value;
                        else
                            p_cod_empresa.Value = pLinTel.cod_empresa;
                        p_cod_empresa.Direction = ParameterDirection.Input;
                        p_cod_empresa.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_empresa);

                        DbParameter p_Valor_Compra = cmdTransaccionFactory.CreateParameter();
                        p_Valor_Compra.ParameterName = "p_Valor_Compra";
                        if (pLinTel.valor_compra == null)
                            p_Valor_Compra.Value = 0;
                        else
                            p_Valor_Compra.Value = pLinTel.valor_compra;
                        p_Valor_Compra.Direction = ParameterDirection.Input;
                        p_Valor_Compra.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_Valor_Compra);

                        DbParameter p_Beneficio = cmdTransaccionFactory.CreateParameter();
                        p_Beneficio.ParameterName = "p_Beneficio";
                        if (pLinTel.beneficio == null)
                            p_Beneficio.Value = 0;
                        else
                            p_Beneficio.Value = pLinTel.beneficio;
                        p_Beneficio.Direction = ParameterDirection.Input;
                        p_Beneficio.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_Beneficio);

                        DbParameter p_Valor_Mercado = cmdTransaccionFactory.CreateParameter();
                        p_Valor_Mercado.ParameterName = "p_Valor_Mercado";
                        if (pLinTel.valor_mercado == null)
                            p_Valor_Mercado.Value = 0;
                        else
                            p_Valor_Mercado.Value = pLinTel.valor_mercado;
                        p_Valor_Mercado.Direction = ParameterDirection.Input;
                        p_Valor_Mercado.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_Valor_Mercado);

                        DbParameter p_COD_OPE = cmdTransaccionFactory.CreateParameter();
                        p_COD_OPE.ParameterName = "p_cod_ope";
                        p_COD_OPE.Value = vCod_Ope;
                        p_COD_OPE.Direction = ParameterDirection.Input;
                        p_COD_OPE.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_COD_OPE);

                        DbParameter p_COD_SERVICIO_FIJO = cmdTransaccionFactory.CreateParameter();
                        p_COD_SERVICIO_FIJO.ParameterName = "p_COD_SERVICIO_FIJO";
                        p_COD_SERVICIO_FIJO.Value = pLinTel.cod_serv_fijo;
                        p_COD_SERVICIO_FIJO.Direction = ParameterDirection.Output;
                        p_COD_SERVICIO_FIJO.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_COD_SERVICIO_FIJO);

                        DbParameter p_COD_SERVICIO_ADICIONALES = cmdTransaccionFactory.CreateParameter();
                        p_COD_SERVICIO_ADICIONALES.ParameterName = "p_COD_SERVICIO_ADICIONALES";
                        p_COD_SERVICIO_ADICIONALES.Value = pLinTel.cod_serv_adicional;
                        p_COD_SERVICIO_ADICIONALES.Direction = ParameterDirection.Output;
                        p_COD_SERVICIO_ADICIONALES.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_COD_SERVICIO_ADICIONALES);

                        DbParameter P_MENSAJE = cmdTransaccionFactory.CreateParameter();
                        P_MENSAJE.ParameterName = "P_MENSAJE";
                        P_MENSAJE.Value = DBNull.Value;
                        P_MENSAJE.Direction = ParameterDirection.Output;
                        P_MENSAJE.DbType = DbType.String;
                        P_MENSAJE.Size = 2000;
                        cmdTransaccionFactory.Parameters.Add(P_MENSAJE);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SER_LIN_TELE_ACTIV";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        if (p_COD_SERVICIO_FIJO.Value != DBNull.Value)
                            pLinTel.cod_serv_fijo = Convert.ToInt64(p_COD_SERVICIO_FIJO.Value);
                        if (p_COD_SERVICIO_ADICIONALES.Value != DBNull.Value)
                            pLinTel.cod_serv_adicional = Convert.ToInt64(p_COD_SERVICIO_ADICIONALES.Value);
                        if (P_MENSAJE.Value != DBNull.Value)
                            pError = Convert.ToString(P_MENSAJE.Value);

                        return pLinTel;
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                        return null;
                    }
                }
            }
        }


    }
}
