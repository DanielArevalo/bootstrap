using System;
using System.Collections.Generic;
using System.Transactions;
using Xpinn.Integracion.Data;
using Xpinn.Integracion.Entities;
using Xpinn.Util;
using Xpinn.Util.PaymentACH;

namespace Xpinn.Integracion.Business
{
    public class ACHBusiness : GlobalBusiness
    {
        private ACHData DAPayment;
        private IntegracionData BOIntegracionData;
        public ACHBusiness()
        {
            DAPayment = new ACHData();
            BOIntegracionData = new IntegracionData();
        }

        private ACHCredentials obtenerCredencialesACH(Usuario pUsuario)
        {
            ACHCredentials ach = new ACHCredentials();
            try
            {
                Entities.Integracion entidad = BOIntegracionData.ObtenerIntegracion(2, pUsuario);

                if (entidad != null && !string.IsNullOrEmpty(entidad.password))
                {
                    ach.TicketOfficeId = Convert.ToInt32(entidad.usuario);
                    ach.password = entidad.password;
                    if (!string.IsNullOrEmpty(entidad.datos))
                    {
                        //crea array vacio
                        string[] sParametros = new string[3] { "", "", "" };
                        //llena array
                        sParametros = entidad.datos.Split(';');

                        //Toma valores
                        ach.serviceCode = sParametros[0].Split(':')[1];
                        ach.use_ws_security = sParametros[1].Split(':')[1].ToString() == "1" ? true : false;
                        ach.dynamic_fields = sParametros[2].Split(':')[1];
                    }
                    return ach;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ACHBusiness", "obtenerCredencialesACH", ex);
                return null;
            }
        }

        #region METODOS BASE DE DATOS

        public ACHPayment CreatePaymentACHBusiness(ACHPayment pPaymentACH, Usuario pUsuario)
        {
            string _paso = "";
            ACHPayment pResult = null;
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, TimeSpan.MaxValue))
            {
                try
                {
                    string pError = string.Empty;
                    // CREANDO LA TRANSACCION retornando el id en base de datos
                    _paso = "Creando Registro en Financial";
                    pResult = DAPayment.CreateModifyPaymentACH(pPaymentACH, ref pError, OperacionCRUD.Crear, pUsuario);

                    //Si no creó la transacción en base de datos devuelve el error
                    if (!string.IsNullOrEmpty(pError))
                    {
                        pResult = new ACHPayment();
                        pResult.Success = false;
                        pResult.ErrorMessage = pError;
                    }
                    else
                    {
                        if (pResult != null)
                        {
                            //Segunda validación en caso de no insersión 
                            if (pResult.ID == 0)
                            {
                                pResult = new ACHPayment();
                                pResult.Success = false;
                                pResult.ErrorMessage = "SE GENERÓ UN ERROR AL CREAR LA TRANSACCIÓN";
                            }
                            else
                            {
                                _paso = "Obetener credenciales PSE";
                                //Si creó la transacción en base de datos intenta consumir el web service
                                pResult.Success = true;
                                //Obtiene las credenciales de ACH
                                ACHCredentials ach = obtenerCredencialesACH(pUsuario);
                                if (ach != null)
                                {
                                    Util.PaymentACH.PSEHostingField[] fields = new Util.PaymentACH.PSEHostingField[3];
                                    //construye fields                                    
                                    if (!string.IsNullOrEmpty(ach.dynamic_fields))
                                    {
                                        //campos
                                        string[] campos = ach.dynamic_fields.Split(',');
                                        //valores
                                        string[] valores = pPaymentACH.Fields.Split(',');

                                        if (campos.Length == valores.Length)
                                        {
                                            for (int i = 0; i < campos.Length; i++)
                                            {
                                                Util.PaymentACH.PSEHostingField fi = new Util.PaymentACH.PSEHostingField()
                                                {
                                                    Value = valores[i],
                                                    Name = campos[i]
                                                };
                                                fields[i] = fi;

                                            }
                                        }
                                    }

                                    //Ejecuta el servicio
                                    _paso = "realizando transacción PSE";
                                    pError = "";
                                    pResult = createTransactionPayment(pResult, ach, fields);
                                    if (pResult != null)
                                    {
                                        //Transacción procesada Actualiza
                                        _paso = "aplicando transacción PSE";
                                        pPaymentACH = DAPayment.CreateModifyPaymentACH(pResult, ref pError, OperacionCRUD.Modificar, pUsuario);
                                        //si presentó error al actualizar 
                                        if (!string.IsNullOrEmpty(pError))
                                        {
                                            pPaymentACH.Success = false;
                                            pPaymentACH.ErrorMessage = pError;
                                        }
                                    }
                                }
                                else
                                {
                                    pPaymentACH.Success = false;
                                    pPaymentACH.ErrorMessage = "No se pudo realizar el proceso por problemas en la parametrización";
                                }
                            }

                        }
                    }
                    ts.Complete();
                }
                catch (Exception ex)
                {
                    pResult = new ACHPayment();
                    pResult.Success = false;
                    pResult.ErrorMessage = _paso + ".." + ex.Message;
                    ts.Dispose();
                }
            }
            return pResult;
        }

