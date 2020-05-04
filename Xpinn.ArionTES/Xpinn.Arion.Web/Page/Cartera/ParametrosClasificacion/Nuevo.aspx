<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Credito :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../../../General/Controles/decimales.ascx" tagname="decimales" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager> 
    <br /><br />
     <asp:Panel ID="pConsulta" runat="server" Width="840px">
        <table style="width:70%;">
            <tr>
                <td style="text-align: left">
                    Clasificación : 
                    <asp:TextBox ID="txtdescripcion" runat="server" CssClass="textbox" 
                        Enabled="false" />
                    <asp:TextBox ID="txtcodigo" runat="server" CssClass="textbox" Enabled="false" 
                        Visible="False" Width="28px" />
                </td>
                <td style="text-align: left">
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table border="0" cellpadding="5" cellspacing="0" width="70%" >
        <tr>
            <td  style="height: 30px; text-align: left" colspan="3">
                <strong>Provisión Individual:</strong>
            </td>
        </tr>
        <tr>
            <td style="height: 67px; vertical-align: top">                
                <asp:GridView ID="gvLista" AllowPaging="False" runat="server" 
                    Width="100%" PageSize="20" GridLines="Horizontal" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False"                   
                    style="font-size: small" ShowFooter="False" 
                    DataKeyNames="rango" onrowediting="gvLista_RowEditing">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <span>
                                <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" 
                                    ImageUrl="~/Images/gr_edit.jpg" ToolTip="Editar" Width="16px" />
                                </span>
                                <asp:Label Text='<%# Bind("tipo_provision") %>'  ID="lblcategoria" Visible="false" runat="server" />

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="rango" HeaderText="Id." />
                        <asp:BoundField DataField="categoria" HeaderText="Categoria" />
                        <asp:BoundField DataField="codigo" HeaderText="Cod.Categoria" />
                        <asp:BoundField DataField="diasminimo" HeaderText="Días Mínimo" />
                        <asp:BoundField DataField="diasmaximo" HeaderText="Días Máximo" />
                        <asp:BoundField DataField="NOMBRE" HeaderText="Tipo Provisión" />
                        <asp:BoundField DataField="por_provision" HeaderText="% Provisión" 
                            DataFormatString="{0:d}" />
                        <asp:BoundField DataField="causa" HeaderText="Causa" />
                    </Columns>
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                    <RowStyle CssClass="gridItem" />
                </asp:GridView>
            </td>
            <td style="height: 67px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td  style="height: 20px" colspan="3">
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado." Visible="False" />
            </td>
        </tr>
        <tr>
            <td  style="text-align: left" colspan="3">
                <strong>Efecto de las garantías sobre las provisiones:</strong>
            </td>
        </tr>
        <tr>
            <td style="text-align: left; vertical-align: top">
                <asp:Button ID="btnAgregar" runat="server" CssClass="btn8" Text="Agregar"
                    OnClick="btnAgregar_Click" Width="140px" Height="22px" />
                &nbsp;&nbsp;
                <asp:GridView ID="gvGarantias" OnRowEditing="gvGarantias_RowEditing" AllowPaging="False" runat="server" 
                    Width="70%" PageSize="20" GridLines="Horizontal" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False"                   
                    style="font-size: small" ShowFooter="False" DataKeyNames="idgarantia" >
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <span>
                                <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" 
                                    ImageUrl="~/Images/gr_edit.jpg" ToolTip="Editar" Width="16px" />
                                </span>
                                <asp:Label Text='<%# Bind("cod_clasifica") %>'  ID="lblcod_clasifica" Visible="false" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="idgarantia" HeaderText="Id." />
                        <asp:BoundField DataField="nom_tipo_garantia" HeaderText="Tipo Garantía" />                           
                        <asp:BoundField DataField="dias_inicial" HeaderText="Meses Inicial" />
                        <asp:BoundField DataField="dias_final" HeaderText="Meses Final" />
                        <asp:BoundField DataField="porcentaje" HeaderText="% Provisión" />
                    </Columns>
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                    <RowStyle CssClass="gridItem" />
                </asp:GridView>
            </td>
            <td style="height: 67px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>

    <asp:HiddenField ID="HiddenField1" runat="server" />    
    <asp:ModalPopupExtender ID="mpeNuevo" runat="server" PopupControlID="panelcategorias" TargetControlID="HiddenField1"
        X="400" Y="200"  BackgroundCssClass="backgroundColor" > 
    </asp:ModalPopupExtender>    
    <asp:AnimationExtender ID="popUpAnimationActualizar" runat="server" TargetControlID="btnEditar">
        <Animations>
            <OnClick>
            <Parallel AnimationTarget="panelcategorias" 
            Duration=".3" Fps="25">
            <Move Horizontal="800" Vertical="800" />
            <Resize Width="800" Height="800" />
                <Color PropertyKey="backgroundColor" 
                StartValue="#FFFFFF" 
                EndValue="#FFFF00" />
            </Parallel>                    
        </OnClick>
        </Animations>
    </asp:AnimationExtender>        
    <asp:Panel ID="panelcategorias" runat="server" BackColor="White" style="text-align: left">    
            <div id="Div3"  style="border: medium groove #0000FF; width:312px; background-color: #FFFFFF;">                    
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
                                        ACTUALIZAR
                                    </div>
                                    <br />
                                </div>
                            </div>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="row">
                            <div class="cell" >
                                <div class="cell ordereditcell">
                                    <div class="cell ordereditcell">
                                        <div class="cell ordereditcell">
                                            &nbsp;Categoria
                                        </div>
                                    </div>
                                    <div class="cell ordereditcell">
                                        &nbsp;<asp:TextBox ID="txt_categoria" runat="server" Enabled="false" CssClass="textbox" 
                                            MaxLength="128" />
                                    </div>
                                    <div class="cell">
                                        &nbsp;Días Mínimo<br />
                                        &nbsp;<asp:TextBox ID="txtdiasminimo" runat="server" CssClass="textbox" 
                                            MaxLength="128" />
                                    </div>
                                    <div class="cell">
                                        &nbsp;Días Máximo<br />
                                        &nbsp;<asp:TextBox ID="txtdiasmaximo" runat="server" CssClass="textbox" 
                                            MaxLength="128" />
                                        </div>
                                        &nbsp;Tipo de Provisión
                                        <div class="cell ordereditcell">
                                        &nbsp;<asp:DropDownList ID="Ddlprovision" runat="server" CssClass="dropdown">
                                            <asp:ListItem Value="0">No provisiona</asp:ListItem>
                                            <asp:ListItem Value="1">Capital, 100% Otros Atributos</asp:ListItem>
                                            <asp:ListItem Value="2">Todos los Atributos</asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                    </div>
                                    <div class="cell">
                                        &nbsp;% Provisión<br />
                                        &nbsp;<asp:TextBox ID="porce_provision" runat="server" CssClass="textbox" 
                                            MaxLength="128" >0</asp:TextBox>
                                    </div>
                                    <div class="cell">
                                        &nbsp;<asp:CheckBox ID="Chkcausa" Text="Causa" runat="server" Checked='<%# Eval("causa")%>' />
                                    </div>
                                    <br />
                                </div>
                                &nbsp;<asp:Button ID="btnGuardar" runat="server" CssClass="button" Height="20px" 
                                    style="margin-right:10px;" Text="Guardar" 
                                    ValidationGroup="vgActividadAct" onclick="btnGuardar_Click" />
                                &nbsp;<asp:Button ID="btnActualizar" runat="server" CssClass="button" Height="20px" 
                                    onclick="btnActualizar_Click" style="margin-right:10px;" Text="Actualizar" 
                                    ValidationGroup="vgActividadAct" />
                                &nbsp;<asp:Button ID="btnClose" runat="server" CausesValidation="false" 
                                    CssClass="button" Height="20px"  Text="Cerrar" onclick="btnClose_Click" />
                                <br />
                            </div>
                            <div class="cell" style="text-align:right">
                            </div>
                        </div>
                    </div>
                </div>
            </div>        
        </div>
    </asp:Panel>

    <asp:HiddenField ID="HiddenField2" runat="server" />
    <asp:ModalPopupExtender ID="mpeGarantias" runat="server" PopupControlID="panelGarantias" TargetControlID="HiddenField2" 
        X="400" Y="200"  BackgroundCssClass="backgroundColor">
    </asp:ModalPopupExtender>    
    <asp:Panel ID="panelGarantias" runat="server" Style="margin-bottom: 0px">        
        <asp:Panel ID="PanelEncGarantias" runat="server" BackColor="White" Style="text-align: left;padding:15px">
            <table border="0" cellpadding="5" cellspacing="0" style="border: medium groove #0000FF;
                background-color: #FFFFFF; margin-right: 5px;" width="300px" align="center">
                <tr>
                    <td class="tdI" style="text-align: center" colspan="3">
                        <div class="gridHeader" style="width: 100%">
                            EFECTO GARANTIAS SOBRE PROVISION</div>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;" colspan="3">                                
                        Tiempo de Mora del crédito (Meses)
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;">
                        Rango Inicial<br />
                        <asp:TextBox ID="txtDiasIni" runat="server" CssClass="textbox" Enabled="True" MaxLength="8" Width="95%" />
                    </td>
                    <td style="text-align: left;">
                        Rango Final<br />
                        <asp:TextBox ID="txtDiasFin" runat="server" CssClass="textbox" Enabled="True" MaxLength="8" Width="95%" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;" colspan="3">
                        Tipo de Garantía<br />
                        <asp:DropDownList ID="ddlTipoGarantia" CssClass="textbox" runat="server"  Width="97%">
                            <asp:ListItem Text="admisibles no hipotecarias" Value="1"></asp:ListItem>
                            <asp:ListItem Text="hipotecarias" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;">
                        Porcentaje<br />
                        <asp:TextBox ID="txtPorcentaje" runat="server" CssClass="textbox" MaxLength="8" Width="95%" />
                    </td>
                    <td style="text-align: left;">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; height: 23px;" colspan="3"> 
                        <asp:Button ID="btnGuardarReg" runat="server" CssClass="btn8"  Height="25px" OnClick="btnGuardarReg_Click"
                            Style="margin-right: 10px;" Text="Guardar" ValidationGroup="vgActividadReg" Width="100px"/>&#160;&#160;&#160;
                        <asp:Button ID="btnCloseReg" runat="server" CausesValidation="false" CssClass="btn8" 
                            Height="25px" OnClick="btnCloseReg_Click" Text="Cerrar" Width="100px"/>
                    </td>
                </tr>
                        
                <tr>
                    <td style="text-align: left;" colspan="3">                                
                        <asp:Label ID="lblMensaje" runat="server" Text="" Width="97%" ForeColor="Red" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </asp:Panel>

</asp:Content>