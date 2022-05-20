using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshaker : MonoBehaviour
{
    public static Screenshaker Instance { get; private set; }

    public float duration = 0.3f;
    public Vector3 amplitude = Vector3.one;
    public AnimationCurve animationCurve;


    /// <summary>
    /// Awake
    /// </summary>
    /// 
    void Awake()
    {
        Instance = this;
    }


    /// <summary>
    /// PlayScreenshake
    /// </summary>
    /// 
    public void Play()
    {
        StopCoroutine(ScreenshakeCoroutine());
        StartCoroutine(ScreenshakeCoroutine());
    }


    /// <summary>
    /// PlayScreenshakeCoroutine
    /// </summary>
    /// <returns></returns>
    /// 
    IEnumerator ScreenshakeCoroutine()
    {
        float timeCpt = 0f;

        while (timeCpt <= duration)
        {
            float amount = Mathf.Lerp(1f, 0f, animationCurve.Evaluate(timeCpt / duration));
            transform.localPosition = amount * new Vector3(
                amplitude.x * Random.Range(-0.5f, 0.5f),
                amplitude.y * Random.Range(-0.5f, 0.5f),
                amplitude.z * Random.Range(-0.5f, 0.5f));

            timeCpt += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = Vector3.zero;

        yield return null;
    }
}
