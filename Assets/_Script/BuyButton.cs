using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BuyButton : MonoBehaviour
{
    public Type CharacterType;
    private int _buttonIndex;
    public void OnPointerEnter()
    {
        if (ExpBar.Instance.CanUpdate())
        {
            Invoke("clicked", 1f);
            ShopManager.Instance.RunButtonProcess(_buttonIndex);

        }

    }
    private void Start()
    {
        SwitchType();
    }
    public void OnPointerExit()
    {
        ShopManager.Instance.CancelProcess(_buttonIndex);
        CancelInvoke("clicked");

    }
    private void SwitchType()
    {
        switch(CharacterType)
        {
            case Type.Cannon:
                _buttonIndex = 0;
                break;
            case Type.Archer:
                _buttonIndex = 1;
                break;
            case Type.Mage:
                _buttonIndex = 2;
                break;
            case Type.CannonTower:
                _buttonIndex = 3;
                break;

        }
            
    }
    public void OnPointerClick()
    {
        
        
    }

    
    private void clicked()
    {
        CharacterManager.Instance.UpgradeCharacter(CharacterType,_buttonIndex);
    }
}
