// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;

#if UNITY_WSA && UNITY_2017_2_OR_NEWER
using System.Collections.Generic;
using UnityEngine.XR.WSA.Input;
#endif

namespace VR_Test
{
    public class MotionControllersManager : MonoBehaviour
    {

        public HandController RightHand;
        public HandController LeftHand;


#if UNITY_WSA && UNITY_2017_2_OR_NEWER
        public class ControllerState
        {
            public InteractionSourceHandedness Handedness;
            public Vector3 PointerPosition;
            public Quaternion PointerRotation;
            public Vector3 GripPosition;
            public Quaternion GripRotation;
            public bool Grasped;
            public bool MenuPressed;
            public bool SelectPressed;
            public float SelectPressedAmount;
            public bool ThumbstickPressed;
            public Vector2 ThumbstickPosition;
            public bool TouchpadPressed;
            public bool TouchpadTouched;
            public Vector2 TouchpadPosition;
        }

        private Dictionary<uint, ControllerState> controllers;
#endif

        private void Awake()
        {
#if UNITY_WSA && UNITY_2017_2_OR_NEWER
            controllers = new Dictionary<uint, ControllerState>();

            InteractionManager.InteractionSourceDetected += InteractionManager_InteractionSourceDetected;

            InteractionManager.InteractionSourceLost += InteractionManager_InteractionSourceLost;
            InteractionManager.InteractionSourceUpdated += InteractionManager_InteractionSourceUpdated;

            
#endif
        }

        void SelectHand()
        {
            if (RightHand == null)
            {
                var rightController = GameObject.Find("RightController");
                if (rightController != null)
                {
                    RightHand = rightController.transform.GetChild(0).GetComponent<HandController>();
                }
            }

            if (LeftHand == null)
            {
                var leftController = GameObject.Find("LeftController");
                if (leftController != null)
                {
                    LeftHand = leftController.transform.GetChild(0).GetComponent<HandController>();
                }
            }
        }

        
        private void Start()
        {
            

        }

#if UNITY_WSA && UNITY_2017_2_OR_NEWER
        private void InteractionManager_InteractionSourceDetected(InteractionSourceDetectedEventArgs obj)
        {
            Debug.LogFormat("{0} {1} Detected", obj.state.source.handedness, obj.state.source.kind);

            if (obj.state.source.kind == InteractionSourceKind.Controller && !controllers.ContainsKey(obj.state.source.id))
            {
                controllers.Add(obj.state.source.id, new ControllerState { Handedness = obj.state.source.handedness });
            }
        }

        private void InteractionManager_InteractionSourceLost(InteractionSourceLostEventArgs obj)
        {
            Debug.LogFormat("{0} {1} Lost", obj.state.source.handedness, obj.state.source.kind);

            controllers.Remove(obj.state.source.id);
        }

        private void InteractionManager_InteractionSourceUpdated(InteractionSourceUpdatedEventArgs obj)
        {
            ControllerState controllerState;
            if (controllers.TryGetValue(obj.state.source.id, out controllerState))
            {
                obj.state.sourcePose.TryGetPosition(out controllerState.PointerPosition, InteractionSourceNode.Pointer);
                obj.state.sourcePose.TryGetRotation(out controllerState.PointerRotation, InteractionSourceNode.Pointer);
                obj.state.sourcePose.TryGetPosition(out controllerState.GripPosition, InteractionSourceNode.Grip);
                obj.state.sourcePose.TryGetRotation(out controllerState.GripRotation, InteractionSourceNode.Grip);

                controllerState.Grasped = obj.state.grasped;
                controllerState.MenuPressed = obj.state.menuPressed;
                controllerState.SelectPressed = obj.state.selectPressed;
                controllerState.SelectPressedAmount = obj.state.selectPressedAmount;
                controllerState.ThumbstickPressed = obj.state.thumbstickPressed;
                controllerState.ThumbstickPosition = obj.state.thumbstickPosition;
                controllerState.TouchpadPressed = obj.state.touchpadPressed;
                controllerState.TouchpadTouched = obj.state.touchpadTouched;
                controllerState.TouchpadPosition = obj.state.touchpadPosition;
            }


        }
#endif

