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
    void Start()
    {
        //GetComponent<NetworkTransform>().IsServerAuthoritative()
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOwner) return;
    }
    public void OnMovement(InputAction.CallbackContext move)
    {
        //_horizontal = move.ReadValue<Vector2>().x;
        print("A");
        direction = move.ReadValue<Vector2>();
    }
    private void FixedUpdate()
    {
        //  myRBD.linearVelocity = new Vector3(_horizontal * _speed, myRBD.linearVelocity.y, _vertical*_speed);
        Vector3 move = new Vector3(direction.x, 0f, direction.y) * _speed;
        myRBD.linearVelocity = new Vector3(move.x, myRBD.linearVelocity.y, move.z);
    }
}
