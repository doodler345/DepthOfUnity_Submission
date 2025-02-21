using System;
using System.Collections;
using UnityEngine;

public static class InterpolateExtensionMethods
{ 
    public static IEnumerator INTERPMoveTo(this Transform transform, Vector3 targetValue, float duration, EaseType easeType, Action finishedCallback = null)
    {
        AnimationCurve animationCurve = InterpolateEase.Instance.GetEase(easeType);

        Vector3 initValue = transform.position;
        float timer = 0;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = animationCurve.Evaluate((timer / duration));
            transform.position = Vector3.Lerp(initValue, targetValue, t);
            yield return null;
        }

        finishedCallback?.Invoke();
    }

    public static IEnumerator INTERPRotateTo(this Transform transform, Quaternion targetValue, float duration, EaseType easeType, Action finishedCallback = null)
    {
        AnimationCurve animationCurve = InterpolateEase.Instance.GetEase(easeType);

        Quaternion initValue = transform.rotation;
        float timer = 0;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = animationCurve.Evaluate((timer / duration));
            transform.rotation = Quaternion.Lerp(initValue, targetValue, t);
            yield return null;
        }

        finishedCallback?.Invoke();
    }

    public static IEnumerator INTERPOverTime(this float instance, float targetValue, float duration, EaseType easeType, Action<float> onUpdate, Action finishedCallback = null)
    {
        AnimationCurve animationCurve = InterpolateEase.Instance.GetEase(easeType);

        float startValue = instance;
        float timer = 0;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = animationCurve.Evaluate((timer / duration));
            instance = Mathf.Lerp(startValue, targetValue, t);

            onUpdate?.Invoke(instance);

            yield return null;
        }

        finishedCallback?.Invoke();
    }

    public static IEnumerator INTERPOverTime(this Vector3 instance, Vector3 targetValue, float duration, EaseType easeType, Action<Vector3> onUpdate, Action finishedCallback = null)
    {
        AnimationCurve animationCurve = InterpolateEase.Instance.GetEase(easeType);

        Vector3 startValue = instance;
        float timer = 0;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = animationCurve.Evaluate((timer / duration));
            instance = Vector3.Lerp(startValue, targetValue, t);

            onUpdate?.Invoke(instance);

            yield return null;
        }

        finishedCallback?.Invoke();
    }

    public static IEnumerator INTERPOverTime(this Color instance, Color targetValue, float duration, EaseType easeType, Action<Color> onUpdate, Action finishedCallback = null)
    {
        AnimationCurve animationCurve = InterpolateEase.Instance.GetEase(easeType);

        Color startValue = instance;
        float timer = 0;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = animationCurve.Evaluate((timer / duration));
            instance = Color.Lerp(startValue, targetValue, t);

            onUpdate?.Invoke(instance);

            yield return null;
        }

        finishedCallback?.Invoke();
    }
}
