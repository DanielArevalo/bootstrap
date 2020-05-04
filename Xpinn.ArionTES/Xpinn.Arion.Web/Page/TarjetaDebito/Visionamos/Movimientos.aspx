<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Movimientos.aspx.cs" Inherits="Lista" Title=".: Xpinn - Movimientos Coopcentral:." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
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
                </td>
                <td style="text-align: left; width:140px">
                  <br />&nbsp;
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
                        <asp:GridView ID="gvLista" runat="server" Width="95%" AutoGenerateColumns="False" 
                            AllowPaging="False" PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                            style="font-size: x-small" onrowdatabound="gvLista_RowDataBound">
                            <Columns>    
                                <asp:BoundField DataField="idasociado" HeaderText="Id.Asociado">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="tarjeta" HeaderText="Tarjeta">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="cuenta_origen" HeaderText="Cuenta Origen">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="tipo_cuenta_origen" HeaderText="T.Cta Origen" />
                                <asp:BoundField DataField="cuenta_destino" HeaderText="Cuenta Destino">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="tipo_cuenta_destino" HeaderText="T.Cta Destino" />
                                <asp:BoundField DataField="fecha_transaccion" HeaderText="F.Transacción">
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:BoundField>      
                                <asp:BoundField DataField="hora_transaccion" HeaderText="H.Transacción">
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fecha_contable" HeaderText="F.Contable">
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:BoundField>      
                                <asp:BoundField DataField="transaccion" HeaderText="Transacción">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField> 
                                <asp:BoundField DataField="descripcion" HeaderText="Descripción Transacción" >
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>                 
                                <asp:BoundField DataField="escenario" HeaderText="Escenario" >
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>  
                                <asp:BoundField DataField="valor" HeaderText="Valor" DataFormatString="{0:n}" >                                
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>                           
                                <asp:BoundField DataField="comision_asociado" HeaderText="Comisión Asociado" DataFormatString="{0:n}" >                                
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>                           
                                <asp:BoundField DataField="utilidad_entidad" HeaderText="Utilidad Entidad" DataFormatString="{0:n}" >                                
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>  
                                <asp:BoundField DataField="secuencia" HeaderText="Secuencia" /> 
                                <asp:BoundField DataField="descripcion_terminal" HeaderText="Descripción Terminal" />  
                                <asp:BoundField DataField="codigo_terminal" HeaderText="Código Terminal" />    
                                <asp:BoundField DataField="tipo_terminal" HeaderText="Tipo Terminal" />                            
                                <asp:BoundField DataField="comision_asumida" HeaderText="Comisión Asumida" DataFormatString="{0:n}" >                                
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>         
                                <asp:BoundField DataField="ubicacion_terminal" HeaderText="Ubicación Terminal" />                                
                                <%--Campos de control para FINANCIAL--%>
                                <asp:BoundField DataField="cod_ofi" HeaderText="Cod.Ofi." />
                                <asp:BoundField DataField="cod_caja" HeaderText="Cod.Caja" />
                                <asp:BoundField DataField="num_tran" HeaderText="Num.Tran.Tar" /> 
                                <asp:BoundField DataField="valor_apl" HeaderText="Monto Apl." ItemStyle-HorizontalAlign="Right" />
                                <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                    <ItemTemplate>
                                        <asp:Image ID="imgAplicada" runat="server" ImageUrl="~/Images/aplicada.png" Width="16px" Visible="false" />
                                        <asp:Image ID="imgNoAplicada" runat="server" ImageUrl="~/Images/noaplicada.png" Width="16px" Visible="false" />
                                        <asp:Label ID="lblAplicada" runat="server" Visible="false" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="comision_apl" HeaderText="Vr.Com.Apl." ItemStyle-HorizontalAlign="Right" />                                
                                <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                    <ItemTemplate>
                                        <asp:Image ID="imgAplicadaCom" runat="server" ImageUrl="~/Images/aplicada.png" Width="16px" Visible="false" />
                                        <asp:ImageButton ID="imgNoAplicadaCom" runat="server" ImageUrl="~/Images/noaplicada.png" Width="16px" Visible="false" CommandName="Select" />
                                        <asp:Label ID="lblAplicadaCom" runat="server" Visible="false" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                </asp:TemplateField>
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
                            <asp:FileUpload ID="FileUploadMetas" runat="server" /><br /> 
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center" colspan="3">&#160;&#160;&#160;
                        <asp:Label ID="lblMensj" runat="server" Style="color: Red; font-size: x-small" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center" colspan="3">&#160;&#160;&#160;
                    </td>
                </tr>
                <tr>
                    <td styl
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
    
</asp:Content>
