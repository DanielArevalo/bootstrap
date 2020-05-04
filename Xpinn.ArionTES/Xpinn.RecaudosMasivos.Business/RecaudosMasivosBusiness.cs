using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Transactions;
using System.Xml;
using System.Linq;
using Xpinn.Util;
using Xpinn.Tesoreria.Data;
using Xpinn.Tesoreria.Entities;
using Xpinn.FabricaCreditos.Entities;
using System.Web;
using System.IO;

namespace Xpinn.Tesoreria.Business
{
    public class RecaudosMasivosBusiness : GlobalBusiness
    {
        private RecaudosMasivosData DARecaudosMasivos;
        private StreamReader strReader;

        const string quote = "\"";

        public RecaudosMasivosBusiness()
        {
            DARecaudosMasivos = new RecaudosMasivosData();
        }


        /// <summary>
        /// Cargar el archivo con los descuentos de nomina
        /// </summary>
        /// <param name="pEntidad"></param>
        /// <param name="pEstructura"></param>
        /// <param name="pFecha"></param>
        /// <param name="pstream"></param>
        /// <param name="perror"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public List<RecaudosMasivos> CargarArchivo(Int64 pEntidad, Int64 pEstructura, DateTime pFecha, Stream pstream, Int64? pNumeroNovedad, ref string perror, ref List<Xpinn.Tesoreria.Entities.ErroresCarga> plstErrores, Usuario pUsuario)
        {
            // Inicializar datos
            Configuracion conf = new Configuracion();
            string sSeparadorDecimal = conf.ObtenerSeparadorDecimalConfig();
            // Cargar datos por defecto
            const char separador = ',';
            Boolean bEncabezado = false;
            Boolean bFinal = false;
            // Consultar datos de la empresa
            EmpresaRecaudoData DAEmpresa = new EmpresaRecaudoData();
            EmpresaRecaudo empresa = new EmpresaRecaudo();
            empresa = DAEmpresa.ConsultarEMPRESARECAUDO(pEntidad, pUsuario);
            if (empresa == null)
            {
                perror = "No pudo encontrar datos de la empresa";
                return null;
            }
            if (pNumeroNovedad == null)
            {
                empresa.aplica_novedades = 0;
            }
            // Traer datos de la estructura
            Estructura_Carga estructura = new Estructura_Carga();
            EstructuraRecaudoBusiness DAEstructura = new EstructuraRecaudoBusiness();
            estructura.cod_estructura_carga = Convert.ToInt32(pEstructura);
            estructura = DAEstructura.ConsultarEstructuraCarga(estructura, pUsuario);
            List<Estructura_Carga_Detalle> lstEstructuraDetalle = new List<Estructura_Carga_Detalle>();
            if (estructura.cod_estructura_carga != null)
            {
                Estructura_Carga_Detalle estructuraDET = new Estructura_Carga_Detalle();
                estructuraDET.cod_estructura_carga = estructura.cod_estructura_carga;
                lstEstructuraDetalle = DAEstructura.ListarEstructuraDetalle(estructuraDET, " ORDER BY NUMERO_COLUMNA,COD_ESTRUCTURA_DETALLE ", pUsuario);
            }
            // Inicializar control de errores
            RegistrarError(-1, "", "", "", ref plstErrores);
            // Determinar el número de líneas
            int totallineas = 4;
            string readLine;
            // Cargar los datos                        
            int contador = 0;
            List<RecaudosMasivos> lstRecaudos = new List<RecaudosMasivos>();
            try
            {
                using (strReader = new StreamReader(pstream))
                {
                    while (strReader.Peek() >= 0)
                    {
                        readLine = strReader.ReadLine();
                        RecaudosMasivos entidad = new RecaudosMasivos();
                        if (estructura.cod_estructura_carga != null)
                        {
                            Boolean bCargar = true;
                            if (estructura.tipo_archivo == 0)
                            {
                                // Si el archivo es un EXCEL
                            }
                            else
                            {
                                // No cargar lineas del encabezado
                                if (estructura.encabezado > 0 && contador < estructura.encabezado)
                                    bCargar = false;
                                // No cargar líneas del final
                                if (estructura.final > 0 && totallineas - contador + 1 < estructura.final)
                                    bCargar = false;
                                // Validar que la línea tenga datos
                                if (readLine.Trim() == "")
                                    bCargar = false;
                                // Si es un archivo de TEXTO
                                if (bCargar == true)
                                {
                                    // Inicializando las variables
                                    Int64? codpersona = null;
                                    string identificacion = "";
                                    string nombre = "";
                                    string concepto = "";
                                    decimal? valor = null;
                                    string numero_producto = "";
                                    string cod_nomina = "";
                                    DateTime? fechaRecaudo = null;

                                    // Cargando los datos según el tipo de archivo
                                    string[] arrayline;
                                    if (estructura.tipo_datos == 0)
                                    {
                                        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                                        // Si el archivo es delimitado
                                        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                                        // Separar los campos del archivo
                                        if (estructura.separador_campo == "0")
                                            estructura.separador_campo = "  ";
                                        if (estructura.separador_campo == "1")
                                            estructura.separador_campo = ",";
                                        if (estructura.separador_campo == "2")
                                            estructura.separador_campo = ";";
                                        if (estructura.separador_campo == "3")
                                            estructura.separador_campo = " ";
                                        arrayline = readLine.Split(Convert.ToChar(estructura.separador_campo));
                                    }
                                    else
                                    {
                                        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                                        // Si el archivo es de ancho fijo
                                        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                                        arrayline = readLine.Split(Convert.ToChar("&"));
                                        arrayline[0] = readLine.ToString();
                                    }

                                    // Inicializar variables requeridas
                                    int largoTotal = 0;
                                    int contadorreg = 0;
                                    foreach (Estructura_Carga_Detalle estDetalle in lstEstructuraDetalle)
                                    {
                                        if (estDetalle.justificador == null)
                                            estDetalle.justificador = "";
                                        // Cuando el archivo es de ancho fijo validar las posiciones de cada campo
                                        if (estructura.tipo_datos != 0)
                                        {
                                            if (largoTotal + Convert.ToInt32(estDetalle.longitud) - 1 > arrayline[0].ToString().Length)
                                            {
                                                if (largoTotal - 1 < arrayline[0].ToString().Length)
                                                    estDetalle.longitud = arrayline[0].ToString().Length - largoTotal - 1;
                                                else
                                                    estDetalle.longitud = 0;
                                            }
                                        }
                                        // Ajustar los datos según el tipo de dato 0=Separador 1=Ancho Fijo
                                        Int32 posicion;
                                        if (estructura.tipo_datos == 0)
                                        {
                                            posicion = Convert.ToInt32(estDetalle.posicion_inicial);
                                        }
                                        else
                                        {
                                            // Se ajusta la posición inicial porque en c# la primera posición es cero
                                            posicion = Convert.ToInt32(estDetalle.posicion_inicial) - 1;
                                            if (posicion > arrayline[0].ToString().Length)
                                            {
                                                posicion = arrayline[0].ToString().Length;
                                                estDetalle.longitud = 0;
                                            }
                                            // Control de la longitud de la cadena
                                            largoTotal += Convert.ToInt32(estDetalle.longitud);
                                        }

                                        // Validar la linea a cargar
                                        string lineaACargar = "";
                                        if (estructura.tipo_datos == 0)
                                            lineaACargar = arrayline[contadorreg];
                                        else
                                            lineaACargar = arrayline[0];

                                        if (posicion >= 0 && ((lineaACargar.Trim().Length > 0 && estructura.tipo_datos == 0) || (lineaACargar.Trim().Length > 0 && estructura.tipo_datos != 0)))
                                        {
                                            // 1=codigoCliente 2=Identificacion 3=Nombre y Apellidos 4=Linea 5=Valor 6=FechaEncabezado 7=NumProducto 8=Fec Prox Pago 9=Tipo Producto
                                            if (estDetalle.codigo_campo == 1)
                                            {
                                                try
                                                {
                                                    if (estructura.tipo_datos == 0)
                                                        codpersona = Convert.ToInt64(arrayline[contadorreg].ToString().Replace(estructura.separador_miles, ""));
                                                    else
                                                        codpersona = Convert.ToInt64(arrayline[0].ToString().Substring(posicion, Convert.ToInt32(estDetalle.longitud)).Replace(estructura.separador_miles, ""));
                                                }
                                                catch
                                                {
                                                    codpersona = null;
                                                }
                                            }
                                            if (estDetalle.codigo_campo == 2)
                                            {
                                                if (estructura.tipo_datos == 0)
                                                {
                                                    if (estDetalle.justificacion == "1" && estDetalle.justificador != "")
                                                        identificacion = AjustarCampos(arrayline[contadorreg].ToString().Replace(estructura.separador_miles, ""), Convert.ToChar(estDetalle.justificador)).Trim();
                                                    else
                                                        identificacion = arrayline[contadorreg].ToString().Replace(estructura.separador_miles, "").Trim();
                                                }
                                                else
                                                {
                                                    identificacion = arrayline[contadorreg].ToString().Substring(posicion, Convert.ToInt32(estDetalle.longitud)).Replace(estructura.separador_miles, "").Trim();
                                                }
                                            }
                                            if (estDetalle.codigo_campo == 3)
                                            {
                                                if (estructura.tipo_datos == 0)
                                                {
                                                    if (estDetalle.justificacion == "1" && estDetalle.justificador != "")
                                                        nombre = AjustarCampos(arrayline[contadorreg].ToString().Replace(estructura.separador_miles, ""), Convert.ToChar(estDetalle.justificador));
                                                    else
                                                        nombre = arrayline[contadorreg].ToString().Replace(estructura.separador_miles, "");
                                                }
                                                else
                                                {
                                                    nombre = arrayline[0].ToString().Substring(posicion, Convert.ToInt32(estDetalle.longitud)).Replace(estructura.separador_miles, "");
                                                }
                                            }
                                            if (estDetalle.codigo_campo == 4)
                                            {
                                                if (estructura.tipo_datos == 0)
                                                {
                                                    if (estDetalle.justificacion == "1" && estDetalle.justificador != "")
                                                        concepto = AjustarCampos(arrayline[contadorreg].ToString().Replace(estructura.separador_miles, ""), Convert.ToChar(estDetalle.justificador));
                                                    else
                                                        concepto = arrayline[contadorreg].ToString().Replace(estructura.separador_miles, "");
                                                }
                                                else
                                                {
                                                    concepto = arrayline[0].ToString().Substring(posicion, Convert.ToInt32(estDetalle.longitud)).Replace(estructura.separador_miles, "");
                                                }
                                            }
                                            if (estDetalle.codigo_campo == 5)
                                            {
                                                try
                                                {
                                                    if (estructura.tipo_datos == 0)
                                                    {
                                                        if (estDetalle.justificacion == "1" && estDetalle.justificador != "")
                                                        {
                                                            valor = Decimal.Parse(AjustarCampos(arrayline[contadorreg].ToString().Replace(estructura.separador_miles, ""), Convert.ToChar(estDetalle.justificador)));
                                                        }
                                                        else
                                                        {
                                                            string aux = "";
                                                            aux = arrayline[contadorreg].ToString().Replace(estructura.separador_decimal, "^");
                                                            aux = aux.Replace(estructura.separador_miles, "");
                                                            aux = arrayline[contadorreg].ToString().Replace("^", sSeparadorDecimal);
                                                            valor = Decimal.Parse(aux);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        string aux = "";
                                                        aux = arrayline[0].ToString().Replace(estructura.separador_decimal, "^");
                                                        aux = aux.Replace(estructura.separador_miles, "");
                                                        aux = arrayline[0].ToString().Substring(posicion, Convert.ToInt32(estDetalle.longitud)).Replace("^", sSeparadorDecimal);
                                                        valor = Decimal.Parse(aux);
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    valor = 0;
                                                    RegistrarError(contadorreg, estDetalle.codigo_campo.ToString(), ex.Message, readLine.ToString(), ref plstErrores);
                                                }
                                            }
                                            if (estDetalle.codigo_campo == 6)
                                            {
                                                string sformato_fecha = "";
                                                if (estructura.formato_fecha == "1")
                                                    sformato_fecha = "dd/MM/yyyy";
                                                else if (estructura.formato_fecha == "2")
                                                    sformato_fecha = "yyyy/MM/dd";
                                                else if (estructura.formato_fecha == "3")
                                                    sformato_fecha = "MM/dd/yyyy";
                                                else
                                                    sformato_fecha = "dd/MM/yyyy";
                                                if (estructura.tipo_datos == 0)
                                                {
                                                    if (estDetalle.justificacion == "1" && estDetalle.justificador != "")
                                                        fechaRecaudo = DateTime.ParseExact(AjustarCampos(arrayline[contadorreg].ToString().Replace(estructura.separador_miles, ""), Convert.ToChar(estDetalle.justificador)), sformato_fecha, null);
                                                    else
                                                        fechaRecaudo = DateTime.ParseExact(arrayline[contadorreg].ToString().Replace(estructura.separador_miles, ""), sformato_fecha, null);
                                                }
                                                else
                                                {
                                                    fechaRecaudo = DateTime.ParseExact(arrayline[0].ToString().Substring(posicion, Convert.ToInt32(estDetalle.longitud)).Replace(estructura.separador_miles, ""), sformato_fecha, null);
                                                }
                                            }
                                            if (estDetalle.codigo_campo == 7)
                                            {
                                                if (estructura.tipo_datos == 0)
                                                {
                                                    if (estDetalle.justificacion == "1" && estDetalle.justificador != "")
                                                        numero_producto = AjustarCampos(arrayline[contadorreg].ToString().Replace(estructura.separador_miles, ""), Convert.ToChar(estDetalle.justificador));
                                                    else
                                                        numero_producto = arrayline[contadorreg].ToString().Replace(estructura.separador_miles, "");
                                                }
                                                else
                                                {
                                                    numero_producto = arrayline[0].ToString().Substring(posicion, Convert.ToInt32(estDetalle.longitud)).Replace(estructura.separador_miles, "");
                                                }
                                            }
                                            if (estDetalle.codigo_campo == 23)
                                            {
                                                if (estructura.tipo_datos == 0)
                                                {
                                                    if (estDetalle.justificacion == "1" && estDetalle.justificador != "")
                                                        cod_nomina = AjustarCampos(arrayline[contadorreg].ToString().Replace(estructura.separador_miles, ""), Convert.ToChar(estDetalle.justificador)).Trim();
                                                    else
                                                        cod_nomina = arrayline[contadorreg].ToString().Replace(estructura.separador_miles, "").Trim();
                                                }
                                                else
                                                {
                                                    cod_nomina = arrayline[0].ToString().Substring(posicion, Convert.ToInt32(estDetalle.longitud)).Replace(estructura.separador_miles, "").Trim();
                                                    cod_nomina = AjustarCampos(cod_nomina, Convert.ToChar(estDetalle.justificador)).Trim();
                                                }
                                            }
                                        }
                                        contadorreg += 1;
                                    }

                                    // Cargar los conceptos según la distribucion
                                    List<RecaudosMasivos> lstRecaudosGen = new List<RecaudosMasivos>();
                                    lstRecaudosGen = DistribuirPago(pEntidad, pFecha, codpersona, identificacion, cod_nomina, concepto, numero_producto, Convert.ToDecimal(valor), Convert.ToInt32(empresa.mayores_valores), Convert.ToInt32(empresa.aplica_novedades), pNumeroNovedad, Convert.ToInt32(empresa.forma_cobro), empresa.aplica_refinanciados, lstRecaudos, pUsuario, empresa.aplica_mora, empresa.tipo_recaudo, ref plstErrores);
                                    if (lstRecaudosGen != null)
                                    {
                                        foreach (RecaudosMasivos eRecaudos in lstRecaudosGen)
                                        {
                                            Boolean bEncontro = false;
                                            foreach (RecaudosMasivos eRecTot in lstRecaudos)
                                            {
                                                if (eRecTot.tipo_producto == eRecaudos.tipo_producto && eRecTot.numero_producto == eRecaudos.numero_producto
                                                    && eRecTot.identificacion == eRecaudos.identificacion && eRecTot.tipo_aplicacion == eRecaudos.tipo_aplicacion)
                                                {
                                                    bEncontro = true;
                                                    eRecTot.valor += eRecaudos.valor;
                                                    break;
                                                }
                                            }
                                            if (bEncontro == false && eRecaudos.valor != 0)
                                                lstRecaudos.Add(eRecaudos);
                                        }
                                    }
                                    else
                                    {
                                        RegistrarError(contadorreg, contador.ToString(), "", readLine.ToString(), ref plstErrores);
                                    }
                                }
                            }
                        }
                        else
                        {
                            ////////////////// Archivo para el BANCO DE BOGOTA //////////////////
                            if (pEntidad == 1)
                            {
                                readLine = ValidarCampos(readLine, separador);
                                // Determinar si corresponde al encabezado del archivo
                                if (contador == 0)
                                    bEncabezado = true;
                                else
                                    bEncabezado = false;
                                // Determinar si corresponde al final del archivo
                                if (readLine.Split(separador).Count() < 15)
                                    bFinal = true;
                                else
                                    bFinal = false;
                                // Cargar cuerpo del archivo
                                if (readLine.Trim() != "")
                                {
                                    if (!bEncabezado && !bFinal)
                                    {
                                        string[] arrayline = readLine.Split(separador);
                                        string identificacion = arrayline[5].ToString().Replace(quote, "").Trim();
                                        DateTime fechaRecaudo = System.DateTime.Now;
                                        decimal valor = 0;
                                        try
                                        {
                                            string svalor = "";
                                            svalor = arrayline[11].ToString().Replace("$", "");
                                            svalor = svalor.Replace(",", "");
                                            svalor = svalor.Replace(quote, "");
                                            svalor = svalor.Replace(".", sSeparadorDecimal);
                                            valor = Convert.ToDecimal(svalor);
                                            svalor = arrayline[0].ToString().Trim();
                                            fechaRecaudo = new DateTime(Convert.ToInt32(svalor.Substring(6, 4)), Convert.ToInt32(svalor.Substring(3, 2)), Convert.ToInt32(svalor.Substring(0, 2)));
                                        }
                                        catch (Exception ex)
                                        {
                                            perror = ex.Message + " " + readLine;
                                            return null;
                                        }
                                        List<RecaudosMasivos> lstRecaudosGen = new List<RecaudosMasivos>();
                                        lstRecaudosGen = DistribuirPago(pEntidad, pFecha, null, identificacion, "", "", "", valor, Convert.ToInt32(empresa.mayores_valores), Convert.ToInt32(empresa.aplica_novedades), pNumeroNovedad, Convert.ToInt32(empresa.forma_cobro), empresa.aplica_refinanciados, lstRecaudos, pUsuario, empresa.aplica_mora, empresa.tipo_recaudo, ref plstErrores);
                                        foreach (RecaudosMasivos eRecaudos in lstRecaudosGen)
                                        {
                                            Boolean bEncontro = false;
                                            foreach (RecaudosMasivos eRecTot in lstRecaudos)
                                            {
                                                if (eRecTot.tipo_producto == eRecaudos.tipo_producto && eRecTot.numero_producto == eRecaudos.numero_producto
                                                    && eRecTot.identificacion == eRecaudos.identificacion && eRecTot.tipo_aplicacion == eRecaudos.tipo_aplicacion)
                                                {
                                                    bEncontro = true;
                                                    eRecTot.valor += eRecaudos.valor;
                                                    break;
                                                }
                                            }
                                            if (bEncontro == false && eRecaudos.valor != 0)
                                                lstRecaudos.Add(eRecaudos);
                                        }
                                    }
                                }
                            }
                            ////////////////// Archivo para el BANCO DE OCCIDENTE //////////////////
                            if (pEntidad == 2)
                            {
                                if (readLine.Contains("TOTALES Banco de Occidente:") == true)
                                    return lstRecaudos;
                                // Cargar cuerpo del archivo
                                if (readLine.Trim() != "")
                                {
                                    string identificacion = AjustarCampos(readLine.Substring(0, 18), '0');
                                    string numero_referencia = "";
                                    decimal valor = 0;
                                    string saux = "";
                                    try
                                    {
                                        saux = readLine.Substring(19, 19).Replace("$", "");
                                        saux = saux.Replace(",", "");
                                        saux = saux.Replace(quote, "");
                                        saux = saux.Replace(".", sSeparadorDecimal);
                                        valor = Convert.ToDecimal(saux);
                                    }
                                    catch (Exception ex)
                                    {
                                        if (contador > 9)
                                        {
                                            perror = ex.Message + " " + readLine;
                                            return null;
                                        }
                                    }
                                    DateTime fechaRecaudo = System.DateTime.Now;
                                    saux = "";
                                    try
                                    {
                                        saux = readLine.Substring(87, 2) + "/" + readLine.Substring(84, 2) + "/" + readLine.Substring(79, 4);
                                        fechaRecaudo = Convert.ToDateTime(saux);
                                    }
                                    catch
                                    {
                                        fechaRecaudo = System.DateTime.Now;
                                    }
                                    List<RecaudosMasivos> lstRecaudosGen = new List<RecaudosMasivos>();
                                    lstRecaudosGen = DistribuirPago(pEntidad, pFecha, null, identificacion, "", "", numero_referencia, valor, Convert.ToInt32(empresa.mayores_valores), Convert.ToInt32(empresa.aplica_novedades), pNumeroNovedad, Convert.ToInt32(empresa.forma_cobro), empresa.aplica_refinanciados, lstRecaudos, pUsuario, empresa.aplica_mora, empresa.tipo_recaudo, ref plstErrores);
                                    foreach (RecaudosMasivos eRecaudos in lstRecaudosGen)
                                    {
                                        if (fechaRecaudo != System.DateTime.Now)
                                            eRecaudos.fecha_recaudo = fechaRecaudo;
                                        Boolean bEncontro = false;
                                        foreach (RecaudosMasivos eRecTot in lstRecaudos)
                                        {
                                            if (eRecTot.tipo_producto == eRecaudos.tipo_producto && eRecTot.numero_producto == eRecaudos.numero_producto
                                                && eRecTot.identificacion == eRecaudos.identificacion && eRecTot.tipo_aplicacion == eRecaudos.tipo_aplicacion)
                                            {
                                                bEncontro = true;
                                                eRecTot.valor += eRecaudos.valor;
                                                break;
                                            }
                                        }
                                        if (bEncontro == false && eRecaudos.valor != 0)
                                            lstRecaudos.Add(eRecaudos);
                                    }
                                }
                            }


                            ////////////////// Archivo para Pendientes//////////////////
                            if (pEntidad == 3)
                            {
                                const char separador2 = ';';
                                readLine = ValidarCampos(readLine, separador2);

                                // Cargar cuerpo del archivo
                                if (readLine.Trim() != "")
                                {

                                    string[] arrayline = readLine.Split(separador2);
                                    string identificacion = arrayline[0].ToString().Replace(quote, "").Trim();
                                    DateTime fechaRecaudo = System.DateTime.Now;
                                    decimal valor = 0;
                                    try
                                    {
                                        string svalor = "";
                                        svalor = arrayline[1].ToString().Replace("$", "");
                                        svalor = svalor.Replace(",", "");
                                        svalor = svalor.Replace(quote, "");
                                        svalor = svalor.Replace(".", sSeparadorDecimal);
                                        valor = Convert.ToDecimal(svalor);
                                        svalor = arrayline[0].ToString().Trim();

                                    }
                                    catch (Exception ex)
                                    {
                                        if (contador > 1)
                                        {
                                            perror = ex.Message + " " + readLine;
                                            return null;
                                        }

                                    }
                                    List<RecaudosMasivos> lstRecaudosGen = new List<RecaudosMasivos>();
                                    lstRecaudosGen = DistribuirPago(pEntidad, pFecha, null, identificacion, "", "", "", valor, Convert.ToInt32(empresa.mayores_valores), Convert.ToInt32(empresa.aplica_novedades), pNumeroNovedad, Convert.ToInt32(empresa.forma_cobro), empresa.aplica_refinanciados, lstRecaudos, pUsuario, empresa.aplica_mora, empresa.tipo_recaudo, ref plstErrores);
                                    foreach (RecaudosMasivos eRecaudos in lstRecaudosGen)
                                    {
                                        Boolean bEncontro = false;
                                        foreach (RecaudosMasivos eRecTot in lstRecaudos)
                                        {
                                            if (eRecTot.tipo_producto == eRecaudos.tipo_producto && eRecTot.numero_producto == eRecaudos.numero_producto
                                                && eRecTot.identificacion == eRecaudos.identificacion && eRecTot.tipo_aplicacion == eRecaudos.tipo_aplicacion)
                                            {
                                                bEncontro = true;
                                                eRecTot.valor += eRecaudos.valor;
                                                break;
                                            }
                                        }
                                        if (bEncontro == false && eRecaudos.valor != 0)
                                            lstRecaudos.Add(eRecaudos);
                                    }

                                }
                            }
                        }
                        contador = contador + 1;
                    }
                }
            }
            catch (IOException ex)
            {
                perror = ex.Message;
            }

            return lstRecaudos;
        }

        public List<RecaudosMasivos> ListarDetalleRecaudoDeUnPeriodoYEmpresa(string codigoEmpresaRecaudadora, string fechaPeriodoNovedad, Usuario usuario)
        {
            try
            {
                return DARecaudosMasivos.ListarDetalleRecaudoDeUnPeriodoYEmpresa(codigoEmpresaRecaudadora, fechaPeriodoNovedad, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RecaudosMasivosBusiness", "ListarDetalleRecaudoDeUnPeriodoYEmpresa", ex);
                return null;
            }
        }

        /// <summary>
        /// Método para ajustar información de campos 
        /// </summary>
        /// <param name="plinea"></param>
        /// <param name="pseparador"></param>
        /// <returns></returns>
        public string ValidarCampos(string plinea, char pseparador)
        {
            string lineaNueva = "";
            Boolean bIniciaCampo = false;
            char[] array = plinea.ToCharArray();
            for (int i = 0; i < array.Length; i++)
            {
                char letter = array[i];
                if (letter == Convert.ToChar(quote))
                {
                    if (bIniciaCampo == false)
                        bIniciaCampo = true;
                    else
                        bIniciaCampo = false;
                }
                if (!(bIniciaCampo == true && letter == pseparador))
                {
                    lineaNueva = lineaNueva + Convert.ToString(letter);
                }
            }
            return lineaNueva;
        }

        public string AjustarCampos(string pcampo, char pcaracterajuste)
        {
            Boolean bencontro = true;
            string lineaNueva = "";
            char[] array = pcampo.ToCharArray();
            for (int i = 0; i < array.Length; i++)
            {
                char letter = array[i];
                if (!(letter == pcaracterajuste))
                    bencontro = false;
                if (bencontro == false)
                    lineaNueva = lineaNueva + Convert.ToString(letter);
            }
            return lineaNueva;
        }

        /// <summary>
        /// Método para distribuir el pago entre los créditos del asociado 
        /// </summary>
        /// <param name="pCodEmpresa">Código de la empresa</param>
        /// <param name="pFecha">Fecha o período al que corresponden los descuentos</param>
        /// <param name="CodPersona">Código de la persona</param>
        /// <param name="pidentificacion">Identificación de la persona</param>
        /// <param name="pconcepto">concepto al que corresponden los descuentos</param>
        /// <param name="pnumero_producto">número de producto</param>
        /// <param name="pValor">valor a descontar</param>
        /// <param name="lstRecaudosAcu">listado de los recuados previamente aplicados</param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public List<RecaudosMasivos> DistribuirPago(Int64 pCodEmpresa, DateTime pFecha, Int64? CodPersona, string pidentificacion, string pcod_nomina, string pconcepto, string pnumero_producto, decimal pValor, int pMayoresValores, int pAplicaSegunNovedades, Int64? pNumero_Novedad, int? pforma_cobro, int? paplicar_refinanciados, List<RecaudosMasivos> lstRecaudosAcu, Usuario pUsuario, int? paplica_mora, int? ptipo_recaudo, ref List<Xpinn.Tesoreria.Entities.ErroresCarga> plstErrores)
        {
            decimal pAplicado = pValor;
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    List<RecaudosMasivos> lstRecaudos = new List<RecaudosMasivos>();
                    List<Producto> lstProductos = new List<Producto>();

                    // Consultar datos de la persona
                    Persona1 persona = new Persona1();

                    if (CodPersona != null)
                    {
                        pidentificacion = "";
                        Xpinn.FabricaCreditos.Data.Persona1Data personaData = new FabricaCreditos.Data.Persona1Data();
                        persona = personaData.ConsultarPersona1(Convert.ToInt64(CodPersona), pUsuario);
                        if (persona != null)
                            pidentificacion = persona.identificacion;
                        else
                            pidentificacion = "";
                    }
                    else if (!string.IsNullOrWhiteSpace(pidentificacion))
                    {
                        CodPersona = null;
                        Xpinn.FabricaCreditos.Data.Persona1Data personaData = new FabricaCreditos.Data.Persona1Data();
                        persona.seleccionar = "Identificacion";
                        persona.identificacion = pidentificacion;
                        persona.soloPersona = 1;
                        persona = personaData.ConsultarPersona1Param(persona, pUsuario);
                        if (persona != null)
                            CodPersona = persona.cod_persona;
                        else
                            CodPersona = null;
                    }
                    else if (!string.IsNullOrWhiteSpace(pcod_nomina))
                    {
                        CodPersona = null;
                        Xpinn.FabricaCreditos.Data.Persona1Data personaData = new FabricaCreditos.Data.Persona1Data();
                        persona.seleccionar = "CodNomina";
                        persona.cod_nomina_empleado = pcod_nomina;
                        persona.soloPersona = 1;
                        persona = personaData.ConsultarPersona1Param(persona, pUsuario);
                        if (persona != null)
                            CodPersona = persona.cod_persona;
                        else
                            CodPersona = null;

                        if (string.IsNullOrWhiteSpace(pidentificacion))
                            pidentificacion = persona.identificacion;
                    }
                    else
                    {
                        return null;
                    }

                    if (pidentificacion == null && CodPersona == null || pidentificacion == "" && CodPersona == null)
                    {
                        RegistrarError(0, "", "Identificación", "La identificacion " + pidentificacion + "no fue encontrada", ref plstErrores);
                        return null;
                    }
                    // Determinar los productos que aplican para el concepto dado
                    if (pAplicaSegunNovedades == 1)
                    {
                        lstProductos = DARecaudosMasivos.ListarProductosNovedad(pCodEmpresa, pFecha, pidentificacion, pconcepto, pnumero_producto, Convert.ToInt64(pNumero_Novedad), paplicar_refinanciados, pUsuario, paplica_mora);
                    }
                    else
                    {
                        try
                        { 
                            if (paplica_mora == 1)
                            {
                                lstProductos = DARecaudosMasivos.ListarProductos(pCodEmpresa, pFecha, CodPersona.ToString(), pconcepto, pnumero_producto, pforma_cobro, pUsuario, paplica_mora, ptipo_recaudo);
                            }
                            else
                            {
                                lstProductos = DARecaudosMasivos.ListarProductos(pCodEmpresa, pFecha, pidentificacion, pconcepto, pnumero_producto, pforma_cobro, pUsuario, paplica_mora, ptipo_recaudo);
                            }
                        }
                        catch
                        {
                            RegistrarError(0, "", "Identificación", "La identificacion " + pidentificacion + "no se pudo listar productos", ref plstErrores);
                            return null;
                        }

                    }
                    // Se verifica si la persona esta en vacaciones, si lo esta se multiplica el valor total a pagar por el numero de cuotas estipuladas
                    if (pNumero_Novedad.HasValue && pNumero_Novedad.Value != 0 && (!string.IsNullOrWhiteSpace(pidentificacion) || (CodPersona.HasValue && CodPersona.Value != 0)))
                    {
                        int numeroCuotas = DARecaudosMasivos.ConsultarCuotasPersonaEnVacaciones(CodPersona, pCodEmpresa, pidentificacion, pFecha, pUsuario);

                        if (numeroCuotas != 0)
                        {

                            lstProductos.ForEach(x =>
                            {
                                if (x.vacaciones == 0)
                                {
                                    x.valor_a_pagar *= numeroCuotas;
                                    x.total_a_pagar *= numeroCuotas;
                                }
                            });
                            // Se ajustó para que no cambie el valor que hay que distribuir se le puso la condición. FerOrt. 27-Marzo-2018.
                            /*if (pAplicaSegunNovedades == 1)
                                pAplicado *= numeroCuotas;    */
                        }
                    }

                    foreach (Producto eProducto in lstProductos)
                    {
                        if (pAplicado > 0)
                        {
                            string[] strNomProd = null;
                            if (eProducto.nom_tipo_producto.ToUpper().Contains("CREDITOS-CUOTAS EXTRAS"))
                            {
                                eProducto.nom_tipo_producto = "CREDITOS - CUOTAS EXTRAS".ToLower();
                            }
                            else
                            {
                                if (eProducto.nom_tipo_producto.Contains("-"))
                                    strNomProd = eProducto.nom_tipo_producto.Split('-');
                                eProducto.nom_tipo_producto = strNomProd != null ? strNomProd[0].Trim() : eProducto.nom_tipo_producto.Trim();
                            }
                            // Distribución de los aportes
                            if (eProducto.nom_tipo_producto == "1" || eProducto.nom_tipo_producto.ToLower() == "aportes" || eProducto.nom_tipo_producto.ToLower() == "1-aportes")
                            {
                                // Determina si el aporte ya existe y descuenta el valor
                                decimal valorAcumulado = 0;
                                foreach (RecaudosMasivos eRecTot in lstRecaudosAcu)
                                {
                                    if (eRecTot.tipo_producto.ToLower() == "aportes" && eRecTot.numero_producto == Convert.ToString(eProducto.num_producto)
                                        && eRecTot.identificacion == eProducto.identificacion)
                                        valorAcumulado += eRecTot.valor;
                                }
                                eProducto.valor_a_pagar = eProducto.valor_a_pagar - valorAcumulado;
                                if (eProducto.valor_a_pagar < 0)
                                    eProducto.valor_a_pagar = 0;
                                // Distribuye valor aplicado, si es el único producto lo aplica todo.
                                decimal valorAplicar = 0;
                                if (pMayoresValores == 1)
                                {

                                    if (eProducto.valor_a_pagar > pAplicado)
                                        valorAplicar = Convert.ToDecimal(pAplicado);
                                    else
                                        valorAplicar = eProducto.valor_a_pagar;
                                }
                                else
                                {

                                    if (lstProductos.Count() == 1)
                                        valorAplicar = Convert.ToDecimal(pAplicado);
                                    else
                                          if (eProducto.valor_a_pagar > pAplicado)
                                        valorAplicar = Convert.ToDecimal(pAplicado);
                                    else
                                        valorAplicar = eProducto.valor_a_pagar;
                                }
                                RecaudosMasivos eRecaudos = new RecaudosMasivos();
                                eRecaudos.fecha_aplicacion = pFecha;
                                eRecaudos.fecha_recaudo = pFecha;
                                eRecaudos.tipo_producto = "Aportes";
                                eRecaudos.numero_producto = Convert.ToString(eProducto.num_producto);
                                eRecaudos.cod_cliente = Convert.ToInt64(CodPersona);
                                eRecaudos.identificacion = eProducto.identificacion;
                                eRecaudos.nombre = eProducto.nombre;
                                eRecaudos.num_cuotas = 0;
                                eRecaudos.valor = valorAplicar;
                                eRecaudos.tipo_aplicacion = "Por Valor a Capital";
                                eRecaudos.cod_nomina_empleado = pcod_nomina;
                                lstRecaudos.Add(eRecaudos);
                                pAplicado = pAplicado - valorAplicar;
                                eProducto.total_a_pagar = eProducto.total_a_pagar - valorAplicar;
                            }
                            // Distribución de los créditos                           
                            if (eProducto.nom_tipo_producto == "2" || eProducto.nom_tipo_producto.ToLower() == "creditos" || eProducto.nom_tipo_producto.ToLower() == "2-creditos" || eProducto.nom_tipo_producto.ToLower() == "créditos")
                            {
                                // Determina si el crédito ya existe y descuenta el valor
                                decimal valorAcumulado = 0;
                                foreach (RecaudosMasivos eRecTot in lstRecaudosAcu)
                                {
                                    if (eRecTot.tipo_producto.ToLower() == "creditos" && eRecTot.numero_producto == Convert.ToString(eProducto.num_producto)
                                        && eRecTot.identificacion == eProducto.identificacion && (eRecTot.tipo_aplicacion == "Por Valor a Capital" || eRecTot.tipo_aplicacion == "Abono a Capital"))
                                        valorAcumulado += eRecTot.valor;
                                }
                                eProducto.valor_a_pagar = eProducto.valor_a_pagar - valorAcumulado;
                                if (eProducto.valor_a_pagar < 0)
                                    eProducto.valor_a_pagar = 0;
                                // Distribuye valor aplicado si es el único producto lo aplica todo
                                decimal valorAplicar = 0;
                                if (pMayoresValores == 0)
                                {
                                    if (lstProductos.Count() == 1)
                                        valorAplicar = Convert.ToDecimal(pAplicado);
                                    else
                                        if (eProducto.valor_a_pagar > pAplicado)
                                        valorAplicar = Convert.ToDecimal(pAplicado);
                                    else
                                        valorAplicar = eProducto.valor_a_pagar;
                                }
                                else
                                {
                                    if (eProducto.valor_a_pagar > pAplicado)
                                        valorAplicar = Convert.ToDecimal(pAplicado);
                                    else
                                        valorAplicar = eProducto.valor_a_pagar;
                                }
                                // El valor a aplicar no puede ser mayor que el total adeudado del crédito
                                if (paplica_mora == 0)
                                {

                                    if (valorAplicar > eProducto.total_a_pagar)
                                        valorAplicar = eProducto.total_a_pagar;
                                }
                                RecaudosMasivos eRecaudos = new RecaudosMasivos();
                                eRecaudos.fecha_aplicacion = pFecha;
                                eRecaudos.fecha_recaudo = pFecha;
                                eRecaudos.tipo_producto = "Creditos";
                                eRecaudos.numero_producto = Convert.ToString(eProducto.num_producto);
                                eRecaudos.cod_cliente = Convert.ToInt64(CodPersona);
                                eRecaudos.identificacion = eProducto.identificacion;
                                eRecaudos.nombre = eProducto.nombre;
                                eRecaudos.valor = valorAplicar;
                                if (eProducto.refinanciado == 1)
                                {
                                    eRecaudos.tipo_aplicacion = "Abono a Capital";
                                    eRecaudos.num_cuotas = 1;
                                }
                                else
                                {
                                    eRecaudos.tipo_aplicacion = "Por Valor a Capital";
                                    eRecaudos.num_cuotas = 0;
                                }
                                eRecaudos.cod_nomina_empleado = pcod_nomina;
                                lstRecaudos.Add(eRecaudos);
                                pAplicado = pAplicado - valorAplicar;
                                eProducto.total_a_pagar = eProducto.total_a_pagar - valorAplicar;
                            }
                            // Distribución de las cuentas de ahorros a la vista
                            if (eProducto.nom_tipo_producto == "3" || eProducto.nom_tipo_producto.ToLower() == "ahorros a la vista" || eProducto.nom_tipo_producto.ToLower() == "3-ahorros a la vista" || eProducto.nom_tipo_producto.ToLower() == "depositos")
                            {
                                // Determina si la cuenta ya existe y descuenta el valor
                                decimal valorAcumulado = 0;
                                foreach (RecaudosMasivos eRecTot in lstRecaudosAcu)
                                {
                                    if ((eRecTot.tipo_producto.ToLower() == "ahorros a la vista" || eRecTot.tipo_producto.ToLower() == "depositos") && eRecTot.numero_producto == Convert.ToString(eProducto.num_producto)
                                        && eRecTot.identificacion == eProducto.identificacion)
                                        valorAcumulado += eRecTot.valor;
                                }
                                eProducto.valor_a_pagar = eProducto.valor_a_pagar - valorAcumulado;
                                if (eProducto.valor_a_pagar < 0)
                                    eProducto.valor_a_pagar = 0;
                                // Distribuye valor aplicado, si es el único producto lo aplica todo.
                                decimal valorAplicar = 0;
                                if (pMayoresValores == 0)
                                {
                                    if (lstProductos.Count() == 1)
                                        valorAplicar = Convert.ToDecimal(pAplicado);
                                    else
                                        if (eProducto.valor_a_pagar > pAplicado)
                                        valorAplicar = Convert.ToDecimal(pAplicado);
                                    else
                                        valorAplicar = eProducto.valor_a_pagar;
                                }
                                else
                                {
                                    if (lstProductos.Count() == 1)
                                        valorAplicar = Convert.ToDecimal(pAplicado);
                                    else
                                        if (eProducto.valor_a_pagar > pAplicado)
                                        valorAplicar = Convert.ToDecimal(pAplicado);
                                    else
                                        valorAplicar = eProducto.valor_a_pagar;
                                }
                                RecaudosMasivos eRecaudos = new RecaudosMasivos();
                                eRecaudos.fecha_aplicacion = pFecha;
                                eRecaudos.fecha_recaudo = pFecha;
                                eRecaudos.tipo_producto = "ahorros a la Vista";
                                eRecaudos.numero_producto = Convert.ToString(eProducto.num_producto);
                                eRecaudos.cod_cliente = Convert.ToInt64(CodPersona);
                                eRecaudos.identificacion = eProducto.identificacion;
                                eRecaudos.nombre = eProducto.nombre;
                                eRecaudos.num_cuotas = 0;
                                eRecaudos.valor = valorAplicar;
                                eRecaudos.tipo_aplicacion = "Déposito";
                                eRecaudos.cod_nomina_empleado = pcod_nomina;
                                lstRecaudos.Add(eRecaudos);
                                pAplicado = pAplicado - valorAplicar;
                                eProducto.total_a_pagar = eProducto.total_a_pagar - valorAplicar;
                            }
                            // Distribución de los servicios
                            if (eProducto.nom_tipo_producto == "4" || eProducto.nom_tipo_producto.ToLower() == "servicios" || eProducto.nom_tipo_producto.ToLower() == "4-servicios")
                            {
                                // Determina si el servicio ya existe y descuenta el valor
                                decimal valorAcumulado = 0;
                                foreach (RecaudosMasivos eRecTot in lstRecaudosAcu)
                                {
                                    if (eRecTot.tipo_producto.ToLower() == "servicios" && eRecTot.numero_producto == Convert.ToString(eProducto.num_producto)
                                        && eRecTot.identificacion == eProducto.identificacion)
                                        valorAcumulado += eRecTot.valor;
                                }
                                eProducto.valor_a_pagar = eProducto.valor_a_pagar - valorAcumulado;
                                if (eProducto.valor_a_pagar < 0)
                                    eProducto.valor_a_pagar = 0;
                                // Distribuye valor aplicado, si es el único producto lo aplica todo.
                                decimal valorAplicar = 0;
                                if (pMayoresValores == 0)
                                {
                                    if (lstProductos.Count() == 1)
                                        valorAplicar = Convert.ToDecimal(pAplicado);
                                    else
                                        if (eProducto.valor_a_pagar > pAplicado)
                                        valorAplicar = Convert.ToDecimal(pAplicado);
                                    else
                                        valorAplicar = eProducto.valor_a_pagar;
                                }
                                else
                                {
                                    if (lstProductos.Count() == 1)
                                        valorAplicar = Convert.ToDecimal(pAplicado);
                                    else
                                        if (eProducto.valor_a_pagar > pAplicado)
                                        valorAplicar = Convert.ToDecimal(pAplicado);
                                    else
                                        valorAplicar = eProducto.valor_a_pagar;
                                }
                                RecaudosMasivos eRecaudos = new RecaudosMasivos();
                                eRecaudos.fecha_aplicacion = pFecha;
                                eRecaudos.fecha_recaudo = pFecha;
                                eRecaudos.tipo_producto = "Servicios";
                                eRecaudos.numero_producto = Convert.ToString(eProducto.num_producto);
                                eRecaudos.cod_cliente = Convert.ToInt64(CodPersona);
                                eRecaudos.identificacion = eProducto.identificacion;
                                eRecaudos.nombre = eProducto.nombre;
                                eRecaudos.num_cuotas = 0;
                                eRecaudos.valor = valorAplicar;
                                eRecaudos.tipo_aplicacion = "Por Valor a Capital";
                                eRecaudos.cod_nomina_empleado = pcod_nomina;
                                lstRecaudos.Add(eRecaudos);
                                pAplicado = pAplicado - valorAplicar;
                                eProducto.total_a_pagar = eProducto.total_a_pagar - valorAplicar;
                            }
                            // Distribución de la afiliación
                            if (eProducto.nom_tipo_producto == "6" || eProducto.nom_tipo_producto.ToLower() == "afiliacion" || eProducto.nom_tipo_producto.ToLower() == "6-afiliacion" || eProducto.nom_tipo_producto.ToLower() == "afiliación")
                            {
                                // Determina si la afiliación ya existe y descuenta el valor
                                decimal valorAcumulado = 0;
                                foreach (RecaudosMasivos eRecTot in lstRecaudosAcu)
                                {
                                    if (eRecTot.tipo_producto.ToLower() == "afiliacion" && eRecTot.numero_producto == Convert.ToString(eProducto.num_producto)
                                        && eRecTot.identificacion == eProducto.identificacion)
                                        valorAcumulado += eRecTot.valor;
                                }
                                eProducto.valor_a_pagar = eProducto.valor_a_pagar - valorAcumulado;
                                if (eProducto.valor_a_pagar < 0)
                                    eProducto.valor_a_pagar = 0;
                                // Distribuye valor aplicado si es el único producto lo aplica toto
                                decimal valorAplicar = 0;
                                if (eProducto.valor_a_pagar > pAplicado)
                                    valorAplicar = Convert.ToDecimal(pAplicado);
                                else
                                    valorAplicar = eProducto.valor_a_pagar;
                                RecaudosMasivos eRecaudos = new RecaudosMasivos();
                                eRecaudos.fecha_aplicacion = pFecha;
                                eRecaudos.fecha_recaudo = pFecha;
                                eRecaudos.tipo_producto = "Afiliacion";
                                eRecaudos.numero_producto = Convert.ToString(eProducto.num_producto);
                                eRecaudos.cod_cliente = Convert.ToInt64(CodPersona);
                                eRecaudos.identificacion = eProducto.identificacion;
                                eRecaudos.nombre = eProducto.nombre;
                                eRecaudos.num_cuotas = 0;
                                eRecaudos.valor = valorAplicar;
                                eRecaudos.tipo_aplicacion = "Por Valor a Capital";
                                eRecaudos.cod_nomina_empleado = pcod_nomina;
                                lstRecaudos.Add(eRecaudos);
                                pAplicado = pAplicado - valorAplicar;
                                eProducto.total_a_pagar = eProducto.total_a_pagar - valorAplicar;
                            }
                            // Distribución de las cuentas de ahorros programado
                            if (eProducto.nom_tipo_producto == "9" || eProducto.nom_tipo_producto.ToLower() == "ahorro programado" || eProducto.nom_tipo_producto.ToLower() == "9-ahorro programado")
                            {
                                // Determina si la cuenta ya existe y descuenta el valor
                                decimal valorAcumulado = 0;
                                foreach (RecaudosMasivos eRecTot in lstRecaudosAcu)
                                {
                                    if (eRecTot.tipo_producto.ToLower() == "ahorro programado" && eRecTot.numero_producto == Convert.ToString(eProducto.num_producto)
                                        && eRecTot.identificacion == eProducto.identificacion)
                                        valorAcumulado += eRecTot.valor;
                                }
                                eProducto.valor_a_pagar = eProducto.valor_a_pagar - valorAcumulado;
                                if (eProducto.valor_a_pagar < 0)
                                    eProducto.valor_a_pagar = 0;
                                //Distribuye valor aplicado, si es el único producto lo aplicatodo 
                                decimal valorAplicar = 0;
                                if (pMayoresValores == 0)
                                {
                                    if (lstProductos.Count() == 1)
                                        valorAplicar = Convert.ToDecimal(pAplicado);
                                    else
                                        if (eProducto.valor_a_pagar > pAplicado)
                                        valorAplicar = Convert.ToDecimal(pAplicado);
                                    else
                                        valorAplicar = eProducto.valor_a_pagar;
                                }
                                else
                                {
                                    if (lstProductos.Count() == 1)
                                        valorAplicar = Convert.ToDecimal(pAplicado);
                                    else
                                        if (eProducto.valor_a_pagar > pAplicado)
                                        valorAplicar = Convert.ToDecimal(pAplicado);
                                    else
                                        valorAplicar = eProducto.valor_a_pagar;
                                }
                                RecaudosMasivos eRecaudos = new RecaudosMasivos();
                                eRecaudos.fecha_aplicacion = pFecha;
                                eRecaudos.fecha_recaudo = pFecha;
                                eRecaudos.tipo_producto = "ahorro programado";
                                eRecaudos.numero_producto = Convert.ToString(eProducto.num_producto);
                                eRecaudos.cod_cliente = Convert.ToInt64(CodPersona);
                                eRecaudos.identificacion = eProducto.identificacion;
                                eRecaudos.nombre = eProducto.nombre;
                                eRecaudos.num_cuotas = 0;
                                eRecaudos.valor = valorAplicar;
                                eRecaudos.tipo_aplicacion = "Déposito";
                                eRecaudos.cod_nomina_empleado = pcod_nomina;
                                lstRecaudos.Add(eRecaudos);
                                pAplicado = pAplicado - valorAplicar;
                                eProducto.total_a_pagar = eProducto.total_a_pagar - valorAplicar;
                            }
                            // Distribución de los créditos                           
                            if (eProducto.nom_tipo_producto == "10" || eProducto.nom_tipo_producto.ToLower() == "creditos - cuotas extras")
                            {
                                // Determina si el crédito ya existe y descuenta el valor
                                decimal valorAcumulado = 0;
                                foreach (RecaudosMasivos eRecTot in lstRecaudosAcu)
                                {
                                    if (eRecTot.tipo_producto.ToLower() == "creditos - cuotas extras" && eRecTot.numero_producto == Convert.ToString(eProducto.num_producto))
                                        valorAcumulado += eRecTot.valor;
                                }
                                eProducto.valor_a_pagar = eProducto.valor_a_pagar - valorAcumulado;
                                if (eProducto.valor_a_pagar < 0)
                                    eProducto.valor_a_pagar = 0;
                                // Distribuye valor aplicado si es el único producto lo aplica todo
                                decimal valorAplicar = 0;
                                if (pMayoresValores == 0)
                                {
                                    if (lstProductos.Count() == 1)
                                        valorAplicar = Convert.ToDecimal(pAplicado);
                                    else
                                        if (eProducto.valor_a_pagar > pAplicado)
                                        valorAplicar = Convert.ToDecimal(pAplicado);
                                    else
                                        valorAplicar = eProducto.valor_a_pagar;
                                }
                                else
                                {
                                    if (eProducto.valor_a_pagar > pAplicado)
                                        valorAplicar = Convert.ToDecimal(pAplicado);
                                    else
                                        valorAplicar = eProducto.valor_a_pagar;
                                }
                                RecaudosMasivos eRecaudos = new RecaudosMasivos();
                                eRecaudos.fecha_aplicacion = pFecha;
                                eRecaudos.fecha_recaudo = pFecha;
                                eRecaudos.tipo_producto = "Creditos - cuotas extras";
                                eRecaudos.numero_producto = Convert.ToString(eProducto.num_producto);
                                eRecaudos.cod_cliente = Convert.ToInt64(CodPersona);
                                eRecaudos.identificacion = eProducto.identificacion;
                                eRecaudos.nombre = eProducto.nombre;
                                eRecaudos.valor = valorAplicar;
                                eRecaudos.tipo_aplicacion = "";
                                eRecaudos.num_cuotas = 0;
                                eRecaudos.cod_nomina_empleado = pcod_nomina;
                                lstRecaudos.Add(eRecaudos);
                                pAplicado = pAplicado - valorAplicar;
                                eProducto.total_a_pagar = eProducto.total_a_pagar - valorAplicar;
                            }
                        }
                    }
                    // Si sobra plata y existen productos entonces aplicar valor                                        
                    if (pAplicado > 0 && lstProductos.Count > 0 && pMayoresValores == 1)
                    {
                        bool bEncontrado = false;
                        Producto eProductoSeleccionado = new Producto();
                        // Buscar en los productos los que tengan valores pendientes por aplicar
                        decimal valorAplicar = 0;
                        bool bExiste = false;
                        var lstcantidad = (from c in lstRecaudosAcu where c.cod_cliente == Convert.ToInt64(CodPersona) select new { c.tipo_producto, c.numero_producto, c.identificacion, c.tipo_aplicacion, c.valor }).ToList();
                        for (int j = 0; j < lstProductos.Count() && !bExiste; j++)
                        {
                            Producto eProducto = eProducto = lstProductos[j];
                            // Determinar si ya existen valores asignados para aplicar
                            decimal valorListAplicar = 0;
                            for (int i = 0; i < lstcantidad.Count(); i++)
                            {
                                if (lstcantidad[i].tipo_producto.ToLower() == "creditos" && lstcantidad[i].numero_producto == Convert.ToString(eProducto.num_producto)
                                    && lstcantidad[i].identificacion == eProducto.identificacion && (lstcantidad[i].tipo_aplicacion == "Por Valor a Capital" || lstcantidad[i].tipo_aplicacion == "Abono a Capital"))
                                {
                                    bExiste = true;
                                    valorListAplicar += lstcantidad[i].valor;
                                }
                            }
                            // Si el producto no tiene valores asignados por aplicar lo asigna
                            if (bExiste && lstProductos[j].total_a_pagar > valorListAplicar)
                            {
                                eProductoSeleccionado = eProducto;
                                bEncontrado = true;
                            }
                        }
                        if (bEncontrado)
                        {
                            if (eProductoSeleccionado.total_a_pagar > 0 && !bExiste)
                            {
                                RecaudosMasivos eRecaudos = new RecaudosMasivos();
                                eRecaudos.fecha_aplicacion = pFecha;
                                eRecaudos.fecha_recaudo = pFecha;
                                eRecaudos.tipo_producto = eProductoSeleccionado.nom_tipo_producto;
                                eRecaudos.numero_producto = Convert.ToString(eProductoSeleccionado.num_producto);
                                eRecaudos.cod_cliente = Convert.ToInt64(CodPersona);
                                eRecaudos.identificacion = eProductoSeleccionado.identificacion;
                                eRecaudos.nombre = eProductoSeleccionado.nombre;
                                if (eProductoSeleccionado.refinanciado == 1)
                                {
                                    eRecaudos.num_cuotas = 1;
                                    eRecaudos.tipo_aplicacion = "Abono a Capital";
                                }
                                else
                                {
                                    eRecaudos.num_cuotas = 0;
                                    eRecaudos.tipo_aplicacion = "Por Valor a Capital";
                                }
                                if (pAplicado > eProductoSeleccionado.total_a_pagar)
                                    valorAplicar = eProductoSeleccionado.total_a_pagar;
                                else
                                    valorAplicar = pAplicado;
                                eRecaudos.valor = valorAplicar;
                                eRecaudos.cod_nomina_empleado = pcod_nomina;
                                lstRecaudos.Add(eRecaudos);
                                pAplicado = pAplicado - valorAplicar;
                            }
                        }
                    }
                    // Si no se pudo aplicar entonces generar devolución
                    if (pAplicado > 0 && (pMayoresValores == 0 || pMayoresValores == 1))
                    {
                        RecaudosMasivos eRecaudos = new RecaudosMasivos();
                        eRecaudos.fecha_aplicacion = pFecha;
                        eRecaudos.fecha_recaudo = pFecha;
                        eRecaudos.tipo_producto = "Devolucion";
                        eRecaudos.numero_producto = pconcepto;
                        eRecaudos.cod_cliente = Convert.ToInt64(CodPersona);
                        eRecaudos.identificacion = pidentificacion;
                        Persona1 ePersona = new Persona1();
                        ePersona = DARecaudosMasivos.ConsultarPersona(pidentificacion, pUsuario);
                        eRecaudos.nombre = ePersona.nombre;
                        eRecaudos.num_cuotas = 0;
                        eRecaudos.valor = pAplicado;
                        eRecaudos.tipo_aplicacion = "";
                        eRecaudos.cod_nomina_empleado = pcod_nomina;
                        lstRecaudos.Add(eRecaudos);
                    }
                    // Si quedan valores mayores pendientes por aplicar enviarlo a aportes
                    if (pAplicado > 0 && pMayoresValores == 2)
                    {
                        RecaudosMasivos eRecaudos = new RecaudosMasivos();
                        eRecaudos.fecha_aplicacion = pFecha;
                        eRecaudos.fecha_recaudo = pFecha;
                        eRecaudos.tipo_producto = "Aportes";
                        eRecaudos.numero_producto = "";
                        eRecaudos.cod_cliente = Convert.ToInt64(CodPersona);
                        eRecaudos.identificacion = pidentificacion;
                        Persona1 ePersona = new Persona1();
                        ePersona = DARecaudosMasivos.ConsultarPersona(pidentificacion, pUsuario);
                        eRecaudos.nombre = ePersona.nombre;
                        eRecaudos.num_cuotas = 0;
                        eRecaudos.valor = pAplicado;
                        eRecaudos.tipo_aplicacion = "Por Valor a Capital";
                        eRecaudos.cod_nomina_empleado = pcod_nomina;
                        lstRecaudos.Add(eRecaudos);
                    }
                    return lstRecaudos;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RecaudosMasivosBusiness", "DistribuirPago", ex);
                return null;
            }

        }

        /// <summary>
        /// Método para aplicar los pagos
        /// </summary>
        /// <param name="numero_aplicacion"></param>
        /// <param name="fecha_reclamacion"></param>
        /// <param name="lstRecaudos"></param>
        /// <param name="pUsuario"></param>
        /// <param name="Error"></param>
        /// <param name="pCodOpe"></param>
        /// <returns></returns>
        public Boolean AplicarPago(Int64 numero_aplicacion, DateTime fecha_reclamacion, List<RecaudosMasivos> lstRecaudos, Usuario pUsuario, ref string Error, ref Int64 pCodOpe)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    bool resultado = ProcesoAplicarPago(numero_aplicacion, fecha_reclamacion, lstRecaudos, false, pUsuario, ref Error, ref pCodOpe);
                    ts.Complete();
                    return resultado;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RecaudosMasivosBusiness", "AplicarPago", ex);
                return false;
            }
        }

