<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master"  AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../../../General/Controles/decimales.ascx" tagname="decimales" tagprefix="uc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table cellpadding="5" cellspacing="0" style="width: 100%">
         <tr>
            <td  class="tdI">
            <asp:Panel ID="pConsulta" runat="server">
                <table border="0" width="70%">
                <tr align="left">
                    <td> Número de Obligación<br/>
                            <asp:TextBox ID="txtNumeObl"  maxlength="20"  CssClass="textbox" 
                            runat="server" Width="192px"></asp:TextBox>
                    </td>
                    <td> Entidad<br/>
                         <asp:DropDownList ID="ddlEntidad" Width="174px" class="dropdown" runat="server" Height="23px">
                         </asp:DropDownList>
                    </td>
                    <td>
                       
                        Monto Solicitado
                        <br/>
                        <uc1:decimales ID="txtMotoSolicitado" maxLegnth="20" runat="server" CssClass="textbox" Width="143px" />
                   </td>
                </tr>
                </table>
            </asp:Panel>
            </td>
        </tr>
        <tr>
            <td align="center">
            </td>
        </tr>
        <tr>
            <td align="left">
            <div id="gvDiv">
                <asp:GridView ID="gvObCredito" runat="server"
                    AutoGenerateColumns="False" PageSize="20" BackColor="White" 
                    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                    ForeColor="Black" GridLines="Vertical" 
                    OnSelectedIndexChanged="gvObCredito_SelectedIndexChanged" 
                    onrowdeleting="gvObCredito_RowDeleting" DataKeyNames="codobligacion">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                                    ToolTip="Detalle" Width="16px" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                        </asp:TemplateField>
                         <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" 
                            ShowDeleteButton="True" />
                         <asp:BoundField DataField="codobligacion"  HeaderText="No Obligación" />
                         <asp:BoundField DataField="nomentidad"  HeaderText="Entidad" />
                        <asp:BoundField DataField="fecha_solicitud" DataFormatString="{0:d}" HeaderText="Fecha Solicitud" />
                          <asp:BoundField DataField="montosolicitud" HeaderText="Monto Solicitado" DataFormatString="{0:N0}">
                          <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="estadoobligacion" HeaderText="Estado" />
                    </Columns>
                    <FooterStyle BackColor="#CCCC99" />
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                    <RowStyle CssClass="gridItem" />
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#FBFBF2" />
                    <SortedAscendingHeaderStyle BackColor="#848384" />
                    <SortedDescendingCellStyle BackColor="#EAEAD3" />
                    <SortedDescendingHeaderStyle BackColor="#575357" />
                </asp:GridView>
                </div>
            </td>
        </tr>
        <tr>
            <td style="text-align:center">
                <asp:Label ID="lblTotalReg" runat="server" Visible="False" />
                <asp:Label ID="Label2" runat="server" Text="Su consulta no obtuvo ningun resultado."
                    Visible="False" />
            </td>
        </tr>
        </table>

        <uc4:mensajeGrabar ID="ctlMensaje" runat="server"/>

</asp:Content>





