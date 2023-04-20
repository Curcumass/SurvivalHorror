using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _layerMask;

    private PlayerUI _playerUI;

    private float _distance = 2f;

    private void Start()
    {
        _playerUI = GetComponent<PlayerUI>();
    }

    private void Update()
    {
        _playerUI.UpdateText(string.Empty);

        Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * _distance, Color.green);

        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, _distance, _layerMask))
        {
            if (hit.collider.GetComponent<Interactable>() != null)
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                _playerUI.UpdateText(interactable.promptMessage);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.BaseInteract();
                    _playerUI.UpdateText("");
                }
            }
        }
    }
}
