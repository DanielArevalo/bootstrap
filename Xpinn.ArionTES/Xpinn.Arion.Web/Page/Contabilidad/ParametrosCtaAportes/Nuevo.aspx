<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Concepto :." %>

<%@ Register Src="../../../General/Controles/ctlPlanCuentas.ascx" TagName="ListadoPlanCtas"
    TagPrefix="uc1" %>
<%@ Register Src="../../../General/Controles/ctlPlanCuentasNif.ascx" TagName="ListadoPlanNif"
    TagPrefix="uc2" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:MultiView ID="mvParametros" runat="server" ActiveViewIndex="0">
    <asp:View ID="vwDatos" runat="server">
    <asp:Panel ID="pConsulta" runat="server">
        <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="650px">
            <tr>
                <td style="text-align: left; width: 150px;">
                    Código<br />
                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="90%" />
                </td>
                <td style="width: 500;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: left;">
                    Línea de Aporte<br />
                    <asp:DropDownList ID="ddlLineaAporte" runat="server" Height="25px" Width="90%" CssClass="textbox"
                        AppendDataBoundItems="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: left">
                    Estructura<br />
                    <asp:DropDownList ID="ddlEstructura" runat="server" Height="25px" Width="90%" CssClass="textbox"
                        AppendDataBoundItems="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align: left; width: 150px">
                    Cuenta Contable<br />
                    <cc1:TextBoxGrid ID="txtCodCuenta" runat="server" AutoPostBack="True" CssClass="textbox"
                        Style="text-align: left" BackColor="#F4F5FF" Width="100px" OnTextChanged="txtCodCuenta_TextChanged"></cc1:TextBoxGrid>
                    <cc1:ButtonGrid ID="btnListadoPlan" CssClass="btnListado" runat="server" Text="..."
                        OnClick="btnListadoPlan_Click" />
                </td>
                <td style="text-align: left; width: 500px">
                    Nombre de la Cuenta<br />
                    <uc1:ListadoPlanCtas ID="ctlListadoPlan" runat="server" />
                    <cc1:TextBoxGrid ID="txtNomCuenta" runat="server" Style="text-align: left" BackColor="#F4F5FF"
                        Width="80%" Enabled="False" CssClass="textbox"></cc1:TextBoxGrid>
                </td>
            </tr>
            <tr>
                <td style="width: 150px; text-align: left">
                    Cod Cuenta NIIF<br />
                    <cc1:TextBoxGrid ID="txtCodCuentaNIF" runat="server" AutoPostBack="True" Style="text-align: left"
                        CssClass="textbox" Width="100px" OnTextChanged="txtCodCuentaNIF_TextChanged">    
                    </cc1:TextBoxGrid>
                    <cc1:ButtonGrid ID="btnListadoPlanNIF" CssClass="btnListado" runat="server" Text="..."
                        OnClick="btnListadoPlanNIF_Click" />
                </td>
                <td style="text-align: left">
                    Nombre de la Cuenta<br />
                    <uc2:ListadoPlanNif ID="ctlListadoPlanNif" runat="server" />
                    <cc1:TextBoxGrid ID="txtNomCuentaNif" runat="server" Style="text-align: left" CssClass="textbox"
                        Width="80%" Enabled="False">
                    </cc1:TextBoxGrid>
                </td>
            </tr>
            <tr>
                <td style="width: 150px; text-align: left">
                    Tipo de Movimiento<br />
                    <asp:DropDownList ID="ddlTipoMov" runat="server" Height="26px" Width="170px" CssClass="textbox" />
                </td>
                <td style="text-align: left">
                    Tipo de Transacción<br />
                    <asp:DropDownList ID="ddltransac" runat="server" Height="26px" Width="80%" CssClass="textbox"
                        AppendDataBoundItems="True">                        
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </asp:Panel>
    </asp:View>
    <asp:View ID="vwFin" runat="server">
    <table style="width:100%">
    <tr>
    <td style="text-align:center">
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <asp:Label ID="lblmsj" runat="server" style="color:Red" Font-Bold="True" /><br />
    <asp:Button ID="btnFin" runat="server" CssClass="btn8" Height="28px" 
            Text="  Regresar  " onclick="btnFin_Click"/>
    </td>
    </tr>
    </table>
    
    </asp:View>
    </asp:MultiView>
    
</asp:Content>
