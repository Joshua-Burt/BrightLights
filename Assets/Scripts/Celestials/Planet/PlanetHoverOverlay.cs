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
    private Transform _reticle;
    private Transform _distance;
    private Text _distanceText;
    private Image _image;
    private RectTransform _imgTransform, _textTransform;
    private ShapeSettings _shapeSettings;

    void Start() {
        _shapeSettings = GetComponent<ShapeSettings>();
        _cam = Camera.main;
        _transform = transform;
        canvas = GameObject.Find("Canvas");
        
        clone = Instantiate(image, canvas.transform, true);
        clone.transform.SetSiblingIndex(0);

        _distance = clone.transform.GetChild(0);
        _reticle = clone.transform.GetChild(1);

        _image = _reticle.GetComponent<Image>();
        _imgTransform = _reticle.GetComponent<RectTransform>();
        _image.color = GetComponent<ColourSettings>().planetColour;
        
        _distanceText = _distance.GetComponent<Text>();
        _textTransform = _distance.GetComponent<RectTransform>();
        _distanceText.color = GetComponent<ColourSettings>().planetColour;
    }

    private void Update() {
        if(_cam is { } && clone) {
            Vector3 pos = _cam.WorldToScreenPoint(_transform.position);
            float distanceBetweenPlayerAndPlanet = (_cam.transform.position - _transform.position).magnitude;
            float planetRadius = _shapeSettings.planetRadius;
            
            if(pos.x > 0 && pos.x < Screen.width && pos.y > 0 && pos.y < Screen.height && distanceBetweenPlayerAndPlanet > planetRadius * 5) {
                clone.SetActive(true);

                float lookingFadeValue = Math.Abs(1000 / distance(pos.x, pos.y, Screen.width / 2f, Screen.height / 2f));
                float distFadeValue = distanceFadeValue(distanceBetweenPlayerAndPlanet);

                float fadingValue = Math.Min(lookingFadeValue, distFadeValue);

                var imgColour = _image.color;
                imgColour.a = fadingValue;
                _image.color = imgColour;
                
                var textColour = _distanceText.color;
                textColour.a = fadingValue;
                _distanceText.color = textColour;

                float apparentSize = DistanceAndDiameterToPixelSize();
                var imgLocalScale = _imgTransform.localScale;
                
                float imgSize =  Mathf.Clamp(apparentSize, 0.5f, 4f);
                imgLocalScale.x = imgSize / 2;
                imgLocalScale.y = imgSize / 2;

                _imgTransform.localScale = imgLocalScale;

                float textSize = 0.5f;
                var textLocalScale = _textTransform.localScale;
                textLocalScale.x = textSize; 
                textLocalScale.y = textSize; 
                _textTransform.localScale = textLocalScale;
                
                _imgTransform.position = new Vector2(pos.x, pos.y);
                _textTransform.position = new Vector2(_imgTransform.position.x + _imgTransform.rect.width + 30, pos.y);
            } else {

            clone.SetActive(false);
            }
            
            _distanceText.text = "Distance: " + Math.Round((_transform.position - _cam.transform.position).magnitude) + "km"; 
        }
    }
    
    //Get the screen size of an object in pixels, given its distance and diameter.
    float DistanceAndDiameterToPixelSize(){
        var pixelSize = GetComponent<ShapeSettings>().planetRadius * Mathf.Rad2Deg * Screen.height 
                        / ((_transform.position - _cam.transform.position).magnitude * _cam.fieldOfView);
        return pixelSize;
    }

    // Created using Desmos until I thought it looked good
    float distanceFadeValue(float x) {
        return (float) (1 / (1 + Math.Pow(Math.E,-0.005*x+25)));
    }

    private float distance(float x1, float y1, float x2, float y2) {
        return (x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2);
    }
}

