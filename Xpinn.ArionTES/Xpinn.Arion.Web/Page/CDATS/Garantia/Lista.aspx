<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - CDATS Renovacion:." %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="numero" TagPrefix="ucnumero" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
                                    </asp:ScriptManager>

    <table style="width: 1039px">
        <tr>
            <td colspan="4" style="text-align:left">
                <strong>Criterios de Busqueda :</strong>
            </td>
        </tr>
        <tr>            
            <td style="width: 149px; text-align: left">
                Numero CDAT<br />
                <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="62%" />
            </td>         
            <td style="width: 149px; text-align: left">
                Numero Credito<br />
                <asp:TextBox ID="txtNumCredito" runat="server" CssClass="textbox" Width="62%" />
            </td>   
           
            <td style="text-align: left; width:129px">
                Fecha de Garantia<br />
                <ucFecha:fecha ID="txtFechaGarantia" runat="server"/>
            </td>
             <td style="width: 161px; text-align: left">
                Identificación<br />
                <asp:TextBox ID="txtidentificacion" runat="server" CssClass="textbox" Width="90%" />
            </td>    
             <td style="width: 158px; text-align: left">
                Nombre<br />
                <asp:TextBox ID="txtnombre" runat="server" CssClass="textbox" Width="90%" />
            </td>   
            <td style="text-align: left; width: 121px">
                Oficina<br />
                <asp:DropDownList ID="ddlOficina" runat="server" CssClass="textbox" 
                    Width="93%" AppendDataBoundItems="True" />
            </td>       
        </tr>
   </table>
            
    <asp:Panel ID="panelGrilla" runat="server">
        <table style="width: 100%">
            <tr>
                <td>              
                    <asp:GridView ID="gvLista" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
                        AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging" OnRowEditing="gvLista_RowEditing"
                        PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                        RowStyle-CssClass="gridItem" DataKeyNames="codigo_cdat" OnRowDeleting="gvLista_RowDeleting"
                        style="font-size: xx-small">
                        <Columns>                        
                            <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_elim.jpg"   ShowEditButton="True" />
                            <asp:BoundField DataField="codigo_cdat" HeaderText="Cod CDA">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                             <asp:BoundField DataField="numero_fisico" HeaderText="Número">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cod_lineacdat" HeaderText="Línea">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                             <asp:BoundField DataField="cod_oficina" HeaderText="Oficina">
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                             <asp:BoundField DataField="identificacion" HeaderText="Identificacion">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombre" HeaderText="Nombres">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_apertura" HeaderText="Fecha Apertura" 
                                DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="valor" HeaderText="Valor" DataFormatString="{0:n}">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nommoneda" HeaderText="Moneda">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="plazo" HeaderText="Plazo">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_inicio" HeaderText="Fecha Inicio" 
                                DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_vencimiento" HeaderText="Fecha Vencimiento" 
                                DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                             <asp:BoundField DataField="estado" HeaderText="Estado">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                             <asp:BoundField DataField="numero_fisico" HeaderText="Num Credito">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cod_lineacdat" HeaderText="Línea">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="monto" HeaderText="Monto">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="valor" HeaderText="Saldo">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                             <asp:BoundField DataField="estado" HeaderText="Estado">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                             <asp:BoundField DataField="monto" HeaderText="Valor Garantia">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                        <PagerStyle CssClass="gridPager"></PagerStyle>
                        <RowStyle CssClass="gridItem"></RowStyle>
                    </asp:GridView>
                    <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <br />
    <br />

    <uc4:mensajeGrabar ID="ctlMensaje" runat="server"/>
   
</asp:Content>
