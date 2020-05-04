<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/ctlDatosPersona.ascx" TagName="ctlPersona" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server">
        <br />
        <br />
    </asp:Panel>

    <asp:MultiView ActiveViewIndex="0" ID="mvPrincipal" runat="server">
        <asp:View runat="server" ID="viewPrincipal">
            <table style="text-align: center; width: 80%;">
                <tr>
                    <td style="text-align: left; width: 220px">
                        <asp:Label ID="Label2" Text="Cierres" runat="server" /><br />
                        <asp:DropDownList ID="ddlFechaCierre" runat="server" AppendDataBoundItems="true" CssClass="textbox" Width="90%">
                            <asp:ListItem Text="Seleccione un Item" Value=" " />
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left; width: 220px">
                        <asp:Label ID="lbloficina" Text="Oficina" runat="server" /><br />
                        <asp:DropDownList ID="ddloficina" runat="server" AppendDataBoundItems="true" CssClass="textbox" Width="90%">
                            <asp:ListItem Text="Seleccione un Item" Value=" " />
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left; width: 220px">
                        <asp:Label ID="Label1" Text="Factor de riesgo" runat="server" /><br />
                        <asp:DropDownList ID="ddlSegmentoActual" AppendDataBoundItems="true" runat="server" CssClass="textbox" Width="90%">
                            <asp:ListItem Text="Seleccione un Item" Value=" " />
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left; width: 220px">
                        <asp:Label ID="Label3" Text="Calificaciòn" runat="server" /><br />
                        <asp:DropDownList ID="ddlCalificacion" AppendDataBoundItems="true" runat="server" CssClass="textbox" Width="90%">
                            <asp:ListItem Text="" Value="0" />
                            <asp:ListItem Text="BAJO" Value="1" />
                            <asp:ListItem Text="MODERADO" Value="2" />
                            <asp:ListItem Text="ALTO" Value="3" />
                            <asp:ListItem Text="EXTREMO" Value="4" />
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: left; width: 220px">
                        <asp:Label ID="Label4" Text="Tipo Rol" runat="server" /><br />
                        <asp:DropDownList ID="ddlTipoRol" AppendDataBoundItems="true" runat="server" CssClass="textbox" Width="90%">
                            <asp:ListItem Text="" Value="" />
                            <asp:ListItem Text="Activo" Value="A" />
                            <asp:ListItem Text="Retirado" Value="R" />
                            <asp:ListItem Text="Tercero" Value="T" />
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 160px">Codigo Persona<br />
                        <asp:TextBox ID="txtCodigoPersona" onkeypress="return isNumber(event)" runat="server" CssClass="textbox" Width="90%"></asp:TextBox>
                        <br />
                    </td>
                    <td style="width: 200px">No. Identificación:<br />
                        <asp:TextBox ID="txtIdentificacion" onkeypress="return isNumber(event)" runat="server" CssClass="textbox"
                            Width="90%"></asp:TextBox>
                        <br />
                    </td>

                    <td style="text-align: left; width: 220px" colspan="3">
                        <asp:CheckBox runat="server" ID="chkCambiaronSegmento" Text="Cambiaron de calificación" AutoPostBack="true" OnCheckedChanged="chkVolverConsultar_CheckedChanged" />
                        <br />
                        <asp:CheckBox runat="server" ID="chkFaltanPorAnalizar" Text="Faltan por Analizar" AutoPostBack="true" OnCheckedChanged="chkVolverConsultar_CheckedChanged" />
                    </td>
                </tr>
            </table>
            <hr style="width: 100%" />
            <table style="width: 100%">
                <tr>
                    <td colspan="4">
                        <asp:GridView ID="gvLista" runat="server" AllowPaging="True"
                            AutoGenerateColumns="False" 
                            PageSize="20"
                            ShowHeaderWhenEmpty="True" Width="100%"
                            OnPageIndexChanging="gvLista_PageIndexChanging"
                            OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                            Style="font-size: x-small" DataKeyNames="consecutivo">
                            <Columns>
                                <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select"
                                            ImageUrl="~/Images/gr_info.jpg" ToolTip="Detalle" Width="16px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="fechacierre" HeaderText="Fecha Cierre" DataFormatString="{0:d}" />
                                <asp:BoundField DataField="codigopersona" HeaderText="Codigo Persona" />
                                <asp:BoundField DataField="identificacion_persona" HeaderText="Identificacion" />
                                <asp:BoundField DataField="nombre_persona" HeaderText="Nombre" />
                                <asp:BoundField DataField="cod_oficina" HeaderText="Codigo Oficina" />
                                <asp:BoundField DataField="nombre_oficina" HeaderText="Nombre Oficina" />
                                <asp:BoundField DataField="segmentoactual" HeaderText="Cod. Segm. Actual" Visible="false" />
                                <asp:BoundField DataField="nombre_segmentoactual" HeaderText="Nom. Segm. Actual" Visible="false" />
                                <asp:BoundField DataField="segmentoanterior" HeaderText="Cod. Segm. Anterior" Visible="false" />
                                <asp:BoundField DataField="nombre_segmentoanterior" HeaderText="Nom. Segm. Anterior" Visible="false" />
                                <asp:BoundField DataField="segmentoaso" HeaderText="F.R.ASOCIADOS" />
                                <asp:BoundField DataField="Segmentopro" HeaderText="F.R.PRODUCTOS" />
                                <asp:BoundField DataField="Segmentocan" HeaderText="F.R.CANALES" />
                                <asp:BoundField DataField="Segmentojur" HeaderText="F.R.JURISDICCION" />
                                <asp:BoundField DataField="tipo_rol" HeaderText="Tipo Rol" />
                                <asp:BoundField DataField="calificacion_anterior" HeaderText="Calif.Anterior"/>                                
                                <asp:BoundField DataField="calificacion" HeaderText="Calif.Actual" />                            
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
        </asp:View>
        <asp:View runat="server" ID="viewDetalle">
            <%-- <br />
           <br />
            <br />--%>
            <table style="width: 100%;">
                <tr>
                    <td colspan="3" style="text-align: left">
                        <Label style="font-weight:bold">Analisis Oficial Cumplimiento</Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: left;"><%--Identificación<br />--%>
                        <uc1:ctlPersona ID="ctlPersona" runat="server"/>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;">Observación<br />
                        <asp:TextBox ID="txtAnalisisOficial" Width="400px" Height="40px" runat="server" TextMode="MultiLine" Style="resize: none" CssClass="textbox" />
                    </td>
                    <td style="text-align: left;">Fecha de Analisis<br />
                        <asp:TextBox ID="txtFechaAnalisis" Width="150px" runat="server" ReadOnly="true" CssClass="textbox" />
                    </td>
                    <td style="text-align: left;">Usuario que realiza<br />
                        <asp:TextBox ID="txtUsuarioAnalisis" runat="server" Width="250px" ReadOnly="true" CssClass="textbox" />
                    </td>
                </tr>
            </table>
            <div id="divValores" runat="server" visible="false"> 
            <table>
                <tr>
                    <td>
                        <br />
                        <h3>Calificación actual<asp:Label ID="lblactual" runat="server"></asp:Label> </h3>
                    </td>
                    <td>
                        <br />
                        <h3>Calificación anterior<asp:Label ID="lblAnterior" runat="server"></asp:Label></h3>
                    </td>
                </tr>
                <tr>
                    <td style="width: 50%;">
                        <asp:GridView runat="server" ID="gvSegmentoActual" HorizontalAlign="Center" Width="99%" AutoGenerateColumns="false"
                            Style="font-size: x-small" OnRowDataBound="gvSegmentoAnterior_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Descripción">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCondicion" runat="server" Text='<%# Bind("variable") %>' Visible="false"></asp:Label>
                                        <cc1:DropDownListGrid
                                            ID="ddlCondicion" runat="server" AppendDataBoundItems="True" CommandArgument='<%#Container.DataItemIndex %>'
                                            CssClass="textbox" Style="font-size: xx-small; text-align: left" Width="130px" Enabled="false">
                                        </cc1:DropDownListGrid>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="130" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Operador">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOperador" runat="server" Text='<%# Bind("operador") %>' Visible="false"></asp:Label>
                                        <cc1:DropDownListGrid
                                            ID="ddlOperador" runat="server" AppendDataBoundItems="True" CommandArgument='<%#Container.DataItemIndex %>'
                                            CssClass="textbox" Style="font-size: xx-small; text-align: left" Width="120px" Enabled="false">
                                        </cc1:DropDownListGrid>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="130px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Valor" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtValor" runat="server" Style="font-size: xx-small; text-align: left"
                                            Text='<%# Bind("valor") %>' Width="100px" CssClass="textbox" Visible="false" ReadOnly="true" />
                                        <cc1:DropDownListGrid
                                            ID="ddlValor" runat="server" AppendDataBoundItems="True" CommandArgument='<%#Container.DataItemIndex %>'
                                            CssClass="textbox" Style="font-size: xx-small; text-align: left" Width="120px" Visible="false" Enabled="false">
                                        </cc1:DropDownListGrid>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="center" Width="100px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Segundo Valor" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtSegundoValor" runat="server" Style="font-size: xx-small; text-align: left"
                                            Text='<%# Bind("segundo_valor") %>' Width="100px" CssClass="textbox" Visible="false" ReadOnly="true" />
                                        <cc1:DropDownListGrid
                                            ID="ddlSegundoValor" runat="server" AppendDataBoundItems="True" CommandArgument='<%#Container.DataItemIndex %>'
                                            CssClass="textbox" Style="font-size: xx-small; text-align: left" Width="100px" Visible="false" Enabled="false">
                                        </cc1:DropDownListGrid>
                                        <asp:UpdatePanel ID="upRecoger" runat="server">
                                            <ContentTemplate>
                                                <asp:HiddenField ID="hfValue" runat="server" Visible="false" />
                                                <asp:TextBox ID="txtRecoger" CssClass="textbox" runat="server" Visible="false" Width="131px" ReadOnly="True" Style="text-align: right" TEXTMODE="SingleLine"></asp:TextBox>
                                                
                                                <asp:Panel ID="panelLista" runat="server" Height="200px" Width="300px"
                                                    BorderStyle="Solid" BorderWidth="2px" Direction="LeftToRight"
                                                    ScrollBars="Auto" BackColor="#CCCCCC" Style="display: none">
                                                    <asp:GridView ID="gvRecoger" runat="server" Width="100%" AutoGenerateColumns="False"
                                                            HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" DataKeyNames="cod_act">

                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Código">
                                                                <itemtemplate>
                                                            <asp:Label ID="lbl_destino" runat="server" Text='<%# Bind("cod_act") %>'></asp:Label>
                                                            </itemtemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="50px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Descripción">
                                                                    <itemtemplate>
                                                            <asp:Label ID="lbl_descripcion" runat="server" Text='<%# Bind("nombre_act") %>'></asp:Label>
                                                            </itemtemplate>                                                        
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="gridIco">   
                                                                <ItemTemplate>
                                                                    <cc1:CheckBoxGrid ID="cbListado" runat="server" CommandArgument='<%#DataBinder.Eval(Container, "RowIndex") %>'
                                                                    AutoPostBack="false" />
                                                                </ItemTemplate>
                                                                <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                                            </asp:TemplateField>
                                                        </Columns>  
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </ContentTemplate>                                
                                        </asp:UpdatePanel>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="center" Width="100px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Valor Historico" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtValorHistorico" runat="server" Style="font-size: xx-small; text-align: left"
                                            Text='<%# Bind("valor_historico") %>' Width="100px" CssClass="textbox" Visible="false" ReadOnly="true" />
                                        <cc1:DropDownListGrid
                                            ID="ddlValorHistorico" runat="server" AppendDataBoundItems="True" CommandArgument='<%#Container.DataItemIndex %>'
                                            CssClass="textbox" Style="font-size: xx-small; text-align: left" Width="100px" Visible="false" Enabled="false">
                                        </cc1:DropDownListGrid>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="center" Width="100px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                        <asp:Label runat="server" ID="lblNoExisteSegmentoActual" Text="No existe Segmento Actual" Visible="false" Font-Size="Medium" Font-Bold="true" />
                    </td>
                    <td style="width: 50%;">
                        <asp:GridView runat="server" ID="gvSegmentoAnterior" HorizontalAlign="Center" Width="99%" AutoGenerateColumns="false"
                            OnRowDataBound="gvSegmentoAnterior_RowDataBoundA" Style="font-size: x-small">
                            <Columns>
                                <asp:TemplateField HeaderText="Descripción">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCondicion" runat="server" Text='<%# Bind("variable") %>' Visible="false"></asp:Label>
                                        <cc1:DropDownListGrid
                                            ID="ddlCondicion" runat="server" AppendDataBoundItems="True" CommandArgument='<%#Container.DataItemIndex %>'
                                            CssClass="textbox" Style="font-size: xx-small; text-align: left" Width="120px" Enabled="false">
                                        </cc1:DropDownListGrid>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="130" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Operador">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOperador" runat="server" Text='<%# Bind("operador") %>' Visible="false"></asp:Label>
                                        <cc1:DropDownListGrid
                                            ID="ddlOperador" runat="server" AppendDataBoundItems="True" CommandArgument='<%#Container.DataItemIndex %>'
                                            CssClass="textbox" Style="font-size: xx-small; text-align: left" Width="120px" Enabled="false">
                                        </cc1:DropDownListGrid>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="130px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Valor" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtValor" runat="server" Style="font-size: xx-small; text-align: left"
                                            Text='<%# Bind("valor") %>' Width="100px" CssClass="textbox" Visible="false" ReadOnly="true" />
                                        <cc1:DropDownListGrid
                                            ID="ddlValor" runat="server" AppendDataBoundItems="True" CommandArgument='<%#Container.DataItemIndex %>'
                                            CssClass="textbox" Style="font-size: xx-small; text-align: left" Width="100px" Visible="false" Enabled="false">
                                        </cc1:DropDownListGrid>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="center" Width="100px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Segundo Valor" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtSegundoValor" runat="server" Style="font-size: xx-small; text-align: left"
                                            Text='<%# Bind("segundo_valor") %>' Width="100px" CssClass="textbox" Visible="false" ReadOnly="true" />
                                        <cc1:DropDownListGrid
                                            ID="ddlSegundoValor" runat="server" AppendDataBoundItems="True" CommandArgument='<%#Container.DataItemIndex %>'
                                            CssClass="textbox" Style="font-size: xx-small; text-align: left" Width="100px" Visible="false" Enabled="false">
                                        </cc1:DropDownListGrid>
                                        <asp:UpdatePanel ID="upRecogerA" runat="server">
                                            <ContentTemplate>
                                                <asp:HiddenField ID="hfValueA" runat="server" Visible="false" />
                                                <asp:TextBox ID="txtRecogerA" CssClass="textbox" runat="server" Visible="false" Width="131px" ReadOnly="True" Style="text-align: right" TEXTMODE="SingleLine"></asp:TextBox>
                                                
                                                <asp:Panel ID="panelListaA" runat="server" Height="200px" Width="300px"
                                                    BorderStyle="Solid" BorderWidth="2px" Direction="LeftToRight"
                                                    ScrollBars="Auto" BackColor="#CCCCCC" Style="display: none">
                                                    <asp:GridView ID="gvRecogerA" runat="server" Width="100%" AutoGenerateColumns="False"
                                                            HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" DataKeyNames="cod_act">

                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Código">
                                                                <itemtemplate>
                                                            <asp:Label ID="lbl_destino" runat="server" Text='<%# Bind("cod_act") %>'></asp:Label>
                                                            </itemtemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="50px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Descripción">
                                                                    <itemtemplate>
                                                            <asp:Label ID="lbl_descripcion" runat="server" Text='<%# Bind("nombre_act") %>'></asp:Label>
                                                            </itemtemplate>                                                        
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="gridIco">   
                                                                <ItemTemplate>
                                                                    <cc1:CheckBoxGrid ID="cbListado" runat="server" CommandArgument='<%#DataBinder.Eval(Container, "RowIndex") %>'
                                                                    AutoPostBack="false" />
                                                                </ItemTemplate>
                                                                <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                                            </asp:TemplateField>
                                                        </Columns>  
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </ContentTemplate>                                
                                        </asp:UpdatePanel>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="center" Width="100px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Valor Historico" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtValorHistorico" runat="server" Style="font-size: xx-small; text-align: left"
                                            Text='<%# Bind("valor_historico") %>' Width="100px" CssClass="textbox" Visible="false" ReadOnly="true" />
                                        <cc1:DropDownListGrid
                                            ID="ddlValorHistorico" runat="server" AppendDataBoundItems="True" CommandArgument='<%#Container.DataItemIndex %>'
                                            CssClass="textbox" Style="font-size: xx-small; text-align: left" Width="100px" Visible="false" Enabled="false">
                                        </cc1:DropDownListGrid>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="center" Width="100px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                        <asp:Label runat="server" ID="lblNoExisteSegmentoAnterior" Text="No existe Segmento Anterior" Visible="false" Font-Size="Medium" Font-Bold="true" />
                    </td>
                </tr>
            </table>
            <br />
            <br />
            </div>
            <br />
            <hr style="width: 100%" />
            <table style="width: 100%">
                <tr>
                    <td colspan="4">
                        <asp:GridView ID="gvDetalle" runat="server" AllowPaging="True"
                            AutoGenerateColumns="False" PageSize="20"
                            ShowHeaderWhenEmpty="True" Width="100%" OnRowCreated="gvDetalle_RowCreated"
                            Style="font-size: x-small" DataKeyNames="consecutivo">
                            <Columns>
                                <asp:BoundField DataField="fechacierre" HeaderText="Fecha Cierre" DataFormatString="{0:d}" />
                                <%--<asp:BoundField DataField="codigopersona" HeaderText="Codigo Persona" Visible="false" />
                                <asp:BoundField DataField="identificacion_persona" HeaderText="Identificacion" Visible="false" />
                                <asp:BoundField DataField="nombre_persona" HeaderText="Nombre" Visible="false" />
                                <asp:BoundField DataField="cod_oficina" HeaderText="Codigo Oficina" Visible="false" />--%>
                                <asp:BoundField DataField="nombre_oficina" HeaderText="Nombre Oficina" />
                                <%--<asp:BoundField DataField="segmentoactual" HeaderText="Cod. Segm. Actual" Visible="false" />
                                <asp:BoundField DataField="nombre_segmentoactual" HeaderText="Nom. Segm. Actual" Visible="false" />
                                <asp:BoundField DataField="segmentoanterior" HeaderText="Cod. Segm. Anterior" Visible="false" />
                                <asp:BoundField DataField="nombre_segmentoanterior" HeaderText="Nom. Segm. Anterior" Visible="false" />--%>
                                <asp:BoundField DataField="segmentoaso" HeaderText="F.R.ASOCIADOS" />
                                <asp:BoundField DataField="Segmentopro" HeaderText="F.R.PRODUCTOS" />
                                <asp:BoundField DataField="Segmentocan" HeaderText="F.R.CANALES" />
                                <asp:BoundField DataField="Segmentojur" HeaderText="F.R.JURISDICCION" />
                                <asp:BoundField DataField="calificacion" HeaderText="Calificación" />
                                <asp:BoundField DataField="segmentoaso_anterior" HeaderText="F.R.ASOCIADOS" />
                                <asp:BoundField DataField="Segmentopro_anterior" HeaderText="F.R.PRODUCTOS" />
                                <asp:BoundField DataField="Segmentocan_anterior" HeaderText="F.R.CANALES" />
                                <asp:BoundField DataField="Segmentojur_anterior" HeaderText="F.R.JURISDICCION" />
                                <asp:BoundField DataField="calificacion_anterior" HeaderText="Calificación"/>                                
                                <asp:BoundField DataField="tipo_rol" HeaderText="Tipo Rol" />
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridHeader" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                        <asp:Label ID="Label5" runat="server" Visible="False" />
                        <asp:Label ID="Label6" runat="server" Text="Su consulta no obtuvo ningun resultado."
                            Visible="False" />
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="mvFinal" runat="server">
            <asp:Panel ID="PanelFinal" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <asp:Label ID="lblMensaje" runat="server" Text="Operación Realizada Correctamente"
                                Style="color: #FF3300"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnRegresarGuardar" runat="server" Text="Regresar" OnClick="btnRegresarGuardar_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
</asp:Content>
