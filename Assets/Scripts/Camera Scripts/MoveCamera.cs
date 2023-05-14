using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField]
    Transform playerTransform;

    void Update()
    {
        this.transform.position = playerTransform.position;
    }
}
