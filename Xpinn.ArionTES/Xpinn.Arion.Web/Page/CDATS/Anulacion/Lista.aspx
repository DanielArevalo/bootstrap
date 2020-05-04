<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - CDATS Anulación :." %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="numero" TagPrefix="ucnumero" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
                                    </asp:ScriptManager>

    <table style="width: 850px">
        <tr>
            <td colspan="7" style="text-align:left">
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
                    Fecha Vencimiento<br />
                    <ucFecha:fecha ID="txtFechaVencimiento" runat="server" />
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
            <td colspan="7">
                <hr style="width: 850px" />
            </td>
        </tr>
    </table>    
            
    <asp:Panel ID="panelGrilla" runat="server">
        <table style="width: 100%">
            <tr>
                <td style="text-align:left">               
                <div style="overflow:scroll; width:100%"> 
                    <asp:GridView ID="gvLista" runat="server" GridLines="Horizontal" AutoGenerateColumns="False"
                        AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging" OnRowEditing="gvLista_RowEditing"
                        PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                        RowStyle-CssClass="gridItem" DataKeyNames="codigo_cdat,estado">
                        <Columns>
                            <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_edit.jpg" ShowEditButton="True" />
                            
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

    <br />
    <br />

    <uc4:mensajeGrabar ID="ctlMensaje" runat="server"/>


</asp:Content>
