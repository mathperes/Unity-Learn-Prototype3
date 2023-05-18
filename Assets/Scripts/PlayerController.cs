using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{

    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;
    private MoveLeft MoveLeftScript;

    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;

    public float jumpForce;
    public float gravityModifier;

    public bool gameOver = false;
    public bool canDoubleJump = false;
    public bool isOnGround = true;
    public bool isDashing = false;
    public bool gameStarted = true;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        MoveLeftScript = GetComponent<MoveLeft>();
        Physics.gravity *= gravityModifier;

    }

    // Update is called once per frame
    void Update()
    {

        PlayerJump();

        if (!gameOver)
        {
            PLayerDash();
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            canDoubleJump = false;
            dirtParticle.Play();
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            Debug.Log("GAME OVER " + gameOver);
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1);
        }
    }

    void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1);
            canDoubleJump = true;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && canDoubleJump)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerAnim.SetTrigger("Jump_trig");
            playerAudio.PlayOneShot(jumpSound, 1);
            canDoubleJump = false;
        }
    }

    void PLayerDash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && isOnGround)
        {
            isDashing = true;
            playerAnim.SetFloat("Speed_Multiplier", 2.0f);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) || !isOnGround)
        {
            isDashing = false;
            playerAnim.SetFloat("Speed_Multiplier", 1.0f);
        }
    }

}
