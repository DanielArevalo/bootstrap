using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Xpinn.Util;
using Xpinn.CDATS.Services;
/// <summary>
/// Descripción breve de NumeracionCuentas
/// </summary>
public class NumeracionCuentas
{
    AperturaCDATService AperturaService = new AperturaCDATService();
    Validadores BOValidacion = new Validadores();

	public NumeracionCuentas()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}

    protected Int64 ObtenerAcumulador(List<Xpinn.Programado.Entities.CuentasProgramado> lstNumeracion, int posicion)
    {
        Int64 pValorAcumulado = 0;
        foreach (Xpinn.Programado.Entities.CuentasProgramado xNum in lstNumeracion)
        {
            if (xNum.posicion < posicion)
                pValorAcumulado += xNum.longitud;
        }
        return pValorAcumulado;
    }


    public string ObtenerCodigoParametrizado(int pTipoProducto, string pIdentificacion, Int64? pCodPersona, string pCodLinea, ref string pError, Usuario pUsuario)
    {
        try
        {
            String cadena = "";
            Int32 longitud = 0;
            String valorfijo = "";
            String consecutivo = "";
            String consecutivoOficina = "";
            String consecutivoCliente = "";
            String consecutivoLinea = "";
            String oficina = pUsuario.cod_oficina.ToString();
            String identificacion = pIdentificacion == null || pIdentificacion.Trim() == "" ? null : pIdentificacion;
            String codpersona = pCodPersona == null || pCodPersona == 0 ? null : pCodPersona.ToString();
            String codlinea = pCodLinea == null || pCodLinea.Trim() == "" ? null : pCodLinea.Trim();
            Char caracter_llenado;
            string ConsultaSql = "";

            Xpinn.Programado.Services.CuentasProgramadoServices CuentasPrograServicios = new Xpinn.Programado.Services.CuentasProgramadoServices();

            //RECUPERAR DATOS 
            List<Xpinn.Programado.Entities.CuentasProgramado> LstNumeracion = new List<Xpinn.Programado.Entities.CuentasProgramado>();
            Xpinn.Programado.Entities.CuentasProgramado pNum = new Xpinn.Programado.Entities.CuentasProgramado();
            pNum.tipo_producto = pTipoProducto;
            LstNumeracion = CuentasPrograServicios.ListarParametrizacionCuentas(pNum, pUsuario);
            if (LstNumeracion.Count == 0)
            {
                pError = "No existen parametrización para la numeración de cuentas";
                return null;
            }
            //OBTENIENDO TOTAL DE LONGITUD TIPO DATO CONSECUTIVOS
            Int64 Acumulador = 0, AcumuOficina = 0, AcumuCliente = 0,AcumuLinea = 0;
            foreach (Xpinn.Programado.Entities.CuentasProgramado numeracion in LstNumeracion)
            {
                if (numeracion.posicion > 0)
                {
                    if (numeracion.tipo_campo == 5 || numeracion.tipo_campo == 6 || numeracion.tipo_campo == 7 || numeracion.tipo_campo == 8)
                    {
                        //OBTENER LA SUMATORIA DE LONGITUD A EXTRAER
                        if (numeracion.tipo_campo == 5)
                            Acumulador = ObtenerAcumulador(LstNumeracion, numeracion.posicion);
                        if (numeracion.tipo_campo == 6)
                            AcumuOficina = ObtenerAcumulador(LstNumeracion, numeracion.posicion);
                        if (numeracion.tipo_campo == 7)
                            AcumuCliente = ObtenerAcumulador(LstNumeracion, numeracion.posicion);
                        if (numeracion.tipo_campo == 8)
                            AcumuLinea = ObtenerAcumulador(LstNumeracion, numeracion.posicion);
                    }
                }
            }

            //CONSTRUYENDO EL NUMERO PARAMETRIZADO
            foreach (Xpinn.Programado.Entities.CuentasProgramado numeracion in LstNumeracion)
            {
                longitud = numeracion.longitud;
                caracter_llenado = Convert.ToChar(numeracion.caracter_llenado);
                if (numeracion.posicion > 0)
                {
                    if (numeracion.tipo_campo == 0)
                    {
                        //("VALOR_FIJO"
                        valorfijo = Convert.ToString(numeracion.valor);
                        if (numeracion.alinear == "D")
                            cadena += valorfijo.PadLeft(longitud, caracter_llenado);
                        else
                            cadena += valorfijo.PadRight(longitud, caracter_llenado);
                    }

                    //OFICINA
                    if (numeracion.tipo_campo == 1)
                    {
                        if (numeracion.alinear == "D")
                            cadena += oficina.PadLeft(longitud, caracter_llenado);
                        else
                            cadena += oficina.PadRight(longitud, caracter_llenado);
                    }

                    //IDENTIFICACION
                    if (numeracion.tipo_campo == 2)
                    {
                        if (identificacion != null)
                        {
                            if (numeracion.alinear == "D")
                                cadena += identificacion.PadLeft(longitud, caracter_llenado);
                            else
                                cadena += identificacion.PadRight(longitud, caracter_llenado);
                        }
                    }

                    //CODPERSONA
                    if (numeracion.tipo_campo == 3)
                    {
                        if (codpersona != null)
                        {
                            if (numeracion.alinear == "D")
                                cadena += codpersona.PadLeft(longitud, caracter_llenado);
                            else
                                cadena += codpersona.PadRight(longitud, caracter_llenado);
                        }
                    }

                    //CODLINEA
                    if (numeracion.tipo_campo == 4)
                    {
                        if (codlinea != null)
                        {
                            if (numeracion.alinear == "D")
                                cadena += codlinea.PadLeft(longitud, caracter_llenado);
                            else
                                cadena += codlinea.PadRight(longitud, caracter_llenado);
                        }
                    }

                    //CONSECUTIVO
                    if (numeracion.tipo_campo == 5)
                    {
                        //Invocar siguiente consecutivo
                        if (pTipoProducto == 1)
                            ConsultaSql = " SELECT MAX(TO_NUMBER(SUBSTR(NUMERO_CUENTA," + (Acumulador + 1) + "," + longitud + "))) FROM AHORRO_VISTA";
                        else if (pTipoProducto == 2)
                            ConsultaSql = " SELECT MAX(TO_NUMBER(SUBSTR(NUMERO_PROGRAMADO," + (Acumulador + 1) + "," + longitud + "))) FROM AHORRO_PROGRAMADO";
                        else if (pTipoProducto == 3)
                            ConsultaSql = " SELECT MAX(TO_NUMBER(SUBSTR(NUMERO_CDAT," + (Acumulador + 1) + "," + longitud + "))) FROM CDAT";
                        
                        consecutivo = AperturaService.ObtenerConsecutivo(ConsultaSql, pUsuario).ToString();
                        //Incrementando el consecutivo
                        consecutivo = (Convert.ToInt32(consecutivo) + 1).ToString();
                        if (!BOValidacion.IsValidNumber(consecutivo))
                        {
                            pError = "Se generó un error al construir el consecutivo";
                            return null;
                        }
                        if (numeracion.alinear == "D")
                            cadena += consecutivo.PadLeft(longitud, caracter_llenado);
                        else
                            cadena += consecutivo.PadRight(longitud, caracter_llenado);
                    }

                    //CONSECUTIVO OFICINA
                    if (numeracion.tipo_campo == 6)
                    {
                        if (pTipoProducto == 1)
                            ConsultaSql = " SELECT MAX(TO_NUMBER(SUBSTR(NUMERO_CUENTA," + (AcumuOficina + 1) + "," + longitud + "))) FROM AHORRO_VISTA WHERE COD_OFICINA = " + pUsuario.cod_oficina;
                        else if (pTipoProducto == 2)
                            ConsultaSql = " SELECT MAX(TO_NUMBER(SUBSTR(NUMERO_PROGRAMADO," + (AcumuOficina + 1) + "," + longitud + "))) FROM AHORRO_PROGRAMADO WHERE COD_OFICINA = " + pUsuario.cod_oficina;
                        else if (pTipoProducto == 3)
                            ConsultaSql = " SELECT MAX(TO_NUMBER(SUBSTR(NUMERO_CDAT," + (AcumuOficina + 1) + "," + longitud + "))) FROM CDAT WHERE COD_OFICINA = " + pUsuario.cod_oficina;
                        consecutivoOficina = AperturaService.ObtenerConsecutivo(ConsultaSql, pUsuario).ToString();
                        //Incrementando el consecutivo
                        consecutivoOficina = (Convert.ToInt32(consecutivoOficina) + 1).ToString();
                        if (!BOValidacion.IsValidNumber(consecutivoOficina))
                        {
                            pError = "Se generó un error al construir el consecutivo";
                            return null;
                        }
                        if (numeracion.alinear == "D")
                            cadena += consecutivoOficina.PadLeft(longitud, caracter_llenado);
                        else
                            cadena += consecutivoOficina.PadRight(longitud, caracter_llenado);
                    }

                    //CONSECUTIVO CLIENTE
                    if (numeracion.tipo_campo == 7)
                    {
                        if (pTipoProducto == 1)
                            ConsultaSql = " SELECT MAX(TO_NUMBER(SUBSTR(NUMERO_CUENTA," + (AcumuCliente + 1) + "," + longitud + "))) FROM AHORRO_VISTA WHERE COD_PERSONA = " + pCodPersona;
                        else if (pTipoProducto == 2)
                            ConsultaSql = " SELECT MAX(TO_NUMBER(SUBSTR(NUMERO_PROGRAMADO," + (AcumuCliente + 1) + "," + longitud + "))) FROM AHORRO_PROGRAMADO WHERE COD_PERSONA = " + pCodPersona;
                        else if (pTipoProducto == 3)
                        {
                            ConsultaSql = " SELECT MAX(TO_NUMBER(SUBSTR(C.NUMERO_CDAT," + (AcumuCliente + 1) + "," + longitud + @"))) 
                                        FROM CDAT C INNER JOIN CDAT_TITULAR T 
                                        ON T.CODIGO_CDAT = C.CODIGO_CDAT WHERE T.COD_PERSONA = " + codpersona;
                        }
                        consecutivoCliente = AperturaService.ObtenerConsecutivo(ConsultaSql, pUsuario).ToString();
                        //Incrementando el consecutivo
                        consecutivoCliente = (Convert.ToInt32(consecutivoCliente) + 1).ToString();
                        if (!BOValidacion.IsValidNumber(consecutivoCliente))
                        {
                            pError = "Se generó un error al construir el consecutivo";
                            return null;
                        }
                        if (numeracion.alinear == "D")
                            cadena += consecutivoCliente.PadLeft(longitud, caracter_llenado);
                        else
                            cadena += consecutivoCliente.PadRight(longitud, caracter_llenado);
                    }

                    //CONSECUTIVO LINEA
                    if (numeracion.tipo_campo == 8)
                    {
                        if (pTipoProducto == 1)
                            ConsultaSql = " SELECT MAX(TO_NUMBER(SUBSTR(NUMERO_CUENTA," + (AcumuLinea + 1) + "," + longitud + "))) FROM AHORRO_VISTA WHERE COD_LINEA_AHORRO = '" + codlinea + "'";
                        else if (pTipoProducto == 2)
                            ConsultaSql = " SELECT MAX(TO_NUMBER(SUBSTR(NUMERO_PROGRAMADO," + (AcumuLinea + 1) + "," + longitud + "))) FROM AHORRO_PROGRAMADO WHERE COD_LINEA_PROGRAMADO = '" + codlinea + "'";
                        else if (pTipoProducto == 3)
                        {
                            ConsultaSql = " SELECT MAX(TO_NUMBER(SUBSTR(NUMERO_CDAT," + (AcumuLinea + 1) + "," + longitud + "))) FROM CDAT WHERE COD_LINEACDAT = '" + codlinea + "'";
                        }
                        consecutivoLinea = AperturaService.ObtenerConsecutivo(ConsultaSql, pUsuario).ToString();
                        //Incrementando el consecutivo
                        consecutivoLinea = (Convert.ToInt32(consecutivoLinea) + 1).ToString();
                        if (!BOValidacion.IsValidNumber(consecutivoLinea))
                        {
                            pError = "Se generó un error al construir el consecutivo";
                            return null;
                        }
                        if (numeracion.alinear == "D")
                            cadena += consecutivoLinea.PadLeft(longitud, caracter_llenado);
                        else
                            cadena += consecutivoLinea.PadRight(longitud, caracter_llenado);
                    }
                }
            }
            return cadena;
        }
        catch (Exception ex)
        {
            pError = ex.Message;
            return "ErrorGeneracion";
        }
    }


}