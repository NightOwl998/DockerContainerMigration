#!/bin/sh
cd /home/locally-mounted

sudo docker create --name  lrm-clone  -v /root/:/config derekrs/lrm_node:Bleeding-Edge
sudo docker start lrm-clone
sudo docker stop lrm-clone
sudo cp -r /home/locally-mounted/checkpoint-lrm /var/lib/docker/containers/$(docker ps -aq --no-trunc --filter name=lrm-clone)/checkpoints
sudo docker start --checkpoint=checkpoint-lrm  lrm-clone 
