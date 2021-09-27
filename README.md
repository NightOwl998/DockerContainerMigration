# DockerContainerMigration
The following repo contains steps to implement a CRIU based docker container migration between a primary node and a secondary node.
0- Environement details: 
   0.1-OS version: Ubuntu 16.04.7.
   0.2-CRIU version: 3.15.
   0.3-Docker version: 18.09 (expiremental).
1- setting up the NFS pool between the primary and secondary node:
    1.1- On the primary Node : 
        1.1.1- Install NFS : 
              sudo apt-get update
              sudo apt-get install nfs-kernel-server
         1.1.2 We’ll now create the root directory of the NFS shares, this is also known as an export folder
              sudo mkdir /mnt/myshareddir
         1.1.3 Edit the /etc/exports file in a text editor, and add the following directive.
              /mnt/myshareddir ip_secondary_node (rw,sync,no_subtree_check)
         1.1.4 Make the NFS Share Available to Clients.
              exportfs -a 
         1.1.5 restart the NFS kernel
              systemctl restart nfs-kernel-server 
              
          P.S: If you have a firewall enabled, you’ll also need to open up firewall access using the sudo "ufw allow command". 
          
    1.2-  On the secondary node 
         1.2.1 Install NFS : 
               sudo apt-get update
               sudo apt-get install nfs-common
          1.2.2 Create a local directory—this will be the mount point for the NFS share:
               sudo mkdir /var/locally-mounted
          1.2.3 Edit the /etc/fstab by adding the following line :
               {IP of NFS server}:/mnt/myshareddir /var/locally-mounted nfs defaults 0 0
          1.2.4 Now mount the file share using the following command:
               mount /var/locally-mounted
               mount {IP of NFS server}:/mnt/myshareddir 
               
  2- CRIU Migration 
    2.1- On the primary Host 
        2.1.1 Run the script Run.sh to run the lrm node 
         --- Launch your VR application and connect your clients ( the VR unity sample is included in Tanks) 
        2.1.2 Run the script Checkpoint.sh to save the state of the container
    2.2- On the secondary Host
        2.2.1 Run the script restore.sh to restore the lrm container in its previous state 
     P.s: The VR sample game is the tank game provided by mirror with LRM incroporated in it in addaition to requestServerList the script that allows us to view and join existing servers and the script NeyworkPingDisplay from mirror that we modified to save the rtt every one 1s in a csv File. 
     
        
         
         
               
  

         




