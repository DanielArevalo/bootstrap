using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Nomina.Data;
using Xpinn.Nomina.Entities;

namespace Xpinn.Nomina.Business
{

    public class CajaDeCompensacionBussines : GlobalBusiness
    {

        private CajaDeCompensacionaData DADIRCAJADECOMPENSACION;

        public CajaDeCompensacionBussines()
        {
            DADIRCAJADECOMPENSACION = new CajaDeCompensacionaData();
        }

        public CajaDeCompensacion CrearDIRCAJADECOMPENSACION(CajaDeCompensacion pDIRCAJADECOMPENSACION, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pDIRCAJADECOMPENSACION = DADIRCAJADECOMPENSACION.CrearCajaDeCompensacion(pDIRCAJADECOMPENSACION, pusuario);

                    ts.Complete();

                }

                return pDIRCAJADECOMPENSACION;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajaDeCompensacionBussines", "CrearDIRCAJADECOMPENSACION", ex);
                return null;
            }
        }


        public CajaDeCompensacion ModificarDIRCAJADECOMPENSACION(CajaDeCompensacion pDIRCAJADECOMPENSACION, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pDIRCAJADECOMPENSACION = DADIRCAJADECOMPENSACION.ModificarCajaDeCompensacion(pDIRCAJADECOMPENSACION, pusuario);

                    ts.Complete();

                }

                return pDIRCAJADECOMPENSACION;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajaDeCompensacionBussines", "ModificarDIRCAJADECOMPENSACION", ex);
                return null;
            }
        }

        public void EliminarDIRCAJADECOMPENSACION(Int64 pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DADIRCAJADECOMPENSACION.EliminarCajaDeCompensacion(pId, pusuario);

                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajaDeCompensacionBussines", "EliminarDIRCAJADECOMPENSACION", ex);
            }
        }

        public CajaDeCompensacion ConsultarDIRCAJADECOMPENSACION(Int64 pId, Usuario pusuario)
        {
            try
            {
                CajaDeCompensacion DIRCAJADECOMPENSACION = new CajaDeCompensacion();
                DIRCAJADECOMPENSACION = DADIRCAJADECOMPENSACION.ConsultarCajaDeCompensacion(pId, pusuario);
                return DIRCAJADECOMPENSACION;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajaDeCompensacionBussines", "ConsultarDIRCAJADECOMPENSACION", ex);
                return null;
            }
        }
        
        public CajaDeCompensacion ConsultarDatosDIRCAJADECOMPENSACION(string pId, Usuario pusuario)
        {
            try
            {
                CajaDeCompensacion DIRCAJADECOMPENSACION = new CajaDeCompensacion();
                DIRCAJADECOMPENSACION = DADIRCAJADECOMPENSACION.ConsultarDatosCajaDeCompensacion(pId, pusuario);
                return DIRCAJADECOMPENSACION;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajaDeCompensacionBussines", "ConsultarDatosDIRCAJADECOMPENSACION", ex);
                return null;
            }
        }

        public List<CajaDeCompensacion> ListarDIRCAJADECOMPENSACION(string filtro, Usuario pusuario)
        {
            try
            {
                return DADIRCAJADECOMPENSACION.ListarCajaDeCompensacion(filtro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajaDeCompensacionBussines", "ListarDIRCAJADECOMPENSACION", ex);
                return null;
            }
        }


    }
}

