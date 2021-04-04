<?php
$servername = "localhost:8888";
$username = "root";
$password = "root";
$dbname = "robot";
 $link = new PDO("mysql:host=$servername;dbname=$dbname", $username, $password);
session_start();
$satirci=$_GET['st'];
$sutuncu=$_GET['su'];
$satm=$_GET['stm'];
$sutm=$_GET['sum'];
$dol=$_GET['dol'];
$kut=$_GET['kut'];


//aynı urlde ise
if ($_SESSION['username']=="admin"&&$_SESSION['code'] ==="12345") {
$link->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);

    $sql = "UPDATE dolaporanlari SET satir=$satirci , sutun=$sutuncu , sutunmetre=$sutm,satirmetre=$satm,dolabinkutuboyu=$kut, dolabinicboyu=$dol WHERE id=1";

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