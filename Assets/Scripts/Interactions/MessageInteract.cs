using Assets.Scripts.Interactions;
using TMPro;
using UnityEngine;

public class MessageInteract : MonoBehaviour, IInteractable
{
    public GameObject messageUIPrefab;

    [TextArea(3, 10)]
    public string messageText = "";

    /*[Header("Exit info")]
    [SerializeField] private string imageExitLocation = "PopUp/Exit/ExitImage";
    [SerializeField] private Sprite _keboardExitImg;
    [SerializeField] private Sprite _gamepadExitImg;*/

    public void Interact(GameObject interactor)
    {
        if (messageUIPrefab == null) return;

       // BLOQUEAR MOVIMIENTO 

        GameObject currentMessageUI = Instantiate(messageUIPrefab);

        TextMeshProUGUI tmpText = currentMessageUI.GetComponentInChildren<TextMeshProUGUI>();
        if (tmpText != null)
            tmpText.text = messageText;

        //Remove outline
        if (gameObject.TryGetComponent<Outline>(out Outline otline)) otline.enabled = false;





        //Exit image based on current device
        /*DeviceDetector deviceDetector = interactor.GetComponent<DeviceDetector>();
        if (deviceDetector)
        {
            Transform exitImage = currentMessageUI.transform.Find(imageExitLocation);
            if (exitImage != null && exitImage.gameObject)
            {
                if (deviceDetector.IsUsingKeyboard())
                {
                    if (_keboardExitImg) exitImage.gameObject.GetComponent<Image>().sprite = _keboardExitImg;
                }
                else
                {
                    if (_gamepadExitImg) exitImage.gameObject.GetComponent<Image>().sprite = _gamepadExitImg;
                }
            }
        }*/

    }

}