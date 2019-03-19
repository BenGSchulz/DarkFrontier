using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    protected GameObject player;
    protected GameObject smObject;
    protected MySceneManager sm;
    protected GameObject controlObject;
    protected SceneSelector ss;
    protected ParticleSystem ps;

    protected AudioSource aud;
    protected AudioClip death;
    //protected SpriteRenderer rend;
    protected bool moving;
    protected int hitsTaken;
    protected int maxHits;

    protected bool dead;

    private float maxDegree = 90f;
    private float minDegree = 0f;
    private float t = 0f;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");

        //rend = GetComponent<SpriteRenderer>();
        moving = false;
        hitsTaken = 0;

        dead = false;
        //Debug.Log("In start of Enemy " + player.activeInHierarchy);
	}
	
	// Update is called once per frame
	void Update () {

	}

    public virtual void Move(float moveSpeed, float minDist) {

        if (player != null && !sm.paused && !dead) {
            if (Vector3.Distance(player.transform.position, transform.position) < minDist) {
                moving = true;
                float step = moveSpeed * Time.deltaTime;
                Vector3 move = Vector3.MoveTowards(transform.position, player.transform.position, step);

                transform.position = move;

                ps.transform.right = player.transform.position - ps.transform.position;
            }
        }
    }

    public virtual void Hit() {
        hitsTaken++;
        if (hitsTaken >= maxHits) {
            Die();
        }
    }

    public virtual void Die() {
        StartCoroutine(dieRoutine());
    }

    public virtual IEnumerator dieRoutine() {
        sm.enemyCount--;
        sm.enemiesKilled++;
        dead = true;

        if (sm.enemiesKilled == sm.enemyGoal) {
            ss.Win();
        }

        float rand = Random.value;

        if (rand > .6f) {
            sm.ammoDrop(transform.position);
        }

        ps.Play();

        aud.clip = death;
        aud.Play();

        yield return new WaitForSeconds(.25f);

        while (t < 1f) {
            transform.Rotate(Vector3.forward, Mathf.Lerp(minDegree, maxDegree, t));
            t += .7f * Time.deltaTime;

            //Debug.Log(t);
            yield return null;
        }

        Destroy(gameObject);
    }
}
