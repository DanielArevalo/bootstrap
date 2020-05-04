using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Seguridad.Entities;

namespace Xpinn.Seguridad.Data
{
    public class EmpresaComunData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public EmpresaComunData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
        public Empresa CrearEmpresa(Empresa pEmpresa, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_empresa = cmdTransaccionFactory.CreateParameter();
                        pcod_empresa.ParameterName = "p_cod_empresa";
                        pcod_empresa.Value = pEmpresa.cod_empresa;
                        pcod_empresa.Direction = ParameterDirection.Output;
                        pcod_empresa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_empresa);

                        DbParameter pnit = cmdTransaccionFactory.CreateParameter();
                        pnit.ParameterName = "p_nit";
                        pnit.Value = pEmpresa.nit;
                        pnit.Direction = ParameterDirection.Input;
                        pnit.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnit);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "p_nombre";
                        if (pEmpresa.nombre == null)
                            pnombre.Value = DBNull.Value;
                        else
                            pnombre.Value = pEmpresa.nombre;
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        DbParameter psigla = cmdTransaccionFactory.CreateParameter();
                        psigla.ParameterName = "p_sigla";
                        if (pEmpresa.sigla == null)
                            psigla.Value = DBNull.Value;
                        else
                            psigla.Value = pEmpresa.sigla;
                        psigla.Direction = ParameterDirection.Input;
                        psigla.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(psigla);

                        DbParameter pdireccion = cmdTransaccionFactory.CreateParameter();
                        pdireccion.ParameterName = "p_direccion";
                        if (pEmpresa.direccion == null)
                            pdireccion.Value = DBNull.Value;
                        else
                            pdireccion.Value = pEmpresa.direccion;
                        pdireccion.Direction = ParameterDirection.Input;
                        pdireccion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdireccion);

                        DbParameter ptelefono = cmdTransaccionFactory.CreateParameter();
                        ptelefono.ParameterName = "p_telefono";
                        if (pEmpresa.telefono == null)
                            ptelefono.Value = DBNull.Value;
                        else
                            ptelefono.Value = pEmpresa.telefono;
                        ptelefono.Direction = ParameterDirection.Input;
                        ptelefono.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptelefono);

                        DbParameter pfecha_constitución = cmdTransaccionFactory.CreateParameter();
                        pfecha_constitución.ParameterName = "p_fecha_constitución";
                        if (pEmpresa.fecha_constitución == null)
                            pfecha_constitución.Value = DBNull.Value;
                        else
                            pfecha_constitución.Value = pEmpresa.fecha_constitución;
                        pfecha_constitución.Direction = ParameterDirection.Input;
                        pfecha_constitución.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_constitución);

                        DbParameter pciudad = cmdTransaccionFactory.CreateParameter();
                        pciudad.ParameterName = "p_ciudad";
                        if (pEmpresa.ciudad == null)
                            pciudad.Value = DBNull.Value;
                        else
                            pciudad.Value = pEmpresa.ciudad;
                        pciudad.Direction = ParameterDirection.Input;
                        pciudad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pciudad);

                        DbParameter pe_mail = cmdTransaccionFactory.CreateParameter();
                        pe_mail.ParameterName = "p_e_mail";
                        if (pEmpresa.e_mail == null)
                            pe_mail.Value = DBNull.Value;
                        else
                            pe_mail.Value = pEmpresa.e_mail;
                        pe_mail.Direction = ParameterDirection.Input;
                        pe_mail.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pe_mail);

                        DbParameter prepresentante_legal = cmdTransaccionFactory.CreateParameter();
                        prepresentante_legal.ParameterName = "p_representante_legal";
                        if (pEmpresa.representante_legal == null)
                            prepresentante_legal.Value = DBNull.Value;
                        else
                            prepresentante_legal.Value = pEmpresa.representante_legal;
                        prepresentante_legal.Direction = ParameterDirection.Input;
                        prepresentante_legal.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(prepresentante_legal);

                        DbParameter pcontador = cmdTransaccionFactory.CreateParameter();
                        pcontador.ParameterName = "p_contador";
                        if (pEmpresa.contador == null)
                            pcontador.Value = DBNull.Value;
                        else
                            pcontador.Value = pEmpresa.contador;
                        pcontador.Direction = ParameterDirection.Input;
                        pcontador.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcontador);

                        DbParameter ptarjeta_contador = cmdTransaccionFactory.CreateParameter();
                        ptarjeta_contador.ParameterName = "p_tarjeta_contador";
                        if (pEmpresa.tarjeta_contador == null)
                            ptarjeta_contador.Value = DBNull.Value;
                        else
                            ptarjeta_contador.Value = pEmpresa.tarjeta_contador;
                        ptarjeta_contador.Direction = ParameterDirection.Input;
                        ptarjeta_contador.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptarjeta_contador);

                        DbParameter ptipo = cmdTransaccionFactory.CreateParameter();
                        ptipo.ParameterName = "p_tipo";
                        if (pEmpresa.tipo == null)
                            ptipo.Value = DBNull.Value;
                        else
                            ptipo.Value = pEmpresa.tipo;
                        ptipo.Direction = ParameterDirection.Input;
                        ptipo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ptipo);

                        DbParameter preporte_egreso = cmdTransaccionFactory.CreateParameter();
                        preporte_egreso.ParameterName = "p_reporte_egreso";
                        if (pEmpresa.reporte_egreso == null)
                            preporte_egreso.Value = DBNull.Value;
                        else
                            preporte_egreso.Value = pEmpresa.reporte_egreso;
                        preporte_egreso.Direction = ParameterDirection.Input;
                        preporte_egreso.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(preporte_egreso);

                        DbParameter previsor = cmdTransaccionFactory.CreateParameter();
                        previsor.ParameterName = "p_revisor";
                        if (pEmpresa.revisor == null)
                            previsor.Value = DBNull.Value;
                        else
                            previsor.Value = pEmpresa.revisor;
                        previsor.Direction = ParameterDirection.Input;
                        previsor.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(previsor);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        if (pEmpresa.cod_persona == null)
                            pcod_persona.Value = DBNull.Value;
                        else
                            pcod_persona.Value = pEmpresa.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter preporte_ingreso = cmdTransaccionFactory.CreateParameter();
                        preporte_ingreso.ParameterName = "p_reporte_ingreso";
                        if (pEmpresa.reporte_ingreso == null)
                            preporte_ingreso.Value = DBNull.Value;
                        else
                            preporte_ingreso.Value = pEmpresa.reporte_ingreso;
                        preporte_ingreso.Direction = ParameterDirection.Input;
                        preporte_ingreso.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(preporte_ingreso);

                        DbParameter pcod_uiaf = cmdTransaccionFactory.CreateParameter();
                        pcod_uiaf.ParameterName = "p_cod_uiaf";
                        if (pEmpresa.cod_uiaf == null)
                            pcod_uiaf.Value = DBNull.Value;
                        else
                            pcod_uiaf.Value = pEmpresa.cod_uiaf;
                        pcod_uiaf.Direction = ParameterDirection.Input;
                        pcod_uiaf.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_uiaf);

                        DbParameter pclavecorreo = cmdTransaccionFactory.CreateParameter();
                        pclavecorreo.ParameterName = "p_clavecorreo";
                        if (pEmpresa.clavecorreo == null)
                            pclavecorreo.Value = DBNull.Value;
                        else
                            pclavecorreo.Value = pEmpresa.clavecorreo;
                        pclavecorreo.Direction = ParameterDirection.Input;
                        pclavecorreo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pclavecorreo);

                        DbParameter pmaneja_sincronizacion = cmdTransaccionFactory.CreateParameter();
                        pmaneja_sincronizacion.ParameterName = "p_maneja_sincronizacion";
                        if (pEmpresa.maneja_sincronizacion == null)
                            pmaneja_sincronizacion.Value = DBNull.Value;
                        else
                            pmaneja_sincronizacion.Value = pEmpresa.maneja_sincronizacion;
                        pmaneja_sincronizacion.Direction = ParameterDirection.Input;
                        pmaneja_sincronizacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pmaneja_sincronizacion);

                        DbParameter p_tipo_empresa = cmdTransaccionFactory.CreateParameter();
                        p_tipo_empresa.ParameterName = "p_tipo_empresa";
                        if (pEmpresa.tipo_empresa == null)
                            p_tipo_empresa.Value = DBNull.Value;
                        else
                            p_tipo_empresa.Value = pEmpresa.tipo_empresa;
                        p_tipo_empresa.Direction = ParameterDirection.Input;
                        p_tipo_empresa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_tipo_empresa);


                        DbParameter pdesc_regimen = cmdTransaccionFactory.CreateParameter();
                        pdesc_regimen.ParameterName = "pdesc_regimen";
                        if (pEmpresa.desc_regimen == null)
                            pdesc_regimen.Value = DBNull.Value;
                        else
                            pdesc_regimen.Value = pEmpresa.desc_regimen;
                        pdesc_regimen.Direction = ParameterDirection.Input;
                        pdesc_regimen.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdesc_regimen);

                        DbParameter presol_facturacion = cmdTransaccionFactory.CreateParameter();
                        presol_facturacion.ParameterName = "presol_facturacion";
                        if (pEmpresa.resol_facturacion == null)
                            presol_facturacion.Value = DBNull.Value;
                        else
                            presol_facturacion.Value = pEmpresa.resol_facturacion;
                        presol_facturacion.Direction = ParameterDirection.Input;
                        presol_facturacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(presol_facturacion);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SEG_EMPRESA_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pEmpresa.cod_empresa = pcod_empresa.Value != DBNull.Value ? Convert.ToInt32(pcod_empresa.Value) : 0;

                        return pEmpresa;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpresaData", "CrearEmpresa", ex);
                        return null;
                    }
                }
            }
        }

        public Empresa ConsultarEmpresa(Usuario usuario)
        {
            DbDataReader resultado;
            Empresa entidad = new Empresa();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Empresa ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt32(resultado["COD_EMPRESA"]);
                            if (resultado["NIT"] != DBNull.Value) entidad.nit = Convert.ToString(resultado["NIT"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["SIGLA"] != DBNull.Value) entidad.sigla = Convert.ToString(resultado["SIGLA"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["FECHA_CONSTITUCION"] != DBNull.Value) entidad.fecha_constitución = Convert.ToDateTime(resultado["FECHA_CONSTITUCION"]);
                            if (resultado["CIUDAD"] != DBNull.Value) entidad.ciudad = Convert.ToInt32(resultado["CIUDAD"]);
                            if (resultado["E_MAIL"] != DBNull.Value) entidad.e_mail = Convert.ToString(resultado["E_MAIL"]);
                            if (resultado["REPRESENTANTE_LEGAL"] != DBNull.Value) entidad.representante_legal = Convert.ToString(resultado["REPRESENTANTE_LEGAL"]);
                            if (resultado["CONTADOR"] != DBNull.Value) entidad.contador = Convert.ToString(resultado["CONTADOR"]);
                            if (resultado["TARJETA_CONTADOR"] != DBNull.Value) entidad.tarjeta_contador = Convert.ToString(resultado["TARJETA_CONTADOR"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToDecimal(resultado["TIPO"]);
                            if (resultado["REPORTE_EGRESO"] != DBNull.Value) entidad.reporte_egreso = Convert.ToString(resultado["REPORTE_EGRESO"]);
                            if (resultado["REVISOR"] != DBNull.Value) entidad.revisor = Convert.ToString(resultado["REVISOR"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["REPORTE_INGRESO"] != DBNull.Value) entidad.reporte_ingreso = Convert.ToString(resultado["REPORTE_INGRESO"]);
                            if (resultado["LOGOEMPRESA"] != DBNull.Value) entidad.logoempresa_bytes = (byte[])(resultado["LOGOEMPRESA"]);
                            if (resultado["COD_UIAF"] != DBNull.Value) entidad.cod_uiaf = Convert.ToString(resultado["COD_UIAF"]);
                            if (resultado["CLAVECORREO"] != DBNull.Value) entidad.clavecorreo = Convert.ToString(resultado["CLAVECORREO"]);
                            if (resultado["MANEJA_SINCRONIZACION"] != DBNull.Value) entidad.maneja_sincronizacion = Convert.ToInt32(resultado["MANEJA_SINCRONIZACION"]);
                            if (resultado["tipo_empresa"] != DBNull.Value) entidad.tipo_empresa = Convert.ToInt32(resultado["tipo_empresa"]);

                            if (resultado["DESCRIPCION_REGIMEN"] != DBNull.Value) entidad.desc_regimen = Convert.ToString(resultado["DESCRIPCION_REGIMEN"]);
                            if (resultado["RESOLUCION_FACTURACION"] != DBNull.Value) entidad.resol_facturacion = Convert.ToString(resultado["RESOLUCION_FACTURACION"]);

                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpresaData", "ConsultarEmpresa", ex);
                        return null;
                    }
                }
            }
        }

        public Empresa ModificarEmpresa(Empresa pEmpresa, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_empresa = cmdTransaccionFactory.CreateParameter();
                        pcod_empresa.ParameterName = "p_cod_empresa";
                        pcod_empresa.Value = pEmpresa.cod_empresa;
                        pcod_empresa.Direction = ParameterDirection.Input;
                        pcod_empresa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_empresa);

                        DbParameter pnit = cmdTransaccionFactory.CreateParameter();
                        pnit.ParameterName = "p_nit";
                        pnit.Value = pEmpresa.nit;
                        pnit.Direction = ParameterDirection.Input;
                        pnit.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnit);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "p_nombre";
                        if (pEmpresa.nombre == null)
                            pnombre.Value = DBNull.Value;
                        else
                            pnombre.Value = pEmpresa.nombre;
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        DbParameter psigla = cmdTransaccionFactory.CreateParameter();
                        psigla.ParameterName = "p_sigla";
                        if (pEmpresa.sigla == null)
                            psigla.Value = DBNull.Value;
                        else
                            psigla.Value = pEmpresa.sigla;
                        psigla.Direction = ParameterDirection.Input;
                        psigla.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(psigla);

                        DbParameter pdireccion = cmdTransaccionFactory.CreateParameter();
                        pdireccion.ParameterName = "p_direccion";
                        if (pEmpresa.direccion == null)
                            pdireccion.Value = DBNull.Value;
                        else
                            pdireccion.Value = pEmpresa.direccion;
                        pdireccion.Direction = ParameterDirection.Input;
                        pdireccion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdireccion);

                        DbParameter ptelefono = cmdTransaccionFactory.CreateParameter();
                        ptelefono.ParameterName = "p_telefono";
                        if (pEmpresa.telefono == null)
                            ptelefono.Value = DBNull.Value;
                        else
                            ptelefono.Value = pEmpresa.telefono;
                        ptelefono.Direction = ParameterDirection.Input;
                        ptelefono.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptelefono);

                        DbParameter pfecha_constitución = cmdTransaccionFactory.CreateParameter();
                        pfecha_constitución.ParameterName = "p_fecha_constitución";
                        if (pEmpresa.fecha_constitución == null)
                            pfecha_constitución.Value = DBNull.Value;
                        else
                            pfecha_constitución.Value = pEmpresa.fecha_constitución;
                        pfecha_constitución.Direction = ParameterDirection.Input;
                        pfecha_constitución.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_constitución);

                        DbParameter pciudad = cmdTransaccionFactory.CreateParameter();
                        pciudad.ParameterName = "p_ciudad";
                        if (pEmpresa.ciudad == null)
                            pciudad.Value = DBNull.Value;
                        else
                            pciudad.Value = pEmpresa.ciudad;
                        pciudad.Direction = ParameterDirection.Input;
                        pciudad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pciudad);

                        DbParameter pe_mail = cmdTransaccionFactory.CreateParameter();
                        pe_mail.ParameterName = "p_e_mail";
                        if (pEmpresa.e_mail == null)
                            pe_mail.Value = DBNull.Value;
                        else
                            pe_mail.Value = pEmpresa.e_mail;
                        pe_mail.Direction = ParameterDirection.Input;
                        pe_mail.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pe_mail);

                        DbParameter prepresentante_legal = cmdTransaccionFactory.CreateParameter();
                        prepresentante_legal.ParameterName = "p_representante_legal";
                        if (pEmpresa.representante_legal == null)
                            prepresentante_legal.Value = DBNull.Value;
                        else
                            prepresentante_legal.Value = pEmpresa.representante_legal;
                        prepresentante_legal.Direction = ParameterDirection.Input;
                        prepresentante_legal.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(prepresentante_legal);

                        DbParameter pcontador = cmdTransaccionFactory.CreateParameter();
                        pcontador.ParameterName = "p_contador";
                        if (pEmpresa.contador == null)
                            pcontador.Value = DBNull.Value;
                        else
                            pcontador.Value = pEmpresa.contador;
                        pcontador.Direction = ParameterDirection.Input;
                        pcontador.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcontador);

                        DbParameter ptarjeta_contador = cmdTransaccionFactory.CreateParameter();
                        ptarjeta_contador.ParameterName = "p_tarjeta_contador";
                        if (pEmpresa.tarjeta_contador == null)
                            ptarjeta_contador.Value = DBNull.Value;
                        else
                            ptarjeta_contador.Value = pEmpresa.tarjeta_contador;
                        ptarjeta_contador.Direction = ParameterDirection.Input;
                        ptarjeta_contador.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptarjeta_contador);

                        DbParameter ptipo = cmdTransaccionFactory.CreateParameter();
                        ptipo.ParameterName = "p_tipo";
                        if (pEmpresa.tipo == null)
                            ptipo.Value = DBNull.Value;
                        else
                            ptipo.Value = pEmpresa.tipo;
                        ptipo.Direction = ParameterDirection.Input;
                        ptipo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ptipo);

                        DbParameter preporte_egreso = cmdTransaccionFactory.CreateParameter();
                        preporte_egreso.ParameterName = "p_reporte_egreso";
                        if (pEmpresa.reporte_egreso == null)
                            preporte_egreso.Value = DBNull.Value;
                        else
                            preporte_egreso.Value = pEmpresa.reporte_egreso;
                        preporte_egreso.Direction = ParameterDirection.Input;
                        preporte_egreso.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(preporte_egreso);

                        DbParameter previsor = cmdTransaccionFactory.CreateParameter();
                        previsor.ParameterName = "p_revisor";
                        if (pEmpresa.revisor == null)
                            previsor.Value = DBNull.Value;
                        else
                            previsor.Value = pEmpresa.revisor;
                        previsor.Direction = ParameterDirection.Input;
                        previsor.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(previsor);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        if (pEmpresa.cod_persona == null)
                            pcod_persona.Value = DBNull.Value;
                        else
                            pcod_persona.Value = pEmpresa.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter preporte_ingreso = cmdTransaccionFactory.CreateParameter();
                        preporte_ingreso.ParameterName = "p_reporte_ingreso";
                        if (pEmpresa.reporte_ingreso == null)
                            preporte_ingreso.Value = DBNull.Value;
                        else
                            preporte_ingreso.Value = pEmpresa.reporte_ingreso;
                        preporte_ingreso.Direction = ParameterDirection.Input;
                        preporte_ingreso.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(preporte_ingreso);

                        DbParameter pcod_uiaf = cmdTransaccionFactory.CreateParameter();
                        pcod_uiaf.ParameterName = "p_cod_uiaf";
                        if (pEmpresa.cod_uiaf == null)
                            pcod_uiaf.Value = DBNull.Value;
                        else
                            pcod_uiaf.Value = pEmpresa.cod_uiaf;
                        pcod_uiaf.Direction = ParameterDirection.Input;
                        pcod_uiaf.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_uiaf);

                        DbParameter pclavecorreo = cmdTransaccionFactory.CreateParameter();
                        pclavecorreo.ParameterName = "p_clavecorreo";
                        if (pEmpresa.clavecorreo == null)
                            pclavecorreo.Value = DBNull.Value;
                        else
                            pclavecorreo.Value = pEmpresa.clavecorreo;
                        pclavecorreo.Direction = ParameterDirection.Input;
                        pclavecorreo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pclavecorreo);

                        DbParameter pmaneja_sincronizacion = cmdTransaccionFactory.CreateParameter();
                        pmaneja_sincronizacion.ParameterName = "p_maneja_sincronizacion";
                        if (pEmpresa.maneja_sincronizacion == null)
                            pmaneja_sincronizacion.Value = DBNull.Value;
                        else
                            pmaneja_sincronizacion.Value = pEmpresa.maneja_sincronizacion;
                        pmaneja_sincronizacion.Direction = ParameterDirection.Input;
                        pmaneja_sincronizacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pmaneja_sincronizacion);

                        DbParameter p_tipo_empresa = cmdTransaccionFactory.CreateParameter();
                        p_tipo_empresa.ParameterName = "p_tipo_empresa";
                        if (pEmpresa.tipo_empresa == null)
                            p_tipo_empresa.Value = DBNull.Value;
                        else
                            p_tipo_empresa.Value = pEmpresa.tipo_empresa;
                        p_tipo_empresa.Direction = ParameterDirection.Input;
                        p_tipo_empresa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_tipo_empresa);

                        DbParameter pdesc_regimen = cmdTransaccionFactory.CreateParameter();
                        pdesc_regimen.ParameterName = "pdesc_regimen";
                        if (pEmpresa.desc_regimen == null)
                            pdesc_regimen.Value = DBNull.Value;
                        else
                            pdesc_regimen.Value = pEmpresa.desc_regimen;
                        pdesc_regimen.Direction = ParameterDirection.Input;
                        pdesc_regimen.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdesc_regimen);

                        DbParameter presol_facturacion = cmdTransaccionFactory.CreateParameter();
                        presol_facturacion.ParameterName = "presol_facturacion";
                        if (pEmpresa.resol_facturacion == null)
                            presol_facturacion.Value = DBNull.Value;
                        else
                            presol_facturacion.Value = pEmpresa.resol_facturacion;
                        presol_facturacion.Direction = ParameterDirection.Input;
                        presol_facturacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(presol_facturacion);




                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SEG_EMPRESA_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pEmpresa;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpresaData", "ModificarEmpresa", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarEmpresa(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {   
                    try
                    {
                        Empresa pEmpresa = new Empresa();
                        pEmpresa = ConsultarEmpresa(pId, vUsuario);

                        DbParameter pcod_empresa = cmdTransaccionFactory.CreateParameter();
                        pcod_empresa.ParameterName = "p_cod_empresa";
                        pcod_empresa.Value = pEmpresa.cod_empresa;
                        pcod_empresa.Direction = ParameterDirection.Input;
                        pcod_empresa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_empresa);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_COM_EMPRESA_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpresaData", "EliminarEmpresa", ex);
                    }
                }
            }
        }


        public Empresa ConsultarEmpresa(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Empresa entidad = new Empresa();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Empresa WHERE COD_EMPRESA = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt32(resultado["COD_EMPRESA"]);
                            if (resultado["NIT"] != DBNull.Value) entidad.nit = Convert.ToString(resultado["NIT"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["SIGLA"] != DBNull.Value) entidad.sigla = Convert.ToString(resultado["SIGLA"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["FECHA_CONSTITUCION"] != DBNull.Value) entidad.fecha_constitución = Convert.ToDateTime(resultado["FECHA_CONSTITUCION"]);
                            if (resultado["CIUDAD"] != DBNull.Value) entidad.ciudad = Convert.ToInt32(resultado["CIUDAD"]);
                            if (resultado["E_MAIL"] != DBNull.Value) entidad.e_mail = Convert.ToString(resultado["E_MAIL"]);
                            if (resultado["REPRESENTANTE_LEGAL"] != DBNull.Value) entidad.representante_legal = Convert.ToString(resultado["REPRESENTANTE_LEGAL"]);
                            if (resultado["CONTADOR"] != DBNull.Value) entidad.contador = Convert.ToString(resultado["CONTADOR"]);
                            if (resultado["TARJETA_CONTADOR"] != DBNull.Value) entidad.tarjeta_contador = Convert.ToString(resultado["TARJETA_CONTADOR"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToDecimal(resultado["TIPO"]);
                            if (resultado["REPORTE_EGRESO"] != DBNull.Value) entidad.reporte_egreso = Convert.ToString(resultado["REPORTE_EGRESO"]);
                            if (resultado["REVISOR"] != DBNull.Value) entidad.revisor = Convert.ToString(resultado["REVISOR"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["REPORTE_INGRESO"] != DBNull.Value) entidad.reporte_ingreso = Convert.ToString(resultado["REPORTE_INGRESO"]);
                            if (resultado["LOGOEMPRESA"] != DBNull.Value) entidad.logoempresa_bytes = (byte[])(resultado["LOGOEMPRESA"]);
                            if (resultado["COD_UIAF"] != DBNull.Value) entidad.cod_uiaf = Convert.ToString(resultado["COD_UIAF"]);
                            if (resultado["CLAVECORREO"] != DBNull.Value) entidad.clavecorreo = Convert.ToString(resultado["CLAVECORREO"]);
                            if (resultado["MANEJA_SINCRONIZACION"] != DBNull.Value) entidad.maneja_sincronizacion = Convert.ToInt32(resultado["MANEJA_SINCRONIZACION"]);
                            if (resultado["DESCRIPCION_REGIMEN"] != DBNull.Value) entidad.desc_regimen = Convert.ToString(resultado["DESCRIPCION_REGIMEN"]);
                            if (resultado["RESOLUCION_FACTURACION"] != DBNull.Value) entidad.resol_facturacion = Convert.ToString(resultado["RESOLUCION_FACTURACION"]);

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
                        BOExcepcion.Throw("EmpresaData", "ConsultarEmpresa", ex);
                        return null;
                    }
                }
            }
        }


        public List<Empresa> ListarEmpresa(Empresa pEmpresa, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Empresa> lstEmpresa = new List<Empresa>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Empresa " + ObtenerFiltro(pEmpresa) + " ORDER BY COD_EMPRESA ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Empresa entidad = new Empresa();
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt32(resultado["COD_EMPRESA"]);
                            if (resultado["NIT"] != DBNull.Value) entidad.nit = Convert.ToString(resultado["NIT"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["SIGLA"] != DBNull.Value) entidad.sigla = Convert.ToString(resultado["SIGLA"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["FECHA_CONSTITUCION"] != DBNull.Value) entidad.fecha_constitución = Convert.ToDateTime(resultado["FECHA_CONSTITUCION"]);
                            if (resultado["CIUDAD"] != DBNull.Value) entidad.ciudad = Convert.ToInt32(resultado["CIUDAD"]);
                            if (resultado["E_MAIL"] != DBNull.Value) entidad.e_mail = Convert.ToString(resultado["E_MAIL"]);
                            if (resultado["REPRESENTANTE_LEGAL"] != DBNull.Value) entidad.representante_legal = Convert.ToString(resultado["REPRESENTANTE_LEGAL"]);
                            if (resultado["CONTADOR"] != DBNull.Value) entidad.contador = Convert.ToString(resultado["CONTADOR"]);
                            if (resultado["TARJETA_CONTADOR"] != DBNull.Value) entidad.tarjeta_contador = Convert.ToString(resultado["TARJETA_CONTADOR"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToDecimal(resultado["TIPO"]);
                            if (resultado["REPORTE_EGRESO"] != DBNull.Value) entidad.reporte_egreso = Convert.ToString(resultado["REPORTE_EGRESO"]);
                            if (resultado["REVISOR"] != DBNull.Value) entidad.revisor = Convert.ToString(resultado["REVISOR"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["REPORTE_INGRESO"] != DBNull.Value) entidad.reporte_ingreso = Convert.ToString(resultado["REPORTE_INGRESO"]);
                            if (resultado["LOGOEMPRESA"] != DBNull.Value) entidad.logoempresa_bytes = (byte[])(resultado["LOGOEMPRESA"]);
                            if (resultado["COD_UIAF"] != DBNull.Value) entidad.cod_uiaf = Convert.ToString(resultado["COD_UIAF"]);
                            if (resultado["CLAVECORREO"] != DBNull.Value) entidad.clavecorreo = Convert.ToString(resultado["CLAVECORREO"]);
                            if (resultado["MANEJA_SINCRONIZACION"] != DBNull.Value) entidad.maneja_sincronizacion = Convert.ToInt32(resultado["MANEJA_SINCRONIZACION"]);

                            if (resultado["DESCRIPCION_REGIMEN"] != DBNull.Value) entidad.desc_regimen = Convert.ToString(resultado["DESCRIPCION_REGIMEN"]);
                            if (resultado["RESOLUCION_FACTURACION"] != DBNull.Value) entidad.resol_facturacion = Convert.ToString(resultado["RESOLUCION_FACTURACION"]);

                            lstEmpresa.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstEmpresa;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpresaData", "ListarEmpresa", ex);
                        return null;
                    }
                }
            }
        }


    }
}
