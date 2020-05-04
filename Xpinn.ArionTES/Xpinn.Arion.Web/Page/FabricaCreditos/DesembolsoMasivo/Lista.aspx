<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Desembolso Masivo :." %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="numero" TagPrefix="ucnumero" %>
<%@ Register Src="~/General/Controles/ctlProcesoContable.ascx" TagName="procesoContable" TagPrefix="uc2" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">

    <script type="text/javascript">
        function Consultar(btnConsultar_Click) {
            var obj = document.getElementById("btnConsultar_Click");
            if (obj) {
                obj.click();
            }
        }

        function ValidNum(e) {
            var tecla = document.all ? tecla = e.keyCode : tecla = e.which;
            return ((tecla > 47 && tecla < 58) || tecla == 46);
        }

    </script>

    <asp:Panel ID="panelGeneral" runat="server">
        <asp:MultiView ID="mvDesembolsoMasivo" runat="server" ActiveViewIndex="0">
            <asp:View ID="vwCreditos" runat="server">
                <asp:Panel ID="pConsulta" runat="server">
                    <table id="tbCriterios" border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="tdI">
                                <asp:Panel ID="Panel1" runat="server">
                                    <table style="width: 80%;">
                                        <tr>
                                            <td style="text-align: left" colspan="2">
                                                <asp:ScriptManager ID="ScriptManager1" runat="server">
                                                </asp:ScriptManager>
                                            </td>
                                            <td style="text-align: left">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">Oficina :<br />
                                                <asp:DropDownList ID="ddlOficinas" runat="server" CssClass="textbox"
                                                    Width="230px" AppendDataBoundItems="True">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: left">Identificación<br />
                                                <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" />
                                                <asp:CompareValidator ID="CompareValidator2" runat="server"
                                                    ControlToValidate="txtIdentificacion" Display="Dynamic"
                                                    ErrorMessage="Sólo números enteros" ForeColor="Red" Operator="DataTypeCheck"
                                                    SetFocusOnError="True" Style="font-size: x-small" Type="Integer"
                                                    ValidationGroup="vgGuardar" />
                                            </td>
                                            <td style="text-align: left">Nombre Completo<br />
                                                <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox"
                                                    Width="300px" />
                                            </td>
                                            <td style="text-align: left;">Código de nómina<br />
                                                <asp:TextBox ID="txtCodigoNomina" runat="server" CssClass="textbox" Width="150px" />
                                            </td>
                                            <td style="width: 10px">&nbsp;</td>
                                        </tr>
                                    </table>
                                    <table style="width: 80%;">
                                        <tr>
                                            <td style="text-align: left;">Num. Radicación:<br />
                                                <asp:TextBox ID="txtnumero_radicacion" runat="server" CssClass="textbox" Width="90px" />
                                                <asp:CompareValidator ID="CompareValidator1" runat="server"
                                                    ControlToValidate="txtnumero_radicacion" Display="Dynamic"
                                                    ErrorMessage="Sólo números enteros" ForeColor="Red" Operator="DataTypeCheck"
                                                    SetFocusOnError="True" Style="font-size: x-small" Type="Integer"
                                                    ValidationGroup="vgGuardar" />
                                                &nbsp;a&nbsp;
                                            <asp:TextBox ID="txtnro_radicacion" runat="server" CssClass="textbox" Width="90px" />
                                                <asp:CompareValidator ID="cvtxtAntiguedadlugarEmpresa" runat="server"
                                                    ControlToValidate="txtnro_radicacion" Display="Dynamic"
                                                    ErrorMessage="Sólo números enteros" ForeColor="Red" Operator="DataTypeCheck"
                                                    SetFocusOnError="True" Style="font-size: x-small" Type="Integer"
                                                    ValidationGroup="vgGuardar" />
                                            </td>
                                            <td style="text-align: left;">Fecha de Aprobación :<br />
                                                <ucFecha:fecha ID="txtAprobacion_ini" runat="server" CssClass="textbox" MaxLength="1"
                                                    ValidationGroup="vgGuardar" Width="80px" />
                                                &nbsp;a
                                            <ucFecha:fecha ID="txtAprobacion_fin" runat="server" AutoPostBack="True"
                                                CssClass="textbox" MaxLength="1" ValidationGroup="vgGuardar" Width="80px" />
                                            </td>
                                            <td style="text-align: left;">Línea de Credito<br />
                                                <asp:DropDownList ID="ddlLineaCredito" runat="server" CssClass="textbox" Width="250px" AppendDataBoundItems="True">
                                                    <asp:ListItem Value="0">Seleccione un Item</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 10px">&nbsp;</td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                    <asp:UpdatePanel ID="updFormaPago" runat="server" UpdateMode="Conditional" RenderMode="Inline">
                        <ContentTemplate>
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 20%; height: 25px;"></td>
                                    <td style="width: 20%; height: 25px;"></td>
                                    <td style="width: 60%; height: 25px;"></td>
                                </tr>
                                <tr>
                                    <td style="text-align: center">Fecha Desembolso<br />
                                        <ucFecha:fecha ID="txtFechaRealiza" runat="server" CssClass="textbox" />
                                    </td>
                                    <td style="text-align: left">Forma Desembolso:
                             <br />
                                        <asp:DropDownList ID="ddlForma_Desem" runat="server" CssClass="textbox"
                                            Width="170px"
                                            OnSelectedIndexChanged="ddlForma_Desem_SelectedIndexChanged"
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:Panel ID="pnlCtasBanc" runat="server">
                                            <table>
                                                <tr>
                                                    <td style="text-align: left">Banco Giro<br />
                                                        <asp:DropDownList ID="ddlEntidad_giro" runat="server" CssClass="textbox" Width="250px"
                                                            AutoPostBack="true" OnSelectedIndexChanged="ddlEntidad_giro_SelectedIndexChanged" />
                                                    </td>
                                                    <td style="text-align: left">Cuenta Giro<br />
                                                        <asp:DropDownList ID="ddlCuenta_Giro" runat="server" CssClass="textbox" Width="200px" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlForma_Desem" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="ddlEntidad_giro" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </asp:Panel>
                <asp:Panel ID="pDatos" runat="server">
                    <hr style="width: 100%" />
                    <table style="width: 100%">
                        <tr>
                            <td>
                                <asp:GridView ID="gvLista" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
                                    OnRowEditing="gvLista_RowEditing" OnRowDataBound="gvLista_RowDataBound" Style="font-size: x-small"
                                    HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" DataKeyNames="numero_radicacion">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg" ToolTip="Modificar" />
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                            <ItemStyle CssClass="gridIco"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="cbSeleccionarEncabezado" runat="server" Checked="false"
                                                    OnCheckedChanged="cbSeleccionarEncabezado_CheckedChanged" AutoPostBack="True" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cbSeleccionar" runat="server" Checked="false" />
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                            <ItemStyle CssClass="gridIco"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="numero_radicacion" HeaderText="Radicación">
                                            <ItemStyle HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                            <ItemStyle HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="nombre" HeaderText="Nombre completo">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="oficina" HeaderText="Oficina">
                                            <ItemStyle HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="monto" HeaderText="Monto" DataFormatString="{0:n0}">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="plazo" HeaderText="Plazo">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="periodicidad" HeaderText="Periodicidad">
                                            <ItemStyle HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="forma_pago" HeaderText="Forma de pago">
                                            <ItemStyle HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_aprobacion" HeaderText="Fecha Aprobación"
                                            DataFormatString="{0:d}">
                                            <ItemStyle HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_inicio" DataFormatString="{0:d}" HeaderText="Fecha Inicio" />
                                        <asp:BoundField DataField="cod_persona" HeaderText="Cod.Persona">
                                            <ItemStyle HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Empresa Recaudo">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltipoPagaduria" runat="server" Visible="false" Text='<%# Bind("cod_empresa") %>' />
                                                <cc1:DropDownListGrid ID="ddlpagaduria" runat="server" CssClass="textbox" Width="130px"
                                                    CommandArgument="<%#Container.DataItemIndex %>" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cta. Bancaria Destino">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtCuentaBancaria" runat="server" CssClass="textbox" Width="80px" Text='<%# Bind("numero_cuenta") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Tipo Cuenta">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltipocuenta" runat="server" Visible="false" Text='<%# Bind("tipocuenta") %>' />
                                                <cc1:DropDownListGrid ID="ddltipocuenta" runat="server" CssClass="textbox" Width="130px"
                                                    CommandArgument="<%#Container.DataItemIndex %>" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>


                                    </Columns>
                                    <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                                    <PagerStyle CssClass="gridPager"></PagerStyle>
                                    <RowStyle CssClass="gridItem"></RowStyle>
                                </asp:GridView>
                                <asp:Label ID="lblTotalRegs" runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </asp:View>
            <asp:View ID="mvFinal" runat="server">
                <asp:Panel ID="PanelFinal" runat="server">
                    <table style="width: 100%; text-align: center">
                        <tr>
                            <td style="text-align: center; font-size: large;">
                                <br />
                                <br />
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center; font-size: large;" colspan="2"></td>
                        </tr>
                        <tr>
                            <td style="text-align: center; font-size: large;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;   
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;             
                            </td>
                            <td style="text-align: left; font-size: large;">
                                <asp:Label ID="lblMensajeGrabar" runat="server" Text="Desembolsos Realizados Correctamente"></asp:Label><br />
                                <asp:GridView ID="gvOperacion" runat="server" AllowPaging="False"
                                    AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE"
                                    BorderStyle="None" BorderWidth="0px" CellPadding="0" ForeColor="Black"
                                    GridLines="Vertical" PageSize="5" Style="font-size: x-small;" Width="40%">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField="cod_ope" HeaderText="Cod.Ope">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:HyperLinkField DataNavigateUrlFields="num_comp"
                                            DataNavigateUrlFormatString="../../Contabilidad/Comprobante/Lista.aspx?num_comp={0}"
                                            DataTextField="num_comp" HeaderText="Num.Comp" Target="_blank"
                                            Text="Número de Comprobante" />
                                        <asp:BoundField DataField="tipo_comp" HeaderText="Tipo.Comp" />
                                    </Columns>
                                    <FooterStyle BackColor="#CCCC99" />
                                    <HeaderStyle CssClass="gridHeader" />
                                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                    <RowStyle CssClass="gridItem" />
                                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                    <SortedAscendingHeaderStyle BackColor="#848384" />
                                    <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                    <SortedDescendingHeaderStyle BackColor="#575357" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center; font-size: large;">
                                <br />
                                <br />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </asp:View>
        </asp:MultiView>
    </asp:Panel>
    <asp:Panel ID="panelProceso" runat="server" Width="100%">
        <uc2:procesoContable ID="ctlproceso" runat="server" />
    </asp:Panel>

    <br />
    <br />

    <asp:ModalPopupExtender ID="mpeActualizarTasa" runat="server"
        PopupControlID="panelActualizarTasa" TargetControlID="hfFechaIni" X="300" Y="200"
        BackgroundCssClass="backgroundColor">
    </asp:ModalPopupExtender>
    <asp:HiddenField ID="hfFechaIni" runat="server" />
    <asp:Panel ID="panelActualizarTasa" runat="server" BackColor="White" Style="text-align: left">
        <%--<asp:UpdatePanel ID="upModificacionTasa" runat="server" 
            UpdateMode="Conditional">
            <ContentTemplate>--%>
        <div id="popupcontainer"
            style="border: medium groove #0000FF; width: 340px; background-color: #FFFFFF;"
            align="center">
            <div class="row popupcontainertitle">
                <div class="gridHeader" style="text-align: center">
                    MODIFICACION DE DATOS
                </div>
            </div>
            <div class="row">
                <div class="cell popupcontainercell">
                    <div id="ordereditcontainer">
                        <div class="row">
                            <div class="cell ordereditcell"
                                style="height: 44px; width: 304px; text-align: center;">
                                <div style="text-align: center; width: 330px;">
                                    <br />
                                    Fecha :&nbsp;<br />
                                </div>
                                <div class="cell ordereditcell" style="width: 330px; text-align: center;">
                                    <%-- <asp:TextBox ID="txtFechaAct" runat="server" CssClass="textbox" 
                                                style="text-align:left" Width="154px"></asp:TextBox>--%>
                                    <ucFecha:fecha ID="txtFechaAct" runat="server" CssClass="textbox" MaxLength="1"
                                        OnTextChanged="txtFechanacimiento_TextChanged" AutoPostBack="True"
                                        ValidationGroup="vgGuardar" Width="148px" />
                                </div>
                            </div>


                            <div class="cell" style="width: 303px; text-align: center;">
                                <br />

                                <asp:UpdatePanel ID="upModificacionTasa" runat="server"
                                    UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="row">
                                                <div class="row">
                                                    <span style="text-align: center; width: 330px;">
                                                        <asp:CheckBox ID="chkTasa" runat="server"
                                                            OnCheckedChanged="chkTasa_CheckedChanged" Text="Actualizar Tasa"
                                                            AutoPostBack="True" />
                                                    </span>
                                                    <br />
                                                </div>
                                            </div>
                                            <div class="row" style="text-align: center">
                                                Tasa de Credito :
                                            </div>

                                            <div style="text-align: center">
                                                <asp:TextBox ID="txttasa" runat="server" CssClass="textbox" MaxLength="5"
                                                    Width="156px" onkeypress="return ValidNum(event)"></asp:TextBox>
                                            </div>
                                            <div class="cell">
                                            </div>
                                            <div class="cell ordereditcell" style="text-align: center; width: 330px;">
                                                Tipo de Tasa :
                                            </div>
                                            <div class="cell" style="text-align: center; width: 330px;">
                                                <asp:DropDownList ID="ddltipotasa" runat="server" Width="189px" CssClass="dropdown">
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>

                                <div class="row">
                                    <div class="cell">
                                        <asp:TextBox ID="txtNum_radicacion" runat="server" CssClass="textbox"
                                            Style="text-align: left" Width="142px" Visible="false" />
                                        <br />
                                    </div>

                                    <div class="cell" style="text-align: left">
                                        <div class="cell" style="text-align: center">
                                            <asp:Button ID="btnModificar" runat="server" Height="30px"
                                                Text="Modificar" Width="100px" OnClick="btnModificar_Click"
                                                OnClientClick="Consultar(btnConsultar)" />
                                            <asp:Button ID="btnCloseReg1" runat="server" CausesValidation="false"
                                                CssClass="button" Height="30px" Text="Cerrar" Width="100px"
                                                OnClick="btnCloseReg1_Click" />
                                        </div>
                                    </div>


                                    <div class="cell" style="text-align: left">
                                        <div class="cell" style="text-align: center">
                                            <br />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--</ContentTemplate>
        </asp:UpdatePanel>--%>
    </asp:Panel>
    <br />
    <br />

    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />

</asp:Content>
