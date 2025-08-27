using Unity.Cinemachine;
using Unity.Netcode;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    private static GameManager instance;
    [SerializeField] Transform playerprefab;
    [SerializeField] CinemachineCamera cameraRef;
    void Start()
    {
        
    }

    void Awake()
    {
        if(Instance == null)
        {
            instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void GetImage()
    {
        if (IsOwner)
        {
            cameraRef.Follow = playerprefab;
            /* CameraFollow cam = Camera.main.GetComponent<CameraFollow>();
             if (cam != null)
             {
                 cam.transform.SetParent(playerprefab);
                 //cam.SetTarget(playerprefab); 
             }*/
        }
    }
    public override void OnNetworkSpawn()
    {
        print(NetworkManager.Singleton.LocalClientId);
        //  Instantiate(playerprefab);
        InstancëPLayerRPC(NetworkManager.Singleton.LocalClientId);
        GetImage();
    }
    [Rpc(SendTo.Server)]
   public void InstancëPLayerRPC(ulong ownerID)
    {
        Transform player = Instantiate(playerprefab);
       // player.GetComponent<NetworkObject>().(true);
        player.GetComponent<NetworkObject>().SpawnWithOwnership(ownerID, true);

    }
    void Update()
    {
        
    }
    public static GameManager Instance => instance;
}
