#!/bin/sh
sudo docker checkpoint create --checkpoint-dir=/mnt/myshareddir lrm-node checkpoint-lrm 
sudo chmod 777 checkpoint-lrm
