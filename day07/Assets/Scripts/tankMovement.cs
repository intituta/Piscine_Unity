using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tankMovement : tank {


    public int nitro = 100;
    public float speed = 5;
    private CharacterController cc;
    private Coroutine looseNitro;
    private Coroutine reloadNitro;
    private bool looseNitroRoutine = false;
    private bool reloadNitroRoutine = false;

	// Use this for initialization
	void Start () {
        this.cc = GetComponent<CharacterController>();
		this.lifeText.text = this.life + " ❤";
	}
	
	// Update is called once per frame
	void Update () {

		transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0)); // Force a rester droit

        // Gestion routine de la nitro
        if (!this.looseNitroRoutine && Input.GetKey("left shift"))
        {
            if (this.reloadNitroRoutine)
            {
				StopCoroutine(this.reloadNitro);
                this.reloadNitroRoutine = false;            
            }
            this.looseNitroRoutine = true;
            this.looseNitro = StartCoroutine(this.looseNitroFn());
        }
        else if (this.looseNitroRoutine && !Input.GetKey("left shift"))
        {
            StopCoroutine(this.looseNitro);
			this.looseNitroRoutine = false;
            this.reloadNitro = StartCoroutine(this.loadNitro());
            this.reloadNitroRoutine = true;
        }


        // Move avant/arriere
        Vector3 v;
        if (Input.GetKey(KeyCode.W))
        {
            v = transform.rotation * Vector3.forward;
            if (this.looseNitroRoutine && this.nitro > 0)
                this.cc.SimpleMove(v * (this.speed + 3f));
            else
                this.cc.SimpleMove(v * this.speed);
            Camera.main.GetComponent<cameraScript>().move();
        }
        else if (Input.GetKey(KeyCode.S))
        {
            v = transform.rotation * Vector3.forward * -1;
            if (this.looseNitroRoutine && this.nitro > 0)
                this.cc.SimpleMove(v * (this.speed + 3f));
            else
                this.cc.SimpleMove(v * this.speed);
        }

        // Move rotation, sur les cote
        v = new Vector3();
        if (Input.GetKey(KeyCode.A)) // gauche
            v += new Vector3(0, -50, 0);
        if (Input.GetKey(KeyCode.D))
            v += new Vector3(0, 50, 0); // droite
        if (v != Vector3.zero)
            transform.Rotate(v * Time.deltaTime);
    }

    IEnumerator looseNitroFn()
    {
        while (true)
        {
            if (this.nitro <= 0)
            {
                yield return new WaitForSeconds(1f);
                Debug.Log("No more nitro");
				continue;
            }
            this.nitro -= 4; // 2.5s
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator loadNitro()
    {
        while (true)
        {
            if (this.nitro >= 100)
                break;
            this.nitro += 2; // 4s
            yield return new WaitForSeconds(0.1f);
        }
        Debug.Log("Full nitro");
        this.reloadNitroRoutine = false;
    }
}
