using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshNinja : MonoBehaviour
{
    public GameObject characterObject;
    public string childMeshName;
    public string childMeshName1;
    public void ActivateMesh()
    {
        Transform childMesh = characterObject.transform.Find(childMeshName);
        Transform childMesh1 = characterObject.transform.Find(childMeshName1);
        if (childMesh != null)
            childMesh.gameObject.SetActive(true);
        if (childMesh1 != null)
            childMesh1.gameObject.SetActive(true);
    }
}
