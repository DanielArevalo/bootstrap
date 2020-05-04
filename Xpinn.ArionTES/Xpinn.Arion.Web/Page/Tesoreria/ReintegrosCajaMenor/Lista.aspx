<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>
<%@ Register Src="../../../General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/ctlGiro.ascx" TagName="ctlgiro" TagPrefix="uc3" %>
<%@ Register Src="~/General/Controles/ctlProcesoContable.ascx" TagName="procesoContable" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pnlGeneral" runat="server">
        <asp:Panel ID="pConsulta" runat="server">
            <br />
            <table id="tbCriterios" border="0" cellpadding="0" cellspacing="0" width="50%">
                <tr>
                    <td class="tdD" style="text-align: left">Fecha<br />
                        <uc1:fecha ID="txtFecha" runat="server"></uc1:fecha>
                    </td>
                    <td class="tdI" style="text-align: left;">Area<br />
                        <asp:DropDownList ID="ddlArea" runat="server" AppendDataBoundItems="True"
                            CssClass="textbox" Style="font-size: xx-small; text-align: left"
                            Width="400px">
                            <asp:ListItem Value=""></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <br />
        </asp:Panel>
        <hr />
        <br />
        <table>
            <tr>
                <td style="text-align: center">
                    <div style="overflow:auto; max-height: 550px">
                        <asp:GridView ID="gvLista" runat="server" Width="90%" AutoGenerateColumns="False"
                            GridLines="Horizontal" ShowHeaderWhenEmpty="True" DataKeyNames="idsoporte">
                            <Columns>
                                <asp:BoundField DataField="idsoporte" HeaderText="Código">
                                    <ItemStyle HorizontalAlign="Left" Width="50px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="cod_per" HeaderText="Cod.Per">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nombre" HeaderText="Nombre">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fecha" HeaderText="Fecha" DataFormatString="{0:d}">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="valor" HeaderText="Valor" DataFormatString="{0:N2}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nomestado" HeaderText="Estado">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="tipo" HeaderText="Id.Tipo">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nomtiposop" HeaderText="Tipo">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="num_comp" HeaderText="Num.Comp.">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nomtipo_comp" HeaderText="Tipo Comp.">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="descripcion" HeaderText="Descripción">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nomoficina" HeaderText="Oficina">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nomusuario" HeaderText="Usuario">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nomarea" HeaderText="Area">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="cod_cuenta" HeaderText="Cod.Cuenta">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                    </div>
                    <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                    <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningun resultado."
                        Visible="False" />
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <br />
                    <uc3:ctlgiro ID="ctlGiro" runat="server" />
                </td>
                <td style="text-align: left">&nbsp;</td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="panelProceso" runat="server" Width="100%">
        <uc2:procesoContable ID="ctlproceso" runat="server" />
    </asp:Panel>
    <br />
</asp:Content>
