using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPad : Interactable
{
    [SerializeField] private Transform _door;

    private bool _doorOpen;

    private void Start()
    {
        // _door = GetComponent<GameObject>();
    }

    protected override void Interact()
    {
        _doorOpen = !_doorOpen;
        _door.GetComponent<Animator>().SetBool("isOpen", _doorOpen);
        Debug.Log("Interact with " + gameObject.name);
    }


}
