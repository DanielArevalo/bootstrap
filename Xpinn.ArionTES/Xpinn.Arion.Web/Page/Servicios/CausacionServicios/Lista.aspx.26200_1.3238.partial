<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Desembolso Masivo :." %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar"
    TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="numero" TagPrefix="ucnumero" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script type="text/javascript" src="../../../Scripts/jquery-1.4.4.min.js"></script>
    
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
                    $(".filtro tr:visible .indexColumn:not(:contains('"+ this + "'))").parent().hide();
                });
            });
        });
     </script>

    <table>
        <tr>
            <td style="text-align: left; width: 150px">
                Fecha Causación<br />
                <ucFecha:fecha ID="txtFecha" runat="server" CssClass="textbox" />
                <%--<asp:DropDownList ID="ddlFechaCorte" runat="server" CssClass="dropdown" 
                        Width="160px" />--%>
            </td>
            <td style="text-align: left;">
                Línea de Servicio<br />
                <asp:DropDownList ID="ddlLinea" runat="server" CssClass="textbox" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="ddlLinea_SelectedIndexChanged" />
                <asp:Label ID="lblCodProveedor" runat="server" Visible="false" />    
            </td>
        </tr>
    </table>
    <hr style="width: 100%" />
<%--    <asp:UpdatePanel ID="grid" runat="server">
        <ContentTemplate>--%>
            <table style="width: 100%">
                <tr>
                    <td style="text-align: left">
                        <asp:Panel ID="panelGrilla" runat="server">
                            <div style="overflow:scroll; max-height:630px; text-align:center">
                                <strong>Listado de Servicios a Renovar</strong><br /><br />
                            <asp:Label ID="lblBuscar" runat="server" Text="Buscar:" />
                            <input type="text" id="txtBuscar" name="txtBuscar"/><br />
                            <asp:GridView ID="gvLista" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
                                HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" class="filtro"
                                DataKeyNames="numero_servicio, valor_cuota" Style="font-size: x-small">
                                <Columns> 
                                    <asp:TemplateField HeaderText="Causación">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkEncabezado" runat="server" AutoPostBack="true" OnCheckedChanged="chkEncabezado_CheckedChanged" Checked="true" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkCausacion" runat="server" AutoPostBack="true" OnCheckedChanged="chkCausacion_CheckedChanged" Checked="true"/>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="numero_servicio" HeaderText="Num. Servicio">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="cod_persona" HeaderText="Cod. Persona" Visible="false">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nombre" HeaderText="Nombre">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nom_linea" HeaderText="Linea">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nom_plan" HeaderText="Plan">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="num_poliza" HeaderText="Poliza">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fecha_aprobacion" HeaderText="Fec. Aprobación" DataFormatString="{0:d}">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fecha_proximo_pago" HeaderText="Fec. Prox Pago" DataFormatString="{0:d}">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fecha_inicio_vigencia" HeaderText="Fec. Inicial" DataFormatString="{0:d}">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fecha_final_vigencia" HeaderText="Fec. Final" DataFormatString="{0:d}">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="valor_total" HeaderText="Valor" DataFormatString="{0:n0}" />
                                    <asp:BoundField HeaderText="#Cuota" DataField="numero_cuotas" />
                                    <asp:BoundField HeaderText="Vr.Cuota" DataField="valor_cuota" DataFormatString="{0:n0}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Saldo" DataField="saldo" DataFormatString="{0:n0}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Periodicidad" DataField="nom_periodicidad" />
                                    <asp:BoundField HeaderText="Forma de Pago" DataField="forma_pago" />
                                    <asp:BoundField HeaderText="Nom. Proveedor" DataField="nombre_proveedor" />
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
                    <td style="text-align: right">
                        <asp:Label ID="lblTexto" runat="server" Text="Total : " />
                        <asp:TextBox ID="txtVrTotal" runat="server" Width="160px" Enabled="false" CssClass="textbox"
                            Style="text-align: right" />
                    </td>
                </tr>
            </table>
<%--        </ContentTemplate>
    </asp:UpdatePanel>--%>
    <center>
        <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
        <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado"
            Visible="False" />
    </center>
    <br />
    <br />
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
