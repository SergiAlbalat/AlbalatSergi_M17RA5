using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(MoveBehaviour))]

public class Player : MonoBehaviour, InputSystem_Actions.IPlayerActions
{
    private InputSystem_Actions inputActions;
    private AnimationBehaviour _aB;
    private Vector2 _movement;
    private bool _running = false;
    private bool _dancing = false;
    private bool _aiming = false;
    private bool _interact = false;
    private MoveBehaviour _mB;
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
        if(!_dancing)
            _mB.MoveCharacter(new Vector3(_movement.x, 0, _movement.y), _running, _aiming);
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
        {
            _aB.Dance(context.performed);
            _dancing = true;
        }
        if (context.canceled)
        {
            _aB.Dance(false);
            _dancing = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _mB.Jump();
        }
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _aiming = true;
            _aB.Aim(_aiming);
        }
        if (context.canceled)
        {
            _aiming = false;
            _aB.Aim(_aiming);
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (_aiming)
            {
                _aB.Shoot(true);
            }
            else
            {
                _aB.Shoot(false);
            }
        }
        if (context.canceled)
        {
            _aB.Shoot(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("HouseDoor"))
        {
            _interact = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("HouseDoor"))
        {
            _interact = false;
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if(context.performed && _interact)
        {
            if(SceneManager.GetActiveScene().name == "House")
            {
                SceneManager.LoadScene("Mountain");
            }else if(SceneManager.GetActiveScene().name == "Mountain")
            {
                SceneManager.LoadScene("House");
            }
        }
    }
}
