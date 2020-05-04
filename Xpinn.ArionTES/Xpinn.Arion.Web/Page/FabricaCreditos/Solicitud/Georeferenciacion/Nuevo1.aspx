<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo1.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Georeferencia :." %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
                   <tr>
                       <td class="tdI">
                       Codgeoreferencia&nbsp;*&nbsp;<br />
                       <asp:TextBox ID="txtcodgeoreferencia" runat="server" CssClass="textbox" 
                               MaxLength="128" Enabled="False" />
                       </td>
                       <td class="tdD">
                       Cod_persona&nbsp;&nbsp;<asp:CompareValidator ID="cvCOD_PERSONA" runat="server" ControlToValidate="txtCod_persona" ErrorMessage="Solo se admiten n&uacute;meros enteros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                       <asp:TextBox ID="txtCod_persona" runat="server" CssClass="textbox" MaxLength="128" />
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Latitud&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvLATITUD" runat="server" ControlToValidate="txtLatitud" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><br />
                       <asp:TextBox ID="txtLatitud" runat="server" CssClass="textbox" MaxLength="128" />
                       </td>
                       <td class="tdD">
                       Longitud&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvLONGITUD" runat="server" ControlToValidate="txtLongitud" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><br />
                       <asp:TextBox ID="txtLongitud" runat="server" CssClass="textbox" MaxLength="128" />
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Observaciones&nbsp;&nbsp;<br />
                       <asp:TextBox ID="txtObservaciones" runat="server" CssClass="textbox" MaxLength="128" />
                       </td>
                       <td class="tdD">
                       Fechacreacion&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvFECHACREACION" runat="server" ControlToValidate="txtFechacreacion" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><asp:CompareValidator ID="cvFECHACREACION" runat="server" ControlToValidate="txtFechacreacion" ErrorMessage="Formato de Fecha (dd/mm/aaaa)" Operator="DataTypeCheck" SetFocusOnError="True" ToolTip="Formato fecha" Type="Date" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                       <asp:TextBox ID="txtFechacreacion" runat="server" CssClass="textbox" MaxLength="128" />
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Usuariocreacion&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvUSUARIOCREACION" runat="server" ControlToValidate="txtUsuariocreacion" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><br />
                       <asp:TextBox ID="txtUsuariocreacion" runat="server" CssClass="textbox" MaxLength="128" />
                       </td>
                       <td class="tdD">
                       Fecultmod&nbsp;&nbsp;<asp:CompareValidator ID="cvFECULTMOD" runat="server" ControlToValidate="txtFecultmod" ErrorMessage="Formato de Fecha (dd/mm/aaaa)" Operator="DataTypeCheck" SetFocusOnError="True" ToolTip="Formato fecha" Type="Date" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                       <asp:TextBox ID="txtFecultmod" runat="server" CssClass="textbox" MaxLength="128" />
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Usuultmod&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvUSUULTMOD" runat="server" ControlToValidate="txtUsuultmod" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><br />
                       <asp:TextBox ID="txtUsuultmod" runat="server" CssClass="textbox" MaxLength="128" />
                       </td>
                       <td class="tdD">
                       &nbsp;
                       </td>
    </table>
  <%--  <script type="text/javascript" language="javascript">
        function SetFocus()
        {
            document.getElementById('cphMain_txtCODGEOREFENCIA').focus(); 
        }
        window.onload = SetFocus;
    </script>--%>
</asp:Content>