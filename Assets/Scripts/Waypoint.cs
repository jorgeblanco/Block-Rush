using System;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    private const int GridSize = 10;
    private PathFinder _pathFinder;
    private Instantiator _instantiator;

    private void Start()
    {
        _pathFinder = FindObjectOfType<PathFinder>();
        _instantiator = FindObjectOfType<Instantiator>();
    }

    public int GetGridSize()
    {
        return GridSize;
    }

    public Vector2Int GetGridPosition()
    {
        var position = transform.position;
        return new Vector2Int(
            Mathf.RoundToInt(position.x / GridSize),
            Mathf.RoundToInt(position.z / GridSize)
        );
    }

    public void SetTopMaterial(Material material)
    {
        MeshRenderer topMeshRenderer = transform.GetComponentInChildren<Top>().GetComponent<MeshRenderer>();
        topMeshRenderer.material = material;
    }

    private void OnMouseOver()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if(!_pathFinder.IsInPath(this))
            {
                _instantiator.PlaceSelectedAt(this);
            }
            else
            {
                Debug.Log("Not placeable: " + gameObject.name);
            }
        }
    }
}
