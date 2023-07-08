using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleManager : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] SpriteRenderer rightRock;
    [SerializeField] SpriteRenderer leftRock;
    [SerializeField] SpriteRenderer canopusSecondBackground;
    [SerializeField] SpriteRenderer lavaBall;
    private float cameraWidth;
    private float cameraHeight;
    
    // Start is called before the first frame update
    void Start()
    {
        cameraHeight = 2f* mainCamera.orthographicSize;
        cameraWidth = cameraHeight * mainCamera.aspect;
    }

    public float GetCameraWidth()
    {
        return cameraWidth;
    }

    public float GetCameraHeight()
    {
        return cameraHeight;
    }

    public float GetRightRockHeight()
    {
        return rightRock.bounds.size.y;
    }

    public float GetLeftRockHeight()
    {
        return leftRock.bounds.size.y;
    }

    public float GetCanopusSecondBackgroundWidth()
    {
        return canopusSecondBackground.bounds.size.x;
    }

    public float GetLavaBallMinimumXPosition()
    {
        return (-cameraWidth / 2) + (lavaBall.bounds.size.x / 2);
    }

    public float GetLavaBallMaximumXPosition()
    {
        return (cameraWidth / 2) - (lavaBall.bounds.size.x / 2);
    }

    public float GetLavaBallYGenerationPosition()
    {
        return (cameraHeight / 2) + (lavaBall.bounds.size.y / 2) + 1;
    }
}
