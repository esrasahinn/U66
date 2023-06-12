using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DusmanBileseni : MonoBehaviour
{
    private bool menzilIci = false;

    public void SetMenzilIci()
    {
        menzilIci = true;
    }

    public void SetMenzilDisi()
    {
        menzilIci = false;
    }

    public bool IsMenzilIci()
    {
        return menzilIci;
    }
}