using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    
    [Header("Flash FX")] public float flashDuration;
    [SerializeField] private Material flashMaterial;
    private Material originalMaterial;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;
    }

    public IEnumerator FlashFX()
    {
        spriteRenderer.material = flashMaterial;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.material = originalMaterial;
    }

    public void StunBlinkFX()
    {
        spriteRenderer.color = spriteRenderer.color != Color.white ? Color.white : Color.red;
    }
    
    public void ResetStunBlinkFX()
    {
        CancelInvoke();
        spriteRenderer.color = Color.white;
    }
}
