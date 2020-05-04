<%@ Page Language="C#" MasterPageFile="~/General/Master/solicitud.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - InformacionNegocio :." %>

<%@ Register Src="../../../../General/Controles/direccion.ascx" TagName="direccion" TagPrefix="uc1" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table border="0" cellpadding="5" cellspacing="0" width="100%">
        <tr>
            <td class="tdI" style="height: 32px; width: 160px;">
                <asp:TextBox ID="txtCod_negocio" runat="server" CssClass="textbox" MaxLength="128"
                    Enabled="False" Visible="False" />
            </td>
            <td class="tdI" style="height: 32px; width: 1501px;">
                <asp:TextBox ID="txtCod_persona" runat="server" CssClass="textbox" MaxLength="128"
                    Enabled="False" Visible="False" />
            </td>
            <td class="tdI" style="height: 32px; width: 758px;">
                &nbsp;</td>
            <td class="tdD" style="height: 32px; " colspan="3">
                &nbsp;</td>
            <td class="tdD" style="height: 32px">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="tdI" style="height: 32px" colspan="7">
                <asp:Panel ID="Panel2" runat="server">
                    <table style="width:100%;">
                        <tr>
                   
                            <td style="text-align:left">
                                Nombre negocio*<br />
                                <asp:TextBox ID="txtNombrenegocio" runat="server" CssClass="textbox" 
                                    MaxLength="128" Width="418px" />
                                <asp:RequiredFieldValidator ID="rfvCOD_OFICINA3" runat="server" 
                                    ControlToValidate="txtNombrenegocio" Display="Dynamic" 
                                    ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True" 
                                    ValidationGroup="vgGuardar" />
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            
                            <td style="text-align:left">
                                Descripción<br />
                                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" 
                                    MaxLength="128" Width="419px" />
                                <asp:RequiredFieldValidator ID="rfvtxtDescripcion" runat="server" 
                                    ControlToValidate="txtDescripcion" Display="Dynamic" 
                                    ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True" 
                                    ValidationGroup="vgGuardar" />
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>                   
                    </table>
                </asp:Panel>
            </td>
        </tr>          
        <tr>
            <td class="tdI" colspan="7">
                <asp:Panel ID="Panel1" runat="server">
                    <table style="width:100%;">
                        <tr>
                            <td style="text-align:left" colspan="4">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:left">
                                Localidad<br />
                                <asp:DropDownList ID="ddlLocalidad" runat="server" AutoPostBack="True" 
                                    CssClass="textbox" OnSelectedIndexChanged="ddlLocalidad_SelectedIndexChanged" Width="251px" AppendDataBoundItems="True">
                                    <asp:ListItem Value="Selecccione un Item"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="text-align:left">
                                Barrio<br />
                                <asp:DropDownList ID="ddlBarrio" runat="server" CssClass="textbox" 
                                    Width="251px">
                                </asp:DropDownList>
                            </td>
                            <td style="text-align:left">
                                Tipo local
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:RadioButtonList ID="rblTipoLocal" runat="server" AutoPostBack="True"  
                                            OnSelectedIndexChanged="rblTipoLocal_SelectedIndexChanged" 
                                            RepeatDirection="Horizontal" Width="268px">
                                            <asp:ListItem Selected="True" Value="1">Propio</asp:ListItem>
                                            <asp:ListItem Value="0">Arrendado</asp:ListItem>
                                            <asp:ListItem Value="2">Familiar</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td style="text-align:left">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="text-align:left">
                                Telefono Arrendador
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtTelefonoarrendador" runat="server" CssClass="textbox"  Width="251px"
                                            MaxLength="50" />
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="rblTipoLocal" 
                                            EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                                <td style="text-align:left">
                                    Valor de Arriendo<asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtvalorarriendo" runat="server" CssClass="textbox"  Width="251px"
                                            MaxLength="50" />
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="rblTipoLocal" 
                                            EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                            <td style="text-align:left">
                                Arrendador<asp:UpdatePanel ID="UpdatePanel5" runat="server" 
                                    ChildrenAsTriggers="False" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtArrendador" runat="server" CssClass="textbox" 
                                            MaxLength="128" Width="251px" />
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="rblTipoLocal" 
                                            EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                            <td style="text-align:left">
                                &nbsp;</td>
                            <td style="text-align:left">
                                &nbsp;</td>
                        </tr>
                    </table>
                </asp:Panel>
                
                
            </td>
        </tr>
           <tr>
            <td class="tdI" colspan="7" style="text-align:left">
                <hr style="font-size: xx-small" />
            </td>
        </tr>
          <tr>
            <td class="tdI" colspan="7" style="text-align:left">
                Dirección:&nbsp;&nbsp;<uc1:direccion ID="direccion" runat="server" 
                    Requerido="True" />
            </td>
        </tr>
        <tr>
            <td class="tdD" style="width: 165px; text-align:left">
                Teléfono*
                <asp:RequiredFieldValidator 
                    ID="RequiredFieldValidator1" runat="server"
                    ControlToValidate="txtNombrenegocio" Display="Dynamic" ErrorMessage="Campo Requerido"
                    ForeColor="Red" SetFocusOnError="True" ValidationGroup="vgGuardar" 
                    style="font-size: xx-small" />
                <br />
                <asp:TextBox ID="txtTelefono" runat="server" CssClass="textbox" Width="93%"
                    MaxLength="100" />
            </td>
            <td class="tdD" style="text-align:left" colspan="2">
                Actividad
                <asp:RequiredFieldValidator ID="rfvActividad" runat="server" 
                    ControlToValidate="ddlActividad" Display="Dynamic" 
                    ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True" 
                    ValidationGroup="vgGuardar" style="font-size: xx-small" 
                    InitialValue="Seleccione un Item" />
                <br />
                <asp:DropDownList ID="ddlActividad" runat="server" CssClass="textbox" 
                    Width="480px" AppendDataBoundItems="True">
                    <asp:ListItem Value="Seleccione un Item"></asp:ListItem>
                </asp:DropDownList>            
            </td>
            <td class="tdD" style="text-align:left" colspan="3">
                Sector:
                <br />
                <asp:RadioButtonList 
                    ID="rblActividad" runat="server" RepeatDirection="Horizontal" Width="286px">
                    <asp:ListItem Value="0">Comercial</asp:ListItem>
                    <asp:ListItem Value="1">Producción</asp:ListItem>
                    <asp:ListItem Value="2">Servicios</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="tdD" style="text-align:left">                
            </td>
        </tr>
        <tr>
            <td class="tdD" style="text-align:left">
                Experiencia (Años)
                <br />
                <asp:TextBox ID="txtExperiencia" runat="server" CssClass="textbox"  Width="150px"
                    MaxLength="5" />
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                    ControlToValidate="txtExperiencia" Display="Dynamic" ErrorMessage="Campo Requerido"
                    ForeColor="Red" SetFocusOnError="True" ValidationGroup="vgGuardar" 
                    style="font-size: xx-small" />
                <br />
                <br />
            </td>
            <td style="text-align:left; width: 1501px;">
                Antiguedad Lugar De Ubicación Actual
                <br />
                <asp:TextBox ID="txtAntiguedad" runat="server" CssClass="textbox"  Width="70%"
                    MaxLength="4" />&nbsp;(Meses) 
                <br />
                <asp:FilteredTextBoxExtender ID="txtAntiguedad_FilteredTextBoxExtender" 
                    runat="server" Enabled="True" FilterType="Numbers" 
                    TargetControlID="txtAntiguedad">
                </asp:FilteredTextBoxExtender>
                <asp:RequiredFieldValidator ID="rfvtxtAntiguedad" runat="server"
                    ControlToValidate="txtAntiguedad" Display="Dynamic" ErrorMessage="Campo Requerido"
                    ForeColor="Red" SetFocusOnError="True" ValidationGroup="vgGuardar" 
                    style="font-size: xx-small" />
                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtAntiguedad"
                    ErrorMessage="Cliente no cumple con la antiguedad" ForeColor="Red" 
                    MaximumValue="9999" MinimumValue="12"
                    ValidationGroup="vgGuardar" Type="Integer" style="font-size: xx-small"></asp:RangeValidator>
            </td>
            <td class="tdD" style="text-align:left; width: 758px;">
                Empleados Permanentes*
                <br />
                <asp:TextBox ID="txtEmplperm" runat="server" CssClass="textbox" MaxLength="5"  
                    Width="90%" ontextchanged="txtEmplperm_TextChanged"/>
                <br />
                <asp:CompareValidator ID="cvEMPLPERM" runat="server" ControlToValidate="txtEmplperm"
                    ErrorMessage="Solo se admiten n&uacute;meros enteros" Operator="DataTypeCheck"
                    SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" ForeColor="Red"
                    Display="Dynamic" style="font-size: xx-small" />
                <asp:RequiredFieldValidator ID="rfvEmplPerm" runat="server"
                    ControlToValidate="txtEmplperm" Display="Dynamic" ErrorMessage="Campo Requerido"
                    ForeColor="Red" SetFocusOnError="True" ValidationGroup="vgGuardar" 
                    style="font-size: xx-small" />
                <br />
            </td>
            <td class="tdD" style="text-align:left; width: 214px;">
                Empleados Temporales*
                <br />
                <asp:TextBox ID="txtEmpltem" runat="server" CssClass="textbox" MaxLength="5" 
                    Width="162px"/>
                <br />
                <asp:RequiredFieldValidator ID="rfvEmplTem" runat="server"
                    ControlToValidate="txtEmpltem" Display="Dynamic" ErrorMessage="Campo Requerido"
                    ForeColor="Red" SetFocusOnError="True" ValidationGroup="vgGuardar" 
                    style="font-size: xx-small" />
                <asp:CompareValidator ID="cvEMPLTEM" runat="server" ControlToValidate="txtEmpltem"
                    ErrorMessage="Solo se admiten n&uacute;meros enteros" Operator="DataTypeCheck"
                    SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" ForeColor="Red"
                    Display="Dynamic" style="font-size: xx-small" />
                <br />
            </td>
            <td class="tdD" style="text-align:left;">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
            <td class="tdD" style="text-align:left;">
             </td>                 
            <td class="tdD" style="text-align:left;">
                &nbsp;</td>
        </tr>
        </table>
</asp:Content>
