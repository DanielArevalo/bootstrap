using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Icetex.Entities;

namespace Xpinn.Icetex.Data
{
    public class ConvocatoriaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public ConvocatoriaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public List<ConvocatoriaRequerido> ValidacionRequisitos(Int64 pCod_Persona, DateTime pFecha, Usuario vUsuario)
        {
            List<ConvocatoriaRequerido> lstRequisitos = new List<ConvocatoriaRequerido>();
            DbDataReader resultado = default(DbDataReader);
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                connection.Open();
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "P_COD_PERSONA";
                        pcod_persona.Value = pCod_Persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter P_FECHA = cmdTransaccionFactory.CreateParameter();
                        P_FECHA.ParameterName = "P_FECHA";
                        P_FECHA.Value = pFecha;
                        P_FECHA.Direction = ParameterDirection.Input;
                        P_FECHA.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(P_FECHA);

                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_ICT_VALIDAREQUISITOS";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConvocatoriaData", "EjecutarPL", ex);
                        return null;
                    }
                };

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select * from TEMP_REQUISITOS_ICETEX";

                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ConvocatoriaRequerido entidad = new ConvocatoriaRequerido();
                            if (resultado["COD_CONVREQ"] != DBNull.Value) entidad.cod_convreq = Convert.ToInt32(resultado["COD_CONVREQ"]);
                            if (resultado["COD_CONVOCATORIA"] != DBNull.Value) entidad.cod_convocatoria = Convert.ToInt32(resultado["COD_CONVOCATORIA"]);
                            if (resultado["TIPO_REQUISITO"] != DBNull.Value) entidad.tipo_requisito = Convert.ToInt32(resultado["TIPO_REQUISITO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["OBLIGATORIO"] != DBNull.Value) entidad.obligatorio = Convert.ToInt32(resultado["OBLIGATORIO"]); else entidad.obligatorio = 0;
                            if (resultado["ESTADO"] != DBNull.Value) entidad.IsVisible = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["OBSERVACION"] != DBNull.Value) entidad.observacion = Convert.ToString(resultado["OBSERVACION"]);
                            lstRequisitos.Add(entidad);
                        }
                        return lstRequisitos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConvocatoriaData", "EjecutarPL", ex);
                        return null;
                    }
                }
            }
        }


        public ConvocatoriaRequerido CrearConvocatoriaRequerido(ConvocatoriaRequerido pRequerido, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter pcod_convreq = cmdTransaccionFactory.CreateParameter();
                        pcod_convreq.ParameterName = "p_cod_convreq";
                        pcod_convreq.Value = pRequerido.cod_convreq;
                        pcod_convreq.Direction = ParameterDirection.Input;
                        pcod_convreq.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_convreq);

                        DbParameter pcod_convocatoria = cmdTransaccionFactory.CreateParameter();
                        pcod_convocatoria.ParameterName = "p_cod_convocatoria";
                        pcod_convocatoria.Value = pRequerido.cod_convocatoria;
                        pcod_convocatoria.Direction = ParameterDirection.Input;
                        pcod_convocatoria.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_convocatoria);

                        DbParameter ptipo_proceso = cmdTransaccionFactory.CreateParameter();
                        ptipo_proceso.ParameterName = "p_tipo_proceso";
                        if (pRequerido.tipo_proceso == null)
                            ptipo_proceso.Value = DBNull.Value;
                        else
                            ptipo_proceso.Value = pRequerido.tipo_proceso;
                        ptipo_proceso.Direction = ParameterDirection.Input;
                        ptipo_proceso.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_proceso);

                        DbParameter ptipo_requisito = cmdTransaccionFactory.CreateParameter();
                        ptipo_requisito.ParameterName = "p_tipo_requisito";
                        ptipo_requisito.Value = pRequerido.tipo_requisito;
                        ptipo_requisito.Direction = ParameterDirection.Input;
                        ptipo_requisito.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_requisito);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        if (pRequerido.descripcion == null)
                            pdescripcion.Value = DBNull.Value;
                        else
                            pdescripcion.Value = pRequerido.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter pobligatorio = cmdTransaccionFactory.CreateParameter();
                        pobligatorio.ParameterName = "p_obligatorio";
                        if (pRequerido.obligatorio == null)
                            pobligatorio.Value = DBNull.Value;
                        else
                            pobligatorio.Value = pRequerido.obligatorio;
                        pobligatorio.Direction = ParameterDirection.Input;
                        pobligatorio.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pobligatorio);

                        DbParameter pmensaje = cmdTransaccionFactory.CreateParameter();
                        pmensaje.ParameterName = "p_mensaje";
                        if (pRequerido.mensaje == null)
                            pmensaje.Value = DBNull.Value;
                        else
                            pmensaje.Value = pRequerido.mensaje;
                        pmensaje.Direction = ParameterDirection.Input;
                        pmensaje.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pmensaje);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        pRequerido.cod_convreq = Convert.ToInt32(pcod_convreq.Value);

                        return pRequerido;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConvocatoriaData", "CrearConvocatoriaRequerido", ex);
                        return null;
                    }
                }
            }
        }


        public CreditoIcetex CrearCreditoIcetex(CreditoIcetex pCreditoIcetex, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pnumero_credito = cmdTransaccionFactory.CreateParameter();
                        pnumero_credito.ParameterName = "p_numero_credito";
                        pnumero_credito.Value = pCreditoIcetex.numero_credito;
                        pnumero_credito.Direction = ParameterDirection.Output;
                        pnumero_credito.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_credito);

                        DbParameter pcod_convocatoria = cmdTransaccionFactory.CreateParameter();
                        pcod_convocatoria.ParameterName = "p_cod_convocatoria";
                        pcod_convocatoria.Value = pCreditoIcetex.cod_convocatoria;
                        pcod_convocatoria.Direction = ParameterDirection.Input;
                        pcod_convocatoria.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_convocatoria);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pCreditoIcetex.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pfecha_solicitud = cmdTransaccionFactory.CreateParameter();
                        pfecha_solicitud.ParameterName = "p_fecha_solicitud";
                        pfecha_solicitud.Value = pCreditoIcetex.fecha_solicitud;
                        pfecha_solicitud.Direction = ParameterDirection.Input;
                        pfecha_solicitud.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_solicitud);

                        DbParameter ptipo_beneficiario = cmdTransaccionFactory.CreateParameter();
                        ptipo_beneficiario.ParameterName = "p_tipo_beneficiario";
                        ptipo_beneficiario.Value = pCreditoIcetex.tipo_beneficiario;
                        ptipo_beneficiario.Direction = ParameterDirection.Input;
                        ptipo_beneficiario.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipo_beneficiario);

                        DbParameter pidentificacion = cmdTransaccionFactory.CreateParameter();
                        pidentificacion.ParameterName = "p_identificacion";
                        if (pCreditoIcetex.identificacion == null)
                            pidentificacion.Value = DBNull.Value;
                        else
                            pidentificacion.Value = pCreditoIcetex.identificacion;
                        pidentificacion.Direction = ParameterDirection.Input;
                        pidentificacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidentificacion);

                        DbParameter pcodtipoidentificacion = cmdTransaccionFactory.CreateParameter();
                        pcodtipoidentificacion.ParameterName = "p_codtipoidentificacion";
                        if (pCreditoIcetex.codtipoidentificacion == null)
                            pcodtipoidentificacion.Value = DBNull.Value;
                        else
                            pcodtipoidentificacion.Value = pCreditoIcetex.codtipoidentificacion;
                        pcodtipoidentificacion.Direction = ParameterDirection.Input;
                        pcodtipoidentificacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodtipoidentificacion);

                        DbParameter pprimer_nombre = cmdTransaccionFactory.CreateParameter();
                        pprimer_nombre.ParameterName = "p_primer_nombre";
                        if (pCreditoIcetex.primer_nombre == null)
                            pprimer_nombre.Value = DBNull.Value;
                        else
                            pprimer_nombre.Value = pCreditoIcetex.primer_nombre;
                        pprimer_nombre.Direction = ParameterDirection.Input;
                        pprimer_nombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pprimer_nombre);

                        DbParameter psegundo_nombre = cmdTransaccionFactory.CreateParameter();
                        psegundo_nombre.ParameterName = "p_segundo_nombre";
                        if (pCreditoIcetex.segundo_nombre == null)
                            psegundo_nombre.Value = DBNull.Value;
                        else
                            psegundo_nombre.Value = pCreditoIcetex.segundo_nombre;
                        psegundo_nombre.Direction = ParameterDirection.Input;
                        psegundo_nombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(psegundo_nombre);

                        DbParameter pprimer_apellido = cmdTransaccionFactory.CreateParameter();
                        pprimer_apellido.ParameterName = "p_primer_apellido";
                        if (pCreditoIcetex.primer_apellido == null)
                            pprimer_apellido.Value = DBNull.Value;
                        else
                            pprimer_apellido.Value = pCreditoIcetex.primer_apellido;
                        pprimer_apellido.Direction = ParameterDirection.Input;
                        pprimer_apellido.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pprimer_apellido);

                        DbParameter psegundo_apellido = cmdTransaccionFactory.CreateParameter();
                        psegundo_apellido.ParameterName = "p_segundo_apellido";
                        if (pCreditoIcetex.segundo_apellido == null)
                            psegundo_apellido.Value = DBNull.Value;
                        else
                            psegundo_apellido.Value = pCreditoIcetex.segundo_apellido;
                        psegundo_apellido.Direction = ParameterDirection.Input;
                        psegundo_apellido.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(psegundo_apellido);

                        DbParameter pdireccion = cmdTransaccionFactory.CreateParameter();
                        pdireccion.ParameterName = "p_direccion";
                        if (pCreditoIcetex.direccion == null)
                            pdireccion.Value = DBNull.Value;
                        else
                            pdireccion.Value = pCreditoIcetex.direccion;
                        pdireccion.Direction = ParameterDirection.Input;
                        pdireccion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdireccion);

                        DbParameter ptelefono = cmdTransaccionFactory.CreateParameter();
                        ptelefono.ParameterName = "p_telefono";
                        if (pCreditoIcetex.telefono == null)
                            ptelefono.Value = DBNull.Value;
                        else
                            ptelefono.Value = pCreditoIcetex.telefono;
                        ptelefono.Direction = ParameterDirection.Input;
                        ptelefono.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptelefono);

                        DbParameter pemail = cmdTransaccionFactory.CreateParameter();
                        pemail.ParameterName = "p_email";
                        if (pCreditoIcetex.email == null)
                            pemail.Value = DBNull.Value;
                        else
                            pemail.Value = pCreditoIcetex.email;
                        pemail.Direction = ParameterDirection.Input;
                        pemail.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pemail);

                        DbParameter pestrato = cmdTransaccionFactory.CreateParameter();
                        pestrato.ParameterName = "p_estrato";
                        if (pCreditoIcetex.estrato == null)
                            pestrato.Value = DBNull.Value;
                        else
                            pestrato.Value = pCreditoIcetex.estrato;
                        pestrato.Direction = ParameterDirection.Input;
                        pestrato.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestrato);

                        DbParameter pcod_universidad = cmdTransaccionFactory.CreateParameter();
                        pcod_universidad.ParameterName = "p_cod_universidad";
                        pcod_universidad.Value = pCreditoIcetex.cod_universidad;
                        pcod_universidad.Direction = ParameterDirection.Input;
                        pcod_universidad.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_universidad);

                        DbParameter pcod_programa = cmdTransaccionFactory.CreateParameter();
                        pcod_programa.ParameterName = "p_cod_programa";
                        pcod_programa.Value = pCreditoIcetex.cod_programa;
                        pcod_programa.Direction = ParameterDirection.Input;
                        pcod_programa.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_programa);

                        DbParameter ptipo_programa = cmdTransaccionFactory.CreateParameter();
                        ptipo_programa.ParameterName = "p_tipo_programa";
                        ptipo_programa.Value = pCreditoIcetex.tipo_programa;
                        ptipo_programa.Direction = ParameterDirection.Input;
                        ptipo_programa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_programa);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        pvalor.Value = pCreditoIcetex.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter pperiodos = cmdTransaccionFactory.CreateParameter();
                        pperiodos.ParameterName = "p_periodos";
                        if (pCreditoIcetex.periodos == null)
                            pperiodos.Value = DBNull.Value;
                        else
                            pperiodos.Value = pCreditoIcetex.periodos;
                        pperiodos.Direction = ParameterDirection.Input;
                        pperiodos.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pperiodos);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pCreditoIcetex.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_ICT_CREDITOICE_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        pCreditoIcetex.numero_credito = Convert.ToInt64(pnumero_credito.Value);
                        return pCreditoIcetex;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConvocatoriaData", "CrearCreditoIcetex", ex);
                        return null;
                    }
                }
            }
        }


        public CreditoIcetex ModificarCreditoIcetex(CreditoIcetex pCreditoIcetex, ref string pError, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pnumero_credito = cmdTransaccionFactory.CreateParameter();
                        pnumero_credito.ParameterName = "p_numero_credito";
                        pnumero_credito.Value = pCreditoIcetex.numero_credito;
                        pnumero_credito.Direction = ParameterDirection.Input;
                        pnumero_credito.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_credito);

                        DbParameter pcod_convocatoria = cmdTransaccionFactory.CreateParameter();
                        pcod_convocatoria.ParameterName = "p_cod_convocatoria";
                        pcod_convocatoria.Value = pCreditoIcetex.cod_convocatoria;
                        pcod_convocatoria.Direction = ParameterDirection.Input;
                        pcod_convocatoria.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_convocatoria);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pCreditoIcetex.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pfecha_solicitud = cmdTransaccionFactory.CreateParameter();
                        pfecha_solicitud.ParameterName = "p_fecha_solicitud";
                        pfecha_solicitud.Value = pCreditoIcetex.fecha_solicitud;
                        pfecha_solicitud.Direction = ParameterDirection.Input;
                        pfecha_solicitud.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_solicitud);

                        DbParameter ptipo_beneficiario = cmdTransaccionFactory.CreateParameter();
                        ptipo_beneficiario.ParameterName = "p_tipo_beneficiario";
                        ptipo_beneficiario.Value = pCreditoIcetex.tipo_beneficiario;
                        ptipo_beneficiario.Direction = ParameterDirection.Input;
                        ptipo_beneficiario.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipo_beneficiario);

                        DbParameter pidentificacion = cmdTransaccionFactory.CreateParameter();
                        pidentificacion.ParameterName = "p_identificacion";
                        if (pCreditoIcetex.identificacion == null)
                            pidentificacion.Value = DBNull.Value;
                        else
                            pidentificacion.Value = pCreditoIcetex.identificacion;
                        pidentificacion.Direction = ParameterDirection.Input;
                        pidentificacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidentificacion);

                        DbParameter pcodtipoidentificacion = cmdTransaccionFactory.CreateParameter();
                        pcodtipoidentificacion.ParameterName = "p_codtipoidentificacion";
                        if (pCreditoIcetex.codtipoidentificacion == null)
                            pcodtipoidentificacion.Value = DBNull.Value;
                        else
                            pcodtipoidentificacion.Value = pCreditoIcetex.codtipoidentificacion;
                        pcodtipoidentificacion.Direction = ParameterDirection.Input;
                        pcodtipoidentificacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodtipoidentificacion);

                        DbParameter pprimer_nombre = cmdTransaccionFactory.CreateParameter();
                        pprimer_nombre.ParameterName = "p_primer_nombre";
                        if (pCreditoIcetex.primer_nombre == null)
                            pprimer_nombre.Value = DBNull.Value;
                        else
                            pprimer_nombre.Value = pCreditoIcetex.primer_nombre;
                        pprimer_nombre.Direction = ParameterDirection.Input;
                        pprimer_nombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pprimer_nombre);

                        DbParameter psegundo_nombre = cmdTransaccionFactory.CreateParameter();
                        psegundo_nombre.ParameterName = "p_segundo_nombre";
                        if (pCreditoIcetex.segundo_nombre == null)
                            psegundo_nombre.Value = DBNull.Value;
                        else
                            psegundo_nombre.Value = pCreditoIcetex.segundo_nombre;
                        psegundo_nombre.Direction = ParameterDirection.Input;
                        psegundo_nombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(psegundo_nombre);

                        DbParameter pprimer_apellido = cmdTransaccionFactory.CreateParameter();
                        pprimer_apellido.ParameterName = "p_primer_apellido";
                        if (pCreditoIcetex.primer_apellido == null)
                            pprimer_apellido.Value = DBNull.Value;
                        else
                            pprimer_apellido.Value = pCreditoIcetex.primer_apellido;
                        pprimer_apellido.Direction = ParameterDirection.Input;
                        pprimer_apellido.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pprimer_apellido);

                        DbParameter psegundo_apellido = cmdTransaccionFactory.CreateParameter();
                        psegundo_apellido.ParameterName = "p_segundo_apellido";
                        if (pCreditoIcetex.segundo_apellido == null)
                            psegundo_apellido.Value = DBNull.Value;
                        else
                            psegundo_apellido.Value = pCreditoIcetex.segundo_apellido;
                        psegundo_apellido.Direction = ParameterDirection.Input;
                        psegundo_apellido.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(psegundo_apellido);

                        DbParameter pdireccion = cmdTransaccionFactory.CreateParameter();
                        pdireccion.ParameterName = "p_direccion";
                        if (pCreditoIcetex.direccion == null)
                            pdireccion.Value = DBNull.Value;
                        else
                            pdireccion.Value = pCreditoIcetex.direccion;
                        pdireccion.Direction = ParameterDirection.Input;
                        pdireccion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdireccion);

                        DbParameter ptelefono = cmdTransaccionFactory.CreateParameter();
                        ptelefono.ParameterName = "p_telefono";
                        if (pCreditoIcetex.telefono == null)
                            ptelefono.Value = DBNull.Value;
                        else
                            ptelefono.Value = pCreditoIcetex.telefono;
                        ptelefono.Direction = ParameterDirection.Input;
                        ptelefono.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptelefono);

                        DbParameter pemail = cmdTransaccionFactory.CreateParameter();
                        pemail.ParameterName = "p_email";
                        if (pCreditoIcetex.email == null)
                            pemail.Value = DBNull.Value;
                        else
                            pemail.Value = pCreditoIcetex.email;
                        pemail.Direction = ParameterDirection.Input;
                        pemail.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pemail);

                        DbParameter pestrato = cmdTransaccionFactory.CreateParameter();
                        pestrato.ParameterName = "p_estrato";
                        if (pCreditoIcetex.estrato == null)
                            pestrato.Value = DBNull.Value;
                        else
                            pestrato.Value = pCreditoIcetex.estrato;
                        pestrato.Direction = ParameterDirection.Input;
                        pestrato.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestrato);

                        DbParameter pcod_universidad = cmdTransaccionFactory.CreateParameter();
                        pcod_universidad.ParameterName = "p_cod_universidad";
                        pcod_universidad.Value = pCreditoIcetex.cod_universidad;
                        pcod_universidad.Direction = ParameterDirection.Input;
                        pcod_universidad.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_universidad);

                        DbParameter pcod_programa = cmdTransaccionFactory.CreateParameter();
                        pcod_programa.ParameterName = "p_cod_programa";
                        pcod_programa.Value = pCreditoIcetex.cod_programa;
                        pcod_programa.Direction = ParameterDirection.Input;
                        pcod_programa.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_programa);

                        DbParameter ptipo_programa = cmdTransaccionFactory.CreateParameter();
                        ptipo_programa.ParameterName = "p_tipo_programa";
                        ptipo_programa.Value = pCreditoIcetex.tipo_programa;
                        ptipo_programa.Direction = ParameterDirection.Input;
                        ptipo_programa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_programa);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        pvalor.Value = pCreditoIcetex.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter pperiodos = cmdTransaccionFactory.CreateParameter();
                        pperiodos.ParameterName = "p_periodos";
                        if (pCreditoIcetex.periodos == null)
                            pperiodos.Value = DBNull.Value;
                        else
                            pperiodos.Value = pCreditoIcetex.periodos;
                        pperiodos.Direction = ParameterDirection.Input;
                        pperiodos.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pperiodos);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pCreditoIcetex.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_ICT_CREDITOICE_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        pCreditoIcetex.numero_credito = Convert.ToInt64(pnumero_credito.Value);
                        return pCreditoIcetex;
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                        return null;
                    }
                }
            }
        }


        public CreditoIcetex CreditoIcetexInscripcion(CreditoIcetex pCreditoIcetex, ref string pError, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pnumero_credito = cmdTransaccionFactory.CreateParameter();
                        pnumero_credito.ParameterName = "p_numero_credito";
                        pnumero_credito.Value = pCreditoIcetex.numero_credito;
                        pnumero_credito.Direction = ParameterDirection.Input;
                        pnumero_credito.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_credito);

                        DbParameter pfecha_inscripcion = cmdTransaccionFactory.CreateParameter();
                        pfecha_inscripcion.ParameterName = "p_fecha_inscripcion";
                        pfecha_inscripcion.Value = pCreditoIcetex.fecha_inscripcion;
                        pfecha_inscripcion.Direction = ParameterDirection.Input;
                        pfecha_inscripcion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_inscripcion);

                        DbParameter pesconforme = cmdTransaccionFactory.CreateParameter();
                        pesconforme.ParameterName = "p_esconforme";
                        pesconforme.Value = pCreditoIcetex.esconforme;
                        pesconforme.Direction = ParameterDirection.Input;
                        pesconforme.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pesconforme);

                        DbParameter pobservaciones = cmdTransaccionFactory.CreateParameter();
                        pobservaciones.ParameterName = "p_observaciones";
                        if (pCreditoIcetex.observacion == null)
                            pobservaciones.Value = DBNull.Value;
                        else
                            pobservaciones.Value = pCreditoIcetex.observacion;
                        pobservaciones.Direction = ParameterDirection.Input;
                        pobservaciones.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pobservaciones);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pCreditoIcetex.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_ICT_INSCRIPCION";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pCreditoIcetex;
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                        return null;
                    }
                }
            }
        }

        public List<IcetexDocumentos> ListarConvocatoriaDocumentos(string pFiltro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<IcetexDocumentos> lstConvocatoria = new List<IcetexDocumentos>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select C.*, T.Descripcion From Convocatoriaicetexdoc C Inner Join Tipo_Docicetex T
                                       on C.Cod_Tipo_Doc = T.Cod_Tipo_Doc " + pFiltro + " ORDER BY c.COD_CONVDOC ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            IcetexDocumentos entidad = new IcetexDocumentos();
                            if (resultado["COD_CONVDOC"] != DBNull.Value) entidad.cod_convdoc = Convert.ToInt32(resultado["COD_CONVDOC"]);
                            if (resultado["COD_CONVOCATORIA"] != DBNull.Value) entidad.cod_convocatoria = Convert.ToInt32(resultado["COD_CONVOCATORIA"]);
                            if (resultado["COD_TIPO_DOC"] != DBNull.Value) entidad.cod_tipo_doc = Convert.ToInt32(resultado["COD_TIPO_DOC"]);
                            if (resultado["PREGUNTA"] != DBNull.Value) entidad.pregunta = Convert.ToString(resultado["PREGUNTA"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            lstConvocatoria.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstConvocatoria;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConvocatoriaData", "ListarConvocatoriaDocumentos", ex);
                        return null;
                    }
                }
            }
        }


        public IcetexDocumentos ConsultarConvocatoriaCalculo(DateTime pFecha, Usuario vUsuario)
        {
            DbDataReader resultado;
            IcetexDocumentos entidad = new IcetexDocumentos();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"";
                        if (pFecha != null && pFecha != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " And ";
                            else
                                sql += " Where ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " fecha = To_Date('" + Convert.ToDateTime(pFecha).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " fecha = '" + Convert.ToDateTime(pFecha).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COD_CONVDOC"] != DBNull.Value) entidad.cod_convdoc = Convert.ToInt32(resultado["COD_CONVDOC"]);
                            if (resultado["COD_CONVOCATORIA"] != DBNull.Value) entidad.cod_convocatoria = Convert.ToInt32(resultado["COD_CONVOCATORIA"]);
                            if (resultado["COD_TIPO_DOC"] != DBNull.Value) entidad.cod_tipo_doc = Convert.ToInt32(resultado["COD_TIPO_DOC"]);
                            if (resultado["PREGUNTA"] != DBNull.Value) entidad.pregunta = Convert.ToString(resultado["PREGUNTA"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConvocatoriaData", "ConsultarConvocatoriaCalculo", ex);
                        return null;
                    }
                }
            }
        }


        public List<CreditoIcetex> ListarCreditosIcetex(string pFiltro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<CreditoIcetex> lstIcetex = new List<CreditoIcetex>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select C.*,T.Descripcion,Case C.Tipo_Beneficiario When '0' Then 'Asociado' When '1' Then 'Hijo del Asociado' When '2' Then 'Nieto del Asociado'
                                    When '3' Then 'Empleado' Else 'Asociado' End As Nom_Tipo_Beneficiario, U.Descripcion As Nom_Univ, P.Descripcion As Nom_Programa,
                                    CASE C.TIPO_PROGRAMA WHEN 1 THEN 'Especialización (1 año)' when 2 then 'Maestria (2 años)' else 'SIN DATOS' end as nom_tipo_programa,
                                    CASE C.ESTADO WHEN 'S' THEN 'Solicitado' when 'A' then 'Aprobado' WHEN 'I' THEN 'Inscrito' end as nom_estado,
                                    V.IDENTIFICACION AS IDENTIC_ASOC, V.NOMBRE AS NOM_ASOC,  CASE WHEN C.ESTADO != 'S' THEN A.OBSERVACIONES END AS OBSERVACIONES,
                                    CASE WHEN (SELECT MAX(X.TIPO_APROBACION) FROM creditoicetexaprobacion X WHERE X.NUMERO_CREDITO = C.NUMERO_CREDITO) = 2 THEN 2 ELSE 1 END AS TIPO_APROBACION
                                    from Creditoicetex c inner join Tipoidentificacion t on C.Codtipoidentificacion = T.Codtipoidentificacion
                                    Inner Join Universidad U On U.Cod_Universidad = C.Cod_Universidad 
                                    INNER JOIN Programa P ON P.Cod_Programa = C.Cod_Programa and P.COD_UNIVERSIDAD = C.Cod_Universidad 
                                    INNER JOIN V_PERSONA V ON V.COD_PERSONA = C.COD_PERSONA 
                                    LEFT JOIN CREDITOICETEXAPROBACION A ON A.NUMERO_CREDITO = C.NUMERO_CREDITO " + pFiltro.ToString() + " ORDER BY C.NUMERO_CREDITO ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            CreditoIcetex entidad = new CreditoIcetex();
                            if (resultado["NUMERO_CREDITO"] != DBNull.Value) entidad.numero_credito = Convert.ToInt64(resultado["NUMERO_CREDITO"]);
                            if (resultado["COD_CONVOCATORIA"] != DBNull.Value) entidad.cod_convocatoria = Convert.ToInt32(resultado["COD_CONVOCATORIA"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["FECHA_SOLICITUD"] != DBNull.Value) entidad.fecha_solicitud = Convert.ToDateTime(resultado["FECHA_SOLICITUD"]);
                            if (resultado["TIPO_BENEFICIARIO"] != DBNull.Value) entidad.tipo_beneficiario = Convert.ToString(resultado["TIPO_BENEFICIARIO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["CODTIPOIDENTIFICACION"] != DBNull.Value) entidad.codtipoidentificacion = Convert.ToInt32(resultado["CODTIPOIDENTIFICACION"]);
                            if (resultado["PRIMER_NOMBRE"] != DBNull.Value) entidad.primer_nombre = Convert.ToString(resultado["PRIMER_NOMBRE"]);
                            if (resultado["SEGUNDO_NOMBRE"] != DBNull.Value) entidad.segundo_nombre = Convert.ToString(resultado["SEGUNDO_NOMBRE"]);
                            if (resultado["PRIMER_APELLIDO"] != DBNull.Value) entidad.primer_apellido = Convert.ToString(resultado["PRIMER_APELLIDO"]);
                            if (resultado["SEGUNDO_APELLIDO"] != DBNull.Value) entidad.segundo_apellido = Convert.ToString(resultado["SEGUNDO_APELLIDO"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["EMAIL"] != DBNull.Value) entidad.email = Convert.ToString(resultado["EMAIL"]);
                            if (resultado["ESTRATO"] != DBNull.Value) entidad.estrato = Convert.ToInt32(resultado["ESTRATO"]);
                            if (resultado["COD_UNIVERSIDAD"] != DBNull.Value) entidad.cod_universidad = Convert.ToString(resultado["COD_UNIVERSIDAD"]);
                            if (resultado["COD_PROGRAMA"] != DBNull.Value) entidad.cod_programa = Convert.ToString(resultado["COD_PROGRAMA"]);
                            if (resultado["TIPO_PROGRAMA"] != DBNull.Value) entidad.tipo_programa = Convert.ToInt32(resultado["TIPO_PROGRAMA"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["PERIODOS"] != DBNull.Value) entidad.periodos = Convert.ToDecimal(resultado["PERIODOS"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (entidad.estado != null)
                            {
                                switch (entidad.estado)
                                {
                                    case "S":
                                        entidad.nom_estado = "Pre-Inscrito";
                                        break;
                                    case "A":
                                        entidad.nom_estado = "Aprobado";
                                        break;
                                    case "Z":
                                        entidad.nom_estado = "Aplazado";
                                        break;
                                    case "N":
                                        entidad.nom_estado = "Negado";
                                        break;
                                    case "I":
                                        entidad.nom_estado = "Inscrito";
                                        break;
                                }
                            }
                            if (resultado["IDENTIC_ASOC"] != DBNull.Value) entidad.identific_asoc = Convert.ToString(resultado["IDENTIC_ASOC"]);
                            if (resultado["NOM_ASOC"] != DBNull.Value) entidad.nom_asoc = Convert.ToString(resultado["NOM_ASOC"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observacion = Convert.ToString(resultado["OBSERVACIONES"]);
                            //ADICIONADOS
                            entidad.nombre = entidad.primer_nombre + " " + entidad.segundo_nombre + " " + entidad.primer_apellido + " " + entidad.segundo_apellido;
                            entidad.nombre = entidad.nombre.Trim();
                            if (resultado["Descripcion"] != DBNull.Value) entidad.nom_tipoidentificacion = Convert.ToString(resultado["Descripcion"]);
                            if (resultado["Nom_Tipo_Beneficiario"] != DBNull.Value) entidad.nom_tipo_beneficiario = Convert.ToString(resultado["Nom_Tipo_Beneficiario"]);
                            if (resultado["Nom_Univ"] != DBNull.Value) entidad.nom_universidad = Convert.ToString(resultado["Nom_Univ"]);
                            if (resultado["Nom_Programa"] != DBNull.Value) entidad.nom_programa_univ = Convert.ToString(resultado["Nom_Programa"]);
                            if (resultado["nom_tipo_programa"] != DBNull.Value) entidad.nom_tipo_programa = Convert.ToString(resultado["nom_tipo_programa"]);
                            if (resultado["nom_estado"] != DBNull.Value) entidad.nom_estado = Convert.ToString(resultado["nom_estado"]);
                            if (resultado["tipo_aprobacion"] != DBNull.Value) entidad.tipo_aprobacion = Convert.ToInt32(resultado["tipo_aprobacion"]);
                            lstIcetex.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstIcetex;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IcetexData", "ListarIcetex", ex);
                        return null;
                    }
                }
            }
        }


        public ConvocatoriaIcetex ConsultarConvocatoriaIcetex(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            ConvocatoriaIcetex entidad = new ConvocatoriaIcetex();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Convocatoriaicetex WHERE COD_CONVOCATORIA = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COD_CONVOCATORIA"] != DBNull.Value) entidad.cod_convocatoria = Convert.ToInt32(resultado["COD_CONVOCATORIA"]);
                            if (resultado["FECHA_CONVOCATORIA"] != DBNull.Value) entidad.fecha_convocatoria = Convert.ToDateTime(resultado["FECHA_CONVOCATORIA"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["FECHA_INICIO"] != DBNull.Value) entidad.fecha_inicio = Convert.ToDateTime(resultado["FECHA_INICIO"]);
                            if (resultado["FECHA_FINAL"] != DBNull.Value) entidad.fecha_final = Convert.ToDateTime(resultado["FECHA_FINAL"]);
                            if (resultado["MENSAJE_SOLICITUD"] != DBNull.Value) entidad.mensaje_solicitud = Convert.ToString(resultado["MENSAJE_SOLICITUD"]);
                            if (resultado["NUMERO_CREDITOS"] != DBNull.Value) entidad.numero_creditos = Convert.ToInt32(resultado["NUMERO_CREDITOS"]);
                            if (resultado["fec_ini_inscripcion"] != DBNull.Value) entidad.fec_ini_inscripcion = Convert.ToDateTime(resultado["fec_ini_inscripcion"]);
                            if (resultado["fec_fin_inscripcion"] != DBNull.Value) entidad.fec_fin_inscripcion = Convert.ToDateTime(resultado["fec_fin_inscripcion"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IcetexData", "ConsultarConvocatoriaIcetex", ex);
                        return null;
                    }
                }
            }
        }


        public List<ConvocatoriaIcetex> ListarConvocatoriaIcetex(string pFiltro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ConvocatoriaIcetex> lstConvocatoria = new List<ConvocatoriaIcetex>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Convocatoriaicetex " + pFiltro.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ConvocatoriaIcetex entidad = new ConvocatoriaIcetex();
                            if (resultado["COD_CONVOCATORIA"] != DBNull.Value) entidad.cod_convocatoria = Convert.ToInt32(resultado["COD_CONVOCATORIA"]);
                            if (resultado["FECHA_CONVOCATORIA"] != DBNull.Value) entidad.fecha_convocatoria = Convert.ToDateTime(resultado["FECHA_CONVOCATORIA"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["FECHA_INICIO"] != DBNull.Value) entidad.fecha_inicio = Convert.ToDateTime(resultado["FECHA_INICIO"]);
                            if (resultado["FECHA_FINAL"] != DBNull.Value) entidad.fecha_final = Convert.ToDateTime(resultado["FECHA_FINAL"]);
                            if (resultado["MENSAJE_SOLICITUD"] != DBNull.Value) entidad.mensaje_solicitud = Convert.ToString(resultado["MENSAJE_SOLICITUD"]);
                            if (resultado["NUMERO_CREDITOS"] != DBNull.Value) entidad.numero_creditos = Convert.ToInt32(resultado["NUMERO_CREDITOS"]);
                            lstConvocatoria.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstConvocatoria;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IcetexData", "ListarConvocatoriaIcetex", ex);
                        return null;
                    }
                }
            }
        }

        public CreditoIcetex ConsultarCreditoIcetex(string pFiltro, Usuario vUsuario)
        {
            DbDataReader resultado;
            CreditoIcetex entidad = new CreditoIcetex();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT c.*, V.NOMBRE as nom_asoc, v.identificacion as identif_asoc, I.DESCRIPCION AS NOM_CONVOCATORIA,
                                CASE WHEN (SELECT MAX(X.TIPO_APROBACION) FROM creditoicetexaprobacion X WHERE X.NUMERO_CREDITO = C.NUMERO_CREDITO) = 2 THEN 2 ELSE 1 END AS TIPO_APROBACION
                                FROM CREDITOICETEX C INNER JOIN V_PERSONA V ON C.COD_PERSONA = V.COD_PERSONA
                                INNER JOIN CONVOCATORIAICETEX I ON C.COD_CONVOCATORIA = I.COD_CONVOCATORIA " + pFiltro.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["NUMERO_CREDITO"] != DBNull.Value) entidad.numero_credito = Convert.ToInt64(resultado["NUMERO_CREDITO"]);
                            if (resultado["COD_CONVOCATORIA"] != DBNull.Value) entidad.cod_convocatoria = Convert.ToInt32(resultado["COD_CONVOCATORIA"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["FECHA_SOLICITUD"] != DBNull.Value) entidad.fecha_solicitud = Convert.ToDateTime(resultado["FECHA_SOLICITUD"]);
                            if (resultado["TIPO_BENEFICIARIO"] != DBNull.Value) entidad.tipo_beneficiario = Convert.ToString(resultado["TIPO_BENEFICIARIO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["CODTIPOIDENTIFICACION"] != DBNull.Value) entidad.codtipoidentificacion = Convert.ToInt32(resultado["CODTIPOIDENTIFICACION"]);
                            if (resultado["PRIMER_NOMBRE"] != DBNull.Value) entidad.primer_nombre = Convert.ToString(resultado["PRIMER_NOMBRE"]);
                            if (resultado["SEGUNDO_NOMBRE"] != DBNull.Value) entidad.segundo_nombre = Convert.ToString(resultado["SEGUNDO_NOMBRE"]);
                            if (resultado["PRIMER_APELLIDO"] != DBNull.Value) entidad.primer_apellido = Convert.ToString(resultado["PRIMER_APELLIDO"]);
                            if (resultado["SEGUNDO_APELLIDO"] != DBNull.Value) entidad.segundo_apellido = Convert.ToString(resultado["SEGUNDO_APELLIDO"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["EMAIL"] != DBNull.Value) entidad.email = Convert.ToString(resultado["EMAIL"]);
                            if (resultado["ESTRATO"] != DBNull.Value) entidad.estrato = Convert.ToInt32(resultado["ESTRATO"]);
                            if (resultado["COD_UNIVERSIDAD"] != DBNull.Value) entidad.cod_universidad = Convert.ToString(resultado["COD_UNIVERSIDAD"]);
                            if (resultado["COD_PROGRAMA"] != DBNull.Value) entidad.cod_programa = Convert.ToString(resultado["COD_PROGRAMA"]);
                            if (resultado["TIPO_PROGRAMA"] != DBNull.Value) entidad.tipo_programa = Convert.ToInt32(resultado["TIPO_PROGRAMA"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["PERIODOS"] != DBNull.Value) entidad.periodos = Convert.ToDecimal(resultado["PERIODOS"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            //ADICIONADO
                            if (resultado["nom_asoc"] != DBNull.Value) entidad.nom_asoc = Convert.ToString(resultado["nom_asoc"]);
                            if (resultado["identif_asoc"] != DBNull.Value) entidad.identific_asoc = Convert.ToString(resultado["identif_asoc"]);
                            if (resultado["NOM_CONVOCATORIA"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOM_CONVOCATORIA"]);
                            if (resultado["TIPO_APROBACION"] != DBNull.Value) entidad.tipo_aprobacion = Convert.ToInt32(resultado["TIPO_APROBACION"]); 
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IcetexData", "ConsultarCreditoIcetex", ex);
                        return null;
                    }
                }
            }
        }



        public List<Reporte> ListarReporteCreditosIcetex(string pFiltro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Reporte> lstCredito = new List<Reporte>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT C.fecha_solicitud, C.VALOR, V.IDENTIFICACION, C.COD_PERSONA, 
                                    TRIM(TRIM(V.PRIMER_NOMBRE) || ' ' || TRIM(V.SEGUNDO_NOMBRE) || ' ' || TRIM(V.PRIMER_APELLIDO) || ' ' || TRIM(V.SEGUNDO_APELLIDO)) AS NOM_ASOCIADO,
                                    TRIM(TRIM(C.PRIMER_NOMBRE) || ' ' || TRIM(C.SEGUNDO_NOMBRE) || ' ' || TRIM(C.PRIMER_APELLIDO) || ' ' || TRIM(C.SEGUNDO_APELLIDO)) AS NOM_BENEFICIARIO,
                                    CASE C.TIPO_PROGRAMA WHEN 1 THEN 'Especialización(1 año)' WHEN 2 THEN 'Maestria(2 años)' else 'SIN DATOS' END AS TIPO_PROGRAMA,
                                    CASE C.TIPO_BENEFICIARIO WHEN '0' THEN 'Asociado' WHEN '1' THEN 'Hijo del Asociado' WHEN '2' THEN 'Nieto del Asociado' WHEN '3' THEN 'Empleado' END AS TIPO_BENEFICIARIO,
                                    U.DESCRIPCION, C.PERIODOS, V.ESTRATO, CASE C.ESTADO WHEN 'A' THEN A.OBSERVACIONES WHEN 'I' THEN C.OBSERVACIONES ELSE NULL END AS OBSERVACIONES,
                                    C.NUMERO_CREDITO, C.ESTADO, CASE C.ESCONFORME WHEN 1 then 'SI' WHEN 0 THEN 'NO' ELSE null END AS ESCONFORME
                                    FROM CREDITOICETEX C LEFT JOIN CREDITOICETEXAPROBACION A ON C.NUMERO_CREDITO = A.NUMERO_CREDITO
                                    INNER JOIN PERSONA V ON V.COD_PERSONA = C.COD_PERSONA
                                    LEFT JOIN UNIVERSIDAD U ON C.COD_UNIVERSIDAD = U.COD_UNIVERSIDAD " + pFiltro.ToString() + " ORDER BY V.ESTRATO, C.PERIODOS, C.TIPO_BENEFICIARIO, C.TIPO_PROGRAMA, A.NUMERO_CREDITO";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Reporte entidad = new Reporte();
                            if (resultado["fecha_solicitud"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["fecha_solicitud"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.monto = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.codigo = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["NOM_ASOCIADO"] != DBNull.Value) entidad.nom_asociado = Convert.ToString(resultado["NOM_ASOCIADO"]);
                            if (resultado["NOM_BENEFICIARIO"] != DBNull.Value) entidad.nom_beneficiario = Convert.ToString(resultado["NOM_BENEFICIARIO"]);
                            if (resultado["TIPO_PROGRAMA"] != DBNull.Value) entidad.programa = Convert.ToString(resultado["TIPO_PROGRAMA"]);
                            if (resultado["TIPO_BENEFICIARIO"] != DBNull.Value) entidad.tipo_beneficiario = Convert.ToString(resultado["TIPO_BENEFICIARIO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.institucion_univ = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["PERIODOS"] != DBNull.Value) entidad.semestre = Convert.ToInt32(resultado["PERIODOS"]);
                            if (resultado["ESTRATO"] != DBNull.Value) entidad.estrato = Convert.ToInt32(resultado["ESTRATO"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observacion = Convert.ToString(resultado["OBSERVACIONES"]);
                            if (resultado["NUMERO_CREDITO"] != DBNull.Value) entidad.numero_credito = Convert.ToInt64(resultado["NUMERO_CREDITO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["ESCONFORME"] != DBNull.Value) entidad.esconforme = Convert.ToString(resultado["ESCONFORME"]);
                            lstCredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IcetexData", "ListarReporteCreditosIcetex", ex);
                        return null;
                    }
                }
            }
        }


    }
}
