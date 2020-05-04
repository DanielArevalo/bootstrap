<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/ctlProcesoContable.ascx" TagName="procesoContable" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    
    <script type="text/javascript">
     function convert_decimal(valor) {
            var valor_formato = "";
            for (var i = 3; i < valor.length; i += 3) {
                var subvalor = valor.substring((valor.length - (i - 3)), (valor.length - i));
                if (valor.length > (i + 3)) {
                    valor_formato = "." + subvalor + valor_formato;
                }
                else {
                    valor_formato = valor.substring(0, (valor.length - i)) + "." + subvalor + valor_formato;
                }
            }
            if (valor.length > 3) {
                return valor_formato;
            }
            else {
                return valor;
            }
        }
     $(window).load(function ()
        {
            $('input:checkbox[id^="chkseleccion"]:checked').each(function () {
                var dato = $(this).parent().parent()[0].children[12].childNodes[1];
                var valor_actual_decimal = convert_decimal(dato.value);
                $(this).parent().parent()[0].children[12].childNodes[1].value =valor_actual_decimal;
            });
           
            var total_actual_decimal = convert_decimal($("#txtTotalAplicado").val());
            $("#txtTotalAplicado").val(total_actual_decimal);
        });
     $(document).ready(function () {
           
                $("#txtTotalPago").focus(function () {
                    var valor = $(this).val();
                    if(valor.includes("."))
                    {
                        valor = valor.replace(/\./g, '');;
                        $(this).val(valor);
                    }
                });

                $("#txtTotalPago").blur(function ()
                {
                    var result = 0;
                    var valor_actual = $(this).val().replace(/\./g, '');
                    if (valor_actual=="")
                    {
                        valor_actual = "0";
                        $(this).val(valor_actual);
                    }

                    $('input:checkbox[id^="chkseleccion"]:checked').each(function () {
                        var dato = $(this).parent().parent()[0].children[12].childNodes[1];
                        result = result + parseFloat(dato.value.replace(/\./g, ''));
                    });

                    var valor_actual_decimal = convert_decimal(""+ parseFloat(valor_actual)+"");
                    $(this).val(valor_actual_decimal);
                    var result_decimal = convert_decimal(""+result+"");
                    $("#txtTotalAplicado").val(result_decimal);
                });

                $("#chkseleccion").click(function () {
                    var dato = $(this).parent().parent()[0].children[12].childNodes[1];
                    var total_apli = $("#txtTotalAplicado").val();
                    var result = 0;
                    if ($(this).is(":checked")) {
                        dato.disabled = false;
                        result = parseFloat(total_apli.replace(/\./g, '')) + parseFloat(dato.value.replace(/\./g, ''));
                        var result_decimal = convert_decimal("" + result + "");
                        $("#txtTotalAplicado").val(result_decimal);
                    }
                    else
                    {
                        dato.disabled = true;
                        result = parseFloat(total_apli.replace(/\./g, '')) - parseFloat(dato.value.replace(/\./g, ''));
                        var result_decimal = convert_decimal("" + result + "");
                        $("#txtTotalAplicado").val(result_decimal);
                    }
              

                });
            });
    </script>
     <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:Panel ID="panelGeneral" runat="server">
     <asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex="0">
    <asp:View ID="vwDAuto" runat="server">
            <asp:Panel ID="pConsulta" runat="server">
                <table id="tbCriterios" border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="tdI">
                            <asp:Panel ID="Panel1" runat="server">
                                <table style="width:100%;">
                                    <tr>
                                        <td style="text-align:left" colspan="2">
                                         <label>Fecha Aplicacion</label>   
                                        </td>
                                        <td style="text-align:left">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="text-align:left">
                                            <ucFecha:fecha ID="txtFecha" runat="server" CssClass="textbox" />
                                       </td>
                                    </tr>
                                </table>
                           
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pDatos" runat="server">                                       
                    <table style="width: 100%"> 
                        <tr>
                            <td>
                                <asp:GridView ID="gvLista" runat="server" Width="100%"  GridLines="Horizontal" AutoGenerateColumns="False"
                                    AllowPaging="False"   Style="font-size: x-small"
                                    PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem"  DataKeyNames="" >                                    <Columns>
                                       <asp:BoundField DataField="numero_radicacion" HeaderText="Num. Radicacion" >
                                            <ItemStyle HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="nom_linea" HeaderText="Linea" >
                                            <ItemStyle HorizontalAlign="center" />
                                        </asp:BoundField>             
                                        <asp:BoundField DataField="fecha_aprobacion" HeaderText="F. Aprobacion" DataFormatString="{0:d}" >
                                            <ItemStyle HorizontalAlign="center" />
                                        </asp:BoundField>
                                        
                                        <asp:BoundField DataField="identificacion" HeaderText="Identificación" >
                                            <ItemStyle HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="nombres" HeaderText="Nombres" >
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="saldo_capital" HeaderText="Saldo Cap" DataFormatString="{0:n0}">
                                            <ItemStyle HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="valor_cuota" HeaderText="Vr. Couta" DataFormatString="{0:n0}">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_proximo_pago" HeaderText="F. Prox. Pago" DataFormatString="{0:d}">
                                            <ItemStyle HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_ultimo_pago" HeaderText="F. Ult. Pago" DataFormatString="{0:d}">
                                            <ItemStyle HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="numero_cuenta" HeaderText="Num. Cuenta" DataFormatString="{0:n0}">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="saldo_total" HeaderText="Saldo Total" DataFormatString="{0:n0}">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="saldo_disponible" HeaderText="Saldo Disponible" DataFormatString="{0:n0}">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Valor a Pagar">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtTotalPago" ClientIDMode="Static" runat="server"  Visible="true" Width="80px"/>
                                                <asp:FilteredTextBoxExtender ID="txtTotalPagoE_FilteredTextBoxExtender"
                                                                runat="server" Enabled="True" FilterType="Numbers,Custom" ValidChars="." TargetControlID="txtTotalPago">
                                                            </asp:FilteredTextBoxExtender>
                                                     <asp:HiddenField ID="cod_cliente" runat="server"/>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderStyle-CssClass="gridIco"  ItemStyle-CssClass="gridIco" HeaderText="Seleccionar">
                                            <ItemTemplate>
                                             <asp:CheckBox ID="chkseleccion" ClientIDMode="Static" runat="server" Checked="true"/>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                            <ItemStyle CssClass="gridIco"></ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                                    <PagerStyle CssClass="gridPager"></PagerStyle>
                                    <RowStyle CssClass="gridItem"></RowStyle>
                                </asp:GridView>
                                <table>
                                    <tr>
                                 <td style="width:200px; text-align:left;">
                                <asp:Label ID="lblTotalRegs" runat="server" Visible="False"/>
                                </td> <td style="width:300px; text-align:left;">
                                            <asp:Label ID="lblTotalAplicar" runat="server" Text="  Valor Total a Aplicar" Visible="False"/>
                                 <asp:TextBox ID="txtTotalAplicado" ClientIDMode="Static" runat="server" Enabled="false" Visible="False" Width="80px" />
                                 <td>
                                    </tr>
                                        </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>

    </asp:View>
    </asp:MultiView>
     </asp:Panel> 

    <uc4:mensajeGrabar ID="ctlMensaje" runat="server"/>
    <asp:Panel ID="panelProceso" runat="server" Width="100%">
        <uc2:procesoContable ID="ctlproceso" runat="server" />  
    </asp:Panel> 
</asp:Content>

