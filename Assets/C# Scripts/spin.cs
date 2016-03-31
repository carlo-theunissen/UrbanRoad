using UnityEngine;
using System.Collections;

public class spin : MonoBehaviour
{
    public float speed = 0.1f;


    void Update()
    {
        transform.Rotate(0, 0, speed );
    }
}