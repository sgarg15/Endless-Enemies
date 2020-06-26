// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using UnityEngine.SceneManagement;

// public class GameUI : MonoBehaviour {

//   // public Text bulletsLeftUI;
//   public RectTransform healthBar;

//   Menu menu;
//   Player player;

//   float lastHurtTime;

//   // Start is called before the first frame update
//   void Start() {
//     menu = FindObjectOfType<Menu> ();
//     if(FindObjectOfType<Player> () != null){
//       player = FindObjectOfType<Player> ();
//     }
//   }

//   void Update(){
//     float healthPercent = 0;
//     if(player != null){
//       healthPercent = player.health / player.startingHealth;
//     }
//     healthBar.localScale = new Vector3 (healthPercent, 1, 1);
//   }

//   //Input UI
//   public void StartNewGame(){
//     SceneManager.LoadScene("Game");
//   }

//   public void ReturnToMainMenu(){
//     SceneManager.LoadScene("Menu");
//   }
// }
