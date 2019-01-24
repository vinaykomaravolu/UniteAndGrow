﻿using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

// in charge of size and liquid or solid
public class FormControl : MonoBehaviour{

    public float volume;
    public float minVolume => sizeToVolume(minSize);
    public float maxVolume => sizeToVolume(maxSize);
    private float size => volumeToSize(volume);
    public float minSize;
    public float maxSize;
    
    public bool isWater;
    
    public float waterDropVolumeDelta; // how much volume change per contact
    public float blockVolumeDelta; // how much volume change per second

    private float volumeToSize(float volume){
        return Mathf.Pow(volume, 1/3);
    }

    private float sizeToVolume(float size){
        return size * size * size;
    }

    private void changeVolume(float change){
        volume += change;
        if (volume > maxVolume) volume = maxVolume;
        transform.localScale = new Vector3(size, size, size);
    }

    private void OnTriggerEnter(Collider other){
        if (other.CompareTag(Global.waterDropTag)){
            changeVolume(waterDropVolumeDelta);
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionStay(Collision collision){
        if (collision.gameObject.CompareTag(Global.increaseBlockTag)) changeVolume(blockVolumeDelta);
        if (collision.gameObject.CompareTag(Global.decreaseBlockTag)) changeVolume(-blockVolumeDelta);
    }
}
