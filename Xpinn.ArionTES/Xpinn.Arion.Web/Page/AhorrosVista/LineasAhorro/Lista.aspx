<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <script type="text/javascript" src="../../../Scripts/gridviewScroll.min.js"></script>   
    <script type="text/javascript">

        $(document).ready(function () {
            gridviewScroll();
        });

        function gridviewScroll() {
            $('#<%=gvLista.ClientID%>').gridviewScroll({
                width: 1200,
                height: 500,
                arrowsize: 30,
                varrowtopimg: "../../../Images/arrowvt.png",
                varrowbottomimg: "../../../Images/arrowvb.png",
                harrowleftimg: "../../../Images/arrowhl.png",
                harrowrightimg: "../../../Images/arrowhr.png"
            });
        }        
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>  

    <asp:Panel ID="pConsulta" runat="server">            
        <table cellpadding="0" cellspacing="0" style="width: 70%">
           
            <tr>
                <td style="text-align: left; font-size: x-small;" colspan="4">
                    <strong>Criterios de Búsqueda:</strong></td>
            </tr>
            <tr>
                <td style="text-align: left;">
                    Código<br />
                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="119px"></asp:TextBox>
                </td>
                <td style="text-align: left;">
                    Descripción<br />
                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" Width="300px"></asp:TextBox>
                </td>
                <td colspan="2" style="height: 15px; text-align: left;">
                </td>
            </tr>
            <tr>
                <td class="tdI" style="text-align: left; width: 205px;">
                </td>
                <td class="tdI" style="text-align: left">
                </td>
                <td class="tdI" style="text-align: left">
                </td>
                <td class="tdI" style="text-align: left">
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table style="width:100%">
        <tr>
            <td>
                <asp:Button ID="btnExportar" runat="server" CssClass="btn8" 
                    onclick="btnExportar_Click" Text="Exportar a Excel" />
            </td>
        </tr>
        <tr>  
            <td>
                <asp:GridView ID="gvLista" runat="server" Width="80%" AutoGenerateColumns="False"
                    AllowPaging="False" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                    OnPageIndexChanging="gvLista_PageIndexChanging" OnRowDeleting="gvLista_RowDeleting"
                    OnRowEditing="gvLista_RowEditing" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                    OnRowDataBound="gvLista_RowDataBound" DataKeyNames="cod_linea_ahorro" 
                    style="font-size: xx-small">
                    <Columns>                   
                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                                    ToolTip="Detalle" Width="16px" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                                    ToolTip="Editar" Width="16px" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg"
                                    ToolTip="Eliminar" Width="16px" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="cod_linea_ahorro" HeaderText="Cod.Línea">
                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="descripcion" HeaderText="Descripción" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="estado" HeaderText="Estado" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cod_moneda" HeaderText="Moneda" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="prioridad" HeaderText="Prioridad" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="valor_apertura" HeaderText="Vr. Apertura"  DataFormatString="{0:n2}" >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="saldo_minimo" HeaderText="Saldo <br/> Mínimo" HtmlEncode="False"  DataFormatString="{0:n2}">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="movimiento_minimo" HeaderText="Movimiento Mínimo"  DataFormatString="{0:n2}">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="maximo_retiro_diario" HeaderText="Máximo Retiro Diario"  DataFormatString="{0:n2}">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="retiro_min_cheque" HeaderText="Retiro Min.Cheque"  DataFormatString="{0:n2}">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="retiro_max_efectivo" HeaderText="Retiro Max.Efectivo"  DataFormatString="{0:n2}">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="requiere_libreta" HeaderText="Requiere Libreta" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="valor_libreta" HeaderText="Vr. Libreta" DataFormatString="{0:n2}" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="num_desprendibles_lib" HeaderText="Num.Desprendibles" DataFormatString="{0:n0}">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>                                                                   
                        <asp:BoundField DataField="cobra_primera_libreta" HeaderText="Cobra Primera Libreta" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cobra_perdida_libreta" HeaderText="Cobra Perdida Libreta" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="canje_automatico" HeaderText="Canje Automático" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="dias_canje" HeaderText="Días Canje" DataFormatString="{0:n0}" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="inactivacion_automatica" HeaderText="Inactivación Automática" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="dias_inactiva" HeaderText="Días Inactiva" DataFormatString="{0:n0}" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cobro_cierre" HeaderText="Cobra Cierre" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cierre_dias" HeaderText="Días Cierre" DataFormatString="{0:n0}">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cierre_valor" HeaderText="Vr.por Cierre" DataFormatString="{0:c2}">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="tipo_saldo_int" HeaderText="Tipo Saldo Base Int." >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cod_periodicidad_int" HeaderText="Periodicidad <br/> Interes"  HtmlEncode="False" >
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="interes_por_cuenta" HeaderText="Intéres x Cuenta">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>                                                                                                                        
                        <asp:BoundField DataField="dias_gracia" HeaderText="Días Gracia" DataFormatString="{0:n0}" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>                        
                        <asp:BoundField DataField="forma_tasa" HeaderText="Forma Tasa" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="tipo_historico" HeaderText="Tipo Histórico" >
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="desviacion" HeaderText="Desviación"  DataFormatString="{0:n0}">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="tipo_tasa" HeaderText="Tipo Tasa">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="tasa" HeaderText="Tasa" DataFormatString="{0:n2}">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>      
                          <asp:BoundField DataField="debito_automatico" HeaderText="Deb.Automatico">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>                  
                        <asp:BoundField DataField="fechacreacion" HeaderText="Fecha Crea."  DataFormatString="{0:d}" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="usuariocreacion" HeaderText="Usuario" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                    <RowStyle CssClass="gridItem" />
                </asp:GridView>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado." Visible="False" />
            </td>
        </tr>        
    </table>
</asp:Content>
