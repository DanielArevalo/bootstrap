<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctlCodeudores.ascx.cs" Inherits="General_Controles_ctlCodeudores" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<%@ Register TagPrefix="uc1" TagName="ListadoPersonas" Src="~/General/Controles/ctlBusquedaRapida.ascx" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>

<%-- variables  --%>
<asp:TextBox runat="server" ID="txtIdSOlicitante" Style="display: none"></asp:TextBox>
<asp:TextBox runat="server" ID="txtIdentificacion" Style="display: none"></asp:TextBox>
<asp:UpdatePanel runat="server">
    <ContentTemplate>

        <asp:Panel runat="server">
            <table width="100%">
                <tr>
                    <td style="text-align: left; color: #FFFFFF; background-color: #0066FF; height: 20px;" colspan="3">
                        <strong>Codeudores</strong>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <asp:GridView ID="gvListaCodeudores" runat="server" Width="100%" AutoGenerateColumns="False"
                            BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" OnRowDataBound="gvListaCodeudores_RowDataBound"
                            BorderWidth="1px" ForeColor="Black" OnRowDeleting="gvListaCodeudores_RowDeleting"
                            OnRowEditing="gvListaCodeudores_RowEditing" OnRowCancelingEdit="gvListaCodeudores_RowCancelingEdit"
                            OnRowUpdating="gvListaCodeudores_RowUpdating" OnRowCommand="gvListaCodeudores_RowCommand"
                            PageSize="5" DataKeyNames="cod_persona" ShowFooter="True" Style="font-size: x-small">
                            <Columns>
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="False" CommandName="Edit"
                                            ImageUrl="~/Images/gr_edit.jpg" ToolTip="Editar" Width="16px" Height="16px" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:ImageButton ID="btnSave" runat="server" CausesValidation="False" CommandName="Update"
                                            ImageUrl="~/Images/gr_guardar.jpg" ToolTip="Guardar" Width="16px" Height="16px" />
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:ImageButton ID="btnNuevo" runat="server" CausesValidation="False" CommandName="AddNew"
                                            ImageUrl="~/Images/gr_nuevo.jpg" ToolTip="Crear Nuevo" Width="16px" Height="16px" />
                                    </FooterTemplate>
                                    <ItemStyle Width="20px" />
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnDelete" runat="server" CausesValidation="False" CommandName="Delete"
                                            ImageUrl="~/Images/gr_elim.jpg" ToolTip="Eliminar" Width="16px" Height="16px" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:ImageButton ID="btnCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                            ImageUrl="~/Images/gr_cancelar.jpg" ToolTip="Cancelar" Width="16px" Height="16px" />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="cod_persona" HeaderText="Codpersona" />--%>
                                <asp:TemplateField HeaderText="Cod Persona">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCodPersona" runat="server" Text='<%# Bind("cod_persona") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblOrden" runat="server" Text='# Orden'></asp:Label>
                                        <asp:TextBox ID="txtOdenFooter" runat="server" Style="font-size: x-small; text-align: right" Width="55px" Enabled="false" />
                                        <asp:FilteredTextBoxExtender ID="fteOrdenFooter" runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtOdenFooter" />
                                        &nbsp;&nbsp;&nbsp;<asp:Label ID="lblCodPersonaFooter" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle Width="170px" />
                                    <FooterStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Identificación">
                                    <ItemTemplate>
                                        <asp:Label ID="lblidentificacion" runat="server" Text='<%# Bind("IDENTIFICACION") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtidentificacion" runat="server" Text='<%# Bind("IDENTIFICACION") %>'
                                            OnTextChanged="txtidentificacion_TextChanged" Style="font-size: x-small" AutoPostBack="True"></asp:TextBox>
                                        <asp:Button ID="btnConsultaPersonas" runat="server" Text="..."
                                            Height="26px" OnClick="btnConsultaPersonas_Click" />
                                        <div style="top: 0 !important; left: 0 !important;">
                                            <uc1:ListadoPersonas ID="ctlBusquedaPersonas" runat="server" />
                                        </div>

                                    </FooterTemplate>
                                    <ItemStyle Width="170px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Primer Nombre">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPrimerNombre" runat="server" Text='<%# Bind("PRIMER_NOMBRE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Segundo Nombre">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSegundoNombre" runat="server" Text='<%# Bind("SEGUNDO_NOMBRE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Primer Apellido">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPrimerApellido" runat="server" Text='<%# Bind("PRIMER_APELLIDO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Segundo Apellido">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSegundoApellido" runat="server" Text='<%# Bind("SEGUNDO_APELLIDO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dirección">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDireccionRow" runat="server" Text='<%# Bind("DIRECCION") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Teléfono">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTelefonoRow" runat="server" Text='<%# Bind("TELEFONO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Orden">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOrdenRow" runat="server" Text='<%# Bind("ORDEN") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtOrdenRow" runat="server" Text='<%# Bind("ORDEN") %>' Width="50px" Style="text-align: right" />
                                        <asp:FilteredTextBoxExtender ID="fteOrden" runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtOrdenRow" />
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="gridHeader" />
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridPager" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        <asp:Label ID="lblTotReg" runat="server" Visible="False" />
                        <asp:Label ID="lblTotalRegsCodeudores" runat="server" Text="No hay codeudores para este crédito."
                            Visible="False" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                    </td>
                </tr>
            </table>
            <asp:Label runat="server" ID="lblError" ForeColor="Red"></asp:Label>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
