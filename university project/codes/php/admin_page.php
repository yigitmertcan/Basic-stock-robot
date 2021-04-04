<?php
$servername = "localhost:8888";
$username = "root";
$password = "root";
$dbname = "robot";
 $link = new PDO("mysql:host=$servername;dbname=$dbname", $username, $password);
session_start();
//aynı urlde ise
if ($_SESSION['username']=="admin"&&$_SESSION['code'] ==="12345") {
$link->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);

    $sql = "UPDATE durumcu SET  Durum='Okunuyor' WHERE id=1";

    // Prepare statement
    $stmt = $link->prepare($sql);

    // execute the query
    $stmt->execute();
    header("Location: adminpaneli.php");
exit;
}
else{	
	mysqli_close($link);
	header("Location: index.htm");
exit;
}
?>