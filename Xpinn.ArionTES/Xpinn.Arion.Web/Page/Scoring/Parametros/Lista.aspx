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
    <asp:GridView ID="gvParametros" runat="server" AutoGenerateColumns="False" BackColor="White"
        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="IDPARAMETRO"
        ForeColor="Black" GridLines="Vertical" Height="10px" OnRowCancelingEdit="gvParametros_RowCancelingEdit"
        OnRowCommand="gvParametros_RowCommand" OnRowDataBound="gvParametros_RowDataBound"
        OnRowDeleting="gvParametros_RowDeleting" OnRowEditing="gvParametros_RowEditing"
        OnRowUpdating="gvParametros_RowUpdating" PageSize="5" ShowFooter="True" Style="font-size: xx-small"
        Width="97%">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField ShowHeader="False">
                <EditItemTemplate>
                    <asp:ImageButton ID="btnActualizar" runat="server" CommandName="Update" ImageUrl="~/Images/gr_guardar.jpg"
                        ToolTip="Actualizar" Width="16px" CausesValidation="True" ValidationGroup="VgGuardarE1" />
                    <asp:ImageButton ID="btnCancelar" runat="server" CommandName="Cancel" ImageUrl="~/Images/gr_cancelar.jpg"
                        ToolTip="Cancelar" Width="16px" />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:ImageButton ID="btnNuevo" runat="server" CommandName="AddNew" ImageUrl="~/Images/gr_nuevo.jpg"
                        ToolTip="Crear Nuevo" Width="16px" CausesValidation="True" ValidationGroup="VgGuardarF1" />
                </FooterTemplate>
                <ItemTemplate>
                    <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                        ToolTip="Editar" Width="16px" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />
            <asp:TemplateField HeaderText="idParametro" Visible="False">
                <ItemTemplate>
                    <asp:Label ID="lblIdParametro" runat="server" Text='<%# Bind("idparametro") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Tipo">
                <EditItemTemplate>
                    <asp:DropDownList ID="ddlTipo" runat="server" DataSource="<%# ListaTipoPar() %>"
                        DataTextField="Nombre" DataValueField="Codigo" SelectedValue='<%# Bind("tipo") %>'
                        Style="text-align: center" Width="80" AutoPostBack="True" onselectedindexchanged="ddlTipoE_SelectedIndexChanged">
                    </asp:DropDownList>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:DropDownList ID="ddlTipoF" runat="server" CssClass="dropdown" Width="80" AutoPostBack="True" onselectedindexchanged="ddlTipoF_SelectedIndexChanged">
                        <asp:ListItem>Variable</asp:ListItem>
                        <asp:ListItem>Campo</asp:ListItem>
                        <asp:ListItem>Sentencia</asp:ListItem>                        
                        <asp:ListItem>Fórmula</asp:ListItem>
                    </asp:DropDownList>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblTipo" runat="server" Text='<%# Bind("tipo") %>' Width="80"></asp:Label>
                </ItemTemplate>
                <FooterStyle Width="80px" />
                <ItemStyle Width="80px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Nombre del Parametro">
                <EditItemTemplate>
                    <asp:TextBox ID="txtNombre" runat="server" Text='<%# Bind("nombre") %>' MaxLength="150"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvNombreE" runat="server" ControlToValidate="txtNombre"
                        Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                        ValidationGroup="vgGuardarE1" />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtNombreF" runat="server" MaxLength="150" />
                    <asp:RequiredFieldValidator ID="rfvNombreF" runat="server" ControlToValidate="txtNombreF"
                        Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                        ValidationGroup="vgGuardarF1" />
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblNombre" runat="server" Text='<%# Bind("nombre") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Left" />
            </asp:TemplateField>
           <asp:TemplateField HeaderText="Variable">
                <EditItemTemplate>
                    <asp:DropDownList ID="ddlVariable" runat="server" DataSource="<%# ListaVariable() %>"
                        DataTextField="nombre" DataValueField="idvariable" SelectedValue='<%# Bind("idvariable") %>'
                        Style="text-align: center" Width="80px" AppendDataBoundItems="True">
                        <asp:ListItem Value="0">.</asp:ListItem>
                    </asp:DropDownList>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:DropDownList ID="ddlVariableF" runat="server" 
                        DataSource="<%# ListaVariable() %>" DataTextField="nombre" DataValueField="idvariable" SelectedValue='<%# Bind("idvariable") %>' 
                        Style="text-align: center" Width="80px" AppendDataBoundItems="True">
                    </asp:DropDownList>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblVariable" runat="server" Text='<%# Bind("idvariable") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Campo">
                <EditItemTemplate>
                    <asp:DropDownList ID="ddlCampo" runat="server" DataSource="<%# ListaCampo() %>"
                        DataTextField="Nombre" DataValueField="Nombre" SelectedValue='<%# Bind("Campo") %>'
                        Style="text-align: center" Width="120px" AppendDataBoundItems="True">
                        <asp:ListItem Value=" "> </asp:ListItem>
                    </asp:DropDownList>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:DropDownList ID="ddlCampoF" runat="server" DataSource="<%# ListaCampo() %>" 
                        DataTextField="Nombre" DataValueField="Nombre" SelectedValue='<%# Bind("Campo") %>' 
                        Style="text-align: center" Width="120px" AppendDataBoundItems="True">                        
                        <asp:ListItem Value=" "> </asp:ListItem>
                    </asp:DropDownList>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblCampo" runat="server" Text='<%# Bind("Campo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Sentencia">
                <EditItemTemplate>
                    <asp:TextBox ID="txtSentencia" runat="server" Text='<%# Bind("sentencia") %>' Width="400"></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtSentenciaF" runat="server" Width="400" TextMode="SingleLine"></asp:TextBox>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblSentencia" runat="server" Text='<%# Bind("sentencia") %>' Width="400"></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="400px" />
                <FooterStyle Width="400px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Formula">
                <EditItemTemplate>
                    <asp:TextBox ID="txtFormula" runat="server" Text='<%# Bind("formula") %>' MaxLength="200"></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtFormulaF" runat="server"></asp:TextBox>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblFormula" runat="server" Text='<%# Bind("formula") %>'></asp:Label>
                </ItemTemplate>
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
    <asp:Label ID="lblTotalRegs1" runat="server" Visible="False" />
    <asp:Label ID="lblInfo1" runat="server" Text="Su consulta no obtuvo ningun resultado."
        Visible="False" />
    </div>                   
</asp:Content>
