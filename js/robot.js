/*-------------------------------------------------------------------------------------- */
// Lates Robot
// @author: Renan Medina
// @date: 02/03/2017 00:20:15
// @do: this robot downloads curriculum XML files from lattes website
// @toWork: In order to work, the robot needs you to first executes an curriculum search on the website
//         through the url: http://lattes.cnpq.br/ -> 
// @howTo: 1º - You need to get the website above, then check for "Nova Busca de curriculos" (first Banner) and click it
//         2º - then you need to check for the URL: "http://buscacv.cnpq.br/buscacv" that will be displayed on that page and click it
//         3º - then search for ANY PERSON and click it to open his curriculum and let this tab opened (Do not CLOSE otherwise the robot won't work)
//         4º - Now you ready for get all XML's you need, click on the robot extension icon on top of the chrome window
//         5º - Add the lattes ID's you need separated by comma (,)
//         6º - click the Start button and wait the downloads finish
//         7º - That's it.
/* ---------------------------------------------------------------------------------------*/

/* -------------------------------------------------------------------------- */
// SimpleAjax 
// @author: Renan Medina
// @date: 02/03/2017 00:20:15
// @do: executes an basic XmlHttpRequest with a promise as an return
/* --------------------------------------------------------------------------- */
var SimpleAjax = {
  get: function(url){
    return new Promise(function(rsv, rjt){
      var xmlhttp = new XMLHttpRequest();
      xmlhttp.open("GET", url);
      xmlhttp.onreadystatechange = function(){
        if(xmlhttp.readyState === 4 && xmlhttp.status === 200 && xmlhttp.responseText != ""){
          var j_result = JSON.stringify(xmlhttp.responseText);
          // parse 2 times because of some bug on parse
          rsv(JSON.parse(JSON.parse(j_result)));
        }
      };
      xmlhttp.send();
    });
  }
}

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
  downloadeds: [], // downloaded files list
  errors:{
    ids:[]
  },
  process_time:0,
  is_paused:false,
  id_regex_check: /([0-9]{16})/,
  download_id_regex_check:/([A-Z0-9]{10})/,
  ids:[], // ids to process list,
  process_time_gap: 1000, // milliseconds to wait until starts next process 
  current_curriculum_json:null,
  previous_id: null,
  notification:{
    id: "lattes_download_notify",
    config:{
      type: "progress",
      title:"Lattes Robot",
      iconUrl:"icons/48.png",
      progress:0,
      message:"",
      requireInteraction:true,
      buttons:[
        {title:'Pausar downloads'},
        {title: 'Cancelar downloads'}
      ]
    }
  },
  getLattesUrl: function(lid){
    // build url based on array pieces
	  return [this.base_url, lid].join("");
  },
  process:function(lid){
    if(lid == ""){
      this.downloadeds.push(lid);
      this.processNext();
    }
    // build lattes Url to get correct Download Code (code based on an 10 caracters string [Ex: K4359686D3])
    var url_o = this.getLattesUrl(lid);
    SimpleAjax.get(url_o).then(function(curriculo){
     if(curriculo.cod_rh_cript_s !== undefined && curriculo.cod_rh_cript_s.match(LattesRobot.download_id_regex_check)){
        // set current json in case user checked to download it
        LattesRobot.current_curriculum_json = curriculo;
        LattesRobot.startDownload(lid, curriculo.cod_rh_cript_s, LattesRobot.config.file_types);
     }
     else
      alert("Erro na extração do ID de download para o ID: "+lid);
    }).catch(function(er){
      console.log(er);
    });
  },
  startDownload:function(lattesid, did){
    var download_config = {url:null, saveAs:false, method:"GET"};
    var t_count = 0;
    this.config.file_types.forEach(function(ftype){
      // start download with selected types (XML, JSON)
      if(ftype == "xml"){
        download_config.url = [LattesRobot.download_url, did].join("");
        // download file using chrome download API.
        chrome.downloads.download(download_config, function(chrome_download_id){
          t_count++;
          LattesRobot.registerDownloaded(t_count, did);
        });
      }
      else if(ftype == "json"){
        var fjson = new Blob([JSON.stringify(LattesRobot.current_curriculum_json)], {type:"application/json"});
        download_config.url = URL.createObjectURL(fjson);
        var filename = lattesid.trim()+".json";
        // download using a[href=''] element because chrome.downloads.download method doesnt use filename setted
        // for file download and not add the correct extension. (THIS IS A WORKAROUND!)
        var a = document.createElement('a');
        a.download = filename;
        a.id = did;
        a.href = download_config.url;
        document.body.appendChild(a);
        document.getElementById(did).click();
        t_count++;
        LattesRobot.registerDownloaded(t_count, did);
      } 
    });
  },
  registerDownloaded:function(t_count, lattes_did){
    if(t_count === LattesRobot.config.file_types.length){
      this.downloadeds.push(lattes_did);
      // RobotLogger.registerLog({
      //     date: new Date(),
      //     lattes_download_id: lattes_did,
      //     operation:"Download"
      // });
      // updates chrome notification progress
      this.updateNotification({message: this.getNotificationMessage(),
                               progress:parseInt((this.downloadeds.length / this.ids.length) * 100)});
        // wait at least 1 second to process next (makes time to download complete)
      var intrvNext = window.setInterval(function(){
        window.clearInterval(intrvNext);
            // process next
        LattesRobot.processNext();
        document.getElementById(lattes_did).remove();
      }, this.process_time_gap);
    }
  },
  processNext:function(){
    // check if download is paused
    if(this.is_paused)
      return;
    // check if downloadeds is less than total ids to process
    if(this.downloadeds.length < this.ids.length)
      // start new download process
      this.process(this.ids[this.downloadeds.length]);
    else{
      // display success popup window with all downloaded files
      this.updateNotification({message: "Arquivos de curriculo baixados com sucesso\n"+LattesRobot.getTotalTimeString()+"\nTotal de "+this.downloadeds.length+" curriculos",
                                      buttons:[]});
      this.downloadeds = [];
      // stop timer count
      this.stopProcessTimer();
      return;
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
       this.errors.ids.push(lids[i]);
    
    return lids;
  },
  onNotificationClick:function(notification_id, buttonIndex){
   switch(buttonIndex){
     case 0: // pause button
       if(LattesRobot.is_paused){
        LattesRobot.is_paused = false;
        LattesRobot.notification.config.buttons[0] = {title: "Pausar downloads"};
       }
       else{
        LattesRobot.is_paused = true;
        LattesRobot.notification.config.buttons[0] = {title: "Retomar downloads"};
       }

       LattesRobot.updateNotification(LattesRobot.notification.config);
       // always call processNext, it checks if is paused or not
       LattesRobot.processNext();
       break;
     case 1: // cancel button
      LattesRobot.is_paused = true;
      LattesRobot.updateNotification({message: "Downloads cancelados pelo usuário \n("+LattesRobot.downloadeds.length+" baixados de um total de "+LattesRobot.ids.length+")\n"+LattesRobot.getTotalTimeString(),
                                      buttons:[]});
      LattesRobot.stopProcessTimer();
      break;
   }
  },
  startProcessTimer:function(){
    this.process_timer = window.setInterval(function(){
      if(!LattesRobot.is_paused){
        LattesRobot.process_time++;
        LattesRobot.updateNotification({message: LattesRobot.getNotificationMessage()});
      }
    }, 1000);
  },
  stopProcessTimer:function(){
    // remove process timer clock
    if(this.process_timer !== undefined)
      window.clearInterval(this.process_timer);
  },
  getTotalTimeString:function(){
    return "Tempo total: "+this.process_time+" segundos";
  },
  // build notification message progress
  getNotificationMessage:function(){
    return "Baixando curriculo "+this.downloadeds.length+" de "+this.ids.length+"\nTempo decorrido: "+this.process_time+" segundos";
  },
  updateNotification:function(to_update){
    for(var k in to_update)
      this.notification.config[k] = to_update[k];

    chrome.notifications.update(this.notification.id, this.notification.config); 
  }
};

