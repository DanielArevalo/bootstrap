<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="~/General/Controles/decimalesGrid.ascx" TagName="decimalesGrid" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvMatrizRiesgo" runat="server" ActiveViewIndex="0">
        <asp:View ID="vw0" runat="server">
            <asp:Panel ID="pConsulta" runat="server" Style="margin-bottom: 0px">                
                <table style="width: 50%;">
                    <tr>
                        <td colspan="4">                            
                            <asp:Label ID="Lblerror" runat="server" CssClass="align-rt" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:left; height: 25px;" colspan="4">                            
                            <asp:Label ID="LblTitulo" runat="server" Text="Paramétros de Búsqueda" 
                                style="font-weight: 700; font-size: small;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:left">
                            Clasificación<br />
                            <asp:DropDownList ID="ddlClasificacionf" runat="server">
                            </asp:DropDownList>
                            <br />
                        </td>
                        <td style="text-align:left">
                            Tipo de Persona<br />
                            <asp:DropDownList ID="ddlTipoPersonaf" runat="server" Width="321px" AppendDataBoundItems="True">
                                <asp:ListItem Value="0">Sin Tipo</asp:ListItem>
                                <asp:ListItem Value="N">Natural</asp:ListItem>
                                <asp:ListItem Value="J">Juridica</asp:ListItem>
                            </asp:DropDownList>
                            <br />
                        </td>
                        <td style="text-align:left">
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <table style="width: 100%;">
                <tr>
                    <td style="text-align:left; height: 25px;" colspan="3">                            
                        <asp:Label ID="lblTituloLista" runat="server" Text="Listado de Matrices de Riesgo" 
                            style="font-weight: 700; font-size: small;"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="height: 25px; font-weight: 700;" colspan="4">
                        <strong>
                            <asp:GridView ID="gvMatrizRiesgo" runat="server" Width="97%" AutoGenerateColumns="False"
                                PageSize="5" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                                CellPadding="4" ForeColor="Black" GridLines="Vertical" Height="10px" ShowFooter="True"
                                Style="font-size: xx-small" OnRowDataBound="gvMatrizRiesgo_RowDataBound" OnRowEditing="gvMatrizRiesgo_RowEditing"
                                DataKeyNames="IDMATRIZ" OnRowCommand="gvMatrizRiesgo_RowCommand" OnSelectedIndexChanged="gvMatrizRiesgo_SelectedIndexChanged">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                                                ToolTip="Editar" Width="16px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True"
                                        Visible="false" />
                                    <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                                                ToolTip="Detalle" Width="16px" />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="IDMATRIZ" Visible="true">
                                        <ItemTemplate>
                                            <strong>
                                                <asp:Label ID="lblconsecutivo" runat="server" Text='<%# Bind("IDMATRIZ") %>'></asp:Label>
                                            </strong>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cod.Clasificación" Visible="False">
                                        <FooterTemplate>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCOD_CLASIFICA" runat="server" Text='<%# Bind("COD_CLASIFICA") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Clasificación">
                                        <FooterTemplate>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCLASIFICA" runat="server" Text='<%# Bind("NOM_CLASIFICA") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cod.Tipo Persona">
                                        <FooterTemplate>
                                        </FooterTemplate>
                                        <ItemTemplate>                                            
                                            <asp:Label ID="lblTIPO_PERSONA" runat="server" Text='<%# Bind("TIPO_PERSONA") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tipo Persona">
                                        <FooterTemplate>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblNOM_TIPO_PERSONA" runat="server" Text='<%# Bind("NOM_TIPO_PERSONA") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fecha Creación">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFECHACREACION" runat="server" Text='<%# Bind("FECHACREACION") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Usuario Creación">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUSUARIOCREACION" runat="server" Text='<%# Bind("USUARIOCREACION") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fecha Ult. Modif.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFECULTMOD" runat="server" Text='<%# Bind("FECULTMOD") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Usuario Ult. Modif.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUSUULTMOD" runat="server" Text='<%# Bind("USUULTMOD") %>'></asp:Label>
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
                        </strong>
                    </td>
                </tr>
                <tr>
                    <td style="height: 25px" colspan="4">
                        <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                        <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningun resultado." Visible="False" />
                    </td>
                </tr>
                <tr>
                    <td style="height: 25px" colspan="3">
                        &nbsp;
                    </td>
                    <td style="height: 25px">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="vw1" runat="server">
            <br />
            <table style="width: 80%;">
                <tr>
                    <td style="text-align: center;color: #FFFFFF; background-color: #0066FF; height: 20px;" colspan="4">
                        &nbsp;<strong style="text-align: center">MATRIZ DE RIESGO</strong>
                    </td>
                </tr>
                <tr>
                    <td>
                        Código<br />
                        <asp:Label ID="lblCodigo" runat="server"></asp:Label>
                        <br />
                    </td>
                    <td>
                        Clasificación<br />
                        <asp:DropDownList ID="ddlClasificacionC" runat="server" CssClass="dropdown" ValidationGroup="vgGuardar" Width="200px">
                            <asp:ListItem Value="0">Sin Clasificación</asp:ListItem>
                        </asp:DropDownList>
                        <br />
                        <asp:RequiredFieldValidator ID="rfvClasificacionC" runat="server" ControlToValidate="ddlClasificacionC"
                            Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                            ValidationGroup="vgGuardar" />
                    </td>
                    <td>
                        Tipo de Persona<br />
                        <asp:DropDownList ID="ddlTipoPersonaC" runat="server" CssClass="dropdown" ValidationGroup="vgGuardar"  Width="200px"
                            AppendDataBoundItems="True">
                            <asp:ListItem Value="0">Sin Tipo</asp:ListItem>
                            <asp:ListItem Value="N">Natural</asp:ListItem>
                            <asp:ListItem Value="J">Juridica</asp:ListItem>
                        </asp:DropDownList>
                        <br />
                        <asp:RequiredFieldValidator ID="rfvTipoPersonaC" runat="server" ControlToValidate="ddlTipoPersonaC"
                            Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                            ValidationGroup="vgGuardar" />
                    </td>                    
                    <td style="text-align: right">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                    </td>
                </tr>
            </table>
            <table style="width: 80%;">
                <tr>
                    <td colspan="4" style="font-weight: 700; text-align: left">
                        VARIABLES
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="font-weight: 700">
                        <asp:GridView ID="gvVariables" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" 
                            BorderWidth="1px" CellPadding="4" DataKeyNames="IDFACTORPONDERA" ForeColor="Black" GridLines="Vertical" Height="10px" 
                            OnRowCommand="gvVariables_RowCommand" OnRowDataBound="gvVariables_RowDataBound" OnRowCancelingEdit="gvVariables_RowCancelingEdit"
                            OnRowEditing="gvVariables_RowEditing" OnRowUpdating="gvVariables_RowUpdating" OnRowDeleting="gvVariables_RowDeleting"
                            PageSize="5" ShowFooter="True" Style="font-size: xx-small"
                            Width="97%">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField ShowHeader="False">
                                    <EditItemTemplate>
                                        <asp:ImageButton ID="btnActualizar" runat="server" CommandName="Update" ImageUrl="~/Images/gr_guardar.jpg"
                                            ToolTip="Actualizar" Width="16px" />
                                        <asp:ImageButton ID="btnCancelar" runat="server" CommandName="Cancel" ImageUrl="~/Images/gr_cancelar.jpg"
                                            ToolTip="Cancelar" Width="16px" />
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
                                <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />
                                <asp:TemplateField HeaderText="IDFACTORPONDERA" Visible="false">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtIdFactorPondera" runat="server" Style="margin-top: 0px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblIdFactorPondera" runat="server" Text='<%# Bind("IDFACTORPONDERA") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Parámetro">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlParametro" runat="server" DataSource="<%# ListaParametros() %>"
                                            DataTextField="nombre" DataValueField="idparametro" SelectedValue='<%# Bind("IDPARAMETRO") %>'
                                            Style="text-align: center" Width="180px">
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ddlParametroF" runat="server" 
                                            DataSource="<%# ListaParametros() %>" DataTextField="nombre" DataValueField="idparametro" SelectedValue='<%# Bind("IDPARAMETRO") %>' 
                                            Style="text-align: center" Width="180px" >
                                        </asp:DropDownList>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblParametro" runat="server" Text='<%# Bind("DESCRIPCIONPARAMETRO") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Factor Ponderación">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtFactorE" runat="server" Text='<%# Bind("FACTOR") %>' />
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtFactorF" runat="server" />
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="lblFactor" runat="server" Text='<%# Bind("FACTOR") %>' Enabled="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mínimo">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtMinimoE" runat="server" Text='<%# Bind("MINIMO") %>' />
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtMinimoF" runat="server" />
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="lblMinimo" runat="server" Text='<%# Bind("MINIMO") %>' Enabled="False" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Máximo">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtMaximoE" runat="server" Text='<%# Bind("MAXIMO") %>' />
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtMaximoF" runat="server" />
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="lblMaximo" runat="server" Text='<%# Bind("MAXIMO") %>' Enabled="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Variable">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtVariableE" runat="server" Text='<%# Bind("VARIABLE") %>' Width="60px" CssClass="textbox" />
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtVariableF" runat="server" Width="60px" CssClass="textbox" />
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="lblVariablea" runat="server" Text='<%# Bind("VARIABLE") %>' Enabled="false" Width="50px" CssClass="textbox" />
                                    </ItemTemplate>
                                    <FooterStyle Width="50px" HorizontalAlign="Right" />
                                    <ItemStyle Width="50px" HorizontalAlign="Right" />
                                </asp:TemplateField>                                
                                <asp:TemplateField HeaderText="Calificación">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtCalificacionE" runat="server" Text='<%# Bind("CALIFICACION") %>' Width="60px" CssClass="textbox" />
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtCalificacionF" runat="server" Width="60px" CssClass="textbox" />
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="lblCalificacion" runat="server" Text='<%# Bind("CALIFICACION") %>' Enabled="false" Width="50px" CssClass="textbox" />
                                    </ItemTemplate>
                                    <FooterStyle Width="50px" HorizontalAlign="Right" />
                                    <ItemStyle Width="50px" HorizontalAlign="Right" />
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
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="font-weight: 700">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
</asp:Content>
