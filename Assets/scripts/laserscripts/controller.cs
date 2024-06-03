using System.Collections;
using Cinemachine;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem.XR;
using UnityEngine.UIElements;

public class controllerlaser : MonoBehaviour
{
    public Transform objectToFollow;
    public float rotationSpeed = 1f;
    public Camera targetCamera;
    public CinemachineVirtualCamera virtualCamera;
    public Background scriptoff;
    public controllforphone touchscript;
    public Transform background;
    public GameObject buttonforlaser;
    public GameObject buttonforactivate;
    public GameObject arrowsoff;
    public float transitionDuration = 1f;
    public RotationConstraint[] controller;

    public Vector2 newbackgroundpos;
    public Vector2 newcamerapos;
    public Vector2 newbackgroundscale;
    public float newcamerascale;
    
    void Update()
    {
        if (objectToFollow != null)
        {
            Vector3 euler = transform.eulerAngles;
            if (euler.z > 180) euler.z = euler.z - 360;
            euler.z = Mathf.Clamp(euler.z, -60, 60);
            transform.eulerAngles = euler;

            Vector3 euller = objectToFollow.eulerAngles;
            if (euller.z > 180) euller.z = euller.z - 360;
            euller.z = Mathf.Clamp(euller.z, -60, 60);
            objectToFollow.eulerAngles = euller;

            transform.rotation = objectToFollow.rotation;
        }
        complete();

        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            targetCamera.nearClipPlane = -30;
            StartCoroutine(TransitionBackgroundAndCamera());
            scriptoff.enabled = false;
            touchscript.enabled = true;
            virtualCamera.enabled = false;
            arrowsoff.SetActive(false);




        }
    }
    private IEnumerator transitduration()
    {
        yield return new WaitForSeconds(1f);
        background.localScale = new Vector2(1, 1);
        arrowsoff.SetActive(true);
        scriptoff.enabled = true;
        touchscript.enabled = false;
        virtualCamera.enabled = true;
        buttonforactivate.SetActive(true);
    }
    private void complete()
    {
        if (!buttonforlaser.activeSelf)
        {
            controller[0].enabled = true;
            controller[1].enabled = true;
            StartCoroutine(transitduration());
        }
    }

    private IEnumerator TransitionBackgroundAndCamera()
    {
        Vector2 initialBackgroundPosition = background.position;
        Vector2 initialBackgroundScale = background.localScale;
    
        Vector3 initialCameraPosition = targetCamera.transform.position;
        float initialCameraOrthographicSize = targetCamera.orthographicSize;
        float initialCameraNearClipPlane = targetCamera.nearClipPlane;

        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            float t = elapsedTime / transitionDuration;
            
            background.position = Vector2.Lerp(initialBackgroundPosition, newbackgroundpos, t);
            background.localScale = Vector2.Lerp(initialBackgroundScale, newbackgroundscale, t);
            
            targetCamera.orthographicSize = Mathf.Lerp(initialCameraOrthographicSize, newcamerascale, t);
            targetCamera.nearClipPlane = Mathf.Lerp(initialCameraNearClipPlane, -30f, t);
            targetCamera.transform.position = Vector2.Lerp(initialCameraPosition, newcamerapos, t);
            
            elapsedTime += Time.deltaTime;
            
            yield return null;
        }

        
        background.position = newbackgroundpos;
        background.localScale = newbackgroundscale;
        targetCamera.orthographicSize = newcamerascale;
        targetCamera.transform.position = newcamerapos;
    }
}

