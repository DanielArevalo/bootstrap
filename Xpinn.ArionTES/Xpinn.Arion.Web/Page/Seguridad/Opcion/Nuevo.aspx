<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Opcion :." %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <table border="0" cellpadding="5" cellspacing="0" width="100%">
        <tr>
            <td class="tdI">C&oacute;digo&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvcod_opcion" runat="server" ControlToValidate="txtCod_opcion" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><asp:CompareValidator ID="cvcod_opcion" runat="server" ControlToValidate="txtCod_opcion" ErrorMessage="Solo se admiten n&uacute;meros enteros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                <asp:TextBox ID="txtCod_opcion" runat="server" CssClass="textbox" MaxLength="128" />
            </td>
            <td class="tdD">Nombre&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvnombre" runat="server" ControlToValidate="txtNombre" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" MaxLength="128" />
            </td>
        </tr>
        <tr>
            <td class="tdI">Proceso&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvcod_proceso" runat="server" ControlToValidate="txtCod_proceso" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                <asp:DropDownList ID="txtCod_proceso" runat="server" CssClass="textbox" />
            </td>
            <td class="tdD">Ruta&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvruta" runat="server" ControlToValidate="txtRuta" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                <asp:TextBox ID="txtRuta" runat="server" CssClass="textbox" MaxLength="128" />
            </td>
        </tr>
        <tr>
            <td class="tdD">Genera Log&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvgeneraLog" runat="server" ControlToValidate="txtGeneralog" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                <asp:DropDownList ID="txtGeneralog" runat="server" CssClass="textbox">
                    <asp:ListItem Text="Si" Value="1" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="tdI">Ayuda&nbsp;&nbsp;<asp:CompareValidator ID="cvrefayuda" runat="server" ControlToValidate="txtRefayuda" ErrorMessage="Solo se admiten n&uacute;meros enteros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                <asp:TextBox ID="txtRefayuda" runat="server" CssClass="textbox" MaxLength="128" />
            </td>
        </tr>
        <tr>
            <td class="tdD">Visualizar desde Menu *
                <asp:RequiredFieldValidator ID="Accion" runat="server"
                    ControlToValidate="txtAccion" ErrorMessage="Campo Requerido"
                    SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red"
                    Display="Dynamic" />
                <br />
                <asp:DropDownList ID="txtAccion" runat="server" CssClass="textbox">
                    <asp:ListItem Text="Si" Value="1" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList>

            </td>
            <td class="tdD">editar Visualizacion *
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                    ControlToValidate="txtAccion" ErrorMessage="Campo Requerido"
                    SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red"
                    Display="Dynamic" />
                <br />
                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox">
                    <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0" Selected="True"></asp:ListItem>
                </asp:DropDownList>

            </td>
            <td class="tdI">&nbsp;</td>
        </tr>
    </table>
    

    <script type="text/javascript" language="javascript">
        function SetFocus() {
            document.getElementById('cphMain_txtCod_opcion').focus();
        }
        window.onload = SetFocus;
    </script>
</asp:Content>
