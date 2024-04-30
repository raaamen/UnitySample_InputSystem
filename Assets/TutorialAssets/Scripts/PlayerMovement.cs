using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.Animations;

//You need this
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    //Player facing inspector variables
    [Header("Player")]
    [Tooltip("Movement speed of the character in m/s")]
    public float moveSpeed;

    [Tooltip("Rotation speed of the character")]
    public float rotationSensitivity;

    [Header("Camera")]
    [Tooltip("Minimum and maximum rotation of Y")]
    public float minY = -89.0f;
    public float maxY = 89.0f;


    //private variables
    private GameObject _mainCamera;
    private float _threshold = 0.01f;
    private float _targetPitch;
    [SerializeField]
    private Vector2 _move;
    [SerializeField]
    private Vector2 _look;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }


    /// <summary>
    /// LateUpdate is called every frame, if the Behaviour is enabled.
    /// It is called after all Update functions have been called.
    /// </summary>
    void FixedUpdate()
    {
        Move();
        MouseLook();
    }

    private void MouseLook(){
        //_mainCamera.transform.position = transform.position;
        //Debug.Log("MouseLook");
        if (_look.sqrMagnitude >= _threshold){
            
            _targetPitch -= _look.y * rotationSensitivity;
            float velocity = _look.x * rotationSensitivity;

            if (_targetPitch < -360f) _targetPitch += 360f;
            if (_targetPitch > 360f) _targetPitch -= 360f;

            _targetPitch = Mathf.Clamp(_targetPitch, minY, maxY);

            _mainCamera.transform.localRotation = Quaternion.Euler(_targetPitch, 0, 0);
            transform.Rotate(Vector3.up * velocity);
        }
    }

    private void Move(){
        Vector3 inputDirection = new Vector3(_move.x, 0.0f, _move.y).normalized;

        transform.Translate(inputDirection.normalized * (Time.deltaTime * moveSpeed));

    }

    public void OnMove(InputValue value){
        _move = value.Get<Vector2>();
        Debug.Log("OnMove value: "+value.Get<Vector2>());
    }

    public void OnLook(InputValue value){
        _look = value.Get<Vector2>();
        //Debug.Log("OnLook value: "+value.Get<Vector2>());
    }

}
