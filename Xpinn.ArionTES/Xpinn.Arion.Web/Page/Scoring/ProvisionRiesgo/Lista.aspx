<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server">
        <br />
        <br />
    </asp:Panel>
    <asp:Panel ID="mvScoringCreditos" runat="server">
        <table style="width: 100%;">
            <tr>
                <td>
                    <asp:Label ID="Label1" Text="Cierres" runat="server" /><br />
                    <asp:DropDownList ID="ddlFechaCierre" runat="server" AppendDataBoundItems="true" CssClass="textbox" Width="90%">
                        <asp:ListItem Text="Seleccione un Item" Value=" " />
                    </asp:DropDownList>
                </td>
                <td style="width: 309px">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 309px">
                </td>
                <td style="width: 309px">
                </td>
                <td style="width: 309px">
                </td>
                <td style="width: 309px">
                </td>
            </tr>
        </table>
        <table style="width: 100%;">
            <tr>
                <td colspan="4">
                    <hr width="100%" />
                </td>
            </tr>
            <tr>
                <td colspan="4" style="text-align: left">
                    <asp:GridView ID="gvLista" runat="server" AllowPaging="True" AutoGenerateColumns="False" Style="text-align: left"
                        ShowHeaderWhenEmpty="True" Width="100%" OnPageIndexChanging="gvLista_PageIndexChanging" >
                        <Columns>
                            <asp:BoundField DataField="fecha_corte" HeaderText="F.Corte" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="oficina" HeaderText="Oficina" />                                
                            <asp:BoundField DataField="numero_radicacion" HeaderText="No.Obligaciòn" />
                            <asp:BoundField DataField="identificacion" HeaderText="NroDocumento" />
                            <asp:BoundField DataField="nombre" HeaderText="Nombres y Apellidos" />
                            <asp:BoundField DataField="linea" HeaderText="Línea" />
                            <asp:BoundField DataField="monto" HeaderText="Monto Aprobado" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="saldo_capital" HeaderText="Saldo al corte" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right" />                                
                            <asp:BoundField DataField="cod_categoria" HeaderText="Calif.Actual" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="dias_mora" HeaderText="Dias Mora" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="porc_provision" HeaderText="%Prov." DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right" />  
                            <asp:BoundField DataField="provision" HeaderText="Provisiòn" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right" />                                
                            <asp:BoundField DataField="probabilidad_incumplimiento" HeaderText="Probabilidad de incumplimiento" ItemStyle-Width="70px" />
                            <asp:BoundField DataField="cod_categoria_pro" HeaderText="Categoría por Probabilidad de incumplimiento" ItemStyle-Width="70px" />
                            <asp:BoundField DataField="porc_provision" HeaderText="% Prov." DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right" /> 
                            <asp:BoundField DataField="porc_provision_riesgo" HeaderText="%Prov.Riesgo" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right" /> 
                            <asp:BoundField DataField="provision_riesgo" HeaderText="Provisiòn Riesgo" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="aumento_provision" HeaderText="Aumento Provisión" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right" /> 
                        </Columns>
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                        <RowStyle CssClass="gridItem" />
                    </asp:GridView>
                    <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                    <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningun resultado."
                        Visible="False" />
                </td>
            </tr>
        </table>
    </asp:Panel>

</asp:Content>