        public Boolean ProcesoAplicarPago(Int64 numero_aplicacion, DateTime fecha_reclamacion, List<RecaudosMasivos> lstRecaudos, Boolean pAporteXaplicar, Usuario pUsuario, ref string Error, ref Int64 pCodOpe, Int64 ptipoOpe = 119)
        {
            try
            {
                TransactionOptions topcio = new TransactionOptions();
                topcio.Timeout = TimeSpan.MaxValue;
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, topcio))
                {

                    Int64 error = 0;
                    Xpinn.Tesoreria.Data.OperacionData DATesoreria = new Xpinn.Tesoreria.Data.OperacionData();
                    Xpinn.Tesoreria.Entities.Operacion pOperacion = new Xpinn.Tesoreria.Entities.Operacion();

                    // Crear la operación
                    pOperacion.cod_ope = 0;
                    pOperacion.tipo_ope = ptipoOpe;
                    pOperacion.cod_usu = pUsuario.codusuario;
                    pOperacion.cod_ofi = pUsuario.cod_oficina;
                    pOperacion.fecha_oper = fecha_reclamacion;
                    pOperacion.fecha_calc = fecha_reclamacion;
                    pOperacion.num_lista = numero_aplicacion;
                    pOperacion.cod_ope = DATesoreria.CrearOperacion(pOperacion, ref Error, pUsuario);
                    if (Error.Trim() == "")
                    { 
                        if (pOperacion.cod_ope < 0)
                        {
                            Error = "No se pudo crear la operación de tipo " + ptipoOpe + " para la fecha " + fecha_reclamacion;
                            ts.Dispose();
                            return false;
                        }
                    }
                    pCodOpe = pOperacion.cod_ope;

                    // Aplica las reclamaciones en cada crédito
                    foreach (RecaudosMasivos gRecaudosMasivos in lstRecaudos)
                    {
                        gRecaudosMasivos.cambioestado = 0;
                        if (gRecaudosMasivos.estadod == 0)
                        {
                            if (gRecaudosMasivos.tipo_producto.ToUpper().Contains("APORTE", StringComparison.OrdinalIgnoreCase))
                            {
                                // Se ajusto para que verifique si es línea de aportes para que no envie a APORTES PENDIENTES los fondos sociales. 5-Nov-2019. FerOrt.
                                AportePendientes pEntidad = new AportePendientes();
                                bool _lineasEsAporte = DARecaudosMasivos.LineaEsAporte(Convert.ToInt64(gRecaudosMasivos.numero_producto), pUsuario);
                                if (pAporteXaplicar && _lineasEsAporte)
                                {
                                    //Registrando Aporte x Aplicar
                                    pEntidad.numero_transaccion = 0;
                                    pEntidad.cod_ope = pCodOpe;
                                    pEntidad.cod_persona = gRecaudosMasivos.cod_cliente;
                                    pEntidad.numero_aporte = Convert.ToInt64(gRecaudosMasivos.numero_producto);
                                    pEntidad.cod_atr = 1;
                                    pEntidad.cod_det_list = Convert.ToInt32(gRecaudosMasivos.iddetalle);
                                    pEntidad.tipo_tran = 117;
                                    pEntidad.valor = gRecaudosMasivos.valor;
                                    pEntidad.estado = 1;
                                    pEntidad.num_tran_anula = null;
                                    pEntidad.numero_recaudo = gRecaudosMasivos.numero_recaudo;
                                    DARecaudosMasivos.CrearAportePendientes(pEntidad, pUsuario);
                                    gRecaudosMasivos.tipo_tran = 117;
                                }
                            }
                            // Esto se colocó porque en COOPERATIVA AVP no dejaba aplicar los aportes pendientes debito a que el tipo de producto lleva también el nombre de la linea. FerOrt. 12-Ago-2017.
                            if (!gRecaudosMasivos.tipo_producto.Trim().ToUpper().StartsWith("CREDITOS-CUOTAS EXTRAS"))
                                if (gRecaudosMasivos.tipo_producto.Contains("-"))
                                    if (gRecaudosMasivos.tipo_producto.Split('-').Count() > 0)
                                        if (gRecaudosMasivos.tipo_producto.Split('-')[0] != null)
                                            gRecaudosMasivos.tipo_producto = gRecaudosMasivos.tipo_producto.Split('-')[0].Trim();
                            // Realizar la aplicación del pago
                            DARecaudosMasivos.AplicarPago(gRecaudosMasivos, pOperacion.cod_ope, pUsuario, error, ref Error);
                            if (Error.Trim() == "")
                            {
                                gRecaudosMasivos.estadod = 1;
                                if (DARecaudosMasivos.ModificarDetalleRecaudo(gRecaudosMasivos, pUsuario, ref Error) == false)
                                    return false;
                            }
                        }
                    }

                    if (lstRecaudos[0].cod_ope != 0)
                    {
                        //Actualizando los Aportes Pendientes por Aplicar
                        AportePendientes pAplicar = new AportePendientes();
                        pAplicar.numero_recaudo = numero_aplicacion;
                        pAplicar.cod_ope = lstRecaudos[0].cod_ope;
                        pAplicar.estado = 2;
                        DARecaudosMasivos.AplicarAportePendientes(pAplicar, pUsuario);
                    }
                    if (DARecaudosMasivos.ModificarEstadoRecaudo(Convert.ToInt32(numero_aplicacion), fecha_reclamacion, 2, pUsuario, ref Error) == false)
                        return false;

                    ts.Complete();
                }
                return true;
            }
            catch (Exception ex)
            {
                Error += ex.Message;
                return false;
            }
        }

        public Boolean Validar(DateTime fecha_reclamacion, List<RecaudosMasivos> lstRecaudos, Usuario pUsuario, ref string Error)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    foreach (RecaudosMasivos gRecaudosMasivos in lstRecaudos)
                    {
                        DARecaudosMasivos.Validar(gRecaudosMasivos, pUsuario, ref Error);
                        if (Error.Trim() != "")
                        {
                            return false;
                        }
                    }
                    ts.Complete();
                    return true;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RecaudosMasivosBusiness", "Validar", ex);
                return false;
            }

        }

        /// <summary>
        /// Método para listar empresas recaudadoras
        /// </summary>
        /// <param name="pEmpresaRecaudo"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public List<EmpresaRecaudo> ListarEmpresaRecaudo(EmpresaRecaudo pEmpresaRecaudo, Usuario pUsuario)
        {
            try
            {
                return DARecaudosMasivos.ListarEmpresaRecaudo(pEmpresaRecaudo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RecaudosMasivosBusiness", "ListarEmpresaRecaudo", ex);
                return null;
            }
        }

        public List<RecaudosMasivos> ListadoProductosEnMora(DateTime pFecha, RecaudosMasivos ProductosEnMora, string filtros, Usuario pUsuario)
        {
            try
            {
                return DARecaudosMasivos.ListadoProductosEnMora(pFecha, ProductosEnMora, filtros, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RecuadosMasivosService", "ListadoProductosEnMora", ex);
                return null;
            }
        }



        public String GuardarRecaudos(RecaudosMasivos eRecaudo, DateTime pfecha_aplicacion, List<RecaudosMasivos> lstRecaudos, Usuario pUsuario, ref string Error)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, TimeSpan.MaxValue))
                {
                    EmpresaRecaudoData DAEmpresa = new EmpresaRecaudoData();
                    // Crear encabezado de archivo de recaudos
                    eRecaudo.numero_recaudo = DARecaudosMasivos.CrearRecaudo(eRecaudo, pUsuario, ref Error);

                    // Aplica las reclamaciones en cada crédito
                    int cont = 0;
                    foreach (RecaudosMasivos gRecaudosMasivos in lstRecaudos)
                    {
                        cont++;
                        gRecaudosMasivos.numero_recaudo = eRecaudo.numero_recaudo;
                        try
                        {
                            if (gRecaudosMasivos.nombre == null)
                                gRecaudosMasivos.nombre = " ";
                            DARecaudosMasivos.CrearDetalleRecaudo(gRecaudosMasivos, pUsuario, ref Error);
                        }
                        catch (Exception ex)
                        {
                            Error = "FILA " + cont + " " + ex.Message;
                        }
                    }

                    String Rpta = String.Empty;
                    EmpresaRecaudo pEmpReca = new EmpresaRecaudo();
                    pEmpReca.cod_empresa = eRecaudo.cod_empresa;
                    pEmpReca = DAEmpresa.ConsultarEMPRESARECAUDO(pEmpReca, pUsuario);
                    if (pEmpReca.aplicar_poroficina == 1)
                        Rpta = DARecaudosMasivos.AplicarRecaudoDividir(eRecaudo, pUsuario);
                    else
                        Rpta = eRecaudo.numero_recaudo.ToString();

                    ts.Complete();

                    return Rpta;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RecaudosMasivosBusiness", "GuardarRecaudos", ex);
                return null;
            }

        }


        public List<RecaudosMasivos> ListarRecaudo(RecaudosMasivos pRecaudosMasivos, Usuario pUsuario)
        {
            try
            {
                return DARecaudosMasivos.ListarRecaudo(pRecaudosMasivos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RecaudosMasivosBusiness", "ListarRecaudo", ex);
                return null;
            }
        }

        public List<RecaudosMasivos> ListarAportesPorAplicar(string pFiltro, Usuario pUsuario)
        {
            try
            {
                return DARecaudosMasivos.ListarAportesPorAplicar(pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RecaudosMasivosBusiness", "ListarAportesPorAplicar", ex);
                return null;
            }
        }

        public RecaudosMasivos ConsultarRecaudo(string pNumeroRecaudo, Usuario pUsuario)
        {
            try
            {
                return DARecaudosMasivos.ConsultarRecaudo(pNumeroRecaudo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RecaudosMasivosBusiness", "ConsultarRecaudo", ex);
                return null;
            }
        }

        public RecaudosMasivos ConsultarRecaudo(RecaudosMasivos pRecaudo, Usuario pUsuario)
        {
            try
            {
                return DARecaudosMasivos.ConsultarRecaudo(pRecaudo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RecaudosMasivosBusiness", "ConsultarRecaudo", ex);
                return null;
            }
        }

        public List<RecaudosMasivos> ListarDetalleRecaudo(int pNumeroRecaudo, Usuario pUsuario)
        {
            try
            {
                return DARecaudosMasivos.ListarDetalleRecaudo(pNumeroRecaudo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RecaudosMasivosBusiness", "ListarDetalleRecaudo", ex);
                return null;
            }
        }


        public List<RecaudosMasivos> ListarDetalleAportePendientes(string pFiltro, Usuario pUsuario)
        {
            try
            {
                return DARecaudosMasivos.ListarDetalleAportePendientes(pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RecaudosMasivosBusiness", "ListarDetalleAportePendientes", ex);
                return null;
            }
        }


        public List<RecaudosMasivos> ListarDetalleRecaudoConsulta(int pNumeroRecaudo, bool bDetallado, Usuario pUsuario)
        {
            try
            {
                return DARecaudosMasivos.ListarDetalleRecaudoConsulta(pNumeroRecaudo, bDetallado, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RecaudosMasivosBusiness", "ListarDetalleRecaudoConsulta", ex);
                return null;
            }
        }

        public void EliminarRecaudosMasivos(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DARecaudosMasivos.EliminarRecaudosMasivos(pId, pUsuario);
                    ts.Complete();
                    return;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RecaudosMasivosBusiness", "EliminarRecaudosMasivos", ex);
                return;
            }

        }


        public Persona1 ConsultarPersona(string recaudosmasivos, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    return DARecaudosMasivos.ConsultarPersona(recaudosmasivos, pUsuario);
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RecaudosMasivosBusiness", "ConsultarPersona", ex);
                return null;
            }
        }

        public int ConsultarProgresoRecaudos(long numero_reclamacion, Usuario usuario)
        {
            try
            {

                return DARecaudosMasivos.ConsultarProgresoRecaudos(numero_reclamacion, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RecaudosMasivosBusiness", "ConsultarProgresoRecaudos", ex);
                return 0;
            }
        }


        public List<RecaudosMasivos> ListarDetalleReporte(int pNumeroRecaudo, int pNumeroNovedad, Usuario pUsuario)
        {
            try
            {
                return DARecaudosMasivos.ListarDetalleReporte(pNumeroRecaudo, pNumeroNovedad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RecaudosMasivosBusiness", "ListarDetalleReporte", ex);
                return null;
            }
        }

        public void RegistrarError(int pNumeroLinea, string pRegistro, string pError, string pDato, ref List<Xpinn.Tesoreria.Entities.ErroresCarga> plstErrores)
        {
            if (pNumeroLinea == -1)
            {
                plstErrores.Clear();
                return;
            }
            Xpinn.Tesoreria.Entities.ErroresCarga registro = new Xpinn.Tesoreria.Entities.ErroresCarga();
            registro.numero_registro = pNumeroLinea.ToString();
            registro.datos = pDato;
            registro.error = " Campo No.:" + pRegistro + " Error:" + pError;
            plstErrores.Add(registro);
        }

        public List<RecaudosMasivos> ListarTEMP_Consolidado(RecaudosMasivos pRecaudos, ref string pError, Usuario vUsuario)
        {
            try
            {
                return DARecaudosMasivos.ListarTEMP_Consolidado(pRecaudos, ref pError, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RecaudosMasivosBusiness", "ListarTEMP_Consolidado", ex);
                return null;
            }
        }

        public void AplicarRecaudo(Int64 numero_recaudo, DateTime fechaaplicacion, Boolean pAportePend, ref Int64 pcod_ope, ref string Error, Usuario pUsuario)
        {
            try
            {
                DARecaudosMasivos.AplicarRecaudo(numero_recaudo, fechaaplicacion, pAportePend, ref pcod_ope, ref Error, pUsuario);
            }
            catch (Exception ex)
            {
                Error += ex.Message;
            }
        }

        public int RegistrosAplicados(Int64 pNumeroRecaudo, Usuario pUsuario)
        {
            return DARecaudosMasivos.RegistrosAplicados(pNumeroRecaudo, pUsuario);
        }

        //EXTRACTO

        public List<RecaudosMasivos> ListarRecaudoExtracto(RecaudosMasivos pRecaudosMasivos, Usuario pUsuario)
        {
            try
            {
                return DARecaudosMasivos.ListarRecaudoExtracto(pRecaudosMasivos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RecaudosMasivosBusiness", "ListarRecaudo", ex);
                return null;
            }
        }



        public List<RecaudosMasivos> ListarDetalleRecaudoConsultaExtracto(int pNumeroRecaudo, string estadoNom, bool bDetallado, Usuario pUsuario)
        {
            try
            {
                return DARecaudosMasivos.ListarDetalleRecaudoConsultaExtracto(pNumeroRecaudo, estadoNom, bDetallado, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RecaudosMasivosBusiness", "ListarDetalleRecaudoConsulta", ex);
                return null;
            }
        }


        public List<RecaudosMasivos> ListarDetalleRecaudoConsultaExtractoxPersona(RecaudosMasivos pRecaudosMasivos, Usuario pUsuario)
        {
            try
            {
                return DARecaudosMasivos.ListarDetalleRecaudoConsultaExtractoxPersona(pRecaudosMasivos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RecaudosMasivosBusiness", "ListarDetalleRecaudoConsulta", ex);
                return null;
            }
        }

        public List<RecaudosMasivos> ListarDeduccionesxPersona(RecaudosMasivos pRecaudos, ref string pError, Usuario vUsuario)
        {
            try
            {
                return DARecaudosMasivos.ListarDeduccionesxPersona(pRecaudos, ref pError, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RecaudosMasivosBusiness", "ListarTEMP_Consolidado", ex);
                return null;
            }
        }

    }
}
