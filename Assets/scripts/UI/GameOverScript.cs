using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Inicia o termina el juego
/// </summary>
public class GameOverScript : MonoBehaviour
{
    private Button[] buttons;
  
    private string levelName;

    void Awake()
    {
        // Obtener los botones.
        buttons = GetComponentsInChildren<Button>();
          
        //Obtener nombre de la escena actual.
        levelName = SceneManager.GetActiveScene().name.ToString();

        //Deshabilitar botones...
        HideButtons();
    }

    //Esconde botones
    public void HideButtons()
    {
        foreach (var b in buttons)
        {
            b.gameObject.SetActive(false);
        }
    }

    //Muestra botones
    public void ShowButtons()
    {
        foreach (var b in buttons)
        {
            b.gameObject.SetActive(true);
        }
    }

    //Salir al menu
    public void ExitToMenu()
    {
      
        SceneManager.LoadScene("Titulo");
    }

    //recargar el nivel actual
    public void RestartGame()
    {

        SceneManager.LoadScene(levelName);
    }
}