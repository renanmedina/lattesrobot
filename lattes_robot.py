#!/usr/bin/python3

def main(cnpq_ids, dest_dir = './cvs'):
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
            print('#[{:<3d} de {}]  CV "{}" gravado;'.format(n+1, len(cnpq_ids), filename))

if __name__ == '__main__':
    import sys
    from time import time

    t = time()
    try:
        input_file = sys.argv[1]
    except IndexError:
        print('AHHHHH! VC N INFORMOU O ARQUIVO COM OS CODIGOS ...')
        sys.exit(1)
    else:
        with open(input_file, 'r') as f:
            cnpq_ids = f.read().splitlines()
        main(cnpq_ids)
    finally:
        print('script finalizado em {:.1f} segundos'.format(time() - t))
                    
 
