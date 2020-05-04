<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" 
    AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Desembolso Masivo :." %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="numero" TagPrefix="ucnumero" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
     <asp:Label ID="Label1" visible="false" runat="server"  ForeColor="Red">No puede Desembolsar avances.Verifique que el Crédito este al dia</asp:Label>
    <table style="width: 800px">
        <tr>
            <td style="text-align: left" colspan="5">
                <strong>Criterios de Búsqueda</strong>
               
            </td>
        </tr>
        <tr>
            <td style="width: 15%; text-align: left">Num. Crédito<br />
                <asp:TextBox ID="txtNumCred" runat="server" CssClass="textbox" Width="90%" />
            </td>
            <td style="text-align: left; width: 15%">Fec. Solicitud<br />
                <ucFecha:fecha ID="txtFecha" runat="server" CssClass="textbox" />
            </td>
            <td style="text-align: left; width: 18%">Linea de Crédito<br />
                <asp:DropDownList ID="ddlLinea" runat="server" CssClass="textbox" Width="90%" />
            </td>
            <td style="text-align: left; width: 10%">Identificación<br />
                <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Width="90%" />
            </td>
            <td style="text-align: left; width: 25%">Nombre<br />
                <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Width="90%" />
            </td>
            <td class="tdI" style="text-align: left">Código de nómina<br />
                <asp:TextBox ID="txtCodigoNomina" runat="server" CssClass="textbox" Width="110px" />
            </td>
            <td style="text-align: left; width: 20%">Oficina<br />
                <asp:DropDownList ID="ddlOficina" runat="server" CssClass="textbox" Width="90%"
                    AppendDataBoundItems="True" />
            </td>
        </tr>
    </table>

    <asp:Panel ID="panelGrilla" runat="server">
        <table style="width: 100%">
            <tr>
                <td style="text-align: left">
                    <strong>Listado de Avances :</strong>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:UpdatePanel ID="Panel1" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="gvLista" runat="server" GridLines="Horizontal" AutoGenerateColumns="False"
                                AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                                PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                                RowStyle-CssClass="gridItem" DataKeyNames="numero_radicacion"
                                Style="font-size: x-small">
                                <Columns>
                                    <asp:CommandField ButtonType="Image" SelectImageUrl="~/Images/gr_edit.jpg" ShowEditButton="False" ShowSelectButton="True" />
                                    <asp:BoundField DataField="numero_radicacion" HeaderText="Num. Crédito">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nomlinea" HeaderText="Linea">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="cod_deudor" HeaderText="Cod.Deudor">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nombre" HeaderText="Nombre">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="cod_nomina" HeaderText="Código de nómina">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="idavance" HeaderText="Num. Avance">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fecha_solicitud" HeaderText="Fec. Solicitud" DataFormatString="{0:d}">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="valor_solicitado" HeaderText="Vr. Solicitado" DataFormatString="{0:c}">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fecha_aprobacion" HeaderText="Fec. Aprobación" DataFormatString="{0:d}">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="valor_aprobado" HeaderText="Vr. Aprobado" DataFormatString="{0:c}">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                </Columns>
                                <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                                <PagerStyle CssClass="gridPager"></PagerStyle>
                                <RowStyle CssClass="gridItem"></RowStyle>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <br />
    <br />

    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />


</asp:Content>
