<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fotohuella.aspx.cs" Inherits="Page_Aportes_Biometria_fotohuella" %>

<!DOCTYPE html>
<html>
<head>
 
  <title>Interaction Cam</title>
<link rel="stylesheet" href="interactioncam.css">
</head>
<body>

  <div id="videocontainer">
  <video id="video"></video>
  </div>
  <canvas id="canvas"></canvas>
  <div id="controls"> 
    <button id="resetbutton">&lt; Uh, let's try that again&hellip;</button>
    <input type="button" id="btnSave" name="btnSave" value="Save the canvas to server" />

  </div>
  <div id="cover"></div>
  <div id="uploading">Uploading&hellip;</div>
  <div id="uploaded">
  



    <form id="imgurform"> 
      Uploaded: <input type="text" id="url"> 
      <a href="#">link</a>
    </form>
    <button id="startbutton">&lt; Take another</button>
     
  </div>
  <footer>
    <p>
      Written by <a href="http://twitter.com/codepo8"></a> - 
      <a href="https://github.com/codepo8/interaction-cam"></a>
    </p>
   
  </footer>
  <audio src="snap.wav"></audio>
  <!-- http://www.freesound.org/people/thecheeseman/sounds/51360/ -->
  <audio src="rip.wav"></audio>
  <!-- http://www.freesound.org/people/aboe/sounds/68900/ -->
  <audio src="takeoff.wav"></audio>
  <!-- http://www.freesound.org/people/duckduckpony/sounds/130508/ -->
  <script src="interactioncam.js"></script> 
   <meta http-equiv="Content-Type" content="text/html; charset=utf-8">



<meta http-equiv="Content-Type" content="text/html; charset=utf-8">



 <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.4.2.min.js"           

    temp_src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.4.2.min.js" type="text/javascript"></script>
  <script type="text/javascript">

        // Send the canvas image to the server.

        $(function () {

            $("#btnSave").click(function () {

                var image = document.getElementById("canvas").toDataURL("image/png");

                image = image.replace('data:image/png;base64,', '');

                $.ajax({

                    type: 'POST',

                    url: 'foto.aspx/UploadImage',

                    data: '{ "imageData" : "' + image + '" }',

                    contentType: 'application/json; charset=utf-8',

                    dataType: 'json',
                    success: function (msg) {

                        alert('Foto guardada correctamente !');

                    }

                });

            });

        });
        </script>
</body>
</html>
