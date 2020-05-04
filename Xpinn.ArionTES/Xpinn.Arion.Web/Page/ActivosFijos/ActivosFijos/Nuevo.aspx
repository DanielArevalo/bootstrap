<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Activos Fijos :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="Fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlNumeroConDecimales.ascx" TagName="numerodecimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlProcesoContable.ascx" TagName="procesoContable" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <script type="text/javascript">
        function ActiveTabChanged(sender, e) {
        }

        var HighlightAnimations = {};

        function Highlight(el) {
            if (HighlightAnimations[el.uniqueID] == null) {
                HighlightAnimations[el.uniqueID] = Sys.Extended.UI.Animation.createAnimation({
                    AnimationName: "color",
                    duration: 0.5,
                    property: "style",
                    propertyKey: "backgroundColor",
                    startValue: "#FFFF90",
                    endValue: "#FFFFFF"
                }, el);
            }
            HighlightAnimations[el.uniqueID].stop();
            HighlightAnimations[el.uniqueID].play();
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:Panel ID="panelGeneral" runat="server">
        <asp:MultiView ID="mvActivosFijos" runat="server" ActiveViewIndex="0">
            <asp:View ID="vwDatos" runat="server">
                <asp:Panel ID="pConsulta" runat="server" Width="100%">
                    <table id="tbCriterios" border="0" cellpadding="1" cellspacing="0" >
                        <tr>
                            <td style="text-align:left" colspan="6">
                                <strong>Datos Principales</strong>&nbsp;&nbsp;
                                <asp:Label ID="lblConsecutivo" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:left">
                                Código<br/>
                                <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" />
                            </td>
                            <td style="text-align:left">
                                Estado<br/>
                                <asp:Label ID="lblEstado" runat="server" Visible="False"></asp:Label>
                                <asp:TextBox ID="txtEstado" runat="server" CssClass="textbox" Enabled="False" 
                                    Width="110px" />
                            </td>
                            <td colspan="3" style="text-align:left">
                                Serial<br/>
                                <asp:TextBox ID="txtSerial" runat="server" CssClass="textbox" Width="360px" />
                            </td>
                            <td class="tdD">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="text-align:left" colspan="2">
                                 Nombre<br />
                                 <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Width="280px" />                             
                            </td>
                            <td style="text-align: left" colspan="2">
                                Clase<br />
                                <asp:DropDownList ID="ddlClase" runat="server" CssClass="textbox" Width="95%" AppendDataBoundItems="True">
                                    <asp:ListItem Value="0">Seleccion un Item</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="text-align:left">
                                Tipo<br />
                                <asp:DropDownList ID="ddlTipo" runat="server" CssClass="textbox" Width="165px" AppendDataBoundItems="True">
                                    <asp:ListItem Value="0">Seleccion un Item</asp:ListItem>
                                </asp:DropDownList>                            
                            </td>
                            <td style="text-align:left">
                                Uso del Bien<br />
                                <asp:DropDownList ID="ddlUsoBien" runat="server" CssClass="textbox" Width="165px" AppendDataBoundItems="True">
                                    <asp:ListItem Value="1">En arriendo</asp:ListItem>
                                    <asp:ListItem Value="2">Uso de la entidad</asp:ListItem>
                                    <asp:ListItem Value="3">Desocupado</asp:ListItem>
                                    <asp:ListItem Value="4">Para la venta</asp:ListItem>
                                    <asp:ListItem Value="5">Recreación</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left" colspan="2">
                                Ubicación<br />
                                <asp:DropDownList ID="ddlUbicacion" runat="server" CssClass="textbox" Width="290px"
                                    AppendDataBoundItems="True">
                                    <asp:ListItem Value="0">Seleccion un Item</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="text-align:left" colspan="2">
                                Centro de Costo<br />
                                <asp:DropDownList ID="ddlCentroCosto" runat="server" CssClass="textbox" 
                                    Width="95%" AppendDataBoundItems="True">
                                    <asp:ListItem Value="0">Seleccion un Item</asp:ListItem>
                                </asp:DropDownList>                           
                            </td>
                            <td style="text-align:left">
                                Oficina<br />
                                <asp:DropDownList ID="ddlOficina" runat="server" CssClass="textbox" 
                                    Width="165px" AppendDataBoundItems="True">
                                    <asp:ListItem Value="0">Seleccion un Item</asp:ListItem>
                                </asp:DropDownList>                            
                            </td>
                            <td style="text-align:left">
                                Fec.Ult.Deprec.<br />
                                <uc1:fecha ID="txtFechaDepreciacion" runat="server" 
                                    style="font-size:xx-small; text-align:right" TipoLetra="XX-Small" 
                                    Enabled="True" Width_="80" />  
                             </td>
                        </tr>
                        <tr>
                            <td style="text-align:left" colspan="2">
                                Encargado<br />
                                <asp:TextBox ID="txtCodEncargado" runat="server" CssClass="textbox" Width="50px" ReadOnly="True" Visible="false" />
                                <asp:TextBox ID="txtIdenEncargado" runat="server" CssClass="textbox" Width="50px" ReadOnly="True" Visible="false" />
                                <asp:TextBox ID="txtNomEncargado" runat="server" CssClass="textbox" Width="250px" ReadOnly="True" />
                                <asp:Button ID="btnConsultaPersonas" CssClass="btn8" runat="server" Text="..." 
                                    Height="26px" onclick="btnConsultaPersonas_Click" />
                                <uc1:ListadoPersonas id="ctlBusquedaPersonas" runat="server" />                            
                            </td>
                            <td style="text-align:left">
                                Vida Util (Años)<br />
                                <asp:TextBox ID="txtVidaUtil" runat="server" CssClass="textbox" Width="80px" />
                                <asp:FilteredTextBoxExtender ID="txtVidaUtil_FilteredTextBoxExtender" runat="server" Enabled="True" 
                                    FilterType="Numbers, Custom" TargetControlID="txtVidaUtil" ValidChars=".," />
                            </td>
                            <td style="text-align:left">
                                Valor del Avalúo<br />
                                <uc1:decimales ID="txtValorAvaluo" runat="server" 
                                    style="font-size:xx-small; text-align:right" TipoLetra="XX-Small" 
                                    Enabled="True" Width_="80" />
                            </td>
                            <td style="text-align:left">
                                Valor de Salvamento<br /> 
                                <uc1:decimales ID="txtValorSalvamento" runat="server" 
                                    style="font-size:xx-small; text-align:right" TipoLetra="XX-Small" 
                                    Enabled="True" Width_="80" />
                            </td>
                            <td style="text-align:left">
                                 <asp:Label ID="lblacumulado" runat="server" Text="Acum.Depreciación"></asp:Label>

                             
                              <br />
                               <uc1:numerodecimales ID="txtAcumuladoDepreciacion"  runat="server" 
                                    style="font-size:xx-small; text-align:right" TipoLetra="XX-Small" 
                                    Enabled="True" Width_="80" />
                            </td>
                        </tr>
                    </table>
                    &nbsp;
                     <table border="0" cellpadding="1" cellspacing="0" width="100%">
                        <tr>
                            <td style="text-align:left" colspan="5">
                                <strong>Datos NIIF</strong>                            
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:left" colspan="5">
                                <table border="0" cellpadding="1" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="text-align: left" width="20%">
                                            Clasificación<br />
                                            <asp:DropDownList ID="ddlClasificacion" runat="server" Width="90%" CssClass="textbox"
                                                AppendDataBoundItems="True">
                                                <asp:ListItem Value="0">Seleccion un Item</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: left" width="20%">
                                            Tipo de Activos<br />
                                            <asp:DropDownList ID="ddltipo_Activo" runat="server" CssClass="textbox" Width="90%"
                                                AppendDataBoundItems="True">
                                                <asp:ListItem Value="0">Seleccion un Item</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: left" width="20%">
                                            Metodo de Costeo<br />
                                            <asp:DropDownList ID="ddlmetodo_costo" runat="server" Width="90%" CssClass="textbox">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: left" colspan="2" width="20%">
                                             Unidad Generadora Efectivo <br/>
                                            <asp:DropDownList ID="ddlUniGenerado" runat="server" Width="60%" 
                                                CssClass="textbox" AppendDataBoundItems="True">
                                                <asp:ListItem Value="0">Seleccion un Item</asp:ListItem>
                                            </asp:DropDownList>    
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left" width="20%">
                                            Valor de Activo<br/>
                                           <uc1:decimales ID="txtValorNiif" runat="server" 
                                            style="font-size:xx-small; text-align:right" TipoLetra="XX-Small" 
                                            Enabled="True" Width_="110" />
                                        </td>
                                        <td style="text-align: left" width="20%">
                                            Vida Util(Años)<br />
                                            <asp:TextBox ID="txtvidaNiif" runat="server" CssClass="textbox" Width="80px" />
                                            <asp:FilteredTextBoxExtender ID="fteJalo" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                                TargetControlID="txtvidaNiif" ValidChars=".," />
                                        </td>
                                        <td style="text-align: left" width="20%">
                                            Valor Residual<br/>
                                           <uc1:decimales ID="txtVrResidualNiif" runat="server" 
                                            style="font-size:xx-small; text-align:right" TipoLetra="XX-Small" 
                                            Enabled="True" Width_="110" />
                                        </td>
                                        <td style="text-align: left" width="20%">
                                            Vr. Residual % <br/>
                                           <uc1:decimales ID="txtVrResidualPor" runat="server" 
                                            style="font-size:xx-small; text-align:right" TipoLetra="XX-Small" 
                                            Enabled="True" Width_="50" />
                                        </td>
                                        <td style="text-align: left" width="20%">Vr. Deterioro<br />
                                            <uc1:numerodecimales ID="txtvrDeterioro" runat="server"
                                                style="font-size: xx-small; text-align: right" TipoLetra="XX-Small"
                                                Enabled="True" Largo="80" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left" width="20%">
                                            (+) Adiciones<br/>
                                           <uc1:numerodecimales ID="txtadicionesNiif" runat="server" 
                                            style="font-size:xx-small; text-align:right" TipoLetra="XX-Small" 
                                            Enabled="True" Largo="110" />
                                        </td>
                                        <td style="text-align: left" width="20%">
                                            Fec Ultima Adición<br/>
                                            <uc1:fecha ID="txtFec_ult_adicion" runat="server"
                                                style="font-size: xx-small; text-align: right" TipoLetra="XX-Small"
                                                Enabled="false" Width_="80" />
                                        </td>
                                        <td style="text-align: left" width="20%">
                                            Rec.Deterioro<br/>
                                           <uc1:numerodecimales ID="txtrecdeterioroNiif" runat="server" 
                                            style="font-size:xx-small; text-align:right" TipoLetra="XX-Small" 
                                            Enabled="True" Largo="110" />
                                        </td>
                                        <td style="text-align: left">
                                            Revaluación<br/>
                                           <uc1:numerodecimales ID="txtrevaluacionNiif" runat="server" 
                                            style="font-size:xx-small; text-align:right" TipoLetra="XX-Small" 
                                            Enabled="True" Largo="70" />
                                        </td>
                                        <td style="text-align: left">
                                            Rev. Revaluación<br/>
                                            <uc1:numerodecimales ID="txtrevRevaluacion" runat="server" 
                                            style="font-size:xx-small; text-align:right" TipoLetra="XX-Small" 
                                            Enabled="True" Largo="70" />
                                        </td>
                                    </tr>                       
                                </table>
                            </td>
                        </tr>                   
                    </table>
                     &nbsp;
                    <table id="Table1" border="0" cellpadding="1" cellspacing="0" width="50%">
                        <tr>
                            <td style="text-align:left" colspan="3">
                                <strong>Datos de Compra</strong>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:left">
                                Número de Factura<br/>
                                <asp:TextBox ID="txtFactura" runat="server" CssClass="textbox" Width="120px" />
                                <asp:FilteredTextBoxExtender ID="fteFactura" runat="server" Enabled="True" 
                                    FilterType="Numbers, Custom" TargetControlID="txtFactura" ValidChars="" />
                            </td>
                            <td style="text-align:left" colspan="2">
                                Proveedor<br/>
                                <asp:TextBox ID="txtCodProveedor" runat="server" CssClass="textbox" Width="50px" ReadOnly="True" Visible="false" />
                                <asp:TextBox ID="txtIdeProveedor" runat="server" CssClass="textbox" Width="50px" ReadOnly="True" Visible="false" />
                                <asp:TextBox ID="txtNomProveedor" runat="server" CssClass="textbox" Width="260px" ReadOnly="True" />
                                <asp:Button ID="btnProveedor" CssClass="btn8" runat="server" Text="..." 
                                    Height="26px" onclick="btnProveedor_Click" />
                                <uc1:ListadoPersonas id="ctlBusquedaProveedor" runat="server" />                            
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:left">
                                Valor de la Compra<br /> 
                                <uc1:decimales ID="txtValorCompra" runat="server" 
                                    style="font-size:xx-small; text-align:right" TipoLetra="XX-Small" 
                                    Enabled="True" Width_="80" />
                            </td>
                            <td style="text-align:left">
                                Fecha de Compra<br />
                                <uc1:fecha ID="txtFechaCompra" runat="server" 
                                    style="font-size:xx-small; text-align:right" TipoLetra="XX-Small" 
                                    Enabled="True" Width_="80" />                            
                            </td>
                            <td style="text-align:left">
                                Moneda<br />
                                <asp:DropDownList ID="ddlmoneda" runat="server" Width="200px" CssClass="textbox"
                                    AppendDataBoundItems="True">
                                    <asp:ListItem Value="0">Seleccion un Item</asp:ListItem>
                                </asp:DropDownList>                      
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:left" colspan="2">
                                Observaciones
                                <br />
                                <asp:TextBox ID="txtObservacion" TextMode="MultiLine" runat="server" Width="100%"></asp:TextBox>                        
                            </td>
                        </tr>
                    </table>
                    <table id="Table2" border="0" cellpadding="1" cellspacing="0" width="100%">
                        <tr>
                            <td style="text-align:left" colspan="2">
                                <strong>Datos Adicionales</strong>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:left" colspan="2">
                                <asp:TabContainer runat="server" ID="TabsDetalle" 
                                    OnClientActiveTabChanged="ActiveTabChanged" ActiveTabIndex="2" 
                                    style="margin-right: 5px" CssClass="CustomTabStyle" >
                                    <asp:TabPanel ID="tabDatosAdicionales" runat="server" 
                                        HeaderText="Datos Adicionales"><HeaderTemplate>Datos Adicionales
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <table id="Table3" border="0" cellpadding="1" cellspacing="0" width="70%">
                                            <tr>
                                                <td style="text-align:left">
                                                    <table id="Table4" border="0" cellpadding="1" cellspacing="0" width="50%">
                                                        <tr>
                                                            <td colspan="2" style="text-align:left">
                                                                <strong>Inmuebles</strong> 
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align:left">
                                                                Matrícula<br /> &#160;&#160;&#160;
                                                                <asp:TextBox ID="txtMatricula" runat="server" CssClass="textbox" Width="120px" />
                                                            </td>
                                                            <td style="text-align:left">
                                                                Escritura<br /> &#160;&#160;&#160;
                                                                <asp:TextBox ID="txtEscritura" runat="server" CssClass="textbox" Width="120px" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align:left">
                                                                Notaria<br /> &#160;&#160;&#160;
                                                                <asp:TextBox ID="txtNotaria" runat="server" CssClass="textbox" Width="120px" />
                                                            </td>
                                                            <td style="text-align:left">
                                                                Dirección<br /> &#160;&#160;&#160;
                                                                <asp:TextBox ID="txtDireccion" runat="server" CssClass="textbox" Width="120px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="text-align:left">
                                                    <table id="Table5" border="0" cellpadding="1" cellspacing="0" width="50%">
                                                        <tr>
                                                            <td colspan="2" style="text-align:left">
                                                                <strong>Equipo/Maquinaria</strong> 
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align:left">
                                                                Modelo<br /> &#160;&#160;&#160;
                                                                <asp:TextBox ID="txtModelo" runat="server" CssClass="textbox" Width="120px" />
                                                            </td>
                                                            <td style="text-align:left">
                                                                Marca<br /> &#160;&#160;&#160;
                                                                <asp:TextBox ID="txtMarca" runat="server" CssClass="textbox" Width="120px" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align:left">
                                                                No.Motor<br /> &#160;&#160;&#160;
                                                                <asp:TextBox ID="txtMotor" runat="server" CssClass="textbox" Width="120px" />
                                                            </td>
                                                            <td style="text-align:left">
                                                                Placa<br /> &#160;&#160;&#160;
                                                                <asp:TextBox ID="txtPlaca" runat="server" CssClass="textbox" Width="120px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                    </asp:TabPanel>
                                    <asp:TabPanel ID="tabPolizaSeguros" runat="server" HeaderText="Poliza Seguros"><HeaderTemplate>Poliza de Seguros
                                    </HeaderTemplate>
                                        <ContentTemplate>
                                            <table id="Table6" border="0" cellpadding="1" cellspacing="0" width="50%">
                                                <tr>
                                                    <td colspan="2" style="text-align: left">
                                                        <strong>Poliza de Seguro</strong>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" style="text-align: left">
                                                        Número<br />
                                                        &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtNumeroPol" runat="server" CssClass="textbox"
                                                            Width="120px" /><asp:FilteredTextBoxExtender ID="ftbNumeroPol" runat="server" Enabled="True"
                                                                FilterType="Custom, Numbers" TargetControlID="txtNumeroPol" ValidChars="." />
                                                    </td>
                                                    <td style="text-align: left">
                                                        Estado<br />
                                                        &nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlEstadoPol" runat="server" AppendDataBoundItems="True"
                                                            CssClass="textbox" Width="129px">
                                                            <asp:ListItem></asp:ListItem>
                                                            <asp:ListItem Value="1" Selected="True">Activa</asp:ListItem>
                                                            <asp:ListItem Value="2">Inactiva</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" style="text-align: left">
                                                        Nombre<br />
                                                        &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtNombrePol" runat="server" CssClass="textbox"
                                                            Width="120px" />
                                                    </td>
                                                    <td style="text-align: left">
                                                        Valor<br />
                                                        &nbsp;&nbsp;&nbsp;<uc1:decimales ID="txtValorPol" runat="server" Enabled="True" Habilitado="True"
                                                            style="font-size: xx-small; text-align: right" TipoLetra="XX-Small" Width_="80" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        Fecha Poliza<br />
                                                        &nbsp;&nbsp;&nbsp;<uc1:fecha ID="txtFechaPoliza" runat="server" Enabled="True" Habilitado="True"
                                                            style="font-size: xx-small; text-align: right" TipoLetra="XX-Small" Width_="80" />
                                                    </td>
                                                    <td style="text-align: left">
                                                        Fecha Vigencia<br />
                                                        &nbsp;&nbsp;&nbsp;<uc1:fecha ID="txtFechaVigencia" runat="server" Enabled="True"
                                                            Habilitado="True" style="font-size: xx-small; text-align: right" TipoLetra="XX-Small"
                                                            Width_="80" />
                                                    </td>
                                                    <td style="text-align: left">
                                                        Fecha de Garantía<br />
                                                        &nbsp;&nbsp;&nbsp;<uc1:fecha ID="txtFechaGarantia" runat="server" Enabled="True"
                                                            Habilitado="True" style="font-size: xx-small; text-align: right" TipoLetra="XX-Small"
                                                            Width_="80" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <asp:TabPanel ID="tabLeasing" runat="server" HeaderText="Leasing"><HeaderTemplate>Leasing
                                    </HeaderTemplate>
                                        <ContentTemplate>
                                            <table border="0" cellpadding="1" cellspacing="0" style="width: -2147483648%">
                                                <tr>
                                                    <td colspan="3" style="text-align: left">
                                                        <strong>LEASING</strong>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        Numero de Pagos<br />
                                                        &#160;&#160;&#160;<asp:TextBox ID="txtNumPagos" runat="server" CssClass="textbox"
                                                            Width="120px" /><asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                                Enabled="True" FilterType="Custom, Numbers" TargetControlID="txtNumPagos" ValidChars="." />
                                                    </td>
                                                    <td style="text-align: left">
                                                        Valor de Cuenta<br />
                                                        &#160;&#160;&#160;<uc1:decimales ID="txtvalorLeasing" runat="server" Enabled="True"
                                                            Habilitado="True" style="font-size: xx-small; text-align: right" TipoLetra="XX-Small"
                                                            Width_="80" />
                                                    </td>
                                                    <td style="text-align: left">
                                                        Periodicidad de Pago<br />
                                                        &nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlPeriodicidad" runat="server" AppendDataBoundItems="True"
                                                            CssClass="textbox" Width="134px">
                                                            <asp:ListItem Value="0">Seleccion un Item</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Valor opción compra<br />
                                                        &#160;&#160;&#160;<uc1:decimales ID="txtValorCompLeasing" runat="server" Enabled="True"
                                                            Habilitado="True" style="font-size: xx-small; text-align: right" TipoLetra="XX-Small"
                                                            Width_="80" />
                                                    </td>
                                                    <td>
                                                        &#160;
                                                    </td>
                                                    <td>
                                                        &#160;
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                </asp:TabContainer>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </asp:View>
            <asp:View ID="mvFinal" runat="server">
                <asp:Panel id="PanelFinal" runat="server">
                    <table style="width: 100%;">
                        <tr>
                            <td style="text-align: center; font-size: large;">
                                <br /><br /><br /><br />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center; font-size: large;">
                                <asp:Label ID="lblMensaje" runat="server" 
                                    Text="Datos Grabados Correctamente" style="color: #FF3300"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center; font-size: large;">
                                <asp:Button ID="Btncomprobante" Text="Generar Comprobante" runat="server" OnClick="generar_Onclick"/>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </asp:View>
        </asp:MultiView>
     </asp:Panel>
    <asp:Panel ID="panelProceso" runat="server" Width="100%">
        <uc2:procesoContable ID="ctlproceso" runat="server" />  
    </asp:Panel> 

    <uc1:mensajeGrabar ID="ctlMensaje" runat="server"/>

</asp:Content>