<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/ctlBusquedaRapida.ascx" TagName="listadopersonas" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="../../../Scripts/PCLBryan.js"></script>
    <asp:Panel ID="pConsulta" runat="server" Width="100%">
        <table>
            <tr>
                <td style="height: 15px; text-align: left; font-size: x-small;" colspan="5">
                    <strong>Criterios de Búsqueda:</strong>
                </td>
            </tr>
            <tr>
                <td style="text-align: left;" width="22%">Identificación<br />
                    <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" />
                </td>
                <td style="text-align: left" width="22%">Tipo Identificación
                    <br />
                    <asp:DropDownList ID="ddlTipoIdentificacion" runat="server" CssClass="textbox"
                        Style="text-align: left" Height="26px">
                    </asp:DropDownList>
                </td>
                <td style="text-align: left" width="22%">Nombres<br />
                    <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" />
                </td>
                <td style="text-align: left" width="22%">Apellidos<br />
                    <asp:TextBox ID="txtApellidos" runat="server" CssClass="textbox" />
                </td>
            </tr>
            <tr>
                <td style="text-align: left;" width="20%">Fecha Solicitud<br />
                    <ucFecha:fecha ID="txtFechaSolicitud" runat="server" />
                </td>
                <td style="text-align: left" width="20%">Motivo del Retiro<br />
                    <asp:DropDownList ID="DdlMotRetiro" runat="server" CssClass="textbox" />
                </td>                
            </tr>
        </table>
        <hr style="width: 100%" />
    </asp:Panel>
    <br />
    <div id="GridScroll" style="overflow-x: auto; height: 30pc">
        <table width="100%">
            <tr>
                <td style="text-align: left">
                    <asp:Panel ID="panelGrilla" runat="server">
                        <strong>Listado de cruces de cuentas:</strong><br />
                        <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False"
                            PageSize="20" GridLines="Horizontal" ShowHeaderWhenEmpty="True" OnRowEditing="gvLista_RowEditing"
                            OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                            DataKeyNames="idretiro" Style="margin-top: 0px">
                            <Columns>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" Visible='<%# Eval("estado_modificacion").ToString().Trim() != "Solicitado" ? false:true %>' ID="imgRechazar" CommandName="Edit" ImageUrl="~/Images/gr_elim.jpg" />                                                    
                                    </ItemTemplate>
                                </asp:TemplateField>                                                                
                                <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg" Visible='<%# Eval("estado_modificacion").ToString().Trim() != "Solicitado" ? false:true %>'
                                            ToolTip="Cruce de cuentas" Width="16px" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                </asp:TemplateField>                                        
                                <asp:BoundField DataField="idretiro" HeaderText="No">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="identificacion" HeaderText="Identificación">   
                                </asp:BoundField>
                                <asp:BoundField DataField="Nombres" HeaderText="Nombres">                                    
                                </asp:BoundField>
                                <asp:BoundField DataField="Apellidos" HeaderText="Apellidos">                                    
                                </asp:BoundField>
                                <asp:BoundField DataField="fecha_retiro" HeaderText="Fec. Solicitud" HeaderStyle-Width="10%" DataFormatString="{0:d}">                                    
                                </asp:BoundField>
                                <asp:BoundField DataField="motivoretiro" HeaderText="Motivo retiro" HeaderStyle-Width="10%">                                    
                                </asp:BoundField>
                                <asp:BoundField DataField="observaciones" HeaderText="Observación">                                    
                                </asp:BoundField>
                                <asp:BoundField DataField="estado_modificacion" HeaderText="Estado Solicitud" HeaderStyle-Width="7%">                                    
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                    </asp:Panel>
                </td>
            </tr>
            </table>
    </div>
    <td style="text-align: center">
        <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
        <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado." Visible="False" />
    </td>
        <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
        <uc4:mensajegrabar ID="ctlMensajeBorrar" runat="server" />
</asp:Content>
