using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;
using UnityEngine.InputSystem;

public class SimplePlayer : NetworkBehaviour
{
    private float _horizontal;
    private float _vertical;
    float _speed = 4;
    Vector2 direction;
    [SerializeField] Rigidbody myRBD;
    [SerializeField] LayerMask layerName;
    float jumpForce = 5;
    bool canJump = false;
    public void OnMovement(InputAction.CallbackContext move)
    {
        print("A");
        if (!IsOwner) return;
        direction = move.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext jump)
    {
        print("B");
        if (jump.performed && canJump == true)
        {
            myRBD.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            canJump = false;
        }
    }
    private void FixedUpdate()
    {
        if (!IsOwner) return;
        Vector3 move = new Vector3(direction.x, 0f, direction.y) * _speed;
        myRBD.linearVelocity = new Vector3(move.x, myRBD.linearVelocity.y, move.z);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 1.3f, layerName))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
            canJump = true;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 1.3f, Color.white);
            Debug.Log("Did not Hit");
        }
    }
    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            GameManager.Instance.SetCameraTarget(transform);
        }
    }
}
