using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ball_movement : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public bool outHit;
    public GameObject GM;
    public CinemachineVirtualCamera vcam;
    private CinemachineBasicMultiChannelPerlin noise;
    private float shakeTimer;

    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        outHit = false;
        GM = GameObject.Find("GameManager");
        speed = GM.GetComponent<GameManager>().ballSpeed;
        vcam = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
        noise = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = rb.velocity.normalized * speed;

        if(shakeTimer > 0f){
            shakeTimer -= Time.deltaTime;
        }
        if(shakeTimer <= 0f){
            noise.m_AmplitudeGain = 0f;
        }  
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.transform.tag == "Death"){
            outHit = true;
        }

        if(collision.gameObject.transform.tag == "Brick"){
            shakeCamera(0.5f,0.1f);
        }
    }

    public void shakeCamera(float intensity, float time){
        noise.m_AmplitudeGain = intensity;
        shakeTimer = time;
    }

}
