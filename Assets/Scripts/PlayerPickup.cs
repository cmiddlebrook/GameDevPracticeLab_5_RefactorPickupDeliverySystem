using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPickup : MonoBehaviour {


    public Transform hold;
    public Transform carry;
    public Transform canObj; public Transform ui;
    public BoxCollider col;

    public TextMeshProUGUI bxDeliverui;


    private void Update() {
        canObj = null;

        ui.gameObject.SetActive(false);
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit r, 3f)) {
            if (r.collider.name == "cardboard_box_01_2k" ||
                r.collider.name == "cardboard_box_01_2k (1)" ||
                r.collider.name == "cardboard_box_01_2k (2)" ||
                r.collider.name == "CardboardBox" ||
                r.collider.name == "CardboardBox (1)" ||
                r.collider.name == "CardboardBox (2)") {
                // It's a delivery box
                canObj = r.transform;
                ui.gameObject.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            if (carry == null) {
                // Not carrying anything, pick up
                if (canObj != null) {
                    canObj.GetComponent<Rigidbody>().isKinematic = true;
                    canObj.parent = hold;
                    canObj.localPosition = Vector3.zero;
                    carry = canObj;
                }
            } else {
                // Carrying something, drop it
                carry.GetComponent<Rigidbody>().isKinematic = false;
                carry.parent = null;
                carry = null;

                Collider[] arr = 
                    Physics.OverlapBox(col.transform.position + col.center, col.size * .5f);
                int previousBoxesDelivered = boxes;
                boxes = 0;
                foreach (Collider c in arr) {
                    if (c.CompareTag("Box")) {
                        boxes++;
                    }
                }

                if (previousBoxesDelivered != boxes) {
                    Instantiate(
                        pop, 
                        transform.position + transform.forward * 2f, 
                        Quaternion.identity);
                }

                bxDeliverui.text = "Boxes Delivered: " + boxes;
            }
        }
    }

    public int boxes;
    public Transform pop;

}