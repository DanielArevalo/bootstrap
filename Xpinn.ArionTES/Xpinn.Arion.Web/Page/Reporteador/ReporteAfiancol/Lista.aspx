<%@ Page Title=".: Xpinn - Reporte Cliente X producto:." Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/ctlProcesoContable.ascx" TagName="procesoContable" TagPrefix="uc2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:Panel ID="panelGeneral" runat="server">
    <table style="width: 100%">
        <tr>
            <td align="center">
                <asp:Label ID="LabelFecha" runat="server" Text="Fecha de Corte"></asp:Label>
                <br />
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <%--<asp:TextBox ID="txtFechaIni" runat="server" CssClass="textbox" Height="20px" MaxLength="10"
                                Width="188px"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" DaysModeTitleFormat="dd/MM/yyyy"
                                Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFechaIni" TodaysDateFormat="dd/MM/yyyy"></asp:CalendarExtender>--%>
                        <asp:DropDownList ID="ddlFechas" runat="server" CssClass="dropdown" Width="160px" DataFormatString="{0:dd/MM/yyyy}" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>

        <tr>
            <td>
                <asp:GridView ID="gvLista" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
                    AllowPaging="True" PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                    RowStyle-CssClass="gridItem" DataKeyNames="NumeroRadicacion">
                    <Columns>
                        <asp:BoundField DataField="FechaHistorico" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}"></asp:BoundField>
                        <asp:BoundField DataField="Identificacion" HeaderText="Identificación"></asp:BoundField>
                        <asp:BoundField DataField="NombreApellidos" HeaderText="Nombre"></asp:BoundField>
                        <asp:BoundField DataField="NumeroRadicacion" HeaderText="Numero Radicación"></asp:BoundField>
                        <asp:BoundField DataField="FechaDesembolso" HeaderText="Fecha Desembolso" DataFormatString="{0:dd/MM/yyyy}"></asp:BoundField>
                        <asp:BoundField DataField="Plazo" HeaderText="Plazo"></asp:BoundField>
                        <asp:BoundField DataField="ValorDesembolsado" HeaderText="Valor Desembolsado" DataFormatString="{0:n0}"></asp:BoundField>
                        <asp:BoundField DataField="SaldoCredito" HeaderText="Saldo Credito" DataFormatString="{0:n0}"></asp:BoundField>
                        <asp:BoundField DataField="ValorAportes" HeaderText="Valor Aportes" DataFormatString="{0:n0}"></asp:BoundField>
                        <asp:BoundField DataField="TasaAfiancol" HeaderText="Tasa Afiancol" ></asp:BoundField>
                        <asp:BoundField DataField="SaldoInsoluto" HeaderText="Saldo Insoluto" DataFormatString="{0:n0}"></asp:BoundField>
                        <asp:BoundField DataField="Remuneracion" HeaderText="Remuneracion" DataFormatString="{0:n0}"></asp:BoundField>
                        <asp:BoundField DataField="Iva" HeaderText="Iva" DataFormatString="{0:n0}"></asp:BoundField>
                        <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:n0}"></asp:BoundField>
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
    <asp:Panel ID="panelProceso" runat="server" Width="100%">
        <uc2:procesoContable ID="ctlproceso" runat="server" />
    </asp:Panel>
</asp:Content>

