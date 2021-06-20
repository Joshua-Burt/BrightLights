using System;
using Celestials;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlanetHoverOverlay : MonoBehaviour {
    private Camera _cam;
    private Transform _transform;
    public GameObject image;
    private GameObject clone;
    private GameObject canvas;

    void Start() {
        _cam = Camera.main;
        _transform = transform;
        canvas = GameObject.Find("Canvas");
        clone = Instantiate(image, canvas.transform, true);
        clone.GetComponent<Image>().color = GetComponent<ColourSettings>().planetColour;
    }

    private void Update() {
        if(_cam is { } && clone) {
            Vector3 pos = _cam.WorldToScreenPoint(_transform.position);

            if(pos.x > 0 && pos.x < Screen.width && pos.y > 0 && pos.y < Screen.height) {
                if((_cam.transform.position - _transform.position).magnitude >
                   GetComponent<ShapeSettings>().planetRadius * 9) {
                    clone.SetActive(true);

                    Image img = clone.GetComponent<Image>();

                    var tempColor = img.color;
                    tempColor.a = Math.Abs(1000 / distance(pos.x, pos.y, Screen.width / 2, Screen.height / 2));
                    img.color = tempColor;

                    RectTransform rectTransform = clone.GetComponent<RectTransform>();
                    rectTransform.position = new Vector2(pos.x, pos.y);

                    var rectTransformLocalScale = rectTransform.localScale;
                    rectTransformLocalScale.x = DistanceAndDiameterToPixelSize() / 100;
                    rectTransformLocalScale.y = DistanceAndDiameterToPixelSize() / 100;

                    rectTransform.localScale = rectTransformLocalScale;
                }
            } else {
                clone.SetActive(false);
            }
        }
    }
    
    //Get the screen size of an object in pixels, given its distance and diameter.
    float DistanceAndDiameterToPixelSize(){
        float pixelSize = GetComponent<ShapeSettings>().planetRadius * Mathf.Rad2Deg * Screen.height 
                          / ((_transform.position - _cam.transform.position).magnitude * _cam.fieldOfView);
        return pixelSize;
    }
    
    private static Rect GUIRectWithObject(GameObject go) {
        Vector3 cen = go.GetComponent<Renderer>().bounds.center;
        Vector3 ext = go.GetComponent<Renderer>().bounds.extents;
        Vector2[] extentPoints = new Vector2[8] {
            HandleUtility.WorldToGUIPoint(new Vector3(cen.x-ext.x, cen.y-ext.y, cen.z-ext.z)),
            HandleUtility.WorldToGUIPoint(new Vector3(cen.x+ext.x, cen.y-ext.y, cen.z-ext.z)),
            HandleUtility.WorldToGUIPoint(new Vector3(cen.x-ext.x, cen.y-ext.y, cen.z+ext.z)),
            HandleUtility.WorldToGUIPoint(new Vector3(cen.x+ext.x, cen.y-ext.y, cen.z+ext.z)),
            HandleUtility.WorldToGUIPoint(new Vector3(cen.x-ext.x, cen.y+ext.y, cen.z-ext.z)),
            HandleUtility.WorldToGUIPoint(new Vector3(cen.x+ext.x, cen.y+ext.y, cen.z-ext.z)),
            HandleUtility.WorldToGUIPoint(new Vector3(cen.x-ext.x, cen.y+ext.y, cen.z+ext.z)),
            HandleUtility.WorldToGUIPoint(new Vector3(cen.x+ext.x, cen.y+ext.y, cen.z+ext.z))
        };
        Vector2 min = extentPoints[0];
        Vector2 max = extentPoints[0];
        foreach(Vector2 v in extentPoints) {
            min = Vector2.Min(min, v);
            max = Vector2.Max(max, v);
        }
        return new Rect(min.x, min.y, max.x-min.x, max.y-min.y);
    }


    private float distance(float x1, float y1, float x2, float y2) {
        return (x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2);
    }
}

