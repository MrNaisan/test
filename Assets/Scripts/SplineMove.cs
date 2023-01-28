using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class SplineMove : MonoBehaviour
{
    public SplineComputer spline;
    public float speed = 1f;
    public bool reversed = false;
    public bool looped = false;
    private float progress = 0f;
    public bool isStart = false;

    void Update()
    {
        if(isStart)
        {
            if (spline != null)
            {
                progress += Time.deltaTime * speed;
                if (looped) progress %= 1f;
                else progress = Mathf.Clamp01(progress);

                if (reversed) progress = 1f - progress;
                SplineSample sample = spline.Evaluate(progress);
                transform.position = sample.position;
            }
        }
    }
}
