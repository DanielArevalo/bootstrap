<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc4" %>
<%@ Register Src="../../../General/Controles/mensajeGrabar.ascx" TagName="mensajeGrabar" TagPrefix="uc4" %>
<script runat="server">

</script>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:MultiView ID="mvRevaloriza" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwConsulta" runat="server">
            <asp:Panel ID="Panel2" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td colspan="6" style="text-align: center; color: #FFFFFF; background-color: #0066FF; height: 23px;">
                            <strong>Revalorización de Aportes</strong></td>
                    </tr>
                    <tr>
                        <td style="width: 140px; font-size: x-small; text-align: left; height: 47px;">Fecha Revalorización
                        </td>
                        <td style="width: 181px; height: 47px; text-align: left">
                            <uc4:fecha ID="txtFechaRevalorizacion" runat="server" />
                        </td>
                        <td style="width: 120px; font-size: x-small; height: 47px;">Línea
                        </td>
                        <td style="text-align: left; width">
                            <asp:DropDownList ID="DdlLineaAporte" runat="server" Style="margin-bottom: 0px"
                                Width="250px" CssClass="textbox">
                            </asp:DropDownList>
                        </td>
                        <td style="font-size: x-small; width:120px">
                            Fecha de Operacion                            
                        </td>
                        <td><uc4:fecha ID="txtFechaOperacion" runat="server" /></td>
                    </tr>
                    <tr>
                        <td style="width: 140px; font-size: x-small; text-align: left;">Tipo de Distribución&nbsp;
                        </td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlTipoDistribucion" runat="server" CssClass="textbox" Width="160px">
                                <asp:ListItem Value="1">Saldo Final</asp:ListItem>
                                <asp:ListItem Value="2">Saldo Promedio Dia/Año</asp:ListItem>
                                <asp:ListItem Value="2">Saldo Promedio</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="width: 120px; font-size: x-small;">% Distribución
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtporcdistribuir" runat="server" CssClass="textbox"
                                MaxLength="12" Width="149px"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="fte2" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                ValidChars=".," TargetControlID="txtporcdistribuir" />
                        </td>
                        <td colspan="2" style="font-size: x-small; text-align: left">
                            <asp:CheckBox ID="ChkAsociRetirados" runat="server"
                                Text="Incluir Asociados Retirados" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <hr />

            <asp:Panel ID="pnlPendientes" runat="server">
                <asp:Panel ID="panelNoRevalo" runat="server" CssClass="collapsePanelHeader" Height="30px">
                    <div style="padding: 5px; cursor: pointer; vertical-align: middle;">
                        <div style="float: left; color: #0066FF; font-size: small">
                            Aportes que no pueden realizarse la revalorización
                        </div>
                        <div style="float: left; margin-left: 5px; color: #0066FF; font-size: small">
                            <asp:Label ID="lblRevalo" runat="server">(Mostrar Detalles...)</asp:Label>
                            <asp:ImageButton ID="imgRevalo" runat="server" AlternateText="(Show Details...)"
                                ImageUrl="~/Images/expand.jpg" />
                        </div>
                        <br />
                    </div>
                </asp:Panel>
                <asp:Panel ID="paRevalDeta" runat="server" Width="989px">
                    <div style="overflow:scroll; max-height:600px; border-style: none; border-width: medium; background-color: #f5f5f5">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 100%">
                                    <asp:GridView ID="gvNoReval" runat="server" Width="99%"
                                        AutoGenerateColumns="False" PageSize="5" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                                        Style="font-size: x-small" DataKeyNames="num_aporte, saldo_base, valordist">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="num_aporte" HeaderStyle-CssClass="gridColNo"
                                                ItemStyle-CssClass="gridColNo">
                                                <HeaderStyle CssClass="gridColNo" />
                                                <ItemStyle CssClass="gridColNo" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="codigo" HeaderText="Código" />
                                            <asp:BoundField DataField="identificacion" HeaderText="Identificacion" />
                                            <asp:BoundField DataField="nombres" HeaderText="Nombres" />
                                            <asp:BoundField DataField="estado" HeaderText="Estado" />
                                            <asp:BoundField DataField="num_aporte" HeaderText="Número Aporte">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="saldo_base" HeaderText="Saldo Base" DataFormatString="{0:n}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="valordist" HeaderText="Valor a Distribuir" DataFormatString="{0:n}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                        </Columns>
                                        <FooterStyle BackColor="#CCCC99" />
                                        <HeaderStyle CssClass="gridHeader" />
                                        <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                        <RowStyle CssClass="gridItem" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
                <asp:CollapsiblePanelExtender ID="cpPendiente" runat="server" CollapseControlID="panelNoRevalo"
                    Collapsed="True" CollapsedImage="~/Images/expand.jpg" CollapsedText="(Click Aqui para Mostrar Detalles...)"
                    ExpandControlID="panelNoRevalo" ExpandedImage="~/Images/collapse.jpg" ExpandedText="(Click Aqui para Ocultar Detalles...)"
                    ImageControlID="imgRevalo" SkinID="CollapsiblePanelDemo" SuppressPostBack="True"
                    TargetControlID="paRevalDeta" TextLabelID="lblRevalo"
                    Enabled="True" />
                <br />
            </asp:Panel>

            <asp:Panel ID="pConsulta" runat="server">
                <table style="width:100%">
                    <tr>
                        <td style="text-align:left">
                            <strong>Listado de Aportes por Revalorizar</strong>
                        </td>
                    </tr>
                </table>
                <div style="overflow: scroll; max-height: 600px;">
                    <asp:GridView ID="gvConsultaDatos" runat="server" Width="99%"
                        AutoGenerateColumns="False" PageSize="5" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                        OnRowCommand="gvConsultaDatos_RowCommand" Style="font-size: x-small"
                        DataKeyNames="num_aporte, saldo_base, valordist" OnRowDeleting="gvConsultaDatos_RowDeleting">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg" ToolTip="Estado Cuenta" />
                                </ItemTemplate>
                                <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                <ItemStyle CssClass="gridIco" />
                             </asp:TemplateField>
                            <asp:BoundField DataField="num_aporte" HeaderStyle-CssClass="gridIco">
                                <HeaderStyle CssClass="gridIco"/>
                                <ItemStyle CssClass="gridIco" />                            
                            </asp:BoundField>
                            <asp:BoundField DataField="codigo" HeaderText="Código" />
                            <asp:BoundField DataField="identificacion" HeaderText="Identificacion" />
                            <asp:BoundField DataField="nombres" HeaderText="Nombres" />
                            <asp:BoundField DataField="estado" HeaderText="Estado" />
                            <asp:BoundField DataField="num_aporte" HeaderText="Número Aporte">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="saldo_base" HeaderText="Saldo Base" DataFormatString="{0:n}">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="valordist" HeaderText="Valor a Distribuir" DataFormatString="{0:n}">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Columns>
                        <FooterStyle BackColor="#CCCC99" />
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                        <RowStyle CssClass="gridItem" />
                    </asp:GridView>
                    <br />
                </div>
                <div style="text-align: center; width: 100%">
                    <table style="width: 100%">
                        <tr>
                            <td style="text-align: right; width: 85%">Total de Revalorización</td>
                            <td style="text-align: left; width: 15%">
                                <asp:TextBox ID="txtTotalReva" runat="server" CssClass="textbox" Enabled="false" /></td>
                        </tr>
                    </table>
                </div>
                <div style="text-align: center; width: 100%">
                    <asp:Label ID="lblTotalRev" runat="server" Visible="false" />
                </div>
            </asp:Panel>

        </asp:View>
        <asp:View ID="vwTipoComprobante" runat="server">
            <asp:ImageButton runat="server" ID="btnGuardarCom"
                ImageUrl="~/Images/btnGuardar.jpg" OnClick="btnGuardarCom_Click"
                ImageAlign="Right" />
            <br />
            <div id="gvDiv">
                <hr width="100%" />
                <table style="width: 100%">
                    <tr>
                        <td style="background-color: #0066FF">
                            <asp:Label ID="blComprobante" runat="server" ForeColor="White"
                                Text="Comprobante de Revalorización de Aportes" Font-Bold="True"
                                Font-Size="Medium" Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div style="overflow: scroll; max-height: 570px">

                                <asp:GridView ID="gvConsultaDatosCom" runat="server"
                                    AutoGenerateColumns="False" DataKeyNames="id" GridLines="Horizontal"
                                    OnRowCommand="gvConsultaDatos_RowCommand"
                                    OnRowDeleting="gvConsultaDatos_RowDeleting" PageSize="5"
                                    ShowHeaderWhenEmpty="True" Style="font-size: x-small" Width="99%">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnEliminar0" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg" ToolTip="Estado Cuenta" />
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="gridIco" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="id" HeaderText="Código" />
                                        <asp:BoundField DataField="cod_ope" HeaderText="Operacion" />
                                        <asp:BoundField DataField="tipooperacion" HeaderText="Tipo Operacion" />
                                        <asp:BoundField DataField="oficina" HeaderText="Oficina" />
                                        <asp:BoundField DataField="fecha" HeaderText="Fecha" />
                                        <asp:BoundField DataField="codigo" HeaderText="Codpersona" />
                                        <asp:BoundField DataField="num_aporte" HeaderStyle-CssClass="gridColNo"
                                            ItemStyle-CssClass="gridColNo">
                                            <HeaderStyle CssClass="gridColNo" />
                                            <ItemStyle CssClass="gridColNo" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="saldo_base" HeaderText="Saldo Base">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
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
                            </div>
                        </td>
                    </tr>                   
                </table>
            </div>
        </asp:View>
        <asp:View ID="vwImportacion" runat="server">
            <br /><br />
            <table>
                <tr>
                    <td style="text-align: left; font-size: xx-small">
                        <br />
                        <b>Tipo de Archivo : </b>&nbsp;&nbsp;&nbsp;Excel
                        <br />
                        <br />
                        <b>Estructura de archivo :  Código Asociado, Identificación, Nombre (Opcional), Estado ( 1 = Activo, 2 = Inactivo, 3 = Cerrado), Numero de Aporte, 
                        Saldo Base, Valor a Revalorizar</b>
                        <br /><br />
                        <b>Nombre de Pestaña excel : </b>&#160;&#160; Datos
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left"> 
                        <br />
                        <asp:FileUpload ID="flpArchivo" runat="server" />
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>


    <asp:HiddenField ID="HiddenFieldGrabar" runat="server" />
    <asp:ModalPopupExtender ID="mpeNuevo" runat="server"
        PopupControlID="panelActividadReg" TargetControlID="HiddenFieldGrabar"
        BackgroundCssClass="backgroundColor">
    </asp:ModalPopupExtender>





    <asp:Panel ID="panelActividadReg" runat="server" BackColor="White" Style="text-align: right" BorderWidth="1px" Width="500px">
        <div id="popupcontainer" style="width: 500px;padding:15px">
            <table style="width: 100%;">                
                <tr>
                    <td colspan="3" style="text-align: center">Esta Seguro en Generar&nbsp; la Revalorizacion?
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                    <td style="text-align: center">
                        <asp:Button ID="btnContinuar" runat="server" Text="Continuar"
                            CssClass="btn8" Width="182px" OnClick="btnContinuar_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnParar" runat="server" Text="Cancelar" CssClass="btn8"
                            Width="182px" OnClick="btnParar_Click" />
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <asp:HiddenField ID="HiddenFieldCompro" runat="server" />
    <asp:ModalPopupExtender ID="mpeComprobante" runat="server"
        PopupControlID="panelComprobante" TargetControlID="HiddenFieldCompro"
        BackgroundCssClass="backgroundColor">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelComprobante" runat="server" BackColor="White" Style="text-align: right" BorderWidth="1px" Width="500px">
        <div id="Div1" style="width: 500px;padding: 15px">
            <table style="width: 100%;">
                <tr>
                    <td colspan="3" style="text-align: center">Esta Seguro de Generar el&nbsp; Comprobante?
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                    <td style="text-align: center">
                        <asp:Button ID="BtnComprobante" runat="server" Text="Continuar"
                            CssClass="btn8" Width="182px" OnClick="BtnComprobante_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="BtnCancelar" runat="server" Text="Cancelar" CssClass="btn8"
                            Width="182px" OnClick="BtnCancelar_Click" />
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>


</asp:Content>
