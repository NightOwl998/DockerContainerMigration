using System.Collections.Generic;
using UnityEngine;
using LightReflectiveMirror;
using Mirror;
using UnityEngine.UI;

public class LRMTest : MonoBehaviour
{
    public Transform scrollParent;
    public GameObject serverEntry;
    private LightReflectiveMirrorTransport _LRM;

    void Start()
    {
        if (_LRM == null)
        {
            _LRM = (LightReflectiveMirrorTransport)Transport.activeTransport;
            _LRM.serverListUpdated.AddListener(ServerListUpdate);
        }
    }

    void OnDisable()
    {
        if (_LRM != null)
        {
            _LRM.serverListUpdated.RemoveListener(ServerListUpdate);

        }
    }

    public void RefreshServerList()
    {
        _LRM.RequestServerList();
    }
    void ServerListUpdate()
    {
        foreach (Transform t in scrollParent)
            Destroy(t.gameObject);

        for (int i = 0; i < _LRM.relayServerList.Count; i++)
        {
            var newEntry = Instantiate(serverEntry, scrollParent);
            newEntry.transform.GetChild(0).GetComponent<Text>().text = _LRM.relayServerList[i].serverName;

            string serverID = _LRM.relayServerList[i].serverId;
            newEntry.GetComponent<Button>().onClick.AddListener(() => { ConnectToServer(serverID); });
        }


    }

    void ConnectToServer(string serverID)
    {
        NetworkManager.singleton.networkAddress = serverID.ToString();
        NetworkManager.singleton.StartClient();
    }
}
