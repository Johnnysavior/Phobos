using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour {

   

    public string esceneName;    
    public Transform bossPrefab;
    public static EventManager Instance;    
    public bool fixedPlayerPosition=false;//SI es verdadero, cambia la posicion del jugador al cambiar de nivel.
    

    void Awake() {
        //Instancia unica
        setPlayerPosition();
        if (Instance != null)
        {
            Debug.LogError("Multiple instances of SpecialEffectsHelper!");
        }

        Instance = this;
    }

    public void goToNextLevel() {
        SceneManager.LoadScene(esceneName);
    }

    public void spawnBoss(Vector3 pos) {
        var bossTransform = Instantiate(bossPrefab) as Transform;
        bossTransform.position = pos;
    }

    //busca la posicion del GameObject StartingPos y transporta el jugador a su posicion.
    private void setPlayerPosition() {
        if (fixedPlayerPosition)
        {
            Vector3 pos = GameObject.Find("Player Starting Position").transform.position;
            var Player = GameObject.FindGameObjectWithTag("Player");
            Player.transform.position = pos;
        }
    }
	
}
