<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Cobranzas & Moras :." %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
     <asp:ScriptManager runat="server"></asp:ScriptManager>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:Panel ID="pConsulta" runat="server">
                    <table id="tbCriterios" border="0" cellpadding="10" cellspacing="0" width="100%">
                        <tr>
                            <td style="text-align: center">Código Cliente          
                                <br />
                                <asp:TextBox ID="txtCliente" runat="server" CssClass="textbox" Width="150px"></asp:TextBox>
                            </td>
                            <td style="text-align: center">Identificación Cliente      
                                <br />
                                <asp:TextBox ID="txtIdentiCliente" runat="server" CssClass="textbox"
                                    Width="150px"></asp:TextBox>
                            </td>
                            <td class="tdI" style="text-align: center">Código de nómina<br />
                                <asp:TextBox ID="txtCodigoNomina" runat="server" CssClass="textbox" Width="150px" />
                            </td>
                            <td style="text-align: center">Calificación del cliente<br />
                                <asp:DropDownList ID="ddlCalificacion" runat="server" CssClass="dropdown"
                                    Width="150px">
                                    <asp:ListItem Value="0">Seleccione un Item</asp:ListItem>
                                </asp:DropDownList>
                            </td>




                      <%--      <td style="text-align: center">Tipo documento a Generar<br />
                                <asp:DropDownList ID="ddlTipoDocumento" runat="server"
                                    CssClass="textbox" Width="150px">
                                    <asp:ListItem Value="0"> Seleccione un Item</asp:ListItem>
                                    <asp:ListItem Value="1">Reporte Moras</asp:ListItem>
                                    <asp:ListItem Value="2">Carta Aviso</asp:ListItem>
                                    <asp:ListItem Value="3">Carta Codeudor</asp:ListItem>
                                    <%--<asp:ListItem Value="4">Prejuridico Codeudor</asp:ListItem>
                    <asp:ListItem Value="5">Juridico</asp:ListItem>
                    <asp:ListItem Value="6">Juridico Codeudor</asp:ListItem>
                    <asp:ListItem Value="7">Campaña</asp:ListItem>
                    <asp:ListItem Value="8">Citación</asp:ListItem>--%>
                                <%--</asp:DropDownList>
                            </td>--%>
                        </tr>
                        <tr>
                            <td style="text-align: center">Productos a Anexar<br />
                                <asp:DropDownList ID="ddltipoProducto" runat="server"
                                    CssClass="textbox" Width="150px">
                                    <asp:ListItem Value="0">Todos</asp:ListItem>
                                    <asp:ListItem Value="1">Creditos</asp:ListItem>
                                    <asp:ListItem Value="2">Aportes</asp:ListItem>
                                    <asp:ListItem Value="3">Servicios</asp:ListItem>
                                    <asp:ListItem Value="4">Afiliacion</asp:ListItem>
                                 </asp:DropDownList>
                            </td>
                             <td style="text-align: center">Dias de Mora<br />
                                        <asp:DropDownList ID="ddlMora" runat="server" Height="30px" CssClass="dropdown" Width="200px">
                                        <asp:ListItem Value="0">&lt;Seleccione un Item&gt;</asp:ListItem>
                                        <asp:ListItem Value="1 and 30">1-30</asp:ListItem>
                                        <asp:ListItem Value="31 and 60">31-60</asp:ListItem>
                                        <asp:ListItem Value="61 and 90">61-90</asp:ListItem>
                                        <asp:ListItem Value="91 and 120">91-120</asp:ListItem>
                                        <asp:ListItem Value="120 and 150">120-150</asp:ListItem>
                                        <asp:ListItem Value="151 and 180">151-180</asp:ListItem>
                                        <asp:ListItem Value="181 and 270">181-270</asp:ListItem>
                                        <asp:ListItem Value="271 and 360">271-360</asp:ListItem>
                                        <asp:ListItem>TODAS</asp:ListItem>
                                        </asp:DropDownList>
                                 </td>

                            <td style="text-align: center">Oficina<br />
                                <asp:DropDownList ID="ddlOficina" runat="server" CssClass="textbox"
                                    Width="150px">
                                </asp:DropDownList>
                                <br />
                            </td>

                             <td style="text-align: center">
                             <asp:Button ID="btnExportar" runat="server" CssClass="btn8"
                                Text="Exportar a excel" OnClick="btnExportar_Click" />
                                   </td>

                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <hr />
            </td>
        </tr>
        <tr>
            <td>
                <div id="Layer1" style="width: 100%; height: 600px; overflow: scroll;">
                    <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gvLista_RowDataBound" OnRowDeleting="gvLista_RowDeleting"
                        OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged" OnRowEditing="gvLista_RowEditing"
                        HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" DataKeyNames="IdPersona">
                        <Columns>
                            <asp:BoundField DataField="IdPersona" HeaderStyle-CssClass="gridColNo"
                                ItemStyle-CssClass="gridColNo">
                                <HeaderStyle CssClass="gridColNo"></HeaderStyle>
                                <ItemStyle CssClass="gridColNo"></ItemStyle>
                            </asp:BoundField>
                            <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_info.jpg" ToolTip="Consultar Detalle" />
                                </ItemTemplate>
                                <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                <ItemStyle CssClass="gridIco"></ItemStyle>
                            </asp:TemplateField>

                            <asp:TemplateField Visible="false">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkHeader" runat="server" AutoPostBack="true" OnCheckedChanged="Check_Clicked" />
                                </HeaderTemplate>

                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBoxgv" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="IdPersona" HeaderText="Còdigo Persona">
                                <ItemStyle HorizontalAlign="Left" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NumeroDocumento" HeaderText="Identificación">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CodigoNomina" HeaderText="Còdigo Nomina">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Oficina" HeaderText="Còdigo Oficina">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>                         
                            <asp:BoundField DataField="Nombres" HeaderText="Nombre">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CantCreditos" HeaderText="Creditos">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                              <asp:BoundField DataField="MoraCreditos" HeaderText="Mora Creditos">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>  
                             <asp:BoundField DataField="CantAportes" HeaderText="Aportes">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>

                             <asp:BoundField DataField="MoraAportes" HeaderText="Mora Aportes">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            
                            <asp:BoundField DataField="CantServicios" HeaderText="Servicios">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                               <asp:BoundField DataField="MoraServicios" HeaderText="Mora Servicios">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>

                            <asp:BoundField DataField="CantAfiliacion" HeaderText="Afiliacion">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                                          <asp:BoundField DataField="MoraAfiliacion" HeaderText="Mora Afiliacion">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Productos" HeaderText="Productos en Mora">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                        <PagerStyle CssClass="gridPager"></PagerStyle>
                        <RowStyle CssClass="gridItem"></RowStyle>
                    </asp:GridView>
                </div>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />

                </br>
            
                Generar carta:
                     <asp:DropDownList ID="ddlTipoDocumento" runat="server"
                                    CssClass="textbox"  >
                             <%--OnSelectedIndexChanged="ddlTipoDocumento_SelectedIndexChanged"--%>                                
                                    <asp:ListItem Value="1">Reporte Moras</asp:ListItem>
                                    <asp:ListItem Value="2">Carta 1</asp:ListItem>
                                    <asp:ListItem Value="3">Carta 2</asp:ListItem>                        
                     </asp:DropDownList>
                       
                &nbsp;<asp:Button ID="btnConsulta" runat="server" CssClass="btn8"
                    OnClick="btnGenerar_Click" Text="Generar Cartas" />
                &nbsp;&nbsp;
                

                  <rsweb:reportviewer id="RpviewInfo1" runat="server" font-names="Verdana" font-size="8pt"
                      height="600px" interactivedeviceinfos="(Colección)" waitmessagefont-names="Verdana"
                      waitmessagefont-size="14px" width="990px" enableviewstate="True" visible="false">
                  <LocalReport ReportPath="Page\Asesores\Gestion_Mora\ReporteMoras.rdlc">
                  </LocalReport>
                 </rsweb:reportviewer>

                    <rsweb:ReportViewer ID="RpviewInfo2" runat="server" Font-Names="Verdana" Font-Size="8pt"
                        Height="600px" InteractiveDeviceInfos="(Colección)" WaitMessageFont-Names="Verdana"
                        WaitMessageFont-Size="14px" Width="990px" EnableViewState="True" Visible="false">
                        <LocalReport ReportPath="Page\Asesores\Gestion_Mora\Carta_Aviso.rdlc">
                        </LocalReport>
                    </rsweb:ReportViewer>

            <rsweb:ReportViewer ID="RpviewInfo3" runat="server" Font-Names="Verdana" Font-Size="8pt"
                Height="600px" InteractiveDeviceInfos="(Colección)" WaitMessageFont-Names="Verdana"
                WaitMessageFont-Size="14px" Width="990px" EnableViewState="True" Visible="false">
                <LocalReport ReportPath="Page\Asesores\Gestion_Mora\Carta_Codeudor.rdlc">
                </LocalReport>
            </rsweb:ReportViewer>
            
           
        </tr>
        
    </table>



</asp:Content>