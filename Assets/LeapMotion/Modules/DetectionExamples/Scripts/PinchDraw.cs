/******************************************************************************
 * Copyright (C) Leap Motion, Inc. 2011-2017.                                 *
 * Leap Motion proprietary and  confidential.                                 *
 *                                                                            *
 * Use subject to the terms of the Leap Motion SDK Agreement available at     *
 * https://developer.leapmotion.com/sdk_agreement, or another agreement       *
 * between Leap Motion and you, your company or other organization.           *
 ******************************************************************************/

using System;
using UnityEngine;
using System.Collections.Generic;
using Hover.Core.Renderers.Shapes.Arc;
using Hover.Core.Items.Types;

namespace Leap.Unity.DetectionExamples {

  public class PinchDraw : MonoBehaviour {

    [Tooltip("Each pinch detector can draw one line at a time.")]
    [SerializeField]
    private PinchDetector[] _pinchDetectors;

    [SerializeField]
    private Material _material;

    [SerializeField]
    private Color _drawColor = Color.white;

    [SerializeField]
    private float _smoothingDelay = 0.01f;

    [SerializeField]
    private float _drawRadius = 0.002f;

    [SerializeField]
    private int _drawResolution = 8;

    [SerializeField]
    private float _minSegmentLength = 0.005f;

    public string _selected;
    public int isDrag = 0;
    public float curd;
    public float curt;
    private int pHand = 0;

    private float dist;
    public static int _line;
    public int Line {
      get {
        return _line;
      }
      set {
        _line = value;
      }
    }

    //Pile des objets pouvant être cancel
    public static List<int> zTab;
    public List<int> Ztab {
      get {
        return zTab;
      }
      set {
        zTab = value;
      }
    }
    //Pile des objets pouvant être décancel
    public static List<int> yTab;
    public List<int> Ytab {
      get {
        return yTab;
      }
      set {
        yTab = value;
      }
    }
    // Permet d'acceder à la valeur du slider
    public GameObject arcValue;
    // Permet d'acceder à la valeur du slider
    public static GameObject arcValueU;

    private int state = 2;

    public int State {
      get {
        return state;
      }
      set {
        state = value;
      }
    }


    private DrawState[] _drawStates;

    public Color DrawColor {
      get {
        return _drawColor;
      }
      set {
        _drawColor = value;
      }
    }

    public float DrawRadius {
      get {
        return _drawRadius;
      }
      set {
        _drawRadius = value;
      }
    }

    void OnValidate() {
      _drawRadius = Mathf.Max(0, _drawRadius);
      _drawResolution = Mathf.Clamp(_drawResolution, 3, 24);
      _minSegmentLength = Mathf.Max(0, _minSegmentLength);
    }

    void Awake() {
      if (_pinchDetectors.Length == 0) {
        Debug.LogWarning("No pinch detectors were specified!  PinchDraw can not draw any lines without PinchDetectors.");
      }
    }

    void Start() {
      _drawStates = new DrawState[_pinchDetectors.Length];
      for (int i = 0; i < _pinchDetectors.Length; i++) {
        _drawStates[i] = new DrawState(this);
      }

      //nos initialisation
      _line = 0;
      zTab = new List<int>();
      yTab = new List<int>();
      arcValueU = arcValue;
      arcValueU.GetComponent<HoverItemDataSlider>().Value = 0.1f;
      
    }

    public void removeZ() {
      if (zTab.Count >= 1){
        string s = "line" + zTab[zTab.Count - 1];
        GameObject obj = GameObject.Find(s);
        MeshRenderer m = obj.GetComponent<MeshRenderer>();
        m.enabled = false;
        yTab.Add(zTab[zTab.Count - 1]);
        zTab.RemoveAt(zTab.Count - 1);
        
      }
    }

    public void removeY() {
      if (yTab.Count >= 1){
        string s = "line" + yTab[yTab.Count - 1];
        GameObject obj = GameObject.Find(s);
        MeshRenderer m = obj.GetComponent<MeshRenderer>();
        m.enabled = true;
        zTab.Add(yTab[yTab.Count - 1]);
        yTab.RemoveAt(yTab.Count - 1);
      }
    }

    void Update() {
      if (state == 1)
        drawTrail();
      if (state == 2 || state == 3)
        draw3DObject();
      if (state == 5)
        dragObject();
      
   }

