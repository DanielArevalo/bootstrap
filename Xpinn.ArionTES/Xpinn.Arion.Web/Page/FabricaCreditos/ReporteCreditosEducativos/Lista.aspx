<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista"  %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/ctlNumero.ascx" TagName="Decimal" TagPrefix="ucDecimal" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>    
<table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:Panel ID="pConsulta" runat="server">
                <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="60%">
                   <tr>
                       <td style="text-align:left">
                           Fecha Corte<br/>
                           <ucfecha:fecha ID="txtFechaCorte" runat="server" />
                       </td>
                       <td style="text-align:left">
                           Tasa de Interés<br/>
                           <ucDecimal:Decimal ID="txtTasa" runat="server" />
                       </td>
                       <td style="text-align:left">
                           &nbsp;
                       </td>
                       <td style="text-align:left">
                           &nbsp;
                       </td>
                   </tr>
                </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td><hr /></td>
        </tr>
        <tr>
            <td>
                <div id="divLista" runat="server" style="overflow: scroll; height: 500px">
                    <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False" 
                        AllowPaging="False" OnPageIndexChanging="gvLista_PageIndexChanging" 
                        PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" 
                        RowStyle-CssClass="gridItem"  DataKeyNames="numero_radicacion" 
                        style="font-size: x-small" >
                        <Columns>
                            <asp:BoundField DataField="numero_radicacion" HeaderText="No.Radicación" />
                            <asp:BoundField DataField="cod_linea_credito" HeaderText="Línea" />
                            <asp:BoundField DataField="nom_linea_credito" HeaderText="Descripción" />
                            <asp:BoundField DataField="identificacion" HeaderText="Identificación" />
                            <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                            <asp:BoundField DataField="codigo_oficina" HeaderText="Oficina" />
                            <asp:BoundField DataField="fecha_aprobacion" HeaderText="Fecha Aprobación" DataFormatString="{0:d}" />
                            <asp:BoundField DataField="fecha_desembolso" HeaderText="Fecha Desembolso" DataFormatString="{0:d}" />
                            <asp:BoundField DataField="fecha_prox_pago" HeaderText="Fecha Prox.Pago" DataFormatString="{0:d}" />
                            <asp:BoundField DataField="monto_aprobado" HeaderText="Monto Aprobado" DataFormatString="{0:n}" />
                            <asp:BoundField DataField="valor_cuota" HeaderText="Vr.Cuota" DataFormatString="{0:n}" />
                            <asp:BoundField DataField="saldo_capital" HeaderText="Saldo Capital" DataFormatString="{0:n}" />
                            <asp:BoundField DataField="intcoriente" HeaderText="Int.Corriente" DataFormatString="{0:n}" />
                            <asp:BoundField DataField="maneja_auxilio" HeaderText="Maneja Auxilio" />
                        </Columns>
                        <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                        <PagerStyle CssClass="gridPager"></PagerStyle>
                        <RowStyle CssClass="gridItem"></RowStyle>
                    </asp:GridView> 
                </div>               
            </td>
        </tr>
    </table>
    <asp:Label ID="lblTotalRegs" runat="server" Visible="True"/>
</asp:Content>
