<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="SimulacionCDAT.aspx.cs" Inherits="Pages_Ahorros_Simulacion_SimulacionCDAT" %>
<%@ Register Src="~/Controles/fecha.ascx" TagName="ctlFecha" TagPrefix="ctl" %>
<%@ Register Src="~/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/Controles/ctlTasa.ascx" TagName="tasa" TagPrefix="uc5" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .tableNormal
        {
            border-collapse: separate;
            border-spacing: 4px;
        }        
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:Button ID="btnConsultar" CssClass="btn btn-primary" Style="padding: 3px 15px; border-radius: 0px; width: 110px" OnClientClick="return controlClickeoLocosDesactivarBoton();" runat="server" Text="Consultar" OnClick="btnConsultar_Click" />                
     <asp:Button ID="btnLimpiar" CssClass="btn btn-primary" Style="padding: 3px 15px; border-radius: 0px; width: 110px" OnClientClick="return controlClickeoLocosDesactivarBoton();" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" />
     <br />
     <br />
     <asp:UpdatePanel ID="updLineas" runat="server">
        <ContentTemplate>
    <div class="form-group" style="padding:0px">
        <table style="width:50%; padding:5px 50px 5px 50px" class="tableNormal">
           
       <tr style="text-align:left;">
           <td>
               Fecha simulación:
            </td>
           <td>
                <ctl:ctlFecha ID="txtFecha" runat="server" Width_="100%" Enabled="false"/>
          </td>
           <tr style="text-align:left;">
                <td>
                    Línea CDAT : 
                     </td>
                <td>
                      <asp:DropDownList ID="ddlLineaCDAT" runat="server" CssClass="form-control" AutoPostBack="true" 
                                    onselectedindexchanged="ddlTipoLinea_SelectedIndexChanged"></asp:DropDownList>
               </td>
            </tr>
            <tr style="text-align:left;">
                <td>
                    Plazo: 
                     </td>
                <td>                    
                    <asp:DropDownList ID="ddlCPlazo" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                        <asp:ListItem Selected="True" Value="0">Seleccione un item</asp:ListItem>
                    </asp:DropDownList> 
               </td>
            </tr>
           <tr style="text-align:justify;">
               <td>Valor:
               </td>
               <td>
                   <asp:TextBox ID="txtValor" runat="server" Width="100%" Class="form-control" onkeypress="return ValidaNum(event);" onblur="valorCambio(event)"></asp:TextBox>
               </td>
           </tr>
                   <asp:Panel ID="panelTasa" Enabled="false" runat="server">                       
           <tr style="text-align:justify;">
               <td>
                   <uc5:tasa ID="ctlTasa" runat="server" Visible="false" />
               </td>
           </tr>
            <tr style="text-align:justify;">
                <td>
                    Tasa (NA)
                </td>
                <td>
                    <asp:TextBox ID="txtCTasa" enabled="false" runat="server" CssClass="form-control noEditable" />
                </td>
            </tr>
            <tr style="text-align:justify;">
                <td>
                    Tasa (EA)
                </td>
                <td>
                    <asp:TextBox ID="txtCTasaEA" enabled="false" runat="server" CssClass="form-control noEditable" />
                </td>
            </tr>
                    </asp:Panel>
           <tr>
               <td style="text-align: left; width: 500px" colspan="2">
                   <%--Tipo Tasa Interes<br />
                                    <asp:RadioButtonList ID="rblTipoTasa" runat="server" RepeatDirection="Horizontal"
                                        AutoPostBack="True" OnSelectedIndexChanged="rblTipoTasa_SelectedIndexChanged">
                                        <asp:ListItem Value="Fijo">Fijo</asp:ListItem>
                                        <asp:ListItem Value="Vari">Variable</asp:ListItem>
                                    </asp:RadioButtonList>--%>
               </td>
           </tr>
           <asp:Panel runat="server" Visible="false">
               <tr style="text-align:left;">
               <td>Periodicidad Intereses:
               </td>
               <td>
                   <asp:DropDownList ID="ddlPeriodicidad" runat="server" CssClass="form-control"></asp:DropDownList>
               </td>
           </tr>
           </asp:Panel>           
       </tr>
            
             
        </table> 
        <table style="width:100%; padding:5px 50px 5px 50px" class="tableNormal">
         
                            
         

        </table>
        <asp:panel ID="pnlCapitalizacion" runat="server" Visible="false">
            <table style="width:100%; padding:5px 50px 5px 50px" class="tableNormal">
                <tr><%-- ddlModalidad_SelectedIndexChanged--%>
                    <td style="text-align: left; width: 150px;">
                                    <td style="text-align: left; width: 292px;">
                                        <br />
                                        <asp:CheckBox ID="chkCapitalizaInt" runat="server" Text="Capitaliza Interes" Checked="false" />
                                    </td>
                                    <td style="text-align: left; width: 351px;">
                                        <br />
                                        <asp:CheckBox ID="chkCobraReten" runat="server" Text="Cobra Retención" Checked="true" />
                                    </td>
                </tr>
            </table>
        </asp:panel>
    <%--OnPageIndexChanging="gvLista_PageIndexChanging"--%>
                <div class="col-sm-12">
                    <hr style="width: 100%; border-color: #2780e3;" />
                </div>
                <div class="col-sm-12">
                    <asp:Panel ID="pnlSimulacion" runat="server">
                        <div style="overflow: scroll; max-width: 100%;">
                           <asp:GridView ID="gvDetalle" runat="server" GridLines="Horizontal" AutoGenerateColumns="False"
                                    AllowPaging="True" PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                                    RowStyle-CssClass="gridItem" DataKeyNames="codigo_cdat,estado" Width="750px"
                                    HorizontalAlign="Center" CssClass="table">
                               <Columns>
                                        <asp:BoundField DataField="fecha_intereses" DataFormatString="{0:d}" HeaderText="Fecha">
                                            <ItemStyle HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Interés" DataField="intereses_cap" DataFormatString="{0:C0}">
                                            <ItemStyle HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="retencion_cap" HeaderText="Retención" DataFormatString="{0:C0}">
                                            <ItemStyle HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="capitalizar_int" DataFormatString="{0:n}" HeaderText="Valor a capitalizar" Visible="false">
                                            <ItemStyle HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="valor" DataFormatString="{0:C0}" HeaderText="Rendimiento">
                                            <ItemStyle />
                                        </asp:BoundField>
                                    </Columns>                                
                                    <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                                    <PagerStyle CssClass="gridPager"></PagerStyle>
                                    <RowStyle CssClass="gridItem"></RowStyle>
                            </asp:GridView>
                        </div>
                        <div style="text-align: center; width: 100%;">
                            <asp:Label ID="lblTotReg" runat="server" Visible="false" />
                            <asp:Label ID="lblInfo" runat="server" Visible="false" Text="Su consulta no obtuvo ningún resultado." />
                        </div>
                    </asp:Panel>
                </div>
         </div>
        </ContentTemplate>
    </asp:UpdatePanel>
   
</asp:Content>

