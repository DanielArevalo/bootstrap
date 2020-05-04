<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Src="~/General/Controles/ctlLineaAhorro.ascx" TagName="ddlLineaAhorro" TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlOficina.ascx" TagName="ddlOficina" TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/ctlDestinacion.ascx" TagName="ddlDestinacion" TagPrefix="ctl" %>
<%@ Register Src="~/General/Controles/ctlPersona.ascx" TagName="ddlPersona" TagPrefix="ctl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvAhorroVista" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <asp:Panel ID="pConsulta" runat="server">
                <table cellpadding="5" cellspacing="0" style="width: 70%">
                    <tr>
                        <td style="text-align: left; width: 183px;">
                            <strong style="color: rgb(64, 65, 66); font-family: 'Segoe UI', Arial, Helvetica, Verdana, sans-serif;
                                font-size: 14px; font-style: normal; font-variant: normal; letter-spacing: normal;
                                line-height: normal; orphans: auto; text-align: -webkit-center; text-indent: 0px;
                                text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px;
                                background-color: rgb(255, 255, 255);">Datos De La Persona</strong><br />
                        </td>
                    </tr>
                    </table>
                    <table>
                    <tr>
                        <td style="text-align: left; width: 183px;">
                            Identificación<br />
                            <asp:Label ID="lblCodPersona" runat="server" Visible="false" />
                            <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Enabled="false"></asp:TextBox>
                        </td>
                        <td style="text-align: left; width: 119px;">
                            Tipo Identif.<br />
                            <asp:TextBox ID="txtTipoidentif" runat="server" CssClass="textbox" Enabled="false"></asp:TextBox>
                        </td>
                        <td rowspan="3" style="text-align: center;" width="30%">
                            <asp:HiddenField ID="hdFileName" runat="server" />
                            <asp:Image ID="imgFoto" runat="server" Height="160px" Width="121px" />
                            <asp:HiddenField ID="hdFileNameThumb" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 183px;" >
                            Nombres.<br />
                            <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" Enabled="false" 
                                Width="314px"></asp:TextBox>
                        </td>
                        <td style="text-align: left; width: 119px;" >
                            Apellidos<br />
                            <asp:TextBox ID="txtApellidos" runat="server" CssClass="textbox" 
                                Enabled="false" Width="296px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                    
                        <td style="text-align: left; width: 183px;">
                            Direccion.<br />
                            <asp:TextBox ID="txtDireccion" runat="server" CssClass="textbox" 
                                Enabled="false" Width="317px"></asp:TextBox>
                        </td>
                        <td style="text-align: left; width: 119px;">
                            Telefono.<br />
                            <asp:TextBox ID="TXTtELEFONO" runat="server" CssClass="textbox" Enabled="false"></asp:TextBox>
                            &nbsp;
                        </td>
                          </table>
                          <table style="width: 509px">
                        <caption>
                            <caption>
                                <tr>
                                    <td style="text-align: left; width: 244px;">
                                        <strong style="color: rgb(64, 65, 66); font-family: 'Segoe UI', Arial, Helvetica, Verdana, sans-serif;
                                            font-size: 14px; font-style: normal; font-variant: normal; letter-spacing: normal;
                                            line-height: normal; orphans: auto; text-align: -webkit-center; text-indent: 0px;
                                            text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px;
                                            background-color: rgb(255, 255, 255);">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Datos De La Autorización</strong><br />
                                        <td class="logo">
                                        </td>
                                    </td>
                                </tr>
                                </td>
                            </caption>
                        </caption>
                        </caption> </caption>
                    </tr>
                    <caption>
                    </table>
                    <table>
                        <tr>
                            <td style="width: 183px" >
                                Id.<br />
                                <asp:TextBox ID="txtId" runat="server" CssClass="textbox" Enabled="false"></asp:TextBox>
                            </td>
                            <td>
                                Tipo Producto.<br />
                                <asp:TextBox ID="txtTipoproducto" runat="server" CssClass="textbox" Enabled="false"></asp:TextBox>
                            </td>
                            <td>
                                Numero Producto.<br />
                                <asp:TextBox ID="txtNumeroprod" runat="server" CssClass="textbox" Enabled="false"></asp:TextBox>
                            </td>
                            <td>
                                IP.<br />
                                <asp:TextBox ID="txtip" runat="server" CssClass="textbox" Enabled="false"></asp:TextBox>
                            </td>
                            <td>
                                Usuario.<br />
                                <asp:TextBox ID="txtUsUARIO" runat="server" CssClass="textbox" Enabled="false"></asp:TextBox>
                            </td>
                           
                        </tr>
                        </tr>
                        </table>
                    </caption>
                    </tr> </caption> </tr>
                    <caption>
                        <tr>
                        </table>
                        <table>
                            <tr>
                                <td style="text-align: left; width: 348px;">
                                    <strong style="color: rgb(64, 65, 66); font-family: 'Segoe UI', Arial, Helvetica, Verdana, sans-serif;
                                    font-size: 14px; font-style: normal; font-variant: normal; letter-spacing: normal;
                                    line-height: normal; orphans: auto; text-align: -webkit-center; text-indent: 0px;
                                    text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px;
                                    background-color: rgb(255, 255, 255);">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Datos De La Excepción</strong><br />
                                    <td class="logo">
                                    </td>
                                </td>
                            </tr>
                            <table>
                                <tr>
                                    <td class="logo" style="width: 183px">
                                        Fecha Excepcion<br />
                                        <ucFecha:fecha ID="txtFechaExcep" runat="server" AutoPostBack="True" 
                                            CssClass="textbox" Enabled="false" MaxLength="1" ValidationGroup="vgGuardar" />
                                    </td>
                                    <td>
                                        Motivo Excepcion<br />
                                        <asp:DropDownList ID="ddlMotivoExcep" runat="server" CssClass="textbox" 
                                             Width="152px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        Observaciones<br />
                                        <asp:TextBox ID="txtObservaciones" runat="server" CssClass="textbox"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </tr>
                        
                    </caption>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
        <asp:View ID="mvFinal" runat="server">
            <asp:Panel ID="PanelFinal" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <asp:Label ID="lblMensaje" runat="server" Text="Datos Modificados Correctamente"
                                Style="color: #FF3300"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
    <uc1:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
