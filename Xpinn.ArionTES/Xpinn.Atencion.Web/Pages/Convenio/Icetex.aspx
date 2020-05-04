<%@ Page Title=".: Convenio Icetex :." Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Icetex.aspx.cs" Inherits="icetex" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc2" %>
<%@ Register Src="~/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .abc {
            font-size: small;
            padding: 2px 5px;
        }

        .tableNormal {
            border-collapse: separate;
            border-spacing: 4px;
        }

        .TextTab {
            background-color: rgba(7, 7, 7, 0.2);
            color: #000;
            display: block;
            text-align: center;
            padding: 3px 0px;
        }

            .TextTab:hover {
                border-bottom: 1px solid #dddddd;
                background-color: rgba(48, 47, 47, 0.90);
                color: #fff;
            }

        .TexTab:Active {
            color: #555555;
            background-color: #ffffff;
            border: 1px solid #dddddd;
            border-bottom-color: transparent;
            cursor: default;
        }
    </style>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="panelGeneral" runat="server">
        <div class="col-xs-12">
            <div class="form-group">
                <div class="col-sm-12">
                    <asp:Label ID="lblAlerta" runat="server" Style="font-size: x-small; color: Red" />
                </div>

                <div class="col-sm-12">
                    <div class="col-sm-2 text-left">
                        Fecha de Corte :
                    </div>
                    <div class="col-sm-2">
                        <uc2:fecha ID="txtFecha" Enabled="false" runat="server" cssclass="form-control" />
                    </div>
                    <div class="col-sm-8">
                    </div>
                </div>
                <div class="col-sm-12">
                    <hr style="width: 100%; border-color: #2780e3;" />
                </div>
                <h5 class="text-primary" style="margin-top: 10px; margin-left: 15px;">Información del Asociado
                </h5>
                <table width="100%" class="tableNormal">
                    <tr>
                        <td style="width: 100%">
                            <div class="col-sm-12">
                                <div class="col-md-2 text-left">
                                    Identificación
                                </div>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="txtIdentificacion" Enabled="false" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-sm-1">
                                </div>
                                <div class="col-sm-2 text-left">
                                    Nombre
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtNombre" Enabled="false" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-sm-1">
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%">
                            <div class="col-sm-12">
                                <div class="col-sm-2 text-left">
                                    Ciudad
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtCiudad" Enabled="false" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-sm-2 text-left">
                                    Dirección
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtDireccion" Enabled="false" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-sm-1">
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="col-sm-12">
                <asp:Label ID="lblRangoFecha" runat="server" Style="font-size: x-small; color: Red" />
            </div>
            <asp:TabContainer ID="Tabs" runat="server" ActiveTabIndex="1" Width="100%" CssClass="CustomTabStyle">
                <asp:TabPanel ID="tabHistoria" runat="server" Style="background-color: #FFFFFF;">
                    <HeaderTemplate>
                        <div style="width: 150px;">
                            <asp:Label ID="Label1" runat="server" Text="Historial Convocatorias" class="TextTab"></asp:Label></div>
                    </HeaderTemplate>
                    <ContentTemplate>
                        <br />
                        <div class="col-sm-12" style="overflow: scroll; max-height: 500px">
                            <asp:Panel ID="panelGrid" runat="server">
                                <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" GridLines="Horizontal"
                                    CssClass="table table-hover table-inverse" ShowHeaderWhenEmpty="True" Width="100%"
                                    DataKeyNames="numero_credito,cod_persona,cod_convocatoria">
                                    <Columns>
                                        <asp:BoundField DataField="numero_credito" HeaderText="Codigo" Visible="False">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="cod_persona" HeaderText="Cod persona" Visible="False">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="nom_tipo_beneficiario" HeaderText="Tipo Beneficiario">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="identificacion" HeaderText="Doc Beneficiario">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="nombre" HeaderText="Nombre Beneficiario">
                                            <ItemStyle HorizontalAlign="Left" Width="250px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="cod_convocatoria" HeaderText="Convocatoria">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_solicitud" HeaderText="Fecha Convocatoria" DataFormatString="{0:d}">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="nom_tipo_programa" HeaderText="Tipo de estudio">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="nom_universidad" HeaderText="Universidad">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="nom_programa_univ" HeaderText="Programa">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="valor" HeaderText="Valor Aprobado" DataFormatString="{0:n0}">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="nom_estado" HeaderText="Estado">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="observacion" HeaderText="Observación">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                    </Columns>
                                    <HeaderStyle Font-Size="Small" />
                                </asp:GridView>
                            </asp:Panel>
                        </div>
                        <div class="col-sm-12" style="text-align: center; margin-top: 2%;">
                            <asp:Label ID="lblTotReg" runat="server" Visible="False"></asp:Label><asp:Label ID="lblInfo"
                                runat="server" Text="Su consulta no obtuvo ningún resultado" Visible="False"></asp:Label>
                        </div>
                        <div class="row"></div>
                        <hr />
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="tabConvocatoria" runat="server" Style="background-color: #FFFFFF;">
                    <HeaderTemplate>
                        <div style="text-align: center">
                            <asp:Label ID="lblConvocatoria" runat="server" Text="Convocatoria Actual " CssClass="TextTab"
                                Style="display: block; padding-left: 5px; padding-right: 5px"></asp:Label>
                        </div>
                    </HeaderTemplate>
                    <ContentTemplate>
                        <br />
                        <asp:Panel ID="panelConv1" runat="server" Visible="False">
                            <asp:GridView ID="gvRequisitos" runat="server" AutoGenerateColumns="False" CssClass="table table-hover table-inverse"
                                GridLines="Horizontal" ShowHeaderWhenEmpty="True" DataKeyNames="cod_convreq"
                                Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="cod_convreq" HeaderText="id" Visible="False">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="cod_convocatoria" HeaderText="Convocatoria" Visible="False">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="tipo_requisito" HeaderText="Tipo Requisito" Visible="False">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="descripcion" HeaderText="Pre-Requisitos">
                                        <ItemStyle HorizontalAlign="Left" Width="50%" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Obligatorio">
                                        <ItemTemplate>
                                            <asp:Label ID="lblObli" runat="server" Visible="false" Text='<%# Bind("obligatorio") %>' /><asp:ImageButton
                                                ID="imgObli" runat="server" ImageUrl="~/Imagenes/btnEstadoIcetex.png" Visible='<%# Convert.ToBoolean(Eval("obligatorio")) %>' />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Estado">
                                        <ItemTemplate>
                                            <asp:Label ID="lblImg" runat="server" Visible="false" Text='<%# Bind("IsVisible") %>' /><asp:ImageButton
                                                ID="imgIcetex" runat="server" ImageUrl="~/Imagenes/btnEstadoIcetex.png" Visible='<%# Convert.ToBoolean(Eval("IsVisible")) %>' />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="observacion" HeaderText="Observación">
                                        <ItemStyle HorizontalAlign="Left" Width="30%" />
                                    </asp:BoundField>
                                </Columns>
                                <HeaderStyle Font-Size="Small" />
                                <RowStyle CssClass="table" Font-Size="Small" />
                            </asp:GridView>
                            <div class="col-sm-12" style="text-align: center; padding: 10px; background-color: #FFFFFF;">
                                <asp:Label ID="lblInfoRequisitos" runat="server" Text="Actualmente no existen convocatorias a la que pueda acceder."
                                    Visible="False"></asp:Label>
                            </div>
                            <asp:Panel ID="panelReqFooter" runat="server">
                                <div style="text-align: left; margin-top: 2%;">
                                    <asp:Label ID="LblText" Text="Sr(a) Asociado si cumple con los requisitos por favor dar Clic en <strong> Continuar </strong> para seguir el proceso. "
                                        runat="server" Style="text-align: center;"></asp:Label>
                                </div>
                                <div style="text-align: left; margin-top: 1%;">
                                    <asp:Label ID="lblCod_Convocatoria" runat="server" Visible="False" /></div>
                            </asp:Panel>
                            <hr />
                        </asp:Panel>
                                     
                        <asp:Panel ID="panelConv2" runat="server" Visible="False">
                            <asp:UpdatePanel ID="updDataAfi" runat="server">
                                <ContentTemplate>
                                    <div class="col-sm-12">
                                        <br />
                                        <div class="col-md-2 text-left">Tipo de Beneficiario </div>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlTipoBeneficiario" runat="server" CssClass="form-control"
                                                Width="250px" OnSelectedIndexChanged="ddlTipoBeneficiario_SelectedIndexChanged"
                                                AutoPostBack="True" />
                                        </div>
                                        <div class="col-md-2 text-left">
                                            <asp:Label ID="lblTituloCredito" runat="server" Text="Número de Crédito" Visible="false" /></div>
                                        <div class="col-sm-5 text-left">
                                            <asp:Label ID="lblNumeroCredito" runat="server" Visible="false" /><asp:Label ID="lblCorreoEnvio" runat="server" Visible="false" /><asp:Label ID="lblCodEstado" runat="server" Visible="false" /></div>
                                    </div>
                                    <div class="col-sm-12">
                                        <hr style="width: 100%; border-color: #2780e3;" />
                                    </div>
                                    <div class="col-sm-12" style="margin-bottom: 3px"><strong>Datos Generales</strong> </div>
                                    <div class="col-sm-12" style="overflow: scroll;">
                                        <asp:GridView ID="gvDatosPersona" runat="server" AutoGenerateColumns="False" GridLines="Horizontal"
                                            ShowHeaderWhenEmpty="True" DataKeyNames="codigo" Width="100%" OnRowDataBound="gvDatosPersona_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Tipo Doc">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTipoDoc" runat="server" Text='<%# Bind("tipo_identificacion") %>'
                                                            Visible="false" /><asp:DropDownList ID="ddlTipoDoc" runat="server" CssClass="form-control abc"
                                                                Style="margin: 0px" DataSource="<%# ListaTipoIdentificacion() %>" DataTextField="descripcion"
                                                                DataValueField="idconsecutivo" Width="180px" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" CssClass="abc" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Nro Doc">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtNroDoc" runat="server" CssClass="form-control abc" Text='<%# Bind("identificacion") %>'
                                                            Width="110px" /><asp:FilteredTextBoxExtender ID="ftb13" runat="server" Enabled="True"
                                                                FilterType="Numbers, Custom" TargetControlID="txtNroDoc" ValidChars="-()." />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" CssClass="abc" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Apellido 1">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtApellido1" runat="server" CssClass="form-control abc" Text='<%# Bind("primer_apellido") %>'
                                                            Width="180px" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" CssClass="abc" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Apellido 2">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtApellido2" runat="server" CssClass="form-control abc" Text='<%# Bind("segundo_apellido") %>'
                                                            Width="180px" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" CssClass="abc" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Nombre 1">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtNombre1" runat="server" CssClass="form-control abc" Text='<%# Bind("primer_nombre") %>'
                                                            Width="180px" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" CssClass="abc" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Nombre 2">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtNombre2" runat="server" CssClass="form-control abc" Text='<%# Bind("segundo_nombre") %>'
                                                            Width="180px" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" CssClass="abc" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Dirección">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control abc" Text='<%# Bind("direccion") %>'
                                                            Width="250px" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Télefono">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control abc" Text='<%# Bind("telefono") %>'
                                                            Width="100px" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" CssClass="abc" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Correo Electrónico">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control abc" Text='<%# Bind("email") %>'
                                                            Width="250px" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" CssClass="abc" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Estrato">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtEstrato" runat="server" CssClass="form-control abc" MaxLength="1"
                                                            Text='<%# Bind("Estrato") %>' Width="80px" /><asp:FilteredTextBoxExtender ID="ftb12"
                                                                runat="server" Enabled="True" FilterType="Numbers, Custom" TargetControlID="txtEstrato"
                                                                ValidChars="" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" CssClass="abc" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle Font-Size="Small" HorizontalAlign="Center" />
                                        </asp:GridView>
                                    </div>
                                    <div class="col-sm-12" style="margin-bottom: 3px">
                                        <p class="text-danger">DESPLACE LA BARRA HACIA LA DERECHA PARA QUE OBSERVE TODOS LOS DATOS</p>
                                        <br />
                                        </div>
                                    <div class="col-sm-12">
                                        <strong>Datos del Crédito</strong> </div>
                                    <div class="col-sm-12" style="overflow: scroll;">
                                        <table width="100%">
                                            <thead>
                                                <tr>
                                                    <th>Universidad / I.E.S </th>
                                                    <th>Programa </th>
                                                    <th>Tipo de Programa </th>
                                                    <th>Valor de Programa </th>
                                                    <th>Periodos llevados a semestres </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td style="width: 25%">
                                                        <asp:DropDownList ID="ddlUniversidad" runat="server" CssClass="form-control"
                                                            AutoPostBack="True" Style="margin: 0px;" Width="90%" OnSelectedIndexChanged="ddlUniversidad_SelectedIndexChanged" /></td>
                                                    <td style="width: 25%">
                                                        <asp:DropDownList ID="ddlPrograma" runat="server" CssClass="form-control" Style="margin: 0px"
                                                            Width="90%" /></td>
                                                    <td style="width: 20%">
                                                        <asp:DropDownList ID="ddlTipoPrograma" runat="server" CssClass="form-control"
                                                            Style="margin: 0px;" Width="90%">
                                                            <asp:ListItem Value="1">Especialización(1 año)</asp:ListItem>
                                                            <asp:ListItem Value="2">Maestria(2 años)</asp:ListItem>
                                                        </asp:DropDownList></td>
                                                    <td style="width: 15%">
                                                        <uc1:decimales ID="txtValorPrograma" runat="server" Width_="90%" AutoPostBack_="false" />
                                                    </td>
                                                    <td style="width: 15%">
                                                        <asp:DropDownList ID="ddlPeriodos" runat="server" CssClass="form-control" Style="margin: 0px;"
                                                            Width="90%">
                                                            <asp:ListItem Value="1">1 Semestre</asp:ListItem>
                                                            <asp:ListItem Value="2">2 Semestre</asp:ListItem>
                                                            <asp:ListItem Value="3">3 Semestre</asp:ListItem>
                                                            <asp:ListItem Value="4">4 Semestre</asp:ListItem>
                                                        </asp:DropDownList></td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>                                     
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="col-sm-12">
                                <asp:Label ID="lblNum_Credito" runat="server" Visible="False" /></div>
                        </asp:Panel>
                        <asp:Panel ID="panelConfirmarData" runat="server" Visible="False">
                            <div class="col-sm-12">
                                <hr style="width: 100%; border-color: #2780e3;" />
                                </div>
                            <div class="col-sm-12">
                                <br />
                                <p>Por favor antes de grabar confirme los datos y si tiene alguna inconsistencia escríbala en  observaciones</p>
                                <br />
                                <div class="col-sm-6">
                                    Certifico que los datos anteriores están correctos:<br />
                                    <asp:RadioButtonList ID="rblConfirmacionData" runat="server" RepeatDirection="Horizontal" Width="250px">
                                        <asp:ListItem Value="1" Text="Si" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </div>
                                <div class="col-sm-6">
                                    Observación de inconformidad<br />
                                    <asp:TextBox ID="txtInconformidad" runat="server" Width="90%" TextMode="MultiLine" Height="35px"/>
                                </div>
                            </div>                            
                        </asp:Panel>


                        <asp:Panel ID="panelConv3" runat="server" Visible="False">
                            <div class="col-sm-12">
                                <strong>Documentos Requeridos</strong>
                                <br />
                                <p>
                                    <asp:Label runat="server" Text="Tamaño maximo del archivo a cargar (1 MB)"
                                        Style="font-size: x-small; color: Green; font-weight: 700;" />
                                </p>
                                <p>
                                    <asp:Label ID="lblmsjDocum" runat="server" Style="font-size: small; color: Green; font-weight: 500;" /></p>
                            </div>
                            <div class="col-sm-12">
                                <asp:GridView ID="gvDocumentosReq" runat="server" AutoGenerateColumns="False" CssClass="table table-hover table-inverse"
                                    GridLines="Horizontal" ShowHeaderWhenEmpty="True" DataKeyNames="cod_tipo_doc"
                                    Width="100%" OnRowDataBound="gvDocumentosReq_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="cod_tipo_doc" HeaderText="Cod Tipo Doc" Visible="False">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="descripcion" HeaderText="Tipo Documento">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Pregunta">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPegrunta" runat="server" Text='<%# Bind("pregunta") %>' /></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Respuesta">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkRespuesta" runat="server" /></ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Archivo">
                                            <ItemTemplate>
                                                <asp:FileUpload ID="fuArchivo" runat="server" /></ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="col-sm-12">
                                <asp:Label ID="lblInfoDocu" runat="server" Text="Actualmente no existen documentos requeridos para la convocatoria actual."
                                    Visible="False"></asp:Label>
                            </div>
                            <div class="col-sm-12 text-justify">
                                <h3>AUTORIZACIÓN PARA EL MANEJO DE DATOS PERSONALES</h3>
                                <p>Para efectos del tratamiento de los datos personales recolectados, en concordancia del Decreto 1377 de 2013, reglamentario de la Ley 1581 de 2012, COOACEDED, como responsable de los datos personales obtenidos a través de sus distintos canales de atención, solicita su autorización para que de manera libre, previa, expresa y voluntaria permitan continuar con su tratamiento. Tal autorización permitirá a COOACEDED, recolectar, transferir, almacenar, usar, circular, suprimir, compartir, actualizar y transmitir, de acuerdo con el Procedimiento para el tratamiento de los datos personales en procura de cumplir con las siguientes finalidades: </p>
                                <p>(1) validar la información en cumplimiento de la exigencia legal de conocimiento del Asociado y/o Beneficiario aplicable al CONVENIO INTERINSTITUCIONAL COOACEDED-ICETEX EN LA ULTIMA CONVOCATORIA.</p>
                                <p>(2) para el tratamiento de los datos personales protegidos por nuestro ordenamiento jurídico.</p>
                                <p>(3) para el tratamiento y protección de los datos de contacto (direcciones de correo físico, electrónico y teléfono).</p>
                                <p>(4) para solicitar y recibir de las Instituciones de Educación Superior y de las entidades de derecho público y/o empresas de carácter privado la información personal y académica.</p>
                                <p>El alcance de la autorización comprende la facultad para que COOACEDED le envíe mensajes con contenidos institucionales, notificaciones, información del estado del convenio y demás información relativa al CONVENIO INTERINSTITUCIONAL COOACEDED-ICETEX EN LA ULTIMA CONVOCATORIA, a través de correo electrónico, llamadas telefónicas y/o mensajes de texto al teléfono móvil. Los titulares podrán ejercer sus derechos de conocer, actualizar, rectificar y suprimir sus datos personales, a través de los canales dispuestos por el COOACEDED para la atención al público. </p>
                                        <p>&#160;&#160;&#160;&#160;
                                            <asp:CheckBox ID="chkAceptarTerminos" runat="server" OnCheckedChanged="chkAceptarTerminos_CheckedChanged" AutoPostBack="True" Text="&nbsp;&nbsp;&nbsp;&nbsp;Acepto" /></p>
                                        <br /> 
                                        <asp:LinkButton ID="btnGuardar" runat="server" CssClass="btn btn-default" Width="120px" ToolTip="Save" Visible="False"
                                            Style="border-radius: 0px; padding-left: 5px; padding-right: 5px; padding-top: 7px; padding-bottom: 7px; " OnClick="btnGuardarAnexos_Click">
                                            <div class="pull-left text-primary" style="padding-left:10px">
                                            <span class="glyphicon glyphicon-floppy-disk"></span></div>Grabar
                                        </asp:LinkButton>
                            </div>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </div>
    </asp:Panel>
    <asp:Panel ID="panelConv4" runat="server" Visible="False">
        <div class="col-sm-12" style="padding: 15px">
            <asp:Label ID="lblMensaje" runat="server" Style="font-size: medium" />
        </div>
    </asp:Panel>
    <asp:Panel ID="panelEnvio" runat="server" Visible="false">
        <div style="padding: 15px; background-color: #e6e6e6">
            <div class="container">
                <asp:Panel ID="panel1" runat="server" Style="background-color: White; padding: 10px">

                    <div style="text-align: center; width: 100%">
                        <h3>PRE - INSCRIPCION</h3>
                        <br />
                        <b style="font-size: x-small">DEL 02 al 13 Mayo del 2017</b>
                        <br />
                        <b>Convenio COOACEDED - ICETEX</b>
                        <h5>ULTIMA CONVOCATORIA</h5>
                        <br />
                    </div>

                    <div style="width: 100%; text-align: left">
                        <asp:Label ID="lblFechaTransac" runat="server" />
                        <br />
                        <br />
                        <br />
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 20%; text-align: left">Sr(a) Asociado
                                </td>
                                <td style="width: 80%; text-align: left">
                                    <b>
                                        <asp:Label ID="lblAsiciado" runat="server" /></b>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%; text-align: left">Cedula No.:
                                </td>
                                <td style="width: 80%; text-align: left">
                                    <b>
                                        <asp:Label ID="lblIdentifiAso" runat="server" /></b>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: left">Cordial Saludo</td>
                            </tr>
                        </table>
                    </div>
                    <div>
                        <hr style="width: 100%; margin-top: 4px; box-shadow: 0px 1px 0.5px rgba(0,0,0,.5);" />
                    </div>

                    <div style="width: 100%; text-align: justify">
                        <p>
                            Por medio de la presente le informamos que completo el proceso de Preinscripción exitosamente, de la Última Convocatoria COOACEDED-ICETEX, del 15 al 20 de mayo se estarán validando los datos y documentos suministrado por usted. Espere resultados para la posible continuación en el proceso de Inscripción, por este medio se le estará confirmando o informando sobre cualquier inconveniente en esta etapa.
                        </p>
                        <br />
                        <table style="width: 100%">
                            <tr>
                                <td colspan="2">
                                    <h4><b>Resumen de Información</b></h4>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%">Tipo Beneficiario :
                                </td>
                                <td style="width: 80%">
                                    <asp:Label ID="lblINFtipoBen" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%">
                                    <asp:Label ID="lblINFtitBen" runat="server" Text="Beneficiario :" Visible="false" />
                                </td>
                                <td style="width: 80%">
                                    <asp:Label ID="lblINFbenefi" runat="server" Visible="false" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%">Universidad :
                                </td>
                                <td style="width: 80%">
                                    <asp:Label ID="lblINFuniv" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%">Programa :
                                </td>
                                <td style="width: 80%">
                                    <asp:Label ID="lblINFprog" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%">Valor :
                                </td>
                                <td style="width: 80%">
                                    <asp:Label ID="lblINFval" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%">Estrato :
                                </td>
                                <td style="width: 80%">
                                    <asp:Label ID="lblINFestrato" runat="server" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <p>
                            La Junta Administradora validara en esta etapa lo siguiente:
                        </p>

                        <ul>
                            <li>Asociado hábil (Obligaciones al día y estado)</li>
                            <li>Tiempo de afiliación (mayor a un año)</li>
                            <li>No ser beneficiario de convocatorias anteriores</li>
                            <li>No tener crédito por la línea educativa o subsidiada</li>
                            <li>Estrato 1,2,3 (1 y 2 se financia 100% y el 3 el 50% )</li>
                            <li>Valor no sobrepase los 35 SMLV</li>
                            <li>No sobrepase dos años el programa de estudio</li>
                            <li>Empezar estudios en el segundo periodo de 2017</li>
                            <li>Estar inscrito en I.E.S. avalado por ICETEX</li>
                        </ul>
                        <br />
                        <br />
                        <p style="text-align: left">
                            Atentamente
                                    <br />
                        </p>
                        <p style="text-align: center">
                            Junta Administrativa del Fondo <b>COOACEDED</b>
                        </p>

                        <p style="text-align: left">
                            DATOS SUMINISTRADO:
                        </p>
                    </div>
                </asp:Panel>
            </div>

        </div>
    </asp:Panel>
     <asp:Panel ID="panelEnvioInscripcion" runat="server" Visible="false">
        <div style="padding: 15px; background-color: #e6e6e6">
            <div class="container">
                <asp:Panel ID="panel3" runat="server" Style="background-color: White; padding: 10px">

                    <div style="text-align: center; width: 100%">
                        <h3>INSCRIPCION</h3>
                        <br />
                        <b style="font-size: x-small">DEL 22 al 31 Mayo del 2017</b>
                        <br />
                        <b>Convenio COOACEDED - ICETEX</b>
                        <h5>ULTIMA CONVOCATORIA</h5>
                        <br />
                    </div>

                    <div style="width: 100%; text-align: left">
                        <asp:Label ID="lblFechaTransacIns" runat="server" />
                        <br />
                        <br />
                        <br />
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 20%; text-align: left">Sr(a) Asociado
                                </td>
                                <td style="width: 80%; text-align: left">
                                    <b>
                                        <asp:Label ID="lblAsiciadoIns" runat="server" /></b>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%; text-align: left">Cedula No.:
                                </td>
                                <td style="width: 80%; text-align: left">
                                    <b>
                                        <asp:Label ID="lblIdentifiAsoIns" runat="server" /></b>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: left">Cordial Saludo</td>
                            </tr>
                        </table>
                    </div>
                    <div>
                        <hr style="width: 100%; margin-top: 4px; box-shadow: 0px 1px 0.5px rgba(0,0,0,.5);" />
                    </div>

                    <div style="width: 100%; text-align: justify">
                        <p>
                            Por medio de la presente le informamos que completo el proceso de Inscripción exitosamente de la Última Convocatoria COOACEDED-ICETEX. Del 01 al 04 de junio se estarán validando los datos y documentos suministrado por usted y en la cual por este medio se le estará confirmando o informando sobre cualquier inconveniente en esta etapa.
                        </p>
                        <br />
                        <table style="width: 100%">
                            <tr>
                                <td colspan="2">
                                    <h4><b>Resumen de Información</b></h4>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%">Tipo Beneficiario :
                                </td>
                                <td style="width: 80%">
                                    <asp:Label ID="lblINFtipoBenIns" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%">
                                    <asp:Label ID="lblINFtitBenIns" runat="server" Text="Beneficiario :" Visible="false" />
                                </td>
                                <td style="width: 80%">
                                    <asp:Label ID="lblINFbenefiIns" runat="server" Visible="false" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%">Universidad :
                                </td>
                                <td style="width: 80%">
                                    <asp:Label ID="lblINFunivIns" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%">Programa :
                                </td>
                                <td style="width: 80%">
                                    <asp:Label ID="lblINFprogIns" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%">Valor :
                                </td>
                                <td style="width: 80%">
                                    <asp:Label ID="lblINFvalIns" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%">Estrato :
                                </td>
                                <td style="width: 80%">
                                    <asp:Label ID="lblINFestratoIns" runat="server" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <p>
                            La Junta Administradora validara en esta etapa lo siguiente:
                        </p>

                        <ul>
                            <li>Asociado hábil (Obligaciones al día y estado)</li>
                            <li>Tiempo de afiliación (mayor a un año)</li>
                            <li>No ser beneficiario de convocatorias anteriores</li>
                            <li>No tener crédito por la línea educativa o subsidiada</li>
                            <li>Estrato 1,2,3 (1 y 2 se financia 100% y el 3 el 50% )</li>
                            <li>Valor no sobrepase los 35 SMLV</li>
                            <li>No sobrepase dos años el programa de estudio</li>
                            <li>Empezar estudios en el segundo periodo de 2017</li>
                            <li>Estar inscrito en I.E.S. avalado por ICETEX</li>
                        </ul>
                        <br />
                        <br />
                        <p style="text-align: left">
                            Atentamente
                                    <br />
                        </p>
                        <p style="text-align: center">
                            Junta Administrativa del Fondo <b>COOACEDED – ICETEX ULTIMA CONVOCATORIA</b>
                        </p>

                        <p style="text-align: left">
                            DATOS SUMINISTRADO:
                        </p>
                    </div>
                </asp:Panel>
            </div>

        </div>
    </asp:Panel>

</asp:Content>
