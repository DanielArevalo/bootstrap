<%@ Page Title=".: Estado Cuenta :." Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" EnableEventValidation="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/imprimir.ascx" TagName="imprimir" TagPrefix="ucImprimir" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endReq);
        function endReq(sender, args) {
            $("#divTabs").tabs();
        }
    </script>

    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            $(".filtro tr:has(td)").each(function () {
                var t = $(this).text().toLowerCase();
                $("<td class='indexColumn'></td>")
                 .hide().text(t).appendTo(this);
            });
            $("#txtBuscar").keyup(function () {
                var s = $(this).val().toLowerCase().split(" ");
                $(".filtro tr:hidden").show();
                $.each(s, function () {
                    $(".filtro tr:visible .indexColumn:not(:contains('" + this + "'))").parent().hide();
                });
            });
        });

    </script>
    <style>
        .btn-send{
            padding: 10px;margin: 10px;
            height: unset !important;
            cursor: pointer;
        }

        .btn-send:hover {
            border:none;            
            background-color:cadetblue;
        }       
    </style>

    <asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex="0">
        <asp:View ID="ViewListado" runat="server">
            <asp:Panel runat="server" HorizontalAlign="Center">
                <table border="0" cellpadding="0" cellspacing="0" width="70%" style="text-align: center; margin-top: 2pc;">
                    <tr>
                        <td>
                            <asp:Label ID="lbIdentificacion" runat="server" Text="Identificación" ></asp:Label><br />
                        </td>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="Fecha último envío" ></asp:Label><br />
                        </td>
                        <td>
                            <asp:Label ID="lblfechafinal" runat="server" Text="Fecha de generación"></asp:Label><br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtIdentificacion" CssClass="textbox" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <ucFecha:fecha ID="txtFechaGeneracion" runat="server" style="text-align: center" />
                        </td>
                        <td>
                            <ucFecha:fecha ID="txtFechaCorte" runat="server" style="text-align: center" />
                        </td>                        
                    </tr>                    
                </table>
                <table style="width:100%">
                    <tr>
                        <td>
                            <asp:Button ID="btnEnviar" CssClass="btn8 btn-send" runat="server" Text="Enviar Correos" Height="26px" OnClick="btnEnviar_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <table style="width: 100%">
                <tr>
                    <td style="text-align: left">
                        <asp:Panel ID="panelLista" runat="server" ScrollBars="Vertical">
                            <div style="max-height: 630px; text-align: center">
                                <strong>Listado de personas para el envio</strong><br />
                                <br />
                                <asp:Label ID="lblBuscar" runat="server" Text="Buscar:" />
                                <input type="text" id="txtBuscar" name="txtBuscar" /><br />
                                <asp:GridView ID="gvLista" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
                                    HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" class="filtro" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                                    DataKeyNames="Codigo" Style="font-size: x-small;" ShowHeader="true">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Seleccionar">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkEncabezado" runat="server" AutoPostBack="true" OnCheckedChanged="chkEncabezado_CheckedChanged" Checked="true" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkEnviar" runat="server" AutoPostBack="true" Checked="true" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco" HeaderText="Ver">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg" ToolTip="Detalle" Width="16px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Codigo" HeaderText="Código" HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="identificacion" HeaderText="Identificación" HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="nombre_titular" HeaderText="Nombres" />
                                        <asp:BoundField DataField="email" HeaderText="E-Mail" />
                                        <asp:BoundField DataField="fecha_final_vigencia" HeaderText="Fecha corte" DataFormatString="{0:d}" />
                                        <asp:BoundField DataField="fecha_final" HeaderText="Último envio" DataFormatString="{0:d}" />
                                    </Columns>
                                    <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                                    <PagerStyle CssClass="gridPager"></PagerStyle>
                                    <RowStyle CssClass="gridItem"></RowStyle>
                                </asp:GridView>
                            </div>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        <asp:Label ID="lblTotalRegs" runat="server" />
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="ViewErrores" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <asp:Panel ID="pErrores" runat="server" Visible="true">
                            <asp:Panel ID="pEncBusqueda" runat="server" CssClass="collapsePanelHeader" Height="30px" Visible="true">
                                <div style="padding: 5px; cursor: pointer; vertical-align: middle;">
                                    <div style="float: left; margin-left: 5px; color: #0066FF; font-size: small">
                                        <asp:Label ID="lblMostrarDetalles" runat="server" />
                                        <asp:ImageButton ID="imgExpand" runat="server" ImageUrl="~/Images/expand.jpg" />
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pErroresG" runat="server" Width="100%" Visible="true">
                                <div style="border-style: none; border-width: medium; background-color: #f5f5f5">
                                    <asp:GridView ID="gvErrores" runat="server" Width="80%" ShowHeaderWhenEmpty="false" Visible="true" HorizontalAlign="Center" OnPageIndexChanging="gvErrores_PageIndexChanging"
                                        AutoGenerateColumns="False" AllowPaging="true" PageSize="50" SelectedRowStyle-Font-Size="XX-Small" Style="font-size: x-small; margin-bottom: 0px;">
                                        <Columns>
                                            <asp:BoundField DataField="numero_registro" HeaderText="No." ItemStyle-Width="50"
                                                ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="datos" HeaderText="Datos" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="error" HeaderText="Error" ItemStyle-Width="200" ItemStyle-HorizontalAlign="Left" />
                                        </Columns>
                                        <HeaderStyle CssClass="gridHeader" />
                                        <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                        <RowStyle CssClass="gridItem" />
                                        <SelectedRowStyle Font-Size="XX-Small"></SelectedRowStyle>
                                    </asp:GridView>
                                </div>
                            </asp:Panel>
                            <asp:CollapsiblePanelExtender ID="cpeDemo" runat="Server" CollapseControlID="pEncBusqueda"
                                Collapsed="True" CollapsedImage="~/Images/expand.jpg" CollapsedText="(Click Aqui para Mostrar Detalles...)"
                                ExpandControlID="pEncBusqueda" ExpandedImage="~/Images/collapse.jpg" ExpandedText="(Click Aqui para Ocultar Detalles...)"
                                ImageControlID="imgExpand" SkinID="CollapsiblePanelDemo" SuppressPostBack="true"
                                TargetControlID="pErroresG" TextLabelID="lblMostrarDetalles" />
                            <br />
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="ViewProceso" runat="server">
            <asp:HiddenField ID="HF1" runat="server" />
            <asp:ModalPopupExtender ID="mpeNuevo" runat="server"
                PopupControlID="panelActividadReg" TargetControlID="HF1"
                BackgroundCssClass="backgroundColor">
            </asp:ModalPopupExtender>
            <asp:Panel ID="panelActividadReg" runat="server">
                <div id="popupcontainer">
                    <div class="row">
                        <div class="cell popupcontainercell">
                            <div id="ordereditcontainer">
                                <asp:UpdatePanel ID="upActividadReg" runat="server" width="100%">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="cell ordereditcell" style="width: 100%">
                                                <br>
                                            </div>
                                            <div class="cell" style="width: 100%" align="center">
                                                <div class="cell">
                                                    Este proceso tardará varios minutos. <strong>Esta seguro de continuar?</strong><br />
                                                    <br />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="cell ordereditcell" style="width: 100%">
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <div class="row">
                                    <div class="cell" style="width: 100%">
                                    </div>
                                    <div class="cell" style="text-align: center; width: 100%;">
                                        <asp:Button ID="btnContinuar" runat="server" Text="Continuar"
                                            CssClass="btn8" Width="182px" OnClick="btnContinuar_Click" />
                                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn8"
                                            Width="182px" OnClick="btnCancelar_Click" />
                                    </div>
                                    <div class="cell" style="width: 100%">
                                        <br />
                                    </div>
                                    <div class="cell" style="width: 100%; text-align: center;">
                                        Luego de oprimir el botón continuar deberá esperar hasta que termine el proceso.<br />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <asp:HiddenField ID="HF2" runat="server" />
            <asp:ModalPopupExtender ID="mpeProcesando" runat="server" PopupControlID="pProcesando" TargetControlID="HF2" BackgroundCssClass="backgroundColor">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pProcesando" runat="server" BackColor="White" Style="text-align: center;">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td align="center">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/loading.gif" />
                            <br />
                            <asp:Label ID="Label1" runat="server" Text="Espere un Momento Mientras Termina el Proceso"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <asp:HiddenField ID="HF3" runat="server" />
            <asp:ModalPopupExtender ID="mpeFinal" runat="server" PopupControlID="pFinal" TargetControlID="HF3" BackgroundCssClass="backgroundColor" DropShadow="True" Drag="True" >
            </asp:ModalPopupExtender>
            <asp:Panel ID="pFinal" runat="server" BackColor="White" Style="text-align: center; width: 50%">
                <div id="popupcontainerP" style="width: 500px">
                    <table style="width: 100%;">
                        <tr>
                            <td align="center" style="width: 70%">
                                <br />
                                <asp:Label ID="Label3" runat="server" Text="Se inicio proceso de envio de emails, la tarea seguira ejecutandose en el servidor mientras usted este en FINANCIAL"></asp:Label>
                                <br />
                                <asp:Button ID="btnSalir" runat="server" Text="Salir"
                                    CssClass="btn8" Width="100px" OnClick="btnSalir_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>

            <asp:UpdatePanel ID="UpdatePanel1" runat="server" Style="text-align: center; width: 100%">
                <ContentTemplate>
                    <asp:Timer ID="Timer1" runat="server" Interval="500" OnTick="Timer1_Tick">
                    </asp:Timer>
                    <br />
                    <asp:Label ID="lblError" runat="server" Text=""
                        Style="text-align: left; color: #FF3300"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:View>
        <asp:View ID="vReporteExtracto" runat="server">
            <table width="100%">
                <tr>
                    <td>
                        <rsweb:ReportViewer ID="RpviewEstado" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="true"
                            Height="500px" InteractiveDeviceInfos="(Colección)" WaitMessageFont-Names="Verdana"
                            WaitMessageFont-Size="14pt" Width="96%" EnableViewState="True">
                            <LocalReport ReportPath="Page\Asesores\EnvioMasivo\ReporteEstadoCuentaXFecha.rdlc" />
                        </rsweb:ReportViewer>
                    </td>
                </tr>
            </table>
        </asp:View>
        <%--<asp:View ID="ViewFinal" runat="server">
            <asp:Panel ID="pFinal" runat="server" BackColor="White" Style="text-align: center; width: 100%" Visible="true">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td align="center" style="width: 100%">
                            <br />
                            <asp:Label ID="Label2" runat="server" Text="Envio de estados de cuenta realizado correctamente" Visible="true"></asp:Label>
                            <br />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>--%>
    </asp:MultiView>
    <uc1:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>

