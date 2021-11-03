using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Gates : MonoBehaviour
{
    public event Action<GameObject> OnKilled;

    public void Kill()
    {
        OnKilled?.Invoke(gameObject);
        Destroy(gameObject);
    }

}
