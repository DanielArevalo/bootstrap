<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:panel id="pConsulta" runat="server">
            <br />
            <br />
        </asp:panel>

    <table style="width: 100%;">
        <tr>
            <td class="logo" style="width: 150px">No. Crédito:<br />
                <asp:textbox id="txtCredito" runat="server" cssclass="textbox" width="190px"></asp:textbox>
                <br />
                <asp:comparevalidator id="cvNoCredito" runat="server"
                    controltovalidate="txtCredito" display="Dynamic"
                    errormessage="Solo se admiten números" forecolor="Red" operator="DataTypeCheck"
                    setfocusonerror="True" type="Double" validationgroup="vgGuardar" />
            </td>
            <td class="logo" style="width: 200px">No. Identificación del cliente:<br />
                <asp:textbox id="txtIdentificacion" runat="server" cssclass="textbox"
                    width="190px"></asp:textbox>
                <br />
                <asp:comparevalidator id="cvIdentificacion" runat="server"
                    controltovalidate="txtIdentificacion" display="Dynamic"
                    errormessage="Solo se admiten números" forecolor="Red" operator="DataTypeCheck"
                    setfocusonerror="True" type="Double" validationgroup="vgGuardar" />
            </td>
            <td class="tdI" style="text-align: left">Código de nómina<br />
                <asp:textbox id="txtCodigoNomina" runat="server" cssclass="textbox" width="190px" />
            </td>
            <td style="text-align: left; width: 160px">
                <asp:label id="lbloficina" text="Oficina" runat="server" />
                <br />
                <asp:dropdownlist id="ddloficina" runat="server" cssclass="textbox"
                    width="90%" />
            </td>
            <td style="text-align: left; width: 160px">&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <hr style="width: 100%" />
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:gridview id="gvLista" runat="server" allowpaging="True"
                    autogeneratecolumns="False" gridlines="Horizontal"
                    pagesize="20"
                    showheaderwhenempty="True" width="97%"
                    onpageindexchanging="gvLista_PageIndexChanging"
                    onselectedindexchanged="gvLista_SelectedIndexChanged"
                    style="font-size: small">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" 
                                        ImageUrl="~/Images/gr_info.jpg" ToolTip="Detalle" Width="16px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="NumeroCredito" HeaderText="No. Crédito" />
                            <asp:BoundField DataField="Identificacion" HeaderText="Identificación" />
                            <asp:BoundField DataField="Nombres" HeaderText="Nombres" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="cod_nomina" HeaderText="Código de nómina" />
                            <asp:BoundField DataField="Monto" HeaderText="Monto" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n}" />
                            <asp:BoundField DataField="Cuota" HeaderText="Cuota" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n}" />
                            <asp:BoundField DataField="Plazo" HeaderText="Plazo" />
                            <asp:BoundField DataField="cod_linea_credito" HeaderText="Línea" />      
                            <asp:BoundField DataField="LineaCredito" HeaderText="Nombre de Línea" />    
                            <asp:BoundField DataField="oficina" HeaderText="Oficina" />                   
                        </Columns>
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                        <RowStyle CssClass="gridItem" />
                    </asp:gridview>
                <asp:label id="lblTotalRegs" runat="server" visible="False" />
                <asp:label id="lblInfo" runat="server" text="Su consulta no obtuvo ningun resultado."
                    visible="False" />
            </td>
        </tr>
    </table>
</asp:Content>
