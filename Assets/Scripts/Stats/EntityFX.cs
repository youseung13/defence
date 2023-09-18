using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
      private SpriteRenderer[] sr;

      private SpriteRenderer shadowRenderer; //그림자는 특정한 색이 있어서 제외 해야한다

      [Header("Flash FX")]
      [SerializeField] private Material hitMat;
      private Material[] originalMat;

      [Header("Ailment colors")]
      [SerializeField] private Color[] chillColor;
      [SerializeField] private Color[] ignitedColor;
      [SerializeField] private Color[] shockColor;

      private void Start() {
         
         //그림자는 특정한 색이 있어서 제외 해야한다
         sr = GetComponentsInChildren<SpriteRenderer>();
         originalMat = new Material[sr.Length];

          foreach (SpriteRenderer renderer in sr)
        {
            if (renderer.gameObject.CompareTag("Shadow")) // "Shadow" 오브젝트 태그가 적용되어 있다고 가정합니다.
            {
                shadowRenderer = renderer;
            }
        }
      }

         
      public void MakeTransprent(bool _transprent)
      {
         if(_transprent)
         {
            foreach(SpriteRenderer _sr in sr)
            {
               _sr.color = new Color(1,1,1,.5f);
            }
         }
         else
         {
            foreach(SpriteRenderer _sr in sr)
            {
               _sr.color = Color.white;
            }
         }
         
      }


      private IEnumerator FlashFX()
      {
         foreach(SpriteRenderer _sr in sr)
         {
            _sr.material = hitMat;
         }
         foreach(SpriteRenderer _sr in sr)
         {
          Color currentColor = _sr.color;
         }
        
         foreach(SpriteRenderer _sr in sr)
         {
            _sr.color = Color.white;
         }
        

         yield return new WaitForSeconds(.2f);

         foreach(SpriteRenderer _sr in sr)
         {
            _sr.color = Color.white;
         }
         foreach(SpriteRenderer _sr in sr)
         {
            _sr.material = originalMat[0];
         }
         
      }

      private void RedColorBlink()
      {
         foreach(SpriteRenderer _sr in sr)
         {
            if(_sr.color != Color.white)
            _sr.color = Color.white;
            else
            _sr.color = Color.red;
         }

      }

      private void CancelColorChange()
      {
         CancelInvoke();
         
         foreach(SpriteRenderer _sr in sr)
         {
            _sr.color = Color.white;
         }

           shadowRenderer.color = new Color(0,0,0,143/255f);
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
         foreach(SpriteRenderer _sr in sr)
         {
            if(_sr.color != ignitedColor[0])
            _sr.color = ignitedColor[0];
            else
            _sr.color = ignitedColor[1];
         }

      }

      private void ChillColorFx()
      {
         foreach(SpriteRenderer _sr in sr)
         {
            if(_sr.color != chillColor[0])
            _sr.color = chillColor[0];
            else
            _sr.color = chillColor[1];
         }
      }

      private void ShockColorFx()
      {
         foreach(SpriteRenderer _sr in sr)
         {
            if(_sr.color != shockColor[0])
            _sr.color = shockColor[0];
            else
            _sr.color = shockColor[1];
         }
       
      }


}
