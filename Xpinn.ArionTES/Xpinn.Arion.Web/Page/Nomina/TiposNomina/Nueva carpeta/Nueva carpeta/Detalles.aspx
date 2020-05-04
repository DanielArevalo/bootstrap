<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/General/Master/site.master" CodeFile="Detalle.aspx.cs" Inherits="Detalle" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/porcentajeGrid.ascx" TagName="porcentaje" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="../../../Scripts/PCLBryan.js" type="text/javascript"></script>
    <script type="text/javascript">
 
function fieldNumber (objeto)
{
var valorCampo;
var evento_key = window.event.keyCode;
var numPosPunto = 0;
var strParteEntera = "";
var strParteDecimal = "";
var NUM_DECIMALES = 2;
switch (evento_key)
{
case 48:
case 49:
case 50:
case 51:
case 52:
case 53:
case 54:
case 55:
case 56:
case 57:
case 46:
break;
default:
window.event.keyCode = 0;
return false;
}
valorCampo = objeto.value;
if (evento_key == 46)
if (valorCampo.indexOf(".") != -1)
{
window.event.keyCode = 0;
return false;
}
/* Sólo puede teclear el número de decimales indicado en NUM_DECIMALES */
if ((numPosPunto = valorCampo.indexOf(".")) != -1)
{
strParteEntera = valorCampo.substr(0,(numPosPunto - 1));
strParteDecimal = valorCampo.substr((numPosPunto + 1), valorCampo.length)
if (strParteDecimal.length > (NUM_DECIMALES - 1))
{
window.event.keyCode = 0;
return false;
}
}
return true;
} 
 
function validaNum(n,mini,maxi)
{
n = parseInt(n)
if ( n<mini || n>maxi ) alert("El valor del porcentaje debe ser entre 0 - 100");
}
</script>
    <asp:MultiView ID="mvDatos" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <table cellpadding="5" cellspacing="0" style="width: 92%">
                <tr style="text-align: left">
                    <td>
                        <strong>Informacion del Contrato    </strong>
                    </td>
                    <td style="text-align: center; width: 15%; padding: 10px">Codigo Nomina
                    </td>
                    <td style="text-align: center; width: 15%; padding: 10px">
                        <asp:TextBox ID="txtCodigoNomina" Enabled="false" runat="server" CssClass="textbox"></asp:TextBox>
                    </td>
                    <td></td>
                    <td colspan="2"></td>
                </tr>
                <tr>
                    <td colspan="6">
                        <table style="width: 100%; height: 151px;">
                            <tr>
                                <td style="text-align: center; width: 15%; padding: 10px; height: 58px;">Nombre Nomina
                                </td>
                                <td style="text-align: center; width: 41%; padding: 10px; height: 58px;">
                                    <asp:TextBox ID="txtNombreNomina" Width="90%" runat="server" CssClass="textbox"></asp:TextBox>
                                </td>
                                <td style="text-align: center; width: 26%; padding: 10px; height: 58px;">Tipo de Contrato
                                </td>
                                <td style="text-align: center; width: 15%; padding: 10px; height: 58px;">
                                    <asp:DropDownList ID="ddlTipoContrato" runat="server" CssClass="textbox"
                                        Width="90%">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="padding: 10px; height: 57px;"><strong>Tipo de Nomina</strong>
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center; width: 75%; padding: 10px">
                                    <asp:CheckBoxList ID="checkBoxTipoNomina" runat="server" RepeatColumns="4" RepeatDirection="Horizontal" Width="100%" Height="38px">
                                        <asp:ListItem onclick="ExclusiveCheckBoxList(this);" Text="Mensual" Value="1" />
                                        <asp:ListItem onclick="ExclusiveCheckBoxList(this);" Text="Quincenal" Value="2" />
                                        <asp:ListItem onclick="ExclusiveCheckBoxList(this);" Text="1er Periodo" Value="3" />
                                        <asp:ListItem onclick="ExclusiveCheckBoxList(this);" Text="2do Periodo" Value="4" />
                                        <asp:ListItem onclick="ExclusiveCheckBoxList(this);" Text="3er Periodo" Value="5" />
                                        <asp:ListItem onclick="ExclusiveCheckBoxList(this);" Text="4to Periodo" Value="6" />
                                        <asp:ListItem onclick="ExclusiveCheckBoxList(this);" Text="Todos los Periodos" Value="7" />
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: center; padding: 10px; height: 37px;"></td>
                                <td style="text-align: center; width: 26%; padding: 10px; height: 37px;">
                                    <asp:CheckBox ID="chkPermite_anticipos" runat="server" AutoPostBack="true" OnCheckedChanged="chkPermite_anticipos_CheckedChanged" Style="font-size: x-small" Text="Permite Anticipos de Nómina" />
                                </td>
                                <td style="text-align: center; width: 85%; padding: 10px; height: 37px;"><span style="font-size: x-small">
                                    <asp:Label ID="lblporcentaje" runat="server" Text="Porcentaje Anticipo" Visible="false"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtPorcentaje" runat="server" CssClass="textbox" Enabled="true" MaxLength="3" onChange="validaNum(this.value,0,100)" onkeypress="fieldNumber(this)" size="4" Visible="false" Width="30px"></asp:TextBox>
                                    </span></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; padding: 10px" colspan="6">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; width: 15%; padding: 10px">Oficina </td>
                    <td colspan="2" style="text-align: center; padding: 10px">
                        <asp:DropDownList ID="ddlOficina" runat="server" AutoPostBack="true" CssClass="textbox" OnSelectedIndexChanged="ddlOficina_SelectedIndexChanged" Width="90%">
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: center; width: 15%; padding: 10px">Ciudad de la Oficina </td>
                    <td colspan="2" style="text-align: center; padding: 10px">
                        <asp:DropDownList ID="ddlCiudad" runat="server" CssClass="textbox" Enabled="false" Width="90%">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; width: 20%; padding: 10px" rowspan="2">Direccion de la Oficina
                    </td>
                    <td style="text-align: left; padding: 10px" colspan="3" rowspan="2">
                        <asp:TextBox runat="server" ID="txtDireccionOficina" Width="90%" Enabled="false"/>
                    </td>
                    <td style="text-align: left; padding: 10px" colspan="2">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: left; padding: 10px">&nbsp;</td>
                    <td style="text-align: left; padding: 5px">
                        &nbsp;</td>
                </tr>
            </table>
            <br />
            <br />
            <br />
        </asp:View>
        <asp:View ID="vFinal" runat="server">
            <asp:Panel ID="PanelFinal" runat="server">
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
                        <td style="text-align: center; font-size: large;">
                            <asp:Label ID="lblMensaje" runat="server" Text="Operación Realizada Correctamente"
                                Style="color: #FF3300"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                            <br />
                            <br />
                            <br />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
