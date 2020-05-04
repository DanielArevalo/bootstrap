using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using Xpinn.Asesores.Entities;
using Xpinn.Asesores.Entities.Common;
using System.Data.Common;
using System.Data;

namespace Xpinn.Asesores.Data
{
    public class BarriosData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public BarriosData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public List<Barrios> ListarBarrios(int tipos, Usuario pUsuario)
        {

            DbDataReader reader = default(DbDataReader);
            List<Barrios> lstAseEntbarrios = new List<Barrios>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {

                    string query = "SELECT * FROM CIUDADES WHERE TIPO =" + tipos;
                    connection.Open();
                    cmdTransaccionFactory.Connection = connection;
                    cmdTransaccionFactory.CommandType = CommandType.Text;
                    cmdTransaccionFactory.CommandText = query;
                    reader = cmdTransaccionFactory.ExecuteReader();

                    while (reader.Read())
                    {
                        Barrios aseEntTipoDoc = new Barrios();

                        if (reader["CODCIUDAD"] != DBNull.Value) aseEntTipoDoc.CODCIUDAD = Convert.ToInt64(reader["CODCIUDAD"].ToString());
                        if (reader["NOMCIUDAD"] != DBNull.Value) aseEntTipoDoc.NOMCIUDAD = reader["NOMCIUDAD"].ToString();

                        lstAseEntbarrios.Add(aseEntTipoDoc);
                    }

                    return lstAseEntbarrios;



                }
            }
        }

        public Barrios ConsultarCodigo(long pCodigo , Usuario pUsuario)
        {
            DbDataReader reader = default(DbDataReader);
            Barrios objBarrio = null;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {

                    string query = "SELECT * FROM CIUDADES WHERE CODCIUDAD =" + pCodigo;

                    connection.Open();
                    cmdTransaccionFactory.Connection = connection;
                    cmdTransaccionFactory.CommandType = CommandType.Text;
                    cmdTransaccionFactory.CommandText = query;
                    reader = cmdTransaccionFactory.ExecuteReader();

                    while (reader.Read())
                    {
                        objBarrio = new Barrios();
                        if (reader["CODCIUDAD"] != DBNull.Value) objBarrio.CODCIUDAD = Convert.ToInt64(reader["CODCIUDAD"].ToString());
                        if (reader["NOMCIUDAD"] != DBNull.Value) objBarrio.NOMCIUDAD = reader["NOMCIUDAD"].ToString();
                    }

                    return objBarrio;
                }
            }
        }

        public List<Ejecutivo> listarejecutivoszona(int cod, Usuario pUsuario)
        {

            DbDataReader reader = default(DbDataReader);
            List<Ejecutivo> lstAseEeje = new List<Ejecutivo>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {

                    string query = "SELECT ICODIGO,trim(trim(SNOMBRE1)||' '|| trim(SNOMBRE2)||' '|| trim(SAPELLIDO1) ||' '|| trim(SAPELLIDO2)) as NOMBRECOMPLETO "
                                      + "FROM ASEJECUTIVOS  WHERE IESTADO = 1 ";

                    if(cod != 0)
                        query += " and ICODIGO = "+cod;

                    query += " ORDER BY 2";

                    connection.Open();
                    cmdTransaccionFactory.Connection = connection;
                    cmdTransaccionFactory.CommandType = CommandType.Text;
                    cmdTransaccionFactory.CommandText = query;
                    reader = cmdTransaccionFactory.ExecuteReader();

                    while (reader.Read())
                    {
                        Barrios aseEntTipoDoc = new Barrios();
                        Ejecutivo ejec = new Ejecutivo();
                        if (reader["icodigo"] != DBNull.Value) ejec.Codigo = Convert.ToString(reader["icodigo"].ToString());
                        if (reader["NOMBRECOMPLETO"] != DBNull.Value) ejec.NombreCompleto = reader["NOMBRECOMPLETO"].ToString();

                        lstAseEeje.Add(ejec);
                    }

                    return lstAseEeje;



                }
            }
        }
    }
}
