using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    public float speed = .5f;
    Material material;
    private void Start()
    {
        material = GetComponent<Renderer>().material;
    }
    // Update is called once per frame
    void Update()
    {

        Vector2 offset = new Vector2(Time.deltaTime * speed, 0);

        material.mainTextureOffset += offset;
    }
}