<div>
    <asp:TextBox runat="server" ID="txtError" Visible="False" />
</div>
<br />
<br />
<table style="width: 100%">
    <tr>
        <td style="text-align: left; color: #FFFFFF; background-color: #0066FF; height: 20px;" colspan="3">
            <strong>Referencias</strong>
        </td>
    </tr>
    <tr>
        <td style="text-align: left;">
            <asp:UpdatePanel ID="UpdatePanelReferencias" runat="server">
                <ContentTemplate>
                    <asp:Button ID="btnAgregarReferencia" runat="server" CssClass="btn8" Text="+ Adicionar Referencia" OnClick="btnAgregarReferencia_Click" />
                    <div style="overflow: auto; max-height: 200px;">
                        <asp:GridView ID="gvReferencias" runat="server" Width="100%" AutoGenerateColumns="False"
                            AllowPaging="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None"
                            OnRowCommand="gvReferencia_OnRowCommand"
                            OnRowDeleting="gvReferencias_RowDeleting"
                            BorderWidth="1px" ForeColor="Black"
                            Style="font-size: x-small; overflow: auto;">
                            <Columns>
                                <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True">
                                    <ItemStyle Width="16px" />
                                </asp:CommandField>
                                <asp:TemplateField HeaderText="Quien Referencia" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <cc1:DropDownListGrid ID="ddlQuienReferencia" runat="server" CssClass="textbox" Width="95%">
                                        </cc1:DropDownListGrid>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tipo Referencia">
                                    <ItemTemplate>
                                        <cc1:DropDownListGrid ID="ddlTipoReferencia" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>' CssClass="textbox" AppendDataBoundItems="true" AutoPostBack="true"
                                            SelectedValue='<%# Bind("tiporeferencia") %>' OnSelectedIndexChanged="ddlTipoReferencia_SelectedIndexChanged" Width="95%">
                                            <asp:ListItem Value="2" Text="Personal" />
                                            <asp:ListItem Value="1" Text="Familiar" />
                                            <asp:ListItem Value="3" Text="Comercial" />
                                        </cc1:DropDownListGrid>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nombres">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtNombres" Text='<%# Bind("nombres") %>' runat="server" Style="font-size: x-small" CssClass="textbox" Width="95%" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Parentesco" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <cc1:DropDownListGrid ID="ddlParentesco" runat="server" CssClass="textbox" AppendDataBoundItems="true" Width="95%"
                                            DataSource="<%#ListarParentesco() %>" DataTextField="ListaDescripcion" DataValueField="ListaId"
                                            SelectedValue='<%# Bind("codparentesco") %>'>
                                            <asp:ListItem Value="0" Text="" />
                                        </cc1:DropDownListGrid>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dirección" ItemStyle-Width="16%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDireccion" Text='<%# Bind("direccion") %>' runat="server" CssClass="textbox" Style="font-size: x-small" Width="95%" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Teléfono" ItemStyle-Width="8%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtTelefono" Text='<%# Bind("telefono") %>' onkeypress="return isNumber(event)" runat="server" CssClass="textbox" Style="font-size: x-small" Width="95%" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tel.Oficina" ItemStyle-Width="8%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtTelOficina" Text='<%# Bind("teloficina") %>' onkeypress="return isNumber(event)" runat="server" CssClass="textbox" Style="font-size: x-small" Width="95%" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Celular" ItemStyle-Width="8%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtCelular" Text='<%# Bind("celular") %>' onkeypress="return isNumber(event)" runat="server" CssClass="textbox" Style="font-size: x-small" Width="90%" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="gridHeader" />
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridPager" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
</table>


<script>

    function ValdiarCodeudor() {
        var identificacion_deu = $("#cphMain_Codeudores_txtIdentificacion").val();
        var identificacion_cou = $("#cphMain_Codeudores_gvListaCodeudores_txtidentificacion").val();
        debugger;

        if (identificacion_deu == identificacion_cou) {
            alert('El Solicitante no puede ser codeudor');
        }
    }
    function Recargar() {
        alert("Se recargara la pagina para agregar el nuevo codeudor.")
    }
    function Recargareli() {
        alert("Se recargara la pagina para Eliminar el codeudor.")
    }
</script>

