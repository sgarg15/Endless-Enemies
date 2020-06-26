using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JoinLobbyMenu : MonoBehaviour {

  [SerializeField]
  private NetworkManagerLobby networkManager = null;

  [Header("UI")]
  [SerializeField]
  private GameObject MainMenu = null;
  [SerializeField]
  private TMP_InputField ipAddressInputField = null;
  [SerializeField]
  private Button joinButton = null;

  private void OnEnable(){
    NetworkManagerLobby.OnClientConnected += HandleClientConnected;
    NetworkManagerLobby.OnClientDisconnected += HandleClientDisconnected;
  }

  private void OnDisable(){
    NetworkManagerLobby.OnClientConnected -= HandleClientConnected;
    NetworkManagerLobby.OnClientDisconnected -= HandleClientDisconnected;
  }

  public void JoinLobby(){
    string ipAddress = ipAddressInputField.text;
    networkManager.networkAddress = ipAddress;

    if(ipAddress == "hostIP"){
      networkManager.networkAddress = "216.232.168.16";
    }
    
    networkManager.StartClient();

    joinButton.interactable = false;
  }

  private void HandleClientConnected(){
    joinButton.interactable = true;

    gameObject.SetActive(false);
    MainMenu.SetActive(false);
  }

  private void HandleClientDisconnected(){
    joinButton.interactable = true;
  }
}
