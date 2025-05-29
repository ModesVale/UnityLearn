using UnityEngine;

public class InputRaycaster : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;

    private void Update()
    {
        if (Input.GetMouseButtonDown((int)MouseButton.LeftMouse))
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.TryGetComponent<SplittableCube>(out var cube))
                {
                    cube.HandleClick();
                }
            }
        }
    }
}
