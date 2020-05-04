<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Detalle.aspx.cs" Inherits="Detalle" Title=".: Xpinn - Consecutivos por Oficinas :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <table border="0" cellpadding="5" cellspacing="0" width="60%" >
        <tr>
            <td><br /><br /></td>
        </tr>
        <tr>
            <td colspan="2" width="100%" style="text-align: left">
                Tabla<asp:RequiredFieldValidator ID="rfvtabla" runat="server"  style="font-size:x-small"
                    ControlToValidate="ddltabla" ErrorMessage="Campo Requerido" 
                    SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" 
                    Display="Dynamic"/>
                <br />
                <asp:DropDownList ID="ddltabla" runat="server" CssClass="textbox" Width="60%" 
                    Enabled="false" >
                    <asp:ListItem Text="CREDITO" Value="CREDITO" Selected="True"/>
                    <asp:ListItem Text="COMPROBANTES" Value="COMPROBANTES"/>
                    <asp:ListItem Text="PERSONA" Value="PERSONA"/>
                    <asp:ListItem Text="APORTE" Value="APORTE"/>
                </asp:DropDownList>
                &nbsp;
                <br />
            </td>
        </tr>
        <tr>
            <td colspan="2" width="100%" style="text-align: left">
                Oficina&nbsp;<asp:RequiredFieldValidator ID="rfvoficina" runat="server" 
                    ControlToValidate="ddlOficina" ErrorMessage="Campo Requerido"  style="font-size:x-small"
                    SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" 
                    Display="Dynamic"/>
                <br />
                <asp:DropDownList ID="ddlOficina" runat="server" CssClass="textbox" Width="60%" Enabled="false">
                    <asp:ListItem Text="Sin Datos" Value="0" />
                </asp:DropDownList>
                <br />
            </td>
        </tr>
        <tr>
            <td width="50%" style="text-align: left">
                Rango Inicial&nbsp;<asp:RequiredFieldValidator ID="rfvrangoi" runat="server" 
                    ControlToValidate="txtRangoInicial" ErrorMessage="Campo Requerido" 
                    SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" 
                    Display="Dynamic"  style="font-size:x-small"/>
                <br />
                <asp:TextBox ID="txtRangoInicial" runat="server" CssClass="textbox"  Enabled="false"
                        MaxLength="128" Width="60%" />
                <br />
            </td>
            <td width="50%" style="text-align: left">
                Rango Final&nbsp;&nbsp;<asp:RequiredFieldValidator ID="rfvrangof"  style="font-size:x-small"
                    runat="server" ControlToValidate="txtRangoFinal" ErrorMessage="Campo Requerido" 
                    SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" 
                    Display="Dynamic"/><br />
                    <asp:TextBox ID="txtRangoFinal" runat="server" CssClass="textbox" MaxLength="128"  Enabled="false"
                            ontextchanged="txtRangoFinal_TextChanged" Width="60%" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td width="50%" style="text-align: left">
                &nbsp;Fecha de Creacion&nbsp;*&nbsp;
                <asp:RequiredFieldValidator 
                    ID="rfvfecchacreacion" runat="server" ControlToValidate="txtFechacreacion" 
                    ErrorMessage="Campo Requerido" SetFocusOnError="True"  style="font-size:x-small"
                    ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><br />
                <asp:TextBox ID="txtFechacreacion" runat="server" CssClass="textbox" 
                        MaxLength="128"  Width="60%"/>
                <br />
            </td>
            <td class="tdI">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="tdD" colspan="2">
                <strong>
                <asp:Label ID="Lblerror" runat="server"
                    ForeColor="Red" CssClass="align-rt"></asp:Label>
                </strong>
            </td>
        </tr>
    </table>
    <script type="text/javascript" language="javascript">
        function SetFocus() {
            document.getElementById('cphMain_txtCod_opcion').focus();
        }
        window.onload = SetFocus;
    </script>
</asp:Content>