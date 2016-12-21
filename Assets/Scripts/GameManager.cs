using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    Text scoreText;
    Text timeText;
    static int score;
    public float gameTime;
    Vector3 CarStart;
    Vector3 BallStart;
    // Use this for initialization
    bool inGoal;
    void Start ()
    {
        CarStart = GameObject.FindGameObjectWithTag("Player").transform.position;
        BallStart = GameObject.FindGameObjectWithTag("Ball").transform.position;
        scoreText = GameObject.Find("score").GetComponent<Text>();
        timeText = GameObject.Find("Time").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        scoreText.text = score.ToString();
        timeText.text = gameTime.ToString();
        gameTime -= Time.deltaTime;
        if(gameTime < 0)
        {
            Application.LoadLevel(0);
        }

	
	}
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ball")
        {
            score++;
        }
        GameObject.FindGameObjectWithTag("Ball").transform.position =  BallStart;
        GameObject.FindGameObjectWithTag("Ball").GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
        GameObject.FindGameObjectWithTag("Player").transform.position = CarStart;
    }
}
