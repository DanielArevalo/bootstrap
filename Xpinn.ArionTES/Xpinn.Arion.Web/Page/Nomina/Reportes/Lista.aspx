<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Empleados :." %>


<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <table border="0" width="90%">
        <tr>
            <td>
                <table id="tbCriterios" border="0" width="100%">
                    <tr>
                        <td style="width: 20%">Código Liquidación<br />
                            <asp:TextBox ID="txtCodigoLiquidacion" runat="server" onkeypress="return isNumber(event)" CssClass="textbox" MaxLength="300" Width="70%" />
                        </td>
                        <td style="width: 20%">Fecha Inicio<br />
                            <asp:TextBox ID="txtFechaInicio" MaxLength="10" CssClass="textbox"
                                runat="server" Width="80%"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server"
                                PopupButtonID="imagenCalendarioInicio"
                                TargetControlID="txtFechaInicio"
                                Format="dd/MM/yyyy">
                            </asp:CalendarExtender>
                            <img id="imagenCalendarioInicio" alt="Calendario"
                                src="../../../Images/iconCalendario.png" />
                        </td>
                        <td style="width: 20%">Fecha Fin<br />
                            <asp:TextBox ID="txtFechaFin" MaxLength="10" CssClass="textbox"
                                runat="server" Width="80%"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender7" runat="server"
                                PopupButtonID="imagenCalendarioFin"
                                TargetControlID="txtFechaFin"
                                Format="dd/MM/yyyy">
                            </asp:CalendarExtender>
                            <img id="imagenCalendarioFin" alt="Calendario"
                                src="../../../Images/iconCalendario.png" />
                        </td>
                        <td style="width: 20%">Tipo Nómina<br />
                            <asp:DropDownList ID="ddlTipoNomina" runat="server" CssClass="dropdown" AppendDataBoundItems="true" Width="70%">
                                <asp:ListItem Text="Seleccione un Item" Value=" " />
                            </asp:DropDownList>
                        </td>
                        <td style="width: 20%">Centro Costo<br />
                            <asp:DropDownList ID="ddlCentroCosto" runat="server" CssClass="dropdown" AppendDataBoundItems="true" Width="70%">
                                <asp:ListItem Text="Seleccione un Item" Value=" " />
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">&nbsp;</td>
                    </tr>
                </table>
                
    <asp:MultiView runat="server" ID="mvPrincipal">
        <asp:View runat="server" ID="viewImprimir">
            <asp:Panel ID="Panel1" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center; font-size: large; height: 39px;">
                            <table style="width: 100%">
                                <tr>
                                    <td colspan="4" style="text-align: right;">
                                        <asp:Button ID="btnImprimirPila" runat="server" CssClass="btn8" Height="30px" OnClick="btnImprimirPila_Click" Text="Reporte PILA" />
                                        <asp:Button ID="btnImprimirUGGP" runat="server" CssClass="btn8" Height="30px" Style="margin-left: 10px" Text="UGPP" OnClick="btnImprimirUGGP_Click" />
                                    </td>
                                </tr>
                                <tr display="Dynamic">
                                    <td style="text-align: left; width: 150px;">Tipo de Archivo<br />
                                        <asp:RadioButtonList ID="rbTipoArchivo" runat="server" RepeatDirection="Horizontal" Width="222px">
                                            <asp:ListItem Value=";">CSV</asp:ListItem>
                                            <asp:ListItem Value="   ">TEXTO</asp:ListItem>
                                            <asp:ListItem Value="|">EXCEL</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td style="text-align: left">Nombre del Archivo<br />
                                        <asp:TextBox ID="txtArchivo" runat="server" placeholder="Nombre del Archivo" Width="346px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtArchivo" Display="Dynamic" ErrorMessage="Ingrese una Ruta del Archivo a Generar : C:\Users\..." ForeColor="Red" Style="font-size: x-small;" ValidationGroup="vgConsultar"></asp:RequiredFieldValidator>
                                        <br />
                                    </td>
                                    <td controltovalidate="txtArchivo" style="text-align: left; width: 284px;">&nbsp;</td>
                                    <td style="text-align: left; width: 284px;">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:GridView ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound">
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                            </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
            </td>
        </tr>
        <tr>
            <td>
                <hr />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    </asp:Content>
