using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Tesoreria.Business;
using Xpinn.Tesoreria.Entities;

namespace Xpinn.Tesoreria.Services
{

    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ChequeraService
    {
        private ChequeraBusiness BOChequera;
        private ExcepcionBusiness BOExcepcion;

       
        public ChequeraService()
        {
            BOChequera = new ChequeraBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "40201"; } }


        public Chequera CrearChequera(Chequera pChequera, Usuario vUsuario)
        {
            try
            {
                return BOChequera.CrearChequera(pChequera, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ChequeraService", "CrearChequera", ex);
                return null;
            }
        }


        public Chequera ModificarChequera(Chequera pChequera, Usuario vUsuario)
        {
            try
            {
                return BOChequera.ModificarChequera(pChequera, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ChequeraService", "ModificarChequera", ex);
                return null;
            }
        }



        public void EliminarChequera(Int32 pId, Usuario vUsuario)
        {
            try
            {
                BOChequera.EliminarChequera(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ChequeraService", "EliminarChequera", ex);
            }
        }


        public List<Chequera> ListarChequera(Chequera pChequera, Usuario vUsuario,String filtro)
        {

            try
            {
                return BOChequera.ListarChequera(pChequera, vUsuario,filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ChequeraService", "ListarChequera", ex);
                return null;
            }
        }


        public Chequera ConsultarChequera(Chequera pChequera, Usuario vUsuario)
        {
            try
            {
                return BOChequera.ConsultarChequera(pChequera, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ChequeraService", "ConsultarChequera", ex);
                return null;
            }
        }

        public Chequera ConsultarBanco(Chequera pChequera, Usuario vUsuario)
        {
            try
            {
                return BOChequera.ConsultarBanco(pChequera, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ChequeraService", "ConsultarBanco", ex);
                return null;
            }
        }


        public List<Chequera> ListarCuentasBancarias(Chequera pChequera, Usuario vUsuario)
        {
            try
            {
                return BOChequera.ListarCuentasBancarias(pChequera, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ChequeraService", "ListarCuentasBancarias", ex);
                return null;
            }
        }

        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            try
            {
                return BOChequera.ObtenerSiguienteCodigo(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ChequeraService", "ObtenerSiguienteCodigo", ex);
                return 0;
            }
        }
        public List<Chequera> ConsultarChequeraReporte(Chequera pChequera, Usuario vUsuario)
        {
            try
            {
                return BOChequera.ConsultarChequeraReporte(pChequera, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ChequeraService", "ConsultarChequeraReporte", ex);
                return null;
            }
        }

    }
}