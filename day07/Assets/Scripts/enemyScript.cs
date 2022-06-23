using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using UnityEngine.UI;

public class enemyScript : tank {

	public AudioSource shotSound;
	public AudioSource shotHit;
	public ParticleSystem gunShotParticle;
	public ParticleSystem hitShotParticle;
	public Text missileText;


	private GameObject target = null;
	private bool isInRange = false;
	private NavMeshAgent navMesh;
	private DateTime timeShot;
	private Vector3 vectorTarget;

	void Start () {
		this.navMesh = GetComponent<NavMeshAgent>();
		this.timeShot = DateTime.Now;
		this.missileText.text = "0 ✐";
		this.lifeText.text = this.life + " ❤";
	}
	
	void Update () {
		if (this.isDead)
			return;
		this.getClosestEnemy();
		if (this.target == null)
			return;
		if (this.isInRange
		    && transform.position.y < target.transform.position.y + 0.5f
		    && transform.position.y > target.transform.position.y + -0.5f)
		{
			this.navMesh.destination = transform.position;
			RaycastHit ray;
			bool rayCasthit = Physics.Raycast(transform.position, transform.forward, out ray, 100);
			Vector3 direction = this.vectorTarget - transform.position;
			Quaternion rotation = Quaternion.LookRotation(direction);

			if (Convert.ToInt32(transform.rotation.eulerAngles.y) <= Convert.ToInt32(rotation.eulerAngles.y) + 1
			    && Convert.ToInt32(transform.rotation.eulerAngles.y) >= Convert.ToInt32(rotation.eulerAngles.y) - 1
			    && (DateTime.Now - this.timeShot).TotalSeconds > 1)
			{
				this.timeShot = DateTime.Now;
				this.shotSound.Play();
				this.gunShotParticle.Play();

				if (rayCasthit)
					this.hitShotParticle.transform.position = ray.point;
				this.hitShotParticle.Play();
				this.shotHit.Play();
				if (rayCasthit && ray.collider.gameObject == this.target)
					ray.collider.gameObject.GetComponent<tank>().getHit(this.mitrailletteDamage);
				else
					this.vectorTarget = this.target.transform.position;
			}
			else
				transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
		}
		else
			this.navMesh.destination = target.transform.position;
	}

	private void OnTriggerStay(Collider other)
	{
		if (this.target != null && this.target == other.gameObject)
		{
			this.isInRange = true;
		}
	}

	private void getClosestEnemy()
	{
		GameObject save = null;
		float distance = 9999999;
		foreach (GameObject g in gameManager.instance.enemy)
		{
			if (g == null || g == this.gameObject || g.GetComponent<tank>().isDead)
				continue;
			float d = Vector3.Distance(g.transform.position, transform.position);
			if (d < distance)
			{
				distance = d;
				save = g;
			}
		}
		if (this.target != save)
		{
			this.target = save;
			this.isInRange = false;
			//this.vectorTarget = this.target.transform.position;
		}
		else
			this.target = save;
	}
}
