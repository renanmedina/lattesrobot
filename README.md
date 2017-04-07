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

Após a instalação do plugin, um ícone de plugin deverá aparecer na barra de navegação do google chrome. click neste ícone e uma interface será exibida. Será solicitado os ID's de curriculos lattes à serem baixados, siga os passos solicitados na interface e para executar o robô. 

* Versão Nodejs (CLI) (Autor: Renan Medina (@renanmedina))

```
git clone https://github.com/renanmedina/lattesrobot.git -b lattesrobot_nodejs
node node_modules/lattesrobot/build/run.js -h

ou

npm install lattesrobot
lattesrobot -h
```
para aprender como utilizá-o.

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

# Descobrindo ID de Curriculo Lattes

