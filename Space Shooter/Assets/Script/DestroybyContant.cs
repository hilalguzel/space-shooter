using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroybyContant : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;

    public GameController gameController;

    private void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }


    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);

        if (other.gameObject.tag == "Boundary")
        {
            return;
        }
        Instantiate(explosion, transform.position, transform.rotation); //Atroid patlaması efekti için
        if(other.tag == "Palyer") //Sadece tag da kullanabilirsin
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gameController.GameOver();
        }
        Destroy(other.gameObject); //Collider destroy için
        Destroy(gameObject); //ışın astroide çarğtığı an patlasın diye
        gameController.UpdateScore();
       
    }
}
