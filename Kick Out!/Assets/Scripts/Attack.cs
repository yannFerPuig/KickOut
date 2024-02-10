using System.Collections;
using System.Collections.Generic;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine;

public class Attack : MonoBehaviour
{
    //##############################################################################################################################################
    //Variable pour savoir le combattant est en train d'attaquer pour éviter qu'il lance plusieurs attaques en même temps
    private bool _isAttacking = false;
    
    //Le timer pour le délai entre les attaques
    private float _timer = 0;

    public float _attackSpeed;

    //##############################################################################################################################################
    //UNITY COMPONENTS
    public Animator animator;

    //##############################################################################################################################################
    //ANIMATION
    private bool _punch = false;
    private bool _jab = false;

    public AnimationClip jab;
    public AnimationClip punch;

    void Start()
    {

    }


    void Update()
    {
        // Gestion de l'animation
        animator.SetBool("Jab", _jab);
        animator.SetBool("Punch", _punch);

        //Attaque
        Jab();
        Punch();

        AttackSpeed(_attackSpeed);
    }

    void Jab() 
    {
        if(Input.GetKey(KeyCode.X) && _isAttacking == false)
        {
            _jab = true;
            _isAttacking = true; 
            StartCoroutine(MyFunctionAfterDelay(jab.length));
        }
    }

    void Punch() 
    {
        if(Input.GetKey(KeyCode.Z) && _isAttacking == false)
        {
            _isAttacking = true; 
            _punch = true;
            StartCoroutine(MyFunctionAfterDelay(punch.length));
        }
    }


    void AttackSpeed(float attackSpeed) 
    {
        //Gére la notion de vitesse d'attaque

        if (_isAttacking) 
        {
            //Lance un timer quand le joueur lance une attaque
            _timer += Time.deltaTime;

            if (_timer > attackSpeed)
            {
                //Lorsque le timer dépasse une certaine valeur, le combattant est marqué comme disponible à attaquer à nouveau
                _isAttacking = false;
                _timer = 0; //On reset le timer pour la prochaine attaque
            }
        }
    }

    IEnumerator MyFunctionAfterDelay(float delay)
    {
        //Cette fonction permet de déclencher une action après un certain temps (en lien avec StartCoroutine())
        //Gére le délai entre les attaques de bases pour éviter de lancer plusieurs attaques à la fois

        yield return new WaitForSeconds(delay);

        //_isAttacking = false;
        _jab = false;
        _punch = false;
    }
}
