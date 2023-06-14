using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class TransformSecronize : NetworkBehaviour
{

   public NetworkObject NetworkPrefab;

      void Update()
    {
        if(IsOwner && IsServer) {

          
            transform.RotateAround(GetComponentInParent<Transform>().position, Vector3.up, 100f * Time.deltaTime);

        }
    }
}
