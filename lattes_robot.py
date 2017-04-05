#!/usr/bin/python3

def main(cnpq_ids, dest_dir = './cvs', sts_prg_cb=None):
    import os
    from requests import get
    
    #http://buscacv.cnpq.br/buscacv/rest/espelhocurriculo/[cnpq_id]
    url_espcv = 'http://buscacv.cnpq.br/buscacv/rest/espelhocurriculo/{}'
    #http://buscacv.cnpq.br/buscacv/rest/download/curriculo/[cod_rh_cript_s]
    url_downcv = 'http://buscacv.cnpq.br/buscacv/rest/download/curriculo/{}'

    if not os.path.exists(dest_dir):
        os.makedirs(dest_dir)

    for n, cnpq_id in enumerate(cnpq_ids):
        if cnpq_id.strip():
            resp = get(url_espcv.format(cnpq_id))
            cod_rh_cript_s = resp.json()['cod_rh_cript_s']         
            resp = get(url_downcv.format(cod_rh_cript_s), stream=True)
            filename = os.path.join(dest_dir, cnpq_id + '.zip')            
            with open(filename, 'wb') as cv:
                cv.write(resp.raw.read())            
            if sts_prg_cb:
                sts_prg_cb(n+1)            
        

if __name__ == '__main__':
    import sys    
    import progressbar    

    try:        
        input_file = sys.argv[1]
    except IndexError:
        print('AHHHHH! VC N INFORMOU O ARQUIVO COM OS CODIGOS ...')
        sys.exit(1)
    else:           
        with open(input_file, 'r') as f:
            cnpq_ids = f.read().splitlines()        
        bar = progressbar.ProgressBar(max_value=len(cnpq_ids), term_width=75)
        try:
            main(cnpq_ids, sts_prg_cb=lambda n: bar.update(n))
            print('\n{} curriculos foram baixados e gravados;'.format(len(cnpq_ids)))
        except KeyboardInterrupt:
            print('\nCANCELADO PELO USUARIO')
        except Exception as e:
            print('\n'+str(e))
    finally:
        print('\n')
        
