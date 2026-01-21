using UnityEngine;

public class CloseUpController : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private FirstPersonController firstPersonController;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private PlayerInputHandler playerInputHandler;
    [SerializeField] private ControllerManager controllerManager;

    private Camera currentCamera;


    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Start is called before the first frame update
    void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    // Update is called once per frame
    void Update()
    {
        HandleClick();
    }


    private void HandleClick()
    {
        if (playerInputHandler.BackTriggered) 
        {
            controllerManager.ChangeControllers(true, mainCamera);
        }
    }

    public void SetCurrentCamera(Camera camera) 
    {
        currentCamera = camera;
    }

}
