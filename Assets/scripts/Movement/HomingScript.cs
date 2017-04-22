using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Este Script hace que el objeto que lo tenga se mueva en direccion del objetivo
/// </summary>
public class HomingScript : MonoBehaviour {

    public string targetTag = "Enemy";
    public float speed = 5;
    //public float ignitionDelay = 1;// Tiempo en segundos en que el script empieza a activarse
    //public float acceleration = 0.000001f;
    

    //private float elapsedTime;//Contador de tiempo
    private Transform transformComponent; // Componente Transform de este objeto
    private Rigidbody2D rigidBodyComponent; //Componente fisico del objeto
    private Vector2 targetPos; //Posicion del objetivo
    private Vector2 selfPos; //Posicion propia
  
    
    // Use this for initialization
    void Start () {
        rigidBodyComponent = GetComponent<Rigidbody2D>();        
        transformComponent = GetComponent<Transform>();     
      

    }

    void Update()
    {            
        float step = speed * Time.deltaTime;
        selfPos = GetComponent<Transform>().position;
        Vector2 lastPos = targetPos;
        float angle = Mathf.Atan2(targetPos.y - selfPos.y, targetPos.x - selfPos.x) * Mathf.Rad2Deg;
        transformComponent.eulerAngles = new Vector3(0, 0, angle);
        targetPos = GameObject.FindGameObjectWithTag(targetTag).transform.position;            
        transform.position = Vector2.MoveTowards(transform.position, targetPos, step);                                       
    }		   
}
