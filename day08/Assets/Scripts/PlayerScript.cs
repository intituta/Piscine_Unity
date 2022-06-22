﻿using System.Collections;

    // Player ui references
    public Slider lifeSlider;
    {
        if (state != State.ATTACKING
            && other.gameObject.GetComponent<CharacterScript>().state != State.DEAD
            enemyTarget = other.gameObject;
        }
    }

        // Determine wich value needs to be displayed
        if (enemyTarget)
            enemyInfosPanel.SetActive(true);
            enemyLifeSlider.maxValue = enemyToDisplay.maxLife;
        }

        // Updating UI
        UpdateUi();

        // Sets player click movement instructions
        if (Input.GetMouseButtonDown(0)
            && Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out clickHit)
            && !clickHit.collider.gameObject.CompareTag("Enemy"))
        {
            navMeshAgent.SetDestination(clickHit.point);
            prioritaryWaypoint = true;
            enemyTarget = null;
        }