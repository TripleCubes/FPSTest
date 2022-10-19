using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class game : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    private Transform cam;
    private GameObject cube;
    private TMPro.TextMeshProUGUI text;

    private int loop = 0;
    private float delay = 3;

    private List<GameObject> cubeList = new List<GameObject>();

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        cam = transform.Find("Main Camera");
        cube = GameObject.Find("Cube");
        text = GameObject.Find("Text").GetComponent<TMPro.TextMeshProUGUI>();
        text.gameObject.SetActive(false);
        
        loop = (int)(Time.time / delay);
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (delay * loop < Time.time)
        {
            GameObject newCube = Instantiate(cube);
            newCube.transform.position = new Vector3(Random.Range(-100, 100), 10, Random.Range(-100, 100));
            newCube.transform.LookAt(playerRigidbody.transform);
            cubeList.Add(newCube);
            loop++;
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(cam.position, cam.forward, out hit))
            {
                if (cubeList.Contains(hit.transform.gameObject))
                {
                    Destroy(hit.transform.gameObject);
                    cubeList.Remove(hit.transform.gameObject);
                }
            }
        }

        for (int i = 0; i < cubeList.Count; i++)
        {
            cubeList[i].transform.LookAt(playerRigidbody.transform);
            cubeList[i].transform.position += cubeList[i].transform.forward * 5 * Time.deltaTime;
            if (Vector3.Distance(cubeList[i].transform.position, playerRigidbody.transform.position) < 3)
            {
                Destroy(cubeList[i]);
                cubeList.RemoveAt(i);
                text.gameObject.SetActive(true);
                Debug.Log("Hit");
            }
        }
    }

    void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, cam.rotation.eulerAngles.y, 0));

        if (Input.GetKey(KeyCode.W))
        {
            playerRigidbody.AddRelativeForce(0, 0, 30);
        }
        if (Input.GetKey(KeyCode.A))
        {
            playerRigidbody.AddRelativeForce(-30, 0, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            playerRigidbody.AddRelativeForce(0, 0, -30);
        }
        if (Input.GetKey(KeyCode.D))
        {
            playerRigidbody.AddRelativeForce(30, 0, 0);
        }
    }
}