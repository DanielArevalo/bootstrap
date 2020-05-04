using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Entities;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Xpinn.FabricaCreditos.Data
{
    public class CreditoSolicitadoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para Programa
        /// </summary>
        public CreditoSolicitadoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Obtiene la lista de Aprobadores
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Aprobadores obtenidos</returns>
        public List<CreditoSolicitado> ListarCreditos(CreditoSolicitado entidad, Usuario pUsuario, String filtro = "")
        {
            DbDataReader resultado = default(DbDataReader);
            List<CreditoSolicitado> lstCreditos = new List<CreditoSolicitado>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        //Se comentarea el select inicial donde "c.estado = 'V'". El estado cambia de "S" a "V" cuando el credito pasa por "referenciacion"                        
                        string sql = @"Select lineascredito.tipo_linea,oficina.nombre as oficina,c.numero_radicacion, p.identificacion, p.primer_nombre || ' ' || p.segundo_nombre || ' ' || p.primer_apellido || ' ' || p.segundo_apellido as nombres, c.monto_solicitado, c.numero_cuotas, c.valor_cuota, p.cod_nomina ,
                                         (Select x.puntaje from preanalisis x Where x.cod_persona = p.cod_persona and x.fechanalisis <= SYSDATE - 30)  AS puntaje, c.cod_linea_credito, lineascredito.nombre, to_char(c.fecha_solicitud , 'dd/MM/yyyy') as fecha_solicitud " +
                                   "from credito c inner join lineascredito on c.cod_linea_credito=lineascredito.cod_linea_credito, persona p inner join oficina on p.cod_oficina=oficina.cod_oficina Where c.cod_deudor = p.cod_persona " + filtro;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            entidad = new CreditoSolicitado();
                            //Asociar todos los valores a la entidad
                            if (resultado["numero_radicacion"] != DBNull.Value) entidad.NumeroCredito = Convert.ToInt64(resultado["numero_radicacion"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.Identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["nombres"] != DBNull.Value) entidad.Nombres = Convert.ToString(resultado["nombres"]);
                            if (resultado["cod_nomina"] != DBNull.Value) entidad.cod_nomina = Convert.ToString(resultado["cod_nomina"]);

                            if (resultado["monto_solicitado"] != DBNull.Value) entidad.Monto = Convert.ToInt64(resultado["monto_solicitado"]);
                            if (resultado["numero_cuotas"] != DBNull.Value) entidad.Plazo = Convert.ToString(resultado["numero_cuotas"]);
                            if (resultado["valor_cuota"] != DBNull.Value) entidad.Cuota = Convert.ToInt64(resultado["valor_cuota"]);
                            if (resultado["puntaje"] != DBNull.Value) entidad.Calificacion = Convert.ToInt16(resultado["puntaje"]);
                            if (resultado["cod_linea_credito"] != DBNull.Value) entidad.LineaCredito = Convert.ToString(resultado["cod_linea_credito"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.Nom_linea_credito = Convert.ToString(resultado["nombre"]);
                            if (resultado["oficina"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["oficina"]);
                            if (resultado["fecha_solicitud"] != DBNull.Value) entidad.fechasolicitud = Convert.ToString(resultado["fecha_solicitud"]);
                            if (resultado["tipo_linea"] != DBNull.Value) entidad.tipo_credito = Convert.ToInt64(resultado["tipo_linea"]);
                            lstCreditos.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCreditos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoSolicitadoData", "ListarCreditos", ex);
                        return null;
                    }
                }
            }
        }

        public List<CreditoSolicitado> ListaCreditosFiltradosEstadoV(CreditoSolicitado entidad, Usuario pUsuario, String filtro = "")
        {
            DbDataReader resultado = default(DbDataReader);
            List<CreditoSolicitado> lstCreditos = new List<CreditoSolicitado>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        //string sql = @"Select oficina.nombre as oficina,c.numero_radicacion, p.identificacion, p.primer_nombre || ' ' || p.segundo_nombre || ' ' || p.primer_apellido || ' ' || p.segundo_apellido as nombres, p.cod_nomina, c.monto_solicitado, c.numero_cuotas, c.valor_cuota, 
                        //                (Select x.puntaje from preanalisis x Where x.cod_persona = p.cod_persona and x.fechanalisis <= SYSDATE - 30)  AS puntaje, c.cod_linea_credito, lineascredito.nombre 
                        //                    from credito c 
                        //                    inner join lineascredito on c.cod_linea_credito=lineascredito.cod_linea_credito 
                        //                    inner join persona p on c.cod_deudor = p.cod_persona
                        //                    inner join oficina on c.cod_oficina=oficina.cod_oficina 
                        //                    Where (c.estado = 'V') " + filtro; 

                        //Consulta modificada ya que el filtro del estado se envia de acuerdo a la parametrización de la entidad
                        string sql = @"Select oficina.nombre as oficina,c.numero_radicacion, p.identificacion, p.primer_nombre || ' ' || p.segundo_nombre || ' ' || p.primer_apellido || ' ' || p.segundo_apellido as nombres, p.cod_nomina, c.FECHA_SOLICITUD, c.monto_solicitado, c.numero_cuotas, c.valor_cuota, 
                                        (Select x.puntaje from preanalisis x Where x.cod_persona = p.cod_persona and x.fechanalisis <= SYSDATE - 30)  AS puntaje, c.cod_linea_credito, lineascredito.nombre,ESTADOCREDITO(c.estado)  As estado , z.descripcion Nom_zona
                                            from credito c 
                                            inner join lineascredito on c.cod_linea_credito=lineascredito.cod_linea_credito 
                                            inner join persona p on c.cod_deudor = p.cod_persona
                                            inner join oficina on c.cod_oficina=oficina.cod_oficina 
                                            left join zonas z on z.cod_zona = p.cod_zona
                                            Where " + filtro;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            entidad = new CreditoSolicitado();
                            //Asociar todos los valores a la entidad
                            if (resultado["numero_radicacion"] != DBNull.Value) entidad.NumeroCredito = Convert.ToInt64(resultado["numero_radicacion"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.Identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["nombres"] != DBNull.Value) entidad.Nombres = Convert.ToString(resultado["nombres"]);
                            if (resultado["cod_nomina"] != DBNull.Value) entidad.cod_nomina = Convert.ToString(resultado["cod_nomina"]);
                            if (resultado["FECHA_SOLICITUD"] != DBNull.Value) entidad.fecha_solicitud = Convert.ToDateTime(resultado["FECHA_SOLICITUD"]);
                            if (resultado["monto_solicitado"] != DBNull.Value) entidad.Monto = Convert.ToInt64(resultado["monto_solicitado"]);
                            if (resultado["numero_cuotas"] != DBNull.Value) entidad.Plazo = Convert.ToString(resultado["numero_cuotas"]);
                            if (resultado["valor_cuota"] != DBNull.Value) entidad.Cuota = Convert.ToInt64(resultado["valor_cuota"]);
                            if (resultado["puntaje"] != DBNull.Value) entidad.Calificacion = Convert.ToInt16(resultado["puntaje"]);
                            if (resultado["cod_linea_credito"] != DBNull.Value) entidad.LineaCredito = Convert.ToString(resultado["cod_linea_credito"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.Nom_linea_credito = Convert.ToString(resultado["nombre"]);
                            if (resultado["oficina"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["oficina"]);
                            if (resultado["estado"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["estado"]);
                            if (resultado["Nom_zona"] != DBNull.Value) entidad.NomZona = Convert.ToString(resultado["Nom_zona"]);

                            lstCreditos.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCreditos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoSolicitadoData", "ListarCreditos", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene la lista de Aprobadores
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Aprobadores obtenidos</returns>
        public List<CreditoSolicitado> ListarCreditosRotativos(CreditoSolicitado entidad, DateTime pFecha, Usuario pUsuario, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<CreditoSolicitado> lstCreditos = new List<CreditoSolicitado>();
            Configuracion conf = new Configuracion();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select * from v_creditos where 1=1" + filtro;

                        if (pFecha != null && pFecha != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " And ";
                            else
                                sql += " Where ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " fechasolicitud = To_Date('" + Convert.ToDateTime(pFecha
                                    ).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " fechasolicitud = '" + Convert.ToDateTime(pFecha).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }


                        sql += "Order by numero_radicacion";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            entidad = new CreditoSolicitado();
                            //Asociar todos los valores a la entidad
                            if (resultado["numero_radicacion"] != DBNull.Value) entidad.NumeroCredito = Convert.ToInt64(resultado["numero_radicacion"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.Identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["nombres"] != DBNull.Value) entidad.Nombres = Convert.ToString(resultado["nombres"]);
                            if (resultado["cod_nomina"] != DBNull.Value) entidad.cod_nomina = Convert.ToString(resultado["cod_nomina"]);
                            if (resultado["monto_solicitado"] != DBNull.Value) entidad.Monto = Convert.ToInt64(resultado["monto_solicitado"]);
                            if (resultado["monto_aprobado"] != DBNull.Value) entidad.MontoAprobado = Convert.ToInt64(resultado["monto_aprobado"]);
                            if (resultado["numero_cuotas"] != DBNull.Value) entidad.Plazo = Convert.ToString(resultado["numero_cuotas"]);
                            if (resultado["valor_cuota"] != DBNull.Value) entidad.Cuota = Convert.ToInt64(resultado["valor_cuota"]);
                            if (resultado["cod_linea_credito"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["cod_linea_credito"]);
                            if (resultado["periodicidad"] != DBNull.Value) entidad.Periodicidad = Convert.ToString(resultado["periodicidad"]);
                            if (resultado["fechasolicitud"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["fechasolicitud"]);
                            if (resultado["numero_radicacion"] != DBNull.Value) entidad.NumeroCredito = Convert.ToInt64(resultado["numero_radicacion"]);
                            if (resultado["linea"] != DBNull.Value) entidad.LineaCredito = Convert.ToString(resultado["linea"]);
                            if (resultado["forma_pago"] != DBNull.Value) entidad.forma_pago = Convert.ToString(resultado["forma_pago"]);
                            if (resultado["cod_oficina"] != DBNull.Value) entidad.CodigoCliente = Convert.ToInt32(resultado["cod_oficina"]);
                            if (resultado["saldo_capital"] != DBNull.Value) entidad.saldo_capital = Convert.ToInt32(resultado["saldo_capital"]);

                            lstCreditos.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCreditos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoSolicitadoData", "ListarCreditosRotativos", ex);
                        return null;
                    }
                }
            }
        }
        /// <returns>Conjunto de Aprobadores obtenidos</returns>
        public List<CreditoSolicitado> ListarCreditosRotativosSolicitados(CreditoSolicitado entidad, Usuario pUsuario, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<CreditoSolicitado> lstCreditos = new List<CreditoSolicitado>();
            Configuracion conf = new Configuracion();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select * from v_creditos where 1=1" + filtro;

                        //if (pFecha != null && pFecha != DateTime.MinValue)
                        //{
                        //    if (sql.ToUpper().Contains("WHERE"))
                        //        sql += " And ";
                        //    else
                        //        sql += " Where ";
                        //    if (dbConnectionFactory.TipoConexion() == "ORACLE")
                        //        sql += " fechasolicitud = To_Date('" + Convert.ToDateTime(pFecha
                        //            ).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                        //    else
                        //        sql += " fechasolicitud = '" + Convert.ToDateTime(pFecha).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        //}


                        sql += "and tipo_linea=2 and estado not in('C','A')  Order by numero_radicacion";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            entidad = new CreditoSolicitado();
                            //Asociar todos los valores a la entidad
                            if (resultado["numero_radicacion"] != DBNull.Value) entidad.NumeroCredito = Convert.ToInt64(resultado["numero_radicacion"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.Identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["nombres"] != DBNull.Value) entidad.Nombres = Convert.ToString(resultado["nombres"]);
                            if (resultado["cod_nomina"] != DBNull.Value) entidad.cod_nomina = Convert.ToString(resultado["cod_nomina"]);
                            if (resultado["monto_solicitado"] != DBNull.Value) entidad.Monto = Convert.ToInt64(resultado["monto_solicitado"]);
                            if (resultado["monto_aprobado"] != DBNull.Value) entidad.MontoAprobado = Convert.ToInt64(resultado["monto_aprobado"]);
                            if (resultado["numero_cuotas"] != DBNull.Value) entidad.Plazo = Convert.ToString(resultado["numero_cuotas"]);
                            if (resultado["valor_cuota"] != DBNull.Value) entidad.Cuota = Convert.ToInt64(resultado["valor_cuota"]);
                            if (resultado["cod_linea_credito"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["cod_linea_credito"]);
                            if (resultado["periodicidad"] != DBNull.Value) entidad.Periodicidad = Convert.ToString(resultado["periodicidad"]);
                            if (resultado["fechasolicitud"] != DBNull.Value) entidad.fechasolicitud = Convert.ToString(resultado["fechasolicitud"]);
                            if (resultado["numero_radicacion"] != DBNull.Value) entidad.NumeroCredito = Convert.ToInt64(resultado["numero_radicacion"]);
                            if (resultado["linea"] != DBNull.Value) entidad.LineaCredito = Convert.ToString(resultado["linea"]);
                            if (resultado["forma_pago"] != DBNull.Value) entidad.forma_pago = Convert.ToString(resultado["forma_pago"]);
                            if (resultado["cod_oficina"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["cod_oficina"]);
                            if (resultado["oficina"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["oficina"]);
                            lstCreditos.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCreditos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoSolicitadoData", "ListarCreditosRotativosSolicitados", ex);
                        return null;
                    }
                }
            }
        }

        public CreditoSolicitado ConsultarCreditosRotativos(CreditoSolicitado entidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            //CreditoSolicitado lstCreditos = new List<CreditoSolicitado>();
            Configuracion conf = new Configuracion();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select * from v_creditos where 1=1";
                        sql += "and tipo_linea=2 and numero_radicacion= " + entidad.NumeroCredito;


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            entidad = new CreditoSolicitado();
                            //Asociar todos los valores a la entidad
                            if (resultado["cod_deudor"] != DBNull.Value) entidad.Cod_deudor = Convert.ToInt32(resultado["cod_deudor"]);
                            if (resultado["numero_radicacion"] != DBNull.Value) entidad.NumeroCredito = Convert.ToInt64(resultado["numero_radicacion"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.Identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["nombres"] != DBNull.Value) entidad.Nombres = Convert.ToString(resultado["nombres"]);
                            if (resultado["monto_solicitado"] != DBNull.Value) entidad.Monto = Convert.ToInt64(resultado["monto_solicitado"]);
                            if (resultado["monto_aprobado"] != DBNull.Value) entidad.MontoAprobado = Convert.ToInt64(resultado["monto_aprobado"]);
                            if (resultado["numero_cuotas"] != DBNull.Value) entidad.Plazo = Convert.ToString(resultado["numero_cuotas"]);
                            if (resultado["valor_cuota"] != DBNull.Value) entidad.Cuota = Convert.ToInt64(resultado["valor_cuota"]);
                            if (resultado["cod_linea_credito"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["cod_linea_credito"]);
                            if (resultado["periodicidad"] != DBNull.Value) entidad.Periodicidad = Convert.ToString(resultado["periodicidad"]);
                            if (resultado["fechasolicitud"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["fechasolicitud"]);
                            if (resultado["numero_radicacion"] != DBNull.Value) entidad.NumeroCredito = Convert.ToInt64(resultado["numero_radicacion"]);
                            if (resultado["linea"] != DBNull.Value) entidad.LineaCredito = Convert.ToString(resultado["linea"]);
                            if (resultado["forma_pago"] != DBNull.Value) entidad.forma_pago = Convert.ToString(resultado["forma_pago"]);
                            if (entidad.forma_pago == "Caja")
                                entidad.forma_pago = "1";
                            else
                                entidad.forma_pago = "2";

                            if (resultado["cod_oficina"] != DBNull.Value) entidad.CodigoCliente = Convert.ToInt32(resultado["cod_oficina"]);
                            if (resultado["cod_ASESOR"] != DBNull.Value) entidad.Cod_asesor = Convert.ToInt32(resultado["cod_ASESOR"]);
                            if (resultado["TIPO_LIQU"] != DBNull.Value) entidad.Tipo_liqu = Convert.ToInt32(resultado["TIPO_LIQU"]);
                            if (resultado["COD_PERIOD"] != DBNull.Value) entidad.cod_Periodicidad = Convert.ToInt32(resultado["COD_PERIOD"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);
                            if (resultado["numerosolicitud"] != DBNull.Value) entidad.numsolicitud = Convert.ToInt32(resultado["numerosolicitud"]);
                            if (resultado["empresa"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt64(resultado["empresa"]);
                            if (resultado["fecha_proximo_pago"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["fecha_proximo_pago"]);

                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoSolicitadoData", "ConsultarCreditosRotativos", ex);
                        return null;
                    }
                }
            }
        }


        public Xpinn.FabricaCreditos.Entities.Imagenes ObtenerSoporte(long pId, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            //CreditoSolicitado lstCreditos = new List<CreditoSolicitado>();
            Configuracion conf = new Configuracion();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select d.IDDOCUMENTO, d.IMAGEN, t.DESCRIPCION, p.IDENTIFICACION
                                       from DOCUMENTOSANEXOS d
                                            inner join TIPOSDOCUMENTO t on d.TIPO_DOCUMENTO = t.TIPO_DOCUMENTO
                                            inner join SOLICITUDCRED s on d.NUMEROSOLICITUD = s.NUMEROSOLICITUD
                                            inner join PERSONA p on s.COD_PERSONA = p.COD_PERSONA
                                        where d.IDDOCUMENTO = " + pId + "";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        Xpinn.FabricaCreditos.Entities.Imagenes entidad = new Xpinn.FabricaCreditos.Entities.Imagenes();
                        while (resultado.Read())
                        {
                            //Asociar todos los valores a la entidad
                            if (resultado["IDDOCUMENTO"] != DBNull.Value) entidad.idimagen = Convert.ToInt32(resultado["IDDOCUMENTO"]);
                            if (resultado["IMAGEN"] != DBNull.Value)
                            {
                                entidad.imagen = (byte[])resultado["IMAGEN"];
                            }
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.tipodocumento = Convert.ToString(resultado["IDENTIFICACION"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoSolicitadoData", "ObtenerSoporte", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene la informacion de los creditos solicitados
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Datos del credito solicitado</returns>
        public CreditoSolicitado ConsultarCredito(CreditoSolicitado pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            CreditoSolicitado entidad = new CreditoSolicitado();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        //Se comentarea el select inicial donde "c.estado = 'V'". El estado cambia de "S" a "V" cuando el credito pasa por "referenciacion"                        
                        string sql = @"Select P.Cod_Persona, P.Identificacion, P.Primer_Nombre || ' ' || P.Segundo_Nombre || ' ' || P.Primer_Apellido || ' ' || P.Segundo_Apellido As Nombres,
                                    Trunc(Months_Between(To_Date(To_Char(Sysdate, 'dd/mm/yyyy'), 'dd/mm/yyyy'),To_Date(To_Char(Fechanacimiento, 'dd/mm/yyyy'), 'dd/mm/yyyy'))/12) Edad,
                                    (Select Min(x.puntaje) from preanalisis x Where x.cod_persona = p.cod_persona and x.fechanalisis <= SYSDATE - 30)  AS calificacion,
                                    C.Numero_Radicacion, C.Cod_Linea_Credito || '-' || L.Nombre As Linea_Credito, C.Destinacion As Destino, Sc.Concepto, C.Monto_Solicitado,
                                    C.Numero_Cuotas Plazo, 0 Disponible, C.Valor_Cuota, Per.Descripcion As Periodicidad, C.Cod_Periodicidad, C.Tasa, C.Cod_Clasifica, C.Cod_Linea_Credito,
                                    Case When Max(sc.Reqpoliza) Is Null Then '0' Else Max(sc.Reqpoliza) end reqpoliza, C.Estado, C.Fecha_Primerpago, C.Forma_Pago, C.Cod_Empresa 
                                    From Credito C 
                                    Inner Join Persona P On P.Cod_Persona = C.Cod_Deudor
                                    Inner Join Lineascredito L On L.Cod_Linea_Credito = C.Cod_Linea_Credito
                                    Inner Join Periodicidad Per On Per.Cod_Periodicidad = C.Cod_Periodicidad
                                    left join SolicitudCred sc on Sc.NumeroSolicitud = c.Numero_obligacion
                                    Where Numero_Radicacion = " + pEntidad.NumeroCredito + @"
                                    Group By P.Cod_Persona, P.Identificacion, P.Primer_Nombre, P.Segundo_Nombre, P.Primer_Apellido, P.Segundo_Apellido,
                                    Fechanacimiento, C.Numero_Radicacion, C.Cod_Linea_Credito, L.Nombre, C.Destinacion, Sc.Concepto, 
                                    C.Monto_Solicitado, C.Numero_Cuotas, 0, C.Valor_Cuota, Per.Descripcion, C.Cod_Periodicidad, C.Tasa, C.Cod_Clasifica, 
                                    C.Cod_Linea_Credito, C.Estado, C.Fecha_Primerpago, C.Forma_Pago, C.Cod_Empresa ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            //Asociar todos los valores a la entidad
                            if (resultado["cod_persona"] != DBNull.Value) entidad.CodigoCliente = Convert.ToInt64(resultado["cod_persona"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.Identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["nombres"] != DBNull.Value) entidad.Nombres = Convert.ToString(resultado["nombres"]);
                            if (resultado["edad"] != DBNull.Value) entidad.Edad = Convert.ToInt32(resultado["edad"]);
                            if (resultado["calificacion"] != DBNull.Value) entidad.Calificacion = Convert.ToInt32(resultado["calificacion"]);
                            if (resultado["numero_radicacion"] != DBNull.Value) entidad.NumeroCredito = Convert.ToInt64(resultado["numero_radicacion"]);
                            if (resultado["linea_credito"] != DBNull.Value) entidad.LineaCredito = Convert.ToString(resultado["linea_credito"]);
                            if (resultado["monto_solicitado"] != DBNull.Value) entidad.Monto = Convert.ToDecimal(resultado["monto_solicitado"]);
                            if (resultado["plazo"] != DBNull.Value) entidad.Plazo = Convert.ToString(resultado["plazo"]);
                            if (resultado["disponible"] != DBNull.Value) entidad.Disponible = Convert.ToInt64(resultado["disponible"]);
                            if (resultado["valor_cuota"] != DBNull.Value) entidad.Cuota = Convert.ToInt64(resultado["valor_cuota"]);
                            if (resultado["periodicidad"] != DBNull.Value) entidad.Periodicidad = Convert.ToString(resultado["periodicidad"]);
                            if (resultado["cod_periodicidad"] != DBNull.Value) entidad.Cod_Periodicidad = Convert.ToInt64(resultado["cod_periodicidad"]);
                            if (resultado["tasa"] != DBNull.Value) entidad.tasa = Convert.ToDecimal(resultado["tasa"]);
                            if (resultado["cod_clasifica"] != DBNull.Value) entidad.cod_clasifica = Convert.ToInt32(resultado["cod_clasifica"]);
                            if (resultado["cod_linea_credito"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["cod_linea_credito"]);
                            if (resultado["reqpoliza"] != DBNull.Value) entidad.reqpoliza = Convert.ToInt32(resultado["reqpoliza"]);
                            if (resultado["estado"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["estado"]);
                            if (resultado["fecha_primerpago"] != DBNull.Value) entidad.fecha_primer_pago = Convert.ToDateTime(resultado["fecha_primerpago"]);
                            if (resultado["Forma_Pago"] != DBNull.Value) entidad.forma_pago = Convert.ToString(resultado["Forma_Pago"]);
                            if (resultado["Cod_Empresa"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt32(resultado["Cod_Empresa"]);
                            if (resultado["destino"] != DBNull.Value) entidad.cod_Destino = Convert.ToString(resultado["destino"]);
                            if (resultado["concepto"] != DBNull.Value) entidad.Obs_Concepto = Convert.ToString(resultado["concepto"]);

                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoSolicitadoData", "ConsultarAprobador", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Aprueba el credito en la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad CreditoSolicitado</param>
        /// <returns>Entidad creada</returns>
        public CreditoSolicitado AprobarCredito(CreditoSolicitado pEntidad, Usuario pUsuario, ref string sError)
        {
            sError = "";
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_login = cmdTransaccionFactory.CreateParameter();
                        p_login.ParameterName = "p_login";
                        p_login.Value = pEntidad.Nombres;
                        p_login.Direction = ParameterDirection.Input;

                        DbParameter p_radicacion = cmdTransaccionFactory.CreateParameter();
                        p_radicacion.ParameterName = "p_radicacion";
                        p_radicacion.Value = pEntidad.NumeroCredito;

                        p_radicacion.Direction = ParameterDirection.Input;

                        DbParameter p_observaciones = cmdTransaccionFactory.CreateParameter();
                        p_observaciones.ParameterName = "p_observaciones";
                        p_observaciones.Value = (pEntidad.ObservacionesAprobacion);
                        p_observaciones.Direction = ParameterDirection.Input;

                        DbParameter p_fecha_aprobacion = cmdTransaccionFactory.CreateParameter();
                        p_fecha_aprobacion.ParameterName = "p_fecha_aprobacion";
                        p_fecha_aprobacion.DbType = DbType.DateTime;
                        p_fecha_aprobacion.Value = (pEntidad.fecha);
                        p_fecha_aprobacion.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(p_login);
                        cmdTransaccionFactory.Parameters.Add(p_radicacion);
                        cmdTransaccionFactory.Parameters.Add(p_observaciones);
                        cmdTransaccionFactory.Parameters.Add(p_fecha_aprobacion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APR_CREDI_APROB";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pEntidad, pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                        return pEntidad;
                    }
                    catch (Exception ex)
                    {
                        // BOExcepcion.Throw("CreditoSolicitadoData", "AprobarCredito", ex);
                        sError = ex.Message;
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Aprueba el credito modificando las condiciones en la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad CreditoSolicitado</param>
        /// <returns>Entidad creada</returns>
        public CreditoSolicitado AprobarCreditoModificando(CreditoSolicitado pEntidad, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_login = cmdTransaccionFactory.CreateParameter();
                        p_login.ParameterName = "p_login";
                        // p_login.Size = 400;
                        p_login.Value = pEntidad.Nombres;
                        p_login.Direction = ParameterDirection.Input;

                        DbParameter p_radicacion = cmdTransaccionFactory.CreateParameter();
                        p_radicacion.ParameterName = "p_radicacion";
                        p_radicacion.Value = pEntidad.NumeroCredito;
                        p_radicacion.Direction = ParameterDirection.Input;

                        DbParameter p_observaciones = cmdTransaccionFactory.CreateParameter();
                        p_observaciones.ParameterName = "p_observaciones";
                        p_observaciones.Value = (pEntidad.ObservacionesAprobacion);
                        p_observaciones.Direction = ParameterDirection.Input;

                        DbParameter p_monto = cmdTransaccionFactory.CreateParameter();
                        p_monto.ParameterName = "p_monto";
                        p_monto.Value = (pEntidad.Monto);
                        p_monto.Direction = ParameterDirection.Input;

                        DbParameter p_plazo = cmdTransaccionFactory.CreateParameter();
                        p_plazo.ParameterName = "p_plazo";
                        p_plazo.Value = (pEntidad.Plazo);
                        p_plazo.Direction = ParameterDirection.Input;

                        DbParameter p_periodicidad = cmdTransaccionFactory.CreateParameter();
                        p_periodicidad.ParameterName = "p_periodicidad";
                        p_periodicidad.Value = (pEntidad.cod_Periodicidad);
                        p_periodicidad.Direction = ParameterDirection.Input;

                        DbParameter p_fecha_apro = cmdTransaccionFactory.CreateParameter();
                        p_fecha_apro.ParameterName = "p_fecha_apro";
                        p_fecha_apro.Value = (pEntidad.fecha);
                        p_fecha_apro.DbType = DbType.DateTime;
                        p_fecha_apro.Direction = ParameterDirection.Input;

                        DbParameter p_cod_linea_credito = cmdTransaccionFactory.CreateParameter();
                        p_cod_linea_credito.ParameterName = "p_cod_linea_credito";
                        p_cod_linea_credito.Value = (pEntidad.cod_linea_credito);
                        p_cod_linea_credito.DbType = DbType.String;
                        p_cod_linea_credito.Direction = ParameterDirection.Input;

                        DbParameter p_destinacion = cmdTransaccionFactory.CreateParameter();
                        p_destinacion.ParameterName = "p_destinacion";
                        if (pEntidad.cod_Destino == "" || pEntidad.cod_Destino == null)
                            p_destinacion.Value = DBNull.Value;
                        else
                            p_destinacion.Value = (pEntidad.cod_Destino);
                        p_destinacion.DbType = DbType.String;
                        p_destinacion.Direction = ParameterDirection.Input;

                        DbParameter p_reqpoliza = cmdTransaccionFactory.CreateParameter();
                        p_reqpoliza.ParameterName = "p_reqpoliza";
                        p_reqpoliza.Value = (pEntidad.reqpoliza);
                        p_reqpoliza.DbType = DbType.Int32;
                        p_reqpoliza.Direction = ParameterDirection.Input;

                        DbParameter p_forma_pago = cmdTransaccionFactory.CreateParameter();
                        p_forma_pago.ParameterName = "p_forma_pago";
                        p_forma_pago.Value = (pEntidad.forma_pago);
                        p_forma_pago.DbType = DbType.String;
                        p_forma_pago.Direction = ParameterDirection.Input;

                        DbParameter p_fecha_primer_pago = cmdTransaccionFactory.CreateParameter();
                        p_fecha_primer_pago.ParameterName = "p_fecha_primer_pago";
                        if (pEntidad.fecha_primer_pago == null)
                            p_fecha_primer_pago.Value = DBNull.Value;
                        else
                            p_fecha_primer_pago.Value = (pEntidad.fecha_primer_pago);
                        p_fecha_primer_pago.DbType = DbType.DateTime;
                        p_fecha_primer_pago.Direction = ParameterDirection.Input;

                        DbParameter p_comision = cmdTransaccionFactory.CreateParameter();
                        p_comision.ParameterName = "P_Comision";
                        if (pEntidad.comision == null)
                            p_comision.Value = DBNull.Value;
                        else
                            p_comision.Value = (pEntidad.comision);
                        p_comision.DbType = DbType.Decimal;
                        p_comision.Direction = ParameterDirection.Input;

                        DbParameter p_aporte = cmdTransaccionFactory.CreateParameter();
                        p_aporte.ParameterName = "P_Aporte";
                        if (pEntidad.aporte == null) p_aporte.Value = DBNull.Value; else p_aporte.Value = (pEntidad.aporte);
                        p_aporte.DbType = DbType.Decimal;
                        p_aporte.Direction = ParameterDirection.Input;

                        DbParameter p_seguro = cmdTransaccionFactory.CreateParameter();
                        p_seguro.ParameterName = "p_seguro";
                        if (pEntidad.seguro == null) p_seguro.Value = DBNull.Value; else p_seguro.Value = (pEntidad.seguro);
                        p_seguro.DbType = DbType.Decimal;
                        p_seguro.Direction = ParameterDirection.Input;

                        DbParameter P_Condicion_Especial = cmdTransaccionFactory.CreateParameter();
                        P_Condicion_Especial.ParameterName = "P_Condicion_Especial";
                        if (pEntidad.condicion_especial == null) P_Condicion_Especial.Value = DBNull.Value; else P_Condicion_Especial.Value = (pEntidad.condicion_especial);
                        P_Condicion_Especial.DbType = DbType.Int32;
                        P_Condicion_Especial.Direction = ParameterDirection.Input;

                        DbParameter p_mensajeerror = cmdTransaccionFactory.CreateParameter();
                        p_mensajeerror.ParameterName = "p_mensajeerror";
                        p_mensajeerror.Value = DBNull.Value;
                        p_mensajeerror.Size = 400;
                        p_mensajeerror.DbType = DbType.StringFixedLength;
                        p_mensajeerror.Direction = ParameterDirection.Output;

                        cmdTransaccionFactory.Parameters.Add(p_login);
                        cmdTransaccionFactory.Parameters.Add(p_radicacion);
                        cmdTransaccionFactory.Parameters.Add(p_observaciones);
                        cmdTransaccionFactory.Parameters.Add(p_monto);
                        cmdTransaccionFactory.Parameters.Add(p_plazo);
                        cmdTransaccionFactory.Parameters.Add(p_periodicidad);
                        cmdTransaccionFactory.Parameters.Add(p_fecha_apro);
                        cmdTransaccionFactory.Parameters.Add(p_cod_linea_credito);
                        cmdTransaccionFactory.Parameters.Add(p_destinacion);
                        cmdTransaccionFactory.Parameters.Add(p_reqpoliza);
                        cmdTransaccionFactory.Parameters.Add(p_forma_pago);
                        cmdTransaccionFactory.Parameters.Add(p_fecha_primer_pago);
                        cmdTransaccionFactory.Parameters.Add(p_comision);
                        cmdTransaccionFactory.Parameters.Add(p_aporte);
                        cmdTransaccionFactory.Parameters.Add(p_seguro);
                        cmdTransaccionFactory.Parameters.Add(P_Condicion_Especial);
                        cmdTransaccionFactory.Parameters.Add(p_mensajeerror);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APR_CREDI_APROM";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        string error = p_mensajeerror.Value != DBNull.Value ? p_mensajeerror.Value.ToString().Trim() : string.Empty;

                        if (!string.IsNullOrWhiteSpace(error))
                        {
                            throw new Exception(error);
                        }

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pEntidad, pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        dbConnectionFactory.CerrarConexion(connection);
                        return pEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoSolicitadoData", "AprobarCreditoModificando", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Aplaza el credito en la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad CreditoSolicitado</param>
        /// <returns>Entidad creada</returns>
        public CreditoSolicitado AplazarCredito(CreditoSolicitado pEntidad, Motivo motivo, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_login = cmdTransaccionFactory.CreateParameter();
                        p_login.ParameterName = pEntidad.Nombres; //"p_login";
                        p_login.ParameterName = "p_login";
                        p_login.Value = pEntidad.Nombres;  //"XPINNADM"; //p_login.Value = pUsuario.usuario;
                        //p_login.DbType = DbType.String;
                        //p_login.Size = 20;
                        p_login.Direction = ParameterDirection.Input;

                        DbParameter p_radicacion = cmdTransaccionFactory.CreateParameter();
                        p_radicacion.ParameterName = "p_radicacion";
                        p_radicacion.Value = pEntidad.NumeroCredito;
                        //p_radicacion.DbType = DbType.Int32;
                        //p_radicacion.Size = 38;
                        p_radicacion.Direction = ParameterDirection.Input;

                        DbParameter p_observaciones = cmdTransaccionFactory.CreateParameter();
                        p_observaciones.ParameterName = "p_observaciones";
                        p_observaciones.Value = (pEntidad.ObservacionesAprobacion);
                        //p_observaciones.DbType = DbType.String;
                        //p_observaciones.Size = 250;
                        p_observaciones.Direction = ParameterDirection.Input;

                        DbParameter p_motivo = cmdTransaccionFactory.CreateParameter();
                        p_motivo.ParameterName = "p_motivo";
                        p_motivo.Value = motivo.Codigo;
                        //p_motivo.DbType = DbType.Int32;
                        //p_motivo.Size = 8;
                        p_motivo.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(p_login);
                        cmdTransaccionFactory.Parameters.Add(p_radicacion);
                        cmdTransaccionFactory.Parameters.Add(p_observaciones);
                        cmdTransaccionFactory.Parameters.Add(p_motivo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APR_CREDI_APLAZ";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pEntidad, pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                        return pEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoSolicitadoData", "AplazarCredito", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Niega el credito en la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad CreditoSolicitado</param>
        /// <returns>Entidad creada</returns>
        public CreditoSolicitado NegarCredito(CreditoSolicitado pEntidad, Motivo motivo, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_login = cmdTransaccionFactory.CreateParameter();
                        p_login.ParameterName = "p_login";
                        p_login.Value = pEntidad.Nombres; //"XPINNADM"; //p_login.Value = pUsuario.usuario;
                        //p_login.DbType = DbType.String;
                        //p_login.Size = 20;
                        p_login.Direction = ParameterDirection.Input;

                        DbParameter p_radicacion = cmdTransaccionFactory.CreateParameter();
                        p_radicacion.ParameterName = "p_radicacion";
                        p_radicacion.Value = pEntidad.NumeroCredito;
                        //p_radicacion.DbType = DbType.Int32;
                        //p_radicacion.Size = 38;
                        p_radicacion.Direction = ParameterDirection.Input;

                        DbParameter p_observaciones = cmdTransaccionFactory.CreateParameter();
                        p_observaciones.ParameterName = "p_observaciones";
                        p_observaciones.Value = (pEntidad.ObservacionesAprobacion);
                        //p_observaciones.DbType = DbType.String;
                        //p_observaciones.Size = 250;
                        p_observaciones.Direction = ParameterDirection.Input;

                        DbParameter p_motivo = cmdTransaccionFactory.CreateParameter();
                        p_motivo.ParameterName = "p_motivo";
                        p_motivo.Value = motivo.Codigo;
                        //p_motivo.DbType = DbType.Int32;
                        //p_motivo.Size = 8;
                        p_motivo.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(p_login);
                        cmdTransaccionFactory.Parameters.Add(p_radicacion);
                        cmdTransaccionFactory.Parameters.Add(p_observaciones);
                        cmdTransaccionFactory.Parameters.Add(p_motivo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APR_CREDI_NEGAD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pEntidad, pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                        return pEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoSolicitadoData", "NegarCredito", ex);
                        return null;
                    }
                }
            }
        }



        /// <summary>
        /// Obtiene la lista de Aprobadores
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Aprobadores obtenidos</returns>
        public List<CreditoSolicitado> ListarEstadosCredito(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<CreditoSolicitado> lstCreditos = new List<CreditoSolicitado>();
            Configuracion conf = new Configuracion();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select estado as ListaId, Descripcion as ListaDescripcion from  estado_credito";


                        sql += "  Order by ListaDescripcion";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            CreditoSolicitado entidad = new CreditoSolicitado();
                            //Asociar todos los valores a la entidad
                            if (resultado["ListaId"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ListaId"]);
                            if (resultado["ListaDescripcion"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["ListaDescripcion"]);

                            lstCreditos.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCreditos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoSolicitadoData", "ListarEstadosCredito", ex);
                        return null;
                    }
                }
            }
        }


        public List<DescuentosCredito> ListarDescuentosCredito(DescuentosCredito pDescuentoscredito, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<DescuentosCredito> lstDescuentosCredito = new List<DescuentosCredito>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        //string sql = @"SELECT d.*, a.nombre FROM Descuentoscredito d LEFT JOIN atributos a ON d.cod_atr = a.cod_atr" + ObtenerFiltro(pDescuentoscredito, "d.") + " ORDER BY d.cod_atr ";
                        //  string sql = @"SELECT d.*, a.nombre,(Select x.modifica From descuentoslinea x where x.cod_atr = d.cod_atr  and x.modifica=1  and x.cod_linea_credito = + '" + pDescuentoscredito.cod_linea + "')" + " As modifica From descuentoscredito d left join atributos a on a.cod_atr=d.cod_atr left join descuentoslinea  x on  x.cod_atr = d.cod_atr where x.modifica=1" + "and d.numero_radicacion= " + pDescuentoscredito.numero_radicacion + " ORDER BY d.cod_atr ";
                        string sql = @"SELECT d.*, a.nombre, x.modifica FROM descuentoscredito d LEFT JOIN atributos a ON a.cod_atr = d.cod_atr "
                                   + " LEFT JOIN descuentoslinea x ON x.cod_linea_credito = + '" + pDescuentoscredito.cod_linea + "'"
                                   + " AND x.cod_atr = d.cod_atr   where x.modifica = 1 and  d.numero_radicacion = " + pDescuentoscredito.numero_radicacion + " ORDER BY d.cod_atr ";


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            DescuentosCredito entidad = new DescuentosCredito();
                            //  if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt32(resultado["NUMERO_RADICACION"]);
                            if (resultado["COD_ATR"] != DBNull.Value) entidad.cod_atr = Convert.ToInt32(resultado["COD_ATR"]);
                            if (resultado["TIPO_LIQUIDACION"] != DBNull.Value) entidad.tipo_liquidacion = Convert.ToInt32(resultado["TIPO_LIQUIDACION"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.val_atr = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["COBRA_MORA"] != DBNull.Value) entidad.cobra_mora = Convert.ToInt32(resultado["COBRA_MORA"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.numero_cuotas = Convert.ToDecimal(resultado["NUMERO_CUOTAS"]);
                            if (resultado["FORMA_DESCUENTO"] != DBNull.Value) entidad.forma_descuento = Convert.ToInt32(resultado["FORMA_DESCUENTO"]);
                            if (resultado["TIPO_IMPUESTO"] != DBNull.Value) entidad.tipo_impuesto = Convert.ToInt32(resultado["TIPO_IMPUESTO"]);
                            if (resultado["TIPO_DESCUENTO"] != DBNull.Value) entidad.tipo_descuento = Convert.ToInt32(resultado["TIPO_DESCUENTO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nom_atr = Convert.ToString(resultado["NOMBRE"]);
                            lstDescuentosCredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstDescuentosCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ListarDescuentosCredito", ex);
                        return null;
                    }
                }
            }
        }

        public CreditoSolicitado ConsultarCodigodelProceso(CreditoSolicitado pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            CreditoSolicitado entidad = new CreditoSolicitado();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        //Se comentarea el select inicial donde "c.estado = 'V'". El estado cambia de "S" a "V" cuando el credito pasa por "referenciacion"                        
                        string sql = @"Select * from tipoprocesos where descripcion = '" + pEntidad.estado + "'";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            //Asociar todos los valores a la entidad
                            if (resultado["codtipoproceso"] != DBNull.Value) entidad.Codigoproceso = Convert.ToInt64(resultado["codtipoproceso"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoSolicitadoData", "ConsultarCodigodelProceso", ex);
                        return null;
                    }
                }
            }
        }

        public DescuentosCredito modificardeduccionesCredito(DescuentosCredito entidad, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter PCOD_LINEA_CREDITO = cmdTransaccionFactory.CreateParameter();
                        PCOD_LINEA_CREDITO.ParameterName = "PRADICADO";
                        PCOD_LINEA_CREDITO.Value = entidad.numero_radicacion;

                        DbParameter PCOD_ATR = cmdTransaccionFactory.CreateParameter();
                        PCOD_ATR.ParameterName = "PCOD_ATR";
                        PCOD_ATR.Value = entidad.cod_atr;


                        DbParameter PTIPO_LIQUIDACION = cmdTransaccionFactory.CreateParameter();
                        PTIPO_LIQUIDACION.ParameterName = "PTIPO_LIQUIDACION";
                        PTIPO_LIQUIDACION.Value = entidad.tipo_liquidacion;
                        if (entidad.tipo_liquidacion == null || entidad.tipo_liquidacion == 0)
                            PTIPO_LIQUIDACION.Value = DBNull.Value;
                        else
                            PTIPO_LIQUIDACION.Value = entidad.tipo_liquidacion;



                        DbParameter PVALOR = cmdTransaccionFactory.CreateParameter();
                        PVALOR.ParameterName = "PVALOR";
                        PVALOR.Value = entidad.val_atr;


                        DbParameter PCOBRA_MORA = cmdTransaccionFactory.CreateParameter();
                        PCOBRA_MORA.ParameterName = "PCOBRA_MORA";
                        PCOBRA_MORA.Value = entidad.cobra_mora;

                        DbParameter PNUMERO_CUOTAS = cmdTransaccionFactory.CreateParameter();
                        PNUMERO_CUOTAS.ParameterName = "PNUMERO_CUOTAS";
                        if (entidad.numero_cuotas == null || entidad.numero_cuotas == 0)
                            PNUMERO_CUOTAS.Value = 0;
                        else
                            PNUMERO_CUOTAS.Value = entidad.numero_cuotas;

                        DbParameter PFORMA_DESCUENTO = cmdTransaccionFactory.CreateParameter();
                        PFORMA_DESCUENTO.ParameterName = "PFORMA_DESCUENTO";

                        if (entidad.forma_descuento == 0 || entidad.forma_descuento == null)
                            PFORMA_DESCUENTO.Value = 0;
                        else
                            PFORMA_DESCUENTO.Value = entidad.forma_descuento;


                        DbParameter PTIPO_IMPUESTO = cmdTransaccionFactory.CreateParameter();
                        PTIPO_IMPUESTO.ParameterName = "PTIPO_IMPUESTO";
                        if (entidad.tipo_impuesto == 0 || entidad.tipo_impuesto == null)
                            PTIPO_IMPUESTO.Value = 0;
                        else
                            PTIPO_IMPUESTO.Value = entidad.tipo_impuesto;



                        DbParameter PTIPO_DESCUENTO = cmdTransaccionFactory.CreateParameter();
                        PTIPO_DESCUENTO.ParameterName = "PTIPO_DESCUENTO";
                        if (entidad.tipo_descuento == null)
                            PTIPO_DESCUENTO.Value = 0;
                        else

                            PTIPO_DESCUENTO.Value = entidad.tipo_descuento;



                        cmdTransaccionFactory.Parameters.Add(PCOD_LINEA_CREDITO);
                        cmdTransaccionFactory.Parameters.Add(PCOD_ATR);
                        cmdTransaccionFactory.Parameters.Add(PTIPO_LIQUIDACION);
                        cmdTransaccionFactory.Parameters.Add(PVALOR);
                        cmdTransaccionFactory.Parameters.Add(PCOBRA_MORA);
                        cmdTransaccionFactory.Parameters.Add(PNUMERO_CUOTAS);
                        cmdTransaccionFactory.Parameters.Add(PFORMA_DESCUENTO);
                        cmdTransaccionFactory.Parameters.Add(PTIPO_IMPUESTO);
                        cmdTransaccionFactory.Parameters.Add(PTIPO_DESCUENTO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_DESCUENTOCR_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);


                    }

                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoSolicitadoData", "ModificarDescuentos", ex);

                    }
                    return entidad;
                }


            }
        }

        public CreditoSolicitado ConsultarParamAprobacion(CreditoSolicitado pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            CreditoSolicitado entidad = new CreditoSolicitado();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        //Se comentarea el select inicial donde "c.estado = 'V'". El estado cambia de "S" a "V" cuando el credito pasa por "referenciacion"                        
                        string sql = @"Select aprobar_avances from lineascredito where cod_linea_credito = " + pEntidad.cod_linea_credito;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            //Asociar todos los valores a la entidad
                            if (resultado["aprobar_avances"] != DBNull.Value) entidad.aprobar_avances = Convert.ToInt64(resultado["aprobar_avances"]);

                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoSolicitadoData", "ConsultarParamAprobacion", ex);
                        return null;
                    }
                }
            }
        }

        //Agregado para consultar proceso anterior
        public ControlTiempos ConsultarProcesoAnterior(string estado, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            ControlTiempos entidad = new ControlTiempos();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT T.CODTIPOPROCESO, T.DESCRIPCION, T.TIPO_PROCESO, (CASE WHEN T.TIPO_PROCESO = '1' THEN 'Automático' WHEN T.TIPO_PROCESO = '2' THEN 'Manual' END) as NOM_TIPO_PROCESO, T.ESTADO
                                  FROM TIPOPROCESOS T
                                  LEFT JOIN TIPOPROCESOS P ON T.CODTIPOPROCESO = P.CODPROANTECEDE
                                  WHERE P.DESCRIPCION = '" + estado + "'";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            if (resultado["CODTIPOPROCESO"] != DBNull.Value) entidad.cod_proceso = Convert.ToInt64(resultado["CODTIPOPROCESO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.nom_proceso = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["TIPO_PROCESO"] != DBNull.Value) entidad.tipo_proceso_anterior = Convert.ToInt64(resultado["TIPO_PROCESO"]);
                            if (resultado["NOM_TIPO_PROCESO"] != DBNull.Value) entidad.nom_proceso_anterior = Convert.ToString(resultado["NOM_TIPO_PROCESO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoSolicitadoData", "ConsultarCodigodelProceso", ex);
                        return null;
                    }
                }
            }
        }



        #region METODO DE ATENCION AL CLIENTE

        public SolicitudCreditoAAC CrearSolicitudCreditoAAC(SolicitudCreditoAAC pSolicitudCreditoAAC, Usuario vUsuario, Int32 pOpcion)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pnumerosolicitud = cmdTransaccionFactory.CreateParameter();
                        pnumerosolicitud.ParameterName = "p_numerosolicitud";
                        pnumerosolicitud.Value = pSolicitudCreditoAAC.numerosolicitud;
                        if (pOpcion == 1)
                            pnumerosolicitud.Direction = ParameterDirection.Output;
                        else
                            pnumerosolicitud.Direction = ParameterDirection.Input;
                        pnumerosolicitud.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumerosolicitud);

                        DbParameter pfechasolicitud = cmdTransaccionFactory.CreateParameter();
                        pfechasolicitud.ParameterName = "p_fechasolicitud";
                        pfechasolicitud.Value = pSolicitudCreditoAAC.fechasolicitud;
                        pfechasolicitud.Direction = ParameterDirection.Input;
                        pfechasolicitud.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechasolicitud);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        if (pSolicitudCreditoAAC.cod_persona == null || pSolicitudCreditoAAC.cod_persona == 0)
                            pcod_persona.Value = DBNull.Value;
                        else
                            pcod_persona.Value = pSolicitudCreditoAAC.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pmontosolicitado = cmdTransaccionFactory.CreateParameter();
                        pmontosolicitado.ParameterName = "p_montosolicitado";
                        if (pSolicitudCreditoAAC.montosolicitado == null)
                            pmontosolicitado.Value = DBNull.Value;
                        else
                            pmontosolicitado.Value = pSolicitudCreditoAAC.montosolicitado;
                        pmontosolicitado.Direction = ParameterDirection.Input;
                        pmontosolicitado.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pmontosolicitado);

                        DbParameter pplazosolicitado = cmdTransaccionFactory.CreateParameter();
                        pplazosolicitado.ParameterName = "p_plazosolicitado";
                        if (pSolicitudCreditoAAC.plazosolicitado == null)
                            pplazosolicitado.Value = DBNull.Value;
                        else
                            pplazosolicitado.Value = pSolicitudCreditoAAC.plazosolicitado;
                        pplazosolicitado.Direction = ParameterDirection.Input;
                        pplazosolicitado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pplazosolicitado);

                        DbParameter pcuotasolicitada = cmdTransaccionFactory.CreateParameter();
                        pcuotasolicitada.ParameterName = "p_cuotasolicitada";
                        if (pSolicitudCreditoAAC.cuotasolicitada == null || pSolicitudCreditoAAC.cuotasolicitada == 0)
                            pcuotasolicitada.Value = DBNull.Value;
                        else
                            pcuotasolicitada.Value = pSolicitudCreditoAAC.cuotasolicitada;
                        pcuotasolicitada.Direction = ParameterDirection.Input;
                        pcuotasolicitada.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pcuotasolicitada);

                        DbParameter ptipocredito = cmdTransaccionFactory.CreateParameter();
                        ptipocredito.ParameterName = "p_tipocredito";
                        if (pSolicitudCreditoAAC.tipocredito == null)
                            ptipocredito.Value = DBNull.Value;
                        else
                            ptipocredito.Value = pSolicitudCreditoAAC.tipocredito;
                        ptipocredito.Direction = ParameterDirection.Input;
                        ptipocredito.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipocredito);

                        DbParameter pperiodicidad = cmdTransaccionFactory.CreateParameter();
                        pperiodicidad.ParameterName = "p_periodicidad";
                        if (pSolicitudCreditoAAC.periodicidad == null)
                            pperiodicidad.Value = DBNull.Value;
                        else
                            pperiodicidad.Value = pSolicitudCreditoAAC.periodicidad;
                        pperiodicidad.Direction = ParameterDirection.Input;
                        pperiodicidad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pperiodicidad);

                        DbParameter pmedio = cmdTransaccionFactory.CreateParameter();
                        pmedio.ParameterName = "p_medio";
                        if (pSolicitudCreditoAAC.medio == null)
                            pmedio.Value = DBNull.Value;
                        else
                            pmedio.Value = pSolicitudCreditoAAC.medio;
                        pmedio.Direction = ParameterDirection.Input;
                        pmedio.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pmedio);

                        DbParameter preqpoliza = cmdTransaccionFactory.CreateParameter();
                        preqpoliza.ParameterName = "p_reqpoliza";
                        if (pSolicitudCreditoAAC.reqpoliza == null)
                            preqpoliza.Value = DBNull.Value;
                        else
                            preqpoliza.Value = pSolicitudCreditoAAC.reqpoliza;
                        preqpoliza.Direction = ParameterDirection.Input;
                        preqpoliza.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(preqpoliza);

                        DbParameter potromedio = cmdTransaccionFactory.CreateParameter();
                        potromedio.ParameterName = "p_otromedio";
                        if (pSolicitudCreditoAAC.otromedio == null)
                            potromedio.Value = DBNull.Value;
                        else
                            potromedio.Value = pSolicitudCreditoAAC.otromedio;
                        potromedio.Direction = ParameterDirection.Input;
                        potromedio.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(potromedio);

                        DbParameter pusuario = cmdTransaccionFactory.CreateParameter();
                        pusuario.ParameterName = "p_usuario";
                        if (pSolicitudCreditoAAC.usuario == null)
                            pusuario.Value = DBNull.Value;
                        else
                            pusuario.Value = pSolicitudCreditoAAC.usuario;
                        pusuario.Direction = ParameterDirection.Input;
                        pusuario.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pusuario);

                        DbParameter poficina = cmdTransaccionFactory.CreateParameter();
                        poficina.ParameterName = "p_oficina";
                        if (pSolicitudCreditoAAC.oficina == null)
                            poficina.Value = DBNull.Value;
                        else
                            poficina.Value = pSolicitudCreditoAAC.oficina;
                        poficina.Direction = ParameterDirection.Input;
                        poficina.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(poficina);

                        DbParameter pconcepto = cmdTransaccionFactory.CreateParameter();
                        pconcepto.ParameterName = "p_concepto";
                        if (pSolicitudCreditoAAC.concepto == null)
                            pconcepto.Value = DBNull.Value;
                        else
                            pconcepto.Value = pSolicitudCreditoAAC.concepto;
                        pconcepto.Direction = ParameterDirection.Input;
                        pconcepto.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pconcepto);

                        DbParameter pgarantia = cmdTransaccionFactory.CreateParameter();
                        pgarantia.ParameterName = "p_garantia";
                        if (pSolicitudCreditoAAC.garantia == null)
                            pgarantia.Value = DBNull.Value;
                        else
                            pgarantia.Value = pSolicitudCreditoAAC.garantia;
                        pgarantia.Direction = ParameterDirection.Input;
                        pgarantia.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pgarantia);

                        DbParameter pgarantia_comunitaria = cmdTransaccionFactory.CreateParameter();
                        pgarantia_comunitaria.ParameterName = "p_garantia_comunitaria";
                        if (pSolicitudCreditoAAC.garantia_comunitaria == null)
                            pgarantia_comunitaria.Value = DBNull.Value;
                        else
                            pgarantia_comunitaria.Value = pSolicitudCreditoAAC.garantia_comunitaria;
                        pgarantia_comunitaria.Direction = ParameterDirection.Input;
                        pgarantia_comunitaria.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pgarantia_comunitaria);

                        DbParameter ptipo_liquidacion = cmdTransaccionFactory.CreateParameter();
                        ptipo_liquidacion.ParameterName = "p_tipo_liquidacion";
                        if (pSolicitudCreditoAAC.tipo_liquidacion == null)
                            ptipo_liquidacion.Value = DBNull.Value;
                        else
                            ptipo_liquidacion.Value = pSolicitudCreditoAAC.tipo_liquidacion;
                        ptipo_liquidacion.Direction = ParameterDirection.Input;
                        ptipo_liquidacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_liquidacion);

                        DbParameter pforma_pago = cmdTransaccionFactory.CreateParameter();
                        pforma_pago.ParameterName = "p_forma_pago";
                        if (pSolicitudCreditoAAC.forma_pago == null)
                            pforma_pago.Value = DBNull.Value;
                        else
                            pforma_pago.Value = pSolicitudCreditoAAC.forma_pago;
                        pforma_pago.Direction = ParameterDirection.Input;
                        pforma_pago.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pforma_pago);

                        DbParameter pempresa_recaudo = cmdTransaccionFactory.CreateParameter();
                        pempresa_recaudo.ParameterName = "p_empresa_recaudo";
                        if (pSolicitudCreditoAAC.empresa_recaudo == null && pSolicitudCreditoAAC.empresa_recaudo == 0)
                            pempresa_recaudo.Value = DBNull.Value;
                        else
                            pempresa_recaudo.Value = pSolicitudCreditoAAC.empresa_recaudo;
                        pempresa_recaudo.Direction = ParameterDirection.Input;
                        pempresa_recaudo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pempresa_recaudo);

                        DbParameter pidproveedor = cmdTransaccionFactory.CreateParameter();
                        pidproveedor.ParameterName = "p_idproveedor";
                        if (pSolicitudCreditoAAC.idproveedor == null)
                            pidproveedor.Value = DBNull.Value;
                        else
                            pidproveedor.Value = pSolicitudCreditoAAC.idproveedor;
                        pidproveedor.Direction = ParameterDirection.Input;
                        pidproveedor.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidproveedor);

                        DbParameter pnomproveedor = cmdTransaccionFactory.CreateParameter();
                        pnomproveedor.ParameterName = "p_nomproveedor";
                        if (pSolicitudCreditoAAC.nomproveedor == null)
                            pnomproveedor.Value = DBNull.Value;
                        else
                            pnomproveedor.Value = pSolicitudCreditoAAC.nomproveedor;
                        pnomproveedor.Direction = ParameterDirection.Input;
                        pnomproveedor.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnomproveedor);

                        DbParameter pdestino = cmdTransaccionFactory.CreateParameter();
                        pdestino.ParameterName = "p_destino";
                        if (pSolicitudCreditoAAC.destino == null)
                            pdestino.Value = DBNull.Value;
                        else
                            pdestino.Value = pSolicitudCreditoAAC.destino;
                        pdestino.Direction = ParameterDirection.Input;
                        pdestino.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pdestino);

                        DbParameter pid_persona = cmdTransaccionFactory.CreateParameter();
                        pid_persona.ParameterName = "p_id_persona";
                        if (pSolicitudCreditoAAC.id_persona == null || pSolicitudCreditoAAC.id_persona == 0)
                            pid_persona.Value = DBNull.Value;
                        else
                            pid_persona.Value = pSolicitudCreditoAAC.id_persona;
                        pid_persona.Direction = ParameterDirection.Input;
                        pid_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pid_persona);

                        DbParameter ptipo_cuenta = cmdTransaccionFactory.CreateParameter();
                        ptipo_cuenta.ParameterName = "p_tipo_cuenta";
                        if (pSolicitudCreditoAAC.tipo_cuenta == null)
                            ptipo_cuenta.Value = DBNull.Value;
                        else
                            ptipo_cuenta.Value = pSolicitudCreditoAAC.tipo_cuenta;
                        ptipo_cuenta.Direction = ParameterDirection.Input;
                        ptipo_cuenta.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_cuenta);

                        DbParameter pnum_cuenta = cmdTransaccionFactory.CreateParameter();
                        pnum_cuenta.ParameterName = "p_num_cuenta";
                        if (pSolicitudCreditoAAC.num_cuenta == null)
                            pnum_cuenta.Value = DBNull.Value;
                        else
                            pnum_cuenta.Value = pSolicitudCreditoAAC.num_cuenta;
                        pnum_cuenta.Direction = ParameterDirection.Input;
                        pnum_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnum_cuenta);

                        DbParameter pcod_banco = cmdTransaccionFactory.CreateParameter();
                        pcod_banco.ParameterName = "p_cod_banco";
                        if (pSolicitudCreditoAAC.cod_banco == null && pSolicitudCreditoAAC.cod_banco == 0)
                            pcod_banco.Value = DBNull.Value;
                        else
                            pcod_banco.Value = pSolicitudCreditoAAC.cod_banco;
                        pcod_banco.Direction = ParameterDirection.Input;
                        pcod_banco.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_banco);

                        DbParameter ptipovivienda = cmdTransaccionFactory.CreateParameter();
                        ptipovivienda.ParameterName = "p_tipovivienda";
                        if (pSolicitudCreditoAAC.tipovivienda == null)
                            ptipovivienda.Value = DBNull.Value;
                        else
                            ptipovivienda.Value = pSolicitudCreditoAAC.tipovivienda;
                        ptipovivienda.Direction = ParameterDirection.Input;
                        ptipovivienda.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipovivienda);

                        DbParameter parrendatario = cmdTransaccionFactory.CreateParameter();
                        parrendatario.ParameterName = "p_arrendatario";
                        if (pSolicitudCreditoAAC.arrendatario == null)
                            parrendatario.Value = DBNull.Value;
                        else
                            parrendatario.Value = pSolicitudCreditoAAC.arrendatario;
                        parrendatario.Direction = ParameterDirection.Input;
                        parrendatario.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(parrendatario);

                        DbParameter ptelef_arrendatario = cmdTransaccionFactory.CreateParameter();
                        ptelef_arrendatario.ParameterName = "p_telef_arrendatario";
                        if (pSolicitudCreditoAAC.telef_arrendatario == null)
                            ptelef_arrendatario.Value = DBNull.Value;
                        else
                            ptelef_arrendatario.Value = pSolicitudCreditoAAC.telef_arrendatario;
                        ptelef_arrendatario.Direction = ParameterDirection.Input;
                        ptelef_arrendatario.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptelef_arrendatario);

                        DbParameter ptipo_propiedad = cmdTransaccionFactory.CreateParameter();
                        ptipo_propiedad.ParameterName = "p_tipo_propiedad";
                        if (pSolicitudCreditoAAC.tipo_propiedad == null)
                            ptipo_propiedad.Value = DBNull.Value;
                        else
                            ptipo_propiedad.Value = pSolicitudCreditoAAC.tipo_propiedad;
                        ptipo_propiedad.Direction = ParameterDirection.Input;
                        ptipo_propiedad.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipo_propiedad);

                        DbParameter potro_propiedad = cmdTransaccionFactory.CreateParameter();
                        potro_propiedad.ParameterName = "p_otro_propiedad";
                        if (pSolicitudCreditoAAC.otro_propiedad == null)
                            potro_propiedad.Value = DBNull.Value;
                        else
                            potro_propiedad.Value = pSolicitudCreditoAAC.otro_propiedad;
                        potro_propiedad.Direction = ParameterDirection.Input;
                        potro_propiedad.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(potro_propiedad);

                        DbParameter pdirec_propiedad = cmdTransaccionFactory.CreateParameter();
                        pdirec_propiedad.ParameterName = "p_direc_propiedad";
                        if (pSolicitudCreditoAAC.direc_propiedad == null)
                            pdirec_propiedad.Value = DBNull.Value;
                        else
                            pdirec_propiedad.Value = pSolicitudCreditoAAC.direc_propiedad;
                        pdirec_propiedad.Direction = ParameterDirection.Input;
                        pdirec_propiedad.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdirec_propiedad);

                        DbParameter pcodciudad_propiedad = cmdTransaccionFactory.CreateParameter();
                        pcodciudad_propiedad.ParameterName = "p_codciudad_propiedad";
                        if (pSolicitudCreditoAAC.codciudad_propiedad == null)
                            pcodciudad_propiedad.Value = DBNull.Value;
                        else
                            pcodciudad_propiedad.Value = pSolicitudCreditoAAC.codciudad_propiedad;
                        pcodciudad_propiedad.Direction = ParameterDirection.Input;
                        pcodciudad_propiedad.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodciudad_propiedad);

                        DbParameter pescritura_propiedad = cmdTransaccionFactory.CreateParameter();
                        pescritura_propiedad.ParameterName = "p_escritura_propiedad";
                        if (pSolicitudCreditoAAC.escritura_propiedad == null)
                            pescritura_propiedad.Value = DBNull.Value;
                        else
                            pescritura_propiedad.Value = pSolicitudCreditoAAC.escritura_propiedad;
                        pescritura_propiedad.Direction = ParameterDirection.Input;
                        pescritura_propiedad.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pescritura_propiedad);

                        DbParameter pnotaria = cmdTransaccionFactory.CreateParameter();
                        pnotaria.ParameterName = "p_notaria";
                        if (pSolicitudCreditoAAC.notaria == null)
                            pnotaria.Value = DBNull.Value;
                        else
                            pnotaria.Value = pSolicitudCreditoAAC.notaria;
                        pnotaria.Direction = ParameterDirection.Input;
                        pnotaria.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnotaria);

                        DbParameter pmaneja_hipoteca = cmdTransaccionFactory.CreateParameter();
                        pmaneja_hipoteca.ParameterName = "p_maneja_hipoteca";
                        if (pSolicitudCreditoAAC.maneja_hipoteca == null)
                            pmaneja_hipoteca.Value = DBNull.Value;
                        else
                            pmaneja_hipoteca.Value = pSolicitudCreditoAAC.maneja_hipoteca;
                        pmaneja_hipoteca.Direction = ParameterDirection.Input;
                        pmaneja_hipoteca.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pmaneja_hipoteca);

                        DbParameter pvalor_hipoteca = cmdTransaccionFactory.CreateParameter();
                        pvalor_hipoteca.ParameterName = "p_valor_hipoteca";
                        if (pSolicitudCreditoAAC.valor_hipoteca == null)
                            pvalor_hipoteca.Value = DBNull.Value;
                        else
                            pvalor_hipoteca.Value = pSolicitudCreditoAAC.valor_hipoteca;
                        pvalor_hipoteca.Direction = ParameterDirection.Input;
                        pvalor_hipoteca.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_hipoteca);

                        DbParameter pmatricula_inmov = cmdTransaccionFactory.CreateParameter();
                        pmatricula_inmov.ParameterName = "p_matricula_inmov";
                        if (pSolicitudCreditoAAC.matricula_inmov == null)
                            pmatricula_inmov.Value = DBNull.Value;
                        else
                            pmatricula_inmov.Value = pSolicitudCreditoAAC.matricula_inmov;
                        pmatricula_inmov.Direction = ParameterDirection.Input;
                        pmatricula_inmov.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pmatricula_inmov);

                        DbParameter pvr_propiedad_viv = cmdTransaccionFactory.CreateParameter();
                        pvr_propiedad_viv.ParameterName = "p_vr_propiedad_viv";
                        if (pSolicitudCreditoAAC.vr_propiedad_viv == null)
                            pvr_propiedad_viv.Value = DBNull.Value;
                        else
                            pvr_propiedad_viv.Value = pSolicitudCreditoAAC.vr_propiedad_viv;
                        pvr_propiedad_viv.Direction = ParameterDirection.Input;
                        pvr_propiedad_viv.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvr_propiedad_viv);

                        DbParameter pvr_arriendo_cuota = cmdTransaccionFactory.CreateParameter();
                        pvr_arriendo_cuota.ParameterName = "p_vr_arriendo_cuota";
                        if (pSolicitudCreditoAAC.vr_arriendo_cuota == null)
                            pvr_arriendo_cuota.Value = DBNull.Value;
                        else
                            pvr_arriendo_cuota.Value = pSolicitudCreditoAAC.vr_arriendo_cuota;
                        pvr_arriendo_cuota.Direction = ParameterDirection.Input;
                        pvr_arriendo_cuota.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvr_arriendo_cuota);

                        DbParameter pvr_gastos = cmdTransaccionFactory.CreateParameter();
                        pvr_gastos.ParameterName = "p_vr_gastos";
                        if (pSolicitudCreditoAAC.vr_gastos == null)
                            pvr_gastos.Value = DBNull.Value;
                        else
                            pvr_gastos.Value = pSolicitudCreditoAAC.vr_gastos;
                        pvr_gastos.Direction = ParameterDirection.Input;
                        pvr_gastos.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvr_gastos);

                        DbParameter pvr_otrosgastos = cmdTransaccionFactory.CreateParameter();
                        pvr_otrosgastos.ParameterName = "p_vr_otrosgastos";
                        if (pSolicitudCreditoAAC.vr_otrosgastos == null)
                            pvr_otrosgastos.Value = DBNull.Value;
                        else
                            pvr_otrosgastos.Value = pSolicitudCreditoAAC.vr_otrosgastos;
                        pvr_otrosgastos.Direction = ParameterDirection.Input;
                        pvr_otrosgastos.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvr_otrosgastos);

                        DbParameter pmarca_modelo = cmdTransaccionFactory.CreateParameter();
                        pmarca_modelo.ParameterName = "p_marca_modelo";
                        if (pSolicitudCreditoAAC.marca_modelo == null)
                            pmarca_modelo.Value = DBNull.Value;
                        else
                            pmarca_modelo.Value = pSolicitudCreditoAAC.marca_modelo;
                        pmarca_modelo.Direction = ParameterDirection.Input;
                        pmarca_modelo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pmarca_modelo);

                        DbParameter pvr_comercial_vehi = cmdTransaccionFactory.CreateParameter();
                        pvr_comercial_vehi.ParameterName = "p_vr_comercial_vehi";
                        if (pSolicitudCreditoAAC.vr_comercial_vehi == null)
                            pvr_comercial_vehi.Value = DBNull.Value;
                        else
                            pvr_comercial_vehi.Value = pSolicitudCreditoAAC.vr_comercial_vehi;
                        pvr_comercial_vehi.Direction = ParameterDirection.Input;
                        pvr_comercial_vehi.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pvr_comercial_vehi);

                        DbParameter ppignorado_vehi = cmdTransaccionFactory.CreateParameter();
                        ppignorado_vehi.ParameterName = "p_pignorado_vehi";
                        if (pSolicitudCreditoAAC.pignorado_vehi == null)
                            ppignorado_vehi.Value = DBNull.Value;
                        else
                            ppignorado_vehi.Value = pSolicitudCreditoAAC.pignorado_vehi;
                        ppignorado_vehi.Direction = ParameterDirection.Input;
                        ppignorado_vehi.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ppignorado_vehi);

                        DbParameter pvr_pignorado_vehi = cmdTransaccionFactory.CreateParameter();
                        pvr_pignorado_vehi.ParameterName = "p_vr_pignorado_vehi";
                        if (pSolicitudCreditoAAC.vr_pignorado_vehi == null)
                            pvr_pignorado_vehi.Value = DBNull.Value;
                        else
                            pvr_pignorado_vehi.Value = pSolicitudCreditoAAC.vr_pignorado_vehi;
                        pvr_pignorado_vehi.Direction = ParameterDirection.Input;
                        pvr_pignorado_vehi.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvr_pignorado_vehi);

                        DbParameter pfecha_vencimiento = cmdTransaccionFactory.CreateParameter();
                        pfecha_vencimiento.ParameterName = "p_fecha_vencimiento";
                        if (pSolicitudCreditoAAC.fecha_vencimiento == null)
                            pfecha_vencimiento.Value = DBNull.Value;
                        else
                            pfecha_vencimiento.Value = pSolicitudCreditoAAC.fecha_vencimiento;
                        pfecha_vencimiento.Direction = ParameterDirection.Input;
                        pfecha_vencimiento.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_vencimiento);

                        DbParameter psaldo = cmdTransaccionFactory.CreateParameter();
                        psaldo.ParameterName = "p_saldo";
                        if (pSolicitudCreditoAAC.saldo == null)
                            psaldo.Value = DBNull.Value;
                        else
                            psaldo.Value = pSolicitudCreditoAAC.saldo;
                        psaldo.Direction = ParameterDirection.Input;
                        psaldo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(psaldo);

                        DbParameter pvr_cuota = cmdTransaccionFactory.CreateParameter();
                        pvr_cuota.ParameterName = "p_vr_cuota";
                        if (pSolicitudCreditoAAC.vr_cuota == null)
                            pvr_cuota.Value = DBNull.Value;
                        else
                            pvr_cuota.Value = pSolicitudCreditoAAC.vr_cuota;
                        pvr_cuota.Direction = ParameterDirection.Input;
                        pvr_cuota.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvr_cuota);

                        DbParameter pempresa_recaudo_seg = cmdTransaccionFactory.CreateParameter();
                        pempresa_recaudo_seg.ParameterName = "p_empresa_recaudo_seg";
                        if (pSolicitudCreditoAAC.empresa_recaudo_seg == null && pSolicitudCreditoAAC.empresa_recaudo_seg == 0)
                            pempresa_recaudo_seg.Value = DBNull.Value;
                        else
                            pempresa_recaudo_seg.Value = pSolicitudCreditoAAC.empresa_recaudo_seg;
                        pempresa_recaudo_seg.Direction = ParameterDirection.Input;
                        pempresa_recaudo_seg.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pempresa_recaudo_seg);

                        DbParameter pfecha_vencimiento_seg = cmdTransaccionFactory.CreateParameter();
                        pfecha_vencimiento_seg.ParameterName = "p_fecha_vencimiento_seg";
                        if (pSolicitudCreditoAAC.fecha_vencimiento_seg == null)
                            pfecha_vencimiento_seg.Value = DBNull.Value;
                        else
                            pfecha_vencimiento_seg.Value = pSolicitudCreditoAAC.fecha_vencimiento_seg;
                        pfecha_vencimiento_seg.Direction = ParameterDirection.Input;
                        pfecha_vencimiento_seg.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_vencimiento_seg);

                        DbParameter psaldo_seg = cmdTransaccionFactory.CreateParameter();
                        psaldo_seg.ParameterName = "p_saldo_seg";
                        if (pSolicitudCreditoAAC.saldo_seg == null)
                            psaldo_seg.Value = DBNull.Value;
                        else
                            psaldo_seg.Value = pSolicitudCreditoAAC.saldo_seg;
                        psaldo_seg.Direction = ParameterDirection.Input;
                        psaldo_seg.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(psaldo_seg);

                        DbParameter pvr_cuota_seg = cmdTransaccionFactory.CreateParameter();
                        pvr_cuota_seg.ParameterName = "p_vr_cuota_seg";
                        if (pSolicitudCreditoAAC.vr_cuota_seg == null)
                            pvr_cuota_seg.Value = DBNull.Value;
                        else
                            pvr_cuota_seg.Value = pSolicitudCreditoAAC.vr_cuota_seg;
                        pvr_cuota_seg.Direction = ParameterDirection.Input;
                        pvr_cuota_seg.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvr_cuota_seg);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pSolicitudCreditoAAC.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter preqafiancol = cmdTransaccionFactory.CreateParameter();
                        preqafiancol.ParameterName = "P_REQAFIANCOL";
                        preqafiancol.Value = pSolicitudCreditoAAC.afiancol;
                        preqafiancol.Direction = ParameterDirection.Input;
                        preqafiancol.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(preqafiancol);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        if (pOpcion == 1) //CREAR
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_SOLICITUDC_CREAR";
                        else
                            cmdTransaccionFactory.CommandText = "";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        if (pOpcion == 1)
                            pSolicitudCreditoAAC.numerosolicitud = Convert.ToInt64(pnumerosolicitud.Value);
                        return pSolicitudCreditoAAC;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudCreditoAACData", "CrearSolicitudCreditoAAC", ex);
                        return null;
                    }
                }
            }
        }

        public SolicitudCreditoAAC CrearSolicitudCreditoProveedor(SolicitudCreditoAAC pSolicitudCreditoAAC, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pnumerosolicitud = cmdTransaccionFactory.CreateParameter();
                        pnumerosolicitud.ParameterName = "p_numerosolicitud";
                        pnumerosolicitud.Value = pSolicitudCreditoAAC.numerosolicitud;
                        pnumerosolicitud.Direction = ParameterDirection.Output;
                        pnumerosolicitud.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumerosolicitud);

                        DbParameter pfechasolicitud = cmdTransaccionFactory.CreateParameter();
                        pfechasolicitud.ParameterName = "p_fechasolicitud";
                        pfechasolicitud.Value = pSolicitudCreditoAAC.fechasolicitud;
                        pfechasolicitud.Direction = ParameterDirection.Input;
                        pfechasolicitud.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechasolicitud);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        if (pSolicitudCreditoAAC.cod_persona == null || pSolicitudCreditoAAC.cod_persona == 0)
                            pcod_persona.Value = DBNull.Value;
                        else
                            pcod_persona.Value = pSolicitudCreditoAAC.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pmontosolicitado = cmdTransaccionFactory.CreateParameter();
                        pmontosolicitado.ParameterName = "p_montosolicitado";
                        if (pSolicitudCreditoAAC.montosolicitado == null)
                            pmontosolicitado.Value = DBNull.Value;
                        else
                            pmontosolicitado.Value = pSolicitudCreditoAAC.montosolicitado;
                        pmontosolicitado.Direction = ParameterDirection.Input;
                        pmontosolicitado.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pmontosolicitado);

                        DbParameter pplazosolicitado = cmdTransaccionFactory.CreateParameter();
                        pplazosolicitado.ParameterName = "p_plazosolicitado";
                        if (pSolicitudCreditoAAC.plazosolicitado == null)
                            pplazosolicitado.Value = DBNull.Value;
                        else
                            pplazosolicitado.Value = pSolicitudCreditoAAC.plazosolicitado;
                        pplazosolicitado.Direction = ParameterDirection.Input;
                        pplazosolicitado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pplazosolicitado);

                        DbParameter pcuotasolicitada = cmdTransaccionFactory.CreateParameter();
                        pcuotasolicitada.ParameterName = "p_cuotasolicitada";
                        if (pSolicitudCreditoAAC.cuotasolicitada == null || pSolicitudCreditoAAC.cuotasolicitada == 0)
                            pcuotasolicitada.Value = DBNull.Value;
                        else
                            pcuotasolicitada.Value = pSolicitudCreditoAAC.cuotasolicitada;
                        pcuotasolicitada.Direction = ParameterDirection.Input;
                        pcuotasolicitada.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pcuotasolicitada);

                        DbParameter ptipocredito = cmdTransaccionFactory.CreateParameter();
                        ptipocredito.ParameterName = "p_tipocredito";
                        if (pSolicitudCreditoAAC.tipocredito == null)
                            ptipocredito.Value = DBNull.Value;
                        else
                            ptipocredito.Value = pSolicitudCreditoAAC.tipocredito;
                        ptipocredito.Direction = ParameterDirection.Input;
                        ptipocredito.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipocredito);

                        DbParameter pperiodicidad = cmdTransaccionFactory.CreateParameter();
                        pperiodicidad.ParameterName = "p_periodicidad";
                        if (pSolicitudCreditoAAC.periodicidad == null)
                            pperiodicidad.Value = DBNull.Value;
                        else
                            pperiodicidad.Value = pSolicitudCreditoAAC.periodicidad;
                        pperiodicidad.Direction = ParameterDirection.Input;
                        pperiodicidad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pperiodicidad);

                        DbParameter pconcepto = cmdTransaccionFactory.CreateParameter();
                        pconcepto.ParameterName = "p_concepto";
                        if (pSolicitudCreditoAAC.concepto == null)
                            pconcepto.Value = DBNull.Value;
                        else
                            pconcepto.Value = pSolicitudCreditoAAC.concepto;
                        pconcepto.Direction = ParameterDirection.Input;
                        pconcepto.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pconcepto);

                        DbParameter pforma_pago = cmdTransaccionFactory.CreateParameter();
                        pforma_pago.ParameterName = "p_forma_pago";
                        if (pSolicitudCreditoAAC.forma_pago == null)
                            pforma_pago.Value = DBNull.Value;
                        else
                            pforma_pago.Value = pSolicitudCreditoAAC.forma_pago;
                        pforma_pago.Direction = ParameterDirection.Input;
                        pforma_pago.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pforma_pago);

                        DbParameter pdestino = cmdTransaccionFactory.CreateParameter();
                        pdestino.ParameterName = "p_destino";
                        if (pSolicitudCreditoAAC.destino == null)
                            pdestino.Value = DBNull.Value;
                        else
                            pdestino.Value = pSolicitudCreditoAAC.destino;
                        pdestino.Direction = ParameterDirection.Input;
                        pdestino.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pdestino);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pSolicitudCreditoAAC.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pusuario = cmdTransaccionFactory.CreateParameter();
                        pusuario.ParameterName = "P_USUARIO";
                        pusuario.Value = pSolicitudCreditoAAC.afiancol;
                        pusuario.Direction = ParameterDirection.Input;
                        pusuario.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pusuario);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_SOLICREPRO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        pSolicitudCreditoAAC.numerosolicitud = Convert.ToInt64(pnumerosolicitud.Value);
                        return pSolicitudCreditoAAC;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoSolicitadoData", "CrearSolicitudCreditoproveedor", ex);
                        return null;
                    }
                }
            }
        }

        public Int64 ConfirmarSolicitudCreditoAutomatico(SolicitudCreditoAAC pSolicitud, Usuario vUsuario)
        {
            Int64 pNumero_Radicacion = 0;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pnumerosolicitud = cmdTransaccionFactory.CreateParameter();
                        pnumerosolicitud.ParameterName = "p_numerosolicitud";
                        pnumerosolicitud.Value = pSolicitud.numerosolicitud;
                        pnumerosolicitud.Direction = ParameterDirection.Input;
                        pnumerosolicitud.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumerosolicitud);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pSolicitud.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pusuario = cmdTransaccionFactory.CreateParameter();
                        pusuario.ParameterName = "p_usuario";
                        pusuario.Value = vUsuario.codusuario;
                        pusuario.Direction = ParameterDirection.Input;
                        pusuario.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pusuario);

                        DbParameter pnumero_radicacion = cmdTransaccionFactory.CreateParameter();
                        pnumero_radicacion.ParameterName = "p_numero_radicacion";
                        pnumero_radicacion.Value = 0;
                        pnumero_radicacion.Direction = ParameterDirection.Output;
                        pnumero_radicacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_radicacion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_CONFIRSOLICITUD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        pNumero_Radicacion = Convert.ToInt64(pnumero_radicacion.Value);
                        return pNumero_Radicacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudCreditoAACData", "ConfirmarSolicitudCreditoAutomatico", ex);
                        return 0;
                    }
                }
            }
        }
        #endregion

        public List<AtributosCredito> ListarAtributosCredito(AtributosCredito pAtributosCredito, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<AtributosCredito> lstAtributosCredito = new List<AtributosCredito>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT d.*, a.nombre FROM AtributosCredito d LEFT JOIN atributos a ON a.cod_atr = d.cod_atr " +
                                      " WHERE d.numero_radicacion = " + pAtributosCredito.numero_radicacion + " ORDER BY d.cod_atr ";


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            AtributosCredito entidad = new AtributosCredito();
                            //  if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt32(resultado["NUMERO_RADICACION"]);
                            if (resultado["COD_ATR"] != DBNull.Value) entidad.cod_atr = Convert.ToInt32(resultado["COD_ATR"]);
                            if (resultado["CALCULO_ATR"] != DBNull.Value) entidad.calculo_atr = Convert.ToString(resultado["CALCULO_ATR"]);
                            if (resultado["TIPO_HISTORICO"] != DBNull.Value) entidad.tipo_historico = Convert.ToInt32(resultado["TIPO_HISTORICO"]);
                            if (resultado["DESVIACION"] != DBNull.Value) entidad.desviacion = Convert.ToDecimal(resultado["DESVIACION"]);
                            if (resultado["TIPO_TASA"] != DBNull.Value) entidad.tipo_tasa = Convert.ToInt32(resultado["TIPO_TASA"]);
                            if (resultado["TASA"] != DBNull.Value) entidad.tasa = Convert.ToDecimal(resultado["TASA"]);
                            if (resultado["COBRA_MORA"] != DBNull.Value) entidad.cobra_mora = Convert.ToInt32(resultado["COBRA_MORA"]);
                            if (resultado["SALDO_ATRIBUTO"] != DBNull.Value) entidad.saldo_atributo = Convert.ToDecimal(resultado["SALDO_ATRIBUTO"]);
                            if (resultado["CAUSADO_ATRIBUTO"] != DBNull.Value) entidad.causado_atributo = Convert.ToDecimal(resultado["CAUSADO_ATRIBUTO"]);
                            if (resultado["ORDEN_ATRIBUTO"] != DBNull.Value) entidad.orden_atributo = Convert.ToDecimal(resultado["ORDEN_ATRIBUTO"]);
                            lstAtributosCredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAtributosCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ListarAtributosCredito", ex);
                        return null;
                    }
                }
            }
        }
        public List<FabricaCreditos.Entities.Imagenes> ListarDocumentosAnexos(FabricaCreditos.Entities.Imagenes pDoc, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<FabricaCreditos.Entities.Imagenes> lstAtributosCredito = new List<FabricaCreditos.Entities.Imagenes>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT d.*,t.descripcion as doc FROM documentosanexos d JOIN tiposdocumento t on d.tipo_documento=t.tipo_documento where numero_radicacion = " + pDoc.numero_radiacion;


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Entities.Imagenes entidad = new Entities.Imagenes();
                            //  if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt32(resultado["NUMERO_RADICACION"]);
                            if (resultado["IDDOCUMENTO"] != DBNull.Value) entidad.idimagen = Convert.ToInt32(resultado["IDDOCUMENTO"]);
                            if (resultado["doc"] != DBNull.Value) entidad.tipodocumento = Convert.ToString(resultado["doc"]);
                            if (resultado["FECHAANEXO"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHAANEXO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);

                            lstAtributosCredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAtributosCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ListarAtributosCredito", ex);
                        return null;
                    }
                }
            }
        }

    }
}
