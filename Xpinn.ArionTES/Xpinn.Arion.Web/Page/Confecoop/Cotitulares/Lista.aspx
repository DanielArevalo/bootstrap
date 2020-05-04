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
                <table style="width: 100%">
                    <tr>
                        <td colspan="4" style="text-align: right;">
                            <asp:ImageButton ID="btnGenerar" runat="server" OnClick="btnGenerar_Click" ImageUrl="~/Images/btnGenerar.jpg" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 150px; height: 50px;">
                            Fecha<br />
                            <%--<ucFecha:fecha ID="ucFecha" runat="server" style="text-align: center" />--%>
                            <asp:DropDownList ID="ddlFecha" runat="server" CssClass="textbox" 
                                Width="135px">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left; height: 50px;">
                            <br />
                        </td>
                        <td style="text-align: left; width: 284px; height: 50px;">
                            </td>
                        <td style="text-align: left; width: 284px; height: 50px;">
                            </td>
                    </tr>
                    <tr display="Dynamic">
                        <td style="text-align: left; width: 150px;">
                            Tipo de Archivo<br />
                            <asp:RadioButtonList ID="rbTipoArchivo" runat="server" RepeatDirection="Horizontal"
                                Width="222px">
                                <asp:ListItem Value=";">CSV</asp:ListItem>
                                <asp:ListItem Value="   ">TEXTO</asp:ListItem>
                                <asp:ListItem Value="|">EXCEL</asp:ListItem>
                            </asp:RadioButtonList>
                             <br />
                        </td>
                        <td style="text-align: left;vertical-align:top">
                            Nombre del Archivo<br />
                            <asp:TextBox ID="txtArchivo" runat="server" Width="346px" placeholder="Nombre del Archivo"></asp:TextBox>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: left;">
                            &nbsp;
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
                            &nbsp;
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                        </td>                
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <asp:Label ID="lblResultado" runat="server" 
                                Text="Archivo Generado Correctamente" style="color: #FF0000; font-weight: 600"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>

        </asp:View>
    </asp:MultiView>

</asp:Content>
