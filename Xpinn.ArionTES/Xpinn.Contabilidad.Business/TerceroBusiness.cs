using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Contabilidad.Data;
using Xpinn.Contabilidad.Entities;
using Xpinn.FabricaCreditos.Data;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.Contabilidad.Business
{
    /// <summary>
    /// Objeto de negocio para Tercero
    /// </summary>
    public class TerceroBusiness : GlobalBusiness
    {
        private TerceroData DATercero;
        private Xpinn.Imagenes.Data.ImagenesORAData DAImagenes;
        /// <summary>
        /// Constructor del objeto de negocio para Tercero
        /// </summary>
        public TerceroBusiness()
        {
            DATercero = new TerceroData();
            DAImagenes = new Imagenes.Data.ImagenesORAData();
        }

        /// <summary>
        /// Crea un Tercero
        /// </summary>
        /// <param name="pTercero">Entidad Tercero</param>
        /// <returns>Entidad Tercero creada</returns>
        public Tercero CrearTercero(Tercero pTercero, Usuario vusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTercero = DATercero.CrearTercero(pTercero, vusuario);
                    Int64 cod = pTercero.cod_persona;

                    if (pTercero.lstInformacion != null)
                    {
                        InformacionAdicionalData DAActi = new InformacionAdicionalData();
                        foreach (InformacionAdicional eInf in pTercero.lstInformacion)
                        {
                            InformacionAdicional nInforma = new InformacionAdicional();

                            eInf.cod_persona = cod;
                            nInforma = DAActi.CrearPersona_InfoAdicional(eInf, vusuario);
                        }
                    }

                    // ACTIVIDAD CIIU
                    if (pTercero.lstActividadCIIU != null)
                    {
                        ActividadesData DAActi = new ActividadesData();
                        foreach (Actividades objActividadEconomica in pTercero.lstActividadCIIU)
                        {
                            DAActi.CrearActividadEconomicaSecundaria(cod, objActividadEconomica.codactividad, vusuario);
                        }
                    }

                    if (pTercero.foto != null)
                    {
                        Xpinn.FabricaCreditos.Entities.Imagenes pImagenes = new Xpinn.FabricaCreditos.Entities.Imagenes();
                        pImagenes.idimagen = 0;
                        pImagenes.cod_persona = pTercero.cod_persona;
                        pImagenes.tipo_imagen = 2;
                        pImagenes.imagen = pTercero.foto;
                        pImagenes.fecha = System.DateTime.Now;
                        DAImagenes.CrearImagenesPersona(pImagenes, vusuario);
                    }

                    if (pTercero.lstMonedaExt != null)
                    {
                        EstadosFinancierosData DAEstadosFinancieros = new EstadosFinancierosData();
                        foreach (EstadosFinancieros eMoneda in pTercero.lstMonedaExt)
                        {
                            EstadosFinancieros pMoneda = new EstadosFinancieros();
                            eMoneda.cod_persona = cod;
                            pMoneda = DAEstadosFinancieros.CrearMonedaExtranjera(eMoneda, vusuario);
                        }
                    }

                    if (pTercero.lsProductosExt != null)
                    {
                        EstadosFinancierosData DAEstadosFinancieros = new EstadosFinancierosData();
                        foreach (EstadosFinancieros eMoneda in pTercero.lsProductosExt)
                        {
                            EstadosFinancieros pMoneda = new EstadosFinancieros();
                            eMoneda.cod_persona = cod;
                            pMoneda = DAEstadosFinancieros.CrearMonedaExtranjera(eMoneda, vusuario);
                        }
                    }

                    if(pTercero.lstAsociados != null)
                    {
                        foreach(Tercero vAsociado in pTercero.lstAsociados)
                        {
                            Tercero pAsociado = new Tercero();
                            vAsociado.cod_persona = cod;
                            pAsociado = DATercero.CrearAsociado(vAsociado, vusuario);
                        }
                    }

                    ts.Complete();
                }

                return pTercero;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TerceroBusiness", "CrearTercero", ex);
                return null;
            }
        }

        public Tuple<bool, string> CambiarTipoDePersona(long id, string tipoPersona, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    Tuple<bool, string>  tuple = DATercero.CambiarTipoDePersona(id, tipoPersona, pUsuario);

                    ts.Complete();

                    return tuple;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TerceroBusiness", "CambiarTipoDePersona", ex);
                return Tuple.Create(false, ex.Message);
            }
        }

        /// <summary>
        /// Modifica un Tercero
        /// </summary>
        /// <param name="pTercero">Entidad Tercero</param>
        /// <returns>Entidad Tercero modificada</returns>
        public Tercero ModificarTercero(Tercero pTercero, Usuario vusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTercero = DATercero.ModificarTercero(pTercero, vusuario);
                    Int64 cod = pTercero.cod_persona;

                    if (pTercero.lstInformacion != null)
                    {
                        InformacionAdicionalData DAInfor = new InformacionAdicionalData();
                        foreach (InformacionAdicional eInf in pTercero.lstInformacion)
                        {
                            InformacionAdicional nInforma = new InformacionAdicional();
                            eInf.cod_persona = cod;
                            if (eInf.idinfadicional != null && eInf.idinfadicional != 0)
                                nInforma = DAInfor.ModificarPersona_InfoAdicional(eInf, vusuario);
                            else
                                nInforma = DAInfor.CrearPersona_InfoAdicional(eInf, vusuario);
                        }
                    }

                    if (pTercero.lstEmpresaRecaudo != null && pTercero.lstEmpresaRecaudo.Count > 0)
                    {
                        PersonaEmpresaRecaudoData DAEmpresa = new PersonaEmpresaRecaudoData();
                        foreach (PersonaEmpresaRecaudo eEmpresa in pTercero.lstEmpresaRecaudo)
                        {
                            PersonaEmpresaRecaudo nEmpresa = new PersonaEmpresaRecaudo();
                            eEmpresa.cod_persona = cod;
                            if (eEmpresa.seleccionar)
                            {
                                if (eEmpresa.idempresarecaudo != null)
                                    nEmpresa = DAEmpresa.ModificarPersonaEmpresaRecaudo(eEmpresa, vusuario);
                                else
                                    nEmpresa = DAEmpresa.CrearPersonaEmpresaRecaudo(eEmpresa, vusuario);
                            }
                            else
                                if (eEmpresa.idempresarecaudo != null)
                                    DAEmpresa.EliminarPersonaEmpresaRecaudo(Convert.ToInt64(eEmpresa.idempresarecaudo), vusuario);
                        }
                    }

                    // ACTIVIDAD CIIU
                    if (pTercero.lstActividadCIIU != null)
                    {
                        ActividadesData DAActi = new ActividadesData();
                        foreach (Actividades objActividadEconomica in pTercero.lstActividadCIIU)
                        {
                            DAActi.CrearActividadEconomicaSecundaria(cod, objActividadEconomica.codactividad, vusuario);
                        }
                    }

                    if (pTercero.foto != null)
                    {
                        Xpinn.FabricaCreditos.Entities.Imagenes pImagenes = new Xpinn.FabricaCreditos.Entities.Imagenes();
                        pImagenes.idimagen = 0;
                        pImagenes.cod_persona = pTercero.cod_persona;
                        pImagenes.tipo_imagen = 2;
                        pImagenes.imagen = pTercero.foto;
                        pImagenes.fecha = System.DateTime.Now;
                        if (DAImagenes.ExisteImagenPersona(Convert.ToInt64(pTercero.cod_persona), 2, vusuario))
                            DAImagenes.ModificarImagenesPersona(pImagenes, vusuario);
                        else
                            DAImagenes.CrearImagenesPersona(pImagenes, vusuario);
                    }

                    if (pTercero.lstMonedaExt != null)
                    {
                        EstadosFinancierosData DAEstadosFinancieros = new EstadosFinancierosData();
                        foreach (EstadosFinancieros eMoneda in pTercero.lstMonedaExt)
                        {
                            EstadosFinancieros pMoneda = new EstadosFinancieros();
                            eMoneda.cod_persona = cod;
                            if (eMoneda.cod_moneda_ext != null &&  eMoneda.cod_moneda_ext != 0)
                                pMoneda = DAEstadosFinancieros.ModificarMonedaExtranjera(eMoneda, vusuario);
                            else if(eMoneda.cod_moneda_ext == null || eMoneda.cod_moneda_ext == 0)
                                pMoneda = DAEstadosFinancieros.CrearMonedaExtranjera(eMoneda, vusuario);
                        }
                    }

                    if (pTercero.lsProductosExt != null)
                    {
                        EstadosFinancierosData DAEstadosFinancieros = new EstadosFinancierosData();
                        foreach (EstadosFinancieros eMoneda in pTercero.lsProductosExt)
                        {
                            EstadosFinancieros pMoneda = new EstadosFinancieros();
                            eMoneda.cod_persona = cod;
                            if (eMoneda.cod_moneda_ext != null && eMoneda.cod_moneda_ext != 0)
                                pMoneda = DAEstadosFinancieros.ModificarMonedaExtranjera(eMoneda, vusuario);
                            else if (eMoneda.cod_moneda_ext == null || eMoneda.cod_moneda_ext == 0)
                                pMoneda = DAEstadosFinancieros.CrearMonedaExtranjera(eMoneda, vusuario);
                        }
                    }

                    if (pTercero.lstAsociados != null)
                    {
                        foreach (Tercero vAsociado in pTercero.lstAsociados)
                        {
                            Tercero pAsociado = new Tercero();
                            vAsociado.cod_persona = cod;
                            if (vAsociado.cod_representante != 0)
                                pAsociado = DATercero.ModificarAsociado(vAsociado, vusuario);
                            else if(vAsociado.cod_representante == 0)
                                pAsociado = DATercero.CrearAsociado(vAsociado, vusuario);
                        }
                    }

                    ts.Complete();
                }

                return pTercero;
            }
            catch (Exception ex)
             {
                BOExcepcion.Throw("TerceroBusiness", "ModificarTercero", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un Tercero
        /// </summary>
        /// <param name="pId">Identificador de Tercero</param>
        public void EliminarTercero(Int64 pId, Usuario vusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DATercero.EliminarTercero(pId, vusuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TerceroBusiness", "EliminarTercero", ex);
            }
        }

        /// <summary>
        /// Obtiene un Tercero
        /// </summary>
        /// <param name="pId">Identificador de Tercero</param>
        /// <returns>Entidad Tercero</returns>
        public Tercero ConsultarTercero(Int64? pCod, string pId, Usuario vusuario)
        {
            try
            {
                Tercero Tercero = new Tercero();

                Tercero = DATercero.ConsultarTercero(pCod, pId, vusuario);
                Tercero.regimen = DATercero.ConsultarRegimen(Convert.ToInt64(pCod), vusuario);
                if (Tercero != null)
                {
                    Int64 idImagen = 0;
                    Tercero.foto = DAImagenes.ConsultarImagenPersona(Convert.ToInt64(Tercero.cod_persona), 1, ref idImagen, vusuario);
                    Tercero.idimagen = idImagen;
                }
                return Tercero;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TerceroBusiness", "ConsultarTercero", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pTercero">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Tercero obtenidos</returns>
        public List<Tercero> ListarTercero(Tercero pTercero, Usuario vUsuario)
        {
            try
            {
                return DATercero.ListarTercero(pTercero, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TerceroBusiness", "ListarTercero", ex);
                return null;
            }
        }

        public List<Tercero> ListarTerceroNoAfiliados(Tercero pTercero, string pFiltro, Usuario vUsuario, string pOrden = null)
        {
            try
            {
                return DATercero.ListarTerceroNoAfiliados(pTercero, pFiltro, vUsuario, pOrden);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TerceroBusiness", "ListarTerceroNoAfiliados", ex);
                return null;
            }
        }

        public List<Tercero> ListarTerceroSoloAfiliados(Tercero pTercero,string pFiltro, Usuario vUsuario, string pOrden = null)
        {
            try
            {
                return DATercero.ListarTerceroSoloAfiliados(pTercero,pFiltro, vUsuario, pOrden);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TerceroBusiness", "ListarTerceroSoloAfiliados", ex);
                return null;
            }
        }


        public List<Tercero> ListarTercero(Tercero pTercero, string pFiltro, string pOrden, Usuario vUsuario)
        {
            try
            {
                return DATercero.ListarTercero(pTercero, pFiltro, pOrden, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TerceroBusiness", "ListarTercero", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene un Tercero
        /// </summary>
        /// <param name="pId">Identificador de Tercero</param>
        /// <returns>Entidad Tercero</returns>
        public Tercero ValidarTercero(Int64 pTercero, Usuario pUsuario)
        {
            try
            {
                Tercero Tercero = new Tercero();

                try
                {
                    Tercero = DATercero.ValidarTercero(pTercero, pUsuario);
                }
                catch (ExceptionBusiness ex)
                {
                    if (ex.Message.Contains("El registro no existe. Verifique por favor."))
                        throw new ExceptionBusiness("Tipo de Comprobante no encontrado.");
                    else
                        throw new ExceptionBusiness(ex.Message);
                }

                return Tercero;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TerceroBusiness", "ValidarTercero", ex);
                return null;
            }
        }

        public Int64 ObtenerSiguienteCodigo(Usuario vusuario)
        {
            try
            {
                return DATercero.ObtenerSiguienteCodigo(vusuario);                
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TerceroBusiness", "ObtenerSiguienteCodigo", ex);
                return Int64.MinValue;
            }
        }

        /// <summary>
        /// Listar asociados de una persona juridica
        /// </summary>
        /// <param name="cod_persona">Código de la persona juridica</param>
        /// <param name="pUsuario">Variable de Usuario</param>
        /// <returns></returns>
        public List<Tercero> ListarAsociados(Int64 cod_persona, Usuario pUsuario)
        {
            try
            {
                return DATercero.ListarAsociados(cod_persona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TerceroBusiness", "ListarAsociados", ex);
                return null;
            }
        }

    }
}