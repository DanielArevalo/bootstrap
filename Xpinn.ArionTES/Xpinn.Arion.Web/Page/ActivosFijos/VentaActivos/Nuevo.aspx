<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Activos Fijos :." %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="Fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register Src="../../../General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="../../../General/Controles/decimalesGridRow.ascx" TagName="decimalesGrid" TagPrefix="uc1" %>
<%@ Register Src="../../../General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Src="../../../General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <script type="text/javascript">
        function ActiveTabChanged(sender, e) {
        }

        var HighlightAnimations = {};

        function Highlight(el) {
            if (HighlightAnimations[el.uniqueID] == null) {
                HighlightAnimations[el.uniqueID] = Sys.Extended.UI.Animation.createAnimation({
                    AnimationName: "color",
                    duration: 0.5,
                    property: "style",
                    propertyKey: "backgroundColor",
                    startValue: "#FFFF90",
                    endValue: "#FFFFFF"
                }, el);
            }
            HighlightAnimations[el.uniqueID].stop();
            HighlightAnimations[el.uniqueID].play();
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:MultiView ID="mvActivosFijos" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <asp:Panel ID="pConsulta" runat="server" Width="100%">
                <table id="tbCriterios" border="0" cellpadding="1" cellspacing="0" >
                    <tr>
                        <td style="text-align:left" colspan="5">
                            <strong>Datos Principales</strong>&nbsp;&nbsp;
                            <asp:Label ID="lblConsecutivo" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:left">
                            Código<br/>
                            <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" />
                        </td>
                        <td style="text-align:left">
                            Estado<br/>
                            <asp:Label ID="lblEstado" runat="server" Visible="False"></asp:Label>
                            <asp:TextBox ID="txtEstado" runat="server" CssClass="textbox" Enabled="False" 
                                Width="100px" />
                        </td>
                        <td colspan="3" style="text-align:left">
                            Serial<br/>
                            <asp:TextBox ID="txtSerial" runat="server" CssClass="textbox" Width="205px" 
                                Enabled="False" />
                        </td>
                        <td class="tdD">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align:left" colspan="2">
                             Nombre<br />
                             <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Width="292px" Enabled="False" />                             
                        </td>
                        <td style="text-align:left" colspan="2">
                            Clase<br />
                            <asp:DropDownList ID="ddlClase" runat="server" CssClass="textbox" 
                                Width="190px" AppendDataBoundItems="True" Enabled="False" >
                                <asp:ListItem Value=""></asp:ListItem>
                            </asp:DropDownList>                            
                        </td>
                        <td style="text-align:left">
                            Tipo<br />
                            <asp:DropDownList ID="ddlTipo" runat="server" CssClass="textbox" Width="165px" AppendDataBoundItems="True" Enabled="False" >
                                <asp:ListItem Value="" ></asp:ListItem>
                            </asp:DropDownList>                            
                        </td>
                        <td class="tdD">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align:left" colspan="2">
                            Ubicación<br />
                            <asp:DropDownList ID="ddlUbicacion" runat="server" CssClass="textbox" 
                                Width="300px" AppendDataBoundItems="True" Enabled="False" >
                                <asp:ListItem Value=""></asp:ListItem>
                            </asp:DropDownList>                            
                        </td>
                        <td style="text-align:left" colspan="2">
                            Centro de Costo<br />
                            <asp:DropDownList ID="ddlCentroCosto" runat="server" CssClass="textbox" 
                                Width="190px" AppendDataBoundItems="True" Enabled="False" >
                                <asp:ListItem Value=""></asp:ListItem>
                            </asp:DropDownList>                            
                        </td>
                        <td style="text-align:left">
                            Oficina<br />
                            <asp:DropDownList ID="ddlOficina" runat="server" CssClass="textbox" 
                                Width="165px" AppendDataBoundItems="True" Enabled="False" >
                                <asp:ListItem Value=""></asp:ListItem>
                            </asp:DropDownList>                            
                        </td>
                        <td class="tdD">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align:left" colspan="2">
                            Encargado<br />
                            <asp:DropDownList ID="ddlEncargado" runat="server" CssClass="textbox" 
                                Width="300px" AppendDataBoundItems="True" Enabled="False" >
                                <asp:ListItem Value=""></asp:ListItem>
                            </asp:DropDownList>                            
                        </td>
                        <td style="text-align:left">
                            Vida Util (Años)<br />
                            <asp:TextBox ID="txtVidaUtil" runat="server" CssClass="textbox" 
                                Width="80px" Enabled="False"  />                            
                        </td>
                        <td style="text-align:left">
                            Valor del Avalúo<br />
                            <uc1:decimales ID="txtValorAvaluo" runat="server" 
                                style="font-size:xx-small; text-align:right" TipoLetra="XX-Small" 
                                Habilitado="False" Enabled="False" Width_="80" />
                        </td>
                        <td style="text-align:left">
                            Valor de Salvamento<br /> 
                            <uc1:decimales ID="txtValorSalvamento" runat="server" 
                                style="font-size:xx-small; text-align:right" TipoLetra="XX-Small" 
                                Habilitado="False" Enabled="False" Width_="80" />
                        </td>
                        <td class="tdD">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align:left">
                            F.Ult.Depreciación<br />
                            <asp:TextBox ID="txtFecUltDep" runat="server" CssClass="textbox" Width="120px" Enabled="False"  />
                        </td>
                        <td style="text-align:left" colspan="2">
                            Depreciación Acumulada<br />
                            <uc1:decimales ID="txtDepAcum" runat="server" Enabled="False" 
                                Habilitado="False" style="font-size:xx-small; text-align:right" 
                                TipoLetra="XX-Small" Width_="80" />
                        </td>
                        <td style="text-align:left">
                            Saldo por Depreciar<br />
                            <uc1:decimales ID="txtSaldoDep" runat="server" Enabled="False" 
                                Habilitado="False" style="font-size:xx-small; text-align:right" 
                                TipoLetra="XX-Small" Width_="80" />
                        </td>
                        <td class="tdD">
                            &nbsp;</td>
                    </tr>
                </table>
                <table id="Table1" border="0" cellpadding="1" cellspacing="0" width="50%">
                    <tr>
                        <td style="text-align:left" colspan="2">
                            <strong>Datos de Compra</strong>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:left">
                            Número de Factura<br/>
                            <asp:TextBox ID="txtFactura" runat="server" CssClass="textbox" Width="120px" Enabled="False"  />
                        </td>
                        <td style="text-align:left">
                            Proveedor<br/>
                            <asp:DropDownList ID="ddlProveedor" runat="server" CssClass="textbox" 
                                Width="300px" AppendDataBoundItems="True" Enabled="False" >
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 49px; text-align:left">
                            Valor de la Compra<br /> 
                            <uc1:decimalesGrid ID="txtValorCompra" runat="server" 
                                style="font-size:xx-small; text-align:right" TipoLetra="XX-Small" 
                                Habilitado="True" Enabled="False" Width_="80" Requerido="True" Validador="vgGuardar" />
                        </td>
                        <td style="width: 206px; text-align:left">
                            Fecha de Compra<br />
                            <uc1:fecha ID="txtFechaCompra" runat="server" 
                                style="font-size:xx-small; text-align:right" TipoLetra="XX-Small" 
                                Habilitado="False" Enabled="False" Width_="80" />                            
                        </td>
                    </tr>            
                    <tr>
                        <td colspan="2" style="text-align:left">
                            Observaciones<br />
                            <asp:TextBox ID="txtObservacion" TextMode="MultiLine" runat="server" Width="100%" Enabled="False" ></asp:TextBox>
                        </td>
                    </tr>
                </table>                
                <table id="Table2" border="0" cellpadding="1" cellspacing="0" width="50%">
                    <tr>
                        <td style="text-align:left" colspan="3">
                            <strong>Datos de la Venta</strong>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:left">
                             Fecha Venta<br />
                             <uc1:fecha ID="txtFechaVenta" runat="server" CssClass="textbox" Enabled="True" Requerido="true" />                             
                        </td>
                        <td style="text-align:left">
                             Valor Venta<br />                             
                             <uc1:decimales ID="txtValorVenta" runat="server" 
                                style="font-size:xx-small; text-align:right" TipoLetra="XX-Small" 
                                Habilitado="True" Enabled="True" Width_="80" />                            
                        </td>
                        <td style="text-align:left">
                             Comprador<br />                             
                            <asp:DropDownList ID="ddlComprador" runat="server" CssClass="textbox" 
                                Width="442px" AppendDataBoundItems="True" Enabled="True" >
                                <asp:ListItem Value="0">Seleccione un Item</asp:ListItem>
                            </asp:DropDownList>                                                           
                             <asp:RequiredFieldValidator ID="rfvComprador" runat="server" 
                                 ErrorMessage="Ingrese el comprador" ControlToValidate="ddlComprador" 
                                 ValidationGroup="vgGuardar" Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>                         
                        </td>
                    </tr>
                </table>            
            </asp:Panel>
        </asp:View>
        <asp:View ID="mvFinal" runat="server">
            <asp:Panel id="PanelFinal" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br /><br /><br /><br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <asp:Label ID="lblMensaje" runat="server" 
                                Text="Datos Grabados Correctamente" style="color: #FF3300"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br /><br />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>

    <uc1:mensajeGrabar ID="ctlMensaje" runat="server"/>

</asp:Content>