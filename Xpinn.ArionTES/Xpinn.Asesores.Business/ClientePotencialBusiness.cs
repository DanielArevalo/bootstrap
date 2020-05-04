using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Asesores.Entities;
using Xpinn.Asesores.Entities.Common;
using Xpinn.Asesores.Data;
using Xpinn.Util;
using System.IO;

namespace Xpinn.Asesores.Business
{
    public class ClientePotencialBusiness : GlobalData
    {
        private ClientePotencialData dataClientePotencial;
        protected AsUdMotivoAfiliacionData dataMotAfiliacion;

        public ClientePotencialBusiness()
        {
            dataClientePotencial = new ClientePotencialData();
            dataMotAfiliacion = new AsUdMotivoAfiliacionData();
        }

        public ClientePotencial CrearClientePotencial(ClientePotencial pAseEntiClientePotencial, Usuario pUsuario)
        {
            try
            {
                BarriosData objBarriosData = new BarriosData();

                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pAseEntiClientePotencial = dataClientePotencial.CrearCliente(pAseEntiClientePotencial, pUsuario);
                    dataMotAfiliacion.CrearMotivoAfiliacion(pAseEntiClientePotencial, pUsuario);
                    ts.Complete();
                }
                return pAseEntiClientePotencial;
            }
            catch (TransactionException ex)
            {
                BOExcepcion.Throw("BusinessClientePotencial", "CrearClientePotencial", ex);
                return null;
            }
        }

        public bool CrearClientesPotenciales(List<ClientePotencial> pClientes, Usuario pUsuario,int limpiar, List<ErroresCarga> pErrores)
        {
            try
            {
                BarriosData objBarriosData = new BarriosData();
                EjecutivoData objEjecutivoData = new EjecutivoData();
                int NumeroLinea = 1;
                List<ClientePotencial> lsObjetosConError = new List<ClientePotencial>();
                if (limpiar==1)
                {
                    dataClientePotencial.LimpiarClientes(pUsuario);
                }
                foreach (ClientePotencial objClientePotencial in pClientes)
                {
                    using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                    {
                        try
                        {
                            NumeroLinea++;
                            //Valida la existencia de la zona
                            Barrios objBarrioConsulta = objBarriosData.ConsultarCodigo(objClientePotencial.Zona.IdZona, pUsuario);
                            if (objBarrioConsulta == null)
                            {
                                RegistrarError(NumeroLinea, objClientePotencial.Zona.IdZona.ToString(), "No se encuentra encuentra registrada una zona con el código " + objClientePotencial.Zona.IdZona, objClientePotencial.Zona.IdZona.ToString(), pErrores);
                                lsObjetosConError.Add(objClientePotencial);
                                continue;
                            }

                            //Valida la existencia del ejecutivo
                            try
                            {
                                Ejecutivo objEjecutivoConsulta = objEjecutivoData.ConsultarDatosEjecutivo(objClientePotencial.codasesor, pUsuario);
                            }
                            catch
                            {
                                RegistrarError(NumeroLinea, objClientePotencial.codasesor.ToString(), "No se encuentra un ejecutivo con el código " + objClientePotencial.codasesor, objClientePotencial.codasesor.ToString(), pErrores);
                                lsObjetosConError.Add(objClientePotencial);
                                continue;
                            }

                            dataClientePotencial.CrearCliente(objClientePotencial, pUsuario);
                            dataMotAfiliacion.CrearMotivoAfiliacion(objClientePotencial, pUsuario);
                            ts.Complete();
                        }
                        catch (Exception ex)
                        {
                            ErroresCarga registro = new ErroresCarga();
                            registro.numero_registro = NumeroLinea.ToString();
                            registro.error = "Error: " + ex.Message;
                            pErrores.Add(registro);
                            lsObjetosConError.Add(objClientePotencial);
                        }
                    }
                }

                foreach (ClientePotencial ClienteError in lsObjetosConError)
                {
                    pClientes.Remove(ClienteError);
                }

                return pErrores.Count == 0;
            }
            catch (TransactionException ex)
            {
                BOExcepcion.Throw("BusinessCliente", "CrearCliente", ex);
                return false;
            }
        }

        public void RegistrarError(int pNumeroLinea, string pRegistro, string pError, string pDato, List<ErroresCarga> plstErrores)
        {
            if (pNumeroLinea == -1)
            {
                plstErrores.Clear();
                return;
            }
            ErroresCarga registro = new ErroresCarga();
            registro.numero_registro = pNumeroLinea.ToString();
            registro.datos = pDato;
            registro.error = " Campo No.:" + pRegistro + " Error:" + pError;
            plstErrores.Add(registro);
        }

        public void EliminarClientePotencial(Int64 pIdClientePotencial, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    dataMotAfiliacion.Eliminar(pIdClientePotencial, pUsuario);
                    dataClientePotencial.EliminarCliente(pIdClientePotencial, pUsuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BusinessClientePotencial", "EliminarClientePotencial", ex);
            }
        }

        public ClientePotencial ConsultarClientePotencial(Int64 pIdAseEntiClientePotencial, Usuario pUsuario)
        {
            try
            {
                return dataClientePotencial.ConsultarCliente(pIdAseEntiClientePotencial, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BusinessClientePotencial", "ConsultarClientePotencial", ex);
                return null;
            }

        }

        public List<ClientePotencial> ListarClientePotencial(ClientePotencial pAseEntiClientePotencial, Usuario pUsuario)
        {
            try
            {
                return dataClientePotencial.ListarCliente(pAseEntiClientePotencial, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BusinessClientePotencial", "ListarClientePotencial", ex);
                return null;
            }
        }

        public ClientePotencial ActualizarClientePotencial(ClientePotencial pAseEntiClientePotencial, Usuario pUsuario)
        {
            //try
            //{
            //    using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            //    {
            pAseEntiClientePotencial = dataClientePotencial.ActualizarCliente(pAseEntiClientePotencial, pUsuario);
            //    ts.Complete();
            //}
            return pAseEntiClientePotencial;
            //}
            //catch (TransactionException ex)
            //{
            //    BOExcepcion.Throw("BusinessClientePotencial", "ActualizarClientePotencial", ex);
            //    return null;
            //}
        }
        public ClientePotencial ConsultarClienteyaExistente(Int64 pIdAseEntiClientePotencial, Usuario pUsuario)
        {
            try
            {
                return dataClientePotencial.ConsultarClienteyaExistente(pIdAseEntiClientePotencial, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BusinessClientePotencial", "ConsultarClientePotencial", ex);
                return null;
            }

        }

        public ClientePotencial ConsultarUsuario(Int64 pIdUsuario, Usuario pUsuario)
        {
            try
            {
                return dataClientePotencial.ConsultarUsuario(pIdUsuario, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BusinessClientePotencial", "ConsultarUsuario", ex);
                return null;
            }

        }

    }
}