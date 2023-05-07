using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class nav_generator : MonoBehaviour
{
    private NavMeshSurface nms = null;
    // Start is called before the first frame update
    void Awake()
    {
        nms = GetComponent<NavMeshSurface>();
        nms.BuildNavMesh();
        Invoke(nameof(NavBuildSet), 0.5f);
    }
    void NavBuildSet()
    {
        nms.BuildNavMesh();
    }
}
