<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Usuario :." %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register assembly="Xpinn.Util" namespace="Xpinn.Util" tagprefix="cc1" %>
<%@ Register src="../../../General/Controles/ctlPlanCuentas.ascx" tagname="ListadoPlanCtas" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <style type="text/css">
        .cssClass1
        {
        background: Red;
        color:White;
        font-weight:bold;
        font-size:x-small;
        }
        .cssClass2
        {
        background: Gray;
        color:White;
        font-weight:bold;
        font-size:x-small;
        }
        .cssClass3
        {
        background: orange;
        color:black;
        font-weight:bold;
        font-size:x-small;
        }
        .cssClass4
        {
        background: blue;
        color:White;
        font-weight:bold;
        font-size:x-small;
        }
        .cssClass5
        {
        background: Green;
        color:White;
        font-weight:bold;
        font-size:x-small;
        }
        .BarBorder
        {
        border-style: solid;
        border-width: 1px;
        width: 180px;
        padding:2px;
        }
    </style>

    <script>
        $(document).keydown(function (event) {
            if (event.keyCode == 123 || event.keyCode == 166) { // Prevent F12
                return false;
            } else if (event.ctrlKey && event.shiftKey && event.keyCode == 73) { // Prevent Ctrl+Shift+I        
                return false;
            }
        });
    </script>

    <script language=JavaScript>  

        function inhabilitar(){ 
   	        alert ("Esta función está inhabilitada.\n\nPerdonen las molestias.") 
   	        return false 
        } 

        document.oncontextmenu=inhabilitar 

    </script>

    <table border="0" cellpadding="5" cellspacing="0" width="70%" >
        <tr>
            <td class="tdI" style="text-align:left">
                Usuario&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtIdentificacion" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><br />
                <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" required="required"
                    MaxLength="128" AutoPostBack="True" />
            </td>
            <td class="tdI" style="text-align:left">
                Identificación&nbsp;&nbsp;<asp:RequiredFieldValidator ID="rfvidentificacion" runat="server" ControlToValidate="txtIdentificacion" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><br />
                <asp:TextBox ID="txtIdentdoc" runat="server" CssClass="textbox" 
                    MaxLength="128" AutoPostBack="True" 
                    ontextchanged="txtIdentificacion_TextChanged" />
            </td>
            <td class="tdD" style="text-align:left">
                Código persona&nbsp;&nbsp;<br />
                <asp:TextBox ID="txtCod_persona" runat="server" CssClass="textbox" ReadOnly="true"
                    MaxLength="128" AutoPostBack="True" />
            </td>
        </tr>
        <tr>
            <td class="tdI" style="text-align:left" colspan="2">
                Nombre&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvnombre" runat="server" ControlToValidate="txtNombre" ErrorMessage="Campo Requerido"
                 SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" style="font-size: x-small"/><br />
                <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" MaxLength="128" 
                    Width="400px" />

            </td>
            <td class="tdD" style="text-align:left">
                Fecha de Creacion&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvfechaCreacion" runat="server" ControlToValidate="txtFechacreacion" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><br />
                <asp:TextBox ID="txtFechacreacion" runat="server" CssClass="textbox" 
                        MaxLength="128" Enabled="False" />
            </td>
        </tr>
        <tr>
            <td class="tdI" style="text-align:left" colspan="2">
                Direccion&nbsp;&nbsp;<br />
                <asp:TextBox ID="txtDireccion" runat="server" CssClass="textbox" 
                    MaxLength="128" Width="400px" />
            </td>
            <td class="tdD" style="text-align:left">
                Telefono&nbsp;&nbsp;<br />
                <asp:TextBox ID="txtTelefono" runat="server" CssClass="textbox" MaxLength="128" />
            </td>
        </tr>
        <tr>
            <td class="tdI" style="text-align:left">
                Clave&nbsp;*&nbsp;
                <asp:RequiredFieldValidator ID="rfvlogin" runat="server" ControlToValidate="txtLogin" ErrorMessage="Campo Requerido" SetFocusOnError="True"
                 ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"  style="font-size: x-small"/><br />
                <asp:TextBox ID="txtLogin" runat="server" CssClass="textbox" MaxLength="25" 
                    TextMode="Password" />       
                <asp:Label ID="lblClave" runat="server" Visible="false" />         
                <asp:PasswordStrength ID="PS" runat="server" TargetControlID="txtLogin" 
                    DisplayPosition="RightSide" PreferredPasswordLength="6" PrefixText="Nivel:"
                    TextCssClass="TextIndicator_TextBox1" MinimumNumericCharacters="2" MinimumSymbolCharacters="1"
                    RequiresUpperAndLowerCaseCharacters="true" TextStrengthDescriptions="Muy pobre; Débil; Medio; Fuerte; Excelente"
                    TextStrengthDescriptionStyles="cssClass1;cssClass2;cssClass3;cssClass4;cssClass5"
                    CalculationWeightings="50;15;15;20" MinimumLowerCaseCharacters="1" 
                    MinimumUpperCaseCharacters="1" Enabled="True">
                </asp:PasswordStrength>
            </td>
            <td class="tdI" style="text-align:left">
                Confirmar Clave&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvloginConf" 
                    runat="server" ControlToValidate="txtLoginConf" ErrorMessage="Campo Requerido" 
                    SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" 
                    Display="Dynamic" style="font-size: x-small"/>
                <asp:CompareValidator ID="CompareValidator1" runat="server" 
                    ControlToCompare="txtLogin" ControlToValidate="txtLoginConf" Display="Dynamic" 
                    ErrorMessage="Las claves no son iguales" ForeColor="Red" SetFocusOnError="True" 
                    ValidationGroup="vgGuardar" style="font-size: x-small"></asp:CompareValidator>
                <br />
                <asp:TextBox ID="txtLoginConf" runat="server" CssClass="textbox" MaxLength="15" 
                    TextMode="Password" />
                <asp:Label ID="lblConfirmaClave" runat="server" Visible="false" />  
                <asp:PasswordStrength ID="PasswordStrength1" runat="server" TargetControlID="txtLoginConf" DisplayPosition="RightSide"
                StrengthIndicatorType="Text" PreferredPasswordLength="6" PrefixText="Nivel:"
                TextCssClass="TextIndicator_TextBox1" MinimumNumericCharacters="2" MinimumSymbolCharacters="1"
                RequiresUpperAndLowerCaseCharacters="true" TextStrengthDescriptions="Muy pobre; Débil; Medio; Fuerte; Excelente"
                TextStrengthDescriptionStyles="cssClass1;cssClass2;cssClass3;cssClass4;cssClass5"
                CalculationWeightings="50;15;15;20" MinimumLowerCaseCharacters="1" 
                        MinimumUpperCaseCharacters="1">
                </asp:PasswordStrength>
            </td>
            <td class="tdD" style="text-align:left">
                Estado&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvestado" runat="server" ControlToValidate="txtEstado" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><br />
                <asp:DropDownList ID="txtEstado" runat="server" CssClass="textbox">
                    <asp:ListItem Text="Inactivo" Value="0"/>
                    <asp:ListItem Text="Activo" Value="1" Selected="True"/>
                    <asp:ListItem Text="Bloqueado" Value="2"/>
                    </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="tdI"style="text-align:left" colspan="2">
                Perfil&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvcodperfil" runat="server" ControlToValidate="txtCodperfil" ErrorMessage="Campo Requerido" SetFocusOnError="True" 
                ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" style="font-size: x-small"/><br />
                <asp:DropDownList ID="txtCodperfil" runat="server" CssClass="textbox" />
            </td>
            <td class="tdD" style="text-align:left">
                Oficina&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvcod_oficina" runat="server" ControlToValidate="txtCod_oficina" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><br />
                <asp:DropDownList ID="txtCod_oficina" runat="server" CssClass="textbox">
                    <asp:ListItem Text="Sin Datos" Value="0"/>
                </asp:DropDownList>
            </td>
        </tr>
    </table>    
    <table border="0" cellpadding="5" cellspacing="0" width="80%">
        <tr>
            <td class="tdI" style="text-align: left">
                <strong>Atribuciones</strong>
            </td>
            <td class="tdI" style="text-align: left">
                &nbsp;
            </td>
            <td class="tdI" style="text-align: left">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="tdI" style="text-align: left">
                <asp:CheckBox ID="ckbTipoAtribucion0" runat="server" Style="font-size: x-small" Text="Genera Reporte de Asesores de la Oficina" />
            </td>
            <td class="tdI" style="text-align: left">
                <asp:CheckBox ID="ckbTipoAtribucion1" runat="server" Style="font-size: x-small" Text="Genera Reporte de Todos los Asesores" />
            </td>
            <td class="tdI" style="text-align: left">
                <asp:CheckBox ID="ckbTipoAtribucion2" runat="server" Style="font-size: x-small" Text="Puede Cambiar Tasas" />
            </td>
        </tr>
        <tr>
            <td class="tdI" style="text-align: left">
                <asp:CheckBox ID="ckbTipoAtribucion3" runat="server" Style="font-size: x-small" Text="Puede Cambiar Nombres/Apellidos del Cliente en Solicitud" />
            </td>
            <td class="tdI" style="text-align: left">
                <asp:CheckBox ID="ckbTipoAtribucion4" runat="server" Style="font-size: x-small" Text="Puede modificar la fecha del comprobante" />
            </td>
            <td class="tdI" style="text-align: left">
                <asp:CheckBox ID="ckbTipoAtribucion5" runat="server" Style="font-size: x-small" Text="Puede modificar el detalle del comprobante" />
            </td>
           
        </tr>
       <tr>
            <td class="tdI" style="text-align: left">
                 <asp:CheckBox ID="ckbTipoAtribucion6" runat="server" Style="font-size: x-small" Text="Puede modificar el salario de los asociados," />
            </td>
            <td class="tdI" style="text-align: left">
                 <asp:CheckBox ID="ckbTipoAtribucion7" runat="server" Style="font-size: x-small" Text="Puede modificar oficinas de los asociados" />
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
        <tr>
            <td class="tdI"style="text-align:left" colspan="4">
                <strong>IP&#39;s Autorizadas</strong>&nbsp;
            </td>
        </tr>
        <tr>
            <td class="tdI"style="text-align:left" colspan="2">
                <asp:GridView ID="gvIPS" runat="server" Width="40%" ShowHeaderWhenEmpty = "True" EmptyDataText = "No se encontraron registros."
                    AutoGenerateColumns="False" PageSize="5" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                    ForeColor="Black" GridLines="Vertical" Height="16px"  ShowFooter="True" style="font-size: x-small"
                    OnRowDataBound="gvIPS_RowDataBound" OnRowCommand="gvIPS_RowCommand" onrowdeleting="gvIPS_RowDeleting" >
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField ShowHeader="False">
                            <FooterTemplate>
                                <asp:ImageButton ID="btnNuevo" runat="server" CausesValidation="False" CommandName="AddNew"
                                ImageUrl="~/Images/gr_nuevo.jpg" ToolTip="Crear Nuevo" Width="16px" />
                            </FooterTemplate>                                            
                            <ItemStyle Width="20px" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Delete"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" 
                                    ImageUrl="~/Images/gr_elim.jpg" ToolTip="Eliminar" Width="16px" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridIco" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Dirección IP">
                            <ItemTemplate>
                                <asp:Label ID="lblDireccionIP" runat="server" Text='<%# Bind("DireccionIP") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtDireccionIP" runat="server" Text='<%# Bind("DireccionIP") %>'></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="txtDireccionIP_FilteredTextBoxExtender" 
                                    runat="server" Enabled="True" FilterType="Custom" TargetControlID="txtDireccionIP" ValidChars="0123456789." />
                                <%--<asp:MaskedEditExtender TargetControlID="txtDireccionIP" Mask="999.999.999.999" runat="server"
                                    MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError"
                                    MaskType="None" InputDirection="LeftToRight" AcceptNegative="None" DisplayMoney="None" ErrorTooltipEnabled="True"/>--%>
                            </FooterTemplate>
                            <ItemStyle Width="50px" />
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle CssClass="gridHeader" />
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
    </table>
         

    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
        <tr>
            <td class="tdI"style="text-align:left" colspan="2">
                <strong>MAC&#39;s Autorizadas</strong>&nbsp;
            </td>
        </tr>
        <tr>
            <td class="tdI"style="text-align:left" colspan="2">
                <asp:GridView ID="gvMac" runat="server" Width="40%" ShowHeaderWhenEmpty = "True" EmptyDataText = "No se encontraron registros."
                    AutoGenerateColumns="False" PageSize="5" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                    ForeColor="Black" GridLines="Vertical" Height="16px"  ShowFooter="True" style="font-size: x-small"
                    OnRowDataBound="gvMac_RowDataBound" OnRowCommand="gvMac_RowCommand" onrowdeleting="gvMac_RowDeleting" >
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField ShowHeader="False">
                            <FooterTemplate>
                                <asp:ImageButton ID="btnNuevo0" runat="server" CausesValidation="False" CommandName="AddNew"
                                ImageUrl="~/Images/gr_nuevo.jpg" ToolTip="Crear Nuevo" Width="16px" />
                            </FooterTemplate>                                            
                            <ItemStyle Width="20px" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEliminar0" runat="server" CommandName="Delete"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" 
                                    ImageUrl="~/Images/gr_elim.jpg" ToolTip="Eliminar" Width="16px" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridIco" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Dirección Mac">
                            <ItemTemplate>
                                <asp:Label ID="lblDireccionMac" runat="server" Text='<%# Bind("DireccionMac") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtDireccionMac" runat="server" Text='<%# Bind("DireccionMac") %>'></asp:TextBox>                             
                                <%--<asp:MaskedEditExtender TargetControlID="txtDireccionIP" Mask="999.999.999.999" runat="server"
                                    MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError"
                                    MaskType="None" InputDirection="LeftToRight" AcceptNegative="None" DisplayMoney="None" ErrorTooltipEnabled="True"/>--%>
                            </FooterTemplate>
                            <ItemStyle Width="50px" />
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle CssClass="gridHeader" />
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
            <td style="text-align: left">
                Cod Cuenta Giros<br />
                <cc1:TextBoxGrid ID="txtCodCuenta" runat="server" AutoPostBack="True" Style="text-align: left"
                    CssClass="textbox" Width="100px" OnTextChanged="txtCodCuenta_TextChanged">    
                </cc1:TextBoxGrid>
                <cc1:ButtonGrid ID="btnListadoPlan" CssClass="btnListado" runat="server" Text="..."
                    Width="22px" OnClick="btnListadoPlan_Click" />
                <uc1:ListadoPlanCtas ID="ctlListadoPlan" runat="server" />
            </td>
            <td class="tdI"style="text-align:left">
                Nombre de la Cuenta<br />
                <cc1:TextBoxGrid ID="txtNomCuenta" runat="server" Style="text-align: left" Width="280px"
                    CssClass="textbox" Enabled="False">
                </cc1:TextBoxGrid>
            </td>
          
        </tr>
    </table>
               
</asp:Content>