<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Reporte Productos :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc2" %>
<%@ Register Src="~/General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/decimalesGridRow.ascx" TagName="decimalesGridRow" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">


    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table style="width: 70%;">
        <tr>
            <td style="font-size: x-small; text-align: left" colspan="4">
                <strong>Criterios de consulta :</strong>
            </td>
        </tr>
        <tr>
            <td style="font-size: x-small; text-align: left" colspan="2">
                <strong>Periodo Afiliación:</strong>
            </td>
            <td style="font-size: x-small; text-align: left">
            </td>
            <td style="font-size: x-small; text-align: left">
            </td>
        </tr>
        <tr>
            <td style="text-align: left;">
                <br />
                <uc2:fecha id="txtFechaIni" runat="server" Visible="false"/>
            </td>
            <td style="text-align: left;">
                Fecha Final<br />
                <uc2:fecha id="txtFechaFin" runat="server" />
            </td>
            <td style="text-align: left;">
                Identificación<br />
                <asp:TextBox id="txtIdentificacion" runat="server" CssClass="textbox" />
            </td>
            <td style="text-align: left;">
                Oficina<br />
                <asp:DropDownList ID="ddlOficinas" runat="server" CssClass="textbox" Width="230px" AppendDataBoundItems="True" />
            </td>
            <td style="text-align: left;">
                Estado asociado<br />
                <asp:DropDownList ID="ddlEstado" runat="server" CssClass="textbox" Width="230px" AppendDataBoundItems="True" >
                    <asp:ListItem Value="" Selected="True">Seleccione un item</asp:ListItem>
                    <asp:ListItem Value="A">Activo</asp:ListItem>
                    <asp:ListItem Value="R">Retirado</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="text-align: left;">
                <asp:CheckBox ID="chkSaldo" runat="server" Width="230px" Text="Mostrar saldos en 0" OnCheckedChanged="saldosCero" AutoPostBack="true"></asp:CheckBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
    </table>
    <asp:Panel ID="pListado" runat="server" Visible ="false">
    <div style="text-align:Center">
        <asp:Button ID="btnExportar" runat="server" CssClass="btn8" onclick="btnExportar_Click" Text="Exportar a excel" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
    </div>
    <div style="overflow:scroll; max-height:630px; text-align:center">
    <asp:UpdatePanel ID="Panelgrilla" runat="server" RenderMode="Inline" UpdateMode="Conditional"><ContentTemplate>                
        <table border="0" cellpadding="0" cellspacing="0" width="100%">                        
            <tr>
                <td style="text-align: left; width:100%"> 
                    <asp:GridView ID="gvProductos" runat="server" AutoGenerateColumns="False" GridLines="Horizontal" HeaderStyle-CssClass="gridHeader"
                        PagerStyle-CssClass="gridPager" PageSize="20" RowStyle-CssClass="gridItem" Style="font-size: xx-small" Width="100%">
                        <Columns>                      
                            <asp:BoundField DataField="cod_persona" HeaderText="Cod."><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                            <asp:BoundField DataField="tipo_identificacion" HeaderText="T.Identific." ItemStyle-Width="60px"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                            <asp:BoundField DataField="identificacion" HeaderText="Identificación" ItemStyle-Width="70px"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                            <asp:BoundField DataField="fecha_expedicion" HeaderText="F.Expedicion" DataFormatString="{0:d}"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                            <asp:BoundField DataField="nombre" HeaderText="Nombre" ItemStyle-Width="100px"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                            <asp:BoundField DataField="genero" HeaderText="Genero"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                            <asp:BoundField DataField="oficina" HeaderText="Oficina"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                            <asp:BoundField DataField="fecha_nacimiento" HeaderText="F.Nacimiento" DataFormatString="{0:d}" ><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                            <asp:BoundField DataField="fecha_afiliacion" HeaderText="F.Afiliacion" DataFormatString="{0:d}"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                            <asp:BoundField DataField="ciudad" HeaderText="Ciudad"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                            <asp:BoundField DataField="direccion" HeaderText="Dirección" ItemStyle-Width="100px"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                            <asp:BoundField DataField="telefono" HeaderText="Telefóno"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                            <asp:BoundField DataField="email" HeaderText="E-Mail"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                            <asp:BoundField DataField="estado" HeaderText="Estado"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                            <asp:BoundField DataField="saldo_aportes" HeaderText="Saldo Aportes" ItemStyle-Width="80px"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                            <asp:BoundField DataField="saldo_creditos" HeaderText="Saldo Créditos" ItemStyle-Width="80px"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                            <asp:BoundField DataField="saldo_ahorroV" HeaderText="Saldo A. Vista" ItemStyle-Width="80px"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                            <asp:BoundField DataField="saldo_cdat" HeaderText="Saldo CDATS" ItemStyle-Width="80px"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                            <asp:BoundField DataField="saldo_ahorroP" HeaderText="Saldo A. Programado" ItemStyle-Width="80px"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                            <asp:BoundField DataField="saldo_servicios" HeaderText="Saldo Servicios" ItemStyle-Width="80px"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                        </Columns>
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle CssClass="gridPager" />
                        <RowStyle CssClass="gridItem" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </ContentTemplate></asp:UpdatePanel>
    </div>
    </asp:Panel>     
    <asp:Label ID="lblTotalRegs" runat="server" Visible="false" />
    <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningun resultado." Visible="False"/>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
