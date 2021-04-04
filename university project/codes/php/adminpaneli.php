<html>
<head>
<title>STOK ROBOTU v1.0</title>
</head>
<body>
<center><big>Admin Paneli</big></center>
<center>
<?php

$servername = "localhost:8888";
$username = "root";
$password = "root";
$dbname = "robot";
 $conn = new mysqli($servername, $username, $password, $dbname);
$sql = "SELECT id, Durum, adet FROM durumcu";
$sc = "SELECT satir,sutun,satirmetre,sutunmetre,dolabinicboyu,id,dolabinkutuboyu FROM dolaporanlari";
$result = $conn->query($sql);
$results = $conn->query($sc);
if ($result->num_rows > 0) {
    // output data of each row
    while($row = $result->fetch_assoc()) {
        echo  "  Durum: " . $row["Durum"]. "- Adet: " . $row["adet"]. "<br>";
    }
} else {
    echo "0 results";
}
if ($results->num_rows > 0) {
    // output data of each row
    while($rows = $results->fetch_assoc()) {
        echo "satir sayisi: " . $rows["satir"]. " - Satir (metre): " . $rows["satirmetre"]. "- Kutu boyu: " . $rows["dolabinkutuboyu"]. "<br>";
        echo "sutun sayisi: " . $rows["sutun"]. " - sutun(metre): " . $rows["sutunmetre"]. "- Dolabin eni: " . $rows["dolabinicboyu"]. "<br>";
    }
} else {
    echo "0 results";
}	

?>	<br>
<form action="admin_page.php">
<input type="submit" value="Oku"><br></form>
<form action="deger.htm">
    <input type="submit" value="Deger gir">
</form>
</center>	
</body>


</html>