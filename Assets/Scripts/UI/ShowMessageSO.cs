using TMPro;
using UnityEngine;


[CreateAssetMenu(fileName = "ShowMessage", menuName = "Scriptable Objects/ShowMessageSO")]
public class ShowMessageSO : ScriptableObject
{
    public GameObject messageUIPrefab;

    [Header("Exit info")]
    //[SerializeField] private string imageExitLocation = "PopUp/Exit/ExitImage";
    [SerializeField] private Sprite _keboardExitImg;
    [SerializeField] private Sprite _gamepadExitImg;

    public bool ShowMessage(string messageText)
    {

        if (messageUIPrefab == null) return false;
        GameObject currentMessageUI = Instantiate(messageUIPrefab);

        TextMeshProUGUI tmpText = currentMessageUI.GetComponentInChildren<TextMeshProUGUI>();
        if (tmpText != null)
            tmpText.text = messageText;

        return true;

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