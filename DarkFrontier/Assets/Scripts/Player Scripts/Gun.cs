using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour {

    //private GameObject gameModel;
    //private GameData data;

    public int bulletStock;
    public Image bulletImage;
    public List<Sprite> bulletImages;
    public Text stockText;

    public int bulletCount;
    private int cocked;

    public GameObject pistol;

    private SpriteRenderer rend;
    private SpriteRenderer flash;

    private LayerMask enemyMask;

    private AudioSource aud;
    public List<AudioClip> clips;

	// Use this for initialization
	void Start () {
        //gameModel = GameObject.FindGameObjectWithTag("GameModel");
        //data = gameModel.GetComponent<GameData>();

        //bulletCount = data.bulletCount;
        //bulletStock = data.bulletStock;

        aud = GetComponent<AudioSource>();
        rend = GetComponent<SpriteRenderer>();
        flash = pistol.GetComponent<SpriteRenderer>();

        enemyMask = LayerMask.GetMask("Enemy");
	}
	
	// Update is called once per frame
	void Update () {
        stockText.text = "x " + bulletStock.ToString();
        bulletImage.sprite = bulletImages[(bulletCount*2) - cocked];

        if (Input.GetKeyDown(KeyCode.Space)) {
            shoot();
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            reload();
        }

	}

    public void shoot() {
        if (cocked == 0) {
            if (bulletCount != 0) { 
                cocked = 1;

                aud.clip = clips[0];
                aud.Play();
            } else {
                aud.clip = clips[4];
                aud.Play();
            }

        } else {
            bulletCount--;
            cocked = 0;

            aud.clip = clips[1];
            aud.Play();

            Vector2 firePoint;
            RaycastHit2D hit;

            if (rend.flipX) {
                pistol.transform.position = new Vector2(transform.position.x - .5f, pistol.transform.position.y);
                flash.flipX = true;
                firePoint = new Vector2(pistol.transform.position.x, pistol.transform.position.y);
                hit = Physics2D.Raycast(firePoint, -Vector2.right, 10, enemyMask);
                Debug.DrawRay(firePoint, -Vector2.right*10, Color.green, .25f);
                StartCoroutine(muzzleFlash());
            } else {
                pistol.transform.position = new Vector2(transform.position.x + .5f, pistol.transform.position.y);
                flash.flipX = false;
                firePoint = new Vector2(pistol.transform.position.x, pistol.transform.position.y);
                hit = Physics2D.Raycast(firePoint, Vector2.right, 10, enemyMask); 
                Debug.DrawRay(firePoint, Vector2.right*10, Color.green, .25f);
                StartCoroutine(muzzleFlash());
            }

            if (hit.collider != null) {
                Debug.Log("Hit " + hit.collider.name);
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                if(enemy != null) {
                    enemy.Hit();
                }
            }

            
        }
    }

    public void reload() {
        if (bulletStock > 0 && bulletCount < 6) {
            bulletCount++;
            bulletStock--;

            aud.clip = clips[2];
            aud.Play();
        }

        if (bulletStock == 0) {
            aud.clip = clips[3];
            aud.Play();
        }
    }

    IEnumerator muzzleFlash() {
        flash.enabled = true;
        yield return new WaitForSeconds(0.25f);
        flash.enabled = false;
    }
}
