<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master"
    AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="~/General/Controles/decimalesGrid.ascx" TagName="decimalesGrid"
    TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <script  runat="server">
    </script>   
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>    
    <div id="table-container" style="width:70%">
    <strong style="text-align: left; color: #FFFFFF; background-color: #0066FF">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;VARIABLES PARA SCORING&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</strong></td>
    <asp:GridView ID="gvVar" runat="server" AutoGenerateColumns="False" BackColor="White"
        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="IDVARIABLE"
        ForeColor="Black" GridLines="Vertical" Height="10px" OnRowCancelingEdit="gvVar_RowCancelingEdit"
        OnRowCommand="gvVar_RowCommand" OnRowDataBound="gvVar_RowDataBound" OnRowDeleting="gvVar_RowDeleting"
        OnRowEditing="gvVar_RowEditing" OnRowUpdating="gvVar_RowUpdating" PageSize="5"
        ShowFooter="True" Style="font-size: xx-small" Width="97%">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField ShowHeader="False">
                <EditItemTemplate>
                    <asp:ImageButton ID="btnActualizar1" runat="server" CommandName="Update" ImageUrl="~/Images/gr_guardar.jpg"
                        ToolTip="Actualizar" Width="16px" CausesValidation="True" ValidationGroup="vgGuardarE" />
                    <asp:ImageButton ID="btnCancelar1" runat="server" CommandName="Cancel" ImageUrl="~/Images/gr_cancelar.jpg"
                        ToolTip="Cancelar" Width="16px" />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:ImageButton ID="btnNuevo1" runat="server" CausesValidation="True" CommandName="AddNew"
                        ImageUrl="~/Images/gr_nuevo.jpg" ToolTip="Crear Nuevo" Width="16px" ValidationGroup="vgGuardarF" />
                </FooterTemplate>
                <ItemTemplate>
                    <asp:ImageButton ID="btnEditar1" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                        ToolTip="Editar" Width="16px" />
                </ItemTemplate>
                <ItemStyle Width="16px" />
                <FooterStyle Width="16px" />
            </asp:TemplateField>
            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" >
                <FooterStyle Width="16px" />
                <ItemStyle Width="16px" />
            </asp:CommandField>
            <asp:TemplateField HeaderText="idvariable" Visible="False">
                <ItemTemplate>
                    <asp:Label ID="lblIdvariable" runat="server" Text='<%# Bind("idvariable") %>'></asp:Label>
                </ItemTemplate>
                <FooterStyle Width="40px" />
                <ItemStyle Width="40px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Variable">
                <EditItemTemplate>
                    <asp:TextBox ID="txtVariableE" runat="server" Text='<%# Bind("variable") %>' MaxLength="5"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvVariableE" runat="server" ControlToValidate="txtVariableE"
                        Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                        ValidationGroup="vgGuardarE" />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtVariableF" runat="server" MaxLength="5" Width="40" />
                    <asp:RequiredFieldValidator ID="rfvVariableF" runat="server" ControlToValidate="txtVariableF"
                        Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                        ValidationGroup="vgGuardarF" />
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblVariable" runat="server" Text='<%# Bind("variable") %>'></asp:Label>
                </ItemTemplate>
                <FooterStyle Width="40px" />
                <ItemStyle Width="40px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Nombre de la Variable">
                <EditItemTemplate>
                    <asp:TextBox ID="txtNombreE" runat="server" Text='<%# Bind("nombre") %>' MaxLength="100"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvNombreE" runat="server" ControlToValidate="txtNombreE"
                        Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                        ValidationGroup="vgGuardarE" />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtNombreF" runat="server" MaxLength="100" />
                    <asp:RequiredFieldValidator ID="rfvNombreF" runat="server" ControlToValidate="txtNombreF"
                        Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                        ValidationGroup="vgGuardarF" />
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblNombre" runat="server" Text='<%# Bind("nombre") %>'></asp:Label>
                </ItemTemplate>
                <FooterStyle Width="80px" />
                <ItemStyle Width="80px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Tipo">
                <EditItemTemplate>
                    <asp:DropDownList ID="ddlTipoE" runat="server" DataSource="<%# ListaTipo() %>" DataTextField="Nombre"
                        DataValueField="Codigo" SelectedValue='<%# Bind("tipo") %>' 
                        Style="text-align: center;">
                    </asp:DropDownList>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:DropDownList ID="ddlTipoF" runat="server" CssClass="dropdown" Width="90">
                        <asp:ListItem>Sentencia</asp:ListItem>
                        <asp:ListItem>Usuario</asp:ListItem>
                    </asp:DropDownList>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblTipo" runat="server" Text='<%# Bind("tipo") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="90px" />
                <FooterStyle Width="90px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Sentencia">
                <EditItemTemplate>
                    <asp:TextBox ID="txtSentenciaE" runat="server" Text='<%# Bind("sentencia") %>' MaxLength="500" Width="400"></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtSentenciaF" runat="server" Width="400" TextMode="SingleLine"></asp:TextBox>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblSentencia" runat="server" Text='<%# Bind("sentencia") %>' Width="540"></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="400px" />
                <FooterStyle Width="400px" />
            </asp:TemplateField>
        </Columns>
        <FooterStyle CssClass="gridHeader" />
        <HeaderStyle CssClass="gridHeader" HorizontalAlign="Center" />
        <PagerStyle CssClass="gridHeader" Font-Bold="False" />
        <RowStyle CssClass="gridItem" />
        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#FBFBF2" />
        <SortedAscendingHeaderStyle BackColor="#848384" />
        <SortedDescendingCellStyle BackColor="#EAEAD3" />
        <SortedDescendingHeaderStyle BackColor="#575357" />
    </asp:GridView>
    <asp:Label ID="lblTotalRegs0" runat="server" Visible="False" />
    <asp:Label ID="lblInfo0" runat="server" Text="Su consulta no obtuvo ningun resultado."
        Visible="False" />                        
    </div>  
</asp:Content>
