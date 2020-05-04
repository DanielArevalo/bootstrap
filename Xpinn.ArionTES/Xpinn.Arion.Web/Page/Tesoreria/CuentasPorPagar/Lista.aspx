<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Cuentas por Pagar :." %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="numero" TagPrefix="ucnumero" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <table style="width: 95%">
        <tr>
            <td class="logo" style="width: 10%; text-align: left">
                Código :<br />
                <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="90%" />
            </td>
            <td style="width: 15%; text-align: left">
                Num. Factura<br />
                <asp:TextBox ID="txtNumFact" runat="server" CssClass="textbox" Width="90%" />
            </td>
            <td style="text-align: center; width:40%">
                Fecha Ingreso<br />
                <ucFecha:fecha ID="txtIngresoIni" runat="server" CssClass="textbox"/>
                &nbsp; a &nbsp;
                <ucFecha:fecha ID="txtIngresoFin" runat="server" CssClass="textbox"/>
            </td>
            <td style="text-align: center; width:40%">
                Fecha Vencimiento<br />
                <ucFecha:fecha ID="txtVencimientoIni" runat="server" CssClass="textbox"/>
                &nbsp; a &nbsp;
                <ucFecha:fecha ID="txtVencimientoFin" runat="server" CssClass="textbox"/>
            </td>
        </tr>
    </table>
    <table style="width: 780px">
        <tr>
            <td style="width: 200px; text-align: left">
                Tipo Cta Por Pagar<br />
                              
                 <asp:DropDownList ID="ddlTipoCuenta" runat="server" CssClass="textbox"  Width="90%">
                      
                            </asp:DropDownList>   
                
            </td>
            <td style="text-align: left;width:100px">
                Cod. Proveedor<br />
                <asp:TextBox ID="txtIdProveedor" runat="server" CssClass="textbox" Width="90%"/>
            </td>
            <td style="text-align: left; width:280px">
                Nombre<br />
                <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Width="95%" />
            </td>
            <td style="text-align: left; width:200px">
               Estado<br />
                <asp:DropDownList ID="ddlEstado" runat="server" CssClass="textbox" Width="95%" />
            </td>
        </tr>
    </table>
    <hr style="width: 100%" />
    <asp:Panel ID="panelGrilla" runat="server">
        <table style="width: 100%">
            <tr>
                <td>
                    <asp:GridView ID="gvLista" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
                        AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging" OnRowEditing="gvLista_RowEditing"
                        PageSize="20" HeaderStyle-CssClass="gridHeader" 
                        PagerStyle-CssClass="gridPager" DataKeyNames="codigo_factura"
                        onrowdeleting="gvLista_RowDeleting" style="font-size: xx-small">
                        <Columns>
                            <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_edit.jpg" 
                                ShowEditButton="True" />
                            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" 
                                ShowDeleteButton="True" Visible="false" />
                            <asp:BoundField DataField="codigo_factura" HeaderText="Código">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="numero_factura" HeaderText="Num_Factura">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_ingreso" HeaderText="F. Ingreso" DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_factura" HeaderText="F. Factura" DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_radicacion" HeaderText="F. Radicación" DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_vencimiento" HeaderText="F. Vencimi."                                 DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tiponom" HeaderText="Tipo">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="doc_equivalente" HeaderText="Doc.Equivalente">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                            <asp:BoundField HeaderText="Valor total" DataField="valortotal" 
                                DataFormatString="{0:c}" />
                            <asp:BoundField HeaderText="Valor Neto" DataField="valorneto" 
                                DataFormatString="{0:c}" />
                            <asp:BoundField HeaderText="Maneja Dscto" DataField="manejadscto" />
                            <asp:BoundField HeaderText="Maneja Anticipo" DataField="manejaanti" />
                             <asp:BoundField HeaderText="Estado" DataField="nomestado" />
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