      /*  private string GetControllerInfo()
        {
            string toReturn = string.Empty;
#if UNITY_WSA && UNITY_2017_2_OR_NEWER
            foreach (ControllerState controllerState in controllers.Values)
            {
                // Debug message
                toReturn += string.Format("Hand: {0}\nPointer: Position: {1} Rotation: {2}\n" +
                                          "Grip: Position: {3} Rotation: {4}\nGrasped: {5} " +
                                          "MenuPressed: {6}\nSelect: Pressed: {7} PressedAmount: {8}\n" +
                                          "Thumbstick: Pressed: {9} Position: {10}\nTouchpad: Pressed: {11} " +
                                          "Touched: {12} Position: {13}\n\n",
                                          controllerState.Handedness, controllerState.PointerPosition, controllerState.PointerRotation.eulerAngles,
                                          controllerState.GripPosition, controllerState.GripRotation.eulerAngles, controllerState.Grasped,
                                          controllerState.MenuPressed, controllerState.SelectPressed, controllerState.SelectPressedAmount,
                                          controllerState.ThumbstickPressed, controllerState.ThumbstickPosition, controllerState.TouchpadPressed,
                                          controllerState.TouchpadTouched, controllerState.TouchpadPosition);

                // Text label display
                if (controllerState.Handedness.Equals(InteractionSourceHandedness.Left))
                {
                    LeftInfoTextPointerPosition.text = controllerState.Handedness.ToString();
                    LeftInfoTextPointerRotation.text = controllerState.PointerRotation.ToString();
                    LeftInfoTextGripPosition.text = controllerState.GripPosition.ToString();
                    LeftInfoTextGripRotation.text = controllerState.GripRotation.ToString();
                    LeftInfoTextGripGrasped.text = controllerState.Grasped.ToString();
                    LeftInfoTextMenuPressed.text = controllerState.MenuPressed.ToString();
                    LeftInfoTextTriggerPressed.text = controllerState.SelectPressed.ToString();
                    LeftInfoTextTriggerPressedAmount.text = controllerState.SelectPressedAmount.ToString();
                    LeftInfoTextThumbstickPressed.text = controllerState.ThumbstickPressed.ToString();
                    LeftInfoTextThumbstickPosition.text = controllerState.ThumbstickPosition.ToString();
                    LeftInfoTextTouchpadPressed.text = controllerState.TouchpadPressed.ToString();
                    LeftInfoTextTouchpadTouched.text = controllerState.TouchpadTouched.ToString();
                    LeftInfoTextTouchpadPosition.text = controllerState.TouchpadPosition.ToString();
                }
                else if (controllerState.Handedness.Equals(InteractionSourceHandedness.Right))
                {
                    RightInfoTextPointerPosition.text = controllerState.PointerPosition.ToString();
                    RightInfoTextPointerRotation.text = controllerState.PointerRotation.ToString();
                    RightInfoTextGripPosition.text = controllerState.GripPosition.ToString();
                    RightInfoTextGripRotation.text = controllerState.GripRotation.ToString();
                    RightInfoTextGripGrasped.text = controllerState.Grasped.ToString();
                    RightInfoTextMenuPressed.text = controllerState.MenuPressed.ToString();
                    RightInfoTextTriggerPressed.text = controllerState.SelectPressed.ToString();
                    RightInfoTextTriggerPressedAmount.text = controllerState.SelectPressedAmount.ToString();
                    RightInfoTextThumbstickPressed.text = controllerState.ThumbstickPressed.ToString();
                    RightInfoTextThumbstickPosition.text = controllerState.ThumbstickPosition.ToString();
                    RightInfoTextTouchpadPressed.text = controllerState.TouchpadPressed.ToString();
                    RightInfoTextTouchpadTouched.text = controllerState.TouchpadTouched.ToString();
                    RightInfoTextTouchpadPosition.text = controllerState.TouchpadPosition.ToString();
                }
            }
#endif
            return toReturn.Substring(0, Math.Max(0, toReturn.Length - 2));
        }*/


        private void FixedUpdate()
        {
            SelectHand();
            ParsingControllers();

        }

        void ParsingControllers()
        {

            foreach (ControllerState controllerState in controllers.Values)
            {
               
               
                if (controllerState.Handedness.Equals(InteractionSourceHandedness.Left))
                {
                    LeftHand.PointerRotation = controllerState.PointerRotation;
                    LeftHand.GripPosition = controllerState.GripPosition;
                    LeftHand.GripRotation = controllerState.GripRotation;
                    LeftHand.Grasped = controllerState.Grasped;
                    LeftHand.MenuPressed = controllerState.MenuPressed;
                    LeftHand.SelectPressed = controllerState.SelectPressed;
                    LeftHand.SelectPressedAmount = controllerState.SelectPressedAmount;
                    LeftHand.ThumbstickPressed = controllerState.ThumbstickPressed;
                    LeftHand.ThumbstickPosition = controllerState.ThumbstickPosition;
                    LeftHand.TouchpadPressed = controllerState.TouchpadPressed;
                    LeftHand.TouchpadTouched = controllerState.TouchpadTouched;
                    LeftHand.TouchpadPosition = controllerState.TouchpadPosition;
                }
                else if (controllerState.Handedness.Equals(InteractionSourceHandedness.Right))
                {
                    RightHand.PointerRotation = controllerState.PointerRotation;
                    RightHand.GripPosition = controllerState.GripPosition;
                    RightHand.GripRotation = controllerState.GripRotation;
                    RightHand.Grasped = controllerState.Grasped;
                    RightHand.MenuPressed = controllerState.MenuPressed;
                    RightHand.SelectPressed = controllerState.SelectPressed;
                    RightHand.SelectPressedAmount = controllerState.SelectPressedAmount;
                    RightHand.ThumbstickPressed = controllerState.ThumbstickPressed;
                    RightHand.ThumbstickPosition = controllerState.ThumbstickPosition;
                    RightHand.TouchpadPressed = controllerState.TouchpadPressed;
                    RightHand.TouchpadTouched = controllerState.TouchpadTouched;
                    RightHand.TouchpadPosition = controllerState.TouchpadPosition;
                    
                }
            }

               
        }
    }
}