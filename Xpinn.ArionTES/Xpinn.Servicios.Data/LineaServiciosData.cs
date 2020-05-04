using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Servicios.Entities;
using Xpinn.Imagenes.Data;

namespace Xpinn.Servicios.Data
{
    public class LineaServiciosData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;
        private ImagenesORAData DAImagenes;

        public LineaServiciosData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public LineaServicios CrearLineaServicio(LineaServicios pLinea, byte[] foto, byte[] banner, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_linea_servicio = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_servicio.ParameterName = "p_cod_linea_servicio";
                        pcod_linea_servicio.Value = pLinea.cod_linea_servicio;
                        pcod_linea_servicio.Direction = ParameterDirection.Input;
                        pcod_linea_servicio.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_servicio);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "p_nombre";
                        pnombre.Value = pLinea.nombre;
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        DbParameter ptipo_servicio = cmdTransaccionFactory.CreateParameter();
                        ptipo_servicio.ParameterName = "p_tipo_servicio";
                        ptipo_servicio.Value = pLinea.tipo_servicio;
                        ptipo_servicio.Direction = ParameterDirection.Input;
                        ptipo_servicio.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_servicio);

                        DbParameter pidentificacion_proveedor = cmdTransaccionFactory.CreateParameter();
                        pidentificacion_proveedor.ParameterName = "p_identificacion_proveedor";
                        if (pLinea.identificacion_proveedor != null) pidentificacion_proveedor.Value = pLinea.identificacion_proveedor; else pidentificacion_proveedor.Value = DBNull.Value;
                        pidentificacion_proveedor.Direction = ParameterDirection.Input;
                        pidentificacion_proveedor.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidentificacion_proveedor);

                        DbParameter pnombre_proveedor = cmdTransaccionFactory.CreateParameter();
                        pnombre_proveedor.ParameterName = "p_nombre_proveedor";
                        if (pLinea.nombre_proveedor != null) pnombre_proveedor.Value = pLinea.nombre_proveedor; else pnombre_proveedor.Value = DBNull.Value;
                        pnombre_proveedor.Direction = ParameterDirection.Input;
                        pnombre_proveedor.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre_proveedor);

                        DbParameter pcodperiodo_renovacion = cmdTransaccionFactory.CreateParameter();
                        pcodperiodo_renovacion.ParameterName = "p_codperiodo_renovacion";
                        if (pLinea.codperiodo_renovacion != 0) pcodperiodo_renovacion.Value = pLinea.codperiodo_renovacion; else pcodperiodo_renovacion.Value = DBNull.Value;
                        pcodperiodo_renovacion.Direction = ParameterDirection.Input;
                        pcodperiodo_renovacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodperiodo_renovacion);

                        DbParameter pcodperiodo_pago = cmdTransaccionFactory.CreateParameter();
                        pcodperiodo_pago.ParameterName = "p_codperiodo_pago";
                        if (pLinea.codperiodo_pago != 0) pcodperiodo_pago.Value = pLinea.codperiodo_pago; else pcodperiodo_pago.Value = DBNull.Value;
                        pcodperiodo_pago.Direction = ParameterDirection.Input;
                        pcodperiodo_pago.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodperiodo_pago);

                        DbParameter pcodperiocidad = cmdTransaccionFactory.CreateParameter();
                        pcodperiocidad.ParameterName = "p_codperiocidad";
                        if (pLinea.codperiocidad != 0) pcodperiocidad.Value = pLinea.codperiocidad; else pcodperiocidad.Value = DBNull.Value;
                        pcodperiocidad.Direction = ParameterDirection.Input;
                        pcodperiocidad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodperiocidad);

                        DbParameter pnumero_beneficiarios = cmdTransaccionFactory.CreateParameter();
                        pnumero_beneficiarios.ParameterName = "p_numero_beneficiarios";
                        if (pLinea.numero_beneficiarios != 0) pnumero_beneficiarios.Value = pLinea.numero_beneficiarios; else pnumero_beneficiarios.Value = DBNull.Value;
                        pnumero_beneficiarios.Direction = ParameterDirection.Input;
                        pnumero_beneficiarios.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_beneficiarios);

                        DbParameter pcobra_interes = cmdTransaccionFactory.CreateParameter();
                        pcobra_interes.ParameterName = "p_cobra_interes";
                        pcobra_interes.Value = pLinea.cobra_interes;
                        pcobra_interes.Direction = ParameterDirection.Input;
                        pcobra_interes.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcobra_interes);

                        DbParameter requierebenef = cmdTransaccionFactory.CreateParameter();
                        requierebenef.ParameterName = "P_REQUIEREBENEFICIARIOS";
                        requierebenef.Value = pLinea.requierebeneficiarios;
                        requierebenef.Direction = ParameterDirection.Input;
                        requierebenef.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(requierebenef);

                        DbParameter ptasa_interes = cmdTransaccionFactory.CreateParameter();
                        ptasa_interes.ParameterName = "p_tasa_interes";
                        if (pLinea.tasa_interes != 0) ptasa_interes.Value = pLinea.tasa_interes; else ptasa_interes.Value = DBNull.Value;
                        ptasa_interes.Direction = ParameterDirection.Input;
                        ptasa_interes.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ptasa_interes);

                        DbParameter ptipo_tasa = cmdTransaccionFactory.CreateParameter();
                        ptipo_tasa.ParameterName = "p_tipo_tasa";
                        if (pLinea.tipo_tasa != 0) ptipo_tasa.Value = pLinea.tipo_tasa; else ptipo_tasa.Value = DBNull.Value;
                        ptipo_tasa.Direction = ParameterDirection.Input;
                        ptipo_tasa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_tasa);

                        DbParameter pfecha_pago_proveedor = cmdTransaccionFactory.CreateParameter();
                        pfecha_pago_proveedor.ParameterName = "p_fecha_pago_proveedor";
                        if (pLinea.fecha_pago_proveedor != DateTime.MinValue) pfecha_pago_proveedor.Value = pLinea.fecha_pago_proveedor; else pfecha_pago_proveedor.Value = DBNull.Value;
                        pfecha_pago_proveedor.Direction = ParameterDirection.Input;
                        pfecha_pago_proveedor.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_pago_proveedor);

                        DbParameter pno_requiere_aprobacion = cmdTransaccionFactory.CreateParameter();
                        pno_requiere_aprobacion.ParameterName = "p_no_requiere_aprobacion";
                        pno_requiere_aprobacion.Value = pLinea.no_requiere_aprobacion;
                        pno_requiere_aprobacion.Direction = ParameterDirection.Input;
                        pno_requiere_aprobacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pno_requiere_aprobacion);

                        DbParameter p_numero_servicios = cmdTransaccionFactory.CreateParameter();
                        p_numero_servicios.ParameterName = "p_numero_servicios";
                        p_numero_servicios.Value = pLinea.numero_servicios;
                        p_numero_servicios.Direction = ParameterDirection.Input;
                        p_numero_servicios.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_numero_servicios);

                        DbParameter p_MAXIMO_PLAZO = cmdTransaccionFactory.CreateParameter();
                        p_MAXIMO_PLAZO.ParameterName = "p_MAXIMO_PLAZO";
                        p_MAXIMO_PLAZO.Value = pLinea.maximo_plazo;
                        p_MAXIMO_PLAZO.Direction = ParameterDirection.Input;
                        p_MAXIMO_PLAZO.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_MAXIMO_PLAZO);

                        DbParameter p_MAXIMO_VALOR = cmdTransaccionFactory.CreateParameter();
                        p_MAXIMO_VALOR.ParameterName = "p_MAXIMO_VALOR";
                        p_MAXIMO_VALOR.Value = pLinea.maximo_valor;
                        p_MAXIMO_VALOR.Direction = ParameterDirection.Input;
                        p_MAXIMO_VALOR.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_MAXIMO_VALOR);

                        DbParameter p_TIPO_CUOTA = cmdTransaccionFactory.CreateParameter();
                        p_TIPO_CUOTA.ParameterName = "p_TIPO_CUOTA";
                        p_TIPO_CUOTA.Value = pLinea.tipo_cuota;
                        p_TIPO_CUOTA.Direction = ParameterDirection.Input;
                        p_TIPO_CUOTA.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_TIPO_CUOTA);

                        DbParameter p_orden = cmdTransaccionFactory.CreateParameter();
                        p_orden.ParameterName = "p_orden";
                        p_orden.Value = pLinea.orden;
                        p_orden.Direction = ParameterDirection.Input;
                        p_orden.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_orden);

                        DbParameter p_ocultar_informacion = cmdTransaccionFactory.CreateParameter();
                        p_ocultar_informacion.ParameterName = "p_ocultar_informacion";
                        p_ocultar_informacion.Value = pLinea.ocultar_informacion;
                        p_ocultar_informacion.Direction = ParameterDirection.Input;
                        p_ocultar_informacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_ocultar_informacion);

                        DbParameter p_maneja_causacion = cmdTransaccionFactory.CreateParameter();
                        p_maneja_causacion.ParameterName = "p_maneja_causacion";
                        p_maneja_causacion.Value = pLinea.maneja_causacion;
                        p_maneja_causacion.Direction = ParameterDirection.Input;
                        p_maneja_causacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_maneja_causacion);

                        DbParameter p_nogenerarvacaciones = cmdTransaccionFactory.CreateParameter();
                        p_nogenerarvacaciones.ParameterName = "p_nogenerarvacaciones";
                        p_nogenerarvacaciones.Value = pLinea.no_generar_vacaciones;
                        p_nogenerarvacaciones.Direction = ParameterDirection.Input;
                        p_nogenerarvacaciones.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_nogenerarvacaciones);

                        DbParameter p_servicio_telefonia = cmdTransaccionFactory.CreateParameter();
                        p_servicio_telefonia.ParameterName = "p_servicio_telefonia";
                        p_servicio_telefonia.Value = pLinea.servicio_telefonia;
                        p_servicio_telefonia.Direction = ParameterDirection.Input;
                        p_servicio_telefonia.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_servicio_telefonia);

                        DbParameter p_maneja_retirados = cmdTransaccionFactory.CreateParameter();
                        p_maneja_retirados.ParameterName = "P_MANEJA_RETIRADOS";
                        p_maneja_retirados.Value = pLinea.maneja_retirados;
                        p_maneja_retirados.Direction = ParameterDirection.Input;
                        p_maneja_retirados.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_maneja_retirados);

                        DbParameter p_oficina_virtual = cmdTransaccionFactory.CreateParameter();
                        p_oficina_virtual.ParameterName = "P_OFICINA_VIRTUAL";
                        p_oficina_virtual.Value = pLinea.oficinaVirtual;
                        p_oficina_virtual.Direction = ParameterDirection.Input;
                        p_oficina_virtual.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_oficina_virtual);

                        DbParameter p_enlace = cmdTransaccionFactory.CreateParameter();
                        p_enlace.ParameterName = "P_ENLACE";
                        p_enlace.Value = pLinea.enlace;
                        p_enlace.Direction = ParameterDirection.Input;
                        p_enlace.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_enlace);

                        connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SER_LINEASSERV_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);

                        return pLinea;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaServiciosData", "CrearLineaServicio", ex);
                        return null;
                    }
                }
            }
        }

        public LineaServicios ModificarLineaServicio(LineaServicios pLinea, byte[] foto, byte[] banner, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_linea_servicio = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_servicio.ParameterName = "p_cod_linea_servicio";
                        pcod_linea_servicio.Value = pLinea.cod_linea_servicio;
                        pcod_linea_servicio.Direction = ParameterDirection.Input;
                        pcod_linea_servicio.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_servicio);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "p_nombre";
                        pnombre.Value = pLinea.nombre;
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        DbParameter ptipo_servicio = cmdTransaccionFactory.CreateParameter();
                        ptipo_servicio.ParameterName = "p_tipo_servicio";
                        ptipo_servicio.Value = pLinea.tipo_servicio;
                        ptipo_servicio.Direction = ParameterDirection.Input;
                        ptipo_servicio.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_servicio);

                        DbParameter pidentificacion_proveedor = cmdTransaccionFactory.CreateParameter();
                        pidentificacion_proveedor.ParameterName = "p_identificacion_proveedor";
                        if (pLinea.identificacion_proveedor != null) pidentificacion_proveedor.Value = pLinea.identificacion_proveedor; else pidentificacion_proveedor.Value = DBNull.Value;
                        pidentificacion_proveedor.Direction = ParameterDirection.Input;
                        pidentificacion_proveedor.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidentificacion_proveedor);

                        DbParameter pnombre_proveedor = cmdTransaccionFactory.CreateParameter();
                        pnombre_proveedor.ParameterName = "p_nombre_proveedor";
                        if (pLinea.nombre_proveedor != null) pnombre_proveedor.Value = pLinea.nombre_proveedor; else pnombre_proveedor.Value = DBNull.Value;
                        pnombre_proveedor.Direction = ParameterDirection.Input;
                        pnombre_proveedor.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre_proveedor);

                        DbParameter pcodperiodo_renovacion = cmdTransaccionFactory.CreateParameter();
                        pcodperiodo_renovacion.ParameterName = "p_codperiodo_renovacion";
                        if (pLinea.codperiodo_renovacion != 0) pcodperiodo_renovacion.Value = pLinea.codperiodo_renovacion; else pcodperiodo_renovacion.Value = DBNull.Value;
                        pcodperiodo_renovacion.Direction = ParameterDirection.Input;
                        pcodperiodo_renovacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodperiodo_renovacion);

                        DbParameter pcodperiodo_pago = cmdTransaccionFactory.CreateParameter();
                        pcodperiodo_pago.ParameterName = "p_codperiodo_pago";
                        if (pLinea.codperiodo_pago != 0) pcodperiodo_pago.Value = pLinea.codperiodo_pago; else pcodperiodo_pago.Value = DBNull.Value;
                        pcodperiodo_pago.Direction = ParameterDirection.Input;
                        pcodperiodo_pago.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodperiodo_pago);

                        DbParameter requierebeneficiarios = cmdTransaccionFactory.CreateParameter();
                        requierebeneficiarios.ParameterName = "P_REQUIEREBENEFICIARIOS";
                        requierebeneficiarios.Value = pLinea.requierebeneficiarios;
                        requierebeneficiarios.Direction = ParameterDirection.Input;
                        requierebeneficiarios.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(requierebeneficiarios);

                        DbParameter pcodperiocidad = cmdTransaccionFactory.CreateParameter();
                        pcodperiocidad.ParameterName = "p_codperiocidad";
                        if (pLinea.codperiocidad != 0) pcodperiocidad.Value = pLinea.codperiocidad; else pcodperiocidad.Value = DBNull.Value;
                        pcodperiocidad.Direction = ParameterDirection.Input;
                        pcodperiocidad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodperiocidad);

                        DbParameter pnumero_beneficiarios = cmdTransaccionFactory.CreateParameter();
                        pnumero_beneficiarios.ParameterName = "p_numero_beneficiarios";
                        if (pLinea.numero_beneficiarios != 0) pnumero_beneficiarios.Value = pLinea.numero_beneficiarios; else pnumero_beneficiarios.Value = DBNull.Value;
                        pnumero_beneficiarios.Direction = ParameterDirection.Input;
                        pnumero_beneficiarios.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_beneficiarios);

                        DbParameter pcobra_interes = cmdTransaccionFactory.CreateParameter();
                        pcobra_interes.ParameterName = "p_cobra_interes";
                        pcobra_interes.Value = pLinea.cobra_interes;
                        pcobra_interes.Direction = ParameterDirection.Input;
                        pcobra_interes.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcobra_interes);

                        DbParameter ptasa_interes = cmdTransaccionFactory.CreateParameter();
                        ptasa_interes.ParameterName = "p_tasa_interes";
                        if (pLinea.tasa_interes != 0) ptasa_interes.Value = pLinea.tasa_interes; else ptasa_interes.Value = DBNull.Value;
                        ptasa_interes.Direction = ParameterDirection.Input;
                        ptasa_interes.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ptasa_interes);

                        DbParameter ptipo_tasa = cmdTransaccionFactory.CreateParameter();
                        ptipo_tasa.ParameterName = "p_tipo_tasa";
                        if (pLinea.tipo_tasa != 0) ptipo_tasa.Value = pLinea.tipo_tasa; else ptipo_tasa.Value = DBNull.Value;
                        ptipo_tasa.Direction = ParameterDirection.Input;
                        ptipo_tasa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_tasa);

                        DbParameter pfecha_pago_proveedor = cmdTransaccionFactory.CreateParameter();
                        pfecha_pago_proveedor.ParameterName = "p_fecha_pago_proveedor";
                        if (pLinea.fecha_pago_proveedor != DateTime.MinValue) pfecha_pago_proveedor.Value = pLinea.fecha_pago_proveedor; else pfecha_pago_proveedor.Value = DBNull.Value;
                        pfecha_pago_proveedor.Direction = ParameterDirection.Input;
                        pfecha_pago_proveedor.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_pago_proveedor);

                        DbParameter pno_requiere_aprobacion = cmdTransaccionFactory.CreateParameter();
                        pno_requiere_aprobacion.ParameterName = "p_no_requiere_aprobacion";
                        pno_requiere_aprobacion.Value = pLinea.no_requiere_aprobacion;
                        pno_requiere_aprobacion.Direction = ParameterDirection.Input;
                        pno_requiere_aprobacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pno_requiere_aprobacion);

                        DbParameter p_numero_servicios = cmdTransaccionFactory.CreateParameter();
                        p_numero_servicios.ParameterName = "p_numero_servicios";
                        p_numero_servicios.Value = pLinea.numero_servicios;
                        p_numero_servicios.Direction = ParameterDirection.Input;
                        p_numero_servicios.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_numero_servicios);

                        DbParameter p_MAXIMO_PLAZO = cmdTransaccionFactory.CreateParameter();
                        p_MAXIMO_PLAZO.ParameterName = "p_MAXIMO_PLAZO";
                        p_MAXIMO_PLAZO.Value = pLinea.maximo_plazo;
                        p_MAXIMO_PLAZO.Direction = ParameterDirection.Input;
                        p_MAXIMO_PLAZO.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_MAXIMO_PLAZO);

                        DbParameter p_MAXIMO_VALOR = cmdTransaccionFactory.CreateParameter();
                        p_MAXIMO_VALOR.ParameterName = "p_MAXIMO_VALOR";
                        p_MAXIMO_VALOR.Value = pLinea.maximo_valor;
                        p_MAXIMO_VALOR.Direction = ParameterDirection.Input;
                        p_MAXIMO_VALOR.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_MAXIMO_VALOR);

                        DbParameter p_TIPO_CUOTA = cmdTransaccionFactory.CreateParameter();
                        p_TIPO_CUOTA.ParameterName = "p_TIPO_CUOTA";
                        p_TIPO_CUOTA.Value = pLinea.tipo_cuota;
                        p_TIPO_CUOTA.Direction = ParameterDirection.Input;
                        p_TIPO_CUOTA.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_TIPO_CUOTA);

                        DbParameter p_orden = cmdTransaccionFactory.CreateParameter();
                        p_orden.ParameterName = "p_orden";
                        p_orden.Value = pLinea.orden;
                        p_orden.Direction = ParameterDirection.Input;
                        p_orden.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_orden);

                        DbParameter p_ocultar_informacion = cmdTransaccionFactory.CreateParameter();
                        p_ocultar_informacion.ParameterName = "p_ocultar_informacion";
                        p_ocultar_informacion.Value = pLinea.ocultar_informacion;
                        p_ocultar_informacion.Direction = ParameterDirection.Input;
                        p_ocultar_informacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_ocultar_informacion);

                        DbParameter p_maneja_causacion = cmdTransaccionFactory.CreateParameter();
                        p_maneja_causacion.ParameterName = "p_maneja_causacion";
                        p_maneja_causacion.Value = pLinea.maneja_causacion;
                        p_maneja_causacion.Direction = ParameterDirection.Input;
                        p_maneja_causacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_maneja_causacion);

                        DbParameter p_nogenerarvacaciones = cmdTransaccionFactory.CreateParameter();
                        p_nogenerarvacaciones.ParameterName = "p_nogenerarvacaciones";
                        p_nogenerarvacaciones.Value = pLinea.no_generar_vacaciones;
                        p_nogenerarvacaciones.Direction = ParameterDirection.Input;
                        p_nogenerarvacaciones.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_nogenerarvacaciones);

                        DbParameter p_servicio_telefonia = cmdTransaccionFactory.CreateParameter();
                        p_servicio_telefonia.ParameterName = "p_servicio_telefonia";
                        p_servicio_telefonia.Value = pLinea.servicio_telefonia;
                        p_servicio_telefonia.Direction = ParameterDirection.Input;
                        p_servicio_telefonia.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_servicio_telefonia);

                        DbParameter p_maneja_retirados = cmdTransaccionFactory.CreateParameter();
                        p_maneja_retirados.ParameterName = "p_MANEJA_RETIRADOS";
                        p_maneja_retirados.Value = pLinea.maneja_retirados;
                        p_maneja_retirados.Direction = ParameterDirection.Input;
                        p_maneja_retirados.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_maneja_retirados);

                        DbParameter p_oficina_virtual = cmdTransaccionFactory.CreateParameter();
                        p_oficina_virtual.ParameterName = "P_OFICINA_VIRTUAL";
                        p_oficina_virtual.Value = pLinea.oficinaVirtual;
                        p_oficina_virtual.Direction = ParameterDirection.Input;
                        p_oficina_virtual.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_oficina_virtual);

                        DbParameter p_enlace = cmdTransaccionFactory.CreateParameter();
                        p_enlace.ParameterName = "P_ENLACE";
                        p_enlace.Value = pLinea.enlace;
                        p_enlace.Direction = ParameterDirection.Input;
                        p_enlace.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_enlace);

                        connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SER_LINEASSERV_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pLinea.cod_linea_servicio = Convert.ToString(pcod_linea_servicio.Value);

                        /*******************ACTUALIZO CON LA IMAGEN Y EL BANNER*********************/
                        DAImagenes = new ImagenesORAData();
                        if (foto != null || banner != null)
                            DAImagenes.imagenServicio(pLinea.cod_linea_servicio, pLinea.tipo_servicio, foto, banner, pUsuario);

                        return pLinea;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaServiciosData", "ModificarLineaServicio", ex);
                        return null;
                    }
                }
            }
        }
        public List<LineaServicios> ListarLineaServicios(LineaServicios pLinea, Usuario pUsuario, string filtro)
        {
            DbDataReader resultado;
            List<LineaServicios> lstServicio = new List<LineaServicios>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select l.cod_linea_servicio,case tipo_servicio when 1 then 'Medicina Prepagada' when 2 then 'Planes Exequiales' "
                                        + "when 3 then 'Seguros' when 4 then 'Otros' when 5 then 'Orden de Servicios' end as nomtiposervicio,l.nombre, identificacion_proveedor,nombre_proveedor, "
                                        + "(select p.descripcion from periodicidad p where p.COD_PERIODICIDAD = l.codperiodo_renovacion) as PeriodoRenov, "
                                        + "(select x.descripcion from periodicidad x where x.COD_PERIODICIDAD = l.codperiodo_pago) as PeriodoPago,l.fecha_pago_proveedor, "
                                        + "l.numero_beneficiarios, case l.cobra_interes when 1 then 'SI' when 0 then 'NO' end as CobraInteres,l.tasa_interes, "
                                        + "t.nombre as tipotasa, l.foto as foto, l.oficina_virtual,  l.MAXIMO_VALOR, l.MAXIMO_PLAZO, l.ENLACE, l.BANNER "
                                        + "from lineasservicios l "
                                        + "left join tipotasa t on t.cod_tipo_tasa = l.tipo_tasa where 1=1 " + filtro + " ORDER BY L.Nombre desc ";

                        connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            LineaServicios entidad = new LineaServicios();
                            if (resultado["COD_LINEA_SERVICIO"] != DBNull.Value) entidad.cod_linea_servicio = Convert.ToString(resultado["COD_LINEA_SERVICIO"]);
                            if (resultado["NOMTIPOSERVICIO"] != DBNull.Value) entidad.nomtiposervicio = Convert.ToString(resultado["NOMTIPOSERVICIO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["IDENTIFICACION_PROVEEDOR"] != DBNull.Value) entidad.identificacion_proveedor = Convert.ToString(resultado["IDENTIFICACION_PROVEEDOR"]);
                            if (resultado["NOMBRE_PROVEEDOR"] != DBNull.Value) entidad.nombre_proveedor = Convert.ToString(resultado["NOMBRE_PROVEEDOR"]);
                            if (resultado["PERIODORENOV"] != DBNull.Value) entidad.periodorenov = Convert.ToString(resultado["PERIODORENOV"]);
                            if (resultado["PERIODOPAGO"] != DBNull.Value) entidad.periodopago = Convert.ToString(resultado["PERIODOPAGO"]);
                            if (resultado["FECHA_PAGO_PROVEEDOR"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA_PAGO_PROVEEDOR"]);
                            if (resultado["NUMERO_BENEFICIARIOS"] != DBNull.Value) entidad.numero_beneficiarios = Convert.ToInt32(resultado["NUMERO_BENEFICIARIOS"]);
                            if (resultado["COBRAINTERES"] != DBNull.Value) entidad.cobrainteres = Convert.ToString(resultado["COBRAINTERES"]);
                            if (resultado["TASA_INTERES"] != DBNull.Value) entidad.tasa_interes = Convert.ToDecimal(resultado["TASA_INTERES"]);
                            if (resultado["TIPOTASA"] != DBNull.Value) entidad.nomtipotasa = Convert.ToString(resultado["TIPOTASA"]);
                            if (resultado["FOTO"] != DBNull.Value) entidad.foto = (byte[])resultado["FOTO"];
                            if (resultado["MAXIMO_VALOR"] != DBNull.Value) entidad.maximo_valor = Convert.ToString(resultado["MAXIMO_VALOR"]);
                            if (resultado["MAXIMO_PLAZO"] != DBNull.Value) entidad.maximo_plazo = Convert.ToString(resultado["MAXIMO_PLAZO"]);
                            if (resultado["ENLACE"] != DBNull.Value) entidad.enlace = Convert.ToString(resultado["ENLACE"]);
                            if (resultado["BANNER"] != DBNull.Value) entidad.banner = (byte[])resultado["BANNER"];

                            lstServicio.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstServicio;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaServiciosData", "ListarLineaServicios", ex);
                        return null;
                    }
                }
            }
        }

        public void EliminarLineaServicio(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_linea_servicio = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_servicio.ParameterName = "p_cod_linea_servicio";
                        pcod_linea_servicio.Value = pId;
                        pcod_linea_servicio.Direction = ParameterDirection.Input;
                        pcod_linea_servicio.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_servicio);

                        connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SER_LINEASSERV_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaServiciosData", "EliminarLineaServicio", ex);
                    }
                }
            }
        }

        public LineaServicios ConsultarLineaSERVICIO(string pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            LineaServicios entidad = new LineaServicios();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select l.*, p.cod_persona as cod_proveedor from lineasservicios l 
                                    left join persona p on l.identificacion_proveedor = p.identificacion
                                    where l.cod_linea_servicio = '" + pId.ToString() + "'";
                        connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COD_LINEA_SERVICIO"] != DBNull.Value) entidad.cod_linea_servicio = Convert.ToString(resultado["COD_LINEA_SERVICIO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["TIPO_SERVICIO"] != DBNull.Value) entidad.tipo_servicio = Convert.ToInt32(resultado["TIPO_SERVICIO"]);
                            if (resultado["IDENTIFICACION_PROVEEDOR"] != DBNull.Value) entidad.identificacion_proveedor = Convert.ToString(resultado["IDENTIFICACION_PROVEEDOR"]);
                            if (resultado["NOMBRE_PROVEEDOR"] != DBNull.Value) entidad.nombre_proveedor = Convert.ToString(resultado["NOMBRE_PROVEEDOR"]);
                            if (resultado["CODPERIODO_RENOVACION"] != DBNull.Value) entidad.codperiodo_renovacion = Convert.ToInt32(resultado["CODPERIODO_RENOVACION"]);
                            if (resultado["CODPERIODO_PAGO"] != DBNull.Value) entidad.codperiodo_pago = Convert.ToInt32(resultado["CODPERIODO_PAGO"]);
                            if (resultado["CODPERIOCIDAD"] != DBNull.Value) entidad.codperiocidad = Convert.ToInt32(resultado["CODPERIOCIDAD"]);
                            if (resultado["NUMERO_BENEFICIARIOS"] != DBNull.Value) entidad.numero_beneficiarios = Convert.ToInt32(resultado["NUMERO_BENEFICIARIOS"]);
                            if (resultado["COBRA_INTERES"] != DBNull.Value) entidad.cobra_interes = Convert.ToInt32(resultado["COBRA_INTERES"]);
                            if (resultado["TASA_INTERES"] != DBNull.Value) entidad.tasa_interes = Convert.ToDecimal(resultado["TASA_INTERES"]);
                            if (resultado["TIPO_TASA"] != DBNull.Value) entidad.tipo_tasa = Convert.ToInt32(resultado["TIPO_TASA"]);
                            if (resultado["FECHA_PAGO_PROVEEDOR"] != DBNull.Value) entidad.fecha_pago_proveedor = Convert.ToDateTime(resultado["FECHA_PAGO_PROVEEDOR"]);
                            if (resultado["REQUIEREBENEFICIARIOS"] != DBNull.Value) entidad.requierebeneficiarios = Convert.ToInt32(resultado["REQUIEREBENEFICIARIOS"]);
                            if (resultado["NO_REQUIERE_APROBACION"] != DBNull.Value) entidad.no_requiere_aprobacion = Convert.ToInt32(resultado["NO_REQUIERE_APROBACION"]);
                            if (resultado["NUMERO_SERVICIOS"] != DBNull.Value) entidad.numero_servicios = Convert.ToInt32(resultado["NUMERO_SERVICIOS"]);
                            if (resultado["MAXIMO_PLAZO"] != DBNull.Value) entidad.maximo_plazo = Convert.ToString(resultado["MAXIMO_PLAZO"]);
                            if (resultado["MAXIMO_VALOR"] != DBNull.Value) entidad.maximo_valor = Convert.ToString(resultado["MAXIMO_VALOR"]);
                            if (resultado["TIPO_CUOTA"] != DBNull.Value) entidad.tipo_cuota = Convert.ToInt32(resultado["TIPO_CUOTA"]);
                            if (resultado["ORDEN"] != DBNull.Value) entidad.orden = Convert.ToInt32(resultado["ORDEN"]);
                            if (resultado["ocultar_informacion"] != DBNull.Value) entidad.ocultar_informacion = Convert.ToInt32(resultado["ocultar_informacion"]);
                            if (resultado["COD_PROVEEDOR"] != DBNull.Value) entidad.cod_proveedor = Convert.ToInt64(resultado["COD_PROVEEDOR"]);
                            if (resultado["MANEJA_CAUSACION"] != DBNull.Value) entidad.maneja_causacion = Convert.ToInt32(resultado["MANEJA_CAUSACION"]);
                            if (resultado["NOGENERARVACACIONES"] != DBNull.Value) entidad.no_generar_vacaciones = Convert.ToInt32(resultado["NOGENERARVACACIONES"]);
                            if (resultado["SERVICIO_TELEFONIA"] != DBNull.Value) entidad.servicio_telefonia = Convert.ToInt32(resultado["SERVICIO_TELEFONIA"]);
                            if (resultado["MANEJA_RETIRADOS"] != DBNull.Value) entidad.maneja_retirados = Convert.ToInt32(resultado["MANEJA_RETIRADOS"]);
                            if (resultado["FOTO"] != DBNull.Value) entidad.foto = (byte[])resultado["FOTO"];
                            if (resultado["OFICINA_VIRTUAL"] != DBNull.Value) entidad.oficinaVirtual = Convert.ToInt32(resultado["OFICINA_VIRTUAL"]);
                            if (resultado["ENLACE"] != DBNull.Value) entidad.enlace = Convert.ToString(resultado["ENLACE"]);
                            if (resultado["BANNER"] != DBNull.Value) entidad.banner = (byte[])resultado["BANNER"];
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaServiciosData", "ConsultarLineaSERVICIO", ex);
                        return null;
                    }
                }
            }
        }

        public planservicios CrearPlanServicio(planservicios pPlan, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_plan_servicio = cmdTransaccionFactory.CreateParameter();
                        pcod_plan_servicio.ParameterName = "p_cod_plan_servicio";
                        pcod_plan_servicio.Value = pPlan.cod_plan_servicio;
                        pcod_plan_servicio.Direction = ParameterDirection.Input;
                        pcod_plan_servicio.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_plan_servicio);

                        DbParameter pcod_linea_servicio = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_servicio.ParameterName = "p_cod_linea_servicio";
                        pcod_linea_servicio.Value = pPlan.cod_linea_servicio;
                        pcod_linea_servicio.Direction = ParameterDirection.Input;
                        pcod_linea_servicio.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_servicio);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "p_nombre";
                        pnombre.Value = pPlan.nombre;
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        DbParameter pnumero_usuarios = cmdTransaccionFactory.CreateParameter();
                        pnumero_usuarios.ParameterName = "p_numero_usuarios";
                        pnumero_usuarios.Value = pPlan.numero_usuarios;
                        pnumero_usuarios.Direction = ParameterDirection.Input;
                        pnumero_usuarios.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_usuarios);

                        DbParameter pedad_minima = cmdTransaccionFactory.CreateParameter();
                        pedad_minima.ParameterName = "p_edad_minima";
                        pedad_minima.Value = pPlan.edad_minima;
                        pedad_minima.Direction = ParameterDirection.Input;
                        pedad_minima.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pedad_minima);

                        DbParameter pedad_maxima = cmdTransaccionFactory.CreateParameter();
                        pedad_maxima.ParameterName = "p_edad_maxima";
                        pedad_maxima.Value = pPlan.edad_maxima;
                        pedad_maxima.Direction = ParameterDirection.Input;
                        pedad_maxima.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pedad_maxima);

                        DbParameter pcodgrupo_familiar = cmdTransaccionFactory.CreateParameter();
                        pcodgrupo_familiar.ParameterName = "p_codgrupo_familiar";
                        if (pPlan.codgrupo_familiar != null && pPlan.codgrupo_familiar != 0) pcodgrupo_familiar.Value = pPlan.codgrupo_familiar; else pcodgrupo_familiar.Value = DBNull.Value;
                        pcodgrupo_familiar.Direction = ParameterDirection.Input;
                        pcodgrupo_familiar.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodgrupo_familiar);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        pvalor.Value = pPlan.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SER_PLANSERVIC_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pPlan.cod_plan_servicio = Convert.ToString(pcod_plan_servicio.Value);
                        return pPlan;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaServiciosData", "CrearPlanServicio", ex);
                        return null;
                    }
                }
            }
        }

        public planservicios ModificarPlanServicio(planservicios pPlan, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_plan_servicio = cmdTransaccionFactory.CreateParameter();
                        pcod_plan_servicio.ParameterName = "p_cod_plan_servicio";
                        pcod_plan_servicio.Value = pPlan.cod_plan_servicio;
                        pcod_plan_servicio.Direction = ParameterDirection.Input;
                        pcod_plan_servicio.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_plan_servicio);

                        DbParameter pcod_linea_servicio = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_servicio.ParameterName = "p_cod_linea_servicio";
                        pcod_linea_servicio.Value = pPlan.cod_linea_servicio;
                        pcod_linea_servicio.Direction = ParameterDirection.Input;
                        pcod_linea_servicio.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_servicio);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "p_nombre";
                        pnombre.Value = pPlan.nombre;
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        DbParameter pnumero_usuarios = cmdTransaccionFactory.CreateParameter();
                        pnumero_usuarios.ParameterName = "p_numero_usuarios";
                        pnumero_usuarios.Value = pPlan.numero_usuarios;
                        pnumero_usuarios.Direction = ParameterDirection.Input;
                        pnumero_usuarios.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_usuarios);

                        DbParameter pedad_minima = cmdTransaccionFactory.CreateParameter();
                        pedad_minima.ParameterName = "p_edad_minima";
                        pedad_minima.Value = pPlan.edad_minima;
                        pedad_minima.Direction = ParameterDirection.Input;
                        pedad_minima.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pedad_minima);

                        DbParameter pedad_maxima = cmdTransaccionFactory.CreateParameter();
                        pedad_maxima.ParameterName = "p_edad_maxima";
                        pedad_maxima.Value = pPlan.edad_maxima;
                        pedad_maxima.Direction = ParameterDirection.Input;
                        pedad_maxima.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pedad_maxima);

                        DbParameter pcodgrupo_familiar = cmdTransaccionFactory.CreateParameter();
                        pcodgrupo_familiar.ParameterName = "p_codgrupo_familiar";
                        if (pPlan.codgrupo_familiar != null && pPlan.codgrupo_familiar != 0) pcodgrupo_familiar.Value = pPlan.codgrupo_familiar; else pcodgrupo_familiar.Value = DBNull.Value;
                        pcodgrupo_familiar.Direction = ParameterDirection.Input;
                        pcodgrupo_familiar.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodgrupo_familiar);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        pvalor.Value = pPlan.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SER_PLANSERVIC_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pPlan;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaServiciosData", "ModificarPlanServicio", ex);
                        return null;
                    }
                }
            }
        }

        public void EliminarDETALLELineaSERVICIO(string pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_plan_servicio = cmdTransaccionFactory.CreateParameter();
                        pcod_plan_servicio.ParameterName = "p_cod_plan_servicio";
                        pcod_plan_servicio.Value = pId;
                        pcod_plan_servicio.Direction = ParameterDirection.Input;
                        pcod_plan_servicio.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_plan_servicio);

                        connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SER_PLANSERVIC_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaServiciosData", "EliminarDETALLELineaSERVICIO", ex);
                    }
                }
            }
        }

        public List<planservicios> ConsultarDETALLELineaSERVICIO(string pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<planservicios> LstDetalle = new List<planservicios>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM planservicios where COD_LINEA_SERVICIO = '" + pId.ToString() + "'";
                        connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            planservicios entidad = new planservicios();
                            if (resultado["COD_PLAN_SERVICIO"] != DBNull.Value) entidad.cod_plan_servicio = Convert.ToString(resultado["COD_PLAN_SERVICIO"]);
                            if (resultado["COD_LINEA_SERVICIO"] != DBNull.Value) entidad.cod_linea_servicio = Convert.ToString(resultado["COD_LINEA_SERVICIO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["NUMERO_USUARIOS"] != DBNull.Value) entidad.numero_usuarios = Convert.ToInt32(resultado["NUMERO_USUARIOS"]);
                            if (resultado["EDAD_MINIMA"] != DBNull.Value) entidad.edad_minima = Convert.ToInt32(resultado["EDAD_MINIMA"]);
                            if (resultado["EDAD_MAXIMA"] != DBNull.Value) entidad.edad_maxima = Convert.ToInt32(resultado["EDAD_MAXIMA"]);
                            if (resultado["CODGRUPO_FAMILIAR"] != DBNull.Value) entidad.codgrupo_familiar = Convert.ToInt32(resultado["CODGRUPO_FAMILIAR"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            LstDetalle.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return LstDetalle;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaServiciosData", "ConsultarDETALLELineaSERVICIO", ex);
                        return null;
                    }
                }
            }
        }

        public Int64 ObtenerUltimoCodigo(Usuario pUsuario)
        {
            Int64 resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select max(cod_plan_servicio) from planservicios";

                        connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        try
                        {
                            resultado = Convert.ToInt64(cmdTransaccionFactory.ExecuteScalar());
                        }
                        catch
                        {
                            resultado = 1;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return resultado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActivoFijoData", "ObtenerSiguienteCodigo", ex);
                        return -1;
                    }
                }
            }
        }

        #region DESTINCACION
        public void EliminarDestinacion_Linea(string pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"delete from LINEASER_DESTINACION where COD_LINEA_SERVICIO = '" + pId.ToString() + "'";
                        connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasServicioData", "EliminarDestinacion_Linea", ex);
                    }
                }
            }
        }

        public LineaServicios CrearDestino_Linea(LineaServicios pDestino, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter P_COD_LINEA_SERVICIO = cmdTransaccionFactory.CreateParameter();
                        P_COD_LINEA_SERVICIO.ParameterName = "P_COD_LINEA_SERVICIO";
                        P_COD_LINEA_SERVICIO.Value = pDestino.cod_linea_servicio;
                        P_COD_LINEA_SERVICIO.Direction = ParameterDirection.Input;
                        P_COD_LINEA_SERVICIO.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_COD_LINEA_SERVICIO);

                        DbParameter P_COD_DESTINO = cmdTransaccionFactory.CreateParameter();
                        P_COD_DESTINO.ParameterName = "P_COD_DESTINO";
                        P_COD_DESTINO.Value = pDestino.cod_destino;
                        P_COD_DESTINO.Direction = ParameterDirection.Input;
                        P_COD_DESTINO.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_COD_DESTINO);

                        connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SER_DESTINO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pDestino;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasServicioData", "CrearDestino_Linea", ex);
                        return null;
                    }
                }
            }
        }

        public List<LineaServicios> ConsultarDestinacion_Linea(string pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<LineaServicios> lstData = new List<LineaServicios>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select * from LINEASER_DESTINACION where COD_LINEA_SERVICIO ='" + pId.ToString() + "'"
                                         + " ORDER  BY 1";
                        connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            LineaServicios entidad = new LineaServicios();
                            if (resultado["COD_DESTINO"] != DBNull.Value) entidad.cod_destino = Convert.ToInt32(resultado["COD_DESTINO"]);
                            lstData.Add(entidad);
                            if (entidad.cod_destino != 0)
                                entidad.seleccionar = 1;
                            else
                                entidad.seleccionar = 0;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstData;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasServiciosData", "Consultardestinacion_Linea", ex);
                        return null;
                    }
                }
            }
        }

        public LineaServ_Destinacion consultaDestinacionservicio(string pId, string pIdLin, Usuario pUsuario)
        {
            DbDataReader resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        LineaServ_Destinacion entidad = new LineaServ_Destinacion();

                        string sql = @"SELECT L.* FROM LINEASER_DESTINACION L where L.COD_LINEA_SERVICIO = " + pIdLin.ToString() + " and L.COD_DESTINO = " + pId.ToString();
                        connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_LINEA_SERVICIO"] != DBNull.Value) entidad.cod_linea_servicio = Convert.ToInt32(resultado["COD_LINEA_SERVICIO"]);
                            if (resultado["COD_DESTINO"] != DBNull.Value) entidad.cod_destino = Convert.ToInt32(resultado["COD_DESTINO"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasServiciosData", "consultaDestinacionservicio", ex);
                        return null;
                    }
                }
            }
        }
        #endregion


    }
}
