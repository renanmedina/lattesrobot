<?php
/**
 * php lattesrobot.php -i "1105310151557397"
 * php lattesrobot.php -i "1105310151557397, 0007116541401886, 0010059982369237"
 * php lattesrobot.php -f "list_of_ids.txt"
 * php lattesrobot.php -s ";" -f "1105310151557397; 0007116541401886; 0010059982369237"
 * php lattesrobot.php -o "output_folder_with_csv/" -f "1105310151557397; 0007116541401886; 0010059982369237"
 * php lattesrobot.php -ve -i "1105310151557397, 0007116541401886, 0010059982369237"
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
 * Esperamos 1 parâmetro (lembrando que o nome do script
 * também conta) com o nome do TXT.
 */
if ($argc < 2) {
   echo "O nome do arquivo TXT deve ser passado para o script.\n";

   /*
    * Passamos um código de erro (qualquer número inteiro
    * entre 1 e 254) para indicar que o programa encerrou com erro.
    */
   exit(1);
}
$file = $argv[1];

/*
 * Verificamos se a extensão do arquivo é txt.
 */
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

   /*
    * Imprimimos cada palavra e sua respectiva posição
    * dentro do texto.
    */
   echo "{$word} ({$position})\n";
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
            $ret['flags'][] = $arg[$i];
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

// print_r(arguments($argv));