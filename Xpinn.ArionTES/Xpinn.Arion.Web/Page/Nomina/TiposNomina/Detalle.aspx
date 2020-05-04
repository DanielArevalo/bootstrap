<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/General/Master/site.master" CodeFile="Detalle.aspx.cs" Inherits="Detalle" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register src="~/General/Controles/fecha.ascx" tagname="fecha" tagprefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
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

    <script src="../../../Scripts/PCLBryan.js" type="text/javascript"></script>
    <asp:MultiView ID="mvDatos" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <table cellpadding="5" cellspacing="0" style="width: 75%">
                <tr>
                    <td style="text-align: left; width: 90%; padding: 10px" colspan="4">
                        <strong>Datos de Tipo de Nómina</strong>
                        <hr />
                    </td>
                    <td></td>
                </tr>
                <tr style="text-align: left">
                    <td style="text-align: Left; width: 25%; padding: 10px">                        
                        Código
                    </td>
                    <td style="text-align: Left; width: 35%; padding: 10px">
                         <asp:TextBox ID="txtCodigoNomina" Enabled="false" runat="server" CssClass="textbox"></asp:TextBox>
                    </td>
                    <td style="text-align: Left; width: 30%; padding: 10px">
                        &nbsp;</td>
                    <td style="text-align: Left; width: 35%; padding: 10px">
                        <asp:DropDownList ID="ddlTipoContrato" runat="server" Visible="false" CssClass="textbox" Width="95%" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td style="text-align: Left; padding: 10px; width: 25%; height: 27px;">
                        Descripción
                    </td>
                    <td style="text-align: Left; padding: 10px; height: 27px;" colspan="3">
                        <asp:TextBox ID="txtNombreNomina" runat="server" CssClass="textbox" Width="97%" />
                    </td>
                    <td style="height: 27px"></td>
                </tr>
                <tr>
                    <td style="text-align: Left; padding: 10px" colspan="4">
                        <strong>Periodicidad</strong>
                        <hr />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td style="text-align: Left; padding: 10px" colspan="2">
                        <asp:CheckBoxList ID="checkBoxTipoNomina" runat="server" Width="100%" RepeatDirection="Horizontal" RepeatColumns="3">
                            <asp:ListItem Text="Semanal"            Value="3" onclick="ExclusiveCheckBoxList(this);" />
                            <asp:ListItem Text="Bisemanal"          Value="5" onclick="ExclusiveCheckBoxList(this);" />
                            <asp:ListItem Text="Decadal"            Value="4" onclick="ExclusiveCheckBoxList(this);" />
                            <asp:ListItem Text="Mensual"            Value="1" onclick="ExclusiveCheckBoxList(this);" />
                            <asp:ListItem Text="Quincenal"          Value="2" onclick="ExclusiveCheckBoxList(this);" />
                        </asp:CheckBoxList>
                    </td>
                    <td style="text-align: Left; padding: 10px; width: 30%;">
                        Fecha Ult.Liquidación
                    </td>
                    <td style="text-align: Left; padding: 10px">
                        <uc:fecha ID="txtFechaCorte" runat="server" Enabled="false" CssClass="textbox" Width="85px" />
                    </td>
                    <td></td>
                </tr>
               <tr>
                                <td colspan="2" style="text-align: center; padding: 10px; height: 6px;">
                                    <asp:CheckBox ID="chkPermite_anticipos" runat="server" AutoPostBack="true" OnCheckedChanged="chkPermite_anticipos_CheckedChanged" Style="font-size: x-small" Text="Permite Anticipos de Nómina" />
                                     <br />
                                     <asp:Label ID="lblporcentaje" runat="server" Text="Porcentaje Anticipo" Visible="false"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtPorcentaje" runat="server" CssClass="textbox" Enabled="true" MaxLength="3" onChange="validaNum(this.value,0,100)" onkeypress="fieldNumber(this)" size="4" Visible="false" Width="30px"></asp:TextBox>
                                

                                </td>
                                <td style="text-align: center; padding: 10px; height: 6px;">
                                    <span style="font-size: x-small">
                                    <asp:CheckBox ID="chkPermite_anticipossubsidio" runat="server" AutoPostBack="true" OnCheckedChanged="chkPermite_anticipossubsidio_CheckedChanged" Style="font-size: x-small" Text="Permite Anticipos de Nómina de subsidio transporte " />
                                    <br />
                                         <asp:Label ID="lblporcentajesubsidio" runat="server" Text="Porcentaje Anticipo" Visible="false"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtPorcentajesubsidio" runat="server" CssClass="textbox" Enabled="true" MaxLength="3" onChange="validaNum(this.value,0,100)" onkeypress="fieldNumber(this)" size="4" Visible="false" Width="30px"></asp:TextBox>
                                    </span></td>
                                <td style="text-align: center; padding: 10px; height: 6px;">Periodicidad Anticipos
                                    <asp:CheckBoxList ID="checkBoxPeriodAnticipos" runat="server" RepeatColumns="3" RepeatDirection="Horizontal" Width="100%">
                                        <asp:ListItem onclick="ExclusiveCheckBoxList(this);" Text="Semanal" Value="3" />
                                        <asp:ListItem onclick="ExclusiveCheckBoxList(this);" Text="Bisemanal" Value="5" />
                                        <asp:ListItem onclick="ExclusiveCheckBoxList(this);" Text="Decadal" Value="4" />
                                        <asp:ListItem onclick="ExclusiveCheckBoxList(this);" Text="Mensual" Value="1" />
                                        <asp:ListItem onclick="ExclusiveCheckBoxList(this);" Text="Quincenal" Value="2" />
                                    </asp:CheckBoxList>
                                </td>
                     </tr>
               
                <tr>
                    <td style="text-align: Left; padding: 5px" colspan="4">
                        <strong>Otros Datos</strong>
                        <hr />
                    </td>
                    <tr>
                        <td style="text-align: Left; padding: 10px; width: 25%;">Oficina </td>
                        <td style="text-align: Left; padding: 10px">
                            <asp:DropDownList ID="ddlOficina" runat="server" AutoPostBack="true" CssClass="textbox" OnSelectedIndexChanged="ddlOficina_SelectedIndexChanged" Width="95%">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: Left; padding: 10px; width: 30%;">Ciudad de la Oficina </td>
                        <td style="text-align: Left; padding: 10px">
                            <asp:DropDownList ID="ddlCiudad" runat="server" CssClass="textbox" Enabled="false" Width="95%">
                            </asp:DropDownList>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: left; padding: 10px">Dirección de la Oficina </td>
                        <td colspan="3" style="text-align: left; padding: 10px">
                            <asp:TextBox ID="txtDireccionOficina" runat="server" Enabled="false" Width="100%" />
                        </td>
                    </tr>
                </tr>
            </table>
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
                            <asp:Label ID="lblMensaje" runat="server" Text="Información Guardada Correctamente"
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
