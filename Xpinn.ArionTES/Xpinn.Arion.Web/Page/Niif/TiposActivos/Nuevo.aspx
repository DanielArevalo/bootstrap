<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - NIIF TipoActivo :." %>
<%@ Register Src="../../../General/Controles/ctlPlanCuentas.ascx" TagName="ListadoPlanCtas" TagPrefix="uc1" %>
<%@ Register Src="../../../General/Controles/ctlPlanCuentasNif.ascx" TagName="ListadoPlanNif" TagPrefix="uc2" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwData" runat="server">
        <table id="tbCriterios" border="0" cellpadding="3" cellspacing="0" width="610px">
            <tr>
                <td style="text-align: left; width: 160px;">
                    Código<br />
                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="90%" />
                    <asp:TextBox ID="lblAuto" runat="server" CssClass="textbox" Width="90%" Text="AUTOGENERADO"
                        Enabled="false" />
                </td>
                <td style="width: 220px;">
                    Descripción<br />
                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" Width="90%" />
                </td>
                <td style="width: 230px;">
                    Clasificación<br />
                    <asp:DropDownList ID="ddlClasificacion" runat="server" Height="26px" Width="90%"
                        CssClass="dropdown" AppendDataBoundItems="True">
                        <asp:ListItem Value=""></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="3" cellspacing="0" width="610px">
            <tr>
                <td style="width: 180px; text-align: left">
                    Cod Cuenta<br />
                    <cc1:TextBoxGrid ID="txtCodCuentaNIF" runat="server" AutoPostBack="True" Style="text-align: left"
                        CssClass="textbox" Width="110px" OnTextChanged="txtCodCuentaNIF_TextChanged">    
                    </cc1:TextBoxGrid>
                    <cc1:ButtonGrid ID="btnListaNIF" CssClass="btnListado" runat="server" Text="..."
                       OnClick="btnListaNIF_Click" />
                    <uc2:ListadoPlanNif ID="ctlListadoPlanNif" runat="server" />
                </td>
                <td colspan="2" style="text-align: left">
                    Nombre de la Cuenta<br />
                    <cc1:TextBoxGrid ID="txtNomCuentaNif" runat="server" Style="text-align: left" CssClass="textbox"
                        Width="370px" Enabled="False">
                    </cc1:TextBoxGrid>
                </td>
            </tr>
            <tr>
                <td style="width: 180px; text-align: left">
                    Cod Cuenta Deterioro<br />
                    <cc1:TextBoxGrid ID="txtCodCuentaNIF2" runat="server" AutoPostBack="True" Style="text-align: left"
                        CssClass="textbox" Width="110px" OnTextChanged="txtCodCuentaNIF2_TextChanged">    
                    </cc1:TextBoxGrid>
                    <cc1:ButtonGrid ID="btnListaNIF2" CssClass="btnListado" runat="server" Text="..."
                        OnClick="btnListaNIF2_Click" />
                    <uc2:ListadoPlanNif ID="ctlListadoPlanNif2" runat="server" />
                </td>
                <td colspan="2" style="text-align: left">
                    Nombre de la Cuenta<br />
                    <cc1:TextBoxGrid ID="txtNomCuentaNif2" runat="server" Style="text-align: left" BackColor="#F4F5FF"
                        Width="370px" CssClass="textbox" Enabled="False">
                    </cc1:TextBoxGrid>
                </td>
            </tr>
            <tr>
                <td style="width: 180px; text-align: left">
                    Cod Cuenta Revaluación<br />
                    <cc1:TextBoxGrid ID="txtCodCuentaNIF3" runat="server" AutoPostBack="True" Style="text-align: left"
                        CssClass="textbox" Width="110px" OnTextChanged="txtCodCuentaNIF3_TextChanged">    
                    </cc1:TextBoxGrid>
                    <cc1:ButtonGrid ID="btnListaNIF3" CssClass="btnListado" runat="server" Text="..."
                        OnClick="btnListaNIF3_Click" />
                    <uc2:ListadoPlanNif ID="ctlListadoPlanNif3" runat="server" />
                </td>
                <td colspan="2" style="text-align: left">
                    Nombre de la Cuenta<br />
                    <cc1:TextBoxGrid ID="txtNomCuentaNif3" runat="server" Style="text-align: left" BackColor="#F4F5FF"
                        Width="370px" CssClass="textbox" Enabled="False">
                    </cc1:TextBoxGrid>
                </td>
            </tr>
            <tr>
                <td style="width: 180px; text-align: left">
                    Cod Cuenta Gasto Deterioro<br />
                    <cc1:TextBoxGrid ID="txtCodCuentaNIF4" runat="server" AutoPostBack="True" Style="text-align: left"
                        CssClass="textbox" Width="110px" OnTextChanged="txtCodCuentaNIF4_TextChanged">    
                    </cc1:TextBoxGrid>
                    <cc1:ButtonGrid ID="btnListaNIF4" CssClass="btnListado" runat="server" Text="..."
                        OnClick="btnListaNIF4_Click" />
                    <uc2:ListadoPlanNif ID="ctlListadoPlanNif4" runat="server" />
                </td>
                <td colspan="2" style="text-align: left">
                    Nombre de la Cuenta<br />
                    <cc1:TextBoxGrid ID="txtNomCuentaNif4" runat="server" Style="text-align: left" BackColor="#F4F5FF"
                        Width="370px" CssClass="textbox" Enabled="False">
                    </cc1:TextBoxGrid>
                </td>
            </tr>            
        </table>
        </asp:View>
        
        <asp:View ID="vwFinal" runat="server">
            <table style="width: 100%;">
                <tr>
                    <td style="text-align: center; font-size: large;">
                        <br />
                        <br />
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; font-size: large; color:Red">
                        Tipo de Activo <asp:Label ID="lblmsjFin" runat="server" /> Correctamente
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; font-size: large;">
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; font-size: large;">
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>

    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>