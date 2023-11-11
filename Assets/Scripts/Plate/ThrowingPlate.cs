using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ThrowingPlate : MonoBehaviour
{
    private PlayerAnimations playerAnimations;
    private RaycastHit hit;
    [SerializeField] private GameObject _platePrefab;
    [SerializeField] private float throwForce;

    private void Start()
    {
        _platePrefab = _platePrefab.gameObject;
        playerAnimations = gameObject.GetComponent<PlayerAnimations>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (InventoryUI.Instance.SelectedItem == "Plate")
                {
                    playerAnimations.ThrowPlateAnimation();
                    gameObject.transform.DOLookAt(hit.point, .5f);
                }
            }
        }
    }

    public void ThrowPlate()
    {
        string plateName = "Plate";
        _platePrefab.gameObject.SetActive(true);
        Rigidbody plateRigid = _platePrefab.GetComponent<Rigidbody>();
        plateRigid.constraints = RigidbodyConstraints.None;
        plateRigid.useGravity = true;
        Vector3 direction = hit.point - transform.position;
        plateRigid.velocity = direction * throwForce;
        ItemData item = ItemLoader.ConvertNameToItemData(plateName);
        InventoryUI.Instance.Inventory.RemoveItem(item);
    }
}
