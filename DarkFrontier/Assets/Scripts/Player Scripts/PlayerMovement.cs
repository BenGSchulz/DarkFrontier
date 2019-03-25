using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public Vector3 startPos;
    public float moveSpeed;

    private Vector3 hMoveVec;
    private Vector3 vMoveVec;
    private float hInput;
    private float vInput;
    private float hMoveStep;
    private float vMoveStep;

    //private GameObject gameModel;
    //private GameData data;

    private GameObject sm;
    private MySceneManager smScript;
    private SceneSelector ss;

    private Animator animator;

    private SpriteRenderer rend;

    private Rigidbody2D rb;

    private ParticleSystem ps;

    private AudioSource aud;
    public AudioClip death;

    private bool colliding;
    public bool dead;

    private float maxDegree = 90f;
    private float minDegree = 0f;
    private float t = 0f;

    // Use this for initialization
    void Start () {
        //gameModel = GameObject.FindGameObjectWithTag("GameModel");
        //data = gameModel.GetComponent<GameData>();

        transform.position = startPos;
        hInput = 0f;
        vInput = 0f;

        sm = GameObject.FindGameObjectWithTag("SceneManager");
        smScript = sm.GetComponent<MySceneManager>();

        ss = GameObject.FindGameObjectWithTag("Control").GetComponent<SceneSelector>();

        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;

        animator = GetComponent<Animator>();

        rend = GetComponent<SpriteRenderer>();

        ps = GetComponentInChildren<ParticleSystem>();

        aud = GetComponent<AudioSource>();

        dead = false;
    }

    // Update is called once per frame
    void Update() {
        if (!smScript.paused && !dead) {
            hInput = Input.GetAxis("Horizontal");
            vInput = Input.GetAxis("Vertical");

            animator.SetBool("Moving", false);

            hMoveStep = moveSpeed * Time.deltaTime * hInput;
            vMoveStep = moveSpeed * Time.deltaTime * vInput;
            hMoveVec = new Vector3(hMoveStep, 0f, 0f);
            vMoveVec = new Vector3(0f, vMoveStep, 0f);

            if (Mathf.Abs(hInput) > 0.001) {
                if (!colliding) {
                    transform.position += hMoveVec;
                }
                animator.SetBool("Moving", true);
            }

            if (Mathf.Abs(vInput) > 0.001) {
                if (!colliding) {
                    transform.position += vMoveVec;
                }
                animator.SetBool("Moving", true);
            }

            if (hInput > 0f) {
                rend.flipX = false;
                ps.transform.right = Vector3.right;
            } else if (hInput < 0f) {
                rend.flipX = true;
                ps.transform.right = -Vector3.right;
            }
        }
    }

    public IEnumerator dieRoutine() {
        dead = true;

        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().isKinematic = true;

        ps.Play();

        aud.clip = death;
        aud.Play();

        yield return new WaitForSeconds(.5f);

        while (t < 1f) {
            transform.Rotate(Vector3.forward, Mathf.Lerp(minDegree, maxDegree, t));
            t += 1f * Time.deltaTime;

            //Debug.Log(t);
            yield return null;
        }

        ss.Dead();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!smScript.enemies.Contains(collision.gameObject)) {
            //colliding = true;
        }
        //Debug.Log("Collision Entered");
    }

    private void OnTriggerStay2D(Collider2D collision) {
        //Debug.Log("Collision Stay");
    }

    private void OnTriggerExit2D(Collider2D collision) {
        colliding = false;
        //Debug.Log("Collision Exited");
    }
}
