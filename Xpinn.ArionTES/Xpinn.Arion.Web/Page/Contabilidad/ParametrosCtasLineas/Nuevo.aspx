<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Concepto :." %>
<%@ Register Src="../../../General/Controles/ctlPlanCuentas.ascx" TagName="ListadoPlanCtas" TagPrefix="uc1" %>
<%@ Register Src="../../../General/Controles/ctlPlanCuentasNif.ascx" TagName="ListadoPlanNif" TagPrefix="uc2" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server">
        
        <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="590px">
            <tr>
                <td style="text-align: left;width:120px;">
                    Código<br />
                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="113px" />
                </td>
                <td style="width:120px;">
                    &nbsp;
                </td>
                <td style="width:120px;">
                    &nbsp;
                </td>
                <td style="width:230px;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="3" style="text-align: left; ">
                    Línea de Crédito<br />
                    <asp:DropDownList ID="ddlLineaCredito" runat="server" Height="25px" Width="95%"
                        CssClass="dropdown" AppendDataBoundItems="True">
                        <asp:ListItem Value=""></asp:ListItem>
                    </asp:DropDownList>                   
                </td>
                <td class="tdD" style="width:230px; text-align: left">
                    Atributo<br />
                    <asp:DropDownList ID="ddlAtributo" runat="server" Height="26px" Width="90%" CssClass="dropdown"
                        AppendDataBoundItems="True">
                        <asp:ListItem Value=""></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="text-align: left;">
                    <table>
                        <tr>                        
                            <td style="width: 180px; text-align: left" >
                                Tipo Parametro<br />
                                <asp:DropDownList ID="ddlTipo" runat="server" Height="26px" Width="90%" 
                                    CssClass="dropdown" AutoPostBack="True" 
                                    onselectedindexchanged="ddlTipo_SelectedIndexChanged">
                                    <asp:ListItem Value=""></asp:ListItem>
                                    <asp:ListItem Value="0">Operaciones</asp:ListItem>
                                    <asp:ListItem Value="1">Clasificación</asp:ListItem>
                                    <asp:ListItem Value="2">Provisión</asp:ListItem>
                                    <asp:ListItem Value="3">Causación</asp:ListItem>
                                    <asp:ListItem Value="4">Provisión General</asp:ListItem>
                                    <asp:ListItem Value="5">Garantias</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="width: 180px; text-align: left">
                                Tipo Cuenta<br />
                                <asp:DropDownList ID="ddlTipoCuenta" runat="server" Height="26px" Width="90%" CssClass="dropdown">
                                    <asp:ListItem Value=""></asp:ListItem>
                                    <asp:ListItem Value="1">Normal</asp:ListItem>
                                    <asp:ListItem Value="2">Causado</asp:ListItem>
                                    <asp:ListItem Value="3">Orden</asp:ListItem>
                                </asp:DropDownList>
                            </td>                            
                            <td style="width: 230px; text-align: left">
                                Estructura<br />
                                <asp:DropDownList ID="ddlEstructura" runat="server" Height="26px" Width="200px" CssClass="dropdown"
                                    AppendDataBoundItems="True">
                                    <asp:ListItem Value=""></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 120px; text-align: left">
                                <asp:Label ID="lblCategoria" runat="server" Text="Categoria" /><br />
                                <asp:DropDownList ID="ddlCategoria" runat="server" Height="26px" Width="90%" CssClass="dropdown"
                                    AppendDataBoundItems="True">
                                    <asp:ListItem Value=""></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="width: 120px; text-align: left">
                                <asp:Label ID="lblLibranza" runat="server" Text="Libranza" /><br />
                                <asp:DropDownList ID="ddlLibranza" runat="server" Height="26px" Width="110px" CssClass="dropdown"
                                    AppendDataBoundItems="True">
                                    <asp:ListItem Value=""></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="width: 120px; text-align: left">
                                <asp:Label ID="lblGarantia" runat="server" Text="Garantia" /><br />
                                <asp:DropDownList ID="ddlGarantia" runat="server" Height="26px" Width="110px" CssClass="dropdown"
                                    AppendDataBoundItems="True">
                                    <asp:ListItem Value=""></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width:230px; text-align: left">
                    &nbsp;
                </td>                             
            </tr>            
            <tr>
                <td colspan="4" style="width:590px; text-align: left">
                    <table>
                        <tr>
                            <td style="width: 130px; text-align: left">
                                Cod Cuenta<br />
                                <cc1:TextBoxGrid ID="txtCodCuenta" runat="server" AutoPostBack="True" Style="text-align: left"
                                    CssClass="textbox" Width="120px" OnTextChanged="txtCodCuenta_TextChanged">    
                                </cc1:TextBoxGrid>                               
                                <uc1:ListadoPlanCtas ID="ctlListadoPlan" runat="server" />
                            </td>
                            <td style="width: 25px; text-align: center">
                                <br />
                                <cc1:ButtonGrid ID="btnListadoPlan" CssClass="btnListado" runat="server" Text="..." Width="95%"
                                    OnClick="btnListadoPlan_Click"/>                                
                            </td>
                            <td style="width: 230px; text-align: left">
                                Nombre de la Cuenta<br />
                                <cc1:TextBoxGrid ID="txtNomCuenta" runat="server" Style="text-align: left"
                                    BackColor="#F4F5FF" Width="200px" CssClass="textbox"
                                    Enabled="False">
                                </cc1:TextBoxGrid>                               
                            </td>
                            <td style="width: 190px; text-align: left">
                                Tipo Mov.<br />
                                <asp:DropDownList ID="ddlTipoMov" runat="server" Height="26px" Width="170px" CssClass="dropdown"
                                    AppendDataBoundItems="True">
                                    <asp:ListItem Value="1">Débito</asp:ListItem>
                                    <asp:ListItem Value="2">Crédito</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>                
            </tr>
            <tr>
                <td colspan="4" style="width:590px; text-align: left">
                    <table>
                        <tr>
                            <td style="width: 130px; text-align: left">
                                Cod Cuenta NIIF
                                <br />
                                <cc1:TextBoxGrid ID="txtCodCuentaNIF" runat="server" AutoPostBack="True" Style="text-align: left"
                                    CssClass="textbox" Width="120px" OnTextChanged="txtCodCuentaNIF_TextChanged">    
                                </cc1:TextBoxGrid> 
                                <uc2:ListadoPlanNif ID="ctlListadoPlanNif" runat="server" />                               
                            </td>
                            <td style="width: 25px; text-align: center">
                            <br />
                            <cc1:ButtonGrid ID="btnListadoPlanNIF" CssClass="btnListado" runat="server" Text="..." Width="95%"
                                    OnClick="btnListadoPlanNIF_Click" />                                
                            </td>
                            <td style="width: 230px; text-align: left">
                                Nombre de la Cuenta
                                <br />
                                <cc1:TextBoxGrid ID="txtNomCuentaNif" runat="server" Style="text-align: left" 
                                CssClass="textbox" Width="200px"
                                    Enabled="False">
                                </cc1:TextBoxGrid>
                            </td>
                            <td style="width: 190px; text-align: left">
                            &nbsp;
                            </td>
                        </tr>
                    </table>                                        
                </td>
            </tr>
            <tr>
                <td colspan="4" style="text-align: left">
                    Tipo de Transacción
                    <br />
                    <asp:DropDownList ID="ddlTipoTran" runat="server" Height="26px" Width="80%" CssClass="dropdown"
                        AppendDataBoundItems="True">
                        <asp:ListItem Value=""></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="tdD" style="width: 230px">
                    &nbsp;
                </td>               
            </tr>
        </table>
    </asp:Panel>
</asp:Content>