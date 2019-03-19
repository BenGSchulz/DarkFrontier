using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextMovement : MonoBehaviour {

    public Text text;
    public Vector3 startPos;
    public Vector3 targetPos;
    public float speed;

	// Use this for initialization
	void Start () {
        StartCoroutine(lerpText(speed));
    }
	
	// Update is called once per frame
	void Update () {
	}

    public IEnumerator lerpText(float time) {
        float elapsedTime = 0f;

        while (elapsedTime < time) {
            text.rectTransform.anchoredPosition = Vector3.Lerp(startPos, targetPos, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
