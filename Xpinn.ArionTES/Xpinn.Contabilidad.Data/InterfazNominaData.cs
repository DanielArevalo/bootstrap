using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.Contabilidad.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla ConceptoS
    /// </summary>
    public class InterfazNominaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla ConceptoS
        /// </summary>
        public InterfazNominaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Listar los pagos detallados por concepto de un período de nomina
        /// </summary>
        /// <param name="piden_periodo"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public List<InterfazNomina> ListarNomina(Int64 piden_periodo, string pFiltro, Usuario pUsuario)
        {
            return ListarNomina(piden_periodo, "", pFiltro, "", pUsuario);
        }

        public List<InterfazNomina> ListarNomina(Int64 piden_periodo, String pidentificacion, string pFiltro, string pTipo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<InterfazNomina> lstNomina = new List<InterfazNomina>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexionInterfazNomina(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select d.iden_empleado, e.identificacion, e.nombre1, e.apellido1,
                                        c.nombre, b.iden, b.iden_concepto, b.total, b.totalempleador, b.baseingreso, b.valorcuotaprestamo, 
                                        b.iden_pago, b.fechacreacion, c.tercero, d.centrocosto, d.FormaPago,
                                        (Select x.tercero From nm_fondosalud x Where x.iden = d.iden_fsalud) As iden_fsalud, 
                                        (Select x.tercero From nm_fondopension x Where x.iden = d.iden_fpension) As iden_fpension, 
                                        (Select x.tercero From nm_fondopension x Where x.iden = d.iden_fpensionvoluntaria) As iden_fpensionvoluntaria, 
                                        (Select x.tercero From nm_fondopension x Where x.iden = d.iden_fsolidaridad) As iden_fsolidaridad
                                        From nm_devengosdctosperiodo a
                                        Inner Join nm_devengosdctosconceptos b On a.iden = b.iden_devengodcto
                                        Inner Join nm_concepto c On b.iden_concepto = c.id
                                        Inner Join nm_contrato d On a.iden_contrato = d.iden
                                        Inner JOin nm_empleado e On e.iden = d.iden_empleado            
                                        Where a.iden_periodo = " + piden_periodo.ToString() + (pTipo == "T" ? "" : " And c.codigo Not In ('DV28', 'DV33', 'DV34', 'DV35') ") + @" And (b.total != 0 Or b.totalempleador != 0)";
                        if (pidentificacion.Trim() != "")
                            sql += " And e.identificacion = '" + pidentificacion + "' ";
                        if (pFiltro.Trim() != "")
                            sql += pFiltro;
                        sql += " Order by d.centrocosto, e.apellido1, b.iden_pago, b.iden_concepto";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            string concepto = "";
                            string identificacion = "";
                            if (resultado["iden_concepto"] != DBNull.Value) concepto = Convert.ToString(resultado["iden_concepto"]);
                            if (resultado["identificacion"] != DBNull.Value) identificacion = Convert.ToString(resultado["identificacion"]);
                            //if (concepto.Trim() == "43")
                            //{
                            //    // Contabilización de la provisión de vacaciones
                            //    List<InterfazNomina> lstVacaciones = new List<InterfazNomina>();
                            //    lstVacaciones = ListarNominaVacaciones(piden_periodo, identificacion, pUsuario);
                            //    if (lstVacaciones != null)
                            //    {
                            //        foreach (InterfazNomina item in lstVacaciones)
                            //        {
                            //            lstNomina.Add(item);
                            //        }
                            //    }
                            //}
                            //else
                            //{
                                // Contabilización de la nomina de empleados
                                InterfazNomina entidad = new InterfazNomina();

                                if (resultado["iden_empleado"] != DBNull.Value) entidad.iden_empleado = Convert.ToString(resultado["iden_empleado"]);
                                if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                                if (resultado["nombre1"] != DBNull.Value) entidad.nombre1 = Convert.ToString(resultado["nombre1"]);
                                if (resultado["apellido1"] != DBNull.Value) entidad.apellido1 = Convert.ToString(resultado["apellido1"]);
                                if (resultado["nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nombre"]);
                                if (resultado["iden"] != DBNull.Value) entidad.iden = Convert.ToString(resultado["iden"]);
                                if (resultado["iden_concepto"] != DBNull.Value) entidad.iden_concepto = Convert.ToString(resultado["iden_concepto"]);
                                if (resultado["total"] != DBNull.Value) entidad.total = Convert.ToDecimal(resultado["total"]);
                                if (resultado["totalempleador"] != DBNull.Value) entidad.totalempleador = Convert.ToDecimal(resultado["totalempleador"]);
                                if (resultado["baseingreso"] != DBNull.Value) entidad.baseingreso = Convert.ToDecimal(resultado["baseingreso"]);
                                if (resultado["valorcuotaprestamo"] != DBNull.Value) entidad.valorcuotaprestamo = Convert.ToDecimal(resultado["valorcuotaprestamo"]);
                                if (resultado["iden_pago"] != DBNull.Value) entidad.iden_pago = Convert.ToString(resultado["iden_pago"]);
                                if (resultado["fechacreacion"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["fechacreacion"]);
                                if (resultado["tercero"] != DBNull.Value) entidad.tercero = Convert.ToString(resultado["tercero"]);
                                if (resultado["centrocosto"] != DBNull.Value) entidad.centrocosto = Convert.ToString(resultado["centrocosto"]);
                                if (resultado["formapago"] != DBNull.Value) entidad.formapago = Convert.ToString(resultado["formapago"]);
                                if (resultado["iden_fsalud"] != DBNull.Value) entidad.iden_fsalud = Convert.ToString(resultado["iden_fsalud"]);
                                if (resultado["iden_fpension"] != DBNull.Value) entidad.iden_fpension = Convert.ToString(resultado["iden_fpension"]);
                                if (resultado["iden_fpensionvoluntaria"] != DBNull.Value) entidad.iden_fpensionvoluntaria = Convert.ToString(resultado["iden_fpensionvoluntaria"]);
                                if (resultado["iden_fsolidaridad"] != DBNull.Value) entidad.iden_fsolidaridad = Convert.ToString(resultado["iden_fsolidaridad"]);
                                lstNomina.Add(entidad);
                            //}
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstNomina;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InterfazNominaData", "ListarNomina", ex);
                        return null;
                    }
                }
            }
        }        

        public List<InterfazNomina> ListarNominaVacaciones(Int64 piden_periodo, String pidentificacion, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<InterfazNomina> lstNomina = new List<InterfazNomina>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexionInterfazNomina(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"Select a.iden_periodo, d.iden_empleado, e.identificacion, e.nombre1, e.apellido1,
                                        c.nombre, a.iden, a.iden_concepto, a.totaldctovacaciones As total, 0 As totalempleador, 0 As baseingreso, 0 As valorcuotaprestamo, 
                                        0 As iden_pago, a.fechacreacion, c.tercero, d.centrocosto, d.FormaPago,
                                        (Select x.tercero From nm_fondosalud x Where x.iden = d.iden_fsalud) As iden_fsalud, 
                                        (Select x.tercero From nm_fondopension x Where x.iden = d.iden_fpension) As iden_fpension, 
                                        (Select x.tercero From nm_fondopension x Where x.iden = d.iden_fpensionvoluntaria) As iden_fpensionvoluntaria, 
                                        (Select x.tercero From nm_fondopension x Where x.iden = d.iden_fsolidaridad) As iden_fsolidaridad
                                        From nm_VacacionesDevengosDctos a
                                        Inner Join nm_concepto c On a.iden_concepto = c.id
                                        Inner Join nm_contrato d On a.iden_contrato = d.iden
                                        Inner JOin nm_empleado e On e.iden = d.iden_empleado        
                                        Where a.fechacreacion >= (Select Fechainicio From nm_periodo Where iden = " + piden_periodo + ") And a.fechacreacion <= (Select FechaFinal From nm_periodo Where iden = " + piden_periodo + ") And a.totaldctovacaciones != 0";
                        if (pidentificacion.Trim() != "")
                            sql += " And e.identificacion = '" + pidentificacion + "' ";
                        sql += " Order by d.centrocosto, e.apellido1, a.iden_concepto";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            InterfazNomina entidad = new InterfazNomina();

                            if (resultado["iden_empleado"] != DBNull.Value) entidad.iden_empleado = Convert.ToString(resultado["iden_empleado"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["nombre1"] != DBNull.Value) entidad.nombre1 = Convert.ToString(resultado["nombre1"]);
                            if (resultado["apellido1"] != DBNull.Value) entidad.apellido1 = Convert.ToString(resultado["apellido1"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nombre"]);
                            if (resultado["iden"] != DBNull.Value) entidad.iden = Convert.ToString(resultado["iden"]);
                            if (resultado["iden_concepto"] != DBNull.Value) entidad.iden_concepto = Convert.ToString(resultado["iden_concepto"]);
                            if (resultado["total"] != DBNull.Value) entidad.total = Convert.ToDecimal(resultado["total"]);
                            if (resultado["totalempleador"] != DBNull.Value) entidad.totalempleador = Convert.ToDecimal(resultado["totalempleador"]);
                            if (resultado["baseingreso"] != DBNull.Value) entidad.baseingreso = Convert.ToDecimal(resultado["baseingreso"]);
                            if (resultado["valorcuotaprestamo"] != DBNull.Value) entidad.valorcuotaprestamo = Convert.ToDecimal(resultado["valorcuotaprestamo"]);
                            if (resultado["iden_pago"] != DBNull.Value) entidad.iden_pago = Convert.ToString(resultado["iden_pago"]);
                            if (resultado["fechacreacion"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["fechacreacion"]);
                            if (resultado["tercero"] != DBNull.Value) entidad.tercero = Convert.ToString(resultado["tercero"]);
                            if (resultado["centrocosto"] != DBNull.Value) entidad.centrocosto = Convert.ToString(resultado["centrocosto"]);
                            if (resultado["formapago"] != DBNull.Value) entidad.formapago = Convert.ToString(resultado["formapago"]);
                            if (resultado["iden_fsalud"] != DBNull.Value) entidad.iden_fsalud = Convert.ToString(resultado["iden_fsalud"]);
                            if (resultado["iden_fpension"] != DBNull.Value) entidad.iden_fpension = Convert.ToString(resultado["iden_fpension"]);
                            if (resultado["iden_fpensionvoluntaria"] != DBNull.Value) entidad.iden_fpensionvoluntaria = Convert.ToString(resultado["iden_fpensionvoluntaria"]);
                            if (resultado["iden_fsolidaridad"] != DBNull.Value) entidad.iden_fsolidaridad = Convert.ToString(resultado["iden_fsolidaridad"]);
                            lstNomina.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstNomina;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InterfazNominaData", "ListarNominaVacaciones", ex);
                        return null;
                    }
                }
            }
        }

        public List<InterfazNomina> ListarNominaConsolidado(Int64 piden_periodo, string pFiltro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<InterfazNomina> lstNomina = new List<InterfazNomina>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexionInterfazNomina(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        string sql = @"Select d.iden_empleado, e.identificacion, e.nombre1, e.apellido1, Min(b.fechacreacion) As fechacreacion, d.centrocosto, d.FormaPago,
                                        Sum(b.total) As total, Sum(b.totalempleador) As totalempleador, Sum(b.baseingreso) As baseingreso, Sum(b.valorcuotaprestamo) As valorcuotaprestamo
                                        From nm_devengosdctosperiodo a
                                        Inner Join nm_devengosdctosconceptos b On a.iden = b.iden_devengodcto
                                        Inner Join nm_concepto c On b.iden_concepto = c.id
                                        Inner Join nm_contrato d On a.iden_contrato = d.iden
                                        Inner JOin nm_empleado e On e.iden = d.iden_empleado            
                                        Where a.iden_periodo = " + piden_periodo.ToString() + " And (b.total != 0 Or b.totalempleador != 0) " + pFiltro + @"
                                        Group by d.iden_empleado, e.identificacion, e.nombre1, e.apellido1, d.centrocosto, d.FormaPago                                        
                                        Order by d.centrocosto, e.apellido1, e.identificacion";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            InterfazNomina entidad = new InterfazNomina();

                            if (resultado["iden_empleado"] != DBNull.Value) entidad.iden_empleado = Convert.ToString(resultado["iden_empleado"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["nombre1"] != DBNull.Value) entidad.nombre1 = Convert.ToString(resultado["nombre1"]);
                            if (resultado["apellido1"] != DBNull.Value) entidad.apellido1 = Convert.ToString(resultado["apellido1"]);
                            if (resultado["fechacreacion"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["fechacreacion"]);
                            if (resultado["centrocosto"] != DBNull.Value) entidad.centrocosto = Convert.ToString(resultado["centrocosto"]);
                            if (resultado["total"] != DBNull.Value) entidad.total = Convert.ToDecimal(resultado["total"]);
                            if (resultado["totalempleador"] != DBNull.Value) entidad.totalempleador = Convert.ToDecimal(resultado["totalempleador"]);
                            if (resultado["baseingreso"] != DBNull.Value) entidad.baseingreso = Convert.ToDecimal(resultado["baseingreso"]);
                            if (resultado["valorcuotaprestamo"] != DBNull.Value) entidad.valorcuotaprestamo = Convert.ToDecimal(resultado["valorcuotaprestamo"]);
                            if (resultado["formapago"] != DBNull.Value) entidad.formapago = Convert.ToString(resultado["formapago"]);                            

                            lstNomina.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstNomina;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InterfazNominaData", "ListarNominaConsolidado", ex);
                        return null;
                    }
                }
            }
        }

        public List<InterfazNomina> ListarPeriodos(Usuario pUsuario)
        {
            return ListarPeriodos("E", pUsuario);
        }

        public List<InterfazNomina> ListarPeriodos(string pTipo, Usuario pUsuario)
        {
            DateTime? fechaultcierre = FechaUltCierre("C", pUsuario);
            DateTime? fechaultnomina = FechaUltCierre(pTipo, pUsuario);

            DbDataReader resultado = default(DbDataReader);
            List<InterfazNomina> lstNomina = new List<InterfazNomina>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexionInterfazNomina(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"Select p.* From nm_periodo p    
                                        Where p.iden In (Select a.iden_periodo From nm_devengosdctosperiodo a Where a.iden_periodo = p.iden)";
                        if (fechaultcierre != null)
                            sql += " And p.fechafinal > '" + Convert.ToDateTime(fechaultcierre).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        if (fechaultnomina != null)
                            sql += " And p.fechafinal > '" + Convert.ToDateTime(fechaultnomina).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        sql += " Order by p.fechafinal";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            InterfazNomina entidad = new InterfazNomina();

                            if (resultado["iden"] != DBNull.Value) entidad.iden_periodo = Convert.ToString(resultado["iden"]);
                            if (resultado["fechainicio"] != DBNull.Value) entidad.fechainicio = Convert.ToDateTime(resultado["fechainicio"]);
                            if (resultado["fechafinal"] != DBNull.Value) entidad.fechafinal = Convert.ToDateTime(resultado["fechafinal"]);
                            entidad.nom_periodo = (entidad.fechainicio == null ? "" : Convert.ToDateTime(entidad.fechainicio).ToString(conf.ObtenerFormatoFecha())) + "-" + (entidad.fechafinal == null ? "" : Convert.ToDateTime(entidad.fechafinal).ToString(conf.ObtenerFormatoFecha()));
                            lstNomina.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstNomina;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InterfazNominaData", "ListarPeriodos", ex);
                        return null;
                    }
                }
            }
        }

        public List<InterfazNomina> ListarPeriodosPrima(Usuario pUsuario)
        {
            DateTime? fechaultcierre = FechaUltCierre("C", pUsuario);
            DateTime? fechaultnomina = FechaUltCierre("T", pUsuario);

            DbDataReader resultado = default(DbDataReader);
            List<InterfazNomina> lstNomina = new List<InterfazNomina>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexionInterfazNomina(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"Select p.* From nm_periodo p    
                                        Where p.iden In (Select a.iden_periodo From nm_devengosdctosperiodo a Where a.iden_periodo = p.iden)
                                        And p.iden In (select iden_periodo From nm_devengosdctosconceptos Where iden_concepto = '7') ";
                        if (fechaultcierre != null)
                            sql += " And p.fechafinal >= '" + Convert.ToDateTime(fechaultcierre).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        if (fechaultnomina != null)
                            sql += " And p.fechafinal >= '" + Convert.ToDateTime(fechaultnomina).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        sql += " Order by p.fechafinal";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            InterfazNomina entidad = new InterfazNomina();

                            if (resultado["iden"] != DBNull.Value) entidad.iden_periodo = Convert.ToString(resultado["iden"]);
                            if (resultado["fechainicio"] != DBNull.Value) entidad.fechainicio = Convert.ToDateTime(resultado["fechainicio"]);
                            if (resultado["fechafinal"] != DBNull.Value) entidad.fechafinal = Convert.ToDateTime(resultado["fechafinal"]);
                            entidad.nom_periodo = (entidad.fechainicio == null ? "" : Convert.ToDateTime(entidad.fechainicio).ToString(conf.ObtenerFormatoFecha())) + "-" + (entidad.fechafinal == null ? "" : Convert.ToDateTime(entidad.fechafinal).ToString(conf.ObtenerFormatoFecha()));
                            lstNomina.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstNomina;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InterfazNominaData", "ListarPeriodos", ex);
                        return null;
                    }
                }
            }
        }


        public DateTime? FechaUltCierre(string pTipo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            DateTime? entidad = null;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"Select Max(fecha) As fecha From cierea Where tipo = '" + pTipo + "' And estado = 'D' ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["fecha"] != DBNull.Value) entidad = Convert.ToDateTime(resultado["fecha"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InterfazNominaData", "ListarPeriodos", ex);
                        return null;
                    }
                }
            }
        }


        public List<InterfazNomina> ListarCreditos(Int64 piden_periodo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<InterfazNomina> lstNomina = new List<InterfazNomina>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexionInterfazNomina(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select d.iden_empleado, e.identificacion, e.nombre1, e.apellido1, c.nombre, b.iden, b.iden_concepto, 
                                        -b.total As total, d.centrocosto, b.iden_prestamo,  
                                        f.valorprestamo, (Select x.valor From nm_prestamocuotas x Where x.iden_prestamo = b.iden_prestamo And x.cuota = 1) As valorcuota, f.saldoprestamo
                                        From nm_devengosdctosperiodo a
                                        Inner Join nm_devengosdctosconceptos b On a.iden = b.iden_devengodcto
                                        Inner Join nm_concepto c On b.iden_concepto = c.id
                                        Inner Join nm_contrato d On a.iden_contrato = d.iden
                                        Inner Join nm_empleado e On e.iden = d.iden_empleado
                                        Left Join nm_prestamo f On b.iden_prestamo = f.iden
                                        Where a.iden_periodo = " + piden_periodo.ToString() + @" And b.iden_concepto In (47, 48)
                                        Order by e.apellido1, b.iden_pago, b.iden_concepto";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            InterfazNomina entidad = new InterfazNomina();

                            if (resultado["iden_empleado"] != DBNull.Value) entidad.iden_empleado = Convert.ToString(resultado["iden_empleado"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["nombre1"] != DBNull.Value) entidad.nombre1 = Convert.ToString(resultado["nombre1"]);
                            if (resultado["apellido1"] != DBNull.Value) entidad.apellido1 = Convert.ToString(resultado["apellido1"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nombre"]);
                            if (resultado["iden"] != DBNull.Value) entidad.iden = Convert.ToString(resultado["iden"]);
                            if (resultado["iden_concepto"] != DBNull.Value) entidad.iden_concepto = Convert.ToString(resultado["iden_concepto"]);
                            if (resultado["total"] != DBNull.Value) entidad.total = Convert.ToDecimal(resultado["total"]);
                            if (resultado["centrocosto"] != DBNull.Value) entidad.centrocosto = Convert.ToString(resultado["centrocosto"]);
                            if (resultado["iden_prestamo"] != DBNull.Value) entidad.iden_prestamo = Convert.ToString(resultado["iden_prestamo"]);
                            if (resultado["valorprestamo"] != DBNull.Value) entidad.valorprestamo = Convert.ToDecimal(resultado["valorprestamo"]);
                            if (resultado["valorcuota"] != DBNull.Value) entidad.valorcuota = Convert.ToDecimal(resultado["valorcuota"]);
                            if (resultado["saldoprestamo"] != DBNull.Value) entidad.saldoprestamo = Convert.ToDecimal(resultado["saldoprestamo"]);

                            lstNomina.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstNomina;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InterfazNominaData", "ListarConcepto", ex);
                        return null;
                    }
                }
            }
        }


        public List<InterfazNomina> ListarCreditosDelEmpleado(string pIdentificacion, string pConcepto, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<InterfazNomina> lstCreditos = new List<InterfazNomina>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"Select c.numero_radicacion, c.cod_linea_credito, l.nombre, c.monto_aprobado, c.valor_cuota, c.saldo_capital, c.fecha_proximo_pago 
                                        From credito c Inner Join persona p On c.cod_deudor = p.cod_persona Inner Join lineascredito l On c.cod_linea_credito = l.cod_linea_credito
                                        Where c.estado = 'C' And p.identificacion = '" + pIdentificacion + "' ";
                        if (pConcepto.Trim() != "")
                            sql += " And c.cod_linea_credito In (" + pConcepto + ") ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            InterfazNomina entidad = new InterfazNomina();
                            if (resultado["numero_radicacion"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["numero_radicacion"]);
                            if (resultado["cod_linea_credito"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["cod_linea_credito"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nom_linea_credito = Convert.ToString(resultado["nombre"]);
                            if (resultado["monto_aprobado"] != DBNull.Value) entidad.monto_aprobado = Convert.ToDecimal(resultado["monto_aprobado"]);
                            if (resultado["valor_cuota"] != DBNull.Value) entidad.valor_cuota = Convert.ToDecimal(resultado["valor_cuota"]);
                            if (resultado["saldo_capital"] != DBNull.Value) entidad.saldo_capital = Convert.ToDecimal(resultado["saldo_capital"]);
                            if (resultado["fecha_proximo_pago"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["fecha_proximo_pago"]);
                            entidad.descripcion = entidad.numero_radicacion + "-" + entidad.nom_linea_credito + "-" + entidad.valor_cuota;
                            lstCreditos.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCreditos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InterfazNominaData", "ListarCreditosDelEmpleado", ex);
                        return null;
                    }
                }
            }
        }

        public List<InterfazNomina> ListarCuentasConcepto(string pConcepto, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<InterfazNomina> lstCreditos = new List<InterfazNomina>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"Select p.cod_cuenta, c.nombre, p.tipo_mov, p.cod_tercero, p.tipo_tercero, l.identificacion
                                        From par_cue_nomina p Inner Join plan_cuentas c On p.cod_cuenta = c.cod_cuenta
                                        Left Join persona l On l.cod_persona = p.cod_tercero
                                        Where p.tipo_tran Is Null ";
                        if (pConcepto.Trim() != "")
                            sql += " And p.cod_concepto = '" + pConcepto + "' ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            InterfazNomina entidad = new InterfazNomina();
                            if (resultado["cod_cuenta"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["cod_cuenta"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nom_cuenta = Convert.ToString(resultado["nombre"]);
                            if (resultado["tipo_mov"] != DBNull.Value) entidad.tipomov = Convert.ToString(resultado["tipo_mov"]);
                            entidad.nom_cuenta = entidad.cod_cuenta + ' ' + entidad.nom_cuenta;
                            if (resultado["identificacion"] != DBNull.Value) entidad.iden_tercero = Convert.ToString(resultado["identificacion"]);
                            if (resultado["tipo_tercero"] != DBNull.Value) entidad.tipo_tercero = Convert.ToString(resultado["tipo_tercero"]);
                            lstCreditos.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCreditos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InterfazNominaData", "ListarCuentasConcepto", ex);
                        return null;
                    }
                }
            }
        }

        public void AplicarPago(InterfazNomina recaudosmasivos, Int64 pcod_ope, Usuario pUsuario, Int64 pnerror, ref string Error)
        {
            pnerror = 0;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pn_num_producto = cmdTransaccionFactory.CreateParameter();
                        pn_num_producto.ParameterName = "pn_num_producto";
                        if (recaudosmasivos.numero_radicacion == 0 || recaudosmasivos.numero_radicacion == null) pn_num_producto.Value = DBNull.Value; else pn_num_producto.Value = recaudosmasivos.numero_radicacion;
                        pn_num_producto.DbType = DbType.String;
                        pn_num_producto.Direction = ParameterDirection.Input;

                        DbParameter pn_cod_cliente = cmdTransaccionFactory.CreateParameter();
                        pn_cod_cliente.ParameterName = "pn_cod_cliente";
                        if (recaudosmasivos.cod_cliente == null) pn_cod_cliente.Value = DBNull.Value; else pn_cod_cliente.Value = recaudosmasivos.cod_cliente;
                        pn_cod_cliente.DbType = DbType.Int64;
                        pn_cod_cliente.Direction = ParameterDirection.Input;

                        DbParameter pn_cod_ope = cmdTransaccionFactory.CreateParameter();
                        pn_cod_ope.ParameterName = "pn_cod_ope";
                        pn_cod_ope.Value = pcod_ope;
                        pn_cod_ope.DbType = DbType.Int64;
                        pn_cod_ope.Direction = ParameterDirection.Input;

                        DbParameter pf_fecha_pago = cmdTransaccionFactory.CreateParameter();
                        pf_fecha_pago.ParameterName = "pf_fecha_pago";
                        if (recaudosmasivos.fecha_aplicacion == null) pf_fecha_pago.Value = DateTime.MinValue; else pf_fecha_pago.Value = recaudosmasivos.fecha_aplicacion;
                        pf_fecha_pago.DbType = DbType.DateTime;
                        pf_fecha_pago.Direction = ParameterDirection.Input;

                        DbParameter pn_valor_pago = cmdTransaccionFactory.CreateParameter();
                        pn_valor_pago.ParameterName = "pn_valor_pago";
                        pn_valor_pago.Value = recaudosmasivos.total;
                        pn_valor_pago.DbType = DbType.Double;
                        pn_valor_pago.Direction = ParameterDirection.Input;

                        DbParameter pn_num_cuotas = cmdTransaccionFactory.CreateParameter();
                        pn_num_cuotas.ParameterName = "pn_num_cuotas";
                        pn_num_cuotas.Value = DBNull.Value; 
                        pn_num_cuotas.DbType = DbType.Double;
                        pn_num_cuotas.Direction = ParameterDirection.Input;

                        DbParameter pn_tipo_tran = cmdTransaccionFactory.CreateParameter();
                        pn_tipo_tran.ParameterName = "pn_tipo_tran";
                        pn_tipo_tran.Value = 4;
                        pn_tipo_tran.DbType = DbType.Int64;
                        pn_tipo_tran.Direction = ParameterDirection.Input;

                        DbParameter pn_cod_usu = cmdTransaccionFactory.CreateParameter();
                        pn_cod_usu.ParameterName = "pn_cod_usu";
                        pn_cod_usu.Value = pUsuario.codusuario;
                        pn_cod_usu.DbType = DbType.Int64;
                        pn_cod_usu.Direction = ParameterDirection.Input;

                        DbParameter pn_cod_recuado = cmdTransaccionFactory.CreateParameter();
                        pn_cod_recuado.ParameterName = "pn_cod_recuado";
                        pn_cod_recuado.Value = DBNull.Value; 
                        pn_cod_recuado.DbType = DbType.Int64;
                        pn_cod_recuado.Direction = ParameterDirection.Input;

                        DbParameter rn_sobrante = cmdTransaccionFactory.CreateParameter();
                        rn_sobrante.ParameterName = "rn_sobrante";
                        rn_sobrante.Value = recaudosmasivos.sobrante;
                        rn_sobrante.DbType = DbType.Int64;
                        rn_sobrante.Direction = ParameterDirection.InputOutput;

                        DbParameter n_error = cmdTransaccionFactory.CreateParameter();
                        n_error.ParameterName = "n_error";
                        n_error.Value = pnerror;
                        n_error.DbType = DbType.Int64;
                        n_error.Direction = ParameterDirection.InputOutput;

                        cmdTransaccionFactory.Parameters.Add(pn_num_producto);
                        cmdTransaccionFactory.Parameters.Add(pn_cod_cliente);
                        cmdTransaccionFactory.Parameters.Add(pn_cod_ope);
                        cmdTransaccionFactory.Parameters.Add(pf_fecha_pago);
                        cmdTransaccionFactory.Parameters.Add(pn_valor_pago);
                        cmdTransaccionFactory.Parameters.Add(pn_num_cuotas);
                        cmdTransaccionFactory.Parameters.Add(pn_tipo_tran);
                        cmdTransaccionFactory.Parameters.Add(pn_cod_usu);
                        cmdTransaccionFactory.Parameters.Add(pn_cod_recuado);
                        cmdTransaccionFactory.Parameters.Add(rn_sobrante);
                        cmdTransaccionFactory.Parameters.Add(n_error);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_APLICARPAGO";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                    }
                    catch (Exception ex)
                    {
                        Error = ex.Message;
                        BOExcepcion.Throw("", "", ex);
                    }

                }
            }
        }

        public void GrabarNomina(InterfazNomina recaudosmasivos, Int64 pcod_ope, Usuario pUsuario, Int64 pnerror, ref string Error)
        {
            pnerror = 0;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pn_consecutivo = cmdTransaccionFactory.CreateParameter();
                        pn_consecutivo.ParameterName = "pn_consecutivo";
                        pn_consecutivo.Value = "0";
                        pn_consecutivo.DbType = DbType.Int64;
                        pn_consecutivo.Direction = ParameterDirection.InputOutput;
                        cmdTransaccionFactory.Parameters.Add(pn_consecutivo);

                        DbParameter pn_cod_ope = cmdTransaccionFactory.CreateParameter();
                        pn_cod_ope.ParameterName = "pn_cod_ope";
                        pn_cod_ope.Value = pcod_ope;
                        pn_cod_ope.DbType = DbType.Int64;
                        pn_cod_ope.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pn_cod_ope);

                        DbParameter pn_cod_empleado = cmdTransaccionFactory.CreateParameter();
                        pn_cod_empleado.ParameterName = "pn_cod_empleado";
                        if (recaudosmasivos.cod_cliente == null) pn_cod_empleado.Value = DBNull.Value; else pn_cod_empleado.Value = recaudosmasivos.cod_cliente;
                        pn_cod_empleado.DbType = DbType.Int64;
                        pn_cod_empleado.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pn_cod_empleado);

                        DbParameter pn_identificacion = cmdTransaccionFactory.CreateParameter();
                        pn_identificacion.ParameterName = "pn_identificacion";
                        if (recaudosmasivos.identificacion == null) pn_identificacion.Value = DBNull.Value; else pn_identificacion.Value = recaudosmasivos.identificacion;
                        pn_identificacion.DbType = DbType.String;
                        pn_identificacion.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pn_identificacion);

                        DbParameter pn_cod_concepto = cmdTransaccionFactory.CreateParameter();
                        pn_cod_concepto.ParameterName = "pn_cod_concepto";
                        if (recaudosmasivos.iden_concepto == null) pn_cod_concepto.Value = 0; else pn_cod_concepto.Value = recaudosmasivos.iden_concepto;
                        pn_cod_concepto.DbType = DbType.String;
                        pn_cod_concepto.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pn_cod_concepto);

                        DbParameter pn_concepto = cmdTransaccionFactory.CreateParameter();
                        pn_concepto.ParameterName = "pn_concepto";
                        if (recaudosmasivos.nombre == null) pn_concepto.Value = DBNull.Value; else pn_concepto.Value = recaudosmasivos.nombre;
                        pn_concepto.DbType = DbType.String;
                        pn_concepto.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pn_concepto);

                        DbParameter pn_valor = cmdTransaccionFactory.CreateParameter();
                        pn_valor.ParameterName = "pn_valor";
                        pn_valor.Value = recaudosmasivos.total;
                        pn_valor.DbType = DbType.Int64;
                        pn_valor.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pn_valor);

                        DbParameter pn_valor_empleador = cmdTransaccionFactory.CreateParameter();
                        pn_valor_empleador.ParameterName = "pn_valor_empleador";
                        pn_valor_empleador.Value = recaudosmasivos.valorcuota;
                        pn_valor_empleador.DbType = DbType.Int64;
                        pn_valor_empleador.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pn_valor_empleador);

                        DbParameter pn_fecha = cmdTransaccionFactory.CreateParameter();
                        pn_fecha.ParameterName = "pn_fecha";
                        if (recaudosmasivos.fecha_aplicacion == null) pn_fecha.Value = DateTime.MinValue; else pn_fecha.Value = recaudosmasivos.fecha_aplicacion;
                        pn_fecha.DbType = DbType.DateTime;
                        pn_fecha.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pn_fecha);

                        DbParameter pn_tercero = cmdTransaccionFactory.CreateParameter();
                        pn_tercero.ParameterName = "pn_tercero";
                        if (recaudosmasivos.tercero == null)
                        {
                            pn_tercero.Value = " ";
                        }
                        else
                        {
                            if (recaudosmasivos.tercero.Trim() == "")
                                pn_tercero.Value = " ";
                            else
                                pn_tercero.Value = recaudosmasivos.tercero;
                        }
                        pn_tercero.DbType = DbType.String;
                        pn_tercero.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pn_tercero);

                        DbParameter pn_centro_costo = cmdTransaccionFactory.CreateParameter();
                        pn_centro_costo.ParameterName = "pn_centro_costo";
                        if (recaudosmasivos.centrocosto == null)
                            pn_centro_costo.Value = DBNull.Value;
                        else
                            pn_centro_costo.Value = recaudosmasivos.centrocosto;
                        pn_centro_costo.DbType = DbType.String;
                        pn_centro_costo.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pn_centro_costo);

                        DbParameter pn_cod_cuenta = cmdTransaccionFactory.CreateParameter();
                        pn_cod_cuenta.ParameterName = "pn_cod_cuenta";
                        if (recaudosmasivos.cod_cuenta == null)
                        {
                            pn_cod_cuenta.Value = DBNull.Value;
                        }
                        else
                        {
                            if (recaudosmasivos.cod_cuenta == "")
                                pn_cod_cuenta.Value = DBNull.Value;
                            else
                                pn_cod_cuenta.Value = recaudosmasivos.cod_cuenta;
                        }
                        pn_cod_cuenta.DbType = DbType.String;
                        pn_cod_cuenta.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pn_cod_cuenta);

                        DbParameter pn_cod_cuenta_contra = cmdTransaccionFactory.CreateParameter();
                        pn_cod_cuenta_contra.ParameterName = "pn_cod_cuenta_contra";
                        if (recaudosmasivos.cod_cuenta_gasto == null)
                        {
                            pn_cod_cuenta_contra.Value = DBNull.Value;
                        }
                        else
                        {
                            if (recaudosmasivos.cod_cuenta_gasto == "")
                                pn_cod_cuenta_contra.Value = DBNull.Value;
                            else
                                pn_cod_cuenta_contra.Value = recaudosmasivos.cod_cuenta_gasto;
                        }
                        pn_cod_cuenta_contra.DbType = DbType.String;
                        pn_cod_cuenta_contra.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pn_cod_cuenta_contra);

                        DbParameter pn_forma_pago = cmdTransaccionFactory.CreateParameter();
                        pn_forma_pago.ParameterName = "pn_forma_pago";
                        if (recaudosmasivos.formapago == null)
                        {
                            pn_forma_pago.Value = " ";
                        }
                        else
                        {
                            if (recaudosmasivos.formapago.Trim() == "")
                                pn_forma_pago.Value = " ";
                            else
                                pn_forma_pago.Value = recaudosmasivos.formapago;
                        }
                        pn_forma_pago.DbType = DbType.String;
                        pn_forma_pago.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pn_forma_pago);

                        DbParameter pn_cod_usu = cmdTransaccionFactory.CreateParameter();
                        pn_cod_usu.ParameterName = "pn_cod_usu";
                        pn_cod_usu.Value = pUsuario.codusuario;
                        pn_cod_usu.DbType = DbType.Int64;
                        pn_cod_usu.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pn_cod_usu);
                        
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_NOMINA_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                    }
                    catch (Exception ex)
                    {
                        Error = ex.Message + "--> Id:" + recaudosmasivos.identificacion + " Con:" + recaudosmasivos.iden_concepto + " Cta:" + recaudosmasivos.cod_cuenta + " Val:" + recaudosmasivos.total + " CtaGas:" + recaudosmasivos.cod_cuenta_gasto + " Emp:" + recaudosmasivos.valorcuota + " Usu:" + pUsuario.codusuario + " Fec:" + recaudosmasivos.fecha_aplicacion + " FPag:" + recaudosmasivos.formapago + " NCon:" + recaudosmasivos.nombre + " Ter:" + recaudosmasivos.tercero + " C/C:" + recaudosmasivos.centrocosto + " Cli:" + recaudosmasivos.cod_cliente + "<---";
                        BOExcepcion.Throw("", "", ex);
                    }

                }
            }
        }


        public Int64? CodigoDelEmpleado(string pIdentificacion, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Int64? cod_persona = null;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"Select p.cod_persona From persona p Where p.identificacion = '" + pIdentificacion + "' ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["cod_persona"] != DBNull.Value) cod_persona = Convert.ToInt64(resultado["cod_persona"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return cod_persona;
                    }
                    catch 
                    {
                        return null;
                    }
                }
            }
        }


        public Xpinn.Comun.Entities.Cierea CrearCierea(Xpinn.Comun.Entities.Cierea pCierea, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "p_fecha";
                        pfecha.Value = pCierea.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        DbParameter ptipo = cmdTransaccionFactory.CreateParameter();
                        ptipo.ParameterName = "p_tipo";
                        ptipo.Value = pCierea.tipo;
                        ptipo.Direction = ParameterDirection.Input;
                        ptipo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipo);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pCierea.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pcampo1 = cmdTransaccionFactory.CreateParameter();
                        pcampo1.ParameterName = "p_campo1";
                        pcampo1.Value = pCierea.campo1;
                        pcampo1.Direction = ParameterDirection.Input;
                        pcampo1.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcampo1);

                        DbParameter pcampo2 = cmdTransaccionFactory.CreateParameter();
                        pcampo2.ParameterName = "p_campo2";
                        if (pCierea.campo2 == null)
                            pcampo2.Value = DBNull.Value;
                        else
                            pcampo2.Value = pCierea.campo2;
                        pcampo2.Direction = ParameterDirection.Input;
                        pcampo2.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcampo2);

                        DbParameter pfecrea = cmdTransaccionFactory.CreateParameter();
                        pfecrea.ParameterName = "p_fecrea";
                        if (pCierea.fecrea == null)
                            pfecrea.Value = DBNull.Value;
                        else
                            pfecrea.Value = pCierea.fecrea;
                        pfecrea.Direction = ParameterDirection.Input;
                        pfecrea.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecrea);

                        DbParameter pcodusuario = cmdTransaccionFactory.CreateParameter();
                        pcodusuario.ParameterName = "p_codusuario";
                        pcodusuario.Value = DBNull.Value;                        
                        pcodusuario.Direction = ParameterDirection.Input;
                        pcodusuario.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodusuario);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_CIEREA_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pCierea;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InterfazNominaData", "CrearCierea", ex);
                        return null;
                    }
                }
            }
        }

        public DateTime? FechaDelPeriodo(Int64 pPeriodo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            DateTime? fechafinal = null;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexionInterfazNomina(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"Select p.* From nm_periodo p    
                                        Where p.iden = " + pPeriodo.ToString(); ;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["fechafinal"] != DBNull.Value) fechafinal = Convert.ToDateTime(resultado["fechafinal"]);                         
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return fechafinal;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InterfazNominaData", "FechaDelPeriodo", ex);
                        return null;
                    }
                }
            }
        }

        public bool ExistePeriodo(Int64 pPeriodo, String pTipo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"Select p.fecha From cierea p Where p.tipo = '" + pTipo + "' And p.campo1 = '" + pPeriodo.ToString() + "' And p.estado = 'D' ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            return true;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return false;
                    }
                    catch 
                    {
                        return false;
                    }
                }
            }
        }

        public List<InterfazNomina> ListarNominaProvision(Int64 pAño, Int64 pMes, string pFiltro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<InterfazNomina> lstNomina = new List<InterfazNomina>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexionInterfazNomina(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        string sql = @"Select a.iden_contrato, d.iden_empleado, e.identificacion, e.nombre1, e.apellido1, 
                                        a.iden_provision, b.codigo, b.nombre, d.centrocosto, Sum(a.totalprovision) As totalprovision
                                        From nm_provisionperiodos a Inner Join nm_provisionparafiscal b On a.iden_provision = b.iden
                                        Inner Join nm_contrato d On a.iden_contrato = d.iden
                                        Inner JOin nm_empleado e On e.iden = d.iden_empleado   
                                        Where a.iden_periodo In (Select Iden From nm_periodo Where Ano = " + pAño + " And Mes = " + pMes + @") " + pFiltro + @"
                                        Group by a.iden_contrato, d.iden_empleado, e.identificacion, e.nombre1, e.apellido1, a.iden_provision, b.codigo, b.nombre, d.centrocosto
                                        Order by d.centrocosto, e.apellido1";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            InterfazNomina entidad = new InterfazNomina();

                            if (resultado["iden_empleado"] != DBNull.Value) entidad.iden_empleado = Convert.ToString(resultado["iden_empleado"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["nombre1"] != DBNull.Value) entidad.nombre1 = Convert.ToString(resultado["nombre1"]);
                            if (resultado["apellido1"] != DBNull.Value) entidad.apellido1 = Convert.ToString(resultado["apellido1"]);
                            if (resultado["codigo"] != DBNull.Value) entidad.iden_concepto = Convert.ToString(resultado["codigo"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nombre"]);                            
                            if (resultado["centrocosto"] != DBNull.Value) entidad.centrocosto = Convert.ToString(resultado["centrocosto"]);
                            if (resultado["totalprovision"] != DBNull.Value) entidad.total = Convert.ToDecimal(resultado["totalprovision"]);                            
                            lstNomina.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstNomina;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InterfazNominaData", "ListarNominaConsolidado", ex);
                        return null;
                    }
                }
            }
        }

        public List<InterfazNomina> ListarPeriodosProvision(Usuario pUsuario)
        {
            DateTime? fechaultcierre = FechaUltCierre("C", pUsuario);
            DateTime? fechaultprovision = FechaUltCierre("F", pUsuario);

            DbDataReader resultado = default(DbDataReader);
            List<InterfazNomina> lstNomina = new List<InterfazNomina>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexionInterfazNomina(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"Select b.Ano, b.Mes, b.fechafinal, b.iden
                                        From nm_provisionperiodos a Inner Join nm_periodo b On a.IDEN_Periodo = b.iden";
                        if (fechaultcierre != null)
                            sql += " And b.fechafinal > '" + Convert.ToDateTime(fechaultcierre).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        if (fechaultprovision != null)
                            sql += " And b.fechafinal > '" + Convert.ToDateTime(fechaultprovision).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        sql += " Group by b.Ano, b.Mes, b.fechafinal, b.iden Order by 3 desc";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            InterfazNomina entidad = new InterfazNomina();

                            if (resultado["iden"] != DBNull.Value) entidad.iden_periodo = Convert.ToString(resultado["iden"]);                            
                            if (resultado["fechafinal"] != DBNull.Value) entidad.fechafinal = Convert.ToDateTime(resultado["fechafinal"]);
                            entidad.nom_periodo = (entidad.fechafinal == null ? "" : Convert.ToDateTime(entidad.fechafinal).ToString(conf.ObtenerFormatoFecha()) );
                            lstNomina.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstNomina;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InterfazNominaData", "ListarPeriodosProvision", ex);
                        return null;
                    }
                }
            }
        }

        public List<InterfazNomina> ListarSaludPension(Int64 piden_periodo, string pFiltro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<InterfazNomina> lstNomina = new List<InterfazNomina>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexionInterfazNomina(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select d.iden_empleado, e.identificacion, e.nombre1, e.apellido1,
                                            c.nombre, b.iden, b.iden_concepto, b.totalempleador, b.baseingreso, 
                                            b.iden_pago, b.fechacreacion, c.tercero, d.centrocosto, 
                                            (Select x.tercero From nm_fondosalud x Where x.iden = d.iden_fsalud) As iden_fsalud, 
                                            (Select x.tercero From nm_fondopension x Where x.iden = d.iden_fpension) As iden_fpension
                                            From nm_devengosdctosperiodo a
                                            Inner Join nm_devengosdctosconceptos b On a.iden = b.iden_devengodcto
                                            Inner Join nm_concepto c On b.iden_concepto = c.id
                                            Inner Join nm_contrato d On a.iden_contrato = d.iden
                                            Inner JOin nm_empleado e On e.iden = d.iden_empleado            
                                            Where a.iden_periodo = " + piden_periodo.ToString() + @" And b.totalempleador != 0 ";
                        if (pFiltro.Trim() != "")
                            sql += pFiltro;
                        sql += " Order by d.centrocosto, e.apellido1, b.iden_pago, b.iden_concepto";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            InterfazNomina entidad = new InterfazNomina();

                            if (resultado["iden_empleado"] != DBNull.Value) entidad.iden_empleado = Convert.ToString(resultado["iden_empleado"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["nombre1"] != DBNull.Value) entidad.nombre1 = Convert.ToString(resultado["nombre1"]);
                            if (resultado["apellido1"] != DBNull.Value) entidad.apellido1 = Convert.ToString(resultado["apellido1"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nombre"]);
                            if (resultado["iden"] != DBNull.Value) entidad.iden = Convert.ToString(resultado["iden"]);
                            if (resultado["iden_concepto"] != DBNull.Value) entidad.iden_concepto = Convert.ToString(resultado["iden_concepto"]);
                            if (resultado["totalempleador"] != DBNull.Value) entidad.totalempleador = Convert.ToDecimal(resultado["totalempleador"]);
                            if (resultado["baseingreso"] != DBNull.Value) entidad.baseingreso = Convert.ToDecimal(resultado["baseingreso"]);
                            if (resultado["iden_pago"] != DBNull.Value) entidad.iden_pago = Convert.ToString(resultado["iden_pago"]);
                            if (resultado["fechacreacion"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["fechacreacion"]);
                            if (resultado["tercero"] != DBNull.Value) entidad.tercero = Convert.ToString(resultado["tercero"]);
                            if (resultado["centrocosto"] != DBNull.Value) entidad.centrocosto = Convert.ToString(resultado["centrocosto"]);
                            if (resultado["iden_fsalud"] != DBNull.Value) entidad.iden_fsalud = Convert.ToString(resultado["iden_fsalud"]);
                            if (resultado["iden_fpension"] != DBNull.Value) entidad.iden_fpension = Convert.ToString(resultado["iden_fpension"]);
                            lstNomina.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstNomina;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InterfazNominaData", "ListarSaludPension", ex);
                        return null;
                    }
                }
            }
        }

        public List<InterfazNomina> ListarCuentasContraConcepto(string pConcepto, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<InterfazNomina> lstCreditos = new List<InterfazNomina>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"Select p.cod_cuenta_contra, c.nombre, p.tipo_mov, p.cod_tercero, p.tipo_tercero, l.identificacion
                                        From par_cue_nomina p Inner Join plan_cuentas c On p.cod_cuenta_contra = c.cod_cuenta
                                        Left Join persona l On l.cod_persona = p.cod_tercero
                                        Where p.tipo_tran Is Null ";
                        if (pConcepto.Trim() != "")
                            sql += " And p.cod_concepto = '" + pConcepto + "' ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            InterfazNomina entidad = new InterfazNomina();
                            if (resultado["cod_cuenta_contra"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["cod_cuenta_contra"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nom_cuenta = Convert.ToString(resultado["nombre"]);
                            if (resultado["tipo_mov"] != DBNull.Value) entidad.tipomov = Convert.ToString(resultado["tipo_mov"]);
                            entidad.nom_cuenta = entidad.cod_cuenta + ' ' + entidad.nom_cuenta;
                            if (resultado["identificacion"] != DBNull.Value) entidad.iden_tercero = Convert.ToString(resultado["identificacion"]);
                            if (resultado["tipo_tercero"] != DBNull.Value) entidad.tipo_tercero = Convert.ToString(resultado["tipo_tercero"]);
                            lstCreditos.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCreditos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InterfazNominaData", "ListarCuentasContraConcepto", ex);
                        return null;
                    }
                }
            }
        }

        public List<InterfazNomina> ListarPeriodosSaludPension(Usuario pUsuario)
        {
            DateTime? fechaultcierre = FechaUltCierre("C", pUsuario);
            DateTime? fechaultnomina = FechaUltCierre("D", pUsuario);

            DbDataReader resultado = default(DbDataReader);
            List<InterfazNomina> lstNomina = new List<InterfazNomina>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexionInterfazNomina(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"Select p.* From nm_periodo p    
                                        Where p.iden In (Select a.iden_periodo From nm_devengosdctosperiodo a Where a.iden_periodo = p.iden)";
                        if (fechaultcierre != null)
                            sql += " And p.fechafinal > '" + Convert.ToDateTime(fechaultcierre).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        if (fechaultnomina != null)
                            sql += " And p.fechafinal > '" + Convert.ToDateTime(fechaultnomina).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        sql += " Order by p.fechafinal";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            InterfazNomina entidad = new InterfazNomina();

                            if (resultado["iden"] != DBNull.Value) entidad.iden_periodo = Convert.ToString(resultado["iden"]);
                            if (resultado["fechainicio"] != DBNull.Value) entidad.fechainicio = Convert.ToDateTime(resultado["fechainicio"]);
                            if (resultado["fechafinal"] != DBNull.Value) entidad.fechafinal = Convert.ToDateTime(resultado["fechafinal"]);
                            entidad.nom_periodo = (entidad.fechainicio == null ? "" : Convert.ToDateTime(entidad.fechainicio).ToString(conf.ObtenerFormatoFecha())) + "-" + (entidad.fechafinal == null ? "" : Convert.ToDateTime(entidad.fechafinal).ToString(conf.ObtenerFormatoFecha()));
                            lstNomina.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstNomina;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InterfazNominaData", "ListarPeriodosSaludPension", ex);
                        return null;
                    }
                }
            }
        }


    }

}