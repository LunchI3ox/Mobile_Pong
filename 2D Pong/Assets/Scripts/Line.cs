using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;
    public Rigidbody2D body2D;

    [HideInInspector] public List<Vector2> points = new List<Vector2>();
    [HideInInspector] public int pointsCount = 0;

    private float pointsMinDistance = 0.1f;
    private float CircleColliderRadius;

    public Vector2 GetLastPoint()
    {
        return (Vector2)lineRenderer.GetPosition(pointsCount - 1);
    }

    public void UsePhysics(bool usePhysics)
    {
        body2D.isKinematic = !usePhysics;
    }
    public void SetLineColor(Gradient LineColor)
    {
        lineRenderer.colorGradient = LineColor;
    }

    public void SetLineWidth(float width)
    {
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;

        edgeCollider.edgeRadius = width / 2f;
        CircleColliderRadius = width / 2f;
    }


    public void Addpoint(Vector2 newPoint)
    {
        if (pointsCount >= 1 && Vector2.Distance(newPoint, GetLastPoint()) < pointsMinDistance)
            return;
        points.Add(newPoint);
        pointsCount++;


        //Add Circle Collider
        CircleCollider2D circleCollider = this.GetComponent<CircleCollider2D>();
        circleCollider.radius = CircleColliderRadius;
        circleCollider.offset = newPoint;

        //Add Linerenderer
        lineRenderer.positionCount = pointsCount;
        lineRenderer.SetPosition(pointsCount - 1, newPoint);

        //EdgeCollider
        if (pointsCount > 1)
        {
            edgeCollider.points = points.ToArray();
        }
    }


    public void SetPointMinDistance(float distance)
    {
        pointsMinDistance = distance;
    }
    public void DestroyLine(GameObject line)
    {
        Destroy(gameObject);
    }
}
