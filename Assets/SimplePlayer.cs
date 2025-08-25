using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;
//using static UnityEditor.Searcher.SearcherWindow.Alignment;
using UnityEngine.InputSystem;

public class SimplePlayer : NetworkBehaviour
{
    private float _horizontal;
    private float _vertical;
    float _speed = 4;
    Vector2 direction;
    [SerializeField] Rigidbody myRBD;
    Camera followCamera;
    [SerializeField] LayerMask layerName;
    float jumpForce = 3;
    bool canJump = false;
    void Start()
    {
        //GetComponent<NetworkTransform>().IsServerAuthoritative()
    }

    // Update is called once per frame
    void Update()
    {
        //if (IsOwner) return;
    }
    public void OnMovement(InputAction.CallbackContext move)
    {
        //_horizontal = move.ReadValue<Vector2>().x;
        print("A");
        if (!IsOwner) return;
        direction = move.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext jump)
    {
        //_horizontal = move.ReadValue<Vector2>().x;
        print("B");
        if (jump.performed && canJump ==true)
        {
            myRBD.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            canJump = false;
        }
    }
    private void FixedUpdate()
    {
        //  myRBD.linearVelocity = new Vector3(_horizontal * _speed, myRBD.linearVelocity.y, _vertical*_speed);
        Vector3 move = new Vector3(direction.x, 0f, direction.y) * _speed;
        myRBD.linearVelocity = new Vector3(move.x, myRBD.linearVelocity.y, move.z);


        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 1.3f, layerName))

        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
            canJump = true;
            /*if( hit.collider.tag == "Ground")
             {

             }*/
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 1.3f, Color.white);
            Debug.Log("Did not Hit");
        }
    }
}
