using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerController))]

public class PlayerSetup : NetworkBehaviour {

    [SerializeField]
    Behaviour[] componentsToDisable;

    [SerializeField]
    string remoteLayerNamer = "RemotePlayer";

    Camera sceneCamera;

    [SerializeField]
    GameObject playerUIPrefab;
    private GameObject playerUIInstance;



    void Start()
    {
        if (!isLocalPlayer)
        {

            DisableComponents();
            AssignRemoteLayer();
        }
        else
        {
            sceneCamera = Camera.main;
            if (sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);
            }


            playerUIInstance = Instantiate(playerUIPrefab);
            playerUIInstance.name = playerUIPrefab.name;

          

        }

        GetComponent<Player>().Setup();

    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        string _netID = GetComponent<NetworkIdentity>().netId.ToString();
        Player _player = GetComponent<Player>();
        GameManager.RegisterPlayer(_netID, _player); 
    }
   

    void AssignRemoteLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayerNamer);

    }

    void DisableComponents()
    {
        for (int i = 0; i < componentsToDisable.Length; i++)
        {
            componentsToDisable[i].enabled = false;
        }

    }

    void OnDisable()
    {
        Destroy(playerUIInstance);


         if( sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true);
        }



        GameManager.UnRegisterPlayer(transform.name);
    }


}
