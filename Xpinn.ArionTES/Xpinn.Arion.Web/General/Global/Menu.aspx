<!doctype html>

<html lang="de">
    <head>                  
        <link href="../../Styles/Styles.css" rel="stylesheet" type="text/css" />    
        <script type="text/javascript" src="../../Scripts/jquery.min.js"></script>    
        <script type="text/javascript" src="../../Scripts/jquery.hoveraccordion.min.js"></script> 
        <script type="text/javascript">
            $(document).ready(function () {
                // Setup HoverAccordion for Example 2 with some custom options
                $('#menuPrincipal').hoverAccordion({
                    activateitem: '1',
                    speed: 'fast'
                });
                $('#menuPrincipal').children('li:first').addClass('firstitem');
                $('#menuPrincipal').children('li:last').addClass('lastitem');
            });  
        </script>
    </head>
    <body>
        <div id="content">      
            <ul id="menuPrincipal">        
                <li><a href="#">Gestión</a>            
                    <ul>                
                        <li>Content #1 - Lorem ipsum dolor sit amet, consetetur sadipscing elitr
                        </li>            
                    </ul>        
                 </li>        
                 <li><a href="#">Parámetros</a>            
                    <ul>               
                        <li>Content #2 - At vero eos et accusam et justo duo dolores et ea rebum.
                        </li>     
                    </ul>  
                 </li> 
                 <li><a href="#">Reportes</a>    
                    <ul>         
                        <li>Content #3 - Sed diam nonumy eirmod tempor invidunt ut labore
                        </li>     
                    </ul> 
                 </li>     
            </ul>
        </div
    ></body>
</html>
