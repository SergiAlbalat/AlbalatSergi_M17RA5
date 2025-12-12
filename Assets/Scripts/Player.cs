using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(MoveBehaviour))]

public class Player : MonoBehaviour, InputSystem_Actions.IPlayerActions
{
    private InputSystem_Actions inputActions;
    private AnimationBehaviour _aB;
    private Vector2 _movement;
    private bool _running = false;
    private MoveBehaviour _mB;
    [SerializeField] private Transform cameraPosition;
    [SerializeField] private float cameraSensibility = 10;
    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Player.SetCallbacks(this);
        _mB = GetComponent<MoveBehaviour>();
        _aB = GetComponent<AnimationBehaviour>();
    }
    private void OnEnable()
    {
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }
    private void FixedUpdate()
    {
        _mB.MoveCharacter(new Vector3(_movement.x, 0, _movement.y), _running);

        Vector3 lookDirection = cameraPosition.forward;
        lookDirection.y = 0;
        if(lookDirection.sqrMagnitude > 0.01f)
        {
            Quaternion playerRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, playerRotation, cameraSensibility * Time.deltaTime);
        }
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        _movement = context.ReadValue<Vector2>();
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if(context.performed)
            _running = true;
        if(context.canceled)
            _running = false;
    }

    public void OnDance(InputAction.CallbackContext context)
    {
        if (context.performed)
            _aB.Dance(context.performed);
        if (context.canceled)
            _aB.Dance(false);
    }
}
