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
  base_url: "http://lattes.cnpq.br/",
  download_url: "http://buscacv.cnpq.br/buscacv/rest/download/curriculo/",
  downloadeds: [], // downloaded files list
  process_time:0,
  is_paused:false,
  ids:[], // ids to process list,
  process_time_gap: 1000, // milliseconds to wait until starts next process 
  lattes_tab:null,
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
  startDownload:function(did){
    // execute script on lattes tab
    var durl =  [this.download_url, did].join("");
    chrome.tabs.create({url: durl}, function(tab){
      var dtid = tab.id;
      // on download link gets loaded, it's automatically closes
      // so we need to watch for tabs that closes and check if it was the download tab opened
      chrome.tabs.onRemoved.addListener(function(tid, changeInfo){
        if(tid == dtid){
          // set download as processed
          LattesRobot.downloadeds.push(did);
          RobotLogger.registerLog({
            date: new Date(),
            lattes_download_id: did,
            operation:"Download"
          });
          // updates chrome notification progress
          LattesRobot.updateNotification({message: LattesRobot.getNotificationMessage(),
                                          progress:parseInt((LattesRobot.downloadeds.length / LattesRobot.ids.length) * 100)});
          // wait at least 1 second to process next (makes time to download complete)
           var intrvNext = window.setInterval(function(){
             window.clearInterval(intrvNext);
             // process next
             LattesRobot.processNext();
           }, LattesRobot.process_time_gap);
        }
       });
      
      
      
    });
  },
  process:function(lid){
    if(lid == ""){
      this.downloadeds.push(lid);
      this.processNext();
    }
    // build lattes Url to get correct Download Code (code based on an 10 caracters string [Ex: K4359686D3])
    var url_o = this.getLattesUrl(lid);
    // instantiate chrome new tab with lattes curriculum page 
    chrome.tabs.create({url: url_o}, function(tab){
      // get tabId from created tab
      var tid = tab.id;
      // listen for redirect when page is loaded
      chrome.tabs.onUpdated.addListener(function(tabId, info, tab){
        // when pages redirect to a new URL the URL contains  
        if(tabId == tid && info.url !== undefined){
          chrome.tabs.remove(tid);
          // extract download ID from new URL when tabId matchs 
          // URL Ex: (http://buscatextual.cnpq.br/buscatextual/visualizacv.do?metodo=apresentar&id=K8620955J6)
          // regex matchs -> K8620955J6
          var download_id = info.url.match(/([A-Z0-9]{10})/)[0];
          // starts download injection code
          LattesRobot.startDownload(download_id);
          // close opened tab to get download code
        }
      });
    });
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
      LattesRobot.updateNotification({message: "Arquivos XML baixados com sucesso",
                                      buttons:[]});
      this.downloadeds = [];
      return;
    }
  },
  extractIDS:function(confs){
    if(confs.separator_type == "C")
     return lids = (confs.ids[confs.ids.length-1] == confs.separator ? confs.ids.substr(0,confs.ids.length-1).split(confs.separator) : confs.ids.split(confs.separator));
    else
     return lids = confs.ids.replace(/(\\r\\n|\\n\\r)/g, "\n").split("\n");
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
      LattesRobot.updateNotification({message: "Downloads cancelados pelo usuário \n("+LattesRobot.downloadeds.length+"baixados de um total de "+LattesRobot.ids.length+")\nTempo total: "+LattesRobot.process_time+" segundos",
                                      buttons:[]});
       // remove process timer clock
       if(this.process_timer !== undefined)
        window.clearInterval(this.process_timer);
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
  // build notification message progress
  getNotificationMessage:function(){
    return "Baixando curriculos XML \nCurriculo: ("+this.downloadeds.length+" de "+this.ids.length+")\nTempo decorrido: "+this.process_time+" segundos";
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
    this.save(cb);
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
window.executeRobot = function(lids){
  RobotConfig.getConfigs(function(confs){
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
  });
};
