using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoyOverTime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(destroyOnSeconds());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator destroyOnSeconds(){
        yield return new WaitForSeconds(2); 
        Destroy(this.gameObject);
    }
}
