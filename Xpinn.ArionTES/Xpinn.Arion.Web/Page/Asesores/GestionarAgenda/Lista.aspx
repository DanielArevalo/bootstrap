<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" MasterPageFile="~/General/Master/site.master"%>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="GMaps" namespace="Subgurim.Controles" tagprefix="cc1" %>
<%@ Register Src="~/General/Controles/EnviarCorreo.ascx" TagName="imprimir" TagPrefix="ucImprimir" %>

<asp:Content ID="Content1" runat="server"  contentplaceholderid="cphMain">
    <br /><br />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server" Width="100%">
        <table style="width:100%;">
            <tr>
                <td colspan="4">
                </td>
            </tr>
            <tr>
                <td style="text-align: left;">
                    Oficina<br />
                    <asp:DropDownList ID="ddlOficina" runat="server" AutoPostBack="True" Width="300px" 
                        CssClass="dropdown" onselectedindexchanged="ddlOficina_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:CompareValidator ID="cvOficina" runat="server" 
                        ControlToValidate="ddlOficina" Display="Dynamic" 
                        ErrorMessage="Seleccione una oficina" ForeColor="Red" Operator="GreaterThan" 
                        SetFocusOnError="true" Type="Integer" ValidationGroup="vgGuardar" 
                        ValueToCompare="0"></asp:CompareValidator>
                </td>
                <td style="text-align: left;">
                    Asesor<br />
                    <asp:DropDownList ID="ddlAsesores" runat="server" AutoPostBack="True" Width="300px" 
                        CssClass="dropdown" onselectedindexchanged="ddlAsesores_SelectedIndexChanged">
                        <asp:ListItem Value="0">&lt;Seleccione un Item&gt;</asp:ListItem>
                    </asp:DropDownList>
                    <asp:CompareValidator ID="cvAsesor" runat="server" 
                        ControlToValidate="ddlAsesores" Display="Dynamic" 
                        ErrorMessage="Seleccione un asesor" ForeColor="Red" Operator="GreaterThan" 
                        SetFocusOnError="true" Type="Integer" ValidationGroup="vgGuardar" 
                        ValueToCompare="0">
                    </asp:CompareValidator>
                    <asp:DropDownList ID="ddlAsesores1" runat="server" AutoPostBack="True" Width="200px" 
                        CssClass="dropdown" onselectedindexchanged="ddlAsesores_SelectedIndexChanged" 
                        Visible="false">
                    </asp:DropDownList>
                </td>
                <td style="text-align: left;">
                    <table style="width:96%;">
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnEnviarCorreo" runat="server" CssClass="btn8" 
                                    onclick="ucImprimir_Click" style="height: 22px" Text="Enviar Correo" Width="100px" />
                            </td>
                            <td align="center">
                                <asp:Button ID="btncolocacion" runat="server" CssClass="btn8" 
                                    onclick="btncolocacion_Click" style="height: 22px" Text="Ir a Colocación" Width="100px" />
                            </td>
                        </tr>
                    </table>
                    <ucImprimir:imprimir ID="ucImprimir" runat="server" Visible="false" />
                </td>
                <td style="text-align: left;">
                    <%--                <asp:Button ID="btnUbicacion" runat="server" CssClass="btn8" Text="Ubicación" 
                        Width="80px" onclick="btnUbicacion_Click" ValidationGroup="vgGuardar" />--%>
                    <%--  <asp:Button ID="btnAlarma" runat="server" CssClass="btn8" 
                        onclick="btnAlarma_Click" Text="Alarmas" ValidationGroup="vgGuardar" 
                        Width="80px" />--%>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <hr width="100%"/>
                </td>
            </tr>
        </table>
    </asp:Panel>
    
    <table style="width: 100%;">
        <tr>
            <td style="text-align: left; vertical-align: top">
                Fecha
                <asp:TextBox ID="txtFecha" runat="server" CssClass="textbox" Width="100px" 
                    AutoPostBack="True" ontextchanged="txtFecha_TextChanged" />
            </td>
            <td style="text-align: left; vertical-align: top" colspan="6">            
                <asp:Label ID="lblAgenda" runat="server" Text="AGENDA" Visible="False" 
                    style="text-align: center"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: left; vertical-align: top">
                <div style="text-align: left">
                    <asp:Calendar ID="Calendar1" runat="server" BackColor="White" 
                        BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" 
                        DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" 
                        ForeColor="#003399" Height="200px" 
                        OnDayRender="Calendar1_OnDayRender" Width="220px">
                        <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
                        <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                        <OtherMonthDayStyle ForeColor="#999999" />
                        <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                        <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
                        <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" 
                            Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
                        <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                        <WeekendDayStyle BackColor="#CCCCFF" />
                    </asp:Calendar>                    
                    <asp:CompareValidator ID="cvFecha_diligencia" runat="server" 
                        ControlToValidate="txtFecha" Display="Dynamic" 
                        ErrorMessage="Formato de Fecha (dd/mm/aaaa)" ForeColor="Red" 
                        Operator="DataTypeCheck" SetFocusOnError="True" ToolTip="Formato fecha" 
                        Type="Date" ValidationGroup="vgGuardar" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        Display="Dynamic"  ErrorMessage="Seleccione una fecha" ForeColor="Red" 
                        ControlToValidate="txtFecha" ValidationGroup="vgGuardar"> </asp:RequiredFieldValidator>
                </div>                
            </td>
            <td style="text-align: left; vertical-align: top" colspan="6">
                 <asp:GridView ID="gvListaActividades" runat="server" AllowPaging="True" 
                    AutoGenerateColumns="False" DataKeyNames="CodActividad" GridLines="Horizontal" 
                    HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" 
                    Width="99%" ForeColor="White" BackColor="#999999" style="margin-right: 0px" 
                    OnRowDeleting="gvListaActividades_RowDeleting"
                    onselectedindexchanged="gvListaActividades_SelectedIndexChanged" 
                    onrowediting="gvListaActividades_RowEditing"
                    OnRowDataBound="gvListaActividades_RowDataBound"
                    onselectedindexchanging="gvListaActividades_SelectedIndexChanging" >
                    <Columns>
                        <asp:BoundField DataField="CodActividad" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo"> 
                            <HeaderStyle CssClass="gridColNo"></HeaderStyle>
                            <ItemStyle Width="1%" />
                        </asp:BoundField>      
                        <asp:BoundField DataField="IdHora"  HeaderStyle-CssClass="gridColNo" 
                            ItemStyle-CssClass="gridColNo" ReadOnly="true" ControlStyle-ForeColor="#000000">  
                            <ControlStyle ForeColor="Black"></ControlStyle>
                            <HeaderStyle CssClass="gridColNo"></HeaderStyle>
                            <ItemStyle CssClass="gridColNo"></ItemStyle>
                        </asp:BoundField>
                         <asp:TemplateField HeaderStyle-CssClass="gridIco"  ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnNuevo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg" ToolTip="Nuevo"/>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                            <ItemStyle CssClass="gridIco"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco"  ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg" ToolTip="Modificar"/>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                            <ItemStyle CssClass="gridIco"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco"  ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnBorrar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg" ToolTip="Borrar" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                            <ItemStyle CssClass="gI" />
                        </asp:TemplateField>  
                        <asp:BoundField DataField="Hora" HeaderText="Hora" ReadOnly="true" DataFormatString="{0:d}" >
                            <ItemStyle HorizontalAlign="Left"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="Tipo" HeaderText="Tipo" ReadOnly="true">
                            <ItemStyle HorizontalAlign="Left"/>
                        </asp:BoundField>        
                        <asp:BoundField DataField="Nombrecliente" HeaderText="Cliente" ReadOnly="true">
                           <ItemStyle HorizontalAlign="Left" Width="250px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TipoActividad" HeaderText="Tipo actividad" ReadOnly="true">
                            <ItemStyle HorizontalAlign="Left" Width="150px" />
                        </asp:BoundField>    
                        <asp:BoundField DataField="Descripcion" HeaderText="Descripción" ReadOnly="true">
                            <ItemStyle HorizontalAlign="Left" Width="250" />
                         </asp:BoundField>
                        <asp:BoundField DataField="Estado" HeaderText="Estado" ReadOnly="true">
                            <ItemStyle Width="1%" />
                        </asp:BoundField> 
                        <asp:BoundField DataField="Respuesta" HeaderText="Respuesta" ReadOnly="true"/>
                    </Columns>
                    <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                    <PagerStyle CssClass="gridPager"></PagerStyle>
                    <RowStyle CssClass="gridItem"></RowStyle>
                </asp:GridView>                                                        
            </td>
        </tr>
        <tr>
            <td style="text-align: left; vertical-align: top">
            </td>
            <td align="center">
                Convenciones:
            </td>
            <td align="center">
                <asp:Label ID="Label1" runat="server" BackColor="#000099" BorderStyle="None" 
                    ForeColor="White" Text=" Programada" Width="150px"></asp:Label>
            </td>
            <td align="center">
                <asp:Label ID="Label6" runat="server" BackColor="#009933" BorderStyle="None" 
                    ForeColor="White" style="text-align: center" Text="  Realizada" Width="150px"></asp:Label>
            </td>
            <td align="center">
                <asp:Label ID="Label5" runat="server" BackColor="#660066" BorderStyle="None" 
                    ForeColor="White" style="text-align: center" Text="Reemplazada" Width="150px"></asp:Label>
            </td>
            <td align="center">
                <asp:Label ID="Label7" runat="server" BackColor="#CCCC00" BorderStyle="None" 
                    ForeColor="White" style="text-align: center" Text="En Proceso" Width="150px"></asp:Label>
            </td>
            <td align="center" style="text-align: left">
                <asp:Label ID="Label8" runat="server" BackColor="#0099FF" BorderStyle="None" 
                    ForeColor="White" style="text-align: center" Text="Programada por asesor" 
                    Width="150px"></asp:Label>
            </td>
        </tr>
    </table>
    

    <asp:HiddenField ID="HiddenField1" runat="server" />    
    <asp:ModalPopupExtender ID="mpeNuevo" runat="server" 
        PopupControlID="panelActividadReg" TargetControlID="HiddenField1"
         BackgroundCssClass="backgroundColor" >
    </asp:ModalPopupExtender>

    <asp:ModalPopupExtender ID="mpeMensaje" runat="server" 
        PopupControlID="panelMensaje" TargetControlID="HiddenField1"
         BackgroundCssClass="backgroundColor" >
    </asp:ModalPopupExtender>

    <asp:Panel ID="panelActividadReg" runat="server" BackColor="White" style="text-align: left">    
        <div id="popupcontainer"  style="border: medium groove #0000FF; width:312px; background-color: #FFFFFF;">              
            <div class="row popupcontainertitle">
                <div class="cell popupcontainercell" style="text-align: center">
                    <div class="gridHeader" style="text-align: center">
                        REGISTRO ACTIVIDAD
                    </div>
                </div>  
            </div>
            <div class="row">
                <div class="cell popupcontainercell">
                    <div id="ordereditcontainer">
                        <asp:UpdatePanel ID="upActividadReg" runat="server">
                        <ContentTemplate>   
                            <div class="row">
                                <div class="cell ordereditcell">
                                    &nbsp;&nbsp;Cliente
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        &nbsp;&nbsp;
                                        <asp:DropDownList ID="DropDownListClientes" runat="server" CssClass="dropdown" 
                                            onselectedindexchanged="DropDownListClientes_SelectedIndexChanged" 
                                            AutoPostBack="True" Width="205px">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="cell">
                                    <div class="cell">
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                            &nbsp;&nbsp;Identificación<br />
                                            &nbsp;&nbsp;
                                            <asp:TextBox ID="txtIdentificacion" runat="server" ontextchanged="txtIdentificacion_TextChanged" 
                                                AutoPostBack="True" Width="195px" CssClass="textbox" ></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="cell ordereditcell">&nbsp;&nbsp;Tipo actividad</div>
                                <div class="cell">
                                    &nbsp;&nbsp;
                                    <asp:DropDownList ID="ddlTipoActividadReg" runat="server" CssClass="dropdown" Width="205px">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="cell ordereditcell">&nbsp;&nbsp;Hora inicial</div>
                                <div class="cell">
                                    &nbsp;&nbsp;
                                    <asp:DropDownList ID="ddlHoraInicialReg" runat="server" CssClass="dropdown" 
                                        AutoPostBack="True"  Width="205px"
                                        onselectedindexchanged="ddlHoraInicial_SelectedIndexChanged"></asp:DropDownList>
                                    <br />
                                    <asp:CompareValidator ID="cvHoraInicialReg" runat="server" ControlToValidate="ddlHoraInicialReg" Display="Dynamic" 
                                        ErrorMessage="Seleccione una hora inicial" ForeColor="Red" Operator="GreaterThan" SetFocusOnError="true" Type="Integer" 
                                        ValidationGroup="vgActividadReg" ValueToCompare="0">
                                    </asp:CompareValidator>
                                </div>
                            </div>
                            <div class="row">
                                <div class="cell ordereditcell">&nbsp;&nbsp;Hora final</div>
                                    <div class="cell">
                                    &nbsp;&nbsp;
                                    <asp:DropDownList ID="ddlHoraFinalReg" runat="server" CssClass="dropdown" Width="205px"></asp:DropDownList>
                                    <br />
                                    <asp:CompareValidator ID="cvHoraFinal" runat="server" ControlToValidate="ddlHoraFinalReg" Display="Dynamic" 
                                    ErrorMessage="Seleccione una hora final" ForeColor="Red" Operator="GreaterThan" 
                                    SetFocusOnError="true" Type="Integer" ValidationGroup="vgActividadReg" ValueToCompare="0">
                                    </asp:CompareValidator>
                                </div>
                            </div>
                            <div class="row">
                                <div class="cell ordereditcell">&nbsp;&nbsp;Descripción</div>
                                <div class="cell">
                                    &nbsp;&nbsp;
                                    <asp:TextBox ID="txtDescripcionReg" runat="server" MaxLength="128" Width="286px" TextMode="MultiLine" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="cell ordereditcell"></div>
                            </div>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="row">
                            <div class="cell" style="text-align:center">
                                <asp:Button ID="btnGuardarReg" runat="server" Text="Guardar" style="margin-right:10px;"
                                    CssClass="button" onclick="btnGuardar_Click" 
                                    ValidationGroup="vgActividadReg" Height="20px" />
                                <asp:Button ID="btnCloseReg" runat="server" Text="Cerrar" Height="20px"
                                    CssClass="button" onclick="btnClose_Click" CausesValidation="false" />
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>   
    </asp:Panel>
      
    <asp:HiddenField ID="HiddenField2" runat="server" />
    <asp:ModalPopupExtender ID="mpeActualizar" runat="server" 
        PopupControlID="panelActividadAct" TargetControlID="HiddenField2"
        BackgroundCssClass="backgroundColor" >
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelActividadAct" runat="server" BackColor="White" 
        style="text-align: left">    
        <div id="Div3"  style="border: medium groove #0000FF; width:320px; background-color: #FFFFFF;">                    
            <div class="row popupcontainertitle">
            </div>
            <div class="row">
                <div class="cell popupcontainercell">
                    <div id="Div4">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>   
                                <div class="row">
                                    <div class="cell">
                                        <div class="gridHeader" style="text-align: center">
                                            ACTUALIZAR ACTIVIDAD
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>  
                        </asp:UpdatePanel>
                        <div class="row">
                            <div class="cell" >
                                <div class="cell ordereditcell">
                                    <div class="cell ordereditcell">
                                        &nbsp;&nbsp;
                                        Estado<br />
                                        &nbsp;&nbsp;
                                        <asp:DropDownList ID="ddlEstadoAct" runat="server" CssClass="dropdown" Width="93%">
                                        </asp:DropDownList>                                        
                                    </div>
                                    <div class="cell ordereditcell">
                                        &nbsp;&nbsp;
                                        Atendido<br />
                                        &nbsp;&nbsp;
                                        <asp:TextBox ID="txtAtendidoAct" runat="server" CssClass="textbox" MaxLength="128" Width="90%" />
                                    </div>
                                    <div class="cell">
                                        &nbsp;&nbsp;
                                        Parentesco<br />
                                        &nbsp;&nbsp;
                                        <asp:DropDownList ID="ddlParentescoAct" runat="server" CssClass="dropdown" Width="93%">
                                        </asp:DropDownList>
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" 
                                            ControlToValidate="ddlParentescoAct" Display="Dynamic" 
                                            ErrorMessage="Seleccione el parentesco " ForeColor="Red" Operator="GreaterThan" 
                                            SetFocusOnError="true" Type="Integer" ValidationGroup="btnGuardarAct" 
                                            ValueToCompare="0">
                                        </asp:CompareValidator>
                                    </div>
                                    <div class="cell">
                                        &nbsp;&nbsp;
                                        Respuesta<br />
                                        &nbsp;&nbsp;
                                        <asp:TextBox ID="txtRespuestaAct" runat="server" CssClass="textbox"  Width="90%"
                                            MaxLength="128" ontextchanged="txtRespuestaAct_TextChanged" />
                                    </div>
                                    <div class="cell ordereditcell">
                                        &nbsp;&nbsp;
                                        Observaciones
                                        <br />
                                    </div>
                                    <div class="cell ordereditcell">
                                        &nbsp;&nbsp;
                                        <asp:TextBox ID="txtObservacionesAct" runat="server" CssClass="textbox"  Width="90%"
                                            MaxLength="128" />
                                        <br />
                                    </div>
                                </div>
                                <div class="cell" style="text-align:center">
                                    <br />
                                    <asp:Button ID="btnGuardarAct" runat="server" CssClass="button" Height="20px" 
                                        onclick="btnActualizar_Click" style="margin-right:10px;" Text="Actualizar" 
                                        ValidationGroup="vgActividadAct" Width="25%"/>
                                    <asp:Button ID="btnCloseAct" runat="server" CausesValidation="false" 
                                        CssClass="button" Height="20px" onclick="btnClose3_Click" Text="Cerrar" Width="25%" />
                                </div>
                            </div>
                            <div class="cell" style="text-align:right">
                            </div>
                        </div>
                    </div>
                </div>
            </div>         
        </div> 
    </asp:Panel>

    <asp:Panel ID="panelMensaje" runat="server" BackColor="White">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>   
                <div id="Div1" style="width:150px">                    
                <div class="row popupcontainertitle">
                    <div class="cell popupcontainercell">
                        Actividad
                    </div>  
                </div>
                <div class="row">
                    <div class="cell popupcontainercell">
                        <div id="Div2">
                            <div class="row">
                                <div class="cell ordereditcell">No se puede ingresar/actualizar actividad..</div>
                            </div>   
                            <div class="cell">
                                <asp:Label ID="lblMotivo" runat="server" MaxLength="128"></asp:Label>
                            </div>              
                            <div class="row">
                                <div class="cell" style="text-align:right">
                                    <asp:Button ID="Button2" runat="server" Text="Cerrar" 
                                        CssClass="button" onclick="btnClose2_Click" CausesValidation="false" 
                                        Height="20px" />
                                    <br />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>     
    
    <asp:Panel ID="panel1" runat="server" BackColor="White">
        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
            <ContentTemplate>   
                <div id="Div5" style="width:150px">                    
                    <div class="row popupcontainertitle">
                        <div class="cell popupcontainercell">
                        </div>  
                    </div>
                    <div class="row">
                        <div class="cell popupcontainercell">
                            <div id="Div6">
                                <div class="row">
                                    <div class="cell" style="text-align:right">
                                        <br />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel> 
        
</asp:Content>
