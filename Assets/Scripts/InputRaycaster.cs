using UnityEngine;

public enum MouseButton
{
    Left = 0,
    Right = 1,
    Middle = 2
}

public class InputRaycaster : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;

    private void Update()
    {
        if (Input.GetMouseButtonDown((int)MouseButton.Left))
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.TryGetComponent<SplittableCube>(out SplittableCube cube))
                {
                    SplittableCube.RaiseCubeCliked(cube);
                }
            }
        }
    }
}
