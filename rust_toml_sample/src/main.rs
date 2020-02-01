use serde_derive::Deserialize;
use toml::value::*;

use std::fs::File;
use std::io::prelude::*;

#[derive(Deserialize)]
#[derive(Debug)]
struct Owner {
    name : String,
    organization : String,
    bio : String,
    dob : Datetime,
}

#[derive(Deserialize)]
#[derive(Debug)]
struct Database {
    server : String,
    ports : Array,
    connection_max : toml::Value,
    enabled : toml::Value,
}

#[derive(Deserialize)]
#[derive(Debug)]
struct Server {
    ip : String,
    dc : String,
}

#[derive(Deserialize)]
#[derive(Debug)]
struct Servers {
    alpha : Server,
    beta : Server,
}

#[derive(Deserialize)]
#[derive(Debug)]
struct Clients {
    data : Array,
}

#[derive(Deserialize)]
#[derive(Debug)]
struct Config {
    title : String,
    hosts : Array,
    owner : Owner,
    database : Database,
    servers : Servers,
    clients : Clients,
}

fn main() {
    let file_path = "sample.toml";
    let mut file = match File::open(file_path) {
        Ok(f) => f,
        Err(e) => panic!("no such file {} exception:{}", file_path, e)
    };
    let mut str_val = String::new();
    match file.read_to_string(&mut str_val) {
        Ok(s) => s
        ,
        Err(e) => panic!("Error Reading file: {}", e)
    };
    let config : Config = toml::from_str(&str_val).unwrap();

    println!("\"Title\" = {}", config.title);
    let ct = config.hosts.len();
    let mut p = 1;
    println!("\"hosts\" = [");
    for h in config.hosts {
        print!("\t");
        print!("{}",h.as_str().unwrap());
        if p < ct {
            p = p + 1;
            println!(",");
        } else {
            println!("");
        }
    }
    println!("]");
    println!("\"owner\".\"name\" = {}", config.owner.name);
    println!("\"owner\".\"organization\" = {}", config.owner.organization);
    println!("\"owner\".\"bio\" = {}", config.owner.bio);
    println!("\"owner\".\"dob\" = {}", config.owner.dob);
    println!("\"database\".\"server\" = {}", config.database.server);
    let ps = config.database.ports.len();
    p = 1;
    println!("\"database\".\"ports\" = [");
    for q in config.database.ports {
        print!("\t {}", q.as_integer().unwrap());
        if p < ps {
            p = p + 1;
            println!(",");
        } else {
            println!("");
        }
    }
    println!("]");
    println!("\"servers\".\"alpha\".\"ip\" = {}", config.servers.alpha.ip);
    println!("\"servers\".\"alpha\".\"dc\" = {}", config.servers.alpha.dc);
    println!("\"servers\".\"beta\".\"ip\" = {}", config.servers.beta.ip);
    println!("\"servers\".\"beta\".\"dc\" = {}", config.servers.beta.dc);
    let als = config.clients.data.len();
    p = 1;
    println!("\"clients\".\"data\" = [");
    for al in config.clients.data {
        if al.is_array() {
            let su = al.as_array().unwrap();
            let suc = su.len();
            let mut sp = 1;
            println!("\t[");
            for sai in su {
                if sai.is_str() {
                    print!("\t\t{}", sai.as_str().unwrap());
                } else {
                    print!("\t\t{}", sai.as_integer().unwrap());
                }
                if sp < suc {
                    sp = sp + 1;
                    println!(",");
                } else {
                    println!("");
                }                
            }
            print!("\t]");
            if p < als {
                p = p + 1;
                println!(",");
            } else {
                println!("");
            }
        }
    }
    println!("]");
}
