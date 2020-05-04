<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="AplicarPagos.aspx.cs" Inherits="Pagos" EnableEventValidation="false" %>

<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc3" %>
<%@ Register Src="~/Controles/mensajeGrabar.ascx" TagName="mensajeGrabar" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../../../Scripts/PCLBryan.js"></script>
    <script src="../../../Scripts/JScript.js"></script>

    <script type="text/javascript">

        function EvitarClickeoLocos() {
            if (contadorClickGuardar == 0) {
                contadorClickGuardar += 1;
                return true;
            }
            return false;
        }

        var contadorClickGuardar = 0;
        $(document).ready(function () {
            $("#btnPse").click(EvitarClickeoLocos);
        });
    </script>

    <div class="form-group">
        <asp:Panel ID="panelRealizaPago" runat="server" Style="padding: 15px">
            <div class="col-sm-12" style="padding-bottom: 8px">
                <asp:Panel ID="pnlInfo" runat="server">
                    <span class="glyphicon glyphicon-alert text-green"></span>&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblContent" runat="server" CssClass="text-green"></asp:Label>
                </asp:Panel>
                <h4 class="text-primary text-left">Resumen del pago a realizar</h4>
                <hr />
            </div>
            <div class="col-sm-12" style="padding-bottom: 8px">
                <div class="col-sm-4 text-left">
                    Fecha de Transacción
                </div>
                <div class="col-sm-8">
                    <asp:Label ID="lblFechaTran" runat="server" />
                </div>
            </div>
            <div class="col-sm-12" style="padding-bottom: 8px">
                <div class="col-sm-4 text-left">
                    Identificación
                </div>
                <div class="col-sm-8">
                    <asp:Label ID="lblIdentiEmer" runat="server" />
                </div>
            </div>
            <div class="col-sm-12" style="padding-bottom: 8px">
                <div class="col-sm-4 text-left">
                    Nombre
                </div>
                <div class="col-sm-8">
                    <asp:Label ID="lblNombreEmer" runat="server" />
                </div>
            </div>
            <div class="col-sm-12" style="padding-bottom: 8px">
                <div class="col-sm-4 text-left">
                    Tipo de Producto
                </div>
                <div class="col-sm-8">
                    <asp:Label ID="lbltipoProducto" runat="server" />
                    <asp:Label ID="lblCodTipoProducto" runat="server" Visible="false" />
                </div>
            </div>
            <div class="col-sm-12" style="padding-bottom: 8px">
                <div class="col-sm-4 text-left">
                    Número de Producto
                </div>
                <div class="col-sm-8">
                    <asp:Label ID="lblNroProducto" runat="server" />
                </div>
            </div>
            <div class="col-sm-12">
                <div class="col-sm-4 text-left">
                    Valor Total a Pagar 
                </div>
                <div class="col-sm-8 text-warning">
                      <asp:TextBox ID="lblValorEmer" runat="server" class="form-control" type="text" style="width: 150px; margin-left: 22.5rem; position:absolute;" ></asp:TextBox>
                    <%--<asp:Label ID="lblValorEmer" runat="server"/>--%>
                    <asp:Label ID="lblVrPago" runat="server" Visible="false" />
                </div>
            </div>
            <div class="col-sm-12">
                <br />
            </div>
            <div class="col-sm-12 text-left" style="padding-bottom: 10px">
                <hr />
                <h4 class="modal-title text-primary">Forma de Pago</h4>
            </div>
            <div class="col-sm-12">
                <div class="col-sm-2">
                    &nbsp;
                </div>
                <div class="col-sm-3 text-center">
                    <asp:ImageButton runat="server" ID="btnCtaAhorro" ImageUrl="~/Imagenes/logoEmpresa.jpg" Width="55px" Height="55px" ToolTip="Cta de Ahorros" OnClick="btnCtaAhorro_Click" />
                    <br />
                    Cooperativa
                </div>
                <div class="col-sm-3 text-center">
                    <asp:ImageButton runat="server" ID="btnPse" ClientIDMode="Static" ImageUrl="~/Imagenes/LogoPSE.png" Width="70px" Height="55px" OnClick="btnPse_Click" />
                    <br />
                    PSE
                </div>
                <div class="col-sm-4">
                    &nbsp;
                </div>
            </div>
            <asp:Panel ID="panelCuentas" runat="server" Visible="false">
                <div class="col-sm-12">
                    <hr />
                </div>
                <div class="col-sm-12" style="padding-bottom: 8px">
                    <div class="col-sm-8">
                        Cuenta de Ahorro      
                    </div>
                    <div class="col-sm-4">
                        Tipo de moneda
                    </div>
                </div>
                <div class="col-sm-12">
                    <div id="IdContainer" class="col-sm-8">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvCtaAhorros" runat="server" Width="100%"
                                    GridLines="Horizontal" AutoGenerateColumns="False" CssClass="table table-striped"
                                    PageSize="20" DataKeyNames="numero_cuenta, saldo_total"
                                    Font-Size="Small" OnRowDataBound="gvCtaAhorros_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>

                                                <cc1:CheckBoxGrid ID="chkSeleccion" runat="server" AutoPostBack="true"
                                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                    OnClick="javascript:SelectSingleCheckBoxInsideContainer(this.id, 'IdContainer')" OnCheckedChanged="chkSeleccion_CheckedChanged" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" CssClass="paddingCero" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="numero_cuenta" HeaderText="Cuenta" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" CssClass="paddingCero" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="saldo_total" HeaderText="Saldo" HeaderStyle-HorizontalAlign="Center"
                                            DataFormatString="{0:N2}">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" CssClass="paddingCero" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Valor a Aplicar" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <uc3:decimales ID="txtValorAplicar" runat="server" cssclass="form-control paddingCero" Width_="120px"
                                                    OneventoCambiar="txtValorAplicar_TextChanged" maxlength="12" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" CssClass="paddingCero" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="thcenter" />
                                </asp:GridView>
                                <asp:Label ID="lblMensaje" runat="server" CssClass="text-success" Style="font-size: x-small" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-sm-4 text-left" style="vertical-align: top">
                        <asp:DropDownList ID="ddlMoneda" runat="server" Width="180px" CssClass="form-control" />
                        <asp:DropDownList ID="ddlTipoPago" runat="server" Width="180px" CssClass="form-control" Visible="false" />
                    </div>
                </div>
            </asp:Panel>

            <div class="col-sm-12">
                <br />
            </div>
        </asp:Panel>

        <asp:Panel ID="panelConfirmación" runat="server" Style="padding: 15px" Visible="false">
            <div class="col-sm-12" style="padding-bottom: 8px">
                <h5 class="text-primary text-left">Confirmación de Pago</h5>
                <hr />
            </div>
            <div class="col-sm-12">
                <asp:GridView ID="gvTransacciones" runat="server" AllowPaging="False" AutoGenerateColumns="False"
                    BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                    ForeColor="Black" GridLines="Horizontal" PageSize="20" CssClass="table table-striped"
                    Width="90%" DataKeyNames="tipo_producto, nroRef, cod_moneda, tipo_tran, valor">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="tipo_producto" HeaderText="Tipo" />
                        <asp:BoundField DataField="nom_tproducto" HeaderText="T. Producto" />
                        <asp:BoundField DataField="nroRef" HeaderText="# Ref" />
                        <asp:BoundField DataField="valor" DataFormatString="{0:n0}" HeaderText="Valor">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cod_moneda" Visible="false">
                            <HeaderStyle CssClass="text-center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nom_moneda" HeaderText="Moneda">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="tipo_tran" Visible="false">
                            <HeaderStyle CssClass="text-center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nom_tipo_tran" HeaderText="Tipo Pago" />
                        <asp:BoundField DataField="tipo_mov" HeaderText="Tipo Movimiento" Visible="false">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
        </asp:Panel>

        <asp:Panel ID="panelFinal" runat="server" Visible="false">
            <div class="col-xs-12">
                <div class="col-lg-1">
                </div>
                <div class="col-lg-10 col-md-12 col-xs-12" style="margin-top: 27px">
                    <div class="col-xs-12">
                        <asp:Label ID="Label2" runat="server" Text="Su transacción se generó correctamente."
                            Style="color: #66757f; font-size: 28px;" />
                    </div>
                    <div class="col-xs-12">
                        <p style="margin-top: 36px">
                            La recordamos que una vez aprobada la transacción realizada, se verán reflejados los cambios en el producto. Para mayor información comuníquese con nosotros o acérquese a alguna de nuestras oficinas.
                        </p>
                    </div>
                    <div class="col-xs-12">
                        &nbsp;
                    </div>
                    <div class="col-xs-12">
                        &nbsp;
                    </div>
                    <div class="col-xs-12">
                        <asp:LinkButton ID="btnInicio" runat="server" CssClass="btn btn-primary" Width="170px" ToolTip="Home"
                            Style="border-radius: 0px; padding-left: 5px; padding-right: 5px; padding-top: 7px; padding-bottom: 7px" OnClick="btnInicio_Click">
                            <div class="pull-left" style="padding-left:10px">
                            <span class="fa fa-home"></span></div>&#160;&#160;Regresar al Inicio
                        </asp:LinkButton>
                    </div>
                </div>
                <div class="col-lg-1">
                </div>
            </div>
        </asp:Panel>
    </div>

    <asp:HiddenField ID="HF2" runat="server" />
    <asp:ModalPopupExtender ID="mpeProcesando" runat="server" PopupControlID="pProcesando"
        TargetControlID="HF2" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pProcesando" runat="server" BackColor="White" Style="text-align: right;"
        CssClass="pnlBackGround">
        <table style="width: 100%;">
            <tr>
                <td align="center">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Imagenes/Loading3.gif" />
                    <br />
                    <asp:Label runat="server" Text="Espere un momento mientras se ejecuta el Proceso."></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>

    <uc4:mensajeGrabar ID="ctlMensaje" runat="server" />

</asp:Content>

