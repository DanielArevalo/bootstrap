<%@ Page Language="C#" MasterPageFile="~/General/Master/solicitud.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Referncias :." %>

<%@ Register src="../../../../../General/Controles/direccion.ascx" tagname="direccion" tagprefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>   
    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
                   <tr>
                       <td class="tdI">
                       <asp:TextBox ID="txtCodreferencia" runat="server" CssClass="textbox" 
                               MaxLength="128" Enabled="False" Visible="False" />
                       </td>
                       <td class="tdD">
                       <asp:TextBox ID="txtCod_persona" runat="server" CssClass="textbox" MaxLength="128" 
                               Visible="False" />
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                           Tipo Referencia&nbsp;*<asp:RadioButtonList 
                               ID="rblTipoReferencia" runat="server" 
                               RepeatDirection="Horizontal">
                               <asp:ListItem Value="1" Selected="True">Familiar</asp:ListItem>
                               <asp:ListItem Value="2">Personal</asp:ListItem>
                               <asp:ListItem Value="3">Comercial</asp:ListItem>
                           </asp:RadioButtonList>
                       </td>
                       <td class="tdD">
                           Nombres&nbsp;*<asp:RequiredFieldValidator ID="rfvNOMBRES" runat="server" ControlToValidate="txtNombres" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/>
                       <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" MaxLength="128" />
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI" colspan="2">
                       Dirección Domicilio<uc1:direccion ID="direccion" runat="server" />
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                           Parentesco<br />
                           <asp:DropDownList ID="ddlParentesco" runat="server">
                           </asp:DropDownList>
                       </td>
                       <td class="tdD">
                       Celular&nbsp;&nbsp;<br />
                       <asp:TextBox ID="txtCelular" runat="server" CssClass="textbox" MaxLength="128" />
                           <br />
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Telefono&nbsp;Domicilio&nbsp;<br />
                       <asp:TextBox ID="txtTelefono" runat="server" CssClass="textbox" MaxLength="128" />
                       </td>
                       <td class="tdD">
                           Telefono Oficina<br />
                       <asp:TextBox ID="txtTeloficina" runat="server" CssClass="textbox" MaxLength="128" />
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                           <asp:CompareValidator ID="cvNUMERO_RADICACION" runat="server" ControlToValidate="txtNumero_radicacion" ErrorMessage="Solo se admiten n&uacute;meros enteros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                       <asp:TextBox ID="txtNumero_radicacion" runat="server" CssClass="textbox" 
                               MaxLength="128" Visible="False" />
                       <asp:TextBox ID="txtObservaciones" runat="server" CssClass="textbox" 
                               MaxLength="128" Visible="False" />
                       <asp:TextBox ID="txtCalificacion" runat="server" CssClass="textbox" MaxLength="128" 
                               Visible="False" />
                       </td>
                       <td class="tdD">
                           <asp:RequiredFieldValidator ID="rfvESTADO" runat="server" ControlToValidate="txtEstado" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/>&nbsp;
                       <asp:CompareValidator ID="cvESTADO" runat="server" ControlToValidate="txtEstado" ErrorMessage="Solo se admiten n&uacute;meros enteros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" />
                       <asp:TextBox ID="txtEstado" runat="server" CssClass="textbox" MaxLength="128" 
                               Visible="False" >1</asp:TextBox>
                           <br />
                           <asp:CompareValidator ID="cvCODUSUVERIFICA" runat="server" ControlToValidate="txtCodusuverifica" ErrorMessage="Solo se admiten n&uacute;meros enteros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" />
                       <asp:TextBox ID="txtCodusuverifica" runat="server" CssClass="textbox" 
                               MaxLength="128" Visible="False" >1</asp:TextBox>
                           <br />
                           <asp:CompareValidator ID="cvFECHAVERIFICA" runat="server" ControlToValidate="txtFechaverifica" ErrorMessage="Formato de Fecha (dd/mm/aaaa)" Operator="DataTypeCheck" SetFocusOnError="True" ToolTip="Formato fecha" Type="Date" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" />
                       <asp:TextBox ID="txtFechaverifica" runat="server" CssClass="textbox" 
                               MaxLength="128" Visible="False" >01/01/2000</asp:TextBox>
                       </td>
    </table>
    <%-- <script type="text/javascript" language="javascript">
        function SetFocus()
        {
            document.getElementById('cphMain_txtCODREFERENCIA').focus(); 
        }
        window.onload = SetFocus;
    </script>--%>
</asp:Content>