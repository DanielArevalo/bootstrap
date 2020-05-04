<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../../../General/Controles/decimalesGridRow.ascx" tagname="decimalesGridRow" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server" />
                  
    <asp:Panel ID="pConsulta" runat="server" Style="margin-right: 0px">
        <table cellpadding="5" cellspacing="0" style="width: 99%">
            <tr>
                <td style="height: 15px; text-align: left; font-size: x-small;" colspan="4">
                    <span style="color: #0099FF;"><strong style="text-align: center; background-color: #FFFFFF;">
                        <asp:Label ID="LblMensaje" runat="server" Style="color: #FF0000; font-weight: 700;"></asp:Label>
                    </strong></span>
                </td>
            </tr>
            <tr>
                <td style="text-align: left;">
                    Numero Grupo<br />
                    <asp:TextBox ID="txtIdGrupo" runat="server" CssClass="textbox" Width="212px"></asp:TextBox>
                </td>
                <td style="text-align: left; width: 253px;">
                    &nbsp;
                </td>
                <td style="text-align: left; width: 124px;">
                    &nbsp;
                </td>
                <td style="text-align: left;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: left;" colspan="4">
                    Tipo de distribución<br />
                    <asp:DropDownList ID="ddlTipoDistribucion" runat="server" CssClass="textbox" 
                        Width="225px" AutoPostBack="true" 
                        onselectedindexchanged="ddlTipoDistribucion_SelectedIndexChanged" />
                </td>
            </tr>
        </table>
    </asp:Panel>
                   
    
    <table style="width: 100%">
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanelCuoExt" runat="server">
                    <ContentTemplate>
                        <strong>
                            <asp:Label ID="Lblerror" runat="server" CssClass="align-rt" ForeColor="Red"></asp:Label>
                        </strong>
                        <br />
                        <asp:GridView ID="gvLista" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                            DataKeyNames="COD_LINEA_aporte" ForeColor="Black" Height="61px" OnPageIndexChanging="gvLista_PageIndexChanging"
                            OnRowCommand="gvLista_RowCommand" OnRowEditing="gvLista_RowEditing" PageSize="6"
                            ShowFooter="True" Style="font-size: x-small" Width="99%" OnRowDataBound="gvLista_RowDataBound"
                            OnRowDeleting="gvLista_RowDeleting" 
                            OnRowCancelingEdit="gvLista_RowCancelingEdit" 
                            onrowupdating="gvLista_RowUpdating" >
                            <Columns>
                                <asp:TemplateField ShowHeader="False">
                                    <EditItemTemplate>
                                        &nbsp;&nbsp;<asp:ImageButton ID="btnActualizar" runat="server" CommandName="Update"
                                            ImageUrl="~/Images/gr_guardar.jpg" ToolTip="Actualizar" Width="16px" CommandArgument='<%#((GridViewRow) Container).RowIndex %>' />
                                        <asp:ImageButton ID="btnCancelar" runat="server" CommandName="Cancel" ImageUrl="~/Images/gr_cancelar.jpg"
                                            ToolTip="Cancelar" Width="16px" CommandArgument='<%#((GridViewRow) Container).RowIndex %>' />
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:ImageButton ID="btnNuevo" runat="server" CausesValidation="False" CommandName="AddNew"
                                            ImageUrl="~/Images/gr_nuevo.jpg" ToolTip="Crear Nuevo" Width="16px" />
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                                            ToolTip="Editar" Width="16px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnBorrar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg"
                                            ToolTip="Borrar" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cod Linea">
                                    <FooterTemplate>
                                        <asp:DropDownList ID="DdlLineaAporte" runat="server" AutoPostBack="True" Height="25px"
                                           OnSelectedIndexChanged="DdlLineaAporte_SelectedIndexChanged"
                                            Style="margin-bottom: 0px" Width="212px">
                                            <asp:ListItem Value="0">Seleccione un item</asp:ListItem>
                                        </asp:DropDownList>
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="DdlLineaAporteEdit" runat="server" DataSource="<%#ListaAporte() %>"
                                            DataTextField="cod_linea_aporte" DataValueField="cod_linea_aporte" SelectedValue='<%# Bind("COD_LINEA_aporte") %>'
                                            Style="text-align: center" Enabled="false">
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblcodlinea" runat="server" Text='<%# Bind("COD_LINEA_aporte") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="100px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Linea">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLinea" runat="server" Text='<%# Bind("NOMBRE") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="txtlinea" runat="server" Text='<%# Bind("COD_LINEA_aporte") %>'></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle Width="400px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Porcentaje">
                                    <EditItemTemplate>
                                        <uc2:decimalesGridRow ID="txtValor" runat="server" style="text-align:right" Text='<%# Bind("porcentaje") %>' AutoPostBack_="false" Width_="100" />
                                        <asp:Label ID="lblValorAnt" runat="server" Text='<%# Bind("porcentaje") %>' Visible="false"></asp:Label>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblValor" runat="server" Text='<%# Bind("porcentaje") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="porcentaje" HeaderText="porcentaje" />--%>
                                <asp:TemplateField HeaderText="Principal">
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="ChkprincipalEdit" runat="server" Checked='<%#Convert.ToBoolean(Eval("principal")) %>'
                                            Enabled="False" EnableViewState="true" />
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:CheckBox ID="ChkPpal" runat="server" Text='<%# Bind("principal") %>' AutoPostBack="True" />
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Chkprincipal" runat="server" Checked='<%#Convert.ToBoolean(Eval("principal")) %>'
                                            Enabled="False" EnableViewState="true" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="gridHeader" />
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridPager" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado."
                    Visible="False" />
            </td>
        </tr>
    </table>
 
</asp:Content>
