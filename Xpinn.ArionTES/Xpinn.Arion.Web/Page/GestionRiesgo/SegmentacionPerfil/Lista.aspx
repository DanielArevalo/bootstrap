<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" %>
<%@ Register Src="../../../General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
     <asp:Panel ID="Panel1" runat="server">
        <table style="width: 100%;">
            <tr>
                <td align="center"></td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnEjecutar" runat="server" CssClass="btn8" OnClick="btnEjecutar_Click"
                        Text="Ejecutar Segmentacion" Width="200px" />
                </td>
            </tr>
        </table>
         <table>
             <tr>
                 <td>
                     <asp:Label ID="lblespera" Visible="false" runat="server" Font-Size="Medium" ForeColor="#000000">El proceso tardara varios minutos por favor espere...</asp:Label>
                 </td>
             </tr>
             <tr>
                 <td>
                     <asp:Label ID="lblConfi" Visible="false"  runat="server" Font-Size="Medium" ForeColor="#000000">No se a ejecutado el  proceso de segmentación</asp:Label>
                 </td>
             </tr>
         </table>
    </asp:Panel>

     <asp:HiddenField ID="HF1" runat="server" />    
  <asp:ModalPopupExtender ID="mpeNuevo" runat="server" 
        PopupControlID="panelActividadReg" TargetControlID="HF1"
         BackgroundCssClass="backgroundColor" >
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelActividadReg" runat="server" BackColor="White" Style="text-align: right;">
        <div id="popupcontainer" style="width: 120%">
            <div class="row">
                <div class="cell popupcontainercell">
                    <div id="ordereditcontainer">
                        <asp:UpdatePanel ID="upActividadReg" runat="server">
                            <ContentTemplate>
                                <div class="row">
                                    <div class="cell ordereditcell" style="width: 120%">
                                        <br>
                                    </div>
                                    <div class="cell" style="width: 120%" align="center">
                                        <div class="cell" >
                                            Este proceso tardará varios minutos. <strong>Esta seguro de continuar?</strong><br /> 
                                            <br />
                                        </div>
                                    </div>
                                </div>                             
                                <div class="row">
                                    <div class="cell ordereditcell" style="width: 120%">
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="row">
                            <div class="cell" style="width: 120%">
                            </div>
                            <div class="cell" style="text-align: center; width: 120%;">
                                <asp:Button ID="btnContinuar" runat="server" Text="Continuar"
                                    CssClass="btn8"  Width="182px" onclick="btnContinuar_Click" />
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn8" 
                                 Width="182px" onclick="btnCancelar_Click" />
                            </div>
                            <div class="cell" style="width: 120%">
                                <br /> 
                            </div>
                            <div class="cell" style="width: 120%; text-align: center;">
                                Luego de oprimir el botón continuar deberá esperar hasta que termine el proceso.<br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

        <asp:HiddenField ID="HF2" runat="server" />
          <asp:ModalPopupExtender ID="mpeProcesando" runat="server" PopupControlID="pProcesando" TargetControlID="HF2" BackgroundCssClass="backgroundColor" >
    </asp:ModalPopupExtender>

     <asp:Panel ID="pProcesando" runat="server" BackColor="White" Style="text-align: right;">
        <table style="width: 145%;">
            <tr>
                <td align="center">
                    <asp:Image ID="Image1"  runat="server" ImageUrl="~/Images/loading.gif" />
                    <br />
                    <asp:Label ID="Label1" runat="server" Text="Espere un Momento Mientras Termina el Proceso"></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>

     <asp:Panel ID="pFinal" Visible="false" runat="server" BackColor="White" Style="text-align: center;">
      <asp:Panel ID="pConsulta" runat="server" >
        <br />
        <table id="tbCriterios" border:"0" cellpadding:"0" cellspacing:"0" width:"70%">
            <tr>
                <td style="text-align: left">
                    <strong>Filtrar por:</strong>
                </td>
            </tr>
            <tr>
                 <td class="tdD" style="text-align: left; width: 200px">Identificacion<br />
                    <asp:TextBox ID="Textident" runat="server" CssClass="textbox"
                        MaxLength="50" Width="200px" />
                </td>
                <td class="tdD" style="text-align: left; width: 200px">Nombre<br />
                    <asp:TextBox ID="txtNomdep" runat="server" CssClass="textbox"
                      MaxLength="100"  Width="200px" />
                </td>
                <td class="tdD" style="text-align: left; width: 200px">Valoración<br />
                    <asp:DropDownList ID="txtValoracion" runat="server" Width="102%" CssClass="textbox"
                        AppendDataBoundItems="True">
                        <asp:ListItem Value=""></asp:ListItem>
                        <asp:ListItem Value="1">1</asp:ListItem>
                        <asp:ListItem Value="2">2</asp:ListItem>
                        <asp:ListItem Value="3">3</asp:ListItem>
                        <asp:ListItem Value="4">4</asp:ListItem>
                        <asp:ListItem Value="5">5</asp:ListItem>
                        <asp:ListItem Value="6">6</asp:ListItem>
                        <asp:ListItem Value="7">7</asp:ListItem>
                        <asp:ListItem Value="8">8</asp:ListItem>
                        <asp:ListItem Value="9">9</asp:ListItem>
                        <asp:ListItem Value="10">10</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="tdD" style="text-align: left; width: 200px">Perfil de riesgo<br />
                    <asp:DropDownList ID="txtPerfilderiesgo" runat="server" Width="102%" CssClass="textbox"
                        AppendDataBoundItems="True">
                        <asp:ListItem Value=""></asp:ListItem>
                        <asp:ListItem Value="Riesgo Normal">Riesgo Normal</asp:ListItem>
                        <asp:ListItem Value="Riesgo Moderado">Riesgo Moderado</asp:ListItem>
                        <asp:ListItem Value="Riesgo Muy Alto">Riesgo Muy Alto</asp:ListItem>
                        <asp:ListItem Value="Riesgo Extremo">Riesgo Extremo</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="text-align: left;" width="14%">Tipo rol<br />
                    <asp:DropDownList ID="ddlTipoRol" runat="server" Width="102%" CssClass="textbox"
                        AppendDataBoundItems="True">
                        <asp:ListItem Value=""></asp:ListItem>
                        <asp:ListItem Value="A">Asociado</asp:ListItem>
                        <asp:ListItem Value="R">Exasociado</asp:ListItem>
                        <asp:ListItem Value="NN">Terceros</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <hr />
     <asp:Panel ID="pSegmento" runat="server" >
    <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False"
        AllowPaging="True" PageSize="20" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
        OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
        DataKeyNames="cod_persona"
        Style="font-size: x-small">
        <Columns>
            <asp:BoundField DataField="cod_persona" HeaderText="CODIGO DE PERSONA">
                <ItemStyle HorizontalAlign="Left" Width="300px" />
            </asp:BoundField>
            <asp:BoundField DataField="identificacion" HeaderText="IDENTIFICACION">
                <ItemStyle HorizontalAlign="Left" Width="300px" />
            </asp:BoundField>
                 <asp:BoundField DataField="primer_nombre" HeaderText="PRIMER NOMBRE">
                <ItemStyle HorizontalAlign="Left" Width="300px" />
            </asp:BoundField>
                 <asp:BoundField DataField="segundo_nombre" HeaderText="SEGUNDO NOMBRE">
                <ItemStyle HorizontalAlign="Left" Width="300px" />
            </asp:BoundField>
                 <asp:BoundField DataField="primer_apellido" HeaderText="APELLIDO">
                <ItemStyle HorizontalAlign="Left" Width="300px" />
            </asp:BoundField>
            <asp:BoundField DataField="valoracion" HeaderText="Valoracion">
                <ItemStyle HorizontalAlign="Left" Width="300px" />
            </asp:BoundField>
             <asp:BoundField DataField="perfil" HeaderText="PERFIL RIESGO">
                <ItemStyle HorizontalAlign="Left" Width="300px" />
            </asp:BoundField>
            <asp:BoundField DataField="nom_tipo_rol" HeaderText="TIPO ROL">
                <ItemStyle HorizontalAlign="Left" Width="300px" />
            </asp:BoundField>
        </Columns>
        <HeaderStyle CssClass="gridHeader" />
        <PagerStyle CssClass="gridHeader" Font-Bold="False" />
        <RowStyle CssClass="gridItem" />
    </asp:GridView>
          <asp:Label ID="lblTotalRegs" runat="server" Visible="False" /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
          <asp:Button  ID="btnExportar" runat="server" CssClass="btn8" Width="180px" onclick="ExportarSegmentoPerfiles" Text="Exportar a Excel" />
    </asp:Panel>
    </asp:Panel>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server"  Style="text-align: center; width:70%">
        <ContentTemplate>
            <asp:Timer ID="Timer1" runat="server" Interval="300" ontick="Timer1_Tick" >
            </asp:Timer>
            <br />
            <asp:Label ID="lblError" runat="server" Text="" 
                style="text-align: left; color: #FF3300"></asp:Label>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningun resultado."
        Visible="False" />
    <br />

    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />

</asp:Content>

