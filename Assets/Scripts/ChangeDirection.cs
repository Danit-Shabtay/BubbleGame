using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDirection : MonoBehaviour
{
    public bool isVisible;
    public Vector2 direction;

    void Awake()
    {
        transform.SetAsLastSibling();
        GetComponent<SpriteRenderer>().enabled = isVisible;
    }
}
