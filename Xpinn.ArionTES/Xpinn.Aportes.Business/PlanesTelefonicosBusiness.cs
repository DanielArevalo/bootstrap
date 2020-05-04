using System;
using System.Collections.Generic;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Aportes.Entities;
using Xpinn.Aportes.Data;
using System.IO;


namespace Xpinn.Aportes.Business
{
    public class PlanesTelefonicosBusiness : GlobalBusiness
    {
        private PlanesTelefonicosData DAPlanesTelefonicos;
        

        public PlanesTelefonicosBusiness()
        {
            DAPlanesTelefonicos = new PlanesTelefonicosData();
        }

        //Crear Plan Telefonico
        public PlanTelefonico CrearPlanTelefonico(PlanTelefonico pPlanTel, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pPlanTel = DAPlanesTelefonicos.CrearPlanTelefonico(pPlanTel, pusuario);
                    ts.Complete();
                }

                return pPlanTel;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanesTelefonicosBusiness", "CrearPlanTelefonico", ex);
                return null;
            }
        }

        //Modificar Plan Telefonico
        public PlanTelefonico ModificarPlanTelefonico(PlanTelefonico pPlanTel, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pPlanTel = DAPlanesTelefonicos.ModificarPlanTelefonico(pPlanTel, pusuario);

                    ts.Complete();

                }
                return pPlanTel;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanesTelefonicosBusiness", "ModificarPlanTelefonico", ex);
                return null;
            }
        }

        //Eliminar Plan Telefonico
        public void EliminarPlanTelefonico(Int64 pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAPlanesTelefonicos.EliminarPlanTelefonico(pId, pusuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanesTelefonicosBusiness", "EliminarPlanTelefonico", ex);
            }
        }

        //Consultar plan Telefonico
        public PlanTelefonico ConsultarPlanTelefonico(Int64 pId, Usuario pusuario)
        {
            try
            {
                PlanTelefonico PlanTelefonico = new PlanTelefonico();
                PlanTelefonico = DAPlanesTelefonicos.ConsultarPlanTelefonico(pId, pusuario);
                return PlanTelefonico;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanesTelefonicosBusiness", "ConsultarPlanTelefonico", ex);
                return null;
            }
        }

        //Consultar Todos lo planes 
        public List<PlanTelefonico> ListarPlanTelefonico(PlanTelefonico pPlanTel, Usuario pusuario)
        {
            try
            {
                return DAPlanesTelefonicos.ListarPlanTelefonico(pPlanTel, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanesTelefonicosBusiness", "ListarPlanTelefonico", ex);
                return null;
            }
        }

        //Listar Proveedores
        public List<PlanTelefonico> ListarProveedores(Usuario pusuario)
        {
            try
            {
                return DAPlanesTelefonicos.ListarProveedores(pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanesTelefonicosBusiness", "ListarProveedores", ex);
                return null;
            }
        }

        //Crear Linea Telefonica
        public PlanTelefonico CrearLineaTelefonica(ref Int64 vCod_Ope, Xpinn.Tesoreria.Entities.Operacion pOperacion, PlanTelefonico pPlanTel, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {

                    //OPERACION
                    Xpinn.Tesoreria.Data.OperacionData DAOperacion = new Tesoreria.Data.OperacionData();
                    pOperacion = DAOperacion.GrabarOperacion(pOperacion, pusuario);
                    vCod_Ope = pOperacion.cod_ope;

                    if (vCod_Ope != 0)
                    {
                        pPlanTel = DAPlanesTelefonicos.CrearLineaTelefonica(vCod_Ope, pPlanTel, pusuario);
                        ts.Complete();
                    }
                    else
                    {
                        return null;
                    }
                }

                return pPlanTel;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanesTelefonicosBusiness", "CrearLineaTelefonica", ex);
                return null;
            }
        }

        //Modificar Linea Telefonica
        public PlanTelefonico ModificarLineaTelefonica(PlanTelefonico pPlanTel, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pPlanTel = DAPlanesTelefonicos.ModificarLineaTelefonica(pPlanTel, pusuario);

                    ts.Complete();

                }
                return pPlanTel;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanesTelefonicosBusiness", "ModificarLineaTelefonica", ex);
                return null;
            }
        }

        //Listar Lineas
        public List<PlanTelefonico> ListarLineasTelefonicas(PlanTelefonico plineafiltro, Usuario pusuario)
        {
            try
            {
                return DAPlanesTelefonicos.ListarLineasTelefonicas(plineafiltro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanesTelefonicosBusiness", "ListarLineasTelefonicas", ex);
                return null;
            }
        }

        //Consultar linea telefonica
        public PlanTelefonico ConsultarLineaTelefonica(string pId, Usuario pusuario)
        {
            try
            {
                PlanTelefonico PlanTelefonico = new PlanTelefonico();
                PlanTelefonico = DAPlanesTelefonicos.ConsultarLineaTelefonica(pId, pusuario);
                return PlanTelefonico;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanesTelefonicosBusiness", "ConsultarPlanTelefonico", ex);
                return null;
            }
        }

        //Consultar si existen líneas telefonicas registradas
        public bool ConsultarLineasExistentes(Usuario vUsuario)
        {
            try
            {
                bool resultado = DAPlanesTelefonicos.ConsultarLineasExistentes(vUsuario);
                return resultado;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanesTelefonicosBusiness", "ConsultarLineasExistentes", ex);
                return false;
            }
        }
        //Cargue Masivo        
        private StreamReader strReader;
        public void CargaAdicionales(ref string pError, Stream pstream, ref List<PlanTelefonico> lstAdicionales, ref List<ErroresCargaAportes> plstErrores, Usuario pUsuario)
        {
            Configuracion conf = new Configuracion();
            string sSeparadorDecimal = conf.ObtenerSeparadorDecimalConfig();

            string readLine;

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
                        string Separador = "|";

                        //Separando la data a un array
                        string[] arrayline = readLine.Split(Convert.ToChar(Separador));
                        int contadorreg = 0;

                        PlanTelefonico pEntidad = new PlanTelefonico();
                        for (int i = 0; i <= 3; i++)
                        {
                            if (i == 0) { try { pEntidad.identificacion_titular = Convert.ToString(arrayline[i].ToString().Trim()); } catch (Exception ex) { RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                            if (i == 1) { try { pEntidad.num_linea_telefonica = Convert.ToString(arrayline[i].ToString().Trim()); } catch (Exception ex) { RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                            if (i == 2) { try { pEntidad.valor_cuota = Convert.ToDecimal(arrayline[i].ToString().Trim()); } catch (Exception ex) { RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                            if (i == 3) { try { pEntidad.fecha_activacion = Convert.ToDateTime(arrayline[i].ToString().Trim()); } catch (Exception ex) { RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                            contadorreg++;
                        }

                        PlanTelefonico PlanTelefonico = new PlanTelefonico();
                        PlanTelefonico = DAPlanesTelefonicos.ConsultarLineaTelefonica(pEntidad.num_linea_telefonica, pUsuario);
                        int cont = 0;

                        //Verificar la existencia de la línea y que la identificación sea igual a la registrada
                        if (PlanTelefonico.num_linea_telefonica == null || PlanTelefonico.num_linea_telefonica == "")
                        {
                            cont++;
                            RegistrarError(cont, "2", "La línea telefónica Nro. "+pEntidad.num_linea_telefonica+" no se encuentra registrada", readLine.ToString(), ref plstErrores);
                        }
                        else if (PlanTelefonico.identificacion_titular != pEntidad.identificacion_titular)
                        {
                            cont++;
                            RegistrarError(cont, "1", "La identificación del titular de la línea telefónica Nro. " + pEntidad.num_linea_telefonica + " no coincide con la registrada", readLine.ToString(), ref plstErrores);
                        }
                        else
                            lstAdicionales.Add(pEntidad);
                    }
                }
            }
            catch (IOException ex)
            {
                pError = ex.Message;
            }
        }

        public void RegistrarError(int pNumeroLinea, string pRegistro, string pError, string pDato, ref List<ErroresCargaAportes> plstErrores)
        {
            if (pNumeroLinea == -1)
            {
                plstErrores.Clear();
                return;
            }
            ErroresCargaAportes registro = new ErroresCargaAportes();
            registro.numero_registro = pNumeroLinea.ToString();
            registro.datos = pDato;
            registro.error = " Campo No.:" + pRegistro + " Error:" + pError;
            plstErrores.Add(registro);
        }

        public void CrearImportacion(ref string pError, List<PlanTelefonico> lstAdicionales, ref Xpinn.Tesoreria.Entities.Operacion vOpe, Usuario pUsuario, ref List<Int64> lst_Num_Lin)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {

                    bool a = false;

                    if (lstAdicionales != null && lstAdicionales.Count > 0)
                    {
                        a = DAPlanesTelefonicos.CuotasActAdici(pUsuario);
                    }

                    //OPERACION
                    Int64 vCod_Ope = 0;
                    Xpinn.Tesoreria.Data.OperacionData DAOperacion = new Tesoreria.Data.OperacionData();
                    vOpe = DAOperacion.GrabarOperacion(vOpe, pUsuario);
                    vCod_Ope = vOpe.cod_ope;

                    if (a == true && vCod_Ope != 0)
                    {
                        if (lstAdicionales != null && lstAdicionales.Count > 0)
                        {
                            foreach (PlanTelefonico nAdicional in lstAdicionales)
                            {

                                PlanTelefonico pEntidad = new PlanTelefonico();
                                nAdicional.fecha_activacion = vOpe.fecha_calc;
                                pEntidad = DAPlanesTelefonicos.CrearAdicional(nAdicional, vCod_Ope, pUsuario);
                                if (pEntidad.num_linea_telefonicaR != "")
                                {
                                    lst_Num_Lin.Add(Convert.ToInt64(pEntidad.num_linea_telefonica));
                                }
                            }

                        }
                        ts.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                pError = ex.Message;
            }
        }

        //Estado de cuenta
        public List<PlanTelefonico> ListarLineas(PlanTelefonico pLinea, DateTime pFecha, Usuario vUsuario, string filtro)
        {
            try
            {
                return DAPlanesTelefonicos.ListarLineas(pLinea, pFecha, vUsuario, filtro);
            }
            catch(Exception ex)
            {
                BOExcepcion.Throw("PlanesTelefonicosBusiness", "ListarLineas", ex);
                return null;
            }
        }
        public List<PlanTelefonico> ListarLineasAtencionWeb(Usuario vUsuario, string filtro)
        {
            try
            {
                return DAPlanesTelefonicos.ListarLineasAtencionWeb(vUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanesTelefonicosBusiness", "ListarLineasAtencionWeb", ex);
                return null;
            }
        }
        //Masivo Lineas telefonicas
        public void CargarLineasMasivo(ref string pError, Stream pstream, ref List<PlanTelefonico> lstLineas, ref List<ErroresCargaAportes> plstErrores, Usuario pUsuario)
        {
            Configuracion conf = new Configuracion();
            string sSeparadorDecimal = conf.ObtenerSeparadorDecimalConfig();

            string readLine;

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
                        string Separador = "|";

                        //Separando la data a un array
                        string[] arrayline = readLine.Split(Convert.ToChar(Separador));
                        int contadorreg = 0;
                        int cont = 0;

                        PlanTelefonico pEntidad = new PlanTelefonico();
                        //Lineas 352 y 359 modificadas para permitir la carga de los demás datos si el valor en el arreglo ese nulo y no grabar error ya que esta es opcional
                        for (int i = 0; i <= 11; i++)
                        {
                            if (i == 0) { try { pEntidad.num_linea_telefonica = Convert.ToString(arrayline[i].ToString().Trim()); } catch (Exception ex) { RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                            if (i == 1) { try { pEntidad.identificacion_titular = Convert.ToString(arrayline[i].ToString().Trim()); } catch (Exception ex) { RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                            if (i == 2) { try { pEntidad.fecha_activacion = Convert.ToDateTime(arrayline[i].ToString().Trim()); } catch (Exception ex) { RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                            if (i == 3) { try { pEntidad.fecha_vencimiento = Convert.ToDateTime(arrayline[i].ToString().Trim()); } catch (Exception ex) { if (String.IsNullOrWhiteSpace(arrayline[i].ToString())) pEntidad.fecha_vencimiento = null; else RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); } }
                            if (i == 4) { try { pEntidad.cod_plan = Convert.ToInt32(arrayline[i].ToString().Trim()); } catch (Exception ex) { RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                            if (i == 5) { try { pEntidad.cod_linea_servicio = Convert.ToInt64(arrayline[i].ToString().Trim()); } catch (Exception ex) { RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                            if (i == 6) { try { pEntidad.fecha_primera_cuota = Convert.ToDateTime(arrayline[i].ToString().Trim()); } catch (Exception ex) { RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                            if (i == 7) { try { pEntidad.valor_cuota = Convert.ToDecimal(arrayline[i].ToString().Trim()); } catch (Exception ex) { RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                            if (i == 8) { try { pEntidad.cod_periodicidad = Convert.ToInt64(arrayline[i].ToString().Trim()); } catch (Exception ex) { RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                            if (i == 9) { try { pEntidad.forma_pago = Convert.ToString(arrayline[i].ToString().Trim()); } catch (Exception ex) { RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                            if (i == 10) { try { pEntidad.cod_empresa = Convert.ToInt64(arrayline[i].ToString().Trim()); } catch (Exception ex) { if (String.IsNullOrWhiteSpace(arrayline[i].ToString())) pEntidad.cod_empresa = null; else RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); } }

                            contadorreg++;
                        }

                        //Validar que la línea telefoncia no se encuentre registrada 
                        PlanTelefonico PlanTelefonico = new PlanTelefonico();
                        PlanTelefonico = DAPlanesTelefonicos.ConsultarLineaTelefonica(pEntidad.num_linea_telefonica, pUsuario);
                    
                        //Validar que la persona no se encuentre en vacaciones
                        Xpinn.Tesoreria.Data.EmpresaNovedadData DAEmpresaRecaudo = new Tesoreria.Data.EmpresaNovedadData();
                        Xpinn.Tesoreria.Entities.EmpresaNovedad pPersonaVac = new Tesoreria.Entities.EmpresaNovedad();
                        Xpinn.FabricaCreditos.Data.Persona1Data DAPersona1 = new FabricaCreditos.Data.Persona1Data();
                        Int64 cod_persona = DAPersona1.ConsultarCodigopersona(pEntidad.identificacion_titular, pUsuario);
                        string pFiltro = " where vac.cod_persona = " + cod_persona + " order by vac.fecha_novedad desc";
                        pPersonaVac = DAEmpresaRecaudo.ConsultarPersonaVacaciones(pFiltro, pUsuario);

                        //Validar que la persona se encuentre en estado activo
                        Xpinn.Servicios.Data.SolicitudServiciosData SolicServicios = new Xpinn.Servicios.Data.SolicitudServiciosData();
                        bool result = true;
                        result = SolicServicios.ConsultarEstadoPersona(cod_persona, pEntidad.identificacion_titular, "A", pUsuario);

                        if (PlanTelefonico.num_linea_telefonica != "" && PlanTelefonico.num_linea_telefonica != null)
                        {
                            cont++;
                            RegistrarError(cont, "1", "El número de línea telefonica ya se encuentra registrado", readLine.ToString(), ref plstErrores);
                        }/*else if (pPersonaVac != null)
                        {
                            if (pPersonaVac.cod_persona > 0)
                            {
                                if (pPersonaVac.fechacreacion != null && pPersonaVac.fecha_inicial != null && pPersonaVac.fecha_final != null)
                                {
                                    DateTime pFechaActual = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                                    if (pPersonaVac.fechacreacion <= pFechaActual && pPersonaVac.fecha_final >= pFechaActual)
                                    {
                                        RegistrarError(cont, "1", "La persona con No. Id: " + pEntidad.identificacion_titular + " tiene un periodo de vacaciones del [ " + Convert.ToDateTime(pPersonaVac.fecha_inicial).ToString("dd/MM/yyyy") + " al " + Convert.ToDateTime(pPersonaVac.fecha_final).ToString("dd/MM/yyyy") + " ]", readLine.ToString(), ref plstErrores);
                                    }
                                    else if (result == false)
                                    {
                                        RegistrarError(cont, "1", "La persona con No. Id: " + pEntidad.identificacion_titular + " no se encuentra en estado activo", readLine.ToString(), ref plstErrores);
                                    }
                                    else
                                        lstLineas.Add(pEntidad);
                                }
                            }
                        }*/
                        else if (result == false)
                        {
                            RegistrarError(cont, "1", "La persona con No. Id: " + pEntidad.identificacion_titular + " no se encuentra en estado activo", readLine.ToString(), ref plstErrores);
                        }
                        else
                            lstLineas.Add(pEntidad);


                    }
                }
            }
            catch (IOException ex)
            {
                pError = ex.Message;
            }
        }

        //Guardar masivo Lineas
        public void CrearMasivoLineas(List<PlanTelefonico> lstLineas, ref string pError, ref List<PlanTelefonico> lst_num_lin, Xpinn.Tesoreria.Entities.Operacion vOpe, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (lstLineas != null && lstLineas.Count > 0)
                    {
                        Int64 vCod_Ope = 0;
                        Xpinn.Tesoreria.Data.OperacionData DAOperacion = new Tesoreria.Data.OperacionData();
                        vOpe = DAOperacion.GrabarOperacion(vOpe, pUsuario);
                        vCod_Ope = vOpe.cod_ope;

                        if (vCod_Ope != 0)
                        {
                            foreach (PlanTelefonico itemlin in lstLineas)
                            {
                                /*Insertamos los valores por defecto*/
                                DateTime fecha = DateTime.Now;

                                itemlin.fecha_solicitud = Convert.ToDateTime(fecha.ToString("dd/MM/yyyy"));
                                // itemlin.fecha_creacion = Convert.ToDateTime(fecha.ToString("dd/MM/yyyy"));   
                                PlanTelefonico pEntidad = new PlanTelefonico();
                                pEntidad = DAPlanesTelefonicos.CrearLineaTelefonica(vCod_Ope, itemlin, pUsuario);

                                if (pEntidad.cod_serv_fijo == 0 && pEntidad.cod_serv_adicional == 0)
                                {
                                    lst_num_lin.Add(pEntidad);
                                }
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

        //TRASPASO
        public PlanTelefonico Traspaso(ref Int64 vCod_Ope, Xpinn.Tesoreria.Entities.Operacion pOperacion, PlanTelefonico pPlanTel, Usuario pusuario)
        {
            try
            {

                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {

                    //OPERACION
                    Xpinn.Tesoreria.Data.OperacionData DAOperacion = new Tesoreria.Data.OperacionData();
                    pOperacion = DAOperacion.GrabarOperacion(pOperacion, pusuario);
                    vCod_Ope = pOperacion.cod_ope;

                    if (vCod_Ope != 0)
                    {
                        pPlanTel = DAPlanesTelefonicos.Traspaso(vCod_Ope, pPlanTel, pusuario);
                        ts.Complete();
                    }
                    else
                    {
                        return null;
                    }
                }

                return pPlanTel;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanesTelefonicosBusiness", "Traspaso", ex);
                return null;
            }
        }
        
        //REPOSICION
        public PlanTelefonico Reposicion(PlanTelefonico pPlanTel, Usuario pusuario)
        {
            try
            {

                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pPlanTel = DAPlanesTelefonicos.Reposicion(pPlanTel, pusuario);
                    ts.Complete();
                }

                return pPlanTel;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanesTelefonicosBusiness", "Reposicion", ex);
                return null;
            }
        }

        //CANCELACION
        public PlanTelefonico Cancelacion(PlanTelefonico pPlanTel, Usuario pusuario)
        {
            try
            {

                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pPlanTel = DAPlanesTelefonicos.Cancelacion(pPlanTel, pusuario);
                    ts.Complete();
                }

                return pPlanTel;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanesTelefonicosBusiness", "Cancelacion", ex);
                return null;
            }
        }

        /// <summary>
        /// Método de activación de líneas telefónicas.
        /// </summary>
        /// <param name="pPlanTel"></param>
        /// <param name="pusuario"></param>
        /// <returns></returns>
        public PlanTelefonico ActivacionDeLineasTelefonica(ref string pError,ref Xpinn.Tesoreria.Entities.Operacion pOperacion, PlanTelefonico pPlanTel, Usuario pusuario)
        {
            try
            {

                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    Xpinn.Tesoreria.Data.OperacionData DAOperacion = new Tesoreria.Data.OperacionData();
                    pOperacion = DAOperacion.GrabarOperacion(pOperacion, pusuario);
                    
                    pPlanTel = DAPlanesTelefonicos.ActivacionDeLineasTelefonica(ref pError, pOperacion.cod_ope, pPlanTel, pusuario);
                    if (!string.IsNullOrEmpty(pError))
                        return null;
                    ts.Complete();
                }

                return pPlanTel;
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                return null;
            }
        }

    }
}


