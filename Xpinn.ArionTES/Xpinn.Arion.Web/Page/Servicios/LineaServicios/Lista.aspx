<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Líneas de Servicio :." %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="numero" TagPrefix="ucnumero" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <table style="width: 780px">
        <tr>            
            <td style="width: 15%; text-align: left">
                Código<br />
                <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="90%" />
            </td>            
            <td style="text-align: left; width:25%">
                 Tipo de Servicio<br />
            <asp:DropDownList ID="ddlTipoServicio" runat="server" CssClass="textbox" Width="90%" />
            </td>  
            <td style="text-align: left; width:30%">
            Descripción<br />
            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" Width="90%"/>
            </td>
            <td style="text-align: left; width:15%">
            Identificación<br />
            <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Width="90%"/>
            </td>
        </tr>
    </table>    
            
    <asp:Panel ID="panelGrilla" runat="server">
        <table style="width: 100%">
            <tr>
                <td>
                <strong>Listado de Servicios :</strong> <br />
                    <asp:GridView ID="gvLista" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
                        AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging" OnRowEditing="gvLista_RowEditing"
                        PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" style="font-size: x-small"
                        RowStyle-CssClass="gridItem" DataKeyNames="cod_linea_servicio" OnRowDeleting="gvLista_RowDeleting">
                        <Columns>
                            <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_edit.jpg" ShowEditButton="True" />
                            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />
                            <asp:BoundField DataField="cod_linea_servicio" HeaderText="Código">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nomtiposervicio" HeaderText="Tipo Servicio">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombre" HeaderText="Descripcion">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="identificacion_proveedor" HeaderText="Identificación">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombre_proveedor" HeaderText="Nombre">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="periodorenov" HeaderText="Periodo Renovación">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="periodopago" HeaderText="Periodo Pago">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha" HeaderText="Fecha" DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="numero_beneficiarios" HeaderText="#Beneficiarios"></asp:BoundField>
                            <asp:BoundField DataField="cobrainteres" HeaderText="Cobra Interés"></asp:BoundField>
                            <asp:BoundField DataField="tasa_interes" HeaderText="Tasa Interés">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nomtipotasa" HeaderText="Tipo Tasa">
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
