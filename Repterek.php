
<?php
echo "<html lang='en'>";
echo " <head>";
echo "  <title>Légitársaságok statisztikája</title>";
echo "  <meta charset='utf-8'>";
echo "   <meta name='viewport' content='width=device-width, initial-scale=1'>";
echo "   <link rel='stylesheet' href='https://maxcdn.bootstrapcdn.com/bootstrap/4.5.0/css/bootstrap.min.css'>";
echo "   <script src='https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js'></script>";
echo "   <script src='https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js'></script>";
echo "   <script src='https://maxcdn.bootstrapcdn.com/bootstrap/4.5.0/js/bootstrap.min.js'></script>";
echo "   <link rel='stylesheet' href='style.css' type='text/css'>";

echo "</head>";
echo "<body>";
 echo "   <div class='jumbotron text-center bg-warning text-success fejlec'>";
 echo "     <h1>Repterek</h1>"; 
 echo "     </div>";
    
echo "    <div class='tartalom kozep'>";
echo "    <nav aria-label='breadcrumb'>";
echo "      <ol class='breadcrumb'>";
echo "        <li class='breadcrumb-item'><a href='#'>Főoldal</a></li>";
echo "    <li class='breadcrumb-item active' aria-current='page'>Repterek</li>";
echo "      </ol>";
echo "    </nav>";
echo "    <ul>";

$myfile = fopen("carrier_name.txt", "r") or die("Unable to open file!");
//echo fread($myfile,filesize("carrier_name.txt"))."\n";
echo" <h2>";
echo fgets($myfile)."<br>";
echo" </h2>";
while(!feof($myfile)){
echo" <div class ='text-success lista'>";
    $sor=fgets($myfile);
    echo "<a class='text-success' href='".$sor."html'>". $sor ."<br>";
echo"</a> </div>";}
fclose($myfile);

echo "    </ul>";
echo "    </ul>";
echo "    </div>";
echo "</body>";
echo "</html>";
