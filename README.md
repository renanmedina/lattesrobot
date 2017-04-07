# Lattes Robot
Robô para automatizar/simplificar o download de curriculos XML/JSON do site Lattes


# Instalação & Utilização
* Versão Nodejs (CLI)

```
npm install -g lattesrobot
```

# Exemplos
Executando download de apenas um ID
```
lattesrobot -i 1105310151557397
```

Executando download de vários ID's (Por padrão o separador é o caracter ",". este pode ser alterado informando o parametro -s [separador])
```
lattesrobot -i 1105310151557397, 0007116541401886, 0010059982369237, 0010606333823322, 0113358938962352, 0116664891372402, 0132525563124805
```

Executando download de ID's em um arquivo (Por padrão o separador é o caracter ",". este pode ser alterado informando o parametro -s [separador])
```
lattesrobot -f [arquivo]
```

Executando download de ID's em um arquivo e alterando a pasta de saída
```
lattesrobot -f [arquivo] -o [caminho_pasta_de_saida]
```

Executando download de ID's em um arquivo e mostrando download a download (verbose mode)
```
lattesrobot -f [arquivo] -ve
```

# Parâmetros suportados
* -i [lista de ID] - ID's à ser realizado os downloads.
* -f [arquivo] - download utilizando um arquivo de ID's.
~~ * -fa - habilita a utilização de "fast mode" que executa os downloads em modo assincrono. (desenvolvimento) ~~
* -s [separador] - altera o separador de ID's dentro do arquivo (-f) ou da lista (-i).
* -o [pasta] - altera a pasta de destino dos downloads realizados
* -ve - habilita a utilização de "verbose mode" que mostrará detalhadamente os curriculos baixados.


