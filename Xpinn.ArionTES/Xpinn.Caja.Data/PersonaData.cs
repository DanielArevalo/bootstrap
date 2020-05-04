using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Caja.Entities;

namespace Xpinn.Caja.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla Persona
    /// </summary>
    public class PersonaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para Persona
        /// </summary>
        public PersonaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Obtiene la lista de Personas dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Personas obtenidas</returns>
        public List<Persona> ListarPersona(Persona pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Persona> lstPersona = new List<Persona>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = " SELECT persona.cod_persona, (PRIMER_NOMBRE ||' '|| SEGUNDO_NOMBRE ||' '|| PRIMER_APELLIDO ||' '|| SEGUNDO_APELLIDO) nom_persona FROM Persona " + ObtenerFiltro(pEntidad, "Persona.") + " order by persona.PRIMER_NOMBRE asc";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Persona entidad = new Persona();
                            //Asociar todos los valores a la entidad
                            if (resultado["cod_persona"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["cod_persona"]);
                            if (resultado["nom_persona"] != DBNull.Value) entidad.nom_persona = Convert.ToString(resultado["nom_persona"]);
                            lstPersona.Add(entidad);
                        }

                        return lstPersona;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PersonaData", "ListarPersona", ex);
                        return null;
                    }
                }
            }
        }



        /// <summary>
        /// Obtiene un registro de la tabla Persona de la base de datos
        /// </summary>
        /// <param name="pId">identificador del registro</param>
        /// <returns>Persona consultada</returns>
        public Persona ConsultarPersona(Persona pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Persona entidad = new Persona();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = " SELECT cod_persona,(PRIMER_NOMBRE ||' '|| SEGUNDO_NOMBRE ||' '|| PRIMER_APELLIDO ||' '|| SEGUNDO_APELLIDO) nom_persona,razon_social,TIPO_IDENTIFICACION, DIRECCION, TELEFONO FROM PERSONA where identificacion like '" + pEntidad.identificacion + "'";
                        if (pEntidad.tipo_identificacion > 0)
                            sql += "and tipo_identificacion=" + pEntidad.tipo_identificacion;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        entidad.mensajer_error = "";

                        if (resultado.Read())
                        {
                            //Asociar todos los valores a la entidad
                            if (resultado["cod_persona"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["cod_persona"]);
                            if (resultado["nom_persona"] != DBNull.Value) entidad.nom_persona = Convert.ToString(resultado["nom_persona"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToInt64(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["razon_social"] != DBNull.Value) entidad.razon_social = Convert.ToString(resultado["razon_social"]);
                        }
                        else
                        {
                            entidad.mensajer_error = "El registro no existe. Verifique por favor.";
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PersonaData", "ConsultarPersona", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene un registro de la tabla Persona de la base de datos
        /// </summary>
        /// <param name="pId">identificador del registro</param>
        /// <returns>Persona consultada por Codigo de Persona</returns>
        public Persona ConsultarPersonaXCodigo(Persona pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Persona entidad = new Persona();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select p.identificacion, p.tipo_identificacion, p.cod_persona, Case p.tipo_persona When 'N' Then EsNulo(p.PRIMER_NOMBRE, '') ||' '|| EsNulo(p.SEGUNDO_NOMBRE, '') ||' '|| EsNulo(p.PRIMER_APELLIDO, '') ||' '|| EsNulo(p.SEGUNDO_APELLIDO, '') Else p.razon_social End As nom_persona,     
                                        p.direccion, p.telefono, c.nomciudad As ciudad 
                                        From persona p Left Join ciudades c On c.codciudad = p.codciudadresidencia Where p.cod_persona = " + pEntidad.cod_persona;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            //Asociar todos los valores a la entidad
                            if (resultado["cod_persona"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["cod_persona"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["tipo_identificacion"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToInt64(resultado["tipo_identificacion"]);
                            if (resultado["nom_persona"] != DBNull.Value) entidad.nom_persona = Convert.ToString(resultado["nom_persona"]);
                            if (resultado["direccion"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["direccion"]);
                            if (resultado["telefono"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["telefono"]);
                            if (resultado["ciudad"] != DBNull.Value) entidad.ciudad = Convert.ToString(resultado["ciudad"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PersonaData", "ConsultarPersonaXCodigo", ex);
                        return null;
                    }
                }
            }
        }

        public Persona ConsultarPersonaXIdentificacion(string identificacion, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Persona entidad = new Persona();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT COD_PERSONA, NOMBREYAPELLIDO, RAZON_SOCIAL from v_persona where identificacion LIKE '%" + identificacion + "%'";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            //Asociar todos los valores a la entidad
                            if (resultado["cod_persona"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["cod_persona"]);
                            entidad.identificacion = identificacion;
                            if (resultado["NOMBREYAPELLIDO"] != DBNull.Value) entidad.nom_persona = Convert.ToString(resultado["NOMBREYAPELLIDO"]);
                            if (resultado["RAZON_SOCIAL"] != DBNull.Value) entidad.razon_social = Convert.ToString(resultado["RAZON_SOCIAL"]);
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PersonaData", "ConsultarPersonaXIdentificacion", ex);
                        return null;
                    }
                }
            }
        }



        /// <summary>
        /// Obtiene un registro de la tabla Persona de la base de datos
        /// </summary>
        /// <param name="pId">identificador del registro</param>
        /// <returns>Persona consultada</returns>
        public Persona ConsultarEmpresa(Persona pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Persona entidad = new Persona();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select cod_empresa,nit,nombre nom_empresa,direccion,telefono from empresa where cod_empresa=1";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            //Asociar todos los valores a la entidad
                            if (resultado["cod_empresa"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["cod_empresa"]);
                            if (resultado["nit"] != DBNull.Value) entidad.nit = Convert.ToString(resultado["nit"]);
                            if (resultado["nom_empresa"] != DBNull.Value) entidad.nom_empresa = Convert.ToString(resultado["nom_empresa"]);
                            if (resultado["direccion"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["direccion"]);
                            if (resultado["telefono"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["telefono"]);
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PersonaData", "ConsultarEmpresa", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene un registro de la tabla Persona de la base de datos
        /// </summary>
        /// <param name="pId">identificador del registro</param>
        /// <returns>Persona consultada</returns>
        public Persona ConsultarValorEfectivo(Persona pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Persona entidad = new Persona();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select sum(valor) totalefectivo from movimientocaja where cod_ope= " + pEntidad.cod_ope + " and cod_tipo_pago=1 and cod_moneda=1";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            //Asociar todos los valores a la entidad
                            if (resultado["totalefectivo"] != DBNull.Value) entidad.valor_total_efectivo = Convert.ToInt64(resultado["totalefectivo"]);
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PersonaData", "ConsultarValorEfectivo", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro de la tabla Persona de la base de datos
        /// </summary>
        /// <param name="pId">identificador del registro</param>
        /// <returns>Persona consultada</returns>
        public Persona ConsultarValorCheque(Persona pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Persona entidad = new Persona();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select sum(valor) totalcheque,num_documento from movimientocaja where cod_ope= " + pEntidad.cod_ope + " and cod_tipo_pago=2 and cod_moneda=1 group by num_documento";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            //Asociar todos los valores a la entidad
                            if (resultado["totalcheque"] != DBNull.Value) entidad.valor_total_cheques = Convert.ToInt64(resultado["totalcheque"]);
                            if (resultado["num_documento"] != DBNull.Value) entidad.referencia = resultado["num_documento"].ToString();
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PersonaData", "ConsultarValorCheque", ex);
                        return null;
                    }
                }
            }
        }

        /// <returns>Persona consultada</returns>
        public Persona ConsultarValorChequeCaja(Persona pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Persona entidad = new Persona();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select sum(valor) totalcheque from movimientocaja where cod_ope= " + pEntidad.cod_ope + " and cod_tipo_pago=2 and cod_moneda=1";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            //Asociar todos los valores a la entidad
                            if (resultado["totalcheque"] != DBNull.Value) entidad.valor_total_cheques = Convert.ToInt64(resultado["totalcheque"]);


                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PersonaData", "ConsultarValorChequeCaja", ex);
                        return null;
                    }
                }
            }
        }




        /// <summary>
        /// Obtiene un registro de la tabla Persona de la base de datos
        /// </summary>
        /// <param name="pId">identificador del registro</param>
        /// <returns>Persona consultada</returns>
        public Persona ConsultarValorOtros(Persona pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Persona entidad = new Persona();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select sum(valor) totalotros,num_documento  from movimientocaja where cod_ope= " + pEntidad.cod_ope + " and cod_tipo_pago not in(1,2) and cod_moneda=1 group by num_documento ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            //Asociar todos los valores a la entidad
                            if (resultado["totalotros"] != DBNull.Value) entidad.valor_total_otros = Convert.ToInt64(resultado["totalotros"]);
                            if (resultado["num_documento"] != DBNull.Value) entidad.referencia = resultado["num_documento"].ToString();
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PersonaData", "ConsultarValorOtros", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene la lista de Personas dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Personas obtenidas</returns>
        public List<Persona> ListarDatosCreditoPersona(Persona pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Persona> lstPersona = new List<Persona>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "";
                        if (pEntidad.tipo_linea.ToEnum<TipoDeProducto>() == TipoDeProducto.Credito)
                        {
                            pEntidad.tipo = "1";
                            string fecha = "";
                            if (pEntidad.fecha_pago == null)
                                pEntidad.fecha_pago = DateTime.Now;
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                fecha = "To_Date('" + Convert.ToDateTime(pEntidad.fecha_pago).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                fecha = " '" + Convert.ToDateTime(pEntidad.fecha_pago).ToString(conf.ObtenerFormatoFecha()) + "' ";
                            sql = @" SELECT b.cod_deudor As cod_persona, b.numero_radicacion, 2 As codtipoproducto, l.nombre linea, b.monto_aprobado, b.fecha_aprobacion, b.valor_cuota, b.saldo_capital, 
                                (Select Sum(cx.saldo) From cuenta_porcobrar_cre cx Where cx.numero_radicacion = b.numero_radicacion) As garantias_comunitarias, 
                                b.fecha_proximo_pago, Calcular_VrAPagar(b.numero_radicacion, " + fecha + @") As valor_a_pagar, Calcular_TotalAPagar(b.numero_radicacion, " + fecha + @") As total_a_pagar, FecDifDia(b.fecha_proximo_pago, " + fecha + @", 1) As dias_mora ,0 as PRINCIPAL,l.TIPO_LINEA
                                 ,CALCULAR_VR_CUOEXTRAS(b.numero_radicacion, " + fecha + @") As VALOR_CUOTAS_EXTRAS
                                FROM credito b Inner Join lineascredito l On b.cod_linea_credito = l.cod_linea_credito
                                WHERE b.cod_deudor = " + pEntidad.cod_persona + @" And b.estado = 'C'
                                ORDER BY b.numero_radicacion asc";
                        }
                        else
                        {

                            if (pEntidad.fecha_pago == null)
                                pEntidad.fecha_pago = DateTime.Now;
                            string fecha = "";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                fecha = "To_Date('" + Convert.ToDateTime(pEntidad.fecha_pago).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                fecha = " '" + Convert.ToDateTime(pEntidad.fecha_pago).ToString(conf.ObtenerFormatoFecha()) + "' ";



                            string FuncionCalcularCuota = "VALOR_CUOTA";
                            switch (pEntidad.tipo_linea.ToEnum<TipoDeProducto>())
                            {
                                case TipoDeProducto.Aporte: FuncionCalcularCuota = "CALCULAR_CUOTA_APORTE(b.NUMERO_RADICACION)"; break;
                                case TipoDeProducto.AhorroProgramado: FuncionCalcularCuota = "CALCULAR_CUOTA_PROGRAMADO(b.NUMERO_RADICACION)"; break;
                                case TipoDeProducto.AhorrosVista: FuncionCalcularCuota = "CALCULAR_CUOTA_AHORRO_VISTA(b.NUMERO_RADICACION)"; break;
                                default: FuncionCalcularCuota = "VALOR_CUOTA"; break;
                            }



                            string FuncionCalcularValoraPagar = "b.valor_a_pagar";
                            switch (pEntidad.tipo_linea.ToEnum<TipoDeProducto>())
                            {
                                case TipoDeProducto.Aporte: FuncionCalcularValoraPagar = "Cal_VrGrupAporte(b.NUMERO_RADICACION," + fecha + ")"; break;
                                case TipoDeProducto.Credito: FuncionCalcularValoraPagar = "Calcular_VrAPagar(b.NUMERO_RADICACION," + fecha + ")"; break;
                                case TipoDeProducto.Servicios: FuncionCalcularValoraPagar = "CALCULAR_TOTALAPAGAR_SERVICIO(b.NUMERO_RADICACION," + fecha + ")"; break;
                                default: FuncionCalcularCuota = "b.valor_a_pagar"; break;

                            }



                            string FuncionCalcularTotalValoraPagar = "b.total_a_pagar";
                            switch (pEntidad.tipo_linea.ToEnum<TipoDeProducto>())
                            {
                                case TipoDeProducto.Credito: FuncionCalcularTotalValoraPagar = "Calcular_TotalAPagar(b.NUMERO_RADICACION," + fecha + ")"; break;
                                case TipoDeProducto.Aporte: FuncionCalcularTotalValoraPagar = "Cal_VrGrupAporte(b.NUMERO_RADICACION," + fecha + ")"; break;
                                case TipoDeProducto.Servicios: FuncionCalcularTotalValoraPagar = "CALCULAR_TOTALAPAGAR_SERVICIO(b.NUMERO_RADICACION," + fecha + ")"; break;
                                default: FuncionCalcularCuota = "b.total_a_pagar"; break;
                            }


                            sql = @"SELECT a.cod_persona, b.numero_radicacion, b.codtipoproducto,b.nombre linea, b.monto_aprobado, b.fecha_aprobacion, " + FuncionCalcularCuota + @" As valor_cuota, b.saldo_capital, b.garantias_comunitarias, b.fecha_proximo_pago, " + FuncionCalcularValoraPagar + @" as valor_a_pagar," + FuncionCalcularTotalValoraPagar + @" as total_a_pagar, b.dias_mora, g.principal,b.TIPO_LINEA,b.VALOR_CUOTAS_EXTRAS
                                    FROM persona a, VCajaProductos b INNER JOIN GRUPO_LINEAAPORTE G ON G.COD_LINEA_APORTE = B.COD_LINEA_CREDITO         
                                    WHERE  b.codtipoproducto = " + pEntidad.tipo_linea + " and b.cod_deudor = " + pEntidad.cod_persona + @" and b.cod_deudor = a.cod_persona 
                                    UNION ALL
                                    SELECT a.cod_persona, b.numero_radicacion, b.codtipoproducto, b.nombre linea, b.monto_aprobado, b.fecha_aprobacion, " + FuncionCalcularCuota + @" As valor_cuota, b.saldo_capital, b.garantias_comunitarias, b.fecha_proximo_pago, " + FuncionCalcularValoraPagar + @" as valor_a_pagar, " + FuncionCalcularTotalValoraPagar + @" as total_a_pagar, b.dias_mora, 1,b.TIPO_LINEA,b.VALOR_CUOTAS_EXTRAS
                                    FROM persona a, VCajaProductos b
                                    WHERE  b.codtipoproducto = " + pEntidad.tipo_linea + " and b.cod_deudor = " + pEntidad.cod_persona + @" and b.cod_deudor = a.cod_persona  and b.cod_linea_credito Not In (Select g.cod_linea_aporte From Grupo_LineaAporte g)
                                    ORDER BY 2 asc";

                        }

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Persona entidad = new Persona();
                            //Asociar todos los valores a la entidad
                            if (resultado["cod_persona"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["cod_persona"]);
                            if (resultado["numero_radicacion"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["numero_radicacion"]);
                            if (resultado["linea"] != DBNull.Value) entidad.linea_credito = Convert.ToString(resultado["linea"]);
                            if (resultado["monto_aprobado"] != DBNull.Value) entidad.monto_aprobado = Convert.ToInt64(resultado["monto_aprobado"]);
                            if (resultado["valor_cuota"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["valor_cuota"]);
                            if (resultado["saldo_capital"] != DBNull.Value) entidad.saldo_capital = Convert.ToInt64(resultado["saldo_capital"]);
                            if (resultado["garantias_comunitarias"] != DBNull.Value) entidad.garantias_comunitarias = Convert.ToInt64(resultado["garantias_comunitarias"]);
                            if (resultado["fecha_proximo_pago"] != DBNull.Value) entidad.fecha_proxima_pago = Convert.ToDateTime(resultado["fecha_proximo_pago"]);
                            if (resultado["fecha_aprobacion"] != DBNull.Value) entidad.fecha_aprobacion = Convert.ToDateTime(resultado["fecha_aprobacion"]);
                            if (resultado["valor_a_pagar"] != DBNull.Value) entidad.valor_a_pagar = Convert.ToInt64(resultado["valor_a_pagar"]);
                            if (resultado["total_a_pagar"] != DBNull.Value) entidad.total_a_pagar = Convert.ToInt64(resultado["total_a_pagar"]);
                            if (resultado["dias_mora"] != DBNull.Value) entidad.Dias_mora = Convert.ToString(resultado["dias_mora"]);
                            if (resultado["principal"] != DBNull.Value) entidad.estado = Convert.ToInt64(resultado["principal"]); else entidad.estado = 1;
                            if (resultado["TIPO_LINEA"] != DBNull.Value) entidad.tipo_linea = Convert.ToInt64(resultado["TIPO_LINEA"]);
                            if (resultado["VALOR_CUOTAS_EXTRAS"] != DBNull.Value) entidad.valor_CE = Convert.ToDecimal(resultado["VALOR_CUOTAS_EXTRAS"]);

                            entidad.valor_a_pagar_CE = entidad.valor_a_pagar + entidad.valor_CE;
                            lstPersona.Add(entidad);
                        }

                        return lstPersona;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PersonaData", "ListarDatosCreditoPersona", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene la lista de Personas dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Personas obtenidas</returns>
        public List<Persona> ListarDatosCreditoPersonaAhorros(Persona pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Persona> lstPersona = new List<Persona>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "";
                        if (pEntidad.tipo_linea.ToEnum<TipoDeProducto>() == TipoDeProducto.Credito)
                        {
                            string fecha = "";
                            if (pEntidad.fecha_pago == null)
                                pEntidad.fecha_pago = DateTime.Now;
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                fecha = "To_Date('" + Convert.ToDateTime(pEntidad.fecha_pago).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                fecha = " '" + Convert.ToDateTime(pEntidad.fecha_pago).ToString(conf.ObtenerFormatoFecha()) + "' ";
                            sql = @" SELECT b.cod_deudor As cod_persona, b.numero_radicacion, 2 As codtipoproducto, l.nombre linea, b.monto_aprobado, b.fecha_aprobacion, valor_cuota, b.saldo_capital, 
                                (Select Sum(cx.saldo) From cuenta_porcobrar_cre cx Where cx.numero_radicacion = b.numero_radicacion) As garantias_comunitarias, 
                                b.fecha_proximo_pago, Calcular_VrAPagar(b.numero_radicacion, " + fecha + @") As valor_a_pagar, Calcular_TotalAPagar(b.numero_radicacion, " + fecha + @") As total_a_pagar, FecDifDia(b.fecha_proximo_pago, " + fecha + @", 1) As dias_mora ,0 as PRINCIPAL 
                                FROM credito b Inner Join lineascredito l On b.cod_linea_credito = l.cod_linea_credito
                                WHERE b.cod_deudor = " + pEntidad.cod_persona + @" And b.estado = 'C'
                                ORDER BY b.numero_radicacion asc";
                        }
                        else
                        {
                            string FuncionCalcularCuota = "";
                            switch (pEntidad.tipo_linea.ToEnum<TipoDeProducto>())
                            {
                                case TipoDeProducto.Aporte: FuncionCalcularCuota = "CALCULAR_CUOTA_APORTE(b.NUMERO_RADICACION)"; break;
                                case TipoDeProducto.AhorroProgramado: FuncionCalcularCuota = "CALCULAR_CUOTA_PROGRAMADO(b.NUMERO_RADICACION)"; break;
                                case TipoDeProducto.AhorrosVista: FuncionCalcularCuota = "CALCULAR_CUOTA_AHORRO_VISTA(b.NUMERO_RADICACION)"; break;
                                default: FuncionCalcularCuota = "VALOR_CUOTA"; break;
                            }
                            if (pEntidad.tipo_linea.ToEnum<TipoDeProducto>() == TipoDeProducto.Aporte)
                                sql = @"SELECT a.cod_persona, b.numero_radicacion, b.codtipoproducto,b.nombre linea, b.monto_aprobado, b.fecha_aprobacion, " + FuncionCalcularCuota + @" As valor_cuota, b.saldo_capital, b.garantias_comunitarias, b.fecha_proximo_pago, b.valor_a_pagar, b.total_a_pagar, b.dias_mora, g.principal
                                    FROM persona a, VCajaProductos b LEFT JOIN GRUPO_LINEAAPORTE G ON G.cod_linea_aporte = B.cod_linea_credito         
                                    WHERE  b.codtipoproducto = " + pEntidad.tipo_linea + " and b.cod_deudor = " + pEntidad.cod_persona + @" and b.cod_deudor = a.cod_persona and (g.principal = 1 Or g.principal Is Null)
                                    UNION ALL ";

                            sql += @"SELECT a.cod_persona, b.numero_radicacion, b.codtipoproducto, b.nombre linea, b.monto_aprobado, b.fecha_aprobacion, " + FuncionCalcularCuota + @" As valor_cuota, b.saldo_capital, b.garantias_comunitarias, b.fecha_proximo_pago, b.valor_a_pagar, b.total_a_pagar, b.dias_mora, 1 As principal
                                    FROM persona a, VCajaProductos b
                                    WHERE  b.codtipoproducto = " + pEntidad.tipo_linea + " and b.cod_deudor = " + pEntidad.cod_persona + @" and b.cod_deudor = a.cod_persona      AND    b.codtipoproducto != 1                                 
                                    ORDER BY 2 asc";

                        }

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Persona entidad = new Persona();
                            //Asociar todos los valores a la entidad
                            if (resultado["cod_persona"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["cod_persona"]);
                            if (resultado["numero_radicacion"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["numero_radicacion"]);
                            if (resultado["linea"] != DBNull.Value) entidad.linea_credito = Convert.ToString(resultado["linea"]);
                            if (resultado["monto_aprobado"] != DBNull.Value) entidad.monto_aprobado = Convert.ToInt64(resultado["monto_aprobado"]);
                            if (resultado["valor_cuota"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["valor_cuota"]);
                            if (resultado["saldo_capital"] != DBNull.Value) entidad.saldo_capital = Convert.ToInt64(resultado["saldo_capital"]);
                            if (resultado["garantias_comunitarias"] != DBNull.Value) entidad.garantias_comunitarias = Convert.ToInt64(resultado["garantias_comunitarias"]);
                            if (resultado["fecha_proximo_pago"] != DBNull.Value) entidad.fecha_proxima_pago = Convert.ToDateTime(resultado["fecha_proximo_pago"]);
                            if (resultado["fecha_aprobacion"] != DBNull.Value) entidad.fecha_aprobacion = Convert.ToDateTime(resultado["fecha_aprobacion"]);
                            if (resultado["valor_a_pagar"] != DBNull.Value) entidad.valor_a_pagar = Convert.ToInt64(resultado["valor_a_pagar"]);
                            if (resultado["total_a_pagar"] != DBNull.Value) entidad.total_a_pagar = Convert.ToInt64(resultado["total_a_pagar"]);
                            if (resultado["dias_mora"] != DBNull.Value) entidad.Dias_mora = Convert.ToString(resultado["dias_mora"]);
                            if (resultado["principal"] != DBNull.Value) entidad.estado = Convert.ToInt64(resultado["principal"]); else entidad.estado = 1;

                            lstPersona.Add(entidad);
                        }

                        return lstPersona;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PersonaData", "ListarDatosCreditoPersona", ex);
                        return null;
                    }
                }
            }
        }

        public List<Persona> ListarDatosCreditoPersonaAportes(Persona pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Persona> lstPersona = new List<Persona>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "";
                        if (pEntidad.tipo_linea.ToEnum<TipoDeProducto>() == TipoDeProducto.Credito)
                        {
                            string fecha = "";
                            if (pEntidad.fecha_pago == null)
                                pEntidad.fecha_pago = DateTime.Now;
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                fecha = "To_Date('" + Convert.ToDateTime(pEntidad.fecha_pago).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                fecha = " '" + Convert.ToDateTime(pEntidad.fecha_pago).ToString(conf.ObtenerFormatoFecha()) + "' ";
                            sql = @" SELECT b.cod_deudor As cod_persona, b.numero_radicacion, 2 As codtipoproducto, l.nombre linea, b.monto_aprobado, b.fecha_aprobacion, b.valor_cuota, b.saldo_capital, 
                                (Select Sum(cx.saldo) From cuenta_porcobrar_cre cx Where cx.numero_radicacion = b.numero_radicacion) As garantias_comunitarias, 
                                b.fecha_proximo_pago, Calcular_VrAPagar(b.numero_radicacion, " + fecha + @") As valor_a_pagar, Calcular_TotalAPagar(b.numero_radicacion, " + fecha + @") As total_a_pagar, FecDifDia(b.fecha_proximo_pago, " + fecha + @", 1) As dias_mora ,0 as PRINCIPAL 
                                FROM credito b Inner Join lineascredito l On b.cod_linea_credito = l.cod_linea_credito
                                WHERE b.cod_deudor = " + pEntidad.cod_persona + @" And b.estado = 'C'
                                ORDER BY b.numero_radicacion asc";
                        }
                        else
                        {
                            //sql = @"SELECT a.cod_persona, b.numero_radicacion, b.codtipoproducto,b.nombre linea, b.monto_aprobado, b.fecha_aprobacion, b.valor_cuota, b.saldo_capital, b.garantias_comunitarias, b.fecha_proximo_pago, b.valor_a_pagar, b.total_a_pagar, b.dias_mora, g.principal
                            //        FROM persona a, VCajaProductos b INNER JOIN GRUPO_LINEAAPORTE G ON G.COD_LINEA_APORTE = B.COD_LINEA_CREDITO         
                            //        WHERE  b.codtipoproducto = " + pEntidad.tipo_linea + " and b.cod_deudor = " + pEntidad.cod_persona + @" and b.cod_deudor = a.cod_persona and g.principal = 1
                            //        UNION ALL
                            //        SELECT a.cod_persona, b.numero_radicacion, b.codtipoproducto, b.nombre linea, b.monto_aprobado, b.fecha_aprobacion, b.valor_cuota, b.saldo_capital, b.garantias_comunitarias, b.fecha_proximo_pago, b.valor_a_pagar, b.total_a_pagar, b.dias_mora, 1
                            //        FROM persona a, VCajaProductos b
                            //        WHERE  b.codtipoproducto = " + pEntidad.tipo_linea + " and b.cod_deudor = " + pEntidad.cod_persona + @" and b.cod_deudor = a.cod_persona and b.cod_linea_credito Not In (Select g.cod_linea_aporte From Grupo_LineaAporte g)
                            //        ORDER BY 2 asc";


                            sql = @" SELECT a.cod_persona, b.numero_radicacion, b.codtipoproducto, b.nombre linea, b.monto_aprobado, b.fecha_aprobacion, b.valor_cuota, b.saldo_capital, b.garantias_comunitarias, b.fecha_proximo_pago, b.valor_a_pagar, b.total_a_pagar, b.dias_mora, 1 as principal
                                    FROM persona a, VCajaProductos b
                                    WHERE  b.codtipoproducto = " + pEntidad.tipo_linea + " and b.cod_deudor = " + pEntidad.cod_persona + @" and b.cod_deudor = a.cod_persona      AND    b.codtipoproducto != 1                                 
                                    ORDER BY 2 asc";

                        }

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Persona entidad = new Persona();
                            //Asociar todos los valores a la entidad
                            if (resultado["cod_persona"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["cod_persona"]);
                            if (resultado["numero_radicacion"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["numero_radicacion"]);
                            if (resultado["linea"] != DBNull.Value) entidad.linea_credito = Convert.ToString(resultado["linea"]);
                            if (resultado["monto_aprobado"] != DBNull.Value) entidad.monto_aprobado = Convert.ToInt64(resultado["monto_aprobado"]);
                            if (resultado["valor_cuota"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["valor_cuota"]);
                            if (resultado["saldo_capital"] != DBNull.Value) entidad.saldo_capital = Convert.ToInt64(resultado["saldo_capital"]);
                            if (resultado["garantias_comunitarias"] != DBNull.Value) entidad.garantias_comunitarias = Convert.ToInt64(resultado["garantias_comunitarias"]);
                            if (resultado["fecha_proximo_pago"] != DBNull.Value) entidad.fecha_proxima_pago = Convert.ToDateTime(resultado["fecha_proximo_pago"]);
                            if (resultado["fecha_aprobacion"] != DBNull.Value) entidad.fecha_aprobacion = Convert.ToDateTime(resultado["fecha_aprobacion"]);
                            if (resultado["valor_a_pagar"] != DBNull.Value) entidad.valor_a_pagar = Convert.ToInt64(resultado["valor_a_pagar"]);
                            if (resultado["total_a_pagar"] != DBNull.Value) entidad.total_a_pagar = Convert.ToInt64(resultado["total_a_pagar"]);
                            if (resultado["dias_mora"] != DBNull.Value) entidad.Dias_mora = Convert.ToString(resultado["dias_mora"]);
                            if (resultado["principal"] != DBNull.Value) entidad.estado = Convert.ToInt64(resultado["principal"]); else entidad.estado = 1;

                            lstPersona.Add(entidad);
                        }

                        return lstPersona;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PersonaData", "ListarDatosCreditoPersona", ex);
                        return null;
                    }
                }
            }
        }

        public List<Persona> ListarPersonasAfiliacion(Persona pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Persona> lstPersona = new List<Persona>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT P.IDAFILIACION, P.FECHA_AFILIACION, P.VALOR, P.COD_PERIODICIDAD, E.DESCRIPCION, P.SALDO, P.FECHA_PROXIMO_PAGO "
                                            + " FROM PERSONA_AFILIACION P LEFT JOIN PERIODICIDAD E ON E.COD_PERIODICIDAD = P.COD_PERIODICIDAD "
                                            + " WHERE P.ESTADO In ('I', 'A') and p.COD_PERSONA = " + pEntidad.cod_persona;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Persona entidad = new Persona();
                            //Asociar todos los valores a la entidad
                            if (resultado["IDAFILIACION"] != DBNull.Value) entidad.idafiliacion = Convert.ToInt64(resultado["IDAFILIACION"]);
                            if (resultado["FECHA_AFILIACION"] != DBNull.Value) entidad.fecha_afiliacion = Convert.ToDateTime(resultado["FECHA_AFILIACION"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["COD_PERIODICIDAD"] != DBNull.Value) entidad.cod_periodicidad = Convert.ToInt32(resultado["COD_PERIODICIDAD"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.saldo = Convert.ToDecimal(resultado["SALDO"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proxima_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            lstPersona.Add(entidad);
                        }

                        return lstPersona;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PersonaData", "ListarDatosCreditoPersona", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene la lista de Personas dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Personas obtenidas</returns>
        public Persona ConsultarDatosCreditoPersona(String pId, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Persona entidad = new Persona();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = " SELECT  credito.fecha_proximo_pago,credito.cod_linea_credito,credito.fecha_desembolso,lineascredito.tipo_linea from credito inner join lineascredito on lineascredito.COD_LINEA_CREDITO = credito.COD_LINEA_CREDITO where  numero_radicacion = " + pId + "";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            // Persona entidad = new Persona();
                            //Asociar todos los valores a la entidad
                            if (resultado["cod_linea_credito"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToInt64(resultado["cod_linea_credito"]);
                            if (resultado["fecha_desembolso"] != DBNull.Value) entidad.fecha_desembolso = Convert.ToDateTime(resultado["fecha_desembolso"]);
                            //añadido para credito rotativo
                            if (resultado["tipo_linea"] != DBNull.Value) entidad.tipo_linea = Convert.ToInt64(resultado["tipo_linea"]);
                            if (resultado["fecha_proximo_pago"] != DBNull.Value) entidad.fecha_pago = Convert.ToDateTime(resultado["fecha_proximo_pago"]);

                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PersonaData", "ListarDatosCreditoPersona", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene la lista de Personas dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Personas obtenidas</returns>
        public List<Persona> ListarDatosAportesPersona(Persona pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Persona> lstPersona = new List<Persona>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = " SELECT a.cod_persona, b.numero_radicacion, b.nombre linea, b.monto_aprobado, b.fecha_aprobacion, b.valor_cuota, b.saldo_capital, b.garantias_comunitarias, b.fecha_proximo_pago, b.valor_a_pagar, b.total_a_pagar, b.dias_mora FROM persona a, VCajaProductos b where b.cod_deudor = " + pEntidad.cod_persona + " and b.cod_deudor = a.cod_persona order by b.numero_radicacion asc";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Persona entidad = new Persona();
                            //Asociar todos los valores a la entidad
                            if (resultado["cod_persona"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["cod_persona"]);
                            if (resultado["numero_radicacion"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["numero_radicacion"]);
                            if (resultado["linea"] != DBNull.Value) entidad.linea_credito = Convert.ToString(resultado["linea"]);
                            if (resultado["monto_aprobado"] != DBNull.Value) entidad.monto_aprobado = Convert.ToInt64(resultado["monto_aprobado"]);
                            if (resultado["valor_cuota"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["valor_cuota"]);
                            if (resultado["saldo_capital"] != DBNull.Value) entidad.saldo_capital = Convert.ToInt64(resultado["saldo_capital"]);
                            if (resultado["garantias_comunitarias"] != DBNull.Value) entidad.garantias_comunitarias = Convert.ToInt64(resultado["garantias_comunitarias"]);
                            if (resultado["fecha_proximo_pago"] != DBNull.Value) entidad.fecha_proxima_pago = Convert.ToDateTime(resultado["fecha_proximo_pago"]);
                            if (resultado["fecha_aprobacion"] != DBNull.Value) entidad.fecha_aprobacion = Convert.ToDateTime(resultado["fecha_aprobacion"]);
                            if (resultado["valor_a_pagar"] != DBNull.Value) entidad.valor_a_pagar = Convert.ToInt64(resultado["valor_a_pagar"]);
                            if (resultado["total_a_pagar"] != DBNull.Value) entidad.total_a_pagar = Convert.ToInt64(resultado["total_a_pagar"]);
                            if (resultado["dias_mora"] != DBNull.Value) entidad.Dias_mora = Convert.ToString(resultado["dias_mora"]);

                            lstPersona.Add(entidad);
                        }

                        return lstPersona;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PersonaData", "ListarDatosCreditoPersona", ex);
                        return null;
                    }
                }
            }
        }



        /// <summary>
        /// Obtiene la lista de Clientes dados unos filtros - Garantias
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Clientes obtenidas</returns>
        public List<Persona> ListarCreditosCliente(Persona pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Persona> lstPersona = new List<Persona>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select numero_radicacion,cod_linea_credito,monto_aprobado,fecha_aprobacion from credito where cod_deudor=" + pEntidad.cod_persona;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Persona entidad = new Persona();
                            //Asociar todos los valores a la entidad
                            if (resultado["numero_radicacion"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["numero_radicacion"]);
                            if (resultado["cod_linea_credito"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToInt64(resultado["cod_linea_credito"]);
                            if (resultado["monto_aprobado"] != DBNull.Value) entidad.monto_aprobado = Convert.ToInt64(resultado["monto_aprobado"]);
                            if (resultado["fecha_aprobacion"] != DBNull.Value) entidad.fecha_aprobacion = Convert.ToDateTime(resultado["fecha_aprobacion"]);

                            lstPersona.Add(entidad);
                        }

                        return lstPersona;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PersonaData", "ListarCreditoCliente", ex);
                        return null;
                    }
                }
            }
        }



        /// <summary>
        /// Obtiene la lista de Clientes dados unos filtros - Garantias Activos Hipotecarios
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Clientes obtenidas</returns>
        public List<Persona> ListarActivosHipotecarios(Persona pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Persona> lstPersona = new List<Persona>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select idactivo,a.descripcion descr,direccion,matricula,escritura,notaria,valor_comercial,estado,valor_comprometido from ACTIVOS_PERSONA a, TIPO_ACTIVO_PER b where a.cod_tipo_activo_per=b.cod_tipo_activo_per and clase='H' and cod_persona=" + pEntidad.cod_persona;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Persona entidad = new Persona();
                            //Asociar todos los valores a la entidad
                            if (resultado["idactivo"] != DBNull.Value) entidad.idactivo = Convert.ToInt64(resultado["idactivo"]);
                            if (resultado["descr"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["descr"]);
                            if (resultado["direccion"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["direccion"]);
                            if (resultado["matricula"] != DBNull.Value) entidad.matricula = Convert.ToString(resultado["matricula"]);
                            if (resultado["escritura"] != DBNull.Value) entidad.escritura = Convert.ToString(resultado["escritura"]);
                            if (resultado["notaria"] != DBNull.Value) entidad.notaria = Convert.ToString(resultado["notaria"]);
                            if (resultado["valor_comercial"] != DBNull.Value) entidad.valor_comercial = Convert.ToInt64(resultado["valor_comercial"]);
                            if (resultado["estado"] != DBNull.Value) entidad.estado = Convert.ToInt64(resultado["estado"]);
                            if (resultado["valor_comprometido"] != DBNull.Value) entidad.valor_comprometido = Convert.ToInt64(resultado["valor_comprometido"]);

                            lstPersona.Add(entidad);
                        }

                        return lstPersona;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PersonaData", "ListarActivosHipotecarios", ex);
                        return null;
                    }
                }
            }
        }



        /// <summary>
        /// Obtiene la lista de Clientes dados unos filtros - Garantias Activos Prendarios
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Clientes obtenidas</returns>
        public List<Persona> ListarActivosPrendarios(Persona pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Persona> lstPersona = new List<Persona>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select idactivo,a.descripcion descr,marca,referencia,modelo,uso,capacidad,numero_chasis,serie_motor,estado from ACTIVOS_PERSONA a, TIPO_ACTIVO_PER b where a.cod_tipo_activo_per=b.cod_tipo_activo_per and clase='P' and cod_persona=" + pEntidad.cod_persona;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Persona entidad = new Persona();
                            //Asociar todos los valores a la entidad
                            if (resultado["idactivo"] != DBNull.Value) entidad.idactivo = Convert.ToInt64(resultado["idactivo"]);
                            if (resultado["descr"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["descr"]);
                            if (resultado["marca"] != DBNull.Value) entidad.marca = Convert.ToString(resultado["marca"]);
                            if (resultado["referencia"] != DBNull.Value) entidad.referencia = Convert.ToString(resultado["referencia"]);
                            if (resultado["modelo"] != DBNull.Value) entidad.modelo = Convert.ToString(resultado["modelo"]);
                            if (resultado["capacidad"] != DBNull.Value) entidad.capacidad = Convert.ToString(resultado["capacidad"]);
                            if (resultado["uso"] != DBNull.Value) entidad.uso = Convert.ToString(resultado["uso"]);
                            if (resultado["numero_chasis"] != DBNull.Value) entidad.numero_chasis = Convert.ToString(resultado["numero_chasis"]);
                            if (resultado["serie_motor"] != DBNull.Value) entidad.serie_motor = Convert.ToString(resultado["serie_motor"]);
                            if (resultado["estado"] != DBNull.Value) entidad.estado = Convert.ToInt64(resultado["estado"]);

                            lstPersona.Add(entidad);
                        }

                        return lstPersona;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PersonaData", "ListarActivosPrendarios", ex);
                        return null;
                    }
                }
            }
        }

        public DataTable ListarPersonasProductos(DateTime pFechaCorte, Usuario vUsuario)
        {
            DbDataReader resultado;
            DataTable dtResultado = new DataTable();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DateTime pFechaInicial = DateTime.Parse("1/" + pFechaCorte.Month + "/" + pFechaCorte.Year);

                        string sql = @"select ID_cliente ,ID_producto,saldo,Plazo,Fecha_apertura ,Linea_Producto  from(
                                    select p.IDENTIFICACION as ID_cliente , a.NUMERO_APORTE as ID_producto , ha.saldo, null as Plazo , 
                                    TO_CHAR(ha.FECHA_APERTURA , 'YYYY-MM-DD') as Fecha_Apertura , 'Aporte' as Linea_Producto from persona p
                                    inner join aporte a on a.COD_PERSONA = p.COD_PERSONA
                                    inner join HISTORICO_APORTE ha on ha.NUMERO_APORTE = a.NUMERO_APORTE and ha.FECHA_HISTORICO = to_date('" + pFechaCorte.ToString("dd/MM/yyyy") + "' , 'DD/MM/YYYY')" +

                                    @"union 
                                    select p.IDENTIFICACION as ID_cliente , to_number(a.NUMERO_CUENTA) as ID_producto , ha.SALDO_TOTAL, null as Plazo , 
                                    TO_CHAR(ha.FECHA_APERTURA , 'YYYY-MM-DD') as Fecha_Apertura , 'Ahorro a la vista' as Linea_Producto from persona p
                                    inner join AHORRO_VISTA a on a.COD_PERSONA = p.COD_PERSONA
                                    inner join HISTORICO_AHORRO ha on ha.NUMERO_CUENTA = a.NUMERO_CUENTA and ha.FECHA_HISTORICO = to_date('" + pFechaCorte.ToString("dd/MM/yyyy") + "' , 'DD/MM/YYYY')" +

                                    @"union 
                                    select p.IDENTIFICACION as ID_cliente , hs.NUMERO_SERVICIO as ID_producto , hs.saldo, hs.NUMERO_CUOTAS as Plazo , 
                                    TO_CHAR(hs.FECHA_SOLICITUD , 'YYYY-MM-DD') as Fecha_Apertura , 'Servicios' as Linea_Producto from persona p
                                    inner join HISTORICO_SERVICIOS hs on hs.COD_PERSONA = p.COD_PERSONA and hs.FECHA_HISTORICO = to_date('" + pFechaCorte.ToString("dd/MM/yyyy") + "', 'DD/MM/YYYY')" +

                                    @"union 
                                    select p.IDENTIFICACION as ID_cliente , to_number(a.NUMERO_PROGRAMADO) as ID_producto , hp.SALDO_TOTAL as Saldo, hp.PLAZO as Plazo , 
                                    TO_CHAR(hp.FECHA_APERTURA , 'YYYY-MM-DD') as Fecha_Apertura , 'Ahorro programado' as Linea_Producto from persona p
                                    inner join AHORRO_PROGRAMADO a on a.COD_PERSONA = p.COD_PERSONA
                                    inner join HISTORICO_PROGRAMADO hp on hp.NUMERO_PROGRAMADO = a.NUMERO_PROGRAMADO and hp.FECHA_HISTORICO = to_date('" + pFechaCorte.ToString("dd/MM/yyyy") + "', 'DD/MM/YYYY')" +

                                    @"union
                                    select p.IDENTIFICACION as ID_cliente , hc.NUMERO_RADICACION as ID_producto , hc.SALDO_CAPITAL, hc.NUMERO_CUOTAS as Plazo , 
                                    TO_CHAR(hc.FECHA_DESEMBOLSO, 'YYYY-MM-DD') as Fecha_Apertura , 'Credito' as Linea_Producto from persona p
                                    inner join HISTORICO_CRE hc on hc.COD_CLIENTE = p.COD_PERSONA and hc.FECHA_HISTORICO = to_date('" + pFechaCorte.ToString("dd/MM/yyyy") + "', 'DD/MM/YYYY')" +

                                    @"union
                                    select ct.IDENTIFICACION as ID_Cliente, to_number(hc.Numero_cdat) , hc.Valor , hc.Plazo ,TO_CHAR(hc.FECHA_APERTURA) as Fecha_Apertura , 'CDAT' from HISTORICO_CDAT hc
                                    inner join(
                                        select CODIGO_CDAT, p.IDENTIFICACION from CDAT_TITULAR c
                                        inner join Persona p on p.COD_PERSONA = c.COD_PERSONA
                                        where PRINCIPAL = 1
                                    )ct on ct.CODIGO_CDAT = hc.CODIGO_CDAT
                                    where hc.FECHA_HISTORICO BETWEEN to_date('" + pFechaInicial.ToString("dd/MM/yyyy") + @"', 'DD/MM/YYYY') and to_date('" + pFechaCorte.ToString("dd/MM/yyyy") + @"', 'DD/MM/YYYY')" +
                                    ") order by ID_Cliente";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        dtResultado.Load(cmdTransaccionFactory.ExecuteReader());

                        dbConnectionFactory.CerrarConexion(connection);
                        return dtResultado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PersonaData", "ListarPersonasProductos", ex);
                        return null;
                    }
                }
            }
        }
    }
}
