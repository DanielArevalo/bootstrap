<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../../General/Controles/direccion.ascx" TagName="direccion" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Src="../../../General/Controles/ctlPlanCuentas.ascx" TagName="ListadoPlanCtas" TagPrefix="uc1" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table cellpadding="2" cellspacing="0" style="width: 20%">
        <tr>
            <td class="tdI" style="width: 349px" align="left">
                C&oacute;digo *</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            <td class="tdD" style="text-align:left">
                Fecha de Creación</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
             <td class="tdf" style="text-align:left">
                Estado
             </td>
        </tr>
        <tr>
            <td class="tdI" style="width: 349px" align="left">
                <asp:TextBox ID="txtCodigo" MaxLength="4" runat="server" CssClass="textbox" 
                    Width="106px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvCodigo" runat="server" 
                    ErrorMessage="Campo Requerido" ControlToValidate="txtCodigo" Display="Dynamic" 
                    ForeColor="Red" ValidationGroup="vgGuardar" style="font-size: x-small"/>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td class="tdD" style="text-align:left">
                <uc1:fecha ID="txtFechaCreacion" runat="server" Enabled="True" />
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
             <td class="tdf"align="left">
                
                <asp:DropDownList ID="ddlestado" CssClass="textbox"  runat="server" 
                     MaxLength="10" Width="110px">
                     <asp:ListItem Value="1">Activo</asp:ListItem>
                     <asp:ListItem Value="0">Inactivo</asp:ListItem>
                     <asp:ListItem Value="2">Cerrada</asp:ListItem>
                </asp:DropDownList> 
            </td>
        </tr>
        </table>
        <table cellpadding="2" cellspacing="0" style="width: 20%">
        <tr>
            <td class="tdI" style="width: 349px" align="left">
                Nombre *</td>
            <td class="tdD" style="text-align:left">
                Ciudad</td>
        </tr>
        <tr>
            <td class="tdI" style="width: 349px" align="left">
                <asp:TextBox ID="txtOficina" runat="server" MaxLength="50" CssClass="textbox" 
                    Width="343px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvOficina" runat="server" 
                    ErrorMessage="Campo Requerido" ControlToValidate="txtOficina" Display="Dynamic" 
                    ForeColor="Red" ValidationGroup="vgGuardar" style="font-size: x-small"/>
            </td>
            <td style="text-align: left">
                <asp:DropDownList ID="ddlCiudades" CssClass="textbox" runat="server" Width="270px">
                </asp:DropDownList>
            </td>
        </tr>    
    </table>
    <table cellpadding="5" cellspacing="0" style="width: 100%; text-align:left">
        <tr>
            <td style="width: 484px" align="left">
                Dirección*                   
            </td>
        </tr>    
        <tr>
            <td style="width: 484px" align="left">
                <uc1:direccion ID="txtDirCorrespondencia" runat="server" aling="center" Width="50%" />
            </td>
        </tr>    
    </table>
    <table cellpadding="4" cellspacing="5" style="text-align:left">        
        <tr>
            <td align="left">
                Teléfono<br />
                <asp:TextBox ID="txtTelefono" MaxLength="20" runat="server" CssClass="textbox" Width="120px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Campo Requerido"
                    ControlToValidate="txtTelefono" Display="Dynamic" ForeColor="Red" ValidationGroup="vgGuardar" />
            </td>
            <td style="text-align: left">
                Encargado<br />
                <%--<asp:DropDownList ID="ddlEncargados" CssClass="textbox" runat="server" Width="280px">
                </asp:DropDownList>--%>
                <asp:TextBox ID="txtNombreEncargado" MaxLength="20" runat="server" CssClass="textbox" Width="280px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfv2" runat="server" ErrorMessage="Campo Requerido" ControlToValidate="txtNombreEncargado" Display="Dynamic" ForeColor="Red" ValidationGroup="vgGuardar" />
                <asp:Button ID="btnConsultaPersonas" CssClass="btn8" runat="server" Text="..." Height="26px" OnClick="btnConsultaPersonas_Click" />
                <uc1:ListadoPersonas ID="ctlBusquedaPersonas" runat="server" />
                <asp:TextBox ID="txtCodEncargado" MaxLength="20" runat="server" CssClass="textbox" Width="120px" Visible="false"></asp:TextBox>                                
            </td>
            <td style="text-align: left">
                Centro de Costo<br />
                <asp:DropDownList ID="ddlCentrosCosto" CssClass="textbox" runat="server" Width="182px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
                Cod Cuenta<br />
                <cc1:TextBoxGrid ID="txtCodCuenta" runat="server" AutoPostBack="True" Style="text-align: left"
                    CssClass="textbox" Width="100px" OnTextChanged="txtCodCuenta_TextChanged">    
                </cc1:TextBoxGrid>
                <cc1:ButtonGrid ID="btnListadoPlan" CssClass="btnListado" runat="server" Text="..."
                    Width="22px" OnClick="btnListadoPlan_Click" />
                <uc1:ListadoPlanCtas ID="ctlListadoPlan" runat="server" />
            </td>
            <td style="text-align: left">
                Nombre de la Cuenta<br />
                <cc1:TextBoxGrid ID="txtNomCuenta" runat="server" Style="text-align: left" Width="280px"
                    CssClass="textbox" Enabled="False">
                </cc1:TextBoxGrid>
            </td>
            <td style="text-align: left">
                Oficina Supersolidaria<br />
                <asp:DropDownList ID="ddlOficinaSuper" CssClass="dropdown"  runat="server" 
                    Height="24px" Width="180px" AutoPostBack="True" Enabled="true">
                </asp:DropDownList> 
            </td>
        </tr>
        <tr>
            <td align="left">
                Sede Propia<br />
                <asp:RadioButtonList ID="rblSedePropia" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1" Selected="true"> Si </asp:ListItem>
                    <asp:ListItem Value="0"> No </asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td style="text-align: left">
                <asp:UpdatePanel ID="upIndicador" runat="server">
                    <ContentTemplate>
                        Indicador Corresponsal<br />
                        <asp:RadioButtonList ID="rblIndicadorCorres" runat="server" RepeatDirection="Horizontal"
                            AutoPostBack="true" OnSelectedIndexChanged="rblIndicadorCorres_SelectedIndexChanged">
                            <asp:ListItem Value="0" Selected="true"> Oficina </asp:ListItem>
                            <asp:ListItem Value="1"> Corresponsal </asp:ListItem>
                        </asp:RadioButtonList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td style="text-align: left">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Label ID="lblTipoNegocio" runat="server" Text="Tipo de Negocio"></asp:Label><br />
                        <asp:DropDownList ID="ddlTipoNegocio" CssClass="textbox" runat="server" Width="182px">
                        </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="rblIndicadorCorres" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>


