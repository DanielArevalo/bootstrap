<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - CDATS Renovacion:." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="numero" TagPrefix="ucnumero" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <table style="width: 708px">
        <tr>
            <td colspan="4" style="text-align: left">
                <strong>Criterios de Busqueda :</strong>
            </td>
        </tr>
        <tr>
            <td style="width: 149px; text-align: left">Numero CDAT<br />
                <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" />
            </td>

            <td style="text-align: left; width: 129px">Fecha de Apertura<br />
                <ucFecha:fecha ID="txtFecha" runat="server" />
            </td>
            <td style="text-align: left">Fecha Vencimiento<br />
                <ucFecha:fecha ID="txtFechaVencimiento" runat="server" />
            </td>
            <td style="text-align: left; width: 129px">Tipo Linea CDAT<br />
                <asp:DropDownList ID="ddlModalidad" runat="server"
                    CssClass="textbox" AppendDataBoundItems="True">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 161px; text-align: left">Identificación<br />
                <asp:TextBox ID="txtidentificacion" runat="server" CssClass="textbox" />
            </td>

            <td style="width: 158px; text-align: left">Nombre<br />
                <asp:TextBox ID="txtnombre" runat="server" CssClass="textbox" />
            </td>

            <td style="text-align: left; width: 121px">Código de nómina<br />
                <asp:TextBox ID="txtCodigoNomina" runat="server" CssClass="textbox"></asp:TextBox>
            </td>

            <td style="text-align: left; width: 121px">Oficina<br />
                <asp:DropDownList ID="ddlOficina" runat="server" CssClass="textbox"
                    AppendDataBoundItems="True" />
            </td>

            <td style="text-align: left; width: 128px">Asesor Comercial<br />
                <asp:DropDownList ID="ddlasesor" runat="server" CssClass="textbox"
                    AppendDataBoundItems="True" />
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
                        RowStyle-CssClass="gridItem" DataKeyNames="codigo_cdat"
                        Style="font-size: xx-small">
                        <Columns>
                            <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_info.jpg" ShowEditButton="True" />
                            <asp:BoundField DataField="codigo_cdat" HeaderText="Cod Cdat">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="numero_cdat" HeaderText="Num.CDAT">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_apertura" HeaderText="Fecha Apertura"
                                DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="modalidad" HeaderText="Modalidad">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="codforma_captacion" HeaderText="Forma">
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
                            <asp:BoundField DataField="tipo_calendario" HeaderText="Tipo">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cod_destinacion" HeaderText="Destina">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombre" HeaderText="Ase">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cod_oficina" HeaderText="Oficina">
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="identificacion" HeaderText="Identificacion">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombre" HeaderText="Nombre Titular">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cod_tipo_tasa" HeaderText="Tipo">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="modalidad_int" HeaderText="Modalida">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tasa_efectiva" HeaderText="Tasa">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tasa_nominal" HeaderText="Tipo Tasa">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tipo_calendario" HeaderText="Tipo Calendario">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cod_tipo_tasa" HeaderText="+pum">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cod_tipo_tasa" HeaderText=" ">
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

</asp:Content>
