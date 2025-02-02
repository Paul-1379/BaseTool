using UnityEngine;

namespace BaseTool.Movement
{
    [AddComponentMenu("BaseTool/Movement/Old Movement Input")]
    [HelpURL("https://github.com/DarkRewar/BaseTool?tab=readme-ov-file#oldmovementinput")]
    public class OldMovementInput : MonoBehaviour
    {
        [SerializeField]
        protected bool _invertXAxis = false;

        [SerializeField]
        protected bool _invertYAxis = false;

        [SerializeField]
        protected string _jumpButton = "Jump";

        [GetComponent]
        protected IMovable _movableComponent;

        [GetComponent]
        protected IJumpable _jumpableComponent;

        [SerializeField]
        protected bool _handleCursorVisibility = true;

        protected virtual void Awake() => Injector.Process(this);

        protected virtual void OnEnable()
        {
            if (!_handleCursorVisibility) return;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        protected virtual void OnApplicationFocus(bool focus)
        {
            if (!_handleCursorVisibility) return;

            Cursor.visible = !focus;
            Cursor.lockState = focus ? CursorLockMode.Locked : CursorLockMode.None;
        }

#if ENABLE_LEGACY_INPUT_MANAGER
        protected virtual void Update()
        {
            UpdateMovement();
            UpdateJump();
        }

        private void UpdateJump()
        {
            if (_jumpableComponent == null) return;

            if (Input.GetButtonDown(_jumpButton))
                _jumpableComponent.Jump();
        }

        private void UpdateMovement()
        {
            if (_movableComponent == null) return;

            Vector2 move = new Vector2(
                Input.GetAxis("Horizontal"),
                Input.GetAxis("Vertical"));

            Vector2 rotate = new Vector2(
                (_invertXAxis ? -1 : 1) * Input.GetAxis("Mouse X"),
                (_invertYAxis ? -1 : 1) * Input.GetAxis("Mouse Y"));

            _movableComponent.Move(move);
            _movableComponent.Rotate(rotate);
        }
#endif
    }
}