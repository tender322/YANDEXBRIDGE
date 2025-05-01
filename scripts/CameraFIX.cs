using UnityEngine;

public class CameraFIX : MonoBehaviour
{
    public enum TypeCamera{Landscape,Portrait,Square};
    public TypeCamera typeCameraGame = TypeCamera.Landscape;
    private float targetAspect = 16f / 9f; // Целевое соотношение 16:9
    private int lastScreenWidth;
    private int lastScreenHeight;

    void Start()
    {
        switch(typeCameraGame)
        {
            case TypeCamera.Landscape:
                targetAspect = 16f / 9f;
                break;
            case TypeCamera.Portrait:
                targetAspect = 9f / 16f;
                break;
            case TypeCamera.Square:
                targetAspect = 1f / 1f;
                break;
        }
        lastScreenWidth = Screen.width;
        lastScreenHeight = Screen.height;
        UpdateCamera();
    }

    void Update()
    {
        // Если экран изменился — пересчитываем
        if (Screen.width != lastScreenWidth || Screen.height != lastScreenHeight)
        {
            lastScreenWidth = Screen.width;
            lastScreenHeight = Screen.height;
            UpdateCamera();
        }
    }

    void UpdateCamera()
    {
        Camera cam = GetComponent<Camera>();
        float windowAspect = (float)Screen.width / Screen.height;
        float scaleHeight = windowAspect / targetAspect;

        if (scaleHeight < 1f)
        {
            Rect rect = new Rect(0, (1f - scaleHeight) / 2f, 1f, scaleHeight);
            cam.rect = rect;
        }
        else
        {
            float scaleWidth = 1f / scaleHeight;
            Rect rect = new Rect((1f - scaleWidth) / 2f, 0, scaleWidth, 1f);
            cam.rect = rect;
        }
    }

    void OnPreCull()
    {
        GL.Clear(true, true, Color.black); // Чёрные полосы
    }


}
