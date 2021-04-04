<?php
$username =$_GET['name'];
$password = $_GET['code'];


//aynı urlde ise
if ($username=="admin"&&$password=="12345") {
session_start();
$_SESSION['username'] = $username;
$_SESSION['code'] = $password;
header("Location: adminpaneli.php");
exit;
	
}
else{
	$_SESSION['username'] = NULL;
$_SESSION['code'] = NULL;
	header("Location: index.htm");
exit;
}





?>