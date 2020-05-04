using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Data;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Imagenes.Data;
using System.IO;
using Xpinn.Aportes.Data;

namespace Xpinn.FabricaCreditos.Business
{
    /// <summary>
    /// Objeto de negocio para Persona1
    /// </summary>
    public class Persona1Business : GlobalData
    {
        private Persona1Data DAPersona1;
        private ImagenesORAData DAImagenes;

        /// <summary>
        /// Constructor del objeto de negocio para Persona1
        /// </summary>
        public Persona1Business()
        {
            DAPersona1 = new Persona1Data();
            DAImagenes = new ImagenesORAData();
        }
        public Persona1 ConsultarPersona2Param(Persona1 identificacion, Usuario pUsuario)
        {
            Persona1 pPersona = new Persona1();
            pPersona = DAPersona1.ConsultarPersona2Param(identificacion, pUsuario);
            return pPersona;
        }

        public Persona1 CrearPersona1(Persona1 pPersona1, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    Int64 cod;

                    pPersona1 = DAPersona1.CrearPersona1(pPersona1, pUsuario);

                    cod = pPersona1.cod_persona;

                    if (pPersona1.lstBeneficiarios != null)
                    {
                        BeneficiarioData DABenef = new BeneficiarioData();
                        int num = 0;
                        foreach (Beneficiario eBenef in pPersona1.lstBeneficiarios)
                        {
                            Beneficiario nBeneficiario = new Beneficiario();
                            eBenef.cod_persona = cod;
                            nBeneficiario = DABenef.CrearBeneficiario(eBenef, pUsuario);
                            num += 1;
                        }
                    }

                    if (pPersona1.lstActividad != null)
                    {
                        ActividadesData DAActi = new ActividadesData();
                        int num = 0;
                        foreach (Actividades eActi in pPersona1.lstActividad)
                        {
                            Actividades nActividad = new Actividades();

                            eActi.cod_persona = cod;
                            nActividad = DAActi.CrearActividadesPersona(eActi, pUsuario);
                            num += 1;
                        }
                    }

                    if (pPersona1.lstCuentasBan != null)
                    {
                        ActividadesData DACuenta = new ActividadesData();
                        int num = 0;
                        foreach (CuentasBancarias eCuentas in pPersona1.lstCuentasBan)
                        {
                            CuentasBancarias nCuentasBanc = new CuentasBancarias();
                            eCuentas.cod_persona = cod;
                            nCuentasBanc = DACuenta.CrearPer_CuentaFinac(eCuentas, pUsuario);
                            num += 1;
                        }
                    }

                    ts.Complete();
                }