    void draw3DObject (){
      for (int i = 0; i < _pinchDetectors.Length; i++) {
        var detector = _pinchDetectors[i];
        var drawState = _drawStates[i];
        if (detector.DidStartHold) {
          if (state == 2){  // Si state vaut 2 on dessine des cubes
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.GetComponent<BoxCollider>().isTrigger = true;
            cube.tag = "3dObj";
            cube.name = "line" + _line;

            
            cube.transform.position = GameObject.Find("RightIndex").transform.position;
            cube.transform.localScale = new Vector3(0.05F, 0.05F, 0.05F);
            zTab.Add(_line);
            _line ++;
            
          }
          if (state == 3){ // Si state vaut 3 on dessine des Spheres
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            BoxCollider boxCollider = sphere.AddComponent<BoxCollider>();
            boxCollider.isTrigger = true;
            sphere.tag = "3dObj";
            sphere.name = "line" + _line;
            

            sphere.transform.position = GameObject.Find("RightIndex").transform.position;
            sphere.transform.localScale = new Vector3(0.05F, 0.05F, 0.05F);
            zTab.Add(_line);
            _line ++;
            
            
          }
        }
      }
    }

    void drawTrail (){
      for (int i = 0; i < _pinchDetectors.Length; i++) {
        var detector = _pinchDetectors[i];
        var drawState = _drawStates[i];
        
        if (detector.DidStartHold) {
          drawState.BeginNewLine();
        }

        if (detector.DidRelease) {
          drawState.FinishLine();
        }

        if (detector.IsHolding) {
          drawState.UpdateLine(detector.Position);
        }
      }    
    }

    double sqr(double val)
    {
      return val * val;
    }
    
    float distance (GameObject p1, GameObject p2)
    {
      double p1x = (double)p1.transform.position.x;
      double p1y = (double)p1.transform.position.y;
      double p1z = (double)p1.transform.position.z;
      double p2x = (double)p2.transform.position.x;
      double p2y = (double)p2.transform.position.y;
      double p2z = (double)p2.transform.position.z;
      
      return (float)Math.Sqrt(sqr(p1x-p2x)+sqr(p1y-p2y)+sqr(p1z-p2z));
      
    }
    
    void dragObject (){
      for (int i = 0; i < _pinchDetectors.Length; i++) {
        var detector = _pinchDetectors[i];
        var drawState = _drawStates[i];
        
        if (detector.DidStartHold) {
          isDrag = 1;
          pHand++;
          Debug.Log(pHand); 
          if (pHand == 2)
          {
            GameObject a = GameObject.Find("PinchDetector_L");
            GameObject b = GameObject.Find("PinchDetector_R");
            curd = distance(a, b);
            
          }
            
        }

        if (detector.DidRelease) {
          isDrag = 0;
          _selected = null;
          pHand--;
        }

        if (detector.IsHolding) {
          if (GameObject.Find(_selected) != null){
            GameObject obj = GameObject.Find(_selected);
            curt = obj.transform.localScale.x;
            obj.transform.position = GameObject.Find("RightIndex").transform.position;
            obj.transform.rotation = GameObject.Find("RightIndex").transform.rotation;
            if (pHand == 2)
            {
              GameObject a = GameObject.Find("PinchDetector_L");
              GameObject b = GameObject.Find("PinchDetector_R");
              dist = distance(a, b);
              obj.transform.localScale = new Vector3((curd+dist)/5, (curd+dist)/5, (curd+dist)/5); // curt taille initila du 3D / curd distance initial au clique
            
            }
          }
        }
      }    
    }

    private class DrawState {
      
      private List<Vector3> _vertices = new List<Vector3>();
      private List<int> _tris = new List<int>();
      private List<Vector2> _uvs = new List<Vector2>();
      private List<Color> _colors = new List<Color>();

      private PinchDraw _parent;

      private int _rings = 0;

      private Vector3 _prevRing0 = Vector3.zero;
      private Vector3 _prevRing1 = Vector3.zero;

      private Vector3 _prevNormal0 = Vector3.zero;
      private Mesh _mesh;
      private SmoothedVector3 _smoothedPosition;



      public DrawState(PinchDraw parent) {
        

        _parent = parent;

        _smoothedPosition = new SmoothedVector3();
        _smoothedPosition.delay = parent._smoothingDelay;
        _smoothedPosition.reset = true;
      }

      public GameObject BeginNewLine() {
        _rings = 0;
        _vertices.Clear();
        _tris.Clear();
        _uvs.Clear();
        _colors.Clear();
        _smoothedPosition.reset = true;

        string s = "line"+_line;
        zTab.Add(_line);
        _line ++;

        GameObject lineObj = new GameObject(s);
        _mesh = new Mesh();
        _mesh.name = "Line Mesh";
        _mesh.MarkDynamic();

        
        lineObj.transform.position = Vector3.zero;
        lineObj.transform.rotation = Quaternion.identity;
        lineObj.transform.localScale = Vector3.one;
        lineObj.AddComponent<MeshFilter>().mesh = _mesh;
        lineObj.AddComponent<MeshRenderer>().sharedMaterial = _parent._material;
        



        return lineObj;
      }


