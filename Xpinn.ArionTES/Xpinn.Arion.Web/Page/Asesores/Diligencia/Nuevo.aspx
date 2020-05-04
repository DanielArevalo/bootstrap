<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Diligencia :." %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
        <tr>
            <td class="tdI">
                Numero radicación&nbsp;&nbsp;<br />
                <asp:TextBox ID="txtNumero_radicacion" runat="server" CssClass="textbox" MaxLength="128" />
                <br />
                <asp:RequiredFieldValidator ID="rfvNumero_radicacion" runat="server" 
                    ControlToValidate="txtNumero_radicacion" ErrorMessage="Campo Requerido" 
                    SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" 
                    Display="Dynamic"/>
                <br />
                <asp:CompareValidator ID="cvNUMERO_RADICACION" runat="server" ControlToValidate="txtNumero_radicacion" ErrorMessage="Solo se admiten n&uacute;meros enteros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" />
            </td>
            <td class="tdD">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="tdI">
            Fecha diligencia&nbsp;&nbsp;<br />
            <asp:TextBox ID="txtFecha_diligencia" runat="server" CssClass="textbox" 
                    MaxLength="128" Enabled="False" />
                <br />
            </td>
            <td class="tdD">
            Tipo diligencia&nbsp;&nbsp;<br />
                <asp:DropDownList ID="ddlTipoDiligencia" runat="server" CssClass="dropdown">
                </asp:DropDownList>
                <br />
                <asp:CompareValidator ID="cvTipoDiligencia" runat="server" 
                    ControlToValidate="ddlTipoDiligencia" Display="Dynamic" 
                    ErrorMessage="Seleccione un tipo de diligencia" ForeColor="Red" 
                    Operator="GreaterThan" SetFocusOnError="true" Type="Integer" 
                    ValidationGroup="vgGuardar" ValueToCompare="0">
                </asp:CompareValidator>
                <br />
            </td>
        </tr>
        <tr>
            <td class="tdI">
            Atendió&nbsp;&nbsp;<br />
            <asp:TextBox ID="txtAtendio" runat="server" CssClass="textbox" MaxLength="128" />
            </td>
            <td class="tdD">
            Respuesta&nbsp;&nbsp;<br />
            <asp:TextBox ID="txtRespuesta" runat="server" CssClass="textbox" MaxLength="128" />
            </td>
        </tr>
        <tr>
            <td class="tdI">
            Acuerdo&nbsp;&nbsp;<asp:CheckBox ID="chkAprueba" runat="server" />
                <br />
            </td>
            <td class="tdD">
            Fecha acuerdo&nbsp;&nbsp;<br />
            <asp:TextBox ID="txtFecha_acuerdo" runat="server" CssClass="textbox" MaxLength="128" />                       
                <br />
                <asp:CompareValidator ID="cvFecha_acuerdo" runat="server" 
                    ControlToValidate="txtFecha_acuerdo" 
                    ErrorMessage="Formato de Fecha (dd/mm/aaaa)" Operator="DataTypeCheck" 
                    SetFocusOnError="True" ToolTip="Formato fecha" Type="Date" 
                    ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" />
            <asp:CalendarExtender ID="txtFecha_CalendarExtender" runat="server" 
                    Enabled="True" TargetControlID="txtFecha_acuerdo" Format="dd/MM/yyyy">
                </asp:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td class="tdI">
            Valor acuerdo&nbsp;&nbsp;<br />
            <asp:TextBox ID="txtValor_acuerdo" runat="server" CssClass="textbox" MaxLength="128" />
                <br />
                <asp:CompareValidator ID="cvVALOR_ACUERDO" runat="server" ControlToValidate="txtValor_acuerdo" ErrorMessage="Solo se admiten n&uacute;meros enteros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" />
            </td>
            <td class="tdD">
            Anexo&nbsp;&nbsp;<br />
                <asp:TextBox ID="txtAnexo" runat="server" CssClass="textbox" Enabled="False" 
                    Visible="False"></asp:TextBox>
                <br />
                <asp:Label ID="lblCambiarArchivo" runat="server" Text="Cambiar archivo:" 
                    Visible="False"></asp:Label>
                <asp:CheckBox ID="chkCambiarArchivo" runat="server" Visible="False" />
                <br />
                <asp:FileUpload ID="FileUpload1" runat="server" CssClass="textbox" />
                <br />
            </td>
        </tr>
        <tr>
            <td class="tdI">
            Observación&nbsp;&nbsp;<br />
            <asp:TextBox ID="txtObservacion" runat="server" CssClass="textbox" MaxLength="128" />
            </td>
            <td class="tdD">
                &nbsp;</td>
        </tr>
    </table>
    <%--<script type="text/javascript" language="javascript">
        function SetFocus()
        {
            document.getElementById('cphMain_txtCODIGO_DILIGENCIA').focus(); 
        }
        window.onload = SetFocus;
    </script>--%>
</asp:Content>