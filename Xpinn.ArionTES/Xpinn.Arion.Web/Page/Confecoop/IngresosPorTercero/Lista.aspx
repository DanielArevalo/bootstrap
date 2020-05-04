<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
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
                        <td style="text-align: left; width: 150px; height: 50px;">
                            Fecha de Corte<br />
                            <%--<ucFecha:fecha ID="ucFecha" runat="server" style="text-align: center" />--%>
                            <asp:UpdatePanel ID="Up1" runat="server" UpdateMode="Conditional" RenderMode="Inline">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlFecha" runat="server" CssClass="textbox" Width="135px">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
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
                        <td style="text-align: left">
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
                            <asp:Label ID="lblResultado" runat="server" Text="Archivo de Centrales de Riesgo Generado Correctamente"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>

        </asp:View>
    </asp:MultiView>

</asp:Content>
