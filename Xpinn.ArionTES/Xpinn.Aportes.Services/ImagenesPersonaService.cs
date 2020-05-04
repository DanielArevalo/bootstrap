using System;
using System.Collections.Generic;
using System.Text;
using Xpinn.Util;
using System.ServiceModel;
using Xpinn.Aportes.Entities;
using Xpinn.Aportes.Business;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.Aportes.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ImagenesService
    {

        private ImagenesPersonaBusiness BOImagenes;
        private ExcepcionBusiness BOExcepcion;

        public ImagenesService()
        {
            BOImagenes = new ImagenesPersonaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "170119"; } }


        public Imagenes CrearImagenesPersona(Imagenes pImagenes, Usuario pusuario)
        {
            try
            {
                pImagenes = BOImagenes.CrearImagenesPersona(pImagenes, pusuario);
                return pImagenes;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ImagenesService", "CrearImagenesPersona", ex);
                return null;
            }
        }


        public Imagenes ModificarImagenesPersona(Imagenes pImagenes, Usuario pusuario)
        {
            try
            {
                pImagenes = BOImagenes.ModificarImagenesPersona(pImagenes, pusuario);
                return pImagenes;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ImagenesService", "ModificarImagenesPersona", ex);
                return null;
            }
        }


        public Imagenes ConsultarImageneDocumentosPersona(Imagenes pImagen, Usuario pUsuario)
        {
            try
            {
                return BOImagenes.ConsultarImageneDocumentosPersona(pImagen, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ImagenesService", "ConsultarImageneDocumentosPersona", ex);
                return null;
            }
        }
        public byte[] DocumentosPersona(Int64 NumCuenta, ref Int64 IdImagen, Usuario vUsuario)
        {
            try
            {
                return BOImagenes.DocumentosPersona(NumCuenta, ref IdImagen, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ImagenesService", "DocumentosPersona", ex);
                return null;
            }
        }
        public List<Imagenes> ListaDocumentos(Imagenes pImagenes, Usuario pUsuario)
        {
            try
            {
                return BOImagenes.ListaDocumentos(pImagenes, pUsuario);
            }
            catch
            {
                return null;
            }
        }
        public Imagenes DocumentosAnexos(Int64 NumCuenta, ref Int64 IdImagen, Usuario vUsuario)
        {
            try
            {
                return BOImagenes.DocumentosAnexos(NumCuenta, ref IdImagen, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ImagenesService", "DocumentosAnexos", ex);
                return null;
            }
        }
    }
}