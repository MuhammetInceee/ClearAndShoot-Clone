using System;
using System.Collections;
using UnityEngine;

public static class MoveExtension
{
    public static IEnumerator TweenMove(this Transform transform, Vector3 endValue, float duration)
    {
        var startPosition = transform.position;
        var elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            var t = Mathf.Clamp01(elapsedTime / duration);
            transform.position = Vector3.Lerp(startPosition, endValue, t);
            yield return null;
        }

        transform.position = endValue;
    }
    
    public static IEnumerator TweenMove(this Transform transform, Vector3 endPos, Vector3 eulerAngles,float duration)
    {
        var startPosition = transform.position;
        var startRotation = transform.eulerAngles;
        var elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            var t = Mathf.Clamp01(elapsedTime / duration);
            transform.position = Vector3.Lerp(startPosition, endPos, t);
            transform.eulerAngles = Vector3.Lerp(startRotation, eulerAngles, t);
            yield return null;
        }

        transform.position = endPos;
        transform.eulerAngles = eulerAngles;
    }
    
    public static IEnumerator TweenMoveRelative(this Transform transform, Vector3 endValue, float duration)
    {
        var startPosition = transform.position;
        var elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            var t = Mathf.Clamp01(elapsedTime / duration);
            transform.position = startPosition + (endValue * t);
            yield return null;
        }

        transform.position = startPosition + endValue;
    }
    
    public static IEnumerator TweenMove(this Transform transform, Vector3 endValue, float duration, Action onComplete)
    {
        var startPosition = transform.position;
        var elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            var t = Mathf.Clamp01(elapsedTime / duration);
            transform.position = Vector3.Lerp(startPosition, endValue, t);
            yield return null;
        }

        transform.position = endValue;
        
        onComplete.Invoke();
    }
    
    public static IEnumerator TweenMove(this Transform transform, Vector3 endPos, Vector3 eulerAngles, float duration, Action onComplete)
    {
        var startPosition = transform.position;
        var startRotation = transform.eulerAngles;
        var elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            var t = Mathf.Clamp01(elapsedTime / duration);
            transform.position = Vector3.Lerp(startPosition, endPos, t);
            transform.eulerAngles = Vector3.Lerp(startRotation, eulerAngles, t);
            yield return null;
        }

        transform.position = endPos;
        transform.eulerAngles = eulerAngles;
        
        onComplete.Invoke();
    }
    
    public static IEnumerator TweenMoveRelative(this Transform transform, Vector3 endValue, float duration, Action onComplete)
    {
        var startPosition = transform.position;
        var elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            var t = Mathf.Clamp01(elapsedTime / duration);
            transform.position = startPosition + (endValue * t);
            yield return null;
        }

        transform.position = startPosition + endValue;
        
        onComplete.Invoke();
    }
    
    public static IEnumerator TweenLocalMove(this Transform transform, Vector3 endValue, float duration)
    {
        var startPosition = transform.localPosition;
        var elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            var t = Mathf.Clamp01(elapsedTime / duration);
            transform.localPosition = Vector3.Lerp(startPosition, endValue, t);
            yield return null;
        }

        transform.localPosition = endValue;
    }
    
    public static IEnumerator TweenLocalMove(this Transform transform, Vector3 endPos, Vector3 eulerAngles,float duration)
    {
        var startPosition = transform.localPosition;
        var startRotation = transform.localEulerAngles;
        var elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            var t = Mathf.Clamp01(elapsedTime / duration);
            transform.localPosition = Vector3.Lerp(startPosition, endPos, t);
            transform.localEulerAngles = Vector3.Lerp(startRotation, eulerAngles, t);
            yield return null;
        }

        transform.position = endPos;
        transform.eulerAngles = eulerAngles;
    }
    
    public static IEnumerator TweenLocalMoveRelative(this Transform transform, Vector3 endValue, float duration)
    {
        var startPosition = transform.localPosition;
        var elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            var t = Mathf.Clamp01(elapsedTime / duration);
            transform.localPosition = startPosition + (endValue * t);
            yield return null;
        }

        transform.position = startPosition + endValue;
    }
    
    public static IEnumerator TweenLocalMove(this Transform transform, Vector3 endValue, float duration, Action onComplete)
    {
        var startPosition = transform.localPosition;
        var elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            var t = Mathf.Clamp01(elapsedTime / duration);
            transform.localPosition = Vector3.Lerp(startPosition, endValue, t);
            yield return null;
        }

        transform.position = endValue;
        onComplete.Invoke();
    }
    
    public static IEnumerator TweenLocalMove(this Transform transform, Vector3 endPos, Vector3 eulerAngles, float duration, Action onComplete)
    {
        var startPosition = transform.localPosition;
        var startRotation = transform.localEulerAngles;
        var elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            var t = Mathf.Clamp01(elapsedTime / duration);
            transform.localPosition = Vector3.Lerp(startPosition, endPos, t);
            transform.localEulerAngles = Vector3.Lerp(startRotation, eulerAngles, t);
            yield return null;
        }

        transform.localPosition = endPos;
        transform.localEulerAngles = eulerAngles;
        
        onComplete.Invoke();
    }
    
    public static IEnumerator TweenLocalMoveRelative(this Transform transform, Vector3 endValue, float duration, Action onComplete)
    {
        var startPosition = transform.localPosition;
        var elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            var t = Mathf.Clamp01(elapsedTime / duration);
            transform.position = startPosition + (endValue * t);
            yield return null;
        }

        transform.localPosition = startPosition + endValue;
        
        onComplete.Invoke();
    }
}
