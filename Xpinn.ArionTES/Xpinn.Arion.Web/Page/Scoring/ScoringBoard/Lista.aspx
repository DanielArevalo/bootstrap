<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="~/General/Controles/decimalesGrid.ascx" TagName="decimalesGrid" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvScoringBoard" runat="server" ActiveViewIndex="0">
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
                            Línes de Crédito<br />
                            <asp:DropDownList ID="ddlLineasf" runat="server" Width="321px" AppendDataBoundItems="True">
                                <asp:ListItem Value="0">Sin Linea</asp:ListItem>
                            </asp:DropDownList>
                            <br />
                        </td>
                        <td style="text-align:left">
                            Clase de Scoring<br />
                            <asp:DropDownList ID="ddlClaseScoring" runat="server" Width="139px" AppendDataBoundItems="True" >
                                <asp:ListItem Value="0">Sin Clase</asp:ListItem>
                                <asp:ListItem Value="1">De Aprobación</asp:ListItem>
                                <asp:ListItem Value="2">De Seguimiento</asp:ListItem>
                                <asp:ListItem Value="3">De Segmentacion</asp:ListItem>
                            </asp:DropDownList>
                            <br />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <table style="width: 100%;">
                <tr>
                    <td style="text-align:left; height: 25px;" colspan="3">                            
                        <asp:Label ID="lblTituloLista" runat="server" Text="Listado de Score Cards" 
                            style="font-weight: 700; font-size: small;"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="height: 25px; font-weight: 700;" colspan="4">
                        <strong>
                            <asp:GridView ID="gvScoring" runat="server" Width="97%" AutoGenerateColumns="False"
                                PageSize="5" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                                CellPadding="4" ForeColor="Black" GridLines="Vertical" Height="10px" ShowFooter="True"
                                Style="font-size: xx-small" OnRowDataBound="gvScoring_RowDataBound" OnRowEditing="gvScoring_RowEditing"
                                DataKeyNames="IDSCORE" OnRowCommand="gvScoring_RowCommand" OnSelectedIndexChanged="gvScoring_SelectedIndexChanged">
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
                                    <asp:TemplateField HeaderText="IDSCORE" Visible="true">
                                        <ItemTemplate>
                                            <strong>
                                                <asp:Label ID="lblconsecutivo" runat="server" Text='<%# Bind("IDSCORE") %>'></asp:Label>
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
                                            <asp:Label ID="lblCLASIFICA" runat="server" Text='<%# Bind("DESCRIPCION") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cod.Línea Crédito">
                                        <FooterTemplate>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCOD_LINEA_CREDITO" runat="server" Text='<%# Bind("COD_LINEA_CREDITO") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Línea de Crédito">
                                        <FooterTemplate>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblLINEA_CREDITO" runat="server" Text='<%# Bind("NOMBRE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Aplica a">
                                        <FooterTemplate>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblAPLICA_A" runat="server" Text='<%# Bind("APLICA_A") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Modelo">
                                        <FooterTemplate>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblMODELO" runat="server" Text='<%# Bind("MODELO") %>'></asp:Label>
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
                                    <asp:TemplateField HeaderText="Beta 0">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBETA0" runat="server" Text='<%# Bind("BETA0") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Score Máximo">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSCORE" runat="server" Text='<%# Bind("score_maximo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Clase de Scoring">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCLASE" runat="server" Text='<%# Bind("clase") %>'></asp:Label>
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
                    <td style="text-align: center;color: #FFFFFF; background-color: #0066FF; height: 20px;" colspan="6">
                        &nbsp;<strong style="text-align: center">SCORED BOARD</strong>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        Código<br />
                        <asp:Label ID="lblCodigo" runat="server"></asp:Label>
                        <br />
                    </td>
                    <td style="width: 50%">
                        Clasificación<br />
                        <asp:DropDownList ID="ddlClasificacionC" runat="server" CssClass="dropdown" ValidationGroup="vgGuardar" style="width:100%">
                            <asp:ListItem Value="0">Sin Clasificación</asp:ListItem>
                        </asp:DropDownList>
                        <br />
                        <asp:RequiredFieldValidator ID="rfvClasificacionC" runat="server" ControlToValidate="ddlClasificacionC"
                            Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                            ValidationGroup="vgGuardar" />
                    </td>
                    <td colspan="3" style="width: 30%">
                        Línea de Crédito<br />
                        <asp:DropDownList ID="ddlLineasC" runat="server" CssClass="dropdown" ValidationGroup="vgGuardar"
                            AppendDataBoundItems="True" Width="100%">
                            <asp:ListItem Value="0">Sin Linea</asp:ListItem>
                        </asp:DropDownList>
                        <br />
                        <asp:RequiredFieldValidator ID="rfvLineasC" runat="server" ControlToValidate="ddlLineasC"
                            Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                            ValidationGroup="vgGuardar" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        Aplica a<br />
                        <asp:DropDownList ID="ddlAPLICA_AC" runat="server" CssClass="textbox" Width="100%">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>Ambos</asp:ListItem>
                            <asp:ListItem>Deudor</asp:ListItem>
                            <asp:ListItem>Codeudor</asp:ListItem>
                        </asp:DropDownList>
                        <br />
                    </td>
                    <td>
                        Modelo de Análisis<br />
                        <asp:DropDownList ID="ddlMODELOC" runat="server" CssClass="textbox" Width="100%">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>Credit Scoring</asp:ListItem>
                            <asp:ListItem>Regresión Logística</asp:ListItem>
                        </asp:DropDownList>
                        <br />
                    </td>
                    <td style="text-align: Center; width: 120px;">
                        Beta Cero<br />
                        <asp:TextBox ID="txtBeta0" runat="server" Width="100px" CssClass="textbox"></asp:TextBox>
                    </td>
                    <td style="text-align: Left; width: 120px;">
                        Score Máximo<br />
                        <asp:TextBox ID="txtScoreMaximo" runat="server" Width="100px" CssClass="textbox"></asp:TextBox>
                    </td>
                    <td style="text-align: Left">
                        Clase de Scoring<br />
                        <asp:DropDownList ID="ddlCLASESCORE" runat="server" Width="139px" AppendDataBoundItems="True"  CssClass="textbox" >
                            <asp:ListItem Value="0">Sin Clase</asp:ListItem>
                            <asp:ListItem Value="1">De Aprobación</asp:ListItem>
                            <asp:ListItem Value="2">De Seguimiento</asp:ListItem>                            
                            <asp:ListItem Value="3">De Segmentación</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: right">
                        &nbsp;
                        <asp:ImageButton ID="btnReporte" runat="server" ImageUrl="~/Images/btnImprimir.jpg"
                            OnClick="btnReporte_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                    </td>
                </tr>
            </table>
            <table style="width: 80%;">
                <tr>
                    <td colspan="3" style="font-weight: 700; text-align: left">
                        VARIABLES
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="font-weight: 700">
                        <asp:GridView ID="gvVariables" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" 
                            BorderWidth="1px" CellPadding="4" DataKeyNames="IDSCOREVAR" ForeColor="Black" GridLines="Vertical" Height="10px" 
                            OnRowCommand="gvVariables_RowCommand" OnRowDataBound="gvVariables_RowDataBound" OnRowCancelingEdit="gvVariables_RowCancelingEdit"
                            OnRowEditing="gvVariables_RowEditing" OnRowUpdating="gvVariables_RowUpdating" OnRowDeleting="gvVariables_RowDeleting"
                            PageSize="5" ShowFooter="True" Style="font-size: xx-small">
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
                                <asp:TemplateField HeaderText="IDSCOREVAR" Visible="false">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtconsecutivo" runat="server" Style="margin-top: 0px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblIdscorevar" runat="server" Text='<%# Bind("IDSCOREVAR") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="IDSCORE" Visible="false">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtIdscoreE" runat="server" Style="margin-top: 0px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblIdscore" runat="server" Text='<%# Bind("IDSCORE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Parámetro">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlVariable" runat="server" DataSource="<%# ListaVariable() %>"
                                            DataTextField="nombre" DataValueField="idparametro" SelectedValue='<%# Bind("IDPARAMETRO") %>'
                                            Style="text-align: center" Width="80px">
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ddlVariableF" runat="server" 
                                            DataSource="<%# ListaVariable() %>" DataTextField="nombre" DataValueField="idparametro" SelectedValue='<%# Bind("IDPARAMETRO") %>' 
                                            Style="text-align: center" Width="80px" >
                                        </asp:DropDownList>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblVariable" runat="server" Text='<%# Bind("DESCRIPCIONPARAMETRO") %>'></asp:Label>
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
                                <asp:TemplateField HeaderText="Valor">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtValorE" runat="server" Text='<%# Bind("VALOR") %>' />
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtValorF" runat="server" />
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="lblValor" runat="server" Text='<%# Bind("VALOR") %>' Enabled="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Beta">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtBetaE" runat="server" Text='<%# Bind("BETA") %>' Width="60px" CssClass="textbox" />
                                        <asp:FilteredTextBoxExtender ID="ftbeBetaE" runat="server" TargetControlID="txtBetaE"         
                                            FilterType="Custom, Numbers" ValidChars="0123456789.," />
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtBetaF" runat="server" Width="60px" CssClass="textbox" />
                                        <asp:FilteredTextBoxExtender ID="ftbeBetaF" runat="server" TargetControlID="txtBetaF"         
                                            FilterType="Custom, Numbers" ValidChars="0123456789.," />
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="lblBeta" runat="server" Text='<%# Bind("BETA") %>' Enabled="false" Width="50px" CssClass="textbox" />
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
            <table style="width: 80%;">
                <tr>
                    <td colspan="3" style="font-weight: 700; text-align: left">
                        CALIFICACIONES
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="font-weight: 700">
                        <asp:GridView ID="gvCalificaciones" runat="server" AutoGenerateColumns="False" BackColor="White"
                            BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="IDSCORECAL"
                            ForeColor="Black" GridLines="Vertical" Height="10px" OnRowCancelingEdit="gvCalificaciones_RowCancelingEdit"
                            OnRowCommand="gvCalificaciones_RowCommand" OnRowDataBound="gvCalificaciones_RowDataBound"
                            OnRowDeleting="gvCalificaciones_RowDeleting" OnRowEditing="gvCalificaciones_RowEditing"
                            OnRowUpdating="gvCalificaciones_RowUpdating" PageSize="5" ShowFooter="True" Style="font-size: xx-small">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField ShowHeader="False">
                                    <EditItemTemplate>
                                        <asp:ImageButton ID="btnActualizar1" runat="server" CommandName="Update" ImageUrl="~/Images/gr_guardar.jpg"
                                            ToolTip="Actualizar" Width="16px" />
                                        <asp:ImageButton ID="btnCancelar1" runat="server" CommandName="Cancel" ImageUrl="~/Images/gr_cancelar.jpg"
                                            ToolTip="Cancelar" Width="16px" />
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:ImageButton ID="btnNuevo1" runat="server" CausesValidation="False" CommandName="AddNew"
                                            ImageUrl="~/Images/gr_nuevo.jpg" ToolTip="Crear Nuevo" Width="16px" />
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEditar1" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                                            ToolTip="Editar" Width="16px" />
                                    </ItemTemplate>
                                    <ItemStyle Width="16px" />
                                </asp:TemplateField>
                                <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" 
                                    ShowDeleteButton="True"  >
                                <ItemStyle Width="16px" />
                                </asp:CommandField>
                                <asp:TemplateField HeaderText="IDSCORE" Visible="false">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtIdscoreE0" runat="server" Style="margin-top: 0px" Width="50"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblIdscore0" runat="server" Text='<%# Bind("IDSCORE") %>' Width="50"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="50px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mínimo">
                                    <EditItemTemplate>
                                        <uc1:decimalesGrid ID="txtMinimoE0" runat="server" Text='<%# Bind("CAL_MINIMO") %>' Width="30" />
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <uc1:decimalesGrid ID="txtMinimoF0" runat="server" Width="30" />
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <uc1:decimalesGrid ID="lblMinimo0" runat="server" Enabled="false" Text='<%# Bind("CAL_MINIMO") %>' Width="30" />
                                    </ItemTemplate>
                                    <FooterStyle Width="30px" />
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle Width="30px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Máximo">
                                    <EditItemTemplate>
                                        <uc1:decimalesGrid ID="txtMaximoE0" runat="server" Text='<%# Bind("CAL_MAXIMO") %>' Width="50" />
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <uc1:decimalesGrid ID="txtMaximoF0" runat="server" Width="50" />
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <uc1:decimalesGrid ID="lblMaximo0" runat="server" Enabled="false" Text='<%# Bind("CAL_MAXIMO") %>' Width="50" />
                                    </ItemTemplate>
                                    <FooterStyle Width="30px" />
                                    <ItemStyle Width="50px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Calificación">
                                    <EditItemTemplate>
                                        <uc1:decimalesGrid ID="txtCalificacionE" runat="server" Text='<%# Bind("CALIFICACION") %>' Width="50" />
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <uc1:decimalesGrid ID="txtCalificacionF" runat="server" Width="50" />
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <uc1:decimalesGrid ID="lblCalificacion" runat="server" Enabled="false" Text='<%# Bind("CALIFICACION") %>' Width="50"/>
                                    </ItemTemplate>
                                    <ItemStyle Width="50px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tipo">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlTipoE" runat="server" DataSource="<%# ListaTipo() %>" DataTextField="Nombre"
                                            DataValueField="Codigo" SelectedValue='<%# Bind("TIPO") %>' Style="text-align: center" Width="80">
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ddlTipoF" runat="server" CssClass="dropdown" Width="80">
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem>Negación</asp:ListItem>
                                            <asp:ListItem>Reevaluación</asp:ListItem>
                                            <asp:ListItem>Aprobación</asp:ListItem>
                                        </asp:DropDownList>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTipo" runat="server" Text='<%# Bind("TIPO") %>' Width="80"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="80px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Observación" ItemStyle-HorizontalAlign="Left">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtObservacionE" runat="server" Text='<%# Bind("OBSERVACION") %>' Width="200"></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtObservacionF" runat="server" Width="200"></asp:TextBox>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblObservacion" runat="server" Text='<%# Bind("OBSERVACION") %>' Width="200"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="200px" />
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
            </table>
        </asp:View>
        <asp:View ID="vw2" runat="server">
            <rsweb:ReportViewer ID="rvScoring" runat="server" Font-Names="Verdana" Font-Size="8pt"
                InteractiveDeviceInfos="(Colección)" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt"
                Height="700px" Width="700px"><LocalReport ReportPath="Page\Scoring\ScoringBoard\Report.rdlc"></LocalReport></rsweb:ReportViewer>
        </asp:View>
    </asp:MultiView>
    <script type="text/javascript" language="javascript">
        function SetFocus() {
            document.getElementById('cphMain_txtCodigoGarantia').focus();
        }
        window.onload = SetFocus;
    </script>
</asp:Content>
