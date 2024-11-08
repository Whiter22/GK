using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1")){
            GameObject go = Instantiate(bullet, transform.position, Quaternion.identity);

            go.GetComponent<Rigidbody>().AddForce(go.transform.forward * 1000);
        }
    }
}
