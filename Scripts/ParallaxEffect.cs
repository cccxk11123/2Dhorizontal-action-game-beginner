using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Camera cam;
    public Transform followTarget;

    //物体初始位置
    Vector2 startingPosition;

    //物体距离z轴的开始距离
    float startingZ;

    //摄像机移动的距离
    Vector2 canMoveSinceStart => (Vector2)cam.transform.position - startingPosition;

    float zDistanceFromTarget => transform.position.z - followTarget.transform.position.z;

    float clippingPlane => (cam.transform.position.z + (zDistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));

    //物体离z轴越近移动的距离越快
    float parallaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPlane;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        startingZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        //当target移动时，进行移动
        Vector2 newPosition = startingPosition + canMoveSinceStart * parallaxFactor;

        transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
    }
}
