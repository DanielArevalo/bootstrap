<%@ Control Language="C#" AutoEventWireup="true" CodeFile="header.ascx.cs" Inherits="ctrl_header" %>

<header style="width: 100%; height: auto;">
    <div id="pruebas" class="line_ambiente pruebas" <%=System.Configuration.ConfigurationManager.AppSettings["Ambiente"] == "1" ? "style='display: none;'" : ""%>>Ambiente de pruebas</div>
    <div id="produccion" class="line_ambiente produccion" <%=System.Configuration.ConfigurationManager.AppSettings["Ambiente"] != "1" ? "style='display: none;'" : ""%>>Ambiente de Produccion</div>
    
    <div class="header divTable">
        <div class="divTableBody">

            <div class="divTableRow">
                <div class="divTableCell">
                    <div class="div_logo">
                        <asp:Image ID="Image1" class="img_logo" ImageUrl="~/Images/logoInterna.jpg" runat="server"></asp:Image>
                    </div>
                </div>
                <div class="divTableCell" id="clEmpresa">
                    <asp:Label ID="lblEmpresa" CssClass="lblRol txtBussines" runat="server" Text=""></asp:Label><br />
                    <nav class=" topnav-right">
                        <!-- Aqui estamos iniciando la nueva etiqueta nav -->
                        <ul class="nav">
                            <li style="color: #fff; background-color: none; text-align: center;">
                                <asp:LinkButton runat="server" href="" Style="color: #fff; background-color: none;">
                            <asp:label runat="server">Idioma &nbsp
                                <div class="imgSpanish"></div>
                                <asp:Image runat="server" style="margin:0px 5px; margin-left:35px; margin-right:10px; height:15px; display:block; float:left;" ImageUrl="~/Images/Español.png" />
                            </asp:label>
                                </asp:LinkButton>
                                <div class="trinagulo" style="height: 0px; width: 0px; border-left: 10px solid transparent; border-right: 10px solid transparent; border-top: 0px solid transparent; border-bottom: 8px solid #fff; margin: 0px auto;"></div>
                                <ul class="links" id="equipos1" style="z-index: 1;">
                                    <li style="border-bottom: 1px solid #eee;" onclick="Buscar">
                                        <asp:LinkButton ID="ES" CommandName="es-CO" runat="server" OnClick="Buscar" class="a" Style="color: #000;"><asp:Image style="margin-right:10px; margin-top:3px;" runat="server" ImageUrl="~/Images/Español.png"/><asp:Label runat="server" Value ="es-CO" Text="<%$  Resources:Resource,Español %>"></asp:Label></asp:LinkButton>
                                    </li>
                                    <li style="border-bottom: 1px solid #eee;" onclick="Buscar">
                                        <asp:LinkButton ID="EN" CommandName="en-US" runat="server" OnClick="Buscar" class="a" Style="color: #000;"><asp:Image style="margin-right:10px; margin-top:3px;" runat="server" ImageUrl="~/Images/Ingles.png"/><asp:Label  runat="server" Value ="en-US" Text="<%$  Resources:Resource,Ingles %>"></asp:Label></asp:LinkButton>
                                    </li>
                                    <li style="border-bottom: 1px solid #eee;" onclick="Buscar">
                                        <asp:LinkButton ID="FR" CommandName="fr-FR" runat="server" OnClick="Buscar" class="a" Style="color: #000;"><asp:Image style="margin-right:10px; margin-top:3px;" runat="server" ImageUrl="~/Images/Frances.png"/><asp:Label runat="server" Value ="fr-FR" Text="<%$  Resources:Resource,Frances %>"></asp:Label></asp:LinkButton>
                                    </li>
                                </ul>
                            </li>
                            <li style="color: #fff; background-color: none; text-align: center;">
                                <asp:Label runat="server" Style="display: block; float: left; margin-left: 35px;">Usuario &nbsp<div style="transform:rotate(180deg); margin-top:4px; display:block; float:right; height: 0px; width: 0px; border-left:6px solid transparent; border-right:6px solid transparent; border-top:6px solid transparent; border-bottom:6px solid #fff;"></div></asp:Label>
                                <br>
                                <div class="trinagulo" style="height: 0px; width: 0px; border-left: 10px solid transparent; border-right: 10px solid transparent; border-top: 0px solid transparent; border-bottom: 8px solid #fff; margin: 0px auto;"></div>
                                <ul class="links" id="equipos1" style="z-index: 1;">
                                    <li style="border-bottom: 1px solid #eee;">
                                        <asp:LinkButton runat="server" class="a" Style="color: #000;">
                                            <asp:Label Style="font-size: 11px; color: #353535; font-style: normal; font-weight: 600" runat="server" Text="<%$  Resources:Resource,Usuario %> "></asp:Label>:<asp:Label ID="lblUser" CssClass="lblRol" runat="server" Text="" Style="font-style: normal; font-size: 11px;"></asp:Label>
                                        </asp:LinkButton>
                                    </li>
                                    <li style="border-bottom: 1px solid #eee;">
                                        <asp:LinkButton runat="server" class="a" Style="color: #000;">
                                            <asp:Label Style="font-size: 11px; color: #353535; font-style: normal; font-weight: 600" runat="server" Text="<%$  Resources:Resource,Tipo_Perfil %> "></asp:Label>:<asp:Label ID="lblRol" CssClass="lblRol" runat="server" Text="" Style="font-style: normal; font-size: 11px;"></asp:Label>
                                        </asp:LinkButton>
                                    </li>
                                    <li style="border-bottom: 1px solid #eee;">
                                        <asp:LinkButton runat="server" class="a" Style="color: #000;">
                                            <asp:Label Style="font-size: 11px; color: #353535; font-style: normal; font-weight: 600" runat="server" Text="<%$  Resources:Resource,Oficina %> "></asp:Label>:<asp:Label ID="lblOficina" CssClass="lblRol" runat="server" Text="" Style="font-style: normal; font-size: 11px;"></asp:Label>
                                        </asp:LinkButton>
                                    </li>
                                    <li style="text-align: center;">
                                        <asp:LinkButton runat="server" ID="LinkCerrarSession" CommandName="fr-FR" OnClick="hlkCerrar_Click" class="a" Style="color: #000; font-size: 11px; font-weight: 600">
                                            <asp:Label ID="hlkCerrar" onclick="hlkCerrar_Click" runat="server" Text="<%$  Resources:Resource,Cerrar_Sesion %>"></asp:Label>
                                        </asp:LinkButton>
                                    </li>
                                </ul>
                            </li>
                        </ul>
                    </nav>
                </div>
            </div>
        </div>
    </div>
</header>

