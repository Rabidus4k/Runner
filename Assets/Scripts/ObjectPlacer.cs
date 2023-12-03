using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    [SerializeField] private float _scaleFactor = 4;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _moveTime = 0.4f;
    private DrawWithMouse _drawer;

    private void Awake()
    {
        _drawer = FindObjectOfType<DrawWithMouse>();
    }

    private void OnEnable()
    {
        _drawer.OnDrawPath += ReplaceObjects;
    }

    private void OnDisable()
    {
        _drawer.OnDrawPath -= ReplaceObjects;
    }

    private void ReplaceObjects(Vector3[] positions)
    {
        List<Character> characters = GetComponentsInChildren<Character>().ToList();

        int offsetIndex = 0;
        for (int i = 0; i < characters.Count; i++)
        {
            Vector3 objNewPosition = new Vector3(positions[offsetIndex].x, 0, positions[offsetIndex].y);
            objNewPosition *= _scaleFactor;
            objNewPosition += _offset;

            LeanTween.moveLocal(characters[i].gameObject, objNewPosition, _moveTime);
            offsetIndex+= positions.Length / characters.Count;
        }
    }
}
