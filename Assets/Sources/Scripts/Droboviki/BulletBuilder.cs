using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBuilder : MonoBehaviour,BuilderBase
{

    
    public bool MagazineInWeapon{get;set;}
    public bool MagazineInTrigger{get; set; }
    public bool Enabled{get; set; }
    private bool _onMouse;
    private Vector3 _startPosition;
    private Vector3 _cursorPosition;
    public bool IsCollider{get; set; }
    [SerializeField] private Weapon _weapon;


   
 
     public Action OnTrigger { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public Action OnReloadMagazine { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
   

    private void Start()
    {
      _onMouse = false;
      _startPosition = transform.position;
    }

    private void OnMouseDown()
    { 
      _onMouse = true;
    }
    private void OnMouseUp()
    {
        _onMouse = false;
    }

    private void LateUpdate()
    {
      SetEnabled();
      
        _cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _cursorPosition.z = 0;

      // var  _isGrounded = Physics2D.(transform.position,_groundDistance,_groundMask);

      /* if(_isGrounded == true)
          _isCollider = true;*/

        
          if (_onMouse == true)
           {

            if(IsCollider == false)
            {
              transform.position = _cursorPosition;

            }
              else if(IsCollider == true)
             {
              
             // _isInParent = true;
              _onMouse = false;
              //transform.position = Vector3.zero;
             // _isCollider = true;
               MagazineInTrigger = true;
              

              /* if(_onMouse == true)
               {
                  OnReloadMagazine?.Invoke();
               }*/
              //transform.localPosition = Vector3.zero;
             // _weapon.CheckOnMagazineAnimation();
              //MagazineInWeapon = true;
              //return;
 
           }

           }
        
        /*else
        {
          if(_onMouse == true)
          {
             transform.position = _cursorPosition;
          }
          else if(_onMouse == false && _isCollider == false)
          {
             _isInParent = false;
          }
        }*/
        
       
    }  

    private void OnTriggerEnter2D( Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("Spawn"))
        {
            IsCollider = true;
        }
    }
   /* private void OnTriggerExit2D(Collider2D collision) 
     {
         if (collision.gameObject.CompareTag("Spawn"))
        {
            _isCollider = false;
           
        }
     }
*/
    private IEnumerator Delay()
    {
       yield return new WaitForSeconds(0.8f);
    }

    public void SetEnabled()
    {
        if(Enabled == true)     
           this.enabled = true;
        
        else
          this.enabled = false;
        
    }
}
