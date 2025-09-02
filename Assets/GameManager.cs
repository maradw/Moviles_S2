using Unity.Cinemachine;
using Unity.Netcode;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    private static GameManager instance;
    [SerializeField] Transform playerprefab;
    [SerializeField] CinemachineCamera cameraRef;
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
    public void SetCameraTarget(Transform playerTransform)
    {
        cameraRef.Follow = playerTransform;
        cameraRef.LookAt = playerTransform;
    }
    public override void OnNetworkSpawn()
    {
        print(NetworkManager.Singleton.LocalClientId);
        InstancePLayerRPC(NetworkManager.Singleton.LocalClientId);
    }
    [Rpc(SendTo.Server)]
   public void InstancePLayerRPC(ulong ownerID)
   {
        Transform player = Instantiate(playerprefab);
        player.GetComponent<NetworkObject>().SpawnWithOwnership(ownerID, true);
   }
    public static GameManager Instance => instance;
}
/* if (ownerID == NetworkManager.Singleton.LocalClientId)
 {
     SetCameraTarget(player);
 }*/