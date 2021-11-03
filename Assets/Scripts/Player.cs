using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField] LayerMask floor;
    [SerializeField] float xOffset = 4.5f;
    [SerializeField] float zMinOffset = -6f;
    [SerializeField] float zMaxOffset = 5f;
    private Camera _camera;
    private BallSpawner _ballSpawner;

    private bool _isStopped;

    public event Action PlayerUnTouched;
    public event Action PlayerTouched;

    private void Awake()
    {
        _camera = Camera.main;
        _ballSpawner = FindObjectOfType<BallSpawner>();
    }

    private void OnMouseUp()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && !_isStopped)
        {
            PlayerUnTouched?.Invoke();
            StartCoroutine(IsStopped());   
        }
    }

    private void OnMouseDrag()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && !_isStopped)
        {
            PlayerTouched?.Invoke();
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, 500, floor))
            {
                transform.position = new Vector3(Mathf.Clamp(raycastHit.point.x, -xOffset, xOffset), 0.5f, Mathf.Clamp(raycastHit.point.z, zMinOffset, zMaxOffset));
            }
        }
    }

    private IEnumerator IsStopped()
    {
        _isStopped = true;
        yield return new WaitForSeconds(1);
        _isStopped = false;
    }
}
