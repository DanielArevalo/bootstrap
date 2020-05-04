<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Credito :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../../General/Controles/fechaeditable.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Src="../../../General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc2" %>
<%@ Register Src="../../../General/Controles/PlanPagos.ascx" TagName="planpagos" TagPrefix="uc3" %>
<%@ Register Src="../../../General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="../../../General/Controles/ctlCuotasExtras.ascx" TagName="ctlCuotasExtras" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <asp:Panel ID="Panel0" runat="server" Visible="true">
            <table border="0" cellpadding="5" cellspacing="0" width="100%">
                <tr>
                    <td class="tdI">
                        <asp:Panel ID="Panel2" runat="server">
                            <table style="width: 100%;">
                                <tr>
                                    <td class="logo" colspan="3" style="text-align: left">
                                        <strong>DATOS DEL DEUDOR</strong>
                                    </td>
                                    <td class="logo" style="text-align: left">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="logo" style="width: 150px; text-align: left">Identificación
                                    </td>
                                    <td style="text-align: left">Tipo Identificación
                                    </td>
                                    <td style="text-align: left">Nombre
                                    </td>
                                    <td style="text-align: left">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="logo" style="width: 150px; text-align: left">
                                        <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox"
                                            Enabled="false" />
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtTipo_identificacion" runat="server" CssClass="textbox" Enabled="false" />
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Enabled="false" Width="377px" />
                                    </td>
                                    <td style="text-align: left">
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="5" cellspacing="0" width="80%">
                <tr>
                    <td colspan="4" style="text-align: left">
                        <strong>DATOS DEL CRÉDITO</strong>
                    </td>
                    <td style="text-align: left">&nbsp;</td>
                    <td style="text-align: left">&nbsp;</td>
                    <td style="text-align: left">&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 151px;">Número Radicación<br />
                        <asp:TextBox ID="txtNumero_radicacion" runat="server" CssClass="textbox" Enabled="false" />
                    </td>
                    <td colspan="2" style="text-align: left">Línea de crédito<br />
                        <asp:TextBox ID="txtLinea_credito" runat="server" CssClass="textbox"
                            Enabled="false" Width="300px" />
                    </td>
                    <td colspan="2" style="text-align: left">
                        <asp:Panel ID="pnlReestruc" Visible="false" runat="server">
                            Línea de Reestructuración<br />
                            <asp:DropDownList runat="server" Width="347px" ID="ddlLineaReestruc">
                            </asp:DropDownList>
                        </asp:Panel>
                    </td>
                    <td style="text-align: left">&nbsp;</td>
                    <td style="text-align: left">&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 151px;">Monto<br />
                        <uc2:decimales ID="txtMonto" runat="server" Enabled="false" />
                    </td>
                    <td style="text-align: left">Plazo<br />
                        <asp:TextBox ID="txtPlazo" runat="server" CssClass="textbox" Enabled="false" />
                    </td>
                    <td style="text-align: left">Periodicidad<br />
                        <asp:TextBox ID="txtPeriodicidad" runat="server" CssClass="textbox" Enabled="false" />
                    </td>
                    <td style="text-align: left">Valor de la Cuota<br />
                        <uc2:decimales ID="txtValor_cuota" runat="server" Enabled="false" />
                    </td>
                    <td style="text-align: left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td style="text-align: left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td style="text-align: left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 151px;">Forma de Pago<br />
                        <asp:TextBox ID="txtForma_pago" runat="server" CssClass="textbox" Enabled="false" />
                    </td>
                    <td style="text-align: left">Moneda<br />
                        <asp:TextBox ID="txtMoneda" runat="server" CssClass="textbox" Enabled="false" />
                    </td>
                    <td style="text-align: left">F.Proximo Pago<br />
                        <asp:TextBox ID="txtFechaProximoPago" runat="server" CssClass="textbox" Enabled="false" />
                    </td>
                    <td style="text-align: left">F.Ultimo Pago
                        <br />
                        <asp:TextBox ID="txtFechaUltimoPago" runat="server" CssClass="textbox" Enabled="false" />
                    </td>
                    <td style="text-align: left">&nbsp;</td>
                    <td style="text-align: left">&nbsp;</td>
                    <td style="text-align: left">&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 151px;">Saldo Capital<br />
                        <uc2:decimales ID="txtSaldoCapital" runat="server" Enabled="false" />
                    </td>
                    <td style="text-align: left">Estado<br />
                        <asp:TextBox ID="txtEstado" runat="server" CssClass="textbox" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td style="text-align: left">F.Aprobación<br />
                        <asp:TextBox ID="txtFechaAprobacion" runat="server" CssClass="textbox" Enabled="false" />
                    </td>
                    <td style="text-align: left; width: 151px;">
                        <table>
                            <tr>
                                <td>
                                    Tasa<br />
                                    <asp:TextBox runat="server" ID="txtTasa" CssClass="textbox" Width="50px" />
                                </td>
                                <td>
                                    Tipo Tasa<br />
                                    <asp:TextBox runat="server" ID="txtTipoTasa" CssClass="textbox" />    
                                 </td>
                                <td>                   
                                    <asp:UpdatePanel ID="UpdatePanelchkCal" runat="server" UpdateMode="Always">
                                        <ContentTemplate>
                                            <asp:CheckBox ID="chkCal" runat="server" AutoPostBack="True" OnCheckedChanged="chkAplica_CheckedChanged" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="text-align: left">&nbsp;</td>
                    <td style="text-align: left">&nbsp;</td>
                </tr>
            </table>
            <table border="0" cellpadding="5" cellspacing="0" width="80%">
                <tr>
                    <td style="text-align: left; width: 151px;" colspan="4">
                        <asp:Label ID="lblCodeudores" Text="Codeudores:" runat="server" />
                        <asp:GridView ID="gvCodeudores" runat="server" AllowPaging="True" PageSize="5" GridLines="Horizontal"
                            ShowHeaderWhenEmpty="True" Width="95%" AutoGenerateColumns="False" Style="text-align: center; font-size: x-small;">
                            <Columns>
                                <asp:BoundField DataField="idcodeud" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo"
                                    HeaderText="Código">
                                    <HeaderStyle CssClass="gridColNo"></HeaderStyle>
                                    <ItemStyle CssClass="gridColNo"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="identificacion" HeaderText="Número Documento">
                                    <ItemStyle HorizontalAlign="Left" Width="30px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nombres" HeaderText="Nombre">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="direccion" HeaderText="Dirección">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="telefono" HeaderText="Teléfono">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                    </td>
                    <td style="text-align: left">&nbsp;</td>
                    <td style="text-align: left">&nbsp;</td>
                </tr>
            </table>
            <table border="0" cellpadding="5" cellspacing="0" width="80%">
                <tr>
                    <td style="text-align: left; width: 151px;"></td>
                    <td style="text-align: left; width: 151px;"></td>
                    <td style="text-align: left">&nbsp;</td>
                    <td style="text-align: left">&nbsp;</td>
                    <td style="text-align: left">&nbsp;</td>
                    <td style="text-align: left">&nbsp;</td>
                    <td style="text-align: left">&nbsp;</td>
                </tr>            
                <tr>
                    <td style="text-align: left;" colspan="4">                                  
                        <div>
                            <uc1:ctlCuotasExtras runat="server" ID="CtrCuotasExtras" />
                        </div>
                    </td>
                    <td style="text-align: left">&nbsp;</td>
                    <td style="text-align: left">&nbsp;</td>
                    <td style="text-align: left">&nbsp;</td>
                    <td style="text-align: left">&nbsp;</td>
                </tr>
            </table>
            <asp:MultiView ID="mvRefinanciacion" runat="server" ActiveViewIndex="0">
                <asp:View ID="vwDatos" runat="server">
                    <asp:UpdatePanel ID="UpdRefinanciacion" runat="server">
                        <ContentTemplate>
                            <table border="0" cellpadding="5" cellspacing="0" width="80%" style="font-size: xx-small">
                                <tr>
                                    <td colspan="2" style="text-align: left">
                                        <strong><span style="font-size: small">DATOS DE LA REESTRUCTURACIÓN</span></strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; vertical-align: top">
                                        <span style="font-size: small">Atributos a Reestructurar:</span>
                                        <asp:GridView ID="gvLista" runat="server" AllowPaging="True"
                                            AutoGenerateColumns="False" OnPageIndexChanging="gvLista_PageIndexChanging"
                                            Width="100%" DataKeyNames="cod_atr"
                                            HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                                            RowStyle-CssClass="gridItem" Style="font-size: small"
                                            ShowFooter="True">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Cod.Atr.">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="15px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCodAtr" runat="server" Text='<%# Eval("cod_atr") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Descripción">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Left" Width="80px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDescripcion" runat="server" Text='<%# Eval("nom_atr") %>' Width="140px" />
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblNomTotal" runat="server" Text='Total a Pagar' Width="140px" />
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Valor">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" Width="50px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblValor" runat="server" Text='<%# Bind("valor", "{0:N}") %>' Width="70px" />
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtTotal" runat="server" Enabled="false" CssClass="textboxAlineadoDerecha" Width="80px" />
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Reestructurar">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkAplica" runat="server" Checked='<%# Eval("Aplica") %>' Enabled='<%# Eval("cod_atr").ToString() == "1" %>' AutoPostBack="True" OnCheckedChanged="chkAplica_CheckedChanged" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle BorderColor="White" BorderStyle="None" BorderWidth="0px" HorizontalAlign="Right" />
                                            <HeaderStyle CssClass="gridHeader" />
                                            <PagerStyle CssClass="gridPager" />
                                            <RowStyle CssClass="gridItem" />
                                        </asp:GridView>
                                    </td>
                                    <td style="text-align: left">
                                        <table border="0" cellpadding="5" cellspacing="0" width="100%">
                                            <tr>
                                                <td>
                                                    <span style="font-size: small">Fecha Reestructuración</span><br />
                                                    <uc1:fecha ID="txtFechaRefinanciacion" runat="server" OneventoCambiar="txtFechaRefinanciacion_eventoCambiar" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <span style="font-size: small">Valor Abono a Capital</span><br />
                                                    <uc2:decimales ID="txtAbono" runat="server" Enabled="true" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <span style="font-size: small">Valor a Reestructurar</span><br />
                                                    <uc2:decimales ID="txtRefinanciar" runat="server" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="text-align: left">
                                        <table border="0" cellpadding="5" cellspacing="0" width="100%">
                                            <tr>
                                                <td style="text-align: left">
                                                    <span style="font-size: small">Plazo</span><br />
                                                    <uc2:decimales ID="txtNuePlazo" runat="server" />
                                                </td>
                                                <td style="text-align: left">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left">
                                                    <span style="font-size: small">Periodicidad</span><br />
                                                    <asp:DropDownList ID="ddlNuePeriodicidad" runat="server" CssClass="dropdown" Width="161px" Height="23" />
                                                </td>
                                                <td style="text-align: left">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left; font-size: small;">F.Proximo Pago<br />
                                                    <uc1:fecha ID="txtNueFechaProximoPago" runat="server" Enabled="true" />
                                                </td>
                                                <td style="text-align: left; font-size: small;">&nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td></td>
                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>

                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <table border="0" width="100%">
                        <tr>
                            <td style="text-align: center">
                                <asp:ImageButton ID="btnAdelante" runat="server"
                                    ImageUrl="~/Images/btnPlanPagos.jpg" OnClick="btnAdelante_Click"
                                    ValidationGroup="vgGuardar" Width="100px" Height="25px" />
                            </td>
                        </tr>
                    </table>
                </asp:View>
                <asp:View ID="vwPlanPagos" runat="server">
                    <div style="text-align: left">
                        &nbsp;&nbsp;Nueva Cuota<br />
                        &nbsp;<uc2:decimales ID="txtCuota" runat="server" Enabled="false" />
                    </div>
                    <br />
                    PLAN DE PAGOS
                    <uc3:planpagos ID="gvPlanPagos" runat="server" />
                    <table border="0" width="100%">
                        <tr>
                            <td style="text-align: center">
                                <asp:ImageButton ID="btnRegresar" runat="server"
                                    ImageUrl="~/Images/btnRegresar.jpg" OnClick="btnRegresar_Click"
                                    ValidationGroup="vgGuardar" Width="100px" Height="25px" />
                            </td>
                        </tr>
                    </table>
                </asp:View>
            </asp:MultiView>
        </asp:Panel>
        <asp:Panel ID="Panel1" runat="server" Visible="false">
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
                        <asp:Label ID="lblMensaje" runat="server" Text="Operación Realizada Correctamente"
                            Style="color: #FF3300"></asp:Label>
                        <br />
                        <br />
                        <br />
                    </td>
                </tr>
            </table>
        </asp:Panel>

    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />

</asp:Content>
