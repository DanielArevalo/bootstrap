<%@ Page Language="C#" EnableEventValidation="false" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Beneficiarios.aspx.cs" Inherits="NuevoPersona" Title=".: Expinn - Personas :."  %>

<%--<%@ Register Assembly="GMaps" Namespace="Subgurim.Controles" TagPrefix="cc1" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/ctlDireccion.ascx" TagName="Direccion" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="txtPesos" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimalesGridRow.ascx" TagName="decimalesGridRow" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlNumero.ascx" TagName="numero" TagPrefix="uc5" %>
<%@ Register Src="~/General/Controles/ctlFormaPago.ascx" TagName="Forma" TagPrefix="uc3" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/ctlFormatoDocum.ascx" TagName="FormatoDocu" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <script type="text/javascript">
        var specialKeys = new Array();
        specialKeys.push(8); //Backspace
        function ValidNum(e) {
            var keyCode = e.which ? e.which : e.keyCode
            return ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1);
        }
    </script>

    <style>
        /*anteriores anderson*/
        .numeric {
            width: 110px;
            text-align: right;
        }

        .auto-style1 {
            width: 158px;
        }


        a {
            color: #383838;
        }

        * {
            margin: 0;
            padding: 0;
            -webkit-box-sizing: border-box;
            -moz-box-sizing: border-box;
            box-sizing: border-box;
        }

        body {
            background: #D4D4D4;
            font-family: 'Open sans';
        }

        .wrap {
            width: 100%;
            max-width: 100%;
            margin: 1px auto;
        }

        .header-tabs {
            padding-top: 0px;
            background: #d4d4d4;
        }

        ul.tabs {
            width: 100%;
            background: #f3f3f3;
            list-style: none;
            display: flex;
            color: #808080;
            text-align: center;
            margin: 0px auto;
        }

        ul.tabs li {
            width: 20%;
        }

        ul.tabs li a {
            text-decoration: none;
            font-size: 12px;
            text-align: center;
            display: block;
            padding: 14px 0px;
        }        

        ul.tabs li a .tab-text {
            margin-left: 7px;
        }

        .secciones {
            width: 100%;
            background: #fff;
        }

        .secciones article {
            padding-top: 20px;
            padding-bottom: 20px;
        }

        .secciones article p {
            text-align: justify;
        }


        @media screen and (max-width: 700px) {
            ul.tabs li {
                width: none;
                flex-basis: 0;
                flex-grow: 1;
            }
        }

        @media screen and (max-width: 450px) {
            ul.tabs li a {
                padding: 15px 0px;
            }

                ul.tabs li a .tab-text {
                    display: none;
                }

            .secciones article {
                padding: 20px;
            }
        }
        /*nuevos w3c*/
        * {
            box-sizing: border-box;
        }

        /* Set height of body and the document to 100% */
        body, html {
            height: 100%;
            margin: 0;
            font-family: Arial;
        }

        /* Style tab links */
        .tablink {
            background-color: #555;
            color: white;
            float: left;
            border: none;
            outline: none;
            cursor: pointer;
            padding: 14px 16px;
            font-size: 17px;
            width: 25%;
        }

        .tablink:hover {
                background-color: #777;
            }

        /* Style the tab content (and add height:100% for full page content) */
        .tabcontent {
            color: black;
            display: none;
            padding: 0px 0px;
            height: 500px;
        }

        .clsInactivos {
            display: none;
        }

        /*Cristhian perez*/
        .tab{
            height: 45px;
            width: 100%;
            border: none;
            background-color: transparent;
            border-bottom: 2px solid darkslategrey;
        }

        .active {
            background: cadetblue;
            color: #fff;
        }

        .tab:hover{
            background-color: lightgray;
            border-bottom: none;
            color: black;
            font-weight: bold;
        }



    </style>
    <asp:Panel ID="panelDAtaGeneral" runat="server">
        <%-- objetos globales de afiliación --%>
        <asp:ScriptManager ID="ScriptManager" runat="server" EnablePageMethods="true">
        </asp:ScriptManager>
        <%--<uc4:FormatoDocu ID="ctlFormatos" runat="server" />--%>
        <%-- fin objetos globales afiliación --%>
        <%-- Sección de tabs, nombres y código --%>
                <asp:Panel ID="panelTabs" runat="server" Visible="true">
				    <div class="wrap">					    
                            <ul class="tabs header-tabs">
                                <li id="liPersonal" title="InfoPersonal">
                                    <asp:Button Text="Inf. personal" runat="server" ID="btnTab1" class="tab" OnClientClick="saltarRequeridos()" OnClick="btnTab1_Click" />
                                </li>
                                <li id="lilaboral" title="InfoLaboral" class="InfoLaboral">
                                    <asp:Button Text="Inf. laboral" runat="server" ID="btnTab2" class="tab" OnClientClick="saltarRequeridos()" OnClick="btnTab2_Click" />    
                                </li>
                                <li id="liBeneficiarios" title="InfoBenefi">
                                    <asp:Button Text="Inf. beneficiarios" runat="server" ID="btnTab3" class="tab active" OnClick="btnTab3_Click" />    
                                </li>
                                <li id="lilEconomica" title="InfoEconomi">
                                    <asp:Button Text="Inf. económica" runat="server" ID="btnTab4" class="tab" OnClick="btnTab4_Click" />    
                                </li>
                                <li id="liAdicional" title="InfoAdicion">
                                    <asp:Button Text="Inf. adicional" runat="server" ID="btnTab5" class="tab" OnClick="btnTab5_Click" />    
                                </li>
                            </ul>
					</div>
                </asp:Panel>                
                <div>
                <strong>
                    <asp:Label Text="Codigo: " runat="server" Visible="false" /></strong>
                <asp:Label ID="lblcodpersona" Text="" runat="server" />
                <strong>
                    <asp:Label Text="Identificación: " runat="server" Visible="false" /></strong>
                <asp:Label ID="lblidentificacion" Text="" runat="server" />
                <strong>
                    <asp:Label Text="Nombre: " runat="server" Visible="false" /></strong>
                <asp:Label ID="lblnombre" Text="" runat="server" />
                </div>
        <%-- Fin sección tabs --%>
        <%-- Inicion sección propia de la página --%>
        <div>
            <asp:Label ID="lblerror" Text="" runat="server" />
        </div>
        
         <asp:UpdatePanel ID="UpdConyugue" runat="server" UpdateMode="Conditional" ClientID="hola">
            <ContentTemplate>
                <div>
                    <asp:Label ID="Label1" Text="" runat="server" />
                </div>
                <table style="text-align: left; padding-top: 15px; width: 100%;">
                    <tr style="text-align: center;">
                        <td colspan="8" style="color: #FFFFFF; background-color: #5295fa; height: 30px; width: 100%;">
                            <strong>Información del conyugue</strong>
                        </td>
                    </tr>
                </table>
                <table style="text-align: left; padding-top: 15px; width: 100%;" border="0">
                    <tr>
                        <td style="text-align: left;">Tipo Identificación *
											<br />
                            <asp:DropDownList ID="ddlTipo" Width="170px" runat="server" CssClass="textbox required" TabIndex="6" required="required">
                                <asp:ListItem Value="" Text="Seleccione un item"></asp:ListItem>
                            </asp:DropDownList>
                        </td>

                        <td style="text-align: left;">Identificación *<br />
                            <asp:TextBox ID="txtIdent_cony" runat="server" CssClass="textbox required" TabIndex="7" MaxLength="20" OnTextChanged="txtIdentificacionE_TextChanged" AutoPostBack="true" required="required" /></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtcod_conyuge" runat="server" CssClass="textbox" TabIndex="1"
                                MaxLength="128" Visible="false" Width="90%" />Primer Nombre *	
                        <br />
                            <asp:TextBox ID="txtnombre1_cony" runat="server" CssClass="textbox required" TabIndex="2"
                                MaxLength="128" Style="text-transform: uppercase" Width="90%" required="required" /><asp:FilteredTextBoxExtender
                                    ID="FilteredTextBoxExtender1" runat="server" Enabled="True" TargetControlID="txtnombre1_cony"
                                    ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyzüÜáéíóúÁÉÍÓ " />
                        </td>
                        <td style="text-align: left;">Segundo Nombre<br />
                            <asp:TextBox ID="txtnombre2_cony" runat="server" CssClass="textbox" TabIndex="3"
                                MaxLength="128" Style="text-transform: uppercase" Width="90%" /><asp:FilteredTextBoxExtender
                                    ID="FilteredTextBoxExtender2" runat="server" Enabled="True" TargetControlID="txtnombre2_cony"
                                    ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyzüÜáéíóúÁÉÍÓ " />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left;">Primer Apellido *
											<br />
                            <asp:TextBox ID="txtapellido1_cony" runat="server" CssClass="textbox required" TabIndex="4"
                                MaxLength="128" Style="text-transform: uppercase" Width="90%" required="required" /><asp:FilteredTextBoxExtender
                                    ID="FilteredTextBoxExtender3" runat="server" Enabled="True" TargetControlID="txtapellido1_cony"
                                    ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyzüÜáéíóúÁÉÍÓ " />
                        </td>
                        <td style="text-align: left;">Segundo Apellido&#160;&#160;<br />
                            <asp:TextBox ID="txtapellido2_cony" runat="server" CssClass="textbox" TabIndex="5"
                                MaxLength="128" Style="text-transform: uppercase" Width="90%" /><asp:FilteredTextBoxExtender
                                    ID="FilteredTextBoxExtender4" runat="server" Enabled="True" TargetControlID="txtapellido2_cony"
                                    ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyzüÜáéíóúÁÉÍÓ " />
                        </td>
                        <td style="text-align: left;">Fecha Expedición <br />
                            <asp:TextBox ID="txtFechaExp_Cony" class="textbox" type="date" runat="server" name="user_date" />
                        </td>
                        <td style="text-align: left;">Ciudad Expedición <br />
                            <asp:DropDownList ID="ddlcuidadExp_Cony" runat="server" Width="170px" CssClass="textbox"
                                AppendDataBoundItems="True" TabIndex="8">
                                <asp:ListItem Value="0" Text="Seleccione un item"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left;">Sexo *<br />
                            <asp:DropDownList ID="ddlsexo" runat="server" AppendDataBoundItems="True"
                                CssClass="textbox required" Width="170px" TabIndex="9" required="required">
                                <asp:ListItem Value="" Text="Seleccione un item"></asp:ListItem>
                                <asp:ListItem Value="F" Text="FEMENINO"></asp:ListItem>
                                <asp:ListItem Value="M" Text="MASCULINO"></asp:ListItem>
                            </asp:DropDownList>
                        </td>

                        <td style="text-align: left;">Fecha Nacimiento<br />
                            <asp:TextBox ID="txtfechaNac_Cony" class="textbox" type="date" runat="server" name="user_date" onblur="edad(this.value)" TabIndex="10" />
                        </td>
                        <td style="text-align: left;">Edad del Conyuge<br />
                            <asp:TextBox ID="txtEdad_Cony" runat="server" CssClass="textbox" Enabled="False"
                                Width="40px"></asp:TextBox>
                            Años
                        </td>
                        <td style="text-align: left;">Lugar de Nacimiento <br />
                            <asp:DropDownList ID="ddlLugNacimiento_Cony" runat="server" Width="170px" CssClass="textbox"
                                AppendDataBoundItems="True" TabIndex="11">
                                <asp:ListItem Value="" Text="Seleccione un item"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left;">Email<br />
                            <asp:TextBox ID="txtemail_cony" runat="server" CssClass="textbox" TabIndex="12" MaxLength="128"
                                Width="90%" /><asp:RegularExpressionValidator ID="rfvEmailConyuge" runat="server"
                                    ControlToValidate="txtemail_cony" Display="Dynamic" ErrorMessage="E-Mail no valido!"
                                    ForeColor="Red" Style="font-size: xx-small" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></td>
                        <td style="text-align: left;">Celular&#160;<br />
                            <asp:TextBox ID="txtCell_cony" runat="server" CssClass="textbox" TabIndex="13" MaxLength="50" /><asp:FilteredTextBoxExtender
                                ID="fte62" runat="server" Enabled="True" FilterType="Numbers, Custom" TargetControlID="txtCell_cony"
                                ValidChars="" />
                        </td>
                        <td style="text-align: left;">Estrato <br />
                            <asp:TextBox ID="txtEstrato_Cony" runat="server" CssClass="textbox" TabIndex="14" MaxLength="2"
                                Width="50%" />
                            <asp:FilteredTextBoxExtender
                                ID="ft63" runat="server" Enabled="True" FilterType="Numbers, Custom" TargetControlID="txtEstrato_Cony"
                                ValidChars="" />
                        </td>
                        <td style="width: 25%; text-align: left">Ocupación <br/>
                            <asp:DropDownList ID="ddlOcupacion_Cony" runat="server" CssClass="textbox" TabIndex="15" Width="170px">
                                <asp:ListItem Value="">Seleccione un item</asp:ListItem>
                                <asp:ListItem Value="1">Empleado</asp:ListItem>
                                <asp:ListItem Value="2">Independiente</asp:ListItem>
                                <asp:ListItem Value="3">Pensionado</asp:ListItem>
                                <asp:ListItem Value="4">Estudiante</asp:ListItem>
                                <asp:ListItem Value="5">Hogar</asp:ListItem>
                                <asp:ListItem Value="6">Cesante</asp:ListItem>
                                <asp:ListItem Value="7">Desempleado</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="text-align: left;" colspan="4">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left;"><strong>Información Laboral</strong></td>
                    </tr>
                    <tr>
                        <td style="text-align: left;" colspan="2">Empresa <br />
                            <asp:TextBox ID="txtempresa_cony" runat="server" CssClass="textbox" TabIndex="16"
                                MaxLength="128" Style="text-transform: uppercase" Width="85%" />
                        </td>
                        <td style="text-align: left;">Tel. Empresa 
                        <br />
                            <asp:TextBox ID="txtTelefonoEmp_Cony" runat="server" CssClass="textbox" TabIndex="17" MaxLength="20" />
                            <asp:FilteredTextBoxExtender
                                ID="fte61" runat="server" Enabled="True" FilterType="Numbers, Custom" TargetControlID="txtTelefonoEmp_Cony"
                                ValidChars="" />
                        </td>
                        <td style="text-align: left;">Tipo Contrato<br />
                            <asp:DropDownList ID="ddlcontrato_cony" runat="server" Width="170px" CssClass="textbox"
                                AppendDataBoundItems="True" TabIndex="18">
                                <asp:ListItem Value="" Text="Seleccione un item"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left;">Cargo <br />
                            <asp:DropDownList ID="ddlCargo_cony" runat="server" Width="170px" CssClass="textbox"
                                AppendDataBoundItems="True" TabIndex="19">
                                <asp:ListItem Value="" Text="Seleccione un item"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left;">Antiguedad Cargo (Meses)<br />
                            <asp:TextBox ID="txtantiguedad_cony" runat="server" CssClass="textbox" TabIndex="20"
                                MaxLength="8" /><asp:FilteredTextBoxExtender ID="fte63" runat="server" Enabled="True"
                                    FilterType="Numbers, Custom" TargetControlID="txtantiguedad_cony" ValidChars="" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="txtIdent_cony" EventName="TextChanged" />
            </Triggers>
        </asp:UpdatePanel>

        <div id="Contenido">
            <asp:UpdatePanel ID="UdpContenido" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:AccordionPane ID="AccordionPane2" runat="server" Visible="true">
                    <Content>
                        <asp:UpdatePanel ID="updConyuge" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:CheckBox ID="chkConyuge" runat="server" AutoPostBack="true" ForeColor="Red"
                                    OnCheckedChanged="chkConyuge_CheckedChanged" Text="&lt;strong&gt;Desea Activar los Datos del Conyuge?&lt;/strong&gt;" /><br />
                                <asp:Panel ID="PanelConyuge" runat="server">
                                    <table width="100%">
                                        <tr>
                                            <td style="text-align: left; width: 100%">Dirección
																<asp:TextBox ID="txtdirec_cony" runat="server" CssClass="textbox" Width="60%"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="chkConyuge" EventName="CheckedChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </Content>
                </asp:AccordionPane>
                <table style="text-align: left; width: 100%; padding-top: 15px; padding-bottom: 15px;">
                    <tr style="text-align: center;">
                        <td colspan="8" style="color: #FFFFFF; background-color: #5295fa; height: 30px; width: 100%;">
                            <strong>Beneficiarios</strong>
                        </td>
                    </tr>
                </table>
                <asp:Button ID="btnAddRow" runat="server" CssClass="btn8" TabIndex="21" OnClick="btnAddRow_Click"
                    Text="+ Adicionar Detalle" UseSubmitBehavior="false" />
                <asp:GridView ID="gvBeneficiarios"
                    runat="server" TabIndex="50" AutoGenerateColumns="false" BackColor="White"
                    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="0" DataKeyNames="idbeneficiario"
                    ForeColor="Black" GridLines="Both" OnRowDataBound="gvBeneficiarios_RowDataBound" OnRowDeleting="gvBeneficiarios_RowDeleting"
                    PageSize="10" ShowFooter="True" Style="font-size: xx-small; padding-top: 10px;" Width="80%">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />
                        <asp:TemplateField HeaderText="Parentesco" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblidbeneficiario" runat="server" Text='<%# Bind("idbeneficiario") %>'
                                    Visible="false"></asp:Label><asp:Label ID="lblParentezco" runat="server" Text='<%# Bind("parentesco") %>'
                                        Visible="false"></asp:Label><cc1:DropDownListGrid ID="ddlParentezco" runat="server"
                                            AppendDataBoundItems="True" CommandArgument="<%#Container.DataItemIndex %>" CssClass="textbox"
                                            Style="text-align: left" Width="185px">
                                        </cc1:DropDownListGrid>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Identificación" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <cc1:TextBoxGrid ID="txtIdentificacion" runat="server" CssClass="textbox" Style="text-align: left"
                                    Text='<%# Bind("identificacion_ben") %>' Width="109px" OnTextChanged="TxtIde_TextChanged" AutoPostBack="true" CommandArgument="<%#Container.DataItemIndex %>"> </cc1:TextBoxGrid>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="110px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nombres y Apellidos" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" Style="text-align: left"
                                    Text='<%# Bind("nombre_ben") %>' Width="260px"> </asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="260px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fecha Nacimiento" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <uc1:fecha ID="txtFechaNacimientoBen" runat="server" cssclass="textbox" style="font-size: xx-small; text-align: left"
                                    Text='<%# Eval("fecha_nacimiento_ben", "{0:" + FormatoFecha() + "}") %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="% Ben." ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:TextBox ID="txtPorcentaje" runat="server" AutoPostBack_="True" CssClass="textbox" onkeyup="validarempresa(this.value)" onkeypress="return isNumber(event)"
                                    Enabled="True" Habilitado="True" Text='<%# Eval("porcentaje_ben") %>' Width_="80" />
                                <%--           <uc1:decimalesGridRow ID="txtPorcentaje" runat="server" AutoPostBack_="True" cssclass="textbox"
                                    Enabled="True" Habilitado="True" Text='<%# Eval("porcentaje_ben") %>' Width_="80" />--%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" Width="100px" />
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
                <table style="text-align: left; width: 100%; padding-top: 15px; padding-bottom: 15px;">
                    <tr style="text-align: center;">
                        <td colspan="8" style="color: #FFFFFF; background-color: #5295fa; height: 30px; width: 100%;">
                            <strong>Parentescos</strong>
                        </td>
                    </tr>
                </table>
                <div>
                    <asp:Label ID="lblestatus" Text="" runat="server" Style="color: #ff6a00" />
                </div>
                <asp:Button ID="btnAgregarFilaParentesco" runat="server" CssClass="btn8" TabIndex="22" OnClick="btnAgregarFilaParentesco_Click" Text="+ Adicionar Detalle" UseSubmitBehavior="false" />
                <div style="width: 100%; overflow: scroll;">
                    <asp:GridView ID="gvParentescos"
                        runat="server" AllowPaging="True" AutoGenerateColumns="false" BackColor="White"
                        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="0" DataKeyNames="consecutivo"
                        ForeColor="Black" GridLines="Both" OnRowDataBound="gvParentescos_RowDataBound"
                        OnRowDeleting="gvParentescos_RowDeleting" PageSize="10" ShowFooter="True"
                        Style="font-size: xx-small; padding-top: 10px;" Width="80%">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />
                            <asp:TemplateField HeaderText="Parentesco" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblCodParentesco" runat="server" Text='<%# Bind("codigoparentesco") %>'
                                        Visible="false"></asp:Label>
                                    <cc1:DropDownListGrid ID="ddlParentesco" runat="server"
                                        AppendDataBoundItems="True" CommandArgument="<%#Container.DataItemIndex %>" CssClass="textbox"
                                        Style="text-align: left" Width="120px" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="120px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tipo Identificacion" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblTipoIdentificacion" runat="server" Text='<%# Bind("codigotipoidentificacion") %>'
                                        Visible="false"></asp:Label>
                                    <cc1:DropDownListGrid ID="ddlTipoIdentificacion" runat="server"
                                        AppendDataBoundItems="True" CommandArgument="<%#Container.DataItemIndex %>" CssClass="textbox"
                                        Style="text-align: left" Width="120px" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="120px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Identificacion" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtIdenficacion" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                        Text='<%# Bind("identificacion") %>' onkeypress="return isNumber(event)" Width="120px"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="120px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nombres" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                        Text='<%# Bind("nombresapellidos") %>' Width="200px"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="200px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Act. Economica" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblCodigoActividadEconomica" runat="server" Text='<%# Bind("codigoactividadnegocio") %>'
                                        Visible="false"></asp:Label>
                                    <cc1:DropDownListGrid ID="ddlActividadEconomica" runat="server"
                                        AppendDataBoundItems="True" CommandArgument="<%#Container.DataItemIndex %>" CssClass="textbox"
                                        Style="text-align: left" Width="120px" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="120px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Empresa" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtEmpresa" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                        Text='<%# Bind("empresa") %>' Width="200px"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="200px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cargo" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCargo" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                        Text='<%# Bind("cargo") %>' Width="200px"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="200px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ing. Mensual" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtIngresoMensual" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                        Text='<%# Bind("ingresomensual") %>' onkeypress="return isNumber(event)" Width="120px"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="120px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Estatus" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px">
                                <ItemTemplate>
                                    <asp:CheckBoxList ID="chListaEstatus" runat="server" RepeatDirection="Horizontal" Width="280px">
                                        <asp:ListItem Value="0" Text="Empleado de la O.S."></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Miembro de administración"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Miembro de control"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="Es PEP"></asp:ListItem>
                                    </asp:CheckBoxList>
                                </ItemTemplate>
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
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnAddRow" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnAgregarFilaParentesco" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        </div> 
        <%-- Fin Sección propia de la página --%>
        <%-- Inicio scripts --%>
        <script>
            document.getElementById("btnCancelar").addEventListener("click", saltarRequeridos);
            Alertando(0);

            function isNumber(evt) {
                evt = (evt) ? evt : window.event;
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
                return true;
            }

            function edad(Fecha) {
                fecha = new Date(Fecha)
                hoy = new Date()
                ed = parseInt((hoy - fecha) / 365 / 24 / 60 / 60 / 1000)
                document.getElementById('txtEdad_Cony').value = ed;
            }

            function checkDate(sender, args) {
                if (sender._selectedDate > new Date()) {
                    var hoy = new Date();
                    alert("Eliga una fecha inferior a la Actual! " + hoy.toDateString());
                    sender._selectedDate = new Date();
                    sender._textbox.set_Value(sender._selectedDate.format(sender._format));
                }
            }

            function tiempoMeses(Fecha) {
                fecha = new Date(Fecha)
                hoy = new Date()
                ed = parseInt((hoy - fecha) / 30 / 24 / 60 / 60 / 1000) // o / 1000 / 60 / 60 / 24 / 30 )
                var txta = document.getElementById('<%=txtantiguedad_cony.ClientID %>');
                txta.value = ed;
            }

            function isNumber(evt) {
                evt = (evt) ? evt : window.event;
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
                return true;
            }

            function validarempresa(valor) {

                var preTotal = 0;
                var total = 0;
                var ind = 0;
                var table = document.getElementById("gvBeneficiarios");
                var cont = 0;

                for (var fila = 1; fila <= table.rows.length - 2; fila++) {
                    ind = document.getElementById('gvBeneficiarios_txtPorcentaje_' + cont).value;
                    ind = !isNaN(ind) ? (ind * 1) : ind;
                    if (ind > 100) {
                        document.getElementById('gvBeneficiarios_txtPorcentaje_' + cont).value = "";
                    }
                    preTotal = document.getElementById('gvBeneficiarios_txtPorcentaje_' + cont).value;
                    preTotal = !isNaN(preTotal) ? (preTotal * 1) : 0;
                    total += preTotal;
                    cont++;
                }

                if (total > 100) {
                    alert("Modifique los porcentajes pertinentes para los beneficiarios sobrepasan 100%");
                }
            }

            function saltarRequeridos() {
                var elements = document.getElementsByClassName('required');
                for (var i = 0; i < elements.length; i++) {
                    elements[i].removeAttribute('required');
                }
            }
        </script>
        <%-- Fin scripts --%>
    </asp:Panel>
</asp:Content>
