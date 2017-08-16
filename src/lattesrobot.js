 /*-------------------------------------------------------------------------------------- */
// Lates Robot
// @author: Renan Medina
// @date: 02/03/2017 00:20:15
// @do: this robot downloads curriculum XML/JSON files from lattes website
/* ---------------------------------------------------------------------------------------*/
// import nodeJS packages
const fs = require("fs");
const SimpleAjax = require('./simpleajax');
const colors = require('colors');
const ProgressBar = require('progress');
const util = require('util');
const path = require('path');
/* -------------------------------------------------------------------------- */
// Lattes Robot Class
// @author: Renan Medina
// @date: 02/03/2017 00:20:15
// @do: execute robot logic to extract information and inject download code
// @uses: (Object) chrome.tabs
//        (Object) RobotLogger
//        (Object) RobotConfig
/* --------------------------------------------------------------------------- */
var LattesRobot = {
  // base Url's
  base_url:"http://buscacv.cnpq.br/buscacv/rest/espelhocurriculo/",
  download_url: "http://buscacv.cnpq.br/buscacv/rest/download/curriculo/",
  CONSOLE_ERROR:1,
  CONSOLE_WARNING:2,
  CONSOLE_SUCCESS:3,
  // default config
  config:{
    ids:[],
    separator_type:"C", // caracter (C) / Lines Breaks (L)
    file_types:["xml"], // file types to download (XML OR JSON OR BOTH)
    separator:",", // Separator of id's when passed multiple ID's using -i 
    output_path:process.cwd()+"/output/",  // output path
    filename:null, // filename when passed -f parameter on CLI
    start_time: 0 // robot execution start time
  },
  // regex's 
  id_regex_check: /([0-9]{16})/,
  download_id_regex_check:/([A-Z0-9]{10})/,
  ids:[], // ids to process list
  downloadeds: [], // downloaded files list,
  errors:[], // errors container
  current_curriculum_json:null,
  progress_bar:null, // progress bar instance 
  verbose_mode:false, // verbose mode flag
  fast_mode:false, // fast mode flag
  getLattesUrl: function(lid){
    // build url based on array pieces
	  return [this.base_url, lid].join("");
  },
  process:function(lid) {
    // check if lid is blank, then, Go to Next!
    if(lid == ""){
      this.downloadeds.push(lid);
      this.processNext();
    }
    // build lattes Url to get correct Download Code (code based on an 10 caracters string [Ex: K4359686D3])
    var url_o = this.getLattesUrl(lid);
    // executes asyn call of http.get();
    SimpleAjax.get(url_o).then((curriculo) => {
     if(curriculo.cod_rh_cript_s !== undefined && curriculo.cod_rh_cript_s.match(LattesRobot.download_id_regex_check)){
        // set current json in case user checked to download it
        LattesRobot.current_curriculum_json = curriculo;
        //LattesRobot.printMessage(-1, "Download ID: "+curriculo.cod_rh_cript_s);
        LattesRobot.startDownload(lid, curriculo.cod_rh_cript_s, LattesRobot.config.file_types);
     }
     else{
      // displays error message
      LattesRobot.printMessage(LattesRobot.CONSOLE_WARNING, util.format("erro na extração do ID de download para o ID: %s", lid));
      // add error to erros container
      LattesRobot.errors.push({lattesId: lid, error:"curriculo.cod_rh_cript_s not found"});
      // process next
      LattesRobot.processNext();
     }
    }).catch(function(er) {
     LattesRobot.printMessage(LattesRobot.CONSOLE_ERROR, er);
      // process next
      LattesRobot.processNext();
    });
  },
  startDownload:function (lattesid, did){
    var download_config = {url:null, output: this.config.output_path+"\\", filename:null, lattesid: lattesid};
    var t_count = 0;
    this.config.file_types.forEach(function(ftype){
      // start download with selected types (XML, JSON)
      if(ftype == "xml"){
        // set up download parameters 
        download_config.url = [LattesRobot.download_url, did].join("");
        download_config.filename_zip = lattesid.trim()+".zip";
        download_config.filename_xml = lattesid.trim()+".xml";
        // increate file types downloaded
        t_count++;
        //LattesRobot.printMessage(-1, "Baixando curriculo "+lattesid+" .... ");
        // download file simpleajax class 
        SimpleAjax.download(download_config).then(function(filename){
          // register download if file_types_count ===this.config.file_types.length
          LattesRobot.registerDownloaded(t_count, lattesid, did); //files_count, lattesID(char {16}), lattes download ID(char{10})
        }).catch(function(err){
          // display error and move to next process
          //LattesRobot.printMessage(LattesRobot.CONSOLE_ERROR, err);
          LattesRobot.errors.push({lattesid: lattesid, error:err});
          LattesRobot.registerDownloaded(t_count, did);
        });
      }
      else if(ftype == "json"){} 
    });
  },
  registerDownloaded:function (c, lattesid, lattes_did) {
    // check for counting file types downloaded
    if(c === this.config.file_types.length){
      // add ID to downloaded ones
      this.downloadeds.push(lattesid);
      // check if verbose mode isn't enabled
      if(!this.config.verbose_mode)
        if(this.downloadeds.length < this.ids.length)
          this.progress_bar.tick();
      else if(this.config.verbose_mode)
        // if verbose mode isn't enable, display an "detailed" info from download
        this.printMessage(LattesRobot.CONSOLE_SUCCESS, util.format("Curriculo %s baixado com sucesso. [%d de %d]", lattesid, this.downloadeds.length - 1, this.ids.length));
      // process next ID 
      this.processNext();
    }
  },
  processNext:function(){
    // check if downloadeds is less than total ids to process
    if(this.downloadeds.length < this.ids.length)
      // start new download process
      this.process(this.ids[this.downloadeds.length].trim());
    else{
      this.displayAnalytics();
      process.exit();
    }
  },
  displayAnalytics:function(){
    console.log("\nResultado final: \n");
    const diff_time = parseInt(process.hrtime(this.config.start_time));
    console.log(`Curriculos baixados com sucesso: ${this.ids.length}`.green);
    console.log(`Tempo total decorrido: ${diff_time} segundos(s)`.green);
    console.log('');
    console.log("\nLista de currículos baixados com sucesso: ".green);
    console.log(this.downloadeds.join(","));
    if(this.errors.length){
      console.log("");
      console.log(`Erros encontrados (${this.errors.length}): `.red);
      this.errors.forEach((err) => {
        console.log(`${err.lattesid}: ${err.error}`.red);
      });
    }
  },
  extractIDS:function(confs){
    this.errors.ids = [];
    if(confs.separator_type == "C")
     var lids = (confs.ids[confs.ids.length-1] == confs.separator ? confs.ids.substr(0,confs.ids.length-1).split(confs.separator) : confs.ids.split(confs.separator));
    else
     var lids = confs.ids.replace(/(\\r\\n|\\n\\r)/g, "\n").split("\n");

     // check regex matchs
     for(var i in lids)
      if(!lids[i].match(this.id_regex_check))
       this.errors.push({lattesId: lids[i], error: util.format("Doesn't match: %s", this.id_regex_check)});
    
    console.log(`[ROBOT] curriculos encontrados: ${lids.length}`.cyan);
    console.log(`[ROBOT] pasta de saída dos curriculos: ${confs.output_path == "." ? __dirname : confs.output_path}`.cyan);
    // check if robot isn't using verbose mode, which means, we need to create an percentage bar of downloads
    if(!this.config.verbose_mode){
      // initialize percentage bar using module ('progress');
      this.progress_bar = new ProgressBar("Baixando curriculo :current de :total [:bar] :percent :eta", {
          total: lids.length,
          curr: 0,
          complete:"=",
          width: 20,
          incomplete:' '
      });
      // start counting progressbar 0 => 1
      this.progress_bar.tick();
      console.log("");
    }

    return lids;
  },
  readIdsFromFile:function(onReadSuccess){
    var conf = this.config;
    // read file from disk
    fs.readFile(conf.filename, function(err, fdata){
      // get file content and store id as and String on LattesRobot.conf.ids
      conf.ids = fdata.toString();
      // extract ID's using config setted, returning an array of ID's striped
      LattesRobot.ids = LattesRobot.extractIDS(conf);
      // execute on read file success method
      onReadSuccess();
    });
  },
  startRobot:function(lids){
   // gets the start time for robot
   this.config.start_time = process.hrtime();
   // check if CLI is using file of ID's or informed ID's by user
   if(this.config.filename)
     return this.readIdsFromFile(function(){
       // start processing the first lattes ID
       LattesRobot.processNext();
     });
   // set up ID's informed by user to be extracted as an array 
   else if(lids !== undefined){
    this.config.ids = lids;
    this.ids = this.extractIDS(this.config);
   }
   // start processing the first lattes ID
   this.processNext();
  },
  printMessage:function(type, msg){
    // print console messages using correct color for each sentence
    switch(type){
      case this.CONSOLE_ERROR:
        var sys_txt = " [ERROR]   ".red;
        break;
      case this.CONSOLE_WARNING:
        var sys_txt = " [WARNING] ".yellow;
        break;
      case this.CONSOLE_SUCCESS:
        var sys_txt = " [SUCCESS] ".green;
        break;
      default:
        var sys_txt = " [INFO]    ".blue;
        break;
    }
    console.log(util.format("%s\t%s", sys_txt , msg));
  }
};

module.exports = LattesRobot;
