using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shootScript : MonoBehaviour {

	public SpriteRenderer crosshair;
    public AudioSource gunShot;
    public AudioSource missShot;
    public ParticleSystem gunShotParticle;
    public ParticleSystem missileShotParticle;
    public ParticleSystem missShotParticle;
	public Text missileText;
    public int missileCount = 5;

	private bool isChangingCrosshair = false;

	// Use this for initialization
	void Start () {
		this.missileText.text = this.missileCount + " ✐";
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
		{
            // Audio
            this.gunShot.Play();
            this.gunShotParticle.Play();

            RaycastHit ray;
            if (Physics.Raycast(transform.position, transform.forward, out ray, 100))
            {
                this.missShotParticle.transform.position = ray.point;
                this.missShotParticle.Play();
                this.missShot.Play();
				if (ray.collider.gameObject.tag == "Enemy")
				{
					ray.collider.gameObject.GetComponent<tank>().getHit(15);
					if (!this.isChangingCrosshair)
					{
						this.isChangingCrosshair = true;
						StartCoroutine(this.hitCrosshair());
					}
				}
            }
		}
		else if (Input.GetMouseButtonDown(1))
		{
            if (this.missileCount <= 0)
                return;

            --this.missileCount;
			this.missileText.text = this.missileCount + " ✐";


            this.gunShot.Play();
            this.missileShotParticle.Play();
            RaycastHit ray;
            if (Physics.Raycast(transform.position, transform.forward, out ray, 100))
            {
                this.missShotParticle.transform.position = ray.point;
                this.missShotParticle.Play();
				if (ray.collider.gameObject.tag == "Enemy")
				{
					ray.collider.gameObject.GetComponent<tank>().getHit(30);
					if (!this.isChangingCrosshair)
					{
						this.isChangingCrosshair = true;
						StartCoroutine(this.hitCrosshair());
					}
				}
            }
			this.missShot.Play();
        }
	}

	IEnumerator hitCrosshair()
	{
		Color save = this.crosshair.color;
		this.crosshair.color = new Color(206f/255f, 47f/255f, 0);
		yield return new WaitForSeconds(1);
		this.crosshair.color = new Color(0, 158f/255f, 14f/255f);
		this.isChangingCrosshair = false;
	}
}
