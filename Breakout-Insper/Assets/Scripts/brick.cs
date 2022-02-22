using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brick : MonoBehaviour
{
    
    public int health;
    public bool invincible;
    public GameObject particles;
    public Sprite[] spritesArray;
    public SpriteRenderer spriteRenderer;
    private GameObject lightComponent;
    public GameObject lightPrefab;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = spritesArray[health - 1];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    private void Hit(){

        Instantiate(lightPrefab,this.transform.position,Quaternion.identity);    
        if(!invincible){
            if(health > 1){
                spriteRenderer.sprite = spritesArray[0];
            }
            Instantiate(particles,this.transform.position,Quaternion.identity);
                    

            health --;
        }

        if(health <= 0){
            this.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision){

        if(collision.gameObject.transform.tag == "Ball"){
            Hit();
        }
    }
}
