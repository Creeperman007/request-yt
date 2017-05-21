<html>
  <head>
    <meta charset="utf-8">
    <title>Request system</title>
    <style>
    /* The Modal (background) */
    .modal {
      display: none;
      position: fixed;
      z-index: 1;
      padding-top: 100px;
      left: 0;
      top: 0;
      width: 100%;
      height: 100%;
      overflow: scroll;
      background-color: rgb(0,0,0);
      background-color: rgba(0,0,0,0.4);
    }
    /* Modal Content */
    .modal-content {
      font-family: Arial;
      text-align: justify;
      background-color: #fefefe;
      margin: auto;
      padding: 20px;
      border: 1px solid #888;
      width: 50%;
    }

    /* The Close Button */
    .close {
      color: #aaaaaa;
      float: right;
      font-size: 28px;
      font-weight: bold;
    }

    .close:hover,
    .close:focus {
      color: #000;
      text-decoration: none;
      cursor: pointer;
    }
    input[type=submit] {
      border: none;
      color: #14B20E;
      background-color: #DDDDDD;
      padding: 3px 5px 3px 5px;
    }
    </style>
  </head>
  <body>
    <form method="get" action="list.php">
      <input type="number" name="session" required="required" placeholder="Session ID" min="1" max="9999">
      <input type="submit" value="Go!">
    </form>
    <a href="https://github.com/Creeperman007/requests-yt" target="_blank"><img src="resources/github-64px.png" alt="GitHub repo" title="GitHub repository" width="32px" height="32px"></a>
    <a href="https://twitter.com/creeperman007" target="_blank"><img src="resources/twitter.png" alt="Twitter" title="Author's twitter" width="32px" height="32px"></a>
    <a href="#" id="myBtn"><img src="resources/license.png" alt="Copyright symbol" width="32px" height="32px" title="License"></a>

    <!-- START License popout -->
    <div id="myModal" class="modal">
      <div class="modal-content">
        <span class="close">&times;</span>
        <?php include("resources/license.html"); ?>
      </div>
    </div>
    <!-- END License popout -->
    <script>
    var modal = document.getElementById('myModal');
    var btn = document.getElementById("myBtn");
    var span = document.getElementsByClassName("close")[0];
    btn.onclick = function()
    {
      modal.style.display = "block";
    }
    span.onclick = function()
    {
      modal.style.display = "none";
    }
    window.onclick = function(event)
    {
      if (event.target == modal)
      {
        modal.style.display = "none";
      }
    }
    </script>
  </body>
</html>
