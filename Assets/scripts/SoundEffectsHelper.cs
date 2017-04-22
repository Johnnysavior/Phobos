using UnityEngine;
using System.Collections;

/// <summary>
/// Para crear instancias de sonidos
/// </summary>
public class SoundEffectsHelper : MonoBehaviour
{

    /// <summary>
    /// Singleton
    /// </summary>
    public static SoundEffectsHelper Instance;

    //sonidos...
    public AudioClip explosionSound;
    public AudioClip playerShotSound1;
    public AudioClip playerShotSound2;
    public AudioClip playerShotSound3;
    public AudioClip playerShotSound4;
    public AudioClip shieldBlock;
    public AudioClip nukeSound;
    public AudioClip hurtSound;
    public AudioClip beamSound;

    void Awake()
    {
        // Register the singleton
        if (Instance != null)
        {
            Debug.LogError("Multiples instancias de SoundEffectsHelper!");
        }
        Instance = this;
        
    }

    public void MakeExplosionSound(){MakeSound(explosionSound);}

    public void MakePlayerShotSound1(){MakeSound(playerShotSound1);}

    public void MakePlayerShotSound2(){MakeSound(playerShotSound2);}

    public void MakePlayerShotSound3(){MakeSound(playerShotSound3);}
       
    public void MakePlayerShotSound4() { MakeSound(playerShotSound4);}

    public void MakeShieldBlockSound() { MakeSound(shieldBlock); }

    public void MakeNukeSound() { MakeSound(nukeSound); }

    public void MakehurtSound() { MakeSound(hurtSound); }

    public void MakeBeamSound() { MakeSound(hurtSound); }

    /// <summary>
    ///Toca un sonido
    /// </summary>
    /// <param name="originalClip"></param>
    private void MakeSound(AudioClip originalClip)
    {
        //El sonido se reproduce en la coordenada que contenga el script.    
        
        AudioSource.PlayClipAtPoint(originalClip, transform.position,0.6f);
    }
}