using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Basket : MonoBehaviour
{
    public ScoreCounter scoreCounter;
    public Round roundCounter;
    public int numApples;
    // Start is called before the first frame update
    void Start()
    {
        GameObject scoreGo = GameObject.Find("ScoreCounter");
        scoreCounter = scoreGo.GetComponent<ScoreCounter>();

        GameObject roundGo = GameObject.Find("Round");
        roundCounter = roundGo.GetComponent<Round>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get the current screen position of the mouse from Input
        Vector3 mousePos2D = Input.mousePosition;

        mousePos2D.z = -Camera.main.transform.position.z;

        // Convert the point from 2D screen space into 3D game world space
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        // Move the x position of this Basket to the x position of the Mouse
        Vector3 pos = this.transform.position;
        pos.x = mousePos3D.x;
        this.transform.position = pos;
    }

    void OnCollisionEnter(Collision coll){
        // Find out what hit this basket
        GameObject collideWith = coll.gameObject;
        if(collideWith.CompareTag("Apple")){
            numApples++;
            if(roundCounter.round < 4){
                if(numApples == 5){
                    roundCounter.round += 1;
                } else if(numApples == 10){
                    roundCounter.round += 1;
                } else if(numApples == 15){
                    roundCounter.round += 1;
                }
            }
            
            Destroy(collideWith);
            scoreCounter.score += 100;
            HighScore.TRY_SET_HIGH_SCORE(scoreCounter.score);
        } else if(collideWith.CompareTag("Branch")){
            Destroy(collideWith);
            HighScore.TRY_SET_HIGH_SCORE(scoreCounter.score);
            SceneManager.LoadScene("GameOver");
        }
    }
}
