<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/ctlDatosPersona.ascx" TagName="ctlPersona" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlSeleccionarPersona.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script type="text/javascript">
        function buscarProcuraduria() {
            var frame = document.getElementById('cphMain_frmPrint');
            alert(frame);
            var form = window.frames["cphMain_frmPrint"].document.forms[0];
            alert(form);
        }
       function buscarRegistraduria() {
            var frame = document.getElementById('cphMain_frmPrint');
            alert(frame);
            var form = window.frames["cphMain_frmPrint"].document.forms[0];
            alert(form);
        }
        function ActiveTabChanged(sender, e) {
        }
    </script>
    <script>
        function iprint(ptarget) {

            var mywindow = window.open('', 'ptarget', 'height=500,width=500');
            mywindow.document.write('<html><head><title>title</title>');
            mywindow.document.write('</head><body >');
            mywindow.document.write($(ptarget).html()); //iFrame data
            mywindow.document.write('</body></html>');

            mywindow.print();
            mywindow.close();
        }
    </script>
        <script type="text/javascript" src="../../../Scripts/jquery-1.8.3.min.js"></script>
        <script type="text/javascript">
        $(document).ready(function() {
	        //$(".boton").click(function(event) {
            //$("#caja").aviso();
	        //});
        });
        setTimeout(function () {
            var num = $("#cphMain_txtIdentificacion").val();
            $("#txtSearchNIT").val(num);
            $("#ContentPlaceHolder1_TextBox1").val(num);
        }, 2000);
        function aviso(){
            $.ajax ({ 
                type : "GET" , 
                encabezados : { "Access-Control-Allow-Origin" : "*" }, 
                url : "https://www.rues.org.co/" });     
                }
        </script>

    <asp:MultiView ActiveViewIndex="0" runat="server" ID="mvConsultas">
        <asp:View ID="ViewAfilados" runat="server">
            <%--<table>
                <tr>
                    <td>
                        <strong>Datos del Asociado</strong>
                    </td>
                </tr>
            </table>--%>
            <br/>
            <asp:Label ID="lblerror" Text="" runat="server" style="color:red;"/>
            <uc1:ListadoPersonas ID="ctlBusquedaPersonas" runat="server" />
        </asp:View>
        <asp:View ID="ViewConsultas" runat="server">
            <asp:Panel ID="panelRiesgo" runat="server" Visible="true" Height="100%">
                <br />
                <table>
                    <tr>
                        <td style="text-align: left">Identificación
                            <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="30px" Visible="false" Style="text-align: left" />
                            <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Width="110px" Style="text-align: left" Enabled="False" />
                        </td>
                        <td style="text-align: left">Tipo de Identificación
                            <asp:DropDownList ID="ddlTipoIdentificacion" runat="server" CssClass="textbox" Width="150px" Style="text-align: left" Enabled="false" />
                        </td>
                        <td style="text-align: left">Tipo de Persona
                            <asp:TextBox ID="txtTipoPersona" runat="server" CssClass="textbox" Width="150px" Style="text-align: left" Enabled="false" />
                        </td>
                        <td style="text-align: left">Nombres
                            <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" Width="400px" Style="text-align: left" Enabled="False"></asp:TextBox>
                        </td>
                        <td style="text-align: left">Estado
                            <asp:TextBox ID="txtEstado" runat="server" CssClass="textbox" Width="20px" Style="text-align: left" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:TabContainer ID="Tabs" runat="server" ActiveTabIndex="0" CssClass="CustomTabStyle"
                    OnClientActiveTabChanged="ActiveTabChanged" Style="margin-right: 1px" Width="100%">
                    <asp:TabPanel ID="tabRegistraduria" runat="server" HeaderText="Datos" >
                        <HeaderTemplate>
                            Registraduria
                            <asp:Image ID="btnCheckR" runat="server" ImageUrl="~/Images/CheckCircularVerde.png" ToolTip="Detalle" Width="16px" onclick="checkList('R', 'N)" style="display:none;"/>
                            <asp:Image ID="btnEquisR" runat="server" ImageUrl="~/Images/EquisCircularRojo.png" ToolTip="Detalle" Width="16px" Visible="true" onclick="checkList('R', 'Y')"/>
                            <asp:CheckBox ID="chkR" runat="server" Width="100%" Text="res" style="display:none;"  TabIndex="23"></asp:CheckBox>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <iframe id="Iframe2" name="IframeRegistraduria" width="100%" frameborder="0" src="https://wsp.registraduria.gov.co/certificado/Datos.aspx" target="_top"
                                height="600px" runat="server" style="border-style: groove; float: left;"></iframe>
                           <%-- <div id="caja">
                                </div>
                            <a href="" onclick="buscarRegistraduria()">Buscar</a>--%>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="TabProcuraduria" runat="server" HeaderText="Datos" >
                        <HeaderTemplate>
                            Procuraduria
                            <asp:Image ID="btnCheckP" runat="server" ImageUrl="~/Images/CheckCircularVerde.png" ToolTip="Detalle" Width="16px" onclick="checkList('P', 'N)" style="display:none;"/>
                            <asp:Image ID="btnEquisP" runat="server" ImageUrl="~/Images/EquisCircularRojo.png" ToolTip="Detalle" Width="16px" Visible="true" onclick="checkList('P', 'Y')"/>
                            <asp:CheckBox ID="chkP" runat="server" Width="100%" Text="res" style="display:none;"  TabIndex="23"></asp:CheckBox>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <iframe id="Iframe5" name="IframeProcuraduria" width="100%" frameborder="0" src="https://www.procuraduria.gov.co/portal/consulta_antecedentes.page" target="_top"
                                height="600px" runat="server" style="border-style: groove; float: left;"></iframe>
                           <%-- <div id="caja">
                                </div>
                            <a href="" onclick="buscarRegistraduria()">Buscar</a>--%>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="TabContraloria" runat="server" HeaderText="Datos" >
                        <HeaderTemplate>
                            <a style="text-decoration:none;" target="_blank" href="https://www.contraloria.gov.co/control-fiscal/responsabilidad-fiscal/control-fiscal/responsabilidad-fiscal/certificado-de-antecedentes-fiscales/persona-natural">Contraloria</a>
                            <asp:Image ID="btnCheckC" runat="server" ImageUrl="~/Images/CheckCircularVerde.png" ToolTip="Detalle" Width="16px" onclick="checkList('C', 'N)" style="display:none;"/>
                            <asp:Image ID="btnEquisC" runat="server" ImageUrl="~/Images/EquisCircularRojo.png" ToolTip="Detalle" Width="16px" Visible="true" onclick="checkList('C', 'Y')"/>
                            <asp:CheckBox ID="chkC" runat="server" Width="100%" Text="res" style="display:none;"  TabIndex="23"></asp:CheckBox>
                        </HeaderTemplate>
                        <ContentTemplate>
                           <%-- <div id="caja">
                                </div>
                            <a href="" onclick="buscarRegistraduria()">Buscar</a>--%>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="tabRues" runat="server" HeaderText="Datos">
                        <HeaderTemplate>
                            RUES
                            <asp:Image ID="btnCheckU" runat="server" ImageUrl="~/Images/CheckCircularVerde.png" ToolTip="Detalle" Width="16px" onclick="checkList('U', 'N)" style="display:none;"/>
                            <asp:Image ID="btnEquisU" runat="server" ImageUrl="~/Images/EquisCircularRojo.png" ToolTip="Detalle" Width="16px" Visible="true" onclick="checkList('U', 'Y')"/>
                            <asp:CheckBox ID="chkU" runat="server" Width="100%" Text="res" style="display:none;"  TabIndex="23"></asp:CheckBox>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <iframe id="Iframe1" name="IframeRues" width="100%" frameborder="0" src="https://www.rues.org.co/RUES_Web/Consultas/Consultasext"
                                height="600px" runat="server" style="border-style: groove; float: left;"></iframe>
 <%--                           <a href="" onclick="buscarRues()">Buscar</a>--%>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="tabPolicia" runat="server" HeaderText="Datos">
                        <HeaderTemplate>
                            POLICIA
                            <asp:Image ID="btnCheckL" runat="server" ImageUrl="~/Images/CheckCircularVerde.png" ToolTip="Detalle" Width="16px" onclick="checkList('L', 'N)" style="display:none;"/>
                            <asp:Image ID="btnEquisL" runat="server" ImageUrl="~/Images/EquisCircularRojo.png" ToolTip="Detalle" Width="16px" Visible="true" onclick="checkList('L', 'Y')"/>
                            <asp:CheckBox ID="chkL" runat="server" Width="100%" Text="res" style="display:none;"  TabIndex="23"></asp:CheckBox>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <iframe id="Iframe3" name="Iframepolicia" width="100%" frameborder="0" src="https://antecedentes.policia.gov.co:7005/WebJudicial/antecedentes.xhtml"
                                height="600px" runat="server" style="border-style: groove; float: left;" tabindex="2" allowfullscreen="true"></iframe>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="tabOFAQ" runat="server" HeaderText="Datos">
                        <HeaderTemplate>OFAC</HeaderTemplate>
                        <ContentTemplate>
                            <asp:Label ID="lblOFAC" runat="server" Text="Listas reportadas" Style="text-align: center; font-weight: bold" Visible="false" /><br />
                            <asp:GridView ID="gvListaOFAC" runat="server" AllowPaging="True" TabIndex="52" AutoGenerateColumns="false" BackColor="White"
                                BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="0" OnRowDataBound="gvLista_RowDataBound"
                                ForeColor="Black" PageSize="10" ShowFooter="True" OnPageIndexChanging="gvListaOFAC_PageIndexChanging"
                                Style="font-size: x-small" Width="100%">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:BoundField DataField="id" HeaderText="Identificación" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:BoundField DataField="source" HeaderText="Lista" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:BoundField DataField="name" HeaderText="Nombre" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:BoundField DataField="alt_name" HeaderText="Alias" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:BoundField DataField="address" HeaderText="Direcciones" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:BoundField DataField="date_of_birth" HeaderText="Fechas de nacimiento" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:BoundField DataField="nationality" HeaderText="Nacionalidades" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:BoundField DataField="place_of_birth" HeaderText="Lugares de Nacimiento" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:BoundField DataField="citizenship" HeaderText="Ciudadanias" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:BoundField DataField="program" HeaderText="Programas" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:TemplateField HeaderStyle-CssClass="gridIco" HeaderText="Coincidencia">
                                        <ItemTemplate>
                                            <asp:Image ID="btnCheck" runat="server" ImageUrl="~/Images/CheckCircularVerde.png" ToolTip="Detalle" Width="16px" Visible="false" />
                                            <asp:Image ID="btnEquis" runat="server" ImageUrl="~/Images/EquisCircularRojo.png" ToolTip="Detalle" Width="16px" Visible="false" />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="gridHeader" />
                                <HeaderStyle CssClass="gridHeader" />
                                <RowStyle CssClass="gridItem" />
                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                <SortedAscendingHeaderStyle BackColor="#848384" />
                                <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                <SortedDescendingHeaderStyle BackColor="#575357" />
                            </asp:GridView>
                            <asp:Label ID="lblResultadosOFAC" runat="server" Text="" Style="text-align: center;" />
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="tabONU" runat="server" HeaderText="Datos">
                        <HeaderTemplate>ONU</HeaderTemplate>
                        <ContentTemplate>
                            <br />
                            <asp:GridView ID="gvONUIndividual" runat="server" AllowPaging="false" TabIndex="52" AutoGenerateColumns="false" BackColor="White"
                                BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="0" ForeColor="Black" ShowFooter="false" HorizontalAlign="Center"
                                Style="font-size: x-small" Width="95%" OnRowDataBound="gvLista_RowDataBound">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:BoundField DataField="dataid" HeaderText="Código" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:BoundField DataField="first_name" HeaderText="Nombres" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:BoundField DataField="un_list_type" HeaderText="Lista" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:BoundField DataField="listed_on" HeaderText="Fecha de Ingreso" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:BoundField DataField="comments1" HeaderText="Comentarios" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:BoundField DataField="designation" HeaderText="Designación" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:BoundField DataField="nationality" HeaderText="Nacionalidad" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:BoundField DataField="list_type" HeaderText="Tipo de Lista" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:TemplateField HeaderStyle-CssClass="gridIco" HeaderText="Coincidencia">
                                        <ItemTemplate>
                                            <asp:Image ID="btnCheck" runat="server" ImageUrl="~/Images/CheckCircularVerde.png" ToolTip="Detalle" Width="16px" />
                                            <asp:Image ID="btnEquis" runat="server" ImageUrl="~/Images/EquisCircularRojo.png" ToolTip="Detalle" Width="16px" />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="gridHeader" />
                                <HeaderStyle CssClass="gridHeader" />
                                <RowStyle CssClass="gridItem" />
                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                <SortedAscendingHeaderStyle BackColor="#848384" />
                                <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                <SortedDescendingHeaderStyle BackColor="#575357" />
                            </asp:GridView>
                            <asp:GridView ID="gvONUEntidad" runat="server" AllowPaging="false" TabIndex="52" AutoGenerateColumns="false" BackColor="White"
                                BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="0" ForeColor="Black" ShowFooter="True"
                                Style="font-size: x-small" Width="90%" OnRowDataBound="gvLista_RowDataBound">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:BoundField DataField="dataid" HeaderText="Código" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:BoundField DataField="first_name" HeaderText="Nombres" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:BoundField DataField="un_list_type" HeaderText="Lista" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:BoundField DataField="listed_on" HeaderText="Fecha de Ingreso" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:BoundField DataField="comments1" HeaderText="Comentarios" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:BoundField DataField="country" HeaderText="País" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:BoundField DataField="city" HeaderText="Ciudad" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:TemplateField HeaderStyle-CssClass="gridIco" HeaderText="Coincidencia">
                                        <ItemTemplate>
                                            <asp:Image ID="btnCheck" runat="server" ImageUrl="~/Images/CheckCircularVerde.png" ToolTip="Detalle" Width="16px" />
                                            <asp:Image ID="btnEquis" runat="server" ImageUrl="~/Images/EquisCircularRojo.png" ToolTip="Detalle" Width="16px" />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="gridHeader" />
                                <HeaderStyle CssClass="gridHeader" />
                                <RowStyle CssClass="gridItem" />
                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                <SortedAscendingHeaderStyle BackColor="#848384" />
                                <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                <SortedDescendingHeaderStyle BackColor="#575357" />
                            </asp:GridView>
                            <br />
                            <asp:Label ID="lblOnu" runat="server" Text="" Style="text-align: center;" />
                        </ContentTemplate>
                    </asp:TabPanel>
                </asp:TabContainer>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
    <script>
        function checkList(tab, visible) {
            var iconY = "";
            var iconN = "";
            var p1 = tab;
            var p2 = "";
            switch(tab){
                case "R":
                    if (visible == "Y") {
                        p2 = "N";
                        iconY = cphMain_Tabs_tabRegistraduria_btnCheckR.id;
                        iconN = cphMain_Tabs_tabRegistraduria_btnEquisR.id;
                    } else {
                        p2 = "Y";
                        iconY = cphMain_Tabs_tabRegistraduria_btnEquisR.id;
                        iconN = cphMain_Tabs_tabRegistraduria_btnCheckR.id;
                    }
                    var v = $("#cphMain_Tabs_tabRegistraduria_btnCheckR").is(":visible");
                    if(v){
                        $("#<%=chkR.ClientID%>").removeAttr('checked');
                    } else { $("#<%=chkR.ClientID%>").attr('checked', 'checked'); }
                    break;
                case "P":
                    if (visible == "Y") {
                        p2 = "N";
                        iconY = cphMain_Tabs_TabProcuraduria_btnCheckP.id;
                        iconN = cphMain_Tabs_TabProcuraduria_btnEquisP.id;
                    } else {
                        p2 = "Y";
                        iconY = cphMain_Tabs_TabProcuraduria_btnEquisP.id;
                        iconN = cphMain_Tabs_TabProcuraduria_btnCheckP.id;
                    }
                    var v = $("#cphMain_Tabs_TabProcuraduria_btnCheckP").is(":visible");
                    if(v){
                        $("#<%=chkP.ClientID%>").removeAttr('checked');
                    } else { $("#<%=chkP.ClientID%>").attr('checked', 'checked'); }
                    break;
                case "U":
                    if (visible == "Y") {
                        p2 = "N";
                        iconY = cphMain_Tabs_tabRues_btnCheckU.id;
                        iconN = cphMain_Tabs_tabRues_btnEquisU.id;
                    } else {
                        p2 = "Y";
                        iconY = cphMain_Tabs_tabRues_btnEquisU.id;
                        iconN = cphMain_Tabs_tabRues_btnCheckU.id;
                    }
                    var v = $("#cphMain_Tabs_tabRues_btnCheckU").is(":visible");
                    if(v){
                        $("#<%=chkU.ClientID%>").removeAttr('checked');
                    } else { $("#<%=chkU.ClientID%>").attr('checked', 'checked'); }
                    break;
                case "L":
                    if (visible == "Y") {
                        p2 = "N";
                        iconY = cphMain_Tabs_tabPolicia_btnCheckL.id;
                        iconN = cphMain_Tabs_tabPolicia_btnEquisL.id;
                    } else {
                        p2 = "Y";
                        iconY = cphMain_Tabs_tabPolicia_btnEquisL.id;
                        iconN = cphMain_Tabs_tabPolicia_btnCheckL.id;
                    }
                    var v = $("#cphMain_Tabs_tabPolicia_btnCheckL").is(":visible");
                    if(v){
                        $("#<%=chkL.ClientID%>").removeAttr('checked');
                    } else { $("#<%=chkL.ClientID%>").attr('checked', 'checked'); }
                    break;
                case "C":
                    if (visible == "Y") {
                        p2 = "N";
                        iconY = cphMain_Tabs_TabContraloria_btnCheckC.id;
                        iconN = cphMain_Tabs_TabContraloria_btnEquisC.id;
                    } else {
                        p2 = "Y";
                        iconY = cphMain_Tabs_TabContraloria_btnEquisC.id;
                        iconN = cphMain_Tabs_TabContraloria_btnCheckC.id;
                    }
                    var v = $("#cphMain_Tabs_TabContraloria_btnCheckC").is(":visible");
                    if(v){
                        $("#<%=chkC.ClientID%>").removeAttr('checked');
                    } else { $("#<%=chkC.ClientID%>").attr('checked', 'checked'); }
                    break;
            }
            $("#" + iconY).show();
            $("#" + iconY).attr('onclick', 'checkList("'+p1+'", "'+p2+'")');
            $("#" + iconN).hide();

        }
    </script>
</asp:Content>


