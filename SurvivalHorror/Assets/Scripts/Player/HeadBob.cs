using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBob : MonoBehaviour
{
    [SerializeField] private float _walkingBobbingSpeed = 10f;
    [SerializeField] private float _bobbingAmount = 0.08f;
    [SerializeField] private PlayerMovement _controller;

    float defaultPosY = 0;
    float timer = 0;

    void Start()
    {
        defaultPosY = transform.localPosition.y;
    }

    void Update()
    {
        if (Mathf.Abs(_controller.moveDirection.x) > 0.1f || Mathf.Abs(_controller.moveDirection.z) > 0.1f)
        {
            timer += Time.deltaTime * _walkingBobbingSpeed;
            transform.localPosition = new Vector3(transform.localPosition.x, 
                defaultPosY + Mathf.Sin(timer) * _bobbingAmount, transform.localPosition.z);
        }
        else
        {
            timer = 0;
            transform.localPosition = new Vector3(transform.localPosition.x,
                Mathf.Lerp(transform.localPosition.y, defaultPosY, Time.deltaTime * _walkingBobbingSpeed), transform.localPosition.z);
        }
    }
}
