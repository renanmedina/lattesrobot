# Lattes Robot
Robô para automatizar/simplificar o download de curriculos XML do site Lattes

# Desenvolvimento
O robô foi desenvolvido inicialmente como um plugin (extensão) do Google Chrome pelo seu facil uso e simplicidade.
porem, pela utilidade encontrada no projeto, estamos desenvolvendo a mesma solução em diversas abordagens tecnológicas.

# Instalação & Utilização

### Versão PHP (CLI) - Por: [Lucas Moraes (@lucaspanik)](https://github.com/lucaspanik)

```
git clone https://github.com/renanmedina/lattesrobot.git -b lattesrobot_php
node node_modules/lattesrobot/build/run.js -h

ou

npm install -g lattesrobot
lattesrobot -h
```
Exemplos:

Executando download de apenas um ID
```
lattesrobot.php -i "1105310151557397"
```

Executando download de vários ID's (Por padrão o separador é o caracter ",". este pode ser alterado informando o parametro -s [separador])
```
lattesrobot.php -i "1105310151557397, 0007116541401886, 0010059982369237, 0010606333823322, 0113358938962352, 0116664891372402, 0132525563124805"
```

Executando download de ID's em um arquivo (Por padrão o separador é o caracter ",". este pode ser alterado informando o parametro -s [separador])
```
lattesrobot.php -f [arquivo]
```

Executando download de ID's em um arquivo e alterando a pasta de saída
```
lattesrobot.php -f [arquivo] -o [caminho_pasta_de_saida]
```

Executando download de ID's em um arquivo e mostrando download a download (verbose mode)
```
lattesrobot.php -f [arquivo] -ve
```

Parâmetros suportados
* -i [lista de ID] - ID's à ser realizado os downloads.
* -f [arquivo] - download utilizando um arquivo de ID's.
* -s [separador] - altera o separador de ID's dentro do arquivo (-f) ou da lista (-i).
* -o [pasta] - altera a pasta de destino dos downloads realizados
* -ve - habilita a utilização de "verbose mode" que mostrará detalhadamente os curriculos baixados.


# Descobrindo ID de Curriculo Lattes