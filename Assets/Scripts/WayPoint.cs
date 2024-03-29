using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]

public class WayPoint : MonoBehaviour
{
    public bool isExplored = false;
    public WayPoint exploredFrom;
    public bool isPlaceable = true;

    const int gridSize = 10;
  
    public int GetGridSize () 
    { 
        return gridSize;
    }
    public Vector2Int GetGridPos()
    {
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x / gridSize),
            Mathf.RoundToInt(transform.position.z / gridSize));
    }
    public void SetTopColor(Color color)
    {
        MeshRenderer topMeshRenderer = transform.Find("Top").GetComponent<MeshRenderer>();
        topMeshRenderer.material.color = color;
    }
    void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (isPlaceable)
            {
                FindObjectOfType<TowerFactory>().AddTower(this);
            }
            else
            {
                Debug.Log("������ �������");
            }
        }
    }
}
