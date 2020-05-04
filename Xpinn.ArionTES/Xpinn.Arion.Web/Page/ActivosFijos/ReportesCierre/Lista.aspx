<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" EnableEventValidation="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/ctlFechaCierre.ascx" TagName="ddlCierreFecha" TagPrefix="ctl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server">
        <table style="width: 90%;">
            <tr>
                <td align="Left">Tipo de Producto<br />
                    <asp:DropDownList ID="ddlTipo" runat="server" AutoPostBack="True" CssClass="textbox" Width="200px" OnSelectedIndexChanged="ddlTipo_SelectedIndexChanged">
                        <asp:ListItem Value="0">&lt;Seleccione un Item&gt;</asp:ListItem>
                        <asp:ListItem Value="Y">Cierre Activos Fijos</asp:ListItem>
       
                    </asp:DropDownList>
                   </td>
                <td>
                    <asp:CheckBox Text="ordenar la fecha de corte" ID="chkOrden" Checked="false" Enabled="false" AutoPostBack="true" OnCheckedChanged="chkOrden_CheckedChanged" runat="server" />
                </td>
                <td style="text-align: left">Fecha de Corte:
                    <ctl:ddlCierreFecha ID="ddlCierreFecha" runat="server" Requerido="True" />
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:MultiView ID="mvLista" runat="server">
        <asp:View ID="vGridReporteCierre" runat="server">
            <div style="overflow: scroll; height: 500px; width: 1100px;">
                <div style="width: 1200px;">
                    <asp:Button ID="btnExportar" runat="server" CssClass="btn8"
                        OnClick="btnExportar_Click" Text="Exportar a excel" />
                    <br />
                    <asp:GridView ID="gvReportecierre" runat="server" AutoGenerateColumns="False"
                        DataKeyNames="cod_act" HeaderStyle-CssClass="gridHeader"
                        PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" ShowFooter="True"
                        Style="font-size: x-small" Width="100%" GridLines="Horizontal">
                        <Columns>
                         <asp:BoundField DataField="fecha_historico" HeaderText="Fecha historico"> <ItemStyle HorizontalAlign="center"/> </asp:BoundField>
                         <asp:BoundField DataField="cod_act" HeaderText="Cod_act"> <ItemStyle HorizontalAlign="center"/> </asp:BoundField>
                        <asp:BoundField DataField="clase" HeaderText="Clase"> <ItemStyle HorizontalAlign="center"/> </asp:BoundField>
                        <asp:BoundField DataField="tipo" HeaderText="Tipo"> <ItemStyle HorizontalAlign="center"/> </asp:BoundField>
                        <asp:BoundField DataField="cod_ubica" HeaderText="Cod_ubica"> <ItemStyle HorizontalAlign="center"/> </asp:BoundField>
                        <asp:BoundField DataField="cod_costo" HeaderText="Centro_costo"> <ItemStyle HorizontalAlign="center"/> </asp:BoundField>
                        <asp:BoundField DataField="nombre" HeaderText="Nombre Activo"> <ItemStyle HorizontalAlign="center"/> </asp:BoundField>
                        <asp:BoundField DataField="anos_util" HeaderText="Años_util"> <ItemStyle HorizontalAlign="center"/> </asp:BoundField>
                        <asp:BoundField DataField="estado" HeaderText="Estado"> <ItemStyle HorizontalAlign="center"/> </asp:BoundField>
                        <asp:BoundField DataField="cod_encargado" HeaderText="Cod_encargado	"> <ItemStyle HorizontalAlign="center"/> </asp:BoundField>
                        <asp:BoundField DataField="fecha_compra" HeaderText="Fecha_compra"> <ItemStyle HorizontalAlign="center"/> </asp:BoundField>
                        <asp:BoundField DataField="valor_compra" HeaderText="Valor_compra"> <ItemStyle HorizontalAlign="center"/> </asp:BoundField>
                        <asp:BoundField DataField="valor_avaluo" HeaderText="Valor_avaluo"> <ItemStyle HorizontalAlign="center"/> </asp:BoundField>
                        <asp:BoundField DataField="valor_salvamen" HeaderText="Valor_salvamen"> <ItemStyle HorizontalAlign="center"/> </asp:BoundField>
                        <asp:BoundField DataField="cod_oficina" HeaderText="Cod_ficina"> <ItemStyle HorizontalAlign="center"/> </asp:BoundField>
                        <asp:BoundField DataField="fecha_ult_depre" HeaderText="Fecha_ult_depre"> <ItemStyle HorizontalAlign="center"/> </asp:BoundField>
                        <asp:BoundField DataField="acumulado_depreciacion" HeaderText="Acumulado_depreciacion"> <ItemStyle HorizontalAlign="center"/> </asp:BoundField>
                        <asp:BoundField DataField="saldo_por_depreciar" HeaderText="Saldo_por_depreciar"> <ItemStyle HorizontalAlign="center"/> </asp:BoundField>
                        <asp:BoundField DataField="codclasificacion_nif" HeaderText="Codclasificacion_nif"> <ItemStyle HorizontalAlign="center"/> </asp:BoundField>
                        <asp:BoundField DataField="tipo_activo_nif" HeaderText="Tipo_activo_nif"> <ItemStyle HorizontalAlign="center"/> </asp:BoundField>
                        <asp:BoundField DataField="valor_activo_nif" HeaderText="Valor_activo_nif"> <ItemStyle HorizontalAlign="center"/> </asp:BoundField>
                        <asp:BoundField DataField="vida_util_nif" HeaderText="Vida_util_nif"> <ItemStyle HorizontalAlign="center"/> </asp:BoundField>
                        <asp:BoundField DataField="valor_residual_nif" HeaderText="Valor_residual_nif"> <ItemStyle HorizontalAlign="center"/> </asp:BoundField>
                        <asp:BoundField DataField="porcentaje_residual_nif" HeaderText="Porcentaje_residual_nif"> <ItemStyle HorizontalAlign="center"/> </asp:BoundField>
                        <asp:BoundField DataField="adiciones_nif" HeaderText="Adiciones_nif"> <ItemStyle HorizontalAlign="center"/> </asp:BoundField>
                        <asp:BoundField DataField="vrdeterioro_nif" HeaderText="Vrdeterioro_nif	"> <ItemStyle HorizontalAlign="center"/> </asp:BoundField>
                        <asp:BoundField DataField="vrrecdeterioro_nif" HeaderText="Vrrecdeterioro_nif	"> <ItemStyle HorizontalAlign="center"/> </asp:BoundField>
                        <asp:BoundField DataField="revaluacion_nif" HeaderText="Revaluacion_nif	"> <ItemStyle HorizontalAlign="center"/> </asp:BoundField>
                        <asp:BoundField DataField="revrevaluacion_nif" HeaderText="Revrevaluacion_nif	"> <ItemStyle HorizontalAlign="center"/> </asp:BoundField>

               </Columns>
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle CssClass="gridPager" />
                        <RowStyle CssClass="gridItem" />
                    </asp:GridView>
                   
                </div>
            </div>
            <asp:Label ID="lblTotalRegs" runat="server" />
            <br />
            <br />
            &nbsp;                                                            
        </asp:View>
    </asp:MultiView>

</asp:Content>

