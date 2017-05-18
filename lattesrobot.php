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

// begin cli
if (isset($argc)) {

   // Esperamos 1 parâmetro (lembrando que o nome do script também conta).
   if ($argc < 2) {
      echo "Nenhum Parâmetro fornecido\n";
      // Passamos um código de erro (qualquer número inteiro entre 1 e 254) para indicar que o programa encerrou com erro.
      exit(10);
   }

   // OBTENDO AS FLAGS
   $_FLAGS = array();
   for ($i = 1; $i < $argc; $i++){
      // Verificando se o argumento é uma flag (prefix '-')
      if ( substr( $argv[$i], 0, 1 ) === '-' ) {
         // Add no array de flag e atribui o próximo valor do array de argumento como valor da flag
         $_FLAGS[$argv[$i]] = isset($argv[$i+1]) ? $argv[$i+1] : '';
      }
   }

   // Não permitindo o uso das flags -i e -f juntos
   /*if (isset($_FLAGS['-i']) && isset($_FLAGS['-f'])){
      echo "Os parâmetros -i e -f não podem ser usados juntos\n";
      exit(10);
   }*/

   // Se a flag -f existir, pegamos o conteúdo do arquivo e salvamos em -i
   if (isset($_FLAGS['-f'])){
      // Verificamos se a extensão do arquivo é txt.
      if ( strtolower(substr($_FLAGS['-f'], -4)) !== ".txt" ) {
         echo "O script aceita somente arquivos TXT.\n";
         exit(10);
      }
      // Pegamos o conteúdo do arquivo.
      $_FLAGS['-i'] = file_get_contents($_FLAGS['-f']);
   }

   // Se flasg -i existir, transforma o valor em um array
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
    $codcurriculum = "0007116541401886";
    echo "Realizando extracao do ID de download do curriculo: ".$codcurriculum.", aguarde ...\n";
    if($response =@file_get_contents('http://buscacv.cnpq.br/buscacv/rest/espelhocurriculo/'.$codcurriculum)){
        // decode received json
        $json = json_decode($response, JSON_PRETTY_PRINT);
        // get download key value
        $rh_cod = $json["cod_rh_cript_s"];
        // check if download key matches the requirement
        if(!preg_match('/([A-Z0-9]{10})/', $rh_cod)){
            echo "Erro na extracao do ID de download para o ID: {$lid} \n";
            exit;
        }
        
        echo "Codigo extraido com sucesso: ".$rh_cod."\n";
        // execute request to get download binary data from server
        $bin_downlaod = file_get_contents("http://buscacv.cnpq.br/buscacv/rest/download/curriculo/".$rh_cod);
        $fname = $codcurriculum.".zip";
        // writes download binary to file .zip
        if(file_put_contents($fname, $bin_downlaod))
            echo "Downlaod realizado com sucesso para ".$fname."\n";
    }
    else
       echo "Problema na requisicao para extrair ID de download do curriculo ".$codcurriculum;

    exit;

   function lattesrobot(){
      $base_url     = "http://buscacv.cnpq.br/buscacv/rest/espelhocurriculo/";
      $download_url = "http://buscacv.cnpq.br/buscacv/rest/download/curriculo/";

      $curl = curl_init($base_url);
      curl_setopt($curl, CURLOPT_RETURNTRANSFER, true);
      $curl_response = curl_exec($curl);
      if ($curl_response === false) {
          $info = curl_getinfo($curl);
          curl_close($curl);
          die('error occured during curl exec. Additioanl info: ' . var_export($info));
      }
      curl_close($curl);
      $decoded = json_decode($curl_response);
      if (isset($decoded->response->status) && $decoded->response->status == 'ERROR') {
          die('error occured: ' . $decoded->response->errormessage);
      }
      echo 'response ok!';
      var_export($decoded->response);
      var_dump($decoded->response);
   }


   function arguments ( $args )
   {
      array_shift( $args );
      $endofoptions = false;

      $ret = array
         (
         'commands' => array(),
         'options' => array(),
         'flags'    => array(),
         'arguments' => array(),
         );

      while ( $arg = array_shift($args) )
      {

         // if we have reached end of options,
         //we cast all remaining argvs as arguments
         if ($endofoptions)
         {
            $ret['arguments'][] = $arg;
            continue;
         }

         // Is it a command? (prefixed with --)
         if ( substr( $arg, 0, 2 ) === '--' )
         {

            // is it the end of options flag?
            if (!isset ($arg[3]))
            {
               $endofoptions = true;; // end of options;
               continue;
            }

            $value = "";
            $com   = substr( $arg, 2 );

            // is it the syntax '--option=argument'?
            if (strpos($com,'='))
               list($com,$value) = split("=",$com,2);

            // is the option not followed by another option but by arguments
            elseif (strpos($args[0],'-') !== 0)
            {
               while (strpos($args[0],'-') !== 0)
                  $value .= array_shift($args).' ';
               $value = rtrim($value,' ');
            }

            $ret['options'][$com] = !empty($value) ? $value : true;
            continue;

         }

         // Is it a flag or a serial of flags? (prefixed with -)
         if ( substr( $arg, 0, 1 ) === '-' )
         {
            for ($i = 1; isset($arg[$i]) ; $i++)
               $ret['flags'][$arg[$i]] = $arg[++$i];
            continue;
         }

         // finally, it is not option, nor flag, nor argument
         $ret['commands'][] = $arg;
         continue;
      }

      if (!count($ret['options']) && !count($ret['flags']))
      {
         $ret['arguments'] = array_merge($ret['commands'], $ret['arguments']);
         $ret['commands'] = array();
      }

      return $ret;
   }

   // $_args = arguments($argv);
   // print_r($_args);

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

print_r($response2);

// http://buscatextual.cnpq.br/buscatextual/download.do?metodo=apresentar&idcnpq=9734589783094732