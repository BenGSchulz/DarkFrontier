using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongEnemy : Enemy {

    public float moveSpeed;
    public float minDist;
    public AudioClip deathSound;

    private Animator animator;
    private SpriteRenderer rend;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");

        animator = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
        ps = GetComponentInChildren<ParticleSystem>();
        //ps = GameGetComponent<ParticleSystem>();

        smObject = GameObject.FindGameObjectWithTag("SceneManager");
        sm = smObject.GetComponent<MySceneManager>();

        controlObject = GameObject.FindGameObjectWithTag("Control");
        ss = controlObject.GetComponent<SceneSelector>();

        aud = GetComponent<AudioSource>();
        death = deathSound;

        animator.SetBool("Moving",false);

        maxHits = 2;
    }
	
	// Update is called once per frame
	void Update () {
        moving = false;

        Move(moveSpeed,minDist);

        animator.SetBool("Moving",moving);
    }

    public override void Move(float moveSpeed, float minDist) {
        base.Move(moveSpeed, minDist);
        if (player.transform.position.x - transform.position.x < 0f) {
            rend.flipX = true;
        } else if (player.transform.position.x - transform.position.x > 0f) {
            rend.flipX = false;
        }
    }

    public override void Hit() {
        base.Hit();
        rend.color = Color.red;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject == player && !player.GetComponent<PlayerMovement>().dead && !dead) {
            //Debug.Log("Player take strong damage!");
            StartCoroutine(player.GetComponent<PlayerMovement>().dieRoutine());
            //Cursor.visible = true;
        }
    }
}
