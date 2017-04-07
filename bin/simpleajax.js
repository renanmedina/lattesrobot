/* -------------------------------------------------------------------------- */
// SimpleAjax 
// @author: Renan Medina
// @date: 02/03/2017 00:20:15
// @do: executes an basic XmlHttpRequest with a promise as an return
/* --------------------------------------------------------------------------- */
var http = require('http');
var fs = require("fs");

var SimpleAjax = {
  // handle a simple get call
  get: function(url){
    // returns a promise for method call
    return new Promise(function(rsv, rjt){
      // executes http request
     var req = http.get(url, function(res){
        // data received 
        var data_received = '';
        // build data piece by piece
        res.on('data', (piece) => data_received += piece);
        // on response finished
        res.on('end', () => {
          try {
            // get json received and convert it to string JSON
            var j_result = JSON.stringify(data_received);
            res.resume();
            // do a double parse because of bugs
            rsv(JSON.parse(JSON.parse(j_result)));
          } catch (e) {
           rjt(e);
          }
        });
      }).on('error', (e) => {
        rjt(e);
      });
    });
  },
  // executes file download using Promise
  download:function(dconfig){
    // return promise for method call
    return new Promise((rsv, rjt) => {
      // executes get for url sent by config
      var req = http.get(dconfig.url, (res) => {
        // file binary container
        var d_received;
        // lambda execution for pieces 
        res.on("data", (piece) => d_received += piece);
        // when download ends, executes lamda to resolve promise or reject it
        res.on("end", () => {
          // write file on filesystem output path
          fs.writeFile(dconfig.output+dconfig.filename, d_received, (err) => {
            // resolve promise or reject it
            if(!err)
              rsv(dconfig.filename);
            else
              rjt(err.toString());
          });
        });
      })
    });
  }
};
// export code for NodeJS pattern
module.exports = SimpleAjax;