using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    private static List<InputProvider> InputComponenets = new List<InputProvider>();
    private static GameObject InputProviders;
    private static GameObject Camera;

    void Awake()
    {
        Camera = this.transform.Find("Camera").gameObject;
        InputProviders = this.transform.Find("InputProvider").gameObject;
        InputProviders.GetComponents<InputProvider>(InputComponenets);

    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public CameraControl GetCameraControl ()
    {
        return Camera.GetComponent<CameraControl>();
    }

    public GameObject GetCamera()
    {
        return Camera;
    }

    public static List<InputProvider> GetInputProviders()
    {
        return InputComponenets;
    }

    public InputProvider GetInputProvider<T>()
    {
        return InputProviders.GetComponent<InputProvider>();
    }
}
