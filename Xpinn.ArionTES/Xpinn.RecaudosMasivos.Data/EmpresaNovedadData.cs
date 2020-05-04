using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using System.Data;
using System.Data.Common;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Tesoreria.Entities;

namespace Xpinn.Tesoreria.Data
{
    public class EmpresaNovedadData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public EmpresaNovedadData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public void GenerarNovedades(EmpresaNovedad EmpresaNovedad, ref string pError, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter ptipo_recaudo = cmdTransaccionFactory.CreateParameter();
                        ptipo_recaudo.ParameterName = "ptipo_recaudo";
                        ptipo_recaudo.Value = EmpresaNovedad.tipo_recaudo;
                        ptipo_recaudo.DbType = DbType.Int32;
                        ptipo_recaudo.Direction = ParameterDirection.Input;

                        DbParameter pcod_empresa = cmdTransaccionFactory.CreateParameter();
                        pcod_empresa.ParameterName = "pcod_empresa";
                        pcod_empresa.Value = EmpresaNovedad.cod_empresa;
                        pcod_empresa.DbType = DbType.Int32;
                        pcod_empresa.Direction = ParameterDirection.Input;

                        DbParameter ptipo_lista = cmdTransaccionFactory.CreateParameter();
                        ptipo_lista.ParameterName = "ptipo_lista";
                        ptipo_lista.Value = EmpresaNovedad.tipo_lista;
                        ptipo_lista.DbType = DbType.Int32;
                        ptipo_lista.Direction = ParameterDirection.Input;

                        DbParameter pperiodo_corte = cmdTransaccionFactory.CreateParameter();
                        pperiodo_corte.ParameterName = "pperiodo_corte";
                        pperiodo_corte.Value = EmpresaNovedad.periodo_corte;
                        pperiodo_corte.DbType = DbType.DateTime;
                        pperiodo_corte.Direction = ParameterDirection.Input;

                        DbParameter pfecha_recaudo = cmdTransaccionFactory.CreateParameter();
                        pfecha_recaudo.ParameterName = "pfecha_recaudo";
                        pfecha_recaudo.Value = EmpresaNovedad.fecha_generacion;
                        pfecha_recaudo.DbType = DbType.DateTime;
                        pfecha_recaudo.Direction = ParameterDirection.Input;

                        DbParameter pusuario = cmdTransaccionFactory.CreateParameter();
                        pusuario.ParameterName = "pusuario";
                        pusuario.Value = vUsuario.nombre;
                        pusuario.DbType = DbType.String;
                        pusuario.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(ptipo_recaudo);
                        cmdTransaccionFactory.Parameters.Add(pcod_empresa);
                        cmdTransaccionFactory.Parameters.Add(ptipo_lista);
                        cmdTransaccionFactory.Parameters.Add(pperiodo_corte);
                        cmdTransaccionFactory.Parameters.Add(pfecha_recaudo);
                        cmdTransaccionFactory.Parameters.Add(pusuario);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REC_GENERAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                        //BOExcepcion.Throw("EmpresaNovedadData", "GenerarNovedades", ex);
                    }
                }
            }
        }

        public void EliminarVacaciones(long idBorrar, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_consecutivo = cmdTransaccionFactory.CreateParameter();
                        p_consecutivo.ParameterName = "p_consecutivo";
                        p_consecutivo.Value = idBorrar;
                        p_consecutivo.DbType = DbType.Int64;
                        p_consecutivo.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(p_consecutivo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REC_VACACIONES_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpresaNovedadData", "EliminarVacaciones", ex);
                    }
                }
            }
        }

        public EmpresaNovedad ConsultarVacaciones(string codVacaciones, Usuario usuario)
        {
            DbDataReader resultado = default(DbDataReader);
            EmpresaNovedad entidad = new EmpresaNovedad();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select vac.* , per.NOMBRE, per.TIPO_IDENTIFICACION
                                        from vacaciones vac
                                        join V_PERSONA per on vac.COD_PERSONA = per.COD_PERSONA 
                                        where vac.CONSECUTIVO = " + codVacaciones;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["Fecha_novedad"] != DBNull.Value) entidad.fecha_novedad = Convert.ToDateTime(resultado["Fecha_novedad"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["CODIGO_PAGADURIA"] != DBNull.Value) entidad.codigo_pagaduria = Convert.ToInt64(resultado["CODIGO_PAGADURIA"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.numero_cuotas = Convert.ToInt32(resultado["NUMERO_CUOTAS"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToInt32(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["TIPO_CALCULO"] != DBNull.Value) entidad.tipo_calculo = Convert.ToString(resultado["TIPO_CALCULO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["fecha_inicial"] != DBNull.Value) entidad.fecha_inicial = Convert.ToDateTime(resultado["fecha_inicial"]);
                            if (resultado["fecha_final"] != DBNull.Value) entidad.fecha_final = Convert.ToDateTime(resultado["fecha_final"]);
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpresaNovedadData", "ConsultarVacaciones", ex);
                        return null;
                    }
                }
            }
        }


        public EmpresaNovedad ConsultarPersonaVacaciones(string pFiltro, Usuario usuario)
        {
            DbDataReader resultado = default(DbDataReader);
            EmpresaNovedad entidad = null;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select vac.* , per.NOMBRE, per.TIPO_IDENTIFICACION
                                        from vacaciones vac
                                        join V_PERSONA per on vac.COD_PERSONA = per.COD_PERSONA " + pFiltro;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            entidad = new EmpresaNovedad();
                            if (resultado["Fecha_novedad"] != DBNull.Value) entidad.fecha_novedad = Convert.ToDateTime(resultado["Fecha_novedad"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["CODIGO_PAGADURIA"] != DBNull.Value) entidad.codigo_pagaduria = Convert.ToInt64(resultado["CODIGO_PAGADURIA"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.numero_cuotas = Convert.ToInt32(resultado["NUMERO_CUOTAS"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToInt32(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["TIPO_CALCULO"] != DBNull.Value) entidad.tipo_calculo = Convert.ToString(resultado["TIPO_CALCULO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["fecha_grabacion"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["fecha_grabacion"]);
                            if (resultado["fecha_inicial"] != DBNull.Value) entidad.fecha_inicial = Convert.ToDateTime(resultado["fecha_inicial"]);
                            if (resultado["fecha_final"] != DBNull.Value) entidad.fecha_final = Convert.ToDateTime(resultado["fecha_final"]);
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpresaNovedadData", "ConsultarVacaciones", ex);
                        return null;
                    }
                }
            }
        }

        public Vacaciones ModificarVacaciones(Vacaciones pVacaciones, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pVacaciones.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pVacaciones.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter p_codigo_pagaduria = cmdTransaccionFactory.CreateParameter();
                        p_codigo_pagaduria.ParameterName = "p_codigo_pagaduria";
                        p_codigo_pagaduria.Value = pVacaciones.codigo_pagaduria;
                        p_codigo_pagaduria.Direction = ParameterDirection.Input;
                        p_codigo_pagaduria.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_codigo_pagaduria);

                        DbParameter p_fecha_novedad = cmdTransaccionFactory.CreateParameter();
                        p_fecha_novedad.ParameterName = "p_fecha_novedad";
                        p_fecha_novedad.Value = pVacaciones.fecha_novedad;
                        p_fecha_novedad.Direction = ParameterDirection.Input;
                        p_fecha_novedad.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(p_fecha_novedad);

                        DbParameter pnumero_cuotas = cmdTransaccionFactory.CreateParameter();
                        pnumero_cuotas.ParameterName = "p_numero_cuotas";
                        pnumero_cuotas.Value = pVacaciones.numero_cuotas;
                        pnumero_cuotas.Direction = ParameterDirection.Input;
                        pnumero_cuotas.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_cuotas);

                        DbParameter pidentificacion = cmdTransaccionFactory.CreateParameter();
                        pidentificacion.ParameterName = "p_identificacion";
                        if (pVacaciones.identificacion == null)
                            pidentificacion.Value = DBNull.Value;
                        else
                            pidentificacion.Value = pVacaciones.identificacion;
                        pidentificacion.Direction = ParameterDirection.Input;
                        pidentificacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidentificacion);

                        DbParameter pfecha_grabacion = cmdTransaccionFactory.CreateParameter();
                        pfecha_grabacion.ParameterName = "p_fecha_grabacion";
                        if (pVacaciones.fecha_grabacion == null)
                            pfecha_grabacion.Value = DBNull.Value;
                        else
                            pfecha_grabacion.Value = pVacaciones.fecha_grabacion;
                        pfecha_grabacion.Direction = ParameterDirection.Input;
                        pfecha_grabacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_grabacion);

                        DbParameter p_tipo_calculo = cmdTransaccionFactory.CreateParameter();
                        p_tipo_calculo.ParameterName = "p_tipo_calculo";
                        p_tipo_calculo.Value = pVacaciones.tipo_calculo;
                        p_tipo_calculo.Direction = ParameterDirection.Input;
                        p_tipo_calculo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_tipo_calculo);

                        DbParameter pfecha_inicial = cmdTransaccionFactory.CreateParameter();
                        pfecha_inicial.ParameterName = "p_fecha_inicial";
                        if (pVacaciones.fecha_inicial == null)
                            pfecha_inicial.Value = DBNull.Value;
                        else
                            pfecha_inicial.Value = pVacaciones.fecha_inicial;
                        pfecha_inicial.Direction = ParameterDirection.Input;
                        pfecha_inicial.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_inicial);

                        DbParameter pfecha_final = cmdTransaccionFactory.CreateParameter();
                        pfecha_final.ParameterName = "p_fecha_final";
                        if (pVacaciones.fecha_final == null)
                            pfecha_final.Value = DBNull.Value;
                        else
                            pfecha_final.Value = pVacaciones.fecha_final;
                        pfecha_final.Direction = ParameterDirection.Input;
                        pfecha_final.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_final);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REC_VACACIONES_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pVacaciones;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("VacacionesData", "ModificarVacaciones", ex);
                        return null;
                    }
                }
            }
        }

        public List<EmpresaNovedad> ListarNovedadPersona(string cod_persona, Usuario usuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<EmpresaNovedad> lstEmpresaNovedad = new List<EmpresaNovedad>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select Distinct emp.NUMERO_NOVEDAD, emp.PERIODO_CORTE
                                        from empresa_novedad emp
                                        JOIN detempresa_novedad det on emp.NUMERO_NOVEDAD = det.NUMERO_NOVEDAD
                                        where det.COD_CLIENTE = " + cod_persona;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            EmpresaNovedad entidad = new EmpresaNovedad();

                            if (resultado["NUMERO_NOVEDAD"] != DBNull.Value) entidad.numero_novedad = Convert.ToInt64(resultado["NUMERO_NOVEDAD"]);
                            if (resultado["PERIODO_CORTE"] != DBNull.Value) entidad.descripcion += entidad.numero_novedad + " - Corte => " + (Convert.ToDateTime(resultado["PERIODO_CORTE"])).ToShortDateString();

                            lstEmpresaNovedad.Add(entidad);
                        }

                        return lstEmpresaNovedad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpresaNovedadData", "ListarNovedadPersona", ex);
                        return null;
                    }
                }
            }
        }


        public void GenerarNovedadesNuevas(EmpresaNovedad EmpresaNovedad, ref string pError, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter ptipo_recaudo = cmdTransaccionFactory.CreateParameter();
                        ptipo_recaudo.ParameterName = "ptipo_recaudo";
                        ptipo_recaudo.Value = EmpresaNovedad.tipo_recaudo;
                        ptipo_recaudo.DbType = DbType.Int32;
                        ptipo_recaudo.Direction = ParameterDirection.Input;

                        DbParameter pcod_empresa = cmdTransaccionFactory.CreateParameter();
                        pcod_empresa.ParameterName = "pcod_empresa";
                        pcod_empresa.Value = EmpresaNovedad.cod_empresa;
                        pcod_empresa.DbType = DbType.Int32;
                        pcod_empresa.Direction = ParameterDirection.Input;

                        DbParameter ptipo_lista = cmdTransaccionFactory.CreateParameter();
                        ptipo_lista.ParameterName = "ptipo_lista";
                        ptipo_lista.Value = EmpresaNovedad.tipo_lista;
                        ptipo_lista.DbType = DbType.Int32;
                        ptipo_lista.Direction = ParameterDirection.Input;

                        DbParameter pfecha_inicial = cmdTransaccionFactory.CreateParameter();
                        pfecha_inicial.ParameterName = "pfecha_inicial";
                        pfecha_inicial.Value = EmpresaNovedad.fecha_inicial;
                        pfecha_inicial.DbType = DbType.DateTime;
                        pfecha_inicial.Direction = ParameterDirection.Input;

                        DbParameter pfecha_final = cmdTransaccionFactory.CreateParameter();
                        pfecha_final.ParameterName = "pfecha_final";
                        pfecha_final.Value = EmpresaNovedad.periodo_corte;
                        pfecha_final.DbType = DbType.DateTime;
                        pfecha_final.Direction = ParameterDirection.Input;

                        DbParameter pfecha_recaudo = cmdTransaccionFactory.CreateParameter();
                        pfecha_recaudo.ParameterName = "pfecha_recaudo";
                        pfecha_recaudo.Value = EmpresaNovedad.fecha_generacion;
                        pfecha_recaudo.DbType = DbType.DateTime;
                        pfecha_recaudo.Direction = ParameterDirection.Input;

                        DbParameter pusuario = cmdTransaccionFactory.CreateParameter();
                        pusuario.ParameterName = "pusuario";
                        pusuario.Value = vUsuario.nombre;
                        pusuario.DbType = DbType.String;
                        pusuario.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(ptipo_recaudo);
                        cmdTransaccionFactory.Parameters.Add(pcod_empresa);
                        cmdTransaccionFactory.Parameters.Add(ptipo_lista);
                        cmdTransaccionFactory.Parameters.Add(pfecha_inicial);
                        cmdTransaccionFactory.Parameters.Add(pfecha_final);
                        cmdTransaccionFactory.Parameters.Add(pfecha_recaudo);
                        cmdTransaccionFactory.Parameters.Add(pusuario);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REC_GENERAR_NUEVAS";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                        //BOExcepcion.Throw("EmpresaNovedadData", "GenerarNovedadesNuevas", ex);
                    }
                }
            }
        }


        public List<EmpresaNovedad> ListarTempNovedades(EmpresaNovedad Recaudo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<EmpresaNovedad> lstEmpresaNovedad = new List<EmpresaNovedad>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    EmpresaNovedad entidad;
                    try
                    {
                        string sql = "Select * from TEMP_RECAUDO order by NOM_TIPO_PRODUCTO";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            entidad = new EmpresaNovedad();
                            if (resultado["IDDETALLE"] != DBNull.Value) entidad.iddetalle = Convert.ToInt32(resultado["IDDETALLE"]);
                            if (resultado["TIPO_PRODUCTO"] != DBNull.Value) entidad.tipo_productotemp = Convert.ToInt32(resultado["TIPO_PRODUCTO"]);
                            if (resultado["NOM_TIPO_PRODUCTO"] != DBNull.Value) entidad.nom_tipo_producto = Convert.ToString(resultado["NOM_TIPO_PRODUCTO"]).Trim();
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["LINEA"]);
                            if (resultado["NOM_LINEA"] != DBNull.Value) entidad.nom_linea = Convert.ToString(resultado["NOM_LINEA"]);
                            if (resultado["NUMERO_PRODUCTO"] != DBNull.Value) entidad.numero_producto = Convert.ToString(resultado["NUMERO_PRODUCTO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["APELLIDOS"] != DBNull.Value) entidad.apellidos = Convert.ToString(resultado["APELLIDOS"]);
                            if (resultado["NOMBRES"] != DBNull.Value)
                            {
                                string[] sNombres;
                                sNombres = Convert.ToString(resultado["NOMBRES"]).Split(' ');
                                entidad.nombres1 = sNombres[0].Trim();
                                entidad.nombres2 = sNombres.Length > 1 ? sNombres[1].Trim() : null;
                            }
                            if (resultado["APELLIDOS"] != DBNull.Value)
                            {
                                string[] sApellidos;
                                sApellidos = Convert.ToString(resultado["APELLIDOS"]).Split(' ');
                                entidad.apellidos1 = sApellidos[0].Trim();
                                entidad.apellidos2 = sApellidos.Length > 1 ? sApellidos[1].Trim() : null;
                            }
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["SALDO_PRODUCTO"] != DBNull.Value) entidad.saldo = Convert.ToDecimal(resultado["SALDO_PRODUCTO"]);
                            if (resultado["COD_NOMINA"] != DBNull.Value) entidad.cod_nomina_empleado = Convert.ToString(resultado["COD_NOMINA"]);
                            if (resultado["VALOR_CAPITAL"] != DBNull.Value) entidad.capital = Convert.ToDecimal(resultado["VALOR_CAPITAL"]);
                            if (resultado["INTERES_CTE"] != DBNull.Value) entidad.intcte = Convert.ToDecimal(resultado["INTERES_CTE"]);
                            if (resultado["INTERES_MORA"] != DBNull.Value) entidad.intmora = Convert.ToDecimal(resultado["INTERES_MORA"]);
                            if (resultado["VALOR_SEGURO"] != DBNull.Value) entidad.seguro = Convert.ToDecimal(resultado["VALOR_SEGURO"]);
                            if (resultado["INTERES_OTROS"] != DBNull.Value) entidad.otros = Convert.ToDecimal(resultado["INTERES_OTROS"]);
                            if (resultado["TOTAL_FIJOS"] != DBNull.Value) entidad.total_fijos = Convert.ToDecimal(resultado["TOTAL_FIJOS"]);
                            if (resultado["TOTAL_PRESTAMOS"] != DBNull.Value) entidad.total_prestamos = Convert.ToDecimal(resultado["TOTAL_PRESTAMOS"]);
                            if (resultado["VACACIONES"] != DBNull.Value) entidad.vacaciones = Convert.ToInt64(resultado["VACACIONES"]);
                            if (resultado["FECHA_INICIO_PRODUCTO"] != DBNull.Value) entidad.fecha_inicio_producto = Convert.ToDateTime(resultado["FECHA_INICIO_PRODUCTO"]);
                            if (resultado["FECHA_VENCIMIENTO_PRODUCTO"] != DBNull.Value) entidad.fecha_vencimiento_producto = Convert.ToDateTime(resultado["FECHA_VENCIMIENTO_PRODUCTO"]);

                            //entidad.linea = (entidad.linea + "-" + entidad.nom_linea).Trim();
                            lstEmpresaNovedad.Add(entidad);
                        }

                        return lstEmpresaNovedad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpresaNovedadData", "ListarTempNovedades", ex);
                        return null;
                    }
                }
            }
        }


        public Vacaciones InsertarVacaciones(Vacaciones pVacaciones, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pVacaciones.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Output;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pVacaciones.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter p_codigo_pagaduria = cmdTransaccionFactory.CreateParameter();
                        p_codigo_pagaduria.ParameterName = "p_codigo_pagaduria";
                        p_codigo_pagaduria.Value = pVacaciones.codigo_pagaduria;
                        p_codigo_pagaduria.Direction = ParameterDirection.Input;
                        p_codigo_pagaduria.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_codigo_pagaduria);

                        DbParameter p_fecha_novedad = cmdTransaccionFactory.CreateParameter();
                        p_fecha_novedad.ParameterName = "p_fecha_novedad";
                        p_fecha_novedad.Value = pVacaciones.fecha_novedad;
                        p_fecha_novedad.Direction = ParameterDirection.Input;
                        p_fecha_novedad.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(p_fecha_novedad);

                        DbParameter pnumero_cuotas = cmdTransaccionFactory.CreateParameter();
                        pnumero_cuotas.ParameterName = "p_numero_cuotas";
                        pnumero_cuotas.Value = pVacaciones.numero_cuotas;
                        pnumero_cuotas.Direction = ParameterDirection.Input;
                        pnumero_cuotas.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_cuotas);

                        DbParameter pidentificacion = cmdTransaccionFactory.CreateParameter();
                        pidentificacion.ParameterName = "p_identificacion";
                        if (pVacaciones.identificacion == null)
                            pidentificacion.Value = DBNull.Value;
                        else
                            pidentificacion.Value = pVacaciones.identificacion;
                        pidentificacion.Direction = ParameterDirection.Input;
                        pidentificacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidentificacion);

                        DbParameter pfecha_grabacion = cmdTransaccionFactory.CreateParameter();
                        pfecha_grabacion.ParameterName = "p_fecha_grabacion";
                        if (pVacaciones.fecha_grabacion == null)
                            pfecha_grabacion.Value = DBNull.Value;
                        else
                            pfecha_grabacion.Value = pVacaciones.fecha_grabacion;
                        pfecha_grabacion.Direction = ParameterDirection.Input;
                        pfecha_grabacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_grabacion);
                        
                        DbParameter p_tipo_calculo = cmdTransaccionFactory.CreateParameter();
                        p_tipo_calculo.ParameterName = "p_tipo_calculo";
                        p_tipo_calculo.Value = pVacaciones.tipo_calculo;
                        p_tipo_calculo.Direction = ParameterDirection.Input;
                        p_tipo_calculo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_tipo_calculo);

                        DbParameter pfecha_inicial = cmdTransaccionFactory.CreateParameter();
                        pfecha_inicial.ParameterName = "p_fecha_inicial";
                        if (pVacaciones.fecha_inicial == null)
                            pfecha_inicial.Value = DBNull.Value;
                        else
                            pfecha_inicial.Value = pVacaciones.fecha_inicial;
                        pfecha_inicial.Direction = ParameterDirection.Input;
                        pfecha_inicial.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_inicial);

                        DbParameter pfecha_final = cmdTransaccionFactory.CreateParameter();
                        pfecha_final.ParameterName = "p_fecha_final";
                        if (pVacaciones.fecha_final == null)
                            pfecha_final.Value = DBNull.Value;
                        else
                            pfecha_final.Value = pVacaciones.fecha_final;
                        pfecha_final.Direction = ParameterDirection.Input;
                        pfecha_final.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_final);

                        DbParameter pcod_usuario = cmdTransaccionFactory.CreateParameter();
                        pcod_usuario.ParameterName = "p_cod_usuario";
                        pcod_usuario.Value = usuario.codusuario;
                        pcod_usuario.Direction = ParameterDirection.Input;
                        pcod_usuario.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_usuario);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REC_VACACIONES_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pVacaciones.consecutivo = pconsecutivo.Value != DBNull.Value ? Convert.ToInt64(pconsecutivo.Value) : 0;

                        dbConnectionFactory.CerrarConexion(connection);
                        return pVacaciones;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpresaNovedadData", "InsertarVacaciones", ex);
                        return null;
                    }
                }
            }
        }


        public List<EmpresaNovedad> ListarTempNovedadesNuevas(EmpresaNovedad Recaudo, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<EmpresaNovedad> lstNovedades = new List<EmpresaNovedad>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM TEMP_RECAUDO_NUEVO ORDER BY IDDETALLE ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            EmpresaNovedad entidad = new EmpresaNovedad();
                            if (resultado["TIPO_PRODUCTO"] != DBNull.Value) entidad.tipo_productotemp = Convert.ToInt32(resultado["TIPO_PRODUCTO"]);
                            if (resultado["NOM_TIPO_PRODUCTO"] != DBNull.Value) entidad.nom_tipo_producto = entidad.tipo_productotemp + "-" + Convert.ToString(resultado["NOM_TIPO_PRODUCTO"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["LINEA"]);
                            if (resultado["NOM_LINEA"] != DBNull.Value) entidad.nom_linea = Convert.ToString(resultado["NOM_LINEA"]);
                            if (resultado["NUMERO_PRODUCTO"] != DBNull.Value) entidad.numero_producto = Convert.ToString(resultado["NUMERO_PRODUCTO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["APELLIDOS"] != DBNull.Value) entidad.apellidos = Convert.ToString(resultado["APELLIDOS"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["IDDETALLE"] != DBNull.Value) entidad.iddetalle = Convert.ToInt32(resultado["IDDETALLE"]);
                            if (resultado["TIPO_NOVEDAD"] != DBNull.Value) entidad.tipo_novedad = Convert.ToString(resultado["TIPO_NOVEDAD"]);
                            if (resultado["FECHA_NOVEDAD"] != DBNull.Value) entidad.fecha_inicial = Convert.ToDateTime(resultado["FECHA_NOVEDAD"]);
                            if (resultado["FECHA_FIN_NOVEDAD"] != DBNull.Value) entidad.fecha_final = Convert.ToDateTime(resultado["FECHA_FIN_NOVEDAD"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.numero_cuotas = Convert.ToInt32(resultado["NUMERO_CUOTAS"]);
                            if (resultado["VALOR_TOTAL"] != DBNull.Value) entidad.valor_total = Convert.ToDecimal(resultado["VALOR_TOTAL"]);
                            entidad.linea = (entidad.linea + "-" + entidad.nom_linea).Trim();
                            lstNovedades.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstNovedades;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpresaNovedadData", "ListarTempNovedadesNuevas", ex);
                        return null;
                    }
                }
            }
        }


        public EmpresaNovedad CREAR_TEMP_RECAUDO(EmpresaNovedad pTemp, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter ptipo_producto = cmdTransaccionFactory.CreateParameter();
                        ptipo_producto.ParameterName = "p_tipo_producto";
                        ptipo_producto.Value = pTemp.tipo_productotemp;
                        ptipo_producto.Direction = ParameterDirection.Input;
                        ptipo_producto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_producto);

                        DbParameter pnom_tipo_producto = cmdTransaccionFactory.CreateParameter();
                        pnom_tipo_producto.ParameterName = "p_nom_tipo_producto";
                        pnom_tipo_producto.Value = pTemp.nom_tipo_producto;
                        pnom_tipo_producto.Direction = ParameterDirection.Input;
                        pnom_tipo_producto.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnom_tipo_producto);

                        DbParameter plinea = cmdTransaccionFactory.CreateParameter();
                        plinea.ParameterName = "p_linea";
                        if (pTemp.linea == null)
                            plinea.Value = DBNull.Value;
                        else
                            plinea.Value = pTemp.linea;
                        plinea.Direction = ParameterDirection.Input;
                        plinea.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(plinea);

                        DbParameter pnom_linea = cmdTransaccionFactory.CreateParameter();
                        pnom_linea.ParameterName = "p_nom_linea";
                        if (pTemp.nom_linea == null)
                            pnom_linea.Value = DBNull.Value;
                        else
                            pnom_linea.Value = pTemp.nom_linea;
                        pnom_linea.Direction = ParameterDirection.Input;
                        pnom_linea.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnom_linea);

                        DbParameter pnumero_producto = cmdTransaccionFactory.CreateParameter();
                        pnumero_producto.ParameterName = "p_numero_producto";
                        if (pTemp.numero_producto == null)
                            pnumero_producto.Value = DBNull.Value;
                        else
                            pnumero_producto.Value = pTemp.numero_producto;
                        pnumero_producto.Direction = ParameterDirection.Input;
                        pnumero_producto.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumero_producto);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pTemp.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pidentificacion = cmdTransaccionFactory.CreateParameter();
                        pidentificacion.ParameterName = "p_identificacion";
                        if (pTemp.identificacion == null)
                            pidentificacion.Value = DBNull.Value;
                        else
                            pidentificacion.Value = pTemp.identificacion;
                        pidentificacion.Direction = ParameterDirection.Input;
                        pidentificacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidentificacion);

                        DbParameter pnombres = cmdTransaccionFactory.CreateParameter();
                        pnombres.ParameterName = "p_nombres";
                        if (pTemp.nombres == null)
                            pnombres.Value = DBNull.Value;
                        else
                            pnombres.Value = pTemp.nombres;
                        pnombres.Direction = ParameterDirection.Input;
                        pnombres.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombres);

                        DbParameter papellidos = cmdTransaccionFactory.CreateParameter();
                        papellidos.ParameterName = "p_apellidos";
                        if (pTemp.apellidos == null)
                            papellidos.Value = DBNull.Value;
                        else
                            papellidos.Value = pTemp.apellidos;
                        papellidos.Direction = ParameterDirection.Input;
                        papellidos.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(papellidos);

                        DbParameter pfecha_proximo_pago = cmdTransaccionFactory.CreateParameter();
                        pfecha_proximo_pago.ParameterName = "p_fecha_proximo_pago";
                        if (pTemp.fecha_proximo_pago == null)
                            pfecha_proximo_pago.Value = DBNull.Value;
                        else
                            pfecha_proximo_pago.Value = pTemp.fecha_proximo_pago;
                        pfecha_proximo_pago.Direction = ParameterDirection.Input;
                        pfecha_proximo_pago.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_proximo_pago);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        pvalor.Value = pTemp.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter piddetalle = cmdTransaccionFactory.CreateParameter();
                        piddetalle.ParameterName = "p_iddetalle";
                        piddetalle.Value = pTemp.iddetalle;
                        piddetalle.Direction = ParameterDirection.Output;
                        piddetalle.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(piddetalle);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REC_TEMP_RECAU_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pTemp.iddetalle = Convert.ToInt64(piddetalle.Value);
                        return pTemp;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpresaNovedadData", "CREAR_TEMP_RECAUDO", ex);
                        return null;
                    }
                }
            }
        }

        public EmpresaNovedad MODIFICAR_TEMP_RECAUDO(EmpresaNovedad pTemp, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter ptipo_producto = cmdTransaccionFactory.CreateParameter();
                        ptipo_producto.ParameterName = "p_tipo_producto";
                        ptipo_producto.Value = pTemp.tipo_productotemp;
                        ptipo_producto.Direction = ParameterDirection.Input;
                        ptipo_producto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_producto);

                        DbParameter pnom_tipo_producto = cmdTransaccionFactory.CreateParameter();
                        pnom_tipo_producto.ParameterName = "p_nom_tipo_producto";
                        if (pTemp.nom_tipo_producto == null)
                            pnom_tipo_producto.Value = DBNull.Value;
                        else
                            pnom_tipo_producto.Value = pTemp.nom_tipo_producto;
                        pnom_tipo_producto.Direction = ParameterDirection.Input;
                        pnom_tipo_producto.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnom_tipo_producto);

                        DbParameter plinea = cmdTransaccionFactory.CreateParameter();
                        plinea.ParameterName = "p_linea";
                        if (pTemp.linea == null)
                            plinea.Value = DBNull.Value;
                        else
                            plinea.Value = pTemp.linea;
                        plinea.Direction = ParameterDirection.Input;
                        plinea.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(plinea);

                        DbParameter pnom_linea = cmdTransaccionFactory.CreateParameter();
                        pnom_linea.ParameterName = "p_nom_linea";
                        if (pTemp.nom_linea == null)
                            pnom_linea.Value = DBNull.Value;
                        else
                            pnom_linea.Value = pTemp.nom_linea;
                        pnom_linea.Direction = ParameterDirection.Input;
                        pnom_linea.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnom_linea);

                        DbParameter pnumero_producto = cmdTransaccionFactory.CreateParameter();
                        pnumero_producto.ParameterName = "p_numero_producto";
                        if (pTemp.numero_producto == null)
                            pnumero_producto.Value = DBNull.Value;
                        else
                            pnumero_producto.Value = pTemp.numero_producto;
                        pnumero_producto.Direction = ParameterDirection.Input;
                        pnumero_producto.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumero_producto);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pTemp.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pidentificacion = cmdTransaccionFactory.CreateParameter();
                        pidentificacion.ParameterName = "p_identificacion";
                        if (pTemp.identificacion == null)
                            pidentificacion.Value = DBNull.Value;
                        else
                            pidentificacion.Value = pTemp.identificacion;
                        pidentificacion.Direction = ParameterDirection.Input;
                        pidentificacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidentificacion);

                        DbParameter pnombres = cmdTransaccionFactory.CreateParameter();
                        pnombres.ParameterName = "p_nombres";
                        if (pTemp.nombres == null)
                            pnombres.Value = DBNull.Value;
                        else
                            pnombres.Value = pTemp.nombres;
                        pnombres.Direction = ParameterDirection.Input;
                        pnombres.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombres);

                        DbParameter papellidos = cmdTransaccionFactory.CreateParameter();
                        papellidos.ParameterName = "p_apellidos";
                        if (pTemp.apellidos == null)
                            papellidos.Value = DBNull.Value;
                        else
                            papellidos.Value = pTemp.apellidos;
                        papellidos.Direction = ParameterDirection.Input;
                        papellidos.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(papellidos);

                        DbParameter pfecha_proximo_pago = cmdTransaccionFactory.CreateParameter();
                        pfecha_proximo_pago.ParameterName = "p_fecha_proximo_pago";
                        if (pTemp.fecha_proximo_pago == null)
                            pfecha_proximo_pago.Value = DBNull.Value;
                        else
                            pfecha_proximo_pago.Value = pTemp.fecha_proximo_pago;
                        pfecha_proximo_pago.Direction = ParameterDirection.Input;
                        pfecha_proximo_pago.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_proximo_pago);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        pvalor.Value = pTemp.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter piddetalle = cmdTransaccionFactory.CreateParameter();
                        piddetalle.ParameterName = "p_iddetalle";
                        piddetalle.Value = pTemp.iddetalle;
                        piddetalle.Direction = ParameterDirection.Input;
                        piddetalle.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(piddetalle);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REC_TEMP_RECAU_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pTemp;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpresaNovedadData", "MODIFICAR_TEMP_RECAUDO", ex);
                        return null;
                    }
                }
            }
        }


        public EmpresaNovedad CREAR_TEMP_RECAUDO_NUEVO(EmpresaNovedad pTemp, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter piddetalle = cmdTransaccionFactory.CreateParameter();
                        piddetalle.ParameterName = "p_iddetalle";
                        piddetalle.Value = pTemp.iddetalle;
                        piddetalle.Direction = ParameterDirection.Output;
                        piddetalle.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(piddetalle);

                        DbParameter ptipo_producto = cmdTransaccionFactory.CreateParameter();
                        ptipo_producto.ParameterName = "p_tipo_producto";
                        if (pTemp.tipo_productotemp == null)
                            ptipo_producto.Value = DBNull.Value;
                        else
                            ptipo_producto.Value = pTemp.tipo_productotemp;
                        ptipo_producto.Direction = ParameterDirection.Input;
                        ptipo_producto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_producto);

                        DbParameter pnom_tipo_producto = cmdTransaccionFactory.CreateParameter();
                        pnom_tipo_producto.ParameterName = "p_nom_tipo_producto";
                        if (pTemp.nom_tipo_producto == null)
                            pnom_tipo_producto.Value = DBNull.Value;
                        else
                            pnom_tipo_producto.Value = pTemp.nom_tipo_producto;
                        pnom_tipo_producto.Direction = ParameterDirection.Input;
                        pnom_tipo_producto.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnom_tipo_producto);

                        DbParameter plinea = cmdTransaccionFactory.CreateParameter();
                        plinea.ParameterName = "p_linea";
                        if (pTemp.linea == null)
                            plinea.Value = DBNull.Value;
                        else
                            plinea.Value = pTemp.linea;
                        plinea.Direction = ParameterDirection.Input;
                        plinea.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(plinea);

                        DbParameter pnom_linea = cmdTransaccionFactory.CreateParameter();
                        pnom_linea.ParameterName = "p_nom_linea";
                        if (pTemp.nom_linea == null)
                            pnom_linea.Value = DBNull.Value;
                        else
                            pnom_linea.Value = pTemp.nom_linea;
                        pnom_linea.Direction = ParameterDirection.Input;
                        pnom_linea.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnom_linea);

                        DbParameter pnumero_producto = cmdTransaccionFactory.CreateParameter();
                        pnumero_producto.ParameterName = "p_numero_producto";
                        if (pTemp.numero_producto == null)
                            pnumero_producto.Value = DBNull.Value;
                        else
                            pnumero_producto.Value = pTemp.numero_producto;
                        pnumero_producto.Direction = ParameterDirection.Input;
                        pnumero_producto.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumero_producto);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        if (pTemp.cod_persona == null)
                            pcod_persona.Value = DBNull.Value;
                        else
                            pcod_persona.Value = pTemp.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pidentificacion = cmdTransaccionFactory.CreateParameter();
                        pidentificacion.ParameterName = "p_identificacion";
                        if (pTemp.identificacion == null)
                            pidentificacion.Value = DBNull.Value;
                        else
                            pidentificacion.Value = pTemp.identificacion;
                        pidentificacion.Direction = ParameterDirection.Input;
                        pidentificacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidentificacion);

                        DbParameter pnombres = cmdTransaccionFactory.CreateParameter();
                        pnombres.ParameterName = "p_nombres";
                        if (pTemp.nombres == null)
                            pnombres.Value = DBNull.Value;
                        else
                            pnombres.Value = pTemp.nombres;
                        pnombres.Direction = ParameterDirection.Input;
                        pnombres.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombres);

                        DbParameter papellidos = cmdTransaccionFactory.CreateParameter();
                        papellidos.ParameterName = "p_apellidos";
                        if (pTemp.apellidos == null)
                            papellidos.Value = DBNull.Value;
                        else
                            papellidos.Value = pTemp.apellidos;
                        papellidos.Direction = ParameterDirection.Input;
                        papellidos.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(papellidos);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        if (pTemp.valor == null)
                            pvalor.Value = DBNull.Value;
                        else
                            pvalor.Value = pTemp.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter ptipo_novedad = cmdTransaccionFactory.CreateParameter();
                        ptipo_novedad.ParameterName = "p_tipo_novedad";
                        ptipo_novedad.Value = pTemp.tipo_novedad;
                        ptipo_novedad.Direction = ParameterDirection.Input;
                        ptipo_novedad.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipo_novedad);

                        DbParameter pfecha_novedad = cmdTransaccionFactory.CreateParameter();
                        pfecha_novedad.ParameterName = "p_fecha_novedad";
                        if (pTemp.fecha_inicial == null)
                            pfecha_novedad.Value = DBNull.Value;
                        else
                            pfecha_novedad.Value = pTemp.fecha_inicial;
                        pfecha_novedad.Direction = ParameterDirection.Input;
                        pfecha_novedad.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_novedad);

                        DbParameter pfecha_fin_novedad = cmdTransaccionFactory.CreateParameter();
                        pfecha_fin_novedad.ParameterName = "p_fecha_fin_novedad";
                        if (pTemp.fecha_final == null)
                            pfecha_fin_novedad.Value = DBNull.Value;
                        else
                            pfecha_fin_novedad.Value = pTemp.fecha_final;
                        pfecha_fin_novedad.Direction = ParameterDirection.Input;
                        pfecha_fin_novedad.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_fin_novedad);

                        DbParameter pnumero_cuotas = cmdTransaccionFactory.CreateParameter();
                        pnumero_cuotas.ParameterName = "p_numero_cuotas";
                        pnumero_cuotas.Value = pTemp.numero_cuotas;
                        pnumero_cuotas.Direction = ParameterDirection.Input;
                        pnumero_cuotas.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_cuotas);

                        DbParameter pvalor_total = cmdTransaccionFactory.CreateParameter();
                        pvalor_total.ParameterName = "p_valor_total";
                        pvalor_total.Value = pTemp.valor_total;
                        pvalor_total.Direction = ParameterDirection.Input;
                        pvalor_total.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_total);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REC_TEMPRECNEW_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pTemp.iddetalle = Convert.ToInt64(piddetalle.Value);
                        return pTemp;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpresaNovedadData", "CREAR_TEMP_RECAUDO_NUEVO", ex);
                        return null;
                    }
                }
            }
        }
        public EmpresaNovedad MODIFICAR_TEMP_RECAUDO_NUEVO(EmpresaNovedad pTemp, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter piddetalle = cmdTransaccionFactory.CreateParameter();
                        piddetalle.ParameterName = "p_iddetalle";
                        piddetalle.Value = pTemp.iddetalle;
                        piddetalle.Direction = ParameterDirection.Input;
                        piddetalle.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(piddetalle);

                        DbParameter ptipo_producto = cmdTransaccionFactory.CreateParameter();
                        ptipo_producto.ParameterName = "p_tipo_producto";
                        if (pTemp.tipo_productotemp == null)
                            ptipo_producto.Value = DBNull.Value;
                        else
                            ptipo_producto.Value = pTemp.tipo_productotemp;
                        ptipo_producto.Direction = ParameterDirection.Input;
                        ptipo_producto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_producto);

                        DbParameter pnom_tipo_producto = cmdTransaccionFactory.CreateParameter();
                        pnom_tipo_producto.ParameterName = "p_nom_tipo_producto";
                        if (pTemp.nom_tipo_producto == null)
                            pnom_tipo_producto.Value = DBNull.Value;
                        else
                            pnom_tipo_producto.Value = pTemp.nom_tipo_producto;
                        pnom_tipo_producto.Direction = ParameterDirection.Input;
                        pnom_tipo_producto.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnom_tipo_producto);

                        DbParameter plinea = cmdTransaccionFactory.CreateParameter();
                        plinea.ParameterName = "p_linea";
                        if (pTemp.linea == null)
                            plinea.Value = DBNull.Value;
                        else
                            plinea.Value = pTemp.linea;
                        plinea.Direction = ParameterDirection.Input;
                        plinea.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(plinea);

                        DbParameter pnom_linea = cmdTransaccionFactory.CreateParameter();
                        pnom_linea.ParameterName = "p_nom_linea";
                        if (pTemp.nom_linea == null)
                            pnom_linea.Value = DBNull.Value;
                        else
                            pnom_linea.Value = pTemp.nom_linea;
                        pnom_linea.Direction = ParameterDirection.Input;
                        pnom_linea.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnom_linea);

                        DbParameter pnumero_producto = cmdTransaccionFactory.CreateParameter();
                        pnumero_producto.ParameterName = "p_numero_producto";
                        if (pTemp.numero_producto == null)
                            pnumero_producto.Value = DBNull.Value;
                        else
                            pnumero_producto.Value = pTemp.numero_producto;
                        pnumero_producto.Direction = ParameterDirection.Input;
                        pnumero_producto.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumero_producto);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        if (pTemp.cod_persona == null)
                            pcod_persona.Value = DBNull.Value;
                        else
                            pcod_persona.Value = pTemp.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pidentificacion = cmdTransaccionFactory.CreateParameter();
                        pidentificacion.ParameterName = "p_identificacion";
                        if (pTemp.identificacion == null)
                            pidentificacion.Value = DBNull.Value;
                        else
                            pidentificacion.Value = pTemp.identificacion;
                        pidentificacion.Direction = ParameterDirection.Input;
                        pidentificacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidentificacion);

                        DbParameter pnombres = cmdTransaccionFactory.CreateParameter();
                        pnombres.ParameterName = "p_nombres";
                        if (pTemp.nombres == null)
                            pnombres.Value = DBNull.Value;
                        else
                            pnombres.Value = pTemp.nombres;
                        pnombres.Direction = ParameterDirection.Input;
                        pnombres.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombres);

                        DbParameter papellidos = cmdTransaccionFactory.CreateParameter();
                        papellidos.ParameterName = "p_apellidos";
                        if (pTemp.apellidos == null)
                            papellidos.Value = DBNull.Value;
                        else
                            papellidos.Value = pTemp.apellidos;
                        papellidos.Direction = ParameterDirection.Input;
                        papellidos.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(papellidos);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        if (pTemp.valor == null)
                            pvalor.Value = DBNull.Value;
                        else
                            pvalor.Value = pTemp.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter ptipo_novedad = cmdTransaccionFactory.CreateParameter();
                        ptipo_novedad.ParameterName = "p_tipo_novedad";
                        ptipo_novedad.Value = pTemp.tipo_novedad;
                        ptipo_novedad.Direction = ParameterDirection.Input;
                        ptipo_novedad.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipo_novedad);

                        DbParameter pfecha_novedad = cmdTransaccionFactory.CreateParameter();
                        pfecha_novedad.ParameterName = "p_fecha_novedad";
                        if (pTemp.fecha_inicial == null)
                            pfecha_novedad.Value = DBNull.Value;
                        else
                            pfecha_novedad.Value = pTemp.fecha_inicial;
                        pfecha_novedad.Direction = ParameterDirection.Input;
                        pfecha_novedad.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_novedad);

                        DbParameter pfecha_fin_novedad = cmdTransaccionFactory.CreateParameter();
                        pfecha_fin_novedad.ParameterName = "p_fecha_fin_novedad";
                        if (pTemp.fecha_final == null)
                            pfecha_fin_novedad.Value = DBNull.Value;
                        else
                            pfecha_fin_novedad.Value = pTemp.fecha_final;
                        pfecha_fin_novedad.Direction = ParameterDirection.Input;
                        pfecha_fin_novedad.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_fin_novedad);

                        DbParameter pnumero_cuotas = cmdTransaccionFactory.CreateParameter();
                        pnumero_cuotas.ParameterName = "p_numero_cuotas";
                        pnumero_cuotas.Value = pTemp.numero_cuotas;
                        pnumero_cuotas.Direction = ParameterDirection.Input;
                        pnumero_cuotas.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_cuotas);

                        DbParameter pvalor_total = cmdTransaccionFactory.CreateParameter();
                        pvalor_total.ParameterName = "p_valor_total";
                        pvalor_total.Value = pTemp.valor_total;
                        pvalor_total.Direction = ParameterDirection.Input;
                        pvalor_total.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_total);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REC_TEMPRECNEW_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pTemp;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpresaNovedadData", "MODIFICAR_TEMP_RECAUDO_NUEVO", ex);
                        return null;
                    }
                }
            }
        }

        public EmpresaNovedad CrearRecaudosGeneracion(EmpresaNovedad pRecaudos, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pnumero_novedad = cmdTransaccionFactory.CreateParameter();
                        pnumero_novedad.ParameterName = "pnumero_novedad";
                        pnumero_novedad.Value = pRecaudos.numero_novedad;
                        pnumero_novedad.Direction = ParameterDirection.Output;
                        pnumero_novedad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_novedad);

                        DbParameter ptipo_recaudo = cmdTransaccionFactory.CreateParameter();
                        ptipo_recaudo.ParameterName = "ptipo_recaudo";
                        ptipo_recaudo.Value = pRecaudos.tipo_recaudo;
                        ptipo_recaudo.Direction = ParameterDirection.Input;
                        ptipo_recaudo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_recaudo);

                        DbParameter pcod_empresa = cmdTransaccionFactory.CreateParameter();
                        pcod_empresa.ParameterName = "pcod_empresa";
                        pcod_empresa.Value = pRecaudos.cod_empresa;
                        pcod_empresa.Direction = ParameterDirection.Input;
                        pcod_empresa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_empresa);

                        DbParameter ptipo_lista = cmdTransaccionFactory.CreateParameter();
                        ptipo_lista.ParameterName = "ptipo_lista";
                        ptipo_lista.Value = pRecaudos.tipo_lista;
                        ptipo_lista.Direction = ParameterDirection.Input;
                        ptipo_lista.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_lista);

                        DbParameter pperiodo_corte = cmdTransaccionFactory.CreateParameter();
                        pperiodo_corte.ParameterName = "pperiodo_corte";
                        pperiodo_corte.Value = pRecaudos.periodo_corte;
                        pperiodo_corte.Direction = ParameterDirection.Input;
                        pperiodo_corte.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pperiodo_corte);

                        DbParameter pfecha_generacion = cmdTransaccionFactory.CreateParameter();
                        pfecha_generacion.ParameterName = "pfecha_generacion";
                        pfecha_generacion.Value = pRecaudos.fecha_generacion;
                        pfecha_generacion.Direction = ParameterDirection.Input;
                        pfecha_generacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_generacion);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "pestado";
                        pestado.Value = pRecaudos.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pusuariocreacion = cmdTransaccionFactory.CreateParameter();
                        pusuariocreacion.ParameterName = "pusuariocreacion";
                        pusuariocreacion.Value = vUsuario.nombre;
                        pusuariocreacion.Direction = ParameterDirection.Input;
                        pusuariocreacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pusuariocreacion);

                        DbParameter pfecha_inicio = cmdTransaccionFactory.CreateParameter();
                        pfecha_inicio.ParameterName = "pfecha_inicio";
                        if (pRecaudos.fecha_inicial == null)
                            pfecha_inicio.Value = DBNull.Value;
                        else
                            pfecha_inicio.Value = pRecaudos.fecha_inicial;
                        pfecha_inicio.Direction = ParameterDirection.Input;
                        pfecha_inicio.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_inicio);

                        DbParameter pnumero_vacaciones = cmdTransaccionFactory.CreateParameter();
                        pnumero_vacaciones.ParameterName = "pnumero_vacaciones";
                        pnumero_vacaciones.Value = pRecaudos.vacaciones;
                        pnumero_vacaciones.Direction = ParameterDirection.Output;
                        pnumero_vacaciones.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_vacaciones);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REC_NOVEDAD_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pRecaudos.numero_novedad = Convert.ToInt64(pnumero_novedad.Value);
                        return pRecaudos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpresaNovedadData", "CrearRecaudosGeneracion", ex);
                        return null;
                    }
                }
            }
        }

        public EmpresaNovedad ModificarRecaudosGeneracion(EmpresaNovedad pRecaudos, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_numero_novedad = cmdTransaccionFactory.CreateParameter();
                        p_numero_novedad.ParameterName = "pnumero_novedad";
                        p_numero_novedad.Value = pRecaudos.numero_novedad;
                        p_numero_novedad.Direction = ParameterDirection.Input;
                        p_numero_novedad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_numero_novedad);

                        DbParameter ptipo_recaudo = cmdTransaccionFactory.CreateParameter();
                        ptipo_recaudo.ParameterName = "ptipo_recaudo";
                        ptipo_recaudo.Value = pRecaudos.tipo_recaudo;
                        ptipo_recaudo.Direction = ParameterDirection.Input;
                        ptipo_recaudo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_recaudo);

                        DbParameter pcod_empresa = cmdTransaccionFactory.CreateParameter();
                        pcod_empresa.ParameterName = "pcod_empresa";
                        pcod_empresa.Value = pRecaudos.cod_empresa;
                        pcod_empresa.Direction = ParameterDirection.Input;
                        pcod_empresa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_empresa);

                        DbParameter ptipo_lista = cmdTransaccionFactory.CreateParameter();
                        ptipo_lista.ParameterName = "ptipo_lista";
                        if (pRecaudos.tipo_lista == null)
                            ptipo_lista.Value = DBNull.Value;
                        else
                            ptipo_lista.Value = pRecaudos.tipo_lista;
                        ptipo_lista.Direction = ParameterDirection.Input;
                        ptipo_lista.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_lista);

                        DbParameter pperiodo_corte = cmdTransaccionFactory.CreateParameter();
                        pperiodo_corte.ParameterName = "pperiodo_corte";
                        if (pRecaudos.periodo_corte == null)
                            pperiodo_corte.Value = DBNull.Value;
                        else
                            pperiodo_corte.Value = pRecaudos.periodo_corte;
                        pperiodo_corte.Direction = ParameterDirection.Input;
                        pperiodo_corte.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pperiodo_corte);

                        DbParameter pfecha_generacion = cmdTransaccionFactory.CreateParameter();
                        pfecha_generacion.ParameterName = "pfecha_generacion";
                        if (pRecaudos.fecha_generacion == null)
                            pfecha_generacion.Value = DBNull.Value;
                        else
                            pfecha_generacion.Value = pRecaudos.fecha_generacion;
                        pfecha_generacion.Direction = ParameterDirection.Input;
                        pfecha_generacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_generacion);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "pestado";
                        if (pRecaudos.estado == null)
                            pestado.Value = "0";
                        else
                            pestado.Value = pRecaudos.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pusuultmod = cmdTransaccionFactory.CreateParameter();
                        pusuultmod.ParameterName = "pusuariocreacion";
                        pusuultmod.Value = vUsuario.nombre;
                        pusuultmod.Direction = ParameterDirection.Input;
                        pusuultmod.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pusuultmod);

                        DbParameter pfecha_inicio = cmdTransaccionFactory.CreateParameter();
                        pfecha_inicio.ParameterName = "pfecha_inicio";
                        if (pRecaudos.fecha_inicial == null)
                            pfecha_inicio.Value = DBNull.Value;
                        else
                            pfecha_inicio.Value = pRecaudos.fecha_inicial;
                        pfecha_inicio.Direction = ParameterDirection.Input;
                        pfecha_inicio.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_inicio);

                        DbParameter pnumero_vacaciones = cmdTransaccionFactory.CreateParameter();
                        pnumero_vacaciones.ParameterName = "pnumero_vacaciones";
                        pnumero_vacaciones.Value = pRecaudos.vacaciones;
                        pnumero_vacaciones.Direction = ParameterDirection.Input;
                        pnumero_vacaciones.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumero_vacaciones);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REC_NOVEDAD_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pRecaudos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpresaNovedadData", "CrearRecaudosGeneracion", ex);
                        return null;
                    }
                }
            }
        }

        public EmpresaNovedad ConsultarRecaudo(string pNumeroRecaudo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            EmpresaNovedad eRecaudosMasivos = new EmpresaNovedad();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM EMPRESA_NOVEDAD WHERE NUMERO_NOVEDAD = " + pNumeroRecaudo;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["NUMERO_NOVEDAD"] != DBNull.Value) eRecaudosMasivos.numero_novedad = Convert.ToInt64(resultado["NUMERO_NOVEDAD"]);
                            if (resultado["TIPO_RECAUDO"] != DBNull.Value) eRecaudosMasivos.tipo_recaudo = Convert.ToInt64(resultado["TIPO_RECAUDO"]);
                            if (resultado["TIPO_LISTA"] != DBNull.Value) eRecaudosMasivos.tipo_lista = Convert.ToInt32(resultado["TIPO_LISTA"]);
                            if (resultado["COD_EMPRESA"] != DBNull.Value) eRecaudosMasivos.cod_empresa = Convert.ToInt64(resultado["COD_EMPRESA"]);
                            if (resultado["PERIODO_CORTE"] != DBNull.Value) eRecaudosMasivos.periodo_corte = Convert.ToDateTime(resultado["PERIODO_CORTE"]);
                            if (resultado["FECHA_GENERACION"] != DBNull.Value) eRecaudosMasivos.fecha_generacion = Convert.ToDateTime(resultado["FECHA_GENERACION"]);
                            if (resultado["ESTADO"] != DBNull.Value) eRecaudosMasivos.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["FECHA_INICIO"] != DBNull.Value) eRecaudosMasivos.fecha_inicial = Convert.ToDateTime(resultado["FECHA_INICIO"]);
                        }

                        return eRecaudosMasivos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpresaNovedadData", "ConsultarRecaudo", ex);
                        return null;
                    }
                }
            }
        }

        public List<EmpresaNovedad> ListarRecaudo(EmpresaNovedad pRecaudo, string pOrden, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<EmpresaNovedad> lstRecaudosMasivos = new List<EmpresaNovedad>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql;
                        sql = @"SELECT A.*, B.NOM_EMPRESA, CASE A.TIPO_RECAUDO WHEN 0 THEN 'Bancarios' WHEN 1 THEN 'Nomina' END AS NOM_TIPO_RECAUDO, CASE A.ESTADO WHEN 1 THEN 'Pendiente' WHEN 2 THEN 'Aplicado' END AS NOM_ESTADO, C.DESCRIPCION AS NOM_TIPO_LISTA 
                                FROM EMPRESA_NOVEDAD A LEFT JOIN TIPO_LISTA_RECAUDO C ON A.TIPO_LISTA = C.IDTIPO_LISTA LEFT JOIN EMPRESA_RECAUDO B ON A.COD_EMPRESA = B.COD_EMPRESA " + ObtenerFiltro(pRecaudo, "A.");
                        if (pRecaudo.fecha_generacion != DateTime.MinValue && pRecaudo.fecha_generacion != null)
                            sql += (sql.ToUpper().Contains("WHERE") ? " AND " : " WHERE ") + " A.FECHA_GENERACION = To_Date('" + Convert.ToDateTime(pRecaudo.fecha_generacion).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') ";
                        if (pOrden.Trim() != "")
                        {
                            sql += " ORDER BY " + pOrden;
                        }
                        else
                        {
                            sql += " ORDER BY A.NUMERO_NOVEDAD";
                        }
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            EmpresaNovedad entidad = new EmpresaNovedad();

                            if (resultado["NUMERO_NOVEDAD"] != DBNull.Value) entidad.numero_novedad = Convert.ToInt64(resultado["NUMERO_NOVEDAD"]);
                            if (resultado["TIPO_RECAUDO"] != DBNull.Value) entidad.tipo_recaudo = Convert.ToInt64(resultado["TIPO_RECAUDO"]);
                            if (resultado["NOM_TIPO_RECAUDO"] != DBNull.Value) entidad.nom_tipo_recaudo = Convert.ToString(resultado["NOM_TIPO_RECAUDO"]);
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt64(resultado["COD_EMPRESA"]);
                            if (resultado["NOM_EMPRESA"] != DBNull.Value) entidad.nom_empresa = Convert.ToString(resultado["NOM_EMPRESA"]);
                            if (resultado["PERIODO_CORTE"] != DBNull.Value) entidad.periodo_corte = Convert.ToDateTime(resultado["PERIODO_CORTE"]);
                            if (resultado["FECHA_GENERACION"] != DBNull.Value) entidad.fecha_generacion = Convert.ToDateTime(resultado["FECHA_GENERACION"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["NOM_ESTADO"] != DBNull.Value) entidad.nom_estado = Convert.ToString(resultado["NOM_ESTADO"]);
                            if (resultado["USUARIO_CREACION"] != DBNull.Value) entidad.usuario = Convert.ToString(resultado["USUARIO_CREACION"]);
                            if (resultado["PERIODO_CORTE"] != DBNull.Value) entidad.concepto = Convert.ToDateTime(resultado["PERIODO_CORTE"]).ToString(conf.ObtenerFormatoFecha());
                            if (resultado["NOM_TIPO_LISTA"] != DBNull.Value) entidad.concepto += " " + Convert.ToString(resultado["NOM_TIPO_LISTA"]);
                            if (resultado["NOM_TIPO_LISTA"] != DBNull.Value) entidad.nom_tipo_lista = Convert.ToString(resultado["NOM_TIPO_LISTA"]);

                            lstRecaudosMasivos.Add(entidad);
                        }

                        return lstRecaudosMasivos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpresaNovedadData", "ListarRecaudo", ex);
                        return null;
                    }
                }
            }
        }

        public EmpresaNovedad CrearDetRecaudosGeneracion(EmpresaNovedad pRecaudos, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter piddetalle = cmdTransaccionFactory.CreateParameter();
                        piddetalle.ParameterName = "piddetalle";
                        piddetalle.Value = pRecaudos.iddetalle;
                        piddetalle.Direction = ParameterDirection.Output;
                        piddetalle.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(piddetalle);

                        DbParameter pnumero_novedad = cmdTransaccionFactory.CreateParameter();
                        pnumero_novedad.ParameterName = "pnumero_novedad";
                        pnumero_novedad.Value = pRecaudos.numero_novedad;
                        pnumero_novedad.Direction = ParameterDirection.Input;
                        pnumero_novedad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_novedad);

                        DbParameter pcod_cliente = cmdTransaccionFactory.CreateParameter();
                        pcod_cliente.ParameterName = "pcod_cliente";
                        pcod_cliente.Value = pRecaudos.cod_persona;
                        pcod_cliente.Direction = ParameterDirection.Input;
                        pcod_cliente.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_cliente);

                        DbParameter pidentificacion = cmdTransaccionFactory.CreateParameter();
                        pidentificacion.ParameterName = "pidentificacion";
                        pidentificacion.Value = pRecaudos.identificacion;
                        pidentificacion.Direction = ParameterDirection.Input;
                        pidentificacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidentificacion);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "pnombre";
                        pnombre.Value = pRecaudos.nombre;
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        DbParameter ptipo_producto = cmdTransaccionFactory.CreateParameter();
                        ptipo_producto.ParameterName = "ptipo_producto";
                        ptipo_producto.Value = pRecaudos.nom_tipo_producto;
                        ptipo_producto.Direction = ParameterDirection.Input;
                        ptipo_producto.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipo_producto);

                        DbParameter pnumero_producto = cmdTransaccionFactory.CreateParameter();
                        pnumero_producto.ParameterName = "pnumero_producto";
                        pnumero_producto.Value = pRecaudos.numero_producto;
                        pnumero_producto.Direction = ParameterDirection.Input;
                        pnumero_producto.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumero_producto);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "pvalor";
                        pvalor.Value = pRecaudos.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter pnum_cuotas = cmdTransaccionFactory.CreateParameter();
                        pnum_cuotas.ParameterName = "pnum_cuotas";
                        pnum_cuotas.Value = 0;
                        pnum_cuotas.Direction = ParameterDirection.Input;
                        pnum_cuotas.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnum_cuotas);

                        DbParameter pobservaciones = cmdTransaccionFactory.CreateParameter();
                        pobservaciones.ParameterName = "pobservaciones";
                        if (pRecaudos.observaciones == null)
                            pobservaciones.Value = DBNull.Value;
                        else
                            pobservaciones.Value = pRecaudos.observaciones;
                        pobservaciones.Direction = ParameterDirection.Input;
                        pobservaciones.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pobservaciones);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "pestado";
                        pestado.Value = pRecaudos.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pcod_linea = cmdTransaccionFactory.CreateParameter();
                        pcod_linea.ParameterName = "pcod_linea";
                        if (pRecaudos.linea == null)
                            pcod_linea.Value = DBNull.Value;
                        else
                            pcod_linea.Value = pRecaudos.linea;
                        pcod_linea.Direction = ParameterDirection.Input;
                        pcod_linea.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea);

                        DbParameter pusuariocreacion = cmdTransaccionFactory.CreateParameter();
                        pusuariocreacion.ParameterName = "pusuariocreacion";
                        pusuariocreacion.Value = vUsuario.nombre;
                        pusuariocreacion.Direction = ParameterDirection.Input;
                        pusuariocreacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pusuariocreacion);

                        DbParameter p_fecha_proximo_pago = cmdTransaccionFactory.CreateParameter();
                        p_fecha_proximo_pago.ParameterName = "p_fecha_proximo_pago";
                        if (pRecaudos.fecha_proximo_pago == null)
                            p_fecha_proximo_pago.Value = DBNull.Value;
                        else
                            p_fecha_proximo_pago.Value = pRecaudos.fecha_proximo_pago;
                        p_fecha_proximo_pago.Direction = ParameterDirection.Input;
                        p_fecha_proximo_pago.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(p_fecha_proximo_pago);

                        DbParameter p_saldo_producto = cmdTransaccionFactory.CreateParameter();
                        p_saldo_producto.ParameterName = "p_saldo_producto";
                        p_saldo_producto.Value = pRecaudos.saldo;
                        p_saldo_producto.Direction = ParameterDirection.Input;
                        p_saldo_producto.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_saldo_producto);

                        DbParameter p_valor_capital = cmdTransaccionFactory.CreateParameter();
                        p_valor_capital.ParameterName = "p_valor_capital";
                        p_valor_capital.Value = pRecaudos.capital;
                        p_valor_capital.Direction = ParameterDirection.Input;
                        p_valor_capital.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_valor_capital);

                        DbParameter p_interes_cte = cmdTransaccionFactory.CreateParameter();
                        p_interes_cte.ParameterName = "p_interes_cte";
                        p_interes_cte.Value = pRecaudos.intcte;
                        p_interes_cte.Direction = ParameterDirection.Input;
                        p_interes_cte.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_interes_cte);

                        DbParameter p_interes_mora = cmdTransaccionFactory.CreateParameter();
                        p_interes_mora.ParameterName = "p_interes_mora";
                        p_interes_mora.Value = pRecaudos.intmora;
                        p_interes_mora.Direction = ParameterDirection.Input;
                        p_interes_mora.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_interes_mora);

                        DbParameter p_valor_seguro = cmdTransaccionFactory.CreateParameter();
                        p_valor_seguro.ParameterName = "p_valor_seguro";
                        p_valor_seguro.Value = pRecaudos.seguro;
                        p_valor_seguro.Direction = ParameterDirection.Input;
                        p_valor_seguro.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_valor_seguro);

                        DbParameter p_interes_otros = cmdTransaccionFactory.CreateParameter();
                        p_interes_otros.ParameterName = "p_interes_otros";
                        p_interes_otros.Value = pRecaudos.otros;
                        p_interes_otros.Direction = ParameterDirection.Input;
                        p_interes_otros.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_interes_otros);

                        DbParameter p_total_prestamos = cmdTransaccionFactory.CreateParameter();
                        p_total_prestamos.ParameterName = "p_total_prestamos";
                        p_total_prestamos.Value = pRecaudos.total_prestamos;
                        p_total_prestamos.Direction = ParameterDirection.Input;
                        p_total_prestamos.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_total_prestamos);

                        DbParameter p_total_fijos = cmdTransaccionFactory.CreateParameter();
                        p_total_fijos.ParameterName = "p_total_fijos";
                        p_total_fijos.Value = pRecaudos.total_fijos;
                        p_total_fijos.Direction = ParameterDirection.Input;
                        p_total_fijos.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_total_fijos);


                        DbParameter p_vacaciones = cmdTransaccionFactory.CreateParameter();
                        p_vacaciones.ParameterName = "p_vacaciones";
                        p_vacaciones.Value = pRecaudos.vacaciones;
                        p_vacaciones.Direction = ParameterDirection.Input;
                        p_vacaciones.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_vacaciones);

                        DbParameter p_fecha_inicio = cmdTransaccionFactory.CreateParameter();
                        p_fecha_inicio.ParameterName = "p_fecha_inicio";
                        if (pRecaudos.fecha_inicio_producto != null && pRecaudos.fecha_inicio_producto != DateTime.MinValue)
                            p_fecha_inicio.Value = pRecaudos.fecha_inicio_producto;
                        else
                            p_fecha_inicio.Value = DBNull.Value;
                        p_fecha_inicio.Direction = ParameterDirection.Input;
                        p_fecha_inicio.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(p_fecha_inicio);

                        DbParameter p_fecha_vencimiento = cmdTransaccionFactory.CreateParameter();
                        p_fecha_vencimiento.ParameterName = "p_fecha_vencimiento";
                        if (pRecaudos.fecha_vencimiento_producto != null && pRecaudos.fecha_vencimiento_producto != DateTime.MinValue)
                            p_fecha_vencimiento.Value = pRecaudos.fecha_vencimiento_producto;
                        else
                            p_fecha_vencimiento.Value = DBNull.Value;
                        p_fecha_vencimiento.Direction = ParameterDirection.Input;
                        p_fecha_vencimiento.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(p_fecha_vencimiento);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REC_DETNOVEDAD_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pRecaudos.iddetalle = Convert.ToInt32(piddetalle.Value);
                        return pRecaudos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpresaNovedadData", "CrearDetRecaudosGeneracion", ex);
                        return null;
                    }
                }
            }
        }


        public EmpresaNovedad ModificarDetRecaudosGeneracion(EmpresaNovedad pRecaudos, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter piddetalle = cmdTransaccionFactory.CreateParameter();
                        piddetalle.ParameterName = "piddetalle";
                        piddetalle.Value = pRecaudos.iddetalle;
                        piddetalle.Direction = ParameterDirection.Input;
                        piddetalle.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(piddetalle);

                        DbParameter pnumero_novedad = cmdTransaccionFactory.CreateParameter();
                        pnumero_novedad.ParameterName = "pnumero_novedad";
                        pnumero_novedad.Value = pRecaudos.numero_novedad;
                        pnumero_novedad.Direction = ParameterDirection.Input;
                        pnumero_novedad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_novedad);

                        DbParameter pcod_cliente = cmdTransaccionFactory.CreateParameter();
                        pcod_cliente.ParameterName = "pcod_cliente";
                        if (pRecaudos.cod_persona != 0) pcod_cliente.Value = pRecaudos.cod_persona; else pcod_cliente.Value = DBNull.Value;
                        pcod_cliente.Direction = ParameterDirection.Input;
                        pcod_cliente.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_cliente);

                        DbParameter pidentificacion = cmdTransaccionFactory.CreateParameter();
                        pidentificacion.ParameterName = "pidentificacion";
                        pidentificacion.Value = pRecaudos.identificacion;
                        pidentificacion.Direction = ParameterDirection.Input;
                        pidentificacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidentificacion);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "pnombre";
                        pnombre.Value = pRecaudos.nombre;
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        DbParameter ptipo_producto = cmdTransaccionFactory.CreateParameter();
                        ptipo_producto.ParameterName = "ptipo_producto";
                        ptipo_producto.Value = pRecaudos.nom_tipo_producto;
                        ptipo_producto.Direction = ParameterDirection.Input;
                        ptipo_producto.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipo_producto);

                        DbParameter pnumero_producto = cmdTransaccionFactory.CreateParameter();
                        pnumero_producto.ParameterName = "pnumero_producto";
                        pnumero_producto.Value = pRecaudos.numero_producto;
                        pnumero_producto.Direction = ParameterDirection.Input;
                        pnumero_producto.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumero_producto);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "pvalor";
                        pvalor.Value = pRecaudos.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter pnum_cuotas = cmdTransaccionFactory.CreateParameter();
                        pnum_cuotas.ParameterName = "pnum_cuotas";
                        pnum_cuotas.Value = DBNull.Value;
                        pnum_cuotas.Direction = ParameterDirection.Input;
                        pnum_cuotas.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnum_cuotas);

                        DbParameter pobservaciones = cmdTransaccionFactory.CreateParameter();
                        pobservaciones.ParameterName = "pobservaciones";
                        if (pRecaudos.observaciones == null)
                            pobservaciones.Value = DBNull.Value;
                        else
                            pobservaciones.Value = pRecaudos.observaciones;
                        pobservaciones.Direction = ParameterDirection.Input;
                        pobservaciones.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pobservaciones);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "pestado";
                        pestado.Value = pRecaudos.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pcod_linea = cmdTransaccionFactory.CreateParameter();
                        pcod_linea.ParameterName = "pcod_linea";
                        if (pRecaudos.linea == null)
                            pcod_linea.Value = DBNull.Value;
                        else
                            pcod_linea.Value = pRecaudos.linea;
                        pcod_linea.Direction = ParameterDirection.Input;
                        pcod_linea.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea);

                        DbParameter pusuultmod = cmdTransaccionFactory.CreateParameter();
                        pusuultmod.ParameterName = "pusuultmod";
                        pusuultmod.Value = vUsuario.nombre;
                        pusuultmod.Direction = ParameterDirection.Input;
                        pusuultmod.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pusuultmod);

                        DbParameter pcambioestado = cmdTransaccionFactory.CreateParameter();
                        pcambioestado.ParameterName = "pcambioestado";
                        pcambioestado.Value = 1;
                        pcambioestado.Direction = ParameterDirection.Input;
                        pcambioestado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcambioestado);

                        DbParameter p_valor_capital = cmdTransaccionFactory.CreateParameter();
                        p_valor_capital.ParameterName = "p_valor_capital";
                        p_valor_capital.Value = pRecaudos.capital;
                        p_valor_capital.Direction = ParameterDirection.Input;
                        p_valor_capital.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_valor_capital);

                        DbParameter p_interes_cte = cmdTransaccionFactory.CreateParameter();
                        p_interes_cte.ParameterName = "p_interes_cte";
                        p_interes_cte.Value = pRecaudos.intcte;
                        p_interes_cte.Direction = ParameterDirection.Input;
                        p_interes_cte.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_interes_cte);

                        DbParameter p_interes_mora = cmdTransaccionFactory.CreateParameter();
                        p_interes_mora.ParameterName = "p_interes_mora";
                        p_interes_mora.Value = pRecaudos.intmora;
                        p_interes_mora.Direction = ParameterDirection.Input;
                        p_interes_mora.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_interes_mora);

                        DbParameter p_valor_seguro = cmdTransaccionFactory.CreateParameter();
                        p_valor_seguro.ParameterName = "p_valor_seguro";
                        p_valor_seguro.Value = pRecaudos.seguro;
                        p_valor_seguro.Direction = ParameterDirection.Input;
                        p_valor_seguro.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_valor_seguro);

                        DbParameter p_interes_otros = cmdTransaccionFactory.CreateParameter();
                        p_interes_otros.ParameterName = "p_interes_otros";
                        p_interes_otros.Value = pRecaudos.otros;
                        p_interes_otros.Direction = ParameterDirection.Input;
                        p_interes_otros.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_interes_otros);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REC_DETNOVEDAD_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pRecaudos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpresaNovedadData", "ModificarDetRecaudosGeneracion", ex);
                        return null;
                    }
                }
            }
        }



        public void EliminarDetRecaudosGeneracion(Int64 pId, Usuario vUsuario, int opcion)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter piddetalle = cmdTransaccionFactory.CreateParameter();
                        if (opcion == 1)
                        {
                            piddetalle.ParameterName = "p_numero_novedad";//p_numero_recaudo
                        }
                        else if (opcion == 2)
                        {
                            piddetalle.ParameterName = "p_iddetalle";
                        }
                        else
                        {
                            piddetalle.ParameterName = "p_iddetalle";
                        }
                        piddetalle.Value = pId;
                        piddetalle.Direction = ParameterDirection.Input;
                        piddetalle.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(piddetalle);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        if (opcion == 1) //ELIMINA EL ENCABEZADO Y EL DETALLE SI EXISTE
                            cmdTransaccionFactory.CommandText = "USP_XPINN_REC_NOVEDAD_ELIMI";
                        else if (opcion == 2)// ELIMINA SOLO EL DETALLE DE LA TEMPORAL
                            cmdTransaccionFactory.CommandText = "USP_XPINN_REC_TEMP_RECAU_ELIMI";
                        else // ELIMINA EL DETALLE DE LA TABLA DETRECAUDO
                            cmdTransaccionFactory.CommandText = "USP_XPINN_REC_DETNOVGEN_ELIMI"; //USP_XPINN_REC_RECAMASIVO_ELIMI  PENDIENTE POR VERIFICAR
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpresaNovedadData", "EliminarRecaudosGeneracion", ex);
                    }
                }
            }
        }



        public EmpresaNovedad CrearDetRecaudosGeneracionNEW(EmpresaNovedad pEmpresaNovedad, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter piddetalle = cmdTransaccionFactory.CreateParameter();
                        piddetalle.ParameterName = "p_iddetalle";
                        piddetalle.Value = pEmpresaNovedad.iddetalle;
                        piddetalle.Direction = ParameterDirection.Output;
                        piddetalle.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(piddetalle);

                        DbParameter pnumero_novedad = cmdTransaccionFactory.CreateParameter();
                        pnumero_novedad.ParameterName = "p_numero_novedad";
                        pnumero_novedad.Value = pEmpresaNovedad.numero_novedad;
                        pnumero_novedad.Direction = ParameterDirection.Input;
                        pnumero_novedad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_novedad);

                        DbParameter pcod_cliente = cmdTransaccionFactory.CreateParameter();
                        pcod_cliente.ParameterName = "p_cod_cliente";
                        if (pEmpresaNovedad.cod_persona != 0) pcod_cliente.Value = pEmpresaNovedad.cod_persona; else pcod_cliente.Value = DBNull.Value;
                        pcod_cliente.Direction = ParameterDirection.Input;
                        pcod_cliente.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_cliente);

                        DbParameter ptipo_producto = cmdTransaccionFactory.CreateParameter();
                        ptipo_producto.ParameterName = "p_tipo_producto";
                        ptipo_producto.Value = pEmpresaNovedad.nom_tipo_producto;
                        ptipo_producto.Direction = ParameterDirection.Input;
                        ptipo_producto.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipo_producto);

                        DbParameter pnumero_producto = cmdTransaccionFactory.CreateParameter();
                        pnumero_producto.ParameterName = "p_numero_producto";
                        if (pEmpresaNovedad.numero_producto == null)
                            pnumero_producto.Value = DBNull.Value;
                        else
                            pnumero_producto.Value = pEmpresaNovedad.numero_producto;
                        pnumero_producto.Direction = ParameterDirection.Input;
                        pnumero_producto.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumero_producto);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        pvalor.Value = pEmpresaNovedad.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter ptipo_novedad = cmdTransaccionFactory.CreateParameter();
                        ptipo_novedad.ParameterName = "p_tipo_novedad";
                        if (pEmpresaNovedad.tipo_novedad == null)
                            ptipo_novedad.Value = DBNull.Value;
                        else
                            ptipo_novedad.Value = pEmpresaNovedad.tipo_novedad;
                        ptipo_novedad.Direction = ParameterDirection.Input;
                        ptipo_novedad.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipo_novedad);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pEmpresaNovedad.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pobservaciones = cmdTransaccionFactory.CreateParameter();
                        pobservaciones.ParameterName = "p_observaciones";
                        if (pEmpresaNovedad.observaciones == null)
                            pobservaciones.Value = DBNull.Value;
                        else
                            pobservaciones.Value = pEmpresaNovedad.observaciones;
                        pobservaciones.Direction = ParameterDirection.Input;
                        pobservaciones.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pobservaciones);

                        DbParameter pcod_linea_producto = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_producto.ParameterName = "p_cod_linea_producto";
                        if (pEmpresaNovedad.linea == null)
                            pcod_linea_producto.Value = DBNull.Value;
                        else
                            pcod_linea_producto.Value = pEmpresaNovedad.linea;
                        pcod_linea_producto.Direction = ParameterDirection.Input;
                        pcod_linea_producto.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_producto);

                        DbParameter pfecha_novedad = cmdTransaccionFactory.CreateParameter();
                        pfecha_novedad.ParameterName = "p_fecha_novedad";
                        if (pEmpresaNovedad.fecha_inicial == null)
                            pfecha_novedad.Value = DBNull.Value;
                        else
                            pfecha_novedad.Value = pEmpresaNovedad.fecha_inicial;
                        pfecha_novedad.Direction = ParameterDirection.Input;
                        pfecha_novedad.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_novedad);

                        DbParameter pfecha_fin_novedad = cmdTransaccionFactory.CreateParameter();
                        pfecha_fin_novedad.ParameterName = "p_fecha_fin_novedad";
                        if (pEmpresaNovedad.fecha_final == null)
                            pfecha_fin_novedad.Value = DBNull.Value;
                        else
                            pfecha_fin_novedad.Value = pEmpresaNovedad.fecha_final;
                        pfecha_fin_novedad.Direction = ParameterDirection.Input;
                        pfecha_fin_novedad.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_fin_novedad);

                        DbParameter pnumero_cuotas = cmdTransaccionFactory.CreateParameter();
                        pnumero_cuotas.ParameterName = "p_numero_cuotas";
                        pnumero_cuotas.Value = pEmpresaNovedad.numero_cuotas;
                        pnumero_cuotas.Direction = ParameterDirection.Input;
                        pnumero_cuotas.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_cuotas);

                        DbParameter pvalor_total = cmdTransaccionFactory.CreateParameter();
                        pvalor_total.ParameterName = "p_valor_total";
                        pvalor_total.Value = pEmpresaNovedad.valor_total;
                        pvalor_total.Direction = ParameterDirection.Input;
                        pvalor_total.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_total);

                        DbParameter pfechacreacion = cmdTransaccionFactory.CreateParameter();
                        pfechacreacion.ParameterName = "p_fechacreacion";
                        pfechacreacion.Value = DateTime.Now;
                        pfechacreacion.Direction = ParameterDirection.Input;
                        pfechacreacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechacreacion);

                        DbParameter pusuariocreacion = cmdTransaccionFactory.CreateParameter();
                        pusuariocreacion.ParameterName = "p_usuariocreacion";
                        pusuariocreacion.Value = vUsuario.nombre;
                        pusuariocreacion.Direction = ParameterDirection.Input;
                        pusuariocreacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pusuariocreacion);

                        DbParameter pfecultmod = cmdTransaccionFactory.CreateParameter();
                        pfecultmod.ParameterName = "p_fecultmod";
                        pfecultmod.Value = DateTime.Now;
                        pfecultmod.Direction = ParameterDirection.Input;
                        pfecultmod.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecultmod);

                        DbParameter pusuultmod = cmdTransaccionFactory.CreateParameter();
                        pusuultmod.ParameterName = "p_usuultmod";
                        pusuultmod.Value = vUsuario.nombre;
                        pusuultmod.Direction = ParameterDirection.Input;
                        pusuultmod.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pusuultmod);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REC_DETNOVNEW_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pEmpresaNovedad.iddetalle = Convert.ToInt64(piddetalle.Value);
                        return pEmpresaNovedad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpresaNovedadData", "CrearDetRecaudosGeneracionNEW", ex);
                        return null;
                    }
                }
            }
        }


        public EmpresaNovedad ModificarDetRecaudosGeneracionNEW(EmpresaNovedad pEmpresaNovedad, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter piddetalle = cmdTransaccionFactory.CreateParameter();
                        piddetalle.ParameterName = "p_iddetalle";
                        piddetalle.Value = pEmpresaNovedad.iddetalle;
                        piddetalle.Direction = ParameterDirection.Input;
                        piddetalle.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(piddetalle);

                        DbParameter pnumero_novedad = cmdTransaccionFactory.CreateParameter();
                        pnumero_novedad.ParameterName = "p_numero_novedad";
                        pnumero_novedad.Value = pEmpresaNovedad.numero_novedad;
                        pnumero_novedad.Direction = ParameterDirection.Input;
                        pnumero_novedad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_novedad);

                        DbParameter pcod_cliente = cmdTransaccionFactory.CreateParameter();
                        pcod_cliente.ParameterName = "p_cod_cliente";
                        if (pEmpresaNovedad.cod_persona != 0) pcod_cliente.Value = pEmpresaNovedad.cod_persona; else pcod_cliente.Value = DBNull.Value;
                        pcod_cliente.Direction = ParameterDirection.Input;
                        pcod_cliente.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_cliente);

                        DbParameter ptipo_producto = cmdTransaccionFactory.CreateParameter();
                        ptipo_producto.ParameterName = "p_tipo_producto";
                        ptipo_producto.Value = pEmpresaNovedad.nom_tipo_producto;
                        ptipo_producto.Direction = ParameterDirection.Input;
                        ptipo_producto.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipo_producto);

                        DbParameter pnumero_producto = cmdTransaccionFactory.CreateParameter();
                        pnumero_producto.ParameterName = "p_numero_producto";
                        if (pEmpresaNovedad.numero_producto == null)
                            pnumero_producto.Value = DBNull.Value;
                        else
                            pnumero_producto.Value = pEmpresaNovedad.numero_producto;
                        pnumero_producto.Direction = ParameterDirection.Input;
                        pnumero_producto.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumero_producto);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        pvalor.Value = pEmpresaNovedad.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter ptipo_novedad = cmdTransaccionFactory.CreateParameter();
                        ptipo_novedad.ParameterName = "p_tipo_novedad";
                        if (pEmpresaNovedad.tipo_novedad == null)
                            ptipo_novedad.Value = DBNull.Value;
                        else
                            ptipo_novedad.Value = pEmpresaNovedad.tipo_novedad;
                        ptipo_novedad.Direction = ParameterDirection.Input;
                        ptipo_novedad.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipo_novedad);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pEmpresaNovedad.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pobservaciones = cmdTransaccionFactory.CreateParameter();
                        pobservaciones.ParameterName = "p_observaciones";
                        if (pEmpresaNovedad.observaciones == null)
                            pobservaciones.Value = DBNull.Value;
                        else
                            pobservaciones.Value = pEmpresaNovedad.observaciones;
                        pobservaciones.Direction = ParameterDirection.Input;
                        pobservaciones.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pobservaciones);

                        DbParameter pcod_linea_producto = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_producto.ParameterName = "p_cod_linea_producto";
                        if (pEmpresaNovedad.linea == null)
                            pcod_linea_producto.Value = DBNull.Value;
                        else
                            pcod_linea_producto.Value = pEmpresaNovedad.linea;
                        pcod_linea_producto.Direction = ParameterDirection.Input;
                        pcod_linea_producto.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_producto);

                        DbParameter pfecha_novedad = cmdTransaccionFactory.CreateParameter();
                        pfecha_novedad.ParameterName = "p_fecha_novedad";
                        if (pEmpresaNovedad.fecha_inicial == null)
                            pfecha_novedad.Value = DBNull.Value;
                        else
                            pfecha_novedad.Value = pEmpresaNovedad.fecha_inicial;
                        pfecha_novedad.Direction = ParameterDirection.Input;
                        pfecha_novedad.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_novedad);

                        DbParameter pfecha_fin_novedad = cmdTransaccionFactory.CreateParameter();
                        pfecha_fin_novedad.ParameterName = "p_fecha_fin_novedad";
                        if (pEmpresaNovedad.fecha_final == null)
                            pfecha_fin_novedad.Value = DBNull.Value;
                        else
                            pfecha_fin_novedad.Value = pEmpresaNovedad.fecha_final;
                        pfecha_fin_novedad.Direction = ParameterDirection.Input;
                        pfecha_fin_novedad.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_fin_novedad);

                        DbParameter pnumero_cuotas = cmdTransaccionFactory.CreateParameter();
                        pnumero_cuotas.ParameterName = "p_numero_cuotas";
                        pnumero_cuotas.Value = pEmpresaNovedad.numero_cuotas;
                        pnumero_cuotas.Direction = ParameterDirection.Input;
                        pnumero_cuotas.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_cuotas);

                        DbParameter pvalor_total = cmdTransaccionFactory.CreateParameter();
                        pvalor_total.ParameterName = "p_valor_total";
                        pvalor_total.Value = pEmpresaNovedad.valor_total;
                        pvalor_total.Direction = ParameterDirection.Input;
                        pvalor_total.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_total);

                        DbParameter pfecultmod = cmdTransaccionFactory.CreateParameter();
                        pfecultmod.ParameterName = "p_fecultmod";
                        pfecultmod.Value = DateTime.Now;
                        pfecultmod.Direction = ParameterDirection.Input;
                        pfecultmod.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecultmod);

                        DbParameter pusuultmod = cmdTransaccionFactory.CreateParameter();
                        pusuultmod.ParameterName = "p_usuultmod";
                        pusuultmod.Value = vUsuario.nombre;
                        pusuultmod.Direction = ParameterDirection.Input;
                        pusuultmod.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pusuultmod);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REC_DETNOVNEW_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pEmpresaNovedad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpresaNovedadData", "ModificarDetRecaudosGeneracionNEW", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarDetRecaudosGeneracionNew(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter piddetalle = cmdTransaccionFactory.CreateParameter();
                        piddetalle.ParameterName = "p_iddetalle";
                        piddetalle.Value = pId;
                        piddetalle.Direction = ParameterDirection.Input;
                        piddetalle.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(piddetalle);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REC_DETNOVNEW_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpresaNovedadData", "EliminarDetRecaudosGeneracionNew", ex);
                    }
                }
            }
        }

        public List<EmpresaNovedad> ListarDetalleGeneracion(EmpresaNovedad pRecaudo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<EmpresaNovedad> lstEmpresaNovedad = new List<EmpresaNovedad>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT a.iddetalle, a.tipo_producto, a.numero_producto, a.cod_cliente, p.Cod_Nomina, p.identificacion, p.cod_persona, p.nombre, p.identificacion,p.NOMBRES,P.APELLIDOS, a.valor, a.cod_linea_producto,a.saldo_producto, A.FECHA_PROXIMO_PAGO,
                                     a.VALOR_CAPITAL, a.INTERES_CTE, a.INTERES_MORA, a.VALOR_SEGURO, a.INTERES_OTROS,a.TOTAL_FIJOS,a.TOTAL_PRESTAMOS,a.VACACIONES, a.FECHA_INICIO_PRODUCTO, a.FECHA_VENCIMIENTO_PRODUCTO   
                                     FROM DETEMPRESA_NOVEDAD A
                                     INNER JOIN V_PERSONA P ON A.COD_CLIENTE = P.COD_PERSONA 
                                     INNER JOIN EMPRESA_NOVEDAD EN ON EN.NUMERO_NOVEDAD = A.NUMERO_NOVEDAD
                                     INNER JOIN EMPRESA_RECAUDO ER ON ER.COD_EMPRESA = EN.COD_EMPRESA 
                                     WHERE A.NUMERO_NOVEDAD = " + pRecaudo.numero_novedad
                                     + @" and P.estado = (case ER.DESHABILITAR_DESC_INHABILES WHEN 1 then 'A' else P.estado end)
                                     ORDER BY IDDETALLE";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            EmpresaNovedad entidad = new EmpresaNovedad();

                            if (resultado["IDDETALLE"] != DBNull.Value) entidad.iddetalle = Convert.ToInt32(resultado["IDDETALLE"]);
                            if (resultado["TIPO_PRODUCTO"] != DBNull.Value) entidad.nom_tipo_producto = Convert.ToString(resultado["TIPO_PRODUCTO"]);
                            if (resultado["COD_LINEA_PRODUCTO"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["COD_LINEA_PRODUCTO"]);
                            if (resultado["NUMERO_PRODUCTO"] != DBNull.Value) entidad.numero_producto = Convert.ToString(resultado["NUMERO_PRODUCTO"]);
                            if (resultado["COD_CLIENTE"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_CLIENTE"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["APELLIDOS"] != DBNull.Value) entidad.apellidos = Convert.ToString(resultado["APELLIDOS"]);                            
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["COD_NOMINA"] != DBNull.Value) entidad.cod_nomina_empleado = Convert.ToString(resultado["COD_NOMINA"]);
                            if (resultado["SALDO_PRODUCTO"] != DBNull.Value) entidad.saldo = Convert.ToDecimal(resultado["SALDO_PRODUCTO"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            entidad.tipo_producto = CodTipoProducto(entidad.nom_tipo_producto);
                            if (resultado["VALOR_CAPITAL"] != DBNull.Value) entidad.capital = Convert.ToDecimal(resultado["VALOR_CAPITAL"]);
                            if (resultado["INTERES_CTE"] != DBNull.Value) entidad.intcte = Convert.ToDecimal(resultado["INTERES_CTE"]);
                            if (resultado["INTERES_MORA"] != DBNull.Value) entidad.intmora = Convert.ToDecimal(resultado["INTERES_MORA"]);
                            if (resultado["VALOR_SEGURO"] != DBNull.Value) entidad.seguro = Convert.ToDecimal(resultado["VALOR_SEGURO"]);
                            if (resultado["INTERES_OTROS"] != DBNull.Value) entidad.otros = Convert.ToDecimal(resultado["INTERES_OTROS"]);
                            if (resultado["TOTAL_FIJOS"] != DBNull.Value) entidad.total_fijos = Convert.ToDecimal(resultado["TOTAL_FIJOS"]);
                            if (resultado["TOTAL_PRESTAMOS"] != DBNull.Value) entidad.total_prestamos = Convert.ToDecimal(resultado["TOTAL_PRESTAMOS"]);
                            if (resultado["VACACIONES"] != DBNull.Value) entidad.vacaciones = Convert.ToInt64(resultado["VACACIONES"]);
                            if (resultado["FECHA_INICIO_PRODUCTO"] != DBNull.Value) entidad.fecha_inicio_producto = Convert.ToDateTime(resultado["FECHA_INICIO_PRODUCTO"]);
                            if (resultado["FECHA_VENCIMIENTO_PRODUCTO"] != DBNull.Value) entidad.fecha_vencimiento_producto = Convert.ToDateTime(resultado["FECHA_VENCIMIENTO_PRODUCTO"]);

                            lstEmpresaNovedad.Add(entidad);
                            /*
                            string sql2 = "Insert Into temp_recaudo (IDDETALLE, NOM_TIPO_PRODUCTO, NUMERO_PRODUCTO, COD_PERSONA, IDENTIFICACION,"
                                + "NOMBRES, APELLIDOS, VALOR, LINEA) Values(" + entidad.iddetalle + ",'" + entidad.nom_tipo_producto + "','" + entidad.numero_producto + "',"
                                + entidad.cod_persona + ",'" + entidad.identificacion + "','" + entidad.nombres + "','" + entidad.apellidos + "'," + entidad.valor + ",'" + entidad.linea + "')";

                            cmdTransaccionFactory.Connection = connection;
                            cmdTransaccionFactory.CommandType = CommandType.Text;
                            cmdTransaccionFactory.CommandText = sql2;
                            cmdTransaccionFactory.ExecuteScalar();*/
                        }

                        return lstEmpresaNovedad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpresaNovedadData", "ListarRecaudo", ex);
                        return null;
                    }
                }
            }

        }


        public List<EmpresaNovedad> ActualizarDetalleGeneracion(EmpresaNovedad pRecaudos, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<EmpresaNovedad> lstRecaudos = new List<EmpresaNovedad>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT a.iddetalle, a.tipo_producto, a.numero_producto, a.cod_cliente, p.identificacion, p.nombres ||', '|| p.apellidos as nombre, a.valor, a.cod_linea_producto, 
                            P.CODCIUDADRESIDENCIA,P.DIRECCION,P.TELEFONO,P.EMAIL, P.COD_NOMINA, A.SALDO_PRODUCTO, p.NOMBRES, p.APELLIDOS, p.PRIMER_NOMBRE, p.SEGUNDO_NOMBRE, p.PRIMER_APELLIDO, p.SEGUNDO_APELLIDO, 
                            a.VALOR_CAPITAL, a.INTERES_CTE, a.INTERES_MORA, a.VALOR_SEGURO, a.INTERES_OTROS,a.TOTAL_FIJOS,a.TOTAL_PRESTAMOS, a.FECHA_INICIO_PRODUCTO, a.FECHA_VENCIMIENTO_PRODUCTO 
                            FROM DETEMPRESA_NOVEDAD A
                            INNER JOIN V_PERSONA P ON a.cod_cliente = p.cod_persona 
                            INNER JOIN EMPRESA_NOVEDAD EN ON EN.NUMERO_NOVEDAD = A.NUMERO_NOVEDAD
                            INNER JOIN EMPRESA_RECAUDO ER ON ER.COD_EMPRESA = EN.COD_EMPRESA "
                            + ObtenerFiltro(pRecaudos, "a.")
                            + @" AND P.estado = (case ER.DESHABILITAR_DESC_INHABILES WHEN 1 then 'A' else P.estado end) 
                            ORDER BY a.IDDETALLE ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            EmpresaNovedad entidad = new EmpresaNovedad();
                            if (resultado["IDDETALLE"] != DBNull.Value) entidad.iddetalle = Convert.ToInt32(resultado["IDDETALLE"]);
                            if (resultado["TIPO_PRODUCTO"] != DBNull.Value) entidad.nom_tipo_producto = Convert.ToString(resultado["TIPO_PRODUCTO"]);
                            entidad.tipo_producto = CodTipoProducto(entidad.nom_tipo_producto);
                            //if (entidad.nom_tipo_producto == "CREDITOS-CUOTAS EXTRAS")
                            //    entidad.tipo_producto = CodTipoProducto(entidad.nom_tipo_producto);
                            //else
                            //    if (entidad.nom_tipo_producto.Trim().Contains('-'))
                            //    {
                            //        string[] pData = entidad.nom_tipo_producto.Trim().Split('-');
                            //        entidad.tipo_producto = pData[0].ToString().Trim();
                            //    }
                            //    else
                            //        entidad.tipo_producto = CodTipoProducto(entidad.nom_tipo_producto);
                            if (resultado["COD_LINEA_PRODUCTO"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["COD_LINEA_PRODUCTO"]);
                            if (resultado["NUMERO_PRODUCTO"] != DBNull.Value) entidad.numero_producto = Convert.ToString(resultado["NUMERO_PRODUCTO"]);
                            if (resultado["COD_CLIENTE"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_CLIENTE"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["APELLIDOS"] != DBNull.Value) entidad.apellidos = Convert.ToString(resultado["APELLIDOS"]);
                            if (resultado["PRIMER_NOMBRE"] != DBNull.Value) entidad.nombres1 = Convert.ToString(resultado["PRIMER_NOMBRE"]);
                            if (resultado["SEGUNDO_NOMBRE"] != DBNull.Value) entidad.nombres2 = Convert.ToString(resultado["SEGUNDO_NOMBRE"]);
                            if (resultado["PRIMER_APELLIDO"] != DBNull.Value) entidad.apellidos1 = Convert.ToString(resultado["PRIMER_APELLIDO"]);
                            if (resultado["SEGUNDO_APELLIDO"] != DBNull.Value) entidad.apellidos2 = Convert.ToString(resultado["SEGUNDO_APELLIDO"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["CODCIUDADRESIDENCIA"] != DBNull.Value) entidad.codciudad = Convert.ToInt64(resultado["CODCIUDADRESIDENCIA"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["EMAIL"] != DBNull.Value) entidad.email = Convert.ToString(resultado["EMAIL"]); 
                            if (resultado["COD_NOMINA"] != DBNull.Value) entidad.cod_nomina_empleado = Convert.ToString(resultado["COD_NOMINA"]);
                            if (resultado["SALDO_PRODUCTO"] != DBNull.Value) entidad.saldo = Convert.ToDecimal(resultado["SALDO_PRODUCTO"]);
                            if (resultado["VALOR_CAPITAL"] != DBNull.Value) entidad.capital = Convert.ToDecimal(resultado["VALOR_CAPITAL"]);
                            if (resultado["INTERES_CTE"] != DBNull.Value) entidad.intcte = Convert.ToDecimal(resultado["INTERES_CTE"]);
                            if (resultado["INTERES_MORA"] != DBNull.Value) entidad.intmora = Convert.ToDecimal(resultado["INTERES_MORA"]);
                            if (resultado["VALOR_SEGURO"] != DBNull.Value) entidad.seguro = Convert.ToDecimal(resultado["VALOR_SEGURO"]);
                            if (resultado["INTERES_OTROS"] != DBNull.Value) entidad.otros = Convert.ToDecimal(resultado["INTERES_OTROS"]);
                            if (resultado["TOTAL_FIJOS"] != DBNull.Value) entidad.total_fijos = Convert.ToDecimal(resultado["TOTAL_FIJOS"]);
                            if (resultado["TOTAL_PRESTAMOS"] != DBNull.Value) entidad.total_prestamos = Convert.ToDecimal(resultado["TOTAL_PRESTAMOS"]);
                            if (resultado["FECHA_INICIO_PRODUCTO"] != DBNull.Value) entidad.fecha_inicio_producto = Convert.ToDateTime(resultado["FECHA_INICIO_PRODUCTO"]);
                            if (resultado["FECHA_VENCIMIENTO_PRODUCTO"] != DBNull.Value) entidad.fecha_vencimiento_producto = Convert.ToDateTime(resultado["FECHA_VENCIMIENTO_PRODUCTO"]);
                            lstRecaudos.Add(entidad);
                        }
                         dbConnectionFactory.CerrarConexion(connection);
                        return lstRecaudos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpresaNovedadData", "ListarDetalleGeneracion", ex);
                        return null;
                    }
                }
            }
        }


        public List<EmpresaNovedad> ListarDetalleGeneracionNuevas(EmpresaNovedad pRecaudos, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<EmpresaNovedad> lstRecaudos = new List<EmpresaNovedad>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT a.iddetalle, a.tipo_producto, a.numero_producto, a.cod_cliente, p.cod_nomina, p.identificacion,p.NOMBRES,P.APELLIDOS, a.valor, a.cod_linea_producto,a.TIPO_NOVEDAD, "
                                     + "a.FECHA_NOVEDAD,a.FECHA_FIN_NOVEDAD,a.NUMERO_CUOTAS,a.VALOR_TOTAL,P.CODCIUDADRESIDENCIA,P.DIRECCION,P.TELEFONO,P.EMAIL "
                                     + "FROM DETEMPRESA_NOVEDAD_NUEVAS A LEFT JOIN V_PERSONA P ON a.cod_cliente = p.cod_persona " + ObtenerFiltro(pRecaudos) + " ORDER BY IDDETALLE ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            EmpresaNovedad entidad = new EmpresaNovedad();
                            if (resultado["IDDETALLE"] != DBNull.Value) entidad.iddetalle = Convert.ToInt32(resultado["IDDETALLE"]);
                            if (resultado["TIPO_PRODUCTO"] != DBNull.Value) entidad.nom_tipo_producto = Convert.ToString(resultado["TIPO_PRODUCTO"]);
                            if (entidad.nom_tipo_producto.Trim().Contains('-'))
                            {
                                string[] pData = entidad.nom_tipo_producto.Trim().Split('-');
                                entidad.tipo_producto = pData[0].ToString().Trim();
                            }
                            else
                                entidad.tipo_producto = CodTipoProducto(entidad.nom_tipo_producto);
                            if (resultado["COD_LINEA_PRODUCTO"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["COD_LINEA_PRODUCTO"]);
                            if (resultado["NUMERO_PRODUCTO"] != DBNull.Value) entidad.numero_producto = Convert.ToString(resultado["NUMERO_PRODUCTO"]);
                            if (resultado["COD_CLIENTE"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_CLIENTE"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["APELLIDOS"] != DBNull.Value) entidad.apellidos = Convert.ToString(resultado["APELLIDOS"]);
                            
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["TIPO_NOVEDAD"] != DBNull.Value) entidad.tipo_novedad = Convert.ToString(resultado["TIPO_NOVEDAD"]);
                            if (resultado["FECHA_NOVEDAD"] != DBNull.Value) entidad.fecha_inicial = Convert.ToDateTime(resultado["FECHA_NOVEDAD"]);
                            if (resultado["FECHA_FIN_NOVEDAD"] != DBNull.Value) entidad.fecha_final = Convert.ToDateTime(resultado["FECHA_FIN_NOVEDAD"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.numero_cuotas = Convert.ToInt32(resultado["NUMERO_CUOTAS"]);
                            if (resultado["VALOR_TOTAL"] != DBNull.Value) entidad.valor_total = Convert.ToDecimal(resultado["VALOR_TOTAL"]);

                            if (resultado["CODCIUDADRESIDENCIA"] != DBNull.Value) entidad.valor_total = Convert.ToDecimal(resultado["CODCIUDADRESIDENCIA"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["EMAIL"] != DBNull.Value) entidad.email = Convert.ToString(resultado["EMAIL"]);
                            if (resultado["COD_NOMINA"] != DBNull.Value) entidad.cod_nomina_empleado = Convert.ToString(resultado["COD_NOMINA"]);
                            lstRecaudos.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstRecaudos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpresaNovedadData", "ListarDetalleGeneracion", ex);
                        return null;
                    }
                }
            }
        }


        public Persona1 ConsultarDatosPersona(string pIdentificacion, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Persona1 ePersona = new Persona1();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  V_PERSONA WHERE IDENTIFICACION = '" + pIdentificacion + "' ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_PERSONA"] != DBNull.Value) ePersona.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["NOMBRES"] != DBNull.Value) ePersona.nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["APELLIDOS"] != DBNull.Value) ePersona.apellidos = Convert.ToString(resultado["APELLIDOS"]);
                        }
                        return ePersona;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpresaNovedadData", "ConsultarPersona", ex);
                        return null;
                    }
                }
            }
        }

        public List<EmpresaNovedad> ListarDetalleRecaudo(int pNumeroRecaudo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<EmpresaNovedad> lstRecaudosMasivos = new List<EmpresaNovedad>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT A.*, P.IDENTIFICACION, P.NOMBRE FROM DETEMPRESA_NOVEDAD A LEFT JOIN V_PERSONA P ON A.COD_CLIENTE = P.COD_PERSONA WHERE A.NUMERO_NOVEDAD = " + pNumeroRecaudo + " ORDER BY A.IDDETALLE";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            EmpresaNovedad entidad = new EmpresaNovedad();

                            if (resultado["IDDETALLE"] != DBNull.Value) entidad.iddetalle = Convert.ToInt64(resultado["IDDETALLE"]);
                            if (resultado["COD_CLIENTE"] != DBNull.Value) entidad.cod_cliente = Convert.ToInt64(resultado["COD_CLIENTE"]);
                            if (resultado["COD_LINEA_PRODUCTO"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["COD_LINEA_PRODUCTO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["TIPO_PRODUCTO"] != DBNull.Value) entidad.tipo_producto = Convert.ToString(resultado["TIPO_PRODUCTO"]);
                            if (resultado["NUMERO_PRODUCTO"] != DBNull.Value) entidad.numero_producto = Convert.ToString(resultado["NUMERO_PRODUCTO"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estadod = Convert.ToInt32(resultado["ESTADO"]);

                            lstRecaudosMasivos.Add(entidad);
                        }

                        return lstRecaudosMasivos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpresaNovedadData", "ListarDetalleRecaudo", ex);
                        return null;
                    }
                }
            }
        }


        public List<EmpresaNovedad> ListarEmpresaNovedad(string filtro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<EmpresaNovedad> lstEmpresaNovedad = new List<EmpresaNovedad>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select vac.IDENTIFICACION, vac.COD_PERSONA, vac.NUMERO_CUOTAS, vac.CONSECUTIVO, vac.FECHA_NOVEDAD, 
                                        CASE vac.TIPO_CALCULO WHEN 0 THEN 'Calculo en Generacion' WHEN 1 THEN 'Calculo en Carga' END as TIPO_CALCULO,
                                        tip.DESCRIPCION as desc_identificacion, per.nombre, per.COD_NOMINA,
                                        reca.NOM_EMPRESA 
                                        from vacaciones vac
                                        join V_PERSONA per on per.COD_PERSONA = vac.COD_PERSONA
                                        join TIPOIDENTIFICACION tip on tip.CODTIPOIDENTIFICACION = per.TIPO_IDENTIFICACION
                                        join empresa_recaudo reca on reca.COD_EMPRESA = vac.CODIGO_PAGADURIA "
                                        + filtro + " order by consecutivo ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            EmpresaNovedad entidad = new EmpresaNovedad();

                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.idconsecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["desc_identificacion"] != DBNull.Value) entidad.desc_tipo_identificacion = Convert.ToString(resultado["desc_identificacion"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["COD_NOMINA"] != DBNull.Value) entidad.cod_nomina_empleado = Convert.ToString(resultado["COD_NOMINA"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["nombre"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.numero_cuotas = Convert.ToInt32(resultado["NUMERO_CUOTAS"]);
                            if (resultado["NOM_EMPRESA"] != DBNull.Value) entidad.nom_empresa = Convert.ToString(resultado["NOM_EMPRESA"]);
                            if (resultado["FECHA_NOVEDAD"] != DBNull.Value) entidad.fecha_novedad = Convert.ToDateTime(resultado["FECHA_NOVEDAD"]);
                            if (resultado["TIPO_CALCULO"] != DBNull.Value) entidad.tipo_calculo = Convert.ToString(resultado["TIPO_CALCULO"]);

                            lstEmpresaNovedad.Add(entidad);
                        }

                        return lstEmpresaNovedad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpresaNovedadData", "ListarEmpresaNovedad", ex);
                        return null;
                    }
                }
            }
        }


        public List<EmpresaNovedad> ListarEstructuraXempresa(Int32 pId, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<EmpresaNovedad> lstRecaudosMasivos = new List<EmpresaNovedad>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql;
                        sql = "select c.cod_estructura_carga,c.descripcion From empresa_estructura_carga e left join estructura_carga c "
                               + "on e.cod_estructura_carga = c.cod_estructura_carga where e.cod_empresa = " + pId.ToString() + " order by 1";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            EmpresaNovedad entidad = new EmpresaNovedad();

                            if (resultado["cod_estructura_carga"] != DBNull.Value) entidad.cod_estructura_carga = Convert.ToInt32(resultado["cod_estructura_carga"]);
                            if (resultado["descripcion"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["descripcion"]);
                            lstRecaudosMasivos.Add(entidad);
                        }

                        return lstRecaudosMasivos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpresaNovedadData", "ListarEstructuraXempresa", ex);
                        return null;
                    }
                }
            }
        }

        public Boolean ConsultarConcepto(Int64 pCodEmpresa, Int64 pTipoProducto, string pCodLinea, ref Int64 prioridad, ref String concepto, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        if (pTipoProducto == 5 || pTipoProducto == 6 || pTipoProducto == 11)
                            sql = "SELECT COD_CONCEPTO, PRIORIDAD FROM EMPRESA_RECAUDO_CONCEPTO WHERE COD_EMPRESA = " + pCodEmpresa.ToString() + " AND TIPO_PRODUCTO = " + pTipoProducto.ToString();
                        else
                            sql = "SELECT COD_CONCEPTO, PRIORIDAD FROM EMPRESA_RECAUDO_CONCEPTO WHERE COD_EMPRESA = " + pCodEmpresa.ToString() + " AND TIPO_PRODUCTO = " + pTipoProducto.ToString() + " AND COD_LINEA = '" + pCodLinea + "' ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["PRIORIDAD"] != DBNull.Value) prioridad = Convert.ToInt64(resultado["PRIORIDAD"]);
                            if (resultado["COD_CONCEPTO"] != DBNull.Value) concepto = Convert.ToString(resultado["COD_CONCEPTO"]);
                            return true;
                        }

                        return false;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }

        public enum enumConceptos { APORTES = 1, CREDITOS = 2, DEPOSITOS = 3, SERVICIOS = 4, AFILIACION = 6 }

        public string CodTipoProducto(string pNomTipoProducto)
        {
            string tipo_producto = "";
            if (pNomTipoProducto.ToUpper().Contains("APORTES") || pNomTipoProducto == "1") tipo_producto = "1";
            if (pNomTipoProducto.ToUpper().Contains("CREDITOS") || pNomTipoProducto.ToUpper().Contains("CRÉDITOS") || pNomTipoProducto == "2") tipo_producto = "2";
            if (pNomTipoProducto.ToUpper().Contains("DEPOSITOS") || pNomTipoProducto.ToUpper().Contains("DEPÓSITOS") || pNomTipoProducto.ToUpper().Contains("AHORRO VISTA") || pNomTipoProducto == "3") tipo_producto = "3";
            if (pNomTipoProducto.ToUpper().Contains("SERVICIOS") || pNomTipoProducto == "4") tipo_producto = "4";
            if (pNomTipoProducto.ToUpper().Contains("AFILIACION") || pNomTipoProducto.ToUpper().Contains("AFILIACIÓN") || pNomTipoProducto == "6") tipo_producto = "6";
            if (pNomTipoProducto.ToUpper().Contains("AHORRO PROGRAMADO") || pNomTipoProducto == "9") tipo_producto = "9";
            if (pNomTipoProducto.ToUpper().Contains("CREDITOS-CUOTAS EXTRAS") || pNomTipoProducto.ToUpper().Contains("CRÉDITOS-CUOTAS EXTRAS") || pNomTipoProducto == "10") tipo_producto = "10";
            if (pNomTipoProducto.ToUpper().Contains("INT.AHORRO PERMANENTE") || pNomTipoProducto == "11") tipo_producto = "11";
            return tipo_producto;
        }

        public string NomTipoProducto(string pTipoProducto)
        {
            string nomtipo_producto = "";
            if (pTipoProducto == "1") nomtipo_producto = "APORTES";
            if (pTipoProducto == "2") nomtipo_producto = "CREDITOS";
            if (pTipoProducto == "3") nomtipo_producto = "DEPOSITOS";
            if (pTipoProducto == "4") nomtipo_producto = "SERVICIOS";
            if (pTipoProducto == "6") nomtipo_producto = "AFILIACION";
            if (pTipoProducto == "9") nomtipo_producto = "AHORRO PROGRAMADO";
            if (pTipoProducto == "10") nomtipo_producto = "CREDITOS-CUOTAS EXTRAS";
            if (pTipoProducto == "11") nomtipo_producto = "INT.AHORRO PERMANENTE";
            return nomtipo_producto;
        }


        public bool CrearDetRecaudosGeneracion(Int64 pNumeroNovedad, ref List<EmpresaNovedad> pLstRecaudos, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();

                        foreach (EmpresaNovedad pRecaudos in pLstRecaudos)
                        {
                            pRecaudos.numero_novedad = pNumeroNovedad;
                            pRecaudos.nombre = pRecaudos.nombres + ", " + pRecaudos.apellidos;

                            cmdTransaccionFactory.Parameters.Clear();

                            DbParameter piddetalle = cmdTransaccionFactory.CreateParameter();
                            piddetalle.ParameterName = "piddetalle";
                            piddetalle.Value = pRecaudos.iddetalle;
                            piddetalle.Direction = ParameterDirection.Output;
                            piddetalle.DbType = DbType.Int32;
                            cmdTransaccionFactory.Parameters.Add(piddetalle);

                            DbParameter pnumero_novedad = cmdTransaccionFactory.CreateParameter();
                            pnumero_novedad.ParameterName = "pnumero_novedad";
                            pnumero_novedad.Value = pRecaudos.numero_novedad;
                            pnumero_novedad.Direction = ParameterDirection.Input;
                            pnumero_novedad.DbType = DbType.Int32;
                            cmdTransaccionFactory.Parameters.Add(pnumero_novedad);

                            DbParameter pcod_cliente = cmdTransaccionFactory.CreateParameter();
                            pcod_cliente.ParameterName = "pcod_cliente";
                            pcod_cliente.Value = pRecaudos.cod_persona;
                            pcod_cliente.Direction = ParameterDirection.Input;
                            pcod_cliente.DbType = DbType.Int32;
                            cmdTransaccionFactory.Parameters.Add(pcod_cliente);

                            DbParameter pidentificacion = cmdTransaccionFactory.CreateParameter();
                            pidentificacion.ParameterName = "pidentificacion";
                            pidentificacion.Value = pRecaudos.identificacion;
                            pidentificacion.Direction = ParameterDirection.Input;
                            pidentificacion.DbType = DbType.String;
                            cmdTransaccionFactory.Parameters.Add(pidentificacion);

                            DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                            pnombre.ParameterName = "pnombre";
                            pnombre.Value = pRecaudos.nombre;
                            pnombre.Direction = ParameterDirection.Input;
                            pnombre.DbType = DbType.String;
                            cmdTransaccionFactory.Parameters.Add(pnombre);

                            DbParameter ptipo_producto = cmdTransaccionFactory.CreateParameter();
                            ptipo_producto.ParameterName = "ptipo_producto";
                            ptipo_producto.Value = pRecaudos.nom_tipo_producto;
                            ptipo_producto.Direction = ParameterDirection.Input;
                            ptipo_producto.DbType = DbType.String;
                            cmdTransaccionFactory.Parameters.Add(ptipo_producto);

                            DbParameter pnumero_producto = cmdTransaccionFactory.CreateParameter();
                            pnumero_producto.ParameterName = "pnumero_producto";
                            pnumero_producto.Value = pRecaudos.numero_producto;
                            pnumero_producto.Direction = ParameterDirection.Input;
                            pnumero_producto.DbType = DbType.String;
                            cmdTransaccionFactory.Parameters.Add(pnumero_producto);

                            DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                            pvalor.ParameterName = "pvalor";
                            pvalor.Value = pRecaudos.valor;
                            pvalor.Direction = ParameterDirection.Input;
                            pvalor.DbType = DbType.Decimal;
                            cmdTransaccionFactory.Parameters.Add(pvalor);

                            DbParameter pnum_cuotas = cmdTransaccionFactory.CreateParameter();
                            pnum_cuotas.ParameterName = "pnum_cuotas";
                            pnum_cuotas.Value = 0;
                            pnum_cuotas.Direction = ParameterDirection.Input;
                            pnum_cuotas.DbType = DbType.Int32;
                            cmdTransaccionFactory.Parameters.Add(pnum_cuotas);

                            DbParameter pobservaciones = cmdTransaccionFactory.CreateParameter();
                            pobservaciones.ParameterName = "pobservaciones";
                            if (pRecaudos.observaciones == null)
                                pobservaciones.Value = DBNull.Value;
                            else
                                pobservaciones.Value = pRecaudos.observaciones;
                            pobservaciones.Direction = ParameterDirection.Input;
                            pobservaciones.DbType = DbType.String;
                            cmdTransaccionFactory.Parameters.Add(pobservaciones);

                            DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                            pestado.ParameterName = "pestado";
                            pestado.Value = pRecaudos.estado;
                            pestado.Direction = ParameterDirection.Input;
                            pestado.DbType = DbType.Int32;
                            cmdTransaccionFactory.Parameters.Add(pestado);

                            DbParameter pcod_linea = cmdTransaccionFactory.CreateParameter();
                            pcod_linea.ParameterName = "pcod_linea";
                            if (pRecaudos.linea == null)
                                pcod_linea.Value = DBNull.Value;
                            else
                                pcod_linea.Value = pRecaudos.linea;
                            pcod_linea.Direction = ParameterDirection.Input;
                            pcod_linea.DbType = DbType.String;
                            cmdTransaccionFactory.Parameters.Add(pcod_linea);

                            DbParameter pusuariocreacion = cmdTransaccionFactory.CreateParameter();
                            pusuariocreacion.ParameterName = "pusuariocreacion";
                            pusuariocreacion.Value = vUsuario.nombre;
                            pusuariocreacion.Direction = ParameterDirection.Input;
                            pusuariocreacion.DbType = DbType.String;
                            cmdTransaccionFactory.Parameters.Add(pusuariocreacion);

                            DbParameter p_fecha_proximo_pago = cmdTransaccionFactory.CreateParameter();
                            p_fecha_proximo_pago.ParameterName = "p_fecha_proximo_pago";
                            if (pRecaudos.fecha_proximo_pago == null)
                                p_fecha_proximo_pago.Value = DBNull.Value;
                            else
                                p_fecha_proximo_pago.Value = pRecaudos.fecha_proximo_pago;
                            p_fecha_proximo_pago.Direction = ParameterDirection.Input;
                            p_fecha_proximo_pago.DbType = DbType.Date;
                            cmdTransaccionFactory.Parameters.Add(p_fecha_proximo_pago);

                            DbParameter p_saldo_producto = cmdTransaccionFactory.CreateParameter();
                            p_saldo_producto.ParameterName = "p_saldo_producto";
                            p_saldo_producto.Value = pRecaudos.saldo;
                            p_saldo_producto.Direction = ParameterDirection.Input;
                            p_saldo_producto.DbType = DbType.Decimal;
                            cmdTransaccionFactory.Parameters.Add(p_saldo_producto);

                            DbParameter p_valor_capital = cmdTransaccionFactory.CreateParameter();
                            p_valor_capital.ParameterName = "p_valor_capital";
                            p_valor_capital.Value = pRecaudos.capital;
                            p_valor_capital.Direction = ParameterDirection.Input;
                            p_valor_capital.DbType = DbType.Decimal;
                            cmdTransaccionFactory.Parameters.Add(p_valor_capital);

                            DbParameter p_interes_cte = cmdTransaccionFactory.CreateParameter();
                            p_interes_cte.ParameterName = "p_interes_cte";
                            p_interes_cte.Value = pRecaudos.intcte;
                            p_interes_cte.Direction = ParameterDirection.Input;
                            p_interes_cte.DbType = DbType.Decimal;
                            cmdTransaccionFactory.Parameters.Add(p_interes_cte);

                            DbParameter p_interes_mora = cmdTransaccionFactory.CreateParameter();
                            p_interes_mora.ParameterName = "p_interes_mora";
                            p_interes_mora.Value = pRecaudos.intmora;
                            p_interes_mora.Direction = ParameterDirection.Input;
                            p_interes_mora.DbType = DbType.Decimal;
                            cmdTransaccionFactory.Parameters.Add(p_interes_mora);

                            DbParameter p_valor_seguro = cmdTransaccionFactory.CreateParameter();
                            p_valor_seguro.ParameterName = "p_valor_seguro";
                            p_valor_seguro.Value = pRecaudos.seguro;
                            p_valor_seguro.Direction = ParameterDirection.Input;
                            p_valor_seguro.DbType = DbType.Decimal;
                            cmdTransaccionFactory.Parameters.Add(p_valor_seguro);

                            DbParameter p_interes_otros = cmdTransaccionFactory.CreateParameter();
                            p_interes_otros.ParameterName = "p_interes_otros";
                            p_interes_otros.Value = pRecaudos.otros;
                            p_interes_otros.Direction = ParameterDirection.Input;
                            p_interes_otros.DbType = DbType.Decimal;
                            cmdTransaccionFactory.Parameters.Add(p_interes_otros);

                            DbParameter p_total_prestamos = cmdTransaccionFactory.CreateParameter();
                            p_total_prestamos.ParameterName = "p_total_prestamos";
                            p_total_prestamos.Value = pRecaudos.total_prestamos;
                            p_total_prestamos.Direction = ParameterDirection.Input;
                            p_total_prestamos.DbType = DbType.Decimal;
                            cmdTransaccionFactory.Parameters.Add(p_total_prestamos);

                            DbParameter p_total_fijos = cmdTransaccionFactory.CreateParameter();
                            p_total_fijos.ParameterName = "p_total_fijos";
                            p_total_fijos.Value = pRecaudos.total_fijos;
                            p_total_fijos.Direction = ParameterDirection.Input;
                            p_total_fijos.DbType = DbType.Decimal;
                            cmdTransaccionFactory.Parameters.Add(p_total_fijos);


                            DbParameter p_vacaciones = cmdTransaccionFactory.CreateParameter();
                            p_vacaciones.ParameterName = "p_vacaciones";
                            p_vacaciones.Value = pRecaudos.vacaciones;
                            p_vacaciones.Direction = ParameterDirection.Input;
                            p_vacaciones.DbType = DbType.Decimal;
                            cmdTransaccionFactory.Parameters.Add(p_vacaciones);

                            DbParameter p_fecha_inicio = cmdTransaccionFactory.CreateParameter();
                            p_fecha_inicio.ParameterName = "p_fecha_inicio";
                            if (pRecaudos.fecha_inicio_producto != null && pRecaudos.fecha_inicio_producto != DateTime.MinValue)
                                p_fecha_inicio.Value = pRecaudos.fecha_inicio_producto;
                            else
                                p_fecha_inicio.Value = DBNull.Value;
                            p_fecha_inicio.Direction = ParameterDirection.Input;
                            p_fecha_inicio.DbType = DbType.DateTime;
                            cmdTransaccionFactory.Parameters.Add(p_fecha_inicio);

                            DbParameter p_fecha_vencimiento = cmdTransaccionFactory.CreateParameter();
                            p_fecha_vencimiento.ParameterName = "p_fecha_vencimiento";
                            if (pRecaudos.fecha_vencimiento_producto != null && pRecaudos.fecha_vencimiento_producto != DateTime.MinValue)
                                p_fecha_vencimiento.Value = pRecaudos.fecha_vencimiento_producto;
                            else
                                p_fecha_vencimiento.Value = DBNull.Value;
                            p_fecha_vencimiento.Direction = ParameterDirection.Input;
                            p_fecha_vencimiento.DbType = DbType.DateTime;
                            cmdTransaccionFactory.Parameters.Add(p_fecha_vencimiento);

                            cmdTransaccionFactory.Connection = connection;
                            cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                            cmdTransaccionFactory.CommandText = "USP_XPINN_REC_DETNOVEDAD_CREAR";
                            cmdTransaccionFactory.ExecuteNonQuery();

                            pRecaudos.iddetalle = Convert.ToInt32(piddetalle.Value);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpresaNovedadData", "CrearDetRecaudosGeneracion", ex);
                        return false;
                    }
                }
            }
        }

        public int? ConsultarTipoLista(string pTipoLista, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<EmpresaNovedad> lstRecaudosMasivos = new List<EmpresaNovedad>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        int? tipo_lista = null;
                        string sql;
                        sql = @"SELECT C.IDTIPO_LISTA FROM TIPO_LISTA_RECAUDO C WHERE C.DESCRIPCION = '" + pTipoLista + "' ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["IDTIPO_LISTA"] != DBNull.Value) tipo_lista = Convert.ToInt32(resultado["IDTIPO_LISTA"]);                            
                        }

                        return tipo_lista;
                    }
                    catch 
                    {
                        return null;
                    }
                }
            }
        }

        public string ConsultarEstadoPersonaAfiliacion(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            string estado = "";
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT estado FROM Persona_Afiliacion WHERE cod_persona = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["ESTADO"] != DBNull.Value) estado = Convert.ToString(resultado["ESTADO"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return estado;
                    }
                    catch
                    {
                        return estado;
                    }
                }
            }
        }



    }

}
