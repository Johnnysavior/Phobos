using UnityEngine;


public class Pause : MonoBehaviour
{
    private GameObject player;   
    public bool paused = false;
    private AudioSource bgm; 

    void Start()
    {
        bgm = GameObject.Find("Music").GetComponent<AudioSource>();
        player = GameObject.Find("player");       
        paused = false;
        Time.timeScale = 1f;
    }
    // Update is called once per frame
    void Update()
    {                   

        if (Input.GetButtonDown("Submit") && paused == false)
        {
            Time.timeScale = 0;
            paused = true;
            player.GetComponent<PlayerScript>().enabled = false;
            bgm.Pause();
        }
        else if (Input.GetButtonDown("Submit") && paused == true)
        {
            Time.timeScale = 1;
            paused = false;
            player.GetComponent<PlayerScript>().enabled = true;
            bgm.UnPause();
        }
    }
}