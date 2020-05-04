<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar"
    TagPrefix="uc4" %>
<%@ Register Src="../../../General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas"
    TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlProveedor.ascx" TagName="BuscarProveedor" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvAplicar" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwLista" runat="server">
            <table style="width: 520px; text-align: center" cellspacing="0" cellpadding="0">
                <tr>
                    <td style="text-align: left; width: 180px">
                        Num. Auxilio<br />
                        <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="100px"></asp:TextBox>
                    </td>
                    <td style="text-align: left; width: 140px">
                        Fec. Solicitud<br />
                        <ucFecha:fecha ID="txtFecha" runat="server" CssClass="textbox" />
                    </td>
                    <td style="text-align: left; width: 200px">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 180px">
                        Asociado<br />
                        <asp:TextBox ID="txtCodPersona" runat="server" CssClass="textbox" ReadOnly="True"
                            Width="50px" Visible="false" />
                        <asp:TextBox ID="txtIdPersona" runat="server" CssClass="textbox" AutoPostBack="true"
                            Width="100px" OnTextChanged="txtIdPersona_TextChanged" />
                        <asp:Button ID="btnConsultaPersonas" runat="server" CssClass="btn8" Height="26px"
                            OnClick="btnConsultaPersonas_Click" Text="..." />
                    </td>
                    <td style="text-align: left; width: 340px" colspan="2">
                        Nombre<br />
                        <uc1:ListadoPersonas ID="ctlBusquedaPersonas" runat="server" />
                        <asp:TextBox ID="txtNomPersona" runat="server" CssClass="textbox" ReadOnly="True"
                            Width="270px" />
                        <asp:RequiredFieldValidator ID="rfvEncargado" runat="server" ControlToValidate="txtNomPersona"
                            Display="Dynamic" ErrorMessage="Seleccione encargado" ForeColor="Red" InitialValue="0"
                            Style="font-size: xx-small" ValidationGroup="vgGuardar" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 180px">
                        Linea de Auxilio<br />
                        <asp:DropDownList ID="ddlLinea" runat="server" CssClass="textbox" Width="95%" OnSelectedIndexChanged="ddlLinea_SelectedIndexChanged"
                            AutoPostBack="True" />
                    </td>
                    <td style="text-align: left; width: 140px">
                        Cupos<br />
                        <asp:TextBox ID="txtCupos" runat="server" CssClass="textbox" Width="90%" />
                    </td>
                    <td style="text-align: left; width: 200px">
                        Monto Disponible<br />
                        <uc1:decimales ID="txtMontoDisp" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 180px">
                        <asp:Panel ID="panelValorMatri" runat="server">
                            Valor Matricula<br />
                            <asp:Label ID="lblPorcMATRI" runat="server" Visible="false" />
                            <uc1:decimales ID="txtValorMatricula" runat="server" />
                            <br />
                        </asp:Panel>
                        Valor Solicitado<br />
                        <uc1:decimales ID="txtValorSoli" runat="server" />
                    </td>
                    <td style="text-align: left; width: 340px; vertical-align: top" colspan="2">
                        Detalle<br />
                        <asp:TextBox ID="txtDetalle" runat="server" CssClass="textbox" Width="290px" TextMode="MultiLine" />
                    </td>
                </tr>
            </table>

            <uc1:BuscarProveedor ID="ctlBusquedaProveedor" runat="server" />
            <%-- 
            <asp:Panel ID="panelProveedor" runat="server">
                <table>
                    <tr>
                        <td colspan="2" style="text-align: left;">
                            <span style="font-weight: bold">
                                <asp:Label ID="lblTitOrden" runat="server" Text="Proveedor para La Orden de Servicio:" /></span>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left;">
                        <asp:Label ID="lblCodAuxOrden" runat="server" visible="false"/>
                            <asp:Label ID="lblTitIdenProveedor" runat="server" Text="Identificación Proveedor" />
                            <br />
                            <asp:TextBox ID="txtIdentificacionprov" runat="server" CssClass="textbox" AutoPostBack="true"
                                Width="170px" MaxLength="20" OnTextChanged="txtIdentificacionprov_TextChanged"></asp:TextBox>
                            <cc1:ButtonGrid ID="btnListadoPersona" CssClass="btnListado" runat="server" Text="..."
                                OnClick="btnListadoPersona_Click" />
                            <uc1:ListadoPersonas ID="ListadoPersonas1" runat="server" />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblTitNomProveedor" runat="server" Text="Nombre Proveedor" />
                            <br />
                            <asp:TextBox ID="txtNombreProveedor" runat="server" CssClass="textbox" Enabled="false"
                                Width="455px" MaxLength="200"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
                --%>
            <asp:Panel ID="panelRequisitos" runat="server">
            <hr style="width: 100%" />
                <table style="width: 740px">                    
                    <tr>
                        <td style="width: 740px; text-align: left">
                            <strong>Validación de Requisitos</strong><br />
                            <asp:GridView ID="gvValidacion" runat="server" PageSize="20" ShowHeaderWhenEmpty="True"
                                AutoGenerateColumns="False" SelectedRowStyle-Font-Size="XX-Small" Style="font-size: small;
                                margin-bottom: 0px;" OnRowDataBound="gvBeneficiarios_RowDataBound" OnRowDeleting="gvBeneficiarios_RowDeleting"
                                GridLines="Horizontal">
                                <Columns>
                                    <asp:TemplateField HeaderText="Nro">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCodRequisito" runat="server" Text='<%# Bind("codrequisitoaux") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Codigo" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCodigo" runat="server" Text='<%# Bind("CODREQUISITOAUXILIO") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Descripción">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDescripcion" runat="server" Text='<%# Bind("descripcion") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" Width="200px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Aceptado" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <cc1:CheckBoxGrid ID="chkAceptado" runat="server" Checked='<%#Convert.ToBoolean(Eval("aceptado"))%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="gridHeader" />
                                <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                <RowStyle CssClass="gridItem" />
                                <SelectedRowStyle Font-Size="XX-Small"></SelectedRowStyle>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="panelGrilla" runat="server">
            <hr style="width: 100%" />
                <table style="width: 740px">                    
                    <tr>
                        <td style="width: 740px; text-align: left">
                            <strong>Beneficiarios</strong><br />
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btnAdicionarFila" runat="server" CssClass="btn8" OnClick="btnAdicionarFila_Click"
                                        OnClientClick="btnAdicionarFila_Click" Text="+ Adicionar Beneficiario" Height="22px" />
                                    <asp:GridView ID="gvBeneficiarios" runat="server" PageSize="20" ShowHeaderWhenEmpty="True"
                                        AutoGenerateColumns="False" SelectedRowStyle-Font-Size="XX-Small" Style="font-size: small;
                                        margin-bottom: 0px;" OnRowDataBound="gvBeneficiarios_RowDataBound" OnRowDeleting="gvBeneficiarios_RowDeleting"
                                        DataKeyNames="codbeneficiarioaux" GridLines="Horizontal">
                                        <Columns>
                                            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True">
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:CommandField>
                                            <asp:TemplateField HeaderText="Codigo" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCodigo" runat="server" CssClass="textbox" Text='<%# Bind("codbeneficiarioaux") %>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Identificación" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <cc1:TextBoxGrid ID="txtIdenti_Grid" runat="server"  CssClass="textbox" Text='<%# Bind("identificacion") %>'
                                                        Width="110px"></cc1:TextBoxGrid>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Nombres y Apellidos" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <cc1:TextBoxGrid ID="txtNombreComple" runat="server" CssClass="textbox" Text='<%# Bind("nombre") %>' OnTextChanged="txtNombreComple_ontextchanged" AutoPostBack="true"
                                                        Width="250px" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Parentesco">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblParentesco" runat="server" Text='<%# Bind("cod_parentesco") %>'
                                                        Visible="false" /><cc1:DropDownListGrid ID="ddlParentesco" runat="server" CommandArgument="<%#Container.DataItemIndex %>"
                                                            CssClass="textbox" Width="170px">
                                                        </cc1:DropDownListGrid>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="%Beneficiario">
                                                <ItemTemplate>
                                                    <cc1:TextBoxGrid ID="txtPorcBene" runat="server" CssClass="textbox" Style="text-align: right"
                                                        Text='<%# Bind("porcentaje_beneficiario") %>' Width="100px" />
                                                    <asp:FilteredTextBoxExtender ID="fte3" runat="server" FilterType="Custom, Numbers"
                                                        TargetControlID="txtPorcBene" ValidChars="+-=/*()." />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="gridHeader" />
                                        <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                        <RowStyle CssClass="gridItem" />
                                        <SelectedRowStyle Font-Size="XX-Small"></SelectedRowStyle>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 740px; text-align: center">
                            <asp:Label ID="lblTotalReg" runat="server" Visible="False" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
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
                        <td style="text-align: center; font-size: large; color: Red">
                            La solicitud de auxilio fue
                            <asp:Label ID="lblmsj" runat="server"></asp:Label>
                            &nbsp;correctamente.<br />
                            Nro. de Auxilio :
                            <asp:Label ID="lblNroMsj" runat="server"></asp:Label>
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
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
