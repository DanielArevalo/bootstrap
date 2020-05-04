using System;
using System.Collections.Generic;
using System.Text;
using Xpinn.Util;
using System.Data;
using System.Data.Common;
using Xpinn.ActivosFijos.Entities;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Data
{
    public class GarantiaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public GarantiaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public List<Garantia> Listar(Int64 pNumeroRadicacion, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Garantia> lstGarantias = new List<Garantia>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        //DbParameter pNumRadicacion = cmdTransaccionFactory.CreateParameter();
                        //pNumRadicacion.ParameterName = "P_NUMRADICACION";
                        //pNumRadicacion.Direction = ParameterDirection.Input;
                        //pNumRadicacion.Value = pNumeroRadicacion;

                        //OracleParameter pData = (OracleParameter)cmdTransaccionFactory.CreateParameter();
                        //pData.ParameterName = "P_DATA";
                        //pData.Direction = ParameterDirection.Output;
                        //pData.OracleType = OracleType.Cursor;

                        //cmdTransaccionFactory.Parameters.Add(pNumRadicacion);
                        //cmdTransaccionFactory.Parameters.Add(pData);

                        //connection.Open();
                        //cmdTransaccionFactory.Connection = connection;
                        //cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        //cmdTransaccionFactory.CommandText = "XPF_AS_GARANTIA_CONSULTAR";
                        //resultado = cmdTransaccionFactory.ExecuteReader();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText =
                            @"SELECT * FROM VAsesoresDatosGarantias
                                WHERE	numero_radicacion = " + pNumeroRadicacion;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Garantia entidad = new Garantia();

                            if (resultado["numero_radicacion"] != DBNull.Value) entidad.NumeroRadicacion = Convert.ToInt64(resultado["numero_radicacion"].ToString());
                            if (resultado["idgarantia"] != DBNull.Value) entidad.IdGarantia = Convert.ToInt64(resultado["idgarantia"].ToString());
                            if (resultado["Tipo_garantia"] != DBNull.Value) entidad.Tipo = resultado["Tipo_garantia"].ToString();
                            if (resultado["fecha_garantia"] != DBNull.Value) entidad.FechaGarantia = Convert.ToDateTime(resultado["fecha_garantia"].ToString());
                            if (resultado["descripcion"] != DBNull.Value) entidad.Descripcion = resultado["descripcion"].ToString();
                            if (resultado["valor_garantia"] != DBNull.Value) entidad.Valor = Convert.ToInt64(resultado["valor_garantia"].ToString());
                            if (resultado["estado_garantia"] != DBNull.Value) entidad.Estado = resultado["estado_garantia"].ToString();

                            lstGarantias.Add(entidad);
                        }

                        return lstGarantias;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("GarantiaData", "Listar", ex);
                        return null;
                    }
                }
            }
        }

        public string ConsultarCliente(string nradicacion, Usuario _usuario)
        {
            string cod_deudor = string.Empty;
            DbDataReader resultado = default(DbDataReader);

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(_usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select c.cod_deudor 
                                        from credito c
                                        where c.NUMERO_RADICACION =  " + nradicacion;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();


                        while (resultado.Read())
                        {
                            if (resultado["cod_deudor"] != DBNull.Value) cod_deudor = Convert.ToString(resultado["cod_deudor"]);
                        }

                        return cod_deudor;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("GarantiaData", "ConsultarCliente", ex);
                        return null;
                    }
                }
            }
        }

        public ActivoFijo ConsultarActivoFijoPersonal(long idActivoFijo, Usuario pUsuario)
        {
            DbDataReader resultado;
            ActivoFijo entidad = new ActivoFijo();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @" SELECT act.*, tip.CLASE
                                        FROM ACTIVOS_PERSONA  act
                                        JOIN Tipo_Activo_per tip on tip.COD_TIPO_ACTIVO_PER = act.COD_TIPO_ACTIVO_PER
                                        where IDACTIVO = " + idActivoFijo.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDACTIVO"] != DBNull.Value) entidad.idActivo = Convert.ToInt32(resultado["IDACTIVO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["COD_TIPO_ACTIVO_PER"] != DBNull.Value) entidad.cod_tipo_activo_per = Convert.ToInt32(resultado["COD_TIPO_ACTIVO_PER"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["FECHA_ADQUISICION"] != DBNull.Value) entidad.fecha_compra = Convert.ToDateTime(resultado["FECHA_ADQUISICION"]);
                            if (resultado["VALOR_COMERCIAL"] != DBNull.Value) entidad.valor_compra = Convert.ToDecimal(resultado["VALOR_COMERCIAL"]);
                            if (resultado["VALOR_COMPROMETIDO"] != DBNull.Value) entidad.valor_comprometido = Convert.ToDecimal(resultado["VALOR_COMPROMETIDO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["LOCALIZACION"] != DBNull.Value) entidad.localizacion = Convert.ToString(resultado["LOCALIZACION"]);
                            if (resultado["MATRICULA"] != DBNull.Value) entidad.matricula = Convert.ToString(resultado["MATRICULA"]);
                            if (resultado["ESCRITURA"] != DBNull.Value) entidad.escritura = Convert.ToString(resultado["ESCRITURA"]);
                            if (resultado["NOTARIA"] != DBNull.Value) entidad.notaria = Convert.ToString(resultado["NOTARIA"]);
                            if (resultado["MARCA"] != DBNull.Value) entidad.marca = Convert.ToString(resultado["MARCA"]);
                            if (resultado["REFERENCIA"] != DBNull.Value) entidad.referencia = Convert.ToString(resultado["REFERENCIA"]);
                            if (resultado["MODELO"] != DBNull.Value) entidad.modelo = Convert.ToString(resultado["MODELO"]);
                            if (resultado["USO"] != DBNull.Value) entidad.cod_uso = Convert.ToInt32(resultado["USO"]);
                            if (resultado["CAPACIDAD"] != DBNull.Value) entidad.capacidad = Convert.ToString(resultado["CAPACIDAD"]);
                            if (resultado["NUMERO_CHASIS"] != DBNull.Value) entidad.num_chasis = Convert.ToString(resultado["NUMERO_CHASIS"]);
                            if (resultado["SERIE_MOTOR"] != DBNull.Value) entidad.num_motor = Convert.ToString(resultado["SERIE_MOTOR"]);
                            if (resultado["PLACA"] != DBNull.Value) entidad.placa = Convert.ToString(resultado["PLACA"]);
                            if (resultado["COLOR"] != DBNull.Value) entidad.color = Convert.ToString(resultado["COLOR"]);
                            if (resultado["DOCIMPORTACION"] != DBNull.Value) entidad.documentos_importacion = Convert.ToString(resultado["DOCIMPORTACION"]);
                            if (resultado["FIMPORTACION"] != DBNull.Value) entidad.fecha_importacion = Convert.ToDateTime(resultado["FIMPORTACION"]);
                            if (resultado["RANGOVIVIENDA"] != DBNull.Value) entidad.rango_vivienda = Convert.ToString(resultado["RANGOVIVIENDA"]);
                            if (resultado["TIPOVIVIENDA"] != DBNull.Value) entidad.tipo_vivienda = Convert.ToString(resultado["TIPOVIVIENDA"]);
                            if (resultado["SENALVIS"] != DBNull.Value) entidad.SENALVIS = Convert.ToInt32(resultado["SENALVIS"]);
                            if (resultado["ENTIDAD_REDESCUENTO"] != DBNull.Value) entidad.entidad_redescuento = Convert.ToString(resultado["ENTIDAD_REDESCUENTO"]);
                            if (resultado["MARGEN_REDESCUENTO"] != DBNull.Value) entidad.margen_redescuento = Convert.ToString(resultado["MARGEN_REDESCUENTO"]);
                            if (resultado["DESEMBOLSO_DIRECTO"] != DBNull.Value) entidad.desembolso_directo = Convert.ToString(resultado["DESEMBOLSO_DIRECTO"]);
                            if (resultado["DESEMBOLSO"] != DBNull.Value) entidad.desembolso = Convert.ToString(resultado["DESEMBOLSO"]);
                            if (resultado["CLASE"] != DBNull.Value) entidad.str_clase = Convert.ToString(resultado["CLASE"]);
                            if (resultado["HIPOTECA"] != DBNull.Value) entidad.hipoteca = Convert.ToInt32(resultado["HIPOTECA"]);
                            if (resultado["PORCENTAJE_PIGNORADO"] != DBNull.Value) entidad.porcentaje_pignorado = Convert.ToInt32(resultado["PORCENTAJE_PIGNORADO"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActivoFijoData", "ConsultarActivoFijoPersonal", ex);
                        return null;
                    }
                }
            }
        }

        public ActivoFijo ModificarActivoFijoPersonal(ActivoFijo pActivoFijo, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidactivo = cmdTransaccionFactory.CreateParameter();
                        pidactivo.ParameterName = "p_idactivo";
                        pidactivo.Value = pActivoFijo.idActivo;
                        pidactivo.Direction = ParameterDirection.Input;
                        pidactivo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidactivo);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        if (pActivoFijo.cod_persona == null)
                            pcod_persona.Value = DBNull.Value;
                        else
                            pcod_persona.Value = pActivoFijo.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pcod_tipo_activo_per = cmdTransaccionFactory.CreateParameter();
                        pcod_tipo_activo_per.ParameterName = "p_cod_tipo_activo_per";
                        pcod_tipo_activo_per.Value = pActivoFijo.cod_tipo_activo_per;
                        pcod_tipo_activo_per.Direction = ParameterDirection.Input;
                        pcod_tipo_activo_per.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_tipo_activo_per);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        if (pActivoFijo.descripcion == null)
                            pdescripcion.Value = DBNull.Value;
                        else
                            pdescripcion.Value = pActivoFijo.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter pfecha_adquisicion = cmdTransaccionFactory.CreateParameter();
                        pfecha_adquisicion.ParameterName = "p_fecha_adquisicion";
                        pfecha_adquisicion.Value = pActivoFijo.fecha_compra;
                        pfecha_adquisicion.Direction = ParameterDirection.Input;
                        pfecha_adquisicion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_adquisicion);

                        DbParameter pvalor_comercial = cmdTransaccionFactory.CreateParameter();
                        pvalor_comercial.ParameterName = "p_valor_comercial";
                        pvalor_comercial.Value = pActivoFijo.valor_compra;
                        pvalor_comercial.Direction = ParameterDirection.Input;
                        pvalor_comercial.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_comercial);

                        DbParameter pvalor_comprometido = cmdTransaccionFactory.CreateParameter();
                        pvalor_comprometido.ParameterName = "p_valor_comprometido";
                        if (pActivoFijo.valor_comprometido == null)
                            pvalor_comprometido.Value = DBNull.Value;
                        else
                            pvalor_comprometido.Value = pActivoFijo.valor_comprometido;
                        pvalor_comprometido.Direction = ParameterDirection.Input;
                        pvalor_comprometido.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_comprometido);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        if (pActivoFijo.estado == null)
                            pestado.Value = DBNull.Value;
                        else
                            pestado.Value = pActivoFijo.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pdireccion = cmdTransaccionFactory.CreateParameter();
                        pdireccion.ParameterName = "p_direccion";
                        if (pActivoFijo.direccion == null)
                            pdireccion.Value = DBNull.Value;
                        else
                            pdireccion.Value = pActivoFijo.direccion;
                        pdireccion.Direction = ParameterDirection.Input;
                        pdireccion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdireccion);

                        DbParameter plocalizacion = cmdTransaccionFactory.CreateParameter();
                        plocalizacion.ParameterName = "p_localizacion";
                        if (pActivoFijo.localizacion == null)
                            plocalizacion.Value = DBNull.Value;
                        else
                            plocalizacion.Value = pActivoFijo.localizacion;
                        plocalizacion.Direction = ParameterDirection.Input;
                        plocalizacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(plocalizacion);

                        DbParameter pmatricula = cmdTransaccionFactory.CreateParameter();
                        pmatricula.ParameterName = "p_matricula";
                        if (pActivoFijo.matricula == null)
                            pmatricula.Value = DBNull.Value;
                        else
                            pmatricula.Value = pActivoFijo.matricula;
                        pmatricula.Direction = ParameterDirection.Input;
                        pmatricula.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pmatricula);

                        DbParameter pescritura = cmdTransaccionFactory.CreateParameter();
                        pescritura.ParameterName = "p_escritura";
                        if (pActivoFijo.escritura == null)
                            pescritura.Value = DBNull.Value;
                        else
                            pescritura.Value = pActivoFijo.escritura;
                        pescritura.Direction = ParameterDirection.Input;
                        pescritura.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pescritura);

                        DbParameter pnotaria = cmdTransaccionFactory.CreateParameter();
                        pnotaria.ParameterName = "p_notaria";
                        if (pActivoFijo.notaria == null)
                            pnotaria.Value = DBNull.Value;
                        else
                            pnotaria.Value = pActivoFijo.notaria;
                        pnotaria.Direction = ParameterDirection.Input;
                        pnotaria.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnotaria);

                        DbParameter pmarca = cmdTransaccionFactory.CreateParameter();
                        pmarca.ParameterName = "p_marca";
                        if (pActivoFijo.marca == null)
                            pmarca.Value = DBNull.Value;
                        else
                            pmarca.Value = pActivoFijo.marca;
                        pmarca.Direction = ParameterDirection.Input;
                        pmarca.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pmarca);

                        DbParameter preferencia = cmdTransaccionFactory.CreateParameter();
                        preferencia.ParameterName = "p_referencia";
                        if (pActivoFijo.referencia == null)
                            preferencia.Value = DBNull.Value;
                        else
                            preferencia.Value = pActivoFijo.referencia;
                        preferencia.Direction = ParameterDirection.Input;
                        preferencia.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(preferencia);

                        DbParameter pmodelo = cmdTransaccionFactory.CreateParameter();
                        pmodelo.ParameterName = "p_modelo";
                        if (pActivoFijo.modelo == null)
                            pmodelo.Value = DBNull.Value;
                        else
                            pmodelo.Value = pActivoFijo.modelo;
                        pmodelo.Direction = ParameterDirection.Input;
                        pmodelo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pmodelo);

                        DbParameter puso = cmdTransaccionFactory.CreateParameter();
                        puso.ParameterName = "p_uso";
                        if (pActivoFijo.cod_uso == null)
                            puso.Value = DBNull.Value;
                        else
                            puso.Value = pActivoFijo.cod_uso;
                        puso.Direction = ParameterDirection.Input;
                        puso.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(puso);

                        DbParameter pcapacidad = cmdTransaccionFactory.CreateParameter();
                        pcapacidad.ParameterName = "p_capacidad";
                        if (pActivoFijo.capacidad == null)
                            pcapacidad.Value = DBNull.Value;
                        else
                            pcapacidad.Value = pActivoFijo.capacidad;
                        pcapacidad.Direction = ParameterDirection.Input;
                        pcapacidad.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcapacidad);

                        DbParameter pnumero_chasis = cmdTransaccionFactory.CreateParameter();
                        pnumero_chasis.ParameterName = "p_numero_chasis";
                        if (pActivoFijo.num_chasis == null)
                            pnumero_chasis.Value = DBNull.Value;
                        else
                            pnumero_chasis.Value = pActivoFijo.num_chasis;
                        pnumero_chasis.Direction = ParameterDirection.Input;
                        pnumero_chasis.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumero_chasis);

                        DbParameter pserie_motor = cmdTransaccionFactory.CreateParameter();
                        pserie_motor.ParameterName = "p_serie_motor";
                        if (pActivoFijo.num_motor == null)
                            pserie_motor.Value = DBNull.Value;
                        else
                            pserie_motor.Value = pActivoFijo.num_motor;
                        pserie_motor.Direction = ParameterDirection.Input;
                        pserie_motor.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pserie_motor);

                        DbParameter pplaca = cmdTransaccionFactory.CreateParameter();
                        pplaca.ParameterName = "p_placa";
                        if (pActivoFijo.placa == null)
                            pplaca.Value = DBNull.Value;
                        else
                            pplaca.Value = pActivoFijo.placa;
                        pplaca.Direction = ParameterDirection.Input;
                        pplaca.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pplaca);

                        DbParameter pcolor = cmdTransaccionFactory.CreateParameter();
                        pcolor.ParameterName = "p_color";
                        if (pActivoFijo.color == null)
                            pcolor.Value = DBNull.Value;
                        else
                            pcolor.Value = pActivoFijo.color;
                        pcolor.Direction = ParameterDirection.Input;
                        pcolor.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcolor);

                        DbParameter pdocimportacion = cmdTransaccionFactory.CreateParameter();
                        pdocimportacion.ParameterName = "p_docimportacion";
                        if (pActivoFijo.documentos_importacion == null)
                            pdocimportacion.Value = DBNull.Value;
                        else
                            pdocimportacion.Value = pActivoFijo.documentos_importacion;
                        pdocimportacion.Direction = ParameterDirection.Input;
                        pdocimportacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdocimportacion);

                        DbParameter pfimportacion = cmdTransaccionFactory.CreateParameter();
                        pfimportacion.ParameterName = "p_fimportacion";
                        if (pActivoFijo.fecha_importacion == null)
                            pfimportacion.Value = DBNull.Value;
                        else
                            pfimportacion.Value = pActivoFijo.fecha_importacion;
                        pfimportacion.Direction = ParameterDirection.Input;
                        pfimportacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfimportacion);

                        DbParameter prangovivienda = cmdTransaccionFactory.CreateParameter();
                        prangovivienda.ParameterName = "p_rangovivienda";
                        if (pActivoFijo.rango_vivienda == null)
                            prangovivienda.Value = DBNull.Value;
                        else
                            prangovivienda.Value = pActivoFijo.rango_vivienda;
                        prangovivienda.Direction = ParameterDirection.Input;
                        prangovivienda.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(prangovivienda);

                        DbParameter ptipovivienda = cmdTransaccionFactory.CreateParameter();
                        ptipovivienda.ParameterName = "p_tipovivienda";
                        if (pActivoFijo.tipo_vivienda == null)
                            ptipovivienda.Value = DBNull.Value;
                        else
                            ptipovivienda.Value = pActivoFijo.tipo_vivienda;
                        ptipovivienda.Direction = ParameterDirection.Input;
                        ptipovivienda.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipovivienda);

                        DbParameter psenalvis = cmdTransaccionFactory.CreateParameter();
                        psenalvis.ParameterName = "p_senalvis";
                        if (pActivoFijo.SENALVIS == null)
                            psenalvis.Value = DBNull.Value;
                        else
                            psenalvis.Value = pActivoFijo.SENALVIS;
                        psenalvis.Direction = ParameterDirection.Input;
                        psenalvis.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(psenalvis);

                        DbParameter pentidad_redescuento = cmdTransaccionFactory.CreateParameter();
                        pentidad_redescuento.ParameterName = "p_entidad_redescuento";
                        if (pActivoFijo.entidad_redescuento == null)
                            pentidad_redescuento.Value = DBNull.Value;
                        else
                            pentidad_redescuento.Value = pActivoFijo.entidad_redescuento;
                        pentidad_redescuento.Direction = ParameterDirection.Input;
                        pentidad_redescuento.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pentidad_redescuento);

                        DbParameter pmargen_redescuento = cmdTransaccionFactory.CreateParameter();
                        pmargen_redescuento.ParameterName = "p_margen_redescuento";
                        if (pActivoFijo.margen_redescuento == null)
                            pmargen_redescuento.Value = DBNull.Value;
                        else
                            pmargen_redescuento.Value = pActivoFijo.margen_redescuento;
                        pmargen_redescuento.Direction = ParameterDirection.Input;
                        pmargen_redescuento.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pmargen_redescuento);

                        DbParameter pdesembolso_directo = cmdTransaccionFactory.CreateParameter();
                        pdesembolso_directo.ParameterName = "p_desembolso_directo";
                        if (pActivoFijo.desembolso_directo == null)
                            pdesembolso_directo.Value = DBNull.Value;
                        else
                            pdesembolso_directo.Value = pActivoFijo.desembolso_directo;
                        pdesembolso_directo.Direction = ParameterDirection.Input;
                        pdesembolso_directo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdesembolso_directo);

                        DbParameter pdesembolso = cmdTransaccionFactory.CreateParameter();
                        pdesembolso.ParameterName = "p_desembolso";
                        if (pActivoFijo.desembolso == null)
                            pdesembolso.Value = DBNull.Value;
                        else
                            pdesembolso.Value = pActivoFijo.desembolso;
                        pdesembolso.Direction = ParameterDirection.Input;
                        pdesembolso.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdesembolso);

                        DbParameter p_hipoteca = cmdTransaccionFactory.CreateParameter();
                        p_hipoteca.ParameterName = "p_hipoteca";
                        if (pActivoFijo.hipoteca == null)
                            p_hipoteca.Value = DBNull.Value;
                        else
                            p_hipoteca.Value = pActivoFijo.hipoteca;
                        p_hipoteca.Direction = ParameterDirection.Input;
                        p_hipoteca.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_hipoteca);

                        DbParameter p_porcentaje_pignorado = cmdTransaccionFactory.CreateParameter();
                        p_porcentaje_pignorado.ParameterName = "p_porcentaje_pignorado";
                        if (pActivoFijo.porcentaje_pignorado == null)
                            p_porcentaje_pignorado.Value = DBNull.Value;
                        else
                            p_porcentaje_pignorado.Value = pActivoFijo.porcentaje_pignorado;
                        p_porcentaje_pignorado.Direction = ParameterDirection.Input;
                        p_porcentaje_pignorado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_porcentaje_pignorado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_ACT_ACTIVOS_PE_MOD";
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

        public void EliminarGarantia(long pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidactivo = cmdTransaccionFactory.CreateParameter();
                        pidactivo.ParameterName = "p_idgarantia";
                        pidactivo.Value = pId;
                        pidactivo.Direction = ParameterDirection.Input;
                        pidactivo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidactivo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_GARANTIAS_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActivoFijoData", "EliminarGarantia", ex);
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Garantias dados unos filtros - CRUD
        /// </summary>
        /// <param name="pTransaccionesCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Garantias obtenidos</returns>
        public List<Garantia> ListarFullGarantias(string filtro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Garantia> lstGarantia = new List<Garantia>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT g.IDGARANTIA, g.TIPO_GARANTIA, g.UBICACION, g.ESTADO, g.NUMERO_RADICACION, g.FECHA_GARANTIA , g.VALOR_GARANTIA, g.FECHA_LIBERACION, g.ENCARGADO, g.FECHA_AVALUO, g.FECHA_VEN_SEG, per.IDENTIFICACION
                                       FROM Garantias g 
                                       JOIN Credito c on c.NUMERO_RADICACION = g.NUMERO_RADICACION
                                       JOIN LINEASCREDITO l on l.COD_LINEA_CREDITO = c.COD_LINEA_CREDITO
                                       JOIN PERSONA per on per.COD_PERSONA = c.COD_DEUDOR " + filtro;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Garantia entidad = new Garantia();

                            if (resultado["IDGARANTIA"] != DBNull.Value) entidad.IdGarantia = Convert.ToInt64(resultado["IDGARANTIA"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.NumeroRadicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["FECHA_GARANTIA"] != DBNull.Value) entidad.FechaGarantia = Convert.ToDateTime(resultado["FECHA_GARANTIA"]);

                            if (resultado["TIPO_GARANTIA"] != DBNull.Value)
                            {
                                entidad.nombre_tipo_garantia = Convert.ToInt32(resultado["TIPO_GARANTIA"]) == 2 ? "Hipotecaria" : Convert.ToInt32(resultado["TIPO_GARANTIA"]) == 3 ? "Prendaria" : "Hipotecaria";
                            }

                            if (resultado["VALOR_GARANTIA"] != DBNull.Value) entidad.Valor = Convert.ToInt64(resultado["VALOR_GARANTIA"]);
                            if (resultado["UBICACION"] != DBNull.Value) entidad.Ubicacion = Convert.ToString(resultado["UBICACION"]);
                            if (resultado["FECHA_LIBERACION"] != DBNull.Value) entidad.FechaLiberacion = Convert.ToDateTime(resultado["FECHA_LIBERACION"]);
                            if (resultado["ENCARGADO"] != DBNull.Value) entidad.Encargado = Convert.ToString(resultado["ENCARGADO"]);
                            if (resultado["FECHA_AVALUO"] != DBNull.Value) entidad.FechaAvaluo = Convert.ToDateTime(resultado["FECHA_AVALUO"]);
                            if (resultado["FECHA_VEN_SEG"] != DBNull.Value) entidad.FechaVencimiento = Convert.ToDateTime(resultado["FECHA_VEN_SEG"]);

                            if (resultado["ESTADO"] != DBNull.Value)
                            {
                                string estado = Convert.ToString(resultado["ESTADO"]);

                                if (estado == "1")
                                {
                                    estado = "Activa";
                                }
                                else if (estado == "2")
                                {
                                    estado = "Terminado";
                                }
                                else
                                {
                                    estado = "Anulada";
                                }

                                entidad.Estado = estado;
                            }

                            lstGarantia.Add(entidad);
                        }

                        return lstGarantia;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("GarantiaData", "ListarFullGarantias", ex);
                        return null;
                    }
                }
            }
        }

        public List<Garantia> ListarSinGarantias(string filtro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Garantia> lstGarantia = new List<Garantia>();
            string estadoFiltro = "('D','P','T','N')";

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select p.cod_persona, c.valor_cuota, c.numero_cuotas, c.monto_solicitado, c.numero_radicacion, p.identificacion, p.nombre as nombres, p.primer_apellido, p.segundo_apellido, c.cod_linea_credito, p.cod_oficina, 
                                        (select a.SNOMBRE1||' '||a.SNOMBRE2||' '||a.SAPELLIDO1||' '||a.SAPELLIDO2 from asejecutivos a where a.ICODIGO=c.cod_asesor_com) AS Nombre_Asesor, 
                                        (select ofi.nombre from oficina ofi where p.cod_oficina=ofi.cod_oficina) as oficina  
                                        From v_persona p Join credito c on c.cod_deudor = p.cod_persona 
                                        where p.cod_persona=c.cod_deudor and c.numero_radicacion not in (select numero_radicacion from garantias) and c.estado not in " + estadoFiltro + " " + filtro;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Garantia entidad = new Garantia();
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.NumeroRadicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nom_persona = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["Nombre_Asesor"] != DBNull.Value) entidad.nombre_asesor = Convert.ToString(resultado["Nombre_Asesor"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["MONTO_SOLICITADO"] != DBNull.Value) entidad.monto = Convert.ToInt64(resultado["MONTO_SOLICITADO"]); else entidad.monto = 0;
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.plazo = Convert.ToInt64(resultado["NUMERO_CUOTAS"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);

                            lstGarantia.Add(entidad);
                        }

                        return lstGarantia;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("GarantiaData", "ListarSinGarantias", ex);
                        return null;
                    }
                }
            }
        }


        public ActivoFijo CrearActivoFijoPersonal(ActivoFijo pActivoFijo, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_idactivo = cmdTransaccionFactory.CreateParameter();
                        p_idactivo.ParameterName = "p_idactivo";
                        p_idactivo.Value = 0;
                        p_idactivo.Direction = ParameterDirection.Output;
                        p_idactivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_idactivo);

                        DbParameter p_cod_persona = cmdTransaccionFactory.CreateParameter();
                        p_cod_persona.ParameterName = "p_cod_persona";
                        p_cod_persona.Value = pActivoFijo.cod_persona;
                        p_cod_persona.Direction = ParameterDirection.Input;
                        p_cod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_persona);

                        DbParameter p_cod_tipo_activo_per = cmdTransaccionFactory.CreateParameter();
                        p_cod_tipo_activo_per.ParameterName = "p_cod_tipo_activo_per";
                        p_cod_tipo_activo_per.Value = pActivoFijo.cod_tipo_activo_per;
                        p_cod_tipo_activo_per.Direction = ParameterDirection.Input;
                        p_cod_tipo_activo_per.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_tipo_activo_per);

                        DbParameter p_descripcion = cmdTransaccionFactory.CreateParameter();
                        p_descripcion.ParameterName = "p_descripcion";
                        if (pActivoFijo.descripcion == null || pActivoFijo.descripcion == "")
                            p_descripcion.Value = DBNull.Value;
                        else
                            p_descripcion.Value = pActivoFijo.descripcion;
                        p_descripcion.Direction = ParameterDirection.Input;
                        p_descripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_descripcion);

                        DbParameter p_fecha_adquisicion = cmdTransaccionFactory.CreateParameter();
                        p_fecha_adquisicion.ParameterName = "p_fecha_adquisicion";
                        if (pActivoFijo.fecha_compra == null)
                            p_fecha_adquisicion.Value = DBNull.Value;
                        else
                            p_fecha_adquisicion.Value = pActivoFijo.fecha_compra;
                        p_fecha_adquisicion.Direction = ParameterDirection.Input;
                        p_fecha_adquisicion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(p_fecha_adquisicion);

                        DbParameter p_valor_comercial = cmdTransaccionFactory.CreateParameter();
                        p_valor_comercial.ParameterName = "p_valor_comercial";
                        p_valor_comercial.Value = pActivoFijo.valor_compra;
                        p_valor_comercial.Direction = ParameterDirection.Input;
                        p_valor_comercial.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_valor_comercial);

                        DbParameter p_valor_comprometido = cmdTransaccionFactory.CreateParameter();
                        p_valor_comprometido.ParameterName = "p_valor_comprometido";
                        if (pActivoFijo.valor_comprometido == null)
                            p_valor_comprometido.Value = DBNull.Value;
                        else
                            p_valor_comprometido.Value = pActivoFijo.valor_comprometido;
                        p_valor_comprometido.Direction = ParameterDirection.Input;
                        p_valor_comprometido.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_valor_comprometido);

                        DbParameter p_estado = cmdTransaccionFactory.CreateParameter();
                        p_estado.ParameterName = "p_estado";
                        p_estado.Value = pActivoFijo.estado;
                        p_estado.Direction = ParameterDirection.Input;
                        p_estado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_estado);

                        DbParameter p_direccion = cmdTransaccionFactory.CreateParameter();
                        p_direccion.ParameterName = "p_direccion";
                        if (pActivoFijo.direccion == null)
                            p_direccion.Value = DBNull.Value;
                        else
                            p_direccion.Value = pActivoFijo.direccion;
                        p_direccion.Direction = ParameterDirection.Input;
                        p_direccion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_direccion);

                        DbParameter p_localizacion = cmdTransaccionFactory.CreateParameter();
                        p_localizacion.ParameterName = "p_localizacion";
                        if (pActivoFijo.localizacion == null)
                            p_localizacion.Value = DBNull.Value;
                        else
                            p_localizacion.Value = pActivoFijo.localizacion;
                        p_localizacion.Direction = ParameterDirection.Input;
                        p_localizacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_localizacion);

                        DbParameter p_matricula = cmdTransaccionFactory.CreateParameter();
                        p_matricula.ParameterName = "p_matricula";
                        if (pActivoFijo.matricula == null)
                            p_matricula.Value = DBNull.Value;
                        else
                            p_matricula.Value = pActivoFijo.matricula;
                        p_matricula.Direction = ParameterDirection.Input;
                        p_matricula.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_matricula);

                        DbParameter p_escritura = cmdTransaccionFactory.CreateParameter();
                        p_escritura.ParameterName = "p_escritura";
                        if (pActivoFijo.escritura == null)
                            p_escritura.Value = DBNull.Value;
                        else
                            p_escritura.Value = pActivoFijo.escritura;
                        p_escritura.Direction = ParameterDirection.Input;
                        p_escritura.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_escritura);

                        DbParameter p_notaria = cmdTransaccionFactory.CreateParameter();
                        p_notaria.ParameterName = "p_notaria";
                        if (pActivoFijo.notaria == null)
                            p_notaria.Value = DBNull.Value;
                        else
                            p_notaria.Value = pActivoFijo.notaria;
                        p_notaria.Direction = ParameterDirection.Input;
                        p_notaria.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_notaria);

                        DbParameter p_marca = cmdTransaccionFactory.CreateParameter();
                        p_marca.ParameterName = "p_marca";
                        if (pActivoFijo.marca == null)
                            p_marca.Value = DBNull.Value;
                        else
                            p_marca.Value = pActivoFijo.marca;
                        p_marca.Direction = ParameterDirection.Input;
                        p_marca.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_marca);

                        DbParameter p_referencia = cmdTransaccionFactory.CreateParameter();
                        p_referencia.ParameterName = "p_referencia";
                        if (pActivoFijo.referencia == null)
                            p_referencia.Value = DBNull.Value;
                        else
                            p_referencia.Value = pActivoFijo.referencia;
                        p_referencia.Direction = ParameterDirection.Input;
                        p_referencia.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_referencia);

                        DbParameter p_modelo = cmdTransaccionFactory.CreateParameter();
                        p_modelo.ParameterName = "p_modelo";
                        if (pActivoFijo.modelo == null)
                            p_modelo.Value = DBNull.Value;
                        else
                            p_modelo.Value = pActivoFijo.modelo;
                        p_modelo.Direction = ParameterDirection.Input;
                        p_modelo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_modelo);

                        DbParameter p_uso = cmdTransaccionFactory.CreateParameter();
                        p_uso.ParameterName = "p_uso";
                        if (pActivoFijo.cod_uso == null)
                            p_uso.Value = DBNull.Value;
                        else
                            p_uso.Value = pActivoFijo.cod_uso;
                        p_uso.Direction = ParameterDirection.Input;
                        p_uso.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_uso);

                        DbParameter p_capacidad = cmdTransaccionFactory.CreateParameter();
                        p_capacidad.ParameterName = "p_capacidad";
                        if (pActivoFijo.capacidad == null)
                            p_capacidad.Value = DBNull.Value;
                        else
                            p_capacidad.Value = pActivoFijo.capacidad;
                        p_capacidad.Direction = ParameterDirection.Input;
                        p_capacidad.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_capacidad);

                        DbParameter p_numero_chasis = cmdTransaccionFactory.CreateParameter();
                        p_numero_chasis.ParameterName = "p_numero_chasis";
                        if (pActivoFijo.num_chasis == null)
                            p_numero_chasis.Value = DBNull.Value;
                        else
                            p_numero_chasis.Value = pActivoFijo.num_chasis;
                        p_numero_chasis.Direction = ParameterDirection.Input;
                        p_numero_chasis.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_numero_chasis);

                        DbParameter p_serie_motor = cmdTransaccionFactory.CreateParameter();
                        p_serie_motor.ParameterName = "p_serie_motor";
                        if (pActivoFijo.num_motor == null)
                            p_serie_motor.Value = DBNull.Value;
                        else
                            p_serie_motor.Value = pActivoFijo.num_motor;
                        p_serie_motor.Direction = ParameterDirection.Input;
                        p_serie_motor.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_serie_motor);

                        DbParameter p_placa = cmdTransaccionFactory.CreateParameter();
                        p_placa.ParameterName = "p_placa";
                        if (pActivoFijo.placa == null)
                            p_placa.Value = DBNull.Value;
                        else
                            p_placa.Value = pActivoFijo.placa;
                        p_placa.Direction = ParameterDirection.Input;
                        p_placa.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_placa);

                        DbParameter p_color = cmdTransaccionFactory.CreateParameter();
                        p_color.ParameterName = "p_color";
                        if (pActivoFijo.color == null)
                            p_color.Value = DBNull.Value;
                        else
                            p_color.Value = pActivoFijo.color;
                        p_color.Direction = ParameterDirection.Input;
                        p_color.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_color);

                        DbParameter p_docimportacion = cmdTransaccionFactory.CreateParameter();
                        p_docimportacion.ParameterName = "p_docimportacion";
                        if (pActivoFijo.documentos_importacion == null)
                            p_docimportacion.Value = DBNull.Value;
                        else
                            p_docimportacion.Value = pActivoFijo.documentos_importacion;
                        p_docimportacion.Direction = ParameterDirection.Input;
                        p_docimportacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_docimportacion);

                        DbParameter FIMPORTACION = cmdTransaccionFactory.CreateParameter();
                        FIMPORTACION.ParameterName = "p_fimportacion";
                        if (pActivoFijo.fecha_importacion == null)
                            FIMPORTACION.Value = DBNull.Value;
                        else
                            FIMPORTACION.Value = pActivoFijo.fecha_importacion;
                        FIMPORTACION.Direction = ParameterDirection.Input;
                        FIMPORTACION.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(FIMPORTACION);

                        DbParameter RANGOVIVIENDA = cmdTransaccionFactory.CreateParameter();
                        RANGOVIVIENDA.ParameterName = "p_rangovivienda";
                        if (pActivoFijo.rango_vivienda == null)
                            RANGOVIVIENDA.Value = DBNull.Value;
                        else
                            RANGOVIVIENDA.Value = pActivoFijo.rango_vivienda;
                        RANGOVIVIENDA.Direction = ParameterDirection.Input;
                        RANGOVIVIENDA.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(RANGOVIVIENDA);

                        DbParameter TIPOVIVIENDA = cmdTransaccionFactory.CreateParameter();
                        TIPOVIVIENDA.ParameterName = "p_tipovivienda";
                        if (pActivoFijo.tipo_vivienda == null)
                            TIPOVIVIENDA.Value = DBNull.Value;
                        else
                            TIPOVIVIENDA.Value = pActivoFijo.tipo_vivienda;
                        TIPOVIVIENDA.Direction = ParameterDirection.Input;
                        TIPOVIVIENDA.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(TIPOVIVIENDA);

                        DbParameter SENALVIS = cmdTransaccionFactory.CreateParameter();
                        SENALVIS.ParameterName = "p_senalvis";
                        if (pActivoFijo.tipo_vivienda == null)
                            SENALVIS.Value = DBNull.Value;
                        else
                            SENALVIS.Value = pActivoFijo.SENALVIS;
                        SENALVIS.Direction = ParameterDirection.Input;
                        SENALVIS.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(SENALVIS);


                        DbParameter p_entidad_redescuento = cmdTransaccionFactory.CreateParameter();
                        p_entidad_redescuento.ParameterName = "p_entidad_redescuento";
                        if (pActivoFijo.entidad_redescuento == null)
                            p_entidad_redescuento.Value = DBNull.Value;
                        else
                            p_entidad_redescuento.Value = pActivoFijo.entidad_redescuento;
                        p_entidad_redescuento.Direction = ParameterDirection.Input;
                        p_entidad_redescuento.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_entidad_redescuento);

                        DbParameter p_margen_redescuento = cmdTransaccionFactory.CreateParameter();
                        p_margen_redescuento.ParameterName = "p_margen_redescuento";
                        if (pActivoFijo.margen_redescuento == null)
                            p_margen_redescuento.Value = DBNull.Value;
                        else
                            p_margen_redescuento.Value = pActivoFijo.margen_redescuento;
                        p_margen_redescuento.Direction = ParameterDirection.Input;
                        p_margen_redescuento.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_margen_redescuento);

                        DbParameter p_desembolso_directo = cmdTransaccionFactory.CreateParameter();
                        p_desembolso_directo.ParameterName = "p_desembolso_directo";
                        if (pActivoFijo.desembolso_directo == null)
                            p_desembolso_directo.Value = DBNull.Value;
                        else
                            p_desembolso_directo.Value = pActivoFijo.desembolso_directo;
                        p_desembolso_directo.Direction = ParameterDirection.Input;
                        p_desembolso_directo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_desembolso_directo);

                        DbParameter p_desembolso = cmdTransaccionFactory.CreateParameter();
                        p_desembolso.ParameterName = "p_desembolso";
                        if (pActivoFijo.desembolso == null)
                            p_desembolso.Value = DBNull.Value;
                        else
                            p_desembolso.Value = pActivoFijo.desembolso;
                        p_desembolso.Direction = ParameterDirection.Input;
                        p_desembolso.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_desembolso);

                        DbParameter p_hipoteca = cmdTransaccionFactory.CreateParameter();
                        p_hipoteca.ParameterName = "p_hipoteca";
                        if (pActivoFijo.hipoteca == null)
                            p_hipoteca.Value = DBNull.Value;
                        else
                            p_hipoteca.Value = pActivoFijo.hipoteca;
                        p_hipoteca.Direction = ParameterDirection.Input;
                        p_hipoteca.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_hipoteca);

                        DbParameter p_porcentaje_pignorado = cmdTransaccionFactory.CreateParameter();
                        p_porcentaje_pignorado.ParameterName = "p_porcentaje_pignorado";
                        if (pActivoFijo.porcentaje_pignorado == null)
                            p_porcentaje_pignorado.Value = DBNull.Value;
                        else
                            p_porcentaje_pignorado.Value = pActivoFijo.porcentaje_pignorado;
                        p_porcentaje_pignorado.Direction = ParameterDirection.Input;
                        p_porcentaje_pignorado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_porcentaje_pignorado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_ACT_ACTIVOS_PE_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pActivoFijo.cod_act = Convert.ToInt64(p_idactivo.Value);

                        return pActivoFijo;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActivoFijoData", "CrearActivoFijoPersonal", ex);
                        return null;
                    }
                }
            }
        }


        public bool EliminarActivoFijo(Int64 pId, Int64 pNum_Radicacion, ref string pError, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidactivo = cmdTransaccionFactory.CreateParameter();
                        pidactivo.ParameterName = "p_idactivo";
                        pidactivo.Value = pId;
                        pidactivo.Direction = ParameterDirection.Input;
                        pidactivo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidactivo);

                        DbParameter pnumero_radicacion = cmdTransaccionFactory.CreateParameter();
                        pnumero_radicacion.ParameterName = "p_numero_radicacion";
                        pnumero_radicacion.Value = pNum_Radicacion;
                        pnumero_radicacion.Direction = ParameterDirection.Input;
                        pnumero_radicacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_radicacion);

                        DbParameter perror = cmdTransaccionFactory.CreateParameter();
                        perror.ParameterName = "p_error";
                        perror.Value = pError;
                        perror.Direction = ParameterDirection.Output;
                        perror.DbType = DbType.String;
                        perror.Size = 200;
                        cmdTransaccionFactory.Parameters.Add(perror);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_ACT_ACTIVOS_PE_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pError = perror.Value.ToString();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActivoFijoData", "EliminarActivoFijo", ex);
                        return false;
                    }
                }
            }
        }


        /// <summary>
        /// Crea una entidad Garantia en la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad Garantia</param>
        /// <returns>Entidad creada</returns>
        public Garantia InsertarGarantia(Garantia pEntidad, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pnum_radicacion = cmdTransaccionFactory.CreateParameter();
                        pnum_radicacion.ParameterName = "pnumeroradicacion";
                        pnum_radicacion.Value = pEntidad.NumeroRadicacion;
                        pnum_radicacion.DbType = DbType.Int64;
                        pnum_radicacion.Direction = ParameterDirection.Input;

                        DbParameter pid_activo = cmdTransaccionFactory.CreateParameter();
                        pid_activo.ParameterName = "pidactivo";
                        pid_activo.Value = pEntidad.IdActivo;
                        pid_activo.DbType = DbType.Int64;
                        pid_activo.Direction = ParameterDirection.Input;

                        DbParameter pfecha_garantia = cmdTransaccionFactory.CreateParameter();
                        pfecha_garantia.ParameterName = "pfechagarantia";
                        pfecha_garantia.Value = pEntidad.FechaGarantia;
                        pfecha_garantia.DbType = DbType.DateTime;
                        pfecha_garantia.Direction = ParameterDirection.Input;

                        DbParameter pval_garantia = cmdTransaccionFactory.CreateParameter();
                        pval_garantia.ParameterName = "pvalorgarantia";
                        pval_garantia.Value = pEntidad.valor_garantia;
                        pval_garantia.DbType = DbType.Decimal;
                        pval_garantia.Direction = ParameterDirection.Input;

                        DbParameter ptipo_garantia = cmdTransaccionFactory.CreateParameter();
                        ptipo_garantia.ParameterName = "ptipogarantia";
                        ptipo_garantia.Value = pEntidad.tipo_garantia;
                        ptipo_garantia.DbType = DbType.Int64;
                        ptipo_garantia.Direction = ParameterDirection.Input;

                        DbParameter pfecha_avaluo = cmdTransaccionFactory.CreateParameter();
                        pfecha_avaluo.ParameterName = "pfechaavaluo";

                        if (pEntidad.FechaAvaluo == DateTime.MinValue)
                        {
                            pfecha_avaluo.Value = DBNull.Value;
                        }
                        else
                        {
                            pfecha_avaluo.Value = pEntidad.FechaAvaluo;
                        }

                        pfecha_avaluo.DbType = DbType.DateTime;
                        pfecha_avaluo.Direction = ParameterDirection.Input;

                        DbParameter pval_avaluo = cmdTransaccionFactory.CreateParameter();
                        pval_avaluo.ParameterName = "pvaloravaluo";
                        pval_avaluo.Value = pEntidad.valor_avaluo;
                        pval_avaluo.DbType = DbType.Decimal;
                        pval_avaluo.Direction = ParameterDirection.Input;

                        DbParameter pval_seguro = cmdTransaccionFactory.CreateParameter();
                        pval_seguro.ParameterName = "pvalorseguro";
                        pval_seguro.Value = 0;
                        pval_seguro.DbType = DbType.Decimal;
                        pval_seguro.Direction = ParameterDirection.Input;

                        DbParameter pfecha_ven_seg = cmdTransaccionFactory.CreateParameter();
                        pfecha_ven_seg.ParameterName = "pfechavenseg";

                        if (pEntidad.FechaVencimiento == DateTime.MinValue)
                        {
                            pfecha_ven_seg.Value = DBNull.Value;
                        }
                        else
                        {
                            pfecha_ven_seg.Value = pEntidad.FechaVencimiento;
                        }

                        pfecha_ven_seg.DbType = DbType.DateTime;
                        pfecha_ven_seg.Direction = ParameterDirection.Input;

                        DbParameter p_seguradora = cmdTransaccionFactory.CreateParameter();
                        p_seguradora.ParameterName = "paseguradora";
                        p_seguradora.Value = pEntidad.Aseguradora;
                        p_seguradora.Size = 200;
                        p_seguradora.DbType = DbType.AnsiString;
                        p_seguradora.Direction = ParameterDirection.Input;

                        DbParameter pnum_poliza = cmdTransaccionFactory.CreateParameter();
                        pnum_poliza.ParameterName = "pnumeropoliza";
                        pnum_poliza.Value = DBNull.Value;
                        pnum_poliza.DbType = DbType.AnsiString;
                        pnum_poliza.Direction = ParameterDirection.Input;

                        DbParameter pval_poliza = cmdTransaccionFactory.CreateParameter();
                        pval_poliza.ParameterName = "pvalorpoliza";
                        pval_poliza.Value = 0;
                        pval_poliza.DbType = DbType.Decimal;
                        pval_poliza.Direction = ParameterDirection.Input;

                        DbParameter pfecha_liberacion = cmdTransaccionFactory.CreateParameter();
                        pfecha_liberacion.ParameterName = "pfechaliberacion";

                        if (pEntidad.FechaLiberacion == DateTime.MinValue)
                        {
                            pfecha_liberacion.Value = DBNull.Value;
                        }
                        else
                        {
                            pfecha_liberacion.Value = pEntidad.FechaLiberacion;
                        }

                        pfecha_liberacion.DbType = DbType.DateTime;
                        pfecha_liberacion.Direction = ParameterDirection.Input;

                        DbParameter p_ubicacion = cmdTransaccionFactory.CreateParameter();
                        p_ubicacion.ParameterName = "pubicacion";
                        p_ubicacion.Value = pEntidad.Ubicacion;
                        p_ubicacion.DbType = DbType.AnsiString;
                        p_ubicacion.Direction = ParameterDirection.Input;

                        DbParameter p_encargado = cmdTransaccionFactory.CreateParameter();
                        p_encargado.ParameterName = "pencargado";
                        p_encargado.Value = pEntidad.Encargado;
                        p_encargado.DbType = DbType.AnsiString;
                        p_encargado.Direction = ParameterDirection.Input;

                        DbParameter p_estado = cmdTransaccionFactory.CreateParameter();
                        p_estado.ParameterName = "pestado";
                        p_estado.Value = pEntidad.Estado;
                        p_estado.DbType = DbType.AnsiString;
                        p_estado.Direction = ParameterDirection.Input;

                        DbParameter p_cod_ope = cmdTransaccionFactory.CreateParameter();
                        p_cod_ope.ParameterName = "pcod_ope";
                        p_cod_ope.Value = pEntidad.cod_ope;
                        p_cod_ope.DbType = DbType.Int64;
                        p_cod_ope.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(pnum_radicacion);
                        cmdTransaccionFactory.Parameters.Add(pid_activo);
                        cmdTransaccionFactory.Parameters.Add(pfecha_garantia);
                        cmdTransaccionFactory.Parameters.Add(pval_garantia);
                        cmdTransaccionFactory.Parameters.Add(ptipo_garantia);
                        cmdTransaccionFactory.Parameters.Add(pfecha_avaluo);
                        cmdTransaccionFactory.Parameters.Add(pval_avaluo);
                        cmdTransaccionFactory.Parameters.Add(pval_seguro);
                        cmdTransaccionFactory.Parameters.Add(pfecha_ven_seg);
                        cmdTransaccionFactory.Parameters.Add(p_seguradora);
                        cmdTransaccionFactory.Parameters.Add(pnum_poliza);
                        cmdTransaccionFactory.Parameters.Add(pval_poliza);
                        cmdTransaccionFactory.Parameters.Add(pfecha_liberacion);
                        cmdTransaccionFactory.Parameters.Add(p_ubicacion);
                        cmdTransaccionFactory.Parameters.Add(p_encargado);
                        cmdTransaccionFactory.Parameters.Add(p_estado);
                        cmdTransaccionFactory.Parameters.Add(p_cod_ope);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_GARANTIA_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pEntidad, pUsuario, Convert.ToInt64(pEntidad..ToString()), "REINTEGRO", Accion.Crear.ToString(), connection, cmdTransaccionFactory); //REGISTRO DE AUDITORIA

                        return pEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("GarantiaData", "InsertarReintegro", ex);
                        return null;
                    }

                }
            }
        }


        /// <summary>
        /// Crea una entidad Garantia en la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad Garantia</param>
        /// <returns>Entidad creada</returns>
        public Garantia GenerarCompModificarGarantia(Int16 origen,Garantia pEntidad, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter p_origen = cmdTransaccionFactory.CreateParameter();
                        p_origen.ParameterName = "p_origen";
                        p_origen.Value = pEntidad.origen;
                        p_origen.DbType = DbType.Int16;
                        p_origen.Direction = ParameterDirection.Input;
                        p_origen.Size = 1;


                        DbParameter pid_garantia = cmdTransaccionFactory.CreateParameter();
                        pid_garantia.ParameterName = "pidgarantia";
                        pid_garantia.Value = pEntidad.IdGarantia;
                        pid_garantia.DbType = DbType.Int64;
                        pid_garantia.Direction = ParameterDirection.Input;
                        pid_garantia.Size = 8;

                        DbParameter pnum_radicacion = cmdTransaccionFactory.CreateParameter();
                        pnum_radicacion.ParameterName = "pnumeroradicacion";
                        pnum_radicacion.Value = pEntidad.NumeroRadicacion;
                        pnum_radicacion.DbType = DbType.Int64;
                        pnum_radicacion.Direction = ParameterDirection.Input;

                        DbParameter pid_activo = cmdTransaccionFactory.CreateParameter();
                        pid_activo.ParameterName = "pidactivo";
                        pid_activo.Value = pEntidad.IdActivo;
                        pid_activo.DbType = DbType.Int64;
                        pid_activo.Direction = ParameterDirection.Input;
                        pid_activo.Size = 8;

                        DbParameter pfecha_garantia = cmdTransaccionFactory.CreateParameter();
                        pfecha_garantia.ParameterName = "pfechagarantia";
                        pfecha_garantia.Value = pEntidad.FechaGarantia;
                        pfecha_garantia.DbType = DbType.DateTime;
                        pfecha_garantia.Direction = ParameterDirection.Input;

                        DbParameter pval_garantia = cmdTransaccionFactory.CreateParameter();
                        pval_garantia.ParameterName = "pvalorgarantia";
                        pval_garantia.Value = pEntidad.valor_garantia;
                        pval_garantia.DbType = DbType.Decimal;
                        pval_garantia.Direction = ParameterDirection.Input;

                        DbParameter ptipo_garantia = cmdTransaccionFactory.CreateParameter();
                        ptipo_garantia.ParameterName = "ptipogarantia";
                        ptipo_garantia.Value = pEntidad.tipo_garantia;
                        ptipo_garantia.Size = 8;
                        ptipo_garantia.DbType = DbType.Int64;
                        ptipo_garantia.Direction = ParameterDirection.Input;

                        DbParameter pfecha_avaluo = cmdTransaccionFactory.CreateParameter();
                        pfecha_avaluo.ParameterName = "pfechaavaluo";
                        pfecha_avaluo.Value = pEntidad.FechaAvaluo;
                        pfecha_avaluo.DbType = DbType.DateTime;
                        pfecha_avaluo.Direction = ParameterDirection.Input;

                        DbParameter pval_avaluo = cmdTransaccionFactory.CreateParameter();
                        pval_avaluo.ParameterName = "pvaloravaluo";
                        pval_avaluo.Value = pEntidad.valor_avaluo;
                        pval_avaluo.DbType = DbType.Decimal;
                        pval_avaluo.Direction = ParameterDirection.Input;

                        DbParameter pval_seguro = cmdTransaccionFactory.CreateParameter();
                        pval_seguro.ParameterName = "pvalorseguro";
                        pval_seguro.Value = 0;
                        pval_seguro.DbType = DbType.Decimal;
                        pval_seguro.Direction = ParameterDirection.Input;

                        DbParameter pfecha_ven_seg = cmdTransaccionFactory.CreateParameter();
                        pfecha_ven_seg.ParameterName = "pfechavenseg";
                        pfecha_ven_seg.Value = pEntidad.FechaVencimiento;
                        pfecha_ven_seg.DbType = DbType.DateTime;
                        pfecha_ven_seg.Direction = ParameterDirection.Input;

                        DbParameter p_seguradora = cmdTransaccionFactory.CreateParameter();
                        p_seguradora.ParameterName = "paseguradora";
                        p_seguradora.Value = pEntidad.Aseguradora;
                        p_seguradora.Size = 200;
                        p_seguradora.DbType = DbType.String;
                        p_seguradora.Direction = ParameterDirection.Input;

                        DbParameter pnum_poliza = cmdTransaccionFactory.CreateParameter();
                        pnum_poliza.ParameterName = "pnumeropoliza";
                        pnum_poliza.Value = "";
                        pnum_poliza.DbType = DbType.String;
                        pnum_poliza.Direction = ParameterDirection.Input;

                        DbParameter pval_poliza = cmdTransaccionFactory.CreateParameter();
                        pval_poliza.ParameterName = "pvalorpoliza";
                        pval_poliza.Value = 0;
                        pval_poliza.DbType = DbType.Decimal;
                        pval_poliza.Direction = ParameterDirection.Input;

                        DbParameter pfecha_liberacion = cmdTransaccionFactory.CreateParameter();
                        pfecha_liberacion.ParameterName = "pfechaliberacion";
                        pfecha_liberacion.Value = pEntidad.FechaLiberacion;
                        pfecha_liberacion.DbType = DbType.DateTime;
                        pfecha_liberacion.Direction = ParameterDirection.Input;

                        DbParameter p_ubicacion = cmdTransaccionFactory.CreateParameter();
                        p_ubicacion.ParameterName = "pubicacion";
                        p_ubicacion.Value = pEntidad.Ubicacion;
                        p_ubicacion.DbType = DbType.String;
                        p_ubicacion.Direction = ParameterDirection.Input;

                        DbParameter p_encargado = cmdTransaccionFactory.CreateParameter();
                        p_encargado.ParameterName = "pencargado";
                        p_encargado.Value = pEntidad.Encargado;
                        p_encargado.DbType = DbType.String;
                        p_encargado.Direction = ParameterDirection.Input;

                        DbParameter p_estado = cmdTransaccionFactory.CreateParameter();
                        p_estado.ParameterName = "pestado";
                        p_estado.Value = pEntidad.Estado;
                        p_estado.DbType = DbType.String;
                        p_estado.Direction = ParameterDirection.Input;

                        DbParameter p_cod_ope = cmdTransaccionFactory.CreateParameter();
                        p_cod_ope.ParameterName = "pcod_ope";
                        p_cod_ope.Value = pEntidad.cod_ope;
                        p_cod_ope.DbType = DbType.Int64;
                        p_cod_ope.Direction = ParameterDirection.Input;

                        DbParameter p_cod_persona = cmdTransaccionFactory.CreateParameter();
                        p_cod_persona.ParameterName = "PCOD_PERSONA";
                        p_cod_persona.Value = pEntidad.cod_persona;
                        p_cod_persona.DbType = DbType.Int64;
                        p_cod_persona.Direction = ParameterDirection.Input;

                        DbParameter p_cod_proceso = cmdTransaccionFactory.CreateParameter();
                        p_cod_proceso.ParameterName = "PCOD_PROCESO";
                        p_cod_proceso.Value = pEntidad.cod_proceso;
                        p_cod_proceso.DbType = DbType.Int64;

                        DbParameter plnum_comp = cmdTransaccionFactory.CreateParameter();
                        plnum_comp.ParameterName = "pnum_comp";
                        plnum_comp.Value = 0;
                        plnum_comp.Direction = ParameterDirection.Output;

                        DbParameter pltipo_comp = cmdTransaccionFactory.CreateParameter();
                        pltipo_comp.ParameterName = "ptipo_comp";
                        pltipo_comp.Value = 0;
                        pltipo_comp.Direction = ParameterDirection.Output;

                        cmdTransaccionFactory.Parameters.Add(pid_garantia);
                        cmdTransaccionFactory.Parameters.Add(pnum_radicacion);
                        cmdTransaccionFactory.Parameters.Add(pid_activo);
                        cmdTransaccionFactory.Parameters.Add(pfecha_garantia);
                        cmdTransaccionFactory.Parameters.Add(pval_garantia);
                        cmdTransaccionFactory.Parameters.Add(ptipo_garantia);
                        cmdTransaccionFactory.Parameters.Add(pfecha_avaluo);
                        cmdTransaccionFactory.Parameters.Add(pval_avaluo);
                        cmdTransaccionFactory.Parameters.Add(pval_seguro);
                        cmdTransaccionFactory.Parameters.Add(pfecha_ven_seg);
                        cmdTransaccionFactory.Parameters.Add(p_seguradora);
                        cmdTransaccionFactory.Parameters.Add(pnum_poliza);
                        cmdTransaccionFactory.Parameters.Add(pval_poliza);
                        cmdTransaccionFactory.Parameters.Add(pfecha_liberacion);
                        cmdTransaccionFactory.Parameters.Add(p_ubicacion);
                        cmdTransaccionFactory.Parameters.Add(p_encargado);
                        cmdTransaccionFactory.Parameters.Add(p_estado);
                        cmdTransaccionFactory.Parameters.Add(p_cod_ope);
                        cmdTransaccionFactory.Parameters.Add(p_cod_persona);
                        cmdTransaccionFactory.Parameters.Add(p_cod_proceso);
                        cmdTransaccionFactory.Parameters.Add(plnum_comp);
                        cmdTransaccionFactory.Parameters.Add(pltipo_comp);
                        cmdTransaccionFactory.Parameters.Add(p_origen);

                        

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_NOTAGARANTIA";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (plnum_comp.Value != DBNull.Value) pEntidad.num_comp = Convert.ToInt64(plnum_comp.Value);
                        if (pltipo_comp.Value != DBNull.Value) pEntidad.tipo_comp = Convert.ToInt64(pltipo_comp.Value);

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pEntidad, pUsuario, Convert.ToInt64(pEntidad..ToString()), "REINTEGRO", Accion.Crear.ToString(), connection, cmdTransaccionFactory); //REGISTRO DE AUDITORIA

                        return pEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("GarantiaData", "GenerarCompModificarGarantia", ex);
                        return null;
                    }

                }
            }
        }


        /// <summary>
        /// Obtiene un registro de la tabla Garantia de la base de datos
        /// </summary>
        /// <param name="pId">identificador del registro</param>
        /// <returns>Garantia consultada</returns>
        public Garantia ConsultarGarantia(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Garantia entidad = new Garantia();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT IDGARANTIA, idactivo,fecha_garantia,valor_garantia,tipo_garantia,fecha_avaluo,valor_avaluo,valor_seguro,fecha_ven_seg,
                                        aseguradora,valor_poliza,fecha_liberacion,ubicacion,encargado,estado, cod_ope
                                        FROM GARANTIAS g 
                                        where NUMERO_RADICACION = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["IDGARANTIA"] != DBNull.Value) entidad.IdGarantia = Convert.ToInt64(resultado["IDGARANTIA"]);
                            if (resultado["idactivo"] != DBNull.Value) entidad.IdActivo = Convert.ToInt64(resultado["idactivo"]);
                            if (resultado["fecha_garantia"] != DBNull.Value) entidad.FechaGarantia = Convert.ToDateTime(resultado["fecha_garantia"]);
                            if (resultado["valor_garantia"] != DBNull.Value) entidad.valor_garantia = Convert.ToInt64(resultado["valor_garantia"]);
                            if (resultado["tipo_garantia"] != DBNull.Value) entidad.tipo_garantia = Convert.ToInt64(resultado["tipo_garantia"]);
                            if (resultado["fecha_avaluo"] != DBNull.Value) entidad.FechaAvaluo = Convert.ToDateTime(resultado["fecha_avaluo"]);
                            if (resultado["valor_avaluo"] != DBNull.Value) entidad.valor_avaluo = Convert.ToInt64(resultado["valor_avaluo"]);
                            if (resultado["valor_seguro"] != DBNull.Value) entidad.valor_seguro = Convert.ToInt64(resultado["valor_seguro"]);
                            if (resultado["fecha_ven_seg"] != DBNull.Value) entidad.FechaVencimiento = Convert.ToDateTime(resultado["fecha_ven_seg"]);
                            if (resultado["aseguradora"] != DBNull.Value) entidad.Aseguradora = Convert.ToString(resultado["aseguradora"]);
                            if (resultado["valor_poliza"] != DBNull.Value) entidad.Valor = Convert.ToInt64(resultado["valor_poliza"]);
                            if (resultado["fecha_liberacion"] != DBNull.Value) entidad.FechaLiberacion = Convert.ToDateTime(resultado["fecha_liberacion"]);
                            if (resultado["ubicacion"] != DBNull.Value) entidad.Ubicacion = Convert.ToString(resultado["ubicacion"]);
                            if (resultado["encargado"] != DBNull.Value) entidad.Encargado = Convert.ToString(resultado["encargado"]);
                            if (resultado["estado"] != DBNull.Value) entidad.Estado = Convert.ToString(resultado["estado"]);
                            if (resultado["COD_OPE"] != DBNull.Value) entidad.cod_ope = Convert.ToInt64(resultado["COD_OPE"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("GarantiaData", "ConsultarGarantia", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Consultar los créditos para generar la garantía
        /// </summary>
        /// <param name="pId"></param>
        /// <param name="pnum"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public Garantia ConsultarCreditoCliente(Int64 pnum, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Garantia entidad = new Garantia();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT p.cod_persona, p.identificacion, c.monto_solicitado, c.cod_linea_credito, 
                                      (PRIMER_NOMBRE ||' '|| SEGUNDO_NOMBRE ||' '|| PRIMER_APELLIDO ||' '|| SEGUNDO_APELLIDO) AS NombreCompleto, 
                                      p.tipo_identificacion, 
                                      l.NOMBRE as Nombre_Linea
                                      from persona p 
                                      join credito c on c.COD_DEUDOR = p.COD_PERSONA
                                      join lineascredito l on l.COD_LINEA_CREDITO = c.COD_LINEA_CREDITO
                                      where c.numero_radicacion =" + pnum.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {

                            if (resultado["NombreCompleto"] != DBNull.Value) entidad.nom_persona = Convert.ToString(resultado["NombreCompleto"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.cod_ident = Convert.ToInt64(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["cod_persona"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["cod_persona"]);
                            if (resultado["MONTO_SOLICITADO"] != DBNull.Value) entidad.monto = Convert.ToInt64(resultado["MONTO_SOLICITADO"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea_cred = Convert.ToInt64(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["Nombre_Linea"] != DBNull.Value) entidad.nom_linea_cred = Convert.ToString(resultado["Nombre_Linea"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("GarantiaData", "ConsultarCreditoCliente", ex);
                        return null;
                    }

                }

            }
        }

        /// <summary>
        /// Listar activos fijos del cliente
        /// </summary>
        /// <param name="cod_persona"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public List<Garantia> Listaractivos(string cod_persona, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Garantia> lstGarantias = new List<Garantia>();


            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * from v_consulactivos where cod_persona  = " + cod_persona.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Garantia entidad = new Garantia();

                            if (resultado["IDACTIVO"] != DBNull.Value) entidad.IdActivo = Convert.ToInt64(resultado["IDACTIVO"]);
                            if (resultado["DESCR"] != DBNull.Value) entidad.Descripcion = Convert.ToString(resultado["DESCR"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion_activo = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["VALOR_COMERCIAL"] != DBNull.Value) entidad.valor_comercial = Convert.ToInt64(resultado["VALOR_COMERCIAL"]);
                            if (resultado["FECHA_ADQUISICION"] != DBNull.Value) entidad.Fecha_adquisicionactivo = Convert.ToDateTime(resultado["FECHA_ADQUISICION"]);
                            if (resultado["NOMESTADO"] != DBNull.Value) entidad.estado_descripcion = Convert.ToString(resultado["NOMESTADO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.Estado = Convert.ToString(resultado["ESTADO"]);

                            if (resultado["CLASE"] != DBNull.Value)
                            {
                                string clase = Convert.ToString(resultado["CLASE"]);

                                if (clase == "H")
                                {
                                    entidad.tipo_garantia = 2;
                                }
                                else if(clase == "P")
                                {
                                    entidad.tipo_garantia = 3;
                                }
                            }

                            lstGarantias.Add(entidad);
                        }

                        return lstGarantias;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("GarantiaData", "Listar", ex);
                        return null;
                    }

                }

            }
        }


    }
}