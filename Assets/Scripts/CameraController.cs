using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class CameraController : MonoBehaviour
{
   
   #region Variables

   [Header("INSTRUCTIONS")] [TextArea(5, 10)]
   [SerializeField]
   private string instructions;

   [Header("Movement Setup")]
   [SerializeField] private float movementSpeed;
   [SerializeField] private float rotationSpeed;
   [SerializeField] private float zoomSpeed;
   private Vector3 movement = Vector3.zero;
   public bool freeViewOn, viewPointOn, switchOn, isSwitched;

   [Header("UI Setup")] 
   [SerializeField] private Toggle freeViewToggle;
   [SerializeField] private Toggle viewPointToggle;

   [Header("Focus Object")] 
   [SerializeField] private List<Transform> targetPoints;
   [SerializeField] private Transform currentTarget;
   [SerializeField] private float smoothSpeed;
   [SerializeField] private Vector3 offset;
   

   [SerializeField]private KeyCode movementKey = KeyCode.Mouse2;
   [SerializeField]private KeyCode rotationKey = KeyCode.Mouse1;
   
   
   #endregion

   #region Generic Methods

   private void Start()
   {
      EnableFreeViewMode(true);
   }
   

   private void FixedUpdate()
   {
      // Camera Rotation with Mouse
      RotateCamera();
   }

   private void LateUpdate()
   {
      // Camera Zoom with Mouse Wheel
      ZoomCamera();
      
      // Camera Focus with Switch Button 
      FocusCamera();
   }

   #endregion

   #region Custom Methods

   private void RotateCamera()
   {
      var mouseMoveY = Input.GetAxis("Mouse Y");
      var mouseMoveX = Input.GetAxis("Mouse X");

      if (Input.GetKey(movementKey) && freeViewOn)
      {
         movement += Vector3.up * mouseMoveY * -movementSpeed;
         movement += Vector3.right * mouseMoveX * -movementSpeed;
         
         transform.Translate(movement);
      }

      if (!Input.GetKey((rotationKey))) return;
      
      transform.RotateAround(transform.position, transform.right, mouseMoveY * -rotationSpeed);
      transform.RotateAround(transform.position, transform.up, mouseMoveX * -rotationSpeed);
   }

   private void ZoomCamera()
   {
      var mouseScroll = Input.GetAxis("Mouse ScrollWheel");
      transform.Translate(Vector3.forward * mouseScroll * zoomSpeed);
   }

   public void EnableFreeViewMode(bool freeModeTog)
   {
      if (!freeModeTog) return;
      freeViewOn = true;
      viewPointOn = false;

      freeViewToggle.isOn = true;
      viewPointToggle.isOn = false;

      freeViewToggle.enabled = false;
      viewPointToggle.enabled = true;

   }

   public void EnableViewPointMode(bool viewPointTog)
   {
      if(!viewPointTog) return;
      viewPointOn = true;
      freeViewOn = false;
      
      viewPointToggle.isOn = true;
      freeViewToggle.isOn = false;
      
      freeViewToggle.enabled = true;
      viewPointToggle.enabled = false;
   }

   public void SwitchCamera()
   {
      switchOn = true;
   }

   private void FocusCamera()
   {
      if (switchOn)
      {
         currentTarget = targetPoints[Random.Range(0, targetPoints.Count)];
         switchOn = false;
         isSwitched = false;
      }

      if (currentTarget != null && !isSwitched)
      {
         Vector3 desiredPosition = currentTarget.position + offset;
         Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
         transform.position = smoothedPosition;
         transform.LookAt((currentTarget));
         if(Vector3.Distance(transform.position, desiredPosition) <= 0.1f) isSwitched = true;
      }
   }
   
   #endregion
}
