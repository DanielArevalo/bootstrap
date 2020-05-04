using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Data;

namespace Xpinn.FabricaCreditos.Business
{

    public class AvanceBusiness : GlobalData
    {
        private AvanceData BAAvance;
        private CreditoData DACredito;

        /// <summary>
        /// Constructor del objeto de negocio para Credito
        /// </summary>
        public AvanceBusiness()
        {
            BAAvance = new AvanceData();
        }


        public Avance CrearCreditoAvance(Avance pAvance, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pAvance = BAAvance.CrearCreditoAvance(pAvance, vUsuario);

                    ts.Complete();
                }

                return pAvance;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AvanceBusiness", "CrearCreditoAvance", ex);
                return null;
            }
        }



        public void EliminarCreditoAvance(Int64 pId, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    BAAvance.EliminarCreditoAvance(pId, vUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AvanceBusiness", "EliminarCreditoAvance", ex);
            }
        }


        public List<Avance> ListarCreditoRotativos(Avance pRotativo, DateTime pFecha, Usuario pUsuario, String filtro)
        {
            try
            {
                return BAAvance.ListarCreditoRotativos(pRotativo, pFecha, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AvanceBusiness", "ListarCreditoRotativos", ex);
                return null;
            }
        }


        public Avance ConsultarCreditoRotativo(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BAAvance.ConsultarCreditoRotativo(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AvanceBusiness", "ConsultarCreditoRotativo", ex);
                return null;
            }
        }

        public Avance ConsultarTasaCreditoTotativo(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BAAvance.ConsultarTasaCreditoTotativo(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AvanceBusiness", "ConsultarTasaCreditoTotativo", ex);
                return null;
            }
        }

        public Avance ConsultarPlazoCreditoTotativo(String pNombre, Usuario pUsuario)
        {
            try
            {
                return BAAvance.ConsultarPlazoCreditoTotativo(pNombre, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AvanceBusiness", "ConsultarPlazoCreditoTotativo", ex);
                return null;
            }
        }

        public Avance ConsultarPlazoMaximoCredito(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BAAvance.ConsultarPlazoMaximoCredito(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AvanceBusiness", "ConsultarPlazoMaximoCredito", ex);
                return null;
            }
        }

        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            try
            {
                return BAAvance.ObtenerSiguienteCodigo(pUsuario);
            }
            catch
            {
                return 1;
            }
        }



        //LISTAR CREDITOS POR APROBAR

        public Avance ModificarCreditoAvance(Avance pAvance, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pAvance = BAAvance.ModificarCreditoAvance(pAvance, vUsuario);

                    ts.Complete();
                }

                return pAvance;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AvanceBusiness", "ModificarCreditoAvance", ex);
                return null;
            }
        }


        public Avance GrabarAprobacionDavance(Avance pAvance, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pAvance = BAAvance.GrabarAprobacionDavance(pAvance, vUsuario);

                    ts.Complete();
                }

                return pAvance;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AvanceBusiness", "GrabarAprobacionDavance", ex);
                return null;
            }
        }

        public List<Avance> ListarCreditoXaprobar(Avance pRotativo, DateTime pFecha, Usuario pUsuario, String filtro)
        {
            try
            {
                return BAAvance.ListarCreditoXaprobar(pRotativo, pFecha, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AvanceBusiness", "ListarCreditoXaprobar", ex);
                return null;
            }
        }

        public List<Avance> ListarCreditoXaprobar(Avance pRotativo, Usuario pUsuario, String filtro)
        {
            try
            {
                return BAAvance.ListarCreditoXaprobar(pRotativo, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AvanceBusiness", "ListarCreditoXaprobar", ex);
                return null;
            }
        }


        public Avance ConsultarCredRotativoXaprobar(Int64 pId, Usuario pUsuario)
        {
            try
            {
                Avance eAvance = new Avance();
                eAvance = BAAvance.ConsultarCredRotativoXaprobar(pId, pUsuario);

                if (eAvance != null)
                {
                    if (eAvance.numero_radicacion != null)
                    {
                        eAvance.lstDescuentosCredito = new List<DescuentosCredito>();
                        DescuentosCredito eDescCred = new DescuentosCredito();
                        eDescCred.numero_radicacion = eAvance.numero_radicacion;
                        eDescCred.cod_linea = Convert.ToInt32(eAvance.cod_linea_credito);
                        eAvance.lstDescuentosCredito = BAAvance.ListarDescuentosCredito(eDescCred, pUsuario);
                    }
                }
                return eAvance;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AvanceBusiness", "ConsultarCredRotativoXaprobar", ex);
                return null;
            }
        }

        public Avance ConsultarCredRotativoXaprobarXCredito(Int64 pId, Usuario pUsuario)
        {
            try
            {
                Avance eAvance = new Avance();
                eAvance = BAAvance.ConsultarCredRotativoXaprobarXCredito(pId, pUsuario);

                if (eAvance != null)
                {
                    if (eAvance.numero_radicacion != null)
                    {
                        eAvance.lstDescuentosCredito = new List<DescuentosCredito>();
                        DescuentosCredito eDescCred = new DescuentosCredito();
                        eDescCred.numero_radicacion = eAvance.numero_radicacion;
                        eDescCred.cod_linea = Convert.ToInt32(eAvance.cod_linea_credito);
                        eAvance.lstDescuentosCredito = BAAvance.ListarDescuentosCredito(eDescCred, pUsuario);
                    }
                }
                return eAvance;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AvanceBusiness", "ConsultarCredRotativoXaprobar", ex);
                return null;
            }
        }

        public Avance ValidarNumeroRadicacion(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BAAvance.ValidarNumeroRadicacion(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AvanceBusiness", "ValidarNumeroRadicacion", ex);
                return null;
            }
        }

        public ControlCreditos CrearControlCreditos(ControlCreditos pControl, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pControl = BAAvance.CrearControlCreditos(pControl, vUsuario);

                    ts.Complete();
                }

                return pControl;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AvanceBusiness", "CrearControlCreditos", ex);
                return null;
            }
        }

        public ControlCreditos ModificarControlCreditos(ControlCreditos pControl, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pControl = BAAvance.ModificarControlCreditos(pControl, vUsuario);

                    ts.Complete();
                }

                return pControl;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AvanceBusiness", "ModificarControlCreditos", ex);
                return null;
            }
        }




        public Giro CrearGiro(Giro pGiro, Usuario vUsuario, int opcion)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pGiro = BAAvance.CrearGiro(pGiro, vUsuario, opcion);

                    ts.Complete();
                }

                return pGiro;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AvanceBusiness", "CrearGiro", ex);
                return null;
            }
        }


        public DateTime ObtenerUltimaFecha(Usuario pUsuario)
        {
            try
            {
                return BAAvance.ObtenerUltimaFecha(pUsuario);
            }
            catch
            {
                return DateTime.Now;
            }
        }


        public Giro ConsultarFormaDesembolso(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BAAvance.ConsultarFormaDesembolso(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AvanceBusiness", "ConsultarFormaDesembolso", ex);
                return null;
            }
        }




        public ControlCreditos ConsultarCodigoPersonaXBanco(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return BAAvance.ConsultarCodigoPersonaXBanco(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AvanceBusiness", "ConsultarCodigoPersonaXBanco", ex);
                return null;
            }
        }


        public Avance ModificarDesembolsoAvance(Avance pAvance, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pAvance = BAAvance.ModificarDesembolsoAvance(pAvance, vUsuario);
                    ts.Complete();
                }

                return pAvance;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AvanceBusiness", "ModificarDesembolsoAvance", ex);
                return null;
            }
        }

        public Avance ModificarCuota(Avance pAvance, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pAvance = BAAvance.ModificarCuota(pAvance, vUsuario);
                    ts.Complete();
                }

                return pAvance;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AvanceBusiness", "ModificarCuota", ex);
                return null;
            }
        }



        public TRAN_CRED CrearTransaCred(TRAN_CRED pTransac, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTransac = BAAvance.CrearTransaCred(pTransac, vUsuario);
                    ts.Complete();
                }

                return pTransac;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AvanceBusiness", "CrearTransaCred", ex);
                return null;
            }
        }

        public Giro ModificarGiro(Giro pAvance, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pAvance = BAAvance.ModificarGiro(pAvance, vUsuario);
                    ts.Complete();
                }

                return pAvance;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AvanceBusiness", "ModificarGiro", ex);
                return null;
            }
        }

        public Avance fecha_ult_avance(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BAAvance.fecha_ult_avance(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AvanceBusiness", "fecha_ult_avance", ex);
                return null;
            }
        }

        public Avance ConsultarCuotaCredito(Avance avance, Usuario pUsuario)
        {
            try
            {
                return BAAvance.ConsultarCuotaCredito(avance, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AvanceBusiness", "ConsultarCuotaCredito", ex);
                return null;
            }
        }


        //tarjeta 

        public Avance CrearCreditoAvanceTarjeta(Avance pAvance, String numero_radicacion, Int64 codcliente, Int64 codoperacion, DateTime fechaavance, Int64 valoravance, Int64 plazo, ref string pError, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pAvance = BAAvance.CrearCreditoAvanceTarjeta(pAvance, numero_radicacion, codcliente, codoperacion, fechaavance, valoravance, plazo, ref pError, pUsuario);

                    ts.Complete();
                }

                return pAvance;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AvanceBusiness", "CrearCreditoAvanceTarjeta", ex);
                return null;
            }
        }

        public Avance CrearPagoAvanceTarjeta(Avance pAvance, String numero_radicacion, Int64 codcliente, Int64 codoperacion, DateTime fechapago, Int64 valorapagar, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pAvance = BAAvance.CrearPagoAvanceTarjeta(pAvance, numero_radicacion, codcliente, codoperacion, fechapago, valorapagar, pUsuario);

                    ts.Complete();
                }

                return pAvance;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AvanceBusiness", "CrearPagoAvanceTarjeta", ex);
                return null;
            }
        }


        public Avance NegarAvances(Avance pAvance, Motivo motivo, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pAvance = BAAvance.NegarAvances(pAvance, motivo, pUsuario);
                    ts.Complete();
                }
                return pAvance;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AvanceBusiness", "NegarAvances", ex);
                return null;
            }
        }

        public string AlertaTarjeta(Int64 cod_persona, Usuario pUsuario)
        {
            try
            {
                return BAAvance.AlertaTarjeta(cod_persona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AvanceBusiness", "AlertaTarjeta", ex);
                return null;
            }        
        }

    }
}