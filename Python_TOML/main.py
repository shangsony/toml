# -*- coding: utf-8 -*-
from sys import argv
from os.path import exists
from toml import load


if __name__ == '__main__':
    if len(argv) < 2:
        print('缺少必要参数!')
        exit(-1)
    else:
        path = argv[1]

        if not exists(path):
            print(path + ' 不存在！');
        else:
            with open(path, 'r', encoding='utf-8') as fp:
                doc = load(fp)
                print('"Title" = {}'.format(doc['title']))
                print('"hosts" = [')
                a1 = doc['hosts']
                l = len(a1)
                for i in a1:
                    if l > 1:
                        print('\t{},'.format(i))
                        l = l -1
                    else:
                        print('\t{}'.format(i))

                print(']')
                owner_doc = doc['owner']
                print('"owner"."name" = {}'.format(owner_doc['name']))
                print('"owner"."organization" = {}'.format(owner_doc['organization']))
                print('"owner"."bio" = {}'.format(owner_doc['bio']))
                database_doc = doc['database']
                print('"database"."server" = {}'.format(database_doc['server']))
                a2 = database_doc['ports']
                l = len(a2)
                print('"database"."ports" = [')
                for i in a2:
                    if l > 1:
                        print('\t{},'.format(i))
                        l = l - 1
                    else:
                        print('\t{}'.format(i))

                print(']')
                print('"database"."connection_max" = {}'.format(database_doc['connection_max']))
                print('"database"."enabled" = {}'.format(database_doc['enabled']))
                servers_doc = doc['servers']
                alpha_doc = servers_doc['alpha']
                print('"servers"."alpha"."ip" = {}'.format(alpha_doc['ip']))
                print('"servers"."alpha"."dc" = {}'.format(alpha_doc['dc']))
                beta_doc = servers_doc['beta']
                print('"servers"."beta"."ip" = {}'.format(beta_doc['ip']))
                print('"servers"."beta"."dc" = {}'.format(beta_doc['dc']))
                clients_doc = doc['clients']
                a2 = clients_doc['data']
                l = len(a2)
                print('"clients"."data" = [')
                for i in a2:
                    if l > 1:
                        a3 = i
                        l1 = len(a3)
                        print('\t[')
                        for j in a3:
                            if l1 > 1:
                                print('\t\t{},'.format(j))
                                l1 = l1 - 1
                            else:
                                print('\t\t{}'.format(j))
                        print('\t],')
                        l = l - 1
                    else:
                        a3 = i
                        l1 = len(a3)
                        print('\t[')
                        for j in a3:
                            if l1 > 1:
                                print('\t\t{},'.format(j))
                                l1 = l1 - 1
                            else:
                                print('\t\t{}'.format(j))
                        print('\t]')

                print(']')
