using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla consultasdatacredito
    /// </summary>
    public class consultasdatacreditoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla consultasdatacredito
        /// </summary>
        public consultasdatacreditoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla consultasdatacredito de la base de datos
        /// </summary>
        /// <param name="pconsultasdatacredito">Entidad consultasdatacredito</param>
        /// <returns>Entidad consultasdatacredito creada</returns>
        public consultasdatacredito Crearconsultasdatacredito(consultasdatacredito pconsultasdatacredito, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pNUMEROFACTURA = cmdTransaccionFactory.CreateParameter();
                        pNUMEROFACTURA.ParameterName = "p_NUMEROFACTURA";
                        pNUMEROFACTURA.Value = pconsultasdatacredito.numerofactura;
                        pNUMEROFACTURA.Direction = ParameterDirection.InputOutput;

                        DbParameter pFECHACONSULTA = cmdTransaccionFactory.CreateParameter();
                        pFECHACONSULTA.ParameterName = "p_FECHACONSULTA";
                        pFECHACONSULTA.Value = pconsultasdatacredito.fechaconsulta;

                        DbParameter pCEDULACLIENTE = cmdTransaccionFactory.CreateParameter();
                        pCEDULACLIENTE.ParameterName = "p_CEDULACLIENTE";
                        pCEDULACLIENTE.Value = pconsultasdatacredito.cedulacliente;

                        DbParameter pUSUARIO = cmdTransaccionFactory.CreateParameter();
                        pUSUARIO.ParameterName = "p_USUARIO";
                        pUSUARIO.Value = pconsultasdatacredito.usuario;

                        DbParameter pIP = cmdTransaccionFactory.CreateParameter();
                        pIP.ParameterName = "p_IP";
                        pIP.Value = pconsultasdatacredito.ip;

                        DbParameter pOFICINA = cmdTransaccionFactory.CreateParameter();
                        pOFICINA.ParameterName = "p_OFICINA";
                        pOFICINA.Value = pconsultasdatacredito.oficina;

                        DbParameter pVALORCONSULTA = cmdTransaccionFactory.CreateParameter();
                        pVALORCONSULTA.ParameterName = "p_VALORCONSULTA";
                        pVALORCONSULTA.Value = pconsultasdatacredito.valorconsulta;

                        DbParameter pPUNTAJE = cmdTransaccionFactory.CreateParameter();
                        pPUNTAJE.ParameterName = "p_PUNTAJE";
                        pPUNTAJE.Value = pconsultasdatacredito.puntaje;


                        cmdTransaccionFactory.Parameters.Add(pNUMEROFACTURA);
                        cmdTransaccionFactory.Parameters.Add(pFECHACONSULTA);
                        cmdTransaccionFactory.Parameters.Add(pCEDULACLIENTE);
                        cmdTransaccionFactory.Parameters.Add(pUSUARIO);
                        cmdTransaccionFactory.Parameters.Add(pIP);
                        cmdTransaccionFactory.Parameters.Add(pOFICINA);
                        cmdTransaccionFactory.Parameters.Add(pVALORCONSULTA);
                        cmdTransaccionFactory.Parameters.Add(pPUNTAJE);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_CONDC_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pconsultasdatacredito, "consultasdatacredito",pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pconsultasdatacredito.numerofactura = Convert.ToInt64(pNUMEROFACTURA.Value);
                        return pconsultasdatacredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("consultasdatacreditoData", "Crearconsultasdatacredito", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla consultasdatacredito de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad consultasdatacredito modificada</returns>
        public consultasdatacredito Modificarconsultasdatacredito(consultasdatacredito pconsultasdatacredito, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pNUMEROFACTURA = cmdTransaccionFactory.CreateParameter();
                        pNUMEROFACTURA.ParameterName = "p_NUMEROFACTURA";
                        pNUMEROFACTURA.Value = pconsultasdatacredito.numerofactura;

                        DbParameter pFECHACONSULTA = cmdTransaccionFactory.CreateParameter();
                        pFECHACONSULTA.ParameterName = "p_FECHACONSULTA";
                        pFECHACONSULTA.Value = pconsultasdatacredito.fechaconsulta;

                        DbParameter pCEDULACLIENTE = cmdTransaccionFactory.CreateParameter();
                        pCEDULACLIENTE.ParameterName = "p_CEDULACLIENTE";
                        pCEDULACLIENTE.Value = pconsultasdatacredito.cedulacliente;

                        DbParameter pUSUARIO = cmdTransaccionFactory.CreateParameter();
                        pUSUARIO.ParameterName = "p_USUARIO";
                        pUSUARIO.Value = pconsultasdatacredito.usuario;

                        DbParameter pIP = cmdTransaccionFactory.CreateParameter();
                        pIP.ParameterName = "p_IP";
                        pIP.Value = pconsultasdatacredito.ip;

                        DbParameter pOFICINA = cmdTransaccionFactory.CreateParameter();
                        pOFICINA.ParameterName = "p_OFICINA";
                        pOFICINA.Value = pconsultasdatacredito.oficina;

                        DbParameter pVALORCONSULTA = cmdTransaccionFactory.CreateParameter();
                        pVALORCONSULTA.ParameterName = "p_VALORCONSULTA";
                        pVALORCONSULTA.Value = pconsultasdatacredito.valorconsulta;

                        cmdTransaccionFactory.Parameters.Add(pNUMEROFACTURA);
                        cmdTransaccionFactory.Parameters.Add(pFECHACONSULTA);
                        cmdTransaccionFactory.Parameters.Add(pCEDULACLIENTE);
                        cmdTransaccionFactory.Parameters.Add(pUSUARIO);
                        cmdTransaccionFactory.Parameters.Add(pIP);
                        cmdTransaccionFactory.Parameters.Add(pOFICINA);
                        cmdTransaccionFactory.Parameters.Add(pVALORCONSULTA);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_Xpinn_FabricaCreditos_consultasdatacredito_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pconsultasdatacredito, "consultasdatacredito",pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA

                        return pconsultasdatacredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("consultasdatacreditoData", "Modificarconsultasdatacredito", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla consultasdatacredito de la base de datos
        /// </summary>
        /// <param name="pId">identificador de consultasdatacredito</param>
        public void Eliminarconsultasdatacredito(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        consultasdatacredito pconsultasdatacredito = new consultasdatacredito();

                        if (pUsuario.programaGeneraLog)
                            pconsultasdatacredito = Consultarconsultasdatacredito(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pNUMEROFACTURA = cmdTransaccionFactory.CreateParameter();
                        pNUMEROFACTURA.ParameterName = "p_NUMEROFACTURA";
                        pNUMEROFACTURA.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pNUMEROFACTURA);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_Xpinn_FabricaCreditos_consultasdatacredito_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pconsultasdatacredito, "consultasdatacredito", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("consultasdatacreditoData", "Eliminarconsultasdatacredito", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla consultasdatacredito de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla consultasdatacredito</param>
        /// <returns>Entidad consultasdatacredito consultado</returns>
        public consultasdatacredito Consultarconsultasdatacredito(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            consultasdatacredito entidad = new consultasdatacredito();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  CONSULTASDATACREDITO WHERE NUMEROFACTURA = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["NUMEROFACTURA"] != DBNull.Value) entidad.numerofactura = Convert.ToInt64(resultado["NUMEROFACTURA"]);
                            if (resultado["FECHACONSULTA"] != DBNull.Value) entidad.fechaconsulta = Convert.ToDateTime(resultado["FECHACONSULTA"]);
                            if (resultado["CEDULACLIENTE"] != DBNull.Value) entidad.cedulacliente = Convert.ToString(resultado["CEDULACLIENTE"]);
                            if (resultado["USUARIO"] != DBNull.Value) entidad.usuario = Convert.ToString(resultado["USUARIO"]);
                            if (resultado["IP"] != DBNull.Value) entidad.ip = Convert.ToString(resultado["IP"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["VALORCONSULTA"] != DBNull.Value) entidad.valorconsulta = Convert.ToInt64(resultado["VALORCONSULTA"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        return entidad;
                    }
                    catch(Exception ex)
                    {
                        BOExcepcion.Throw("consultasdatacreditoData", "Consultarconsultasdatacredito", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla consultasdatacredito dados unos filtros
        /// </summary>
        /// <param name="pconsultasdatacredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de consultasdatacredito obtenidos</returns>
        public List<consultasdatacredito> Listarconsultasdatacredito(consultasdatacredito pconsultasdatacredito, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<consultasdatacredito> lstconsultasdatacredito = new List<consultasdatacredito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  CONSULTASDATACREDITO " ;
                        if (pconsultasdatacredito.cedulacliente != null)
                            sql = " SELECT * FROM CONSULTASDATACREDITO where cedulacliente ='" + pconsultasdatacredito.cedulacliente + "' and numerofactura in (SELECT max(numerofactura) FROM  CONSULTASDATACREDITO where cedulacliente ='" + pconsultasdatacredito.cedulacliente + "')";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            consultasdatacredito entidad = new consultasdatacredito();

                            if (resultado["NUMEROFACTURA"] != DBNull.Value) entidad.numerofactura = Convert.ToInt64(resultado["NUMEROFACTURA"]);
                            if (resultado["FECHACONSULTA"] != DBNull.Value) entidad.fechaconsulta = Convert.ToDateTime(resultado["FECHACONSULTA"]);
                            if (resultado["CEDULACLIENTE"] != DBNull.Value) entidad.cedulacliente = Convert.ToString(resultado["CEDULACLIENTE"]);
                            if (resultado["USUARIO"] != DBNull.Value) entidad.usuario = Convert.ToString(resultado["USUARIO"]);
                            if (resultado["IP"] != DBNull.Value) entidad.ip = Convert.ToString(resultado["IP"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["VALORCONSULTA"] != DBNull.Value) entidad.valorconsulta = Convert.ToInt64(resultado["VALORCONSULTA"]);

                            lstconsultasdatacredito.Add(entidad);
                        }

                        return lstconsultasdatacredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("consultasdatacreditoData", "Listarconsultasdatacredito", ex);
                        return null;
                    }
                }
            }
        }




        public List<CreditoEmpresaRecaudo> ListarPersona_Empresa_Recaudo(Int64 pIdPersona, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<CreditoEmpresaRecaudo> lstEmpresa = new List<CreditoEmpresaRecaudo>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select p.cod_empresa, e.nom_empresa from persona_empresa_recaudo p "
                                  + "left join empresa_recaudo e on e.cod_empresa = p.cod_empresa and p.cod_persona = " + pIdPersona.ToString() + " where e.estado = 1 order by 1";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            CreditoEmpresaRecaudo entidad = new CreditoEmpresaRecaudo();
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt32(resultado["COD_EMPRESA"]);
                            if (resultado["NOM_EMPRESA"] != DBNull.Value) entidad.nom_empresa = Convert.ToString(resultado["NOM_EMPRESA"]);
                            lstEmpresa.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstEmpresa;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("consultasdatacreditoData", "ListarPersona_Empresa_Recaudo", ex);
                        return null;
                    }
                }
            }
        }


        public List<CreditoEmpresaRecaudo> ListarCreditoEmpresa_Recaudo(Int64 pNumRadicacion, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<CreditoEmpresaRecaudo> lstEmpresa = new List<CreditoEmpresaRecaudo>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select C.Cod_Empresa,E.Nom_Empresa,C.Porcentaje,C.valor From Credito_Empresa_Recaudo C Left Join Empresa_Recaudo E "
                                     +"on C.Cod_Empresa = E.Cod_Empresa where E.Estado = 1 and Numero_Radicacion = " + pNumRadicacion.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            CreditoEmpresaRecaudo entidad = new CreditoEmpresaRecaudo();
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt32(resultado["COD_EMPRESA"]);
                            if (resultado["NOM_EMPRESA"] != DBNull.Value) entidad.nom_empresa = Convert.ToString(resultado["NOM_EMPRESA"]);
                            if (resultado["PORCENTAJE"] != DBNull.Value) entidad.porcentaje = Convert.ToDecimal(resultado["PORCENTAJE"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            lstEmpresa.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstEmpresa;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("consultasdatacreditoData", "ListarCreditoEmpresa_Recaudo", ex);
                        return null;
                    }
                }
            }
        }


        public List<CreditoEmpresaRecaudo> ListarEmpresa_Recaudo(Usuario vUsuario)
        {
            DbDataReader resultado;
            List<CreditoEmpresaRecaudo> lstEmpresa = new List<CreditoEmpresaRecaudo>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select * from empresa_recaudo";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            CreditoEmpresaRecaudo entidad = new CreditoEmpresaRecaudo();
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt32(resultado["COD_EMPRESA"]);
                            if (resultado["NOM_EMPRESA"] != DBNull.Value) entidad.nom_empresa = Convert.ToString(resultado["NOM_EMPRESA"]);
                            lstEmpresa.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstEmpresa;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("consultasdatacreditoData", "ListarCreditoEmpresa_Recaudo", ex);
                        return null;
                    }
                }
            }
        }

        public List<ControlCreditos> ListarObservacionesCreditos(Int64 pNumRadicacion, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ControlCreditos> lstEmpresa = new List<ControlCreditos>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select Idcontrol,Fechaproceso,Observaciones From Controlcreditos where Numero_Radicacion =  " + pNumRadicacion.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ControlCreditos entidad = new ControlCreditos();
                            if (resultado["Idcontrol"] != DBNull.Value) entidad.idcontrol = Convert.ToInt32(resultado["Idcontrol"]);
                            if (resultado["Fechaproceso"] != DBNull.Value) entidad.fechaproceso = Convert.ToDateTime(resultado["Fechaproceso"]);
                            if (resultado["Observaciones"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["Observaciones"]);
                            lstEmpresa.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstEmpresa;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("consultasdatacreditoData", "ListarObservacionesCreditos", ex);
                        return null;
                    }
                }
            }
        }


    }
}