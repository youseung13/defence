﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
      private SpriteRenderer sr;

      [Header("Flash FX")]
      [SerializeField] private Material hitMat;
      private Material originalMat;

      [Header("Ailment colors")]
      [SerializeField] private Color[] chillColor;
      [SerializeField] private Color[] ignitedColor;
      [SerializeField] private Color[] shockColor;

      private void Start() {
         
         sr = GetComponentInChildren<SpriteRenderer>();
         originalMat = sr.material;
      }

         
      public void MakeTransprent(bool _transprent)
      {
         if(_transprent)
            sr.color = Color.clear;
         else
            sr.color = Color.white;
      }


      private IEnumerator FlashFX()
      {
         sr.material = hitMat;
         Color currentColor = sr.color;

         sr.color = Color.white;

         yield return new WaitForSeconds(.2f);

         sr.color = currentColor;
         sr.material = originalMat;
         
         
      }

      private void RedColorBlink()
      {
         if(sr.color != Color.white)
         sr.color = Color.white;
         else
         sr.color = Color.red;

      }

      private void CancelColorChange()
      {
         CancelInvoke();
         sr.color = Color.white;
      }


      public void IgniteFxFor(float _seconds)
      {
         InvokeRepeating("IgniteColorFx",0,.15f);
         Invoke("CancelColorChange", _seconds);
      }

      public void ChillFxFor(float _seconds)
      {
         InvokeRepeating("ChillColorFx",0, .15f);
         Invoke("CancelColorChange", _seconds);
      }


      public void ShockFxFor(float _seconds)
      {
         InvokeRepeating("ShockColorFx",0,.15f);
         Invoke("CancelColorChange", _seconds);
      }


      private void IgniteColorFx()
      {
         if(sr.color != ignitedColor[0])
            sr.color = ignitedColor[0];
         else
            sr.color = ignitedColor[1];
      }

      private void ChillColorFx()
      {
         if(sr.color != chillColor[0])
            sr.color = chillColor[0];
         else
            sr.color = chillColor[1];
      }

      private void ShockColorFx()
      {
         if(sr.color != shockColor[0])
            sr.color = shockColor[0];
         else
            sr.color = shockColor[1];
      }


}
