<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/solicitud.master"
    AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
                <strong>
                <asp:Label ID="Lblerror" runat="server"
                    ForeColor="Red" CssClass="align-rt"></asp:Label>
                </strong>
    <table cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td>
                <asp:Panel ID="pConsulta1" runat="server">
                    <table cellpadding="5" cellspacing="0" style="width: 100%">
                       
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <table style="width: 100%">
        <tr>
            <td style="text-align: center">
                <asp:GridView ID="gvMiembroFamiliar1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    GridLines="Horizontal" OnPageIndexChanging="gvMiembroFamiliar_PageIndexChanging"
                    OnRowDataBound="gvMiembroFamiliar_RowDataBound" OnRowDeleting="gvMiembroFamiliar_RowDeleting"
                    OnRowEditing="gvMiembroFamiliar_RowEditing" OnSelectedIndexChanged="gvMiembroFamiliar_SelectedIndexChanged"
                    PageSize="20" ShowHeaderWhenEmpty="True" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="CODFAMILIAR" HeaderText="CODFAMILIAR" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo" />
                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                                    ToolTip="Detalle" Width="16px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnBorrar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg"
                                    ToolTip="Borrar" Width="16px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="COD_PERSONA" HeaderText="COD_PERSONA" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo"/>
                        <asp:BoundField DataField="NOMBRES" HeaderText="Nombre Familiar" />
                        <asp:BoundField DataField="IDENTIFICACION" HeaderText="Identificacion" />
                        <asp:BoundField DataField="DESCRIPCION" HeaderText="Parentesco" />
                        <asp:BoundField DataField="SEXO" HeaderText="Sexo" />
                        <asp:BoundField DataField="FECHANACIMIENTO" HeaderText="Edad" />
                        <asp:BoundField DataField="ACARGO" HeaderText="A Cargo" />
                        <asp:BoundField DataField="ESTUDIA" HeaderText="Estudia" />
                        <asp:BoundField DataField="CODPARENTESCO" HeaderText="CODPARENTESCO" Visible="false" />
                        <asp:BoundField DataField="OBSERVACIONES" HeaderText="Observaciones" />                                
                    </Columns>
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                    <RowStyle CssClass="gridItem" />
                </asp:GridView>                    
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado."
                    Visible="False" />
            </td>
        </tr>
    </table>
    <table cellpadding="5" cellspacing="0" style="width: 100%">
        <tr>
            <td class="tdI">
                &nbsp;Nombres *
                <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ErrorMessage="Campo Requerido"
                    ControlToValidate="txtNombres" Display="Dynamic" ForeColor="Red" ValidationGroup="vgGuardar" /><br />
                <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" MaxLength="200" 
                    Width="291px" />
                <br />
            </td>
            <td class="tdD">
                Identificación *
                <br />
                <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox"  Width="291px"
                    MaxLength="20" />
            </td>
            <td class="tdD">
                Parentesco *<br />
                <asp:DropDownList ID="ddlParentesco" runat="server" Width="291px" 
                    AppendDataBoundItems="True">
                    <asp:ListItem Value="Seleccione un Item"></asp:ListItem>
                </asp:DropDownList><br />
                <asp:RequiredFieldValidator ID="rfvParentesco" runat="server" 
                    ControlToValidate="ddlParentesco" Display="Dynamic" 
                    ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True" 
                    ValidationGroup="vgGuardar" style="font-size: xx-small" 
                    InitialValue="Seleccione un Item" />
            </td>
        </tr>
        <tr>
          <td class="tdI">
                Sexo *
                <asp:RequiredFieldValidator ID="rfvSexo" runat="server" 
                    ControlToValidate="rblSexo" Display="Dynamic" 
                    ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True" 
                    ValidationGroup="vgGuardar" style="font-size: xx-small"  /><br />
                <asp:RadioButtonList ID="rblSexo" runat="server" RepeatDirection="Horizontal" Width="291px" CssClass="check">
                    <asp:ListItem>M</asp:ListItem>
                    <asp:ListItem>F</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td>
                Fecha Nacimiento
                <asp:RequiredFieldValidator ID="rfvFechaNacimiento" runat="server" ControlToValidate="txtFechaNacimiento"
                    Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                    ValidationGroup="vgGuardar" />
                <asp:MaskedEditValidator ID="mevFechaNacimiento" runat="server" ControlExtender="mskFechaNacimiento"
                    ControlToValidate="txtFechaNacimiento" EmptyValueMessage="Fecha Requerida" InvalidValueMessage="Fecha No Valida"
                    Display="Dynamic" TooltipMessage="Seleccione una Fecha" EmptyValueBlurredText="Fecha No Valida"
                    InvalidValueBlurredMessage="Fecha No Valida" ValidationGroup="vgGuardar" ForeColor="Red" />
                <br />
                <asp:TextBox ID="txtFechaNacimiento" runat="server" CssClass="textbox"  Width="291px"></asp:TextBox>
                <asp:MaskedEditExtender ID="mskFechaNacimiento" runat="server" TargetControlID="txtFechaNacimiento"
                    Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                    OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"
                    ErrorTooltipEnabled="True" />
                <asp:CalendarExtender ID="ceFechaNacimiento" runat="server" DaysModeTitleFormat="dd/MM/yyyy"
                    Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFechaNacimiento" TodaysDateFormat="dd/MM/yyyy">
                </asp:CalendarExtender>
            </td>
            <td>
                Estudia
                <asp:RequiredFieldValidator ID="rfvEstudia" runat="server" 
                    ControlToValidate="rblEstudia" Display="Dynamic" 
                    ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True" 
                    ValidationGroup="vgGuardar" style="font-size: xx-small"  /><br />
                <asp:RadioButtonList ID="rblEstudia" runat="server" RepeatDirection="Horizontal" Width="291px"
                    CssClass="check">
                    <asp:ListItem Value="1">Si</asp:ListItem>
                    <asp:ListItem Value="0">No</asp:ListItem>
                </asp:RadioButtonList>                
            </td>
        </tr>
        <tr>
           <td class="tdI">
                A Cargo
                <asp:RequiredFieldValidator ID="rfvACargo" runat="server" 
                    ControlToValidate="rblAcargo" Display="Dynamic" 
                    ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True" 
                    ValidationGroup="vgGuardar" style="font-size: xx-small"  /><br />
                <asp:RadioButtonList ID="rblAcargo" runat="server" RepeatDirection="Horizontal" Width="291px"
                    CssClass="check">
                    <asp:ListItem Value="1">Si</asp:ListItem>
                    <asp:ListItem Value="0">No</asp:ListItem>
                </asp:RadioButtonList>              
            </td>
            <td>
                Observaciones<br />
                <asp:TextBox ID="txtObservaciones" runat="server" CssClass="textbox" MaxLength="200"  Width="291px" />
            </td>
            <td>
                &nbsp;
                <asp:TextBox ID="txtCodigoPersona" runat="server" CssClass="textbox" MaxLength="15" Width="291px"
                    Enabled="False" Visible="False" />
                <asp:FilteredTextBoxExtender ID="txtCodigoPersona_FilteredTextBoxExtender" runat="server"
                    Enabled="True" FilterType="Numbers" TargetControlID="txtCodigoPersona">
                </asp:FilteredTextBoxExtender>
            </td>
        </tr>
    </table>
</asp:Content>
