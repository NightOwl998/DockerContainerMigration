# DockerContainerMigration
The following repo contains steps to implement a CRIU based docker container migration between a primary node and a secondary node.<br/>

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
              sudo apt-get install nfs-kernel-server  </li>
        <li> We’ll now create the root directory of the NFS shares, this is also known as an export folder <br/>
              sudo mkdir /mnt/myshareddir </li>
         <li> Edit the /etc/exports file in a text editor, and add the following directive. <br/>
              /mnt/myshareddir ip_secondary_node (rw,sync,no_subtree_check) </li>
         <li> Make the NFS Share Available to Clients. <br/>
              exportfs -a  </li>
         <li> restart the NFS kernel <br/>
              systemctl restart nfs-kernel-server  </li>
              
          P.S: If you have a firewall enabled, you’ll also need to open up firewall access using the sudo "ufw allow command".  <br/>
       
  </ol>
  </li>
          
    <li>  On the secondary node  
      <ol>
         <li> Install NFS :  <br/>
               sudo apt-get update <br/>
               sudo apt-get install nfs-common </li>
          <li> Create a local directory—this will be the mount point for the NFS share: <br/>
               sudo mkdir /var/locally-mounted </li>
          <li> Edit the /etc/fstab by adding the following line : <br/>
               {IP of NFS server}:/mnt/myshareddir /var/locally-mounted nfs defaults 0 0 </li>
          <li> Now mount the file share using the following command: <br/>
               mount /var/locally-mounted <br/>
               mount {IP of NFS server}:/mnt/myshareddir  </li>
      </ol>
  </li>
  </ol>
  </li> 
  
  2. CRIU Migration  <br/>
    2.1- On the primary Host  <br/>
        2.1.1 Run the script Run.sh to run the lrm node  <br/>
         --- Launch your VR application and connect your clients ( the VR unity sample is included in Tanks)  <br/>
        2.1.2 Run the script Checkpoint.sh to save the state of the container <br/>
    2.2- On the secondary Host <br/>
        2.2.1 Run the script restore.sh to restore the lrm container in its previous state <br/>
     P.s: The VR sample game is the tank game provided by mirror with LRM incroporated in it in addaition to requestServerList the script that allows us to view and join existing servers and the script NeyworkPingDisplay from mirror that we modified to save the rtt every one 1s in a csv File.  <br/>
     
        
         
         
               
  

         




