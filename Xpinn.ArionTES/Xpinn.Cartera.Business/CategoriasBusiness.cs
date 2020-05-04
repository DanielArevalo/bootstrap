using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Cartera.Data;
using Xpinn.Cartera.Entities;
 
namespace Xpinn.Cartera.Business
{
 
    public class CategoriasBusiness : GlobalBusiness
    {
 
        private CategoriasData DACategorias;
 
        public CategoriasBusiness()
        {
            DACategorias = new CategoriasData();
        }
 
        public Categorias CrearCategorias(Categorias pCategorias, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pCategorias = DACategorias.CrearCategorias(pCategorias, pusuario);
 
                    ts.Complete();
 
                }
 
                return pCategorias;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CategoriasBusiness", "CrearCategorias", ex);
                return null;
            }
        }
 
 
        public Categorias ModificarCategorias(Categorias pCategorias, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pCategorias = DACategorias.ModificarCategorias(pCategorias, pusuario);
 
                    ts.Complete();
 
                }
 
                return pCategorias;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CategoriasBusiness", "ModificarCategorias", ex);
                return null;
            }
        }


        public void EliminarCategorias(string pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DACategorias.EliminarCategorias(pId, pusuario);
 
                    ts.Complete();
 
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CategoriasBusiness", "EliminarCategorias", ex);
            }
        }


        public Categorias ConsultarCategorias(string pId, Usuario pusuario)
        {
            try
            {
                Categorias Categorias = new Categorias();
                Categorias = DACategorias.ConsultarCategorias(pId, pusuario);
                return Categorias;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CategoriasBusiness", "ConsultarCategorias", ex);
                return null;
            }
        }
 
 
        public List<Categorias> ListarCategorias(Categorias pCategorias, Usuario pusuario)
        {
            try
            {
                return DACategorias.ListarCategorias(pCategorias, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CategoriasBusiness", "ListarCategorias", ex);
                return null;
            }
        }
 
 
    }
}

