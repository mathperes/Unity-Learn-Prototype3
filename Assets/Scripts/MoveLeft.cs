using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class MoveLeft : MonoBehaviour
{
    private PlayerController playerController;

    private float leftBound = -15;
    private float startSpeed;
    public float speed = 30;
    //public bool isDashing = false;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        startSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        
            if (playerController.gameOver == false)
            {
                transform.Translate(Vector3.left * speed * Time.deltaTime);
                PLayerDash();
            }

            if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
            {
                Destroy(gameObject);
            }
        
    }

    void PLayerDash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && playerController.isOnGround)
        {
            speed *= 2;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) || !playerController.isOnGround)
        {
            speed = startSpeed;
        }
    }

}
