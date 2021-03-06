﻿<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:MultiView ID="mvActivosFijos" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <asp:Panel ID="PanelDatos" runat="server">
                <div style="text-align:right">
                <asp:ImageButton ID="btnConsultar" runat="server" OnClick="btnConsultar_Click" ImageUrl="~/Images/btnConsultar.jpg" />
                </div>
                <table style="width: 90%" cellspacing="4" cellpadding="4">
                    <tr>
                        <td style="text-align: left; width: 150px"; colspan="4">
                            <asp:CheckBox ID="cbCorte" runat="server" Text="Al Corte" OnCheckedChanged="cbCorte_CheckedChanged" AutoPostBack="true" />                            
                            <asp:CheckBox ID="cbPeriodo" runat="server" Text="Por Periodo" OnCheckedChanged="cbPeriodo_CheckedChanged" AutoPostBack="true" />                            
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 150px; height: 50px;">
                            <asp:Label ID="lblTituloFecha" runat="server" Visible="true" Text="Fecha de Corte" />
                            <br />
                            
                            <asp:UpdatePanel ID="Up1" runat="server" UpdateMode="Conditional" RenderMode="Inline">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlFecha" runat="server" CssClass="textbox" Width="135px">
                                    </asp:DropDownList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlTipoCuenta" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <asp:Panel ID="panelperiodo" runat="server">                            
                                <asp:Label ID="lblTituloPeriodo1" runat="server" Text="Fecha" />
                                <br />
                                <ucFecha:fecha ID="txtFecha" runat="server" CssClass="textbox" Width="85px" />
                            </asp:Panel>
                        </td>
                        <td style="text-align: left; height: 50px;">
                            <br />
                        </td>
                        <td style="text-align: left; width: 284px; height: 50px;">
                        </td>
                        <td style="text-align: left; width: 284px; height: 50px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 150px;">
                            Tipo de Archivo<br />
                            <asp:RadioButtonList ID="rbTipoArchivo" runat="server" RepeatDirection="Horizontal"
                                Width="222px">
                                <asp:ListItem Value=";">CSV</asp:ListItem>
                                <asp:ListItem Value="   ">TEXTO</asp:ListItem>
                                <asp:ListItem Value="|">EXCEL</asp:ListItem>
                            </asp:RadioButtonList>                            
                        </td>
                        <td style="text-align: left">
                            Nombre del Archivo<br />
                            <asp:TextBox ID="txtArchivo" runat="server" Width="346px" placeholder="Nombre del Archivo"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Ingrese una Ruta del Archivo a Generar : C:\Users\..."
                                ValidationGroup="vgConsultar" Display="Dynamic" ControlToValidate="txtArchivo"
                                ForeColor="Red" Style="font-size: x-small;"></asp:RequiredFieldValidator>
                            <br />
                        </td>
                        <td style="text-align: left; width: 284px;">
                            &nbsp;
                        </td>
                        <td style="text-align: left; width: 284px;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 150px;">                            
                            <asp:CheckBox ID="chkCuenta" runat="server" Text="Reporta Cuentas en cero" />
                        </td>
                        <td style="text-align: left">
                            <asp:UpdatePanel ID="upNiif" runat="server">
                                <ContentTemplate>
                                    Tipo de Norma<br />
                                    <asp:DropDownList ID="ddlTipoCuenta" runat="server" AutoPostBack="true" CssClass="textbox" Width="150px"
                                        onselectedindexchanged="ddlTipoCuenta_SelectedIndexChanged" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td style="text-align: left; width: 284px;">
                            &nbsp;
                        </td>
                        <td style="text-align: left; width: 284px;">
                            &nbsp;
                        </td>
                    </tr>
                </table>

                <table style="width: 90%" cellspacing="4" cellpadding="4">
                    <tr>
                        <td style="width: 308px">
                            &nbsp;
                        </td>
                        <td>
                            <div id="DivButtons">
                                <table>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td align="center">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td align="center">
                                            &nbsp;</td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            </asp:View>
            <asp:View ID="vwFinal" runat="server">
            <asp:Panel ID="PanelFinal" runat="server">
                <table width="100%">
                    <tr>
                        <td style="text-align: center">
                            &nbsp;</td>                
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <asp:Label ID="lblResultado" runat="server" Text="Archivo de SUPERSOLIDARIA - PUC Generado Correctamente"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>

        </asp:View>
    </asp:MultiView>

</asp:Content>
