using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeUnitPos : MonoBehaviour
{
    public List<GameObject> units = new List<GameObject>();
    public SplineMove splineMove;
    public float index = 1f;
    public float speed = 1f;
    public List<Vector3> poses = new List<Vector3>();
    private bool isEnd = false;
    public bool isStart = false;

    public GameObject but;

    private void Start() 
    {
        AddUnits();
    }
    private void Update() 
    {
        AddUnits();
        if(!isEnd && isStart)
        {
            splineMove.isStart = true;
            for(int i = 0; i < units.Count; i++)
            {
                if(units[i].transform.localPosition == poses[i])
                    isEnd = true;
                else
                {
                    units[i].transform.localPosition = Vector3.Lerp(units[i].transform.localPosition, poses[i], Time.deltaTime*speed);
                    isEnd = false;
                }
            }  

        }

        if(units.Count == 0)
        {
            splineMove.isStart = false;
            but.SetActive(true);
        }
    }
    public void ChangePos(LineRenderer lineRenderer)
    {
        int objectCount = units.Count;
        float lineLength = 0f;
        for (int i = 0; i < lineRenderer.positionCount - 1; i++)
        {
            lineLength += Vector3.Distance(lineRenderer.GetPosition(i), lineRenderer.GetPosition(i + 1));
        }

        float objectDistance = lineLength / (objectCount + 1);
        float currentDistance = objectDistance;
        poses.Clear();
        for (int i = 0; i < objectCount; i++)
        {
            Vector3 position = FindPositionOnLine(currentDistance, lineRenderer);
            poses.Add(new Vector3(position.x*index, 0, position.y*index));
            currentDistance += objectDistance;
        }
        isStart = true;
        isEnd = false;
    }
    
    Vector3 FindPositionOnLine(float distance, LineRenderer lineRenderer)
    {
        Vector3 position = Vector3.zero;
        float currentLineLength = 0f;
        for (int i = 0; i < lineRenderer.positionCount - 1; i++)
        {
            Vector3 point1 = lineRenderer.GetPosition(i);
            Vector3 point2 = lineRenderer.GetPosition(i + 1);
            float segmentLength = Vector3.Distance(point1, point2);
            if (currentLineLength + segmentLength >= distance)
            {
                float overshoot = (currentLineLength + segmentLength) - distance;
                position = point2 + ((point1 - point2).normalized * overshoot);
                break;
            }
            currentLineLength += segmentLength;
        }
        return position;
    }

    void AddUnits()
    {
        var obj = FindObjectsOfType<ActiveUnit>();
        units.Clear();
        for(int i = 0; i < obj.Length; i++)
        {
            units.Add(obj[i].gameObject);
        } 
    }
    
}
