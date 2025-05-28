using UnityEngine;

public class InputRaycaster : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
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
