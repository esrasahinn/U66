using UnityEngine;

public class MeshDeactivation : MonoBehaviour
{
    public GameObject characterObject;
    public string childMeshName;
    public string childMeshName1;
    public void DectivateMesh()
    {
        Transform childMesh = characterObject.transform.Find(childMeshName);
        Transform childMesh1 = characterObject.transform.Find(childMeshName1);
        if (childMesh != null)
            childMesh.gameObject.SetActive(false);
        if (childMesh1 != null)
            childMesh1.gameObject.SetActive(false);
    }
}
