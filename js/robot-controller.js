// bind events
window.onload = function(){
  // btn Start event binding
	document.getElementById("btn-start").onclick = startRobot;
  // btn clear event binding
	document.getElementById("btn-clear").onclick = clearTextarea;
  // checkbox event binding 
  //document.getElementById("download-limited").onchange = toggleLimitFields;
  // radio btn event binding 
  document.getElementsByName("lattes-separator-type")[0].onchange = toggleSeparatorField;
  document.getElementsByName("lattes-separator-type")[1].onchange = toggleSeparatorField;
  // set form config values
  RobotConfig.getConfigs(function(confs){
    UISetConfigs(confs);
  });

  document.getElementById("lattes-ids").onkeyup = updateIdsCounter;
};

function UISetConfigs(cfs){
  if(cfs.separator_type == "C")
    var septype_radio = document.getElementsByName("lattes-separator-type")[0];
  else
    var septype_radio = document.getElementsByName("lattes-separator-type")[1];

  var sept_txt = document.getElementById("lattes-separator");
  var limit_chk = document.getElementById("download-limited");
 // var limit_count_txt = document.getElementById("lattes-download-count");
  //var limit_interv_txt = document.getElementById("lattes-download-interval");
  
  septype_radio.checked = true;
  sept_txt.value = cfs.separator;
  //limit_chk.checked = cfs.limit_download;
  //limit_interv_txt.value = cfs.limit_download_time;
  //limit_count_txt.value = cfs.limit_download_count;

  //toggleLimitFields(limit_chk);
  //toggleSeparatorField(septype_radio);
};

function toggleLimitFields(evt){
  var chk = evt.target || evt;
  document.getElementById("lattes-download-count").disabled = !chk.checked;
  document.getElementById("lattes-download-interval").disabled = !chk.checked;
}

function toggleSeparatorField(evt){
  var rd = evt.target || evt;
  if(rd.value == "C")
    document.getElementById("lattes-separator").disabled = false;
  else
    document.getElementById("lattes-separator").disabled = true;
}

function updateIdsCounter(){
  var counter_el = document.getElementById("ids-count");
  var errors_counter = document.getElementById("errors-count");
  var configs = buildConfigs();
  var ids = LattesRobot.extractIDS(configs);
  document.getElementById("lattes-ids").value = ids;
  counter_el.innerHTML = "";
  errors_counter.innerHTML = "";

  if(ids != "")
    counter_el.innerHTML = ids.length+" curriculos a serem baixados";

  if(LattesRobot.errors.ids.length)
    errors_counter.innerHTML = LattesRobot.errors.ids.length + " erro(s) encontrado(s) <a href='#'>[visualizar]</a>";
}

function startRobot(){
  // build configs from form
  var current_conf = buildConfigs();
  if(current_conf.ids != ""){
    RobotConfig.saveConfigs(current_conf, function(){
      // get robot script backgroung
      var robotScript = chrome.extension.getBackgroundPage();
      robotScript.executeRobot(LattesRobot.extractIDS(current_conf), current_conf);
    });
  }
  else{
    alert("Informe pelo menos 1 ID de curriculo Lattes !");
    return;
  }
};

function clearTextarea(){
  document.getElementById("lattes-ids").value = '';
}

function buildConfigs(){
  var chk_types = document.getElementsByName("lattes-filetypes");
  var ftypes = [];
  for(var c in chk_types)
    if(chk_types[c].checked)
      ftypes.push(chk_types[c].value);
  
  return {
      // lattes ids written by user
      ids: document.getElementById("lattes-ids").value,
      separator_type:(document.getElementsByName("lattes-separator-type")[0].checked ? "C" : "L"),
      file_types: ftypes,
      separator: document.getElementById("lattes-separator").value,
      //limit_download: document.getElementById("download-limited").checked,
      //limit_download_count: document.getElementById("lattes-download-count").value,
      //limit_download_time: document.getElementById("lattes-download-interval").value
  };
}