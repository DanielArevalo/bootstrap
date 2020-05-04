using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.NIIF.Entities;
using Xpinn.NIIF.Data;
using System.Transactions;
using System.Data;
using Xpinn.Util;

namespace Xpinn.NIIF.Business
{
    public class TasaMercadoNIFBusiness
    {
        protected ExcepcionBusiness BOExcepcion = new ExcepcionBusiness();
        /// Objeto de negocio para Credito
        /// 
        private TasaMercadoNIFData DATasaMercado;

                /// <summary>
        /// Constructor del objeto de negocio para Atributo
        /// </summary>
        public TasaMercadoNIFBusiness()
        {
            DATasaMercado = new TasaMercadoNIFData();
        }

        public void EliminarTasaCondicionNIIF(TasaMercadoCondicionNIF pTasaCondicionNIIF, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DATasaMercado.EliminarTasaCondicionNIIF(pTasaCondicionNIIF, vUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TasaMercadoNIIFBusiness", "EliminarTasaCondicionNIIF", ex);
            }   
        }


        public List<TasaMercadoCondicionNIF> FiltrarDatosTasaCondicion(int codigo, Usuario vUsuario)
        {
            try
            {
                return DATasaMercado.FiltrarDatosTasaCondicion(codigo, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TasaMercadoNIIFBusiness", "FiltrarDatosTasaCondicion", ex);
                return null;
            }
        }


        public int ObtenerCodigo(TasaMercadoNIF pTasaMercado,Usuario pUsuario)
        {
            try
            {
                return DATasaMercado.ObtenerCodigo(pTasaMercado, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TasaMercadoNIIFBusiness", "ObtenerCodigo", ex);
                return 0;
            }   
              
        }


        public void EliminarTasaMercadoNIIF(TasaMercadoNIF pCarteraNIIF, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DATasaMercado.EliminarTasaMercadoNIIF(pCarteraNIIF,vUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TasaMercadoNIIFBusiness", "EliminarTasaMercadoNIIF", ex);
            }   
        }



        public TasaMercadoNIF ModificarTasaMercadoNIIF(TasaMercadoNIF pTasaMercado, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTasaMercado= DATasaMercado.ModificarTasaMercadoNIIF(pTasaMercado, vUsuario);
                    
                    int Cod;
                    Cod = pTasaMercado.cod_tasa_mercado;

                    if (pTasaMercado.lstTasaCondi != null)
                    {
                        int num = 0;
                        foreach (TasaMercadoCondicionNIF eProgra in pTasaMercado.lstTasaCondi)
                        {
                            eProgra.cod_tasa_mercado = Cod;
                            TasaMercadoCondicionNIF nProgramacion = new TasaMercadoCondicionNIF();
                            if (eProgra.cod_tasa_condicion <= 0 || eProgra.cod_tasa_condicion == null)
                                nProgramacion = DATasaMercado.CrearTasaMercado_CondicionNIIF(eProgra, vUsuario);
                            else
                                nProgramacion = DATasaMercado.ModificarTasaMercado_CondicionNIIF(eProgra, vUsuario);
                            num += 1;
                        }
                    }
                    
                    ts.Complete();
                }
                return pTasaMercado;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TasaMercadoNIIFBusiness", "ModificarTasaMercadoNIIF", ex);
                return null;
            }
        }


        public TasaMercadoNIF CrearTasaMercadoNIIF(TasaMercadoNIF pTasaMercado, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTasaMercado = DATasaMercado.CrearTasaMercadoNIIF(pTasaMercado, vUsuario);
                    int cod;
                    cod = pTasaMercado.cod_tasa_mercado;

                    if (pTasaMercado.lstTasaCondi != null)
                    {
                        int num = 0;
                        foreach (TasaMercadoCondicionNIF eProg in pTasaMercado.lstTasaCondi)
                        {
                            TasaMercadoCondicionNIF nPrograma = new TasaMercadoCondicionNIF();
                            eProg.cod_tasa_mercado = cod;
                            nPrograma = DATasaMercado.CrearTasaMercado_CondicionNIIF(eProg, vUsuario);
                            num += 1;
                        }
                    }

                    ts.Complete();
                }

                return pTasaMercado;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TasaMercadoNIIFBusiness", "CrearTasaMercadoNIIF", ex);
                return null;
            }
        }


        public List<TasaMercadoNIF> ListarTasaMercadoNIIF(TasaMercadoNIF pTasaMercado, Usuario vUsuario)
        {
            try
            {
                return DATasaMercado.ListarTasaMercadoNIIF(pTasaMercado, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TasaMercadoNIIFBusiness", "ListarTasaMercadoNIIF", ex);
                return null;
            }
        }


        public List<TasaMercadoNIF> DatosCondicionNIIF(TasaMercadoNIF pTasaMercadoNIIF, Usuario vUsuario)
        {
            try
            {
               return DATasaMercado.DatosCondicionNIIF(pTasaMercadoNIIF, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TasaMercadoNIIFBusiness", "DatosCondicionNIIF", ex);
                return null;
            }
        }


    }
}
