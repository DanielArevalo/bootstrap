<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Desembolso Masivo :." %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="numero" TagPrefix="ucnumero" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <table style="width: 99%">
        <tr>
        <td >
                Cod. Persona<br />
                <asp:TextBox ID="txtcodigo" runat="server" CssClass="textbox"  />
            </td>
            <td >
                Identificación<br />
                <asp:TextBox ID="txtidentificacion" runat="server" CssClass="textbox"  />
            </td>
            <td  >
                Tipo Identif.<br />
                <asp:DropDownList ID="ddlidentificacion" runat="server" CssClass="textbox" 
                    Width="200px"  />
            </td>
            <td >
                Nombre<br />
                <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox"  />
            </td>
            <td >
                Num. Factura<br />
                <asp:TextBox ID="txtNumFact" runat="server" CssClass="textbox" />
            </td>
            <td >
                Fecha Factura<br />
                <ucFecha:fecha ID="txtfecFactuta" runat="server" CssClass="textbox"/>
            </td>
            <td >
                Fecha Anticipo<br />
                <ucFecha:fecha ID="txtfecanticipo" runat="server" CssClass="textbox"/>
            </td>
        </tr>
    </table>      
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
                            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />
                            <asp:BoundField DataField="id" HeaderText="Id">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="codigo_factura" HeaderText="Cód.Factura">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_factura" HeaderText="F. Factura" DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombre" HeaderText="Nombres" />
                            <asp:BoundField HeaderText="Valor Neto" DataField="valorneto" DataFormatString="{0:c}" />
                            <asp:BoundField HeaderText="Saldo" DataField="valortotal" DataFormatString="{0:c}" />
                            <asp:BoundField DataField="fecha_radicacion" HeaderText="F. Anticipo" DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_vencimiento" HeaderText="F. Aprobación" DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Valor Neto" DataField="valorneto" DataFormatString="{0:c}" />
                            <asp:BoundField HeaderText="Saldo" DataField="valor_total" DataFormatString="{0:c}" />
                            <asp:BoundField DataField="estado" HeaderText="Estado">
                                <ItemStyle HorizontalAlign="center" />
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
