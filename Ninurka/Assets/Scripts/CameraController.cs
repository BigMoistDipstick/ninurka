﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{ 
    Camera cam;                         //declares cam var as camera (used throughout the script)
    public Transform target;            //declares target var as transform (used in conjunction with camera to function)
    public Vector3 offset;              //declares offset var as vector3 (position) (used to configure default camera position)
    public float camSpeed = 150f;       //declares camSpeed var as float (used to change the speed of changing camera angle)
    
    //ZOOM
    private float currentZoom = 10f;    //declares currentZoom as float (used to know how close the camera is to player)
    public float zoomSpeed = 7f;        //declares zoomSpeed var as float (used to change the speed of zoom)
    public float minZoom = 3f;          //declares minZoom var as float (used to clamp the closest value of zoom)
    public float maxZoom = 30f;         //declares maxZoom var as float (used to clamp the furthest value of zoom)

    //YAW (horizontal camera rotation)
    private float currentYaw = 0f;      //declares currentYaw var as float (used to keep the current value of the yaw)

    //PITCH (vertical camera rotation)
    private float currentPitch = 0f;    //declares currentPitch var as float (used to keep the current value of the pitch)
    public float minPitch = -35f;       //declares minPitch var as float (used to clamp the max height of camera rotation)
    public float maxPitch = 25f;        //declares maxPitch var as float (used to clamp the min height of camera rotation)


    private void Start()
    {
        cam = Camera.main; //connects cam var to camera in unity
    }

    void Update()
    {
        target.transform.LookAt(cam.transform); //changes rotation of 'target' object to face camera

        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;          //currentZoom var = mouse wheel multiplied by zoomSpeed var
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);               //currentZoom var values clamped between minZoom and maxZoom vars
        currentPitch = Mathf.Clamp(currentPitch, minPitch, maxPitch);           //currentPitch var values clamped between minPitch and maxPitch vars

        currentYaw -= Input.GetAxis("Horizontal") * camSpeed * Time.deltaTime;  //currentYaw var = horizontal input multiplied by camSpeed and deltatime
        currentPitch -= Input.GetAxis("Vertical") * camSpeed * Time.deltaTime;  //currentPitch var = vertical input multiplied by camSpeed and deltatime
    }

    private void LateUpdate()                                                   //runs after update
    {
        transform.position = target.position - offset * currentZoom;            //cam position set to default values (given values in vars)
        transform.LookAt(target.position);                                      //cam points at 'target' position

        transform.RotateAround(target.position, Vector3.up, currentYaw);        //cam rotates around 'target' position (only around y axis)
        transform.RotateAround(target.position, target.right, currentPitch);    //cam rotates around 'target' position (only around x, relative to 'target')
    }
}
