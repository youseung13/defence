using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HPBar : MonoBehaviour
{
    [SerializeField]
   private Slider HPbar;
     [SerializeField]
   private Enemy enemy;
  
    // Start is called before the first frame update

    private void Awake() 
    {
        
    }
    void Start()
    {
        HPbar.value = (float)enemy.hp/ (float)enemy.maxhp;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHPbar();
    }

    private void UpdateHPbar()
    {
        HPbar.value = (float)enemy.hp/ (float)enemy.maxhp;
    }
}
