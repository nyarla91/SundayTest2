using Extentions;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Joystick
{
    public class OnScreenJoystickArea : RectTransformable, IPointerDownHandler, IPointerUpHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private OnScreenJoystick _targetJoystick;

        public void OnPointerDown(PointerEventData eventData) => _targetJoystick.Show(eventData.position);
        
        public void OnDrag(PointerEventData eventData) => _targetJoystick.MoveStick(eventData.position);

        public void OnPointerUp(PointerEventData eventData) => _targetJoystick.Hide();
        
        public void OnEndDrag(PointerEventData eventData) => _targetJoystick.Hide();
    }
}