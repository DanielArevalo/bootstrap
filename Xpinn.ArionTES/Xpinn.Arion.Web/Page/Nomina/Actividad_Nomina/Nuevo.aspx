<%@ Page Title=".: Empleados :." Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/fechaeditable.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/direccion.ascx" TagName="direccion" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/ctlFecha.ascx" TagPrefix="uc1" TagName="ctlFecha" %>
<%@ Register Src="~/General/Controles/ctlCentroCosto.ascx" TagPrefix="uc2" TagName="ctlCentroCosto" %>
<%@ Register Src="~/General/Controles/ctlResponsable.ascx" TagPrefix="uc1" TagName="ctlResponsable" %>




<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:MultiView ID="mvPrincipal" ActiveViewIndex="0" runat="server">
        <asp:View runat="server" ID="viewDatos">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td class="tdI" colspan="3" style="color: #FFFFFF; background-color: #0066FF; height: 20px; width: 100%;">
                        <strong>Datos Básicos</strong>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                        <asp:Panel runat="server">
                            <table>
                                <tr>
                                    <td style="text-align: left; width: 200px">Consecutivo:
                           <br />
                                        <asp:TextBox ID="txtconsecutivo" runat="server" CssClass="textbox" Enabled="false" Width="145px"></asp:TextBox>
                                    </td>
                                    <td style="text-align: left; width: 200px">Nombre de Actividad:
                         <br />
                                        <asp:TextBox ID="txtnombre" runat="server" CssClass="textbox" ValidationGroup="return isString(event)" Width="145px"> </asp:TextBox>
                                    </td>
                                    <td style="text-align: left; width: 200px">Objetivo:
                            <br />
                                        <asp:TextBox ID="txtobjetivo" runat="server" CssClass="textbox" ValidationGroup="return isString(event)"></asp:TextBox>
                                    </td>
                                    <td style="text-align: left; width: 200px">Fecha De Inicio:
                          <br />
                                        <uc1:ctlFecha runat="server" ID="ctlFechainicio" />
                                    </td>
                                    <td style="text-align: left; width: 200px">Fecha De Terminación:
                           <br />
                                        <uc1:ctlFecha runat="server" ID="ctlFechatermino" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left">Responsable De Actividad:
                        <br />
                                        <uc1:ctlResponsable runat="server" ID="ctlResponsable" Width="155px" />
                                    </td>

                                    <td style="text-align: left">Centro De Costos:
                            <br />
                                        <uc2:ctlCentroCosto runat="server" ID="ctlCentroCosto" Width="150px" />
                                    </td>

                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View runat="server" ID="viewGuardado">
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
                            <asp:Label ID="lblMensaje" runat="server" Text="Información Guardada Correctamente"
                                Style="color: #FF3300"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;"></td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
</asp:Content>
