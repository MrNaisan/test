using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public ChangeUnitPos changeUnitPos;
    private List<Vector3> poses = new List<Vector3>();
    public RunAnimStart runAnimStart;
    private const float xMin = 72.10532f;
    private const float xMax = 1008.947f;
    private const float yMin = 243.6843f;
    private const float yMax = 870.0001f;
    private const float y = 2.4f;
    private const float x = 3.6f;
    private float xRadius = 0;
    private float yRadius = 0; 
    private float xCenter = xMax - ((xMax - xMin)/2);
    private float yCenter = yMax - ((yMax - yMin)/2);
    private bool runCour = false;
    private bool isFirstDraw = false;

   private void Start() 
    {
        xRadius = xCenter - xMin;
        yRadius = yCenter - yMin;
        lineRenderer.positionCount = 0;
        StartCoroutine(Draw());
    }

    private void Update() 
    {
        if(Input.mousePosition.x <= xMax && Input.mousePosition.x >= xMin && Input.mousePosition.y <= yMax && Input.mousePosition.y >= yMin)
        {
            if(Input.GetMouseButtonDown(0))
            {
                runCour = true;
                if(!isFirstDraw)
                {
                    isFirstDraw = true;
                    runAnimStart.StartRunAnim();
                }
            }
            else if(Input.GetMouseButtonUp(0))
            {
                if(isFirstDraw)
                {
                    runCour = false;
                    changeUnitPos.ChangePos(lineRenderer);
                    lineRenderer.positionCount = 0;
                    poses.Clear();
                }
            }
        }
        else
        {
            if(isFirstDraw)
            {
                runCour = false;
                //changeUnitPos.ChangePos(lineRenderer);
                lineRenderer.positionCount = 0;
                poses.Clear();
            }
        }

    }

    private Vector3 GetWorldCoordinate(Vector3 mousePosition)
    {
        float posX = Pos(mousePosition.x, xRadius, x, xCenter);
        float posY = Pos(mousePosition.y, yRadius, y, yCenter);
        Vector3 mousePoint = new Vector3(posX, posY, -1);
        return mousePoint;
    }

    private float Pos(float mousePos, float radius, float index, float Center)
    {
        float pos = 0f;
        if(Center - mousePos < 0f)
            pos = ((mousePos - Center) / radius) * index;
        else
            pos = ((Center - mousePos)/ radius) * index * -1f;
        return pos;
    }

    IEnumerator Draw()
    {
        while(true)
        {
            if(runCour)
            {
                Vector3 currentPoint = GetWorldCoordinate(Input.mousePosition);
                poses.Add(currentPoint);
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, currentPoint);
            }
            yield return new WaitForSeconds(0.03f);
        }
    }
}
