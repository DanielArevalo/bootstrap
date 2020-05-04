using System;
using System.Collections.Generic;
using System.Net;
using System.Web;
using Newtonsoft.Json;
using Xpinn.Interfaces.Entities;
using Xpinn.Interfaces.Services;
using Xpinn.Util;

/// <summary>
/// Summary description for InterfazRiesgo
/// </summary>
public class InterfazRIESGO
{
    TradeUSServices _tradeService = new TradeUSServices();    
    public bool existeEnOFAC;

    public InterfazRIESGO()
    {
        existeEnOFAC = false;
    }

    public TradeUSSearchResults ConsultarPersonaOFAQ(string pIdentificacion, string pNombre)
    {
        TradeUSSearchResults tradeResult = null;
        try
        {
            tradeResult = _tradeService.ConsultarPersonaBuscadaPorIdentificacion(pNombre, pIdentificacion);
        }
        catch (Exception)
        {
            return null;
        }
        return tradeResult;
    }

    public TradeUSSearchResults ConsultarPersonaRiesgo(string pIdentificacion, string pNombre)
    {
        TradeUSSearchResults tradeResult = null;
        try
        {
            tradeResult = _tradeService.ConsultarPersonaBuscadaPorIdentificacion(pNombre, pIdentificacion);
        }
        catch (Exception)
        {
            return null;
        }
        return tradeResult;
    }

    public string ConsultarPersona(string pIdentificacion, string pNombre)
    {
        TradeUSSearchResults tradeResult = null;
        try
        {
            tradeResult = _tradeService.ConsultarPersonaBuscadaPorIdentificacion(pNombre, pIdentificacion);
        }
        catch (Exception)
        {
            return "";
        }
        return tradeResult.search_performed_at;
    }

    /// <summary>
    /// Método para consultar personas o entidades registradas en lista CS/ONU
    /// </summary>
    /// <param name="pIdentificacion">No Identificacion</param>
    /// <param name="pNombre">Nombre completo persona</param>
    /// <param name="tipo_persona">Persona Natural o Juridica</param>
    /// <param name="PersonaIndividual">Entidad con datos de la persona natural registrada en la lista</param>
    /// <param name="PersonaEntidad">Entidad con datos de la persona juridica registrada en la lista</param>
    public void ConsultarPersonaCSONU(string pIdentificacion, string pNombre, string tipo_persona, ref Individual PersonaIndividual, ref Entity PersonaEntidad)
    {
        try
        {
            _tradeService.ListaConsolidadaCSONU(pNombre, pIdentificacion, tipo_persona, ref PersonaIndividual, ref PersonaEntidad);
        }
        catch (Exception ex)
        {

        }
    }    
}