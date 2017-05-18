<?php
/**
 * --------------------------------------------------------------------------------------
 * Lates Robot PHP Mode
 * @author: Lucas Moraes
 * @date: 18/05/2017
 * @do: this robot downloads curriculum files from lattes website
 * ---------------------------------------------------------------------------------------
 */

/**
 *
 * php lattesrobot.php -i "1105310151557397"
 * php lattesrobot.php -i "1105310151557397, 0007116541401886, 0010059982369237"
 * php lattesrobot.php -f "list_of_ids.txt"
 * php lattesrobot.php -s ";" -f "1105310151557397; 0007116541401886; 0010059982369237"
 * php lattesrobot.php -o "output_folder_with_csv/" -f "1105310151557397; 0007116541401886; 0010059982369237"
 * php lattesrobot.php -ve -i "1105310151557397, 0007116541401886, 0010059982369237"
 * php lattesrobot.php -i "1111222333, 4444455555666" -f "arquivo.txt" -s ";" -o "output_folder_with_csv/" -ve
 *
 *
 * Parâmetros suportados:
 *
 *  -i [Array de ID's] - ID's à ser realizado os downloads.
 *  -f [arquivo] - download utilizando um arquivo de ID's.
 *  -s [separador] - altera o separador de ID's dentro do arquivo (-f) ou da lista (-i).
 *  -o [pasta] - altera a pasta de destino dos downloads realizados
 *  -ve - habilita a utilização de "verbose mode" que mostrará detalhadamente os curriculos baixados.
 *
 */

/*


$file = $argv[1];

// Verificamos se a extensão do arquivo é txt.
if ( strtolower(substr($file, -4)) !== ".txt" ) {
   echo "O script aceita somente arquivos TXT.\n";
   exit(1);
}

// Pegamos o conteúdo do arquivo.
$contents = file_get_contents($file);

// Separamos as palavras em um array.
$words = preg_split("/[\s+,\.!\?\(\):]/", $contents, null, PREG_SPLIT_NO_EMPTY);

// Ordenamos alfabeticamente.
natcasesort($words);

foreach ($words as $position => $word) {
   // Imprimimos cada palavra e sua respectiva posição dentro do texto.
   echo "{$word} ({$position})\n";
}
*/

define("OS", (defined(PATH_SEPARATOR) && PATH_SEPARATOR == ":" ? 'L' : 'W') ); // PHP_SHLIB_SUFFIX  so : dll



// begin cli
if (isset($argc)) {

   // We expect 1 parameter (remember that the script name also counts).
   if ($argc < 2) {
      echo "Nenhum Parâmetro fornecido\n";
      // Passamos um código de erro (qualquer número inteiro entre 1 e 254) para indicar que o programa encerrou com erro.
      exit(10);
   }

   // Getting the FLAGS
   $_FLAGS = array();
   for ($i = 1; $i < $argc; $i++){
      // Checking if the argument is a flag (prefix '-')
      if ( substr( $argv[$i], 0, 1 ) === '-' ) {
         // Adds in the array of flag and assigns the next value of the array of argument like value of flag
         $_FLAGS[$argv[$i]] = isset($argv[$i+1]) ? $argv[$i+1] : '';
      }
   }

   // Not permitted flags -i and -f together
   /*if (isset($_FLAGS['-i']) && isset($_FLAGS['-f'])){
      echo "Os parâmetros -i e -f não podem ser usados juntos\n";
      exit(10);
   }*/

   // Read file of IDS and guard in flag -i
   if (isset($_FLAGS['-f'])){
      // Check if extension is TXT
      if ( strtolower(substr($_FLAGS['-f'], -4)) !== ".txt" ) {
         echo "O script aceita somente arquivos TXT.\n";
         exit(10);
      }
      // Get content of archive
      $_FLAGS['-i'] = file_get_contents($_FLAGS['-f']);
   }

   // Converting flag value -i into array
   if (isset($_FLAGS['-i'])){
      $_FLAGS['-i'] = explode((isset($_FLAGS['-s']) ? $_FLAGS['-s'] : ','), $_FLAGS['-i']);
      $_FLAGS['-i'] = array_map("trim", $_FLAGS['-i']);
   }


   print_r($_FLAGS);

   // system('cls');
   // system('clear');

   // echo "\033[1;32m----------------------------------------------------------------\033[0m\n";
   // sleep(4);
   // echo "\033[0;32m----------------------------------------------------------------\033[0m\n";
   // echo "\033[1;31m----------------------------------------------------------------\033[0m\n";
   // echo "\033[0;31m----------------------------------------------------------------\033[0m\n";

   function lattesrobot($ilid){
      $base_url     = "http://buscacv.cnpq.br/buscacv/rest/espelhocurriculo/";
      $download_url = "http://buscacv.cnpq.br/buscacv/rest/download/curriculo/";

      // $response = file_get_contents("http://lattes.cnpq.br/{$ilid}");
      // preg_match('/([A-Z0-9]{10})/', $response, $matches, PREG_OFFSET_CAPTURE);
      // if (!isset($matches[0][0])){
      //    echo "Erro na extração do ID de download para o ID: {$ilid} \n";
      // }

      echo "Realizando extracao do ID de download do curriculo: ".$ilid.", aguarde ...\n";
      if($response =@file_get_contents($base_url.$ilid)){
         // decode received json
         $json = json_decode($response, JSON_PRETTY_PRINT);

         // get download key value
         $rh_cod = $json["cod_rh_cript_s"];

         // check if download key matches the requirement
         if(!preg_match('/([A-Z0-9]{10})/', $rh_cod)){
            echo "Erro na extracao do ID de download para o ID: {$ilid} \n";
            exit;
         }

         echo "Codigo extraido com sucesso: ".$rh_cod."\n";

         // execute request to get download binary data from server
         $bin_downlaod = file_get_contents($download_url.$rh_cod);
         $fname = $ilid.".zip";

         // writes download binary to file .zip
         if(file_put_contents($fname, $bin_downlaod))
            echo "Downlaod realizado com sucesso para {$fname}\n";

      }else{
         echo "Problema na requisicao para extrair ID de download do curriculo {$ilid}\n";
      }
      exit;
   }

   lattesrobot("0007116541401886");

   echo "\n";

   echo $_args['flags']['0'];

   echo "\n----------------------------------------------------------------\n";
}
// end cli



// ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
// lattesrobot();


//$response2 = file_get_contents("http://buscacv.cnpq.br/buscacv/rest/download/curriculo/".$matches[0][0]);

// file_put_contents("teste.xml", fopen("http://buscacv.cnpq.br/buscacv/rest/download/curriculo/".$matches[0][0], 'r'));
// $response = json_decode($response);

// print_r($response2);

// http://buscatextual.cnpq.br/buscatextual/download.do?metodo=apresentar&idcnpq=9734589783094732