                return pPersona1;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Business", "CrearPersona1", ex);
                return null;
            }
        }

        public Persona1 CrearPersonasImagenes(Persona1 pPersona1, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (pPersona1.foto != null)
                    {
                        Xpinn.FabricaCreditos.Entities.Imagenes pImagenes = new Xpinn.FabricaCreditos.Entities.Imagenes();
                        pImagenes.idimagen = 0;
                        pImagenes.cod_persona = pPersona1.cod_persona;
                        pImagenes.tipo_imagen = 1;
                        pImagenes.imagen = pPersona1.foto;
                        pImagenes.fecha = System.DateTime.Now;
                        pImagenes.tipo_documento = Convert.ToInt32(pPersona1.tipo_identificacion);
                        DAImagenes.CrearImagenesPersona(pImagenes, pUsuario);
                    }

                    ts.Complete();
                }

                return pPersona1;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Business", "CrearPersonaAporte", ex);
                return null;
            }
        }


        public Persona1 ModificarPersonasImagenes(Persona1 pPersona1, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (pPersona1.foto != null)
                    {
                        Xpinn.FabricaCreditos.Entities.Imagenes pImagenes = new Xpinn.FabricaCreditos.Entities.Imagenes();
                        pImagenes.idimagen = Convert.ToInt64(pPersona1.idimagen);
                        pImagenes.cod_persona = pPersona1.cod_persona;
                        pImagenes.tipo_imagen = 1;
                        pImagenes.imagen = pPersona1.foto;
                        pImagenes.fecha = System.DateTime.Now;
                        pImagenes.tipo_documento = Convert.ToInt32(pPersona1.tipo_identificacion);
                        DAImagenes.ModificarImagenesPersona(pImagenes, pUsuario);
                    }

                    ts.Complete();
                }

                return pPersona1;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Business", "CrearPersonaAporte", ex);
                return null;
            }
        }

        public Persona1 CrearPersonaAporte(Persona1 pPersona1, bool EsMenor, PersonaResponsable pDataMenor, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    Int64 cod;

                    pPersona1 = DAPersona1.CrearPersonaAporte(pPersona1, pUsuario, 1);

                    cod = pPersona1.cod_persona;

                    if (pPersona1.lstBeneficiarios != null)
                    {
                        BeneficiarioData DABenef = new BeneficiarioData();
                        int num = 0;
                        foreach (Beneficiario eBenef in pPersona1.lstBeneficiarios)
                        {
                            Beneficiario nBeneficiario = new Beneficiario();
                            eBenef.cod_persona = cod;
                            nBeneficiario = DABenef.CrearBeneficiario(eBenef, pUsuario);
                            num += 1;
                        }
                    }

                    if (pPersona1.lstActividad != null)
                    {
                        ActividadesData DAActi = new ActividadesData();
                        int num = 0;
                        foreach (Actividades eActi in pPersona1.lstActividad)
                        {
                            Actividades nActividad = new Actividades();

                            eActi.cod_persona = cod;
                            nActividad = DAActi.CrearActividadesPersona(eActi, pUsuario);
                            num += 1;
                        }
                    }

                    if (pPersona1.lstActEconomicasSecundarias != null)
                    {
                        ActividadesData DAActi = new ActividadesData();
                        foreach (Actividades objActividadEconomica in pPersona1.lstActEconomicasSecundarias)
                        {
                            DAActi.CrearActividadEconomicaSecundaria(cod, objActividadEconomica.codactividad, pUsuario);
                        }
                    }


                    if (pPersona1.lstCuentasBan != null)
                    {
                        ActividadesData DACuenta = new ActividadesData();
                        int num = 0;
                        foreach (CuentasBancarias eCuentas in pPersona1.lstCuentasBan)
                        {
                            CuentasBancarias nCuentasBanc = new CuentasBancarias();
                            eCuentas.cod_persona = cod;
                            nCuentasBanc = DACuenta.CrearPer_CuentaFinac(eCuentas, pUsuario);
                            num += 1;
                        }
                    }

                    if (pPersona1.lstEmpresaRecaudo != null)
                    {
                        PersonaEmpresaRecaudoData DAEmpresa = new PersonaEmpresaRecaudoData();
                        int num = 0;
                        foreach (PersonaEmpresaRecaudo eEmpresa in pPersona1.lstEmpresaRecaudo)
                        {
                            PersonaEmpresaRecaudo nEmpresa = new PersonaEmpresaRecaudo();
                            eEmpresa.cod_persona = cod;
                            if (eEmpresa.seleccionar)
                                nEmpresa = DAEmpresa.CrearPersonaEmpresaRecaudo(eEmpresa, pUsuario);
                            else
                                if (eEmpresa.idempresarecaudo != null)
                                DAEmpresa.EliminarPersonaEmpresaRecaudo(Convert.ToInt64(eEmpresa.idempresarecaudo), pUsuario);
                            num += 1;
                        }
                    }

                    if (pPersona1.lstInformacion != null && pPersona1.lstInformacion.Count > 0)
                    {
                        InformacionAdicionalData DAInfor = new InformacionAdicionalData();
                        foreach (InformacionAdicional eInf in pPersona1.lstInformacion)
                        {
                            InformacionAdicional nInforma = new InformacionAdicional();
                            eInf.cod_persona = cod;
                            nInforma = DAInfor.CrearPersona_InfoAdicional(eInf, pUsuario);
                        }
                    }

                    if (EsMenor == true)
                    {
                        AfiliacionData DAAfiliacion = new AfiliacionData();
                        PersonaResponsable pEntidad = new PersonaResponsable();
                        pDataMenor.cod_persona = pPersona1.cod_persona;
                        pEntidad = DAAfiliacion.Crear_Mod_PersonaResponsable(pDataMenor, pUsuario, 1);


                    }

                    if (pPersona1.foto != null)
                    {
                        Xpinn.FabricaCreditos.Entities.Imagenes pImagenes = new Xpinn.FabricaCreditos.Entities.Imagenes();
                        pImagenes.idimagen = 0;
                        pImagenes.cod_persona = pPersona1.cod_persona;
                        pImagenes.tipo_imagen = 1;
                        pImagenes.imagen = pPersona1.foto;
                        pImagenes.fecha = System.DateTime.Now;

                        DAImagenes.CrearImagenesPersona(pImagenes, pUsuario);
                    }
                    //Agregado para información de moneda extranjera
                    if (pPersona1.lstMonedaExt != null)
                    {
                        EstadosFinancierosData DAEstadosFinancieros = new EstadosFinancierosData();
                        foreach (EstadosFinancieros eMoneda in pPersona1.lstMonedaExt)
                        {
                            EstadosFinancieros pMoneda = new EstadosFinancieros();
                            eMoneda.cod_persona = cod;
                            pMoneda = DAEstadosFinancieros.CrearMonedaExtranjera(eMoneda, pUsuario);
                        }
                    }

                    if (pPersona1.lstProductosFinExt != null)
                    {
                        EstadosFinancierosData DAEstadosFinancieros = new EstadosFinancierosData();
                        foreach (EstadosFinancieros eMoneda in pPersona1.lstProductosFinExt)
                        {
                            EstadosFinancieros pMoneda = new EstadosFinancieros();
                            eMoneda.cod_persona = cod;
                            pMoneda = DAEstadosFinancieros.CrearMonedaExtranjera(eMoneda, pUsuario);
                        }
                    }

                    //Determinar perfil de riesgo de la persona
                    Riesgo.Data.PerfilData DAPerfil = new Riesgo.Data.PerfilData();
                    DAPerfil.CrearPerfilPersona(pPersona1, null, false, pUsuario);


                    ts.Complete();
                }

                return pPersona1;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Business", "CrearPersonaAporte", ex);
                return null;
            }
        }

        public Persona1 ConsultarDatosCliente(Persona1 pPersona, Usuario pUsuario)
        {
            try
            {
                Persona1 datos = new Persona1();
                datos = DAPersona1.ConsultarDatosCliente(pPersona, pUsuario);
                return datos;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Business", "ConsultarDatosCliente", ex);
                return null;
            }
        }

        public string ConsultarIdentificacionPersona(long codPersona, Usuario usuario)
        {
            try
            {
                return DAPersona1.ConsultarIdentificacionPersona(codPersona, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "ConsultarIdentificacionPersona", ex);
                return null;
            }
        }

        public Persona1 ModificarConyugeAporte(Persona1 pPersona1, bool esMenor, PersonaResponsable pDataMenor, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pPersona1 = DAPersona1.ModificarPersonaConyugue(pPersona1, pUsuario);

                    ts.Complete();
                }

                return pPersona1;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Business", "ModificarPersonaAporte", ex);
                return null;
            }
        }


        public Persona1 ModificarPersonaAporte(Persona1 pPersona1, bool EsMenor, PersonaResponsable pDataMenor, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pPersona1 = DAPersona1.CrearPersonaAporte(pPersona1, pUsuario, 2); //MODIFICAR
                    Int64 Cod;
                    Cod = pPersona1.cod_persona;

                    if (pPersona1.lstBeneficiarios != null && pPersona1.origen != "Afiliacion-Conyuge")
                    {
                        BeneficiarioData DABenef = new BeneficiarioData();
                        int num = 0;
                        foreach (Beneficiario eBenef in pPersona1.lstBeneficiarios)
                        {
                            eBenef.cod_persona = Cod;
                            Beneficiario nBeneficiario = new Beneficiario();
                            if (eBenef.idbeneficiario <= 0)
                                nBeneficiario = DABenef.CrearBeneficiario(eBenef, pUsuario);
                            else
                                nBeneficiario = DABenef.ModificarBeneficiario(eBenef, pUsuario);
                            num += 1;
                        }
                    }

                    if (pPersona1.lstActividad != null && pPersona1.origen != "Afiliacion-Conyuge")
                    {
                        ActividadesData DAActiv = new ActividadesData();
                        int num = 0;
                        foreach (Actividades eActi in pPersona1.lstActividad)
                        {
                            eActi.cod_persona = Cod;
                            Actividades nActividad = new Actividades();
                            if (eActi.idactividad <= 0 || eActi.fecha_realizacion == DateTime.MinValue)
                            {
                                nActividad = DAActiv.CrearActividadesPersona(eActi, pUsuario);
                            }
                            else
                                nActividad = DAActiv.ModificarActividadesPersona(eActi, pUsuario);
                            num += 1;
                        }
                    }

                    ActividadesData DACuenta = new ActividadesData();
                    if (pPersona1.lstCuentasBan != null && pPersona1.origen != "Afiliacion-Conyuge")
                    {
                        int num = 0;
                        foreach (CuentasBancarias eCuenta in pPersona1.lstCuentasBan)
                        {
                            eCuenta.cod_persona = Cod;
                            CuentasBancarias nCuentaBanc = new CuentasBancarias();
                            if (eCuenta.idcuentabancaria <= 0 || eCuenta.fecha_apertura == DateTime.MinValue || eCuenta.numero_cuenta == "")
                            {
                                nCuentaBanc = DACuenta.CrearPer_CuentaFinac(eCuenta, pUsuario);
                            }
                            else
                                nCuentaBanc = DACuenta.ModificarPer_CuentaFinac(eCuenta, pUsuario);
                            num += 1;
                        }
                    }

                    DACuenta.EliminarActividadesEconomicasSecundarias(pPersona1.cod_persona, pUsuario);
                    if (pPersona1.lstActEconomicasSecundarias != null)
                    {
                        foreach (Actividades objActividad in pPersona1.lstActEconomicasSecundarias)
                        {
                            DACuenta.CrearActividadEconomicaSecundaria(pPersona1.cod_persona, objActividad.codactividad, pUsuario);
                        }
                    }


                    if (pPersona1.lstEmpresaRecaudo != null && pPersona1.origen != "Afiliacion-Conyuge")
                    {
                        PersonaEmpresaRecaudoData DAEmpresa = new PersonaEmpresaRecaudoData();
                        int num = 0;
                        foreach (PersonaEmpresaRecaudo eEmpresa in pPersona1.lstEmpresaRecaudo)
                        {
                            PersonaEmpresaRecaudo nEmpresa = new PersonaEmpresaRecaudo();
                            eEmpresa.cod_persona = Cod;
                            if (eEmpresa.seleccionar)
                            {
                                if (eEmpresa.idempresarecaudo != null)
                                    nEmpresa = DAEmpresa.ModificarPersonaEmpresaRecaudo(eEmpresa, pUsuario);
                                else
                                    nEmpresa = DAEmpresa.CrearPersonaEmpresaRecaudo(eEmpresa, pUsuario);
                            }
                            else
                            {
                                if (eEmpresa.idempresarecaudo != null)
                                    DAEmpresa.EliminarPersonaEmpresaRecaudo(Convert.ToInt64(eEmpresa.idempresarecaudo), pUsuario);
                            }
                            num += 1;
                        }
                    }

                    if (pPersona1.lstInformacion != null && pPersona1.lstInformacion.Count > 0)
                    {
                        InformacionAdicionalData DAInfor = new InformacionAdicionalData();
                        foreach (InformacionAdicional eInf in pPersona1.lstInformacion)
                        {
                            InformacionAdicional nInforma = new InformacionAdicional();
                            eInf.cod_persona = Cod;
                            if (eInf.idinfadicional != 0)
                                nInforma = DAInfor.ModificarPersona_InfoAdicional(eInf, pUsuario);
                            else
                                nInforma = DAInfor.CrearPersona_InfoAdicional(eInf, pUsuario);
                        }
                    }


                    if (EsMenor == true)
                    {
                        AfiliacionData DAAfiliacion = new AfiliacionData();
                        PersonaResponsable pEntidad = new PersonaResponsable();
                        pDataMenor.cod_persona = pPersona1.cod_persona;
                        int opcion = pDataMenor.consecutivo == 0 ? 1 : 2;
                        pEntidad = DAAfiliacion.Crear_Mod_PersonaResponsable(pDataMenor, pUsuario, opcion);
                    }

                    // Logica para interactuar con la foto de la persona
                    if (pPersona1.foto != null)
                    {
                        Entities.Imagenes pImagenes = new Xpinn.FabricaCreditos.Entities.Imagenes();
                        pImagenes.idimagen = 0;
                        pImagenes.cod_persona = pPersona1.cod_persona;
                        pImagenes.tipo_imagen = 1;
                        pImagenes.imagen = pPersona1.foto;
                        pImagenes.fecha = DateTime.Now;
                        pImagenes.tipo_documento = Convert.ToInt32(pPersona1.tipo_identificacion);
                        // Foto persona 
                        // Para modificar no necesito el consecutivo, solo busco si existe, si de verdad existe
                        // modifico segun el codigo persona y el tipo de documento
                        if (DAImagenes.ExisteImagenPersona(Convert.ToInt64(pPersona1.cod_persona), 1, pUsuario))
                            DAImagenes.ModificarImagenesPersona(pImagenes, pUsuario);
                        else
                            DAImagenes.CrearImagenesPersona(pImagenes, pUsuario);
                    }

                    //Agregado para información de moneda extranjera
                    if (pPersona1.lstMonedaExt != null)
                    {
                        EstadosFinancierosData DAEstadosFinancieros = new EstadosFinancierosData();
                        foreach (EstadosFinancieros eMoneda in pPersona1.lstMonedaExt)
                        {
                            EstadosFinancieros pMoneda = new EstadosFinancieros();
                            eMoneda.cod_persona = pPersona1.cod_persona;
                            if (eMoneda.cod_moneda_ext != null && eMoneda.cod_moneda_ext != 0)
                                pMoneda = DAEstadosFinancieros.ModificarMonedaExtranjera(eMoneda, pUsuario);
                            else if (eMoneda.cod_moneda_ext == null || eMoneda.cod_moneda_ext == 0)
                                pMoneda = DAEstadosFinancieros.CrearMonedaExtranjera(eMoneda, pUsuario);
                        }
                    }

                    if (pPersona1.lstProductosFinExt != null)
                    {
                        EstadosFinancierosData DAEstadosFinancieros = new EstadosFinancierosData();
                        foreach (EstadosFinancieros eMoneda in pPersona1.lstProductosFinExt)
                        {
                            EstadosFinancieros pMoneda = new EstadosFinancieros();
                            eMoneda.cod_persona = pPersona1.cod_persona;
                            if (eMoneda.cod_moneda_ext != null && eMoneda.cod_moneda_ext != 0)
                                pMoneda = DAEstadosFinancieros.ModificarMonedaExtranjera(eMoneda, pUsuario);
                            else if (eMoneda.cod_moneda_ext == null || eMoneda.cod_moneda_ext == 0)
                                pMoneda = DAEstadosFinancieros.CrearMonedaExtranjera(eMoneda, pUsuario);
                        }
                    }

                   
                    ts.Complete();
                }

                return pPersona1;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Business", "ModificarPersonaAporte", ex);
                return null;
            }
        }

        public long ConsultarCodigoEmpresaPagaduria(string identificacion, Usuario usuario)
        {
            try
            {
                return DAPersona1.ConsultarCodigoEmpresaPagaduria(identificacion, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "ConsultarCodigoEmpresaPagaduria", ex);
                return 0;
            }
        }

        public long ConsultarCodigoEmpresaPagaduria(Int64 pCodPersona, Usuario usuario)
        {
            try
            {
                return DAPersona1.ConsultarCodigoEmpresaPagaduria(pCodPersona, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "ConsultarCodigoEmpresaPagaduria", ex);
                return 0;
            }
        }

        public List<Persona1> ConsultarPersonasAfiliadas(string filtro, Usuario usuario)
        {
            try
            {
                return DAPersona1.ConsultarPersonasAfiliadas(filtro, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "ConsultarPersonasAfiliadas", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un Persona1
        /// </summary>
        /// <param name="pPersona1">Entidad Persona1</param>
        /// <returns>Entidad Persona1 modificada</returns>
        public Persona1 ModificarPersona1(Persona1 pPersona1, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pPersona1 = DAPersona1.ModificarPersona1(pPersona1, pUsuario);
                    Int64 Cod;
                    Cod = pPersona1.cod_persona;

                    if (pPersona1.lstBeneficiarios != null)
                    {
                        BeneficiarioData DABenef = new BeneficiarioData();
                        int num = 0;
                        foreach (Beneficiario eBenef in pPersona1.lstBeneficiarios)
                        {
                            eBenef.cod_persona = Cod;
                            Beneficiario nBeneficiario = new Beneficiario();
                            if (eBenef.idbeneficiario <= 0)
                                nBeneficiario = DABenef.CrearBeneficiario(eBenef, pUsuario);
                            else
                                nBeneficiario = DABenef.ModificarBeneficiario(eBenef, pUsuario);
                            num += 1;
                        }
                    }

                    if (pPersona1.lstActividad != null)
                    {
                        ActividadesData DAActiv = new ActividadesData();
                        int num = 0;
                        foreach (Actividades eActi in pPersona1.lstActividad)
                        {
                            eActi.cod_persona = Cod;
                            Actividades nActividad = new Actividades();
                            if (eActi.idactividad <= 0 || eActi.fecha_realizacion == DateTime.MinValue)
                            {
                                nActividad = DAActiv.CrearActividadesPersona(eActi, pUsuario);
                            }
                            else
                                nActividad = DAActiv.ModificarActividadesPersona(eActi, pUsuario);
                            num += 1;
                        }
                    }

                    if (pPersona1.lstCuentasBan != null)
                    {
                        ActividadesData DACuenta = new ActividadesData();
                        int num = 0;
                        foreach (CuentasBancarias eCuenta in pPersona1.lstCuentasBan)
                        {
                            eCuenta.cod_persona = Cod;
                            CuentasBancarias nCuentaBanc = new CuentasBancarias();
                            if (eCuenta.idcuentabancaria <= 0 || eCuenta.fecha_apertura == DateTime.MinValue || eCuenta.numero_cuenta == "")
                            {
                                nCuentaBanc = DACuenta.CrearPer_CuentaFinac(eCuenta, pUsuario);
                            }
                            else
                                nCuentaBanc = DACuenta.ModificarPer_CuentaFinac(eCuenta, pUsuario);
                            num += 1;
                        }
                    }

                    if (pPersona1.lstEmpresaRecaudo != null)
                    {
                        PersonaEmpresaRecaudoData DAEmpresa = new PersonaEmpresaRecaudoData();
                        int num = 0;
                        foreach (PersonaEmpresaRecaudo eEmpresa in pPersona1.lstEmpresaRecaudo)
                        {
                            PersonaEmpresaRecaudo nEmpresa = new PersonaEmpresaRecaudo();
                            eEmpresa.cod_persona = Cod;
                            if (eEmpresa.seleccionar)
                            {
                                if (eEmpresa.idempresarecaudo != null)
                                    nEmpresa = DAEmpresa.ModificarPersonaEmpresaRecaudo(eEmpresa, pUsuario);
                                else
                                    nEmpresa = DAEmpresa.CrearPersonaEmpresaRecaudo(eEmpresa, pUsuario);
                            }
                            else
                            {
                                if (eEmpresa.idempresarecaudo != null)
                                    DAEmpresa.EliminarPersonaEmpresaRecaudo(Convert.ToInt64(eEmpresa.idempresarecaudo), pUsuario);
                            }
                            num += 1;
                        }
                    }

                    if (pPersona1.foto != null)
                    {
                        Xpinn.FabricaCreditos.Entities.Imagenes pImagenes = new Xpinn.FabricaCreditos.Entities.Imagenes();
                        pImagenes.idimagen = 0;
                        pImagenes.cod_persona = pPersona1.cod_persona;
                        pImagenes.tipo_imagen = 1;
                        pImagenes.imagen = pPersona1.foto;
                        pImagenes.fecha = System.DateTime.Now;
                        if (DAImagenes.ExisteImagenPersona(Convert.ToInt64(pPersona1.cod_persona), 1, pUsuario))
                            DAImagenes.ModificarImagenesPersona(pImagenes, pUsuario);
                        else
                            DAImagenes.CrearImagenesPersona(pImagenes, pUsuario);
                    }
                    // REGISTRO DE CONTROL DE ACTUALIZACION
                    PersonaActDatosData DAPersonaUpdate = new PersonaActDatosData();
                    Xpinn.Aportes.Entities.PersonaActDatos pPersonaActualizacion = new Aportes.Entities.PersonaActDatos();

                    pPersonaActualizacion.cod_persona = pPersona1.cod_persona;
                    pPersonaActualizacion.cod_usua = pUsuario.codusuario;
                    pPersonaActualizacion.fecha_act = DateTime.Now;

                    DAPersonaUpdate.CrearPersonaActDatos(pPersonaActualizacion, pUsuario);

                    ts.Complete();
                }

                return pPersona1;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Business", "ModificarPersona1", ex);
                return null;
            }
        }

        public long ConsultarCodigopersona(string pIdentificacion, Usuario pUsuario)
        {
            try
            {
                return DAPersona1.ConsultarCodigopersona(pIdentificacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Business", "ConsultarCodigopersona", ex);
                return 0;
            }
        }

        /// <summary>
        /// Elimina un Persona1
        /// </summary>
        /// <param name="pId">Identificador de Persona1</param>
        public void EliminarPersona1(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAPersona1.EliminarPersona1(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Business", "EliminarPersona1", ex);
            }
        }

        /// <summary>
        /// Obtiene un Persona1
        /// </summary>
        /// <param name="pId">Identificador de Persona1</param>
        /// <returns>Entidad Persona1</returns>
        public Persona1 ConsultarPersona1(Int64 pId, Usuario pUsuario)
        {
            try
            {
                Persona1 ePersona = new Persona1();
                ePersona = DAPersona1.ConsultarPersona1(pId, pUsuario);
                //ePersona = DAPersona1.ConsultarNotificaciones(pId, pUsuario);
                if (ePersona.cod_persona != 0)
                {
                    ePersona.lstBeneficiarios = new List<Beneficiario>();
                    BeneficiarioData DABenef = new BeneficiarioData();
                    Beneficiario eBeneficiario = new Beneficiario();
                    eBeneficiario.cod_persona = ePersona.cod_persona;
                    ePersona.lstBeneficiarios = DABenef.ListarBeneficiario(eBeneficiario, pUsuario);
                }
                return ePersona;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Business", "ConsultarPersona1", ex);
                return null;
            }
        }
        public Persona1 ConsultarNotificacion(Int64 pId, Usuario pUsuario, int opcion)
        {
            try
            {
                Persona1 ePersona = new Persona1();

                ePersona = DAPersona1.ConsultarNotificaciones(pId, pUsuario, opcion);

                return ePersona;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Business", "ConsultarNotificacion", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene un Persona1
        /// </summary>
        /// <param name="pId">Identificador de Persona1</param>
        /// <returns>Entidad Persona1</returns>
        public Persona1 ConsultarPersona1TrasladoOficina(Int64 pId, Usuario pUsuario)
        {
            try
            {
                Persona1 ePersona = new Persona1();
                ePersona = DAPersona1.ConsultarPersona1TrasladoOficina(pId, pUsuario);
                return ePersona;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Business", "ConsultarPersona1TrasladoOficina", ex);
                return null;
            }
        }

        public Persona1 ModificarTrasladoOficina(Persona1 pTrasladoOficina, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    foreach (Productos_Persona producto in pTrasladoOficina.Lista_Producto)
                    {
                        Productos_Persona _producto = new Productos_Persona();
                        _producto = DAPersona1.ModificarTrasladoOficina(producto, pTrasladoOficina.cod_persona, pusuario);
                    }
                    ts.Complete();
                }

                return pTrasladoOficina;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Business", "ModificarTrasladoOficina", ex);
                return null;
            }
        }

        public Persona1 Consultarnegocio(string cod, Usuario pUsuario)
        {
            try
            {
                return DAPersona1.Consultarnegocio(cod, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "ConsultarPersona1", ex);
                return null;
            }
        }
        /// <summary>
        /// Obtiene un Persona1 cedula
        /// </summary>
        /// <param name="pId">Identificador de Persona1</param>
        /// <returns>Entidad Persona1</returns>
        public Persona1 ConsultarPersona1Param(Persona1 pPersona1, Usuario pUsuario)
        {
            try
            {
                Persona1 pPersona = new Persona1();
                pPersona = DAPersona1.ConsultarPersona1Param(pPersona1, pUsuario);
                if (pPersona != null)
                {
                    if (pPersona.sinImagen == null)
                    {
                        Int64 idImagen = 0;
                        pPersona.foto = DAImagenes.ConsultarImagenPersona(Convert.ToInt64(pPersona.cod_persona != 0 ? pPersona.cod_persona : pPersona1.cod_persona), Convert.ToInt64(pPersona.tipo_identificacion), ref idImagen, pUsuario);
                        pPersona.idimagen = idImagen;
                    }

                    ActividadesData objActividadesData = new ActividadesData();
                    pPersona.lstActEconomicasSecundarias = objActividadesData.ConsultarActividadesEconomicasSecundarias(pPersona1.cod_persona, pUsuario);

                }
                return pPersona;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Business", "ConsultarPersona1Param", ex);
                return null;
            }
        }

        public Persona1 crearidentificacion(Persona1 pPersona1, Usuario pUsuario)
        {
            try
            {
                return DAPersona1.crearidentificacion(pPersona1, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "ConsultarPersona1Param", ex);
                return null;
            }
        }

        public void InsertarRespuestasRetiro(List<Persona1> pPersona, Usuario pUsuario)
        {
            try
            {
                DAPersona1.InsertarRespuestasRetiro(pPersona, pUsuario);
                return;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "InsertarRespuestasRetiro", ex);
                return;
            }
        }

        public Persona1 ConsultaDatosCliente(Persona1 pPersona, Usuario pUsuario)
        {
            try
            {
                return DAPersona1.ConsultaDatosCliente(pPersona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Business", "ConsultaDatosCliente", ex);
                return null;
            }
        }

        public Persona1 consultaridentificacion(Usuario pUsuario)
        {
            try
            {
                return DAPersona1.consultaridentificacion(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "ConsultarPersona1Param", ex);
                return null;
            }
        }

        public bool VerificarSiPersonaEsAsociado(long codigoPersona, Usuario usuario)
        {
            try
            {
                return DAPersona1.VerificarSiPersonaEsAsociado(codigoPersona, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "VerificarSiPersonaEsAsociado", ex);
                return false;
            }
        }

        public bool VerificarSiPersonaEsNatural(long codigoPersona, Usuario usuario)
        {
            try
            {
                return DAPersona1.VerificarSiPersonaEsNatural(codigoPersona, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "VerificarSiPersonaEsNatural", ex);
                return false;
            }
        }

        public void eliminaridentificacion(Persona1 pPersona1, Usuario pUsuario)
        {
            try
            {
                DAPersona1.eliminaridentificacion(pPersona1, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "ConsultarPersona1Param", ex);
            }
        }

        public Int64? ConsultarPersona1Imagen(Int64 pCodPersona, Int64 TipoDoc, Usuario pUsuario)
        {
            try
            {
                byte[] foto;
                Int64 idImagen = 0;
                foto = DAImagenes.ConsultarImagenPersona(Convert.ToInt64(pCodPersona), TipoDoc, ref idImagen, pUsuario);
                return idImagen;
            }
            catch
            {
                return null;
            }
        }

        public Persona1 BuscarDepartamentoPorCodigoCiudad(long ciudad, Usuario usuario)
        {
            try
            {
                return DAPersona1.BuscarDepartamentoPorCodigoCiudad(ciudad, usuario);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool validaMorosoBusiness(Usuario pUsuario, string identificacion)
        {
            try
            {
                return DAPersona1.validadMoroso(pUsuario, identificacion);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Business", "ConsultarPersona1", ex);
                throw;
            }
        }

        public Persona1 ConsultarPersonaAPP(Persona1 pPersona1, Usuario pUsuario)
        {
            Persona1 pPersona = new Persona1();
            try
            {
                using (TransactionScope ts= new TransactionScope(TransactionScopeOption.Required,TimeSpan.MaxValue))
                {
                    pPersona = DAPersona1.ConsultarPersonaAPP(pPersona1, pUsuario);
                    Int64 idImagen = 0;
                    pPersona.foto = DAImagenes.ConsultarImagenPersona(Convert.ToInt64(pPersona1.cod_persona), 1, ref idImagen, pUsuario);
                    pPersona.idimagen = idImagen;
                    return pPersona;
                }
              
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Business", "ConsultarPersona1", ex);
                return null;
            }
        }

        public Persona1 ConsultarPersona1conyuge(Persona1 pPersona1, Usuario pUsuario)
        {
            try
            {
                return DAPersona1.ConsultarPersona1conyuge(pPersona1, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Business", "ConsultarPersona1conyuge", ex);
                return null;
            }
        }

        public List<Referencia> referencias(Persona1 pPersona1, long radicacion, Usuario pUsuario)
        {
            try
            {
                return DAPersona1.referencias(pPersona1, radicacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "ConsultarPersona1", ex);
                return null;
            }
        }

        public List<Referncias> ListadoPersonas1ReporteReferencias(Referncias preferencias, Usuario pUsuario)
        {
            try
            {
                return DAPersona1.ListadoPersonas1ReporteReferencias(preferencias, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Business", "ListadoPersonas1ReporteReferencias", ex);
                return null;
            }
        }

        public Referncias ListadoPersonas1ReporteReferencias(Int64 pnumero_credito, string pidentificacion, Usuario pUsuario)
        {
            try
            {
                return DAPersona1.ListadoPersonas1ReporteReferencias(pnumero_credito, pidentificacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Business", "ListadoPersonas1ReporteReferencias", ex);
                return null;
            }
        }

        public List<Persona1> ListadoPersonas1ReporteFamiliares(Persona1 Ppersona, Usuario pUsuario)
        {
            try
            {
                return DAPersona1.ListadoPersonas1ReporteFamiliares(Ppersona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Business", "ListadoPersonas1ReporteFamiliares", ex);
                return null;
            }
        }

        public List<Persona1> ListadoPersonas1ReporteCodeudor(Persona1 Pcodeudor, Usuario pUsuario)
        {
            try
            {
                return DAPersona1.ListadoPersonas1ReporteCodeudor(Pcodeudor, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Business", "ListadoPersonas1ReporteCodeudor", ex);
                return null;
            }
        }

        public InformacionNegocio Consultardatosnegocio(long radicacion, string identificacion, Usuario pUsuario)
        {
            try
            {
                return DAPersona1.Consultardatosnegocio(radicacion, identificacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Business", "ConsultarPersona1", ex);
                return null;
            }
        }

        public List<VentasSemanales> ListadoEstacionalidadSemanal(VentasSemanales identificacion, Usuario pUsuario)
        {
            try
            {
                return DAPersona1.ListadoEstacionalidadSemanal(identificacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Business", "ListadoEstacionalidadSemanal", ex);
                return null;
            }
        }

        public List<EstacionalidadMensual> ListadoEstacionalidadMensual(EstacionalidadMensual identificacion, Usuario pUsuario)
        {
            try
            {
                return DAPersona1.ListadoEstacionalidadMensual(identificacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Business", "ListadoEstacionalidadMensual", ex);
                return null;
            }
        }

        public CreditoPlan ConsultarPersona1Paramcred(long radicacion, string identificacion, Usuario pUsuario)
        {
            try
            {
                return DAPersona1.ConsultarPersona1Paramcred(radicacion, identificacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Business", "ConsultarPersona1", ex);
                return null;
            }
        }

        public List<Persona1> ListadoPersonas1Reporte(Persona1 pPersona1, Usuario pUsuario)
        {
            try
            {
                return DAPersona1.ListadoPersonas1Reporte(pPersona1, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Business", "ConsultarPersona1", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pPersona1">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Persona1 obtenidos</returns>
        public List<Persona1> ListarPersona1(Persona1 pPersona1, Usuario pUsuario)
        {
            try
            {
                return DAPersona1.ListarPersona1(pPersona1, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Business", "ListarPersona1", ex);
                return null;
            }
        }

        public List<Persona1> Listarsolicitudesdecredito(Persona1 pPersona1, Usuario pUsuario, String filtro)
        {
            try
            {
                return DAPersona1.Listarsolicitudesdecredito(pPersona1, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Business", "ListarPersona1", ex);
                return null;
            }
        }

        public long consultarSolicitud(long radicacion, Usuario pUsuario)
        {
            try
            {
                return DAPersona1.consultarSolicitud(radicacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosClienteService", "ListarListasMenu", ex);
                return 0;
            }
        }
        public List<Persona1> ListasBarrios(Int32 ciudad, Usuario pUsuario)
        {
            try
            {
                return DAPersona1.ListasBarrios(ciudad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosClienteBusiness", "ListarTiposDoc", ex);
                return null;
            }
        }
        /// <summary>
        /// Obtiene la lista de tipos documento dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de tipos documento obtenidos</returns>
        public List<Persona1> ListasDesplegables(String ListaSolicitada, Usuario pUsuario)
        {
            try
            {
                return DAPersona1.ListasDesplegables(ListaSolicitada, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosClienteBusiness", "ListarTiposDoc", ex);
                return null;
            }
        }

        public List<Persona1> ListadoPersonas1(Persona1 pPersona1, Usuario pUsuario)
        {
            try
            {
                return DAPersona1.ListadoPersonas1(pPersona1, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Business", "ListadoPersonas1", ex);
                return null;
            }
        }

        public List<Persona1> ListarddLinea(string pFiltro, Usuario pUsuario)
        {
            try
            {
                return DAPersona1.ListarddLinea(pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosClienteBusiness", "listaddlBusines", ex);
                return null;
            }
        }

        public Persona1 ConsultaDatosPersona(string pidentificacion, Usuario pUsuario)
        {
            try
            {
                return DAPersona1.ConsultaDatosPersona(pidentificacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Business", "ConsultaDatosPersona", ex);
                return null;
            }
        }

        public Persona1 ConsultaDatosPersona(Int64 pCodigo, Usuario pUsuario)
        {
            try
            {
                Persona1 pPersona = new Persona1();
                pPersona = DAPersona1.ConsultaDatosPersona(pCodigo, pUsuario);
                return pPersona;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Business", "ConsultaDatosPersona", ex);
                return null;
            }
        }

        public List<Xpinn.FabricaCreditos.Entities.Imagenes> Handler(Xpinn.FabricaCreditos.Entities.Imagenes pImagenes, Usuario pUsuario)
        {
            try
            {
                return DAImagenes.Handler(pImagenes, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaBusiness", "Handler", ex);
                return null;
            }
        }

        public List<Xpinn.FabricaCreditos.Entities.Imagenes> HandlerHuella(Xpinn.FabricaCreditos.Entities.Imagenes pImagenes, Usuario pUsuario)
        {
            try
            {
                return DAImagenes.HandlerHuella(pImagenes, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaBusiness", "HandlerHuella", ex);
                return null;
            }
        }

        public Persona1 ModificarPersonaAtencionCliente(Persona1 pPersona1, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pPersona1 = DAPersona1.ModificarPersonaAtencionCliente(pPersona1, pUsuario);

                    ts.Complete();
                }

                return pPersona1;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Business", "ModificarPersonaAtencionCliente", ex);
                return null;
            }
        }

        public Boolean ModificarPersonaAPP(Persona1 pPersona1, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAPersona1.ModificarPersonaAPP(pPersona1, pUsuario);
                    ts.Complete();
                }
                return true;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Business", "ModificarPersonaAtencionCliente", ex);
                return false;
            }
        }

        public bool ConsultaClavePersona(string pIdentificacion, string pClave, Usuario pUsuario)
        {
            try
            {
                return DAPersona1.ConsultaClavePersona(pIdentificacion, pClave, pUsuario);
            }
            catch
            {
                return false;
            }
        }

        public List<PersonaAutorizacion> ListarPersonaAutorizacion(PersonaAutorizacion pPersonaAutorizacion, Usuario pUsuario)
        {
            try
            {
                return DAPersona1.ListarPersonaAutorizacion(pPersonaAutorizacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Business", "ListarPersonaAutorizacion", ex);
                return null;
            }
        }

        public List<PersonaAutorizacion> ListarPersonaAutorizacion(string pIdentificacion, Usuario pUsuario)
        {
            try
            {
                return DAPersona1.ListarPersonaAutorizacion(pIdentificacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Business", "ListarPersonaAutorizacion", ex);
                return null;
            }
        }

        public PersonaAutorizacion ModificarPersonaAutorizacion(PersonaAutorizacion pPersonaAutorizacion, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pPersonaAutorizacion = DAPersona1.ModificarPersonaAutorizacion(pPersonaAutorizacion, pUsuario);

                    ts.Complete();
                }

                return pPersonaAutorizacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Business", "ModificarPersonaAutorizacion", ex);
                return null;
            }
        }

        public List<PersonaAutorizacion> ListarUsuarioAutorizacion(Int64 pCodUsuario, Usuario pUsuario)
        {
            try
            {
                return DAPersona1.ListarUsuarioAutorizacion(pCodUsuario, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Business", "ListarPersonaAutorizacion", ex);
                return null;
            }
        }

        public Xpinn.FabricaCreditos.Entities.Imagenes ConsultarImagenesPersonaIdentificacion(string pId, Int64 pTipoImagen, Usuario pUsuario)
        {
            try
            {
                return DAImagenes.ConsultarImagenesPersonaIdentificacion(pId, pTipoImagen, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Business", "ConsultarImagenesPersonaIdentificacion", ex);
                return null;
            }
        }

        public byte[] ConsultarImagenHuellaPersona(Int64 pId, Int64 pDedo, ref Int64 pIdImagen, Usuario pUsuario)
        {
            try
            {
                return DAImagenes.ConsultarImagenHuellaPersona(pId, pDedo, ref pIdImagen, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Business", "ConsultarImagenHuellaPersona", ex);
                return null;
            }
        }

        public Persona1 ConsultarPersona(Int64? pCod, string pId, Usuario pUsuario)
        {
            try
            {

                return DAPersona1.ConsultarPersona(pCod, pId, pUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Business", "ConsultarPersona", ex);
                return null;
            }
        }

        public Xpinn.Comun.Entities.General consultarsalariominimo(Int64? pCod, Usuario pUsuario)
        {
            try
            {

                return DAPersona1.consultarsalariominimo(pCod, pUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Business", "consultarsalariominimo", ex);
                return null;
            }
        }


        public Persona1 consultaraportes(Int64? pCod, string pId, Usuario pUsuario)
        {
            try
            {

                return DAPersona1.consultaraportes(pCod, pId, pUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Business", "consultaraportes", ex);
                return null;
            }
        }
        public Persona1 consultarcreditos(Int64? pCod, string pId, Usuario pUsuario)
        {
            try
            {

                return DAPersona1.consultarcreditos(pCod, pId, pUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Business", "consultarcreditos", ex);
                return null;
            }
        }

        public List<Credito> ConsultarResumenPersona(Int64 pCodPersona, Usuario vUsuario)
        {
            try
            {

                return DAPersona1.ConsultarResumenPersona(pCodPersona, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Business", "ConsultarResumenPersona", ex);
                return null;
            }
        }


        public Xpinn.FabricaCreditos.Entities.Imagenes CrearImagenesPersona(Xpinn.FabricaCreditos.Entities.Imagenes pImagen, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAImagenes.CrearImagenesPersona(pImagen, pUsuario);

                    ts.Complete();
                }

                return pImagen;
            }
            catch
            {
                pImagen.idimagen = 0;
                return pImagen;
            }
        }

        public Xpinn.FabricaCreditos.Entities.Imagenes ModificarImagenesPersona(Xpinn.FabricaCreditos.Entities.Imagenes pImagen, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAImagenes.ModificarImagenesPersona(pImagen, pUsuario);

                    ts.Complete();
                }

                return pImagen;
            }
            catch
            {
                pImagen.idimagen = 0;
                return pImagen;
            }
        }


        private StreamReader strReader;
        public void CargarPersonas(DateTime pFechaCarga, string pTipoCarga, string pTipoPersona, string sformato_fecha, Stream pstream, ref string perror, ref List<Xpinn.Contabilidad.Entities.Tercero> lstJuridica, ref List<Persona1> lstNatural, ref List<Xpinn.FabricaCreditos.Entities.CuentasBancarias> lstCtaBancarias, ref List<ErroresCarga> plstErrores, Usuario pUsuario)
        {
            ActividadesData DAActivadesPersona = new ActividadesData();
            Configuracion conf = new Configuracion();
            string sSeparadorDecimal = conf.ObtenerSeparadorDecimalConfig();

            string readLine;
            bool sinErrores = true;
            int contadorreg = 1;

            // Inicializar control de errores
            RegistrarError(-1, "", "", "", ref plstErrores);

            try
            {
                using (strReader = new StreamReader(pstream))
                {
                    //recorriendo las filas del archivo
                    while (strReader.Peek() >= 0)
                    {
                        //BAJANDO LA FILA A UNA VARIABLE
                        readLine = strReader.ReadLine();
                        char Separador = '|';

                        //Separando la data a un array
                        string[] arrayline = readLine.Split(Separador);
                        if (pTipoCarga == "P")
                        {
                            if (pTipoPersona == "N")
                            {
                                Persona1 pEntidad = new Persona1();
                                for (int i = 0; i <= 32; i++)
                                {
                                    pEntidad.cod_persona = 0;
                                    pEntidad.tipo_persona = "N";
                                    pEntidad.digito_verificacion = 0;
                                    if (i == 0)
                                    { try { pEntidad.identificacion = Convert.ToString(arrayline[i].ToString().Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                                    if (i == 1)
                                    { try { pEntidad.tipo_identificacion = Convert.ToInt64(arrayline[i].ToString().Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                                    if (i == 2)
                                    {
                                        try { pEntidad.fechaexpedicion = DateTime.ParseExact(arrayline[i].ToString().Trim(), sformato_fecha, null); break; }
                                        catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; }
                                    }
                                    if (i == 3)
                                    { try { pEntidad.codciudadexpedicion = Convert.ToInt64(arrayline[i].ToString()); } catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                                    if (i == 4)
                                    {
                                        try
                                        {
                                            pEntidad.sexo = Convert.ToString(arrayline[i].ToString());
                                        }
                                        catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; }
                                    }
                                    if (i == 5)
                                    {
                                        try
                                        {
                                            pEntidad.primer_nombre = Convert.ToString(arrayline[i].ToString().Trim());
                                        }
                                        catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; }
                                    }
                                    if (i == 6)
                                    {
                                        try
                                        {
                                            if (arrayline[i].ToString().Trim() != null && arrayline[i].ToString().Trim() != "")
                                                pEntidad.segundo_nombre = Convert.ToString(arrayline[i].ToString().Trim());
                                            else
                                                pEntidad.segundo_nombre = null;
                                        }
                                        catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; }
                                    }
                                    if (i == 7)
                                    { try { pEntidad.primer_apellido = Convert.ToString(arrayline[i].ToString().Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                                    if (i == 8)
                                    {
                                        try
                                        {
                                            if (arrayline[i].ToString().Trim() != null && arrayline[i].ToString().Trim() != "")
                                                pEntidad.segundo_apellido = Convert.ToString(arrayline[i].ToString().Trim());
                                            else
                                                pEntidad.segundo_apellido = null;
                                        }
                                        catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; }
                                    }
                                    if (i == 9)
                                    {
                                        try
                                        {
                                            pEntidad.fechanacimiento = DateTime.ParseExact(arrayline[i].ToString().Trim(), sformato_fecha, null);
                                        }
                                        catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; }
                                    }
                                    if (i == 10)
                                    {
                                        try
                                        {
                                            pEntidad.codciudadnacimiento = Convert.ToInt64(arrayline[i].ToString().Trim());
                                        }
                                        catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; }
                                    }
                                    if (i == 11)
                                    { try { pEntidad.codestadocivil = Convert.ToInt64(arrayline[i].ToString().Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                                    if (i == 12)
                                    { try { pEntidad.codescolaridad = Convert.ToInt64(arrayline[i].ToString().Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                                    if (i == 13)
                                    { try { pEntidad.codactividadStr = Convert.ToString(arrayline[i].ToString().Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                                    if (i == 14)
                                    { try { pEntidad.direccion = Convert.ToString(arrayline[i].ToString().Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                                    if (i == 15)
                                    { try { pEntidad.telefono = Convert.ToString(arrayline[i].ToString().Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                                    if (i == 16)
                                    {
                                        try
                                        {
                                            if (arrayline[i].ToString().Trim() != "" && arrayline[i].ToString().Trim() != null)
                                                pEntidad.codciudadresidencia = Convert.ToInt64(arrayline[i].ToString().Trim());
                                            else
                                                pEntidad.codciudadresidencia = null;
                                        }
                                        catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; }
                                    }
                                    if (i == 17)
                                    {
                                        try
                                        {
                                            if (arrayline[i].ToString().Trim() != "" && arrayline[i].ToString().Trim() != null)
                                                pEntidad.tipovivienda = arrayline[i].ToString().Trim();
                                            else
                                                pEntidad.tipovivienda = String.Empty;
                                        }
                                        catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; }
                                    }
                                    if (i == 18)
                                    { try { pEntidad.celular = Convert.ToString(arrayline[i].ToString().Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                                    if (i == 19)
                                    { try { pEntidad.email = Convert.ToString(arrayline[i].ToString().Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                                    if (i == 20)
                                    {
                                        try
                                        {
                                            if (arrayline[i].ToString().Trim() != "" && arrayline[i].ToString().Trim() != null)
                                                pEntidad.cod_oficina = Convert.ToInt64(arrayline[i].ToString().Trim());
                                            else
                                                pEntidad.cod_oficina = 0;
                                        }
                                        catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; }
                                    }
                                    if (i == 21)
                                    {
                                        try
                                        {
                                            if (arrayline[i].ToString().Trim() != "" && arrayline[i].ToString().Trim() != null)
                                                pEntidad.estado = arrayline[i].ToString().Trim();
                                            else
                                                pEntidad.estado = "A";
                                        }
                                        catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; }
                                    }
                                    if (i == 22)
                                    {
                                        try
                                        {
                                            pEntidad.dirCorrespondencia = Convert.ToString(arrayline[i].ToString().Trim());
                                        }
                                        catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; }
                                    }
                                    if (i == 23)
                                    { try { pEntidad.telCorrespondencia = Convert.ToString(arrayline[i].ToString().Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                                    if (i == 24)
                                    { try { pEntidad.ciuCorrespondencia = Convert.ToInt32(arrayline[i].ToString().Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                                    if (i == 25)
                                    { try { pEntidad.barrioCorrespondencia = Convert.ToInt32(arrayline[i].ToString().Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                                    if (i == 26)
                                    { try { pEntidad.numPersonasaCargo = Convert.ToInt32(arrayline[i].ToString().Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                                    if (i == 27)
                                    {
                                        try
                                        {
                                            if (arrayline[i].ToString().Trim() != "" && arrayline[i].ToString().Trim() != null)
                                                pEntidad.profecion = Convert.ToString(arrayline[i].ToString().Trim());
                                            else
                                                pEntidad.profecion = null;
                                        }
                                        catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; }
                                    }
                                    if (i == 28)
                                    { try { pEntidad.Estrato = Convert.ToInt32(arrayline[i].ToString().Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                                    if (i == 29)
                                    { try { pEntidad.ActividadEconomicaEmpresaStr = Convert.ToString(arrayline[i].ToString().Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                                    if (i == 30)
                                    { try { pEntidad.empleado_entidad = Convert.ToInt32(arrayline[i].ToString()); } catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                                    if (i == 31)
                                    { try { pEntidad.jornada_laboral = Convert.ToInt32(arrayline[i].ToString()); } catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                                    if (i == 32)
                                    { try { pEntidad.ocupacionApo = Convert.ToInt32(arrayline[i].ToString()); } catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }

                                    //posicion 17
                                    pEntidad.antiguedadlugar = 0;
                                    pEntidad.arrendador = null;
                                    pEntidad.telefonoarrendador = null;
                                    pEntidad.empresa = null;
                                    pEntidad.telefonoempresa = String.Empty;
                                    pEntidad.codcargo = 0;
                                    pEntidad.codtipocontrato = 0;
                                    pEntidad.cod_asesor = pUsuario.codusuario;
                                    pEntidad.residente = String.Empty;
                                    pEntidad.fecha_residencia = DateTime.MinValue;
                                    pEntidad.tratamiento = null;
                                    pEntidad.ValorArriendo = 0;
                                    pEntidad.direccionempresa = String.Empty;
                                    pEntidad.antiguedadlugarempresa = 0;
                                    pEntidad.barrioResidencia = 0;
                                    pEntidad.CelularEmpresa = null;
                                    pEntidad.ciudad = 0;
                                    pEntidad.relacionEmpleadosEmprender = 0;

                                    pEntidad.fechacreacion = pFechaCarga;
                                    pEntidad.usuariocreacion = pUsuario.nombre;
                                    pEntidad.nombre_funcionario = null;
                                    pEntidad.fecha_ingresoempresa = DateTime.MinValue;
                                    pEntidad.mujer_familia = -1;
                                    pEntidad.idescalafon = 0;

                                }

                                if (sinErrores)
                                {
                                    lstNatural.Add(pEntidad);
                                }

                                contadorreg++;
                            }
                            else //persona juridica
                            {
                                Xpinn.Contabilidad.Entities.Tercero pEntidadTer = new Contabilidad.Entities.Tercero();
                                for (int i = 0; i <= 15; i++)
                                {
                                    pEntidadTer.cod_persona = 0;
                                    pEntidadTer.tipo_persona = "J";
                                    if (i == 0)
                                    { try { pEntidadTer.identificacion = Convert.ToString(arrayline[i].ToString().Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                                    if (i == 1)
                                    { try { pEntidadTer.digito_verificacion = Convert.ToInt32(arrayline[i].ToString().Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                                    if (i == 2)
                                    { try { pEntidadTer.tipo_identificacion = Convert.ToInt64(arrayline[i].ToString().Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                                    if (i == 3)
                                    {
                                        try { pEntidadTer.fechaexpedicion = DateTime.ParseExact(arrayline[i].ToString().Trim(), sformato_fecha, null); }
                                        catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; }
                                    }
                                    if (i == 4)
                                    { try { pEntidadTer.codciudadexpedicion = Convert.ToInt64(arrayline[i].ToString()); } catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                                    if (i == 5)
                                    { try { pEntidadTer.razon_social = Convert.ToString(arrayline[i].ToString()); } catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                                    if (i == 6)
                                    { try { pEntidadTer.codactividadStr = Convert.ToString(arrayline[i].ToString().Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                                    if (i == 7)
                                    { try { pEntidadTer.direccion = Convert.ToString(arrayline[i].ToString().Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                                    if (i == 8)
                                    { try { pEntidadTer.telefono = Convert.ToString(arrayline[i].ToString().Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                                    if (i == 9)
                                    { try { pEntidadTer.email = Convert.ToString(arrayline[i].ToString().Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                                    if (i == 10)
                                    {
                                        try
                                        {
                                            if (arrayline[i].ToString().Trim() != "" && arrayline[i].ToString().Trim() != null)
                                                pEntidadTer.cod_oficina = Convert.ToInt64(arrayline[i].ToString().Trim());
                                            else
                                                pEntidadTer.cod_oficina = 0;
                                        }
                                        catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; }
                                    }
                                    if (i == 11)
                                    {
                                        try
                                        {
                                            if (arrayline[i].ToString().Trim() != "" && arrayline[i].ToString().Trim() != null)
                                                pEntidadTer.estado = arrayline[i].ToString().Trim();
                                            else
                                                pEntidadTer.estado = "A";
                                        }
                                        catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; }
                                    }
                                    if (i == 12)
                                    { try { pEntidadTer.regimen = Convert.ToString(arrayline[i].ToString().Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                                    if (i == 13)
                                    { try { pEntidadTer.tipo_acto_creacion = Convert.ToInt32(arrayline[i].ToString().Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                                    if (i == 14)
                                    { try { pEntidadTer.num_acto_creacion = Convert.ToString(arrayline[i].ToString().Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                                    if (i == 15)
                                    { try { pEntidadTer.celular = Convert.ToString(arrayline[i].ToString().Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }

                                }

                                if (sinErrores)
                                {
                                    lstJuridica.Add(pEntidadTer);
                                }

                                contadorreg++;
                            }
                        }
                        else
                        {
                            Xpinn.FabricaCreditos.Entities.CuentasBancarias pEntidadCta = new FabricaCreditos.Entities.CuentasBancarias();
                            for (int i = 0; i <= 7; i++)
                            {

                                if (i == 0)
                                { try { pEntidadCta.cod_persona = Convert.ToInt32(arrayline[i].ToString().Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                                if (i == 1)
                                { try { pEntidadCta.tipo_cuenta = Convert.ToInt32(arrayline[i].ToString().Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                                if (i == 2)
                                { try { pEntidadCta.numero_cuenta = Convert.ToString(arrayline[i].ToString().Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                                if (i == 3)
                                { try { pEntidadCta.cod_banco = Convert.ToInt32(arrayline[i].ToString()); } catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                                if (i == 4)
                                { try { pEntidadCta.sucursal = Convert.ToString(arrayline[i].ToString()); } catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                                if (i == 5)
                                {
                                    try { pEntidadCta.fecha_apertura = DateTime.ParseExact(arrayline[i].ToString().Trim(), sformato_fecha, null); }
                                    catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; }
                                }
                                if (i == 6)
                                { try { pEntidadCta.principal = Convert.ToInt32(arrayline[i].ToString().Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                            }

                            if (sinErrores)
                            {
                                if (pEntidadCta.principal == 1)
                                {
                                    if (pEntidadCta.cod_persona != 0)
                                    {
                                        List<CuentasBancarias> lstCuentas = DAActivadesPersona.ConsultarCuentasBancarias(pEntidadCta.cod_persona, string.Empty, pUsuario);
                                        bool resultCuenta = lstCuentas.Where(x => x.principal == 1).Any();
                                        if (resultCuenta)
                                            RegistrarError(contadorreg, "0", "La persona de código " + pEntidadCta.cod_persona + " ya tiene una cuenta bancaria principal", readLine.ToString(), ref plstErrores);
                                    }
                                }

                                lstCtaBancarias.Add(pEntidadCta);
                            }
                            contadorreg++;
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                perror = ex.Message;
            }
        }


        public void RegistrarError(int pNumeroLinea, string pRegistro, string pError, string pDato, ref List<ErroresCarga> plstErrores)
        {
            if (pNumeroLinea == -1)
            {
                plstErrores.Clear();
                return;
            }
            ErroresCarga registro = new ErroresCarga();
            registro.numero_registro = pNumeroLinea.ToString();
            registro.datos = pDato;
            registro.error = " Campo No.:" + pRegistro + " Error:" + pError;
            plstErrores.Add(registro);
        }


        public void CrearPersonaImportacion(DateTime pFechaCarga, ref string pError, string TipoCarga, string TipoPersona, List<Persona1> lstNatural, List<Xpinn.Contabilidad.Entities.Tercero> lstJuridica, List<Xpinn.FabricaCreditos.Entities.CuentasBancarias> lstCtaBancarias, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (TipoCarga == "P")
                    {
                        if (TipoPersona == "N")
                        {
                            //GRABACION DE PERSONAS NATURALES
                            if (lstNatural != null && lstNatural.Count > 0)
                            {
                                foreach (Persona1 nPersona in lstNatural)
                                {
                                    nPersona.cod_persona = 0;
                                    nPersona.tipo_persona = "N";
                                    nPersona.digito_verificacion = 0;
                                    nPersona.antiguedadlugar = 0;
                                    nPersona.arrendador = null;
                                    nPersona.telefonoarrendador = null;
                                    nPersona.empresa = null;
                                    nPersona.telefonoempresa = String.Empty;
                                    nPersona.codcargo = 0;
                                    nPersona.codtipocontrato = 0;
                                    nPersona.cod_asesor = pUsuario.codusuario;
                                    nPersona.residente = String.Empty;
                                    nPersona.fecha_residencia = DateTime.MinValue;
                                    nPersona.tratamiento = null;
                                    nPersona.ValorArriendo = 0;
                                    nPersona.direccionempresa = String.Empty;
                                    nPersona.antiguedadlugarempresa = 0;
                                    nPersona.barrioResidencia = 0;
                                    nPersona.CelularEmpresa = null;
                                    nPersona.ciudad = 0;
                                    nPersona.relacionEmpleadosEmprender = 0;
                                    nPersona.cuota = null;
                                    nPersona.fechacreacion = pFechaCarga;
                                    nPersona.usuariocreacion = pUsuario.nombre;
                                    nPersona.nombre_funcionario = null;
                                    nPersona.fecha_ingresoempresa = DateTime.MinValue;
                                    nPersona.mujer_familia = -1;
                                    nPersona.idescalafon = 0;
                                    nPersona.salario = 0;
                                    nPersona.cod_nomina = null;
                                    nPersona.identificacion = null;
                                    Persona1 pEntidad = new Persona1();
                                    pEntidad = DAPersona1.CrearPersonaAporte(pEntidad, pUsuario, 1);
                                }
                            }
                        }
                        else
                        {
                            //GRABACION DE PERSONAS JURIDICAS
                            if (lstJuridica != null && lstJuridica.Count > 0)
                            {
                                Xpinn.Contabilidad.Data.TerceroData DATercero = new Contabilidad.Data.TerceroData();
                                foreach (Xpinn.Contabilidad.Entities.Tercero nTercero in lstJuridica)
                                {
                                    nTercero.cod_persona = 0;
                                    nTercero.tipo_persona = "J";
                                    nTercero.primer_apellido = nTercero.razon_social;
                                    nTercero.fechacreacion = pFechaCarga;
                                    nTercero.usuariocreacion = pUsuario.nombre;
                                    nTercero.fechacreacion = DateTime.Now;
                                    nTercero.usuultmod = pUsuario.nombre;
                                    Xpinn.Contabilidad.Entities.Tercero pEntidad = new Xpinn.Contabilidad.Entities.Tercero();
                                    pEntidad = DATercero.CrearTercero(nTercero, pUsuario);
                                }
                            }
                        }
                    }
                    else
                    {
                        //GRABACION DE CUENTAS BANCARIAS
                        if (lstCtaBancarias != null && lstCtaBancarias.Count > 0)
                        {
                            Xpinn.FabricaCreditos.Data.ActividadesData DAActividades = new Xpinn.FabricaCreditos.Data.ActividadesData();
                            foreach (Xpinn.FabricaCreditos.Entities.CuentasBancarias nCtaBancaria in lstCtaBancarias)
                            {
                                Xpinn.FabricaCreditos.Entities.CuentasBancarias pEntidad = new Xpinn.FabricaCreditos.Entities.CuentasBancarias();
                                pEntidad = DAActividades.CrearPer_CuentaFinac(nCtaBancaria, pUsuario);
                            }
                        }
                    }
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                pError = ex.Message;
            }
        }

        public List<Persona1> ModificarPersonasAporte(List<Persona1> lstPersona, Usuario pUsuario)
        {
            try
            {

                foreach (Persona1 Persona1 in lstPersona)
                {
                    Persona1 persona = new Persona1();
                    using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                    {
                        persona = DAPersona1.ModificarPersonasAporte(Persona1, pUsuario);
                        ts.Complete();
                    }
                }
                return lstPersona;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Business", "ModificarPersonas", ex);
                return null;
            }
        }

        public List<Persona1> ListarPersonasAporte(string pcod_ini, string pcod_fin, string pcod_empresa, string pcod_asesor, string pcod_zona, Usuario pUsuario)
        {
            try
            {
                return DAPersona1.ListarPersonasAporte(pcod_ini, pcod_fin, pcod_empresa, pcod_asesor, pcod_zona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Business", "ListarPersonasAporte", ex);
                return null;
            }
        }

        public void CargarPersonasDatos(DateTime pFechaCarga, string sformato_fecha, Stream pstream, ref string perror, ref List<Persona1> lstPersonas, ref List<ErroresCarga> plstErrores, Usuario pUsuario)
        {
            Configuracion conf = new Configuracion();
            string sSeparadorDecimal = conf.ObtenerSeparadorDecimalConfig();

            string readLine;
            bool sinErrores = true;
            int contadorreg = 1;

            // Inicializar control de errores
            RegistrarError(-1, "", "", "", ref plstErrores);

            try
            {
                using (strReader = new StreamReader(pstream))
                {
                    //recorriendo las filas del archivo
                    while (strReader.Peek() > 0)
                    {
                        //BAJANDO LA FILA A UNA VARIABLE
                        readLine = strReader.ReadLine();
                        char Separador = '|';

                        //Separando la data a un array
                        string[] arrayline = readLine.Split(Separador);

                        Persona1 pEntidad = new Persona1();
                        for (int i = 0; i <= 9; i++)
                        {
                            if (i == 0)
                            { if (!string.IsNullOrEmpty((arrayline[i].ToString().Trim())))
                                try { pEntidad.cod_persona = Convert.ToInt64(arrayline[i].ToString().Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                            if (i == 1)
                            { if (!string.IsNullOrEmpty((arrayline[i].ToString().Trim())))
                                    try { pEntidad.identificacion = Convert.ToString(arrayline[i].ToString().Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                            if (i == 2)
                            { if (!string.IsNullOrEmpty((arrayline[i].ToString().Trim()))) 
                                    try { pEntidad.nombre = Convert.ToString(arrayline[i].ToString().Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                            if (i == 3)
                            { if (!string.IsNullOrEmpty((arrayline[i].ToString().Trim()))) 
                                    try { pEntidad.cod_nomina_empleado = Convert.ToString(arrayline[i].ToString()); } catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                            if (i == 4)
                            { if (!string.IsNullOrEmpty((arrayline[i].ToString().Trim()))) 
                                    try { pEntidad.cod_asesor = Convert.ToInt64(arrayline[i].ToString()); } catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                            if (i == 5)
                            { if (!string.IsNullOrEmpty((arrayline[i].ToString().Trim()))) 
                                    try { pEntidad.nombres = Convert.ToString(arrayline[i].ToString().Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                            if (i == 6)
                            { if (!string.IsNullOrEmpty((arrayline[i].ToString().Trim()))) 
                                    try { pEntidad.zona = Convert.ToInt64(arrayline[i].ToString().Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                            if (i == 7)
                            { if (!string.IsNullOrEmpty((arrayline[i].ToString().Trim()))) 
                                    try { pEntidad.nom_zona = Convert.ToString(arrayline[i].ToString().Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                            if (i == 8)
                            { if (!string.IsNullOrEmpty((arrayline[i].ToString().Trim()))) 
                                    try { pEntidad.salario = Convert.ToInt64(arrayline[i].ToString().Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                            if (i == 9)
                            { if (!string.IsNullOrEmpty((arrayline[i].ToString().Trim()))) 
                                    try { pEntidad.cuota = Convert.ToInt64(arrayline[i].ToString().Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }

                            //posicion 17
                            pEntidad.antiguedadlugar = 0;
                            pEntidad.arrendador = null;
                            pEntidad.telefonoarrendador = null;
                            pEntidad.empresa = null;
                            pEntidad.telefonoempresa = String.Empty;
                            pEntidad.codcargo = 0;
                            pEntidad.codtipocontrato = 0;
                            pEntidad.residente = String.Empty;
                            pEntidad.fecha_residencia = DateTime.MinValue;
                            pEntidad.tratamiento = null;
                            pEntidad.ValorArriendo = 0;
                            pEntidad.direccionempresa = String.Empty;
                            pEntidad.antiguedadlugarempresa = 0;
                            pEntidad.barrioResidencia = 0;
                            pEntidad.CelularEmpresa = null;
                            pEntidad.ciudad = 0;
                            pEntidad.relacionEmpleadosEmprender = 0;
                            pEntidad.fechacreacion = pFechaCarga;
                            pEntidad.usuariocreacion = pUsuario.nombre;
                            pEntidad.nombre_funcionario = null;
                            pEntidad.fecha_ingresoempresa = DateTime.MinValue;
                            pEntidad.mujer_familia = -1;
                            pEntidad.idescalafon = 0;

                        }

                        if (sinErrores)
                        {
                            lstPersonas.Add(pEntidad);
                        }

                        contadorreg++;
                    }
                }
            }
            catch (IOException ex)
            {
                perror = ex.Message;
            }
        }

        public Persona1 ValidarPersona(string pIdentificacion, int pTipo, Usuario pUsuario)
        {
            try
            {
                Persona1 pPersona = new Persona1();
                pPersona = DAPersona1.ValidarPersona(pIdentificacion, pTipo, pUsuario);
                if (pPersona != null)
                {
                    Int64 idImagen = 0;
                    pPersona.foto = DAImagenes.ConsultarImagenPersona(Convert.ToInt64(pPersona.cod_persona), 1, ref idImagen, pUsuario);
                    pPersona.idimagen = idImagen;
                }
                return pPersona;
            }
            catch
            {
                return null;
            }
        }

        public Int64? GrabarControl(string pIdentificacion, int pTipo, Usuario pUsuario)
        {
            Int64? idcontrol = null;
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    idcontrol = DAPersona1.GrabarControl(pIdentificacion, pTipo, pUsuario);

                    ts.Complete();
                }

                return idcontrol;
            }
            catch
            {
                return null;
            }
        }
        public Persona1 FechaNacimiento(Int64 CodCliente, Usuario pUsuario)
        {

            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {
                return DAPersona1.FechaNacimiento(CodCliente, pUsuario);
            }

        }
        public int NotificacionidMax(Usuario pUsuario)
        {

            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {
                return DAPersona1.NotificacionidMax(pUsuario);
            }

        }
        public Persona1 Notificacion(Persona1 pPersona, Usuario pUsuario, int opcion)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pPersona = DAPersona1.Notificacion(pPersona, pUsuario, opcion);

                    ts.Complete();
                }

                return pPersona;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Business", "Notificacion", ex);
                return null;
            }
        }
        public List<Persona1> ProxVencer(Usuario pUsuario)
        {
            try
            {
                return DAPersona1.ProxVencer(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Business", "ProxVencer", ex);
                return null;
            }
        }
        public Persona1 TabPersonal(Persona1 pPersona, int opcion, Usuario pUsuario)
        {
            // Grabar los datos de la persona
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    Int64 cod;
                    DAPersona1.TabPersonal(pPersona, opcion, pUsuario);
                    cod = pPersona.cod_persona;                    
                    if (pPersona.lstActEconomicasSecundarias != null)
                    {
                        ActividadesData DAActi = new ActividadesData();
                        foreach (Actividades objActividadEconomica in pPersona.lstActEconomicasSecundarias)
                        {
                            DAActi.CrearActividadEconomicaSecundaria(cod, objActividadEconomica.codactividad, pUsuario);
                        }
                    }
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Business", "TabPersonal", ex);
                return null;
            }

            // Grabar la foto de la persona
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (pPersona.foto != null)
                    {
                        Xpinn.FabricaCreditos.Entities.Imagenes pImagenes = new Xpinn.FabricaCreditos.Entities.Imagenes();
                        pImagenes.idimagen = 0;
                        pImagenes.cod_persona = pPersona.cod_persona;
                        pImagenes.tipo_imagen = 1;
                        pImagenes.tipo_documento = Convert.ToInt32(pPersona.tipo_identificacion);
                        pImagenes.imagen = pPersona.foto;
                        pImagenes.fecha = System.DateTime.Now;
                        DAImagenes.CrearImagenesPersona(pImagenes, pUsuario);
                    }
                    ts.Complete();
                }                
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Business", "TabPersonal", ex);
                return null;
            }

            return pPersona;

        }


        public Persona1 TabLaboral(Persona1 pPersona, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    Int64 cod;
                    DAPersona1.TabLaboral(pPersona, pUsuario);
                    cod = pPersona.cod_persona;
                    if (pPersona.lstEmpresaRecaudo != null)
                    {
                        PersonaEmpresaRecaudoData DAEmpresa = new PersonaEmpresaRecaudoData();
                        int num = 0;                       

                        foreach (PersonaEmpresaRecaudo eEmpresa in pPersona.lstEmpresaRecaudo)
                        {
                            PersonaEmpresaRecaudo nEmpresa = new PersonaEmpresaRecaudo();
                            eEmpresa.cod_persona = cod;

                            if (eEmpresa.seleccionar)
                            {
                                if (eEmpresa.idempresarecaudo != null)
                                    nEmpresa = DAEmpresa.ModificarPersonaEmpresaRecaudo(eEmpresa, pUsuario);
                                else
                                    nEmpresa = DAEmpresa.CrearPersonaEmpresaRecaudo(eEmpresa, pUsuario);
                            }
                            else
                            {
                                if (eEmpresa.idempresarecaudo != null)
                                    DAEmpresa.EliminarPersonaEmpresaRecaudo(Convert.ToInt64(eEmpresa.idempresarecaudo), pUsuario);
                            }
                            num += 1;
                        }
                    }
                    ts.Complete();
                }
                return pPersona;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Business", "TabLaboral", ex);
                return null;
            }
        }
        public Persona1 TabEconomica(Persona1 pPersona, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    Int64 cod;
                    cod = pPersona.cod_persona;


                    ActividadesData DACuenta = new ActividadesData();
                    if (pPersona.lstCuentasBan != null && pPersona.origen != "Afiliacion-Conyuge")
                    {
                        int num = 0;
                        foreach (CuentasBancarias eCuenta in pPersona.lstCuentasBan)
                        {
                            eCuenta.cod_persona = cod;
                            CuentasBancarias nCuentaBanc = new CuentasBancarias();
                            if (eCuenta.idcuentabancaria <= 0 || eCuenta.fecha_apertura == DateTime.MinValue || eCuenta.numero_cuenta == "")
                            {
                                nCuentaBanc = DACuenta.CrearPer_CuentaFinac(eCuenta, pUsuario);
                                pPersona.lstCuentasBan[num].idcuentabancaria = nCuentaBanc.idcuentabancaria;
                            }
                            else
                                nCuentaBanc = DACuenta.ModificarPer_CuentaFinac(eCuenta, pUsuario);
                            num += 1;
                        }
                    }

                    //Moneda extranjera
                    if (pPersona.lstMonedaExt != null)
                    {
                        int num = 0;
                        EstadosFinancierosData DAEstadosFinancieros = new EstadosFinancierosData();
                        foreach (EstadosFinancieros eMoneda in pPersona.lstMonedaExt)
                        {
                            EstadosFinancieros pMoneda = new EstadosFinancieros();
                            eMoneda.cod_persona = pPersona.cod_persona;
                            if (eMoneda.cod_moneda_ext != null && eMoneda.cod_moneda_ext != 0)
                                pMoneda = DAEstadosFinancieros.ModificarMonedaExtranjera(eMoneda, pUsuario);
                            else if (eMoneda.cod_moneda_ext == null || eMoneda.cod_moneda_ext == 0)
                            {
                                pMoneda = DAEstadosFinancieros.CrearMonedaExtranjera(eMoneda, pUsuario);
                                pPersona.lstMonedaExt[num].cod_moneda_ext = pMoneda.cod_moneda_ext;
                            }
                            num += 1;
                        }
                    }

                    //Productos extranjeros
                    if (pPersona.lstProductosFinExt != null)
                    {
                        int num = 0;
                        EstadosFinancierosData DAEstadosFinancieros = new EstadosFinancierosData();
                        foreach (EstadosFinancieros eMoneda in pPersona.lstProductosFinExt)
                        {
                            EstadosFinancieros pMoneda = new EstadosFinancieros();
                            eMoneda.cod_persona = pPersona.cod_persona;
                            if (eMoneda.cod_moneda_ext != null && eMoneda.cod_moneda_ext != 0)
                                pMoneda = DAEstadosFinancieros.ModificarMonedaExtranjera(eMoneda, pUsuario);
                            else if (eMoneda.cod_moneda_ext == null || eMoneda.cod_moneda_ext == 0)
                            {
                                pMoneda = DAEstadosFinancieros.CrearMonedaExtranjera(eMoneda, pUsuario);
                                pPersona.lstProductosFinExt[num].cod_moneda_ext = pMoneda.cod_moneda_ext;
                            }
                            num += 1;
                        }
                    }

                    ts.Complete();
                }
                return pPersona;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Business", "TabEconomica", ex);
                return null;
            }
        }
        public Persona1 TabAdicional(Persona1 pPersona, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    Int64 cod;
                    cod = pPersona.cod_persona;

                    //Actividades
                    if (pPersona.lstActividad != null && pPersona.origen != "Afiliacion-Conyuge")
                    {
                        ActividadesData DAActi = new ActividadesData();
                        int num = 0;
                        foreach (Actividades eActi in pPersona.lstActividad)
                        {
                            eActi.cod_persona = cod;
                            Actividades nActividad = new Actividades();
                            if (eActi.idactividad <= 0 || eActi.fecha_realizacion == DateTime.MinValue)
                            {
                                nActividad = DAActi.CrearActividadesPersona(eActi, pUsuario);
                                pPersona.lstActividad[num].idactividad = nActividad.idactividad;
                            }
                            else
                                nActividad = DAActi.ModificarActividadesPersona(eActi, pUsuario);
                            num += 1;
                        }
                    }

                    //Información Adicional
                    if (pPersona.lstInformacion != null && pPersona.lstInformacion.Count > 0)
                    {
                        InformacionAdicionalData DAInfor = new InformacionAdicionalData();
                        foreach (InformacionAdicional eInf in pPersona.lstInformacion)
                        {
                            InformacionAdicional nInforma = new InformacionAdicional();
                            eInf.cod_persona = cod;
                            if (eInf.idinfadicional != 0)
                                nInforma = DAInfor.ModificarPersona_InfoAdicional(eInf, pUsuario);
                            else
                                nInforma = DAInfor.CrearPersona_InfoAdicional(eInf, pUsuario);
                        }
                    }

                    ts.Complete();
                }
                return pPersona;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Business", "TabAdicional", ex);
                return null;
            }
        }

        public int CrearSolicitudRetiro(Persona1 pPersona, Usuario pUsuario)
        {
            try
            {
                return DAPersona1.CrearSolicitudRetiro(pPersona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "CrearSolicitudRetiro", ex);
                return 0;
            }
        }

        public decimal ConsultarNivelEndeudamiento(string identificacion, Usuario pUsuario)
        {
            return DAPersona1.ConsultarNivelEndeudamiento(identificacion, pUsuario);
        }

        public Int64? ConsultarCodigoPersona(string pIdentificacion, Usuario pUsuario)
        {
            return DAPersona1.ConsultarCodigoPersona(pIdentificacion, pUsuario);
        }


    }
}