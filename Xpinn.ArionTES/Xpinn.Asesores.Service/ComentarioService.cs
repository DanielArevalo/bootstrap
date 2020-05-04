using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Asesores.Business;
using Xpinn.Asesores.Entities;
using Xpinn.Util;

namespace Xpinn.Asesores.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ComentarioService
    {
        private ComentarioBusiness busComentario;
        private ExcepcionBusiness excepBusinnes;

        public ComentarioService()
        {
            busComentario = new ComentarioBusiness();
            excepBusinnes = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "110113"; } }

        public Comentario CrearComentario(Comentario pComentario, Usuario pUsuario)
        {
            try
            {
                return busComentario.CrearComentario(pComentario, pUsuario);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ComentarioService", "CrearComentario", ex);
                return null;
            }
        }

        public List<Comentario> ListarComentario(Producto pProducto, Usuario pUsuario)
        {
            try
            {
                return busComentario.ListarComentario(pProducto, pUsuario);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ComentarioService", "ListarComentario", ex);
                return null;
            }
        }

        public List<Comentario> ListarComentarios(Producto pProducto, Usuario pUsuario)
        {
            try
            {
                return busComentario.ListarComentarios(pProducto, pUsuario);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ComentarioService", "ListarComentario", ex);
                return null;
            }
        }

        public void EliminarComentario(Int64 pId, Usuario pusuario)
        {
            try
            {
                busComentario.EliminarComentario(pId, pusuario);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ComentarioService", "EliminarComentario", ex);
            }
        }

        public Comentario ConsultarComentarios(Int64 pcodigo, Usuario pUsuario)
        {
            try
            {
                return busComentario.ConsultarComentario(pcodigo, pUsuario);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ComentarioService", "ConsultarComentarios", ex);
                return null;
            }
        }

        public void ModificarComentario(Comentario comentario, Usuario usuario)
        {
            try
            {
                busComentario.ModificarComentario(comentario, usuario);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ComentarioService", "ModificarComentario", ex);
            }
        }
    }
}
