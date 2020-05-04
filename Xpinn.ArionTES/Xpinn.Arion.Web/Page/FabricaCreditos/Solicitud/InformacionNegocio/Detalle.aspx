<%@ Page Language="C#" MasterPageFile="~/General/Master/solicitud.master" AutoEventWireup="true" CodeFile="Detalle.aspx.cs" Inherits="Detalle" Title=".: Xpinn - InformacionNegocio :." %>
<%@ Register src="../../../../General/Controles/direccion.ascx" tagname="direccion" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
   
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
                   <tr>
                       <td class="tdI">
                       <asp:TextBox ID="txtCod_negocio" runat="server" CssClass="textbox" Enabled="false" 
                               Visible="False"/>
                       </td>
                       <td class="tdD">
                       <asp:TextBox ID="txtCod_persona" runat="server" CssClass="textbox" Enabled="false" 
                               Visible="False"/>
                       </td>
                       <td class="tdD">
                           &nbsp;</td>
                   </tr>
                   <tr>
                       <td class="tdI" colspan="2">
                       Dirección&nbsp;</td>
                       <td class="tdI">
                           &nbsp;</td>
                   </tr>
                   <tr>
                       <td class="tdI" colspan="3">
                           <uc1:direccion ID="direccion" runat="server" 
                               Requerido="True" Enabled="False" />
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                           Tipo de local<asp:RadioButtonList ID="rblTipoLocal" 
                               runat="server" RepeatDirection="Horizontal" Enabled="False">
                            <asp:ListItem Selected="True" Value="1">Propio</asp:ListItem>
                            <asp:ListItem Value="0">Arrendado</asp:ListItem>
                        </asp:RadioButtonList>
                       </td>
                       <td class="tdD">
                       Arrendador&nbsp;<br />
                       <asp:TextBox ID="txtArrendador" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Telefono arrendador&nbsp;<br />
                       <asp:TextBox ID="txtTelefonoarrendador" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                           Localidad<br />
                                <asp:DropDownList ID="ddlLocalidad" runat="server" AutoPostBack="True" 
                               Enabled="False">
                                </asp:DropDownList>
                       </td>
                       <td class="tdD">
                                Barrio<br __designer:mapid="b25" />
                                <asp:DropDownList ID="ddlBarrio" runat="server" Enabled="False">
                                </asp:DropDownList>
                       </td>
                       <td class="tdD">
                       Nombre negocio&nbsp;<br />
                       <asp:TextBox ID="txtNombrenegocio" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Teléfono&nbsp;<br />
                       <asp:TextBox ID="txtTelefono" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                           Actividad<asp:RadioButtonList ID="rblActividad" runat="server" 
                               RepeatDirection="Horizontal" Enabled="False">
                            <asp:ListItem Selected="True" Value="0">C</asp:ListItem>
                            <asp:ListItem Value="1">P</asp:ListItem>
                               <asp:ListItem Value="2">S</asp:ListItem>
                        </asp:RadioButtonList>
                       </td>
                       <td class="tdD">
                       Descripción&nbsp;<br />
                       <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Antiguedad&nbsp;<br />
                       <asp:TextBox ID="txtAntiguedad" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Empleados permanentes<br />
                       <asp:TextBox ID="txtEmplperm" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                       Experiencia&nbsp;<br />
                       <asp:TextBox ID="txtExperiencia" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                   </tr>
                   <tr>
                      <td class="tdI">
                       Empleados temporales&nbsp;<br />
                       <asp:TextBox ID="txtEmpltem" runat="server" CssClass="textbox" Enabled="false"/>
                       </td>
                       <td class="tdD">
                           &nbsp;</td>
                       <td class="tdD">
                           &nbsp;</td>
                   </tr>
                   </table>
</asp:Content>