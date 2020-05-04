using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Business;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Services
{

    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class AvanceService
    {
        private AvanceBusiness BOAvance;
        private ExcepcionBusiness BOExcepcion;


        public AvanceService()
        {
            BOAvance = new AvanceBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "100504"; } }
        public string CodigoProgramaAprobacion { get { return "100505"; } }
        public string CodigoProgramaDesem { get { return "100507"; } }
        public string CodigoProgramaModCupo { get { return "100510"; } }

        public string CodigoProgramaAnulAvances { get { return "100511"; } }

        public Avance CrearCreditoAvance(Avance pAvance, Usuario vUsuario)
        {
            try
            {
                return BOAvance.CrearCreditoAvance(pAvance, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AvanceService", "CrearCreditoAvance", ex);
                return null;
            }
        }



        public void EliminarCreditoAvance(Int64 pId, Usuario vUsuario)
        {
            try
            {
                BOAvance.EliminarCreditoAvance(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AvanceService", "vUsuario", ex);
            }
        }



        public List<Avance> ListarCreditoRotativos(Avance pRotativo, DateTime pFecha, Usuario pUsuario, String filtro)
        {
            try
            {
                return BOAvance.ListarCreditoRotativos(pRotativo, pFecha, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AvanceService", "ListarCreditoRotativos", ex);
                return null;
            }
        }


        public Avance ConsultarCreditoRotativo(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOAvance.ConsultarCreditoRotativo(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AvanceService", "ConsultarCreditoRotativo", ex);
                return null;
            }
        }

        public Avance ConsultarTasaCreditoTotativo(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOAvance.ConsultarTasaCreditoTotativo(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AvanceService", "ConsultarTasaCreditoTotativo", ex);
                return null;
            }
        }

        public Avance ConsultarPlazoCreditoTotativo(String pNombre, Usuario pUsuario)
        {
            try
            {
                return BOAvance.ConsultarPlazoCreditoTotativo(pNombre, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AvanceService", "ConsultarPlazoCreditoTotativo", ex);
                return null;
            }
        }

        public Avance ConsultarPlazoMaximoCredito(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOAvance.ConsultarPlazoMaximoCredito(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AvanceService", "ConsultarPlazoMaximoCredito", ex);
                return null;
            }
        }


        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            try
            {
                return BOAvance.ObtenerSiguienteCodigo(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AvanceService", "ObtenerSiguienteCodigo", ex);
                return 1;
            }
        }


        //LISTAR CREDITOS POR APROBAR

        public Avance ModificarCreditoAvance(Avance pAvance, Usuario vUsuario)
        {
            try
            {
                return BOAvance.ModificarCreditoAvance(pAvance, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AvanceService", "ModificarCreditoAvance", ex);
                return null;
            }
        }


        public Avance GrabarAprobacionDavance(Avance pAvance, Usuario vUsuario)
        {
            try
            {
                return BOAvance.GrabarAprobacionDavance(pAvance, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AvanceService", "GrabarAprobacionDavance", ex);
                return null;
            }
        }


        public List<Avance> ListarCreditoXaprobar(Avance pRotativo, DateTime pFecha, Usuario pUsuario, String filtro)
        {
            try
            {
                return BOAvance.ListarCreditoXaprobar(pRotativo, pFecha, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AvanceService", "ListarCreditoXaprobar", ex);
                return null;
            }
        }

        public List<Avance> ListarCreditoXaprobar(Avance pRotativo, Usuario pUsuario, String filtro)
        {
            try
            {
                return BOAvance.ListarCreditoXaprobar(pRotativo, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AvanceService", "ListarCreditoXaprobar", ex);
                return null;
            }
        }


        public Avance ConsultarCredRotativoXaprobar(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOAvance.ConsultarCredRotativoXaprobar(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AvanceService", "ConsultarCredRotativoXaprobar", ex);
                return null;
            }
        }

        public Avance ConsultarCredRotativoXaprobarXCredito(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOAvance.ConsultarCredRotativoXaprobarXCredito(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AvanceService", "ConsultarCredRotativoXaprobarXCredito", ex);
                return null;
            }
        }

        public Avance ValidarNumeroRadicacion(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOAvance.ValidarNumeroRadicacion(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AvanceService", "ValidarNumeroRadicacion", ex);
                return null;
            }
        }


        public ControlCreditos CrearControlCreditos(ControlCreditos pControl, Usuario vUsuario)
        {
            try
            {
                return BOAvance.CrearControlCreditos(pControl, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AvanceService", "CrearControlCreditos", ex);
                return null;
            }
        }


        public ControlCreditos ModificarControlCreditos(ControlCreditos pControl, Usuario vUsuario)
        {
            try
            {
                return BOAvance.ModificarControlCreditos(pControl, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AvanceService", "ModificarControlCreditos", ex);
                return null;
            }
        }




        public Giro CrearGiro(Giro pGiro, Usuario vUsuario, int opcion)
        {
            try
            {
                return BOAvance.CrearGiro(pGiro, vUsuario, opcion);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AvanceService", "CrearGiro", ex);
                return null;
            }
        }


        public DateTime ObtenerUltimaFecha(Usuario pUsuario)
        {
            try
            {
                return BOAvance.ObtenerUltimaFecha(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AvanceService", "ObtenerUltimaFecha", ex);
                return DateTime.MinValue;
            }
        }

        public Giro ConsultarFormaDesembolso(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOAvance.ConsultarFormaDesembolso(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AvanceService", "ConsultarFormaDesembolso", ex);
                return null;
            }
        }


        public ControlCreditos ConsultarCodigoPersonaXBanco(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return BOAvance.ConsultarCodigoPersonaXBanco(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AvanceService", "ConsultarCodigoPersonaXBanco", ex);
                return null;
            }
        }

        public Avance ModificarDesembolsoAvance(Avance pAvance, Usuario vUsuario)
        {
            try
            {
                return BOAvance.ModificarDesembolsoAvance(pAvance, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AvanceService", "ModificarDesembolsoAvance", ex);
                return null;
            }
        }
        public Avance ModificarCuota(Avance pAvance, Usuario vUsuario)
        {
            try
            {
                return BOAvance.ModificarCuota(pAvance, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AvanceService", "ModificarCuota", ex);
                return null;
            }
        }


        public TRAN_CRED CrearTransaCred(TRAN_CRED pTransac, Usuario vUsuario)
        {
            try
            {
                return BOAvance.CrearTransaCred(pTransac, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AvanceService", "CrearTransaCred", ex);
                return null;
            }
        }


        public Giro ModificarGiro(Giro pAvance, Usuario vUsuario)
        {
            try
            {
                return BOAvance.ModificarGiro(pAvance, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AvanceService", "ModificarGiro", ex);
                return null;
            }
        }
        public Avance fecha_ult_avance(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOAvance.fecha_ult_avance(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AvanceService", "fecha_ult_avance", ex);
                return null;
            }
        }
        public Avance ConsultarCuotaCredito(Avance pAvance, Usuario pUsuario)
        {
            try
            {
                return BOAvance.ConsultarCuotaCredito(pAvance, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AvanceService", "ConsultarCuotaCredito", ex);
                return null;
            }
        }


        public Avance NegarAvances(Avance pAvance, Motivo motivo, Usuario pUsuario)
        {
            try
            {
                return BOAvance.NegarAvances(pAvance, motivo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AvanceService", "NegarAvances", ex);
                return null;
            }
        }

        public string AlertaTarjeta(Int64 cod_persona, Usuario pUsuario)
        {
            try
            {
                return BOAvance.AlertaTarjeta(cod_persona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AvanceService", "AlertaTarjeta", ex);
                return null;
            }
        }


    }
}