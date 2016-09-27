﻿using UnityEngine;
using System.Collections;

public class CarriablesDrag : MonoBehaviour {

  Vector3 dist;
  float posX;
  float posY;
  StackingList stackingList;
  public Transform MiddleofBike;
  public float LerpSpeed = 2;

  [Range (0.1f, 2.0f)]
  public float translateSpeed = 0.1f;

  protected Vector3 initPosition;
  protected Quaternion initRotation;
  private Vector3 initScreenPosition;

  public float heightOfObject;

  void OnEnable(){
    stackingList = GameObject.FindGameObjectWithTag ("CarriableDetector").GetComponent<StackingList> ();
  }

  void Start() {
    initPosition = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
    initRotation = transform.rotation;
  }

  void OnMouseDown() {
    dist = Camera.main.WorldToScreenPoint (transform.position);
    posX = Input.mousePosition.x - dist.x;
    posY = Input.mousePosition.y - dist.y;
  }

  void OnMouseDrag(){
    Vector3 curPos= new Vector3 (Input.mousePosition.x - posX, Input.mousePosition.y - posY, dist.z);
    Vector3 worldPos =  Camera.main.ScreenToWorldPoint (curPos);
    transform.position = new Vector3(worldPos.x, worldPos.y, initPosition.z);
  }

  public void Sort(){
    StopAllCoroutines();
    StartCoroutine (MoveToFinalPosition (this.transform));
    stackingList.currentHeight += heightOfObject;
  }

  void OnMouseUp() {
    RaycastHit[] testhit;
    Vector3 fwd = transform.TransformDirection (Vector3.forward);

    testhit = Physics.RaycastAll (transform.position, Vector3.forward, 100f);

    if (testhit.Length > 0 && stackingList.CollectedCarriables.Count < stackingList.maxAmountOfCarriables) {
      bool hitted = false;

      foreach (var hit in testhit) {
        if (hit.collider.gameObject.tag == "CarriableDetector") {
          hitted = true;
          StartCoroutine (MoveToFinalPosition(this.transform));

          if (stackingList != null) {
            stackingList.addObject (this.gameObject, heightOfObject);
          }
          break;
        }   
      }

      if (!hitted) {
        MoveToInitialPosition ();
      }
    } else {
      MoveToInitialPosition ();
    }
  }

  void MoveToInitialPosition() {
    StartCoroutine (MoveToInitialPosition(this.transform));
    if (stackingList != null) {
      stackingList.removeObject (this.gameObject, heightOfObject);
    }
  }

  IEnumerator MoveToFinalPosition(Transform transThis) {
    if (transThis != null) {
      this.GetComponent<BoxCollider> ().enabled = false;

      Vector3 newPosition = MiddleofBike.position + new Vector3 (0, stackingList.currentHeight, 0);
      float distance = Vector3.Distance (transThis.position, newPosition);

      while (distance > 0.1f) {
        transThis.position = Vector3.MoveTowards (transThis.position, newPosition, Time.deltaTime * 10);
        transThis.rotation = Quaternion.RotateTowards (transThis.rotation, MiddleofBike.rotation, Time.deltaTime * 1000);
        distance = Vector3.Distance (transThis.position, newPosition);
        yield return null;
      }

      this.GetComponent<BoxCollider> ().enabled = true;
      transThis.position = newPosition;
    }
  }

  IEnumerator MoveToInitialPosition(Transform transThis) {
    if (transThis != null) {
      this.GetComponent<BoxCollider> ().enabled = false;
      float distance = Vector3.Distance (transThis.position, initPosition);
      while (distance > 0.1f) {
        transThis.position = Vector3.MoveTowards (transThis.position, initPosition, Time.deltaTime * 10);
        transThis.rotation = Quaternion.RotateTowards (transThis.rotation, initRotation, Time.deltaTime * 500);
        distance = Vector3.Distance (transThis.position, initPosition);
        yield return null;
      }
      this.GetComponent<BoxCollider> ().enabled = true;
      transThis.position = initPosition;
    }
  }
}
