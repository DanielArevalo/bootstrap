using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Data;
using Xpinn.FabricaCreditos.Entities;
using System.Transactions;

namespace Xpinn.FabricaCreditos.Business
{
    public class InformacionAdicionalBusiness : GlobalData
    {

        private InformacionAdicionalData DAInformacionData;


        public InformacionAdicionalBusiness()
        {
            DAInformacionData = new InformacionAdicionalData();
        }


        #region PERSONA INFORMACION ADICIONAL

        public InformacionAdicional CrearTipo_InforAdicional(InformacionAdicional pAdicional, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (pAdicional.lstInfor != null)
                    {
                        int num = 0;
                        foreach (InformacionAdicional nInforma in pAdicional.lstInfor)
                        {
                            InformacionAdicional eInfo = new InformacionAdicional();
                            eInfo = DAInformacionData.CrearTipo_InforAdicional(nInforma, vUsuario);
                            num += 1;
                        }                        
                    }
                    
                    ts.Complete();
                }
                return pAdicional;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InformacionAdicionalBusiness", "CrearTipo_InforAdicional", ex);
                return null;
            }
        }


        public InformacionAdicional ModificarTipo_InforAdicional(InformacionAdicional pAdicional, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (pAdicional.lstInfor != null)
                    {                        
                        int num = 0;
                        foreach (InformacionAdicional nInforma in pAdicional.lstInfor)
                        {
                            InformacionAdicional eInfo = new InformacionAdicional();
                            if (nInforma.cod_infadicional <= 0 || nInforma.cod_infadicional == 0)
                                eInfo = DAInformacionData.CrearTipo_InforAdicional(nInforma, vUsuario);
                            else
                                eInfo = DAInformacionData.ModificarTipo_InforAdicional(nInforma, vUsuario);
                            num += 1;
                        }
                    }

                    ts.Complete();
                }
                return pAdicional;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InformacionAdicionalBusiness", "ModificarTipo_InforAdicional", ex);
                return null;
            }
        }


        public void EliminarInformacionAdicional(Int64 pIdActividad, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAInformacionData.EliminarInformacionAdicional(pIdActividad, vUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InformacionAdicionalBusiness", "EliminarInformacionAdicional", ex);
            }
        }


        #endregion


        public List<InformacionAdicional> ListarInformacionAdicional(InformacionAdicional pInformacion, string tipo, Usuario vUsuario)
        {
            try
            {
                return DAInformacionData.ListarInformacionAdicional(pInformacion,tipo, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InformacionAdicionalBusiness", "ListarInformacionAdicional", ex);
                return null;
            }
        }


        public List<InformacionAdicional> ListarInformacionAdicionalGeneral(InformacionAdicional pInformacion, Usuario vUsuario)
        {
            try
            {
                return DAInformacionData.ListarInformacionAdicionalGeneral(pInformacion, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InformacionAdicionalBusiness", "ListarInformacionAdicionalGeneral", ex);
                return null;
            }
        }



        public List<InformacionAdicional> ConsultarInformacionAdicional(InformacionAdicional pInfo, Usuario vUsuario)
        {
            try
            {
                return DAInformacionData.ConsultarInformacionAdicional(pInfo, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InformacionAdicionalBusiness", "ConsultarInformacionAdicional", ex);
                return null;
            }
        }

        public InformacionAdicional CrearPersona_InfoAdicional(InformacionAdicional pAdicional, Usuario usuario)
        {
            try
            {
                return DAInformacionData.CrearPersona_InfoAdicional(pAdicional, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InformacionAdicionalBusiness", "CrearPersona_InfoAdicional", ex);
                return null;
            }
        }

        public string ConsultarInformacionPersonalDeUnaPersona(long codigoPersona, long codigoTipoInformacion, Usuario vUsuario)
        {
            try
            {
                return DAInformacionData.ConsultarInformacionPersonalDeUnaPersona(codigoPersona, codigoTipoInformacion, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InformacionAdicionalBusiness", "ConsultarInformacionPersonalDeUnaPersona", ex);
                return null;
            }
        }

        public void ModificarPersona_InfoAdicional(InformacionAdicional pAdicional, Usuario usuario)
        {
            try
            {
                DAInformacionData.ModificarPersonaInformacionAdicional(pAdicional, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActividadPersonaService", "ModificarPersonaInformacionAdicional", ex);
            }
        }

        public List<InformacionAdicional> ListarPersonaInformacion(Int64 pCod, string tipo_persona, Usuario vUsuario)
        {
            try
            {
                return DAInformacionData.ListarPersonaInformacion(pCod,tipo_persona, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InformacionAdicionalBusiness", "ListarPersonaInformacion", ex);
                return null;
            }
        }
        public bool ActualizacionMasiva(List<InformacionAdicional> pEntidad, Usuario vUsuario,List<ErroresCarga> pErrores)
        {
            try
            {
                Int64 NumeroLinea=1;

                List<InformacionAdicional> lsObjetosConError = new List<InformacionAdicional>();
              
                foreach (InformacionAdicional objClientePotencial in pEntidad)
                {
                    using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                    {
                        try
                        {

                            DAInformacionData.ActualizacionMasiva(objClientePotencial, vUsuario);
                        
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
                    NumeroLinea = NumeroLinea + 1;
                }

                foreach (InformacionAdicional ClienteError in lsObjetosConError)
                {
                    pEntidad.Remove(ClienteError);
                }

                return pErrores.Count == 0;
            }
            catch (TransactionException ex)
            {
                BOExcepcion.Throw("BusinessCliente", "CrearCliente", ex);
                return false;
            }
        }
        public void EliminarActividadPersona(Int64 pIdActividad, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAInformacionData.EliminarActividadPersona(pIdActividad, vUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InformacionAdicionalBusiness", "EliminarActividadPersona", ex);
            }
        }

    }
}
