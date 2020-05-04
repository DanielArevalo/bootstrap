<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Desembolso Masivo :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">

   <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server">
        <table id="tbCriterios" border="0" cellpadding="0" cellspacing="0" style="width: 100%">
            
            <tr>                
                <td style="width: 50%; text-align: center;">
                    Fecha de Costeo :
                    <br />
                    <ucFecha:fecha ID="txtFecha" runat="server" CssClass="textbox" MaxLength="1" 
                        Width="148px" />
                </td>
                <td style="width: 50%; text-align: center;">
                    Ordenado por :
                    <br />
                    <asp:DropDownList ID="ddlOrdenado" runat="server" CssClass="textbox" 
                        Width="190px" AppendDataBoundItems="True" 
                        onselectedindexchanged="ddlOrdenado_SelectedIndexChanged">                        
                    </asp:DropDownList>               
               
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pDatos" runat="server">
        <hr style="width: 100%" />
        <table style="width: 100%">
            <tr>
                <td style="height: 493px">
                <asp:Button ID="btnExportar" runat="server" CssClass="btn8" 
                                onclick="btnExportar_Click" Text="Exportar a Excel"/>
                            
                    <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False"
                        AllowPaging="True" OnRowEditing="gvLista_RowEditing" PageSize="20" HeaderStyle-CssClass="gridHeader"
                        PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                        OnPageIndexChanging="gvLista_PageIndexChanging" style="font-size: x-small" 
                        onrowdatabound="gvLista_RowDataBound" ShowFooter="True" 
                        OnRowDeleting="gvLista_RowDeleting" DataKeyNames="numero_radicacion">
                        <Columns>
                            <%--<asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="cbSeleccionarEncabezado" runat="server" Checked="false" OnCheckedChanged="cbSeleccionarEncabezado_CheckedChanged"
                                        AutoPostBack="True" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbSeleccionar" runat="server" Checked="false" />
                                </ItemTemplate>
                                <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                <ItemStyle CssClass="gridIco"></ItemStyle>
                            </asp:TemplateField>--%>
                            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />                        
                            <asp:BoundField HeaderText="Id Credito" DataField="numero_radicacion">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="identificacion" HeaderText="Identificación" />
                            <asp:BoundField DataField="nombre" HeaderText="Cliente">
                            <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tipo" HeaderText="Tipo" />
                            <asp:BoundField HeaderText="Monto Inicial" DataField="monto" 
                                DataFormatString="{0:n0}">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="saldo" DataFormatString="{0:n0}" 
                                HeaderText="Saldo Capital">
                            <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Plazo Inicial" DataField="plazo">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Periodicidad" DataField="cod_periodicidad">
                            <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Cuota" DataFormatString="{0:n0}" 
                                DataField="cuota">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="F. PróxPago" DataField="fecha_proximo_pago" 
                                DataFormatString="{0:d}">
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Días Mora" DataField="dias_mora">
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Valor del Crédito" DataField="valor_total" 
                                DataFormatString="{0:n0}">
                            </asp:BoundField>
                            <asp:BoundField HeaderText="EXP" DataField="valor_exp" 
                                DataFormatString="{0:n0}" />
                            <asp:BoundField HeaderText="PI" DataField="probabilidad_incump" 
                                DataFormatString="{0:n0}" />
                            <asp:BoundField 
                                HeaderText="PE" DataField="perdida_esperada" DataFormatString="{0:n0}" />                                
                            <asp:BoundField HeaderText="Garantía" DataField="garantia" 
                                DataFormatString="{0:n0}" />
                            <asp:BoundField HeaderText="PDI" DataField="porcentaje_pdi" 
                                DataFormatString="{0:n0}" />
                            <asp:BoundField HeaderText="Total" DataField="total_ajuste" 
                                DataFormatString="{0:n0}" />
                        </Columns>
                        <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                        <PagerStyle CssClass="gridPager"></PagerStyle>
                        <RowStyle CssClass="gridItem"></RowStyle>
                    </asp:GridView>
                    <asp:Label ID="lblTotalRegs" runat="server"/>
                </td>
            </tr>
        </table>
    </asp:Panel>
    

    <asp:HiddenField ID="hfNiif" runat="server" />
   
    <uc4:mensajeGrabar ID="ctlMensaje" runat="server"/>

</asp:Content>