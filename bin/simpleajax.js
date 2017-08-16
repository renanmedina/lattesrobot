/* -------------------------------------------------------------------------- */
// SimpleAjax 
// @author: Renan Medina
// @date: 02/03/2017 00:20:15
// @do: executes an basic XmlHttpRequest with a promise as an return
/* --------------------------------------------------------------------------- */
const http = require('http');
const fs = require("fs");
const unzip = require("unzip");

const SimpleAjax = {
  // handle a simple get call
  get: function(url){
    // returns a promise for method call
    return new Promise(function(rsv, rjt){
      // executes http request
     const req = http.get(url, function(res){
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
      // builds complete output file path
      const foutpath = dconfig.output+dconfig.filename_zip;
      // create a write stream to be piped on GET REQUEST result
      const fstream = fs.createWriteStream(foutpath);
       // executes get for url sent by config
      var req = http.get(dconfig.url, (res) => {
        // set result to file stream to be written
        res.pipe(fstream);
        // on file stream is finished closes it and resolve the Promise
        fstream.on("finish", () => {
          // closes the stream
          fstream.close();
          // unzip file and rename
          fs.createReadStream(foutpath).pipe(unzip.Extract({path:  dconfig.output}))
          .on('error', (err) => {
            rjt(err_unzip);
          }).on('close', () => {
            fs.unlink(foutpath, (err_unlink) => {
              // if error occurs on unlink .zip file, then rejects the promise from download method
              if(err_unlink)
                rjt(err_unlink);
              fs.rename(dconfig.output+"curriculo.xml", dconfig.output+dconfig.filename_xml, (err_rename) => {
                if(!err_rename)
                  // resolve promise from download method
                  rsv(dconfig.filename_xml);
                else
                  rjt(err_rename); // reject promisse with rename error
              });
            })
          });  
        });
      }).on('error', (err) => {
        // reject promise from download method with the error
        rjt(err);
      });
    });
  }
};
// export code for NodeJS pattern
module.exports = SimpleAjax;