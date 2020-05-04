<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"   CodeFile="Nuevo.aspx.cs" Inherits="AseClienteNuevo" %>
<%@ Register Src="../../../General/Controles/direccion.ascx" TagName="direccion" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table style="width: 100%">
        <tr>
            <td>                         
                <asp:Label ID="Labelerror" runat="server" style="color: #FF0000; font-weight: 700" colspan="5" 
                    Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: center; color: #FFFFFF; background-color: #0066FF" colspan="5">
                <strong style="text-align: center">Información Cliente</strong>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: 467px">
                Primer Nombre
                <asp:RequiredFieldValidator ID="rfvPrimerNombre" runat="server" ErrorMessage="Campo Requerido"
                    ControlToValidate="txtPrimerNombre" Display="Dynamic" ForeColor="Red" 
                    ValidationGroup="vgGuardar" style="font-size: x-small" />
                <br />
                <asp:TextBox ID="txtPrimerNombre" runat="server" CssClass="textbox" Width="50%"></asp:TextBox>
            </td>
            <td>
                Segundo Nombre
                <br />
                <asp:TextBox ID="txtSegundoNombre" runat="server" CssClass="textbox" Width="50%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 467px">
                Primer Apellido
                <asp:RequiredFieldValidator ID="rfvPrimerApellido" runat="server" ErrorMessage="Campo Requerido"
                    ControlToValidate="txtPrimerApellido" Display="Dynamic" ForeColor="Red" 
                    ValidationGroup="vgGuardar" style="font-size: x-small" />
                <br />
                <asp:TextBox ID="txtPrimerApellido" runat="server" CssClass="textbox" Width="50%"></asp:TextBox>
            </td>
            <td>
                Segundo Apellido<br />
                <asp:TextBox ID="txtSegundoApellido" runat="server" CssClass="textbox" Width="50%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 467px">
                <br />
                Tipo Documento
                <asp:RequiredFieldValidator ID="rfvDdlTipoDoc" runat="server" ErrorMessage="Campo Requerido"
                    ControlToValidate="ddlTipoDoc" Display="Dynamic" ForeColor="Red" 
                    ValidationGroup="vgGuardar" style="font-size: x-small" />
                <br />
              <asp:DropDownList ID="ddlTipoDoc" runat="server" CssClass="dropdown" Width="52%">
                </asp:DropDownList>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="rfvTxtNumDoc" runat="server" ErrorMessage="Campo Requerido"
                    ControlToValidate="txtNumDoc" Display="Dynamic" ForeColor="Red" 
                    ValidationGroup="vgGuardar" style="font-size: x-small" />
                <asp:RegularExpressionValidator ID="revNumDoc" runat="server" ValidationExpression="^\d+$"
                    ErrorMessage="Solo Numeros" ControlToValidate="txtNumDoc" ValidationGroup="vgGuardar"
                    ForeColor="Red" style="font-size: x-small"></asp:RegularExpressionValidator>
                <br />
                Número Documento&nbsp;<br />
                <asp:TextBox ID="txtNumDoc" runat="server" CssClass="textbox" MaxLength="15" Width="50%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td >
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Teléfono
                <asp:RequiredFieldValidator ID="rfvTxtTelefono" runat="server" ErrorMessage="Campo Requerido"
                    ControlToValidate="txtTelefono" Display="Dynamic" ForeColor="Red" 
                    ValidationGroup="vgGuardar" style="font-size: x-small" />
                <asp:RegularExpressionValidator ID="revTxtTelefono" runat="server" ValidationExpression="^\d+$"
                    ErrorMessage="Solo Numeros" ControlToValidate="txtTelefono" ValidationGroup="vgGuardar"
                    ForeColor="Red" style="font-size: x-small"></asp:RegularExpressionValidator>
                <br />
                <asp:TextBox ID="txtTelefono" runat="server" CssClass="textbox" MaxLength="12" Width="50%"></asp:TextBox>
            </td>
            <td>
                Correo Electrónico
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Correo Invalido"
                    ValidationGroup="vgGuardar" ControlToValidate="txtEmail" ForeColor="Red" Display="Dynamic" 
                    
                    ValidationExpression="[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+(?:[A-Z]{2}|co|es|com|org|net|edu|gov|mil|biz|info|mobi|name|aero|asia|jobs|museum)\b" 
                    style="font-size: x-small"> </asp:RegularExpressionValidator>
                <br />
                <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox" Width="50%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 467px">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        Zona
                        <asp:RequiredFieldValidator ID="rfvZona" runat="server" ErrorMessage="Campo Requerido"
                            ControlToValidate="ddlZona" Display="Dynamic" ForeColor="Red" 
                            ValidationGroup="vgGuardar" style="font-size: x-small" />
                        <br />
                        <asp:DropDownList ID="ddlZona" runat="server" CssClass="dropdown" Width="54%">
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        Ejecutivo
                        <asp:RequiredFieldValidator ID="rfvZona0" runat="server" ErrorMessage="Campo Requerido"
                            ControlToValidate="ddlZona" Display="Dynamic" ForeColor="Red" 
                            ValidationGroup="vgGuardar" style="font-size: x-small" />
                        <br />
                        <asp:DropDownList ID="ddlejecutivo" runat="server" CssClass="dropdown" Width="54%"
                            AutoPostBack="True">
                        </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlZona" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        </table>
    <table style="width: 100%">
        <tr>
            <td>
            </td>
            <td style="text-align: left">
                &nbsp;Dirección Correspondencia:
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
            <td>
                <asp:TextBox ID="txtDirCorrespondencia" runat="server" CssClass="textbox" TabIndex="10" />
                <%--</div>--%>
            </td>
        </tr>
    </table>
    <table style="width: 100%">
        <tr>
            <td style="text-align: center; color: #FFFFFF; background-color: #0066FF" colspan="5">
                <strong style="text-align: center">Información Empresa</strong>
            </td>
         
        </tr>
        </table>
        <table width="100%">
        <tr>
            <td style="width: 480px">
                Razón Social
                <br />
                <asp:TextBox ID="txtRazonSocial" runat="server" CssClass="textbox" Width="80%"></asp:TextBox>
            </td>
            <td>
                Sigla
                <br />
                <asp:TextBox ID="txtSigla" runat="server" CssClass="textbox" Width="80%"></asp:TextBox>
            </td>
            </tr>
            <tr>
            <td colspan="2">
                Actividad
                &nbsp;Económica<br />
                <asp:DropDownList ID="ddlActividad" runat="server" CssClass="dropdown" Width="50%"/>
            </td>
        </tr>
    </table>
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
            <table style="width: 100%">
                <tr> 
                    <td style="text-align: center; color: #FFFFFF; background-color: #0066FF; width: 474px;" colspan="2" >
                            <strong style="text-align: center">Motivos de Afiliación</strong>                      
                    </td>
                </tr>
                <tr>     
                    <td style="width: 474px" >
                        <asp:Label ID="lbMotAfili" runat="server" Text="Label">Motivo Afiliación</asp:Label>
                        <asp:RequiredFieldValidator ID="rfvDdlMotAfilia" runat="server" ErrorMessage="Campo Requerido"
                            ControlToValidate="ddlMotAfilia" Display="Dynamic" ForeColor="Red" ValidationGroup="vgGuardar" />
                        <br />
                        <asp:DropDownList ID="ddlMotAfilia" runat="server" CssClass="dropdown" 
                            Width="212px" />
                    </td>
                    <td >
                        <asp:Label ID="lbObsAfilia" runat="server" Text="Label">Observaciones</asp:Label>
                        <br />
                        <asp:TextBox ID="txtObservaciones" runat="server" CssClass="textbox" MaxLength="80"
                            Width="90%" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
            </table>
        </asp:View>
    <asp:View ID="View2" runat="server">
        <table style="width: 100%">
            <tr>                  
                <td style="text-align: center; color: #FFFFFF; background-color: #0066FF; width: 463px; " colspan="2">
                    <strong style="text-align: center">Motivos de Modificación</strong>                      
                </td>
            </tr>
            <tr>     
               <td style="width: 474px" >
                    <asp:Label ID="lbMotMod" runat="server" Text="Label">Motivo Modificación</asp:Label>
                    <asp:RequiredFieldValidator ID="rfvDdlMotModi" runat="server" ErrorMessage="Campo Requerido"
                        ControlToValidate="ddlMotModi" Display="Dynamic" ForeColor="Red" ValidationGroup="vgGuardar" />
                    <br />
                    <asp:DropDownList ID="ddlMotModi" runat="server" CssClass="dropdown" Width="215px" />
                </td>
                <td>
                    <asp:Label ID="lbObsMod" runat="server" Text="Label">Observaciones</asp:Label>
                    <br />
                    <asp:TextBox ID="txtObservacionModif" runat="server" CssClass="textbox" MaxLength="80"
                        Width="90%" Height="29px"></asp:TextBox>
                </td>
            </tr>
        </table>
    </asp:View>

    </asp:MultiView>
    
    <table style="width: 102%;">
        <tr>
            <td>
                <hr style="height: -12px" />
            </td>
        </tr>      
    </table>
   
</asp:Content>
