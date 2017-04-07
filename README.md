# Lattes Robot
Robô para automatizar/simplificar o download de curriculos XML do site Lattes

# Desenvolvimento
O robô foi desenvolvido inicialmente como um plugin (extensão) do Google Chrome pelo seu facil uso e simplicidade.
porem, pela utilidade encontrada no projeto, estamos desenvolvendo a mesma solução em diversas abordagens tecnológicas.

# Instalação & Utilização
* Versão Google Chrome (``Autor: Renan Medina (@renanmedina)``)

execute o comando abaixo para baixar o plugin:

``
git clone https://github.com/renanmedina/lattesrobot.git -b lattesrobot_chrome
``

O código fonte do projeto será baixado para a sua máquina. Utilizando o google chrome, instale o arquivo encontrado na pasta:

``
build/lattesrobot.crx
``

(fique a vontade para visualizar e melhorar o código fonte do projeto, mande um pull request e avaliaremos a solução/melhoria/otimização para realizar um merge)

Após a instalação do plugin, um ícone de plugin deverá aparecer na barra de navegação do google chrome. click neste ícone e uma interface será exibida. Será solicitado os ID's de curriculos lattes à serem baixados, siga os passos solicitados na interface para executar o robô. 

* Versão Nodejs (CLI) (Autor: Renan Medina (@renanmedina))

```
git clone https://github.com/renanmedina/lattesrobot.git -b lattesrobot_nodejs
node node_modules/lattesrobot/build/run.js -h

ou

npm install -g lattesrobot
lattesrobot -h
```
Exemplos:
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

Parâmetros suportados
* -i [lista de ID] - ID's à ser realizado os downloads.
* -f [arquivo] - download utilizando um arquivo de ID's.
~~ * -fa - habilita a utilização de "fast mode" que executa os downloads em modo assincrono. (desenvolvimento) ~~
* -s [separador] - altera o separador de ID's dentro do arquivo (-f) ou da lista (-i).
* -o [pasta] - altera a pasta de destino dos downloads realizados
* -ve - habilita a utilização de "verbose mode" que mostrará detalhadamente os curriculos baixados.


* Versão Python (CLI) (``Autor: Jairo (@akaarosh)``)

```
git clone https://github.com/renanmedina/lattesrobot.git -b lattesrobot_python
```
O código fonte do projeto será baixado para a sua máquina. Utilizando um interpretador python 3, execute o comando:
```
./lattes_robot.py -h

ou

python lattes_robot.py -h
```
para aprender como utilizá-o.

* Versão PHP (CLI)
```
Em desenvolvimento  
Autor: Lucas Moraes (@lucas.panik) 
```

* Versão C# (CLI)
```
Em desenvolvimento  
Autor: Renan Medina (@renanmedina) 
```

* Versão Java (CLI)
```
Em desenvolvimento  
Autor: Renan Medina (@renanmedina) 
```

* Versão Ruby (CLI)
```
Em desenvolvimento  
Autor: Renan Medina (@renanmedina) 
```

* Versão Perl (CLI)
```
Em desenvolvimento  
Autor: Jairo (@akaarosh)
```

# Descobrindo ID de Curriculo Lattes

