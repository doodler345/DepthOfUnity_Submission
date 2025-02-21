using UnityEngine;
using InterpolateExtentionMethods;

public class InterpolateExtensionMethodsTester : MonoBehaviour
{
    [Header("Interpolation of Cube Position & Rotation")]
    [SerializeField] private Transform destinationTransform;
    [SerializeField] private float travelDuration = 3;

    [Header("Interpolation of Float")]
    [SerializeField] private float testFloat = 0;
    [SerializeField] private float testFloatTargetValue = 10;
    [SerializeField] private float testFloatInterpolationDuration = 5;

    [Header("Interpolation of Vector3")]
    [SerializeField] private Vector3 testVector3 = Vector3.zero;
    [SerializeField] private Vector3 testVector3TargetValue = new Vector3(5,7,1);
    [SerializeField] private float testVector3InterpolationDuration = 2;

    [Header("Interpolation of Vector3")]
    [SerializeField] private Color testColor = Color.white;
    [SerializeField] private Color testColorTargetValue = Color.yellow;
    [SerializeField] private float testColorInterpolationDuration = 2;

    void Start()
    {
        StartCoroutine(transform.INTERPMoveTo(destinationTransform.position, travelDuration, EaseType.InOutSine, OnMoveToFinished));
        StartCoroutine(transform.INTERPRotateTo(destinationTransform.rotation, travelDuration, EaseType.Linear, OnRotateToFinished));

        StartCoroutine(testFloat.INTERPOverTime(testFloatTargetValue, testFloatInterpolationDuration, EaseType.Linear, value => testFloat = value, OnFloatOverTimeFinished));
        StartCoroutine(testVector3.INTERPOverTime(testVector3TargetValue, testVector3InterpolationDuration, EaseType.Linear, value => testVector3 = value, OnVector3OverTimeFinished));
        StartCoroutine(testColor.INTERPOverTime(testColorTargetValue, testColorInterpolationDuration, EaseType.OutSine, value => testColor = value, OnColorOverTimeFinished));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StopAllCoroutines();
        }
    }

    private void OnMoveToFinished()
    {
        Debug.Log("MoveTo Finished");
    }

    private void OnRotateToFinished()
    {
        Debug.Log("RotateTo Finished");
    }

    private void OnFloatOverTimeFinished()
    {
        Debug.Log("Float Interpolation Over Time Finished");
    }

    private void OnVector3OverTimeFinished()
    {
        Debug.Log("Vector3 Interpolation Over Time Finished");
    }

    private void OnColorOverTimeFinished()
    {
        Debug.Log("Color Interpolation Over Time Finished");
    }
}
