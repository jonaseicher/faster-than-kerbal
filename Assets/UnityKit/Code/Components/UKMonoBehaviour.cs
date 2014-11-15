using UnityEngine;
using System.Collections;

public class UKMonoBehaviour : MonoBehaviour 
{
    private Transform cachedTransform;
    public Transform CachedTransform
    {
        get
        {
            if (cachedTransform == null) cachedTransform = GetComponent<Transform>();
            return cachedTransform;
        }
    }

#if UNITY_4_6
    private RectTransform cachedRectTransform;
    public RectTransform CachedRectTransform
    {
        get
        {
            if (cachedRectTransform == null) cachedRectTransform = GetComponent<RectTransform>();
            return cachedRectTransform;
        }
    }
#endif
}
