using UnityEngine;

/// <summary>
/// Singleton, but you need to manually place one instance in the scene.
/// ATTENTION If you overwrite Awake or OnDestroy you must call the base class methods. 
/// </summary>
public class UKUnitySingletonManuallyCreated<T> : UKMonoBehaviour
	where T : Component
{
    private static T instance;
	public static T Instance
    {
        get 
        {
            if (Application.isEditor) return GameObject.FindObjectOfType<T>();
            return instance;
        }
    }

	protected virtual void Awake () {
        instance = this.GetComponent<T>();
	}

	protected virtual void OnDestroy() {
        instance = null;
	}
}