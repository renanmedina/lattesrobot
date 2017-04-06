#!/usr/bin/env python3

def main(cnpq_ids, dest_dir, status_cb):
    import os
    from requests import get

    #http://buscacv.cnpq.br/buscacv/rest/espelhocurriculo/[cnpq_id]
    url_espcv = 'http://buscacv.cnpq.br/buscacv/rest/espelhocurriculo/{}'
    #http://buscacv.cnpq.br/buscacv/rest/download/curriculo/[cod_rh_cript_s]
    url_downcv = 'http://buscacv.cnpq.br/buscacv/rest/download/curriculo/{}'

    if dest_dir is None:
        dest_dir = './curriculos'

    if not os.path.exists(dest_dir):
        os.makedirs(dest_dir)

    for n, cnpq_id in enumerate(cnpq_ids):
        if cnpq_id.strip():
            resp = get(url_espcv.format(cnpq_id))
            if resp.status_code == 200:
                cod_rh_cript_s = resp.json()['cod_rh_cript_s']         
                resp = get(url_downcv.format(cod_rh_cript_s), stream=True)
                if resp.status_code == 200:
                    filename = os.path.join(dest_dir, cnpq_id + '.zip')            
                    with open(filename, 'wb') as cv:
                        cv.write(resp.raw.read())            
                    status_cb(n+1, len(cnpq_ids), filename)            
        

if __name__ == '__main__':
    import argparse

    parser = argparse.ArgumentParser()
    g = parser.add_mutually_exclusive_group()
    g.add_argument('-f', '--file', type=str,
                   help='Indica um arquivo, contendo os IDs, para ser lido;')
    g.add_argument('-i', '--ids', type=str,
                   help='Indica os IDs do curriculos a serem baixados;')
    parser.add_argument('-s', '--sep', type=str,
                        help='Quando a opcão for -f, este param indica qual caracter esta '\
                             'sendo usado para representar a separacão entre os IDs;')
    parser.add_argument('-o', '--output', type=str,
                        help='Indica o diretório onde os curriculos baixados serão gravados;')
    parser.add_argument('-v', '--verbose', action='store_true',
                        help='Indica que o app deve mostrar mais detalhes durante a execução;')
    
    args = parser.parse_args()
    args.sep = args.sep if args.sep else '\n'
    
    try:
        if args.file:
            with open(args.file, 'r') as f:
                cnpq_ids = f.read().split(args.sep)
        elif args.ids:
            cnpq_ids = args.ids.split(args.sep)
        else:
            raise Exception('Nenhuma fonte de dados de IDs (arquivo ou manual) foi informada. '\
                            'Use -h ou --help para mais informações.')
        cnpq_ids = list(filter(None, cnpq_ids))
    except (TypeError, FileNotFoundError):        
        print('O arquivo de IDs não foi informado ou é inexistente.')
    except Exception as e:
        print(str(e))
    else:
        try:            
            if args.verbose:
                status_cb_fc = lambda n,m,s: print('[ {} de {} ] "{}" baixado;'.format(n,m,s))
            else:
                import progressbar
                pgbar = progressbar.ProgressBar(max_value=len(cnpq_ids), term_width=75)
                status_cb_fc = lambda n,m,s: pgbar.update(n)
            main(cnpq_ids, dest_dir=args.output, status_cb=status_cb_fc)
            print('\n{} curriculos foram baixados e gravados.'.format(len(cnpq_ids)))
        except KeyboardInterrupt:
            print('\nCancelado pelo usuário.')
        except Exception as e:
            print('\n'+str(e))
    finally:
        print('\n')
        
