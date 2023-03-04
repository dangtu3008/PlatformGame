using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByPoints : MonoBehaviour
{
    int pointIndex = 0;
    [SerializeField] private Transform pointsHolder;
    [SerializeField] private List<Transform> points;
    [SerializeField] private float speed;

    private void Start()
    {
        this.LoadPoints();
    }

    private void Update()
    {
        PointMoving();
    }

    protected void LoadPoints()
    {
        string name = transform.name + "_Points";
        this.pointsHolder = GameObject.Find(name).transform;
        foreach (Transform point in this.pointsHolder)
        {
            this.points.Add(point);
        }
    }

    protected void PointMoving()
    {
        float step = this.speed * Time.deltaTime;
        Transform currentPoints = this.CurrentPoint();
        transform.position = Vector3.MoveTowards(transform.position, currentPoints.position, step);
        if (transform.position == currentPoints.position)
            pointIndex++;
        if (this.pointIndex >= this.points.Count) this.pointIndex = 0;
    }

    protected Transform CurrentPoint()
    {
        return this.points[this.pointIndex];
    }
}
