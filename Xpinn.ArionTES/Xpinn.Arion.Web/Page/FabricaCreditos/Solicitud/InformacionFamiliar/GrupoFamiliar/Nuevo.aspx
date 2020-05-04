<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/solicitudSub.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table cellpadding="5" cellspacing="0" style="width: 100%">
        <tr>
            <td class="tdI">
                C&oacute;digo Persona*
                &nbsp;<br />
                <asp:TextBox ID="txtCodigoPersona" runat="server" CssClass="textbox" 
                    MaxLength="15" Enabled="False" />
                <asp:FilteredTextBoxExtender ID="txtCodigoPersona_FilteredTextBoxExtender" runat="server"
                    Enabled="True" FilterType="Numbers" TargetControlID="txtCodigoPersona">
                </asp:FilteredTextBoxExtender>
            </td>
            <td class="tdD">
                Nombres *
                <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ErrorMessage="Campo Requerido"
                    ControlToValidate="txtNombres" Display="Dynamic" ForeColor="Red" ValidationGroup="vgGuardar" /><br />
                <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" 
                    MaxLength="200" />
            </td>
            <td class="tdD">
                Parentesco *<br />
                <asp:DropDownList ID="ddlParentesco" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                Sexo *<asp:RadioButtonList ID="rblSexo" runat="server" RepeatDirection="Horizontal" CssClass="check">
                    <asp:ListItem Selected="True">M</asp:ListItem>
                    <asp:ListItem>F</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td>
                Fecha Nacimiento
                <asp:RequiredFieldValidator ID="rfvFechaNacimiento" runat="server" ControlToValidate="txtFechaNacimiento"
                    Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                    ValidationGroup="vgGuardar" />
                <br />
                <asp:TextBox ID="txtFechaNacimiento" runat="server" CssClass="textbox"></asp:TextBox>
                <asp:CalendarExtender ID="ceFechaNacimiento" runat="server" DaysModeTitleFormat="dd/MM/yyyy"
                    Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFechaNacimiento" TodaysDateFormat="dd/MM/yyyy">
                </asp:CalendarExtender>
            </td>
            <td>
                Estudia<asp:RadioButtonList ID="rblEstudia" runat="server" 
                    RepeatDirection="Horizontal" CssClass="check">
                    <asp:ListItem Selected="True" Value="1">Si</asp:ListItem>
                    <asp:ListItem Value="0">No</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td>
                A Cargo<asp:RadioButtonList ID="rblAcargo" runat="server" RepeatDirection="Horizontal"
                    CssClass="check">
                    <asp:ListItem Value="1" Selected="True">Si</asp:ListItem>
                    <asp:ListItem Value="0">No</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td>
                Observaciones<br />
                <asp:TextBox ID="txtObservaciones" runat="server" CssClass="textbox" 
                    MaxLength="200" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <%-- <script type="text/javascript" language="javascript">
        function SetFocus() {
            document.getElementById('cphMain_txtCodigo').focus();
        }
        window.onload = SetFocus;
    </script>--%>
</asp:Content>
