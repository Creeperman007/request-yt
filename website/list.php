<!DOCTYPE html>
<html>
  <head>
    <meta charset="utf-8">
    <title>List | Request system</title>
    <style>
    th, td {
      vertical-align: sub;
    }
    input[type=submit] {
      border: none;
      color: #0e1790;
      background-color: #DDDDDD;
    }
    form {
      margin: 0px;
    }
    .break {
      text-align: center;
      font-size: 20px;
      padding-top: 40px;
    }
    .back {
      margin-top: 30px;
    }
    </style>
  </head>
  <body>
    <table border="0">
      <tr>
        <th>Name</th>
        <th style="padding-left: 10px;">Song</th>
        <th></th>
      </tr>
<?php
//Automatic refresh in seconds (you can change the number)
header("Refresh:10");
//Variables
$session = $_GET["session"]; //Session (used for getting data from SQL)
require_once("config.php");
//SQL Commands
$conn = new mysqli($servername, $username, $password, $dbname);
if ($conn->connect_error)
{
    die("Connection failed: " . $conn->connect_error);
}
$sql = "SELECT * FROM `requests` WHERE session=$session AND state='active' ORDER BY id DESC";
$result = $conn->query($sql);
if ($result->num_rows > 0)
{
  while($row = $result->fetch_assoc())
  {
    list($name, $song) = preg_split('/¤/', $row["name"]);
    $ban = array('!request' => '', '!r' => '', '!Request' => '', '!R' => '');
    $song = strtr($song, $ban);
    $id = $row["id"];
    echo "<tr><td>$name</td><td style=\"padding-left: 10px;\">$song</td><td><form method=\"post\"><input type=\"submit\" name=\"sub\" value=\"Archive\"><input type=\"hidden\" name=\"id\" value=\"$id\"></form></td></tr>";
  }
}
else
{
  echo "<tr><td>0 results</td></tr>";
}
echo "<tr><th colspan=\"2\" class=\"break\">Archived</th></tr><tr><th>Name</th><th style=\"padding-left: 10px;\">Song</th></tr>";
$sql = "SELECT * FROM `requests` WHERE session=$session AND state='archived' ORDER BY id DESC";
$result = $conn->query($sql);
if ($result->num_rows > 0)
{
  while($row = $result->fetch_assoc())
  {
    list($name, $song) = preg_split('/¤/', $row["name"]);
    $ban = array('!request' => '', '!r' => '', '!Request' => '');
    $song = strtr($song, $ban);
    $id = $row["id"];
    echo "<tr><td>$name</td><td style=\"padding-left: 10px;\">$song</td></tr>";
  }
}
else
{
  echo "<tr><td>0 results</td></tr>";
}
$conn->close();

//Archiving data
if (isset($_POST["sub"]))
{
  $num = $_POST["id"];
  $conn = new mysqli($servername, $username, $password, $dbname);
  if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
  }
  $sql = "UPDATE `requests` SET state='archived' WHERE id=$num";
  if ($conn->query($sql) === true)
  {
    header("Refresh: 1");
  }
  $conn->close();
}
?>
    </table>
    <div class="back">
      <a href="/"><img src="resources/back.png" alt="Back" width="32px" height="32px" title="Back to main page"></a>
    </div>
  </body>
</html>
