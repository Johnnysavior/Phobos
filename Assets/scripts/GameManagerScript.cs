using UnityEngine;


//Script solo manejar datos entre escenas.
public class GameManagerScript : MonoBehaviour {

    //Singleton
    public static GameManagerScript Instance;
       
    //Nivel actual que se esta jugando
    public int currentLevel = 1;
    //opciones de armas del jugador, cada 1 representa el arma indicada para cada slot.
    public int[] weapons= {1,1,1,1};

    //Configuracion de dificultad
    //0-Facil
    //1-Normal
    //2-Dificil
    //3-Extremo
    public int difficultySetting = 1;

    //Score y high Score...
    public  int score=0 ;
    public  int highScore;
    // Use this for initialization

    public int totalEnemies;
    public int enemiesDefeated=0;
    
    void Awake() {

        if (Instance) DestroyImmediate(gameObject);
        else {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }

    }


    void Start() {
        highScore = 0;
        totalEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        enemiesDefeated = 0;
        
    }
     
	
	// Update is called once per frame
	void Update () {
        if (score > highScore)
        {
            highScore = score;           
        }
     
	}
     
}
