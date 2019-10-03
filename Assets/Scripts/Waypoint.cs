using UnityEngine;

public class Waypoint : MonoBehaviour
{
    private const int GridSize = 10;

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
}
