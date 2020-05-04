<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - CDATS Administración:." %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="numero" TagPrefix="ucnumero" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <table style="width: 850px">
        <tr>
            <td colspan="8" style="text-align:left">
                <strong>Criterios de Busqueda :</strong>
            </td>
        </tr>
        <tr>            
            <td style="width: 120px; text-align: left">
                Número CDAT<br />
                <asp:TextBox ID="txtNroCDAT" runat="server" CssClass="textbox" Width="90%" />
            </td>            
            <td style="text-align: left; width:130px">
                Fecha de Apertura<br />
            <ucFecha:fecha ID="txtFecha" runat="server"/>
            </td>
          
            <td style="text-align: left; width:130px">
                    Fecha Vencimiento<ucFecha:fecha ID="txtFechaVencimiento" runat="server" />
            </td>
            <td style="text-align: left; width: 170px">
                Tipo/Linea de CDAT<br />
                <asp:DropDownList ID="ddlTipoLinea" runat="server" CssClass="textbox" Width="95%"
                    AppendDataBoundItems="True" />
            </td>
            <td style="text-align: left; width: 170px">
                Asesor Comercial<br />
                <asp:DropDownList ID="ddlAsesor" runat="server" CssClass="textbox" Width="95%" AppendDataBoundItems="True" />
            </td>
            <td style="text-align: left; width: 130px">
                Oficina<br />
                <asp:DropDownList ID="ddlOficina" runat="server" CssClass="textbox" Width="95%" AppendDataBoundItems="True" />
            </td>
            <td style="text-align: left; width: 130px">
                Estado<br />
                <asp:DropDownList ID="ddlEstado" runat="server" CssClass="textbox" Width="95%" />
            </td>            
        </tr>
        <tr>
            <td colspan="8">
                <hr style="width: 850px" />
            </td>
        </tr>
    </table>    
            
    <asp:Panel ID="panelGrilla" runat="server">
        <table>
            <tr>
                <td style="text-align:left;width:100%">
                <strong>Listado de CDATS :</strong> <br />
                <div style="overflow:scroll; width:90%"> 
                    <asp:GridView ID="gvLista" runat="server" GridLines="Horizontal" AutoGenerateColumns="False"
                        AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging" OnRowEditing="gvLista_RowEditing"
                        PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                        RowStyle-CssClass="gridItem" DataKeyNames="codigo_cdat,estado" OnRowDeleting="gvLista_RowDeleting"
                        OnRowCommand="gvLista_RowCommand">
                        <Columns>
                            <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_edit.jpg" ShowEditButton="True" />
                            <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnImprimir" runat="server" ImageUrl="~/Images/gr_imp.gif" ToolTip="Imprimir"
                                        CommandName="Imprimir" CommandArgument='<%#Eval("codigo_cdat") + ";" + Eval("numero_fisico")%>' />
                                </ItemTemplate>
                                <HeaderStyle CssClass="gridIco"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />
                            <asp:BoundField DataField="codigo_cdat" HeaderText="Cod">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Num. CDAT" DataField="numero_cdat">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Num. Físico" DataField="numero_fisico">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_apertura" DataFormatString="{0:d}" HeaderText="Fec Apertura">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="modalidad" HeaderText="Modalidad">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Form Captación" DataField="nomcapta">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="valor" DataFormatString="{0:n}" HeaderText="Valor">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nommoneda" HeaderText="Moneda">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="plazo" HeaderText="Plazo">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Tipo Calend" DataField="nomtipocalendario">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Destinación" DataField="nomdestinacion">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nomoficina" HeaderText="Oficina">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Asesor" DataField="nomusuario">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Tipo" DataField="nomtipointeres">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Tasa Int" DataField="tasa_interes">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Tipo Tasa" DataField="nomtipotasa">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Tipo Hist" DataField="nomtipohistorico">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="+ Puntos" DataField="desviacion">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Modalidad Int" DataField="nommodalidadint">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Period" DataField="nomperiodicidad">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Capita" DataField="nomcapitaliza">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nomretencion" HeaderText="Cobra Int">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Desmat" DataField="nomdesmate">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Estado" DataField="estado" Visible="False" />
                        </Columns>
                        <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                        <PagerStyle CssClass="gridPager"></PagerStyle>
                        <RowStyle CssClass="gridItem"></RowStyle>
                    </asp:GridView>
                    </div>                    
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                </td>
            </tr>
        </table>
    </asp:Panel>


    <asp:ModalPopupExtender ID="mpeReImprimir" runat="server" 
        PopupControlID="PanelImpresion" TargetControlID="HiddenField1"  X="300" Y="300"
        BackgroundCssClass="backgroundColor" >
    </asp:ModalPopupExtender>                                                              
    <asp:Panel ID="PanelImpresion" runat="server" Width="356px">
        <asp:HiddenField ID="HiddenField1" runat="server" />
        <asp:UpdatePanel ID="upPanelEstructura" runat="server">
            <Triggers>           
            <asp:PostBackTrigger ControlID="btnModificar" />   
            </Triggers> 
            <ContentTemplate>
                <div ID="popupcontainer"                                     
                    style="border: medium groove #0000FF; width:478px; background-color: #FFFFFF;">
                    <div class="row popupcontainertitle">
                        <div class="gridHeader" style="text-align: center">GENERAR RE-IMPRESIÓN</div>
                    </div>
                    
                    <table style="width:100%">
                        <caption>
                            <br />
                            <tr>
                                <td>
                                    <center>
                                        <b><span style="color: #0099FF;">Número Físico</span></b></center>
                                </td>
                                <td>
                                    <asp:TextBox ID="CodigoEmergente" runat="server" CssClass="textbox" 
                                        Visible="false" />
                                    <asp:TextBox ID="txtNumeroFisico" runat="server" CssClass="textbox" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center">
                                    <br />
                                    <asp:Button ID="btnModificar" runat="server" CssClass="btn8" 
                                        onclick="btnModificar_Click" Text="GENERAR RE-IMPRESIÓN" />
                                </td>
                                <td style="text-align: center">
                                    <br />
                                    <asp:Button ID="btnCancelar" runat="server" CssClass="btn8" 
                                        onclick="btnCancelar_Click" Text="CANCELAR" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: center">
                                    <br />
                                </td>
                            </tr>
                        </caption>
                    </table>
                </div>        
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>

    <br />
    <br />

    <uc4:mensajeGrabar ID="ctlMensaje" runat="server"/>


</asp:Content>
