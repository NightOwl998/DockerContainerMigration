#CRIU Based Migration For LRM NODE Docker 
##This repo explains the steps to implement the CRIU  based docker container migration for lrm node between a primary node and a secondary node 
<ol> <li> Environement details:  
  <ol>  
     <li> OS version: Ubuntu 16.04.7. </li>
     <li> CRIU version: 3.15. </li>
     <li> Docker version: 18.09 (expiremental). </li>
   </ol>
   </li> 
  
<li> setting up the NFS pool between the primary and secondary node: 
  <ol>
   <li> On the primary Node : 
     <ol>
        <li> Install NFS : <br/>
              sudo apt-get update <br/>
              sudo apt-get install nfs-kernel-server  
        </li>
        <li> We’ll now create the root directory of the NFS shares, this is also known as an export folder <br/>
              sudo mkdir /mnt/myshareddir 
        </li>
        <li> Edit the /etc/exports file in a text editor, and add the following directive. <br/>
              /mnt/myshareddir ip_secondary_node (rw,sync,no_subtree_check) 
        </li>
         <li> Make the NFS Share Available to Clients. <br/>
              exportfs -a  
          </li>
         <li> restart the NFS kernel <br/>
              systemctl restart nfs-kernel-server  
          </li>          
       </ol>      
  </li>
          
  <li>  On the secondary node  
      <ol>
         <li> Install NFS :  <br/>
               sudo apt-get update <br/>
               sudo apt-get install nfs-common 
          </li>
          <li> Create a local directory—this will be the mount point for the NFS share: <br/>
               sudo mkdir /var/locally-mounted 
          </li>
          <li> Edit the /etc/fstab by adding the following line : <br/>
               {IP of NFS server}:/mnt/myshareddir /var/locally-mounted nfs defaults 0 0 
          </li>
          <li> Now mount the file share using the following command: <br/>
               mount /var/locally-mounted <br/>
               mount {IP of NFS server}:/mnt/myshareddir  
          </li>
      </ol>
  </li>
  </ol>
  </li> 
  
  <li> CRIU Migration  
    <ol> 
      <li> On the primary Host  
        <ol>
        <li> Run the script Run.sh to run the lrm node  </li>
        <li> --- Launch your VR application and connect your clients ( the VR unity sample is included in Tanks)  </li>
        <li> Run the script Checkpoint.sh to save the state of the container </li>
        </ol>
        </li>
  </li>
    <li> On the secondary Host <br/>
        <ol> 
          <li>Run the script restore.sh to restore the lrm container in its previous state </li>
     <li> The VR sample game is the tank game provided by mirror with LRM incroporated in it in addaition to requestServerList the script that allows us to view and join existing servers and the script NeyworkPingDisplay from mirror that we modified to save the rtt every one 1s in a csv File.</li>
     </ol>  </li>
      </ol>
