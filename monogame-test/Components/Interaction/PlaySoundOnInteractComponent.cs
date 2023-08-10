using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monogametest.Components;
public class PlaySoundOnInteractComponent : InteractableComponent
{
    public PlaySoundOnInteractComponent(SoundEffectCollection snd, RandomGradient vol, RandomGradient pitchh, bool isAlt = false)
    {
        sound = snd;
        volume = vol;
        pitch = pitchh;
        this.isAlt = isAlt;
    }
    public SoundEffectCollection sound;
    public RandomGradient volume = 1;
    public RandomGradient pitch = 0;
    public bool isAlt = false;
    public override void OnInteract(PlayerComponent player)
    {
        if (isAlt) return;
        sound.Play3DRandom(gameObject.position, volume, pitch);
    }
    public override void OnInteractAlt(PlayerComponent player)
    {
        if (!isAlt) return;
        sound.Play3DRandom(gameObject.position, volume, pitch);
    }
}

