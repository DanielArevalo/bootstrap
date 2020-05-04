using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Aportes.Data;
using Xpinn.Aportes.Entities;
using Xpinn.FabricaCreditos;
using Xpinn.Imagenes.Data;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.Aportes.Business
{
    public class ImagenesPersonaBusiness : GlobalBusiness
    {

        private ImagenesORAData DAImagenes;

        public ImagenesPersonaBusiness()
        {
            DAImagenes = new ImagenesORAData();
        }


        public FabricaCreditos.Entities.Imagenes CrearImagenesPersona(FabricaCreditos.Entities.Imagenes pImagenes, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pImagenes = DAImagenes.CrearImagenesPersona(pImagenes, pusuario);

                    ts.Complete();
                }

                return pImagenes;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ImagenesBusiness", "CrearImagenesPersona", ex);
                return null;
            }
        }


        public FabricaCreditos.Entities.Imagenes ConsultarImageneDocumentosPersona(FabricaCreditos.Entities.Imagenes pImagen, Usuario pUsuario)
        {
            try
            {
                return DAImagenes.ConsultarImageneDocumentosPersona(pImagen, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ImagenesBusiness", "ConsultarImageneDocumentosPersona", ex);
                return null;
            }
        }

        public FabricaCreditos.Entities.Imagenes ModificarImagenesPersona(FabricaCreditos.Entities.Imagenes pImagenes, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pImagenes = DAImagenes.ModificarImagenesPersona(pImagenes, pusuario);
                    pImagenes = ConsultarImageneDocumentosPersona(pImagenes, pusuario);

                    ts.Complete();
                }

                return pImagenes;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ImagenesBusiness", "ModificarImagenesPersona", ex);
                return null;
            }
        }
        public byte[] DocumentosPersona(Int64 NumCuenta, ref Int64 IdImagen, Usuario vUsuario)
        {
            try
            {
                return DAImagenes.ConsultarDocPersona(NumCuenta, ref IdImagen, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ImagenesBusiness", "DocumentosPersona", ex);
                return null;
            }
        }
        public List<Xpinn.FabricaCreditos.Entities.Imagenes> ListaDocumentos(Xpinn.FabricaCreditos.Entities.Imagenes pImagenes, Usuario pUsuario)
        {
            try
            {
                return DAImagenes.ListaDocumentos(pImagenes, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ImagenesBusiness", "ListaDocumentos", ex);
                return null;
            }
        }
        public Xpinn.FabricaCreditos.Entities.Imagenes DocumentosAnexos(Int64 NumCuenta, ref Int64 IdImagen, Usuario vUsuario)
        {
            try
            {
                return DAImagenes.ConsultarDocumentosAnexos(NumCuenta, ref IdImagen, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ImagenesBusiness", "DocumentosAnexos", ex);
                return null;
            }
        }
    }
}