      public void UpdateLine(Vector3 position) {
        _smoothedPosition.Update(position, Time.deltaTime);

        bool shouldAdd = false;

        shouldAdd |= _vertices.Count == 0;
        shouldAdd |= Vector3.Distance(_prevRing0, _smoothedPosition.value) >= _parent._minSegmentLength;

        if (shouldAdd) {
          addRing(_smoothedPosition.value);
          updateMesh();
        }
      }

      public void FinishLine() {
        _mesh.UploadMeshData(true);
      }

      private void updateMesh() {
        _mesh.SetVertices(_vertices);
        _mesh.SetColors(_colors);
        _mesh.SetUVs(0, _uvs);
        _mesh.SetIndices(_tris.ToArray(), MeshTopology.Triangles, 0);
        _mesh.RecalculateBounds();
        _mesh.RecalculateNormals();
      }

      private void addRing(Vector3 ringPosition){
        _rings++;
        Debug.Log(arcValueU.GetComponent<HoverItemDataSlider>().Value*10); //Debug la valeur selectionné dans l'arc value

        if (_rings == 1) {
          addVertexRing();
          addVertexRing();
          addTriSegment();
        }

        addVertexRing();
        addTriSegment();

        Vector3 ringNormal = Vector3.zero;
        if (_rings == 2) {
          Vector3 direction = ringPosition - _prevRing0;
          float angleToUp = Vector3.Angle(direction, Vector3.up);

          if (angleToUp < 10 || angleToUp > 170) {
            ringNormal = Vector3.Cross(direction, Vector3.right);
          } else {
            ringNormal = Vector3.Cross(direction, Vector3.up);
          }

          ringNormal = ringNormal.normalized;

          _prevNormal0 = ringNormal;
        } else if (_rings > 2) {
          Vector3 prevPerp = Vector3.Cross(_prevRing0 - _prevRing1, _prevNormal0);
          ringNormal = Vector3.Cross(prevPerp, ringPosition - _prevRing0).normalized;
        }

        if (_rings == 2) {
          updateRingVerts(0,
                          _prevRing0,
                          ringPosition - _prevRing1,
                          _prevNormal0,
                          0);
        }

        if (_rings >= 2) {
          updateRingVerts(_vertices.Count - _parent._drawResolution,
                          ringPosition,
                          ringPosition - _prevRing0,
                          ringNormal,
                          0);

          updateRingVerts(_vertices.Count - _parent._drawResolution * 2,
                          ringPosition,
                          ringPosition - _prevRing0,
                          ringNormal,
                          arcValueU.GetComponent<HoverItemDataSlider>().Value*10); //On donne la valeur selectionné dans l'arc value

          updateRingVerts(_vertices.Count - _parent._drawResolution * 3,
                          _prevRing0,
                          ringPosition - _prevRing1,
                          _prevNormal0,
                          arcValueU.GetComponent<HoverItemDataSlider>().Value*10); //On donne la valeur selectionné dans l'arc value
        }

        _prevRing1 = _prevRing0;
        _prevRing0 = ringPosition;

        _prevNormal0 = ringNormal;

        //COLOR//
        
        _parent.DrawColor = GameObject.Find("Picker").GetComponent<ColorPicker>().CurrentColor;
        //_parent.DrawColor = Color.green;
      }

      private void addVertexRing() {
        for (int i = 0; i < _parent._drawResolution; i++) {
          _vertices.Add(Vector3.zero);  //Dummy vertex, is updated later
          _uvs.Add(new Vector2(i / (_parent._drawResolution - 1.0f), 0));
          _colors.Add(_parent._drawColor);
        }
      }

      //Connects the most recently added vertex ring to the one before it
      private void addTriSegment() {
        for (int i = 0; i < _parent._drawResolution; i++) {
          int i0 = _vertices.Count - 1 - i;
          int i1 = _vertices.Count - 1 - ((i + 1) % _parent._drawResolution);

          _tris.Add(i0);
          _tris.Add(i1 - _parent._drawResolution);
          _tris.Add(i0 - _parent._drawResolution);

          _tris.Add(i0);
          _tris.Add(i1);
          _tris.Add(i1 - _parent._drawResolution);
        }
      }

      private void updateRingVerts(int offset, Vector3 ringPosition, Vector3 direction, Vector3 normal, float radiusScale) {
        direction = direction.normalized;
        normal = normal.normalized;

        for (int i = 0; i < _parent._drawResolution; i++) {
          float angle = 360.0f * (i / (float)(_parent._drawResolution));
          Quaternion rotator = Quaternion.AngleAxis(angle, direction);
          Vector3 ringSpoke = rotator * normal * _parent._drawRadius * radiusScale;
          _vertices[offset + i] = ringPosition + ringSpoke;
        }
      }
    }
  }
}