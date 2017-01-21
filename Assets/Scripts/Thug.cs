﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Thug : MonoBehaviour {

    private float speed = 0.25f;
    private string state = "moving";
    private int hits = 0;
    
    private Hero hero;
	private SceneManager manager;
    
	private void Start() {
		this.manager = GameObject.Find("SceneManager").GetComponent<SceneManager>();
        this.hero = GameObject.Find("Hero").GetComponent<Hero>();
	}

    private void Update() {
        if(this.state == "moving") {
            this.Moving();
        } else if(this.state == "attacking") {
            this.Attacking();
        }
    }

    private void Moving() {
        // Normalize the delta.
        float delta = Time.deltaTime / (1f / 60f);
        
        // Calculate the distance to move.
        Vector2 movement = new Vector2(-1 * this.speed * delta, 0f);
        
        // Execute the movement.
        this.transform.Translate(movement);
        
        if(this.transform.position.x <= this.hero.transform.position.x + this.hero.transform.localScale.x) {
            this.state = "attacking";
        }
    }
    
    private void Attacking() {
        if(Input.GetButtonDown("Action")) {
            this.hero.beAttacked();
            hits += 1;
            if(hits >= 3) {
                this.Die();
            }
        }
    }
    
    private void Die() {
		manager.handleThugDeath();
        
        Object.Destroy(this.gameObject);
    }
}
