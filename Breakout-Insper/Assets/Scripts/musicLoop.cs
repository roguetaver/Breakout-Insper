using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicLoop : MonoBehaviour
{
    private static musicLoop original;
    public AudioSource aS;

    void Start(){
        aS = GetComponent<AudioSource>();
        aS.Play(0);
    }

    private void Awake()
    {     
        if(original == null){   
            original = this; 
            DontDestroyOnLoad(gameObject);
        }
        else if(original != this){
            Destroy(gameObject); 
        } 
    }
    
}
