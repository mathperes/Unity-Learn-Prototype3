using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    private PlayerController playerControllerScript;

    public TextMeshProUGUI scoreText;
    public Transform startingPoint;
    
    public float lerpSpeed;
    public float score;
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();

        playerControllerScript.gameOver = true;
        StartCoroutine(PlayIntro());
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerControllerScript.gameOver)
        {
            GameScore();
            Debug.Log("Your score: " + Mathf.Round(score));
        }
    }

    IEnumerator PlayIntro()
    {

        Vector3 startPos = playerControllerScript.transform.position;
        Vector3 endPos = startingPoint.position;

        float journeyLength = Vector3.Distance(startPos, endPos);
        float startTime = Time.time;

        float distanceCovered = (Time.time - startTime) * lerpSpeed;
        float fractionOfJourney = distanceCovered / journeyLength;

        playerControllerScript.GetComponent<Animator>().SetFloat("Speed_Multiplier",0.5f);

        while (fractionOfJourney < 1)
        {
            distanceCovered = (Time.time - startTime) * lerpSpeed;
            fractionOfJourney = distanceCovered / journeyLength;
            playerControllerScript.transform.position = Vector3.Lerp(startPos, endPos,
            fractionOfJourney);


            yield return null;
        }

        playerControllerScript.GetComponent<Animator>().SetFloat("Speed_Multiplier",1.0f);
        playerControllerScript.gameOver = false;
    }

    void GameScore()
    {
        if (playerControllerScript.isDashing)
        {
            score += 20 * Time.deltaTime;
        }
        else
        {
            score += 10 * Time.deltaTime;
        }

        //scoreText.text = "Score: " + score;
    }
}
