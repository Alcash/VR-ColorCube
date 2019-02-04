using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VR_Test
{
    public class HandController : MonoBehaviour
    {

        public UsableItem Item;

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

        // Use this for initialization
        void Start()
        {
            
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if(SelectPressed)
                Item.UseItem();
            if(TouchpadTouched)
            {
                Item.TouchPad(TouchpadPosition, TouchpadPressed);
            }
        }
    }
}
