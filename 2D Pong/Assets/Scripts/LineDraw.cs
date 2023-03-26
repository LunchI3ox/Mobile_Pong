using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LineDraw : MonoBehaviour
{
    public GameObject LinePreFabs;
    public LayerMask CantDrawOverLayer;
    int cantDrawOverLayerIndex;
    


    public float linePointsMinDistance;
    public float lineWidth;
    public Gradient lineColor;

    Line currentLine;
    private void Start()
    {
        cantDrawOverLayerIndex = LayerMask.NameToLayer("CantDrawOver");
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Destroy(GameObject.FindWithTag("Line"));
            BeginDraw();
        }
        if (currentLine != null)
        {
            Draw();
        }
        if (Input.GetMouseButtonUp(0))
        {
            EndDraw();
            
        }
    }
    private void BeginDraw()
    {


            currentLine = Instantiate(LinePreFabs, this.transform).GetComponent<Line>();

            currentLine.SetLineColor(lineColor);
            currentLine.SetLineWidth(lineWidth);
            currentLine.SetPointMinDistance(linePointsMinDistance);
            currentLine.UsePhysics(false);
      
    }
    private void Draw()
    {
        Vector2 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.CircleCast(MousePos, lineWidth / 3f, Vector2.zero, 1f, CantDrawOverLayer);

        if (hit)
        {
            EndDraw();
        }
        else
        {
            currentLine.Addpoint(MousePos);
        }

    }

    private void EndDraw()
    {
        if(currentLine!!= null) 
        {
            if(currentLine.pointsCount < 2)
            {
                Destroy(currentLine.gameObject);
            }
            else
            {
                currentLine.gameObject.layer = cantDrawOverLayerIndex;
                currentLine.UsePhysics(true);
                currentLine = null;
            }
        }
    }
}