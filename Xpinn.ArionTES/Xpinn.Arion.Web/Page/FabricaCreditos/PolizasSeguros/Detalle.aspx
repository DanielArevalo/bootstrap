<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Detalle.aspx.cs" Inherits="Detalle" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <table style="width: 785px; font-size: x-small; height: 981px;">
        <tr>
            <td class="align-rt" colspan="8" style="font-size: medium; background-color: #99CCFF;
                height: 21px; text-align: center;">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
            </td>
        </tr>
        <tr>
            <td style="font-size: x-small; height: 25px; text-align: center; width: 10px;">
                <div style="width: 100px">
                    Código Póliza</div>
            </td>
            <td style="font-size: x-small; height: 25px; text-align: left; width: 246px;">
                <strong>
                <asp:Label ID="lblcodpoliza" runat="server" Style="font-size: large" 
                    Enabled="False"></asp:Label>
                </strong>
            </td>
            <td style="font-size: x-small; height: 25px; text-align: left; width: 246px;">
                &nbsp;</td>
            <td style="font-size: x-small; height: 25px; text-align: left; width: 7px;">
                <div style="width: 150px; text-align: center;">
                    No. Radicación</div>
            </td>
            <td style="font-size: x-small; height: 25px; text-align: left; ">
                <asp:TextBox ID="TxtNumRadicacion" class="numeric" runat="server" 
                    Style="font-size: x-small" Width="109px" Enabled="False"></asp:TextBox>
            </td>
            <td style="font-size: x-small; height: 25px; text-align: left; width: 50px;">
                <div style="width: 114px">
                    &nbsp;Tipo&nbsp; Identificación</div>
            </td>
            <td style="font-size: x-small; height: 25px; text-align: left; width: 200px;">
                <asp:TextBox ID="TxtTipoIden" runat="server" style="font-size: x-small" 
                    Width="97px" Enabled="False"></asp:TextBox>
            </td>
            <td style="font-size: x-small; height: 25px; text-align: left; width: 202px;" 
                >
                <strong>
                <asp:TextBox ID="Txtcodasegurado" runat="server" 
                    Style="font-size: x-small; margin-bottom: 0px;" Width="16px"
                    Enabled="False" Visible="False"></asp:TextBox>
                <asp:TextBox ID="Txtcodasesor" runat="server" Style="font-size: x-small" Width="23px"
                    Enabled="False" Visible="False"></asp:TextBox>
                <asp:TextBox ID="Txtindividual" runat="server" style="font-size: x-small" 
                    Width="20px" Enabled="False" Visible="False"></asp:TextBox>
                </strong>
            </td>
        </tr>
        <tr>
            <td style="font-size: x-small; height: 5px; text-align: center; width: 10px;" 
               >
                <div style="width: 100px">
                    &nbsp;Identificación</div>
            </td>
            <td style="font-size: x-small; height: 5px; text-align: left; width: 246px;" 
               >
                <asp:TextBox ID="TxtIdentificacion"     runat="server" 
                    Style="font-size: x-small" Enabled="False" Width="135px" Height="22px"></asp:TextBox>
            </td>
            <td style="font-size: x-small; height: 5px; text-align: left; width: 246px;" 
               >
                &nbsp;</td>
            <td style="font-size: x-small; height: 5px; text-align: left; width: 7px;" >
                <div style="width: 150px; text-align: center;">
                    Nombres</div>
            </td>
            <td style="font-size: x-small; height: 5px; text-align: left; " colspan="4" >
                <asp:TextBox ID="TxtNombres" runat="server" style="font-size: x-small; margin-left: 0px; text-align: left;" 
                    Width="434px" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="align-rt" colspan="2" style="font-size: medium; background-color: #99CCFF;
                height: 21px; text-align: center;">
                <strong>INFORMACIÓN DE LA PÓLIZA</strong></td>
            <td class="align-rt" style="font-size: medium; background-color: #99CCFF;
                text-align: center;" rowspan="9">
                 &nbsp;</td>
            <td class="align-rt" colspan="5" style="font-size: medium; background-color: #99CCFF;
                height: 21px; text-align: center;">
                <strong>DATOS DEL ASEGURADO PRINCIPAL </strong>
            </td>
        </tr>
        <tr>
            <td style="font-size: x-small; height: 25px; text-align: center; width: 10px;">
                <div style="width: 132px">
                Número Póliza 
                </div>
            </td>
            <td style="font-size: x-small; height: 25px; text-align: left; width: 246px;">
                <asp:TextBox ID="TxtNumPoliza"  runat="server" Width="134px" MaxLength="20" 
                      Enabled="False" Height="22px"></asp:TextBox>
            </td>
            <td  
                style="font-size: x-small; height: 4px; width: 7px; text-align: left;">
                <div style="width: 150px; text-align: center; height: 27px;">
                    Nombres-Apellidos Asegurado Principal 
                </div>
            </td>
            <td  
                style="font-size: x-small; height: 4px; text-align: left;" colspan="2">
                <asp:TextBox ID="TxtNomAsegu" runat="server" Style="font-size: x-small" Width="200px"
                    Enabled="False" Height="22px"></asp:TextBox>
            </td>
            <td style="font-size: x-small; height: 25px; text-align: center; width: 200px;">
                <div style="width: 83px; height: 17px;">
                    C.C óNit</div>
            </td>
            <td 
                style="height: 4px; text-align: left; width: 202px;">
                <asp:TextBox ID="TxtIdenAsegur" runat="server" Style="font-size: x-small; text-align: left;"
                    Width="100px" Enabled="False" Height="22px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td 
                style="font-size: x-small; height: 4px; width: 10px;" 
                >
                <div style="width: 132px; text-align: center;">
                    Oficina<br />
                </div>
            </td>
            <td 
                style="font-size: x-small; height: 4px; text-align: left; " 
                >
                <div style="width: 136px; text-align: center;">
                    Identificación Asesor</div>
            </td>
            <td 
                style="font-size: x-small; height: 4px; text-align: left; width: 7px;" rowspan="2" 
                >
                <div style="width: 150px; text-align: center; height: 27px;">
                    Fecha Nacimiento (Dia/Mes/año) 
                </div>
            </td>
            <td 
                style="font-size: x-small; height: 4px; text-align: left; " colspan="2" rowspan="2" 
                >
                <asp:TextBox ID="TxtFechaNac" runat="server" Style="font-size: x-small" Width="200px"
                    Enabled="False" Height="22px"></asp:TextBox>
            </td>
            <td class="logo" 
                style="font-size: x-small; height: 4px; width: 200px; text-align: center;" 
                rowspan="2">
                <div style="width: 83px; ">
                    Estado Civil</div>
            </td>
            <td  
                style="font-size: x-small; height: 4px; text-align: left; width: 202px;" 
                rowspan="2">
                <asp:TextBox ID="TxtEstadoCivil" runat="server" Style="font-size: x-small" Width="100px"
                    Enabled="False" Height="22px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td 
                style="font-size: x-small; height: 4px; width: 10px;" 
                >
                <div style="width: 132px; text-align: center;">
                    <asp:TextBox ID="TxtOficina" runat="server" Style="font-size: x-small; text-align: left;"
                    Width="103px" Enabled="False" Height="22px"></asp:TextBox>
                </div>
            </td>
            <td 
                style="font-size: x-small; height: 4px; text-align: left; " 
                >
                <asp:TextBox ID="TxtIdenAsesor" runat="server" Style="font-size: x-small" Width="126px"
                    Enabled="False" Height="22px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td 
                style="font-size: x-small; height: 4px; text-align: center; width: 10px;" 
                >
                <div style="width: 132px; ">
                    Nombre Asesor</div>
            </td>
            <td 
                style="font-size: x-small; height: 4px; text-align: left; width: 246px;" 
                >
                <asp:TextBox ID="TxtNomAsesor" runat="server" Height="22px" Style="text-align: left;
                    font-size: x-small" Width="231px" Enabled="False"></asp:TextBox>
            </td>
            <td 
                style="font-size: x-small; height: 4px; text-align: left; width: 7px;" 
                >
                <div style="width: 150px; text-align: center; height: 27px;">
                Ocupación y/o Profesión Principal
                </div>
            </td>
            <td 
                style="font-size: x-small; height: 4px; text-align: left; " colspan="2" 
                >
                <asp:TextBox ID="TxtOcupacion" runat="server" Width="200px" Style="font-size: x-small;
                    margin-left: 0px;" Enabled="False" Height="22px"></asp:TextBox>
            </td>
            <td class="logo" 
                style="font-size: x-small; height: 4px; width: 200px; text-align: center;">
                <div style="width: 89px; text-align: center;">
                    Sexo</div>
            </td>
            <td class="logo" 
                style="font-size: x-small; height: 4px; text-align: left; width: 202px;">
                <asp:TextBox ID="TxtSexo" runat="server" Style="font-size: x-small" Width="100px"
                    Enabled="False" Height="22px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td 
                
                style="font-size: x-small; height: 4px; text-align: center; width: 10px;">
                <div style="width: 132px; ">
                    Tomador del seguro</div>
            </td>
            <td 
                
                style="font-size: x-small; height: 4px; text-align: left; width: 246px;">
                <asp:TextBox ID="TxtTomadorSeg" runat="server" Style="font-size: x-small; text-align: left;"
                    Width="231px" Enabled="False" Height="22px"></asp:TextBox>
            </td>
            <td style="height: 4px; text-align: left; width: 7px;">
                <div style="width: 150px; text-align: center; height: 27px;">
                Dirección Residencia
                </div>
            </td>
            <td style="height: 4px; text-align: left; " colspan="2">
                <asp:TextBox ID="TxtDireccion" runat="server" Width="200px" Style="font-size: x-small"
                    Enabled="False" Height="22px"></asp:TextBox>
            </td>
            <td style="height: 4px; text-align: center; width: 200px;">
                <div style="width: 90px; text-align: center;">
                    Actividades</div>
            </td>
            <td 
                style="height: 4px; text-align: left; width: 202px;">
                <asp:TextBox ID="TxtActividades" runat="server" Style="font-size: x-small" Width="100px"
                    Enabled="False" Height="22px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td 
                
                
                style="font-size: x-small; height: 19px; text-align: center; width: 10px;">
                <div style="width: 132px; ">
                    Nit</div>
            </td>
            <td 
                
                
                style="font-size: x-small; height: 19px; text-align: left; width: 246px;">
                <asp:TextBox ID="TxtNit" runat="server" 
                    Style="font-size: x-small; text-align: left;" Width="148px"
                    Enabled="False" Height="22px"></asp:TextBox>
            </td>
            <td 
                
                style="font-size: x-small; height: 19px; text-align: center; width: 7px;">
                <div style="width: 150px; text-align: center; height: 27px;">
                    Dirección Electrónica 
                </div>
            </td>
            <td 
                
                style="font-size: x-small; height: 19px; text-align: left; " colspan="2">
                <asp:TextBox ID="TxtEmail" runat="server" Width="200px" Style="font-size: x-small"
                    Enabled="False" Height="22px"></asp:TextBox>
            </td>
            <td style="height: 19px; text-align: center; width: 200px;">
                <div style="width: 89px; text-align: center;">
                    Ciudad</div>
            </td>
            <td 
                style="height: 19px; text-align: left; width: 202px;">
                <asp:TextBox ID="TxtCiudad" runat="server" Width="100px" Style="font-size: x-small"
                    Enabled="False" Height="22px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td 
                
                
                style="font-size: x-small; height: 19px; text-align: center; width: 10px;">
                <div style="width: 132px">
                Vigencia&nbsp; 
                Desde Hasta</div>
            </td>
            <td 
                
                
                style="font-size: x-small; height: 19px; text-align: left; width: 246px;">
                <asp:TextBox ID="TxtVigenPolDesde" runat="server" CssClass="textbox" 
                    Height="22px" Width="90px" Enabled="False"></asp:TextBox>
                <asp:CalendarExtender ID="TxtVigenPolDesde_CalendarExtender" runat="server"
                    Format="dd/MM/yyyy" TargetControlID="TxtVigenPolDesde">
                </asp:CalendarExtender>
                <asp:MaskedEditExtender ID="TxtVigenPolDesde_MaskedEditExtender" 
                    runat="server"  TargetControlID="TxtVigenPolDesde" Mask="99/99/9999" 
                    clearmaskonlostfocus="false" MaskType="None" UserDateFormat="MonthDayYear">
                </asp:MaskedEditExtender>
                <asp:TextBox ID="TxtVigenPolHasta" runat="server" CssClass="textbox" 
                    Height="22px" Width="90px" Enabled="False"></asp:TextBox>
                <asp:CalendarExtender ID="TxtVigenPolHasta_CalendarExtender" runat="server" Enabled="True"
                    Format="dd/MM/yyyy" TargetControlID="TxtVigenPolHasta">
                </asp:CalendarExtender>
                <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="TxtVigenPolHasta"
                  Mask="99/99/9999" ClearMaskOnLostFocus="false" MaskType="None" UserDateFormat="MonthDayYear">
                </asp:MaskedEditExtender>
                <br />
                <asp:RequiredFieldValidator ID="rfvFecha" runat="server" ControlToValidate="TxtVigenPolHasta"
                    Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                    ValidationGroup="vgGuardar" style="font-size: xx-small" />
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtVigenPolDesde"
                    Display="Dynamic"  ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                    ValidationGroup="vgGuardar" style="font-size: xx-small" />
            </td>
            <td 
                
                style="font-size: x-small; height: 19px; text-align: center; width: 7px;">
                <div style="width: 150px; text-align: center; height: 27px;">
                    Celular 
                </div>
            </td>
            <td 
                
                style="font-size: x-small; height: 19px; text-align: left; " colspan="2">
                <asp:TextBox ID="TxtCelular" runat="server" Width="200px" Style="font-size: x-small"
                    Enabled="False" Height="22px"></asp:TextBox>
            </td>
            <td style="height: 19px; text-align: center; width: 200px;">
                <div style="width: 84px; text-align: center;">
                    Teléfono</div>
            </td>
            <td 
                style="height: 19px; text-align: left; width: 202px;">
                <asp:TextBox ID="TxtTelefono" runat="server" Width="100px" Style="font-size: x-small"
                    Enabled="False" Height="22px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td 
                
                
                style="font-size: x-small; height: 19px; text-align: center; width: 10px;">
                <div style="width: 132px; ">
                    Valor Prima</div>
            </td>
            <td 
                
                
                style="font-size: x-small; height: 19px; width: 246px;">
                <asp:TextBox class="numeric" ID="TxtValorPrimaMensual" runat="server" Style="font-size: x-small;
                    text-align: left;" Width="139px" Enabled="False" Height="22px">0</asp:TextBox>
            </td>
            <td 
                
                style="font-size: x-small; height: 19px; text-align: center; width: 7px;">
                &nbsp;</td>
            <td 
                
                style="font-size: x-small; height: 19px; text-align: center; " colspan="2">
                &nbsp;</td>
            <td style="height: 19px; text-align: center; width: 200px;">
                &nbsp;</td>
            <td 
                style="height: 19px; text-align: left; width: 202px;">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="space" colspan="8" style="font-size: x-small; background-color: #99CCFF;
                height: 15px; text-align: center;">
            </td>
        </tr>
        <tr>
            <td style="font-size: x-small; text-align: center; height: 4px;" colspan="6">
            </td>
            <td style="font-size: x-small; text-align: center; height: 4px;" colspan="2">
                <asp:DropDownList ID="ddlTipoPlanSegVida" runat="server" Height="21px" 
                    Style="font-size: x-small; text-align: center; margin-left: 0px;" 
                    Width="158px" AutoPostBack="True" Enabled="False">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="font-size: x-small; text-align: center; " colspan="6" 
                rowspan="6">
                <asp:accordion ID="Accordion3" runat="server" 
                    FadeTransitions="True" 
                    FramesPerSecond="50" 
                    Width="753px" 
                    TransitionDuration="200" 
                    HeaderCssClass="accordionCabecera" 
                    ContentCssClass="accordionContenido" Height="199px" CssClass="master" 
                    BorderColor="#0033CC">
                    <Panes>
                        <asp:AccordionPane ID="AccordionPane3" runat="server" ContentCssClass="" 
                            HeaderCssClass="">
                            <Header>
                                VIDA GRUPO VOLUNTARIO- AMPAROS Y VALORES ASEGURADOS 
                            </Header>
                            <Content>
                                <asp:GridView ID="gvVidaGrupo" runat="server" Width="99%" ShowHeaderWhenEmpty="true"
                                    EmptyDataText="No se encontraron registros." AutoGenerateColumns="False" AllowPaging="True"
                                    PageSize="4" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                                    CellPadding="4" ForeColor="Black" GridLines="Vertical" Height="16px" Style="text-align: left;
                                    font-size: small">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField="descripcion" HeaderText="Amparo" />
                                        <asp:BoundField DataField="valor_cubierto" HeaderText="Valor" />
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
                            </Content>
                        </asp:AccordionPane>
                    </Panes>
                </asp:accordion>
            </td>
            <td style="font-size: x-small; text-align: center; height: 4px;" colspan="2" 
                class="space">
                <asp:CheckBox ID="chkprimaindividual" runat="server" AutoPostBack="True" 
                    Width="158px" Enabled="False" />
            </td>
        </tr>
        <tr>
            <td style="font-size: x-small; text-align: center; height: 4px;" colspan="2" 
                class="space">
                Valor prima Mensual Individual</td>
        </tr>
        <tr>
            <td style="font-size: x-small; text-align: center; height: 4px;" colspan="2" 
                class="space">
                <asp:TextBox ID="txtPrimaindividual" runat="server" Width="158px" 
                    Enabled="False" style="text-align: center" Height="22px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="font-size: x-small; text-align: center; height: 4px;" colspan="2" 
                class="space">
                <asp:CheckBox ID="chkprimacony" runat="server" AutoPostBack="True" 
                    Width="158px" Enabled="False" />
            </td>
        </tr>
        <tr>
            <td style="font-size: x-small; text-align: center; height: 4px;" colspan="2" 
                class="space">
                Valor prima mensual 
                cónyuge&nbsp;o<br />&nbsp;Primera pérdida 
            </td>
        </tr>
        <tr>
            <td style="font-size: x-small; text-align: center; height: 4px;" colspan="2" 
                class="space">
                <asp:TextBox ID="txtPrimacony" runat="server" Width="158px" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="space" colspan="8" style="font-size: x-small; background-color: #99CCFF; height: 9px; text-align: center;">
            </td>
        </tr>
        <tr>
            <td class="align-rt" style="font-size: x-small; text-align: center;" 
                colspan="6" rowspan="7">
                <asp:accordion ID="Accordion2" runat="server" 
                    FadeTransitions="True" 
                    FramesPerSecond="50" 
                    Width="756px" 
                    TransitionDuration="200" 
                    HeaderCssClass="accordionCabecera" 
                    ContentCssClass="accordionContenido" Height="224px">
                    <Panes>
                        <asp:AccordionPane ID="AccordionPane2" runat="server">
                            <Header>
                                ACCIDENTES PERSONALES  AMPAROS Y VALORES ASEGURADOS 
                            </Header>
                            <Content>
                                <asp:GridView ID="gvAccPers" runat="server" Width="99%" ShowHeaderWhenEmpty="true"
                                    EmptyDataText="No se encontraron registros." AutoGenerateColumns="False" AllowPaging="True"
                                    PageSize="4" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                                    CellPadding="4" ForeColor="Black" GridLines="Vertical" Height="16px"
                                    Style="font-size: small">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField="descripcion" HeaderText="Amparo" />
                                        <asp:BoundField DataField="valor_cubierto" HeaderText="Valor Cubierto Princ." />
                                        <asp:BoundField DataField="valor_cubierto_conyuge" HeaderText="Valor Cubierto Cony." />
                                        <asp:BoundField DataField="valor_cubierto_hijos" HeaderText="Valor Cubierto Hijos" />
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
                            </Content>
                        </asp:AccordionPane>
                    </Panes>
                </asp:accordion>
            </td>
            <td class="align-rt" style="font-size: x-small; text-align: center; height: 4px;" 
                colspan="2">
                <asp:DropDownList ID="ddlTipoPlanAcc" runat="server" Height="27px" Style="font-size: x-small"
                    Width="158px" AutoPostBack="True" Enabled="False">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="align-rt" style="font-size: x-small; text-align: center; height: 4px;" 
                colspan="2">
                <asp:CheckBox ID="chkprimaacc" runat="server" AutoPostBack="True" 
                    Width="158px" Enabled="False" />
            </td>
        </tr>
        <tr>
            <td class="align-rt" style="font-size: x-small; text-align: center; height: 4px;" 
                colspan="2">
                
                Prima Mensual
                Op. Indiv(Asegurado 
                <br />Principal) 
            </td>
        </tr>
        <tr>
            <td class="align-rt" style="font-size: x-small; text-align: center; height: 4px;" 
                colspan="2">
                <asp:TextBox ID="txtPrimaacc" runat="server" Width="158px" Enabled="False" 
                    style="text-align: center; margin-left: 0px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="align-rt" style="font-size: x-small; text-align: center; height: 3px;" 
                colspan="2">
                <asp:CheckBox ID="chkprimaopfam" runat="server" AutoPostBack="True" 
                    Width="158px" Enabled="False"/>
            </td>
        </tr>
        <tr>
            <td class="align-rt" style="font-size: x-small; text-align: center; height: 4px;" 
                colspan="2">
                Prima Mensual Op.Fam(Asegurado
                principal,Cónyuge
                y hasta 3 hijos)</td>
        </tr>
        <tr>
            <td class="align-rt" style="font-size: x-small; text-align: center; height: 4px;" 
                colspan="2">
                <asp:TextBox ID="txtPrimaopfam" runat="server" Width="158px" Enabled="False" 
                    style="text-align: center"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="align-rt" style="font-size: x-small; text-align: center; height: 45px;" 
                colspan="8">
                 <asp:accordion ID="Accordion1" runat="server" 
                    FadeTransitions="True" 
                    FramesPerSecond="50" 
                    Width="950px" 
                    TransitionDuration="200" 
                    HeaderCssClass="accordionCabecera" 
                    ContentCssClass="accordionContenido" Height="476px"> 
                    <Panes> 
                    <asp:AccordionPane ID="AccordionPane7" runat="server"> 
                        <Header>INFORMACIÓN DE FAMILIARES A TITULO GRATUITO</Header> 
                        <Content>
                            <asp:GridView ID="gvBeneficiarios" runat="server" Width="100%" 
                                ShowHeaderWhenEmpty = "True" EmptyDataText = "No se encontraron registros."
                                autoGenerateColumns="False" PageSize="20" BackColor="White" 
                                BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                                ForeColor="Black" GridLines="Vertical" 
                                DataKeyNames="consecutivo" 
                                onrowcancelingedit="gvBeneficiarios_RowCancelingEdit" 
                                onrowdeleted="gvBeneficiarios_RowDeleted" 
                                onrowdeleting="gvBeneficiarios_RowDeleting" 
                                onrowediting="gvBeneficiarios_RowEditing" 
                                onrowupdated="gvBeneficiarios_RowUpdated" 
                                onrowupdating="gvBeneficiarios_RowUpdating" 
                                onselectedindexchanged="gvBeneficiarios_SelectedIndexChanged" 
                                onrowcommand="gvBeneficiarios_RowCommand" ShowFooter="True">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Nombres y Apellidos">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtnombresyapellidos" runat="server" 
                                                Text='<%# Bind("nombres") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblnombresyapellidos" runat="server" 
                                                Text='<%# Bind("nombres") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Documento Identidad">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtdocumentoidentidad" runat="server" 
                                                Text='<%# Bind("identificacion") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbldocumentoidentidad" runat="server" 
                                                Text='<%# Bind("identificacion") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Parentesco">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtparentesco" runat="server" 
                                                Text='<%# Bind("parentesco") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblparentesco" runat="server" 
                                                Text='<%# Bind("parentesco") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fecha Nacimiento">
                                        <EditItemTemplate>
                                        <asp:TextBox ID="txtfechanac0" runat="server" CssClass="textbox" 
                                                Text='<%# String.Format("{0:MM/dd/yyyy}", Eval("fecha_nacimiento")) %>' 
                                                Height="16px"></asp:TextBox>
                                            <asp:CalendarExtender ID="txtfechanac_CalendarExtender" runat="server" Enabled="True"
                                                Format="MM/dd/yyyy" TargetControlID="txtfechanac">
                                            </asp:CalendarExtender>
                                            <br />                               
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                           <asp:Label ID="lblfechanac" runat="server" Text='<%# String.Format("{0:MM/dd/yyyy}", Eval("fecha_nacimiento")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="%">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtporcentaje" runat="server" 
                                                Text='<%# Bind("porcentaje") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblporcentaje" runat="server" 
                                                Text='<%# Bind("porcentaje") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="gridHeader" />
                                <HeaderStyle CssClass="gridHeader" />
                                <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                <RowStyle CssClass="gridItem" />
                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                <SortedAscendingHeaderStyle BackColor="#848384" />
                                <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                <SortedDescendingHeaderStyle BackColor="#575357" />
                            </asp:GridView>                          
                        </Content> 
                    </asp:AccordionPane>         
                    <asp:AccordionPane ID="AccordionPane1" runat="server"> 
                        <Header>SEGURADOS CUBIERTOS EN OPCIÓN FAMILIAR PARA ACCIDENTES PERSONALES (SOLO CONYUGE Y 3 HIJOS)</Header> 
                        <Content>        
                            <asp:GridView ID="gvFamiliares" runat="server" Width="100%" 
                               ShowHeaderWhenEmpty = "True" EmptyDataText = "No se encontraron registros."
                                AutoGenerateColumns="False" PageSize="5" BackColor="White" 
                                BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                                ForeColor="Black" GridLines="Vertical" Height="16px" 
                                onrowediting="gvFamiliares_RowEditing" DataKeyNames="consecutivo" 
                                onrowcancelingedit="gvFamiliares_RowCancelingEdit" 
                                onrowdeleted="gvFamiliares_RowDeleted" onrowdeleting="gvFamiliares_RowDeleting" 
                                onrowupdated="gvFamiliares_RowUpdated" onrowupdating="gvFamiliares_RowUpdating" 
                                onselectedindexchanged="gvFamiliares_SelectedIndexChanged" 
                                onrowcommand="gvFamiliares_RowCommand" ShowFooter="True">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Nombres y Apellidos">
                                        <ItemTemplate>
                                            <asp:Label ID="lblnombresyapellidos0" runat="server" 
                                                Text='<%# Bind("nombres") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtnombresyapellidos0" runat="server" 
                                                Text='<%# Bind("nombres") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Documento Identidad">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldocumentoidentidad0" runat="server" 
                                                Text='<%# Bind("identificacion") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtdocumentoidentidad0" runat="server" 
                                                Text='<%# Bind("identificacion") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sexo">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsexo" runat="server" 
                                                Text='<%# Bind("sexo") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtsexo0" runat="server" 
                                                Text='<%# Bind("sexo") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Parentesco">
                                        <ItemTemplate>
                                            <asp:Label ID="lblparentesco0" runat="server" Text='<%# Bind("parentesco") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtparentesco0" runat="server" 
                                                Text='<%# Bind("parentesco") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fecha Nac.">
                                        <ItemTemplate>
                                        <asp:Label ID="lblfechanac0" runat="server" 
                                                Text='<%# String.Format("{0:MM/dd/yyyy}", Eval("fecha_nacimiento")) %>'></asp:Label>
                              
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtfechanac1" runat="server" 
                                                Text='<%# Bind("fecha_nacimiento") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Actividad">
                                        <ItemTemplate>
                                            <asp:Label ID="lblactividad" runat="server" 
                                                Text='<%# Bind("actividad") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtactividad" runat="server" 
                                                Text='<%# Bind("actividad") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="gridHeader" />
                                <HeaderStyle CssClass="gridHeader" />
                                <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                <RowStyle CssClass="gridItem" />
                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                <SortedAscendingHeaderStyle BackColor="#848384" />
                                <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                <SortedDescendingHeaderStyle BackColor="#575357" />
                            </asp:GridView>
                        </Content> 
                    </asp:AccordionPane> 
                    </Panes> 
                </asp:accordion>
            </td>
        </tr>
        <tr>
            <td class="align-rt" style="font-size: x-small; font-weight: bold; text-align: center;" 
                colspan="8">
            </td>
         </tr>   
    </table>
    <script type="text/javascript">
        $(".numeric").numeric();
        $(".integer").numeric(false, function () { alert("Integers only"); this.value = ""; this.focus(); });
        $(".positive").numeric({ negative: false }, function () { alert("No negative values"); this.value = ""; this.focus(); });
        $(".positive-integer").numeric({ decimal: false, negative: false }, function () { alert("Positive integers only"); this.value = ""; this.focus(); });
        $("#remove").click(
		    function (e) {
		        e.preventDefault();
		        $(".numeric,.integer,.positive").removeNumeric();
		    }
	    );
    </script>

</asp:Content>

