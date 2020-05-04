using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util; 
using Xpinn.Asesores.Data;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Business
{
    /// <summary>
    /// Objeto de negocio para AseComentario
    /// </summary>
    public class ComentarioBusiness : GlobalBusiness
    {
        private ComentarioData DAAseComentario;

        /// <summary>
        /// Constructor del objeto de negocio para AseComentario
        /// </summary>
        public ComentarioBusiness()
        {
            DAAseComentario = new ComentarioData();
        }

        /// <summary>
        /// Crea un AseComentario
        /// </summary>
        /// <param name="pAseComentario">Entidad AseComentario</param>
        /// <returns>Entidad AseComentario creada</returns>
        public Comentario CrearComentario(Comentario pComentario, Usuario pUsuario)
        {
            try
            {
               // using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pComentario = DAAseComentario.Crear(pComentario, pUsuario);

                    //ts.Complete();
                }
                return pComentario;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AseComentarioBusiness", "CrearAseComentario", ex);
                return null;
            }
        }


        public void EliminarComentario(Int64 pId, Usuario vusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAAseComentario.EliminarComentario(pId, vusuario);

                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComentarioBusiness", "EliminarComentario", ex);
            }
        }
 
        

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pAseComentario">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de AseComentario obtenidos</returns>
        public List<Comentario> ListarComentario(Producto pProducto, Usuario pUsuario)
        {
            try
            {
                return DAAseComentario.Listar(pProducto, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComentarioBusiness", "ListarComentario", ex);
                return null;
            }
        }

        public List<Comentario> ListarComentarios(Producto pProducto, Usuario pUsuario)
        {
            try
            {
                return DAAseComentario.Listarcomentario(pProducto, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComentarioBusiness", "ListarComentario", ex);
                return null;
            }
        }

        public void ModificarComentario(Comentario comentario, Usuario usuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAAseComentario.ModificarComentario(comentario, usuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComentarioBusiness", "ModificarComentario", ex);
            }
        }

        public Comentario ConsultarComentario(Int64 pcodigo, Usuario pUsuario)
        {
            try
            {
                return DAAseComentario.ConsultarComentario(pcodigo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComentarioBusiness", "ConsultarComentario", ex);
                return null;
            }

        }
      


    }
}