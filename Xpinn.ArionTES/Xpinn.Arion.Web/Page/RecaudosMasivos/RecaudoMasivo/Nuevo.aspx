﻿<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/ctlProcesoContable.ascx" TagName="procesoContable" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="panelGeneral" runat="server">
        <asp:MultiView ID="mvAplicar" runat="server" ActiveViewIndex="0">
            <asp:View ID="vwLista" runat="server">
                <table style="width: 85%">
                    <tr>
                        <td style="text-align: left; width: 25%">Empresa Recaudadora<br />
                            <asp:DropDownList ID="ddlEntidad" runat="server" CssClass="dropdown" Width="94%"
                                Enabled="False">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left; width: 15%">
                            <asp:Label ID="lblFechaAplica" runat="server" Visible="True" Text="Fecha de Aplicacion" /><br />
                            <ucFecha:fecha ID="ucFechaAplicacion" runat="server" Enabled="true" />
                        </td>
                        <td style="text-align: left; width: 20%">
                            <asp:Label ID="lblNumeroLista" runat="server" Visible="True" Text="Número de Aplicación" /><br />
                            <asp:TextBox ID="txtNumeroLista" runat="server" Enabled="false" Width="90%"></asp:TextBox>
                        </td>
                        <td colspan="2" style="text-align: left; width: 30%; vertical-align: central">
                            <asp:CheckBox ID="chkPorAplicar" runat="server" Text="Aportes Pendientes por aplicar" TextAlign="Right" />
                        </td>
                    </tr>
                </table>
                <table style="width: 100%">
                    <tr>
                        <td colspan="2">
                            <div style="overflow: scroll; height: 500px; width: 100%;">
                                <div style="width: 100%;">
                                    <asp:GridView ID="gvMovGeneral" runat="server" Width="100%" PageSize="20" ShowHeaderWhenEmpty="True"
                                        AutoGenerateColumns="False" SelectedRowStyle-Font-Size="XX-Small" Style="font-size: small; margin-bottom: 0px;"
                                        OnRowDataBound="gvMovGeneral_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="iddetalle" HeaderText="Id." />
                                            <asp:BoundField DataField="cod_cliente" HeaderText="Cod.Cli." />
                                            <asp:BoundField DataField="identificacion" HeaderText="Cedula/Nit">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nombre" HeaderText="Nombres">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="tipo_producto" HeaderText="Tipo de Producto" />
                                            <asp:BoundField DataField="numero_producto" HeaderText="Número de Producto" />
                                            <asp:TemplateField HeaderText="Tipo Aplicacion" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <cc1:DropDownListGrid ID="ddlTipoAplicacion" runat="server" Style="font-size: xx-small; text-align: center"
                                                        Width="100px" CssClass="dropdown" SelectedValue='<%# Bind("tipo_aplicacion") %>'
                                                        CommandArgument='<%#Container.DataItemIndex %>'>
                                                        <asp:ListItem Value="Por Valor">Por Valor</asp:ListItem>
                                                        <asp:ListItem Value="Pago Total">Pago Total</asp:ListItem>
                                                        <asp:ListItem Value="Por Valor a Capital">Por Valor a Capital</asp:ListItem>
                                                        <asp:ListItem Value="Abono a Capital">Abono a Capital</asp:ListItem>
                                                    </cc1:DropDownListGrid>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="num_cuotas" HeaderText="Num.Cuotas" />
                                            <asp:BoundField DataField="valor" HeaderText="Valor a Aplicar" DataFormatString="{0:N}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle CssClass="gridHeader" />
                                        <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                        <RowStyle CssClass="gridItem" />
                                        <SelectedRowStyle Font-Size="XX-Small"></SelectedRowStyle>
                                    </asp:GridView>
                                </div>
                            </div>                            
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; padding-right: 5px">Total Recaudo :&nbsp;&nbsp;<b><asp:Label ID="txtTotal" runat="server" /></b>
                        </td>
                        <td style="text-align: left; padding-left: 5px;">
                            <asp:Label ID="lblTotalReg" runat="server" Visible="False" />
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="vwFinal" runat="server">
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
                                <asp:Label ID="lblMensajeGrabar" runat="server" Text="Recaudos Aplicados Correctamente"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center; font-size: large;">
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center; font-size: large;">
                                <asp:Button ID="btnFinal" runat="server" Text="Continuar" OnClick="btnFinal_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </asp:View>
        </asp:MultiView>
    </asp:Panel>

    
    <script type="text/javascript" language="javascript">
        function SetFocus() {
            document.getElementById('cphMain_txtCodigo').focus();
        }
        window.onload = SetFocus;
    </script>
    <asp:Label ID="msg" runat="server" Font-Bold="true" ForeColor="Red" />
    <asp:Panel ID="panelProceso" runat="server" Width="100%">
        <uc2:procesoContable ID="ctlproceso" runat="server" />
    </asp:Panel>
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />

    <asp:HiddenField ID="HF2" runat="server" />
    <asp:ModalPopupExtender ID="mpeProcesando" runat="server" PopupControlID="pProcesando"
        TargetControlID="HF2" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pProcesando" runat="server" BackColor="White" Style="text-align: right;" CssClass="pnlBackGround">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <table style="width: 100%;">
                    <tr>
                        <td align="center">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/loading.gif" />
                            <br />
                            <asp:Label ID="lblProgreso" ClientIDMode="Static" Font-Size="Medium" runat="server" Style="text-align: center; color: #FF3300"></asp:Label>
                            <br />
                            <asp:Label ID="Label3" runat="server" Text="Espere un momento mientras se ejecuta el Proceso."></asp:Label>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" Style="text-align: center; width: 100%">
        <ContentTemplate>
            <asp:Timer ID="Timer1" runat="server" Interval="300" OnTick="Timer1_Tick">
            </asp:Timer>
            <br />
            <asp:Label ID="lblError" runat="server" Style="text-align: left; color: #FF3300"></asp:Label>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="hiddenValor" ClientIDMode="Static" runat="server" />

    <script type='text/javascript'>
        function Forzar() {
            __doPostBack('', '');
        }
    </script>

</asp:Content>