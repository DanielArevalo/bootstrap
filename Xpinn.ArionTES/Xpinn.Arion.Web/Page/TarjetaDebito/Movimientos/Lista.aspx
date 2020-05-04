<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Movimientos Tarjeta :." %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="numero" TagPrefix="ucnumero" %>
<%@ Register Src="~/General/Controles/ctlProcesoContable.ascx" TagName="procesoContable" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <script type="text/javascript" src="../../../Scripts/gridviewScroll.min.js"></script>   
    <script type="text/javascript">
        $(document).ready(function () {
            gridviewScroll();
        });

        function gridviewScroll() {
            $('#<%=gvLista.ClientID%>').gridviewScroll({
                width: CalcularAncho(),
                height: 400,
                arrowsize: 30,
                varrowtopimg: "../../../Images/arrowvt.png",
                varrowbottomimg: "../../../Images/arrowvb.png",
                harrowleftimg: "../../../Images/arrowhl.png",
                harrowrightimg: "../../../Images/arrowhr.png"
            });
        }

        function CalcularAncho() {
            if (navigator.platform == 'Win32') {
                return screen.width - 330;
            }
            else {
                return 1000;
            }
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:Panel ID="panelGeneral" runat="server">
        <table style="width: 95%">
            <tr>
                <td style="text-align: left;">
                    Fecha<br />
                    <asp:TextBox ID="txtFecha" runat="server" CssClass="textbox" 
                        MaxLength="10" Width="90px" AutoPostBack="True"></asp:TextBox>
                    <asp:CalendarExtender ID="ceFechaAperturan" runat="server" 
                        DaysModeTitleFormat="dd/MM/yyyy" Enabled="True" Format="dd/MM/yyyy" 
                        TargetControlID="txtFecha" TodaysDateFormat="dd/MM/yyyy">
                    </asp:CalendarExtender>
                </td>
                <td>
                    &nbsp;&nbsp;
                </td>
                <td style="text-align: left;">
                    Fecha Aplicación<br />
                    <asp:TextBox ID="txtFechaAplica" runat="server" CssClass="textbox" 
                        MaxLength="10" Width="90px" AutoPostBack="True"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" 
                        DaysModeTitleFormat="dd/MM/yyyy" Enabled="True" Format="dd/MM/yyyy" 
                        TargetControlID="txtFechaAplica" TodaysDateFormat="dd/MM/yyyy">
                    </asp:CalendarExtender>
                </td><td style="text-align: left; width:80px">
                  <asp:Label ID="lblNroCuenta" Text="Nro.Cuenta" runat="server" Visible="false"/>
                  <br />
                  <asp:TextBox ID="txtNroCuenta" runat="server" CssClass="textbox" Width="90%" Visible="false" />
                </td>
                <td style="text-align: left; width:80px">
                  Convenio<br />
                  <asp:TextBox ID="txtConvenio" runat="server" CssClass="textbox" Width="90%" ReadOnly="true" />
                </td>
                <td style="text-align: left; width:80px">
                  Host<br />
                  <asp:TextBox ID="txtIpAppliance" runat="server" CssClass="textbox" Width="90%" ReadOnly="true" />
                </td>
                <td style="text-align: left; width:140px">
                  <br />&nbsp;
                  <asp:CheckBox ID="chNokMonto" runat="server" Text="Nok Monto" />&nbsp;
                </td>
                <td style="text-align: left; width:140px">
                  <br />&nbsp;
                  <asp:CheckBox ID="chNokComision" runat="server" Text="Nok Comisión" />&nbsp;
                </td>
                <td style="text-align: left; width:80px">
                </td>
            </tr>
        </table>
            
        <hr style="width: 100%" />

        <asp:Panel ID="panelGrilla" runat="server">
            <table style="width: 90%">
                <tr>
                    <td>
                        <asp:Label ID="lblError" runat="server" Width="95%" ForeColor="Red" />
                        <br />
                        <asp:GridView ID="gvLista" runat="server" Width="95%" AutoGenerateColumns="False" DataKeyNames="num_tran_verifica"
                            AllowPaging="False" PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                            style="font-size: x-small" onrowdatabound="gvLista_RowDataBound" OnSelectedIndexChanged="gvLista_SelectedIndexChanged">
                            <Columns>     
                                <asp:BoundField DataField="fecha" HeaderText="Fecha">
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:BoundField>                       
                                <asp:BoundField DataField="hora" HeaderText="Hora">
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="documento" HeaderText="Documento">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nrocuenta" HeaderText="NroCuenta">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="tarjeta" HeaderText="Tarjeta">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="tipotransaccion" HeaderText="Tipo Transacción">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>                            
                                <asp:BoundField DataField="descripcion" HeaderText="Descripción" >
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="monto" HeaderText="Monto" DataFormatString="{0:n}" >                                
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="comision" HeaderText="Comisión" DataFormatString="{0:n}" >                                
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>                            
                                <asp:BoundField DataField="lugar" HeaderText="Lugar" >
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>                            
                                <asp:BoundField DataField="operacion" HeaderText="Operación" />                            
                                <asp:BoundField DataField="red" HeaderText="Red" />
                                <asp:BoundField HeaderText="Datafono" />
                                <asp:BoundField DataField="num_tran_apl" HeaderText="Num.Tran.Apl." />
                                <asp:BoundField DataField="valor_apl" HeaderText="Monto Apl." />
                                <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                    <ItemTemplate>
                                        <asp:Image ID="imgAplicada" runat="server" ImageUrl="~/Images/aplicada.png" Width="16px" Visible="false" />
                                        <asp:Image ID="imgNoAplicada" runat="server" ImageUrl="~/Images/noaplicada.png" Width="16px" Visible="false" />
                                        <asp:Label ID="lblAplicada" runat="server" Visible="false" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="comision_apl" HeaderText="Vr.Com.Apl." />                                
                                <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                    <ItemTemplate>
                                        <asp:Image ID="imgAplicadaCom" runat="server" ImageUrl="~/Images/aplicada.png" Width="16px" Visible="false" />
                                        <asp:ImageButton ID="imgNoAplicadaCom" runat="server" ImageUrl="~/Images/noaplicada.png" Width="16px" Visible="false" CommandName="Select" />
                                        <asp:Label ID="lblAplicadaCom" runat="server" Visible="false" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="cod_ope_apl" HeaderText="Cod.Ope" />
                                <asp:BoundField DataField="num_comp_apl" HeaderText="Num.Comp." />
                                <asp:BoundField DataField="tipo_comp_apl" HeaderText="T.Comp." />
                                <asp:BoundField DataField="saldo_total" HeaderText="Saldo Cuenta" DataFormatString="{0:n}" >                                
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="tipocuenta" HeaderText="T.Cta" />
                                <asp:BoundField DataField="num_tran_verifica" HeaderText="Num.Tran.Tar." />
                                <asp:BoundField DataField="cod_ofi" HeaderText="Ofi." />
                                <asp:BoundField DataField="cuenta_porcobrar" HeaderText="CxC" DataFormatString="{0:n}" >                                
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fecha_corte" HeaderText="F.Corte" />
                            </Columns>
                            <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                            <PagerStyle CssClass="gridPager"></PagerStyle>
                        </asp:GridView>
                        <asp:Label ID="lblTotalRegs" runat="server" Visible="False" /><br /><br /><br />
                        <asp:TextBox ID="txtRespuesta" runat="server" Height="100px" 
                            TextMode="MultiLine" Width="95%" BorderStyle="None" Font-Bold="True" 
                            Font-Italic="True" Font-Size="Small"></asp:TextBox>
                        <asp:TextBox ID="txtpRequestXmlString" runat="server" Height="100px" visible="false"
                            TextMode="MultiLine" Width="95%" BorderStyle="None" Font-Bold="True" 
                            Font-Italic="True" Font-Size="Small"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="panelCarga" runat="server">
            <table width="100%">
                <tr>
                    <td style="text-align: center; width: 100%" class="gridHeader" colspan="3">
                        <strong>Carga de Archivo de Movimientos</strong><br />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        &nbsp;
                    </td>
                    <td style="text-align: left">
                        &nbsp;
                    </td>
                    <td style="text-align: left">  
                        &nbsp;                     
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; width: 100%" colspan="3">
                            <asp:FileUpload ID="FileUploadMetas" runat="server" /> 
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center" colspan="3">
                        <asp:Label ID="lblMensj" runat="server" Style="color: Red; font-size: x-small" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center" colspan="3">
                        <asp:Button ID="btnAceptarCarga" runat="server" Height="30px" Text="Cargar" CssClass="btn8" 
                            Width="150px" OnClick="btnAceptarCarga_Click" />
                        &#160;&#160;&#160;
                        <asp:Button ID="btnCerrarCarga" runat="server" Height="30px" Text="Cancelar" CssClass="btn8" 
                            Width="150px" OnClick="btnCerrarCarga_Click" />
                    </td>
                </tr>
            </table>       
        </asp:Panel>
        <br />
        <br />
    </asp:Panel>
    <uc4:mensajeGrabar ID="ctlMensaje" runat="server"/>
    <asp:Panel ID="panelProceso" runat="server" Width="100%">
        <uc2:procesoContable ID="ctlproceso" runat="server" />  
    </asp:Panel> 
    
    <asp:ModalPopupExtender ID="mpeNuevoAjuste" runat="server" PopupControlID="PanelAjuste"
        TargetControlID="hfAjuste" BackgroundCssClass="backgroundColor">
    </asp:ModalPopupExtender>
    <asp:Panel ID="PanelAjuste" runat="server">
        <asp:HiddenField ID="hfAjuste" runat="server" />
        <asp:UpdatePanel ID="UpdatePanelAjuste" runat="server">
            <Triggers>
                <asp:PostBackTrigger ControlID="btnCerrarAjuste" />                
                <asp:PostBackTrigger ControlID="btGrabarAjuste" />
            </Triggers>
            <ContentTemplate>
                <table border="0" cellpadding="0" cellspacing="0" style="border: medium groove #0000FF; width: 280px; background-color: #FFFFFF; height: 250px; margin-right: 5px; margin-left: 5px">
                    <tr>
                        <td class="tdI" colspan="3" style="text-align: left">
                            <div class="gridHeader" style="height: 22px; width: 100%; text-align: center;">
                                AJUSTAR TRANSACCION DE TARJETA
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            Núm.Transacción<br />
                            <asp:TextBox ID="txtNumTranTarjeta" runat="server" CssClass="textbox" Enabled="false" Width="100px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            Tipo Transacción<br />
                            <asp:DropDownList ID="ddlTipoTran" runat="server" CssClass="textbox" Enabled="true" Width="170px" OnTextChanged="ddlTipoTran_TextChanged" AutoPostBack="true"  />
                            <asp:TextBox ID="txtTipoCuenta" runat="server" CssClass="textbox" Enabled="false" Width="30px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            Valor Actual<br />
                            <asp:TextBox ID="txtValor" runat="server" CssClass="textbox" Enabled="false" Width="100px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            Valor Nuevo<br />
                            <asp:TextBox ID="txtValorNuevo" runat="server" CssClass="textbox" Enabled="true" Width="100px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <asp:Button ID="btnCerrarAjuste" runat="server" CssClass="btn8" OnClick="btnCerrarAjuste_Click"
                                 Text="Regresar" Visible="True" Height="22px" />
                            &#160;&#160;&#160;
                            <asp:Button ID="btGrabarAjuste" runat="server" CssClass="btn8" OnClick="btGrabarAjuste_Click"
                                 Text="Realizar Ajuste" Visible="True" Height="22px"/>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>

</asp:Content>
