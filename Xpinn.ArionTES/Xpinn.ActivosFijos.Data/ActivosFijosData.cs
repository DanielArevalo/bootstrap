using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.ActivosFijos.Entities;

namespace Xpinn.ActivosFijos.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla ActivoFijos
    /// </summary>
    public class ActivosFijosData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla ACTIVOS_FIJOS
        /// </summary>
        public ActivosFijosData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public ActivoFijo CrearActivoFijo(ActivoFijo pActivoFijo, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pActivoFijo.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Output;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcod_act = cmdTransaccionFactory.CreateParameter();
                        pcod_act.ParameterName = "p_cod_act";
                        pcod_act.Value = pActivoFijo.cod_act;
                        pcod_act.Direction = ParameterDirection.Input;
                        pcod_act.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_act);

                        DbParameter pclase = cmdTransaccionFactory.CreateParameter();
                        pclase.ParameterName = "p_clase";
                        pclase.Value = pActivoFijo.clase;
                        pclase.Direction = ParameterDirection.Input;
                        pclase.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pclase);

                        DbParameter ptipo = cmdTransaccionFactory.CreateParameter();
                        ptipo.ParameterName = "p_tipo";
                        ptipo.Value = pActivoFijo.tipo;
                        ptipo.Direction = ParameterDirection.Input;
                        ptipo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo);

                        DbParameter pcod_ubica = cmdTransaccionFactory.CreateParameter();
                        pcod_ubica.ParameterName = "p_cod_ubica";
                        pcod_ubica.Value = pActivoFijo.cod_ubica;
                        pcod_ubica.Direction = ParameterDirection.Input;
                        pcod_ubica.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_ubica);

                        DbParameter pcod_costo = cmdTransaccionFactory.CreateParameter();
                        pcod_costo.ParameterName = "p_cod_costo";
                        pcod_costo.Value = pActivoFijo.cod_costo;
                        pcod_costo.Direction = ParameterDirection.Input;
                        pcod_costo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_costo);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "p_nombre";
                        pnombre.Value = pActivoFijo.nombre;
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        DbParameter panos_util = cmdTransaccionFactory.CreateParameter();
                        panos_util.ParameterName = "p_anos_util";
                        panos_util.Value = pActivoFijo.anos_util;
                        panos_util.Direction = ParameterDirection.Input;
                        panos_util.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(panos_util);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pActivoFijo.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pserial = cmdTransaccionFactory.CreateParameter();
                        pserial.ParameterName = "p_serial";
                        pserial.Value = pActivoFijo.serial;
                        pserial.Direction = ParameterDirection.Input;
                        pserial.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pserial);

                        DbParameter pcod_encargado = cmdTransaccionFactory.CreateParameter();
                        pcod_encargado.ParameterName = "p_cod_encargado";
                        pcod_encargado.Value = pActivoFijo.cod_encargado;
                        pcod_encargado.Direction = ParameterDirection.Input;
                        pcod_encargado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_encargado);

                        DbParameter pfecha_compra = cmdTransaccionFactory.CreateParameter();
                        pfecha_compra.ParameterName = "p_fecha_compra";
                        pfecha_compra.Value = pActivoFijo.fecha_compra;
                        pfecha_compra.Direction = ParameterDirection.Input;
                        pfecha_compra.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_compra);

                        DbParameter pvalor_compra = cmdTransaccionFactory.CreateParameter();
                        pvalor_compra.ParameterName = "p_valor_compra";
                        pvalor_compra.Value = pActivoFijo.valor_compra;
                        pvalor_compra.Direction = ParameterDirection.Input;
                        pvalor_compra.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_compra);

                        DbParameter pvalor_avaluo = cmdTransaccionFactory.CreateParameter();
                        pvalor_avaluo.ParameterName = "p_valor_avaluo";
                        pvalor_avaluo.Value = pActivoFijo.valor_avaluo;
                        pvalor_avaluo.Direction = ParameterDirection.Input;
                        pvalor_avaluo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_avaluo);

                        DbParameter pvalor_salvamen = cmdTransaccionFactory.CreateParameter();
                        pvalor_salvamen.ParameterName = "p_valor_salvamen";
                        pvalor_salvamen.Value = pActivoFijo.valor_salvamen;
                        pvalor_salvamen.Direction = ParameterDirection.Input;
                        pvalor_salvamen.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_salvamen);

                        DbParameter pnum_factura = cmdTransaccionFactory.CreateParameter();
                        pnum_factura.ParameterName = "p_num_factura";
                        pnum_factura.Value = pActivoFijo.num_factura;
                        pnum_factura.Direction = ParameterDirection.Input;
                        pnum_factura.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pnum_factura);

                        DbParameter pcod_proveedor = cmdTransaccionFactory.CreateParameter();
                        pcod_proveedor.ParameterName = "p_cod_proveedor";
                        pcod_proveedor.Value = pActivoFijo.cod_proveedor;
                        pcod_proveedor.Direction = ParameterDirection.Input;
                        pcod_proveedor.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_proveedor);

                        DbParameter pobservaciones = cmdTransaccionFactory.CreateParameter();
                        pobservaciones.ParameterName = "p_observaciones";
                        pobservaciones.Value = pActivoFijo.observaciones;
                        pobservaciones.Direction = ParameterDirection.Input;
                        pobservaciones.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pobservaciones);

                        DbParameter pcod_oficina = cmdTransaccionFactory.CreateParameter();
                        pcod_oficina.ParameterName = "p_cod_oficina";
                        pcod_oficina.Value = pActivoFijo.cod_oficina;
                        pcod_oficina.Direction = ParameterDirection.Input;
                        pcod_oficina.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_oficina);

                        DbParameter pfecha_ult_depre = cmdTransaccionFactory.CreateParameter();
                        pfecha_ult_depre.ParameterName = "p_fecha_ult_depre";
                        pfecha_ult_depre.Value = pActivoFijo.fecha_ult_depre;
                        pfecha_ult_depre.Direction = ParameterDirection.Input;
                        pfecha_ult_depre.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_ult_depre);

                        DbParameter pacumulado_depreciacion = cmdTransaccionFactory.CreateParameter();
                        pacumulado_depreciacion.ParameterName = "p_acumulado_depreciacion";
                        pacumulado_depreciacion.Value = pActivoFijo.acumulado_depreciacion;
                        pacumulado_depreciacion.Direction = ParameterDirection.Input;
                        pacumulado_depreciacion.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pacumulado_depreciacion);

                        DbParameter psaldo_por_depreciar = cmdTransaccionFactory.CreateParameter();
                        psaldo_por_depreciar.ParameterName = "p_saldo_por_depreciar";
                        psaldo_por_depreciar.Value = pActivoFijo.saldo_por_depreciar;
                        psaldo_por_depreciar.Direction = ParameterDirection.Input;
                        psaldo_por_depreciar.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(psaldo_por_depreciar);

                        DbParameter pfechacreacion = cmdTransaccionFactory.CreateParameter();
                        pfechacreacion.ParameterName = "p_fechacreacion";
                        pfechacreacion.Value = pActivoFijo.fechacreacion;
                        pfechacreacion.Direction = ParameterDirection.Input;
                        pfechacreacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechacreacion);

                        DbParameter pusuariocreacion = cmdTransaccionFactory.CreateParameter();
                        pusuariocreacion.ParameterName = "p_usuariocreacion";
                        pusuariocreacion.Value = pActivoFijo.usuariocreacion;
                        pusuariocreacion.Direction = ParameterDirection.Input;
                        pusuariocreacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pusuariocreacion);

                        DbParameter pfecultmod = cmdTransaccionFactory.CreateParameter();
                        pfecultmod.ParameterName = "p_fecultmod";
                        pfecultmod.Value = pActivoFijo.fecultmod;
                        pfecultmod.Direction = ParameterDirection.Input;
                        pfecultmod.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecultmod);

                        DbParameter pusuultmod = cmdTransaccionFactory.CreateParameter();
                        pusuultmod.ParameterName = "p_usuultmod";
                        pusuultmod.Value = pActivoFijo.usuultmod;
                        pusuultmod.Direction = ParameterDirection.Input;
                        pusuultmod.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pusuultmod);

                        DbParameter pimagen = cmdTransaccionFactory.CreateParameter();
                        pimagen.ParameterName = "p_imagen";
                        pimagen.Value = DBNull.Value;
                        pimagen.Direction = ParameterDirection.Input;
                        pimagen.DbType = DbType.Binary;
                        cmdTransaccionFactory.Parameters.Add(pimagen);

                        //AGREGADO_NIIF                       
                        DbParameter pcodclasificacion_nif = cmdTransaccionFactory.CreateParameter();
                        pcodclasificacion_nif.ParameterName = "p_codclasificacion_nif";
                        pcodclasificacion_nif.Value = pActivoFijo.codclasificacion_nif;
                        pcodclasificacion_nif.Direction = ParameterDirection.Input;
                        pcodclasificacion_nif.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodclasificacion_nif);

                        DbParameter ptipo_activo_nif = cmdTransaccionFactory.CreateParameter();
                        ptipo_activo_nif.ParameterName = "p_tipo_activo_nif";
                        ptipo_activo_nif.Value = pActivoFijo.tipo_activo_nif;
                        ptipo_activo_nif.Direction = ParameterDirection.Input;
                        ptipo_activo_nif.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_activo_nif);

                        DbParameter pmetodo_costeo_nif = cmdTransaccionFactory.CreateParameter();
                        pmetodo_costeo_nif.ParameterName = "p_metodo_costeo_nif";
                        pmetodo_costeo_nif.Value = pActivoFijo.metodo_costeo_nif;
                        pmetodo_costeo_nif.Direction = ParameterDirection.Input;
                        pmetodo_costeo_nif.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pmetodo_costeo_nif);

                        DbParameter pvalor_activo_nif = cmdTransaccionFactory.CreateParameter();
                        pvalor_activo_nif.ParameterName = "p_valor_activo_nif";
                        pvalor_activo_nif.Value = pActivoFijo.valor_activo_nif;
                        pvalor_activo_nif.Direction = ParameterDirection.Input;
                        pvalor_activo_nif.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_activo_nif);

                        DbParameter pvida_util_nif = cmdTransaccionFactory.CreateParameter();
                        pvida_util_nif.ParameterName = "p_vida_util_nif";
                        pvida_util_nif.Value = pActivoFijo.vida_util_nif;
                        pvida_util_nif.Direction = ParameterDirection.Input;
                        pvida_util_nif.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvida_util_nif);

                        DbParameter pvalor_residual_nif = cmdTransaccionFactory.CreateParameter();
                        pvalor_residual_nif.ParameterName = "p_valor_residual_nif";
                        pvalor_residual_nif.Value = pActivoFijo.valor_residual_nif;
                        pvalor_residual_nif.Direction = ParameterDirection.Input;
                        pvalor_residual_nif.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_residual_nif);

                        DbParameter pporcentaje_residual_nif = cmdTransaccionFactory.CreateParameter();
                        pporcentaje_residual_nif.ParameterName = "p_porcentaje_residual_nif";
                        pporcentaje_residual_nif.Value = pActivoFijo.porcentaje_residual_nif;
                        pporcentaje_residual_nif.Direction = ParameterDirection.Input;
                        pporcentaje_residual_nif.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pporcentaje_residual_nif);

                        DbParameter punigeneradora_nif = cmdTransaccionFactory.CreateParameter();
                        punigeneradora_nif.ParameterName = "p_unigeneradora_nif";
                        punigeneradora_nif.Value = pActivoFijo.unigeneradora_nif;
                        punigeneradora_nif.Direction = ParameterDirection.Input;
                        punigeneradora_nif.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(punigeneradora_nif);

                        DbParameter padiciones_nif = cmdTransaccionFactory.CreateParameter();
                        padiciones_nif.ParameterName = "p_adiciones_nif";
                        padiciones_nif.Value = pActivoFijo.adiciones_nif;
                        padiciones_nif.Direction = ParameterDirection.Input;
                        padiciones_nif.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(padiciones_nif);

                        DbParameter pvrdeterioro_nif = cmdTransaccionFactory.CreateParameter();
                        pvrdeterioro_nif.ParameterName = "p_vrdeterioro_nif";
                        pvrdeterioro_nif.Value = pActivoFijo.vrdeterioro_nif;
                        pvrdeterioro_nif.Direction = ParameterDirection.Input;
                        pvrdeterioro_nif.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvrdeterioro_nif);

                        DbParameter pvrrecdeterioro_nif = cmdTransaccionFactory.CreateParameter();
                        pvrrecdeterioro_nif.ParameterName = "p_vrrecdeterioro_nif";
                        pvrrecdeterioro_nif.Value = pActivoFijo.vrrecdeterioro_nif;
                        pvrrecdeterioro_nif.Direction = ParameterDirection.Input;
                        pvrrecdeterioro_nif.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvrrecdeterioro_nif);

                        DbParameter prevaluacion_nif = cmdTransaccionFactory.CreateParameter();
                        prevaluacion_nif.ParameterName = "p_revaluacion_nif";
                        prevaluacion_nif.Value = pActivoFijo.revaluacion_nif;
                        prevaluacion_nif.Direction = ParameterDirection.Input;
                        prevaluacion_nif.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(prevaluacion_nif);

                        DbParameter prevrevaluacion_nif = cmdTransaccionFactory.CreateParameter();
                        prevrevaluacion_nif.ParameterName = "p_revrevaluacion_nif";
                        prevrevaluacion_nif.Value = pActivoFijo.revrevaluacion_nif;
                        prevrevaluacion_nif.Direction = ParameterDirection.Input;
                        prevrevaluacion_nif.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(prevrevaluacion_nif);

                        DbParameter pcod_moneda = cmdTransaccionFactory.CreateParameter();
                        pcod_moneda.ParameterName = "p_cod_moneda";
                        pcod_moneda.Value = pActivoFijo.cod_moneda;
                        pcod_moneda.Direction = ParameterDirection.Input;
                        pcod_moneda.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_moneda);

                        DbParameter p_fecha_ult_adicion = cmdTransaccionFactory.CreateParameter();
                        p_fecha_ult_adicion.ParameterName = "p_fecha_ult_adicion";
                        if (!pActivoFijo.fecha_ult_adicion.HasValue)
                            p_fecha_ult_adicion.Value = pActivoFijo.fecha_ult_adicion;
                        else
                            p_fecha_ult_adicion.Value = DBNull.Value;
                        p_fecha_ult_adicion.Direction = ParameterDirection.Input;
                        p_fecha_ult_adicion.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(p_fecha_ult_adicion);

                        DbParameter puso_del_bien = cmdTransaccionFactory.CreateParameter();
                        puso_del_bien.ParameterName = "p_uso_del_bien";
                        if(pActivoFijo.uso_del_bien != null)
                            puso_del_bien.Value = pActivoFijo.uso_del_bien;
                        else
                            puso_del_bien.Value = DBNull.Value;
                        puso_del_bien.Direction = ParameterDirection.Input;
                        puso_del_bien.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(puso_del_bien);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_ACT_ACTIVOFIJO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pActivoFijo.consecutivo = Convert.ToInt64(pconsecutivo.Value);

                        return pActivoFijo;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActivoFijoData", "CrearActivoFijo", ex);
                        return null;
                    }
                }
            }
        }




        /// <summary>
        /// Modifica un registro en la tabla ActivoFijo de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad ActivoFijo modificada</returns>
        public ActivoFijo ModificarActivoFijo(ActivoFijo pActivoFijo, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pActivoFijo.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcod_act = cmdTransaccionFactory.CreateParameter();
                        pcod_act.ParameterName = "p_cod_act";
                        pcod_act.Value = pActivoFijo.cod_act;
                        pcod_act.Direction = ParameterDirection.Input;
                        pcod_act.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_act);

                        DbParameter pclase = cmdTransaccionFactory.CreateParameter();
                        pclase.ParameterName = "p_clase";
                        pclase.Value = pActivoFijo.clase;
                        pclase.Direction = ParameterDirection.Input;
                        pclase.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pclase);

                        DbParameter ptipo = cmdTransaccionFactory.CreateParameter();
                        ptipo.ParameterName = "p_tipo";
                        ptipo.Value = pActivoFijo.tipo;
                        ptipo.Direction = ParameterDirection.Input;
                        ptipo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo);

                        DbParameter pcod_ubica = cmdTransaccionFactory.CreateParameter();
                        pcod_ubica.ParameterName = "p_cod_ubica";
                        pcod_ubica.Value = pActivoFijo.cod_ubica;
                        pcod_ubica.Direction = ParameterDirection.Input;
                        pcod_ubica.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_ubica);

                        DbParameter pcod_costo = cmdTransaccionFactory.CreateParameter();
                        pcod_costo.ParameterName = "p_cod_costo";
                        pcod_costo.Value = pActivoFijo.cod_costo;
                        pcod_costo.Direction = ParameterDirection.Input;
                        pcod_costo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_costo);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "p_nombre";
                        pnombre.Value = pActivoFijo.nombre;
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        DbParameter panos_util = cmdTransaccionFactory.CreateParameter();
                        panos_util.ParameterName = "p_anos_util";
                        panos_util.Value = pActivoFijo.anos_util;
                        panos_util.Direction = ParameterDirection.Input;
                        panos_util.DbType = DbType.Double;
                        cmdTransaccionFactory.Parameters.Add(panos_util);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pActivoFijo.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pserial = cmdTransaccionFactory.CreateParameter();
                        pserial.ParameterName = "p_serial";
                        pserial.Value = pActivoFijo.serial;
                        pserial.Direction = ParameterDirection.Input;
                        pserial.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pserial);

                        DbParameter pcod_encargado = cmdTransaccionFactory.CreateParameter();
                        pcod_encargado.ParameterName = "p_cod_encargado";
                        pcod_encargado.Value = pActivoFijo.cod_encargado;
                        pcod_encargado.Direction = ParameterDirection.Input;
                        pcod_encargado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_encargado);

                        DbParameter pfecha_compra = cmdTransaccionFactory.CreateParameter();
                        pfecha_compra.ParameterName = "p_fecha_compra";
                        pfecha_compra.Value = pActivoFijo.fecha_compra;
                        pfecha_compra.Direction = ParameterDirection.Input;
                        pfecha_compra.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_compra);

                        DbParameter pvalor_compra = cmdTransaccionFactory.CreateParameter();
                        pvalor_compra.ParameterName = "p_valor_compra";
                        pvalor_compra.Value = pActivoFijo.valor_compra;
                        pvalor_compra.Direction = ParameterDirection.Input;
                        pvalor_compra.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_compra);

                        DbParameter pvalor_avaluo = cmdTransaccionFactory.CreateParameter();
                        pvalor_avaluo.ParameterName = "p_valor_avaluo";
                        pvalor_avaluo.Value = pActivoFijo.valor_avaluo;
                        pvalor_avaluo.Direction = ParameterDirection.Input;
                        pvalor_avaluo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_avaluo);

                        DbParameter pvalor_salvamen = cmdTransaccionFactory.CreateParameter();
                        pvalor_salvamen.ParameterName = "p_valor_salvamen";
                        pvalor_salvamen.Value = pActivoFijo.valor_salvamen;
                        pvalor_salvamen.Direction = ParameterDirection.Input;
                        pvalor_salvamen.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_salvamen);

                        DbParameter pnum_factura = cmdTransaccionFactory.CreateParameter();
                        pnum_factura.ParameterName = "p_num_factura";
                        pnum_factura.Value = pActivoFijo.num_factura;
                        pnum_factura.Direction = ParameterDirection.Input;
                        pnum_factura.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pnum_factura);

                        DbParameter pcod_proveedor = cmdTransaccionFactory.CreateParameter();
                        pcod_proveedor.ParameterName = "p_cod_proveedor";
                        pcod_proveedor.Value = pActivoFijo.cod_proveedor;
                        pcod_proveedor.Direction = ParameterDirection.Input;
                        pcod_proveedor.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_proveedor);

                        DbParameter pobservaciones = cmdTransaccionFactory.CreateParameter();
                        pobservaciones.ParameterName = "p_observaciones";
                        pobservaciones.Value = pActivoFijo.observaciones;
                        pobservaciones.Direction = ParameterDirection.Input;
                        pobservaciones.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pobservaciones);

                        DbParameter pcod_oficina = cmdTransaccionFactory.CreateParameter();
                        pcod_oficina.ParameterName = "p_cod_oficina";
                        pcod_oficina.Value = pActivoFijo.cod_oficina;
                        pcod_oficina.Direction = ParameterDirection.Input;
                        pcod_oficina.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_oficina);

                        DbParameter pfecha_ult_depre = cmdTransaccionFactory.CreateParameter();
                        pfecha_ult_depre.ParameterName = "p_fecha_ult_depre";
                        pfecha_ult_depre.Value = pActivoFijo.fecha_ult_depre;
                        pfecha_ult_depre.Direction = ParameterDirection.Input;
                        pfecha_ult_depre.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_ult_depre);

                        DbParameter pacumulado_depreciacion = cmdTransaccionFactory.CreateParameter();
                        pacumulado_depreciacion.ParameterName = "p_acumulado_depreciacion";
                        pacumulado_depreciacion.Value = pActivoFijo.acumulado_depreciacion;
                        pacumulado_depreciacion.Direction = ParameterDirection.Input;
                        pacumulado_depreciacion.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pacumulado_depreciacion);

                        DbParameter psaldo_por_depreciar = cmdTransaccionFactory.CreateParameter();
                        psaldo_por_depreciar.ParameterName = "p_saldo_por_depreciar";
                        psaldo_por_depreciar.Value = pActivoFijo.saldo_por_depreciar;
                        psaldo_por_depreciar.Direction = ParameterDirection.Input;
                        psaldo_por_depreciar.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(psaldo_por_depreciar);

                        DbParameter pfechacreacion = cmdTransaccionFactory.CreateParameter();
                        pfechacreacion.ParameterName = "p_fechacreacion";
                        pfechacreacion.Value = pActivoFijo.fechacreacion;
                        pfechacreacion.Direction = ParameterDirection.Input;
                        pfechacreacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechacreacion);

                        DbParameter pusuariocreacion = cmdTransaccionFactory.CreateParameter();
                        pusuariocreacion.ParameterName = "p_usuariocreacion";
                        pusuariocreacion.Value = pActivoFijo.usuariocreacion;
                        pusuariocreacion.Direction = ParameterDirection.Input;
                        pusuariocreacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pusuariocreacion);

                        DbParameter pfecultmod = cmdTransaccionFactory.CreateParameter();
                        pfecultmod.ParameterName = "p_fecultmod";
                        pfecultmod.Value = pActivoFijo.fecultmod;
                        pfecultmod.Direction = ParameterDirection.Input;
                        pfecultmod.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecultmod);

                        DbParameter pusuultmod = cmdTransaccionFactory.CreateParameter();
                        pusuultmod.ParameterName = "p_usuultmod";
                        pusuultmod.Value = pActivoFijo.usuultmod;
                        pusuultmod.Direction = ParameterDirection.Input;
                        pusuultmod.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pusuultmod);

                        DbParameter pimagen = cmdTransaccionFactory.CreateParameter();
                        pimagen.ParameterName = "p_imagen";
                        pimagen.Value = DBNull.Value;
                        pimagen.Direction = ParameterDirection.Input;
                        pimagen.DbType = DbType.Binary;
                        cmdTransaccionFactory.Parameters.Add(pimagen);


                        //AGREGADO_NIIF                       

                        DbParameter pcodclasificacion_nif = cmdTransaccionFactory.CreateParameter();
                        pcodclasificacion_nif.ParameterName = "p_codclasificacion_nif";
                        pcodclasificacion_nif.Value = pActivoFijo.codclasificacion_nif;
                        pcodclasificacion_nif.Direction = ParameterDirection.Input;
                        pcodclasificacion_nif.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodclasificacion_nif);

                        DbParameter ptipo_activo_nif = cmdTransaccionFactory.CreateParameter();
                        ptipo_activo_nif.ParameterName = "p_tipo_activo_nif";
                        ptipo_activo_nif.Value = pActivoFijo.tipo_activo_nif;
                        ptipo_activo_nif.Direction = ParameterDirection.Input;
                        ptipo_activo_nif.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_activo_nif);

                        DbParameter pmetodo_costeo_nif = cmdTransaccionFactory.CreateParameter();
                        pmetodo_costeo_nif.ParameterName = "p_metodo_costeo_nif";
                        pmetodo_costeo_nif.Value = pActivoFijo.metodo_costeo_nif;
                        pmetodo_costeo_nif.Direction = ParameterDirection.Input;
                        pmetodo_costeo_nif.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pmetodo_costeo_nif);

                        DbParameter pvalor_activo_nif = cmdTransaccionFactory.CreateParameter();
                        pvalor_activo_nif.ParameterName = "p_valor_activo_nif";
                        pvalor_activo_nif.Value = pActivoFijo.valor_activo_nif;
                        pvalor_activo_nif.Direction = ParameterDirection.Input;
                        pvalor_activo_nif.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_activo_nif);

                        DbParameter pvida_util_nif = cmdTransaccionFactory.CreateParameter();
                        pvida_util_nif.ParameterName = "p_vida_util_nif";
                        pvida_util_nif.Value = pActivoFijo.vida_util_nif;
                        pvida_util_nif.Direction = ParameterDirection.Input;
                        pvida_util_nif.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvida_util_nif);

                        DbParameter pvalor_residual_nif = cmdTransaccionFactory.CreateParameter();
                        pvalor_residual_nif.ParameterName = "p_valor_residual_nif";
                        pvalor_residual_nif.Value = pActivoFijo.valor_residual_nif;
                        pvalor_residual_nif.Direction = ParameterDirection.Input;
                        pvalor_residual_nif.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_residual_nif);

                        DbParameter pporcentaje_residual_nif = cmdTransaccionFactory.CreateParameter();
                        pporcentaje_residual_nif.ParameterName = "p_porcentaje_residual_nif";
                        pporcentaje_residual_nif.Value = pActivoFijo.porcentaje_residual_nif;
                        pporcentaje_residual_nif.Direction = ParameterDirection.Input;
                        pporcentaje_residual_nif.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pporcentaje_residual_nif);

                        DbParameter punigeneradora_nif = cmdTransaccionFactory.CreateParameter();
                        punigeneradora_nif.ParameterName = "p_unigeneradora_nif";
                        punigeneradora_nif.Value = pActivoFijo.unigeneradora_nif;
                        punigeneradora_nif.Direction = ParameterDirection.Input;
                        punigeneradora_nif.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(punigeneradora_nif);

                        DbParameter padiciones_nif = cmdTransaccionFactory.CreateParameter();
                        padiciones_nif.ParameterName = "p_adiciones_nif";
                        padiciones_nif.Value = pActivoFijo.adiciones_nif;
                        padiciones_nif.Direction = ParameterDirection.Input;
                        padiciones_nif.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(padiciones_nif);

                        DbParameter pvrdeterioro_nif = cmdTransaccionFactory.CreateParameter();
                        pvrdeterioro_nif.ParameterName = "p_vrdeterioro_nif";
                        pvrdeterioro_nif.Value = pActivoFijo.vrdeterioro_nif;
                        pvrdeterioro_nif.Direction = ParameterDirection.Input;
                        pvrdeterioro_nif.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvrdeterioro_nif);

                        DbParameter pvrrecdeterioro_nif = cmdTransaccionFactory.CreateParameter();
                        pvrrecdeterioro_nif.ParameterName = "p_vrrecdeterioro_nif";
                        pvrrecdeterioro_nif.Value = pActivoFijo.vrrecdeterioro_nif;
                        pvrrecdeterioro_nif.Direction = ParameterDirection.Input;
                        pvrrecdeterioro_nif.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvrrecdeterioro_nif);

                        DbParameter prevaluacion_nif = cmdTransaccionFactory.CreateParameter();
                        prevaluacion_nif.ParameterName = "p_revaluacion_nif";
                        prevaluacion_nif.Value = pActivoFijo.revaluacion_nif;
                        prevaluacion_nif.Direction = ParameterDirection.Input;
                        prevaluacion_nif.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(prevaluacion_nif);

                        DbParameter prevrevaluacion_nif = cmdTransaccionFactory.CreateParameter();
                        prevrevaluacion_nif.ParameterName = "p_revrevaluacion_nif";
                        prevrevaluacion_nif.Value = pActivoFijo.revrevaluacion_nif;
                        prevrevaluacion_nif.Direction = ParameterDirection.Input;
                        prevrevaluacion_nif.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(prevrevaluacion_nif);

                        DbParameter pcod_moneda = cmdTransaccionFactory.CreateParameter();
                        pcod_moneda.ParameterName = "p_cod_moneda";
                        pcod_moneda.Value = pActivoFijo.cod_moneda;
                        pcod_moneda.Direction = ParameterDirection.Input;
                        pcod_moneda.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_moneda);

                        DbParameter pfecha_ult_adicion = cmdTransaccionFactory.CreateParameter();
                        pfecha_ult_adicion.ParameterName = "p_fecha_ult_adicion";
                        if (pActivoFijo.fecha_ult_adicion != null)
                            pfecha_ult_adicion.Value = pActivoFijo.fecha_ult_adicion;
                        else
                            pfecha_ult_adicion.Value = DBNull.Value;
                        pfecha_ult_adicion.Direction = ParameterDirection.Input;
                        pfecha_ult_adicion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_ult_adicion);

                        DbParameter puso_del_bien = cmdTransaccionFactory.CreateParameter();
                        puso_del_bien.ParameterName = "p_uso_del_bien";
                        if (pActivoFijo.uso_del_bien != null)
                            puso_del_bien.Value = pActivoFijo.uso_del_bien;
                        else
                            puso_del_bien.Value = DBNull.Value;
                        puso_del_bien.Direction = ParameterDirection.Input;
                        puso_del_bien.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(puso_del_bien);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_ACT_ACTIVOFIJO_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pActivoFijo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActivoFijoData", "ModificarActivoFijo", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla ActivoFijoS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de ActivoFijoS</param>
        public void EliminarActivoFijo(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        ActivoFijo pActivoFijo = new ActivoFijo();

                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = Convert.ToInt32(pId);
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_ACT_ActivoFijo_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConceptoData", "EliminarConcepto", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla ActivoFijoS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla ActivoFijoS</param>
        /// <returns>Entidad ActivoFijo consultado</returns>
        public ActivoFijo ConsultarActivoFijo(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            ActivoFijo entidad = new ActivoFijo();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT Activos_Fijos.*,(select primer_nombre||' '||segundo_nombre||' '||primer_apellido||' '||segundo_apellido from persona where cod_persona = Activos_fijos.cod_proveedor) as NomProveedor, Clase_Activo.nombre As NomClase, Tipo_Activo.nombre As NomTipo, Ubicacion_Activo.nombre As NomUbica, Centro_Costo.nom_centro As NomCosto,
                                            Estado_Activo.nombre As NomEstado, Oficina.nombre As NomOficina, 
                                            persona.primer_nombre ||' '|| persona.segundo_nombre ||' '|| persona.primer_apellido ||' '|| persona.segundo_apellido as NomEncargado
                                            FROM Activos_Fijos 
                                            LEFT JOIN Clase_Activo On Activos_Fijos.clase = Clase_Activo.clase
                                            LEFT JOIN Tipo_Activo On Activos_Fijos.tipo = Tipo_Activo.tipo 
                                            LEFT JOIN Ubicacion_Activo On Activos_Fijos.cod_ubica = Ubicacion_Activo.cod_ubica
                                            LEFT JOIN Centro_Costo On Activos_Fijos.cod_costo = Centro_Costo.centro_costo
                                            LEFT JOIN Estado_Activo On Activos_Fijos.estado = Estado_Activo.estado
                                            LEFT JOIN Oficina On Activos_Fijos.cod_oficina = Oficina.cod_oficina
                                            LEFT JOIN persona On activos_Fijos.cod_encargado = persona.cod_persona
                                            WHERE CONSECUTIVO = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["COD_ACT"] != DBNull.Value) entidad.cod_act = Convert.ToInt32(resultado["COD_ACT"]);
                            if (resultado["CLASE"] != DBNull.Value) entidad.clase = Convert.ToInt32(resultado["CLASE"]);
                            if (resultado["NOMCLASE"] != DBNull.Value) entidad.nomclase = Convert.ToString(resultado["NOMCLASE"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToInt32(resultado["TIPO"]);
                            if (resultado["NOMTIPO"] != DBNull.Value) entidad.nomtipo = Convert.ToString(resultado["NOMTIPO"]);
                            if (resultado["COD_UBICA"] != DBNull.Value) entidad.cod_ubica = Convert.ToInt32(resultado["COD_UBICA"]);
                            if (resultado["NOMUBICA"] != DBNull.Value) entidad.nomubica = Convert.ToString(resultado["NOMUBICA"]);
                            if (resultado["COD_COSTO"] != DBNull.Value) entidad.cod_costo = Convert.ToInt32(resultado["COD_COSTO"]);
                            if (resultado["NOMCOSTO"] != DBNull.Value) entidad.nomcosto = Convert.ToString(resultado["NOMCOSTO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["ANOS_UTIL"] != DBNull.Value) entidad.anos_util = Convert.ToDouble(resultado["ANOS_UTIL"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["NOMESTADO"] != DBNull.Value) entidad.nomestado = Convert.ToString(resultado["NOMESTADO"]);
                            if (resultado["SERIAL"] != DBNull.Value) entidad.serial = Convert.ToString(resultado["SERIAL"]);
                            if (resultado["COD_ENCARGADO"] != DBNull.Value) entidad.cod_encargado = Convert.ToInt64(resultado["COD_ENCARGADO"]);
                            if (resultado["NOMENCARGADO"] != DBNull.Value) entidad.nom_encargado = Convert.ToString(resultado["NOMENCARGADO"]);
                            if (resultado["FECHA_COMPRA"] != DBNull.Value) entidad.fecha_compra = Convert.ToDateTime(resultado["FECHA_COMPRA"]);
                            if (resultado["VALOR_COMPRA"] != DBNull.Value) entidad.valor_compra = Convert.ToDecimal(resultado["VALOR_COMPRA"]);
                            if (resultado["VALOR_AVALUO"] != DBNull.Value) entidad.valor_avaluo = Convert.ToDecimal(resultado["VALOR_AVALUO"]);
                            if (resultado["VALOR_SALVAMEN"] != DBNull.Value) entidad.valor_salvamen = Convert.ToDecimal(resultado["VALOR_SALVAMEN"]);
                            if (resultado["NUM_FACTURA"] != DBNull.Value) entidad.num_factura = Convert.ToDecimal(resultado["NUM_FACTURA"]);
                            if (resultado["COD_PROVEEDOR"] != DBNull.Value) entidad.cod_proveedor = Convert.ToInt64(resultado["COD_PROVEEDOR"]);
                            if (resultado["NOMPROVEEDOR"] != DBNull.Value) entidad.nom_proveedor = Convert.ToString(resultado["NOMPROVEEDOR"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["COD_OFICINA"]);
                            if (resultado["NOMOFICINA"] != DBNull.Value) entidad.nomoficina = Convert.ToString(resultado["NOMOFICINA"]);
                            if (resultado["FECHA_ULT_DEPRE"] != DBNull.Value) entidad.fecha_ult_depre = Convert.ToDateTime(resultado["FECHA_ULT_DEPRE"]);
                            if (resultado["ACUMULADO_DEPRECIACION"] != DBNull.Value) entidad.acumulado_depreciacion = Convert.ToDecimal(resultado["ACUMULADO_DEPRECIACION"]);
                            if (resultado["SALDO_POR_DEPRECIAR"] != DBNull.Value) entidad.saldo_por_depreciar = Convert.ToDecimal(resultado["SALDO_POR_DEPRECIAR"]);
                            if (resultado["FECHACREACION"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["FECHACREACION"]);
                            if (resultado["USUARIOCREACION"] != DBNull.Value) entidad.usuariocreacion = Convert.ToString(resultado["USUARIOCREACION"]);
                            if (resultado["FECULTMOD"] != DBNull.Value) entidad.fecultmod = Convert.ToDateTime(resultado["FECULTMOD"]);
                            if (resultado["USUULTMOD"] != DBNull.Value) entidad.usuultmod = Convert.ToString(resultado["USUULTMOD"]);
                            if (resultado["IMAGEN"] != DBNull.Value) entidad.imagen = Convert.ToString(resultado["IMAGEN"]);

                            //AGREGADO
                            if (resultado["CODCLASIFICACION_NIF"] != DBNull.Value) entidad.codclasificacion_nif = Convert.ToInt32(resultado["CODCLASIFICACION_NIF"]);
                            if (resultado["TIPO_ACTIVO_NIF"] != DBNull.Value) entidad.tipo_activo_nif = Convert.ToInt32(resultado["TIPO_ACTIVO_NIF"]);
                            if (resultado["METODO_COSTEO_NIF"] != DBNull.Value) entidad.metodo_costeo_nif = Convert.ToInt32(resultado["METODO_COSTEO_NIF"]);
                            if (resultado["VALOR_ACTIVO_NIF"] != DBNull.Value) entidad.valor_activo_nif = Convert.ToDecimal(resultado["VALOR_ACTIVO_NIF"]);
                            if (resultado["VIDA_UTIL_NIF"] != DBNull.Value) entidad.vida_util_nif = Convert.ToDecimal(resultado["VIDA_UTIL_NIF"]);
                            if (resultado["VALOR_RESIDUAL_NIF"] != DBNull.Value) entidad.valor_residual_nif = Convert.ToDecimal(resultado["VALOR_RESIDUAL_NIF"]);
                            if (resultado["PORCENTAJE_RESIDUAL_NIF"] != DBNull.Value) entidad.porcentaje_residual_nif = Convert.ToDecimal(resultado["PORCENTAJE_RESIDUAL_NIF"]);
                            if (resultado["UNIGENERADORA_NIF"] != DBNull.Value) entidad.unigeneradora_nif = Convert.ToInt32(resultado["UNIGENERADORA_NIF"]);
                            if (resultado["ADICIONES_NIF"] != DBNull.Value) entidad.adiciones_nif = Convert.ToDecimal(resultado["ADICIONES_NIF"]);
                            if (resultado["VRDETERIORO_NIF"] != DBNull.Value) entidad.vrdeterioro_nif = Convert.ToDecimal(resultado["VRDETERIORO_NIF"]);
                            if (resultado["VRRECDETERIORO_NIF"] != DBNull.Value) entidad.vrrecdeterioro_nif = Convert.ToDecimal(resultado["VRRECDETERIORO_NIF"]);
                            if (resultado["REVALUACION_NIF"] != DBNull.Value) entidad.revaluacion_nif = Convert.ToDecimal(resultado["REVALUACION_NIF"]);
                            if (resultado["REVREVALUACION_NIF"] != DBNull.Value) entidad.revrevaluacion_nif = Convert.ToDecimal(resultado["REVREVALUACION_NIF"]);
                            if (resultado["COD_MONEDA"] != DBNull.Value) entidad.cod_moneda = Convert.ToInt32(resultado["COD_MONEDA"]);
                            if (resultado["FECHA_ULT_ADICION"] != DBNull.Value) entidad.fecha_ult_adicion = Convert.ToDateTime(resultado["FECHA_ULT_ADICION"]);
                            if (resultado["USO_DEL_BIEN"] != DBNull.Value) entidad.uso_del_bien = Convert.ToInt32(resultado["USO_DEL_BIEN"]);
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
                        BOExcepcion.Throw("ActivoFijoData", "ConsultarActivoFijo", ex);
                        return null;
                    }
                }
            }
        }

        public List<ActivoFijo> ListarTipoActivoFijo(Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ActivoFijo> lstActivoFijo = new List<ActivoFijo>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Tipo_Activo_per";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ActivoFijo entidad = new ActivoFijo();
                            if (resultado["COD_TIPO_ACTIVO_PER"] != DBNull.Value) entidad.cod_act = Convert.ToInt64(resultado["COD_TIPO_ACTIVO_PER"]);
                            if (resultado["CLASE"] != DBNull.Value) entidad.str_clase = Convert.ToString(resultado["CLASE"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.nomclase = Convert.ToString(resultado["DESCRIPCION"]);
                            lstActivoFijo.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstActivoFijo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActivoFijoData", "ListarTipoActivoFijo", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla ActivoFijoS dados unos filtros
        /// </summary>
        /// <param name="pActivoFijoS">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ActivoFijo obtenidos</returns>
        public List<ActivoFijo> ListarActivoFijo(ActivoFijo pActivoFijo, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ActivoFijo> lstActivoFijo = new List<ActivoFijo>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT Activos_Fijos.*, Clase_Activo.nombre As NomClase, Tipo_Activo.nombre As NomTipo, Ubicacion_Activo.nombre As NomUbica, Centro_Costo.nom_centro As NomCosto,
                                            Estado_Activo.nombre As NomEstado, Oficina.nombre As NomOficina , tipo_activo_nif.descripcion As NomTipoNif,
                                             Case Activos_Fijos.metodo_costeo_nif When 1 Then 'Modelo de Costo' When 2 Then 'Modelo de Revaluación' End As NomMetodo
                                            FROM Activos_Fijos 
                                            LEFT JOIN Clase_Activo On Activos_Fijos.clase = Clase_Activo.clase
                                            LEFT JOIN Tipo_Activo On Activos_Fijos.tipo = Tipo_Activo.tipo 
                                            LEFT JOIN Ubicacion_Activo On Activos_Fijos.cod_ubica = Ubicacion_Activo.cod_ubica
                                            LEFT JOIN Centro_Costo On Activos_Fijos.cod_costo = Centro_Costo.centro_costo
                                            LEFT JOIN Estado_Activo On Activos_Fijos.estado = Estado_Activo.estado
                                            LEFT JOIN Oficina On Activos_Fijos.cod_oficina = Oficina.cod_oficina
                                            LEFT JOIN tipo_activo_nif on Activos_Fijos.tipo_activo_nif = tipo_activo_nif.tipo_activo_nif       
                                      " + ObtenerFiltro(pActivoFijo, "Activos_Fijos.") + " ORDER BY COD_ACT";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ActivoFijo entidad = new ActivoFijo();
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["COD_ACT"] != DBNull.Value) entidad.cod_act = Convert.ToInt32(resultado["COD_ACT"]);
                            if (resultado["CLASE"] != DBNull.Value) entidad.clase = Convert.ToInt32(resultado["CLASE"]);
                            if (resultado["NOMCLASE"] != DBNull.Value) entidad.nomclase = Convert.ToString(resultado["NOMCLASE"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToInt32(resultado["TIPO"]);
                            if (resultado["NOMTIPO"] != DBNull.Value) entidad.nomtipo = Convert.ToString(resultado["NOMTIPO"]);
                            if (resultado["COD_UBICA"] != DBNull.Value) entidad.cod_ubica = Convert.ToInt32(resultado["COD_UBICA"]);
                            if (resultado["NOMUBICA"] != DBNull.Value) entidad.nomubica = Convert.ToString(resultado["NOMUBICA"]);
                            if (resultado["COD_COSTO"] != DBNull.Value) entidad.cod_costo = Convert.ToInt32(resultado["COD_COSTO"]);
                            if (resultado["NOMCOSTO"] != DBNull.Value) entidad.nomcosto = Convert.ToString(resultado["NOMCOSTO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["ANOS_UTIL"] != DBNull.Value) entidad.anos_util = Convert.ToInt32(resultado["ANOS_UTIL"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["NOMESTADO"] != DBNull.Value) entidad.nomestado = Convert.ToString(resultado["NOMESTADO"]);
                            if (resultado["SERIAL"] != DBNull.Value) entidad.serial = Convert.ToString(resultado["SERIAL"]);
                            if (resultado["COD_ENCARGADO"] != DBNull.Value) entidad.cod_encargado = Convert.ToInt64(resultado["COD_ENCARGADO"]);
                            if (resultado["FECHA_COMPRA"] != DBNull.Value) entidad.fecha_compra = Convert.ToDateTime(resultado["FECHA_COMPRA"]);
                            if (resultado["VALOR_COMPRA"] != DBNull.Value) entidad.valor_compra = Convert.ToDecimal(resultado["VALOR_COMPRA"]);
                            if (resultado["VALOR_AVALUO"] != DBNull.Value) entidad.valor_avaluo = Convert.ToDecimal(resultado["VALOR_AVALUO"]);
                            if (resultado["VALOR_SALVAMEN"] != DBNull.Value) entidad.valor_salvamen = Convert.ToDecimal(resultado["VALOR_SALVAMEN"]);
                            if (resultado["NUM_FACTURA"] != DBNull.Value) entidad.num_factura = Convert.ToDecimal(resultado["NUM_FACTURA"]);
                            if (resultado["COD_PROVEEDOR"] != DBNull.Value) entidad.cod_proveedor = Convert.ToInt64(resultado["COD_PROVEEDOR"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["COD_OFICINA"]);
                            if (resultado["NOMOFICINA"] != DBNull.Value) entidad.nomoficina = Convert.ToString(resultado["NOMOFICINA"]);
                            if (resultado["FECHA_ULT_DEPRE"] != DBNull.Value) entidad.fecha_ult_depre = Convert.ToDateTime(resultado["FECHA_ULT_DEPRE"]);
                            if (resultado["ACUMULADO_DEPRECIACION"] != DBNull.Value) entidad.acumulado_depreciacion = Convert.ToDecimal(resultado["ACUMULADO_DEPRECIACION"]);
                            if (resultado["SALDO_POR_DEPRECIAR"] != DBNull.Value) entidad.saldo_por_depreciar = Convert.ToDecimal(resultado["SALDO_POR_DEPRECIAR"]);
                            if (resultado["FECHACREACION"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["FECHACREACION"]);
                            if (resultado["USUARIOCREACION"] != DBNull.Value) entidad.usuariocreacion = Convert.ToString(resultado["USUARIOCREACION"]);
                            if (resultado["FECULTMOD"] != DBNull.Value) entidad.fecultmod = Convert.ToDateTime(resultado["FECULTMOD"]);
                            if (resultado["USUULTMOD"] != DBNull.Value) entidad.usuultmod = Convert.ToString(resultado["USUULTMOD"]);
                            if (resultado["IMAGEN"] != DBNull.Value) entidad.imagen = Convert.ToString(resultado["IMAGEN"]);
                            if (resultado["NOMTIPONIF"] != DBNull.Value) entidad.nomTipoNif = Convert.ToString(resultado["NOMTIPONIF"]);
                            if (resultado["NOMMETODO"] != DBNull.Value) entidad.nomMetodo = Convert.ToString(resultado["NOMMETODO"]);
                            if (resultado["VIDA_UTIL_NIF"] != DBNull.Value) entidad.vida_util_nif = Convert.ToDecimal(resultado["VIDA_UTIL_NIF"]);
                            lstActivoFijo.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstActivoFijo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActivoFijoData", "ListarActivoFijo", ex);
                        return null;
                    }
                }
            }
        }

        public List<ActivoFijo> ListarActivoFijoDepre(DateTime pFechaDepreciacion, ActivoFijo pActivoFijo, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ActivoFijo> lstActivoFijo = new List<ActivoFijo>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string filtro = ObtenerFiltro(pActivoFijo);
                        string sql = @"SELECT Activos_Fijos.*, Clase_Activo.nombre As NomClase, Tipo_Activo.nombre As NomTipo, Ubicacion_Activo.nombre As NomUbica, Centro_Costo.nom_centro As NomCosto,
                                            Estado_Activo.nombre As NomEstado, Oficina.nombre As NomOficina, Tipo_Activo.cod_cuenta_depreciacion, pl.nombre As nomcuenta_depreciacion, 
                                            Tipo_Activo.cod_cuenta_depreciacion_gasto, pg.nombre As nomcuenta_depreciacion_gasto, 
                                            Depreciacion(To_Date('" + pFechaDepreciacion.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + @"'), Activos_Fijos.cod_act) As valor_a_depreciar,
                                            DiasADepreciar(To_Date('" + pFechaDepreciacion.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + @"'), Activos_Fijos.cod_act) As dias_a_depreciar
                                            FROM Activos_Fijos 
                                            LEFT JOIN Clase_Activo On Activos_Fijos.clase = Clase_Activo.clase
                                            LEFT JOIN Tipo_Activo On Activos_Fijos.tipo = Tipo_Activo.tipo 
                                            LEFT JOIN Plan_Cuentas pl On Tipo_Activo.cod_cuenta_depreciacion = pl.cod_cuenta
                                            LEFT JOIN Plan_Cuentas pg On Tipo_Activo.cod_cuenta_depreciacion_gasto = pg.cod_cuenta
                                            LEFT JOIN Ubicacion_Activo On Activos_Fijos.cod_ubica = Ubicacion_Activo.cod_ubica
                                            LEFT JOIN Centro_Costo On Activos_Fijos.cod_costo = Centro_Costo.centro_costo
                                            LEFT JOIN Estado_Activo On Activos_Fijos.estado = Estado_Activo.estado
                                            LEFT JOIN Oficina On Activos_Fijos.cod_oficina = Oficina.cod_oficina
                                            WHERE Activos_Fijos.estado = 1
                                             ORDER BY CONSECUTIVO ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ActivoFijo entidad = new ActivoFijo();
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["COD_ACT"] != DBNull.Value) entidad.cod_act = Convert.ToInt32(resultado["COD_ACT"]);
                            if (resultado["CLASE"] != DBNull.Value) entidad.clase = Convert.ToInt32(resultado["CLASE"]);
                            if (resultado["NOMCLASE"] != DBNull.Value) entidad.nomclase = Convert.ToString(resultado["NOMCLASE"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToInt32(resultado["TIPO"]);
                            if (resultado["NOMTIPO"] != DBNull.Value) entidad.nomtipo = Convert.ToString(resultado["NOMTIPO"]);
                            if (resultado["COD_UBICA"] != DBNull.Value) entidad.cod_ubica = Convert.ToInt32(resultado["COD_UBICA"]);
                            if (resultado["NOMUBICA"] != DBNull.Value) entidad.nomubica = Convert.ToString(resultado["NOMUBICA"]);
                            if (resultado["COD_COSTO"] != DBNull.Value) entidad.cod_costo = Convert.ToInt32(resultado["COD_COSTO"]);
                            if (resultado["NOMCOSTO"] != DBNull.Value) entidad.nomcosto = Convert.ToString(resultado["NOMCOSTO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["ANOS_UTIL"] != DBNull.Value) entidad.anos_util = Convert.ToInt32(resultado["ANOS_UTIL"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["NOMESTADO"] != DBNull.Value) entidad.nomestado = Convert.ToString(resultado["NOMESTADO"]);
                            if (resultado["SERIAL"] != DBNull.Value) entidad.serial = Convert.ToString(resultado["SERIAL"]);
                            if (resultado["COD_ENCARGADO"] != DBNull.Value) entidad.cod_encargado = Convert.ToInt64(resultado["COD_ENCARGADO"]);
                            if (resultado["FECHA_COMPRA"] != DBNull.Value) entidad.fecha_compra = Convert.ToDateTime(resultado["FECHA_COMPRA"]);
                            if (resultado["VALOR_COMPRA"] != DBNull.Value) entidad.valor_compra = Convert.ToDecimal(resultado["VALOR_COMPRA"]);
                            if (resultado["VALOR_AVALUO"] != DBNull.Value) entidad.valor_avaluo = Convert.ToDecimal(resultado["VALOR_AVALUO"]);
                            if (resultado["VALOR_SALVAMEN"] != DBNull.Value) entidad.valor_salvamen = Convert.ToDecimal(resultado["VALOR_SALVAMEN"]);
                            if (resultado["NUM_FACTURA"] != DBNull.Value) entidad.num_factura = Convert.ToDecimal(resultado["NUM_FACTURA"]);
                            if (resultado["COD_PROVEEDOR"] != DBNull.Value) entidad.cod_proveedor = Convert.ToInt64(resultado["COD_PROVEEDOR"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["COD_OFICINA"]);
                            if (resultado["NOMOFICINA"] != DBNull.Value) entidad.nomoficina = Convert.ToString(resultado["NOMOFICINA"]);
                            if (resultado["FECHA_ULT_DEPRE"] != DBNull.Value) entidad.fecha_ult_depre = Convert.ToDateTime(resultado["FECHA_ULT_DEPRE"]);
                            if (resultado["ACUMULADO_DEPRECIACION"] != DBNull.Value) entidad.acumulado_depreciacion = Convert.ToDecimal(resultado["ACUMULADO_DEPRECIACION"]);
                            if (resultado["SALDO_POR_DEPRECIAR"] != DBNull.Value) entidad.saldo_por_depreciar = Convert.ToDecimal(resultado["SALDO_POR_DEPRECIAR"]);
                            if (resultado["FECHACREACION"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["FECHACREACION"]);
                            if (resultado["USUARIOCREACION"] != DBNull.Value) entidad.usuariocreacion = Convert.ToString(resultado["USUARIOCREACION"]);
                            if (resultado["FECULTMOD"] != DBNull.Value) entidad.fecultmod = Convert.ToDateTime(resultado["FECULTMOD"]);
                            if (resultado["USUULTMOD"] != DBNull.Value) entidad.usuultmod = Convert.ToString(resultado["USUULTMOD"]);
                            if (resultado["IMAGEN"] != DBNull.Value) entidad.imagen = Convert.ToString(resultado["IMAGEN"]);
                            if (resultado["COD_CUENTA_DEPRECIACION"] != DBNull.Value) entidad.cod_cuenta_depreciacion = Convert.ToString(resultado["COD_CUENTA_DEPRECIACION"]);
                            if (resultado["NOMCUENTA_DEPRECIACION"] != DBNull.Value) entidad.nomcuenta_depreciacion = Convert.ToString(resultado["NOMCUENTA_DEPRECIACION"]);
                            if (resultado["COD_CUENTA_DEPRECIACION_GASTO"] != DBNull.Value) entidad.cod_cuenta_depreciacion_gasto = Convert.ToString(resultado["COD_CUENTA_DEPRECIACION_GASTO"]);
                            if (resultado["NOMCUENTA_DEPRECIACION_GASTO"] != DBNull.Value) entidad.nomcuenta_depreciacion_gasto = Convert.ToString(resultado["NOMCUENTA_DEPRECIACION_GASTO"]);
                            if (resultado["VALOR_A_DEPRECIAR"] != DBNull.Value) entidad.valor_a_depreciar = Convert.ToDecimal(resultado["VALOR_A_DEPRECIAR"]);
                            if (resultado["DIAS_A_DEPRECIAR"] != DBNull.Value) entidad.dias_a_depreciar = Convert.ToInt64(resultado["DIAS_A_DEPRECIAR"]);
                            lstActivoFijo.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstActivoFijo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActivoFijoData", "ListarActivoFijoDepre", ex);
                        return null;
                    }
                }
            }
        }


        public List<ActivoFijo> ListarActivoDeterioroNif(DateTime pFecha, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ActivoFijo> lstActivoFijo = new List<ActivoFijo>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        
                        string sql = @"SELECT Activos_Fijos.*, tipo_activo_nif.descripcion As NomTipo, Ubicacion_Activo.nombre As NomUbica, Centro_Costo.nom_centro As NomCosto,
                                            Estado_Activo.nombre As NomEstado, Oficina.nombre As NomOficina, tipo_activo_nif.cod_cuenta_deterioro, pl.nombre As nomcuenta_depreciacion, 
                                            tipo_activo_nif.cod_cuenta_gastodet, pg.nombre As nomcuenta_depreciacion_gasto , 
                                            clasificacion_activo_nif.descripcion As NomClasificacion,
                                            tipomoneda.descripcion As NomMoneda,
                                            Case Activos_Fijos.metodo_costeo_nif When 1 Then 'Costo' When 2 Then 'Revaluación' End As NomMetodo,
                                            (SELECT max(x.FECHA) FROM DETERIORO_ACTIVOS x where x.CODIGO_ACT = Activos_Fijos.COD_ACT) as FechaUltDeterioro
                                            FROM Activos_Fijos 
                                            LEFT JOIN tipo_activo_nif On activos_fijos.tipo_activo_nif = tipo_activo_nif.tipo_activo_nif
                                            LEFT JOIN Plan_Cuentas pl On tipo_activo_nif.cod_cuenta_deterioro = pl.cod_cuenta
                                            LEFT JOIN Plan_Cuentas pg On tipo_activo_nif.cod_cuenta_gastodet = pg.cod_cuenta
                                            LEFT JOIN Ubicacion_Activo On Activos_Fijos.cod_ubica = Ubicacion_Activo.cod_ubica
                                            LEFT JOIN Centro_Costo On Activos_Fijos.cod_costo = Centro_Costo.centro_costo
                                            LEFT JOIN Estado_Activo On Activos_Fijos.estado = Estado_Activo.estado
                                            LEFT JOIN Oficina On Activos_Fijos.cod_oficina = Oficina.cod_oficina
                                            LEFT JOIN clasificacion_activo_nif on Activos_fijos.codclasificacion_nif = clasificacion_activo_nif.codclasificacion_nif            
                                            LEFT JOIN tipomoneda on activos_fijos.cod_moneda = tipomoneda.cod_moneda
                                            WHERE trunc(Activos_Fijos.FECHA_COMPRA) <= To_Date('" + pFecha.ToString(conf.ObtenerFormatoFecha()) + @"', 'dd/MM/yyyy')
                                            AND Activos_Fijos.estado = 1";
                                                                        
                        if (pFecha != null && pFecha != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " And ";
                            else
                                sql += " Where ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " FECHA_COMPRA <= To_Date('" + Convert.ToDateTime(pFecha).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " FECHA_COMPRA <= '" + Convert.ToDateTime(pFecha).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }

                        sql += " ORDER BY CONSECUTIVO";


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ActivoFijo entidad = new ActivoFijo();
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["COD_ACT"] != DBNull.Value) entidad.cod_act = Convert.ToInt64(resultado["COD_ACT"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["NOMOFICINA"] != DBNull.Value) entidad.nomoficina = Convert.ToString(resultado["NOMOFICINA"]);
                            if (resultado["FECHA_COMPRA"] != DBNull.Value) entidad.fecha_compra = Convert.ToDateTime(resultado["FECHA_COMPRA"]);
                            if (resultado["VALOR_COMPRA"] != DBNull.Value) entidad.valor_compra = Convert.ToDecimal(resultado["VALOR_COMPRA"]);
                            if (resultado["NOMMONEDA"] != DBNull.Value) entidad.nomMoneda = Convert.ToString(resultado["NOMMONEDA"]);
                            if (resultado["COD_CUENTA_DETERIORO"] != DBNull.Value) entidad.cod_cuenta_depreciacion = Convert.ToString(resultado["COD_CUENTA_DETERIORO"]);
                            if (resultado["NOMCLASIFICACION"] != DBNull.Value) entidad.nomClasificacion = Convert.ToString(resultado["NOMCLASIFICACION"]);
                            if (resultado["NOMTIPO"] != DBNull.Value) entidad.nomtipo = Convert.ToString(resultado["NOMTIPO"]);
                            if (resultado["NOMMETODO"] != DBNull.Value) entidad.nomMetodo = Convert.ToString(resultado["NOMMETODO"]);
                            if (resultado["UNIGENERADORA_NIF"] != DBNull.Value) entidad.unigeneradora_nif = Convert.ToInt32(resultado["UNIGENERADORA_NIF"]);
                            if (resultado["VALOR_COMPRA"] != DBNull.Value) entidad.valor_compra = Convert.ToDecimal(resultado["VALOR_COMPRA"]); else entidad.valor_compra = 0;
                            if (resultado["ACUMULADO_DEPRECIACION"] != DBNull.Value) entidad.acumulado_depreciacion = Convert.ToDecimal(resultado["ACUMULADO_DEPRECIACION"]); else entidad.acumulado_depreciacion = 0; 
                            if (resultado["VRDETERIORO_NIF"] != DBNull.Value) entidad.vrdeterioro_nif = Convert.ToDecimal(resultado["VRDETERIORO_NIF"]); else entidad.vrdeterioro_nif = 0 ;
                            if (resultado["VRRECDETERIORO_NIF"] != DBNull.Value) entidad.vrrecdeterioro_nif = Convert.ToDecimal(resultado["VRRECDETERIORO_NIF"]);

                            if (resultado["VALOR_ACTIVO_NIF"] != DBNull.Value) entidad.valor_activo_nif = Convert.ToDecimal(resultado["VALOR_ACTIVO_NIF"]);
                            if (resultado["VALOR_RESIDUAL_NIF"] != DBNull.Value) entidad.valor_residual_nif = Convert.ToDecimal(resultado["VALOR_RESIDUAL_NIF"]);
                            if (resultado["VIDA_UTIL_NIF"] != DBNull.Value) entidad.vida_util_nif = Convert.ToDecimal(resultado["VIDA_UTIL_NIF"]);
                            if (resultado["FechaUltDeterioro"] != DBNull.Value) entidad.fecha_ult_depre = Convert.ToDateTime(resultado["FechaUltDeterioro"]);
                            if (resultado["ADICIONES_NIF"] != DBNull.Value) entidad.adiciones_nif = Convert.ToDecimal(resultado["ADICIONES_NIF"]);
                            //Calculando valor deterioro
                            int meses = 0;
                            decimal vr_activo = 0, vr_residual = 0, vida_util = 0, total = 0, totalValida = 0;
                            DateTime pFecUltDeterioro;
                            vr_activo = entidad.valor_activo_nif;
                            vr_residual = entidad.valor_residual_nif;

                            //entidad.valoractual = (entidad.valor_compra - entidad.acumulado_depreciacion - entidad.vrdeterioro_nif);
                            entidad.valoractual = (vr_activo - entidad.vrdeterioro_nif);
                            totalValida = ((vr_activo + entidad.adiciones_nif) - entidad.vrdeterioro_nif);

                            if (entidad.vida_util_nif != 0)
                            {
                                vida_util = entidad.vida_util_nif;
                                vida_util = (vida_util * 12);
                            }

                            if (vida_util == 0)
                                total = 0;
                            else
                            {
                                total = (((vr_activo + entidad.adiciones_nif) - vr_residual) / vida_util) * 1;
                                //total = (vr_activo - vr_residual) / vida_util;
                            }
                            total = Math.Round(total, 2);
                            //calculando los dias de deterioro
                            if (entidad.fecha_ult_depre != DateTime.MinValue)
                            {
                                entidad.fecha_ult_deterioro = entidad.fecha_ult_depre; 
                                pFecUltDeterioro = entidad.fecha_ult_depre;
                                if (entidad.fecha_ult_depre > pFecha)
                                    meses = 0;
                                else
                                {
                                    meses = CalcularMesesDeDiferencia(pFecUltDeterioro,pFecha);
                                }
                            }

                            if (meses > 0)
                                total = total * meses;

                            entidad.valor_deterioro = totalValida < total ? entidad.valoractual : Convert.ToDecimal(total);

                            lstActivoFijo.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstActivoFijo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActivoFijoData", "ListarActivoDeterioroNif", ex);
                        return null;
                    }
                }
            }
        }

        public int CalcularMesesDeDiferencia(DateTime fechaDesde, DateTime fechaHasta)
        {
            return Math.Abs((fechaHasta.Month - fechaDesde.Month) + 12 * (fechaHasta.Year - fechaDesde.Year));
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
                        string sql = "SELECT MAX(cod_act) + 1 FROM ACTIVOS_FIJOS ";

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

        public ActivoFijo CrearDatosAdicionales(ActivoFijo pDatos_Adicionales, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pDatos_Adicionales.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcod_act = cmdTransaccionFactory.CreateParameter();
                        pcod_act.ParameterName = "p_cod_act";
                        pcod_act.Value = pDatos_Adicionales.cod_act;
                        pcod_act.Direction = ParameterDirection.Input;
                        pcod_act.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_act);

                        DbParameter ptipo = cmdTransaccionFactory.CreateParameter();
                        ptipo.ParameterName = "p_tipo";
                        if (pDatos_Adicionales.tipo == null)
                            ptipo.Value = DBNull.Value;
                        else
                            ptipo.Value = pDatos_Adicionales.tipo;
                        ptipo.Direction = ParameterDirection.Input;
                        ptipo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo);

                        DbParameter pmarca = cmdTransaccionFactory.CreateParameter();
                        pmarca.ParameterName = "p_marca";
                        if (pDatos_Adicionales.marca == null)
                            pmarca.Value = DBNull.Value;
                        else
                            pmarca.Value = pDatos_Adicionales.marca;
                        pmarca.Direction = ParameterDirection.Input;
                        pmarca.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pmarca);

                        DbParameter pmodelo = cmdTransaccionFactory.CreateParameter();
                        pmodelo.ParameterName = "p_modelo";
                        if (pDatos_Adicionales.modelo == null)
                            pmodelo.Value = DBNull.Value;
                        else
                            pmodelo.Value = pDatos_Adicionales.modelo;
                        pmodelo.Direction = ParameterDirection.Input;
                        pmodelo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pmodelo);

                        DbParameter pmatricula = cmdTransaccionFactory.CreateParameter();
                        pmatricula.ParameterName = "p_matricula";
                        if (pDatos_Adicionales.matricula == null)
                            pmatricula.Value = DBNull.Value;
                        else
                            pmatricula.Value = pDatos_Adicionales.matricula;
                        pmatricula.Direction = ParameterDirection.Input;
                        pmatricula.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pmatricula);

                        DbParameter pnotaria = cmdTransaccionFactory.CreateParameter();
                        pnotaria.ParameterName = "p_notaria";
                        if (pDatos_Adicionales.notaria == null)
                            pnotaria.Value = DBNull.Value;
                        else
                            pnotaria.Value = pDatos_Adicionales.notaria;
                        pnotaria.Direction = ParameterDirection.Input;
                        pnotaria.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnotaria);

                        DbParameter pescritura = cmdTransaccionFactory.CreateParameter();
                        pescritura.ParameterName = "p_escritura";
                        if (pDatos_Adicionales.escritura == null)
                            pescritura.Value = DBNull.Value;
                        else
                            pescritura.Value = pDatos_Adicionales.escritura;
                        pescritura.Direction = ParameterDirection.Input;
                        pescritura.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pescritura);

                        DbParameter pplaca = cmdTransaccionFactory.CreateParameter();
                        pplaca.ParameterName = "p_placa";
                        if (pDatos_Adicionales.placa == null)
                            pplaca.Value = DBNull.Value;
                        else
                            pplaca.Value = pDatos_Adicionales.placa;
                        pplaca.Direction = ParameterDirection.Input;
                        pplaca.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pplaca);

                        DbParameter pnum_motor = cmdTransaccionFactory.CreateParameter();
                        pnum_motor.ParameterName = "p_num_motor";
                        if (pDatos_Adicionales.num_motor == null)
                            pnum_motor.Value = DBNull.Value;
                        else
                            pnum_motor.Value = pDatos_Adicionales.num_motor;
                        pnum_motor.Direction = ParameterDirection.Input;
                        pnum_motor.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnum_motor);

                        DbParameter pdireccion = cmdTransaccionFactory.CreateParameter();
                        pdireccion.ParameterName = "p_direccion";
                        if (pDatos_Adicionales.direccion == null)
                            pdireccion.Value = DBNull.Value;
                        else
                            pdireccion.Value = pDatos_Adicionales.direccion;
                        pdireccion.Direction = ParameterDirection.Input;
                        pdireccion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdireccion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_ACT_DATOSADI_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pDatos_Adicionales;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActivoFijoData", "CrearDatos_Adicionales", ex);
                        return null;
                    }
                }
            }
        }

        public ActivoFijo ModificarDatosAdicionales(ActivoFijo pDatos_Adicionales, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pDatos_Adicionales.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcod_act = cmdTransaccionFactory.CreateParameter();
                        pcod_act.ParameterName = "p_cod_act";
                        pcod_act.Value = pDatos_Adicionales.cod_act;
                        pcod_act.Direction = ParameterDirection.Input;
                        pcod_act.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_act);

                        DbParameter ptipo = cmdTransaccionFactory.CreateParameter();
                        ptipo.ParameterName = "p_tipo";
                        if (pDatos_Adicionales.tipo == null)
                            ptipo.Value = DBNull.Value;
                        else
                            ptipo.Value = pDatos_Adicionales.tipo;
                        ptipo.Direction = ParameterDirection.Input;
                        ptipo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo);

                        DbParameter pmarca = cmdTransaccionFactory.CreateParameter();
                        pmarca.ParameterName = "p_marca";
                        if (pDatos_Adicionales.marca == null)
                            pmarca.Value = DBNull.Value;
                        else
                            pmarca.Value = pDatos_Adicionales.marca;
                        pmarca.Direction = ParameterDirection.Input;
                        pmarca.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pmarca);

                        DbParameter pmodelo = cmdTransaccionFactory.CreateParameter();
                        pmodelo.ParameterName = "p_modelo";
                        if (pDatos_Adicionales.modelo == null)
                            pmodelo.Value = DBNull.Value;
                        else
                            pmodelo.Value = pDatos_Adicionales.modelo;
                        pmodelo.Direction = ParameterDirection.Input;
                        pmodelo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pmodelo);

                        DbParameter pmatricula = cmdTransaccionFactory.CreateParameter();
                        pmatricula.ParameterName = "p_matricula";
                        if (pDatos_Adicionales.matricula == null)
                            pmatricula.Value = DBNull.Value;
                        else
                            pmatricula.Value = pDatos_Adicionales.matricula;
                        pmatricula.Direction = ParameterDirection.Input;
                        pmatricula.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pmatricula);

                        DbParameter pnotaria = cmdTransaccionFactory.CreateParameter();
                        pnotaria.ParameterName = "p_notaria";
                        if (pDatos_Adicionales.notaria == null)
                            pnotaria.Value = DBNull.Value;
                        else
                            pnotaria.Value = pDatos_Adicionales.notaria;
                        pnotaria.Direction = ParameterDirection.Input;
                        pnotaria.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnotaria);

                        DbParameter pescritura = cmdTransaccionFactory.CreateParameter();
                        pescritura.ParameterName = "p_escritura";
                        if (pDatos_Adicionales.escritura == null)
                            pescritura.Value = DBNull.Value;
                        else
                            pescritura.Value = pDatos_Adicionales.escritura;
                        pescritura.Direction = ParameterDirection.Input;
                        pescritura.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pescritura);

                        DbParameter pplaca = cmdTransaccionFactory.CreateParameter();
                        pplaca.ParameterName = "p_placa";
                        if (pDatos_Adicionales.placa == null)
                            pplaca.Value = DBNull.Value;
                        else
                            pplaca.Value = pDatos_Adicionales.placa;
                        pplaca.Direction = ParameterDirection.Input;
                        pplaca.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pplaca);

                        DbParameter pnum_motor = cmdTransaccionFactory.CreateParameter();
                        pnum_motor.ParameterName = "p_num_motor";
                        if (pDatos_Adicionales.num_motor == null)
                            pnum_motor.Value = DBNull.Value;
                        else
                            pnum_motor.Value = pDatos_Adicionales.num_motor;
                        pnum_motor.Direction = ParameterDirection.Input;
                        pnum_motor.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnum_motor);

                        DbParameter pdireccion = cmdTransaccionFactory.CreateParameter();
                        pdireccion.ParameterName = "p_direccion";
                        if (pDatos_Adicionales.direccion == null)
                            pdireccion.Value = DBNull.Value;
                        else
                            pdireccion.Value = pDatos_Adicionales.direccion;
                        pdireccion.Direction = ParameterDirection.Input;
                        pdireccion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdireccion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_ACT_DATOSADI_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pDatos_Adicionales;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActivoFijoData", "CrearDatos_Adicionales", ex);
                        return null;
                    }
                }
            }
        }

        public ActivoFijo ConsultarDatosAdicionales(ActivoFijo entidad, Usuario vUsuario)
        {
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Datos_Adicionales WHERE CONSECUTIVO = " + entidad.consecutivo.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipos = Convert.ToInt32(resultado["TIPO"]);
                            if (resultado["MARCA"] != DBNull.Value) entidad.marca = Convert.ToString(resultado["MARCA"]);
                            if (resultado["MODELO"] != DBNull.Value) entidad.modelo = Convert.ToString(resultado["MODELO"]);
                            if (resultado["MATRICULA"] != DBNull.Value) entidad.matricula = Convert.ToString(resultado["MATRICULA"]);
                            if (resultado["NOTARIA"] != DBNull.Value) entidad.notaria = Convert.ToString(resultado["NOTARIA"]);
                            if (resultado["ESCRITURA"] != DBNull.Value) entidad.escritura = Convert.ToString(resultado["ESCRITURA"]);
                            if (resultado["PLACA"] != DBNull.Value) entidad.placa = Convert.ToString(resultado["PLACA"]);
                            if (resultado["NUM_MOTOR"] != DBNull.Value) entidad.num_motor = Convert.ToString(resultado["NUM_MOTOR"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            entidad.bDatosAdicionales = true;
                        }
                        else
                        {
                            entidad.bDatosAdicionales = false;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Datos_AdicionalesData", "ConsultarDatos_Adicionales", ex);
                        return null;
                    }
                }
            }
        }

        public ActivoFijo CrearPolizaActivo(ActivoFijo pActivoFijo, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pActivoFijo.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcod_act = cmdTransaccionFactory.CreateParameter();
                        pcod_act.ParameterName = "p_cod_act";
                        pcod_act.Value = pActivoFijo.cod_act;
                        pcod_act.Direction = ParameterDirection.Input;
                        pcod_act.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_act);

                        DbParameter pnum_poliza = cmdTransaccionFactory.CreateParameter();
                        pnum_poliza.ParameterName = "p_num_poliza";
                        if (pActivoFijo.num_poliza == null)
                            pnum_poliza.Value = DBNull.Value;
                        else
                            pnum_poliza.Value = pActivoFijo.num_poliza;
                        pnum_poliza.Direction = ParameterDirection.Input;
                        pnum_poliza.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnum_poliza);

                        DbParameter pasegurador = cmdTransaccionFactory.CreateParameter();
                        pasegurador.ParameterName = "p_asegurador";
                        if (pActivoFijo.asegurador == null)
                            pasegurador.Value = DBNull.Value;
                        else
                            pasegurador.Value = pActivoFijo.asegurador;
                        pasegurador.Direction = ParameterDirection.Input;
                        pasegurador.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pasegurador);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        if (pActivoFijo.valor == null)
                            pvalor.Value = DBNull.Value;
                        else
                            pvalor.Value = pActivoFijo.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter pfecha_poliza = cmdTransaccionFactory.CreateParameter();
                        pfecha_poliza.ParameterName = "p_fecha_poliza";
                        if (pActivoFijo.fecha_poliza == null)
                            pfecha_poliza.Value = DBNull.Value;
                        else
                            pfecha_poliza.Value = pActivoFijo.fecha_poliza;
                        pfecha_poliza.Direction = ParameterDirection.Input;
                        pfecha_poliza.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_poliza);

                        DbParameter pfecha_vigencia = cmdTransaccionFactory.CreateParameter();
                        pfecha_vigencia.ParameterName = "p_fecha_vigencia";
                        if (pActivoFijo.fecha_vigencia == null)
                            pfecha_vigencia.Value = DBNull.Value;
                        else
                            pfecha_vigencia.Value = pActivoFijo.fecha_vigencia;
                        pfecha_vigencia.Direction = ParameterDirection.Input;
                        pfecha_vigencia.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_vigencia);

                        DbParameter pfecha_garantia = cmdTransaccionFactory.CreateParameter();
                        pfecha_garantia.ParameterName = "p_fecha_garantia";
                        if (pActivoFijo.fecha_garantia == null)
                            pfecha_garantia.Value = DBNull.Value;
                        else
                            pfecha_garantia.Value = pActivoFijo.fecha_garantia;
                        pfecha_garantia.Direction = ParameterDirection.Input;
                        pfecha_garantia.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_garantia);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        if (pActivoFijo.estadop == null)
                            pestado.Value = DBNull.Value;
                        else
                            pestado.Value = pActivoFijo.estadop;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_ACT_POLIZAACT_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pActivoFijo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CrearPolizaActivo", "CrearActivoFijo", ex);
                        return null;
                    }
                }
            }
        }

        public ActivoFijo ModificarPolizaActivo(ActivoFijo pActivoFijo, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pActivoFijo.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcod_act = cmdTransaccionFactory.CreateParameter();
                        pcod_act.ParameterName = "p_cod_act";
                        pcod_act.Value = pActivoFijo.cod_act;
                        pcod_act.Direction = ParameterDirection.Input;
                        pcod_act.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_act);

                        DbParameter pnum_poliza = cmdTransaccionFactory.CreateParameter();
                        pnum_poliza.ParameterName = "p_num_poliza";
                        if (pActivoFijo.num_poliza == null)
                            pnum_poliza.Value = DBNull.Value;
                        else
                            pnum_poliza.Value = pActivoFijo.num_poliza;
                        pnum_poliza.Direction = ParameterDirection.Input;
                        pnum_poliza.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnum_poliza);

                        DbParameter pasegurador = cmdTransaccionFactory.CreateParameter();
                        pasegurador.ParameterName = "p_asegurador";
                        if (pActivoFijo.asegurador == null)
                            pasegurador.Value = DBNull.Value;
                        else
                            pasegurador.Value = pActivoFijo.asegurador;
                        pasegurador.Direction = ParameterDirection.Input;
                        pasegurador.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pasegurador);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        if (pActivoFijo.valor == null)
                            pvalor.Value = DBNull.Value;
                        else
                            pvalor.Value = pActivoFijo.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter pfecha_poliza = cmdTransaccionFactory.CreateParameter();
                        pfecha_poliza.ParameterName = "p_fecha_poliza";
                        if (pActivoFijo.fecha_poliza == null)
                            pfecha_poliza.Value = DBNull.Value;
                        else
                            pfecha_poliza.Value = pActivoFijo.fecha_poliza;
                        pfecha_poliza.Direction = ParameterDirection.Input;
                        pfecha_poliza.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_poliza);

                        DbParameter pfecha_vigencia = cmdTransaccionFactory.CreateParameter();
                        pfecha_vigencia.ParameterName = "p_fecha_vigencia";
                        if (pActivoFijo.fecha_vigencia == null)
                            pfecha_vigencia.Value = DBNull.Value;
                        else
                            pfecha_vigencia.Value = pActivoFijo.fecha_vigencia;
                        pfecha_vigencia.Direction = ParameterDirection.Input;
                        pfecha_vigencia.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_vigencia);

                        DbParameter pfecha_garantia = cmdTransaccionFactory.CreateParameter();
                        pfecha_garantia.ParameterName = "p_fecha_garantia";
                        if (pActivoFijo.fecha_garantia == null)
                            pfecha_garantia.Value = DBNull.Value;
                        else
                            pfecha_garantia.Value = pActivoFijo.fecha_garantia;
                        pfecha_garantia.Direction = ParameterDirection.Input;
                        pfecha_garantia.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_garantia);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        if (pActivoFijo.estadop == null)
                            pestado.Value = DBNull.Value;
                        else
                            pestado.Value = pActivoFijo.estadop;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_ACT_POLIZAACT_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pActivoFijo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ModificarPolizaActivo", "CrearActivoFijo", ex);
                        return null;
                    }
                }
            }
        }

        public ActivoFijo ConsultarPolizaActivo(ActivoFijo entidad, Usuario vUsuario)
        {
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM poliza_activo WHERE CONSECUTIVO = " + entidad.consecutivo.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["NUM_POLIZA"] != DBNull.Value) entidad.num_poliza = Convert.ToInt32(resultado["NUM_POLIZA"]);
                            if (resultado["ASEGURADOR"] != DBNull.Value) entidad.asegurador = Convert.ToString(resultado["ASEGURADOR"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToInt32(resultado["VALOR"]);
                            if (resultado["FECHA_POLIZA"] != DBNull.Value) entidad.fecha_poliza = Convert.ToDateTime(resultado["FECHA_POLIZA"]);
                            if (resultado["FECHA_VIGENCIA"] != DBNull.Value) entidad.fecha_vigencia = Convert.ToDateTime(resultado["FECHA_VIGENCIA"]);
                            if (resultado["FECHA_GARANTIA"] != DBNull.Value) entidad.fecha_garantia = Convert.ToDateTime(resultado["FECHA_GARANTIA"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estadop = Convert.ToInt32(resultado["ESTADO"]);
                            entidad.bPoliza = true;
                        }
                        else
                        {
                            entidad.bPoliza = false;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActivoFijoData", "ConsultarPolizaActivo", ex);
                        return null;
                    }
                }
            }
        }

        //AGREGADO

        public ActivoFijo CrearLEASING(ActivoFijo pActivoFijo, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pActivoFijo.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcodigo_act = cmdTransaccionFactory.CreateParameter();
                        pcodigo_act.ParameterName = "p_codigo_act";
                        pcodigo_act.Value = pActivoFijo.codigo_act;
                        pcodigo_act.Direction = ParameterDirection.Input;
                        pcodigo_act.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodigo_act);

                        DbParameter pnumero_cuotas = cmdTransaccionFactory.CreateParameter();
                        pnumero_cuotas.ParameterName = "p_numero_cuotas";
                        pnumero_cuotas.Value = pActivoFijo.numero_cuotas;
                        pnumero_cuotas.Direction = ParameterDirection.Input;
                        pnumero_cuotas.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_cuotas);

                        DbParameter pvalor_cuota = cmdTransaccionFactory.CreateParameter();
                        pvalor_cuota.ParameterName = "p_valor_cuota";
                        pvalor_cuota.Value = pActivoFijo.valor_cuota;
                        pvalor_cuota.Direction = ParameterDirection.Input;
                        pvalor_cuota.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_cuota);

                        DbParameter pcod_periodicidad = cmdTransaccionFactory.CreateParameter();
                        pcod_periodicidad.ParameterName = "p_cod_periodicidad";
                        pcod_periodicidad.Value = pActivoFijo.cod_periodicidad;
                        pcod_periodicidad.Direction = ParameterDirection.Input;
                        pcod_periodicidad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_periodicidad);

                        DbParameter popcion_compra = cmdTransaccionFactory.CreateParameter();
                        popcion_compra.ParameterName = "p_opcion_compra";
                        popcion_compra.Value = pActivoFijo.opcion_compra;
                        popcion_compra.Direction = ParameterDirection.Input;
                        popcion_compra.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(popcion_compra);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_ACT_LEASINGACT_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pActivoFijo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActivoFijoData", "CrearLEASING", ex);
                        return null;
                    }
                }
            }
        }

        public ActivoFijo ModificarLEASING(ActivoFijo pActivoFijo, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pActivoFijo.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcodigo_act = cmdTransaccionFactory.CreateParameter();
                        pcodigo_act.ParameterName = "p_codigo_act";
                        pcodigo_act.Value = pActivoFijo.codigo_act;
                        pcodigo_act.Direction = ParameterDirection.Input;
                        pcodigo_act.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodigo_act);

                        DbParameter pnumero_cuotas = cmdTransaccionFactory.CreateParameter();
                        pnumero_cuotas.ParameterName = "p_numero_cuotas";
                        pnumero_cuotas.Value = pActivoFijo.numero_cuotas;
                        pnumero_cuotas.Direction = ParameterDirection.Input;
                        pnumero_cuotas.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_cuotas);

                        DbParameter pvalor_cuota = cmdTransaccionFactory.CreateParameter();
                        pvalor_cuota.ParameterName = "p_valor_cuota";
                        pvalor_cuota.Value = pActivoFijo.valor_cuota;
                        pvalor_cuota.Direction = ParameterDirection.Input;
                        pvalor_cuota.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_cuota);

                        DbParameter pcod_periodicidad = cmdTransaccionFactory.CreateParameter();
                        pcod_periodicidad.ParameterName = "p_cod_periodicidad";
                        pcod_periodicidad.Value = pActivoFijo.cod_periodicidad;
                        pcod_periodicidad.Direction = ParameterDirection.Input;
                        pcod_periodicidad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_periodicidad);

                        DbParameter popcion_compra = cmdTransaccionFactory.CreateParameter();
                        popcion_compra.ParameterName = "p_opcion_compra";
                        popcion_compra.Value = pActivoFijo.opcion_compra;
                        popcion_compra.Direction = ParameterDirection.Input;
                        popcion_compra.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(popcion_compra);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_ACT_LEASINGACT_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pActivoFijo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActivoFijoData", "ModificarLEASING", ex);
                        return null;
                    }
                }
            }
        }

        public ActivoFijo ConsultarLEASING(ActivoFijo entidad, Usuario vUsuario)
        {
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM leasing_activos WHERE CONSECUTIVO = " + entidad.consecutivo.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.numero_cuotas = Convert.ToInt32(resultado["NUMERO_CUOTAS"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt32(resultado["VALOR_CUOTA"]);
                            if (resultado["COD_PERIODICIDAD"] != DBNull.Value) entidad.cod_periodicidad = Convert.ToInt32(resultado["COD_PERIODICIDAD"]);
                            if (resultado["OPCION_COMPRA"] != DBNull.Value) entidad.opcion_compra = Convert.ToDecimal(resultado["OPCION_COMPRA"]);
                            entidad.bLeasing = true;
                        }
                        else
                        {
                            entidad.bLeasing = false;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActivoFijoData", "ConsultarLEASING", ex);
                        return null;
                    }
                }
            }
        }





        public Boolean DepreciarActivoFijo(ActivoFijo pActivoFijo, Int64 pCodOpe, ref string pError, Usuario vUsuario)
        {
            pError = "";
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pActivoFijo.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcod_act = cmdTransaccionFactory.CreateParameter();
                        pcod_act.ParameterName = "p_cod_act";
                        pcod_act.Value = pActivoFijo.cod_act;
                        pcod_act.Direction = ParameterDirection.Input;
                        pcod_act.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_act);

                        DbParameter pcod_ope = cmdTransaccionFactory.CreateParameter();
                        pcod_ope.ParameterName = "p_cod_ope";
                        pcod_ope.Value = pCodOpe;
                        pcod_ope.Direction = ParameterDirection.Input;
                        pcod_ope.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_ope);

                        DbParameter pvalor_a_depreciar = cmdTransaccionFactory.CreateParameter();
                        pvalor_a_depreciar.ParameterName = "p_valor_a_depreciar";
                        pvalor_a_depreciar.Value = pActivoFijo.valor_a_depreciar;
                        pvalor_a_depreciar.Direction = ParameterDirection.Input;
                        pvalor_a_depreciar.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_a_depreciar);

                        DbParameter pdias_a_depreciar = cmdTransaccionFactory.CreateParameter();
                        pdias_a_depreciar.ParameterName = "p_dias_a_depreciar";
                        pdias_a_depreciar.Value = pActivoFijo.dias_a_depreciar;
                        pdias_a_depreciar.Direction = ParameterDirection.Input;
                        pdias_a_depreciar.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pdias_a_depreciar);

                        DbParameter pfecha_depreciacion = cmdTransaccionFactory.CreateParameter();
                        pfecha_depreciacion.ParameterName = "p_fecha_depreciacion";
                        pfecha_depreciacion.Value = pActivoFijo.fecha_depreciacion;
                        pfecha_depreciacion.Direction = ParameterDirection.Input;
                        pfecha_depreciacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_depreciacion);

                        DbParameter pcentro_costo = cmdTransaccionFactory.CreateParameter();
                        pcentro_costo.ParameterName = "p_centro_costo";
                        pcentro_costo.Value = pActivoFijo.cod_costo;
                        pcentro_costo.Direction = ParameterDirection.Input;
                        pcentro_costo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcentro_costo);
                       
                        DbParameter pusuariocreacion = cmdTransaccionFactory.CreateParameter();
                        pusuariocreacion.ParameterName = "p_usuariocreacion";
                        pusuariocreacion.Value = vUsuario.codusuario;
                        pusuariocreacion.Direction = ParameterDirection.Input;
                        pusuariocreacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pusuariocreacion);
                        
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_ACT_DEPRECIACION";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return true;
                    }
                    catch (Exception ex)
                    {                        
                        pError = ex.Message;
                        return false;
                    }
                }
            }
        }

        public Boolean BajaActivoFijo(ActivoFijo pActivoFijo, Int64 pCodOpe, ref string pError, Usuario pUsuario)
        {
            pError = "";
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pActivoFijo.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcod_act = cmdTransaccionFactory.CreateParameter();
                        pcod_act.ParameterName = "p_cod_act";
                        pcod_act.Value = pActivoFijo.cod_act;
                        pcod_act.Direction = ParameterDirection.Input;
                        pcod_act.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_act);
                        
                        DbParameter pfecha_baja = cmdTransaccionFactory.CreateParameter();
                        pfecha_baja.ParameterName = "p_fecha_baja";
                        pfecha_baja.Value = pActivoFijo.fecha_baja;
                        pfecha_baja.Direction = ParameterDirection.Input;
                        pfecha_baja.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_baja);
                        
                        DbParameter pmotivo = cmdTransaccionFactory.CreateParameter();
                        pmotivo.ParameterName = "p_motivo";
                        pmotivo.Value = pActivoFijo.motivo;
                        pmotivo.Direction = ParameterDirection.Input;
                        pmotivo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pmotivo);

                        DbParameter pcod_ope = cmdTransaccionFactory.CreateParameter();
                        pcod_ope.ParameterName = "p_cod_ope";
                        pcod_ope.Value = pCodOpe;
                        pcod_ope.Direction = ParameterDirection.Input;
                        pcod_ope.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_ope);

                        DbParameter pusuario = cmdTransaccionFactory.CreateParameter();
                        pusuario.ParameterName = "p_usuario";
                        pusuario.Value = pUsuario.codusuario;
                        pusuario.Direction = ParameterDirection.Input;
                        pusuario.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pusuario);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_ACT_BAJA";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return true;
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                        BOExcepcion.Throw("ActivoFijoData", "BajaActivoFijo", ex);
                        return false;
                    }
                }
            }
        }

        public Boolean VentaActivoFijo(ActivoFijo pActivoFijo, Int64 pCodOpe, ref string pError, Usuario pUsuario)
        {
            pError = "";
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pActivoFijo.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcod_act = cmdTransaccionFactory.CreateParameter();
                        pcod_act.ParameterName = "p_cod_act";
                        pcod_act.Value = pActivoFijo.cod_act;
                        pcod_act.Direction = ParameterDirection.Input;
                        pcod_act.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_act);

                        DbParameter pfecha_venta = cmdTransaccionFactory.CreateParameter();
                        pfecha_venta.ParameterName = "p_fecha_venta";
                        pfecha_venta.Value = pActivoFijo.fecha_venta;
                        pfecha_venta.Direction = ParameterDirection.Input;
                        pfecha_venta.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_venta);

                        DbParameter pvalor_venta = cmdTransaccionFactory.CreateParameter();
                        pvalor_venta.ParameterName = "p_valor_venta";
                        pvalor_venta.Value = pActivoFijo.valor_venta;
                        pvalor_venta.Direction = ParameterDirection.Input;
                        pvalor_venta.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_venta);

                        DbParameter pcod_comprador = cmdTransaccionFactory.CreateParameter();
                        pcod_comprador.ParameterName = "p_cod_comprador";
                        pcod_comprador.Value = pActivoFijo.cod_comprador;
                        pcod_comprador.Direction = ParameterDirection.Input;
                        pcod_comprador.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_comprador);

                        DbParameter pcod_ope = cmdTransaccionFactory.CreateParameter();
                        pcod_ope.ParameterName = "p_cod_ope";
                        pcod_ope.Value = pCodOpe;
                        pcod_ope.Direction = ParameterDirection.Input;
                        pcod_ope.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_ope);

                        DbParameter pusuario = cmdTransaccionFactory.CreateParameter();
                        pusuario.ParameterName = "p_usuario";
                        pusuario.Value = pUsuario.codusuario;
                        pusuario.Direction = ParameterDirection.Input;
                        pusuario.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pusuario);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_ACT_VENTA";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return true;
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                        BOExcepcion.Throw("ActivoFijoData", "VentaActivoFijo", ex);
                        return false;
                    }
                }
            }
        }


        public ActivoFijo ModificarMantenimientoNif(ActivoFijo pActivoFijo, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pActivoFijo.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);
                                                                                       

                        DbParameter pobservaciones = cmdTransaccionFactory.CreateParameter();
                        pobservaciones.ParameterName = "p_observaciones";
                        pobservaciones.Value = pActivoFijo.observaciones;
                        pobservaciones.Direction = ParameterDirection.Input;
                        pobservaciones.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pobservaciones);

                      
                        DbParameter pfecultmod = cmdTransaccionFactory.CreateParameter();
                        pfecultmod.ParameterName = "p_fecultmod";
                        pfecultmod.Value = pActivoFijo.fecultmod;
                        pfecultmod.Direction = ParameterDirection.Input;
                        pfecultmod.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecultmod);

                        //AGREGADO_NIIF                       

                        DbParameter pcodclasificacion_nif = cmdTransaccionFactory.CreateParameter();
                        pcodclasificacion_nif.ParameterName = "p_codclasificacion_nif";
                        pcodclasificacion_nif.Value = pActivoFijo.codclasificacion_nif;
                        pcodclasificacion_nif.Direction = ParameterDirection.Input;
                        pcodclasificacion_nif.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodclasificacion_nif);

                        DbParameter ptipo_activo_nif = cmdTransaccionFactory.CreateParameter();
                        ptipo_activo_nif.ParameterName = "p_tipo_activo_nif";
                        ptipo_activo_nif.Value = pActivoFijo.tipo_activo_nif;
                        ptipo_activo_nif.Direction = ParameterDirection.Input;
                        ptipo_activo_nif.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_activo_nif);

                        DbParameter pvida_util_nif = cmdTransaccionFactory.CreateParameter();
                        pvida_util_nif.ParameterName = "p_vida_util_nif";
                        pvida_util_nif.Value = pActivoFijo.vida_util_nif;
                        pvida_util_nif.Direction = ParameterDirection.Input;
                        pvida_util_nif.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvida_util_nif);

                        DbParameter pvalor_residual_nif = cmdTransaccionFactory.CreateParameter();
                        pvalor_residual_nif.ParameterName = "p_valor_residual_nif";
                        pvalor_residual_nif.Value = pActivoFijo.valor_residual_nif;
                        pvalor_residual_nif.Direction = ParameterDirection.Input;
                        pvalor_residual_nif.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_residual_nif);

                      
                        DbParameter pvrdeterioro_nif = cmdTransaccionFactory.CreateParameter();
                        pvrdeterioro_nif.ParameterName = "p_vrdeterioro_nif";
                        pvrdeterioro_nif.Value = pActivoFijo.vrdeterioro_nif;
                        pvrdeterioro_nif.Direction = ParameterDirection.Input;
                        pvrdeterioro_nif.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvrdeterioro_nif);

                        DbParameter pvrrecdeterioro_nif = cmdTransaccionFactory.CreateParameter();
                        pvrrecdeterioro_nif.ParameterName = "p_vrrecdeterioro_nif";
                        pvrrecdeterioro_nif.Value = pActivoFijo.vrrecdeterioro_nif;
                        pvrrecdeterioro_nif.Direction = ParameterDirection.Input;
                        pvrrecdeterioro_nif.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvrrecdeterioro_nif);
                                            

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_ACT_MANTENINIF_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pActivoFijo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActivoFijoData", "ModificarMantenimientoNif", ex);
                        return null;
                    }
                }
            }
        }



        public void CrearDeterioroNIF(ActivoFijo pActivoFijo,int pCodOpe, Usuario vUsuario)
        {
           
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pActivoFijo.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcodigo_act = cmdTransaccionFactory.CreateParameter();
                        pcodigo_act.ParameterName = "p_codigo_act";
                        pcodigo_act.Value = pActivoFijo.codigo_act;
                        pcodigo_act.Direction = ParameterDirection.Input;
                        pcodigo_act.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodigo_act);

                        DbParameter pcodigo_ope = cmdTransaccionFactory.CreateParameter();
                        pcodigo_ope.ParameterName = "p_codigo_ope";
                        pcodigo_ope.Value = pCodOpe;
                        pcodigo_ope.Direction = ParameterDirection.Input;
                        pcodigo_ope.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodigo_ope);

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "p_fecha";
                        pfecha.Value = pActivoFijo.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        DbParameter pvalor_deterioro = cmdTransaccionFactory.CreateParameter();
                        pvalor_deterioro.ParameterName = "p_valor_deterioro";
                        pvalor_deterioro.Value = pActivoFijo.valor_deterioro;
                        pvalor_deterioro.Direction = ParameterDirection.Input;
                        pvalor_deterioro.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_deterioro);

                        DbParameter pobservaciones = cmdTransaccionFactory.CreateParameter();
                        pobservaciones.ParameterName = "p_observaciones";
                        pobservaciones.Value = pActivoFijo.observaciones;
                        if (pActivoFijo.observaciones == "")
                        {
                            pobservaciones.Value = DBNull.Value;
                        }
                        pobservaciones.Direction = ParameterDirection.Input;
                        pobservaciones.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pobservaciones);

                        DbParameter pusuariocreacion = cmdTransaccionFactory.CreateParameter();
                        pusuariocreacion.ParameterName = "p_usuariocreacion";
                        pusuariocreacion.Value = vUsuario.codusuario;
                        pusuariocreacion.Direction = ParameterDirection.Input;
                        pusuariocreacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pusuariocreacion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_ACT_DETERIORO";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {                       
                        BOExcepcion.Throw("ActivoFijoData", "CrearDeterioroNIF", ex);                       
                    }
                }
            }
        }



        public ActivoFijo CrearCOMPRA_ACTIVO(ActivoFijo pCOMPRA_ACTIVO, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pCOMPRA_ACTIVO.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcodigo_act = cmdTransaccionFactory.CreateParameter();
                        pcodigo_act.ParameterName = "p_codigo_act";
                        pcodigo_act.Value = pCOMPRA_ACTIVO.codigo_act;
                        pcodigo_act.Direction = ParameterDirection.Input;
                        pcodigo_act.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigo_act);

                        DbParameter pcod_ope = cmdTransaccionFactory.CreateParameter();
                        pcod_ope.ParameterName = "p_cod_ope";
                        pcod_ope.Value = pCOMPRA_ACTIVO.cod_ope;
                        pcod_ope.Direction = ParameterDirection.Input;
                        pcod_ope.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_ope);

                        DbParameter pfecha_compra = cmdTransaccionFactory.CreateParameter();
                        pfecha_compra.ParameterName = "p_fecha_compra";
                        if (pCOMPRA_ACTIVO.fecha_compra == null)
                            pfecha_compra.Value = DBNull.Value;
                        else
                            pfecha_compra.Value = pCOMPRA_ACTIVO.fecha_compra;
                        pfecha_compra.Direction = ParameterDirection.Input;
                        pfecha_compra.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_compra);

                        DbParameter pvalor_compra = cmdTransaccionFactory.CreateParameter();
                        pvalor_compra.ParameterName = "p_valor_compra";
                        pvalor_compra.Value = pCOMPRA_ACTIVO.valor_compra;
                        pvalor_compra.Direction = ParameterDirection.Input;
                        pvalor_compra.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_compra);

                        DbParameter ptipo_tran = cmdTransaccionFactory.CreateParameter();
                        ptipo_tran.ParameterName = "p_tipo_tran";
                        if (pCOMPRA_ACTIVO.tipo_tran == null)
                            ptipo_tran.Value = DBNull.Value;
                        else
                            ptipo_tran.Value = pCOMPRA_ACTIVO.tipo_tran;
                        ptipo_tran.Direction = ParameterDirection.Input;
                        ptipo_tran.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_tran);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_ACT_COMPRA_ACT_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pCOMPRA_ACTIVO;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("COMPRA_ACTIVOData", "CrearCOMPRA_ACTIVO", ex);
                        return null;
                    }
                }
            }
        }


        public string ValidarDeterioroNiif(DateTime pFecha, Usuario vUsuario)
        {
            string pMensaje = string.Empty;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "p_fecha";
                        pfecha.Value = pFecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        DbParameter pmensaje_error = cmdTransaccionFactory.CreateParameter();
                        pmensaje_error.ParameterName = "p_mensaje_error";
                        pmensaje_error.Value = DBNull.Value;
                        pmensaje_error.Size = 8000;
                        pmensaje_error.DbType = DbType.String;
                        pmensaje_error.Direction = ParameterDirection.Output;
                        cmdTransaccionFactory.Parameters.Add(pmensaje_error);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NIF_VALIDA_DETERIORO";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pMensaje = Convert.ToString(pmensaje_error.Value);
                        return pMensaje;
                    }
                    catch (Exception ex)
                    {
                        pMensaje = ex.Message;
                        return pMensaje;
                    }
                }
            }
        }




        public ActivoFijo ConsultarCierreActivosFijos(Usuario vUsuario)
        {
            DbDataReader resultado;
            ActivoFijo entidad = new ActivoFijo();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT MAX(FECHA) as fecha,estado FROM CIEREA WHERE TIPO = 'Y' AND ESTADO = 'D'   group by estado";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha_cierre = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estadocierre = Convert.ToString(resultado["ESTADO"]);

                        }
                        else
                        {
                            dbConnectionFactory.CerrarConexion(connection);
                            return null;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;


                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActivosFijosData", "ConsultarCierreActivosFijos", ex);
                        return null;
                    }
                }
            }
        }




        public List<ActivoFijo> ListarActivoFijoReporteCierre(DateTime pFecha, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ActivoFijo> lstActivosFijos= new List<ActivoFijo>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"SELECT H.FECHA_HISTORICO,h.	COD_ACT	,h.	CLASE	,h.	TIPO	,h.	COD_UBICA	,h.	COD_COSTO	,h.	NOMBRE	,h.	ANOS_UTIL	,h.	ESTADO	,h.	COD_ENCARGADO	,    h.	FECHA_COMPRA	,h.	VALOR_COMPRA	,h.	VALOR_AVALUO	,h.	VALOR_SALVAMEN	,h.	COD_OFICINA	,h.	FECHA_ULT_DEPRE	,h.	ACUMULADO_DEPRECIACION	,h.	SALDO_POR_DEPRECIAR	,h.	CODCLASIFICACION_NIF	,h.	TIPO_ACTIVO_NIF	,h.	VALOR_ACTIVO_NIF	,h.	VIDA_UTIL_NIF   ,   h.	VALOR_RESIDUAL_NIF	,h.	PORCENTAJE_RESIDUAL_NIF	,h.	ADICIONES_NIF	,h.	VRDETERIORO_NIF	,h.	VRRECDETERIORO_NIF	,h.	REVALUACION_NIF	,h.	REVREVALUACION_NIF "
                                    + "from HISTORICO_ACTIVOS_FIJOS h inner join activos_fijos a on h.COD_ACT=a.COD_ACT "                                    
                                    + "Left join TIPO_ACTIVO l on h.TIPO=l.TIPO "
                                   ;
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            sql = sql + " where h.fecha_historico = to_date('" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "','" + conf.ObtenerFormatoFecha() + "')";
                        else
                            sql = sql + " where h.fecha_historico = '" + pFecha + "'";
                        sql = sql + "order by h.COD_ACT asc "; 
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ActivoFijo entidad = new ActivoFijo();
                            if (resultado["FECHA_HISTORICO"] != DBNull.Value) entidad.fecha_historico = Convert.ToDateTime(resultado["FECHA_HISTORICO"]);
                            if (resultado["COD_ACT"] != DBNull.Value) entidad.cod_act = Convert.ToInt32(resultado["COD_ACT"]);
                            if (resultado["CLASE"] != DBNull.Value) entidad.clase = Convert.ToInt32(resultado["CLASE"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToInt32(resultado["TIPO"]);
                            if (resultado["COD_UBICA"] != DBNull.Value) entidad.cod_ubica = Convert.ToInt32(resultado["COD_UBICA"]);
                            if (resultado["COD_COSTO"] != DBNull.Value) entidad.cod_costo = Convert.ToInt32(resultado["COD_COSTO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["ANOS_UTIL"] != DBNull.Value) entidad.anos_util = Convert.ToInt32(resultado["ANOS_UTIL"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["COD_ENCARGADO"] != DBNull.Value) entidad.cod_encargado = Convert.ToInt32(resultado["COD_ENCARGADO"]);
                            if (resultado["FECHA_COMPRA"] != DBNull.Value) entidad.fecha_compra = Convert.ToDateTime(resultado["FECHA_COMPRA"]);
                            if (resultado["VALOR_COMPRA"] != DBNull.Value) entidad.valor_compra = Convert.ToDecimal(resultado["VALOR_COMPRA"]);
                            if (resultado["VALOR_AVALUO"] != DBNull.Value) entidad.valor_avaluo = Convert.ToDecimal(resultado["VALOR_AVALUO"]);
                            if (resultado["VALOR_SALVAMEN"] != DBNull.Value) entidad.valor_salvamen = Convert.ToDecimal(resultado["VALOR_SALVAMEN"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["COD_OFICINA"]);
                            if (resultado["FECHA_ULT_DEPRE"] != DBNull.Value) entidad.fecha_ult_depre = Convert.ToDateTime(resultado["FECHA_ULT_DEPRE"]);
                            if (resultado["ACUMULADO_DEPRECIACION"] != DBNull.Value) entidad.acumulado_depreciacion = Convert.ToDecimal(resultado["ACUMULADO_DEPRECIACION"]);
                            if (resultado["SALDO_POR_DEPRECIAR"] != DBNull.Value) entidad.saldo_por_depreciar = Convert.ToDecimal(resultado["SALDO_POR_DEPRECIAR"]);
                            if (resultado["CODCLASIFICACION_NIF"] != DBNull.Value) entidad.codclasificacion_nif = Convert.ToInt32(resultado["CODCLASIFICACION_NIF"]);
                            if (resultado["TIPO_ACTIVO_NIF"] != DBNull.Value) entidad.tipo_activo_nif = Convert.ToInt32(resultado["TIPO_ACTIVO_NIF"]);
                            if (resultado["VALOR_ACTIVO_NIF"] != DBNull.Value) entidad.valor_activo_nif = Convert.ToDecimal(resultado["VALOR_ACTIVO_NIF"]);
                            if (resultado["VIDA_UTIL_NIF"] != DBNull.Value) entidad.vida_util_nif = Convert.ToDecimal(resultado["VIDA_UTIL_NIF"]);
                            if (resultado["VALOR_RESIDUAL_NIF"] != DBNull.Value) entidad.valor_residual_nif = Convert.ToDecimal(resultado["VALOR_RESIDUAL_NIF"]);
                            if (resultado["PORCENTAJE_RESIDUAL_NIF"] != DBNull.Value) entidad.porcentaje_residual_nif = Convert.ToDecimal(resultado["PORCENTAJE_RESIDUAL_NIF"]);
                            if (resultado["ADICIONES_NIF"] != DBNull.Value) entidad.adiciones_nif = Convert.ToDecimal(resultado["ADICIONES_NIF"]);
                            if (resultado["VRDETERIORO_NIF"] != DBNull.Value) entidad.vrdeterioro_nif = Convert.ToDecimal(resultado["VRDETERIORO_NIF"]);
                            if (resultado["VRRECDETERIORO_NIF"] != DBNull.Value) entidad.vrrecdeterioro_nif = Convert.ToDecimal(resultado["VRRECDETERIORO_NIF"]);
                            if (resultado["REVALUACION_NIF"] != DBNull.Value) entidad.revaluacion_nif = Convert.ToDecimal(resultado["REVALUACION_NIF"]);
                            if (resultado["REVREVALUACION_NIF"] != DBNull.Value) entidad.revrevaluacion_nif = Convert.ToDecimal(resultado["REVREVALUACION_NIF"]);
                         

                            lstActivosFijos.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstActivosFijos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActivosFijosData", "ListarActivoFijoReporteCierre", ex);
                        return null;
                    }
                }
            }
        }


    }
}
