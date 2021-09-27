#!/bin/sh
sudo  docker run -d --rm -p 172.16.1.16:8080:8080 -p 172.16.1.16:7777:7777/udp -p 172.16.1.16:7776:7776/udp -p 10.0.2.8:8080:8080 -p 10.0.2.8:7777:7777/udp -p 10.0.2.8:7776:7776/udp  -v /root/:/config --name lrm-node derekrs/lrm_node:Bleeding-Edge
