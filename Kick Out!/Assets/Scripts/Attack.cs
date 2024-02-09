using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    //##############################################################################################################################################
    //Variable pour savoir le combattant est en train d'attaquer pour éviter qu'il lance plusieurs attaques en même temps
    private bool _isAttacking = false;
    
    //Le timer pour le délai entre les attaques
    private float _timeBetweenAttacks = 0;

    //##############################################################################################################################################
    //UNITY COMPONENTS
    public Animator animator;

    //##############################################################################################################################################
    //ANIMATION
    private bool _punch = false;
    private bool _jab = false;


    public AnimationClip jab;

    void Start()
    {

    }


    void Update()
    {
        // Gestion de l'animation
        jab.wrapMode = WrapMode.Once;

        //animator.SetBool("Jab", _jab);
        animator.SetBool("Punch", _punch);

        //Attaque
        Jab();
        Punch();

        BasicAttack();
    }

    void Jab() 
    {
        if(Input.GetKey(KeyCode.X) && _isAttacking == false)
        {
            animator.Play(jab.name);
            _isAttacking = true; 
            StartCoroutine(MyFunctionAfterDelay(jab.length));
            StopCoroutine(MyFunctionAfterDelay(jab.length));
            //_jab = true;
        }
    }

    void Punch() 
    {
        if(Input.GetKey(KeyCode.Z) && _isAttacking == false)
        {
            _isAttacking = true; 
            _punch = true;
        }
    }

    void BasicAttack() 
    {
        //Gére le délai entre les attaques de bases pour éviter de lancer plusieurs attaques à la fois

        if(_isAttacking) 
        {
            //Si le joueur lance une attaque, alors on lance un timer pour éviter qu'il relance une attaque 
            _timeBetweenAttacks += Time.deltaTime;

            //Dès que le timer atteint une certaine valeur (à modifer selon les combattants)
            if (_timeBetweenAttacks > .6f)
            {
                //On met en place un délai pour indiquer à la fin de l'animation de l'attaque que le combattant est prêt à attaquer à nouveau.
                //Pour réduire le délai entre les attaques (attaque speed), il faut augmenter les paramètres dans les fonctions suivantes
                StartCoroutine(MyFunctionAfterDelay(.1f));
                StopCoroutine(MyFunctionAfterDelay(.3f)); //Modifer ce paramètre pour réduire la vitesse d'attaque

                //On remet le timer à 0
                _timeBetweenAttacks = 0f;
            }
        }
    }

    IEnumerator MyFunctionAfterDelay(float delay)
    {
        //Cette fonction permet de déclencher une action après un certain temps (en lien avec StartCoroutine())

        yield return new WaitForSeconds(delay);

        _isAttacking = false;
        _jab = false;
        _punch = false;
    }
}
