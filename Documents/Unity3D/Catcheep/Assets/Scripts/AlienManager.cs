﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AlienManager : MonoBehaviour
{
    public GameObject alienShip;
    public GameObject rightButton;
    public GameObject leftButton;

    private GameObject spaceShip;
    private Vector2 edgeOfScreen;

    // Use this for initialization
    void Start()
    {
        edgeOfScreen = new Vector2(Screen.width, Screen.height);
        StartCoroutine(alienSpawner());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator alienSpawner()
    {
        deactivatingButtons();

        spaceShip = Instantiate(alienShip, alienShip.transform.localPosition, alienShip.transform.rotation);
        spaceShip.transform.SetParent(GameObject.Find("Canvas").transform, false);

        Vector3 destination = new Vector3(alienShip.transform.localPosition.x, -edgeOfScreen.y * 0.05f, 0f);
        do
        {
            spaceShip.transform.localPosition =
                Vector3.Lerp(spaceShip.transform.localPosition, destination, 2.5f * Time.deltaTime);
            yield return new WaitForSeconds(0.02f);
        } while ((int) spaceShip.transform.localPosition.y > 0f);
        
        activatingButtons();

        StartCoroutine(shipGoingRight());
    }

    IEnumerator shipLeaving()
    {
        deactivatingButtons();

        Vector3 destination = new Vector3(alienShip.transform.localPosition.x, edgeOfScreen.y, 0f);
        do
        {
            spaceShip.transform.localPosition =
                Vector3.Lerp(spaceShip.transform.localPosition, destination, 2.5f * Time.deltaTime);
            yield return new WaitForSeconds(0.02f);
        } while (spaceShip.transform.localPosition.y < destination.y);

        Destroy(spaceShip.gameObject);
        activatingButtons();
    }

    public void shipGoingRightButtonClicked()
    {
        StartCoroutine(shipGoingRight());
    }
    IEnumerator shipGoingRight()
    {
        deactivatingButtons();
        //spaceShip.transform.localRotation = new Quaternion(spaceShip.transform.localRotation.x, spaceShip.transform.rotation.y, -5, spaceShip.transform.rotation.w);
        Vector3 destination = new Vector3(
            edgeOfScreen.x - alienShip.GetComponentInChildren<Image>().sprite.bounds.size.x,
            spaceShip.transform.localPosition.y,
            spaceShip.transform.localPosition.z);
        do
        {
            spaceShip.transform.localPosition = new Vector3(spaceShip.transform.localPosition.x + 10,
                spaceShip.transform.localPosition.y, spaceShip.transform.localPosition.z);
            yield return new WaitForSeconds(0.01f);
        } while ((int)spaceShip.transform.position.x < (int)destination.x * 1.05f);

        activatingButtons();
    }

    public void shipGoingLeftButtonClicked()
    {
        StartCoroutine(shipGoingLeft());
    }
    IEnumerator shipGoingLeft()
    {
        deactivatingButtons();
        //spaceShip.transform.localRotation = new Quaternion(spaceShip.transform.localRotation.x, spaceShip.transform.rotation.y, -5, spaceShip.transform.rotation.w);
        Vector3 destination = new Vector3(
           -edgeOfScreen.x - alienShip.GetComponentInChildren<Image>().sprite.bounds.size.x,
            spaceShip.transform.localPosition.y,
            spaceShip.transform.localPosition.z);
        do
        {
            spaceShip.transform.localPosition = new Vector3(spaceShip.transform.localPosition.x - 10,
                spaceShip.transform.localPosition.y, spaceShip.transform.localPosition.z);
            yield return new WaitForSeconds(0.01f);
        } while ((int) spaceShip.transform.position.x > (int) destination.x * 1.05f);
        
        activatingButtons();
    }

    void activatingButtons()
    {
        rightButton.GetComponent<Button>().enabled = true;
        leftButton.GetComponent<Button>().enabled = true;
    }
    void deactivatingButtons()
    {
        rightButton.GetComponent<Button>().enabled = false;
        leftButton.GetComponent<Button>().enabled = false;
    }


    public void startMenu()
    {
        SceneManager.LoadScene(1);
    }
}