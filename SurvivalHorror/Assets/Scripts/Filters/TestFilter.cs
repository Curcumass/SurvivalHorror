using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFilter : MonoBehaviour
{
    [SerializeField] private Shader _shader;

    private Material _material;
    private bool _useFilter = true;

    private void Awake()
    {
        _material = new Material(_shader);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            _useFilter = !_useFilter;
        }
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if(_useFilter)
            Graphics.Blit(source, destination, _material);
        else
            Graphics.Blit(source, destination);
    }
}
