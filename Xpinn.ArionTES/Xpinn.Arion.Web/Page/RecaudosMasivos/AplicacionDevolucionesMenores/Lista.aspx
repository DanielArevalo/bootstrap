<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Devoluciones Menores :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="numero" TagPrefix="ucnumero" %>
<%@ Register Src="~/General/Controles/ctlProcesoContable.ascx" TagName="procesoContable" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="panelGeneral" runat="server">
        <asp:UpdatePanel ID="updFormaPago" runat="server">
            <ContentTemplate>                    
                <asp:Panel ID="panelGrilla" runat="server">
                    <table style="width: 100%">
                        <tr>
                            <td style="text-align: left">
                                <strong>Devoluciones</strong><br />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                Fecha:<br />
                                <ucFecha:fecha ID="txtFecha" runat="server" CssClass="textbox" />
                            </td>
                        </tr>
                        <tr>
                            <td style="margin-left: 40px">
                                <asp:Button ID="btnExportar" runat="server" CssClass="btn8" 
                                    onclick="btnExportar_Click" Text="Exportar a Excel" />
                                <div id="divLista" runat="server" style="overflow: scroll; height: 500px; width: 100%">
                                <asp:GridView ID="gvLista" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
                                    AllowPaging="False" OnPageIndexChanging="gvLista_PageIndexChanging" DataKeyNames="cod_persona"
                                    PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                                    RowStyle-CssClass="gridItem">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="cbSeleccionarEncabezado" runat="server" Checked="false" OnCheckedChanged="cbSeleccionarEncabezado_CheckedChanged"
                                                    AutoPostBack="True" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cbSeleccionar" runat="server" Checked="false" />
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                            <ItemStyle CssClass="gridIco"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="cod_persona" HeaderText="Cod. Persona">
                                            <ItemStyle HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="nombre" HeaderText="Nombre" HeaderStyle-Width="400px">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="saldo" HeaderText="Saldo" DataFormatString="{0:N2}">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="valor" HeaderText="Valor" DataFormatString="{0:N2}">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                    </Columns>
                                    <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                                    <PagerStyle CssClass="gridPager"></PagerStyle>
                                    <RowStyle CssClass="gridItem"></RowStyle>
                                </asp:GridView>
                                </div>
                                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
            <Triggers>                    
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Panel ID="panelProceso" runat="server" Width="100%">
        <uc2:procesoContable ID="ctlproceso" runat="server" />  
    </asp:Panel> 

    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
