using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private GameObject ball;
    public int ballsLife;
    public GameObject ballPrefab;
    public GameObject paddle;
    public bool launch;
    public float ballSpeed = 10f;
    public Vector2 force;
    public Text ballsLifeText;
    public GameObject bailTrail;
    public GameObject leftLine;
    public GameObject rightLine;


    void Start()
    {
        ball = GameObject.Find("ball");
        paddle = GameObject.Find("paddle");
        ballsLifeText.text = ballsLife.ToString();
        force = new Vector2 (-1f,1f);
        bailTrail = ball.transform.GetChild(0).gameObject;
        leftLine = ball.transform.GetChild(1).gameObject;
        rightLine = ball.transform.GetChild(2).gameObject;
        rightLine.SetActive(false);
        leftLine.SetActive(true);
        resetPaddleAndBall();

    }

    // Update is called once per frame
    void Update()
    {
        if(ball.GetComponent<ball_movement>().outHit){
            ballsLife --;
            ballsLifeText.text = ballsLife.ToString();
            ball.GetComponent<ball_movement>().outHit = false;
            resetPaddleAndBall();
            launch = false;
        }

        if(ballsLife <= 0){
            StartCoroutine(reloadLevel());
        }

        if(!launch){
            ball.transform.position = new Vector3 (paddle.transform.position.x,  paddle.transform.position.y + 1f , paddle.transform.position.z);
            bailTrail.SetActive(false);

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
                force = new Vector2 (-1f,1f);
                rightLine.SetActive(false);
                leftLine.SetActive(true);
            } else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
                force = new Vector2 (1f,1f);
                rightLine.SetActive(true);
                leftLine.SetActive(false);
            }
        }

        if(!launch && Input.GetKey(KeyCode.Space)){
            ball.GetComponent<ball_movement>().rb.AddForce(force.normalized * ballSpeed);
            launch = true;
            bailTrail.SetActive(true);
            rightLine.SetActive(false);
            leftLine.SetActive(false);
        }
    }

    IEnumerator reloadLevel(){
        yield return new WaitForSeconds(2); 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void resetPaddleAndBall(){
        ball.transform.position = new Vector3 (paddle.transform.position.x, paddle.transform.position.y + 1f, paddle.transform.position.z);
        ball.GetComponent<ball_movement>().rb.velocity = new Vector3 (0f,0f,0f);

        paddle.GetComponent<paddle_movement>().rb.velocity = new Vector3 (0f,0f,0f);

        rightLine.SetActive(false);
        leftLine.SetActive(true);

        //paddle.transform.position = new Vector3 (0f, paddle.transform.position.y , paddle.transform.position.z);
    }

    

}
