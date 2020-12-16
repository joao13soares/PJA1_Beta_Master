using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimationManager
{
    private Animation animationComponent;

    private AnimationClip holsterWeaponAnimation;
    private AnimationClip drawWeaponAnimation;
    private AnimationClip reloadWeaponAnimation;

    public bool IsAnimationPlaying => animationComponent.isPlaying;


    public WeaponAnimationManager(Animation animationComponent, AnimationClip holsterWeaponAnimation,
        AnimationClip drawWeaponAnimation, AnimationClip reloadWeaponAnimation)
    {
        this.animationComponent = animationComponent;
        this.holsterWeaponAnimation = holsterWeaponAnimation;
        this.drawWeaponAnimation = drawWeaponAnimation;
        this.reloadWeaponAnimation = reloadWeaponAnimation;
    }

    public void PlayHolsterAnimation() => PlayAnimation(holsterWeaponAnimation);

    public void PlayDrawAnimation() => PlayAnimation(drawWeaponAnimation);
    public void PlayReloadAnimation() => PlayAnimation(reloadWeaponAnimation);

    private void PlayAnimation(AnimationClip animationToPlay)
    {
        animationComponent.clip = animationToPlay;
        animationComponent.Play();
    }
}