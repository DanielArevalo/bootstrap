using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using System.Data;
using System.Data.Common;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Data
{
    public class TrasladarClienteData : GlobalData
    {
         protected ConnectionDataBase dbConnectionFactory;
         Configuracion global = new Configuracion();
         string FormatoFecha = " ";

         public TrasladarClienteData()
        {
            dbConnectionFactory = new ConnectionDataBase();
            FormatoFecha = global.ObtenerValorConfig("FormatoFechaBase");
        }

         public List<Persona> ListarClientesAsesor(string filtro, Usuario pUsuario)
        {
            DbDataReader reader = default(DbDataReader);
            List<Persona> lstPrograma = new List<Persona>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = " SELECT * FROM v_listarclientesasesor " + filtro;


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        reader = cmdTransaccionFactory.ExecuteReader();


                        while (reader.Read())
                        {
                            Persona entidad = new Persona();

                           if (reader["PRIMER_NOMBRE"] != DBNull.Value) entidad.PrimerNombre = reader["PRIMER_NOMBRE"].ToString()+" "+reader["PRIMER_APELLIDO"].ToString()+" "+reader["SEGUNDO_APELLIDO"].ToString();
                           if (reader["IDENTIFICACION"] != DBNull.Value) entidad.SegundoNombre = reader["IDENTIFICACION"].ToString();
                           if (reader["SNOMBRE1"] != DBNull.Value) entidad.RazonSocial = reader["SNOMBRE1"].ToString() + " " + reader["SNOMBRE2"].ToString() + " " + reader["SAPELLIDO1"].ToString() + " " + reader["SAPELLIDO2"].ToString();
                          
                           lstPrograma.Add(entidad);
                        }

                        return lstPrograma;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProgramaData", "ListarPrograma", ex);
                        return null;
                    }
                }
            }
        }

         public List<Persona> ListarProductosAsesor(string filtro, Usuario pUsuario)
        {
            DbDataReader reader = default(DbDataReader);
            List<Persona> lstPrograma = new List<Persona>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = " SELECT * FROM v_listarproductosasesor " + filtro;


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        reader = cmdTransaccionFactory.ExecuteReader();


                        while (reader.Read())
                        {
                            Persona entidad = new Persona();

                            if (reader["SNOMBRE1"] != DBNull.Value) entidad.PrimerNombre = reader["SNOMBRE1"].ToString()+" "+ reader["SAPELLIDO1"].ToString()+" "+reader["SAPELLIDO2"].ToString();
                            if (reader["COD_LINEA_CREDITO"] != DBNull.Value) entidad.SegundoNombre = reader["COD_LINEA_CREDITO"].ToString();
                            if (reader["MONTO_APROBADO"] != DBNull.Value) entidad.NumeroDocumento = Convert.ToString(reader["MONTO_APROBADO"].ToString());
                            if (reader["ESTADO"] != DBNull.Value) entidad.EstadoCivil = reader["ESTADO"].ToString();
                            if (reader["NUMERO_RADICACION"] != DBNull.Value) entidad.TelefonoEmpresa = Convert.ToString(reader["NUMERO_RADICACION"].ToString());
                            if (reader["PRIMER_NOMBRE"] != DBNull.Value) entidad.Empresa = reader["PRIMER_NOMBRE"].ToString() + " " + reader["PRIMER_APELLIDO"].ToString();
                            if (reader["SIDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(reader["SIDENTIFICACION"].ToString());
                           
                           lstPrograma.Add(entidad);
                        }

                        return lstPrograma;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProgramaData", "ListarPrograma", ex);
                        return null;
                    }
                }
            }
        }

         public List<Persona> ListarProductos(string filtro, Usuario pUsuario)
         {
             DbDataReader reader = default(DbDataReader);
             List<Persona> lstPrograma = new List<Persona>();

             using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
             {
                 using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                 {
                     try
                     {
                         string sql = " SELECT PR.*,PER.TIPO_PERSONA FROM VS_PRODUCTOS PR JOIN V_PERSONA PER ON PR.COD_PERSONA=PER.COD_PERSONA " + filtro;


                         connection.Open();
                         cmdTransaccionFactory.Connection = connection;
                         cmdTransaccionFactory.CommandType = CommandType.Text;
                         cmdTransaccionFactory.CommandText = sql;
                         reader = cmdTransaccionFactory.ExecuteReader();


                         while (reader.Read())
                         {
                             Persona entidad = new Persona();

                             if (reader["TIPO_PRODUCTO"] != DBNull.Value) entidad.TipoProducto = Convert.ToInt64(reader["TIPO_PRODUCTO"].ToString());
                             if (reader["NOM_TIPO_PRODUCTO"] != DBNull.Value) entidad.producto = reader["NOM_TIPO_PRODUCTO"].ToString();
                             if (reader["COD_PERSONA"] != DBNull.Value) entidad.codpersona = Convert.ToInt64(reader["COD_PERSONA"].ToString());
                             if (reader["NUMERO_PRODUCTO"] != DBNull.Value) entidad.numproducto = Convert.ToString(reader["NUMERO_PRODUCTO"]);
                             if (reader["COD_LINEA"] != DBNull.Value) entidad.codlinea = Convert.ToString(reader["COD_LINEA"].ToString());
                             if (reader["NOM_LINEA"] != DBNull.Value) entidad.nomlinea = Convert.ToString(reader["NOM_LINEA"].ToString());
                             if (reader["FECHA_APERTURA"] != DBNull.Value) entidad.fechaapertura = Convert.ToDateTime(reader["FECHA_APERTURA"].ToString());
                             if (reader["SALDO"] != DBNull.Value) entidad.saldo = Convert.ToDecimal(reader["SALDO"].ToString());
                             if (reader["CUOTA"] != DBNull.Value) entidad.cuota = Convert.ToDecimal(reader["CUOTA"].ToString());
                             if (reader["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fechaproxpago = Convert.ToDateTime(reader["FECHA_PROXIMO_PAGO"].ToString());
                             if (reader["COD_OFICINA"] != DBNull.Value) entidad.codoficina = Convert.ToInt64(reader["COD_OFICINA"].ToString());
                             if (reader["NOMBRE"] != DBNull.Value) entidad.nombrecliente = Convert.ToString(reader["NOMBRE"].ToString());
                             if (reader["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(reader["IDENTIFICACION"].ToString());
                            
                             lstPrograma.Add(entidad);
                         }

                         return lstPrograma;
                     }
                     catch (Exception ex)
                     {
                         BOExcepcion.Throw("ProgramaData", "ListarPrograma", ex);
                         return null;
                     }
                 }
             }
         }
        
         

         public void ModificarProductosOficinasTodos(int producto,int cod, int condicion, Usuario pUsuario)
         {
             DbDataReader reader = default(DbDataReader);
             List<Persona> lstPrograma = new List<Persona>();
             string sql = "";
             using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
             {
                 using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                 {
                     if (producto == 2)
                     {
                         sql = " UPDATE credito  SET cod_oficina =" + cod + "where cod_oficina=" + condicion;
                     }
                         if (producto == 1)
                         {
                             sql = " UPDATE aporte  SET cod_oficina =" + cod + "where cod_oficina=" + condicion;
                         }

                    
                  
                     connection.Open();
                     cmdTransaccionFactory.Connection = connection;
                     cmdTransaccionFactory.CommandType = CommandType.Text;
                     cmdTransaccionFactory.CommandText = sql;
                     reader = cmdTransaccionFactory.ExecuteReader();



                 }
             }
         }
       
         public void ModificarProductosAsesorTodos(int cod, int condicion, Usuario pUsuario)
         {
             DbDataReader reader = default(DbDataReader);
             List<Persona> lstPrograma = new List<Persona>();

             using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
             {
                 using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                 {

                     string sql = " UPDATE credito  SET cod_asesor_com =" + cod + "where cod_asesor_com=" + condicion;

                     connection.Open();
                     cmdTransaccionFactory.Connection = connection;
                     cmdTransaccionFactory.CommandType = CommandType.Text;
                     cmdTransaccionFactory.CommandText = sql;
                     reader = cmdTransaccionFactory.ExecuteReader();



                 }
             }
         }
         public void ModificarClientesAsesorTodos(int cod, int condicion, Usuario pUsuario)
         {
             DbDataReader reader = default(DbDataReader);
             List<Persona> lstPrograma = new List<Persona>();

             using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
             {
                 using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                 {

                     string sql = " UPDATE  persona SET cod_asesor =" + cod + "where cod_asesor=" + condicion;

                     connection.Open();
                     cmdTransaccionFactory.Connection = connection;
                     cmdTransaccionFactory.CommandType = CommandType.Text;
                     cmdTransaccionFactory.CommandText = sql;
                     reader = cmdTransaccionFactory.ExecuteReader();



                 }
             }
         }
         public void ModificarClientesoficinaSTodos(int cod, int condicion, Usuario pUsuario)
         {
             DbDataReader reader = default(DbDataReader);
             List<Persona> lstPrograma = new List<Persona>();

             using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
             {
                 using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                 {

                    

                     string sql = " UPDATE  persona SET cod_oficina = " + cod + " WHERE IDENTIFICACION = '" + condicion + "'";

                     connection.Open();
                     cmdTransaccionFactory.Connection = connection;
                     cmdTransaccionFactory.CommandType = CommandType.Text;
                     cmdTransaccionFactory.CommandText = sql;
                     reader = cmdTransaccionFactory.ExecuteReader();



                 }
             }
         }
         public void ModificarProductosAsesor(int cod, int condicion, Usuario pUsuario)
         {
             DbDataReader reader = default(DbDataReader);
             List<Persona> lstPrograma = new List<Persona>();

             using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
             {
                 using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                 {

                     string sql = " UPDATE credito  SET cod_asesor_com = " + cod + " where NUMERO_RADICACION = '" + condicion+"'";

                     connection.Open();
                     cmdTransaccionFactory.Connection = connection;
                     cmdTransaccionFactory.CommandType = CommandType.Text;
                     cmdTransaccionFactory.CommandText = sql;
                     reader = cmdTransaccionFactory.ExecuteReader();



                 }
             }
         }
         public void ModificarProductosOficina(int producto,int cod, int condicion, Usuario pUsuario)
         {
             DbDataReader reader = default(DbDataReader);
             List<Persona> lstPrograma = new List<Persona>();
             string sql="";
             using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
             {
                 using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                 {
                     
                     if (producto == 2)
                     {
                          sql = " UPDATE credito  SET cod_oficina = " + cod + " where NUMERO_RADICACION = '" + condicion + "'";
                     }
                     if (producto == 1)
                     {
                          sql = " UPDATE aporte  SET cod_oficina = " + cod + " where NUMERO_APORTE = '" + condicion + "'";
                     }
                     
                     connection.Open();
                     cmdTransaccionFactory.Connection = connection;
                     cmdTransaccionFactory.CommandType = CommandType.Text;
                     cmdTransaccionFactory.CommandText = sql;                     
                     reader = cmdTransaccionFactory.ExecuteReader();



                 }
             }
         }

         public void ModificarProductosOficina2( int cod, int condicion, Usuario pUsuario)
         {
             DbDataReader reader = default(DbDataReader);
             List<Persona> lstPrograma = new List<Persona>();
             string sql = "";
             string sql1 = "";
             using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
             {
                 using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                 {

                   //  if (producto == 2)
                     //{
                         sql = " UPDATE credito  SET cod_oficina = " + cod + " where cod_persona = '" + condicion + "'";
                    // }
                    // if (producto == 1)
                     //{
                         sql1 = " UPDATE aporte  SET cod_oficina = " + cod + " where cod_persona  = '" + condicion + "'";
                     //}

                     connection.Open();
                     cmdTransaccionFactory.Connection = connection;
                     cmdTransaccionFactory.CommandType = CommandType.Text;
                     cmdTransaccionFactory.CommandText = sql;
                     cmdTransaccionFactory.CommandText = sql1;
                     reader = cmdTransaccionFactory.ExecuteReader();



                 }
             }
         }
         public void ModificarClientesAsesor(int cod, int condicion, Usuario pUsuario)
         {
             DbDataReader reader = default(DbDataReader);
             List<Persona> lstPrograma = new List<Persona>();

             using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
             {
                 using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                 {

                     string sql = " UPDATE  persona SET cod_asesor = " + cod + " WHERE IDENTIFICACION = '" + condicion+"'";

                     connection.Open();
                     cmdTransaccionFactory.Connection = connection;
                     cmdTransaccionFactory.CommandType = CommandType.Text;
                     cmdTransaccionFactory.CommandText = sql;
                     reader = cmdTransaccionFactory.ExecuteReader();



                 }
             }
         }
         public void ModificarClientesOficinas(int cod, int condicion, Usuario pUsuario)
         {
             DbDataReader reader = default(DbDataReader);
             List<Persona> lstPrograma = new List<Persona>();

             using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
             {
                 using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                 {

                     string sql = " UPDATE  persona SET cod_oficina = " + cod + " WHERE IDENTIFICACION = '" + condicion + "'";

                     connection.Open();
                     cmdTransaccionFactory.Connection = connection;
                     cmdTransaccionFactory.CommandType = CommandType.Text;
                     cmdTransaccionFactory.CommandText = sql;
                     reader = cmdTransaccionFactory.ExecuteReader();



                 }
             }
         }
         public void CrearRegistroAsesores(int tipo,int nuevo, int antiguo, string cod, string obser, Usuario pUsuario)
         {
             DbDataReader reader = default(DbDataReader);
             List<Persona> lstPrograma = new List<Persona>();

             using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
             {
                 using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                 {
                     Configuracion Configuracion = new Configuracion();
                     try
                     {
                         var a = DateTime.Today.ToString(Configuracion.ObtenerValorConfig("FormatoFechaBase"));
                         string sql = "INSERT INTO REGISTRO_TRASLADOS(ASESOR_NUEVO,ASESOR_ANTIGUO,COD_MOTIVO_TRASLADO,OBSERVACION,FECHA_MODIFICACION,OFICINA_ANTIGUA,OFICINA_NUEVA,TIPO)VALUES(" + nuevo + "," + antiguo + "," + Convert.ToInt32(cod) + ",'" + obser + "','" + a + "'" + 0 + "," + 0 + "," + tipo + " )";
                        // string sql = "INSERT INTO REGISTRO_TRASLADOS(OFICINA_ANTIGUA,OFICINA_NUEVA,COD_MOTIVO_TRASLADO,OBSERVACION,FECHA_MODIFICACION,ASESOR_NUEVO,ASESOR_ANTIGUO,TIPO)VALUES(" + nuevo + "," + antiguo + "," + Convert.ToInt32(cod) + ",'" + obser + "','" + a + "'" + 0 + "," + 0 + "," + tipo + " )";


                         connection.Open();
                         cmdTransaccionFactory.Connection = connection;
                         cmdTransaccionFactory.CommandType = CommandType.Text;
                         cmdTransaccionFactory.CommandText = sql;
                         reader = cmdTransaccionFactory.ExecuteReader();
                     }
                     catch
                     {
                     }
                 }
             }
         }
         public void CrearRegistroOficinas(int tipo, int nuevo, int antiguo, string cod, string obser, Usuario pUsuario)
         {
             DbDataReader reader = default(DbDataReader);
             List<Persona> lstPrograma = new List<Persona>();

             using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
             {
                 using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                 {
                     Configuracion Configuracion = new Configuracion();
                     try
                     {
                         var a = DateTime.Today.ToString(Configuracion.ObtenerValorConfig("FormatoFechaBase"));
                         //string sql = "INSERT INTO REGISTRO_TRASLADOS(ASESOR_NUEVO,ASESOR_ANTIGUO,COD_MOTIVO_TRASLADO,OBSERVACION,FECHA_MODIFICACION)VALUES(" + nuevo + "," + antiguo + "," + Convert.ToInt32(cod) + ",'" + obser + "','" + a + "')";
                         string sql = "INSERT INTO REGISTRO_TRASLADOS(OFICINA_ANTIGUA,OFICINA_NUEVA,COD_MOTIVO_TRASLADO,OBSERVACION,FECHA_MODIFICACION,ASESOR_NUEVO,ASESOR_ANTIGUO,TIPO)VALUES(" + nuevo + "," + antiguo + "," + Convert.ToInt32(cod) + ",'" + obser + "','" + a + "'" + "," + 0 + "," + 0 + "," + tipo + " )";

                         connection.Open();
                         cmdTransaccionFactory.Connection = connection;
                         cmdTransaccionFactory.CommandType = CommandType.Text;
                         cmdTransaccionFactory.CommandText = sql;
                         reader = cmdTransaccionFactory.ExecuteReader();
                     }
                     catch
                     {
                     }
                 }
             }
         }


         public List<TrasladarClientes> ListarMotivos(Usuario pUsuario)
        {
            DbDataReader reader = default(DbDataReader);
            List<TrasladarClientes> lstPrograma = new List<TrasladarClientes>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = " SELECT * FROM MOTIVOS_TRASLADOS_CLIENTES";


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        reader = cmdTransaccionFactory.ExecuteReader();


                        while (reader.Read())
                        {
                            TrasladarClientes entidad = new TrasladarClientes();

                            if (reader["COD_MOTIVO_TRASLADO"] != DBNull.Value) entidad.COD_MOTIVO_TRASLADO = Convert.ToInt32(reader["COD_MOTIVO_TRASLADO"].ToString());
                            if (reader["DESCRIPCION"] != DBNull.Value) entidad.DESCRIPCION = reader["DESCRIPCION"].ToString();
                           
                            
                           lstPrograma.Add(entidad);
                        }

                        return lstPrograma;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProgramaData", "ListarPrograma", ex);
                        return null;
                    }
                }
            }
        }


    }
}
