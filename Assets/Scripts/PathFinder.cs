using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] WayPoint _start, _finish;
    Dictionary<Vector2Int, WayPoint> _grid = new Dictionary<Vector2Int, WayPoint>();
    Queue<WayPoint> queue = new Queue<WayPoint>();
    bool _isRunning = true;
    WayPoint searchPoint;

    public List<WayPoint> path = new List<WayPoint>();

    Vector2Int[] directions =
    {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

    public List<WayPoint> GetPath()
    {
        if(path.Count == 0)
        {
            LoadBlocks();
            //SetColorStartAndEnd();
            PathFind();
            CreatePath();
        }
            return path;
    }
    private void CreatePath()
    {
        path.Add(_finish);
        _finish.isPlaceable = false;
        WayPoint prevPoint = _finish.exploredFrom;
        while(prevPoint != _start)
        {
            //prevPoint.SetTopColor(Color.yellow);
            path.Add(prevPoint);
            prevPoint.isPlaceable = false;
            prevPoint = prevPoint.exploredFrom;
        }
        path.Add(_start);
        _start.isPlaceable = false;
        path.Reverse();
    }
    private void PathFind()
    {
        queue.Enqueue(_start);
        while(queue.Count > 0 && _isRunning == true)
        {
            searchPoint = queue.Dequeue();
            searchPoint.isExplored = true;
            CheckForEndpoint();
            ExploreNearPoints();
        }
    }
    private void CheckForEndpoint()
    {
        if (searchPoint == _finish)
        {
            _isRunning = false;
        }
    }
    private void ExploreNearPoints()
    {
        if(!_isRunning) { return; }
        foreach (Vector2Int direction in directions) 
        {
            Vector2Int nearPointCoord = searchPoint.GetGridPos() + direction;
            if(_grid.ContainsKey(nearPointCoord))
            {
                WayPoint nearPoint = _grid[nearPointCoord];
                AddPointToQueue(nearPoint);
            }
        }
    }
    private void AddPointToQueue(WayPoint nearPoint)
    {
        if (nearPoint.isExplored || queue.Contains(nearPoint)) {return;}
        else 
        {
            queue.Enqueue(nearPoint);
            nearPoint.exploredFrom = searchPoint;
        }
    }
    private void LoadBlocks()
    {
        var waypoints = FindObjectsOfType<WayPoint>();
        foreach (var waypoint in waypoints)
        {
            Vector2Int gridPos = waypoint.GetGridPos();
            if (!_grid.ContainsKey(gridPos))
                _grid.Add(gridPos, waypoint);
        }
    }
    /*void SetColorStartAndEnd()
    {
        _start.SetTopColor(Color.green);
        _finish.SetTopColor(Color.red);
    }*/
}
