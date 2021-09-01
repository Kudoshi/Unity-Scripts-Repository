using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PickupTextDissapearAnimation : MonoBehaviour
{
    [HideInInspector]
    public float msgDissapearDur;
    [HideInInspector]
    public float moveSpeed;
    [HideInInspector]
    public float fadeDuration;
    private float dissapearTime;
    private TextMeshProUGUI text;
    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        dissapearTime = Time.time + msgDissapearDur;
        StartCoroutine(FadeAnimation());
    }
    private IEnumerator FadeAnimation()
    {
        //yield return new WaitForSeconds(fadeStartTime);
        for (float i = fadeDuration; i >= 0; i-= Time.deltaTime)
        {
            text.color = new Color(1, 1, 1, i);
            yield return null;
        }


    }
    private void Update()
    {
        transform.Translate(Vector3.up * moveSpeed);



        if (Time.time >= dissapearTime)
        {
            Destroy(gameObject);
        }

    }


}
