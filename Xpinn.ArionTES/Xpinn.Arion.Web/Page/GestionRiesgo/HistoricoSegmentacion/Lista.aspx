<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>
<%@ Register Src="../../../General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
     <asp:MultiView ID="mvHistorico" runat="server" ActiveViewIndex="0">
      <asp:View ID="vListado" runat="server">
    <asp:Panel ID="pConsulta" runat="server">
        <br />
        <table id="tbCriterios" border:"0" cellpadding:"0" cellspacing:"0" width:"70%">
            <tr>
                <td style="text-align: left">
                    <strong>Filtrar por:</strong>
                </td>
            </tr>
            <tr>
                   <td style="text-align: left; width: 120px">
                        <asp:Label ID="Label2" Text="Fecha Cierre" runat="server" /><br />
                        <asp:DropDownList ID="ddlFechaCierre" runat="server" AppendDataBoundItems="true" CssClass="textbox" Width="90%">
                            <asp:ListItem Text="Seleccione un Item" Value=" " />
                        </asp:DropDownList>
                    </td>
                <td class="tdD" style="text-align: left; width: 120px">Nombre<br />
                    <asp:TextBox ID="txtNompe" runat="server" CssClass="textbox"
                        MaxLength="50" Width="120px" />
                </td> 
                 <td class="tdD" style="text-align: left; width: 120px">Apellido<br />
                    <asp:TextBox ID="txtApe" runat="server" CssClass="textbox"
                        MaxLength="50" Width="120px" />
                </td> 
                 <td class="tdD" style="text-align: left; width: 120px">Identificacion<br />
                    <asp:TextBox ID="txtIden" runat="server" CssClass="textbox"
                        MaxLength="25" Width="120px" />
                </td> 
                <td class="tdD" style="text-align: left; width: 200px">Perfil de Riesgo<br />
                    <asp:DropDownList ID="ddlPerfilRiesgo" runat="server" AppendDataBoundItems="true" CssClass="textbox" Width="90%">
                        <asp:ListItem Text="Seleccione un Item" Value="" />
                        <asp:ListItem Text="Riesgo Normal" Value="Riesgo Normal" />
                        <asp:ListItem Text="Riesgo Moderado" Value="Riesgo Moderado" />
                        <asp:ListItem Text="Riesgo Alto" Value="Riesgo Alto" />
                    </asp:DropDownList>
                </td> 
                <td class="tdD" style="text-align: left; width: 200px">Segmento de Riesgo<br />
                    <asp:DropDownList ID="ddlSegmentoRiesgo" runat="server" AppendDataBoundItems="true" CssClass="textbox" Width="90%">
                        <asp:ListItem Text="Seleccione un Item" Value="" />
                    </asp:DropDownList>
                </td> 
            </tr>
        </table>
    </asp:Panel>
    <hr /
    <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False"
        AllowPaging="True" PageSize="20" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
        OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged" OnRowCommand="gvlista_RowCommand"
        DataKeyNames="cod_persona "
        Style="font-size: x-small">
        <Columns>
          <asp:TemplateField HeaderStyle-CssClass="gridIco">
                   <ItemTemplate>
                   <asp:ImageButton ID="btnImprimir" runat="server" ImageUrl="~/Images/gr_imp.gif" ToolTip="Imprimir" CommandName="Imprimir" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
               </ItemTemplate>
            <HeaderStyle CssClass="gridIco"></HeaderStyle>
           </asp:TemplateField>
            <asp:BoundField DataField="cod_persona" HeaderText="Codigo Persona">
                <ItemStyle HorizontalAlign="Left" Width="300px" />
            </asp:BoundField>
            <asp:BoundField DataField="primer_nombre" HeaderText="Primer Nombre">
                <ItemStyle HorizontalAlign="Left" Width="300px" />
            </asp:BoundField>
            <asp:BoundField DataField="segundo_nombre" HeaderText="Segundo Nombre">
                <ItemStyle HorizontalAlign="Left" Width="300px" />
            </asp:BoundField>
                        <asp:BoundField DataField="primer_apellido" HeaderText="Primer apellido">
                <ItemStyle HorizontalAlign="Left" Width="300px" />
            </asp:BoundField>
            <asp:BoundField DataField="identificacion" HeaderText="Identificacion">
                <ItemStyle HorizontalAlign="Left" Width="300px" />
            </asp:BoundField> 
            <asp:BoundField DataField="segmentoActual" HeaderText="F.R.ASOCIADOS">
                <ItemStyle HorizontalAlign="Left" Width="300px" />
            </asp:BoundField> 
            <asp:BoundField DataField="segmento_pro" HeaderText="F.R.PRODUCTOS">
                <ItemStyle HorizontalAlign="Left" Width="300px" />
            </asp:BoundField> 
            <asp:BoundField DataField="segmento_can" HeaderText="F.R.CANALES">
                <ItemStyle HorizontalAlign="Left" Width="300px" />
            </asp:BoundField> 
            <asp:BoundField DataField="segmento_jur" HeaderText="F.R.JURISDICCION">
                <ItemStyle HorizontalAlign="Left" Width="300px" />
            </asp:BoundField>
            <asp:BoundField DataField="calificacion" HeaderText="Calificación">
                <ItemStyle HorizontalAlign="Left" Width="300px" />
            </asp:BoundField>  
            <asp:BoundField DataField="Perfil_riesgo" HeaderText="Perfil de riesgo">
                <ItemStyle HorizontalAlign="Left" Width="300px" />
            </asp:BoundField> 
            <asp:BoundField DataField="FECHACIERRE" HeaderText="Fecha de cierre">
                <ItemStyle HorizontalAlign="Left" Width="300px" />
            </asp:BoundField>
            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblCod" runat="server" Text='<%# Bind("cod_persona") %>' Visible="false" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <HeaderStyle CssClass="gridHeader" />
        <PagerStyle CssClass="gridHeader" Font-Bold="False" />
        <RowStyle CssClass="gridItem" />
    </asp:GridView>
    <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
    <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningun resultado."
        Visible="False" />
    <br />

    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
    </asp:View>
      <asp:View ID="vReporte" runat="server">
            <br /><br />
            <table>
                <tr>
                    <td>
                        <rsweb:ReportViewer ID="ReportViewerFactor" runat="server" Font-Names="Verdana" Font-Size="8pt"
                            AsyncRendering="false" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="10pt" Width="100%">
                            <LocalReport ReportPath="Page\GestionRiesgo\HojaVidaRiesgo\ReporteRiesgo.rdlc">
                                <DataSources>
                                    <rsweb:ReportDataSource />
                                </DataSources>
                            </LocalReport>
                        </rsweb:ReportViewer>
                    </td>
                </tr>
            </table>
        </asp:View>
 </asp:MultiView>
</asp:Content>

