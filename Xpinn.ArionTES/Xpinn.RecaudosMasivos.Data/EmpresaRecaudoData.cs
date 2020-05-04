using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Tesoreria.Entities;
using System.Web;

namespace Xpinn.Tesoreria.Data
{
    public class EmpresaRecaudoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public EmpresaRecaudoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public EmpresaRecaudo CrearEmpresaRecaudo(EmpresaRecaudo pEmpresaRecaudo, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_empresa = cmdTransaccionFactory.CreateParameter();
                        pcod_empresa.ParameterName = "p_cod_empresa";
                        pcod_empresa.Value = pEmpresaRecaudo.cod_empresa;
                        pcod_empresa.Direction = ParameterDirection.Output;
                        pcod_empresa.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_empresa);

                        DbParameter pnom_empresa = cmdTransaccionFactory.CreateParameter();
                        pnom_empresa.ParameterName = "p_nom_empresa";
                        pnom_empresa.Value = pEmpresaRecaudo.nom_empresa;
                        pnom_empresa.Direction = ParameterDirection.Input;
                        pnom_empresa.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnom_empresa);

                        DbParameter p_num_planilla = cmdTransaccionFactory.CreateParameter();
                        p_num_planilla.ParameterName = "p_num_planilla";
                        if (pEmpresaRecaudo.numero_planilla != null) p_num_planilla.Value = pEmpresaRecaudo.numero_planilla; else p_num_planilla.Value = DBNull.Value;
                        p_num_planilla.Direction = ParameterDirection.Input;
                        p_num_planilla.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_num_planilla);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pEmpresaRecaudo.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pdireccion = cmdTransaccionFactory.CreateParameter();
                        pdireccion.ParameterName = "p_direccion";
                        if (pEmpresaRecaudo.direccion != null) pdireccion.Value = pEmpresaRecaudo.direccion; else pdireccion.Value = DBNull.Value;
                        pdireccion.Direction = ParameterDirection.Input;
                        pdireccion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdireccion);

                        DbParameter ptelefono = cmdTransaccionFactory.CreateParameter();
                        ptelefono.ParameterName = "p_telefono";
                        if (pEmpresaRecaudo.telefono != null) ptelefono.Value = pEmpresaRecaudo.telefono; else ptelefono.Value = DBNull.Value;
                        ptelefono.Direction = ParameterDirection.Input;
                        ptelefono.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptelefono);

                        DbParameter presponsable = cmdTransaccionFactory.CreateParameter();
                        presponsable.ParameterName = "p_responsable";
                        if (pEmpresaRecaudo.responsable != null) presponsable.Value = pEmpresaRecaudo.responsable; else presponsable.Value = DBNull.Value;
                        presponsable.Direction = ParameterDirection.Input;
                        presponsable.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(presponsable);

                        DbParameter ptipo_novedad = cmdTransaccionFactory.CreateParameter();
                        ptipo_novedad.ParameterName = "p_tipo_novedad";
                        ptipo_novedad.Value = pEmpresaRecaudo.tipo_novedad;
                        ptipo_novedad.Direction = ParameterDirection.Input;
                        ptipo_novedad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_novedad);

                        DbParameter pdias_novedad = cmdTransaccionFactory.CreateParameter();
                        pdias_novedad.ParameterName = "p_dias_novedad";
                        if (pEmpresaRecaudo.dias_novedad != null) pdias_novedad.Value = pEmpresaRecaudo.dias_novedad; else pdias_novedad.Value = DBNull.Value;
                        pdias_novedad.Direction = ParameterDirection.Input;
                        pdias_novedad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pdias_novedad);

                        DbParameter pfecha_convenio = cmdTransaccionFactory.CreateParameter();
                        pfecha_convenio.ParameterName = "p_fecha_convenio";
                        if (pEmpresaRecaudo.fecha_convenio != DateTime.MinValue) pfecha_convenio.Value = pEmpresaRecaudo.fecha_convenio; else pfecha_convenio.Value = DBNull.Value;
                        pfecha_convenio.Direction = ParameterDirection.Input;
                        pfecha_convenio.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_convenio);

                        DbParameter ptipo_recaudo = cmdTransaccionFactory.CreateParameter();
                        ptipo_recaudo.ParameterName = "p_tipo_recaudo";
                        if (pEmpresaRecaudo.tipo_recaudo == null)
                            ptipo_recaudo.Value = DBNull.Value;
                        else
                            ptipo_recaudo.Value = pEmpresaRecaudo.tipo_recaudo;
                        ptipo_recaudo.Direction = ParameterDirection.Input;
                        ptipo_recaudo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_recaudo);

                        DbParameter paplica_novedades = cmdTransaccionFactory.CreateParameter();
                        paplica_novedades.ParameterName = "p_aplica_novedades";
                        if (pEmpresaRecaudo.aplica_novedades == null)
                            paplica_novedades.Value = DBNull.Value;
                        else
                            paplica_novedades.Value = pEmpresaRecaudo.aplica_novedades;
                        paplica_novedades.Direction = ParameterDirection.Input;
                        paplica_novedades.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(paplica_novedades);

                        DbParameter pdeshabilitar_desc_inhabiles = cmdTransaccionFactory.CreateParameter();
                        pdeshabilitar_desc_inhabiles.ParameterName = "p_deshabilitar_desc_inhabiles";
                        pdeshabilitar_desc_inhabiles.Value = pEmpresaRecaudo.deshabilitar_desc_inhabiles ? 1 : 0;
                        pdeshabilitar_desc_inhabiles.Direction = ParameterDirection.Input;
                        pdeshabilitar_desc_inhabiles.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pdeshabilitar_desc_inhabiles);

                        DbParameter pmayores_valores = cmdTransaccionFactory.CreateParameter();
                        pmayores_valores.ParameterName = "p_mayores_valores";
                        if (pEmpresaRecaudo.mayores_valores == null)
                            pmayores_valores.Value = DBNull.Value;
                        else
                            pmayores_valores.Value = pEmpresaRecaudo.mayores_valores;
                        pmayores_valores.Direction = ParameterDirection.Input;
                        pmayores_valores.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pmayores_valores);

                        DbParameter paplica_refinanciados = cmdTransaccionFactory.CreateParameter();
                        paplica_refinanciados.ParameterName = "p_aplica_refinanciados";
                        if (pEmpresaRecaudo.aplica_refinanciados == null)
                            paplica_refinanciados.Value = DBNull.Value;
                        else
                            paplica_refinanciados.Value = pEmpresaRecaudo.aplica_refinanciados;
                        paplica_refinanciados.Direction = ParameterDirection.Input;
                        paplica_refinanciados.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(paplica_refinanciados);

                        DbParameter pforma_cobro = cmdTransaccionFactory.CreateParameter();
                        pforma_cobro.ParameterName = "p_forma_cobro";
                        if (pEmpresaRecaudo.forma_cobro == null)
                            pforma_cobro.Value = DBNull.Value;
                        else
                            pforma_cobro.Value = pEmpresaRecaudo.forma_cobro;
                        pforma_cobro.Direction = ParameterDirection.Input;
                        pforma_cobro.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pforma_cobro);

                        DbParameter paplicar_poroficina = cmdTransaccionFactory.CreateParameter();
                        paplicar_poroficina.ParameterName = "p_aplicar_poroficina";
                        paplicar_poroficina.Value = pEmpresaRecaudo.aplicar_poroficina;
                        paplicar_poroficina.Direction = ParameterDirection.Input;
                        paplicar_poroficina.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(paplicar_poroficina);

                        DbParameter p_cod_recaudo_estructura = cmdTransaccionFactory.CreateParameter();
                        p_cod_recaudo_estructura.ParameterName = "p_cod_recaudo_estructura";
                        if (!string.IsNullOrWhiteSpace(pEmpresaRecaudo.codigo_recaudo_estructura))
                        {
                            p_cod_recaudo_estructura.Value = pEmpresaRecaudo.codigo_recaudo_estructura;
                        }
                        else
                        {
                            p_cod_recaudo_estructura.Value = DBNull.Value;
                        }

                        p_cod_recaudo_estructura.Direction = ParameterDirection.Input;
                        p_cod_recaudo_estructura.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_cod_recaudo_estructura);

                        DbParameter p_maneja_atributos = cmdTransaccionFactory.CreateParameter();
                        p_maneja_atributos.ParameterName = "p_maneja_atributos";
                        p_maneja_atributos.Value = pEmpresaRecaudo.maneja_atributos;
                        p_maneja_atributos.Direction = ParameterDirection.Input;
                        p_maneja_atributos.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_maneja_atributos);

                        DbParameter p_cod_persona = cmdTransaccionFactory.CreateParameter();
                        p_cod_persona.ParameterName = "p_cod_persona";
                        p_cod_persona.Value = pEmpresaRecaudo.cod_persona;
                        p_cod_persona.Direction = ParameterDirection.Input;
                        p_cod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_persona);

                        DbParameter P_DESCUENTO_RETIRO = cmdTransaccionFactory.CreateParameter();
                        P_DESCUENTO_RETIRO.ParameterName = "P_DESCUENTO_RETIRO";
                        P_DESCUENTO_RETIRO.Value = pEmpresaRecaudo.descuento_retiro;
                        P_DESCUENTO_RETIRO.Direction = ParameterDirection.Input;
                        P_DESCUENTO_RETIRO.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(P_DESCUENTO_RETIRO);

                        DbParameter P_MANTENER_CONDICION = cmdTransaccionFactory.CreateParameter();
                        P_MANTENER_CONDICION.ParameterName = "P_MANTENER_CONDICION";
                        P_MANTENER_CONDICION.Value = pEmpresaRecaudo.mantener_condicion_inicial;
                        P_MANTENER_CONDICION.Direction = ParameterDirection.Input;
                        P_MANTENER_CONDICION.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(P_MANTENER_CONDICION);

                        DbParameter P_APLICAR_MORA = cmdTransaccionFactory.CreateParameter();
                        P_APLICAR_MORA.ParameterName = "P_APLICAR_MORA";
                        P_APLICAR_MORA.Value = pEmpresaRecaudo.aplica_mora;
                        P_APLICAR_MORA.Direction = ParameterDirection.Input;
                        P_APLICAR_MORA.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(P_APLICAR_MORA);

                        DbParameter P_APORTE_VACACIONES = cmdTransaccionFactory.CreateParameter();
                        P_APORTE_VACACIONES.ParameterName = "P_APORTE_VACACIONES";
                        P_APORTE_VACACIONES.Value = pEmpresaRecaudo.aporte_vacaciones;
                        P_APORTE_VACACIONES.Direction = ParameterDirection.Input;
                        P_APORTE_VACACIONES.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(P_APORTE_VACACIONES);


                        DbParameter p_debitoautomatico = cmdTransaccionFactory.CreateParameter();
                        p_debitoautomatico.ParameterName = "p_debito_automatico";
                        p_debitoautomatico.Value = pEmpresaRecaudo.debito_automatico;
                        p_debitoautomatico.Direction = ParameterDirection.Input;
                        p_debitoautomatico.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_debitoautomatico);

                        DbParameter p_debito_automatico_sem = cmdTransaccionFactory.CreateParameter();
                        p_debito_automatico_sem.ParameterName = "p_debito_automatico_sem";
                        p_debito_automatico_sem.Value = pEmpresaRecaudo.debito_automatico_sem;
                        p_debito_automatico_sem.Direction = ParameterDirection.Input;
                        p_debito_automatico_sem.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_debito_automatico_sem);


                        DbParameter pdescuentos_productos_inact = cmdTransaccionFactory.CreateParameter();
                        pdescuentos_productos_inact.ParameterName = "P_DESC_PROD_INACTIVOS";
                        pdescuentos_productos_inact.Value = pEmpresaRecaudo.descuentos_productos_inact ? 1 : 0;
                        pdescuentos_productos_inact.Direction = ParameterDirection.Input;
                        pdescuentos_productos_inact.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pdescuentos_productos_inact);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REC_EMPRESAREC_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pEmpresaRecaudo.cod_empresa = Convert.ToInt64(pcod_empresa.Value);
                        
                        return pEmpresaRecaudo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpresaRecaudoData", "CrearEmpresaRecaudo", ex);
                        return null;
                    }
                }
            }
        }

        public bool VerificarQueYaNoSeHallaCargadoLaMismaNovedad(EmpresaRecaudo empresa, Usuario usuario)
        {
            DbDataReader resultado;
            bool existe = false;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "";
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            sql = @"SELECT COUNT(*) as contador
                                    FROM RECAUDO_MASIVO 
                                    WHERE TIPO_RECAUDO = " + empresa.tipo_recaudo + 
                                    " AND COD_EMPRESA = " + empresa.cod_empresa +
                                    (empresa.tipo_lista == null ? " " : " AND TIPO_LISTA = " + empresa.tipo_lista) +
                                    " AND TRUNC(PERIODO_CORTE) = to_date('" + empresa.periodo_novedad.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') AND ESTADO IN (1, 2) ";
                        else
                            sql = @"SELECT COUNT(*) as contador
                                    FROM RECAUDO_MASIVO 
                                    WHERE TIPO_RECAUDO = " + empresa.tipo_recaudo +
                                    " AND COD_EMPRESA = " + empresa.cod_empresa +
                                    (empresa.tipo_lista == null ? " " : " AND TIPO_LISTA = " + empresa.tipo_lista) +
                                    " AND PERIODO_CORTE = '" + empresa.periodo_novedad.ToString(conf.ObtenerFormatoFecha()) + "' AND ESTADO IN (1, 2) ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["CONTADOR"] != DBNull.Value)
                            {
                                int contador = Convert.ToInt32(resultado["CONTADOR"]);
                                existe = contador > 0;
                            }
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return existe;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpresaRecaudoData", "VerificarQueYaNoSeHallaCargadoLaMismaNovedad", ex);
                        return false;
                    }
                }
            }
        }

        public EmpresaRecaudo ModificarEmpresaRecaudo(EmpresaRecaudo pEmpresaRecaudo, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_empresa = cmdTransaccionFactory.CreateParameter();
                        pcod_empresa.ParameterName = "p_cod_empresa";
                        pcod_empresa.Value = pEmpresaRecaudo.cod_empresa;
                        pcod_empresa.Direction = ParameterDirection.Input;
                        pcod_empresa.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_empresa);

                        DbParameter pnom_empresa = cmdTransaccionFactory.CreateParameter();
                        pnom_empresa.ParameterName = "p_nom_empresa";
                        pnom_empresa.Value = pEmpresaRecaudo.nom_empresa;
                        pnom_empresa.Direction = ParameterDirection.Input;
                        pnom_empresa.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnom_empresa);

                        DbParameter p_num_planilla = cmdTransaccionFactory.CreateParameter();
                        p_num_planilla.ParameterName = "p_num_planilla";
                        if (pEmpresaRecaudo.numero_planilla != null) p_num_planilla.Value = pEmpresaRecaudo.numero_planilla; else p_num_planilla.Value = DBNull.Value;
                        p_num_planilla.Direction = ParameterDirection.Input;
                        p_num_planilla.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_num_planilla);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pEmpresaRecaudo.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pdireccion = cmdTransaccionFactory.CreateParameter();
                        pdireccion.ParameterName = "p_direccion";
                        if (pEmpresaRecaudo.direccion != null) pdireccion.Value = pEmpresaRecaudo.direccion; else pdireccion.Value = DBNull.Value;
                        pdireccion.Direction = ParameterDirection.Input;
                        pdireccion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdireccion);

                        DbParameter ptelefono = cmdTransaccionFactory.CreateParameter();
                        ptelefono.ParameterName = "p_telefono";
                        if (pEmpresaRecaudo.telefono != null) ptelefono.Value = pEmpresaRecaudo.telefono; else ptelefono.Value = DBNull.Value;
                        ptelefono.Direction = ParameterDirection.Input;
                        ptelefono.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptelefono);

                        DbParameter presponsable = cmdTransaccionFactory.CreateParameter();
                        presponsable.ParameterName = "p_responsable";
                        if (pEmpresaRecaudo.responsable != null) presponsable.Value = pEmpresaRecaudo.responsable; else presponsable.Value = DBNull.Value;
                        presponsable.Direction = ParameterDirection.Input;
                        presponsable.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(presponsable);

                        DbParameter ptipo_novedad = cmdTransaccionFactory.CreateParameter();
                        ptipo_novedad.ParameterName = "p_tipo_novedad";
                        ptipo_novedad.Value = pEmpresaRecaudo.tipo_novedad;
                        ptipo_novedad.Direction = ParameterDirection.Input;
                        ptipo_novedad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_novedad);

                        DbParameter pdias_novedad = cmdTransaccionFactory.CreateParameter();
                        pdias_novedad.ParameterName = "p_dias_novedad";
                        if (pEmpresaRecaudo.dias_novedad != null) pdias_novedad.Value = pEmpresaRecaudo.dias_novedad; else pdias_novedad.Value = DBNull.Value;
                        pdias_novedad.Direction = ParameterDirection.Input;
                        pdias_novedad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pdias_novedad);

                        DbParameter pfecha_convenio = cmdTransaccionFactory.CreateParameter();
                        pfecha_convenio.ParameterName = "p_fecha_convenio";
                        if (pEmpresaRecaudo.fecha_convenio != DateTime.MinValue) pfecha_convenio.Value = pEmpresaRecaudo.fecha_convenio; else pfecha_convenio.Value = DBNull.Value;
                        pfecha_convenio.Direction = ParameterDirection.Input;
                        pfecha_convenio.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_convenio);

                        DbParameter ptipo_recaudo = cmdTransaccionFactory.CreateParameter();
                        ptipo_recaudo.ParameterName = "p_tipo_recaudo";
                        if (pEmpresaRecaudo.tipo_recaudo == null)
                            ptipo_recaudo.Value = DBNull.Value;
                        else
                            ptipo_recaudo.Value = pEmpresaRecaudo.tipo_recaudo;
                        ptipo_recaudo.Direction = ParameterDirection.Input;
                        ptipo_recaudo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_recaudo);

                        DbParameter paplica_novedades = cmdTransaccionFactory.CreateParameter();
                        paplica_novedades.ParameterName = "p_aplica_novedades";
                        if (pEmpresaRecaudo.aplica_novedades == null)
                            paplica_novedades.Value = DBNull.Value;
                        else
                            paplica_novedades.Value = pEmpresaRecaudo.aplica_novedades;
                        paplica_novedades.Direction = ParameterDirection.Input;
                        paplica_novedades.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(paplica_novedades);

                        DbParameter pdeshabilitar_desc_inhabiles = cmdTransaccionFactory.CreateParameter();
                        pdeshabilitar_desc_inhabiles.ParameterName = "p_deshabilitar_desc_inhabiles";
                        pdeshabilitar_desc_inhabiles.Value = pEmpresaRecaudo.deshabilitar_desc_inhabiles ? 1 : 0;
                        pdeshabilitar_desc_inhabiles.Direction = ParameterDirection.Input;
                        pdeshabilitar_desc_inhabiles.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pdeshabilitar_desc_inhabiles);

                        DbParameter pmayores_valores = cmdTransaccionFactory.CreateParameter();
                        pmayores_valores.ParameterName = "p_mayores_valores";
                        if (pEmpresaRecaudo.mayores_valores == null)
                            pmayores_valores.Value = DBNull.Value;
                        else
                            pmayores_valores.Value = pEmpresaRecaudo.mayores_valores;
                        pmayores_valores.Direction = ParameterDirection.Input;
                        pmayores_valores.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pmayores_valores);

                        DbParameter paplica_refinanciados = cmdTransaccionFactory.CreateParameter();
                        paplica_refinanciados.ParameterName = "p_aplica_refinanciados";
                        if (pEmpresaRecaudo.aplica_refinanciados == null)
                            paplica_refinanciados.Value = DBNull.Value;
                        else
                            paplica_refinanciados.Value = pEmpresaRecaudo.aplica_refinanciados;
                        paplica_refinanciados.Direction = ParameterDirection.Input;
                        paplica_refinanciados.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(paplica_refinanciados);

                        DbParameter pforma_cobro = cmdTransaccionFactory.CreateParameter();
                        pforma_cobro.ParameterName = "p_forma_cobro";
                        if (pEmpresaRecaudo.forma_cobro == null)
                            pforma_cobro.Value = DBNull.Value;
                        else
                            pforma_cobro.Value = pEmpresaRecaudo.forma_cobro;
                        pforma_cobro.Direction = ParameterDirection.Input;
                        pforma_cobro.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pforma_cobro);

                        DbParameter paplicar_poroficina = cmdTransaccionFactory.CreateParameter();
                        paplicar_poroficina.ParameterName = "p_aplicar_poroficina";
                        paplicar_poroficina.Value = pEmpresaRecaudo.aplicar_poroficina;
                        paplicar_poroficina.Direction = ParameterDirection.Input;
                        paplicar_poroficina.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(paplicar_poroficina);

                        DbParameter p_cod_recaudo_estructura = cmdTransaccionFactory.CreateParameter();
                        p_cod_recaudo_estructura.ParameterName = "p_cod_recaudo_estructura";
                        if (!string.IsNullOrWhiteSpace(pEmpresaRecaudo.codigo_recaudo_estructura))
                        {
                            p_cod_recaudo_estructura.Value = pEmpresaRecaudo.codigo_recaudo_estructura;
                        }
                        else
                        {
                            p_cod_recaudo_estructura.Value = DBNull.Value;
                        }

                        p_cod_recaudo_estructura.Direction = ParameterDirection.Input;
                        p_cod_recaudo_estructura.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_cod_recaudo_estructura);

                        DbParameter p_maneja_atributos = cmdTransaccionFactory.CreateParameter();
                        p_maneja_atributos.ParameterName = "p_maneja_atributos";
                        p_maneja_atributos.Value = pEmpresaRecaudo.maneja_atributos;
                        p_maneja_atributos.Direction = ParameterDirection.Input;
                        p_maneja_atributos.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_maneja_atributos);

                        DbParameter p_cod_persona = cmdTransaccionFactory.CreateParameter();
                        p_cod_persona.ParameterName = "p_cod_persona";
                        p_cod_persona.Value = pEmpresaRecaudo.cod_persona;
                        p_cod_persona.Direction = ParameterDirection.Input;
                        p_cod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_persona);


                        DbParameter P_DESCUENTO_RETIRO = cmdTransaccionFactory.CreateParameter();
                        P_DESCUENTO_RETIRO.ParameterName = "P_DESCUENTO_RETIRO";
                        P_DESCUENTO_RETIRO.Value = pEmpresaRecaudo.descuento_retiro;
                        P_DESCUENTO_RETIRO.Direction = ParameterDirection.Input;
                        P_DESCUENTO_RETIRO.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(P_DESCUENTO_RETIRO);

                        DbParameter P_MANTENER_CONDICION = cmdTransaccionFactory.CreateParameter();
                        P_MANTENER_CONDICION.ParameterName = "P_MANTENER_CONDICION";
                        P_MANTENER_CONDICION.Value = pEmpresaRecaudo.mantener_condicion_inicial;
                        P_MANTENER_CONDICION.Direction = ParameterDirection.Input;
                        P_MANTENER_CONDICION.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(P_MANTENER_CONDICION);


                        DbParameter P_APLICAR_MORA = cmdTransaccionFactory.CreateParameter();
                        P_APLICAR_MORA.ParameterName = "P_APLICAR_MORA";
                        P_APLICAR_MORA.Value = pEmpresaRecaudo.aplica_mora;
                        P_APLICAR_MORA.Direction = ParameterDirection.Input;
                        P_APLICAR_MORA.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(P_APLICAR_MORA);

                        DbParameter P_APORTE_VACACIONES = cmdTransaccionFactory.CreateParameter();
                        P_APORTE_VACACIONES.ParameterName = "P_APORTE_VACACIONES";
                        P_APORTE_VACACIONES.Value = pEmpresaRecaudo.aporte_vacaciones;
                        P_APORTE_VACACIONES.Direction = ParameterDirection.Input;
                        P_APORTE_VACACIONES.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(P_APORTE_VACACIONES);


                        DbParameter p_debitoautomatico = cmdTransaccionFactory.CreateParameter();
                        p_debitoautomatico.ParameterName = "p_debito_automatico";
                        p_debitoautomatico.Value = pEmpresaRecaudo.debito_automatico;
                        p_debitoautomatico.Direction = ParameterDirection.Input;
                        p_debitoautomatico.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_debitoautomatico);

                        DbParameter p_debito_automatico_sem = cmdTransaccionFactory.CreateParameter();
                        p_debito_automatico_sem.ParameterName = "p_debito_automatico_sem";
                        p_debito_automatico_sem.Value = pEmpresaRecaudo.debito_automatico_sem;
                        p_debito_automatico_sem.Direction = ParameterDirection.Input;
                        p_debito_automatico_sem.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_debito_automatico_sem);




                        DbParameter pdescuentos_productos_inact = cmdTransaccionFactory.CreateParameter();
                        pdescuentos_productos_inact.ParameterName = "P_DESC_PROD_INACTIVOS";
                        pdescuentos_productos_inact.Value = pEmpresaRecaudo.descuentos_productos_inact ? 1 : 0;
                        pdescuentos_productos_inact.Direction = ParameterDirection.Input;
                        pdescuentos_productos_inact.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pdescuentos_productos_inact);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REC_EMPRESAREC_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pEmpresaRecaudo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpresaRecaudoData", "ModificarEmpresaRecaudo", ex);
                        return null;
                    }
                }
            }
        }

        public EmpresaRecaudo_Programacion CrearProgramacionRecaudo(EmpresaRecaudo_Programacion pEmpresa, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidprogramacion = cmdTransaccionFactory.CreateParameter();
                        pidprogramacion.ParameterName = "p_idprogramacion";
                        pidprogramacion.Value = pEmpresa.idprogramacion;
                        pidprogramacion.Direction = ParameterDirection.Output;
                        pidprogramacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidprogramacion);

                        DbParameter pcod_empresa = cmdTransaccionFactory.CreateParameter();
                        pcod_empresa.ParameterName = "p_cod_empresa";
                        pcod_empresa.Value = pEmpresa.cod_empresa;
                        pcod_empresa.Direction = ParameterDirection.Input;
                        pcod_empresa.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_empresa);

                        DbParameter ptipo_lista = cmdTransaccionFactory.CreateParameter();
                        ptipo_lista.ParameterName = "p_tipo_lista";
                        ptipo_lista.Value = pEmpresa.tipo_lista;
                        ptipo_lista.Direction = ParameterDirection.Input;
                        ptipo_lista.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_lista);

                        DbParameter pcod_periodicidad = cmdTransaccionFactory.CreateParameter();
                        pcod_periodicidad.ParameterName = "p_cod_periodicidad";
                        pcod_periodicidad.Value = pEmpresa.cod_periodicidad;
                        pcod_periodicidad.Direction = ParameterDirection.Input;
                        pcod_periodicidad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_periodicidad);

                        DbParameter pfecha_inicio = cmdTransaccionFactory.CreateParameter();
                        pfecha_inicio.ParameterName = "p_fecha_inicio";
                        pfecha_inicio.Value = pEmpresa.fecha_inicio;
                        pfecha_inicio.Direction = ParameterDirection.Input;
                        pfecha_inicio.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_inicio);

                        DbParameter pprincipal = cmdTransaccionFactory.CreateParameter();
                        pprincipal.ParameterName = "p_principal";
                        pprincipal.Value = pEmpresa.principal;
                        pprincipal.Direction = ParameterDirection.Input;
                        pprincipal.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pprincipal);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REC_EMPPROGRAM_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pEmpresa.idprogramacion = Convert.ToInt32(pidprogramacion.Value);
                        return pEmpresa;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpresaRecaudoData", "CrearProgramacionRecaudo", ex);
                        return null;
                    }
                }
            }
        }

        public EmpresaRecaudo_Programacion ModificarProgramacionRecaudo(EmpresaRecaudo_Programacion pEmpresa, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidprogramacion = cmdTransaccionFactory.CreateParameter();
                        pidprogramacion.ParameterName = "p_idprogramacion";
                        pidprogramacion.Value = pEmpresa.idprogramacion;
                        pidprogramacion.Direction = ParameterDirection.Input;
                        pidprogramacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidprogramacion);

                        DbParameter pcod_empresa = cmdTransaccionFactory.CreateParameter();
                        pcod_empresa.ParameterName = "p_cod_empresa";
                        pcod_empresa.Value = pEmpresa.cod_empresa;
                        pcod_empresa.Direction = ParameterDirection.Input;
                        pcod_empresa.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_empresa);

                        DbParameter ptipo_lista = cmdTransaccionFactory.CreateParameter();
                        ptipo_lista.ParameterName = "p_tipo_lista";
                        ptipo_lista.Value = pEmpresa.tipo_lista;
                        ptipo_lista.Direction = ParameterDirection.Input;
                        ptipo_lista.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_lista);

                        DbParameter pcod_periodicidad = cmdTransaccionFactory.CreateParameter();
                        pcod_periodicidad.ParameterName = "p_cod_periodicidad";
                        pcod_periodicidad.Value = pEmpresa.cod_periodicidad;
                        pcod_periodicidad.Direction = ParameterDirection.Input;
                        pcod_periodicidad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_periodicidad);

                        DbParameter pfecha_inicio = cmdTransaccionFactory.CreateParameter();
                        pfecha_inicio.ParameterName = "p_fecha_inicio";
                        pfecha_inicio.Value = pEmpresa.fecha_inicio;
                        pfecha_inicio.Direction = ParameterDirection.Input;
                        pfecha_inicio.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_inicio);

                        DbParameter pprincipal = cmdTransaccionFactory.CreateParameter();
                        pprincipal.ParameterName = "p_principal";
                        pprincipal.Value = pEmpresa.principal;
                        pprincipal.Direction = ParameterDirection.Input;
                        pprincipal.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pprincipal);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REC_EMPPROGRAM_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pEmpresa;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpresaRecaudoData", "ModificarProgramacionRecaudo", ex);
                        return null;
                    }
                }
            }
        }


        public EMPRESARECAUDO_CONCEPTO CrearConceptoRecaudo(EMPRESARECAUDO_CONCEPTO pEmpresa, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter pidempconcepto = cmdTransaccionFactory.CreateParameter();
                        pidempconcepto.ParameterName = "p_idempconcepto";
                        pidempconcepto.Value = pEmpresa.idempconcepto;
                        pidempconcepto.Direction = ParameterDirection.Output;
                        pidempconcepto.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidempconcepto);

                        DbParameter pcod_empresa = cmdTransaccionFactory.CreateParameter();
                        pcod_empresa.ParameterName = "p_cod_empresa";
                        pcod_empresa.Value = pEmpresa.cod_empresa;
                        pcod_empresa.Direction = ParameterDirection.Input;
                        pcod_empresa.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_empresa);

                        DbParameter ptipo_producto = cmdTransaccionFactory.CreateParameter();
                        ptipo_producto.ParameterName = "p_tipo_producto";
                        ptipo_producto.Value = pEmpresa.tipo_producto;
                        ptipo_producto.Direction = ParameterDirection.Input;
                        ptipo_producto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_producto);

                        DbParameter pcod_linea = cmdTransaccionFactory.CreateParameter();
                        pcod_linea.ParameterName = "p_cod_linea";
                        if (pEmpresa.cod_linea == null)
                            pcod_linea.Value = DBNull.Value;
                        else
                            pcod_linea.Value = pEmpresa.cod_linea;
                        pcod_linea.Direction = ParameterDirection.Input;
                        pcod_linea.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea);

                        DbParameter pcod_concepto = cmdTransaccionFactory.CreateParameter();
                        pcod_concepto.ParameterName = "p_cod_concepto";
                        if (pEmpresa.cod_concepto == null)
                            pcod_concepto.Value = DBNull.Value;
                        else
                            pcod_concepto.Value = pEmpresa.cod_concepto;
                        pcod_concepto.Direction = ParameterDirection.Input;
                        pcod_concepto.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_concepto);

                        DbParameter pprioridad = cmdTransaccionFactory.CreateParameter();
                        pprioridad.ParameterName = "p_prioridad";
                        if (pEmpresa.prioridad == null)
                            pprioridad.Value = DBNull.Value;
                        else
                            pprioridad.Value = pEmpresa.prioridad;
                        pprioridad.Direction = ParameterDirection.Input;
                        pprioridad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pprioridad);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REC_EMPCONCEP_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pEmpresa.idempconcepto = Convert.ToInt32(pidempconcepto.Value);
                        return pEmpresa;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpresaRecaudoData", "CrearConceptoRecaudo", ex);
                        return null;
                    }
                }
            }
        }


        public EMPRESARECAUDO_CONCEPTO ModificarConceptoRecaudo(EMPRESARECAUDO_CONCEPTO pEmpresa, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter pidempconcepto = cmdTransaccionFactory.CreateParameter();
                        pidempconcepto.ParameterName = "p_idempconcepto";
                        pidempconcepto.Value = pEmpresa.idempconcepto;
                        pidempconcepto.Direction = ParameterDirection.Input;
                        pidempconcepto.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidempconcepto);

                        DbParameter pcod_empresa = cmdTransaccionFactory.CreateParameter();
                        pcod_empresa.ParameterName = "p_cod_empresa";
                        pcod_empresa.Value = pEmpresa.cod_empresa;
                        pcod_empresa.Direction = ParameterDirection.Input;
                        pcod_empresa.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_empresa);

                        DbParameter ptipo_producto = cmdTransaccionFactory.CreateParameter();
                        ptipo_producto.ParameterName = "p_tipo_producto";
                        ptipo_producto.Value = pEmpresa.tipo_producto;
                        ptipo_producto.Direction = ParameterDirection.Input;
                        ptipo_producto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_producto);

                        DbParameter pcod_linea = cmdTransaccionFactory.CreateParameter();
                        pcod_linea.ParameterName = "p_cod_linea";
                        if (pEmpresa.cod_linea == null)
                            pcod_linea.Value = DBNull.Value;
                        else
                            pcod_linea.Value = pEmpresa.cod_linea;
                        pcod_linea.Direction = ParameterDirection.Input;
                        pcod_linea.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea);

                        DbParameter pcod_concepto = cmdTransaccionFactory.CreateParameter();
                        pcod_concepto.ParameterName = "p_cod_concepto";
                        if (pEmpresa.cod_concepto == null)
                            pcod_concepto.Value = DBNull.Value;
                        else
                            pcod_concepto.Value = pEmpresa.cod_concepto;
                        pcod_concepto.Direction = ParameterDirection.Input;
                        pcod_concepto.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_concepto);

                        DbParameter pprioridad = cmdTransaccionFactory.CreateParameter();
                        pprioridad.ParameterName = "p_prioridad";
                        if (pEmpresa.prioridad == null)
                            pprioridad.Value = DBNull.Value;
                        else
                            pprioridad.Value = pEmpresa.prioridad;
                        pprioridad.Direction = ParameterDirection.Input;
                        pprioridad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pprioridad);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REC_EMPCONCEP_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pEmpresa;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpresaRecaudoData", "ModificarConceptoRecaudo", ex);
                        return null;
                    }
                }
            }
        }

      
        public void EliminarEmpresaRecaudo(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter pcod_empresa = cmdTransaccionFactory.CreateParameter();
                        pcod_empresa.ParameterName = "p_cod_empresa";
                        pcod_empresa.Value = pId;
                        pcod_empresa.Direction = ParameterDirection.Input;
                        pcod_empresa.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_empresa);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REC_EMPRESAREC_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpresaRecaudoData", "EliminarEmpresaRecaudo", ex);
                    }
                }
            }
        }


        public List<EmpresaRecaudo> ListarEmpresaRecaudo(EmpresaRecaudo pEmpresaRecaudo, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<EmpresaRecaudo> lstEmpresaRecaudo = new List<EmpresaRecaudo>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT empresa_recaudo.*, Case empresa_recaudo.tipo_novedad When 0 Then 'Todas' When 1 Then 'Nuevas' End As nomtipo_novedad FROM empresa_recaudo " + ObtenerFiltro(pEmpresaRecaudo, "empresa_recaudo.") + " ORDER BY COD_EMPRESA ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            EmpresaRecaudo entidad = new EmpresaRecaudo();
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt32(resultado["COD_EMPRESA"]);
                            if (resultado["NOM_EMPRESA"] != DBNull.Value) entidad.nom_empresa = Convert.ToString(resultado["NOM_EMPRESA"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["RESPONSABLE"] != DBNull.Value) entidad.responsable = Convert.ToString(resultado["RESPONSABLE"]);
                            if (resultado["TIPO_NOVEDAD"] != DBNull.Value) entidad.tipo_novedad = Convert.ToInt32(resultado["TIPO_NOVEDAD"]);
                            if (resultado["NOMTIPO_NOVEDAD"] != DBNull.Value) entidad.nomtipo_novedad = Convert.ToString(resultado["NOMTIPO_NOVEDAD"]);
                            if (resultado["DIAS_NOVEDAD"] != DBNull.Value) entidad.dias_novedad = Convert.ToInt32(resultado["DIAS_NOVEDAD"]);
                            if (resultado["FECHA_CONVENIO"] != DBNull.Value) entidad.fecha_convenio = Convert.ToDateTime(resultado["FECHA_CONVENIO"]);
                            if (resultado["TIPO_RECAUDO"] != DBNull.Value) entidad.tipo_recaudo = Convert.ToInt32(resultado["TIPO_RECAUDO"]);
                            if (resultado["APLICA_NOVEDADES"] != DBNull.Value) entidad.aplica_novedades = Convert.ToInt32(resultado["APLICA_NOVEDADES"]);
                            if (resultado["MAYORES_VALORES"] != DBNull.Value) entidad.mayores_valores = Convert.ToInt32(resultado["MAYORES_VALORES"]);
                            if (resultado["FORMA_COBRO"] != DBNull.Value) entidad.forma_cobro = Convert.ToInt32(resultado["FORMA_COBRO"]);
                            if (resultado["DEBITO_AUTOMATICO"] != DBNull.Value) entidad.debito_automatico = Convert.ToInt32(resultado["DEBITO_AUTOMATICO"]);
                            if (resultado["DEBITO_AUTOMATICO_SEM"] != DBNull.Value) entidad.debito_automatico_sem = Convert.ToInt32(resultado["DEBITO_AUTOMATICO_SEM"]);
                            if (resultado["DESC_PROD_INACTIVOS"] != DBNull.Value) entidad.descuentos_productos_inact = Convert.ToBoolean(resultado["DESC_PROD_INACTIVOS"]);

                            lstEmpresaRecaudo.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstEmpresaRecaudo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpresaRecaudoData", "ListarEmpresaRecaudo", ex);
                        return null;
                    }
                }
            }
        }

        public List<EmpresaRecaudo> ListarEmpresaRecaudoPersona(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<EmpresaRecaudo> lstEmpresaRecaudo = new List<EmpresaRecaudo>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM v_persona_empresa_recaudo WHERE cod_persona = " + pId.ToString() + " ORDER BY COD_EMPRESA ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            EmpresaRecaudo entidad = new EmpresaRecaudo();
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt32(resultado["COD_EMPRESA"]);
                            if (resultado["NOM_EMPRESA"] != DBNull.Value) entidad.nom_empresa = Convert.ToString(resultado["NOM_EMPRESA"]);                           
                            lstEmpresaRecaudo.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstEmpresaRecaudo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpresaRecaudoData", "ListarEmpresaRecaudoPersona", ex);
                        return null;
                    }
                }
            }
        }

        public EmpresaRecaudo ConsultarEMPRESARECAUDO(EmpresaRecaudo pEntidad, Usuario vUsuario)
        {
            DbDataReader resultado;
            EmpresaRecaudo entidad = new EmpresaRecaudo();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT e.*, p.identificacion FROM EMPRESA_RECAUDO  e LEFT JOIN PERSONA p ON e.COD_PERSONA = p.COD_PERSONA " + ObtenerFiltro(pEntidad);
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt32(resultado["COD_EMPRESA"]);
                            if (resultado["NOM_EMPRESA"] != DBNull.Value) entidad.nom_empresa = Convert.ToString(resultado["NOM_EMPRESA"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]); else entidad.direccion = null;
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]); else entidad.telefono = null;
                            if (resultado["RESPONSABLE"] != DBNull.Value) entidad.responsable = Convert.ToString(resultado["RESPONSABLE"]); else entidad.responsable = null;
                            if (resultado["TIPO_NOVEDAD"] != DBNull.Value) entidad.tipo_novedad = Convert.ToInt32(resultado["TIPO_NOVEDAD"]); else entidad.tipo_novedad = 0;
                            if (resultado["DIAS_NOVEDAD"] != DBNull.Value) entidad.dias_novedad = Convert.ToInt32(resultado["DIAS_NOVEDAD"]); else entidad.dias_novedad = 0;
                            if (resultado["FECHA_CONVENIO"] != DBNull.Value) entidad.fecha_convenio = Convert.ToDateTime(resultado["FECHA_CONVENIO"]);
                            if (resultado["TIPO_RECAUDO"] != DBNull.Value) entidad.tipo_recaudo = Convert.ToInt32(resultado["TIPO_RECAUDO"]); else entidad.tipo_recaudo = 0;
                            if (resultado["APLICA_NOVEDADES"] != DBNull.Value) entidad.aplica_novedades = Convert.ToInt32(resultado["APLICA_NOVEDADES"]);
                            if (resultado["DESHABILITAR_DESC_INHABILES"] != DBNull.Value) entidad.deshabilitar_desc_inhabiles = Convert.ToInt32(resultado["DESHABILITAR_DESC_INHABILES"]) == 1;
                            if (resultado["MAYORES_VALORES"] != DBNull.Value) entidad.mayores_valores = Convert.ToInt32(resultado["MAYORES_VALORES"]);
                            if (resultado["APLICA_REFINANCIADOS"] != DBNull.Value) entidad.aplica_refinanciados = Convert.ToInt32(resultado["APLICA_REFINANCIADOS"]);
                            if (resultado["FORMA_COBRO"] != DBNull.Value) entidad.forma_cobro = Convert.ToInt32(resultado["FORMA_COBRO"]);
                            if (resultado["APLICAR_POROFICINA"] != DBNull.Value) entidad.aplicar_poroficina = Convert.ToInt32(resultado["APLICAR_POROFICINA"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["NUM_PLANILLA"] != DBNull.Value) entidad.numero_planilla = Convert.ToInt32(resultado["NUM_PLANILLA"]);
                            if (resultado["COD_RECAUDO_ESTRUCTURA"] != DBNull.Value) entidad.codigo_recaudo_estructura = Convert.ToString(resultado["COD_RECAUDO_ESTRUCTURA"]);
                            if (resultado["MANEJA_ATRIBUTOS"] != DBNull.Value) entidad.maneja_atributos = Convert.ToInt32(resultado["MANEJA_ATRIBUTOS"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToInt64(resultado["IDENTIFICACION"]);
                            if (resultado["DESCUENTO_RETIRO"] != DBNull.Value) entidad.descuento_retiro = Convert.ToInt32(resultado["DESCUENTO_RETIRO"]);
                            if (resultado["MANTENER_CONDICION"] != DBNull.Value) entidad.mantener_condicion_inicial = Convert.ToInt32(resultado["MANTENER_CONDICION"]);
                            if (resultado["APLICAR_MORA"] != DBNull.Value) entidad.aplica_mora = Convert.ToInt32(resultado["APLICAR_MORA"]);
                            if (resultado["APORTE_VACACIONES"] != DBNull.Value) entidad.aporte_vacaciones = Convert.ToInt32(resultado["APORTE_VACACIONES"]);
                            if (resultado["DEBITO_AUTOMATICO"] != DBNull.Value) entidad.debito_automatico = Convert.ToInt32(resultado["DEBITO_AUTOMATICO"]);
                            if (resultado["DEBITO_AUTOMATICO_SEM"] != DBNull.Value) entidad.debito_automatico_sem = Convert.ToInt32(resultado["DEBITO_AUTOMATICO_SEM"]);
                            if (resultado["DESC_PROD_INACTIVOS"] != DBNull.Value) entidad.descuentos_productos_inact = Convert.ToBoolean(resultado["DESC_PROD_INACTIVOS"]);


                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpresaRecaudoData", "ConsultarEMPRESARECAUDO", ex);
                        return null;
                    }
                }
            }
        }

        public EmpresaRecaudo ConsultarEMPRESARECAUDO(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            EmpresaRecaudo entidad = new EmpresaRecaudo();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT e.*, p.identificacion FROM EMPRESA_RECAUDO  e LEFT JOIN PERSONA p ON e.COD_PERSONA = p.COD_PERSONA WHERE e.COD_EMPRESA = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt32(resultado["COD_EMPRESA"]);
                            if (resultado["NOM_EMPRESA"] != DBNull.Value) entidad.nom_empresa = Convert.ToString(resultado["NOM_EMPRESA"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["RESPONSABLE"] != DBNull.Value) entidad.responsable = Convert.ToString(resultado["RESPONSABLE"]);
                            if (resultado["TIPO_NOVEDAD"] != DBNull.Value) entidad.tipo_novedad = Convert.ToInt32(resultado["TIPO_NOVEDAD"]);
                            if (resultado["DIAS_NOVEDAD"] != DBNull.Value) entidad.dias_novedad = Convert.ToInt32(resultado["DIAS_NOVEDAD"]);
                            if (resultado["FECHA_CONVENIO"] != DBNull.Value) entidad.fecha_convenio = Convert.ToDateTime(resultado["FECHA_CONVENIO"]);
                            if (resultado["TIPO_RECAUDO"] != DBNull.Value) entidad.tipo_recaudo = Convert.ToInt32(resultado["TIPO_RECAUDO"]);
                            if (resultado["APLICA_NOVEDADES"] != DBNull.Value) entidad.aplica_novedades = Convert.ToInt32(resultado["APLICA_NOVEDADES"]);
                            if (resultado["MAYORES_VALORES"] != DBNull.Value) entidad.mayores_valores = Convert.ToInt32(resultado["MAYORES_VALORES"]);
                            if (resultado["APLICA_REFINANCIADOS"] != DBNull.Value) entidad.aplica_refinanciados = Convert.ToInt32(resultado["APLICA_REFINANCIADOS"]);
                            if (resultado["NUM_PLANILLA"] != DBNull.Value) entidad.numero_planilla = Convert.ToInt32(resultado["NUM_PLANILLA"]);
                            if (resultado["COD_RECAUDO_ESTRUCTURA"] != DBNull.Value) entidad.codigo_recaudo_estructura = Convert.ToString(resultado["COD_RECAUDO_ESTRUCTURA"]);
                            if (resultado["MANEJA_ATRIBUTOS"] != DBNull.Value) entidad.maneja_atributos = Convert.ToInt32(resultado["MANEJA_ATRIBUTOS"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToInt64(resultado["IDENTIFICACION"]);
                            if (resultado["APLICAR_MORA"] != DBNull.Value) entidad.aplica_mora = Convert.ToInt32(resultado["APLICAR_MORA"]);
                            if (resultado["DEBITO_AUTOMATICO"] != DBNull.Value) entidad.debito_automatico = Convert.ToInt32(resultado["DEBITO_AUTOMATICO"]);
                            if (resultado["DEBITO_AUTOMATICO_SEM"] != DBNull.Value) entidad.debito_automatico_sem = Convert.ToInt32(resultado["DEBITO_AUTOMATICO_SEM"]);
                            if (resultado["DESC_PROD_INACTIVOS"] != DBNull.Value) entidad.descuentos_productos_inact = Convert.ToBoolean(resultado["DESC_PROD_INACTIVOS"]);

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
                        BOExcepcion.Throw("EMPRESA_RECAUDOData", "ConsultarEMPRESA_RECAUDO", ex);
                        return null;
                    }
                }
            }
        }

        public List<EMPRESARECAUDO_CONCEPTO> ListarEMPRESACONCEPTO(EMPRESARECAUDO_CONCEPTO pConcepto, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<EMPRESARECAUDO_CONCEPTO> lstEmpresa = new List<EMPRESARECAUDO_CONCEPTO>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM EMPRESA_RECAUDO_CONCEPTO " + ObtenerFiltro(pConcepto) + " ORDER BY IDEMPCONCEPTO";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            EMPRESARECAUDO_CONCEPTO entidad = new EMPRESARECAUDO_CONCEPTO();
                            if (resultado["IDEMPCONCEPTO"] != DBNull.Value) entidad.idempconcepto = Convert.ToInt32(resultado["IDEMPCONCEPTO"]);
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt32(resultado["COD_EMPRESA"]);
                            if (resultado["TIPO_PRODUCTO"] != DBNull.Value) entidad.tipo_producto = Convert.ToInt32(resultado["TIPO_PRODUCTO"]);
                            if (resultado["COD_LINEA"] != DBNull.Value) entidad.cod_linea = Convert.ToString(resultado["COD_LINEA"]);
                            if (resultado["COD_CONCEPTO"] != DBNull.Value) entidad.cod_concepto = Convert.ToString(resultado["COD_CONCEPTO"]);
                            if (resultado["PRIORIDAD"] != DBNull.Value) entidad.prioridad = Convert.ToInt32(resultado["PRIORIDAD"]);
                            lstEmpresa.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstEmpresa;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpresaRecaudoData", "ListarEMPRESACONCEPTO", ex);
                        return null;
                    }
                }
            }
        }

        public EmpresaRecaudo_Programacion ConsultarEMPRESAPROGRAMACION(EmpresaRecaudo_Programacion pProgramacion, Usuario vUsuario)
        {
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM EMPRESA_RECAUDO_PROGRAMACION " + ObtenerFiltro(pProgramacion) + " ORDER BY IDPROGRAMACION ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            EmpresaRecaudo_Programacion entidad = new EmpresaRecaudo_Programacion();
                            if (resultado["IDPROGRAMACION"] != DBNull.Value) entidad.idprogramacion = Convert.ToInt32(resultado["IDPROGRAMACION"]);
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt32(resultado["COD_EMPRESA"]);
                            if (resultado["TIPO_LISTA"] != DBNull.Value) entidad.tipo_lista = Convert.ToInt32(resultado["TIPO_LISTA"]);
                            if (resultado["COD_PERIODICIDAD"] != DBNull.Value) entidad.cod_periodicidad = Convert.ToInt32(resultado["COD_PERIODICIDAD"]);
                            if (resultado["FECHA_INICIO"] != DBNull.Value) entidad.fecha_inicio = Convert.ToDateTime(resultado["FECHA_INICIO"]);
                            if (resultado["PRINCIPAL"] != DBNull.Value) entidad.principal = Convert.ToInt32(resultado["PRINCIPAL"]);
                            dbConnectionFactory.CerrarConexion(connection);
                            return entidad;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return null;
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }
            }
        }

        public List<EmpresaRecaudo_Programacion> ListarEMPRESAPROGRAMACION(EmpresaRecaudo_Programacion pProgramacion, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<EmpresaRecaudo_Programacion> lstProgra = new List<EmpresaRecaudo_Programacion>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM EMPRESA_RECAUDO_PROGRAMACION " + ObtenerFiltro(pProgramacion) + " ORDER BY IDPROGRAMACION ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            EmpresaRecaudo_Programacion entidad = new EmpresaRecaudo_Programacion();
                            if (resultado["IDPROGRAMACION"] != DBNull.Value) entidad.idprogramacion = Convert.ToInt32(resultado["IDPROGRAMACION"]);
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt32(resultado["COD_EMPRESA"]);
                            if (resultado["TIPO_LISTA"] != DBNull.Value) entidad.tipo_lista = Convert.ToInt32(resultado["TIPO_LISTA"]);
                            if (resultado["COD_PERIODICIDAD"] != DBNull.Value) entidad.cod_periodicidad = Convert.ToInt32(resultado["COD_PERIODICIDAD"]);
                            if (resultado["FECHA_INICIO"] != DBNull.Value) entidad.fecha_inicio = Convert.ToDateTime(resultado["FECHA_INICIO"]);
                            if (resultado["PRINCIPAL"] != DBNull.Value) entidad.principal = Convert.ToInt32(resultado["PRINCIPAL"]);                            
                            lstProgra.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstProgra;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpresaRecaudoData", "ListarEMPRESAPROGRAMACION", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarEmpresaPrograma(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter pidprogramacion = cmdTransaccionFactory.CreateParameter();
                        pidprogramacion.ParameterName = "p_idprogramacion";
                        pidprogramacion.Value = pId;
                        pidprogramacion.Direction = ParameterDirection.Input;
                        pidprogramacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidprogramacion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REC_EMPPROGRAM_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpresaRecaudoData", "EliminarEmpresaPrograma", ex);
                    }
                }
            }
        }

        public void EliminarEmpresaConcepto(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter pidempconcepto = cmdTransaccionFactory.CreateParameter();
                        pidempconcepto.ParameterName = "p_idempconcepto";
                        pidempconcepto.Value = pId;
                        pidempconcepto.Direction = ParameterDirection.Input;
                        pidempconcepto.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidempconcepto);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REC_EMPCONCEP_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpresaRecaudoData", "EliminarEmpresaConcepto", ex);
                    }
                }
            }
        }

    }
}




