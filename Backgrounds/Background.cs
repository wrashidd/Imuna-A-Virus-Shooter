using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This class is responsible for animating the first level's default background
*/
public class Background : MonoBehaviour
{
    private float _speed = 0.17f;
    public float _speedM = 1f;

    public Renderer bgRend;
    private bool stopBG = false;

    // Start is called before the first frame update
    // On start transforms the blood vessel mesh
    // to its default position
    void Start()
    {
        transform.position = new Vector3(0, 2, 27);
    }

    // Update is called once per frame
    // Handles playing and stopping texture movement animation
    void Update()
    {
        if (stopBG == false)
        {
            bgRend.material.mainTextureOffset += new Vector2(0f, _speed * Time.deltaTime * _speedM);
        }

        if (stopBG == true)
        {
            bgRend.material.mainTextureOffset += new Vector2(0f, 0.001f * Time.deltaTime);
        }
    }

    // Sets stopBG boolean to true
    public void StopBGmovement()
    {
        stopBG = true;
    }
}