        public ACHPayment ConsultPaymentACHBusiness(string filtro, ref string pError, Usuario vUsuario)
        {
            try
            {
                //Consulta la ultima transacción PSE 
                ACHPayment pPayment = DAPayment.ConsultPaymentACH(filtro, ref pError, vUsuario);
                if (pPayment != null)
                {
                    //Si no es estado final intenta actualizar antes de devolver el dato
                    if (pPayment.Status != Xpinn.Integracion.Entities.PaymentStatusEnum.approved && pPayment.Status != Entities.PaymentStatusEnum.rejected)
                    {
                        //Obtiene credenciales de ACH
                        ACHCredentials ach = obtenerCredencialesACH(vUsuario);
                        if (ach != null)
                        {
                            ACHPayment result = GetTransactionInformation(ach, pPayment, vUsuario);
                            if (result != null && result.ErrorMessage == "")
                            {
                                return result;
                            }
                        }
                    }
                }
                return pPayment;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PaymentACHBusiness", "ConsultPaymentACHBusiness", ex);
                return null;
            }
        }

        public bool UpdatePaymentsACH(long cod_persona, Usuario pUsuario)
        {
            try
            {
                string pError = "";
                string filtro = "where STATE not in (2,3) and identifier is not null and cod_persona = " + cod_persona;
                List<ACHPayment> lst = new List<ACHPayment>();
                lst = DAPayment.ListPaymentACH(filtro, ref pError, pUsuario);
                if (lst != null && lst.Count > 0)
                {
                    //Obtiene credenciales de ACH
                    ACHCredentials ach = obtenerCredencialesACH(pUsuario);
                    if (ach != null)
                    {
                        foreach (ACHPayment item in lst)
                        {
                            //Consulta el estado de la transacción y si ha cambiado lo actualiza aplicando el pago
                            GetTransactionInformation(ach, item, pUsuario);
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PaymentACHBusiness", "UpdatePaymentsACH", ex);
                return false;
            }
        }
        #endregion

        #region METODOS WS ACH
        public ACHPayment createTransactionPayment(ACHPayment pPaymentACH, ACHCredentials ach, Util.PaymentACH.PSEHostingField[] fields)
        {
            try
            {
                // GENERAR CONSUMO DE SERVICIO                          
                pPaymentACH.Entity_url = pPaymentACH.Entity_url + "?ID=" + pPaymentACH.ID;
                InterfazPaymentPSE PaymentServices = new InterfazPaymentPSE();
                PSEHostingCreateTransactionReturn ret = PaymentServices.CreateProcessPayment(pPaymentACH.Cod_persona,
                                       ach.TicketOfficeId,
                                       pPaymentACH.Amount,
                                       pPaymentACH.VATAmount,
                                       pPaymentACH.PaymentDescription,
                                       pPaymentACH.ReferenceNumber1,
                                       pPaymentACH.ReferenceNumber2,
                                       pPaymentACH.ReferenceNumber3,
                                       ach.serviceCode,
                                       pPaymentACH.Email,
                                       fields,
                                       pPaymentACH.Entity_url,
                                       pPaymentACH.TypeProduct,
                                       pPaymentACH.NumberProduct,
                                       Convert.ToInt32(pPaymentACH.ID));
                // VERIFICAR RESPUESTA
                pPaymentACH.Identifier = ret.PaymentIdentifier;
                if (ret.ReturnCode == PSEHostingCreateTransactionReturnCode.ERRORS)
                    pPaymentACH.Status = Entities.PaymentStatusEnum.failed;
                else
                {
                    pPaymentACH.Success = true;
                }
                return pPaymentACH;
            }
            catch (Exception e)
            {
                pPaymentACH.ErrorMessage = e.ToString();
                pPaymentACH.Status = Entities.PaymentStatusEnum.failed;
                return pPaymentACH;
            }
        }
        #endregion       
        public ACHPayment GetTransactionInformation(ACHCredentials ach, ACHPayment item, Usuario pUsuario)
        {
            try
            {
                string pError = "";
                //VerifyPayment
                InterfazPaymentPSE PaymentServices = new InterfazPaymentPSE();
                PSEHostingTransactionInformationReturn inf = PaymentServices.VerifyPayment(ach.TicketOfficeId,ach.password, item.ID.ToString());

                //--IMPORTANT! obtener respuesta en variable inf                        
                //Guarda la información de la transacción
                if (inf != null)
                {                    
                    //Compara cambio de estado, almacena si ha cambiado 
                    if (inf.State.ToString() != item.Status.ToString())
                    {
                        switch (inf.State.ToString().ToUpper())
                        {
                            case "CREATED":
                                item.Status = Entities.PaymentStatusEnum.created;
                                break;
                            case "INVALIDPAYMENTID":
                                item.Status = Entities.PaymentStatusEnum.rejected;
                                break;
                            case "PENDING":
                                item.Status = Entities.PaymentStatusEnum.pending;
                                break;
                            case "FAILED":
                                item.Status = Entities.PaymentStatusEnum.failed;
                                break;
                            case "NOT_AUTHORIZED":
                                item.Status = Entities.PaymentStatusEnum.rejected;
                                break;
                            case "OK":
                                item.Status = Entities.PaymentStatusEnum.approved;
                                break;
                            default:
                                item.Status = Entities.PaymentStatusEnum.pending;
                                break;
                        }
                        item.ErrorMessage = inf.ErrorMessage;                         
                        item.BankCode = inf.BankCode;
                        item.BankName = inf.BankName;
                        item.TrazabilityCode = inf.TrazabilityCode;
                        item = DAPayment.CreateModifyPaymentACH(item, ref pError, OperacionCRUD.Modificar, pUsuario);
                        return item;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                item.ErrorMessage = ex.Message;
                return item;
            }
        }
        public List<ACHPayment> ListPaymentACHBusiness(string filtro, ref string pError, Usuario vUsuario)
        {
            try
            {
                return DAPayment.ListPaymentACH(filtro, ref pError, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PaymentACHBusiness", "ListPaymentACHBusiness", ex);
                return null;
            }
        }

        public ACHPayment ModifyPaymentACHBusiness(ACHPayment pPaymentACH, Usuario pUsuario)
        {
            ACHPayment pResult = null;
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, TimeSpan.MaxValue))
            {
                try
                {
                    string pError = string.Empty;

                    // MODIFY PAYMENT
                    pResult = DAPayment.CreateModifyPaymentACH(pPaymentACH, ref pError, OperacionCRUD.Modificar, pUsuario);

                    if (!string.IsNullOrEmpty(pError))
                    {
                        pResult = new ACHPayment();
                        pResult.Success = false;
                        pResult.ErrorMessage = pError;
                    }
                    else
                    {
                        if (pResult != null)
                        {
                            if (pResult.ID == 0)
                            {
                                pResult = new ACHPayment();
                                pResult.Success = false;
                                pResult.ErrorMessage = "SE GENERÓ UN ERROR AL CREAR LA TRANSACCIÓN";
                            }
                            else
                                pResult.Success = true;
                        }
                    }

                    ts.Complete();
                }
                catch (Exception ex)
                {
                    pResult = new ACHPayment();
                    pResult.Success = false;
                    pResult.ErrorMessage = ex.Message;
                    ts.Dispose();
                }
            }
            return pResult;
        }

        public string getCredencialesACH(Usuario pUsuario)
        {
            string _configuracion = "";
            ACHCredentials ach = obtenerCredencialesACH(pUsuario);
            if (ach != null)
            {
                _configuracion = ach.TicketOfficeId + ";" + ach.serviceCode + ";" + ach.use_ws_security + ";" + AppConstants.PSE_URL + ";" + AppConstants.USE_WS_SECURITY;
                Util.PaymentACH.PSEHostingField[] fields = new Util.PaymentACH.PSEHostingField[3];
                //construye fields                                    
                if (!string.IsNullOrEmpty(ach.dynamic_fields))
                {
                    //campos
                    string[] campos = ach.dynamic_fields.Split(',');
                    for (int i = 0; i < campos.Length; i++)
                    {
                        _configuracion += ";" + campos[i];
                    }
                }
            }
            return _configuracion;
        }



    }
}
