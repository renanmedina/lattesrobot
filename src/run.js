#!/usr/bin/env node

// using strict javascript
// import used packages
var robot = require('./lattesrobot');
var argparser = require('argparse').ArgumentParser;
var colors = require('colors');
var argcliparser = new argparser({
  version: '1.0.0',
  addHelp:true,
  description:'LattesRobot - Help'
});
// create available arguments
argcliparser.addArgument(['-f', '--f'],{
  help:'filename with ID\'s',
});
argcliparser.addArgument(['-s', '--s'],{
  help:'ID\'s separator caracter [default: ","]'
});
argcliparser.addArgument(['-i', '--i'],{
  help:'ID\'s to download separated by "separator" [default: ","]'
});
argcliparser.addArgument(['-fa', '--fa'],{
  help:'Fast mode download, using multiple threads to download files'
});
argcliparser.addArgument(['-o', '--o'],{
  help:'Output path of all downloaded files from app'
});
argcliparser.addArgument(['-ve', '--ve'],{
  action:'storeTrue', 
  dest:"ve",
  help:'Verbose mode, display detailed info about each download made'
});


// get passed args by user
var cliargs = argcliparser.parseArgs();
process.stdout.write('\033c');
// process.on('exit', (code) => {
//   console.log(code);
//   console.log("Cancelado pelo usuário ..".red);
// });
console.log("---------------------------------------------------------------------------");
console.log("                          LattesRobot - NodeJS                             ");
console.log("---------------------------------------------------------------------------");
console.log(" Inicializado, aguarde ...".green);
console.log("");

if(!cliargs.f && !cliargs.i){
  robot.printMessage(robot.CONSOLE_ERROR, "Ops, você não informou nenhum ID lattes para realizar o download");
  console.log("\n Finalizando ...".red);
  return;
}

if(cliargs.f)
  robot.config.filename = cliargs.f;
else if(cliargs.i)
  var ids = cliargs.i;

if(cliargs.s){
 if(cliargs.s == "\n")
  robot.config.separator_type = "L";
 else
  robot.config.separator = cliargs.s;
}

if(cliargs.o)
  robot.config.output_path = cliargs.o;

robot.config.verbose_mode = cliargs.ve;
robot.startRobot((ids !== undefined ? ids : undefined));