/* -------------------------------------------------------------------------- */
// Robot Logger Class
// @author: Renan Medina
// @date: 03/03/2017 23:28:56
// @do: register logs of robot operations
// @uses: (Object) chrome.storage.sync
/* --------------------------------------------------------------------------- */
var RobotLogger = {
  registerLog:function(log, cb){
    this.getLogs(function(logs){
      logs.push(log);
      chrome.storage.sync.set({"lattes_robot_log": logs}, function(result){
        cb(result);
      });
    });
  },
  getLogs:function(cb){
    if(arguments[1] === undefined){
      chrome.storage.sync.get(["lattes_robot_log"], function(logs){
        if(logs.lattes_robot_log === undefined)
          cb([]);
        else
          cb(logs.lattes_robot_log);
      })
    }
    else{
    }
  }
};

/* -------------------------------------------------------------------------- */
// Robot Config Class
// @author: Renan Medina
// @date: 03/03/2017 13:34:22
// @do: save/read configs for robot operation
// @uses: (Object) chrome.storage.sync
/* --------------------------------------------------------------------------- */
var RobotConfig =  {
  default:{
    ids:[],
    separator_type:"C",
    file_types:["xml"],
    separator:",",
    limit_download:false,
    limit_download_count:50,
    limit_download_time: 15
  },
  getConfigs:function(cb){
    return chrome.storage.sync.get(["lattes_robot"], function(confs){
      if(confs.lattes_robot !== undefined)
        cb(confs.lattes_robot);
      else
        cb(RobotConfig.default);
    });
  },
  saveConfigs:function(confs, cb){
    this.save((cb !== undefined ? cb : null));
  },
  save:function(cb){
    return chrome.storage.sync.set({lattes_robot: this.current}, cb);
  },
};

/* -------------------------------------------------------------------------- */
// executeRobot 
// @author: Renan Medina
// @date: 01/03/2017 00:20:15
// @do: start robot execution when "start" button on UI is clicked
// @uses: (Object) chrome.tabs
/* --------------------------------------------------------------------------- */
window.executeRobot = function(lids, confs){
  LattesRobot.config = confs;
  LattesRobot.ids = lids;
  LattesRobot.notification.config.message = LattesRobot.getNotificationMessage();
  // creates chrome notification
  chrome.notifications.create(LattesRobot.notification.id, LattesRobot.notification.config);
  // add listeners for button click on notification
  chrome.notifications.onButtonClicked.addListener(LattesRobot.onNotificationClick);
  // start processing the first lattes ID
  LattesRobot.processNext();
  // start timer
  LattesRobot.startProcessTimer();
  //RobotConfig.getConfigs(function(confs){
    
  //});
};
